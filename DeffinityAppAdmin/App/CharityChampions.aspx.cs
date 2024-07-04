using ClosedXML.Excel;
using DeffinityAppDev.WF.Admin;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class CharityChampions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        void BindGrid()
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> trRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            var tlist = trRep.GetAll().ToList();

            IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var plist = prRep.GetAll().ToList();

            IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();

            var rlist = (from r in pRep.GetAll().ToList()
                         orderby r.ID descending
                         select new
                         {
                             r.commission,
                             r.CreatedDate,
                             r.FirstName,
                             r.ID,
                             r.LastName,
                             r.RefCode,
                             r.UNID,
                              Url = Deffinity.systemdefaults.GetWebUrl() + "/Registration.aspx?r=" + r.RefCode + "&ngid="+DateTime.Now.Ticks,
                             NoofInstances = plist.Where(o=>o.RefCode == r.RefCode).Count(),
DonationsThisMonth = 0.00,
DonationsLastMonth = 0.00,
DonationsThisYear=0.00,
                         }).ToList();

            grid_display.DataSource = rlist;
            grid_display.DataBind();
        }

        protected void grid_display_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                        if (p != null)
                        {
                            hid.Value = p.ID.ToString();
                            txtFirstName.Text = p.FirstName;
                            txtLastName.Text = p.LastName;
                            txtReferralCode.Text = p.RefCode;
                            txtCommission.Text = (p.commission).ToString();
                           
                            mdl.Show();
                        }

                    }
                }
                if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        if (p != null)
                        {
                            pRep.Delete(p);
                        }
                        // PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(id);
                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                        //BindGrid();
                        Response.Redirect(Request.RawUrl, false);
                    }
                }

              
               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    

        protected void btnAddBundle_Click(object sender, EventArgs e)
        {
            hid.Value = "0";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtReferralCode.Text = "";
            txtCommission.Text = "0";


            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();
                List<string> sList = new List<string>();
                if (pRep.GetAll().Count() > 0)
                    sList = pRep.GetAll().Select(o => o.RefCode).ToList();

                var rcode = Deffinity.Utility.GenerateUniqueCode(sList);
                txtReferralCode.Text = rcode;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            mdl.Show();
        }

        protected void btnSubmit_onclick(object sender, EventArgs e)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();
                if (txtReferralCode.Text.Length > 0)
                {
                    if (hid.Value == "0")
                    {
                        var p = new PortfolioMgt.Entity.tblReferral();
                        p.CreatedDate = DateTime.Now;
                       
                        p.commission = Convert.ToDouble(txtCommission.Text.Trim() == "" ? "0.00" : txtCommission.Text.Trim());
                        p.FirstName = txtFirstName.Text.Trim();
                        p.LastName = txtLastName.Text.Trim();
                        p.RefCode = txtReferralCode.Text.Trim();
                       
                      
                        //p.SMSCount = 
                        pRep.Add(p);

                       // uploadImage(p.ID);
                        sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                        Response.Redirect(Request.RawUrl, false);
                    }
                    else
                    {
                        var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (p != null)
                        {
                            p.commission = Convert.ToDouble(txtCommission.Text.Trim() == "" ? "0.00" : txtCommission.Text.Trim());
                            p.FirstName = txtFirstName.Text.Trim();
                            p.LastName = txtLastName.Text.Trim();
                            p.RefCode = txtReferralCode.Text.Trim();

                            pRep.Edit(p);
                         //   uploadImage(p.ID);
                            sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                            Response.Redirect(Request.RawUrl, false);
                        }
                    }
                }
                //lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                //string script = "window.onload = function() { toastr.success('etetetetet', 'testet'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //Export to excel

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> trRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var tlist = trRep.GetAll().ToList();

                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
                var plist = prRep.GetAll().ToList();

                IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();
                var reflist = pRep.GetAll().ToList();


                var rlist = (from r in tlist
                             join p in plist on r.OrganizationID equals p.ID
                             join f in reflist on p.RefCode equals f.RefCode
                             where r.IsPaid == true
                             orderby r.ID descending
                             select new
                             {
                                Date = r.PaidDate.Value.ToShortDateString(),
                                Time = r.PaidDate.Value.ToShortTimeString(),
                                 TransactionValue = r.PaidAmount,
                                 PlatformFee = r.PlatformFee,
                                 InstanceName = p.PortFolio,
                                 Refcod = p.RefCode,
                                 FirstName = f.FirstName,
                                 LastName = f.LastName,
                                 Commission = p.commission,
                             }).ToList();


                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Charity Champions Report");



                // Title
                ws.Cell("A1").Value = "Charity Champions Report"; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("A2").Value = "Date";
                ws.Cell("B2").Value = "Time";
                ws.Cell("C2").Value = "Transaction Value";
               // ws.Cell("D2").Value = "Platform Fee %";
                ws.Cell("D2").Value = "Platform Fee Value";
                ws.Cell("E2").Value = "Instance Name";
                ws.Cell("F2").Value = "Referral Code";
                ws.Cell("G2").Value = "First Name";
                ws.Cell("H2").Value = "Last Name";
                ws.Cell("I2").Value = "Commission %";
                int i = 3;
                foreach (var item in rlist)
                {
                    ws.Cell("A" + i.ToString()).Value = "" + item.Date ;
                   
                    ws.Cell("B" + i.ToString()).Value = item.Time;
                    ws.Cell("C" + i.ToString()).Value = string.Format("{0:F2}", item.TransactionValue);
                    ws.Cell("D" + i.ToString()).Value = string.Format("{0:F2}", item.PlatformFee);
                    ws.Cell("E" + i.ToString()).Value = item.InstanceName;
                    ws.Cell("F" + i.ToString()).Value = item.Refcod;
                    ws.Cell("G" + i.ToString()).Value = item.FirstName;
                    ws.Cell("H" + i.ToString()).Value = item.LastName;
                    ws.Cell("I" + i.ToString()).Value = item.Commission;
                    //ws.Cell("J" + i.ToString()).Value = item.StatusDisplay;

                    i = i + 1;
                }

                // From worksheet
                var rngTable = ws.Range("A1:I2");

                var rngHeaders = rngTable.Range("A2:I2");
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

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=\"CharityChampionsReport.xlsx\"");

                // 3. Write the workbook to response stream
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                }

                // 4. End the response
                Response.End();
                //string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/SAMReports");

                //if (Directory.Exists(path) == false)
                //{
                //    Directory.CreateDirectory(path);
                //}

                //wb.SaveAs(path + "\\" + "TithingReport.xlsx");
                //System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "TithingReport.xlsx");
                //if (fileInfo.Exists)
                //{
                //    System.Web.HttpContext.Current.Response.Clear();
                //    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                //    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=TithingReport.xlsx");
                //    System.Web.HttpContext.Current.Response.Flush();
                //    System.Web.HttpContext.Current.Response.End();

                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {

        }
    }
}