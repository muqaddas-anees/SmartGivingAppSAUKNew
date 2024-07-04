using DC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using UserMgt.DAL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

namespace DeffinityAppDev.WF.Projects.webservices
{
    /// <summary>
    /// Summary description for ProjectDashboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProjectDashboardService : System.Web.Services.WebService
    {


        [WebMethod]
        public object ProjectsData()
        {
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ProjectMgt.BAL.ProjectBAL projectsdata = new ProjectMgt.BAL.ProjectBAL();
            var pd = projectsdata.Project_SelectAll().Take(10).ToList();

            var retCls = (from p in pd
                          select new
                          {
                              p.ProjectReferenceWithPrefix,
                              p.ProjectTitle
                          }).ToList();
         return jsonSerializer.Serialize(retCls).ToString();
        }
        [WebMethod]
        public object ProjectTeamData()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ProjectMgt.BAL.ProjectBAL projectsdata = new ProjectMgt.BAL.ProjectBAL();
            var pd = projectsdata.Project_SelectAll().Take(10).ToList();

            var retCls = (from p in pd
                          select new
                          {
                              p.ProjectReferenceWithPrefix,
                              p.ProjectTitle
                          }).ToList();
            return jsonSerializer.Serialize(retCls).ToString();
        }

        [WebMethod]
        public object TeamNumbersChartData()
        {
            List<DxchartF> DxchartFList = new List<DxchartF>();
            DxchartF Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                Dlist = new DxchartF();
                Dlist.Name = "Kishore";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Ramesh";
                Dlist.Value = 40;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Siva";
                Dlist.Value = 60;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Chitti";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Naresh";
                Dlist.Value = 40;
                DxchartFList.Add(Dlist);

                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod]
        public object RiskChartData()
        {
            List<DxchartF> DxchartFList = new List<DxchartF>();
            DxchartF Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                Dlist = new DxchartF();
                Dlist.Name = "Kishore";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Ramesh";
                Dlist.Value = 40;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Siva";
                Dlist.Value = 60;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Chitti";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Naresh";
                Dlist.Value = 40;
                DxchartFList.Add(Dlist);

                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod]
        public object FinancialhealthData()
        {
            List<DxchartF> DxchartFList = new List<DxchartF>();
            DxchartF Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                Dlist = new DxchartF();
                Dlist.Name = "Budgeted Cost";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Actual Cost";
                Dlist.Value = 40;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Variations Approved";
                Dlist.Value = 60;
                DxchartFList.Add(Dlist);
                Dlist = new DxchartF();
                Dlist.Name = "Variations Pending Approval";
                Dlist.Value = 20;
                DxchartFList.Add(Dlist);

                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod]
        public object IssuesChartData()
        {
            int sts1 = 22;
            List<DxchartF> DxchartFList = new List<DxchartF>();
            DxchartF Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    using (DCDataContext dc = new DCDataContext())
                    {

                        var clist = Udc.Contractors.ToList();
                        var FlsList = dc.FLSDetails.ToList();
                        var cdlist = dc.CallDetails.ToList();
                        var rlist = (from a in FlsList
                                     join b in cdlist on a.CallID equals b.ID
                                     where b.StatusID == sts1//new status
                                     select new { a.UserID, b.StatusID }).ToList();
                        var listOfTechnicians = (from a in FlsList
                                                 join b in clist on a.UserID equals b.ID
                                                 select new
                                                 {
                                                     value = b.ID,
                                                     name = b.ContractorName
                                                 }).Distinct().ToList();
                        var newList = new object();
                        foreach (var L in listOfTechnicians)
                        {
                            Dlist = new DxchartF();
                            Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.UserID == L.value).Count();
                            Dlist.Name = L.name;
                            DxchartFList.Add(Dlist);
                        }
                        return jsonSerializer.Serialize(DxchartFList).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }





       
    
    
    #region "Custom Class"
    public class PortfolioContactDetails
    {
        public int ID { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
    }
    #endregion
    public class GvFields
    {
        public int callid { get; set; }
        public string Des { get; set; }
    }
    public class DxchartF
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
    public class DxchartFWith3Fields
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Name { get; set; }
    }
    public class StatusList
    {
        public int Pending { get; set; }
        public int InHand { get; set; }
        public int Completed { get; set; }
        public int Resolved { get; set; }
        public string Name { get; set; }
    }
    }
}
