using System;
using System.Data;
using Incidents.Entity;
using System.Data.SqlClient;
using Incidents.StateManager;

namespace Incidents.DAL
{
    /// <summary>
    /// Gets the Risk that is related to the Change Control(which is related to the incident)
    /// </summary>
    public class RiskHelper
    {
        public RiskHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Risk Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Risk risk)
        {
            int recordsAffected = InsertUpdateHelper(risk, false, RiskCommands.cmdInsert);
            RiskState.ClearRiskCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Risk Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Risk risk)
        {
            int recordsAffected = InsertUpdateHelper(risk, true, RiskCommands.cmdUpdate);
            RiskState.ClearRiskCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Risk risk, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", risk.Id);
                    cmd.Parameters.AddWithValue("@assignedTo", risk.AssignedTo);
                    cmd.Parameters.AddWithValue("@changeControlID", risk.ChangeControlID);
                    cmd.Parameters.AddWithValue("@risk", risk.RiskDescription);
                    cmd.Parameters.AddWithValue("@rollBackPlan", risk.RollBackPlan);
                    cmd.Parameters.AddWithValue("@testPlan", risk.TestPlan);
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
        public static bool Delete(Risk risk)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(RiskCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", risk.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            RiskState.ClearRiskCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the RiskCollection object for all the records</returns>
        public static RiskCollection LoadAllRisks()
        {
            RiskCollection risks = new RiskCollection();
            if (RiskState.RiskCache == null)
            {
                LoadDataHelper(risks, RiskCommands.cmdSelectAll,0);
                RiskState.RiskCache = risks;
            }
            else
                risks = (RiskCollection)RiskState.RiskCache;
            return risks;
        }
        public static RiskCollection LoadRisksById(int id)
        {
            RiskCollection risks = new RiskCollection();
            if (RiskState.RiskCache == null)
            {
                LoadDataHelper(risks, RiskCommands.cmdSelectById,id);
                RiskState.RiskCache = risks;
            }
            else
                risks = (RiskCollection)RiskState.RiskCache;
            return risks;
        }
        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(RiskCollection risks, string sqlStatement,int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@changeControlID", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Risk risk = GetRisk(reader);
                            risks.Add(risk);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Risk GetRisk(SqlDataReader reader)
        {
            Risk risk = new Risk();
            if (reader.IsClosed)
                reader.Read();
            risk.Id = Convert.ToInt32(reader["Id"]);
            risk.AssignedTo = Convert.ToInt32(reader["AssignedTo"]);
            risk.RiskDescription = reader["RiskDescription"].ToString();
            risk.ChangeControlID = Convert.ToInt32(reader["ChangeControlID"]);
            risk.RollBackPlan = Convert.ToBoolean(reader["RollBackPlan"]);
            risk.TestPlan = Convert.ToBoolean(reader["TestPlan"]);
            risk.ResourceName = reader["resourceName"].ToString();
            return risk;
        }
    }
}
 