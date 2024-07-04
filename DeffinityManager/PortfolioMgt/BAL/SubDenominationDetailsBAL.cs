using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class SubDenominationDetailsBAL
    {
        public static IQueryable<PortfolioMgt.Entity.SubDenominationDetail> SubDenominationDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
            return pRep.GetAll();
        }

        public static PortfolioMgt.Entity.SubDenominationDetail SubDenominationDetailsBAL_Add(PortfolioMgt.Entity.SubDenominationDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
            var p = pRep.GetAll().Where(o => o.Name.ToLower().Trim() == t.Name.ToLower().Trim() && o.DenominationGroupDetailsID == t.DenominationGroupDetailsID).FirstOrDefault();
            if (p == null)
            {
             
                pRep.Add(t);
            }
            return t;
        }
        public static PortfolioMgt.Entity.SubDenominationDetail SubDenominationDetailsBAL_Update(PortfolioMgt.Entity.SubDenominationDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
            var p = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if (p != null)
            {
               
                p.IsActive = t.IsActive;
                p.Name = t.Name;
                p.DateStamp = t.DateStamp;
                p.Contribution = t.Contribution;
                p.DenominationGroupDetailsID = t.DenominationGroupDetailsID;
                pRep.Edit(p);
            }
            return p;
        }

        public static bool SubDenominationDetailsBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
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
