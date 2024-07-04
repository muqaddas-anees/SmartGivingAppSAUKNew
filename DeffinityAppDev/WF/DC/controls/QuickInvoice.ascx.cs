using DC.BLL;
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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DeffinityAppDev.WF.WM;
using Org.BouncyCastle.Asn1.Ocsp;
using DocumentFormat.OpenXml.Drawing;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class QuickInvoice : System.Web.UI.UserControl
    {
        public const string paymentSucessStatus = "Approved";
        public static int step = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //set unique id for transaction
                huid.Value = Guid.NewGuid().ToString();
                showPanels();
                pamentFieldsDefaults();
                PanelChange();
            }

        }
        private void showPanels()
        {
            try
            {
                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                if (payDetials != null)
                {
                    var mid = payDetials.Vendor.Trim();
                    if (mid.Length == 0)
                    {
                        pnlccd.Visible = false;
                        pnltakepayment.Attributes.Add("class", "col-sm-12");
                    }
                    else
                    {
                        pnlccd.Visible = false;
                        pnltakepayment.Attributes.Add("class", "col-sm-12");
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                if (payDetials != null)
                {
                    if (payDetials.Host.ToLower().Contains("cardconnect"))
                    {
                        tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Host);
                        pnlCardConnect.Visible = true;
                        pnlCreditCard.Visible = false;
                        rfCardnumber.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitQuote_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtContactEmail.Text.Trim()))
                {
                    var contactname = txtContactName.Text.Trim();
                    var contactemail = txtContactEmail.Text.Trim();
                    var portfolioid = sessionKeys.PortfolioID;
                    var userid = sessionKeys.UID;
                    var jobDetails = txtDetails.Text.Trim();
                    var amount = txtAmount.Text.Trim();
                    //Create contact 
                    var contactAddress = QuickPayBAL.CreateContact(contactname, contactemail, portfolioid, userid);
                    //Create address
                    var jobEntity = QuickPayBAL.Create_a_Job(contactAddress, jobDetails, portfolioid, userid);

                    var quote = QuickPayBAL.Create_a_Quote(jobEntity, jobDetails, portfolioid, userid, Convert.ToDouble(amount), huid.Value);
                    if (quote.ID > 0)
                    {
                        FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Quote_Sent);
                        pnlResult.Visible = true;
                        pnlPaymentDetails.Visible = false;

                        lblResultSucess.Text = "Great! You’re all done. Your quote has been sent to the customer.";
                        lblResultSucess.Visible = true;
                        btnBack.Text = "Send Another Quote";
                    }
                    //
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtContactEmail.Text.Trim()))
            {
                var contactname = txtContactName.Text.Trim();
                var contactemail = txtContactEmail.Text.Trim();
                var portfolioid = sessionKeys.PortfolioID;
                var userid = sessionKeys.UID;
                var jobDetails = txtDetails.Text.Trim();
                var amount = txtAmount.Text.Trim();
                var ccnumber = txtCardConnectNumber.Text;
                var cvv = txtCvv.Text;
                var year_expiry = ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                //Create contact 
                var contactAddress = QuickPayBAL.CreateContact(contactname, contactemail, portfolioid, userid);
                //Create address
                var jobEntity = QuickPayBAL.Create_a_Job(contactAddress, jobDetails, portfolioid, userid);
                var paymentResult = string.Empty;
                paymentResult = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value);

                if (paymentResult == paymentSucessStatus)
                {
                    //update the call status
                    FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Closed);
                    btnBack.Text = "Take another Quick payment";
                    pnlResult.Visible = true;
                    pnlPaymentDetails.Visible = false;
                    lblResultSucess.Visible = true;
                    lblResultSucess.Text = "Great! You’re all done. Your invoice has been sent to the customer.";
                }
                else
                {
                    //                    LogExceptions.LogException("Callid:" + jobEntity.ID + ", Invoice ref:" + InvoiceRefID + ", Error:", paymentResult);
                    ////update the call status
                    FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Cancelled);
                    pnlResult.Visible = true;
                    pnlPaymentDetails.Visible = false;
                    lblResultFail.Visible = true;
                    lblResultFail.Text = "Payment process failed, Please try again";

                    btnBack.Text = "Try payment again";
                }
            }


        }
        public string CardConnectPay_ByAddress(PortfolioPaymentSetting payDetials, PortfolioContactAddress pAddress, PortfolioContact pContact, int InvoiceRefID)
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
                var ipRep = new DCRepository<Incident_ServicePrice>();
                var InvoiceDetails = ipRep.GetAll().Where(o => o.ID == InvoiceRefID).FirstOrDefault();

                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
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
                var amount = txtAmount.Text.Trim(); //(InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString();


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
                        ProcessReturnValues(p, rval, InvoiceRefID);
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
        private void Service_Prices(int callid, int InvoiceRef)
        {
            SqlDataReader dr = IncidentService_Price.IncidentService_Price_Select(callid, "", InvoiceRef);
            while (dr.Read())
            {

            }
            dr.Close();
            dr.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl, false);

        }

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(Request.RawUrl, false);
        //}

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                step--;
                if (step < 1)
                {
                    step = 1;
                }
                PanelChange();
                //Response.Redirect("~/App/AddEducationalVideo.aspx", false);
                //  pnlIsRecurring.Visible = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btAddClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx", false);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (step == 4)

                {
                    if (!string.IsNullOrEmpty(txtContactEmail.Text.Trim()))
                    {
                        var contactname = txtContactName.Text.Trim();
                        var contactemail = txtContactEmail.Text.Trim();
                        var portfolioid = sessionKeys.PortfolioID;
                        var userid = sessionKeys.UID;
                        var jobDetails = txtDetails.Text.Trim();
                        var amount = txtAmount.Text.Trim();
                        var ccnumber = txtCardConnectNumber.Text;
                        var cvv = txtCvv.Text;
                        var year_expiry = ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                        //Create contact 
                        var contactAddress = QuickPayBAL.CreateContact(contactname, contactemail, portfolioid, userid);
                        //Create address
                        var jobEntity = QuickPayBAL.Create_a_Job(contactAddress, jobDetails, portfolioid, userid);
                        var paymentResult = string.Empty;
                        paymentResult = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value);

                        if (paymentResult == paymentSucessStatus)
                        {
                            //update the call status
                            FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Closed);
                           // btnBack.Text = "Take another Quick payment";
                           // pnlResult.Visible = true;
                           // pnlPaymentDetails.Visible = false;
                            lblResultSucess.Visible = true;
                            lblResultSucess.Text = "Your client will now receive your invoice. Sometimes the email can land into their Junk so please ask them to take a peek in there just in case they don’t see it immediately in their inbox. ";
                        }
                        else
                        {
                            //                    LogExceptions.LogException("Callid:" + jobEntity.ID + ", Invoice ref:" + InvoiceRefID + ", Error:", paymentResult);
                            ////update the call status
                            FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Cancelled);
                           // pnlResult.Visible = true;
                           // pnlPaymentDetails.Visible = false;
                            lblResultFail.Visible = true;
                            lblResultFail.Text = "Payment process failed, Please try again";

                           // btnBack.Text = "Try payment again";
                        }
                    }

                }

                step++;


                if (step > 5)
                {
                    step = 1;
                }
                PanelChange();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
                    private void panelVisibility(bool isWelcome, bool isClientdetails,bool isCardDetails, bool isAttachfile, bool isResult)
        {
            pnlwelcome.Visible = isWelcome;
            pnlClientDetails.Visible = isClientdetails;
            pnlCardDetails.Visible = isCardDetails;
            pnlAttachfiles.Visible = isAttachfile;
            pnlResult.Visible = isResult;
        }
        private void PanelChange()
        {

            if (step == 1)
            {
                lblheader.Text = "";
                panelVisibility(true, false, false, false,false);
                btnBack.Visible = false;
                btnNext.Visible = false;
            }
            else if (step == 2)
            {
                lblheader.Text = "Client Details";
                panelVisibility(false, true, false, false,false);
                btnBack.Visible = true;
                btnNext.Visible = true;
                btnNext.Text = "Next";
            }
            else if (step == 3)
            {
                lblheader.Text = "Card Details";
                panelVisibility(false, false, true, false,false);
                btnBack.Visible = true;
                btnNext.Visible = true;
                // btnNext.Text = "Send Quick Quote";
            }
            else if (step == 4)
            {
                lblheader.Text = "Attach Files";
                panelVisibility(false, false, false, true,false);
                btnBack.Visible = true;
                btnNext.Visible = true;
                 btnNext.Text = "Process Payment";
            }
            else if (step == 5)
            {
                lblheader.Text = "";
                panelVisibility(false, false, false, false,true);
                btnBack.Visible = false;
                btnNext.Visible = false;
                // btnNext.Text = "Process Payment";
            }
        }

        protected void btnBackToHOme_Click(object sender, EventArgs e)
        {
            step = 1;
            Response.Redirect(Request.RawUrl, false);
        }
    }
}