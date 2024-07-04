using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DeffinityManager.DAL.ServiceCatalogueMaterialsTableAdapters;
using DeffinityManager.DAL;
/// <summary>
/// Summary description for SC_ContractorMaterial
/// </summary>
namespace SC_ContractorMaterialCS
{
    public class SC_ContractorMaterial
    {
        
        private static CMCatalogueTableAdapter _CMCTableAdapater;
        private static Manufacturer_TableAdapter _ManufactureTabelAdapter;
        private static ServiceCategoryTableAdapter _CategoryTableAdapter;
        private static DN_PortfolioTableAdapter _DNPortfolioTableAdapter;
        private static ServiceSubCategoryTableAdapter _ServiceSubCategoryTableAdapter;
        private static SiteTableAdapter _SiteTableAdapter;
        private static InventoryManagerTableAdapter _InventoryManagerTableAdapter;
        public static InventoryManagerTableAdapter IMAdapter
        {
            get
            {
                if (_InventoryManagerTableAdapter == null)
                    _InventoryManagerTableAdapter = new InventoryManagerTableAdapter();
                return _InventoryManagerTableAdapter;
            }
        }
        public static SiteTableAdapter SiteAdapter
        {
            get
            {
                if (_SiteTableAdapter == null)
                    _SiteTableAdapter = new SiteTableAdapter();
                return _SiteTableAdapter;
            }
        }
        public static ServiceSubCategoryTableAdapter SubCategoryAdapter
        {
            get
            {
                if(_ServiceSubCategoryTableAdapter == null)
                _ServiceSubCategoryTableAdapter = new ServiceSubCategoryTableAdapter();
                return _ServiceSubCategoryTableAdapter;

            }
        }
        public static DN_PortfolioTableAdapter PortfolioAdapter
        {
            get
            {
                if (_DNPortfolioTableAdapter == null)
                    _DNPortfolioTableAdapter = new DN_PortfolioTableAdapter();
                return _DNPortfolioTableAdapter;
            }
        }
        public static ServiceCategoryTableAdapter CategoryAdapater
        {
            get
            {
                if (_CategoryTableAdapter == null)
                    _CategoryTableAdapter = new ServiceCategoryTableAdapter();
                return _CategoryTableAdapter;
            }
        }
        public static Manufacturer_TableAdapter ManufacturerAdapter
        {
            get
            {
                if (_ManufactureTabelAdapter == null)
                    _ManufactureTabelAdapter = new Manufacturer_TableAdapter();
                return _ManufactureTabelAdapter;
            }
        }

