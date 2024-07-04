using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for ReceivedInformationJournalBAL
    /// </summary>
    public class ReceivedInformationJournalBAL
    {
        #region Add ReceivedInformationJournal
        public static void AddReceivedInformation(RecievedInformationJournal riJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.RecievedInformationJournals.InsertOnSubmit(riJournal);
                dd.SubmitChanges();
            }
        }
        #endregion
    
        #region Select ReceivedInformationJournal by CallID
        public static RecievedInformationJournal SelectbyCallId(int cid)
        {

            RecievedInformationJournal ri = new RecievedInformationJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                ri = dd.RecievedInformationJournals.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return ri;
        }
        #endregion
        #region ReceivedInformationJournal by CallID
        public static List<RecievedInformationJournal> SelectReceivedInformationJournalbyCallID(int cid)
        {
            List<RecievedInformationJournal> lstrij = new List<RecievedInformationJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                lstrij = dd.RecievedInformationJournals.Where(c => c.CallID == cid).Select(c => c).ToList();
                return lstrij;
            }
        }
        #endregion
    }
}