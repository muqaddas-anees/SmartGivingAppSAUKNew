using System;
using System.Data;
using Health.Entity;
using Health.StateManager;
using System.Data.SqlClient;

namespace Health.DAL
{
    public class PortfolioHealthCheckHelper
    {
        /// <summary>Inserts the record into the PortfolioHealthCheck Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(PortfolioHealthCheck portfolioHealthCheck,int importID)
        {
            //This is not the id of the newly generated one.  The is the id of the other health check.
            portfolioHealthCheck.Id = importID;
            int recordsAffected = InsertUpdateHelper(portfolioHealthCheck, false, PortfolioHealthCheckCommands.cmdInsert);
            PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the PortfolioHealthCheck Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(PortfolioHealthCheck portfolioHealthCheck)
        {
            int recordsAffected = InsertUpdateHelper(portfolioHealthCheck, true, PortfolioHealthCheckCommands.cmdUpdate);
            PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(PortfolioHealthCheck portfolioHealthCheck, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@Id", portfolioHealthCheck.Id);
                    else
                        cmd.Parameters.AddWithValue("@EmailImportID", portfolioHealthCheck.Id);
                    cmd.Parameters.AddWithValue("@EmailDistributionList", portfolioHealthCheck.EmailDistributionList);
                    cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
                    cmd.Parameters.AddWithValue("@Title", portfolioHealthCheck.Title);
                    cmd.Parameters.AddWithValue("@CheckListID", portfolioHealthCheck.CheckListID);
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
        public static bool Delete(PortfolioHealthCheck portfolioHealthCheck)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(PortfolioHealthCheckCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", portfolioHealthCheck.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            PortfolioHealthCheckState.ClearPortfolioHealthCheckCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the PortfolioHealthCheckCollection object for all the records</returns>
        public static PortfolioHealthCheckCollection LoadAllPortfolioHealthChecks()
        {
            PortfolioHealthCheckCollection portfolioHealthChecks = new PortfolioHealthCheckCollection();
            if (PortfolioHealthCheckState.PortfolioHealthCheckCache == null)
            {
                PortfolioHealthCheck portfolioHealthCheck = PortfolioHealthCheckState.PortfolioHealthCheckStateSaver;
                LoadDataHelper(portfolioHealthChecks, PortfolioHealthCheckCommands.cmdSelectAll, sessionKeys.PortfolioID);
                PortfolioHealthCheckState.PortfolioHealthCheckCache = portfolioHealthChecks;
            }
            else
                portfolioHealthChecks = (PortfolioHealthCheckCollection)PortfolioHealthCheckState.PortfolioHealthCheckCache;
            return portfolioHealthChecks;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(PortfolioHealthCheckCollection portfolioHealthChecks, string sqlStatement, int portfolioID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PortfolioID", portfolioID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PortfolioHealthCheck portfolioHealthCheck = GetPortfolioHealthCheck(reader);
                            portfolioHealthChecks.Add(portfolioHealthCheck);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static PortfolioHealthCheck GetPortfolioHealthCheck(SqlDataReader reader)
        {
            PortfolioHealthCheck portfolioHealthCheck = new PortfolioHealthCheck();
            if (reader.IsClosed)
                reader.Read();
            portfolioHealthCheck.Id = Convert.ToInt32(reader["Id"]);
            portfolioHealthCheck.EmailDistributionList = reader["EmailDistributionList"].ToString();
            portfolioHealthCheck.PortfolioID = Convert.ToInt32(reader["PortfolioID"]);
            portfolioHealthCheck.PortfolioName = reader["PortfolioName"].ToString();
            portfolioHealthCheck.Title = reader["Title"].ToString();
            portfolioHealthCheck.CheckListText = reader["CheckListText"].ToString();
            portfolioHealthCheck.CheckListID = Convert.ToInt32(reader["CheckListID"]);
            return portfolioHealthCheck;
        }
    }
}