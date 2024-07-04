using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class controls_DashboardTabs : System.Web.UI.UserControl
{

    private string[] Purl = { "Dashboardmain.aspx", "Tasks.aspx", "DashboardIssues.aspx", "Risks.aspx", "DashboardCaseMgmt.aspx", "DashboardHealthCheck.aspx", "ResourceDashboard.aspx", "PortfolioMain.aspx", "Programme.aspx", "CSI.aspx","FLSDashboard.aspx" };

//16 Dashboard Project  
//17 Dashboard Tasks  
//18 Dashboard Issues  
//19 Dashboard Resources  
//20 Dashboard Risks  
//21 Dashboard CSI  
//22 Dashboard Portfolio  
//23 Dashboard Programme  
//24 Dashboard Case  
//25 Dashboard Search  
//26 Dashboard Reports  
//27 Dashboard HealthChecks  
//28 Dashboard Service Requests  



    protected void Page_Load(object sender, EventArgs e)
    {
        string[] str = PermissionManager.GetFeatures();
        if (!Page.IsPostBack)
        {

            div_projects.Visible = Convert.ToBoolean(str[16]);
            div_tasks.Visible = Convert.ToBoolean(str[17]);
            div_issues.Visible = Convert.ToBoolean(str[18]);
            div_resource.Visible = Convert.ToBoolean(str[19]);
            div_risks.Visible = Convert.ToBoolean(str[20]);
            div_csi.Visible = Convert.ToBoolean(str[21]);
            div_portfolio.Visible = Convert.ToBoolean(str[22]);
            div_programe.Visible = Convert.ToBoolean(str[23]);
            //div_sr.Visible = Convert.ToBoolean(str[28]);
            div_helath.Visible = Convert.ToBoolean(str[27]);
            
        }
    }
    
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                
                //service request
                if (i == 4)
                {
                    rtValue = "current2";
                }
                //health check
                else if (i == 5)
                {
                    rtValue = "current4";
                }
                else if (i == 7)
                {
                    rtValue = "current5";
                }
                else
                {
                    rtValue = "current1";                    
                }
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;
    }
    
    protected string GetTabLayOut()
    {
       string _GetTabLayOut = string.Empty; 
        for(int i=0; i < Purl.Length; i++)
        {
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                //service request
                if (i == 4)
                {
                    _GetTabLayOut = "tabs_list2";
                }
                //health check
                else if (i == 5)
                {
                    _GetTabLayOut = "tabs_list4";
                }
               //portfolio
                else if (i == 7)
                {
                    _GetTabLayOut = "tabs_list5";
                }
                else
                {
                    _GetTabLayOut = "tabs_list1";
                }
            }
             
        }
        return _GetTabLayOut;
    }
   
}
