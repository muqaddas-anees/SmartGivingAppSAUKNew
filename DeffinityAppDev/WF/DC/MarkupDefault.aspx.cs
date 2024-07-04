using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class MarkupDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtMarkup.Text = "0.00";// PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SelectMargin().ToString();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try {
                PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_Update(sessionKeys.PortfolioID, Convert.ToDouble(txtMarkup.Text.Trim()));

                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}