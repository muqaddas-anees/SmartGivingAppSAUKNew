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
using UserMgt.DAL;
using UserMgt.Entity;

/// <summary>
/// Summary description for Assignement
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Assignement : System.Web.Services.WebService {

    public Assignement () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly projectTaskDataContext _db = new projectTaskDataContext();
    readonly UserDataContext _dbu = new UserDataContext();

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public Object Get()
    {
      
        //try
        //{
        int[] sid={7,8,10};
        var resources = (from r in _dbu.Contractors
                         where !sid.Contains(r.SID.Value) && r.Status != "InActive"
                         orderby r.ContractorName
                         select new { ID = r.ID, Name = r.ContractorName }).ToList();

            string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
            // return _db.ProjectTaskAssignments.Where(b=>b.ProjectReference==int.Parse(ProjectReference)).ToList();
           var list1 = (from r in _db.ProjectItems
                       //join c in resources on r.ContractorID equals c.ID
                        where r.ProjectReference == int.Parse(ProjectReference) 
                        select new { Id = r.ID, TaskId = r.ItemReference, ResourceId = r.ContractorID, ProjectReference = r.ProjectReference }).ToList();


           var list = (from r in list1
                       join c in resources on r.ResourceId equals c.ID
                       select r);
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
        return list.ToList();
    }



   


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Create(ProjectMgt.Entity.Assignment[] jsonData)
    {
        try
        {
            projectTaskDataContext _db1 = new projectTaskDataContext();
            string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
            //_db1.Assignments.InsertAllOnSubmit(jsonData);
            //_db1.SubmitChanges(ConflictMode.ContinueOnConflict);
            int i = 0;
            int ac2pID = 0;
            foreach (ProjectMgt.Entity.Assignment vals in jsonData)
            {
                var ac2p = (from r in _db.AssignedContractorsToProjects
                            where r.ContractorID == vals.ResourceId && r.ProjectReference == int.Parse(ProjectReference)
                            select r).ToList();
                if (ac2p != null)
                {
                    if (ac2p.Count == 0)
                    {
                        AssignedContractorsToProject ac2pInsert = new AssignedContractorsToProject();
                        ac2pInsert.ProjectReference = int.Parse(ProjectReference);
                        ac2pInsert.ContractorID = vals.ResourceId;
                        _db.AssignedContractorsToProjects.InsertOnSubmit(ac2pInsert);
                        _db.SubmitChanges();
                        ac2pID = ac2pInsert.ID;
                    }
                    else
                    {
                        var ac2pIDs = (from r in _db.AssignedContractorsToProjects
                                       where r.ContractorID == vals.ResourceId && r.ProjectReference == int.Parse(ProjectReference)
                                       select r).ToList().FirstOrDefault();
                        ac2pID = ac2pIDs.ID;
                    }
                    if (ac2pID != 0)
                    {
                        int? maxPostion= TasksNewVersion.GetMaxListPosition(vals.ResourceId);

                        ProjectItem insert = new ProjectItem();
                        insert.ContractorID = vals.ResourceId;
                        insert.ProjectReference = int.Parse(ProjectReference);
                        insert.ItemReference = vals.TaskId;
                        insert.AC2PID = ac2pID;
                        insert.ListPosition = maxPostion + 1;
                        insert.Utilisation = 100;
                        _db.ProjectItems.InsertOnSubmit(insert);
                        _db.SubmitChanges(ConflictMode.ContinueOnConflict);
                        //vals.Id = insert.ID;
                        jsonData[i].Id = insert.ID;
                        jsonData[i].TaskId = insert.ItemReference.Value;
                        jsonData[i].ResourceId = insert.ContractorID.Value;
                        jsonData[i].ProjectReference = insert.ProjectReference.Value;
                    }
                }




                i++;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return jsonData;
    }

   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Delete(ProjectMgt.Entity.Assignment[] jsonData)
    {
        try
        {
            //DataClasses1DataContext _db = new DataClasses1DataContext();
            foreach (ProjectMgt.Entity.Assignment t in jsonData)
            {
                ProjectItem assignment = _db.ProjectItems.SingleOrDefault(b => b.ID == t.Id);

                if (assignment != null)
                {
                    var deps = _db.ProjectItems.Where(b => (b.ID == t.Id));
                    _db.ProjectItems.DeleteAllOnSubmit(deps);

                    var projectItems = (from r in _db.ProjectItems
                                        where r.ContractorID == t.ResourceId && r.ProjectReference == t.ProjectReference
                                        select r).ToList();
                    if (projectItems != null)
                    {
                        if (projectItems.Count == 0)
                        {
                            var ac2ps = _db.AssignedContractorsToProjects.Where(b => (b.ContractorID == t.ResourceId && b.ProjectReference == t.ProjectReference));
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
    
}
