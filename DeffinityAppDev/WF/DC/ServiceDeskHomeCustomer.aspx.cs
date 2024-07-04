using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_ServiceDeskHomeCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtFromdate.Text = FirstDayOfWeek(DateTime.Now).ToShortDateString();
            txttodate.Text = LastDayOfWeek(DateTime.Now).ToShortDateString();
            try
            {
                //SqlDataSourceTitle2.DataBind();
                //ddlCustomerChart1.SelectedValue = "92";
                //ccdRequestType.DataBind();
                //ccdRequestType.SelectedValue = "187";
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if (sessionKeys.PortfolioID > 0)
            //    ddlCustomerChart1.SelectedValue = sessionKeys.PortfolioID.ToString();
            //else
            //{
            //    if (ddlCustomerChart1.Items.Count >0)
            //    ddlCustomerChart1.SelectedIndex = 1;
            //}
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