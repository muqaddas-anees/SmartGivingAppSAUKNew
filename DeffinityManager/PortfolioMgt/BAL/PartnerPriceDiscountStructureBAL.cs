using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public  class PartnerPriceDiscountStructureBAL
    {

        public static PortfolioMgt.Entity.PartnerPriceDiscountStructure PartnerPriceDiscountStructureBAL_Add(PortfolioMgt.Entity.PartnerPriceDiscountStructure cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure>();
          
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerPriceDiscountStructure PartnerPriceDiscountStructureBAL_Update(PortfolioMgt.Entity.PartnerPriceDiscountStructure cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.DiscountOnMonthly = cat.DiscountOnMonthly;
                s.MonthlyTransactionAmount = cat.MonthlyTransactionAmount;
                
            }

            pRep.Edit(s);
            return s;
        }
       

        public static bool PartnerPriceDiscountStructureBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
       
        public static List<PortfolioMgt.Entity.PartnerPriceDiscountStructure> PartnerPriceDiscountStructureBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();

        }
       
        public static IQueryable<PortfolioMgt.Entity.PartnerPriceDiscountStructure> PartnerPriceDiscountStructureBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPriceDiscountStructure>();
            return pRep.GetAll();

        }
    }
}
