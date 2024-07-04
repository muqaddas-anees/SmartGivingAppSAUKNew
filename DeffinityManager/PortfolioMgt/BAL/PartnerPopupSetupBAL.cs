using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PartnerPopupSetupBAL
    {

        public static PortfolioMgt.Entity.PartnerPopupSetup PartnerPopupSetupBAL_Update(PortfolioMgt.Entity.PartnerPopupSetup mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.ButtonColor = mat.ButtonColor;
                s.LinkUrl = mat.LinkUrl;
                s.Popup1Time = mat.Popup1Time;
                s.Popup2Time = mat.Popup2Time;
                s.PopupContent = mat.PopupContent;
               
            }
            pRep.Edit(s);
            return s;
        }

        public static PortfolioMgt.Entity.PartnerPopupSetup PartnerPopupSetupBAL_AddUpdate(PortfolioMgt.Entity.PartnerPopupSetup mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            var s = pRep.GetAll().Where(o => o.PartnerID == mat.PartnerID).FirstOrDefault();
            if (s != null)
            {
                s.ButtonColor = mat.ButtonColor;
                s.LinkUrl = mat.LinkUrl;
                s.Popup1Time = mat.Popup1Time;
                s.Popup2Time = mat.Popup2Time;
                s.PopupContent = mat.PopupContent;
                pRep.Edit(s);

            }
            else
            {
                pRep.Add(mat);
            }
            
            return s;
        }

        public static bool PartnerPopupSetupBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
      
        public static List<PortfolioMgt.Entity.PartnerPopupSetup> PartnerPopupSetupBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();
        }
        public static PortfolioMgt.Entity.PartnerPopupSetup PartnerPopupSetupBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PartnerPopupSetup> PartnerPopupSetupBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup> pRep = new PortfolioRepository<PortfolioMgt.Entity.PartnerPopupSetup>();
            return pRep.GetAll();

        }



    }
}
