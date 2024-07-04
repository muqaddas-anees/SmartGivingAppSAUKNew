using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class TithingAmountBAL
    {
        public static PortfolioMgt.Entity.TithingAmount TithingAmountBAL_Add(PortfolioMgt.Entity.TithingAmount cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingAmount> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingAmount>();
          
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.TithingAmount TithingAmountBAL_Update(PortfolioMgt.Entity.TithingAmount cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingAmount> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingAmount>();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
          if(s != null)
            {
                s.Amount = cat.Amount;
                s.Description = cat.Description;
                s.Shortdescription = cat.Shortdescription;
                s.TithingDefaultUnid = cat.TithingDefaultUnid;
            }

            pRep.Edit(s);
            return s;
        }
      
        public static bool TithingAmountBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.TithingAmount> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingAmount>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
       
        public static PortfolioMgt.Entity.TithingAmount TithingAmountBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingAmount> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingAmount>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.TithingAmount> TithingAmountBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingAmount> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingAmount>();
            return pRep.GetAll();

        }
    }
}
