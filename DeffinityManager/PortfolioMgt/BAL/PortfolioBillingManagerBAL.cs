using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioBillingManagerBAL
    {

        public static bool ShowUpgradeOption()
        {
            var retval = false;
            var ptdate = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(sessionKeys.PartnerID);
            if (ptdate != null)
            {

                if (ptdate.TrailDays.HasValue)
                {
                    if (ptdate.TrailDays.Value > 0)
                    {
                        var mtdata = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.IsActive == true).FirstOrDefault();
                        if (mtdata != null)
                        {
                            if (mtdata.IsPaid.HasValue)
                            {
                                if (mtdata.IsPaid.Value)
                                    retval = false;
                                else
                                    retval = true;
                            }
                            else
                                retval = true;

                        }
                        else
                            retval = true;
                    }

                }
            }

            return retval;

        }


       
        public static PortfolioMgt.Entity.PortfolioBillingManager   PortfolioBillingManagerBAL_Add(PortfolioMgt.Entity.PortfolioBillingManager  cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager > pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager >();

            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PortfolioBillingManager   PortfolioBillingManagerBAL_Update(PortfolioMgt.Entity.PortfolioBillingManager  cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager > pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager >();
            var s = pRep.GetAll().Where(o => o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Amount = cat.Amount;
                s.AmountPerUser = cat.AmountPerUser;
                s.BillingStatus = cat.BillingStatus;
                s.IsPaid = cat.IsPaid;
                s.NumberofUsers = cat.NumberofUsers;
                s.PaymentTerm = cat.PaymentTerm;
                s.PlanID = cat.PlanID;
                s.SubscriptionCancelDate = cat.SubscriptionCancelDate;
                s.SubscriptionStartDate = cat.SubscriptionStartDate;
                s.CancellationReason = cat.CancellationReason;
                s.IsActive = cat.IsActive;
                s.TrailDays = cat.TrailDays;
                s.TrailEndDate = cat.TrailEndDate;
                s.TrailStartDate = cat.TrailStartDate;
                
            }

            pRep.Edit(s);
            return s;
        }


        public static bool  PortfolioBillingManagerBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager > pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager >();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static PortfolioMgt.Entity.PortfolioBillingManager   PortfolioBillingManagerBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager > pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager >();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioBillingManager >  PortfolioBillingManagerBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager > pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBillingManager >();
            return pRep.GetAll();

        }
    }
}
