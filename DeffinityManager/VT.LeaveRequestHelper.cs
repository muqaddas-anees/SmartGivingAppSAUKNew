using System;
using System.Collections.Generic;
using System.Web;
using VT.Entity;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for VT
/// </summary>
/// 
namespace VT.DAL
{
    public class LeaveRequestHelper
    {
        public LeaveRequestHelper()
        {

        }

        public LeaveRequestCollection SelectAll()
        {
            LeaveRequestCollection requests = new LeaveRequestCollection();
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.SelectAll, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveRequest request = new LeaveRequest();
                            request.ID = Convert.ToInt32(reader["ID"]);
                            request.AbsenseType = Convert.ToInt32(reader["AbsenseType"]);
                            request.ApprovalStatus = reader["Status"].ToString();
                            request.Days = reader["Days"].ToString();
                            request.FromDate = Convert.ToDateTime(reader["FromDate"]);
                            request.ToDate = Convert.ToDateTime(reader["ToDate"]);
                            requests.Add(request);
                        }
                    }
                }
            }
            return requests;
        }

        public LeaveRequestCollection SelectByUser(int UserID)
        {
            LeaveRequestCollection requests = new LeaveRequestCollection();
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.SelectPending, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveRequest request = new LeaveRequest();
                            request.ID = Convert.ToInt32(reader["ID"]);
                            request.AbsenseType = Convert.ToInt32(reader["AbsenseType"]);
                            request.AbsenseTypeName = reader["AbsenseTypeName"].ToString();
                            request.ApprovalStatus = reader["StatusName"].ToString();
                            request.Days = reader["Days"].ToString();
                            request.FromDate = Convert.ToDateTime(reader["FromDate"]);
                            request.ToDate = Convert.ToDateTime(reader["ToDate"]);
                            request.RequestNotes = reader["RequestNotes"].ToString();
                            requests.Add(request);
                        }
                    }
                }
            }
            return requests;
        }


        public int Delete(int id,int sid =0)
        {
            //int? status = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.Delete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", id);
                    cmd.Parameters.AddWithValue("SID", sid);

                    SqlParameter paramReturnValue = new SqlParameter();
                    paramReturnValue.ParameterName = "@Deleted";
                    paramReturnValue.SqlDbType = SqlDbType.Int;
                   
                    paramReturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramReturnValue);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int returnValue = (int)cmd.Parameters["@Deleted"].Value;
                    return returnValue;
                }
            }
        }
        //public int Delete(int id)
        //{
        //    //int? status = 0;
        //   return Delete(id, 0);
        //}
        public Object Update(LeaveRequest leaveRequest)
        {
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.Update, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", leaveRequest.ID);
                    cmd.Parameters.AddWithValue("AbsenseType",leaveRequest.AbsenseType);
                    cmd.Parameters.AddWithValue("FromDate", leaveRequest.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", leaveRequest.ToDate);
                    cmd.Parameters.AddWithValue("RequesterNotes", leaveRequest.RequestNotes);
                    cmd.Parameters.AddWithValue("FromDatePeriod", leaveRequest.FromDatePeriod);
                    cmd.Parameters.AddWithValue("ToDatePeriod", leaveRequest.ToDatePeriod); 
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        //public Object Insert(LeaveRequest leaveRequest)
        //{
        //    using (SqlConnection conn = new SqlConnection(Constants.DBString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.Insert, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("AbsenseType", leaveRequest.AbsenseType);
        //            cmd.Parameters.AddWithValue("FromDate", leaveRequest.FromDate);
        //            cmd.Parameters.AddWithValue("ToDate", leaveRequest.ToDate);
        //            cmd.Parameters.AddWithValue("ApprovalStatus", leaveRequest.ApprovalStatus);
        //            cmd.Parameters.AddWithValue("UserID", leaveRequest.RequesterID);
        //            cmd.Parameters.AddWithValue("RequesterNotes", leaveRequest.RequestNotes);
        //            cmd.Parameters.AddWithValue("MemberId", leaveRequest.MemberID);
        //            cmd.Parameters.AddWithValue("TeamType", leaveRequest.TeamType);
        //            cmd.Parameters.AddWithValue("Site", leaveRequest.SiteID);
        //            conn.Open();
        //            return cmd.ExecuteScalar();
        //        }
        //    }
        //}
        public Object Insert(LeaveRequest leaveRequest)
        {
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AbsenseType", leaveRequest.AbsenseType);
                    cmd.Parameters.AddWithValue("FromDate", leaveRequest.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", leaveRequest.ToDate);
                    cmd.Parameters.AddWithValue("ApprovalStatus", leaveRequest.ApprovalStatus);
                    cmd.Parameters.AddWithValue("UserID", leaveRequest.RequesterID);
                    cmd.Parameters.AddWithValue("Notes", leaveRequest.RequestNotes);
                    cmd.Parameters.AddWithValue("FromDatePeriod", leaveRequest.FromDatePeriod);
                    cmd.Parameters.AddWithValue("ToDatePeriod", leaveRequest.ToDatePeriod);
                    cmd.Parameters.AddWithValue("FromDateMeridian", leaveRequest.FromDateMeridian);
                    cmd.Parameters.AddWithValue("ToDateMeridian", leaveRequest.ToDateMeridian);
                    cmd.Parameters.AddWithValue("ModifiedBy", sessionKeys.UID);
                    conn.Open();                   
                    return cmd.ExecuteScalar();
                }
            }
        }
        public Object Insert_PreRequest(LeaveRequest leaveRequest)
        {
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(LeaveRequestProcedure.Pre_Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AbsenseType", leaveRequest.AbsenseType);
                    cmd.Parameters.AddWithValue("FromDate", leaveRequest.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", leaveRequest.ToDate);
                    cmd.Parameters.AddWithValue("ApprovalStatus", leaveRequest.ApprovalStatus);
                    cmd.Parameters.AddWithValue("UserID", leaveRequest.RequesterID);
                    cmd.Parameters.AddWithValue("Notes", leaveRequest.RequestNotes);
                    cmd.Parameters.AddWithValue("FromDatePeriod", leaveRequest.FromDatePeriod);
                    cmd.Parameters.AddWithValue("ToDatePeriod", leaveRequest.ToDatePeriod);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        public DataTable GetLeaveRequest(int ID)
        {
            DataTable Dt_ReqDetails;
            Dt_ReqDetails = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.LeaveRequestByID", new SqlParameter("ID",ID)).Tables[0];
            return Dt_ReqDetails;
        }
        public static DataTable DisplayResourceSummary(int ResourceID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,"VT.RequestSummaryByResource_Mod",new SqlParameter("@ResourceID",ResourceID)).Tables[0];
        }
        public static string ReturnToMeridian(string Meridian)
        {
            if (Meridian == "1")
                return ".12.00";
            else if (Meridian == "2")
                return ".24.00";
            else
               return string.Empty;
            
        }
        public static string ReturnFromMeridian(string Meridian)
        {
            if (Meridian == "1")
                return ".00.00";
            else if (Meridian == "2")
                return ".12.00";
            else
                return string.Empty;

        }
        public static string ReturnColor(string AbsenseType)
        {
            if (AbsenseType.ToLower().Contains("sick"))
                return "Green";
            else if (AbsenseType.ToLower().Contains("annual"))
                return "Yellow";
            else if (AbsenseType.ToLower().Contains("holiday"))
                return "Yellow";
            else
                return "Red";
        }

    }
}