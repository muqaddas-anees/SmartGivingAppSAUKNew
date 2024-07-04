using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerMaintenacePlanEquipmentMaterialBAL
    {
         public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial PartnerMaintenacePlanEquipmentMaterialMaterialBAL_Add(PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
           
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial PartnerMaintenacePlanEquipmentMaterialMaterialBAL_Update(PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Material = mat.Material;
                s.Price = mat.Price;
                s.QtyPerVisit = mat.QtyPerVisit;
                s.EquipmentID = mat.EquipmentID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool PartnerMaintenacePlanEquipmentMaterialMaterialBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
            var retEntity = pRep.GetAll().Where(o => o.EquipmentID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static List<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> PartnerMaintenacePlanEquipmentMaterialMaterialBAL_SelectByEquipmentID(int addressid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
            return pRep.GetAll().Where(o => o.EquipmentID == addressid).ToList();
        }
        public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial PartnerMaintenacePlanEquipmentMaterialBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> PartnerMaintenacePlanEquipmentMaterialBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial>();
            return pRep.GetAll();

        }

    }
}
