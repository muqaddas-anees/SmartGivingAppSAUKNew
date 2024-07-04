using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Reports_ProjectTimeBookingReport : System.Web.UI.Page //Basepage
{
    DisBindings getData = new DisBindings();
    //ReportDocument rpt;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPortfolio();
            BindProject();
            BindProjectTasks();
        }
        lblError.Text = "";
    }
    #region DAL
    private DataTable GetPortfolio()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_DisplayPortFolio1").Tables[0];
    }
    private DataTable GetProject(int PortfolioID)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheetBook_ProjectTile", new SqlParameter("@projectPortfolio", PortfolioID),
             new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
    }
    private DataTable GetProjectTask(int ProjectReference)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectTaskbyselect", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
    }


    #endregion
    #region Dropdown bindings

    private void BindPortfolio()
    {
        //ddlCustomers.DataSource = GetPortfolio();
        //ddlCustomers.DataTextField = "PortFolio";
        //ddlCustomers.DataValueField = "ID";
        //ddlCustomers.DataBind();
    }
    private void BindProject()
    {
        ddlProejcts.DataSource = GetProject(int.Parse(ddlCustomers.SelectedValue == "" ? "0" : ddlCustomers.SelectedValue));
        ddlProejcts.DataTextField = "ProjectTitle";
        ddlProejcts.DataValueField = "ID";
        ddlProejcts.DataBind();
    }
    private void BindProjectTasks()
    {
        ddProjectTaks.DataSource = GetProjectTask(int.Parse(ddlProejcts.SelectedValue == null ? "0" : ddlProejcts.SelectedValue));
        ddProjectTaks.DataTextField = "ItemDescription";
        ddProjectTaks.DataValueField = "ID";
        ddProjectTaks.DataBind();
    }
    #endregion

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (ddlProejcts.SelectedValue == "0")
        {
            lblError.Visible = true;
            lblError.Text = "Please select Project Title"; //Resources.DeffinityRes.NoRecords;
        }
        else
        {
            TimesheetSummary.Attributes.Add("src", string.Format("ProjectTimeBookingReportfrm.aspx?project={0}&task={1}&sdate={2}&edate={3}&type=xsl", ddlProejcts.SelectedValue,ddProjectTaks.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim()));

            //BindReport();
        }

    }
    protected void btnExportExcel1_Click(object sender, EventArgs e)
    {
        //if (ddlProejcts.SelectedValue == "0")
        //{
        //    lblError.Visible = true;
        //    lblError.Text = "Please select Project Title";//Resources.DeffinityRes.NoRecords;
        //}
        //else
        //{
        //    TimesheetSummary.Attributes.Add("src", string.Format("ProjectTimeBookingReportfrm.aspx?project={0}&task={1}&sdate={2}&edate={3}&type=xsl1", ddlProejcts.SelectedValue, ddProjectTaks.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim()));

        //    //BindReport();
        //}
    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        if (ddlProejcts.SelectedValue == "0")
        {
            lblError.Visible = true;
            lblError.Text = "Please select Project Title";
        }
        else
        {
            TimesheetSummary.Attributes.Add("src", string.Format("ProjectTimeBookingReportfrm.aspx?project={0}&task={1}&sdate={2}&edate={3}&type=pdf", ddlProejcts.SelectedValue, ddProjectTaks.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim()));
        }
    }
    protected void ddlProejcts_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProjectTasks();

    }
    protected void ddlProejcts_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;

        if (ddl != null)
        {

            foreach (ListItem item in ddl.Items)
            {

                item.Attributes["title"] = item.Text;

            }

        } 
    }
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProject();
    }
    protected void ddProjectTaks_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;

        if (ddl != null)
        {

            foreach (ListItem item in ddl.Items)
            {

                item.Attributes["title"] = item.Text;

            }

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (ddlProejcts.SelectedValue == "0")
        {
            lblError.Visible = true;
            lblError.Text = "Please select Project Title";
        }
        else
        {
            TimesheetSummary.Attributes.Add("src", string.Format("ProjectTimeBookingReportfrm.aspx?project={0}&task={1}&sdate={2}&edate={3}&type=xsl1", ddlProejcts.SelectedValue, ddProjectTaks.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim()));
        }
    }
}
