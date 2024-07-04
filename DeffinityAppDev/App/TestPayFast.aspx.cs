using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayFast;
using PayFast.AspNet;

namespace DeffinityAppDev.App
{
    public partial class TestPayFast : System.Web.UI.Page
    {
         PayFastSettings payFastSettings;
       

        protected void Page_Load(object sender, EventArgs e)
        {
           // PayFast.
            try
            {
                this.payFastSettings = new PayFastSettings();
                this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
                this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
                this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
                this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
                this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
                this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
                this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
                this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];



                var onceOffRequest = new PayFastRequest("plegitsaltpass");

                // Merchant Details
                onceOffRequest.merchant_id = "10028239";
                onceOffRequest.merchant_key = "e6y8289zjke4t";
                onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
                //onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
                //onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

                // Buyer Details
                onceOffRequest.email_address = "k.indrasenareddy@gmail.com";

                // Transaction Details
                onceOffRequest.m_payment_id = Guid.NewGuid().ToString();
                onceOffRequest.amount = 30;
                onceOffRequest.item_name = "Once off option";
                onceOffRequest.item_description = "Some details about the once off payment";
                
                // Transaction Options
                onceOffRequest.email_confirmation = true;
                onceOffRequest.confirmation_address = "k.indrasenareddy@gmail.com";
                var signature  = onceOffRequest.signature;

                //payFastSettings.



                //  var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";
                var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

                Response.Redirect(redirectUrl,false);

                // onceOffRequest.SetPassPhrase();








                //PayFast pf = new PayFast();

                //// Set the merchant ID and merchant key
                //pf.MerchantId = "YOUR_MERCHANT_ID";
                //pf.MerchantKey = "YOUR_MERCHANT_KEY";

                //// Set the payment details
                //pf.Amount = Convert.ToDouble(txtAmount.Text);
                //pf.ItemName = txtItemName.Text;
                //pf.EmailAddress = txtEmailAddress.Text;

                //// Set the return URL
                //pf.ReturnUrl = "http://localhost:1234/ThankYou.aspx";

                //// Generate the payment URL
                //string paymentUrl = pf.GeneratePaymentUrl();

                //// Redirect the user to the payment URL
                //Response.Redirect(paymentUrl);











            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}