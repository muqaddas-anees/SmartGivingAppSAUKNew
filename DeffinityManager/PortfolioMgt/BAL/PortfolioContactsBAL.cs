using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioContactsBAL
    {
        #region Source of lead
        public class SourceofLeadItem
        {
            public string Item { set; get; }
        }
        public static List<SourceofLeadItem> SourceofLead_Select()
        {
            List<SourceofLeadItem> li = new List<SourceofLeadItem>();
            li.Add(new SourceofLeadItem() { Item = "Event" });
            li.Add(new SourceofLeadItem() { Item = "Partner" });
            li.Add(new SourceofLeadItem() { Item = "Google Search" });
            li.Add(new SourceofLeadItem() { Item = "Inbound Call" });
            li.Add(new SourceofLeadItem() { Item = "Outbound Sales" });
            li.Add(new SourceofLeadItem() { Item = "Website" });
            li.Add(new SourceofLeadItem() { Item = "Social Media" });
            li.Add(new SourceofLeadItem() { Item = "Speaking Engagement" });
            li.Add(new SourceofLeadItem() { Item = "Referral" });
            li.Add(new SourceofLeadItem() { Item = "Past Customer" });
            li.Add(new SourceofLeadItem() { Item = "Competitor" });

            return li.OrderBy(o => o.Item).ToList();
        }

        #endregion
        public static string PorfolioContact_SelectName(int ContactID)
        {
            string name = string.Empty;
            var p = PorfolioContact_Select(ContactID);
            if (p != null)
                name = p.Name;
            return name;
        }
        public static PortfolioMgt.Entity.PortfolioContact PorfolioContact_Select(int ContactID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            return pc.GetAll().Where(o => o.ID == ContactID).FirstOrDefault();
        }
        public static List<PortfolioMgt.Entity.PortfolioContact> PorfolioContact_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            return pc.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
        }

        public static List<PortfolioMgt.Entity.PortfolioContact> PorfolioContact_SelectAll(int PortfolioID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            return pc.GetAll().Where(o => o.PortfolioID == PortfolioID).ToList();
        }

        public static bool PorfolioContact_DeleteByPortfolioID(int portfolioID)
        {
            bool retval = false;
           using(PortfolioMgt.DAL.PortfolioDataContext pd = new DAL.PortfolioDataContext())
            {
                pd.PortfolioContacts_DeleteByPortfolioID(portfolioID);
                retval = true;
            }
            return retval;
        }

        //add portfolio contact

        public static PortfolioMgt.Entity.PortfolioContact PortfolioContactsBAL_add(PortfolioMgt.Entity.PortfolioContact p)
        {
            var cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
           var  uRepository = new UserRepository<UserMgt.Entity.Contractor>();
            var pContact = new PortfolioContact();
            var pContactExist = cRepository.GetAll().Where(o => o.Email.ToLower() == p.Email.ToLower() && (o.isDisabled.HasValue ? o.isDisabled.Value : false) == false && o.PortfolioID == p.PortfolioID).FirstOrDefault();
            if (pContactExist == null)
            {
               // var pContact = new PortfolioContact();
                pContact.Name = p.Name;
                pContact.PortfolioID = p.PortfolioID;
                pContact.Email = p.Email;
                pContact.Telephone = p.Telephone;
                pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
                pContact.Mobile = p.Mobile;
                pContact.Address1 = p.Address1;
                pContact.Town = p.Town;
                pContact.City = p.City;
                pContact.Postcode = p.Postcode;
                pContact.DateLogged = DateTime.Now;
                pContact.SourceofLead = p.SourceofLead;
                pContact.Tags = p.Tags;
                //add to contact 
                cRepository.Add(pContact);
               
               
            }
            else
            {
                pContact = pContactExist;

                pContactExist.Name = p.Name;
                pContactExist.Telephone = p.Telephone;
                pContactExist.Mobile = p.Mobile;
                pContact.Address1 = p.Address1;
                pContact.Town = p.Town;
                pContact.City = p.City;
                pContact.Postcode = p.Postcode;
                pContact.Email = p.Email;

                cRepository.Edit(pContactExist);
            }

            return pContact;

        }

    }
}
