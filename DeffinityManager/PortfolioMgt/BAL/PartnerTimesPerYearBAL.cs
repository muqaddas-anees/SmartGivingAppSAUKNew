using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerTimesPerYearBAL
    {
        public static PortfolioMgt.Entity.PartnerTimesPerYear PartnerTimesPerYearBAL_Add(PortfolioMgt.Entity.PartnerTimesPerYear cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerTimesPerYear PartnerTimesPerYearBAL_Update(PortfolioMgt.Entity.PartnerTimesPerYear cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.TimePerYear = cat.TimePerYear;
            }

            pRep.Edit(s);
            return s;
        }
        public static bool PartnerTimesPerYearBAL_IsExists(int categoryID, string timePerYear)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            var retEntity = pRep.GetAll().Where(o => o.TimePerYear == Convert.ToInt32(timePerYear) && o.PartnerID == sessionKeys.PartnerID).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }
        public static bool ManufacturerBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static List<PortfolioMgt.Entity.PartnerTimesPerYear> PartnerTimesPerYearBAL_SelectByPortfolioID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }
        public static List<PortfolioMgt.Entity.PartnerTimesPerYear> PartnerTimesPerYearBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();

        }
        public static PortfolioMgt.Entity.PartnerTimesPerYear PartnerTimesPerYearBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerTimesPerYear> PartnerTimesPerYearBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTimesPerYear>();
            return pRep.GetAll();

        }

    }
}
