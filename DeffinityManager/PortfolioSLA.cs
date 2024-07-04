using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace Deffinity.PortfolioSLAmanager
{
    /// <summary>
    /// Summary description for PortfolioSLA
    /// </summary>
    public class PortfolioSLA
    {
        public static DataTable PortfolioSLA_Select(int PortfolioID,int Category)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_PortfolioSLA_Select",
                new SqlParameter("@Portfolio", PortfolioID), new SqlParameter("@Category", Category)).Tables[0];
        }
        public static int PortfolioSLA_Insert(int TypeofRequestID,int CategoryID,int SubCategoryID,int PortfolioID ,string Timetoresolve,string Description,
         string TimeType, int TimeInHand, string TimeInHandEx)
        {
            SqlParameter outvalue = new SqlParameter("@outvalue", SqlDbType.Int, 8);
            outvalue.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_PortfolioSLA_Add",
                new SqlParameter("@TypeofRequestID", TypeofRequestID),
                new SqlParameter("@CategoryID", CategoryID),
                new SqlParameter("@SubCategotyID", SubCategoryID),
                new SqlParameter("@PortfolioID", PortfolioID),
                new SqlParameter("@Timetoresolve", Timetoresolve),
                new SqlParameter("@Description", Description),
                new SqlParameter("@TimeType", TimeType),
                new SqlParameter("@TimeInHand", TimeInHand),
                new SqlParameter("@TimeInHandEx", TimeInHandEx)
               ,outvalue);
            return int.Parse(outvalue.Value.ToString());
                    
        }
//        @Portfolio int,@Category nvarchar(50),@Timetoresolve int,    
//@Description nvarchar(500),@TimeType nvarchar(50),@ID int,
//@TimeInHand int,@TimeInHandEx char(1),
//@outvalue int output   

        public static int PortfolioSLA_Update(int ID, string Timetoresolve, string Description, string TimeType, int TimeInHand, string TimeInHandEx)
        {
            SqlParameter outvalue = new SqlParameter("@outvalue", SqlDbType.Int, 8);
            outvalue.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_PortfolioSLA_Update",
                new SqlParameter("@ID", ID),
                new SqlParameter("@Timetoresolve", Timetoresolve),
                new SqlParameter("@Description", Description),
                new SqlParameter("@TimeType", TimeType),
                new SqlParameter("@TimeInHand", TimeInHand),
                new SqlParameter("@TimeInHandEx", TimeInHandEx),
                outvalue);

//            @Portfolio int,@Category nvarchar(50),@Timetoresolve int,    
//@Description nvarchar(500),@TimeType nvarchar(50),@ID int,
//@TimeInHand int,@TimeInHandEx char(1),
            return int.Parse(outvalue.Value.ToString());

        }
        public static void InsertPortfolioSLACategory(string CategotyName,int PortfolioID,string Timetoresolve,string Description,string TimeType)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_PortfolioSLA_Add", new SqlParameter("@CategotyName", CategotyName),
                new SqlParameter("@PortfolioID",PortfolioID),new SqlParameter("@Timetoresolve",Timetoresolve),
                new SqlParameter("@Description", Description), new SqlParameter("@TimeType", TimeType));    
        }

        public static void InsertPortfolioSLACategory(string CategotyName, int PortfolioID)
        {
            InsertPortfolioSLACategory(CategotyName, PortfolioID, string.Empty, string.Empty, string.Empty);
        }
        public static int InsertMasterCategory(string Name,int PortfolioID)
        {
            SqlParameter outvalue = new SqlParameter("@outvalue", SqlDbType.Int, 8);
            outvalue.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_MasterCategory_Insert", new SqlParameter("@Name", Name), new SqlParameter("@PortfolioID", PortfolioID),outvalue);
            return int.Parse(outvalue.Value.ToString());
        }
        public static DataTable GetMasterCategory(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetMasterCategory", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }
        public static DataTable GetProjectMasterCategory(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetProjectMasterCategory", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }
        public static DataTable CategoryAssociatedToPortfolio(int portfolioid, int MasterID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT ProjectCategory.CategoryID, ProjectCategory.CategoryName FROM ProjectCategory where  MasterID=@MasterID", new SqlParameter("@MasterID", MasterID)).Tables[0];
        }
        public static void DeleteMasterCategory(int MasterCategoryID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_MasterCategory_Delete", new SqlParameter("@ID", MasterCategoryID));
        }
        public static void SubCategoryInsert(int MasterCategoryID,string SubCategoryName)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectCategory_insert", new SqlParameter("@MasterCategoryID", MasterCategoryID), new SqlParameter("@SubCategoryName", SubCategoryName));
        }
        
 
    }
}