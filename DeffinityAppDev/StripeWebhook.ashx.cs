using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace DeffinityAppDev
{
    /// <summary>
    /// Summary description for StripeWebhook
    /// </summary>
    public class StripeWebhook : IHttpHandler
    {

        private const string EndpointSecret = "whsec_PSBFeGEQVGdNlv7US7iWvvIp3ZIQ2cLN"; //Testmode"whsec_wQBzzg1KxXGHaeAPV0sAIXhzQrB3Z3oV"; //"whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7";

        public void ProcessRequest(HttpContext context)
        {
            LogExceptions.LogException("StripeWebhook.ashx: start");
            var json = new StreamReader(context.Request.InputStream).ReadToEnd();

            try
            {
                LogRequestHeaders(context);

                //if (context.Request.Headers["Stripe-Signature"] != null)
                //{
                //    LogExceptions.LogException("StripeWebhook.ashx json: " + json);
                //    var stripeEvent = ConstructStripeEvent(json, context.Request.Headers["Stripe-Signature"]);

                //    if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                //    {
                //        HandleCheckoutSessionCompleted(stripeEvent);
                //    }

                //    context.Response.StatusCode = 200;
                //    context.Response.Write(JsonConvert.SerializeObject(new { status = "success" }));
                //}
                //else
                //{
                //    LogExceptions.LogException("StripeWebhook.ashx: Stripe-Signature header is missing");
                //    context.Response.StatusCode = 400;
                //    context.Response.Write(JsonConvert.SerializeObject(new { error = "Stripe-Signature header is missing" }));
                //}
            }
            catch (StripeException e)
            {
                LogExceptions.LogException("StripeWebhook.ashx StripeException: " + e.Message);
                context.Response.StatusCode = 400;
                context.Response.Write(JsonConvert.SerializeObject(new { error = e.Message }));
            }
            catch (Exception e)
            {
                LogExceptions.LogException("StripeWebhook.ashx Exception: " + e.Message);
                context.Response.StatusCode = 500;
                context.Response.Write(JsonConvert.SerializeObject(new { error = e.Message }));
            }
        }

        private void LogRequestHeaders(HttpContext context)
        {
            //foreach (var key in context.Request.Headers.AllKeys)
            //{
            //    LogExceptions.LogException($"Header: {key} = {context.Request.Headers[key]}");
            //}
        }

        private Event ConstructStripeEvent(string json, string stripeSignature)
        {
            try
            {
                return EventUtility.ConstructEvent(json, stripeSignature, EndpointSecret, 600, false);
            }
            catch (StripeException e)
            {
                LogExceptions.LogException($"StripeException: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                LogExceptions.LogException($"Exception: {e.Message}");
                throw;
            }
        }

        private void HandleCheckoutSessionCompleted(Event stripeEvent)
        {
            try
            {
                var session = stripeEvent.Data.Object as Session;
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var pEntity = pmRep.GetAll().FirstOrDefault(o => o.StripeSessionID == session.Id);

                if (pEntity != null)
                {
                    ProcessPayment(session, pEntity);
                }
                else
                {
                    LogExceptions.LogException("No entity found with session ID: " + session.Id);
                    CreateDefaultTransfer(session);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.LogException("StripeWebhook.ashx HandleCheckoutSessionCompleted exception: " + ex.Message);
            }
        }

        private void ProcessPayment(Session session, PortfolioMgt.Entity.TithingPaymentTracker pEntity)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
            LogExceptions.LogException("StripeWebhook.ashx paymentIntent.ComapanyAmount: " + (long?)((pEntity.ComapanyAmount ?? 0.00) * 100));
            LogExceptions.LogException("StripeWebhook.ashx paymentIntent.AmountReceived: " + paymentIntent.AmountReceived);

            var transferOptions = new TransferCreateOptions
            {
                Amount = 100,// (long?)((pEntity.ComapanyAmount ?? 0.00) * 100),
                Currency = "GBP",
                Destination = pEntity.CompanyAccountID
            };

            var transferService = new TransferService();
            var transfer = transferService.Create(transferOptions);
           
            LogExceptions.LogException("Transfer created: " + transfer.Id);
        }

        private void CreateDefaultTransfer(Session session)
        {
            var transferOptions = new TransferCreateOptions
            {
                Amount = (long?)((Convert.ToDouble(0.50)) * 100),

                //Amount = 50 * 100,
                Currency = "GBP",
                Destination = "acct_1P9j0YSAzteR3BT7"
            };

            var transferService = new TransferService();
            var transfer = transferService.Create(transferOptions);

            LogExceptions.LogException("Default transfer created: " + transfer.Id);
        }

        public bool IsReusable => false;
        ////public void ProcessRequest(HttpContext context)
        ////{
        ////    LogExceptions.LogException("StripeWebhook.ashx: start");
        ////    var json = new StreamReader(context.Request.InputStream).ReadToEnd();
        ////    // const string endpointSecret = "whsec_MDCoBLFfZiXYMyDPuEfVE0YHG23H0vLX"; // Replace with your actual webhook secret
        ////    const string endpointSecret = "whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7";
        ////    //whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7
        ////    try
        ////    {
        ////        LogExceptions.LogException("StripeWebhook.ashx: first step");

        ////        // Log all request headers
        ////        foreach (var key in context.Request.Headers.AllKeys)
        ////        {
        ////            LogExceptions.LogException($"Header: {key} = {context.Request.Headers[key]}");
        ////        }

        ////        if (context.Request.Headers["Stripe-Signature"] != null)
        ////        {
        ////            //LogExceptions.LogException("StripeWebhook.ashx: stripeEvent");

        ////            LogExceptions.LogException("StripeWebhook.ashx json: "+json);

        ////            // Increasing the tolerance to 600 seconds (10 minutes)
        ////            var stripeEvent = ConstructStripeEvent(json, context.Request.Headers["Stripe-Signature"]);

        ////            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        ////            {
        ////                LogExceptions.LogException("StripeWebhook.ashx: CheckoutSessionCompleted");
        ////                try
        ////                {



        ////                    var session = stripeEvent.Data.Object as Session;


        ////                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
        ////                    var pEntity = pmRep.GetAll().Where(o => o.StripeSessionID == session.Id).FirstOrDefault();

        ////                    if (pEntity != null)
        ////                    {
        ////                        LogExceptions.LogException("session ID: " + pEntity.StripeSessionID);

        ////                        // Retrieve the payment intent
        ////                        var paymentIntentService = new PaymentIntentService();
        ////                        var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
        ////                        LogExceptions.LogException("StripeWebhook.ashxpaymentIntent.AmountReceived: " + (long)(paymentIntent.AmountReceived * 0.7));
        ////                        // Calculate 70% of the amount received
        ////                       // var transferAmount = 300;// (long)(paymentIntent.AmountReceived * 0.7);

        ////                        // Create a transfer to another account
        ////                        var transferOptions = new TransferCreateOptions
        ////                        {
        ////                            Amount = (long?)(pEntity.ComapanyAmount??0 * 100 ),
        ////                            Currency = "GBP", //"inr",
        ////                            Destination = pEntity.CompanyAccountID//"acct_1P9j0YSAzteR3BT7", // Replace with the actual Stripe account ID to transfer to
        ////                        };

        ////                        var transferService = new TransferService();
        ////                        var transfer = transferService.Create(transferOptions);
        ////                    }
        ////                    else
        ////                    {
        ////                        LogExceptions.LogException("session ID: " + pEntity.StripeSessionID);

        ////                        // Retrieve the payment intent
        ////                        var paymentIntentService = new PaymentIntentService();
        ////                        var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
        ////                       // LogExceptions.LogException("StripeWebhook.ashxpaymentIntent.AmountReceived: " + (long)(paymentIntent.AmountReceived * 0.7));
        ////                        // Calculate 70% of the amount received
        ////                        // var transferAmount = 300;// (long)(paymentIntent.AmountReceived * 0.7);

        ////                        // Create a transfer to another account
        ////                        var transferOptions = new TransferCreateOptions
        ////                        {
        ////                            Amount = (long?)(0.5 * 100),
        ////                            Currency = "GBP", //"inr",
        ////                            Destination = "acct_1P9j0YSAzteR3BT7", // Replace with the actual Stripe account ID to transfer to
        ////                        };

        ////                        var transferService = new TransferService();
        ////                        var transfer = transferService.Create(transferOptions);
        ////                    }
        ////                }
        ////                catch (Exception ex)
        ////                {
        ////                    LogExceptions.LogException("StripeWebhook.ashx: ex:" + ex.Message);
        ////                }
        ////            }

        ////            context.Response.StatusCode = 200;
        ////        }
        ////        else
        ////        {
        ////            LogExceptions.LogException("StripeWebhook.ashx: Stripe-Signature header is missing");
        ////            context.Response.StatusCode = 400;
        ////            context.Response.Write(JsonConvert.SerializeObject(new { error = "Stripe-Signature header is missing" }));
        ////        }
        ////    }
        ////    catch (StripeException e)
        ////    {
        ////        LogExceptions.LogException("StripeWebhook.ashx:" + e.Message);
        ////        context.Response.StatusCode = 400;
        ////        context.Response.Write(JsonConvert.SerializeObject(new { error = e.Message }));
        ////    }
        ////}
        ////public Event ConstructStripeEvent(string json, string stripeSignature)
        ////{
        ////    const string endpointSecret = "whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7";
        ////    const int toleranceInSeconds = 600;
        ////    const bool throwOnApiVersionMismatch = false;

        ////    try
        ////    {
        ////        return EventUtility.ConstructEvent(json, stripeSignature, endpointSecret, toleranceInSeconds, throwOnApiVersionMismatch);
        ////    }
        ////    catch (StripeException e)
        ////    {
        ////        // Handle Stripe exception (e.g., log the error, notify admin)
        ////        LogExceptions.LogException($"StripeException: {e.Message}");
        ////        throw;
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        // Handle general exception
        ////        LogExceptions.LogException($"Exception: {e.Message}");
        ////        throw;
        ////    }
        ////}
        ////private Transfer TransferToConnectedAccount(string chargeId, long amount, string currency, string destinationAccountId)
        ////{
        ////    var transferOptions = new TransferCreateOptions
        ////    {
        ////        Amount = amount,
        ////        Currency = currency,
        ////        Destination = destinationAccountId,
        ////        SourceTransaction = chargeId
        ////    };

        ////    var transferService = new TransferService();
        ////    try
        ////    {
        ////        return transferService.Create(transferOptions);
        ////    }
        ////    catch (StripeException ex)
        ////    {
        ////        LogExceptions.WriteExceptionLog(ex);
        ////        // Handle exception
        ////        Console.WriteLine(ex.Message);
        ////        HttpContext.Current.Response.Write(ex.Message);
        ////        return null;
        ////    }
        ////}

        //public void ProcessRequest(HttpContext context)
        //{
        //    var json = new StreamReader(context.Request.InputStream).ReadToEnd();

        //    try
        //    {
        //        LogExceptions.LogException("StripeWebhook.ashx: first step");

        //        if (context.Request.Headers["Stripe-Signature"] != null)
        //        {
        //            LogExceptions.LogException("StripeWebhook.ashx: stripeEvent");
        //            //whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7 -nm
        //            //whsec_MDCoBLFfZiXYMyDPuEfVE0YHG23H0vLX - indra
        //              var stripeEvent = EventUtility.ConstructEvent(json, context.Request.Headers["Stripe-Signature"], "whsec_MDCoBLFfZiXYMyDPuEfVE0YHG23H0vLX");

        //            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        //            {
        //                LogExceptions.LogException("StripeWebhook.ashx: CheckoutSessionCompleted");
        //                try
        //                {
        //                    var session = stripeEvent.Data.Object as Session;

        //                    // Retrieve the payment intent
        //                    var paymentIntentService = new PaymentIntentService();
        //                    var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

        //                    // Calculate 70% of the amount received
        //                    var transferAmount = (long)(paymentIntent.AmountReceived * 0.7);

        //                    // Create a transfer to another account
        //                    var transferOptions = new TransferCreateOptions
        //                    {
        //                        Amount = transferAmount,
        //                        Currency = "inr",
        //                        Destination = "acct_1P9j0YSAzteR3BT7", // Replace with the actual Stripe account ID to transfer to
        //                    };

        //                    var transferService = new TransferService();
        //                    var transfer = transferService.Create(transferOptions);
        //                }
        //                catch(Exception ex)
        //                {
        //                    LogExceptions.LogException("StripeWebhook.ashx: ex:"+ ex.Message);
        //                }
        //            }

        //            context.Response.StatusCode = 200;
        //        }
        //        else
        //        {
        //            context.Response.StatusCode = 400;
        //            context.Response.Write(JsonConvert.SerializeObject(new { error ="error" }));

        //        }
        //    }
        //    catch (StripeException e)
        //    {
        //        LogExceptions.LogException("StripeWebhook.ashx:"+e.Message);
        //        context.Response.StatusCode = 400;
        //        context.Response.Write(JsonConvert.SerializeObject(new { error = e.Message }));
        //    }
        //}

        //public void ProcessRequest(HttpContext context)
        //{

        //    // This is your Stripe CLI webhook secret for testing
        //    // //we_1PKqtUSEzdqVp8IHQSh3NLFPyour endpoint locally.
        //    //whsec_52smbNhoGVkcxmaG77PXMvzqa8dLwfx7
        //    //whsec_MDCoBLFfZiXYMyDPuEfVE0YHG23H0vLX
        //    const string endpointSecret = "whsec_31e262800c442a7f28dfa3b4bff1f6f65c310225e857b4bd7572dc08ca9718cb";
        //    //whsec_31e262800c442a7f28dfa3b4bff1f6f65c310225e857b4bd7572dc08ca9718cb

        //    try
        //    {
        //        LogExceptions.LogException("StripeWebhook.ashx: first step");

        //        context.Response.ContentType = "application/json";

        //        var json = new StreamReader(context.Request.InputStream).ReadToEnd();
        //        var stripeEvent = EventUtility.ConstructEvent(
        //            json,
        //            context.Request.Headers["Stripe-Signature"],
        //            endpointSecret // Replace with your webhook secret
        //        );
        //        LogExceptions.LogException("stripeEvent.Type");
        //        if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        //        {

        //            LogExceptions.LogException("Events.CheckoutSessionCompleted");

        //            var session = stripeEvent.Data.Object as Session;
        //            var paymentIntentId = session.PaymentIntentId;

        //            // Retrieve the PaymentIntent
        //            var paymentIntentService = new PaymentIntentService();
        //            var paymentIntent = paymentIntentService.Get(paymentIntentId, new PaymentIntentGetOptions
        //            {
        //                Expand = new List<string> { "charges" }
        //            });

        //            // Now create the transfer
        //            //.Charges?.Data[0].Id
        //            LogExceptions.LogException("Events.CheckoutSessionCompleted");
        //            var transfer = TransferToConnectedAccount(paymentIntent.LatestChargeId, 500, "inr", "acct_1P9j0YSAzteR3BT7");
        //          //  var transfer = TransferToConnectedAccount(paymentIntent.LatestChargeId, 500, "usd", "acct_1OWcDdJzy21cAPjJ");
        //            if (transfer != null)
        //            {
        //                context.Response.Write("{\"status\":\"success\"}");
        //            }
        //            else
        //            {
        //                context.Response.Write("{\"status\":\"failure\"}");
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);

        //        context.Response.Write("{\"status\":\"0\"}");
        //    }
        //}

        //private Transfer TransferToConnectedAccount(string chargeId, long amount, string currency, string destinationAccountId)
        //{
        //    var transferOptions = new TransferCreateOptions
        //    {
        //        Amount = amount,
        //        Currency = currency,
        //        Destination = destinationAccountId,
        //        SourceTransaction = chargeId
        //    };

        //    var transferService = new TransferService();
        //    try
        //    {
        //        return transferService.Create(transferOptions);
        //    }
        //    catch (StripeException ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //        // Handle exception
        //        Console.WriteLine(ex.Message);
        //        HttpContext.Current.Response.Write(ex.Message);
        //        return null;
        //    }
        //}

        //public bool IsReusable
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
    }
}