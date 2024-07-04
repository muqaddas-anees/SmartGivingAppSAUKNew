using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ApproversReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TimesheetSummary.Attributes.Add("src", string.Format("ApproversReportfrm.aspx?type=pdf&Ord={0}&Status={1}", ddlSortOption.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            TimesheetSummary.Attributes.Add("src", string.Format("ApproversReportfrm.aspx?type=xsl1&Ord={0}&Status={1}", ddlSortOption.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgviewReport_Click(object sender, EventArgs e)
    {
        try
        {
          
                TimesheetSummary.Attributes.Add("src", string.Format("ApproversReportfrm.aspx?type=pdf&Ord={0}&Status={1}", ddlSortOption.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString()));
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            TimesheetSummary.Attributes.Add("src", string.Format("ApproversReportfrm.aspx?type=xsl&Ord={0}&Status={1}", ddlSortOption.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString()));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
