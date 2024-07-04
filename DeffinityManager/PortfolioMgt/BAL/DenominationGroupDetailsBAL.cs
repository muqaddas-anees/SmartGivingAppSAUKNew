using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class DenominationGroupDetailsBAL
    {
        public static IQueryable<PortfolioMgt.Entity.DenominationGroupDetail> DenominationGroupDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            return pRep.GetAll();
        }

        public static PortfolioMgt.Entity.DenominationGroupDetail DenominationGroupDetailsBAL_Add(PortfolioMgt.Entity.DenominationGroupDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            var p = pRep.GetAll().Where(o => o.Name.ToLower().Trim() == t.Name.ToLower().Trim() && o.DenominationDetailsID == t.DenominationDetailsID).FirstOrDefault();
            if (p == null)
            {

                pRep.Add(t);
            }
            return t;
        }
        public static PortfolioMgt.Entity.DenominationGroupDetail DenominationGroupDetailsBAL_Update(PortfolioMgt.Entity.DenominationGroupDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            var p = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if (p != null)
            {

                //p.IsActive = t.IsActive;
                p.Name = t.Name;
                p.DateStamp = t.DateStamp;
                p.Contribution = t.Contribution;
                pRep.Edit(p);
            }
            return p;
        }

        public static bool DenominationGroupDetailsBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
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
