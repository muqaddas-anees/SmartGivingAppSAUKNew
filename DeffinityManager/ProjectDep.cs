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

/// <summary>
/// Summary description for ProjectDep
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ProjectDep : System.Web.Services.WebService {

    public ProjectDep () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    readonly projectTaskDataContext _db = new projectTaskDataContext(); //_db = new DataClasses1DataContext();

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public Object Get()
    {
        string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        return _db.ProjectTaskDeps.Where(b=>b.ProjectReference==int.Parse(ProjectReference));
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Create(ProjectTaskDep[] jsonData)
    {
        try
        {
            //_db.ProjectTaskDeps.InsertAllOnSubmit(jsonData);
            //_db.SubmitChanges(ConflictMode.ContinueOnConflict);
            ProjectTaskDependency t = new ProjectTaskDependency();
            t.TaskDependent = jsonData[0].From;
            t.TaskDependingOn = jsonData[0].To;
            t.Type = jsonData[0].Type;
            //_db.ProjectTaskDependencies.InsertOnSubmit(jsonData);
            _db.ProjectTaskDependencies.InsertOnSubmit(t);
            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonData;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Delete(ProjectTaskDep[] jsonData)
    {
        try
        {
            if (jsonData != null)
            {
                foreach (ProjectTaskDep d in jsonData)
                {
                    ProjectTaskDependency dep = _db.ProjectTaskDependencies.SingleOrDefault(b => b.ID == d.Id);
                    _db.ProjectTaskDependencies.DeleteOnSubmit(dep);
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Update(ProjectTaskDep[] jsonData)
    {
        try
        {
            if (jsonData != null)
            {
                foreach (ProjectTaskDep vals in jsonData)
                {
                    ProjectTaskDependency dep = _db.ProjectTaskDependencies.SingleOrDefault(b => b.ID == vals.Id);
                    if (dep != null)
                    {
                        dep.Type = vals.Type;
                        dep.TaskDependent = vals.From;
                        dep.TaskDependingOn = vals.To;

                    }
                }
                _db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return new { success = true };
    }
    
}
