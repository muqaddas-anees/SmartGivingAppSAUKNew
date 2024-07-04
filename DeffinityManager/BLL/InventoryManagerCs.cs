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
using DeffinityManager.DAL.InventoryTableAdapters;
using DeffinityManager.DAL;

/// <summary>
/// Summary description for InventoryManagerCs
/// </summary>
namespace IMClass
{
    public class InventoryManagerCs
    {
       
        private static InventoryManager_TableAdapter _IM_TableAdapter;
        private static IMPSDProducts_TableAdapter _IMPSDProductAdapter;

        public static IMPSDProducts_TableAdapter IMPSDAdapter
        {
            get
            {
                if (_IMPSDProductAdapter == null)
                    _IMPSDProductAdapter = new IMPSDProducts_TableAdapter();
                return _IMPSDProductAdapter;
            }
        }
        public static InventoryManager_TableAdapter IMAdapter
        {
            get{
                if (_IM_TableAdapter == null)
                    _IM_TableAdapter = new InventoryManager_TableAdapter();
                return _IM_TableAdapter;

                }

        }
        #region IM products 
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public Inventory.IMPSDProductsDataTable SelectIMPSDProducts(int ProductId,string SectionType)
        {
            return IMPSDAdapter.GetIMPSDProductData(ProductId,SectionType);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Inventory.IMPSDProductsDataTable SelectIMPSDByID(int Id)
        {
            return IMPSDAdapter.GetIMPSDDataById(Id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public object InsertIMPSDProduct(int ProductID, int SiteId, string SectionType, int QtyUsed,int QtyReplenish)
        {
            object retVal = 0;
            try
            {
                retVal = IMPSDAdapter.IMPSDProducts_Insert(ProductID, SiteId, SectionType, QtyUsed, QtyReplenish);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]

        public object UpdateIMPSDProduct(int Id, int ProductId, int SiteId, string SectionType, int QtyUsed, int Replenish)
        {
            object retVal = 0;
            try
            {
                retVal = IMPSDAdapter.IMPSDProducts_Update(Id, ProductId, SiteId, QtyUsed, Replenish);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeleteIMPSDProduct(int Id)
        {
            int retVal = -1;
            try
            {
                Inventory.IMPSDProductsDataTable _IMProduct = IMPSDAdapter.GetIMPSDDataById(Id);
                if (_IMProduct.Rows.Count > 0)
                {
                    object obj =0;
                     obj= IMPSDAdapter.IMPSDProducts_Delete(Id);
                     if (obj != null)
                     {
                         retVal = int.Parse(obj.ToString());
                     }

                    
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }

        #endregion
        #region InventoryManager
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Inventory.InventoryManagerDataTable SelectProducts(int CustomerId, int CategoryId, int SubcategoryId, int SiteId, string SectionType)
        {
            return IMAdapter.GetInventoryData(CustomerId, CategoryId, SubcategoryId, SiteId, SectionType);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Inventory.InventoryManagerDataTable SelectProductByPID(int Id)
        {
            return IMAdapter.GetDataByPID(Id);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertInventoryProduct(int SiteId, int ProductId, int Qty, int ReorderLevel, string SectionType,
            int Category, int Subcategory, int PortfolioId, int QtyOnOrder, DateTime ExpctArvlDate, int LeadTime, 
            string ItemDescription,string PartNumber, Guid Image, int Supplier, int Manufacturer,DateTime DateOrdered,string barcode)
        {
            int retVal = -1;
            try
            {
                retVal = IMAdapter.Insert(SiteId, ProductId, Qty, ReorderLevel, SectionType, Category, Subcategory, PortfolioId,
                    QtyOnOrder, ExpctArvlDate, LeadTime, ItemDescription, Supplier, PartNumber, Image, Manufacturer, DateOrdered, barcode);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateInventoryProduct(int Id, int SiteId, int ProductId, int Qty, int ReorderLevel, string SectionType,
            int Category, int Subcategory, int PortfolioId, int QtyOnOrder, DateTime ExpctArvlDate, int LeadTime,
            string ItemDescription,string PartNumber, Guid Image, int Supplier, int Manufacturer,DateTime DateOrdered,string barcode)
        {
            int retVal = -1;
            try
            {
                Inventory.InventoryManagerDataTable IMTable = IMAdapter.GetDataByPID(Id);
                if (IMTable.Rows.Count > 0)
                {
                    Inventory.InventoryManagerRow IMRow = IMTable[0];
                    IMRow.SiteId = SiteId;
                    IMRow.ProductId = ProductId;
                    IMRow.Qty = Qty;
                    IMRow.ReOrderLevel = ReorderLevel;
                    IMRow.SectionType = SectionType;
                    IMRow.Categoery = Category;
                    IMRow.SubCategory = Subcategory;
                    IMRow.PortfolioID = PortfolioId;
                    IMRow.QtyOnOrder = QtyOnOrder;
                    IMRow.ExpctArvlDate = ExpctArvlDate;
                    IMRow.LeadTime = LeadTime;
                    IMRow.ItemDescription = ItemDescription;
                    IMRow.PartNumber = PartNumber;
                    IMRow.Image = Image;
                    IMRow.Supplier = Supplier;
                    IMRow.Manufacturer = Manufacturer;
                    IMRow.DateOrdered = DateOrdered;
                    IMRow.Barcode = barcode;
                    retVal = IMAdapter.Update(IMRow);

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateQOO(int Id, int QtyOnOrder)
        {
            int retVal = -1;
            try
            {
                Inventory.InventoryManagerDataTable IMTable = IMAdapter.GetDataByPID(Id);
                if (IMTable.Rows.Count > 0)
                {
                    retVal =int.Parse( IMAdapter.IM_UpdateQtyOnOrder(Id, QtyOnOrder).ToString());
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeletePrdouct(int Id)
        {
            int retVal = -1;
            try
            {
                Inventory.InventoryManagerDataTable IMTable = IMAdapter.GetDataByPID(Id);
                if (IMTable.Rows.Count > 0)
                {
                    retVal = IMAdapter.Delete(Id);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public object CheckProductDescExist(int PortfolioId, string ItemDescription, int Category, int SubCategory)
        {
            object retVal=0;
            try
            {
                
                retVal = IMAdapter.IM_ProductExist(PortfolioId, ItemDescription, Category, SubCategory);
               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        # endregion
        
    }

}