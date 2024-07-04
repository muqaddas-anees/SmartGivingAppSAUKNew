using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeffinityManager.DAL.DBForProjectDeviationTableAdapters;
using DeffinityManager;
using DeffinityManager.DAL;


/// <summary>
/// Summary description for Project
/// </summary>
/// 
namespace Deffinity.GlobalIssues
{

    public enum DeviationSection
    {
        Project = 1,
        Global = 2,
        Healthcheck = 3

    }

    public class ProjectDeviation
    {
        public ProjectDeviation()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        #region "Deviation Documents"
        

        private static DeviationDocsTableAdapter _DeviationDocsTableAdapter;        
        public static DeviationDocsTableAdapter DeviationDocsAdapter
        {
            get
            {
                if (_DeviationDocsTableAdapter == null)
                    _DeviationDocsTableAdapter = new DeviationDocsTableAdapter();
                return _DeviationDocsTableAdapter;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtDeviationDocumentsDataTable GetDataDeviationDocument(int DiviationID)
        {

            return DeviationDocsAdapter.GetDataDeviationDocument(DiviationID);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int   DeviationDocument_Insert(string documentname,int DiviationID, DateTime createdate,string Extension )
        {

            int documentID = 0;

            DBForProjectDeviation.DtDeviationDocumentsDataTable DtDeviationDocumentsDataTable=DeviationDocsAdapter.GetDataDeviationDocumentInsert(documentname, DiviationID, createdate, Extension);
            if (DtDeviationDocumentsDataTable.Rows.Count > 0)
            {
                DBForProjectDeviation.DtDeviationDocumentsRow DtDeviationDocumentsRow = DtDeviationDocumentsDataTable[0];                
                documentID = DtDeviationDocumentsRow.DocId;
            }
            return documentID;

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int DeviationDocument_Update(int docId,string documentname, int DiviationID, DateTime createdate, string Extension)
        {

            return  DeviationDocsAdapter.Update(docId,documentname, DiviationID, createdate, Extension);

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int DeviationDocument_Delete(int docId)       
        {
            return DeviationDocsAdapter.Delete(docId);

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtDeviationDocumentsDataTable GetDataDeviationDeleteDocuments(int Id)
        {
            return DeviationDocsAdapter.GetDataDeviationDeleteDocuments(Id);
        }




        #endregion  "Deviation Documents"

        #region "Deviation Health Check"


        private static DeviationHealthChecksTableAdapter _DeviationHealthChecksTableAdapter;


        public static DeviationHealthChecksTableAdapter DeviationHealthChecksAdapter
        {
            get
            {
                if (_DeviationHealthChecksTableAdapter == null)
                    _DeviationHealthChecksTableAdapter = new DeviationHealthChecksTableAdapter();
                return _DeviationHealthChecksTableAdapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtHealthChecksDataTable GetDataHealthChecks()
        {

            return DeviationHealthChecksAdapter.GetDataHealthChecks();

        }

        #endregion "Deviation  Health Check"

        #region "Deviation Projects"


        private static DeviationProjectsTableAdapter _DeviationProjectsTableAdapter;

        
        public static DeviationProjectsTableAdapter DeviationProjectsAdapter
        {
            get
            {
                if (_DeviationProjectsTableAdapter == null)
                    _DeviationProjectsTableAdapter = new DeviationProjectsTableAdapter();
                return _DeviationProjectsTableAdapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtDeviationProjectsDataTable GetDataProjects()
        {
            return DeviationProjectsAdapter.GetDataProjects();

        }

        #endregion "Deviation Projects"

        #region "Deviation Projects"
        private static DeviationTableAdapter _DeviationAdapter;

       
        public static DeviationTableAdapter DeviationAdapter
        {
            get
            {
                if (_DeviationAdapter == null)
                    _DeviationAdapter = new DeviationTableAdapter();
                return _DeviationAdapter;
            }
        }

        public object UpdateStatus(int IssueID)
        {
            object objInsert = null;
            try
            {
                DeviationAdapter.MarkAsComplete(IssueID);
                objInsert = 1;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return objInsert;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtDeviationDataTable GetDeviationSelect(int ID, int IssueType, string IssueSection,int AssignTo,int Status,string Issue)
        {
            
            return DeviationAdapter.GetDeviationSelect(ID, IssueType, IssueSection,AssignTo,Status,Issue);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForProjectDeviation.DtDeviationDataTable GetDataDeviationByID(int ID)
        {                                                 
            return DeviationAdapter.GetDataDeviationByID(ID);

        }

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeviationAndDocumentsDelete(int ID)
        {

            int intRetValue=0;
            try
            {
                DeviationAdapter.DNDeviationDelete(ID);
                intRetValue = 1;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return intRetValue;           
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int GetDataDeviationInsert
                    (int Projectreference,
                    DateTime DateRaised,
                    string InternalRef,
                    int Customer,
                    int Site,
                    int Loggedby,
                    int Reportedby,
                    string Associated,
                    string AffectedArea,
                    int Severity,
                    int PlanType,
                    string Descriptiontxt,
                    string DetailsDeviation,
                    string Impactbusiness,
                    int Assignedto,
                    DateTime SendReminderDate,
                    int Reminderto,
                    string IssueSection,
                    int IssueType,int status)
        {
            DBForProjectDeviation.DtDeviationDataTable DtDeviationDataTable = DeviationAdapter.GetDataDeviationInsert
            (Projectreference,
            DateRaised,
            InternalRef,
            Customer,
            Site, Loggedby,
            Reportedby,
            Associated,
            AffectedArea,
            Severity,
            PlanType,
            Descriptiontxt,
            DetailsDeviation,
            Impactbusiness,
            Assignedto,
            SendReminderDate,
            Reminderto,
            IssueSection,
            IssueType, 
            status 
            );



            int ID = 0;

            if (DtDeviationDataTable.Rows.Count > 0)
            {
                DBForProjectDeviation.DtDeviationRow DtDeviationRow = DtDeviationDataTable[0];                
                ID = DtDeviationRow.ID;
            }
            
            return ID;
            

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public object DeviationUpdate
        (
                int ID,
                    string InternalRef,
                    int Customer,
                    int Site,
                    int Loggedby,
                    int Reportedby,
                    string Associated,
                    string AffectedArea,
                    int Severity,
                    int PlanType,
                    string Descriptiontxt,
                    string DetailsDeviation,
                    string Impactbusiness,
                    int Assignedto,
                    DateTime SendReminderDate,
                    int Reminderto,
                    string IssueSection,
                    int IssueType,
                    int status
            )
        {


            object retObject = DeviationAdapter.DeviationUpdate
                   (ID,
                   InternalRef,
                   Customer,
                   Site,
                   Loggedby,
                   Reportedby,
                   Associated,
                   AffectedArea,
                   Severity,
                   PlanType,
                   Descriptiontxt,
                   DetailsDeviation,
                   Impactbusiness,
                   Assignedto,
                   SendReminderDate,
                   Reminderto,
                   IssueSection,
                   IssueType,
                   status);

            return retObject;

        }


        #endregion "Deviation Details"
    }
}   

