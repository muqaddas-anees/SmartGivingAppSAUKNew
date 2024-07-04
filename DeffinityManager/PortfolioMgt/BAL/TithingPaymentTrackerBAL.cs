using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class TithingPaymentTrackerBAL
    {

        public static List<string> GetDonorEmails()
        {
            List<string> emailList = new List<string>();

            var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
            foreach (var t in tList)
            {
                if (t.DonerEmail != null)
                {
                    if (emailList.Where(o => o == t.DonerEmail.Trim().ToLower()).Count() == 0)
                        emailList.Add(t.DonerEmail.Trim().ToLower());
                }
            }

            return emailList;
        }

        public static PortfolioMgt.Entity.TithingPaymentTracker TithingPaymentTrackerBAL_Add(PortfolioMgt.Entity.TithingPaymentTracker cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            cat.CreatedDateTime = DateTime.Now;
            cat.ModifiedDateTime = DateTime.Now;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.TithingPaymentTracker TithingPaymentTrackerBAL_Update(PortfolioMgt.Entity.TithingPaymentTracker cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.IsPaid = cat.IsPaid;
                if (s.IsPaid.HasValue)
                {
                    if (s.IsPaid.Value)
                    {
                        s.PaidDate = DateTime.Now;
                    }
                }
                s.ModifiedDateTime = DateTime.Now;
                s.unid = cat.unid;
                s.Notes = cat.Notes;
            }

            pRep.Edit(s);
            return s;
        }
      

        public static bool TithingPaymentTrackerBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.TithingPaymentTracker> TithingPaymentTrackerBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            return pRep.GetAll();

        }


    }
}
