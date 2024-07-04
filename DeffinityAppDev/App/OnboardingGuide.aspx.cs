using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class OnboardingGuide : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    var pdetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (pdetails != null)
                    {
                        if (pdetails.OrgUniqID != null)
                        {
                            linkLanding.NavigateUrl = "~/web/" + pdetails.OrgUniqID;
                            myInput.Value = Deffinity.systemdefaults.GetWebUrl() + "/Web/" + pdetails.OrgUniqID.ToString();
                        }
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                
            }
        }
    }
}