using DC.Entity;
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
    public partial class OtherDonationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(QueryStringValues.Type == "inkind")
                {
                    lblPanelTitle.Text = "In-Kind Donations";
                    vimeo.Src = "https://player.vimeo.com/video/773765036?h=14ad471b2b";
                }
                else
                {
                    lblPanelTitle.Text = "Cash Donations";
                    vimeo.Src = "https://player.vimeo.com/video/773365862?h=58aa39060d";
                }
               
                BindDropDown();
                BindGrid();
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

                var dDefault = tList.Where(o => (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
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
        private void bindTemplateData()
        {

            try
            {
                var id = hid.Value;
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.Type == ddlTemplate.SelectedValue).FirstOrDefault();

                if (p != null)
                {

                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
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
                                     IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false)
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

                        if (dItem.FundriserUNID != null)
                        {
                            var fund = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == dItem.FundriserUNID).FirstOrDefault();
                            if (fund != null)
                            {
                                body = body.Replace("{{fundraisername}}", fund.Title ?? "");
                            }
                            else
                                body = body.Replace("{{fundraisername}}", "");
                        }
                        else
                            body = body.Replace("{{fundraisername}}", "");

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
                            retval = retval + f.Split(':').First() + "   :   <b>" + string.Format("{0:F2}", Convert.ToDouble(f.Split(':').Last() != null ? f.Split(':').Last() : "0.00")) + "</b><br/>";
                        }
                    }
                }
            }

            return retval;

        }
        //private void bindTemplateData()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

        //    var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.Type == ddlTemplate.SelectedValue).FirstOrDefault();

        //    if (p != null)
        //    {

        //        var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
        //        var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
        //        var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
        //        Random generator = new Random();
        //        var rlist = (from t in tList
        //                         // join c in ulist on t.LoggedByID equals c.ID
        //                         //join tc in tclist on t.TithingID equals tc.ID
        //                     select new
        //                     {
        //                         ID = t.ID,
        //                         Name = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
        //                         Email = getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
        //                         TithigName = t.DetailsOfDonation,// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
        //                         PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
        //                         Amount = t.PaidAmount,
        //                         PaidDate = t.PaidDate,
        //                         PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
        //                         PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
        //                         REcurring = t.RecurringType,
        //                         // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
        //                         Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
        //                         CategoryListWithAmount = "",
        //                         CategoryList = "",
        //                         t.MoreDetails

        //                     }).ToList();

        //        var dItem = rlist.FirstOrDefault();


        //        IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
        //        var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
        //        String body = "";
        //        if (tn != null)
        //        {
        //            body = tn.EmailContent;
        //            //{{currentyear}}
        //            body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
        //            body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0));
        //            body = body.Replace("{{name}}", dItem.Name);
        //            body = body.Replace("{{category}}", dItem.CategoryList);
        //            body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
        //            body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


        //            body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount));

        //            body = body.Replace("{{donorfirstname}}", dItem.Name);
        //            body = body.Replace("{{donorsurname}}", dItem.Name);
        //            //donorcompany
        //            body = body.Replace("{{category}}", dItem.CategoryList);

        //            body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


        //            body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.Amount));

        //            body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
        //            body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
        //            //logo

        //            body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

        //        }



        //        if (!body.Contains("!DOCTYPE HTML PUBLIC"))
        //        {
        //            Emailer em = new Emailer();
        //            string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

        //            html_body = html_body.Replace("[table]", body);
        //            body = html_body;

        //            string fromid = Deffinity.systemdefaults.GetFromEmail();

        //            string toid = dItem.Email;
        //            string subject = "Donation";
        //            htomail.Value = toid;
        //            hsubject.Value = subject;
        //            CKEditor1.Text = body;
        //            //mdlShowMail.Show();
        //            //Email ToEmail = new Email();


        //            //ToEmail.SendingMail(fromid, subject,body,toid,"");

        //            //sessionKeys.Message = "Your message is on it's way!";

        //            //Response.Redirect(Request.RawUrl, false);
        //        }


        //        // CKEditor1.Text = p.EmailContent;
        //        //hid.Value = p.ID.ToString();
        //    }
        //}
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
        private void BindGrid()
        {
            try
            {
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
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
                                     Amount = t.DetailsOfDonation,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.CheckNumber,
                                     PaymentType = "Cash",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     ValueOfGoods= string.Format("{0:F2}", t.ValueOfGoods.HasValue?t.ValueOfGoods.Value:0),
                                     Company =t.Company,
                                     CheckNumber =t.CheckNumber


                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    if (rlist.Count > 0)
                    {
                        GridDashboard.HeaderRow.Cells[4].Text = "Value of donation ";
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
                                     Name = c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.DetailsOfDonation,
                                     PaidDate = t.PaidDate,
                                     PayRef = String.Empty,
                                     PaymentType = "In-Kind",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     ValueOfGoods = string.Format("{0:F2}", t.ValueOfGoods.HasValue ? t.ValueOfGoods.Value : 0),
                                     Company = t.Company,
                                     CheckNumber = t.CheckNumber

                                 }).ToList();

                    rlist = rlist.Where(o => (o.Name != null && o.Name.ToLower().Contains(txtMemberSearch.Text.Trim().ToLower()))
                        || (o.TithigName != null && o.TithigName.ToLower().Contains(txtMemberSearch.Text.Trim().ToLower()))
                        || (o.Company != null && o.Company.ToLower().Contains(txtMemberSearch.Text.Trim().ToLower()))
                         || (o.CheckNumber != null && o.CheckNumber.ToLower().Contains(txtMemberSearch.Text.Trim().ToLower()))
                    ).ToList();
                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    if (rlist.Count > 0)
                    {
                     
                            GridDashboard.HeaderRow.Cells[4].Text = "Value of goods";
                      
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
                                     Name = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = "",// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList ="",
                                     t.MoreDetails,
                                     ValueOfGoods = string.Format("{0:F2}", t.ValueOfGoods.HasValue ? t.ValueOfGoods.Value : 0),
                                     Company = t.Company,
                                     CheckNumber = t.CheckNumber

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
                                     Name = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = t.DetailsOfDonation,// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     t.unid,

                                 }).FirstOrDefault();

                    // var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                    if (dItem.unid == null)
                    {
                        var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (dEntity != null)
                        {
                            dEntity.unid = Guid.NewGuid().ToString();

                            PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(dEntity);
                            hunid.Value = dEntity.unid;
}
}
                    else
                        hunid.Value = dItem.unid;


                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                    var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
                    if (tn == null)
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

                        if (dItem.Status.Contains("Successful"))
                            mdlShowMail.Show();
                        //Email ToEmail = new Email();


                        //ToEmail.SendingMail(fromid, subject,body,toid,"");

                        //sessionKeys.Message = "Your message is on it's way!";

                        //Response.Redirect(Request.RawUrl, false);
                    }




                }
                if (e.CommandName == "view")
                {
                    var id = e.CommandArgument.ToString();
                    hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                    var tEntity = tList.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (tEntity != null)
                    {
                        if (QueryStringValues.Type.Length > 0)
                        {
                            Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + tEntity.unid, false);
                        }
                    }

                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();


                    //Random generator = new Random();
                    //var rlist = (from t in tList
                    //                 // join c in ulist on t.LoggedByID equals c.ID
                    //                 //join tc in tclist on t.TithingID equals tc.ID
                    //             select new
                    //             {
                    //                 ID = t.ID,
                    //                 Name = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                    //                 Email = getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                    //                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                    //                 PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                    //                 Amount = t.PaidAmount,
                    //                 PaidDate = t.PaidDate,
                    //                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                    //                 PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                    //                 REcurring = t.RecurringType,
                    //                 Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                    //                 // Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                    //                 CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                    //                 CategoryList = "",
                    //                 t.MoreDetails,
                    //                 t.Notes,
                    //                 t.unid,
                    //             }).ToList();

                    //var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    //if (dItem != null)
                    //{
                    //    //lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                    //    //lblStatus.Text = dItem.Status;
                    //    //txtname.Text = dItem.Name;
                    //    //txttype.Text = dItem.PaymentType;
                    //    //txtemail.Text = dItem.Email;
                    //    //lblCategories.Text = dItem.CategoryListWithAmount;
                    //    //txtNotes.Text = dItem.Notes;
                    //    //hunid.Value = dItem.unid;
                    //    //Gridfilesbind();
                    //}
                }

                if (e.CommandName == "del")
                {
                    var id = e.CommandArgument.ToString();

                    // var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (id.Length > 0)
                    {
                        PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_delete(Convert.ToInt32(id));
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Deleted successfully", "");
                        BindGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
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

        protected void btnTithing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/OtherDonations.aspx?type="+QueryStringValues.Type, false);
        }

        public string getValueGoods()
        {
            return "Value of Goods";
        }
    }
}