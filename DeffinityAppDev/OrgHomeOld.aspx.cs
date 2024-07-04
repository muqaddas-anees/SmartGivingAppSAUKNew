using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class OrgHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["orgguid"] != null)
                {
                    var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgguid"].ToString()).FirstOrDefault();

                    sessionKeys.PortfolioID = orgEntity.ID;
                    sessionKeys.PortfolioName = orgEntity.PortFolio;

                    imglogo.Src = Deffinity.PortfolioManager.Portfilio.setLogo();
                }

                
            }
        }

      
        //public string getImageUrl()
        //{
        //    return "../../WF/UploadData/Customers/portfolio_" + sessionKeys.PortfolioID + ".png?d=" + DateTime.Now.Second;
        //}
    }
}