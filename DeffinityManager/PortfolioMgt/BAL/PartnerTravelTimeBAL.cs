using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerTravelTimeBAL
    {
        public static PortfolioMgt.Entity.PartnerTravelTime PartnerTravelTimeBAL_Add(PortfolioMgt.Entity.PartnerTravelTime cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerTravelTime PartnerTravelTimeBAL_Update(PortfolioMgt.Entity.PartnerTravelTime cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.TravelTimeMinutes = cat.TravelTimeMinutes;
            }

            pRep.Edit(s);
            return s;
        }
        public static bool PartnerTravelTimeBAL_IsExists(int categoryID, string TravelTimeMinutes)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            var retEntity = pRep.GetAll().Where(o => o.TravelTimeMinutes == Convert.ToInt32(TravelTimeMinutes) && o.PartnerID == sessionKeys.PartnerID).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }
        public static bool ManufacturerBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static List<PortfolioMgt.Entity.PartnerTravelTime> PartnerTravelTimeBAL_SelectByPortfolioID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }
        public static List<PortfolioMgt.Entity.PartnerTravelTime> PartnerTravelTimeBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();

        }
        public static PortfolioMgt.Entity.PartnerTravelTime PartnerTravelTimeBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerTravelTime> PartnerTravelTimeBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTravelTime>();
            return pRep.GetAll();

        }

    }
}
