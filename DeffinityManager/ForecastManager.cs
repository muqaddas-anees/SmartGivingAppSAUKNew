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


/// <summary>
/// Summary description for ForecastManager
/// </summary>
namespace Deffinity.ForcastManager
{
    public class ForecastManager
    {
        public ForecastManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Display list of Forcast based on projectreference
        /// <param name="ProjectReference"></param>  
        /// <param name="TermType"></param>
        /// <returns>Datatable which shows the forecast</returns>
        /// </summary>
        public static DataTable Forcast_Selectall(int ProjectReference)
        {
                return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORECAST_SELECTALL", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
        }

        /// <summary>
        /// Insert project forecast for the selected project and ternm
        /// <param name="ProjectReference"></param>  
        /// <param name="TermType"></param>
        /// <returns>insert forecast</returns>
        /// </summary>
        public static void Forcast_Insert(int ProjectReference, int TermType)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_INSERTFORECASTFORPROJECT", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@Termtype", TermType));
            
        }
        /// <summary>
        /// Update project forecast of the selected ID
        /// <param name="ID"></param>  
        /// <param name="Notes"></param>  
        ///  <param name="RemainderDate"></param>  
        ///  <param name="ActualSpend"></param>  
        /// <returns>update forecast</returns>
        /// </summary>
        public static void Forecast_Update(int ID, string Notes, DateTime RemainderDate, float ActualSpend)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORCAST_UPDATE",
                new SqlParameter("@ID", ID),
                new SqlParameter("@Notes", Notes),
                new SqlParameter("@RemainderDate", RemainderDate),
                new SqlParameter("@ActualSpend", ActualSpend)
                );
        }
        /// <summary>
        /// Delete project forecast of the selected ID
        /// <param name="ID"></param>  
        /// <returns>delete forecast</returns>
        /// </summary>
        public static void Forcast_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORECAST_DELETE", new SqlParameter("@ID", ID));
        }
        public static void Forecast_Insert_Ind(DateTime Duedate ,float forecastspend ,float actualspend , DateTime RemainderDate, string Notes, int ProjectReference)
        {
        
               SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_INSERTFORECAST_INDIVIDUAL",
                new SqlParameter("@Duedate", Duedate),
                new SqlParameter("@forecastspend", forecastspend),
                new SqlParameter("@actualspend", actualspend),
                new SqlParameter("@RemainderDate", RemainderDate),
                new SqlParameter("@Notes", Notes),
                new SqlParameter("@ProjectReference", ProjectReference)
                );
        }
        public static decimal Forecast_GetTotalForecast(int ProjectReference)
        {
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORECAST_GETFORECASTSPEND", new SqlParameter("@ProjectReference", ProjectReference)));
        }
        public static decimal Forecast_GetTotalActual(int ProjectReference)
        {
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORECAST_GETACTUAlSPEND", new SqlParameter("@ProjectReference", ProjectReference)));
        }
        public static decimal Forecast_GetTotalVariance(int ProjectReference)
        {
           return Convert.ToDecimal(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_FORECAST_GETVARIANCE", new SqlParameter("@ProjectReference", ProjectReference)));
        }
    }
}