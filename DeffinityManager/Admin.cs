using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.ProgrammeEntitys;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for Admin
/// </summary>
namespace Deffinity.ProgrammeManagers
{
    public class Admin
    {
      
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        DataTable dt;
        DataSet ds;
        int retVal = 0;
        

        public Admin()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Programme Management
        /// <summary>
        /// if OutVal retuns 1 -> success fully inserted/update
        /// else OutVal retuns 2-> faild to insert/updated because of already exists
        /// </summary>
        /// <param name="InsertOrUpdate"></param>
        /// <param name="programme"></param>
        /// <param name="OutVal"></param>
        /// <returns></returns>
        public static int InsertUpdateProgramme(bool InsertOrUpdate, Programme programme, out int OutVal)
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd;
            int retVal = 0;
            if (InsertOrUpdate)
            {
                cmd = db.GetStoredProcCommand("DN_InsertOwner");
            }
            else
            {
                cmd = db.GetStoredProcCommand("DN_updateOwner");
                db.AddInParameter(cmd, "@ID", DbType.Int32, programme.ProgrammeID);
            }
            db.AddInParameter(cmd, "@OperationsOwners", DbType.String, programme.OperationsOwners);
            db.AddInParameter(cmd, "@EmailAddress", DbType.String, programme.EmailAddress);
            db.AddInParameter(cmd, "@Visible", DbType.String, programme.Visible);
            db.AddInParameter(cmd, "@ProgrammOwnerID", DbType.Int32, programme.ProgrammOwnerID);
            db.AddInParameter(cmd, "@DateofReview", DbType.DateTime, programme.DateofReview);
            db.AddInParameter(cmd, "@TargetSLAPercentCompleted", DbType.Int32, programme.TargetSLAPercentCompleted);
            db.AddInParameter(cmd, "@ExpectedStartDate", DbType.DateTime, programme.ExpectedStartDate);
            db.AddInParameter(cmd, "@ExpectedEndDate", DbType.DateTime, programme.ExpectedEndDate);
            db.AddInParameter(cmd, "@CostCenter", DbType.String, programme.CostCenter);
            db.AddInParameter(cmd, "@MaximumBudget", DbType.String, programme.MaximumBudget);
            db.AddInParameter(cmd, "@Justification", DbType.String, programme.Justification);
            db.AddInParameter(cmd, "@Description", DbType.String, programme.Description);
            db.AddInParameter(cmd, "@BenefitsToOrganisation", DbType.String, programme.BenefitsToOrganisation);
            db.AddInParameter(cmd, "@StrategicFitAlignment", DbType.String, programme.StrategicFitAlignment);
            db.AddInParameter(cmd, "@VisionStatement", DbType.String, programme.VisionStatement);
            db.AddInParameter(cmd, "@RisksAndIssues", DbType.String, programme.RisksAndIssues);
            db.AddInParameter(cmd, "@ResourcesRequired", DbType.String, programme.ResourcesRequired);
            db.AddInParameter(cmd, "@Approve", DbType.Boolean, programme.Approve);
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, programme.PortfolioID);
            db.AddInParameter(cmd, "@MasterProgramme", DbType.Int32, programme.MasterProgrammeID);
            db.AddInParameter(cmd, "@Level", DbType.Int32, programme.LevelID);
            db.AddOutParameter(cmd, "@OutVal", DbType.Int32, 4);
            retVal = db.ExecuteNonQuery(cmd);

            OutVal = int.Parse(db.GetParameterValue(cmd, "OutVal").ToString());

