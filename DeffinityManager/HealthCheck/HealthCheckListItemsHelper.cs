using System;
using System.Data;
using System.Data.SqlTypes;
using Health.Entity;
using Health.StateManager;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;

namespace Health.DAL
{
    public class HealthCheckListItemsHelper
    {
        /// <summary>Inserts the record into the HealthCheckListItems Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(HealthCheckListItems healthCheckListItems)
        {
            int recordsAffected = InsertUpdateHelper(healthCheckListItems, false, HealthCheckListItemsCommands.cmdInsert);
            HealthCheckListItemsState.ClearHealthCheckListItemsCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the HealthCheckListItems Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(HealthCheckListItems healthCheckListItems)
        {
            int recordsAffected = InsertUpdateHelper(healthCheckListItems, true, HealthCheckListItemsCommands.cmdUpdate);
            HealthCheckListItemsState.ClearHealthCheckListItemsCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(HealthCheckListItems healthCheckListItems, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                    {
                        cmd.Parameters.AddWithValue("@Id", healthCheckListItems.Id);
                        cmd.Parameters.AddWithValue("@IssueDate", healthCheckListItems.IssueDate);
                    }
                    cmd.Parameters.AddWithValue("@AssignedTeam", healthCheckListItems.AssignedTeam);
                    cmd.Parameters.AddWithValue("@HealthCheck", healthCheckListItems.HealthCheck);
                    cmd.Parameters.AddWithValue("@HealthCheckListID", healthCheckListItems.HealthCheckListID);
                    cmd.Parameters.AddWithValue("@IsChecked", healthCheckListItems.IsChecked);
                    cmd.Parameters.AddWithValue("@Issues", healthCheckListItems.Issues);
                    cmd.Parameters.AddWithValue("@Notes", healthCheckListItems.Notes);
                    cmd.Parameters.AddWithValue("@Assignee", healthCheckListItems.Assignee);
                    cmd.Parameters.AddWithValue("@Status", healthCheckListItems.Status);
                    cmd.Parameters.AddWithValue("@DateCompleted", healthCheckListItems.DateCompleted);
                    cmd.Parameters.AddWithValue("@DueDate",healthCheckListItems.DueDate);
                    cmd.Parameters.AddWithValue("@Rag", healthCheckListItems.RAG);

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
        public static bool Delete(HealthCheckListItems healthCheckListItems)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(HealthCheckListItemsCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", healthCheckListItems.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            HealthCheckListItemsState.ClearHealthCheckListItemsCache();
            return ((recordsAffected > 0) ? true : false);
        }

        public static HealthCheckListItemsCollection LoadAllHealthCheckListItems()
        {
            HealthCheckListItemsCollection healthCheckListItemsCollection = new HealthCheckListItemsCollection();
            if (HealthCheckListItemsState.HealthCheckListItemsCache == null)
            {
                HealthCheckListItems healthCheckListItems = HealthCheckListItemsState.HealthCheckListItemsStateSaver;
                LoadDataHelper(healthCheckListItemsCollection, HealthCheckListItemsCommands.cmdSelectAllWithoutID, new SqlCommand());
                HealthCheckListItemsState.HealthCheckListItemsCache = healthCheckListItemsCollection;
            }
            else
                healthCheckListItemsCollection = (HealthCheckListItemsCollection)HealthCheckListItemsState.HealthCheckListItemsCache;
            return healthCheckListItemsCollection;
        }

        /// <summary>
        /// Loads the Health Check List Items based on the title
        /// </summary>
        /// <param name="healthCheckListTitleID"></param>
        /// <returns></returns>
        public static HealthCheckListItemsCollection LoadAllHealthCheckListItemsByTitle(int healthCheckListTitleID)
        {
            HealthCheckListItemsCollection healthCheckListItemsCollection = new HealthCheckListItemsCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@HealthCheckTitleID", healthCheckListTitleID);
            LoadDataHelper(healthCheckListItemsCollection, HealthCheckListItemsCommands.cmdSelectAllByTitle, cmd);
            HealthCheckListItemsState.HealthCheckListItemsCache = healthCheckListItemsCollection;
            return healthCheckListItemsCollection;
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the HealthCheckListItemsCollection object for all the records</returns>
        public static HealthCheckListItemsCollection LoadAllHealthCheckListItems(int healthCheckId)
        {
            HealthCheckListItemsCollection healthCheckListItemsCollection = new HealthCheckListItemsCollection();
            HealthCheckListItemsState.ClearHealthCheckListItemsCache();
            if (HealthCheckListItemsState.HealthCheckListItemsCache == null)
            {
                HealthCheckListItems healthCheckListItems = HealthCheckListItemsState.HealthCheckListItemsStateSaver;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@HealthCheckID", healthCheckId);
                LoadDataHelper(healthCheckListItemsCollection, HealthCheckListItemsCommands.cmdSelectAll, cmd);
                HealthCheckListItemsState.HealthCheckListItemsCache = healthCheckListItemsCollection;
            }
            else
                healthCheckListItemsCollection = (HealthCheckListItemsCollection)HealthCheckListItemsState.HealthCheckListItemsCache;
            return healthCheckListItemsCollection;
        }
        public static HealthCheckListItemsCollection GetHealthCheckListItemsById(int healthCheckId)
        {
            HealthCheckListItemsCollection healthCheckListItemsCollection = new HealthCheckListItemsCollection();
            HealthCheckListItemsState.ClearHealthCheckListItemsCache();
            if (HealthCheckListItemsState.HealthCheckListItemsCache == null)
            {
                HealthCheckListItems healthCheckListItems = HealthCheckListItemsState.HealthCheckListItemsStateSaver;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@HealthCheckID", healthCheckId);
                LoadDataHelper(healthCheckListItemsCollection, "HealthCheckListItemsByID", cmd);
                HealthCheckListItemsState.HealthCheckListItemsCache = healthCheckListItemsCollection;
            }
            else
                healthCheckListItemsCollection = (HealthCheckListItemsCollection)HealthCheckListItemsState.HealthCheckListItemsCache;
            return healthCheckListItemsCollection;
        }
        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(HealthCheckListItemsCollection healthCheckListItemss, string sqlStatement, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HealthCheckListItems healthCheckListItems = GetHealthCheckListItems(reader);
                            healthCheckListItemss.Add(healthCheckListItems);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static HealthCheckListItems GetHealthCheckListItems(SqlDataReader reader)
        {
            HealthCheckListItems healthCheckListItems = new HealthCheckListItems();
            if (reader.IsClosed)
                reader.Read();
            healthCheckListItems.Id = Convert.ToInt32(reader["Id"]);
            healthCheckListItems.AssignedTeam = Convert.ToInt32(reader["AssignedTeam"]);
            healthCheckListItems.AssignedTeamName = reader["AssignedTeamName"].ToString();
            healthCheckListItems.HealthCheck = reader["HealthCheck"].ToString();
            healthCheckListItems.HealthCheckListID = Convert.ToInt32(reader["HealthCheckListID"]);
            healthCheckListItems.IsChecked = reader.GetSqlBoolean(reader.GetOrdinal("Checked"));
            healthCheckListItems.Issues = reader["Issues"].ToString();
            healthCheckListItems.Notes = reader["Notes"].ToString();
            healthCheckListItems.Assignee = Convert.ToInt32(reader["Assignee"]);
            healthCheckListItems.AssigneeName = reader["AssigneeName"].ToString();
            healthCheckListItems.Status = reader["Status"].ToString();
            healthCheckListItems.DateCompleted = reader.GetSqlDateTime(reader.GetOrdinal("DateCompleted"));
            healthCheckListItems.IssueDate = reader.GetSqlDateTime(reader.GetOrdinal("IssueDate"));
            healthCheckListItems.DueDate =Convert.ToDateTime(reader["DueDate"]);//reader.GetSqlDateTime(reader.GetOrdinal("DueDate")); 
            string rag = reader["HCLItemsRag"].ToString();
            if (rag == null || rag =="")
            {
                healthCheckListItems.RAG = "Select...";
            }
            else
            {
                healthCheckListItems.RAG = reader["HCLItemsRag"].ToString();
            }
            return healthCheckListItems;
        }

        public static int IsExist(int HealthCheckId)
        {

            int ret = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(HealthCheckRecurrence.cmdIsExist, conn))
                {
                    SqlParameter prm = new SqlParameter("@return", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HealthCheckID", HealthCheckId);
                    cmd.Parameters.Add(prm);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ret = int.Parse(prm.Value.ToString());
                }
            }
            return ret;
        }

        public static void InsertUpdateRecurr(HealthCheckRecurr healthCheckRecur)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(HealthCheckRecurrence.cmdInsertUpdate, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartTime", healthCheckRecur.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", healthCheckRecur.EndTime);
                    cmd.Parameters.AddWithValue("@Duration", healthCheckRecur.Duration);
                    cmd.Parameters.AddWithValue("@RecurWeekOn", healthCheckRecur.RecurWeekOn);
                    cmd.Parameters.AddWithValue("@WeekDayName", healthCheckRecur.WeekDayName);
                    cmd.Parameters.AddWithValue("@StartDate", healthCheckRecur.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", healthCheckRecur.EndDate);
                    cmd.Parameters.AddWithValue("@EndAfter", healthCheckRecur.EndAfter);
                    cmd.Parameters.AddWithValue("@ReCurrencePattern", healthCheckRecur.ReCurrencePattern);
                    cmd.Parameters.AddWithValue("@ReCurrenceRange", healthCheckRecur.ReCurrenceRange);
                    cmd.Parameters.AddWithValue("@HealthCheckID", healthCheckRecur.HealthCheckID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public static HealthCheckRecurr SelectById(int HealthCheckId)
        {
            HealthCheckRecurr entity = new HealthCheckRecurr();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(HealthCheckRecurrence.cmdSelectByID, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HealthCheckID", HealthCheckId);
                    SqlDataAdapter dad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dad.Fill(dt);
                    entity.EndAfter = Convert.ToInt32(dt.Rows[0]["EndAfter"].ToString());
                    entity.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString());
                    entity.EndTime = Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString());
                    entity.ReCurrencePattern = int.Parse(dt.Rows[0]["ReCurrencePattern"].ToString());
                    entity.ReCurrenceRange = int.Parse(dt.Rows[0]["ReCurrenceRange"].ToString());
                    entity.RecurWeekOn = int.Parse(dt.Rows[0]["RecurWeekOn"].ToString());
                    entity.StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                    entity.StartTime = Convert.ToDateTime(dt.Rows[0]["StartTime"].ToString());
                    entity.WeekDayName = dt.Rows[0]["WeekDayName"].ToString();
                    entity.Duration = dt.Rows[0]["Duration"].ToString();
                    
                }
            }

            return entity;
        }

        public static DataTable GetMembers(int TeamID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT Contractors.ID, Contractors.ContractorName FROM TeamMember INNER JOIN Contractors ON TeamMember.Name = Contractors.ID where Contractors.Status ='Active' and TeamID=@teamid order by Contractors.ContractorName", new SqlParameter("@teamid", TeamID)).Tables[0];
        }
    }
}