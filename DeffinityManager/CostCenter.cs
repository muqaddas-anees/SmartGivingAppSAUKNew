using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for CostCenter
/// </summary>
namespace Deffinity.CostCenterManager
{
    public class CostCenter
    {
        /// <summary>
        /// Insert cost center with portfolioid
        /// </summary>
        /// <param name="costCenter"></param>
        /// <param name="portfolioID"></param>
        public static void InsertCostCenter(string costCenter, int portfolioID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Insert into CostCentres (CostCentre,MaxBudgetValue,CurrencyCode,PortfolioID)values(@CostCenter,@MaxBudgetValue,@CurrencyCode,@PortfolioID)", new SqlParameter("@CostCenter", costCenter), new SqlParameter("@MaxBudgetValue", 0), new SqlParameter("@CurrencyCode", "GBP"), new SqlParameter("@PortfolioID", portfolioID));
        }
        public static void UpdateCostCenter(string costCenter, int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update CostCentres set CostCentre = @CostCenter where ID =@ID", new SqlParameter("@CostCenter", costCenter), new SqlParameter("@ID", ID));
        }
        //Delete from CostCentres where ID =@ID
        public static void DeleteCostCenter(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Delete from CostCentres where ID =@ID", new SqlParameter("@ID", ID));
        }
        public static DataTable CostCenter_SelectAll()
        {
           return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CostCenterSelect").Tables[0];
        }
    }
}