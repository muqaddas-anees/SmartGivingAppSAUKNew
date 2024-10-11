using DC.Entity;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class PayResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    //string paymentStatus = Request.QueryString["payment_status"];

                    var portfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                    lblOrgname.Text = portfolio.PortFolio;
                    var unid = QueryStringValues.UNID;
                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                    var pE = pmRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();

                    if (pE != null)
                    {
                        if (unid.Length > 0)
                        {


                            if (QueryStringValues.Type == "success")
                            {


                                pE.IsPaid = true;
                                pE.PaidDate = DateTime.Now;
                                sendThankyouMail(pE.unid);
                                pmRep.Edit(pE);
                                lblMsg.Text = "We're Doing More With Your Support. Thank you For Your Donation";
                            }

                            else if (QueryStringValues.Type == "cancel")
                            {
                                lblMsg.Text = "Failed to process payment, Please try again  ";
                            }
                            else if (QueryStringValues.Type == "notify")
                            {
                                lblMsg.Text = "Failed to process payment, Please try again  ";
                            }
                            else
                            {
                                if (pE.IsPaid.Value)
                                {

                                    lblMsg.Text = "We're Doing More With Your Support. Thank you For Your Donation";
                                }
                                else
                                {
                                    if (pE.CResult != null)
                                    {
                                        if (pE.CResult.Length > 0)
                                            lblMsg.Text = "Failed to process payment, Please try again  ";
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        if (QueryStringValues.Type == "booking_success")
                        {
                            IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                            var bookingDetails = pRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();
                            bookingDetails.CTracker = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", "", "", "", "", "");

                            bookingDetails.PaymentDatetime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                            bookingDetails.PaymentRef = "retref:" + unid + ";profileid:" + unid;
                            bookingDetails.IsPaid = true;
                            pRep.Edit(bookingDetails);
                            //send mail to user
                            SendEmailTOClient(unid);
                            lblMsg.Text = "Ticket(s) booked successfully";
                            //rettext = ret;

                        }
                        else if (QueryStringValues.Type == "online_success")
                        {
                             unid = QueryStringValues.UNID;
                            var pmRep_new = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();
                            var pE_new = pmRep_new.GetAll().Where(o => o.ProductSaleGuid == unid).FirstOrDefault();
                            if (pE_new != null)
                            {
                                pE_new.PaidDate = DateTime.Now;
                                pE_new.IsPaid = true;
                                //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                // pE.IsPaid = true;
                                pmRep_new.Edit(pE_new);


                                lblMsg.Text = "Congratulations! A new purchase has been made from your online shop.";
                                AdminOrderMail(unid);
                                CustomerOrderMail(unid);
                                // Response.Redirect("~/PayResult.aspx?tunid=" + Session["invoiceref"].ToString() + "&unid=" + Session["invoiceref"].ToString() + "&type=" + Request.QueryString["frm"].ToString(), false);
                            }
                        }
                        else if (QueryStringValues.Type == "sms_success")
                        {
                            try
                            {
                                unid = QueryStringValues.UNID;
                                var pmRep_new = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();
                                var pE_new = pmRep_new.GetAll().Where(o => o.ID == Convert.ToInt32(unid)).FirstOrDefault();
                                if (pE_new != null)
                                {
                                    pE_new.PaidOn = DateTime.Now;
                                    pE_new.IsPaid = true;
                                    
                                    //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                    // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                    // pE.IsPaid = true;
                                    pmRep_new.Edit(pE_new);


                                    lblMsg.Text = "Processed successfully.";
                                    //  AdminOrderMail(unid);
                                    // CustomerOrderMail(unid);
                                    // Response.Redirect("~/PayResult.aspx?tunid=" + Session["invoiceref"].ToString() + "&unid=" + Session["invoiceref"].ToString() + "&type=" + Request.QueryString["frm"].ToString(), false);
                                }
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                        }

                    }

                    BindLogo();

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindLogo()
        {
            imgLogo.ImageUrl = $"imagehandler.ashx?id={sessionKeys.PortfolioID}&s={ImageManager.file_section_portfolio}";
        }
        public static void AdminOrderMail(string unid)
        {
            try
            {

                PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> psrep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();
                var ps = psrep.GetAll().Where(o => o.ProductSaleGuid == unid).FirstOrDefault();
                PortfolioRepository<PortfolioMgt.Entity.ProductDetail> prep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
                var pd = prep.GetAll().Where(o => o.ProductGuid == ps.ProductGuid).FirstOrDefault();

                var company = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(pd.PortfolioID);
                var teamuser = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == company.ID).FirstOrDefault();

                string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                EmailFooter ef = new EmailFooter();
                string subject = "Online Shop Order Notification";// "Ticket Reference: " + callid.ToString();
                Emailer em = new Emailer();
                string body = em.ReadFile("~/App/EmailTemplates/EmailBasic.html");

                // body = body.Replace("[mail_head]", "Service Request Update");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(company.ID, Deffinity.systemdefaults.GetLocalPath()));
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                body = body.Replace("[name]", teamuser.ContractorName);
                string content = "";
                content = content + "<p>" + "" + "</p><br><br>";
                content = content + "<p>" + "<b>Customer Name:</b>" + ps.CustomerFirstName + " " + ps.CustomerLastName + "</p><br>";
                content = content + "<p>" + "<b>Order Number:</b> " + ps.ID + "</p><br>";
                content = content + "<p>" + "<b>Order Date:</b>" + ps.OrderDate.Value.ToShortDateString() + "</p><br><br>";
                var items = GetNotesList(pd, ps);
                content = content + "<p>" + "" + "</p><br>";
                content = content + "<p>" + "Best regards," + "</p><br>";
                content = content + "<p>" + company.PortFolio + "</p><br>";
                body = body.Replace("[content]", content);

                // mail to requester
                em.SendingMail(fromemailid, subject, body, teamuser.EmailAddress);
                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static void CustomerOrderMail(string unid)
        {
            try
            {

                PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> psrep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();
                var ps = psrep.GetAll().Where(o => o.ProductSaleGuid == unid).FirstOrDefault();
                PortfolioRepository<PortfolioMgt.Entity.ProductDetail> prep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
                var pd = prep.GetAll().Where(o => o.ProductGuid == ps.ProductGuid).FirstOrDefault();

                var company = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(pd.PortfolioID);


                string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                EmailFooter ef = new EmailFooter();
                string subject = "Order Confirmation";// "Ticket Reference: " + callid.ToString();
                Emailer em = new Emailer();
                string body = em.ReadFile("~/App/EmailTemplates/EmailBasic.html");

                // body = body.Replace("[mail_head]", "Service Request Update");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(company.ID, Deffinity.systemdefaults.GetLocalPath()));
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                body = body.Replace("[name]", ps.CustomerFirstName);
                string content = "";
                content = content + "<p>" + "Here are the details of the recent transaction:" + "</p><br><br>";
                content = content + "<p>" + "<b>Customer Name:</b>" + ps.CustomerFirstName + " " + ps.CustomerLastName + "</p><br>";
                content = content + "<p>" + "<b>Order Number:</b> " + ps.ID + "</p><br>";
                content = content + "<p>" + "<b>Order Date:</b>" + ps.OrderDate.Value.ToShortDateString() + "</p><br><br>";
                var items = GetNotesList(pd, ps);
                content = content + "<p>" + "<b>Product(s) Purchased:</b><br>" + items + "</p><br>";
                content = content + "<p>" + "<b>Total Amount:</b>" + string.Format("{1}{0:N2}", pd.ProductPrice, Deffinity.Utility.GetCurrencySymbol()) + "</p><br>";
                content = content + "<p>" + "" + "</p><br>";

                //content = content + "<p>" + "Please ensure that you process the order promptly and provide the best customer service possible. We encourage you to reach out to the customer to confirm their order, provide any necessary updates or assistance, and address any potential questions or concerns they may have." + "</p><br>";
                //content = content + "<p>" + "Additionally, don't forget to update your inventory accordingly to ensure accurate stock levels." + "</p><br>";
                content = content + "<p>" + "" + "</p><br>";
                content = content + "<p>" + "Best regards," + "</p><br>";
                content = content + "<p>" + company.PortFolio + "</p><br>";
                body = body.Replace("[content]", content);

                bool ismailsent = false;
                // mail to requester

                em.SendingMail(fromemailid, subject, body, ps.CustomerEmail);
                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static string GetNotesList(PortfolioMgt.Entity.ProductDetail pd, PortfolioMgt.Entity.ProductSalesTracker ps)
        {
            StringBuilder sbuild = new StringBuilder();
            if (pd != null)
            {
                // UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();
                // var uids = noteslist.Select(p => p.).ToArray();
                // var usercollection = cCollection.Contractor_SelectAll().Where(p => uids.Contains(p.ID)).ToList();
                sbuild.Append("<table style='width:100%'>");
                sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                sbuild.Append("<td style='width:50%'>Product</td><td style='text-align:right;'>Quantity</td><td style='text-align:right;'> Price</td><td style='text-align:right;'> Total</td>");
                sbuild.Append("</tr>");
                if (pd != null)
                {
                    sbuild.Append("<tr>");
                    sbuild.Append(string.Format("<td>{0}</td><td style='text-align:right;'>{1}</td><td style='text-align:right;'>{2}</td><td style='text-align:right;'>{3}</td>", pd.ProductName,
                       ps.ProductQTY.HasValue ? ps.ProductQTY.Value : 0,
                       string.Format("{0:N2}", (pd.ProductPrice)),
                       string.Format("{0:N2}", (ps.ProductQTY.HasValue ? ps.ProductQTY.Value : 0) * ((pd.ProductPrice)))));

                    sbuild.Append("</tr>");
                }
                sbuild.Append("</table>");
            }
            return sbuild.ToString();
        }
        private void SendEmailTOClient(string unid)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> auRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var ulist = puRep.GetAll().Where(o => o.BookingUNID == unid).ToList();

                //get booking details
                var bEntity = pRep.GetAll().Where(o => o.unid == ulist.FirstOrDefault().BookingUNID).FirstOrDefault();

                //get activity or event detsils
                var aEntity = auRep.GetAll().Where(o => o.ID == bEntity.ActivityID).FirstOrDefault();
                sessionKeys.PortfolioID = aEntity.OrganizationID;
                //get customer details 
                var portfolioDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == aEntity.OrganizationID).FirstOrDefault();

                // int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(sessionKeys.PortfolioID);
                string subject = "Here's your tickets";
                Emailer em = new Emailer();
                string bodyNew = em.ReadFile("~/WF/DC/EmailTemplates/eventtickets.htm");

                foreach (var u in ulist)
                {
                    string body = bodyNew;

                    body = body.Replace("[mail_head]", " Ticket(s)");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID));
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                    body = body.Replace("[noteslist]", GetQuoteItemList(u.UserBookingUNID));

                    body = body.Replace("[eventname]", aEntity.Title);
                    body = body.Replace("[eventdatetime]", string.Format("{0:dd/MM/yyyy hh:mm tt}", aEntity.StartDateTime));
                    body = body.Replace("[eventvenue]", string.Format("{0} {1} {2} {3} {4} {5}", aEntity.Venue_Name, aEntity.Address1, aEntity.Address2, aEntity.City, aEntity.state_Province, aEntity.Postalcode));
                    body = body.Replace("[orgname]", portfolioDetails.PortFolio);

                    body = body.Replace("[eventimage]", Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Events/" + aEntity.unid.ToString() + "/0.png");

                    //[date]
                    string Dis_body = body;
                    bool ismailsent = false;
                    // mail to requester
                    //if help desk or assign users are changed then mail should go to requester
                    body = body.Replace("[user]", u.UserName);



                    //var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                    //string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                    //if (File.Exists(pname))
                    //{
                    //    var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                    //    Email ToEmail = new Email();
                    //    Attachment attachment1 = new Attachment(pname);
                    //    attachment1.Name = q.CurrentTemplateName + ".pdf";

                    //    ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                    //}
                    //else
                    //{
                    em.SendingMail(fromemailid, subject, body, u.UserEmail);
                    //}
                    ismailsent = true;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetQuoteItemList(string unid)
        {
            StringBuilder sbuild = new StringBuilder();

            sbuild.Append("<table style='width:70%'>");
            var b = Deffinity.systemdefaults.GetWebUrl() + string.Format("/App/Events/TicketDetails.aspx?unid={0}", unid);
            sbuild.Append(string.Format("<tr><td><b> {1} </b><br></td><td>{0}</td></tr>", getButton(b, "View Ticket"), ""));
            sbuild.Append("</table>");




            return sbuild.ToString();
        }

        private string DefaultThankyouMail()
        {

            string retval = "";


            retval = @"

{{logo}} 

{{categorydonationdate}}

Dear {{donorfirstname}},

Thank you for your generous donation to {{instancename}}. With your contribution, we are one step closer to making a positive impact in the lives of those we serve.
We assure you that your donation is utilized effectively to maximize its impact. We will keep you updated on our progress and how your support is changing lives.

Once again, thank you for your generosity. We are honored to have you as a valued partner in our mission.
If you have specific questions about how your gift is being used or our organisation as a whole, please don’t hesitate to contact us.

Sincerely,

{{instancename}}

";

            return retval;

        }

        private string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#7239EA'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #7239EA; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
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
        private void sendThankyouMail(string _unid)
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var dItem = rpNew.GetAll().Where(o => o.unid == _unid).FirstOrDefault();


                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.AmountGrater >= (dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0) && o.AmountLesser <= (dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0)).FirstOrDefault();
                if (tn == null)
                {
                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                }



                var portfoliodetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                 sessionKeys.PortfolioName=  portfoliodetails.PortFolio;

                //if template is empty
                if (tn == null)
                {
                    tn.EmailContent = DefaultThankyouMail();
                    tn.PortfolioID = sessionKeys.PortfolioID;
                    tn.IsRecurring = false;
                    tn.Type = "Default";
                    rp.Add(tn);

                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();

                }
                    

                  

                String body = "";
                if (tn != null)
                {
                    body = tn.EmailContent;

                    
                    //{{currentyear}}
                    //{{instancename}}
                    body = body.Replace("{{instancename}}", portfoliodetails.PortFolio);
                    body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                    body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));
                    body = body.Replace("{{name}}", dItem.DonerName);
                    body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));
                    body = body.Replace("{{signature}}", portfoliodetails.PortFolio);
                    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                    //  body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount));

                    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                    body = body.Replace("{{donorsurname}}", dItem.DonerName);
                    //donorcompany
                    //  body = body.Replace("{{category}}", dItem.CategoryList);

                    body = body.Replace("{{donorcompany}}", portfoliodetails.PortFolio);



                    body = body.Replace("{{categorydonationamount}}", string.Format("{1}{0:N2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

                    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                    //logo

                    //  body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID) + "' />");
                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + portfoliodetails.ID + "&s=portfolio" + "' />");

                    body = body.Replace("{{fundraiseramount}}", string.Format("{1}{0:N2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

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

                }



                if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                {
                    Emailer em = new Emailer();
                    string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                    html_body = html_body.Replace("[table]", body);
                    body = html_body;

                    string fromid = Deffinity.systemdefaults.GetFromEmail();

                    string toid = dItem.DonerEmail;
                    string subject = "Donation";
                    //htomail.Value = toid;
                    //hsubject.Value = subject;
                    //CKEditor1.Text = body;
                    Email ToEmail = new Email();


                    ToEmail.SendingMail(fromid, subject, body, toid, "");

                    sendThankyouMailTracker(dItem.ID, body, subject, toid, tn.ID);
                    // sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                    //Email ToEmail = new Email();


                    //ToEmail.SendingMail(fromid, subject,body,toid,"");

                    //sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void sendThankyouMailTracker(int donationid, string mailcontent, string mailsubject, string mailto, int templateid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                rpNew.Add(new PortfolioMgt.Entity.ThankYouMailTracker()
                {
                    DonationID = donationid,
                    MailContent = mailcontent,
                    MailSubject = mailsubject,
                    MailTo = mailto,
                    PortfolioID = sessionKeys.PortfolioID,
                    TemplateID = templateid
                    ,
                    SentOn = DateTime.Now,
                    SendBy = sessionKeys.UID
                });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnBacktologin_Click(object sender, EventArgs e)
        {
            if (sessionKeys.SID == 3)
            {
                Response.Redirect("~/App/FundraiserListView.aspx", false);
            }
            else
            {

                if (sessionKeys.UID > 0)
                    Response.Redirect("~/App/Dashboard.aspx", false);
                else
                {
                    
                    var portfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);

                    Response.Redirect("~/OrgHomeNewV2.aspx?orgguid="+portfolio.OrgarnizationGUID, false);
                }
                
            }
        }
    }
}