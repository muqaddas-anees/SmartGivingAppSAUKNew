using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Controls
{
    public partial class ResourceSideMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var lPath = Deffinity.PortfolioManager.Portfilio.setLogo();
                img_logo.Src = lPath;// "~/Content/assets/images/logo@2x.png";
                img_logo_small.Src = lPath;// "~/Content/assets/images/logo-collapsed@2x.png";

                //if (Request.Url.AbsolutePath.ToLower().Contains("serviceproviders.aspx"))
                //{
                //    img_logo.Src = "~/Content/images/FirstDataLogo.png";
                //    img_logo_small.Src = "~/Content/images/FirstDataLogo.png";
                //}


                string link = string.Empty;
                //if the user is customer
                if (sessionKeys.SID == 7)
                {
                    link = "~/WF/Portal/Home.aspx";
                    link_small_logo.HRef = link_logo.HRef = link;
                }
                else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link = "~/WF/DC/FLSResourceList.aspx?type=FLS";
                    link_small_logo.HRef = link_logo.HRef = link;
                }
                else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                {
                    link = "~/WF/DC/FLSJlist.aspx?type=FLS";
                    link_small_logo.HRef = link_logo.HRef = link;
                }
                else if (sessionKeys.SID == 12)
                {
                    link = "~/WF/DC/FLSJlist.aspx?type=FLS";
                    link_small_logo.HRef = link_logo.HRef = link;
                    //LinkFlatrate.Visible = false;
                }
                else if (sessionKeys.SID == 11)
                {
                    link = "~/WF/DC/FLSJlist.aspx?type=FLS";
                    link_small_logo.HRef = link_logo.HRef = link;
                    //LinkFlatrate.Visible = false;
                    //LinkSericeDesk.Visible = false;
                    //LinkFeedback.Visible = false;
                    //LinkDocs.Visible = false;
                }

                //VisibilityChecking();
            }
        }


        public string setLogo()
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string userImg = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID));
            if (File.Exists(userImg))
                defaultImg = userImg;
            return defaultImg;
        }
    }
}