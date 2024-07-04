using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class DefaultJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    huid.Value = "0";
                    BindDropdown();
                    //BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid(int sectorID)
        {
            try {
                var rlistgrid = DefaultJobsBAL.DefaultJobsBAL_select(sectorID);

                GridModules.DataSource = rlistgrid;
                GridModules.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
            private void BindDropdown()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
                var result = (from p in rlist
                              orderby p.Portfoliotype1
                              select new ListItem { Value = p.ID.ToString(), Text = p.Portfoliotype1 }).ToList();
               
                ddlSectorTop.DataSource = result;
                ddlSectorTop.DataTextField = "Text";
                ddlSectorTop.DataValueField = "Value";
                ddlSectorTop.DataBind();

                ddlSectorTop.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
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
                    var m = DefaultJobsBAL.DefaultJobsBAL_select(Convert.ToInt32(ddlSectorTop.SelectedValue)).Where(o=>o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (m != null)
                    {

                        txtDescription.Text = m.JobDescription;
                        
                        mdlManageOptions.Show();
                    }

                }
                else if (e.CommandName == "del")
                {
                    DefaultJobsBAL.DefaultJobsBAL_delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    BindGrid(Convert.ToInt32(ddlSectorTop.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                {
                    if (Convert.ToInt32(ddlSectorTop.SelectedValue) > 0)
                    {
                        if (Convert.ToInt32(huid.Value) > 0)
                        {

                            DefaultJobsBAL.DefaultJobsBAL_update(Convert.ToInt32(huid.Value), txtDescription.Text);
                            //DeffinityManager.Portfolio.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleUpdate(moduleid, txtDescription.Text.Trim(), txtImage.Text.Trim(), chkPartofPremium.Checked);
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                            txtDescription.Text = string.Empty;
                            huid.Value = "0";
                            mdlManageOptions.Hide();
                            BindGrid(Convert.ToInt32(ddlSectorTop.SelectedValue));
                        }
                        else
                        {
                            DefaultJobsBAL.DefaultJobsBAL_add(Convert.ToInt32(ddlSectorTop.SelectedValue), txtDescription.Text);
                            //DeffinityManager.Portfolio.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleUpdate(moduleid, txtDescription.Text.Trim(), txtImage.Text.Trim(), chkPartofPremium.Checked);
                            lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                            txtDescription.Text = string.Empty;
                            mdlManageOptions.Hide();
                            BindGrid(Convert.ToInt32(ddlSectorTop.SelectedValue));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlSectorTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGrid(Convert.ToInt32(ddlSectorTop.SelectedValue));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtDescription.Text = string.Empty;

            mdlManageOptions.Show();
        }
    }
}