using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Infragistics.UltraChart.Core;
using Infragistics.UltraChart.Core.ColorModel;
using Infragistics.UltraChart.Core.Util;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Core.Primitives;
using Infragistics.UltraChart.Data;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Resources.Editor;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Shared.Events;

using System.Collections;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Incidents.Entity;
using Incidents.StateManager;
using Incidents.DAL;


public partial class ChangeControlDashBoardCharts : System.Web.UI.Page
{
    private const int columnWidth = 100;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataDictionary();
            BindArea();
            Chart_Bind();
        }
    }

    private void DataDictionary()
    {
        lblSubCategory.Text = DataDictionaryBAL.DataDictionary_GetName("Sub Category");
        lblArea.Text = DataDictionaryBAL.DataDictionary_GetName("Area");
        lblCategory.Text = DataDictionaryBAL.DataDictionary_GetName("Category");
        
    }
    #region Data filter data dinding
    private void BindArea()
    {
        using (PortfolioDataContext pdt = new PortfolioDataContext())
        {
            ddlArea.DataSource = from area in pdt.incident_Areas
                                 where area.Portfolio == sessionKeys.PortfolioID
                                 orderby area.Name ascending
                                 select new { id = area.ID, name = area.Name };
            ddlArea.DataTextField = "name";
            ddlArea.DataValueField = "id";
            ddlArea.DataBind();

            ddlCategory.DataSource = from p in pdt.Projectcategory_Masers
                                     where p.PortfolioID == sessionKeys.PortfolioID
                                     orderby p.CategoryName ascending
                                     select new { id = p.ID, name = p.CategoryName };
            ddlCategory.DataTextField = "name";
            ddlCategory.DataValueField = "id";
            ddlCategory.DataBind();


        }
        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));

    }
   
    #endregion
    private void SetColors(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1)
    {

        UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
        System.Drawing.Color[] chartColor;
        chartColor = new System.Drawing.Color[]{
            
            //53 different color's

            Color.Orange,Color.Orchid,Color.Pink,Color.Green,Color.Black,Color.Lime,Color.Red,Color.Blue,
            Color.Yellow,Color.Aqua,Color.DeepPink,Color.Maroon,Color.DarkMagenta,Color.DarkOrange,Color.DarkOrchid,Color.DarkRed,Color.DarkSalmon,
           Color.GreenYellow, Color.AliceBlue,Color.AntiqueWhite,Color.Aquamarine,Color.Azure,Color.Beige,Color.BlanchedAlmond,
            Color.BlueViolet,Color.Brown,Color.BurlyWood,Color.DarkBlue,Color.DarkCyan,Color.DarkGray,
            Color.DarkGreen,Color.DarkKhaki,Color.DarkSeaGreen,Color.DarkSlateBlue,Color.DarkSlateGray,
            Color.DarkTurquoise,Color.DarkViolet,Color.DeepSkyBlue,
            Color.HotPink,Color.IndianRed,Color.LimeGreen,Color.OrangeRed,Color.RosyBrown,
            Color.RoyalBlue,Color.SaddleBrown,Color.Salmon,Color.SandyBrown,Color.SeaGreen,Color.SteelBlue,
            Color.Tan,Color.Tomato,Color.Violet,Color.Cyan
            

        };
        UltraChart1.ColorModel.CustomPalette = chartColor;
    }
    private void Bind_chartByCategory()
    {
        try
        {
            SetColors(chart_bycategory);
            //Assets_MovesBySite_SubCat..Assets_MovesBySite_SubCatNew
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ChangeControl_ChartByCategoryID",
                           new SqlParameter("@fromDate", Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text)),
                           new SqlParameter("@toDate", Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text)),
                           new SqlParameter("@Category", ddlCategory.SelectedValue),
                           new SqlParameter("@SubCat", ddlSubCategory.SelectedValue), new SqlParameter("@PortfolioID", sessionKeys.PortfolioID)).Tables[0];

            chart_bycategory.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
            //chrtCS.Axis.X.RangeMin = 0;
            chart_bycategory.Axis.X.Labels.Visible = false;
            chart_bycategory.Legend.Visible = true;
            chart_bycategory.TitleTop.FontSizeBestFit = true;
            //chrtCS.TitleTop.HorizontalAlign =
            chart_bycategory.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;


            if (ddlCategory.SelectedValue == "0")
            {
                chart_bycategory.TitleTop.Text = "Volume of Requests by " + lblCategory.Text;
            }
            if (ddlCategory.SelectedValue != "0")
            {
                chart_bycategory.TitleTop.Text = "Volume of Requests by "+lblCategory.Text +" :" + ddlCategory.SelectedItem; ;
            }
            
            if (dt.Rows.Count >= 1 && dt.Columns.Count > 1)
            {
                Hashtable labelRenderers = new Hashtable();
                labelRenderers.Add("CUSTOM", new LabelRenderer());
                chart_bycategory.LabelHash = labelRenderers;

                chart_bycategory.Legend.FormatString = "<CUSTOM>";
                chart_bycategory.Visible = true;
                chart_bycategory.Legend.SpanPercentage = ((dt.Columns.Count) * 2) <= 15 ? 15 : ((dt.Columns.Count) * 2);
                chart_bycategory.DataSource = dt;
                chart_bycategory.DataBind();
            }
            else
            {
                chart_bycategory.Visible = true;
                chart_bycategory.EmptyChartText = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private void Bind_ChanrtByCategoryYear()
    {
        try
        {
            SetColors(chart_byYear);
            //Assets_MovesBySite_SubCat..Assets_MovesBySite_SubCatNew
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ChangeControl_ChartByCatagore_Year",
                           new SqlParameter("@fromDate", Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text)),
                           new SqlParameter("@toDate", Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text)),
                           new SqlParameter("@Category", ddlCategory.SelectedValue),
                           new SqlParameter("@SubCat", ddlSubCategory.SelectedValue), new SqlParameter("@PortfolioID", sessionKeys.PortfolioID)).Tables[0];

            chart_byYear.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
            //chrtCS.Axis.X.RangeMin = 0;
            chart_byYear.Axis.X.Labels.Visible = false;
            chart_byYear.Legend.Visible = true;
            chart_byYear.TitleTop.FontSizeBestFit = true;
            //chrtCS.TitleTop.HorizontalAlign =
            chart_byYear.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
            if ( ddlCategory.SelectedValue == "0")
            {
                chart_byYear.TitleTop.Text = "Volume of Requests by " + lblCategory.Text;
            }


            if (ddlCategory.SelectedValue != "0")
            {
                chart_byYear.TitleTop.Text = "Volume of Requests by "+lblCategory.Text +":" + ddlCategory.SelectedItem; ;
            }
            // dt.Columns.Remove("Test");
            if (dt.Rows.Count >= 1 && dt.Columns.Count > 1)
            {
               
                chart_byYear.Visible = true;

                chart_byYear.DataSource = dt;
                chart_byYear.DataBind();
            }
            else
            {
                chart_byYear.Visible = true;
                chart_byYear.EmptyChartText = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindChartByArea()
    {
        try
        {
            SetColors(chart_byArea_category);
            //Assets_MovesBySite_SubCat..Assets_MovesBySite_SubCatNew
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ChangeControl_ChartDataByArea",
                       new SqlParameter("@PortfolioID", sessionKeys.PortfolioID),
                           new SqlParameter("@StartDate", Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text)),
                           new SqlParameter("@EndDate", Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text))
                          ).Tables[0];

            chart_byArea_category.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
            //chrtCS.Axis.X.RangeMin = 0;
            chart_byArea_category.Axis.X.Labels.Visible = false;
            chart_byArea_category.Legend.Visible = true;
            chart_byArea_category.TitleTop.FontSizeBestFit = true;
            //chrtCS.TitleTop.HorizontalAlign =
            chart_byArea_category.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;

            //dt.Columns.Remove("Test");
            if (dt.Rows.Count >= 1 && dt.Columns.Count > 1)
            {
                chart_byArea_category.Visible = true;

                chart_byArea_category.DataSource = dt;
                chart_byArea_category.DataBind();
            }
            else
            {
                chart_byArea_category.Visible = true;
                chart_byArea_category.EmptyChartText = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridChangeControl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Gets the Change from the database and saves to the session and redirects to the ChangeControl.aspx page.
        if (e.CommandName == "Change")
        {
            int changeID = Convert.ToInt32(e.CommandArgument);
            sessionKeys.ChangeControlID = changeID;
            Change change = ChangeHelper.LoadChangesById(changeID);
            ChangeState.ChangeSaver = change;
            //sessionKeys.PortfolioID = change.PortfolioID;
            //sessionKeys.PortfolioName = change.Customer;
            if(Request.QueryString["customer"] != null)
            Response.Redirect("ChangeControlByCustomer.aspx?customer=0", false);
            else
             Response.Redirect("ChangeControl.aspx", false);
        }
    }

    protected void gridChangeControl_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

   
    protected void chrtCS_ChartDrawItem(object sender, ChartDrawItemEventArgs e)
    {

        Box box = e.Primitive as Box;

        if (box == null)
        {

            return;
        }

        if (box.DataPoint == null)
        {

            return;
        }

        int dWidth = box.rect.Width - columnWidth;
        if (dWidth <= 0)
        {

            return;
        }

        box.rect.Width = columnWidth;

        box.rect.X += dWidth / 2;
    }
    protected void imgViewChart_Click(object sender, EventArgs e)
    {
        Chart_Bind();
    }

    private void Chart_Bind()
    {
        if (Request.QueryString["panel"] != null)
        {
            int panelid = int.Parse(Request.QueryString["panel"]);

            if (panelid == 1)
            {
                pnl_byCategory.Visible = true;
                pnl_byYear.Visible = false;
                pnl_chartByAreaCategory.Visible = false;
                pnl_pieCharts.Visible = false;
                Bind_chartByCategory();
            }
            else if (panelid == 2)
            {
                pnl_byCategory.Visible = false;
                pnl_byYear.Visible = true;
                pnl_chartByAreaCategory.Visible = false;
                pnl_pieCharts.Visible = false;
                Bind_ChanrtByCategoryYear();
            }
            else if (panelid == 3)
            {
                pnl_byCategory.Visible = false;
                pnl_byYear.Visible = false;
                pnl_chartByAreaCategory.Visible = true;
                pnl_pieCharts.Visible = false;
                BindChartByArea();
            }
            else if (panelid == 4)
            {
                pnlFilter.Visible = false;
                pnl_byCategory.Visible = false;
                pnl_byYear.Visible = false;
                pnl_chartByAreaCategory.Visible = false;
                pnl_pieCharts.Visible = true;
                ChangeControlPieCharts1.DataBind();
                gridChangeControl.DataBind();
                //BindChartByArea();
            }
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridChangeControl.DataBind();
    }
}
class LabelRenderer : Infragistics.UltraChart.Resources.IRenderLabel
{
    public string ToString(Hashtable Context)
    {
        string label = (string)(Context["ITEM_LABEL"]);

        if (label.Length > 5)
            return label.Substring(0, label.Length - 2);

        return label;
    }
}