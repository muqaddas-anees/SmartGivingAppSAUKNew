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
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Services;
using System.Collections.Generic;


public partial class DashBoard3 : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //to allow dash board users
            //Master.AllowValidSid = 6;  
            if (!this.IsPostBack)
            {
                //bool s=Master.dashboardView ;
                if (sessionKeys.SID != 0)
                {
                    //Master.PageHead = Resources.DeffinityRes.Dashboard; //"Dashboard";
                    ddlProjGroups.DataBind();
                    ddlsubprogram.DataBind();
                    ddlProjects.DataBind();
                    BindCharts();
                    BindToDropdown();
                    //BindGrid();
                    BindPipeLineGrid();
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }



    private void BindToDropdown()
    {
        using (projectTaskDataContext pt = new projectTaskDataContext())
        {

            var projectstatus = from p in pt.ProjectStatus
                                select new { p.ID, p.Status };

            DropDownListStatus.DataSource = projectstatus;
            DropDownListStatus.DataBind();
            DropDownListStatus.Items.Insert(0, new ListItem("All", "0"));
            //set live project default
            DropDownListStatus.SelectedValue = "2";
        }
    }
//    private void BindGrid()
//    {
//        projectTaskDataContext pt = new projectTaskDataContext();
//        if (ddlProjGroups.SelectedValue != "0")
//        {
//            if (ddlsubprogram.SelectedValue != "0")
//            {
//                if (Convert.ToInt32(DropDownListStatus.SelectedValue) > 0)
//                {
//                    var projectlist = from n in pt.ProjectDetails
//                                      where n.OwnerGroupID == Convert.ToInt32(ddlProjGroups.SelectedValue) && n.SubProgramme == Convert.ToInt32(ddlsubprogram.SelectedValue) && n.ProjectStatusID == Convert.ToInt32(DropDownListStatus.SelectedValue)
//                                      orderby n.ProjectReference
//                                      select new { n.ID, n.ProjectReference, n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                    GridView1.DataSource = projectlist;
//                    GridView1.DataBind();
//                }
//                else
//                {
//                    var projectlist = from n in pt.ProjectDetails
//                                      where n.OwnerGroupID == Convert.ToInt32(ddlProjGroups.SelectedValue) && n.SubProgramme == Convert.ToInt32(ddlsubprogram.SelectedValue)
//                                      orderby n.ProjectReference
//                                      select new { n.ID, n.ProjectReference, n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                    GridView1.DataSource = projectlist;
//                    GridView1.DataBind();
//                }
//            }
//            else
//            {
//                if (Convert.ToInt32(DropDownListStatus.SelectedValue) > 0)
//                {
//                    var projectlist = from n in pt.ProjectDetails
//                                      where n.OwnerGroupID == Convert.ToInt32(ddlProjGroups.SelectedValue) && n.ProjectStatusID == Convert.ToInt32(DropDownListStatus.SelectedValue)
//                                      orderby n.ProjectReference
//                                      select new { n.ID, n.ProjectReference, n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                    GridView1.DataSource = projectlist;
//                    GridView1.DataBind();
//                }
//                else
//                {
//                    var projectlist = from n in pt.ProjectDetails
//                                      where n.OwnerGroupID == Convert.ToInt32(ddlProjGroups.SelectedValue)
//                                      orderby n.ProjectReference
//                                      select new { n.ID, n.ProjectReference, n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                    GridView1.DataSource = projectlist;
//                    GridView1.DataBind();
//                }
//            }


//        }
//        else
//        {
//            if (Convert.ToInt32(DropDownListStatus.SelectedValue) > 0)
//            {
//                var projectlist = from n in pt.ProjectDetails
//                                  where n.ProjectStatusID == Convert.ToInt32(DropDownListStatus.SelectedValue)
//                                  orderby n.ProjectReference
//                                  select new { n.ID, n.ProjectReference, n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                GridView1.DataSource = projectlist;
//                GridView1.DataBind();
//            }
//            else
//            {
//                var projectlist = from n in pt.ProjectDetails
//                                  orderby n.ProjectReference
//                                  select new { n.ID,n.ProjectReference,n.ProjectStatusName, n.ProjectReferenceWithPrefix, n.ProjectTitle, n.ProgrammeName, n.SubProgrammeName, n.BudgetaryCostLevel3, n.OwnerName, n.BudgetaryCost, n.ActualCost, n.RAGstatus };

//                GridView1.DataSource = projectlist;
//                GridView1.DataBind();
//            }
//        }

//        if (GridView1.Rows.Count == 0)
//        {

//           // lbl6.Visible = true;
//            pnlGrid.Visible = false;
//        }
//        else
//        {
////lbl6.Visible = false;
//            pnlGrid.Visible = true;
//        }
//    }
    private void BindCharts()
    {
        ////if (checkChart(SqlDataSource1))
        ////{
        ////    UltraChart1.Visible = false;
        ////    lbl1.Visible = true;
        ////}
        ////else
        ////{
        ////    UltraChart1.Visible = true;
        ////    lbl1.Visible = false;
        ////}
        //if (checkChart(SqlDataSource2))
        //{
        //    UltraChart2.DataSourceID = "SqlDataSource2i";
        //    UltraChart2.DataBind();
        //    //UltraChart2.Visible = false;
        //    //lbl2.Visible = true;
        //}
        //else
        //{
        //    //UltraChart2.DataSourceID = "SqlDataSource2";
        //    //UltraChart2.DataBind();
        //    //UltraChart2.Visible = true;
        //    //lbl2.Visible = false;
        //}
        //if (checkChart(SqlDataSource3))
        //{
        //    UltraChart3.DataSourceID = "SqlDataSource3i";
        //    UltraChart3.DataBind();
        //    //UltraChart2.Visible = false;
        //    //lbl2.Visible = true;
        //}
        //else
        //{
        //    //UltraChart3.DataSourceID = "SqlDataSource3";
        //    //UltraChart3.DataBind();
        //    //UltraChart2.Visible = true;
        //    //lbl2.Visible = false;
        //}
        //if (checkChart(SqlDataSource4))
        //{
        //   // SetColors(UltraChart4);
        //    UltraChart4.DataSourceID = "SqlDataSource4i";
        //    UltraChart4.DataBind();
        //    //UltraChart2.Visible = false;
        //    //lbl2.Visible = true;
        //}
        //else
        //{
        //    //SetColors(UltraChart4);
        //    //UltraChart4.DataSourceID = "SqlDataSource4";
        //    //UltraChart4.DataBind();
        //    //UltraChart2.Visible = true;
        //    //lbl2.Visible = false;
        //}
        ////if (checkChart(SqlDataSource7))
        ////{
        ////    UltraChart6.DataSourceID = "SqlDataSource7i";
        ////    UltraChart6.DataBind();
        ////    //UltraChart2.Visible = false;
        ////    //lbl2.Visible = true;
        ////}
        ////else
        ////{
        ////    //UltraChart6.DataSourceID = "SqlDataSource7";
        ////    //UltraChart6.DataBind();
        ////    //UltraChart2.Visible = true;
        ////    //lbl2.Visible = false;
        ////}
      

        
    }
    private void SetColors(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1)
    {

        UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
        System.Drawing.Color[] chartColor;
        chartColor = new System.Drawing.Color[]{
            
            //53 different color's

          System.Drawing.Color.Red,System.Drawing.Color.Yellow,System.Drawing.Color.Green
            

        };
        UltraChart1.ColorModel.CustomPalette = chartColor;
    }
    private bool checkChart(SqlDataSource _sqlDatasource)
    {
        DataView dv;

        dv = (DataView)_sqlDatasource.Select(DataSourceSelectArguments.Empty);
        if (dv.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected string loadImage(string status)
    {
        string returnColour = "images/indcate_green.png";
        if (status != null)
        {
            switch (status.ToUpper())
            {
                case "RED":
                case "r":
                    returnColour = "images/indcate_red.png";
                    break;
                case "GREEN":
                case "g":
                    returnColour = "images/indcate_green.png";
                    break;
                case "AMBER":
                case "a":
                    returnColour = "images/indcate_yellow.png";
                    break;
                                   


            }

        }
        if (!string.IsNullOrEmpty(returnColour))
        {
            returnColour = "<img alt='' src='" + returnColour + "' />";
        }
        return returnColour;
    }
    protected void DropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
        BindPipeLineGrid();
        BindCharts();
    }
    protected void ddlsubprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
        BindPipeLineGrid();
        BindCharts();
    }
    protected void ddlProjGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProjects.SelectedIndex = 0;
        SqlDataSourcesubprogram.SelectParameters[0].DefaultValue = ddlsubprogram.SelectedValue;
        //ddlsubprogram.SelectedValue=
       // BindGrid();
        BindPipeLineGrid();
        BindCharts();
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlProjGroups.SelectedIndex = 0;
        //BindGrid();
        BindPipeLineGrid();
        BindCharts();
    }
    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //   // BindGrid();
    //    BindCharts();
    //}

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.FindControl("lit_Rag") != null)
    //        {
    //            Label lit_Rag = (Label)e.Row.FindControl("lit_Rag");

    //            lit_Rag.Text = loadImage(lit_Rag.Text);
    //        }

    //    }

    //}


    #region copied pipeline Grid 22/02/2011
    private void BindPipeLineGrid()
    {
        try
        {
            gviewclientprojectstatus.DataSource = GetSearchData();
            gviewclientprojectstatus.DataBind();
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private DataTable GetSearchData()
    {
         DataTable dt = new DataTable();
        //string Pstauts = Request.QueryString["Status"].ToString();
         using (SqlConnection con = new SqlConnection(Constants.DBString))
         {
             try
             {
                 con.Open();
                 string cmd1;
                 cmd1 = "DN_DashboardMain_ProjectSummary";

                 SqlCommand cmd = new SqlCommand(cmd1, con);

                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(DropDownListStatus.SelectedValue));
                 cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(sessionKeys.UID));
                 //cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
                 //cmd.Parameters.AddWithValue("@Site", (string.IsNullOrEmpty(ddlsite.SelectedValue) ? " Please Select..." : ddlsite.SelectedValue));
                 //cmd.Parameters.AddWithValue("@Owner", Convert.ToInt32((string.IsNullOrEmpty(ddlowner.SelectedValue) ? "0" : ddlowner.SelectedValue)));
                 cmd.Parameters.AddWithValue("@Programme", Convert.ToInt32((string.IsNullOrEmpty(ddlProjGroups.SelectedValue) ? "0" : ddlProjGroups.SelectedValue)));
                 cmd.Parameters.AddWithValue("@SubProgramme", Convert.ToInt32((string.IsNullOrEmpty(ddlsubprogram.SelectedValue) ? "0" : ddlsubprogram.SelectedValue)));
                 //cmd.Parameters.AddWithValue("@Projectdesc", txtprojectdesc.Text.Trim());
                 cmd.Parameters.AddWithValue("@ProjectReference", Convert.ToInt32((string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue)));
                 SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                 myadapter.Fill(dt);
             }
             catch(Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
             finally
             {
                 con.Close();
             }
         }
        return dt;

    }
    protected string ReturnDays(string days, string CR)
    {
        string stemp = string.Empty;
        if (CR != "")
        {

            double val = Convert.ToDouble(days);
            if (val > 0.0)
            {
                stemp = Math.Round(val, 2).ToString();
            }
            else if (val < 0)
            {
                stemp = " ";
            }
            else
            {
                stemp = "-";
            }
        }
        return stemp;
    }
    protected string totalprojectvalue(double pvalue, double pvariation)
    {
        return string.Format("{0:F2}", pvalue + pvariation);
    }
    protected string ReturnDays(string days, string CR, string DDays, string totalHours)
    {
        string stemp = string.Empty;
        try
        {
            //, string DDays

            if (CR != "")
            {

                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (Convert.ToDouble(DDays) == 0)
                {
                    stemp = " ";
                }
                else if (Convert.ToDouble(DDays) < Convert.ToDouble(totalHours))
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else
                {
                    stemp = "-";
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return stemp;

    }
    protected System.Drawing.Color foreColor(string days)
    {
        System.Drawing.Color stemp = System.Drawing.Color.Gray;
        try
        {

            double val = Convert.ToDouble(days);
            if (val < 0)
            {
                stemp = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return stemp;
    }

    public void SetProgress(Label lblProgress, decimal percent, double days, double ddays, double totldays,string CR)
    {
        if (CR != "")
        {
            if (ddays == 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>Setup&nbsp;Required</TD</TR></TABLE>";
            }
            if (ddays < totldays && days < 0 && ddays != 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>0%</TD</TR></TABLE>";
            }

            if (days != 0 && days > 0)
            {
                if (percent == 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD bgcolor=#FFF7CE width=100% align=center>100%</TD</TR></TABLE>";
                }
                if (percent >= 50)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD  bgcolor=#99FF66 width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 49 && percent > 10)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD  bgcolor=#FFCC00  width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 10 && percent > 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD bgcolor=red width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
            }
        }
     }

    protected void gviewclientprojectstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProgress = (Label)e.Row.FindControl("lblProgress");
            Label lblCR = (Label)e.Row.FindControl("lblCR");
            Label lblPer = (Label)e.Row.FindControl("lblPer");
            Label lblDyas = (Label)e.Row.FindControl("lblDyas");
            Label lblDDays = (Label)e.Row.FindControl("lblDDays");
            Label lblTotalHrs = (Label)e.Row.FindControl("lblTotalDays");
          //  lblProgress.Visible = true;
            if (lblDyas.Text != "-")
            {
                //SetProgress(lblProgress, Math.Round(Convert.ToDecimal(lblPer.Text)), Convert.ToDouble(lblDyas.Text));
                SetProgress(lblProgress, Math.Round(Convert.ToDecimal(lblPer.Text), 2), Convert.ToDouble(lblDyas.Text), Convert.ToDouble(lblDDays.Text), Convert.ToDouble(lblTotalHrs.Text), lblCR.Text);
            }
            //SetProgress(lblProgress,100);
            // progressBar(lblProgress, Convert.ToDecimal(lblPer.Text));

        }
    }
    protected void gviewclientprojectstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gviewclientprojectstatus.DataSource = SortDataTable(GetSearchData(), true);
        gviewclientprojectstatus.PageIndex = e.NewPageIndex;
        gviewclientprojectstatus.DataBind();
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    /// Gets or Sets the gridview sortdirection property
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    /// Gets or Sets the gridview sortexpression property
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    /// Returns the direction of the sorting
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    

    protected void gviewclientprojectstatus_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        //get the pageindex of the grid.
        int pageIndex = gviewclientprojectstatus.PageIndex;
        gviewclientprojectstatus.DataSource = SortDataTable(GetSearchData(), false);
        gviewclientprojectstatus.DataBind();
        gviewclientprojectstatus.PageIndex = pageIndex;
        BindCharts();
    }
    protected bool getImage(string status)
    {
        bool s = false;

        if (Convert.ToInt32(status) > 0)
        {
            s = true;
        }


        return s;
    }
    protected string LoadPriority(string p)
    {
        string retVal = @"~\images\icon_priority_low.gif";
        if (!string.IsNullOrEmpty(p))
        {
            retVal = @"~\images\icon_priority_" + p.ToLower().Trim() + ".gif";
        }
        return retVal;

    }
    protected string LoadOverBudget(double overBudget)
    {
        string retVal = @"~\media\indcate_green.png";
        if (overBudget > 0)
        {
            retVal = @"~\media\indcate_red.png";
        }
        return retVal;

    }
    protected string LoadLate(int late)
    {
        string retVal = @"~\media\indcate_green.png";
        if (late > 1)
        {
            retVal = @"~\media\indcate_red.png";
        }
        return retVal;

    }
    protected bool POVisible(string val)
    {
        bool visible = false;
        if (int.Parse(val) != 0)
        {
            visible = true;
        }

        return visible;
    }
    #endregion

    #region Get chart data
    [WebMethod]
    public static List<Chart1> GetChartData1(string projectGroup, string projectId,string subProgrammeId)
    {
        List<Chart1> dataList = new List<Chart1>();
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("[DEFFINITY_CHART_BUDGETVSACTUAL]", con);
                cmd.Parameters.AddWithValue("@ProjGroup", Convert.ToInt32(projectGroup));
                cmd.Parameters.AddWithValue("@UID", sessionKeys.UID);
                cmd.Parameters.AddWithValue("@ProjID", Convert.ToInt32(projectId));
                cmd.Parameters.AddWithValue("@SubProgID", Convert.ToInt32(subProgrammeId));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            

            foreach (DataRow dtrow in dt.Rows)
            {
                Chart1 details = new Chart1();
                details.Budget = Convert.ToDouble(dtrow[0]);
                details.ActualCost = Convert.ToDouble(string.IsNullOrEmpty(dtrow[1].ToString()) ? "0" : dtrow[1].ToString());
                details.ProjectReference = dtrow[2].ToString();
                dataList.Add(details);
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dataList;
    }
    [WebMethod]
    public static List<Chart2> GetChartData2(string projectGroup, string projectId, string subProgrammeId)
    {
        List<Chart2> dataList = new List<Chart2>();
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[DEFFINITY_CHART_VARIATIONS]", con);
                cmd.Parameters.AddWithValue("@ProjGroup", projectGroup);
                cmd.Parameters.AddWithValue("@UID", sessionKeys.UID);
                cmd.Parameters.AddWithValue("@ProjID", projectId);
                cmd.Parameters.AddWithValue("@SubProgID", subProgrammeId);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }

            foreach (DataRow dtrow in dt.Rows)
            {
                Chart2 details = new Chart2();
                details.Budget = Convert.ToDouble(string.IsNullOrEmpty(dtrow[0].ToString()) ? "0" : dtrow[0].ToString());
                details.Variences = Convert.ToDouble(string.IsNullOrEmpty(dtrow[1].ToString()) ? "0" : dtrow[1].ToString());
                details.ProjectReference = dtrow[2].ToString();
                dataList.Add(details);
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dataList;
    }
    [WebMethod]
    public static List<Chart3> GetChartData3(string projectGroup, string projectId, string subProgrammeId)
    {
        List<Chart3> dataList = new List<Chart3>();
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[DEFFINITY_CHART_PROJECTPERFORMANCE]", con);
                cmd.Parameters.AddWithValue("@ProjGroup", projectGroup);
                cmd.Parameters.AddWithValue("@UID", sessionKeys.UID);
                cmd.Parameters.AddWithValue("@ProjID", projectId);
                cmd.Parameters.AddWithValue("@SubProgID", subProgrammeId);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
          

            foreach (DataRow dtrow in dt.Rows)
            {
                Chart3 details = new Chart3();
                details.Green = Convert.ToInt32(string.IsNullOrEmpty(dtrow[3].ToString()) ? "0" : dtrow[3].ToString());
                details.Amber = Convert.ToInt32(string.IsNullOrEmpty(dtrow[2].ToString()) ? "0" : dtrow[2].ToString());
                details.Red = Convert.ToInt32(string.IsNullOrEmpty(dtrow[1].ToString()) ? "0" : dtrow[1].ToString());
                details.ProjectReference = dtrow[0].ToString();
                dataList.Add(details);
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dataList;
    }
    #endregion
}
#region # Custom Class #
public class Chart1
{
    public double Budget { get; set; }
    public double ActualCost { get; set; }
    public string ProjectReference { get; set; }
}
public class Chart2
{
    public double Budget { get; set; }
    public double Variences { get; set; }
    public string ProjectReference { get; set; }
}
public class Chart3
{
    public int Green { get; set; }
    public int Amber { get; set; }
    public int Red { get; set; }
    public string ProjectReference { get; set; }
}
#endregion

