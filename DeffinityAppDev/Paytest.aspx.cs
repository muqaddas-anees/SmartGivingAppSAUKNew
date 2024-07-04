using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using System.Net;

namespace DeffinityAppDev
{
    public partial class Paytest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
            }
        }

        public  void CancelSubscription()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            // Replace with your actual PayFast credentials
            string merchantId = "10028239";
            string merchantKey = "e6y8289zjke4t";
            string subscriptionId = "1f6506c0-dabb-474d-b7ba-66be801180c5"; // Replace with the actual subscription ID

            // Set the PayFast API endpoint for subscription cancellations
            string apiEndpoint = "https://api.payfast.co.za/subscriptions/"+subscriptionId + "/cancel?testing=true";
            //https://api.payfast.co.za/subscriptions/dc0521d3-55fe-269b-fa00-b647310d760f/cancel 

            // Prepare the request data
            Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"merchant_id", merchantId},
            {"subscription_id", subscriptionId}
        };

            // Generate the signature
            data["signature"] = CalculateMd5Hash($"{data["merchant_id"]}{data["subscription_id"]}{merchantKey}", "plegitsaltpass");

            // Create RestClient
            var client = new RestClient(apiEndpoint);

            // Create request
            var request = new RestRequest(Method.POST);

            // Add parameters to the request
            foreach (var kvp in data)
            {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            // Execute the request
            IRestResponse response = client.Execute(request);

            // Check if the request was successful
            if (response.IsSuccessful)
            {
                // Read and process the API response
                string responseData = response.Content;

                // Assuming the response is in JSON format
                // You may need to adjust this based on the actual response format
                Console.WriteLine("Response: " + responseData);
            }
            else
            {
                // Request failed, handle accordingly
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }

        // Helper method to calculate MD5 hash
        public  string CalculateMd5Hash(string input,string passPhrase)
        {

            var inputStringBuilder = new StringBuilder(input.ToString());
            if (!string.IsNullOrWhiteSpace(passPhrase))
            {
                inputStringBuilder.Append($"passphrase={this.UrlEncode(passPhrase)}");
            }
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(inputStringBuilder.ToString());
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public void PaymentProcess()
        {


            string site =  "https://sandbox.payfast.co.za/eng/process?";
            string merchant_id = "10028239";
            string merchant_key = "e6y8289zjke4t";

            // Build the query string for payment site

            StringBuilder strHashed = new StringBuilder();

            // strHashed.AppendFormat("passphrase={0}&", UrlEncodeUpper(settings.passphrase));
            strHashed.AppendFormat("merchant_id={0}&", UrlEncodeUpper(merchant_id));
            strHashed.AppendFormat("merchant_key={0}&", UrlEncodeUpper(merchant_key));
                strHashed.AppendFormat("return_url={0}&", UrlEncodeUpper("http://localhost:51383/App/Dashboard.aspx")); // Just thank the user and tell them you are processing their order (should already be done or take a few more seconds with ITN)
                strHashed.AppendFormat("cancel_url={0}&", UrlEncodeUpper("http://localhost:51383/App/Dashboard.aspx")); // Just thank the user and tell them that they cancelled the order (encourage them to email you if they have problems paying
           
                strHashed.AppendFormat("notify_url={0}&", UrlEncodeUpper("http://localhost:51383/App/Dashboard.aspx")); // Called once by the payment processor to validate

         
                strHashed.AppendFormat("email_address={0}&", UrlEncodeUpper("k.indrasenareddy@gmail.com"));

            var unid = Guid.NewGuid().ToString();
          
                strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(unid));

            strHashed.AppendFormat("amount={0}&", UrlEncodeUpper("12.65"));
            strHashed.AppendFormat("item_name={0}&", UrlEncodeUpper("demo"));


            strHashed.AppendFormat("subscription_type={0}&", UrlEncodeUpper("1"));
           strHashed.AppendFormat("billing_date={0}&", UrlEncodeUpper(DateTime.Now.ToString("yyyy-MM-dd")));
            //monthly
            var t = "month";
            if (t != "")
            {
                var v = 0;
                if (t.ToLower().Contains("month"))
                    v = 3;
                else if (t.ToLower().Contains("week"))
                    v = 2;
                else if (t.ToLower().Contains("daily"))
                    v = 1;
                //else if (t.ToLower().Contains("daily"))
                //    v = 1;

                strHashed.AppendFormat("frequency={0}&", UrlEncodeUpper(v.ToString()));
            }
            strHashed.AppendFormat("cycles={0}&", UrlEncodeUpper("0"));


            //if (!string.IsNullOrEmpty(settings.payment_method))
            //    strHashed.AppendFormat("payment_method={0}&", UrlEncodeUpper(settings.payment_method));

            //var setup = "";
            //if (!string.IsNullOrEmpty(settings.setup))
            //    setup = "&setup=" + settings.setup;
            ////  strHashed.AppendFormat("payment_method={0}", UrlEncodeUpper("cc"));

            var temp = $"{site}{strHashed.ToString()}signature={CreateHash(strHashed, "plegitsaltpass")}".Trim() + "";

            Response.Redirect($"{site}{strHashed.ToString()}signature={CreateHash(strHashed, "plegitsaltpass")}".Trim() + "", false);
        }

        private string GetMd5(string input)
        {
            StringBuilder sb = new StringBuilder();
            var hash = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }
        Regex _upperUrlEncodeRegex = new Regex(@"%[a-f0-9]{2}");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelSubscription();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            PaymentProcess();
        }
    }
}