using DC.BLL;
using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;
using DocumentFormat.OpenXml.Vml;
using Org.BouncyCastle.Asn1.X509;
using PayFast;
using PortfolioMgt.Entity;
using Stripe.Checkout;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakepaymentsGateway;
using TuesPechkin;
using static DC_FLS_ctrl;

namespace DeffinityAppDev.App
{
    public partial class PayProcess : System.Web.UI.Page
    {
        private static string threeDSRef;

        DirectAPIForm payment = new DirectAPIForm();
        PayFastSettings payFastSettings;


        #region Payfast code

        Regex _upperUrlEncodeRegex = new Regex(@"%[a-f0-9]{2}");
        public class PaySettings
        {
            public bool IsLive { set; get; }
            public string MerchantID { set; get; }
            public string MerchantKey { set; get; }
            public string URLReturn { set; get; }
            public string URLCancel { set; get; }
            public string URLNotify { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string Email { set; get; }
            public string CellNumber { set; get; }
            public string OrderId { set; get; }
            public string Amount { set; get; }
            public string ItemName { set; get; }
            public string Description { set; get; }


            public string setup { set; get; }

            public string passphrase { set; get; }

            public string payment_method { set; get; }

            public string recurring { set; get; }

            public string startdate { set; get; }

            public string payref { set; get; }

        }

        public void PaymentProcess_recurring(PaySettings settings,string unid)
        {


            string site = settings.IsLive ? "https://www.payfast.co.za/eng/process?" : "https://sandbox.payfast.co.za/eng/process?";
            string merchant_id = settings.MerchantID;
            string merchant_key = settings.MerchantKey;

            // Build the query string for payment site

            StringBuilder strHashed = new StringBuilder();
            var subscriptionid = unid;// Guid.NewGuid().ToString();
            // strHashed.AppendFormat("passphrase={0}&", UrlEncodeUpper(settings.passphrase));
            strHashed.AppendFormat("merchant_id={0}&", UrlEncodeUpper(merchant_id));
            strHashed.AppendFormat("merchant_key={0}&", UrlEncodeUpper(merchant_key));
            if (!string.IsNullOrEmpty(settings.URLReturn))
               strHashed.AppendFormat("return_url={0}&", UrlEncodeUpper(settings.URLReturn)); // Just thank the user and tell them you are processing their order (should already be done or take a few more seconds with ITN)
            if (!string.IsNullOrEmpty(settings.URLCancel))
                strHashed.AppendFormat("cancel_url={0}&", UrlEncodeUpper(settings.URLCancel)); // Just thank the user and tell them that they cancelled the order (encourage them to email you if they have problems paying
            if (!string.IsNullOrEmpty(settings.URLNotify))
                strHashed.AppendFormat("notify_url={0}&", UrlEncodeUpper(settings.URLNotify+"&TransactionID="+subscriptionid)); // Called once by the payment processor to validate
         
            if (!string.IsNullOrEmpty(settings.Email))
                strHashed.AppendFormat("email_address={0}&", UrlEncodeUpper(settings.Email));
            //if (!string.IsNullOrEmpty(settings.CellNumber))
            //    strHashed.AppendFormat("cell_number={0}&", UrlEncodeUpper(settings.CellNumber));

            //if (!string.IsNullOrEmpty(settings.OrderId))
            //    strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(settings.OrderId));

            strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(subscriptionid));
            strHashed.AppendFormat("amount={0}&", UrlEncodeUpper(settings.Amount.ToString()));
            // strHashed.AppendFormat("item_name={0}&", UrlEncodeUpper(settings.ItemName));
            strHashed.AppendFormat("item_name={0}&", UrlEncodeUpper("Subscription"));

            if (!string.IsNullOrEmpty(settings.payref))
                strHashed.AppendFormat("custom_str1={0}&", UrlEncodeUpper(settings.payref));

            strHashed.AppendFormat("subscription_type={0}&", UrlEncodeUpper("1"));

            var s = settings.startdate;
            if(s !="")
            strHashed.AppendFormat("billing_date={0}&", UrlEncodeUpper(Convert.ToDateTime(s).ToString("yyyy-MM-dd")));
            else
                strHashed.AppendFormat("billing_date={0}&", UrlEncodeUpper(DateTime.Now.ToString("yyyy-MM-dd")));
            //monthly
            var t = settings.recurring;
            if (t != "")
            {
                var v = 0;
                if (t.ToLower().Contains("month"))
                    v = 3;
                else if (t.ToLower().Contains("week"))
                    v = 2;
                else if (t.ToLower().Contains("daily"))
                    v = 1;
                else if (t.ToLower().Contains("quart"))
                    v = 4;
                //else if (t.ToLower().Contains("daily"))
                //    v = 1;

                strHashed.AppendFormat("frequency={0}&", UrlEncodeUpper(v.ToString()));
            }
            strHashed.AppendFormat("cycles={0}&", UrlEncodeUpper("0"));

           // strHashed.AppendFormat("recurring_amount={0}&", UrlEncodeUpper(settings.Amount.ToString()));


            //if (!string.IsNullOrEmpty(settings.Description))
            //    strHashed.AppendFormat("item_description={0}&", UrlEncodeUpper(settings.Description));

            //if (!string.IsNullOrEmpty(settings.payment_method))
            //    strHashed.AppendFormat("payment_method={0}&", UrlEncodeUpper(settings.payment_method));

            var setup = "";
            if (!string.IsNullOrEmpty(settings.setup))
                setup = "&setup=" + settings.setup;
            //  strHashed.AppendFormat("payment_method={0}", UrlEncodeUpper("cc"));

            var temp = $"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim() + setup;

            Response.Redirect($"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim() + setup, false);
        }

