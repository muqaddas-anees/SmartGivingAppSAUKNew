using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class PaySuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            // Get userID, portfolioID, and productID from query and session
            var userId = int.Parse(Request.QueryString["uid"]);      // Get userID from query string
            var portfolioId = sessionKeys.PortfolioID;               // Get portfolioID from session
            var productId = int.Parse(Request.QueryString["pid"]);   // Get productID from query string
            double price = double.Parse(Request.QueryString["price"]); // Get price from query string

            using (var context = new PortfolioDataContext())
            {
                // Fetch the product from the MarketplaceProducts table
                var product = context.MarketplaceProducts.FirstOrDefault(o => o.Id == productId);
                if (product == null)
                {
                    // Handle the case where the product is not found
                    Response.Write("Product not found.");
                    return;
                }

                // Create new BoughtMarketplaceProduct record
                var boughtproduct = new BoughtMarketplaceProduct
                {
                    UserID = userId,
                    PortfolioID = portfolioId,
                    ProductID = productId,
                    Price = (decimal)price,       // Explicit cast from double to decimal
                    BuyDate = DateTime.Now,
                    TrialPeriod = product.TrialPeriod
                };

                // Insert the new purchase record into the BoughtMarketplaceProducts table
                context.BoughtMarketplaceProducts.InsertOnSubmit(boughtproduct);

                // Check if active services for the portfolio already exist
                var activeservices = context.PortfolioActiveProducts.FirstOrDefault(o => o.PortfolioID == portfolioId);
                if (activeservices == null)
                {
                    // Create a new active services record if none exists
                    activeservices = new PortfolioActiveProduct
                    {
                        UserID = userId,
                        PortfolioID = portfolioId
                    };

                    // Insert the new active services record
                    context.PortfolioActiveProducts.InsertOnSubmit(activeservices);
                }

                // Update the active services based on the product features
                if (product.IsProjectManagement ?? false)
                    activeservices.IsProjectManagement = true;
                if (product.IsAcademy ?? false)
                    activeservices.IsAcademy = true;
                if (product.IsAI ?? false)
                    activeservices.IsAI = true;
                if (product.IsBeneficiaryManagement ?? false)
                    activeservices.IsBeneficiaryManagement = true;
                if (product.IsLivestream ?? false)
                    activeservices.IsLivestream = true;
                if (product.IsOnlineShop ?? false)
                    activeservices.IsOnlineShop = true;
                if (product.IsPeerToPeerFundraising ?? false)
                    activeservices.IsPeerToPeerFundraising = true;

                // Commit all changes to the database
                context.SubmitChanges();
            }

            // Optional: Redirect or give feedback after saving
            Response.Redirect("App/Dashboard.aspx");
        }

    }
}