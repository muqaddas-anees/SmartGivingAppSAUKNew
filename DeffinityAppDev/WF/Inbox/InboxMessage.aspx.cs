using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;


public partial class InboxMessage : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7)
            this.Page.MasterPageFile = "~/WF/CustomerMainTab.Master";
        else if (sessionKeys.SID == 0)
            Response.Redirect("~/WF/Default.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindInbox();
                //Master.PageHead = "Inbox";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      
    }
    private void BindInbox()
    {
        int inboxCount = InboxBAL.GetUserInboxCount();
        if (inboxCount > 0)
            lblInboxCount.Text = "Inbox (" + inboxCount + ")";
        else
            lblInboxCount.Text = "Inbox";

    }
    private void BindGrid()
    {
        gvInbox.DataSource = InboxBAL.BindUserInbox();
        gvInbox.DataBind();
    }

    protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Inbox")
            {
                lblMailMsg.Text = string.Empty;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvInbox.Rows[rowIndex];
                LinkButton lnkbtn = (LinkButton)row.FindControl("lnkbtnInbox");
                HiddenField hfPath = (HiddenField)row.FindControl("hfPath");
                HiddenField hfIsViewed = (HiddenField)row.FindControl("hfIsViewed");
                HiddenField hfFrom = (HiddenField)row.FindControl("hfFrom");
                HiddenField hfTo = (HiddenField)row.FindControl("hfTo");
                HiddenField hfID = (HiddenField)row.FindControl("hfID");

                int id = Convert.ToInt32(hfID.Value);
                if (!(bool.Parse(hfIsViewed.Value)))
                {
                    InboxBAL.UpdateInbox(id);
                    BindGrid();
                    BindInbox();
                }
              
                string path = Server.MapPath("~/WF/UploadData/Inbox/" + hfPath.Value + ".htm");
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = string.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lblMailMsg.Text += line;
                    }
                }
                pnlHeader.Visible = true;
                pnlMailMessage.Visible = true;
                lblFrom.Text = hfFrom.Value;
                lblTo.Text = hfTo.Value;
                lblSubject.Text = lnkbtn.Text;
                lblReceived.Text = lnkbtn.ToolTip.Substring(9);
               

            }
            if (e.CommandName == "Deleterow")
            {
                var gid = Guid.Parse(e.CommandArgument.ToString());
                InboxBAL.InboxDeleteByGuid(gid);
                var fileName = Server.MapPath("~/WF/UploadData/Inbox/" + gid + ".htm");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                BindGrid();
                BindInbox();
                pnlHeader.Visible = false;
                pnlMailMessage.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfIsViewed = (HiddenField)e.Row.FindControl("hfIsViewed");
                LinkButton lnkbtnInbox = (LinkButton)e.Row.FindControl("lnkbtnInbox");
                if (!bool.Parse(hfIsViewed.Value))
                {
                    lnkbtnInbox.Font.Size = new FontUnit(7);
                    lnkbtnInbox.Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
        
    }
   
}