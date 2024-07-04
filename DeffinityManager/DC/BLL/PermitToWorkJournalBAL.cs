using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for PermitToWorkJournalBAL
    /// </summary>
    public class PermitToWorkJournalBAL
    {
        #region Add PermitToWorkJournalBAL
        public static void AddPermitToWorkJournalJournal(PermitToWorkJournal pwJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.PermitToWorkJournals.InsertOnSubmit(pwJournal);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select PermitToWorkJournalBAL by CallID
        public static PermitToWorkJournal SelectbyCallId(int cid)
        {
            PermitToWorkJournal pw = new PermitToWorkJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                pw = dd.PermitToWorkJournals.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return pw;
        }
        #endregion
        #region Select PermitToWorkJournalBAL by CallID
        public static List<PermitToWorkJournal> SelectPermitToWorkJournalbyCallID(int cid)
        {
            List<PermitToWorkJournal> lstpw = new List<PermitToWorkJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                //  lstdij = dd.DeliveryInformationJournals.Where(c => c.CallID == cid).Select(c => c).OrderByDescending(c => c.ModifiedDate).Take(2).ToList();
                lstpw = dd.PermitToWorkJournals.Where(c => c.CallID == cid).Select(c => c).ToList();
                return lstpw;
            }
        }
        public static List<PermitToWorkJournal> SelectPermitToWorkJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectPermitToWorkJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion
    }
}