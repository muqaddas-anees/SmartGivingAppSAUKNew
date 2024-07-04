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
public partial class ProjectBOMVendorCatalogue : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblErr.Text = "";

                BindVendors(ddlVendors, string.Empty);
                //if (ddlVendors.Items.Count > 0)
                //{
                //    ddlVendors.SelectedIndex = 1;
                //}
                BindPopWindow();

                

            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    #region "Add Service catalog"
   
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void BindPopWindow()
    {
        try
        {
            BaseCache.Cache_Remove("vendorcatalogue");
            //Query_SerchItems
            using (projectTaskDataContext projectDB = new projectTaskDataContext())
            {
                //var shopItems = (from r in projectDB.ShopItems_vendorDetails
                //                 orderby r.Type
                //                 select r).ToList();


                if (BaseCache.Cache_Select("vendorcatalogue") == null)
                {
                    var shopItems_temp = NewMethod(projectDB);
                    BaseCache.Cache_Insert("vendorcatalogue", shopItems_temp);
                }

                var shopItems = (BaseCache.Cache_Select("vendorcatalogue")) as List<ShopItems_vendorDetails>;
                if (int.Parse(ddlVendors.SelectedValue) > 0)
                {
                    shopItems = shopItems.Where(p => p.VID == int.Parse(ddlVendors.SelectedValue)).ToList();

                    if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                    {
                        if (int.Parse(ddlCategory.SelectedValue) > 0)
                        {
                            shopItems = shopItems.Where(p => p.Category == int.Parse(ddlCategory.SelectedValue)).ToList();
                            if (!string.IsNullOrEmpty(ddlSubCategory.SelectedValue))
                            {
                                if (int.Parse(ddlSubCategory.SelectedValue) > 0)
                                {
                                    shopItems = shopItems.Where(p => p.SubCategory == int.Parse(ddlSubCategory.SelectedValue)).ToList();
                                }
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
    private void BindVendors(DropDownList ddlVendor, string setvalue)
    {


        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

            var vendorsList = from r in Vendors.v_Vendors

                              orderby r.ContractorName
                              select r;
            ddlVendor.DataSource = vendorsList;
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "ContractorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));

            //if (ddlVendor.Items.FindByText(setvalue) != null)
            //{
            //    ddlVendor.SelectedValue = ddlVendor.Items.FindByText(setvalue).Value;
            //}
            //else
            //{
            //    ddlVendor.SelectedValue = "0";
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

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

    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        int worksheetid = int.Parse(Request.QueryString["worksheetid"].ToString());
        AddItemsToBOM(worksheetid);

    }

    private void AddItemsToBOM(int worksheetid)
    {
        projectTaskDataContext InsertBOM = new projectTaskDataContext();
        try
        {

            var currencey = (from r in InsertBOM.ProjectDefaults
                             select r).ToList().FirstOrDefault();
            int Defaultcurrence = 0;
            if (currencey != null)
            {
                Defaultcurrence = currencey.DefaultCurrency.Value;
            }
            int items = 0;
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("chkbox");
                if (chkRow.Checked)
                {
                    items++;
                    Label lblID = (Label)row.FindControl("lblID");
                    ShopItems_vendorDetails vitems = InsertBOM.ShopItems_vendorDetails.Where(p => p.ID == int.Parse(lblID.Text)).FirstOrDefault();

                    Label lblType = (Label)row.FindControl("lblType");
                    Label lblVendorID = (Label)row.FindControl("lblVendorID");
                    Label lblDescription = (Label)row.FindControl("lblDescription");


                    ProjectMgt.Entity.ProjectBOM add = new ProjectMgt.Entity.ProjectBOM();
                    add.Description = lblDescription.Text;
                    add.PartNumber = vitems.PartNumber;
                    add.ProjectReference = QueryStringValues.Project;
                    add.Supplier = int.Parse(string.IsNullOrEmpty(lblVendorID.Text) ? "0" : lblVendorID.Text);
                    //add.Unit = Convert.ToDouble(txtUnitf.Text);
                    add.Unit = vitems.UnitPrice;
                    add.WorkSheetID = worksheetid;
                    add.Qty = 1;
                    add.Material = vitems.BP;
                    //add.Material = vitems.SP;
                    add.Labour = 0;
                    add.Mics = 0;//Convert.ToDouble(string.IsNullOrEmpty(txtMiscf.Text) ? "0" : txtMiscf.Text);
                    add.CurrencyID = Defaultcurrence;
                    add.SellingTotal = vitems.SP;
                    add.GP = vitems.BP > 0 ? ((vitems.SP - vitems.BP) / vitems.BP) * 100 : 0;
                    add.Unit = "1";
                    InsertBOM.ProjectBOMs.InsertOnSubmit(add);
                    InsertBOM.SubmitChanges();

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
    protected void imgVendorSearch_Click(object sender, EventArgs e)
    {
        BindPopWindow();
        
    }
    protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPopWindow();
        
    }
    private string Query_SerchItems(int VendorID, int Type, string Description, string category, string subcategory)
    {

        string sql = string.Empty;
        sql = "select ID,VID,Type,Description,SP,VendorName,BP,Category,SubCategory from v_ShopItems_vendor where vid!=0";
        if (VendorID != 0)
        {
            sql += " and vid=" + VendorID.ToString();
        }
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

    public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);

        ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
            }
        }
        else
        {
            eImageType = ImageManager.ImageType.OriginalData;
        }

        return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }

    public bool CheckImageVisibility(Guid a_guid)
    {
        bool _visible = false;
        if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        {
            _visible = true;
        }
        return _visible;
    }
}