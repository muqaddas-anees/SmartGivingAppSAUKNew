using ClosedXML.Excel;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;

namespace DeffinityAppDev.App
{
    public partial class PlatformSupportReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                  // sessionKeys.PortfolioID = QueryStringValues.OrgID;
                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }
                   //BindDropDown();
                    txtStartDate.Text = Deffinity.Utility.StartDateOfMonth(DateTime.Now).ToShortDateString();
                    txtEndDate.Text = Deffinity.Utility.EndDateOfMonth(DateTime.Now).ToShortDateString();
                    BindGrid();


                   


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //        private void BindDropDown()
        //        {
        //            try
        //            {
        //                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

        //                var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        //                var tpLfist = tList.Select(o => o.Type).Distinct().ToList();
        //                ddlTemplate.DataSource = tpLfist;
        //ddlTemplate.DataBind();
        //                ddlTemplate.Items.Insert(0, "Please select...");

        //                var dDefault = tList.Where(o => (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
        //                if (dDefault != null)
        //{
        //                    ddlTemplate.SelectedValue = dDefault.ID.ToString();
        //                    bindTemplateData();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogExceptions.WriteExceptionLog(ex);
        //            }
        //        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            try
            {
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID). Where(t=> t.IsPaid.HasValue ? t.IsPaid.Value : false).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();

                if (QueryStringValues.Type == "cash")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "cash"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.ValueOfGoods,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.CheckNumber,
                                     PaymentType = "Cash",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     Email = t.DonerEmail,
                                     PlatformFee = string.Format("{0:F2}", t.PlatformFee != null ? t.PlatformFee.Value : 0),
                                     TransactionFee = string.Format("{0:F2}", t.TransactionFee != null ? t.TransactionFee.Value : 0)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    //var dItem = rlist.LastOrDefault();
                    //if (dItem != null)
                    //{
                    //    lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                    //    lblStatus.Text = dItem.Status;
                    //    txtname.Text = dItem.Name;
                    //    txttype.Text = dItem.PaymentType;
                    //    txtemail.Text = dItem.Email;
                    //    lblCategories.Text = dItem.CategoryListWithAmount;
                    //    txtNotes.Text = dItem.Notes;
                    //    hunid.Value = dItem.unid;
                    //    Gridfilesbind();

                    //}
                }
                else if (QueryStringValues.Type == "inkind")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "inkind"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.ValueOfGoods,
                                     PaidDate = t.PaidDate,
                                     PayRef = String.Empty,
                                     PaymentType = "In Kind",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     Email = t.DonerEmail,
                                     PlatformFee = string.Format("{0:F2}", t.PlatformFee != null ? t.PlatformFee.Value : 0),
                                     TransactionFee = string.Format("{0:F2}", t.TransactionFee != null ? t.TransactionFee.Value : 0)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    //var dItem = rlist.LastOrDefault();
                    //if (dItem != null)
                    //{
                    //    lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                    //    lblStatus.Text = dItem.Status;
                    //    txtname.Text = dItem.Name;
                    //    txttype.Text = dItem.PaymentType;
                    //    txtemail.Text = dItem.Email;
                    //    lblCategories.Text = dItem.CategoryListWithAmount;
                    //    txtNotes.Text = dItem.Notes;
                    //    hunid.Value = dItem.unid;
                    //    Gridfilesbind();

                    //}

                }
                else
                {
                    //tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var rlist = (from t in tList
                                     //join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName, //t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     PlatformFee= string.Format("{0:F2}", t.PlatformFee != null?t.PlatformFee.Value:0),
                                     TransactionFee = string.Format("{0:F2}", t.TransactionFee != null ? t.TransactionFee.Value : 0)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                   
                }

                //if (rlist.Count > 0)
                //{
                //    //lblthisweek.Text = string.Format("{0:F2}", rlist.Sum(o=>o.Amount.HasValue?o.Amount.Value:0));
                //    //lblthismonth.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }


        protected void btnReport_Click(object sender, EventArgs e)
        {
            //Export to excel

            try
            {

                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(t => t.IsPaid.HasValue ? t.IsPaid.Value : false).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();
                var rlist = (from t in tList
                                 //join c in ulist on t.LoggedByID equals c.ID
                                 //join tc in tclist on t.TithingID equals tc.ID
                             select new
                             {
                                 ID = t.ID,
                                 Name = t.DonerName,// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Email = t.DonerEmail, //t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                 PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Amount = t.PaidAmount,
                                 PaidDate = t.PaidDate,
                                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                 PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                 REcurring = t.RecurringType,
                                 Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                 //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                 CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                 CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                 t.MoreDetails,
                                  PlatformFee = string.Format("{0:F2}", t.PlatformFee != null ? t.PlatformFee.Value : 0),
                                 TransactionFee = string.Format("{0:F2}", t.TransactionFee != null ? t.TransactionFee.Value : 0)
                             }).ToList();
                //if (site != "")
                //    reportList = reportList.Where(r => r.SiteName == site).ToList();
                //if (department != "")
                //    reportList = reportList.Where(r => r.DepartmentName == department).ToList();
                //if (technician != "")
                //    reportList = reportList.Where(r => r.TechnecianName == technician).ToList();


                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Donation Report");



                // Title
                ws.Cell("A1").Value = "Donation Report "; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("A2").Value = "Date";
                //ws.Cell("B2").Value = "Comapany";
                ws.Cell("B2").Value = "Donor Name";
                ws.Cell("C2").Value = "Donor Email Address";
                ws.Cell("D2").Value = "Amount Donated";
                ws.Cell("E2").Value = "Platform Contribution";
                //ws.Cell("G2").Value = "Site";
                //ws.Cell("H2").Value = "Department";
                ws.Cell("F2").Value = "Transaction Reference";
                int i = 3;
                foreach (var item in rlist)
                {
                    ws.Cell("A" + i.ToString()).Value = "" + item.PaidDate.Value.ToShortDateString() + item.PaidDate.Value.ToShortTimeString();
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value = item.Name;
                    ws.Cell("C" + i.ToString()).Value = item.Email;
                    ws.Cell("D" + i.ToString()).Value = string.Format("{0:F2}", item.Amount);

                    ws.Cell("E" + i.ToString()).Value = string.Format("{0:F2}", item.PlatformFee);

                    ws.Cell("F" + i.ToString()).Value = item.PayRef;

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

                wb.SaveAs(path + "\\" + "DonorReport.xlsx");
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "DonorReport.xlsx");
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=DonorReport.xlsx");
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetDonationCategories(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f.Split(':').First() + " ";
                        }
                    }
                }
            }

            return retval;

        }

        private string GetDonationCategoriesWithAmount(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f;
                        }
                    }
                }
            }

            return retval;

        }
        private string getUserData(List<UserMgt.Entity.Contractor> ulist, int loggedby, string check_value, string nameOrEmail)
        {
            string retval = "";


            if (loggedby == 0)
                retval = check_value;
            else
            {
                var eDetails = ulist.Where(o => o.ID == loggedby).FirstOrDefault();
                if (eDetails != null)
                {
                    if (nameOrEmail == "name")
                        retval = eDetails.ContractorName;
                    else
                        retval = eDetails.EmailAddress;
                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }

        private string getTithing(List<PortfolioMgt.Entity.TithingDefaultDetail> tulist, int id)
        {
            string retval = "";


            if (id == 0)
                retval = "";
            else
            {
                var eDetails = tulist.Where(o => o.ID == id).FirstOrDefault();
                if (eDetails != null)
                {

                    retval = eDetails.Title;

                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}