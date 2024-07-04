using System;
using System.Data;
using System.Data.SqlClient;
using Incidents.Entity;
using Incidents.StateManager;
using Microsoft.ApplicationBlocks.Data;

namespace Incidents.DAL
{
    /// <summary>
    /// The DAL class for the incident management.
    /// </summary>
    public class IncidentHelper
    {
        public static string priorityname = string.Empty;

        public static string Priorityname
        {
            get { return priorityname; }
            set { priorityname = value; }
        }


        /// <summary>Inserts the record into the Incident Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static int Insert(Incident incident)
        {
            int recordInserted = InsertUpdateHelper(incident,false,IncidentCommands.cmdInsert);
            IncidentState.ClearIncidentCache();
            return ((recordInserted > 0) ? recordInserted : 0);
        }

        /// <summary>
        /// Updates the record of the Incident Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static int Update(Incident incident)
        {
            int recordInserted = InsertUpdateHelper(incident,true,IncidentCommands.cmdUpdate);
            IncidentState.ClearIncidentCache();
            //IncidentState.IncidentSaver = null;
            return ((recordInserted > 0) ? recordInserted : 0);
        }

        private static int InsertUpdateHelper(Incident incident,bool isUpdate,string sqlCommand)
        {
            int recordInserted = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", incident.ID);
                    cmd.Parameters.AddWithValue("@PortfolioID", incident.PortfolioID);
                    cmd.Parameters.AddWithValue("@ProjectCategoryID", incident.ProjectCategoryID);
                    cmd.Parameters.AddWithValue("@ContractorID", incident.ContractorID);
                    cmd.Parameters.AddWithValue("@Visible", incident.Visible);
                    cmd.Parameters.AddWithValue("@DateLogged", incident.DateLogged);
                    cmd.Parameters.AddWithValue("@CategorySLA", incident.CategorySLA);
                    if (incident.SLATarget == null)
                        incident.SLATarget = string.Empty;
                    cmd.Parameters.AddWithValue("@SLATarget", incident.SLATarget);
                    cmd.Parameters.AddWithValue("@SLAMet", incident.SLAMet);
                    cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                    cmd.Parameters.AddWithValue("@AssignedTo", incident.AssignedTo);
                    cmd.Parameters.AddWithValue("@Status", incident.Status);
                    cmd.Parameters.AddWithValue("@StartTime", incident.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", incident.EndTime);
                    cmd.Parameters.AddWithValue("@AssignedToTeam", incident.AssignedToTeam);
                    cmd.Parameters.AddWithValue("@WeekCommencingDate", incident.WeekCommencingDate);
                    cmd.Parameters.AddWithValue("@RequesterName", incident.RequesterName);
                    cmd.Parameters.AddWithValue("@RequesterEmail", incident.RequesterEmail);
                    cmd.Parameters.AddWithValue("@RequesterTelephone", incident.RequesterTelephone);
                    cmd.Parameters.AddWithValue("@RequesterDeskLocation", incident.RequesterDeskLocation);
                    cmd.Parameters.AddWithValue("@RequesterDepartmentID", incident.RequesterDepartmentID);
                    cmd.Parameters.AddWithValue("@SiteID", incident.SiteID);
                    cmd.Parameters.AddWithValue("@PriorityLevel", incident.PriorityLevel);
                    cmd.Parameters.AddWithValue("@Resolution", incident.Resolution);
                    cmd.Parameters.AddWithValue("@Subject", incident.Subject);
                    cmd.Parameters.AddWithValue("@Details", incident.Details);
                    cmd.Parameters.AddWithValue("@InHandTime", incident.InHandTime);
                    cmd.Parameters.AddWithValue("@WorkDays", incident.WorkDays);
                    cmd.Parameters.AddWithValue("@WorkHours", incident.WorkHours);
                    cmd.Parameters.AddWithValue("@WorkMinutes", incident.WorkMinutes);
                    cmd.Parameters.AddWithValue("@Case_Custom1", incident.Case_Custom1);
                    cmd.Parameters.AddWithValue("@Case_Custom2", incident.Case_Custom2);
                    cmd.Parameters.AddWithValue("@Case_Custom3", incident.Case_Custom3);
                    cmd.Parameters.AddWithValue("@Case_Custom4", incident.Case_Custom4);
                    cmd.Parameters.AddWithValue("@Case_Custom5", incident.Case_Custom5);
                    cmd.Parameters.AddWithValue("@Case_Custom6", incident.Case_Custom6);
                    cmd.Parameters.AddWithValue("@Case_Custom7", incident.Case_Custom7);
                    cmd.Parameters.AddWithValue("@Case_Custom8", incident.Case_Custom8);
                    cmd.Parameters.AddWithValue("@Case_Custom9", incident.Case_Custom9);
                    cmd.Parameters.AddWithValue("@Case_Custom10", incident.Case_Custom10);
                    cmd.Parameters.AddWithValue("@Case_Custom11", incident.Case_Custom11);
                    cmd.Parameters.AddWithValue("@Case_Custom12", incident.Case_Custom12);
                    cmd.Parameters.AddWithValue("@Case_Custom13", incident.Case_Custom13);
                    cmd.Parameters.AddWithValue("@Case_Custom14", incident.Case_Custom14);
                    cmd.Parameters.AddWithValue("@Case_Custom15", incident.Case_Custom15);
                    cmd.Parameters.AddWithValue("@Case_Custom16", incident.Case_Custom16);
                    cmd.Parameters.AddWithValue("@OutOfHours", incident.IsOutOfHours);
                    cmd.Parameters.AddWithValue("@Projectreference", incident.ProjectReference);
                    cmd.Parameters.AddWithValue("@Program", incident.Program);
                    cmd.Parameters.AddWithValue("@POnumber", incident.POnumber);
                    cmd.Parameters.AddWithValue("@QuoteStatus", incident.QuoteStatus);
                    cmd.Parameters.AddWithValue("@QuoteLineStatus", incident.QuoteLineStatus);
                    cmd.Parameters.AddWithValue("@ProjectCategoryMasterID", incident.ProjectCategoryMasterID);
                    cmd.Parameters.AddWithValue("@Area", incident.Area);
                    cmd.Parameters.AddWithValue("@Callout", incident.IsCallout);
                    cmd.Parameters.AddWithValue("@Notes", incident.Notes);
                    cmd.Parameters.AddWithValue("@BuildingID", incident.BuildingID);
                    cmd.Parameters.AddWithValue("@FloorID", incident.FloorID);
                    cmd.Parameters.AddWithValue("@LoggedBy", incident.LoggedBy);
                    conn.Open();
                    recordInserted = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
            return recordInserted;
        }

        /// <summary>
        /// Deletes the record by ID
        /// </summary>
        /// <param name="ID">ID of the record to be deleted</param>
        /// <returns>Returns true if deleted successfully.  If not returns false.</returns>
        public static bool Delete(Incident incident)
        {
            int recordsAffected=0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(IncidentCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", incident.ID);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            IncidentState.ClearIncidentCache();
            return ((recordsAffected > 0) ? true : false);
        }


        //Reads through the datareader. Returns a single record.
        private static Incident GetIncident(SqlDataReader reader)
        {
            Incident incident = new Incident();
            if (reader.IsClosed)
                reader.Read();
            incident.ID = Convert.ToInt32(reader["ID"]);
            incident.PortfolioID = Convert.ToInt32(reader["portfolioID"]);
            incident.ProjectCategoryID = Convert.ToInt32(reader["ProjectCategoryID"]);
            incident.ContractorID = Convert.ToInt32(reader["ContractorID"]);
            incident.Visible = Convert.ToBoolean(reader["visible"]);
            incident.DateLogged = Convert.ToDateTime(reader["DateLogged"]);
            incident.CategorySLA = reader["CategorySLA"].ToString();
            incident.SLATarget = reader["SLATarget"].ToString();
            incident.SLAMet = Convert.ToBoolean(reader["SLAMet"]);
            incident.IncidentType = reader["IncidentType"].ToString();
            incident.AssignedTo = Convert.ToInt32(reader["AssignedTo"]);
            incident.Status = reader["Status"].ToString();
            incident.StartTime = Convert.ToDateTime(reader["StartTime"]);
            incident.EndTime = Convert.ToDateTime(reader["EndTime"]);
            incident.AssignedToTeam = Convert.ToInt32(reader["AssignedToTeam"]);
            incident.WeekCommencingDate = Convert.ToDateTime(reader["WeekCommencingDate"]);
            incident.RequesterName = reader["RequesterName"].ToString();
            incident.RequesterEmail = reader["RequesterEmail"].ToString();
            incident.RequesterTelephone = reader["RequesterTelephone"].ToString();
            incident.RequesterDeskLocation = reader["RequesterDeskLocation"].ToString();
            incident.RequesterDepartmentID = Convert.ToInt32(reader["RequesterDepartmentID"]);
            incident.SiteID = Convert.ToInt32(reader["SiteID"]);
            incident.PriorityLevel = reader["PriorityLevel"].ToString();
            incident.PriorityName = reader["PriorityName"].ToString();
            incident.Resolution = reader["Resolution"].ToString();
            incident.Subject = reader["Subject"].ToString();
            incident.Details = reader["Details"].ToString();
            incident.SiteName = reader["SiteName"].ToString();
            incident.PortfolioName = reader["PortfolioName"].ToString();
            incident.WorkDays = Convert.ToInt16( reader["WorkDays"]);
            incident.WorkHours = Convert.ToInt16(reader["WorkHours"]);
            incident.WorkMinutes = Convert.ToInt16(reader["WorkMinutes"]);
            incident.AssigneeName = reader["AssigneeName"].ToString();
            incident.Case_Custom1 = reader["Case_Custom1"].ToString();
            incident.Case_Custom2 = reader["Case_Custom2"].ToString();
            incident.Case_Custom3 = reader["Case_Custom3"].ToString();
            incident.Case_Custom4 = reader["Case_Custom4"].ToString();
            incident.Case_Custom5 = reader["Case_Custom5"].ToString();
            incident.Case_Custom6 = reader["Case_Custom6"].ToString();
            incident.Case_Custom7 = reader["Case_Custom7"].ToString();
            incident.Case_Custom8 = reader["Case_Custom8"].ToString();
            incident.Case_Custom9 = reader["Case_Custom9"].ToString();
            incident.Case_Custom10 = reader["Case_Custom10"].ToString();
            incident.Case_Custom11 = reader["Case_Custom11"].ToString();
            incident.Case_Custom12 = reader["Case_Custom12"].ToString();
            incident.Case_Custom13 = reader["Case_Custom13"].ToString();
            incident.Case_Custom14 = reader["Case_Custom14"].ToString();
            incident.Case_Custom15 = reader["Case_Custom15"].ToString();
            incident.Case_Custom16 = reader["Case_Custom16"].ToString();
            incident.IsOutOfHours = Convert.ToBoolean(reader["OutOfHours"]);
            incident.ProjectReference = Convert.ToInt32(reader["ProjectReference"]);
            incident.Program = Convert.ToInt32(reader["Program"]);
            incident.POnumber = reader["POnumber"].ToString();
            incident.QuoteStatus = Convert.ToInt32(string.IsNullOrEmpty(reader["QuoteStatus"].ToString()) ? "0" : reader["QuoteStatus"].ToString());
            incident.QuoteLineStatus = Convert.ToInt32(string.IsNullOrEmpty(reader["QuoteLineStatus"].ToString()) ? "0" : reader["QuoteLineStatus"].ToString());
            incident.ProjectCategoryMasterID = Convert.ToInt32(reader["ProjectCategoryMasterID"]);
            incident.Area = Convert.ToInt32(reader["Area"]);
            incident.IsCallout = Convert.ToBoolean(reader["Callout"]);
            incident.Notes = reader["Notes"].ToString();
            incident.BuildingID = Convert.ToInt32(reader["BuildingID"]);
            incident.FloorID = Convert.ToInt32(reader["FloorID"]);
            incident.LoggedBy = Convert.ToInt32(reader["LoggedBy"]);
            incident.LoggedByName = reader["LoggedByName"].ToString();
            incident.InHandSLAMet = Convert.ToBoolean(reader["InHandSLAMet"]);
            try
            {
                Priorityname = reader["PriorityName"].ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            try
            {
                incident.InHandTime = Convert.ToDateTime(reader["InHandTime"]);
            }
            catch 
            {
                incident.InHandTime = Convert.ToDateTime("01/01/1900");
            }
            try
            {
                incident.CloseTime = Convert.ToDateTime(reader["ClosedTime"]);
            }
            catch
            {
                incident.CloseTime = Convert.ToDateTime("01/01/1900");
            }
            incident.RAG_SLA = reader["RAG_SLA"].ToString();
            incident.CategoryName = reader["CategoryName"].ToString();
            incident.SubCategoryName = reader["SubCategoryName"].ToString();
            incident.AreaName = reader["AreaName"].ToString();
            incident.AssignedToTeamName = reader["AssignedToTeamName"].ToString();
            return incident;
        }

        ////Adds the Incident to the IncidentCollection and build the table.
        //private static void LoadDataHelper(IncidentCollection incidents, string sqlStatement,string Status,string type,string @ContractorID)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
        //            //Adds the parameter for status
        //            if (!string.IsNullOrEmpty(Status))
        //                cmd.Parameters.AddWithValue("@Status", Status);
        //            if (!string.IsNullOrEmpty(type))
        //                cmd.Parameters.AddWithValue("@IncidentType", type);
        //            if (!string.IsNullOrEmpty(ContractorID))
        //                cmd.Parameters.AddWithValue("@ContractorID", ContractorID);
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Incident incident = GetIncident(reader);
        //                    incidents.Add(incident);
        //                }
        //            }
        //        }
        //        conn.Close();
        //    }
        //}
		
		 private static void LoadDataHelper(IncidentCollection incidents, string sqlStatement, string Status, string type, string @ContractorID,int PortfolioID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PortfolioID", PortfolioID);
                    //Adds the parameter for status
                    if (!string.IsNullOrEmpty(Status))
                        cmd.Parameters.AddWithValue("@Status", Status);
                    if (!string.IsNullOrEmpty(type))
                        cmd.Parameters.AddWithValue("@IncidentType", type);
                    if (!string.IsNullOrEmpty(ContractorID))
                        cmd.Parameters.AddWithValue("@ContractorID", ContractorID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Incident incident = GetIncident(reader);
                            incidents.Add(incident);
                        }
                    }
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Gets all the Incidents from the database
        /// </summary>
        /// <returns>Returns the IncidentCollection object for all the records</returns>
        public static IncidentCollection LoadAllIncidents(int portfolioID)
        {
            IncidentCollection incidents = new IncidentCollection();
            if (IncidentState.IncidentCache == null)
            {
                LoadDataHelper(incidents, IncidentCommands.cmdSelectAll,string.Empty,string.Empty, string.Empty,portfolioID);
                IncidentState.IncidentCache = incidents;
            }
            else
                incidents = (IncidentCollection)IncidentState.IncidentCache;
            return incidents;
        }

        public static IncidentCollection LoadIncidentByStatus(string status, string sortExpression,int portfolioID)
        {
            IncidentCollection incidents = LoadIncidentByStatus(status,portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }

        //public static IncidentCollection LoadIncidentByAssignee(string status,

        private static void SortByColumn(string sortExpression, IncidentCollection incidents)
        {
            if (!string.IsNullOrEmpty(sortExpression))
            {
                switch (sortExpression)
                {
                    case "RequesterName":
                    case "RequesterName ASC":
                        incidents.Sort(new IncidentComparer("RequesterName"));
                        break;
                    case "RequesterName DESC":
                        incidents.Sort(new IncidentComparer("RequesterName"));
                        incidents.Reverse();
                        break;
                    case "Subject":
                    case "Subject ASC":
                        incidents.Sort(new IncidentComparer("Subject"));
                        break;
                    case "Subject DESC":
                        incidents.Sort(new IncidentComparer("Subject"));
                        incidents.Reverse();
                        break;
                    case "IncidentType":
                    case "IncidentType ASC":
                        incidents.Sort(new IncidentComparer("IncidentType"));
                        break;
                    case "IncidentType DESC":
                        incidents.Sort(new IncidentComparer("IncidentType"));
                        incidents.Reverse();
                        break;
                    case "PriorityLevel":
                    case "PriorityLevel ASC":
                        incidents.Sort(new IncidentComparer("PriorityLevel"));
                        break;
                    case "PriorityLevel DESC":
                        incidents.Sort(new IncidentComparer("PriorityLevel"));
                        incidents.Reverse();
                        break;
                    case "ID":
                    case "ID ASC":
                        incidents.Sort(new IncidentComparer("ID"));
                        break;
                    case "ID DESC":
                        incidents.Sort(new IncidentComparer("ID"));
                        incidents.Reverse();
                        break;
                    case "InHandTime":
                    case "InHandTime ASC":
                        incidents.Sort(new IncidentComparer("InHandTime"));
                        break;
                    case "InHandTime DESC":
                        incidents.Sort(new IncidentComparer("InHandTime"));
                        incidents.Reverse();
                        break;
                    case "IsOutOfHours":
                    case "IsOutOfHours ASC":
                        incidents.Sort(new IncidentComparer("IsOutOfHours"));
                        break;
                    case "IsOutOfHours DESC":
                        incidents.Sort(new IncidentComparer("IsOutOfHours"));
                        incidents.Reverse();
                        break;
                    case "Status":
                    case "Status ASC":
                        incidents.Sort(new IncidentComparer("Status"));
                        break;
                    case "Status DESC":
                        incidents.Sort(new IncidentComparer("Status"));
                        incidents.Reverse();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets all the Incidents from the database
        /// </summary>
        /// <returns>Returns the IncidentCollection object for all the records</returns>
        public static IncidentCollection LoadIncidentByStatus(string status,int portfolioID)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectByStatus,status,string.Empty,string.Empty,portfolioID);
            return incidents;
        }

        /// <summary>
        /// Gets all the Incidents from the database which are marked as visible to client.
        /// Currently displays all the items.  Needs to update the stored procedure.
        /// </summary>
        /// <returns>Returns the IncidentCollection object for all the records</returns>
        public static IncidentCollection LoadVisbileIncidentByStatus(string status, string Type,int portfolioID)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectVisibleIncidentsByStatus, status, Type,sessionKeys.UID.ToString(),portfolioID);
            return incidents;
        }

        public static IncidentCollection LoadVisbileIncidentByStatus(string status, string Type, string sortExpression,int portfolioID)
        {
            IncidentCollection incidents = LoadVisbileIncidentByStatus(status, Type,portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }

        /// <summary>
        /// Gets all the Incidents from the database.
        /// </summary>
        /// <returns>Returns the IncidentCollection object for all the records</returns>
        public static IncidentCollection LoadAllIncidentByStatusAndType(string status, string type,int portfolioID)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentsByStatusAndType, status, type, sessionKeys.UID.ToString(),portfolioID);
            return incidents;
        }
        public static IncidentCollection LoadAllIncidentByStatusAndType(string status, string type, int portfolioID, string sortExpression)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadAllIncidentByStatusAndType(status,type,portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }
        public static IncidentCollection LoadAllIncidentByStatusAndType(string status, string type, int portfolioID,int UID)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentsByStatusAndType, status, type, UID.ToString(), portfolioID);
            return incidents;
        }
        public static IncidentCollection LoadAllIncidentByStatusAndType(string status, string type, int portfolioID, int UID, string sortExpression)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentsByStatusAndType, status, type, UID.ToString(), portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }
        //Over loaded method for sorting
        public static IncidentCollection LoadAllIncidentByStatusAndType(string status, string type,string sortExpression,int portfolioID)
        {
            IncidentCollection incidents = LoadAllIncidentByStatusAndType(status, type,portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }
        public static IncidentCollection LoadAllIncidentsByAssignee(string status, string type, int portfolioID, int UID, string sortExpression)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentByAssignee, status, type, sessionKeys.UID.ToString(), portfolioID);
            return incidents;
        }

        public static IncidentCollection LoadAllIncidentsByAssignee(string status, string type,int portfolioID)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentByAssignee, status, type, sessionKeys.UID.ToString(),portfolioID);
            return incidents;
        }
        public static IncidentCollection LoadAllIncidentsByAssignee(int PortfolioID, string status, string type)
        {
            IncidentCollection incidents = new IncidentCollection();
            LoadDataHelper(incidents, IncidentCommands.cmdSelectIncidentByAssignee, status, type, sessionKeys.UID.ToString(), PortfolioID);
            return incidents;
        }

        //Over loaded method for sorting
        public static IncidentCollection LoadAllIncidentsByAssignee(int PortfolioID, string status, string type, string sortExpression)
        {
            IncidentCollection incidents = LoadAllIncidentsByAssignee(PortfolioID , status, type);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }
      
        public static IncidentCollection LoadAllIncidentsByAssignee(string status, string type,string sortExpression,int portfolioID)
        {
            IncidentCollection incidents = LoadAllIncidentsByAssignee(status, type,portfolioID);
            SortByColumn(sortExpression, incidents);
            return incidents;
        }

        /// <summary>
        /// Select the particular Incident based on the ID
        /// </summary>
        /// <param name="ID">Id of the record to be retrieved</param>
        /// <returns>Returns the selected record in the Incident object</returns>
        public static Incident SelectById(int incidentID)
        {
            Incident incident = LoadTheIncident(incidentID, IncidentCommands.cmdSelectIncidentByID);
            return incident;
        }

        //Loads the single incident
        private static Incident LoadTheIncident(int incidentID, string sqlString)
        {
            Incident incident = new Incident();
            using(SqlConnection conn=new SqlConnection(connectionString.retrieveConnString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", incidentID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incident = GetIncident(reader);
                        }
                    }
                }
                conn.Close();
            }
            return incident;
        }


        /// <summary>
        /// Adds the record to the Knowledge Base table
        /// </summary>
        /// <param name="incident"></param>
        /// <returns>True if added to KB is success. Else returns false.</returns>
        public static bool AddToKB(Incident incident)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(IncidentCommands.cmdAddToKB, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Subject", incident.Subject);
                    cmd.Parameters.AddWithValue("@Details", incident.Details);
                    cmd.Parameters.AddWithValue("@Resolution", incident.Resolution);
                    cmd.Parameters.AddWithValue("@DateLogged", incident.DateLogged);
                    cmd.Parameters.AddWithValue("@AuthorID", incident.ContractorID);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return ((recordsAffected > 0) ? true : false);
        }
        
        #region Needs to be implemented when needed

        /// <summary>
        /// Gets the Messages that are provided for the contractor
        /// </summary>
        /// <param name="ContractorID">The user id of the person being using the system</param>
        /// <returns>Returns the DataTable object for all the messages of the particular user</returns>
        public static void SelectMyMessages(int ContractorID)
        {

        }

        #endregion

        #region Building and Floor
        public static DataTable Incident_Floor_SelectAll()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Incident_Floor_SelectAll").Tables[0];
            return dt;
        }
        public static DataTable Incident_Building_SelectAll()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Incident_Building_SelectAll").Tables[0];
            return dt;
        }
        public static int Incident_Floor_Insert(string FloorName)
        {

            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@Name", FloorName)
                 };

            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Floor_Insert", sqlParams);
            return rowaffected;
        }
        public static int Incident_Building_Insert(string BuildingName)
        {

            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@Name", BuildingName)
                 };

            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Building_Insert", sqlParams);
            return rowaffected;
        }
        public static int Incident_Floor_Update(string FloorName, int ID)
        {

            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@Name", FloorName),
                                                             new SqlParameter("@ID",ID)   };

            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Floor_Update", sqlParams);
            return rowaffected;
        }
        public static int Incident_Building_Update(string BuildingName, int ID)
        {

            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@Name", BuildingName),
                                                             new SqlParameter("@ID",ID)   };

            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Building_Update", sqlParams);
            return rowaffected;
        }
        public static int Incident_Floor_Delete(int Id)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@ID", Id) };
            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Floor_Delete", sqlParams);
            return rowaffected;
        }
        public static int Incident_Building_Delete(int Id)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@ID", Id) };
            int rowaffected = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Building_Delete", sqlParams);
            return rowaffected;
        }


        #endregion
    }
}