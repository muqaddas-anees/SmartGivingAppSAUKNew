using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class JobTargetsDocBAL
    {
        public static DC.Entity.JobTargetsDoc JobTargetsDocBAL_Add(DC.Entity.JobTargetsDoc cat)
        {
            IDCRespository<DC.Entity.JobTargetsDoc> pRep = new DCRepository<DC.Entity.JobTargetsDoc>();


            pRep.Add(cat);
            return cat;
        }

        public static DC.Entity.JobTargetsDoc JobTargetsDocBAL_Update(DC.Entity.JobTargetsDoc mat)
        {
            IDCRespository<DC.Entity.JobTargetsDoc> pRep = new DCRepository<DC.Entity.JobTargetsDoc>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CallID = mat.CallID;
                s.ContentType = mat.ContentType;
                s.FileName = mat.FileName;                

            }
            pRep.Edit(s);
            return s;
        }

        public static bool JobTargetsDocBAL_delete(int id)
        {
            bool retval = false;
            IDCRespository<DC.Entity.JobTargetsDoc> pRep = new DCRepository<DC.Entity.JobTargetsDoc>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<DC.Entity.JobTargetsDoc> JobTargetsDocBAL_SelectAll()
        {
            IDCRespository<DC.Entity.JobTargetsDoc> pRep = new DCRepository<DC.Entity.JobTargetsDoc>();
            return pRep.GetAll();

        }

    }
}