        public void PaymentProcess(PaySettings settings,TithingPaymentTracker pE,string unid)
        {
            Response.Redirect("~/PaymentForm.aspx?unid="+pE.ID,false);
           // StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey();
            //StripeConfiguration.ApiKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";
            //var sessionUrl = CreateCheckoutSession(settings,pE);
            //if (!string.IsNullOrEmpty(sessionUrl.Url))
            //{

            //    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            //    var pEntity = pmRep.GetAll().Where(o => o.ID == pE.ID).FirstOrDefault();
            //    if(pEntity !=  null)
            //    {
            //        pEntity.StripeSessionID = sessionUrl.Id;
            //        pmRep.Edit(pEntity);
            //    }
                
            //    // Redirect to Stripe Checkout using the session URL
            //    Response.Redirect(sessionUrl.Url);
            //}
            //else
            //{
            //    // Handle the case where session creation failed
            //    Response.Write("Failed to create a Stripe Checkout session.");
            //}
            //string site = settings.IsLive ? "https://www.payfast.co.za/eng/process?" : "https://sandbox.payfast.co.za/eng/process?";
            //string merchant_id = settings.MerchantID;
            //string merchant_key = settings.MerchantKey;

            //// Build the query string for payment site

            //StringBuilder strHashed = new StringBuilder();

            //// strHashed.AppendFormat("passphrase={0}&", UrlEncodeUpper(settings.passphrase));
            //strHashed.AppendFormat("merchant_id={0}&", UrlEncodeUpper(merchant_id));
            //strHashed.AppendFormat("merchant_key={0}&", UrlEncodeUpper(merchant_key));
            //if (!string.IsNullOrEmpty(settings.URLReturn))
            //    strHashed.AppendFormat("return_url={0}&", UrlEncodeUpper(settings.URLReturn)); // Just thank the user and tell them you are processing their order (should already be done or take a few more seconds with ITN)
            //if (!string.IsNullOrEmpty(settings.URLCancel))
            //    strHashed.AppendFormat("cancel_url={0}&", UrlEncodeUpper(settings.URLCancel)); // Just thank the user and tell them that they cancelled the order (encourage them to email you if they have problems paying
            //if (!string.IsNullOrEmpty(settings.URLNotify))
            //    strHashed.AppendFormat("notify_url={0}&", UrlEncodeUpper(settings.URLNotify)); // Called once by the payment processor to validate

            ////if (!string.IsNullOrEmpty(settings.FirstName))
            ////    strHashed.AppendFormat("name_first={0}&", UrlEncodeUpper(settings.FirstName));
            ////if (!string.IsNullOrEmpty(settings.LastName))
            ////    strHashed.AppendFormat("name_last={0}&", UrlEncodeUpper(settings.LastName));
            //if (!string.IsNullOrEmpty(settings.Email))
            //    strHashed.AppendFormat("email_address={0}&", UrlEncodeUpper(settings.Email));
            ////if (!string.IsNullOrEmpty(settings.CellNumber))
            ////    strHashed.AppendFormat("cell_number={0}&", UrlEncodeUpper(settings.CellNumber));

            //if (!string.IsNullOrEmpty(settings.OrderId))
            //    strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(unid));

            ////var subscriptionid = Guid.NewGuid().ToString();
            ////strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(subscriptionid));
            ////strHashed.AppendFormat("subscription_type={0}&", UrlEncodeUpper("1"));
            ////strHashed.AppendFormat("billing_date={0}&", UrlEncodeUpper(DateTime.Now.ToString("yyyy-MM-dd")));
            //////monthly
            ////strHashed.AppendFormat("frequency={0}&", UrlEncodeUpper("3"));
            ////strHashed.AppendFormat("cycles={0}&", UrlEncodeUpper("0"));

            ////strHashed.AppendFormat("recurring_amount={0}&", UrlEncodeUpper(settings.Amount.ToString()));



            //strHashed.AppendFormat("amount={0}&", UrlEncodeUpper(settings.Amount.ToString()));
            //strHashed.AppendFormat("item_name={0}&", UrlEncodeUpper(settings.ItemName));

            //if (!string.IsNullOrEmpty(settings.Description))
            //    strHashed.AppendFormat("item_description={0}&", UrlEncodeUpper(settings.Description));

            //if (!string.IsNullOrEmpty(settings.payref))
            //    strHashed.AppendFormat("custom_str1={0}&", UrlEncodeUpper(settings.payref));

            //if (!string.IsNullOrEmpty(settings.payment_method))
            //    strHashed.AppendFormat("payment_method={0}&", UrlEncodeUpper(settings.payment_method));

            //var setup = "";
            //if (!string.IsNullOrEmpty(settings.setup))
            //    setup = "&setup=" + settings.setup;
            ////  strHashed.AppendFormat("payment_method={0}", UrlEncodeUpper("cc"));

            //var temp = $"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim() + setup;

            //Response.Redirect($"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim() + setup, false);
        }

        private string GetMd5(string input)
        {
            StringBuilder sb = new StringBuilder();
            var hash = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }

        private string UrlEncodeUpper(string input)
        {
            string value = HttpUtility.UrlEncode(input);
            return _upperUrlEncodeRegex.Replace(value, m => m.Value.ToUpperInvariant());
        }
        protected string UrlEncode(string url)
        {
            Dictionary<string, string> convertPairs = new Dictionary<string, string>() { { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "+" },
            { "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" }, { "+", "%2B" }, { ",", "%2C" },
            { "/", "%2F" }, { ":", "%3A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%40" }, { "[", "%5B" }, { "]", "%5D" } };

            var replaceRegex = new Regex(@"[%!# $&'()*+,/:;=?@\[\]]");
            MatchEvaluator matchEval = match => convertPairs[match.Value];
            string encoded = replaceRegex.Replace(url, matchEval);

            return encoded;
        }

