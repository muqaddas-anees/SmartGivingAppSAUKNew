using DeffinityAppDev.App.Beneficiaries;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System;
using System.Linq;
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

        private void LoadBeneficiaries()
        {
            using (var context = new MyDatabaseContext())
            {
                // Fetch beneficiaries where TithingDefaultDetailsID = 1
                var beneficiaries = context.Beneficiaries
                                           .Where(b => b.TithingDefaultDetailsID == 1)
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
                                                    .Where(sb => sb.TithingID == 1)
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
