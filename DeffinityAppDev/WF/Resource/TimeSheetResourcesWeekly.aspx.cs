using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using AjaxControlToolkit;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
public partial class TimeSheetResourcesWeekly : System.Web.UI.Page
{
    TimeSheetDataContext assets = new TimeSheetDataContext();
    List<TimesheetEntry_WeeklyViewResult> twList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Resource";

            lblError.Visible = false;
            Session["NewRow"] = 10;
            Session["update1"] = Server.UrlEncode(System.DateTime.Now.ToString());
            DateTime dtMonday = GetFirstDayOfWeek(DateTime.Today);
            txtweekcommencedate.Text = dtMonday.ToShortDateString();
            BindGrid(txtweekcommencedate.Text,sessionKeys.UID,10);
            //Bind Vacation traker grid
            Bind_VTgrid(Convert.ToDateTime(txtweekcommencedate.Text),sessionKeys.UID, 0);
            viewButtonCode_Timeandexpenses();
        }
    }
    
    //public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
    //{
    //    CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
    //    return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
    //}
    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
    {
        DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        DateTime firstDayInWeek = dayInWeek.Date;
        while (firstDayInWeek.DayOfWeek != firstDay)
            firstDayInWeek = firstDayInWeek.AddDays(-1);

        return firstDayInWeek;
    }
    private void BindGrid(string wcdate,int contractorid, int row)
    {
        try
        {
        twList = (from r in assets.TimesheetEntry_WeeklyView(Convert.ToDateTime(wcdate),sessionKeys.UID, row)
                                                select r).ToList();
        grdTimeSheetViewWeekly.DataSource = twList;
        grdTimeSheetViewWeekly.DataBind();
        }
        catch (IndexOutOfRangeException ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public string ChangeHoures(string GetHours)
    {

        string GetActivity = "";
        try
        {
            char[] comm1 = { '.' };
            string[] displayTime = GetHours.Split(comm1);


            GetActivity = displayTime[0] + ":" + displayTime[1];


        }
       
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
        return GetActivity;
    }
    protected void ddlProjectTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    private void BindCustomersByProjectRef(DropDownList ddlCustomersGrid, int setVal, int projectRef)
    {
        //TimeSheetDataContext timeSheet = new TimeSheetDataContext();
        //PortfolioDataContext project = new PortfolioDataContext();
        

        //var getCustomers = from r in project.ProjectDetails
        //                   orderby r.ProjectTitle
        //                   select r;
        ddlCustomersGrid.DataSource = TimesheetresourceSection.GetProjectListByResource();
        ddlCustomersGrid.DataValueField = "ProjectReference";
        ddlCustomersGrid.DataTextField = "ProjectTitle";
        ddlCustomersGrid.DataBind();
        //ddlCustomersGrid.Items.Insert(0, new ListItem("Please select...", "0"));

        ddlCustomersGrid.SelectedValue = setVal.ToString();

    }

    private void BindEntryTypes(DropDownList ddlEntryTypes,int setVal)
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "TimeSheet_ListOfEntryTypes").Tables[0];
        ddlEntryTypes.DataSource = dt;
        ddlEntryTypes.DataTextField = "EntryType";
        ddlEntryTypes.DataValueField = "ID";
        ddlEntryTypes.DataBind();
       // ddlEntryTypes.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlEntryTypes.SelectedValue = setVal.ToString();
    }
    protected void grdTimeSheetViewWeekly_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            TimeSheetDataContext timeSheet = new TimeSheetDataContext();
            PortfolioDataContext project = new PortfolioDataContext();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TimesheetEntry_WeeklyViewResult row = (TimesheetEntry_WeeklyViewResult)e.Row.DataItem;
                if (row.custom == 2)
                {
                    e.Row.Visible = false;
                }
                if (e.Row.FindControl("ddlProjectTitle") != null)
                {
                    DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
                    DropDownList ddlProjectTask = (DropDownList)e.Row.FindControl("ddlProjectTask");
                    DropDownList ddlEntryType = (DropDownList)e.Row.FindControl("ddlEntryType");
                    DropDownList ddlSites = (DropDownList)e.Row.FindControl("ddlSites");

                    Label lblProjectRef = (Label)e.Row.FindControl("lblProjectRef");
                    Label lblProjectTask = (Label)e.Row.FindControl("lblProjectTask");
                    Label lblEntryType = (Label)e.Row.FindControl("lblEntryType");
                    Label lblSite = (Label)e.Row.FindControl("lblSite");

                    CascadingDropDown casCadProjectTask = (CascadingDropDown)e.Row.FindControl("casCadProjectTask");
                   // CascadingDropDown casCadEntryType = (CascadingDropDown)e.Row.FindControl("casCadEntryType");
                    CascadingDropDown casCadSitesGrid = (CascadingDropDown)e.Row.FindControl("casCadSitesGrid");


                    BindCustomersByProjectRef(ddlProjectTitle, int.Parse(lblProjectRef.Text), int.Parse(lblProjectRef.Text));
                    BindEntryTypes(ddlEntryType, int.Parse(lblEntryType.Text));
                    casCadProjectTask.SelectedValue = lblProjectTask.Text;
                    //casCadEntryType.SelectedValue = lblEntryType.Text;
                    casCadSitesGrid.SelectedValue = lblSite.Text;


                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TimesheetEntry_WeeklyViewResult row = (from r in twList
                                                       where r.custom == 2
                                                       select r).FirstOrDefault();
                if (row.custom == 2)
                {
                    e.Row.Visible = true;
                    TextBox txtHoursf1 = (TextBox)e.Row.FindControl("txtHoursf1");
                    TextBox txtHoursf2 = (TextBox)e.Row.FindControl("txtHoursf2");
                    TextBox txtHoursf3 = (TextBox)e.Row.FindControl("txtHoursf3");
                    TextBox txtHoursf4 = (TextBox)e.Row.FindControl("txtHoursf4");
                    TextBox txtHoursf5 = (TextBox)e.Row.FindControl("txtHoursf5");
                    TextBox txtHoursf6 = (TextBox)e.Row.FindControl("txtHoursf6");
                    TextBox txtHoursf7 = (TextBox)e.Row.FindControl("txtHoursf7");
                    TextBox txtHoursf8 = (TextBox)e.Row.FindControl("txtHoursf8");
                    txtHoursf1.Text = ChangeHoures(row.c0.ToString());
                    txtHoursf2.Text = ChangeHoures(row.c1.ToString());
                    txtHoursf3.Text = ChangeHoures(row.c2.ToString());
                    txtHoursf4.Text = ChangeHoures(row.c3.ToString());
                    txtHoursf5.Text = ChangeHoures(row.c4.ToString());
                    txtHoursf6.Text = ChangeHoures(row.c5.ToString());
                    txtHoursf7.Text = ChangeHoures(row.c6.ToString());
                    txtHoursf8.Text = ChangeHoures(row.ctotal.ToString());

                }

            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCell cell4 = e.Row.Cells[4];
                cell4.Text = "Mon<br>" + AddOrdinal(GetDates(0));

                TableCell cell5 = e.Row.Cells[5];
                cell5.Text = "Tue<br>"+AddOrdinal(GetDates(1));

                TableCell cell6 = e.Row.Cells[6];
                cell6.Text = "Wed<br>" + AddOrdinal(GetDates(2));

                TableCell cell7 = e.Row.Cells[7];
                cell7.Text = "Thu<br>" + AddOrdinal(GetDates(3));

                TableCell cell8 = e.Row.Cells[8];
                cell8.Text = "Fri<br>" + AddOrdinal(GetDates(4));

                TableCell cell9 = e.Row.Cells[9];
                cell9.Text = "Sat<br>" + AddOrdinal(GetDates(5));

                TableCell cell10 = e.Row.Cells[10];
                cell10.Text = "Sun<br>" + AddOrdinal(GetDates(6));
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["update"] = Session["update1"];
        // hiddenSession.Value = hidden;
    }
    private void UpdatePONumber(int projectRef, string PONumber)
    {

        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "PONumberUpdate_Timesheet",
            new SqlParameter("@ProjectReference", projectRef), new SqlParameter("@PONumber", PONumber));



    }

    protected void grdTimeSheetViewWeekly_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                lblError.Visible = false;

                lblstatus.Visible = false;
                int newRow = Convert.ToInt32(Session["NewRow"]);
                int Total = grdTimeSheetViewWeekly.Rows.Count-1 ;
                Total = Total + 1;
                Session["NewRow"] = Total;
                BindGrid(txtweekcommencedate.Text.Trim(),sessionKeys.UID,Total);
            }
            if (e.CommandName == "Update1")
            {
                if (ViewState["update"].ToString() == Session["update1"].ToString())
                {
                    DateTime eDate1 = Convert.ToDateTime(txtweekcommencedate.Text.Trim());
                    DateTime DateEntered;
                    TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();
                    projectTaskDataContext pro = new projectTaskDataContext();
                    Nullable<int> output = null;
                    int CountRow = grdTimeSheetViewWeekly.Rows.Count;
                    string Msg = "";
                    for (int i = 0; i < CountRow; i++)
                    {

                        GridViewRow Row = grdTimeSheetViewWeekly.Rows[i];

                        DropDownList ddlProjectTitle = (DropDownList)Row.FindControl("ddlProjectTitle");
                        DropDownList ddlProjectTask = (DropDownList)Row.FindControl("ddlProjectTask");
                        DropDownList ddlEntryType = (DropDownList)Row.FindControl("ddlEntryType");
                        DropDownList ddlSites = (DropDownList)Row.FindControl("ddlSites");
                        CascadingDropDown casCadProjectTask = (CascadingDropDown)Row.FindControl("casCadProjectTask");
                        //CascadingDropDown casCadEntryType = (CascadingDropDown)Row.FindControl("casCadEntryType");
                        CascadingDropDown casCadSitesGrid = (CascadingDropDown)Row.FindControl("casCadSitesGrid");
                        int projectRef = 0;
                        int projectTask = 0;
                        int entryType = 0;
                        int site = 0;
                        double hours = 0.0;
                        projectRef = int.Parse(string.IsNullOrEmpty(ddlProjectTitle.SelectedValue) ? "0" : ddlProjectTitle.SelectedValue);
                        projectTask = int.Parse(string.IsNullOrEmpty(ddlProjectTask.SelectedValue) ? "0" : ddlProjectTask.SelectedValue);
                        entryType = int.Parse(string.IsNullOrEmpty(ddlEntryType.SelectedValue) ? "0" : ddlEntryType.SelectedValue);
                        site = int.Parse(string.IsNullOrEmpty(ddlSites.SelectedValue) ? "0" : ddlSites.SelectedValue);
                        
                        for (int j = 0; j <=6; j++)
                        {
                            string txt = "txtHours" + (j).ToString();
                            string lbl = "lblid" + (j).ToString();
                            string val = ((TextBox)Row.FindControl(txt)).Text.Trim();
                            string id = ((Label)Row.FindControl(lbl)).Text.Trim();
                            string valid = CheckValidHours(val);
                            if (!string.IsNullOrEmpty(valid))
                            {
                                hours = GetHours_double(val);
                            }
                            DateEntered = eDate1.Date.AddDays(j);
                            
                            if ((int.Parse(ddlProjectTitle.SelectedValue) == 0))
                            {
                                lblstatus.Visible = true;
                                //lblstatus.Text = "Select project";
                            }
                            else
                            {
                                if (id != "0" && projectRef > 0 )
                                {
                                    try{
                                    TimeSheetEntry.DN_TimesheetEntryupdate(int.Parse(id), projectRef, "0", 0, sessionKeys.UID,
                                        entryType, DateEntered, hours, "",
                                        site, projectTask, ref output);


                                    var projectPO = (from r in pro.Projects
                                                     where r.ProjectReference == projectRef
                                                     select r).ToList().FirstOrDefault();
                                    if (projectPO != null)
                                    {
                                        UpdatePONumber(projectRef, projectPO.CustomerReference.ToString());
                                    }

                                    if (output == 10)
                                    {
                                        lblstatus.Visible = true;
                                        //lblstatus.Text = "You have exceeded 24 hours for the date entered";
                                    }
                                    else if (output == 0)
                                    {
                                        lblstatus.Visible = true;
                                        //lblstatus.Text = "Error While Timesheet Updation";
                                    }
                                    else if (output == 1)
                                    {
                                        lblstatus.Visible = true;
                                        //lblstatus.Text = "Timesheet Updated  Successfully ";
                                        //lblstatus.ForeColor = System.Drawing.Color.Green;
                                    }
                                    else if (output == 2)
                                    {
                                        lblstatus.Visible = true;
                                        //lblstatus.Text = "Week is  Updated ";
                                    }
                                    else if (output == 4)
                                    {
                                        // lblstatus.Text = "Vacation request already exists for this date";
                                    }
                                    else
                                    {
                                        // lblstatus.Text = "Please enter date with in the current week.";
                                    }
                                    }
                                    catch (Exception ex)
                                    { LogExceptions.LogException(ex.Message,"timesheet update"); }
                                }
                                if (id == "0" && projectRef > 0 && hours !=0.0)
                                {
                                    try
                                    {
                                        TimeSheetEntry.DN_TimesheetEntry(projectRef, string.Empty, 0, sessionKeys.UID,
                                       entryType, DateEntered, hours, string.Empty, site, projectTask, ref output);

                                        var projectPO1 = (from r in pro.Projects
                                                         where r.ProjectReference == projectRef
                                                         select r).ToList().FirstOrDefault();
                                        if (projectPO1 != null)
                                        {
                                            UpdatePONumber(projectRef, projectPO1.CustomerReference.ToString());
                                        }

                                        lblstatus.Visible = true;
                                        if (output == 0)
                                        {
                                            lblstatus.Text = "Error While inserting";
                                            lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else if (output == 1)
                                        {
                                            //lblstatus.Text = "Timesheet entered successfully";
                                            // lblstatus.ForeColor = System.Drawing.Color.Green;
                                        }
                                        else if (output == 2)
                                        {
                                            //lblstatus.Text = "You cannot add an entry to a timesheet that has been submitted for approval";
                                            //lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else if (output == 3)
                                        {
                                            //lblstatus.Text = "Timesheet entry already exists";
                                            //lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else if (output == 4)
                                        {
                                            //lblstatus.Text = "Vacation request already exists for this date";
                                            //lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else if (output == 10)
                                        {
                                            //lblstatus.Text = "You have exceeded 24 hours for the date entered";
                                            //lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                        else if (output == 5)
                                        {
                                            //lblstatus.Text = "Please enter date with in the current week.";
                                            //lblstatus.ForeColor = System.Drawing.Color.Red;
                                        }
                                    }
                                    catch (Exception ex)
                                    { LogExceptions.LogException(ex.Message, "timesheet insert"); }
                                  
                                }

                            }
                            //if (output == 10)
                            //{
                            //    lblstatus.Visible = true;
                            //    lblstatus.Text = "You have exceeded 24 hours for the date entered";
                            //    lblstatus.ForeColor = System.Drawing.Color.Red;
                            //}

                            lblError.Visible = true;
                            lblError.Text = Msg;
                        }
                        
                    }
                    BindGrid(txtweekcommencedate.Text, sessionKeys.UID, CountRow);
                    Session["update1"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }

    private int GetDates(int increment)
    {
        int date = 0;
        DateTime eDate1 = Convert.ToDateTime(txtweekcommencedate.Text.Trim());
       
        //date = string.Format("{0:d}", eDate1.Date.AddDays(increment).Day);
        date =  eDate1.Date.AddDays(increment).Day;
        return date;
    }
    protected void grdTimeSheetViewWeekly_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    public string AddOrdinal(int num)
    {
        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num.ToString() + "<sup>th</sup>";
        }

        switch (num % 10)
        {
            case 1:
                return num.ToString() + "<sup>st</sup>";
            case 2:
                return num.ToString() + "<sup>nd</sup>";
            case 3:
                return num.ToString() + "<sup>rd</sup>";
            default:
                return num.ToString() + "<sup>th</sup>";
        }

    }
    protected void btn_viewdate_Click(object sender, EventArgs e)
    {
        DateTime dtMonday = GetFirstDayOfWeek(Convert.ToDateTime(txtweekcommencedate.Text.Trim()));
        txtweekcommencedate.Text = dtMonday.ToShortDateString();
        BindGrid(txtweekcommencedate.Text.Trim(), sessionKeys.UID, 10);
    }

    public static string CheckValidHours(string originalString)
    {
        try
        {
            string s = originalString;
            //s = s.ToLower();
            string e = @"^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$";
            string sProper = "";
            //if(s==e)
            foreach (Match m in Regex.Matches(s, e))
            {
                sProper += (m.Value[0]) + m.Value.Substring(1, m.Length - 1);
                // sProper = e;
            }
            return sProper;
        }
        catch
        {
            return "";
        }
    }
    private double GetHours_double(string val )
    {
        double hours = 0.0;
        try
        {
            char[] comm = { ':' };
            string[] getva = val.Split(comm);
            string newval = "";
            newval = getva[0] + "." + getva[1];
            hours = Convert.ToDouble(newval);
        }
        catch (Exception ex)
        { hours = 0.0; LogExceptions.WriteExceptionLog(ex); }

        return hours;
    }


    #region VT update
    private DataTable GetVTdata(DateTime wcdate, int contractorid, int status)
    {
        DataTable dt = new DataTable();

        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.LeaveRequest_ByWCdate"
            , new SqlParameter("@WCdate_temp", wcdate),
            new SqlParameter("@ContractorID", contractorid),
            new SqlParameter("@Status", status)
            ).Tables[0];

        return dt;
    }

    private void Bind_VTgrid(DateTime wcdate, int contractorid, int status)
    {
        try
        {
            GridVT.DataSource = GetVTdata(wcdate, contractorid, status);
            GridVT.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    #endregion



    #region Newly added Section:- Expenses
    protected void ddlEntryTandE_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = GridView2.EditIndex;
        GridViewRow Row = GridView2.Rows[i];
        int test = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryTandE")).SelectedValue);

        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select BuyingPrice from ExpensesentryType where ID = {0}", test));
        string GetBuyingPrice = "";
        //  string GetSellingPrice = "";
        while (dr.Read())
        {
            GetBuyingPrice = dr["BuyingPrice"].ToString();
            //  GetSellingPrice = dr["sellingPrice"].ToString();

        }
        dr.Close();
        ((TextBox)Row.FindControl("txtUnitprice_expenses")).Text = GetBuyingPrice;
        //   ((TextBox)Row.FindControl("txtSellingprice_expenses")).Text = GetSellingPrice;


    }

    protected void ddlEntry_footerexpenses_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DropDownList ddlEntry = ((DropDownList)(GridView2.FooterRow.FindControl(GridView2.FindControl("ddlEntry"))));
        string test = ((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).Text;
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select BuyingPrice from ExpensesentryType where ID = {0}", test));
        string GetBuyingPrice = "";
        //  string GetSellingPrice = "";
        while (dr.Read())
        {
            GetBuyingPrice = dr["BuyingPrice"].ToString();
            //  GetSellingPrice = dr["sellingPrice"].ToString();
        }
        dr.Close();
        ((TextBox)GridView2.FooterRow.FindControl("txtUnitprice_footerexpenses")).Text = GetBuyingPrice;
        //  ((TextBox)GridView2.FooterRow.FindControl("txtSellingprice_footerexpenses")).Text = GetSellingPrice;


    }

    private void viewButtonCode_Timeandexpenses()
    {

        try
        {

            string WCdate = txtweekcommencedate.Text;
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DN_TimeExpensesdisplay", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate);
            myCommand.Parameters.Add("@contractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(sessionKeys.UID);


            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);
            //string _output = myCommand.Parameters["@out"].Value.ToString();
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       

    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyInsert_FooterExpenses")
        {
            ((DropDownList)GridView2.FooterRow.FindControl("ddlTitle_footerexpenses")).SelectedIndex = 0;
            ((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text = "";
            ((TextBox)GridView2.FooterRow.FindControl("txtQty_footerexpenses")).Text = "";
            ((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).SelectedIndex = 0;
            ((TextBox)GridView2.FooterRow.FindControl("txtNotes_footerexpenses")).Text = "";
        }

        if (e.CommandName == "Insert_FooterExpenses")
        {

            try
            {
                int ProjectReferenec_FooterExpenses = 0;
                int entrytype_FooterExpenses = 0;
                double BuyingPrice_FooterExpenses = 0;

                double Footer_Qty = 0;

                string notepad_FooterExpenses = "";

                ProjectReferenec_FooterExpenses = Convert.ToInt32(((DropDownList)GridView2.FooterRow.FindControl("ddlTitle_footerexpenses")).SelectedValue);



                DateTime eDate1_footer = DateTime.MinValue;
                if (((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text != "")
                {
                    eDate1_footer = Convert.ToDateTime(((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text);
                }

                if (((TextBox)GridView2.FooterRow.FindControl("txtUnitprice_footerexpenses")).Text != "")
                {
                    BuyingPrice_FooterExpenses = Convert.ToDouble(((TextBox)GridView2.FooterRow.FindControl("txtUnitprice_footerexpenses")).Text);
                }

                //if (((TextBox)GridView2.FooterRow.FindControl("txtSellingprice_footerexpenses")).Text != "")
                //{
                //    SellingPrice_FooterExpenses = Convert.ToDouble(((TextBox)GridView2.FooterRow.FindControl("txtSellingprice_footerexpenses")).Text);
                //}

                if (((TextBox)GridView2.FooterRow.FindControl("txtQty_footerexpenses")).Text != "")
                {
                    Footer_Qty = Convert.ToDouble(((TextBox)GridView2.FooterRow.FindControl("txtQty_footerexpenses")).Text);
                }
                if (((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).Text != "")
                {
                    entrytype_FooterExpenses = Convert.ToInt32(((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).SelectedValue);
                }

                notepad_FooterExpenses = ((TextBox)GridView2.FooterRow.FindControl("txtNotes_footerexpenses")).Text;


                int timeandexpenses;
                timeandexpenses = InsertTimeandexpenses(ProjectReferenec_FooterExpenses, Convert.ToInt32(sessionKeys.UID), entrytype_FooterExpenses, eDate1_footer, Footer_Qty, BuyingPrice_FooterExpenses, notepad_FooterExpenses);
                if (timeandexpenses == 0)
                    lblError.Text = "Error while inserting";
                else if (timeandexpenses >= 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "Expenses entered successfully";
                    viewButtonCode_Timeandexpenses();
                    ((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text = "";
                    ((TextBox)GridView2.FooterRow.FindControl("txtQty_footerexpenses")).Text = "";
                    ((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).SelectedIndex = 0;
                    ((TextBox)GridView2.FooterRow.FindControl("txtNotes_footerexpenses")).Text = "";
                    ProjectReferenec_FooterExpenses = 0;
                    entrytype_FooterExpenses = 0;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        if (e.CommandName == "Update")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView2.EditIndex;
                GridViewRow Row = GridView2.Rows[i];
                int ProjectReferenceTandE = Convert.ToInt32(((DropDownList)Row.FindControl("ddlTitleTandE")).SelectedItem.Value);
                int EntryTypeTandE = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryTandE")).SelectedItem.Value);
                DateTime eDateTandE = Convert.ToDateTime(((TextBox)Row.FindControl("txtEndDateTandE")).Text);
                double BuyingPrice = 0;

                if (((TextBox)Row.FindControl("txtUnitprice_expenses")).Text != "")
                {
                    BuyingPrice = Convert.ToDouble(((TextBox)Row.FindControl("txtUnitprice_expenses")).Text);
                }
                //if (((TextBox)Row.FindControl("txtSellingprice_expenses")).Text != "")
                //{
                //    SellingPrice = Convert.ToDouble(((TextBox)Row.FindControl("txtSellingprice_expenses")).Text);
                //}
                double Qty = 0;
                if (((TextBox)Row.FindControl("txtQtyTandE")).Text != "")
                {
                    Qty = Convert.ToDouble(((TextBox)Row.FindControl("txtQtyTandE")).Text);
                }
                string NotesTandE = ((TextBox)Row.FindControl("txtNotesTandE")).Text;

                UpdateTimeandexpenses(ID, ProjectReferenceTandE, EntryTypeTandE, eDateTandE, Qty, BuyingPrice, NotesTandE);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
            }
        }
        if (e.CommandName == "Delete")
        {

            string id = e.CommandArgument.ToString();
            try
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "delete from TimeExpenses where ID='" + id + "'");
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

            GridView2.EditIndex = -1;
            // viewButtonCode();
            viewButtonCode_Timeandexpenses();
        }
        if (e.CommandName == "ExtraExpensesType")
        {
            Response.Redirect("~/WF/Admin/AdminDropDown.aspx?type=ExpensesType", false);
        }

    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;

        viewButtonCode_Timeandexpenses();
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;

        viewButtonCode_Timeandexpenses();
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView2.EditIndex = -1;
        viewButtonCode_Timeandexpenses();

    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView2.EditIndex = -1;

        viewButtonCode_Timeandexpenses();
    }
    private int InsertTimeandexpenses(int Projectreference_Expenses, int Resource_Expenses, int EntryType_Expenses, DateTime DateEnter_Expenses, double Amount_Expenses, Double BuyingPrice_Footer, string Notes_Expenses)
    {
        try
        {

            int getVal = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InserTimeandexpenses_Resource",
                new SqlParameter("@ProjectReference", Projectreference_Expenses), new SqlParameter("@ContractorID", Resource_Expenses), new SqlParameter("@EntryType", EntryType_Expenses),
                new SqlParameter("@TimeExpensesDate", DateEnter_Expenses), new SqlParameter("@Qty", Amount_Expenses), new SqlParameter("@BuyingPrice", BuyingPrice_Footer),
                new SqlParameter("@Notes", Notes_Expenses));
           
            return getVal;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return 0;
        }
    }

    public void UpdateTimeandexpenses(int ID, int ProjectReference1, int EntryType1, DateTime eDate1, double amount, double BuyingPrice, string Notes1)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_Timeandexpensesupdate_resource");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference1);
            db.AddInParameter(cmd, "@ResourceID", DbType.Int32, Convert.ToInt32(sessionKeys.UID));
            db.AddInParameter(cmd, "@entryid", DbType.Int32, EntryType1);
            db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, eDate1);
            db.AddInParameter(cmd, "@Qty", DbType.Double, amount);
            db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, BuyingPrice);

            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "@notes", DbType.String, Notes1);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@output");

            if (getVal == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Error While Timesheet Updation";
            }
            else if (getVal == 1)
            {
                lblError.Visible = true;
                lblError.Text = "Expenses updated  successfully ";
            }
            cmd.Dispose();
            viewButtonCode_Timeandexpenses();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            //return 0;
        }
    }
    #endregion
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
            if (objList != null)
            {
                if (objList[0].ToString() == "-99")
                {
                    e.Row.Visible = false;
                }
            }
        }

    }
    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
    {
        CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
        return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
    }
    //public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
    //{
    //    DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
    //    DateTime firstDayInWeek = dayInWeek.Date;
    //    while (firstDayInWeek.DayOfWeek != firstDay)
    //        firstDayInWeek = firstDayInWeek.AddDays(-1);

    //    return firstDayInWeek;
    //}
}
