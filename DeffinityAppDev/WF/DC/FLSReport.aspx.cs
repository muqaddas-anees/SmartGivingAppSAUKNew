using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DC.BLL;
using DC.Entity;
using ClosedXML.Excel;
using System.IO;

public partial class DC_FLSReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPageHead.Text = Resources.DeffinityRes.ServiceDeskReport;
        if (!IsPostBack)
        {
            //ddlRequester.SelectedValue = sessionKeys.PortfolioID;
            ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();

            txtLoggedStartDate.Text = Deffinity.Utility.StartDateOfMonth(DateTime.Now).AddMonths(-5).ToShortDateString();
            txtLoggedEndDate.Text = Deffinity.Utility.EndDateOfMonth(DateTime.Now).ToShortDateString();
        }
            
    }
    [WebMethod(EnableSession = true)]
    public static object Get(string company, string callid, string name, string status, string loggedStartDate,string loggedEndDate, string scheduledate, string site, string department, string technician, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            List<Jqgrid> reportList = new List<Jqgrid>();
            //if (company != "[Loading company...]" && company != "Please Select...")
            //{
            reportList = FLSDetailsBAL.GetFLSReportList();
            //if (company != "All" && company != "[Loading ...]")
            //    reportList = reportList.Where(r => r.ComapanyName == company).ToList();
            //if (!string.IsNullOrEmpty(callid))
            //    reportList = reportList.Where(r => r.CallID.ToString() == callid).ToList();
            //if (name != "All" && name != "[Loading ...]" && name != "")
            //    reportList = reportList.Where(r => r.RequesterName == name).ToList();
            
            if (!string.IsNullOrEmpty(loggedStartDate) && !string.IsNullOrEmpty(loggedEndDate))
                reportList = reportList.Where(r => r.LoggedDate.Date >= (Convert.ToDateTime(loggedStartDate)) && r.LoggedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
            if (!string.IsNullOrEmpty(loggedStartDate) || !string.IsNullOrEmpty(loggedEndDate))
            {
                if (!string.IsNullOrEmpty(loggedStartDate))
                    reportList = reportList.Where(r => r.LoggedDate >= (Convert.ToDateTime(loggedStartDate))).ToList();
                if (!string.IsNullOrEmpty(loggedEndDate))
                    reportList = reportList.Where(r => r.LoggedDate <= (Convert.ToDateTime(loggedEndDate))).ToList();
            }

            String searchtext = callid;
            if (searchtext.Length > 0)
            {
                reportList = reportList.Where(p => (
                    (p.RequesterName != null ? p.RequesterName.ToLower().Contains(searchtext.ToLower()) : false)
           || (p.RequestersAddress != null ? p.RequestersAddress.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
             || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
              || (p.RequestersCity != null ? p.RequestersCity.ToLower().Contains(searchtext.ToLower()) : false)
               || (p.Details != null ? p.Details.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.AssignedTechnician != null ? p.AssignedTechnician.ToLower().Contains(searchtext.ToLower()) : false)
                 || (p.TypeofRequest != null ? p.TypeofRequest.ToLower().Contains(searchtext.ToLower()) : false)
                  || (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false)

                   )).ToList();
            }
            //if (!string.IsNullOrEmpty(scheduledate))
            //reportList = reportList.Where(r => r.AssignedTechnician).ToList();
            //if (site != "All" && site != "[Loading ...]")
            //    reportList = reportList.Where(r => r.SiteName == site).ToList();
            //if (department != "All" && department != "[Loading ...]" && department != "")
            //    reportList = reportList.Where(r => r.DepartmentName == department).ToList();
            //if (technician != "All" && technician != "[Loading ...]" && department != "")
            //    reportList = reportList.Where(r => r.TechnecianName == technician).ToList();

            //if (jtSorting.Equals("ComapanyName ASC"))
            //{
            //    reportList = reportList.OrderBy(o => o.ComapanyName).ToList();
            //}
            //else if (jtSorting.Equals("ComapanyName DESC"))
            //{
            //    reportList = reportList.OrderByDescending(o => o.ComapanyName).ToList();
            //}
            if (jtSorting.Equals("CallID ASC"))
            {
                reportList = reportList.OrderBy(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("CallID DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.CallID).ToList();
            }

            if (jtSorting.Equals("RequesterName ASC"))
            {
                reportList = reportList.OrderBy(o => o.RequesterName).ToList();
            }
            else if (jtSorting.Equals("RequesterName DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.RequesterName).ToList();
            }

            if (jtSorting.Equals("StatusName ASC"))
            {
                reportList = reportList.OrderBy(o => o.Status).ToList();
            }
            else if (jtSorting.Equals("StatusName DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.Status).ToList();
            }

            if (jtSorting.Equals("LoggedDate ASC"))
            {
                reportList = reportList.OrderBy(o => o.LoggedDate).ToList();
            }
            else if (jtSorting.Equals("LoggedDate DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.LoggedDate).ToList();
            }
            if (jtSorting.Equals("ScheduleDate ASC"))
            {
                reportList = reportList.OrderBy(o => o.ScheduledEndDateTime).ToList();
            }
            else if (jtSorting.Equals("ScheduleDate DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.ScheduledEndDateTime).ToList();
            }
           
           
            if (jtSorting.Equals("AssignedTechnician ASC"))
            {
                reportList = reportList.OrderBy(o => o.AssignedTechnician).ToList();
            }
            else if (jtSorting.Equals("AssignedTechnician DESC"))
            {
                reportList = reportList.OrderByDescending(o => o.AssignedTechnician).ToList();
            }
            //}
            var result = reportList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result.OrderByDescending(o=>o.CCID).ToList(), TotalRecordCount = reportList.Count() };

        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }


    protected void ImgFLSExport_Click(object sender, EventArgs e)
    {
        try
        {

            string company = ddlRequestersCompany.SelectedItem.Text;
            string searchtext =  Request.Form["ticketno"];
            string name = ddlRequester.SelectedItem.Text;
            string status = ddlStatus.SelectedItem.Text;
            string loggedStartDate = txtLoggedStartDate.Text;
            string loggedEndDate = txtLoggedEndDate.Text;
            string scheduleDate = txtScheduledDate.Text;
           // string site = ddlSite.SelectedItem.Text;
            //string department = ddlDepratment.SelectedItem.Text;
            //string technician = ddlTechnician.SelectedItem.Text;


            List<Jqgrid> reportList = new List<Jqgrid>();
            //if (company != "[Loading company...]" && company != "Please Select...")
            //{
            reportList = FLSDetailsBAL.GetFLSReportList();
            //if (company != "")
            //    reportList = reportList.Where(r => r.ComapanyName == company).ToList();
            //if (!string.IsNullOrEmpty(callid))
            //    reportList = reportList.Where(r => r.CallID.ToString() == callid).ToList();
            //if (name != "")
            //    reportList = reportList.Where(r => r.RequesterName == name).ToList();
            //if (status != "")
            //    reportList = reportList.Where(r => r.Status == status).ToList();
            if (!string.IsNullOrEmpty(loggedStartDate) && !string.IsNullOrEmpty(loggedEndDate))
                reportList = reportList.Where(r => r.LoggedDate.Date >= (Convert.ToDateTime(loggedStartDate)) && r.LoggedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
            if (!string.IsNullOrEmpty(loggedStartDate) || !string.IsNullOrEmpty(loggedEndDate))
            {
                if (!string.IsNullOrEmpty(loggedStartDate))
                    reportList = reportList.Where(r => r.LoggedDate.Date >= (Convert.ToDateTime(loggedStartDate))).ToList();
                if (!string.IsNullOrEmpty(loggedEndDate))
                    reportList = reportList.Where(r => r.LoggedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
            }

            if (searchtext.Length > 0)
            {
                reportList = reportList.Where(p => (
                    (p.RequesterName != null ? p.RequesterName.ToLower().Contains(searchtext.ToLower()) : false)
           || (p.RequestersAddress != null ? p.RequestersAddress.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
             || (p.RequestersPostCode != null ? p.RequestersPostCode.ToLower().Contains(searchtext.ToLower()) : false)
              || (p.RequestersCity != null ? p.RequestersCity.ToLower().Contains(searchtext.ToLower()) : false)
               || (p.Details != null ? p.Details.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.AssignedTechnician != null ? p.AssignedTechnician.ToLower().Contains(searchtext.ToLower()) : false)
                 || (p.TypeofRequest != null ? p.TypeofRequest.ToLower().Contains(searchtext.ToLower()) : false)

                   )).ToList();
            }

            //if (site != "")
            //    reportList = reportList.Where(r => r.SiteName == site).ToList();
            //if (department != "")
            //    reportList = reportList.Where(r => r.DepartmentName == department).ToList();
            //if (technician != "")
            //    reportList = reportList.Where(r => r.TechnecianName == technician).ToList();


            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Jobs Report");



            // Title
            ws.Cell("A1").Value = "Jobs Report "; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            ws.Cell("A2").Value = "Ticket No";
            //ws.Cell("B2").Value = "Comapany";
            ws.Cell("B2").Value = "Requester";
            ws.Cell("C2").Value = "Status";
            ws.Cell("D2").Value = "Logged";
            ws.Cell("E2").Value = "Schedule";
            //ws.Cell("G2").Value = "Site";
            //ws.Cell("H2").Value = "Department";
            ws.Cell("F2").Value = "Technician";
            int i = 3;
            foreach (var item in reportList)
            {
                ws.Cell("A" + i.ToString()).Value = "" + item.CallID;
                //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                ws.Cell("B" + i.ToString()).Value = item.RequesterName;
                ws.Cell("C" + i.ToString()).Value = item.Status;
                ws.Cell("D" + i.ToString()).Value = item.LoggedDate;
                ws.Cell("D" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                ws.Cell("E" + i.ToString()).Value = item.ScheduledDateTime;
                ws.Cell("E" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                //ws.Cell("G" + i.ToString()).Value = item.SiteName;
                //ws.Cell("H" + i.ToString()).Value = item.DepartmentName;
                ws.Cell("F" + i.ToString()).Value = item.AssignedTechnician;

                i = i + 1;
            }

            // From worksheet
            var rngTable = ws.Range("A1:F2");

            var rngHeaders = rngTable.Range("A2:F2"); 
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

            wb.SaveAs(path + "\\" + "JobsReport.xlsx");
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "JobsReport.xlsx");
            if (fileInfo.Exists)
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=JobsReport.xlsx");
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}