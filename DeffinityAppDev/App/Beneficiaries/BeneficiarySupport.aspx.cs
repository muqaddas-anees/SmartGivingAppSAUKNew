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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
         
                // Register the startup script to show the success modal and redirect after delay

    
         
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
            ddlDonated.Items.Insert(0, new ListItem("Select a Fundraiser", ""));
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
                var donations = context.BeneficiaryDonations.Where(o => o.TithingID == sessionKeys.PortfolioID)
                    .Select(d => new
                    {
                        d.DonationID,
                        d.DonationDate,
                        d.AssociatedFundraiser,
                        d.DonatedBy,
                        d.DonationAmount,
                        d.Currency,
                        d.Notes
                    }).ToList();

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

        private bool SaveDonation()
        {
            using (var context = new MyDatabaseContext())
            {
                try
                {
                    var donation = new BeneficiaryDonation
                    {
                        DonationDate = DateTime.Parse(txtDonationDate.Text),
                        LoggedBy = ddlLogged.SelectedValue,
                        AssociatedFundraiser = ddlAssociatedFundraise.SelectedValue,
                        DonationAmount = decimal.Parse(txtDonationAmount.Text),
                        Currency = ddlCurrency.SelectedValue,
                        PaymentType = GetSelectedPaymentType(),
                        DonatedBy = ddlDonated.SelectedValue,
                        Notes = txtDonationNotes.Text,
                        TithingID = sessionKeys.PortfolioID,
                        DocumentUpload = SaveUploadedFile()
                    };

                    // Add the new donation and save changes to the database
                    context.BeneficiaryDonations.Add(donation);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException ex) // Database update errors
                {
                    var innerException = ex.InnerException?.InnerException;
                    if (innerException != null)
                    {
                      
                        System.Diagnostics.Debug.WriteLine($"Inner Exception: {innerException.Message}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("error eadfkdslk");
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