using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DeffinityAppDev.App.stripe
{
    public partial class Charge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Form["stripeToken"] != null)
                {
                    //var customers = new CustomerService();
                    //var charges = new ChargeService();

                    //var customer = customers.Create(new CustomerCreateOptions
                    //{
                    //    Email = Request.Form["stripeEmail"],
                    //    Source = Request.Form["stripeToken"]
                    //});

                    //var charge = charges.Create(new ChargeCreateOptions
                    //{
                    //    Amount = 500,
                    //    Description = "Sample Charge",
                    //    Currency = "usd",
                    //    Customer = customer.Id
                    //});

                    //Console.WriteLine(charge);


                    //var r = StripeWebApp.Methods.BaseClass.CreateCustomercharges("First name", Request.Form["stripeEmail"], Request.Form["stripeToken"]);
                    //LogExceptions.LogException(r.Email);


                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}