using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using System.IO;

namespace DeffinityAppDev
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  btnsendagain.Visible = false;
                }
                lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();
                Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
                lblyear.InnerText = DateTime.Now.Year.ToString();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string getbackImageUrl()
        {
            return "../WF/UploadData/Customers/partnerback_org_" + sessionKeys.PartnerID + ".png?d=" + DateTime.Now.Second;
        }

        public string getImageUrl()
        {
            return "../WF/UploadData/Customers/partner_" + sessionKeys.PartnerID + ".png?d=" + DateTime.Now.Second;
        }
        public void PasswrdSendToMail(UserMgt.Entity.v_contractor con, string PwdLink)
        {
            try
            {
                var weburl = Deffinity.systemdefaults.GetWebUrl();

                Email em = new Email();
                string Body = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/DC/EmailTemplates/PasswordSendingmail.html"));
                Body = Body.Replace("[Logo]", weburl+"/" + Deffinity.systemdefaults.GetMailLogo());
                Body = Body.Replace("[border]", weburl + "/" + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                if (Body.Contains("[firstname]"))
                {
                    Body = Body.Replace("[firstname]", con.ContractorName);
                }
                if (Body.Contains("[username]"))
                {
                    Body = Body.Replace("[username]", con.LoginName);
                }
                if (Body.Contains("[Link]"))
                {
                    Body = Body.Replace("[Link]", PwdLink);
                }
                // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), con.ContractorName + " Password Reset", Body, con.EmailAddress, "");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 0);
        }
        protected void btnforgot_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim() != "")
                {
                    UsernameAndEmail();
                }
                else
                {
                    Username();
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private void Username()
        {
            ForgotPasswordInfo Fpwd = null;
            UserMgt.Entity.Contractor c = null;
            using (UserDataContext ud = new UserDataContext())
            {
                Fpwd = new ForgotPasswordInfo();
                var lc = new UserMgt.Entity.v_contractor();
                //not a donor
                lc = (from a in ud.v_contractors where a.LoginName.ToLower() == txtEmail.Text.ToLower().Trim() && a.SID !=2  select a).FirstOrDefault();
                if (c != null)
                {
                    UserMgt.Entity.ForgotPasswordInfo ForgotInfo = (from a in ud.ForgotPasswordInfos
                                                                    where a.Userid == c.ID && a.IsActive == true
                                                                    orderby (a.id) descending
                                                                    select a).FirstOrDefault();
                    if (ForgotInfo == null)
                    {
                        Guid vid = Guid.NewGuid();
                        string PwdLink = Deffinity.systemdefaults.GetWebUrl() + "/ResetPasswordNew.aspx?Vid=" + vid.ToString();
                        Fpwd.Userid = c.ID;
                        Fpwd.verifyCode = vid.ToString();
                        Fpwd.Currenttime = DateTime.Now;
                        Fpwd.IsActive = true;
                        ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                        F_BAL.InsertRecord(Fpwd);
                        PasswrdSendToMail(lc, PwdLink);

                        lblMsg.Text = "Please check your email.";
                    }
                    else
                    {

                        lblError.Text = "An email has already been sent to your email address. For a further reset email please ";
                        //btnsendagain.Visible = true;
                    }
                }
                else
                {

                    lblError.Text = "Please enter valid username.";
                }
            }
        }

        private void UsernameAndEmail()
        {
            using (UserDataContext ud = new UserDataContext())
            {
                ForgotPasswordInfo Fpwd = null;
                UserMgt.Entity.Contractor c = null;
                Fpwd = new ForgotPasswordInfo();
                var lc = new UserMgt.Entity.v_contractor();
                lc = (from a in ud.v_contractors where a.LoginName.ToLower() == txtEmail.Text.ToLower().Trim() select a).FirstOrDefault();
                if (lc != null)
                {
                    UserMgt.Entity.ForgotPasswordInfo ForgotInfo = (from a in ud.ForgotPasswordInfos
                                                                    where a.Userid == c.ID && a.IsActive == true
                                                                    orderby (a.id) descending
                                                                    select a).FirstOrDefault();
                    if (ForgotInfo == null)
                    {
                        Guid vid = Guid.NewGuid();
                        string PwdLink = Deffinity.systemdefaults.GetWebUrl() + "/ResetPasswordNew.aspx?Vid=" + vid.ToString();
                        Fpwd.Userid = c.ID;
                        Fpwd.verifyCode = vid.ToString();
                        Fpwd.Currenttime = DateTime.Now;
                        Fpwd.IsActive = true;
                        ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                        F_BAL.InsertRecord(Fpwd);
                        PasswrdSendToMail(lc, PwdLink);
                        // lblError.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Please check your email.";
                    }
                    else
                    {
                        //lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = "An email has already been sent to your email address. For a further reset email please ";
                       // btnsendagain.Visible = true;

                    }
                }
                else
                {
                    // lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please enter valid username and email.";
                }
            }
        }
        protected void lnllogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/Default.aspx");
        }
        protected void btnsendagain_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim() != "")
                {
                    using (UserDataContext ud = new UserDataContext())
                    {
                        ForgotPasswordInfo Fpwd = null;
                        //UserMgt.Entity.Contractor c = null;
                        Fpwd = new ForgotPasswordInfo();
                        var lc = new UserMgt.Entity.v_contractor();
                        lc = (from a in ud.v_contractors where a.LoginName.ToLower().Trim() == txtEmail.Text.ToLower().Trim() select a).FirstOrDefault();
                        if (lc != null)
                        {
                            UserMgt.Entity.ForgotPasswordInfo ForgotInfo = (from a in ud.ForgotPasswordInfos
                                                                            where a.Userid == lc.ID && a.IsActive == true
                                                                            orderby (a.id) descending
                                                                            select a).FirstOrDefault();
                            if (ForgotInfo != null)
                            {
                                ForgotInfo.IsActive = false;
                                ud.SubmitChanges();
                            }
                            Guid vid = Guid.NewGuid();
                            string PwdLink = Deffinity.systemdefaults.GetWebUrl() + "/ResetPasswordNew.aspx?Vid=" + vid.ToString();
                            Fpwd.Userid = lc.ID;
                            Fpwd.verifyCode = vid.ToString();
                            Fpwd.Currenttime = DateTime.Now;
                            Fpwd.IsActive = true;
                            ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                            F_BAL.InsertRecord(Fpwd);
                            PasswrdSendToMail(lc, PwdLink);
                            // lblError.ForeColor = System.Drawing.Color.Green;
                            lblMsg.Text = "Please check your email";
                           // btnsendagain.Visible = false;
                        }
                    }
                }
                else
                {
                    using (UserDataContext ud = new UserDataContext())
                    {
                        ForgotPasswordInfo Fpwd = null;
                        UserMgt.Entity.Contractor c = null;
                        Fpwd = new ForgotPasswordInfo();
                        var lc = new UserMgt.Entity.v_contractor();
                        lc = (from a in ud.v_contractors where a.LoginName.ToLower() == txtEmail.Text.ToLower().Trim() select a).FirstOrDefault();
                        if (lc != null)
                        {
                            UserMgt.Entity.ForgotPasswordInfo ForgotInfo = (from a in ud.ForgotPasswordInfos
                                                                            where a.Userid == c.ID && a.IsActive == true
                                                                            orderby (a.id) descending
                                                                            select a).FirstOrDefault();
                            if (ForgotInfo != null)
                            {
                                ForgotInfo.IsActive = false;
                                ud.SubmitChanges();
                            }
                            Guid vid = Guid.NewGuid();
                            string PwdLink = Deffinity.systemdefaults.GetWebUrl() + "/ResetPasswordNew.aspx?Vid=" + vid.ToString();
                            Fpwd.Userid = c.ID;
                            Fpwd.verifyCode = vid.ToString();
                            Fpwd.Currenttime = DateTime.Now;
                            Fpwd.IsActive = true;
                            ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                            F_BAL.InsertRecord(Fpwd);
                            PasswrdSendToMail(lc, PwdLink);
                            // lblError.ForeColor = System.Drawing.Color.Green;
                            lblMsg.Text = "Please check your email";
                            //btnsendagain.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
    }
}