using DC.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Controls
{
    public partial class CustomerSideMenu : System.Web.UI.UserControl
    {
        IPortfolioRepository<PortfolioMgt.Entity.CustomerConfig> cRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.CustomerPotalSection> cpRepository = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var lPath = Deffinity.PortfolioManager.Portfilio.setLogo();
                img_logo.Src = lPath;// "~/Content/assets/images/logo@2x.png";
                img_logo_small.Src = lPath;// "~/Content/assets/images/logo-collapsed@2x.png";

                CheckDlt();
                if (sessionKeys.SID == 7)
                {
                    int contactid = 0;
                    if (Session["contactid"] == null)
                        contactid = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID);
                    else
                        contactid = Convert.ToInt32(Session["contactid"].ToString());

                    linkProfileurl.HRef = "~/WF/Portal/ContactDetails.aspx?ContactID=" + contactid;
                }
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
        public void CheckDlt()
        {
            cRepository = new PortfolioRepository<PortfolioMgt.Entity.CustomerConfig>();
            cpRepository = new PortfolioRepository<PortfolioMgt.Entity.CustomerPotalSection>();
            var cplist = cRepository.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID || o.PortfolioID == 0).ToList();
            var sList = cpRepository.GetAll().ToList();
            //DataTable DT = CustomerConfigManager.CustomerConfig_Select(sessionKeys.PortfolioID);

            if (sList.Count > 0)
            {
                try
                {
                    link_Projects.Visible = SetVisibility("project", sList, cplist);
                    link_ServiceRequest.Visible = SetVisibility("Service Desk", sList, cplist);
                    link_ServiceCatalgues.Visible = SetVisibility("Service Catalog", sList, cplist);
                    link_HealthChecks.Visible = SetVisibility("Health Checks", sList, cplist);
                    link_Docs.Visible = SetVisibility("Doc", sList, cplist);
                    link_FlowChats.Visible = SetVisibility("Flow Charts", sList, cplist);
                    link_OrgCharts.Visible = SetVisibility("Organisation Charts ", sList, cplist);
                    link_MyTasks.Visible = SetVisibility("My Tasks", sList, cplist);
                    link_Timesheet.Visible = SetVisibility("Timesheet", sList, cplist);
                    //link_ChangeControl.Visible = SetVisibility("change control", sList, cplist);
                    //link_Units.Visible = SetVisibility("unit", sList, cplist);
                    link_delivery.Visible = SetVisibility("Delivery", sList, cplist);
                    link_accesscontrol.Visible = SetVisibility("Access", sList, cplist);
                    link_permittowork.Visible = SetVisibility("PermittoWork", sList, cplist);
                    //link_commondocuments.Visible = SetVisibility("Common Documents", sList, cplist);
                    //link_Inventory.Visible = SetVisibility("inventory", sList, cplist);
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
        }

        public bool SetVisibility(string getval, List<PortfolioMgt.Entity.CustomerPotalSection> slist, List<PortfolioMgt.Entity.CustomerConfig> cplist)
        {
            bool retval = false;
            var s = slist.Where(o => o.SectionName.ToLower().Contains(getval.ToLower())).FirstOrDefault();
            if (s != null)
            {
                var c = cplist.Where(o => o.SectionID == s.ID && o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                if (c != null)
                    retval = c.Visible;
                else
                {
                    var cp = cplist.Where(o => o.SectionID == s.ID && o.PortfolioID == 0).FirstOrDefault();
                    if (cp != null)
                    {
                        retval = cp.Visible;
                    }
                    else
                    {
                        retval = false;
                    }
                }
            }

            return retval;
        }
    }
}