//using DeffinityManager.Marketplace.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeffinityManager.Portfolio.BAL;
using PortfolioMgt.BAL;

public partial class WF_SideMenu : System.Web.UI.UserControl
{
    //Market_BLL M_BLL = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //set partner id = 
                try
                {

                    lbtnShowUpgrade.Visible = PortfolioMgt.BAL.PortfolioBillingManagerBAL.ShowUpgradeOption();
                    link_upgrade.Visible = lbtnShowUpgrade.Visible;
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

                menuVisibility();
                var lPath = Deffinity.PortfolioManager.Portfilio.setLogo();
                img_logo.Src = lPath;// "~/Content/assets/images/logo@2x.png";
                img_logo_small.Src = lPath;// "~/Content/assets/images/logo-collapsed@2x.png";

                //if (Request.Url.AbsolutePath.ToLower().Contains("serviceproviders.aspx"))
                //{
                //    img_logo.Src = "~/Content/images/FirstDataLogo.png";
                //    img_logo_small.Src = "~/Content/images/FirstDataLogo.png";
                //}


                string link = string.Empty;
                //if the user is customer
                if (sessionKeys.SID == 7)
                {
                    link = "~/WF/Portal/Home.aspx";
                    link_small_logo.HRef = link_logo.HRef = link;
                }
                else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link = "~/WF/Resource/ResourceNewChitChat.aspx";
                    link_small_logo.HRef = link_logo.HRef = link;
                }
                else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                {
                    //link = "~/WF/DC/FLSJlist.aspx?type=FLS";
                    //link_small_logo.HRef = link_logo.HRef = link;

                    var url = "~/WF/DC/DashboardV2.aspx";
                    var dLogin = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.DispatchBoard);
                    //if dipatch borad dispable 
                    //then it should redirect to Job list
                    if (!dLogin)
                    {
                        //admin
                        if (sessionKeys.SID == 1)
                        {
                            link_small_logo.HRef = link_logo.HRef = Deffinity.systemdefaults.GetHomepage("~/WF/DC/DashboardV2.aspx");
                        }
                        else
                        {
                            link_small_logo.HRef = link_logo.HRef = Deffinity.systemdefaults.GetHomepage(url);
                        }
                    }
                    else
                        link_small_logo.HRef = link_logo.HRef = Deffinity.systemdefaults.GetHomepage(url);
                }
                else if (sessionKeys.SID == 12)
                {
                    link = Deffinity.systemdefaults.GetHomepage("~/WF/DC/DashboardV2.aspx");
                    link_small_logo.HRef = link_logo.HRef = link;
                    //LinkFlatrate.Visible = false;
                }
                else if (sessionKeys.SID == 11)
                {
                    link = Deffinity.systemdefaults.GetHomepage("~/WF/DC/DashboardV2.aspx");
                    link_small_logo.HRef = link_logo.HRef = link;
                    //LinkFlatrate.Visible = false;
                    link_servicedesk.Visible = false;
                    LinkFeedback.Visible = false;
                    //LinkDocs.Visible = false;
                }

                checkMenuItemsMID();
                //VisibilityChecking();
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void checkMenuItemsMID()
    {
        LinkFeedback.Visible = sessionKeys.PayStatus;
        link_reports.Visible = sessionKeys.PayStatus;
       // li_premium.Visible = sessionKeys.PayStatus;
       // li_purchasetraining.Visible = sessionKeys.PayStatus;

    }
    public void menuVisibility()
    {
        link_docs.Visible = true;
        link_equipment.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Equipment);
        link_forms.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Forms);
        link_inventory.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Inventory);
        link_invoices.Visible = true;
        link_market.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Marketing);
        //link_Quotations.Visible
        link_remainders.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Reminders);
        link_maintenancePlan.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.ServicePlans) ;
        link_timesheet.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.TimesheetsAndExpenseses);
        link_expenses.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.TimesheetsAndExpenseses);
        link_reports.Visible = true;
        link_settings.Visible = true;
       
    }
    public string setLogo()
    {
        string defaultImg = "~/Content/assets/images/logo@2x.png";
        string userImg = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID));
        if (File.Exists(userImg))
            defaultImg = userImg;
        return defaultImg;
    }

    //public void VisibilityChecking()
    //{
    //    try
    //    {

    //        M_BLL = new Market_BLL();
    //        LinkSericeDesk.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.servicedesk);
    //        LinkHealthChecks.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.healthcheck);
    //        LinkVendorManagement.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Vendormanagement);
    //        LinkInventory.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Inventory);
    //        LinkDelivery.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Delivery);
    //        LinkAccessControl.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Accesscontrol);
    //        LinkPermittoWork.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Permittowork);
    //        LinkTraining.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Training);
    //        LinkChangeControl.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Changecontrol);

    //        LinkForms.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.Forms);
    //        LinkSmartApps.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.SmartApps);

    //        //CustomerAdmin
    //        CustomerAdminServiceDesk.Visible = LinkSericeDesk.Visible;
    //        CustomerAdminHealthCheck.Visible = LinkHealthChecks.Visible;

    //        //DashBoard
    //        DashBoardHealthChecks.Visible = LinkHealthChecks.Visible;

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //public static bool Visibility(string Name)
    //{
    //    bool visibleOrNot = false;
    //    try
    //    {
    //        Market_BLL M_BLL = new Market_BLL();
    //       // visibleOrNot = M_BLL.VisibilityChecking(Name);
    //        return visibleOrNot;
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //    return visibleOrNot;
    //}

}