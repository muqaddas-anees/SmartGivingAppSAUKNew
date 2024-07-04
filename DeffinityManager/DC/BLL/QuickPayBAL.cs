using DC.Entity;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.VariantTypes;
//using java.security.cert;
//using java.security;
//using javax.swing.text.html;
using PortfolioMgt.Entity;
//using sun.security.jca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.BAL;
using DC.DAL;
using DC.SRV;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Net;
using DeffinityManager.WF.CustomerAdmin.PayPalPayflowPro;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
//using com.sun.swing.@internal.plaf.metal.resources;
using System.Collections;
using DocumentFormat.OpenXml.Bibliography;
using System.Runtime.InteropServices.ComTypes;

//using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;

namespace DC.BLL
{
    public class QuickPayBAL
    {
        public static PortfolioContactAddress CreateContact(string ContactName, string ContactEmail, int portfolioid, int userid)
        {
            var retAdress = new PortfolioMgt.Entity.PortfolioContactAddress();
            var ret_p = new PortfolioMgt.Entity.PortfolioContact();
            var p = new PortfolioMgt.Entity.PortfolioContact();
            if (ContactEmail.Length > 0)
            {
                var ExistingENtity = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectAll(portfolioid).Where(o => o.Email.ToLower() == ContactEmail.ToLower().Trim()).FirstOrDefault();

                if (ExistingENtity == null)
                {

                    p.Name = ContactName;
                    p.Email = ContactEmail;
                    p.BuildingName = ContactName;
                    p.City = "";
                    p.DateLogged = DateTime.Now;
                    p.isDisabled = false;
                    p.PortfolioID = Convert.ToInt32(portfolioid);
                    p.Town = string.Empty;

                    ret_p = PortfolioMgt.BAL.PortfolioContactsBAL.PortfolioContactsBAL_add(p);
                }
                else
                {
                    ret_p = ExistingENtity;
                }

                //check address exist for this contact
                var exisitngAddress = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectAll(ret_p.ID).FirstOrDefault();
                if (exisitngAddress == null)
                {
                    //insert contact address
                    var pa = new PortfolioMgt.Entity.PortfolioContactAddress();
                    pa.Address = ContactName;
                    pa.BillingAddress1 = ContactName;
                    pa.ContactID = ret_p.ID;
                    pa.LoggedBy = userid;
                    retAdress = PortfolioMgt.BAL.PortfolioContactAddressBAL.PortfolioContactAddressBAL_add(pa);
                }
                else
                {
                    retAdress = exisitngAddress;
                }
            }

            return retAdress;
        }

        public static CallDetail Create_a_Job(PortfolioContactAddress addressEntity, string jobdetails, int portfolioid, int userid)
        {
            var dt = DateTime.Now;
            var dtEnd = DateTime.Now.AddHours(1);
            //Create job
            //add to call details
            var c = new CallDetail();
            c.CompanyID = portfolioid;
            c.LoggedBy = userid;
            c.LoggedDate = DateTime.Now;
            c.RequesterID = addressEntity.ContactID;
            //6 default
            c.RequestTypeID = 6;
            c.SiteID = 0;
            c.StatusID = JobStatus.New;
            CallDetailsBAL.AddCallDetails(c);
            var CallID = c.ID;
            //Journal entiry
            CallDetailsJournalBAL.AddCallDetailsJournal(c);


            try
            {
                //add to fls details
                var f = new FLSDetail();
                f.CallID = c.ID;
                f.CategoryID = 0;
                f.ContactAddressID = addressEntity.ID;
                f.DateTimeClosed = dtEnd;
                f.DateTimeStarted = dt;
                f.DepartmentID = 0;
                f.Details = jobdetails;
                f.PriorityId = 0;
                f.ScheduledDate = dt;
                f.ScheduledEndDateTime = dtEnd;
                f.SourceOfRequestID = 0;
                f.SubCategoryID = 0;
                f.SubjectID = 0;
                f.UserID = userid;
                FLSDetailsBAL.AddFLSDetails(f);
                //add to journal
                FLSDetailsJournalBAL.AddFLSDetailsJournal(f);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return c;
        }
        public static QuotationOption CreateQuoteOption (CallDetail callDetail, string jobdetails, int portfolioid, int userid)
        {
            var q = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = callDetail.ID, OptionName = jobdetails, CustomerID = callDetail.CompanyID.Value, IsActive = false, Description = jobdetails });
          

            return q;
        }
        public static QuotationOption Create_a_Quote(QuotationOption q,CallDetail callDetail, string jobdetails, int portfolioid, int userid, double amount, string attachfile_folderpath,string templatefolderpath)
        {
          //  var q = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = callDetail.ID, OptionName = jobdetails, CustomerID = callDetail.CompanyID.Value, IsActive = false, Description = jobdetails });
            if (q != null)
            {
                QuotationBAL.InsertQuoteItem(0, callDetail.ID, 1, 1, "1", jobdetails, 1, amount, 0, string.Empty, "00000000-0000-0000-0000-000000000000", amount, 0, 0, false, 0, "", q.ID);

                QuotationBAL.QuotationPrice_AddUpdate(callDetail.ID, amount, amount, 0, 0, q.ID, userid);
                List<int> ql = new List<int>();
                ql.Add(q.ID);
                //update the folder if any attachments there
             
              

                FLS_OptionalMailtoRequester(ql, portfolioid, callDetail,attachfile_folderpath,templatefolderpath);




            }

            return q;
        }

