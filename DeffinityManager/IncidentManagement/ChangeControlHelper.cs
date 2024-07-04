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
    public class ChangeHelper
    {
        public ChangeHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Change Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static int Insert(Change change)
        {
            int recordsAffected = InsertUpdateHelper(change, false, ChangeCommands.cmdInsert);
            ChangeState.ClearChangeCache();
            return recordsAffected;
        }


        /// <summary>
        /// Updates the record of the Change Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Change change)
        {
            int recordsAffected = InsertUpdateHelper(change, true, ChangeCommands.cmdUpdate);
            ChangeState.ClearChangeCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Change change, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                    cmd.Parameters.AddWithValue("@ID", change.Id);
                    cmd.Parameters.AddWithValue("@incidentID", change.IncidentID);
                    cmd.Parameters.AddWithValue("@PortfolioID", change.PortfolioID);
                    cmd.Parameters.AddWithValue("@justification", change.Justification);
                    cmd.Parameters.AddWithValue("@resourceImpact", change.ResourceImpact);
                    cmd.Parameters.AddWithValue("@targetReleaseDate", change.TargetReleaseDate);
                    cmd.Parameters.AddWithValue("@title", change.Title);
                    cmd.Parameters.AddWithValue("@changeDescription", change.ChangeDescription);
                    cmd.Parameters.AddWithValue("@dateRaised", change.DateRaised);
                    cmd.Parameters.AddWithValue("@RequesterName", change.RequesterName);
                    cmd.Parameters.AddWithValue("@RequesterEmailID", change.RequesterEmailID);
                    cmd.Parameters.AddWithValue("@Status", change.Status);
                    cmd.Parameters.AddWithValue("@CategoryID", change.CategoryID);
                    cmd.Parameters.AddWithValue("@TargetStartDate", change.TargetStartDate);
                    cmd.Parameters.AddWithValue("@RaisedBy", change.RaisedBy);
                    cmd.Parameters.AddWithValue("@RelatesToProjectRef", change.RelatesToProjectRef);
                    cmd.Parameters.AddWithValue("@RelatesToServiceRequest", change.RelatesToservicerequest);
                    cmd.Parameters.AddWithValue("@EstimatedCost", change.EstimatedCost);
                    cmd.Parameters.AddWithValue("@EstimatedDaysRequired", change.EstimatedDaysRequired);
                   
                    conn.Open();
                    recordsAffected =Convert.ToInt16(cmd.ExecuteScalar());
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
        public static bool Delete(Change change)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ChangeCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", change.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            ChangeState.ClearChangeCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the ChangeCollection object for all the records</returns>
        public static ChangeCollection LoadAllChanges()
        {
            ChangeCollection changes = new ChangeCollection();
            if (ChangeState.ChangeCache == null)
            {
                LoadDataHelper(changes, ChangeCommands.cmdSelectAll,0);
                ChangeState.ChangeCache = changes;
            }
            else
                changes = (ChangeCollection)ChangeState.ChangeCache;
            return changes;
        }
        
        public static Change LoadChangesById(int id)
        {
            Change change = new Change();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ChangeCommands.cmdSelectById, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            change = GetChange(reader);
                        }
                    }
                }
                conn.Close();
            }
            return change;
        }

        public static ChangeCollection LoadChangesByPortfolio(string sortExpression)
        {
            ChangeCollection changes = LoadChangesByPortfolio();
            if (!string.IsNullOrEmpty(sortExpression))
            {
                switch (sortExpression)
                { 
                    case "ID":
                    case "ID ASC":
                        changes.Sort(new ChangeComparer("Id"));
                        break;
                    case "ID DESC":
                        changes.Sort(new ChangeComparer("Id"));
                        changes.Reverse();
                        break;
                    case "Title":
                    case "Title ASC":
                        changes.Sort(new ChangeComparer("Title"));
                        break;
                    case "Title DESC":
                        changes.Sort(new ChangeComparer("Title"));
                        changes.Reverse();
                        break;
                    case "ChangeDescription":
                    case "ChangeDescription ASC":
                        changes.Sort(new ChangeComparer("ChangeDescription"));
                        break;
                    case "ChangeDescription DESC":
                        changes.Sort(new ChangeComparer("ChangeDescription"));
                        changes.Reverse();
                        break;
                    case "Justification":
                    case "Justification ASC":
                        changes.Sort(new ChangeComparer("Justification"));
                        break;
                    case "Justification DESC":
                        changes.Sort(new ChangeComparer("Justification"));
                        changes.Reverse();
                        break;
                    case "DateRaised":
                    case "DateRaised ASC":
                        changes.Sort(new ChangeComparer("DateRaised"));
                        break;
                    case "DateRaised DESC":
                        changes.Sort(new ChangeComparer("DateRaised"));
                        changes.Reverse();
                        break;
                    case "TargetReleaseDate":
                    case "TargetReleaseDate ASC":
                        changes.Sort(new ChangeComparer("TargetReleaseDate"));
                        break;
                    case "TargetReleaseDate DESC":
                        changes.Sort(new ChangeComparer("TargetReleaseDate"));
                        changes.Reverse();
                        break;
                    case "ResourceImpact":
                    case "ResourceImpact ASC":
                        changes.Sort(new ChangeComparer("ResourceImpact"));
                        break;
                    case "ResourceImpact DESC":
                        changes.Sort(new ChangeComparer("ResourceImpact"));
                        changes.Reverse();
                        break;
                }
            }
            return changes;
        }

        public static ChangeCollection LoadChangesByPortfolio()
        {
            ChangeCollection changes = new ChangeCollection();
            Change change = new Change();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ChangeCommands.cmdSelectAllByPortfolio, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            change = GetChange(reader);
                            changes.Add(change);
                        }
                    }
                }
                conn.Close();
            }
            return changes;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(ChangeCollection changes, string sqlStatement,int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (id!=0)
                        cmd.Parameters.AddWithValue("@incidentID",id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Change change = GetChange(reader);
                            changes.Add(change);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Change GetChange(SqlDataReader reader)
        {
            Change change = new Change();
            if (reader.IsClosed)
                reader.Read();
            change.Id = Convert.ToInt32(reader["Id"]);
            change.IncidentID = Convert.ToInt32(reader["IncidentID"]);
            change.PortfolioID = Convert.ToInt32(reader.IsDBNull(reader.GetOrdinal("PortfolioID"))?0:reader["PortfolioID"]);
            change.ChangeDescription = reader["ChangeDescription"].ToString();
            change.DateRaised = Convert.ToDateTime(reader["DateRaised"]);
            change.Justification = reader["Justification"].ToString();
            change.ResourceImpact = reader["ResourceImpact"].ToString();
            change.TargetReleaseDate = Convert.ToDateTime(reader["TargetReleaseDate"]);
            change.Title = reader["Title"].ToString();
            change.RequesterName = reader["RequesterName"].ToString();
            change.RequesterEmailID = reader["RequesterEmailID"].ToString();
            change.Customer = reader["Customer"].ToString();
            return change;
        }
    }
}