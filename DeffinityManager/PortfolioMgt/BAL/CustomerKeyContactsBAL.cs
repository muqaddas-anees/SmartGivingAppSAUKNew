using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class CustomerKeyContactsBAL
    {
        public static List<CustomerKeyContact> CustomerKeyContact_SelectAll(int ContactID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact>();
            var mlist = mRep.GetAll().Where(o => o.ContactID == ContactID).ToList();

            return mlist.ToList();
        }
        public static CustomerKeyContact CustomerKeyContact_Select(int ID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact>();
            var mlist = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();

            return mlist;
        }

        public static CustomerKeyContact CustomerKeyContact_Add(CustomerKeyContact c)
        {
            IPortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact>();
            mRep.Add(c);
            return c;
        }

        public static CustomerKeyContact CustomerKeyContact_Update(CustomerKeyContact c)
        {
            IPortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact>();

            var a = mRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if (a != null)
            {
                a.JobTitle = c.JobTitle;
                a.MobileNo = c.MobileNo;
                a.Name = c.Name;
                a.TelephoneNo = c.TelephoneNo;
                a.EmailAddress = c.EmailAddress;
            }


            mRep.Edit(a);
            return c;
        }

        public static bool CustomerKeyContact_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.CustomerKeyContact>();
            var eEntity = mRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (eEntity != null)
            {
                mRep.Delete(eEntity);
                retval = true;
            }

            return retval;

        }

        public static bool CustomerKeyContact_AssociateAddress_addUpdate(int KeyConcatid, int Addressid)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact>();
            var eEntity = mRep.GetAll().Where(o => o.AddressID == Addressid).FirstOrDefault();
            if (eEntity != null)
            {
                eEntity.KeyContactID = KeyConcatid;
                mRep.Edit(eEntity);
            }
            else
            {
                eEntity = new ContactAddressAssoiciateKeyContact();
                eEntity.AddressID = Addressid;
                eEntity.KeyContactID = KeyConcatid;
                mRep.Add(eEntity);

            }

            return retval;

        }

        public static bool CustomerKeyContact_AssociateAddress_Delete(int Addressid)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact>();
            var eEntity = mRep.GetAll().Where(o => o.AddressID == Addressid).FirstOrDefault();
            if (eEntity != null)
            {

                mRep.Delete(eEntity);
                retval = true;
            }
            return retval;
        }

        public static int CustomerKeyContact_AssociateAddress_select(int Addressid)
        {
            int retval = 0;
            IPortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact> mRep = new PortfolioRepository<PortfolioMgt.Entity.ContactAddressAssoiciateKeyContact>();
            var eEntity = mRep.GetAll().Where(o => o.AddressID == Addressid).FirstOrDefault();
            if (eEntity != null)
            {
                retval = eEntity.KeyContactID.HasValue ? eEntity.KeyContactID.Value : 0;

            }
            return retval;

        }
    }
    }
