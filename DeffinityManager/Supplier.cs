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
/// Summary description for Supplier
/// </summary>
namespace Deffinity.SupplierManagement
{
    public class Supplier
    {
        public static int Supplier_insert(string SupplierName,string Email,string Telephone,string Address)
        {
            SqlParameter _outVal = new SqlParameter("@OutVal", SqlDbType.Int);
            _outVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Supplier_Insert", new SqlParameter("@SupplierName", SupplierName), new SqlParameter("@Email", Email), new SqlParameter("@Telephone", Telephone), new SqlParameter("@Address", Address), _outVal);
            return int.Parse(_outVal.Value.ToString());
        }

        public static int Supplier_Update(int ID, string SupplierName, string Email, string Telephone, string Address)
        {
            SqlParameter _outVal = new SqlParameter("@OutVal", SqlDbType.Int);
            _outVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Supplier_update", new SqlParameter("@ID", ID), new SqlParameter("@SupplierName", SupplierName), new SqlParameter("@Email", Email), new SqlParameter("@Telephone", Telephone), new SqlParameter("@Address", Address), _outVal);
            return int.Parse(_outVal.Value.ToString());
        }

        public static void Supplier_delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Supplier_delete", new SqlParameter("@ID", ID));
        }

        public static DataTable Supplier_Select()
        {
           return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Supplier_Select").Tables[0];
        }

    }
   
}