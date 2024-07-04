using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_ServiceDeskHomeSiteCharts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (sessionKeys.PortfolioID == 0)
            {
                ddlCustomer.SelectedIndex = 1;
            }
            else
            {
                ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            txtFromdate.Text = FirstDayOfWeek(DateTime.Now).ToShortDateString();
            txttodate.Text = LastDayOfWeek(DateTime.Now).ToShortDateString();

            Label1.Text = "Volume of Open Calls By Site from " + txtFromdate.Text + " To " + txttodate.Text;
            Label2.Text = "Volume of Completed Calls By Site from " + txtFromdate.Text + " To " + txttodate.Text;
            Label3.Text = "Calls by Site from " + txtFromdate.Text + " To " + txttodate.Text;
        }
    }
    public static DateTime FirstDayOfWeek(DateTime date)
    {
        DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        int offset = fdow - date.DayOfWeek;
        DateTime fdowDate = date.AddDays(offset);
        return fdowDate;
    }

    public static DateTime LastDayOfWeek(DateTime date)
    {
        DateTime ldowDate = FirstDayOfWeek(date).AddDays(6);
        return ldowDate;
    }

}