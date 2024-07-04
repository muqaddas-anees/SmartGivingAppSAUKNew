using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Entity;

namespace DC.BLL
{
    public class QuotationOptionsBAL
    {

        public static QuotationOption QuotationOption_Add(QuotationOption q)
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            if (qiRep.GetAll().Where(o => o.CallID == q.CallID && o.OptionName == q.OptionName).FirstOrDefault() == null)
            {
                q.LoggedDate = DateTime.Now;
                q.ModifiedDate = DateTime.Now;

                qiRep.Add(q);
            }
            else
            {
                q.ID = 0;
            }
            return q;
        }
        public static QuotationOption QuotationOption_Update(QuotationOption q)
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            var qEntity = qiRep.GetAll().Where(o => o.ID == q.ID  ).FirstOrDefault();
            if (qEntity != null)
            {
                qEntity.IsActive = q.IsActive;
                qEntity.ModifiedDate = DateTime.Now;
                qEntity.OptionName = q.OptionName;
                qEntity.Description = q.Description;
                qiRep.Edit(qEntity);
            }
            return qEntity;
        }
        public static QuotationOption QuotationOption_SelectByID(int ID)
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            return qiRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        public static bool QuotationOption_DeleteByID(int ID)
        {
            bool retval = false;
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            var v = qiRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (v != null)
            {
                qiRep.Delete(v);
                retval = true;
            }
            return retval;

        }
        public static List<QuotationOption> QuotationOption_Select(int CallID)
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            return qiRep.GetAll().Where(o => o.CallID == CallID).ToList();
        }
        public static List<QuotationOption> QuotationOption_SelectAll(int portfolioid)
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            return qiRep.GetAll().Where(o => o.CustomerID == portfolioid).ToList();
        }
        public static List<QuotationOption> QuotationOption_SelectAll()
        {
            IDCRespository<QuotationOption> qiRep = new DCRepository<QuotationOption>();
            return qiRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).ToList();
        }
    }
}
