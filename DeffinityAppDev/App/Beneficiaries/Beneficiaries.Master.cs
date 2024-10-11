using System;
using System.Web.UI;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class Beneficiaries : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if it's a postback
            if (!IsPostBack)
            {
                // Initialize or perform actions for the first page load
            }
        }

        // Event handler for the upload button
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // Add logic for the upload button
            Response.Write("Upload button clicked.");
        }

        // Event handler for the delete button
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Add logic for the delete button
            Response.Write("Delete button clicked.");
        }
    }
}
