using System;
using System.Web;
using Stripe;

namespace StripeWebApp.Methods
{
    /// <summary>
    /// Summary description for SubscriptionCreateHandler
    /// </summary>
    public class SubscriptionCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonString = String.Empty;
            HttpContext.Current.Request.InputStream.Position = 0;
            using (System.IO.StreamReader inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
                System.Web.Script.Serialization.JavaScriptSerializer jSerialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                var appSubscription = jSerialize.Deserialize<AppSubscription>(jsonString);

                if (appSubscription != null)
                {
                    try
                    {
                        var customer = BaseClass.CreateCustomer(appSubscription.BillingDetails, appSubscription.PaymentMethodId);
                        
                        var subscription = BaseClass.CreateSubscription(appSubscription.PriceId, customer.Id, appSubscription.Quantity);
                        context.Response.Write(jSerialize.Serialize(subscription));
                    }
                    catch (StripeException e)
                    {
                        var message = e.Message;
                        context.Response.StatusCode = 500;
                        context.Response.Write(jSerialize.Serialize(message));
                    }
                }
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