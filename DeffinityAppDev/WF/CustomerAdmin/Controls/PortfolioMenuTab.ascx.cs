using System;

public partial class controls_PortfolioMenuTab1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbtn_Navigate.HRef = "~/WF/CustomerAdmin/PortfolioAdmin.aspx"; lbtn_NavigateText.InnerText = "Back to Customer Admin";
        string Nav_type = QueryStringValues.Type.ToLower();

        switch (Nav_type)
        {
            case "prj": if (Request.QueryString["project"] != null) { lbtn_Navigate.HRef = string.Format("~/WF/Projects/ProjectOverviewV4.aspx?project={0}", QueryStringValues.Project); } else { lbtn_Navigate.HRef = "~/WF/Projects/ProjectPipeline.aspx?status=2"; } lbtn_NavigateText.InnerText = "Back to Projects"; break;
            case "sr": lbtn_Navigate.HRef = "~/WF/DC/SDsummary.aspx"; lbtn_NavigateText.InnerText = "Back to Service desk"; break;
            case "shift": lbtn_Navigate.HRef = "~/WF/Admin/ResourcePlanner.aspx"; lbtn_NavigateText.InnerText = "Back to Resource planner"; break;
            case "portfolio": lbtn_Navigate.HRef = "~/WF/CustomerAdmin/PortfolioAdmin.aspx"; lbtn_NavigateText.InnerText = "Back to Customer Admin"; break;
            case "health": lbtn_Navigate.HRef = "~/WF/Health/HealthCheckSchedule.aspx"; lbtn_NavigateText.InnerText = "Back to Health Check Schedule"; break;
            default: lbtn_Navigate.HRef = "~/WF/CustomerAdmin/PortfolioAdmin.aspx"; lbtn_NavigateText.InnerText = "Back to Customer Admin"; break;

        }
       
    }
}
