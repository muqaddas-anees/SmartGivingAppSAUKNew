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
using DeffinityManager;
using DeffinityAppDev.App.Beneficiaries;
using System.Runtime.Remoting.Contexts;
using UserMgt.DAL;
using System.Web.UI.WebControls;

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
                BindLoggedByDropdown();
                // Reset the session flag

            }
        }
        protected void BindLoggedByDropdown()
        {
            try
            {
                UserDataContext tags = new UserDataContext();
                var contractorsList = tags.v_contractors
                    .Where(o => o.CompanyID == sessionKeys.PortfolioID)
                    .ToList();

                ddlLoggedBy.DataSource = contractorsList;
                ddlLoggedBy.DataTextField = "ContractorName"; // Update this line
                ddlLoggedBy.DataValueField = "ID"; // Ensure ID is correct as well
                ddlLoggedBy.DataBind(); // Don't forget to call DataBind after setting DataSource
                ddlLoggedBy.Items.Insert(0, new ListItem("Select a person", "")); // Add the default item if needed
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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
                        .Where(a => a.ActivityDate >= today && a.ActivityDate < tomorrow && a.TithingDefaultDetailsID==sessionKeys.PortfolioID)
                        .OrderByDescending(a => a.ActivityDate)
                         
                        .ToList();

                    for(int i=0;i<activities.Count;i++)
                    {
                        activities[i].LoggedBy = Helper.GetPersonNamebyID(activities[i].LoggedBy);
                    }
                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log the error message
                Debug.WriteLine("Error loading today's activities: " + ex.Message);
                lblErrorMessage.Visible = true; // Display the error message to the user
                LogExceptions.WriteExceptionLog(ex);
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
                        .Where(a => a.ActivityDate >= startOfWeek && a.ActivityDate < endOfWeek && a.TithingDefaultDetailsID==sessionKeys.PortfolioID)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();
                    for (int i = 0; i < activities.Count; i++)
                    {
                        activities[i].LoggedBy = Helper.GetPersonNamebyID(activities[i].LoggedBy);
                    }
                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading weekly activities: " + ex.Message;
                lblErrorMessage.Visible = true;
                LogExceptions.WriteExceptionLog(ex);

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
                        .Where(a => a.ActivityDate >= startOfMonth && a.ActivityDate < endOfMonth && a.TithingDefaultDetailsID==sessionKeys.PortfolioID)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();
                    for (int i = 0; i < activities.Count; i++)
                    {
                        activities[i].LoggedBy = Helper.GetPersonNamebyID(activities[i].LoggedBy);
                    }
                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                    Debug.WriteLine( "Error loading monthly activities: " + ex.Message);
                lblErrorMessage.Visible = true;
                LogExceptions.WriteExceptionLog(ex);

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
                        .Where(a => a.ActivityDate >= startOfYear && a.ActivityDate < endOfYear && a.TithingDefaultDetailsID==sessionKeys.PortfolioID)
                        .OrderByDescending(a => a.ActivityDate)
                        .ToList();
                    for (int i = 0; i < activities.Count; i++)
                    {
                        activities[i].LoggedBy = Helper.GetPersonNamebyID(activities[i].LoggedBy);
                    }
                    rptActivities.DataSource = activities;
                    rptActivities.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading yearly activities: " + ex.Message;
                lblErrorMessage.Visible = true;
                LogExceptions.WriteExceptionLog(ex);

            }
        }


        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = "Error: " + message;
            lblErrorMessage.Visible = true;
        }
    


protected void btnSaveActivity_Click(object sender, EventArgs e)
        {
          
          

            // Save activity to the database
            bool isSaved = SaveActivity();
            if (isSaved)
            {
                ClearForm();
                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Activity saved Succesfully", "close");
                LoadTodayActivities( sender,  e);
            }
            else
            {
                lblErrorMessage.Text = "An error occurred while saving the activity.";
                lblErrorMessage.CssClass = "alert alert-danger";
                lblErrorMessage.Visible = true;
                System.Diagnostics.Debug.WriteLine("Form submission failed: Activity could not be saved.");
                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "something wrong occured", "close");
            }
        }

        private bool SaveActivity()
        {
            using (var context = new MyDatabaseContext())
            {
                var newActivity = new DeffinityAppDev.App.Beneficiaries.Entities.BeneficiaryActivity
                {
                    ActivityDate = DateTime.Parse(txtActivityDate.Text),  // Ensure the date is correctly formatted
                    LoggedBy = ddlLoggedBy.SelectedValue,
                    ProgressDetails = txtProgressDetails.Text,
                    CreatedAt = DateTime.Now,
                    ImageData = SaveUploadedFiles(),
                    // Ensure you include image data
                    TithingDefaultDetailsID = sessionKeys.PortfolioID
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
            ddlLoggedBy.SelectedValue= "";
            fileUploadDocuments.Dispose();  // Reset the file input
        }
    }
}
