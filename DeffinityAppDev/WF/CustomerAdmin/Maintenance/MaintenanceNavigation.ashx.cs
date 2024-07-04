using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    /// <summary>
    /// Summary description for MaintenanceNavigation
    /// </summary>
    public class MaintenanceNavigation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
          
            context.Response.Redirect("~/WF/CustomerAdmin/Maintenance/MaintenancePlanSetup.aspx" + string.Format("?ContactID={0}&addressid={1}&planid={2}", QueryStringValues.ContactID, QueryStringValues.AddressID, BindPlanData()),false);
           

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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}