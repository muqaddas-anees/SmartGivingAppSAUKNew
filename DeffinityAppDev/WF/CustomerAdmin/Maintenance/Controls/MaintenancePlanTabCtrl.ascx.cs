using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance.Controls
{
    public partial class MaintenancePlanTabCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (QueryStringValues.PlanID)
            //{
            //link_Agreement.NavigateUrl = "~/WF/CustomerAdmin/Maintenance/Agreement.aspx?project=" + QueryStringValues.PlanID;
            //link_Viewplan.NavigateUrl = "~/WF/CustomerAdmin/Maintenance/Viewplan.aspx?project=" + QueryStringValues.PlanID;
            //link_Calender.NavigateUrl = "~/WF/CustomerAdmin/Maintenance/Calender.aspx?project=" + QueryStringValues.PlanID;
            //link_Equipment.NavigateUrl = "~/WF/CustomerAdmin/Maintenance/MaintenancePlanSetup.aspx?project=" + QueryStringValues.PlanID;
            // }
            link_return.HRef = "~/WF/CustomerAdmin/CustomerAddressList.aspx?ContactID="+QueryStringValues.ContactID;
        }

        protected string getUrl(int i)
        {
            string[] array_path = { "MaintenancePlanSetup.aspx", "Viewplan.aspx", "Calender.aspx", "Agreement.aspx" };
           // if (QueryStringValues.PlanID)

                //if (Request.QueryString["ContactID"] != null && Request.QueryString["addid"] != null)

                //    return array_path[i] + "?ContactID=" + Request.QueryString["ContactID"].ToString() + "&addid=" + Request.QueryString["addid"].ToString();
                //else if (Request.QueryString["ContactID"] != null)
                    return array_path[i] + string.Format( "?ContactID={0}&addressid={1}&planid={2}",QueryStringValues.ContactID,QueryStringValues.AddressID, BindPlanData());
                //else
               // return array_path[i];

        }

        private int BindPlanData()
        {
            int planid = 0;
            var p = PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_SelectByAddressID(QueryStringValues.AddressID).FirstOrDefault();
            if (p != null)
            {
                planid = p.MaintenacePlanID;
            }
            else
                planid = 0;
            return planid;
        }
    }
}