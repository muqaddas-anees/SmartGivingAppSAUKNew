using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class JobTargetCommentBAL
    {
        public static DC.Entity.JobTargetComment JobTargetCommentBAL_Add(DC.Entity.JobTargetComment cat)
        {
            IDCRespository<DC.Entity.JobTargetComment> pRep = new DCRepository<DC.Entity.JobTargetComment>();


            pRep.Add(cat);
            return cat;
        }

        public static DC.Entity.JobTargetComment JobTargetCommentBAL_Update(DC.Entity.JobTargetComment mat)
        {
            IDCRespository<DC.Entity.JobTargetComment> pRep = new DCRepository<DC.Entity.JobTargetComment>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CallID = mat.CallID;
                s.Details = mat.Details;
                
            }
            pRep.Edit(s);
            return s;
        }

        public static bool JobTargetCommentBAL_delete(int id)
        {
            bool retval = false;
            IDCRespository<DC.Entity.JobTargetComment> pRep = new DCRepository<DC.Entity.JobTargetComment>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<DC.Entity.JobTargetComment> JobTargetCommentBAL_SelectAll()
        {
            IDCRespository<DC.Entity.JobTargetComment> pRep = new DCRepository<DC.Entity.JobTargetComment>();
            return pRep.GetAll();

        }

    }
}
