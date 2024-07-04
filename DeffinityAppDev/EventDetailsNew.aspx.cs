using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events
{
    public partial class EventDetailsNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {

                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                   // var tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                    var tList = cRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                    if(tList != null)
                    sessionKeys.PortfolioID = tList.OrganizationID;


                }


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
        }
    }
}