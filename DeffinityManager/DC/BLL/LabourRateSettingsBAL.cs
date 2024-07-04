using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Entity;

namespace DC.BLL
{
    public class LabourRateSettingBAL
    {
        public static LabourRateSetting LabourRateSettingBAL_Update(double costrate,double sellrate, int portfolioID)
        {
            IDCRespository<LabourRateSetting> pRep = new DCRepository<LabourRateSetting>();
            var s = pRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
            if (s == null)
            {
                s = new LabourRateSetting();
                s.PortfolioID = portfolioID;
                s.SellRate = sellrate;
                s.CostRate = costrate;
                pRep.Add(s);
            }
            else
            {
                s.CostRate = costrate;
                s.SellRate = sellrate;
                pRep.Edit(s);
            }
            return s;
        }

        public static LabourRateSetting LabourRateSettingBAL_Select(int portfolioID)
        {
            IPortfolioRepository<LabourRateSetting> pRep = new PortfolioRepository<LabourRateSetting>();
            return pRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

        }
    }
}
