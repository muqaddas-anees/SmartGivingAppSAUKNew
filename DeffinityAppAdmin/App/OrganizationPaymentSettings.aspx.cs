using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class OrganizationPaymentSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                var p = prep.GetAll().FirstOrDefault();
                if (p != null)
                {
                    txtMarchantID.Text = p.Payment_Merchant_ID;
                    txtMarchantKey.Text = p.Payment_Merchant_key;
                    txtPercent.Text = (p.Paymet_Percentage ?? 0.00).ToString();
                    ddlHost.SelectedValue = p.Payment_Host ==  null?"": p.Payment_Host;
                    txtSaltPass.Text = p.Payment_Salt_Pass;
                   

                }
            }

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> prep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                var p = prep.GetAll().FirstOrDefault();
                if(p != null)
                {
                    p.Payment_Merchant_ID=txtMarchantID.Text;
                    p.Payment_Merchant_key = txtMarchantKey.Text;
                    p.Paymet_Percentage = Convert.ToDouble( txtPercent.Text.Trim().Length>0?txtPercent.Text.Trim():"0");
                    p.Payment_Host=ddlHost.SelectedValue ;
                    p.Payment_Salt_Pass= txtSaltPass.Text;
                    prep.Edit(p);
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Updated Successfully", "Ok");

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}