using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
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
                            Title = marketPlaceProducts[i].Title,
                            Description = marketPlaceProducts[i].Description,
                            buyText= marketPlaceProducts[i].textforbuynowbutton,
                            videoText= marketPlaceProducts[i].textforvideobutton,
                            TrialLink = url,
                            VideoLink = marketPlaceProducts[i].VideoDescriptionUrl,
                            ImageUrl = $"/imagehandler.ashx?id={marketPlaceProducts[i].Id}&s={ImageManager.file_section_marketplace}"
                        };
                        produces.Add(Product);
                    }

                    // You can also add other modifications here
                    // product.SomeOtherProperty = newValue;
                }

                // Now you can bind the modified list to the ListView
                lvCards.DataSource = produces;
                lvCards.DataBind();
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
    }
  
    class Products
    {

        public string TrialLink { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string buyText { get;set; }
        public string videoText { get;set; }
    }
}

