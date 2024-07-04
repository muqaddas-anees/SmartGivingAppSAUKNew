using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class CCPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindPartners();

                    GetPartnerPopupdata();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }

        private void BindPartners()
        {
            var plist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll();
            ddlPartner.DataSource = plist.ToList().OrderBy(o => o.ID).ToList();
            ddlPartner.DataTextField = "PartnerName";
            ddlPartner.DataValueField = "ID";
            ddlPartner.DataBind();
            ddlPartner.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        private void GetPartnerPopupdata()
        {
            var p = PortfolioMgt.BAL.PartnerPopupSetupBAL.PartnerPopupSetupBAL_SelectAll().Where(o => o.PartnerID == Convert.ToInt32(ddlPartner.SelectedValue)).FirstOrDefault();
            if (p != null)
            {
                txtButtoncolor.Text = p.ButtonColor;
                txtBottonLink.Text = p.LinkUrl;
                txt1poptime.Text = p.Popup1Time.ToString();
                txt2poptime.Text = p.Popup2Time.ToString();
                CKEditor1.Text = Server.HtmlDecode(p.PopupContent);

            }
        }
        private void clearData()
        {
            txt1poptime.Text = string.Empty;
            txt2poptime.Text = string.Empty;
            txtBottonLink.Text = string.Empty;
            txtButtoncolor.Text = string.Empty;
            CKEditor1.Text = string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPartner.SelectedValue != "0")
                {
                    var p = new PortfolioMgt.Entity.PartnerPopupSetup();
                    p.ButtonColor = txtButtoncolor.Text.Trim();
                    p.LinkUrl = txtBottonLink.Text.Trim();
                    p.PartnerID = Convert.ToInt32(ddlPartner.SelectedValue);
                    p.Popup1Time = Convert.ToInt32(!string.IsNullOrEmpty(txt1poptime.Text.Trim()) ? txt1poptime.Text.Trim() : "0");
                    p.Popup2Time = Convert.ToInt32(!string.IsNullOrEmpty(txt2poptime.Text.Trim()) ? txt2poptime.Text.Trim() : "0");
                    p.PopupContent = Server.HtmlEncode(CKEditor1.Text);

                    PortfolioMgt.BAL.PartnerPopupSetupBAL.PartnerPopupSetupBAL_AddUpdate(p);

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearData();

                GetPartnerPopupdata();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}