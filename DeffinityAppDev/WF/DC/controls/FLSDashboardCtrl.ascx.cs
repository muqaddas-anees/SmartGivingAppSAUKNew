using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;


public partial class DC_controls_FLSDashboardCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindCustomer();
                BindStatus();
                ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
                BindRequestType();
               
                if (sessionKeys.PortalUser || sessionKeys.SID == 7)
                {
                    ddlCustomer.Enabled = false;
                }
               
                DateTime today = DateTime.Today;
                int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endOfMonth = new DateTime(today.Year, today.Month, numberOfDaysInMonth);
                txtFromDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), startOfMonth);
                txtToDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), endOfMonth);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #region Bind Dropdowns
    private void BindCustomer()
    {

        DC.SRV.WebService service = new DC.SRV.WebService();
        var list = service.GetCompany("", "").ToList();
        List<Dropdown> customerList = new List<Dropdown>();
        foreach (var item in list)
        {
            customerList.Add(new Dropdown { ID = item.value, Name = item.name });
        }
        ddlCustomer.DataSource = customerList;
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataTextField = "Name";
        ddlCustomer.DataBind();
    }

    private void BindRequestType()
    {
        DC.SRV.WebService service = new DC.SRV.WebService();
        var list = service.GetRequestTypeByCustomer("type:" + ddlCustomer.SelectedValue + ";", "").ToList();
        List<Dropdown> requestTypeList = new List<Dropdown>();
        foreach (var item in list)
        {
            requestTypeList.Add(new Dropdown { ID = item.value, Name = item.name });
        }
        ddlRequestType.DataSource = requestTypeList;
        ddlRequestType.DataValueField = "ID";
        ddlRequestType.DataTextField = "Name";
        ddlRequestType.DataBind();
    }
    private void BindStatus()
    {

        DC.SRV.WebService service = new DC.SRV.WebService();
        var list = service.GetStatusByTypeId("type:6;", "").ToList();
        List<Dropdown> stastusList = new List<Dropdown>();
        foreach (var item in list)
        {
            if (item.name == "Closed" || item.name == "Resolved")
                stastusList.Add(new Dropdown { ID = item.value, Name = item.name });
        }
        ddlStatus.DataSource = stastusList;
        ddlStatus.DataValueField = "ID";
        ddlStatus.DataTextField = "Name";
        ddlStatus.DataBind();
    }
    #endregion

    protected void BtnSearch_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRequestType();
    }
}
#region Custom Class
public class Chart
{
    public string Name { get; set; }
    public int Count { get; set; }
}
public class Dropdown
{
    public string ID { get; set; }
    public string Name { get; set; }
}
#endregion