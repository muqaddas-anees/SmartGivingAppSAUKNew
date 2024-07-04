using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet_Admin;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using TimesheetMgt.BAL;
public partial class TimesheetReminders : System.Web.UI.Page
{
    TimesheetRemindersBAL T_Bal = new TimesheetRemindersBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TimesheetRemindersTbl i = T_Bal.Select();
            if (i == null)
            {
                BtnUpdate.Visible = false;
                txttime.Text = string.Format("{0:HH:mm}", DateTime.Now);
            }
            else
            {
                checkboxalert.Checked = (bool)i.SwitchOnTimesheetAlert;
                txttime.Text = i.SendReminderAt.Value.ToString(@"hh\:mm");
                CheckboxAvoid.Checked = (bool)i.Avoidweekends;
                BtnSubmit.Visible = false;
            }
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if(txttime.Text.Trim()!=string.Empty)
        {
            TimesheetRemindersTbl t = new TimesheetRemindersTbl();
            t.SwitchOnTimesheetAlert = checkboxalert.Checked;
            t.SendReminderAt =TimeSpan.Parse(txttime.Text.Trim());
            t.Avoidweekends = CheckboxAvoid.Checked;
            T_Bal.Insert(t);
            Response.Redirect(Request.Url.AbsoluteUri);
            Lblmsg.Text = "New record inserted...";
        }
        else
        {
            Lblmsg.Text="Please enter time";
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (txttime.Text.Trim() != string.Empty)
        {
            TimesheetRemindersTbl t = new TimesheetRemindersTbl();
            t.SwitchOnTimesheetAlert = checkboxalert.Checked;
            t.SendReminderAt = TimeSpan.Parse(txttime.Text.Trim());
            t.Avoidweekends = CheckboxAvoid.Checked;
            T_Bal.Update(t);
            Response.Redirect(Request.Url.AbsoluteUri);
            Lblmsg.Text = "Updated Successfully...";
        }
        else
        {
            Lblmsg.Text = "Please enter time";
        }
    }
}