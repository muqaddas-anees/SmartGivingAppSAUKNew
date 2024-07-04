using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class ActivitySubCategoryBAL
    {
        public static PortfolioMgt.Entity.ActivitySubCategory ActivitySubCategoryBAL_Add(PortfolioMgt.Entity.ActivitySubCategory cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();


            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.ActivitySubCategory ActivitySubCategoryBAL_Update(PortfolioMgt.Entity.ActivitySubCategory mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Name = mat.Name;
                s.ActivityCategoryID = mat.ActivityCategoryID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool ActivitySubCategoryBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.ActivitySubCategory> ActivitySubCategoryBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();
            return pRep.GetAll();

        }
    }
}
