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

public partial class PortfolioRag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
       
        if (!IsPostBack)
        {
            try
            {
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
        GridViewProjectRag.DataSource = RagSectiontoPorfolioManager.RagSectionPorfolioSelectAll(sessionKeys.PortfolioID);
        GridViewProjectRag.DataBind();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try{
        RagSectiontoPortfolioEntity rsp = new RagSectiontoPortfolioEntity();
        rsp.PortfolioID = sessionKeys.PortfolioID;
        rsp.RAGDescription = txtRAGDescription.Text.Trim();
        rsp.RAGSectionName = txtRagName.Text.Trim();
        int outval;
        RagSectiontoPorfolioManager.RagSectionPorfolioInsert(rsp,out outval);
        if (outval == 0)
        {
            lblMsg.Text = Resources.DeffinityRes.KeyMilestone +" already exist.";
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
        try{
        int id = (int)GridViewProjectRag.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0];
        if (e.CommandName == "Update")
        {
            int retval =0;
            RagSectiontoPortfolioEntity rsp = new RagSectiontoPortfolioEntity();
            int i = GridViewProjectRag.EditIndex;
            GridViewRow Row = GridViewProjectRag.Rows[i];
            rsp.ID = id;
            rsp.RAGSectionName = ((TextBox)Row.FindControl("txtRAGSectionName")).Text;
            rsp.RAGDescription = ((TextBox)Row.FindControl("txtDescription")).Text;
            rsp.PortfolioID = sessionKeys.PortfolioID;            
            RagSectiontoPorfolioManager.RagSectionPorfolioUpdate(rsp,out retval);
            if (retval == 0)
            {
                lblMsg.Text = Resources.DeffinityRes.KeyMilestones +" already exist.";
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
}