        protected string CreateHash(StringBuilder input, string passPhrase)
        {
            var inputStringBuilder = new StringBuilder(input.ToString());
            if (!string.IsNullOrWhiteSpace(passPhrase))
            {
                inputStringBuilder.Append($"passphrase={this.UrlEncode(passPhrase)}");
            }

            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(inputStringBuilder.ToString());

            var hash = md5.ComputeHash(inputBytes);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if(sessionKeys.UName =="")
                    lblUser.Text = "Guest";
                else
                lblUser.Text = sessionKeys.UName;
                // Session["threeDSRef"] = "";
                // Session["threeDSMethodData"] = "";

                //if (Request.Form.Count > 0)
                //{
                //    LogExceptions.LogException("Page load"+ )
                //    getdata(Request.QueryString["refid"]);

                //    string url = "";
                //    string remoteaddress = "";
                //    string marchantid = "";
                //    int portfolioid = 0;

                //    ProcessUpdate(sessionKeys.PortfolioID, url, remoteaddress, marchantid, Convert.ToDouble(Session["amount"]).ToString(), Session["cardnumber"].ToString(), Session["month"].ToString(), Session["year"].ToString().Substring(Session["year"].ToString().Length - 2),
                //       Session["cvv"].ToString(), Session["customername"].ToString(), Session["customeremail"].ToString(), Session["address"].ToString(), Session["postcode"].ToString());

                //}
                if (Request.QueryString["refid"] != null)
                {
                    if (Request.QueryString["refid"].Length > 0)
                    {




                        getdata(Request.QueryString["refid"]);

                        var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(sessionKeys.PortfolioID);
                        pnlDefault.Visible = true;
                        //if (payDetials == null)
                        //{
                        //    if (payDetials.Vendor == null)
                        //    {
                        //        pnlDefault.Visible = false;
                        //        pnlNoCard.Visible = true;
                        //    }
                        //    else if (payDetials.Vendor.Length == 0)
                        //    {
                        //        pnlDefault.Visible = false;
                        //        pnlNoCard.Visible = true;
                        //    }
                        //    else if (payDetials.Vendor.Length > 0)
                        //    {
                        //        pnlDefault.Visible = true;
                        //        pnlNoCard.Visible = false;
                        //    }


                        //}
                        //else
                        //{
                        //    if (payDetials.Vendor == null)
                        //    {
                        //        pnlDefault.Visible = false;
                        //        pnlNoCard.Visible = true;
                        //    }
                        //    else if (payDetials.Vendor.Length == 0)
                        //    {
                        //        pnlDefault.Visible = false;
                        //        pnlNoCard.Visible = true;
                        //    }
                        //    else if (payDetials.Vendor.Length > 0)
                        //    {
                        //        pnlDefault.Visible = true;
                        //        pnlNoCard.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        pnlDefault.Visible = true;
                        //        pnlNoCard.Visible = false;
                        //    }
                        //}

                        if (pnlDefault.Visible == true)
                        {
                            if (QueryStringValues.Type == "stripe")
                            {
                                var r = StripeWebApp.Methods.BaseClass.CreateCustomercharges(Session["customername"].ToString(), Session["customeremail"].ToString(), Session["cardnumber"].ToString(),
                                    Session["month"].ToString(), Session["year"].ToString(), Session["cvv"].ToString(), Session["amount"].ToString());
                                //  LogExceptions.LogException(r.Email);



                                if (r.Status == "succeeded")
                                {

                                    var id = Session["invoiceref"].ToString();
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.PaidDate = DateTime.Now;
                                        pE.IsPaid = true;
                                        //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE.IsPaid = true;
                                        pE.CResult = "";
                                        pmRep.Edit(pE);

                                        sendThankyouMail(pE.unid);
                                        // Response.Redirect("~/App/TithingProcess.aspx?status=ok&unid=" + pE.unid, false);

                                        Response.Redirect("~/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid, false);
                                    }
                                }
                                else
                                {
                                    LogExceptions.LogException(r.Status);
                                }
                                //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);

                            }
                            else
                            {


                                var id = Session["invoiceref"].ToString();

                                if (Request.QueryString["frm"] == "tickets")
                                {
                                    id = Session["invoiceref"].ToString();



                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();
                                    var pE = pmRep.GetAll().Where(o => o.unid == id).FirstOrDefault();

                                    var pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var AdminPayDetails = pRep.GetAll().FirstOrDefault();
                                    // var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(sessionKeys.PortfolioID);

                                    PaySettings ps = new PaySettings();
                                    ps.MerchantID = payDetials.Vendor;
                                    ps.MerchantKey = payDetials.consumerSecret;
                                    ps.passphrase = payDetials.consumerKey ?? "";

                                    ps.Amount = Convert.ToDouble(Session["amount"].ToString()).ToString();
                                    ps.CellNumber = "";
                                    ps.Description = "Donation";
                                    ps.Email = Session["customeremail"].ToString();
                                    //ps.FirstName = "Demo";

                                    ps.IsLive = payDetials.Host.ToLower().Contains("sandbox") ? false : true;
                                    ps.ItemName = "Donation";
                                    // ps.LastName = "";
                                    ps.OrderId = Request.QueryString["refid"];
                                    ps.URLReturn = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "booking_success";
                                    ps.URLCancel = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "booking_cancel";
                                    ps.URLNotify = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "booking_notify";

                                    if (Session["code"] != null)
                                        ps.payment_method = Session["code"].ToString();


                                    if (Session["payref"] != null)
                                        ps.payref = Session["payref"].ToString();
                                    else
                                        ps.payref = "";

                                    if (AdminPayDetails != null)
                                    {
                                        if (AdminPayDetails.Payment_Merchant_ID != null)
                                        {
                                            var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";

                                            stemp = stemp.Replace("mid", AdminPayDetails.Payment_Merchant_ID);
                                            stemp = stemp.Replace("pcent", (AdminPayDetails.Paymet_Percentage ?? 0).ToString());

                                            var temp1 = "{%22split_payment%22:" + stemp + "}";

                                            ps.setup = temp1;
                                        }
                                        else
                                        {
                                            ps.setup = "";
                                        }
                                    }
                                    else
                                    {
                                        ps.setup = "";
                                    }

                                    if (Session["payref"] != null)
                                        ps.payref = Session["payref"].ToString();
                                    else
                                        ps.payref = "";
                                    // ps.setup = JsonConvert.SerializeObject(sp);
                                    IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var pDetails = prep.GetAll().First();


                                    var str_split = "";

                                    //if (recurring.Length > 0)
                                    //    PaymentProcess_recurring(ps);

                                    //else

                                    //PaymentProcess(ps,pE, pE.unid);

                                }
                                else if (Request.QueryString["frm"] == "online")
                                {
                                    id = Session["invoiceref"].ToString();





                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ProductSaleGuid == id).FirstOrDefault();

                                    var pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var AdminPayDetails = pRep.GetAll().FirstOrDefault();


                                    PaySettings ps = new PaySettings();
                                    ps.MerchantID = payDetials.Vendor;
                                    ps.MerchantKey = payDetials.consumerSecret;
                                    ps.passphrase = payDetials.consumerKey ?? "";

                                    ps.Amount = Convert.ToDouble(Session["amount"].ToString()).ToString();
                                    ps.CellNumber = "";
                                    ps.Description = "Donation";
                                    ps.Email = Session["customeremail"].ToString();
                                    //ps.FirstName = "Demo";

                                    ps.IsLive = payDetials.Host.ToLower().Contains("sandbox") ? false : true;
                                    ps.ItemName = "Donation";
                                    // ps.LastName = "";
                                    ps.OrderId = Request.QueryString["refid"];
                                    ps.URLReturn = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ProductSaleGuid + "&unid=" + pE.ProductSaleGuid + "&type=" + "online_success";
                                    ps.URLCancel = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ProductSaleGuid + "&unid=" + pE.ProductSaleGuid + "&type=" + "online_cancel";
                                    ps.URLNotify = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ProductSaleGuid + "&unid=" + pE.ProductSaleGuid + "&type=" + "online_notify";


                                    if (Session["code"] != null)
                                        ps.payment_method = Session["code"].ToString();

                                    if (Session["payref"] != null)
                                        ps.payref = Session["payref"].ToString();
                                    else
                                        ps.payref = "";

                                    if (AdminPayDetails != null)
                                    {
                                        if (AdminPayDetails.Payment_Merchant_ID != null)
                                        {
                                            var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";

                                            stemp = stemp.Replace("mid", AdminPayDetails.Payment_Merchant_ID);
                                            stemp = stemp.Replace("pcent", (AdminPayDetails.Paymet_Percentage ?? 0).ToString());

                                            var temp1 = "{%22split_payment%22:" + stemp + "}";

                                            ps.setup = temp1;
                                        }
                                        else
                                        {
                                            ps.setup = "";
                                        }
                                    }
                                    else
                                    {
                                        ps.setup = "";
                                    }
                                    // ps.setup = JsonConvert.SerializeObject(sp);
                                    IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var pDetails = prep.GetAll().First();


                                    var str_split = "";


                                //    PaymentProcess(ps, pE, pE.ProductSaleGuid);





                                }
                                else if (Request.QueryString["frm"] == "sms")
                                {
                                    id = Session["invoiceref"].ToString();



                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                                    var pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var AdminPayDetails = pRep.GetAll().FirstOrDefault();
                                    // var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(sessionKeys.PortfolioID);

                                    PaySettings ps = new PaySettings();
                                    //ps.MerchantID = payDetials.Vendor;
                                    //ps.MerchantKey = payDetials.consumerSecret;
                                    //ps.passphrase = payDetials.consumerKey ?? "";
                                    ps.MerchantID = AdminPayDetails.Payment_Merchant_ID;
                                    ps.MerchantKey = AdminPayDetails.Payment_Merchant_key;
                                    ps.passphrase = AdminPayDetails.Payment_Salt_Pass ?? "";

                                    ps.Amount = Convert.ToDouble(Session["amount"].ToString()).ToString();
                                    ps.CellNumber = "";
                                    ps.Description = "SMS Package";
                                    ps.Email = Session["customeremail"].ToString();
                                    //ps.FirstName = "Demo";

                                    ps.IsLive = payDetials.Host.ToLower().Contains("sandbox") ? false : true;
                                    ps.ItemName = "SMS Package";
                                    // ps.LastName = "";
                                    ps.OrderId = Request.QueryString["refid"];
                                    ps.URLReturn = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ID + "&unid=" + pE.ID + "&type=" + "sms_success";
                                    ps.URLCancel = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ID + "&unid=" + pE.ID + "&type=" + "sms_cancel";
                                    ps.URLNotify = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.ID + "&unid=" + pE.ID + "&type=" + "sms_notify";


                                    if (Session["code"] != null)
                                        ps.payment_method = Session["code"].ToString();

                                    if (Session["payref"] != null)
                                        ps.payref = Session["payref"].ToString();
                                    else
                                        ps.payref = "";


                                    IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var pDetails = prep.GetAll().First();


                                    var str_split = "";


                                   // PaymentProcess(ps, pE, pE.ID.ToString());








                                }
                                else
                                {

                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                                    var pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    var AdminPayDetails = pRep.GetAll().FirstOrDefault();
                                    // var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(sessionKeys.PortfolioID);

                                    var amt_withoutfee = 0.00;
                                    if (Session["amount_withoutfee"] != null)
                                    {
                                        amt_withoutfee = Convert.ToDouble(Session["amount_withoutfee"].ToString());


                                    }

                                    //string recurring = "";
                                    //if (Session["recurring"] != null)
                                    //    recurring = Session["recurring"].ToString();

                                    //string startdate = "";
                                    //if(Session["startdate"] != null)
                                    //    startdate = Session["startdate"].ToString();

                                    PaySettings ps = new PaySettings();
                                    ps.MerchantID = payDetials.Vendor;
                                    ps.MerchantKey = payDetials.consumerSecret;
                                    ps.passphrase = payDetials.consumerKey ?? "";

                                    ps.Amount = Convert.ToDouble(Session["amount"].ToString()).ToString();
                                    ps.CellNumber = "";
                                    ps.Description = "Donation";
                                    ps.Email = Session["customeremail"].ToString();
                                    //ps.FirstName = "Demo";

                                    ps.IsLive = payDetials.Host.ToLower().Contains("sandbox") ? false : true;
                                    ps.ItemName = "Donation";
                                    // ps.LastName = "";
                                    ps.OrderId = Request.QueryString["refid"];
                                    ps.URLReturn = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "success";
                                    ps.URLCancel = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "cancel";
                                    ps.URLNotify = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "notify";

                                    if (Session["code"] != null)
                                        ps.payment_method = Session["code"].ToString();

                                    if (Session["payref"] != null)
                                        ps.payref = Session["payref"].ToString();
                                    else
                                        ps.payref = "";


                                    if (AdminPayDetails != null)
                                    {
                                        if (AdminPayDetails.Payment_Merchant_ID != null)
                                        {
                                            // var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";
                                            //var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";
                                            //stemp = stemp.Replace("mid", AdminPayDetails.Payment_Merchant_ID);
                                            //stemp = stemp.Replace("pcent", (AdminPayDetails.Paymet_Percentage??0).ToString());

                                            //var temp1 = "{%22split_payment%22:" + stemp + "}";



                                            //if (amt_withoutfee > 0)

                                            //{
                                            //    if ((payDetials.TransactionFee ?? 0) > 0)
                                            //    {
                                            //        //convet to centes
                                            //        var pamount = Convert.ToInt32(CalculatePercentage(amt_withoutfee, (payDetials.TransactionFee ?? 0)) * 100);


                                            //        var stemp = "{%22merchant_id%22:mid,%22amount%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";
                                            //        stemp = stemp.Replace("mid", AdminPayDetails.Payment_Merchant_ID);


                                            //        stemp = stemp.Replace("pcent", (pamount).ToString());

                                            //        var temp1 = "{%22split_payment%22:" + stemp + "}";

                                            //        ps.setup = temp1;
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    // var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";
                                            //    var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";
                                            //    stemp = stemp.Replace("mid", AdminPayDetails.Payment_Merchant_ID);
                                            //    stemp = stemp.Replace("pcent", (AdminPayDetails.Paymet_Percentage ?? 0).ToString());

                                            //    var temp1 = "{%22split_payment%22:" + stemp + "}";

                                            //    ps.setup = temp1;

                                            //}
                                        }
                                        else
                                        {
                                            ps.setup = "";
                                        }
                                    }
                                    else
                                    {
                                        ps.setup = "";
                                    }
                                    // ps.setup = JsonConvert.SerializeObject(sp);
                                    //IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                    //var pDetails = prep.GetAll().First();


                                    var str_split = "";

                                    string recurring = "";
                                    if (Session["recurring"] != null)
                                        recurring = Session["recurring"].ToString();

                                    string startdate = "";
                                    if (Session["startdate"] != null)
                                        startdate = Session["startdate"].ToString();

                                    if (recurring.Length > 0)
                                    {

                                        ps.recurring = recurring;
                                        ps.startdate = startdate;

                                        PaymentProcess_recurring(ps, pE.unid);
                                    }

                                    else

                                        PaymentProcess(ps, pE, pE.unid);


                                }

                                //string url = "";
                                //string remoteaddress = "";
                                //string marchantid = "";

                                //ProcessUpdate(sessionKeys.PortfolioID, url, remoteaddress, marchantid,
                                //    Convert.ToDouble(Session["amount"]).ToString(), Session["cardnumber"].ToString(), Session["month"].ToString(), Session["year"].ToString().Substring(Session["year"].ToString().Length - 2),
                                //   Session["cvv"].ToString(), Session["customername"].ToString(), Session["customeremail"].ToString(), Session["address"].ToString(), Session["postcode"].ToString());
                            }
                        }

                    }

                }
                else
                {
                    pnlDefault.Visible = false;
                    pnlNoCard.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region stripe

        private Session CreateCheckoutSession(PaySettings ps, TithingPaymentTracker pE)
        {
            StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey(); //"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";
            //IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
            //var pDetails = prep.GetAll().First();
            //get plegit mail 

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long?)((pE.PaidAmount??0) * 100), // Amount in cents (e.g., 100.00 USD)
                            Currency = "gbp",
                           // Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Service Charge",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                //SuccessUrl = Deffinity.systemdefaults.GetWebUrl() + "/stripe_success.aspx?session_id={CHECKOUT_SESSION_ID}", // Replace with your success URL
                //CancelUrl = Deffinity.systemdefaults.GetWebUrl() + "/stripe_cancel.aspx", // Replace with your cancel URL

                SuccessUrl = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?session_id={CHECKOUT_SESSION_ID}&tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "success", // Replace with your success URL
                CancelUrl = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid + "&type=" + "cancel", // Replace with your cancel URL
                Metadata = new Dictionary<string, string>
                {
                    { "order_id", "6735" } // Example metadata
                }
            };


