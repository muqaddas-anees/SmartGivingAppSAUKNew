using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public static class PortfolioBillingTypeBAL
    {
        public const string PlanName_Standard = "Standard";
        public const string PlanName_Advanced = "Advanced";
        public const string PlanName_Premium = "Premium";
        public static PortfolioMgt.Entity.PortfolioBillingType PortfolioBillingTypeBAL_Add(PortfolioMgt.Entity.PortfolioBillingType cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
          
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PortfolioBillingType PortfolioBillingTypeBAL_Update(PortfolioMgt.Entity.PortfolioBillingType cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.MonthlyPrice = cat.MonthlyPrice;
                s.PlanName = cat.PlanName;
                s.YearlyPrice = cat.YearlyPrice;
                s.IsActive = cat.IsActive;
            }

            pRep.Edit(s);
            return s;
        }
     

        public static bool PortfolioBillingTypeBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
       
        public static PortfolioMgt.Entity.PortfolioBillingType PortfolioBillingTypeBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioBillingType> PortfolioBillingTypeBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
            return pRep.GetAll();

        }

       

        public static IQueryable<PortfolioMgt.Entity.PortfolioBillingType> PortfolioBillingTypeBAL_SelectByPartner(int partnerid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingType>();
            var clist = pRep.GetAll().Where(o => o.PartnerID == partnerid).ToList();

            if(clist.Count ==0)
            {
                //add default data
                pRep.Add(new Entity.PortfolioBillingType() { PartnerID = partnerid, PlanName = PlanName_Standard, MonthlyPrice=10, YearlyPrice=100 });
                pRep.Add(new Entity.PortfolioBillingType() { PartnerID = partnerid, PlanName = PlanName_Advanced, MonthlyPrice = 20, YearlyPrice = 200 });
                pRep.Add(new Entity.PortfolioBillingType() { PartnerID = partnerid, PlanName = PlanName_Premium, MonthlyPrice = 30, YearlyPrice = 300 });
            }
            return pRep.GetAll().Where(o=>o.PartnerID == partnerid);

        }

    }
}
