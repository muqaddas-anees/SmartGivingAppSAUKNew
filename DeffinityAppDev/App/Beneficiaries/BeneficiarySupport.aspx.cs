using AngleSharp.Text;
using DeffinityAppDev.App.Beneficiaries.Entities;
using Microsoft.Ajax.Utilities;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Animation;
using UserMgt.BAL;
using UserMgt.DAL;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiarySupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BindDonations();
                BindLoggedByDropdown();
                BindBeneficiariesByDropDown();
              BindDonorsByDropDown();
            }

            else if (Request["__EVENTTARGET"] == "EditDonations")
            {
                string donationID = Request["__EVENTARGUMENT"];
                LoadDonationDetails(donationID); // Load beneficiary details

                // Set hidden field value to "true"
                hfShowModal.Value = "true";
            }

            // Register the startup script to show the success modal and redirect after delay



        }

        protected void LoadDonationDetails(string  donationID)
        {
            using (var context = new MyDatabaseContext())
            {
                var donations=context.BeneficiaryDonations.FirstOrDefault(c => c.DonationID.ToString()==donationID);
                if (donations != null)
                {
                    // Check if DonationDate has a valid value
                    if (donations.DonationDate != null)
                    {
                        // Assign the date in the correct format to the TextBox (TextMode="Date" expects yyyy-MM-dd)
                        txtDonationDate.Text = ((DateTime)donations.DonationDate).ToString("yyyy-MM-dd");
                    }
                    ddlLogged.SelectedValue = donations.LoggedBy;
                    ddlAssociatedFundraise.SelectedValue = donations.AssociatedFundraiser;
                    ddlCurrency.SelectedValue = donations.Currency;
                    ddlDonated.SelectedValue = donations.DonatedBy;
                    ddlCurrency.SelectedValue=donations.Currency;
                    txtDonationAmount.Text = donations.DonationAmount.ToString();
                    txtDonationNotes.Text = donations.Notes;
                    if (donations.PaymentType == "One Off")
                    {
                        rbOneOff.Checked=true;
                    }
                    else if (donations.PaymentType == "Monthly")
                    {
                        rbMonthly.Checked = true;
                    }
                    else if (donations.PaymentType == "Annual")
                    {
                        rbAnnual.Checked = true;
                    }
               


                }
            }

        }
        protected void BindDonorsByDropDown()
        {

            var iList = ContractorsBAL.Contractor_SelectAll_WithOutCompany()
  .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.SID == UserType.Donor)
  .OrderBy(o => o.ContractorName)
  .ToList();
            ddlDonated.DataSource = iList;
            ddlDonated.DataTextField = "ContractorName"; // assign the table column 
            ddlDonated.DataBind(); // Don't forget to call DataBind after setting DataSource
            ddlDonated.Items.Insert(0, new ListItem("Select a Donor", ""));
        }

        protected void BindBeneficiariesByDropDown()
        {
            PortfolioDataContext tag = new PortfolioDataContext();
            var associatedFundraiserDetailsList = tag.TithingDefaultDetails.Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
            ddlAssociatedFundraise.DataSource = associatedFundraiserDetailsList;
            ddlAssociatedFundraise.DataTextField = "Title"; // assign the table column 
            ddlAssociatedFundraise.DataBind(); // Don't forget to call DataBind after setting DataSource
            ddlAssociatedFundraise.DataValueField = "Title";
            ddlAssociatedFundraise.Items.Insert(0, new ListItem("Select a Fundraiser", ""));
        }

        protected void BindLoggedByDropdown()
        {
            try
            {
                UserDataContext tags = new UserDataContext();
                var contractorsList = tags.v_contractors
                    .Where(o => o.CompanyID == sessionKeys.PortfolioID)
                    .ToList();

                ddlLogged.DataSource = contractorsList;
                ddlLogged.DataTextField = "ContractorName"; // Update this line
                ddlLogged.DataValueField = "ContractorName"; // Ensure ID is correct as well
                ddlLogged.DataBind(); // Don't forget to call DataBind after setting DataSource
                ddlLogged.Items.Insert(0, new ListItem("Select a person", "")); // Add the default item if needed
            }           catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindDonations()
        {
            using (var context = new MyDatabaseContext())
            {
                // Using LINQ to get donations from the database
                string BeneficiaryID = Request.QueryString["personid"];
                var donations = context.BeneficiaryDonations.Where(o => o.TithingID == sessionKeys.PortfolioID && o.PrimaryBeneficiaryID==BeneficiaryID)
                   .ToList();
                for(int i=0;i<donations.Count;i++)
                {
                    donations[i].AssociatedFundraiser = Helper.GetFundraiserNamebyID(donations[i].AssociatedFundraiser);
                    donations[i].DonatedBy = Helper.GetPersonNamebyID(donations[i].DonatedBy);


                }
                // Binding the retrieved data to the Repeater control
                RepeaterDonations.DataSource = donations;
                RepeaterDonations.DataBind();
            }
        }




            protected void btnSaveDonation_Click(object sender, EventArgs e)
        {
          
            // Check if the page is valid
            if (Page.IsValid)
            {
                // Try to save the donation details
                bool isSaved = SaveDonation();

                if (isSaved)
                {
                    // Show success message
                    ShowSuccessMessage("Donation saved successfully.");

                    // Clear form fields after successful submission
                    ClearForm();
                    BindDonations();
                    // Mark the form as submitted to prevent duplicate submissions
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "User Saved Succesfully";
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved SuccessFully ", "OK");

                }
            }
        }
        protected void DeleteButtonForBeneficiaries_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int donationID = int.Parse(btn.CommandArgument);
            DeleteDonations(donationID);
            BindDonations();

        }
        protected void DeleteDonations(int id)
        {
            using (var context = new MyDatabaseContext())
            {
                var donater= context.BeneficiaryDonations.Find(id);
                if (donater!= null)
                {
                    context.BeneficiaryDonations.Remove(donater);
                    context.SaveChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Deleted Successfully", "OK");

                }
            }
        }
        private bool SaveDonation()
        {
            using (var context = new MyDatabaseContext())
            {
                try
                {
                    string BeneficiaryID = Request.QueryString["personid"];
                    // Check if hfDonationID has a value (indicating an edit operation)
                    if (!string.IsNullOrEmpty(hfDonationID.Value))
                    {
                        // Attempt to retrieve the existing donation by ID
                        var donationId = int.Parse(hfDonationID.Value); // Assuming the ID is an integer
                        var donation = context.BeneficiaryDonations.FirstOrDefault(d => d.DonationID == donationId);

                        if (donation != null)
                        {
                            // Update the existing donation
                            // Attempt to parse the DonationDate
                            DateTime donationDate;
                            if (DateTime.TryParse(txtDonationDate.Text, out donationDate))
                            {
                                donation.DonationDate = donationDate;
                            }
                            else
                            {
                                // Handle the parsing failure, e.g., log an error, set a default date, or show a message
                                donation.DonationDate = DateTime.Now; // Set a default date or handle accordingly
                            }

                            // Attempt to parse the DonationAmount
                            decimal donationAmount;
                            if (decimal.TryParse(txtDonationAmount.Text, out donationAmount))
                            {
                                donation.DonationAmount = donationAmount;
                            }
                            else
                            {
                                // Handle the parsing failure, e.g., log an error, set a default amount, or show a message
                                donation.DonationAmount = 0; // Set a default amount or handle accordingly
                            }

                            // Assign remaining values
                            donation.PrimaryBeneficiaryID = BeneficiaryID;
                            donation.LoggedBy = ddlLogged.SelectedValue;
                            donation.AssociatedFundraiser = ddlAssociatedFundraise.SelectedValue;
                            donation.Currency = ddlCurrency.SelectedValue;
                            donation.PaymentType = GetSelectedPaymentType();
                            donation.DonatedBy = ddlDonated.SelectedValue;
                            donation.Notes = txtDonationNotes.Text;
                            donation.TithingID = sessionKeys.PortfolioID;


                            // Optionally update the document if a new file is uploaded
                            if (FileUploadDocuments.HasFile)
                            {
                                donation.DocumentUpload = SaveUploadedFile();
                            }

                            // Display the existing image in the imgPreview control
                            if (donation.DocumentUpload != null)
                            {
                                string base64String = Convert.ToBase64String(donation.DocumentUpload);
                                imgPreview.ImageUrl = "data:image/png;base64," + base64String; // You can change 'png' to appropriate file type
                                imgPreview.Visible = true;

                            }
                            // Optionally handle the document upload if needed

                        }
                    }
                    else
                    {
                        // Create a new donation
                        // Initialize variables with default values
                        DateTime donationDate = DateTime.Now; // Default to current date
                        decimal donationAmount = 0; // Default amount

                        // Attempt to parse DonationDate
                        if (DateTime.TryParse(txtDonationDate.Text, out var parsedDate))
                        {
                            donationDate = parsedDate;
                        }
                        else
                        {
                            // Handle parsing failure if needed (e.g., logging, notification)
                        }

                        // Attempt to parse DonationAmount
                        if (decimal.TryParse(txtDonationAmount.Text, out var parsedAmount))
                        {
                            donationAmount = parsedAmount;
                        }
                        else
                        {
                            // Handle parsing failure if needed (e.g., logging, setting default)
                        }

                        var donation = new BeneficiaryDonation
                        {
                            DonationDate = donationDate, // Use the parsed date
                            LoggedBy = ddlLogged.SelectedValue,
                            AssociatedFundraiser = ddlAssociatedFundraise.SelectedValue,
                            DonationAmount = donationAmount, // Use the parsed amount
                            Currency = ddlCurrency.SelectedValue,
                            PaymentType = GetSelectedPaymentType(),
                            DonatedBy = ddlDonated.SelectedValue,
                            PrimaryBeneficiaryID = BeneficiaryID,
                            Notes = txtDonationNotes.Text,
                            TithingID = sessionKeys.PortfolioID,
                            CreatedAt = DateTime.Now.Date,
                            DocumentUpload = SaveUploadedFile() // Assuming this returns a valid file path or object
                        };

                        // Add the new donation to the context
                        context.BeneficiaryDonations.Add(donation);
                    }

                    // Save changes to the database
                    context.SaveChanges();
                    hfDonationID.Value = string.Empty;
                    return true;
                }
                catch (DbUpdateException ex) // Handle database update errors
                {
                    var innerException = ex.InnerException?.InnerException;
                    if (innerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Inner Exception: {innerException.Message}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error occurred during donation save.");
                    }
                    System.Diagnostics.Debug.WriteLine($"Error occurred: {ex.Message}");
                }
                return false;
            }
        }

        private byte[] SaveUploadedFile()
        {
            // Assuming FileUploadDocuments is the FileUpload control for uploading a single file
            if (FileUploadDocuments.HasFile)
            {
                try
                {
                    // Get the uploaded file
                    var uploadedFile = FileUploadDocuments.PostedFile;

                    if (uploadedFile != null && uploadedFile.ContentLength > 0)
                    {
                        using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                        {
                            // Convert the file into a byte array
                            byte[] fileData = br.ReadBytes(uploadedFile.ContentLength);
                            return fileData;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while reading the uploaded file: " + ex.Message);
                }
            }

            return null; // No valid file uploaded
        }
        private void ClearForm()
        {
            // Clear all form fields
            txtDonationDate.Text = string.Empty;
            ddlLogged.SelectedValue = string.Empty;
            ddlAssociatedFundraise.SelectedValue= string.Empty;
            txtDonationAmount.Text = string.Empty;
            ddlCurrency.SelectedIndex = 0;
            rbOneOff.Checked = false;
            rbMonthly.Checked = false;
            rbAnnual.Checked = false;
            ddlDonated.SelectedValue = string.Empty;
            txtDonationNotes.Text = string.Empty;
        }
        private string GetSelectedPaymentType()
        {
            if (rbOneOff.Checked) return "One Off";
            if (rbMonthly.Checked) return "Monthly";
            if (rbAnnual.Checked) return "Annual";
            return "Unknown";
        }
        private void ShowSuccessMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-success";
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
//please do something here i thing there is an issue of saving can you please solve it 