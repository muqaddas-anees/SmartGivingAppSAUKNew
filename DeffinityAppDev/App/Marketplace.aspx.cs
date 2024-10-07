using PortfolioMgt.DAL;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev
{
    public partial class Marketplace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindListView();
        }
        private void BindListView()
        {
            using (var c = new PortfolioDataContext())
            {
                int sizeOfCurrentCharity = GetSize();

                // Get the size range based on the current size
                var range = c.CharitySizeRanges
                    .FirstOrDefault(o => o.FromRange <= sizeOfCurrentCharity && o.ToRange >= sizeOfCurrentCharity);

                // Get available marketplace products
                var marketPlaceProducts = c.MarketplaceProducts
                    .Where(o => o.IsModuleAvailable == true)
                    .ToList();
                List<Products> produces=new List<Products>();
                List<Products> Boughtproduces = new List<Products>();
                // Modify the list as needed
                for (int i=0;i < marketPlaceProducts.Count();i++)
                {
                    string url="";

                    // Example modification: adjust the price based on the size range
                    if (range != null)
                    {
                        if (range.Size == "small")
                        {
                            url = marketPlaceProducts[i].UrlForSmall; // Use URL for small
                                                       // Apply any additional logic for small if needed
                        }
                        else if (range.Size == "medium")
                        {
                            url = marketPlaceProducts[i].UrlForMedium; // Use URL for medium
                        }
                        else if (range.Size == "large")
                        {
                            url = marketPlaceProducts[i].UrlForLarge; // Use URL for large
                        }
                 
                        
                        var Product = new Products
                        {
                            Id = marketPlaceProducts[i].Id,
                            Title = marketPlaceProducts[i].Title,
                            Description = marketPlaceProducts[i].Description,
                            buyText= marketPlaceProducts[i].textforbuynowbutton,
                            videoText= marketPlaceProducts[i].textforvideobutton,
                            TrialLink = url,
                            VideoLink = marketPlaceProducts[i].VideoDescriptionUrl,
                            ImageUrl = $"/imagehandler.ashx?id={marketPlaceProducts[i].Id}&s={ImageManager.file_section_marketplace}"
                        };
                        var boughtproducts = c.BoughtMarketplaceProducts.FirstOrDefault(o => o.ProductID == marketPlaceProducts[i].Id);
                        if (boughtproducts != null)
                        {
                            Boughtproduces.Add(Product);
                        }
                        else { 
                        produces.Add(Product);
                        }
                    }

                    // You can also add other modifications here
                    // product.SomeOtherProperty = newValue;
                }

                // Now you can bind the modified list to the ListView
                lvCards.DataSource = produces;
                lvCards.DataBind();


                lv_BoughtCards.DataSource = Boughtproduces;
                lv_BoughtCards.DataBind();
            }

        }
        protected void lvCards_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try { 
            if (e.CommandName == "BuyNow")
            {
                // Get the Id from CommandArgument
                int serviceId = Convert.ToInt32(e.CommandArgument);

                // Perform actions based on the selected service ID
                // For example, redirect to a buy now page or process payment
                Response.Redirect($"BuyNow.aspx?ServiceId={serviceId}");
            }
            }catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private int GetSize()
        {
            using (var c = new UserDataContext())
            {
                var count = c.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Count();
                return count;
            }
            
        }
        protected void BtnOpen_Command(object sender, CommandEventArgs e)
        {
            // Command logic based on the CommandName
            switch (e.CommandName)
            {
                case "PortalBranding":
                    // Add your logic for the PortalBranding command
                    OpenPortalBranding();
                    break;

                // You can add more cases here for other buttons with different CommandNames if needed

                default:
                    break;
            }
        }

        private void OpenPortalBranding()
        {
            // Logic for opening portal branding (or any other functionality you need)
            Response.Redirect("~/PortalBranding.aspx"); // Redirect example
        }

        protected void lvCards_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "BuyNow")
                {
                    // Get the Id from CommandArgument
                    int serviceId = Convert.ToInt32(e.CommandArgument);
                    BuyNow(serviceId);
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        private void BuyNow(int ID, bool success)
        {
            try { 
            using (var ucontext = new UserDataContext())
            using (var context = new PortfolioDataContext())
            {
                    string stripeApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
                    string stripePublishableApiKey = ConfigurationManager.AppSettings["StripePublishableKey"];

                    string url = "https://dev.plegit.ai";
                    string stripepaymenturl = "";
                    var product = context.MarketplaceProducts.FirstOrDefault(o => o.Id == ID);
                int sizeOfCurrentCharity = GetSize();

                // Get the size range based on the current size
                var range = context.CharitySizeRanges
                    .FirstOrDefault(o => o.FromRange <= sizeOfCurrentCharity && o.ToRange >= sizeOfCurrentCharity);

                double price = 0; // Initialize price
                    string currency = "usd";
                if (range != null)
                {
                    if (range.Size.ToLower() == "small")
                    {
                            currency = product.CurrencyForSmallCharities;
                        price = double.Parse(product.PriceForSmallCharities.ToString()); // Convert to double
                            stripepaymenturl = product.UrlForSmall;
                    }
                    else if (range.Size.ToLower() == "medium")
                    {
                            currency = product.CurrencyForMediumCharities;
                            price = double.Parse(product.PriceForSmallCharities.ToString()); // Convert to double
                            stripepaymenturl = product.UrlForMedium;
                        }
                    else if (range.Size.ToLower() == "large")
                    {
                            currency = product.CurrencyForLargeCharities;
                            price = double.Parse(product.PriceForSmallCharities.ToString()); // Convert to double
                            stripepaymenturl = product.UrlForLarge;
                        }
                }
                    Response.Write("Price:" + price);
                // Set Stripe API Key
                StripeConfiguration.ApiKey = stripeApiKey;

                var user = ucontext.Contractors.FirstOrDefault(o => o.ID == sessionKeys.UID);

                // Create a product if it doesn't exist
                var stripeProductService = new ProductService();
                var stripeProducts = stripeProductService.List();

                var stripeProduct = stripeProducts.FirstOrDefault(p => p.Name == product.Title); // Check by title

                if (stripeProduct == null)
                {
                    // Create a new product if it doesn't exist
                    stripeProduct = stripeProductService.Create(new ProductCreateOptions
                    {
                        Name = product.Title,
                        Description = product.Description,
                        Type = "service", // Assuming this is a service
                    });
                }

                // Create a Price for the subscription (monthly)
                var priceService = new PriceService();
                var stripePrice = priceService.Create(new PriceCreateOptions
                {
                    UnitAmount = (long)(price * 100), // Stripe requires amount in cents
                    Currency = currency, // Change to your required currency
                    Recurring = new PriceRecurringOptions
                    {
                        Interval = "month", // Monthly charge
                        TrialPeriodDays = product.TrialPeriod??0,
                    },
                    Product = stripeProduct.Id,
                    
                });

                // Create a Checkout Session with metadata
                var sessionService = new SessionService();
                var sessionOptions = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = stripePrice.Id,
                    Quantity = 1,
                },
            },
                    Mode = "subscription",
                    
                    SuccessUrl = $"{url}/PaySuccess.aspx?uid={user.ID}&pid={product.Id}&price={price}", // Replace with your success URL
                    CancelUrl = $"{url}/app/marketplace.aspx", // Replace with your cancel URL
                    Metadata = new Dictionary<string, string>
            {
                { "UserId", user.ID.ToString() } // Add user ID to metadata
            },
                };
                    string successURL = $"{url}/PaySuccess.aspx?uid={user.ID}&pid={product.Id}&price={price}";
                var session = sessionService.Create(sessionOptions);
                    string redirectUrl = $"{stripepaymenturl}?sessionId={session.Id}&successUrl={successURL}&cancelUrl=https://dev.plegit.ai/cancel";
                // Redirect to the checkout URL
               Response.Redirect(redirectUrl);
            }
            }catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        string stripeApiKey = "sk_test_51PGlrNGzv4qSCbkBuyXuul65KHEZ97wThTwdXwb7uTiyuUkfkwf58xRcN3n8KxXmbEtZckXguKjxFHnPxIuS5ZA300JD4kOxbg";
        string stripePublishableApiKey = "pk_test_51PGlrNGzv4qSCbkB8m9lJUs6u578E4vyWXg4Wj65zbokvveztQo3nH0LLWFVmpX4SdUjmbPcWjjBY13yLDWeggxa00tLaNsvJ8";
        private void BuyNow(int ID)
        {
            try
            {
                using (var ucontext = new UserDataContext())
                using (var context = new PortfolioDataContext())
                {
                    string url = "https://dev.plegit.ai";
                    var product = context.MarketplaceProducts.FirstOrDefault(o => o.Id == ID);
                    int sizeOfCurrentCharity = GetSize();

                    // Get the size range based on the current size
                    var range = context.CharitySizeRanges
                        .FirstOrDefault(o => o.FromRange <= sizeOfCurrentCharity && o.ToRange >= sizeOfCurrentCharity);

                    double price = 0; // Initialize price
                    string currency = "usd";
                    string priceid = "";
                    if (range != null)
                    {
                        if (range.Size.ToLower() == "small")
                        {
                            currency = product.CurrencyForSmallCharities;
                            price = double.Parse(product.PriceForSmallCharities.ToString()); // Convert to double
                            priceid = product.UrlForSmall;
                        }
                        else if (range.Size.ToLower() == "medium")
                        {
                            currency = product.CurrencyForMediumCharities;
                            price = double.Parse(product.PriceForMediumCharities.ToString()); // Convert to double
                            priceid = product.UrlForMedium;
                        }
                        else if (range.Size.ToLower() == "large")
                        {
                            currency = product.CurrencyForLargeCharities;
                            price = double.Parse(product.PriceForLargeCharities.ToString()); // Convert to double
                            priceid = product.UrlForLarge;
                        }
                    }
                    var user = ucontext.Contractors.FirstOrDefault(o => o.ID == sessionKeys.UID);

                    if (price==0)
                    {
                        Response.Redirect($"{url}/PaySuccess.aspx?uid={user.ID}&pid={product.Id}&price={price}");
                    }
                    if(String.IsNullOrEmpty(priceid))
                    {
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Oops! There seems to be an issue. We are working to get it fixed.");
                        return;
                    }
                    Response.Write("Price: " + price);

                    // Set Stripe API Key
                    StripeConfiguration.ApiKey = stripeApiKey;


                    // Skip creating a new price, use the existing price ID
                    var sessionService = new SessionService();
                    var sessionOptions = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceid, // Use the existing price ID
                        Quantity = 1,
                    },
                },
                        Mode = "subscription",
                        SuccessUrl = $"{url}/PaySuccess.aspx?uid={user.ID}&pid={product.Id}&price={price}", // Success URL
                        CancelUrl = $"{url}/app/marketplace.aspx", // Cancel URL
                        Metadata = new Dictionary<string, string>
                {
                    { "user_id", user.ID.ToString() }, // Add user ID to metadata
                },
                    };

                    var session = sessionService.Create(sessionOptions);

                    // Redirect to the Stripe Checkout session URL
                    Response.Redirect(session.Url);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

    }

    class Products
    {
        public int Id { get; set; }
        public string TrialLink { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string buyText { get;set; }
        public string videoText { get;set; }
    }
}

