using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Health.Entity;
using Health.DAL;
using Health.StateManager;
using Microsoft.ApplicationBlocks.Data;

public partial class PortfolioHealthcheckPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Customer Admin";
            PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            PortfolioHealthCheck portfolioHealthCheck = new PortfolioHealthCheck();
            portfolioHealthCheck.CheckListID = Convert.ToInt32(ddlCheckList.SelectedValue);
            portfolioHealthCheck.PortfolioID = sessionKeys.PortfolioID;
            portfolioHealthCheck.Title = txtTitle.Text;

            PortfolioHealthCheckHelper.Insert(portfolioHealthCheck, Convert.ToInt32(ddlHealthCheckListID.SelectedValue));
            gridHealthChecks.DataBind();
            lblMessage.Text = "Inserted Successfully";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            ClearFormFields();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            lblMessage.Text = "Failed to add. Please try again.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
            gridHealthChecks.DataBind();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ddlHealthCheckListID.DataSource = PortfolioHealthCheckHelper.LoadAllPortfolioHealthChecks();
        ddlHealthCheckListID.DataTextField = "Title";
        ddlHealthCheckListID.DataValueField = "ID";
        ListItem item = new ListItem("Don't import any thing", "0");
        ddlHealthCheckListID.Items.Clear();
        ddlHealthCheckListID.Items.Add(item);
        ddlHealthCheckListID.DataBind();
    }

    private void ClearFormFields()
    {
        ddlCheckList.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
    }

    protected void gridHealthChecks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(((Label)gridHealthChecks.Rows[e.RowIndex].FindControl("lblID")).Text);
        PortfolioHealthCheck portfolioHealthCheck = new PortfolioHealthCheck();
        portfolioHealthCheck = GetPortfolioCheckListRow(id, portfolioHealthCheck);
        portfolioHealthCheck.EmailDistributionList = ((TextBox)gridHealthChecks.Rows[e.RowIndex].FindControl("txtEmailID")).Text;
        PortfolioHealthCheckHelper.Update(portfolioHealthCheck);
    }

    private static PortfolioHealthCheck GetPortfolioCheckListRow(int id, PortfolioHealthCheck portfolioHealthCheck)
    {
        foreach (PortfolioHealthCheck tempPortfolioHealthCheck in PortfolioHealthCheckHelper.LoadAllPortfolioHealthChecks())
        {
            if (tempPortfolioHealthCheck.Id == id)
            {
                portfolioHealthCheck = tempPortfolioHealthCheck;
                break;
            }
        }
        return portfolioHealthCheck;
    }
    protected void gridHealthChecks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(((Label)gridHealthChecks.Rows[e.RowIndex].FindControl("lblID")).Text);
        PortfolioHealthCheck portfolioHealthCheck = new PortfolioHealthCheck();
        portfolioHealthCheck = GetPortfolioCheckListRow(id, portfolioHealthCheck);
        PortfolioHealthCheckHelper.Delete(portfolioHealthCheck);
        PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
        gridHealthChecks.DataBind();
    }

    private int AddEmail(string Name,string Email,string HealthCheckID)
    {
        return SqlHelper.ExecuteNonQuery(connectionString.retrieveConnString(), "Insert_HealthCheckNameMailID", HealthCheckID, Name, Email);
    }
    
    protected void btnAddEmails_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridHealthChecks.Rows.Count; i++)
        {
            getUserIDandMailID(i);
        }
        gridHealthChecks.DataBind();
    }

    private void getUserIDandMailID(int selectedRow)
    {
        GridViewRow row = gridHealthChecks.Rows[selectedRow];
        string name = ((TextBox)row.FindControl("txtName")).Text;
        string eMail = ((TextBox)row.FindControl("txtEmailID")).Text;
        string healthCheckID = ((Label)row.FindControl("lblID")).Text;
        try
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(eMail))
            {
                int i = AddEmail(name, eMail, healthCheckID);
                ((TextBox)row.FindControl("txtName")).Text = string.Empty;
                ((TextBox)row.FindControl("txtEmailID")).Text = string.Empty;
                ((GridView)row.FindControl("gridInner")).DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gridHealthCheck_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int selectedRow = e.NewSelectedIndex;
        bool isForAll=((CheckBox)gridHealthChecks.Rows[selectedRow].FindControl("chkAddToAll")).Checked;
        if (!isForAll)
            getUserIDandMailID(selectedRow);
        else
        {
            string name = ((TextBox)gridHealthChecks.Rows[selectedRow].FindControl("txtName")).Text;
            string eMail = ((TextBox)gridHealthChecks.Rows[selectedRow].FindControl("txtEmailID")).Text;
            foreach (GridViewRow row in gridHealthChecks.Rows)
            {
                string healthCheckID = ((Label)row.FindControl("lblID")).Text;
                AddEmail(name, eMail, healthCheckID);
            }
        }
        gridHealthChecks.DataBind();
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        LogExceptions.WriteExceptionLog(ex);
        Email eMail = new Email();
        StringBuilder sb = new StringBuilder();
        sb.Append("<br/>Message: " + ex.Message);
        sb.Append("<br/>Source: " + ex.Source);
        sb.Append("<br/>TargetSite: " + ex.TargetSite);
        sb.Append("<br/>Data: " + ex.Data);
        sb.Append("<br/>Inner Exception: " + ex.InnerException);
        sb.Append("<br/>Stack Trace:<br/><hr/>" + ex.StackTrace);
        ArrayList mailIds = new ArrayList();
        mailIds.Add("indra@emsysindia.com");
        eMail.SendingMail(string.Empty, "Deffinity Error", sb.ToString(), string.Empty, string.Empty, mailIds);
        //Response.Redirect("~/Message.aspx?aspxerrorpath=/HealthCheckForm.aspx?R=Y");
    }
}