            var service = new SessionService();
            try
            {
                Session session = service.Create(options);

                LogExceptions.LogException("sessionid:" + session.Id);
                return session;
            }
            catch (StripeException ex)
            {

                LogExceptions.WriteExceptionLog(ex);
                // Handle exception
                Console.WriteLine(ex.Message);
                Response.Write(ex.Message);
                return null;
            }


        }
        #endregion

        static double CalculatePercentage(double number, double percentage)
        {
            return (number * percentage) / 100;
        }
        public void formSessions(string url, string field1, string field1value, string field2, string field2value)
        {
            Session["actionurl"] = url;
            Session["field1"] = field1;

            Session["field1value"] = field1value;
            Session["field2"] = field2;

            Session["field2value"] = field2value;
        }
        private string ProcessResponseFields(Dictionary<string, string> responseFields)
        {
            LogExceptions.LogException(responseFields["responseCode"]);
            switch (responseFields["responseCode"])
            {
                case "65802":
                    threeDSRef = responseFields["threeDSRef"];
                    Session["ThreeDSecure"] = true;
                    Session["threeDSRef"] = threeDSRef;
                    return ShowFrameForThreeDS(responseFields, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                case "0":
                    lbloutput.Text = "Payment Successful " ;
                    if (Request.QueryString["frm"] != null)
                    {
                        if (Request.QueryString["frm"] == "donation")
                        {
                            // getdata(invoiceref);

                            if (responseFields["responseCode"] == "0")
                            {
                                getdata(Request.QueryString["refid"]);
                                try
                                {
                                   // LogExceptions.LogException("Fund Session['responseCode']" + payment.responseCode.ToString());
                                    //LogExceptions.LogException("Session['responseMessage']" + payment.responseMessage.ToString());


                                    LogExceptions.LogException("Session['invoiceref']" + Session["invoiceref"].ToString());
                                    var id = Session["invoiceref"].ToString();
                                    //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.PaidDate = DateTime.Now;
                                        pE.IsPaid = true;
                                        //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE.IsPaid = true;
                                        pE.CResult = "";
                                        pmRep.Edit(pE);

                                        sendThankyouMail(pE.unid);
                                        // Response.Redirect("~/App/TithingProcess.aspx?status=ok&unid=" + pE.unid, false);

                                        Response.Redirect("~/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid, false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }

                            }
                            else
                            {
                                getdata(Request.QueryString["refid"]);
                                //App/TithingProcess.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);
                                try
                                {
                                    var id = Session["invoiceref"].ToString();
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.CResult = responseFields["responseCode"] + " " + responseFields["responseMessage"].ToString();

                                        pmRep.Edit(pE);
                                    }

                                    Response.Redirect("~/PayResult.aspx?unid=" + pE.unid, false);
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                    Response.Redirect("~/PayResult.aspx?unid=" + "", false);
                                }


                                // Response.Redirect("~/App/TithingProcess.aspx?status=fail&unid=" + invoiceref, false);
                            }
                        }
                      else  if (Request.QueryString["frm"] == "fund")
                        {
                            getdata(Request.QueryString["refid"]);
                            if (responseFields["responseCode"] == "0")
                            {
                                try
                                {


                                    LogExceptions.LogException("Session['invoiceref']" + Session["invoiceref"].ToString());
                                    var id = Session["invoiceref"].ToString();
                                    //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.PaidDate = DateTime.Now;
                                        pE.IsPaid = true;
                                        //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE.IsPaid = true;
                                        pE.CResult = "";
                                        pmRep.Edit(pE);

                                        sendThankyouMail(pE.unid);
                                        // Response.Redirect("~/App/Donations.aspx?status=ok&unid=" + pE.unid, false);

                                        Response.Redirect("~/FundResult.aspx?tunid=" + pE.FundriserUNID + "&unid=" + pE.unid, false);
                                    }

                                    //var id = Session["invoiceref"].ToString();
                                    ////Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);
                                    //var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    //var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    //if (pE != null)
                                    //{
                                    //    pE.PaidDate = DateTime.Now;
                                    //    pE.IsPaid = true;
                                    //    //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                    //    // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                    //    // pE.IsPaid = true;
                                    //    pE.CResult = "";
                                    //    pmRep.Edit(pE);

                                    //    sendThankyouMail(pE.unid);
                                    //    // Response.Redirect("~/App/Donations.aspx?status=ok&unid=" + pE.unid, false);
                                    //    Response.Redirect("~/FundResult.aspx?tunid=" + pE.FundriserUNID + "&unid=" + pE.unid, false);
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }

                            }
                            else
                            {
                                //App/Donations.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);

                                // Response.Redirect("~/App/Donations.aspx?status=fail&unid=" + invoiceref, false);
                                getdata(Request.QueryString["refid"]);
                                //App/Donations.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);
                                try
                                {
                                    var id = Session["invoiceref"].ToString();
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.CResult = responseFields["responseCode"]  + " - " + responseFields["responseMessage"].ToString();

                                        pmRep.Edit(pE);
                                    }

                                    Response.Redirect("~/FundResult.aspx?tunid=" + pE.FundriserUNID + "&unid=" + pE.unid, false);
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                    Response.Redirect("~/FundResult.aspx?tunid=" + "" + "&unid=" + "", false);
                                }

                            }
                        }
                        else
                        {
                            if (QueryStringValues.Type.Length > 0)
                            {
                                //if (QueryStringValues.Type == "mail")
                                //{
                                //    Response.Redirect("~/WF/payinvoice/PaymentProcess.aspx?uqid=" + uqid, false);
                                //}
                                //else
                                //    Response.Redirect("~/WF/CustomerAdmin/ProcessPayment.aspx", false);
                            }
                            //else
                            //    Response.Redirect("~/PayResult.aspx", false);
                        }
                    }
                    else
                        Response.Redirect("~/PayResult.aspx", false);
                    return responseFields["orderRef"] + " - Payment Successful - " + responseFields["responseMessage"];
                default:
                    lbloutput.Text = "Failed to take payment: " + responseFields["responseMessage"];
                    return "";
            }
        }
        private string SilentPost(string url, Dictionary<string, string> fields, string target = "_self")
        {
            //Session["threeDSURL"] = url;
            //Session["target"] = target;
            //Session["devUrl"] = url;
            var rtn = new StringBuilder($@"<form id=""silentPost"" action=""{url}"" method=""post"" target=""{target}"">");

            foreach (var f in fields)
            {
                rtn.AppendLine($@"<input type=""hidden"" name=""{f.Key}"" value=""{f.Value}"" /> ");
            }

            rtn.AppendLine(@"
                <noscript><input type=""submit"" value=""Continue""></noscript>
                </form >
                <script >
                            window.setTimeout('document.forms.silentPost.submit()', 5000);
                </script > ");

            return rtn.ToString();
        }

        private string ShowFrameForThreeDS(Dictionary<string, string> fieldsFromServer, string realUrl)
        {

            //Send request to the ACS server displaying response in an IFRAME
            //Render an IFRAME to show the ACS challenge (hidden for fingerprint method)

            var style = fieldsFromServer.Keys.Contains("threeDSRequest[threeDSMethodData]") ? "display: none;" : "";
           // var style = fieldsFromServer.Keys.Contains("threeDSRequest[threeDSMethodData]") ? "" : "";

            
            //rtn = f'<iframe name="threeds_acs" style="height:420px; width:420px; {style}"></iframe>\n\n'

            var formFields = new Dictionary<string, string>();

            formFields.Add("devUrl", realUrl);

            //Session["devUrl"] = realUrl;

            foreach (var field in fieldsFromServer)
            {
                if (field.Key.StartsWith("threeDSRequest["))
                {
                    formFields.Add(field.Key.Substring(15, field.Key.Length - 16), field.Value);
                    Session[field.Key.Substring(15, field.Key.Length - 16)] = field.Value;
                }
            }
            //secondtime post
            if (fieldsFromServer.Keys.Contains("threeDSRequest[threeDSMethodData]"))
            {
              //  formpo

            }
            else
            {

            }

            // Session["threeds_acs"] = fieldsFromServer["threeds_acs"];

            return $"<iframe name=\"threeds_acs\" style=\"height: 420px; width: 420px;border:0; {style}\"></iframe>" +
                SilentPost(fieldsFromServer["threeDSURL"], formFields, "threeds_acs");

        }
        public string ProcessUpdate(int portfolioid, string url, string remoteAddress, string marchantid, string amount, string cardNumber, string cardExpiryMM, string cardExpiryYY, string cardCVV, string customerName,
           string customerEmail, string customerAddress, string customerPostcode)
        {
            img.Visible = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolioid);
            string retval = string.Empty;
            remoteAddress = string.Empty;
            var gateway = new TakepaymentsGateway.TakepaymentsGateway(payDetials.Vendor, payDetials.consumerSecret);

            //var retval = gateway.CollectBrowserInfo(null, GetEnvironmentData()); 


            var req = System.Web.HttpContext.Current.Request;

            // After the customer as responded to the ACS server, it returns the user
            // to the URL we provide, which includes acs=1 as a GET variable. 
            // This code renames all the submitted form inputs, and also breaks out of the frame. 
            // (In dynamic languages, this is effectively `responseCode = POST`. In C# we can 
            // use string manipulation instead. 
            if (req.QueryString.AllKeys.Contains("acs"))
            {
                
                var code = "";
                getdata(Request.QueryString["refid"]);
                var fields = new Dictionary<string, string>();
                // string retval = "";
                LogExceptions.LogException("Form keys count: " + req.Form.AllKeys.Length);
                foreach (var formItem in req.Form.AllKeys)
                {
                    fields.Add("threeDSResponse[" + formItem + "]", req.Form[formItem]);

                    retval = retval + "threeDSResponse[" + formItem + "]" + req.Form[formItem];
                    Session[formItem] = req.Form[formItem];
                }
                //lbloutput.Text = retval;
                Session["ThreeDSecure"] = true;
               // lblMessage.Text = "Processing 3DSv2 response";

                retval = SilentPost(req.Url.AbsoluteUri.Replace("acs=1", ""), fields, "_parent");
                lblsubform.Text = retval;
                Session["threeDSURL"] = req.Url.AbsoluteUri.Replace("acs=1", "");
                //lblsubform.Text = retval;

                Session["GatewayHtml"] = "";
                //Session["threeDSMethodData"] = null;
                // return View();
              //  pnlFirst.Visible = true;



               //// getdata(Request.QueryString["refid"]);
               //  fields = new Dictionary<string, string>
               //     {
               //         { "action", "SALE" }
               //     };

               // foreach (string item in req.Form)
               // {
               //     if (item.StartsWith("threeDSResponse"))
               //     {
               //         fields.Add(item, req.Form[item]);
               //     }
               // }
               // // threeDSResponse[threeDSMethodData]
               // fields.Add("threeDSResponse[threeDSMethodData]", Session["threeDSMethodData"].ToString());
               // fields.Add("threeDSRef", threeDSRef);
                

               // // Send the threeDSresponse to the server, along with threeDSRef previously stored. 
               // // The request will generally look like:
               // // threeDSResponse[AnyKeyFromACS] => value from the ACS
               // // threeDSRef => threeDSRef previously sent by the gateway. 
               // // action => SALE
               // // MerchantID => 100856
               // // ViewBag.Message = "Transaction response";
               // lblMessage.Text = "Transaction response";
               // //payDetials.Vendor, payDetials.consumerSecret,
               // retval = ProcessResponseFields(gateway.DirectRequest(fields,  null));

               

            }
            else
            {

                if (AnyKeyStartsWith(req.Form, "threeDSResponse"))
                {

                    img.Visible = false;
                    //pnlsecond.Visible = true;
                    getdata(Request.QueryString["refid"]);
                    var fields = new Dictionary<string, string>
                    {
                        { "action", "SALE" }
                    };

                    foreach (string item in req.Form)
                    {
                        if (item.StartsWith("threeDSResponse"))
                        {
                            fields.Add(item, req.Form[item]);
                        }
                    }
                    // threeDSResponse[threeDSMethodData]
                    fields.Add("threeDSRef", threeDSRef);
                   // fields.Add("threeDSRef", Session["threeDSMethodData"].ToString());

                    // Send the threeDSresponse to the server, along with threeDSRef previously stored. 
                    // The request will generally look like:
                    // threeDSResponse[AnyKeyFromACS] => value from the ACS
                    // threeDSRef => threeDSRef previously sent by the gateway. 
                    // action => SALE
                    // MerchantID => 100856
                    // ViewBag.Message = "Transaction response";
                    lblMessage.Text = "Transaction response";
                    retval = ProcessResponseFields(gateway.DirectRequest(fields ,null));
                    lblsubform.Text = retval;
                    // lblsubform.Text = retval;
                    //  Session["GatewayHtml"] = retval;
                }
                else // We don't have a threeDSResponse, but this is a POST request. 
                {    // This is the first request send to the Gateway, which may then request the user
                     // be redirected to the ACS server. 
                     // var q = gateway.CollectBrowserInfo(null, GetEnvironmentData());

                    remoteAddress = req.UserHostAddress;

                    if (remoteAddress == "::1")
                    {
                        // In development environments we often see the IPv6 localhost address.
                        // The Gateway requires an IPv4 address. 
                        remoteAddress = "8.8.8.8";
                        //remoteAddress = "192.168.29.23";
                    }

                    // Please note that this value MUST be HTTPS. 
                    // In development, this may be achieved using Apache as a reverse proxy and 
                    // changing this value. 
                    var thisUrl = req.Url.AbsoluteUri;

                    //var fields = new Dictionary<string, string>
                    //{
                    //    { "action", "SALE" }
                    //};

                    //foreach (string item in req.Form)
                    //{

                    //    fields.Add(item, req.Form[item]);

                    //}

                    // ViewBag.GatewayHtml = gateway.CollectBrowserInfo(null, GetEnvironmentData());

                    var requestFields = GetInitialForm(thisUrl, remoteAddress, payDetials.Vendor, amount, cardNumber, cardExpiryMM, cardExpiryYY, cardCVV, customerName, customerEmail, customerAddress, customerPostcode);

                    foreach (string item in req.Form)
                    {
                        if (item.StartsWith("browserInfo["))
                        {
                            // remove the browserInfo[ and the trailing ]
                            requestFields.Add(item.Substring(12, item.Length - 13), req.Form[item]);
                        }

                    }

                    if (!requestFields.ContainsKey("deviceChanne"))
                        requestFields.Add("deviceChanne", "browser");
                    if (!requestFields.ContainsKey("deviceIdentity"))
                        requestFields.Add("deviceIdentity", System.Web.HttpContext.Current.Request.UserAgent);
                    if (!requestFields.ContainsKey("deviceTimeZone"))
                        requestFields.Add("deviceTimeZone", "0");
                    if (!requestFields.ContainsKey("deviceCapabilities"))
                        requestFields.Add("deviceCapabilities", "");
                    if (!requestFields.ContainsKey("deviceScreenResolution"))
                        requestFields.Add("deviceScreenResolution", "1x1x1");
                    if (!requestFields.ContainsKey("deviceAcceptContent"))
                        requestFields.Add("deviceAcceptContent", String.Join(",", System.Web.HttpContext.Current.Request.AcceptTypes));
                    if (!requestFields.ContainsKey("deviceAcceptEncoding"))
                        requestFields.Add("deviceAcceptEncoding", System.Web.HttpContext.Current.Request.Headers.GetValues("Accept-Encoding")[0]);
                    if (!requestFields.ContainsKey("deviceAcceptLanguage"))
                        requestFields.Add("deviceAcceptLanguage", System.Web.HttpContext.Current.Request.Headers.GetValues("Accept-Language")[0]);
                    //ViewBag.Message = "";
                    // gateway.CollectBrowserInfo(null,GetEnvironmentData());
                    retval = ProcessResponseFields(gateway.DirectRequest(requestFields,  null));

                    lblsubform.Text = retval;
                    // pnl1.Controls.Add(new LiteralControl(retval));
                    //  lblsubform.Text = retval;
                    //Session["GatewayHtml"] = retval;
                }
            }

            return retval;

        }


