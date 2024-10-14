using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.ClientServices.Providers;
namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiaryCommunication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                BindFeedbackData();
            }
            string beneficiaryIdStr = Request.QueryString["beneficiaryId"];
            if (!string.IsNullOrEmpty(beneficiaryIdStr) && int.TryParse(beneficiaryIdStr, out int beneficiaryId))
            {
                // Serve the attachment
                ServeAttachment(beneficiaryId);
            }
            // Check if we need to show the success modal
          

                // Register the startup script to show the success modal and redirect after delay
            
                
          

            
        }

        private void ServeAttachment(int beneficiaryId)
        {
            using (var context = new MyDatabaseContext())
            {
                var attachment = context.BeneficiariesFeedBack
                    .Where(f => f.FeedbackID == beneficiaryId && f.TithingID == sessionKeys.PortfolioID)
                    .Select(f => f.Attachments)
                    .FirstOrDefault();

                if (attachment != null)
                {
                    byte[] fileBytes = attachment;

                    // Infer file type (for this example, we'll handle JPG, PNG, and PDF)
                    string fileType = GetFileTypeFromBytes(fileBytes);

                    // Set the appropriate content type
                    Response.ContentType = fileType;
                    string extension = GetFileExtensionFromMimeType(fileType);

                    // Set Content-Disposition header to inline or attachment
                    Response.AddHeader("Content-Disposition", $"inline; filename=Attachment_{beneficiaryId}.{extension}");

                    // Clear the response and write the file bytes
                    Response.Clear();
                    Response.BinaryWrite(fileBytes);
                    Context.ApplicationInstance.CompleteRequest();  // Replaced Response.End()
                }
                else
                {
                    // Handle the case where there is no attachment
                    Response.StatusCode = 404;
                    Response.Write("Attachment not found.");
                    Context.ApplicationInstance.CompleteRequest();  // Replaced Response.End()
                }
            }
        }

        private string GetFileTypeFromBytes(byte[] fileBytes)
        {
            // Check for PDF magic number (first 4 bytes)
            if (fileBytes.Length > 4 && fileBytes[0] == 0x25 && fileBytes[1] == 0x50 && fileBytes[2] == 0x44 && fileBytes[3] == 0x46)
            {
                return "application/pdf";
            }

            // Check for JPG magic number (first 3 bytes: FF D8 FF)
            if (fileBytes.Length > 3 && fileBytes[0] == 0xFF && fileBytes[1] == 0xD8 && fileBytes[2] == 0xFF)
            {
                return "image/jpeg";
            }

            // Check for PNG magic number (first 8 bytes: 89 50 4E 47 0D 0A 1A 0A)
            if (fileBytes.Length > 8 && fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
            {
                return "image/png";
            }

            // Default to octet-stream if unknown
            return "application/octet-stream";
        }

        private string GetFileExtensionFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "application/pdf":
                    return "pdf";
                case "image/jpeg":
                    return "jpg";
                case "image/png":
                    return "png";
                default:
                    return "bin"; // Default binary extension for unknown types
            }
        }

        protected void btnSaveCommunication_Click(object sender, EventArgs e)
        {
            

            // Validate inputs before saving
            if (string.IsNullOrEmpty(txtCommunicationDate.Text) || string.IsNullOrEmpty(txtCommunicationText.Text))
            {
                // Display validation error
                lblMessage.Text = "Date and Feedback are required.";
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
                return;
            }

            // Save communication to the database using LINQ
            bool isSaved = SaveCommunication();
            if (isSaved)
            {
               

                // Bind data and clear the form
                BindFeedbackData();
                ClearForm();
              

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "User Saved Succesfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalAndRedirect", "showSuccessModal()", true);

            }
            else
            {
                lblMessage.Text = "An error occurred while saving feedback.";
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }

        private bool SaveCommunication()
        {
            using (var context = new MyDatabaseContext())
            {
                // Create a new feedback object
                var communication = new BeneficiariesFeedBack
                {
                    FeedbackDate = DateTime.Parse(txtCommunicationDate.Text),
                    FeedbackText = txtCommunicationText.Text,
                    Attachments = SaveUploadedFiles(), // Assuming this method returns a byte[]
                    CreatedAt = DateTime.Now,
                    Deleted = false,
                    TithingID = sessionKeys.PortfolioID// Set this based on your logic
                };

                try
                {
                    // Add the new feedback to the context and save changes
                    context.BeneficiariesFeedBack.Add(communication);
                    context.SaveChanges(); // This will persist changes to the database
                    return true; // Return true if successful
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    // Log the error or handle it appropriately
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return false; // Return false if there is an error
                }
            }
        }

        private byte[] SaveUploadedFiles()
        {
            if (fileUploadDocuments.HasFiles)
            {
                // Handle the first file for simplicity
                var uploadedFile = fileUploadDocuments.PostedFiles[0];

                if (uploadedFile != null)
                {
                    using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                    {
                        byte[] fileData = br.ReadBytes(uploadedFile.ContentLength);  // Convert file to byte array
                        return fileData;
                    }
                }
            }
            return null;
        }

        private void ClearForm()
        {
            // Clear all form fields after saving
            txtCommunicationDate.Text = string.Empty;
            txtCommunicationText.Text = string.Empty;
            fileUploadDocuments.Dispose();  // Reset the file input
          
        }

        protected void RepeaterFeedback_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int feedbackID = Convert.ToInt32(e.CommandArgument);
                DeleteFeedback(feedbackID);
                BindFeedbackData(); // Refresh the data
            }
        }

        private void DeleteFeedback(int feedbackID)
        {
            using (var context = new MyDatabaseContext())
            {
                // Find the feedback entry by its ID
                var feedbackToDelete = context.BeneficiariesFeedBack.SingleOrDefault(f => f.FeedbackID == feedbackID && f.TithingID==sessionKeys.PortfolioID);

                if (feedbackToDelete != null)
                {
                    // Remove the feedback entry from the context
                    context.BeneficiariesFeedBack.Remove(feedbackToDelete);

                    // Save changes to the database
                    context.SaveChanges();
                }
                else
                {
                    // Handle the case where the feedback entry is not found
                    System.Diagnostics.Debug.WriteLine($"Feedback with ID {feedbackID} not found.");
                }
            }
        }


        [WebMethod]
        public static string GetAttachmentByBeneficiaryId(int beneficiaryId)
        {
            System.Diagnostics.Debug.WriteLine("Fetching attachment for BeneficiaryID: " + beneficiaryId);
            string base64Image = string.Empty;

            using (var context = new MyDatabaseContext())
            {
                // Find the feedback entry by ID
                var feedback = context.BeneficiariesFeedBack.SingleOrDefault(f => f.FeedbackID == beneficiaryId && f.TithingID == sessionKeys.PortfolioID);

                if (feedback != null && feedback.Attachments != null)
                {
                    // Convert the byte array to a Base64 string
                    base64Image = Convert.ToBase64String(feedback.Attachments);
                }
            }

            return string.IsNullOrEmpty(base64Image) ? "" : "data:image/jpeg;base64," + base64Image; // Adjust the MIME type if needed
        }



        private void BindFeedbackData()
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch the TithingID from session
              

                // Fetch feedback data using LINQ with the TithingID filter
                var feedbackData = context.BeneficiariesFeedBack
                    .Where(f => f.TithingID == sessionKeys.PortfolioID)
                    .Select(f => new
                    {
                        f.FeedbackID,
                        f.FeedbackDate,
                        f.FeedbackText,
                        f.Attachments
                    })
                    .ToList();

                // Bind the data to the Repeater
                RepeaterFeedback.DataSource = feedbackData;
                RepeaterFeedback.DataBind();
            }
        }



    }
}
