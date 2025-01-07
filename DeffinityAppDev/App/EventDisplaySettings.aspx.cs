using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class EventDisplaySettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetColourSettings();
            }
        }
        private void SetColourSettings()
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {
                    var colourSettings = context.ViewOptions.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);
                    if (colourSettings == null)
                        return;

                    // Bind List View Options
                    txtListHeaderBackgroundColour.Text = colourSettings.ListHeaderBackgroundColour;
                    txtListHeaderFontSize.Text = colourSettings.ListHeaderFontSize;
                    txtListHeaderFontColour.Text = colourSettings.ListHeaderFontColour;
                    txtListTimeSlotBackgroundColour.Text = colourSettings.ListTimeSlotBackgroundColour;
                    txtListTimeSlotFontColour.Text = colourSettings.ListTimeSlotFontColour;
                    txtListTimeSlotFontSize.Text = colourSettings.ListTimeSlotFontSize;
                    txtListEventTitleColour.Text = colourSettings.ListEventTitleColour;
                    txtListEventTitleSize.Text = colourSettings.ListEventTitleSize;
                    txtListEventSubjectColour.Text = colourSettings.ListEventSubjectColour;
                    txtListEventSubjectFontSize.Text = colourSettings.ListEventSubjectFontSize;
                    txtListEventPanelBackgroundColour.Text = colourSettings.ListEventPanelBackgroundColour;
                    txtListDatePickerColour.Text = colourSettings.ListDatePickerColour;

                    // Bind Panel View Options
                    txtPanelBookTicketsButtonColour.Text = colourSettings.PanelBookTicketsButtonColour;
                    txtPanelBookTicketsButtonFontColour.Text = colourSettings.PanelBookTicketsButtonFontColour;
                    txtPanelViewLiveButtonColour.Text = colourSettings.PanelViewLiveButtonColour;
                    txtPanelViewLiveButtonFontColour.Text = colourSettings.PanelViewLiveButtonFontColour;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SaveSettings()
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {
                    // Retrieve existing settings for the portfolio or create a new entry
                    var colourSettings = context.ViewOptions.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);

                    if (colourSettings == null)
                    {
                        // Create new settings if not found
                        colourSettings = new ViewOption
                        {
                            PortfolioID = sessionKeys.PortfolioID
                        };
                        context.ViewOptions.InsertOnSubmit(colourSettings);
                    }

                    // Update List View Options
                    colourSettings.ListHeaderBackgroundColour = txtListHeaderBackgroundColour.Text;
                    colourSettings.ListHeaderFontSize = txtListHeaderFontSize.Text;
                    colourSettings.ListHeaderFontColour = txtListHeaderFontColour.Text;
                    colourSettings.ListTimeSlotBackgroundColour = txtListTimeSlotBackgroundColour.Text;
                    colourSettings.ListTimeSlotFontColour = txtListTimeSlotFontColour.Text;
                    colourSettings.ListTimeSlotFontSize = txtListTimeSlotFontSize.Text;
                    colourSettings.ListEventTitleColour = txtListEventTitleColour.Text;
                    colourSettings.ListEventTitleSize = txtListEventTitleSize.Text;
                    colourSettings.ListEventSubjectColour = txtListEventSubjectColour.Text;
                    colourSettings.ListEventSubjectFontSize = txtListEventSubjectFontSize.Text;
                    colourSettings.ListEventPanelBackgroundColour = txtListEventPanelBackgroundColour.Text;
                    colourSettings.ListDatePickerColour = txtListDatePickerColour.Text;

                    // Update Panel View Options
                    colourSettings.PanelBookTicketsButtonColour = txtPanelBookTicketsButtonColour.Text;
                    colourSettings.PanelBookTicketsButtonFontColour = txtPanelBookTicketsButtonFontColour.Text;
                    colourSettings.PanelViewLiveButtonColour = txtPanelViewLiveButtonColour.Text;
                    colourSettings.PanelViewLiveButtonFontColour = txtPanelViewLiveButtonFontColour.Text;

                    // Save changes to the database
                    context.SubmitChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfully!");
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            
        }
    }
}