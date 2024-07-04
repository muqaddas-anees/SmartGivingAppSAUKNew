using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
   public class PartnerPopupTrackBAL
    {
        public static PortfolioMgt.Entity.PartnerPopupTrack PartnerPopupTrackBAL_Update(PortfolioMgt.Entity.PartnerPopupTrack mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Popup1Closed = mat.Popup1Closed;
                s.Popup2Closed = mat.Popup2Closed;
               
            }
            pRep.Edit(s);
            return s;
        }


        public static bool PartnerPopupTrackBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static List<PortfolioMgt.Entity.PartnerPopupTrack> PartnerPopupTrackBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PartnerID).ToList();
        }
        public static PortfolioMgt.Entity.PartnerPopupTrack PartnerPopupTrackBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerPopupTrack> PartnerPopupTrackBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupTrack>();
            return pRep.GetAll();

        }

    }
}
