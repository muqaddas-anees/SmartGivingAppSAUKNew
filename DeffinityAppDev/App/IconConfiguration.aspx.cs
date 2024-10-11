using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class IconConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindIconRanges();
            }

        }

         protected string GetImageUrl(object id)
        {
            // Assuming you have images stored somewhere with the ID as part of the filename or path
            int iconRangeId = Convert.ToInt32(id);
            return "~/imagehandler.ashx?s=Icon&id=" + iconRangeId; // Adjust the path as per your image storage setup
        }

        private void BindIconRanges(string country = "")
        {
            using (var context = new PortfolioDataContext())
            {
                var iconRanges = context.IconRanges.Where(o=>o.PortfolioID==sessionKeys.PortfolioID.ToString()).ToList();
                if(iconRanges.Any())
                { 
                gvLevels.DataSource = iconRanges;
                gvLevels.DataBind();


                    return;
                }
              
           
            }
        }
        protected void btnSaveLevel_Click(object sender, EventArgs e)
        {
            // Retrieve values from the modal's input fields
            string lowerLevel = txtFromRange.Text.Replace(".", "").Replace(",", "");
            string upperLevel = txtToRange.Text.Replace(".", "").Replace(",", "");
            int? levelId = string.IsNullOrEmpty(hfLevelId.Value) ? (int?)null : int.Parse(hfLevelId.Value);

            using (var context = new PortfolioDataContext())
            {
                IconRange iconRange;

                if (levelId.HasValue) // Editing existing level
                {
                    iconRange = context.IconRanges.FirstOrDefault(ir => ir.Id == levelId.Value);
                    if (iconRange == null)
                    {
                        // Handle case where the icon range is not found
                        return;
                    }
                }
                else // Adding a new level
                {
                    iconRange = new IconRange();
                    context.IconRanges.InsertOnSubmit(iconRange);
                }

                // Set or update the icon range properties
                iconRange.FromRange = int.Parse(lowerLevel);
                iconRange.ToRange = int.Parse(upperLevel);
                iconRange.PortfolioID = sessionKeys.PortfolioID.ToString();

                context.SubmitChanges(); // Save changes to the database

                // Upload the file if provided
                UploadFile(iconRange.Id, context);
            }

            // Rebind the GridView to reflect the updated data
            BindIconRanges();



            // Hide the modal after saving
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModalScript", "$('#addLevelModal').modal('hide');", true);
        }


        private void UploadFile(int ID, PortfolioDataContext context)
        {
            // Check if a file has been uploaded using the FileUpload control
            if (Image.HasFile)
            {
                // Retrieve the file from the database where FileID matches and the section is "Icon"
                var file = context.FileDatas.FirstOrDefault(o => o.FileID == ID.ToString() && o.Section == "Icon");

                if (file == null)
                {
                    // If file does not exist, create a new instance of the FileData object
                    file = new FileData
                    {
                        FileID = ID.ToString(), // Set the FileID
                        Section = "Icon" // Set the section as "Icon"
                    };

                    // Add the new file object to the context
                    context.FileDatas.InsertOnSubmit(file);
                }

                // Convert the uploaded file to a byte array
                using (var binaryReader = new BinaryReader(Image.PostedFile.InputStream))
                {
                    file.FileData1 = binaryReader.ReadBytes(Image.PostedFile.ContentLength); // Store file data in FileData1 column
                }

                // Update file details
                file.FileName = Image.FileName; // Name of the uploaded file
                file.FileType = Image.PostedFile.ContentType; // File MIME type (e.g., image/jpeg)
                file.ContentLength = Image.PostedFile.ContentLength; // File size in bytes
                file.FileExtenstion = Path.GetExtension(Image.FileName); // File extension (e.g., .jpg, .png)

                // Save changes to the database
                context.SubmitChanges();
            }
        }
        protected void gvLevels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            // int id = Convert.ToInt32(gvLevels.DataKeys[rowIndex].Value);

            if (e.CommandName == "EditLevel")
            {
                // Handle Edit action
                EditLevel(rowIndex);
            }
            else if (e.CommandName == "DeleteLevel")
            {
                // Handle Delete action
                DeleteLevel(rowIndex);
                BindIconRanges();
            }
        }

        private void EditLevel(int id)
        {
            using (var context = new PortfolioDataContext())
            {
                // Fetch the icon range data for the selected row
                var iconRange = context.IconRanges.FirstOrDefault(o => o.Id == id);

                if (iconRange != null)
                {
                    // Populate the modal fields with existing data for editing
                    txtFromRange.Text = iconRange.FromRange.ToString();
                    txtToRange.Text = iconRange.ToRange.ToString();

                    // Assign the selected row's id to the hidden field
                    hfLevelId.Value = id.ToString();

                    // Call the JavaScript function to show the modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowEditModal", "showEditModal();", true);
                }
            }
        }

        private void DeleteLevel(int id)
        {
            using (var context = new PortfolioDataContext())
            {
                // Fetch the icon range data for the selected row
                var iconRange = context.IconRanges.FirstOrDefault(o => o.Id == id);

                if (iconRange != null)
                {
                    // Delete the icon range from the database
                    context.IconRanges.DeleteOnSubmit(iconRange);
                    context.SubmitChanges();

                    // Rebind the GridView to reflect changes
                    BindIconRanges();
                }
            }
        }

    }
}