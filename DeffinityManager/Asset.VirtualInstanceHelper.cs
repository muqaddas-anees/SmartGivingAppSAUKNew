using System;
using System.Collections.Generic;
using System.Web;
using AssetConfigurator.Entity;
using System.Data;
using System.Data.SqlClient;

namespace AssetConfigurator.DAL
{
    public class VirtualInstanceHelper
    {
        public void Create(int assetID,int instances)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.VirtualInstancesInsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    cmd.Parameters.AddWithValue("Instances", instances);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VirtualInstanceCollection Select(int assetID)
        {
            VirtualInstanceCollection instances = new VirtualInstanceCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.VirtualInstancesSelect", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VirtualInstanceEntity instance = new VirtualInstanceEntity();
                            instance.Id = Convert.ToInt32(reader["Id"]);
                            instance.Applications = reader["Applications"].ToString();
                            instance.AssetId = Convert.ToInt32(reader["AssetID"]);
                            instance.Department = reader["Department"].ToString();
                            instance.DiskSpace = reader["DiskSpace"].ToString();
                            instance.InstanceName = reader["InstanceName"].ToString();
                            instance.IPAddresses = reader["IPAddresses"].ToString();
                            instance.KeyContacts = reader["KeyContact"].ToString();
                            instance.Memory = reader["Memory"].ToString();
                            instance.OperatingSystem = reader["OperatingSystem"].ToString();
                            instances.Add(instance);
                        }
                    }
                }
                return instances;
            }
        }

        public int Update(VirtualInstanceEntity vInstance)
        { 
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.VirtualInstancesUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", vInstance.Id);
                    cmd.Parameters.AddWithValue("Applications", vInstance.Applications);
                    cmd.Parameters.AddWithValue("AssetID", vInstance.AssetId);
                    cmd.Parameters.AddWithValue("Department", vInstance.Department);
                    cmd.Parameters.AddWithValue("DiskSpace", vInstance.DiskSpace);
                    cmd.Parameters.AddWithValue("InstanceName", vInstance.InstanceName);
                    cmd.Parameters.AddWithValue("IPAddresses", vInstance.IPAddresses);
                    cmd.Parameters.AddWithValue("KeyContact", vInstance.KeyContacts);
                    cmd.Parameters.AddWithValue("Memory", vInstance.Memory);
                    cmd.Parameters.AddWithValue("OperatingSystem", vInstance.OperatingSystem);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public VirtualInstanceHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}