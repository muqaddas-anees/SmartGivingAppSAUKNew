using DeffinityAppDev.App.Beneficiaries;
using DeffinityAppDev.App.Beneficiaries.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Beneficaries
{
    public partial class BasicInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Reset form submission flag on initial load
               
                LoadCountryDropdown();
                BindCountryCodes();
            }

            // Check if we need to show the success modal
          
                // Register the startup script to show the success modal and redirect after 

         
        }
        protected void cvEmailUniqueModal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value.Trim();

            try
            {
                // Use Entity Framework DbContext to check the existence of the email
                using (var context = new MyDatabaseContext())
                {
                    bool emailExists = context.Beneficiaries
                                              .Any(b => b.Email == email);

                    // If the email exists, set validation to false
                    args.IsValid = !emailExists;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception as necessary
                args.IsValid = false;
                Debug.WriteLine(ex.Message);
                // Optionally, log the error
                // LogError(ex); // Replace this with your logging method
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Prevent duplicate form submission
         

            if (Page.IsValid)
            {
                try
                {
                    // Create a new instance of Beneficiary with the given input fields
                    var beneficiary = new Beneficiary
                    {
                        Type = ddlType.SelectedValue,
                        DateOfBirth = DateTime.TryParse(txtDOBModal.Text, out var dob) ? dob : (DateTime?)null,
                        Gender = ddlGender.SelectedValue,
                        InternalIDNumber = txtID.Text,
                        Name = txtName.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Town = txtTown.Text.Trim(),
                        City = txtCity.Text.Trim(),
                        PostalCode = txtZip.Text.Trim(),
                        Country = ddlCountry.SelectedValue,
                        DocumentType = ddlDocumentType.SelectedValue,
                        GovernmentID = txtGovID.Text.Trim(),
                        DocumentFront = fileUploadFront.HasFile ? fileUploadFront.FileBytes : null,
                        DocumentBack = fileUploadBack.HasFile ? fileUploadBack.FileBytes : null,
                        CreatedAt = DateTime.Now,
                        Email = txtEmail.Text.Trim(),
                        Phone = $"{ddlCountryCode.SelectedValue}{txtPhone.Text.Trim()}",
                        Background = txtNotes.Text.Trim(),
                        EmploymentStatus = ddlEmploymentStatus.SelectedValue,
                        HealthCondition = txtHealthCondition.Text.Trim(),
                        TithingDefaultDetailsID = 1 // Default Tithing ID
                    };

                    // Save the new Beneficiary to the database using Entity Framework
                    using (var context = new MyDatabaseContext())
                    {
                        context.Beneficiaries.Add(beneficiary);
                        context.SaveChanges(); // Commit the changes to the database
                    
                lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "User Saved Succesfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalAndRedirect", "showSuccessModal()", true);
                    }

                   


                }
                catch (DbUpdateException ex)
                {
                    // Iterate through all inner exceptions to get to the root of the issue
                    Exception innerException = ex;
                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    // Log the detailed error to the debug output
                    System.Diagnostics.Debug.WriteLine("Error saving beneficiary: " + ex.Message);
                    System.Diagnostics.Debug.WriteLine("Inner Exception: " + innerException.Message);

                    // Optionally, log the full stack trace for more insight
                    System.Diagnostics.Debug.WriteLine("Stack Trace: " + innerException.StackTrace);

                    // Display a friendly error message to the user
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "An unexpected error occurred while saving the data. Please try again later.";
                }

            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please fill in all required fields.";
            }
        }
        private void ClearForm()
        {
            ddlType.SelectedIndex = -1;
            txtDOBModal.Text = string.Empty;
            ddlGender.SelectedIndex = -1;
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtZip.Text = string.Empty;
            ddlCountry.SelectedIndex = -1;
            ddlDocumentType.SelectedIndex = -1;
            txtGovID.Text = string.Empty;

            // Clear FileUpload controls (Note: FileUpload cannot be cleared programmatically, so just inform the user to re-upload if necessary)
            fileUploadFront.Attributes.Clear();
            fileUploadBack.Attributes.Clear();
        }
        private void BindCountryCodes()
        {
            try
            {
                // Use WebClient to download data from the REST Countries API
                using (WebClient webClient = new WebClient())
                {
                    string apiUrl = "https://restcountries.com/v3.1/all";
                    string jsonResponse = webClient.DownloadString(apiUrl);

                    // Deserialize JSON into a list of Country objects
                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(jsonResponse);

                    // Filter out countries without dialing codes
                    List<Country> filteredCountries = countries.FindAll(c => !string.IsNullOrEmpty(c.DialingCode));

                    // Sort the list alphabetically by country name
                    filteredCountries.Sort((x, y) => string.Compare(x.Name.Common, y.Name.Common, StringComparison.Ordinal));

                    // Bind the data to the DropDownList
                    ddlCountryCode.Items.Clear();
                    ddlCountryCode.DataSource = filteredCountries;
                    ddlCountryCode.DataTextField = "DisplayName"; // e.g., "United States (+1)"
                    ddlCountryCode.DataValueField = "DialingCode"; // e.g., "+1"
                    ddlCountryCode.DataBind();

                    // Insert a default item at the top
                    ddlCountryCode.Items.Insert(0, new ListItem("", ""));
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, display a message)
                Console.WriteLine($"Error fetching country data: {ex.Message}");

                // Optionally, you can display a user-friendly message in the UI
                ddlCountryCode.Enabled = false;
                ddlCountryCode.Items.Clear();
                ddlCountryCode.Items.Add(new ListItem("Unable to load country codes", ""));
            }
        }
        private void LoadCountryDropdown()
        {
            // Get all specific cultures
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            // Use a HashSet to store unique country codes
            HashSet<string> countryCodes = new HashSet<string>();

            // Loop over the cultures and extract unique country codes
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    RegionInfo region = new RegionInfo(culture.Name);
                    countryCodes.Add(region.TwoLetterISORegionName);
                }
                catch
                {
                    // Ignore cultures that do not have region information
                }
            }

            // Now, build a list of unique RegionInfo objects from the country codes
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (string countryCode in countryCodes)
            {
                try
                {
                    RegionInfo region = new RegionInfo(countryCode);
                    countries.Add(region);
                }
                catch
                {
                    // Ignore invalid country codes
                }
            }

            // Sort countries by English name
            countries = countries.OrderBy(c => c.EnglishName).ToList();

            // Clear existing items in the dropdown
            ddlCountry.Items.Clear();

            // Add default item
            ddlCountry.Items.Add(new ListItem("-- Select Country --", ""));

            // Add countries to dropdown
            foreach (RegionInfo country in countries)
            {
                ddlCountry.Items.Add(new ListItem(country.EnglishName, country.TwoLetterISORegionName));
            }
        }


    }
}

