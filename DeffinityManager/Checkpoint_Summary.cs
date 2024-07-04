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

namespace Deffinity.CheckpointSummaryManager
{
    /// <summary>
    /// Summary description for Checkpoint_Summary
    /// </summary>
    public class Checkpoint_Summary
    {
        public static DataTable GetCheckpoint_Projectlist(int ResourceID,int sid)
        {
            DataTable DT = new DataTable();
            try
            {
                DT = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_CheckPoint_Projects_Admin", new SqlParameter("@ResourceID", ResourceID), new SqlParameter("@SID", sid)).Tables[0];
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return DT;
        }
        public static DataTable GetCheckpoint_Projectlist_Customer(int portfolioID)
        {
            DataTable DT = new DataTable();
            try
            {
                DT = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_CheckPoint_Projects_Customer", new SqlParameter("@PortfolioID", portfolioID)).Tables[0];
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return DT;
        }
    }
}
