using DC.BLL;
using DeffinityManager.Portfolio.BAL;
using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_Controls_TopNavigation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserName.Text = sessionKeys.UName;
            //BindMsgsgrid();
            SetChangePassword();
            MenuItemsVisibility();
        }
    }

    private void MenuItemsVisibility()
    {
        link_DispatchBorad.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.DispatchBoard);
        if(sessionKeys.SID == 4 )
        {
            link_DispatchBorad.Visible = false;
            link_Dashboard.Visible = false;
            link_newContact.Visible = false;
            btnUpgrade.Visible = false;
        }

        if (sessionKeys.SID == 7)
        {
            link_DispatchBorad.Visible = false;
            link_Dashboard.Visible = false;
            link_newContact.Visible = false;
            btnUpgrade.Visible = false;
        }

        btnUpgrade.Visible = sessionKeys.PayStatus;
    }
    private void SetChangePassword()
    {
        if (sessionKeys.SID == 7)
        {
           // linkProfile.Visible = false;
            int contactid = 0;
            if (Session["contactid"] == null)
                contactid = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID);
            else
                contactid = Convert.ToInt32(Session["contactid"].ToString());

            //linkProfileurl.HRef = "~/WF/Portal/ContactDetails.aspx?ContactID=" + contactid;
            link_ChangePwd.HRef = "~/WF/Portal/CustomerChgPassword.aspx";

            imgUser.Src = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + contactid;

            linknewjob.HRef = "~/WF/DC/FLSCustomer.aspx";
        }
        else
        {
            //linkProfile.Visible = false;
            link_ChangePwd.HRef = "~/WF/AdminChgPassword.aspx";
            imgUser.Src = "~/WF/Admin/ImageHandler.ashx?type=user&id=0";
        }
    }
    public void BindMsgsgrid()
    {
        try
        {
            //var InboxList = InboxBAL.BindUserInbox(sessionKeys.UID).Take(4).ToList();
            //RepeaterCntlMsgs.DataSource = InboxList;
            //RepeaterCntlMsgs.DataBind();

            //LblMsgCount.Text = InboxBAL.GetUserInboxCount().ToString();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //btnUpgrade
    protected void btnUpgrade_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/CustomerAdmin/UpgradeModules.aspx", false);
    }
}