using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Summary description for InventoryMgr
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class InventoryMgr : System.Web.Services.WebService {

    
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string GetBarcode(int id)
    {
        string barcode = string.Empty;
        using (InventoryDataContext db = new InventoryDataContext())
        {
           barcode = db.Inventory_AssociatedBarcodes.Where(a => a.SubCategoryID == id).Select(a => a.Barcode).FirstOrDefault();
           if (barcode == null)
               barcode = string.Empty;
           return barcode;
        }
    }

    [WebMethod(EnableSession=true)]
    public CascadingDropDownNameValue[] GetSite(string knownCategoryValues, string category)
    {
       
        int projectRef = sessionKeys.Project;
        using (InventoryDataContext db = new InventoryDataContext())
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                int portfolioId = pd.Projects.Where(p => p.ProjectReference == projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
                var result = (from s in db.Sites
                              join a in db.AssignedSitesToPortfolios
                                  on s.ID equals a.SiteID
                              where a.Portfolio == portfolioId
                              orderby s.Site1
                              select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Site1 }).ToArray();

                return result;
            }
        }


       


       
    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetInventorySite(string knownCategoryValues, string category)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
                var result = (from s in db.Sites
                              join a in db.AssignedSitesToPortfolios
                                  on s.ID equals a.SiteID
                              where a.Portfolio == sessionKeys.PortfolioID
                              orderby s.Site1
                              select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Site1 }).ToArray();

                return result;
        }

    }
   // [WebMethod(EnableSession = true)]
    //public CascadingDropDownNameValue[] GetStatusList(string knownCategoryValues, string category)
    //{
    //    string[] arr1 = new string[] { "Please select...", "Use", "Dispose", "Available" };
    //    var x = arr1.ToList();
    //    var mylist = (from r in x
    //                  select new CascadingDropDownNameValue { value = r.ToString(), name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle.ToString() }).ToArray();
    //    return mylist;
    //}
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetProjectsList(string knownCategoryValues, string category)
    {
        using (projectTaskDataContext timeSheet = new projectTaskDataContext())
        {
            string[] _CustomerValues = knownCategoryValues.Split(':', ';');
            int CRef = int.Parse(_CustomerValues[1]);
            var mylist = (from r in timeSheet.ProjectDetails
                          where r.ProjectStatusID == 2 && r.Portfolio == CRef
                          orderby r.ProjectTitle
                          select new CascadingDropDownNameValue { value = r.ProjectReference.ToString(), name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle.ToString() }).ToArray();
            return mylist;
        }
    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetProductBySubcategory(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int subCategoryID;
        if (!kv.ContainsKey("subCategory") || !Int32.TryParse(kv["subCategory"], out subCategoryID))
        {
            return null;
        }
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var result = (from p in db.InventoryManagers
                          where p.SubCategory == subCategoryID
                          select new CascadingDropDownNameValue { value = p.ItemDescription.ToString(), name = p.ItemDescription }).Distinct().ToArray();
            return result;
        }

    }
   
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetCategory(string knownCategoryValues, string category)
    {
        int projectRef = sessionKeys.Project;
        using (InventoryDataContext db = new InventoryDataContext())
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
               
                int portfolioId = pd.Projects.Where(p => p.ProjectReference ==projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
               
                    var result = (from s in db.ServiceCatalog_categories
                                  where s.PortfolioID == portfolioId && s.Type == 0 && s.PageType == 1
                                  orderby s.CategoryName
                                  select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.CategoryName }).ToArray();

                    return result;
                
            }
        }

    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetSubCategory(string knownCategoryValues, string category,string contextKey)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string categoryId = (_catgoryValue[1]);

        using (InventoryDataContext db = new InventoryDataContext())
        {
             using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                int projectRef = sessionKeys.Project;
                int portfolioId = pd.Projects.Where(p => p.ProjectReference ==projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
                  var result = (from s in db.ServiceCatalog_categories
                          where s.MasterID == int.Parse(categoryId) && s.PortfolioID == portfolioId && s.Type == 1
                          orderby s.CategoryName
                          select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.CategoryName }).ToArray();

            return result;
             }
        }

    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetUseSubCategory(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string categoryId = (_catgoryValue[1]);

        using (InventoryDataContext db = new InventoryDataContext())
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
               
                var result = (from s in db.ServiceCatalog_categories
                              where s.MasterID == int.Parse(categoryId) && s.PortfolioID == sessionKeys.PortfolioID && s.Type == 1
                              orderby s.CategoryName
                              select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.CategoryName }).ToArray();

                return result;
            }
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetInventoryUse(string knownCategoryValues, string category)
    {
        
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var result = (from i in db.Inventory_Uses
                          orderby i.Name
                          select new CascadingDropDownNameValue { value = i.ID.ToString(), name = i.Name+" ("+i.Code+")" }).ToArray();
                return result;
        }
    }
    public void AddFields(inventoryCustomField flsCustomField)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            db.inventoryCustomFields.InsertOnSubmit(flsCustomField);
            db.SubmitChanges();
        }
    }
    public void DeleteField(int id)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            inventoryCustomField flsCustomField = db.inventoryCustomFields.Where(c => c.ID == id).FirstOrDefault();
            if (flsCustomField != null)
            {
                db.inventoryCustomFields.DeleteOnSubmit(flsCustomField);
                db.SubmitChanges();
            }
        }
    }
    public  inventoryCustomField GetFieldByID(int fieldId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.inventoryCustomFields.Where(f => f.ID == fieldId).FirstOrDefault();
        }
    }
    public void UpdateFields(inventoryCustomField flsCustomField)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            inventoryCustomField flsCustomFieldCurrent = db.inventoryCustomFields.Where(f => f.ID == flsCustomField.ID).FirstOrDefault();
            if (flsCustomFieldCurrent != null)
            {
                flsCustomFieldCurrent.LabelName = flsCustomField.LabelName;
                flsCustomFieldCurrent.DefaultText = flsCustomField.DefaultText;
                flsCustomFieldCurrent.MinimumValue = flsCustomField.MinimumValue;
                flsCustomFieldCurrent.MaximumValue = flsCustomField.MaximumValue;
                flsCustomFieldCurrent.ListValue = flsCustomField.ListValue;
                flsCustomFieldCurrent.Mandatory = flsCustomField.Mandatory;
                flsCustomFieldCurrent.FieldPosition = flsCustomField.FieldPosition;
                db.SubmitChanges();
            }
        }
    }
    public List<InventoryAdditionalInfo> GetFLSAdditonalInfoByCallID(int callId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.InventoryAdditionalInfos.Where(f => f.productId == callId).ToList();

        }
    }
    public IEnumerable<inventoryCustomField> GetFieldList(int customerId)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            return db.inventoryCustomFields.Where(f => f.CustomerID == customerId).ToList();
        }
    }
    public  void UpdateFLSAddtionalInfo(InventoryAdditionalInfo flsAdditionalInfo)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            var currentFLSAddtionalInfo = db.InventoryAdditionalInfos.Where(f => f.ID == flsAdditionalInfo.ID).FirstOrDefault();
            if (currentFLSAddtionalInfo != null)
            {
                currentFLSAddtionalInfo.CustomFieldValue = flsAdditionalInfo.CustomFieldValue;
                db.SubmitChanges();
            }
        }
    }
    public void InsertFLSAdditionalInfo(InventoryAdditionalInfo flsAdditionalInfo)
    {
        using (InventoryDataContext db = new InventoryDataContext())
        {
            db.InventoryAdditionalInfos.InsertOnSubmit(flsAdditionalInfo);
            db.SubmitChanges();
        }
    }
    [WebMethod]
    public string DataTableToJsonWithJsonNet(int productid, int Cid)
    {
        string jsonString = string.Empty;
        try
        {
            DataTable table = BindBatchsInmainGrid(productid, Cid);
            jsonString = JsonConvert.SerializeObject(table);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonString;
    }
    [WebMethod]
    public DataTable BindBatchsInmainGrid(int productid, int Cid)
    {
        DataTable dt = new DataTable();
        try
        {
            InventoryRepository<GridFieldConfigurator> IGFC = new InventoryRepository<GridFieldConfigurator>();
            var ibatchRepository = new InventoryRepository<Inventory_Batch>();
            var ibatchCustomRepository = new InventoryRepository<Inventory_BatchCustomData>();
            var ibatchArea = new InventoryRepository<Inventory_Area>();
            var ibatchLocation = new InventoryRepository<Inventory_Locatin>();
            var ibatchShelf = new InventoryRepository<Inventory_Shelf>();
            var ibatchBin = new InventoryRepository<Inventory_Bin>();
            IinventoryRepository<InventoryMgt.Entity.InventoryManager> imRepository = new InventoryRepository<InventoryMgt.Entity.InventoryManager>();

            var plist = IGFC.GetAll().Where(o => o.CustomerId == Cid && o.Visibility == true).OrderBy(o => o.Position).ToList();
            //var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == int.Parse(ddlProduct.SelectedValue)).ToList();
            //  var productid = int.Parse(ddlProduct.SelectedValue);
            var bList = ibatchRepository.GetAll().Where(o => o.InventoryID == productid).ToList();
            //Get location Data
            var areaList = ibatchArea.GetAll().Where(o => bList.Select(p => p.Area).ToArray().Contains(o.id)).ToList();
            var locationList = ibatchLocation.GetAll().Where(o => bList.Select(p => p.Location).ToArray().Contains(o.id)).ToList();
            var shelfList = ibatchShelf.GetAll().Where(o => bList.Select(p => p.Shelf).ToArray().Contains(o.id)).ToList();
            var binList = ibatchBin.GetAll().Where(o => bList.Select(p => p.Bin).ToArray().Contains(o.id)).ToList();

            //get batch custom fields
            InventoryRepository<InventoryManager_PSDProduct> IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
            InventoryManager_PSDProduct PSD = new InventoryManager_PSDProduct();
            List<InventoryManager_PSDProduct> PSDList = new List<InventoryManager_PSDProduct>();
            PSDList = IMPSD.GetAll().Where(p => p.ProductId == productid).ToList();



            dt.Columns.Add("BatchID", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Batch Ref", typeof(string));
            dt.Columns.Add("Batch Control", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("Available QTY", typeof(string));
            dt.Columns.Add("Area", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Shelf", typeof(string));
            dt.Columns.Add("Bin", typeof(string));
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
                if (plist.Count == 0)
                {
                    datarw[0] = d.ID;
                    datarw[1] = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.DateReceived.HasValue ? d.DateReceived.Value : Convert.ToDateTime("01/01/1900"));
                    datarw[2] = d.BatchControl == true ? d.BatchDisplayName : "";

                    datarw[3] = d.BatchControl == true ? "YES" : "NO";

                    datarw[4] = d.QTY.ToString();
                    datarw[5] = Convert.ToString(d.QTY - PSDList.Where(o => o.BatchID == d.ID).Select(o => o.QtyUsed).Sum());
                    datarw[6] = d.Area.HasValue ? (d.Area.Value > 0 ? (areaList.Where(o => o.id == d.Area.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                    datarw[7] = d.Location.HasValue ? (d.Location.Value > 0 ? (locationList.Where(o => o.id == d.Location.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                    datarw[8] = d.Shelf.HasValue ? (d.Shelf.Value > 0 ? (shelfList.Where(o => o.id == d.Shelf.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                    datarw[9] = d.Bin.HasValue ? (d.Bin.Value > 0 ? (binList.Where(o => o.id == d.Bin.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                    batchid = d.ID;
                }
                else
                {
                    foreach (var p in plist)
                    {
                        if (i == 0)
                        {
                            datarw[0] = d.ID;
                            datarw[1] = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.DateReceived.HasValue ? d.DateReceived.Value : Convert.ToDateTime("01/01/1900"));
                            datarw[2] = d.BatchControl == true ? d.BatchDisplayName : "";

                            datarw[3] = d.BatchControl == true ? "YES" : "NO";

                            datarw[4] = d.QTY.ToString();
                            datarw[5] = Convert.ToString(d.QTY - PSDList.Where(o => o.BatchID == d.ID).Select(o => o.QtyUsed).Sum());
                            datarw[6] = d.Area.HasValue ? (d.Area.Value > 0 ? (areaList.Where(o => o.id == d.Area.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                            datarw[7] = d.Location.HasValue ? (d.Location.Value > 0 ? (locationList.Where(o => o.id == d.Location.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                            datarw[8] = d.Shelf.HasValue ? (d.Shelf.Value > 0 ? (shelfList.Where(o => o.id == d.Shelf.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                            datarw[9] = d.Bin.HasValue ? (d.Bin.Value > 0 ? (binList.Where(o => o.id == d.Bin.Value).FirstOrDefault().Name) : string.Empty) : string.Empty;
                            batchid = d.ID;
                            i = 10;
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
                }
                dt.Rows.Add(datarw);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dt;
        //int ij = dt.Rows.Count;
        //if (ij > 0)
        //{
        //    string Names = "tesing";
        //}
    }
      [WebMethod]
    public string DataTableToJsonWithJsonNetForUsageGrid(int productid, int Cid)
    {
        string jsonString = string.Empty;
        try
        {
            DataTable table = Bind_UsagesInMainGrid(productid, Cid);
            jsonString = JsonConvert.SerializeObject(table);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonString;
    }


    [WebMethod]
    public List<System.Web.UI.WebControls.ListItem> InventoryStatus()
       {
           List<System.Web.UI.WebControls.ListItem> li = new List<System.Web.UI.WebControls.ListItem>();
           li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
           li.Add(new System.Web.UI.WebControls.ListItem("Use", "1"));
           li.Add(new System.Web.UI.WebControls.ListItem("Dispose", "2"));
           li.Add(new System.Web.UI.WebControls.ListItem("Replenish", "3"));
           return li;
       }
    [WebMethod]
    public DataTable Bind_UsagesInMainGrid(int productid, int Cid)
       {
           DataTable dt = new DataTable();
           try
           {
               InventoryRepository<GridFieldConfigurator> IGFC = new InventoryRepository<GridFieldConfigurator>();
               InventoryRepository<InventoryManager_PSDProduct> IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
               InventoryRepository<InventoryManager_UsageCustomData> IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();

               var plist = IGFC.GetAll().Where(o => o.CustomerId == Cid).OrderBy(o => o.Position).ToList();

               var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == productid).ToList();
               var projectids = InList.Select(o => o.projectid).ToArray();
               var pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();

               var projectlist = pRepository.GetAll().Where(o => projectids.Contains(o.ProjectReference)).ToList();


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
                           if (d.Requester != null)
                           {
                               datarw[i] = d.Requester.ToString();
                           }
                           else
                           {
                               datarw[i] = string.Empty;
                           }
                       }
                       else if (p.DisplayName == "Status")
                       {
                           datarw[i] = InventoryStatus().Where(o => o.Value == Convert.ToString(d.Status)).FirstOrDefault().ToString();
                       }
                       else if (p.DisplayName == "Project")
                       {
                           if (d.projectid.HasValue)
                           {
                               datarw[i] = projectlist.Where(o => o.ProjectReference == d.projectid).Select(o => o.ProjectReferenceWithPrefix + "-" + o.ProjectTitle).FirstOrDefault();
                           }
                           else
                           {
                               datarw[i] = string.Empty;
                           }
                       }
                       else if (p.DisplayName == "Notes")
                       {
                           if (!string.IsNullOrEmpty(d.notes))
                           {
                               datarw[i] = d.notes.ToString();
                           }
                           else
                           {
                               datarw[i] = string.Empty;
                           }
                       }
                       else if (p.DisplayName == "Record Number")
                       {
                           if (!string.IsNullOrEmpty(d.RecordNumber))
                           {
                               datarw[i] = d.RecordNumber.ToString();
                           }
                           else
                           {
                               datarw[i] = string.Empty;
                           }
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
           }
           catch (Exception ex)
           {
               LogExceptions.WriteExceptionLog(ex);
           }
           return dt;
       }
}
