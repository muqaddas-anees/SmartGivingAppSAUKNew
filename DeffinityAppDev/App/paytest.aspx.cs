using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class paytest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

        }

        
        
        public void PaymentProcess(PaySettings settings)
        {


            string site = settings.IsLive ? "https://www.payfast.co.za/eng/process?" : "https://sandbox.payfast.co.za/eng/process?";
            string merchant_id = settings.MerchantID;
            string merchant_key = settings.MerchantKey;

            // Build the query string for payment site

            StringBuilder strHashed = new StringBuilder();

            // strHashed.AppendFormat("passphrase={0}&", UrlEncodeUpper(settings.passphrase));
            strHashed.AppendFormat("merchant_id={0}&", UrlEncodeUpper(merchant_id));
            strHashed.AppendFormat("merchant_key={0}&", UrlEncodeUpper(merchant_key));
            if (!string.IsNullOrEmpty(settings.URLReturn))
                strHashed.AppendFormat("return_url={0}&", UrlEncodeUpper(settings.URLReturn)); // Just thank the user and tell them you are processing their order (should already be done or take a few more seconds with ITN)
            if (!string.IsNullOrEmpty(settings.URLCancel))
                strHashed.AppendFormat("cancel_url={0}&", UrlEncodeUpper(settings.URLCancel)); // Just thank the user and tell them that they cancelled the order (encourage them to email you if they have problems paying
            if (!string.IsNullOrEmpty(settings.URLNotify))
                strHashed.AppendFormat("notify_url={0}&", UrlEncodeUpper(settings.URLNotify)); // Called once by the payment processor to validate

            //if (!string.IsNullOrEmpty(settings.FirstName))
            //    strHashed.AppendFormat("name_first={0}&", UrlEncodeUpper(settings.FirstName));
            //if (!string.IsNullOrEmpty(settings.LastName))
            //    strHashed.AppendFormat("name_last={0}&", UrlEncodeUpper(settings.LastName));
            if (!string.IsNullOrEmpty(settings.Email))
                strHashed.AppendFormat("email_address={0}&", UrlEncodeUpper(settings.Email));
            //if (!string.IsNullOrEmpty(settings.CellNumber))
            //    strHashed.AppendFormat("cell_number={0}&", UrlEncodeUpper(settings.CellNumber));

            if (!string.IsNullOrEmpty(settings.OrderId))
                strHashed.AppendFormat("m_payment_id={0}&", UrlEncodeUpper(settings.OrderId));

            strHashed.AppendFormat("amount={0}&", UrlEncodeUpper(settings.Amount.ToString()));
            strHashed.AppendFormat("item_name={0}&", UrlEncodeUpper(settings.ItemName));

            if (!string.IsNullOrEmpty(settings.Description))
                strHashed.AppendFormat("item_description={0}&", UrlEncodeUpper(settings.Description));

            var setup = "";
           if (!string.IsNullOrEmpty(settings.setup))
                setup = "&setup="+settings.setup;
            //  strHashed.AppendFormat("payment_method={0}", UrlEncodeUpper("cc"));

            var temp = $"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim() + setup;

            Response.Redirect($"{site}{strHashed.ToString()}signature={CreateHash(strHashed, settings.passphrase)}".Trim()+ setup, false);
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

        protected void btnPay_Click(object sender, EventArgs e)
        {

            string id = Guid.NewGuid().ToString();

            var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(sessionKeys.PortfolioID);
            PaySettings ps = new PaySettings();
            ps.MerchantID = payDetials.Vendor; 
            ps.MerchantKey = payDetials.consumerSecret;
            ps.passphrase = payDetials.consumerKey??"";
            ps.Amount = "10.00";
            ps.CellNumber = "0823456789";
            ps.Description = "demoitem";
            ps.Email = "indra@emsysindia.com";
            ps.FirstName = "Demo";
            ps.IsLive = false;
            ps.ItemName = "Demo";
            ps.LastName = "D";
            ps.OrderId = "100001";
            ps.URLReturn = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + id + "&unid=" + id + "&type=" + "success";
            ps.URLCancel = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + id + "&unid=" + id + "&type=" + "booking_cancel";
           
            ps.URLNotify = Deffinity.systemdefaults.GetWebUrl() + "/PayResult.aspx?tunid=" + id + "&unid=" + id + "&type=" + "booking_notify";

        
            var stemp = "{%22merchant_id%22:mid,%22percentage%22:%22pcent%22,%22min%22:%221%22,%22max%22:%22100000%22}";

            stemp = stemp.Replace("mid", payDetials.Vendor);
            stemp = stemp.Replace("pcent", "10");

            var temp1 = "{%22split_payment%22:" + stemp + "}";

            ps.setup = temp1;
            // ps.setup = JsonConvert.SerializeObject(sp);
            IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
            var pDetails = prep.GetAll().First();


            var str_split = "";


            PaymentProcess(ps);
        }


        
    }
}