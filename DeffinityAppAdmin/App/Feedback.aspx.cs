using DocumentFormat.OpenXml.Spreadsheet;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev.App
{
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
            BindTeam(); 
        }
        private void BindGrid()
        {
            using (var context = new PortfolioDataContext())
            {
                // Get all feedbacks initially
                var feedbacks = context.FeedbackSystems.ToList();

                // Filter based on search term and selected status
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    feedbacks = feedbacks.Where(f =>
                        f.Name.Contains(txtSearch.Text) ||
                        f.EmailAddress.Contains(txtSearch.Text) ||
                        f.MobileNumber.Contains(txtSearch.Text) ||
                        f.Comments.Contains(txtSearch.Text)).ToList();
                }

                if (string.IsNullOrEmpty(txtSearch.Text) && ddlStatus.SelectedValue != "Select a Status")
                {
                    feedbacks = feedbacks.Where(f => f.Status == ddlStatus.SelectedValue).ToList();
                }
                
                // Bind the filtered data to GridView
                grid_issues.DataSource = feedbacks;
                grid_issues.DataBind();
            }
        }


        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void grid_issues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int feedbackId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditFeedback":
                    // Redirect to edit feedback page or show edit modal
                    EditFeedback(feedbackId);
                    break;

                case "SendMessage":
                    // Code to send message to customer
                    ShowSendEmail(feedbackId);
                    break;  

                case "EmailTrail":
                    Response.Redirect("FeedbackEmailTrail.aspx?MID=" + feedbackId);
                    // Code to display email trail for feedback
                    break;

                case "DeleteFeedback":
                    deleteFeedback(feedbackId);
                    // Code to delete the feedback entry
                    break;
            }
        }

        private void deleteFeedback(int feedbackId)
        {
            using (var context = new PortfolioDataContext())
            {
                // Find the feedback by its ID
                var feedbackToDelete = context.FeedbackSystems.SingleOrDefault(f => f.ID == feedbackId);

                // If the feedback is found, remove it from the context
                if (feedbackToDelete != null)
                {
                    context.FeedbackSystems.DeleteOnSubmit(feedbackToDelete);

                    // Submit the changes to the database
                    context.SubmitChanges();

                    // Optionally, refresh the grid after deletion
                    BindGrid();

                    Console.WriteLine("Feedback deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Feedback not found.");
                }
            }
        }

        private void ShowSendEmail(int feedbackId)
        {
            using (var context = new PortfolioDataContext())
            using (var ucontext = new UserDataContext())
            {
                var feedback = context.FeedbackSystems.FirstOrDefault(f => f.ID == feedbackId);
                if (feedback != null)
                {
                    var instance = context.ProjectPortfolios.FirstOrDefault(o => o.ID == feedback.PortfolioID);
                    var Loggedby = ucontext.Contractors.FirstOrDefault(o => o.ID == feedback.Submittedby);


                    lblInstance.Text = instance?.PortFolio;
                    lblloggedby.Text = Loggedby?.ContractorName + " " + Loggedby?.LastName;
                    lblfeedback.Text = feedback?.Comments;
                    hfIDtosendemail.Value = feedback?.ID.ToString();
                    string script = "showSendEmailModal();";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowEmailModal", script, true);


                    // Set the values of JavaScript variables using ClientScript
                }
            }

        }
        private void EditFeedback(int feedbackId)
        {
            using (var context = new PortfolioDataContext())
            {
                var feedback = context.FeedbackSystems.FirstOrDefault(f => f.ID == feedbackId);
                if (feedback != null)
                {

                    // Set the values of JavaScript variables using ClientScript
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowEditModal",
                        $"showEditModal({feedbackId}, '{feedback.Name}', '{feedback.EmailAddress}', '{feedback.MobileNumber}', '{feedback.FeedbackType}', '{feedback.Comments}', '{feedback.UrgencyLevel}', {feedback.IsAgreetobeContacted.ToString().ToLower()});", true);
                }
            }
        }

        protected void btnSubmitFeedback_Click(object sender, EventArgs e)
        {
            int feedbackId = Convert.ToInt32(hdnFeedbackID.Value);

            using (var context = new PortfolioDataContext())
            {
                var feedback = context.FeedbackSystems.FirstOrDefault(f => f.ID == feedbackId);
                if (feedback != null)
                {
                    // Update feedback data with values from modal
                    feedback.Name = txtName.Text;
                    feedback.EmailAddress = txtEmail.Text;
                    feedback.MobileNumber = txtMobile.Text;
                    feedback.FeedbackType = ddlFeedbackType.SelectedValue;
                    feedback.Comments = txtComments.Text;
                    feedback.UrgencyLevel = ddlUrgencyLevel.SelectedValue;
                    feedback.IsAgreetobeContacted = chkContact.Checked;

                    // Save changes to the database
                    context.SubmitChanges();

                    // Refresh the GridView to reflect updates
                    SendEmailToContact(feedback);
                    BindGrid();
                }
            }
        }

        private void SendEmailToContact(FeedbackSystem f)
        {
            try
            {
                using (var ucontext = new UserDataContext())
                using (var context = new PortfolioDataContext()) {
                    var instance = context.ProjectPortfolios.FirstOrDefault(o => o.ID == f.PortfolioID);
                    var user=context.PortfolioTrackerLogins.FirstOrDefault(o=>o.ID==sessionKeys.UID);
                string imagelogo = $"https://admindev.plegit.ai/ImageHandler.ashx?s=portfolio&id={f.PortfolioID}";
                    string subject = $"Thank You for Your Feedback on {instance.PortFolio}";
                    string html = $@"<!DOCTYPE html>
<html>
<head>
</head>
<body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; line-height: 1.6; color: #333;"">
    <!-- Instance Logo -->
    <div style=""text-align: center; margin-bottom: 20px;"">
        <img src=""{imagelogo}"" alt=""[Instance Logo]"" style=""max-width: 150px;"">
    </div>

    <!-- Greeting -->
    <p style=""margin: 10px 0;"">Hi <strong>{f.Name}</strong>,</p>

    <!-- Body Content -->
    <p style=""margin: 10px 0;"">
        Thank you for taking the time to share your feedback on <strong>{instance.PortFolio}</strong>. <br>
        We truly value your insights, as they help us continually improve and tailor our platform to better meet your needs.
    </p>

    <p style=""margin: 10px 0;"">After reviewing your comments, we’ve implemented a few updates to address your suggestions:</p>

    <!-- Feedback and Improvement -->
    <p style=""margin: 10px 0;""><strong>Your Feedback:</strong> {f.Comments}</p>
    <p style=""margin: 10px 0;""><strong>Improvement Implemented:</strong> {txtimprovemnts.Text}</p>

    <p style=""margin: 10px 0;"">
        We’re committed to creating a platform that works best for you, and your feedback plays a vital role in that journey. <br>
        Please don’t hesitate to reach out if you have more ideas or suggestions.
    </p>

    <p style=""margin: 10px 0;"">Thanks again for being an essential part of our community!</p>

    <!-- Footer -->
    <p style=""margin-top: 20px; margin-bottom: 5px;"">Best regards,</p>
    <p style=""margin: 10px 0;"">
        <img src=""{imagelogo}"" alt=""[Instance Logo]"" style=""max-width: 100px;""><br>
        {user.DisplayName}<br>
        <a href=""mailto:{user.Username}"" style=""color: #0073e6; text-decoration: none;"">
            {user.Username}
        </a>
    </p>
</body>
</html>";

                    Email em = new Email();
                    em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), subject, html, f.EmailAddress, "");
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfully", "OK");


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void AddEmailTrail(int fid,string subject,string toemail,string emailcontent)
        {
            using (var context = new PortfolioDataContext())
            {
                var Emailtrail = new EmailTrail
                {
                    Createdate = DateTime.Now,
                    Subject = subject,
                    EmailAddress = toemail,
                    Email = emailcontent,
                    Sentby = sessionKeys.UID,
                    Feedbackid = fid
                };
                context.EmailTrails.InsertOnSubmit(Emailtrail);
                context.SubmitChanges();
            }
        }
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            using(var context=new PortfolioDataContext())
            {
                var feedback = context.FeedbackSystems.FirstOrDefault(o => o.ID.ToString() == hfIDtosendemail.Value);
            Email em = new Email();
            
            // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
            em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), txtSubject.Text, txtEmailofSender.Text, feedback.EmailAddress, "");
                AddEmailTrail(feedback.ID, txtSubject.Text,  feedback.EmailAddress, txtEmailofSender.Text);
                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Email Sent Successfully", "OK");
            }
        }

        protected void btnaddteam_Click(object sender, EventArgs e)
        {
            using (var context = new PortfolioDataContext())
            {
                var newteam = new FeedbackTeam
                {
                    name = txtNameofTeam.Text,
                    Email = txtEmailofTeam.Text
                };
                context.FeedbackTeams.InsertOnSubmit(newteam);
                context.SubmitChanges();
                BindTeam();
            }

        }

        private void BindTeam()
        {
            using (var context = new PortfolioDataContext())
            {
                var members=context.FeedbackTeams.ToList();

                GridView1.DataSource= members;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (var context = new PortfolioDataContext())
            {
                int feedbackteamId = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "del":
                        // Redirect to edit feedback page or show edit modal

                        var team = context.FeedbackTeams.FirstOrDefault(o => o.id == feedbackteamId);
                        context.FeedbackTeams.DeleteOnSubmit(team);
                        context.SubmitChanges();
                        break;
                }


                }
        }
    }
}