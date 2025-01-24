using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DocumentFormat.OpenXml.Office2010.Excel;
using PortfolioMgt.DAL;
using System.Text;
using System.Globalization;
using Deffinity;

namespace DeffinityAppDev.App.controls
{
    public partial class ActivitiesCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindActiviteis();
                    BindListviewActivities();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        private void BindActiviteis()
        {
            try
            {
                // Step 1: Define default values for UI customization
                string defaultBookTicketsButtonColor = "#1E2129";
                string defaultBookTicketsButtonFontColor = "#FFFFFF";
                string defaultViewLiveButtonColor = "#17C653";
                string defaultViewLiveButtonFontColor = "#FFFFFF";

                using(var context=new PortfolioDataContext())
                { 
                // Step 2: Fetch custom values from the database (if available)
                var viewOptions =context.ViewOptions.FirstOrDefault(o=>o.PortfolioID==sessionKeys.PortfolioID);
                string bookTicketsButtonColor = viewOptions?.PanelBookTicketsButtonColour ?? defaultBookTicketsButtonColor;
                string bookTicketsButtonFontColor = viewOptions?.PanelBookTicketsButtonFontColour ?? defaultBookTicketsButtonFontColor;
                string viewLiveButtonColor = viewOptions?.PanelViewLiveButtonColour ?? defaultViewLiveButtonColor;
                string viewLiveButtonFontColor = viewOptions?.PanelViewLiveButtonFontColour ?? defaultViewLiveButtonFontColor;
               
                // Step 3: Fetch activities
                var currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var alist = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll();
                if (sessionKeys.PortfolioID > 0)
                    alist = alist
                                 .Where(o => o.OrganizationID == sessionKeys.PortfolioID);

                // Step 4: Create the return object and include the UI options
                var retval = (from a in alist
                              select new
                              {
                                  a.ActivityCategoryID,
                                  a.ActivitySubCategoryID,
                                  a.Address1,
                                  a.Address2,
                                  a.BookingEndDate,
                                  a.BookingStartDate,
                                  a.CategoryName,
                                  a.City,
                                  a.CreatedDate,
                                  Description = Deffinity.Utility.RemoveHTMLTags(a.Description == null ? "" : a.Description),
                                  a.EndDateTime,
                                  a.Event_Capacity,
                                  a.OrganizationID,
                                  a.OrganizationName,
                                  isInPerson = GetStatus(a.unid),
                                  a.Title,
                                  a.unid,
                                  a.StartDateTime,

                                  // Add UI options to the result
                                  BookTicketsButtonColor = bookTicketsButtonColor,
                                  BookTicketsButtonFontColor = bookTicketsButtonFontColor,
                                  ViewLiveButtonColor = viewLiveButtonColor,
                                  ViewLiveButtonFontColor = viewLiveButtonFontColor
                              }).ToList();

                // Step 5: Bind the data to the ListView
                ListActivites.DataSource = retval.OrderBy(o => o.StartDateTime).ToList();
                ListActivites.DataBind();
            }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        public bool GetStatus(string unid)
        {
            using(var context=new PortfolioDataContext())
            {
                var activity = context.ActivityDetails.FirstOrDefault(o => o.unid == unid);
                if (activity == null)
                {
                    return true;
                }
                else
                {
                    return activity.isInPerson ?? false;
                }
            }
        }
        protected static string GetAddress(object description)
        {
            string retval = "";
            if (description != null)
            {

                if (description.ToString().Length > 300)
                {
                    retval = description.ToString().Substring(0, 290) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        private void BindListviewActivities()
        {
            // Default Style Variables
            string headerBackgroundColor = "#FFFFFF";
            string headerFontSize = "30px";
            string headerFontColor = "#252F4A";

            string timeSlotBackgroundColor = "#DAF6E4";
            string timeSlotFontColor = "#252F4A";
            string timeSlotFontSize = "20px";

            string eventTitleColor = "#252F4A";
            string eventTitleSize = "16px";

            string eventSubjectColor = "#98A0B7";
            string eventSubjectFontSize = "12px";

            string eventPanelBackgroundColor = "#FFFFFF";
            string datePickerColor = "#9CA4BA";

            // Fetch values from database if they exist (assume ViewOptions table exists)
            using (var context = new PortfolioDataContext())
            {
                var viewOptions = context.ViewOptions.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID); // Fetch ViewOptions from the database
                if (viewOptions != null)
                {
                    headerBackgroundColor = viewOptions.ListHeaderBackgroundColour ?? headerBackgroundColor;
                    headerFontSize = $"{viewOptions.ListHeaderFontSize}px";
                    headerFontColor = viewOptions.ListHeaderFontColour ?? headerFontColor;

                    timeSlotBackgroundColor = viewOptions.ListTimeSlotBackgroundColour ?? timeSlotBackgroundColor;
                    timeSlotFontColor = viewOptions.ListTimeSlotFontColour ?? timeSlotFontColor;
                    timeSlotFontSize = $"{viewOptions.ListTimeSlotFontSize}px";

                    eventTitleColor = viewOptions.ListEventTitleColour ?? eventTitleColor;
                    eventTitleSize = $"{viewOptions.ListEventTitleSize}px";

                    eventSubjectColor = viewOptions.ListEventSubjectColour ?? eventSubjectColor;
                    eventSubjectFontSize = $"{viewOptions.ListEventSubjectFontSize}px";

                    eventPanelBackgroundColor = viewOptions.ListEventPanelBackgroundColour ?? eventPanelBackgroundColor;
                    datePickerColor = viewOptions.ListDatePickerColour ?? datePickerColor;
                }
            }

            // Use StringBuilder for better performance
            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine(@"<div style=""width: 100%; border: 1px solid #ddd; box-sizing: border-box;"">");

            // Format the date as "MON 4 NOVEMBER"
            string formattedDate = DateTime.Now.ToString("ddd d MMMM", CultureInfo.InvariantCulture).ToUpper();

            // Header Section
            htmlBuilder.AppendLine($@"
        <div style=""background-color: {headerBackgroundColor}; color: {headerFontColor}; padding: 10px 15px; font-size: {headerFontSize}; font-weight: bold;"">
            {formattedDate}
        </div>");

            // Fetch and display activities
            using (var context = new PortfolioDataContext())
            {
                var currentDate = DateTime.Now.Date;
                var activities = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll()
                                  .Where(o=>o.OrganizationID == sessionKeys.PortfolioID);

                foreach (var activity in activities)
                {
                    string startTime = activity.StartDateTime.ToString("HH:mm"); // Format time
                    string title = HttpUtility.HtmlEncode(activity.Title);
                    string description = Utility.RemoveHTMLTags(activity.Description).Length > 100 ? Utility.RemoveHTMLTags(activity.Description).Substring(0, 100) + "..." : Utility.RemoveHTMLTags(activity.Description);// Keep raw HTML from database
                    string displayOption =  "display: flex;";
                  
                    htmlBuilder.AppendLine($@"
                <div list-data-start-time='{activity.StartDateTime}' list-data-end-time='{activity.EndDateTime}' style=""{displayOption}; border-bottom: 1px solid #ddd; background-color: {eventPanelBackgroundColor};"">
                    <!-- Time Section -->
                    <div style=""background-color: {timeSlotBackgroundColor}; color: {timeSlotFontColor}; width: 80px; text-align: center; padding: 10px; font-size: {timeSlotFontSize};"">
                        <div style=""font-size: {timeSlotFontSize}; font-weight: bold;"">{startTime}</div>
                    </div>
                    <!-- Content Section -->
                    <div style=""flex: 1; padding: 10px; box-sizing: border-box;"">
                        <div style=""font-size: {eventTitleSize}; font-weight: bold; color: {eventTitleColor};"">{title}</div>
                        <div style=""font-size: {eventSubjectFontSize}; color: {eventSubjectColor}; margin-top: 10px;"">{description}</div>
                    </div>
                </div>");
                }
            }

            htmlBuilder.AppendLine("</div>");

            // Bind the generated HTML to the Literal control
            ltrEvents.Text = htmlBuilder.ToString();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindActiviteis();
        }
        protected void listamount_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
           
        }
        protected string GetImmage(object activityid)
        {
            //string retval = string.Empty;
            //if (activityid != null)
            //{
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
            //    {
            //        // retval = "../../WF/UploadData/Events/" + activityid + "/0.png";
            //        retval = "~/WF/UploadData/Events/" + activityid + "/0.png";
            //    }
            //    else
            //    {

            //        retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
            //}
            //else
            //   retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //return retval;
            return "../../ImageHandler.ashx?id=" + activityid + "&s=" + ImageManager.file_section_event; //
        }

        protected void ListActivites_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string v = e.CommandArgument.ToString();
                if (v.Length > 0)
                {

                    Response.Redirect("~/EventDetailsNew.aspx?unid=" + v, false);
                }
                //txtOtherAmount.Text = e.CommandArgument.ToString().Trim();
                // mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}