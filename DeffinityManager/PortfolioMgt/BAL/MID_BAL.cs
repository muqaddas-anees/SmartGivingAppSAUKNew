using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using DeffinityManager.PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioMgt.BAL
{
    public static class MID_BAL
    {
        public static string _fromemail = "service@.plegit.africa";
        public static string sendlogindetails(int userid)
        {
            try
            {
                var webName = Deffinity.systemdefaults.GetInstanceTitle();
                //get user data
                var uData = UserMgt.BAL.ContractorsBAL.v_Contractor_SelectByID(userid);
                var pData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(uData.CompanyID.HasValue ? uData.CompanyID.Value : 0);
                //LogExceptions.LogException("send mail method");

                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = string.Format("Your {0} Portal is now ready.", webName);
                string fromemail = _fromemail;

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = webName;
                string siteurl = pData.BaseUrl;

                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/App/EmailTemplates/newinstancewisal.htm");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }
                //mail to owner
                if (uData != null)
                {
                    string s = string.Empty;
                    if (uData.ContactNumber != null)
                    {
                        var tempContent = contents;
                        tempContent = tempContent.Replace("[user]", uData.ContractorName);
                        tempContent = tempContent.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                        tempContent = tempContent.Replace("[site]", Deffinity.systemdefaults.GetWebUrl());

                        tempContent = tempContent.Replace("[urlroot]", Deffinity.systemdefaults.GetWebUrl());
                        tempContent = tempContent.Replace("[displayname]", uData.CompanyName);
                        tempContent = tempContent.Replace("[loginurl]", Deffinity.systemdefaults.GetWebUrl());
                        tempContent = tempContent.Replace("[supportmail]", Deffinity.systemdefaults.GetFromEmail());
                        //[supportmail]
                        //[loginurl]
                        //[name]
                        //foreach (var c in udetails)
                        //{
                        tempContent = tempContent.Replace("[name]", uData.ContractorName);
                        var pwd = string.Empty;
                        //get the password
                        if (pData.KeyContactName != null)
                        {
                            if (pData.KeyContactName.Length > 0)
                                pwd = pData.KeyContactName;
                        }
                        //if empty generate new one
                        if (pwd.Length == 0)
                        {
                            pwd = DeffinityManager.RandomPassword.GetPassword();
                        }
                        //update password
                        UserMgt.BAL.ContractorsBAL.Contractor_UpdatePassword(userid, pwd);


                        s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", uData.ContractorName, uData.LoginName, pwd);
                        //}
                        tempContent = tempContent.Replace("[datarow]", s);
                        //if mail gives exception
                        try
                        {
                            em.SendingMail(uData.EmailAddress, subject, tempContent, "", "", "");
                            //update date
                            pData.ResendLoginDateTime = DateTime.Now;
                            //password stored to send next time
                            pData.KeyContactName = pwd;
                            PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pData);
                            //update date 

                            LogExceptions.LogException("send mail method - to :", uData.EmailAddress);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(uData.EmailAddress + " failed to send mail " + ex);
                        }

                        try
                        {
                            //em.SendingMail("indra@deffinity.com", "Portal Login:" + subject, tempContent);
                            //if (!uData.EmailAddress.ToLower().Contains("indra"))
                            //{
                            //    //em.SendingMail("Instance@123smartpro.com", "Resend login:" + subject, tempContent);
                            //     em.SendingMail("nadeem.mohammed@deffinity.com", "Portal Login:" + subject, tempContent);
                            //    //em.SendingMail("123SignupEmailDistribution@deffinity.com", subject, tempContent);
                            //}
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException("mail to faild send" + ex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return "success";
        }
        public static bool SendMailToMembers(string orgGuid)
        {
            bool retval = false;
            try
            {

                //int callid = QueryStringValues.CallID;

                var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == orgGuid).FirstOrDefault();
               
                if (rData != null)
                {
                    //member not customers
                    var mList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == rData.ID && o.SID != 7).ToList();
                    Emailer em = new Emailer();
                    string body1 = em.ReadFile("~/App/EmailTemplates/SendtoMember.htm");


                    foreach (var m in mList)
                    {

                        string body = body1;
                        string subject = "Download the Smart Giving App now!";//"Job Reference: " + fls.CCID.ToString();

                        body = body.Replace("[name]", m.ContractorName);
                        //body = body.Replace("[mail_head]", "E-Signature");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() +"/assets/media/logos/logo-1-100.png");
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                        body = body.Replace("[faith]", rData.DenominationName);
                        body = body.Replace("[group]", rData.DenominationGroupName);
                        body = body.Replace("[denomination]", rData.SubDenominationName);


                        // em.SendingMail(_fromemail, subject, body, rData.EmailAddress);
                        //em.SendingMail(_fromemail, subject, body, m.EmailAddress);
                        //em.SendingMail(_fromemail, subject, body, "indra@deffinity.com");
                    }

                    retval = true;

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
            return retval;
        }
        //send mail to organization
        public static bool SendMailToOrganization(string orgGuid)
        {
            bool retval = false;
            try
            {

                //int callid = QueryStringValues.CallID;

                var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == orgGuid).FirstOrDefault();
                if (rData != null)
                {

                    string subject = "CARD CONNECT Application Information for E-Signature";//"Job Reference: " + fls.CCID.ToString();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/App/EmailTemplates/SendChurch.htm");
                    body = body.Replace("[name]", rData.BankName);
                    body = body.Replace("[mail_head]", "E-Signature");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() +"/assets/media/logos/logo-1-100.png");
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                  //  var btn = getButton(Deffinity.systemdefaults.GetWebUrl() +"midapplication.aspx?orgref=" +rData.OrgarnizationGUID, "View Form");


                    body = body.Replace("[link]", getButton(Deffinity.systemdefaults.GetWebUrl() + "//midapplication.aspx?orgref=" + rData.OrgarnizationGUID, "View Form"));

                    //[date]

                    //string Dis_body = body;
                    //bool ismailsent = false;


                   //em.SendingMail(_fromemail, subject, body, rData.EmailAddress);
                   // em.SendingMail(_fromemail, subject, body, "indra@deffinity.com");
                    retval = true;

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        public static string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#5014D0'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #5014D0; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }

        //send mail back to admin
        public static bool SendMailToAdmin(string orgGuid)
        {
            bool retval = false;
            try
            {

                var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == orgGuid).FirstOrDefault();
                if (rData != null)
                {

                    string subject = "CARD CONNECT Application Information for E-Signature";//"Job Reference: " + fls.CCID.ToString();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/App/EmailTemplates/SendtoAdmin.htm");
                    body = body.Replace("[name]", rData.BankName);
                    body = body.Replace("[mail_head]", "E-Signature");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() +"/assets/media/logos/logo-1-100.png");
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                    body = body.Replace("[Organization]", rData.PortFolio);
                    body = body.Replace("[contact]", rData.BankName);
                    body = body.Replace("[ContactNumber]", rData.TelephoneNumber);
                    body = body.Replace("[ContactEmail]", rData.EmailAddress);

                    //  var btn = getButton(Deffinity.systemdefaults.GetWebUrl() +"midapplication.aspx?orgref=" +rData.OrgarnizationGUID, "View Form");
                    //[Organization]
                    //[contact]
                    //[ContactNumber]
                    //[ContactEmail]

                    body = body.Replace("[link]", getButton(Deffinity.systemdefaults.GetWebUrl() + "//midapplication.aspx?v=1&orgref=" + rData.OrgarnizationGUID, "View Form"));

                    //[date]

                    //string Dis_body = body;
                    //bool ismailsent = false;

                    //1 is admins
                    var admins = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.PartnerID == 1).ToList();

                    foreach (var a in admins)
                    {

                         //em.SendingMail(_fromemail, subject, body,a.Username);
                    }
                    //em.SendingMail(_fromemail, subject, body, "indra@deffinity.com");
                    retval = true;

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }


        //send mail to card connect
        public static bool SendMailToCardConnect(string orgGuid)
        {
            bool retval = false;
            try
            {

                var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == orgGuid).FirstOrDefault();
                if (rData != null)
                {

                    string subject = "CARD CONNECT Application Information for E-Signature";//"Job Reference: " + fls.CCID.ToString();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/App/EmailTemplates/SendtoCardconnect.htm");
                    body = body.Replace("[name]", rData.BankName);
                    body = body.Replace("[mail_head]", "E-Signature");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() +"/assets/media/logos/logo-1-100.png");
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                    body = body.Replace("[Organization]", rData.PortFolio);
                    body = body.Replace("[contact]", rData.BankName);
                    body = body.Replace("[ContactNumber]", rData.TelephoneNumber);
                    body = body.Replace("[ContactEmail]", rData.EmailAddress);

                    //  var btn = getButton(Deffinity.systemdefaults.GetWebUrl() +"midapplication.aspx?orgref=" +rData.OrgarnizationGUID, "View Form");
                    //[Organization]
                    //[contact]
                    //[ContactNumber]
                    //[ContactEmail]

                    body = body.Replace("[link]", getButton(Deffinity.systemdefaults.GetWebUrl() + "//midapplication.aspx?v=2&orgref=" + rData.OrgarnizationGUID, "View Form"));

                    //[date]

                    //string Dis_body = body;
                    //bool ismailsent = false;

                    //1 is cardconnect admids
                    var admins = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.PartnerID == 2).ToList();

                    //foreach (var a in admins)
                    //{

                    //    em.SendingMail(_fromemail, subject, body, a.Username);
                    //}
                    //em.SendingMail(_fromemail, subject, body, "indra@deffinity.com");
                    retval = true;

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

    }
}
