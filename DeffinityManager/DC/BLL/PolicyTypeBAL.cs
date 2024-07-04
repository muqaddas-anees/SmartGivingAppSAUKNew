using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace DC.BAL
{
   
    public class PolicyTypeBAL
    {
        IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ppRepository = null;

        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paRepository = null;

        public PolicyTypeBAL()
        {
            ppRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
            paRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
        }

        public PortfolioMgt.Entity.ProductPolicyType  PolicyType_Add(ProductPolicyType pt)
        {
            pt.CustomerID = sessionKeys.PortfolioID;
            ppRepository.Add(pt);
            return pt;
        }
        public PortfolioMgt.Entity.ProductPolicyType PolicyType_Edit(ProductPolicyType pt)
        {
            ppRepository.Edit(pt);
            return pt;
        }

        public PortfolioMgt.Entity.ProductPolicyType PolicyType_SelectBYID(int id)
        {
            return ppRepository.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        public bool PolicyType_DeleteBYID(int id)
        {
            bool retval = false;
            var v = PolicyType_SelectBYID(id);
            if(v!= null)
            {
                //update policy link with address list before delete policy
                var elist = paRepository.GetAll().Where(o => o.PolicyTypeID == v.ID).ToList();
                if(elist.Count >0)
                {
                    foreach(var a in elist)
                    {
                        a.PolicyTypeID = 0;
                        paRepository.Edit(a);
                    }
                }
                ppRepository.Delete(v);
                retval = true;
            }
            return retval;
        }
        public List<ProductPolicyType> PolicyType_SelectAll()
        {
            return ppRepository.GetAll().Where(o=>o.CustomerID == sessionKeys.PortfolioID).ToList();
        }

        public bool PolicyType_IsExists(string title)
        {
            if (ppRepository.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).Where(o => o.Title.ToLower() == title.ToLower()).Count() > 0)
                return true;
            else
                return false;
        }
        public bool PolicyType_IsExistsOnUpdate(string title, int id)
        {
            if (ppRepository.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).Where(o => o.Title.ToLower() == title.ToLower() && o.ID != id).Count() > 0)
                return true;
            else
                return false;
        }
    }
}
