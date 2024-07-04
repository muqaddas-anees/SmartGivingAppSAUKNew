using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Default_1 : System.Web.UI.Page
    {
        IPortfolioRepository<ProjectPortfolio> pRepository = null;
        private string error = "";
        //"~/WF/Projects/ProjectPipeline.aspx?Status=2"
        //~/WF/DC/FLSJlist.aspx?type=FLS
        //string[] Purl = { "", "~/WF/DC/ResourceSchedular.aspx", "~/WF/DC/FLSJlist.aspx?type=FLS", "~/WF/DC/FLSJlist.aspx?type=FLS", "~/WF/DC/FLSResourceList.aspx?type=FLS",
        //                    "~/WF/Projects/QA/QASummary.aspx", "", "~/WF/Portal/Home.aspx","~/WF/Vendors/RFIVendorSummary.aspx",
        //                    "~/WF/DC/FLSResourceList.aspx?type=FLS","","~/WF/CustomerAdmin/PortfolioContacts.aspx","~/WF/DC/FLSJlist.aspx?type=FLS"};
        string[] Purl = { "", "~/App/Dashboard.aspx", "~/App/Dashboard.aspx", "~/WF/DC/DashboardV2.aspx", "~/App/Donations.aspx",
                        "~/WF/Projects/QA/QASummary.aspx", "", "~/WF/Portal/Home.aspx","~/WF/Vendors/RFIVendorSummary.aspx",
                        "~/WF/DC/FLSResourceList.aspx?type=FLS","","~/WF/CustomerAdmin/PortfolioContacts.aspx","~/WF/DC/DashboardV2.aspx"};
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // sessionKeys.PartnerID = 1;
                // get partner list

                //if (Request.Url.Host.ToLower().Contains("smarttechapp"))
                //{
                //    Page.Title = "Welcome to " + "Smart Tech App";
                //    sessionKeys.PartnerID = 2;
                //}
                //else
                //{
                //    sessionKeys.PartnerID = 1;
                //    Page.Title = "Welcome to " + "123 Smart Pro";
                //}
                lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();


                if (!IsPostBack)
                {
                    sessionKeys.CompanyModules = null;
                    sessionKeys.CompanyAccess = null;
                    SetPartnerDetialsByUrl();
                    //
                    //get first company details
                    //HttpContext.Current.Application["instance"] = "HWA";
                    //clear application data
                    HttpContext.Current.Application["FromEmail"] = null;
                    //clear  the payment session
                    //PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.Clear_IsPaymentActive_Session();
                    //Redirect to page

                    //if (Request.Url.Host.Contains("localhost") && Request.Url.Port != 80)
                    //{
                    //    CheckLogin("indra@emsysindia.com", "steel", "~/WF/DC/DCQuotationItems.aspx?CCID=61&callid=2595&SDID=2595&Option=313&tab=quote");
                    //}
                    //else if (Request.Form["uid"] != null)
                    //{
                    //    CheckLogin(Request.Form["uid"], Request.Form["pwd"]);
                    //}
                    //if (sessionKeys.UID == 0 )
                    // {
                    //     Response.Redirect("~/Default.aspx", false);
                    //}
                    //else
                    //{
                    //    if (ViewState["pwd"] == null)
                    //    Clear_sessions();

                    //}
                    //if (Request.Url.Host.Contains("localhost") && Request.Url.Port != 80)
                    //    CheckLogin("nadeem.mohammed@deffinity.com", "steel");
                    lblyear.InnerText = System.DateTime.Now.Year.ToString();
                    txtName.Focus();
                    lblError.Visible = false;
                }
                txtName.Attributes.Add("placeholder", "Username");
                txtPwd.Attributes.Add("placeholder", "Password");
                //cbox.Attributes.Add("onClick", "javascript:fnShow();");
                //btnCancel.Attributes.Add("onClick", "javascript:fnUncheck();");

                // PageBody.Style.Add("background", "#eeeeee url('../WF/UploadData/Customers/partnerback_org_" + sessionKeys.PartnerID + ".png?d=" + DateTime.Now.Second + "') top center no-repeat;");
                this.Page.DataBind();
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

        private void CheckLogin(string userName, string Password, string setUrl = "")
        {

            lblError.Visible = false;
            bool success = true;
            try
            {

                int retval = Deffinity.Users.Login.LoginUser_withDecript(userName, Password);
                if (retval > 0)
                {

                    //get url from array
                    int Navigate = 0;
                    if (sessionKeys.SID == 99)
                    {
                        Navigate = 0;
                    }
                    else
                    {

                        var portfoliodata = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                        if (portfoliodata != null)
                        {
                            sessionKeys.IsGroup = portfoliodata.IsGroup.HasValue ? portfoliodata.IsGroup.Value : false;
                            sessionKeys.IsService = portfoliodata.IsServiceCompany.HasValue ? portfoliodata.IsServiceCompany.Value : false;
                            sessionKeys.PortfolioName = portfoliodata.PortFolio;

                            if (sessionKeys.IsService)
                            {
                                sessionKeys.IsOrganization = false;
                            }

                            else if (sessionKeys.IsGroup)
                            {
                                sessionKeys.IsOrganization = false;
                            }
                            //if sid 4 
                            else if (sessionKeys.SID == 4)
                                sessionKeys.IsOrganization = false;





                        }
                        //get the customer portfolio
                        if (sessionKeys.SID == 7)
                        {
                            if (sessionKeys.PortfolioID == 0)
                            {
                                lblError.Visible = true;
                                lblError.Text = "You have not been assigned to a portal. Please contact the system administrator who should be able to set you up.";
                                success = false;
                            }
                            else
                            {
                                Navigate = sessionKeys.SID;
                                //TO check cutomer PortalUser
                                sessionKeys.PortalUser = true;
                            }
                        }
                        else
                        {
                            Navigate = sessionKeys.SID;
                            //TO check cutomer PortalUser
                            sessionKeys.PortalUser = false;
                        }
                    }

                    if (success)
                    {


                        //Add the model pop up control
                        //if (!Deffinity.Users.Login.IsFirstLogged(sessionKeys.UID))
                        //{
                        //    showModelPopUp();
                        //    ViewState["pwd"] = Password;
                        //}
                        //else
                        //{
                        //set default customer



                        //SetDefaultCustomer();
                        // SetPartnerID();
                        //set is organization 
                        // sessionKeys.IsOrganization = true;
                        //add trail period
                        //try
                        //{
                        //   // Deffinity.chat

                        //    PortfolioMgt.BAL.ProjectPortfolioBAL.AddUpdateTrailPerioid(sessionKeys.PartnerID, sessionKeys.PortfolioID);
                        //}
                        //catch (Exception ex)
                        //{
                        //    LogExceptions.WriteExceptionLog(ex);
                        //}

                        //StreamChatBAL scb = new StreamChatBAL();
                        //scb.CreateUser(sessionKeys.UID, sessionKeys.PortfolioID);



                        FormsAuthentication.RedirectFromLoginPage(sessionKeys.UName, false);
                        //check dispatch bord login is enabled

                        if (!string.IsNullOrEmpty(setUrl))
                            Response.Redirect(setUrl, false);
                        else
                        {
                            var url = "~/WF/DC/DashboardV2.aspx";
                            var dLogin = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.DispatchBoard);
                            //if dipatch borad dispable 
                            //then it should redirect to Job list
                            if (!dLogin)
                            {
                                //admin
                                if (sessionKeys.SID == 1)
                                {
                                    if (sessionKeys.IsService)
                                    {
                                        Response.Redirect(Deffinity.systemdefaults.GetHomepage("~/WF/DC/DashboardV2.aspx"), false);
                                    }
                                    else
                                        Response.Redirect(Deffinity.systemdefaults.GetHomepage("~/App/Dashboard.aspx"), false);
                                }
                                else
                                {
                                    Response.Redirect(Deffinity.systemdefaults.GetHomepage(Purl[Navigate].ToString()), false);
                                }
                            }
                            else
                            {
                                if (sessionKeys.SID == 1)
                                {
                                    if (sessionKeys.IsService)
                                    {
                                        Response.Redirect(Deffinity.systemdefaults.GetHomepage("~/WF/DC/DashboardV2.aspx"), false);
                                    }
                                    else
                                        Response.Redirect(Deffinity.systemdefaults.GetHomepage("~/App/Dashboard.aspx"), false);
                                }
                                else
                                {
                                    Response.Redirect(Deffinity.systemdefaults.GetHomepage(Purl[Navigate].ToString()), false);
                                }
                            }
                        }

                        // }

                    }
                }
                else
                {
                    txtPwd.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "The username or password is incorrect. Please try again.";
                }

            }
            catch (Exception ex)
            {
                //write into log file
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //private void SetDefaultCustomer()
        //{
        //    pRepository = new PortfolioRepository<ProjectPortfolio>();
        //    if (sessionKeys.PortfolioID == null)
        //    {
        //        var pdetails = pRepository.GetAll().FirstOrDefault();
        //        if (pdetails != null)
        //        {

        //            sessionKeys.PortfolioID = pdetails.ID;
        //            sessionKeys.PortfolioName = pdetails.PortFolio;
        //        }
        //    }
        //}

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
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //set customer default data

            CheckLogin(txtName.Text.Trim(), txtPwd.Text.Trim());
        }
        private void showModelPopUp()
        {
            //ModalPopupExtender1.Show();
        }

        protected void btnCtn_Click(object sender, EventArgs e)
        {
            Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 1);
            if (ViewState["pwd"] != null)
                CheckLogin(txtName.Text.Trim(), ViewState["pwd"].ToString());
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