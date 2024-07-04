using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using TuesPechkin;

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignTemplateDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    controlsbind(QueryStringValues.CTID);
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void controlsbind(int id)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var m = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                if (m != null)
                {
                    lblTemplateName.Text = m.TemplateName;
                    if (m.ScheduledStartDate.HasValue)
                    {
                        txtDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), m.ScheduledStartDate);
                        txtTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), m.ScheduledStartDate);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplateTags.aspx?CTID=" + QueryStringValues.CTID, false);
            if (QueryStringValues.CSID > 0)
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateTags.aspx?CTID={0}&CSID={1}", +QueryStringValues.CTID, QueryStringValues.CSID), false);
            }
            else
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateTags.aspx?CTID={0}", +QueryStringValues.CTID), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var tmp = cRep.GetAll().Where(o => o.ID == QueryStringValues.CTID).FirstOrDefault();

                if (tmp != null)
                {
                    tmp.ScheduledStartDate = Convert.ToDateTime(txtDate.Text.Trim() + " " + (string.IsNullOrEmpty(txtTime.Text.Trim()) ? "00:00:00" : txtTime.Text.Trim() + ":00"));
                    cRep.Edit(tmp);
                    /// add data in scheduler
                    var retval = CampaignTemplateBAL.CampaignTemplate_Schedule_IsExists(QueryStringValues.CTID, tmp.ScheduledStartDate.Value);
                    if(!retval)
                    {
                        var c = new PortfolioMgt.Entity.CampaignTemplate_Schedule();
                        c.IsMailSent = false;
                        c.LoggedBy = sessionKeys.UID;
                        c.LoggedDate = DateTime.Now;
                        c.ScheduledEndDate = tmp.ScheduledStartDate.Value.AddDays(1);
                        c.ScheduledStartDate = tmp.ScheduledStartDate.Value;
                        c.TemplateID = tmp.ID;

                        CampaignTemplateBAL.CampaignTemplate_Schedule_Add(c);
                    }
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignList.aspx", false);
                }

                else
                {
                    lblError.Text = "Please enter scheduled date";
                }
            }
            
        }

        protected void btnSendNow_Click(object sender, EventArgs e)
        {
            try
            {
                SendMailSelectedData(QueryStringValues.CTID);

                //}
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Email has been sent successfully");
        }


        public static void SendMailSelectedData(int CTID)
        {
            try
            {
                int cnt = 0;
                var cDatetime = DateTime.Now;
                //var templateList = CampaignTemplateBAL.CampaignTemplate_SelectAll(sessionKeys.PortfolioID).Where(o=>o.PortfolioID (QueryStringValues.CTID);

                //foreach (var t in templateList)
                //{
                //get the template 
                var t_plate = CampaignTemplateBAL.CampaignTemplate_SelectByID(CTID);


                if (t_plate != null)
                {
                    var t_plate_tags = t_plate.Tags;
                    var t_plate_content = t_plate.TemplateContent;
                    var t_plate_subject = t_plate.Subject;
                    var portfolioID = t_plate.PortfolioID;
                    List<UserMgt.Entity.v_contractor> contactsList = new List<UserMgt.Entity.v_contractor>();

                    //get the tags base customers
                    if (t_plate_tags.ToLower().Contains("all donors"))
                    {
                        //skip admins
                        contactsList = CampaignTemplateBAL.PorfolioContact_SelectByTag(t_plate_tags, portfolioID).Where(o => o.SID != 1).ToList();
                    }
                    else
                    {
                        contactsList = CampaignTemplateBAL.PorfolioContact_SelectByTag(t_plate_tags, portfolioID).ToList();
                    }


                    //Compose the mail
                    foreach (var c in contactsList)
                    {
                        //check this customer has mail sent already
                        //var bVal = CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_IsExists(c.ID, cDatetime);
                        //if (!bVal)
                        //{
                        try
                        {
                            //IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                            //var tn = rp.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.SetAsDefault == true).FirstOrDefault();

                            //IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> rpep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                            //var tnrep = rpep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.SetAsDefault == true).FirstOrDefault();

                            //IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                            //var dItem = rpNew.GetAll().Where(o => o.DonerEmail.ToLower().Trim() == c.EmailAddress.ToLower().Trim()).FirstOrDefault();



                            //if (tn == null)
                            //    tn = rp.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
                            string body = t_plate_content;


                            body = body.Replace("{{donorfirstname}}", c.ContractorName);
                            body = body.Replace("{{donorsurname}}", c.LastName ?? "");

                            body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                            body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                            body = body.Replace("{{signature}}", c.CompanyName);
                            body = body.Replace("{{instancename}}", c.CompanyName);
                            body = body.Replace("{{donorcompany}}", c.CompanyName);
                            // Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) 
                            body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(c.CompanyID.Value, Deffinity.systemdefaults.GetLocalPath()) + "' />");
                            body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                            //    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                            //    body = body.Replace("{{donorsurname}}", dItem.DonerName);




                            if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                            {
                                Emailer em = new Emailer();
                                string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                                html_body = html_body.Replace("[table]", body);
                                body = html_body;

                                string fromid = Deffinity.systemdefaults.GetFromEmail();

                                string toid = c.EmailAddress;
                                string subject = t_plate_subject;
                                //htomail.Value = toid;
                                //hsubject.Value = subject;
                                //CKEditor1.Text = body;
                                Email ToEmail = new Email();


                                ToEmail.SendingMail(fromid, subject, body, toid, "");
                                cnt = cnt + 1;


                            }

                            // WinSendingMail("service@123servicepro.com", t_plate_subject, body, c.Email, t.TemplateID);

                            //  LogExceptions.LogException("to mail:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

                            //update the customer record
                            var cHistory = new CampaignTemplate_ScheduleHistory();
                            cHistory.ContactID = c.ID;
                            cHistory.IsMailSent = true;
                            cHistory.MailSentOn = cDatetime;
                            cHistory.ScheduledEndDate = DateTime.Now;
                            cHistory.ScheduledStartDate = DateTime.Now;
                            cHistory.TemplateID = CTID;

                            CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_Add(cHistory);
                        }
                        catch (Exception ex)
                        { LogExceptions.WriteExceptionLog(ex); }
                        //if (cnt > 0)
                        //    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Email has been sent successfully");
                        //}
                        //else
                        //{
                        //    LogExceptions.LogException("Faild is exist condition:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

                        //}
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

      //  private void Update 

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
        //public void CampainMainSending(object state)
        //{
        //    LogExceptions.LogException("Executed at: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        //    var cDatetime = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        //    DateTime currentdate = TimeZoneInfo.ConvertTime(cDatetime, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneID("GMT+00:00")));
        //    //get the list of scheudled mails
        //    var templateList = CampaignTemplateBAL.V_CampaignTemplate_Schedule_SelectByCurrentDate(cDatetime);

        //    foreach (var t in templateList)
        //    {
        //        //get the template 
        //        var t_plate = CampaignTemplateBAL.CampaignTemplate_SelectByID(t.TemplateID);
        //        if (t_plate != null)
        //        {
        //            var t_plate_tags = t_plate.Tags;
        //            var t_plate_content = t_plate.TemplateContent;
        //            var t_plate_subject = t_plate.Subject;
        //            var portfolioID = t.PortfolioID;
        //            //get the tags base customers
        //            var contactsList = CampaignTemplateBAL.PorfolioContact_SelectByTag(t_plate_tags, portfolioID);
        //            //Compose the mail
        //            foreach (var c in contactsList)
        //            {
        //                //check this customer has mail sent already
        //                var bVal = CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_IsExists(c.ID, cDatetime);
        //                if (!bVal)
        //                {
        //                    try
        //                    {
        //                        string body = t_plate_content;

        //                        //body = body.Replace("[logo]", string.Format("<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Customers/portfolio_{0}.png?d=1'/>", portfolioID));
        //                        //body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
        //                        //body = body.Replace("[name]", c.Name);
        //                        //body = body.Replace("[email]", c.Email);
        //                        //body = body.Replace("[contact]", c.Mobile);

        //                        //body = body.Replace("[Logo]", string.Format("<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Customers/portfolio_{0}.png?d=1'/>", portfolioID));
        //                        //body = body.Replace("[Border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
        //                        //body = body.Replace("[Name]", c.Name);
        //                        //body = body.Replace("[Email]", c.Email);
        //                        //body = body.Replace("[Contact]", c.Mobile);



        //                        body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
        //                        body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
        //                        body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
        //                        body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
        //                        body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
        //                        body = body.Replace("{{name}}", dItem.DonerName);
        //                        body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));
        //                        body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
        //                        body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


        //                        //  body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount));

        //                        body = body.Replace("{{donorfirstname}}", dItem.DonerName);
        //                        body = body.Replace("{{donorsurname}}", dItem.DonerName);
        //                        //donorcompany
        //                        //  body = body.Replace("{{category}}", dItem.CategoryList);

        //                        body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);



        //                        body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));

        //                        body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
        //                        body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
        //                        //logo

        //                        body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");




        //                        WinSendingMail("service@123servicepro.com", t_plate_subject, body, c.Email, t.TemplateID);

        //                        LogExceptions.LogException("to mail:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

        //                        //update the customer record
        //                        var cHistory = new CampaignTemplate_ScheduleHistory();
        //                        cHistory.ContactID = c.ID;
        //                        cHistory.IsMailSent = true;
        //                        cHistory.MailSentOn = cDatetime;
        //                        cHistory.ScheduledEndDate = t.ScheduledEndDate;
        //                        cHistory.ScheduledStartDate = t.ScheduledStartDate;
        //                        cHistory.TemplateID = t.TemplateID;

        //                        CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_Add(cHistory);
        //                    }
        //                    catch (Exception ex)
        //                    { LogExceptions.WriteExceptionLog(ex); }
        //                }
        //                else
        //                {
        //                    LogExceptions.LogException("Faild is exist condition:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

        //                }
        //            }

        //        }

        //    }


        //}

        public void WinSendingMail(string FromEmailID, string Subject, string strHTML, string ToEmailID, int templateID = 0)
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(ToEmailID);          // For receiver's address  
                message.From = new MailAddress(FromEmailID);  // For sender's address  
                message.Subject = Subject;         //  For Subject of the mail  
                //message.Body = HttpUtility.HtmlDecode(Body); //  For Body of the mail  
                message.Body = strHTML;
                message.IsBodyHtml = true;

                if (templateID > 0)
                {
                    string FilesPath = ConfigurationManager.AppSettings["Filepath"] + templateID.ToString();
                    if (FilesPath.Length > 0)
                    {
                        bool AttachFile = false;
                        if (System.IO.Directory.Exists(FilesPath))
                        {
                            string[] filePaths = Directory.GetFiles((FilesPath));
                            foreach (string filePath in filePaths)
                            {
                                AttachFile = true;
                                System.Net.Mail.Attachment attachment;
                                string t1 = Path.GetFileName(filePath);
                                attachment = new System.Net.Mail.Attachment(filePath);
                                message.Attachments.Add(attachment);
                            }
                        }
                    }
                }

                //Email e = new Email();
                //e.SendingMail()


                SmtpClient mailClient = new SmtpClient();
                //System.Net.NetworkCredential basicAuthentication =
                //      new System.Net.NetworkCredential(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["pwd"]);
                //mailClient.Host = ConfigurationManager.AppSettings["host"];
                //mailClient.UseDefaultCredentials = false;
                //mailClient.Credentials = basicAuthentication;
                try
                {
                    //LogException.WriteException(nBody);
                    mailClient.Send(message);
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        }

        // System.Net.Mail.MailMessage message = null;
        public string ReadFilehtml(string FileName)
        {
            try
            {
                string FILENAME = FileName;
                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    string contents = objstreamreader.ReadToEnd();
                    return contents;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return "";
        }

    }
}