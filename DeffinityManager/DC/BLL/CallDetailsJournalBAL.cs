using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for CallDetailsJournalBAL
    /// </summary>
    public class CallDetailsJournalBAL
    {
        #region Add Call Details Journal
        public static void AddCallDetailsJournal(CallDetailsJournal cdJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.CallDetailsJournals.InsertOnSubmit(cdJournal);
                dd.SubmitChanges();
            }
        }

        public static void AddCallDetailsJournal(CallDetail cd)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                var cj = new CallDetailsJournal();
                cj.CallID = cd.ID;
                cj.LoggedBy = cd.LoggedBy;
                cj.LoggedDate = cd.LoggedDate;
                cj.ModifiedBy = cd.LoggedBy;
                cj.ModifiedDate = cd.LoggedDate;
                cj.RequesterID = cd.RequesterID;
                cj.RequestTypeID = cd.RequestTypeID;
                cj.SiteID = cd.SiteID;
                cj.StatusID = cd.StatusID;
                cj.VisibleToCustomer = false;
                dd.CallDetailsJournals.InsertOnSubmit(cj);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Call Details Journal by ID
        public static CallDetailsJournal SelectbyId(int id)
        {

            CallDetailsJournal cdJournal = new CallDetailsJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                cdJournal = dd.CallDetailsJournals.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return cdJournal;
        }
        #endregion
        #region Select Call Details Journal by CallID
        public static List<CallDetailsJournal> SelectCallDetailsJournalbyCallID(int cid)
        {
            List<CallDetailsJournal> lstcdj = new List<CallDetailsJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
              //  lstcdj = dd.CallDetailsJournals.Where(c => c.CallID == cid).Select(c => c).OrderByDescending(c => c.ModifiedDate).Take(2).ToList();
                lstcdj = dd.CallDetailsJournals.Where(c => c.CallID == cid).Select(c => c).ToList();
                return lstcdj;
            }
        }
        public static List<CallDetailsJournal> SelectCallDetailsJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectCallDetailsJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion

         #region Select call details by date
        public static CallDetailsJournal SelectByDate(int cid)
        {
            CallDetailsJournal cdj = new CallDetailsJournal();
            using (DCDataContext dc = new DCDataContext())
            {

                cdj = (from c in dc.CallDetailsJournals
                        where c.CallID == cid orderby c.ModifiedDate ascending
                        select c).FirstOrDefault();
            }

            return cdj;
        }
        #endregion
       
    }
}