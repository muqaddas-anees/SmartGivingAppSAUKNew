using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VT.Entity;

namespace VT.DAL
{
    public class LeaveApproverHelper
    {
        public LeaveApproverCollection SelectAll()
        {
            LeaveApproverCollection approvers = new LeaveApproverCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectAll, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveApprover approver = new LeaveApprover();
                            approver.ID = Convert.ToInt32(reader["ID"]);
                            approver.ApproverID = Convert.ToInt32(reader["ApproverID"]);
                            approver.ApproverName = reader["ApproverName"].ToString();
                            approver.UserID = Convert.ToInt32(reader["UserID"]);
                            approver.UserName = reader["UserName"].ToString();
                            approvers.Add(approver);
                        }
                    }
                }
            }
            return approvers;
        }

        public LeaveApproverCollection SelectApproverByUser(int userId)
        {
            LeaveApproverCollection approvers = new LeaveApproverCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectApproverByUser, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveApprover approver = new LeaveApprover();
                            approver.ID = Convert.ToInt32(reader["ID"]);
                            approver.ApproverID = Convert.ToInt32(reader["ApproverID"]);
                            approver.ApproverName = reader["ApproverName"].ToString();
                            approver.UserID = Convert.ToInt32(reader["UserID"]);
                            approver.UserName = reader["UserName"].ToString();
                            approvers.Add(approver);
                        }
                    }
                }
            }
            return approvers;
        }

        public DataTable SelectByApprover(int approverID)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectApproverByApprover, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApproverID", approverID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }
        public DataTable SelectArchivedByApprover(int approverID)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectArchivedByApprover, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApproverID", approverID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }
        public AllowanceCollection SelectAllowanceByUser(int userID)
        {
            AllowanceCollection allowanceDetails = new AllowanceCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.SelectByUser, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Allowance allowance = new Allowance();
                            allowance.ID = Convert.ToInt32(reader["ID"]);
                            allowance.UserID = Convert.ToInt32(reader["UserID"]);
                            allowance.UserName = reader["UserName"].ToString();
                            allowance.CarriedOver = Convert.ToSingle(reader["CarriedOver"]);
                            allowance.LeaveAllowance = Convert.ToSingle(reader["LeaveAllowance"]);
                            allowance.Available = Convert.ToSingle(reader["Available"]);
                            allowance.Year = Convert.ToInt32(reader["Year"]);
                            allowanceDetails.Add(allowance);
                        }
                    }
                }
            }
            return allowanceDetails;
        }
       
        public string SelectByUser(int userID)
        {
            string Approvername="";
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectApproverByUser, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();                  
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveApprover approver = new LeaveApprover();
                            approver.ID = Convert.ToInt32(reader["ID"]);
                            approver.ApproverID = Convert.ToInt32(reader["ApproverID"]);
                            approver.ApproverName = reader["ApproverName"].ToString();
                            approver.UserID = Convert.ToInt32(reader["UserID"]);
                            approver.UserName = reader["UserName"].ToString();
                             Approvername = reader["ApproverName"].ToString();
                          
                        }
                    }
                }
            }
            return Approvername;
        }


        //public LeaveApproverCollection SelectByUser(int userID)
        //{
        //    LeaveApproverCollection approvers = new LeaveApproverCollection();
        //    using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SelectApproverByUser, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@UserID", userID);
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    LeaveApprover approver = new LeaveApprover();
        //                    approver.ID = Convert.ToInt32(reader["ID"]);
        //                    approver.ApproverID = Convert.ToInt32(reader["ApproverID"]);
        //                    approver.ApproverName = reader["ApproverName"].ToString();
        //                    approver.UserID = Convert.ToInt32(reader["UserID"]);
        //                    approver.UserName = reader["UserName"].ToString();
        //                    approvers.Add(approver);
        //                }
        //            }
        //        }
        //    }
        //    return approvers;
        //}

        public int SetApprovalStatus(int ID, int StatusID, string ApproverNotes)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.SetStatus, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("StatusID", StatusID);
                    cmd.Parameters.AddWithValue("ApproverNotes", ApproverNotes);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(LeaveApprover leaveApprover)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", leaveApprover.UserID);
                    cmd.Parameters.AddWithValue("ApproverID", leaveApprover.ApproverID);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.Delete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Object Insert(int userID, int approverID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();                  
                        cmd.Parameters.AddWithValue("UserID", userID);
                        cmd.Parameters.AddWithValue("ApproverID", approverID);
                        return  cmd.ExecuteScalar();
                        //cmd.Parameters.Clear();
                   
                }
            }
           
            
        }

        //public string Insert(int userID, List<int> approverID)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.Insert, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            conn.Open();
        //            foreach (int approver in approverID)
        //            {
        //                cmd.Parameters.AddWithValue("UserID", userID);
        //                cmd.Parameters.AddWithValue("ApproverID", approver);
        //                cmd.ExecuteNonQuery();
        //                cmd.Parameters.Clear();
        //            }
        //        }
        //    }
        //    return string.Empty;
        //    //bool isUserAlreadyAssociated = false;
        //    //using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        //    //{
        //    //    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VT.LeaveApprovers WHERE UserID=" + userID, conn))
        //    //    {
        //    //        conn.Open();
        //    //        int countItems = Convert.ToInt32(cmd.ExecuteScalar());
        //    //        if (countItems > 0)
        //    //            isUserAlreadyAssociated = true;
        //    //    }
        //    //}
        //    //if (!isUserAlreadyAssociated)
        //    //{
        //    //    using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        //    //    {
        //    //        using (SqlCommand cmd = new SqlCommand(LeaveApproverProcedure.Insert, conn))
        //    //        {
        //    //            cmd.CommandType = CommandType.StoredProcedure;
        //    //            conn.Open();
        //    //            foreach (int approver in approverID)
        //    //            {
        //    //                cmd.Parameters.AddWithValue("UserID", userID);
        //    //                cmd.Parameters.AddWithValue("ApproverID", approver);
        //    //                cmd.ExecuteNonQuery();
        //    //                cmd.Parameters.Clear();
        //    //            }
        //    //        }
        //    //    }
        //    //    return string.Empty;
        //    //}
        //    //else
        //    //    return "AlreadyExists";
        //}
    }
}