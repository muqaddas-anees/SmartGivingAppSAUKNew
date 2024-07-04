using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
using Stripe;

namespace StripeWebApp.Methods
{
    public class AppSubscription
    {
        public ChargeBillingDetails BillingDetails { get; set; }
        public String PaymentMethodId { get; set; }
        public String PriceId { get; set; }
        public long Quantity { get; set; }
    }

    public class BaseClass
    {


        public static Customer CreateCustomer(ChargeBillingDetails billingDetails, string paymentMethodId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var customerOptions = new CustomerCreateOptions
            {
                Name = billingDetails.Name,
                Email = billingDetails.Email,
                
            };
            var customerService = new CustomerService();
            var customer = customerService.Create(customerOptions);

            UpdateCustomer(customer.Id, paymentMethodId);

            return customer;
        }
        public static void UpdateCustomer(string customerId, string paymentMethodId)
        {

            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var paymentMethodAttachOptions = new PaymentMethodAttachOptions
            {
                Customer = customerId,
            };
            var paymentMethodservice = new PaymentMethodService();
            paymentMethodservice.Attach(paymentMethodId, paymentMethodAttachOptions);

            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethodId,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(customerId, customerOptions);
        }


        public static Charge CreateCustomercharges(string name, string email, string cardnumber,string expmonth,string expyear,string cvv,string amount)
        {

            var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.IsActive == true).FirstOrDefault();
            //StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

            //CreditCardOptions card = new TokenCardOptions();
            //card.Name = cardOnName;
            //card.Number = cardNumber;
            //card.ExpYear = cardYear;
            //card.ExpMonth = cardMonth;
            //card.Cvc = cardCVV;

            //#region Strip token
            //CreditCardOptions card = new Stripe.CreditCardOptions();
            //Stripe.CreditCard card = new CreditCard();
            //card.Name = "demo";
            //card.Number = "4242 4242 4242 4242";
            //card.ExpYear = 23;
            //card.ExpMonth = 1;
            //card.Cvc = "123";
            ////Assign Card to Token Object and create Token  
            ///
            string Strieppublickey = "";
            string striepSecretKey = "";


            Strieppublickey = d.consumerKey; //ConfigurationManager.AppSettings["StripePublishKey"].ToString();
            striepSecretKey = d.consumerSecret; //ConfigurationManager.AppSettings["StripeSecretKey"].ToString();


            Stripe.StripeConfiguration.SetApiKey(Strieppublickey);
            Stripe.StripeConfiguration.ApiKey = striepSecretKey;
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = cardnumber,
                    ExpMonth = expmonth,
                    ExpYear =  expyear.Substring(expyear.Length - 2),
                    Cvc = cvv,
                },
            };
            //Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            //token.Card = card ;
            Stripe.TokenService serviceToken = new Stripe.TokenService();
            Stripe.Token newToken = serviceToken.Create(options);
            //#endregion


            var customers = new Stripe.CustomerService();
            var charges = new Stripe.ChargeService();

            var customerOptions = new Stripe.CustomerCreateOptions
            {
                Name = name,
                Email = email,
                Source = newToken.Id //source

            };
            var customerService = new Stripe.CustomerService();
            var customer = customerService.Create(customerOptions);

            // UpdateCustomer(customer.Id, paymentMethodId);

            var chargeOptions = new Stripe.ChargeCreateOptions
            {
                Customer = customer.Id,
                Description = "Custom t-shirt",
                Amount = Convert.ToInt64( (Convert.ToDouble(amount))*100),
                Currency = "GBP",
            };
            Charge charge = charges.Create(chargeOptions);


           

            return charge;
        }
        public static Subscription CreateSubscription(string priceId, string customerId, long quantity)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
                    {
                        new SubscriptionItemOptions
                        {
                            Price = priceId,
                            Quantity = quantity, 
                        },
                    },
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");

            var subscriptionService = new SubscriptionService();
            Subscription subscription = subscriptionService.Create(subscriptionOptions);
            return subscription;
        }
        public static Subscription RetrieveSubscription(string subscriptionId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var subscriptionService = new SubscriptionService();
            return subscriptionService.Get(subscriptionId);
        }



        //for your future usage
        public static Product CreateProduct(string id, string name, string description)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var productService = new ProductService();
            var productCreateOptions = new ProductCreateOptions
            {
                Id = id,
                Name = name,
                Description = description,
            };
            var product = productService.Create(productCreateOptions);

            return product;
        }
        public static Product RetrieveProduct(string productId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var productService = new ProductService();
            return productService.Get(productId);
        }
        public static Price CreatePrice(string productId, string currency, long unitAmount, string interval)
        {
            var product = RetrieveProduct(productId);

            var priceService = new PriceService();
            var priceCreateOptions = new PriceCreateOptions
            {
                Currency = currency,
                UnitAmount = unitAmount,
                Product = product.Id,
                Recurring = new PriceRecurringOptions
                {
                    Interval = interval, /*month or year*/
                    
                },
            };
            var price = priceService.Create(priceCreateOptions);

            return price;
        }
        public static Price RetrievePrice(string priceId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var priceService = new PriceService();
            return priceService.Get(priceId);
        }
        public static Invoice GetInvoice(string invoiceId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var invoiceOptions = new InvoiceGetOptions();
            invoiceOptions.AddExpand("payment_intent");
            var invoiceService = new InvoiceService();
            Invoice invoice = invoiceService.Get(invoiceId, invoiceOptions);
            return invoice;
        }
        //for your future usage
    }
}



