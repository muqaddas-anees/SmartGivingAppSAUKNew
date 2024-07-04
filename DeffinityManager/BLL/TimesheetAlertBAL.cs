using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

namespace TimesheetMgt.BAL
{

    /// <summary>
    /// Summary description for TimesheetAlertBAL
    /// </summary>
    public class TimesheetAlertBAL
    {
        public TimesheetAlertBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void SendTimesheetAlert(int projectReference, int userId, string userName)
        {
            using (TimeSheetDataContext td = new TimeSheetDataContext())
            {
                // int projectRef = int.Parse(ddlProjectTile.SelectedValue);
                var totalHours = td.TimesheetEntries.Where(t => t.ProjectReference == projectReference && t.ContractorID == userId)
                                 .Select(t => t.Hours).Sum();
                if (totalHours.HasValue)
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {
                        var assignedContractorsToProjects = pd.AssignedContractorsToProjects.Where(a => a.ProjectReference == projectReference && a.ContractorID == userId).Select(a => a).FirstOrDefault();
                        if (assignedContractorsToProjects != null)
                        {
                            var notificationHours = assignedContractorsToProjects.NotificationRemainingHours.HasValue ? assignedContractorsToProjects.NotificationRemainingHours : 0;
                            if (notificationHours > 0)
                            {
                                if (notificationHours <= totalHours)
                                {
                                    var maxHours = assignedContractorsToProjects.MaxHoursAllocated.HasValue ? assignedContractorsToProjects.MaxHoursAllocated : 0;
                                    SendMail(projectReference, maxHours.ToString(), totalHours.ToString(), userName);
                                }
                            }
                        }
                    }

                }

            }
        }


        public static void SendMail(int projectReference, string maxHrs, string totalBookedHrs, string resourceName)
        {

            // TimesheetAlert1.Visible = true;

            try
            {
                string projecref_withPrefix = string.Empty;
                string reciverEmail = string.Empty;
                string reciverName = string.Empty;

                SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select ProjectReferenceWithPrefix,OwnerName,OwnerEmail from V_ProjectDetails where ProjectReference = {0}", projectReference));
                while (dr.Read())
                {
                    projecref_withPrefix = dr["ProjectReferenceWithPrefix"].ToString();
                    reciverEmail = dr["OwnerEmail"].ToString();
                    reciverName = dr["OwnerName"].ToString();
                }
                dr.Close();
               
                maxHrs = maxHrs.Replace('.', ':');
                totalBookedHrs = totalBookedHrs.Replace('.', ':');
             
                string fromEmailId = Deffinity.systemdefaults.GetFromEmail();
                string subject = string.Format("Time Alert for Project {0}", projecref_withPrefix);
                Emailer em = new Emailer();
                string body = em.ReadFile("~/EmailTemplates/TimesheetAlert.htm");
                body = body.Replace("[mail_head]", subject);
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + "/MailLogo/deffinity_emailer_logo.png");
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + "/images/border.gif");
                body = body.Replace("[owner]", reciverName);
                body = body.Replace("[ResourceName]", resourceName);
                body = body.Replace("[BookedHours]", totalBookedHrs);
                body = body.Replace("[MaxHours]", maxHrs);

                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                em.SendingMail(fromEmailId, subject, body, reciverEmail);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }
    }
}