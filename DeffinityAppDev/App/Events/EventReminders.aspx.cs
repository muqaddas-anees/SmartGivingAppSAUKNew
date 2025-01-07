using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events
{
    public partial class EventReminders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadActivityDetails();
                BindPTags();
            }
        }

        private void LoadActivityDetails()
        {
            var ActivityDetail = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().FirstOrDefault(o=>o.unid==QueryStringValues.UNID);
            if (ActivityDetail != null)
            {
                addressadr.Text= BasicInfo.GetAddress(ActivityDetail.Address1, ActivityDetail.Address2, ActivityDetail.City, ActivityDetail.state_Province, ActivityDetail.Postalcode, ActivityDetail.Country);
                title.Text = ActivityDetail.Title;
                loggedbyname.Text = ActivityDetail.LoggedByName;
                price.Text = ActivityDetail.Price.ToString();
                Date.Text = ActivityDetail.StartDateTime.ToShortDateString();
                addressdesc.Text = GetAddress(ActivityDetail.Description);
                img_user.ImageUrl = GetImageUrl(ActivityDetail.LoggedBy.ToString());
                img_event.ImageUrl = GetImmage(ActivityDetail.unid);


            }
        }
        protected string GetImmage(object activityid)
        {
            //string retval = string.Empty;
            //if (activityid != null)
            //{
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
            //    {
            //        retval = "~/WF/UploadData/Events/" + activityid + "/0.png?v=" + DateTime.Now.Ticks;
            //    }

            //   else
            //    {

            //        retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
            //}
            //else
            //    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //return retval;
            return "~/ImageHandler.ashx?id=" + activityid + "&s=" + ImageManager.file_section_event; //"img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        }

        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected static string GetAddress(string description)
        {
            string retval = "";
            if (description != null)
            {
                if (description.ToString().Length > 500)
                {
                    retval = description.ToString().Substring(0, 490) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        public List<PTags> GetPersonalizationTags()
        {
            List<PTags> tags = new List<PTags>();

            // Adding each tag to the list
            tags.Add(new PTags { Name = "First Name", ID = "{{firstname}}" });
            tags.Add(new PTags { Name = "Last Name", ID = "{{lastname}}" });
            tags.Add(new PTags { Name = "Email Address", ID = "{{emailaddress}}" });
            tags.Add(new PTags { Name = "Event Title", ID = "{{eventtitle}}" });
            tags.Add(new PTags { Name = "Event Date", ID = "{{eventdate}}" });
            tags.Add(new PTags { Name = "Event Physical Location", ID = "{{eventlocation}}" });
            tags.Add(new PTags { Name = "Virtual URL", ID = "{{virtualurl}}" });
            tags.Add(new PTags { Name = "Portal Name", ID = "{{portalname}}" });

            return tags;
        }
        public void BindPTags()
        {
            // Get the data for dropdowns
            var personalizationTags = GetPersonalizationTags();

            // Add the custom item to the Body Tags

            // Bind Body Tags Dropdown
            ddlBodyTags.DataSource = personalizationTags;
            ddlBodyTags.DataTextField = "Name";
            ddlBodyTags.DataValueField = "ID";
            ddlBodyTags.DataBind();

            // Insert "Please Select" at the first position
            ddlBodyTags.Items.Insert(0, new ListItem("Please Select", ""));
            ddlBodyTags.Items.Insert(1, new ListItem("Logo", "{{logo}}"));

            // Bind Subject Tags Dropdown
            ddlSubjectTags.DataSource = GetPersonalizationTags(); // Re-fetch the original data without the custom Logo tag
            ddlSubjectTags.DataTextField = "Name";
            ddlSubjectTags.DataValueField = "ID";
            ddlSubjectTags.DataBind();

            // Insert "Please Select" at the first position
            ddlSubjectTags.Items.Insert(0, new ListItem("Please Select", ""));
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var context = new PortfolioDataContext())
            {
                // Ensure ddlReminderDays.SelectedValue is not null or empty
                if (!string.IsNullOrEmpty(ddlReminderDays.SelectedValue))
                {
                    // Retrieve the reminder with the matching days value
                    var reminder = context.EventReminders
                        .FirstOrDefault(o => o.Days.ToString() == ddlReminderDays.SelectedValue && o.EventUNID==QueryStringValues.UNID);

                    // If no matching reminder found, create a new one
                    if (reminder == null)
                    {
                        reminder = new PortfolioMgt.Entity.EventReminder
                        {
                            // Assuming you want to set Days as well
                            Days = int.Parse(ddlReminderDays.SelectedValue),
                            EventUNID=QueryStringValues.UNID
                        };
                        context.EventReminders.InsertOnSubmit(reminder); // Add the new reminder to the context
                    }

                    // Update the reminder's subject and email body
                    reminder.Subject = txtSubjectLine.Text;
                    reminder.EmailBody = txtDescription.Text;
                    string timeZoneName = hfTimeZoneName.Value;  // Get the time zone name
                    int timeZoneOffset = int.Parse(hfTimeZoneOffset.Value);  // Get the time zone offset (in minutes)

                    // Assign the time zone and offset to the reminder
                    reminder.TimeZoneName = timeZoneName;
                    reminder.TimeZoneOffset = timeZoneOffset;
                    try
                    {
                        // Save changes to the database
                        context.SubmitChanges();
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Updated Successfully!");
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, such as database errors, here
                        // You can log the exception or show a message
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    // Handle the case where no reminder day is selectedhttp://localhost:4242/App/Events/EventReminders.aspx.cs
                    // Possibly show an error or handle this scenario
                    Console.WriteLine("Reminder day is not selected.");
                }
            }

        }

        protected void ddlReminderDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new PortfolioDataContext())
            {
                var reminder=context.EventReminders.FirstOrDefault(o=>o.Days.ToString()==ddlReminderDays.SelectedValue);
                if(reminder!=null)
                { 
                txtDescription.Text = reminder.EmailBody;
                txtSubjectLine.Text = reminder.Subject;
                }
            }
        }
    }

    public class PTags
    {
       public string Name { get; set; }
        public string ID { get; set; }
    }
}