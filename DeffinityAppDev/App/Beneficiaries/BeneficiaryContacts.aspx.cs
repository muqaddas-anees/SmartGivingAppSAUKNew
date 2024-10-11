using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.UI.WebControls;
using Antlr.Runtime.Tree;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System.Linq;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiaryContacts : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
            if (!IsPostBack)
            {
                
                BindCountryCodes();
                LoadContacts();
            }
           else if (Request["__EVENTTARGET"] == "EditContact")
            {
                string contactID = Request["__EVENTARGUMENT"];
                LoadContactDetails(contactID); // Load the contact details
                hfShowModal.Value = "true"; // Show the modal
            }

                // Register the startup script to show the success modal and redirect after delay

            }catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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
        private void LoadContactDetails(string contactID)
        {
            using (var context = new MyDatabaseContext())
            {
                // Convert contactID to an integer
                int parsedContactID = int.Parse(contactID);

                // Query to get the specific contact
                var contact = context.BeneficiaryContacts
                    .FirstOrDefault(c => c.ContactID == parsedContactID);

                if (contact != null)
                {
                    hfContactID.Value = contactID;
                    txtFirstName.Text = contact.FirstName;
                    txtLastName.Text = contact.LastName;
                    txtEmailAddress.Text = contact.EmailAddress;
                    ddlCountryCode.SelectedValue = contact.CountryCode;
                    txtPhoneNumber.Text = contact.PhoneNumber;
                    txtPosition.Text = contact.Position;
                    txtNotes.Text = contact.Notes;
                }
            }
        }

        private void LoadContacts()
        {
            Debug.WriteLine("i am in LoadContacts");
            using (var context = new MyDatabaseContext())
            {
                try
                {
                    // Query to get all contacts
                    var contacts = context.BeneficiaryContacts.ToList();

                    // Debugging: Check how many contacts were retrieved
 

                    RepeaterContacts.DataSource = contacts;
                    RepeaterContacts.DataBind();

                    // Debugging: Check if Repeater has data
                    if (contacts.Count == 0)
                    {
                        Debug.WriteLine("No contacts found.");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Debug.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                    if (ex.InnerException is SqlException sqlEx)
                    {
                        Debug.WriteLine($"SQL Exception: {sqlEx.Message}");
                    }
                    ShowErrorMessage("An error occurred while loading contacts: " + ex.Message);
                }
                catch (ConstraintException constraintEx)
                {
                    Debug.WriteLine($"Constraint Exception: {constraintEx.Message}");
                    ShowErrorMessage("A constraint violation occurred: " + constraintEx.Message);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred: " + ex.Message);
                    Debug.WriteLine(ex.Message);
                }
            }
        }



        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
          

            if (Page.IsValid)
            {
                // Save the contact details
                SaveContact();
            }
        }

        private void SaveContact()
        {
            try
            {
                using (var context = new MyDatabaseContext())
                {
                    int contactID;
                    bool isNew = !int.TryParse(hfContactID.Value, out contactID);

                    BeneficiaryContact contact;

                    if (isNew)
                    {
                        contact = new BeneficiaryContact();
                            
                        context.BeneficiaryContacts.Add(contact);
                        contact.CreatedAt = DateTime.Now;
                    }
                    else
                    {
                        contact = context.BeneficiaryContacts.FirstOrDefault(c => c.ContactID == contactID);
                    }

                    if (contact != null)
                    {
                        contact.FirstName = txtFirstName.Text;
                        contact.LastName = txtLastName.Text;
                        contact.EmailAddress = txtEmailAddress.Text;
                        contact.CountryCode = ddlCountryCode.SelectedValue;
                        contact.PhoneNumber = txtPhoneNumber.Text;
                        contact.Position = txtPosition.Text;
                        contact.Notes = txtNotes.Text;
                        contact.TithingID = 1;
                       

                        context.SaveChanges();
                        ShowSuccessMessage("user Saved Succesfully");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showTheWorld", "showSuccessModal()", true);
                        LoadContacts();
                        ClearForm();

                    }
                }

                // Clear the form and show success message
                
               // Use this for the success modal trigger
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error saving contact: " + ex.ToString());
                ShowErrorMessage("Error saving contact: " + ex.Message);
            }
        }




        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            ddlCountryCode.SelectedIndex = 0;
            txtPhoneNumber.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtNotes.Text = string.Empty;
        }

        private void ShowSuccessMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Visible = true;
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-danger";
            lblMessage.Visible = true;
        }
    }
}
