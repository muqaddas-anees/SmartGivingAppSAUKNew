using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class controls_PTInvoicing : System.Web.UI.UserControl
{
    RiseValuation RiseVal = new RiseValuation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid3();
            BindLables();
            CheckUserRole();
        }
    }
    #region Bind Invoice status dropdown
    private void BingInvoicestatus(DropDownList ddlInvoiceStatus, int val)
    {
        ddlInvoiceStatus.DataSource = RiseVal.LoadInvoiceStatus();
        ddlInvoiceStatus.DataTextField = "text";
        ddlInvoiceStatus.DataValueField = "value";
        ddlInvoiceStatus.DataBind();
        ddlInvoiceStatus.Items.Insert(0, new ListItem(" ", "0"));
        ddlInvoiceStatus.SelectedValue = val.ToString();
    }
    private void BindGrid3()
    {
        if (Request.QueryString["Project"] != null)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select VATPercentage,Notes,ProjectReference,ValuationID,InvoiceReference,(select ContractorName from contractors where ID = RaisedBy) as RaisedBy1,RaisedBy,DateRaised, (select SUM(isnull(ClaimedTotal,0)) from ProjectValuationItems where ProjectReference=ProjectValuations.ProjectReference and ProjectValuationID=ProjectValuations.ValuationID)as Value,InvoiceStatus,(case isnull(InvoiceStatus,0) when 1 then 'Paid' when 2 then 'Pending' when 3 then 'Submitted' end)as Status  from ProjectValuations where ProjectReference =@ProjectReference", new SqlParameter("@ProjectReference", int.Parse(Request.QueryString["Project"].ToString()))).Tables[0];
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
    }
    //Invoice summary
    private void BindLables()
    {

        try
        {
            using (IDataReader dr = RiseVal.GetInvoiceSummary(QueryStringValues.Project))
            {
                while (dr.Read())
                {
                    lblRevisedProjectValue.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["RevisedValue"]);
                    lblTotalInvoice.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["TotalValue"]);
                    lblOutstandingVal.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["OutstandingValue"]);
                    lblPaid.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["paid"]);
                    lblUnPaid.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["unpaid"]);
                }
                dr.Close();
            }
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }
                role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }

            }
        }
    }
    private void Disable()
    {
        
        ImageButton6.Enabled = false;
       // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

    }
    private string VatCalcualtion(string VAT, string amt)
    {
        double val = 0;
        try
        {

            //projectTaskDataContext project = new projectTaskDataContext();
            //var items = (from r in project.ProjectValuations
            //             where r.ProjectReference == QueryStringValues.Project && r.ValuationID == int.Parse(ID)
            //             select r).ToList().FirstOrDefault();

            if (Convert.ToDouble(string.IsNullOrEmpty(VAT) ? "0" : VAT) != 0)
            {
                val = Convert.ToDouble(string.IsNullOrEmpty(amt) ? "0" : amt) + ((Convert.ToDouble(string.IsNullOrEmpty(amt) ? "0" : amt) * Convert.ToDouble(string.IsNullOrEmpty(VAT) ? "0" : VAT)) / 100);
            }
            else
            {
                val = 0;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return string.Format("{0:F2}", val);
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {

            int i = GridView3.EditIndex;
            GridViewRow row = GridView3.Rows[i];
            TextBox txtNotes = (TextBox)row.FindControl("txtNotes");
            DropDownList ddlInvoiceStatus = (DropDownList)row.FindControl("ddlInvoiceStatus");
            Label lblID = (Label)row.FindControl("lblID");
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text,
                "Update ProjectValuations set InvoiceStatus=@InvoiceStatus,Notes=@Notes where ValuationID=@ValuationID",
                new SqlParameter("@InvoiceStatus", int.Parse(ddlInvoiceStatus.SelectedValue)),
                new SqlParameter("@ValuationID", int.Parse(e.CommandArgument.ToString())),
                new SqlParameter("@Notes", txtNotes.Text));

            GridView3.EditIndex = -1;
            BindGrid3();
            BindLables();
            // GridView3.DataSource = SqlDataSource2;
            //GridView3.DataBind();
        }
        if (e.CommandName == "Delete")
        {
            try
            {

                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DeleteValuations",
                    new SqlParameter("@ValuationID", int.Parse(e.CommandArgument.ToString())),
                    new SqlParameter("@ProjectRef", int.Parse((Request.QueryString["Project"].ToString()))));
                //Database db = DatabaseFactory.CreateDatabase("DBstring");
                //DbCommand cmd = db.GetStoredProcCommand("Deffinity_DeleteValuations");
                //db.AddInParameter(cmd, "@ProjectRef", DbType.Int32, int.Parse((Request.QueryString["Project"].ToString())));

                //db.AddInParameter(cmd, "@ValuationID", DbType.Int32,int.Parse(e.CommandArgument.ToString()));

                //db.ExecuteNonQuery(cmd);

                //cmd.Dispose();

                BindGrid3();
                BindLables();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        //if (txtProjectvalue.Text.Trim() != "")
        //{
        Response.Redirect("~/WF/Projects/Newinvoice.aspx?type=2&Project=" + QueryStringValues.Project.ToString());
        // }
        //else
        //{
        //    lblError1.Visible = true;
        //    lblError1.Text = "Please check project fee";// Resources.DeffinityRes.Plschktheprjvalues;//"Please check the project values.";
        //}
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.FindControl("ddlInvoiceStatus") != null)
                {
                    //DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                    //DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
                    DropDownList ddlInvoiceStatus = (DropDownList)e.Row.FindControl("ddlInvoiceStatus");
                    BingInvoicestatus(ddlInvoiceStatus, int.Parse(string.IsNullOrEmpty(lblStatusID.Text) ? "0" : lblStatusID.Text));
                }
                if (e.Row.FindControl("lblID1") != null)
                {
                    Label lblID = (Label)e.Row.FindControl("lblID1");
                    Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
                    Label lblInvoice1 = (Label)e.Row.FindControl("lblInvoice1");
                    lblInvoice1.Text = VatCalcualtion(lblID.Text, lblInvoice.Text);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;

        BindGrid3();
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;
        BindGrid3();
        // GridView3.DataSource = SqlDataSource2;
        // GridView3.DataBind();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView3.EditIndex = -1;
        BindGrid3();
        //GridView3.DataSource = SqlDataSource2;
        // GridView3.DataBind();


    }
    #endregion
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    
}