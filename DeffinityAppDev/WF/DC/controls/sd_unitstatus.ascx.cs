using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using IncidentMgt.DAL;
using IncidentMgt.Entity;
using System.Collections.Generic;
//using Infragistics.UltraGauge.Resources;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Data.Linq.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;


public partial class Servicedesk_sdcontrols_sd_unitstatus : System.Web.DynamicData.FieldTemplateUserControl {
    IncidentDataContext obj = new IncidentDataContext();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadPieChart();
                LoadBarchart();
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void LoadBarchart()
    {
        try
        {
            lblmsb.Text = string.Empty;
            //int SdId = QueryStringValues.SDID;
            int customerid = sessionKeys.PortfolioID;
            var PurchaseHistoryCount = (from p in obj.Incident_UnitsPurchaseHistories
                                        where p.PortfolioID == customerid
                                        select p).Count();

            //var result = (from p in obj.Incident_UnitsPurchaseHistories
            //              where p.PortfolioID == customerid
            //              group p by p.DatePurchased into s
            //              select new
            //              {
            //                  UnitsPurchased = s.Sum(i => i.UnitsPurchased),
            //                  DatePurchased = s.Key
            //              }).ToList();

            var result = obj.Incident_UnitsPurchaseHistory_BarchartXYvalue(DateTime.Parse("01/01/1900"), DateTime.Parse("01/01/1900"),customerid).ToList();


            if (PurchaseHistoryCount == 0)
            {
                Chart2.Visible = false;
                pnlsearch.Visible = false;
                lblmsb.Text = "No Charts Available";
                
            }
            else
            {
                pnlsearch.Visible = true;
                Chart2.DataSource = result;
                Chart2.Height = 400;
                Chart2.Width = 800;
                Chart2.Series["MaxTemp"].XValueType = ChartValueType.DateTime;
                Chart2.Series["MaxTemp"].XValueMember = "DatePurchased";
                Chart2.Series["MaxTemp"].YValueMembers = "UnitsPurchased";
                Chart2.Series["MaxTemp"].IsValueShownAsLabel = true;
                Chart2.Series["MinTemp"].IsValueShownAsLabel = true;
                Chart2.Series["MaxTemp"]["PointWidth"] = (0.6).ToString();
                Chart2.Series["MinTemp"]["PointWidth"] = (0.6).ToString();
                //Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;


                Chart2.Series["MinTemp"].XValueType = ChartValueType.DateTime;
                Chart2.Series["MinTemp"].XValueMember = "DatePurchased";
                Chart2.Series["MinTemp"].YValueMembers = "QtyUsed";
                Chart2.Titles.Add("Volume of Units Consumed");
                Chart2.Titles[0].Font = new Font("Vardana", 12, System.Drawing.FontStyle.Bold);
                Chart2.Titles[0].ForeColor = System.Drawing.Color.Gray;
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Date Range";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Volume of Units Consumed";
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MinorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Black;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Vardana", 8, System.Drawing.FontStyle.Regular);
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Vardana", 8, System.Drawing.FontStyle.Regular);
                Chart2.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }

    private void LoadPieChart()
    {

        IncidentDataContext obj = new IncidentDataContext();
        //int SdId = QueryStringValues.SDID;
        int customerid = sessionKeys.PortfolioID;
        var PurchaseHistoryCount = (from p in obj.Incident_UnitsPurchaseHistories
                                    where p.PortfolioID == customerid
                                    select p).Count();

        if (PurchaseHistoryCount == 0)
        {
            Chart1.Visible = false;
        }
        else
        {

            var Green = (from p in obj.Incident_UnitsPurchaseHistories
                         where p.PortfolioID == customerid && p.AgeRAGStatus == "Green"
                         group p by new { p.PortfolioID, p.AgeRAGStatus } into s 
                         select new
                         {
                             greenau = s.Sum(i => i.UnitsPurchased == null ? 0 : i.UnitsPurchased) - s.Sum(i => i.QtyUsed == null ? 0 : i.QtyUsed)
                         }).FirstOrDefault(); 
            

            var Amber = (from p in obj.Incident_UnitsPurchaseHistories
                         where p.PortfolioID == customerid && p.AgeRAGStatus == "Amber"
                         group p by new { p.PortfolioID, p.AgeRAGStatus } into s
                         select new
                         {
                            amberau = s.Sum(i => i.UnitsPurchased == null ? 0 : i.UnitsPurchased) - s.Sum(i => i.QtyUsed == null ? 0 : i.QtyUsed)
                         }).FirstOrDefault();

            var Red = (from p in obj.Incident_UnitsPurchaseHistories
                       where p.PortfolioID == customerid && p.AgeRAGStatus == "Red" && p.ExpiryDate >= DateTime.Now
                       group p by new { p.PortfolioID, p.AgeRAGStatus } into s
                       select new
                       {
                           redau = s.Sum(i => i.UnitsPurchased == null ? 0 : i.UnitsPurchased) - s.Sum(i => i.QtyUsed == null ? 0 : i.QtyUsed)
                       }).FirstOrDefault();

            decimal total = (decimal)(Green == null ? 0 : Green.greenau) + (decimal)(Amber == null ? 0 : Amber.amberau) + (decimal)(Red == null ? 0 : Red.redau);



            decimal[] yValues = { (decimal)(Green==null? 0: Green.greenau), (decimal)(Amber==null?0:Amber.amberau),(decimal)(Red==null?0: Red.redau)};
                Chart1.Series["Series1"].Points.DataBindY(yValues);

                Chart1.Series["Series1"].Points[0].Color = Color.Green;
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                Chart1.Series["Series1"].Points[0].LegendText = "Current";
            Chart1.Series["Series1"].Points[0].XValue = Convert.ToDouble(Green==null? 0: Green.greenau);
                Chart1.Series["Series1"].Points[1].Color = Color.Yellow;
                Chart1.Series["Series1"].Points[1].LegendText = "Expiring Soon";
            Chart1.Series["Series1"].Points[1].XValue = Convert.ToDouble(Amber==null?0: Amber.amberau);
                Chart1.Series["Series1"].Points[2].Color = Color.Red;
                Chart1.Series["Series1"].Points[2].LegendText = "Expiring Imminently";
            Chart1.Series["Series1"].Points[2].XValue = Convert.ToDouble(Red==null?0: Red.redau);
                Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
                Chart1.Titles.Add("Available Unit Status");
                Chart1.Titles[0].Font = new Font("Vardana", 12, System.Drawing.FontStyle.Bold);
                Chart1.Titles[0].ForeColor = System.Drawing.Color.Gray;
                Chart1.Legends[0].Enabled = true;

                if (total == 0)
                {
                    Chart1.Visible = false;
                    lblmsg.Text = "No Available units";
                }

            
            
        }
    }


    private void LoadGrid()
    {
        try
        {
            int SdId = QueryStringValues.SDID;
            int customerid=sessionKeys.PortfolioID;

            //var result = (from p in obj.Incident_UnitsPurchaseHistories
            //               join c in obj.Incident_UnitCategories on p.UnitCategory equals c.ID
            //                where p.PortfolioID == customerid
            //                 select new { p, UnitCategoryName = c.Name }).ToList();


            List<Incident_UnitsPurchaseHistory_SelectByCustomerIDResult> result = obj.Incident_UnitsPurchaseHistory_SelectByCustomerID(customerid).ToList();
            Incident_UnitsPurchaseHistory_SelectByCustomerIDResult iunit = new Incident_UnitsPurchaseHistory_SelectByCustomerIDResult();
            iunit.ID = 0;
            result.Add(iunit);
            GridView1.DataSource = result;
            GridView1.DataBind();
            
        }
        catch (Exception Ex)
        {
            LogExceptions.WriteExceptionLog(Ex);
        }

        
    }

  
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        LoadGrid();
        LoadPieChart();
        LoadBarchart();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Update2")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                using (IncidentDataContext su = new IncidentDataContext())
                {
                    int i = GridView1.EditIndex;
                    GridViewRow Row = GridView1.Rows[i];

                    string date=((TextBox)Row.FindControl("txtDatePurchased")).Text;
                    DateTime DatePurchased = DateTime.Parse(string.IsNullOrEmpty(date) ? "01/01/1900" : date);

                    string unit = ((TextBox)Row.FindControl("txtUnitPurchased")).Text;
                    decimal UnitsPurchased = decimal.Parse(string.IsNullOrEmpty(unit) ? "0" : unit);
                    
                    string UnitCategory = ((DropDownList)Row.FindControl("ddlUnitCategory")).SelectedValue;
                    
                    string PuchaseOrderNo = ((TextBox)Row.FindControl("txtPurchaseOrderNo")).Text;
                    
                    string expdate=((TextBox)Row.FindControl("txtExpiryDate")).Text;
                    DateTime ExpiryDate = DateTime.Parse(string.IsNullOrEmpty(expdate) ? "01/01/1900" : expdate); 
                    
                    string AgeRAGStatus = ((DropDownList)Row.FindControl("ddlAgeRAGStatus")).SelectedItem.Text;
                   
                    //string qty=((TextBox)Row.FindControl("txtQtyWasted")).Text;
                    //decimal QtyWAsted = decimal.Parse(string.IsNullOrEmpty(qty) ? "0" : qty);
                    
                    Incident_UnitsPurchaseHistory up = su.Incident_UnitsPurchaseHistories.Where(p => p.ID == id).FirstOrDefault();

                    up.DatePurchased = DatePurchased;
                    up.UnitsPurchased = UnitsPurchased;
                    up.UnitCategory = int.Parse(UnitCategory);
                    up.PurchaseOrderNo = PuchaseOrderNo;
                    up.ExpiryDate = ExpiryDate;
                    up.AgeRAGStatus = AgeRAGStatus;
                    //up.QtyWasted = QtyWAsted;
                    su.SubmitChanges();

                }

                GridView1.EditIndex = -1;
                LoadGrid();
                LoadPieChart();
                LoadBarchart();
            }
            if (e.CommandName == "Delete2")
            {
                using (IncidentDataContext obj = new IncidentDataContext())
                {
                    string id = e.CommandArgument.ToString();
                    var UnitpurchasehistoryDelete = from p in obj.Incident_UnitsPurchaseHistories
                                                    where p.ID == int.Parse(id)
                                                    select p;

                    obj.Incident_UnitsPurchaseHistories.DeleteAllOnSubmit(UnitpurchasehistoryDelete);
                    obj.SubmitChanges();

                }

                //obj.Incident_UnitsPurchaseHistory_DeleteByID(int.Parse(id));
                GridView1.EditIndex = -1;
                LoadGrid();
                LoadPieChart();
                LoadBarchart();

            }

            if (e.CommandName == "Add")
            {
                //int SdID = QueryStringValues.SDID;
                int customerid = sessionKeys.PortfolioID;

                string  Date = ((TextBox)GridView1.FooterRow.FindControl("txtDatePurchased")).Text.Trim();
                DateTime DatePurchased = DateTime.Parse(string.IsNullOrEmpty(Date) ? "01/01/1900" : Date);

               
                string unit = ((TextBox)GridView1.FooterRow.FindControl("txtUnitPurchased")).Text.Trim();
                decimal UnitPurchased = decimal.Parse(string.IsNullOrEmpty(unit) ? "0" : unit);
                
                
                int UnitCategory = Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddlUnitCategory")).SelectedValue);
                
                string PurchaseOrderNo = ((TextBox)GridView1.FooterRow.FindControl("txtPurchaseOrderNo")).Text.Trim();
                
                string expdate = ((TextBox)GridView1.FooterRow.FindControl("txtExpiryDate")).Text.Trim();
                DateTime ExpiryDate = DateTime.Parse(string.IsNullOrEmpty(expdate) ? "01/01/1900" : expdate);
                
                string AgeRAGStatus =((DropDownList)GridView1.FooterRow.FindControl("ddlAgeRAGStatus")).SelectedItem.Text.Trim();
                
                //string qty  = ((TextBox)GridView1.FooterRow.FindControl("txtQtyWasted")).Text.Trim();
                //decimal QtyWasted = decimal.Parse(string.IsNullOrEmpty(qty) ? "0" : qty);
               
                Incident_UnitsPurchaseHistory iup = new Incident_UnitsPurchaseHistory
                {
                    PortfolioID=customerid,
                    DatePurchased = DatePurchased,
                    UnitsPurchased=UnitPurchased,
                    UnitCategory=UnitCategory,
                    PurchaseOrderNo=PurchaseOrderNo,
                    ExpiryDate=ExpiryDate,
                    AgeRAGStatus=AgeRAGStatus,
                    QtyWasted=0
                };
                obj.Incident_UnitsPurchaseHistories.InsertOnSubmit(iup);
                obj.SubmitChanges();
                //obj.Incident_UnitsPurchaseHistory_Insert(SdID, DatePurchased, UnitPurchased, UnitCategory, PurchaseOrderNo, ExpiryDate, AgeRAGStatus, QtyWasted);
                LoadGrid();
                LoadPieChart();
                LoadBarchart();


            }
        }

