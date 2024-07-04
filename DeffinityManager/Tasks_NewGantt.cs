using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
//using Ext_Gantt_CRUD_Demo.models;
using System.Web.Script.Serialization;
using System.Data.Linq;
using System.Configuration;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

/// <summary>
/// Summary description for Tasks1
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
    [ScriptService]
public class Tasks1 : System.Web.Services.WebService {

    public Tasks1 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public object Get()
    {
       string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        //string ProjectReference = "305";
        
        projectTaskDataContext _db = new projectTaskDataContext();
      

        var result = new
        {

            //taskdata = _db.ProjectTaskItems.Where(b => b.ProjectReference == int.Parse(ProjectReference)),
            //dependencydata = _db.ProjectTaskDependencies
            taskdata = _db.ProjectTask_Task(int.Parse(ProjectReference)),
            dependencydata = _db.ProjectTask_Dependencey(int.Parse(ProjectReference))

        };


        return result;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Create(ProjectMgt.Entity.Tasktb[] jsonData)
    {
        //insertUpdateProjectItems_NewGanttChart
        try
        {
           
            string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
          
            projectTaskDataContext _db = new projectTaskDataContext();
           


            int ParentID;
            if (jsonData[0].ParentId == null)
            {
                ParentID = 0;
            }
            else
            {
                ParentID = jsonData[0].ParentId.Value;
            }
            string AssignedResources = "";
            if (jsonData[0].AssignedResources == null)
            {
                AssignedResources = "";
            }
            else
            {
                AssignedResources = jsonData[0].AssignedResources;
            }
            int? out1 = 0;
            _db.insertUpdateProjectItems_NewGanttChart(jsonData[0].Name, jsonData[0].IsLeaf, ParentID,
jsonData[0].PercentDone.ToString(), jsonData[0].StartDate, jsonData[0].EndDate, jsonData[0].Priority, AssignedResources,

jsonData[0].Id, jsonData[0].type, jsonData[0].index, jsonData[0].currID, int.Parse(ProjectReference), 1, ref  out1,
jsonData[0].RAG, jsonData[0].QA, jsonData[0].RAGRequired, jsonData[0].ProjectStartDate, jsonData[0].ProjectEndDate);
            int ID = out1.Value;
            if (ID != 0)
            {
                var retDate = (from r in _db.ProjectTaskItems
                               where r.ID == ID
                               select r).FirstOrDefault();


                jsonData[0].Name = retDate.ItemDescription;
                jsonData[0].IsLeaf = retDate.isLeaf.Value;
                jsonData[0].ParentId = retDate.ParentID;
                jsonData[0].PercentDone = int.Parse(retDate.PercentComplete);
                jsonData[0].StartDate = retDate.StartDate.Value;
                jsonData[0].EndDate = retDate.CompletionDate.Value;
                jsonData[0].Priority = retDate.Priority;
                jsonData[0].AssignedResources = retDate.AssignedResources;
                jsonData[0].Id = retDate.ID;
                jsonData[0].RAG = retDate.RAGStatus;
                jsonData[0].QA = Convert.ToChar(retDate.QA);
                jsonData[0].RAGRequired=Convert.ToChar(retDate.RAGRequired);
                jsonData[0].ProjectStartDate = retDate.ProjectStartDate.Value;
                jsonData[0].ProjectEndDate = retDate.ProjectEndDate.Value;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return new { success = true, taskdata = jsonData };
    }

    [WebMethod (EnableSession=true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Update(ProjectMgt.Entity.Tasktb[] jsonData)
    {
        string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        projectTaskDataContext _db = new projectTaskDataContext();
        int projectstatus=0;
        try
        {
           

            var projectStatus = (from r in _db.Projects
                                 where r.ProjectReference == int.Parse(ProjectReference)
                                 select r).FirstOrDefault();
            if (projectStatus != null)
            {
                projectstatus = projectStatus.ProjectStatusID.Value;
            }

            foreach (ProjectMgt.Entity.Tasktb data in jsonData)
            {

                 var projectJournal=(from r in _db.ProjectTaskJournals
                                where r.TaskID==data.Id && ((r.TaskStatus==data.StatusID && r.ActualStartDate==data.StartDate
                                && r.ActualEndDate == data.EndDate && r.PercentComplete == data.PercentDone.ToString()
                                && r.EndDate == data.ProjectEndDate && r.StartDate == data.ProjectStartDate)
                               )
                                         select r).ToList();
                 if (projectJournal != null)
                 {
                     if (projectJournal.Count == 0)
                     {
                         try
                         {
                             ProjectTaskJournal insert = new ProjectTaskJournal();
                             insert.TaskStatus = data.StatusID;
                             insert.Projectreference = int.Parse(ProjectReference);
                             insert.StartDate = data.ProjectStartDate;
                             insert.EndDate = data.ProjectEndDate;
                             insert.ActualStartDate = data.StartDate;
                             insert.ActualEndDate = data.EndDate;
                             insert.PercentComplete = data.PercentDone.ToString();
                             insert.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                             insert.ChangedbyUserID = sessionKeys.UID;
                             insert.TaskID = data.Id;

                             _db.ProjectTaskJournals.InsertOnSubmit(insert);
                             _db.SubmitChanges();
                         }
                         catch (Exception ex)
                         {
                             LogExceptions.WriteExceptionLog(ex);
                         }

                     }

                 }

                ProjectTaskItem t = _db.ProjectTaskItems.SingleOrDefault(b => b.ID == data.Id);
                if (t != null)
                {
                    t.ItemDescription = data.Name;
                    t.isLeaf = data.IsLeaf;
                    t.ParentID = data.ParentId;
                    t.ItemStatus = data.StatusID;
                    if (data.StatusID == 3)
                    {
                        t.PercentComplete = "100";
                    }
                    else
                    {
                        t.PercentComplete = data.PercentDone.ToString();
                    }
                    t.StartDate = data.StartDate;
                    t.CompletionDate = data.EndDate;
                    t.Priority = data.Priority;
                    t.RAGStatus = data.RAG;
                    
                    t.QA = data.QA.ToString();
                    if (projectstatus != 2)
                    {
                        t.ProjectStartDate = data.ProjectStartDate;
                        t.ProjectEndDate = data.ProjectEndDate;
                    }
                    t.RAGRequired = data.RAGRequired.ToString();
                    if (data.AssignedResources == null)
                    {
                        t.AssignedResources = "";
                    }
                    else
                    {
                        t.AssignedResources = data.AssignedResources;
                    }
                    // t.AssignedResources = data.AssignedResources;
                }
            }
            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return new { success = true };
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Delete(int[] jsonData)
    {
        //  string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        projectTaskDataContext _db = new projectTaskDataContext();
        foreach (int id in jsonData)
        {
            ProjectTaskItem task = _db.ProjectTaskItems.SingleOrDefault(b => b.ID == id);

            if (task != null)
            {
                var deps = _db.ProjectTaskDependencies.Where(b => (b.TaskDependingOn == id || b.TaskDependent == id));
                _db.ProjectTaskDependencies.DeleteAllOnSubmit(deps);
                _db.ProjectTaskItems.DeleteOnSubmit(task);
            }
        }
        _db.SubmitChanges();
        return new { success = true };
    }

}
