using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

public partial class TimesheetApproverMailAlert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Timesheet";
        if (!IsPostBack)
        {
            setDisiableVal();  
        }
    }
    protected void chkEnable_CheckedChanged(object sender, EventArgs e)
    {
        try {
            //insert udpate value
            InsertUpdateVal(sessionKeys.UID);

            setDisiableVal();
            
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    #region Get status of user
    private void setDisiableVal()
    {
        chkEnable.Checked = RetApprover(sessionKeys.UID);
    }
    private bool RetApprover(int approverid)
    {
        bool retval = true;

        TimeSheetDataContext td = new TimeSheetDataContext();

        var rval = (from t in td.TimesheetApproverEmails
                   where t.ApproverID== approverid 
                    select t).FirstOrDefault();
        if (rval !=null)
        {
            retval = rval.Enable.Value;            
        }
        
        return retval;
    }
    private void InsertUpdateVal(int approverid)
    {
        try
        {
            TimeSheetDataContext td = new TimeSheetDataContext();

            var rval = (from t in td.TimesheetApproverEmails
                        where t.ApproverID == approverid
                        select t).FirstOrDefault();
            TimesheetApproverEmail tm = new TimesheetApproverEmail();
            if (rval == null)
            {
                tm.ApproverID = approverid;
                tm.Enable = chkEnable.Checked;
                td.TimesheetApproverEmails.InsertOnSubmit(tm);
                td.SubmitChanges();
                lblmsg.Text = "Updated successfully.";
            }
            else
            {
                ((TimesheetApproverEmail)rval).Enable = chkEnable.Checked;
                td.SubmitChanges();
                lblmsg.Text = "Updated successfully.";
            }

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }


    }
    #endregion
}
