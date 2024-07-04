using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class PartnerDetailBAL
    {

        public static PartnerDetail PartnerDetailBAL_Add(PartnerDetail p)
        {
            IPortfolioRepository<PartnerDetail> pRep = new PortfolioRepository<PartnerDetail>();
            pRep.Add(p);
            return p;
        }

        public static PartnerDetail PartnerDetailBAL_Update(PartnerDetail p)
        {
            IPortfolioRepository<PartnerDetail> pRep = new PortfolioRepository<PartnerDetail>();
            var s = pRep.GetAll().Where(o=>o.ID == p.ID).FirstOrDefault();
            if (s != null)
            {
                s.PartnerName = p.PartnerName;
                s.IsActive = p.IsActive;
                s.PartnerWebSite = p.PartnerWebSite;
                s.ParnerPortal = p.ParnerPortal;
                if (p.Logo != null)
                {
                    s.Logo = p.Logo;
                    s.LogoContentType = p.LogoContentType;
                }
                s.Theme = p.Theme;
                s.Portalname = p.Portalname;
                s.SupportEmail = p.SupportEmail;
                s.FromEmail = p.FromEmail;
                s.TrailDays = p.TrailDays;
                s.BackgroundImage = p.BackgroundImage;
                s.TrackerUrl = p.TrackerUrl;
                s.EcoSystem = p.EcoSystem;
                pRep.Edit(s);
            }

          
            return s;
        }
        public static bool PartnerDetailBAL_IsExists(string partnerName)
        {
            IPortfolioRepository<PartnerDetail> pRep = new PortfolioRepository<PartnerDetail>();
            var retEntity = pRep.GetAll().Where(o => o.PartnerName.ToLower().Trim() == partnerName.ToLower().Trim()).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }
      
        public static PartnerDetail PartnerDetailBAL_Select(int id)
        {
            IPortfolioRepository<PartnerDetail> pRep = new PortfolioRepository<PartnerDetail>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        public static List<PartnerDetail> PartnerDetailBAL_SelectAll()
        {
            IPortfolioRepository<PartnerDetail> pRep = new PortfolioRepository<PartnerDetail>();
            return pRep.GetAll().ToList();

        }


    }
}
