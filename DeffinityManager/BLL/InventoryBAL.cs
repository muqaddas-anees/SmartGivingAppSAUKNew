using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

/// <summary>
/// Summary description for InventoryBAL
/// </summary>
public class InventoryBAL
{
    #region Get CategoryID
    public static int GetCategoryID()
    {
        int categoryId = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    string name = "Misc";
                    int portfolioId = GetPortfolioId();
                    var category = db.ServiceCatalog_categories.Where(s => s.PortfolioID == portfolioId && s.Type == 0 && s.PageType == 1 && s.CategoryName == name).Select(s => s).FirstOrDefault();

                    if (category != null)
                        categoryId = category.ID;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return categoryId;
    }
    #endregion
    #region Get SubcategoryID
    public static int GetSubCategoryID()
    {
        int subCategoryId = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    string name = "Misc";
                    int portfolioId = GetPortfolioId();
                    var subCategory = db.ServiceCatalog_categories.Where(s => s.PortfolioID == portfolioId && s.Type == 1 && s.PageType == 1 && s.CategoryName == name).Select(s => s).FirstOrDefault();

                    if (subCategory != null)
                        subCategoryId = subCategory.ID;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return subCategoryId;
    }
    #endregion
    #region Category and Subcategory Insert 
    public static void CategoryInsert()
    {
        try
        {
            string name = "Misc";
            using (InventoryDataContext db = new InventoryDataContext())
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    int portfolioId = GetPortfolioId();
                    var category = db.ServiceCatalog_categories.Where(s => s.PortfolioID == portfolioId && s.Type == 0 && s.PageType == 1 && s.CategoryName == name).Select(s => s).FirstOrDefault();
                    var subCategory = db.ServiceCatalog_categories.Where(s => s.PortfolioID == portfolioId && s.Type == 1 && s.PageType == 1 && s.CategoryName == name).Select(s => s).FirstOrDefault();
                    if (category == null && subCategory == null)
                    {
                        //Category Insert -"Misc"
                        InventoryMgt.Entity.ServiceCatalog_category sc = new InventoryMgt.Entity.ServiceCatalog_category();
                        sc.MasterID = 0;
                        sc.PageType = 1;
                        sc.Type = 0;
                        sc.PortfolioID = portfolioId;
                        sc.CategoryName = name;
                        sc.VendorID = 0;
                        db.ServiceCatalog_categories.InsertOnSubmit(sc);
                        db.SubmitChanges();

                        //SubCategory Insert -"Misc"
                        InventoryMgt.Entity.ServiceCatalog_category scs = new InventoryMgt.Entity.ServiceCatalog_category();
                        scs.MasterID = sc.ID; // categoryId
                        scs.PageType = 1;
                        scs.Type = 1;
                        scs.CategoryName = name;
                        scs.PortfolioID = portfolioId;
                        scs.VendorID = 0;
                        db.ServiceCatalog_categories.InsertOnSubmit(scs);
                        db.SubmitChanges();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    #endregion
    #region Get PortfolioId by project reference
    public static int GetPortfolioId()
    {
        int portfolioId = 0;
        try
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                portfolioId = pd.Projects.Where(p => p.ProjectReference == sessionKeys.Project).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return portfolioId;
    }
    #endregion
   
    #region Add site as "HQ"
    public static int AddDefaultSite()
    {
        int siteId = 0;
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                string name = "HQ";

                Site si = db.Sites.Where(s => s.Site1 == name).Select(s => s).FirstOrDefault();
                if (si == null)
                {
                    Site site = new InventoryMgt.Entity.Site();
                    site.CityID = 1;
                    site.Address1 = string.Empty;
                    site.Address2 = string.Empty;
                    site.Address3 = string.Empty;
                    site.Site1 = name;
                    site.Visible = 'Y';
                    db.Sites.InsertOnSubmit(site);
                    db.SubmitChanges();
                    siteId = site.ID;
                }

                Site defaultSite = db.Sites.Where(s => s.Site1 == name).Select(s => s).FirstOrDefault();
                if (defaultSite != null)
                {
                    AssignedSitesToPortfolio asp = db.AssignedSitesToPortfolios.Where(a => a.Portfolio == GetPortfolioId() && a.SiteID == defaultSite.ID).Select(a => a).FirstOrDefault();
                    if (asp == null)
                    {
                        AssignedSitesToPortfolio ap = new AssignedSitesToPortfolio();
                        ap.SiteID = defaultSite.ID;
                        ap.CityID = 1;
                        ap.CountryID = 1;
                        ap.Portfolio = GetPortfolioId();
                        db.AssignedSitesToPortfolios.InsertOnSubmit(ap);
                        db.SubmitChanges();
                    }
                    siteId = defaultSite.ID;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return siteId;
    }
    #endregion
    

    #region Journal
    public static void InsertInventoryJournal(InventoryManager inventoryManager, int qtyDelployed, string reasoncode, int transferQty, int openingStock, int useId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            InventoryManagerJournal inventoryManagerJournal = new InventoryManagerJournal();
            inventoryManagerJournal.InventoryId = inventoryManager.Id;
            inventoryManagerJournal.SiteId = inventoryManager.SiteId;
            inventoryManagerJournal.ProductId = inventoryManager.ProductId;
            inventoryManagerJournal.Qty = inventoryManager.Qty;
            inventoryManagerJournal.ReOrderLevel = inventoryManager.ReOrderLevel;
            inventoryManagerJournal.SectionType = inventoryManager.SectionType;
            inventoryManagerJournal.Categoery = inventoryManager.Categoery;
            inventoryManagerJournal.SubCategory = inventoryManager.SubCategory;
            inventoryManagerJournal.PortfolioID = inventoryManager.PortfolioID;
            inventoryManagerJournal.QtyOnOrder = inventoryManager.QtyOnOrder;
            inventoryManagerJournal.ExpctArvlDate = inventoryManager.ExpctArvlDate;
            inventoryManagerJournal.LeadTime = inventoryManager.LeadTime;
            inventoryManagerJournal.ItemDescription = inventoryManager.ItemDescription;
            inventoryManagerJournal.Supplier = inventoryManager.Supplier;
            inventoryManagerJournal.PartNumber = inventoryManager.PartNumber;
            inventoryManagerJournal.Manufacturer = inventoryManager.Manufacturer == null ? 0 : inventoryManager.Manufacturer;
            inventoryManagerJournal.DateOrdered = inventoryManager.DateOrdered;
            inventoryManagerJournal.Barcode = inventoryManager.Barcode;
            inventoryManagerJournal.Notes = inventoryManager.Notes;
            inventoryManagerJournal.ModifiedBy = sessionKeys.UID;
            inventoryManagerJournal.MdofiedDate = DateTime.Now;
            inventoryManagerJournal.NoDeployed = qtyDelployed;
            inventoryManagerJournal.ReasonCode = reasoncode;
            inventoryManagerJournal.TransferQty = transferQty;
            inventoryManagerJournal.OpeningStock = openingStock;
            inventoryManagerJournal.UseID = useId;

            var TestSample = db.InventoryManagerJournals.Where(a => a.InventoryId.Value == inventoryManagerJournal.InventoryId.Value).OrderByDescending(a => a.Id).FirstOrDefault();
            if (TestSample != null)
            {
                if (TestSample.Qty.Value != inventoryManagerJournal.Qty.Value)
                {
                    inventoryManagerJournal.QtyChangedOrNot = true;
                }
                else
                {
                    inventoryManagerJournal.QtyChangedOrNot = false;
                }
            }
            else
            {
                inventoryManagerJournal.QtyChangedOrNot = true;
            }
            db.InventoryManagerJournals.InsertOnSubmit(inventoryManagerJournal);
            db.SubmitChanges();

        }
    }
    public static int GetQtyAvailable(string id)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == int.Parse(id) && i.UseStatus == false).Select(i => i).ToList().Count();
        }
    }

