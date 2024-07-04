using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.BAL;
using DC.BLL;

namespace DeffinityAppDev.WF.DC
{
    public partial class QuoteTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTitle();
            }

            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }
        }

        private void BindTitle()
        {
            try
            {
                ddlTitle.Items.Clear();
                var qlist = QuoteTemplateBAL.QuoteTemplateSelectAll();
                if (qlist.Count > 0)
                {
                    ddlTitle.DataSource = qlist.ToList().OrderBy(o => o.Title);
                    ddlTitle.DataTextField = "Title";
                    ddlTitle.DataValueField = "ID";
                    ddlTitle.DataBind();
                }
                else
                {
                    CreateButtonVisibility();
                }
                ddlTitle.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTitle.SelectedValue) > 0)
            {
                btnEdit.Visible = true;
                var id = Convert.ToInt32(ddlTitle.SelectedValue);
                setSelectedTemplateValues(id);
            }
            else
            {
                btnEdit.Visible = false;
            }
        }

        private void setSelectedTemplateValues(int id)
        {
            var q = QuoteTemplateBAL.QuoteTemplateSelect(id);
            if (q != null)
            {
                txtTitle.Text = q.Title;
                txtTemplate.Text = QuoteTemplateBAL.QuoteTemplateSelectHTML(id);
            }
            else
            {
                txtTitle.Text = string.Empty;
                txtTemplate.Text = string.Empty;
            }
        }

        private void ClearFields()
        {
            txtTemplate.Text = string.Empty;
            txtTitle.Text = string.Empty;
            ddlTitle.SelectedValue = "0";
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            ClearFields();
            CreateButtonVisibility();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTitle.SelectedValue) > 0)
            {
                txtTitle.Text = ddlTitle.SelectedItem.Text;
                txtTitle.Visible = true;
                ddlTitle.Visible = false;
                btnEdit.Visible = false;
                btnCreate.Visible = false;
                btnDelete.Visible = false;
            }
        }
        private void CreateButtonVisibility()
        {
            txtTitle.Text = string.Empty;
            txtTitle.Visible = true;
            ddlTitle.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnCreate.Visible = false;
        }

        private void EditButtonVisibility()
        {
            txtTitle.Text = string.Empty;
            txtTitle.Visible = false;
            ddlTitle.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnCreate.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlTitle.SelectedValue) > 0)
                {
                    btnEdit.Visible = true;
                    var id = Convert.ToInt32(ddlTitle.SelectedValue);
                    QuoteTemplateBAL.QuoteTemplateDelete(id);
                    lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;

                    BindTitle();
                    ClearFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtTitle.Visible == true)
                {
                    //add template
                    if (!QuoteTemplateBAL.QuoteTemplateTitleExists(Convert.ToInt32(ddlTitle.SelectedValue), txtTitle.Text.Trim()))
                    {
                       var q = QuoteTemplateBAL.AddUpdateQuoteTemplate(Convert.ToInt32(ddlTitle.SelectedValue), txtTitle.Text, txtTemplate.Text);
                        EditButtonVisibility();

                        if (Convert.ToInt32(ddlTitle.SelectedValue) > 0)
                            lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                        else
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        BindTitle();
                        ddlTitle.SelectedValue = q.ID.ToString();
                        setSelectedTemplateValues(q.ID);
                       
                    }
                    else
                    {
                        lblError.Text = "Template name already exists";
                    }
                }
                else
                {
                    if (!QuoteTemplateBAL.QuoteTemplateTitleExists(Convert.ToInt32(ddlTitle.SelectedValue), txtTitle.Text.Trim()))
                    {
                        var q = QuoteTemplateBAL.AddUpdateQuoteTemplate(Convert.ToInt32(ddlTitle.SelectedValue), txtTitle.Text, txtTemplate.Text);
                        EditButtonVisibility();
                        setSelectedTemplateValues(q.ID);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                    else
                    {
                        lblError.Text = "Template name already exists";
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}