using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for DefaultEntries
/// </summary>

namespace Deffinity.ProgrammeEntitys
{

    public class Programme
    {
        private string _OperationsOwners, _EmailAddress, _Visible, _ResourcesRequired, _RisksAndIssues, _VisionStatement, _StrategicFitAlignment,
            _BenefitsToOrganisation, _Description, _Justification, _CostCenter;
        private int _ProgrammOwnerID, _TargetSLAPercentCompleted, _ID, _PortfolioID,_LevelID,_MasterProgrammeID;
        private double _MaximumBudget;
        private bool _Approve;
        private DateTime _ExpectedEndDate, _ExpectedStartDate, _DateofReview;
        
        
        public int ProgrammeID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int LevelID
        {
            get { return _MasterProgrammeID; }
            set { _MasterProgrammeID = value; }
        }
        public int MasterProgrammeID
        {
            get { return _LevelID; }
            set { _LevelID = value; }
        }
        public string OperationsOwners
        {
            get { return _OperationsOwners; }
            set { _OperationsOwners = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }
        public string ResourcesRequired
        {
            get { return _ResourcesRequired; }
            set { _ResourcesRequired = value; }
        }
        public string RisksAndIssues
        {
            get { return _RisksAndIssues; }
            set { _RisksAndIssues = value; }
        }
        public string VisionStatement
        {
            get { return _VisionStatement; }
            set { _VisionStatement = value; }
        }
        public string StrategicFitAlignment
        {
            get { return _StrategicFitAlignment; }
            set { _StrategicFitAlignment = value; }
        }
        public string BenefitsToOrganisation
        {
            get { return _BenefitsToOrganisation; }
            set { _BenefitsToOrganisation = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Justification
        {
            get { return _Justification; }
            set { _Justification = value; }
        }
        public string CostCenter
        {
            get { return _CostCenter; }
            set { _CostCenter = value; }
        }
        public int ProgrammOwnerID
        {
            get { return _ProgrammOwnerID; }
            set { _ProgrammOwnerID = value; }
        }
        public int TargetSLAPercentCompleted
        {
            get { return _TargetSLAPercentCompleted; }
            set { _TargetSLAPercentCompleted = value; }
        }
        public DateTime ExpectedEndDate
        {
            get { return _ExpectedEndDate; }
            set
            { _ExpectedEndDate = value; }
        }
        public DateTime ExpectedStartDate
        {
            get { return _ExpectedStartDate; }
            set { _ExpectedStartDate = value; }
        }
        public DateTime DateofReview
        {
            get { return _DateofReview; }
            set { _DateofReview = value; }
        }
        public double MaximumBudget
        {
            get { return _MaximumBudget; }
            set { _MaximumBudget = value; }
        }
        public bool Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }
        public int PortfolioID
        {
            get { return _PortfolioID; }
            set { _PortfolioID = value; }
        }

    }

    public class ProgrammeAssessmentCL
    {
        private string _ProgressToDate, _Benefits, _EmergentOpportunities, _PaceOfProgress;
        private int _ID, _programmeID,_PortfolioID;
        private DateTime _RaisedDate;
        private DateTime _datelogged;
        

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ProgrammeID
        {
            get { return _programmeID; }
            set { _programmeID = value; }
        }
        public int PortfolioID
        {
            get { return _PortfolioID; }
            set { _PortfolioID = value; }
        }
        public string ProgressToDate
        {
            get { return _ProgressToDate; }
            set { _ProgressToDate = value; }
        }
        public string Benefits
        {
            get { return _Benefits; }
            set { _Benefits = value; }
        }
        public string EmergentOpportunities
        {
            get { return _EmergentOpportunities; }
            set { _EmergentOpportunities = value; }
        }
        public string PaceOfProgress
        {
            get { return _PaceOfProgress; }
            set { _PaceOfProgress = value; }
        }
        public DateTime RaisedDate
        {
            get { return _RaisedDate; }
            set { _RaisedDate = value; }
        }
        public DateTime DateLogged
        {
            get { return _datelogged; }
            set { _datelogged = value; }
        }
        
    }

}