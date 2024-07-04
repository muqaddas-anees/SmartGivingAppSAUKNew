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

namespace TimeSheet_Admin
{
    public class TimeSheetAdminApprove
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        DbCommand cmd1;
        DbCommand cmd2;
        DbCommand cmd3;
        DbCommand cmd5;
        DbCommand cmd6;
        DbCommand cmd7;
        DbCommand cmd8;
        DbCommand cmd10;
        public TimeSheetAdminApprove()
        {
           
        }

        public void Timesheet_Decline(string timesheetids,int decline_by,string comments)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Timesheetentry_decline", new SqlParameter("@timesheetids", timesheetids), new SqlParameter("@decline_by", decline_by), new SqlParameter("@ApproverComments", comments));
        }

        public DataSet displayTimesheet_SendforApproval(int TimesheetApproverID)
        {
            
                DataSet DisplayApprove = new DataSet();
                cmd = db.GetStoredProcCommand("DN_TimeSheetAdmin");
                db.AddInParameter(cmd, "@TimeSheetApproverID", DbType.Int32, TimesheetApproverID);
                //@TimeSheetApproverID
                DisplayApprove = db.ExecuteDataSet(cmd);

                return DisplayApprove;
            
        }
        public DataSet displayTimesheet_Decline(int TimesheetApproverID)
        {

            DataSet DisplayApprove = new DataSet();
            cmd = db.GetStoredProcCommand("DN_TimeSheetAdmindecline");
            db.AddInParameter(cmd, "@TimeSheetApproverID", DbType.Int32, TimesheetApproverID);
            //@TimeSheetApproverID
            DisplayApprove = db.ExecuteDataSet(cmd);

            return DisplayApprove;

        }
        public DataSet displayTimesheet_PendingApproval(string WCDATEID,int ApproverID)
        {
            try
            {
                if ((WCDATEID == "Please select...") || (WCDATEID == "") || WCDATEID=="0")
                {
                    WCDATEID = "";
                    DataSet PnedingToApprove = new DataSet();
                    cmd1 = db.GetStoredProcCommand("DN_TimeSheetAdmin_Datewise");
                    db.AddInParameter(cmd1, "@WCDateID", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(WCDATEID) ? "01/01/1900" : WCDATEID));
                    db.AddInParameter(cmd1, "@ApproverID", DbType.Int32, ApproverID);

                    
                    PnedingToApprove = db.ExecuteDataSet(cmd1);
                    return PnedingToApprove;
                }

                else
                {
                    DataSet PnedingToApprove = new DataSet();
                    cmd1 = db.GetStoredProcCommand("DN_TimeSheetAdmin_Datewise");
                    db.AddInParameter(cmd1, "@WCDateID", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(WCDATEID) ? "01/01/1900" : WCDATEID));
                    db.AddInParameter(cmd1, "@ApproverID", DbType.Int32, ApproverID);
                    PnedingToApprove = db.ExecuteDataSet(cmd1);
                    return PnedingToApprove;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

       
        public DataSet GetContractors(int ApproverID)
        {
            DataSet AllCustomers = new DataSet();
            cmd2 = db.GetStoredProcCommand("DN_TimeSheetTotalcontarctors");
            db.AddInParameter(cmd2, "@ApproverID", DbType.Int32, ApproverID);
            AllCustomers = db.ExecuteDataSet(cmd2);
            return AllCustomers;
        }
        public DataSet GetWeekCommanceDate(int ApproverID)
        {
            DataSet AllCustomers = new DataSet();
            cmd5 = db.GetStoredProcCommand("DN_DisplayWCDate");
            db.AddInParameter(cmd5, "@ApproverID", DbType.Int16, ApproverID);
            AllCustomers = db.ExecuteDataSet(cmd5);
            return AllCustomers;
        }



        public DataSet TimeSheetJournals(int GetRasourceID, string StartDate, string EndDate, int TimesheetApproveID)
        {
            try
            {
                DataSet GetTimeSheetJournal = new DataSet();

                cmd3 = db.GetStoredProcCommand("DN_TimeSheetAdmin_Journal");
                db.AddInParameter(cmd3, "@RourceID", DbType.Int32, GetRasourceID);
                db.AddInParameter(cmd3, "@StartDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(StartDate) ? "01/01/1900" : StartDate));
                db.AddInParameter(cmd3, "@EndDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(EndDate) ? "01/01/1900" : EndDate));
                db.AddInParameter(cmd3, "@TimesheetApproveID", DbType.Int32, TimesheetApproveID);
                GetTimeSheetJournal = db.ExecuteDataSet(cmd3);

                return GetTimeSheetJournal;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }

        public DataSet ViewTimeSheet_SubmittApproval(int WCDateID, int Key, int contractorID, int TimeSheetApproverID)
        {
            //Key 0 is The Timesheet Send ForApproval from Submitt send for Sebmitted one That status Is 2
            //Key 1 is The Timesheet Send ForApproval from Awaiting for Approval that status 1
            //Key 2 is The Timesheet Journal....
            try
            {
                DataSet ViewTimeSheetSubmittApproval = new DataSet();
                cmd3 = db.GetStoredProcCommand("DN_ViewSendsumbmitTApprove");
                db.AddInParameter(cmd3, "@WCDateID", DbType.Int32, WCDateID);
                db.AddInParameter(cmd3, "@contractorID", DbType.Int32, contractorID);
                db.AddInParameter(cmd3, "@TimeSheetApproverID", DbType.Int32, TimeSheetApproverID);
                db.AddInParameter(cmd3, "@key", DbType.Int32, Key);
                ViewTimeSheetSubmittApproval = db.ExecuteDataSet(cmd3);
                return ViewTimeSheetSubmittApproval;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        public DataSet dsTimeSheet_Signoff(int WCDateID, int contractorID)
        {
            try
            {
                DataSet dsTimeSheetSubmitt = new DataSet();
                cmd3 = db.GetStoredProcCommand("DN_TimesheetUserlistByWcDate");
                db.AddInParameter(cmd3, "@wcdateid", DbType.Int32, WCDateID);
                db.AddInParameter(cmd3, "@userid", DbType.Int32, contractorID);
                dsTimeSheetSubmitt = db.ExecuteDataSet(cmd3);
                return dsTimeSheetSubmitt;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public int UpdateTimeSheetStaus(int ID, int StatusID, string Commant,int contractorID,int LoginUserID)
        {
            try
            {
                cmd6 = db.GetStoredProcCommand("DN_UpdateTimesheetStausID");
                db.AddInParameter(cmd6, "@ID", DbType.Int32, ID);
                db.AddInParameter(cmd6, "@TimesheetStatusID", DbType.Int32, StatusID);
                db.AddInParameter(cmd6, "@Notes", DbType.String, Commant);
                db.AddInParameter(cmd6, "@contractorID", DbType.Int32, contractorID);
                db.AddInParameter(cmd6, "@LoginID", DbType.Int32, LoginUserID);
                db.AddOutParameter(cmd6, "@out", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd6);
                int getVal = (int)db.GetParameterValue(cmd6, "@out");

                return getVal;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }

        public int UpdateTimeApprovalAll(int TimesheetWCdateID, int StatusID, string Commant, int contractorID, int LoginUserID)
        {
            try
            {
                cmd7 = db.GetStoredProcCommand("DN_UpdateTimesheetStausID_ByApprovalAll");
                db.AddInParameter(cmd7, "@ID", DbType.Int32, TimesheetWCdateID);
                db.AddInParameter(cmd7, "@TimesheetStatusID", DbType.Int32, StatusID);
                db.AddInParameter(cmd7, "@Notes", DbType.String, Commant);
                db.AddInParameter(cmd7, "@contractorID", DbType.Int32, contractorID);
                db.AddInParameter(cmd7, "@LoginID", DbType.Int32, LoginUserID);
                db.AddOutParameter(cmd7, "@out", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd7);
                int getVal = (int)db.GetParameterValue(cmd7, "@out");

                return getVal;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }


        public DataSet ViewTimeSheet_SubmittApproval1(string WCDateID, int Key, int TimeSheetApproverID)
        {
            //Key 0 is The Timesheet Send ForApproval from Submitt send for Sebmitted one That status Is 2
            //Key 1 is The Timesheet Send ForApproval from Awaiting for Approval that status 1
            //Key 2 is The Timesheet Journal....
            try
            {
                DataSet ViewTimeSheetSubmittApproval = new DataSet();
                cmd8 = db.GetStoredProcCommand("DN_ViewPnedingTimeSheets");
                db.AddInParameter(cmd8, "@WCDateID", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(WCDateID) ? "01/01/1900" : WCDateID));
                db.AddInParameter(cmd8, "@TimeSheetApproverID", DbType.Int32, TimeSheetApproverID);
                db.AddInParameter(cmd8, "@key", DbType.Int32, Key);
                ViewTimeSheetSubmittApproval = db.ExecuteDataSet(cmd8);
                return ViewTimeSheetSubmittApproval;
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        //@ContractorID
        public DataSet ViewTimeSheet_SubmittApproval1(string WCDateID, int Key, int TimeSheetApproverID, int ContractorID)
        {
            //Key 0 is The Timesheet Send ForApproval from Submitt send for Sebmitted one That status Is 2
            //Key 1 is The Timesheet Send ForApproval from Awaiting for Approval that status 1
            //Key 2 is The Timesheet Journal....
            try
            {
                DataSet ViewTimeSheetSubmittApproval = new DataSet();
                cmd8 = db.GetStoredProcCommand("DN_ViewPnedingTimeSheets");
                db.AddInParameter(cmd8, "@WCDateID", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(WCDateID) ? "01/01/1900" : WCDateID));
                db.AddInParameter(cmd8, "@TimeSheetApproverID", DbType.Int32, TimeSheetApproverID);
                db.AddInParameter(cmd8, "@key", DbType.Int32, Key);
                db.AddInParameter(cmd8, "@ContractorID", DbType.Int32, ContractorID);
                ViewTimeSheetSubmittApproval = db.ExecuteDataSet(cmd8);
                return ViewTimeSheetSubmittApproval;
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        #region Mailing Part display

        public string GetTimesheet_ResourceMailID(int ResourceID)
        {
            return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select  EmailAddress from Contractors as c where c.ID =  @UID", new SqlParameter("@UID", ResourceID)).ToString();
        }

        #endregion

        public DataTable  GetWCDATEID(String WCDATE)
        {
       
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DN_GetWCDATEID", conn))
                {
                    cmd.Parameters.AddWithValue("WCDate", Convert.ToDateTime(WCDATE));
                
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        
                     table.Load(reader);

                       return table;
                    }
                }
            }


           
        }


        public DataTable ContractorID(int WCDATEID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DN_GetContractorIDTime", conn))
                {
                    cmd.Parameters.AddWithValue("WCDate", WCDATEID);

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();

                        table.Load(reader);

                        return table;
                    }
                }
            }



        }
        #region Newimplementaion for Approvers

        public bool  CheckTimesheetApproversExists(int contratorID)
        {
            int TimesheetFirstApprover=0;
            int TimesheetSecondAppriver=0;
            bool Flag = false;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select isnull(TimeApproveID,0) as TimeApproveID ,isnull(SecondTSApprover,0)as SecondTSApprover from Contractors where ID={0}", contratorID));
            while (dr.Read())
            {
                TimesheetFirstApprover = Convert.ToInt32(dr["TimeApproveID"]);
                TimesheetSecondAppriver = Convert.ToInt32(dr["SecondTSApprover"]);

            }
            dr.Close();

            if ((TimesheetFirstApprover != 0) && (TimesheetSecondAppriver != 0))
            {
                Flag = true;

            }
            else
            {
                Flag = false;
            }

          


            return Flag;

        }

        public DataSet GetApprovallist(int ContratorID)
        {
            DataSet Dt = new DataSet();
            cmd5 = db.GetSqlStringCommand("select isnull(TimeApproveID,0) as TimeApproveID,isnull(SecondTSApprover,0)as SecondTSApprover  from Contractors where ID=" + ContratorID);
                       
            Dt = db.ExecuteDataSet(cmd5);
            return Dt;

            

        }
        public int updatePrimaryApproverStatus(int TimesheetApproverID, int flag, int contractorID,int OptionalApproverID, int WCDateID, int stausID)
        {
            cmd7 = db.GetStoredProcCommand("DN_UpdateTimesheetApproberStatus");
            db.AddInParameter(cmd7, "@WCDateID", DbType.Int32, WCDateID);
            db.AddInParameter(cmd7, "@ApproverID", DbType.Int32, TimesheetApproverID);
            db.AddInParameter(cmd7, "@flag", DbType.Int32, flag);
            db.AddInParameter(cmd7, "@ContractorID", DbType.Int32, contractorID);
            db.AddInParameter(cmd7, "@OptionalApproverID", DbType.Int32, OptionalApproverID);
            db.AddInParameter(cmd7, "@TimesheetApproverStatus", DbType.Int32, stausID);
             db.AddOutParameter(cmd7, "@out", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd7);
            int getVal = (int)db.GetParameterValue(cmd7, "@out");


            return getVal;
        }


        public int CheckFlgStatusofApprover(int WCDateID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select Flag  from  TimesheetWCDate where WCDateID=@WCDateID", new SqlParameter("@WCDateID", WCDateID)));
        }


        public int CheckFlgStatusandupdate(int WCDateID,int PrimaryTimesheet,int optinalTimesheet)
        {
            if (optinalTimesheet == 0)
            {

                return Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select Flag  from  TimesheetWCDate where WCDateID=@WCDateID and ApproverID =@ApproverID ", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@ApproverID", PrimaryTimesheet)));
            }
            else
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select Flag  from  TimesheetWCDate where WCDateID=@WCDateID and OptionalApproverID=@OptionalApproverID", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@OptionalApproverID", optinalTimesheet)));
            }
        }

        public int CheckFlgStatusofEachTimesheetupdate(int TimesheetID)
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(StatusFlag,0) as Flag  from  TimesheetEntry where ID=@WCDateID", new SqlParameter("@WCDateID", TimesheetID)));
           
        }



        public int UpdateFlagTimesheetentry(int statusflag, int TimesheetEntryID, int ApproverType, int TimeSheetApproversStatusID)
        {
            cmd7 = db.GetStoredProcCommand("DN_UpdatetimesheetFlag");
            db.AddInParameter(cmd7, "@TimesheetID", DbType.Int32, TimesheetEntryID);
            db.AddInParameter(cmd7, "@statusflag", DbType.Int32, statusflag);
            db.AddInParameter(cmd7, "@approverType", DbType.Int32, ApproverType);
            db.AddInParameter(cmd7, "@TimeSheetApproversStatusID", DbType.Int32, TimeSheetApproversStatusID);
            db.AddOutParameter(cmd7, "@out", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd7);
            int getVal = (int)db.GetParameterValue(cmd7, "@out");


            return getVal;

        }


        #endregion


        



    }



    


}
