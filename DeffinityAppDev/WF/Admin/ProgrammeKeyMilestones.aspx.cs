using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Deffinity.RagSectionPortfolioEntity;
using Deffinity.RagSectionPorfolioManager;
using Deffinity.Bindings;


public partial class ProgrammeKeyMilestones : System.Web.UI.Page
{
    DisBindings getData = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //Master.PageHead = Resources.DeffinityRes.CustomerAdmin;//"Customer Admin";
        if (!IsPostBack)
        {
            try
            {
                BindProgramme();
                BindSubprogramme();
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    //BindGrid
    private void BindGrid()
    {
        GridViewProjectRag.DataSource = RagSectiontoPorfolioManager.RagSectionProgrammeSelectAll(0);
        GridViewProjectRag.DataBind();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            RagSectiontoPortfolioEntity rsp = new RagSectiontoPortfolioEntity();
            rsp.PortfolioID = 0;
            rsp.RAGDescription = txtRAGDescription.Text.Trim();
            rsp.RAGSectionName = txtRagName.Text.Trim();
            rsp.Programmeid = int.Parse(ddlProgramme.SelectedValue);
            rsp.Subprogrammeid = int.Parse(ddlSubProgramme.SelectedValue);
            rsp.Taskid = 0;
            int outval;
            RagSectiontoPorfolioManager.RagSectionPorfolioInsert(rsp, out outval);
            if (outval == 0)
            {
                lblMsg.Text = Resources.DeffinityRes.KeyMlstnalrdyexist;//"Key Milestone already exist.";
            }
            else
            {
                BindGrid();
                ClearFields();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridViewProjectRag_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewProjectRag.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GridViewProjectRag_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewProjectRag.EditIndex = -1;
        BindGrid();
    }
    protected void GridViewProjectRag_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = (int)GridViewProjectRag.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0];
            if (e.CommandName == "Update")
            {
                int retval = 0;
                RagSectiontoPortfolioEntity rsp = new RagSectiontoPortfolioEntity();
                rsp = RagSectiontoPorfolioManager.RagSectionPorfolioSelect(id);
                int i = GridViewProjectRag.EditIndex;
                GridViewRow Row = GridViewProjectRag.Rows[i];
                rsp.ID = id;
                rsp.RAGSectionName = ((TextBox)Row.FindControl("txtRAGSectionName")).Text;
                rsp.RAGDescription = ((TextBox)Row.FindControl("txtDescription")).Text;
                rsp.Programmeid = int.Parse(((DropDownList)Row.FindControl("ddlGridProgramme")).SelectedValue);
                rsp.Subprogrammeid = int.Parse(((DropDownList)Row.FindControl("ddlGridSubProgramme")).SelectedValue);
                //rsp.PortfolioID = 0;
                RagSectiontoPorfolioManager.RagSectionPorfolioUpdate(rsp, out retval);
                if (retval == 0)
                {
                    lblMsg.Text = Resources.DeffinityRes.KeyMlstnalrdyexist;//"Key Milestone already exist.";
                }
                GridViewProjectRag.EditIndex = -1;
                BindGrid();

            }
            else if (e.CommandName == "Delete")
            {
                RagSectiontoPorfolioManager.RagSectionPorfolioDelete(id);
                GridViewProjectRag.EditIndex = -1;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void GridViewProjectRag_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = false;
        }
    }
    protected void GridViewProjectRag_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridViewProjectRag_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();

    }
    private void ClearFields()
    {
        txtRagName.Text = string.Empty;
        txtRAGDescription.Text = string.Empty;
    }
    protected void ddlSubProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubprogramme();
    }

    private void BindSubprogramme()
    {

        getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and level =2 order by operationsowners ", ddlProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
    }
    private void BindProgramme()
    {
        getData.DdlBindSelect(ddlProgramme, string.Format("select id,operationsowners from operationsowners where level =1 order by operationsowners "), "ID", "OperationsOwners", false, false, true);
    }
    protected void GridViewProjectRag_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];

                if (e.Row.FindControl("ddlGridProgramme") != null)
                {
                    DropDownList ddlGridProgramme = e.Row.FindControl("ddlGridProgramme") as DropDownList;
                    getData.DdlBindSelect(ddlGridProgramme, string.Format("select id,operationsowners from operationsowners order by operationsowners "), "ID", "OperationsOwners", false, false, true);
                    ddlGridProgramme.SelectedIndex = ddlGridProgramme.Items.IndexOf(ddlGridProgramme.Items.FindByValue(objList[4].ToString()));


                    DropDownList ddlGridSubProgramme = e.Row.FindControl("ddlGridSubProgramme") as DropDownList;
                    getData.DdlBindSelect(ddlGridSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and level =2 order by operationsowners ", ddlGridProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
                    ddlGridSubProgramme.SelectedIndex = ddlGridSubProgramme.Items.IndexOf(ddlGridSubProgramme.Items.FindByValue(objList[5].ToString()));
                    // lblRemediation.Text = objList[3].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
}
