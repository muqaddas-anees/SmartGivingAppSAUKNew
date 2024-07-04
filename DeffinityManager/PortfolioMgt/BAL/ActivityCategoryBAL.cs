using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class ActivityCategoryBAL
    {
        public static PortfolioMgt.Entity.ActivityCategory ActivityCategoryBAL_Add(PortfolioMgt.Entity.ActivityCategory cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
           

            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.ActivityCategory ActivityCategoryBAL_Update(PortfolioMgt.Entity.ActivityCategory mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Name = mat.Name;
                s.OrganizationID = mat.OrganizationID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool ActivityCategoryBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.ActivityCategory> ActivityCategoryBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
            return pRep.GetAll();

        }
    }
}
