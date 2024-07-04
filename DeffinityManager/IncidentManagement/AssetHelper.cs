using System;
using System.Data;
using Incidents.Entity;
using Incidents.StateManager;
using System.Data.SqlClient;

namespace Incidents.DAL
{
    /// <summary>
    /// The DAL class for the incident management.
    /// </summary>
    public class AssetHelper
    {
        public AssetHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Asset Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Asset asset)
        {
            int recordsAffected = InsertUpdateHelper(asset, false, AssetCommands.cmdInsert);
            AssetState.ClearAssetCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Asset Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Asset asset)
        {
            int recordsAffected = InsertUpdateHelper(asset, true, AssetCommands.cmdUpdate);
            AssetState.ClearAssetCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Asset asset, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", asset.Id);
                    cmd.Parameters.AddWithValue("@AssetID", asset.AssetID);
                    cmd.Parameters.AddWithValue("@IncidentID", (asset.IncidentID==0)? sessionKeys.IncidentID:asset.IncidentID);
                    cmd.Parameters.AddWithValue("@Make", asset.Make );
                    cmd.Parameters.AddWithValue("@Model", asset.Model);
                    cmd.Parameters.AddWithValue("@DateOfChange", asset.DateOfChange);
                    cmd.Parameters.AddWithValue("@DetailsOfChange", asset.DetailsOfChange);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return recordsAffected;
        }

        /// <summary>
        /// Deletes the record by ID
        /// </summary>
        /// <param name="ID">ID of the record to be deleted</param>
        /// <returns>Returns true if deleted successfully.  If not returns false.</returns>
        public static bool Delete(Asset asset)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AssetCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", asset.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            AssetState.ClearAssetCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the AssetCollection object for all the records</returns>
        public static AssetCollection LoadAllAssets()
        {
            AssetCollection assets = new AssetCollection();
            if (AssetState.AssetCache == null)
            {
                Incident incident = IncidentState.IncidentSaver;
                int incidentID = 0;
                incidentID = (incident == null) ? 0 : incident.ID;
                LoadDataHelper(assets, AssetCommands.cmdSelectAll, incidentID);
                AssetState.AssetCache = assets;
            }
            else
                assets = (AssetCollection)AssetState.AssetCache;
            return assets;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(AssetCollection assets, string sqlStatement,int incidentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@incidentId", incidentId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Asset asset = GetAsset(reader);
                            assets.Add(asset);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Asset GetAsset(SqlDataReader reader)
        {
            Asset asset = new Asset();
            if (reader.IsClosed)
                reader.Read();
            asset.Id = Convert.ToInt32(reader["Id"]);
            asset.AssetID = reader["AssetId"].ToString();
            asset.IncidentID = Convert.ToInt32(reader["IncidentID"]);
            asset.DateOfChange = Convert.ToDateTime(reader["DateOfChange"]);
            asset.DetailsOfChange = reader["DetailsOfChange"].ToString();
            asset.Make = reader["Make"].ToString();
            asset.Model = reader["Model"].ToString();
            return asset;
        }
    }
}