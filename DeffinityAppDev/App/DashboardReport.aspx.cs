using ClosedXML.Excel;
using DC.BLL;
using DC.Entity;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DeffinityAppDev.App
{
    public partial class TaithingDashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if(sessionKeys.Message.Length >0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }
                    BindDropDown();
                    txtStartDate.Text = Deffinity.Utility.StartDateOfMonth(DateTime.Now).ToShortDateString();
                    txtEndDate.Text = Deffinity.Utility.EndDateOfMonth(DateTime.Now).ToShortDateString();
                    BindGrid();


                    //var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                    //var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    //var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                    //Random generator = new Random();
                    //var rlist = (from t in tList
                    //             //join c in ulist on t.LoggedByID equals c.ID
                    //             //join tc in tclist on t.TithingID equals tc.ID
                    //             select new
                    //             {
                    //                 ID = t.ID,
                    //                 Name = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                    //                 Email = t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                    //                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                    //                 PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                    //                 Amount = t.PaidAmount,
                    //                 PaidDate = t.PaidDate,
                    //                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                    //                 PaymentType = t.DonationType == null ?( t.RecurringType == null ? "Normal" : "Recurring"): (t.DonationType == "inkind"? "In Kind": "Cash"),
                    //                 REcurring = t.RecurringType,
                    //                 // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                    //                 Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",

                    //                 CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                    //                 CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                    //                 t.MoreDetails,
                    //                 t.Notes,
                    //                 t.unid,
                    //             }).OrderByDescending(o=>o.ID).ToList();

                    //var dItem = rlist.FirstOrDefault();
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
            }
            catch(Exception ex)
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
                            retval = retval + f.Split(':').First() + " " + "<br/>";
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
                            retval = retval + f.Split(':').First() +"   :   <b>" + string.Format("{0:F2}", Convert.ToDouble( f.Split(':').Last() != null? f.Split(':').Last():"0.00")) + "</b><br/>";
                        }
                    }
                }
            }

            return retval;

        }
        private void BindGrid()
        {
            try
            {
                btnCancelSubscription.Visible = false;
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o=>o.OrganizationID == sessionKeys.PortfolioID).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o=>o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();

                if(QueryStringValues.Type == "cash")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "cash"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = getDonorName(t),// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = getDonorEmail(t),// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
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
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),

                                     GiftAid =  (t.GiftAid.HasValue?t.GiftAid.Value:false)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
                        lblamount.Text = string.Format("{0:N2}", dItem.Amount);
                        lblStatus.Text = dItem.Status;
                        txtname.Text = dItem.Name;
                        txttype.Text = dItem.PaymentType;
                        txtemail.Text = dItem.Email;
                        lblCategories.Text = dItem.CategoryListWithAmount;
                        txtNotes.Text = dItem.Notes;
                        hunid.Value = dItem.unid;
                        SetFeePanel(dItem.IsPaid);
                        chkgiftaid.Checked = dItem.GiftAid;
                        if (dItem.IsPaid)
                        {
                            SetCanelation("Recurring");
                        }
                        Gridfilesbind(dItem.unid);

                    }
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
                                     Name = getDonorName(t),// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = getDonorEmail(t),// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.ValueOfGoods,
                                     PaidDate = t.PaidDate,
                                     PayRef = String.Empty,
                                     PaymentType = "In Kind",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount ="",
                                     CategoryList ="",
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false)
                                     ,

                                     GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
                        lblamount.Text = string.Format("{0:N2}", dItem.Amount);
                        lblStatus.Text = dItem.Status;
                        txtname.Text = dItem.Name;
                        txttype.Text = dItem.PaymentType;
                        txtemail.Text = dItem.Email;
                        lblCategories.Text = dItem.CategoryListWithAmount;
                        txtNotes.Text = dItem.Notes;
                        hunid.Value = dItem.unid;
                        SetFeePanel(dItem.IsPaid);
                        chkgiftaid.Checked = dItem.GiftAid;
                        if (dItem.IsPaid)
                        {
                            SetCanelation("Recurring");
                        }

                        Gridfilesbind(dItem.unid);

                    }

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
                                     Name = getDonorName(t),// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = getDonorEmail(t),// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist,t.TithingID.HasValue?t.TithingID.Value:0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                     REcurring = t.RecurringType,
                                      Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails== null?"" :t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails== null?"": t.MoreDetails),
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     PlatformFee = t.PlatformFee.HasValue? t.PlatformFee.Value:0,
                                     TransactionFee =  t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false)
                                     ,

                                     GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o=>o.ID).ToList();
                    GridDashboard.DataBind();

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
                        lblamount.Text = string.Format("{0:N2}", dItem.Amount);
                        lblStatus.Text = dItem.Status;
                        txtname.Text = dItem.Name;
                        txttype.Text = dItem.PaymentType;
                        txtemail.Text = dItem.Email;
                        lblCategories.Text = dItem.CategoryListWithAmount;
                        txtNotes.Text = dItem.Notes;
                        hunid.Value = dItem.unid;
                        lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                        lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                        SetFeePanel(dItem.IsPaid);
                        chkgiftaid.Checked = dItem.GiftAid;
                        if (dItem.IsPaid)
                        {
                            SetCanelation("Recurring");
                        }

                        Gridfilesbind(dItem.unid);

                    }
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
        private void SetCanelation(string paymenttype)
        {
            if (QueryStringValues.Type.ToLower() == "inkind")
            {
                btnCancelSubscription.Visible = false;
            }
            else if (QueryStringValues.Type.ToLower() == "cash")
            {
                btnCancelSubscription.Visible = false;
            }
            else
            {

                if (paymenttype == "Recurring")
                {
                    btnCancelSubscription.Visible = true;
                }
                else
                {
                    btnCancelSubscription.Visible = false;
                }
            }
        }
        private void SetFeePanel(bool IsPaid)
        {
            pnlTransaction.Visible = IsPaid;
            pnlPlatform.Visible = IsPaid;
        }
        private string getDonorName(TithingPaymentTracker t)
        {
            string retval = "";

            if(t.IsAnonymously.HasValue)
            {
                if(t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerName;
                }
            }
            else
            {
                retval = t.DonerName;
            }

            return retval;
        }
        private string getDonorEmail(TithingPaymentTracker t)
        {
            string retval = "";

            if (t.IsAnonymously.HasValue)
            {
                if (t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerEmail;
                }
            }
            else
            {
                retval = t.DonerEmail;
            }

            return retval;
        }

        private string getDonorContact(TithingPaymentTracker t)
        {
            string retval = "";

            if (t.IsAnonymously.HasValue)
            {
                if (t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerContact;
                }
            }
            else
            {
                retval = t.DonerContact;
            }

            return retval;
        }
        private string getUserData(List<UserMgt.Entity.Contractor> ulist, int loggedby,string check_value,string nameOrEmail)
        {
            string retval = "";

            
            if (loggedby == 0)
                retval = check_value;
            else {
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //Export to excel

            try
            {

                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();
                var rlist = (from t in tList
                             //join c in ulist on t.LoggedByID equals c.ID
                             //join tc in tclist on t.TithingID equals tc.ID
                             select new
                             {
                                 ID = t.ID,
                                 Name = getDonorName(t),// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Email = getDonorEmail(t),// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                 PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Amount = t.PaidAmount,
                                 PaidDate = t.PaidDate,
                                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                 PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                 REcurring = t.RecurringType,
                                 Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                 StatusDisplay = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "Successful" : "Failed",
                                 //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                 CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                 CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                 t.MoreDetails,
                                 t.unid,
                                 t.Notes,
                                 PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                 TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                 IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),
                                 

                                 GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)
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
                ws.Cell("B2").Value = "Name";
                ws.Cell("C2").Value = "Email";
                ws.Cell("D2").Value = "Amount";
                ws.Cell("E2").Value = "Type";
                //ws.Cell("G2").Value = "Site";
                //ws.Cell("H2").Value = "Department";
                ws.Cell("F2").Value = "Status";
                int i = 3;
                foreach (var item in rlist)
                {
                    ws.Cell("A" + i.ToString()).Value = "" + item.PaidDate.Value.ToShortDateString() +" "+ item.PaidDate.Value.ToShortTimeString();
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value = item.Name;
                    ws.Cell("C" + i.ToString()).Value = item.Email;
                    ws.Cell("D" + i.ToString()).Value = string.Format("{0:F2}", item.Amount);
                  
                    ws.Cell("E" + i.ToString()).Value = item.PaymentType;
                   
                    ws.Cell("F" + i.ToString()).Value = item.StatusDisplay;

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

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=\"DonorReport.xlsx\"");

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
        public void btnSend_Click(object sender, EventArgs e)
        {
            try
            {




                Email ToEmail = new Email();
                // var folderpath = Server.MapPath("~/WF/UploadData/Donations/" + hunid.Value);
                // Email er = new Email();
                List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                if (Directory.Exists(Server.MapPath("~/WF/UploadData/Donations/" + hunid.Value)))
                {
                    string[] S1 = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Donations/" + hunid.Value));
                    foreach (string fileName in S1)
                    {
                        a.Add(new System.Net.Mail.Attachment(fileName));
                    }
                }
                if (a.Count > 0)
                {
                    ToEmail.SendingMail(htomail.Value, hsubject.Value, CKEditor1.Text, Deffinity.systemdefaults.GetFromEmail(), a);
                }
                else
                {
                    ToEmail.SendingMail(Deffinity.systemdefaults.GetFromEmail(), hsubject.Value, CKEditor1.Text, htomail.Value, "");
                }

                sessionKeys.Message = "Your message is on it's way!";

                Response.Redirect(Request.RawUrl, false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getTitle(int TithingID, List<PortfolioMgt.Entity.TithingDefaultDetail> tclist,List<PortfolioMgt.Entity.TithingPaymentTracker> tlist,int tid)
        {
            string retval = string.Empty;

            if(TithingID >0)
            {
                if(tclist.Where(o => o.ID == TithingID).FirstOrDefault() != null)
                retval = tclist.Where(o => o.ID == TithingID).FirstOrDefault().Title;
            }

            if(QueryStringValues.Type.Length >0)
            {
                if(tlist.Where(o => o.ID == tid).FirstOrDefault() != null)
                retval = tlist.Where(o => o.ID == tid).FirstOrDefault().DetailsOfDonation;
            }

            return retval;

        }
        protected void GridDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
               
                if (e.CommandName == "SendReceipt")
                {
                    var id = e.CommandArgument.ToString();
                    hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                    Random generator = new Random();
                    var dItem = (from t in tList
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 where t.ID == Convert.ToInt32(id)
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                      Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.unid,
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),
                                     
                                     GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)

                                 }).FirstOrDefault();

                   // var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                    if(dItem.unid == null)
                    {
                        var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if(dEntity != null)
                        {
                            dEntity.unid = Guid.NewGuid().ToString();

                            PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(dEntity);
                            hunid.Value = dEntity.unid;
                        }
                    }
                    else
                    hunid.Value = dItem.unid;


                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                    var tn= rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue?o.SetAsDefault.Value:false)== true).FirstOrDefault();
                    if(tn == null)
                        tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();


                   // ddlTemplate.SelectedValue = tn.ID.ToString();

                    String body = "";
                    if (tn != null)
                    {
                        body = tn.EmailContent;
                        //{{currentyear}}
                        body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                        body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                        body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));
                        body = body.Replace("{{name}}", dItem.Name);
                        body = body.Replace("{{category}}", dItem.CategoryList);
                        body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                        body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

                        body = body.Replace("{{donorfirstname}}", dItem.Name);
                        body = body.Replace("{{donorsurname}}", dItem.Name);
                        //donorcompany
                        body = body.Replace("{{category}}", dItem.CategoryList);

                        body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


                        body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.Amount));

                        body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                        //logo

                        // body = body.Replace("{{logo}}", "<img src='"+ Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID,Deffinity.systemdefaults.GetLocalPath())+"' />");

                      // body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio" + "' />");
                        body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio" + "' />");
                    }



                    if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                    {
                        Emailer em = new Emailer();
                        string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                        html_body = html_body.Replace("[table]", body);
                        body = html_body;

                        string fromid = Deffinity.systemdefaults.GetFromEmail();
                        
                        string toid = dItem.Email;
                        string subject = "Donation";
                        htomail.Value = toid;
                        hsubject.Value = subject;
                        CKEditor1.Text = body;

                        if(dItem.Status.Contains("Successful") )
                        mdlShowMail.Show();
                        //Email ToEmail = new Email();


                        //ToEmail.SendingMail(fromid, subject,body,toid,"");

                        //sessionKeys.Message = "Your message is on it's way!";

                        //Response.Redirect(Request.RawUrl, false);
                    }
                   


                    
                }
                if(e.CommandName == "member")
                {
                    try
                    {
                        var id = e.CommandArgument.ToString();
                        hid.Value = id.ToString();
                        var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                        var uEntity = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == tList.OrganizationID).Where(o => o.EmailAddress.ToLower().Trim() == tList.DonerEmail.ToLower().Trim()).FirstOrDefault();
                   
                        if(uEntity != null)
                        {
                            Response.Redirect("~/App/Member.aspx?mid="+uEntity.ID,false);
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                }
                if (e.CommandName == "view")
                {
                    btnCancelSubscription.Visible = false;
                    var id = e.CommandArgument.ToString();
                    hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                    var tEntity = tList.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if(tEntity != null)
                    {
                        if(QueryStringValues.Type.Length >0)
                        {
                            Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid="+tEntity.unid,false);
                        }
                    }

                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                   
                    
                    Random generator = new Random();
                    var rlist = (from t in tList
                                 .Where(o => o.ID == Convert.ToInt32(id))
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                      Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                    // Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.Notes,
                                     t.unid,
                                     PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),
                                     
                                     GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)
                                 }).ToList();

                    var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (dItem != null)
                    {
                        lblamount.Text = string.Format("{0:N2}", dItem.Amount);
                        lblStatus.Text = dItem.Status;
                        txtname.Text = dItem.Name;
                        txttype.Text = dItem.PaymentType;
                        txtemail.Text = dItem.Email;
                        lblCategories.Text = dItem.CategoryListWithAmount;
                        txtNotes.Text = dItem.Notes;
                        hunid.Value = dItem.unid;
                        lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                        lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                        SetFeePanel(dItem.IsPaid);
                        chkgiftaid.Checked = dItem.GiftAid;
                        if (dItem.IsPaid)
                        {
                            SetCanelation("Recurring");
                        }
                        Gridfilesbind(dItem.unid);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
      
        private void BindDropDown()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                var tpLfist = tList.Select(o => o.Type).Distinct().ToList();
                ddlTemplate.DataSource = tpLfist;
                ddlTemplate.DataBind();
                ddlTemplate.Items.Insert(0, "Please select...");

                var dDefault = tList.FirstOrDefault();
                if (dDefault != null)
                {
                    ddlTemplate.SelectedValue = dDefault.ID.ToString();
                    bindTemplateData();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void bindTemplateData()
        {

            try
            {
                var id = hid.Value;
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.Type == ddlTemplate.SelectedValue).FirstOrDefault();

                if (p != null)
                {

                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(id)).Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                    Random generator = new Random();
                    var rlist = (from t in tList
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     // Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.Notes,
                                     t.unid,
                                     t.FundriserUNID,
                                     PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),
                                    
                                     GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)
                                 }).ToList();

                    var dItem = rlist.FirstOrDefault();


                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                    var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                    String body = "";
                    if (tn != null)
                    {
                        body = tn.EmailContent;


                        body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));

                        //{{currentyear}}
                        body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                        // body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0));

                        body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));
                        // {{fundraisername}}

                        if(dItem.FundriserUNID != null)
                        {
                            var fund = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == dItem.FundriserUNID).FirstOrDefault();
                            if(fund != null)
                            {
                                body = body.Replace("{{fundraisername}}", fund.Title??"");
                            }
                            else
                                body = body.Replace("{{fundraisername}}", "");
                        }
                        else
                        body = body.Replace("{{fundraisername}}","");

                        body = body.Replace("{{fundraiseramount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));
                        body = body.Replace("{{name}}", dItem.Name);
                        body = body.Replace("{{category}}", dItem.CategoryList);
                        body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                        body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

                        body = body.Replace("{{donorfirstname}}", dItem.Name);
                        body = body.Replace("{{donorsurname}}", dItem.Name);
                        //donorcompany
                        body = body.Replace("{{category}}", dItem.CategoryList);

                        body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


                        body = body.Replace("{{categorydonationamount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

                        body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                        //logo
                        // /ImageHandler.ashx?id=3074&s=portfolio
                        // body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

                        body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id="+sessionKeys.PortfolioID+"&s=portfolio" + "' />");

                    }



                    if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                    {
                        Emailer em = new Emailer();
                        string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                        html_body = html_body.Replace("[table]", body);
                        body = html_body;

                        string fromid = Deffinity.systemdefaults.GetFromEmail();

                        string toid = dItem.Email;
                        string subject = "Donation";
                        htomail.Value = toid;
                        hsubject.Value = subject;
                        CKEditor1.Text = body;
                        //mdlShowMail.Show();
                        //Email ToEmail = new Email();


                        //ToEmail.SendingMail(fromid, subject,body,toid,"");

                        //sessionKeys.Message = "Your message is on it's way!";

                        //Response.Redirect(Request.RawUrl, false);
                    }


                    // CKEditor1.Text = p.EmailContent;
                    //hid.Value = p.ID.ToString();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindTemplateData();


                mdlShowMail.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var id = hid.Value;

                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o=>o.ID ==Convert.ToInt32(hid.Value)).FirstOrDefault();

                if(tList != null)
                {
                    tList.Notes = txtNotes.Text.Trim();
                    PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(tList);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #region File
        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Download")
            {
                try
                {
                    GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    // string contenttype = gridfiles.DataKeys[gvrow.RowIndex].Values[1].ToString();
                    //string filename = gridfiles.DataKeys[gvrow.RowIndex].Values[2].ToString();
                    //string[] ex = filename.Split('.');
                    //string ext = ex[ex.Length - 1];
                    //"~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString(
                    string filepath = string.Format("~/WF/UploadData/Donations/{0}/", hunid.Value, e.CommandArgument.ToString());
                    //Response.ContentType = contenttype;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + e.CommandArgument.ToString() + "\"");
                    Context.Response.ContentType = "octet/stream";
                    Response.TransmitFile(filepath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
           

        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
              //  File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.ID == Convert.ToInt32(filePath)).FirstOrDefault();

                if (fList != null)
                    fRep.Delete(fList);

                Gridfilesbind(hunid.Value);

                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void Gridfilesbind(string SID)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_donor_doc).Where(o => o.FileID.StartsWith( SID.ToString())).ToList();

                var rList = (from r in fList
                             select new
                             {
                                 ID = r.ID,
                                 Value = r.FileID,
                                 Text = r.FileName
                             }).ToList();
                gridfiles.DataSource = rList;
                gridfiles.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                // File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var f = fRep.GetAll().Where(o => o.FileID == filePath && o.Section == ImageManager.file_section_donor_doc).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id=" + filePath + "&s=" + ImageManager.file_section_donor_doc);
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #endregion
        //public void Gridfilesbind()
        //{
        //    try
        //    {
        //        if (hunid.Value.Length > 0)
        //        {
        //            var folderpath = Server.MapPath("~/WF/UploadData/Donations/" + hunid.Value);
        //            if (System.IO.Directory.Exists(folderpath))
        //            {
        //                string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Donations/" + hunid.Value));
        //                List<System.Web.UI.WebControls.ListItem> files = new List<System.Web.UI.WebControls.ListItem>();
        //                foreach (string filePath in filePaths)
        //                {
        //                    files.Add(new System.Web.UI.WebControls.ListItem(Path.GetFileName(filePath), filePath));
        //                }

        //                gridfiles.DataSource = files;
        //                gridfiles.DataBind();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        protected void btnCancelSubscription_Click(object sender, EventArgs e)
        {
            try
            {
                var unid = hunid.Value;
              //  var t = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.unid == hunid.Value ).FirstOrDefault();
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> ptRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var t = ptRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();
                if (t != null)
                {
                    t.Notes = "Subscription Cancelled";
                    ptRep.Edit(t);
                    sessionKeys.Message = "Subscription cancelled successfully.";

                    Response.Redirect(Request.RawUrl, false);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}