using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using System.Web.Script.Serialization;


namespace DeffinityAppDev.api
{
    public class ProjectController : ApiController
    {
       
        // GET api/<controller>
        public IEnumerable<ProjectMgt.Entity.ProjectDetails> Get()
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            return pb.Project_SelectAll().Take(5).ToList();
        }
        // GET api/<controller>/5
        public ProjectMgt.Entity.ProjectDetails Get(int id)
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            return pb.Project_SelectAll().Where(o=>o.ProjectReference == id).FirstOrDefault();
        }
        [Route("api/project/ProjectsDashboardSelect")]
        [HttpGet]
        public IEnumerable<ProjectMgt.Entity.Projects_DashboardSummaryResult> ProjectsDashboardSelect()
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            return pb.Projects_DashboardSummary_Selectall().ToList();
           
        }
        [Route("api/Project/ProjectsBasicSummary")]
        [HttpGet]
        public object ProjectsBasicSummary()
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            return JsonConvert.SerializeObject(pb.Projects_DashboardBasicSummary_Selectall());
            
        }
        [Route("api/Project/ProjectsFinanceChart/{projectrefernce}")]
        [HttpGet]
        public object ProjectsFinanceChart(int projectreference)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            try
            {
                ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
                var resultlist = pb.Projects_DashboardFinance_Select(Convert.ToInt32(projectreference));
                List<dynamic> li_name = new List<dynamic>();
                dynamic d1 = new JObject();
                d1["val"] = resultlist.BudgetedCost;
                d1["name"] = "Budgeted Cost";
                li_name.Add(d1);
                dynamic d2 = new JObject();
                d2["val"] = resultlist.ActaulCost;
                d2["name"] = "Actual Cost";
                li_name.Add(d2);
                dynamic d3 = new JObject();
                d3["val"] = resultlist.VariationsApproved;
                d3["name"] = "Variations Approved";
                li_name.Add(d3);
                dynamic d4 = new JObject();
                d4["val"] = resultlist.VariationsPendingApproval;
                d4["name"] = "Variations Pending Approval";
                li_name.Add(d4);

                return JsonConvert.SerializeObject(li_name);
            }
            catch (Exception ex)
            {
                //return hc;
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }

        }
        [Route("api/Project/GetChart/{projectrefernce}")]
        [HttpGet]
        public object GetChart(int projectreference)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            var resultlist = pb.Projects_DashboardFinance_Select(Convert.ToInt32(projectreference));
            List<dynamic> li_name = new List<dynamic>();
            dynamic d1 = new JObject();
            d1["val"] = resultlist.BudgetedCost;
            d1["name"] = "Budgeted Cost";
            li_name.Add(d1);
            dynamic d2 = new JObject();
            d2["val"] = resultlist.ActaulCost;
            d2["name"] = "Actual Cost";
            li_name.Add(d2);
            dynamic d3 = new JObject();
            d3["val"] = resultlist.VariationsApproved;
            d3["name"] = "Variations Approved";
            li_name.Add(d3);
            dynamic d4 = new JObject();
            d4["val"] = resultlist.VariationsPendingApproval;
            d4["name"] = "Variations Pending Approval";
            li_name.Add(d4);

            return JsonConvert.SerializeObject(li_name);

        }
       

        [Route("api/Project/GetTasks/{projectrefernce}")]
        public object GetTasks(int projectrefernce)
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            var x = pb.ProjectTaskDetails_ByProjectRef(projectrefernce);
            return x;
        }
        [Route("api/Project/GetActiveTasks/{projectrefernce}")]
        public object GetActiveTasks(int projectrefernce)
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            var y = pb.ProjectActiveTaskDetails_ByProjectRef(projectrefernce);
            return y;
        }

        [Route("api/Project/GetIssues/{projectrefernce}")]
        public object GetIssues(int projectrefernce)
        {
            ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
            UserMgt.BAL.ContractorsBAL ub = new UserMgt.BAL.ContractorsBAL();
            var iList = pb.ProjectIssues_SelectByProjectReference(projectrefernce).ToList();
            var ublist = ub.Contractor_SelectAll().Where(p => iList.Select(o => o.AssignTo).ToArray().Contains(p.ID)).ToList();
            var result = (from i in iList
                          join u in ublist on i.AssignTo equals u.ID
                          select new
                          {
                              i.Issue,
                              u.ContractorName,
                              i.ScheduledDate
                          }).ToList();


            return result;
        }

        [Route("api/Project/GetTimesheets/{projectrefernce}")]
        public IEnumerable<TimesheetMgt.Entity.V_TimesheetDetail> GetTimesheets(int projectrefernce)
        {
            using (TimesheetMgt.DAL.TimeSheetDataContext pb = new TimesheetMgt.DAL.TimeSheetDataContext())
            {
                return pb.V_TimesheetDetails.Where(o => o.ProjectReference == projectrefernce).OrderByDescending(o=>o.DateEntered).ToList();
            }
        }
    }
}