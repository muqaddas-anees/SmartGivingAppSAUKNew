using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryMgt.DAL;
using InventoryMgt.Entity;

namespace InventoryUse.BAL
{
    /// <summary>
    /// Summary description for InventoryUseBAL
    /// </summary>
    public class InventoryUseBAL
    {
        public InventoryUseBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Get InventoryUse List
        public static IEnumerable<Inventory_Use> BindInventoryUse()
        {
            List<Inventory_Use> use = new List<Inventory_Use>();
            using (InventoryDataContext db = new InventoryDataContext())
            {
                use = db.Inventory_Uses.Select(o => o).OrderBy(o => o.Name).ToList();
            }
            return use;
        }
        #endregion

        #region Add InventoryUse
        public static void AddInventoryUse(Inventory_Use inventory_Use)
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                db.Inventory_Uses.InsertOnSubmit(inventory_Use);
                db.SubmitChanges();
            }

        }
        #endregion

        #region Update InventoryUse
        public static void UpdateInventoryUse(Inventory_Use inventory_Use)
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                Inventory_Use inventory_UseCurrent = db.Inventory_Uses.Where(o => o.ID == inventory_Use.ID).Select(o => o).FirstOrDefault();
                inventory_UseCurrent.Name = inventory_Use.Name;
                inventory_UseCurrent.Code = inventory_Use.Code;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Delete InventoryUse By ID
        public static void DeleteInventoryUse(int id)
        {

            using (InventoryDataContext dd = new InventoryDataContext())
            {
                Inventory_Use iu = dd.Inventory_Uses.Where(o => o.ID == id).Select(o => o).FirstOrDefault();
                if (iu != null)
                {
                    dd.Inventory_Uses.DeleteOnSubmit(iu);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
        #region Select InventoryUse by ID
        public static Inventory_Use SelectbyId(int id)
        {
            Inventory_Use iu = new Inventory_Use();
            using (InventoryDataContext dd = new InventoryDataContext())
            {
                iu = dd.Inventory_Uses.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return iu;
        }
        #endregion
        #region Check InventoryUse Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name,string code)
        {
            Inventory_Use iu = new Inventory_Use();
            using (InventoryDataContext dd = new InventoryDataContext())
            {
                iu = dd.Inventory_Uses.Where(r => r.ID != id && (r.Name.ToLower() == name.ToLower() || r.Code == code)).Select(r => r).FirstOrDefault();
            }
            if (iu != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Check InventoryUse Exists when Inserting
        public static bool CheckExists(string name,string code)
        {

            Inventory_Use iu = new Inventory_Use();
            using (InventoryDataContext dd = new InventoryDataContext())
            {
                iu = dd.Inventory_Uses.Where(r => r.Name.ToLower() == name.ToLower() || r.Code == code).Select(r => r).FirstOrDefault();
            }
            if (iu != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}