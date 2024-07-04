using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgt.BAL
{
    public class InventoryCategoryBAL
    {

        public static bool InventoryCategoryBAL_CategoryAdd(string CategoryName)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            var d = IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == 0 && o.Name.ToLower() == CategoryName.ToLower()).FirstOrDefault();
            if (d == null)
            {
                d = new InventoryMgt.Entity.InventoryCategory();
                d.MasterID = 0;
                d.Name = CategoryName;
                d.PortfolioID = sessionKeys.PortfolioID;
                IvRep.Add(d);
                retval = true;
            }
            return retval;
        }
        public static bool InventoryCategoryBAL_SubCategoryAdd(string SubCategoryName, int MasterID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            var d = IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == MasterID && o.Name.ToLower() == SubCategoryName.ToLower()).FirstOrDefault();
            if (d == null)
            {
                d = new InventoryMgt.Entity.InventoryCategory();
                d.MasterID = MasterID;
                d.Name = SubCategoryName;
                d.PortfolioID = sessionKeys.PortfolioID;
                IvRep.Add(d);
                retval = true;
            }
            return retval;
        }

        public static bool InventoryCategoryBAL_CategoryUpdate(string CategoryName,int ID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            //check name already exists 
            var e = IvRep.GetAll().Where(o => o.ID != ID && o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == 0 && o.Name.ToLower() == CategoryName.ToLower()).FirstOrDefault();
            if (e == null)
            {
                var d = IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (d != null)
                {
                    d.Name = CategoryName;
                    IvRep.Edit(d);
                    retval = true;
                }
            }
            else
            {
                retval = false;
            }
            return retval;
        }
        public static bool InventoryCategoryBAL_SubCategoryUpdate(string SubcategoryName,int MasterID, int ID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            //check name already exists 
            var e = IvRep.GetAll().Where(o => o.ID != ID && o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == MasterID && o.Name.ToLower() == SubcategoryName.ToLower()).FirstOrDefault();
            if (e == null)
            {
                var d = IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (d != null)
                {
                    d.Name = SubcategoryName;
                    IvRep.Edit(d);
                    retval = true;
                }
            }
            else
            {
                retval = false;
            }
            return retval;
        }

        public static bool InventoryCategoryBAL_CategoryDelete(int ID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            var d = IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (d != null)
            {
                IvRep.Delete(d);
                retval = true;
            }
            return retval;
        }
        public static bool InventoryCategoryBAL_SubCategoryDelete(int ID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            var d = IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (d != null)
            {
                IvRep.Delete(d);
                retval = true;
            }
            return retval;
        }
        public static InventoryMgt.Entity.InventoryCategory InventoryCategoryBAL_CategorySelectByID(int ID)
        {
            
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            return IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
           
        }
        public static InventoryMgt.Entity.InventoryCategory InventoryCategoryBAL_SubCategorySelectByID(int ID)
        {
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            return IvRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }

        public static List<InventoryMgt.Entity.InventoryCategory> InventoryCategoryBAL_CategorySelect()
        {
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            return IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == 0).ToList();
        }
        public static List<InventoryMgt.Entity.InventoryCategory> InventoryCategoryBAL_SubCategorySelect(int MasterID)
        {
            InventoryRepository<InventoryMgt.Entity.InventoryCategory> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryCategory>();
            return IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.MasterID == MasterID).ToList();
        }

    }
}
