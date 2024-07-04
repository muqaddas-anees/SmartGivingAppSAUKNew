using System;
using System.Data;
using Health.Entity;
using Health.StateManager;
using System.Data.SqlClient;
using System.Web;
namespace Health.DAL
{
    public class HealthCheckListHelper
    {

        /// <summary>Inserts the record into the HealthCheckList Table</summary>
        /// <returns>The id(auto id of database) inserted.</returns>
        public static int Insert(HealthCheckList healthCheckList)
        {
            int newIdentity = InsertUpdateHelper(healthCheckList, false, HealthCheckListCommands.cmdInsert);
            HealthCheckListState.ClearHealthCheckItemsCache();
            return newIdentity;
        }


        /// <summary>
        /// Updates the record of the HealthCheckList Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(HealthCheckList healthCheckList)
        {
            int recordsAffected = InsertUpdateHelper(healthCheckList, true, HealthCheckListCommands.cmdUpdate);
            HealthCheckListState.ClearHealthCheckItemsCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(HealthCheckList healthCheckList, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", healthCheckList.Id);
                    cmd.Parameters.AddWithValue("@assignedTeam", healthCheckList.AssignedTeam);
                    cmd.Parameters.AddWithValue("@CheckListId", healthCheckList.HealthCheckListID);
                    cmd.Parameters.AddWithValue("@dateRaised", healthCheckList.DateRaised);
                    cmd.Parameters.AddWithValue("@Location", healthCheckList.LocationID);
                    cmd.Parameters.AddWithValue("@Issue", healthCheckList.Issue);
                    cmd.Parameters.AddWithValue("@Notes", healthCheckList.Notes);
                    cmd.Parameters.AddWithValue("@Status", healthCheckList.Status);
                    cmd.Parameters.AddWithValue("@RAG", healthCheckList.RAG);
                    
                    cmd.Parameters.AddWithValue("@Assignmember", healthCheckList.Assignmember);
                    //cmd.Parameters.AddWithValue("@DueDate", healthCheckList.DueDate);
                    if(string.IsNullOrEmpty(healthCheckList.IssueStatus))
                        cmd.Parameters.AddWithValue("@IssueStatus",DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@IssueStatus",healthCheckList.IssueStatus);

                    cmd.Parameters.AddWithValue("@iSChecklist", healthCheckList.iSChecklist);

                    conn.Open();
                    recordsAffected = Convert.ToInt32(cmd.ExecuteScalar());
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
        public static bool Delete(HealthCheckList healthCheckList)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(HealthCheckListCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", healthCheckList.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            HealthCheckListState.ClearHealthCheckItemsCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the HealthCheckListCollection object for all the records</returns>
        public static HealthCheckListCollection LoadAllHealthCheckLists()
        {
            HealthCheckListCollection healthCheckLists = new HealthCheckListCollection();
            if (HealthCheckListState.HealthCheckItemsCache == null)
            {
                HealthCheckList healthCheckList = HealthCheckListState.HealthCheckItemsSaver;
                LoadDataHelper(healthCheckLists, HealthCheckListCommands.cmdSelectAll, sessionKeys.PortfolioID,false);
                HealthCheckListState.HealthCheckItemsCache = healthCheckLists;
            }
            else
                healthCheckLists = (HealthCheckListCollection)HealthCheckListState.HealthCheckItemsCache;
            return healthCheckLists;
        }
        public static HealthCheckListCollection LoadAllHealthCheckLists_byUser(string section)
        {
            if (HttpContext.Current.Session["HealthSection"] == null)
                HttpContext.Current.Session["HealthSection"] = string.Empty;
            section = string.IsNullOrEmpty(section) ? "main" : section;
            if (section != "main")
            {
                HealthCheckListState.ClearHealthCheckItemsCache();
            }

            HealthCheckListCollection healthCheckLists = new HealthCheckListCollection();
            if (HealthCheckListState.HealthCheckItemsCache == null)
            {
                HealthCheckList healthCheckList = HealthCheckListState.HealthCheckItemsSaver;
                LoadDataHelper_ByUser(healthCheckLists, HealthCheckListCommands.cmdSelectAll, sessionKeys.PortfolioID, section, sessionKeys.UID);
                HttpContext.Current.Session["HealthSection"] = string.IsNullOrEmpty(section) ? "main" : section;
                HealthCheckListState.HealthCheckItemsCache = healthCheckLists;
            }
            else
                healthCheckLists = (HealthCheckListCollection)HealthCheckListState.HealthCheckItemsCache;

            return healthCheckLists;
        }

        public static HealthCheckListCollection LoadAllHealthCheckLists_byUser(string section, string sortExpression)
        {
            HealthCheckListCollection healthCheckLists = LoadAllHealthCheckLists_byUser(section);
            SortByColumn(sortExpression, healthCheckLists);
            return healthCheckLists;
        }
        public static HealthCheckListCollection LoadAllHealthCheckLists(int PortfolioId)
        {
            HealthCheckListCollection healthCheckLists = new HealthCheckListCollection();
            if (HealthCheckListState.HealthCheckItemsCache == null)
            {
                HealthCheckList healthCheckList = HealthCheckListState.HealthCheckItemsSaver;
                LoadDataHelper(healthCheckLists, HealthCheckListCommands.cmdSelectAll,PortfolioId, false);
                HealthCheckListState.HealthCheckItemsCache = healthCheckLists;
            }
            else
                healthCheckLists = (HealthCheckListCollection)HealthCheckListState.HealthCheckItemsCache;
            return healthCheckLists;
        }

        public static HealthCheckListCollection LoadAllHealthCheckListsByTeam(int teamID)
        {
            HealthCheckListCollection healthCheckLists = new HealthCheckListCollection();
            if (HealthCheckListState.HealthCheckItemsCache == null)
            {
                HealthCheckList healthCheckList = HealthCheckListState.HealthCheckItemsSaver;
                LoadDataHelper(healthCheckLists, HealthCheckListCommands.cmdSelectByTeamID, teamID,true);
                HealthCheckListState.HealthCheckItemsCache = healthCheckLists;
            }
            else
                healthCheckLists = (HealthCheckListCollection)HealthCheckListState.HealthCheckItemsCache;
            return healthCheckLists;
        }

        public static HealthCheckListCollection LoadAllHealthCheckLists(string sortExpression)
        {
            HealthCheckListCollection healthCheckLists = LoadAllHealthCheckLists();
            SortByColumn(sortExpression, healthCheckLists);
            return healthCheckLists;
        }

        public static void SortByColumn(string sortExpression, HealthCheckListCollection healthCheckList)
        {
            if (!string.IsNullOrEmpty(sortExpression))
            {
                switch (sortExpression)
                {
                    case "DateRaised":
                    case "DateRaised ASC":
                        healthCheckList.Sort(new HealthComparer("DateRaised"));
                        break;
                    case "DateRaised DESC":
                        healthCheckList.Sort(new HealthComparer("DateRaised"));
                        healthCheckList.Reverse();
                        break;
                    case "LocationName":
                    case "LocationName ASC":
                        healthCheckList.Sort(new HealthComparer("LocationName"));
                        break;
                    case "LocationName DESC":
                        healthCheckList.Sort(new HealthComparer("LocationName"));
                        healthCheckList.Reverse();
                        break;
                    case "HealthCheckTitle":
                    case "HealthCheckTitle ASC":
                        healthCheckList.Sort(new HealthComparer("HealthCheckTitle"));
                        break;
                    case "HealthCheckTitle DESC":
                        healthCheckList.Sort(new HealthComparer("HealthCheckTitle"));
                        healthCheckList.Reverse();
                        break;
                    case "AssignedTeamName":
                    case "AssignedTeamName ASC":
                        healthCheckList.Sort(new HealthComparer("AssignedTeamName"));
                        break;
                    case "AssignedTeamName DESC":
                        healthCheckList.Sort(new HealthComparer("AssignedTeamName"));
                        healthCheckList.Reverse();
                        break;
                    case "Status":
                    case "Status ASC":
                        healthCheckList.Sort(new HealthComparer("Status"));
                        break;
                    case "Status DESC":
                        healthCheckList.Sort(new HealthComparer("Status"));
                        healthCheckList.Reverse();
                        break;
                    case "IssueStatus":
                        healthCheckList.Sort(new HealthComparer("IssueStatus"));
                        break;
                    case "IssueStatus DESC":
                        healthCheckList.Sort(new HealthComparer("IssueStatus"));
                        healthCheckList.Reverse();
                        break;
                    case "PortfolioName":
                        healthCheckList.Sort(new HealthComparer("PortfolioName"));
                        break;
                    case "PortfolioName DESC":
                        healthCheckList.Sort(new HealthComparer("PortfolioName"));
                        healthCheckList.Reverse();
                        break;
                }
            }
        }

        public static HealthCheckList LoadHealthcheckByID(int healthCheckID)
        {
            foreach (HealthCheckList healthCheckList in HealthCheckListHelper.LoadAllHealthCheckLists())
            {
                if (healthCheckID == healthCheckList.Id)
                    return healthCheckList;
            }
            return null;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(HealthCheckListCollection healthCheckLists, string sqlStatement, int param,bool isByTeam)
        {
            int Pageid = sessionKeys.pageid;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pageid", Pageid);
                    if (isByTeam)
                        cmd.Parameters.AddWithValue("@TeamID", param);
                    else
                        cmd.Parameters.AddWithValue("@PortfolioID", param);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HealthCheckList healthCheckList = GetHealthCheckList(reader);
                            healthCheckLists.Add(healthCheckList);
                        }
                    }
                }
                conn.Close();
            }
        }

        private static void LoadDataHelper_ByUser(HealthCheckListCollection healthCheckLists, string sqlStatement, int param, string section,int userid)
        {
            int Pageid = sessionKeys.pageid;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PortfolioID", param);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@Pageid", Pageid);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HealthCheckList healthCheckList = GetHealthCheckList(reader);
                            healthCheckLists.Add(healthCheckList);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static HealthCheckList GetHealthCheckList(SqlDataReader reader)
        {
            HealthCheckList healthCheckList = new HealthCheckList();
            if (reader.IsClosed)
                reader.Read();
            healthCheckList.Id = Convert.ToInt32(reader["Id"]);
            healthCheckList.AssignedTeam = Convert.ToInt32(reader["AssignedTeam"]);
            healthCheckList.AssignedTeamName = reader["AssignedTeamName"].ToString();
            healthCheckList.HealthCheckTitle = reader["HealthCheckTitle"].ToString();
            healthCheckList.DateRaised = Convert.ToDateTime(reader["DateRaised"]);
            healthCheckList.HealthCheckListID = Convert.ToInt32(reader["HealthCheckListID"]);
            healthCheckList.LocationID = Convert.ToInt32(reader["LocationID"]);
            healthCheckList.LocationName = reader["LocationName"].ToString();
            healthCheckList.Issue = reader["Issue"].ToString();
            healthCheckList.Notes = reader["Notes"].ToString();
            healthCheckList.Status = reader["Status"].ToString();
            healthCheckList.IssueStatus = reader["IssueStatus"].ToString();
            healthCheckList.RAG = reader["RAG"].ToString();
            healthCheckList.Assignmember = Convert.ToInt32(reader["Assignmember"]);
            healthCheckList.PortfolioName = reader["PortfolioName"].ToString();
            healthCheckList.PortfolioID = Convert.ToInt32(reader["PortfolioID"]);
            healthCheckList.iSChecklist = Convert.ToBoolean(reader["iSChecklist"]);
            //healthCheckList. = Convert.ToDateTime(reader["DueDate"]);
            return healthCheckList;
        }
    }
}
