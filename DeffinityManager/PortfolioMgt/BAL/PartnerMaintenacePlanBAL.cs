using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerMaintenacePlanBAL
    {
        public static PortfolioMgt.Entity.PartnerMaintenacePlan PartnerMaintenacePlanBAL_Add(PortfolioMgt.Entity.PartnerMaintenacePlan cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            cat.LoggedBy = sessionKeys.UID;
            cat.LoggedDate = DateTime.Now;
            cat.ModifiDate = DateTime.Now;
            cat.ModifiedBy = sessionKeys.UID;
                       
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerMaintenacePlan PartnerMaintenacePlanBAL_Update(PortfolioMgt.Entity.PartnerMaintenacePlan mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            var s = pRep.GetAll().Where(o => o.MaintenacePlanID == mat.MaintenacePlanID).FirstOrDefault();
            if (s != null)
            {
                s.Amount = mat.Amount;
                s.ModifiDate = DateTime.Now;
                s.ModifiedBy = sessionKeys.UID;
            }
            pRep.Edit(s);
            return s;
        }
      
        public static bool PartnerMaintenacePlanBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            var retEntity = pRep.GetAll().Where(o => o.MaintenacePlanID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
      
        public static List<PortfolioMgt.Entity.PartnerMaintenacePlan> PartnerMaintenacePlanBAL_SelectByAddressID(int addressid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            return pRep.GetAll().Where(o => o.AddressID == addressid).ToList();
        }
        public static PortfolioMgt.Entity.PartnerMaintenacePlan PartnerMaintenacePlanBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            return pRep.GetAll().Where(o => o.MaintenacePlanID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerMaintenacePlan> PartnerMaintenacePlanBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlan>();
            return pRep.GetAll();

        }

    }
}
