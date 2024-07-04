using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Summary description for ChangeControlEntity
    /// </summary>
    public class Change
    {

        #region Fields

        int id = 0;
        int incidentID = 0;
        string title = string.Empty;
        string changeDescription = string.Empty;
        string justification = string.Empty;
        DateTime dateRaised;
        DateTime targetReleaseDate;
        string estimatedDaysRequired;
        DateTime targetStartDate;
        DateTime dateLogged;
        DateTime inhanddate;
        DateTime closeddate;
        string status;
        int categoryID;
        int requesterID;
        int coOrdinator;
        int subCategoryID;
        string subCategoryName;
       
       
       
        //Target Start Date
        string estimatedCost;
        int relatesToservicerequest;
        int relatesToProjectRef;
        int raisedBy;
        string resourceImpact = string.Empty;
        string requesterName = string.Empty;
        string requesterEmailID = string.Empty;
        int portfolioID = 0;
        string customer;
        int areaID = 0; int priorityID = 0;
        string areaName = string.Empty;
        string priorityName = string.Empty;

      

        #endregion

        #region Public Properties
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime TargetStartDate
        {
            get { return targetStartDate; }
            set { targetStartDate = value; }
        }
        public string EstimatedDaysRequired
        {
            get { return estimatedDaysRequired; }
            set { estimatedDaysRequired = value; }
        }
        public int RaisedBy
        {
            get { return raisedBy; }
            set { raisedBy = value; }
        }
        public int RelatesToProjectRef
        {
            get { return relatesToProjectRef; }
            set { relatesToProjectRef = value; }
        }
        public int RelatesToservicerequest
        {
            get { return relatesToservicerequest; }
            set { relatesToservicerequest = value; }
        }
     
        public string EstimatedCost
        {
            get { return estimatedCost; }
            set { estimatedCost = value; }
        }
      
        public int PortfolioID
        {
            get { return portfolioID; }
            set { portfolioID = value; }
        }

        public string RequesterName
        {
            get { return requesterName; }
            set { requesterName = value; }
        }

        public string RequesterEmailID
        {
            get { return requesterEmailID; }
            set { requesterEmailID = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IncidentID
        {
            get { return incidentID; }
            set { incidentID = value; }
        }


        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string ChangeDescription
        {
            get { return changeDescription; }
            set { changeDescription = value; }
        }

        public string Justification
        {
            get { return justification; }
            set { justification = value; }
        }

        public DateTime DateRaised
        {
            get { return dateRaised; }
            set { dateRaised = value; }
        }


        public DateTime TargetReleaseDate
        {
            get { return targetReleaseDate; }
            set { targetReleaseDate = value; }
        }


        public string ResourceImpact
        {
            get { return resourceImpact; }
            set { resourceImpact = value; }
        }

        public int RequesterID
        {
            get { return requesterID; }
            set { requesterID = value; }
        }
        public int CoOrdinator
        {
            get { return coOrdinator; }
            set { coOrdinator = value; }
        }
        public int SubCategoryID
        {
            get { return subCategoryID; }
            set { subCategoryID = value; }
        }
        public DateTime DateLogged
        {
            get { return dateLogged; }
            set { dateLogged = value; }
        }
        public DateTime InhandDate
        {
            get { return inhanddate; }
            set { inhanddate = value; }
        }
        public DateTime ClosedDate
        {
            get { return closeddate; }
            set { closeddate = value; }
        }
        public string SubCategoryName
        {
            get { return subCategoryName; }
            set { subCategoryName = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public int AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }
        public int PriorityID
        {
            get { return priorityID; }
            set { priorityID = value; }
        }
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public string PriorityName
        {
            get { return priorityName; }
            set { priorityName = value; }
        }
        int siteID =0;
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        string siteName=string.Empty;
        public string SiteName
        {
            get { return siteName; }
            set { siteName = value; }
        }
        #endregion

        public Change()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class ChangeCollection : List<Change>
    { 
        
    }
}