using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class MailDataProcessBAL
    {
        public static MailDataProcess MailDataProcess_Add(MailDataProcess q)
        {
            IDCRespository<MailDataProcess> qiRep = new DCRepository<MailDataProcess>();
            if (qiRep.GetAll().Where(o => o.UQID == q.UQID).FirstOrDefault() == null)
            {
            
                qiRep.Add(q);
            }
            else
            {
                q.ID = 0;
            }
            return q;
        }
        public static MailDataProcess MailDataProcess_Update(MailDataProcess q)
        {
            IDCRespository<MailDataProcess> qiRep = new DCRepository<MailDataProcess>();
            var qEntity = qiRep.GetAll().Where(o => o.ID == q.ID).FirstOrDefault();
            if (qEntity != null)
            {
                qEntity.UQID = q.UQID;
                qEntity.UQValues = q.UQValues;
                qEntity.IsActive = q.IsActive;
                qiRep.Edit(qEntity);
            }
            return qEntity;
        }
        public static MailDataProcess MailDataProcess_SelectByID(int ID)
        {
            IDCRespository<MailDataProcess> qiRep = new DCRepository<MailDataProcess>();
            return qiRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        public static MailDataProcess MailDataProcess_SelectByUQID(string uqid)
        {
            IDCRespository<MailDataProcess> qiRep = new DCRepository<MailDataProcess>();
            return qiRep.GetAll().Where(o => o.UQID == uqid).FirstOrDefault();
        }
        public static bool MailDataProcess_DeleteByID(int ID)
        {
            bool retval = false;
            IDCRespository<MailDataProcess> qiRep = new DCRepository<MailDataProcess>();
            var v = qiRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (v != null)
            {
                qiRep.Delete(v);
                retval = true;
            }
            return retval;

        }
    }
}
