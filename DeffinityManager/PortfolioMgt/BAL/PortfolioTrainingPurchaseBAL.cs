using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioTrainingPurchaseBAL
    {
        public static List<PortfolioMgt.Entity.PortfolioTrainingPurchase> PortfolioTrainingPurchaseBAL_select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioTrainingPurchase> pmRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioTrainingPurchase>();
            return pmRes.GetAll().ToList();

        }

        public static PortfolioMgt.Entity.PortfolioTrainingPurchase PortfolioTrainingPurchaseBAL_add(PortfolioMgt.Entity.PortfolioTrainingPurchase p)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioTrainingPurchase> pmRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioTrainingPurchase>();

            pmRes.Add(p);

            return p;
        }
    }
}
