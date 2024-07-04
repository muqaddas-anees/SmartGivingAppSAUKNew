using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for CustomerConfig
/// </summary>
namespace Deffinity.CustomerConfig
{
    public class CustomerConfig
    {
        string p_SectionName;
        int p_ID, p_PortfolioID,p_SectionID;
        bool p_Visible = false;

        #region Properties
        public int ID
        {
            get { return p_ID; }
            set { p_ID = value; }

        }
        public int PortfolioID
        {
            get { return p_PortfolioID; }
            set { p_PortfolioID = value; }
        }
        public int SectionID
        {
            get { return p_SectionID; }
            set { p_SectionID = value; }
        }
        public string SectionName
        {
            get { return p_SectionName; }
            set { p_SectionName = value; }
        }
        public bool Visible
        {
            get { return p_Visible; }
            set { p_Visible = value; }
        }
        #endregion

    }
    public class CustomerConfigManager
    {
        public static void CustomerConfig_Update(CustomerConfig cc)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CustomerConfig_Update", new SqlParameter("@ID", cc.ID), new SqlParameter("@Visible", cc.Visible));
        }

        public static DataTable CustomerConfig_Select(int PortfolioID)
        {
          return  SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DeffinitY_CustomerConfig_Select", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }

        public static void CustomerConfig_Insert(int PortfolioID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CustomerPortal_DLT_insert", new SqlParameter("@PortfolioID", PortfolioID));
        }

        public static void CustomerConfig_ApplyToAll(int PortfolioID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "CustomerConfigCopyToAll", new SqlParameter("@PortfolioID", PortfolioID));
        }
        public static void CustomerConfig_ApplyToAll(int PortfolioID,int SectionID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "CustomerConfigCopyToAllBySection", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@SectionID", SectionID));
        }
    }
}
