using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
//using Ext_Gantt_CRUD_Demo.models;
using System.Web.Script.Serialization;
using System.Data.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

/// <summary>
/// Summary description for Dependencies1
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
    [ScriptService]
public class Dependencies1 : System.Web.Services.WebService {

    public Dependencies1 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    readonly projectTaskDataContext _db = new projectTaskDataContext();


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Create(Dependency jsonData)
    {
        
         int ID=0;
        try
        {
            ProjectTaskDependency t = new ProjectTaskDependency();
            t.TaskDependent = jsonData.From;
            t.TaskDependingOn = jsonData.To;
            t.Type = jsonData.Type;
            //_db.ProjectTaskDependencies.InsertOnSubmit(jsonData);
            _db.ProjectTaskDependencies.InsertOnSubmit(t);
            _db.SubmitChanges(ConflictMode.ContinueOnConflict);
           ID = t.ID;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        if (ID != 0)
        {
            var retDate = (from r in _db.ProjectTaskDependencies
                           where r.ID == ID
                           select r).FirstOrDefault();
            if (retDate != null)
            {
                jsonData.Id = retDate.ID;
                jsonData.Type = retDate.Type.Value;
                jsonData.To = retDate.TaskDependingOn;
                jsonData.From = retDate.TaskDependent;
            }
        }

        return new { success = true, dependencydata = jsonData };


    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Delete(int jsonData)
    {
        ProjectTaskDependency dep = _db.ProjectTaskDependencies.SingleOrDefault(b => b.ID == jsonData);

        if (dep != null)
        {
            _db.ProjectTaskDependencies.DeleteOnSubmit(dep);
            _db.SubmitChanges();
        }
        return new { success = true };
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Object Update(Dependency jsonData)
    {

        if (jsonData != null)
        {
            ProjectTaskDependency dep = _db.ProjectTaskDependencies.SingleOrDefault(b => b.ID == jsonData.Id);
            if (dep != null)
            {
                dep.Type = jsonData.Type;
                dep.TaskDependent = jsonData.From;
                dep.TaskDependingOn = jsonData.To;
                _db.SubmitChanges();
            }

        }
        return new { success = true };
    }
}
