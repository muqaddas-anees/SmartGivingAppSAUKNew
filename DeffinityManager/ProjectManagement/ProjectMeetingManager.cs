using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectMeetingEntitys;

/// <summary>
/// Summary description for ProjectMeetingManager
/// </summary>
namespace Deffinity.ProjectMeetingManager
{
    public class ProjectMeetingManager
    {

        /// <summary>
        /// Use to insert and update the Project meetings 
        /// </summary>
        public static int ProjectMeetingSave(ProjectMeetingEntity ProjectMeeting)
        {
            int restult;
            restult = Insert_Update(ProjectMeeting, true, "Deffinity_ProjectMeetingsInsert");
            //after inserting a new meeting 
            //should insert project items in meetings table
            InsertTaskItems("Deffinity_ProjectMeetingTaskItemsBulkInsert", ProjectMeeting.ProjectReference,ProjectMeeting.ID);
            //return ((restult > 0) ? true : false);
            return restult;
        }

        private static int Insert_Update(ProjectMeetingEntity ProjectMeeting, bool SqlType, string spName)
        {
            SqlParameter meetingID = new SqlParameter("@IDRet", SqlDbType.Int);

            meetingID.Direction = ParameterDirection.Output;
            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ID", ProjectMeeting.ID),
                                                   new SqlParameter("@ProjectReference", ProjectMeeting.ProjectReference),
                                                   new SqlParameter("@MeetingDate", DateTime.Parse(ProjectMeeting.MeetingDate)),
                                                   new SqlParameter("@MeetingTime", ProjectMeeting.MeetingTime),
                                                   new SqlParameter("@Location", ProjectMeeting.Location),
                                                   new SqlParameter("@Attendees", ProjectMeeting.Attendees),
                                                   new SqlParameter("@GeneralNotes", ProjectMeeting.GeneralNotes),
                                                   new SqlParameter("@LessonsLearnt", ProjectMeeting.LessonsLearnt),
                                                   new SqlParameter("@KeyAchievements", ProjectMeeting.KeyAchievements),
                                                   new SqlParameter("@KeyTasks", ProjectMeeting.KeyTasks),
                                                       new SqlParameter("@VisibletoCustomer",ProjectMeeting.VisibletoCustomer),
                                                       new SqlParameter("@RAGStatus",ProjectMeeting.RAGStatus),meetingID

                                                    };

           // return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);
            return int.Parse(meetingID.Value.ToString());
        }
        private static void InsertTaskItems(string spName, int ProjectReference, int MeetingID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@MeetingID", MeetingID));
        }
        public static ProjectMeetingEntity ProjectMeetingSelect(int ID)
        {
            ProjectMeetingEntity ProjectMeeting = new ProjectMeetingEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_ProjectMeetingSelect", new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                        ProjectMeeting.ID = int.Parse(dr["ID"].ToString());
                        ProjectMeeting.Attendees = dr["Attendees"].ToString();
                        ProjectMeeting.GeneralNotes = dr["GeneralNotes"].ToString();
                        ProjectMeeting.LessonsLearnt = dr["LessonsLearnt"].ToString();
                        ProjectMeeting.Location = dr["Location"].ToString();
                        ProjectMeeting.MeetingDate = DateTime.Parse(dr["MeetingDate"].ToString()).ToShortDateString();
                        ProjectMeeting.MeetingTime = dr["MeetingTime"].ToString();
                        ProjectMeeting.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                        ProjectMeeting.KeyAchievements = dr["KeyAchievements"].ToString();
                        ProjectMeeting.KeyTasks = dr["KeyTasks"].ToString();
                        ProjectMeeting.VisibletoCustomer = bool.Parse(string.IsNullOrEmpty(dr["VisibletoCustomer"].ToString()) ? "false" : dr["VisibletoCustomer"].ToString());
                        ProjectMeeting.RAGStatus =int.Parse(dr["RAGStatus"].ToString());
                    }
                    dr.Close();
                }
            }
            return ProjectMeeting;
        }
        public static DataTable ProjectMeetingSelectAll(int ProjectReference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectMeetingSelect", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
        }

        public static DataTable ProjectMeetingSelectByCustomer(int Userid, int ProjectReference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectMeetingSelect", new SqlParameter("@UserId", Userid), new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
        }
        
    }
}