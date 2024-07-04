using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SC_ContractorMaterialCS;
using Deffinity.ServiceCatalogManager;
using IMClass;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using AjaxControlToolkit;
using System.IO;
using System.Data.OleDb;
using UserMgt.DAL;
using ClosedXML.Excel;
using System.Web;
using Deffinity.BLL;
using iTextSharp.text.pdf;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using iTextSharp.text.pdf.draw;
using iTextSharp.text;
using System.Collections;


public partial class controls_InventoryCustomer : System.Web.UI.UserControl
{
    #region Intialization
    SC_ContractorMaterial SC_cntMaterialCS;
    InventoryManagerCs IMCS;
    public int PageType = 1;
    SqlConnection conn = new SqlConnection(Constants.DBString);

    public int VendorID = 0;
    int PortfolioID = 0;
    //InventoryRepository<InventoryMgt.Entity.ProjectPortfolio> IC = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Area> IA = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Locatin> IL = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Shelf> IS = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Bin> IB = null;
    InventoryRepository<InventoryFieldsConfig> INF = null;
    InventoryRepository<InventoryAdditionalInfo> IAinfo = null;
    InventoryRepository<inventoryCustomField> ICF = null;

    PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> IPP = null;
    InventoryRepository<InventoryMgt.Entity.InventoryManager_UsageCustomData> IMCD = null;


    InventoryRepository<Inventory_New_Stock> INS = null;
    InventoryRepository<InventoryManager> IM = null;
    InventoryRepository<InventoryManagerJournal> IMJ = null;
    InventoryRepository<UserMgt.Entity.Contractor> ICntr = null;
    InventoryRepository<InventoryManager_PSDProduct> IMPSD = null;
    InventoryManager_PSDProduct PSD = null;
    List<InventoryManager_PSDProduct> PSDList = null;
    InventoryRepository<GridFieldConfigurator> IGFC = null;
    InventoryRepository<InventoryManager_UsageCustomData> IUCD = null;

    List<InventoryManager_UsageCustomData> IM_ucd_List = null;
    GridFieldConfigurator GFC = null;
    Inventory_Area In_A = null;
    Inventory_Locatin In_L = null;
    Inventory_Shelf In_s = null;
    Inventory_Bin In_B = null;
    InventoryDataContext idc = null;
    InventoryFieldsConfig in_f = null;
    PortfolioMgt.Entity.ProjectPortfolio in_p = null;
    InventoryManager in_m = null;
    List<InventoryFieldsConfig> in_fList = null;
    InventoryDataContext Idc = null;
    projectTaskDataContext pdc = null;
    Inventory_New_Stock In_ns = null;
    InventoryManager In_Ma = null;
    InventoryManagerJournal In_mj = null;
    UserDataContext udc = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //Master.PageHead = Resources.DeffinityRes.InventMngr_Title;
        try
        {
            //IinventoryRepository<InventoryMgt.Entity.InventoryManager> IMR = new InventoryRepository<InventoryMgt.Entity.InventoryManager>();
            //var Ilist = IMR.GetAll().Where(o => o.PortfolioID == 1).ToList();
            if (!IsPostBack)
            {
                //Bind usage columns
                //BindColumns_UsageGrid();
                onLoad();
                BindCustomerInSearch();
                BindCustomerInNewItem();
                //BindData_SearchUsageGrid();
                //InventoryGridBind();
                //BindGridInSummary();

                int folderId = int.Parse(CreateFolder());
                hlinkExecption.NavigateUrl = "~/ProjectDocuments.aspx?mode=central&folderID=" + folderId + "&type=in";
                if (Request.QueryString["project"] != null)
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {
                        var projectDetails = pd.Projects.Where(p => p.ProjectReference == Convert.ToInt32(Request.QueryString["project"])).FirstOrDefault();
                        if (projectDetails != null)
                        {
                            ddlCustomer.SelectedValue = projectDetails.Portfolio.ToString();
                            BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
                            BindSite();
                            ddlSite.SelectedValue = projectDetails.SiteID.ToString();
                            BindProduct();
                        }
                    }
                }
                //sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString());
                //sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text.ToString();
                //GridInventoryBinding();


                if (int.Parse(ddlCustomer.SelectedValue) > 0)
                {
                    BindArea(int.Parse(ddlCustomer.SelectedValue));
                }
            }
            if (int.Parse(ddlCustomer.SelectedValue) > 0)
            {
                BindPlaceholderFields(int.Parse(ddlCustomer.SelectedValue));
                //BindSiteInPopup();
                //BindAreaInPopup(int.Parse(ddlCustomer.SelectedValue));

            }
            //if (sessionKeys.PortfolioID > 0)
            //{
            BindUsagePlaceholderFields();
            BindBatchPlaceholderFields();
            //BindBatchPlaceholderFields();
            //}
            HideFields();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    //Search Button Click

