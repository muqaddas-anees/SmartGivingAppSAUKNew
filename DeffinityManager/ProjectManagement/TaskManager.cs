using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Deffinity.Project;
//using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net.Mail;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Generic;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.ProgrammeManagers;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

/// <summary>
/// Summary description for TaskManager
/// </summary>

namespace Deffinity.ProjectTasksManagers
{
    public class ProjectTasksManager
    {
        /// <summary>
        /// Display list of tasks based on projectreference
        /// <param name="ProjectReference"></param>        
        /// <returns>Project task items based on project reference</returns>
        /// </summary>
        public static Task ProjectTaskList_Select(int ID)
        {
            Task taskenity = new Task();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_ProjectTaskSelect", new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                        taskenity.AmberDays = int.Parse(string.IsNullOrEmpty(dr["AmberDays"].ToString()) ? "0" : dr["AmberDays"].ToString());
                        //taskenity.Approved = (string.IsNullOrEmpty(dr["Approved"].ToString())?bool.FalseString():bool.Parse(dr["Approved"].ToString()));
                        taskenity.BCExtension = dr["BCExtension"].ToString();
                        taskenity.BestCaseTime = int.Parse(string.IsNullOrEmpty(dr["BestCaseTime"].ToString()) ? "0" : dr["BestCaseTime"].ToString());
                        taskenity.CheckPoint_Notes = dr["CheckPoint_Notes"].ToString();
                        taskenity.Comments = dr["Comments"].ToString();
                        taskenity.CompletedBy = int.Parse(string.IsNullOrEmpty(dr["CompletedBy"].ToString()) ? "0" : dr["CompletedBy"].ToString());
                        taskenity.CompletionDate = dr["CompletionDate"].ToString();
                        taskenity.Defects = int.Parse(string.IsNullOrEmpty(dr["Defects"].ToString()) ? "0" : dr["Defects"].ToString());
                        taskenity.GrossProfit = double.Parse(string.IsNullOrEmpty(dr["GrossProfit"].ToString()) ? "0" : dr["GrossProfit"].ToString());
                        taskenity.IncludeInValuation = dr["IncludeInValuation"].ToString();
                        taskenity.IndentLevel = int.Parse(string.IsNullOrEmpty(dr["IndentLevel"].ToString()) ? "0" : dr["IndentLevel"].ToString());
                        taskenity.ItemDescription = dr["ItemDescription"].ToString();
                        taskenity.ItemStatus = int.Parse(string.IsNullOrEmpty(dr["ItemStatus"].ToString()) ? "0" : dr["ItemStatus"].ToString());
                        taskenity.ListPosition = int.Parse(string.IsNullOrEmpty(dr["ListPosition"].ToString()) ? "0" : dr["ListPosition"].ToString());
                        taskenity.MCExtension = dr["MCExtension"].ToString();
                        taskenity.Notes = dr["Notes"].ToString();
                        taskenity.MostLikelyCaseTime = int.Parse(string.IsNullOrEmpty(dr["MostLikelyCaseTime"].ToString()) ? "0" : dr["MostLikelyCaseTime"].ToString());
                        taskenity.PercentComplete = dr["PercentComplete"].ToString();
                        taskenity.Price = double.Parse(string.IsNullOrEmpty(dr["Price"].ToString()) ? "0" : dr["Price"].ToString());
                        taskenity.ProjectEndDate = dr["ProjectEndDate"].ToString();
                        taskenity.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                        taskenity.ProjectStartDate = dr["ProjectStartDate"].ToString();
                        taskenity.QA = dr["QA"].ToString();
                        taskenity.QTY = int.Parse(string.IsNullOrEmpty(dr["QTY"].ToString()) ? "0" : dr["QTY"].ToString());
                        taskenity.RAGRequired = dr["RAGRequired"].ToString();
                        taskenity.RAGStatus = dr["RAGStatus"].ToString();
                        taskenity.RedDays = int.Parse(string.IsNullOrEmpty(dr["RedDays"].ToString()) ? "0" : dr["RedDays"].ToString());
                        taskenity.SellingPrice = double.Parse(string.IsNullOrEmpty(dr["SellingPrice"].ToString()) ? "0" : dr["SellingPrice"].ToString());
                        taskenity.StartDate = dr["StartDate"].ToString();
                        taskenity.WCExtension = dr["WCExtension"].ToString();
                        taskenity.WorstCaseTime = int.Parse(string.IsNullOrEmpty(dr["WorstCaseTime"].ToString()) ? "0" : dr["WorstCaseTime"].ToString());
                    }
                }
            }

            return taskenity;
        }

        public static bool ProjectTaskItemUpdate(Task taskentity)
        {
            int restult = 0;

            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ID", taskentity.ID),
                                                   new SqlParameter("@ProjectReference", taskentity.ProjectReference),
                                                   new SqlParameter("@ItemDescription", taskentity.ItemDescription),
                                                   new SqlParameter("@ListPosition", taskentity.ListPosition),
                                                   new SqlParameter("@StartDate", DateTime.Parse(string.IsNullOrEmpty(taskentity.StartDate) ?"01/01/1900":taskentity.StartDate)),
                                                   new SqlParameter("@CompletionDate", DateTime.Parse(string.IsNullOrEmpty(taskentity.CompletionDate)?"01/01/1900":taskentity.CompletionDate)),
                                                   new SqlParameter("@RAGStatus", taskentity.RAGStatus),
                                                   new SqlParameter("@PercentComplete", taskentity.PercentComplete),
                                                   new SqlParameter("@RAGRequired", taskentity.RAGRequired),
                                                   new SqlParameter("@AmberDays", taskentity.AmberDays),
                                                   new SqlParameter("@RedDays", taskentity.RedDays),
                                                   new SqlParameter("@ItemStatus", taskentity.ItemStatus),
                                                   new SqlParameter("@ProjectStartDate", DateTime.Parse(string.IsNullOrEmpty(taskentity.ProjectStartDate)?"01/01/1900":taskentity.ProjectStartDate)),
                                                   new SqlParameter("@ProjectEndDate", DateTime.Parse(string.IsNullOrEmpty(taskentity.ProjectEndDate)?"01/01/1900":taskentity.ProjectEndDate)),
                                                   new SqlParameter("@Comments", taskentity.Comments),
                                                   new SqlParameter("@IndentLevel", taskentity.IndentLevel),
                                                   new SqlParameter("@Price", taskentity.Price),
                                                   new SqlParameter("@QTY", taskentity.QTY),
                                                   new SqlParameter("@IncludeInValuation", taskentity.IncludeInValuation),
                                                   new SqlParameter("@Notes", taskentity.Notes),
                                                   new SqlParameter("@QA", taskentity.QA),
                                                   new SqlParameter("@SellingPrice", taskentity.SellingPrice),
                                                   new SqlParameter("@GrossProfit", taskentity.GrossProfit),
                                                   new SqlParameter("@CompletedBy", taskentity.CompletedBy),
                                                   new SqlParameter("@CostTotal", taskentity.CostTotal),
                                                   new SqlParameter("@SellingTotal", taskentity.SellingTotal),
                                                   new SqlParameter("@WorstCaseTime", taskentity.WorstCaseTime),
                                                   new SqlParameter("@BestCaseTime", taskentity.BestCaseTime),
                                                   new SqlParameter("@MostLikelyCaseTime", taskentity.MostLikelyCaseTime),
                                                   new SqlParameter("@WCExtension", taskentity.WCExtension),
                                                   new SqlParameter("@BCExtension", taskentity.BCExtension),
                                                   new SqlParameter("@MCExtension", taskentity.MCExtension),
                                                   new SqlParameter("@Approved", taskentity.Approved),
                                                   new SqlParameter("@Defects", taskentity.Defects),
                                                   new SqlParameter("@CheckPoint_Notes", taskentity.CheckPoint_Notes)                                                    
                                                    };

            restult = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_TaskItemUpdate", sqlParams);
            return ((restult > 0) ? true : false);

        }
        /// <summary>
        /// Display list of tasks based on projectreference
        /// <param name="ProjectReference"></param>        
        /// <returns>Project task items based on project reference</returns>
        /// </summary>
        public static DataTable ProjectTaskList_SelectAll(int ProjectReference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskSelectAll", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
        }
        /// <summary>
        /// Display list of tasks based on projectreference
        /// <param name="ProjectReference"></param>  
        /// <param name="IndentLevel"></param>
        /// <returns>Task items based on Project reference and IndentLevel (IndentLevel zero is heading)</returns>
        /// </summary>
        public static DataTable ProjectTaskList_SelectAll(int ProjectReference, bool IndentLevel)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskSelectAll", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@IndentLevel", IndentLevel)).Tables[0];
        }
        /// <summary>
        /// Display list of tasks based on projectreference
        /// <param name="ProjectReference"></param>  
        /// <param name="IndentLevel"></param>
        /// <returns>Task items based on Project reference and IndentLevel (IndentLevel zero is heading)</returns>
        /// </summary>
        public static DataTable ProjectTaskList_SelectAll(int ProjectReference, bool IndentLevel, string RAG)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskSelectAll", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@RAGstatus", RAG)).Tables[0];
        }
        /// <summary>
        /// Display list of tasks based on projectreference and assined user
        /// <param name="ProjectReference"></param>
        /// <param name="PUID"></param>
        /// <returns>Project task items based on project reference and project user id</returns>
        /// </summary>
        public static DataTable ProjectTask_SelectAll(int ProjectReference, int PUID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectProjItmes", new SqlParameter("@Project", ProjectReference), new SqlParameter("@AC2PID", PUID)).Tables[0];
        }
        /// <summary>
        /// Display list of tasks based on projectreference and assined user
        /// <param name="ProjectReference"></param>
        /// <param name="PUID"></param>
        /// <returns>Project task items based on project reference and project user id</returns>
        /// </summary>
        public static DataTable ProjectTask_GridSelect(int ProjectReference)
        {
            //string cacheKey = "Tasklist_" + QueryStringValues.Project.ToString();
            DataTable ProjectTaks;//= HttpContext.Current.Cache[cacheKey] as DataTable;

            //if (ProjectTaks == null)
            //{ 

            ProjectTaks = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectTaskItems", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
            //HttpContext.Current.Cache.Insert(cacheKey, ProjectTaks);   
            //}
            return ProjectTaks;
        }
        public static void ProjectTask_RemoveCache()
        {
            string cacheKey = "Tasklist_" + QueryStringValues.Project.ToString();
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        public static void ProjectTask_GridUpdate(int ID, string ItemDescription, DateTime ProjectStartDate, DateTime ProjectEndDate,char RAGRequired, int ItemStatus, char QA,bool isMilestone)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TaskitemUpdateNew",
                new SqlParameter("@ID", ID),
                new SqlParameter("@ItemDescription", ItemDescription),
                new SqlParameter("@ProjectStartDate", ProjectStartDate),
                new SqlParameter("@ProjectEndDate", ProjectEndDate),
                new SqlParameter("@RAGRequired", RAGRequired),
                new SqlParameter("@ItemStatus", ItemStatus),
                new SqlParameter("@QA", QA),new SqlParameter("@isMilestone",isMilestone));
        }

        #region Task Display based on indent level

        public static string DisplayIndentLevel(string Description, int IndentLevel)
        {
            string val = "";
            if (IndentLevel == 0)
            {
                val = string.Format("<div style='padding-left:0px'><strong>{0}</strong></div>", Description);
            }
            else if (IndentLevel == 1)
            {
                val = string.Format("<div style='padding-left:10px'><strong>{0}</strong></div>", Description);
            }
            else if (IndentLevel == 2)
            {
                val = string.Format("<div style='padding-left:20px'>{0}</div>", Description);
            }
            else if (IndentLevel == 3)
            {
                val = string.Format("<div style='padding-left:30px'>{0}</div>", Description);
            }
            else if (IndentLevel == 4)
            {
                val = string.Format("<div style='padding-left:40px'>{0}</div>", Description);
            }
            else if (IndentLevel == 5)
            {
                val = string.Format("<div style='padding-left:50px'>{0}</div>", Description);
            }
            else if (IndentLevel == 6)
            {
                val = string.Format("<div style='padding-left:60px'>{0}</div>", Description);
            }
            else if (IndentLevel == 7)
            {
                val = string.Format("<div style='padding-left:70px'>{0}</div>", Description);
            }
            else if (IndentLevel == 8)
            {
                val = string.Format("<div style='padding-left:80px'>{0}</div>", Description);
            }
            else if (IndentLevel == 9)
            {
                val = string.Format("<div style='padding-left:90px'>{0}</div>", Description);
            }

            return val;
        }
        #endregion

        #region Task mouseover popup window
        public static string DisplayGridPopUp(string str)
        {
            string retVal = "";
            string[] arInfo = new string[22];
            try
            {
                // define which character is seperating fields
                char[] splitter = { ',' };
                arInfo = str.Split(splitter);
                retVal = retVal + "<div style=background-color:#F9FBFD><table style=width:100%><tr style=background-color:#ECEEEE><td style=width:41%><strong></strong></td><td><strong></strong></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Task:</strong></td><td>" + arInfo[0] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Start Date:</strong></td><td>" + string.Format("{0:d}", arInfo[1]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>End Date:</strong></td><td>" + string.Format("{0:d}", arInfo[2]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Actual Completion  Date:</strong></td><td>" +(string.Format("{0:d}", arInfo[3])!="01/01/1900" ? string.Format("{0:d}", arInfo[3]):"" ) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Planned Date:</strong></td><td>" + (string.Format("{0:d}", arInfo[22]) != "01/01/1900" ? string.Format("{0:d}", arInfo[22]) : "") + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Amber Days:</strong></td><td>" + arInfo[4] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Red Days:</strong></td><td>" + arInfo[5] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>QTY:</strong></td><td>" + arInfo[6] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Cost Price:</strong></td><td>" + string.Format("{0:#.00}", arInfo[7]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Selling Price:</strong></td><td>" + string.Format("{0:#.00}", arInfo[8]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Gross Profit:</strong></td><td>" + string.Format("{0:p}", Convert.ToDouble(string.IsNullOrEmpty(arInfo[9]) ? "0" : arInfo[9])) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Status:</strong></td><td>" + arInfo[10] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Include In Valuation:</strong></td><td>" + arInfo[11] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>RAG Required:</strong></td><td>" + arInfo[12] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>QA:</strong></td><td>" + arInfo[13] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Indent Level:</strong></td><td>" + arInfo[14] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Estimated Time to Complete:</strong></td><td>" + arInfo[15] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Notes:</strong></td><td>" + arInfo[18] + "<br/></td></tr>";
                retVal = retVal + "</table></div>";

            }
            catch (Exception ex)
            {

            }

            return retVal;

        }

        public static string DisplayGridPopUpTask(string str)
        {
            string retVal = "";
            string[] arInfo = new string[6];
            try
            {
                // define which character is seperating fields
                char[] splitter = { ',' };
                arInfo = str.Split(splitter);
                retVal = retVal + "<div style=background-color:#F9FBFD><table style=width:100%><tr style=background-color:#ECEEEE><td style=width:41%><strong></strong></td><td><strong></strong></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Task:</strong></td><td>" + arInfo[0] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Start Date:</strong></td><td>" + string.Format("{0:d}", arInfo[4]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>End Date:</strong></td><td>" + string.Format("{0:d}", arInfo[5]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Status:</strong></td><td>" + arInfo[1] + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Completed Date:</strong></td><td>" + string.Format("{0:d}", arInfo[2]) + "<br/></td></tr>";
                retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Completed by:</strong></td><td>" + arInfo[3] + "<br/></td></tr>";

                retVal = retVal + "</table></div>";

            }
            catch (Exception ex)
            {

            }

            return retVal;

        }
        #endregion

        #region send Task alert mail
        public static string CreateTask(DateTime start, DateTime end, string sub, string msgBody)
        {
            StringBuilder sbvCalendar = new StringBuilder();

            //Header
            sbvCalendar.Append("METHOD: REQUEST");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("BEGIN:VCALENDAR");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("PRODID:-//Microsoft Corporation//Outlook ");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("MIMEDIR//ENVERSION:1.0");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("BEGIN:VEVENT");
            sbvCalendar.Append("\n");

            //DTSTART 
            sbvCalendar.Append("DTSTART:");
            string hour = start.Hour.ToString();
            if (hour.Length < 2) { hour = "0" + hour; }

            string min = start.Minute.ToString();
            if (min.Length < 2) { min = "0" + min; }

            string sec = start.Second.ToString();
            if (sec.Length < 2) { sec = "0" + sec; }

            string mon = start.Month.ToString();
            if (mon.Length < 2) { mon = "0" + mon; }

            string day = start.Day.ToString();
            if (day.Length < 2) { day = "0" + day; }

            sbvCalendar.Append(start.Year.ToString() + mon + day
                                   + "T" + hour + min + sec);
            sbvCalendar.Append("\n");

            //DTEND
            sbvCalendar.Append("DTEND:");
            hour = end.Hour.ToString();
            if (hour.Length < 2) { hour = "0" + hour; }

            min = end.Minute.ToString();
            if (min.Length < 2) { min = "0" + min; }

            sec = end.Second.ToString();
            if (sec.Length < 2) { sec = "0" + sec; }

            mon = end.Month.ToString();
            if (mon.Length < 2) { mon = "0" + mon; }

            day = end.Day.ToString();
            if (day.Length < 2) { day = "0" + day; }

            sbvCalendar.Append(end.Year.ToString() + mon +
                         day + "T" + hour + min + sec);
            sbvCalendar.Append("\n");

            //Location
            sbvCalendar.Append("LOCATION;ENCODING=QUOTED-PRINTABLE: "
                                                     + String.Empty);
            sbvCalendar.Append("\n");

            //Message body
            sbvCalendar.Append("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:"
                                                            + msgBody);
            sbvCalendar.Append("\n");

            //Subject
            sbvCalendar.Append("SUMMARY;ENCODING=QUOTED-PRINTABLE:"
                                                            + sub);
            sbvCalendar.Append("\n");

            //Priority
            sbvCalendar.Append("PRIORITY:3");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("END:VEVENT");
            sbvCalendar.Append("\n");
            sbvCalendar.Append("END:VCALENDAR");
            sbvCalendar.Append("\n");

            return sbvCalendar.ToString();
        }
        #endregion

        #region Task item list position
        public static void GridItemPosition(int oldPos, int newPos, int id, int pref)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TaskListPosition", new SqlParameter("@newPos", newPos),
                new SqlParameter("@oldPos", oldPos), new SqlParameter("@id", id), new SqlParameter("@ProjRef", pref));

        }
        #endregion

        #region Task indent value position
        public static void IndetIncreaseDecrease(int IndentLevel,int TaskID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectTaskItems set IndentLevel = @IndentLevel where ID = @TaskID", 
                new SqlParameter("@IndentLevel", IndentLevel), new SqlParameter("@TaskID", TaskID));
        }
        #endregion

        #region save check list template
        /// <summary>
        /// retuns status : 1- success 2- failed item already exists
        /// </summary>
        /// <param name="TemplateTitle"></param>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public static int InsertTemplate(int ProjectReference,string TemplateTitle,bool ReplaceTemplate)
        {
            
            SqlParameter OutMsg = new SqlParameter("@OutMsg",SqlDbType.Int,4);
            OutMsg.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertMaster", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@Description", TemplateTitle), OutMsg, new SqlParameter("@ReplaceTemplate", ReplaceTemplate));
            int msg = int.Parse(OutMsg.Value.ToString());
            
            return msg;
        }


        #endregion

        #region Checkpoint Update
        public static void CheckPointUpdate(int ID,int Defects,string Approved,string CheckPoint_Notes)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString,CommandType.StoredProcedure,"Deffinity_ProjectTasks_CheckpointUpdate",new SqlParameter("@ID",ID),new SqlParameter("@Defects",Defects),new SqlParameter("@Approved",Approved),new SqlParameter("@CheckPoint_Notes",CheckPoint_Notes));
        }
        #endregion

        #region ProjectTask Chart
        public static DataSet GetChartData(int ProjectReferece)
        {
            DataSet ds = new DataSet();
            try
            {
                //string Key = string.Format("{0}-{1}", CacheNames.DefaultNames.ProjectReference, ProjectReferece);
                //if (BaseCache.Cache_Select(Key) == null)
                //{
                //    DataSet ds = new DataSet();
                //    ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTask_Ganttchart", new SqlParameter("@ProjectReference", ProjectReferece));
                //    BaseCache.Cache_Insert(Key, ds);
                //}
                //return BaseCache.Cache_Select(Key) as DataSet;
                ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTask_Ganttchart", new SqlParameter("@ProjectReference", ProjectReferece));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return ds;
        }

        public static void TaskGantt_InsertUpdate(int TaskID ,int ProjectReference ,string ItemDescription,int IndentLevel,string             
ProjectStartDate,string ProjectEndDate,string CompletionDate,string StartDate,string PercentComplete,string Notes,                 
int AmberDays,int RedDays ,int AmberPercent ,int RedPercent ,string RAGRequired,string QA,string IncludeInValuation,
string ResourceIDs, int ItemStatus, string RAGStatus, bool isMilestone, int CategoryID,int LoggedUser,int SelectedTaskID,int TeamID,int CheckPointID,int customerId)
        {
            string default_date = "01/01/1900";
            try {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Task_InsertUpdate",
                    new SqlParameter("@TaskID",TaskID),
                    new SqlParameter("@ProjectReference",ProjectReference),
                    new SqlParameter("@ItemDescription",ItemDescription),
                    new SqlParameter("@IndentLevel",IndentLevel),
                    new SqlParameter("@ProjectStartDate", Convert.ToDateTime((string.IsNullOrEmpty(ProjectStartDate) ? default_date : ProjectStartDate))),
                    new SqlParameter("@ProjectEndDate", Convert.ToDateTime((string.IsNullOrEmpty(ProjectEndDate) ? default_date : ProjectEndDate))),
                    new SqlParameter("@CompletionDate", Convert.ToDateTime((string.IsNullOrEmpty(CompletionDate) ? default_date : CompletionDate))),
                    new SqlParameter("@StartDate", Convert.ToDateTime((string.IsNullOrEmpty(StartDate) ? default_date : StartDate))),
                    new SqlParameter("@PercentComplete",PercentComplete),
                    new SqlParameter("@Notes",Notes),
                    new SqlParameter("@AmberDays",AmberDays),
                    new SqlParameter("@RedDays",RedDays),
                    new SqlParameter("@AmberPercent",AmberPercent),
                    new SqlParameter("@RedPercent",RedPercent),
                    new SqlParameter("@RAGRequired", (string.IsNullOrEmpty(RAGRequired)?"N":RAGRequired)),
                    new SqlParameter("@QA",(string.IsNullOrEmpty(QA)?"N":QA)),
                    new SqlParameter("@IncludeInValuation",(string.IsNullOrEmpty(IncludeInValuation)?"N":IncludeInValuation)),
                    new SqlParameter("@ResourceIDs",ResourceIDs),
                    new SqlParameter("@ItemStatus", (ItemStatus == 0?1:ItemStatus) ),
                    new SqlParameter("@RAGStatus",(string.IsNullOrEmpty(RAGStatus)? "Green":RAGStatus)),
                     new SqlParameter("@isMilestone", isMilestone),
                      new SqlParameter("@CategoryID", CategoryID),
                      new SqlParameter("@LoggedUser", LoggedUser),
                      new SqlParameter("@selectedTaskid", SelectedTaskID),
                      new SqlParameter("@TeamID", TeamID),
                      new SqlParameter("@CheckPointID", CheckPointID),
                      new SqlParameter("@CustomerID", customerId)
                    );
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        /// <summary>
        /// type: 1-increase, 0-decrease
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="type"></param>
        public static void Update_Indentlevel(int TaskID, int type)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_IndentUpdate", new SqlParameter("@TaskID", TaskID), new SqlParameter("@extPram", type));
            
        }
        public static int Delete_ProjectTask(int TaskID, int ProjectReference)
        {
            int outval = 0;
            SqlParameter a_outval = new SqlParameter("@OutVal", SqlDbType.Int, 4);
            a_outval.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskItems_delete", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@TaskID", TaskID), a_outval);

            outval = int.Parse(a_outval.Value.ToString());

            return outval;
        }
        public static void Delete_ProjectTaskDependency(int TaskID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskDependency_delete", new SqlParameter("@TaskID", TaskID));

        }
        public static void Update_ProjectTaskDates(string startdate, string enddate, int taskid, int projectstatusid)
        {
            if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
            {
                if (projectstatusid == 2)
                {
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectTaskItems set StartDate = @startdate,CompletionDate =@enddate where ID= @TaskID", new SqlParameter("@startdate", Convert.ToDateTime(startdate)), new SqlParameter("@enddate", Convert.ToDateTime(enddate)), new SqlParameter("@TaskID", taskid));
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectTaskItems set ProjectStartDate = @startdate,ProjectEndDate =@enddate where ID= @TaskID", new SqlParameter("@startdate", Convert.ToDateTime(startdate)), new SqlParameter("@enddate", Convert.ToDateTime(enddate)), new SqlParameter("@TaskID", taskid));
                }
            }
        }
        public static void Delete_ProjectTasks(int projectreference)
        {
            //
            

                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "delete from  ProjectTaskItems where projectreference = @projectreference", new SqlParameter("@projectreference", projectreference));
           
        }
        public static void Update_TaskPosition(int newPos, int oldPos, int taskid, int ProjectReference)
        {
            
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TaskListPosition",
                new SqlParameter("@newPos", newPos), new SqlParameter("@oldPos", oldPos),
                new SqlParameter("@id", taskid), new SqlParameter("@ProjRef", ProjectReference));
        }

        public static int GetTaskMaxPosition(int ProjectReference)
        { 
            return int.Parse(SqlHelper.ExecuteScalar(Constants.DBString,CommandType.Text,"select COUNT(ID) from ProjectTaskItems where ProjectReference = @ProjectReference"
                , new SqlParameter("@ProjectReference", ProjectReference)).ToString());
        }

        public static void Dependency_add(int ProjectReference,int TaskID,int DependencyPosID)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectTaks_Dependency",
                 new SqlParameter("@ProjectReference", ProjectReference),
                 new SqlParameter("@TaskID", TaskID),
                  new SqlParameter("@Dependency", DependencyPosID));
           
        }
        public static void Dependency_del(int ProjectReference, int TaskID, int DependencyPosID)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectTaks_Dependency_del",
                 new SqlParameter("@ProjectReference", ProjectReference),
                 new SqlParameter("@TaskID", TaskID),
                  new SqlParameter("@Dependency", DependencyPosID));

        }

        public static void ProjectTask_DeleteResourceByTask(int ProjectReference, int TaskID)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ProjectItems_DeleteResourceByTask",
                new SqlParameter("@ProjectReference", ProjectReference),
                 new SqlParameter("@TaskID", TaskID));


        }
        public static void ProjectTask_CopyResourceByTask(int ProjectReference, int ListPosition_selected, int ListPosition_apply)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ProjectItems_CopyResourceByTask",
               new SqlParameter("@ProjectReference", ProjectReference),
                new SqlParameter("@Listposition_Selected", ListPosition_selected),
                new SqlParameter("@Listposition_Apply", ListPosition_apply));

        }
        public static void ProjectTask_MoveResourceByTask(int ProjectReference, int ListPosition_selected, int ListPosition_apply)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ProjectItems_MoveResourceByTask",
               new SqlParameter("@ProjectReference", ProjectReference),
                new SqlParameter("@Listposition_Selected", ListPosition_selected),
                new SqlParameter("@Listposition_Apply", ListPosition_apply));

        }

        #endregion

         #region Resource Plan - Project Task

        public static void ProjectTaskToResourceType_Insert(int TaskID, int ResourceTypeID, int QTY)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskToResourceType_insert", new SqlParameter("@TaskID", TaskID), new SqlParameter("@ResourceTypeID", ResourceTypeID), new SqlParameter("@QTY", QTY));
        }
        public static DataSet ProjectTaskToResourceType_Select(int TaskID)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskToResourceType_Select", new SqlParameter("@TaskID", TaskID));

            return ds;
        }
        public static void ProjectTaskToResourceType_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTaskToResourceType_Delete", new SqlParameter("@ID", ID));
        }
        #region Rakesh
        public static DataSet GetAllResourceTypes()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ResourceType_GetAll");
           return ds;
        }
        #endregion
        public static DataSet SelectTeamData(int PortfolioID)
        {
            DataSet ds = new DataSet();
            //string key = string.Format("{0}_{1}", CacheNames.DefaultNames.ResourcePlanner, sessionKeys.PortfolioID);

            try
            {

                //if (BaseCache.Cache_Select(key) == null)
                //{
                //    DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ResourceProjectPlanner", new SqlParameter("@PortfolioID", PortfolioID));
                //    BaseCache.Cache_Insert(key, ds);
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return ds;
        }
    #endregion 

        #region Check point Recurence
        public static void ProjectTaskRecur_InsertUpdate(int taskID, int projectRef, DateTime startDate, int recurrRange,
         string dayName,string QA)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ProjectsTask_AddReoccur",
                    new SqlParameter("@projectTaskID", taskID), new SqlParameter("@ProjectReference", projectRef),
                    new SqlParameter("@StartDate", startDate), new SqlParameter("@ReoccurRange", recurrRange),
                    new SqlParameter("@DayName", dayName),new SqlParameter("@QA",QA));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        #endregion

        #region Project task Planner


        public static CascadingDropDownNameValue[] ProjectTaskPlanner_GetTasks(int ProjectReference)
        {
            projectTaskDataContext taskPlanner = new projectTaskDataContext();
            List<CascadingDropDownNameValue> GetTasks = new List<CascadingDropDownNameValue>();

            DataSet ds = new DataSet();
            try
            {

                //ds=SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectTasks_GetTasks", new SqlParameter("@ProjectReference", ProjectReference));
                //foreach (DataRow dRow in ds.Tables[0].Rows)
                //{

                //    string ID = dRow["ID"].ToString();
                //    string ItemDescription = dRow["ItemDescription"].ToString();
                //    GetTasks.Add(new CascadingDropDownNameValue
                //    (ItemDescription, ID));
                //}
                GetTasks = (from r in taskPlanner.ProjectTaskItems
                            where r.ProjectReference == ProjectReference
                            select new CascadingDropDownNameValue { name = r.ItemDescription, value = r.ID.ToString() }).ToList();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            //CascadingDropDownNameValue[] GetTasks

            return GetTasks.ToArray();
        }
        public static CascadingDropDownNameValue[] ProjectTaskPlanner_GetProjets()
        {
            projectTaskDataContext taskPlanner = new projectTaskDataContext();
            List<CascadingDropDownNameValue> GetTasks = new List<CascadingDropDownNameValue>();

            DataSet ds = new DataSet();
            try
            {

                GetTasks = (from r in taskPlanner.ProjectDetails

                            select new CascadingDropDownNameValue { name = r.ProjectReferenceWithPrefix, value = r.ProjectReference.ToString() }).ToList();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            //CascadingDropDownNameValue[] GetTasks

            return GetTasks.ToArray();
        }
        public static int insertResource(string _strsql, string _txtEdit, int ID,int intRet)
        {


            //int k = insertResource("Deffinity_ResourceTypeInsertUpdate", "textbox.text", 0);
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand(_strsql);
                db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@Item", DbType.String, _txtEdit);
                db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd);
                int getVal = (int)db.GetParameterValue(cmd, "@output");
                cmd.Dispose();

                return getVal;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {

            }

        }


        #endregion

        public static bool CommandField(int SID,int UID)
        {
            bool vis = true;
            try
            {
                if (SID != 1)
                    {
                        int role = 0;
                        role = Admin.CheckLoginUserPermission(UID);
                        if (role == 3)
                        {

                            vis = false;
                            //  Disable();

                        }
                        role = Admin.GetTeamID(UID);
                        if (role == 3)
                        {
                            vis = false;

                            // Disable();

                        }

                    }
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return vis;

        }
        //28/07/2011 ------------sani
        public static CascadingDropDownNameValue[] Timesheet_GetResourceByTeam(int teamID)
        {
            PortfolioDataContext resources = new PortfolioDataContext();
            UserDataContext contractors=new UserDataContext();
            List<CascadingDropDownNameValue> GetSites = new List<CascadingDropDownNameValue>();
            try
            {
                GetSites = (from r in resources.TeamMembers
                            join c in contractors.Contractors on r.Name equals c.ID
                            where r.TeamID==teamID
                            orderby c.ContractorName
                            select new CascadingDropDownNameValue { name = c.ContractorName, value = c.ID.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetSites.ToArray();


        }
        public static DataSet ResourceByTeam(int teamID)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string key = string.Format("{0}_{1}", CacheNames.DefaultNames.ResourcePlanner, sessionKeys.PortfolioID);

            try
            {

                //if (BaseCache.Cache_Select(key) == null)
                //{
                //    DataSet ds = new DataSet();
                //ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ResourceProjectPlanner", new SqlParameter("@PortfolioID", PortfolioID));
                //    BaseCache.Cache_Insert(key, ds);
                //}
               
                ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT distinct ID,ContractorName FROM Contractors where id  in  (select name from TeamMember where TeamID=@TeamID)", new SqlParameter("@TeamID", teamID));
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return ds;
        }
        
        public static string ResourceIDByTask(int TaskID)
        {
            string retVal = string.Empty;
            try
            {
                retVal = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "ProjectTaskItems_ResourceIDByTask", new SqlParameter("@TaskID", TaskID)).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
                
        }
        public static string ResourceIDByListposition(int Listposition, int ProjectReference)
        {
            string retVal = string.Empty;
            try
            {
                retVal = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "ProjectTaskItems_ResourceIDByListposition", new SqlParameter("@Listposition", Listposition), new SqlParameter("@ProjectReference", ProjectReference)).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;

        }
        
    }
   
}
