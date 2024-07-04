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

namespace DeffinityAppDev.WF.DC
{
    public partial class TakePaymentNow : System.Web.UI.Page
    {
        public const string  paymentSucessStatus = "Approved";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showPanels();
                pamentFieldsDefaults();
            }

        }
        private void showPanels()
        {
            try
            {
                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                if(payDetials != null)
                {
                    var mid = payDetials.Vendor.Trim();
                    if (mid.Length == 0)
                    {
                        pnlccd.Visible = true;
                        pnltakepayment.Attributes.Add("class", "col-sm-8");
                    }
                    else
                    {
                        pnlccd.Visible = false;
                        pnltakepayment.Attributes.Add("class", "col-sm-12");
                    }
                }
            }
            catch(Exception ex)
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
                if (payDetials.Host.ToLower().Contains("cardconnect"))
                {
                    tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Host);
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
            if (hContact.Value != "" && hAddress.Value != "" && Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") >0)
            {
                try
                {
                    var contactid = Convert.ToInt32( !string.IsNullOrEmpty(hContact.Value) ? hContact.Value : "0");
                    var addressid = Convert.ToInt32(!string.IsNullOrEmpty(hAddress.Value) ? hAddress.Value : "0");
                    var dt = DateTime.Now;
                    var dtEnd = DateTime.Now.AddHours(1);
                    //Create job
                    //add to call details
                    var c = new CallDetail();
                    c.CompanyID = sessionKeys.PortfolioID;
                    c.LoggedBy = sessionKeys.UID;
                    c.LoggedDate = DateTime.Now;
                    c.RequesterID = Convert.ToInt32(hContact.Value);
                    //6 default
                    c.RequestTypeID = 6;
                    c.SiteID = 0;
                    c.StatusID = JobStatus.New;
                    CallDetailsBAL.AddCallDetails(c);
                    var CallID = c.ID;
                    //Journal entiry
                    CallDetailsJournalBAL.AddCallDetailsJournal(c);
                    

                    try
                    {
                        //add to fls details
                        var f = new FLSDetail();
                        f.CallID = c.ID;
                        f.CategoryID = 0;
                        f.ContactAddressID = addressid;
                        f.DateTimeClosed = dtEnd;
                        f.DateTimeStarted = dt;
                        f.DepartmentID = 0;
                        f.Details = txtDetails.Text.Trim();
                        f.PriorityId = 0;
                        f.ScheduledDate = dt;
                        f.ScheduledEndDateTime = dtEnd;
                        f.SourceOfRequestID = 0;
                        f.SubCategoryID = 0;
                        f.SubjectID = 0;
                        f.UserID = sessionKeys.UID;
                        FLSDetailsBAL.AddFLSDetails(f);
                        //add to journal
                        FLSDetailsJournalBAL.AddFLSDetailsJournal(f);
                        //add data to history

                        //Create invoice ref
                        var iRef = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = c.ID }, sessionKeys.PortfolioID,sessionKeys.UID);
                        int InvoiceRefID = iRef.ID;
                        //add item incoice item
                        InvoiceBAL.AddIvoiceItem(sessionKeys.PortfolioID, 0, c.ID, 1, 0, "", "Item", 0, Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00"), InvoiceRefID);
                        //Update the invoice price
                        //Service_Prices(CallID, iRef.ID);

                        //payment 
                        //int addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                        var paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                        var pAddress = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                        var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

                        var pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                        var pcontact = pcRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                        var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                        //Payment details
                        var paymentResult = string.Empty;
                        if (payDetials != null)
                        {
                            paymentResult = CardConnectPay_ByAddress(payDetials, pAddress, pcontact, InvoiceRefID);
                        }

                        if(paymentResult == paymentSucessStatus)
                        {
                            //update the call status
                            FLSDetailsBAL.UpdateTicketStatus(CallID, sessionKeys.UID, JobStatus.Closed);
                            btnBack.Text = "Take another payment";
                            pnlResult.Visible = true;
                            pnlPaymentDetails.Visible = false;
                            lblResultSucess.Visible = true;
                            lblResultSucess.Text = "Payment completed successfully";
                        }
                        else
                        {
                            LogExceptions.LogException("Callid:"+ CallID+", Invoice ref:"+InvoiceRefID+", Error:", paymentResult);
                            //update the call status
                            FLSDetailsBAL.UpdateTicketStatus(CallID, sessionKeys.UID, JobStatus.Cancelled);
                            pnlResult.Visible = true;
                            pnlPaymentDetails.Visible = false;
                            lblResultFail.Visible = true;
                            lblResultFail.Text = "Payment process failed, Please try again";

                            btnBack.Text = "Try payment again";
                        }
                      
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                        FLSDetailsBAL.UpdateTicketStatus(CallID, sessionKeys.UID, JobStatus.Cancelled);
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            else
            {
                if (hContact.Value == "")
                    lblError.Text = "Please select client";
                if (hAddress.Value == "")
                    lblError.Text = "Please select address";
                if (Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") == 0)
                    lblError.Text = "Please enter valid amount";
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
                        retval= "Approved";// ret.value.ToString();
                       
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

        private void ProcessReturnValues(PortfolioContactPaymentDetail p, List<ReponseValues> rval,int invoiceid)
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
        private void Service_Prices(int callid,int InvoiceRef)
        {
            SqlDataReader dr = IncidentService_Price.IncidentService_Price_Select(callid,"", InvoiceRef);
            while (dr.Read())
            {
                
            }
            dr.Close();
            dr.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl,false);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
    }
}