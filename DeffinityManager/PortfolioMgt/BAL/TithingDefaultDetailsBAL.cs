using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class TithingDefaultDetailsBAL
    {
        public static IQueryable<PortfolioMgt.Entity.TithingDefaultDetail> TithingDefaultDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
            return pRep.GetAll();
        }

        public static PortfolioMgt.Entity.TithingDefaultDetail TithingDefaultDetailsBAL_Add(PortfolioMgt.Entity.TithingDefaultDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
            var p = pRep.GetAll().Where(o => o.Title.ToLower().Trim() == t.Title.ToLower().Trim()).FirstOrDefault();
            t.CreatedDateTime = DateTime.Now;
            t.ModifiedDateTime = DateTime.Now;
            pRep.Add(t);
            //if (p == null)
            //{

            //}
            return p;
        }
        public static PortfolioMgt.Entity.TithingDefaultDetail TithingDefaultDetailsBAL_Update(PortfolioMgt.Entity.TithingDefaultDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
            var p = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if (p != null)
            {
                p.Currency = t.Currency;
                p.DefaultBanner = t.DefaultBanner;
                p.DefaultTarget = t.DefaultTarget;
                p.DefaultValues = t.DefaultValues;
                p.Description = t.Description;
                p.OrganizationID = t.OrganizationID;
                p.SendMailAfterDonation = t.SendMailAfterDonation;
                p.Title = t.Title;
                p.ModifiedDateTime = DateTime.Now;
                p.StartDate = t.StartDate;
                p.EndDate = t.EndDate;
                p.ShowChart = t.ShowChart;
                p.ShowQRCode = t.ShowQRCode;
                p.unid = t.unid;
                p.Event_unid = t.Event_unid;
                p.IsEnable = t.IsEnable;
                p.IsFundraiser = t.IsFundraiser;
                p.QRcode = t.QRcode;
                p.SocialDescription = t.SocialDescription;
                p.SocialKeywords = t.SocialKeywords;
                p.SocialTitle = t.SocialTitle;

                if (p.unid == null)
                    p.unid = Guid.NewGuid().ToString();

                p.Event_unid = t.Event_unid;

                p.FundraiserDetails = t.FundraiserDetails;
                p.EnableP2P = t.EnableP2P;
                p.ShowProgress = t.ShowProgress;
                p.Address = t.Address;
                p.City = t.City;
                p.State = t.State;
                p.Postcode = t.Postcode;
                p.Country = t.Country;

                pRep.Edit(p);
            }
            return p;
        }

        public static bool TithingDefaultDetailsBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
            var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (p != null)
            {
                pRep.Delete(p);
                retval = true;
            }
            return retval;
        }
    }
}
