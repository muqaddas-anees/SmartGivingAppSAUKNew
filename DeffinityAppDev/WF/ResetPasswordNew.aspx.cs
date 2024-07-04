using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;

namespace DeffinityAppDev.WF
{
    public partial class ResetPasswordNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetPartnerDetialsByUrl();
                    if (Request.QueryString["Vid"] != null)
                    {
                        string Vid = Request.QueryString["Vid"].ToString();
                        ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                        ForgotPasswordInfo F = F_BAL.SelectByVid(Vid);
                        if (F != null)
                        {
                            if (F.IsActive == true)
                            {
                                using (UserDataContext dc = new UserDataContext())
                                {
                                    UserMgt.Entity.Contractor c = (from a in dc.Contractors where a.ID == F.Userid select a).FirstOrDefault();
                                    if (c != null)
                                    {
                                        txtusername.Text = c.LoginName;
                                    }
                                    else
                                    {
                                        // lblMsg.ForeColor = System.Drawing.Color.Red;
                                        lblError.Text = "Input is wrong.";
                                        //Panel1.Visible = false;
                                        lblMsg.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                // lblMsg.ForeColor = System.Drawing.Color.Red;
                                lblError.Text = "Password link has been expired";
                                //Panel1.Visible = false;
                                //lblMsg.Visible = true;
                                //btnredirect.Visible = true;
                            }
                        }
                        else
                        {
                            //lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblError.Text = "Invalid link.";
                            // Panel1.Visible = false;
                            //btnredirect.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/WF/Default.aspx");
                    }
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
        private void SetPartnerDetialsByUrl()
        {
            try
            {

                var pdata = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.ParnerPortal != null).Where(o => o.ParnerPortal.ToLower().Contains(Request.Url.Host.ToLower())).FirstOrDefault();
                if (pdata != null)
                {
                    Page.Title = "Welcome to " + pdata.Portalname;
                    sessionKeys.PartnerID = pdata.ID;
                    sessionKeys.PartnerName = pdata.Portalname;
                   // hpid.Value = pdata.ID.ToString();
                }
            }
            catch (Exception ex)
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
        protected void btnforgot_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = txtusername.Text.Trim();
                string Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtpwd.Text.Trim(), "SHA1");
                ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                var F = F_BAL.SelectByVid(Request.QueryString["Vid"].ToString());
                using (UserDataContext dc = new UserDataContext())
                {
                    UserMgt.Entity.Contractor c = (from a in dc.Contractors where a.ID == F.Userid select a).FirstOrDefault();
                    c.Password = Pwd;
                    ForgotPasswordInfo Fp = (from a in dc.ForgotPasswordInfos where a.verifyCode == F.verifyCode select a).FirstOrDefault();
                    Fp.IsActive = false;
                    dc.SubmitChanges();

                    UpdateCustomerPasswordRecord(c.ID, txtpwd.Text.Trim());
                    Response.Redirect("~/WF/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnredirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/Default.aspx");
        }

        private static void UpdateCustomerPasswordRecord(int uid, string newpwd)
        {
            UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
            var cEntity = cb.Contractor_Select_ByID(uid);
            if (cEntity != null)
            {
                //if user id customer then only update password
                if (cEntity.SID == 7)
                    PortfolioMgt.BAL.PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(0, cEntity.ID, cEntity.LoginName, newpwd);
            }
        }
    }
}