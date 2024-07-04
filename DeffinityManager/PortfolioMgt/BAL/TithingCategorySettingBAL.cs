using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class TithingCategorySettingBAL
    {
        public static PortfolioMgt.Entity.TithingCategorySetting TithingCategorySettingBAL_Add(PortfolioMgt.Entity.TithingCategorySetting cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();


            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.TithingCategorySetting TithingCategorySettingBAL_Update(PortfolioMgt.Entity.TithingCategorySetting mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Name = mat.Name;
                s.CategoryID = mat.CategoryID;
                s.Description = mat.Description;
                s.IsActive = mat.IsActive;
                s.IsHidden = mat.IsHidden;
                s.OrganizationID = mat.OrganizationID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool TithingCategorySettingBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.TithingCategorySetting> TithingCategorySettingBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();
            return pRep.GetAll();

        }
    }
}
