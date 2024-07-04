using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
     
    public class PortfolioContactBAL
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> iContactRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> iPorfolioRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> iPorfolioAddressRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.V_PortfolioContact> ivPorfolioRepository = null;

        public PortfolioContactBAL()
        {
            iContactRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            iPorfolioRepository = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            iPorfolioAddressRepository = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();
            ivPorfolioRepository = new PortfolioRepository<PortfolioMgt.Entity.V_PortfolioContact>();


        }
        //Select All
        public IEnumerable<PortfolioMgt.Entity.PortfolioContact> PortfolioContact_SelectAll()
        {
            return iContactRepository.GetAll();
        }
        //Select All active
        public IEnumerable<PortfolioMgt.Entity.PortfolioContact> PortfolioContact_SelectByID(int ContactID)
        {
            return PortfolioContact_SelectAll().Where(o => o.ID == ContactID);
        }
        //public PortfolioMgt.Entity.v_PortfolioContactAddress PortfolioContactAddress_SelectByID(int AddressID)
        //{
        //    return iPorfolioAddressRepository.GetAll().Where(o => o.AddressID == AddressID).FirstOrDefault();
        //}
        public List<PortfolioMgt.Entity.v_PortfolioContactAddress> v_PortfolioContactAddress_SelectByID(int AddressID)
        {
            return iPorfolioAddressRepository.GetAll().Where(o => o.AddressID == AddressID).ToList();
        }

        public IQueryable<PortfolioMgt.Entity.V_PortfolioContact> V_PortfolioContact_SelectAll()
        {
            return ivPorfolioRepository.GetAll();
        }
        public PortfolioMgt.Entity.V_PortfolioContact V_PortfolioContact_SelectByID(int ContactID)
        {
            return ivPorfolioRepository.GetAll().Where(o => o.ID == ContactID).FirstOrDefault();
        }

        public PortfolioMgt.Entity.PortfolioContact PortfolioContact_update(PortfolioMgt.Entity.PortfolioContact c)
        {
            var v= iContactRepository.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if (v != null)
            {
                v.Telephone = c.Telephone;
                v.Address1 = c.Address1;
                v.Address2 = c.Address2;
                v.BuildingName = c.BuildingName;
                v.City = c.City;
                v.Country = c.Country;
                v.County = c.County;
                v.Email = c.Email;
                v.isDisabled = c.isDisabled;
                v.Location = c.Location;
                v.LogintoPortal = c.LogintoPortal;
                v.Mobile = c.Mobile;
                v.Name = c.Name;
                v.Notes = c.Notes;
                v.Postcode = c.Postcode;
                v.Town = c.Town;
                v.Title = c.Title;
                v.Tags = c.Tags;
                v.SourceofLead = c.SourceofLead;

                iContactRepository.Edit(c);
            }

            return c;
        }
    }
}