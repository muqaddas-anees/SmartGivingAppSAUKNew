using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Timesheets
{
    public partial class AddTimesheets : System.Web.UI.Page
    {
        int entrytype = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    txtDate.Text = DateTime.Now.ToShortDateString();
                    txtweekcommencedate.Text = DateTime.Now.ToShortDateString();
                    BindJobs();
                    BindSmartTechs();
                    
                    viewButtonCode(1);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindSmartTechs()
        {
            try
            {
                if (sessionKeys.SID == 1)
                {
                    var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                                 orderby p.ContractorName
                                 select new { ID = p.ID, Text = p.ContractorName }).ToList();

                    ddlSmartTech.DataSource = jlist;
                }
                else
                {
                    var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                                 where p.ID == sessionKeys.UID
                                 orderby p.ContractorName
                                 select new { ID = p.ID, Text = p.ContractorName }).ToList();

                    ddlSmartTech.DataSource = jlist;
                }


                ddlSmartTech.DataTextField = "Text";
                ddlSmartTech.DataValueField = "ID";
                ddlSmartTech.DataBind();
                ddlSmartTech.Items.Insert(0, new ListItem("Please select...", "0"));

                //set defaultvalue
                ddlSmartTech.SelectedValue = sessionKeys.UID.ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindJobs()
        {
            try
            {
                var jlist = FLSDetailsBAL.GetActiveJobDropdownLists(sessionKeys.PortfolioID).ToList();
                ddlJobs.DataSource = jlist;
                ddlJobs.DataTextField = "jobtitle";
                ddlJobs.DataValueField = "jobid";
                ddlJobs.DataBind();
                ddlJobs.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btn_viewdate_Click(object sender, EventArgs e)
        {
            try
            {

                viewButtonCode(1);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       

        protected void imgBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "0")
                {
                    AddTimesheetEntry(Convert.ToInt32(ddlSmartTech.SelectedValue), Convert.ToInt32(ddlJobs.SelectedValue), Convert.ToDateTime(txtDate.Text.Trim()), TimeSpan.Parse(txtFromTime.Text.Trim()).ToString().Substring(0, 5), txtToTime.Text.Trim().Length >0?( TimeSpan.Parse(txtToTime.Text.Trim()).ToString().Substring(0, 5)): TimeSpan.Parse(txtFromTime.Text.Trim()).ToString().Substring(0, 5));

                }
                else
                {
                    UpdateTimesheetEntry(Convert.ToInt32(ddlSmartTech.SelectedValue),Convert.ToInt32(hid.Value), Convert.ToInt32(ddlJobs.SelectedValue), Convert.ToDateTime(txtDate.Text.Trim()), TimeSpan.Parse(txtFromTime.Text.Trim()).ToString().Substring(0, 5), txtToTime.Text.Trim().Length > 0 ? (TimeSpan.Parse(txtToTime.Text.Trim()).ToString().Substring(0, 5)) : TimeSpan.Parse(txtFromTime.Text.Trim()).ToString().Substring(0, 5));
                }
                viewButtonCode(1);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region Grid functions
        protected bool GetTimeSheetStatusCheck(string statusid)
        {
            bool st = false;
            try
            {
                //check the status is approve or submitted
                if ((int.Parse(statusid) == 1))
                    st = true;
                if ((int.Parse(statusid) == 3))
                    st = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return st;

        }

        protected bool GetTimeSheetStatusDeclineCheck(string statusid)
        {
            bool st = false;
            try
            {
                //check the status is approve or submitted
                if ((int.Parse(statusid) == 3))
                    st = true;
                else
                    st = false;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return st;

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
            catch (Exception ex)
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
        #endregion
        protected void grdTimeSheetEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "edit1")
            {
               
                hid.Value = e.CommandArgument.ToString();
                try
                {
                    using (TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext())
                    {
                        var t = TimeSheetEntry.TimesheetEntries.Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (t != null)
                        {
                            txtDate.Text = t.DateEntered.HasValue ? t.DateEntered.Value.ToShortDateString() : string.Empty;
                            txtFromTime.Text = t.fromtime.HasValue ? t.fromtime.Value.ToString().Substring(0, 5) : "";
                            txtToTime.Text = t.totime.HasValue ? t.totime.Value.ToString().Substring(0, 5) : "";
                            if (txtFromTime.Text == txtToTime.Text)
                                txtToTime.Text = string.Empty;
                            ddlJobs.SelectedValue = t.ProjectReference.HasValue ? t.ProjectReference.Value.ToString() : "0";
                            ddlSmartTech.SelectedValue = t.ContractorID.HasValue ? t.ContractorID.Value.ToString() : "0";
                            lblOptions.Text = "Update Timesheet";
                            imgBtnAdd.Text = "Submit";
                            btnStart.Visible = false;
                            mdlAddTimesheet.Show();
                        }

                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                   
            }
            if (e.CommandName == "stopped")
            {

                hid.Value = e.CommandArgument.ToString();
                try
                {
                    using (TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext())
                    {
                        var t = TimeSheetEntry.TimesheetEntries.Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (t != null)
                        {

                            txtToTime.Text = t.totime.HasValue ? t.totime.Value.ToString().Substring(0, 5) : "";
                            //ddlJobs.SelectedValue = t.ProjectReference.HasValue ? t.ProjectReference.Value.ToString() : "0";
                            var fromtime = t.fromtime;
                            var totime = t.totime;
                            t.totime = (  DateTime.Now.TimeOfDay - t.fromtime).Value.Add(fromtime.Value);
                            TimeSpan tmp = TimeSpan.Parse(t.totime.ToString()) - TimeSpan.Parse(t.fromtime.ToString());
                            var h = tmp.Hours;
                            var m = tmp.Minutes;
                            //li.Item4.Subtract(li.Item3);
                            TimeSpan span1 = TimeSpan.FromMinutes(1);
                            TimeSpan to_temp_end;
                            if (TimeSpan.Parse(t.totime.ToString()).ToString().Substring(0, 5) == "23:59")
                            {
                                to_temp_end = tmp.Add(span1);
                            }
                            else
                            {
                                to_temp_end = tmp;
                            }
                            string time = string.Empty;
                            if (to_temp_end.Minutes < 10)
                            {
                                time = string.Format("{0}.0{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);
                            }
                            else
                                time = string.Format("{0}.{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);


                            t.Hours = decimal.Parse(time);
                            TimeSheetEntry.SubmitChanges();
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                            viewButtonCode(1);
                        }

                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

            }
            else if(e.CommandName == "del")
            {
                using (TimeSheetDataContext TimeSheetEntry = new TimeSheetDataContext())
                {
                    string id = e.CommandArgument.ToString();
                    TimeSheetEntry.DN_TimesheetEntryDelete(int.Parse(id));
                    lblMsg.Visible = true;
                    lblMsg.Text = "Deleted Successfully";
                    //FillCalender();
                    viewButtonCode(1);
                }
            }
        }

        protected void grdTimeSheetEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TimesheetEntrySelectDayViewResult objList = e.Row.DataItem as TimesheetEntrySelectDayViewResult ;
                    if (objList != null)
                    {
                        if (objList.ID.ToString() == "-99")
                        {
                            e.Row.Visible = false;
                        }
                        else
                        {
                            Label lblGridEndTime = (Label)e.Row.FindControl("lblGridEndTime");
                            Label lblGridStartTime = (Label)e.Row.FindControl("lblGridStartTime");
                            Button btnStop = (Button)e.Row.FindControl("btnStop");
                            //index 19,20 are StartTime and end time
                            if (objList.fromtime.HasValue)
                            {
                                if (objList.fromtime.HasValue && objList.totime.HasValue)
                                {
                                    lblGridEndTime.Text = objList.totime.Value.ToString().Substring(0, 5);
                                    lblGridStartTime.Text = objList.fromtime.Value.ToString().Substring(0, 5);
                                    if(lblGridStartTime.Text == lblGridEndTime.Text)
                                    {
                                        lblGridEndTime.Text = "";
                                        lblGridStartTime.Text = "Started at " + objList.fromtime.Value.ToString().Substring(0, 5);
                                        btnStop.Visible = true;
                                    }
                                    else
                                    {
                                        btnStop.Visible = false; 
                                    }
                                }
                                else
                                {
                                    lblGridEndTime.Text = string.Empty;
                                    lblGridStartTime.Text = string.Empty;
                                }
                            }
                            else
                            {
                                lblGridEndTime.Text = string.Empty;
                                lblGridStartTime.Text = string.Empty;
                            }
                        }

                    }
                    }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddTimesheet_Click(object sender, EventArgs e)
        {
            lblOptions.Text = "Add Timesheet";
            hid.Value = "0";
            ddlJobs.SelectedValue = "0";
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtFromTime.Text = "";
            txtToTime.Text = "";
            imgBtnAdd.Text = "Submit";
            btnStart.Visible = true;
            mdlAddTimesheet.Show();
        }

        private void viewButtonCode(int status)
        {

            #region commentedOriginalCode

            try
            {

                using (TimesheetMgt.DAL.TimeSheetDataContext tm = new TimesheetMgt.DAL.TimeSheetDataContext())
                {
                    int? _out = 0;
                    var date = Convert.ToDateTime(string.IsNullOrEmpty(txtweekcommencedate.Text.Trim()) ? "01/01/1900" : txtweekcommencedate.Text.Trim());
                    //
                    var rlist = tm.TimesheetEntrySelectDayView(date, sessionKeys.UID, 1, sessionKeys.PortfolioID, true, ref _out).ToList();
                    //string WCdate = txtweekcommencedate.Text;
                    //SqlCommand myCommand = new SqlCommand("DN_selecttimesheetcveiw", con);
                    //myCommand.CommandType = CommandType.StoredProcedure;
                    //myCommand.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate);
                    //myCommand.Parameters.Add("@contractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(sessionKeys.UID);
                    //myCommand.Parameters.Add("@Status", SqlDbType.Int, 32).Value = status;
                    //SqlParameter _out = new SqlParameter("@out", SqlDbType.Int);
                    //_out.Direction = ParameterDirection.Output;
                    //myCommand.Parameters.Add(_out);
                    //SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
                    // DataSet ds = new DataSet();
                    //myadapter.Fill(ds);

                    //ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_selecttimesheetcveiw", new SqlParameter("@Date", Convert.ToDateTime(string.IsNullOrEmpty(WCdate) ? "01/01/1900" : WCdate)),
                    //  new SqlParameter("@contractorID", sessionKeys.UID), new SqlParameter("@Status", status), new SqlParameter("@PortfolioID", sessionKeys.PortfolioID), _out);
                    string _output = _out.Value.ToString();

                    if (status == 4)
                    {
                        if (rlist.Count() > 1)
                        {

                            if (_output == "1")
                            {

                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "Timesheet entries that have been approved.";
                            }
                        }
                    }
                    else if (status == 2)
                    {
                        if (_output == "1")
                        {
                            //  GridView1.Rows[2].Visible = false;ApproveGrid
                            //GridView1.Columns[0].Visible = false;
                            grdTimeSheetEntry.Visible = true;
                            grdTimeSheetEntry.DataSource = rlist;
                            grdTimeSheetEntry.DataBind();
                        }
                        else
                        {
                            grdTimeSheetEntry.Visible = true;
                            grdTimeSheetEntry.DataSource = rlist;
                            grdTimeSheetEntry.DataBind();

                        }
                    }
                    else if (status == 0)
                    {
                        //GridView1.Columns[0].Visible = false;
                        grdTimeSheetEntry.Visible = true;
                        grdTimeSheetEntry.DataSource = rlist;
                        grdTimeSheetEntry.DataBind();
                    }
                    else
                    {

                        if (_output == "1")
                        {
                            grdTimeSheetEntry.Visible = true;
                            grdTimeSheetEntry.DataSource = rlist;
                            grdTimeSheetEntry.DataBind();
                        }
                        else
                        {
                            grdTimeSheetEntry.Visible = true;
                            grdTimeSheetEntry.DataSource = rlist;
                            grdTimeSheetEntry.DataBind();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            if (grdTimeSheetEntry.Rows.Count == 0)
            {
                mdlAddTimesheet.Show();
                // Label1.Visible = true;
                //status();
            }
            #endregion commented Originalcode

        }

        private int AddTimesheetEntry(int userid, int pref,DateTime entiryDate,   string FromTime, string ToTime, string notes="", int entrytype=1, int site=0, int task=0, double hours=0, int ProfileID=0, double ratemultiplier=0, double amount=0, bool IsNightShift=false, bool IsOnCallOut=false   )
        {
            int output = 0;
            if (hours <= 24.0)
            {
                if (FromTime.Length > 1 && string.IsNullOrEmpty(ToTime))
                {
                    ToTime = FromTime;
                }
                TimeSpan tmp = TimeSpan.Parse(ToTime) - TimeSpan.Parse(FromTime);
                var h = tmp.Hours;
                var m = tmp.Minutes;
                //li.Item4.Subtract(li.Item3);
                TimeSpan span1 = TimeSpan.FromMinutes(1);
                TimeSpan to_temp_end;
                if (TimeSpan.Parse(ToTime).ToString().Substring(0, 5) == "23:59")
                {
                    to_temp_end = tmp.Add(span1);
                }
                else
                {
                    to_temp_end = tmp;
                }
                string time = string.Empty;
                if (to_temp_end.Minutes < 10)
                {
                    time = string.Format("{0}.0{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);
                }
                else
                    time = string.Format("{0}.{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);
                //if ((ddlProjectTile.SelectedItem.Value.ToString() != "0") || (txtServiceRequest.Text != ""))
                //{
                var db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_TimesheetEntry");
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
                db.AddInParameter(cmd, "@Activity", DbType.String, "s");
                db.AddInParameter(cmd, "@AssignType", DbType.Int32, 0);
                db.AddInParameter(cmd, "@ResourceID", DbType.Int32, userid);
                db.AddInParameter(cmd, "@entryid", DbType.Int32, entrytype);
                db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, Convert.ToDateTime(string.Format("{0} {1}", entiryDate.ToShortDateString(), DateTime.Now.ToShortTimeString())));
                db.AddInParameter(cmd, "@hours", DbType.Double, time);
                db.AddInParameter(cmd, "@notes", DbType.String, notes);
                db.AddInParameter(cmd, "@SiteID", DbType.Int32, site);
                db.AddInParameter(cmd, "@ProjectTaskID", DbType.Int32, task);
                db.AddInParameter(cmd, "@ProfileID", DbType.Int32, ProfileID);
                db.AddInParameter(cmd, "@RateMultiplier", DbType.Double, ratemultiplier);
                db.AddInParameter(cmd, "@Amount", DbType.Double, amount);
                db.AddInParameter(cmd, "@IsNightShift", DbType.Boolean, false);
                db.AddInParameter(cmd, "@IsOnCallOut", DbType.Boolean, false);
                if (string.IsNullOrEmpty(FromTime) && FromTime != "00:00")
                    db.AddInParameter(cmd, "@fromtime", DbType.Time, DBNull.Value);
                else
                    db.AddInParameter(cmd, "@fromtime", DbType.Time, TimeSpan.Parse(FromTime).ToString().Substring(0, 5));

              

                if (string.IsNullOrEmpty(ToTime) && ToTime != "00:00")
                    db.AddInParameter(cmd, "@totime", DbType.Time, DBNull.Value);
                else
                    db.AddInParameter(cmd, "@totime", DbType.Time, TimeSpan.Parse(ToTime).ToString().Substring(0, 5));
                db.AddOutParameter(cmd, "@output", DbType.Int32, 0);
                db.ExecuteNonQuery(cmd);
                output = (int)db.GetParameterValue(cmd, "@output");

                cmd.Dispose();
                //lblMsg.Visible = true;
                if (output == 0)
                {
                    lblError.Text = "Error While inserting";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 1)
                {

                    lblMsg.Text = "Timesheet entered successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;

                }

                else if (output == 2)
                {
                    lblError.Text = "You cannot add an entry to a timesheet that has been submitted for approval";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 3)
                {
                    lblError.Text = "Timesheet entry already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 4)
                {
                    lblError.Text = "Vacation request already exists for this date";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 10)
                {
                    lblError.Text = "You have exceeded 24 hours for the date entered";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 5)
                {
                    lblError.Text = "Please enter date with in the current week.";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (output == 11)
                {
                    lblError.Text = "Please check the time you have entered. You have an entry this week that overlaps with the time you have entered.";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                //}
                //else
                //{
                //    lblMsg.Visible = true;
                //    lblMsg.Text = "Please Select Project / Service Request";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}

            }
            return output;
        }


        private int UpdateTimesheetEntry(int userid,int ID,  int pref,  DateTime entryDate, string FromTime, string ToTime, int entrytype = 1, string notes="", int site=0, int task=0, double hours=0, int ProfileID = 0, double ratemultiplier = 0, double amount = 0, bool IsNightShift = false, bool IsOnCallOut = false)
        {
            int? output = 0;
            using (TimeSheetDataContext timeSheetEntry = new TimeSheetDataContext())
            {
                if (FromTime.Length > 1 && string.IsNullOrEmpty(ToTime))
                {
                    ToTime = FromTime;
                }
                TimeSpan tmp = TimeSpan.Parse(ToTime) - TimeSpan.Parse(FromTime);
                var h = tmp.Hours;
                var m = tmp.Minutes;
                //li.Item4.Subtract(li.Item3);
                TimeSpan span1 = TimeSpan.FromMinutes(1);
                TimeSpan to_temp_end;
                if (TimeSpan.Parse(ToTime).ToString().Substring(0, 5) == "23:59")
                {
                    to_temp_end = tmp.Add(span1);
                }
                else
                {
                    to_temp_end = tmp;
                }
                string time = string.Empty;
                if (to_temp_end.Minutes < 10)
                {
                    time = string.Format("{0}.0{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);
                }
                else
                    time = string.Format("{0}.{1}", ((int)to_temp_end.TotalHours), to_temp_end.Minutes);


                timeSheetEntry.DN_TimesheetEntryupdate(ID, pref, string.Empty, 0, userid,
                            entrytype, entryDate,Convert.ToDouble(time), notes,
                            site, task, TimeSpan.Parse(string.IsNullOrEmpty(FromTime) ? "00:00" : FromTime), TimeSpan.Parse(string.IsNullOrEmpty(ToTime) ? "00:00" : ToTime), ref output);

                if (output == 10)
                {
                    lblMsg.Text = "You have exceeded 24 hours for the date entered";
                }
                else if (output == 0)
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Error While Timesheet Updation";
                }
                else if (output == 1)
                {
                    lblMsg.Text = "Timesheet updated successfully";
                }
                else if (output == 2)
                {
                    lblMsg.Text = "Week is  Updated ";
                }
                else if (output == 4)
                {
                    lblMsg.Text = "Vacation request already exists for this date";
                }
                else if (output == 5)
                {

                    lblMsg.Text = "Please enter date with in the current week.";
                }
                else
                {
                    lblMsg.Text = "New Timesheet is inserted";
                }
            }
            return output.Value;
            
        }

        public DateTime GetDateTime(string d_date, string t_time, string t_am_pm)
        {

            string s = string.Format("{0} {1} {2}", d_date, t_time, t_am_pm);
            DateTimeFormatInfo fi = new CultureInfo("en-GB", false).DateTimeFormat;
            DateTime myDate = DateTime.ParseExact(s, "dd/MM/yyyy hh:mm:ss tt", fi);

            return myDate;
        }

        public string GetAm_PM(DateTime d_datetime)
        {
            string s = d_datetime.ToString("tt");
            return s;
        }

    }
}