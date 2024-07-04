using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SC_ContractorMaterialCS;
using Deffinity.ServiceCatalogManager;
using System.Data.SqlClient;
using System.Data;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;


public partial class controls_InventoryBarcode : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(Constants.DBString);
    public int PageType = 1;
    public int VendorID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DefaultBinding();
            BindGrid(0,0,0);
        }
    }
    private void DefaultBinding()
    {
        try
        {
            BindCustomer();
            BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindCategory(int portfolio)
    {
        SC_ContractorMaterial SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlCategory.DataSource = SC_cntMaterialCS.SelectAllCategory(portfolio, PageType, VendorID);
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataBind();

    }
    private void BindCustomer()
    {
        ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomer.DataTextField = "PortFolio";
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.RemoveAt(0);
        ddlCustomer.Items.Insert(0, new ListItem("Please Select...", "0"));
        ddlCustomer.SelectedValue = Convert.ToString(sessionKeys.PortfolioID);
       
    }
    private void BindSubCategory(int categoryID)
    {
        SC_ContractorMaterial SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlSubCategory.DataSource = SC_cntMaterialCS.GetSubCategory(categoryID, int.Parse(ddlCustomer.SelectedValue), 0);
        ddlSubCategory.DataValueField = "Id";
        ddlSubCategory.DataTextField = "CategoryName";
        ddlSubCategory.DataBind();
    }

    private void FilterGrid(int customer, int category, int subCategory)
    {

    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblError.Visible = false;
        BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

        sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString());
        sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text.ToString();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

    }
    private void panelVisibleCategory(bool ddl, bool txt)
    {
        pnladdcategory.Visible = txt;
        pnlcategory.Visible = ddl;

    }
    protected void btnaddcategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCustomer.SelectedItem.Value != "0")
            {
                txtAddCategory.Text = string.Empty;
                panelVisibleCategory(false, true);
                BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
                BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Please select Customer";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btn_CategoryEdit_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlCategory.Visible == true)
        {
            if (ddlCategory.SelectedValue != "0")
            {
                HID_Category.Value = ddlCategory.SelectedValue;
                txtAddCategory.Text = ddlCategory.SelectedItem.Text;
                panelVisibleCategory(false, true);
            }
        }
    }
    protected void btnDeleteCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "0")
                ServiceCatalogManager.DeleteCategory(int.Parse(ddlCategory.SelectedValue));

            BindCategory(int.Parse(ddlCustomer.SelectedValue));
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
            
            HID_Category.Value = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private int UpdateCategory(int ID, string Name, int type)
    {
        int retval = 0;
        SqlCommand comm_add = new SqlCommand("Deffinity_UpdateServiceCategory", conn);
        comm_add.CommandType = CommandType.StoredProcedure;
        comm_add.Parameters.Add("@CategoryID", SqlDbType.Int).Value = ID;
        comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = Name;
        comm_add.Parameters.Add("@Type", SqlDbType.Int).Value = type;
        //comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
        //comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value =(QueryStringValues.Vendor == null ? 0 : QueryStringValues.Vendor);
        SqlParameter outCategoryID = new SqlParameter("@Outval", SqlDbType.Int);
        outCategoryID.Direction = ParameterDirection.Output;
        comm_add.Parameters.Add(outCategoryID);
        conn.Open();
        try
        {
            int i = comm_add.ExecuteNonQuery();
            DefaultBinding();
            retval = int.Parse(comm_add.Parameters["@Outval"].Value.ToString());

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        return retval;
    }
    protected void btnSaveCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(HID_Category.Value))
            {
                int msg = UpdateCategory(int.Parse(HID_Category.Value), txtAddCategory.Text.Trim(), 0);
                if (msg == 1)
                {
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Green;
                    lblError.Text = "Category updated successfully";
                    ddlCategory.DataBind();
                    panelVisibleCategory(true, false);
                    ddlCategory.SelectedValue = HID_Category.Value;
                    HID_Category.Value = string.Empty;
                    txtAddCategory.Text = string.Empty;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please check Category name already exists.";
                }


            }
            else
            {
                SqlCommand comm_add = new SqlCommand("Deffinity_InsertServiceCategory", conn);
                comm_add.CommandType = CommandType.StoredProcedure;
                comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = txtAddCategory.Text.Trim();
                comm_add.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = sessionKeys.PortfolioID;
                comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                SqlParameter outCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                outCategoryID.Direction = ParameterDirection.Output;
                comm_add.Parameters.Add(outCategoryID);
                conn.Open();
                try
                {
                    int i = comm_add.ExecuteNonQuery();
                    if (i > 0)
                    {
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Green;
                        lblError.Text = "Category added successfully";
                        ddlCategory.DataBind();
                        BindCategory(sessionKeys.PortfolioID);
                        panelVisibleCategory(true, false);
                        //DefaultBinding();
                        ddlCategory.SelectedValue = comm_add.Parameters["@CategoryID"].Value.ToString();
                        txtAddCategory.Text = string.Empty;


                    }
                    else if (i == -1)
                    {
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = "Category Already Exists";
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                        // LogExceptions.LogException(ex.Message);
                        lblError.Text = "Error Occured while inserting";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        //Bind sub category
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));

    }
    protected void btnCancelCategory_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(HID_Category.Value))
        {
            HID_Category.Value = string.Empty;
            txtAddCategory.Text = string.Empty;
        }
        panelVisibleCategory(true, false);
        BindCategory(sessionKeys.PortfolioID);
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
    }
    private void panelVisibleSubCategory(bool ddl, bool txt)
    {
        pnladdsubcategory.Visible = txt;
        pnlsubcategory.Visible = ddl;
    }
    protected void btnaddsubcategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCustomer.SelectedItem.Value != "0")
            {
                if (ddlCategory.SelectedItem.Value != "0")
                {
                    txtAddSubCategory.Text = string.Empty;
                    panelVisibleSubCategory(false, true);
                    HID_Category.Value = ddlCategory.SelectedItem.Value;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please select Category";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Please select Customer";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void btn_editSubCategory_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSubCategory.Visible == true)
        {
            if (ddlSubCategory.SelectedValue != "0")
            {
                HID_SubCategory.Value = ddlSubCategory.SelectedValue;
                txtAddSubCategory.Text = ddlSubCategory.SelectedItem.Text;
                panelVisibleSubCategory(false, true);
            }
        }
    }
    protected void btnSubCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlSubCategory.SelectedValue != "0")
                ServiceCatalogManager.DeleteSubCategory(int.Parse(ddlSubCategory.SelectedValue));

            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));

          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnSaveSubCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(HID_SubCategory.Value))
            {
                int msg = UpdateCategory(int.Parse(HID_SubCategory.Value), txtAddSubCategory.Text.Trim(), 1);
                if (msg == 1)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sub Category updated successfully";
                    BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
                    panelVisibleSubCategory(true, false);
                    ddlSubCategory.SelectedValue = HID_SubCategory.Value;
                    HID_SubCategory.Value = string.Empty;
                    txtAddSubCategory.Text = string.Empty;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please check Sub Category name already exists.";
                }
            }
            else
            {
                SqlCommand comm_add = new SqlCommand("Deffinity_InsertServiceSubCategory", conn);
                comm_add.CommandType = CommandType.StoredProcedure;
                comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = txtAddSubCategory.Text.Trim();
                comm_add.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = sessionKeys.PortfolioID;
                comm_add.Parameters.Add("@MasterID", SqlDbType.Int).Value = Convert.ToInt32(ddlCategory.SelectedValue);
                comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                SqlParameter outCategoryID = new SqlParameter("@SubCategoryID", SqlDbType.Int);
                outCategoryID.Direction = ParameterDirection.Output;
                comm_add.Parameters.Add(outCategoryID);
                conn.Open();
                try
                {
                    comm_add.ExecuteNonQuery();
                    int i = Convert.ToInt32(comm_add.Parameters["@SubCategoryID"].Value);
                    if (i > 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Sub Category added successfully";
                        BindCategory(sessionKeys.PortfolioID);
                        ddlCategory.SelectedValue = HID_Category.Value;
                        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
                        //DefaultBinding();
                        panelVisibleSubCategory(true, false);
                        ddlSubCategory.SelectedValue = comm_add.Parameters["@SubCategoryID"].Value.ToString();
                        txtAddSubCategory.Text = string.Empty;
                    }
                    else if (i == -1)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Sub Category Already Exists";
                    }
                    else
                    {
                        lblError.Text = "Error Occured while inserting";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    protected void btnCancelSubCategory_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(HID_SubCategory.Value))
        {
            HID_SubCategory.Value = string.Empty;
            txtAddSubCategory.Text = string.Empty;
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        }
        panelVisibleSubCategory(true, false);
    }
    private void BindGrid(int customer,int category,int subCategory)
    {
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var portfolioList = pd.ProjectPortfolios.Select(p => p).ToList();
                    var inventoryBarcodeList = db.Inventory_AssociatedBarcodes.Select(i => i).ToList();

                    var result = (from a in inventoryBarcodeList
                                  join b in portfolioList on a.CustomerID equals b.ID
                                  join c in db.ServiceCatalog_categories on a.CategoryID equals c.ID
                                  join d in db.ServiceCatalog_categories on a.SubCategoryID equals d.ID orderby c.CategoryName
                                  select new { ID = a.ID, CustomerId=a.CustomerID, Customer = b.PortFolio, CategoryId=a.CategoryID, Category = c.CategoryName, SubCategoryId= a.SubCategoryID, SubCategory = d.CategoryName, Barcode = a.Barcode }).ToList();
                    if (customer != 0)
                        result = result.Where(r => r.CustomerId == customer).Select(r => r).ToList();
                    if (category != 0)
                        result = result.Where(r => r.CategoryId == category).Select(r => r).ToList();
                    if (subCategory != 0)
                        result = result.Where(r => r.SubCategoryId == subCategory).Select(r => r).ToList();

                    gvBarcode.DataSource = result;
                    gvBarcode.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgSaveItems_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                int customerId = int.Parse(ddlCustomer.SelectedValue);
                int categoryId = int.Parse(ddlCategory.SelectedValue);
                int subCategoryId = int.Parse(ddlSubCategory.SelectedValue);
                var exists = db.Inventory_AssociatedBarcodes.Where(i => i.CustomerID == customerId && i.CategoryID == categoryId && i.SubCategoryID == subCategoryId).Select(i => i).FirstOrDefault();
                if (exists == null)
                {
                    lblGridMsg.Visible = false;
                    Inventory_AssociatedBarcode inventory_AssociatedBarcode = new Inventory_AssociatedBarcode();
                    inventory_AssociatedBarcode.CustomerID = customerId;
                    inventory_AssociatedBarcode.CategoryID = categoryId;
                    inventory_AssociatedBarcode.SubCategoryID = subCategoryId;
                    inventory_AssociatedBarcode.Barcode = txtBarcode.Text.Trim();
                    db.Inventory_AssociatedBarcodes.InsertOnSubmit(inventory_AssociatedBarcode);
                    db.SubmitChanges();
                    BindGrid(customerId, categoryId, subCategoryId);

                }
                else
                {
                    lblGridMsg.Text = "Record already exists. Please check and try again.";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvBarcode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            int i = gvBarcode.EditIndex;
            GridViewRow Row = gvBarcode.Rows[i];
            string barcode = ((TextBox)Row.FindControl("txtBarcode")).Text.Trim();
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Inventory_AssociatedBarcode inventory_AssociatedBarcode = db.Inventory_AssociatedBarcodes.Where(a => a.ID == id).Select(a => a).FirstOrDefault();
                if (inventory_AssociatedBarcode != null)
                {
                    inventory_AssociatedBarcode.Barcode = barcode;
                    db.SubmitChanges();
                }
            }
        }
        if (e.CommandName == "Delete1")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Inventory_AssociatedBarcode inventory_AssociatedBarcode = db.Inventory_AssociatedBarcodes.Where(a => a.ID == id).Select(a => a).FirstOrDefault();
                if (inventory_AssociatedBarcode != null)
                {
                    db.Inventory_AssociatedBarcodes.DeleteOnSubmit(inventory_AssociatedBarcode);
                    db.SubmitChanges();
                }
            }
            BindGrid(int.Parse(ddlCustomer.SelectedValue),int.Parse(ddlCategory.SelectedValue),int.Parse(ddlSubCategory.SelectedValue));

        }
    }
    protected void gvBarcode_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvBarcode.EditIndex = e.NewEditIndex;
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

    }
    protected void gvBarcode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBarcode.EditIndex = -1;
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

    }
    protected void gvBarcode_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvBarcode.EditIndex = -1;
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));

    }
    protected void gvBarcode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBarcode.PageIndex = e.NewPageIndex;
        BindGrid(int.Parse(ddlCustomer.SelectedValue), int.Parse(ddlCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue));
    }
}