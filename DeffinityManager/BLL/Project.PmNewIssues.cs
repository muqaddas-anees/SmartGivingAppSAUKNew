using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeffinityManager.DAL.PmNewIssuesTableAdapters;
using DeffinityManager.DAL;

/// <summary>
/// Summary description for Project
/// </summary>
/// 
namespace Deffinity.GlobalIssues
{
    
    public enum IssueSection
    {
        Project = 1,
        Global = 2,
	Healthcheck =3

    }
   
    public class Project
    {
        public static string IssueTypeDefault = "Health Check";
        public Project()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        string _IssueSection = string.Empty;
        public string IssueSection
        {
            get { return _IssueSection; }
            set { _IssueSection = value; }
        }
        private static adaIssues _AdaIssue;

        public static adaIssues AdaIssue
        {
            get
            {
                if (_AdaIssue == null)
                    _AdaIssue = new adaIssues();
                return _AdaIssue;

            }
        }

        public object IssueInsert(int projectRefID, int taskID, string issues, int assignTo, DateTime sheduledDate,
            string notes, int status, int issueType, string expectedOutCome, int raisedBy, int checkedBy, DateTime dateChecked,
            int issueraisedBy, int location, DateTime DateCompleted, string IssueSection, DateTime DateNextReviewDate, int EnableCust, int RAGStatus,string ActionPlane)
        {
            object objInsert = null;
            try
            {
                //objInsert = AdaIssue.Insert_Issues(projectRefID, taskID, issues, assignTo, sheduledDate, notes, status, issueType, expectedOutCome, raisedBy, checkedBy, dateChecked, issueraisedBy, location, DateCompleted);
                objInsert = AdaIssue.Insert_Issues(projectRefID, taskID, issues, assignTo, sheduledDate, notes, status, issueType, expectedOutCome, raisedBy, checkedBy, dateChecked, issueraisedBy, location, DateCompleted, IssueSection,DateNextReviewDate,
                    EnableCust, RAGStatus,ActionPlane);
                return objInsert;


                //PmNewIssues.DtIssueDataTable IssueDatas = new PmNewIssues.DtIssueDataTable ();
                //PmNewIssues.DtIssueRow IssueDataRow = IssueDatas.NewDtIssueRow();
                //IssueDataRow.ProjectReference = projectRefID;

                //rowsAffected = AdaIssue.Update_DN_Issue(IssueDatas);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
            
            return objInsert;

        }
        public PmNewIssues.DtIssueDataTable SelectionWithID(int ID)
        {
            return AdaIssue.SelectIssueByID(ID);
        }
        public int IssueAndDocumentsDelete(int ID)
        {
            
            int intRetVal = 0;
            try
            {
                AdaIssue.DNIssueDelete(ID);
                intRetVal = 1;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }            
            return intRetVal;
        }

        
        public object UpdateStatus(int IssueID)
        {
            object objInsert = null;
            try
            {
                AdaIssue.MarkAsComplete(IssueID);
                objInsert = 1;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return objInsert;
        }
        public object IssueUpdate(int IssueID, string issues, int assignTo, DateTime sheduledDate,
           string notes, int status, int issueType, string expectedOutCome, DateTime DateCompleted,
            int IssueraisedBy, DateTime NextReviewDate, int EnableCust, int RAGStatus,string ActionPlan)
        {
            object objInsert = null;
            try
            {
                AdaIssue.Update_DN_Issue(IssueID, issues, assignTo, sheduledDate, notes, status,
                    issueType, expectedOutCome, DateCompleted, IssueraisedBy, NextReviewDate, EnableCust, RAGStatus,ActionPlan);
                objInsert = "1";
                return objInsert;


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
            
            return objInsert;

        }

        private static adaProjectDocuments _AdaDocument;

        public static adaProjectDocuments AdaDocument
        {
            get
            {
                if (_AdaDocument == null)
                    _AdaDocument = new adaProjectDocuments();
                return _AdaDocument;

            }
        }

        public PmNewIssues.dtProjectDocumentsDataTable GetDataIssueDeleteDocuments(int ID)
        {
            return AdaDocument.GetDataIssueDeleteDocuments(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public PmNewIssues.dtProjectDocumentsDataTable IssueDocument_Delete(int Id)
        {
            return AdaDocument.GetDataIssueDeleteDocuments(Id);            
        }


        public PmNewIssues.dtProjectDocumentsDataTable DocumentsByID(int ID)
        {
            return AdaDocument.GetData(ID);
        }
        public void DocumentDelete(int DocID)
        {
            AdaDocument.DeleteProject_Document(DocID);
        }
        public object InsertDocument(string DocumentName, DateTime createDate, int IssueID, string Extension)
        {
            object objInsert = null;
            try
            {


                objInsert = AdaDocument.Insert_ProjectDocument(DocumentName, IssueID, createDate, Extension);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
            return objInsert;
        }

        public object UpdateDocument(int DocId, string DocumentName, DateTime createDate, int IssueID, string Extension)
        {
            object objUpdate = null;
            try
            {


                AdaDocument.Update_Project_Document(DocId, DocumentName, IssueID, createDate, Extension);
                objUpdate = 1;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
            return objUpdate;
        }


    }
}