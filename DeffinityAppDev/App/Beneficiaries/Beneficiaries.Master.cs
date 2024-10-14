using ClosedXML.Excel;
using DeffinityAppDev.App.Beneficiaries.Entities;
using Infragistics.WebUI.Shared.Util;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class Beneficiaries : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string personID = Request.QueryString["PersonID"];

                // Check if PersonID is present in the query string
                if (!string.IsNullOrEmpty(personID))
                {
                    // PersonID exists, make buttons visible
                    btnUpload.Visible = true;
                    btnDelete.Visible = true;

                    // Fetch beneficiary information
                    LoadBeneficiaryInfo(personID);
                }
                else
                {
                    // No PersonID, hide the buttons
                    btnUpload.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        private void LoadBeneficiaryInfo(string personID)
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch the beneficiary using the PersonID
                int id = int.Parse(personID);
                Beneficiary beneficiary = context.Beneficiaries.SingleOrDefault(b => b.PersonID == id);

                if (beneficiary != null)
                {
                    // Check if the ProfileImage is not null and convert it to a base64 string for display
                    if (beneficiary.ProfileImage != null)
                    {
                       

                       
                        string base64Image = Convert.ToBase64String(beneficiary.ProfileImage);
                        string imageUrl = $"data:image/png;base64,{base64Image}"; // Assuming the image is PNG
                                                                                  // Update the src attribute of the image element
                        if (profileImage != null) // Check if profileImage is not null
                        {
                            profileImage.Attributes["src"] = imageUrl; // Update the src attribute of the image element
                        }
                    }

                    // Set the address (assuming you have a Label or TextBox to display it)
                    if (lblAddress != null)
                    {
                        lblAddress.Text = beneficiary.Address;
                    }

                    lblName.Text = beneficiary.Name;// Make sure you have a control named lblAddress
                }
                else
                {
                    // Handle case where beneficiary is not found
                  
                    profileImage.Attributes["src"] = ""; // Clear the image if not found
                }
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
           
            try
            {
                // Debug: Start of upload process

                // Get the base64 string from the hidden field
                string base64Image = hfBase64Image.Value;

                if (!string.IsNullOrEmpty(base64Image))
                {
                    // Decode the base64 string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(base64Image);

                    using (var context = new MyDatabaseContext())
                    {
                        // Retrieve PersonID from query string
                        string personID = Request.QueryString["PersonID"];

                        if (!string.IsNullOrEmpty(personID))
                        {
                            int id = int.Parse(personID);

                            // Find the corresponding beneficiary
                            Beneficiary beneficiary = context.Beneficiaries.SingleOrDefault(b => b.PersonID == id);

                            if (beneficiary != null)
                            {
                                // Update the ProfileImage column
                                beneficiary.ProfileImage = imageBytes;
                                context.SaveChanges();
                                string updatedImageBase64 = Convert.ToBase64String(imageBytes);
                                System.Web.UI.ScriptManager.RegisterStartupScript(this,this.GetType(), "UpdateImage", $"updateImage('{updatedImageBase64}');", true);
                                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Uploaded Successfully", "OK");
                            }
                            else
                            {
                                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Beneficiary not found!", "Error");
                            }
                        }
                        else
                        {
                            DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Invalid PersonID.", "Error");
                        }
                    }
                }
                else
                {
                    DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "No image selected. Please choose an image to upload.", "Error");
                }
            }
            catch (Exception ex)
            {
                // Generic error handling
                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "An error occurred during upload.", "Error");
                LogExceptions.WriteExceptionLog(ex);
            }
          
        }


        // Event handler for the upload button
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (var context = new MyDatabaseContext())
            {
                // Retrieve PersonID from query string
                string personID = Request.QueryString["PersonID"];
                if (!string.IsNullOrEmpty(personID))
                {
                    int id = int.Parse(personID);

                    // Find the corresponding beneficiary
                    Beneficiary beneficiary = context.Beneficiaries.SingleOrDefault(b => b.PersonID == id);

                    if (beneficiary != null)
                    {
                        // Set the ProfileImage column to null or an empty byte array
                        beneficiary.ProfileImage = null; // Or new byte[0] if you prefer an empty byte array

                        context.SaveChanges();

                    
                    }
                    else
                    {
                       
                    }
                }
            }
        }


        // Event handler for the delete button
     
    }
}
