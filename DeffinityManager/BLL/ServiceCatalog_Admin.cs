using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;


namespace PortfolioMgt.BAL
{
    /// <summary>
    /// Summary description for ServiceCatalog_Admin
    /// </summary>
    public class ServiceCatalog_Admin
    {

        //Region Labour
         #region Labour
        public static ServiceCatelog_Labour ServiceCatelog_Labour_SelectByID(int LabourID)
        {
            ServiceCatelog_Labour Labour_list = new ServiceCatelog_Labour();

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                Labour_list = (from p in pd.ServiceCatelog_Labours
                                 where p.ID == LabourID 
                                 select p).FirstOrDefault();
            }

            return Labour_list;
        }
        public static IEnumerable<ServiceCatelog_Labour> ServiceCatelog_Labour_SelectByPortfolio(int portfolioid)
        {
            List<ServiceCatelog_Labour> labour_list = new List<ServiceCatelog_Labour>();

            using(PortfolioDataContext pd = new PortfolioDataContext())
            {
                labour_list = (from p in pd.ServiceCatelog_Labours
                              where p.PortfolioID == portfolioid
                              select p).ToList();
            }

            return labour_list;
        }
         public static IEnumerable<ServiceCatelog_Labour> ServiceCatelog_Labour_SelectByPortfolioAdmin()
        {
            List<ServiceCatelog_Labour> labour_list = new List<ServiceCatelog_Labour>();

            using(PortfolioDataContext pd = new PortfolioDataContext())
            {
                labour_list = (from p in pd.ServiceCatelog_Labours
                              where p.PortfolioID == 0 && p.VendorID == 0 
                              &&p.ProjectReference == 0 && p.ItemDelete !='1'
                              select p).ToList();
            }

            return labour_list;
        }
         public static IEnumerable<ServiceCatelog_Labour> ServiceCatelog_Labour_SelectByPortfolioAdmin(int categoryID, int subcategoryID)
         { 
             List<ServiceCatelog_Labour> labour_list = new List<ServiceCatelog_Labour>();

             
             if(categoryID > 0 && subcategoryID >0)
                 labour_list = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.Category == categoryID && p.SubCategory == subcategoryID).ToList();
             else if (categoryID > 0)
                 labour_list = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.Category == categoryID).ToList();


             return labour_list;
         }
         public static void ServiceCatelog_Labour_InsertByPortfolioAdmin(ServiceCatelog_Labour labour)
         {
             
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 int count = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.EngineerDescription.ToLower() == labour.EngineerDescription.ToLower() && p.Category == labour.Category && p.SubCategory == labour.SubCategory).Count();

                 if (count == 0)
                 {
                     labour.PortfolioID =0;
                     labour.ProjectReference =0;
                     labour.VendorID=0;
                     labour.ItemDelete = '0';
                     labour.Type = 0;
                     labour.ItemLock = "0";
                     labour.PageType = 1;
                     pd.ServiceCatelog_Labours.InsertOnSubmit(labour);
                     pd.SubmitChanges();
                 }
             }
             
         }
         public static int ServiceCatelog_Labour_InsertByPortfolio(ServiceCatelog_Labour labour)
         {
             int id = 0;
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Labour sl = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.EngineerDescription.ToLower() == labour.EngineerDescription.ToLower() && p.Category == labour.Category && p.SubCategory == labour.SubCategory && p.PortfolioID == labour.PortfolioID && p.RateType == labour.RateType && p.ItemDelete != '1').FirstOrDefault();

                 if (sl == null)
                 {
                     labour.ProjectReference = 0;
                     labour.VendorID = 0;
                     labour.ItemDelete = '0';
                     labour.Type = 0;
                     labour.ItemLock = "0";
                     labour.PageType = 1;
                     pd.ServiceCatelog_Labours.InsertOnSubmit(labour);
                     pd.SubmitChanges();
                     id = labour.ID;
                 }
                 else
                 {
                     id = labour.ID;
                 }
             }
             return id;
         }
         public static void ServiceCatelog_Labour_UpdateByPortfolioAdmin(ServiceCatelog_Labour labour)
         {
            
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Labour labour_current = new ServiceCatelog_Labour();
                 List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
                 labour_current =  (from p in pd.ServiceCatelog_Labours
                                   where p.ID == labour.ID
                                   select p).Single();
                 //ServiceCatelog_Labour labour_current = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.ID == labour.ID).FirstOrDefault();

                 if (labour_current != null)
                 {
                     //labour_current.EngineerDescription = labour.EngineerDescription;
                     labour_current.EngineerDescription = labour.EngineerDescription;
                     labour_current.DiscountPrice = labour.DiscountPrice;
                     labour_current.currency = labour.currency;
                     labour_current.BuyingPrice = labour.BuyingPrice;
                     labour_current.Image = labour.Image;
                     labour_current.Notes = labour.Notes;
                     labour_current.Rate = labour.Rate;
                     labour_current.RateType = labour.RateType;
                     labour_current.RouteToServiceTeam = labour.RouteToServiceTeam;
                     labour_current.SellingPrice = labour.SellingPrice;
                     labour_current.UnitConsumption = labour.UnitConsumption;
                     labour_current.MrkUp = labour.MrkUp;
                     sa = (from p in pd.ServiceCatalogItem_Associates
                           where p.CatalogID == labour_current.ID && p.TypeofCatelog == 1
                           select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var labour_list = (from p in pd.ServiceCatelog_Labours
                                            where p.ID == s.AssociateCatalogID
                                            select p).ToList();
                         foreach (ServiceCatelog_Labour sl in labour_list)
                         {
                             sl.EngineerDescription = labour.EngineerDescription;
                             sl.DiscountPrice = labour.DiscountPrice;
                             sl.currency = labour.currency;
                             sl.BuyingPrice = labour.BuyingPrice;
                             sl.Image = labour.Image;
                             sl.Notes = labour.Notes;
                             sl.Rate = labour.Rate;
                             sl.RateType = labour.RateType;
                             sl.RouteToServiceTeam = labour.RouteToServiceTeam;
                             sl.SellingPrice = labour.SellingPrice;
                             sl.UnitConsumption = labour.UnitConsumption;
                             sl.MrkUp = labour.MrkUp;
                         }
                         
                     }
                     
                     //pd.ServiceCatelog_Labours.Attach(labour);
                     pd.SubmitChanges();
                 }
             }

         }

         public static void ServiceCatelog_Labour_DeleteByPortfolioAdmin(int ID)
         {
             ServiceCatelog_Labour labour_current = new ServiceCatelog_Labour();
             List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 labour_current = (from p in pd.ServiceCatelog_Labours
                                  where p.ID == ID
                                  select p).Single();

                 if (labour_current != null)
                 {
                     labour_current.ItemDelete = '1';

                     sa = (from p in pd.ServiceCatalogItem_Associates
                                       where p.CatalogID == ID && p.TypeofCatelog == 1
                                       select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var labour_list = (from p in pd.ServiceCatelog_Labours
                                           where p.ID == s.AssociateCatalogID
                                           select p).ToList();
                         foreach (ServiceCatelog_Labour sl in labour_list)
                         {
                             sl.ItemDelete = '1';
                         }
                         pd.ServiceCatalogItem_Associates.DeleteOnSubmit(s);

                     }

                     pd.SubmitChanges();
                 }
             }

         }

