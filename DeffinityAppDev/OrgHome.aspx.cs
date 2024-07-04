using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class OrgHomeNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   
                    if (Request.QueryString["orgguid"] != null)
                    {
                        Response.Redirect("~/OrgHomeNew.aspx?orgguid=" + Request.QueryString["orgguid"].ToString(),false);
                        return;
                        var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgguid"].ToString()).FirstOrDefault();
                        if (orgEntity != null)
                        {
                            sessionKeys.PortfolioID = orgEntity.ID;
                            sessionKeys.PortfolioName = orgEntity.PortFolio;
                            lblOrgnizationname.Text = orgEntity.PortFolio;
                        }
                        //imglogo.Src = Deffinity.PortfolioManager.Portfilio.setLogo();
                    }

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
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