using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class FaithEducationDetailBAL
    {
        public static PortfolioMgt.Entity.FaithEducationDetail FaithEducationDetailBAL_Add(PortfolioMgt.Entity.FaithEducationDetail cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail>();


            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.FaithEducationDetail FaithEducationDetailBAL_Update(PortfolioMgt.Entity.FaithEducationDetail t)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail>();
            var s = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if (s != null)
            {
                s.DateLogged = t.DateLogged;
                s.DateLogged = DateTime.Now;
                s.DenominationDetailsID = t.DenominationDetailsID;
                s.SubDenominationDetailsID = t.SubDenominationDetailsID;
                s.Title = t.Title;
                s.VideoUrl = t.VideoUrl;
                s.CategoryID = t.CategoryID;
                
            }
            pRep.Edit(s);
            return s;
        }

        public static bool FaithEducationDetailBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.FaithEducationDetail> FaithEducationDetailBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.FaithEducationDetail>();
            return pRep.GetAll();

        }


    }
}
