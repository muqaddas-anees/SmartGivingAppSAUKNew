using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_MyTasksTimeSheetReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "Timesheet Summary Report-" + User.Identity.Name;
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

            TimesheetSummary.Attributes.Add("src", string.Format("MyTasksTimeSheetReportfrm.aspx?sdate={0}&edate={1}&type=xsl1",
              Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
              , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        try
        {

            TimesheetSummary.Attributes.Add("src", string.Format("MyTasksTimeSheetReportfrm.aspx?sdate={0}&edate={1}&type=pdf",
              Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
              , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
