using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for ServicePriceJournal
    /// </summary>
    public class ServicePriceJournalBAL
    {
        #region Add FLSDetailsJournal
        public static void AddServicePriceJournal(ServicePriceJournal servicePriceJournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.ServicePriceJournals.InsertOnSubmit(servicePriceJournal);
                dd.SubmitChanges();
            }
        }
        #endregion

        #region Select FLSDetailsJournal By CallID
        public static ServicePriceJournal SelectByCallID(int CallID)
        {
            ServicePriceJournal fj = new ServicePriceJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                fj = dd.ServicePriceJournals.Where(f => f.IncidentID == CallID &&  f.Type.ToLower() == "fls").Select(f => f).FirstOrDefault();
            }
            return fj;
        }
        #endregion
        #region Select FLSDetailsJournal by CallID
        public static List<ServicePriceJournal> SelectServicePriceJournalbyCallID(int cid)
        {
            List<ServicePriceJournal> lstflsj = new List<ServicePriceJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                lstflsj = dd.ServicePriceJournals.Where(c => c.IncidentID == cid && c.Type.ToLower() == "fls").Select(c => c).ToList();
                return lstflsj;
            }
        }

        public static List<ServicePriceJournal> SelectServicePriceJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectServicePriceJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion
    }
}