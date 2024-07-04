using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class UpgradePopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindPartnerDropdown();
                    PartnerData();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindPartnerDropdown()
        {
            try
            {
                var mlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().OrderBy(o => o.PartnerName).ToList();
                ddlPartner.DataSource = mlist;
                ddlPartner.DataValueField = "ID";
                ddlPartner.DataTextField = "PartnerName";
                ddlPartner.DataBind();
                ddlPartner.Items.Insert(0, new ListItem("Please select...", "0"));

                if (mlist.Count > 0)
                {
                    ddlPartner.SelectedValue = mlist.FirstOrDefault().ID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPartner.SelectedValue != "0")
                {
                    var p = new PortfolioMgt.Entity.PortfolioAdminUserPopContent();
                  
                    p.PartnerID = Convert.ToInt32(ddlPartner.SelectedValue);
                  
                    p.Content = Server.HtmlEncode(CKEditor1.Text);
                   

                    PortfolioMgt.BAL.PortfolioAdminUserPopContentBAL.PortfolioAdminUserPopContentBAL_AddUpdate(p);

                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartnerData();
        }

        private void PartnerData()
        {
            try
            {
                if (Convert.ToInt32(ddlPartner.SelectedValue) > 0)
                {
                    var dEnity = PortfolioMgt.BAL.PortfolioAdminUserPopContentBAL.PortfolioAdminUserPopContentBAL_SelectByPartnerID(Convert.ToInt32(ddlPartner.SelectedValue)).FirstOrDefault();
                    if (dEnity != null)
                    {
                        ddlPartner.SelectedValue = dEnity.PartnerID.Value.ToString();
                        CKEditor1.Text = Server.HtmlDecode(dEnity.Content);

                    }
                    else
                    {
                        CKEditor1.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}