//using DeffinityManager.Marketplace.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_SideMenu : System.Web.UI.UserControl
{
    //Market_BLL M_BLL = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var lPath = Deffinity.PortfolioManager.Portfilio.setLogo();
            img_logo.Src = lPath;// "~/Content/assets/images/logo@2x.png";
            img_logo_small.Src = lPath;// "~/Content/assets/images/logo-collapsed@2x.png";

           

            string link = string.Empty;
           
        }

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