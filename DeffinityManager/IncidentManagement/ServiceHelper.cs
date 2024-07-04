using System;
using System.Data;
using Incidents.Entity;
using Incidents.StateManager;
using System.Data.SqlClient;

namespace Incidents.DAL
{
    /// <summary>
    /// Summary description for ServiceHelper
    /// </summary>
    public class ServiceHelper
    {
        public ServiceHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Service Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Service service)
        {
            int recordsAffected = InsertUpdateHelper(service, false, ServiceCommands.cmdInsert);
            ServiceState.ClearServiceCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Service Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Service service)
        {
            int recordsAffected = InsertUpdateHelper(service, true, ServiceCommands.cmdUpdate);
            ServiceState.ClearServiceCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Service service, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", service.Id);
                    cmd.Parameters.AddWithValue("@IncidentID", service.IncidentID);
                    cmd.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                    cmd.Parameters.AddWithValue("@Qty", service.Qty);
                    cmd.Parameters.AddWithValue("@UnitPrice", service.UnitPrice);
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
        public static bool Delete(Service service)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ServiceCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", service.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            ServiceState.ClearServiceCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the ServiceCollection object for all the records</returns>
        public static ServiceCollection LoadAllServices()
        {
            ServiceCollection services = new ServiceCollection();
            if (ServiceState.ServiceCache == null)
            {
                Incident incident = IncidentState.IncidentSaver;
                int incidentID = 0;
                incidentID = (incident == null) ? 0 : incident.ID;
                LoadDataHelper(services, ServiceCommands.cmdSelectAll, incidentID);
                ServiceState.ServiceCache = services;
            }
            else
                services = (ServiceCollection)ServiceState.ServiceCache;
            return services;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(ServiceCollection services, string sqlStatement,int incidentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@incidentID", incidentId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Service service = GetService(reader);
                            services.Add(service);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Service GetService(SqlDataReader reader)
        {
            Service service = new Service();
            if (reader.IsClosed)
                reader.Read();
            service.Id = Convert.ToInt32(reader["Id"]);
            service.IncidentID = Convert.ToInt32(reader["IncidentID"]);
            service.Qty = Convert.ToInt32(reader["Qty"]);
            service.ServiceID= Convert.ToInt32(reader["ServiceId"]);
            service.UnitPrice= Convert.ToDouble(reader["UnitPrice"]);
            service.IncidentDetails = reader["IncidentDetails"].ToString();
            service.IncidentRequesterEmail = reader["IncidentRequesterEmail"].ToString();
            service.IncidentRequesterName = reader["IncidentRequesterName"].ToString();
            service.IncidentResolution = reader["IncidentResolution"].ToString();
            service.IncidentSubject = reader["IncidentSubject"].ToString();
            service.ServiceDescription = reader["ServiceDescription"].ToString();

            return service;
        }
    }
}
