using System;
using System.Web;
using Stripe;

namespace StripeWebApp.Methods
{
    /// <summary>
    /// Summary description for SubscriptionRetrieveHandler
    /// </summary>
    public class SubscriptionRetrieveHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonString = String.Empty;
            HttpContext.Current.Request.InputStream.Position = 0;
            using (System.IO.StreamReader inputStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
                System.Web.Script.Serialization.JavaScriptSerializer jSerialize = new System.Web.Script.Serialization.JavaScriptSerializer();

                if (jsonString != null)
                {
                    try
                    {
                        var subscription = BaseClass.RetrieveSubscription(jsonString);
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