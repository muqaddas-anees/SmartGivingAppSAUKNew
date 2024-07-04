using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


public partial class Reports_BenefitTrackingSummaryReport : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int portfolioID = Convert.ToInt32(Request.QueryString["PortfolioID"].ToString());
            int progID = Convert.ToInt32(Request.QueryString["ProgID"].ToString());
            int subprogID = Convert.ToInt32(Request.QueryString["SubprogID"].ToString());
            int benefitID = Convert.ToInt32(Request.QueryString["BenefitID"].ToString());
            TimesheetSummary.Attributes.Add("src", string.Format("BenefitTrackingSummaryReportfrm.aspx?type={0}&PortfolioID={1}&ProgID={2}&SubprogID={3}&BenefitID={4}", "pdf", portfolioID, progID, subprogID, benefitID));
        }
    }
   
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        int portfolioID = Convert.ToInt32(Request.QueryString["PortfolioID"].ToString());
        int progID = Convert.ToInt32(Request.QueryString["ProgID"].ToString());
        int subprogID = Convert.ToInt32(Request.QueryString["SubprogID"].ToString());
        int benefitID = Convert.ToInt32(Request.QueryString["BenefitID"].ToString());
        TimesheetSummary.Attributes.Add("src", string.Format("BenefitTrackingSummaryReportfrm.aspx?type={0}&PortfolioID={1}&ProgID={2}&SubprogID={3}&BenefitID={4}", "xls", portfolioID, progID, subprogID, benefitID));
    }
    
 
}
