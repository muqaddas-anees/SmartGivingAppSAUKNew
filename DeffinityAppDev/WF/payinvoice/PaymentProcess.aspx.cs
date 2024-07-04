using Newtonsoft.Json.Linq;

using PortfolioMgt.BAL;
using PortfolioMgt.BLL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using DC.Entity;
using DC.BLL;
using System.Text.RegularExpressions;

using DC.BAL;
using DC.DAL;
using DC.SRV;
using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;

namespace DeffinityAppDev.WF.payinvoice
{
    public partial class PaymentProcess : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pcRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm> ptRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> pmRep = null;
        IDCRespository<Incident_ServicePrice> ipRep = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["uqid"] != null)
                {
                    var uqid = Request.QueryString["uqid"].ToString();
                    GetDefaultData(uqid);
                    PageLoadDefaults();
                }
                else
                {
                    pnlInvalidUrl.Visible = true;
                }
            }




        }

        private void GetDefaultData(string uqid)
        {
            try
            {
                var d = MailDataProcessBAL.MailDataProcess_SelectByUQID(uqid);
                if(d != null)
                {
                    if(d.IsActive)
                    {

                        var pdata = d.UQValues;
                        if(pdata.Length >0)
                        {
                            List<string> slist = pdata.Split(',').ToList();
                            foreach(var s in slist)
                            {
                                if (s.Contains("portfolioid"))
                                    sessionKeys.PortfolioID = Convert.ToInt32( ((s.Split(':')).ToArray())[1].ToString());
                                if (s.Contains("invid"))
                                    hinvid.Value = ((s.Split(':')).ToArray())[1].ToString();
                            }
                        }

                        pnlMain.Visible = true;
                    }
                    else
                    {
                        //pnlInvalidUrl.Visible = true;
                        pnlInvalidLink.Visible = true;
                    }
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void PageLoadDefaults()
        {
            // var workingdata = CardConnectRestClientExt.authTransaction();
            //return;
            if (Request.QueryString["addid"] != null)
            {
                paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var pEntity = paRep.GetAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["addid"].ToString())).FirstOrDefault();
                txtAmount.Text = string.Format("{0:F2}", pEntity.Amount.HasValue ? pEntity.Amount.Value : 0.00);
                hadid.Value = Request.QueryString["addid"].ToString();
                pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                var pcEntity = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();
                lblHeader.Text = pcEntity.Name + " - " + "Payment";
                pnlCardConnect.Visible = false;
                pnlCreditCard.Visible = true;
                rfCardnumber.Visible = false;
            }

            if (hinvid.Value.Length >0)
            {
                ipRep = new DCRepository<Incident_ServicePrice>();
                var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(hinvid.Value)).FirstOrDefault();

                txtAmount.Text = string.Format("{0:F2}", (pEntity.FinalPriceIncludeTax.HasValue ? pEntity.FinalPriceIncludeTax.Value : 0.00) > 0 ? pEntity.FinalPriceIncludeTax.Value : pEntity.RevicedPrice.HasValue ? pEntity.RevicedPrice.Value : 0.00);
                //txtAmount.Text = string.Format("{0:F2}", pEntity.OriginalPrice.HasValue ? pEntity.OriginalPrice.Value : 0.00);
                hadid.Value = hinvid.Value;
                var j = FLSDetailsBAL.Jqgridlist(pEntity.IncidentID.Value).FirstOrDefault();
                txtJobRef.Text = j.CCID.ToString();
                txtJobDetails.Text = j.Details;
                txtInvoiceRef.Text = pEntity.InvoiceRef;
                txtClientDetails.Text = j.RequesterName + Environment.NewLine + j.RequestersAddress + Environment.NewLine + j.RequestersCity + Environment.NewLine + j.RequestersPostCode;
                //pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                //var pcEntity = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();
                lblHeader.Text = "Invoice Payment";
                pnlCardConnect.Visible = true;
                pnlCreditCard.Visible = false;
                rfCardnumber.Visible = true;
            }


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
            var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
            if (payDetials.Host.ToLower().Contains("cardconnect"))
            {
                tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Host);
                pnlCardConnect.Visible = true;
                pnlCreditCard.Visible = false;
                rfCardnumber.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["addid"] != null)
                {
                    int addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                    paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                    var pEntity = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                    ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

                    pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var contact = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();

                    var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();

                    if (payDetials != null)
                    {

                        var TermEntity = ptRep.GetAll().Where(o => o.PCTID == pEntity.ContractTermID).FirstOrDefault();
                        if (TermEntity.Name == "Monthly")
                        {
                            //if month is grater than one month 
                            //if (Deffinity.Utility.MonthDifference(pEntity.StartDate.Value, pEntity.ExpiryDate.Value) > 1)
                            if (payDetials.Host.ToLower().Contains("mxmerchant"))
                            {
                                //MxMarchant_RecurringSubmit(payDetials);
                            }
                            else if (payDetials.Host.ToLower().Contains("cardconnect"))
                            {
                                CardConnectPay_ByAddress(payDetials, pEntity, contact);
                            }
                           
                            //CardConnectPay_ByAddress
                            //else
                            //    AddUpdateProfileWithTransaction_NonRcurring();
                        }
                        else
                        {
                            if (payDetials.Host.ToLower().Contains("mxmerchant"))
                            {
                                // AddUpdateMXMurchant_NonRecurring(payDetials);
                            }
                            else if (payDetials.Host.ToLower().Contains("cardconnect"))
                            {
                                CardConnectPay_ByAddress(payDetials, pEntity, contact);
                            }
                           
                        }
                    }
                }

                if (hinvid.Value.Length >0)
                {
                    ipRep = new DCRepository<Incident_ServicePrice>();
                    var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(hinvid.Value)).FirstOrDefault();

                    var callRep = new DCRepository<FLSDetail>();
                    var flsdetails = callRep.GetAll().Where(o => o.CallID == pEntity.IncidentID).FirstOrDefault();

                    int addressid = flsdetails.ContactAddressID.HasValue ? flsdetails.ContactAddressID.Value : 0;
                    paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                    var pAddressEntity = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                    //ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

                    pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var contact = pcRep.GetAll().Where(o => o.ID == pAddressEntity.ContactID).FirstOrDefault();

                    var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();

                    if (payDetials != null)
                    {
                        if (payDetials.Host.ToLower().Contains("cardconnect"))
                        {
                            CardConnectPay(payDetials, pAddressEntity, contact, pEntity);
                        }
                    }
                }
                //PayMethodNew();
                //OldPayMethod();
                //WorkingPaypal();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #region MX Marchant
        public static string baseURL = "https://sandbox.api.mxmerchant.com/checkout/v3";

        public static readonly string endPointRequestToken = baseURL + "/oauth/1a/requesttoken";
        public static readonly string endPointAccessToken = baseURL + "/oauth/1a/accesstoken";
        public static readonly string createPayment = baseURL + "/payment";
        public static readonly string createCustomer = baseURL + "/customer";
        public static readonly string createCustomercardaccount = baseURL + "/customercardaccount";
        public static readonly string createContract = baseURL + "/contractsubscription";
        public static readonly string getPayment = baseURL + "/payment/{0}";
        public static readonly string getCustomer = baseURL + "/customer/{0}";
        public static readonly string getCustomercardaccount = baseURL + "/customercardaccount?id={0}";
        public static readonly string getContract = baseURL + "/contract/{0}";
        public static readonly string getPayments = baseURL + "/payment";

       

        #endregion

        #region Pay flow payment
      
       

        private NameValueCollection GetPayPalCollection(string payPalInfo)
        {
            //place the responses into collection
            NameValueCollection PayPalCollection = new System.Collections.Specialized.NameValueCollection();
            string[] ArrayReponses = payPalInfo.Split('&');

            for (int i = 0; i < ArrayReponses.Length; i++)
            {
                string[] Temp = ArrayReponses[i].Split('=');
                PayPalCollection.Add(Temp[0], Temp[1]);
            }
            return PayPalCollection;
        }
        private string ShowPayPalInfo(NameValueCollection collection)
        {
            string PayPalInfo = "";
            foreach (string key in collection.AllKeys)
            {
                PayPalInfo += "<br /><span class=\"bold-text\">" + key + ":</span> " + collection[key];
            }
            return PayPalInfo;
        }
        string _TransactionID = string.Empty;
       
       


       


        #endregion


        #region Card Connect Payment
        public void CardConnectPay(PortfolioPaymentSetting payDetials, PortfolioContactAddress pAddress,
            PortfolioContact pContact, Incident_ServicePrice InvoiceDetails)
        {
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
                        lblCardERROR.Text = "Please enter card number";
                        return;
                    }
                    LogExceptions.LogException("mytoken:" + ccnumber);
                }
                pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var p = new PortfolioMgt.Entity.PortfolioContactPaymentDetail();
                p.AddressID = pAddress.ID;
                p.IsPaid = false;
                p.PaidAmount = Convert.ToDouble(txtAmount.Text.Trim()); //InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00;
                p.PayDate = DateTime.Now;
                p.PayOnWebsite = false;
                //p.Payref = pref;
                p.OrderRef = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;
                p.InvoiceID = InvoiceDetails.ID;
                pmRep.Add(p);
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var cvv = txtCvv.Text.Trim();
                var MID = payDetials.Vendor;
                var amount = txtAmount.Text.Trim();//  (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();

                string retval = string.Empty;
                List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile(
                    payDetials.Host + "/cardconnect/rest", payDetials.Username, payDetials.Password,
                    //Here will pass MID value
                    MID,
                    //mytoken.value 
                    ccnumber,
                    ddlCardType.SelectedValue,
                    //month year expiry
                    month_year_expiry,
                    txtCvv.Text.Trim(),
                    amount,
                    //Order and address details
                    p.OrderRef, pContact.Name, pAddress.State, pAddress.City, pAddress.Address, "USA", pAddress.PostCode);
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
                        //Process the return values 
                        ProcessReturnValues(p, rval);
                        //Display success process status
                        lblResult.Text = "Payment completed successfully";// ret.value.ToString();
                                                                          //update the url
                        UpdateMailActiveLink();

                        FLS_SendPaidMail(InvoiceDetails.IncidentID.Value, InvoiceDetails.ID);

                    }
                    else
                    {
                        //Display faild process status
                        lblResult.Text = ret.value.ToString();
                    }
                    ChangePanelVisibility();
                }
                LogExceptions.LogException(retval);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if(rval.Where(o=>o.key == ""))
            //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
            //pE.PayPalRef = RecurResponse.TrxPNRef;
            //pE.IsPaid = true;
            //pmRep.Edit(pE);
            ////Genrate policy and generate pdf
            ////GeneratePolicy.SendPolicyMail(addressid);
            //lblResult.Text = TrxnResponse.RespMsg;
            //var PNREF = TrxnResponse.Pnref;
            //var PPREF = TrxnResponse.PPref;
            //pnlResult.Visible = true;
            //pnlCCD.Visible = false;

        }

        private void UpdateMailActiveLink()
        {
            try
            {
                var mEntity = MailDataProcessBAL.MailDataProcess_SelectByUQID(Request.QueryString["uqid"].ToString());
                if (mEntity != null)
                {
                    mEntity.IsActive = false;
                    MailDataProcessBAL.MailDataProcess_Update(mEntity);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static void FLS_SendPaidMail(int callid, int invoicerefid)
        {
            try
            {
                //int callid = callid;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
               WebService ws = new WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, sessionKeys.PortfolioID);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var fls = FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
                        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid && c.Incident_ServicePriceID == invoicerefid).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid && c.ID == invoicerefid).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Job Reference: " + fls.CCID.ToString() + " Invoice Payment Completed Successfully";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoicePaid.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                       



                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));

                        body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                      
                        body = body.Replace("[invno]", pricelist.InvoiceRef.ToString());
                        body = body.Replace("[refno]", fls.CCID.ToString());
                       
                       
                     

                        //[date]

                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester
                        body = body.Replace("[user]", pcontact.Name);

                        em.SendingMail(fromemailid, subject, body, portfolio.EmailAddress);

                      

                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SendPaidMail()
        {
            try
            {
                var mEntity = MailDataProcessBAL.MailDataProcess_SelectByUQID(Request.QueryString["uqid"].ToString());
                if (mEntity != null)
                {
                    mEntity.IsActive = false;
                    MailDataProcessBAL.MailDataProcess_Update(mEntity);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void ChangePanelVisibility()
        {
            pnlResult.Visible = true;
            pnlCCD.Visible = false;
        }

        private void ProcessReturnValues(PortfolioContactPaymentDetail p, List<ReponseValues> rval)
        {
            try
            {
                var pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                pE.PayPalRef = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                pE.Payref = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                pE.IsPaid = true;

                pmRep.Edit(pE);

                ipRep = new DCRepository<Incident_ServicePrice>();
                var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(hinvid.Value)).FirstOrDefault();
                if (pEntity != null)
                {
                    pEntity.Status = "Paid";
                    pEntity.ModifiedDate = DateTime.Now;
                    ipRep.Edit(pEntity);
                }
                //check the matenance policy already exists or not
                int policyid = 0;
                List<PolicyOutput> poutlist = new List<PolicyOutput>();
                Regex regex = new Regex(@"\(([^()]+)\)*");
                var ISRep = new DCRepository<Incident_Service>();
                var inList = ISRep.GetAll().Where(o => o.Incident_ServicePriceID == Convert.ToInt32(hinvid.Value)).ToList();
                var getJobDetails = FLSDetailsBAL.Jqgridlist(inList.FirstOrDefault().IncidentID.Value).FirstOrDefault();
                foreach (var iv in inList)
                {

                    //check policy and add on records are exists
                    //will get two policy 
                    foreach (Match match in regex.Matches(iv.ServiceDescription))
                    {

                        //(MP-0000-1) 1 is policy type id
                        //(AD-0000-1) 1 is add on id
                        string[] str = match.Value.ToString().Split('-');
                        if (str[0].Contains("MP"))
                        {
                            string policyid_str = str[2].Replace(")", "");
                            if (policyid_str.Length > 0)
                            {
                                poutlist.Add(new PolicyOutput() { name = policy, id = Convert.ToInt32(policyid_str), addressid = getJobDetails.ContactAddressID, amout = (((iv.SellingPrice.HasValue ? iv.SellingPrice.Value : 0) * (iv.QTY.HasValue ? iv.QTY.Value : 0)) + (iv.VAT.HasValue ? iv.VAT.Value : 0)) });
                            }
                        }

                        if (str[0].Contains("AD"))
                        {
                            string addonid_str = str[2].Replace(")", "");
                            if (addonid_str.Length > 0)
                            {
                                poutlist.Add(new PolicyOutput() { name = addon, id = Convert.ToInt32(addonid_str), addressid = getJobDetails.ContactAddressID, amout = (((iv.SellingPrice.HasValue ? iv.SellingPrice.Value : 0) * (iv.QTY.HasValue ? iv.QTY.Value : 0)) + (iv.VAT.HasValue ? iv.VAT.Value : 0)) });
                            }
                        }
                        // get policy data

                    }
                }

                if (poutlist.Count > 0)
                {
                    //get calldetails and 
                    var callid = pEntity.IncidentID.HasValue ? pEntity.IncidentID.Value : 0;
                    if (callid > 0)
                    {

                        //Get the policy add on

                       // DeffinityAppDev.Controllers.ContactController c = new Controllers.ContactController();
                        //Get requester adderess deatils
                        var paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                        IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                        var pAddressEntity = paRep.GetAll().Where(o => o.ID == getJobDetails.ContactAddressID).FirstOrDefault();

                        string str_addonids = string.Empty;
                        foreach (var a in poutlist.Where(o => o.name == addon).ToList())
                        {
                            if (paRepository.GetAll().Where(o => o.AddonID == a.id && o.AddressID == a.addressid).FirstOrDefault() == null)
                            {
                                //insert added ons
                                var caid = new PortfolioMgt.Entity.ProductAddonPriceAssociate();
                                caid.AddonID = a.id;
                                caid.AddressID = a.addressid;
                                paRepository.Add(caid);

                                str_addonids = str_addonids + a.id.ToString() + ",";
                            }
                        }




                        foreach (var a in poutlist.Where(o => o.name == policy).ToList())
                        {
                            //update the invoice deatils
                            if (pAddressEntity != null)
                            {
                                pAddressEntity.PolicyTypeID = a.id;
                                pAddressEntity.StartDate = DateTime.Now;
                                pAddressEntity.ExpiryDate = DateTime.Now.AddMonths(12);
                                pAddressEntity.PolicyStartsID = 1;
                                //try
                                //{
                                //    pAddressEntity.PolicyNumber = c.WSGetPolicyno(a.id);
                                //}
                                //catch (Exception ex)
                                //{
                                //    LogExceptions.WriteExceptionLog(ex);
                                //}
                                //2 is one year
                                pAddressEntity.ContractTermID = 2;
                                // pAddressEntity.Amount =
                                pAddressEntity.Amount = poutlist.Sum(o => o.amout);
                                if (pAddressEntity.BillingAddress1 != null)
                                    pAddressEntity.BillingAddress1 = pAddressEntity.Address;
                                if (pAddressEntity.BillingAddress2 != null)
                                    pAddressEntity.BillingAddress2 = pAddressEntity.Address2;
                                if (pAddressEntity.City != null)
                                    pAddressEntity.City = pAddressEntity.City;
                                if (pAddressEntity.State != null)
                                    pAddressEntity.BillingState = pAddressEntity.State;
                                if (pAddressEntity.BillingZipcode != null)
                                    pAddressEntity.BillingZipcode = pAddressEntity.PostCode;



                                paRep.Edit(pAddressEntity);

                                //update 
                                pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                                pE.ProductPolicyType = a.id;
                                pE.ProductPolicyAddonIds = str_addonids;




                                pmRep.Edit(pE);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public string policy = "policy";
        public string addon = "addon";
        public class PolicyOutput
        {
            public string name { set; get; }
            public int id { set; get; }
            public int addressid { set; get; }
            public double amout { set; get; }

        }

        public void CardConnectPay_ByAddress(PortfolioPaymentSetting payDetials, PortfolioContactAddress pAddress, PortfolioContact pContact)
        {
            try
            {
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var pEntity = paRep.GetAll().Where(o => o.ID == Convert.ToInt32(hadid.Value)).FirstOrDefault();
                pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                var pcEntity = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();
                ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();
                var TermEntity = ptRep.GetAll().Where(o => o.PCTID == pEntity.ContractTermID).FirstOrDefault();
                var pref = Guid.NewGuid().ToString();
                //add paymnet details
                pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var p = new PortfolioMgt.Entity.PortfolioContactPaymentDetail();
                p.AddressID = Convert.ToInt32(hadid.Value);
                p.IsPaid = false;
                p.PaidAmount = pEntity.Amount.HasValue ? pEntity.Amount.Value : 0.00;
                p.PayDate = DateTime.Now;
                p.PayOnWebsite = false;
                p.Payref = string.Empty;
                pmRep.Add(p);

                lblHeader.Text = pcEntity.Name + " - " + "Payment";

                //ddlMonth.SelectedValue + ddlYear.SelectedValue

                List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile(payDetials.Host + "/cardconnect/rest", payDetials.Username, payDetials.Password, payDetials.Vendor,
                    txtCardNumber.Text, ddlCardType.SelectedValue, ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), (pEntity.Amount.HasValue ? pEntity.Amount.Value : 0.00).ToString(),
                    p.OrderRef, pContact.Name, pAddress.State, pAddress.City, pAddress.Address, "USA", pAddress.PostCode);

                string retval = string.Empty;
                foreach (var r in rval)
                {
                    retval = retval + "key:" + r.key + "value:" + r.value + "/n";

                    //if(rval.Where(o=>o.key == ""))
                    //var pE = pmRep.GetAll().Where(o => o.PayID == pref).FirstOrDefault();
                    //pE.PayPalRef = RecurResponse.TrxPNRef;
                    //pE.IsPaid = true;
                    //pmRep.Edit(pE);
                    ////Genrate policy and generate pdf
                    ////GeneratePolicy.SendPolicyMail(addressid);
                    //lblResult.Text = TrxnResponse.RespMsg;
                    //var PNREF = TrxnResponse.Pnref;
                    //var PPREF = TrxnResponse.PPref;
                    //pnlResult.Visible = true;
                    //pnlCCD.Visible = false;

                }
                var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                if (ret != null)
                {
                    if (ret.value.ToString() == "Approval")
                    {
                        //if(rval.Where(o=>o.key == ""))
                        var pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                        pE.PayPalRef = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                        pE.Payref = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : "";//profileid
                        pE.IsPaid = true;
                        pmRep.Edit(pE);

                        //ipRep = new DCRepository<Incident_ServicePrice>();
                        //var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["invid"].ToString())).FirstOrDefault();
                        //if (pEntity != null)
                        //{
                        //    pEntity.Status = "Paid";
                        //    pEntity.ModifiedDate = DateTime.Now;
                        //    ipRep.Edit(pEntity);
                        //}
                        lblResult.Text = "Approved";// ret.ToString();
                    }
                    else
                    {
                        //Genrate policy and generate pdf
                        //GeneratePolicy.SendPolicyMail(addressid);
                        lblResult.Text = ret.value.ToString();
                    }

                    pnlResult.Visible = true;
                    pnlCCD.Visible = false;


                }
                LogExceptions.LogException(retval);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        #endregion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString["ContactID"] != null)
            //    Response.Redirect(string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), Request.QueryString["addid"].ToString()));
            //else if (Request.QueryString["invid"] != null)
            //{
            //    Response.Redirect(string.Format("~/WF/DC/DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID));
            //}

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString["ContactID"] != null)
            //    Response.Redirect(string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), Request.QueryString["addid"].ToString()));

            //if (Request.QueryString["invid"] != null)
            //{
            //    Response.Redirect(string.Format("~/WF/DC/DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID));
            //}
        }
    }
}