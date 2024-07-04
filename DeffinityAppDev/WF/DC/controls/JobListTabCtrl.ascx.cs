using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class JobListTabCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string getUrl(int i)
        {
            string[] array_path = { "FLSJlist.aspx?type=FLS", "FLSJlist.aspx?type=FLS&c=1" };

            return array_path[i];

        }
    }
}