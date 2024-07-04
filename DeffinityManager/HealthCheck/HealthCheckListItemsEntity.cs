using System;
using System.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace Health.Entity
{
    public class HealthCheckListItems
    {
        int id = 0;
        int healthCheckListID = 0;
        string healthCheck = string.Empty;
        SqlBoolean isChecked = SqlBoolean.Null;
        string issues = string.Empty;
        string notes = string.Empty;
        int assignedTeam = 0;
        string assignedTeamName = string.Empty;
        int assignee = 0;
        string assigneeName = string.Empty;
        string status = string.Empty;
        SqlDateTime dateCompleted;
        DateTime duedate = Convert.ToDateTime("1/1/1990");
        SqlDateTime issueDate = Convert.ToDateTime("1/1/1990");
        string rag = string.Empty;
        int assignedMember = 0;
        string assignedMemberName = string.Empty;
        public SqlDateTime IssueDate
        {
            get { return issueDate; }
            set { issueDate = value; }
        }


        public SqlDateTime DateCompleted
        {
            get { return dateCompleted; }
            set { dateCompleted = value; }
        }
        public DateTime DueDate
        {
            get { return duedate; }
            set { duedate = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Assignee
        {
            get { return assignee; }
            set { assignee = value; }
        }

        public string AssigneeName
        {
            get { return assigneeName; }
            set { assigneeName = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public int HealthCheckListID
        {
            get { return healthCheckListID; }
            set { healthCheckListID = value; }
        }
        
        public string HealthCheck
        {
            get { return healthCheck; }
            set { healthCheck = value; }
        }
        
        public SqlBoolean IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
        
        public string Issues
        {
            get { return issues; }
            set { issues = value; }
        }
        
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        
        public int AssignedTeam
        {
            get { return assignedTeam; }
            set { assignedTeam = value; }
        }
        

        public string AssignedTeamName
        {
            get { return assignedTeamName; }
            set { assignedTeamName = value; }
        }
        public string RAG
        {
            get { return rag; }
            set { rag = value; }
        }
        
        public int AssignedMember
        {
            get { return assignedMember; }
            set { assignedMember = value; }
        }
        
        public string AssignedMemberName
        {
            get { return assignedMemberName; }
            set { assignedMemberName = value; }
        }
    }

    public class HealthCheckListItemsCollection : List<HealthCheckListItems>
    { 
        
    }

    #region Health Check Recurences

    public class HealthCheckRecurr
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecurWeekOn { get; set; }
        public String WeekDayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EndAfter { get; set; }
        public int ReCurrencePattern { get; set; }
        public int ReCurrenceRange { get; set; }
        public int HealthCheckID { get; set; }

        public string Duration { get; set; }
    }
    #endregion
}