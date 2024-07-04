using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class JobTargetBAL
    {
        public static DC.Entity.JobTarget JobTargetBAL_Add(DC.Entity.JobTarget cat)
        {
            IDCRespository<DC.Entity.JobTarget> pRep = new DCRepository<DC.Entity.JobTarget>();


            pRep.Add(cat);
            return cat;
        }

        public static DC.Entity.JobTarget JobTargetBAL_Update(DC.Entity.JobTarget mat)
        {
            IDCRespository<DC.Entity.JobTarget> pRep = new DCRepository<DC.Entity.JobTarget>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CallID = mat.CallID;
                s.Details = mat.Details;
                s.ModifiedDate = mat.ModifiedDate;
                s.Notes = mat.Notes;
                s.Status = mat.Status;
                s.Title = mat.Title;
                
            }
            pRep.Edit(s);
            return s;
        }

        public static bool JobTargetBAL_delete(int id)
        {
            bool retval = false;
            IDCRespository<DC.Entity.JobTarget> pRep = new DCRepository<DC.Entity.JobTarget>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<DC.Entity.JobTarget> JobTargetBAL_SelectAll()
        {
            IDCRespository<DC.Entity.JobTarget> pRep = new DCRepository<DC.Entity.JobTarget>();
            return pRep.GetAll();

        }


    }
}
