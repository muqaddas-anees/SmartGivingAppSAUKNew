using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using DC.DAL;
using DC.Entity;
using System.Web.Services;
using System.Web.Script.Services;
using DC.BLL;
using ClosedXML.Excel;
using System.IO;

public partial class DC_AccessReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Access Report";
    }

   

    [WebMethod(EnableSession = true)]
    public static object GetVisitors(string company, string ticketno, string VstName, string VstCmpy,string loggedStartDate,string loggedEndDate, string purposeofvisit, string adate, string ddate, string status, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
      {
        try
        {
            List<AccessReport> visitors = new List<AccessReport>();

            //if (!string.IsNullOrEmpty(ticketno))
            //{

                visitors = VisitorsBAL.Visitors_SecurityModule();
                if (company != "All" && company != "[Loading company...]")
                    visitors = visitors.Where(r => r.Company == company).ToList();
                if (!string.IsNullOrEmpty(ticketno))
                    visitors = visitors.Where(r => r.CallID.ToString() == ticketno).ToList();
                if (!string.IsNullOrEmpty(VstName))
                    visitors = visitors.Where(r => r.Name.ToLower().Contains(VstName.ToLower())).ToList();
                if (!string.IsNullOrEmpty(VstCmpy))
                    visitors = visitors.Where(r => r.VisitorCompany.ToLower().Contains(VstCmpy.ToLower())).ToList();
               
                if (status != "0" && !string.IsNullOrEmpty(status))
                {
                    visitors = visitors.Where(r => r.Status == int.Parse(status)).ToList();
                }
                if (purposeofvisit != "0" && !string.IsNullOrEmpty(purposeofvisit))
                {
                    visitors = visitors.Where(r => r.PurposeofVisit == int.Parse(purposeofvisit)).ToList();
                }

                if (!string.IsNullOrEmpty(loggedStartDate) && !string.IsNullOrEmpty(loggedEndDate))
                    visitors = visitors.Where(r => r.RequestedDate.Date >= (Convert.ToDateTime(loggedStartDate)) && r.RequestedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
                if (!string.IsNullOrEmpty(loggedStartDate) || !string.IsNullOrEmpty(loggedEndDate))
                {
                    if (!string.IsNullOrEmpty(loggedStartDate))
                        visitors = visitors.Where(r => r.RequestedDate.Date >= (Convert.ToDateTime(loggedStartDate))).ToList();
                    if (!string.IsNullOrEmpty(loggedEndDate))
                        visitors = visitors.Where(r => r.RequestedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
                }
               
                if (!string.IsNullOrEmpty(adate) && !string.IsNullOrEmpty(ddate))
                {
                    visitors = visitors.Where(vd => vd.ArriveDate != null || vd.DepartDate != null).ToList();
                    visitors = visitors.Where(r => r.ArriveDate >= Convert.ToDateTime(adate) && r.DepartDate <= Convert.ToDateTime(ddate).AddDays(1).AddMinutes(-1)).ToList();
                }
                else if (!string.IsNullOrEmpty(adate))
                {
                    visitors = visitors.Where(vd => vd.ArriveDate != null).ToList();
                    visitors = visitors.Where(r => r.ArriveDate >= Convert.ToDateTime(adate)).ToList();
                }
                else if (!string.IsNullOrEmpty(ddate))
                {
                    visitors = visitors.Where(vd => vd.DepartDate != null).ToList();
                    visitors = visitors.Where(r => r.DepartDate >= Convert.ToDateTime(ddate)).ToList();
                }

                if (jtSorting.Equals("Name ASC"))
                {
                    visitors = visitors.OrderBy(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Company ASC"))
                {
                    visitors = visitors.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("VisitorCompany ASC"))
                {
                    visitors = visitors.OrderBy(o => o.VisitorCompany).ToList();
                }
                else if (jtSorting.Equals("VisitorCompany DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.VisitorCompany).ToList();
                }
                else if (jtSorting.Equals("EmailAddress ASC"))
                {
                    visitors = visitors.OrderBy(o => o.EmailAddress).ToList();
                }
                else if (jtSorting.Equals("EmailAddress DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.EmailAddress).ToList();
                }
                else if (jtSorting.Equals("PhoneNumber ASC"))
                {
                    visitors = visitors.OrderBy(o => o.PhoneNumber).ToList();
                }
                else if (jtSorting.Equals("PhoneNumber DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.PhoneNumber).ToList();
                }
                else if (jtSorting.Equals("AccessNumber ASC"))
                {
                    visitors = visitors.OrderBy(o => o.AccessNumber).ToList();
                }
                else if (jtSorting.Equals("AccessNumber DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.AccessNumber).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    visitors = visitors.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    visitors = visitors.OrderByDescending(o => o.CallID).ToList();
                }

                else
                {
                    visitors = visitors.OrderBy(o => o.CallID).ToList();
                }

                
           // }
            var result = visitors.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = visitors.Count() };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }


    protected void ImgAccessExport_Click(object sender, EventArgs e)
    {
        try
        {
            List<AccessReport> visitors = new List<AccessReport>();

            string company = ddlRequestersCompany.SelectedItem.Text;
            string ticketno = String.Format("{0}", Request.Form["ticketno"]);
            string vstName = String.Format("{0}", Request.Form["VstName"]);
            string vstCmpy = String.Format("{0}", Request.Form["VstCmpy"]);
            string status = ddlStatus.SelectedValue;
            string purposeOfVisit = ddlvstngpurp.SelectedValue;
            string loggedStartDate = txtLoggedStartDate.Text;
            string loggedEndDate = txtLoggedEndDate.Text;
            string adate = txtAtime.Text;
            string ddate = txtDtime.Text;

            visitors = VisitorsBAL.Visitors_SecurityModule();
            if (company != "")
                visitors = visitors.Where(r => r.Company == company).ToList();
            if (!string.IsNullOrEmpty(ticketno))
                visitors = visitors.Where(r => r.CallID.ToString() == ticketno).ToList();
            if (!string.IsNullOrEmpty(vstName))
                visitors = visitors.Where(r => r.Name.ToLower().Contains(vstName.ToLower())).ToList();
            if (!string.IsNullOrEmpty(vstCmpy))
                visitors = visitors.Where(r => r.VisitorCompany.ToLower().Contains(vstCmpy.ToLower())).ToList();

            if (status != "")
            {
                visitors = visitors.Where(r => r.Status == int.Parse(status)).ToList();
            }
            if (purposeOfVisit != "")
            {
                visitors = visitors.Where(r => r.PurposeofVisit == int.Parse(purposeOfVisit)).ToList();
            }

            if (!string.IsNullOrEmpty(loggedStartDate) && !string.IsNullOrEmpty(loggedEndDate))
                visitors = visitors.Where(r => r.RequestedDate.Date >= (Convert.ToDateTime(loggedStartDate)) && r.RequestedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
            if (!string.IsNullOrEmpty(loggedStartDate) || !string.IsNullOrEmpty(loggedEndDate))
            {
                if (!string.IsNullOrEmpty(loggedStartDate))
                    visitors = visitors.Where(r => r.RequestedDate.Date >= (Convert.ToDateTime(loggedStartDate))).ToList();
                if (!string.IsNullOrEmpty(loggedEndDate))
                    visitors = visitors.Where(r => r.RequestedDate.Date <= (Convert.ToDateTime(loggedEndDate))).ToList();
            }

            if (!string.IsNullOrEmpty(adate) && !string.IsNullOrEmpty(ddate))
            {
                visitors = visitors.Where(vd => vd.ArriveDate != null || vd.DepartDate != null).ToList();
                visitors = visitors.Where(r => r.ArriveDate >= Convert.ToDateTime(adate) && r.DepartDate <= Convert.ToDateTime(ddate).AddDays(1).AddMinutes(-1)).ToList();
            }
            else if (!string.IsNullOrEmpty(adate))
            {
                visitors = visitors.Where(vd => vd.ArriveDate != null).ToList();
                visitors = visitors.Where(r => r.ArriveDate >= Convert.ToDateTime(adate)).ToList();
            }
            else if (!string.IsNullOrEmpty(ddate))
            {
                visitors = visitors.Where(vd => vd.DepartDate != null).ToList();
                visitors = visitors.Where(r => r.DepartDate >= Convert.ToDateTime(ddate)).ToList();
            }

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Access Report");
            // Title
            ws.Cell("A1").Value = "Access Control Report ";// +string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);

            ws.Cell("A2").Value = "Ticket No";
            ws.Cell("B2").Value = "Company";
            ws.Cell("C2").Value = "Visitor Name";
            ws.Cell("D2").Value = "Visitor Company";
            ws.Cell("E2").Value = "Requested Date";
            ws.Cell("F2").Value = "Arrive Date";
            ws.Cell("G2").Value = "Depart Date";
            ws.Cell("H2").Value = "Email Address";
            ws.Cell("I2").Value = "Phone No";
            ws.Cell("J2").Value = "Access No";

            int i = 3;
            foreach (var item in visitors)
            {
                ws.Cell("A" + i.ToString()).Value = "" + item.CallID;
                ws.Cell("B" + i.ToString()).Value = item.Company;
                ws.Cell("C" + i.ToString()).Value = item.Name;
                ws.Cell("D" + i.ToString()).Value = item.VisitorCompany;
                ws.Cell("E" + i.ToString()).Value = item.RequestedDate;
                ws.Cell("E" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                ws.Cell("F" + i.ToString()).Value = item.ArriveDate;
                ws.Cell("F" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                ws.Cell("G" + i.ToString()).Value = item.DepartDate;
                ws.Cell("G" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();

                ws.Cell("H" + i.ToString()).Value = item.EmailAddress;
                ws.Cell("I" + i.ToString()).Value = item.PhoneNumber;
             //   ws.Cell("I" + i.ToString()).DataType = XLCellValues.Text;
                ws.Cell("J" + i.ToString()).Value = item.AccessNumber;

                i = i + 1;
            }

          
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

            string path = HttpContext.Current.Server.MapPath("UploadData\\SAMReports");

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }


            wb.SaveAs(path + "\\" + "AccessReport.xlsx");

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "AccessReport.xlsx");
            if (fileInfo.Exists)
            {


                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=AccessReport.xlsx");
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