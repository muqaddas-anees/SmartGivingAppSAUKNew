using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace DeffinityAppDev.App
{
    public partial class ManageCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    BindCOmpany();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddCompnay_Click(object sender, EventArgs e)
        {
            hid.Value = "0";
            txtAddCompany.Text = string.Empty;
            txtCompanyAddress.Text = string.Empty;
            txtCompanyPhone.Text = string.Empty;
            txtCompanyEmail.Text = string.Empty;
            txtCompanyNotes.Text = string.Empty;
            mdlPopup.Show();
        }

        protected void btnSubmitCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddCompany.Text.Trim().Length > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                    var id = Convert.ToInt32(hid.Value);
                    //var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == txtAddCompany.Text.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                    if (id == 0)
                    {
                        var cDetails = new PortfolioMgt.Entity.UserCompany()
                        {
                            Name = txtAddCompany.Text.Trim(),
                            OrganisationID = sessionKeys.PortfolioID,
                            Address = txtCompanyAddress.Text.Trim(),
                            Contactno = txtCompanyPhone.Text.Trim(),
                            Email = txtCompanyEmail.Text.Trim(),
                            Notes = txtCompanyNotes.Text.Trim()
                        };
                        uRep.Add(cDetails);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                    }
                    else
                    {
                        var cdetails = uRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        if(cdetails != null)
                        {
                            cdetails.Name = txtAddCompany.Text.Trim();
                            cdetails.OrganisationID = sessionKeys.PortfolioID;
                            cdetails.Address = txtCompanyAddress.Text.Trim();
                            cdetails.Contactno = txtCompanyPhone.Text.Trim();
                            cdetails.Email = txtCompanyEmail.Text.Trim();
                            cdetails.Notes = txtCompanyNotes.Text.Trim();
                            uRep.Edit(cdetails);
                            DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                        }

                    }
                    BindCOmpany();

                    txtAddCompany.Text = string.Empty;
                    txtCompanyAddress.Text = string.Empty;
                    txtCompanyPhone.Text = string.Empty;
                    txtCompanyEmail.Text = string.Empty;
                    txtCompanyNotes.Text = string.Empty;
                    hid.Value = "0";
                    mdlPopup.Hide();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindCOmpany()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                var pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();

                if (pList.Count == 0)
                {
                    IUserRepository<UserMgt.Entity.v_contractor> pRep = new UserRepository<UserMgt.Entity.v_contractor>();
                    var rList = pRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                    foreach (var r in rList)
                    {
                        if (r.Company.Trim().Length > 0)
                        {
                            var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == r.Company.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                            if (cnt == 0)
                            {
                                uRep.Add(new PortfolioMgt.Entity.UserCompany() { Name = r.Company, OrganisationID = sessionKeys.PortfolioID });
                            }
                        }
                    }

                    pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();
                }

                GridInstances.DataSource = pList.ToList();
                GridInstances.DataBind();
                //ddlCompany.DataSource = pList.OrderBy(o => o.Name).ToList();
                //ddlCompany.DataTextField = "Name";
                //ddlCompany.DataValueField = "Name";
                //ddlCompany.DataBind();

                //ddlCompany.Items.Insert(0, new ListItem("Please select...", ""));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                var pList = uRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                if (e.CommandName == "edit1")
                {
                    if(pList != null)
                    {
                        hid.Value = pList.ID.ToString();
                        txtAddCompany.Text = pList.Name;
                        txtCompanyAddress.Text = pList.Address;
                        txtCompanyEmail.Text = pList.Email;
                        txtCompanyPhone.Text = pList.Contactno;
                        txtCompanyNotes.Text = pList.Notes;
                        mdlPopup.Show();
                    }
                }
                else if(e.CommandName == "del")
                {
                    uRep.Delete(pList);
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    BindCOmpany();
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}