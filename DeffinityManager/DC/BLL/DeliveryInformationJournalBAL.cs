using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for DeliveryInformationJournalBAL
    /// </summary>
    public class DeliveryInformationJournalBAL
    {
        #region Add DeliveryInformationJournal
        public static void AddDeliveryInformationJournal(DeliveryInformationJournal diJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.DeliveryInformationJournals.InsertOnSubmit(diJournal);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select DeliveryInformationJournal by CallID
        public static DeliveryInformationJournal SelectbyCallId(int cid)
        {
            DeliveryInformationJournal di = new DeliveryInformationJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                di = dd.DeliveryInformationJournals.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return di;
        }
        #endregion
        #region Select DeliveryInformationJournal by CallID
        public static List<DeliveryInformationJournal> SelectDeliveryInformationJournalbyCallID(int cid)
        {
            List<DeliveryInformationJournal> lstdij = new List<DeliveryInformationJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                //  lstdij = dd.DeliveryInformationJournals.Where(c => c.CallID == cid).Select(c => c).OrderByDescending(c => c.ModifiedDate).Take(2).ToList();
                lstdij = dd.DeliveryInformationJournals.Where(c => c.CallID == cid).Select(c => c).ToList();
                return lstdij;
            }
        }
        public static List<DeliveryInformationJournal> SelectDeliveryInformationJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectDeliveryInformationJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion
    }
}