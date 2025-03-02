using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SqlSrv = Microsoft.ApplicationBlocks.Data;

namespace Deffinity.DAL
{
	/// <summary>
	/// Table RFI_BOM
	/// 
	/// Generated by matricrix's C# Layer Builder
	/// 17/07/2009 15:59:27
	/// 
	/// Remark: Use this class to write custom code.
	/// </summary>
	public class RFI_BOM_DAL : RFI_BOM_Base_DAL
	{
		/// <summary>
		/// Constructor
		/// </summary>
		private RFI_BOM_DAL() { }

        /// <summary>
        /// Fill BOM Type list
        /// </summary>
        public static DataTable FillBOMType()
        {
            DataSet dsRFI_BOM = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DEFFINITY_RFI_BOMType_FILL");
            return dsRFI_BOM.Tables[0];
        }
        /// <summary>
        /// Fill entity list wrt ProjectReference
        /// </summary>
        public static DataTable Fill(int ProjectReference, int SubsectionID,int WorksheetID, string spName)
        {
            DataSet dsRFI_BOM = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, spName, new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@PROJECTREFERENCE", ProjectReference), new System.Data.SqlClient.SqlParameter("@SUBSECTIONID", SubsectionID), new System.Data.SqlClient.SqlParameter("@WorksheetID", WorksheetID) });
            return dsRFI_BOM.Tables[0];
        }
        public static DataTable GetCurrencies()
        {
            DataSet dsRFI_BOM = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "DEFFINITY_RFI_GETCURRENCIES");
            return dsRFI_BOM.Tables[0];
        }

	}
}