        public static QuotationOption Create_a_Quote(CallDetail callDetail, string jobdetails, int portfolioid, int userid, double amount,string attachfile_folder_ref)
        {
            var q = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = callDetail.ID, OptionName = jobdetails, CustomerID = callDetail.CompanyID.Value, IsActive = false, Description = jobdetails });
            if (q != null)
            {
                QuotationBAL.InsertQuoteItem(0, callDetail.ID, 1, 1, "1", jobdetails, 1, amount, 0, string.Empty, "00000000-0000-0000-0000-000000000000", amount, 0, 0, false, 0, "", q.ID);

                QuotationBAL.QuotationPrice_AddUpdate(callDetail.ID, amount, amount, 0, 0, q.ID);
                List<int> ql = new List<int>();
                ql.Add(q.ID);
                //update the folder if any attachments there
                string path = System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC");

                var folder = System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + attachfile_folder_ref);
                if (System.IO.Directory.Exists(folder))
                {
                    // System.IO.Directory.CreateDirectory(folder);
                    string Fromfol = string.Format("\\{0}\\", attachfile_folder_ref);
                    var foldername = q.CallID.ToString() + "-OPTION" + q.ID.ToString();

                    string Tofol = string.Format("\\{0}\\", foldername); ;

                    Directory.Move(path + Fromfol, path + Tofol);

                }

                FLS_OptionalMailtoRequester(ql,portfolioid,callDetail);

              

              
            }

            return q;
        }

        public static void FLS_OptionalMailtoRequester(List<int> optionID,int portfolioid,CallDetail callDetails)
        {
            try
            {
                int callid = callDetails.ID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(portfolioid);
                WebService ws = new WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, portfolioid);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var fls = FLSDetailsBAL.Jqgridlist(callDetails.ID).FirstOrDefault();
                        //var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var afooter = dc.EmailFooters.Where(o => o.customerID == callDetails.CompanyID).FirstOrDefault();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                        var fdetails = dc.FLSDetails.Where(c => c.CallID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var pdata = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        //var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        List<QuotationItem> noteslist = new List<QuotationItem>();
                        if (optionID.Count > 0)
                            noteslist = dc.QuotationItems.Where(c => c.CallidID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                            noteslist = dc.QuotationItems.Where(c => c.CallidID == callid).ToList();

                        List<QuotationPrice> qtPrice = new List<QuotationPrice>();
                        if (optionID.Count > 0)
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid).ToList();
                        var stypelist = dc.QuotationOptions.Where(c => c.CallID == callid).ToList();
                        var policylist = pd.ProductPolicyTypes.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                        var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Quotation";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSQuotation.htm");

                        body = body.Replace("[mail_head]", "Job Quotation");
                        //body = body.Replace("[logo]", pdata.ParnerPortal + Deffinity.systemdefaults.GetMailLogo(portfolioid));
                        body = body.Replace("[logo]", portfolio.LogoPath);
                        body = body.Replace("[border]", pdata.ParnerPortal + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", pdata.ParnerPortal);
                        body = body.Replace("[InstnaceTitle]", portfolio.PortFolio);
                        body = body.Replace("[noteslist]", GetQuoteItemList(noteslist, stypelist, qtPrice, policylist, addressDetails, Deffinity.systemdefaults.GetWebUrl(),portfolioid, fls));

                        // body = body.Replace("[notes]", GetQuoteItemList(noteslist, stypelist, qtPrice));

                        // body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        //var sp = noteslist.Select(o => o.SellingPrice).Sum();
                        //var vat = 0.00;
                        //var s1 = Convert.ToDouble((vat * sp) / Convert.ToDouble(100));
                        body = body.Replace("[refno]", fls.CCID.ToString());
                        body = body.Replace("[details]", fls.Details.ToString());
                        body = body.Replace("[emailfooter]", afooter != null?afooter.EmailFooter1.ToString(): string.Empty);

                        //[date]
                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester
                        body = body.Replace("[user]", pcontact.Name);

                        Email er = new Email();
                        List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + fls.CallID.ToString() + "-OPTION" + optionID.FirstOrDefault())))
                        {
                            string[] S1 = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + fls.CallID.ToString() + "-OPTION" + optionID.FirstOrDefault()));
                            foreach (string fileName in S1)
                            {
                                a.Add(new System.Net.Mail.Attachment(fileName));
                            }
                        }

                        var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", fls.CallID.ToString());
                        string pname = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", fls.CallID.ToString()));
                        if (File.Exists(pname))
                        {
                            var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(fls.CallID);
                            Email ToEmail = new Email();
                            Attachment attachment1 = new Attachment(pname);
                            attachment1.Name = q.CurrentTemplateName + ".pdf";
                                a.Add(attachment1);
                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                            // ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                        }
                        else
                        {
                            //em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                        }
                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        public static void FLS_OptionalMailtoRequester(List<int> optionID, int portfolioid, CallDetail callDetails,string attachfolderpath,string templatefolderpath)
        {
            try
            {
                int callid = callDetails.ID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(portfolioid);
                WebService ws = new WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, portfolioid);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var portfolioEntity= PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(portfolioid);
                        var pdata = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.ID == portfolioEntity.PartnerID).FirstOrDefault();

                        var fls = FLSDetailsBAL.Jqgridlist(callDetails.ID,portfolioid).FirstOrDefault();
                       
                        //var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                        var afooter = dc.EmailFooters.Where(o => o.customerID == cdetails.CompanyID).FirstOrDefault();
                        var fdetails = dc.FLSDetails.Where(c => c.CallID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        //var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        List<QuotationItem> noteslist = new List<QuotationItem>();
                        if (optionID.Count > 0)
                            noteslist = dc.QuotationItems.Where(c => c.CallidID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                            noteslist = dc.QuotationItems.Where(c => c.CallidID == callid).ToList();

                        List<QuotationPrice> qtPrice = new List<QuotationPrice>();
                        if (optionID.Count > 0)
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid).ToList();
                        var stypelist = dc.QuotationOptions.Where(c => c.CallID == callid).ToList();
                        var policylist = pd.ProductPolicyTypes.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                        var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Quotation";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSQuotation.htm");

                        body = body.Replace("[mail_head]", "Job Quotation");
                        //body = body.Replace("[logo]", pdata.ParnerPortal + Deffinity.systemdefaults.GetMailLogo(portfolioid));
                        body = body.Replace("[logo]", portfolio.LogoPath);
                        body = body.Replace("[border]", pdata.ParnerPortal + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", pdata.ParnerPortal);
                        body = body.Replace("[InstnaceTitle]", portfolioEntity.PortFolio);
                        body = body.Replace("[noteslist]", GetQuoteItemList(noteslist, stypelist, qtPrice, policylist, addressDetails, pdata.ParnerPortal, portfolioid, fls));

                        // body = body.Replace("[notes]", GetQuoteItemList(noteslist, stypelist, qtPrice));

                        // body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        //var sp = noteslist.Select(o => o.SellingPrice).Sum();
                        //var vat = 0.00;
                        //var s1 = Convert.ToDouble((vat * sp) / Convert.ToDouble(100));
                        body = body.Replace("[refno]", fls.CCID.ToString());
                        body = body.Replace("[details]", fls.Details.ToString());
                        body = body.Replace("[emailfooter]", afooter != null ? afooter.EmailFooter1.ToString() : string.Empty);
//[date]
                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester
                        body = body.Replace("[user]", pcontact.Name);

                        Email er = new Email();
                        List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                        if (Directory.Exists((attachfolderpath )))
                        {
                            string[] S1 = Directory.GetFiles(attachfolderpath);
                            foreach (string fileName in S1)
                            {
                                a.Add(new System.Net.Mail.Attachment(fileName));
                            }
                        }

                        var templatePath = string.Format(templatefolderpath+"{0}/Template.pdf", fls.CallID.ToString());
                        string pname = (string.Format(templatefolderpath + "{0}/Template.pdf", fls.CallID.ToString()));
                        if (File.Exists(pname))
                        {
                            var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(fls.CallID);
                            Email ToEmail = new Email();
                            Attachment attachment1 = new Attachment(pname);
                            attachment1.Name = q.CurrentTemplateName + ".pdf";
                            a.Add(attachment1);
                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                            // ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                        }
                        else
                        {
                            //em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                        }
                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static string GetQuoteItemList(List<QuotationItem> rlist, List<QuotationOption> qlist, List<QuotationPrice> qtPrice, List<PortfolioMgt.Entity.ProductPolicyType> policylist, PortfolioMgt.Entity.PortfolioContactAddress addressDetails, string weburl,int portfolioid, Jqgrid j)
        {
            StringBuilder sbuild = new StringBuilder();
            foreach (var q in qlist)
            {
                var noteslist = rlist.Where(o => o.QuotationOptionID == q.ID).ToList();
                var qPrice = qtPrice.Where(o => o.QuotationOptionID == q.ID).FirstOrDefault();
                if (noteslist.Count > 0)
                {
                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append(string.Format("<tr><td><b> Option: {0}</b></td></tr>", q.OptionName));
                    sbuild.Append("</table>");
                    // UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                    sbuild.Append("<td style='width:40%'>Item</td><td>Unit Price</td><td>QTY</td><td> TAX</td><td>Total</td>");
                    sbuild.Append("</tr>");
                    foreach (var n in noteslist)
                    {
                        sbuild.Append("<tr>");
                        sbuild.Append(string.Format("<td>{0}</td><td style='text-align:right'>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td>", n.ServiceDescription, string.Format("{0:F2}", n.SalesPrice / n.QTY), n.QTY, string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", n.SalesPrice + n.VAT)));

                        sbuild.Append("</tr>");
                    }
                    sbuild.Append("</table>");

                    sbuild.Append("<table style='width:40%'>");
                    var price = string.Format("{0:F2}", qPrice.OriginalPrice);
                    sbuild.Append(string.Format("<tr><td><b> Total price: </b></td><td>{0}</td></tr>", price));

                    if ((qPrice.FinalPrice.HasValue ? qPrice.FinalPrice.Value : 0) > 0)
                    {
                        var discountpriceIncludetax = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0));
                        var taxtprice = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0) - (qPrice.FinalPrice.HasValue ? qPrice.FinalPrice.Value : 0));
                        sbuild.Append(string.Format("<tr><td><b> Your price: </b></td><td>{0} Price includes Tax ({1})</td></tr>", discountpriceIncludetax, taxtprice));
                    }


                    sbuild.Append("</table>");
                    if (!string.IsNullOrEmpty(qPrice.Notes))
                    {
                        sbuild.Append("<table style='width:100%'>");
                        var notes = qPrice.Notes;
                        sbuild.Append(string.Format("<tr><td><b> Notes: </b><br>{0}</td></tr>", notes));
                        sbuild.Append("</table>");
                    }

                    sbuild.Append("<table style='width:70%'>");
                    var b = weburl + string.Format("/WF/DC/DCQuoteMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}", q.CallID, 1, "mail", j.CCID, q.ID, portfolioid);
                    sbuild.Append(string.Format("<tr><td><b> {1}: </b><br></td><td>{0}</td></tr>", getButton(b, "Click here to accept this quote"), q.OptionName));
                    sbuild.Append("</table>");
                    if (addressDetails != null)
                    {
                        if ((addressDetails.PolicyTypeID.HasValue ? addressDetails.PolicyTypeID.Value : 0) > 0)
                        {
                            sbuild.Append("<table style='width:100%'>");

                            sbuild.Append(string.Format("<tr><td><br></td></tr>"));
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:100%'>");

                            //sbuild.Append(string.Format("<tr><td style='font-size:15px'> Special offer: <br /></td></tr>"));

                            sbuild.Append(string.Format("<tr><td style='font-size:15px'> <img src='" + Deffinity.systemdefaults.GetWebUrl() + "/Content/images/SpecialOffer.png' style='border:0px' /> <br /></td></tr>"));

                            sbuild.Append(string.Format("<tr><td> If you were to take out the following maintenance plan, this price would be as follows:</td></tr>"));
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:85%'>");
                            //Header row
                            sbuild.Append(string.Format("<tr class='tab_header'><td><b> Plan </b></td><td><b> Discount Amount </b></td><td><b>New Total Price</b></td><td><b></b></td></tr>"));
                            foreach (var v in policylist)
                            {
                                var cUrl = weburl + string.Format("/WF/DC/DCQuoteContactMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}&planid={6}", j.CallID, 1, "mail", j.CCID, q.ID, portfolioid, v.ID.ToString());

                                sbuild.Append(string.Format("<tr><td><b> {0} </b></td><td style='text-align:right'> {1} </td><td style='text-align:right'>{2}</td><td>{3} </td></tr>", v.Title, string.Format("{0:F2}", GetDiscountAmount(qPrice.OriginalPrice, v.DiscountPercent.HasValue ? v.DiscountPercent.Value : 0)), string.Format("{0:F2}", GetDiscountAmountTotal(qPrice.OriginalPrice, v.DiscountPercent.HasValue ? v.DiscountPercent.Value : 0)), getGreenButton(cUrl, "Please contact me")));
                            }
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:100%'>");

                            sbuild.Append(string.Format("<tr><td><br><br></td></tr>"));
                            sbuild.Append("</table>");
                        }
                    }
                }
            }



            return sbuild.ToString();
        }
        public static double GetDiscountAmount(double amount, double discount)
        {
            double retval = 0;
            if (discount > 0)
            {
                retval = (amount * (discount / 100));
            }
            else retval = 0;

            return retval;
        }
        public static string getGreenButton(string url, string name)
        {
            //var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style='border-radius:2px;' bgcolor='#63b026'><a href = '{0}' target = '_blank' style='padding: 8px 12px; border: 1px solid #63b026;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);

            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' ><a href = '{0}' target = '_blank' >{1}</a></td ></tr></table></td></tr ></table>", url, "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/Content/images/ContactMe.png' style='border:0px' />");
            return v;
        }

        public static double GetDiscountAmountTotal(double amount, double discount)
        {
            double retval = 0;
            if (discount > 0)
            {
                retval = amount - (amount * (discount / 100));
            }
            else retval = amount;

            return retval;
        }
        public static string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#ED7D31'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #ED7D31; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
return v;
}
        public static int Create_Invoice_Ref( string jobDetails, int portfolioid, CallDetail c, int userid)
        {
            var iRef = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = c.ID, InvoiceDescription = jobDetails }, portfolioid, userid);
            return iRef.ID;
        }
        public static string Create_Invoice(int invoiceref,string jobDetails, string name, int portfolioid, CallDetail c, int userid, string amount, int addressid, string cardnumber, string month, string year, string cvv, string cardtype, string attachfile_folder)
        {
            var retval = "";
            try
            {
                LogExceptions.LogException(" start Create_Invoice");
                // var jqgridEntity = FLSDetailsBAL.Jqgridlist(c.ID).FirstOrDefault();
               // var iRef = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = c.ID, InvoiceDescription = jobDetails }, portfolioid, userid);
                int InvoiceRefID = invoiceref;// iRef.ID;
                LogExceptions.LogException(" start price added");

                string path = System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC");

              
                LogExceptions.LogException(" add invoice item");
                //add item incoice item
                InvoiceBAL.AddIvoiceItem(portfolioid, 0, c.ID, 1, 0, "", jobDetails, 0, Convert.ToDouble(!string.IsNullOrEmpty(amount) ? amount : "0.00"), InvoiceRefID, false);
                //Update the invoice price
                //Service_Prices(CallID, iRef.ID);

                //payment 
                //int addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                // var paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                // var pAddress = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                //var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

                // var pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                // var pcontact = pcRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolioid);
                //Payment details
                var paymentResult = string.Empty;
                if (payDetials != null)
                {
                    // paymentResult = CardConnectPay_ByAddress(payDetials, pAddress, pcontact, InvoiceRefID,cardnumber,month,cvv,cardtype);
                    retval = CardConnectPay(name, Convert.ToInt32(portfolioid), InvoiceRefID, cardnumber, month, year, cvv, cardtype, userid, c.ID,attachfile_folder);
                    if (retval == "Approved")
                    {

                        FLSDetailsBAL.UpdateTicketStatus(c.ID, userid, JobStatus.Closed);
                    }
                    else
                    {
                        //update the call status
                        FLSDetailsBAL.UpdateTicketStatus(c.ID, userid, JobStatus.Cancelled);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        public static string Create_Invoice(string jobDetails,string name,int portfolioid, CallDetail c, int userid, string amount, int addressid, string cardnumber, string month, string year, string cvv, string cardtype, string attachfile_folder_ref)
        {
            var retval = "";
            try
            {
                LogExceptions.LogException(" start Create_Invoice");
                // var jqgridEntity = FLSDetailsBAL.Jqgridlist(c.ID).FirstOrDefault();
                var iRef = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = c.ID,InvoiceDescription = jobDetails }, portfolioid, userid);
                int InvoiceRefID = iRef.ID;
                LogExceptions.LogException(" start price added");

                string path = System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC");

                var folder = System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + attachfile_folder_ref);
                if (System.IO.Directory.Exists(folder))
                {
                    // System.IO.Directory.CreateDirectory(folder);
                    string Fromfol = string.Format("\\{0}\\", attachfile_folder_ref);
                    var foldername = c.ID.ToString() + "-" + InvoiceRefID.ToString();

                    string Tofol = string.Format("\\{0}\\", foldername); ;

                    Directory.Move(path + Fromfol, path + Tofol);

                }
                LogExceptions.LogException(" add invoice item");
                //add item incoice item
                InvoiceBAL.AddIvoiceItem(portfolioid, 0, c.ID, 1, 0, "", jobDetails, 0, Convert.ToDouble(!string.IsNullOrEmpty(amount) ? amount : "0.00"), InvoiceRefID,false);
                //Update the invoice price
                //Service_Prices(CallID, iRef.ID);

                //payment 
                //int addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
               // var paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
               // var pAddress = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                //var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

               // var pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
               // var pcontact = pcRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolioid);
                //Payment details
                var paymentResult = string.Empty;
                if (payDetials != null)
                {
                    // paymentResult = CardConnectPay_ByAddress(payDetials, pAddress, pcontact, InvoiceRefID,cardnumber,month,cvv,cardtype);
                     retval = CardConnectPay( name,Convert.ToInt32(portfolioid), InvoiceRefID, cardnumber, month, year, cvv, cardtype, userid, c.ID);
                    if (retval == "Approved")
                    {
                        
                        FLSDetailsBAL.UpdateTicketStatus(c.ID, userid, JobStatus.Closed);
                    }
                    else
                    {
                        //update the call status
                        FLSDetailsBAL.UpdateTicketStatus(c.ID, userid, JobStatus.Cancelled);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
}
return retval;
}

        public static string CardConnectPay(string name,int portfolioID, int invoiceID, string CardNumber, string month, string year, string CVV, string cardtype,int userid,int callid,string attchfile_folder="")
        {
            string rettext = "";
            try
            {
                rettext = "";
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var ccnumber = CardNumber;
                if (string.IsNullOrEmpty(CardNumber))
                {
                    ccnumber = CardNumber;
                    //if (string.IsNullOrEmpty(ccnumber))
                    //{
                    //    lblCardERROR.Text = "Please enter card number";
                    //    return;
                    //}
                    LogExceptions.LogException("mytoken:" + ccnumber);

                }

                var invRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
                var InvoiceDetails = invRep.GetAll().Where(o => o.IncidentID == callid).FirstOrDefault();

                var flsRep = new DCRepository<DC.Entity.FLSDetail>();
                var flsDetails = flsRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
                var adrRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var pAddress = adrRep.GetAll().Where(o => o.ID == flsDetails.ContactAddressID).FirstOrDefault();
               // var cntRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
              //  var pContact = cntRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                var payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

                LogExceptions.LogException(" address id"+ flsDetails.ContactAddressID);
                LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                var p = new PortfolioMgt.Entity.PortfolioContactPaymentDetail();
                p.AddressID = flsDetails.ContactAddressID.HasValue?flsDetails.ContactAddressID.Value:0;
                p.IsPaid = false;
                p.PaidAmount = InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00;
                p.PayDate = DateTime.Now;
                p.PayOnWebsite = false;
                //p.Payref = pref;
                p.OrderRef = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;
                pmRep.Add(p);
                LogExceptions.LogException(" added PortfolioContactPaymentDetail:" );
                var month_year_expiry = month + year;
                var cvv = CVV;
                var MID = payDetials.Vendor;
                var amount = (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();

                string retval = string.Empty;
                LogExceptions.LogException("start credit card processign :");
                List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile(
                    payDetials.Host + "/cardconnect/rest", payDetials.Username, payDetials.Password,
                    //Here will pass MID value
                    MID,
                    //mytoken.value 
                    ccnumber,
                    cardtype,
                    //month year expiry
                    month_year_expiry,
                    CVV,
                    amount,
                    //Order and address details
                    p.OrderRef, name, pAddress.State, pAddress.City, pAddress.Address, "USA", pAddress.PostCode);
                LogExceptions.LogException("end credit card process");
                //Get Reponse values
                foreach (var r in rval)
                {
                    retval = retval + "key:" + r.key + "value:" + r.value + "/n";
                    
                }
                LogExceptions.LogException("retval: "+ retval);
                var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                if (ret != null)
                {
                    if (ret.value.ToString() == "Approval")
                    {
                        //Process the return values 
                        ProcessReturnValues(p, rval, invoiceID);
                       
                        try
                        {
                            if(attchfile_folder.Length >0)
                                FLS_SendInvoiceMailtoRequester(portfolioID, callid, userid,attchfile_folder);
                            else
                            FLS_SendInvoiceMailtoRequester(portfolioID, callid, userid);

                            var aRepository = new DCRepository<DC.Entity.FixedPriceApproval>();
                            var inv = aRepository.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
                            if (inv == null)
                            {
                                var ent = new DC.Entity.FixedPriceApproval();
                                ent.CallID = callid;
                                ent.ModifiedDate = DateTime.Now;
                                inv.DeniedBy = userid;
                                aRepository.Add(ent);
                            }
                            else
                            {
                                inv.DeniedBy = userid;
                                inv.ModifiedDate = DateTime.Now;
                                aRepository.Edit(inv);
                            }
                            IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
                            var inDetails = inRepository.GetAll().Where(o => o.CallID == callid).FirstOrDefault();

                            if (inDetails != null)
                            {
                                // lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
                            }
                            else
                            {
                                inDetails = new CallInvoice();
                                inDetails.CreatedDate = DateTime.Now;
                                inDetails.CallID = callid;
                                inRepository.Add(inDetails);
                            }

                            
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        //Display success process status
                        rettext = "Approved";// ret.value.ToString();
                    }
                    else
                    {
                        //Display faild process status
                        rettext = ret.value.ToString();
                    }
                   ;
                }
                LogExceptions.LogException(retval);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if(rval.Where(o=>o.key == ""))
            //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
            //pE.PayPalRef = RecurResponse.TrxPNRef;
            //pE.IsPaid = true;
            //pmRep.Edit(pE);
            ////Genrate policy and generate pdf
            ////GeneratePolicy.SendPolicyMail(addressid);
            //lblResult.Text = TrxnResponse.RespMsg;
            //var PNREF = TrxnResponse.Pnref;
            //var PPREF = TrxnResponse.PPref;
            //pnlResult.Visible = true;
            //pnlCCD.Visible = false;
            return rettext;
        }

        private static void ProcessReturnValues(PortfolioContactPaymentDetail p, List<ReponseValues> rval, int invoiceid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                pE.PayPalRef = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                pE.Payref = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                pE.IsPaid = true;

                pmRep.Edit(pE);

                var ipRep = new DCRepository<Incident_ServicePrice>();
                var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(invoiceid)).FirstOrDefault();
                if (pEntity != null)
                {
                    pEntity.Status = "Paid";
                    pEntity.ModifiedDate = DateTime.Now;
                    ipRep.Edit(pEntity);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private static void TithingProcessReturnValues(TithingPaymentTracker p, List<ReponseValues> rval, int trackerid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var pE = pmRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
                pE.IsPaid = true;
                pE.PayRef= "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty; 
                // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                // pE.IsPaid = true;
                pE.CResult = "";
                pmRep.Edit(pE);

                //var ipRep = new DCRepository<Incident_ServicePrice>();
                //var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(invoiceid)).FirstOrDefault();
                //if (pEntity != null)
                //{
                //    pEntity.Status = "Paid";
                //    pEntity.ModifiedDate = DateTime.Now;
                //    ipRep.Edit(pEntity);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static string OnlineCardConnectPay(int portfolioID, string productguid, string CardNumber, string month, string year, string CVV, string cardtype,
          double amount, string contactfirstname, string contactlastname, string contactemail, string contactnumber, string contactaddress, string contacttown,
          string contactstate, string contactzipcode, int qty
         )
        {
            string rettext = "";
            try
            {
                rettext = "";
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var ccnumber = CardNumber;
                if (string.IsNullOrEmpty(CardNumber))
                {
                    ccnumber = CardNumber;
                    //if (string.IsNullOrEmpty(ccnumber))
                    //{
                    //    lblCardERROR.Text = "Please enter card number";
                    //    return;
                    //}
                    LogExceptions.LogException("mytoken:" + ccnumber);

                }



                var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                var payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

                var activepay = payRep.GetAll().Where(o => o.PortfolioID == portfolioID && o.IsActive == true).FirstOrDefault();
                if (activepay == null)
                {
                    payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
                }
                else
                {
                    payDetials = activepay;
                }

                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();
                //var mRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategoryAmount>();
                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();

                //LogExceptions.LogException(" address id" + flsDetails.ContactAddressID);
                //LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                var p = new PortfolioMgt.Entity.ProductSalesTracker();
                p.ProductGuid = productguid;
                p.PortfolioID = portfolioID;// flsDetails.ContactAddressID.HasValue ? flsDetails.ContactAddressID.Value : 0;
                p.IsPaid = false;
                p.ProductQTY = qty;
                p.ProductPrice = amount * qty;
                p.PaidDate = DateTime.Now;
                p.OrderDate = DateTime.Now;
                p.PaymentResult = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", payDetials.Vendor, ccnumber, cardtype, month + year, CVV);


                p.CuseromerAddress = contactaddress;
                p.CuseromerContactNo = contactnumber;
                p.CuseromerState = contactstate;
                p.CuseromerTown = contacttown;
                p.CuseromerZipCode = contactzipcode;
                p.CustomerEmail = contactemail;
                p.CustomerFirstName = contactfirstname;
                p.CustomerLastName = contactlastname;
                p.ProductSaleGuid = Guid.NewGuid().ToString();
                var payref = Deffinity.Utility.GetSevenCharRandomString();
                p.PaymentRef = payref;
                // p.PayOnWebsite = false;
                //p.Payref = pref;
                //p.note = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;

                pmRep.Add(p);



                //add user member
                if (contactemail.Trim().Length > 0)
                {
                    try
                    {

                        //  UserMgt.BAL.UserMgtBAL.AddOrUpdateMembers(contactemail, contactname, "", contactnumber);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                //  LogExceptions.LogException(" added PortfolioContactPaymentDetail:");
                var month_year_expiry = month + year;
                var cvv = CVV;
                var MID = payDetials.Vendor;
                //var amount = (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();

                string retval = string.Empty;
                LogExceptions.LogException("start credit card processign :");
                var refdata = addpayrefdata((amount * qty).ToString(), p.ProductSaleGuid, ccnumber, cvv, month, month_year_expiry, contactfirstname, contactemail, "", "", "", p.ID.ToString(), sessionKeys.UID.ToString(), sessionKeys.PortfolioID.ToString(), sessionKeys.UName, sessionKeys.PartnerID.ToString(), p.ID,payref);
                //var retval = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value, out invoiceid);

                HttpContext.Current.Session["amount"] = amount * qty;
                HttpContext.Current.Session["invoiceref"] = p.ProductSaleGuid;
                HttpContext.Current.Session["cardnumber"] = ccnumber;
                HttpContext.Current.Session["cvv"] = cvv;
                HttpContext.Current.Session["month"] = month_year_expiry;
                HttpContext.Current.Session["year"] = month_year_expiry;
                HttpContext.Current.Session["customername"] = contactfirstname;
                HttpContext.Current.Session["customeremail"] = contactemail;
                HttpContext.Current.Session["address"] = "";
                HttpContext.Current.Session["postcode"] = "";
                HttpContext.Current.Session["phone"] = contactnumber;
                HttpContext.Current.Session["customerref"] = p.ID.ToString();// retval.PayID;

                HttpContext.Current.Session["payref"] = payref;

                rettext = refdata.refid;


                if (payDetials.PayType == "cardconnect")
                {

                    List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile(
                        payDetials.Host + "/cardconnect/rest", payDetials.Username, payDetials.Password,
                        //Here will pass MID value
                        MID,
                        //mytoken.value 
                        ccnumber,
                        cardtype,
                        //month year expiry
                        month_year_expiry,
                        CVV,
                        (amount * qty).ToString(),
                        //Order and address details
                        "", contactfirstname, "", "", "", "USA", "");
                    //LogExceptions.LogException("end credit card process");
                    ////Get Reponse values
                    foreach (var r in rval)
                    {
                        retval = retval + "key:" + r.key + "value:" + r.value + "/n";

                    }
                    //LogExceptions.LogException("retval: " + retval);
                    var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                    if (ret != null)
                    {
                        if (ret.value.ToString() == "Approval")
                        {
                            //Process the return values 
                            //TithingProcessReturnValues(p, rval, tithingid);
                            var pEdit = pmRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
                            if (pEdit != null)
                            {
                                pEdit.IsPaid = true;
                                pEdit.PaidDate = DateTime.Now;

                                pmRep.Edit(pEdit);
                            }
                            rettext = "Approved";// ret.value.ToString();
                        }
                        else
                        {
                            //Display faild process status
                            rettext = ret.value.ToString();
                            var pEdit = pmRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
                            if (pEdit != null)
                            {
                                pEdit.PaymentResult = rettext;
                                //pEdit.PaidDate = DateTime.Now;

                                pmRep.Edit(pEdit);
                            }
                        }
                       ;
                    }
                }
                else if (payDetials.PayType != "cardconnect")
                {

                    HttpContext.Current.Response.Redirect("~/PayProcess.aspx?frm=online&refid=" + refdata.refid + "&type=" + payDetials.PayType, false);

                }
                LogExceptions.LogException(retval);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if(rval.Where(o=>o.key == ""))
            //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
            //pE.PayPalRef = RecurResponse.TrxPNRef;
            //pE.IsPaid = true;
            //pmRep.Edit(pE);
            ////Genrate policy and generate pdf
            ////GeneratePolicy.SendPolicyMail(addressid);
            //lblResult.Text = TrxnResponse.RespMsg;
            //var PNREF = TrxnResponse.Pnref;
            //var PPREF = TrxnResponse.PPref;
            //pnlResult.Visible = true;
            //pnlCCD.Visible = false;
            return rettext;
        }
        public static string TithingCardConnectPay(string name, int portfolioID, int tithingid, string CardNumber, string month, string year, string CVV, string cardtype, int userid, 
            double amount,string contactname,string contactemail,string contactnumber,double transactionfeee,double platformfee,string recurringtype="",DateTime? startdate =null, DateTime? enddate= null, 
            int? dayStart = 0,string notes="", bool IsAnonymously = false,int RecurringPayID =0, string moredetails="",string unid = "", string fundriserUNID = "",string code="",bool IsCoveredFee= true,double amout_withoutfee=0.00,bool giftaid=false)
        {
            string payref = "";
            string rettext = "";
            try
            {
                rettext = "";
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var ccnumber = CardNumber;
                if (string.IsNullOrEmpty(CardNumber))
                {
                    ccnumber = CardNumber;
                    //if (string.IsNullOrEmpty(ccnumber))
                    //{
                    //    lblCardERROR.Text = "Please enter card number";
                    //    return;
                    //}
                    LogExceptions.LogException("mytoken:" + ccnumber);

                }

                var invRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
               // var InvoiceDetails = invRep.GetAll().Where(o => o.IncidentID == callid).FirstOrDefault();

                var flsRep = new DCRepository<DC.Entity.FLSDetail>();
                //var flsDetails = flsRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
                var adrRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                //var pAddress = adrRep.GetAll().Where(o => o.ID == flsDetails.ContactAddressID).FirstOrDefault();
                // var cntRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                //  var pContact = cntRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                var payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

                var activepay = payRep.GetAll().Where(o => o.PortfolioID == portfolioID && o.IsActive == true).FirstOrDefault();
                if (activepay ==  null)
                {
                    payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
                }
                else
                {
                    payDetials = activepay;
                }

                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var mRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategoryAmount>();
                var cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();

                //LogExceptions.LogException(" address id" + flsDetails.ContactAddressID);
                //LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                var p = new PortfolioMgt.Entity.TithingPaymentTracker();
                p.TithingID = tithingid;
                p.OrganizationID = portfolioID;// flsDetails.ContactAddressID.HasValue ? flsDetails.ContactAddressID.Value : 0;
                p.IsPaid = false;
                p.PaidAmount =  amount;
                p.PaidDate = DateTime.Now;
                p.LoggedByID = userid;
                p.CreatedDateTime = DateTime.Now;
                p.ModifiedDateTime = DateTime.Now;
                if(recurringtype.Length >0)
                p.RecurringType = recurringtype;
                p.StartDate = startdate;
                p.EndDate = enddate;
                p.DayStart = dayStart;
                p.Notes = notes;
                p.IsAnonymously = IsAnonymously;
                p.RecurringPayID = RecurringPayID;
                p.MoreDetails = moredetails;
                p.CTracker = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", payDetials.Vendor, ccnumber, cardtype, month + year, CVV);
                p.DonerName = contactname;
                p.DonerEmail = contactemail;
                p.DonerContact = contactnumber;
                p.unid = unid;
                p.FundriserUNID = fundriserUNID;
                p.TransactionFee = transactionfeee;
                p.PlatformFee = platformfee;
                payref = Deffinity.Utility.GetSevenCharRandomString();
                p.PayRef = payref;
                p.GiftAid = giftaid;

                try
                {

                    p.PlegitAmount = amount - amout_withoutfee;
                    p.CompanyAccountID = payDetials.Vendor;
                    p.CompanyChargers = Deffinity.Utility.GetAmountCharges(amout_withoutfee, payDetials.TransactionFee ?? 0);
                    p.ComapanyFixedFee = payDetials.FixedPrice;
                    p.ComapanyAmount = (amout_withoutfee) - (p.CompanyChargers + p.ComapanyFixedFee);
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
               // p.PayOnWebsite = false;
                //p.Payref = pref;
                //p.note = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;

                pmRep.Add(p);

                //update 
                if(moredetails.Length >0)
                {
                    var mList = moredetails.Split(';');
                    foreach(var m in mList)
                    {
                        try
                        {
                            var iList = m.Split(':');
                            if (iList.Length > 0)
                            {
                               

                                var cLIst = cRep.GetAll().Where(o => o.OrganizationID == portfolioID).ToList();

                                var ac = new TithingCategoryAmount();
                                ac.CategoryAmount = Convert.ToDouble(iList.Last());
                                ac.CategoryName = iList.First();
                                ac.CategoryUNID = cLIst.Where(o => o.Name == iList.First().Trim()).Select(o => o.unid).FirstOrDefault();
                                ac.DonationID = p.ID;
                                ac.DonationUNID = p.DonationUNID;
                                ac.LoggedDateTime = DateTime.Now;
                                mRep.Add(ac);
    
                        }
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }
                }

                else
                {
                    var cLIst = cRep.GetAll().Where(o => o.OrganizationID == portfolioID).FirstOrDefault();
                    if (cLIst != null)
                    {
                        var ac = new TithingCategoryAmount();
                        ac.CategoryAmount = Convert.ToDouble(amount - transactionfeee - platformfee);
                        ac.CategoryName = cLIst.Name;
                        ac.CategoryUNID = cLIst.unid;
                        ac.DonationID = p.ID;
                        ac.DonationUNID = p.DonationUNID;
                        ac.LoggedDateTime = DateTime.Now;
                        mRep.Add(ac);
                    }
                }


                //add user member
                if (contactemail.Trim().Length > 0)
                {
                    try
                    {

                        UserMgt.BAL.UserMgtBAL.AddOrUpdateMembers(contactemail, contactname, "", contactnumber);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
              //  LogExceptions.LogException(" added PortfolioContactPaymentDetail:");
                var month_year_expiry = month + year;
                var cvv = CVV;
                var MID = payDetials.Vendor;
                //var amount = (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();

                string retval = string.Empty;
                LogExceptions.LogException("start credit card processign :");
                var refdata = addpayrefdata(amount.ToString(),p.ID.ToString(), ccnumber, cvv, month, month_year_expiry, contactname, contactemail, "", "", "", p.ID.ToString(), sessionKeys.UID.ToString(), sessionKeys.PortfolioID.ToString(), sessionKeys.UName, sessionKeys.PartnerID.ToString(), p.ID,payref);
                //var retval = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value, out invoiceid);

                HttpContext.Current.Session["amount"] = amount;
                HttpContext.Current.Session["invoiceref"] = unid;
                HttpContext.Current.Session["cardnumber"] = ccnumber;
                HttpContext.Current.Session["cvv"] = cvv;
                HttpContext.Current.Session["month"] = month_year_expiry;
                HttpContext.Current.Session["year"] = month_year_expiry;
                HttpContext.Current.Session["customername"] = contactname;
                HttpContext.Current.Session["customeremail"] = contactemail;
                HttpContext.Current.Session["address"] = "";
                HttpContext.Current.Session["postcode"] = "";
                HttpContext.Current.Session["phone"] = contactnumber;
                HttpContext.Current.Session["customerref"] = p.ID.ToString();
                HttpContext.Current.Session["code"] = code;
                HttpContext.Current.Session["amount_withoutfee"] = amout_withoutfee;
                HttpContext.Current.Session["iscoverd"] = IsCoveredFee;// retval.PayID;


                HttpContext.Current.Session["recurring"] = recurringtype;
                HttpContext.Current.Session["startdate"] = startdate;
                HttpContext.Current.Session["payref"] = payref;

                rettext = refdata.refid;

              
                //List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile(
                //    payDetials.Host + "/cardconnect/rest", payDetials.Username, payDetials.Password,
                //    //Here will pass MID value
                //    MID,
                //    //mytoken.value 
                //    ccnumber,
                //    cardtype,
                //    //month year expiry
                //    month_year_expiry,
                //    CVV,
                //    amount.ToString(),
                //    //Order and address details
                //    "", name, "", "", "", "USA", "");
                //LogExceptions.LogException("end credit card process");
                ////Get Reponse values
                //foreach (var r in rval)
                //{
                //    retval = retval + "key:" + r.key + "value:" + r.value + "/n";

                //}
                //LogExceptions.LogException("retval: " + retval);
                //var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                //if (ret != null)
                //{
                //    if (ret.value.ToString() == "Approval")
                //    {
                //        //Process the return values 
                //        TithingProcessReturnValues(p, rval, tithingid);

                //        rettext = "Approved";// ret.value.ToString();
                //    }
                //    else
                //    {
                //        //Display faild process status
                //        rettext = ret.value.ToString();
                //    }
                //   ;
                //}
                LogExceptions.LogException(retval);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if(rval.Where(o=>o.key == ""))
            //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
            //pE.PayPalRef = RecurResponse.TrxPNRef;
            //pE.IsPaid = true;
            //pmRep.Edit(pE);
            ////Genrate policy and generate pdf
            ////GeneratePolicy.SendPolicyMail(addressid);
            //lblResult.Text = TrxnResponse.RespMsg;
            //var PNREF = TrxnResponse.Pnref;
            //var PPREF = TrxnResponse.PPref;
            //pnlResult.Visible = true;
            //pnlCCD.Visible = false;
            return rettext;
        }

        private static PortfolioMgt.Entity.ReferenceData addpayrefdata(string amount, string invoiceref, string cardnumber, string cvv,
           string month, string year, string customername, string customeremail, string address, string postcode, string phone,
           string customerref, string uid, string portfolioid, string uname, string partnerid, int payid,string payref)
        {

            IPortfolioRepository<PortfolioMgt.Entity.ReferenceData> rep = new PortfolioRepository<PortfolioMgt.Entity.ReferenceData>();

            var p = new PortfolioMgt.Entity.ReferenceData();

            string setval = string.Empty;

            setval = setval + string.Format("amount:{0};", amount);
            setval = setval + string.Format("invoiceref:{0};", invoiceref);
            setval = setval + string.Format("cardnumber:{0};", cardnumber);
            setval = setval + string.Format("cvv:{0};", cvv);
            setval = setval + string.Format("month:{0};", month);
            setval = setval + string.Format("year:{0};", year);
            setval = setval + string.Format("customername:{0};", customername);
            setval = setval + string.Format("customeremail:{0};", customeremail);
            setval = setval + string.Format("address:{0};", address);
            setval = setval + string.Format("postcode:{0};", postcode);
            setval = setval + string.Format("phone:{0};", phone);
            setval = setval + string.Format("customerref:{0};", customerref);
            setval = setval + string.Format("uqid:{0};", "");
            setval = setval + string.Format("payid:{0};", payid);
            //setval = setval + string.Format("refdataid:{0};", refdataid);

            setval = setval + string.Format("uid:{0};", uid);
            setval = setval + string.Format("portfolioid:{0};", portfolioid);
            setval = setval + string.Format("uname:{0};", uname);
            setval = setval + string.Format("partnerid:{0};", partnerid);
            setval = setval + string.Format("payref:{0};", payref);

            p.refdata = setval;
            p.refid = Guid.NewGuid().ToString();

            rep.Add(p);

            return p;

            //Session["amount"] = amount;
            //Session["invoiceref"] = invoiceid;
            //Session["cardnumber"] = ccnumber;
            //Session["cvv"] = cvv;
            //Session["month"] = ddlMonth.SelectedValue;
            //Session["year"] = year_expiry;
            //Session["customername"] = contactname;
            //Session["address"] = contactAddress.Address;
            //Session["postcode"] = contactAddress.PostCode;
            //Session["phone"] = contactAddress.PortfolioContact.Telephone;
            //Session["customerref"] = retval.PayID;



        }
        public static string GetNotesList(List<Incident_Service> noteslist, List<FixedRateType> stypelist)
        {
            StringBuilder sbuild = new StringBuilder();
            try
            {
                if (noteslist.Count > 0)
                {
                    UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                    sbuild.Append("<td style='width:50%'>Service</td><td>Type</td><td>Quantity</td><td> Unit Price</td><td>TAX</td><td>Total</td>");
                    sbuild.Append("</tr>");
                    foreach (var n in noteslist)
                    {
                        double vTotal = 0;
                        if (n.VAT.HasValue)
                        {
                            if (n.VAT.Value > 0)
                            {
                                vTotal = (n.QTY.Value * n.SellingPrice.Value) + n.VAT.Value;
                            }
                            else
                            {
                                vTotal = (n.QTY.Value * n.SellingPrice.Value);
                            }
                        }
                        string FixedRateTypeName = string.Empty;
                        var f = stypelist.Where(o => o.FixedRateTypeID == (n.FixedRateTypeID.HasValue ? n.FixedRateTypeID.Value : 0)).FirstOrDefault();
                        if (f != null)
                            FixedRateTypeName = f.FixedRateTypeName;

                        sbuild.Append("<tr>");
                        sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td><td style='text-align:right'>{5}</td>", n.ServiceDescription, FixedRateTypeName, n.QTY, string.Format("{0:F2}", n.SellingPrice), string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", vTotal)));

                        sbuild.Append("</tr>");
                    }
                    sbuild.Append("</table>");
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return sbuild.ToString();
        }
        public static void FLS_SendInvoiceMailtoRequester(int portfolioid, int callid, int userid,string attchfile_folder, List<ToEmailCalss> tlist = null)
        {
            try
            {
                //int callid = callid;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(portfolioid);
                DC.SRV.WebService ws = new DC.SRV.WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(portfolioid);

                ef = FooterEmail.EmailFooter_selectByID(6, portfolioid);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DC.DAL.DCDataContext dc = new DCDataContext())
                    {


                        var portfolioEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(portfolioid);
                        var pdata = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.ID == portfolioEntity.PartnerID).FirstOrDefault();
                        var fls = FLSDetailsBAL.Jqgridlist(callid,portfolioid).FirstOrDefault();
                        // var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == portfolioid).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var afooter = dc.EmailFooters.Where(o => o.customerID == cdetails.CompanyID).FirstOrDefault();
                        // var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        // var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        // string subject = "Job Reference: " + fls.CCID.ToString();
                        string subject = "Invoice:  " + pricelist.InvoiceDescription;

                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoiceNewPaid.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        //body = body.Replace("[logo]", pdata.ParnerPortal + Deffinity.systemdefaults.GetMailLogo(portfolioid));
                        body = body.Replace("[logo]", portfolio.LogoPath);
                        body = body.Replace("[border]", pdata.ParnerPortal + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", pdata.ParnerPortal);
                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));


                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", pricelist.InvoiceRef.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", pricelist.LoggedDate.Value.ToString());
                        body = body.Replace("[company]", portfolio.PortFolio);
                        body = body.Replace("[bank]", portfolio.BankName);
                        body = body.Replace("[account]", portfolio.AccountNumber);
                        body = body.Replace("[taxreg]", portfolio.TaxReg);
                        body = body.Replace("[sortcode]", portfolio.SortCode);
                        body = body.Replace("[iban]", portfolio.IBAN);
                        body = body.Replace("[swiftcode]", portfolio.SwiftCode);
                        body = body.Replace("[emailfooter]", afooter != null ? afooter.EmailFooter1.ToString() : string.Empty);
                        if ((pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) > 0)
                        {
                            var tPrice = string.Format("{0:F2}", (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tTax = string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) - (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tStr = string.Format("<tr><td>Your Price:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tPrice);
                            tStr = tStr + string.Format("<tr><td>Tax:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tTax);
                            body = body.Replace("[specialprice]", tStr);

                            body = body.Replace("[gtotal]", string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00)));
                        }
                        else
                        {
                            body = body.Replace("[specialprice]", string.Empty);
                            body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                        }

                        //[specialprice]

                        //   var uqid = GenerateButtonUrlNew(fls.Company, userid, fls.CallID, fls.CCID, pricelist.ID);
                        //   body = body.Replace("[link]", string.Format("{0}/WF/payinvoice/PaymentProcess.aspx?uqid={1}", Deffinity.systemdefaults.GetWebUrl(), uqid));

                        //[date]

                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        Email er = new Email();
                        List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                        if (Directory.Exists(attchfile_folder))
                        {
                            string[] S1 = Directory.GetFiles(attchfile_folder);
                            foreach (string fileName in S1)
                            {
                                a.Add(new System.Net.Mail.Attachment(fileName));
                            }
                        }

                        if (tlist != null)
                        {
                            if (tlist.Count > 0)
                            {
                                foreach (var t in tlist)
                                {
                                    var body1 = body;
                                    //if help desk or assign users are changed then mail should go to requester
                                    body1 = body1.Replace("[user]", t.name);
                                    //insert pay now button generated url



                                    er.SendingMail(t.email, subject, body, fromemailid, a);

                                    // em.SendingMail(fromemailid, subject, body, t.email);
                                }
                            }
                            else
                            {
                                //if help desk or assign users are changed then mail should go to requester
                                body = body.Replace("[user]", pcontact.Name);
                                //insert pay now button generated url


                                er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                                //  em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            }

                        }
                        else
                        {
                            //if help desk or assign users are changed then mail should go to requester
                            body = body.Replace("[user]", pcontact.Name);
                            //insert pay now button generated url

                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);

                            // em.SendingMail(fromemailid, subject, body, pcontact.Email);

                        }



                        //update the status
                        InvoiceBAL.UpdateInvoiceStatus_SendtoCustomer(fls.CallID, pricelist.ID);

                        // addtocomunications(fromemailid, fls, pcontact, subject, body);

                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void FLS_SendInvoiceMailtoRequester( int portfolioid, int callid, int userid, List<ToEmailCalss> tlist = null)
        {
            try
            {
                //int callid = callid;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, portfolioid);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DC.DAL.DCDataContext dc = new DCDataContext())
                    {



                        var fls = FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
                       // var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == portfolioid).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                        var afooter = dc.EmailFooters.Where(o => o.customerID == cdetails.CompanyID).FirstOrDefault();
                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                       // var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                       // var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        // string subject = "Job Reference: " + fls.CCID.ToString();
                        string subject = "Invoice:  " + pricelist.InvoiceDescription;

                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoiceNewPaid.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        // body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[logo]", portfolio.LogoPath);
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));


                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", pricelist.InvoiceRef.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", pricelist.LoggedDate.Value.ToString() );
                        body = body.Replace("[company]", portfolio.PortFolio);
                        body = body.Replace("[bank]", portfolio.BankName);
                        body = body.Replace("[account]", portfolio.AccountNumber);
                        body = body.Replace("[taxreg]", portfolio.TaxReg);
                        body = body.Replace("[sortcode]", portfolio.SortCode);
                        body = body.Replace("[iban]", portfolio.IBAN);
                        body = body.Replace("[swiftcode]", portfolio.SwiftCode);
                        body = body.Replace("[emailfooter]", afooter != null ? afooter.EmailFooter1.ToString() : string.Empty);
                        if ((pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) > 0)
                        {
                            var tPrice = string.Format("{0:F2}", (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tTax = string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) - (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tStr = string.Format("<tr><td>Your Price:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tPrice);
                            tStr = tStr + string.Format("<tr><td>Tax:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tTax);
                            body = body.Replace("[specialprice]", tStr);

                            body = body.Replace("[gtotal]", string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00)));
                        }
                        else
                        {
                            body = body.Replace("[specialprice]", string.Empty);
                            body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                        }

                        //[specialprice]

                        //   var uqid = GenerateButtonUrlNew(fls.Company, userid, fls.CallID, fls.CCID, pricelist.ID);
                        //   body = body.Replace("[link]", string.Format("{0}/WF/payinvoice/PaymentProcess.aspx?uqid={1}", Deffinity.systemdefaults.GetWebUrl(), uqid));

                        //[date]

                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        Email er = new Email();
                        List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + fls.CallID.ToString() + "-" + pricelist.ID.ToString())))
                        {
                            string[] S1 = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + fls.CallID.ToString() + "-" + pricelist.ID.ToString()));
                            foreach (string fileName in S1)
                            {
                                a.Add(new System.Net.Mail.Attachment(fileName));
                            }
                        }

                        if (tlist != null)
                        {
                            if (tlist.Count > 0)
                            {
                                foreach (var t in tlist)
                                {
                                    var body1 = body;
                                    //if help desk or assign users are changed then mail should go to requester
                                    body1 = body1.Replace("[user]", t.name);
                                    //insert pay now button generated url



                                    er.SendingMail(t.email, subject, body, fromemailid, a);

                                    // em.SendingMail(fromemailid, subject, body, t.email);
                                }
                            }
                            else
                            {
                                //if help desk or assign users are changed then mail should go to requester
                                body = body.Replace("[user]", pcontact.Name);
                                //insert pay now button generated url


                                er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                                //  em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            }

                        }
                        else
                        {
                            //if help desk or assign users are changed then mail should go to requester
                            body = body.Replace("[user]", pcontact.Name);
                            //insert pay now button generated url

                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);

                            // em.SendingMail(fromemailid, subject, body, pcontact.Email);

                        }



                        //update the status
                        InvoiceBAL.UpdateInvoiceStatus_SendtoCustomer(fls.CallID, pricelist.ID);

                        // addtocomunications(fromemailid, fls, pcontact, subject, body);

                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static string GenerateButtonUrlNew(int portfolioid, int userid, int callid, int ccid, int invoiceid)
        {
            string retval = string.Empty;
            try
            {
                var udate = string.Format("portfolioid:{0},userid:{1},callid:{2},ccid:{3},invid:{4}", portfolioid, userid, callid, ccid, invoiceid);
                var uqid = Guid.NewGuid().ToString();
                var d = DC.BLL.MailDataProcessBAL.MailDataProcess_Add(new MailDataProcess() { UQID = uqid, IsActive = true, UQValues = udate });
                if (d != null)
                {
                    retval = d.UQID;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        public static string FundrisersProcessPayment(int portfolioid,string cardtype, string cardnumber, string month_year, double amount, string cvv, string refno, string nameoncard,string payref,string recurringtype,string startdate)
{

            string retval = "";

            try
            {

                var refdata = addpayrefdata(amount.ToString(), refno, cardnumber, cvv, month_year, month_year, nameoncard, "", "", "", "", refno, sessionKeys.UID.ToString(), sessionKeys.PortfolioID.ToString(), sessionKeys.UName, sessionKeys.PartnerID.ToString(), 0,payref);
                //var retval = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value, out invoiceid);

                HttpContext.Current.Session["amount"] = amount;
                HttpContext.Current.Session["invoiceref"] = refno;
                HttpContext.Current.Session["cardnumber"] = cardnumber;
                HttpContext.Current.Session["cvv"] = cvv;
                HttpContext.Current.Session["month"] = "";
                HttpContext.Current.Session["year"] = "";
                HttpContext.Current.Session["customername"] = "";
                HttpContext.Current.Session["customeremail"] = "";
                HttpContext.Current.Session["address"] = "";
                HttpContext.Current.Session["postcode"] = "";
                HttpContext.Current.Session["phone"] = "";
                HttpContext.Current.Session["customerref"] = refno;// retval.PayID;
                HttpContext.Current.Session["bookingunid"] = refno;

                HttpContext.Current.Session["recurring"] = recurringtype;
                HttpContext.Current.Session["startdate"] = startdate;

                HttpContext.Current.Session["payref"] = payref;

                retval = refdata.refid;

                //  rettext = refdata.refid;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;


        }


        public static string SMSPay(int portfolioID, string packageid,
        double amount
       )
        {
            string rettext = "";
            try
            {
                rettext = "";
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var ccnumber = CardNumber;
                //if (string.IsNullOrEmpty(CardNumber))
                //{
                //    ccnumber = CardNumber;
                //    //if (string.IsNullOrEmpty(ccnumber))
                //    //{
                //    //    lblCardERROR.Text = "Please enter card number";
                //    //    return;
                //    //}
                //    LogExceptions.LogException("mytoken:" + ccnumber);

                //}



                var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                var payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

                var activepay = payRep.GetAll().Where(o => o.PortfolioID == portfolioID && o.IsActive == true).FirstOrDefault();
                if (activepay == null)
                {
                    payDetials = payRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
                }
                else
                {
                    payDetials = activepay;
                }

                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();
                //var mRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategoryAmount>();
                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();

                //LogExceptions.LogException(" address id" + flsDetails.ContactAddressID);
                //LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                var p = new PortfolioMgt.Entity.SMSPackageDetail();
                p.PackageID = Convert.ToInt32( packageid);
                p.PortfolioID = portfolioID;// flsDetails.ContactAddressID.HasValue ? flsDetails.ContactAddressID.Value : 0;
                p.IsPaid = false;
                p.Amount = Convert.ToDouble(amount);
                p.DateAdded = DateTime.Now;
                p.DateModified = DateTime.Now;
                p.PaidBy = sessionKeys.UID;
                var payref = Deffinity.Utility.GetSevenCharRandomString();
                p.PayRef = payref ;
                //p.ProductQTY = qty;
                //p.ProductPrice = amount * qty;
                //p.PaidDate = DateTime.Now;
                //p.OrderDate = DateTime.Now;
                //p.PaymentResult = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", payDetials.Vendor, ccnumber, cardtype, month + year, CVV);


                //p.CuseromerAddress = contactaddress;
                //p.CuseromerContactNo = contactnumber;
                //p.CuseromerState = contactstate;
                //p.CuseromerTown = contacttown;
                //p.CuseromerZipCode = contactzipcode;
                //p.CustomerEmail = contactemail;
                //p.CustomerFirstName = contactfirstname;
                //p.CustomerLastName = contactlastname;
                //p.ProductSaleGuid = Guid.NewGuid().ToString();
                // p.PayOnWebsite = false;
                //p.Payref = pref;
                //p.note = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;

                pmRep.Add(p);



             
                //  LogExceptions.LogException(" added PortfolioContactPaymentDetail:");
                var month_year_expiry = "";
                var cvv = "";
                var MID = payDetials.Vendor;
                //var amount = (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();

                string retval = string.Empty;
                LogExceptions.LogException("start credit card processign :");
                var refdata = addpayrefdata((amount ).ToString(), p.ID.ToString(), "", cvv, "", month_year_expiry, "", "", "", "", "", p.ID.ToString(), sessionKeys.UID.ToString(), sessionKeys.PortfolioID.ToString(), sessionKeys.UName, sessionKeys.PartnerID.ToString(), p.ID,payref);
                //var retval = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value, out invoiceid);

                HttpContext.Current.Session["amount"] = amount ;
                HttpContext.Current.Session["invoiceref"] = p.ID;
                HttpContext.Current.Session["cardnumber"] = "";
                HttpContext.Current.Session["cvv"] = cvv;
                HttpContext.Current.Session["month"] = month_year_expiry;
                HttpContext.Current.Session["year"] = month_year_expiry;
                HttpContext.Current.Session["customername"] = "";
                HttpContext.Current.Session["customeremail"] = "";
                HttpContext.Current.Session["address"] = "";
                HttpContext.Current.Session["postcode"] = "";
                HttpContext.Current.Session["phone"] = "";
                HttpContext.Current.Session["customerref"] = p.ID.ToString();// retval.PayID;

                HttpContext.Current.Session["payref"] = payref;

                rettext = refdata.refid;


                HttpContext.Current.Response.Redirect("~/PayProcess.aspx?frm=sms&refid=" + refdata.refid + "&type=" + payDetials.PayType, false);
                LogExceptions.LogException(retval);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if(rval.Where(o=>o.key == ""))
            //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
            //pE.PayPalRef = RecurResponse.TrxPNRef;
            //pE.IsPaid = true;
            //pmRep.Edit(pE);
            ////Genrate policy and generate pdf
            ////GeneratePolicy.SendPolicyMail(addressid);
            //lblResult.Text = TrxnResponse.RespMsg;
            //var PNREF = TrxnResponse.Pnref;
            //var PPREF = TrxnResponse.PPref;
            //pnlResult.Visible = true;
            //pnlCCD.Visible = false;
            return rettext;
        }


    }

}