        private void getdata(string refid)
        {

            var rep = new PortfolioRepository<PortfolioMgt.Entity.ReferenceData>();
            var refdata = rep.GetAll().Where(o => o.refid == refid).FirstOrDefault();
            if (refdata != null)
            {
                var spdata = refdata.refdata;

                var sdata = spdata.Split(';');

                foreach (string s in sdata)
                {
                    if (s.Length > 0)
                    {
                        var d = s.Split(':');
                        if (d.First().Length > 1)
                        {
                            if (d[0].ToString() == "uid")
                            {
                                if (d.Last().Length > 0)
                                    sessionKeys.UID = Convert.ToInt32(d.Last());
                            }
                            else if (d[0].ToString() == "portfolioid")
                            {
                                if (d.Last().Length > 0)
                                    sessionKeys.PortfolioID = Convert.ToInt32(d.Last());
                            }
                            else if (d[0].ToString() == "uname")
                            {
                                if (d.Last().Length > 0)
                                    sessionKeys.UName = d[1].ToString();
                            }
                            else if (d[0].ToString() == "partnerid")
                            {
                                if (d.Last().Length > 0)
                                    sessionKeys.PartnerID = Convert.ToInt32(d.Last());
                            }
                            else
                            {
                                Session[d.First()] = d.Last();
                            }
                        }

                    }
                }



            }



        }
        private PortfolioMgt.Entity.ReferenceData postdata()
        {

            IPortfolioRepository<PortfolioMgt.Entity.ReferenceData> rep = new PortfolioRepository<PortfolioMgt.Entity.ReferenceData>();

            var p = new PortfolioMgt.Entity.ReferenceData();

            string setval = string.Empty;

            setval = setval + string.Format("amount:{0};", Session["amount"]);
            setval = setval + string.Format("invoiceref:{0};", Session["invoiceref"]);
            setval = setval + string.Format("cardnumber:{0};", Session["cardnumber"]);
            setval = setval + string.Format("cvv:{0};", Session["cvv"]);
            setval = setval + string.Format("month:{0};", Session["month"]);
            setval = setval + string.Format("year:{0};", Session["year"]);
            setval = setval + string.Format("customername:{0};", Session["customername"]);
            setval = setval + string.Format("customeremail:{0};", Session["customeremail"]);
            setval = setval + string.Format("address:{0};", Session["address"]);
            setval = setval + string.Format("postcode:{0};", Session["postcode"]);
            setval = setval + string.Format("phone:{0};", Session["phone"]);
            setval = setval + string.Format("customerref:{0};", Session["customerref"]);

            setval = setval + string.Format("uid:{0};", sessionKeys.UID);
            setval = setval + string.Format("portfolioid:{0};", sessionKeys.PortfolioID);
            setval = setval + string.Format("uname:{0};", sessionKeys.UName);
            setval = setval + string.Format("partnerid:{0};", sessionKeys.PartnerID);

            p.refdata = setval;
            p.refid = Guid.NewGuid().ToString();

            rep.Add(p);

            return p;

            //Session["amount"] = amount;
            //Session["invoiceref"] = invoiceid;
            //Session["cardnumber"] = ccnumber;
            //Session["cvv"] = cvv;
            //Session["month"] = ddlMonth.SelectedValue;
            //Session["year"] = year_expiry;
            //Session["customername"] = contactname;
            //Session["address"] = contactAddress.Address;
            //Session["postcode"] = contactAddress.PostCode;
            //Session["phone"] = contactAddress.PortfolioContact.Telephone;
            //Session["customerref"] = retval.PayID;



        }

