using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace Deffinity.Bindings
{
    public class DefaultDatabind
    {

        #region Default select Rows
        //select_Cap(1.Please select... 2.Please Select...3.ALL)
        public static DataTable AddSelectRow(string col1, string col2, DataTable dt, int Select_Cap)
        {
            DataRow row = dt.NewRow();
            row[col1] = "0";
            if (Select_Cap == 1)
                row[col2] = "Please select...";
            else if (Select_Cap == 2)
                row[col2] = "Please Select...";
            else if (Select_Cap == 3)
                row[col2] = "ALL";

            dt.Rows.InsertAt(row, 0);
            return dt;
        }
        #endregion

        #region Users
        //************************************************
        //Return's users 
        //parameter to check the active or not
        /// <param name="Active"></param>
        //************************************************
        public static DataTable UserSelectAll(bool Active)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ResourcesListALL",new SqlParameter("@Active",Active)).Tables[0];
        }
        public static DataTable UserSelectAll_Withselect(bool Active)
        {
            return AddSelectRow("ID", "ContractorName", UserSelectAll(Active),2);
        }  
        public static DataTable UserSelectAdmin()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ResourcesListAdmin").Tables[0];
        }
        public static DataTable UserSelectAdmin_Withselect()
        {
            return AddSelectRow("ID", "ContractorName", UserSelectAdmin(), 1);
        }
        public static DataTable UserSelect_SDUsers_Withselect()
        {
            return AddSelectRow("ID", "ContractorName", UserSelect_SDUsers(), 1);
        }
        public static DataTable UserSelect_SDUsers()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,ContractorName from Contractors where SID =9 order by ContractorName").Tables[0]; 
        }
        public static DataTable UserType()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ResourceType").Tables[0];
        }
        public static DataTable UserCompany()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "Select distinct Company,Company from users where Company <>''").Tables[0];
        }
        public static DataTable b_ExperienceClassification()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ExperienceClassificationSelectAll").Tables[0];
        }
        #endregion

        #region Locations
        //************************************************
        //Return's coutry list 
        //parameter to check the active or not
        //************************************************
        public static DataTable b_Country()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CountrySelect").Tables[0];
        }
        //************************************************
        //Return's city depending on country 
        //parameter
        /// <param name="CountryID"></param>
        //************************************************
        public static DataTable b_City(int CountryID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CitySelect", new SqlParameter("@CountryID", CountryID)).Tables[0];
        }
        //************************************************
        //Return's site depeinding on city 
        //parameter to check the active or not
        /// <param name="CityID"></param>
        //************************************************
        public static DataTable b_Site(int CityID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_SiteSelect", new SqlParameter("@CityID", CityID)).Tables[0];
        }

        public static DataTable b_SiteSelect_Portfilio(int portfolioid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Site_Select_Portfilo", new SqlParameter("@PortfolioID", portfolioid)).Tables[0];
        }
        public static DataTable b_SiteSelect_Portfilio_withSelect(int portfolioid)
        {
            return AddSelectRow("ID", "Site", b_SiteSelect_Portfilio(portfolioid), 1);
        }
        public static DataTable Sitelist(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_GetSiteList", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }
        #endregion

        #region Program
        //************************************************
        //Return's programme list depending owner/user
        //parameter
        /// <param name="CityID"></param>
        //************************************************
        public static DataTable b_Programme(int ProgrammeID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_pgmProgrammeList", new SqlParameter("@ProgrammeID", ProgrammeID)).Tables[0];
        }
        public static DataTable GetProgrammes(int ProgrammeID)
        {
            SqlParameter[] sqlparams = new SqlParameter[] { new SqlParameter("@PROGRAMMEID", ProgrammeID) };
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_GETPROGRAMMES", sqlparams).Tables[0];
        }
        public static DataTable ProgrammeList(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_GetProgrammeList", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];

        }
        public static DataTable ProgrammeList_Portfolio(int PortfolioID,int ProgramID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProgramSelect", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@ProgrameID", ProgramID)).Tables[0];

        }
        public static DataTable ProgrammeList_WithSelect(int PortfolioID, int ProgramID)
        {
            return AddSelectRow("ID", "OPERATIONSOWNERS", ProgrammeList_Portfolio(PortfolioID, ProgramID), 1);
        }
        //to select all programe pass 0
        public static DataTable ProgrammeList_AllWithSelect(int PortfolioID)
        {
            return AddSelectRow("ID", "OPERATIONSOWNERS", ProgrammeList_Portfolio(PortfolioID, 0), 1);
        } 
        #endregion

        #region Portfolio
        //************************************************
        //Return's portfolio list depending owner/user
        //parameter
        /// <param name="CityID"></param>
        //************************************************
        public static DataTable b_Portfolio(int CityID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_SiteSelect", new SqlParameter("@CityID", CityID)).Tables[0];
        }
        //************************************************
        //Return's Item status only 1,2,3
        //parameter        
        //************************************************
        public static DataTable b_Portfolio()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_Portfolio_Select").Tables[0];
        }
        public static DataTable b_Portfolio_Byuser(string section)
        {
            
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_Portfolio_ByUser",new SqlParameter("@userid",sessionKeys.UID),new SqlParameter("@section",string.IsNullOrEmpty(section)?"main":section)).Tables[0];
        }
        public static DataTable b_Portfolio_All()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_Portfolio_SelectAll").Tables[0];
        }

        public static DataTable Portfolio_All_Withselect()
        {
            return AddSelectRow("ID", "PortFolio", b_Portfolio_All(), 1);
        }
        public static DataTable Portfolio_Withselect()
        {
            return AddSelectRow("ID", "PortFolio", b_Portfolio(), 1);
        }  
        #endregion

        #region ItemStatus
        //************************************************
        //Return's All Item status
        //parameter        
        //************************************************
        public static DataTable b_ItemStatusAll()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Status from Itemstatus").Tables[0];
        }
        //************************************************
        //Return's Item status only 1,2,3
        //parameter        
        //************************************************
        public static DataTable b_ItemStatus()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Status from Itemstatus where ID in(1,2,3)").Tables[0];
        }
        
        //************************************************
        //Return's Item status only 1,2,3
        //parameter        
        //************************************************
        public static DataTable b_Issue_ItemStatus()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "Select ID,Status from ItemStatus where ID in (1,5,3,6)").Tables[0];
        }
        public static DataTable b_Issue_ItemStatus_withSelect()
        {
            return AddSelectRow("ID", "Status", b_Issue_ItemStatus(), 1);
        }
        //************************************************
        //Return's Item status only 1,2,3,4
        //parameter        
        //************************************************
        public static DataTable b_ProjectItemStatus()
        {
            DataTable ProjectItemStatus = HttpContext.Current.Cache["ProjectItemStatus"] as DataTable;

            if (ProjectItemStatus == null)
            {
                ProjectItemStatus = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Status from Itemstatus where ID in(1,2,3,4)").Tables[0];
                HttpContext.Current.Cache.Insert("ProjectItemStatus", ProjectItemStatus);

            }
            return ProjectItemStatus;
        }
        #endregion

        #region RAG Status
        //add list items
        public static ListItemCollection b_RagStatus()
        {
            ListItemCollection LitemsCollection = new ListItemCollection();
            LitemsCollection.Add(Constants.ddlDefaultBind(false));
            LitemsCollection.Add(new ListItem("GREEN"));
            LitemsCollection.Add(new ListItem("RED"));
            LitemsCollection.Add(new ListItem("AMBER"));
            
            return LitemsCollection;
        }
        public static DataTable b_RAGSections()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectionsSelectAll").Tables[0];
        }
        #endregion

        #region check list
        public static DataTable b_Checklist(int Projectreference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetChecklist", new SqlParameter("@ProjectRefrence", Projectreference)).Tables[0];
        }
        public static DataTable b_ChecklistbyType(int ChecklistType)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetChecklistbyType", new SqlParameter("@CheckListType", ChecklistType)).Tables[0];
        }
        public static DataTable b_ChecklistbyType__Withselect(int ChecklistType)
        {
            return AddSelectRow("ID", "Description", b_ChecklistbyType(ChecklistType), 1);
        }
        public static DataTable b_Checklist_Withselect(int Projectreference)
        {
            return AddSelectRow("ID", "Description", b_Checklist(Projectreference),1);
        }
        public static DataTable b_PortfolioQAchecklist(int Projectreference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfolioQAChecklist", new SqlParameter("@ProjectReference", Projectreference)).Tables[0];
        }
        #endregion
       
        #region Issue type
        public static DataTable b_IssueType()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_IssuetypeSelectAll").Tables[0];
        }
        public static DataTable b_IssueTypeWithALL()
        {
            return AddSelectRow("ID", "IssueTypeName", b_IssueType(), 3);
        }
        public static DataTable b_IssueTypeWithSelect()
        {
            return AddSelectRow("ID", "IssueTypeName", b_IssueType(), 2);
        }
        #endregion
                
        #region Ratetype

        public static DataTable RateType()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT * FROM RateType").Tables[0];

        }
        #endregion

        #region Category
        public static DataTable CategoryAssociatedToPortfolio(int portfolioid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text,  "SELECT ProjectCategory.CategoryID, ProjectCategory.CategoryName, PortfolioSLA.Portfolio FROM ProjectCategory INNER JOIN PortfolioSLA ON ProjectCategory.CategoryID = PortfolioSLA.Category where Portfolio =@PortfolioID",new SqlParameter("@PortfolioID",portfolioid)).Tables[0];

        }
        public static DataTable CategoryAssociatedToPortfolio_withAll(int portfolioid)
        {
            return AddSelectRow("CategoryID", "CategoryName", CategoryAssociatedToPortfolio(portfolioid), 3);

        }
        public static DataTable CategoryAssociatedToPortfolio_withSelect(int portfolioid)
        {
            return AddSelectRow("CategoryID", "CategoryName", CategoryAssociatedToPortfolio(portfolioid), 2) as DataTable;
        }
        public static DataTable b_Category()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_ADMINCATEGORY").Tables[0];
        }
        #endregion

        #region Time sheet entry
        //time sheet
        public static DataTable TimeSheetEntry()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID as EntryTypeID,EntryType from TimesheetEntryType").Tables[0];

        }
        public static DataTable TimeSheetEntry_withSelect()
        {
            return AddSelectRow("EntryTypeID", "EntryType", TimeSheetEntry(), 1) as DataTable;
        }
        #endregion

        #region ProjectRefrerence
        //projectref with name
        public static DataTable PrjectTitleWithProjectReference(int ResourceID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTitelwithReference", new SqlParameter("@ResourceID", ResourceID)).Tables[0];

        }
        public static DataTable PrjectTitleWithProjectReference_withSelect(int ResourceID)
        {
            return AddSelectRow("ProjectReference", "ProjectTitle", PrjectTitleWithProjectReference(ResourceID), 1) as DataTable;
        }
        public static DataTable PrjectTitleWithProjectReference()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTitelwithReference_All").Tables[0];
        }
        public static DataTable PrjectTitleWithProjectReference_withSelectAll()
        {
            return AddSelectRow("ProjectReference", "ProjectTitle", PrjectTitleWithProjectReference(), 1) as DataTable;
        }
        public static string GetProjectTitle(int projectreference)
        {
            if (projectreference > 0)
            {
                return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(ProjectTitle,'') from Projects where ProjectReference = @pref", new SqlParameter("@pref", projectreference)).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        //Deffinity_ProjectTitelwithReference_All
        #endregion

        #region ProjectPipeline
        public static bool CountProjectOrders()
        {
            bool retVal = false;
            int cnt = 0;

            //string CacheStr = "Ordercnt" + sessionKeys.PortfolioID.ToString();
            
            if (sessionKeys.PortfolioID != 0)
            {
                //if (HttpContext.Current.Cache[CacheStr] == null)
                //{
                //    HttpContext.Current.Cache[CacheStr] = int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("Select Count(*) from Projects where ProjectStatusID= 8 and Portfolio = {0}", sessionKeys.PortfolioID)).ToString());
                //}

                cnt = int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("Select Count(*) from Projects where ProjectStatusID= 8 and Portfolio = {0}", sessionKeys.PortfolioID)).ToString());
                //int.Parse(HttpContext.Current.Cache[CacheStr].ToString());
            }
            else
            {
                //if (HttpContext.Current.Cache[CacheStr] == null)
                //{
                //    HttpContext.Current.Cache[CacheStr] = int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "Select Count(*) from Projects where ProjectStatusID= 8").ToString());
                //}
                cnt = int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "Select Count(*) from Projects where ProjectStatusID= 8").ToString());
 
                //int.Parse(HttpContext.Current.Cache[CacheStr].ToString());
            }

            if (cnt > 0)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }
        #endregion

        #region currency
        public static DataTable CurrencyList()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT [ID], [CurrencyName], [ShortCurrencyName] FROM [CurrencyList] order by CurrencyName").Tables[0];
        }
        public static DataTable CurrencyList_withSelect()
        {
            return AddSelectRow("ShortCurrencyName", "CurrencyName", CurrencyList(), 1) as DataTable;
        }
        public static DataTable ActiveCurrencyList()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "dbo.deffinity_getCurrencyList").Tables[0];
        }
        public static DataTable ActiveCurrencyList_withSelect()
        {
            return AddSelectRow("ID", "ShortCurrencyName", ActiveCurrencyList(), 1) as DataTable;
        }
        #endregion

		#region Incident
        public static DataTable SelectAreaByPortfolio(int portfolioid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT ID,Name from incident_Area union select 0 ID,' Please select...' Name" ).Tables[0];

        }
        #endregion
    }
}
