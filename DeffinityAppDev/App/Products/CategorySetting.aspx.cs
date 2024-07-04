using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Products
{
    public partial class CategorySetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PortfolioMgt.BAL.ProductCategoryBAL.ProductCategoryBAL_AddDefaults();
                BindCategoryData();
            }
        }

        private void BindCategoryData()
        {
            try
            {
                PortfolioRepository<PortfolioMgt.Entity.ProductCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();
                var dList = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).OrderBy(o => o.Name).ToList();
                GridInstances.DataSource = dList;
                GridInstances.DataBind();
                if(dList.Count ==0)
                {
                    txtCategory.Text = string.Empty;
                    hid.Value = "0";
                    mdlLanguage.Show();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                PortfolioRepository<PortfolioMgt.Entity.ProductCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();
                
                if(hid.Value != "0")
                {
                    var dList = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                    if(dList != null)
                    {
                        if (txtCategory.Text.Length > 0)
                        {
                            dList.Name = txtCategory.Text.Trim();
                            pRep.Edit(dList);
                            txtCategory.Text = string.Empty;
                            hid.Value = "0";

                            mdlLanguage.Hide();
                            BindCategoryData();
                        }
                    }

                }
                else
                {
                    if(txtCategory.Text.Length >0)
                    {
                        pRep.Add(new PortfolioMgt.Entity.ProductCategory() { Name = txtCategory.Text.Trim(), PortfolioID = sessionKeys.PortfolioID });

                        txtCategory.Text = string.Empty;
                        hid.Value = "0";

                        mdlLanguage.Hide();
                        BindCategoryData();
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "edit1")
                {
                    hid.Value = e.CommandArgument.ToString();
                    PortfolioRepository<PortfolioMgt.Entity.ProductCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();
                    var dList = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    if (dList != null)
                    {
                        txtCategory.Text = dList.Name;

                    }
                        mdlLanguage.Show();
                }
                else if (e.CommandName == "del")
                {
                    PortfolioRepository<PortfolioMgt.Entity.ProductCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();
                    var dList = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    if(dList != null)
                    {
                        pRep.Delete(dList);
                        BindCategoryData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            txtCategory.Text = string.Empty;
            hid.Value = "0";
            mdlLanguage.Show();
        }
    }
}