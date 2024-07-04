using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text;
            int.TryParse(txtExpiryMonth.Text, out int expMonth);
            int.TryParse(txtExpiryYear.Text, out int expYear);
            string cvc = txtCVC.Text;
            decimal.TryParse(txtAmount.Text, out decimal totalAmount);

            // Keys for different accounts
            string primarySecretKey = "sk_test_51M90vESEzdqVp8IHBkqUWoXXAGxuRgSZ2Hc1qvhiIHuHjYyS5mfJj2RWoopNMGVbGj1BlCxWBWBVGy64MRb37zkp00YEKCJwYG";// ConfigurationManager.AppSettings["PrimaryStripeSecretKey"];
            string secondarySecretKey = "sk_test_51P9j0YSAzteR3BT796wIqNTx0j8H9IT73H6n9Nj0D4WX0AJPpLPOtsMmFXccCstALB14V6ro7kK9McHKTje5WhXx00zgjZU0oq";// ConfigurationManager.AppSettings["SecondaryStripeSecretKey"];

            try
            {
                // Process the primary payment (80%)
                decimal primaryAmount = totalAmount * 0.80M; // 80% of the total
                string message1 = ProcessPayment(cardNumber, expMonth.ToString(), expYear.ToString(), cvc, primaryAmount, "usd", primarySecretKey);

                // Process the secondary payment (20%)
                decimal secondaryAmount = totalAmount * 0.20M; // 20% of the total
                string message2 = ProcessPayment(cardNumber, expMonth.ToString(), expYear.ToString(), cvc, secondaryAmount, "usd", secondarySecretKey);

                lblMessage.Text = $"Primary payment: {message1}<br/>Secondary payment: {message2}";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Payment processing failed: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Helper method to process the payment
        private string ProcessPayment(string cardNumber, string expMonth, string expYear, string cvc, decimal amount, string currency, string secretKey)
        {
            StripeConfiguration.ApiKey = secretKey;

            try
            {
                // Create a token with the card details
                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = cardNumber,
                        ExpMonth = expMonth,
                        ExpYear = expYear,
                        Cvc = cvc
                    }
                };

                var tokenService = new TokenService();
                var token = tokenService.Create(tokenOptions);

                // Convert the token to a payment method
                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions { Token = token.Id }
                };


                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = paymentMethodService.Create(paymentMethodOptions);


                // Create the payment intent using the payment method
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt64(amount * 100), // convert to cents
                    Currency = currency,
                    PaymentMethod = paymentMethod.Id,
                    Confirm = true,
                    PaymentMethodTypes = new List<string> { "card" },
                    UseStripeSdk = true // For automatic handling of payment flows that require additional actions
                };

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Create(paymentIntentOptions);


                return "Payment successful";
            }
            catch (StripeException ex)
            {
                return $"Payment failed: {ex.Message}";
            }
        }
    }
}