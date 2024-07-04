using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LogExceptions.LogException("Schedule date time"+ DateTime.Now);
                CampainMainSending(null);
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {

        }
        public string GetTimeZoneID(string val)
        {
            string retval = "Pacific Standard Time";
            if (val.ToUpper().Contains("GMT-08:00"))
                retval = "Pacific Standard Time";
            else if (val.ToUpper().Contains("GMT+00:00"))
                retval = "GMT Standard Time";
            else if (val.ToUpper().Contains("GMT+05:30"))
                retval = "India Standard Time";
            return retval;
        }
        public void CampainMainSending(object state)
        {
            var cnt = 0;
            LogExceptions.LogException("Executed at: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            var cDatetime = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            DateTime currentdate = TimeZoneInfo.ConvertTime(cDatetime, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneID("GMT+00:00")));
            //get the list of scheudled mails
            var templateList = CampaignTemplateBAL.V_CampaignTemplate_Schedule_SelectByCurrentDate(cDatetime);

            foreach (var t in templateList)
            {
                //get the template 
                var t_plate = CampaignTemplateBAL.CampaignTemplate_SelectByID(t.TemplateID);
                if (t_plate != null)
                {
                    cnt = 0;
                    var t_plate_tags = t_plate.Tags;
                    var t_plate_content = t_plate.TemplateContent;
                    var t_plate_subject = t_plate.Subject;
                    var portfolioID = t.PortfolioID;
                    //get the tags base customers
                    var contactsList = CampaignTemplateBAL.PorfolioContact_SelectByTag(t_plate_tags, portfolioID);
                    //Compose the mail
                    foreach (var c in contactsList)
                    {
                        //check this customer has mail sent already
                        var bVal = CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_IsExists(c.ID, t.TemplateID, cDatetime);
                        if (!bVal)
                        {
                            try
                            {
                               // string body = t_plate_content;

                                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                                var tn = rp.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.SetAsDefault == true).FirstOrDefault();

                                //IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> rpep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                                //var tnrep = rpep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.SetAsDefault == true).FirstOrDefault();

                                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                var dItem = rpNew.GetAll().Where(o => o.DonerEmail.ToLower().Trim() == c.EmailAddress.ToLower().Trim()).FirstOrDefault();



                                //if (tn == null)
                                //    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                                string body = t_plate_content;



                                body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                                body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                                body = body.Replace("{{signature}}", c.CompanyName);
                                body = body.Replace("{{instancename}}", c.CompanyName);
                                body = body.Replace("{{donorcompany}}", c.CompanyName);
                                body = body.Replace("{{donorfirstname}}",c.ContractorName.Split(' ').First());
                                body = body.Replace("{{donorsurname}}", c.ContractorName.Split(' ').Last());
                                body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(c.CompanyID.Value, Deffinity.systemdefaults.GetLocalPath()) + "' />");
                                body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                                //    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                                //    body = body.Replace("{{donorsurname}}", dItem.DonerName);

                                //if (dItem != null)
                                //{

                                //    body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                                //    body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
                                //    body = body.Replace("{{name}}", dItem.DonerName);
                                //    body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));

                                //    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());
                                //    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                                //    body = body.Replace("{{donorsurname}}", dItem.DonerName);
                                //    body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
                                //    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                                //}

                                //logo



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


                                   // ToEmail.SendingMail(fromid, subject, body, toid, "");

                                    WinSendingMail(fromid, subject, body, toid, t.TemplateID);
                                    cnt = cnt + 1;


                                }

                                //update the customer record
                                var cHistory = new CampaignTemplate_ScheduleHistory();
                                cHistory.ContactID = c.ID;
                                cHistory.IsMailSent = true;
                                cHistory.MailSentOn = cDatetime;
                                cHistory.ScheduledEndDate = t.ScheduledEndDate;
                                cHistory.ScheduledStartDate = t.ScheduledStartDate;
                                cHistory.TemplateID = t.TemplateID;

                                CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_Add(cHistory);
                            }
                            catch (Exception ex)
                            { LogExceptions.WriteExceptionLog(ex); }
                        }
                        else
                        {
                            //LogExceptions.LogException("Faild is exist condition:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

                        }
                    }

                }

            }


        }
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