#endregion

         #region Material
         public static ServiceCatelog_Material ServiceCatelog_Material_SelectByID(int MaterialID)
         {
             ServiceCatelog_Material Material_list = new ServiceCatelog_Material();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Material_list = (from p in pd.ServiceCatelog_Materials
                                  where p.ID == MaterialID 
                                  select p).FirstOrDefault();
             }

             return Material_list;
         }
         public static IEnumerable<ServiceCatelog_Material> ServiceCatelog_Material_SelectByPortfolio(int portfolioid)
         {
             List<ServiceCatelog_Material> Material_list = new List<ServiceCatelog_Material>();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Material_list = (from p in pd.ServiceCatelog_Materials
                                where p.PortfolioID == portfolioid
                                select p).ToList();
             }

             return Material_list;
         }
         public static IEnumerable<ServiceCatelog_Material> ServiceCatelog_Material_SelectByPortfolioAdmin()
         {
             List<ServiceCatelog_Material> Material_list = new List<ServiceCatelog_Material>();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Material_list = (from p in pd.ServiceCatelog_Materials
                                where p.PortfolioID == 0 && p.VendorID == 0
                                && p.ProjectReference == 0 && p.ItemDelete != '1'
                                select p).ToList();
             }

             return Material_list;
         }
         public static IEnumerable<ServiceCatelog_Material> ServiceCatelog_Material_SelectByPortfolioAdmin(int categoryID, int subcategoryID)
         {
             List<ServiceCatelog_Material> material_list = new List<ServiceCatelog_Material>();

             if (categoryID > 0 && subcategoryID > 0)
                 material_list = ServiceCatelog_Material_SelectByPortfolioAdmin().Where(p => p.Category == categoryID && p.SubCategory == subcategoryID).ToList();
             else if (categoryID > 0)
                 material_list = ServiceCatelog_Material_SelectByPortfolioAdmin().Where(p => p.Category == categoryID).ToList();
             return material_list;
         }
         public static void ServiceCatelog_Material_InsertByPortfolioAdmin(ServiceCatelog_Material material)
         {

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 int count = ServiceCatelog_Material_SelectByPortfolioAdmin().Where(p => p.ItemDescription.ToLower() == material.ItemDescription.ToLower() && p.Category == material.Category && p.SubCategory == material.SubCategory && p.PartNumber == material.PartNumber).Count();

                 if (count == 0)
                 {
                     material.PortfolioID =0;
                     material.ProjectReference =0;
                     material.VendorID=0;
                     material.ItemDelete = '0';
                     material.Type = 0;
                     material.ItemLock = "0";
                     material.PageType = 1;
                     pd.ServiceCatelog_Materials.InsertOnSubmit(material);
                     pd.SubmitChanges();
                 }
             }

         }
         public static int ServiceCatelog_Material_InsertByPortfolio(ServiceCatelog_Material material)
         {
             int id = 0;
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Material sm = ServiceCatelog_Material_SelectByPortfolioAdmin().Where(p => p.ItemDescription.ToLower() == material.ItemDescription.ToLower() && p.Category == material.Category && p.SubCategory == material.SubCategory && p.PartNumber == material.PartNumber && p.PortfolioID == material.PortfolioID && p.ItemDelete != '1').LastOrDefault();

                 if (sm == null)
                 {
                     material.ProjectReference = 0;
                     material.VendorID = 0;
                     material.ItemDelete = '0';
                     material.Type = 0;
                     material.ItemLock = "0";
                     material.PageType = 1;
                     pd.ServiceCatelog_Materials.InsertOnSubmit(material);
                     pd.SubmitChanges();
                     id = material.ID;
                 }
                 else
                 {
                     id = sm.ID;
                 }
             }
             return id;
         }

         public static void ServiceCatelog_Material_UpdateByPortfolioAdmin(ServiceCatelog_Material material)
         {

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Material material_current = new ServiceCatelog_Material();
                 List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
                 material_current = (from p in pd.ServiceCatelog_Materials
                                   where p.ID == material.ID
                                   select p).Single();
                 //ServiceCatelog_Labour labour_current = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.ID == labour.ID).FirstOrDefault();

                 if (material_current != null)
                 {
                     //labour_current.EngineerDescription = labour.EngineerDescription;
                     material_current.ItemDescription = material.ItemDescription;
                     material_current.BuyingPrice = material.BuyingPrice;
                     material_current.DiscountPrice = material.DiscountPrice;
                     material_current.Image = material.Image;
                     material_current.MrkUp = material.MrkUp;
                     material_current.Notes = material.Notes;
                     material_current.NSPPrice = material.NSPPrice;
                     material_current.PartNumber = material.PartNumber;
                     material_current.QTY = material.QTY;
                     material_current.QtyOnOrder = material.QtyOnOrder;
                     material_current.ReorderLevel = material.ReorderLevel;
                     material_current.SellingPrice = material.SellingPrice;
                     material_current.Supplier = material.Supplier;
                     material_current.Unit = material.Unit;
                     material_current.UnitConsumption = material.UnitConsumption;
                     material_current.UnitPrice = material.UnitPrice;
                     material_current.UnitsinStock = material.UnitsinStock;

                     sa = (from p in pd.ServiceCatalogItem_Associates
                           where p.CatalogID == material.ID && p.TypeofCatelog == 2
                           select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var material_list = (from p in pd.ServiceCatelog_Materials
                                            where p.ID == s.AssociateCatalogID
                                            select p).ToList();
                         foreach (ServiceCatelog_Material sm in material_list)
                         {
                             sm.ItemDescription = material.ItemDescription;
                             sm.BuyingPrice = material.BuyingPrice;
                             sm.DiscountPrice = material.DiscountPrice;
                             sm.Image = material.Image;
                             sm.MrkUp = material.MrkUp;
                             sm.Notes = material.Notes;
                             sm.NSPPrice = material.NSPPrice;
                             sm.PartNumber = material.PartNumber;
                             sm.QTY = material.QTY;
                             sm.QtyOnOrder = material.QtyOnOrder;
                             sm.ReorderLevel = material.ReorderLevel;
                             sm.SellingPrice = material.SellingPrice;
                             sm.Supplier = material.Supplier;
                             sm.Unit = material.Unit;
                             sm.UnitConsumption = material.UnitConsumption;
                             sm.UnitPrice = material.UnitPrice;
                             sm.UnitsinStock = material.UnitsinStock;
                         }

                     }

                     //pd.ServiceCatelog_Labours.Attach(labour);
                     pd.SubmitChanges();
                 }
             }

         }
         public static void ServiceCatelog_Material_DeleteByPortfolioAdmin(int ID)
         {

             ServiceCatelog_Material labour_material = new ServiceCatelog_Material();
             List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 labour_material = (from p in pd.ServiceCatelog_Materials
                                    where p.ID == ID
                                    select p).Single();

                 if (labour_material != null)
                 {
                     labour_material.ItemDelete = '1';

                     sa = (from p in pd.ServiceCatalogItem_Associates
                           where p.CatalogID == ID && p.TypeofCatelog == 2
                           select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var labour_list = (from p in pd.ServiceCatelog_Materials
                                            where p.ID == s.AssociateCatalogID 
                                            select p).ToList();
                         foreach (ServiceCatelog_Material sm in labour_list)
                         {
                             sm.ItemDelete = '1';
                         }
                         pd.ServiceCatalogItem_Associates.DeleteOnSubmit(s);

                     }

                     pd.SubmitChanges();
                 }
             }


         }

         #endregion

         #region Service
         public static ServiceCatelog_Service ServiceCatelog_Service_SelectByID(int ServiceID)
         {
             ServiceCatelog_Service Service_list = new ServiceCatelog_Service();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Service_list = (from p in pd.ServiceCatelog_Services
                                 where p.ID == ServiceID 
                                select p).FirstOrDefault();
             }

             return Service_list;
         }
         public static IEnumerable<ServiceCatelog_Service> ServiceCatelog_Service_SelectByPortfolio(int portfolioid)
         {
             List<ServiceCatelog_Service> Service_list = new List<ServiceCatelog_Service>();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Service_list = (from p in pd.ServiceCatelog_Services
                                  where p.PortfolioID == portfolioid && p.ItemDelete !=1
                                  select p).ToList();
             }

             return Service_list;
         }
      
         public static IEnumerable<ServiceCatelog_Service> ServiceCatelog_Service_SelectByPortfolioAdmin()
         {
             List<ServiceCatelog_Service> Service_list = new List<ServiceCatelog_Service>();

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 Service_list = (from p in pd.ServiceCatelog_Services
                                  where p.PortfolioID == 0 && p.VendorID == 0
                                  && p.ProjectReference == 0 && p.ItemDelete != 1
                                  select p).ToList();
             }

             return Service_list;
         }
         public static IEnumerable<ServiceCatelog_Service> ServiceCatelog_Service_SelectByPortfolioAdmin(int categoryID, int subcategoryID)
         {
             List<ServiceCatelog_Service> labour_list = new List<ServiceCatelog_Service>();

             if (categoryID > 0 && subcategoryID > 0)
                 labour_list = ServiceCatelog_Service_SelectByPortfolioAdmin().Where(p => p.Category == categoryID && p.SubCategory == subcategoryID).ToList();
             else if (categoryID > 0)
                 labour_list = ServiceCatelog_Service_SelectByPortfolioAdmin().Where(p => p.Category == categoryID).ToList();

             return labour_list;
         }
         public static void ServiceCatelog_Service_Insert_ByPortfolioAdmin(ServiceCatelog_Service service)
         {

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 int count = ServiceCatelog_Service_SelectByPortfolioAdmin().Where(p => p.ServiceDescription.ToLower() == service.ServiceDescription.ToLower() && p.Category == service.Category && p.SubCategory == service.SubCategory && p.ItemDelete != 1).Count();

                 if (count == 0)
                 {
                     service.PortfolioID =0;
                     service.ProjectReference =0;
                     service.VendorID=0;
                     service.ItemDelete = 0;
                     service.Type = 0;
                     service.PageType = 1;
                     service.ServiceType = 3;
                     pd.ServiceCatelog_Services.InsertOnSubmit(service);
                     pd.SubmitChanges();
                 }
             }

         }
         public static int ServiceCatelog_Service_Insert_ByPortfolio(ServiceCatelog_Service service)
         {
             int id = 0;
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Service ss = ServiceCatelog_Service_SelectByPortfolioAdmin().Where(p => p.ServiceDescription.ToLower() == service.ServiceDescription.ToLower() && p.Category == service.Category && p.SubCategory == service.SubCategory && p.PortfolioID == service.PortfolioID && p.ItemDelete != 1).FirstOrDefault();

                 if (ss == null)
                 {
                     service.ProjectReference = 0;
                     service.VendorID = 0;
                     service.ItemDelete = 0;
                     service.Type = 0;
                     service.PageType = 1;
                     service.ServiceType = 3;
                     pd.ServiceCatelog_Services.InsertOnSubmit(service);
                     pd.SubmitChanges();
                     id = service.ID;
                 }
                 else
                 {
                     id = ss.ID;
                 }
             }
             return id;
         }

         public static void ServiceCatelog_Service_Update_ByPortfolioAdmin(ServiceCatelog_Service service)
         {

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatelog_Service service_current = new ServiceCatelog_Service();
                 List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
                 service_current = (from p in pd.ServiceCatelog_Services
                                     where p.ID == service.ID
                                     select p).Single();
                 //ServiceCatelog_Labour labour_current = ServiceCatelog_Labour_SelectByPortfolioAdmin().Where(p => p.ID == labour.ID).FirstOrDefault();

                 if (service_current != null)
                 {
                     //labour_current.EngineerDescription = labour.EngineerDescription;
                     service_current.BuyingPrice = service.BuyingPrice;
                     service_current.DetailedDesc = service.DetailedDesc;
                     service_current.DiscountPrice = service.DiscountPrice;
                     service_current.GP = service.GP;
                     service_current.Image = service.Image;
                     service_current.LabourBuy = service.LabourBuy;
                     service_current.LabourSell = service.LabourSell;
                     service_current.MaterialsBuy = service.MaterialsBuy;
                     service_current.MaterialsSell = service.MaterialsSell;
                     service_current.QTY = service.QTY;
                     service_current.SellingPrice = service.SellingPrice;
                     service_current.ServiceDescription = service.ServiceDescription;
                     service_current.SetupBuy = service.SetupBuy;
                     service_current.SetupSell = service.SetupSell;
                     service_current.TotalServiceBuy = service.TotalServiceSell;
                     service_current.UnitConsumption = service.UnitConsumption;
                     service_current.MrkUp = service.MrkUp;

                     sa = (from p in pd.ServiceCatalogItem_Associates
                           where p.CatalogID == service.ID && p.TypeofCatelog == 3
                           select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var service_list = (from p in pd.ServiceCatelog_Services
                                              where p.ID == s.AssociateCatalogID
                                              select p).ToList();
                         foreach (ServiceCatelog_Service ss in service_list)
                         {
                             ss.BuyingPrice = service.BuyingPrice;
                             ss.DetailedDesc = service.DetailedDesc;
                             ss.DiscountPrice = service.DiscountPrice;
                             ss.GP = service.GP;
                             ss.Image = service.Image;
                             ss.LabourBuy = service.LabourBuy;
                             ss.LabourSell = service.LabourSell;
                             ss.MaterialsBuy = service.MaterialsBuy;
                             ss.MaterialsSell = service.MaterialsSell;
                             ss.QTY = service.QTY;
                             ss.SellingPrice = service.SellingPrice;
                             ss.ServiceDescription = service.ServiceDescription;
                             ss.SetupBuy = service.SetupBuy;
                             ss.SetupSell = service.SetupSell;
                             ss.TotalServiceBuy = service.TotalServiceSell;
                             ss.UnitConsumption = service.UnitConsumption;
                             ss.MrkUp = service.MrkUp;
                         }

                     }

                     //pd.ServiceCatelog_Labours.Attach(labour);
                     pd.SubmitChanges();
                 }
             }

         }

         public static void ServiceCatelog_Service_DeleteByPortfolioAdmin(int ID)
         {

             ServiceCatelog_Service labour_service = null;
             List<ServiceCatalogItem_Associate> sa = new List<ServiceCatalogItem_Associate>();
             
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 labour_service = (from p in pd.ServiceCatelog_Services
                                   where p.ID == ID
                                   select p).FirstOrDefault();

                 if (labour_service != null)
                 {
                     labour_service.ItemDelete = 1;
                     //pd.ServiceCatelog_Services.i(labour_service);
                     pd.SubmitChanges();
                     

                     sa = (from p in pd.ServiceCatalogItem_Associates
                           where p.CatalogID == ID && p.TypeofCatelog==3
                           select p).ToList();
                     foreach (ServiceCatalogItem_Associate s in sa)
                     {
                         var labour_list = (from p in pd.ServiceCatelog_Services
                                            where p.ID == s.AssociateCatalogID 
                                            select p).ToList();
                         foreach (ServiceCatelog_Service ss in labour_list)
                         {
                             ss.ItemDelete = 1;
                         }
                         pd.ServiceCatalogItem_Associates.DeleteOnSubmit(s);

                     }

                     pd.SubmitChanges();
                 }
             }

         }

         #endregion

         #region Category
         public static ServiceCatalog_category ServiceCatalog_category_ByID(int ID)
         {
             ServiceCatalog_category category = new ServiceCatalog_category();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 category = (from p in pd.ServiceCatalog_categories
                             where p.ID == ID
                             select p).FirstOrDefault();

             }

             return category;
         }
         public static List<ServiceCatalog_category> ServiceCatalog_category_CategoryByAdmin()
         {
             List<ServiceCatalog_category> category = new List<ServiceCatalog_category>();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 category = (from p in pd.ServiceCatalog_categories
                            where p.PortfolioID == 0 && p.VendorID == 0 && p.MasterID == 0
                            select p).ToList();
                
             }

             return category;
         }
         public static ServiceCatalog_category ServiceCatalog_category_CategoryByAdmin(int CategoryID)
         {
             ServiceCatalog_category category = new ServiceCatalog_category();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 category = (from p in pd.ServiceCatalog_categories
                             where p.ID == CategoryID
                             select p).FirstOrDefault();

             }

             return category;
         }
         public static void ServiceCatalog_CategoryInsertByAdmin(ServiceCatalog_category category)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 int count = pd.ServiceCatalog_categories.Where(p => p.CategoryName.ToLower() == category.CategoryName.ToLower() && p.PortfolioID == 0 && p.VendorID == 0 && p.MasterID ==0 ).Count();
                 if (count == 0)
                 {
                     category.PortfolioID = 0;
                     category.VendorID = 0;
                     category.MasterID = 0;
                     category.PageType = 1;
                     category.Type = 0;
                     pd.ServiceCatalog_categories.InsertOnSubmit(category);
                     pd.SubmitChanges();
                 }
             }
         }

         public static int ServiceCatalog_CategoryInsertByPortoflio(ServiceCatalog_category category)
         {
             int categoryID = 0;
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalog_category sc = pd.ServiceCatalog_categories.Where(p => p.CategoryName.ToLower() == category.CategoryName.ToLower() && p.PortfolioID == category.PortfolioID && p.VendorID == 0 && p.MasterID == 0).FirstOrDefault();
                 if (sc == null)
                 {
                     category.VendorID = 0;
                     category.MasterID = 0;
                     category.PageType = 1;
                     category.Type = 0;
                     pd.ServiceCatalog_categories.InsertOnSubmit(category);
                     pd.SubmitChanges();
                     categoryID = category.ID;

                 }
                 else
                 {
                     categoryID = sc.ID;
                 }
             }
             return categoryID;
         }
      

         public static void ServiceCatalog_Category_category_UpdateByAdmin(ServiceCatalog_category category)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalog_category sc_current = pd.ServiceCatalog_categories.Where(p => p.ID == category.ID).FirstOrDefault();
                 sc_current.CategoryName = category.CategoryName;
                 pd.SubmitChanges();
             }
         }
         public static void ServiceCatalog_Category_category_DeleteByAdmin(int categoryid)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalog_category sc_current = pd.ServiceCatalog_categories.Where(p => p.ID == categoryid).FirstOrDefault();
                 pd.ServiceCatalog_categories.DeleteOnSubmit(sc_current);
                 pd.SubmitChanges();
             }
         }
        #endregion

         #region Sub Category
         public static List<ServiceCatalog_category> ServiceCatalog_category_SubCategoryByAdmin(int CategoryID)
         {
             List<ServiceCatalog_category> category = new List<ServiceCatalog_category>();
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 category = (from p in pd.ServiceCatalog_categories
                             where p.PortfolioID == 0 && p.VendorID == 0 && p.MasterID == CategoryID && p.MasterID >0
                             select p).ToList();

             }

             return category;
         }

         public static void ServiceCatalog_SubCategoryInsertByAdmin(ServiceCatalog_category category)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 int count = pd.ServiceCatalog_categories.Where(p => p.CategoryName.ToLower() == category.CategoryName.ToLower() && p.PortfolioID == 0 && p.VendorID == 0 && p.MasterID > 0).Count();
                 if (count == 0)
                 {
                     category.PortfolioID = 0;
                     category.VendorID = 0;
                     category.PageType = 1;
                     category.Type = 1;
                     pd.ServiceCatalog_categories.InsertOnSubmit(category);
                     pd.SubmitChanges();
                 }
             }
         }
         public static int ServiceCatalog_SubCategoryInsertByPortfolio(ServiceCatalog_category category)
         {
             int subCategory = 0;
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalog_category sc = pd.ServiceCatalog_categories.Where(p => p.CategoryName.ToLower() == category.CategoryName.ToLower() && p.PortfolioID == category.PortfolioID && p.VendorID == 0 && p.MasterID > 0 && p.MasterID == category.MasterID).FirstOrDefault();
                 if (sc == null)
                 {
                     category.VendorID = 0;
                     category.PageType = 1;
                     category.Type = 1;
                     pd.ServiceCatalog_categories.InsertOnSubmit(category);
                     pd.SubmitChanges();
                     subCategory = category.ID;

                 }
                 else
                 {
                     subCategory = sc.ID;
                 }
             }
             return subCategory;
         }
         public static void ServiceCatalog_Category_subcategory_UpdateByAdmin(ServiceCatalog_category category)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalog_category sc_current = pd.ServiceCatalog_categories.Where(p => p.ID == category.ID).FirstOrDefault();
                 sc_current.CategoryName = category.CategoryName;
                 pd.SubmitChanges();
             }
         }
        #endregion


        #region Service Catelog associate

         public static void ServiceCatalog_Associate_insert(int type,int CatalogID, int CatalogNewID)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 ServiceCatalogItem_Associate sa = new ServiceCatalogItem_Associate();
                 sa.TypeofCatelog = type;
                 sa.CatalogID = CatalogID;
                 sa.AssociateCatalogID = CatalogNewID;
                 pd.ServiceCatalogItem_Associates.InsertOnSubmit(sa);
                 pd.SubmitChanges();

             }
         }

        #endregion

    }
}