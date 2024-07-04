using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AC2PDocumentsUploadTableAdapters;
using DeffinityManager.DAL.AC2PDocumentsUploadTableAdapters;
using DeffinityManager.DAL;

//using AC2PDocumentsTableAdapters;

/// <summary>
/// Summary description for AC2P_DocumentsController
/// </summary>
///

namespace Deffinity.BLL
{
//public class AC2P_DocumentsController
//{
//    //public AC2P_DocumentsController()
//    //{
		
//    //}

//}
    public class AC2P_DocumentsController
    {
        private static AC2PDocumentsTableAdapter _AC2PDocumentsAdapter;
        private static DocumentJournalGetTableAdapter _DocumentJournalAdapter;
        //private static DocumentJournalAdapter _DocumentJournalAdapter;

        #region "Documents Journal Details"
        public static DocumentJournalGetTableAdapter JournalAdapter
        {
            get
            {
                if (_DocumentJournalAdapter == null)
                    _DocumentJournalAdapter = new DocumentJournalGetTableAdapter();
                return _DocumentJournalAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AC2PDocumentsUpload.DocumentJournalAdapterDataTable GetDocumentJournal(int DocumentID,int CustomerID)
        {
                   
            return JournalAdapter.GetDocumentJournal(DocumentID, CustomerID);

        }
        public int DocumentJournalInsert(int DocumentID, int CustomerID, int UserID)
        {

            JournalAdapter.DocumentJournalInsert(DocumentID, CustomerID, UserID);

            int rowsAffected;
            return rowsAffected = 1;
        }
        #endregion "Documents Journal Details"
        #region "AC2PDocuments Details"

        public static AC2PDocumentsTableAdapter AC2PDocumentsAdapter
        {
            get
            {
                if (_AC2PDocumentsAdapter == null)
                    _AC2PDocumentsAdapter = new AC2PDocumentsTableAdapter();
                return _AC2PDocumentsAdapter;

            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AC2PDocumentsUpload.AC2PDocumentsDataTableDataTable GetAC2PDocuments()
        {

            return AC2PDocumentsAdapter.GetAC2PDocuments();

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public string GetAC2PGetDocumentName(int id)
        {            
            AC2PDocumentsUpload.AC2PDocumentsDataTableDataTable  GetAC2PGetDocumentName=AC2PDocumentsAdapter.GetAC2PGetDocumentName(id);
            AC2PDocumentsUpload.AC2PDocumentsDataTableRow AC2PDocumentsDataTableRow = GetAC2PGetDocumentName[0];
            return AC2PDocumentsDataTableRow.DocumentName;

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int AC2PDocumentsInsert(int DocumentID, int MasterID, string CopyStatus)
        {
            //employee.EmployeeDatatableDataTable EmployeeDatas = new employee.EmployeeDatatableDataTable();
            //employee.EmployeeDatatableRow EmployeeDataRow = EmployeeDatas.NewEmployeeDatatableRow();
            //EmployeeDataRow.fname = fname;
            //EmployeeDataRow.lname = lname;
            //EmployeeDataRow.address = address;
            //EmployeeDataRow.mobile = mobile;
            //EmployeeDatas.AddEmployeeDatatableRow(EmployeeDataRow);
            //int rowsAffected = EmployeeAdapter.Update(EmployeeDatas);
            AC2PDocumentsAdapter.Insert(DocumentID, MasterID, CopyStatus);

            int rowsAffected;
            return rowsAffected = 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int AC2PDocumentsDelete(string DocumentIDS)
        {
            //employee.EmployeeDatatableDataTable EmployeeDatas = new employee.EmployeeDatatableDataTable();
            //employee.EmployeeDatatableRow EmployeeDataRow = EmployeeDatas.NewEmployeeDatatableRow();
            //EmployeeDataRow.fname = fname;
            //EmployeeDataRow.lname = lname;
            //EmployeeDataRow.address = address;
            //EmployeeDataRow.mobile = mobile;
            //EmployeeDatas.AddEmployeeDatatableRow(EmployeeDataRow);
            //int rowsAffected = EmployeeAdapter.Update(EmployeeDatas);
            return AC2PDocumentsAdapter.Delete(DocumentIDS);
        }

        //CheckInOutUpdate

        public int DN_ProjectUploadInsertNew(string ProjectReference,string DocumentName,byte[]Document,string SourceFileName,string ContentType,string ApplicationSection,int DataSize,int MasterID,int ContractorID,int SDID )
        {
                int rowsAffected=0;
                AC2PDocumentsAdapter.DNProjectUploadInsertNew(ProjectReference, DocumentName, Document, SourceFileName, ContentType, ApplicationSection, DataSize, MasterID, ContractorID, SDID);
             return rowsAffected;
        }

        public int AC2PDocumentsCheckOut(string DocumentIDS, bool CheckOut, int CheckOutUserID)
        {
            int rowsAffected = 0;
           object retObject=AC2PDocumentsAdapter.AC2PDocumentsCheckOutIn(DocumentIDS, CheckOut, CheckOutUserID);
           
           //rowsAffected = (int)retObject;
            return rowsAffected;
        }
        


        //public void DocumentsUpload()
        //{
        //    //if (context.Request.Files.Count > 0)
        //    //{
        //    //    for (int j = 0; j < context.Request.Files.Count; j++)
        //    //    {
        //    //        HttpPostedFile uploadFile = context.Request.Files[j];
        //    //        if (context.Request.Files.Count > 0)
        //    //        {
        //    SqlConnection myConnection = new SqlConnection(connectionString.retrieveConnString());
        //    using (myConnection)
        //    {
        //        SqlCommand myCommand = new SqlCommand();
        //        myCommand = new SqlCommand("DN_ProjectUploadInsertNew", myConnection);
        //        using (myCommand)
        //        {
        //            myCommand.CommandType = CommandType.StoredProcedure;
        //            myCommand.Parameters.Add(new SqlParameter("@DocumentName", "uploadFile.FileName"));
        //            myCommand.Parameters.Add(new SqlParameter("@SourceFileName", "uploadFile.FileName"));
        //            myCommand.Parameters.Add(new SqlParameter("@ContentType", "uploadFile.ContentType"));
        //            myCommand.Parameters.Add(new SqlParameter("@DataSize", "uploadFile.ContentLength"));
        //            myCommand.Parameters.Add(new SqlParameter("@ProjectReference", "context.Request.QueryString[project]"));
        //            myCommand.Parameters.Add(new SqlParameter("@ApplicationSection", "P"));
        //            myCommand.Parameters.Add(new SqlParameter("@MasterID", "context.Request.QueryString[folderID]"));
        //            myCommand.Parameters.Add(new SqlParameter("@ContractorID", "context.Request.QueryString[contractorID]"));
        //            myCommand.Parameters.AddWithValue("SDID", "context.Request.QueryString[IncidentID]");
        //            //Convert the file content to the byte array to store in the database.
        //            //byte[] myFileData = new byte[uploadFile.ContentLength];
        //            //uploadFile.InputStream.Read(myFileData, 0, uploadFile.ContentLength);
        //            myCommand.Parameters.Add(new SqlParameter("@Document", "myFileData"));

        //            myCommand.Connection.Open();
        //            myCommand.ExecuteNonQuery();
        //            myCommand.Connection.Close();
        //            myCommand.Dispose();
        //        }
        //        //        }
        //        //    }
        //        //}
        //    }
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        //public bool EmployeeUpdate(int id, string fname, string lname, string address, string mobile)
        //{

        //    int rowsAffected = -1;
        //    employee.EmployeeDatatableDataTable EmployeeDatas = EmployeeAdapter.EmployeeGetByID(id);

        //    if (EmployeeDatas.Rows.Count > 0)
        //    {
        //        employee.EmployeeDatatableRow EmployeeDataRow = EmployeeDatas[0];
        //        EmployeeDataRow.fname = fname;
        //        EmployeeDataRow.lname = lname;
        //        EmployeeDataRow.address = address;
        //        EmployeeDataRow.mobile = mobile;

        //        rowsAffected = EmployeeAdapter.Update(EmployeeDataRow);
        //    }
        //    return rowsAffected == 1;
        //}

        #endregion "AC2PDocuments Details"

    }
}
