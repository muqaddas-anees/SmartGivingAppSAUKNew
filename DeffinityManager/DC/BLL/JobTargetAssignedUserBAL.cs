using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class JobTargetAssignedUserBAL
    {
        public static DC.Entity.JobTargetAssignedUser JobTargetAssignedUserBAL_Add(DC.Entity.JobTargetAssignedUser cat)
        {
            IDCRespository<DC.Entity.JobTargetAssignedUser> pRep = new DCRepository<DC.Entity.JobTargetAssignedUser>();
            if (pRep.GetAll().Where(o => o.JobTargetID == cat.JobTargetID && o.UserID == cat.UserID).FirstOrDefault() == null)
            {
                pRep.Add(cat);
            }
            return cat;
        }

        public static DC.Entity.JobTargetAssignedUser JobTargetAssignedUserBAL_Update(DC.Entity.JobTargetAssignedUser mat)
        {
            IDCRespository<DC.Entity.JobTargetAssignedUser> pRep = new DCRepository<DC.Entity.JobTargetAssignedUser>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CallID = mat.CallID;
                s.UserID = mat.UserID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool JobTargetAssignedUserBAL_delete(int id)
        {
            bool retval = false;
            IDCRespository<DC.Entity.JobTargetAssignedUser> pRep = new DCRepository<DC.Entity.JobTargetAssignedUser>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<DC.Entity.JobTargetAssignedUser> JobTargetAssignedUserBAL_SelectAll()
        {
            IDCRespository<DC.Entity.JobTargetAssignedUser> pRep = new DCRepository<DC.Entity.JobTargetAssignedUser>();
            return pRep.GetAll();

        }



    }
}
