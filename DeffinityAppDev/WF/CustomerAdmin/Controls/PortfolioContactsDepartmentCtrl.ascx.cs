using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using PortfolioMgt.BLL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

public partial class controls_PortfolioContactsDepartmentCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hide();
        }
    }
    #region Hide Controls
    private void Hide()
    {
        ddlDepartment.Visible = true;
        txtDepartment.Visible = false;
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
        ddlDepartment.Visible = false;
        txtDepartment.Visible = true;
        imb_Submit.Visible = true;
        imb_Cancel.Visible = true;
        imb_Add.Visible = false;
        imb_Delete.Visible = false;
        imb_Edit.Visible = false;
        lblMsg.Text = string.Empty;

    }
    #endregion

    protected void imb_Add_Click(object sender, EventArgs e)
    {
        Show();
        txtDepartment.Text = string.Empty;
    }

    protected void imb_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;
            PortfolioContactsDepartment portfolioContactsDepartment = new PortfolioContactsDepartment();
            portfolioContactsDepartment.Name = txtDepartment.Text.Trim();
            portfolioContactsDepartment.CustomerID = customerId;
            int id = int.Parse(string.IsNullOrEmpty(hfId.Value) ? "0" : hfId.Value);
            if (id > 0)
            {
                bool exists = PortfolioContactsDepartmentBAL.CheckPortfolioContactsDepartment(id, txtDepartment.Text.Trim(), customerId);
                if (!exists)
                {
                    portfolioContactsDepartment.ID = id;
                    PortfolioContactsDepartmentBAL.UpdatePortfolioContactsDepartment(portfolioContactsDepartment);
                    lblMsg.Text = "Department updated successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ddlDepartment.SelectedValue = id.ToString();
                    hfId.Value = "0";
                    txtDepartment.Text = string.Empty;
                }
                else
                {
                    lblMsg.Text = "Department already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = PortfolioContactsDepartmentBAL.CheckPortfolioContactsDepartment(txtDepartment.Text.Trim(), customerId);
                if (!exists)
                {
                    PortfolioContactsDepartmentBAL.AddPortfolioContactsDepartment(portfolioContactsDepartment);
                    lblMsg.Text = "Department added successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ddlDepartment.SelectedValue = portfolioContactsDepartment.ID.ToString();
                    hfId.Value = "0";
                    txtDepartment.Text = string.Empty;


                }
                else
                {
                    lblMsg.Text = "Department already exists.";
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
            PortfolioContactsDepartment portfolioContactsDepartment = PortfolioContactsDepartmentBAL.SelectByID(int.Parse(ddlDepartment.SelectedValue));
            if (portfolioContactsDepartment != null)
            {
                txtDepartment.Text = portfolioContactsDepartment.Name;
                hfId.Value = portfolioContactsDepartment.ID.ToString();
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
            if (ddlDepartment.SelectedValue != "0")
            {
                PortfolioContactsDepartmentBAL.DeleteByID(int.Parse(ddlDepartment.SelectedValue));
                lblMsg.Text = "Department deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCopyToAllCustomers_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                var departmentList = pd.PortfolioContactsDepartments.Where(c => c.CustomerID == customerID).ToList();
                if (departmentList.Count() > 0)
                {
                    List<PortfolioContactsDepartment> dList = new List<PortfolioContactsDepartment>();
                    foreach (var c in customerList)
                    {
                        foreach (var s in departmentList)
                        {
                            bool exists = PortfolioContactsDepartmentBAL.CheckPortfolioContactsDepartment(s.Name, c.ID);
                            if (!exists)
                            {
                                PortfolioContactsDepartment department = new PortfolioContactsDepartment();
                                department.CustomerID = c.ID;
                                department.Name = s.Name;
                                dList.Add(department);
                            }
                        }
                    }
                    //Bulk insert
                    pd.PortfolioContactsDepartments.InsertAllOnSubmit(dList);
                    pd.SubmitChanges();
                    lblMsg.Text = "Successfully copied...";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "0")
            {
                PortfolioContactsDepartmentBAL.DeleteByID(int.Parse(ddlDepartment.SelectedValue));
                lblMsg.Text = "Department deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btndeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "0")
            {
                PortfolioContactsDepartmentBAL.DeteleaReqDepttoAllCustomers(ddlDepartment.SelectedItem.ToString());
                lblMsg.Text = "Department deleted to all customers successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}