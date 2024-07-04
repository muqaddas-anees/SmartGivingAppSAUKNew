using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerTermsandConditionBAL
    {

        public static PortfolioMgt.Entity.PartnerTermsandCondition PartnerTermsandConditionBAL_Add(PortfolioMgt.Entity.PartnerTermsandCondition cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            cat.PartnerID = sessionKeys.PartnerID;
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PartnerTermsandCondition PartnerTermsandConditionBAL_Update(PortfolioMgt.Entity.PartnerTermsandCondition mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.TermsandConditions = mat.TermsandConditions;

            }
            pRep.Edit(s);
            return s;
        }

        public static string PartnerTermsandConditionBAL_SelectByPortfolioID()
        {
            string retval = string.Empty;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            var p = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            if (p != null)
                retval = p.TermsandConditions;
           

            return retval;

        }
        public static string PartnerTermsandConditionBAL_SelectByPartnerID()
        {
            string retval = string.Empty;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            var p = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PartnerID).FirstOrDefault();
            if (p != null)
                retval = p.TermsandConditions;

            return retval;
        }
        public static PortfolioMgt.Entity.PartnerTermsandCondition PartnerTermsandConditionBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerTermsandCondition> PartnerTermsandConditionBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerTermsandCondition>();
            return pRep.GetAll();

        }

    }
}
