using PortfolioMgt.DAL;
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
            BindGrid();
        }
        private void BindGrid()
        {
            using (var context = new PortfolioDataContext())
            {
                // Retrieve all Marketplace Services from the database
                var services = context.MarketplaceServices.ToList();

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
                    var service = context.MarketplaceServices.SingleOrDefault(s => s.Id == serviceID);

                    if (service != null)
                    {
                        // Remove the service from the context
                        context.MarketplaceServices.DeleteOnSubmit(service);

                        // Submit the changes to the database
                        context.SubmitChanges();
                    }
                }

                // Re-bind the grid to reflect the changes
                BindGrid();
            }
        }

    }
}