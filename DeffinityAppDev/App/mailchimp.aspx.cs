using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class mailchimp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMailChimpSettings();

        }
        private void LoadMailChimpSettings()
        {
            using (var db = new PortfolioDataContext())
            {
                // Check if settings already exist for the user
                var settings = db.MailchimpSettings.Where(o => o.UserId == sessionKeys.UID).FirstOrDefault();
                if (settings != null)
                {
                    // Load existing settings into form fields
                    if (!string.IsNullOrEmpty(settings.ApiKey))
                    {
                        txtApiKey.Text = settings.ApiKey;
                    }

                    if (!string.IsNullOrEmpty(settings.TeamId))
                    {
                        txtTeamId.Text = settings.TeamId;
                    }

                    // Handle other fields if necessary
                    // Example:
                    // if (settings.PortfolioId != null)
                    // {
                    //     // Populate related fields or handle as needed
                    // }
                }
            }
        }


        protected void savesttings_Click(object sender, EventArgs e)
        {
            string apikey = txtApiKey.Text;
            string teamid = txtTeamId.Text;

            using (var db = new PortfolioDataContext())
            {
                // Check if settings already exist for the user
                var settings = db.MailchimpSettings.Where(o => o.UserId == sessionKeys.UID).FirstOrDefault();
                if (settings != null)
                {
                    // Update existing settings
                    settings.ApiKey = apikey;
                    settings.TeamId = teamid;
                    db.SubmitChanges();
                }
                else
                {
                    // Create new settings record
                    var newSettings = new MailchimpSetting
                    {
                        ApiKey = apikey,
                        TeamId = teamid,
                        UserId = sessionKeys.UID,
                        PortfolioId = sessionKeys.PortfolioID,  // Assuming PortfolioId is available in session or context
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    db.MailchimpSettings.InsertOnSubmit(newSettings);
                    db.SubmitChanges();
                }
            }
            DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Updated Successfully. You can now synchronise your contacts with Mailchimp.", "OK");
        }

    }
}