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
    public class ApprovalHelper
    {
        public ApprovalHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Approval Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static int Insert(Approval approval)
        {
            int recordsAffected = InsertUpdateHelper(approval, false, ApprovalCommands.cmdInsert);
            ApprovalState.ClearApprovalCache();
            return recordsAffected;
        }


        /// <summary>
        /// Updates the record of the Approval Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Approval approval)
        {
            int recordsAffected = InsertUpdateHelper(approval, true, ApprovalCommands.cmdUpdate);
            ApprovalState.ClearApprovalCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Approval approval, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", approval.Id);
                    cmd.Parameters.AddWithValue("@changeControlID",approval.ChangeControlID);
                    cmd.Parameters.AddWithValue("@title",approval.Title );
                    cmd.Parameters.AddWithValue("@approvalID",approval.ApprovalID);
                    cmd.Parameters.AddWithValue("@comments",approval.Comments);
                    cmd.Parameters.AddWithValue("@approved",approval.Approved);
                    cmd.Parameters.AddWithValue("@dateApproved",approval.DateApproved);
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
        public static bool Delete(Approval approval)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(ApprovalCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", approval.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            ApprovalState.ClearApprovalCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the ApprovalCollection object for all the records</returns>
        public static ApprovalCollection LoadAllApprovals()
        {
            ApprovalCollection approvals = new ApprovalCollection();
            if (ApprovalState.ApprovalCache == null)
            {
                LoadDataHelper(approvals, ApprovalCommands.cmdSelectAll,0);
                ApprovalState.ApprovalCache = approvals;
            }
            else
                approvals = (ApprovalCollection)ApprovalState.ApprovalCache;
            return approvals;
        }
        public static ApprovalCollection LoadApprovalsById(int id)
        {
            ApprovalCollection approvals = new ApprovalCollection();
            if (ApprovalState.ApprovalCache == null)
            {
                LoadDataHelper(approvals, ApprovalCommands.cmdSelectById,id);
                ApprovalState.ApprovalCache = approvals;
            }
            else
                approvals = (ApprovalCollection)ApprovalState.ApprovalCache;
            return approvals;
        }
        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(ApprovalCollection changes, string sqlStatement,int id)
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
                            Approval approval = GetChange(reader);
                            changes.Add(approval);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Approval GetChange(SqlDataReader reader)
        {
            Approval approval = new Approval();
            if (reader.IsClosed)
                reader.Read();
            approval.Id = Convert.ToInt32(reader["Id"]);
            approval.ChangeControlID = Convert.ToInt32(reader["ChangeControlID"]);
            approval.ApprovalID = Convert.ToInt32(reader["ApprovalID"]);
            approval.Comments = reader["Comments"].ToString();
            if (reader["approved"] == DBNull.Value)
                approval.Approved = null;
            else
                approval.Approved = Convert.ToBoolean(reader["Approved"]);
            approval.DateApproved = Convert.ToDateTime(reader["DateApproved"]);
            approval.Title = reader["Title"].ToString();
            approval.ApprovalName = reader["ApprovalName"].ToString();
            approval.EmailAddress = reader["EmailAddress"].ToString();
            return approval;
        }
    }
}