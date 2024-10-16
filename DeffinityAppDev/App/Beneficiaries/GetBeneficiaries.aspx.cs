using DeffinityAppDev.App.Beneficiaries;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class GetBeneficiaries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
            if (!IsPostBack)
            {
                LoadBeneficiaries();
                LoadSecondaryBeneficiaries();

            }
            }catch(Exception ex)

            {
                LogExceptions.WriteExceptionLog(ex);
            }



        }

        public string GetProfileImage(object profileImage)
        {
            if (profileImage != DBNull.Value && profileImage != null)
            {
                byte[] imageBytes = (byte[])profileImage;
                return "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            }
            return "/metronic/8/default.jpeg";
        }

        protected void DeleteButtonForBeneficiaries_Click(object sender,EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int personID = int.Parse(btn.CommandArgument);
            DeleteBeneficiaries(personID);
            LoadBeneficiaries();
            
        }
        protected void DeleteBeneficiaries(int id)
        {
            using (var context= new MyDatabaseContext())
            {
                var beneficiaries = context.Beneficiaries.Find(id);
                if (beneficiaries != null)
                {
                    context.Beneficiaries.Remove(beneficiaries);
                    context.SaveChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Deleted Successfully", "OK");

                }
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            // Get the CommandArgument (SecondaryBeneficiaryID)
            LinkButton btn = (LinkButton)sender;
            int secondaryBeneficiaryId = int.Parse(btn.CommandArgument);

            // Call the delete method
            DeleteSecondaryBeneficiary(secondaryBeneficiaryId);

            // Optionally, refresh the data on the page
            LoadSecondaryBeneficiaries();  // Assuming you have a method to reload the page data
        }

        private void DeleteSecondaryBeneficiary(int id)
        {
            using (var context = new MyDatabaseContext())
            {
                // Find the record
                var secondaryBeneficiary = context.SecondaryBeneficiary.Find(id);
                if (secondaryBeneficiary != null)
                {
                    // Remove the record
                    context.SecondaryBeneficiary.Remove(secondaryBeneficiary);
                    context.SaveChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Deleted Successfully", "OK");
                }
            }

            
        }


        private void LoadBeneficiaries()
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch beneficiaries where TithingDefaultDetailsID = 1
                var beneficiaries = context.Beneficiaries
                    .Where(b => b.TithingDefaultDetailsID == sessionKeys.PortfolioID
                                && (b.Name != null || b.Country != null))
                    .ToList();

                // Bind the beneficiaries list to your Repeater/GridView
                BeneficiariesRepeater.DataSource = beneficiaries;
                BeneficiariesRepeater.DataBind();
            }
        }

        private void LoadSecondaryBeneficiaries()
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch secondary beneficiaries where TithingID = 1
                var secondaryBeneficiaries = context.SecondaryBeneficiary
                                                    .Where(sb => sb.TithingID == sessionKeys.PortfolioID)
                                                    .ToList();

                // Bind the secondary beneficiaries list to your Repeater/GridView
                SecondaryBeneficiariesRepeater.DataSource = secondaryBeneficiaries;
                SecondaryBeneficiariesRepeater.DataBind();
            }
        }

        // Event handler for item commands in both repeaters
        protected void RepeaterBeneficiaries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Get the ID from CommandArgument
                var beneficiaryID = e.CommandArgument.ToString();
                // Redirect or perform edit action
                Response.Redirect($"EditBeneficiary.aspx?BeneficiaryID={beneficiaryID}");
            }
        }

        protected void RepeaterSecondaryBeneficiaries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Get the ID from CommandArgument
                var secondaryBeneficiaryID = e.CommandArgument.ToString();
                // Redirect or perform edit action
                Response.Redirect($"EditSecondaryBeneficiary.aspx?BeneficiaryID={secondaryBeneficiaryID}");
            }
        }
    }
}
