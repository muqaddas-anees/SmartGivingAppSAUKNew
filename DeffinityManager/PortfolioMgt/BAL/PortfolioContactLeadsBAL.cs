using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioContactLeadsBAL
    {
        public static PortfolioMgt.Entity.PortfolioContactLead PortfolioContactLeadsBAL_Add(PortfolioMgt.Entity.PortfolioContactLead cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            cat.LoggedBy = sessionKeys.UID;
            cat.LoggedDate = DateTime.Now;
            cat.PortfolioID = sessionKeys.PortfolioID;
           
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.PortfolioContactLead PortfolioContactLeadsBAL_Update(PortfolioMgt.Entity.PortfolioContactLead mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            var s = pRep.GetAll().Where(o => o.LeadID == mat.LeadID).FirstOrDefault();
            if (s != null)
            {
                s.Address = mat.Address;
                s.Cell = mat.Cell;
                s.CustomerName = mat.CustomerName;
                s.Email = mat.Email;
                s.LeadDescription = mat.LeadDescription;
                s.PriceQuoted = mat.PriceQuoted;
                s.ReminderDate = mat.ReminderDate;
                
            }
            pRep.Edit(s);
            return s;
        }
      

        public static bool PortfolioContactLeadsBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            var retEntity = pRep.GetAll().Where(o => o.LeadID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static List<PortfolioMgt.Entity.PortfolioContactLead> PortfolioContactLeadsBAL_SelectByPortfolioID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }
       
        public static PortfolioMgt.Entity.PortfolioContactLead PortfolioContactLeadsBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            return pRep.GetAll().Where(o => o.LeadID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioContactLead> PortfolioContactLeadsBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLead>();
            return pRep.GetAll();

        }
    }
}
