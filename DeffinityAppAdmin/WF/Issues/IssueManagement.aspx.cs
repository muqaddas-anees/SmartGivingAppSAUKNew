using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

public partial class IssueManagement : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Issue Management";
        //Master.PageHead = Resources.DeffinityRes.IssueManage;
        
        //ProjectPmIssues1.IssueSection = Deffinity.GlobalIssues.IssueSection.Global.ToString();
    }
    [System.Web.Services.WebMethod]
    public static object GetIssuesDataWithUser()
    {
        List<DxchartF> DxchartFList = new List<DxchartF>();
        DxchartF Dlist = null;
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    var listOfUsers = Udc.Contractors.ToList();
                    var ProjectIssuesList = Pdc.ProjectIssues.ToList();

                    //if (sessionKeys.PortfolioID > 0)
                    //{
                    //    var ProjectIds = Pdc.Projects.Where(a => a.Portfolio == sessionKeys.PortfolioID).Select(a => a.ProjectReference).ToList();
                    //    ProjectIssuesList = ProjectIssuesList.Where(a => ProjectIds.Contains(a.Projectreference.HasValue ? a.Projectreference.Value : 0)).ToList();
                    //}

                    var newList = new object();

                    foreach (var L in listOfUsers)
                    {
                        Dlist = new DxchartF();
                        Dlist.Value = ProjectIssuesList.Where(x => x.AssignTo == L.ID).Count();
                        Dlist.Name = L.ContractorName;
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
    [System.Web.Services.WebMethod]
    public static object GetIssuesDataWithCategory()
    {
        List<DxchartF> DxchartFList = new List<DxchartF>();
        DxchartF Dlist = null;
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                var listOfCategoryTypes = Pdc.IssueTypes.ToList();
                var ProjectIssuesList = Pdc.ProjectIssues.ToList();

                //if (sessionKeys.PortfolioID > 0)
                //{
                //    var ProjectIds = Pdc.Projects.Where(a => a.Portfolio == sessionKeys.PortfolioID).Select(a => a.ProjectReference).ToList();
                //    ProjectIssuesList = ProjectIssuesList.Where(a => ProjectIds.Contains(a.Projectreference.HasValue ? a.Projectreference.Value : 0)).ToList();
                //}

                var newList = new object();

                foreach (var L in listOfCategoryTypes)
                {
                    Dlist = new DxchartF();
                    Dlist.Value = ProjectIssuesList.Where(x => x.IssuseType == L.ID).Count();
                    Dlist.Name = L.IssueTypeName;
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
}
public class DxchartF
{
    public int Value { get; set; }
    public string Name { get; set; }
}
