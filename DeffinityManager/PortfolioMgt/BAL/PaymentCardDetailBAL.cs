using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PaymentCardDetailBAL
    {
        public static PortfolioMgt.Entity.PaymentCardDetail PaymentCardDetailBAL_Add(PortfolioMgt.Entity.PaymentCardDetail cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail>();
          

            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PaymentCardDetail PaymentCardDetailBAL_Update(PortfolioMgt.Entity.PaymentCardDetail mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CardDisplayNumber = mat.CardDisplayNumber;
                s.CardNumber = mat.CardNumber;
                s.CardType = mat.CardType;
                s.cvv = mat.cvv;
                s.ExpiryMonth = mat.ExpiryMonth;
                s.ExpiryYear = mat.ExpiryYear;
                s.IsActive = mat.IsActive;
                s.Name = mat.Name;
                s.UserID = mat.UserID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool PaymentCardDetailBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.PaymentCardDetail> PPaymentCardDetailBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PaymentCardDetail>();
            return pRep.GetAll();

        }
    }
}
