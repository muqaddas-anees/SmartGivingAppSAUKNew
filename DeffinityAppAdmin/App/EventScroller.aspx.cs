using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class EventScroller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindEventSettings();
        }
        private void BindEventSettings()
        {
            using(var context=new PortfolioDataContext())
            {
                
                var eventsetting = context.EventScrollerSettings.ToList();
                grid_Scrollers.DataSource = eventsetting;
                grid_Scrollers.DataBind();


            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void grid_Scrollers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                // Get the ID of the row to delete
                int id = Convert.ToInt32(e.CommandArgument);

                using (var context = new PortfolioDataContext())
                {
                    // Find the item by ID
                    var itemToDelete = context.EventScrollerSettings.SingleOrDefault(x => x.ID == id);

                    if (itemToDelete != null)
                    {
                        context.EventScrollerSettings.DeleteOnSubmit(itemToDelete);
                        context.SubmitChanges();

                        // Rebind the grid after deletion
                        BindEventSettings();
                    }
                }
            }
        }
    }
}