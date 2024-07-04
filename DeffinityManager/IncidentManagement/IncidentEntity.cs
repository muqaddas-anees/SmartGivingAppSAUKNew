using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// This is the Entity class for the database Entity Incident
    /// </summary>
    public class Incident:ICloneable 
    {

        #region Fields

        private int id = 0, portfolioID = 0, projectCategoryID = 0,ProjectCategoryMasterid=0, assignedTo = 0, requesterDepartmentID = 0, siteID = 0, contractorID = 0;
        private string categorySLA = string.Empty, sLATarget = string.Empty, incidentType = string.Empty, status = string.Empty, requesterName = string.Empty, requesterEmail = string.Empty, requesterTelephone = string.Empty, requesterDeskLocation = string.Empty;
        private string subject = string.Empty, details = string.Empty;
        private bool visible = true, sLAMet = false,inHandSLAMet = false;
        private int assignedToTeam=0;
        private DateTime dateLogged = Convert.ToDateTime("1/1/1900"), startTime = Convert.ToDateTime("1/1/1900"), endTime = Convert.ToDateTime("1/1/1900"), weekCommencingDate = Convert.ToDateTime("1/1/1900");
        private DateTime inHandTime = Convert.ToDateTime("1/1/1900");
        private DateTime closeTime = Convert.ToDateTime("1/1/1900");
        private string siteName = string.Empty;
        private string portfolioName = string.Empty;
        private string priorityLevel = string.Empty, priorityName = string.Empty, resolution = string.Empty;
        private int workDays = 0;
        private int workHours = 0;
        private int workMinutes = 0;
        private string assigneeName = string.Empty;
        private string case_Custom1 = string.Empty;
        private string case_Custom2 = string.Empty;
        private string case_Custom3 = string.Empty;
        private string case_Custom4 = string.Empty;
        private string case_Custom5 = string.Empty;
        private string case_Custom6 = string.Empty;
        private string case_Custom7 = string.Empty;
        private string case_Custom8 = string.Empty;
        private string case_Custom9 = string.Empty;
        private string case_Custom10 = string.Empty;
        private string case_Custom11 = string.Empty;
        private string case_Custom12 = string.Empty;
        private string case_Custom13 = string.Empty;
        private string case_Custom14 = string.Empty;
        private string case_Custom15 = string.Empty;
        private string case_Custom16 = string.Empty;
        private bool isOutOfHours = false;
        private bool isCallout = false;
        private int Project = 0;
        private int program = 0;
        private int area = 0;
        private int buildingID = 0;
        private int floorID = 0;
        private int quotestatus = 0, quotelinestatus = 0;
        private string ponumber = string.Empty;
        private string notes = string.Empty;
        private int loggedby = 0;
        private string loggedbyname = string.Empty;
        private string rag_sla = string.Empty;
        private string categoryName = string.Empty;
        private string subCategoryName = string.Empty;
        private string areaName = string.Empty;
        private string assignedToTeamName = string.Empty;
        #endregion

        #region Properties
        public int ProjectReference
        {
            get { return Project; }
            set { Project = value; }
        }

        public int Program
        {
            get { return program; }
            set { program = value; }
        }
        public int Area
        {
            get { return area; }
            set { area = value; }
        }

        public bool IsOutOfHours
        {
            get { return isOutOfHours; }
            set { isOutOfHours = value; }
        }


        public bool IsCallout
        {
            get { return isCallout; }
            set { isCallout = value; }
        }

        public string Case_Custom1
        {
            get { return case_Custom1; }
            set { case_Custom1 = value; }
        }

        public string Case_Custom2
        {
            get { return case_Custom2; }
            set { case_Custom2 = value; }
        }

        public string Case_Custom3
        {
            get { return case_Custom3; }
            set { case_Custom3 = value; }
        }

        public string Case_Custom4
        {
            get { return case_Custom4; }
            set { case_Custom4 = value; }
        }
        public string Case_Custom5
        {
            get { return case_Custom5; }
            set { case_Custom5 = value; }
        }

        public string Case_Custom6
        {
            get { return case_Custom6; }
            set { case_Custom6 = value; }
        }

        public string Case_Custom7
        {
            get { return case_Custom7; }
            set { case_Custom7 = value; }
        }

        public string Case_Custom8
        {
            get { return case_Custom8; }
            set { case_Custom8 = value; }
        }

        public string Case_Custom9
        {
            get { return case_Custom9; }
            set { case_Custom9 = value; }
        }

        public string Case_Custom10
        {
            get { return case_Custom10; }
            set { case_Custom10 = value; }
        }

        public string Case_Custom11
        {
            get { return case_Custom11; }
            set { case_Custom11 = value; }
        }

        public string Case_Custom12
        {
            get { return case_Custom12; }
            set { case_Custom12 = value; }
        }
        public string Case_Custom13
        {
            get { return case_Custom13; }
            set { case_Custom13 = value; }
        }

        public string Case_Custom14
        {
            get { return case_Custom14; }
            set { case_Custom14 = value; }
        }

        public string Case_Custom15
        {
            get { return case_Custom15; }
            set { case_Custom15 = value; }
        }

        public string Case_Custom16
        {
            get { return case_Custom16; }
            set { case_Custom16 = value; }
        }
        public string AssigneeName
        {
            get { return assigneeName; }
            set { assigneeName = value; }
        }

        public int WorkDays
        {
            get { return workDays; }
            set { workDays = value; }
        }

        public int QuoteStatus
        {
            get { return quotestatus; }
            set { quotestatus = value; }
        }

        public int QuoteLineStatus
        {
            get { return quotelinestatus; }
            set { quotelinestatus = value; }
        }

        public int WorkHours
        {
            get { return workHours; }
            set { workHours = value; }
        }

        public int WorkMinutes
        {
            get { return workMinutes; }
            set { workMinutes = value; }
        }

        /// <summary>
        /// Gets the work time that was entered manually.
        /// </summary>
        public TimeSpan TotalWorkTime
        {
            get
            {
                TimeSpan totalWorkTime = new TimeSpan(workDays, workHours, workMinutes, 0);
                return totalWorkTime;
            }
        }

        public string SiteName
        {
            get
            {
                if (siteName == null)
                    return string.Empty;
                return siteName;
            }
            set { siteName = value; }
        }

        public string PortfolioName
        {
            get { return portfolioName; }
            set { portfolioName = value; }
        }

        public string Subject
        {
            get
            {
                if (subject == null)
                    return string.Empty;
                return subject;
            }
            set { subject = value; }
        }

        public string Details
        {
            get
            {
                if (details == null)
                    return string.Empty;
                return details;
            }
            set { details = value; }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }
            set { visible = value; }
        }

        public bool SLAMet
        {
            get
            {
                return sLAMet;
            }
            set { sLAMet = value; }
        }
        
        public bool InHandSLAMet
        {
            get
            {
                return inHandSLAMet;
            }
            set { inHandSLAMet = value; }
        }
        public int AssignedToTeam
        {
            get
            {
                return assignedToTeam;
            }
            set { assignedToTeam = value; }
        }

        public int ContractorID
        {
            get
            {
                return contractorID;
            }
            set { contractorID = value; }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set { id = value; }
        }

        public int PortfolioID
        {
            get
            {
                return portfolioID;
            }
            set { portfolioID = value; }
        }

        public int ProjectCategoryID
        {
            get
            {
                return projectCategoryID;
            }
            set { projectCategoryID = value; }
        }
        public int ProjectCategoryMasterID
        {
            get { return ProjectCategoryMasterid; }
            set { ProjectCategoryMasterid = value; }
        }

        public int AssignedTo
        {
            get
            {
                return assignedTo;
            }
            set { assignedTo = value; }
        }

        public int RequesterDepartmentID
        {
            get
            {
                return requesterDepartmentID;
            }
            set { requesterDepartmentID = value; }
        }

        public int SiteID
        {
            get
            {
                return siteID;
            }
            set { siteID = value; }
        }

        public int FloorID
        {
            get
            {
                return floorID;
            }
            set { floorID = value; }
        }
        public int BuildingID
        {
            get
            {
                return buildingID;
            }
            set { buildingID = value; }
        }

        public string RequesterDeskLocation
        {
            get
            {
                if (requesterDeskLocation == null)
                    return string.Empty;
                return requesterDeskLocation;
            }
            set { requesterDeskLocation = value; }
        }

        public string RequesterTelephone
        {
            get
            {
                if (requesterTelephone == null)
                    return string.Empty;
                return requesterTelephone;
            }
            set { requesterTelephone = value; }
        }

        public string RequesterEmail
        {
            get
            {
                if (requesterEmail == null)
                    return string.Empty;
                return requesterEmail;
            }
            set { requesterEmail = value; }
        }

        public string RequesterName
        {
            get
            {
                if (requesterName == null)
                    return string.Empty;
                return requesterName;
            }
            set { requesterName = value; }
        }

        public string Status
        {
            get
            {
                if (status == null)
                    return "Open";
                return status;
            }
            set { status = value; }
        }

        public string IncidentType
        {
            get
            {
                if (incidentType == null)
                    return string.Empty;
                return incidentType;
            }
            set { incidentType = value; }
        }

        public string SLATarget
        {
            get
            {
                if (sLATarget == null)
                    return string.Empty;
                return sLATarget;
            }
            set { sLATarget = value; }
        }

        public string CategorySLA
        {
            get
            {
                if (categorySLA == null)
                    return string.Empty;
                return categorySLA;
            }
            set { categorySLA = value; }
        }

        public string PriorityLevel
        {
            get
            {
                if (priorityLevel == null)
                    return string.Empty;
                return priorityLevel;
            }
            set { priorityLevel = value; }
        }
        public string PriorityName
        {
            get
            {
                if (priorityName == null)
                    return string.Empty;
                return priorityName;
            }
            set { priorityName = value; }
        }
        
        public string Resolution
        {
            get
            {
                if (resolution == null)
                    return string.Empty;
                return resolution;
            }
            set { resolution = value; }
        }

        public string POnumber
        {
            get
            {
                return ponumber;
            }
            set { ponumber = value; }
        }
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        public string LoggedByName
        {
            get { return loggedbyname; }
            set { loggedbyname = value; }
        }
        public int LoggedBy
        {
            get { return loggedby; }
            set { loggedby = value; }
        }
        /// <summary>
        /// Gets the upcoming monday from the startdate.
        /// </summary>
        public DateTime WeekCommencingDate
        {
            //Returns the upcoming monday
            get
            {
                DateTime currentTime = new DateTime();
                int dayOfWeek = 0;//0 is sunday, 1 is monday and so on..
                currentTime = (startTime == null) ? DateTime.Now : startTime;
                dayOfWeek = (int)currentTime.DayOfWeek;
                DateTime NextMonday = currentTime.AddDays((dayOfWeek < 1) ? 1 : (8 - dayOfWeek));
                return NextMonday;
            }
            set { weekCommencingDate = value; }
        }


        /// <summary>
        /// Gets the time that the task is beginning.  Also called as Scheduled Time.
        /// </summary>
        public DateTime InHandTime
        {
            get {
                return inHandTime; }
            set { inHandTime = value; }
        }

        public DateTime CloseTime
        {
            get {
                return closeTime;
            }
            set { closeTime = value; }
        }
        /// <summary>
        /// Gets the time between the new call and the call went to in-hand. 
        /// </summary>
        public TimeSpan TimeForNewCallAndInHand
        {
            get
            {
                TimeSpan timeTaken = startTime - inHandTime;
                return timeTaken;
            }
        }

        /// <summary>
        /// Gets the time between the in hand and the completed.
        /// </summary>
        public TimeSpan TimeForInHandAndCompleted
        {
            get
            {
                TimeSpan timeTaken = inHandTime - endTime;
                return timeTaken;
            }
        }

        /// <summary>
        /// Gets the total time for processing the call.
        /// </summary>
        public TimeSpan TotalTimeForTheCall
        {
            get
            {
                TimeSpan timeTaken = TimeForInHandAndCompleted + TimeForNewCallAndInHand;
                return timeTaken;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set { endTime = value; }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set { startTime = value; }
        }

        public DateTime DateLogged
        {
            get
            {
                return dateLogged;
            }
            set { dateLogged = value; }
        }
         public string RAG_SLA
        {
            get
            {
                return rag_sla;
            }
            set { rag_sla = value; }
        }
         public string CategoryName
         {
             get
             {
                 return categoryName;
             }
             set { categoryName = value; }
         }
         public string SubCategoryName
         {
             get
             {
                 return subCategoryName;
             }
             set { subCategoryName = value; }
         }
         public string AreaName
         {
             get
             {
                 return areaName;
             }
             set { areaName = value; }
         }
         public string AssignedToTeamName
         {
             get
             {
                 return assignedToTeamName;
             }
             set { assignedToTeamName = value; }
         }
        #endregion

        public Incident()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region ICloneable Members

        public object Clone()
        {
            Incident incident = new Incident();
            incident.AssignedTo = this.AssignedTo;
            incident.AssignedToTeam = this.AssignedToTeam;
            incident.AssigneeName = this.AssigneeName;
            incident.Case_Custom1 = this.Case_Custom1;
            incident.Case_Custom2 = this.Case_Custom2;
            incident.Case_Custom3 = this.Case_Custom3;
            incident.Case_Custom4 = this.Case_Custom4;
            incident.Case_Custom5 = this.Case_Custom5;
            incident.Case_Custom6 = this.Case_Custom6;
            incident.Case_Custom7 = this.Case_Custom7;
            incident.Case_Custom8 = this.Case_Custom8;
            incident.Case_Custom9 = this.Case_Custom9;
            incident.Case_Custom10 = this.Case_Custom10;
            incident.Case_Custom11 = this.Case_Custom11;
            incident.Case_Custom12 = this.Case_Custom12;
            incident.Case_Custom13 = this.Case_Custom13;
            incident.Case_Custom14 = this.Case_Custom14;
            incident.Case_Custom15 = this.Case_Custom15;
            incident.Case_Custom16 = this.Case_Custom16;
            incident.CategorySLA = this.CategorySLA;
            incident.ContractorID = this.ContractorID;
            incident.DateLogged = this.DateLogged;
            incident.Details = this.Details;
            incident.EndTime = this.EndTime;
            incident.ID = this.ID;
            incident.IncidentType = this.IncidentType;
            incident.InHandTime = this.InHandTime;
            incident.PortfolioID = this.PortfolioID;
            incident.PortfolioName = this.PortfolioName;
            incident.PriorityLevel = this.PriorityLevel;
            incident.PriorityName = this.priorityName;            
            incident.ProjectCategoryID = this.ProjectCategoryID;
            incident.RequesterDepartmentID = this.RequesterDepartmentID;
            incident.RequesterDeskLocation = this.RequesterDeskLocation;
            incident.RequesterEmail = this.RequesterEmail;
            incident.RequesterName = this.RequesterName;
            incident.RequesterTelephone = this.RequesterTelephone;
            incident.Resolution = this.Resolution;
            incident.SiteID = this.SiteID;
            incident.SiteName = this.SiteName;
            incident.SLAMet = this.SLAMet;
            incident.SLATarget = this.SLATarget;
            incident.StartTime = this.StartTime;
            incident.Status = this.Status;
            incident.Subject = this.Subject;
            incident.WorkDays = this.WorkDays;
            incident.WorkHours = this.WorkHours;
            incident.WorkMinutes = this.WorkMinutes;
            incident.ProjectReference = this.ProjectReference;
            incident.POnumber = this.POnumber;
            incident.QuoteStatus = this.QuoteStatus;
            incident.QuoteLineStatus = this.QuoteLineStatus;
            incident.ProjectCategoryMasterID = this.ProjectCategoryMasterid;
            incident.Area = this.area;
            incident.IsCallout = this.isCallout;
            incident.Notes = this.notes;
            incident.CloseTime = this.closeTime;
            incident.BuildingID = this.buildingID;
            incident.FloorID = this.floorID;
            incident.LoggedBy = this.LoggedBy;
            incident.LoggedByName = this.LoggedByName;
            incident.RAG_SLA = this.RAG_SLA;
            incident.CategoryName = this.CategoryName;
            incident.SubCategoryName = this.SubCategoryName;
            incident.AreaName = this.AreaName;
            incident.AssignedToTeamName = this.AssignedToTeamName;
            return incident;
        }

        #endregion
    }
    
    /// <summary>
    /// The collection of the incidents
    /// </summary>
    public class IncidentCollection : List<Incident>
    {

    }
}