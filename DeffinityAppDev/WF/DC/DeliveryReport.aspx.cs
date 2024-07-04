using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.Linq;
using DC.BLL;
using DC.Entity;
using ClosedXML.Excel;
using System.IO;
public partial class DC_DeliveryReport : System.Web.UI.Page
{
      
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
               // Master.PageHead = "Delivery Report";

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    [WebMethod(EnableSession = true)]
    public static object Get(string ticketno, string company, string ctno, string status,string receivedStartDate, string receivedEndDate, string overdue, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            List<DeliveryReport> reportList = new List<DeliveryReport>();
            //if (company != "[Loading company...]" && company != "Please Select...")
            //{
                reportList = DeliveryInformationBAL.BindDeliveryReportList();
                if (company != "All" && company != "[Loading company...]")
                    reportList = reportList.Where(r => r.Company == company).ToList();
                if (!string.IsNullOrEmpty(ticketno))
                    reportList = reportList.Where(r => r.CallID.ToString() == ticketno).ToList();
                if (!string.IsNullOrEmpty(ctno))
                    reportList = reportList.Where(r => r.CourierNumber == ctno).ToList();
                if (status != "ALL" && status != "[Loading status...]")
                    reportList = reportList.Where(r => r.Status == status).ToList();
                if (!string.IsNullOrEmpty(receivedStartDate) && !string.IsNullOrEmpty(receivedEndDate))
                    reportList = reportList.Where(r => r.DateRecieved.Date >= (Convert.ToDateTime(receivedStartDate)) && r.DateRecieved.Date <= (Convert.ToDateTime(receivedEndDate))).ToList();
                if (!string.IsNullOrEmpty(receivedStartDate) || !string.IsNullOrEmpty(receivedEndDate))
                {
                    if (!string.IsNullOrEmpty(receivedStartDate))
                        reportList = reportList.Where(r => r.DateRecieved.Date >= (Convert.ToDateTime(receivedStartDate))).ToList();
                    if (!string.IsNullOrEmpty(receivedEndDate))
                        reportList = reportList.Where(r => r.DateRecieved.Date <= (Convert.ToDateTime(receivedEndDate))).ToList();
                }
                if (overdue == "True")
                    reportList = reportList.Where(r => r.OverdueDays > 0).ToList();
                else
                    reportList = reportList.Where(r => r.OverdueDays == 0).ToList();

                if (jtSorting.Equals("CourierNumber ASC"))
                {
                    reportList = reportList.OrderBy(o => o.CourierNumber).ToList();
                }
                else if (jtSorting.Equals("CourierNumber DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("NumofBoxesRec ASC"))
                {
                    reportList = reportList.OrderBy(o => o.NumofBoxesRec).ToList();
                }
                else if (jtSorting.Equals("NumofBoxesRec DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.NumofBoxesRec).ToList();
                }
                else if (jtSorting.Equals("DateRecieved ASC"))
                {
                    reportList = reportList.OrderBy(o => o.DateRecieved).ToList();
                }
                else if (jtSorting.Equals("DateRecieved DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.DateRecieved).ToList();
                }
                else if (jtSorting.Equals("BoxesRemaining ASC"))
                {
                    reportList = reportList.OrderBy(o => o.BoxesRemaining).ToList();
                }
                else if (jtSorting.Equals("BoxesRemaining DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.BoxesRemaining).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    reportList = reportList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("Status ASC"))
                {
                    reportList = reportList.OrderBy(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("Status DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("StorageLocation ASC"))
                {
                    reportList = reportList.OrderBy(o => o.StorageLocation).ToList();
                }
                else if (jtSorting.Equals("StorageLocation DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.StorageLocation).ToList();
                }
                else if (jtSorting.Equals("OverdueDays ASC"))
                {
                    reportList = reportList.OrderBy(o => o.OverdueDays).ToList();
                }
                else if (jtSorting.Equals("OverdueDays DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.OverdueDays).ToList();
                }
                else if (jtSorting.Equals("TotalCost ASC"))
                {
                    reportList = reportList.OrderBy(o => o.TotalCost).ToList();
                }
                else if (jtSorting.Equals("TotalCost DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.TotalCost).ToList();
                }
                else if (jtSorting.Equals("PeriodCost ASC"))
                {
                    reportList = reportList.OrderBy(o => o.PeriodCost).ToList();
                }
                else if (jtSorting.Equals("PeriodCost DESC"))
                {
                    reportList = reportList.OrderByDescending(o => o.PeriodCost).ToList();
                }
                else
                {
                    reportList = reportList.OrderBy(o => o.CallID).ToList();
                }
            //}
            var result = reportList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = reportList.Count() };

        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }


    protected void ImgDeliveryExport_Click(object sender, EventArgs e)
    {
        try
        {
            string company = ddlRequestersCompany.SelectedItem.Text;
            string ticketNo = String.Format("{0}", Request.Form["ticketno"]);
            string courierTrackingNo = String.Format("{0}", Request.Form["cticketno"]);
            string status = ddlStatus.SelectedItem.Text;
            string receivedStartDate = txtReceivedStartDate.Text;
            string receivedEndDate = txtReceivedEndDate.Text;
            string overdue = chkOverdue.Checked.ToString();
            List<DeliveryReport> reportList = new List<DeliveryReport>();
            //if (company != "[Loading company...]" && company != "Please Select...")
            //{
            reportList = DeliveryInformationBAL.BindDeliveryReportList();
            if (company != "")
                reportList = reportList.Where(r => r.Company == company).ToList();
            if (!string.IsNullOrEmpty(ticketNo))
                reportList = reportList.Where(r => r.CallID.ToString() == ticketNo).ToList();
            if (!string.IsNullOrEmpty(courierTrackingNo))
                reportList = reportList.Where(r => r.CourierNumber == courierTrackingNo).ToList();
            if (status != "")
                reportList = reportList.Where(r => r.Status == status).ToList();
            if (!string.IsNullOrEmpty(receivedStartDate) && !string.IsNullOrEmpty(receivedEndDate))
                reportList = reportList.Where(r => r.DateRecieved.Date >= (Convert.ToDateTime(receivedStartDate)) && r.DateRecieved.Date <= (Convert.ToDateTime(receivedEndDate))).ToList();
            if (!string.IsNullOrEmpty(receivedStartDate) || !string.IsNullOrEmpty(receivedEndDate))
            {
                if (!string.IsNullOrEmpty(receivedStartDate))
                    reportList = reportList.Where(r => r.DateRecieved.Date >= (Convert.ToDateTime(receivedStartDate))).ToList();
                if (!string.IsNullOrEmpty(receivedEndDate))
                    reportList = reportList.Where(r => r.DateRecieved.Date <= (Convert.ToDateTime(receivedEndDate))).ToList();
            }
            if (overdue == "True")
                reportList = reportList.Where(r => r.OverdueDays > 0).ToList();
            else
                reportList = reportList.Where(r => r.OverdueDays == 0).ToList();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Delivery Report");



            // Title
            ws.Cell("A1").Value = "Delivery Report ";// +string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);

            ws.Cell("A2").Value = "Ticket No";
            ws.Cell("B2").Value = "Courier Tracking No";
            ws.Cell("C2").Value = "Status";
            ws.Cell("D2").Value = "Received";
            ws.Cell("E2").Value = "Date";
            ws.Cell("F2").Value = "Remaining";
            ws.Cell("G2").Value = "Location";
            ws.Cell("H2").Value = "Overdue";
            ws.Cell("I2").Value = "Total Charge";
            ws.Cell("J2").Value = " Monthly Charge";

            int i = 3;
            foreach (var item in reportList)
            {
                ws.Cell("A" + i.ToString()).Value = "" + item.CallID;
                ws.Cell("B" + i.ToString()).Value = item.CourierNumber;
                ws.Cell("C" + i.ToString()).Value = item.Status;
                ws.Cell("D" + i.ToString()).Value = item.NumofBoxesRec;
                ws.Cell("E" + i.ToString()).Value =  item.DateRecieved;
                ws.Cell("E" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                ws.Cell("F" + i.ToString()).Value = item.BoxesRemaining;
                ws.Cell("G" + i.ToString()).Value = item.StorageLocation;
                ws.Cell("H" + i.ToString()).Value = item.OverdueDays;
                ws.Cell("I" + i.ToString()).Value = item.TotalCost;
                ws.Cell("J" + i.ToString()).Value = item.PeriodCost;

                i = i + 1;
            }

            // From worksheet
            var rngTable = ws.Range("A1:J2");

            var rngHeaders = rngTable.Range("A2:J2");
            rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngHeaders.Style.Font.Bold = true;
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

            rngTable.Cell(1, 1).Style.Font.Bold = true;
            rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            rngTable.Cell(1, 1).Style.Font.FontSize = 15;
            rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
            rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            rngTable.Row(1).Merge(); 

            ws.Columns(1, 10).AdjustToContents();

            string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/SAMReports");

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }


            wb.SaveAs(path + "\\" + "DeliveryReport.xlsx");

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "DeliveryReport.xlsx");
            if (fileInfo.Exists)
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=DeliveryReport.xlsx");
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