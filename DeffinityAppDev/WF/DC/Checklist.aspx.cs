using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.IO;
using DC.BLL;
using DC.Entity;

public partial class DC_Checklist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Checklist";
                if (Request.QueryString["callid"] != null)
                {
                    int tid = int.Parse(Convert.ToString(Request.QueryString["callid"]));
                    lblTitle.InnerText = "Checklist - Ticket Reference " + tid;
                    BindData(tid);
                   // lbtnChecklists.NavigateUrl = string.Format("~/DC/Checklist.aspx?callid={0}", tid);
                   // lbtnPermit.NavigateUrl = string.Format("~/DC/PermitToWork.aspx?callid={0}", tid);
                }
                else
                {
                    Response.Redirect("~/DC/PermitToWork.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindData(int cid)
    {
        try
        {
            gvchecklist.DataSource = ChecklistitemsBAL.BindChecklistItemsbyCallID(cid);
            gvchecklist.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvchecklist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Label lblstatus = (Label)gvchecklist.Rows[e.NewEditIndex].FindControl("lblstatus");
                Label lblDescription = (Label)gvchecklist.Rows[e.NewEditIndex].FindControl("lblDescription");
                gvchecklist.EditIndex = e.NewEditIndex; 
                gvchecklist.ShowFooter = false;
                BindData(int.Parse(Request.QueryString["callid"].ToString()));
              
                DropDownList ddlstatus = (DropDownList)gvchecklist.Rows[e.NewEditIndex].FindControl("ddlstatus");
                TextBox txtDescription = (TextBox)gvchecklist.Rows[e.NewEditIndex].FindControl("txtDescription");
                ddlstatus.SelectedValue = lblstatus.Text;  
            txtDescription.Text = lblDescription.Text;
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
           
        }
    }
    protected void gvchecklist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvchecklist.EditIndex = -1;
            gvchecklist.ShowFooter = true;
            BindData(int.Parse(Request.QueryString["callid"].ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
   
    protected void gvchecklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvchecklist.PageIndex = e.NewPageIndex;
            BindData(int.Parse(Request.QueryString["callid"].ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
    protected void gvchecklist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string ID = gvchecklist.DataKeys[e.RowIndex].Value.ToString();
            int i = gvchecklist.EditIndex;
            GridViewRow Row = gvchecklist.Rows[i];
            DropDownList ddlstatus = (DropDownList)Row.FindControl("ddlstatus");
            TextBox txtDescription = (TextBox)Row.FindControl("txtDescription");
            ChecklistItem cl = ChecklistitemsBAL.GetChecklistItembyID(int.Parse(ID));
            if (cl != null)
            {
                cl.ItemDescription = txtDescription.Text;
                cl.Status = ddlstatus.SelectedValue;
                if (ddlstatus.SelectedValue == "Closed")
                    cl.ClosedDate = DateTime.Now;
                ChecklistitemsBAL.ChecklistItemsUpdate(cl);
            }
            gvchecklist.EditIndex = -1;
            gvchecklist.ShowFooter = true;
            BindData(int.Parse(Request.QueryString["callid"].ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvchecklist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Insert")
            {
                ChecklistItem cl = new ChecklistItem();
                cl.CallID = int.Parse(Request.QueryString["callid"].ToString());
                cl.ItemDescription = ((TextBox)gvchecklist.FooterRow.FindControl("txtDescriptionFooter")).Text;
                cl.Status = ((DropDownList)gvchecklist.FooterRow.FindControl("ddlstatusFooter")).SelectedValue;
                if (cl.Status == "Closed")
                    cl.ClosedDate = DateTime.Now;
                ChecklistitemsBAL.AddChecklistItems(cl);
                BindData(int.Parse(Request.QueryString["callid"].ToString()));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvchecklist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton LinkButtonEdit = (ImageButton)e.Row.FindControl("LinkButtonEdit");
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                if (status == "Closed")
                {
                    LinkButtonEdit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}