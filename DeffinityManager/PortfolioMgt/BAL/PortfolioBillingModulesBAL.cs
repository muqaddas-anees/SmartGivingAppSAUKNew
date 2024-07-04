using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public static class PortfolioBillingModulesBAL
    {
        public static PortfolioMgt.Entity.PortfolioBillingModule PortfolioBillingModulesBAL_Add(PortfolioMgt.Entity.PortfolioBillingModule cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule>();

            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PortfolioBillingModule PortfolioBillingModulesBAL_Update(PortfolioMgt.Entity.PortfolioBillingModule cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.ModuleID = cat.ModuleID;
                s.PortfolioBillingTypeID = cat.PortfolioBillingTypeID;
                
            }

            pRep.Edit(s);
            return s;
        }


        public static bool PortfolioBillingModulesBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static PortfolioMgt.Entity.PortfolioBillingModule PortfolioBillingModulesBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioBillingModule> PortfolioBillingModulesBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingModule>();
            return pRep.GetAll();

        }


    }
}
