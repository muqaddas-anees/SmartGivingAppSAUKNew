using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class ProductPolicyTypeBAL
    {
        public static List<PortfolioMgt.Entity.ProductPolicyType> ProductPolicyType_Select(int CustomerID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
            return pRep.GetAll().Where(o => o.CustomerID == CustomerID).ToList();
        }

       
    }

    public class ProductAddonPriceBAL
    {
        public static List<PortfolioMgt.Entity.ProductAddonPrice> ProductAddonPrice_select(int CustomerID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
            return pRep.GetAll().Where(o => o.CustomerID == CustomerID).ToList();
        }

        public static List<PortfolioMgt.Entity.ProductAddonPrice> ProductAddonPrice_selectByPolicyType(int policyTypeID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
            return pRep.GetAll().Where(o => o.ProductPolicyTypeID == policyTypeID).ToList();
        }

    }
}
