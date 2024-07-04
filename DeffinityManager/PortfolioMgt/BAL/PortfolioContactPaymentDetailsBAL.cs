using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class PortfolioContactPaymentDetailsBAL
    {
        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_SelectbyID(int payID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            return payRes.GetAll().Where(o => o.PayID == payID).FirstOrDefault();
        }
        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_SelectLatestByAddress(int addressID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            return payRes.GetAll().Where(o => o.AddressID == addressID).OrderByDescending(o=>o.PayID).FirstOrDefault();
        }

        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_SelectLatestByAddress(int addressID,int planID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            return payRes.GetAll().Where(o => o.AddressID == addressID && o.ProductPolicyType == planID).OrderByDescending(o => o.PayID).FirstOrDefault();
        }

        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_SelectLatestByInvoice(int invoiceID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            return payRes.GetAll().Where(o => o.InvoiceID == invoiceID).OrderByDescending(o => o.PayID).FirstOrDefault();
        }

        public static IQueryable< PortfolioMgt.Entity.PortfolioContactPaymentDetail> PortfolioContactPaymentDetailsBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            return payRes.GetAll();
        }

        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_Add(PortfolioMgt.Entity.PortfolioContactPaymentDetail p)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
             payRes.Add(p);
            return p;
        }

        public static PortfolioMgt.Entity.PortfolioContactPaymentDetail PortfolioContactPaymentDetailsBAL_Update(PortfolioMgt.Entity.PortfolioContactPaymentDetail p)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

            var po = payRes.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
            if (po != null)
            {
                po.AddressID = p.AddressID;
                po.InvoiceID = p.InvoiceID;
                po.IsPaid = p.IsPaid;
                po.Notes = p.Notes;
                po.OrderRef = p.OrderRef;
                po.PaidAmount = p.PaidAmount;
                po.PayDate = p.PayDate;
                po.PayOnWebsite = p.PayOnWebsite;
                po.PayPalRef = p.PayPalRef;
                po.Payref = p.Payref;
                po.ProductPolicyAddonIds = p.ProductPolicyAddonIds;
                po.ProductPolicyType = p.ProductPolicyType;
                payRes.Edit(po);
            }
            return po;
        }

    }
}
