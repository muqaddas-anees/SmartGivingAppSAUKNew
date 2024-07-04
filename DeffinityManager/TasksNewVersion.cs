using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Web.Script.Serialization;
using System.Data.Linq;
using System.Collections;
using System.ServiceModel.Web;
using ProjectMgt.BLL;

/// <summary>
/// Summary description for TasksNewVersion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TasksNewVersion : System.Web.Services.WebService
{
    readonly projectTaskDataContext _db = new projectTaskDataContext();
    public TasksNewVersion()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class NestedTaskModel : ProjectMgt.Entity.ProjectTasks
    {
        public List<NestedTaskModel> children = null;
        public bool leaf = true;
        public bool expanded;

        public NestedTaskModel() : base() { }

        public NestedTaskModel(ProjectMgt.Entity.ProjectTasks t)
        {
            this.Id = t.Id;
            this.depth = t.depth;
            this.parentId = t.parentId;
            this.StartDate = t.StartDate;
            this.EndDate = t.EndDate;
            this.Duration = t.Duration;
            this.DurationUnit = t.DurationUnit;
            this.PercentDone = t.PercentDone;
            this.Priority = t.Priority;
            this.Name = t.Name;
            this.index = t.index;
            this.Id = t.Id;
            this.ProjectReference = t.ProjectReference;
            this.StatusID = t.StatusID;
            this.RAG = t.RAG;
            this.RAGRequired = t.RAGRequired.Trim();
            this.ProjectStartDate = t.ProjectStartDate;
            this.ProjectEndDate = t.ProjectEndDate;
            this.QA = t.QA.Trim();
            this.CheckPointID = t.CheckPointID;
            this.ListPosition = t.ListPosition;
            this.PID = t.PID;
            this.Notes = t.Notes;
            this.AssignedResources = t.AssignedResources;
            this.AssignedResourceNames = t.AssignedResourceNames;
            this.TeamID = t.TeamID;
            this.CategoryID = t.CategoryID;
            this.TeamIds = t.TeamIds;
            //this.TeamNames = t.TeamNames;
            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object Get()
    {

        string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        //projectTaskDataContext _db = new projectTaskDataContext();
        var manyTasks = GetProjectTaskData(ProjectReference, true).ToList();
        var rootTasks = manyTasks.Where(b => !b.parentId.HasValue && b.Id != -99 && b.ProjectReference == int.Parse(ProjectReference)).ToList();
        // var rootTasks = _db.ProjectTask_Task2(int.Parse(ProjectReference)).ToList();
        List<NestedTaskModel> roots = new List<NestedTaskModel>();


        NestedTaskModel n = null;
        foreach (ProjectMgt.Entity.ProjectTasks cd in rootTasks)
        {
            n = new NestedTaskModel(cd);
            roots.Add(n);
            this.SetNodeChildren(n, int.Parse(ProjectReference));
        }

        return roots;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Create(ProjectMgt.Entity.ProjectTasks[] jsonData)
    {
        try
        {
            using (projectTaskDataContext _db = new projectTaskDataContext())
            {
                //projectTaskDataContext _db = new projectTaskDataContext();
                // _db.Tasks.InsertAllOnSubmit(jsonData);
                // Get Project status
                int status = _db.ProjectDetails.Where(p => p.ProjectReference == jsonData[0].ProjectReference.Value).Select(p => p.ProjectStatusID.Value).FirstOrDefault();

                ProjectTaskItem insert = new ProjectTaskItem();
                insert.ProjectReference = jsonData[0].ProjectReference;
                //if the status is pending
                if (status == 1)
                {
                    insert.ProjectEndDate = jsonData[0].ProjectEndDate;
                    insert.ProjectStartDate = jsonData[0].ProjectStartDate;
                    insert.StartDate = jsonData[0].ProjectStartDate;
                    insert.CompletionDate = jsonData[0].ProjectEndDate;
                }
                else
                {
                    insert.ProjectStartDate = jsonData[0].ProjectStartDate;
                    insert.ProjectEndDate = jsonData[0].ProjectEndDate;
                    insert.StartDate = jsonData[0].StartDate.HasValue ? jsonData[0].StartDate : jsonData[0].ProjectStartDate;
                    insert.CompletionDate = jsonData[0].EndDate.HasValue ? jsonData[0].EndDate : jsonData[0].ProjectEndDate;
                }
                insert.depth = jsonData[0].depth;
                insert.Duration = jsonData[0].Duration;
                insert.DurationUnit = "d";
                insert.QA = jsonData[0].QA;
                insert.RAGRequired = jsonData[0].RAGRequired;
                insert.RAGStatus = jsonData[0].RAG;
                insert.ItemStatus = jsonData[0].StatusID;
                insert.ItemDescription = jsonData[0].Name;
                insert.index = jsonData[0].index;
                insert.ParentID = jsonData[0].parentId;
                insert.PercentComplete = jsonData[0].PercentDone.ToString();
                insert.AssignedResources = "";
                insert.TeamID = jsonData[0].TeamID;
                insert.CategoryID = jsonData[0].CategoryID;
                insert.PID = jsonData[0].PID;
                if (jsonData[0].Duration > 0)
                {
                    insert.isMilestone = false;
                }
                else
                {
                    insert.isMilestone = true;
                }

                insert.ListPosition = jsonData[0].ListPosition;
                _db.ProjectTaskItems.InsertOnSubmit(insert);
                _db.SubmitChanges(ConflictMode.ContinueOnConflict);
                jsonData[0].Id = insert.ID;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Update(ProjectMgt.Entity.ProjectTasks[] jsonData)
    {
        try
        {
            List<ProjectAssignedTeam> TeamAddCollection = new List<ProjectAssignedTeam>();
            List<ProjectAssignedTeam> TeamDeleteCollection = new List<ProjectAssignedTeam>();
            projectTaskDataContext _db = new projectTaskDataContext();

            foreach (ProjectMgt.Entity.ProjectTasks vals in jsonData)
            {

                var projectJournal = (from r in _db.ProjectTaskJournals
                                      where r.TaskID == vals.Id && ((r.TaskStatus == vals.StatusID && r.ActualStartDate == vals.StartDate
                                      && r.ActualEndDate == vals.EndDate && r.PercentComplete == vals.PercentDone.ToString()
                                      && r.EndDate == vals.ProjectEndDate && r.StartDate == vals.ProjectStartDate)
                                     )
                                      select r).ToList();
                if (projectJournal != null)
                {
                    if (projectJournal.Count == 0)
                    {
                        try
                        {
                            ProjectTaskJournal insert = new ProjectTaskJournal();
                            insert.TaskStatus = vals.StatusID;
                            insert.Projectreference = vals.ProjectReference;
                            insert.StartDate = vals.ProjectStartDate;
                            insert.EndDate = vals.ProjectEndDate;
                            insert.ActualStartDate = vals.StartDate;
                            insert.ActualEndDate = vals.EndDate;
                            insert.PercentComplete = vals.PercentDone.ToString();
                            insert.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            insert.ChangedbyUserID = sessionKeys.UID;
                            insert.TaskID = vals.Id;

                            _db.ProjectTaskJournals.InsertOnSubmit(insert);
                            _db.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                    }

                }

                ProjectTaskItem t = _db.ProjectTaskItems.SingleOrDefault(b => b.ID == vals.Id);

                if (t != null)
                {

                    int status = _db.ProjectDetails.Where(p => p.ProjectReference == jsonData[0].ProjectReference.Value).Select(p => p.ProjectStatusID.Value).FirstOrDefault();
                    //if the status is pending
                    if (status == 1)
                    {
                        t.StartDate = vals.StartDate;
                        t.CompletionDate = vals.EndDate;
                        t.ProjectStartDate = vals.StartDate;
                        t.ProjectEndDate = vals.EndDate;
                    }
                    else
                    {
                        t.StartDate = vals.StartDate.HasValue ? vals.StartDate : vals.ProjectStartDate;
                        t.CompletionDate = vals.EndDate.HasValue ? vals.EndDate : vals.ProjectEndDate;
                        t.ProjectStartDate = vals.ProjectStartDate;
                        t.ProjectEndDate = vals.ProjectEndDate;
                    }
                    t.ItemDescription = vals.Name;
                    t.index = vals.index;
                    t.depth = vals.depth;
                    t.ParentID = vals.parentId;
                   
                    TimeSpan ts;
                    if (vals.StatusID == 2)
                    {
                        ts = (vals.EndDate.Value - vals.StartDate.Value);
                    }
                    else
                    {
                        ts = (vals.ProjectEndDate.Value - vals.ProjectStartDate.Value);
                    }

                   
                    t.DurationUnit = vals.DurationUnit;
                    t.PercentComplete = vals.PercentDone.ToString();

                    t.Priority = "Normal";// vals.Priority.ToString();

                    t.QA = vals.QA;
                    t.CategoryID = vals.CategoryID;
                   
                    t.RAGRequired = vals.RAGRequired;
                    t.RAGStatus = vals.RAG;
                    t.ItemStatus = vals.StatusID;

                    //if (((t.TeamID.HasValue ? t.TeamID : 0) != vals.TeamID) && ((t.TeamID.HasValue ? t.TeamID : 0) != 0))
                    //{
                    //    DeleteTeam(vals.Id);
                    //}
                    //if ((t.TeamID.HasValue?t.TeamID:0) != vals.TeamID)
                    //{
                    //    AssignedTeam(vals.TeamID, vals.Id, jsonData[0].ProjectReference.Value.ToString());
                    //}
                    t.TeamID = 0;// vals.TeamID;

                    if (vals.Duration > 0)
                    {
                        t.isMilestone = false;
                    }
                    else
                    {
                        t.isMilestone = true;
                    }
                    if (t.QA == "Y")
                    {
                        t.isMilestone = true;
                        t.StartDate = t.CompletionDate;
                        ts = (t.CompletionDate.Value - t.StartDate.Value);
                        if (vals.StatusID == 1)
                        {
                            t.ProjectStartDate = t.StartDate;
                            t.ProjectEndDate = t.CompletionDate;
                        }

                        //Checklist items InsertUpdate
                        //ChecklistItemsInsertUpdate(jsonData[0].ProjectReference.Value);
                    }
                    if (t.PercentComplete == "100")
                        t.ItemStatus = 3;
                   
                    t.Notes = vals.Notes;
                    t.Duration = ts.Days;
                    //t.CheckPointID = vals.CheckPointID;
                    jsonData[0].QA = t.QA;
                    jsonData[0].Notes = t.Notes;
                    jsonData[0].StartDate = t.StartDate;
                    jsonData[0].Duration = ts.Days;
                    jsonData[0].StatusID = t.ItemStatus;
                }
                //Team update
                if (vals.TeamIds != string.Empty)
                {
                    //get the existing team collection
                    var teamCollection = _db.ProjectAssignedTeams.Where(o => o.TaskID == vals.Id).ToList();
                    var assignedResource = _db.ProjectItems.Where(o => o.ItemReference == vals.Id).ToList();
                    var TeamIDs = vals.TeamIds.Split(',');
                    //delete the teams with not exists
                    var teams_to_delete = (from o in teamCollection
                                           where !TeamIDs.Contains(o.TeamID.Value.ToString())
                                           select o).ToList();
                    //delete the teams which are selected
                    DeleteTeams(_db, vals, teams_to_delete);
                    addTeams(TeamAddCollection, _db, vals, teamCollection, TeamIDs);
                  
                   


                }
                else
                {
                    var teamCollection = _db.ProjectAssignedTeams.Where(o => o.TaskID == vals.Id).ToList();
                    var TeamIDs = vals.TeamIds.Split(',');
                    //delete the teams with not exists
                    var teams_to_delete = (from o in teamCollection
                                           where !TeamIDs.Contains(o.TeamID.Value.ToString())
                                           select o).ToList();
                    //delete the teams which are selected
                    DeleteTeams(_db, vals, teams_to_delete);

                }
            }

            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
            //jsonData[0].Duration=t.
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonData;
    }

    private static void DeleteTeams(projectTaskDataContext _db, ProjectMgt.Entity.ProjectTasks vals, List<ProjectAssignedTeam> teams_to_delete)
    {
        if (teams_to_delete != null)
        {
            if ((teams_to_delete as List<ProjectAssignedTeam>).Count() > 0)
            {
                _db.ProjectAssignedTeams.DeleteAllOnSubmit(teams_to_delete);
                _db.SubmitChanges(ConflictMode.ContinueOnConflict);
                foreach (ProjectAssignedTeam o in teams_to_delete)
                {
                    var d_resource = _db.TeamMembers.Where(q => q.TeamID == o.TeamID).Select(q => q.Name.Value).ToArray();
                    var resourceTasks = from q in _db.ProjectItems
                                        where d_resource.Contains(q.ContractorID.Value) && q.ItemReference == vals.Id
                                        select q;
                    _db.ProjectItems.DeleteAllOnSubmit(resourceTasks);
                    _db.SubmitChanges(ConflictMode.ContinueOnConflict);
                }
            }
        }
    }

    private void addTeams(List<ProjectAssignedTeam> TeamAddCollection, projectTaskDataContext _db, ProjectMgt.Entity.ProjectTasks vals, List<ProjectAssignedTeam> teamCollection, string[] TeamIDs)
    {
        foreach (string teamid in TeamIDs)
        {
            if (teamCollection.Where(o => o.TeamID == Convert.ToInt32(teamid)).Count() == 0)
            {
                //if team not exists add 
                TeamAddCollection.Add(new ProjectAssignedTeam() { ProjectReference = vals.ProjectReference, TaskID = vals.Id, TeamID = Convert.ToInt32(teamid) });
                //var team_resource = _db.TeamMembers.Where(o=>o.TeamID == Convert.ToInt32(teamid))
                AssignedTeam(Convert.ToInt32(teamid), vals.Id, vals.ProjectReference.Value.ToString());
            }
        }
        //insert new teams at a time
        if (TeamAddCollection.Count > 0)
        {
            _db.ProjectAssignedTeams.InsertAllOnSubmit(TeamAddCollection);
            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
    public string  CheckList(int Id)
    {

        //UserDataContext _db = new UserDataContext();
        //var resources = (from r in _db.Contractors
        //                 where !sid.Contains(r.SID.Value)
        //                 orderby r.ContractorName
        //                 select new { Name = r.ContractorName, Id = r.ID }).ToList();

        projectTaskDataContext pd = new projectTaskDataContext();
        var result = (from pc in pd.MasterTemplates
                      where (pc.ChecklistType == 8)
                      select new { pc.ID,pc.Description}).ToList();
        //return result;
        //return _db.Contractors.Where(c => sid.Contains(c.SID)).ToList();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        JavaScriptSerializer jss = new JavaScriptSerializer();

        string json = jss.Serialize(result);

        return json;
       
    }
    

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Delete(ProjectMgt.Entity.ProjectTasks[] jsonData)
    {
        try
        {
            projectTaskDataContext _db = new projectTaskDataContext();
            //projectTaskDataContext _db = new projectTaskDataContext();
            foreach (ProjectMgt.Entity.ProjectTasks t in jsonData)
            {
                ProjectTaskItem task = _db.ProjectTaskItems.SingleOrDefault(b => b.ID == t.Id);

                if (task != null)
                {
                    //var deps = _db.Dependencies.Where(b => (b.To == t.Id || b.From == t.Id));
                    //_db.Dependencies.DeleteAllOnSubmit(deps);
                    //_db.Tasks.DeleteOnSubmit(task);
                    var deps = _db.ProjectTaskDependencies.Where(b => (b.TaskDependingOn == t.Id || b.TaskDependent == t.Id));
                    _db.ProjectTaskDependencies.DeleteAllOnSubmit(deps);
                    _db.ProjectTaskItems.DeleteOnSubmit(task);

                    var resources = _db.ProjectItems.Where(b => b.ItemReference == t.Id);
                    _db.ProjectItems.DeleteAllOnSubmit(resources);
                }
            }
            _db.SubmitChanges();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return new { success = true };
    }


    public void SetNodeChildren(NestedTaskModel node, int projectRef)
    {
        try
        {
            //ProjectMgt.Entity.ProjectTasks d = new ProjectMgt.Entity.ProjectTasks();
            //projectTaskDataContext _db = new projectTaskDataContext();
            // projectTaskDataContext _db = new projectTaskDataContext();
            // var manyTasks1 = GetProjectTaskData(projectRef.ToString()).ToList();
            List<ProjectMgt.Entity.ProjectTasks> manyTasks1 = GetProjectTaskData(projectRef.ToString(), false).ToList();
            //List<ProjectMgt.Entity.ProjectTasks> manyTasks1 = manyTasks.ToList();
            var children = manyTasks1.Where(b => b.parentId == node.Id && b.ProjectReference == projectRef).ToList(); //_db.ProjectTasks.Where(b => b.parentId == node.Id && b.ProjectReference == projectRef);
            NestedTaskModel n = null;
            if (children.Count<ProjectMgt.Entity.ProjectTasks>() > 0)
            {
                node.children = new List<NestedTaskModel>();

                foreach (ProjectMgt.Entity.ProjectTasks t in children)
                {
                    n = new NestedTaskModel(t);
                    node.children.Add(n);
                    this.SetNodeChildren(n, projectRef);
                }

                // Last step, sort children on the 'index' field
                node.children = node.children.OrderBy(a => a.index).ToList();
            }
            node.leaf = (node.children == null);
            node.expanded = false;//!node.leaf;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public List<ProjectMgt.Entity.ProjectTasks> GetProjectTaskData(string Projectref, bool firsttime)
    {

        //using (projectTaskDataContext _db = new projectTaskDataContext())
        //{
        string c_key = "project" + Projectref;
        if (firsttime)
            BaseCache.Cache_Remove(c_key);

        if (HttpContext.Current.Cache[c_key] == null)
        {
            ProjectMgt.Entity.ProjectTasks d = new ProjectMgt.Entity.ProjectTasks();
            projectTaskDataContext _db = new projectTaskDataContext();
            //System.Data.Linq.ISingleResult<ProjectMgt.Entity.ProjectTasks> s= _db.ProjectTask_Details(int.Parse(Projectref));
            List<ProjectMgt.Entity.ProjectTasks> li = new List<ProjectMgt.Entity.ProjectTasks>();
            li = _db.ProjectTask_Details(int.Parse(Projectref)).ToList();
            BaseCache.Cache_Insert(c_key, li);

        }
        //var manyTasks1 = _db.ProjectTask_Details(int.Parse(Projectref)).ToList();
        return BaseCache.Cache_Select(c_key) as List<ProjectMgt.Entity.ProjectTasks>;
        //}

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void AssignedTeam(int teamId, int? taskId, string projectReference)
    {
        try
        {
            projectTaskDataContext _db1 = new projectTaskDataContext();
            //string ProjectReference = HttpContext.Current.Request.QueryString["project"].ToString();
            //_db1.Assignments.InsertAllOnSubmit(jsonData);
            //_db1.SubmitChanges(ConflictMode.ContinueOnConflict);

            var teamList = _db1.TeamMembers.Where(t => t.TeamID == teamId).ToList();
            int i = 0;
            int ac2pID = 0;
            foreach (var vals in teamList)
            {
                var ac2p = (from r in _db.AssignedContractorsToProjects
                            where r.ContractorID == vals.Name && r.ProjectReference == int.Parse(projectReference)
                            select r).ToList();
                if (ac2p != null)
                {
                    if (ac2p.Count == 0)
                    {
                        AssignedContractorsToProject ac2pInsert = new AssignedContractorsToProject();
                        ac2pInsert.ProjectReference = int.Parse(projectReference);
                        ac2pInsert.ContractorID = vals.Name;
                        _db.AssignedContractorsToProjects.InsertOnSubmit(ac2pInsert);
                        _db.SubmitChanges();
                        ac2pID = ac2pInsert.ID;
                    }
                    else
                    {
                        var ac2pIDs = (from r in _db.AssignedContractorsToProjects
                                       where r.ContractorID == vals.Name && r.ProjectReference == int.Parse(projectReference)
                                       select r).ToList().FirstOrDefault();
                        ac2pID = ac2pIDs.ID;
                    }
                    if (ac2pID != 0)
                    {
                        if (_db.ProjectItems.Where(o => o.ItemReference == taskId && o.ContractorID == vals.Name).Count() == 0)
                        {
                            ProjectItem insert = new ProjectItem();
                            insert.ContractorID = vals.Name;
                            insert.ProjectReference = int.Parse(projectReference);
                            insert.ItemReference = taskId;
                            insert.AC2PID = ac2pID;
                            _db.ProjectItems.InsertOnSubmit(insert);
                            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
                        }
                        ////vals.Id = insert.ID;
                        //jsonData[i].Id = insert.ID;
                        //jsonData[i].TaskId = insert.ItemReference.Value;
                        //jsonData[i].ResourceId = insert.ContractorID.Value;
                        //jsonData[i].ProjectReference = insert.ProjectReference.Value;
                    }
                }




                i++;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    public Object DeleteTeam(int? taskId, int teamid)
    {
        try
        {
            var resourcelist = _db.TeamMembers.Where(o => o.TeamID == teamid).ToList();
            var projectItemsList = _db.ProjectItems.Where(b => b.ItemReference ==taskId).ToList();
            //DataClasses1DataContext _db = new DataClasses1DataContext();
            foreach (var t in projectItemsList)
            {
                ProjectItem assignment = _db.ProjectItems.SingleOrDefault(b => b.ID == t.ID);

                if (assignment != null)
                {
                    var deps = _db.ProjectItems.Where(b => (b.ID == t.ID));
                    _db.ProjectItems.DeleteAllOnSubmit(deps);

                    var projectItems = (from r in _db.ProjectItems
                                        where r.ContractorID == t.ContractorID && r.ProjectReference == t.ProjectReference
                                        select r).ToList();
                    if (projectItems != null)
                    {
                        if (projectItems.Count == 0)
                        {
                            var ac2ps = _db.AssignedContractorsToProjects.Where(b => (b.ContractorID == t.ContractorID && b.ProjectReference == t.ProjectReference));
                            _db.AssignedContractorsToProjects.DeleteAllOnSubmit(ac2ps);
                        }
                    }
                }
            }
            _db.SubmitChanges();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return new { success = true };
    }


    public static int? GetMaxListPosition(int contractorID)
    {
        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            var maxPostion = db.ProjectItems.Where(c => c.ContractorID == contractorID).Select(c => c.ListPosition).Max();
            return maxPostion.HasValue ? maxPostion : 0;
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetTaskCategoryList()
    {
       

        var categoryList = ProjectTaskCategoryBAL.GetCategoryList().ToList();
        categoryList.Add(new ProjectTaskCategory { ID = 0, Name = "" });
        var categories = (from c in categoryList
                          orderby c.Name
                          select new { id = c.ID, name = c.Name }).ToArray();
        return categories;
    }


}
