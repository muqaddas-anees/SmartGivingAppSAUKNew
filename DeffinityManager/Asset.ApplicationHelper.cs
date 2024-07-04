using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AssetConfigurator.Entity;

namespace AssetConfigurator.DAL
{
    public class ApplicationHelper
    {
        public void Create(int noOfApplications, int assetID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.ApplicationsCreate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    cmd.Parameters.AddWithValue("NoOfApplications", noOfApplications);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ApplicationCollection Select(int assetID)
        {
            ApplicationCollection applications = new ApplicationCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.ApplicationSelect", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AssetID", assetID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationEntity application = new ApplicationEntity();
                            application.Id = Convert.ToInt32(reader["ID"]);
                            application.ApplicationName = reader["ApplicationName"].ToString();
                            application.AssetId = assetID;
                            application.Department = reader["Department"].ToString();
                            application.KeyContact = reader["KeyContact"].ToString();
                            application.ServerName = reader["ServerName"].ToString();
                            application.Version = reader["Version"].ToString();
                            applications.Add(application);
                        }
                    }
                }
            }
            return applications;
        }

        public int Update(ApplicationEntity application)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Asset.ApplicationsUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", application.Id);
                    cmd.Parameters.AddWithValue("AssetId", application.AssetId);
                    cmd.Parameters.AddWithValue("ApplicationName", application.ApplicationName);
                    cmd.Parameters.AddWithValue("Department", application.Department);
                    cmd.Parameters.AddWithValue("KeyContact", application.KeyContact);
                    cmd.Parameters.AddWithValue("ServerName", application.ServerName);
                    cmd.Parameters.AddWithValue("Version", application.Version);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}