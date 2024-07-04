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
using System.Web.UI.HtmlControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class QuickQuoteCtrl : System.Web.UI.UserControl
    {
        public const string paymentSucessStatus = "Approved";
        public static int step_New = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //set unique id for transaction
                huid.Value = Guid.NewGuid().ToString();
                //showPanels();
                //pamentFieldsDefaults();

               // PanelChange();
            }

            //HtmlLink subcss = new HtmlLink();
            //subcss.Href = "~/Content/assets/js/
            //ne/css/dropzone.css";
            //subcss.Attributes.Add("rel", "stylesheet");
            //subcss.Attributes.Add("type", "text/css");
            //Page.Header.Controls.Add(subcss);

            //var script = new HtmlGenericControl();
            //script.TagName = "script";
            //script.Attributes.Add("type", "text/javascript");
            //script.Attributes.Add("src", script.ResolveUrl("~/Content/assets/js/dropzone/dropzone.min.js"));

            //Page.Header.Controls.Add(script);
        }


        //public static HtmlLink CreateCssLink(string cssFilePath, string media)
        //{
        //    var link = new HtmlLink();
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    link.Href = link.ResolveUrl(cssFilePath);
        //    if (string.IsNullOrEmpty(media))
        //    {
        //        media = "all";
        //    }

        //    link.Attributes.Add("media", media);
        //    return link;
        //}

        //public static HtmlGenericControl CreateJavaScriptLink(string scriptFilePath)
        //{
        //    var script = new HtmlGenericControl();
        //    script.TagName = "script";
        //    script.Attributes.Add("type", "text/javascript");
        //    script.Attributes.Add("src", script.ResolveUrl(scriptFilePath));

        //    return script;
        //}
        private void showPanels()
        {
            //try
            //{
            //    var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
            //    if (payDetials != null)
            //    {
            //        var mid = payDetials.Vendor.Trim();
            //        if (mid.Length == 0)
            //        {
            //            pnlccd.Visible = false;
            //            pnltakepayment.Attributes.Add("class", "col-sm-12");
            //        }
            //        else
            //        {
            //            pnlccd.Visible = false;
            //            pnltakepayment.Attributes.Add("class", "col-sm-12");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
        }
        private void pamentFieldsDefaults()
        {
            try
            {
                //populate month
                string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                //ddlMonth.DataSource = Month;
                //ddlMonth.DataBind();
                ////pre-select one for testing
                //ddlMonth.SelectedIndex = 4;

                ////populate year
                //ddlYear.Items.Add("");
                //int Year = DateTime.Now.Year;
                //for (int i = 0; i < 10; i++)
                //{
                //    ddlYear.Items.Add((Year + i).ToString());
                //}
                ////pre-select one for testing
                //ddlYear.SelectedIndex = 3;

                //// set the url for iframe for card connect payment
                //var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                //if (payDetials != null)
                //{
                //    if (payDetials.Host.ToLower().Contains("cardconnect"))
                //    {
                //        tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Host);
                //        pnlCardConnect.Visible = true;
                //        pnlCreditCard.Visible = false;
                //        rfCardnumber.Visible = true;
                //    }
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnQuoteNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (hquotestep.Value == "2")

                {
                    //add pament code
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
                           // pnlResult.Visible = true;
                           // pnlPaymentDetails.Visible = false;

                            lblResultSucess.Text = "Your client will have the quotation sitting in their inbox ready to approve";
                            lblResultSucess.Visible = true;
                            //btnBack.Text = "Send Another Quote";
                            hquotestep.Value = "3";

                            txtContactEmail.Text = string.Empty;
                            txtContactName.Text = string.Empty;
                            txtAmount.Text = "0.00";
                            txtDetails.Text = string.Empty;
                        }
                        //
                    }

                   // sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                   // Response.Redirect(Request.RawUrl, false);
                    //return;
                }

                //step_New++;


                //if (step_New > 4)
                //{
                //    step_New = 1;
                //}
               // PanelChange();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void panelVisibility(bool isWelcome, bool isClientdetails, bool isAttachfile, bool isResult)
        {
            //pnlwelcome.Visible = isWelcome;
            //pnlClientDetails.Visible = isClientdetails;
            //pnlAttachfiles.Visible = isAttachfile;
            //pnlResult.Visible = isResult;
        }
        //private void PanelChange()
        //{

        //    if (step_New == 1)
        //    {
        //        lblheader.Text = "";
        //        panelVisibility(true, false, false, false);
        //        btnBack.Visible = false;
        //        btnNext.Visible = false;
        //    }
        //    else if (step_New == 2)
        //    {
        //        lblheader.Text = "Client Details";
        //        panelVisibility(false, true, false, false);
        //        btnBack.Visible = true;
        //        btnNext.Visible = true;
        //        btnNext.Text = "Next";
        //    }
        //    else if (step_New == 3)
        //    {
        //        lblheader.Text = "Attach Files";
        //        panelVisibility(false, false, true, false);
        //        btnBack.Visible = true;
        //        btnNext.Text = "Send Quick Quote";
        //    }
        //    else if (step_New == 4)
        //    {
        //        lblheader.Text = "";
        //        panelVisibility(false, false, false, true);
        //        btnBack.Visible = false;
        //        btnNext.Visible = false;
        //       // btnNext.Text = "Process Payment";
        //    }
        //}
        
              protected void btnBackToHOme_Click(object sender, EventArgs e)
        {
            step_New = 1;
            Response.Redirect(Request.RawUrl,false);
        }
            protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                step_New--;
                if (step_New < 1)
                {
                    step_New = 1;
                }
               // PanelChange();
                //Response.Redirect("~/App/AddEducationalVideo.aspx", false);
                //  pnlIsRecurring.Visible = true;
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
                        pnlQuoteResult.Visible = true;
                        //pnlPaymentDetails.Visible = false;

                        lblResultSucess.Text = "Great! You’re all done. Your quote has been sent to the customer.";
                        lblResultSucess.Visible = true;
                       // btnBack.Text = "Send Another Quote";
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


            //if (!string.IsNullOrEmpty(txtContactEmail.Text.Trim()))
            //{
            //    var contactname = txtContactName.Text.Trim();
            //    var contactemail = txtContactEmail.Text.Trim();
            //    var portfolioid = sessionKeys.PortfolioID;
            //    var userid = sessionKeys.UID;
            //    var jobDetails = txtDetails.Text.Trim();
            //    var amount = txtAmount.Text.Trim();
            //    var ccnumber = txtCardConnectNumber.Text;
            //    var cvv = txtCvv.Text;
            //    var year_expiry = ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
            //    //Create contact 
            //    var contactAddress = QuickPayBAL.CreateContact(contactname, contactemail, portfolioid, userid);
            //    //Create address
            //    var jobEntity = QuickPayBAL.Create_a_Job(contactAddress, jobDetails, portfolioid, userid);
            //    var paymentResult = string.Empty;
            //    paymentResult = QuickPayBAL.Create_Invoice(jobDetails, contactname, portfolioid, jobEntity, userid, amount, contactAddress.ID, ccnumber, ddlMonth.SelectedValue, year_expiry, cvv, ddlCardType.SelectedValue, huid.Value);

            //    if (paymentResult == paymentSucessStatus)
            //    {
            //        //update the call status
            //        FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Closed);
            //        //btnBack.Text = "Take another Quick payment";
            //        pnlResultQuote.Visible = true;
            //       // pnlPaymentDetails.Visible = false;
            //        lblResultSucess.Visible = true;
            //        lblResultSucess.Text = "Great! You’re all done. Your invoice has been sent to the customer.";
            //    }
            //    else
            //    {
            //        //                    LogExceptions.LogException("Callid:" + jobEntity.ID + ", Invoice ref:" + InvoiceRefID + ", Error:", paymentResult);
            //        ////update the call status
            //        FLSDetailsBAL.UpdateTicketStatus(jobEntity.ID, sessionKeys.UID, JobStatus.Cancelled);
            //        pnlResultQuote.Visible = true;
            //       // pnlPaymentDetails.Visible = false;
            //        lblResultFail.Visible = true;
            //        lblResultFail.Text = "Payment process failed, Please try again";

            //        //btnBack.Text = "Try payment again";
            //    }
            //}


          

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


        protected void btAddClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx", false);
        }
    }
}