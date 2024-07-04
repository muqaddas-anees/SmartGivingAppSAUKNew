using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Timesheets
{
    public partial class TimesheetReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
                BindGrid();

            }
        }
        private void BindUsers()
        {
            try
            {
                var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                             orderby p.ContractorName
                             select new { ID = p.ID, Text = p.ContractorName }).ToList();
                ddlUsers.DataSource = jlist;
                ddlUsers.DataTextField = "Text";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string ToHours(string GetHours)
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
        public string ChangeTimeDisplay(string GetHours)
        {

            string GetActivity = "";
            try
            {
                char[] comm1 = { ':' };
                string[] displayTime = GetHours.Split(comm1);


                GetActivity = displayTime[0] + ":" + displayTime[1];


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetActivity;
        }
        private void BindGrid()
        {
            try
            {
                IQueryable<TimesheetMgt.Entity.v_timesheetentry> vlist;// = new List<TimesheetMgt.Entity.v_timesheetentry>();
                vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID);
                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()) && ddlUsers.SelectedValue != "0")
                {
                    vlist = vlist.Where(o => o.DateEntered >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.DateEntered <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue));
                }
                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = vlist.Where(o => o.DateEntered >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.DateEntered <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())));
                if (ddlUsers.SelectedValue != "0")
                    vlist = vlist.Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue));
                //else
                //    vlist = vlist.OrderByDescending(o => o.TimeSheetStatusName).OrderByDescending(o => o.PrimeApproverName).ToList();
               
                GridPartner.DataSource = vlist.OrderByDescending(o=>o.DateEntered).ToList();
                GridPartner.DataBind();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btn_viewdate_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                // string company = lblContractorName.SelectedItem.Text;
                // string searchtext = Request.Form["ticketno"];
                // string name = ddlRequester.SelectedItem.Text;
                // string status = ddlStatus.SelectedItem.Text;
                // string loggedStartDate = txtLoggedStartDate.Text;
                // string loggedEndDate = txtLoggedEndDate.Text;
                // string scheduleDate = txtScheduledDate.Text;


                // List<Jqgrid> reportList = new List<Jqgrid>();

                // reportList = FLSDetailsBAL.GetFLSReportList();

                // if (!string.IsNullOrEmpty(loggedStartDate) && !string.IsNullOrEmpty(loggedEndDate))
                //     reportList = reportList.Where(r => r.LoggedDate.Date >= (Convert.ToDateTime(loggedStartDate)) && r.LoggedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
                // if (!string.IsNullOrEmpty(loggedStartDate) || !string.IsNullOrEmpty(loggedEndDate))
                // {
                //     if (!string.IsNullOrEmpty(loggedStartDate))
                //         reportList = reportList.Where(r => r.LoggedDate.Date >= (Convert.ToDateTime(loggedStartDate))).ToList();
                //     if (!string.IsNullOrEmpty(loggedEndDate))
                //         reportList = reportList.Where(r => r.LoggedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
                // }

                // if (searchtext.Length > 0)
                // {
                //     reportList = reportList.Where(p => (
                //         (p.RequesterName != null ? p.RequesterName.ToLower().Contains(searchtext.ToLower()) : false)
                //|| (p.RequestersAddress != null ? p.RequestersAddress.ToLower().Contains(searchtext.ToLower()) : false)
                // || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
                //  || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
                //   || (p.RequestersCity != null ? p.RequestersCity.ToLower().Contains(searchtext.ToLower()) : false)
                //    || (p.Details != null ? p.Details.ToLower().Contains(searchtext.ToLower()) : false)
                //     || (p.AssignedTechnician != null ? p.AssignedTechnician.ToLower().Contains(searchtext.ToLower()) : false)
                //      || (p.TypeofRequest != null ? p.TypeofRequest.ToLower().Contains(searchtext.ToLower()) : false)

                //        )).ToList();
                // }

                ////
                List<TimesheetMgt.Entity.v_timesheetentry> vlist = new List<TimesheetMgt.Entity.v_timesheetentry>();

                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()) && ddlUsers.SelectedValue != "0")
                {
                    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.DateEntered >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.DateEntered <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.DateEntered).ToList();
                }
                else if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.DateEntered >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.DateEntered <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.DateEntered).ToList();
                else if (ddlUsers.SelectedValue != "0")
                    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.DateEntered).ToList();
                else
                    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).OrderByDescending(o => o.TimeSheetStatusName).OrderByDescending(o => o.PrimeApproverName).ToList();
              //  GridPartner.DataSource = vlist.ToList();

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Timesheet Report");



                // Title
                ws.Cell("A1").Value = "Timesheet Report"; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("A2").Value = "Users";
                //ws.Cell("B2").Value = "Comapany";
                ws.Cell("B2").Value = "Date";
                ws.Cell("C2").Value = "Job";
                ws.Cell("D2").Value = "From Time";
                ws.Cell("E2").Value = "To Time";
                ws.Cell("F2").Value = "Hours";
                ws.Cell("G2").Value = "Approver";
                ws.Cell("H2").Value = "	Status";
                int i = 3;
                foreach (var item in vlist)
                {
                    ws.Cell("A" + i.ToString()).Value = "" + item.ResourceName;
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value = item.DateEntered;
                    ws.Cell("C" + i.ToString()).Value = item.ProjectTitle;
                    ws.Cell("D" + i.ToString()).Value = item.fromtime;
                    ws.Cell("B" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                    ws.Cell("E" + i.ToString()).Value = item.totime;
                    // ws.Cell("E" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetStringTimeformat();
                    ws.Cell("F" + i.ToString()).Value = item.Hours.ToString();
                    ws.Cell("F" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetTimeformat();
                    ws.Cell("G" + i.ToString()).Value = item.PrimeApproverName;
                    ws.Cell("H" + i.ToString()).Value = item.TimeSheetStatusName;
                   

                    i = i + 1;
                }

                // From worksheet
                var rngTable = ws.Range("A1:H2");

                var rngHeaders = rngTable.Range("A2:H2");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                rngTable.Cell(1, 1).Style.Font.FontSize = 15;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Row(1).Merge();

                ws.Columns(1, 9).AdjustToContents();

                string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/SAMReports");

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                wb.SaveAs(path + "\\" + "TimesheetReport.xlsx");
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "TimesheetReport.xlsx");
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=TimesheetReport.xlsx");
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        
    }

        protected void GridPartner_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridPartner.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GridPartner_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //v_timesheetentry objList = e.Row.DataItem as v_timesheetentry;
                    //if (objList != null)
                    //{

                    //    Label lblGridEndTime = (Label)e.Row.FindControl("lblGridEndTime");
                    //    Label lblGridStartTime = (Label)e.Row.FindControl("lblGridStartTime");
                    //    //Button btnStop = (Button)e.Row.FindControl("btnStop");
                    //    //index 19,20 are StartTime and end time
                    //    LogExceptions.LogException("todate: " + objList.totime.ToString());
                    //    if (objList.totime.HasValue && objList.fromtime.HasValue)
                    //    {
                    //        lblGridEndTime.Text = objList.totime.Value.ToString().Substring(0, 5);
                    //        lblGridStartTime.Text = objList.fromtime.Value.ToString().Substring(0, 5);
                    //        if (lblGridStartTime.Text == lblGridEndTime.Text)
                    //        {
                    //            lblGridEndTime.Text = "";
                    //            lblGridStartTime.Text = "Started at " + objList.fromtime.Value.ToString().Substring(0, 5);
                    //            //btnStop.Visible = true;
                    //        }
                    //        else
                    //        {
                    //            //btnStop.Visible = false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        lblGridEndTime.Text = string.Empty;
                    //        lblGridStartTime.Text = string.Empty;
                    //    }


                    //}


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}