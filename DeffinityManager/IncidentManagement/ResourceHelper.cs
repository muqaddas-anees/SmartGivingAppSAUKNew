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
    public class ResourceHelper
    {
        public ResourceHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Resource Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Resource resource)
        {
            int recordsAffected = InsertUpdateHelper(resource, false, ResourceCommands.cmdInsert);
            ResourceState.ClearResourceCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Resource Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Resource resource)
        {
            int recordsAffected = InsertUpdateHelper(resource, true, ResourceCommands.cmdUpdate);
            ResourceState.ClearResourceCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Resource resource, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", resource.Id);
                    cmd.Parameters.AddWithValue("@IncidentID", resource.IncidentID);
                    cmd.Parameters.AddWithValue("@MemberID", resource.MemberID);
                    cmd.Parameters.AddWithValue("@Activity", resource.Activity);
                    cmd.Parameters.AddWithValue("@Duration", resource.Duration);
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
        public static bool Delete(Resource resource)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ResourceCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", resource.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            ResourceState.ClearResourceCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the ResourceCollection object for all the records</returns>
        public static ResourceCollection LoadAllResources()
        {
            ResourceCollection resources = new ResourceCollection();
            if (ResourceState.ResourceCache == null)
            {
                Incident incident = IncidentState.IncidentSaver;
                int incidentID = 0;
                incidentID = (incident == null) ? 0 : incident.ID;
                LoadDataHelper(resources, ResourceCommands.cmdSelectAll,incidentID);
                ResourceState.ResourceCache = resources;
            }
            else
                resources = (ResourceCollection)ResourceState.ResourceCache;
            return resources;
        }
        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(ResourceCollection resources, string sqlStatement,int IncidentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IncidentID",IncidentID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Resource resource = GetResource(reader);
                            resources.Add(resource);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Resource GetResource(SqlDataReader reader)
        {
            Resource resource = new Resource();
            if (reader.IsClosed)
                reader.Read();
            resource.Id = Convert.ToInt32(reader["Id"]);
            resource.IncidentID=Convert.ToInt32(reader["IncidentID"]);
            resource.Activity = reader["Activity"].ToString();
            resource.Duration = reader["Duration"].ToString();
            resource.MemberID = Convert.ToInt32(reader["MemberID"]);
            resource.MemberName = reader["MemberName"].ToString();
            return resource;
        }
    }
}