            cmd.Dispose();
            return retVal;
        }

        public static int DeleteProgramme(Programme programme, out int OutVal)
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd;
            int retVal = 0;

            cmd = db.GetStoredProcCommand("DN_DeleteOwner");
            db.AddInParameter(cmd, "@ID", DbType.Int32, programme.ProgrammeID);
            db.AddOutParameter(cmd, "@OutVal", DbType.Int32, 4);
            retVal = db.ExecuteNonQuery(cmd);
            OutVal = int.Parse(db.GetParameterValue(cmd, "OutVal").ToString());

            cmd.Dispose();
            return retVal;
        }
        


        public Programme SelectProgramme(int id)
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd;
            //int retVal = 0;
            Programme programme = new Programme();
            cmd = db.GetStoredProcCommand("DN_SelectProgramme");
            db.AddInParameter(cmd, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    programme.ProgrammeID = Convert.ToInt32(dr["ID"]);
                    programme.OperationsOwners = dr["OperationsOwners"].ToString();
                    programme.EmailAddress = dr["EmailAddress"].ToString();
                    programme.Visible = dr["Visible"].ToString();
                    programme.ProgrammOwnerID = Convert.ToInt32(dr["ProgrammOwnerID"].ToString());
                    
                    programme.TargetSLAPercentCompleted = string.IsNullOrEmpty(dr["TargetSLAPercentCompleted"].ToString()) ? 0 : Convert.ToInt32(dr["TargetSLAPercentCompleted"]);
                    programme.ExpectedStartDate = string.IsNullOrEmpty(dr["ExpectedStartDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(string.Format(systemdefaults.GetStringDateformat(), dr["ExpectedStartDate"]));
                    programme.ExpectedEndDate = string.IsNullOrEmpty(dr["ExpectedEndDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), dr["ExpectedEndDate"]));
                    programme.CostCenter = dr["CostCenter"].ToString();
                    programme.MaximumBudget = string.IsNullOrEmpty(dr["MaximumBudget"].ToString()) ? 0 : Convert.ToDouble(dr["MaximumBudget"]);
                    programme.Justification = dr["Justification"].ToString();
                    programme.Description = dr["Description"].ToString();
                    programme.BenefitsToOrganisation = dr["BenefitsToOrganisation"].ToString();
                    programme.StrategicFitAlignment = dr["StrategicFitAlignment"].ToString();
                    programme.VisionStatement = dr["VisionStatement"].ToString();
                    programme.RisksAndIssues = dr["RisksAndIssues"].ToString();
                    programme.ResourcesRequired = dr["ResourcesRequired"].ToString();
                    programme.Approve = string.IsNullOrEmpty(dr["Approve"].ToString()) ? false : Convert.ToBoolean(dr["Approve"]);
                    programme.PortfolioID = Convert.ToInt32(dr["PortfolioID"].ToString());
                    programme.MasterProgrammeID = Convert.ToInt32(dr["MasterProgramme"].ToString());
                    programme.LevelID = Convert.ToInt32(dr["Level"].ToString());
                }
            }
            cmd.Dispose();
            return programme;
        }
        #endregion
        #region Programme Stucture
        public int AssociateCheckListInsert(int CheckListID, int ProgrammeID)
        {
            cmd = db.GetStoredProcCommand("DN_AssignedTemplatesToGroups");
            db.AddInParameter(cmd, "@TemplateID", DbType.Int32, CheckListID);
            db.AddInParameter(cmd, "@GroupOwnerID", DbType.Int32, ProgrammeID);
            db.AddOutParameter(cmd, "@OutValue", DbType.Int32, 8);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "OutValue");
            cmd.Dispose();
            return getVal;
        }
        public int AssociateQACheckListInsert(int QAchecklistID, int ProgrammeID)
        {
            cmd = db.GetStoredProcCommand("DN_AssocitedQAchecklistInsert");
            db.AddInParameter(cmd, "@QAchecklistID", DbType.Int32, QAchecklistID);
            db.AddInParameter(cmd, "@GroupOwner", DbType.Int32, ProgrammeID);
            db.AddOutParameter(cmd, "@OutValue", DbType.Int32, 8);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "OutValue");
            cmd.Dispose();
            return getVal;
        }
        public int AssociateUserInsert(int ResourceID, int ProgrammeID)
        {
            cmd = db.GetStoredProcCommand("DN_InsertCtrOwner");
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, ResourceID);
            db.AddInParameter(cmd, "@OpsOwner", DbType.Int32, ProgrammeID);
            db.AddOutParameter(cmd, "@Outval", DbType.Int32, 8);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "Outval");
            cmd.Dispose();
            return getVal;
        }
        //get data table according to search
        public DataTable SearchDataBinding(string Name, string Skils)
        {
            try
            {
                //dt=db.ExecuteDataSet(cmd, "DN_SelectResourceBasedSearch", new DbParameter("@Name", (string.Empty == Name ? string.Empty : Name)), new DbParameter("@Name", (string.Empty == Name ? string.Empty : Name))).Tables[0];
                cmd = db.GetStoredProcCommand("DN_SelectResourceBasedSearch");
                db.AddInParameter(cmd, "@Name", DbType.String, (string.Empty == Name ? string.Empty : Name));
                db.AddInParameter(cmd, "@Skils", DbType.String, (string.Empty == Skils ? string.Empty : Skils));
                db.ExecuteNonQuery(cmd);
                dt = db.ExecuteDataSet(cmd).Tables[0];
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "search button");
            }
            return dt;
        }
        #endregion
        #region Prgramme Assessment
        public static int InsertUpdateProgrammeAssessment(bool InsertOrUpdate, ProgrammeAssessmentCL Assessment)
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd;
            int retVal = 0;
            if (InsertOrUpdate)
            {
                cmd = db.GetStoredProcCommand("DN_InsertProgrammeAssessment");
            }
            else
            {
                cmd = db.GetStoredProcCommand("DN_UpdateProgrammeAssessment");
                db.AddInParameter(cmd, "@ID", DbType.Int32, Assessment.ID);
            }
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, Assessment.PortfolioID);
            db.AddInParameter(cmd, "@ProgrammeID", DbType.Int32, Assessment.ProgrammeID);
            db.AddInParameter(cmd, "@ProgressToDate", DbType.String, Assessment.ProgressToDate);
            db.AddInParameter(cmd, "@Benefits", DbType.String, Assessment.Benefits);
            db.AddInParameter(cmd, "@EmergentOpportunities", DbType.String, Assessment.EmergentOpportunities);
            db.AddInParameter(cmd, "@PaceOfProgress", DbType.String, Assessment.PaceOfProgress);
            db.AddInParameter(cmd, "@datelogged", DbType.DateTime, Assessment.DateLogged);
            
            retVal = db.ExecuteNonQuery(cmd);
            cmd.Dispose();

            return retVal;
        }
        //DN_SelectProgrammeAssessment
        public ProgrammeAssessmentCL SelectProgrammeAssessment(int id)
        {
             ProgrammeAssessmentCL Assessment = new ProgrammeAssessmentCL();
             try
             {
                 Database db = DatabaseFactory.CreateDatabase("DBstring");
                 DbCommand cmd;
                 //int retVal = 0;

                 cmd = db.GetStoredProcCommand("DN_SelectProgrammeAssessment");
                 db.AddInParameter(cmd, "ID", DbType.Int32, id);
                 using (IDataReader dr = db.ExecuteReader(cmd))
                 {
                     while (dr.Read())
                     {
                         Assessment.Benefits = dr["Benefits"].ToString();
                         Assessment.EmergentOpportunities = dr["EmergentOpportunities"].ToString();
                         Assessment.PaceOfProgress = dr["PaceOfProgress"].ToString();
                         Assessment.ProgressToDate = dr["ProgressToDate"].ToString();
                         Assessment.ID = Convert.ToInt32(dr["ID"].ToString());
                         Assessment.DateLogged = Convert.ToDateTime(dr["datelogged"].ToString());

                     }
                 }
                 cmd.Dispose();
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
            return Assessment;
        }
        #endregion
        #region Issue

        #endregion


        #region Permissions Level
        //Created on 03/06/2010
        public static int CheckLoginUserPermission(int UserID)
        {
            int val = 0;
            try
            {
                
                PortfolioDataContext portfolioDB = new PortfolioDataContext();
                UserDataContext userDB = new UserDataContext();
                projectTaskDataContext projetsDB = new projectTaskDataContext();

                //var getPortfolio = (from r in projetsDB.ProjectDetails
                //                    where r.ProjectReference == QueryStringValues.Project
                //                    select new { r.Portfolio }).ToList().FirstOrDefault();
                //if (getPortfolio != null)
                //{
                //    var getCustomerPermission = (from r in portfolioDB.PermissionLevels
                //                                 where r.UserID == sessionKeys.UID && r.PortfolioID == getPortfolio.Portfolio
                //                                 && r.LevelType == 1
                //                                 select new { r.RoleID }).ToList().FirstOrDefault();

                //    if (getCustomerPermission == null)
                //    {

                        //var getPermission = (from r in portfolioDB.ProjectPermissionLevels
                        //                     where r.UserID == UserID && r.ProjectReference == QueryStringValues.Project
                        //                     select r).ToList().FirstOrDefault();
                        //if (getPermission != null)
                        //{
                        //    val = getPermission.Role.Value;
                        //}
                    
                    //else
                    //{
                    //    val = getCustomerPermission.RoleID.Value;
                    //}
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_CheckProjectPermissions", new
                SqlParameter("@UserID", sessionKeys.UID),
                new SqlParameter("@ProjectReference", sessionKeys.Project)).Tables[0];

                val = Convert.ToInt32(dt.Rows[0]["val"].ToString());
                }
    
            
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return val;
        }

        public static int GetTeamID(int UserID)
        {
            int val = 0;
            try
            {

                //PortfolioDataContext portfolioDB = new PortfolioDataContext();
                //UserDataContext userDB = new UserDataContext();
                //projectTaskDataContext projetsDB = new projectTaskDataContext();
                //var getPermission = (from r in portfolioDB.ProjectPermissionLevels
                //                     where r.ProjectReference == QueryStringValues.Project
                //                     select r).ToList();
                //var getTeams = (from r in portfolioDB.TeamMembers
                //                select r).ToList();

                //var getTeamsUser = (from r in getTeams
                //                    join t in getPermission on r.TeamID equals t.TeamID
                //                    where t.TeamID != 0 && r.Name == UserID
                //                    orderby t.Role
                //                    select new { t.UserID, t.Role }).ToList().FirstOrDefault();

                //if (getTeamsUser != null)
                //{
                //    val = getTeamsUser.Role.Value;
                //}
                if (CheckLoginUserPermission(UserID) >= 2)
                {
                    DataTable dt = new DataTable();
                    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_CheckUserPermissions", new
                    SqlParameter("@UserID", sessionKeys.UID),
                    new SqlParameter("@ProjectReference", sessionKeys.Project)).Tables[0];

                    val = Convert.ToInt32(dt.Rows[0]["Role"].ToString());
                }
                else
                {
                    val = CheckLoginUserPermission(UserID);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return val;
        }
        #endregion
    }

}
