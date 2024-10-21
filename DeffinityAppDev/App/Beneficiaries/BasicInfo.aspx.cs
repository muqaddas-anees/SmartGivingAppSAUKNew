using DeffinityAppDev.App.Beneficiaries;
using DeffinityAppDev.App.Beneficiaries.Entities;
using Newtonsoft.Json;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        public static string file_section_beneficiary_doc = "beneficiarydoc";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Reset form submission flag on initial load

                LoadCountryDropdown();
                BindCountryCodes();


                // Check if PersonID is present in the query string
                string personID = Request.QueryString["PersonID"];
                if (!string.IsNullOrEmpty(personID))
                {
                    int id = int.Parse(personID);
                    LoadBeneficiaryDetails(id); // A method to load details
                    GridFilesBind();
                }
                else
                {
                    CreateandRedirectBeneficiary();
                }

            }
            if (IsPostBack)
            {
                // Register the script to run the toggleFields function after postback
                ScriptManager.RegisterStartupScript(this, GetType(), "toggleFieldsScript", "toggleFields();", true);
            }
        }

        private void CreateandRedirectBeneficiary()
        {
            try
            {
                using (var context = new MyDatabaseContext())
                {
                    // Create a new beneficiary
                    var newBeneficiary = new Beneficiary
                    {
                        CreatedAt = DateTime.Now // Ensure CreatedAt has a valid value
                                                 // Set other properties as necessary
                    };

                    // Add the new beneficiary to the database
                    context.Beneficiaries.Add(newBeneficiary);

                    // Save the changes to the database
                    context.SaveChanges();

                    // Ensure the PersonID is not 0
                    if (newBeneficiary.PersonID != 0)
                    {
                        // Redirect to the BasicInfo page with the new PersonID
                        Response.Redirect("BasicInfo.aspx?Personid=" + newBeneficiary.PersonID);
                    }
                    else
                    {
                        // Handle the case where PersonID is 0 if needed
                        throw new Exception("PersonID is 0 after saving the beneficiary.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your own logging method)
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void LoadBeneficiaryDetails(int personID)
            {
                using (MyDatabaseContext db = new MyDatabaseContext())
                {
                    var beneficiary = db.Beneficiaries.SingleOrDefault(b => b.PersonID == personID);
                    if (beneficiary != null)
                    {
                        // Pre-fill the form inputs with the beneficiary details
                        txtName.Text = beneficiary.Name;
                        txtEmail.Text = beneficiary.Email;
                    txtTown.Text = beneficiary.Town;
                    txtAddress.Text = beneficiary.Address;
                    txtCity.Text = beneficiary.City;
                    txtEmail.Text = beneficiary.Email;
                    txtGovID.Text = beneficiary.GovernmentID;
                    txtHealthCondition.Text = beneficiary.HealthCondition;
                    txtNotes.Text = beneficiary.Background;
                    txtZip.Text = beneficiary.PostalCode;
                    ddlCountry.SelectedValue = beneficiary.Country;
                    ddlEmploymentStatus.SelectedValue = beneficiary.EmploymentStatus;
                    ddlGender.SelectedValue = beneficiary.Gender;
                    ddlCountry.SelectedValue = beneficiary.Country;
                    ddlDocumentType.SelectedValue = beneficiary.DocumentType;
                    txtID.Text = beneficiary.InternalIDNumber;
                    ddlType.SelectedValue = beneficiary.Type;


                    // Assuming beneficiary.Phone is in the format "countryCode-phoneNumber" (e.g., "1-1234567890")
                    if (!string.IsNullOrEmpty(beneficiary.Phone))
                    {
                        // Split the phone number by '-'
                        string[] phoneParts = beneficiary.Phone.Split('-');

                        if (phoneParts.Length == 2)
                        {
                            // Assign the first part (country code) to the DropDownList ddlCountryCode
                            ddlPhone.SelectedValue = phoneParts[0];  // Ensure the country code exists in the ddl options

                            // Assign the second part (phone number) to the TextBox txtPhone
                            txtPhone.Text = phoneParts[1];
                        }
                        else
                        {
                            // If the format is unexpected or there's no '-' to split
                            txtPhone.Text = beneficiary.Phone;
                        }
                    }
                    else
                    {
                        // Handle case when the phone number is empty or null
                        txtPhone.Text = string.Empty;
                        ddlPhone.SelectedIndex = 0;  // Reset or set to a default value
                    }


                    // Add other fields here as needed
                }
            }
            }
        protected void cvEmailUniqueModal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value.Trim();
            int portfolioID = sessionKeys.PortfolioID;

            // Get the PersonID from the query string to check for updates
            string personID = Request.QueryString["PersonID"];
            int? id = string.IsNullOrEmpty(personID) ? (int?)null : int.Parse(personID);

            try
            {
                // Use Entity Framework DbContext to check the existence of the email
                using (var context = new MyDatabaseContext())
                {
                    // Check if the email exists for a different beneficiary in the same portfolio
                    bool emailExists = context.Beneficiaries
                                              .Any(b => b.Email == email && b.TithingDefaultDetailsID == portfolioID &&
                                                         (!id.HasValue || b.PersonID != id.Value));

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
                    using (var context = new MyDatabaseContext())
                    {
                        Beneficiary beneficiary;

                        // Check if we are editing an existing record
                        string personID = Request.QueryString["PersonID"];
                        if (!string.IsNullOrEmpty(personID))
                        {
                            // Editing an existing record
                            int id = int.Parse(personID);
                            beneficiary = context.Beneficiaries.SingleOrDefault(b => b.PersonID == id);

                            if (beneficiary == null)
                            {
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = "Beneficiary not found.";
                                return;
                            }
                        }
                        else
                        {
                            // Creating a new record
                            beneficiary = new Beneficiary
                            {
                                CreatedAt = DateTime.Now
                            };
                            context.Beneficiaries.Add(beneficiary);
                        }

                        // Set/Update common beneficiary fields
                        beneficiary.Type = ddlType.SelectedValue;
                        beneficiary.DateOfBirth = DateTime.TryParse(txtDOBModal.Text, out var dob) ? dob : (DateTime?)null;
                        beneficiary.Gender = ddlGender.SelectedValue;
                        beneficiary.InternalIDNumber = txtID.Text.Trim();
                        beneficiary.Name = txtName.Text.Trim();
                        beneficiary.Address = txtAddress.Text.Trim();
                        beneficiary.Town = txtTown.Text.Trim();
                        beneficiary.City = txtCity.Text.Trim();
                        beneficiary.PostalCode = txtZip.Text.Trim();
                        beneficiary.Country = ddlCountry.SelectedValue;
                        beneficiary.DocumentType = ddlDocumentType.SelectedValue;
                        beneficiary.GovernmentID = txtGovID.Text.Trim();
                        beneficiary.Email = txtEmail.Text.Trim();
                        beneficiary.Phone = $"{ddlPhone.SelectedValue}-{txtPhone.Text.Trim()}";
                        beneficiary.Background = txtNotes.Text.Trim();
                        beneficiary.EmploymentStatus = ddlEmploymentStatus.SelectedValue;
                        beneficiary.HealthCondition = txtHealthCondition.Text.Trim();
                        beneficiary.TithingDefaultDetailsID = sessionKeys.PortfolioID;

                        // Handle Document Uploads
                        
                        // Save the changes to the database
                        context.SaveChanges();

                        // Clear the form after save
                        ClearForm();
                        LoadBeneficiaryDetails(beneficiary.PersonID);
                        // Display success message
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = personID != null ? "Beneficiary updated successfully!" : "Beneficiary saved successfully!";
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfully!");
                        
                    }
                }

                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
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
            txtEmail.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtHealthCondition.Text = string.Empty;
            // Clear FileUpload controls (Note: FileUpload cannot be cleared programmatically, so just inform the user to re-upload if necessary)
        
        }
        private void BindCountryCodes()
        {

            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Afghanistan (+93)", "+93"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Albania (+355)", "+355"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Algeria (+213)", "+213"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("American Samoa (+1684)", "+1684"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Andorra (+376)", "+376"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Angola (+244)", "+244"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Anguilla (+1264)", "+1264"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antarctica (+672)", "+672"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antigua and Barbuda (+1268)", "+1268"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Argentina (+54)", "+54"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Armenia (+374)", "+374"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Aruba (+297)", "+297"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Australia (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Austria (+43)", "+43"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Azerbaijan (+994)", "+994"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahamas (+1242)", "+1242"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahrain (+973)", "+973"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bangladesh (+880)", "+880"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Barbados (+1246)", "+1246"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belarus (+375)", "+375"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belgium (+32)", "+32"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belize (+501)", "+501"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Benin (+229)", "+229"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bermuda (+1441)", "+1441"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bhutan (+975)", "+975"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bolivia (+591)", "+591"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bosnia and Herzegovina (+387)", "+387"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Botswana (+267)", "+267"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brazil (+55)", "+55"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("British Indian Ocean Territory (+246)", "+246"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brunei (+673)", "+673"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bulgaria (+359)", "+359"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burkina Faso (+226)", "+226"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burundi (+257)", "+257"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cambodia (+855)", "+855"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cameroon (+237)", "+237"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Canada (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cape Verde (+238)", "+238"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cayman Islands (+1345)", "+1345"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Central African Republic (+236)", "+236"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chad (+235)", "+235"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chile (+56)", "+56"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("China (+86)", "+86"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Christmas Island (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cocos (Keeling) Islands (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Colombia (+57)", "+57"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Comoros (+269)", "+269"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (DRC) (+243)", "+243"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (Republic) (+242)", "+242"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cook Islands (+682)", "+682"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Costa Rica (+506)", "+506"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Côte d’Ivoire (+225)", "+225"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Croatia (+385)", "+385"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cuba (+53)", "+53"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Curaçao (+5999)", "+5999"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cyprus (+357)", "+357"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Czech Republic (+420)", "+420"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Denmark (+45)", "+45"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Djibouti (+253)", "+253"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominica (+1767)", "+1767"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominican Republic (+1809, +1829, +1849)", "+1809"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ecuador (+593)", "+593"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Egypt (+20)", "+20"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("El Salvador (+503)", "+503"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Equatorial Guinea (+240)", "+240"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eritrea (+291)", "+291"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Estonia (+372)", "+372"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eswatini (+268)", "+268"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ethiopia (+251)", "+251"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Falkland Islands (+500)", "+500"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Faroe Islands (+298)", "+298"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Fiji (+679)", "+679"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Finland (+358)", "+358"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("France (+33)", "+33"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Guiana (+594)", "+594"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Polynesia (+689)", "+689"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gabon (+241)", "+241"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gambia (+220)", "+220"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Georgia (+995)", "+995"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Germany (+49)", "+49"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ghana (+233)", "+233"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gibraltar (+350)", "+350"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greece (+30)", "+30"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greenland (+299)", "+299"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Grenada (+1473)", "+1473"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guadeloupe (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guam (+1671)", "+1671"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guatemala (+502)", "+502"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guernsey (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea (+224)", "+224"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea-Bissau (+245)", "+245"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guyana (+592)", "+592"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Haiti (+509)", "+509"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Honduras (+504)", "+504"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hong Kong (+852)", "+852"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hungary (+36)", "+36"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iceland (+354)", "+354"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("India (+91)", "+91"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Indonesia (+62)", "+62"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iran (+98)", "+98"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iraq (+964)", "+964"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ireland (+353)", "+353"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Isle of Man (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Israel (+972)", "+972"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Italy (+39)", "+39"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jamaica (+1876)", "+1876"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Japan (+81)", "+81"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jersey (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jordan (+962)", "+962"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kazakhstan (+7)", "+7"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kenya (+254)", "+254"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kiribati (+686)", "+686"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("North Korea (+850)", "+850"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Korea (+82)", "+82"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kosovo (+383)", "+383"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kuwait (+965)", "+965"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kyrgyzstan (+996)", "+996"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Laos (+856)", "+856"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Latvia (+371)", "+371"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lebanon (+961)", "+961"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lesotho (+266)", "+266"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liberia (+231)", "+231"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Libya (+218)", "+218"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liechtenstein (+423)", "+423"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lithuania (+370)", "+370"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Luxembourg (+352)", "+352"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Macau (+853)", "+853"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Madagascar (+261)", "+261"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malawi (+265)", "+265"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malaysia (+60)", "+60"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Maldives (+960)", "+960"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mali (+223)", "+223"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malta (+356)", "+356"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Marshall Islands (+692)", "+692"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Martinique (+596)", "+596"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritania (+222)", "+222"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritius (+230)", "+230"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mexico (+52)", "+52"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Micronesia (+691)", "+691"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Moldova (+373)", "+373"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Monaco (+377)", "+377"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mongolia (+976)", "+976"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montenegro (+382)", "+382"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montserrat (+1664)", "+1664"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Morocco (+212)", "+212"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mozambique (+258)", "+258"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Myanmar (+95)", "+95"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Namibia (+264)", "+264"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nauru (+674)", "+674"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nepal (+977)", "+977"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Netherlands (+31)", "+31"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Caledonia (+687)", "+687"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Zealand (+64)", "+64"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nicaragua (+505)", "+505"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niger (+227)", "+227"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nigeria (+234)", "+234"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niue (+683)", "+683"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norfolk Island (+672)", "+672"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Northern Mariana Islands (+1670)", "+1670"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norway (+47)", "+47"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Oman (+968)", "+968"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pakistan (+92)", "+92"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palau (+680)", "+680"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palestine (+970)", "+970"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Panama (+507)", "+507"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Papua New Guinea (+675)", "+675"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Paraguay (+595)", "+595"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Peru (+51)", "+51"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Philippines (+63)", "+63"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pitcairn Islands (+64)", "+64"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Poland (+48)", "+48"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Portugal (+351)", "+351"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Puerto Rico (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Qatar (+974)", "+974"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Réunion (+262)", "+262"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Romania (+40)", "+40"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Russia (+7)", "+7"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Rwanda (+250)", "+250"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Barthélemy (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Helena (+290)", "+290"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Kitts and Nevis (+1869)", "+1869"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Lucia (+1758)", "+1758"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Martin (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Pierre and Miquelon (+508)", "+508"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Vincent and the Grenadines (+1784)", "+1784"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Samoa (+685)", "+685"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("San Marino (+378)", "+378"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("São Tomé and Príncipe (+239)", "+239"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saudi Arabia (+966)", "+966"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Senegal (+221)", "+221"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Serbia (+381)", "+381"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Seychelles (+248)", "+248"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sierra Leone (+232)", "+232"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Singapore (+65)", "+65"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sint Maarten (+1721)", "+1721"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovakia (+421)", "+421"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovenia (+386)", "+386"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Solomon Islands (+677)", "+677"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Somalia (+252)", "+252"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Africa (+27)", "+27"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Sudan (+211)", "+211"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Spain (+34)", "+34"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sri Lanka (+94)", "+94"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sudan (+249)", "+249"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Suriname (+597)", "+597"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Svalbard and Jan Mayen (+47)", "+47"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sweden (+46)", "+46"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Switzerland (+41)", "+41"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Syria (+963)", "+963"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Taiwan (+886)", "+886"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tajikistan (+992)", "+992"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tanzania (+255)", "+255"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Thailand (+66)", "+66"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Timor-Leste (+670)", "+670"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Togo (+228)", "+228"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tokelau (+690)", "+690"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tonga (+676)", "+676"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Trinidad and Tobago (+1868)", "+1868"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tunisia (+216)", "+216"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkey (+90)", "+90"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkmenistan (+993)", "+993"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turks and Caicos Islands (+1649)", "+1649"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tuvalu (+688)", "+688"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uganda (+256)", "+256"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ukraine (+380)", "+380"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Arab Emirates (+971)", "+971"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Kingdom (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United States (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uruguay (+598)", "+598"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uzbekistan (+998)", "+998"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vanuatu (+678)", "+678"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vatican City (+39)", "+39"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Venezuela (+58)", "+58"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vietnam (+84)", "+84"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Yemen (+967)", "+967"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zambia (+260)", "+260"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zimbabwe (+263)", "+263"));


            // Optionally add a default item
            ddlPhone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Country Code--", "0"));
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
        public void GridFilesBind()
        {
            try
            {
                string SID = Request.QueryString["PersonID"];
                // Repository to fetch file data
                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                // Ensure repository is not null and fetch the file list by section and ID
                if (fRep != null)
                {
                    var fList = fRep.GetAll()
                                    .Where(o => o.Section == file_section_beneficiary_doc)
                                    .Where(o => o.FileID == SID)
                                    .ToList();

                    // Prepare data for binding with a projection to the fields needed
                    var rList = fList.Select(r => new
                    {
                        FileName = r.FileName, // Bind to the correct property 'FileName'
                        ID = r.ID,
                        FileID = r.FileID
                    }).ToList();

                    // Bind the data to the GridView
                    gridfiles.DataSource = rList;
                    gridfiles.DataBind();
                }
                else
                {
                    // Handle the case where repository is null (optional)
                    gridfiles.DataSource = null;
                    gridfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur
                LogExceptions.WriteExceptionLog(ex);

                // Optionally, clear the grid in case of an exception
                gridfiles.DataSource = null;
                gridfiles.DataBind();
            }
        }
        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int fileId = Convert.ToInt32(e.CommandArgument); // Get the file ID from CommandArgument

                // Handle Download Command
                if (e.CommandName == "Download")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                    // Fetch the file by its ID
                    var fileData = fRep.GetAll().FirstOrDefault(o => o.ID == fileId);

                    if (fileData != null)
                    {
                        // File data is stored in the database as bytes (fileData.FileData1)
                        byte[] fileBytes = fileData.FileData1.ToArray();
                        string fileName = fileData.FileName;

                        // Send the file as a download to the user's browser
                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.AddHeader("Content-Length", fileBytes.Length.ToString());
                        Response.BinaryWrite(fileBytes);
                        Response.Flush();
                        Response.End();
                    }
                }

                // Handle Delete Command
                if (e.CommandName == "Delete")
                {
                    using (var context = new PortfolioDataContext())
                    {
                        var file = context.FileDatas.FirstOrDefault(o => o.ID == fileId);
                        context.FileDatas.DeleteOnSubmit(file);
                        context.SubmitChanges();
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            // Check if a file is uploaded
            if (fileUpload.HasFile)
            {
                try
                {
                    using(var context=new PortfolioDataContext())
                    { 
                        
                    // Get the uploaded file as a byte array
                    byte[] fileByteArray;
                    using (BinaryReader br = new BinaryReader(fileUpload.PostedFile.InputStream))
                    {
                        fileByteArray = br.ReadBytes(fileUpload.PostedFile.ContentLength);
                    }

                    // Define parameters
                    string fileid = Request.QueryString["Personid"]; // Assigning fileid from query string
                    string section = file_section_beneficiary_doc; // Assign the section as needed
                    string fileExtension = Path.GetExtension(fileUpload.FileName); // Get the file extension
                    string filename = fileUpload.FileName; // Use the uploaded file's name

                    // Save the file with FileDBSave method, setting Smallfilebytearray as null
                        var file = new FileData
                        {
                            FileID = fileid,
                            Section = section,
                            FileExtenstion = fileExtension,
                            FileName = filename,
                            FileData1 = fileByteArray
                        };
                        context.FileDatas.InsertOnSubmit(file);
                        context.SubmitChanges();


                    // Optionally, handle the result or success/failure of the save
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "File Saved Successfully!");
                    GridFilesBind(); // Refresh the file grid or any necessary UI elements
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions and log errors
                    LogExceptions.WriteExceptionLog(ex);
                    DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Error: " + ex.Message);
                }
            }
            else
            {
                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Please select a file to upload.");
            }
        }

    }
}

