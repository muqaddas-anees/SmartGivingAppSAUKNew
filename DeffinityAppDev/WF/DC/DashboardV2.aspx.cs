using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DashboardV2 : BasePage
    {
        //protected override void InitializeCulture()
        //{
        //    base.InitializeCulture();
        //    if (sessionKeys.Prefix.Length > 0)
        //    {
        //        CultureInfo ci = new CultureInfo(sessionKeys.Prefix);
        //        //Thread.CurrentThread.CurrentCulture = ci;
        //        Thread.CurrentThread.CurrentUICulture = ci;

        //        sessionKeys.Prefix = string.Empty;
        //    }
        //}
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if(Request.QueryString["ui"] != null)
                //{
                //    sessionKeys.Prefix = "es-MX";
                //    Application["MyUICulture"] = "es-MX";
                //    Response.Redirect("~/WF/DC/DashboardV2.aspx", false);
                //}

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}