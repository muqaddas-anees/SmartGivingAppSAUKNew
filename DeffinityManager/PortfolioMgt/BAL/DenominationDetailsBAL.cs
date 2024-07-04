using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class DenominationDetailsBAL
    {
        public static IQueryable<PortfolioMgt.Entity.DenominationDetail> DenominationDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();
            return pRep.GetAll();
        }

        public static PortfolioMgt.Entity.DenominationDetail DenominationDetailsBAL_Add(PortfolioMgt.Entity.DenominationDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();
            var p = pRep.GetAll().Where(o => o.Name.ToLower().Trim() == t.Name.ToLower().Trim() ).FirstOrDefault();
            if (p == null)
            {

                pRep.Add(t);
            }
            return t;
        }
        public static PortfolioMgt.Entity.DenominationDetail DenominationDetailsBAL_Update(PortfolioMgt.Entity.DenominationDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();
            var p = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if (p != null)
            {

                p.IsActive = t.IsActive;
                p.Name = t.Name;
                pRep.Edit(p);
            }
            return p;
        }

        public static bool DenominationDetailsBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();
            var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (p != null)
            {
                pRep.Delete(p);
                retval = true;
            }
            return retval;
        }

    }
}
