using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for IncidentService_Price
/// </summary>
namespace Deffinity.IncidentService_Price_Manager
{
    public class IncidentService_Price
    {
        public static void IncidentService_Price_Update(int @IncidentID, double DiscountPercent, string Notes,string type,int PriceID,string Status,string InvoiceDescription="")
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_ServicePrice_Update", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@DiscountPercent", DiscountPercent), new SqlParameter("@Notes", Notes), new SqlParameter("@Type", type), new SqlParameter("@PriceID", PriceID), new SqlParameter("@Status", Status),new SqlParameter("@InvoiceDescription",InvoiceDescription));
        }
         public static SqlDataReader IncidentService_Price_Select(int IncidentID,string type,int PriceID)
        {
          return  SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Incident_ServicePrice_Select", new SqlParameter("@IncidentID", IncidentID),new SqlParameter("@Type",type), new SqlParameter("@PriceID", PriceID));
        }
        public static void BOM_Price_Update(int @IncidentID, double DiscountPercent, string Notes, string type)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DCBOMPrice_Update", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@DiscountPercent", DiscountPercent), new SqlParameter("@Notes", Notes), new SqlParameter("@Type", type));
        }
        public static SqlDataReader BOM_Price_Select(int IncidentID, string type)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DCBOMPrice_Select", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type));
        }
        public static void QuotationPrice_Update(int @IncidentID, double DiscountPercent, string Notes, string type,int Option,double DiscountPrice =0)
         {
             SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "QuotationPrice_Update", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@DiscountPercent", DiscountPercent), new SqlParameter("@Notes", Notes), new SqlParameter("@Type", type), new SqlParameter("@Option", Option), new SqlParameter("@DiscountPrice", DiscountPrice));
         }
         public static SqlDataReader Quotation_Price_Select(int IncidentID, string type,int Option)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "QuotationPrice_Select", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type),new SqlParameter("@Option",Option));
        }
        
    }
}