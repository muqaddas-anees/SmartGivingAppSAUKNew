using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using PortfolioMgt.BAL;

public partial class ServiceCatelogToCustomer_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblErr.Text = "";

                BindPopWindow();



            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    #region "Add Service catalog"

    private void BindPopWindow()
    {
        try
        {
            //BaseCache.Cache_Remove("vendorcatalogue");
            //Query_SerchItems
            using (PortfolioDataContext projectDB = new PortfolioDataContext())
            {
                var shopItems = (from r in projectDB.v_ServiceCatalog_admins
                                 orderby r.Type
                                 select r).ToList();


              
                //var shopItems = (BaseCache.Cache_Select("vendorcatalogue")) as List<ShopItems_vendorDetails>;
              
                    if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                    {
                        if (int.Parse(ddlCategory.SelectedValue) > 0)
                        {
                            shopItems = shopItems.Where(p => p.Category == int.Parse(ddlCategory.SelectedValue)).ToList();
                            if (!string.IsNullOrEmpty(ddlSubCategory.SelectedValue))
                            {
                                if (int.Parse(ddlSubCategory.SelectedValue) > 0)
                                {
                                    shopItems = shopItems.Where(p => p.Category == int.Parse(ddlSubCategory.SelectedValue)).ToList();
                                }
                            }
                        }
                    }

                

                if (int.Parse(ddlSelect.SelectedValue) > 0)
                {
                    shopItems = shopItems.Where(p => p.Type == int.Parse(ddlSelect.SelectedValue)).ToList();
                }

                if (!string.IsNullOrEmpty(txtItemDescription.Text.Trim()))
                {
                    shopItems = shopItems.Where(p => p.Description.Contains(txtItemDescription.Text.Trim())).ToList();
                }

                //var shopItems = projectDB.ExecuteQuery<ShopItems_vendorDetails>
                //    (Query_SerchItems(int.Parse(ddlVendors.SelectedValue), int.Parse(ddlSelect.SelectedValue), txtItemDescription.Text.Trim(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue)).ToList();

                if (shopItems != null)
                {
                    GridView2.DataSource = shopItems.Take(100);
                    GridView2.DataBind();
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private static IEnumerable<ShopItems_vendorDetails> NewMethod(projectTaskDataContext projectDB)
    {
        var shopItems_temp = (from p in projectDB.ShopItems_vendorDetails
                              select p).ToList();
        return shopItems_temp;
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
   


    public string GetItemsType(string value)
    {
        string val = "";
        if (value == "1")
        {
            val = "Labour";
        }
        if (value == "2")
        {
            val = "Product";
        }
        if (value == "3")
        {
            val = "Service";
        }
        return val;
    }

    protected void imgUpdate_Click(object sender, ImageClickEventArgs e)
    {
        AddItemsToBOM();

    }

    private void AddItemsToBOM()
    {
        
        try
        {
            int Defaultcurrence = 0;
           
            int items = 0;
            int Catalogid = 0;
            int categoryid = 0;
            int subcategoryID = 0;
            foreach (GridViewRow row in GridView2.Rows)
            {
                
                CheckBox chkRow = (CheckBox)row.FindControl("chkbox");
                if (chkRow.Checked)
                {
                    items++;
                    Label lblID = (Label)row.FindControl("lblID");
                    Label lblType = (Label)row.FindControl("lblType");
                    
                    Label lblDescription = (Label)row.FindControl("lblDescription");
                    Label lblCategoryID = (Label)row.FindControl("lblCategoryID");
                    Label lblSubcategoryID = (Label)row.FindControl("lblSubcategoryID");
                    PortfolioMgt.Entity.ServiceCatalog_category sc = new PortfolioMgt.Entity.ServiceCatalog_category();
                    //insert Category
                    sc =ServiceCatalog_Admin.ServiceCatalog_category_ByID(Convert.ToInt32(lblCategoryID.Text));
                    //sc.ID = 0;
                    sc.PortfolioID = sessionKeys.PortfolioID;
                    categoryid = ServiceCatalog_Admin.ServiceCatalog_CategoryInsertByPortoflio(sc);
                   //categoryid = 
                    //insert sub category
                    sc = ServiceCatalog_Admin.ServiceCatalog_category_ByID(Convert.ToInt32(lblSubcategoryID.Text));
                    //sc.ID = 0;
                    sc.PortfolioID = sessionKeys.PortfolioID;
                    sc.MasterID = categoryid;
                    subcategoryID = ServiceCatalog_Admin.ServiceCatalog_SubCategoryInsertByPortfolio(sc);

                    if (lblType.Text == "1")
                    {
                        ServiceCatelog_Labour sl = ServiceCatalog_Admin.ServiceCatelog_Labour_SelectByID(Convert.ToInt32(lblID.Text));
                        sl.ID = 0;
                        sl.PortfolioID = sessionKeys.PortfolioID;
                        sl.Category = categoryid;
                        sl.SubCategory = subcategoryID;

                        Catalogid = ServiceCatalog_Admin.ServiceCatelog_Labour_InsertByPortfolio(sl);
                      
                    }
                    else if (lblType.Text == "2")
                    {
                        ServiceCatelog_Material sm = ServiceCatalog_Admin.ServiceCatelog_Material_SelectByID(Convert.ToInt32(lblID.Text));
                        sm.ID = 0;
                        sm.PortfolioID = sessionKeys.PortfolioID;
                        sm.Category = categoryid;
                        sm.SubCategory = subcategoryID;
                        Catalogid=ServiceCatalog_Admin.ServiceCatelog_Material_InsertByPortfolio(sm);
                    }
                    else if (lblType.Text == "3")
                    {

                        ServiceCatelog_Service ss = ServiceCatalog_Admin.ServiceCatelog_Service_SelectByID(Convert.ToInt32(lblID.Text));
                        ss.ID = 0;
                        ss.PortfolioID = sessionKeys.PortfolioID;
                        ss.Category = categoryid;
                        ss.SubCategory = subcategoryID;
                        Catalogid=ServiceCatalog_Admin.ServiceCatelog_Service_Insert_ByPortfolio(ss);

                    }

                    if (Catalogid > 0)
                    {
                        //inset into assoicate id
                        ServiceCatalog_Admin.ServiceCatalog_Associate_insert(Convert.ToInt32(lblType.Text), Convert.ToInt32(lblID.Text), Catalogid);
                        Catalogid = 0;
                    }

                    chkRow.Checked = false;
                }

            }
            if (items == 0)
            {
                lblErr.Text = "Please select items to apply";

            }
            else
            {
                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "Selectd item(s) copied successfully.";
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgVendorSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindPopWindow();

    }
    protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPopWindow();

    }
    private string Query_SerchItems( int Type, string Description, string category, string subcategory)
    {

        string sql = string.Empty;
        sql = "select ID,VID,Type,Description,SP,VendorName,BP,Category,SubCategory from v_ServiceCatalog_admin where vid!=0";
        if (Type != 0)
        {
            sql += " and Type=" + Type.ToString();
        }
        if (!string.IsNullOrEmpty(Description))
        {
            sql += " and Description like '%" + Description + "%'";
        }
        if (!string.IsNullOrEmpty(category))
        {
            sql += " and Category = " + category;
        }
        if (!string.IsNullOrEmpty(subcategory))
        {
            sql += " and SubCategory = " + subcategory;
        }

        sql += " order by Type ";
        return sql;
    }
    #endregion
}