        catch (Exception Ex)
        {
            LogExceptions.WriteExceptionLog(Ex);
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        LoadGrid();
        LoadPieChart();
        LoadBarchart();


    }

    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Incident_UnitsPurchaseHistory_SelectByCustomerIDResult de = (Incident_UnitsPurchaseHistory_SelectByCustomerIDResult)e.Row.DataItem;

            var result = from p in obj.Incident_UnitCategories
                         select p;
            int customerid = sessionKeys.PortfolioID;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("lblDatePurchased") != null)
                {
                    Label lbldatepurchased = e.Row.FindControl("lblDatePurchased") as Label;
                    if (lbldatepurchased.Text == "01/01/1900")
                    {
                        lbldatepurchased.Text = string.Empty;
                    }
                }
                if (e.Row.FindControl("lblExpiryDate") != null)
                {
                    Label lblexpirydate = e.Row.FindControl("lblExpiryDate") as Label;
                    if (lblexpirydate.Text == "01/01/1900")
                    {
                        lblexpirydate.Text = string.Empty;
                    }
                }

                if (e.Row.FindControl("txtDatePurchased") != null)
                {
                    TextBox txtDatepurchased = e.Row.FindControl("txtDatePurchased") as TextBox;
                    if (txtDatepurchased.Text == "01/01/1900")
                    {
                        txtDatepurchased.Text = string.Empty;
                    }
                }
                if (e.Row.FindControl("txtExpiryDate") != null)
                {
                    TextBox txtExpiryDate = e.Row.FindControl("txtExpiryDate") as TextBox;
                    if (txtExpiryDate.Text == "01/01/1900")
                    {
                        txtExpiryDate.Text = string.Empty;
                    }
                }

                Label lblqu = e.Row.FindControl("lblQtyUsed") as Label;
                if (lblqu.Text == string.Empty)
                {
                    lblqu.Text = "0.00";
                }




               



                if (de.ID == 0)
                {
                    e.Row.Visible = false;
                }
                else
                {
                    if (e.Row.FindControl("HID") != null)
                    {
                        HiddenField hid = e.Row.FindControl("HID") as HiddenField;
                        Label lblID = e.Row.FindControl("lblID") as Label;
                        Label lbldatepurchased = e.Row.FindControl("lblDatePurchased") as Label;
                        Label lblexpirydate = e.Row.FindControl("lblExpiryDate") as Label;

                        DateTime fromdate = DateTime.Parse(lbldatepurchased.Text);
                        DateTime todate = DateTime.Parse(lblexpirydate.Text);

                        double totaldays = SqlMethods.DateDiffDay(fromdate, todate);
                        double remainigdays = SqlMethods.DateDiffDay(DateTime.Now, todate);
                        if (hid.Value == "Red")
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("Image1")).SkinID = "Red_circle";
                        }
                        else if (hid.Value == "Green")
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("Image1")).SkinID = "Green_circle";
                        }
                        else if (hid.Value == "Amber")
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("Image1")).SkinID = "Amber_circle";
                        }
                        else
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("Image1")).SkinID = "Green_circle";
                        }
                    }




                }

                if (e.Row.FindControl("ddlUnitCategory") != null)
                {

                    DropDownList ddlmaster = e.Row.FindControl("ddlUnitCategory") as DropDownList;
                    ddlmaster.DataSource = result;
                    ddlmaster.DataValueField = "ID";
                    ddlmaster.DataTextField = "Name";
                    ddlmaster.DataBind();
                    ddlmaster.SelectedValue = de.UnitCategory.Value.ToString();

                }

                if (e.Row.FindControl("ddlAgeRAGStatus") != null)
                {
                    DropDownList ddlAgeRAG = e.Row.FindControl("ddlAgeRAGStatus") as DropDownList;

                    ddlAgeRAG.SelectedValue = de.AgeRAGStatus;
                }
            }



            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (e.Row.FindControl("ddlUnitCategory") != null)
                {

                    DropDownList ddlmaster = e.Row.FindControl("ddlUnitCategory") as DropDownList;

                    ddlmaster.DataSource = result;
                    ddlmaster.DataValueField = "ID";
                    ddlmaster.DataTextField = "Name";
                    ddlmaster.DataBind();
                    ddlmaster.Items.Insert(0, new ListItem("Please Select...", "0"));
                }


            }


        }
        catch (Exception Ex)
        {
            LogExceptions.WriteExceptionLog(Ex);
        }

        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadGrid();
        LoadPieChart();
        LoadBarchart();
    }


    protected void ddlUnitCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            TextBox txt = (TextBox)GridView1.FooterRow.FindControl("txtExpiryDate");

            DropDownList ddl = GridView1.FooterRow.FindControl("ddlUnitCategory") as DropDownList;
            TextBox txtdatepurchase = (TextBox)GridView1.FooterRow.FindControl("txtDatePurchased");

            int customerid = sessionKeys.PortfolioID;
            var getpriodtypeformonthly = (from p in obj.Incident_UnitExpiryPeriods
                                          where p.PortfolioID == customerid && p.CategoryID == 1
                                          select p.PeriodType).SingleOrDefault();
            var getperiodformonthly = (from p in obj.Incident_UnitExpiryPeriods
                                       where p.PortfolioID == customerid && p.CategoryID == 1
                                       select p.Period).SingleOrDefault();

            var getpriodtypeforprepaid = (from p in obj.Incident_UnitExpiryPeriods
                                          where p.PortfolioID == customerid && p.CategoryID == 2
                                          select p.PeriodType).SingleOrDefault();
            var getperiodforprepaid= (from p in obj.Incident_UnitExpiryPeriods
                                       where p.PortfolioID == customerid && p.CategoryID == 2
                                       select p.Period).SingleOrDefault();
            var getpriodtypeforcredit = (from p in obj.Incident_UnitExpiryPeriods
                                          where p.PortfolioID == customerid && p.CategoryID == 3
                                          select p.PeriodType).SingleOrDefault();
            var getperiodforcredit= (from p in obj.Incident_UnitExpiryPeriods
                                       where p.PortfolioID == customerid && p.CategoryID == 3
                                       select p.Period).SingleOrDefault();




            if (ddl.SelectedItem.Text == "Monthly")
            {

                if (txtdatepurchase.Text == string.Empty)
                {
                    txt.Text = string.Empty;
                }
                else
                {
                    if (getpriodtypeformonthly == "Month")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddMonths(Convert.ToInt32(getperiodformonthly)));
                    }
                    if (getpriodtypeformonthly == "Days")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddDays(Convert.ToInt32(getperiodformonthly)));
                    }
                    if (getpriodtypeformonthly == null)
                    {
                        txt.Text = string.Empty;
                    }

                    
                }

            }
            if (ddl.SelectedItem.Text == "Pre-Paid")
            {

                if (txtdatepurchase.Text == string.Empty)
                {
                    txt.Text = string.Empty;
                }
                else
                {
                    if (getpriodtypeforprepaid == "Month")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddMonths(Convert.ToInt32(getperiodforprepaid)));
                    }
                                       
                    if (getpriodtypeforprepaid == "Days")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddDays(Convert.ToInt32(getperiodforprepaid)));
                    }
                    if (getpriodtypeforprepaid == null)
                    {
                        txt.Text = string.Empty;
                    }
                   


                }

            }
            if (ddl.SelectedItem.Text == "Credit")
            {

                if (txtdatepurchase.Text == string.Empty)
                {
                    txt.Text = string.Empty;
                }
                else
                {
                    if (getpriodtypeforcredit == "Month")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddMonths(Convert.ToInt32(getperiodforcredit)));
                    }
                    if (getpriodtypeforcredit == "Days")
                    {
                        DateTime dates = DateTime.Parse(txtdatepurchase.Text);
                        txt.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dates.AddDays(Convert.ToInt32(getperiodforcredit)));
                    }
                    if (getpriodtypeforcredit ==null)
                    {
                        txt.Text = string.Empty;
                    }


                }

            }
            //else
            //{
            //    txt.Text = string.Empty;
            //}
            LoadBarchart();
            LoadPieChart();
        }
        catch (Exception ex)
        { 
            LogExceptions.WriteExceptionLog(ex);
        }
       
 
    }
   
    


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //int SdId = QueryStringValues.SDID;
            int customerid = sessionKeys.PortfolioID;
            //var result = obj.Incident_UnitsPurchaseHistories.Where(p => p.SdID == SdId).ToList();
            string fromdate = string.IsNullOrEmpty(txtFromaDate.Text) ? "01/01/1900" : txtFromaDate.Text;
            string todate = string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text;
            //var result = (from p in obj.Incident_UnitsPurchaseHistories
            //              where p.PortfolioID == customerid && p.DatePurchased >= DateTime.Parse(fromdate) && p.DatePurchased <= DateTime.Parse(todate)
            //              group p by p.DatePurchased into s
            //              select new
            //              {
            //                  UnitsPurchased = s.Sum(i => i.UnitsPurchased),
            //                  DatePurchased = string.Format(Deffinity.systemdefaults.GetStringDateformat(), s.Key)
            //              }).ToList();

            var result = obj.Incident_UnitsPurchaseHistory_BarchartXYvalue(DateTime.Parse(fromdate), DateTime.Parse(todate), customerid).ToList();

            if (result.Count == 0)
            {
                lblmsb.Text = "No Charts Available";
                Chart2.Visible = false;
            }
            else
            {
                lblmsb.Text = string.Empty;
                Chart2.Height = 400;
                Chart2.Width = 1000;
                Chart2.BorderlineWidth = 0;
                Chart2.DataSource = result;
                Chart2.Series["MaxTemp"].XValueType = ChartValueType.DateTime;
                Chart2.Series["MaxTemp"].XValueMember = "DatePurchased";
                Chart2.Series["MaxTemp"].YValueMembers = "UnitsPurchased";
                Chart2.Series["MaxTemp"].IsValueShownAsLabel = true;
                Chart2.Series["MinTemp"].IsValueShownAsLabel = true;
                Chart2.Series["MaxTemp"]["PointWidth"] = (0.6).ToString();
                Chart2.Series["MinTemp"]["PointWidth"] = (0.6).ToString();
                //Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;


                Chart2.Series["MinTemp"].XValueType = ChartValueType.DateTime;
                Chart2.Series["MinTemp"].XValueMember = "DatePurchased";
                Chart2.Series["MinTemp"].YValueMembers = "QtyUsed";
                if ((fromdate == "01/01/1900") && (todate == "01/01/1900"))
                {
                    Chart2.Titles.Add("Volume of Units Consumed");
                }
                else
                {
                    Chart2.Titles.Add("Volume of Units Consumed from   " + txtFromaDate.Text + "   To  " + txtToDate.Text);
                }
                Chart2.Titles[0].Font = new Font("Vardana", 12, System.Drawing.FontStyle.Bold);
                Chart2.Titles[0].ForeColor = System.Drawing.Color.Gray;
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Date Range";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Volume of Units Consumed";
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MinorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorTickMark.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Black;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Vardana", 8, System.Drawing.FontStyle.Regular);
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Vardana", 8, System.Drawing.FontStyle.Regular);
                Chart2.ChartAreas["ChartArea1"].BorderWidth = 0;
                Chart2.DataBind();
                txtFromaDate.Text = string.Empty;
                txtToDate.Text = string.Empty;


            }
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        LoadGrid();
        LoadPieChart();
    
    }
}
