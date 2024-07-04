using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

/// <summary>
/// Summary description for AccessControlJournalBAL
/// </summary>
namespace DC.BAL
{

    public class AccessControlJournalBAL
    {
        #region Add AccessControlJournal
        public static void InsertAccessControlJournal(AccessControlJournal acJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.AccessControlJournals.InsertOnSubmit(acJournal);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select AccessControlJournal by CallID
        public static List<AccessControlJournal> SelectAccessControlJournalbyCallID(int cid)
        {
            List<AccessControlJournal> acjlist = new List<AccessControlJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                //  lstdij = dd.DeliveryInformationJournals.Where(c => c.CallID == cid).Select(c => c).OrderByDescending(c => c.ModifiedDate).Take(2).ToList();
                acjlist = dd.AccessControlJournals.Where(a => a.CallID == cid).Select(a => a).ToList();
                return acjlist;
            }
        }
        public static List<AccessControlJournal> SelectAccessControlJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectAccessControlJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion
        #region Select first record by CallID
        public static AccessControlJournal SelectFirstRecordbyCallID(int cid)
        {
           
            using (DCDataContext dd = new DCDataContext())
            {
                AccessControlJournal acjlist = new AccessControlJournal();
                using (DCDataContext dc = new DCDataContext())
                {

                    acjlist = (from c in dc.AccessControlJournals
                           where c.CallID == cid
                           orderby c.ModifiedDate ascending
                           select c).FirstOrDefault();
                }

                return acjlist;
            }
        }
        #endregion
    }
}