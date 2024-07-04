using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AssetConfigurator.Entity;

namespace AssetConfigurator.DAL
{
    public class PortsHelper
    {

        public int Insert(int noOfPorts,int assetID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.InsertPort", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    cmd.Parameters.AddWithValue("NoOfPorts", noOfPorts);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public PortsCollection Select(int assetID)
        {
            PortsCollection ports = new PortsCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.SelectPorts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AssetID", assetID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PortsEntity Port = new PortsEntity();
                            Port.AssetID = assetID;
                            Port.ConnectedEquipment = (reader["ConnectedEquipment"] != DBNull.Value) ? reader["ConnectedEquipment"].ToString() : string.Empty;
                            Port.Id = (reader["Id"] != DBNull.Value) ? Convert.ToInt32(reader["Id"]) : 0;
                            Port.PortNumber = (reader["Port#"] != DBNull.Value) ? Convert.ToString(reader["Port#"]) : string.Empty;
                            Port.Side = (reader["Side"] != DBNull.Value) ? Convert.ToChar(reader["Side"]) : ' ';
                            ports.Add(Port);
                        }
                    }
                }
                return ports;
            }
        }

        public int Update(PortsEntity port)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.UpdatePorts", conn))
                {
                    cmd.Parameters.AddWithValue("ID", port.Id);
                    cmd.Parameters.AddWithValue("ConnectedEquipment", port.ConnectedEquipment);
                    cmd.Parameters.AddWithValue("Port#", port.PortNumber);
                    cmd.Parameters.AddWithValue("Side", port.Side);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}