        private void PaymentProcess(int portfolioid, double amount, string invoiceref, string cardnumber, string cvv, string month, string year, string customername, string address, string postcode, string customeremail, string phone, string customerref, string refid, string uqid)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolioid);

                LogExceptions.LogException(" payDetials.Vendor :" + payDetials.Vendor);
                LogExceptions.LogException(" payDetials.consumerKey :" + payDetials.consumerKey);
                Random r = new Random();
                //int ExampleAmount = r.Next(101, 4999);
                string OrderRef = "Purchase -" + invoiceref;
                string TransactionUnique = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
                string ExampleCardNumber = cardnumber;
                string ExampleCVV = cvv;
                string ExampleCardExpiryMM = month;
                string ExampleCardExpiryYY = year.Length == 4 ? year.Substring(2, 2) : year;
                string ExampleCustomerName = customername;
                string ExampleCustomerAddress = address;
                string ExampleCustomerPostcode = postcode;
                string ExampleCustomerEmail = customeremail;
                string ExampleCustomerPhone = phone;
                string ExampleMerchantCustomerRef = customerref;
                int ExampleCountryCode = 826;


                // payment.Amount = 1100f;
                //payment.cardTypeCode
                
                payment.Amount = float.Parse(String.Format("{0:F2}", amount).Replace(".", ""), CultureInfo.InvariantCulture);
                //payment. // ExampleAmount;
                payment.TransactionUnique = TransactionUnique;
                payment.OrderRef = OrderRef;
                payment.CardNumber = ExampleCardNumber;
                payment.CardCVV = ExampleCVV;
                payment.CardExpiryMM = ExampleCardExpiryMM;
                payment.CardExpiryYY = ExampleCardExpiryYY;
                payment.CustomerName = ExampleCustomerName;
                payment.CustomerAddress = ExampleCustomerAddress;
                payment.CustomerPostcode = ExampleCustomerPostcode;
                payment.CustomerEmail = ExampleCustomerEmail;
                payment.CustomerPhone = ExampleCustomerPhone;
                // payment.merchantCustomerReference = ExampleMerchantCustomerRef;
                payment.CountryCode = ExampleCountryCode;
                payment.MerchantID = payDetials.Vendor;
                payment.MerchantPassword = payDetials.Password;
                payment.MerchantKey = payDetials.consumerSecret;
                payment.MerchantCustomerRef = ExampleMerchantCustomerRef;
                if (Request.Form["PaRes"] == null)
                {
                    payment.Signature = payment.SignTransaction();

                }
                else
                {
                    payment.Signature = payment.SignThreeDTransaction();
                }

