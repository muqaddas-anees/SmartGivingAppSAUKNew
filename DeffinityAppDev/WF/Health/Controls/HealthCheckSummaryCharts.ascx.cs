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
using Infragistics.WebUI.UltraWebChart;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;

public partial class controls_Summary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable myCountTable = new DataTable();
        //Get the maximum number of pie's that are done in the year.
        DataHelperClass.DDLHelper(myCountTable, "SELECT COUNT(DISTINCT(Title)) FROM HealthCheckList HCL INNER JOIN PortfolioHealthCheck PHC ON HCL.PortfolioHealthCheckID=PHC.ID WHERE DATEADD(DD,0,DateRaised)>=DATEADD(dd,1,DATEDIFF(dd,0,DATEADD(dd,-DATEPART(dd,GETDATE()),DATEADD(mm,-DATEPART(mm,GETDATE())+1,DATEADD(yy,-1,GETDATE()))))) AND DATEADD(DD,0,DateRaised)<GETDATE()  AND LOWER(HCL.Status)='completed'");
        if (((int)myCountTable.Rows[0][0]) > 0)
        {
            #region Span Percentage Calculated.. Commented

            //int spanPercentage = ((int)myCountTable.Rows[0][0]) * 5;
            //if (spanPercentage < 40)
            //    spanPercentage = 40;
            //else if (spanPercentage > 80)
            //    spanPercentage = 70;
            //Set the span percentage dynamically based on the number of pie's that may return.
            //WeeklyChart.Legend.SpanPercentage = MonthlyChart.Legend.SpanPercentage = YearlyChart.Legend.SpanPercentage = spanPercentage;

            #endregion 

            WeeklyChart.ColorModel.ModelStyle = MonthlyChart.ColorModel.ModelStyle = YearlyChart.ColorModel.ModelStyle = ColorModels.CustomLinear;
            
            DataView weeklyView = (DataView)sqlLastWeek.Select(DataSourceSelectArguments.Empty);
            DataView monthlyView = (DataView)sqlLastMonth.Select(DataSourceSelectArguments.Empty);
            DataView yearlyView = (DataView)sqlLastYear.Select(DataSourceSelectArguments.Empty);

            Color[] ChartColors;
            ChartColors = new Color[] { Color.SteelBlue, Color.Brown, Color.LightSeaGreen, Color.YellowGreen, Color.Violet,Color.DarkCyan, Color.MediumSlateBlue, Color.MediumTurquoise, Color.BurlyWood, Color.Green, Color.Chocolate, Color.CornflowerBlue, Color.Crimson };

            Color[] weeklyColors, monthlyColors, yearlyColors;
            weeklyColors = new Color[ChartColors.Length];
            monthlyColors = new Color[ChartColors.Length];
            yearlyColors = new Color[ChartColors.Length];

            SetColors(ChartColors, weeklyView, monthlyView, yearlyView, weeklyColors, monthlyColors, yearlyColors);
            WeeklyChart.ColorModel.CustomPalette = weeklyColors;
            MonthlyChart.ColorModel.CustomPalette = monthlyColors;
            YearlyChart.ColorModel.CustomPalette = yearlyColors;
            //lstSummary.InnerHtml = healthCheckSummary();
        }
    }

    private string healthCheckSummary()
    {
        DataTable myTable = new DataTable();
        DataHelperClass.DDLHelper(myTable, "exec HealthCheckChartSummary");
        StringBuilder htmlText = new StringBuilder();
        using (DataTableReader tableReader = new DataTableReader(myTable))
        {
            string previousValue = string.Empty;
            string presentValue = string.Empty;
            htmlText.Append("<li>");
            while (tableReader.Read())
            {
                presentValue = tableReader.GetString(0);
                int period = tableReader.GetInt32(2);
                if (!presentValue.Equals(previousValue))
                {
                    htmlText.Append("</li>");
                    htmlText.Append("<li>");
                    if (period != 3)
                        htmlText.Append(string.Format("In a year <strong>{0}</strong> has had 0 health check(s)", tableReader.GetString(0)));
                }
                switch (period)
                {
                    //Weekly Results
                    case 1:
                        htmlText.Append(string.Format(", <strong>{0}</strong> in a week", tableReader.GetInt32(1)));
                        break;
                    //Monthly Results
                    case 2:
                        htmlText.Append(string.Format(", <strong>{0}</strong> in a month", tableReader.GetInt32(1)));
                        break;
                    //Yearly Results
                    case 3:
                        htmlText.Append(string.Format("In a year <strong>{0}</strong> has had <strong>{1}</strong> health check(s)", tableReader.GetString(0), tableReader.GetInt32(1)));
                        break;
                }
                previousValue = presentValue;
            }
            htmlText.Append("</li>");
        }
        return htmlText.ToString();
    }

    private static void SetColors(Color[] ChartColors, DataView weeklyView, DataView monthlyView, DataView yearlyView, Color[] weeklyColors, Color[] monthlyColors, Color[] yearlyColors)
    {
        try
        {
            //Set the same color for month and week same as of the year.
            for (int i = 0; i < yearlyView.Count; i++)
            {
                yearlyColors[i] = ChartColors[i];
                for (int j = 0; j < weeklyView.Count; j++)
                {
                    if (yearlyView[i][1].ToString() == weeklyView[j][1].ToString())
                    {
                        weeklyColors[j] = ChartColors[i];
                        break;
                    }
                }
                for (int j = 0; j < monthlyView.Count; j++)
                {
                    if (yearlyView[i][1].ToString() == monthlyView[j][1].ToString())
                    {
                        monthlyColors[j] = ChartColors[i];
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void sqlLastWeek_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            WeeklyChart.Visible = false;
            spanWeekly.InnerHtml = "<strong>No health checks completed this Week.</strong>";
        }
    }

    protected void sqlLastMonth_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            MonthlyChart.Visible = false;
            spanMonthly.InnerHtml = "<strong>No health checks completed this Month.</strong>";
        }
    }

    protected void sqlLastYear_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            YearlyChart.Visible = false;
            spanYearly.InnerHtml = "<strong>No health checks completed this Year.</strong>";
        }
    }
}
