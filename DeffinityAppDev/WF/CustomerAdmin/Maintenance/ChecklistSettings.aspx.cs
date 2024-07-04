using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class ChecklistSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    hSector.Value = sessionKeys.PartnerID.ToString();

                    if (PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_SelectAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID).ToList().Count()==0)
                    {
                        PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Copy();
                    }


                    if (Request.QueryString["back"] != null)
                    {
                        if (Request.QueryString["pnl"] != null)
                            linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                        else
                            linkBack.NavigateUrl = Request.QueryString["back"];
                        linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                        linkBack.Visible = true;
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