                var retval = payment.SendPayment();

                var response = payment.responseCode;
                if (payment.responseCode == 65802)
                {

                    LogExceptions.LogException("65802 Session['responseCode']" + payment.responseCode.ToString());
                    LogExceptions.LogException("Session['responseMessage']" + payment.responseMessage.ToString());

                    Session["responseCode"] = payment.responseCode;
                    Session["PageUrl"] = payment.PageUrl + "&retval=1&frm=";// + "?refid=" + refid;
                    Session["ThreeDSMD"] = payment.ThreeDSMD;
                    Session["ThreeDSPaReq"] = payment.ThreeDSPaReq;
                    Session["TermUrl"] = payment.PageUrl + "&retval=1";// "?refid="+refid;
                    Session["ThreeDSACSURL"] = payment.ThreeDSACSURL;
                    // lblResult.Text = htmlString();
                    //Response.Redirect("paynext.aspx", false);
                }
                else
                {

                    LogExceptions.LogException("Session['responseCode']"+ payment.responseCode.ToString());
                    LogExceptions.LogException("Session['responseMessage']" + payment.responseMessage.ToString());

                   // sessionKeys.Message = payment.responseCode.ToString() + " " + payment.responseMessage;
                    Session["responseCode"] = payment.responseCode;
                    Session["responseMessage"] = payment.responseMessage;

                    if (payment.responseCode == 0)
                    {
                        try
                        {
                            //QuickPayBAL.UpdatePaySucessStatus(Convert.ToInt32(customerref), TransactionUnique, refid, Convert.ToInt32(invoiceref));
                            //QuickPayBAL.SendPaymentSucessMails(Convert.ToInt32(customerref), Convert.ToInt32(invoiceref));
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                    }
                    if (Request.QueryString["frm"] != null)
                    {
                        if (Request.QueryString["frm"] == "donation")
                        {
                            // getdata(invoiceref);
                           
                            if (payment.responseCode == 0)
                            {
                                getdata(refid);
                                try
                                {
                                    LogExceptions.LogException("Fund Session['responseCode']" + payment.responseCode.ToString());
                                    LogExceptions.LogException("Session['responseMessage']" + payment.responseMessage.ToString());

                                   
                                    LogExceptions.LogException("Session['invoiceref']" + Session["invoiceref"].ToString());
                                    var id = Session["invoiceref"].ToString();
                                    //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.PaidDate = DateTime.Now;
                                        pE.IsPaid = true;
                                        //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE.IsPaid = true;
                                        pE.CResult = "";
                                        pmRep.Edit(pE);

                                        sendThankyouMail(pE.unid);
                                        // Response.Redirect("~/App/TithingProcess.aspx?status=ok&unid=" + pE.unid, false);

                                        Response.Redirect("~/PayResult.aspx?tunid=" + pE.unid + "&unid=" + pE.unid, false);
                                    }
                                }
                                catch(Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }
                               
                            }
                            else
                            {
                                getdata(refid);
                                //App/TithingProcess.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);
                                try
                                {
                                    var id = Session["invoiceref"].ToString();
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.CResult = payment.responseCode.ToString() + " " + payment.responseMessage.ToString();

                                        pmRep.Edit(pE);
                                    }

                                    Response.Redirect("~/PayResult.aspx?unid=" + pE.unid, false);
                                }
                                catch(Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                    Response.Redirect("~/PayResult.aspx?unid=" +"", false);
                                }

                               
                                // Response.Redirect("~/App/TithingProcess.aspx?status=fail&unid=" + invoiceref, false);
                            }
                        }
                        if (Request.QueryString["frm"] == "fund")
                        {
                            getdata(refid);
                            if (payment.responseCode == 0)
                            {
                                try
                                {


                                    var id = Session["invoiceref"].ToString();
                                    //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.PaidDate = DateTime.Now;
                                        pE.IsPaid = true;
                                        //pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                                        // pE.IsPaid = true;
                                        pE.CResult = "";
                                        pmRep.Edit(pE);

                                        sendThankyouMail(pE.unid);
                                        // Response.Redirect("~/App/TithingProcess.aspx?status=ok&unid=" + pE.unid, false);
                                        Response.Redirect("~/FundResult.aspx?tunid=" + pE.FundriserUNID + "&unid=" + pE.unid, false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }

                            }
                            else
                            {
                                //App/TithingProcess.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);

                                // Response.Redirect("~/App/TithingProcess.aspx?status=fail&unid=" + invoiceref, false);
                                getdata(refid);
                                //App/TithingProcess.aspx
                                // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);
                                try
                                {
                                    var id = Session["invoiceref"].ToString();
                                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                                    var pE = pmRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                                    if (pE != null)
                                    {
                                        pE.CResult = payment.responseCode.ToString() + " - " + payment.responseMessage.ToString();

                                        pmRep.Edit(pE);
                                    }

                                    Response.Redirect("~/FundResult.aspx?tunid=" + pE.FundriserUNID + "&unid=" + pE.unid, false);
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                    Response.Redirect("~/FundResult.aspx?tunid=" + "" + "&unid=" + "", false);
                                }

                            }
                        }
                        else
                        {
                            if (QueryStringValues.Type.Length > 0)
                            {
                                if (QueryStringValues.Type == "mail")
                                {
                                    Response.Redirect("~/WF/payinvoice/PaymentProcess.aspx?uqid=" + uqid, false);
                                }
                                else
                                    Response.Redirect("~/WF/CustomerAdmin/ProcessPayment.aspx", false);
                            }
                            //else
                            //    Response.Redirect("~/PayResult.aspx", false);
                        }
                    }
                    else
                        Response.Redirect("~/PayResult.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }
        private string GetDonationCategories(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f.Split(':').First() + " ";
                        }
                    }
                }
            }

            return retval;

        }
        private void sendThankyouMail(string _unid)
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var dItem = rpNew.GetAll().Where(o => o.unid == _unid).FirstOrDefault();


                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.SetAsDefault == true).FirstOrDefault();

                if (tn == null)
                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                String body = "";
                if (tn != null)
                {
                    body = tn.EmailContent;
                    //{{currentyear}}
                    //{{instancename}}
                    body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
                    body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                    body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
                    body = body.Replace("{{name}}", dItem.DonerName);
                    body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));
                    body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                    //  body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount));

                    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                    body = body.Replace("{{donorsurname}}", dItem.DonerName);
                    //donorcompany
                    //  body = body.Replace("{{category}}", dItem.CategoryList);

                    body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);



                    body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));

                    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                    //logo

                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

                }



                if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                {
                    Emailer em = new Emailer();
                    string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                    html_body = html_body.Replace("[table]", body);
                    body = html_body;

                    string fromid = Deffinity.systemdefaults.GetFromEmail();

                    string toid = dItem.DonerEmail;
                    string subject = "Donation";
                    //htomail.Value = toid;
                    //hsubject.Value = subject;
                    //CKEditor1.Text = body;
                    Email ToEmail = new Email();


                    ToEmail.SendingMail(fromid, subject, body, toid, "");

                    //sendThankyouMailTracker(dItem.ID, body, subject, toid, tn.ID);
                    // sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                    //Email ToEmail = new Email();


                    //ToEmail.SendingMail(fromid, subject,body,toid,"");

                    //sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private static void TithingProcessReturnValues(TithingPaymentTracker p, List<ReponseValues> rval, int trackerid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var pE = pmRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
                pE.PaidDate = DateTime.Now;
                pE.IsPaid = true;
                pE.PayRef = "retref:" + rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString() + ";profileid:" + rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                // pE. = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                // pE.IsPaid = true;
                pE.CResult = "";
                pmRep.Edit(pE);

                //var ipRep = new DCRepository<Incident_ServicePrice>();
                //var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(invoiceid)).FirstOrDefault();
                //if (pEntity != null)
                //{
                //    pEntity.Status = "Paid";
                //    pEntity.ModifiedDate = DateTime.Now;
                //    ipRep.Edit(pEntity);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



        

        ///<summary>
        /// Send request to Gateway using HTTP Direct API.
        ///

        /// </summary>
        /// <param name="request"> Request data </params>
        

        private bool AnyKeyStartsWith(NameValueCollection haystack, string needle)
        {
            foreach (string hay in haystack)
            {
                if (hay.StartsWith(needle))
                {
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, string> GetEnvironmentData()
        {
            var rtn = new Dictionary<string, string>();

            rtn.Add("HTTP_USER_AGENT", System.Web.HttpContext.Current.Request.UserAgent);
            rtn.Add("HTTP_ACCEPT", String.Join(",", System.Web.HttpContext.Current.Request.AcceptTypes));
            rtn.Add("HTTP_ACCEPT_ENCODING", System.Web.HttpContext.Current.Request.Headers.GetValues("Accept-Encoding")[0]);
            rtn.Add("HTTP_ACCEPT_LANGUAGE", System.Web.HttpContext.Current.Request.Headers.GetValues("Accept-Language")[0]);

            return rtn;

        }

        ///<summary>
        /// Send request to Gateway using HTTP Direct API.
        ///

        /// </summary>
        /// <param name="request"> Request data </params>
      

        private static Dictionary<string, string> GetInitialForm(string url, string remoteAddress, string marchantid ,string amount,string cardNumber,string cardExpiryMM,string cardExpiryYY,string cardCVV,string customerName,string customerEmail,string customerAddress,string customerPostcode)
        {
            var g = Guid.NewGuid().ToString();
            return new Dictionary<string, string>{
              {"merchantID",marchantid },
              {"action", "SALE"},
              {"type", "1"},

              //Notice: This isn't required by the gateway, but it is strongly recommended. 
              {"transactionUnique", g},

              {"countryCode","826"},
              {"currencyCode", "826"},
              {"amount",  String.Format("{0:F2}", Convert.ToDouble( amount)).Replace(".", "") },
        
              {"cardNumber", cardNumber},
              {"cardExpiryMonth", cardExpiryMM},
              {"cardExpiryYear",cardExpiryYY},
              {"cardCVV",cardCVV},
              {"customerName",customerName},
              {"customerEmail",customerEmail},
              {"customerAddress",customerAddress},
              {"customerPostcode",customerPostcode},
              {"orderRef", g},
    
                // remoteAddress, merchantCategoryCode, threeDSVersion and, threeDSRedirectURL 
                // fields are mandatory for 3DS v2

                // Notice: This must be the card holder's IP address, i.e, 
                // Request.UserHostAddress if you're using ASP.net.
                // For compatibility reasons, it must be an IPv4 address. 
              {"remoteAddress", remoteAddress},
              {"merchantCategoryCode", "5411" },
              {"threeDSVersion", "2"},

              // Notice: This must be set correctly. Customers will be directed
              // here following 3DS authorisation
              {"threeDSRedirectURL", url + "&acs=1&refid="+HttpContext.Current.Request.QueryString["refid"].ToString()},
              



            // Requests can carry arbitrary data in addition to the standard fields. 
            // The below keys contain a variety of symbols which may cause issues with 
            // signature calculation. 

            /*
                {"MerchantData[AnyKey]", "This can be any data"},
                {"MerchantData[SecondValue]", "Symbols: ! \" # $ % & ' () * + , - . / 0 1 2"},
                {"MerchantData[C]", "Nested Arrays should not be sorted"},
                {"MerchantData[MoreSymbols]", ": ; < = > ? @ A B [ \\ ] ^ _ ` a b c { | } ~ "}
            */
                };

        }

        public static string RandomString()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}