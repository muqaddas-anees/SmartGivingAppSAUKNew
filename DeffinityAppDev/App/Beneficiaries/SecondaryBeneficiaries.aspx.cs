using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System.Data.Entity.Infrastructure;
using Microsoft.Ajax.Utilities;


namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class SecondaryBeneficiaries : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindCountryCodes();
                LoadCountryDropdown();
                LoadSecondaryBeneficiaries();

                // Existing code...
            }

            else if (Request["__EVENTTARGET"] == "EditBeneficiary")
            {
                string beneficiaryID = Request["__EVENTARGUMENT"];
                LoadBeneficiaryDetails(beneficiaryID); // Load beneficiary details

                // Set hidden field value to "true"
                hfShowModal.Value = "true";
            }






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
                    ddlCountryCode.Items.Insert(0, new ListItem("Select Country Code", ""));
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
            //Get all specific cultures
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

            // Clear existing items
            ddlCountryModal.Items.Clear();

            // Add default item
            ddlCountryModal.Items.Add(new ListItem("-- Select Country --", ""));

            // Add countries to dropdown
            foreach (RegionInfo country in countries)
            {
                ddlCountryModal.Items.Add(new ListItem(country.EnglishName, country.TwoLetterISORegionName));
            }
        }


        protected void rptSecondaryBeneficiaries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "EditBeneficiary")
            {
                string beneficiaryID = e.CommandArgument.ToString(); // Get the ID of the selected beneficiary
                LoadBeneficiaryDetails(beneficiaryID); // Load details

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "alert('Modal should be shown');", true);

            }
        }


        private void LoadBeneficiaryDetails(string beneficiaryID)
        {


            using (var context = new MyDatabaseContext())
            {
                // Use LINQ to get the SecondaryBeneficiary with the specified ID
                var beneficiary = context.SecondaryBeneficiary.FirstOrDefault(b => b.SecondaryBeneficiaryID.ToString() == beneficiaryID);

                if (beneficiary != null)
                {
                    // Populate the modal fields with the retrieved data
                    ddlTypeModal.SelectedValue = beneficiary.Type;
                    ddlGenderModal.SelectedValue = beneficiary.Gender;
                    txtNameModal.Text = beneficiary.Name;
                    txtEmailModal.Text = beneficiary.Email;

                    txtDOBModal.Text = beneficiary.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty;
                    txtIDModal.Text = beneficiary.InternalIDNumber;
                    txtAddressModal.Text = beneficiary.Address;
                    txtTownModal.Text = beneficiary.Town;
                    txtCityModal.Text = beneficiary.City;
                    txtZipModal.Text = beneficiary.PostalCode;
                    ddlCountryModal.SelectedValue = beneficiary.Country;
                    txtBackgroundModal.Text = beneficiary.Background;
                    txtHealthConditionModal.Text = beneficiary.HealthCondition;
                    ddlEmploymentStatusModal.SelectedValue = beneficiary.EmploymentStatus;

                    // Handle PhoneNumber
                    string fullPhoneNumber = beneficiary.PhoneNumber;
                    string countryCode = "";
                    string phoneNumber = "";

                    if (!string.IsNullOrEmpty(fullPhoneNumber))
                    {
                        // Extract the first 3 or 4 characters as the country code (assuming the '+' sign is included)
                        countryCode = fullPhoneNumber.Substring(0, 3); // e.g., "+92"

                        // Some country codes may be longer (4 chars), so adjust accordingly
                        if (ddlCountryCode.Items.FindByValue(countryCode) == null)
                        {
                            // Try with a longer country code (e.g., "+971")
                            countryCode = fullPhoneNumber.Substring(0, 4);
                        }
                        if (ddlCountryCode.Items.FindByValue(countryCode) == null)
                        {
                            // Try with a longer country code (e.g., "+971")
                            countryCode = fullPhoneNumber.Substring(0, 5);
                        }


                        // Extract the remaining part of the string as the phone number
                        phoneNumber = fullPhoneNumber.Substring(countryCode.Length);

                        // Set the dropdown for country code
                        if (ddlCountryCode.Items.FindByValue(countryCode) != null)
                        {
                            ddlCountryCode.SelectedValue = countryCode;
                        }
                        else
                        {
                            ddlCountryCode.SelectedIndex = 0; // Default or error handling
                        }

                        // Set the phone number in the text box
                        txtPhone.Text = phoneNumber;
                    }

                    // Handle image data, if needed
                    if (beneficiary.ProfileImage != null)
                    {
                        byte[] imageData = beneficiary.ProfileImage;
                        string base64Image = Convert.ToBase64String(imageData);
                        // You can show the image in the modal
                        // Example: imgProfileImage.Attributes["src"] = "data:image/jpeg;base64," + base64Image;
                    }
                }
            }
        }

        private void SaveSecondaryBeneficiary()
        {




            // Get the beneficiary ID from the hidden field
            string beneficiaryID = hfBeneficiaryID.Value;

            try
            {
                using (var context = new MyDatabaseContext())
                {
                    Debug.WriteLine("Entity Framework context opened.");

                    // Determine whether we are in "Edit" or "Add" mode
                    SecondaryBeneficiary beneficiary;

                    if (string.IsNullOrEmpty(beneficiaryID))
                    {
                        // Add mode: Insert a new beneficiary
                        beneficiary = new SecondaryBeneficiary();
                        context.SecondaryBeneficiary.Add(beneficiary);

                        // Generate a new beneficiary ID (assuming an auto-increment mechanism)
                    }
                    else
                    {
                        // Edit mode: Update existing beneficiary
                        int parsedID = int.Parse(beneficiaryID);
                        beneficiary = context.SecondaryBeneficiary.FirstOrDefault(b => b.SecondaryBeneficiaryID == parsedID);

                        if (beneficiary == null)
                        {
                            ShowMessage("Beneficiary not found. Please try again.");
                            return;
                        }
                    }

                    // Assign properties from the form inputs
                    beneficiary.TithingID = 1; // Assuming this dropdown is for TithingID
                    beneficiary.Type = ddlTypeModal.SelectedValue;
                    beneficiary.Gender = ddlGenderModal.SelectedValue;
                    beneficiary.Name = txtNameModal.Text.Trim();
                    beneficiary.Email = txtEmailModal.Text.Trim();
                    beneficiary.PhoneNumber = ddlCountryCode.SelectedValue + txtPhone.Text;
                    beneficiary.DateOfBirth = string.IsNullOrEmpty(txtDOBModal.Text) ? (DateTime?)null : Convert.ToDateTime(txtDOBModal.Text);
                    beneficiary.InternalIDNumber = txtIDModal.Text.Trim();
                    beneficiary.Address = txtAddressModal.Text.Trim();
                    beneficiary.Town = txtTownModal.Text.Trim();
                    beneficiary.City = txtCityModal.Text.Trim();
                    beneficiary.PostalCode = txtZipModal.Text.Trim();
                    beneficiary.Country = ddlCountryModal.SelectedValue;
                    beneficiary.Background = txtBackgroundModal.Text.Trim();
                    beneficiary.HealthCondition = txtHealthConditionModal.Text.Trim();
                    beneficiary.EmploymentStatus = ddlEmploymentStatusModal.SelectedValue;
                    beneficiary.CreatedAt = DateTime.Now;

                    // Handle ProfileImage upload
                    if (fuProfileImage.HasFile)
                    {
                        Debug.WriteLine("File has been uploaded. File name: " + fuProfileImage.FileName);
                        string fileExtension = System.IO.Path.GetExtension(fuProfileImage.FileName).ToLower();
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            byte[] imageData = fuProfileImage.FileBytes;
                            Debug.WriteLine("File size: " + imageData.Length + " bytes.");
                            beneficiary.ProfileImage = imageData;
                        }
                        else
                        {
                            Debug.WriteLine("Invalid file type: " + fileExtension);
                            ShowMessage("Only image files (jpg, jpeg, png, gif) are allowed.");
                            return; // Exit without saving
                        }
                    }

                    // Save changes to the database
                    context.SaveChanges();
                    LoadSecondaryBeneficiaries();
                    ShowMessage("User Saved Succesfully");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowRedirect", "showSuccessModal();", true);

                }




            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.InnerException;
                if (innerException != null)
                {
                    Debug.WriteLine("Inner Exception: " + innerException.Message);
                    ShowMessage("An error occurred: " + innerException.Message);
                }
                else
                {
                    Debug.WriteLine("General DbUpdateException: " + dbEx.Message);
                    ShowMessage("An error occurred: " + dbEx.Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception: " + ex.Message);
                ShowMessage("An unexpected error occurred. Please try again.");
            }

        }


        protected string GenerateNewBeneficiaryID()
        {
            return "1";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

          
            

            
                try
                {

                    SaveSecondaryBeneficiary();

                    // Clear the form fields after saving
                    ClearForm();

                    // Reload the beneficiaries to include the newly added one
                    //LoadSecondaryBeneficiaries();



                }
                catch (Exception ex)
                {
                    ShowMessage("An error occurred: " + ex.Message);

                }
            
          



        }

        protected void cvEmailUniqueModal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value.Trim();

            try
            {
                using (var context = new MyDatabaseContext())
                {
                    // Check if email already exists in the database
                    bool emailExists = context.SecondaryBeneficiary.Any(b => b.Email == email);

                    if (emailExists)
                    {
                        // Email already exists
                        args.IsValid = false;
                        lblMessage.Text = "Email Already Exists";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showMe", "showSuccessModal()", true);
                    }
                    else
                    {
                        // Email is unique
                        args.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

                // Log or handle exception as necessary
                // For now, we'll assume it's invalid to prevent data from being saved in case of an error
                args.IsValid = false;

                // Optionally, log the error
                // LogError(ex); // Replace this with your logging method
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-success";
            lblMessage.Style.Add("margin-top", "30px");
            lblMessage.Visible = true; // Add this line to make the message visible
        }
        private void ClearForm()
        {
           
            ddlTypeModal.SelectedIndex = -1; // Reset dropdown
            ddlGenderModal.SelectedIndex = -1; // Reset dropdown
            txtNameModal.Text = string.Empty; // Clear name
            txtDOBModal.Text = string.Empty; // Clear Date of Birth
            txtIDModal.Text = string.Empty; // Clear Internal ID
            txtAddressModal.Text = string.Empty; // Clear Address
            txtTownModal.Text = string.Empty; // Clear Town
            txtCityModal.Text = string.Empty; // Clear City
            txtZipModal.Text = string.Empty; // Clear Zip code
            ddlCountryModal.SelectedIndex = 0; // Reset dropdown
            txtEmailModal.Text = string.Empty;
            txtPhone.Text = string.Empty;
            ddlEmploymentStatusModal.SelectedIndex = -1;
            txtHealthConditionModal.Text = string.Empty;
            txtBackgroundModal.Text = string.Empty;

        }
        private void LoadSecondaryBeneficiaries()
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch data from the database first
                var beneficiaries = context.SecondaryBeneficiary
                    .Select(b => new
                    {
                        b.SecondaryBeneficiaryID,
                        b.Gender,
                        b.Name,
                        b.Email,
                        b.DateOfBirth,
                        b.InternalIDNumber,
                        b.PhoneNumber,
                        b.ProfileImage // Fetch the binary image data as-is
                    })
                    .ToList(); // Perform the database query first

                // Convert ProfileImage to Base64 string in-memory after data retrieval
                var beneficiariesWithBase64Images = beneficiaries.Select(b => new
                {
                    b.SecondaryBeneficiaryID,
                    b.Gender,
                    b.Name,
                    b.Email,
                    b.DateOfBirth,
                    b.InternalIDNumber,
                    b.PhoneNumber,
                    ProfileImageBase64 = b.ProfileImage != null
                        ? "data:image/jpeg;base64," + Convert.ToBase64String(b.ProfileImage)
                        : "~/metronic8/demo1/assets/media/avatars/300-1.jpg" // Default placeholder if image is null
                }).ToList();

                // Bind the result to the Repeater
                rptSecondaryBeneficiaries.DataSource = beneficiariesWithBase64Images;
                rptSecondaryBeneficiaries.DataBind();
            }
        }

    }

}