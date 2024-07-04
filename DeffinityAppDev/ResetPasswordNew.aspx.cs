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

namespace DeffinityAppDev
{
    public partial class ResetPasswordNew : System.Web.UI.Page
    {
        IPortfolioRepository<ProjectPortfolio> pRepository = null;
        private string error = "";
        //"~/WF/Projects/ProjectPipeline.aspx?Status=2"
        //~/WF/DC/FLSJlist.aspx?type=FLS
        //string[] Purl = { "", "~/WF/DC/ResourceSchedular.aspx", "~/WF/DC/FLSJlist.aspx?type=FLS", "~/WF/DC/FLSJlist.aspx?type=FLS", "~/WF/DC/FLSResourceList.aspx?type=FLS",
        //                    "~/WF/Projects/QA/QASummary.aspx", "", "~/WF/Portal/Home.aspx","~/WF/Vendors/RFIVendorSummary.aspx",
        //                    "~/WF/DC/FLSResourceList.aspx?type=FLS","","~/WF/CustomerAdmin/PortfolioContacts.aspx","~/WF/DC/FLSJlist.aspx?type=FLS"};
      
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
                            //if (F.IsActive == true)
                            //{

                            //}
                            //else
                            //{
                            //    // lblMsg.ForeColor = System.Drawing.Color.Red;
                            //    lblError.Text = "Password link has been expired";
                            //    //Panel1.Visible = false;
                            //    //lblMsg.Visible = true;
                            //    //btnredirect.Visible = true;
                            //}
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
                        Response.Redirect("~/Default.aspx");
                    }
                }
                lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();
                Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
                lblyear.InnerText = DateTime.Now.Year.ToString();
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
                    //hpid.Value = pdata.ID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      
        private void SetDefaultCustomer()
        {
            pRepository = new PortfolioRepository<ProjectPortfolio>();
            if (sessionKeys.PortfolioID == null)
            {
                var pdetails = pRepository.GetAll().FirstOrDefault();
                if (pdetails != null)
                {

                    sessionKeys.PortfolioID = pdetails.ID;
                    sessionKeys.PortfolioName = pdetails.PortFolio;
                }
            }
        }

        private void SetPartnerID()
        {
            try
            {
                var pentity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                if (pentity != null)
                {
                    sessionKeys.PartnerID = pentity.PartnerID.HasValue ? pentity.PartnerID.Value : 0;

                    var prentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(pentity.PartnerID.HasValue ? pentity.PartnerID.Value : 0);
                    if (prentity != null)
                    {
                        sessionKeys.PartnerName = prentity.Portalname;
                        sessionKeys.PartnerTheme = prentity.Theme;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnforgot_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = txtusername.Text.Trim();
                string Pwd = Deffinity.Users.Login.GeneratePasswordString(txtpwd.Text.Trim()); // FormsAuthentication.HashPasswordForStoringInConfigFile(txtpwd.Text.Trim(), "SHA1");
                ForgotPwdTblDetailsBAL F_BAL = new ForgotPwdTblDetailsBAL();
                var F = F_BAL.SelectByVid(Request.QueryString["Vid"].ToString());
                using (UserDataContext dc = new UserDataContext())
                {
                    UserMgt.Entity.Contractor c = (from a in dc.Contractors where a.ID == F.Userid select a).FirstOrDefault();
                    c.Password = Pwd;
                    ForgotPasswordInfo Fp = (from a in dc.ForgotPasswordInfos where a.verifyCode == F.verifyCode select a).FirstOrDefault();
                    Fp.IsActive = false;
                    dc.SubmitChanges();
                  //  sessionKeys.Message = "Password updated successfully";

                   

                    UpdateCustomerPasswordRecord(c.ID, txtpwd.Text.Trim());
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Password updated successfully.", "Ok");
                    //Response.Redirect("~/Default.aspx",false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnredirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 0);
        }

        private void Clear_sessions()
        {
            Response.AddHeader("Expires", new DateTime(1940, 1, 1).ToString("R"));
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("R"));
            Response.AddHeader("Cache-Control", "no-cache, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.CacheControl = "no-cache";
            Response.Expires = -1;
        }
    }
}