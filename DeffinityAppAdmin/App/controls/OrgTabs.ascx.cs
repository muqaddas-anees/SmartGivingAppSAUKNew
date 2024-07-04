using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class OrgTabs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string getUrl(int i)
        {
            string[] array_path = {"Organization.aspx","Contacts.aspx","AssociatedSites.aspx","Denomination.aspx",
                              "Activities.aspx","Images.aspx"
                              };

            if (Request.QueryString["orgid"] != null)
            {
               
                    return array_path[i] + "?orgid=" + Request.QueryString["orgid"].ToString();
            }
            else
                if (i == 0)
            {
                return array_path[i];
            }
            else
                return "#";

        }

    }
}