using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerHourlyRateBAL
    {

        public static PortfolioMgt.Entity.PartnerHourlyRate PartnerHourlyRateBAL_Add(PortfolioMgt.Entity.PartnerHourlyRate cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerHourlyRate PartnerHourlyRateBAL_Update(PortfolioMgt.Entity.PartnerHourlyRate mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.HourlyRate = mat.HourlyRate;
              
            }
            pRep.Edit(s);
            return s;
        }

        public static double PartnerHourlyRateBAL_SelectByPortfolioID()
        {
            double retval = 0;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            var p= pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            if (p != null)
                retval = p.HourlyRate;
            else
                retval = 0;

            return retval;

        }
        public static double PartnerHourlyRateBAL_SelectByPartnerID()
        {
            double retval = 0;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            var p = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PartnerID).FirstOrDefault();
            if (p != null)
                retval = p.HourlyRate;
            else
                retval = 0;

            return retval;
        }
        public static PortfolioMgt.Entity.PartnerHourlyRate PartnerHourlyRateBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerHourlyRate> PartnerHourlyRateBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerHourlyRate>();
            return pRep.GetAll();

        }

    }
}
