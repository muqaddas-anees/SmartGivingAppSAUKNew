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
    public partial class Wordpress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSettings();
            }
        }
        private void BindSettings()
        {
            using (var context = new PortfolioDataContext())
            {
                var settings = context.WordpressSettings.FirstOrDefault();

                if (settings != null)
                {
                    // Fill the TextBox with the current DonateButtonColor value
                    txtDonateButtonColor.Text = settings.DonateButtonColor;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {

                    var settings = context.WordpressSettings.FirstOrDefault();

                    if (settings != null)
                    {
                        // Update existing settings
                        settings.DonateButtonColor = txtDonateButtonColor.Text;

                    }
                    else
                    {
                        // Create a new settings record if none exists
                        settings = new WordpressSetting
                        {
                            DonateButtonColor = txtDonateButtonColor.Text,
                            // Set other properties if needed
                        };
                        context.WordpressSettings.InsertOnSubmit(settings);
                    }

                    // Save changes to the database
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }
        

    }
}