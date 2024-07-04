using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

public partial class controls_CCCategoryCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            BindCutomers();
                
        }
    }
    protected void imgAddCategory_Click(object sender, EventArgs e)
    {
        Deffinity.PortfolioSLAmanager.PortfolioSLA.InsertMasterCategory(txtmastercategory.Text.Trim(), Convert.ToInt32(ddlCustomer.SelectedValue));
        txtmastercategory.Text = string.Empty;
        ddlCategory.DataBind();
        ModalPopupExtender1.Hide();
    }
    protected void imgAddSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
          
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                int cnt = pd.ProjectCategories.Where(p => p.MasterID == int.Parse(ddlCategory.SelectedValue) && p.CategoryName.ToLower() == txtSubCategory.Text.ToLower().Trim()).Count();
                if (cnt == 0)
                {
                    ProjectCategory pc = new ProjectCategory();
                    pc.MasterID = int.Parse(ddlCategory.SelectedValue);
                    pc.CategoryName = txtSubCategory.Text.Trim();
                    pd.ProjectCategories.InsertOnSubmit(pc);
                    pd.SubmitChanges();
                }
            }
         
            txtSubCategory.Text = string.Empty;
            casCadSubCategory.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void BindCutomers()
    {
        ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomer.DataTextField = "PortFolio";
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.RemoveAt(0);
        ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    protected void btn_popup2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
       
    }
    protected void imgsubcat_Click(object sender, EventArgs e)
    {
        modleSubcategory.Show();
    }
    protected void btnDeleteCategory_Click(object sender, EventArgs e)
    {
        try
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                if (categoryId > 0)
                {
                    Projectcategory_Maser category = pd.Projectcategory_Masers.Where(p => p.ID == categoryId).FirstOrDefault();
                    if (category != null)
                    {
                        pd.Projectcategory_Masers.DeleteOnSubmit(category);
                        pd.SubmitChanges();
                        ddlCategory.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgDeleteSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                int subCategoryId = Convert.ToInt32(ddlSubcategory.SelectedValue);
                if (subCategoryId > 0)
                {
                    ProjectCategory projectCategory = pd.ProjectCategories.Where(p => p.CategoryID == subCategoryId).FirstOrDefault();
                    if (projectCategory != null)
                    {
                        pd.ProjectCategories.DeleteOnSubmit(projectCategory);
                        pd.SubmitChanges();
                        casCadSubCategory.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}