using DC.DAL;
using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

public partial class DC_ServiceDeskHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Service Desk";
            BindData();
            bindCheckList();
            if (sessionKeys.PortfolioID == 0)
            {
                ddlCustomerInEngineer.SelectedIndex = 1;
            }
            else
            {
                ddlCustomerInEngineer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            txtFromdate.Text = FirstDayOfWeek(DateTime.Now).ToShortDateString();
            txttodate.Text = LastDayOfWeek(DateTime.Now).ToShortDateString();
            Label1.Text = "Volume of Open Calls By Engineer from " + txtFromdate.Text + " To " + txttodate.Text;
            Label2.Text = "Volume of Completed Calls By Engineer from " + txtFromdate.Text + " To " + txttodate.Text;
            Label3.Text = "Number of Calls by Status and by Engineer from " + txtFromdate.Text + " To " + txttodate.Text;
            Label4.Text = "Calls Completed During " + txtFromdate.Text + " To " + txttodate.Text + " by Site";
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
    public void bindCheckList()
    {
        using (UserDataContext Udc = new UserDataContext())
        {
            using (DCDataContext dc = new DCDataContext())
            {
                var clist = Udc.Contractors.ToList();
                var FlsList = dc.FLSDetails.ToList();
                var result = (from a in FlsList
                              join b in clist on a.UserID equals b.ID
                              select new
                              {
                                  value1 = b.ID,
                                  name = b.ContractorName
                              }).Distinct().OrderBy(o=>o.name).ToList();
                NamesCheckList.DataSource = result;
                NamesCheckList.DataValueField = "value1";
                NamesCheckList.DataTextField = "name";
                NamesCheckList.DataBind();

                foreach (var val in result)
                {
                    for (int i = 0; i < NamesCheckList.Items.Count; i++)
                    {
                        ListItem listItem = NamesCheckList.Items[i];

                        if (listItem.Value.ToString() == val.value1.ToString())
                        {
                            listItem.Text = "&nbsp;<span title='" + listItem.Value.ToString() + "'></span>" + listItem.Text.Trim();
                        }
                    }
                }
            }
        }
    }
    public void BindData()
    {
        projectTaskDataContext db = new projectTaskDataContext();
        if (sessionKeys.SID == 2 || sessionKeys.SID == 3)
        {
            int[] ids = { 4, 5 };
            var projects = (from r in db.ProjectStatus
                            where !ids.Contains(r.ID)
                            select r);
            if (projects != null)
            {
                ddlStatus.DataSource = projects;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "ID";
                ddlStatus.DataBind();
            }
        }
        else
        {
            ddlStatus.DataSource = Deffinity.ProjectMangers.ProjectManager.ProjectStatus();
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
        }
    }
}