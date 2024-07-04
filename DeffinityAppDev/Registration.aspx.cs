using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DC.BAL;
using DC.BLL;
using DC.Entity;
using DeffinityManager.DC.BLL;
using DeffinityManager.PortfolioMgt.BAL;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using UserMgt.BAL;
using UserMgt.Entity;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DeffinityAppDev
{
    public class signupdetails
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Industry { get; set; }
        public string url { get; set; }
        public string partner { get; set; }
        public string EcoSystem { get; set; }
        public string organization { get; set; }
        public string faith { get; set; }

        public string group { get; set; }

        public string denomination { get; set; }
        public int sid { get; set; }
    }
    public class plist
    {
        public string Email { set; get; }
        public string Password { set; get; }
    }
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    if(sessionKeys.Message.Length >0)
                    {
                        // DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Thank you for registering with Plegit. We will review your application and keep you posted once your application has been approved.", "Ok");
                        //We appreciate your registration with Plegit. After reviewing the information you've provided, we will proceed to activate your account and forward you the login credentials. Please keep in mind that submitting proof of your organization's registration is a necessary step to open an account.
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Great thanks for registering with Plegit. Please check your email for a confirmation email. This will contain a link that you need to select to activate your Plegit account.", "Go Back To Plegit");
                        //Did not receive the email ? <a href='#' onclick='resend();'> click here </a> to resend
                        sessionKeys.Message = "";
                    }

                   
                    if (Request.QueryString["r"] != null)
                    {
                        txtRefCode.Text = Request.QueryString["r"].ToString();
                    }
                    BindCountry();
                    lblInstance.Text = Deffinity.systemdefaults.GetInstanceTitle();
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));

            ddlCountry.SelectedValue = "1";
        }
        private void BindPrice()
        {
            var u = sessionKeys.Currency;
            ListItemCollection cl = new ListItemCollection();
            cl.Add(new ListItem(u+"", u+""));
        }
        private void AddDefaultData(int portfolioID)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == portfolioID).ToList();
                var tpLfist = tList.ToList();
                if (tpLfist.Count() == 0)
                {
                    tList = tk.GetAll().ToList();
                    var pE = tList.Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                    if (pE != null)
                    {
                        var dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;
                            

                            tk.Add(dEntity);

                        }

                        pE = tList.Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;

                            tk.Add(dEntity);

                        }
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
            try
            {
                signupdetails cs = new signupdetails();
                cs.CompanyName = txtOrganisationname.Text.Trim();// company;
                cs.EmailAddress = txtEmail.Text.Trim();
                cs.Firstname = txtFirstName.Text.Trim();
                cs.Lastname = txtLastName.Text.Trim();
                cs.Industry = "";
                cs.MobileNumber = "";// txtPhoneNumber.Text.Trim();
                cs.url = Deffinity.systemdefaults.GetWebUrl();
                cs.partner = "partner1";
                cs.EcoSystem = "Organization";
                cs.organization = txtOrganisationname.Text.Trim();
                cs.faith = "";
                cs.group = "";
                cs.denomination = "";
                cs.sid = UserType.SmartPros;
                InsertContractor1(cs, true, false, false, true);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        List<plist> pwd = new List<plist>();
        public string InsertContractor1(signupdetails data, bool IsAorg = false, bool IsGroup = false, bool IsMember = false, bool IsMemberWithService = false)
        {
            string retval = "fail";
            //if (HttpContext.Current.Session["InstId"] != null)
            //{

            try
            {
               var  cRep = new UserRepository<Contractor>();
               var cvRep = new UserRepository<v_contractor>();
               var cmRep = new UserRepository<UserMgt.Entity.Company>();
               var  cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
               var poRep = new PortfolioRepository<ProjectPortfolio>();
               var pcRep = new PortfolioRepository<PortfolioContact>();
              var  pcaRep = new PortfolioRepository<PortfolioContactAddress>();
               var usRep = new UserRepository<UserMgt.Entity.UserDetail>();

                int[] adminaids = { 1, 2 };
                var partnerentity = new PartnerDetail();
                var portfolio = new PortfolioMgt.Entity.ProjectPortfolio();
                var value = new Contractor();
                var udEntity = new UserDetail();
                value.ContractorName = data.Firstname;
                value.EmailAddress = data.EmailAddress;
                value.LoginName = data.EmailAddress; //value.ContractorName.Replace(" ", "");
                value.Password = Deffinity.Users.Login.GeneratePasswordString( txtNewPwd.Text.Trim());
                ////if (HttpContext.Current.Session["password"] == null)
                ////{
                ////    var pw = DeffinityManager.RandomPassword.GetPassword();
                ////    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw });
                ////    value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                ////}
                ////else
                ////{
                ////    var pw1 = HttpContext.Current.Session["password"].ToString();
                ////    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw1 });
                ////    value.Password= Deffinity.Users.Login.GeneratePasswordString(pw1);//  FormsAuthentication.HashPasswordForStoringInConfigFile(pw1, "SHA1");
                ////}
                //1 - Admin


                value.SID = data.sid;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.Status = UserStatus.Active;
                value.isFirstlogin = 1;
                value.ResetPassword = false;
                value.Company = data.CompanyName;
                value.ContactNumber = data.MobileNumber;
                value.Details = data.organization;
                value.LastName = data.Lastname;
                value.TypeofLogin = txtNewPwd.Text.Trim();

                if (cvRep.GetAll().Where(o => o.LoginName.ToLower().Trim() == value.LoginName.ToLower().Trim() && o.SID != UserType.Donor).Count() == 0)
                {
                    cRep.Add(value);

                    Session["u_name"] = value.ContractorName + " " + value.LastName;
                    Session["u_password"] = txtNewPwd.Text.Trim();
                    Session["u_email"] = value.EmailAddress;
                    //Session["u_email"] = "";

                }
                else
                {
                    DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Sorry but this email address already exists in the system. Please use an alternative email address.", "Ok");
                }

                if (value.ID > 0)
                {


                    if (IsMember)
                    {
                        portfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.PortFolio.ToLower() == "Faith church".ToLower()).FirstOrDefault();
                    }
                    else if (IsMemberWithService)
                    {

                        var cmpname = string.Empty;
                        cmpname = data.organization;
                        //if (data.sid == 3)

                        //else
                        //    cmpname = data.CompanyName.ToLower().Trim().Length == 0 ? data.organization.Trim() : data.CompanyName.Trim();


                        portfolio = poRep.GetAll().Where(o => o.PortFolio.ToLower().Trim() == cmpname.ToLower().Trim()).FirstOrDefault();

                        if (portfolio == null)
                        {
                            portfolio = new ProjectPortfolio();
                            var cDate = DateTime.Now;
                            if (IsGroup)
                                portfolio.IsGroup = IsGroup;
                            //set as service company
                            if (data.sid == 3)
                                portfolio.IsServiceCompany = true;

                            //add to refral data
                            try
                            {
                                IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();

                                var r = pRep.GetAll().Where(o => o.RefCode == txtRefCode.Text.Trim()).FirstOrDefault();
                                if (r != null)
                                {
                                    portfolio.RefCode = txtRefCode.Text;
                                    portfolio.commission = r.commission;
                                }
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                            portfolio.BenefitstoOrganisation = data.EcoSystem;
                            portfolio.OrgarnizationGUID = Guid.NewGuid().ToString();
                            portfolio.PortFolio = cmpname;
                            portfolio.EmailAddress = data.EmailAddress;
                            portfolio.Visible = true;
                            portfolio.TelephoneNumber = data.MobileNumber;
                            portfolio.Description = data.Industry;
                            portfolio.StartDate = DateTime.Now;
                            portfolio.EndDate = DateTime.Now;
                            portfolio.Owner = value.ID;
                            portfolio.AdminID = value.ID;
                            portfolio.ContactName = value.ContractorName;
                            portfolio.BankName = value.ContractorName;
                            
                            portfolio.OrgarnizationStatus = "";
                            //amount
                            portfolio.CostCentre = "";// ddlAmount.SelectedValue; 
                            portfolio.SalesNotes = "";
                            portfolio.RisksandIssues = data.denomination;
                            portfolio.EnableThankyouMail = true;
                            //registration number
                            portfolio.RegistrationNumber = "";// txtOrgRegistrationNumber.Text.Trim();
                            portfolio.Chat_ChannelName = txtLastName.Text.Trim();
                            portfolio.PortfolioTypeID = 0;
                            portfolio.CountryID = Convert.ToInt32( ddlCountry.SelectedValue);
                            if (pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault() != null)
                                portfolio.KeyContactName = pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault().Password;

                           
                            //if user is Organization admin or Group admin
                            poRep.Add(portfolio);

                            try
                            {
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                            try
                            {
                                AddDefaultData(portfolio.ID);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                            try
                            {
                                //update plat form fee
                                var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolio.ID);
                                if (payDetails == null)
                                {
                                    payDetails = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                                    payDetails.PortfolioID = portfolio.ID;
                                    payDetails.PayType = "cardconnect";


                                }
                                payDetails.TransactionFee = 5;
                                payDetails.CardFee = 3;
                                payDetails.IsActive = true;
                                PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(payDetails);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }
                        //if (adminaids.Contains(value.SID.Value))
                        //{


                        //}
                        //else
                        //{
                        //    //if user is a member or member 

                        //}
                    }

                    else

                    {
                        var cDate = DateTime.Now;
                        if (IsGroup)
                            portfolio.IsGroup = IsGroup;

                        if (data.sid == 3)
                            portfolio.IsServiceCompany = true;


                        //add to refral data
                        try
                        {
                            IPortfolioRepository<PortfolioMgt.Entity.tblReferral> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblReferral>();

                            var r = pRep.GetAll().Where(o => o.RefCode == txtRefCode.Text.Trim()).FirstOrDefault();
                            if (r != null)
                            {
                                portfolio.RefCode = txtRefCode.Text;
                                portfolio.commission = r.commission;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        portfolio.BenefitstoOrganisation = data.EcoSystem;
                        portfolio.OrgarnizationGUID = Guid.NewGuid().ToString();
                        portfolio.PortFolio = data.CompanyName;
                        portfolio.EmailAddress = data.EmailAddress;
                        portfolio.Visible = true;
                        portfolio.TelephoneNumber = data.MobileNumber;
                        portfolio.Description = data.Industry;
                        portfolio.StartDate = DateTime.Now;
                        portfolio.EndDate = DateTime.Now;
                        portfolio.Owner = value.ID;
                        portfolio.AdminID = value.ID;
                        portfolio.ContactName = value.ContractorName;
                        portfolio.BankName = value.ContractorName;
                        portfolio.OrgarnizationStatus = "";
                        portfolio.CostCentre = data.faith;
                        portfolio.SalesNotes = data.group;
                        portfolio.RisksandIssues = data.denomination;
                        portfolio.PortfolioTypeID = 0;
                        portfolio.Chat_ChannelName = txtLastName.Text.Trim();
                        portfolio.RegistrationNumber = "";// txtOrgRegistrationNumber.Text.Trim();
                      
                        if (pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault() != null)
                            portfolio.KeyContactName = pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault().Password;

                      


                        if (adminaids.Contains(value.SID.Value))
                        {
                            poRep.Add(portfolio);
                            try
                            {
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                            try
                            {
                                //update plat form fee
                                var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolio.ID);
                                if (payDetails == null)
                                {
                                    payDetails = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                                    payDetails.PortfolioID = portfolio.ID;
                                    payDetails.PayType = "cardconnect";


                                }
                                payDetails.TransactionFee = 5;
                                payDetails.CardFee = 3;
                                payDetails.IsActive = true;
                                PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(payDetails);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }
                        else
                        {
                            //if user is a member or member 

                        }
                    }


                    //if user a memeber


                    if (portfolio.ID > 0)
                    {
                        try
                        {
                            
                            //sene
                            //update trail period
                            PortfolioMgt.BAL.ProjectPortfolioBAL.AddUpdateTrailPerioid(portfolio.PartnerID.HasValue ? portfolio.PartnerID.Value : 0, portfolio.ID);
                            //insert default data
                            FLSFieldsConfigBAL.InsertConfigData(portfolio.ID, 1);
                            DefaultConfigurationToAllCustomer D_Configuration = new DefaultConfigurationToAllCustomer();
                            D_Configuration.DataBindToTables(portfolio.ID.ToString());
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
}
                        try
                        {
                            var usertoCompany = new UserMgt.Entity.UserToCompany();
                            usertoCompany.CompanyID = portfolio.ID;
                            usertoCompany.UserID = value.ID;
                            cuRep.Add(usertoCompany);
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                       // uploadImage(portfolio.ID);
                      //  sendmail(data, value, partnerentity, portfolio, IsAorg);

                        SendActivationmail(value, portfolio);
                        //sendmailtoDistributionlist(data, value);
                        sessionKeys.Message = "success";
                        Response.Redirect(Request.RawUrl,false);
                        retval = "success";
                    }


                }
            }
            catch (Exception ex)
            { return "fail"; LogExceptions.WriteExceptionLog(ex); }
            return retval;
            //}
            //else { return "Fail"; }
        }

        private void uploadImage(int portfolioid)
        {
            try

            {

                //using (Stream fs = imgFile.PostedFile.InputStream)
                //{
                //    using (BinaryReader br = new BinaryReader(fs))
                //    {
                //        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //        ImageManager.FileDBSave(bytes, null, portfolioid.ToString(), ImageManager.file_section_portfolio_doc, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);

                //    }
                //}
                //    if (imgFile.PostedFile.FileName.Length > 0)
                //{
                //    Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgFile.PostedFile.InputStream);
                //    ImageManager.FileDBSave(imgFile.FileBytes, null,portfolioid.ToString(),ImageManager.file_section_portfolio_doc, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);
                //    // DisplayLogo();

                //  //  Response.Redirect(Request.RawUrl + "&v=" + DateTime.Now.Ticks.ToString(), false);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string sendmailtoDistributionlist(signupdetails data, Contractor c)
        {
            try
            {
                LogExceptions.LogException("send distribution list mail method");

                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = "Plegit Lead";
                string fromemail = Deffinity.systemdefaults.GetFromEmail();

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = Deffinity.systemdefaults.GetInstanceTitle();
                string siteurl = data.url;
                //siteurl = "http://www.wisal.cloud";
                //displayName = "Wisal";
                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/Content/emailtemplate/NewInstanceDistributionMail.html");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }
                //mail to owner
                if (c != null)
                {
                    string s = string.Empty;
                    if (c.EmailAddress != null)
                    {
                        var tempContent = contents;
                        tempContent = tempContent.Replace("[name]", c.ContractorName);
                        tempContent = tempContent.Replace("[company]", data.CompanyName);
                        tempContent = tempContent.Replace("[email]", c.EmailAddress);
                        tempContent = tempContent.Replace("[mobile]", data.MobileNumber);

                        var t = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select().Where(o => o.ID == Convert.ToInt32(!string.IsNullOrEmpty(data.Industry) ? data.Industry : "0")).FirstOrDefault();
                        if (t != null)
                            tempContent = tempContent.Replace("[industry]", t.Portfoliotype1);
                        else
                            tempContent = tempContent.Replace("[industry]", string.Empty);


                        var newBody = tempContent;

                        newBody = newBody.Replace("[username]", "Support Team");
                       

                        if (!c.EmailAddress.ToLower().Contains("indra"))
                        {
                            //mpho.morutwe@falcorp.co.za
                            //em.SendingMail("mpho.morutwe@falcorp.co.za", subject, newBody);
                            //em.SendingMail("support@plegit.africa", subject, newBody);
                            em.SendingMail("nadeem.mohammed@deffinity.com", subject, newBody);
                            em.SendingMail("indra@deffinity.com", subject, newBody);
                        }
                        else
                        {
                            em.SendingMail("indra@deffinity.com", subject, newBody);
                        }
                        //support@plegit.africa
                        //var dlist = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.PartnerID == 1).ToList(); //DeffinityManager.PortfolioMgt.BAL.PortfolioDistributionListBAL.PortfolioDistributionListBAL_SelectAll();
                        //foreach (var p in dlist)
                        //{


                        //    if (!c.EmailAddress.ToLower().Contains("indra"))
                        //    {
                        //        em.SendingMail("nadeem.mohammed@123servicepro.com", subject, newBody);
                        //        if (!c.EmailAddress.ToLower().Contains("porch1@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch2@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch3@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch4@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch5@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch6@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch7@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch8@deffinity.com"))
                        //        {
                        //            em.SendingMail(p.Username, subject, newBody);
                        //        }

                        //    }



                        //    em.SendingMail("indra@deffinity.com", subject, newBody);

                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return "";

        }
        public string sendmail(signupdetails data, Contractor c, PartnerDetail partnerentity, PortfolioMgt.Entity.ProjectPortfolio portfolio, bool IsAorg)
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
                string siteurl = data.url;
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



                        if (partnerentity != null)
                        {
                            tempContent = tempContent.Replace("[logo]", string.Format("{0}/assets/media/logos/logo-1.png?d=57", Deffinity.systemdefaults.GetWebUrl()));
                            tempContent = tempContent.Replace("[portalname]", Deffinity.systemdefaults.GetInstanceTitle());
                            tempContent = tempContent.Replace("[supportmail]", Deffinity.systemdefaults.GetFromEmail());
//fromemail = partnerentity.FromEmail;
                           var webName = Deffinity.systemdefaults.GetInstanceTitle();
                            fromemail = Deffinity.systemdefaults.GetFromEmail();// partnerentity.FromEmail;
                            subject = string.Format("GREAT NEWS! Your {0} Portal Is Ready.", webName);

                            // else
                            //    tempContent = tempContent.Replace("[url]", partnerentity.ParnerPortal.Contains("https") ? partnerentity.ParnerPortal : "https://" + partnerentity.ParnerPortal);
                        }

                        tempContent = tempContent.Replace("[loginurl]", data.url);

                      
                        tempContent = tempContent.Replace("[url]", data.url);

                        tempContent = tempContent.Replace("[user]", c.ContractorName);
                        tempContent = tempContent.Replace("[site]", data.url);
                        tempContent = tempContent.Replace("[urlroot]", data.url);
                        tempContent = tempContent.Replace("[displayname]", data.CompanyName);

                        //[name]
                        //foreach (var c in udetails)
                        //{
                        tempContent = tempContent.Replace("[name]", c.ContractorName);
                        s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", c.ContractorName, c.LoginName, txtNewPwd.Text.Trim());
                        //}
                        tempContent = tempContent.Replace("[datarow]", s);
                        //if mail gives exception
                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, data.EmailAddress, "");
                            LogExceptions.LogException("send mail method - to :", data.EmailAddress + "-from email: " + fromemail);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(data.EmailAddress + " failed to send mail " + ex);
                        }

                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, "indra@deffinity.com", "");
                            if (!data.EmailAddress.ToLower().Contains("indra"))
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

        public string sendmailAfterActivation( Contractor c, PortfolioMgt.Entity.ProjectPortfolio portfolio)
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
                        tempContent = tempContent.Replace("[name]", c.ContractorName);
                        s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", c.ContractorName, c.LoginName, c.TypeofLogin);
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

        public string SendActivationmail( Contractor c, PortfolioMgt.Entity.ProjectPortfolio p)
        {
            try
            {
                //data.partner
                LogExceptions.LogException("send mail method");

                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = "Verify Your Plegit Email Address";
                string fromemail = Deffinity.systemdefaults.GetFromEmail();

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = Deffinity.systemdefaults.GetInstanceTitle();
                string siteurl = Deffinity.systemdefaults.GetWebUrl();
                //siteurl = "http://www.wisal.cloud";
                //displayName = "Wisal";
                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/Content/emailtemplate/EmailActive.html");

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
                        //if (partnerentity != null)
                        //{
                            //tempContent = tempContent.Replace("[logo]", string.Format("{0}/assets/media/logos/logo-1.png?d=57", Deffinity.systemdefaults.GetWebUrl()));
                            tempContent = tempContent.Replace("{{firstname}}", c.ContractorName + " " + c.LastName);
                            tempContent = tempContent.Replace("[supportmail]", Deffinity.systemdefaults.GetFromEmail());
                            //fromemail = partnerentity.FromEmail;
                            var webName = Deffinity.systemdefaults.GetInstanceTitle();
                            fromemail = Deffinity.systemdefaults.GetFromEmail();// partnerentity.FromEmail;
                           

                            // else
                            //    tempContent = tempContent.Replace("[url]", partnerentity.ParnerPortal.Contains("https") ? partnerentity.ParnerPortal : "https://" + partnerentity.ParnerPortal);
                       //}

                        tempContent = tempContent.Replace("{{unid}}", p.ID.ToString());


                        //tempContent = tempContent.Replace("[url]", data.url);

                        //tempContent = tempContent.Replace("[user]", c.ContractorName);
                        //tempContent = tempContent.Replace("[site]", data.url);
                        //tempContent = tempContent.Replace("[urlroot]", data.url);
                        //tempContent = tempContent.Replace("[displayname]", data.CompanyName);

                        //[name]
                        //foreach (var c in udetails)
                        //{
                        //tempContent = tempContent.Replace("[name]", c.ContractorName);
                        //s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", c.ContractorName, c.LoginName, txtNewPwd.Text.Trim());
                        //}
                        tempContent = tempContent.Replace("[datarow]", s);
                        //if mail gives exception
                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, c.EmailAddress, "");
                           // LogExceptions.LogException("send mail method - to :", data.EmailAddress + "-from email: " + fromemail);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(c.EmailAddress + " failed to send mail " + ex);
                        }

                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, "indra@deffinity.com", "");
                            //if (!c.EmailAddress.ToLower().Contains("indra"))
                            //{
                            //    // em.SendingMail(fromemail, subject, tempContent, "Instance@123smartpro.com","");
                            //    em.SendingMail(fromemail, subject, tempContent, "nadeem.mohammed@deffinity.com", "");
                            //    // em.SendingMail(fromemail, subject, tempContent,"123SignupEmailDistribution@deffinity.com","");
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


    }
}