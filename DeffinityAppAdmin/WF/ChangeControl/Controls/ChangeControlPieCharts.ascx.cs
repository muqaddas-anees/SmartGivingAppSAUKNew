using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Collections;
//using Infragistics.UltraChart.Resources.Appearance;
//using Infragistics.UltraChart.Shared.Styles;
using Deffinity.DAL;
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






public partial class controls_ServiceDeskCharts : System.Web.UI.UserControl
{
    Hashtable main = new Hashtable();
    private int numericColumns;
    Hashtable current = new Hashtable();
    Hashtable LastMonth = new Hashtable();
    Hashtable legend = new Hashtable();
    int key = 0;
    int key2 = 0;
    Hashtable ht = new Hashtable();//For last before one month
    Hashtable ht1 = new Hashtable();//Last month
    Hashtable ht2 = new Hashtable();//Yearly
    

    System.Drawing.Color[] disColor = new Color[40];
    System.Drawing.Color[] YearlyColor = new Color[40];
    System.Drawing.Color[] MonthlyColor = new Color[40];

    string _incidentType = string.Empty;
    private DataTable _dt;




    protected void Page_Load(object sender, EventArgs e)
    {
        DataDictionary();
            BindCharts();
       
       
    }
    private void DataDictionary()
    {
        lblCategory.Text = DataDictionaryBAL.DataDictionary_GetName("Category");
    }
    public void BindCharts()
    {
        try{
        Hid_incidenttype.Value = Incidentype;
        //if (!IsPostBack)
        //{
        //    ddlmastercategory.DataBind();
        //}
        //if (Hid_incidenttype.Value == "Fault")
        //{
        //    if (ddlmastercategory.Items.FindByText("Fault") != null)
        //    {
        //        ddlmastercategory.SelectedIndex = ddlmastercategory.Items.IndexOf(ddlmastercategory.Items.FindByText("Fault"));
        //    }
        //}
        //else 
        //{
        //    if (ddlmastercategory.Items.Count >1)
        //    {
        //        ddlmastercategory.SelectedIndex = 1;
        //    }
        //}
       
        CreateHashTable();

        //WeeklyChart.TitleTop.Text = "Completed " + Hid_incidenttype.Value.ToLower() + "s for last month";
        //MonthlyChart.TitleTop.Text = "Completed " + Hid_incidenttype.Value.ToLower() + "s for current month";
        //YearlyChart.TitleTop.Text = "Completed " + Hid_incidenttype.Value.ToLower() + "s this Year";
         }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    //#region set colors

    //private void SetColors(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1)
    //{
    //   // LegendItemType lt = Infragistics.UltraChart.Shared.Styles.LegendItemType.Point;
    //    UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
    //    System.Drawing.Color[] chartColor;
    //    chartColor = new System.Drawing.Color[]{
    //        Color.Red,Color.DarkGreen,Color.Orchid,Color.YellowGreen,Color.DeepSkyBlue,
    //        Color.DarkBlue,Color.DarkGoldenrod,Color.Purple,Color.DarkCyan,Color.Tan,
    //        Color.Black,Color.DarkViolet,Color.SeaShell,Color.DarkSalmon,Color.SaddleBrown,
    //        Color.Lavender,Color.LemonChiffon,Color.RosyBrown,Color.Silver,Color.PapayaWhip,
    //        Color.Coral,Color.Cyan,Color.Maroon,Color.BlanchedAlmond,Color.White,Color.LimeGreen
    //    };
        
    //    UltraChart1.ColorModel.CustomPalette = chartColor;
       
    //}

    //private void SetColorsColumn(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1)
    //{
    //    //UltraChart1.Override.Enabled = true;
    //    //Override o = new Override();
    //    //o.Column = 1;
    //    //o.Row = 1;
    //    //o.PE = new PaintElement(Color.Red);
    //    //UltraChart1.Override.Add(o);
      
    //}

    //#endregion
    #region Propertys
    public string Incidentype
    {
        get { return _incidentType; }
        set { _incidentType = value; }

    }
    #endregion 

    protected void sqlLastWeek_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            WeeklyChart.Visible = false;
            spanWeekly.InnerHtml = "<strong>No Change Controls completed for last month</strong>";
        }
    }

    protected void sqlLastMonth_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            MonthlyChart.Visible = false;
            spanMonthly.InnerHtml = "<strong>No Change Controls completed for current month</strong>";
        }
    }

    protected void sqlLastYear_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            YearlyChart.Visible = false;
            spanYearly.InnerHtml = "<strong>No Change Controls completed this Year</strong>";
        }
    }
  

   

    private void CreateHashTable()
    {

        DataTable dt = DataHelperClass.Category_SelectAll(_incidentType, int.Parse(string.IsNullOrEmpty(ddlmastercategory.SelectedValue) ? "0" : ddlmastercategory.SelectedValue));
        int cnt = dt.Rows.Count;
        System.Drawing.Color[] chartColor;
        chartColor = new System.Drawing.Color[]{
            
            //53 different color's

            Color.Orange,Color.Orchid,Color.Pink,Color.Green,Color.Lime,Color.Red,Color.Blue,
            Color.Yellow,Color.DarkMagenta,Color.DarkOrange,Color.DarkOrchid,Color.DarkRed,Color.DarkSalmon,
            Color.AliceBlue,Color.AntiqueWhite,Color.Aqua,Color.Aquamarine,Color.Azure,Color.Beige,Color.BlanchedAlmond,
            Color.BlueViolet,Color.Brown,Color.BurlyWood,Color.Cyan,Color.DarkBlue,Color.DarkCyan,Color.DarkGray,
            Color.DarkGreen,Color.DarkKhaki,Color.DarkSeaGreen,Color.DarkSlateBlue,Color.DarkSlateGray,
            Color.DarkTurquoise,Color.DarkViolet,Color.DeepPink,Color.DeepSkyBlue,Color.GreenYellow,
            Color.HotPink,Color.IndianRed,Color.LimeGreen,Color.Maroon,Color.OrangeRed,Color.RosyBrown,
            Color.RoyalBlue,Color.SaddleBrown,Color.Salmon,Color.SandyBrown,Color.SeaGreen,Color.SteelBlue,
            Color.Tan,Color.Tomato,Color.Violet
            

        };

       

        for (int ct = 0; ct < dt.Rows.Count; ct++)
        {
            try
            {
                main.Add(dt.Rows[ct]["Category"].ToString(), chartColor[ct]);
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
           // disColor[ct] = chartColor[ct];
        }
    }
   
    //private int getNumberColumns(DataTable dt)
    //{
    //    int count = 0;
    //    foreach (DataColumn dc in dt.Columns)
    //    {
    //        //checks the DataType.Name property to check for integers and single floats can add addition checks
    //        //for other numeric types or you can use another method to check for the amount of numeric columns
    //        if (dc.DataType.Name.StartsWith("Int") | dc.DataType.Name.StartsWith("Single") | dc.DataType.Name.StartsWith("String"))
    //        {
    //            count++;
    //        }
    //    }
    //    return count;
    //}
    private int getNumberColumns(DataTable dt)
    {
        int count = 0;
        foreach (DataRow dc in dt.Rows)
        {
            //checks the DataType.Name property to check for integers and single floats can add addition checks
            //for other numeric types or you can use another method to check for the amount of numeric columns

            count++;

        }
        return count;
    }

    protected void WeeklyChart_ChartDrawItem(object sender, Infragistics.UltraChart.Shared.Events.ChartDrawItemEventArgs e)
    {
        Infragistics.UltraChart.Core.Primitives.Arc cs =
           e.Primitive as Infragistics.UltraChart.Core.Primitives.Arc;

        Infragistics.UltraChart.Core.Primitives.Box box =
            e.Primitive as Infragistics.UltraChart.Core.Primitives.Box;


          if (cs != null && cs.DataPoint != null)
            {

                foreach (DictionaryEntry entry in main)
                {
                    if (cs.DataPoint.Label == entry.Key.ToString())
                    {

                        cs.PE = new PaintElement((System.Drawing.Color)entry.Value);
                       // ht.Add(e.Primitive.Row, main[cs.DataPoint.Label]);
                       disColor[e.Primitive.Row] = (System.Drawing.Color)main[cs.DataPoint.Label];
                        
                    }
                    if (cs.DataPoint.Label == "")
                    {
                        cs.PE = new PaintElement(Color.Olive);
                        disColor[e.Primitive.Row] = Color.Olive; 
                    }


                }

           
        }

            //if (box != null && e.Primitive.Path.ToLower().IndexOf("legend") != -1)
            //{
            //    foreach (DictionaryEntry entry in ht)
            //    {
            //        if (box.Row == Convert.ToInt32(entry.Key))
            //        {
            //            box.PE = new PaintElement((System.Drawing.Color)entry.Value);

            //        }

            //    }
            //}

            if (e.Primitive is Infragistics.UltraChart.Core.Primitives.Box)
            {
              if (e.HasData)
                {
                    e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;
                   
                    if (e.Primitive.Path == "Border.Title.Legend")
                    {
                       
                        e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;
                        e.Primitive.PE.Fill = this.disColor[System.Math.Abs(e.Primitive.Row)];
                    }
                }
            }


    }
    protected void MonthlyChart_ChartDrawItem(object sender, Infragistics.UltraChart.Shared.Events.ChartDrawItemEventArgs e)
    {

        Infragistics.UltraChart.Core.Primitives.Arc cs =
         e.Primitive as Infragistics.UltraChart.Core.Primitives.Arc;

        Infragistics.UltraChart.Core.Primitives.Box box =
            e.Primitive as Infragistics.UltraChart.Core.Primitives.Box;




        if (cs != null && cs.DataPoint != null)
        {

            foreach (DictionaryEntry entry in main)
            {
                if (cs.DataPoint.Label == entry.Key.ToString())
                {

                    cs.PE = new PaintElement((System.Drawing.Color)entry.Value);

                    MonthlyColor[e.Primitive.Row] = (System.Drawing.Color)main[cs.DataPoint.Label];
                   
                }
                if (cs.DataPoint.Label == "")
                {
                    cs.PE = new PaintElement(Color.Olive);

                    MonthlyColor[e.Primitive.Row] = Color.Olive;

                }

            }
        }

        if (e.Primitive is Infragistics.UltraChart.Core.Primitives.Box)
        {


            if (e.HasData)
            {
                e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;

                if (e.Primitive.Path == "Border.Title.Legend")
                {

                    e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;
                    e.Primitive.PE.Fill = this.MonthlyColor[System.Math.Abs(e.Primitive.Row)];
                }
            }
        }
    }
    protected void YearlyChart_ChartDrawItem(object sender, Infragistics.UltraChart.Shared.Events.ChartDrawItemEventArgs e)
    {

        Infragistics.UltraChart.Core.Primitives.Arc cs =
         e.Primitive as Infragistics.UltraChart.Core.Primitives.Arc;

        Infragistics.UltraChart.Core.Primitives.Box box =
            e.Primitive as Infragistics.UltraChart.Core.Primitives.Box;




        if (cs != null && cs.DataPoint != null)
        {

            foreach (DictionaryEntry entry in main)
            {
                if (cs.DataPoint.Label == entry.Key.ToString())
                {

                    cs.PE = new PaintElement((System.Drawing.Color)entry.Value);
                   
                    YearlyColor[e.Primitive.Row] = (System.Drawing.Color)main[cs.DataPoint.Label];
                    key2++;
                }
                if (cs.DataPoint.Label == "")
                {
                    cs.PE = new PaintElement(Color.Olive);
                   
                    YearlyColor[e.Primitive.Row] = Color.Olive;
                   
                }
                
            }
        }


        if (e.Primitive is Infragistics.UltraChart.Core.Primitives.Box)
        {
           

            if (e.HasData)
            {
                e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;
               
                if (e.Primitive.Path == "Border.Title.Legend")
                {
                   
                    e.Primitive.PE.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.None;
                    e.Primitive.PE.Fill = this.YearlyColor[System.Math.Abs(e.Primitive.Row)];
                }
            }
        }
    }
    protected void ddlmastercategory_DataBound(object sender, EventArgs e)
    {
        //if (ddlmastercategory.Items.Count > 0)
        //{
        //    ddlmastercategory.Items.Remove(new ListItem(" Please Select...","0"));
        //    //foreach (ListItem li in LiteDropDownList.Items)
        //    //{
        //    //    li.Text = Server.HTMLDecode(li.Text);
        //    //}
        //}

    }

    //#region Binding Charts

    //private void bindChart1()
    //{ 
    //    WeeklyChart.
    //}
    //private void bindChart2()
    //{

    //}
    //private void bindChart3()
    //{

    //}

    //#endregion
}
