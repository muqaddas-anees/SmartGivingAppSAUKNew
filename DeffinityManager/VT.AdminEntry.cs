using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for VT
/// </summary>
namespace VT
{
    public class AdminEntry
    {
        //#region Annual Year
        //public static object InsertUpdateAnnualYear(int ID, DateTime StartDate, DateTime EndDate)
        //{
        //    return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[InsertUpdateAnnualYear]", new SqlParameter("@AnnualYearStart", StartDate), new SqlParameter("@AnnualYearEnd", EndDate), new SqlParameter("@ID", ID));
        //}
        //public static DataTable SelectannualYears()
        //{
        //    return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "[VT].[SelectAnnualYear]").Tables[0];
        //}
        //public static DataTable SelectannualYears_All()
        //{
        //    return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "[VT].[SelectAnnualdate_All]").Tables[0];
        //}
        //#endregion


        #region AbsenseType
        public static  DataTable SelectAbsenseTypes()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "[VT].[GetAbsenseType]").Tables[0];
        }

        public static object InsertUpdateAbsenseType(string Type,string Color,int id)
        {
            return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[InsertUpdateAbsenseType]", new SqlParameter("@AbsenseType", Type), new SqlParameter("@Color", Color), new SqlParameter("@id", id));
            //return 1 -- inserted
            //return 2 -- updated
        }
        public static void DeleteAbsenseType(int Id)
        {
            SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[DeleteAbsenseType]", new SqlParameter("@ID", Id));
        }
        
        #endregion

        #region Workhours section
        public static object InsertUpdateWorkHours(int UserID, float FullDayHours, float HalfDayHours, int DaysinLieu)
        {
            object obj=null;
            try
            {
                return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[InsertUpdateWorkHours]",
                    new SqlParameter[] { new SqlParameter("@UserID", UserID), new SqlParameter("@FullDayHours", FullDayHours), new SqlParameter("@HalfDayHours", HalfDayHours), new SqlParameter("@MaxDaysInLieu", DaysinLieu) });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return obj;
        }
        public static DataTable SelectWorkHoursByUser(int UserID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "[VT].[GetWorkHours_ByUser]", new SqlParameter("@UserID", UserID)).Tables[0];
        }
        public static DataTable SelectWorkHours()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "[VT].[GetWorkHours_ByUser]", new SqlParameter("@UserID", 0)).Tables[0];
        }
        public static void DeleteWorkHours(int Id)
        {
            SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[DeleteworkHours]", new SqlParameter("@ID", Id));
        }

        #endregion

        #region USERTIMES --Underuse now
        public static object InsertUpdateUSERTIMES(int UserID, int STARTTIME,int ENDTIME,int HDPTIME, int DaysinLieu)
        {                       
            return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[InsertUpdateUSERTIMES]",
                new SqlParameter[] { new SqlParameter("@UserID", UserID), new SqlParameter("@STARTMIN", STARTTIME), new SqlParameter("@ENDMIN", ENDTIME), new SqlParameter("@HDPMIN", HDPTIME), new SqlParameter("@MAXDAYSINLIEU", DaysinLieu) });
           
        }
        public static SqlDataReader SelectUserTimes()
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "[VT].[SELECTUSERTIMES]");
        }
        public static void DeleteUSERTIMES(int Id)
        {
            SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "[VT].[DELATEUSERTIMES]", new SqlParameter("@ID", Id));
        }

        #endregion

    }
}
