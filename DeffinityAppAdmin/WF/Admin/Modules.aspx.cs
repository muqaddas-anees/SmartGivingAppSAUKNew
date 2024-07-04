using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class Modules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                    ModuleDescription();

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ModuleDescription()
        {
            try
            {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                CKEditor1.Text = Server.HtmlDecode( c.UpgradDescription);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid()
        {
            try
            {
                var mlist = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect();
                ddlModule.DataSource = mlist.OrderBy(o => o.ModuleName).ToList();
                ddlModule.DataTextField = "ModuleName";
                ddlModule.DataValueField = "ModuleID";
                ddlModule.DataBind();

                GridModules.DataSource = mlist;
                GridModules.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridModules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editmodule")
                {
                    huid.Value = e.CommandArgument.ToString();
                    var m = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect().Where(o => o.ModuleID == Convert.ToInt32(huid.Value)).FirstOrDefault();
                    if (m != null)
                    {
                        ddlModule.SelectedValue = m.ModuleID.ToString();
                        txtDescription.Text = m.ModuleDescription;
                        chkPartofPremium.Checked = m.IsPaid;
                        txtImage.Text = m.ModuleImage;
                        mdlManageOptions.Show();
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var moduleid = Convert.ToInt32( huid.Value);
                var m = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect().Where(o => o.ModuleID == Convert.ToInt32(huid.Value)).FirstOrDefault();
                if (m != null)
                {
                    
                    PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleUpdate(moduleid, txtDescription.Text.Trim(), txtImage.Text.Trim(), chkPartofPremium.Checked);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    mdlManageOptions.Hide();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpdateDescription_Click(object sender, EventArgs e)
        {
            try {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();

                if(c != null)
                {
                    c.UpgradDescription = Server.HtmlEncode( CKEditor1.Text);
                    UserMgt.BAL.CompanyBAL.CompanyBAL_Update(c);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
                
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}