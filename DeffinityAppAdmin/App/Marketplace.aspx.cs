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
    public partial class Marketplace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindSize();
            }
        }
        private void BindSize()
        {
            using (var c = new PortfolioDataContext())
            {
                // Fetch the size ranges from the database
                var sizes = c.CharitySizeRanges.ToList();

                // Assuming the size ranges are categorized by 'Size' (Small, Medium, Large)
                foreach (var size in sizes)
                {
                    if (size.Size == "Small")
                    {
                        txtSmallFrom.Text = size.FromRange.ToString();
                        txtSmallTo.Text = size.ToRange.ToString();
                    }
                    else if (size.Size == "Medium")
                    {
                        txtMediumFrom.Text = size.FromRange.ToString();
                        txtMediumTo.Text = size.ToRange.ToString();
                    }
                    else if (size.Size == "Large")
                    {
                        txtLargeFrom.Text = size.FromRange.ToString();
                        txtLargeTo.Text = size.ToRange.ToString();
                    }
                }
            }
        }

        private void BindGrid()
        {
            using (var context = new PortfolioDataContext())
            {
                // Retrieve all Marketplace Services with the updated columns
                var services = context.MarketplaceProducts.Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Description,
                    s.CurrencyForSmallCharities,
                    s.PriceForSmallCharities,
                    s.CurrencyForMediumCharities,
                    s.PriceForMediumCharities,
                    s.CurrencyForLargeCharities,
                    s.PriceForLargeCharities,
                    s.CurrencyForAnnualDiscount,
                    s.AnnualDiscount,
                    s.VideoDescriptionUrl,
                    s.TrialPeriod,
                    s.IsModuleAvailable
                }).ToList();

                // Bind the data to the GridView
                gvMarketplaceServices.DataSource = services;
                gvMarketplaceServices.DataBind();
            }
        }

        protected void gvMarketplaceServices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditService")
            {
                // Retrieve the ID of the service to be edited
                int serviceID = Convert.ToInt32(e.CommandArgument);

                // Redirect to AddService.aspx with the service ID in the query string
                Response.Redirect($"AddService.aspx?MID={serviceID}");
            }
            else if (e.CommandName == "DeleteService")
            {
                // Retrieve the ID of the service to be deleted
                int serviceID = Convert.ToInt32(e.CommandArgument);

                using (var context = new PortfolioDataContext())
                {
                    // Find the service with the given ID
                    var service = context.MarketplaceProducts.SingleOrDefault(s => s.Id == serviceID);

                    if (service != null)
                    {
                        // Remove the service from the context
                        context.MarketplaceProducts.DeleteOnSubmit(service);

                        // Submit the changes to the database
                        context.SubmitChanges();
                    }
                }

                // Re-bind the grid to reflect the changes
                BindGrid();
            }
        }

        protected void btnSaveSize_Click(object sender, EventArgs e)
        {
            using (var c = new PortfolioDataContext())
            {
                // Update existing size ranges or add new ones
                UpdateSizeRange(c, "Small", txtSmallFrom.Text.Replace(",",""), txtSmallTo.Text.Replace(",", ""));
                UpdateSizeRange(c, "Medium", txtMediumFrom.Text.Replace(",", ""), txtMediumTo.Text.Replace(",", ""));
                UpdateSizeRange(c, "Large", txtLargeFrom.Text.Replace(",", ""), txtLargeTo.Text.Replace(",", ""));

                // Save changes to the database
                c.SubmitChanges();
                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfuly!");
            }
        }

        // Helper method to update or insert size ranges
        private void UpdateSizeRange(PortfolioDataContext context, string size, string fromRangeText, string toRangeText)
        {
            // Parse the from and to range values
            if (int.TryParse(fromRangeText, out int fromRange) && int.TryParse(toRangeText, out int toRange))
            {
                // Check if the size range exists
                var existingRange = context.CharitySizeRanges.FirstOrDefault(s => s.Size == size);

                if (existingRange != null)
                {
                    // Update existing size range
                    existingRange.FromRange = fromRange;
                    existingRange.ToRange = toRange;
                }
                else
                {
                    // Insert new size range
                    var newSizeRange = new CharitySizeRange
                    {
                        Size = size,
                        FromRange = fromRange,
                        ToRange = toRange
                    };
                    context.CharitySizeRanges.InsertOnSubmit(newSizeRange);
                }
            }
            else
            {
                // Handle invalid input (optional)
                // You could show an error message here if needed
                // Example: Display a message on the page indicating invalid input
            }
        }

    }
}