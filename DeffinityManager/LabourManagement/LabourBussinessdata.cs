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
using Deffinity.LabourEntity1;
using Deffinity.DataBaseNameforLabourTimesheet1;
namespace Deffinity.BussinessData
{
    
    public class LabourBussinessdata
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        DataBaseNameforLabourTimesheet SPName = new DataBaseNameforLabourTimesheet();

        public LabourBussinessdata()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public  int InsertLabourTimesheetdata(LabourEntity TimesheetlabourInsertEntity)
        {
           

            cmd = db.GetStoredProcCommand(SPName.InsertlabourTimesheetdata);
            db.AddInParameter(cmd, "@ID", DbType.Int32, 0);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, TimesheetlabourInsertEntity._LabourContractorID);
            db.AddInParameter(cmd, "@DateEnter", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(TimesheetlabourInsertEntity._Labour_DateEnter) ? "01/01/1900" : TimesheetlabourInsertEntity._Labour_DateEnter));
            db.AddInParameter(cmd, "@Typeofhours", DbType.Int32, TimesheetlabourInsertEntity._Labour_TypeofHours);
            db.AddInParameter(cmd, "@Hours", DbType.Double,TimesheetlabourInsertEntity._Labour_Hours);
            db.AddInParameter(cmd, "@TaskID", DbType.Int32, TimesheetlabourInsertEntity._Labour_Task);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, TimesheetlabourInsertEntity._Labour_Project);
            db.AddInParameter(cmd, "@SRNumber", DbType.String, TimesheetlabourInsertEntity._Labour_SRNumber);
            db.AddInParameter(cmd, "@Notes", DbType.String, TimesheetlabourInsertEntity._Labour_Notes);
            db.AddInParameter(cmd, "@labourapproveID", DbType.Int32, TimesheetlabourInsertEntity.labouridapproverid);
            db.AddOutParameter(cmd, "@out", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@out");


            return getVal;
        }


        public int IUpdateLabourTimesheetdata(LabourEntity TimesheetlabourInsertEntity)
        {


            cmd = db.GetStoredProcCommand(SPName.InsertlabourTimesheetdata);
            db.AddInParameter(cmd, "@ID", DbType.Int32, TimesheetlabourInsertEntity.labourid);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, TimesheetlabourInsertEntity._LabourContractorID);
            db.AddInParameter(cmd, "@DateEnter", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(TimesheetlabourInsertEntity._Labour_DateEnter) ? "01/01/1900" : TimesheetlabourInsertEntity._Labour_DateEnter));
            db.AddInParameter(cmd, "@Typeofhours", DbType.Int32, TimesheetlabourInsertEntity._Labour_TypeofHours);
            db.AddInParameter(cmd, "@Hours", DbType.Double, TimesheetlabourInsertEntity._Labour_Hours);
            db.AddInParameter(cmd, "@TaskID", DbType.Int32, TimesheetlabourInsertEntity._Labour_Task);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, TimesheetlabourInsertEntity._Labour_Project);
            db.AddInParameter(cmd, "@SRNumber", DbType.String, TimesheetlabourInsertEntity._Labour_SRNumber);
            db.AddInParameter(cmd, "@Notes", DbType.String, TimesheetlabourInsertEntity._Labour_Notes);
            db.AddInParameter(cmd, "@labourapproveID", DbType.Int32, TimesheetlabourInsertEntity.labouridapproverid);
            db.AddOutParameter(cmd, "@out", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@out");


            return getVal;
        }

        public DataSet LabourTimesheetData(LabourEntity DisplayLabourEntries)
        {

            DataSet DisplayApprove = new DataSet();
            cmd = db.GetStoredProcCommand(SPName.FillLabourTimesheetData);
            db.AddInParameter(cmd, "@DateEnter", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(DisplayLabourEntries._Labour_DateEnter) ? "01/01/1900" : DisplayLabourEntries._Labour_DateEnter));
            db.AddInParameter(cmd, "@labourapproveID", DbType.Int32, DisplayLabourEntries.labouridapproverid);
          
            DisplayApprove = db.ExecuteDataSet(cmd);
            return DisplayApprove;

        }

        public DataTable GetProjectData(LabourEntity ProjectValues)
        {
            DataTable Dt=new DataTable();
            SqlCommand cmd1 = new SqlCommand(SPName.GetTaskrekatedProjects, con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@ProjectReference", SqlDbType.Int, 4).Value = ProjectValues._Labour_Project;
            cmd1.Parameters.Add("@contractorID", SqlDbType.Int, 4).Value = ProjectValues._LabourContractorID;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(Dt);
            return Dt;
        }

        public DataTable GetProjectTaskList(int project)
        {
           
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, SPName.GetTaskrekatedProjects, new SqlParameter("@ProjectReference", project), new SqlParameter("@contractorID", sessionKeys.UID)).Tables[0];
        }

        public DataTable GetProjectList()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, SPName.TimesheetProjectTitle, new SqlParameter("@ContractorID", sessionKeys.UID)).Tables[0];
        }
    


    }
}
