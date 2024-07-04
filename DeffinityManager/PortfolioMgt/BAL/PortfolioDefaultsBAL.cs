using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;


namespace DeffinityManager.Portfolio.BAL
{
    public class PortfolioDefaultsBAL
    {
      
        public static PortfolioDefault PortfolioDefaultsBAL_AddUpdate(double montlyPrice, string currency)
        {
            IPortfolioRepository<PortfolioDefault> pdRes = new PortfolioRepository<PortfolioDefault>();
            var p = pdRes.GetAll().FirstOrDefault();
            if (p == null)
            {
                p = new PortfolioDefault();
                p.Currency = currency;
                p.MonthlyPrice = montlyPrice;
                pdRes.Add(p);
            }
            else
            {
                p.Currency = currency;
                p.MonthlyPrice = montlyPrice;
                pdRes.Edit(p);
            }

            return p;

        }

        public static PortfolioDefault PortfolioDefaultsBAL_AddUpdateVAT(double vat)
        {
            IPortfolioRepository<PortfolioDefault> pdRes = new PortfolioRepository<PortfolioDefault>();
            var p = pdRes.GetAll().FirstOrDefault();
           
            if (p != null)
            {
                p.VAT = vat;
                pdRes.Edit(p);
            }

            return p;

        }

      

        public static PortfolioDefault PortfolioDefaultsBAL_Select()
        {
            IPortfolioRepository<PortfolioDefault> pdRes = new PortfolioRepository<PortfolioDefault>();
            return pdRes.GetAll().FirstOrDefault();
        }


        public static double PortfolioDefaultsBAL_SelectVAT()
        {
            double retval = 0.00;
            IPortfolioRepository<PortfolioDefault> pdRes = new PortfolioRepository<PortfolioDefault>();
            var p = pdRes.GetAll().FirstOrDefault();
            if (p != null)
            {
                if (p.VAT.HasValue)
                    retval = p.VAT.Value;
            }

            return retval;
        }

    }
}
