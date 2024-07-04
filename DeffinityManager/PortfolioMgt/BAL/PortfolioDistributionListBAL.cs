using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace DeffinityManager.PortfolioMgt.BAL
{
    public class PortfolioDistributionListBAL
    {
        public static List<PortfolioDistributionList> PortfolioDistributionListBAL_SelectAll()
        {
            bool retval = false;
            IPortfolioRepository<PortfolioDistributionList> pRep = new PortfolioRepository<PortfolioDistributionList>();
            //check the user
            var plist = pRep.GetAll().ToList();
            

            return plist;
        }

        public static bool PortfolioDistributionListBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioDistributionList> pRep = new PortfolioRepository<PortfolioDistributionList>();
            //check the user
            var p = pRep.GetAll().Where(o=>o.ID == id).FirstOrDefault();
            if (p != null)
            {
                pRep.Delete(p);
                retval = true;
            }


            return retval;
        }
        //add user
        public static bool PortfolioDistributionListBAL_Add(string name, string email)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioDistributionList> pRep = new PortfolioRepository<PortfolioDistributionList>();
            var p = pRep.GetAll().Where(o => o.Name.ToLower() == name.ToLower().Trim()&& o.EmailAddress.ToLower() == email.ToLower().Trim()).FirstOrDefault();
            if (p == null)
            {
                p = new PortfolioDistributionList();
                p.EmailAddress = email.Trim();
                p.Name  = name.Trim();
                p.BaseURL = "us.123smartpro.com";
                pRep.Add(p);
                retval = true;
            }

            return retval;
        }

        public static bool PortfolioDistributionListBAL_Update(int id,string name, string email)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioDistributionList> pRep = new PortfolioRepository<PortfolioDistributionList>();
            var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (p != null)
            {
                p.Name = name;
                p.EmailAddress = email;
                pRep.Edit(p);
            }

            return retval;
        }

    }
}
