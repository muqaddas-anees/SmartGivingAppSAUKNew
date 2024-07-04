using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for DocumentsBAL
    /// </summary>
    public class DocumentsBAL
    {
        #region Bind Documents
        public static List<Document> BindDocuments()
        {
            List<Document> documentList = new List<Document>();
            using (DCDataContext dd = new DCDataContext())
            {
                documentList = dd.Documents.Select(r => r).ToList();
            }
            return documentList;
        }
        #endregion

        #region Add Documents
        public static void AddDocuments(Document d)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.Documents.InsertOnSubmit(d);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Documents by ID
        public static Document SelectbyId(int id)
        {

            Document d = new Document();
            using (DCDataContext dd = new DCDataContext())
            {
                d = dd.Documents.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return d;
        }
        #endregion
        #region Select Documents by Call ID
        public static List<Document> SelectbyCallId(int cid)
        {

            List<Document> dList = new List<Document>();
            using (DCDataContext dd = new DCDataContext())
            {
                dList = dd.Documents.Where(r => r.CallID == cid).Select(r => r).ToList();
            }
            return dList;
        }
        #endregion
        # region Update Documents
        public static void DocumentsUpdate(Document d)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                Document dcurrent = dd.Documents.Where(r => r.ID == d.ID).Select(r => r).FirstOrDefault();
                dcurrent.CallID = d.CallID;
                dcurrent.DocumentID = d.DocumentID;
                dcurrent.FileName = d.FileName;
                dcurrent.ContentType = d.ContentType;               
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Documents Delete By ID
        public static void DocumentsDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                Document d = dd.Documents.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (d != null)
                {
                    dd.Documents.DeleteOnSubmit(d);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}