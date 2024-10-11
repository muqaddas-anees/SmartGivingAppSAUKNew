using DeffinityAppDev.App.Beneficiaries.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiarySupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BindDonations();
            }
         
                // Register the startup script to show the success modal and redirect after delay

    
         
        }

        private void BindDonations()
        {
            using (var context = new MyDatabaseContext())
            {
                // Using LINQ to get donations from the database
                var donations = context.BeneficiaryDonations
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showMe", "showSuccessModal()", true);

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
                        LoggedBy = ddlLoggedBy.Text,
                        AssociatedFundraiser = ddlAssociatedFundraiser.Text,
                        DonationAmount = decimal.Parse(txtDonationAmount.Text),
                        Currency = ddlCurrency.SelectedValue,
                        PaymentType = GetSelectedPaymentType(),
                        DonatedBy = ddlDonatedBy.Text,
                        Notes = txtDonationNotes.Text,
                        TithingID = 1,
                        DocumentUpload = SaveUploadedFile()
                    };

                    // Add the new donation and save changes to the database
                    context.BeneficiaryDonations.Add(donation);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while saving the donation: " + ex.Message);
                    return false;
                }
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
            ddlLoggedBy.Text = string.Empty;
            ddlAssociatedFundraiser.Text = string.Empty;
            txtDonationAmount.Text = string.Empty;
            ddlCurrency.SelectedIndex = 0;
            rbOneOff.Checked = false;
            rbMonthly.Checked = false;
            rbAnnual.Checked = false;
            ddlDonatedBy.Text = string.Empty;
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