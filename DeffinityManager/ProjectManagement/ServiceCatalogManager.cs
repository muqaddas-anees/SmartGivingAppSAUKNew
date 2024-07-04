using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ServiceCatalogManager
/// </summary>
namespace Deffinity.ServiceCatalogManager
{
    public class ServiceCatalogManager
    {
        public ServiceCatalogManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Get all Labours
        /// </summary>
        /// <param name="ProjectReference"></param>
        /// <param name="PortfolioID"></param>
        /// <returns></returns>
        /// 
        //SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByName",
        //    new SqlParameter("@Type", Type), new SqlParameter("@PortfolioID", int.Parse(contextKey)), new SqlParameter("@Category", Category),
        //    new SqlParameter("@SubCategory", SubCategory), new SqlParameter("@PreFix", prefixText)).Tables[0]
        public static DataTable ServiceCatalog_ByName(int Type,int PortfolioID,int Category,int SubCategory,string prefixText)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByName",
            new SqlParameter("@Type", Type), new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@Category", Category),
            new SqlParameter("@SubCategory", SubCategory), new SqlParameter("@PreFix", string.IsNullOrEmpty(prefixText)? string.Empty:prefixText )).Tables[0];
        }

        public static DataTable ServiceCatalog_ByVendor(string prefixText)
        {
            DataTable dt=new DataTable();
            try
            {
                
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByVendors",
                new SqlParameter("@PreFix", prefixText)).Tables[0];
               
                //return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByVendors",
                //new SqlParameter("@PreFix", prefixText)).Tables[0];
            }
            catch { }
            return dt;
        }
        public static DataTable LabourSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory, int Type, int pagetype, int vendorid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ServiceCatalog_Labourselect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@Type", Type), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        }
        public static DataTable MaterialSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory, int Type, int pagetype, int vendorid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ServiceCatalog_Materialselect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@Type", Type), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        }
        //public static DataTable ServicesSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory, int Type, int ServiceType, int pagetype, int vendorid)
        //{
        //    return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_CatalogServiceSelect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@Type", Type), new SqlParameter("@ServiceType", ServiceType), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        //}
        public static DataTable ServicesSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory, int Type, int ServiceType)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_CatalogServiceSelect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@Type", Type), new SqlParameter("@ServiceType", ServiceType)).Tables[0];
        }
        public static int InsertUserCartProject(int ProjectReference)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_updateProjectBOM", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ContractorID", sessionKeys.UID), new SqlParameter("@UserID", sessionKeys.CartID));
        }
        public static DataTable LabourSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory,int pagetype,int vendorid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ServiceCatalog_Labourselect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        }
        public static DataTable MaterialSelectAll(int ProjectReference, int PortfolioID, int category, int subcategory, int pagetype, int vendorid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ServiceCatalog_Materialselect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        }
        public static DataTable ServicesSelectAll_ByServiceType(int ProjectReference, int PortfolioID, int category, int subcategory, int ServiceType, int pagetype, int vendorid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_CatalogServiceSelect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortFolioID", PortfolioID), new SqlParameter("@Category", category), new SqlParameter("@SubCategory", subcategory), new SqlParameter("@ServiceType", ServiceType), new SqlParameter("@PageType", pagetype), new SqlParameter("@VendorID", vendorid)).Tables[0];
        }
        public static void DeleteSubCategory(int ID)
        {
            SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DeleteServiceSubCategory", new SqlParameter("@SubCategoryID", ID));
        }
        public static void DeleteCategory(int ID)
        {
            SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DeleteServiceCategory", new SqlParameter("@CategoryID", ID));

        }
        public static DataRow GetLabourItem(int ID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetLabourItem", new SqlParameter("@LabourID", ID)).Tables[0].Rows[0];
        }
        public static DataRow GetMaterialItem(int ID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetMaterialItem", new SqlParameter("@MaterialID", ID)).Tables[0].Rows[0];
        }
        public static DataRow GetServiceItem(int ID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceItem", new SqlParameter("@ServiceID", ID)).Tables[0].Rows[0];
        }

        public static int InsertLabourByProject(string Ids,int Projectreference,int type)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertLabourByproject", new SqlParameter("@StringIds", Ids), new SqlParameter("@ProjectReference", Projectreference), new SqlParameter("@Type", type));
            
        }
        public static int InsertMaterialsByProject(string Ids, int Projectreference, int type)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertMaterialsByproject", new SqlParameter("@StringIds", Ids), new SqlParameter("@ProjectReference", Projectreference), new SqlParameter("@Type", type));

        }
        public static int InsertServicesByProject(string Ids, int Projectreference, int type,int serviceType)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertServicesByproject", new SqlParameter("@StringIds", Ids), new SqlParameter("@ProjectReference", Projectreference), new SqlParameter("@Type", type), new SqlParameter("@ServiceType", serviceType));

        }
        public static int InsertSearchItemsByProject(string Ids,string TypeIds, int Projectreference, int type, int serviceType)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertSearchItemsByproject", new SqlParameter("@StringIds", Ids), new SqlParameter("@StringTypes", TypeIds), new SqlParameter("@ProjectReference", Projectreference), new SqlParameter("@Type", type));

        }
        #region Services
        //insert

        //update

        #endregion


        #region Project taskItems
        public static DataTable ProjectBudget_SelectByRef(int _ProjectRefference)
        {

            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectBudget_Select", new SqlParameter("@ProjectReference", _ProjectRefference)).Tables[0];
            return dt;
        }
        public static DataTable ProjectBudget_SelectTaskItem(int _ID,int _ProjectRefference)
        {
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectBudget_SelectTaskByID",
                new SqlParameter("@ID", _ID),new SqlParameter("@ProjectReference",_ProjectRefference)).Tables[0];
            return dt;
        }
        public static DataTable ProjectBudget_SelectProject(int _ProjectRefference)
        {
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectBudget_SelectProject", new SqlParameter("@ProjectReference", _ProjectRefference)).Tables[0];
            return dt;
        }
        public static DataTable ProjectBudget_SelectBOM(int _ProjectRefference,int TaskID)
        {
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectTaskBOM_select", 
                new SqlParameter("@ProjectReference", _ProjectRefference),new SqlParameter("@Taskid",TaskID)).Tables[0];
            return dt;
        }

        public static void ProjectBudget_UpdatInsertBOM(int _ProjectRefference, int TaskID,int 
            BOMID,int QtyReq,string type)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectTaskBOM_InsertUpdate",
                new SqlParameter("@ProjectReference", _ProjectRefference), new SqlParameter("@Taskid", TaskID),
                new SqlParameter("@QTY_Required", QtyReq), new SqlParameter("@BOMID", BOMID),
                 new SqlParameter("@Type", type));
            
        }

        public static void ProjectBudget_UpdateTaskItems(int ID, double fee, double cost, double resourceFee,
            double resourceCost,  double estimatedHrs)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectBudget_Update",
                new SqlParameter("@ID", ID), new SqlParameter("@Fee", fee), new SqlParameter("@Cost", cost),
                new SqlParameter("@ResourceFee", resourceFee), new SqlParameter("@ResourceCost", resourceCost),
                 new SqlParameter("@EstimatedHrs", estimatedHrs));
        }

        public static void ProjectBudget_UpdateProject(int _ProjectRefference, double projectFee,
            double BudgetedCost)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectBudget_UpdateProject",
                new SqlParameter("@ProjectReference", _ProjectRefference), new SqlParameter("@BudgetaryCost", projectFee)
                , new SqlParameter("@BuyingPrice", BudgetedCost));
        }


        #endregion
    }
}
