using System;
using System.Collections.Generic;

/// <summary>
/// The class to manage the HealthCheck List items that are related to portfolio.
/// These are the task to assign for individuals.
/// </summary>
/// 
namespace Health.Entity
{
    
    public class HealthCheckList
    {
        int id = 0;
        int healthCheckListID = 0;
        string healthCheckTitle = string.Empty;
        DateTime dateRaised = Convert.ToDateTime("01/01/1900");
        int assignedTeam = 0;
        string assignedTeamName = string.Empty;
        string status = string.Empty;
        string notes = string.Empty;
        string issue = string.Empty;
        int locationID = 0;
        string locationName = string.Empty;
        string issueStatus = string.Empty;
        DateTime duedate;
        string rag = string.Empty;
        int assignmember = 0;
        int portfolioID = 0;
        string portfolioName = string.Empty;
        bool isChecklist = false;
        public string IssueStatus
        {
            get { return issueStatus; }
            set { issueStatus = value; }
        }
       
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

        public int LocationID
        {
            get { return locationID; }
            set { locationID = value; }
        }

        public DateTime DueDate
        {
            get { return duedate; }
            set { duedate = value; }
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


        public string HealthCheckTitle
        {
            get { return healthCheckTitle; }
            set { healthCheckTitle = value; }
        }
       

        public DateTime DateRaised
        {
            get { return dateRaised; }
            set { dateRaised = value; }
        }

       

        public int AssignedTeam
        {
            get { return assignedTeam; }
            set { assignedTeam = value; }
        }
        public int Assignmember
        {
            get { return assignmember; }
            set { assignmember = value; }
        }
        

        public string AssignedTeamName
        {
            get { return assignedTeamName; }
            set { assignedTeamName = value; }
        }

        

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

       

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
       

        public string Issue
        {
            get { return issue; }
            set { issue = value; }
        }
        public string RAG
        {
            get { return rag; }
            set { rag = value; }
        }
        public string PortfolioName
        {
            get { return portfolioName; }
            set { portfolioName = value; }
        }
          public int PortfolioID
        {
            get { return portfolioID; }
            set { portfolioID = value; }
        }
        public bool iSChecklist
          {
              get { return isChecklist; }
              set { isChecklist = value; }
          }
        
    }

    public class HealthCheckListCollection : List<HealthCheckList>
    { 
        
    }
}