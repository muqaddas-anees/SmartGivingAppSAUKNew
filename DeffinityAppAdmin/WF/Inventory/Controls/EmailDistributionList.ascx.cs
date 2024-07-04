using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using UserMgt.DAL;

public partial class controls_EmailDistributionList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUsers();
            BindGrid();
        }
    }
    private void BindGrid()
    {
        try
        {
            using (InventoryDataContext dd = new InventoryDataContext())
            {
                using (UserDataContext udc = new UserDataContext())
                {
                    var x = dd.Inventory_DistributionLists.Where(m => m.CustomerID == sessionKeys.PortfolioID).Select(r => r).ToList();
                    var y = udc.Contractors.Select(r => r).ToList();
                    var result = (from p in x
                                  join o in y on p.UserID equals o.ID
                                  where o.Status.ToLower() == "active"
                                  orderby o.ContractorName
                                  select new { p.ID, p.UserID, o.ContractorName }).ToList();
                    GvMailManager.DataSource = result;
                    GvMailManager.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindUsers()
    {
        try
        {
            ddlUsers.DataSource = SecurityAccessMail.BindContractor().Where(p => p.SID == 1 || p.SID == 2 || p.SID == 3).ToList();
            ddlUsers.DataValueField = "ID";
            ddlUsers.DataTextField = "ContractorName";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCopyToAllCustomers_Click(object sender, EventArgs e)
    { 
    }
    protected void GvMailManager_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            using (InventoryDataContext Idc = new InventoryDataContext())
            {
                Inventory_DistributionList Dlist = Idc.Inventory_DistributionLists.Where(a => a.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                if (Dlist != null)
                {
                    Idc.Inventory_DistributionLists.DeleteOnSubmit(Dlist);
                    Idc.SubmitChanges();
                    BindGrid();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Deleted successfully...";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btndeleteUser_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnaddUser_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            Inventory_DistributionList manager = new Inventory_DistributionList();
            manager.UserID = int.Parse(ddlUsers.SelectedValue);
           // manager.RequestTypeID = int.Parse(h_rtid.Value);
            manager.CustomerID = customerID;

            using (InventoryDataContext Idc = new InventoryDataContext())
            {
                var exists = Idc.Inventory_DistributionLists.Where(a => a.UserID == int.Parse(ddlUsers.SelectedValue) && a.CustomerID == customerID).ToList();
                if (exists.Count == 0)
                {
                    Idc.Inventory_DistributionLists.InsertOnSubmit(manager);
                    Idc.SubmitChanges();
                    lblMsg.Text = "User added successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "User already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnsendMail_Click(object sender, EventArgs e)
    {
        using (InventoryDataContext dd = new InventoryDataContext())
        {
            using (UserDataContext udc = new UserDataContext())
            {
                var x = dd.Inventory_DistributionLists.Where(m => m.CustomerID == sessionKeys.PortfolioID).Select(r => r).ToList();
                var y = udc.Contractors.Select(r => r).ToList();
                var result = (from p in x
                              join o in y on p.UserID equals o.ID
                              where o.Status.ToLower() == "active"
                              orderby o.ContractorName
                              select new
                              {
                                  p.UserID,
                                  o.ContractorName,
                                  o.EmailAddress
                              }).ToList();
            }
        }
    }
}