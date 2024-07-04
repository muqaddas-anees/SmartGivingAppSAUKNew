using PortfolioMgt.Entity;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DeffinityAppDev.App.PayProcess;

namespace DeffinityAppDev
{
    public partial class PaymentForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              StripeConfiguration.ApiKey = "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";

            //StripeConfiguration.ApiKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";

            if(!IsPostBack)
            {
             
            }


        }
        private Session CreateCheckoutSession(int id)
        {
            StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey(); //"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";

            var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            var pE = pmRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
           
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


                if (pE != null)
                {
                    pE.StripeSessionID = session.Id;
                    pmRep.Edit(pE);
                }
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
        protected void SubmitButton_Click(object sender, EventArgs e)
        {

             StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey();
            //StripeConfiguration.ApiKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";
            var sessionUrl = CreateCheckoutSession();
            if (!string.IsNullOrEmpty(sessionUrl))
            {
                // Redirect to Stripe Checkout using the session URL
                Response.Redirect(sessionUrl);
            }
            else
            {
                Response.Redirect("~/App/Dashboard.aspx");
                // Handle the case where session creation failed
                Response.Write("Failed to create a Stripe Checkout session.");
            }
        }

        private string CreateCheckoutSession()
        {
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
                            UnitAmount = 123,// ((1.23) * 100), // Amount in cents (e.g., 100.00 USD)
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
                SuccessUrl = Deffinity.systemdefaults.GetWebUrl()+ "/stripe_success.aspx?session_id={CHECKOUT_SESSION_ID}", // Replace with your success URL
                CancelUrl = Deffinity.systemdefaults.GetWebUrl() + "/stripe_cancel.aspx", // Replace with your cancel URL
                
                Metadata = new Dictionary<string, string>
                {
                    { "order_id", "6735" } // Example metadata
                }
            };

            var service = new SessionService();
            try
            {
                Session session = service.Create(options);

                LogExceptions.LogException("sessionid:" +session.Id);
                return session.Url;
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

        protected void Timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                LogExceptions.LogException("Timer1");
                if (QueryStringValues.UNID.Length > 0)
                {
                    var sessionUrl = CreateCheckoutSession(Convert.ToInt32(QueryStringValues.UNID));


                    if (!string.IsNullOrEmpty(sessionUrl.Url))
                    {
                        // Redirect to Stripe Checkout using the session URL
                        Response.Redirect(sessionUrl.Url);
                    }
                    else
                    {
                        // Handle the case where session creation failed
                        Response.Write("Failed to create a Stripe Checkout session.");
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}
//namespace DeffinityAppDev
//{
//    public partial class PaymentForm : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            StripeConfiguration.ApiKey = "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"; // Your Stripe secret key
//        }

//        protected void SubmitButton_Click(object sender, EventArgs e)
//        {
//            var token = hiddenField.Value; // Token from Stripe.js
//            var charge = CreateCharge(100, "gbp", token, "Charge for service"); // Amount in cents
//            if (charge != null)
//            {
//                // Implement logic to handle transfers
//                // Example: Transfer to a connected account
//                // You need to adjust with your actual logic and connected account IDs
//                var transfer = TransferToConnectedAccount(charge.Id, 70, "gbp", "acct_1OWcDdJzy21cAPjJ");
//            }
//        }

//        private Charge CreateCharge(long amount, string currency, string sourceToken, string description)
//        {
//            var chargeOptions = new ChargeCreateOptions
//            {
//                Amount = amount,
//                Currency = currency,
//                Source = sourceToken,
//                Description = description
//            };

//            var chargeService = new ChargeService();
//            try
//            {
//                return chargeService.Create(chargeOptions);
//            }
//            catch (StripeException ex)
//            {
//                // Handle exception
//                Console.WriteLine(ex.Message);
//                Response.Write(ex.Message);
//                return null;
//            }
//        }

//        private Transfer TransferToConnectedAccount(string chargeId, long amount, string currency, string destinationAccountId)
//        {
//            var transferOptions = new TransferCreateOptions
//            {
//                Amount = amount,
//                Currency = currency,
//                Destination = destinationAccountId,
//                SourceTransaction = chargeId
//            };

//            var transferService = new TransferService();
//            try
//            {
//                return transferService.Create(transferOptions);
//            }
//            catch (StripeException ex)
//            {
//                // Handle exception
//                Console.WriteLine(ex.Message);
//                Response.Write(ex.Message);
//                return null;
//            }
//        }
//    }
//}