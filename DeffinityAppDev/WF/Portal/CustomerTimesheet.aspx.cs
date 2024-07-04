using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;



public partial class CustomerPortalTimesheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Timesheet";
        if (!IsPostBack)
        {
            //BindCustomerUserProjects();
            BindCustomerWcDate();
        }
    }

    //#region Customer Projects
    //private void BindCustomerUserProjects()
    //{ 
    //    projectTaskDataContext pdt= new projectTaskDataContext();
    //    var Cprojects = from p in pdt.ProjectDetails
    //                    where p.CustomerUserID == sessionKeys.UID
    //                    select new { ProjectReference = p.ProjectReference, projecttitle = p.ProjectReferenceWithPrefix + "-" + p.ProjectTitle };
    //    ddlProjects.DataSource = Cprojects;
    //    ddlProjects.DataTextField = "projecttitle";
    //    ddlProjects.DataValueField = "ProjectReference";
    //    ddlProjects.DataBind();
    //    ddlProjects.Items.Insert(0, new ListItem("All", "0"));

    //}

    //#endregion

    #region Grid funtions
    public string ChangeHours(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }
    public string ApproverStatus(string primeapprover, string secondapprover,string customerapprover)
    {
        string img = string.Empty;

        if ((primeapprover == "0" && secondapprover == "0") || ( string.IsNullOrEmpty(primeapprover) && string.IsNullOrEmpty(secondapprover) && string.IsNullOrEmpty(customerapprover) ))
            img = "<img src='media/ico_no_app.png' alt='Not Approved' title='Not Approved'/>";
        else if (primeapprover == "1" && secondapprover == "0")
            img = "<img src='media/ico_1_app.png' alt='Approved by primary approver' title='Approved by primary approver'/>";
        else if (primeapprover == "0" && secondapprover == "1")
            img = "<img src='media/ico_2_app.png' alt='Approved by secondary approver' title='Approved by secondary approver'/>";
        else
            img = "";

        return img;
    }
    #endregion
    protected void btn_ApprovalAll_Click(object sender, EventArgs e)
    {
        try
        {
            string s = string.Empty;
            foreach (GridViewRow row in GridCustomerWcdate.Rows)
            {

                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                if (chkNew.Checked)
                {
                    s = s + Convert.ToInt32(((Label)row.FindControl("lblWCDateID")).Text) + ",";
                }

                UpdateTimesheetStatus(string.Empty, s, sessionKeys.UID,string.Empty);
            }
            BindCustomerWcDate();
            //ModalControlExtender2.Hide();
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Timesheets approved successfully.";
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        try
        {
            string s = string.Empty;
            foreach (GridViewRow row in GridView4.Rows)
            {
                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                if (chkNew.Checked)
                {
                    s = s + Convert.ToInt32(((Label)row.FindControl("lblID")).Text) + ",";
                }

                UpdateTimesheetStatus(s, string.Empty, sessionKeys.UID, txtComments.Text.Trim());
                txtComments.Text = string.Empty;
            }
            BindCustomerWcDate();
            ModalControlExtender2.Hide();
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Timesheets approved successfully.";
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    

    #region Bind Grid
       private void BindCustomerWcDate()
       {
            GridCustomerWcdate.DataSource = BindCustomerByWCdate();
            GridCustomerWcdate.DataBind();
       }
    #endregion

    #region DAL

    private DataTable BindCustomerByWCdate()
    { 
        return SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,"TimesheetEntry_Customeruser_wcdate",new SqlParameter("@CustomerUserID",sessionKeys.UID)).Tables[0];
    }
     private DataTable BindCustomerByWCdate(int wcdateid,int Resourceid)
    { 
        return SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,"TimesheetEntry_Customeruser_list",new SqlParameter("@CustomerUserID",sessionKeys.UID),new SqlParameter("@wcdateid",wcdateid)).Tables[0];
    }
    private void UpdateTimesheetStatus(string TimesheetEntryID,string WCdateID,int CustomerUserID,string approverComents)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "TimesheetEntry_Customeruser_Approve", new SqlParameter("@TimesheetEntryID", TimesheetEntryID), new SqlParameter("@WCdateID", WCdateID), new SqlParameter("@CustomerUserID", CustomerUserID), new SqlParameter("@ApproverComents", approverComents));
    }
    private void UpdateTimesheetStatus_decline(string TimesheetEntryID, string WCdateID, int CustomerUserID,string approverComents)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "TimesheetEntry_Customeruser_Decline", new SqlParameter("@TimesheetEntryID", TimesheetEntryID), new SqlParameter("@WCdateID", WCdateID), new SqlParameter("@CustomerUserID", CustomerUserID), new SqlParameter("@ApproverComents", approverComents));
    }
    #endregion
    protected void GridCustomerWcdate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());

            string GetCID = string.Empty;


            //GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            //GetCID = ((Label)gvrow.FindControl("lblContractorID")).Text;

            GridView4.DataSource = BindCustomerByWCdate(ID, 0);
            GridView4.DataBind();
            ModalControlExtender2.Show();
        }
        

    }
    
    protected void GridCustomerWcdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chkAll1")).Attributes.Add("onclick", "javascript:SelectAll_sub('" +
                    ((CheckBox)e.Row.FindControl("chkAll1")).ClientID + "')");
        }
    }
    //btn_declined_Click
    protected void btn_declined_Click(object sender, EventArgs e)
    {
        try
        {
            string s = string.Empty;
            foreach (GridViewRow row in GridView4.Rows)
            {
                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                if (chkNew.Checked)
                {
                    s = s + Convert.ToInt32(((Label)row.FindControl("lblID")).Text) + ",";
                }

                UpdateTimesheetStatus_decline(s, string.Empty, sessionKeys.UID, txtComments.Text.Trim());
                txtComments.Text = string.Empty;

            }
            BindCustomerWcDate();
            ModalControlExtender2.Hide();
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Timesheets declined successfully.";
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
}