        public static CMCatalogueTableAdapter CMCAdapter
        {
            get
            {
                if (_CMCTableAdapater == null)
                    _CMCTableAdapater = new CMCatalogueTableAdapter();
                return _CMCTableAdapater;
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int CheckQty(int ProductId, int EnterQty)
        {
            int retVal = 0;
            retVal = Convert.ToInt32( IMAdapter.IM_CheckQty(ProductId,EnterQty));
            return retVal;

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.InventoryManagerDataTable SelectAllInverntory()
        {
            return IMAdapter.GetInventoryManagerData();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertInventory(int SiteId, int ProductId, int Qty, int ReorderLevel, string SectionType)
        {
            int retVal = -1;
            try
            {
                retVal = IMAdapter.Insert(SiteId, ProductId, Qty, ReorderLevel, SectionType);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateInventory(int Id, int SiteId, int ProductId, int Qty, int ReorderLevel, string SectionType)
        {
            int retVal = -1;
            ServiceCatalogueMaterials.InventoryManagerDataTable InvntyTableData = IMAdapter.GetIM_SelectByID(Id, SectionType);
            if(InvntyTableData.Rows.Count>0)
            {
                ServiceCatalogueMaterials.InventoryManagerRow InvtryRow = InvntyTableData[0];
                InvtryRow.SiteId = SiteId;
                InvtryRow.ProductId = ProductId;
                InvtryRow.Qty = Qty;
                InvtryRow.ReOrderLevel = ReorderLevel;
                InvtryRow.SectionType = SectionType;
                retVal = IMAdapter.Update(InvtryRow);
            }
            ServiceCatalogueMaterials.CMCatalogueDataTable cmcDataTableData = CMCAdapter.GetDataByCMCatId(Id);
            

            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.SiteDataTable GetSites()
        {
            return SiteAdapter.GetSiteData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.ServiceSubCategoryDataTable GetSubCategory(int CategoryID, int PortfolioID, int VendorID)
        {
            return SubCategoryAdapter.GetSubCategoryData(CategoryID, PortfolioID, VendorID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.DN_PortfolioDataTable SelectAllPortfolio()
        {
            return PortfolioAdapter.GetPortfolioData();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.ServiceCategoryDataTable SelectAllCategory(int PortfolioID, int PageType, int VendorID)
        {
            return CategoryAdapater.GetDataCategory(PortfolioID, PageType, VendorID);
        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.ManufacturerDataTable SelectAllManufacturer()
        {
            return ManufacturerAdapter.GetManufacturerData();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertManufacturer(string Name)
        {
            int retVal = -1;
            retVal = ManufacturerAdapter.Insert(Name);
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.CMCatalogueDataTable SelectProducts(int PortfolioId, int CategoryId, int SubCategoryId)
        {
            return CMCAdapter.GetProductData(PortfolioId, CategoryId, SubCategoryId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public ServiceCatalogueMaterials.CMCatalogueDataTable SelectAllMaterials(int PortfolioId)
        {
            return CMCAdapter.GetDataContractorMaterials(PortfolioId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.CMCatalogueDataTable SelectMaterial(int Id)
        {
            return CMCAdapter.GetDataByCMCatId(Id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]

        public int InsertMaterial(int ProjectReference,int ContractorID,int PortfolioID,string ItemDescription,int Supplier,
                                    string PartNumber,string UnitPrice,double BuyingPrice,double SellingPrice,
                                    int UnitsinStock,int ReorderLevel,string Notes,
                                    string ItemLock, string ItemDelete, double WorstCase, double BestCase, double MostLikelyCase, string currency,
                                    string WCEXtension, string BCEXtension, string MCEXtension, int QTY, Guid Image1,
                                    int Category,int SubCategory,int Type,int PageType,int VendorID, int QtyOnOrder,DateTime ExpctArvlDate,
                                    int Manufacturer,string Colour,int Length,int LeadTime,int Replenish)
        {

            int retVal = -1;
            retVal = CMCAdapter.Insert(ProjectReference, ContractorID, PortfolioID, ItemDescription, Supplier, PartNumber, UnitPrice,
                        BuyingPrice, SellingPrice, UnitsinStock, ReorderLevel, Notes,ItemLock,ItemDelete,WorstCase,BestCase,MostLikelyCase,
                        currency,WCEXtension,BCEXtension,MCEXtension,QTY, Image1, Category, SubCategory, Type, PageType, VendorID,
                        QtyOnOrder, ExpctArvlDate, Manufacturer, Colour, Length, Length,Replenish);
            return retVal;
            
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]

        public int UpdateMaterial(int Id, int ProjectReference,int ContractorID,int PortfolioID,string ItemDescription,int Supplier,
                                    string PartNumber,string UnitPrice,double BuyingPrice,double SellingPrice,
                                    int UnitsinStock,int ReorderLevel,string Notes,
                                    string ItemLock, string ItemDelete, double WorstCase, double BestCase, double MostLikelyCase, string currency,
                                    string WCEXtension, string BCEXtension, string MCEXtension, int QTY, Guid Image1,
                                    int Category,int SubCategory,int Type,int PageType,int VendorID, int QtyOnOrder,DateTime ExpctArvlDate,
                                    int Manufacturer,string Colour,int Length,int LeadTime,int Replenish)
        {
            int retVal = -1;

            ServiceCatalogueMaterials.CMCatalogueDataTable cmcDataTableData = CMCAdapter.GetDataByCMCatId(Id);
            if (cmcDataTableData.Rows.Count > 0)
            {
                ServiceCatalogueMaterials.CMCatalogueRow cmcRow = cmcDataTableData[0];
                cmcRow.ProjectReference = ProjectReference;
                cmcRow.ContractorID = ContractorID;
                cmcRow.PortfolioID = PortfolioID;
                cmcRow.ItemDescription = ItemDescription;
                cmcRow.Supplier = Supplier;
                cmcRow.PartNumber = PartNumber;
                cmcRow.UnitPrice = UnitPrice;
                cmcRow.BuyingPrice = BuyingPrice;
                cmcRow.SellingPrice = SellingPrice;
                cmcRow.UnitsinStock = UnitsinStock;
                cmcRow.ReorderLevel = ReorderLevel;
                cmcRow.Notes = Notes;
                cmcRow.ItemLock = ItemLock;
                cmcRow.ItemDelete = ItemDelete;
                cmcRow.WorstCase = WorstCase;
                cmcRow.BestCase = BestCase;
                cmcRow.MostLikelyCase = MostLikelyCase;
                cmcRow.currency = currency;
                cmcRow.WCEXtension = WCEXtension;
                cmcRow.BCEXtension = BCEXtension;
                cmcRow.MCEXtension = MCEXtension;
                cmcRow.QTY = QTY;
                cmcRow.Image = Image1;
                cmcRow.Category = Category;
                cmcRow.SubCategory = SubCategory;
                cmcRow.Type = Type;
                cmcRow.PageType = PageType;
                cmcRow.VendorID = VendorID;
                cmcRow.QtyOnOrder = QtyOnOrder;
                cmcRow.ExpctArvlDate = ExpctArvlDate;
                cmcRow.Manufacturer = Manufacturer;
                cmcRow.Colour = Colour;
                cmcRow.Length = Length;
                cmcRow.LeadTime = LeadTime;
                cmcRow.Replenish = Replenish;
                retVal = CMCAdapter.Update(cmcRow);
            }
            return retVal;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeleteCMCatalogue(int id)
        {
            int retVal = -1;
            retVal= CMCAdapter.Delete(id);
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public object ChkCMaterialDescExist(int PortfolioID, string MaterialDesc, int Category, int SubCategory)
        {

            object retObj = 0;
            retObj = CMCAdapter.ChkMaterialDescExists(PortfolioID, MaterialDesc, Category, SubCategory);
            return retObj;
        }

        public SC_ContractorMaterial()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public ServiceCatalogueMaterials.InventoryManagerDataTable SelectInvetoryByID(int CustomerId, int CategoryId, int SubCategoryId,int ProductId,int SiteId, string SectionType)
        {
            //return IMAdapter.GetInvntDataByCustomer(CustomerId, CategoryId, SubCategoryId);
            return IMAdapter.GetInventoryDataByIds(CustomerId, CategoryId, SubCategoryId, ProductId,SiteId,SectionType);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public ServiceCatalogueMaterials.InventoryManagerDataTable InventoryProductExist(int ProductID, string SectionType,int SiteId)
        {

            return IMAdapter.GetIMProductExist(ProductID, SectionType,SiteId);
            
        }
        public ServiceCatalogueMaterials.InventoryManagerDataTable IMProductById(int Id, string SectionType)
        {
            return IMAdapter.GetIM_SelectByID(Id, SectionType);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ServiceCatalogueMaterials.InventoryManagerDataTable ProductExistBySiteSection(int ProductId, int SectionId, string SectionType)
        {
            return IMAdapter.GetDataByPSSection(ProductId, SectionId, SectionType);
        }
    }
    
}
