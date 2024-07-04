using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class MailActivation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var unid = QueryStringValues.UNID;

                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(unid)).FirstOrDefault();

                if (p != null)
                {
                    var userid = p.AdminID;
                    if (userid != null)
                    {
                        IUserRepository<UserMgt.Entity.Contractor> cRep = new UserRepository<UserMgt.Entity.Contractor>();
                        var c = cRep.GetAll().Where(o => o.ID == userid).FirstOrDefault();

                        if (c != null)
                        {
                            if (cRep.GetAll().Where(o => o.ID == userid && o.isFirstlogin == 1).Count() > 0)
                            {
                                sendmailAfterActivation(c, p);
                            }

                            c.isFirstlogin = 0;
                            cRep.Edit(c);
                        }
                    }

                }
            }

        }


        public string sendmailAfterActivation( UserMgt.Entity.Contractor c, PortfolioMgt.Entity.ProjectPortfolio portfolio)
        {
            try
            {
                //data.partner
                LogExceptions.LogException("send mail method");

                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = string.Format("GREAT NEWS! Your {0} Portal Is Ready.", Deffinity.systemdefaults.GetInstanceTitle());
                string fromemail = Deffinity.systemdefaults.GetFromEmail();

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = Deffinity.systemdefaults.GetInstanceTitle();
                string siteurl = Deffinity.systemdefaults.GetWebUrl();
                //siteurl = "http://www.wisal.cloud";
                //displayName = "Wisal";
                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/Content/emailtemplate/newinstancewisal.htm");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }
                //mail to owner
                if (c != null)
                {
                    string s = string.Empty;
                    if (c.ContactNumber != null)
                    {
                        var tempContent = contents;



                        ////if (partnerentity != null)
                        ////{
                        ////    tempContent = tempContent.Replace("[logo]", string.Format("{0}/assets/media/logos/logo-1.png?d=57", Deffinity.systemdefaults.GetWebUrl()));
                        ////    tempContent = tempContent.Replace("[portalname]", Deffinity.systemdefaults.GetInstanceTitle());
                        ////    tempContent = tempContent.Replace("[supportmail]", Deffinity.systemdefaults.GetFromEmail());
                        ////    //fromemail = partnerentity.FromEmail;
                        ////    var webName = Deffinity.systemdefaults.GetInstanceTitle();
                        ////    fromemail = Deffinity.systemdefaults.GetFromEmail();// partnerentity.FromEmail;
                        ////    subject = string.Format("GREAT NEWS! Your {0} Portal Is Ready.", webName);

                        ////    // else
                        ////    //    tempContent = tempContent.Replace("[url]", partnerentity.ParnerPortal.Contains("https") ? partnerentity.ParnerPortal : "https://" + partnerentity.ParnerPortal);
                        ////}

                        tempContent = tempContent.Replace("[loginurl]", siteurl);


                        tempContent = tempContent.Replace("[url]", siteurl);

                        tempContent = tempContent.Replace("[user]", c.ContractorName);
                        tempContent = tempContent.Replace("[site]", siteurl);
                        tempContent = tempContent.Replace("[urlroot]", siteurl);
                        tempContent = tempContent.Replace("[displayname]", portfolio.PortFolio);

                        //[name]
                        //foreach (var c in udetails)
                        //{
                        tempContent = tempContent.Replace("[name]", c.ContractorName +" "+ c.LastName);
                        s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", c.ContractorName + " " + c.LastName, c.LoginName, c.TypeofLogin);
                        //}
                        tempContent = tempContent.Replace("[datarow]", s);
                        //if mail gives exception
                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, c.EmailAddress, "");
                            LogExceptions.LogException("send mail method - to :", c.EmailAddress + "-from email: " + fromemail);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(c.EmailAddress + " failed to send mail " + ex);
                        }

                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, "indra@deffinity.com", "");
                            if (!c.EmailAddress.ToLower().Contains("indra"))
                            {
                                em.SendingMail(fromemail, subject, tempContent, "nadeem.mohammed@deffinity.com", "");
                            }
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
        protected void btnPortal_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl(),false);
        }
    }
}