using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PortfolioMgt.BAL
{
    public static class PaymentTerm
    {
        public static string Monthly = "Monthly";
        public static string Yearly = "Yearly";
    }
    public class PortolioBillingBAL
    {
        public static PortfolioMgt.Entity.PortfolioBilling PortolioBillingBAL_Add(PortfolioMgt.Entity.PortfolioBilling p)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
            pRep.Add(p);
            return p;

        }
        public static PortfolioMgt.Entity.PortfolioBilling PortolioBillingBAL_Update(PortfolioMgt.Entity.PortfolioBilling pb)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
            var p = pRep.GetAll().Where(o => o.ID == pb.ID).FirstOrDefault();
            if(p != null)
            {
                p.Amount = pb.Amount;
                p.Currency = pb.Currency;
                p.InvoiceSetDate = pb.InvoiceSetDate;
                p.IsPaid = pb.IsPaid;
                p.MonthlyPaymentDate = pb.MonthlyPaymentDate;
                p.Notes = pb.Notes;
                p.NumberofUsers = pb.NumberofUsers;
                p.PaymentDate = pb.PaymentDate;
                p.PaymentProfile = pb.PaymentProfile;
                p.PaymentReference = pb.PaymentReference;
                p.PortfolioID = pb.PortfolioID;
                p.SendInvoice = pb.SendInvoice;
                p.TransationEndDate = pb.TransationEndDate;
                p.TransationStartDate = pb.TransationStartDate;
                p.PaymentTerm = pb.PaymentTerm;
                p.PlanID = pb.PlanID;
                pRep.Edit(p);
            }
            
            return p;
        }
        public static PortfolioMgt.Entity.PortfolioBilling PortolioBillingBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        //public static List<PortfolioMgt.Entity.PortfolioBilling> PortolioBillingBAL_SelectAll()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
        //    return pRep.GetAll().ToList();  
        //}
        public static IQueryable<PortfolioMgt.Entity.PortfolioBilling> PortolioBillingBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
            return pRep.GetAll();
        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioBilling> PortolioBillingBAL_SelectByPortfolioID(int PortfolioID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioBilling> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
            return pRep.GetAll().Where(o=>o.PortfolioID == PortfolioID);
        }
    }
}