    public static void InsertInventoryStockitemJournal(JInventoryStockItem jInventoryStockItem)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            db.JInventoryStockItems.InsertOnSubmit(jInventoryStockItem);
            db.SubmitChanges();
        }
    }

    #endregion

    
    public static string GetSiteNameByID(int siteId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.Sites.Where(s => s.ID == siteId).Select(s => s.Site1).FirstOrDefault();
        }
    }
    public static int GetQtyDeployed(int productId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == productId && i.UseStatus == true).Select(i => i).Count();
        }
    }

    public static void DeleteInventorySiteItems(int productId) 
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var delete = db.Inventory_SiteStorageDetails.Where(i => i.InventoryId == productId).ToList();
            if (delete.Count() > 0)
            {
                db.Inventory_SiteStorageDetails.DeleteAllOnSubmit(delete);
                db.SubmitChanges();
            }
        }
    
    }
    public static void DeleteInventoryJournal(int productId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var delete = db.InventoryManagerJournals.Where(i => i.InventoryId == productId).ToList();
            if (delete.Count() > 0)
            {
                db.InventoryManagerJournals.DeleteAllOnSubmit(delete);
                db.SubmitChanges();
            }
        }
    
    }
    public static InventoryManager GetInventoryByID(int id)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.InventoryManagers.Where(i => i.Id == id).FirstOrDefault();
        }
    }

}