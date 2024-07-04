using System;
using System.Data;
using System.Data.SqlClient;
using Incidents.Entity;
using Incidents.StateManager;

namespace Incidents.DAL
{

    /// <summary>
    /// The DAL class for the Mananing Journal.
    /// </summary>
    public class JournalHelper
    {
        public JournalHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Resource Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Journal journal)
        {
            int recordsAffected = InsertUpdateHelper(journal, false, JournalCommands.cmdInsert);
            JournalState.ClearJournalCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Journal Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Journal journal)
        {
            int recordsAffected = InsertUpdateHelper(journal, true, JournalCommands.cmdUpdate);
            JournalState.ClearJournalCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Journal journal, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", journal.Id);
                    cmd.Parameters.AddWithValue("@Date", journal.Date);
                    cmd.Parameters.AddWithValue("@Time", journal.Time);
                    cmd.Parameters.AddWithValue("@Notes", journal.Notes);
                    cmd.Parameters.AddWithValue("@AssignedTo", journal.AssignedTo);
                    cmd.Parameters.AddWithValue("@IncidentID", journal.IncidentID);
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
        public static bool Delete(Journal journal)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(JournalCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", journal.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            JournalState.ClearJournalCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the JournalCollection object for all the records</returns>
        public static JournalCollection LoadAllJournals()
        {
            JournalCollection journals = new JournalCollection();
            if (JournalState.JournalCache == null)
            {
                Incident incident = IncidentState.IncidentSaver;
                int incidentID = 0;
                incidentID = (incident == null) ? 0 : incident.ID;
                LoadDataHelper(journals, JournalCommands.cmdSelectAll, incidentID);
                JournalState.JournalCache = journals;
            }
            else
                journals = (JournalCollection)JournalState.JournalCache;
            return journals;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(JournalCollection journals, string sqlStatement, int IncidentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IncidentID", IncidentID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Journal journal = GetJournal(reader);
                            journals.Add(journal);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Journal GetJournal(SqlDataReader reader)
        {
            Journal journal = new Journal();
            if (reader.IsClosed)
                reader.Read();
            journal.Id = Convert.ToInt32(reader["Id"]);
            journal.AssignedTo = Convert.ToInt32(reader["AssignedTo"]);
            journal.Date = Convert.ToDateTime(reader["Date"]);
            journal.Notes = reader["Notes"].ToString();
            journal.Time = reader["Time"].ToString();
            journal.AssignedName = reader["AssignedName"].ToString();
            return journal;
        }
    }
}