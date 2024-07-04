using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerMaterialBAL
    {
        public static PortfolioMgt.Entity.PartnerMaterial PartnerMaterialBAL_Add(PortfolioMgt.Entity.PartnerMaterial cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            cat.LoggedBy = sessionKeys.UID;
            cat.LoggedDate = DateTime.Now;
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerMaterial PartnerMaterialBAL_Update(PortfolioMgt.Entity.PartnerMaterial mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Cost = mat.Cost;
                s.Discount = mat.Discount;
                s.ImageID = mat.ImageID;
                s.Markup = mat.Markup;
                s.MaterialDescription = mat.MaterialDescription;
                s.MaterialTitle = mat.MaterialTitle;
                s.Notes = mat.Notes;
                s.Price = mat.Price;
            }
            pRep.Edit(s);
            return s;
        }
        public static bool PartnerMaterialBAL_IsExists(int categoryID, string materialTitle)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            var retEntity = pRep.GetAll().Where(o => o.MaterialTitle.ToLower().Trim() == materialTitle.ToLower().Trim() && o.PartnerID == sessionKeys.PartnerID).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }

        public static bool PartnerMaterialBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static List<PortfolioMgt.Entity.PartnerMaterial> PartnerMaterialBAL_SelectByPortfolioID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }
        public static List<PortfolioMgt.Entity.PartnerMaterial> PartnerMaterialBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();
        }
        public static PortfolioMgt.Entity.PartnerMaterial PartnerMaterialBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerMaterial> PartnerMaterialBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaterial>();
            return pRep.GetAll();

        }
    }
}
