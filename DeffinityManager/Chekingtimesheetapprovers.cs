using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for Chekingtimesheetapprovers
/// </summary>
namespace CheckTimesheetApprovers
{
    public class Chekingtimesheetapprovers
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        public Chekingtimesheetapprovers()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int checkApprovertimesheet(int TimesheetID, int PrimartApprover, int OptionalApprover, int Status)
        {
            int check = 0;
            int check1 = 0;
            int GetResults = 0;
            if (PrimartApprover != 0)
            {

                check = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(primarytimesheetstatusid,0) as Flag  from  timesheetapproverdata where timesheetid=@WCDateID", new SqlParameter("@WCDateID", TimesheetID)));
                check1 = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(optionaltimesheetstatusid,0) as Flag  from  timesheetapproverdata where timesheetid=@WCDateID", new SqlParameter("@WCDateID", TimesheetID)));


            }
            else if(OptionalApprover!=0)
            {
                check1 = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(primarytimesheetstatusid,0) as Flag  from  timesheetapproverdata where timesheetid=@WCDateID", new SqlParameter("@WCDateID", TimesheetID)));
                check = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(optionaltimesheetstatusid,0) as Flag  from  timesheetapproverdata where timesheetid=@WCDateID", new SqlParameter("@WCDateID", TimesheetID)));
            }


            if ((PrimartApprover != 0) && (check == 0) && (check1==0) && (OptionalApprover == 0))
            {
                insertorupdateEachTimesheetentry(TimesheetID, 1, PrimartApprover, OptionalApprover, 1, 0,0);
                //here PrimaryTimesheetapprover approved but optinalTimeheet Approver pending for this resource...
                updateflagStatusinTimesheet(TimesheetID, 1);
                GetResults = 1;
               



            }
            else if ((PrimartApprover != 0) && (check == 1) && (check1 == 0) && (OptionalApprover == 0))
            {
                //primary timesheet approved already Submitted the Timesheet.
                GetResults = 6;

            }
            else if ((PrimartApprover != 0) && (check == 1) && (check1 == 2) && (OptionalApprover == 0))
            {
                //primary timesheet approved already Submitted the Timesheet.
                GetResults = 6;

            }

            else if ((PrimartApprover != 0) && (check == 0) && (check1 == 2)&& (OptionalApprover == 0) )
            {
                insertorupdateEachTimesheetentry(TimesheetID, 3, PrimartApprover, OptionalApprover,1, 0,1);

                updateflagStatusinTimesheet(TimesheetID, 3);
                //Here Timesheet is aproved 
                //you have already Submitted the Timesheet.
                GetResults = 3;

            }

            else if((OptionalApprover!=0) &&(check==0) &&(check1==0) &&(PrimartApprover==0))
            {
                insertorupdateEachTimesheetentry(TimesheetID, 2, 0, OptionalApprover, 2, 2,0);
                updateflagStatusinTimesheet(TimesheetID, 2);
                //Here Optional Timesheet Approver  but primaryTimesheet approver need to approve.....
                GetResults = 2;


            }
            else if ((OptionalApprover != 0) && (check == 0) && (check1 == 1) && (PrimartApprover == 0))
            {
                insertorupdateEachTimesheetentry(TimesheetID, 3, 0, OptionalApprover, 1, 2,1);
                updateflagStatusinTimesheet(TimesheetID, 3);          

                //Full Timesheet entry approved....
                GetResults = 3;


            }
       
            else if ((OptionalApprover != 0) && (check == 2)&& (check1 == 1) && (PrimartApprover == 0))
            {
                //Optional Timesheet approver already submitted.....

              GetResults = 7;
            }
            else if ((OptionalApprover != 0) && (check == 2) && (check1 == 0) && (PrimartApprover == 0))
            {
                //Optional Timesheet approver already submitted.....
              GetResults = 7;
            }
                       
          
            return GetResults;
        }


        private  int insertorupdateEachTimesheetentry(int TimesheetentryID, int status, int Primart_Approver, int OptionalApprover,int primarytimesheet_status, int optionaltimesheet_Status,int checkExsisting)
        {
            int i = 0;

            cmd = db.GetStoredProcCommand("DN_UpdateApproverSatus");
            db.AddInParameter(cmd, "@timesheetid", DbType.Int32, TimesheetentryID);
            db.AddInParameter(cmd, "@primaryapprover", DbType.Int32, Primart_Approver);
            db.AddInParameter(cmd, "@primarytimesheetstatusid", DbType.Int32, primarytimesheet_status);
            db.AddInParameter(cmd, "@optionaltimesheetstatusid", DbType.Int32, optionaltimesheet_Status);
            db.AddInParameter(cmd, "@Checkexisting", DbType.Int32, checkExsisting);
            db.AddInParameter(cmd, "@stausID", DbType.Int32, status);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@output");
                   


            return i;
        }


