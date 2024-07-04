using System;
using System.Collections.Generic;
using System.Web;
using AssetConfigurator.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace AssetConfigurator.DAL
{
    public class AssetMasterHelper
    {
        public int Add(AssetMasterEntity assetMaster)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("IPAddress", assetMaster.IpAddress);
            parameters[1] = new SqlParameter("Ports", assetMaster.Ports);
            parameters[2] = new SqlParameter("Virtualisation", assetMaster.Virtualisation);
            parameters[3] = new SqlParameter("Applications", assetMaster.Applications);
            parameters[4] = new SqlParameter("AssetType", assetMaster.AssetType);
            parameters[5] = new SqlParameter("ImageID", assetMaster.Icon);
            return SqlHelper.ExecuteNonQuery(new SqlConnection(connectionString.retrieveConnString()), "Asset.MasterInsertion", parameters);
        }
        public int Edit(int ID)
        {
            return 0;
        }
        public int Delete(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[DeleteAttribute]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
