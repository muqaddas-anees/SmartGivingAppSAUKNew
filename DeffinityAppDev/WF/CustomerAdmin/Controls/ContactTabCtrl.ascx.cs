using DeffinityManager.Portfolio.BAL;
using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Controls
{
    public partial class ContactTabCtrl : System.Web.UI.UserControl
    {
        string[] array_path = {"ContactDetails.aspx","CustomerAddressList.aspx","CustomerEquimentList.aspx", "ContactMaintenanceSchedule.aspx","ContactPaymentInfo.aspx", "CustomerKeyContacts.aspx", "ContactCommunication.aspx" };
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //hidPortfolioID.Value = sessionKeys.PortfolioID.ToString();
                if (!IsPostBack)
                {
                    //set the session customer valuew
                    sessionKeys.IncidentID = QueryStringValues.CallID;
                   // hsdid.Value = sessionKeys.IncidentID.ToString();
                    checkDlt();
                    MaintainVisibility();


                    //check mid is enabled or not
                    link_equipment.Visible = sessionKeys.PayStatus;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void MaintainVisibility()
        {
            link_equipment.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Equipment);
            link_remainders.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Reminders);
            
        }

        //DLT Database list
        //136	DC - FLS - FLS 	1
        //137	DC - FLS - FLS Details	1
        //138	DC - FLS - Assign Team	1
        //139	DC - FLS - Services	1
        //140	DC - FLS - Moves	1
        //141	DC - FLS - Service Units	1
        //142	DC - FLS - Inventory	1
        //End list
        private void checkDlt()
        {
            try
            {
                string[] str = PermissionManager.GetFeatures();
                if (!Page.IsPostBack)
                {
                    //34 ToolboxDataAttribute 43
                    //tab_Fls_overview.Visible = Convert.ToBoolean(str[136]);
                    //tab_assigneteam.Visible = Convert.ToBoolean(str[138]);
                    //tab_services.Visible = Convert.ToBoolean(str[139]);
                    //tab_moves.Visible = false;// Convert.ToBoolean(str[140]);
                    //tab_service_units.Visible = Convert.ToBoolean(str[141]);
                    //tab_inventory.Visible = false;// Convert.ToBoolean(str[142]);
                    //tab_form.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected string getUrl(int i)
        {
            string[] array_path = { "ContactDetails.aspx", "CustomerAddressList.aspx", "CustomerEquimentList.aspx", "ContactMaintenanceSchedule.aspx", "ContactPaymentInfo.aspx","CustomerKeyContacts.aspx", "ContactCommunication.aspx" };

            if (Request.QueryString["ContactID"] != null && Request.QueryString["addid"] != null)

                return array_path[i] + "?ContactID=" + Request.QueryString["ContactID"].ToString() + "&addid=" + Request.QueryString["addid"].ToString();
            else if (Request.QueryString["ContactID"] != null)
                return array_path[i] + "?ContactID=" + Request.QueryString["ContactID"].ToString();
            else
                if (i == 0)
            {
                return array_path[i];
            }
            else
                return "#";

        }
        protected string GetCssClass(int i)
        {

            string rtValue = string.Empty;
            if (i < array_path.Length)
            {
                if ((Request.Url.ToString().ToLower()).Contains(array_path[i].ToLower()) == true)
                {
                    rtValue = "current1";

                }
                string strTemp = string.Empty;
                if (i == 5)
                {
                    strTemp = "DCAssets.aspx";
                    if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                    {
                        rtValue = "current1";
                    }
                    strTemp = "DCDashboard.aspx";
                    if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                    {
                        rtValue = "current1";
                    }
                    strTemp = "DCDealerVoice.aspx";
                    if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                    {
                        rtValue = "current1";
                    }
                    strTemp = "SDdatacentre.aspx";

                }
            }
            return rtValue;
        }
    }
}