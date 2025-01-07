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
    public partial class AddEventScroller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEvents();
                BindSettings();
            }
        }
        private void BindEvents()
        {
            var activities = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o=>o.OrganizationID==sessionKeys.PortfolioID).ToList();
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(activities);
            hfAllEvents.Value = jsonData;



        }
        private void BindSettings()
        {
            // Check if the query string contains a valid MID parameter
            if (int.TryParse(Request.QueryString["MID"], out int scrollerId) && scrollerId > 0)
            {
                using (var context = new PortfolioDataContext())
                {
                    // Retrieve the EventScrollerSetting based on the MID
                    var scrollerSetting = context.EventScrollerSettings.FirstOrDefault(s => s.ID == scrollerId);

                    if (scrollerSetting != null)
                    {
                        // Populate input fields with retrieved values
                        txtName.Value = scrollerSetting.Name;
                        panelHeight.Value = scrollerSetting.Height;
                        panelWidth.Value = scrollerSetting.Width;
                        titleBgColor.Value = scrollerSetting.TitleBackgroundColor;
                        titleTransparency.Value = scrollerSetting.TitleBackgroundTransparency.ToString();
                        titleFontColor.Value = scrollerSetting.TitleFontColor;
                        timeBgColor.Value = scrollerSetting.TimeBackgroundColor;
                        timeTransparency.Value = scrollerSetting.TimeBackgroundTransparency.ToString();
                        timeFontColor.Value = scrollerSetting.TimeFontColor;
                        categoryBgColor.Value = scrollerSetting.CategoryBackgroundColor;
                        catTransparency.Value = scrollerSetting.CategoryBackgroundTransparency.ToString();
                        categoryFontColor.Value = scrollerSetting.CategoryFontColor;
                        // Check if the value from scrollerSetting.EventType exists in the DropDownList
                        if (ddltype.Items.FindByValue(scrollerSetting.EventType) != null)
                        {
                            // Set the selected value of ddltype to the scrollerSetting.EventType
                            ddltype.SelectedValue = scrollerSetting.EventType;
                        }
                        else
                        {
                            // If the value is not found, clear the selection in ddltype
                            ddltype.SelectedValue = "";

                            // Set the value of txtOtherType (TextBox) to scrollerSetting.EventType
                            txtOtherType.Text = scrollerSetting.EventType;

                            // Optionally, make the TextBox visible
                        }


                        // Get associated events and populate the dropdowns
                        var associatedEvents = context.EventsToEventScrollers
                            .Where(es => es.ScrollerID == scrollerId)
                            .Select(es => es.EventID)
                            .ToList();

                        // Store the associated events in the hidden field
                        hfSavedEvents.Value = string.Join(",", associatedEvents);

                        // Populate the first dropdown

                        // Populate additional dropdowns dynamically (if needed)
                        
                    }
                }
            }
        }


        protected void btnsave_Click1(object sender, EventArgs e)
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {
                    EventScrollerSetting newScroller;

                    if (QueryStringValues.MID == 0) // New Scroller
                    {
                        // Create a new EventScrollerSetting
                        newScroller = new EventScrollerSetting
                        {
                            Name = txtName.Value, // Name of the scroller
                            Height = panelHeight.Value, // Panel height
                            Width = panelWidth.Value, // Panel width
                            TitleBackgroundColor = titleBgColor.Value, // Title background color
                            TitleBackgroundTransparency = Convert.ToInt32(titleTransparency.Value), // Title background transparency
                            TitleFontColor = titleFontColor.Value, // Title font color
                            TimeBackgroundColor = timeBgColor.Value, // Time background color
                            TimeBackgroundTransparency = Convert.ToInt32(timeTransparency.Value), // Time background transparency
                            TimeFontColor = timeFontColor.Value, // Time font color
                            CategoryBackgroundColor = categoryBgColor.Value, // Category background color
                            CategoryBackgroundTransparency = Convert.ToInt32(catTransparency.Value), // Category background transparency
                            CategoryFontColor = categoryFontColor.Value, // Category font color
                            EventType = txtOtherType.Text // Event type (e.g., HIGHLIGHTS)
                        };

                        // Insert the new scroller into the database
                        context.EventScrollerSettings.InsertOnSubmit(newScroller);
                        context.SubmitChanges(); // Commit changes to generate an ID
                    }
                    else // Update existing scroller
                    {
                        // Find the existing scroller by its ID
                        newScroller = context.EventScrollerSettings.FirstOrDefault(s => s.ID == QueryStringValues.MID);

                        if (newScroller == null)
                        {
                            throw new Exception("Scroller not found for update.");
                        }

                        // Update the existing scroller properties
                        newScroller.Name = txtName.Value;
                        newScroller.Height = panelHeight.Value;
                        newScroller.Width = panelWidth.Value;
                        newScroller.TitleBackgroundColor = titleBgColor.Value;
                        newScroller.TitleBackgroundTransparency = Convert.ToInt32(titleTransparency.Value);
                        newScroller.TitleFontColor = titleFontColor.Value;
                        newScroller.TimeBackgroundColor = timeBgColor.Value;
                        newScroller.TimeBackgroundTransparency = Convert.ToInt32(timeTransparency.Value);
                        newScroller.TimeFontColor = timeFontColor.Value;
                        newScroller.CategoryBackgroundColor = categoryBgColor.Value;
                        newScroller.CategoryBackgroundTransparency = Convert.ToInt32(catTransparency.Value);
                        newScroller.CategoryFontColor = categoryFontColor.Value;
                        newScroller.EventType =txtOtherType.Text;
                        
                        // No need to call `InsertOnSubmit` since we are updating
                    }

                    // Save associated events in EventsToEventScrollers
                    var selectedEvents = hfSavedEvents.Value
                        .Split(',')
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse)
                        .ToList();

                    // Remove existing mappings for this scroller
                    var existingMappings = context.EventsToEventScrollers
                        .Where(et => et.ScrollerID == newScroller.ID);
                    context.EventsToEventScrollers.DeleteAllOnSubmit(existingMappings);

                    // Add new mappings
                    foreach (var eventId in selectedEvents)
                    {
                        // Check if the relationship already exists
                        bool exists = context.EventsToEventScrollers
                                             .Any(es => es.ScrollerID == newScroller.ID && es.EventID == eventId);

                        // Only insert if it doesn't already exist
                        if (!exists)
                        {
                            var eventToScroller = new EventsToEventScroller
                            {
                                ScrollerID = newScroller.ID, // Use the scroller's ID
                                EventID = eventId
                            };

                            context.EventsToEventScrollers.InsertOnSubmit(eventToScroller);
                        }
                    }
                    // Save changes to the database
                    context.SubmitChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfully!");

                    Response.Redirect($"AddEventScroller.aspx?mid={newScroller.ID}",false);

                    // Redirect to the same page with the scroller ID
                }

            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                LogExceptions.WriteExceptionLog(ex);

                // Optionally, display a friendly error message to the user
            }
        }

    }
}