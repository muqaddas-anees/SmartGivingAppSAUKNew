using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioAdminUserPopContentBAL
    {

        public static PortfolioMgt.Entity.PortfolioAdminUserPopContent PortfolioAdminUserPopContentBAL_Update(PortfolioMgt.Entity.PortfolioAdminUserPopContent mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
              
                s.Content = mat.Content;
                s.PartnerID = mat.PartnerID;

            }
            pRep.Edit(s);
            return s;
        }

        public static PortfolioMgt.Entity.PortfolioAdminUserPopContent PortfolioAdminUserPopContentBAL_AddUpdate(PortfolioMgt.Entity.PortfolioAdminUserPopContent mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            var s = pRep.GetAll().Where(o => o.PartnerID == mat.PartnerID).FirstOrDefault();
            if (s != null)
            {

                s.Content = mat.Content;
                s.PartnerID = mat.PartnerID;
                pRep.Edit(s);

            }
            else
            {
                pRep.Add(mat);
            }

            return s;
        }

        public static bool PortfolioAdminUserPopContentBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }

        public static List<PortfolioMgt.Entity.PortfolioAdminUserPopContent> PortfolioAdminUserPopContentBAL_SelectByPartnerID()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            return pRep.GetAll().Where(o => o.PartnerID == sessionKeys.PartnerID).ToList();
        }
        public static List<PortfolioMgt.Entity.PortfolioAdminUserPopContent> PortfolioAdminUserPopContentBAL_SelectByPartnerID(int partnerid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            return pRep.GetAll().Where(o => o.PartnerID == partnerid).ToList();
        }
        public static PortfolioMgt.Entity.PortfolioAdminUserPopContent PortfolioAdminUserPopContentBAL_Select(int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioAdminUserPopContent> PortfolioAdminUserPopContentBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioAdminUserPopContent>();
            return pRep.GetAll();

        }


    }
}
