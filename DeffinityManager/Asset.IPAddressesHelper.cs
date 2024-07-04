using System;
using System.Collections.Generic;
using System.Web;
using AssetConfigurator.Entity;
using System.Data;
using System.Data.SqlClient;
using AssetConfigurator.DAL;

namespace AssetConfigurator.DAL
{
    public class IPAddressesHelper
    {

        public void Insert(int assetID,string ipAddress,int start,int end)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.InsertIPAddresses", conn))
                {
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    cmd.Parameters.AddWithValue("IPAddress", ipAddress);
                    cmd.Parameters.AddWithValue("Start", start);
                    cmd.Parameters.AddWithValue("End", end);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IPAddressesCollection Select(int assetID)
        {
            IPAddressesCollection ipAddresses = new IPAddressesCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[IPAddressesSelect]", conn))
                {
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IPAddressesEntity ipAddress = new IPAddressesEntity();
                            ipAddress.Id = Convert.ToInt32(reader["Id"]);
                            ipAddress.IpAddress = reader["IPAddress"].ToString();
                            if (reader["Port#"] != DBNull.Value)
                                ipAddress.Port = reader["Port#"].ToString();
                            else
                                ipAddress.Port = string.Empty;
                            if (reader["Description"] != DBNull.Value)
                                ipAddress.Description = reader["Description"].ToString();
                            else
                                ipAddress.Description = string.Empty;
                            ipAddress.AssetID = assetID;
                            ipAddresses.Add(ipAddress);
                        }
                    }
                }
            }
            return ipAddresses;
        }

        public int Update(IPAddressesEntity ipAddress)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[IPAddressesUpdate]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ipAddress.Id);
                    cmd.Parameters.AddWithValue("AssetID", ipAddress.AssetID);
                    cmd.Parameters.AddWithValue("Description", ipAddress.Description);
                    cmd.Parameters.AddWithValue("IPAddress", ipAddress.IpAddress);
                    cmd.Parameters.AddWithValue("Port#", ipAddress.Port);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public IPAddressesHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}