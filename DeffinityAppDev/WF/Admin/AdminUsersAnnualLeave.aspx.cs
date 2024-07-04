using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.BE;
using Deffinity.BLL;
using Certifications;
using VT.Entity;
using VT.DAL;
using System.Text;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
using System.Collections.Generic;

public partial class AdminUsersAnnualLeave : BasePage
{
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = Resources.DeffinityRes.Admin;//"Admin";
        //int uid = Convert.ToInt32(Request.QueryString["uid"]);
        
        getUserId.Value = Request.QueryString["uid"];
        try
        {
            if (!this.IsPostBack)
            {


                SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));

                BindDays();
                ddlyear.SelectedValue= DateTime.Today.Year.ToString();
                ddlLieuYear.SelectedValue = DateTime.Today.Year.ToString();
                

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Allowance allowance = new Allowance();
        allowance.UserID = Convert.ToInt32(getUserId.Value);
        allowance.LeaveAllowance = Convert.ToSingle(txtleaves.Text);
        allowance.CarriedOver = string.IsNullOrEmpty(txtCarriedOver.Text) ? 0 : Convert.ToSingle(txtCarriedOver.Text);
        allowance.Year = Convert.ToInt32(ddlyear.SelectedValue);
        AllowanceHelper helper = new AllowanceHelper();
        int i = helper.Insert(allowance);
        //Association();
        if (i >= 1)
        {
            Label1.ForeColor = System.Drawing.Color.Green;
            Label1.Text =Resources.DeffinityRes.Allowanceaddedtouser;//"Allowance added to the user";
        }
        else
            if (i < 1)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = Resources.DeffinityRes.Allowancenotadded;//"Allowance not added ";
            }
        GrdUserAllowance.DataBind();
        txtleaves.Text = string.Empty;
        txtCarriedOver.Text = string.Empty;
        ddlyear.ClearSelection();
    }
    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        Association();
        gridUserMappings.DataBind();
    }
    private void Association()
    {
        int ApproverID = int.Parse(ddlUsersapp.SelectedValue);
        LeaveApproverHelper helper = new LeaveApproverHelper();
        Object message = helper.Insert(Convert.ToInt32(getUserId.Value), ApproverID);
        if (Convert.ToInt16(message) > 0)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = Resources.DeffinityRes.onlyoneapproverforauser;//"Cannot add more than one approver for a user";
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = Resources.DeffinityRes.Approverassociatedtouser;//"Approver associated to the user";
        }
    }

    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    
                    lblusername.Text = dr["ContractorName"].ToString();
                    lblallowanceusername.Text = dr["ContractorName"].ToString();

                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }
    protected void imgSubmit_Click(object sender, EventArgs e)
    {
        AllowanceHelper ah = new AllowanceHelper();
        LeaveDaysEntity lde = new LeaveDaysEntity();



        double d = Math.Round(Convert.ToDouble(txtDays.Text.Trim()), 1);
        int Ttxvalue = Convert.ToInt32(d);

        // lde.Days = Convert.ToDouble(string.IsNullOrEmpty(txtDays.Text) ? "0" : txtDays.Text);
        lde.UserID = int.Parse(getUserId.Value);
        lde.UserName = lblusername.Text;
        lde.Comments = txtComments.Text;
        lde.Year = Convert.ToInt32(ddlLieuYear.SelectedValue);


        string[] A1 = d.ToString().Split('.');
        int[] IntValues = { 5 };
        if (A1.Length == 2)
        {
            if (A1[1].ToString() == "5")
            {
                lde.Days = d;
                ah.InsertLeaveDays(lde);
                txtDays.Text = string.Empty;
                txtComments.Text = string.Empty;
                BindDays();
                lblDayMessg.ForeColor = System.Drawing.Color.Green;
                lblDayMessg.Text = "added successfully";
                
            }
            else
            {
                lblDayMessg.ForeColor = System.Drawing.Color.Red;
                lblDayMessg.Text = "Please enter valid day(s)";
               
            }
        }
        else
        {
            lde.Days = d;
            ah.InsertLeaveDays(lde);
            txtDays.Text = string.Empty;
            txtComments.Text = string.Empty;
            BindDays();
            lblDayMessg.ForeColor = System.Drawing.Color.Green;
            lblDayMessg.Text = "added successfully";
           
        }



        //if (lde.Days > 0)
        //{
        //    if (lde.Days == Ttxvalue)
        //    {
        //        ah.InsertLeaveDays(lde);
        //        txtDays.Text = string.Empty;
        //        txtComments.Text = string.Empty;
        //        BindDays();
        //    }
        //    else
        //    {
        //        if (Convert.ToDouble(Ttxvalue) > d)
        //        {
        //            Ttxvalue = Ttxvalue - 1;
        //        }
        //        double d1 = Convert.ToDouble(Ttxvalue) + Convert.ToDouble(0.5);
        //        if (lde.Days == d1)
        //        {
        //            ah.InsertLeaveDays(lde);
        //            txtDays.Text = string.Empty;
        //            txtComments.Text = string.Empty;
        //            BindDays();
        //        }
        //        else
        //        {
        //            lblDayMessg.Text = "Enter valid days";
        //        }
        //    }
        //}
    }
    private void BindDays()
    {
        AllowanceHelper ah = new AllowanceHelper();
        grdDaysLeave.DataSource = ah.LeaveDaysSelectByUser(int.Parse(getUserId.Value));
        grdDaysLeave.DataBind();
    }

    protected void grdDaysLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            AllowanceHelper ah = new AllowanceHelper();
            ah.DeleteLeaveDays(int.Parse(e.CommandArgument.ToString()));

        }
    }
    protected void grdDaysLeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdDaysLeave.EditIndex = -1;
        BindDays();

    }
    protected void imgCancel_Click(object sender, EventArgs e)
    {
        txtDays.Text = "";
    }

    public string YearFormate(string year)
    {
        string myear = string.Empty;
        try
        {
            int nextYear =Convert.ToInt32(year)+1;

            myear =  year + "-" + nextYear.ToString(); 
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return myear;
    }
}
