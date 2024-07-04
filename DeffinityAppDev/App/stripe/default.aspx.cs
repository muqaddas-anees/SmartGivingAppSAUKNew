using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.stripe
{
    public partial class _default : System.Web.UI.Page
    {
        public string stripePublishableKey = WebConfigurationManager.AppSettings["StripePublishableKey"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}