using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class HourlyRateSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var p = PortfolioMgt.BAL.PartnerHourlyRateBAL.PartnerHourlyRateBAL_SelectByPortfolioID();

                    txtHourlyRate.Text = string.Format("{0:F2}", p);


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
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               var p=  PortfolioMgt.BAL.PartnerHourlyRateBAL.PartnerHourlyRateBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                if(p != null)
                {
                    p.HourlyRate = Convert.ToDouble(!string.IsNullOrEmpty(txtHourlyRate.Text.Trim()) ? txtHourlyRate.Text.Trim() : "0");
                    PortfolioMgt.BAL.PartnerHourlyRateBAL.PartnerHourlyRateBAL_Update(p);
                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
                else
                {
                    p = new PortfolioMgt.Entity.PartnerHourlyRate();
                    p.HourlyRate = Convert.ToDouble(!string.IsNullOrEmpty(txtHourlyRate.Text.Trim()) ? txtHourlyRate.Text.Trim() : "0");
                    PortfolioMgt.BAL.PartnerHourlyRateBAL.PartnerHourlyRateBAL_Add(p);
                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}