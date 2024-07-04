using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using System.Data.SqlClient;
using Deffinity.IncidentService_Price_Manager;
using System.Net;
using PortfolioMgt.Entity;
using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;
using DC.BLL;
using Microsoft.Owin;


namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class UpgradePayment : System.Web.UI.Page
    {
        public const string paymentSucessStatus = "Approved";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pamentFieldsDefaults();

                if (Session["amount"] != null && Session["term"] != null)
                {
                    txtAmount.Text = string.Format("{0:F2}", Session["amount"]);
                    txtAmount.ReadOnly = true;
                    hterm.Value = Session["term"].ToString();
                    Session["amount"] = null;
                    Session["term"] = null;
                }
                else
                {
                    Response.Redirect("~/WF/CustomerAdmin/UpgradeModules.aspx");
                }

            }


            //Response.Cookies.Add("Key", "Value", new CookieOptions()
            //{
            //    SameSite = SameSiteMode.None,
            //    //Secure = true,
            //});
            HttpContext.Current.Response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=Secure");
            //HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin","*");
           // HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");

        }

        private void pamentFieldsDefaults()
        {
            try
            {
                //populate month
                string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                ddlMonth.DataSource = Month;
                ddlMonth.DataBind();
                //pre-select one for testing
                ddlMonth.SelectedIndex = 4;

                //populate year
                ddlYear.Items.Add("");
                int Year = DateTime.Now.Year;
                for (int i = 0; i < 10; i++)
                {
                    ddlYear.Items.Add((Year + i).ToString());
                }
                //pre-select one for testing
                ddlYear.SelectedIndex = 3;

                // set the url for iframe for card connect payment
                var payDetials = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                if (payDetials != null)
                {
                    tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Payment_host);
                    pnlCardConnect.Visible = true;
                    pnlCreditCard.Visible = false;
                    rfCardnumber.Visible = true;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") > 0)
            {

                try
                {

                    //payment 
                    var userRep = new UserRepository<UserMgt.Entity.v_contractor>();
                    var userdetailsRep = new UserRepository<UserMgt.Entity.UserDetail>();

                    var user = userRep.GetAll().Where(o => o.ID == sessionKeys.UID).FirstOrDefault();
                    var userdetails = userdetailsRep.GetAll().Where(o => o.UserId == sessionKeys.UID).FirstOrDefault();
                    //var pcontact = pcRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();
                    
                    var payDetials = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                    //Payment details
                    var paymentResult = string.Empty;
                    if (payDetials != null)
                    {
                        paymentResult = CardConnectPay_ByAddress(payDetials,userdetails,user);
                    }

                    if (paymentResult == paymentSucessStatus)
                    {
                        //update the call status

                        // btnBack.Text = "Take another payment";
                        pnlResult.Visible = true;
                        pnlPaymentDetails.Visible = false;
                        lblResultSucess.Visible = true;
                        lblResultSucess.Text = "Payment completed successfully";
                    }
                    else
                    {
                        //LogExceptions.LogException("Callid:" + CallID + ", Invoice ref:" + InvoiceRefID + ", Error:", paymentResult);
                        //update the call status
                        //FLSDetailsBAL.UpdateTicketStatus(CallID, sessionKeys.UID, JobStatus.Cancelled);
                        pnlResult.Visible = true;
                        pnlPaymentDetails.Visible = false;
                        lblResultFail.Visible = true;
                        lblResultFail.Text = "Payment process failed, Please try again";

                        btnBack.Text = "Try payment again";
                    }

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);

                }

            }
            else
            {

                if (Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") == 0)
                    lblError.Text = "Please enter valid amount";
            }

        }
        public string CardConnectPay_ByAddress(UserMgt.Entity.Company payDetials,UserMgt.Entity.UserDetail userdetails, UserMgt.Entity.v_contractor user)
        {
            string retval = string.Empty;
            try
            {
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var ccnumber = txtCardConnectNumber.Text;
                if (string.IsNullOrEmpty(txtCardConnectNumber.Text))
                {
                    ccnumber = mytoken.Value;
                    if (string.IsNullOrEmpty(ccnumber))
                    {
                        lblError.Text = "Please enter card number";
                        //return;
                    }
                    LogExceptions.LogException("mytoken:" + ccnumber);
                }
                //get invoice data
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var cvv = txtCvv.Text.Trim();
                var MID = payDetials.Payment_vendor;
                var amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");

                //var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
                var p = new PortfolioMgt.Entity.PortfolioBilling();
                p.Amount = amount;
                p.BillingFrom = payDetials.Name + ", " + payDetials.Address + ", " + payDetials.Town + ", " + payDetials.City +", "+ payDetials.Zipcode;
                if (userdetails != null)
                    p.BillingTo = user.ContractorName + " ," + userdetails.Address1 + userdetails.Address2 + " ," + userdetails.Town + " ," + userdetails.PostCode;
                else
                    p.BillingTo = user.ContractorName;
                p.Currency = "USD";
                p.InvoiceSetDate = DateTime.Now;
                p.IsPaid = false;
                p.MonthlyPaymentDate = DateTime.Now;
                p.NumberofUsers = Convert.ToInt32(Session["usercount"].ToString());
                p.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");
                p.PaymentDate = DateTime.Now;
                p.PortfolioID = sessionKeys.PortfolioID;
                p.SendInvoice = false;
                p.TransationEndDate = hterm.Value == PortfolioMgt.BAL.PaymentTerm.Monthly ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMonths(12);
                p.TransationStartDate = DateTime.Now;
                p.PaymentTerm = hterm.Value;
                PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Add(p);

                List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile_Recurring(
                    payDetials.Payment_host + "/cardconnect/rest", payDetials.Payment_username, payDetials.Payment_password,
                    //Here will pass MID value
                    MID,
                    //mytoken.value 
                    ccnumber,
                    ddlCardType.SelectedValue,
                    //month year expiry
                    month_year_expiry,
                    txtCvv.Text.Trim(),
                    amount.ToString(),
                    //Order and address details
                    "", user.ContractorName, string.Empty,string.Empty, string.Empty, "USA", string.Empty, String.Format("{0:MMddyyyy}", DateTime.Now));
                //Get Reponse values
                foreach (var r in rval)
                {
                    retval = retval + "key:" + r.key + "value:" + r.value + "/n";
                }
                var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                if (ret != null)
                {
                    if (ret.value.ToString() == "Approval")
                    {

                        var addedData = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Select(p.ID);
                        if(addedData != null)
                        {
                            
                            addedData.PaymentReference = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                            addedData.PaymentProfile = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                            addedData.IsPaid = true;
                            PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Update(addedData);

                        }

                        //Process the return values 
                        // ProcessReturnValues(p, rval, InvoiceRefID);
                        //Display success process status
                        retval = "Approved";// ret.value.ToString();

                    }
                    else
                    {

                        //Display faild process status
                        retval = ret.value.ToString();
                    }
                    //ChangePanelVisibility();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;

        }

        private void ProcessReturnValues(PortfolioContactPaymentDetail p, List<ReponseValues> rval, int invoiceid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                pE.PayPalRef = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                pE.Payref = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                pE.IsPaid = true;

                pmRep.Edit(pE);

                var ipRep = new DCRepository<Incident_ServicePrice>();
                var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(invoiceid)).FirstOrDefault();
                if (pEntity != null)
                {
                    pEntity.Status = "Paid";
                    pEntity.ModifiedDate = DateTime.Now;
                    ipRep.Edit(pEntity);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl, false);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
    }
}