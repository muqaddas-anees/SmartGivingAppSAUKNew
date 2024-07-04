using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class ManufacturerBAL
    {
        public static PortfolioMgt.Entity.Manufacturer ManufacturerBAL_Add(PortfolioMgt.Entity.Manufacturer cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.Manufacturer ManufacturerBAL_Update(PortfolioMgt.Entity.Manufacturer cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            var s = pRep.GetAll().Where(o => o.id == cat.id).FirstOrDefault();
            if (s != null)
            {
                s.Name = cat.Name;
            }

            pRep.Edit(s);
            return s;
        }
        public static bool ManufacturerBAL_IsExists(int categoryID, string categoryName)
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            var retEntity = pRep.GetAll().Where(o => o.Name.ToLower().Trim() == categoryName.ToLower().Trim() && o.PartnerID == sessionKeys.PartnerID).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }

        public static bool ManufacturerBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            var retEntity = pRep.GetAll().Where(o => o.id == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;
           
        }
        public static List<PortfolioMgt.Entity.Manufacturer> ManufacturerBAL_SelectByPortfolioID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }
        public static List<PortfolioMgt.Entity.Manufacturer> ManufacturerBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();

        }
        public static PortfolioMgt.Entity.Manufacturer ManufacturerBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            return pRep.GetAll().Where(o => o.id == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.Manufacturer> ManufacturerBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.Manufacturer> pRep = new PortfolioRepository<PortfolioMgt.Entity.Manufacturer>();
            return pRep.GetAll();

        }

    }
}
