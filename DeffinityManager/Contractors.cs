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
using UserMgt.DAL;
using UserMgt.Entity;
/// <summary>
/// Summary description for Contractors
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class Contractors : System.Web.Services.WebService {

    public Contractors () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod(EnableSession=true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object Get()
    {
        string ProjectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
        int[] sid = { 7, 8, 10 };
        //UserDataContext _db = new UserDataContext();
        //var resources = (from r in _db.Contractors
        //                 where !sid.Contains(r.SID.Value)
        //                 orderby r.ContractorName
        //                 select new { Name = r.ContractorName, Id = r.ID }).ToList();

        projectTaskDataContext _dbP = new projectTaskDataContext();
        var resources1 = (from r in _dbP.Project_AssignedTo_ProjectPlan(int.Parse(ProjectReference), sessionKeys.UID)
                          //where !sid.Contains(r.SID.Value)
                          orderby r.ContractorName
                          select new { Name = r.ContractorName, Id = r.ID }).ToList();

        //return _db.Contractors.Where(c=>sid.Contains(c.SID)).ToList();

        return resources1;
      
    }
    public class DipalyItemClass
    {
        public int id { set; get; }
        public string name { set; get; }
        public string type { set; get; }
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetTeamList()
    {
       // string projectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();
       

        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            List<DipalyItemClass> teamList = new List<DipalyItemClass>();
            
           // int? customerId = db.Projects.Where(p => p.ProjectReference == Convert.ToInt32(projectReference)).Select(p => p.Portfolio.HasValue ? p.Portfolio : 0).FirstOrDefault();
            teamList = (from t in db.Teams
                           // where t.PortfolioID == customerId
                            orderby t.TeamName
                            select new DipalyItemClass { id = t.ID, name = t.TeamName, type = t.TeamName }).ToList();
            teamList.Add(new DipalyItemClass { id = 0, name = " Select...", type = " Select..." });
            
            return teamList.OrderBy(p=>p.name).ToArray();
        }
      
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetTeamList1()
    {
        // string projectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();


        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            List<DipalyItemClass> teamList = new List<DipalyItemClass>();

            // int? customerId = db.Projects.Where(p => p.ProjectReference == Convert.ToInt32(projectReference)).Select(p => p.Portfolio.HasValue ? p.Portfolio : 0).FirstOrDefault();
            teamList = (from t in db.Teams
                        // where t.PortfolioID == customerId
                        orderby t.TeamName
                        select new DipalyItemClass { id = t.ID, name = t.TeamName, type = t.TeamName }).ToList();
            //teamList.Add(new DipalyItemClass { id = 0, name = " Select...", type = " Select..." });

            return teamList.OrderBy(p => p.name);
        }

    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public object GetTeamListNew()
    {
        // string projectReference = HttpContext.Current.Request.QueryString["ProjectReference"].ToString();


        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            // int? customerId = db.Projects.Where(p => p.ProjectReference == Convert.ToInt32(projectReference)).Select(p => p.Portfolio.HasValue ? p.Portfolio : 0).FirstOrDefault();
            var teamList = (from t in db.Teams
                            // where t.PortfolioID == customerId
                            orderby t.TeamName
                            select new {  Name = t.TeamName,Id = t.ID }).ToList();
            return teamList;
        }

    }

}
