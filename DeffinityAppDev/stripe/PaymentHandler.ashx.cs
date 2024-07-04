using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.stripe
{
    /// <summary>
    /// Summary description for PaymentHandler
    /// </summary>
    public class PaymentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            //string stripeApiKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";
            //StripeConfiguration.ApiKey = stripeApiKey;

            StripeConfiguration.ApiKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";

            // Retrieve the payment amount from the request
            string amountStr = context.Request["amount"];
            int amount = int.Parse(amountStr);

            // Calculate the 80% and 20% split amounts
            int amount80 = (int)(amount * 0.8);
            int amount20 = amount - amount80;

            try
            {
                // Create the payment intent for the first account (80%)
                //var options1 = new PaymentIntentCreateOptions
                //{
                //    Amount = amount80,
                //    Currency = "usd",
                //    PaymentMethodTypes = new List<string> { "card" },
                //    TransferData = new PaymentIntentTransferDataOptions
                //    {
                //        Destination = "acct_1M90vESEzdqVp8IH", // First account ID
                //    },
                //};
                //var service1 = new PaymentIntentService();
                //PaymentIntent intent1 = service1.Create(options1);

                // Create the payment intent for the second account (20%)
                var options2 = new PaymentIntentCreateOptions
                {
                    Amount = amount20,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    TransferData = new PaymentIntentTransferDataOptions
                    {
                        Destination = "acct_1PJOurSE9jVIAUPH", // Second account ID
                    },
                };
                var service2 = new PaymentIntentService();
                PaymentIntent intent2 = service2.Create(options2);

                // Return success response
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"status\":\"success\"}");
            }
            catch (StripeException e)
            {
                // Handle error
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"status\":\"error\",\"message\":\"" + e.Message + "\"}");
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        private void TransferFunds(int amount, string accountId)
        {
            // Example function to transfer funds using a mock bank API
            //var bankApi = new BankApi();
            //bankApi.Transfer(amount, accountId);
        }
    }
}