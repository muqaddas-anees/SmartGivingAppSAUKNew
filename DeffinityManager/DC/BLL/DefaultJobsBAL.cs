using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class DefaultJobsBAL
    {

        public static List<DC.Entity.DefaultJob> DefaultJobsBAL_select(int sectorid)
        {
            IDCRespository<DC.Entity.DefaultJob> drep = new DCRepository<DC.Entity.DefaultJob>();
            return drep.GetAll().Where(o => o.SectorID == sectorid).ToList();

        }
        public static DC.Entity.DefaultJob DefaultJobsBAL_add(int sectorid,string Description)
        {
            IDCRespository<DC.Entity.DefaultJob> drep = new DCRepository<DC.Entity.DefaultJob>();
            var d = new DC.Entity.DefaultJob();
            d.SectorID = sectorid;
            d.JobDescription = Description;
            drep.Add(d);
            return d;

        }
        public static DC.Entity.DefaultJob DefaultJobsBAL_update(int id, string Description)
        {
            IDCRespository<DC.Entity.DefaultJob> drep = new DCRepository<DC.Entity.DefaultJob>();

            var d = drep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if(d != null)
            {
                d.JobDescription = Description;
                drep.Edit(d);
            }
            return d;

        }

        public static bool DefaultJobsBAL_delete(int id)
        {
            IDCRespository<DC.Entity.DefaultJob> drep = new DCRepository<DC.Entity.DefaultJob>();
            bool retval = false;
            var d = drep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (d != null)
            {
                drep.Delete(d);
                retval = true;
            }
            return retval;

        }
    }
}
