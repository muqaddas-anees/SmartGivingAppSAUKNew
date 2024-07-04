using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Deffinity;
using SqlSrv = Microsoft.ApplicationBlocks.Data;

namespace DEFFINITY_DAL
{
    public class RFI_PANELISTSCORES_MAIN
    {
        public RFI_PANELISTSCORES_MAIN()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static int GetPanelistID(int ContractorID,int ProjectReference)
        {
            return Convert.ToInt32(SqlSrv.SqlHelper.ExecuteScalar(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DEFFINITY_RFI_PANELISTSCORECARD_GETPANELISTID", new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@CONTRACTORID", ContractorID), new System.Data.SqlClient.SqlParameter("@PROJECTREFERENCE", ProjectReference) }));
        }
        public static DataTable Fill(int ContractorID, int ProjectReference,int SubsectionID)
        {
            DataSet dsRFI_Main = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DEFFINITY_RFI_PANELISTSCORECARD_FILL", new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@CONTRACTORID", ContractorID), new System.Data.SqlClient.SqlParameter("@PROJECTREFERENCE", ProjectReference),new System.Data.SqlClient.SqlParameter("@SUBSECTIONID",SubsectionID) });
            return dsRFI_Main.Tables[0];
        }
    }
}