using DC.BLL;
using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;
using Org.BouncyCastle.Asn1.X509;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakepaymentsGateway;
using static DC_FLS_ctrl;

namespace DeffinityAppDev.WF.DC
{
    public partial class PayProcess : System.Web.UI.Page
    {
        DirectAPIForm payment = new DirectAPIForm();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Form.Count >0)
                {
                    getdata(Request.QueryString["refid"]);
                    // var pay_ment = Session["payment"] as DirectAPIForm;
                    // var sssss = Session["responseCode"];
                    //Session["PaRes"] = Request.Form["PaRes"].ToString();
                    // PaymentProcess();
                    LogExceptions.LogException("3D after redirect ");
                    LogExceptions.LogException("Session['amount'].ToString() :" + Session["amount"]);
                    LogExceptions.LogException("Session['invoiceref'].ToString() :" + Session["invoiceref"]);
                    LogExceptions.LogException("Session['cardnumber'].ToString() :" + Session["cardnumber"]);
                    LogExceptions.LogException("Session['cvv'].ToString() :" + Session["cvv"]);
                    LogExceptions.LogException("Session['month'].ToString() :" + Session["month"]);
                    LogExceptions.LogException("Session['year'].ToString() :" + Session["year"]);
                    LogExceptions.LogException("Session['customername'].ToString() :" + Session["customername"]);
                    LogExceptions.LogException("Session['address'].ToString() :" + Session["address"]);
                    LogExceptions.LogException("Session['postcode'].ToString() :" + Session["postcode"]);
                    LogExceptions.LogException("Session['customeremail'].ToString() :" + Session["customeremail"]);
                    LogExceptions.LogException("Session['phone'].ToString() :" + Session["phone"]);
                    LogExceptions.LogException("Session['customerref'].ToString() :" + Session["customerref"]);
                    if (Session["uqid"] != null)
                    {
                        LogExceptions.LogException("Session['uqid'].ToString() :" + Session["uqid"] != null ? Session["uqid"].ToString() : "");
                        PaymentProcess(sessionKeys.PortfolioID, Convert.ToDouble(Session["amount"].ToString()), Session["invoiceref"].ToString(),
                            Session["cardnumber"].ToString(), Session["cvv"].ToString(), Session["month"].ToString(), Session["year"].ToString(),
                            Session["customername"].ToString(), Session["address"].ToString(), Session["postcode"].ToString(),
                           Session["customeremail"].ToString(), Session["phone"].ToString(), Session["customerref"].ToString(), Request.QueryString["refid"].ToString(), Session["uqid"] != null ? Session["uqid"].ToString() : "");
                    }
                    else
                    {
                        PaymentProcess(sessionKeys.PortfolioID, Convert.ToDouble(Session["amount"].ToString()), Session["invoiceref"].ToString(),
                           Session["cardnumber"].ToString(), Session["cvv"].ToString(), Session["month"].ToString(), Session["year"].ToString(),
                           Session["customername"].ToString(), Session["address"].ToString(), Session["postcode"].ToString(),
                          Session["customeremail"].ToString(), Session["phone"].ToString(), Session["customerref"].ToString(), Request.QueryString["refid"].ToString(), "");
                    }
                }
                else
                {
                    //var p = postdata();
                    getdata(Request.QueryString["refid"]);
                    LogExceptions.LogException("First load after redirect ");

                    LogExceptions.LogException("Session['amount'].ToString() :" + Session["amount"]);
                    LogExceptions.LogException("Session['invoiceref'].ToString() :" + Session["invoiceref"]);
                    LogExceptions.LogException("Session['cardnumber'].ToString() :" + Session["cardnumber"]);
                    LogExceptions.LogException("Session['cvv'].ToString() :" + Session["cvv"]);
                    LogExceptions.LogException("Session['month'].ToString() :" + Session["month"]);
                    LogExceptions.LogException("Session['year'].ToString() :" + Session["year"]);
                    LogExceptions.LogException("Session['customername'].ToString() :" + Session["customername"]);
                    LogExceptions.LogException("Session['address'].ToString() :" + Session["address"]);
                    LogExceptions.LogException("Session['postcode'].ToString() :" + Session["postcode"]);
                    LogExceptions.LogException("Session['customeremail'].ToString() :" + Session["customeremail"]);
                    LogExceptions.LogException("Session['phone'].ToString() :" + Session["phone"]);
                    LogExceptions.LogException("Session['customerref'].ToString() :" + Session["customerref"]);
                    LogExceptions.LogException(" Request.QueryString['refid'] :" + Request.QueryString["refid"]);
                    LogExceptions.LogException("Session['uqid'].ToString() :" + Session["uqid"] != null ? Session["uqid"].ToString() : "");
                    //LogExceptions.LogException("Session['amount'].ToString() :" + Session["amount"]);

                    // portfolioid.Value = sessionKeys.PortfolioID.ToString();
                    if (Session["uqid"] != null)
                    {
                        LogExceptions.LogException("Session['uqid'].ToString() :" + Session["uqid"] != null ? Session["uqid"].ToString() : "");
                        PaymentProcess(sessionKeys.PortfolioID, Convert.ToDouble(Session["amount"].ToString()), Session["invoiceref"].ToString(),
                            Session["cardnumber"].ToString(), Session["cvv"].ToString(), Session["month"].ToString(), Session["year"].ToString(),
                            Session["customername"].ToString(), Session["address"].ToString(), Session["postcode"].ToString(),
                           Session["customeremail"].ToString(), Session["phone"].ToString(), Session["customerref"].ToString(), Request.QueryString["refid"].ToString(), Session["uqid"] != null ? Session["uqid"].ToString() : "");
                    }
                    else
                    {
                        PaymentProcess(sessionKeys.PortfolioID, Convert.ToDouble(Session["amount"].ToString()), Session["invoiceref"].ToString(),
                           Session["cardnumber"].ToString(), Session["cvv"].ToString(), Session["month"].ToString(), Session["year"].ToString(),
                           Session["customername"].ToString(), Session["address"].ToString(), Session["postcode"].ToString(),
                          Session["customeremail"].ToString(), Session["phone"].ToString(), Session["customerref"].ToString(), Request.QueryString["refid"].ToString(), "");
                    }



                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void getdata(string refid)
        {

            var rep = new PortfolioRepository<PortfolioMgt.Entity.ReferenceData>();
            var refdata = rep.GetAll().Where(o => o.refid == refid).FirstOrDefault();
            if(refdata != null)
            {
                var spdata = refdata.refdata;

                var sdata = spdata.Split(';');

                foreach(string s in sdata)
                {
                    if(s.Length >0)
                    {
                        var d = s.Split(':');
                        if (d.First().Length>1)
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
                                if(d.Last().Length>0)
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

            setval = setval + string.Format("uid:{0};",sessionKeys.UID);
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

        private void PaymentProcess(int portfolioid, double amount, string invoiceref, string cardnumber, string cvv, string month, string year, string customername, string address, string postcode, string customeremail, string phone, string customerref,string refid,string uqid)
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
                string ExampleCardExpiryYY = year.Length == 4? year.Substring(2,2): year;
                string ExampleCustomerName = customername;
                string ExampleCustomerAddress = address;
                string ExampleCustomerPostcode = postcode;
                string ExampleCustomerEmail = customeremail;
                string ExampleCustomerPhone = phone;
                string ExampleMerchantCustomerRef = customerref;
                int ExampleCountryCode = 826;


                // payment.Amount = 1100f;
               //payment.cardTypeCode

                payment.Amount = float.Parse(String.Format("{0:F2}", amount).Replace(".",""), CultureInfo.InvariantCulture);
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

                    Session["responseCode"] = payment.responseCode;
                    Session["PageUrl"] = payment.PageUrl + "&retval=1";// + "?refid=" + refid;
                    Session["ThreeDSMD"] = payment.ThreeDSMD;
                    Session["ThreeDSPaReq"] = payment.ThreeDSPaReq;
                    Session["TermUrl"] = payment.PageUrl + "&retval=1";// "?refid="+refid;
                    Session["ThreeDSACSURL"] = payment.ThreeDSACSURL;
                    // lblResult.Text = htmlString();
                    //Response.Redirect("paynext.aspx", false);
                }
                else
                {
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
                            if (payment.responseCode == 0)
                            {
                                //Response.Redirect("~/WF/DC/PayResult.aspx?status=ok", false);

                                Response.Redirect("~/App/Donations.aspx?status=ok&unid=" + invoiceref, false);
                            }
                            else
                            {
                                //App/TithingProcess.aspx
                               // Response.Redirect("~/WF/DC/PayResult.aspx?status=fail", false);

                                Response.Redirect("~/App/Donations.aspx?status=fail&unid=" + invoiceref, false);
                            }
                        }
                        else
                        {
                            if (QueryStringValues.Type.Length > 0)
                            {
                                if (QueryStringValues.Type == "mail")
                                {
                                    Response.Redirect("~/WF/payinvoice/PaymentProcess.aspx?uqid="+uqid, false);
                                }
                                else
                                    Response.Redirect("~/WF/CustomerAdmin/ProcessPayment.aspx", false);
                            }
                            else
                                Response.Redirect("~/WF/DC/DashboardV2.aspx", false);
                        }
                    }
                    else
                        Response.Redirect("~/WF/DC/DashboardV2.aspx", false);
                }
            }
            catch(Exception ex)
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

    }
}