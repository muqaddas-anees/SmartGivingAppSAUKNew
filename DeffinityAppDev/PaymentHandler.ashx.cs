using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DeffinityAppDev
{
    /// <summary>
    /// Summary description for PaymentHandler
    /// </summary>
    public class PaymentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //StripeConfiguration.ApiKey = "sk_test_51P9j0YSAzteR3BT796wIqNTx0j8H9IT73H6n9Nj0D4WX0AJPpLPOtsMmFXccCstALB14V6ro7kK9McHKTje5WhXx00zgjZU0oq";

            //var stripeToken = context.Request.Form["stripeToken"];

            //var options = new ChargeCreateOptions
            //{
            //    Amount = 2000, // $20, specified in cents
            //    Currency = "usd",
            //    Source = stripeToken,
            //    Description = "Example charge"
            //};

            //var service = new ChargeService();
            //Charge charge = service.Create(options);

            //context.Response.ContentType = "text/plain";
            //if (charge.Paid)
            //{
            //    context.Response.Write("Payment successful!");
            //}
            //else
            //{
            //    context.Response.Write("Payment failed.");
            //}

            context.Response.ContentType = "application/json";
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string json = reader.ReadToEnd();
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string token = data.token;

            StripeConfiguration.ApiKey = "sk_test_51P9j0YSAzteR3BT796wIqNTx0j8H9IT73H6n9Nj0D4WX0AJPpLPOtsMmFXccCstALB14V6ro7kK9McHKTje5WhXx00zgjZU0oq";

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = 1000, // amount in cents
                Currency = "usd",
                PaymentMethod = token,
                Confirm = true
            };

            try
            {
                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);

                // Creating transfer for 10% of the amount
                var transferService = new TransferService();
                var transfer1Options = new TransferCreateOptions
                {
                    Amount = 100, // 10% of the amount
                    Currency = "usd",
                    Destination = "acct_1M90vESEzdqVp8IH",
                    TransferGroup = paymentIntent.TransferGroup
                };
                transferService.Create(transfer1Options);

                // Creating transfer for 5% of the amount
                //var transfer2Options = new TransferCreateOptions
                //{
                //    Amount = 50, // 5% of the amount
                //    Currency = "usd",
                //    Destination = "acct_2GqIC8E9XsXhHT5Y",
                //    TransferGroup = paymentIntent.TransferGroup
                //};
                //transferService.Create(transfer2Options);

                context.Response.Write("{\"status\":\"success\"}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Write("{\"status\":\"error\", \"message\":\"" + ex.Message + "\"}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}