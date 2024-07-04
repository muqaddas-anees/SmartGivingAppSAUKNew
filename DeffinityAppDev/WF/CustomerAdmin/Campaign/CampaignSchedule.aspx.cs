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

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(sessionKeys.Message))
                {
                    lblMsg.Text = sessionKeys.Message;
                    sessionKeys.Message = string.Empty;
                }

                BindGrid();

            }
        }

        private void BindGrid()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.V_CampaignTemplate_Schedule> cRep = new PortfolioRepository<PortfolioMgt.Entity.V_CampaignTemplate_Schedule>();
                var tmp = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                //if (tmp.Count > 0)
                //{
                GridList.DataSource = (from t in tmp
                                       orderby t.ID descending
                                       select new
                                       {
                                           t.ID,
                                           t.Tags,
                                           //t.Subject,
                                           t.TemplateName,
                                           StartDate = (t.ScheduledStartDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", t.ScheduledStartDate.Value) : string.Empty),
                                           EndDate = (t.ScheduledEndDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", t.ScheduledEndDate.Value.AddDays(-1)) : string.Empty),

                                       }).ToList();
                GridList.DataBind();
                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public string SetTagCss(string tags)
        {
            string retval = string.Empty;
            if (tags != null)
            {
                if (tags.Trim().Length > 0)
                {
                    retval = "<div class='bootstrap-tagsinput'>";
                    var s = string.Empty;
                    string[] str = tags.Split(',');
                    foreach (var s1 in str)
                    {
                        if (!string.IsNullOrEmpty(s1))
                            s = s + "<span class='tag label label-black'>" + s1 + "</span> ";
                    }
                    retval = retval + s + "</div>";
                }
            }
            return retval;
        }
        protected void GridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                    Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID=" + Convert.ToInt32(e.CommandArgument.ToString()), false);
                }
                else if (e.CommandName == "del")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate_Schedule> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate_Schedule>();
                    var tmp = cRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (tmp != null)
                    {
                        cRep.Delete(tmp);
                        lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;

                        BindGrid();
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        public static string GetTimeZoneID(string val="")
        {
            string retval = "South Africa Standard Time";
            //if (val.ToUpper().Contains("GMT-08:00"))
            //    retval = "Pacific Standard Time";
            //else if (val.ToUpper().Contains("GMT+00:00"))
            //    retval = "GMT Standard Time";
            //else if (val.ToUpper().Contains("GMT+05:30"))
            //    retval = "India Standard Time";
            return retval;
        }


        public static void CampainMainSending()
        {
           // LogExceptions.LogException("Executed at: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            var cDatetime = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            DateTime currentdate = TimeZoneInfo.ConvertTime(cDatetime, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneID()));
            //get the list of scheudled mails
            var templateList = CampaignTemplateBAL.V_CampaignTemplate_Schedule_SelectByCurrentDate(cDatetime);

            foreach (var t in templateList)
            {

                ////get the template 
                var t_plate = CampaignTemplateBAL.CampaignTemplate_SelectByID(t.TemplateID);
                if (t_plate != null)
                {
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


                            DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignTemplateDate.SendMailSelectedData(t.TemplateID);
                            //try
                            //{
                            //    string body = t_plate_content;

                            //    //body = body.Replace("[logo]", string.Format("<img src='"+ Deffinity.systemdefaults.GetWebUrl()+"/WF/UploadData/Customers/portfolio_{0}.png?d=1'/>", portfolioID));
                            //    //body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl()  + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                            //    //body = body.Replace("[name]", c.Name);
                            //    //body = body.Replace("[email]", c.Email);
                            //    //body = body.Replace("[contact]", c.Mobile);

                            //    //body = body.Replace("[Logo]", string.Format("<img src='"+ Deffinity.systemdefaults.GetWebUrl()+"/WF/UploadData/Customers/portfolio_{0}.png?d=1'/>", portfolioID));
                            //    //body = body.Replace("[Border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                            //    //body = body.Replace("[Name]", c.Name);
                            //    //body = body.Replace("[Email]", c.Email);
                            //    //body = body.Replace("[Contact]", c.Mobile);




                            //    //WinSendingMail("service@123servicepro.com", t_plate_subject, body, c.Email, t.TemplateID);

                            //    //LogExceptions.LogException("to mail:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

                            //    //update the customer record
                            //    var cHistory = new CampaignTemplate_ScheduleHistory();
                            //    cHistory.ContactID = c.ID;
                            //    cHistory.IsMailSent = true;
                            //    cHistory.MailSentOn = cDatetime;
                            //    cHistory.ScheduledEndDate = t.ScheduledEndDate;
                            //    cHistory.ScheduledStartDate = t.ScheduledStartDate;
                            //    cHistory.TemplateID = t.TemplateID;

                            //    CampaignTemplateBAL.CampaignTemplate_ScheduleHistory_Add(cHistory);
                            //}
                            //catch (Exception ex)
                            //{ LogExceptions.WriteExceptionLog(ex); }
                        }
                        else
                        {
                            // LogExceptions.LogException("Faild is exist condition:" + "portfolio:" + portfolioID + "customer name:" + c.Name + " customer email:" + c.Email + " tags:" + t_plate_tags);

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