using System;
using System.IO;
using System.Web.Mvc;
using Stripe;
using Stripe.Checkout; // Include this namespace for the Session class
using Newtonsoft.Json;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace DeffinityAppDev.Controllers
{
    public class WebhookController : Controller
    {
        private const int ToleranceInSeconds = 600; // Custom tolerance window in seconds (10 minutes)

        [HttpPost]
        [ValidateInput(false)] // Disable request validation for this action
        public ActionResult Index()
        {
            LogExceptions.LogException("webhook:" + "Start");
            var json = new StreamReader(Request.InputStream).ReadToEnd();
            LogExceptions.LogException("webhook: json" + json);
            try
            {
               
                var stripeSignature = Request.Headers["Stripe-Signature"];
                LogExceptions.LogException("webhook:" + "signature");
                //  var webhookSecret = "whsec_vkpL4Nv0tRgsJwkNDFydLaAU1KO5zz9X";// ConfigurationManager.AppSettings["StripeWebhookSecret"];

                var webhookSecret = "whsec_vkpL4Nv0tRgsJwkNDFydLaAU1KO5zz9X";// //"whsec_OceMEPpmfBbIANMx85FlPZWoyr3uxrG7";
                var eventTimestamp = GetTimestampFromHeader(stripeSignature);
                var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (Math.Abs(currentTimestamp - eventTimestamp) > ToleranceInSeconds)
                {
                    LogExceptions.LogException("webhook:" + "Timestamp outside of the allowed tolerance.");
                    return new HttpStatusCodeResult(400, "Timestamp outside of the allowed tolerance.");
                }

                if (!VerifySignature(json, stripeSignature, webhookSecret))
                {
                    LogExceptions.LogException("webhook:" + "Invalid signature.");
                    return new HttpStatusCodeResult(400, "Invalid signature.");
                }

                var stripeEvent = EventUtility.ParseEvent(json);

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session != null)
                    {
                        LogExceptions.LogException("webhook: PaymentIntentId" + session.PaymentIntentId);
                        // Retrieve the payment intent
                        //var paymentIntentService = new PaymentIntentService();
                        //var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                        //// Calculate 70% of the amount received
                        //var transferAmount = (long)(paymentIntent.AmountReceived * 0.7);

                        //// Create a transfer to another account
                        //var transferOptions = new TransferCreateOptions
                        //{
                        //    Amount = transferAmount,
                        //    Currency = "usd",
                        //    Destination = "acct_1Example" // Replace with the actual Stripe account ID to transfer to
                        //};

                        //var transferService = new TransferService();
                        //var transfer = transferService.Create(transferOptions);
                    }
                }

                return new HttpStatusCodeResult(200);
            }
            catch (StripeException e)
            {
                LogError(e.Message);
                return new HttpStatusCodeResult(400, e.Message);
            }
        }

        private long GetTimestampFromHeader(string stripeSignature)
        {
            var parts = stripeSignature.Split(',');
            foreach (var part in parts)
            {
                if (part.StartsWith("t=", StringComparison.OrdinalIgnoreCase))
                {
                    return long.Parse(part.Substring(2));
                }
            }
            throw new Exception("Timestamp not found in Stripe-Signature header.");
        }

        private bool VerifySignature(string json, string stripeSignature, string webhookSecret)
        {
            var signatureParts = stripeSignature.Split(',');
            string expectedSignature = null;
            string timestamp = null;

            foreach (var part in signatureParts)
            {
                if (part.StartsWith("t=", StringComparison.OrdinalIgnoreCase))
                {
                    timestamp = part.Substring(2);
                }
                else if (part.StartsWith("v1=", StringComparison.OrdinalIgnoreCase))
                {
                    expectedSignature = part.Substring(3);
                }
            }

            var payload = $"{timestamp}.{json}";
            var secretBytes = Encoding.UTF8.GetBytes(webhookSecret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);
            string computedSignature;

            using (var hasher = new HMACSHA256(secretBytes))
            {
                var hash = hasher.ComputeHash(payloadBytes);
                computedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            return computedSignature == expectedSignature;
        }

        private void LogError(string message)
        {
            LogExceptions.LogException("webhook:"+message);
            //var logPath = Server.MapPath("~/App_Data/Logs/StripeWebhookErrors.txt");
            //using (var writer = new StreamWriter(logPath, true))
            //{
            //    writer.WriteLine($"{DateTime.Now}: {message}");
            //}
        }
    }
}