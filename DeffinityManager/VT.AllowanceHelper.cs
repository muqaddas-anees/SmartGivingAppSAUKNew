using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VT.Entity;

/// <summary>
/// Summary description for VT
/// </summary>
/// 
namespace VT.DAL
{
    public class AllowanceHelper
    {
        public AllowanceHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public AllowanceCollection SelectAll()
        {
            AllowanceCollection allowanceCollection = new AllowanceCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.SelectAll, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                            allowanceCollection.Add(allowance);
                        }
                    }
                }
            }
            return allowanceCollection;
        }

        public AllowanceCollection SelectByUser(int userid)
        {
            AllowanceCollection allowanceCollection = new AllowanceCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.SelectByUser, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", userid);
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
                            allowance.Year = Convert.ToInt32(reader["Year"]);
                            allowanceCollection.Add(allowance);
                        }
                    }
                }
            }
            return allowanceCollection;
        }

        public LeaveDaysCollection LeaveDaysSelectByUser(int userid)
        {
            LeaveDaysCollection leaveDaysCollection = new LeaveDaysCollection();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveDays.SelectByUser, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", userid);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveDaysEntity leave = new LeaveDaysEntity();
                            leave.ID = Convert.ToInt32(reader["ID"]);
                            leave.UserID = Convert.ToInt32(reader["UserID"]);
                            leave.UserName = reader["UserName"].ToString();
                            leave.Days = Convert.ToDouble(reader["Days"]);
                            leave.Comments = reader["Comments"].ToString();
                            leave.Year =Convert.ToInt32(reader["Year"]);
                            leaveDaysCollection.Add(leave);
                        }
                    }
                    conn.Close();
                }
            }
            return leaveDaysCollection;
        }
        public int Insert(Allowance allowance)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", allowance.UserID);
                    cmd.Parameters.AddWithValue("LeaveAllowance", allowance.LeaveAllowance);
                    cmd.Parameters.AddWithValue("CarriedOver", allowance.CarriedOver);
                    cmd.Parameters.AddWithValue("Year", allowance.Year);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertLeaveDays(LeaveDaysEntity Ld)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveDays.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", Ld.UserID);
                    cmd.Parameters.AddWithValue("@UserName", Ld.UserName);
                    cmd.Parameters.AddWithValue("@Days", Ld.Days);
                    cmd.Parameters.AddWithValue("@Comments", Ld.Comments);
                    cmd.Parameters.AddWithValue("@Year", Ld.Year);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void DeleteLeaveDays(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveDays.Delete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.Delete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", id);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(Allowance allowance)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(AllowanceProcedure.Update, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", allowance.ID);
                    cmd.Parameters.AddWithValue("UserID", allowance.UserID);
                    cmd.Parameters.AddWithValue("LeaveAllowance", allowance.LeaveAllowance);
                    cmd.Parameters.AddWithValue("CarriedOver", allowance.CarriedOver);
                    cmd.Parameters.AddWithValue("Year", allowance.Year);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}