using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ManufacturerSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hSector.Value = sessionKeys.PartnerID.ToString();
                    //Master.PageHead = "Customer Admin";

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
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}