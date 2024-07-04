using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgt.BAL
{
    public class WarehouseDetailsBAL
    {
        //Add
        public static InventoryMgt.Entity.WarehouseDetail WarehouseDetailsBAL_Add(InventoryMgt.Entity.WarehouseDetail wd)
        {
            IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
            //set logged in user
            wd.LoggedBy = sessionKeys.UID;
            wd.LoggedDate = DateTime.Now;
            wd.PortfolioID = sessionKeys.PortfolioID;
            wdRep.Add(wd);
            return wd;
        }
        //Update 
        public static bool WarehouseDetailsBAL_Update(InventoryMgt.Entity.WarehouseDetail wd)
        {
            bool retval = false;
            try
            {
                IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
                var eEntity = wdRep.GetAll().Where(o => o.ID == wd.ID).FirstOrDefault();
                if (eEntity != null)
                {
                    eEntity.Address1 = wd.Address1;
                    eEntity.Address2 = wd.Address2;
                    eEntity.City = wd.City;
                    eEntity.Email = wd.Email;
                    eEntity.Mobile = wd.Mobile;
                    eEntity.Postcode = wd.Postcode;
                    eEntity.Town = wd.Town;
                    eEntity.WarehouseManagerID = wd.WarehouseManagerID;
                    eEntity.WarehouseName = wd.WarehouseName;
                    wdRep.Edit(wd);
                    retval = true;

                }
            }
            catch
            {
                throw;
            }
            return retval;
        }
        //Delete
        public static bool WarehouseDetailsBAL_Delete(int WarehouseID)
        {
            bool retval = false;
            try
            {
                IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
                var eEntity = wdRep.GetAll().Where(o => o.ID == WarehouseID).FirstOrDefault();
                if (eEntity != null)
                {
                    wdRep.Delete(eEntity);
                }
                retval = true;
            }
            catch
            {
                throw;
            }
            return retval;
        }
        //check is exists
        public static bool WarehouseDetailsBAL_WarehouseNameExists(string WarehouseName,int id =0)
        {
            bool retval = false;
            try
            {
                IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
                if (id == 0)
                {
                    var eEntity = wdRep.GetAll().Where(o => o.WarehouseName.ToLower() == WarehouseName.ToLower() && o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (eEntity != null)
                        retval = true;
                    else
                        retval = false;
                }else
                {
                    var eEntity = wdRep.GetAll().Where(o => o.WarehouseName.ToLower() == WarehouseName.ToLower() && o.PortfolioID == sessionKeys.PortfolioID && o.ID != id).FirstOrDefault();
                    if (eEntity != null)
                        retval = true;
                    else
                        retval = false;

                }
            }
            catch
            {
                throw;
            }
            return retval;
        }
        //Select by id
        public static InventoryMgt.Entity.WarehouseDetail WarehouseDetailsBAL_SelectByID(int WarehouseID)
        {
            IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
            return wdRep.GetAll().Where(o => o.ID == WarehouseID).FirstOrDefault();
        }
        //Select all
        public static List<InventoryMgt.Entity.WarehouseDetail> WarehouseDetailsBAL_SelectAll()
        {
            IinventoryRepository<InventoryMgt.Entity.WarehouseDetail> wdRep = new InventoryRepository<InventoryMgt.Entity.WarehouseDetail>();
            return wdRep.GetAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID).OrderBy(o=>o.WarehouseName).ToList();
        }
    }
}
