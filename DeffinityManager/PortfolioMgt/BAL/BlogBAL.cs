using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class BlogBAL
    {

        public static PortfolioMgt.Entity.tblBlog BlogBAL_Add(PortfolioMgt.Entity.tblBlog cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.tblBlog> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblBlog>();
            cat.LoggedBy = sessionKeys.UID;
            cat.LoogedDatetime = DateTime.Now;
            cat.BlogRef = Guid.NewGuid().ToString();
            
            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.tblBlog BlogBAL_Update(PortfolioMgt.Entity.tblBlog mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.tblBlog> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblBlog>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.LoogedDatetime = mat.LoogedDatetime;
                s.LoggedBy = mat.LoggedBy;
                s.BlogContent = mat.BlogContent;
                s.BlogRef = mat.BlogRef;
                s.BlogTitle = mat.BlogTitle;
                s.IsActive = mat.IsActive;
                s.Button1Link = mat.Button1Link;
                s.Button1Title = mat.Button1Title;
                s.Button2Link = mat.Button2Link;
                s.Button2Title = mat.Button2Title;
                s.Notes = mat.Notes;
                s.StartDate = mat.StartDate;
                s.EndDate = mat.EndDate;
                s.Position = mat.Position;
                
            }
            pRep.Edit(s);
            return s;
        }
       
        public static bool BlogBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.tblBlog> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblBlog>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.tblBlog> PBlogBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.tblBlog> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblBlog>();
            return pRep.GetAll();

        }
    }
}
