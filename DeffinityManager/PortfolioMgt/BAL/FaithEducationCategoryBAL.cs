using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class FaithEducationCategoryBAL
    {
        public static PortfolioMgt.Entity.FaithEducationCategory FaithEducationCategoryBAL_Add(PortfolioMgt.Entity.FaithEducationCategory cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory>();


            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.FaithEducationCategory FaithEducationCategoryBAL_Update(PortfolioMgt.Entity.FaithEducationCategory mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.RegionID = mat.RegionID;
                s.OrganizationID = s.OrganizationID;
                s.DenominationID = s.DenominationID;
                s.CategoryName = mat.CategoryName;
                s.OrganizationID = mat.OrganizationID;
                
            }
            pRep.Edit(s);
            return s;
        }

        public static bool FaithEducationCategoryBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.FaithEducationCategory> FaithEducationCategoryBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationCategory>();
            return pRep.GetAll();

        }

    }
}
