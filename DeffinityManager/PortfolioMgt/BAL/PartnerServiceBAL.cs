using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class PartnerServiceBAL
    {
        public static PartnerService PartnerServiceBAL_Add(PartnerService sub)
        {
            if (!PartnerServiceBAL_IsExists(sub.PartnerSubCategoryID, sub.ServiceName))
            {
                IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
                pRep.Add(sub);
            }
            return sub;
        }

        public static PartnerService PartnerServiceBAL_Update(PartnerService sub)
        {
            IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
            var s = pRep.GetAll().Where(o => o.ID == sub.ID).FirstOrDefault();
            if (s != null)
            {
                s.ServiceName = sub.ServiceName;
                s.TimeInMinutes = sub.TimeInMinutes;
                s.IsDeleted = sub.IsDeleted;
            }

            pRep.Edit(s);
            return sub;
        }
        public static bool PartnerServiceBAL_IsExists(int subcategoryID, string serviceName)
        {
            IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
            var retEntity = pRep.GetAll().Where(o => o.PartnerSubCategoryID == subcategoryID && o.ServiceName.ToLower().Trim() == serviceName.ToLower().Trim() && o.IsDeleted == false).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }
        public static List<PartnerService> PartnerServiceBAL_SelectBySubCategoryID(int subcategoryID)
        {
            IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
            return pRep.GetAll().Where(o => o.PartnerSubCategoryID == subcategoryID && o.IsDeleted == false).ToList();

        }
        public static PartnerService PartnerServiceBAL_Select(int id)
        {
            IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static List<PartnerService> PartnerServiceBAL_SelectAll()
        {
            IPortfolioRepository<PartnerService> pRep = new PortfolioRepository<PartnerService>();
            return pRep.GetAll().ToList();

        }
    }
}
