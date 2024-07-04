using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;
using DeffinityManager.Portfolio.BAL;

namespace DeffinityAppDev.WF.Admin
{
    public partial class Premium : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindCurrency();
                //set saved date
                BindModules();
                SetUpdatedPrice();
            }
        }
        public void BindModules()
        {
            try
            {
               var mList=  PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect();
                chkModuleList.DataSource = mList;
                chkModuleList.DataValueField = "ModuleID";
                chkModuleList.DataTextField  = "ModuleName";
                chkModuleList.DataBind();

                //check check box
                foreach (ListItem item in chkModuleList.Items)
                {
                    var m = mList.Where(o => o.ID == Convert.ToInt32(item.Value)).FirstOrDefault();
                    if (m.IsPaid)
                        item.Selected = true;
                    else
                        item.Selected = false;
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public void BindCurrency()
        {
            try
            {
                IProjectRepository<ProjectMgt.Entity.CurrencyList> cRep = new ProjectRepository<ProjectMgt.Entity.CurrencyList>();
                var cList = cRep.GetAll().Where(o => o.Display == "Y").ToList();

                ddlCurrency.DataSource = cList;
                ddlCurrency.DataTextField = "ShortCurrencyName";
                ddlCurrency.DataValueField = "ID";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, "Please select...");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void SetUpdatedPrice()
        {
            try
            {
                var v = PortfolioDefaultsBAL.PortfolioDefaultsBAL_Select();
                if(v!= null)
                {
                    txtPrice.Text = string.Format("{0:F2}", v.MonthlyPrice);
                    ddlCurrency.SelectedItem.Text = v.Currency;
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
                PortfolioDefaultsBAL.PortfolioDefaultsBAL_AddUpdate(Convert.ToDouble(txtPrice.Text.Trim()), ddlCurrency.SelectedItem.Text);
                lblMsgPrice.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnModuleApply_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem item in chkModuleList.Items)
                {
                    PortfolioModulesBAL.PortfolioModulesBAL_ModuleUpdate(Convert.ToInt32( item.Value),item.Selected);
                }

                lblMsgModule.Text = Resources.DeffinityRes.UpdatedSuccessfully;

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        
    }
}