using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DC.DAL;
using DC.Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using UserMgt.BAL;
using UserMgt.Entity;
using UserMgt.DAL;
using System.Data;
using AssetsMgr.DAL;
using System.Xml;
using System.Reflection;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class DeliveryListCtrl : System.Web.UI.UserControl
    {
        DisplayColumnBAL dcBAL = new DisplayColumnBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                InsertDefaultColumn();
                BindListBoxes();
                ccdCompany.DataBind();
                ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            Img1.Visible = true;
            if (Request.QueryString["type"] == "FLS")
            {
                //pnlCustomer.Visible=false;
                pnlMap.Visible = true;
                Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=FLS";
                pnlExport.Visible = true;
                ccdType.SelectedValue = "6";
                // link_flsreport.Visible = true;
                tdRTLable.Style.Add("display", "none");
                tdRTField.Style.Add("display", "none");
                tdRTpanel.Style.Add("display", "none");
                //tdRequestTypeLable.Style.Add("display", "inline-block");
                //tdRequestTypeField.Style.Add("display", "inline-block");

                // pnlRequestType.Visible = false;
                // link_unitstatus.Visible = true;
                // link_Dashboard.Visible = true;
                //configuration icon can see only admin
                if (sessionKeys.SID == 1)
                    ImgConfig.Visible = true;
                else
                    ImgConfig.Visible = false;
                lblFooter.Visible = true;
                //lblFooter.Text = "Please note that changes made to the SLA in this section will affect SLA’s for tickets raised from this point onwards for every customer.";
            }
            else
            {

                lblFooter.Visible = false;
            }
            if (Request.QueryString["type"] == "Delivery")
            {
                pnlMap.Visible = false;
                //pnlCustomer.Visible = true;
                Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=Delivery";
                // ccdType.SelectedValue = "1";
                //link_deliveryreport.Visible = true;
                tdRequestTypeLable.Style.Add("display", "none");
                tdRequestTypeField.Style.Add("display", "none");
                ImgConfig.Visible = false;
            }

            if (Request.QueryString["type"] == "AccessControl")
            {
                //pnlCustomer.Visible = true;
                pnlMap.Visible = false;
                Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=AccessControl";
                // ccdType.SelectedValue = "3";
                // link_accessreport.Visible = true;
                pnlAccessNo.Visible = true;
                tdRequestTypeLable.Style.Add("display", "none");
                tdRequestTypeField.Style.Add("display", "none");
                ImgConfig.Visible = false;
            }


            if (Request.QueryString["type"].ToLower() == "permittowork")
            {
                //pnlCustomer.Visible = true;
                pnlMap.Visible = false;
                Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=permittowork";
                // ccdType.SelectedValue = "1";
                //link_deliveryreport.Visible = true;
                tdRequestTypeLable.Style.Add("display", "none");
                tdRequestTypeField.Style.Add("display", "none");
                ImgConfig.Visible = false;
            }
        }
        public void InsertDefaultColumn()
        {
            using (DCDataContext dc = new DCDataContext())
            {
                int count = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID select a).Count();
                if (count == 0)
                {
                    dcBAL.Insertfornewuser();
                }
            }

        }

        public void BindListBoxes()
        {

            list.DataSource = dcBAL.selectinggridcolumns();
            list.DataValueField = "ID";
            list.DataTextField = "ColumnName";
            list.DataBind();
            using (DCDataContext dc = new DCDataContext())
            {
                var y = (from a in dc.DisplayColumnsByUsers
                         join b in dc.DisplayColumns on a.DisplayColumnID equals b.ID
                         where a.UserID == sessionKeys.UID
                         orderby (a.Position) ascending
                         select new { a.ID, b.ColumnName }).ToList();
                //Repllace 100 by sessionKeys.UID
                gridlist.DataSource = y;
                gridlist.DataValueField = "ID";
                gridlist.DataTextField = "ColumnName";
                gridlist.DataBind();
            }
        }
        protected void add_Click(object sender, EventArgs e)
        {

            if (list.SelectedItem != null)
            {
                int ID = int.Parse(list.SelectedValue);
                dcBAL.Insertrecord(ID);

                BindListBoxes();
                lblscreen.Text = "";
            }
            //else
            //{
            //    lblscreen.Text = "Select One Field From Additional Fields";
            //}
        }
        protected void remove_Click(object sender, EventArgs e)
        {
            if (gridlist.SelectedItem != null)
            {
                int ID = int.Parse(gridlist.SelectedValue);
                dcBAL.deleterecord(ID);

                BindListBoxes();
                lblscreen.Text = "";
            }
            //else 
            //{
            //    lblscreen.Text = "Select One Field From Current Columns Grid List";
            //}
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            popIssues.Hide();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void imgBtnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportData("Excel");
        }

        protected void imgBtnExportToPDF_Click(object sender, EventArgs e)
        {
            ExportData("PDF");
        }
        public void ExportData(string type)
        {
            try
            {


                using (DCDataContext db = new DCDataContext())
                {
                    var name = (from d in db.DisplayColumns
                                join u in db.DisplayColumnsByUsers
                                     on d.ID equals u.DisplayColumnID
                                where u.UserID == sessionKeys.UID
                                orderby u.Position
                                select new { FieldName = d.Value, ColumnName = d.ColumnName }).ToList();
                    string company = ddlCompany.SelectedItem.Text;
                    string ticketno = String.Format("{0}", Request.Form["ticketno"]);
                    string status = ddlStatus.SelectedItem.Text;
                    string requestType = ddlRequestType.SelectedItem.Text;
                    string url = HttpContext.Current.Request.Url.AbsolutePath;
                    List<Jqgrid> flsList = new List<Jqgrid>();
                   
                    if (url.ToLower().Contains("flsresourcelist.aspx"))
                    {
                        flsList = flsList.Where(f => f.AssignedTechnicianID == sessionKeys.UID).ToList();
                    }
                    if (!string.IsNullOrEmpty(company))
                    {
                        var d_array = new string[] { "[Loading customer...]", "Please select..." };
                        if (!d_array.Contains(company))
                        {
                            flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                        }
                    }

                    if (status == "" && ticketno == string.Empty)
                    {
                        flsList = flsList.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                    }
                    if (ticketno != string.Empty)
                    {
                        flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                    }


                    if (status != "")
                    {
                        flsList = flsList.Where(f => f.Status == status).ToList();
                    }


                    if (!string.IsNullOrEmpty(requestType))
                    {
                        var array = new string[] { "[Loading...]", "Please select..." };
                        if (!array.Contains(requestType))
                            flsList = flsList.Where(f => f.TypeofRequest == requestType).ToList();
                    }

                    if (type == "Excel")
                    {
                        var wb = new XLWorkbook();
                        var ws = wb.Worksheets.Add("List of Tickets");
                        char alpahbets = 'A';
                        int count = 1;

                        foreach (var p in name)
                        {
                            int i = 2;
                            ws.Cell(alpahbets.ToString() + "1").Value = p.ColumnName;

                            foreach (var item in flsList)
                            {
                                if (p.ColumnName == "Ticket Ref")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = "" + item.CallID;
                                }
                                else if (p.ColumnName == "Source of Request")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.SourceofRequest;
                                }
                                else if (p.ColumnName == "Requester Name")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequesterName;
                                }
                                else if (p.ColumnName == "Requesters Telephone No")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersTelephoneNo;
                                }
                                else if (p.ColumnName == "Requesters Email Address")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersEmailAddress;
                                }
                                else if (p.ColumnName == "Requesters Department")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersDepartment;
                                }
                                else if (p.ColumnName == "Requesters Job Title")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersJobTitle;
                                }
                                else if (p.ColumnName == "Subject")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Subject;
                                }
                                else if (p.ColumnName == "Details")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Details;
                                }
                                else if (p.ColumnName == "Site")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Site;
                                }
                                else if (p.ColumnName == "Type of Request")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TypeofRequest;
                                }
                                else if (p.ColumnName == "Status")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Status;
                                }
                                else if (p.ColumnName == "Category")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Category;
                                }
                                else if (p.ColumnName == "Logged By")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.LoggedBy;
                                }
                                else if (p.ColumnName == "Logged Date/Time")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.LoggedDateTime;
                                }
                                else if (p.ColumnName == "Assigned to Department")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.AssignedtoDepartment;
                                }
                                else if (p.ColumnName == "Assigned Technician")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.AssignedTechnician;
                                }
                                else if (p.ColumnName == "Scheduled Date/Time")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.ScheduledDateTime;
                                }
                                else if (p.ColumnName == "Date and Time Started")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.DateandTimeStarted;
                                }
                                else if (p.ColumnName == "Date and Time Closed")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.DateandTimeClosed;
                                }
                                else if (p.ColumnName == "Customer Ref")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.CustomerRef;
                                }
                                else if (p.ColumnName == "PO Number")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.PONumber;
                                }
                                else if (p.ColumnName == "Notes")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Notes;
                                }
                                else if (p.ColumnName == "Time Accumulated")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TimeAccumulated;
                                }
                                else if (p.ColumnName == "Time Worked")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TimeWorked;
                                }
                                else if (p.ColumnName == "Customer Cost Code")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.CustomerCostCode;
                                }
                                else if (p.ColumnName == "In Hand SLA")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.InHandSLA;
                                }
                                else if (p.ColumnName == "Resolution SLA")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.ResolutionSLA;
                                }
                                else if (p.ColumnName == "Company")
                                {
                                    ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Company;
                                }
                                i++;

                            }
                            alpahbets++;
                            count++;
                        }
                        var rngTable = ws.Range("A1:" + alpahbets-- + "1");

                        var rngHeaders = rngTable.Range("A1:" + alpahbets-- + "1");
                        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngHeaders.Style.Font.Bold = true;
                        rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
                        ws.Columns(1, name.Count).AdjustToContents();

                        string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tickets");

                        if (Directory.Exists(path) == false)
                        {
                            Directory.CreateDirectory(path);
                        }

                        wb.SaveAs(path + "\\" + "ListOfTickets.xlsx");
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "ListOfTickets.xlsx");
                        if (fileInfo.Exists)
                        {
                            System.Web.HttpContext.Current.Response.Clear();
                            System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=ListOfTickets.xlsx");
                            System.Web.HttpContext.Current.Response.Flush();
                            System.Web.HttpContext.Current.Response.End();

                        }
                    }
                    else
                    {
                        PdfPTable table = new PdfPTable(name.Count) { WidthPercentage = 100 };// table for Usage

                        Font headerFont = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.BOLD, new BaseColor(System.Drawing.Color.White))));
                        BaseColor headerColour = new BaseColor(System.Drawing.Color.Gray);
                        //Header

                        foreach (var p in name)
                        {

                            PdfPCell cell1 = new PdfPCell(new Phrase(p.ColumnName, headerFont));
                            cell1.BackgroundColor = headerColour;
                            table.AddCell(cell1);

                        }

                        PdfPCell cell = new PdfPCell(new Phrase("", headerFont));
                        cell.BackgroundColor = headerColour;
                        //table.AddCell(cell);
                        foreach (var item in flsList)
                        {
                            foreach (var p in name)
                            {
                                if (p.ColumnName == "Ticket Ref")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString("" + item.CallID), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Source of Request")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.SourceofRequest), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Requester Name")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.RequesterName), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Requesters Telephone No")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersTelephoneNo), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Requesters Email Address")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersEmailAddress), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Requesters Department")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersDepartment), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Requesters Job Title")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersJobTitle), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Subject")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Subject), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Details")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Details), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Site")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Site), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Type of Request")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.TypeofRequest), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Status")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Status), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Category")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Category), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Logged By")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.LoggedBy), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Logged Date/Time")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.LoggedDateTime), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Assigned to Department")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.AssignedtoDepartment), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Assigned Technician")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.AssignedTechnician), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Scheduled Date/Time")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.ScheduledDateTime), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Date and Time Started")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.DateandTimeStarted), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Date and Time Closed")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.DateandTimeClosed), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Customer Ref")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.CustomerRef), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "PO Number")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.PONumber), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Notes")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Notes), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Time Accumulated")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.TimeAccumulated), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Time Worked")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.TimeWorked), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Customer Cost Code")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.CustomerCostCode), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "In Hand SLA")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.InHandSLA), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Resolution SLA")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.ResolutionSLA), font));
                                    table.AddCell(cell);
                                }
                                else if (p.ColumnName == "Company")
                                {
                                    Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                    cell = new PdfPCell(new Phrase(Convert.ToString(item.Company), font));
                                    table.AddCell(cell);
                                }
                            }
                        }

                        string path = Server.MapPath("~/WF/UploadData/Temp");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 2f, 2f, 2f, 1f);
                        var writer = PdfWriter.GetInstance(document, Response.OutputStream);

                        Paragraph paragraph = new Paragraph("List of Tickets", new Font(FontFactory.GetFont("Tahoma", 10f, Font.BOLD)));
                        paragraph.SpacingAfter = 0f;

                        Chunk linebreak = new Chunk(new LineSeparator(1f, 100f, new BaseColor(System.Drawing.Color.Gray), Element.ALIGN_CENTER, -1));

                        document.Open();
                        document.Add(paragraph);
                        document.Add(linebreak);
                        document.Add(table);
                        document.Close();
                        //System.Diagnostics.Process.Start(FullPath); //automatically opens
                        //Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=ListOfTicket.pdf");
                        Response.Write(document);
                        // Response.TransmitFile(FullPath);
                        Response.End();


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}