using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class controls_TimesheetSubTabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Url.ToString().ToLower()).Contains("timesheet.aspx") == true)
        {
            lbtnTimesheet.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("timesheetnotsubmit.aspx") == true)
        {
            lbtnTimeSheetNotSubmit.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("timesheetdeclined.aspx") == true)
        {
            lbtnTimesheetDeclined.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("timesheetjournal.aspx") == true)
        {
            lbtnTimeSheetJournal.BackColor = System.Drawing.Color.White;
        }
        //else if ((Request.Url.ToString().ToLower()).Contains("timesheetapprovermailalert.aspx") == true)
        //{
        //    lbtnTimeSheetalerts.BackColor = System.Drawing.Color.White;
        //}
    }

  
}
