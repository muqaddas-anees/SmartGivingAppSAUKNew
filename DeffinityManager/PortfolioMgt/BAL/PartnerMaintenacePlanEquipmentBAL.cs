using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerMaintenacePlanEquipmentEquipmentBAL
    {

        public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipment PartnerMaintenacePlanEquipmentEquipmentBAL_Add(PortfolioMgt.Entity.PartnerMaintenacePlanEquipment cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
           
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipment PartnerMaintenacePlanEquipmentEquipmentBAL_Update(PortfolioMgt.Entity.PartnerMaintenacePlanEquipment mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
            var s = pRep.GetAll().Where(o => o.EquipmentID == mat.EquipmentID).FirstOrDefault();
            if (s != null)
            {
                s.ChecklistID = mat.ChecklistID;
                s.EquipmentTypeID = mat.EquipmentTypeID;
                s.MaintenacePlanID = mat.MaintenacePlanID;
                s.ManufacturerID = mat.ManufacturerID;
                s.ModelNumber = mat.ModelNumber;
                s.QTY = mat.QTY;
                s.SerialNumber = mat.SerialNumber;
                s.StartMonth = mat.StartMonth;
                s.TimePerYear = mat.TimePerYear;
                s.TypeOfEquipmentID = mat.TypeOfEquipmentID;
                
            }
            pRep.Edit(s);
            return s;
        }

        public static bool PartnerMaintenacePlanEquipmentEquipmentBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
            var retEntity = pRep.GetAll().Where(o => o.EquipmentID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static List<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> PartnerMaintenacePlanEquipmentEquipmentBAL_SelectByEquipmentID(int equipmentID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
            return pRep.GetAll().Where(o => o.EquipmentID == equipmentID).ToList();
        }
        public static List<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment> V_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectByEquipmentID(int equipmentID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment>();
            return pRep.GetAll().Where(o => o.EquipmentID == equipmentID).ToList();
        }
        public static PortfolioMgt.Entity.PartnerMaintenacePlanEquipment PartnerMaintenacePlanEquipmentEquipmentBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
            return pRep.GetAll().Where(o => o.EquipmentID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerMaintenacePlanEquipment>();
            return pRep.GetAll();

        }
        public static IQueryable<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment> v_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment> pRep = new PortfolioRepository<PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment>();
            return pRep.GetAll();

        }

      

    }
}