        public int TimesheetsingleApprover(int timesheetID, int Primart_Approver, int OptionalApprover, int status)
        {
            int check = 0;
            int getVal=0;

            check = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select COUNT(*) as Flag  from  timesheetapproverdata where timesheetid=@WCDateID", new SqlParameter("@WCDateID", timesheetID)));


            if (check == 1)
            {
                getVal = 0;
            }
            else
            {

                if ((Primart_Approver == 0) && (OptionalApprover == 0))
                {
                    getVal = 1;
                }
                else
                {

                    cmd = db.GetStoredProcCommand("DN_SingletimsheetApprover");
                    db.AddInParameter(cmd, "@timesheetid", DbType.Int32, timesheetID);
                    db.AddInParameter(cmd, "@Checkexisting", DbType.Int32, check);
                    db.AddInParameter(cmd, "@stausID", DbType.Int32, status);
                    db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
                    db.ExecuteNonQuery(cmd);
                    getVal = (int)db.GetParameterValue(cmd, "@output");
                    getVal = 3;
                }
            }
            return getVal;
        }



        public int CheckApproveAll(int TimesheetWeekCommanceDateID, int contractorID, int PrimaryApproverID)
        {
            int TimesheetEntryID = 0;
            int resultingchecing = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select ID from TimesheetEntry where WCDateID={0}", TimesheetWeekCommanceDateID));
            while (dr.Read())
            {
                TimesheetEntryID = Convert.ToInt32(dr["ID"]);

                if(PrimaryApproverID==2)
                {
                    //EachTimesheetDataenter(TimesheetEntryID, PrimaryApproverID, OptionalapproverID, contractorID);
                   resultingchecing= checkApprovertimesheet(TimesheetEntryID, PrimaryApproverID, 0, 1);
                }
                else
                {
                    resultingchecing = checkApprovertimesheet(TimesheetEntryID, 0, 3, 1);
                }

            }
            dr.Close();



            return resultingchecing;
        }


        public int CheckAllsingleApprover(int TimesheetWeekCommanceDateID, int contractorID, int PrimaryApproverID)
        {
            int TimesheetEntryID = 0;
            int resultingchecing = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select ID from TimesheetEntry where WCDateID={0}", TimesheetWeekCommanceDateID));
            while (dr.Read())
            {
                TimesheetEntryID = Convert.ToInt32(dr["ID"]);

                     resultingchecing = TimesheetsingleApprover(TimesheetEntryID, PrimaryApproverID, 0, 1);
                     updateflagStatusinTimesheet(TimesheetEntryID, 3);

            }
            dr.Close();



            return resultingchecing;
        }


        private void updateflagStatusinTimesheet(int TimesheetentryID, int StatusFlag)
        {
            cmd = db.GetStoredProcCommand("DN_UpdateTimesheetFlagStaus");
            db.AddInParameter(cmd, "@TimesheetID", DbType.Int32, TimesheetentryID);
            db.AddInParameter(cmd, "@FlagStatusID", DbType.Int32, StatusFlag);
            db.ExecuteNonQuery(cmd);
        }



        public int GetProjectOwner(int ProjectReference)
        {
            int ID = 0;
            ID=Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(OwnerID,0) as Projectowner  from  projects where ProjectReference=@ProjectReference", new SqlParameter("@ProjectReference", ProjectReference)));
            return ID;
        }


        public int GetProjectOwnerIDBasedOnWC(int WCDateID, int Contractor)
        {
            int resultingchecing = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select distinct OwnerID from Projects  where ProjectReference in (select distinct ProjectReference from  TimesheetEntry where WCDateID={0} and ContractorID={1})", WCDateID, Contractor));
            while (dr.Read())
            {
                resultingchecing = Convert.ToInt32(dr["OwnerID"]);

             }
            dr.Close();
            return resultingchecing;
        }

        public int updatedetailsTimesheet(int TimesheetID, int ProjectID, int TaskID, double Hours1,int contractorID,int EntryType)
        {
            int getVal = 0;
            cmd = db.GetStoredProcCommand("DN_UpdateTiemsheetentryadmin");
            db.AddInParameter(cmd, "@ID", DbType.Int32, TimesheetID);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectID);
            db.AddInParameter(cmd, "@Hours", DbType.Double, Hours1);
            db.AddInParameter(cmd, "@ResourceID", DbType.Int32, contractorID);
            db.AddInParameter(cmd, "@ProjectTaskID", DbType.Int32, TaskID);
            db.AddInParameter(cmd, "@entryid", DbType.Int32, EntryType);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            getVal = (int)db.GetParameterValue(cmd, "@output");
            return 0;
        }


    }
}
