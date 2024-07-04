using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class TaithingDashboardV2 : System.Web.UI.Page
    {
        //CultureInfo sa_culture =
        //  new CultureInfo("af-ZA");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    if(sessionKeys.ErrorMessage.Length >0)
                    {
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Please enter valid old password.", "Ok");
                        lblError.Text = "Please enter valid old password.";
                       // mdlPopup.Show();
                        sessionKeys.ErrorMessage = "";
                    }
                   
                    var sdate = Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    var edate = Deffinity.Utility.EndDateOfMonth(Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    txtFromDate.Text = sdate.ToShortDateString();
                    txtToDate.Text = edate.ToShortDateString();

                    BindData(sdate, edate);


                    ShowPaymentSettings();
                    showShowChnagePassword();


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        private void showShowChnagePassword()
        {
            try
            {
                var cEntity = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == sessionKeys.UID).FirstOrDefault();
                if (cEntity != null)
                {
                    if ((cEntity.isFirstlogin ?? 0) == 1)
                    {
                        mdlPopup.Show();
                    }
                    else
                        mdlPopup.Hide();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ShowPaymentSettings()
        {
            try
            {

                var pj = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);


                pnlmidcheck.Visible = false;
                var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                if (d != null)
                {
                    if ((d.Vendor ?? "").Length == 0)
                    {
                        pnlmidcheck.Visible = true;
                    }
                }
                else
                {
                    pnlmidcheck.Visible = true;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string getTithing(List<PortfolioMgt.Entity.TithingDefaultDetail> tulist, int id)
        {
            string retval = "";


            if (id == 0)
                retval = "";
            else
            {
                var eDetails = tulist.Where(o => o.ID == id).FirstOrDefault();
                if (eDetails != null)
                {

                    retval = eDetails.Title;

                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }

        //public static String getCurrencySymbol(String currencyCode)
        //{
        //    Currency currency = Currency.getInstance(currencyCode);
        //  //  System.out.println(currencyCode + ":-" + currency.getSymbol(currencyLocaleMap.get(currency)));
        //    return currency.getSymbol(currencyLocaleMap.get(currency));
        //}
        //public static string GetCurrencySymbol()
        //{
        //    CultureInfo sa_culture =
        //      new CultureInfo("af-ZA");

        //    return sa_culture.NumberFormat.CurrencySymbol;
        //}
        public void AddOrUpdateMembers(string email, string firstname, string lastname, string contactno, string address, string town, string state, string zipcode, string eventname, string eventstatus)
        {

            try
            {
                int userid = 0;

                if (email.Length > 0)
                {


                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.SID == UserType.Donor).Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
                    if (cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();

                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = firstname.Trim() + " " + lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
var pw = "SMG@2022";
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber = contactno;

                        cRep.Add(value);


                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();


                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = Convert.ToInt32(Deffinity.systemdefaults.GetCoutryID());
                        cdEntity.PostCode = zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);

                        userid = value.ID;
                    }



                    //update company
                    var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }

                    var tags = "";
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "All,";
                        //if (eventstatus == "Pending")
                        //{
                        //    toadd = eventname + " - Not Attended";
                        //}
                        //else if (eventstatus == "Attended")
                        //{
                        //    toadd = eventname + " - Attended";
                        //}
                        var notes = toadd;// "[{\"value\":\"" + toadd + "\"}]";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if (!exitingNotes.Contains("All"))
                        {
                            string toadd = "All";
                            //if (eventstatus == "Pending")
                            //{
                            //    toadd = eventname + " - Not Attended";
                            //}
                            //else if (eventstatus == "Attended")
                            //{
                            //    toadd = eventname + " - Attended";
                            //}

                            exitingNotes = exitingNotes.Contains("All") == false ? exitingNotes + "All," : exitingNotes;
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindData(DateTime sdate, DateTime edate)
        {
            try
            {
                var lastmonth_sdate = Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(sdate.AddMonths(-1).ToShortDateString()));
                var lastmonth_edate = Deffinity.Utility.EndDateOfMonth(Convert.ToDateTime(edate.AddMonths(-1).ToShortDateString()));
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll()
                    .Where(o => o.PaidDate >= sdate && o.PaidDate <= edate.AddDays(1).AddMinutes(-1)).Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();

                //AddOrUpdateMembers
                var usergetlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.DonerEmail != null).ToList();

                try
                {
                    foreach (var u in usergetlist)
                    {
                        if (u.DonerEmail != null)
                            AddOrUpdateMembers(u.DonerEmail, u.DonerName, "", u.DonerContact, "", "", "", "", "", "");
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

                var Lastmonth_tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll()
                    .Where(o => o.PaidDate >= lastmonth_sdate && o.PaidDate <= lastmonth_edate).Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
               // var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();

                Random generator = new Random();
                var rlist = (from t in tList
                             //join c in ulist on t.LoggedByID equals c.ID
                             //join tc in tclist on t.TithingID equals tc.ID
                             select new
                             {
                                 ID = t.ID,
                                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),
                                 PaidBy = "",
                                 Amount = t.PaidAmount,
                                 PaidDate = t.PaidDate,
                                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                 t.RecurringType,

                             }).ToList();
             
                //var culture = CultureInfo.GetCultureInfo("af-ZA");

                lbltotal_transaction.InnerText = string.Format( "{1}{0:N2}", tList.Sum(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00),Deffinity.Utility.GetCurrencySymbol());
                lblAverage_transaction.InnerText = string.Format("{1}{0:N2}", tList.Average(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00), Deffinity.Utility.GetCurrencySymbol());
                lblLarget_transaction.InnerText = string.Format("{1}{0:N2}", tList.Max(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00), Deffinity.Utility.GetCurrencySymbol());

                //Console.WriteLine(value.ToString("C3",
                //  CultureInfo.CreateSpecificCulture("da-DK")));
                lblDonors.InnerText = tList.GroupBy(p => new { p.DonerName, p.DonerEmail }).Select(group => group.First()).Count().ToString();
                lblNewDonors.InnerText = tList.GroupBy(p => new { p.DonerName, p.DonerEmail }).Select(group => group.First()).Count().ToString();
                lblTransactions.InnerText = tList.Count().ToString();

                var thismonth_transaction = rlist.Sum(o => o.Amount);
                var thismonth_lblAverage_transaction = rlist.Average(o => o.Amount);
                var thismonth_lblLarget_transaction = rlist.Max(o => o.Amount);
                var thismonth_lblDonors = tList.Select(o => o.LoggedByID).Distinct().Count();
                var thismonth_lblNewDonors = tList.Select(o => o.LoggedByID).Distinct().Count();
                var thismonth_lblTransactions = tList.Select(o => o.ID).Count();

                var lastmonth_transaction = Lastmonth_tList.Sum(o => o.PaidAmount);
                var lastmonth_lblAverage_transaction = Lastmonth_tList.Average(o => o.PaidAmount);
                var lastmonth_lblLarget_transaction = Lastmonth_tList.Max(o => o.PaidAmount);
                var lastmonth_lblDonors = Lastmonth_tList.Select(o => o.LoggedByID).Distinct().Count();
                var lastmonth_lblNewDonors = Lastmonth_tList.Select(o => o.LoggedByID).Distinct().Count();
                var lastmonth_lblTransactions = Lastmonth_tList.Select(o => o.ID).Count();

                var rtotal = 0.00;
                if (thismonth_transaction > 0)
                    rtotal = (((thismonth_transaction - lastmonth_transaction) / thismonth_transaction) * 100).Value;
                icon_totalicon.Text = rtotal > 0 ? GetGreenHtml(string.Format("{0:F2}", rtotal) + "%") : GetRedHtml(string.Format("{0:F2}", rtotal) + "%");

                var rlarget = 0.00;
                if (thismonth_lblAverage_transaction > 0)
                    rlarget = ((((thismonth_lblLarget_transaction.HasValue ? thismonth_lblLarget_transaction.Value : 0.00) - (lastmonth_lblLarget_transaction.HasValue ? lastmonth_lblLarget_transaction.Value : 0.00)) / (thismonth_lblLarget_transaction.HasValue ? thismonth_lblLarget_transaction.Value : 0) * 100));
                icon_larget_transaction.Text = rlarget > 0 ? GetGreenHtml(string.Format("{0:F2}", rlarget) + "%") : GetRedHtml(string.Format("{0:F2}", rlarget) + "%");

                var raverage = 0.00;
                if (thismonth_lblAverage_transaction > 0)
                    // raverage = (((thismonth_lblAverage_transaction - lastmonth_lblAverage_transaction) / thismonth_lblAverage_transaction) * 100).Value;
                    raverage = ((((thismonth_lblAverage_transaction.HasValue ? thismonth_lblAverage_transaction.Value : 0.00) - (lastmonth_lblAverage_transaction.HasValue ? lastmonth_lblAverage_transaction.Value : 0.00)) / (thismonth_lblAverage_transaction.HasValue ? thismonth_lblAverage_transaction.Value : 0) * 100));
                icon_average_transaction_icon.Text = rlarget > 0 ? GetGreenHtml(string.Format("{0:F2}", raverage) + "%") : GetRedHtml(string.Format("{0:F2}", raverage) + "%");


                var rTransaction = thismonth_lblTransactions - lastmonth_lblTransactions;
                icon_transactions.Text = rTransaction > 0 ? GetGreenHtml(rTransaction.ToString()) : GetRedHtml(rTransaction.ToString());
                var rDonors = thismonth_lblDonors - lastmonth_lblDonors;
                icon_donors.Text = rDonors > 0 ? GetGreenHtml(rDonors.ToString()) : GetRedHtml(rDonors.ToString());
                var rNewDonors = thismonth_lblDonors - lastmonth_lblDonors;
                icon_newdonors.Text = rNewDonors > 0 ? GetGreenHtml(rNewDonors.ToString()) : GetRedHtml(rNewDonors.ToString());
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnTithing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/Donations.aspx", false);
        }

        public string GetGreenHtml(string value)
        {
            string retval = @"<span class='badge badge-success fs-6 lh-1 py-1 px-2 d-flex flex-center'>
	<span class='svg-icon svg-icon-7 svg-icon-white'>
															<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'>
																<path opacity='0.5' d='M13 9.59998V21C13 21.6 12.6 22 12 22C11.4 22 11 21.6 11 21V9.59998H13Z' fill='black'></path>
																<path d='M5.7071 7.89291C5.07714 8.52288 5.52331 9.60002 6.41421 9.60002H17.5858C18.4767 9.60002 18.9229 8.52288 18.2929 7.89291L12.7 2.3C12.3 1.9 11.7 1.9 11.3 2.3L5.7071 7.89291Z' fill='black'></path>
															</svg>
														</span>
														"+ value + "</span>";
            return retval;
        }

        public string GetRedHtml(string value)
        {
            string retval = @"<span class='badge badge-danger fs-6 lh-1 py-1 px-2 d-flex flex-center' style='height: 22px'>
														
														<span class='svg-icon svg-icon-7 svg-icon-white ms-n1'>
															<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'>
																<path opacity='0.5' d='M13 14.4V3.00003C13 2.40003 12.6 2.00003 12 2.00003C11.4 2.00003 11 2.40003 11 3.00003V14.4H13Z' fill='black'></path>
																<path d='M5.7071 16.1071C5.07714 15.4771 5.52331 14.4 6.41421 14.4H17.5858C18.4767 14.4 18.9229 15.4771 18.2929 16.1071L12.7 21.7C12.3 22.1 11.7 22.1 11.3 21.7L5.7071 16.1071Z' fill='black'></path>
															</svg>
														</span>
														" + value + "</span>";
            return retval;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/DashboardReport.aspx", false);
        }

        protected void btnCategory_Click(object sender, EventArgs e)
        {
            //App/TithingCategorySettings.aspx?type=active
            Response.Redirect("~/App/TithingCategorySettings.aspx?type=active", false);
        }

        protected void btnDonation_kind_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/OtherDonationList.aspx?type=inkind", false);
        }

        protected void btnDonation_Cash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/OtherDonationList.aspx?type=cash", false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var sdate = Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(txtFromDate.Text.Trim()));
                var edate = Deffinity.Utility.EndDateOfMonth(Convert.ToDateTime(txtToDate.Text.Trim()));

                //txtFromDate.Text = sdate.ToShortDateString();
                //txtToDate.Text = edate.ToShortDateString();

                BindData(sdate, edate);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {

               // var p = PortfolioRepository<PortfolioMgt.Entity.pr>




                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=paymentsettings", false);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Deffinity.Users.Login objLogin = new Deffinity.Users.Login();
            int uid = sessionKeys.UID;
            string oldpwd = txtOldPwd.Text.Trim();
            string newpwd = txtNewPwd.Text.Trim();
            object objOldPassword = objLogin.Old_Password(uid, oldpwd);

            if (objOldPassword != null)
            {

                object objRetVal = objLogin.LoginUser_ChangePassword(newpwd, uid);

               
                UpdateCustomerPasswordRecord(uid, newpwd);
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                //lblMsg.Text = "Password has been changed.";

                IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
                var u = uRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                if(u != null)
                {
                    u.isFirstlogin = 0;
                    uRep.Edit(u);
                }
                mdlPopup.Hide();
                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Password has been changed.", "Ok");
                
            }
            else
            {
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                // lblError.Text = "Please enter valid old password";
                sessionKeys.ErrorMessage = "Please enter valid old password.";
                Response.Redirect(Request.RawUrl, false);
            }
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