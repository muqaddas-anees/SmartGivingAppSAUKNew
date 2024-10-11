using DeffinityAppDev.App.Beneficiaries.Entities;
using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiaryActivity : System.Web.UI.Page
    {
        protected BeneficiaryActivityService _activityService;
        protected void Page_Load(object sender, EventArgs e)
        {
            _activityService = new BeneficiaryActivityService();
            if (!IsPostBack)
            {
                LoadTodayActivities(null, null);
                // Reset the session flag
                Session["FormSubmitted"] = null;
            }

            // Check if we need to show the success modal
            if (Session["ShowSuccessModal"] != null && (bool)Session["ShowSuccessModal"])
            {

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "User Saved Succesfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowTheMOdal", "showSuccessModal();", true);

                // Reset the session flag
                Session["ShowSuccessModal"] = null;
            }
        }
        protected void LoadTodayActivities(object sender, EventArgs e)
        {
            try
            {
                using (MyDatabaseContext context = new MyDatabaseContext())
                {
                    DateTime today = DateTime.Today;
                    DateTime tomorrow = today.AddDays(1); // Get the start of tomorrow

                    // Ensure that the comparison works correctly with DateTime
                    var activities = context.BeneficiaryActivities
                        .Where(a => a.ActivityDate >= today && a.ActivityDate < tomorrow)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();

                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log the error message
                Debug.WriteLine("Error loading today's activities: " + ex.Message);
                lblErrorMessage.Visible = true; // Display the error message to the user
            }
        }


        // Load this week's activities
        protected void LoadWeeklyActivities(object sender, EventArgs e)
        {
            try
            {
                using (MyDatabaseContext context = new MyDatabaseContext())
                {
                    DateTime today = DateTime.Today;
                    DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Start of the week
                    DateTime endOfWeek = startOfWeek.AddDays(7); // End of the week

                    var activities = context.BeneficiaryActivities
                        .Where(a => a.ActivityDate >= startOfWeek && a.ActivityDate < endOfWeek)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();

                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading weekly activities: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        // Load this month's activities
        protected void LoadMonthlyActivities(object sender, EventArgs e)
        {
            try
            {
                using (MyDatabaseContext context = new MyDatabaseContext())
                {
                    DateTime today = DateTime.Today;
                    DateTime startOfMonth = new DateTime(today.Year, today.Month, 1); // Start of the month
                    DateTime endOfMonth = startOfMonth.AddMonths(1); // End of the month

                    var activities = context.BeneficiaryActivities
                        .Where(a => a.ActivityDate >= startOfMonth && a.ActivityDate < endOfMonth)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();

                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                    Debug.WriteLine( "Error loading monthly activities: " + ex.Message);
                lblErrorMessage.Visible = true;
            }
        }

        // Load this year's activities
        protected void LoadYearlyActivities(object sender, EventArgs e)
        {
            try
            {
                using (MyDatabaseContext context = new MyDatabaseContext())
                {
                    DateTime today = DateTime.Today;
                    DateTime startOfYear = new DateTime(today.Year, 1, 1); // Start of the year
                    DateTime endOfYear = startOfYear.AddYears(1); // End of the year

                    var activities = context.BeneficiaryActivities
                        .Where(a => a.ActivityDate >= startOfYear && a.ActivityDate < endOfYear)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();

                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading yearly activities: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }


        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = "Error: " + message;
            lblErrorMessage.Visible = true;
        }
    


protected void btnSaveActivity_Click(object sender, EventArgs e)
        {
          
            if (Session["FormSubmitted"] != null && (bool)Session["FormSubmitted"])
            {
                // Redirect to avoid duplicate form submission
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Save activity to the database
            bool isSaved = SaveActivity();
            if (isSaved)
            {
                ClearForm();
                // Trigger the success modal
                Session["ShowSuccessModal"] = true;
                Session["FormSubmitted"] = true;
                // Set session to show the modal
                System.Diagnostics.Debug.WriteLine("Activity saved successfully.");
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblErrorMessage.Text = "An error occurred while saving the activity.";
                lblErrorMessage.CssClass = "alert alert-danger";
                lblErrorMessage.Visible = true;
                System.Diagnostics.Debug.WriteLine("Form submission failed: Activity could not be saved.");
            }
        }

        private bool SaveActivity()
        {
            using (var context = new MyDatabaseContext())
            {
                var newActivity = new DeffinityAppDev.App.Beneficiaries.Entities.BeneficiaryActivity
                {
                    ActivityDate = DateTime.Parse(txtActivityDate.Text),  // Ensure the date is correctly formatted
                    LoggedBy = txtLoggedBy.Text,
                    ProgressDetails = txtProgressDetails.Text,
                    CreatedAt = DateTime.Now,
                    ImageData = SaveUploadedFiles(),
                    // Ensure you include image data
                    TithingDefaultDetailsID = 1
                };

                
                context.BeneficiaryActivities.Add(newActivity);  // Add to the context

                try
                {
                    context.SaveChanges();  // Save changes to the database
                    return true;
                }
                catch (DbEntityValidationException ex) // Validation errors
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    lblErrorMessage.Text = "Validation error occurred while saving the activity.";
                }
                catch (DbUpdateException ex) // Database update errors
                {
                    var innerException = ex.InnerException?.InnerException;
                    if (innerException != null)
                    {
                        lblErrorMessage.Text = "Error: " + innerException.Message;
                        System.Diagnostics.Debug.WriteLine($"Inner Exception: {innerException.Message}");
                    }
                    else
                    {
                        lblErrorMessage.Text = "An error occurred while updating the entries.";
                    }
                    System.Diagnostics.Debug.WriteLine($"Error occurred: {ex.Message}");
                }
                catch (Exception ex) // Generic errors
                {
                    lblErrorMessage.Text = "Error: " + ex.Message;
                    System.Diagnostics.Debug.WriteLine($"Error occurred: {ex.Message}");
                }

                lblErrorMessage.CssClass = "alert alert-danger";
                lblErrorMessage.Visible = true;
                return false;
            }
        }





        private byte[] SaveUploadedFiles()
        {
            if (fileUploadDocuments.HasFile)
            {
                var uploadedFile = fileUploadDocuments.PostedFile;
                if (uploadedFile != null)
                {
                    using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                    {
                        return br.ReadBytes(uploadedFile.ContentLength);  // Convert file to byte array
                    }
                }
            }
            return null;
        }

        private void ClearForm()
        {
            txtActivityDate.Text = string.Empty;
            txtProgressDetails.Text = string.Empty;
            txtLoggedBy.Text = string.Empty;
            fileUploadDocuments.Dispose();  // Reset the file input
        }
    }
}
