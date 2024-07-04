using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.Entity;
using ProjectMgt.BLL;

public partial class controls_ProjectTaskCategoryCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hide();
            BindCategory();
        }
    }

    #region Hide Controls
    private void Hide()
    {
        ddlCategory.Visible = true;
        txtCategory.Visible = false;
        imb_Submit.Visible = false;
        imb_Cancel.Visible = false;
        imb_Add.Visible = true;
        imb_Delete.Visible = true;
        imb_Edit.Visible = true;

    }
    #endregion

    #region Show Controls
    private void Show()
    {
        ddlCategory.Visible = false;
        txtCategory.Visible = true;
        imb_Submit.Visible = true;
        imb_Cancel.Visible = true;
        imb_Add.Visible = false;
        imb_Delete.Visible = false;
        imb_Edit.Visible = false;
        lblMsg.Text = string.Empty;

    }
    #endregion

    private void BindCategory()
    {
        ddlCategory.DataSource = ProjectTaskCategoryBAL.GetCategoryList().OrderBy(c=>c.Name).ToList();
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    protected void imb_Add_Click(object sender, EventArgs e)
    {
        Show();
        txtCategory.Text = string.Empty;
    }



    protected void imb_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectTaskCategory category = new ProjectTaskCategory();
            category.Name = txtCategory.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(hfId.Value) ? "0" : hfId.Value);
            if (id > 0)
            {
                bool exists = ProjectTaskCategoryBAL.CheckCategory(id, txtCategory.Text.Trim());
                if (!exists)
                {
                    category.ID = id;
                    ProjectTaskCategoryBAL.UpdateCategory(category);
                    lblMsg.Text = "Category updated successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                   // ddlCategory.SelectedValue = id.ToString();
                    hfId.Value = "0";
                    txtCategory.Text = string.Empty;
                    BindCategory();
                }
                else
                {
                    lblMsg.Text = "Category already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = ProjectTaskCategoryBAL.CheckCategory(txtCategory.Text.Trim());
                if (!exists)
                {
                    ProjectTaskCategoryBAL.AddCategory(category);
                    lblMsg.Text = "Category added successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    
                    hfId.Value = "0";
                    txtCategory.Text = string.Empty;
                    BindCategory();


                }
                else
                {
                    lblMsg.Text = "Category already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectTaskCategory category = ProjectTaskCategoryBAL.SelectByID(int.Parse(ddlCategory.SelectedValue));
            if (category != null)
            {
                txtCategory.Text = category.Name;
                hfId.Value = category.ID.ToString();
                Show();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Cancel_Click(object sender, EventArgs e)
    {
        Hide();
        lblMsg.Text = string.Empty;
        hfId.Value = "0";
    }

    protected void imb_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "0")
            {
                ProjectTaskCategoryBAL.DeleteByID(int.Parse(ddlCategory.SelectedValue));
                lblMsg.Text = "Category deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindCategory();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}