using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PortfolioMgt.BAL
{
    public class PortolioMarginBAL
    {
        public const double defval = 25;

        public static void PortolioMarginBAL_SetDefault()
        {
            PortolioMargin m;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            m = mRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            if (m == null)
            {
                m = new PortolioMargin();
                m.PortfolioID = sessionKeys.PortfolioID;
                m.Margin = defval;
                mRep.Add(m);
            }

        }
        public static PortolioMargin PortolioMarginBAL_Update(int portfolioID, double margin)
        {
            PortolioMargin m;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            m = mRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
            if (m == null)
            {
                m = new PortolioMargin();
                m.PortfolioID = portfolioID;
                m.Margin = margin;
                mRep.Add(m);
            }
            else
            {
                m.Margin = margin;
                mRep.Edit(m);
            }

            return m;

        }
        public static PortolioMargin PortolioMarginBAL_Select(int portfolioID)
        {
            PortolioMargin m;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            m = mRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
            return m;
        }
        public static PortolioMargin PortolioMarginBAL_Select()
        {
            PortolioMargin m;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            m = mRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            return m;
        }
        public static double PortolioMarginBAL_SelectMargin(int portfolioID)
        {
            double m = 0;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            var mdata = mRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();
            if (mdata != null)
                m = mdata.Margin;
            return m;
        }
        public static double PortolioMarginBAL_SelectMargin()
        {
            double m = 0;
            IPortfolioRepository<PortolioMargin> mRep = new PortfolioRepository<PortolioMargin>();
            var mdata = mRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            if (mdata != null)
                m = mdata.Margin;
            return m;
        }
    }
}
