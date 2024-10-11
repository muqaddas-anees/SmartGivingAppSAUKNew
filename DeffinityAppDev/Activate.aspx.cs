using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using AngleSharp.Dom;
using DeffinityAppDev;
using DeffinityManager.BLL;
using DocumentFormat.OpenXml.Wordprocessing;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> fRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                    var p = fRep.GetAll().Where(o => o.OrgarnizationGUID == QueryStringValues.UNID).FirstOrDefault();

                    if(p != null)
                    {
                        lblInstance.Text = Deffinity.systemdefaults.GetInstanceTitle();
                        lblInstance1.Text = p.PortFolio;
                        txtemail.Text = p.EmailAddress;
                        var uDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(p.Owner.Value);
                        if (uDetails != null)
                        {
                           




                        }

                    }

                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> fRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                IUserRepository<UserMgt.Entity.Contractor> cRep = new UserRepository<UserMgt.Entity.Contractor>();

                var p = fRep.GetAll().Where(o => o.OrgarnizationGUID == QueryStringValues.UNID).FirstOrDefault();

                if (p != null)
                {
                    try
                    {
                        p.OrgarnizationStatus = "Approved";
                        p.OrgarnizationApproval = "Approved";
                        p.DateStamp = DateTime.Now;
                        fRep.Edit(p);
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }


                    lblInstance.Text = Deffinity.systemdefaults.GetInstanceTitle();
                    lblInstance1.Text = p.PortFolio;
                    txtemail.Text = p.EmailAddress;
                    var uDetails = cRep.GetAll().Where(o => o.ID == (p.Owner.Value)).FirstOrDefault();

                    if (uDetails != null)
                    {
                        var pwd = txtpwd.Text.Trim();
                        Deffinity.Users.Login.GeneratePasswordString(pwd);

                        uDetails.Password = Deffinity.Users.Login.GeneratePasswordString(pwd);
                        uDetails.TypeofLogin = txtpwd.Text.Trim();
                        uDetails.ContractorName = txtname.Text.Trim();
                        uDetails.LastName = txtLastname.Text.Trim();
                        uDetails.isFirstlogin = 0;
                        cRep.Edit(uDetails);

                        // ActiveCampaignHelper a = new ActiveCampaignHelper();
                        try
                        {
                         
                            UpdateActiveCamp(uDetails, "PREMID");

                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "Ok");

                    }

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
      

        private void UpdateActiveCamp(UserMgt.Entity.Contractor uDetails,string tag)
        {
            var helper = new ActiveCampaignHelper();

            // Create contact and get contactId
            // var contactId = helper.CreateContact(txtemail.Text, uDetails.ContractorName, uDetails.LastName);
            var contactId = helper.EnsureContact(uDetails.EmailAddress.ToLower(), uDetails.ContractorName, uDetails.LastName);
            if (contactId.HasValue)
            {

                // Create a tag and get tagId
                //var tagId = helper.CreateTag("PREMID");
                var tagId = helper.EnsureTag(tag);
                if (tagId.HasValue)
                {
                    // Add tag to contact
                    if (helper.AddTagToContact(contactId.Value, tagId.Value))
                    {
                        LogExceptions.LogException("Contact created and tagged successfully!");
                    }
                    else
                    {
                        LogExceptions.LogException("Failed to add tag to contact.");
                    }
                }
                else
                {
                    LogExceptions.LogException("Failed to create tag.");
                }


            }
            else
            {
                LogExceptions.LogException("Failed to create contact.");
            }
        }
    }
}