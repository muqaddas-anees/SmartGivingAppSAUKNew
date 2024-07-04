using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance.Controls
{
    public partial class MaintenanceTabCtrl : System.Web.UI.UserControl
    {
       // string[] array_path = { "ManufacturerSettings.aspx", "MaterialSettings.aspx", "TimesPerYearSettings.aspx", "HourlyRateSettings.aspx", "TravelTimeSettings.aspx", "TermsandConditionsSettings.aspx", "ChecklistSettings.aspx" };
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected string getUrl(int i)
        {
            string[] array_path = { "ManufacturerSettings.aspx", "MaterialSettings.aspx", "TimesPerYearSettings.aspx", "HourlyRateSettings.aspx", "TravelTimeSettings.aspx", "TermsandConditionsSettings.aspx","ChecklistSettings.aspx" };

            //if (Request.QueryString["ContactID"] != null && Request.QueryString["addid"] != null)

                //    return array_path[i] + "?ContactID=" + Request.QueryString["ContactID"].ToString() + "&addid=" + Request.QueryString["addid"].ToString();
                //else if (Request.QueryString["ContactID"] != null)
                //    return array_path[i] + "?ContactID=" + Request.QueryString["ContactID"].ToString();
                //else
            return array_path[i];

        }
    }
}