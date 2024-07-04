using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace PortfolioMgt.BAL
{
    
    public class PortfolioContactsTagsBAL
    {
        IPortfolioRepository<PortfolioContactsTag> pcRepository = null;

        public PortfolioContactsTagsBAL()
        {
            pcRepository = new PortfolioRepository<PortfolioContactsTag>();
        }

        public List<string> PortfolioContactsTags_SelectAll()
        {
            List<string> retval = new List<string>();
             var pcCollection = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.Tags != "").ToList(); //pcRepository.GetAll().Where(o=>o.PortfolioID ==  sessionKeys.PortfolioID);
            //var count = pcCollection.ToList().Count();
            //if count is zero insert default tags
            if (pcCollection.Count > 0)
            {
                foreach (var p in pcCollection)
                {
                    if (p.Tags.Length > 0)
                    {
                        var r = p.Tags.Replace(":", "").Replace("value", "").Replace("[", "").Replace("]", "");
                        foreach (var s in r.Split(','))
                        {
                            if (s.Trim().Length > 0)
                            {
                                if (retval.Where(o => o == s.Trim()).Count() == 0)
                                {
                                    retval.Add(s.Trim());
                                }
                            }
                        }
                    }

                }
               // PortfolioContactsTags_Add("All Donors");
                //PortfolioContactsTags_Add("Designing");
                //PortfolioContactsTags_Add("");
            }

            return retval;
        }
        public void PortfolioContactsTags_Add(PortfolioContactsTag pc)
        {
            if (pc != null)
            {
                var pcCollection = pcRepository.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID);

                //check name alredy exists
                if (pcCollection.Where(o => o.Tag.ToLower() == pc.Tag.ToLower()).Count() == 0)
                {
                    pcRepository.Add(pc);
                }
            }
        }
        public void PortfolioContactsTags_Add(string tagname)
        {
            PortfolioContactsTags_Add(new PortfolioContactsTag() { PortfolioID = sessionKeys.PortfolioID, Tag = tagname });
        }
    }
}
