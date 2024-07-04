using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PortfolioMgt.DAL;
using UserMgt.DAL;
using UserMgt.Entity;
using System.Linq;

public partial class ChangeControlDashBorad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Change Control ";
        
        if (!IsPostBack)
        {
            BindCustomers();
            //BindReports();
            BindData();
        }
        iFrameSetUrl.Attributes.Add("onLoad", "iFrameHeight()");
        changeTabVisibility();
    }
    private void changeTabVisibility()
    {
       
    }


    private void BindReports()
    {
        try
        {
            UserDataContext reprorts = new UserDataContext();

            var listOfReports = (from r in reprorts.portaldashboardrpts
                                 select r).ToList();

            var isExist = (from r in reprorts.PortalDashBoardOpts
                           where r.CustomerID == sessionKeys.PortfolioID
                           select r).ToList();
            if (isExist != null)
            {
                if (isExist.Count != 0)
                {
                    var list = (from r in reprorts.PortalDashBoardOpts
                                where r.CustomerID == sessionKeys.PortfolioID
                                orderby r.ReportName
                                select r).ToList();
                    ddlDropDown.DataSource = list;
                    ddlDropDown.DataValueField = "ReportID";
                    ddlDropDown.DataTextField = "ReportName";
                    ddlDropDown.DataBind();
                    //ddlDropDown.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    var listOfReports1 = (from r in reprorts.portaldashboardrpts
                                          orderby r.RportName
                                          select r).ToList();
                    ddlDropDown.DataSource = listOfReports1;
                    ddlDropDown.DataValueField = "ReportID";
                    ddlDropDown.DataTextField = "RportName";
                    ddlDropDown.DataBind();
                    ddlDropDown.Items.Insert(0, new ListItem("Please select...", "0"));
                    ddlDropDown.SelectedValue = "1";
                }
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomer.DataSource = portfolio;
            ddlCustomer.DataTextField = "PortFolio";
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataBind();

            if (sessionKeys.PortfolioID > 0)
            {
                ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            else
            {
                sessionKeys.PortfolioID = int.Parse(ddlCustomer.SelectedValue);
                sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
            }
            //ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string s = ddlDropDown.SelectedValue;

        switch (s)
        {
            case "1":
                iFrameSetUrl.Attributes.Add("src", "ChangeControlDashBoardCharts.aspx?Custom=" + CheckCustomer() + "&Panel=1");
                break;
            case "2":
                iFrameSetUrl.Attributes.Add("src", "ChangeControlDashBoardCharts.aspx?Custom=" + CheckCustomer() + "&Panel=2");
                break;
            case "3":
                iFrameSetUrl.Attributes.Add("src", "ChangeControlDashBoardCharts.aspx?Custom=" + CheckCustomer() + "&Panel=3");
                break;
            case "4":
                iFrameSetUrl.Attributes.Add("src", "ChangeControlDashBoardCharts.aspx?Custom=" + CheckCustomer() + "&Panel=4");
                break;
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        sessionKeys.PortfolioID = int.Parse(ddlCustomer.SelectedValue);
        sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
       // BindReports();
        BindData();

    }
    //alter table portaldashboardrpts
    //add ReportID int
    private string CheckCustomer()
    {
        string retval = string.Empty;

        if (Request.QueryString["customer"] != null)
            retval = "?customer=0";

        return retval;
    }
}
