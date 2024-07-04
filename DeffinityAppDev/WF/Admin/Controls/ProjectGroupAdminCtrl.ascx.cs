using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.BAL;
using UserMgt.BAL;
using ProjectMgt.Entity;
using UserMgt.Entity;

public partial class controls_ProjectGroupAdminCtrl : System.Web.UI.UserControl
{
    ProjectAdminGroupBAL pd = null;
    ContractorsBAL cb = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAdminDropdown();
            BindGridView();
        }

    }

    private void BindGridView()
    {
        pd = new ProjectAdminGroupBAL();
        cb = new ContractorsBAL();
        gridAdmins.DataSource = from p in pd.ProjectAdminGroup_SelectAll()
                                join q in cb.Contractor_SelectAll() on p.UserID equals q.ID
                                select new
                                {
                                    ID = p.ID,
                                    Name = q.ContractorName
                                };
        gridAdmins.DataBind();
    }

    private void BindAdminDropdown()
    {
        cb = new ContractorsBAL();
        UserMgt.Entity.Contractor c = new UserMgt.Entity.Contractor();
        ddlProjectAdmins.DataSource = cb.Contractor_Select_Admins();
        ddlProjectAdmins.DataTextField = "ContractorName";
        ddlProjectAdmins.DataValueField = "ID";
        ddlProjectAdmins.DataBind();
        ddlProjectAdmins.Items.Insert(0, new ListItem( "Please select...","0"));
    }
    protected void gridAdmins_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            pd = new ProjectAdminGroupBAL();
            pd.ProjectAdminGroup_DeleteByID(id);
            lblMsg.Text = "Deleted successfully.";
            //reload Grid view
            BindAdminDropdown();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlProjectAdmins.SelectedValue != "0")
        { 
            pd = new ProjectAdminGroupBAL();
            var row = pd.ProjectAdminGroup_SelectAll().Where(p => p.UserID == Convert.ToInt32(ddlProjectAdmins.SelectedValue)).FirstOrDefault();
            if(row == null)
            {
                pd.ProjectAdminGroup_Insert(new ProjectAdminGroup() { UserID = Convert.ToInt32(ddlProjectAdmins.SelectedValue) });
                lblMsg.Text = "Added successfully.";
                //reload grid
                BindGridView();
            }
        }
    }
}