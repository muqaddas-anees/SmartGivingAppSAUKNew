using System;
using System.Data;
/// <summary>
/// Summary description for LessonsLearntEntity
/// </summary>
namespace Deffinity.LessonsLearntEntitys
{
    public class LessonsLearntEntity
    {
        int _ID; int _ProjectReference;
        string _Description, _RemediationActions, _BusinessImpact;
        int _IdentifiedBy, _AssignedTo, _Status;
        DateTime _DateRaised, _DateInProgress, _DateCompleted;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ProjectReference
        {
            get { return _ProjectReference; }
            set { _ProjectReference = value; }
        }
        public int IdentifiedBy
        {
            get { return _IdentifiedBy; }
            set { _IdentifiedBy = value; }
        }
        public int AssignedTo
        {
            get { return _AssignedTo; }
            set { _AssignedTo = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string RemediationActions
        {
            get { return _RemediationActions; }
            set { _RemediationActions = value; }
        }
        public string BusinessImpact
        {
            get { return _BusinessImpact; }
            set { _BusinessImpact = value; }
        }
        public DateTime DateRaised
        {
            get { return _DateRaised; }
            set { _DateRaised = value; }
        }
        public DateTime DateInProgress
        {
            get { return _DateInProgress; }
            set { _DateInProgress = value; }
        }
        public DateTime DateCompleted
        {
            get { return _DateCompleted; }
            set { _DateCompleted = value; }
        }
        
        public LessonsLearntEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    }
}