    private void onLoad()
    {
        DefaultBinding();
        DateTime dtMonday = DateTime.Today;
        //txtDateOrdered.Text = dtMonday.ToShortDateString();
    }
    public void VisibilityChecking(int CustomerId)
    {
        try
        {
            INF = new InventoryRepository<InventoryFieldsConfig>();
            var xList = INF.GetAll().Where(a => a.CustomerId == CustomerId && a.IsVisible == false).ToList();
            ControlsVisibility(true);
            foreach (var x in xList)
            {
                if (x.DefaultName == "Area")
                {
                    ControlsVisibility(false);
                }
                else if (x.DefaultName == "Location")
                {
                    ddllocation.Visible = false;
                    lblLocation.Visible = false;
                    lblShelf.Visible = false;
                    ddlshelf.Visible = false;
                    lblBin.Visible = false;
                    ddlbin.Visible = false;
                    ImageButtonbinAdd.Visible = false;
                    ImageButtonBinEdit.Visible = false;
                }
                else if (x.DefaultName == "Shelf")
                {
                    lblShelf.Visible = false;
                    ddlshelf.Visible = false;
                    lblBin.Visible = false;
                    ddlbin.Visible = false;
                    ImageButtonbinAdd.Visible = false;
                    ImageButtonBinEdit.Visible = false;
                }
                else if (x.DefaultName == "Bin")
                {
                    lblBin.Visible = false;
                    ddlbin.Visible = false;
                    ImageButtonbinAdd.Visible = false;
                    ImageButtonBinEdit.Visible = false;
                }
                else if (x.DefaultName == "Site")
                {
                    lblSite.Visible = false;
                    ddlSite.Visible = false;
                    ImageButtonbinAdd.Visible = false;
                    ImageButtonBinEdit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void AdjustColumnOrder()
    {
        try
        {

            //ColumnHeader ch = Listview.col[1];
            //ch.DisplayIndex = 3;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void ControlsVisibility(bool setvalue)
    {
        lblArea.Visible = setvalue;
        ddlarea.Visible = setvalue;
        ddllocation.Visible = setvalue;
        lblLocation.Visible = setvalue;
        lblShelf.Visible = setvalue;
        ddlshelf.Visible = setvalue;
        lblBin.Visible = setvalue;
        ddlbin.Visible = setvalue;
        ImageButtonbinAdd.Visible = setvalue;
        ImageButtonBinEdit.Visible = setvalue;
    }
    #region Bind Dropdowns
    public void BindCustomerInSearch()
    {
        try
        {
            ddlcustomerInSearch.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            ddlcustomerInSearch.DataTextField = "PortFolio";
            ddlcustomerInSearch.DataValueField = "ID";
            ddlcustomerInSearch.DataBind();
            ddlcustomerInSearch.Items.RemoveAt(0);
            ddlcustomerInSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
            ddlsiteInSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
            if (sessionKeys.PortfolioID > 0)
            {
                ddlcustomerInSearch.SelectedValue = sessionKeys.PortfolioID.ToString();
                BindSiteInSearch();
            }
            InventoryGridBind();
            BindData_SearchUsageGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindCustomerInNewItem()
    {
        try
        {
            ddlcustomerInNewItem.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            ddlcustomerInNewItem.DataTextField = "PortFolio";
            ddlcustomerInNewItem.DataValueField = "ID";
            ddlcustomerInNewItem.DataBind();
            ddlcustomerInNewItem.Items.RemoveAt(0);
            ddlcustomerInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
            ddlsiteInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));

            ddlcategoryInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
            ddlSubcategoryInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));



            if (sessionKeys.PortfolioID > 0)
            {
                ddlcustomerInNewItem.SelectedValue = sessionKeys.PortfolioID.ToString();
                BindSiteInNewItem();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    public void BindSiteInNewItem()
    {
        try
        {
            SC_cntMaterialCS = new SC_ContractorMaterial();
            //ddlSite.DataSource =  SC_cntMaterialCS.GetSites();
            ddlsiteInNewItem.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(int.Parse(ddlcustomerInSearch.SelectedValue));
            ddlsiteInNewItem.DataTextField = "Site";
            ddlsiteInNewItem.DataValueField = "ID";
            ddlsiteInNewItem.DataBind();
            ddlsiteInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindSupplier()
    {
        ddlSupplier.DataSource = Supplier();
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "ID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
    }
    private void BindProduct()
    {
        IMCS = new InventoryManagerCs();
        int categoryId = 0;
        if ((ddlCategory.SelectedItem.Value != null))
        {
            categoryId = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());


            int subcategoryId = 0;
            if ((ddlSubCategory.SelectedItem.Value != null))
            {
                subcategoryId = Convert.ToInt32(ddlSubCategory.SelectedItem.Value.ToString());
            }
            int siteId = 0;
            if ((ddlSite.SelectedItem.Value != null))
            {
                siteId = Convert.ToInt32(ddlSite.SelectedItem.Value.ToString());
            }

            var result = IMCS.SelectProducts(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()), categoryId,
                subcategoryId, siteId, "Global").ToList();
            //var groupByDescription = (from p in result select new { Id = p.ItemDescription, ItemDescription = p.ItemDescription }).ToList().OrderBy(p=>p.ItemDescription).Distinct();

            ddlProduct.DataSource = result;
            ddlProduct.DataValueField = "Id";
            ddlProduct.DataTextField = "ItemDescription";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }
        else
        {
            ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }

    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProduct();
        // GridInventoryBinding();
        PanelHide();
        PnlListview.Visible = false;
        pnlAddItems.Visible = true;
    }

    private void BindSubCategoryInNewItem(int CategoryID)
    {

        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlSubcategoryInNewItem.DataSource = SC_cntMaterialCS.GetSubCategory(CategoryID, int.Parse(ddlcustomerInNewItem.SelectedValue), 0);
        ddlSubcategoryInNewItem.DataValueField = "Id";
        ddlSubcategoryInNewItem.DataTextField = "CategoryName";
        ddlSubcategoryInNewItem.DataBind();
    }
    private void BindSubCategory(int CategoryID)
    {

        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlSubCategory.DataSource = SC_cntMaterialCS.GetSubCategory(CategoryID, int.Parse(ddlCustomer.SelectedValue), 0);
        ddlSubCategory.DataValueField = "Id";
        ddlSubCategory.DataTextField = "CategoryName";
        ddlSubCategory.DataBind();
    }
    public DataSet Supplier()
    {
        SqlDataAdapter da = new SqlDataAdapter("select VendorID as ID,ContractorName as SupplierName from v_Vendors order by ContractorName", conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Supplier");
        return ds;
    }
    // Bind Resouce customer - assigned to project
    public void BindResouceCustomer()
    {
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
            var customerList = (from p in pdt.ProjectDetails
                                join q in pdt.ProjectTaskItems on p.ProjectReference equals q.ProjectReference
                                join r in pdt.ProjectItems on q.ID equals r.ItemReference
                                where r.ContractorID == sessionKeys.UID
                                select new { ID = p.Portfolio, Name = p.PortfolioName }).Distinct().OrderBy(p => p.Name).ToList();

            ddlCustomer.DataSource = customerList;
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
            BindProduct();
            pnlInventory.Visible = false;
            sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString());
            sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text.ToString();
            BindSite();
            // GridInventoryBinding();
            PanelHide();
            PnlListview.Visible = true;
            pnlAddItems.Visible = true;
            if (ddlCustomer.SelectedValue != "0")
            {
                BindArea(int.Parse(ddlCustomer.SelectedValue));
                VisibilityChecking(int.Parse(ddlCustomer.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError.Visible = false;
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        BindProduct();
        //GridInventoryBinding();
        HID_Category.Value = ddlCategory.SelectedItem.Value.ToString();
        PanelHide();
        PnlListview.Visible = false;
        pnlAddItems.Visible = true;
    }
    //ddlProduct_SelectedIndexChanged
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridInventoryBinding();
        //GridInventoryBindingByProduct();
    }
    private void BindCustomer()
    {
        ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomer.DataTextField = "PortFolio";
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.RemoveAt(0);
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        // ddlCustomer.SelectedValue = Convert.ToString(sessionKeys.PortfolioID);
        //if (ddlCustomer.Items.Count == 0)
        //{
        //    ddlCustomer.Items.Insert(0, new ListItem("Please Select...", "0"));
        //}
        //For intial binding the inventory grid with products
        //GridInventoryBinding();

        ddlarea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        ddllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        ddlshelf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
    }
    public void BindArea(int cid)
    {
        try
        {
            IA = new InventoryRepository<InventoryMgt.Entity.Inventory_Area>();
            ddlarea.DataSource = IA.GetAll().Where(a => a.Cid == cid).OrderBy(a => a.Name).ToList();
            ddlarea.DataTextField = "Name";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();
            ddlarea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddllocation.Items.Clear();
            ddllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void BindAreaInNewItem(int cid)
    {
        try
        {
            IA = new InventoryRepository<InventoryMgt.Entity.Inventory_Area>();
            ddlareaInNewItem.DataSource = IA.GetAll().Where(a => a.Cid == cid).OrderBy(a => a.Name).ToList();
            ddlareaInNewItem.DataTextField = "Name";
            ddlareaInNewItem.DataValueField = "id";
            ddlareaInNewItem.DataBind();
            ddlareaInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlLocationInNewItem.Items.Clear();
            ddlLocationInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlShelfInNewItem.Items.Clear();
            ddlShelfInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlBinInNewItem.Items.Clear();
            ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindLocationInNewItem(int Aid)
    {
        try
        {
            IL = new InventoryRepository<InventoryMgt.Entity.Inventory_Locatin>();
            ddlLocationInNewItem.DataSource = IL.GetAll().Where(a => a.IA_id == Aid).OrderBy(a => a.Name).ToList();
            ddlLocationInNewItem.DataTextField = "Name";
            ddlLocationInNewItem.DataValueField = "id";
            ddlLocationInNewItem.DataBind();
            ddlLocationInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlShelfInNewItem.Items.Clear();
            ddlShelfInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlBinInNewItem.Items.Clear();
            ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindShelfInNewItem(int Lid)
    {
        try
        {
            IS = new InventoryRepository<InventoryMgt.Entity.Inventory_Shelf>();
            ddlShelfInNewItem.DataSource = IS.GetAll().Where(a => a.IL_id == Lid).OrderBy(a => a.Name).ToList();
            ddlShelfInNewItem.DataTextField = "Name";
            ddlShelfInNewItem.DataValueField = "id";
            ddlShelfInNewItem.DataBind();
            ddlShelfInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlBinInNewItem.Items.Clear();
            ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBinInNewItem(int Sid)
    {
        try
        {
            IB = new InventoryRepository<InventoryMgt.Entity.Inventory_Bin>();
            ddlBinInNewItem.DataSource = IB.GetAll().Where(a => a.IS_id == Sid).OrderBy(a => a.Name).ToList();
            ddlBinInNewItem.DataTextField = "Name";
            ddlBinInNewItem.DataValueField = "id";
            ddlBinInNewItem.DataBind();
            ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindLocation(int Aid)
    {
        try
        {
            IL = new InventoryRepository<InventoryMgt.Entity.Inventory_Locatin>();
            ddllocation.DataSource = IL.GetAll().Where(a => a.IA_id == Aid).OrderBy(a => a.Name).ToList();
            ddllocation.DataTextField = "Name";
            ddllocation.DataValueField = "id";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindShelf(int Lid)
    {
        try
        {
            IS = new InventoryRepository<InventoryMgt.Entity.Inventory_Shelf>();
            ddlshelf.DataSource = IS.GetAll().Where(a => a.IL_id == Lid).OrderBy(a => a.Name).ToList();
            ddlshelf.DataTextField = "Name";
            ddlshelf.DataValueField = "id";
            ddlshelf.DataBind();
            ddlshelf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBin(int Sid)
    {
        try
        {
            IB = new InventoryRepository<InventoryMgt.Entity.Inventory_Bin>();
            ddlbin.DataSource = IB.GetAll().Where(a => a.IS_id == Sid).OrderBy(a => a.Name).ToList();
            ddlbin.DataTextField = "Name";
            ddlbin.DataValueField = "id";
            ddlbin.DataBind();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //Binding Site dropdown
    private void BindSite()
    {

        SC_cntMaterialCS = new SC_ContractorMaterial();
        //ddlSite.DataSource =  SC_cntMaterialCS.GetSites();
        ddlSite.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(int.Parse(ddlCustomer.SelectedValue));
        ddlSite.DataTextField = "Site";
        ddlSite.DataValueField = "ID";
        ddlSite.DataBind();
        ddlSite.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));

    }
    //Binding Category dropdown
    private void BindCategory(int Portfolio)
    {
        Portfolio = Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString());
        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlCategory.DataSource = SC_cntMaterialCS.SelectAllCategory(Portfolio, PageType, VendorID);
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataBind();

    }
    private void BindCategoryInNewItem(int Portfolio)
    {
        Portfolio = Convert.ToInt32(ddlcustomerInNewItem.SelectedItem.Value.ToString());
        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlcategoryInNewItem.DataSource = SC_cntMaterialCS.SelectAllCategory(Portfolio, PageType, VendorID);
        ddlcategoryInNewItem.DataValueField = "ID";
        ddlcategoryInNewItem.DataTextField = "CategoryName";
        ddlcategoryInNewItem.DataBind();

    }
    #endregion
    private string GetCompanyName(int companyId)
    {
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            var result = (from p in pd.ProjectPortfolios
                          where p.ID == companyId
                          select p.PortFolio).FirstOrDefault();
            return result;
        }
    }
    TextBox txt = null;
    DropDownList ddl = null;
    Label lbl = null;
    RadioButtonList rbn = null;
    CheckBox chk = null;

    int txtid = 1;
    int ddlid = 1;
    int lblid = 1;
    int rbtnid = 1;
    int chkid = 1;

    #region Inventory Custom Fields
    string[] typeOfFields = new string[] { "text box", "number field", "date", "text area", "url" };
    public void BindPlaceholderFields(int customerId)
    {
        ph.Controls.Clear();
        InventoryMgr InMgr = new InventoryMgr();
        int cid = 0;
        try
        {
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            if (ddlProduct.SelectedValue != "0")
            {
                cid = int.Parse(ddlProduct.SelectedValue);
            }
            List<InventoryAdditionalInfo> flsAdditionalInfoList = InMgr.GetFLSAdditonalInfoByCallID(cid);

            List<inventoryCustomField> clist = InMgr.GetFieldList(customerId).ToList();
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "100%");
            TableRow tr = new TableRow();
            TableCell td = null;
            int cnt = 0;
            int jcnt = 1;
            int totalCnt = clist.Count();
            if (clist.Count > 0)
            {
                //if (cid > 0)
                //{
                pnlCustomFields.Visible = true;
                lblCustomFiledCustomer.Text = GetCompanyName(Convert.ToInt32(customerId));
                //}
                //else
                //{
                //    pnlCustomFields.Visible = false;
                //}
            }
            else
                pnlCustomFields.Visible = false;

            foreach (inventoryCustomField c in clist)
            {
                string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                string rval = string.Empty;
                if (val != null)
                    rval = val.ToString();

                if (tr == null)
                    tr = new TableRow();

                if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox(c.ID.ToString(), rval, val, Isfirsttime, c.TypeOfField.ToLower(), Convert.ToBoolean(c.Mandatory), c.LabelName, c.MinimumValue, c.MaximumValue, c.DefaultText));
                    if (c.TypeOfField.ToLower() == "date")
                    {
                        td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                    }
                    td.Style.Add("width", "390px");
                    tr.Cells.Add(td);
                }
                else if (c.TypeOfField.ToLower() == "dropdown list")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateDropDown(c.ListValue, c.ID.ToString(), rval, Isfirsttime, Convert.ToBoolean(c.Mandatory), c.LabelName));
                    td.Style.Add("width", "390px");
                    tr.Cells.Add(td);
                }
                else if (c.TypeOfField.ToLower() == "radio button")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateRadioButton(c.ListValue, c.ID.ToString(), rval, Isfirsttime));
                    td.Style.Add("width", "390px");
                    tr.Cells.Add(td);
                }
                else if (c.TypeOfField.ToLower() == "checkbox")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateCheckbox(c.ID.ToString(), rval, Isfirsttime));
                    td.Style.Add("width", "390px");
                    tr.Cells.Add(td);
                }
                cnt = cnt + 1;
                if (cnt == 2)
                {
                    tbl.Rows.Add(tr);
                    tr = null;
                    cnt = 0;
                }
                if (jcnt == totalCnt && cnt == 1)
                {
                    tbl.Rows.Add(tr);
                    tr = null;
                }
                jcnt = jcnt + 1;
            }
            ph.Controls.Add(tbl);
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    InventoryAdditionalInfo flsAdditionalInfo = null;
    public void SettingDataToPlaceHolder(int ProductID, int customerId)
    {
        try
        {
            InventoryMgr InMgr = new InventoryMgr();
            List<inventoryCustomField> clist = InMgr.GetFieldList(customerId).ToList();
            foreach (inventoryCustomField c in clist)
            {
                flsAdditionalInfo = InMgr.GetFLSAdditonalInfoByCallID(ProductID).Where(p => p.CustomFieldID == c.ID).FirstOrDefault();
                if (flsAdditionalInfo != null)
                {
                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            txt.Text = flsAdditionalInfo.CustomFieldValue;
                        }
                    }
                    else
                    {
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                ddl.SelectedValue = flsAdditionalInfo.CustomFieldValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {

                                chk.Checked = Boolean.Parse(flsAdditionalInfo.CustomFieldValue);
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                rdbtn.SelectedValue = flsAdditionalInfo.CustomFieldValue;
                            }
                        }
                    }
                }
                else
                {
                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            txt.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                ddl.SelectedValue = "0";
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                chk.Checked = false;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                rdbtn.SelectedValue = "0";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void SavePlaceholderData(int ProductID, int customerId)
    {
        try
        {
            ViewState["state"] = "2";
            InventoryMgr InMgr = new InventoryMgr();

            List<inventoryCustomField> clist = InMgr.GetFieldList(customerId).ToList();

            foreach (inventoryCustomField c in clist)
            {
                flsAdditionalInfo = InMgr.GetFLSAdditonalInfoByCallID(ProductID).Where(p => p.CustomFieldID == c.ID).FirstOrDefault();
                if (flsAdditionalInfo == null)
                {
                    flsAdditionalInfo = new InventoryAdditionalInfo();
                    flsAdditionalInfo.productId = ProductID;
                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            flsAdditionalInfo.CustomFieldValue = txt.Text;
                        }

                    }
                    else
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                    flsAdditionalInfo.CustomFieldID = c.ID;
                    InMgr.InsertFLSAdditionalInfo(flsAdditionalInfo);

                }
                else
                {

                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            flsAdditionalInfo.CustomFieldValue = txt.Text;
                        }
                    }
                    else
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                    InMgr.UpdateFLSAddtionalInfo(flsAdditionalInfo);
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }
    public Label GenerateLable(string lblName)
    {
        lbl = new Label();
        lbl.ID = "lbl" + lblid.ToString();
        lbl.Text = lblName;
        lbl.EnableViewState = true;
        lblid = lblid + 1;
        return lbl;
    }
    string validationGroup = "Custom";
    public TextBox GenerateTextbox(string id, string setvalue, string val, bool Isfirsttime, string type, bool isMandatory, string labelName, string minValue, string maxValue, string defaultText)
    {
        txt = new TextBox();
        try
        {
            txt.ID = id;
            txt.Width = 175;
            //  txt.Text = setvalue;
            txt.SkinID = "txt_field";
            if (type == "text area")
                txt.TextMode = TextBoxMode.MultiLine;
            txt.EnableViewState = true;

            //if (val == null)
            //{
            //    txt.Text = defaultText;
            //}
            if (!string.IsNullOrEmpty(setvalue))
                txt.Text = setvalue;


            // when we create fls new form set defult text from admin
            if (Request.QueryString["callid"] == null)
            {

            }
            //Validator settings
            if (isMandatory)
            {
                RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();

                requiredFieldValidator.ControlToValidate = txt.ID;
                requiredFieldValidator.ErrorMessage = "Please enter " + labelName.ToLower();
                requiredFieldValidator.SetFocusOnError = true;
                //rfv.Text = "*";
                requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                requiredFieldValidator.ValidationGroup = validationGroup;

                ph.Controls.Add(requiredFieldValidator);
            }
            if (type == "number field")
            {
                RangeValidator rangeValidator = new RangeValidator();
                rangeValidator.MinimumValue = minValue;
                rangeValidator.MaximumValue = maxValue;
                rangeValidator.ControlToValidate = txt.ID;
                rangeValidator.Type = ValidationDataType.Double;
                rangeValidator.SetFocusOnError = true;
                rangeValidator.ValidationGroup = validationGroup;
                rangeValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                rangeValidator.ErrorMessage = "The " + labelName.ToLower() + " must be " + rangeValidator.MinimumValue + " to " + rangeValidator.MaximumValue;

                ph.Controls.Add(rangeValidator);
            }
            if (type == "date")
            {
                txt.Width = 80;

                CalendarExtender calendarExtender = new CalendarExtender();
                calendarExtender.PopupButtonID = "imgDate" + id;
                calendarExtender.Format = Deffinity.systemdefaults.GetDateformat();
                calendarExtender.TargetControlID = txt.ID;
                calendarExtender.CssClass = "MyCalendar";
                ph.Controls.Add(calendarExtender);


                RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                regularExpressionValidator.ControlToValidate = txt.ID;
                regularExpressionValidator.SetFocusOnError = true;
                regularExpressionValidator.Display = ValidatorDisplay.None;
                regularExpressionValidator.ErrorMessage = "Please enter a valid " + labelName.ToLower();
                regularExpressionValidator.ValidationExpression = "(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\\d\\d";
                regularExpressionValidator.ValidationGroup = validationGroup;
                ph.Controls.Add(regularExpressionValidator);
            }
            txtid = txtid + 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return txt;
    }

    public System.Web.UI.WebControls.Image GenerateCalendarImageButton(string id)
    {
        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
        img.ID = "imgDate" + id;
        img.ImageAlign = ImageAlign.AbsMiddle;
        img.ImageUrl = "~/media/icon_calender.png";
        return img;
    }

    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, bool isMandatory, string labelName)
    {
        ddl = new DropDownList();
        ddl.ID = id;
        ddl.Width = 200;
        string[] str = System.Text.RegularExpressions.Regex.Split(Items, @"\s*,\s*");
        // The regex \s*,\s* can be read as: "match zero or more white space characters,
        //followed by a comma followed by zero or more white space characters".
        // http://stackoverflow.com/questions/1483645/what-is-the-cleanest-way-to-remove-all-extra-spaces-from-a-user-input-comma-deli
        System.Array.Sort(str);
        foreach (string s in str)
            ddl.Items.Add(s);
        ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        ddl.AutoPostBack = true;
        //if (!string.IsNullOrEmpty(setvalue))
        //  ddl.SelectedValue = setvalue;

        //RequiredFieldValidator setting
        if (isMandatory)
        {
            RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
            requiredFieldValidator.ControlToValidate = ddl.ID;
            requiredFieldValidator.ErrorMessage = "Please select " + labelName.ToLower();
            requiredFieldValidator.SetFocusOnError = true;
            //rfv.Text = "*";
            requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
            requiredFieldValidator.ValidationGroup = validationGroup;
            requiredFieldValidator.InitialValue = "0";
            ddl.ValidationGroup = validationGroup;
            ph.Controls.Add(requiredFieldValidator);
        }

        ddlid = ddlid + 1;
        return ddl;
    }
    public void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (DropDownList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
    }
    public RadioButtonList GenerateRadioButton(string Items, string id, string setvalue, bool Isfirsttime)
    {
        rbn = new RadioButtonList();
        rbn.ID = id;
        rbn.Width = 200;
        string[] str = Items.Split(',').ToArray();
        foreach (string s in str)
            rbn.Items.Add(s);
        rbn.EnableViewState = true;
        rbn.SelectedIndexChanged += rbn_SelectedIndexChanged;
        rbn.AutoPostBack = true;
        //  if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
        //    rbn.SelectedValue = setvalue;
        rbtnid = rbtnid + 1;
        return rbn;

    }
    public void rbn_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (RadioButtonList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
    }
    public CheckBox GenerateCheckbox(string id, string setvalue, bool Isfirsttime)
    {
        bool val;
        if (string.IsNullOrEmpty(setvalue))
        {
            val = false;
        }
        else
        {
            val = bool.Parse(setvalue);
        }
        chk = new CheckBox();
        chk.ID = id;
        //chk.Width = 200;
        //chk.Checked = val;

        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chk.Checked = val;
        chkid = chkid + 1;
        return chk;
    }
    #endregion

    private void DefaultBinding()
    {
        if (!CheckPermission())
        {
            BindResouceCustomer();
            HideControls();
        }
        else
        {
            BindCustomer();
            ShowControls();
        }
        BindCategory(Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString()));
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
        BindSite();
        BindProduct();

        BindManufacurer();
        BindSupplier();
    }

    private bool CheckPermission()
    {
        // other than admin user type hide the controls
        bool permission = true;
        if (sessionKeys.SID != 1 && sessionKeys.SID != 2 && sessionKeys.SID != 3)
            permission = false;
        return permission;
    }



    //Hide contols when resource comes
    private void HideControls()
    {
        btnAddNewItem.Visible = false;
        imgbtnViewInven.Visible = false;
        imgAddInventory.Visible = false;
        btnaddcategory.Visible = false;
        btn_CategoryEdit.Visible = false;
        btnDeleteCategory.Visible = false;
        btnaddsubcategory.Visible = false;
        btn_editSubCategory.Visible = false;
        btnSubCategory.Visible = false;
        btnSaveMaterial.Visible = false;
        imgbtnUpdateMaterial.Visible = false;
        ImageButton2.Visible = false;
        BtnAddManufacturer.Visible = false;
        //tblDeployedDate.Visible = false;
        chkAddSCMCatalogue.Visible = false;
    }
    private void ShowControls()
    {
        btnAddNewItem.Visible = true;
        imgbtnViewInven.Visible = false;
        imgAddInventory.Visible = true;
        btnaddcategory.Visible = true;
        btn_CategoryEdit.Visible = true;
        btnDeleteCategory.Visible = true;
        btnaddsubcategory.Visible = true;
        btn_editSubCategory.Visible = true;
        btnSubCategory.Visible = true;
        btnSaveMaterial.Visible = true;
        imgbtnUpdateMaterial.Visible = true;
        ImageButton2.Visible = true;
        BtnAddManufacturer.Visible = true;
        // tblDeployedDate.Visible = true;
        chkAddSCMCatalogue.Visible = true;
    }

    private void PanelHide()
    {

        pnlAddItems.Visible = false;
        pnlUse.Visible = false;
    }

    protected void imgAddInventory_Click(object sender, ImageClickEventArgs e)
    {
        SC_cntMaterialCS = new SC_ContractorMaterial();
        try
        {
            IMCS = new InventoryManagerCs();
            int retVal = -1;
            int site = ddlSite.SelectedIndex;
            int cust = ddlCustomer.SelectedIndex;
            // int CustomerId =Convert.ToInt32(ddlCustomer.SelectedValue.ToString());
            if (cust != 0)
            {
                //if (site != 0)
                //{
                int ProductId = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                if (ProductId != 0)
                {
                    if (chkAddSCMCatalogue.Checked == true)
                    {

                        int SiteId = Convert.ToInt32(ddlSite.SelectedValue.ToString());
                        //int Qty = 0;
                        //int ReOrderLevel = 0;
                        string SectionType = "Global";
                        int ProductID = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                        DataTable _dt = SC_cntMaterialCS.ProductExistBySiteSection(ProductID, SiteId, SectionType);
                        if (_dt.Rows.Count > 0)
                        {
                            lblError.Visible = true;
                            lblError.Text = "Product already Exists";
                        }
                        else
                        {
                            int ProjectReference = 0;
                            int ContractorID = sessionKeys.UID;
                            int PortfolioID = int.Parse(ddlCustomer.SelectedValue); //sessionKeys.PortfolioID;
                            string ItemDescription = ddlProduct.SelectedItem.Text.Trim();
                            DataTable _dtt = IMCS.SelectProductByPID(int.Parse(ddlProduct.SelectedItem.Value.ToString()));
                            if (_dtt.Rows.Count > 0)
                            {
                                DataRow _dr = _dtt.Rows[0];
                                string ItemDesc = _dr["ItemDescription"].ToString();
                                int Supplier = int.Parse(_dr["Supplier"].ToString());
                                string PartNumber = _dr["PartNumber"].ToString();
                                string UnitPrice = string.Empty;
                                double BuyingPrice = 0;
                                double SellingPrice = 0;
                                int UnitsinStock = int.Parse(_dr["QTY"].ToString());
                                int ReOrderLevel = int.Parse(_dr["ReOrderLevel"].ToString());
                                string Notes = string.Empty;
                                string ItemLock = string.Empty;
                                string ItemDelete = string.Empty;
                                double WorstCase = 0;
                                double BestCase = 0;
                                double MostLikelyCase = 0;
                                string Currency = string.Empty;
                                string WCEXtension = string.Empty;
                                string BCEXtension = string.Empty;
                                string MCEXtension = string.Empty;
                                int Qty = UnitsinStock;
                                string ii = _dr["Image"].ToString();
                                Guid Image1 = new Guid(ii.ToString());
                                int Category = int.Parse(_dr["Categoery"].ToString());
                                int SubCategory = int.Parse(_dr["SubCategory"].ToString());
                                int Type = 0;
                                int VendorID = 0;
                                int QtyOnOrder = Convert.ToInt32(_dr["QtyOnOrder"].ToString());
                                DateTime tDate = Convert.ToDateTime(_dr["ExpctArvlDate"].ToString());
                                int Manufacturer = Convert.ToInt32(_dr["Manufacturer"].ToString());
                                string Colour = string.Empty;
                                int Length = 0;
                                int LeadTime = Convert.ToInt32(_dr["LeadTime"].ToString());
                                int Replenish = 0;

                                retVal = SC_cntMaterialCS.InsertMaterial(ProjectReference, ContractorID, PortfolioID, ItemDesc, Supplier,
                                    PartNumber, UnitPrice, BuyingPrice, SellingPrice, UnitsinStock, ReOrderLevel, Notes, ItemLock,
                                    ItemDelete, WorstCase, BestCase, MostLikelyCase, Currency, WCEXtension, BCEXtension, MCEXtension,
                                    Qty, Image1, Category, SubCategory, Type, PageType, VendorID, QtyOnOrder, tDate, Manufacturer, Colour,
                                    Length, LeadTime, Replenish);
                                if (retVal > 0)
                                {
                                    GridInventoryBinding();
                                    lblError.Visible = false;
                                    lblMsgDepartment.Visible = true;
                                    lblMsgDepartment.Text = "Product successfully added to Service Catalogue";

                                }


                            }

                            ////Inserting New row
                            //SC_cntMaterialCS.InsertMaterial(SiteId, ProductId, Qty, ReOrderLevel, SectionType);
                            //GridInventoryBinding();

                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please Select Product";
                    //please select product
                }
                //}
                //else
                //{
                //    // please select Site
                //    lblError.Visible = true;
                //    lblError.Text = "Please Select Site";
                //}
            }
            else
            {
                // please select Customer
                lblError.Visible = true;
                lblError.Text = "Please Select Customer";
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
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
        string imgPath = string.Empty;
        if (a_gId.ToString() != "00000000-0000-0000-0000-000000000000")
            imgPath = "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
        else
            imgPath = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";

        return imgPath;
        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
        
    }

    #region Inventory Grid
    public bool CheckImageVisibility(Guid a_guid)
    {
        bool _visible = true;
        //if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        //{
        //    _visible = true;
        //}
        return _visible;
    }

    //According Product Selection From Product Dropdown
    private void GridInventoryBindingByProduct()
    {
        SC_cntMaterialCS = new SC_ContractorMaterial();
        pnlInventory.Visible = true;
        grdInventory.DataSource = SC_cntMaterialCS.SelectMaterial(Convert.ToInt32(ddlProduct.SelectedValue.ToString()));
        grdInventory.DataBind();
    }
    private void GridInventoryBinding()
    {
        try
        {
            // IMCS = new InventoryManagerCs();
            pnlInventory.Visible = true;
            int customer = Convert.ToInt32(ddlcustomerInSearch.SelectedValue.ToString());
            int category = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            int subCategory = Convert.ToInt32(ddlSubCategory.SelectedValue.ToString());
            string product = ddlProduct.SelectedItem.Text.ToString();
            int site = Convert.ToInt32(ddlSite.SelectedValue.ToString());

            using (InventoryDataContext db = new InventoryDataContext())
            {
                var result = (from i in db.InventoryManagers
                              join c in db.ServiceCatalog_categories on i.Categoery equals c.ID
                              join sc in db.ServiceCatalog_categories on i.SubCategory equals sc.ID
                              join m in db.Manufacturers on i.Manufacturer equals m.Id into gg
                              from p in gg.DefaultIfEmpty()
                              join s in db.Sites on i.SiteId equals s.ID
                              orderby c.CategoryName, sc.CategoryName, i.ItemDescription, s.Site1
                              select new
                              {
                                  i.Id,
                                  i.Qty,
                                  i.QtyOnOrder,
                                  i.Categoery,
                                  i.SubCategory,
                                  i.SiteId,
                                  i.ExpctArvlDate,
                                  i.Image,
                                  i.ItemDescription,
                                  i.Notes,
                                  i.PortfolioID,
                                  CategoryName = c.CategoryName,
                                  SubCategoryName = sc.CategoryName,
                                  ManufacturerName = p.Name,
                                  Sitename = s.Site1,
                                  AreaName = db.Inventory_Areas.Where(z => z.id == i.Area).Select(z => z.Name).FirstOrDefault(),
                                  LocationName = db.Inventory_Locatins.Where(z1 => z1.id == i.Location).Select(z1 => z1.Name).FirstOrDefault(),
                                  ShelfName = db.Inventory_Shelfs.Where(z3 => z3.id == i.Shelf).Select(z3 => z3.Name).FirstOrDefault(),
                                  BinName = db.Inventory_Bins.Where(z4 => z4.id == i.Bin).Select(z4 => z4.Name).FirstOrDefault()
                              }).ToList();
                if (customer != 0)
                {
                    result = result.Where(r => r.PortfolioID == customer).ToList();
                }
                if (category != 0)
                    result = result.Where(r => r.Categoery == category).ToList();
                if (subCategory != 0)
                    result = result.Where(r => r.SubCategory == subCategory).ToList();
                if (site != 0)
                    result = result.Where(r => r.SiteId == site).ToList();
                if (product != "Please Select...")
                    result = result.Where(r => r.ItemDescription == product).ToList();
                grdInventory.DataSource = result;
                grdInventory.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //grdInventory.DataSource = IMCS.SelectProducts(cust, cat, sub, site, "Global");
    }
    //grdInventory_PageIndexChanging
    protected void grdInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdInventory.EditIndex = e.NewEditIndex;
        InventoryGridBind();

    }
    protected void grdInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInventory.PageIndex = e.NewPageIndex;
        InventoryGridBind();
    }
    //grdInventory_RowCancelingEdit
    protected void grdInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdInventory.EditIndex = -1;
        InventoryGridBind();

    }
    //'grdInventory_RowCommand' 
    //protected void grdInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //}
    ////grdInventory_RowUpdating
    protected void grdInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdInventory.EditIndex = -1;
        InventoryGridBind();
    }

    #endregion

    private bool IsValid(string fileName)
    {

        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {

            case ".csv":
                return true;
            case ".xlsx":
                return true;
            case ".xls":
                return true;

            default:
                return false;
        }

    }

    private void BindProductsSection(string id)
    {
        try
        {
            pnlAddItems.Visible = true;
            CleargrdInventryFields();
            lblMessage.Text = string.Empty;
            lblItemMsg.Text = string.Empty;
            lblMsgDepartment.Visible = false;
            int ID = Convert.ToInt32(id);
            hfID.Value = ID.ToString();
            //int i = grdInventory.EditIndex;
            //GridViewRow Row = grdInventory.Rows[i];
            //BindStorageGrid(ID);
            IMCS = new InventoryManagerCs();
            DataTable _dt = IMCS.SelectProductByPID(ID);
            if (_dt.Rows.Count > 0)
            {
                btnSaveMaterial.Visible = false;
                if (!CheckPermission())
                {
                    imgbtnUpdateMaterial.Visible = false;
                }
                else
                {
                    imgbtnUpdateMaterial.Visible = true;
                }
                DataRow _dr = _dt.Rows[0];
                BindCustomer();
                ddlCustomer.SelectedValue = _dr["PortfolioID"].ToString();
                BindSite();
                ddlSite.SelectedValue = _dr["SiteId"].ToString();
                BindCategory(int.Parse(ddlCustomer.SelectedValue));
                ddlCategory.SelectedValue = _dr["Categoery"].ToString();
                BindSubCategory(int.Parse(_dr["Categoery"].ToString()));
                ddlSubCategory.SelectedValue = _dr["SubCategory"].ToString();
                string siteId = _dr["SiteId"].ToString();
                hfSiteID.Value = siteId;
                //ddlSite.SelectedValue = siteId;
                BindProduct();
                ddlProduct.SelectedValue = _dr["Id"].ToString();
                txtNotes.Text = _dr["Notes"].ToString();
                hfSubcatID.Value = _dr["SubCategory"].ToString();
                txtItemDesc.Text = _dr["ItemDescription"].ToString();
                ddlSupplier.SelectedValue = _dr["Supplier"].ToString();
                txtPartNumber.Text = _dr["PartNumber"].ToString();
                txtStock.Text = _dr["QTY"].ToString();//code changed here
                txtStockLevel.Text = getActualStackLaevel(id).ToString();
                txtBarcode.Text = _dr["Barcode"].ToString();
                txtReorderlevel.Text = _dr["ReOrderLevel"].ToString();
                txtQntyOrder.Text = _dr["QtyOnOrder"].ToString();
                ddlManufacturer.SelectedValue = string.IsNullOrEmpty(_dr["Manufacturer"].ToString()) ? "0" : _dr["Manufacturer"].ToString();
                DateTime tDate = Convert.ToDateTime(string.IsNullOrEmpty(_dr["ExpctArvlDate"].ToString()) ? "01/01/1900" : _dr["ExpctArvlDate"].ToString());
                String tempDate = FormateDate(tDate.ToString(Deffinity.systemdefaults.GetDateformat()));
                txtArrivalDate.Text = tempDate;
                DateTime OrdDate = Convert.ToDateTime(string.IsNullOrEmpty(_dr["DateOrdered"].ToString()) ? "01/01/1900" : _dr["DateOrdered"].ToString());
                String tempOrdDate = FormateDate(OrdDate.ToString(Deffinity.systemdefaults.GetDateformat()));
                txtDateOrdered.Text = tempOrdDate;
                //txtArrivalDate.Text = _dr["ExpctArvlDate"].ToString();
                txtLeadTime.Text = _dr["LeadTime"].ToString();
                hdnItemID.Value = ID.ToString();
                hdnImageID.Value = _dr["Image"].ToString();
                ImgProfile.ImageUrl = "~/WF/UploadData/OriginalData/" + _dr["Image"].ToString() + ".png";
                pnlUse.Visible = true;
                ltlProduct.Text = _dr["ItemDescription"].ToString();

                BindArea(int.Parse(ddlCustomer.SelectedValue));
                ddlarea.SelectedValue = string.IsNullOrEmpty(_dr["Area"].ToString()) ? "0" : _dr["Area"].ToString();
                BindLocation(int.Parse(ddlarea.SelectedValue));
                ddllocation.SelectedValue = string.IsNullOrEmpty(_dr["Location"].ToString()) ? "0" : _dr["Location"].ToString();
                BindShelf(int.Parse(ddllocation.SelectedValue));
                ddlshelf.SelectedValue = string.IsNullOrEmpty(_dr["Shelf"].ToString()) ? "0" : _dr["Shelf"].ToString();
                BindBin(int.Parse(ddlshelf.SelectedValue));
                ddlbin.SelectedValue = string.IsNullOrEmpty(_dr["Bin"].ToString()) ? "0" : _dr["Bin"].ToString();

                using (InventoryDataContext db = new InventoryDataContext())
                {
                    var siteByName = db.Sites.Where(s => s.ID == int.Parse(siteId)).Select(s => s.Site1).FirstOrDefault();
                    if (siteByName != null)
                        ltlSite.Text = siteByName;
                    //int qtyInUse = db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == int.Parse(id) && i.SiteId == int.Parse(siteId)&&i.UseStatus==true).Select(i => i).ToList().Count();
                    //   txtQtyInUse.Text = GetQtyInUse(id).ToString();
                    txtQtyInUse.Text = GetQtyInUseFromIm_PSDProducts(id).ToString();
                }
                BindUseGrid(int.Parse(hfSiteID.Value), ID);

            }
            //pnlStorageDetails.Visible = true;
            //GridInventoryBinding();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //grdInventory_RowEditing
    public int getActualStackLaevel(string id)
    {
        int Sumvalue = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Sumvalue = (int)(from a in db.InventoryManagerJournals
                                 where a.InventoryId == int.Parse(id) && a.SectionType == "Global"
                                 orderby a.Id descending
                                 select a.Qty).FirstOrDefault();
                int ReplenishQty = GetQtyReplenishFromIm_PSDProducts(id);
                int InuseQty = GetQtyInUseFromIm_PSDProducts(id);
                int DisposeQty = GetDisposeQtyFromIm_PSDProducts(id);
                Sumvalue = Sumvalue + ReplenishQty - InuseQty - DisposeQty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Sumvalue;
    }
    public int GetQtyReplenishFromIm_PSDProducts(string id)
    {
        int Sumvalue = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Sumvalue = (int)db.InventoryManager_PSDProducts.Where(i => i.ProductId == int.Parse(id) && i.SectionType == "PROJECT" && i.Status == 3).Select(i => i.QtyUsed).ToList().Sum();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Sumvalue;
    }
    public int GetDisposeQtyFromIm_PSDProducts(string id)
    {
        int Sumvalue = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Sumvalue = (int)db.InventoryManager_PSDProducts.Where(i => i.ProductId == int.Parse(id) && i.SectionType == "PROJECT" && i.Status == 2).Select(i => i.QtyUsed).ToList().Sum();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Sumvalue;
    }
    public int GetQtyInUseFromIm_PSDProducts(string id)
    {
        int Sumvalue = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Sumvalue = (int)db.InventoryManager_PSDProducts.Where(i => i.ProductId == int.Parse(id) && i.SectionType == "PROJECT" && i.Status == 1).Select(i => i.QtyUsed).ToList().Sum();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Sumvalue;
    }
    public void GetQtyAvailableFromIm_PSDProducts(string id)
    {

    }
    public int GetQtyInUse(string id)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == int.Parse(id) && i.UseStatus == true && i.UseID != 0).Select(i => i).ToList().Count();
        }
    }
    public int GetQtyAvailable(string id)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == int.Parse(id) && i.UseStatus == false).Select(i => i).ToList().Count();
        }
    }

    private void BindHistory(int inventoryId)
    {
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    var journalList = db.InventoryManagerJournals.Where(j => j.InventoryId == inventoryId).Select(j => j).ToList();
                    var useList = db.Inventory_Uses.ToList();
                    useList.Add(new Inventory_Use { ID = 0, Code = "", Name = "" });
                    var result = (from j in journalList
                                  join c in contractorList on j.ModifiedBy equals c.ID
                                  join u in useList.DefaultIfEmpty() on j.UseID equals u.ID
                                  orderby j.MdofiedDate descending
                                  select new { ID = j.Id, j.ItemDescription, j.Qty, j.NoDeployed, j.MdofiedDate, ModifiedBy = c.ContractorName, j.Notes, j.ReasonCode, j.TransferQty, j.OpeningStock, UseCode = u.Name }).ToList();
                    GvHistory.DataSource = result;
                    GvHistory.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }



    protected void grdInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //grdInventory_RowCommand
            if (e.CommandName == "Batch")
            {
                try
                {
                    Label7.Text = "Add Batch";
                    // mdlpopupinGrid.Hide();
                    mdlpopupForNewItem.Show();
                    hid_batchproduct.Value = "0";
                    txtQtyEdit.Text = string.Empty;
                    txtProductEdit.Text = string.Empty;
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    hid_batchproduct.Value = ID.ToString();
                    //var s = grdInventory.DataKeys[grdInventory.SelectedRow.RowIndex].Values[4].ToString();
                    txtBatchreference.Text = GetBatchReference(ID).ToString();
                    IMCS = new InventoryManagerCs();
                    DataTable _dt = IMCS.SelectProductByPID(ID);
                    if (_dt.Rows.Count > 0)
                    {
                        DataRow _dr = _dt.Rows[0];
                        txtProductEdit.Text = _dr["ItemDescription"].ToString();
                    }
                    //bind custom columns
                    ph_batchcolumns.Controls.Clear();
                    BindBatchPlaceholderFields();
                    //Bind area dropdown
                    BindAreaInNewItem(sessionKeys.PortfolioID);
                    //Add panel visibility
                    Add_Panel_Inventory(false);

                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
            if (e.CommandName == "Usage")
            {
                lblProductidInPOPUP.Text = e.CommandArgument.ToString();
                pUsageCustomer.Controls.Clear();
                BindUsagePlaceholderFields();
                ddlStatus.DataSource = InventoryStatus();
                ddlStatus.DataTextField = "text";
                ddlStatus.DataValueField = "value";
                ddlStatus.DataBind();
                Bindproject();
                txtQtyUsed.Text = string.Empty;
                TxtRequester.Text = string.Empty;
                //   ddlProject.SelectedValue = "0";
                ddlStatus.SelectedValue = "0";
                txtNote.Text = string.Empty;
                Btninsert.Visible = true;
                btnupdate.Visible = false;
                //pUsageCustomer
                //BindUsagePlaceholderFields();
                mdlpopupinGrid.Show();
                Label6.Text = "Usage";
                hUsage.Value = e.CommandArgument.ToString();
                //bind batch data
                BindBatchDropdown(Convert.ToInt32(hUsage.Value));

                BindCondtions();
                BindFilteredBatchGrid(e.CommandArgument.ToString());
                //hide fields
                HideFields();

            }
            if (e.CommandName == "Update")
            {
                CleargrdInventryFields();
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdInventory.EditIndex;
                GridViewRow Row = grdInventory.Rows[i];
                //int catID = Convert.ToInt32(ddlCategory.SelectedItem.ToString());
                int subcatID = Convert.ToInt32(ddlSubCategory.SelectedValue.ToString());
                //Check site is selected or not
                //if (ddlSite.SelectedIndex != 0)
                //{
                //int SiteId = Convert.ToInt32(ddlSite.SelectedValue.ToString());
                int Qty = Convert.ToInt32(((TextBox)Row.FindControl("txtQtyOnOrder")).Text);
                //int ReOrderLevel = Convert.ToInt32(((TextBox)Row.FindControl("txtReOrderLevel")).Text);
                //int ProductID = Convert.ToInt32(string.IsNullOrEmpty(ddlProduct.SelectedValue.ToString())? Convert.ToInt32  );

                //Convert.ToInt32((string.IsNullOrEmpty(txtLength.Text)) ? "0" : txtLength.Text);
                // need to find whether the product is already exists in Inventory Table or not
                IMCS = new InventoryManagerCs();
                int retVal = -1;
                retVal = IMCS.UpdateQOO(ID, Qty);

                if (retVal > 0)
                {
                    GridInventoryBinding();
                }
            }
            if (e.CommandName == "Transfer")
            {
                lblTransferMsg.Text = string.Empty;
                int id = int.Parse(e.CommandArgument.ToString());
                hfInventoryId.Value = id.ToString();
                mdlPopupTransferItem.Show();
                sessionKeys.PortfolioID = int.Parse(ddlCustomer.SelectedValue);
                BindTransferItems();
                BindInventoryDetails();
                txtTransferNotes.Text = string.Empty;
                txtAmendment.Text = string.Empty;
                ccdtransferSite.SelectedValue = "0";

            }
            if (e.CommandName == "History")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                mdlpopupHisstory.Show();
                BindHistory(id);
            }
            if (e.CommandName == "MoreOptions")
            {
                try
                {
                    string pid = e.CommandArgument.ToString();

                    // BindStorageGrid();
                    //ViewState["inventoryProductId"] = e.CommandArgument.ToString();
                    InvenotryUpdatePanel(pid);
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                try
                {
                    IMCS = new InventoryManagerCs();
                    int retVal = -1;
                    retVal = IMCS.DeletePrdouct(ID);
                    InventoryBAL.DeleteInventorySiteItems(ID);
                    InventoryBAL.DeleteInventoryJournal(ID);


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void InvenotryUpdatePanel(string productid)
    {
        BindProductsSection(productid);
        BindPlaceholderFields(int.Parse(ddlCustomer.SelectedValue));
        SettingDataToPlaceHolder(int.Parse(ddlProduct.SelectedValue), int.Parse(ddlCustomer.SelectedValue));
        //bind usage grid
        BindData_UsageGrid();
        //bind batch grid
        BindBatchGrid();
        //Bindlistview();
        //    PaneltblDeployedDate.Visible = true;
        PnlNewItem.Visible = true;
        PnlListview.Visible = true;
        // HideUseGrid();
        BindPopUpGridItems(int.Parse(ddlProduct.SelectedValue));
        BindSiteInPopup();
        BindAreaInPopup(int.Parse(ddlCustomer.SelectedValue));
        lblproductName.Text = ddlProduct.SelectedItem.ToString();
        //GridInventoryBinding();
        pnlAddProduct.Visible = true;
        VisibilityChecking(int.Parse(ddlCustomer.SelectedValue));
        UpdatePanel2.Update();

        pnltab_subheader.Visible = false;
    }
    private void BindInventoryDetails()
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var inventory = db.InventoryManagers.Where(i => i.Id == int.Parse(hfInventoryId.Value)).Select(i => i).FirstOrDefault();
            if (inventory != null)
            {
                //GetQtyAvailable.....
                lblCurrentQty.Text = getActualStackLaevel(inventory.Id.ToString()).ToString();
                lblDate.Text = DateTime.Now.ToShortDateString();

                hfCategoryId.Value = inventory.Categoery.ToString();
                hfDescription.Value = inventory.ItemDescription;
                hfSubcategoryId.Value = inventory.SubCategory.ToString();
                hfSiteID.Value = inventory.SiteId.ToString();
                hfPartNumber.Value = inventory.PartNumber;
                hfBarcode.Value = inventory.Barcode;
            }
        }
    }
    private void BindManufacurer()
    {
        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlManufacturer.DataSource = SC_cntMaterialCS.SelectAllManufacturer();
        ddlManufacturer.DataTextField = "Name";
        ddlManufacturer.DataValueField = "id";
        ddlManufacturer.DataBind();
        ddlManufacturer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        ddlManufacturer.SelectedIndex = 0;
    }
    protected void btnManufacturer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int retVal = -1;
            if ((txtManufacturer.Text.ToLower() != "please select...") && (txtManufacturer.Text != string.Empty))
            {

                //txtManufacturer
                SC_cntMaterialCS = new SC_ContractorMaterial();
                retVal = SC_cntMaterialCS.InsertManufacturer(txtManufacturer.Text.Trim());
                BindManufacurer();
                //}
            }
            if (retVal > 0)
            {
            }
            else
            {
                lblError.Text = "Manufacturer already exists";
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public bool checkManufacturer(string Manufacturer)
    {
        bool re = false;
        //SqlCommand comm_chk = new SqlCommand(string.Format("select count(*) from ContractorMaterialCatalogue where PortfolioID = {0} and ItemDescription='{1}' and ProjectReference = 0 and ItemDelete = 0 and Category ={2} and SubCategory={3} ", PortfolioID, Material, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue), conn);
        SqlCommand comm_chk = new SqlCommand(string.Format("select count(*) from Manufacturer where Name = {0}", Manufacturer), conn);
        conn.Open();
        int c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
        if (c == 0)
        {
            re = true;
        }
        conn.Close();
        return re;
    }
    public bool ChkMaterialDesc(int PortfolioID, string Material, int Category, int SubCategory)
    {
        object retObj = 0;
        bool re = false;
        IMCS = new InventoryManagerCs();
        retObj = IMCS.CheckProductDescExist(PortfolioID, Material, Category, SubCategory);
        //SC_cntMaterialCS = new SC_ContractorMaterial();
        //retObj = SC_cntMaterialCS.ChkCMaterialDescExist(PortfolioID, Material, Category, SubCategory);
        if ((int)retObj == 0)
        {
            re = true;
        }
        return re;
    }
    private int GetCatagoryID()
    {
        int retval = 0;
        try
        {
            if (ddlCategory.Visible == true)
            {
                retval = int.Parse(ddlCategory.SelectedValue);
            }
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    private int GetSubCatagoryID()
    {
        int retval = 0;
        try
        {
            if (ddlSubCategory.Visible == true)
            {
                retval = int.Parse(ddlSubCategory.SelectedValue);
            }
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    protected void BindUseGrid(int siteID, int inventoryID)
    {
        //using (InventoryDataContext db = new InventoryDataContext())
        //{
        //    var list = db.Inventory_SiteStorageDetails.Where(s => s.SiteId == siteID && s.InventoryId == inventoryID).Select(s => s).ToList();
        //    GvUse.DataSource = list;
        //    GvUse.DataBind();
        //}
        using (InventoryDataContext db = new InventoryDataContext())
        {

            var usageList = from i in db.Inventory_SiteStorageDetails
                            join u in db.Inventory_Uses on i.UseID equals u.ID
                            where i.SiteId == siteID && i.InventoryId == inventoryID
                            group u by u.Name + "(" + u.Code + ")" into grouping
                            select new
                            {
                                Use = grouping.Key,
                                NumberDeployed = grouping.Count()
                            };


            gvUsageSummary.DataSource = usageList;
            gvUsageSummary.DataBind();
        }
    }

    private void AddInventorySiteStorageDetails(int inventoryID, int categoryID, int subCategoryID, int siteID, int qty)
    {
        try
        {
            if (qty >= 0)
            {
                using (InventoryDataContext db = new InventoryDataContext())
                {
                    List<Inventory_SiteStorageDetail> iList = new List<Inventory_SiteStorageDetail>();//for insert operation

                    var currentStorageList = db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == inventoryID).OrderBy(i => i.UseStatus).Select(i => i).ToList();
                    int count = currentStorageList.Count();
                    if (count == 0)
                    {
                        for (int i = 0; i < qty; i++)
                        {
                            Inventory_SiteStorageDetail isd = new Inventory_SiteStorageDetail();
                            isd.InventoryId = inventoryID;
                            isd.CategoryID = categoryID;
                            isd.SubCategoryID = subCategoryID;
                            isd.SiteId = siteID;
                            isd.UseID = 0;
                            isd.UseStatus = false;
                            iList.Add(isd);
                        }
                    }
                    else
                    {
                        if (qty > count)
                        {
                            for (int i = 1; i <= qty - count; i++)
                            {
                                Inventory_SiteStorageDetail isd = new Inventory_SiteStorageDetail();
                                isd.InventoryId = inventoryID;
                                isd.CategoryID = categoryID;
                                isd.SubCategoryID = subCategoryID;
                                isd.SiteId = siteID;
                                isd.UseID = 0;
                                isd.UseStatus = false;
                                iList.Add(isd);
                            }
                        }
                        if (qty < count)
                        {
                            //delete operation
                            int deleteCount = count - qty;
                            var list = currentStorageList.Take(deleteCount).ToList();
                            db.Inventory_SiteStorageDetails.DeleteAllOnSubmit(list);
                            db.SubmitChanges();

                        }
                    }


                    //bulk insert
                    db.Inventory_SiteStorageDetails.InsertAllOnSubmit(iList);
                    db.SubmitChanges();
                }
            }

        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveMaterial_Click(object sender, ImageClickEventArgs e)
    {
        // need to add product in Inventorymanager
        if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
                {
                    bool saved = false;
                    PortfolioID = int.Parse(ddlCustomer.SelectedValue);
                    //if (checkMaterial(txtItemDesc.Text) == true)
                    if (ChkMaterialDesc(PortfolioID, txtItemDesc.Text, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue)) == true)
                    {
                        try
                        {
                            int i = 0;
                            //To save Material details
                            if ((pnlAddProduct.Visible == true) && (!saved))
                            {
                                //Check validations
                                string supplier = "";
                                if (ddlSupplier.Visible == true)
                                {
                                    supplier = ddlSupplier.SelectedItem.Text.ToString();
                                }
                                else if (txtSupplier.Visible == true)
                                {
                                    supplier = txtSupplier.Text;
                                }
                                if (!string.IsNullOrEmpty(supplier))
                                {
                                    Guid _guid = Guid.NewGuid();
                                    int SiteId = int.Parse(ddlSite.SelectedValue);
                                    int ProjectReferece = 0;
                                    int ContractorID = sessionKeys.UID;
                                    string ItemDescription = txtItemDesc.Text.Trim();
                                    int Supplier = int.Parse(ddlSupplier.SelectedValue);
                                    string PartNumber = txtPartNumber.Text.Trim();
                                    string barcode = txtBarcode.Text;
                                    string notes = txtNotes.Text.Trim();
                                    int Area = int.Parse(ddlarea.SelectedValue);
                                    int Location = int.Parse(ddllocation.SelectedValue);
                                    int Shelf = int.Parse(ddlshelf.SelectedValue);
                                    int bin = int.Parse(ddlbin.SelectedValue);
                                    //string UnitPrice = txtUnit.Text.Trim();
                                    //double BuyingPrice = Convert.ToDouble((string.Empty == txtMBuyingPrice.Text.Trim() ? "0" : txtMBuyingPrice.Text.Trim()));
                                    //double SellingPrice = Convert.ToDouble((string.Empty == txtMSellingPrice.Text.Trim() ? "0" : txtMSellingPrice.Text.Trim()));
                                    int QTY = Convert.ToInt32((string.Empty == txtStockLevel.Text.Trim() ? "0" : txtStockLevel.Text.Trim()));
                                    int ReorderLevel = Convert.ToInt32((string.Empty == txtReorderlevel.Text.Trim() ? "0" : txtReorderlevel.Text.Trim()));
                                    Guid Image1;
                                    if (FileUploadMaterial.HasFile)
                                    {
                                        Image1 = _guid;
                                    }
                                    else
                                    {
                                        Image1 = Guid.Empty;
                                    }

                                    int Category = GetCatagoryID();
                                    int SubCategory = GetSubCatagoryID();
                                    int Type = 0;
                                    int QtyOnOrder = Convert.ToInt32((string.IsNullOrEmpty(txtQntyOrder.Text)) ? "0" : txtQntyOrder.Text);
                                    DateTime ExpctArvlDate = Convert.ToDateTime(string.IsNullOrEmpty(txtArrivalDate.Text.Trim()) ? "01/01/1900" : txtArrivalDate.Text.Trim());
                                    int Manufacturer = Convert.ToInt32(ddlManufacturer.SelectedValue); ;
                                    int LeadTime = Convert.ToInt32((string.IsNullOrEmpty(txtLeadTime.Text)) ? "0" : txtLeadTime.Text);
                                    int Replenish = QtyOnOrder;

                                    IMCS = new InventoryManagerCs();
                                    DateTime DateOrdered = Convert.ToDateTime(string.IsNullOrEmpty(txtDateOrdered.Text.Trim()) ? "01/01/1900" : txtDateOrdered.Text.Trim()); //Convert.ToDateTime(txtDateOrdered.Text.Trim());

                                    int inventoryID = 0;
                                    using (InventoryDataContext db = new InventoryDataContext())
                                    {

                                        inventoryID = db.InventoryManager_Insert(SiteId, 0, QTY, ReorderLevel, "Global", Category, SubCategory,
                                          PortfolioID, QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, Supplier, PartNumber,
                                          Image1, Manufacturer, DateOrdered, barcode, notes, Area, Location, Shelf, bin);
                                        //retval = IMCS.InsertInventoryProduct(SiteId, 0, QTY, ReorderLevel, "Global", Category, SubCategory,
                                        //    PortfolioID, QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, PartNumber, Image1, Supplier, Manufacturer, DateOrdered,barcode);


                                        if (inventoryID > 0)
                                        {

                                            InventoryMgt.Entity.InventoryManager inventoryManager = db.InventoryManagers.Where(m => m.Id == inventoryID).Select(m => m).FirstOrDefault();
                                            if (inventoryManager != null)
                                            {
                                                //Product items insert
                                                AddInventorySiteStorageDetails(inventoryID, Category, SubCategory, SiteId, QTY);
                                                //Inventory Journal Insert
                                                InventoryBAL.InsertInventoryJournal(inventoryManager, 0, "", 0, Convert.ToInt32(inventoryManager.Qty), 0);
                                            }
                                        }
                                    }

                                    //AddInventorySiteStorageDetails(

                                    if (FileUploadMaterial.HasFile)
                                    {
                                        ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                                    }
                                    //if (retval > 0)
                                    //{
                                    GridInventoryBinding();
                                    BindProduct();
                                    //need to call clear fileds method
                                    CleargrdInventryFields();
                                    pnlAddItems.Visible = false;
                                    //place holder data
                                    SavePlaceholderData(int.Parse(ddlProduct.SelectedValue), int.Parse(ddlCustomer.SelectedValue));
                                    lblMsg.Text = "Item added successfully.";
                                    Response.Redirect(Page.Request.RawUrl);
                                }
                                else
                                {
                                    lblError.Visible = true;
                                    lblError.Text = "Please select/enter supplier";
                                }

                            }
                            if (saved)
                            {
                                //fillGrid(int.Parse(ddlSelect.SelectedValue));
                                //AddNewItem(int.Parse(ddlSelect.SelectedValue));
                            }
                        }
                        catch (Exception ex)
                        {
                            //call the error page if any error occurs
                            LogExceptions.LogException(ex.Message);
                            //
                        }
                        finally
                        {
                            //Check the connection state to close
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                    }

                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Material description already exists";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please select Site";
                }
            }

            else
            {
                lblError.Visible = true;
                lblError.Text = "Please select Subcategory";
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Please select category";
        }



    }

    private void CleargrdInventryFields()
    {
        txtStock.Text = string.Empty;
        txtItemDesc.Text = string.Empty;
        ddlSupplier.SelectedIndex = 0;
        txtPartNumber.Text = string.Empty;
        txtStockLevel.Text = string.Empty;
        txtReorderlevel.Text = string.Empty;
        txtQntyOrder.Text = string.Empty;
        ddlManufacturer.SelectedIndex = 0;
        txtArrivalDate.Text = string.Empty;
        txtLeadTime.Text = string.Empty;
        txtQtyInUse.Text = string.Empty;
        txtBarcode.Text = string.Empty;
        //FileUploadMaterial.Dispose();
        imgbtnUpdateMaterial.Visible = false;
        txtDateOrdered.Text = string.Empty;
        btnSaveMaterial.Visible = true;
    }
    public string DisPlayName(string DeafaultName)
    {
        GFC = new GridFieldConfigurator();
        IGFC = new InventoryRepository<GridFieldConfigurator>();
        string Dname = string.Empty;
        try
        {
            if (ddlCustomer.SelectedValue != "0")
            {
                Dname = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlCustomer.SelectedValue) && o.DeafaultName == DeafaultName).Select(o => o.DisplayName).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Dname;
    }
    protected void btnCancelrow_Click(object sender, ImageClickEventArgs e)
    {
        CleargrdInventryFields();
        pnlAddItems.Visible = false;
        pnlUse.Visible = false;
        PnlListview.Visible = false;
        Response.Redirect(Page.Request.RawUrl);
    }
    protected void btnAddSupplier_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void btnCancelSupplier_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void imgbtnUpdateMaterial_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SavePlaceholderData(int.Parse(ddlProduct.SelectedValue), int.Parse(ddlCustomer.SelectedValue));
            IMCS = new InventoryManagerCs();
            int ID = Convert.ToInt32(hdnItemID.Value);
            Guid _guid = new Guid(hdnImageID.Value);
            if ((_guid == Guid.Empty) && (FileUploadMaterial.HasFile))
            {
                _guid = Guid.NewGuid();
            }
            IMCS = new InventoryManagerCs();
            DataTable _dt = IMCS.SelectProductByPID(ID);
            if (_dt.Rows.Count > 0)
            {
                string ItemDescription;
                int Supplier;
                string PartNumber;
                string barcode;
                int Qty; //stocklevel
                int ReorderLevel;
                int QtyOnOrder;
                int Manufacturer;
                DateTime ExpctArvlDate;
                int LeadTime;
                Guid Image;
                DataRow _dr = _dt.Rows[0];
                ItemDescription = txtItemDesc.Text.Trim();
                string notes = txtNotes.Text.Trim();
                int Category = int.Parse(_dr["Categoery"].ToString());
                int currentQty = int.Parse(_dr["Qty"].ToString());
                int SubCategory = int.Parse(_dr["SubCategory"].ToString());
                PortfolioID = int.Parse(ddlCustomer.SelectedValue);
                bool saved = false;
                if (ChkMaterialDesc(PortfolioID, txtItemDesc.Text, Category, SubCategory) == false)
                {
                    if ((pnlAddProduct.Visible == true) && (!saved))
                    {
                        string supplier = "";
                        if (ddlSupplier.Visible == true)
                        {
                            supplier = ddlSupplier.SelectedItem.Text.ToString();
                        }
                        else if (txtSupplier.Visible == true)
                        {
                            supplier = txtSupplier.Text;
                        }
                        if (!string.IsNullOrEmpty(supplier))
                        {
                            int SiteId = int.Parse(_dr["SiteId"].ToString());
                            Supplier = int.Parse(ddlSupplier.SelectedValue);
                            PartNumber = txtPartNumber.Text.Trim();
                            barcode = txtBarcode.Text.Trim();
                            Qty = Convert.ToInt32((string.Empty == txtStockLevel.Text.Trim() ? "0" : txtStockLevel.Text.Trim()));
                            ReorderLevel = Convert.ToInt32((string.Empty == txtReorderlevel.Text.Trim() ? "0" : txtReorderlevel.Text.Trim()));
                            Guid Image1;
                            if (FileUploadMaterial.HasFile)
                            {
                                Image1 = _guid;
                            }
                            else
                            {
                                string Imagess = _dr["Image"].ToString();
                                Image1 = new Guid(Imagess); //Guid.Empty;
                            }

                            int Type = 0;
                            QtyOnOrder = Convert.ToInt32((string.IsNullOrEmpty(txtQntyOrder.Text)) ? "0" : txtQntyOrder.Text);
                            ExpctArvlDate = Convert.ToDateTime(string.IsNullOrEmpty(txtArrivalDate.Text.Trim()) ? "01/01/1900" : txtArrivalDate.Text.Trim());
                            Manufacturer = Convert.ToInt32(ddlManufacturer.SelectedValue); ;
                            LeadTime = Convert.ToInt32((string.IsNullOrEmpty(txtLeadTime.Text)) ? "0" : txtLeadTime.Text);
                            DateTime DateOrdered = Convert.ToDateTime(string.IsNullOrEmpty(txtDateOrdered.Text.Trim()) ? "01/01/1900" : txtDateOrdered.Text.Trim());// Convert.ToDateTime(txtDateOrdered.Text.Trim());
                            int retval = -1;

                            //retval = IMCS.UpdateInventoryProduct(ID, SiteId, 0, Qty, ReorderLevel, "Global", Category, SubCategory,
                            //    PortfolioID, QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, PartNumber, Image1, Supplier, Manufacturer, DateOrdered, barcode);
                            using (InventoryDataContext db = new InventoryDataContext())
                            {
                                int transferQty = 0;
                                bool detailsChanged = InventoryDetailsChange(ID, Qty);
                                if (detailsChanged)
                                {
                                    transferQty = GetTransferQty(ID, Qty);
                                }
                                retval = db.InventoryManager_Update(ID, SiteId, Qty, ReorderLevel, "Global", Category, SubCategory,
                                PortfolioID, QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, Image1,
                                Supplier, Manufacturer, DateOrdered, barcode, notes, int.Parse(ddlarea.SelectedValue),
                                int.Parse(ddllocation.SelectedValue), int.Parse(ddlshelf.SelectedValue), 
                                int.Parse(ddlbin.SelectedValue),txtPartNumber.Text);
                                InventoryMgt.Entity.InventoryManager inventoryManager = db.InventoryManagers.Where(m => m.Id == ID).Select(m => m).FirstOrDefault();
                                if (inventoryManager != null)
                                {

                                    int openingStock = GetQtyAvailable(inventoryManager.Id.ToString());
                                    //Product Item insert
                                    AddInventorySiteStorageDetails(ID, Category, SubCategory, SiteId, Qty);
                                    //Inventory Journal Insert
                                    if (detailsChanged)
                                    {

                                        inventoryManager.Qty = GetQtyAvailable(inventoryManager.Id.ToString());
                                        InventoryBAL.InsertInventoryJournal(inventoryManager, 0, "Adjustment", transferQty, openingStock, 0);
                                    }
                                }
                            }

                            if (FileUploadMaterial.HasFile)
                            {
                                ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                            }
                            if (retval >= 0)
                            {
                                //GridInventoryBinding();
                                BindProduct();
                                pnlUse.Visible = false;
                                //need to call clear fileds method
                                CleargrdInventryFields();
                                pnlAddItems.Visible = false;
                                PnlListview.Visible = false;

                                //Response.Redirect(Page.Request.RawUrl);
                                InvenotryUpdatePanel(hdnItemID.Value);
                                lblMessage.Text = "Updated successfully.";
                            }
                        }
                    }
                }


            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private int GetTransferQty(int inventoryID, int currentQty)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            int transferQty = 0;
            InventoryMgt.Entity.InventoryManager inventoryManager = db.InventoryManagers.Where(m => m.Id == inventoryID).Select(m => m).FirstOrDefault();
            if (inventoryManager != null)
            {
                int previousQty = Convert.ToInt32(inventoryManager.Qty);
                transferQty = currentQty - previousQty;

            }
            return transferQty;
        }

    }

    private bool InventoryDetailsChange(int id, int qty)
    {
        bool changed = false;
        using (InventoryDataContext db = new InventoryDataContext())
        {
            InventoryMgt.Entity.InventoryManager inventoryManager = db.InventoryManagers.Where(i => i.Id == id).Select(i => i).FirstOrDefault();
            if (inventoryManager != null)
            {
                if (inventoryManager.Qty != qty)
                    changed = true;

            }
        }
        return changed;
    }

    public string FormateDate(string value)
    {
        string mydate = "";
        try
        {
            if (value == "01/01/1900")
            {
                mydate = "";
            }
            else
            {
                mydate = value;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return mydate;
    }
    protected void grdInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PanelHide();
        InventoryGridBind();
        BindProduct();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GridInventoryBinding();
        BindProduct();
        PanelHide();
        PnlListview.Visible = false;
        pnlAddItems.Visible = true;
    }
    private void panelVisibleCategory(bool ddl, bool txt)
    {
        pnladdcategory.Visible = txt;
        pnlcategory.Visible = ddl;
        if (pnlcategory.Visible == true)
        {
            //btnAddnew.Visible = true;
        }
        else
        {
            //btnAddnew.Visible = false;
        }

    }

    #region Category - Sub Category
    protected void btnaddcategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCustomer.SelectedItem.Value != "0")
            {
                HID_Category.Value = string.Empty;
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
    private void panelVisibleSubCategory(bool ddl, bool txt)
    {
        pnladdsubcategory.Visible = txt;
        pnlsubcategory.Visible = ddl;

        if (pnlsubcategory.Visible == true)
        {
            // btnAddnew.Visible = true;
            // need to write code here
            //if(ddlSelect.va
        }
        else
        {
            //btnAddnew.Visible = false;
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
    protected void btnDeleteCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "0")
                ServiceCatalogManager.DeleteCategory(int.Parse(ddlCategory.SelectedValue));

            BindCategory(int.Parse(ddlCustomer.SelectedValue));
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString()));
            GridInventoryBinding();
            HID_Category.Value = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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

            GridInventoryBinding();
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
    #endregion

    protected void imgbtnViewInven_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            idc = new InventoryDataContext();
            IMCS = new InventoryManagerCs();
            pnlInventory.Visible = true;
            int cust = Convert.ToInt32(ddlCustomer.SelectedValue.ToString());
            //int cat = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            //int sub = Convert.ToInt32(ddlSubCategory.SelectedValue.ToString());
            //int prod = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
            //int site = Convert.ToInt32(ddlSite.SelectedValue.ToString());
            //grdInventory.DataSource = IMCS.SelectProducts(cust, 0, 0, 0, "Global");
            //  grdInventory.DataSource = idc.IM_GetProduct(cust, 0, 0, 0, "Global");
            //grdInventory.DataBind();
            //grdInventory.Columns[0].Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //}

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        try
        {
            Label7.Text = "Add Inventory";
            // mdlpopupinGrid.Hide();
            mdlpopupForNewItem.Show();
            //clear the data
            hid_batchproduct.Value = "0";
            txtProductEdit.Text = string.Empty;
            txtQtyEdit.Text = string.Empty;
            if (sessionKeys.PortfolioID > 0)
            {
                ddlcustomerInNewItem.SelectedValue = sessionKeys.PortfolioID.ToString();
                BindSiteInNewItem();
                ddlsiteInNewItem.SelectedValue = ddlsiteInSearch.SelectedValue.ToString();

                BindCategoryInNewItem(sessionKeys.PortfolioID);
                BindSubCategoryInNewItem(int.Parse(ddlcategoryInNewItem.SelectedValue));
                txtBatchreference.Text = GetBatchReference(0).ToString();
                //bind custom columns
                ph_batchcolumns.Controls.Clear();
                BindBatchPlaceholderFields();
                //Add panel visibility
                Add_Panel_Inventory(true);

                BindAreaInNewItem(int.Parse(sessionKeys.PortfolioID.ToString()));
            }
            else
            {
                ddlareaInNewItem.Items.Clear();
                ddlareaInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
                ddlLocationInNewItem.Items.Clear();
                ddlLocationInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
                ddlShelfInNewItem.Items.Clear();
                ddlShelfInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
                ddlBinInNewItem.Items.Clear();
                ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        // pnlUse.Visible = true;
    }

    protected void ddlProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        // GridInventoryBinding();
        // PanelHide();
        // PnlListview.Visible = false;
    }

    #region Inventory Upload For CustomerLevel
    public string[] GetExcelSheetNames(string connectionString)
    {
        OleDbConnection con = null; DataTable dt = null;
        String conStr = connectionString;
        con = new OleDbConnection(conStr);
        con.Close();
        con.Open();
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dt == null)
        {
            return null;
        }
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }
        con.Close();
        return excelSheetNames;
    }
    private DataTable Import_To_Grid(string conStr)
    {
        string[] sheetnames = GetExcelSheetNames(conStr);
        string sheetname = string.Empty;
        if (sheetnames.Length > 0)
            sheetname = sheetnames[0];

        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //string SheetName = "DealerVoice$";
        cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", sheetname);
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        return dt;

    }



    // With popup
    private void InventoryUpload(string type, string path, string fileName, string conStr)
    {
        try
        {

            lblCustomerSuccessMsg.Text = string.Empty;
            lblCustomerErrorMsg.Text = string.Empty;
            int customer = int.Parse(ddlCustomer.SelectedValue);
            int siteId = int.Parse(ddlSite.SelectedValue);
            string useCode = string.Empty;
            List<int> list = new List<int>();
            int useId = 0;
            bool error = false;

            using (InventoryDataContext db = new InventoryDataContext())
            {

                var inventoryList = (from i in db.InventoryManagers
                                     join c in db.ServiceCatalog_categories on i.Categoery equals c.ID
                                     join sc in db.ServiceCatalog_categories on i.SubCategory equals sc.ID
                                     join s in db.Sites on i.SiteId equals s.ID
                                     where i.PortfolioID == customer && i.SiteId == siteId
                                     orderby c.CategoryName, i.Id
                                     select new { i.Id, i.Qty, i.QtyOnOrder, i.Categoery, i.Barcode, i.SubCategory, i.SiteId, i.ExpctArvlDate, i.Image, i.ItemDescription, i.Notes, CategoryName = c.CategoryName, SubCategoryName = sc.CategoryName, SiteName = s.Site1 }).ToList();
                //db.InventoryManagers.Where(i => i.PortfolioID == customer && i.SiteId == siteId).Select(i => i).ToList();
                if (inventoryList.Count() > 0)
                {
                    var useList = db.Inventory_Uses.Select(u => u).ToList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("UseCode", typeof(string));
                    dt.Columns.Add("UseID", typeof(int));
                    dt.Columns.Add("BarCode", typeof(string));
                    if (type == "CSV")
                    {
                        var reader = new StreamReader(File.OpenRead(path + fileName));
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            var use = useList.Where(u => u.Code == values[0]).Select(u => u).FirstOrDefault();
                            if (use != null)
                            {
                                useCode = use.Code;
                                useId = use.ID;
                            }
                            else
                            {
                                dt.Rows.Add(useCode, useId, values[0]);
                            }

                        }
                        reader.Close();
                    }
                    else
                    {
                        // from Excel file
                        DataTable dtable = Import_To_Grid(conStr);
                        for (int y = 0; y < dtable.Rows.Count; y++)
                        {
                            string code = dtable.Rows[y][0].ToString();
                            var use = useList.Where(u => u.Code == code).Select(u => u).FirstOrDefault();
                            if (use != null)
                            {
                                useCode = use.Code;
                                useId = use.ID;
                            }
                            else
                            {
                                dt.Rows.Add(useCode, useId, code.ToString());
                            }
                        }
                    }
                    var result = from c in dt.AsEnumerable().ToList()
                                 group c by new { BarCode = c.Field<string>("BarCode"), UseCode = c.Field<string>("UseCode"), UseID = c.Field<int>("UseID") } into g
                                 select new
                                 {
                                     Barcode = g.Key.BarCode,
                                     Use = g.Key.UseCode,
                                     UseID = g.Key.UseID,
                                     ItemCount = g.Count()
                                 };

                    List<InventoryUploadList> upl = new List<InventoryUploadList>();
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Exception Report");
                    int x = 2;
                    // Title
                    ws.Cell("A1").Value = "Use Code";
                    ws.Cell("B1").Value = "Barcode";
                    ws.Cell("C1").Value = "Category";
                    ws.Cell("D1").Value = "Sub Category";
                    ws.Cell("E1").Value = "Item Description";
                    ws.Cell("F1").Value = "Site";
                    ws.Cell("G1").Value = "Result";



                    foreach (var item in result)
                    {
                        var iList = (from i in inventoryList
                                     join s in db.Inventory_SiteStorageDetails on i.Id equals s.InventoryId
                                     where i.Barcode == item.Barcode
                                     select new { i, s }).ToList();
                        var barcodeExists = (from b in iList
                                             where b.i.Barcode == item.Barcode.ToString()
                                             group b by new { Product = b.i.ItemDescription, ID = b.s.InventoryId, Category = b.i.CategoryName, SubCategory = b.i.SubCategoryName, Site = b.i.SiteName } into grouping
                                             select new { Product = grouping.Key.Product, QtyOpen = grouping.Count(), QtyDeployed = item.ItemCount, ID = grouping.Key.ID, Use = item.Use, UseID = item.UseID, Category = grouping.Key.Category, SubCategory = grouping.Key.SubCategory, Site = grouping.Key.Site }).FirstOrDefault();
                        var itemExists = (from b in iList
                                          where b.i.Barcode == item.Barcode.ToString() && b.s.UseStatus == false
                                          group b by new { Product = b.i.ItemDescription, ID = b.s.InventoryId, Category = b.i.CategoryName, SubCategory = b.i.SubCategoryName, Site = b.i.SiteName } into grouping
                                          select new { Product = grouping.Key.Product, QtyOpen = grouping.Count(), QtyDeployed = item.ItemCount, ID = grouping.Key.ID, Use = item.Use, UseID = item.UseID, Category = grouping.Key.Category, SubCategory = grouping.Key.SubCategory, Site = grouping.Key.Site }).FirstOrDefault();


                        //var result1 = (from i in inventoryList
                        //               join s in db.Inventory_SiteStorageDetails on i.Id equals s.InventoryId
                        //               where i.Barcode == item.Barcode.ToString() && s.UseStatus==false
                        //               group i by new { Product = i.ItemDescription, ID=s.InventoryId} into grouping
                        //               select new { Product = grouping.Key.Product, QtyOpen = grouping.Count(), QtyDeployed = item.ItemCount,ID=grouping.Key.ID,Use=item.Use,UseID=item.UseID }).FirstOrDefault();

                        if (barcodeExists != null && itemExists != null)
                        {
                            int qty = 0;
                            if (upl.Count() > 0)
                            {
                                qty = upl.Where(p => p.InventoryID == itemExists.ID).Select(p => Convert.ToInt32(p.NumberDeployed)).Sum();
                            }

                            int openQty = (itemExists.QtyOpen - qty) < 0 ? 0 : (itemExists.QtyOpen - qty);
                            int qtyDeployed = itemExists.QtyDeployed > openQty ? openQty : itemExists.QtyDeployed;

                            if (openQty != 0 && useCode != string.Empty)
                            {
                                InventoryUploadList up = new InventoryUploadList();
                                up.InventoryID = Convert.ToInt32(itemExists.ID);
                                up.Product = itemExists.Product;
                                up.OpeningQty = openQty;
                                up.NumberDeployed = qtyDeployed;
                                up.Use = itemExists.Use.ToString();
                                up.UseID = itemExists.UseID;
                                up.Category = itemExists.Category;
                                up.SubCategory = itemExists.SubCategory;
                                upl.Add(up);
                            }
                            if (useCode == string.Empty)
                            {
                                error = true;
                                for (int i = 0; i < item.ItemCount; i++)
                                {
                                    ws.Cell("A" + x.ToString()).Value = item.Use;
                                    ws.Cell("A" + x.ToString()).Style.Font.Bold = true;
                                    ws.Cell("B" + x.ToString()).Value = item.Barcode;
                                    ws.Cell("B" + x.ToString()).Style.Font.Bold = true;
                                    ws.Cell("C" + x.ToString()).Value = "";
                                    ws.Cell("D" + x.ToString()).Value = "";
                                    ws.Cell("E" + x.ToString()).Value = "";
                                    ws.Cell("F" + x.ToString()).Value = "";
                                    ws.Cell("G" + x.ToString()).Value = "Unsuccessful: Reason - Barcode does not exist";
                                    ws.Cell("G" + x.ToString()).Style.Font.FontColor = XLColor.Red;
                                    x++;
                                }
                            }
                            //over flow qty
                            if (itemExists.QtyDeployed > openQty)
                            {
                                int overFlow = itemExists.QtyDeployed - qtyDeployed;
                                error = true;
                                for (int i = 0; i < overFlow; i++)
                                {
                                    ws.Cell("A" + x.ToString()).Value = item.Use;
                                    ws.Cell("A" + x.ToString()).Style.Font.Bold = true;
                                    ws.Cell("B" + x.ToString()).Value = item.Barcode;
                                    ws.Cell("B" + x.ToString()).Style.Font.Bold = true;
                                    ws.Cell("C" + x.ToString()).Value = barcodeExists.Category;
                                    ws.Cell("D" + x.ToString()).Value = barcodeExists.SubCategory;
                                    ws.Cell("E" + x.ToString()).Value = barcodeExists.Product;
                                    ws.Cell("E" + x.ToString()).Style.Font.Bold = true;
                                    ws.Cell("F" + x.ToString()).Value = barcodeExists.Site;
                                    ws.Cell("G" + x.ToString()).Value = "Unsuccessful: Reason - The site has insufficent stock to allocate this item. Please check the Inventory Module";
                                    ws.Cell("G" + x.ToString()).Style.Font.FontColor = XLColor.Red;
                                    x++;
                                }
                            }
                        }
                        else if (barcodeExists != null && itemExists == null)
                        {
                            error = true;
                            for (int i = 0; i < item.ItemCount; i++)
                            {
                                ws.Cell("A" + x.ToString()).Value = item.Use;
                                ws.Cell("A" + x.ToString()).Style.Font.Bold = true;
                                ws.Cell("B" + x.ToString()).Value = item.Barcode;
                                ws.Cell("B" + x.ToString()).Style.Font.Bold = true;
                                ws.Cell("C" + x.ToString()).Value = barcodeExists.Category;
                                ws.Cell("D" + x.ToString()).Value = barcodeExists.SubCategory;
                                ws.Cell("E" + x.ToString()).Value = barcodeExists.Product;
                                ws.Cell("E" + x.ToString()).Style.Font.Bold = true;
                                ws.Cell("F" + x.ToString()).Value = barcodeExists.Site;
                                ws.Cell("G" + x.ToString()).Value = "Unsuccessful: Reason - The site has insufficent stock to allocate this item. Please check the Inventory Module";
                                ws.Cell("G" + x.ToString()).Style.Font.FontColor = XLColor.Red;
                                x++;
                            }

                        }
                        else
                        {
                            error = true;
                            for (int i = 0; i < item.ItemCount; i++)
                            {
                                ws.Cell("A" + x.ToString()).Value = item.Use;
                                ws.Cell("A" + x.ToString()).Style.Font.Bold = true;
                                ws.Cell("B" + x.ToString()).Value = item.Barcode;
                                ws.Cell("B" + x.ToString()).Style.Font.Bold = true;
                                ws.Cell("C" + x.ToString()).Value = "";
                                ws.Cell("D" + x.ToString()).Value = "";
                                ws.Cell("E" + x.ToString()).Value = "";
                                ws.Cell("F" + x.ToString()).Value = "";
                                ws.Cell("G" + x.ToString()).Value = "Unsuccessful: Reason - Barcode does not exist";
                                ws.Cell("G" + x.ToString()).Style.Font.FontColor = XLColor.Red;
                                x++;
                            }
                        }

                    }


                    //GvUploadList.DataSource = upl;
                    //GvUploadList.DataBind();
                    //mdlPopUpUploadList.Show();

                    //Journal Qty Deployed insert
                    foreach (var item in upl)
                    {
                        var sList = db.Inventory_SiteStorageDetails.Where(s => s.InventoryId == item.InventoryID && s.UseStatus == false).Take(Convert.ToInt32(item.NumberDeployed)).ToList();
                        if (sList.Count() > 0)
                        {
                            foreach (Inventory_SiteStorageDetail s in sList)
                            {
                                s.UseID = item.UseID;
                                s.UseStatus = true; //"IN USE"
                                s.DeployedBy = sessionKeys.UID;
                                s.DeployedDate = DateTime.Now;
                            }
                            //bulk update
                            db.SubmitChanges();

                            InventoryMgt.Entity.InventoryManager inventoryManager = InventoryBAL.GetInventoryByID(item.InventoryID);
                            if (inventoryManager != null)
                            {
                                inventoryManager.Notes = "Deployed By " + sessionKeys.UName;
                                inventoryManager.Qty = GetQtyAvailable(inventoryManager.Id.ToString());
                                InventoryBAL.InsertInventoryJournal(inventoryManager, Convert.ToInt32(item.NumberDeployed), "", 0, Convert.ToInt32(inventoryManager.Qty + item.NumberDeployed), item.UseID);
                            }
                        }

                    }


                    if (error)
                    {
                        int folderId = int.Parse(CreateFolder());
                        lblCustomerErrorMsg.Text = "Errors occurred during upload. Please view exception reports.";
                        //lblUploadExceptionMsg.Text = "Some errors occurred during upload. Please <a href='ProjectDocuments.aspx?mode=central&folderID=" + folderId + "&type=in' target='_blank'>click here </a> to view exception reports.";

                        ws.Columns(1, 9).AdjustToContents();
                        var rngTable = ws.Range("A1:G1");
                        // var rngHeaders = rngTable.Range("A2:I2");
                        rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngTable.Style.Font.Bold = true;
                        rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;
                        WriteUploadExeceptionReport(wb);
                    }
                    else
                    {
                        lblCustomerSuccessMsg.Text = "Items sucessfully uploaded to the inventory database.";
                    }


                }
            }
            GridInventoryBinding();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void WriteUploadExeceptionReport(XLWorkbook wb)
    {
        try
        {
            string pathLocation = HttpContext.Current.Server.MapPath("UploadData\\InventoryExeceptionReport");

            if (Directory.Exists(pathLocation) == false)
            {
                Directory.CreateDirectory(pathLocation);
            }
            string file = ddlCustomer.SelectedItem.Text + "_Inventory_Upload_" + string.Format("{0:ddMMyyy HHmmss}", DateTime.Now) + ".xlsx";
            wb.SaveAs(pathLocation + "\\" + file);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathLocation + "\\" + file);
            if (fileInfo.Exists)
            {
                byte[] document = this.StreamFile(pathLocation + "\\" + file);
                int folderId = int.Parse(CreateFolder());
                AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
                //string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", , folderID, sessionKeys.UID, sessionKeys.IncidentID);
                AC2PDocumentsController.DN_ProjectUploadInsertNew(sessionKeys.Project.ToString(), file, document, file, "application/ms-excel", "P", Convert.ToInt32(fileInfo.Length), folderId, sessionKeys.UID, sessionKeys.IncidentID);

            }
            if (File.Exists(pathLocation + "\\" + file))
                File.Delete(pathLocation + "\\" + file);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private byte[] StreamFile(string filename)
    {
        byte[] ImageData = new byte[0];
        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        try
        {
            // Create a byte array of file stream length
            ImageData = new byte[fs.Length];
            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
            //Close the File Stream

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
        }
        return ImageData;
        //return the byte data
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            string filePath = fileUpload1.PostedFile.FileName;
            string Extension = Path.GetExtension(filePath);
            //Check the Extention of file
            if (string.IsNullOrEmpty(Extension))
            {
                lblUploadErrorMsg.Visible = true;
                lblUploadErrorMsg.ForeColor = System.Drawing.Color.Red;
                lblUploadErrorMsg.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                return;
            }
            if (IsValid(fileUpload1.PostedFile.FileName))
            {

                string path = Server.MapPath("UploadData\\Inventory");
                string fileName = "\\" + fileUpload1.FileName;

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload1.SaveAs(path + fileName);
                if (Extension != ".csv")
                {
                    string conStr = string.Empty;
                    //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                    switch (Extension)
                    {
                        case ".xls": //Excel 97-03
                            conStr = "Provider= Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                            break;
                        case ".xlsx": //Excel 07
                            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                            break;

                    }
                    conStr = String.Format(conStr, path + fileName, "No");

                    InventoryUpload("Excel", path, fileName, conStr);
                }
                else
                {
                    InventoryUpload("CSV", path, fileName, "");
                }

            }
            else
            {
                lblUploadErrorMsg.Text = "Please select a valid file.";

            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }




    }
    private string CreateFolder()
    {
        string id = "0";
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("Inventory_Exeception_CreateFolder", conn))
            {
                cmd.Parameters.AddWithValue("FolderName", "Upload Exception Reports");

                cmd.Parameters.AddWithValue("ParentID", 0);
                cmd.Parameters.AddWithValue("ProjectReference", 0);
                cmd.Parameters.AddWithValue("PortfolioID", 0);
                cmd.Parameters.AddWithValue("SDID", 0);
                cmd.Parameters.AddWithValue("ContractorID", 0);
                cmd.Parameters.AddWithValue("HealthCheckId", 0);
                cmd.Parameters.Add("@FolderId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                id = cmd.Parameters["@FolderId"].Value.ToString();

            }
            return id;
        }
    }
    #endregion

    #region TranferItems Section
    protected void imgTransferApply_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            using (InventoryDataContext db = new InventoryDataContext())
            {
                int currentQty = int.Parse(lblCurrentQty.Text);
                int transferQty = int.Parse(txtAmendment.Text.Trim());
                int siteId = int.Parse(string.IsNullOrEmpty(ddlTransferSite.SelectedValue) ? "0" : ddlTransferSite.SelectedValue);
                lblTransferMsg.Text = string.Empty;
                List<Inventory_SiteStorageDetail> storageListA = new List<Inventory_SiteStorageDetail>();

                var inventoryList = db.InventoryManagers.Select(m => m).ToList();
                InventoryMgt.Entity.InventoryManager imgr = inventoryList.Where(m => m.Id == int.Parse(hfInventoryId.Value)).Select(m => m).FirstOrDefault();
                if (siteId == 0)
                {
                    if (imgr != null)
                    {

                        imgr.Qty = imgr.Qty + transferQty;
                        db.SubmitChanges();

                        int openingStock = GetQtyAvailable(imgr.Id.ToString());
                        //Product Items Insert
                        AddInventorySiteStorageDetails(imgr.Id, int.Parse(hfCategoryId.Value), int.Parse(hfSubcategoryId.Value), int.Parse(hfSiteID.Value), Convert.ToInt32(imgr.Qty));

                        //Journal Inventory Manager insert
                        imgr.Notes = transferQty + " Items manual adjustment by " + sessionKeys.UName;
                        imgr.Qty = GetQtyAvailable(imgr.Id.ToString());
                        InventoryBAL.InsertInventoryJournal(imgr, 0, "Adjustment", transferQty, openingStock, 0);

                        //TrnasferItems
                        //Inventory Stock level Jornal insert
                        JInventoryStockItem journal = new JInventoryStockItem();
                        journal.InventoryID = imgr.Id;
                        journal.UserID = sessionKeys.UID;
                        journal.Qty = transferQty;
                        journal.ReasonCode = "Adjustment";
                        journal.EntryDate = DateTime.Now;
                        journal.Notes = "Adjustment by " + sessionKeys.UName;
                        //journal.Notes = txtTransferNotes.Text.Trim();
                        InventoryBAL.InsertInventoryStockitemJournal(journal);

                    }

                }
                else
                {
                    lblTransferMsg.Text = string.Empty;
                    if (int.Parse(hfSiteID.Value) != siteId)
                    {
                        string amendment = transferQty.ToString().Replace("-", "");
                        if ((currentQty >= int.Parse(amendment)))
                        {

                            var sList = db.Inventory_SiteStorageDetails.Where(s => s.InventoryId == int.Parse(hfInventoryId.Value) && s.UseStatus == false).Select(s => s).Take(int.Parse(amendment)).ToList();
                            InventoryMgt.Entity.InventoryManager im = inventoryList.Where(m => m.ItemDescription == hfDescription.Value && m.PortfolioID == sessionKeys.PortfolioID && m.Categoery == int.Parse(hfCategoryId.Value) && m.SubCategory == int.Parse(hfSubcategoryId.Value) && m.SiteId == siteId).FirstOrDefault();
                            if (im != null)
                            {
                                foreach (var item in sList)
                                {
                                    item.InventoryId = im.Id;
                                    item.SiteId = im.SiteId;
                                    storageListA.Add(item);
                                }
                                //bulk update
                                db.SubmitChanges();

                                im.Qty = Convert.ToInt32(im.Qty) + Convert.ToInt32(amendment);
                                db.SubmitChanges();

                                int openingStock = GetQtyAvailable(im.Id.ToString());
                                //Journal Inventory Manager insert
                                InventoryMgt.Entity.InventoryManager getCurrentInventory = InventoryBAL.GetInventoryByID(im.Id);
                                if (getCurrentInventory != null)
                                {
                                    getCurrentInventory.Notes = amendment + " Items transferred from " + InventoryBAL.GetSiteNameByID(Convert.ToInt32(imgr.SiteId));
                                    getCurrentInventory.Qty = GetQtyAvailable(im.Id.ToString());
                                    InventoryBAL.InsertInventoryJournal(getCurrentInventory, 0, "Transfer", Convert.ToInt32(amendment), openingStock, 0);
                                }

                                //TrnasferItems
                                //Inventory Stock level Jornal insert
                                JInventoryStockItem journal = new JInventoryStockItem();
                                journal.InventoryID = int.Parse(hfInventoryId.Value);
                                journal.UserID = sessionKeys.UID;
                                journal.Qty = transferQty;
                                journal.ReasonCode = "Transfer";
                                journal.Notes = "Transfer to " + ddlTransferSite.SelectedItem.Text;
                                journal.EntryDate = DateTime.Now;
                                //journal.Notes = txtTransferNotes.Text.Trim();
                                InventoryBAL.InsertInventoryStockitemJournal(journal);

                                if (imgr != null)
                                {
                                    imgr.Qty = imgr.Qty - int.Parse(amendment);
                                    db.SubmitChanges();


                                    //Journal Inventory Manager insert
                                    imgr.Qty = GetQtyAvailable(imgr.Id.ToString());
                                    imgr.Notes = amendment + " Items transferred to " + ddlTransferSite.SelectedItem.Text;
                                    InventoryBAL.InsertInventoryJournal(imgr, 0, "Transfer", int.Parse(amendment), (Convert.ToInt32(imgr.Qty) + Convert.ToInt32(amendment)), 0);
                                }




                            }
                            else
                            {
                                InventoryMgt.Entity.InventoryManager inventoryManager = new InventoryMgt.Entity.InventoryManager();
                                inventoryManager.PartNumber = hfPartNumber.Value;
                                inventoryManager.PortfolioID = sessionKeys.PortfolioID;
                                inventoryManager.Categoery = int.Parse(hfCategoryId.Value);
                                inventoryManager.SubCategory = int.Parse(hfSubcategoryId.Value);
                                inventoryManager.SiteId = siteId;
                                inventoryManager.Image = Guid.Empty;
                                inventoryManager.Supplier = 0;
                                inventoryManager.Manufacturer = 0;
                                inventoryManager.ItemDescription = hfDescription.Value;
                                inventoryManager.Barcode = hfBarcode.Value;
                                inventoryManager.SectionType = "Global";
                                inventoryManager.Notes = "";
                                inventoryManager.Qty = int.Parse(amendment);
                                db.InventoryManagers.InsertOnSubmit(inventoryManager);
                                db.SubmitChanges();


                                //Product Items Update

                                foreach (var item in sList)
                                {
                                    item.InventoryId = inventoryManager.Id;
                                    item.SiteId = siteId;
                                    storageListA.Add(item);
                                }
                                //bulk update
                                db.SubmitChanges();


                                //Journal Inventory Manager insert
                                InventoryMgt.Entity.InventoryManager getCurrent = InventoryBAL.GetInventoryByID(inventoryManager.Id);
                                if (getCurrent != null)
                                {
                                    getCurrent.Notes = amendment + " Items transferred from " + InventoryBAL.GetSiteNameByID(Convert.ToInt32(imgr.SiteId));
                                    InventoryBAL.InsertInventoryJournal(getCurrent, 0, "Transfer", int.Parse(amendment), Convert.ToInt32(getCurrent.Qty), 0);
                                }


                                //TrnasferItems
                                //Inventory Stock level Jornal insert
                                JInventoryStockItem journal = new JInventoryStockItem();
                                journal.InventoryID = int.Parse(hfInventoryId.Value);
                                journal.UserID = sessionKeys.UID;
                                journal.Qty = transferQty;
                                journal.ReasonCode = "Transfer";
                                journal.Notes = "Transfer to " + ddlTransferSite.SelectedItem.Text;
                                journal.EntryDate = DateTime.Now;
                                // journal.Notes = txtTransferNotes.Text.Trim();
                                InventoryBAL.InsertInventoryStockitemJournal(journal);

                                if (imgr != null)
                                {
                                    imgr.Qty = imgr.Qty - int.Parse(amendment);
                                    db.SubmitChanges();
                                    //Journal Inventory Manager insert
                                    imgr.Qty = GetQtyAvailable(imgr.Id.ToString());
                                    imgr.Notes = amendment + " Items transferred to " + ddlTransferSite.SelectedItem.Text;
                                    InventoryBAL.InsertInventoryJournal(imgr, 0, "Transfer", int.Parse(amendment), (Convert.ToInt32(imgr.Qty) + Convert.ToInt32(amendment)), 0);
                                }


                            }
                        }
                        else
                        {
                            lblTransferMsg.Text = "Sorry but there are no items available to carry out the transfer. Please release some before you continue.";
                            mdlPopupTransferItem.Show();
                        }
                    }
                    else
                    {
                        lblTransferMsg.Text = "Sorry can't transfer to same site. Please select different site.";
                        mdlPopupTransferItem.Show();
                    }


                }

                //BindProductsSection(hfInventoryId.Value);
                //BindStorageGrid();
                BindTransferItems();
                BindInventoryDetails();
                GridInventoryBinding();
                mdlPopupTransferItem.Show();
                pnlAddItems.Visible = false;
                pnlUse.Visible = false;



            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public string FormateQty(string qty)
    {

        if (!qty.Contains("-"))
            return "+" + qty;
        else
            return qty;

    }
    private void BindTransferItems()
    {
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    var jList = db.JInventoryStockItems.Where(j => j.InventoryID == int.Parse(hfInventoryId.Value)).Select(j => j).ToList();
                    var journalTranferList = (from p in jList
                                              join c in contractorList on p.UserID equals c.ID
                                              where p.InventoryID == int.Parse(hfInventoryId.Value)
                                              orderby p.EntryDate descending
                                              select new { p.ID, p.EntryDate, p.InventoryID, Qty = p.Qty, p.ReasonCode, p.Notes, c.ContractorName }).ToList();

                    gvTransferItems.DataSource = journalTranferList;
                    gvTransferItems.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Export reports to "PDF" and "Excel"
    private void ExportData(string typeOfExport)
    {
        try
        {
            int customer = Convert.ToInt32(ddlcustomerInSearch.SelectedValue.ToString());
            int site = Convert.ToInt32(ddlsiteInSearch.SelectedValue.ToString());

            string customerName = ddlcustomerInSearch.SelectedItem.Text;
            string siteName = ddlsiteInSearch.SelectedItem.Text;
            string fileName = customerName + "_UsageReport_" + DateTime.Now;
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    var inventoryJournalList = db.InventoryManagerJournals.ToList();
                    var inventoryList = db.InventoryManagers.ToList();
                    var siteStorageList = db.Inventory_SiteStorageDetails.ToList();
                    var categoryList = db.ServiceCatalog_categories.ToList();
                    var siteList = db.Sites.ToList();
                    var useList = db.Inventory_Uses.ToList();
                    useList.Add(new Inventory_Use { ID = 0, Code = "", Name = "" });
                    var usageReport = (from i in inventoryJournalList
                                       join p in inventoryList on i.InventoryId equals p.Id
                                       join c in categoryList on i.Categoery equals c.ID
                                       join sc in categoryList on i.SubCategory equals sc.ID
                                       join s in siteList on i.SiteId equals s.ID
                                       join u in contractorList on i.ModifiedBy equals u.ID
                                       join ul in useList on i.UseID equals ul.ID
                                       where i.PortfolioID == customer && i.SiteId == site
                                       orderby c.CategoryName, sc.CategoryName, i.ItemDescription, i.MdofiedDate descending
                                       select new
                                       {
                                           i.Id,
                                           ClosingStock = i.Qty,
                                           Product = i.ItemDescription,
                                           NumberDeployed = i.NoDeployed,
                                           i.Notes,
                                           i.TransferQty,
                                           UseCode = ul.Name,
                                           i.ReasonCode,
                                           OpeningStock = i.OpeningStock,
                                           CategoryName = c.CategoryName,
                                           SubCategoryName = sc.CategoryName,
                                           Sitename = s.Site1,
                                           DeployedDate = string.Format("{0:dd/MM/yy}", i.MdofiedDate),
                                           DeployedBy = u.ContractorName
                                       }).ToList();
                    if (!string.IsNullOrEmpty(txtFromDeployedDate.Text))
                    {
                        usageReport = usageReport.Where(r => Convert.ToDateTime(r.DeployedDate) >= Convert.ToDateTime(txtFromDeployedDate.Text)).Select(r => r).ToList();
                    }
                    if (!string.IsNullOrEmpty(txtToDeployedDate.Text))
                    {
                        usageReport = usageReport.Where(r => Convert.ToDateTime(r.DeployedDate) <= Convert.ToDateTime(txtToDeployedDate.Text)).Select(r => r).ToList();
                    }
                    if (ddlCategory.SelectedValue != "0")
                    {
                        usageReport = usageReport.Where(r => r.CategoryName == ddlCategory.SelectedItem.Text).Select(r => r).ToList();
                    }
                    if (ddlSubCategory.SelectedValue != "0")
                    {
                        usageReport = usageReport.Where(r => r.SubCategoryName == ddlSubCategory.SelectedItem.Text).Select(r => r).ToList();
                    }

                    var summaryReport = from s in usageReport
                                        group s by new { s.CategoryName, s.SubCategoryName, s.Product } into grouping
                                        select new
                                        {
                                            OpeningDate = grouping.Min(g => Convert.ToDateTime(g.DeployedDate)),
                                            EndDate = grouping.Max(g => Convert.ToDateTime(g.DeployedDate)),
                                            Category = grouping.Key.CategoryName,
                                            SubCategory = grouping.Key.SubCategoryName,
                                            Product = grouping.Key.Product,
                                            OpeningStock = grouping.First().OpeningStock,
                                            NumberDeployed = grouping.Sum(g => g.NumberDeployed),
                                            ClosingStock = grouping.First().ClosingStock
                                        };




                    if (typeOfExport == "Excel")
                    {
                        var wb = new XLWorkbook();
                        var ws = wb.Worksheets.Add("Usage Report");

                        // Title
                        ws.Cell("A1").Value = "Customer:";
                        ws.Cell("B1").Value = ddlCustomer.SelectedItem.Text;
                        ws.Cell("C1").Value = "Site:";
                        ws.Cell("D1").Value = ddlSite.SelectedItem.Text;
                        ws.Cell("A2").Value = "Usage Report";
                        ws.Cell("A3").Value = "Date";
                        ws.Cell("B3").Value = "Category";
                        ws.Cell("C3").Value = "Sub-Category";
                        ws.Cell("D3").Value = "Product";
                        ws.Cell("E3").Value = "Opening Stock";
                        ws.Cell("F3").Value = "Number Deployed";
                        ws.Cell("G3").Value = "Use Code";
                        ws.Cell("H3").Value = "Reason Code";
                        ws.Cell("I3").Value = "Transfer Qty";
                        ws.Cell("J3").Value = "Closing Stock";
                        ws.Cell("K3").Value = "Deployed By";

                        int x = 4;
                        foreach (var item in usageReport)
                        {
                            ws.Cell("A" + x.ToString()).Value = item.DeployedDate;
                            ws.Cell("A" + x.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                            ws.Cell("B" + x.ToString()).Value = item.CategoryName;
                            ws.Cell("C" + x.ToString()).Value = item.SubCategoryName;
                            ws.Cell("D" + x.ToString()).Value = item.Product;
                            ws.Cell("E" + x.ToString()).Value = item.OpeningStock;
                            ws.Cell("E" + x.ToString()).Style.Font.Bold = true;
                            ws.Cell("F" + x.ToString()).Value = item.NumberDeployed;
                            ws.Cell("G" + x.ToString()).Value = item.UseCode;
                            ws.Cell("H" + x.ToString()).Value = item.ReasonCode;
                            ws.Cell("I" + x.ToString()).Value = item.TransferQty;
                            ws.Cell("J" + x.ToString()).Value = item.ClosingStock;
                            ws.Cell("J" + x.ToString()).Style.Font.Bold = true;
                            ws.Cell("K" + x.ToString()).Value = item.DeployedBy;
                            x++;
                        }

                        // From worksheet
                        var rngTable = ws.Range("A1:K4");

                        var rngHeaders = rngTable.Range("A3:K3");
                        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngHeaders.Style.Font.Bold = true;
                        rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                        rngTable.Cell(2, 1).Style.Font.Bold = true;
                        rngTable.Cell(1, 2).Style.Font.Bold = true;
                        rngTable.Cell(1, 4).Style.Font.Bold = true;
                        rngTable.Cell(2, 1).Style.Font.FontColor = XLColor.White;
                        rngTable.Cell(2, 1).Style.Font.FontSize = 15;
                        rngTable.Cell(2, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                        rngTable.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        rngTable.Row(2).Merge();

                        ws.Columns(1, 11).AdjustToContents();


                        // Second worksheet
                        var ws2 = wb.AddWorksheet("Summary Report");
                        // Title
                        ws2.Cell("A1").Value = "Opening Date";
                        ws2.Cell("B1").Value = "End Date";
                        ws2.Cell("C1").Value = "Category";
                        ws2.Cell("D1").Value = "Sub-Category";
                        ws2.Cell("E1").Value = "Product";
                        ws2.Cell("F1").Value = "Opening Stock";
                        ws2.Cell("G1").Value = "Number Deployed";
                        ws2.Cell("H1").Value = "Closing Stock";
                        int y = 2;
                        foreach (var item in summaryReport)
                        {
                            ws2.Cell("A" + y.ToString()).Value = item.OpeningDate;
                            ws2.Cell("A" + y.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                            ws2.Cell("B" + y.ToString()).Value = item.EndDate;
                            ws2.Cell("B" + y.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                            ws2.Cell("C" + y.ToString()).Value = item.Category;
                            ws2.Cell("D" + y.ToString()).Value = item.SubCategory;
                            ws2.Cell("E" + y.ToString()).Value = item.Product;
                            ws2.Cell("F" + y.ToString()).Value = item.OpeningStock;
                            ws2.Cell("F" + y.ToString()).Style.Font.Bold = true;
                            ws2.Cell("G" + y.ToString()).Value = item.NumberDeployed;
                            ws2.Cell("H" + y.ToString()).Value = item.ClosingStock;
                            ws2.Cell("H" + y.ToString()).Style.Font.Bold = true;
                            y++;
                        }

                        var rngTable2 = ws2.Range("A1:H1");
                        rngTable2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngTable2.Style.Font.Bold = true;
                        rngTable2.Style.Fill.BackgroundColor = XLColor.LightGray;
                        ws2.Columns(1, 8).AdjustToContents();



                        string path = HttpContext.Current.Server.MapPath("UploadData\\Inventory");

                        if (Directory.Exists(path) == false)
                        {
                            Directory.CreateDirectory(path);
                        }

                        wb.SaveAs(path + "\\" + "UsageReport.xlsx");

                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "UsageReport.xlsx");
                        if (fileInfo.Exists)
                        {
                            System.Web.HttpContext.Current.Response.Clear();
                            System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
                            System.Web.HttpContext.Current.Response.Flush();
                            System.Web.HttpContext.Current.Response.End();

                        }
                    }
                    else
                    {
                        PdfPTable table = new PdfPTable(11) { WidthPercentage = 100 };// table for Usage



                        //PdfPCell cell = new PdfPCell();
                        //float[] widths = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f };
                        //table.SetWidths(widths);
                        //cell.Phrase = new Phrase("ID");
                        //table.AddCell(cell);
                        Font headerFont = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.BOLD, new BaseColor(System.Drawing.Color.White))));
                        BaseColor headerColour = new BaseColor(System.Drawing.Color.Gray);

                        //Header

                        PdfPCell cell = new PdfPCell(new Phrase("Date", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Category", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Sub-Category", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Product", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Opening Stock", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Number Deployed", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Use Code", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Reason Code", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Transfer Qty", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Closing Stock", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Deployed By", headerFont));
                        cell.BackgroundColor = headerColour;
                        table.AddCell(cell);

                        foreach (var item in usageReport)
                        {
                            // Data rows

                            Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                            cell = new PdfPCell(new Phrase(item.DeployedDate, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.CategoryName, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.SubCategoryName, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.Product, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.OpeningStock.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.NumberDeployed.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.UseCode.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.ReasonCode, font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.TransferQty.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.ClosingStock.ToString(), font));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.DeployedBy, font));
                            table.AddCell(cell);

                        }

                        PdfPTable summaryTable = new PdfPTable(8) { WidthPercentage = 100 }; // table for Summary

                        PdfPCell summaryCell = new PdfPCell(new Phrase("Opening Date", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("End Date", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Category", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Sub-Category", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Product", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Opening Stock", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Number Deployed", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);
                        summaryCell = new PdfPCell(new Phrase("Closing Stock", headerFont));
                        summaryCell.BackgroundColor = headerColour;
                        summaryTable.AddCell(summaryCell);


                        foreach (var item in summaryReport)
                        {
                            // Data rows

                            Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                            summaryCell = new PdfPCell(new Phrase(item.OpeningDate.ToString().Remove(10), font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.EndDate.ToString().Remove(10), font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.Category, font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.SubCategory, font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.Product, font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.OpeningStock.ToString(), font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.NumberDeployed.ToString(), font));
                            summaryTable.AddCell(summaryCell);
                            summaryCell = new PdfPCell(new Phrase(item.ClosingStock.ToString(), font));
                            summaryTable.AddCell(summaryCell);

                        }


                        string path = Server.MapPath("~/WF/UploadData/Temp");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 2f, 2f, 2f, 1f);
                        var writer = PdfWriter.GetInstance(document, Response.OutputStream);


                        Paragraph paragraph = new Paragraph("Usage Report", new Font(FontFactory.GetFont("Tahoma", 10f, Font.BOLD)));
                        paragraph.SpacingAfter = 0f;
                        Paragraph paragraph1 = new Paragraph("Summary Report", new Font(FontFactory.GetFont("Tahoma", 10f, Font.BOLD)));
                        paragraph1.SpacingAfter = 0f;
                        Chunk linebreak = new Chunk(new LineSeparator(1f, 100f, new BaseColor(System.Drawing.Color.Gray), Element.ALIGN_CENTER, -1));
                        Chunk chunk = new Chunk("Customer: " + customerName + "    Site: " + siteName, FontFactory.GetFont("Tahoma", 8f, Font.BOLD));
                        string imageURL = Server.MapPath("media/deffinity_logo.gif");
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(400f, 40f);

                        //Give space before image
                        jpg.SpacingAfter = 30f;

                        ////Give some space after the image
                        //jpg.SpacingAfter = 1f;
                        jpg.Alignment = Element.ALIGN_RIGHT;



                        document.Open();
                        document.Add(jpg);
                        document.Add(paragraph);
                        document.Add(linebreak);
                        document.Add(new Paragraph("\n"));
                        document.Add(chunk);
                        document.Add(table);
                        document.Add(new Paragraph("\n"));
                        document.Add(paragraph1);
                        document.Add(linebreak);
                        document.Add(summaryTable);
                        document.Close();

                        //System.Diagnostics.Process.Start(FullPath); //automatically opens
                        //Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                        Response.Write(document);
                        // Response.TransmitFile(FullPath);
                        Response.End();


                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportData("Excel");
    }
    protected void imgbtnExportToPdf_Click(object sender, ImageClickEventArgs e)
    {
        ExportData("PDF");
    }
    #endregion

    protected void GvUploadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToLower() == "commit")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 rowind = gvRow.RowIndex;
                int id = int.Parse(e.CommandArgument.ToString());
                Button btnCommit = (Button)GvUploadList.Rows[rowind].FindControl("btnConfirm");
                Label lblOpenQty = (Label)GvUploadList.Rows[rowind].FindControl("lblOpeningQty");
                int inventoryID = Convert.ToInt32(((Label)GvUploadList.Rows[rowind].FindControl("lblID")).Text);
                int numberDeployed = Convert.ToInt32(((Label)GvUploadList.Rows[rowind].FindControl("lblNumberDeployed")).Text);
                int useID = Convert.ToInt32(((Label)GvUploadList.Rows[rowind].FindControl("lblUseID")).Text);

                if (lblOpenQty.Text == "0")
                {
                    btnCommit.Text = "Failed";
                    mdlPopUpUploadList.Show();
                }
                else
                {
                    btnCommit.Text = "Commited";
                    btnCommit.Enabled = false;
                    using (InventoryDataContext db = new InventoryDataContext())
                    {
                        var sList = db.Inventory_SiteStorageDetails.Where(s => s.InventoryId == inventoryID && s.UseStatus == false).Take(numberDeployed).ToList();
                        if (sList.Count() > 0)
                        {
                            foreach (Inventory_SiteStorageDetail s in sList)
                            {
                                s.UseID = useID;
                                s.UseStatus = true; //"IN USE"
                                s.DeployedBy = sessionKeys.UID;
                                s.DeployedDate = DateTime.Now;
                            }
                            //bulk update
                            db.SubmitChanges();
                            //Journal Insert
                            InventoryManager manager = InventoryBAL.GetInventoryByID(inventoryID);
                            manager.Qty = InventoryBAL.GetQtyAvailable(inventoryID.ToString());
                            manager.Notes = "Deployed By " + sessionKeys.UName;
                            // InventoryBAL.InsertInventoryJournal(manager, numberDeployed);
                        }
                    }
                    mdlPopUpUploadList.Show();
                }



            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        mdlPopUpUploadList.Hide();
        GridInventoryBinding();
    }
    protected void grdInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgDelete = ((ImageButton)e.Row.FindControl("LinkButtonDelete1"));

            // If it's resources hide image 
            if (!CheckPermission())
            {
                imgDelete.Visible = false;
            }
            else
            {
                imgDelete.Visible = true;
            }
        }
    }

    private string GetPropertyValue(dynamic row, string colName)
    {
        string retval = string.Empty;

        switch (colName)
        {
            case "Requester":
                retval = row.Requester;
                break;
            case "Project":
                retval = row.projectid;
                break;
            case "Qty Used":
                retval = Convert.ToString(row.QtyUsed);
                break;
            case "Notes":
                retval = row.notes;
                break;
            case "Status":
                retval = InventoryStatus().Where(o => o.Value == Convert.ToString(row.Status)).FirstOrDefault().ToString();
                break;
            default:
                retval = string.Empty;
                break;
        }

        return retval;
    }
    public List<System.Web.UI.WebControls.ListItem> InventoryStatus()
    {
        List<System.Web.UI.WebControls.ListItem> li = new List<System.Web.UI.WebControls.ListItem>();
        li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        li.Add(new System.Web.UI.WebControls.ListItem("Use", "1"));
        li.Add(new System.Web.UI.WebControls.ListItem("Dispose", "2"));
        li.Add(new System.Web.UI.WebControls.ListItem("Replenish", "3"));
        return li;
    }

    protected void ddlarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlarea.SelectedValue != "0")
        {
            BindLocation(int.Parse(ddlarea.SelectedValue));
        }
        else
        {
            ddllocation.Items.Clear();
            ddllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllocation.SelectedValue != "0")
        {
            BindShelf(int.Parse(ddllocation.SelectedValue));
        }
        else
        {
            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void ddlshelf_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlshelf.SelectedValue != "0")
        {
            BindBin(int.Parse(ddlshelf.SelectedValue));
        }
        else
        {
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            InventoryGridBind();
            //BindGridInSummary();
            BindData_SearchUsageGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void InventoryGridBind(bool isInserted = false)
    {
        try
        {

            IPP = new PortfolioRepository<ProjectPortfolio>();
            IA = new InventoryRepository<Inventory_Area>();
            IL = new InventoryRepository<Inventory_Locatin>();
            IS = new InventoryRepository<Inventory_Shelf>();
            IB = new InventoryRepository<Inventory_Bin>();
            IAinfo = new InventoryRepository<InventoryAdditionalInfo>();
            ICF = new InventoryRepository<inventoryCustomField>();
            idc = new InventoryDataContext();
            pdc = new projectTaskDataContext();
            var bRepository = new InventoryRepository<Inventory_Batch>();
            var bcRepository = new InventoryRepository<Inventory_BatchCustomData>();

            List<int> IntCollection = new List<int>();
            string stringvalue = txtText.Text.ToLower().Trim();

            List<InventoryMgt.Entity.InventoryManager> imlist = new List<InventoryMgt.Entity.InventoryManager>();
            if (sessionKeys.PortfolioID > 0)
            {
                imlist = idc.InventoryManagers.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
            }
            else
            {
                imlist = idc.InventoryManagers.ToList();
            }
            var portfolioList = IPP.GetAll().Where(o => imlist.Select(p => p.PortfolioID).ToArray().Contains(o.ID)).ToList();


            var sitelist = (from a in idc.Sites
                            join b in idc.AssignedSitesToPortfolios on a.ID equals b.SiteID
                            where b.Portfolio != null && imlist.Select(p => p.PortfolioID).ToArray().Contains(b.Portfolio.HasValue ? b.Portfolio.Value : 0)
                            select new
                            {
                                id = b.SiteID,
                                Site = a.Site1
                            }).Distinct().ToList();
            var arealist = IA.GetAll().Where(o => o.id != 0 && imlist.Select(p => p.Area).ToArray().Contains(o.id)).ToList();

            var llist = IL.GetAll().Where(o => imlist.Select(p => p.Location).ToArray().Contains(o.id)).ToList();
            var slist = IS.GetAll().Where(o => imlist.Select(p => p.Shelf).ToArray().Contains(o.id)).ToList();
            var blist = IB.GetAll().Where(o => imlist.Select(p => p.Bin).ToArray().Contains(o.id)).ToList();

            var mlist = idc.Manufacturers.Where(o => imlist.Select(p => p.Manufacturer).ToArray().Contains(o.Id)).ToList();
            var categoryList = idc.ServiceCatalog_categories.Where(a => a.MasterID == 0 && imlist.Select(p => p.Categoery).ToArray().Contains(a.ID)).ToList();
            var SubCategorylist = idc.ServiceCatalog_categories.Where(a => a.MasterID != 0 && imlist.Select(p => p.SubCategory).ToArray().Contains(a.ID)).ToList();
            //var IM_PsdList = idc.InventoryManager_PSDProducts.Where(o => o.SectionType == "PROJECT").ToList();
            //var projectList = pdc.Projects.ToList();
            var Customlist = IAinfo.GetAll().Where(o => imlist.Select(p => p.Id).ToArray().Contains(o.productId.HasValue ? o.productId.Value : 0)).ToList();
            List<Inventory_Batch> bList = bRepository.GetAll().Where(o => imlist.Select(p => p.Id).ToArray().Contains(o.InventoryID.HasValue ? o.InventoryID.Value : 0)).ToList();
            List<Inventory_BatchCustomData> bcList = new List<Inventory_BatchCustomData>();
            if (bList.Count > 0)
            {
                bcList = bcRepository.GetAll().Where(o => bList.Select(b => b.ID).ToArray().Contains(o.BatchID.HasValue ? o.BatchID.Value : 0)).ToList();
            }

            var r1 = (from x in imlist
                      select new InventorySearch
                      {
                          PortfolioID = x.PortfolioID,
                          PortfolioName = portfolioList.Where(o => o.ID == x.PortfolioID).Select(o => o.PortFolio).FirstOrDefault(),
                          Id = x.Id,
                          Qty = x.Qty,
                          QtyOnOrder = x.QtyOnOrder,
                          Categoery = x.Categoery,
                          SubCategory = x.SubCategory,
                          SiteId = x.SiteId,
                          ExpctArvlDate = x.ExpctArvlDate,
                          Image = x.Image,
                          ItemDescription = x.ItemDescription,
                          Notes = x.Notes,
                          CategoryName = categoryList.Where(o => o.ID == x.Categoery).Select(o => o.CategoryName).FirstOrDefault(),
                          SubCategoryName = SubCategorylist.Where(o => o.ID == x.SubCategory).Select(o => o.CategoryName).FirstOrDefault(),
                          ManufacturerName = mlist.Where(o => o.Id == x.Manufacturer).Select(o => o.Name).FirstOrDefault(),
                          Sitename = sitelist.Where(o => o.id == x.SiteId).Select(o => o.Site).FirstOrDefault(),
                          AreaName = arealist.Where(o => o.id == x.Area).Select(o => o.Name).FirstOrDefault(),
                          LocationName = llist.Where(o => o.id == x.Location).Select(o => o.Name).FirstOrDefault(),
                          ShelfName = slist.Where(o => o.id == x.Shelf).Select(o => o.Name).FirstOrDefault(),
                          BinName = blist.Where(o => o.id == x.Bin).Select(o => o.Name).FirstOrDefault(),
                          Area = x.Area,
                          Location = x.Location,
                          Shelf = x.Shelf,
                          Bin = x.Bin,
                          Manufacturer = x.Manufacturer
                      }).ToList();
            // ProductName = IM_PsdList.Where(o => o.ProductId == x.Id).Select(o => o.i.ToString()).FirstOrDefault(),
            // || (x.Id != null && IM_PsdList.Where(o => o.ProductId == x.Id).Select(o => o.projectid.ToString()).ToList().Contains(stringvalue))
            if (ddlcustomerInSearch.SelectedValue != "0")
            {
                r1 = r1.Where(o => o.PortfolioID == int.Parse(ddlcustomerInSearch.SelectedValue)).ToList();
            }
            if (ddlsiteInSearch.SelectedValue != "0")
            {
                r1 = r1.Where(o => o.SiteId == int.Parse(ddlsiteInSearch.SelectedValue)).ToList();
            }
            if (!isInserted)
            {
                if (stringvalue != string.Empty)
                {
                    string[] Svalue = stringvalue.Split(' ');
                    List<string> ListStrings = new List<string>();
                    List<string> ListPreStrings = new List<string>();
                    List<int> ids = new List<int>();
                    string Sname = string.Empty;
                    string PreString = string.Empty;
                    string IsFirstTime = string.Empty;
                    if (Svalue.Length > 1)
                    {
                        for (int i = 0; i < Svalue.Length + 1; i++)
                        {
                            if (i != Svalue.Length)
                            {
                                Sname += Svalue[i].ToString() + " ";
                            }
                            var r1list = (from x in r1
                                          where (
                                              (!string.IsNullOrEmpty(x.Notes) && x.Notes.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.PortfolioName) && x.PortfolioName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.Sitename) && x.Sitename.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.AreaName) && x.AreaName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.LocationName) && x.LocationName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.ShelfName) && x.ShelfName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.BinName) && x.BinName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.ManufacturerName) && x.ManufacturerName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.CategoryName) && x.CategoryName.ToLower().Contains(Sname.Trim()))
                                              || (!string.IsNullOrEmpty(x.SubCategoryName) && x.SubCategoryName.ToLower().Contains(Sname.Trim()))
                                              || (x.ItemDescription != null && x.ItemDescription.ToLower().Contains(Sname.Trim()))
                                              || (Customlist.Count != 0 && (Customlist.Where(o => o.productId == x.Id).Select(o => o.CustomFieldValue.ToLower().ToString())).ToList().Contains(Sname.Trim()))
                                              || (bList.Count != 0 && (bList.Where(o => o.InventoryID == x.Id).Select(o => o.BatchDisplayName.ToLower().ToString())).ToList().Contains(Sname.Trim()))
                                              || (bcList != null && (bcList.Select(o => o.CustomFieldValue.ToLower().ToString())).ToList().Contains(Sname.Trim()))

                                              )
                                          select x).ToList();

                            int r1SampleCount = r1list.Count;
                            if (r1SampleCount == 0)
                            {
                                IntCollection.AddRange(ids.ToList().Distinct());
                                ids.Clear();
                                //ListPreStrings.Add(PreString);
                                // ListStrings.Add(PreString.Trim());
                                // PreString = string.Empty;
                                if (i != Svalue.Length)
                                {
                                    Sname = Svalue[i] + " ";
                                }
                            }
                            else
                            {
                                ids.AddRange(r1list.Select(o => o.Id).ToArray());
                                //PreString = PreString + Svalue[i] + " ";
                                //IsFirstTime = "No";
                                //ListStrings.Add(Sname.Trim());
                            }
                            if (Svalue.Length == i)
                            {
                                IntCollection.AddRange(ids.ToList().Distinct());
                                ids.Clear();
                            }
                        }
                        var duplicates = IntCollection.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                        var Orginal = IntCollection.GroupBy(x => x).Where(g => g.Count() == 1).Select(y => y.Key).ToList();
                        IntCollection.Clear();
                        if (duplicates.Count != 0)
                        {
                            IntCollection.AddRange(duplicates.ToList());
                        }
                        IntCollection.AddRange(Orginal.ToList());
                        grdInventory.DataSource = r1.Where(o => IntCollection.IndexOf(o.Id) >= 0).OrderBy(o => IntCollection.IndexOf(o.Id)).ToList();
                    }
                    else
                    {
                        r1 = (from x in r1
                              where (
                                  (!string.IsNullOrEmpty(x.Notes) && x.Notes.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.PortfolioName) && x.PortfolioName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.Sitename) && x.Sitename.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.AreaName) && x.AreaName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.LocationName) && x.LocationName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.ShelfName) && x.ShelfName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.BinName) && x.BinName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.ManufacturerName) && x.ManufacturerName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.CategoryName) && x.CategoryName.ToLower().Contains(stringvalue))
                                  || (!string.IsNullOrEmpty(x.SubCategoryName) && x.SubCategoryName.ToLower().Contains(stringvalue))
                                  || (x.ItemDescription != null && x.ItemDescription.ToLower().Contains(stringvalue))
                                  || (Customlist.Count != 0 && (Customlist.Where(o => o.productId == x.Id).Select(o => o.CustomFieldValue.ToLower().ToString())).ToList().Contains(stringvalue))
                                  || (bList.Count != 0 && (bList.Where(o => o.InventoryID == x.Id).Select(o => o.BatchDisplayName.ToLower().ToString())).ToList().Contains(stringvalue.Trim()))
                                  || (bcList != null && (bcList.Where(o => o.Inventory_Batch.InventoryID == x.Id).Select(o => o.CustomFieldValue.ToLower().ToString())).ToList().Contains(stringvalue.Trim()))
                                  )
                              select x).ToList();
                        grdInventory.DataSource = r1.ToList();
                    }
                }
                else
                {
                    grdInventory.DataSource = r1.ToList();
                }
            }
            else
            {
                grdInventory.DataSource = r1.OrderByDescending(o => o.Id).ToList();
            }

            grdInventory.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void GridFilteredRecords(Array Svalue)
    {
        try
        {

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlareaStockadd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlareaStockadd.SelectedValue != "0")
        {
            BindLocationInpopUp(int.Parse(ddlareaStockadd.SelectedValue));
        }
        mdlpopupaddnewstock.Show();
    }
    protected void ddllocationStockadd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllocationStockadd.SelectedValue != "0")
        {
            BindShelfInpopUp(int.Parse(ddllocationStockadd.SelectedValue));
        }
        mdlpopupaddnewstock.Show();
    }
    protected void ddlshelfStockadd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlshelfStockadd.SelectedValue != "0")
        {
            BindBinInpopUp(int.Parse(ddlshelfStockadd.SelectedValue));
        }
        mdlpopupaddnewstock.Show();
    }
    public void BindSiteInPopup()
    {
        try
        {
            SC_cntMaterialCS = new SC_ContractorMaterial();
            //ddlSite.DataSource =  SC_cntMaterialCS.GetSites();
            ddlsiteStockadd.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(int.Parse(ddlCustomer.SelectedValue));
            ddlsiteStockadd.DataTextField = "Site";
            ddlsiteStockadd.DataValueField = "ID";
            ddlsiteStockadd.DataBind();
            ddlsiteStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindAreaInPopup(int cid)
    {
        try
        {
            IA = new InventoryRepository<InventoryMgt.Entity.Inventory_Area>();
            ddlareaStockadd.DataSource = IA.GetAll().Where(a => a.Cid == cid).ToList();
            ddlareaStockadd.DataTextField = "Name";
            ddlareaStockadd.DataValueField = "id";
            ddlareaStockadd.DataBind();
            ddlareaStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddllocationStockadd.Items.Clear();
            ddllocationStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlshelfStockadd.Items.Clear();
            ddlshelfStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlbinStockadd.Items.Clear();
            ddlbinStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindLocationInpopUp(int Aid)
    {
        try
        {
            IL = new InventoryRepository<InventoryMgt.Entity.Inventory_Locatin>();
            ddllocationStockadd.DataSource = IL.GetAll().Where(a => a.IA_id == Aid).ToList();
            ddllocationStockadd.DataTextField = "Name";
            ddllocationStockadd.DataValueField = "id";
            ddllocationStockadd.DataBind();
            ddllocationStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlshelfStockadd.Items.Clear();
            ddlshelfStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            ddlbinStockadd.Items.Clear();
            ddlbinStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindShelfInpopUp(int Lid)
    {
        try
        {
            IS = new InventoryRepository<InventoryMgt.Entity.Inventory_Shelf>();
            ddlshelfStockadd.DataSource = IS.GetAll().Where(a => a.IL_id == Lid).ToList();
            ddlshelfStockadd.DataTextField = "Name";
            ddlshelfStockadd.DataValueField = "id";
            ddlshelfStockadd.DataBind();
            ddlshelfStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));

            ddlbinStockadd.Items.Clear();
            ddlbinStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBinInpopUp(int Sid)
    {
        try
        {
            IB = new InventoryRepository<InventoryMgt.Entity.Inventory_Bin>();
            ddlbinStockadd.DataSource = IB.GetAll().Where(a => a.IS_id == Sid).ToList();
            ddlbinStockadd.DataTextField = "Name";
            ddlbinStockadd.DataValueField = "id";
            ddlbinStockadd.DataBind();
            ddlbinStockadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void Imgbtnbinadd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlshelfStockadd.SelectedValue != "0")
            {
                ddlbinStockadd.Visible = false;
                Imgbtnbinadd.Visible = false;
                txtBinInPopup.Visible = true;
                ImgAddBin.Visible = true;
                btnCaneclBin.Visible = true;
                ddlenabledMethod(false);
            }
            else
            {
                lblmsgInpopup.ForeColor = System.Drawing.Color.Red;
                lblmsgInpopup.Text = "Please select shelf.";
            }
            mdlpopupaddnewstock.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ddlenabledMethod(bool setvalue)
    {
        ddlsiteStockadd.Enabled = setvalue;
        ddlareaStockadd.Enabled = setvalue;
        ddllocationStockadd.Enabled = setvalue;
        ddlshelfStockadd.Enabled = setvalue;
        txtQuantity.Enabled = setvalue;
        txtnotesinpopup.Enabled = setvalue;
        btnSubmitInPopUp.Enabled = setvalue;
    }
    protected void btnSubmitInPopUp_Click(object sender, EventArgs e)
    {
        try
        {
            IMJ = new InventoryRepository<InventoryManagerJournal>();
            INS = new InventoryRepository<Inventory_New_Stock>();
            IM = new InventoryRepository<InventoryManager>();
            In_mj = new InventoryManagerJournal();
            In_ns = new Inventory_New_Stock();
            In_Ma = new InventoryManager();

            In_ns.ProductId = int.Parse(ddlProduct.SelectedValue);
            In_ns.SiteId = int.Parse(ddlsiteStockadd.SelectedValue);
            In_ns.AreaId = int.Parse(ddlareaStockadd.SelectedValue);
            In_ns.LocationId = int.Parse(ddllocationStockadd.SelectedValue);
            In_ns.shelfId = int.Parse(ddlshelfStockadd.SelectedValue);
            In_ns.Binid = int.Parse(ddlbinStockadd.SelectedValue);
            In_ns.Qty = int.Parse(txtQuantity.Text);
            In_ns.Notes = txtnotesinpopup.Text;
            In_ns.Userid = sessionKeys.UID;
            INS.Add(In_ns);

            In_Ma = IM.GetAll().Where(o => o.Id == int.Parse(ddlProduct.SelectedValue)).FirstOrDefault();
            int qty = int.Parse(In_Ma.Qty.ToString());
            In_Ma.Qty = In_Ma.Qty + int.Parse(txtQuantity.Text);
            IM.Edit(In_Ma);

            In_mj.InventoryId = int.Parse(ddlProduct.SelectedValue);
            In_mj.ProductId = int.Parse(ddlProduct.SelectedValue);
            In_mj.SiteId = int.Parse(ddlsiteStockadd.SelectedValue);
            //In_mj.AreaId = int.Parse(ddlareaStockadd.SelectedValue);
            //In_mj.LocationId = int.Parse(ddllocationStockadd.SelectedValue);
            //In_mj.shelfId = int.Parse(ddlshelfStockadd.SelectedValue);
            //In_mj.Binid = int.Parse(ddlbinStockadd.SelectedValue);
            In_mj.Qty = qty + int.Parse(txtQuantity.Text);
            In_mj.SectionType = "Global";
            In_mj.PortfolioID = int.Parse(ddlCustomer.SelectedValue);
            In_mj.Notes = txtnotesinpopup.Text;
            In_mj.DateOrdered = DateTime.Now.Date;
            IMJ.Add(In_mj);



            lblmsgInpopup.ForeColor = System.Drawing.Color.Green;
            lblmsgInpopup.Text = "New stock added successfully";

            ddllocationStockadd.Items.Clear();
            ddlshelfStockadd.Items.Clear();
            ddlbinStockadd.Items.Clear();
            txtQuantity.Text = string.Empty;
            txtnotesinpopup.Text = string.Empty;
            BindSiteInPopup();
            BindAreaInPopup(int.Parse(ddlCustomer.SelectedValue));
            BindPopUpGridItems(int.Parse(ddlProduct.SelectedValue));
            BindProductsSection(ddlProduct.SelectedValue);
            // UpdatePanel2.DataBind();
            mdlpopupaddnewstock.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImgAddBin_Click(object sender, EventArgs e)
    {
        try
        {
            IB = new InventoryRepository<Inventory_Bin>();
            In_B = new Inventory_Bin();
            if (txtBinInPopup.Text.Trim() != string.Empty)
            {
                var count = IB.GetAll().Where(o => o.Name == txtBinInPopup.Text.Trim() && o.IS_id == int.Parse(ddlshelfStockadd.SelectedValue)).ToList();
                if (count.Count == 0)
                {
                    In_B.Name = txtBinInPopup.Text.Trim();
                    In_B.IS_id = int.Parse(ddlshelfStockadd.SelectedValue);
                    IB.Add(In_B);
                    BindBinInpopUp(int.Parse(ddlshelfStockadd.SelectedValue));
                    ddlbinStockadd.Items.FindByText(txtBinInPopup.Text.Trim()).Selected = true;
                    ddlenabledMethod(true);
                    lblmsgInpopup.ForeColor = System.Drawing.Color.Green;
                    lblmsgInpopup.Text = "Bin added successfully.";
                    CancelBtn();
                }
                else
                {
                    lblmsgInpopup.ForeColor = System.Drawing.Color.Red;
                    lblmsgInpopup.Text = "Already bin exist with this name.";
                }
            }
            else
            {
                lblmsgInpopup.ForeColor = System.Drawing.Color.Red;
                lblmsgInpopup.Text = "Please enter bin name.";
            }
            mdlpopupaddnewstock.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCaneclBin_Click(object sender, EventArgs e)
    {
        CancelBtn();
        mdlpopupaddnewstock.Show();
    }
    private void CancelBtn()
    {
        ddlenabledMethod(true);
        ddlbinStockadd.Visible = true;
        Imgbtnbinadd.Visible = true;
        ImgAddBin.Visible = false;
        btnCaneclBin.Visible = false;
        txtBinInPopup.Visible = false;
    }
    public void BindPopUpGridItems(int Pid)
    {
        try
        {
            IPP = new PortfolioRepository<ProjectPortfolio>();
            IA = new InventoryRepository<Inventory_Area>();
            IL = new InventoryRepository<Inventory_Locatin>();
            IS = new InventoryRepository<Inventory_Shelf>();
            IB = new InventoryRepository<Inventory_Bin>();
            INS = new InventoryRepository<Inventory_New_Stock>();
            IM = new InventoryRepository<InventoryManager>();
            ICntr = new InventoryRepository<UserMgt.Entity.Contractor>();

            idc = new InventoryDataContext();

            var arealist = IA.GetAll();
            var llist = IL.GetAll();
            var slist = IS.GetAll();
            var blist = IB.GetAll();
            var UserList = ICntr.GetAll().ToList();
            var sitelist = (from a in idc.Sites
                            join b in idc.AssignedSitesToPortfolios on a.ID equals b.SiteID
                            select new
                            {
                                id = b.SiteID,
                                Site = a.Site1
                            }).Distinct().ToList();
            var imlist = IM.GetAll().Where(o => o.Id == Pid).FirstOrDefault();
            var portfolioList = IPP.GetAll().ToList();
            var NewStocklist = INS.GetAll().Where(o => o.ProductId == Pid).ToList();
            var result = (from a in NewStocklist
                          select new
                          {
                              Id = a.id,
                              CustomerName = portfolioList.Where(o => o.ID == imlist.PortfolioID).Select(o => o.PortFolio).FirstOrDefault(),
                              ProductName = imlist.ItemDescription,
                              Sitename = sitelist.Where(o => o.id == a.SiteId).Select(o => o.Site).FirstOrDefault(),
                              AreaName = arealist.Where(o => o.id == a.AreaId).Select(o => o.Name).FirstOrDefault(),
                              LocationName = llist.Where(o => o.id == a.LocationId).Select(o => o.Name).FirstOrDefault(),
                              ShelfName = slist.Where(o => o.id == a.shelfId).Select(o => o.Name).FirstOrDefault(),
                              BinName = blist.Where(o => o.id == a.Binid).Select(o => o.Name).FirstOrDefault(),
                              Qty = a.Qty,
                              Notes = a.Notes,
                              UserName = UserList.Where(o => o.ID == a.Userid).Select(o => o.ContractorName).FirstOrDefault()
                          }).ToList();
            gridNewStockItems.DataSource = result;
            gridNewStockItems.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridNewStockItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            IM = new InventoryRepository<InventoryManager>();
            INS = new InventoryRepository<Inventory_New_Stock>();
            IMJ = new InventoryRepository<InventoryManagerJournal>();

            In_mj = new InventoryManagerJournal();
            in_m = new InventoryManager();

            In_ns = new Inventory_New_Stock();
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                var x = INS.GetAll().Where(o => o.id == id).FirstOrDefault();
                // var y = IM.GetAll().Where(o => o.Id == x.ProductId).FirstOrDefault();
                // int ActualQty =int.Parse(y.Qty.ToString());
                int DeleteQty = int.Parse(x.Qty.ToString());
                int ActualStock = getActualStackLaevel(x.ProductId.ToString());
                if (ActualStock > DeleteQty)
                {
                    In_ns = INS.GetAll().Where(o => o.id == id).FirstOrDefault();
                    INS.Delete(In_ns);

                    in_m = IM.GetAll().Where(o => o.Id == x.ProductId).FirstOrDefault();
                    in_m.Qty = in_m.Qty - DeleteQty;
                    IM.Edit(in_m);

                    In_mj = IMJ.GetAll().Where(o => o.InventoryId == x.ProductId).OrderByDescending(o => o.Id).FirstOrDefault();
                    In_mj.Qty = In_mj.Qty - DeleteQty;
                    IMJ.Edit(In_mj);

                    BindProductsSection(x.ProductId.ToString());

                    lblmsgInpopup.ForeColor = System.Drawing.Color.Green;
                    lblmsgInpopup.Text = "Deleted successfully.";
                    BindPopUpGridItems(int.Parse(ddlProduct.SelectedValue));
                }
                else
                {
                    lblmsgInpopup.ForeColor = System.Drawing.Color.Red;
                    lblmsgInpopup.Text = "You cann't delete this record...Stock is in use";
                }
            }
            mdlpopupaddnewstock.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridNewStockItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindPopUpGridItems(int.Parse(ddlProduct.SelectedValue));
        mdlpopupaddnewstock.Show();
    }
    protected void ddlshelf_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlshelf.SelectedValue != "0")
        {
            BindBin(int.Parse(ddlshelf.SelectedValue));
        }
        else
        {
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        mdlpopupaddnewstock.Hide();
        BindProductsSection(ddlProduct.SelectedValue);
    }
    protected void hlinkExecption_Click(object sender, EventArgs e)
    {
        int folderId = int.Parse(CreateFolder());
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('~/ProjectDocuments.aspx?mode=central&folderID=" + folderId + "&type=in','_newtab');", true);
        Response.Redirect("~/ProjectDocuments.aspx?mode=central&folderID=" + folderId + "&type=in");
        //  Session["behcode"] = 111;
        //   Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect", "Redirect('" + Session["behcode"].ToString() + "');", true);
    }
    //protected void ddlCustomerInsummary_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlCustomerInsummary.SelectedValue != "0")
    //        {
    //            bindSiteInSummary(int.Parse(ddlCustomerInsummary.SelectedValue));
    //            BindGridInSummary();
    //        }
    //        else
    //        {
    //            ddlSiteInSummary.Items.Clear();
    //            ddlSiteInSummary.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
    //            BindGridInSummary();
    //        }
    //        //ModelpopUpSummary.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    public void BindGridInSummary()
    {
        try
        {


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void ddlSiteInSummary_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //        BindGridInSummary();
    //        //ModelpopUpSummary.Show();
    //}
    protected void btnViewUsageSummary_Click(object sender, EventArgs e)
    {
        //ModelpopUpSummary.Show();
        //BindCustomerInSummary();
        BindGridInSummary();
    }
    public int getActualStackLaevelInSummary(string id)
    {
        int Sumvalue = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Sumvalue = (int)(from a in db.InventoryManagerJournals
                                 where a.InventoryId == int.Parse(id) && a.SectionType == "Global"
                                 orderby a.Id descending
                                 select a.Qty).FirstOrDefault();
                int ReplenishQty = GetQtyReplenishFromIm_PSDProducts(id);
                int InuseQty = GetQtyInUseFromIm_PSDProducts(id);
                int DisposeQty = GetDisposeQtyFromIm_PSDProducts(id);
                Sumvalue = Sumvalue + ReplenishQty - InuseQty - DisposeQty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Sumvalue;
    }

    protected void ddlcustomerInSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlcustomerInSearch.SelectedValue) > 0)
        {
            sessionKeys.PortfolioID = Convert.ToInt32(ddlcustomerInSearch.SelectedValue);
            sessionKeys.PortfolioName = ddlcustomerInSearch.SelectedItem.Text;
            InventoryGridBind();
            BindData_SearchUsageGrid();
            BindSiteInSearch();
        }
        else
        {
            ddlsiteInSearch.Items.Clear();
            ddlsiteInSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }
        //if (ddlcustomerInSearch.SelectedValue != "0")
        //{
        //    BindSiteInSearch();
        //}
        //else
        //{
        //    ddlsiteInSearch.Items.Clear();
        //    ddlsiteInSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        //}
    }














    public void BindSiteInSearch()
    {
        try
        {
            SC_cntMaterialCS = new SC_ContractorMaterial();
            //ddlSite.DataSource =  SC_cntMaterialCS.GetSites();
            ddlsiteInSearch.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(int.Parse(ddlcustomerInSearch.SelectedValue));
            ddlsiteInSearch.DataTextField = "Site";
            ddlsiteInSearch.DataValueField = "ID";
            ddlsiteInSearch.DataBind();
            ddlsiteInSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImageButtonBinEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlbin.SelectedValue != "0")
        {
            txtBinName.Text = ddlbin.SelectedItem.ToString();
            popupAdd.Show();
            lbladd.Text = "Edit Bin Name";
            hdForChecking.Value = "Edit";
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please select bin";
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int j = 111;
            IB = new InventoryRepository<Inventory_Bin>();
            if (hdForChecking.Value == "Add")
            {
                if (txtBinName.Text.Trim() != string.Empty)
                {
                    j = IB.GetAll().Where(o => o.Name == txtBinName.Text.Trim() && o.IS_id == int.Parse(ddlshelf.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_B = new Inventory_Bin();
                        In_B.IS_id = int.Parse(ddlshelf.SelectedValue);
                        In_B.Name = txtBinName.Text;
                        IB.Add(In_B);
                        popupAdd.Hide();
                        BindBin(int.Parse(ddlshelf.SelectedValue));
                        ddlbin.Items.FindByText(txtBinName.Text).Selected = true;
                        // lblerror1.ForeColor = System.Drawing.Color.Green;
                        // lblerror1.Text = "Bin added successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Bin added successfully.";
                        UpdatePanel2.Update();
                    }
                    else
                    {
                        popupAdd.Show();
                        lblerror1.ForeColor = System.Drawing.Color.Red;
                        lblerror1.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblerror1.ForeColor = System.Drawing.Color.Red;
                    lblerror1.Text = "Please enter bin name.";
                }
            }
            else if (hdForChecking.Value == "Edit")
            {
                if (txtBinName.Text.Trim() != string.Empty)
                {
                    j = IB.GetAll().Where(o => o.Name == txtBinName.Text.Trim() && o.IS_id == int.Parse(ddlshelf.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_B = IB.GetAll().Where(o => o.id == int.Parse(ddlbin.SelectedValue)).FirstOrDefault();
                        In_B.Name = txtBinName.Text.Trim();
                        IB.Edit(In_B);
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Bin updated successfully.";
                        BindBin(int.Parse(ddlshelf.SelectedValue));
                        ddlbin.Items.FindByText(txtBinName.Text.Trim()).Selected = true;
                        txtBinName.Text = string.Empty;
                        popupAdd.Hide();
                        UpdatePanel2.Update();
                    }
                    else
                    {
                        popupAdd.Show();
                        lblerror1.ForeColor = System.Drawing.Color.Red;
                        lblerror1.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblerror1.ForeColor = System.Drawing.Color.Red;
                    lblerror1.Text = "Please enter bin name.";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImageButtonbinAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlshelf.SelectedValue != "0")
        {
            txtBinName.Text = string.Empty;
            popupAdd.Show();
            lbladd.Text = "Add Bin Name";
            hdForChecking.Value = "Add";
        }
        else
        {
            UpdatePanel2.Update();
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please select shelf";
        }
    }
    protected void btnFieldConfigurator_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDropdown.aspx?Panel=9");
    }

    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {
        mdlpopupinGrid.Hide();
    }




    protected void btnViewInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
        //PnlSummary.Visible = false;
        //pnlInventory.Visible = true;
        //txtText.Text = string.Empty;
        //InventoryGridBind();
    }
    protected void btnViewUsageSummary0_Click(object sender, EventArgs e)
    {
        pnlAddProduct.Visible = false;
        pnlHistory.Visible = false;
        pnlUse.Visible = false;

        PnlSummary.Visible = true;
        pnlInventory.Visible = false;
        txtText.Text = string.Empty;
        //InventoryGridBind();
        BindData_SearchUsageGrid();

    }
    #region Usage Grid
    #region Inventory Usage
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (FilterdBatchGrid.Rows.Count > 0)
            {
                int lblBId = 0;
                int BatchQty = 0;
                int BatchAvaQty = 0;
                for (int i = 0; i < FilterdBatchGrid.Rows.Count; i++)
                {
                    GridViewRow row = FilterdBatchGrid.Rows[i];
                    CheckBox chkbox1 = (CheckBox)row.FindControl("checkedBatch");
                    if (chkbox1.Checked)
                    {
                        lblBId = int.Parse(((Label)row.FindControl("lblBatchId")).Text);
                        break;
                    }
                }
                InventoryDataContext Idc = new InventoryDataContext();
                string pid = lblEid.Text;
                string DeployQty = txtQtyUsed.Text;

                if (lblBId == 0)
                {
                    LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                    LblListviewMsg.Text = "Please select one batch.";
                }
                else
                {
                    BatchQty = (int)Idc.Inventory_Batches.Where(a => a.ID == lblBId).Select(a => a.QTY).FirstOrDefault();

                    if (lblBId == int.Parse(lblBatchId.Text))
                    {
                        BatchAvaQty = (int)Idc.InventoryManager_PSDProducts.Where(a => a.BatchID == lblBId && a.Id != int.Parse(pid)).Select(a => a.QtyUsed).Sum();
                    }
                    else
                    {
                        BatchAvaQty = (int)Idc.InventoryManager_PSDProducts.Where(a => a.BatchID == lblBId).Select(a => a.QtyUsed).Sum();
                    }
                    BatchAvaQty = BatchQty - BatchAvaQty;
                    if (BatchAvaQty >= int.Parse(DeployQty))
                    {
                        string Requseter = TxtRequester.Text;
                        string ProjectId = ddlProject.SelectedValue;
                        string status = ddlStatus.SelectedValue;
                        string Notes = txtNote.Text;


                        InventoryManager_PSDProduct IM_Products = new InventoryManager_PSDProduct();
                        IM_Products = (from a in Idc.InventoryManager_PSDProducts where a.Id == int.Parse(pid) select a).FirstOrDefault();
                        IM_Products.ProductId = int.Parse(ddlProduct.SelectedValue);
                        IM_Products.projectid = int.Parse(ProjectId);
                        IM_Products.SectionType = "PROJECT";
                        IM_Products.QtyUsed = int.Parse(DeployQty);
                        IM_Products.Requester = Requseter;
                        IM_Products.notes = Notes;
                        IM_Products.Status = int.Parse(status);
                        IM_Products.ModifiedBy = sessionKeys.UID;
                        IM_Products.ModifiedDate = DateTime.Now;
                        IM_Products.ConditionId = int.Parse(ddlcondition.SelectedValue);
                        IM_Products.BatchID = lblBId;

                        int MAxQtyValue = getActualStackLaevel(ddlProduct.SelectedValue);
                        MAxQtyValue = MAxQtyValue + int.Parse(lblQtyUsed.Text);
                        if (status != "3")
                        {
                            if (MAxQtyValue >= int.Parse(DeployQty))
                            {
                                Idc.SubmitChanges();
                                LblListviewMsg.Text = "Updated sucessfully";
                                saveUsageGridData(IM_Products.Id);
                                BindProductsSection(ddlProduct.SelectedValue);
                                mdlpopupinGrid.Hide();
                                FieldsClear();
                                BindData_UsageGrid();
                            }
                            else
                            {
                                LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                                LblListviewMsg.Text = "Sorry...U crossed max available QTY";
                                mdlpopupinGrid.Show();
                            }
                        }
                        else
                        {
                            Idc.SubmitChanges();
                            saveUsageGridData(IM_Products.Id);
                            LblListviewMsg.Text = "Updated sucessfully";
                            BindProductsSection(ddlProduct.SelectedValue);
                            mdlpopupinGrid.Hide();
                            FieldsClear();
                            BindData_UsageGrid();
                        }
                    }
                    else
                    {

                        LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                        LblListviewMsg.Text = "Insufficient Qty please select other batch";
                        mdlpopupinGrid.Show();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            pUsageCustomer.Controls.Clear();
            BindUsagePlaceholderFields();
            ddlStatus.DataSource = InventoryStatus();
            ddlStatus.DataTextField = "text";
            ddlStatus.DataValueField = "value";
            ddlStatus.DataBind();
            Bindproject();
            txtQtyUsed.Text = string.Empty;
            TxtRequester.Text = string.Empty;
            //   ddlProject.SelectedValue = "0";
            ddlStatus.SelectedValue = "0";
            txtNote.Text = string.Empty;
            Btninsert.Visible = true;
            btnupdate.Visible = false;
            //pUsageCustomer
            //BindUsagePlaceholderFields();
            mdlpopupinGrid.Show();
            Label6.Text = "Add Usage";
            BindBatchDropdown(Convert.ToInt32(ddlProduct.SelectedValue));

            BindCondtions();
            BindFilteredBatchGrid(ddlProduct.SelectedValue);
            lblProductidInPOPUP.Text = ddlProduct.SelectedValue.ToString();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindUsagePlaceholderFields()
    {
        IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
        IGFC = new InventoryRepository<GridFieldConfigurator>();
        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
        var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
        BindUseagePlaceholder(clist);
    }
    public void BindUseagePlaceholder(List<GridFieldConfigurator> clist)
    {
        try
        {
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            if (clist != null)
            {
                Table tbl = new Table();
                tbl.EnableViewState = true;

                TableRow tr = new TableRow();
                TableCell td = null;
                int i = 1;

                int cnt = 0;
                int jcnt = 1;
                int totalCnt = clist.Count();
                if (totalCnt != 1)
                    tbl.Style.Add("width", "100%");
                else
                    tbl.Style.Add("width", "50%");
                foreach (var c in clist)
                {
                    if (tr == null)
                        tr = new TableRow();

                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.DisplayName));
                    td.Style.Add("width", "27%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox(c.id.ToString(), string.Empty, string.Empty, Isfirsttime, string.Empty, false, c.DisplayName, string.Empty, string.Empty, string.Empty));
                    //if (c.TypeOfField.ToLower() == "date")
                    //{
                    //    td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                    //}
                    td.Style.Add("width", "73%");
                    tr.Cells.Add(td);
                    i++;
                    cnt = cnt + 1;
                    if (cnt == 2)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && cnt == 1)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
                pUsageCustomer.Controls.Add(tbl);
                //ph_batchcolumns.Controls.Add(tbl);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void Btninsert_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int lblBId = 0;
            int BatchQty = 0;
            int BatchAvaQty = 0;

            if (FilterdBatchGrid.Rows.Count > 0)
            {
                for (int i = 0; i < FilterdBatchGrid.Rows.Count; i++)
                {
                    GridViewRow row = FilterdBatchGrid.Rows[i];
                    CheckBox chkbox1 = (CheckBox)row.FindControl("checkedBatch");
                    if (chkbox1.Checked)
                    {
                        lblBId = int.Parse(((Label)row.FindControl("lblBatchId")).Text);
                        break;
                    }
                }
                string DeployQty = txtQtyUsed.Text;

                if (lblBId == 0)
                {
                    LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                    LblListviewMsg.Text = "Please select one batch.";
                }
                else
                {
                    idc = new InventoryDataContext();
                    BatchQty = (int)idc.Inventory_Batches.Where(a => a.ID == lblBId).Select(a => a.QTY).FirstOrDefault();
                    if (idc.InventoryManager_PSDProducts.Where(a => a.BatchID == lblBId).Count() > 0)
                        BatchAvaQty = idc.InventoryManager_PSDProducts.Where(a => (a.BatchID.HasValue ? a.BatchID.Value : 0) == lblBId).Select(a => a.QtyUsed.HasValue ? a.QtyUsed.Value : 0).Sum();
                    BatchAvaQty = BatchQty - BatchAvaQty;

                    if (BatchAvaQty >= int.Parse(DeployQty))
                    {
                        string Requseter = TxtRequester.Text;
                        string ProjectId = ddlProject.SelectedValue;
                        string status = ddlStatus.SelectedValue;
                        string Notes = txtNote.Text;
                        int MAxQtyValue = 0;
                        string Proid = string.Empty;
                        if (ddlProduct.SelectedValue != "0")
                        {
                            Proid = ddlProduct.SelectedValue;
                            hUsage.Value = string.Empty;
                        }
                        else
                        {
                            Proid = lblProductidInPOPUP.Text;
                            hUsage.Value = "POPUP";
                        }
                        MAxQtyValue = getActualStackLaevel(Proid);
                        InventoryManager_PSDProduct IM_Products = new InventoryManager_PSDProduct();
                        IM_Products.ProductId = int.Parse(Proid);
                        IM_Products.projectid = int.Parse(ProjectId);
                        IM_Products.SectionType = "PROJECT";
                        IM_Products.QtyUsed = int.Parse(DeployQty);
                        IM_Products.Requester = Requseter;
                        IM_Products.notes = Notes;
                        IM_Products.Status = int.Parse(status);
                        IM_Products.ModifiedBy = sessionKeys.UID;
                        IM_Products.ModifiedDate = DateTime.Now;
                        IM_Products.ConditionId = int.Parse(ddlcondition.SelectedValue);
                        IM_Products.BatchID = lblBId;


                        if (status != "3")
                        {
                            if (MAxQtyValue >= int.Parse(DeployQty))
                            {
                                using (InventoryDataContext Idc = new InventoryDataContext())
                                {
                                    Idc.InventoryManager_PSDProducts.InsertOnSubmit(IM_Products);
                                    Idc.SubmitChanges();

                                    int pid = IM_Products.Id;
                                    saveUsageGridData(pid);

                                }
                                //Listview.EditIndex = -1;
                                LblListviewMsg.Text = "Added sucessfully";
                                Response.Redirect(Request.RawUrl);
                                mdlpopupinGrid.Hide();
                                FieldsClear();
                                //Bindlistview();
                                //BindProductsSection(Proid);

                                if (!string.IsNullOrEmpty(hUsage.Value))
                                {
                                    InventoryGridBind();
                                    hUsage.Value = string.Empty;
                                    UpdatePanel2.Update();
                                }
                                else
                                {
                                    BindData_UsageGrid();
                                    BindProductsSection(Proid);
                                }
                            }
                            else
                            {
                                LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                                LblListviewMsg.Text = "Please check available quantity";
                            }

                        }
                        else
                        {
                            using (InventoryDataContext Idc1 = new InventoryDataContext())
                            {
                                Idc1.InventoryManager_PSDProducts.InsertOnSubmit(IM_Products);
                                Idc1.SubmitChanges();
                                //insert custom fields
                                saveUsageGridData(IM_Products.Id);
                                popupAdd.Hide();

                            }
                            //Listview.EditIndex = -1;
                            LblListviewMsg.Text = "Added sucessfully";
                            Response.Redirect(Request.RawUrl);
                            mdlpopupinGrid.Hide();
                            FieldsClear();

                            //Bindlistview();
                            if (!string.IsNullOrEmpty(hUsage.Value))
                            {
                                InventoryGridBind();
                                hUsage.Value = string.Empty;
                                UpdatePanel2.Update();
                            }
                            else
                            {
                                BindData_UsageGrid();
                                BindProductsSection(Proid);
                            }
                            Response.Redirect(Request.RawUrl);
                        }
                    }
                    else
                    {
                        LblListviewMsg.ForeColor = System.Drawing.Color.Red;
                        LblListviewMsg.Text = "Please check available quantity";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void saveUsageGridData(int pid)
    {
        try
        {
            ViewState["state"] = 2;
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
            InventoryManager_UsageCustomData iuc = null;
            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
            var dlist = IUCD.GetAll().Where(o => o.PSDID == pid).ToList();

            foreach (var v in clist)
            {
                if (pUsageCustomer.FindControl(v.id.ToString()) != null)
                {
                    var txt = pUsageCustomer.FindControl(v.id.ToString()) as TextBox;
                    //insert data 
                    if (dlist.Where(o => o.CustomFieldID == v.id).Count() == 0)
                    {
                        iuc = new InventoryManager_UsageCustomData();
                        iuc.CustomFieldID = v.id;
                        iuc.CustomFieldValue = txt.Text;
                        iuc.PSDID = pid;
                        IUCD.Add(iuc);
                    }
                    else
                    {
                        iuc = new InventoryManager_UsageCustomData();
                        iuc = dlist.Where(o => o.CustomFieldID == v.id).FirstOrDefault();
                        iuc.CustomFieldID = v.id;
                        iuc.CustomFieldValue = txt.Text;
                        iuc.PSDID = pid;
                        IUCD.Edit(iuc);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void FieldsClear()
    {
        txtQtyUsed.Text = string.Empty;
        TxtRequester.Text = string.Empty;
        ddlProject.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        txtNote.Text = string.Empty;
    }
    public void Bindproject()
    {
        try
        {
            using (projectTaskDataContext timeSheet = new projectTaskDataContext())
            {
                ddlProject.DataSource = (from r in timeSheet.ProjectDetails
                                         where r.ProjectStatusID == 2 && r.Portfolio == int.Parse(ddlcustomerInSearch.SelectedValue)
                                         orderby r.ProjectTitle
                                         select new
                                         {
                                             value = r.ProjectReference.ToString(),
                                             name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle.ToString()
                                         }).ToList();
                ddlProject.DataValueField = "value";
                ddlProject.DataTextField = "name";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridUsage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var v1 = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            var cnt = e.Row.Cells.Count;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[cnt - 1].Visible = false;
                for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                {
                    var v11 = v1.Where(o => o.Position == (i)).FirstOrDefault();
                    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[cnt - 1].Visible = false;
                for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                {
                    var v11 = v1.Where(o => o.Position == (i)).FirstOrDefault();
                    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindData_UsageGrid()
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
            IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
            var plist = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlCustomer.SelectedValue)).OrderBy(o => o.Position).ToList();

            var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == int.Parse(ddlProduct.SelectedValue)).ToList();
            var projectids = InList.Select(o => o.projectid).ToArray();
            var pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();

            var projectlist = pRepository.GetAll().Where(o => projectids.Contains(o.ProjectReference)).ToList();
            DataTable dt = new DataTable();

            foreach (var p in plist)
            {
                dt.Columns.Add(p.DisplayName, typeof(string));
            }
            //plist.Add(new GridFieldConfigurator() { DisplayName = "Visibility_id", DeafaultName = "Visibility_id" });
            //dt.Columns.Add("Visibility_id", typeof(string));
            plist.Add(new GridFieldConfigurator() { DisplayName = "_id", DeafaultName = "_id" });
            dt.Columns.Add("_id", typeof(string));
            DataRow datarw;
            foreach (var d in InList)
            {
                datarw = dt.NewRow();
                int i = 0;
                foreach (var p in plist)
                {
                    // datarw[i] = d.Id.ToString();
                    if (p.DisplayName == "Qty Used")
                    {
                        datarw[i] = d.QtyUsed.Value.ToString();
                    }
                    else if (p.DisplayName == "_id")
                    {
                        datarw[i] = d.Id.ToString();
                    }
                    else if (p.DisplayName == "Requester")
                    {
                        datarw[i] = d.Requester.ToString();
                    }
                    else if (p.DisplayName == "Status")
                    {
                        datarw[i] = InventoryStatus().Where(o => o.Value == Convert.ToString(d.Status)).FirstOrDefault().ToString();
                    }
                    else if (p.DisplayName == "Project")
                    {
                        datarw[i] = projectlist.Where(o => o.ProjectReference == d.projectid).Select(o => o.ProjectReferenceWithPrefix + "-" + o.ProjectTitle).FirstOrDefault();
                    }
                    else if (p.DisplayName == "Notes")
                    {
                        datarw[i] = d.notes.ToString();
                    }
                    else
                    {
                        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
                        var newPlist = plist.Where(o => !strs.Contains(o.DeafaultName) && o.DisplayName == p.DisplayName).ToList();
                        if (newPlist != null)
                        {
                            foreach (var l in newPlist)
                            {
                                // var pids = InList.Select(o => o.Id).ToArray();
                                var cuslist = IUCD.GetAll().Where(o => o.PSDID == d.Id && o.GridFieldConfigurator.DisplayName == p.DisplayName).ToList();
                                if (cuslist != null)
                                {
                                    datarw[i] = cuslist.Select(o => o.CustomFieldValue).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            datarw[i] = string.Empty;
                        }
                    }
                    i++;
                }
                dt.Rows.Add(datarw);
            }
            GridUsage.DataSource = dt;
            GridUsage.DataBind();
            UpdatePanel2.Update();
            upanleUsage.Update();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindColumns_UsageGrid()
    {
        if (sessionKeys.PortfolioID > 0)
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var plist = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            foreach (var p in plist)
            {
                var bfield = new BoundField();
                bfield.HeaderText = p.DisplayName;
                bfield.DataField = p.DisplayName;
                GridUsage.Columns.Add(bfield);
            }
        }

    }

    public void BindData_SearchUsageGrid()
    {
        try
        {
            IinventoryRepository<InventoryMgt.Entity.InventoryManager> imRepository = new InventoryRepository<InventoryMgt.Entity.InventoryManager>();
            List<InventoryManager> imList = new List<InventoryManager>();
            if (sessionKeys.PortfolioID > 0)
            {
                imList = imRepository.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
            }
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
            IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();

            var plist = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            //var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.projectid > 0).ToList();
            List<InventoryManager_PSDProduct> InList = new List<InventoryManager_PSDProduct>();
            if (imList.Count > 0)
                InList = IMPSD.GetAll().Where(o => imList.Select(p => p.Id).ToArray().Contains(o.ProductId.HasValue ? o.ProductId.Value : 0)).ToList();
            else
                InList = IMPSD.GetAll().ToList();
            var cDatalist = IUCD.GetAll().ToList();

            var searchtext = txtText.Text.ToLower().Trim();

            if (searchtext != string.Empty)
            {
                string[] Svalue1 = searchtext.Split(' ');

                cDatalist = (from s in cDatalist
                             where (Svalue1.Any(s1 => s.CustomFieldValue.ToLower().Contains(s1)))
                             select s).ToList();

                var psids = cDatalist.Select(o => o.PSDID).ToArray();

                InList = (from s in InList
                          where (Svalue1.Any(s1 =>
                          (psids != null && psids.Contains(s.Id))
                          || (s.notes != null && s.notes.ToLower().Contains(s1))
                          || (s.Requester != null && s.Requester.ToLower().Contains(s1))
                          || (s.projectid.HasValue && s.projectid.Value.ToString().Contains(s1.Replace(sessionKeys.Prefix.ToLower(), string.Empty)))
                          || (s.Status != null && InventoryStatus().Where(o => o.Value.Contains(s.Status.HasValue ? s.Status.Value.ToString() : "0")).Select(o => o.Text.ToLower().Contains(s1)).FirstOrDefault())))
                          select s).OrderByDescending(o => o.Id).ToList();
            }
            var projectids = InList.Select(o => o.projectid).ToArray();
            var pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();

            var projectlist = pRepository.GetAll().Where(o => projectids.Contains(o.ProjectReference)).ToList();
            DataTable dt = new DataTable();


            foreach (var p in plist)
            {
                dt.Columns.Add(p.DisplayName, typeof(string));
            }

            DataRow datarw;
            foreach (var d in InList)
            {
                datarw = dt.NewRow();
                int i = 0;
                foreach (var p in plist)
                {
                    if (p.DisplayName == "Qty Used")
                    {
                        datarw[i] = d.QtyUsed.HasValue ? d.QtyUsed.Value.ToString() : string.Empty; ;
                    }
                    else if (p.DisplayName == "Requester")
                    {
                        datarw[i] = (d.Requester != null) ? d.Requester : string.Empty;
                    }
                    else if (p.DisplayName == "Status")
                    {
                        datarw[i] = d.Status != null ? InventoryStatus().Where(o => o.Value == Convert.ToString(d.Status)).FirstOrDefault().ToString() : string.Empty;
                    }
                    else if (p.DisplayName == "Project")
                    {
                        datarw[i] = projectlist != null ? (projectlist.Where(o => o.ProjectReference == d.projectid).FirstOrDefault() != null ? projectlist.Where(o => o.ProjectReference == d.projectid).Select(o => o.ProjectReferenceWithPrefix + "-" + o.ProjectTitle).FirstOrDefault() : string.Empty) : string.Empty;
                    }
                    else if (p.DisplayName == "Notes")
                    {
                        datarw[i] = (d.notes != null) ? d.notes : string.Empty;
                    }

                    else
                    {
                        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
                        var newPlist = plist.Where(o => !strs.Contains(o.DeafaultName) && o.DisplayName == p.DisplayName).ToList();
                        if (newPlist != null)
                        {
                            foreach (var l in newPlist)
                            {
                                // var pids = InList.Select(o => o.Id).ToArray();
                                var cuslist = IUCD.GetAll().Where(o => o.PSDID == d.Id && o.GridFieldConfigurator.DisplayName == p.DisplayName).ToList();
                                if (cuslist != null)
                                {
                                    datarw[i] = cuslist.Select(o => o.CustomFieldValue).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            datarw[i] = string.Empty;

                        }


                    }

                    i++;
                }
                dt.Rows.Add(datarw);
            }

            GridUsageSummary1.DataSource = dt;
            GridUsageSummary1.DataBind();
            if (GridUsageSummary1.Rows.Count > 0)
            {
                //var cnt = GridUsageSummary1.Columns.Count;
                //GridUsageSummary1.Columns[0].HeaderStyle.CssClass = "header_bg_l";
                //GridUsageSummary1.Columns[cnt-1].HeaderStyle.CssClass = "header_bg_r";

            }
            UpdatePanel2.Update();
            UpdatePnlSummary.Update();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindColumns_SearchUsageGrid()
    {
        if (sessionKeys.PortfolioID > 0)
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var plist = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            foreach (var p in plist)
            {
                var bfield = new BoundField();
                bfield.HeaderText = p.DisplayName;
                bfield.DataField = p.DisplayName;
                GridUsageSummary1.Columns.Add(bfield);
            }
        }

    }
    #endregion
    public void SettingDataToPlaceHolderInPopUp(int id)
    {
        try
        {
            IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();

            foreach (var x in clist)
            {
                InventoryManager_UsageCustomData DataList = IUCD.GetAll().Where(o => o.CustomFieldID == x.id && o.PSDID == id).FirstOrDefault();
                if (DataList != null)
                {
                    var txt = pUsageCustomer.FindControl(x.id.ToString()) as TextBox;
                    if (txt != null)
                    {
                        txt.Text = DataList.CustomFieldValue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridUsage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit1")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                pUsageCustomer.Controls.Clear();
                BindUsagePlaceholderFields();
                SettingDataToPlaceHolderInPopUp(id);
                mdlpopupinGrid.Show();
                Label6.Text = "Edit Record";
                btnupdate.Visible = true;
                Btninsert.Visible = false;
                //hide fields
                HideFields();
                using (InventoryDataContext Idc = new InventoryDataContext())
                {
                    using (projectTaskDataContext pdc = new projectTaskDataContext())
                    {
                        var plist = pdc.ProjectDetails.Where(a => a.ProjectStatusID == 2).ToList();
                        var InList = Idc.InventoryManager_PSDProducts.Where(a => a.SectionType == "PROJECT").ToList();
                        var VList = (from a in InList
                                     join b in plist on a.projectid equals b.ProjectReference
                                     where a.SectionType == "PROJECT" && a.ProductId == int.Parse(ddlProduct.SelectedValue)
                                     select new
                                     {
                                         productId = a.ProductId,
                                         pid = a.projectid,
                                         projectid = b.ProjectReferenceWithPrefix + " - " + b.ProjectTitle,
                                         Id = a.Id,
                                         QtyUsed = a.QtyUsed,
                                         Requester = a.Requester,
                                         Status = a.Status,
                                         notes = a.notes,
                                         batchid = a.BatchID,
                                         ConditionId = a.ConditionId
                                     }).ToList();
                        VList = VList.Where(o => o.Id == id).ToList();
                        if (VList != null)
                        {
                            ddlBatch.SelectedValue = VList.Select(o => o.batchid).FirstOrDefault().HasValue ? VList.Select(o => o.batchid).FirstOrDefault().Value.ToString() : "0";
                            txtQtyUsed.Text = VList.Select(o => o.QtyUsed).FirstOrDefault().Value.ToString();
                            TxtRequester.Text = VList.Select(o => o.Requester).FirstOrDefault().ToString();
                            Bindproject();
                            ddlProject.SelectedValue = VList.Select(o => o.pid).FirstOrDefault().ToString();
                            ddlStatus.DataSource = InventoryStatus();
                            ddlStatus.DataTextField = "text";
                            ddlStatus.DataValueField = "value";
                            ddlStatus.DataBind();
                            ddlStatus.SelectedValue = VList.Select(o => o.Status).FirstOrDefault().ToString();
                            txtNote.Text = VList.Select(o => o.notes).FirstOrDefault().ToString();
                            lblEid.Text = VList.Select(o => o.Id).FirstOrDefault().ToString();
                            LblProductId.Text = VList.Select(o => o.productId).FirstOrDefault().ToString();
                            LblPid.Text = VList.Select(o => o.pid).FirstOrDefault().ToString();
                            lblQtyUsed.Text = VList.Select(o => o.QtyUsed).FirstOrDefault().Value.ToString();
                            lblBatchId.Text = Convert.ToString(VList.Select(o => o.batchid).FirstOrDefault());


                            BindCondtions();
                            ddlcondition.SelectedValue = VList.Select(o => o.ConditionId).FirstOrDefault().HasValue ? VList.Select(o => o.ConditionId).FirstOrDefault().Value.ToString() : "0";
                            BindFilteredBatchGrid(VList.Select(o => o.productId).FirstOrDefault().ToString());

                            if (FilterdBatchGrid.Rows.Count > 0)
                            {
                                for (int i = 0; i < FilterdBatchGrid.Rows.Count; i++)
                                {
                                    GridViewRow row = FilterdBatchGrid.Rows[i];
                                    int lblBId = int.Parse(((Label)row.FindControl("lblBatchId")).Text);
                                    if (lblBId == VList.Select(o => o.batchid).FirstOrDefault())
                                    {
                                        CheckBox chkbox1 = (CheckBox)row.FindControl("checkedBatch");
                                        chkbox1.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
               
            }
            if (e.CommandName == "Delete1")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                IMCD = new InventoryRepository<InventoryManager_UsageCustomData>();
                IM_ucd_List = new List<InventoryManager_UsageCustomData>();
                IM_ucd_List = IMCD.GetAll().Where(o => o.PSDID == id).ToList();
                IMCD.DeleteAll(IM_ucd_List);
                using (InventoryDataContext Idc = new InventoryDataContext())
                {
                    InventoryManager_PSDProduct IM_Products = new InventoryManager_PSDProduct();
                    IM_Products = (from a in Idc.InventoryManager_PSDProducts where a.Id == id select a).FirstOrDefault();
                    Idc.InventoryManager_PSDProducts.DeleteOnSubmit(IM_Products);
                    Idc.SubmitChanges();
                }
                Label5.Text = "Deleted sucessfully";
                BindData_UsageGrid();
                BindProductsSection(ddlProduct.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridUsage_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridUsage_PreRender(object sender, EventArgs e)
    {

    }
    protected void GridUsageSummary1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var v1 = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    protected void ddlcustomerInNewItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        sessionKeys.PortfolioID = int.Parse(ddlcustomerInNewItem.SelectedValue);
        BindSiteInNewItem();
        BindAreaInNewItem(sessionKeys.PortfolioID);
        BindCategoryInNewItem(int.Parse(ddlcustomerInNewItem.SelectedValue));
        txtBatchreference.Text = GetBatchReference(0).ToString();
        //Bind place holder values
        ph_batchcolumns.Controls.Clear();
        BindBatchPlaceholderFields();
        UpdatePanel_AddItem.Update();
    }
    protected void ddlcategoryInNewItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategoryInNewItem(int.Parse(ddlcategoryInNewItem.SelectedValue));
        ph_batchcolumns.Controls.Clear();
        BindBatchPlaceholderFields();
        UpdatePanel_AddItem.Update();
    }
    protected void btnSaveInNewItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (hid_batchid.Value != "0")
            {
                var bRepository = new InventoryRepository<Inventory_Batch>();
                var bcRepository = new InventoryRepository<Inventory_BatchCustomData>();

                var bEntity = bRepository.GetAll().Where(o => o.ID == Convert.ToInt32(hid_batchid.Value)).FirstOrDefault();
                var ProductID = bEntity.InventoryID;
                if (bEntity.QTY != Convert.ToInt32(txtQtyEdit.Text.Trim()))
                {
                    //Update inventory
                    IM = new InventoryRepository<InventoryManager>();
                    In_Ma = IM.GetAll().Where(o => o.Id == bEntity.InventoryID).FirstOrDefault();
                    int qty = int.Parse(In_Ma.Qty.ToString());
                    In_Ma.Qty = In_Ma.Qty + Convert.ToInt32(txtQtyEdit.Text.Trim()) - bEntity.QTY;
                    IM.Edit(In_Ma);
                    //Update journal
                    IMJ = new InventoryRepository<InventoryManagerJournal>();
                    In_mj = new InventoryManagerJournal();
                    In_mj.InventoryId = ProductID;
                    In_mj.ProductId = ProductID;
                    In_mj.Qty = In_Ma.Qty;
                    In_mj.SectionType = "Global";
                    In_mj.PortfolioID = sessionKeys.PortfolioID;
                    In_mj.DateOrdered = DateTime.Now.Date;
                    IMJ.Add(In_mj);

                    //Update batch data
                    bEntity.QTY = Convert.ToInt32(txtQtyEdit.Text.Trim());
                    bEntity.SiteId = Convert.ToInt32(ddlsiteInNewItem.SelectedValue);
                    bEntity.Area = Convert.ToInt32(ddlareaInNewItem.SelectedValue);
                    bEntity.Location = Convert.ToInt32(ddlLocationInNewItem.SelectedValue);
                    bEntity.Shelf = Convert.ToInt32(ddlShelfInNewItem.SelectedValue);
                    bEntity.Bin = Convert.ToInt32(ddlBinInNewItem.SelectedValue);
                    bEntity.AddedBy = sessionKeys.UID;
                    bRepository.Edit(bEntity);
                }
                //Save Custom data
                saveBatchCustom(Convert.ToInt32(txtBatchreference.Text.Trim()), Convert.ToInt32(ProductID));
                //Update batch custom fields
                //ph_batchcolumns.Controls.Clear();
                //BindBatchPlaceholderFields(bEntity);
                BindBatchGrid();
            }
            else if (hid_batchproduct.Value != "0")
            {
                try
                {
                    int ProductID = Convert.ToInt32(hid_batchproduct.Value);
                    int Qty = Convert.ToInt32(txtQtyEdit.Text.Trim());
                    Inventory_Batch_Add(ProductID, Qty);
                    saveBatchCustom(Convert.ToInt32(txtBatchreference.Text.Trim()), Convert.ToInt32(ProductID));

                    IMJ = new InventoryRepository<InventoryManagerJournal>();
                    INS = new InventoryRepository<Inventory_New_Stock>();
                    IM = new InventoryRepository<InventoryManager>();
                    In_mj = new InventoryManagerJournal();
                    In_ns = new Inventory_New_Stock();
                    In_Ma = new InventoryManager();

                    In_ns.ProductId = Convert.ToInt32(hid_batchproduct.Value);
                    In_ns.SiteId = 0;
                    In_ns.AreaId = 0;
                    In_ns.LocationId = 0;
                    In_ns.shelfId = 0;
                    In_ns.Binid = 0;
                    In_ns.Qty = Qty;
                    In_ns.Userid = sessionKeys.UID;
                    INS.Add(In_ns);

                    In_Ma = IM.GetAll().Where(o => o.Id == ProductID).FirstOrDefault();
                    int qty = int.Parse(In_Ma.Qty.ToString());
                    In_Ma.Qty = In_Ma.Qty + Qty;
                    IM.Edit(In_Ma);

                    In_mj.InventoryId = ProductID;
                    In_mj.ProductId = ProductID;
                    In_mj.Qty = In_Ma.Qty;
                    In_mj.SectionType = "Global";
                    In_mj.PortfolioID = sessionKeys.PortfolioID;
                    In_mj.DateOrdered = DateTime.Now.Date;
                    IMJ.Add(In_mj);


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            else
            {
                //int  inventoryID = db.InventoryManager_Insert(ddlsiteInNewItem.SelectedValue, 0, 0, 0, "Global", dd, SubCategory,
                //                                PortfolioID, 0,null,null, TxtproductInNewItem.Text.Trim(), 0, 0,
                //                                null, 0, DateTime.Now, 0, string.Empty, 0, 0, 0, 0);
                IMCS = new InventoryManagerCs();
                DateTime DateOrdered = Convert.ToDateTime(string.IsNullOrEmpty(txtDateOrdered.Text.Trim()) ? "01/01/1900" : txtDateOrdered.Text.Trim()); //Convert.ToDateTime(txtDateOrdered.Text.Trim());

                int inventoryID = 0;
                using (InventoryDataContext db = new InventoryDataContext())
                {

                    inventoryID = db.InventoryManager_Insert(Convert.ToInt32(ddlsiteInNewItem.SelectedValue), 0, Convert.ToInt32(txtQtyNewItem.Text.Trim()),
                        0, "Global", Convert.ToInt32(ddlcategoryInNewItem.SelectedValue), Convert.ToInt32(ddlSubcategoryInNewItem.SelectedValue),
                      Convert.ToInt32(ddlcustomerInNewItem.SelectedValue), 0, null, 0, TxtproductInNewItem.Text.Trim(), 0, string.Empty,
                      Guid.Empty, 0, DateOrdered, string.Empty, string.Empty, int.Parse(ddlareaInNewItem.SelectedValue),
                      Convert.ToInt32(ddlLocationInNewItem.SelectedValue), Convert.ToInt32(ddlShelfInNewItem.SelectedValue), Convert.ToInt32(ddlBinInNewItem.SelectedValue));
                    //retval = IMCS.InsertInventoryProduct(SiteId, 0, QTY, ReorderLevel, "Global", Category, SubCategory,
                    //    PortfolioID, QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, PartNumber, Image1, Supplier, Manufacturer, DateOrdered,barcode);


                    if (inventoryID > 0)
                    {
                        //Batch reference
                        var ib = Inventory_Batch_Add(inventoryID, Convert.ToInt32(txtQtyNewItem.Text.Trim()));
                        //Save Custome Data
                        saveBatchCustom(ib.BatchRef.Value, inventoryID);
                        InventoryMgt.Entity.InventoryManager inventoryManager = db.InventoryManagers.Where(m => m.Id == inventoryID).Select(m => m).FirstOrDefault();
                        if (inventoryManager != null)
                        {
                            //Product items insert
                            AddInventorySiteStorageDetails(inventoryID, Convert.ToInt32(ddlcategoryInNewItem.SelectedValue), Convert.ToInt32(ddlSubcategoryInNewItem.SelectedValue), Convert.ToInt32(ddlsiteInNewItem.SelectedValue), Convert.ToInt32(txtQtyNewItem.Text.Trim()));
                            //Inventory Journal Insert
                            InventoryBAL.InsertInventoryJournal(inventoryManager, 0, "", 0, Convert.ToInt32(inventoryManager.Qty), 0);
                        }
                    }
                }
            }
            mdlpopupForNewItem.Hide();
            InventoryGridBind(true);
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    protected void BtncancelInNewItem_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    #region Inventory Batch
    IinventoryRepository<InventoryMgt.Entity.Inventory_Batch> batchRepository = null;
    IinventoryRepository<InventoryMgt.Entity.Inventory_BatchCustomData> batchCustomRepository = null;
    public void BindBatchPlaceholderFields(Inventory_Batch bEntity)
    {
        IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
        IGFC = new InventoryRepository<GridFieldConfigurator>();
        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
        var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
        var bcRepository = new InventoryRepository<Inventory_BatchCustomData>();
        var bclist = bcRepository.GetAll().Where(o => o.BatchID == bEntity.ID).ToList();
        if (clist.Count > 0)
        {
            pnlBatch.Visible = true;
            BindBatchPlaceholder(clist, bclist);
        }
    }
    public void BindBatchPlaceholder(List<GridFieldConfigurator> clist, List<Inventory_BatchCustomData> bcList)
    {
        try
        {
            bool Isfirsttime = false;
            if (ViewState["bstate"] == null)
            {
                ViewState["bstate"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            if (clist != null)
            {
                Table tbl = new Table();
                tbl.EnableViewState = true;

                TableRow tr = new TableRow();
                TableCell td = null;
                int i = 1;

                int cnt = 0;
                int jcnt = 1;
                int totalCnt = clist.Count();
                if (totalCnt != 1)
                    tbl.Style.Add("width", "100%");
                else
                    tbl.Style.Add("width", "50%");
                foreach (var c in clist)
                {
                    if (tr == null)
                        tr = new TableRow();

                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.DisplayName));
                    //td.Style.Add("width", "27%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox("B" + c.id.ToString(), bcList.Where(o => o.CustomFieldID == c.id).FirstOrDefault() != null ? bcList.Where(o => o.CustomFieldID == c.id).FirstOrDefault().CustomFieldValue : string.Empty, string.Empty, Isfirsttime, string.Empty, false, c.DisplayName, string.Empty, string.Empty, string.Empty));
                    //if (c.TypeOfField.ToLower() == "date")
                    //{
                    //    td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                    //}
                    //td.Style.Add("width", "73%");
                    tr.Cells.Add(td);
                    i++;
                    cnt = cnt + 1;

                    if (cnt == 3)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && (cnt == 1 || cnt == 2))
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
                ph_batchcolumns.Controls.Add(tbl);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBatchPlaceholderFields()
    {
        IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
        IGFC = new InventoryRepository<GridFieldConfigurator>();
        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
        var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
        if (clist.Count > 0)
        {
            pnlBatch.Visible = true;
            BindBatchPlaceholder(clist);
        }
    }
    public void BindBatchPlaceholder(List<GridFieldConfigurator> clist)
    {
        try
        {
            bool Isfirsttime = false;
            if (ViewState["bstate"] == null)
            {
                ViewState["bstate"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            if (clist != null)
            {
                Table tbl = new Table();
                tbl.EnableViewState = true;

                TableRow tr = new TableRow();
                TableCell td = null;
                int i = 1;

                int cnt = 0;
                int jcnt = 1;
                int totalCnt = clist.Count();
                if (totalCnt != 1)
                    tbl.Style.Add("width", "100%");
                else
                    tbl.Style.Add("width", "50%");
                foreach (var c in clist)
                {
                    if (tr == null)
                        tr = new TableRow();

                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.DisplayName));
                    //td.Style.Add("width", "27%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox("B" + c.id.ToString(), string.Empty, string.Empty, Isfirsttime, string.Empty, false, c.DisplayName, string.Empty, string.Empty, string.Empty));
                    //if (c.TypeOfField.ToLower() == "date")
                    //{
                    //    td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                    //}
                    //td.Style.Add("width", "73%");
                    tr.Cells.Add(td);
                    i++;
                    cnt = cnt + 1;
                    if (cnt == 3)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && (cnt == 1 || cnt == 2))
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
                ph_batchcolumns.Controls.Add(tbl);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void saveBatchCustom(int batchid, int inventoryID)
    {
        try
        {
            ViewState["bstate"] = 2;
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            batchCustomRepository = new InventoryRepository<Inventory_BatchCustomData>();
            batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
            Inventory_BatchCustomData iuc = null;
            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
            var batchDetails = batchRepository.GetAll().Where(o => o.BatchRef == batchid && o.InventoryID == inventoryID).FirstOrDefault();
            if (batchDetails != null)
            {
                var dlist = batchCustomRepository.GetAll().Where(o => o.BatchID == batchDetails.ID).ToList();
                foreach (var v in clist)
                {
                    if (ph_batchcolumns.FindControl("B" + v.id.ToString()) != null)
                    {
                        var txt = ph_batchcolumns.FindControl("B" + v.id.ToString()) as TextBox;
                        //insert data 
                        if (dlist.Where(o => o.CustomFieldID == v.id).Count() == 0)
                        {
                            iuc = new Inventory_BatchCustomData();
                            iuc.CustomFieldID = v.id;
                            iuc.CustomFieldValue = txt.Text;
                            iuc.BatchID = batchDetails.ID;
                            batchCustomRepository.Add(iuc);
                        }
                        else
                        {
                            iuc = new Inventory_BatchCustomData();
                            iuc = dlist.Where(o => o.CustomFieldID == v.id).FirstOrDefault();
                            iuc.CustomFieldID = v.id;
                            iuc.CustomFieldValue = txt.Text;
                            iuc.BatchID = batchDetails.ID;
                            batchCustomRepository.Edit(iuc);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public int GetBatchReference(int inventoryID)
    {
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        int cnt = 1;
        var iv_batch = batchRepository.GetAll().Where(o => o.InventoryID == inventoryID).ToList();
        if (iv_batch != null)
        {
            cnt = iv_batch.Count() + 1;
        }
        return cnt;
    }
    public Inventory_Batch Inventory_Batch_Add(int inventoryID, int Qty)
    {
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        Inventory_Batch ib = new Inventory_Batch();
        ib.InventoryID = inventoryID;
        ib.QTY = Qty;
        ib.DateReceived = DateTime.Now;
        int ibref = GetBatchReference(inventoryID);
        ib.BatchRef = ibref;
        ib.BatchDisplayName = "Batch Reference " + ibref.ToString();
        ib.AddedBy = sessionKeys.UID;
        ib.SiteId = Convert.ToInt32(ddlsiteInNewItem.SelectedValue);
        ib.Area = Convert.ToInt32(ddlareaInNewItem.SelectedValue);
        ib.Location = Convert.ToInt32(ddlLocationInNewItem.SelectedValue);
        ib.Shelf = Convert.ToInt32(ddlShelfInNewItem.SelectedValue);
        ib.Bin = Convert.ToInt32(ddlBinInNewItem.SelectedValue);
        batchRepository.Add(ib);
        return ib;
    }

    public List<Inventory_Batch> Inventory_Batch_SelectByInventoryID(int inventoryID)
    {
        List<Inventory_Batch> batchList = new List<Inventory_Batch>();
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        batchList = batchRepository.GetAll().Where(o => o.InventoryID == inventoryID).ToList();
        return batchList;
    }
    public Inventory_Batch Inventory_Batch_SelectByBatchID(int BatchID, int inventoryID)
    {
        Inventory_Batch batchEntity = new Inventory_Batch();
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        batchEntity = batchRepository.GetAll().Where(o => o.BatchRef == BatchID && o.InventoryID == inventoryID).FirstOrDefault();
        return batchEntity;
    }
    public List<Inventory_BatchCustomData> Inventory_Batch_CustomData_SelectByBatchID(int BatchID, int inventoryID)
    {
        List<Inventory_BatchCustomData> batchCustomList = new List<Inventory_BatchCustomData>();
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        var ib = batchRepository.GetAll().Where(o => o.BatchRef == BatchID && o.InventoryID == inventoryID).FirstOrDefault();
        batchCustomRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_BatchCustomData>();
        if (ib != null)
            batchCustomList = batchCustomRepository.GetAll().Where(o => o.BatchID == ib.ID).ToList();

        return batchCustomList;
    }
    public List<Inventory_BatchCustomData> Inventory_Batch_CustomData_SelectByInventoryID(int inventoryID)
    {
        List<Inventory_BatchCustomData> batchCustomList = new List<Inventory_BatchCustomData>();
        batchRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        var ib = batchRepository.GetAll().Where(o => o.InventoryID == inventoryID).FirstOrDefault();
        batchCustomRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_BatchCustomData>();
        if (ib != null)
            batchCustomList = batchCustomRepository.GetAll().Where(o => o.BatchID == ib.ID).ToList();

        return batchCustomList;
    }
    public void BindFilteredBatchGrid(string Productid)
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var ibatchRepository = new InventoryRepository<Inventory_Batch>();
            var ibatchCustomRepository = new InventoryRepository<Inventory_BatchCustomData>();
            IinventoryRepository<InventoryMgt.Entity.InventoryManager> imRepository = new InventoryRepository<InventoryMgt.Entity.InventoryManager>();

            var plist = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
            //var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == int.Parse(ddlProduct.SelectedValue)).ToList();
            var bList = ibatchRepository.GetAll().Where(o => o.InventoryID == int.Parse(Productid)).ToList();
            //get batch custom fields

            IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
            PSD = new InventoryManager_PSDProduct();
            PSDList = new List<InventoryManager_PSDProduct>();
            PSDList = IMPSD.GetAll().ToList();



            //search condition
            if (txtBatchSearch.Text.Trim() != string.Empty)
            {
                using (InventoryDataContext Idc = new InventoryDataContext())
                {
                    var BatchDataList = Idc.Inventory_BatchCustomDatas.ToList();
                    string[] Sname = txtBatchSearch.Text.Trim().ToLower().Split(' ');
                    bList = (from a in bList
                             join b in BatchDataList on a.ID equals b.BatchID
                             where Sname.Any(c => a.BatchDisplayName.ToLower().Contains(c) || b.CustomFieldValue.ToLower().Contains(c))
                             select a).Distinct().ToList();
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("BatchID", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Batch Ref", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("Available QTY", typeof(string));
            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            plist = plist.Where(o => !strs.Contains(o.DeafaultName)).ToList();
            foreach (var p in plist)
            {
                dt.Columns.Add(p.DisplayName, typeof(string));
            }

            DataRow datarw;
            var batchid = 0;
            foreach (var d in bList)
            {
                datarw = dt.NewRow();
                int i = 0;
                foreach (var p in plist)
                {
                    if (i == 0)
                    {
                        datarw[0] = d.ID;
                        datarw[1] = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.DateReceived.HasValue ? d.DateReceived.Value : Convert.ToDateTime("01/01/1900"));
                        datarw[2] = d.BatchDisplayName;
                        datarw[3] = d.QTY.ToString();
                        datarw[4] = Convert.ToString(d.QTY - PSDList.Where(o => o.BatchID == d.ID).Select(o => o.QtyUsed).Sum());
                        batchid = d.ID;
                        i = 5;
                    }
                    if (i > 0)
                    {
                        //var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
                        // var newPlist = plist.Where(o => !strs.Contains(o.DeafaultName) && o.DisplayName == p.DisplayName).ToList();
                        var bclist = ibatchCustomRepository.GetAll().Where(o => o.BatchID == batchid).ToList();
                        if (plist != null)
                        {
                            foreach (var l in plist)
                            {
                                var cuslist = ibatchCustomRepository.GetAll().Where(o => o.BatchID == batchid && o.GridFieldConfigurator.DisplayName == p.DisplayName).ToList();
                                if (cuslist != null)
                                {
                                    datarw[i] = cuslist.Select(o => o.CustomFieldValue).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            datarw[i] = string.Empty;
                        }
                    }
                    i++;
                }
                dt.Rows.Add(datarw);
            }

            FilterdBatchGrid.DataSource = dt;
            FilterdBatchGrid.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBatchGrid()
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var ibatchRepository = new InventoryRepository<Inventory_Batch>();
            var ibatchCustomRepository = new InventoryRepository<Inventory_BatchCustomData>();
            IinventoryRepository<InventoryMgt.Entity.InventoryManager> imRepository = new InventoryRepository<InventoryMgt.Entity.InventoryManager>();

            var plist = IGFC.GetAll().Where(o => o.CustomerId == sessionKeys.PortfolioID && o.Visibility == true).OrderBy(o => o.Position).ToList();
            //var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == int.Parse(ddlProduct.SelectedValue)).ToList();
            var bList = ibatchRepository.GetAll().Where(o => o.InventoryID == int.Parse(ddlProduct.SelectedValue)).ToList();
            //get batch custom fields


            IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
            PSD = new InventoryManager_PSDProduct();
            PSDList = new List<InventoryManager_PSDProduct>();
            PSDList = IMPSD.GetAll().ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("BatchID", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Batch Ref", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("Available QTY", typeof(string));
            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            plist = plist.Where(o => !strs.Contains(o.DeafaultName)).ToList();
            foreach (var p in plist)
            {
                dt.Columns.Add(p.DisplayName, typeof(string));
            }

            DataRow datarw;
            var batchid = 0;
            foreach (var d in bList)
            {
                datarw = dt.NewRow();
                int i = 0;
                foreach (var p in plist)
                {
                    if (i == 0)
                    {
                        datarw[0] = d.ID;
                        datarw[1] = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.DateReceived.HasValue ? d.DateReceived.Value : Convert.ToDateTime("01/01/1900"));
                        datarw[2] = d.BatchDisplayName;
                        datarw[3] = d.QTY.ToString();
                        datarw[4] = Convert.ToString(d.QTY - PSDList.Where(o => o.BatchID == d.ID).Select(o => o.QtyUsed).Sum());
                        batchid = d.ID;
                        i = 5;
                    }
                    if (i > 0)
                    {
                        //var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
                        // var newPlist = plist.Where(o => !strs.Contains(o.DeafaultName) && o.DisplayName == p.DisplayName).ToList();
                        var bclist = ibatchCustomRepository.GetAll().Where(o => o.BatchID == batchid).ToList();
                        if (plist != null)
                        {
                            foreach (var l in plist)
                            {
                                var cuslist = ibatchCustomRepository.GetAll().Where(o => o.BatchID == batchid && o.GridFieldConfigurator.DisplayName == p.DisplayName).ToList();
                                if (cuslist != null)
                                {
                                    datarw[i] = cuslist.Select(o => o.CustomFieldValue).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            datarw[i] = string.Empty;
                        }
                    }
                    i++;
                }
                dt.Rows.Add(datarw);
            }

            GridBatch.DataSource = dt;
            GridBatch.DataBind();

            UpdatePanel2.Update();
            UpdatePnlSummary.Update();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Inventory Search Class
    public class InventorySearch
    {
        public int? PortfolioID { set; get; }
        public string PortfolioName { set; get; }
        public int Id { set; get; }
        public int? Qty { set; get; }
        public int? QtyOnOrder { set; get; }
        public int? Categoery { set; get; }
        public int? SubCategory { set; get; }
        public int? SiteId { set; get; }
        public DateTime? ExpctArvlDate { set; get; }
        public Guid? Image { set; get; }
        public string ItemDescription { set; get; }
        public string Notes { set; get; }
        public string CategoryName { set; get; }
        public string SubCategoryName { set; get; }
        public string ManufacturerName { set; get; }
        public string Sitename { set; get; }
        public string AreaName { set; get; }
        public string LocationName { set; get; }
        public string ShelfName { set; get; }
        public string BinName { set; get; }
        public int? Area { set; get; }
        public int? Location { set; get; }
        public int? Shelf { set; get; }
        public int? Bin { set; get; }
        public int? Manufacturer { set; get; }

    }
    #endregion

    #region Add inventory
    public void Add_Panel_Inventory(bool setVal)
    {
        pnl_add_Customer.Visible = true;
        pnl_add_Category.Visible = setVal;
        pnl_add_Product.Visible = setVal;
        pnl_edit_product.Visible = !setVal;

        pnl_add_location.Visible = true;// setVal;
        pnl_add_location1.Visible = true;// setVal;
    }
    //public void Add_Panel_location_fields(bool setVal)
    //{
    //    pnl_add_location.Visible = setVal;
    //    pnl_add_location1.Visible = setVal;
    //}
    #endregion

    public void BindBatchDropdown(int ProductID)
    {
        var bRepository = new InventoryRepository<InventoryMgt.Entity.Inventory_Batch>();
        var blist = bRepository.GetAll().Where(o => o.InventoryID == ProductID).ToList();
        ddlBatch.DataSource = blist;
        ddlBatch.DataTextField = "BatchDisplayName";
        ddlBatch.DataValueField = "BatchRef";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        pUsageCustomer.Controls.Clear();
        //BindUsagePlaceholderFields();
        var bRepository = new InventoryRepository<Inventory_Batch>();
        var bcRepository = new InventoryRepository<Inventory_BatchCustomData>();
        IGFC = new InventoryRepository<GridFieldConfigurator>();
        var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
        var clist = IGFC.GetAll().Where(o => !strs.Contains(o.DeafaultName) && o.CustomerId == sessionKeys.PortfolioID).ToList();
        var bList = bRepository.GetAll().Where(o => o.InventoryID == Convert.ToInt32(lblProductidInPOPUP.Text) && o.BatchRef == (Convert.ToInt32(ddlBatch.SelectedValue))).FirstOrDefault();
        var bcList = bcRepository.GetAll().Where(o => o.BatchID == bList.ID).ToList();
        try
        {
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            if (clist != null)
            {
                Table tbl = new Table();
                tbl.EnableViewState = true;

                TableRow tr = new TableRow();
                TableCell td = null;
                int i = 1;

                int cnt = 0;
                int jcnt = 1;
                int totalCnt = clist.Count();
                if (totalCnt != 1)
                    tbl.Style.Add("width", "100%");
                else
                    tbl.Style.Add("width", "50%");
                foreach (var c in clist)
                {
                    if (tr == null)
                        tr = new TableRow();

                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.DisplayName));
                    td.Style.Add("width", "27%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox(c.id.ToString(), (bcList.Where(o => o.CustomFieldID == c.id).FirstOrDefault() != null ? bcList.Where(o => o.CustomFieldID == c.id).FirstOrDefault().CustomFieldValue : string.Empty), string.Empty, Isfirsttime, string.Empty, false, c.DisplayName, string.Empty, string.Empty, string.Empty));
                    //if (c.TypeOfField.ToLower() == "date")
                    //{
                    //    td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                    //}
                    td.Style.Add("width", "73%");
                    tr.Cells.Add(td);
                    i++;
                    cnt = cnt + 1;
                    if (cnt == 2)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && cnt == 1)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
                pUsageCustomer.Controls.Add(tbl);
                mdlpopupinGrid.Show();
                //ph_batchcolumns.Controls.Add(tbl);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridBatch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit1")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                //set batchid
                hid_batchid.Value = id.ToString();
                var bRepository = new InventoryRepository<Inventory_Batch>();
                var bEntity = bRepository.GetAll().Where(o => o.ID == id).FirstOrDefault();
                txtBatchreference.Text = (bEntity.BatchRef.HasValue ? bEntity.BatchRef.Value : 0).ToString();
                txtQtyEdit.Text = (bEntity.QTY.HasValue ? bEntity.QTY.Value : 0).ToString();
                txtProductEdit.Text = bEntity.InventoryManager.ItemDescription;
                //Bind area dropdowns
                BindSiteInNewItem();
                ddlsiteInNewItem.SelectedIndex = ddlsiteInNewItem.Items.IndexOf(ddlsiteInNewItem.Items.FindByValue((bEntity.SiteId.HasValue ? bEntity.SiteId.Value : 0).ToString()));
                BindAreaInNewItem(bEntity.InventoryManager.PortfolioID.HasValue ? bEntity.InventoryManager.PortfolioID.Value : 0);
                ddlareaInNewItem.SelectedIndex = ddlareaInNewItem.Items.IndexOf(ddlareaInNewItem.Items.FindByValue((bEntity.Area.HasValue ? bEntity.Area.Value : 0).ToString()));
                BindLocationInNewItem((bEntity.Area.HasValue ? bEntity.Area.Value : 0));
                ddlLocationInNewItem.SelectedIndex = ddlLocationInNewItem.Items.IndexOf(ddlLocationInNewItem.Items.FindByValue((bEntity.Location.HasValue ? bEntity.Location.Value : 0).ToString()));
                BindShelfInNewItem(bEntity.Location.HasValue ? bEntity.Location.Value : 0);
                ddlShelfInNewItem.SelectedIndex = ddlShelfInNewItem.Items.IndexOf(ddlShelfInNewItem.Items.FindByValue((bEntity.Shelf.HasValue ? bEntity.Shelf.Value : 0).ToString()));
                BindBinInNewItem(bEntity.Shelf.HasValue ? bEntity.Shelf.Value : 0);
                ddlBinInNewItem.SelectedIndex = ddlBinInNewItem.Items.IndexOf(ddlBinInNewItem.Items.FindByValue((bEntity.Bin.HasValue ? bEntity.Bin.Value : 0).ToString()));

                ph_batchcolumns.Controls.Clear();
                BindBatchPlaceholderFields(bEntity);
                Add_Panel_Inventory(false);
                //SettingDataToPlaceHolderInPopUp(id);
                btnSaveInNewItem.Text = "Update";
                mdlpopupForNewItem.Show();


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridBatch_PreRender(object sender, EventArgs e)
    {

    }
    public void BindCondtions()
    {
        try
        {
            using (InventoryDataContext Idcontext = new InventoryDataContext())
            {
                var tList = Idcontext.Inventory_Condtions.ToList();
                ddlcondition.DataSource = tList;
                ddlcondition.DataTextField = "Condition";
                ddlcondition.DataValueField = "id";
                ddlcondition.DataBind();
                ddlcondition.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridBatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    //var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                //    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    //var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                //    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                //}
                e.Row.Cells[1].Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlareaInNewItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlareaInNewItem.SelectedValue != "0")
        {
            BindLocationInNewItem(int.Parse(ddlareaInNewItem.SelectedValue));
        }
        else
        {
            ddlLocationInNewItem.Items.Clear();
            ddlLocationInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }

    }
    protected void ddlLocationInNewItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocationInNewItem.SelectedValue != "0")
        {
            BindShelfInNewItem(int.Parse(ddlLocationInNewItem.SelectedValue));
        }
        else
        {
            ddlShelfInNewItem.Items.Clear();
            ddlShelfInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void ddlShelfInNewItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlShelfInNewItem.SelectedValue != "0")
        {
            BindBinInNewItem(int.Parse(ddlShelfInNewItem.SelectedValue));
        }
        else
        {
            ddlBinInNewItem.Items.Clear();
            ddlBinInNewItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select", "0"));
        }
    }
    protected void FilterdBatchGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    //var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                //    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    //var v11 = v1.Where(o => o.Position == (i + 1)).FirstOrDefault();
                //    e.Row.Cells[i].Visible = v11.Visibility.HasValue ? v11.Visibility.Value : true;
                //}
                e.Row.Cells[1].Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void FilterdBatchGrid_PreRender(object sender, EventArgs e)
    {

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            BindFilteredBatchGrid(lblProductidInPOPUP.Text);
            mdlpopupinGrid.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void HideFields()
    {
        btnSaveInNewItem.Visible = false;
        btnupdate.Visible = false;
        Btninsert.Visible = false;
        btnAddNewItem.Visible = false;
        ImageButtonBinEdit.Visible = false;
        ImageButtonbinAdd.Visible = false;
        btnaddnewstock.Visible = false;
        btnadd.Visible = false;
        btnFieldConfigurator.Visible = false;
        UploadImg.Visible = false;
        hlinkExecption.Visible = false;

        ImageButton2.Visible = true;
        //usage grid add
        Btninsert.Visible=false;
        btnupdate.Visible = false;
        ddlcustomerInNewItem.Enabled = false;
        ddlCustomer.Enabled = false;
        ddlcustomerInSearch.Enabled = false;
    }
}