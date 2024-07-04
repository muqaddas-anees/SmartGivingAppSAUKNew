using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DMain : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
               
                //this.Page.DataBind();
            }
            //Resource settings
            if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            {
                ResourceSideMenu.Visible = true;
                ctrl_sidemenu.Visible = false;
                SiteMap1.Visible = false;
            }
            //to admin pm pm & qa
            else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3 || sessionKeys.SID == 11 || sessionKeys.SID == 12)
            {
                ResourceSideMenu.Visible = false;
                ctrl_sidemenu.Visible = true;
            }
            //setting menu
            //if the user is admin 
            if (sessionKeys.SID == 1)
            {
                pnlSettings.Visible = true;
                btnSettings.Visible = true;
            }
            else
            {
                pnlSettings.Visible = false;
                btnSettings.Visible = false;
            }

            if (sessionKeys.UID == 0)
            {
                Response.Redirect("~/WF/Default.aspx", false);
            }
            else
            {
                Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
                img_user.Src = "~/WF/Admin/ImageHandler.ashx?type=user&id=0";
                link_editprofile.HRef = string.Format("~/WF/Admin/AdminUsers.aspx?uid={0}", sessionKeys.UID);
                link_editprofile1.HRef = string.Format("~/WF/Admin/AdminUsers.aspx?uid={0}", sessionKeys.UID);
            }

            //pnlPayMsg.Visible = true;
            //if (sessionKeys.PartnerTheme.Length > 0)
            //{
            //    hskin.Value = "skin-"+sessionKeys.PartnerTheme;

            //}
            //frm_setpage.Attributes.Add("src", ResolveClientUrl("~/WF/SessionKeepAlive.aspx"));
           
            showPaymentPanel();
            ShowUpgradePanel();


        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ShowUpgradePanel()
    {

        try
        {
            var partnerid = sessionKeys.PartnerID;
            //check this partner has valid plan or not
            var pblist = PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_SelectByPartner(partnerid).ToList();
            if(pblist.Count >0)
            {
                var blist = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID && o.IsActive == true).ToList();
                if(blist.Count() >0)
                {

                }
                else
                {
                    hupgradepopup.Value = "1";
                }
            }


        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private void showPaymentPanel()
    {
        try
        {
            var payIsActive = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.IsPaymentActive();
            sessionKeys.PayStatus = payIsActive;
            if (payIsActive)
            {
                pnlPayMsg.Visible = false;
            }
            else
            {
                pnlPayMsg.Visible = true;

                //set the payment content on popup
                var psetup = PortfolioMgt.BAL.PartnerPopupSetupBAL.PartnerPopupSetupBAL_SelectByPartnerID().FirstOrDefault();
                if (psetup != null)
                {
                    lblContent.Text = Server.HtmlDecode( psetup.PopupContent);
                    btnPopSubmit.NavigateUrl = psetup.LinkUrl;
                    btnPopSubmit.Attributes.Add("style", "width:100%;background-color:#"+psetup.ButtonColor+ ";color:#ffffff");
                    hpop1.Value = psetup.Popup1Time.ToString() + "000";
                    hpop2.Value = psetup.Popup2Time.ToString() + "000";
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string setskin()
    {
        string retval = "page-body skin-navy";
        if (sessionKeys.PartnerTheme.Length > 0)
        {
            hskin.Value = sessionKeys.PartnerTheme;
            retval = "page-body skin-" + sessionKeys.PartnerTheme.ToLower();
        }

        return retval;
    }


}
