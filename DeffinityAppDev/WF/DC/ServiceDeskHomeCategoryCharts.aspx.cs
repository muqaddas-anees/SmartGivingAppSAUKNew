using DC.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_ServiceDeskHomeCategoryCharts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //SqlDataSourceTitle2.DataBind();
                //ddlCustomerCat1.SelectedValue = "92";
                //ccdRequestType.DataBind();
                //ccdRequestType.SelectedValue = "187";
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

           // BindList(true, "187");
            txtFromdate.Text = FirstDayOfWeek(DateTime.Now).ToShortDateString();
            txttodate.Text = LastDayOfWeek(DateTime.Now).ToShortDateString();

            Label1.Text = "Volume of Open Calls By Category from " + txtFromdate.Text + " To " + txttodate.Text;
            Label2.Text = "Volume of Completed Calls By Category from " + txtFromdate.Text + " To " + txttodate.Text;
            Label3.Text = "Calls by Category from " + txtFromdate.Text + " To " + txttodate.Text;

            
        }
    }
    public void BindList(bool isfirsttime,string setval)
    {
        int typerequestid = 0;
        if (!isfirsttime)
        {
            if (setval == "0")
            {
                if (!string.IsNullOrEmpty(ddlRequestTypeCat.SelectedValue))
                    typerequestid = Convert.ToInt32(ddlRequestTypeCat.SelectedValue);
            }
        }
        else
        {
            typerequestid = Convert.ToInt32(setval);
        }

        using (DCDataContext Dc = new DCDataContext())
        {
            var siteslist = (from a in Dc.Categories where a.TypeOfRequestID == typerequestid select a).ToList();
            checklistforCategory.DataSource = siteslist;
            checklistforCategory.DataTextField = "Name";
            checklistforCategory.DataValueField = "ID";
            checklistforCategory.DataBind();

            foreach (var val in siteslist)
            {
                for (int i = 0; i < checklistforCategory.Items.Count; i++)
                {
                    ListItem listItem = checklistforCategory.Items[i];

                    if (listItem.Value.ToString() == val.ID.ToString())
                    {
                        listItem.Text = "&nbsp;<span title='" + listItem.Value.ToString() + "'></span>" + listItem.Text.Trim();
                    }
                }
            }
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    protected void ddlRequestTypeCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList(false,"0");
    }
}
