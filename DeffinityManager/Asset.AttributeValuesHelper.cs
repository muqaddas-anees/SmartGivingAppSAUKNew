using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AssetConfigurator.Entity;

/// <summary>
/// Summary description for AssetConfigurator
/// </summary>
/// 
namespace AssetConfigurator.DAL
{
    public class ValuesHelper
    {

        public int Insert(ValuesEntity values)
        {
            using(SqlConnection conn=new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[ValuesInsert]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AttributeID", values.AttributeId);
                    cmd.Parameters.AddWithValue("@Value", values.AttributeValue);
                    cmd.Parameters.AddWithValue("@RecordID", values.UniqueIdentifier);
                    cmd.Parameters.AddWithValue("@AssetID", values.AssetID);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable Select(int assetID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.GetAssetValues", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AssetID", assetID );
                    conn.Open();
                    DataTable table = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }

        public int Update(ValuesEntity values)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[UpdateValues]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AssetID", values.AssetID);
                    cmd.Parameters.AddWithValue("@RecordID", values.UniqueIdentifier);
                    cmd.Parameters.AddWithValue("@AttributeName", values.AttributeName);
                    cmd.Parameters.AddWithValue("@Value", values.AttributeValue);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public ValuesHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}