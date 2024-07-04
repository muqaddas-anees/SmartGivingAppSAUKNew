using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class OrgQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var pval = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == QueryStringValues.UNID).FirstOrDefault();
                if (pval != null)
                {

                    lblOrgname.Text = pval.PortFolio;

                    
                     imgqrcode.ImageUrl = "~/WF/UploadData/Customers/" + pval.OrgarnizationGUID.ToString() + ".png";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}