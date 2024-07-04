using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
//using PortfolioMgt.DAL;
//using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TimeSheet_Admin;
using CheckTimesheetApprovers;
using Deffinity.ProgrammeManagers;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

public partial class Resource_TimeSheetResourcesDaily : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    //List<Timesheet_BindDates2Result> getDates = null;
    public string selectedDate = DateTime.Now.ToShortDateString();
    DateTime getDate = DateTime.Now;
    IEnumerable<CustomerTimesheetMail> customerTimesheetMail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Master.PageHead = "Timesheet-Resources";
            //set current date to text box
           

           // TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();

            //getDates = (from r in TimeSheetEntry.Timesheet_BindDates2(sessionKeys.UID)
            //            select new Timesheet_BindDates2Result
            //            {
            //                color = r.color,
            //                EnteredDate = r.EnteredDate,
            //                total = r.total
            //            }).ToList();


            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetDate();
                txtDate.Text = DateTime.Now.ToShortDateString();
                txtHours.Text = "00:00";

                lblMsg.Visible = false;
                lblTitle.Text = "Timesheet-Resources: " + sessionKeys.UName;
                //lblTitle.Text = "Timesheet-Resources: " + User.Identity.Name;
                //calPreviousMonth.VisibleDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
                //calCurrentMonth.VisibleDate = DateTime.Today;
                //BindCustomers();

                BindProjectTitle();
                BindEntryTypes();
                //lblCurrentDate.Visible = false;
                DateTime dtMonday = GetFirstDayOfWeek(DateTime.Today);
                string weekComencingDate = dtMonday.ToShortDateString();
                txtweekcommencedate.Text = weekComencingDate;
                //imgSubmit.Visible = false;
                //hidDate.Value = DateTime.Now.ToShortDateString();
                hidDate.Value = dtMonday.ToString();
                //BindTimeSheetEntryGrid(DateTime.Now.ToShortDateString());
                BindTimeSheetEntryGrid(Convert.ToDateTime(txtweekcommencedate.Text).ToString());
                Bind_VTgrid(Convert.ToDateTime(txtweekcommencedate.Text), sessionKeys.UID, 0);
               // calCurrentMonth.VisibleDate = DateTime.Today;

                //FillCalender();
                
                viewButtonCode_Timeandexpenses();

            }
        }
        catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
               
            }
    }
    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
    {
        CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
        return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
    }
    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
    {
        DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        DateTime firstDayInWeek = dayInWeek.Date;
        while (firstDayInWeek.DayOfWeek != firstDay)
            firstDayInWeek = firstDayInWeek.AddDays(-1);

        return firstDayInWeek;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["update"] = Session["update"];
        // hiddenSession.Value = hidden;
    }
    #region Bind customers,Grid and entry type 

    //private void FillCalender()
    //{
    //    try
    //    {
    //        TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();

    //        getDates = (from r in TimeSheetEntry.Timesheet_BindDates2(sessionKeys.UID)
    //                    select new Timesheet_BindDates2Result
    //                    {
    //                        color = r.color,
    //                        EnteredDate = r.EnteredDate,
    //                        total = r.total
    //                    }).ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void BindTimeSheetEntryGrid(string date)
    {
        try
        {
            //imgSubmit.Visible = false;
            int status = Convert.ToInt32(ddlStatusupdate.SelectedValue.ToString());
            int _out = 0;
            //Nullable<int> _out = null;
            //TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();
            //List<DN_selecttimesheetcveiwResult> getEntry = (from r in TimeSheetEntry.DN_selecttimesheetcveiw
            //                   (Convert.ToDateTime(date), sessionKeys.UID,status,ref _out)
            //                      select r).ToList();


            //int cnt=getEntry.Count();

            DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter("DN_selecttimesheetcveiw", Constants.DBString);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@Date", DbType.DateTime).Value = Convert.ToDateTime(date);
            //da.SelectCommand.Parameters.Add("@contractorID", DbType.Int32).Value = sessionKeys.UID;
            //da.SelectCommand.Parameters.Add("@Status", DbType.Int32).Value = status;
            //da.SelectCommand.Parameters.Add("@out", DbType.Int32).Value = 0;
            //da.Fill(ds);

            ds= SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,"DN_selecttimesheetcveiw", new SqlParameter("@Date",Convert.ToDateTime(date)),
                new SqlParameter("@contractorID", sessionKeys.UID), new SqlParameter("@Status", status), new SqlParameter("@out",0));

            //if (ds.Tables[0].Rows.Count > 1)
            //{
            //    imgSubmit.Visible = true;
            //}
            // int cnt;
            //if (cnt > 1)
            //{
            //    imgSubmit.Visible = true;
            //}

            string day = string.Format("{0:dddd}", Convert.ToDateTime(date));
            string monthName = string.Format("{0:MMM}", Convert.ToDateTime(date));
            string days = string.Format("{0:dd}", Convert.ToDateTime(date));
            string year = string.Format("{0:yyyy}", Convert.ToDateTime(date));
            //lblCurrentDate.Text = " <h1> <b>"+day+"</b><br/>"+days+"</h1> <h2>"+monthName+"<br />"+year+"</h2>";
            // lblCurrentDate.ForeColor = Color.Red;
            lblMsg.Visible = false;
            //lblCurrentDate.Visible = false;

            //if (getEntry != null)
            //{
            //    grdTimeSheetEntry.Visible = true;
            //    grdTimeSheetEntry.DataSource = getEntry;
            //    grdTimeSheetEntry.DataBind();
            //    if (grdTimeSheetEntry.Rows.Count <= 1)
            //    {
            //        grdTimeSheetEntry.Visible = false;
            //    }

            //}

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdTimeSheetEntry.Visible = true;
                grdTimeSheetEntry.DataSource = ds;
                grdTimeSheetEntry.DataBind();
                if (grdTimeSheetEntry.Rows.Count <= 0)
                {
                    grdTimeSheetEntry.Visible = false;
                }
            }
            else
            {
                grdTimeSheetEntry.Visible = false;
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            con.Close();
            
        }
    }
    private void BindProjectTitle()
    {
        try
        {
            
            //TimeSheet_GetProjectsTitlesWithReference

            using (projectTaskDataContext timeSheet = new projectTaskDataContext())
            {
                DataSet ds = new DataSet();

                var mylist = from r in timeSheet.ProjectDetails
                             join k in timeSheet.ProjectItems on r.ProjectReference equals k.ProjectReference
                             where k.ContractorID == sessionKeys.UID && r.ProjectStatusName == "Live"
                             orderby r.ProjectTitle
                             select new { ID = r.ID, ABC = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle, ProjectRef = r.ProjectReference };

                ddlProjectTile.DataSource = mylist.Distinct();
                ddlProjectTile.DataTextField = "ABC";
                ddlProjectTile.DataValueField = "ProjectRef";
                ddlProjectTile.DataBind();
                ddlProjectTile.Items.Insert(0, new ListItem("Please select...", "0"));




            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //private void BindCustomers()
    //{
    //    PortfolioDataContext timeSheet = new PortfolioDataContext();
       
      
    //    try
    //    {
    //      var  portfolio= from r in timeSheet.ProjectPortfolios
    //                      where r.Visible==true
    //                      orderby r.PortFolio
    //                         select r;
    //        ddlCustomers.DataSource = portfolio;
    //        ddlCustomers.DataTextField = "PortFolio";
    //        ddlCustomers.DataValueField = "ID";
    //        ddlCustomers.DataBind();
    //        ddlCustomers.Items.Insert(0, new ListItem("Please select...", "0"));
    //        ddlCustomers.SelectedItem.Value = sessionKeys.PortfolioID.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    private void BindEntryTypes()
    {
        try
        {
            using (TimeSheetDataContext timeSheet = new TimeSheetDataContext())
            {

                List<TimesheetEntryType> portfolio = (from r in timeSheet.TimesheetEntryTypes
                                                      orderby r.EntryType
                                                      select new TimesheetEntryType { EntryType = r.EntryType, ID = r.ID }).ToList();
                if (portfolio != null)
                {
                    ddlType.DataSource = portfolio;
                    ddlType.DataTextField = "EntryType";
                    ddlType.DataValueField = "ID";
                    ddlType.DataBind();
                }
                //ddlType.Items.Insert(0, new ListItem("Please select...", "0"));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    #endregion

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string val = txtHours.Text;
            char[] comm = { ':' };
            string[] getva = val.Split(comm);

            string newval = "";

            newval = getva[0] + "." + getva[1];
            double hours = Convert.ToDouble(newval);
           // Nullable<int> output = null;
            int site = 0;
            int task = 0;
            int entrytype = 0;

            string notes = txtAddNotes.Text.ToString();
            task = int.Parse(string.IsNullOrEmpty(ddlTasks.SelectedValue) ? "0" : ddlTasks.SelectedValue);
            entrytype = int.Parse(string.IsNullOrEmpty(ddlType.SelectedValue) ? "0" : ddlType.SelectedValue);
            site = int.Parse(string.IsNullOrEmpty(ddlSite.SelectedValue) ? "0" : ddlSite.SelectedValue);
               


            if (ViewState["update"].ToString() == Session["update"].ToString())
            {
                //TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();

                if (hours <= 24.0)
                {
                    if ((ddlProjectTile.SelectedItem.Value.ToString() != "0") || (txtServiceRequest.Text != ""))
                    {
                        //if (txtServiceRequest.Text != "")
                        //{

                        Database db = DatabaseFactory.CreateDatabase("DBstring");
                        DbCommand cmd = db.GetStoredProcCommand("DN_TimesheetEntry");
                        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, int.Parse(ddlProjectTile.SelectedValue));
                        db.AddInParameter(cmd, "@Activity", DbType.String, txtServiceRequest.Text);
                        db.AddInParameter(cmd, "@AssignType", DbType.Int32, 0);
                        db.AddInParameter(cmd, "@ResourceID", DbType.Int32, sessionKeys.UID);
                        db.AddInParameter(cmd, "@entryid", DbType.Int32, entrytype);
                        db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, Convert.ToDateTime(string.Format("{0} {1}", txtDate.Text,DateTime.Now.ToShortTimeString()) ));

                        db.AddInParameter(cmd, "@hours", DbType.Double, hours);
                        db.AddInParameter(cmd, "@notes", DbType.String, notes);
                        db.AddInParameter(cmd, "@SiteID", DbType.Int32, site);
                        db.AddInParameter(cmd, "@ProjectTaskID", DbType.Int32, task);
                        db.AddOutParameter(cmd, "@output", DbType.Int32, 0);
                        db.ExecuteNonQuery(cmd);
                        int output = (int)db.GetParameterValue(cmd, "@output");

                        cmd.Dispose();





                            //TimeSheetEntry.DN_TimesheetEntry(int.Parse(ddlProjectTile.SelectedValue), txtServiceRequest.Text,
                            //                        0, sessionKeys.UID,
                            //                        entrytype, Convert.ToDateTime(txtDate.Text), hours, notes,
                            //                         site, task, ref output);
                            lblMsg.Visible = true;
                            if (output == 0)
                            {
                                lblMsg.Text = "Error While inserting";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (output == 1)
                            {

                                lblMsg.Text = "Timesheet entered successfully";
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                //FillCalender();
                                //hidDate.Value = txtDate.Text;
                                BindTimeSheetEntryGrid(txtDate.Text);
                                //calPreviousMonth.VisibleDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
                                //calCurrentMonth.VisibleDate = DateTime.Today;
                                //calCurrentMonth.VisibleDate = Convert.ToDateTime(txtDate.Text);

                            }

                            else if (output == 2)
                            {
                                lblMsg.Text = "You cannot add an entry to a timesheet that has been submitted for approval";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (output == 3)
                            {
                                lblMsg.Text = "Timesheet entry already exists";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (output == 4)
                            {
                                lblMsg.Text = "Vacation request already exists for this date";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (output == 10)
                            {
                                lblMsg.Text = "You have exceeded 24 hours for the date entered";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (output == 5)
                            {
                                lblMsg.Text = "Please enter date with in the current week.";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                            ResetAll();
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            //pnl.Update();
                        //}
                        //else
                        //{
                        //    lblMsg.Visible = true;
                        //    lblMsg.Text = "Please enter Service Request";
                        //    lblMsg.ForeColor = System.Drawing.Color.Red;
                        //}
                    }
                    else
                    {
                        lblMsg.Visible = true;  
                        lblMsg.Text = "Please Select Project / Service Request";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
  

    public string ChangeHoues(string GetHours)
    {

        string GetActivity = "";
        try
        {
            char[] comm1 = { '.' };
            string[] displayTime = GetHours.Split(comm1);


            GetActivity = displayTime[0] + ":" + displayTime[1];


        }
        catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        return GetActivity;
    }

    public string TimesheetStatus(string Status)
    {
        string TimesheetStaus = "";
        try
        {
           
            if (Status == "4")
            {
                TimesheetStaus = "Approved";
            }
            else if (Status == "2")
            {
                TimesheetStaus = "Submitted for Approval";
            }
            else if (Status == "1")
            {
                TimesheetStaus = "Not Submitted";
            }
            else
            {
                TimesheetStaus = "Declined";
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return TimesheetStaus;
    }
    protected bool GetTimeSheetStatusCheck(string statusid)
    {
        bool st = false;
        try
        {
            //check the status is approve or submitted
            if ((int.Parse(statusid) == 1))
                st = true;

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


        return st;

    }

   

   
    private void BindCustomersByProjectRef(DropDownList ddlCustomersGrid, int setVal, int projectRef)
    {
        //TimeSheetDataContext timeSheet = new TimeSheetDataContext();
        //projectTaskDataContext project = new projectTaskDataContext();
        using (projectTaskDataContext task = new projectTaskDataContext())
        {
            var pd = from r in task.ProjectDetails
                     where r.ProjectReference == projectRef
                     select r;
            int id = 0;
            foreach (ProjectMgt.Entity.ProjectDetails i in pd)
            {
                id = int.Parse(i.Portfolio.ToString());
            }

            var getCustomers = from r in task.DN_TimeSheet_ProjectTitle(sessionKeys.UID, id)
                               orderby r.ProjectTitle
                               select r;
            ddlCustomersGrid.DataSource = getCustomers;
            ddlCustomersGrid.DataValueField = "ProjectReference";
            ddlCustomersGrid.DataTextField = "ProjectTitle";
            ddlCustomersGrid.DataBind();
            // ddlCustomersGrid.Items.Insert(0, new ListItem("Please select...", "0"));

            ddlCustomersGrid.SelectedValue = setVal.ToString();
        }

    }



    protected void grdTimeSheetEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //TimeSheetDataContext timeSheet = new TimeSheetDataContext();
            projectTaskDataContext project = new projectTaskDataContext();
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                //DN_selecttimesheetcveiwModifyResult de = (DN_selecttimesheetcveiwModifyResult)e.Row.DataItem;
                //DN_selecttimesheetcveiwResult de = (DN_selecttimesheetcveiwResult)e.Row.DataItem;
              
                //if (de.ID == -99)
                //{
                //    e.Row.Visible = false;

                //}

                if (objList != null)
                {
                    if (objList[0].ToString() == "-99")
                    {
                        e.Row.Visible = false;
                    }
                    else
                    {
                        if (e.Row.FindControl("ddlProjectTitle") != null)
                        {
                            DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
                            DropDownList ddlProjectTask = (DropDownList)e.Row.FindControl("ddlProjectTask");
                            DropDownList ddlEntryType = (DropDownList)e.Row.FindControl("ddlEntryType");
                            DropDownList ddlSites = (DropDownList)e.Row.FindControl("ddlSites");
                            CascadingDropDown casCadProjectTask = (CascadingDropDown)e.Row.FindControl("casCadProjectTask");
                            CascadingDropDown casCadEntryType = (CascadingDropDown)e.Row.FindControl("casCadEntryType");
                            CascadingDropDown casCadSitesGrid = (CascadingDropDown)e.Row.FindControl("casCadSitesGrid");




                            //BindCustomersByProjectRef(ddlProjectTitle,de.ProjectReference.Value, de.ProjectReference.Value);
                            //casCadProjectTask.SelectedValue = de.TaskID.ToString();
                            //casCadEntryType.SelectedValue = de.EntryTypeID.ToString();
                            //casCadSitesGrid.SelectedValue = de.SiteID.ToString();


                            casCadProjectTask.SelectedValue = objList[15].ToString();
                            casCadEntryType.SelectedValue = objList[2].ToString();
                            casCadSitesGrid.SelectedValue = objList[13].ToString();
                            projectTaskDataContext prjTask = new projectTaskDataContext();
                            DataSet ds = new DataSet();

                            var mylist = from r in prjTask.ProjectDetails
                                         join k in prjTask.ProjectItems on r.ProjectReference equals k.ProjectReference
                                         where k.ContractorID == sessionKeys.UID && r.ProjectStatusName == "Live"
                                         orderby r.ProjectTitle
                                         select new { ID = r.ID, ABC = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle, ProjectRef = r.ProjectReference };

                            ddlProjectTitle.DataSource = mylist.Distinct();
                            ddlProjectTitle.DataTextField = "ABC";
                            ddlProjectTitle.DataValueField = "ProjectRef";
                            ddlProjectTitle.DataBind();
                            ddlProjectTitle.Items.Insert(0, new ListItem("Please select...", "0"));
                            //ddlProjectTitle.SelectedValue = de.ProjectReference.ToString();

                            //ddlProjectTask.SelectedValue = de.ProjectTask.ToString();
                            ddlProjectTitle.SelectedValue = objList[6].ToString();

                            ddlProjectTask.SelectedValue = objList[15].ToString();
                           

                        }
                    }
                }


                //if (e.Row.FindControl("lblID") != null)
                //{
                //    Label lblid = (Label)e.Row.FindControl("lblID");
                //    if (lblid.Text == "-99")
                //    {
                //        e.Row.Visible = false;
                //    }
                //}
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    //protected void calCurrentMonth_DayRender(object sender, DayRenderEventArgs e)
    //{
    //    try
    //    {
    //        if (getDates != null)
    //        {
    //            if (!e.Day.IsOtherMonth)
    //            {
    //                foreach (Timesheet_BindDates2Result s in getDates)
    //                {
    //                    if (s.EnteredDate != DBNull.Value.ToString() && !string.IsNullOrEmpty(s.color))
    //                    {
    //                        if (s.EnteredDate.Equals(e.Day.Date.ToShortDateString()))
    //                        {
    //                            ColorConverter clrConverter = new ColorConverter();
    //                            Color clr1 = (Color)clrConverter.ConvertFromString(s.color);
    //                            e.Cell.BackColor = clr1;
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                e.Cell.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}
   
    //protected void calPreviousMonth_DayRender(object sender, DayRenderEventArgs e)
    //{

    //    try
    //    {
    //        if (getDates != null)
    //        {
    //            if (!e.Day.IsOtherMonth)
    //            {
    //                foreach (Timesheet_BindDates2Result s in getDates)
    //                {
    //                    if (s.EnteredDate != DBNull.Value.ToString())
    //                    {
    //                        if (s.EnteredDate.Equals(e.Day.Date.ToShortDateString()))
    //                        {
    //                            ColorConverter clrConverter = new ColorConverter();
    //                            Color clr1 = (Color)clrConverter.ConvertFromString(s.color);
    //                            e.Cell.BackColor = clr1;
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                e.Cell.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
       
    //}

    private void ResetAll()
    {
        //ddlCustomers.SelectedIndex = -1;
        txtDate.Text = DateTime.Now.ToShortDateString();
        txtHours.Text = "00:00";
        ddlType.SelectedIndex = -1;
        txtAddNotes.Text = string.Empty;
        txtServiceRequest.Text = string.Empty;
        ddlSite.SelectedIndex = -1;
        ddlProjectTile.SelectedIndex = -1;
        ddlTasks.SelectedIndex = -1;
    }
    protected void calCurrentMonth_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
       // FillCalender();
    }
    //protected void calPreviousMonth_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    //{
    //   // FillCalender();
    //}
    protected void grdTimeSheetEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        try
        {
            TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();
             Nullable<int> output = null;
            if (e.CommandName == "Update")
            {

                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdTimeSheetEntry.EditIndex;
                GridViewRow Row = grdTimeSheetEntry.Rows[i];
                
                    
                    TextBox txtEndDate=(TextBox)Row.FindControl("txtEndDate"); 
                 TextBox txtHoursE=(TextBox)Row.FindControl("txtHoursE");
                 TextBox txtActivity = (TextBox)Row.FindControl("txtActivity");
                DropDownList ddlProjectTitle = (DropDownList)Row.FindControl("ddlProjectTitle");
                DropDownList ddlProjectTask = (DropDownList)Row.FindControl("ddlProjectTask");
                DropDownList ddlEntryType = (DropDownList)Row.FindControl("ddlEntryType");
                DropDownList ddlSites = (DropDownList)Row.FindControl("ddlSites");

                int site = 0;
                int task = 0;
                int entrytype = 0;

                var myTimesheetEntry = from t in TimeSheetEntry.TimesheetEntries
                                       where t.ID == ID
                                       select t;
                //string PTitle = myTimesheetEntry.

                task = int.Parse(string.IsNullOrEmpty(ddlProjectTask.SelectedValue) ? "0" : ddlProjectTask.SelectedValue);
                entrytype = int.Parse(string.IsNullOrEmpty(ddlEntryType.SelectedValue) ? "0" : ddlEntryType.SelectedValue);
                site = int.Parse(string.IsNullOrEmpty(ddlSites.SelectedValue) ? "0" : ddlSites.SelectedValue);
               
                double Hours1 = 0;
                if (((TextBox)Row.FindControl("txtHoursE")).Text != "")
                {
                    string val = ((TextBox)Row.FindControl("txtHoursE")).Text;
                    char[] comm = { ':' };
                    string[] getva = val.Split(comm);

                    string newval = "";

                    newval = getva[0] + "." + getva[1];
                    Hours1 = Convert.ToDouble(newval);
                }
                string Notes1 = ((TextBox)Row.FindControl("txtNotes")).Text;
                 int projectRef = 0;
                 foreach (var c in myTimesheetEntry)
                 {
                     projectRef = c.ProjectReference.Value;
                 }

                 if (projectRef != 0)
                 {
                      if ((int.Parse(ddlProjectTitle.SelectedValue) == 0))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Select project";
                }
                else
                {
                    if((int.Parse(ddlProjectTask.SelectedValue)==0))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Select Task";
                    }
                 }
                 }

                 
                   
                        
                    if (Hours1 <= 24.0)
                    {
                        TimeSheetEntry.DN_TimesheetEntryupdate(ID, projectRef, txtActivity.Text, 0, sessionKeys.UID,
                           entrytype, Convert.ToDateTime(string.Format("{0} {1}", txtEndDate.Text,DateTime.Now.ToShortTimeString())), Hours1, Notes1,
                            site, task, ref output);

                        


                        if (output == 10)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Visible = true;
                            lblMsg.Text = "You have exceeded 24 hours for the date entered";
                        }
                        else if (output == 0)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Error While Timesheet Updation";
                        }
                        else if (output == 1)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Timesheet Updated  Successfully ";
                            lblMsg.ForeColor = System.Drawing.Color.Green;

                            //FillCalender();

                            //calPreviousMonth.VisibleDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
                            //calCurrentMonth.VisibleDate = DateTime.Today;

                        }
                        else if (output == 2)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Week is  Updated ";
                        }
                        else if (output == 4)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Text = "Vacation request already exists for this date";
                        }
                        else if (output == 5)
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Text = "Please enter date with in the current week.";
                        }
                        else
                        {
                            grdTimeSheetEntry.EditIndex = -1;
                            lblMsg.Visible = true;
                            lblMsg.Text = "New Timesheet is inserted";
                        }
                    }
                    grdTimeSheetEntry.EditIndex = -1;
                    BindTimeSheetEntryGrid(txtEndDate.Text);
                    }

                
            

            if (e.CommandName == "Delete")
            {

                string id = e.CommandArgument.ToString();
                TimeSheetEntry.DN_TimesheetEntryDelete(int.Parse(id));
                lblMsg.Visible = true;
                lblMsg.Text = "Deleted Successfully";
                //FillCalender();
                BindTimeSheetEntryGrid(hidDate.Value);
                //calPreviousMonth.VisibleDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
                //calCurrentMonth.VisibleDate = DateTime.Today;
            }


           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }
    protected void grdTimeSheetEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //grdTimeSheetEntry.EditIndex = -1;
        grdTimeSheetEntry.EditIndex = -1;
        BindTimeSheetEntryGrid(hidDate.Value);
        
    }
    protected void grdTimeSheetEntry_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        
        grdTimeSheetEntry.EditIndex = e.NewEditIndex; //grdTimeSheetEntry.EditIndex = -1;
        BindTimeSheetEntryGrid(hidDate.Value);
    }
    protected void grdTimeSheetEntry_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdTimeSheetEntry.EditIndex = -1;

        BindTimeSheetEntryGrid(hidDate.Value);
    }
    
    
    protected void grdTimeSheetEntry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        
        grdTimeSheetEntry.EditIndex = -1;
        BindTimeSheetEntryGrid(hidDate.Value);
    }
    protected void ddlProjectTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdTimeSheetEntry.EditIndex;
        GridViewRow row = grdTimeSheetEntry.Rows[index];
        DropDownList ddlProjectTitle = (DropDownList)row.FindControl("ddlProjectTitle");
        DropDownList ddlProjectTask = (DropDownList)row.FindControl("ddlProjectTask");
        DropDownList ddlEntryType = (DropDownList)row.FindControl("ddlEntryType");
        DropDownList ddlSites = (DropDownList)row.FindControl("ddlSites");
        //BindTasks(ddlProjectTask,0,int.Parse(ddlProjectTitle.SelectedValue));
        //BindSites(ddlSites, 0, int.Parse(ddlProjectTitle.SelectedValue));
    }

    private int GetWCdataID()
    {
        return int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Timesheet_WCDateID", new SqlParameter("@Date", Convert.ToDateTime(txtweekcommencedate.Text.Trim())), new SqlParameter("@contractorID", sessionKeys.UID)).ToString());
    }
    private string GetApproverEmail()
    {
        return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select (select EmailAddress from Contractors as c where c.ID = Contractors.TimeApproveID) as Email  from Contractors where ID = @UID", new SqlParameter("@UID", sessionKeys.UID)).ToString();
    }
    private string GetApproverEmail1(int ApproverID)
    {
        return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select  EmailAddress as Email from Contractors where ID = @UID", new SqlParameter("@UID", ApproverID)).ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private int GetOwnerID(int wcdateid)
    {
        int getOwnerid = 0;
        
        try
        {
          getOwnerid = int.Parse( SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select isnull(OwnerID,0) as OwnerID   from Projects where ProjectReference in(select top 1 ProjectReference  from TimesheetEntry where WCDateID={0} and ContractorID={1})", wcdateid, sessionKeys.UID)).ToString());
        }
        catch (Exception ex)
        {
            getOwnerid = 0;
        }
        return getOwnerid;
    }
    string id2;
    string EmailAddress;
    string subjectline1;
    protected void imgSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext();
            int index = grdTimeSheetEntry.EditIndex;
            string ids = string.Empty;
            //bool isSelectd = false;
            for (int i = 0; i < grdTimeSheetEntry.Rows.Count; i++)
            {

                GridViewRow row = grdTimeSheetEntry.Rows[i];
                CheckBox chkResource = (CheckBox)row.FindControl("chkResource");
                Label lblID = (Label)row.FindControl("lblID");
                if (lblID.Text != "-99")
                {
                    Label lblStatus = (Label)row.FindControl("lblStaus_Time");
                    if (lblStatus.Text == "Not Submitted")
                    {
                        //TimeSheetEntry.TimesheetEntry_UpdateStatus(int.Parse(lblID.Text), 2);
                        ids = ids + lblID.Text + ",";
                    }
                }
            }
            //update timesheet status 
            if (!string.IsNullOrEmpty(ids))
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "TimesheetEntry_UpdateStatusByIDs", new SqlParameter("@IDs", ids), new SqlParameter("@StatusID", 2));
            }

                    #region newcode for emailing
                    #region Newly add section with including projectowner

                    //need to send mails to projectOwners.
                    int wcdateid = GetWCdataID();
                    DateTime weekdate = Convert.ToDateTime(txtweekcommencedate.Text.Trim());
                    int ProjectOwnerID = 0;
                    TimesheetresourceSection GetProjectOwnerEmaiID = null;
                  
                        ProjectOwnerID = GetOwnerID(wcdateid);

                        if (ProjectOwnerID != 0 && ApproverEmailAlertCheck(ProjectOwnerID))
                        {
                            // Timesheet1.Visible = true;
                            //  here we are getting the project owner Email address.
                            try
                            {
                                TimesheetProject1.Visible = true;
                                GetProjectOwnerEmaiID = new TimesheetresourceSection();
                                int GetProjectReference = 0;
                                GetProjectReference = GetProjectOwnerEmaiID.GetProjectReference(wcdateid, sessionKeys.UID, ProjectOwnerID);
                                string GetprojectTitle = GetProjectOwnerEmaiID.GetProjectTitle(GetProjectReference);
                                //
                                string ProjectTitle = sessionKeys.Prefix + GetProjectReference.ToString() + " - " + GetprojectTitle;

                                TimesheetProject1.ProjectOwner_BindData(sessionKeys.UID, GetProjectReference, ProjectOwnerID, wcdateid, GetprojectTitle, String.Format("{0:s}", weekdate));
                                EmailAddress = GetProjectOwnerEmaiID.GetProjectOwnermailID(ProjectOwnerID);
                                subjectline1 = "Timesheet(s) Submitted for approval";

                                string htmlText = string.Empty;

                                StringWriter sw = new StringWriter();
                                Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                TimesheetProject1.RenderControl(htmlTW);
                                htmlText = htmlTW.InnerWriter.ToString();
                                string errorString = string.Empty;
                                Email eMail = new Email();
                                eMail.SendingMail(EmailAddress, subjectline1, htmlText);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            finally
                            {
                                TimesheetProject1.Visible = false;
                            }
                        }

                    
                    #endregion
                    bool CheckApprovers = false;
                    TimeSheetAdminApprove GetTimesheetApproverslist = new TimeSheetAdminApprove();
                    TimeSheetAdminApprove CheckTimesheetApproverslist = new TimeSheetAdminApprove();
                    CheckApprovers = CheckTimesheetApproverslist.CheckTimesheetApproversExists(sessionKeys.UID);
                    Chekingtimesheetapprovers CheckTimesheetApprover = new Chekingtimesheetapprovers();




                    if ((CheckApprovers == true))
                    {
                        DataSet listofapprovers = new DataSet();
                        listofapprovers = GetTimesheetApproverslist.GetApprovallist(sessionKeys.UID);
                        int TimesheetPrimaryApprover = 0;
                        int TimesheetOptionalApprover = 0;
                        if (listofapprovers.Tables[0].Rows.Count > 0)
                        {
                            TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                            TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

                            if (TimesheetOptionalApprover != 0 && ApproverEmailAlertCheck(TimesheetOptionalApprover))
                            {
                                Timesheet1.Visible = true;

                                try
                                {

                                    Timesheet1.BindData(wcdateid, sessionKeys.UID, String.Format("{0:s}", weekdate), TimesheetOptionalApprover);
                                    string subjectline2 = "Timesheet(s) Submitted for approval";

                                    string htmlText = string.Empty;

                                    StringWriter sw = new StringWriter();
                                    Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                    Timesheet1.RenderControl(htmlTW);
                                    htmlText = htmlTW.InnerWriter.ToString();
                                    string errorString = string.Empty;
                                    Email eMail = new Email();
                                    string SecondaryTimesheetEmailAddress;
                                    SecondaryTimesheetEmailAddress = GetApproverEmail1(TimesheetOptionalApprover);

                                    eMail.SendingMail(SecondaryTimesheetEmailAddress, subjectline2, htmlText);
                                    // eMail.SendingMail(GetApproverEmail1(TimesheetOptionalApprover), subjectline2, htmlText);
                                }

                                catch (Exception ex)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Timesheet1.Visible = false;
                                }
                            }
                        }


                    }
                


                    #endregion

            SendCustomerMail(wcdateid);
            
            BindTimeSheetEntryGrid(hidDate.Value);
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public string TimeSheetActivity(string GetACtivity1)
    {
        string GetActivity = "";

        if (GetACtivity1 == "0")
        {
            GetActivity = "";
        }
        else
        {
            GetActivity = GetACtivity1.ToString();
        }
        return GetActivity;
    }

    public string GetTimeSheetActivity(string GetACtivity2)
    {
        string GetActivity = "";

        if (GetACtivity2 == "0")
        {
            GetActivity = "";
        }
        else
        {
            GetActivity = GetACtivity2.ToString();
        }
        return GetActivity;
    }


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
        //SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        try
        {
            DateTime dtMonday = GetFirstDayOfWeek(DateTime.Today);
            string WCdate = dtMonday.ToShortDateString();
            
            //SqlCommand myCommand = new SqlCommand("DN_TimeExpensesdisplay", Constants.DBString);
            //myCommand.CommandType = CommandType.StoredProcedure;
            //myCommand.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate);
            //myCommand.Parameters.Add("@contractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(sessionKeys.UID);


            //SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            //DataSet ds = new DataSet();
            //myadapter.Fill(ds);

            DataTable dt= SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeExpensesdisplay", new SqlParameter("@Date", Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate)), new SqlParameter("@contractorID", sessionKeys.UID)).Tables[0];

            //string _output = myCommand.Parameters["@out"].Value.ToString();
            GridView2.DataSource = dt;
            GridView2.DataBind();
            //myCommand.Dispose();
            //myConnection.Close();



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
        //finally
        //{
            
        //    //myConnection.Close();
        //}


    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyInsert_FooterExpenses")
        {
            ((DropDownList)GridView2.FooterRow.FindControl("ddlTitle_footerexpenses")).SelectedIndex = 0;
            ((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text = DateTime.Now.ToShortDateString();
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
                    //lblError.Visible = true;
                    //lblError.Text = "Expenses entered successfully";
                    viewButtonCode_Timeandexpenses();
                    ((TextBox)GridView2.FooterRow.FindControl("Date_footerexpenses")).Text = DateTime.Now.ToShortDateString();
                    ((TextBox)GridView2.FooterRow.FindControl("txtQty_footerexpenses")).Text = "";
                    ((DropDownList)GridView2.FooterRow.FindControl("ddlEntry_footerexpenses")).SelectedIndex = 0;
                    ((TextBox)GridView2.FooterRow.FindControl("txtNotes_footerexpenses")).Text = "";
                    ProjectReferenec_FooterExpenses = 0;
                    entrytype_FooterExpenses = 0;
                    Page.SetFocus(GridView2);
                    
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
            Response.Redirect("AdminDropDown.aspx?type=ExpensesType", false);
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
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtmyDate = (TextBox)e.Row.FindControl("Date_footerexpenses");
            txtmyDate.Text = DateTime.Now.ToShortDateString();

        }

    }
    #endregion

    #region newlyModifiedCode -- giri

    protected void btn_viewdate_Click(object sender, EventArgs e)
    {
        //if (ddlStatusupdate.SelectedValue == "2")
        //{

        //}

        viewButtonCode(Convert.ToInt32(ddlStatusupdate.SelectedValue));
        Bind_VTgrid(Convert.ToDateTime(string.IsNullOrEmpty(txtweekcommencedate.Text) ? "01/01/1900" : txtweekcommencedate.Text), sessionKeys.UID, 0);
        viewButtonCode_Timeandexpenses();
        hidDate.Value = txtweekcommencedate.Text.ToString();
        // viewButtonCode_ExsternalExpenses();
    }

    private void viewButtonCode(int status)
    {

        #region commentedOriginalCode
       
        try
        {
            string WCdate = txtweekcommencedate.Text;

           
            //SqlCommand myCommand = new SqlCommand("DN_selecttimesheetcveiw", con);
            //myCommand.CommandType = CommandType.StoredProcedure;
            //myCommand.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate);
            //myCommand.Parameters.Add("@contractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(sessionKeys.UID);
            //myCommand.Parameters.Add("@Status", SqlDbType.Int, 32).Value = status;

            SqlParameter _out = new SqlParameter("@out", SqlDbType.Int);
            _out.Direction = ParameterDirection.Output;
            //myCommand.Parameters.Add(_out);

            //SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            //myadapter.Fill(ds);

            ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_selecttimesheetcveiw", new SqlParameter("@Date", Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate)),
              new SqlParameter("@contractorID", sessionKeys.UID), new SqlParameter("@Status", status), _out);


            string _output = _out.Value.ToString();

            if (status == 4)
            {
                //Approvepannel.Visible = true;
                //Panel2.Visible = false;
                if (ds.Tables[0].Rows.Count > 1)
                {

                    if (_output == "1")
                    {
                        //  GridView1.Rows[2].Visible = false;ApproveGrid
                        //ApproveGrid.Columns[0].Visible = true;
                        //ApproveGrid.DataSource = ds;
                        //ApproveGrid.DataBind();
                        lblResultTEST.Visible = true;
                        lblResultTEST.Text = "No Approve Timesheet Data with selected Date";
                        lblstatus.Visible = true;

                    }
                    else
                    {
                        //  GridView1.FooterRow.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "Timesheet entries that have been approved.";
                        //ApproveGrid.Columns[0].Visible = true;

                        //ApproveGrid.DataSource = ds;
                        //ApproveGrid.DataBind();
                    }
                }

                else
                {
                    lblResultTEST.Visible = true;
                    lblResultTEST.Text = "No Approve Timesheet Data with selected Date";
                    lblstatus.Visible = true;
                    //lblstatus.Text=""
                }
            }
            else if (status == 2)
            {
                //  Approvepannel.Visible = false;
                //Panel2.Visible = true;
                //lblError.Visible = true;
                //lblError.Text = "Time Sheet(s) already submitted for approval";
                if (_output == "1")
                {
                    //  GridView1.Rows[2].Visible = false;ApproveGrid
                    //GridView1.Columns[0].Visible = false;
                    grdTimeSheetEntry.Visible = true;
                    grdTimeSheetEntry.DataSource = ds;
                    grdTimeSheetEntry.DataBind();

                }
                else
                {
                    //  GridView1.FooterRow.Visible = false;
                    //  Approvepannel.Visible = true;
                    //GridView1.Columns[2].Visible = false;
                    lblstatus.ForeColor = System.Drawing.Color.FromName("red");
                    //lblstatus.Text = "Time Sheet(s) already submitted for approval";
                    grdTimeSheetEntry.Visible = true;
                    grdTimeSheetEntry.DataSource = ds;
                    grdTimeSheetEntry.DataBind();

                }
            }
            else if (status == 0)
            {
                //GridView1.Columns[0].Visible = false;
                grdTimeSheetEntry.Visible = true;
                grdTimeSheetEntry.DataSource = ds;
                grdTimeSheetEntry.DataBind();

            }

            else
            {
                //Approvepannel.Visible = false;
                //Panel2.Visible = true;

                if (_output == "1")
                {
                    //  GridView1.Rows[2].Visible = false;ApproveGrid
                    // GridView1.Columns[0].Visible = false;
                    grdTimeSheetEntry.Visible = true;
                    grdTimeSheetEntry.DataSource = ds;
                    grdTimeSheetEntry.DataBind();

                }
                else
                {
                    //  GridView1.FooterRow.Visible = false;
                    // GridView1.Columns[0].Visible = false;
                    //ApproveGrid.Columns[2].Visible = true;
                    grdTimeSheetEntry.Visible = true;
                    grdTimeSheetEntry.DataSource = ds;
                    grdTimeSheetEntry.DataBind();

                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
        if (grdTimeSheetEntry.Rows.Count > 0)
        {
            // Label1.Visible = true;
            //status();
        }
        #endregion commented Originalcode
        
    }

    public void GetDate()
    {
        try
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetDate");
            while (dr.Read())
            {
                txtweekcommencedate.Text = dr["today"].ToString();

            }
            dr.Close();

            //viewButtonCode(Convert.ToInt32(ddlStatusupdate.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

   
    #endregion

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

    #region "Permission Code Here"
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    vis = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    vis = false;

                    // Disable();

                }

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void CheckUserRole()
    {

        if (sessionKeys.SID != 1)
        {
            int role = 0;
            role = Admin.CheckLoginUserPermission(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }
            role = Admin.GetTeamID(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }

        }

    }
    private void Disable()
    {
        btn_viewdate.Enabled = false;
        imgBtnAdd.Enabled = false;
        imgSubmit.Enabled = false;

    }
    #endregion 

    #region customer User mail

    //private int GetCustomerUser(int ProjectReference)
    //{
    //    projectTaskDataContext pd = new projectTaskDataContext();

    //    var cu = from p in pd.ProjectDefaults

    //}
    public IEnumerable<CustomerTimesheetMail> GetCustomerUsermailData(int wcdateid)
    {
        List<CustomerTimesheetMail> customerTimesheetMail = new List<CustomerTimesheetMail>();
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "TimesheetEntry_CustomerUserMail", new SqlParameter("@wcdateid", wcdateid)).Tables[0];
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var emp = new CustomerTimesheetMail
                    {
                        Project = dt.Rows[T1_cnt]["Project"].ToString(),
                        Contractor = dt.Rows[T1_cnt]["ContractorName"].ToString(),
                        Hours = double.Parse(dt.Rows[T1_cnt]["Hours"].ToString()),
                        ApprovType = dt.Rows[T1_cnt]["ApproveType"].ToString(),
                        CustomerUser = int.Parse(dt.Rows[T1_cnt]["Customeruser"].ToString()),
                        CustomerEmail = dt.Rows[T1_cnt]["Email"].ToString(),
                        CustomerName = dt.Rows[T1_cnt]["CustomerName"].ToString(),
                        Task = dt.Rows[T1_cnt]["Task"].ToString()
                        
                    };
                    customerTimesheetMail.Add(emp);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return customerTimesheetMail;
    }

    private string SendCustomerMail(int wcdateid)
    {
        Email e =  new Email();
        string retval = string.Empty;
        customerTimesheetMail = GetCustomerUsermailData(wcdateid);
        string MailFormat= Deffinity.MailFormat.MailCss("Timesheet");

        string mailContent = string.Empty;

        var CustomerUser = (from r in customerTimesheetMail where r.CustomerUser > 0 select r.CustomerUser).Distinct();
        //subprogramme column
        foreach (int cu in CustomerUser)
         {

            
             string strTop = string.Empty;
             strTop = MailFormat + string.Format("<table align='center' width='600' style='border:5px solid #b3b3b3; margin-top:10px;' cellspacing='0' cellpadding='0'>  <tr>    <td height='30' valign='top' ><img src='{0}/media/deffinity_logo.gif'  alt=''/></td><td><table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'><tr> <td class='hdr1'>Timesheet</td> </tr></table> </td></tr> <tr>  <td height='9' colspan='2' ><img src='{0}/images/border.gif' style='height:5px;width:580px' alt=''> </img></td>  </tr>  <tr>    <td colspan='2'>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
             strTop = strTop + "<table> ";
             strTop = strTop + "<tr> <td>";
             strTop = strTop + string.Format("Dear <b>{0}</b></tr> </td>", customerTimesheetMail.Where(p => p.CustomerUser == cu).Select(p => p.CustomerName).FirstOrDefault());
             strTop = strTop + string.Format("<tr> <td>The following Timesheet has been submitted for approval for week commencing  <b>{0}:</td></tr><tr> <td>",txtweekcommencedate.Text.Trim());

             strTop = strTop + "<table style='width:100%'> ";
             strTop = strTop + "<tr style='background-color:#d1e7ed;color:#777;padding:3px;'> <td> <b>Resource</b> </td><td> <b>Project</b> </td><td><b> Task</b> </td><td><b> Entry Type</b> </td><td><b> Hours</b> </td></tr>";

             var CustomerUserDetails = (from r in customerTimesheetMail where r.CustomerUser == cu select r);

             foreach (CustomerTimesheetMail cud in CustomerUserDetails)
             {
                 strTop = strTop + string.Format("<tr><td>{0}</td><td>{1}</td><td>{4}</td><td>{2}</td><td>{3}</td> </tr>",cud.Contractor,cud.Project,cud.ApprovType,string.Format("{0:F2}", cud.Hours),cud.Task);
             }


             strTop = strTop + "</table> ";
                
            
             strTop = strTop + " </td></tr>";
             strTop = strTop + string.Format("<tr> <td> Please <a href='{0}'> click here </a> to access the system. </td></tr>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
             strTop = strTop + "<tr> <td>  Thank you. </td></tr>";
             strTop = strTop + string.Format("<tr> <td> <a href='{0}'> {0} </a>  </td></tr>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
             strTop = strTop + "</table>";
             strTop = strTop + "</td></tr></table> ";
            strTop = strTop + "</html>";
            string toemail = customerTimesheetMail.Where(p => p.CustomerUser == cu).Select(p => p.CustomerEmail).FirstOrDefault();
            e.SendingMail(toemail, "Timesheet(s) Submitted for approval", strTop);

         }

       

        return retval;
    }

    #endregion
    public class CustomerTimesheetMail
    {
        public string Project { get; set; }
        public string Contractor { get; set; }
        public double Hours { get; set; }
        public string ApprovType { get; set; }
        public int CustomerUser { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string Task { get; set; }

    }

    #region Timesheet apporver mail checking

    private bool ApproverEmailAlertCheck(int ApproverID)
    {
        TimeSheetDataContext td = new TimeSheetDataContext();
        bool retval = true;
        try
        {
            var rval = (from t in td.TimesheetApproverEmails
                        where t.ApproverID == ApproverID
                        select t).FirstOrDefault();
            if (rval != null)
            {
                retval = rval.Enable.Value;
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        
        return retval;
    }


    #endregion
}
