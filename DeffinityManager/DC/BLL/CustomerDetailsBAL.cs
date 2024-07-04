using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
namespace DC.BLL
{
    /// <summary>
    /// Summary description for CustomerDetailsBAL
    /// </summary>
    public class CustomerDetailsBAL
    {
        #region Get Contractor Details by ID
        public static UserMgt.Entity.Contractor GetContractorDetailsbyID(int id)
        {
            using (UserDataContext ud = new UserDataContext())
            {
               UserMgt.Entity.Contractor c = ud.Contractors.Where(cn => cn.ID == id).Select(cn => cn).FirstOrDefault();
               return c;
            }
        }
        #endregion

        #region Get Portfolio Contact Details by ID
        public static PortfolioContact GetPortfolioContactDetailsbyID(int id)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                PortfolioContact p = pd.PortfolioContacts.Where(pc => pc.ID == id).Select(pc => pc).FirstOrDefault();
                return p;
            }
        }
        #endregion

        #region Check Portfolio Contact Exists by Name and Email
        public static int CheckExists(string name, string email,int pid)
        {
            int id= 0;
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                PortfolioContact pc = pd.PortfolioContacts.Where(p => p.Name.ToLower().Trim() == name.ToLower().Trim() && p.Email.ToLower().Trim() == email.ToLower().Trim() && p.PortfolioID==pid).Select(p => p).FirstOrDefault();
                if (pc != null)
                    id = pc.ID;
                return id;
            }
        }
        #endregion

        #region Insert PortfolioContact
        public static int InsertPortfolioContact(string name, string email,string telephone, int? PortfolioID)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                PortfolioContact pc = new PortfolioContact();
                pc.PortfolioID = PortfolioID;
                pc.Name = name;
                pc.Email = email;
                pc.Telephone = telephone;
                pc.Key_Contact = false;
                pc.DateOfBirth = Convert.ToDateTime("01/01/1900");
                pd.PortfolioContacts.InsertOnSubmit(pc);
                pd.SubmitChanges();
                return pc.ID;
            }
        }
        //Update the Email and telephone number
        public static void Update_PortfolioContact(int ContactID, string email, string telephone)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                PortfolioContact pc = pd.PortfolioContacts.Where(p=>p.ID == ContactID).FirstOrDefault();
                pc.ID = ContactID;
                pc.Telephone = telephone;
                pc.Email = email;
                pd.SubmitChanges();
            }
        }
        //update the Email and telphone number
        public static void Update_ContactorDetails(int id, string email, string telephone)
        {
            using (UserDataContext ud = new UserDataContext())
            {
                UserMgt.Entity.Contractor cn = ud.Contractors.Where(p => p.ID == id && p.SID == 7).FirstOrDefault();
                if (cn != null)
                {
                    cn.EmailAddress = email;
                    cn.ContactNumber = telephone;
                    ud.SubmitChanges();
                }
            }
        }

        public static void Update_ProfileDetails(int ContactID, int UserID, string email, string telephone)
        { 
            Update_PortfolioContact(ContactID, email, telephone);
            Update_ContactorDetails(UserID, email, telephone);
        }

        public static void PortfolioContactAssociate_Insert(int CustomerUserID,int PortfolioID)
        {
            //get the user detils
            UserMgt.Entity.Contractor customeruserDetails = GetContractorDetailsbyID(CustomerUserID);
            //check contact is exists or not
            int contactID = CheckExists(customeruserDetails.ContractorName, customeruserDetails.EmailAddress, PortfolioID);
            //if the contactID not exists insert new
            if (contactID == 0)
                contactID = InsertPortfolioContact(customeruserDetails.ContractorName, customeruserDetails.EmailAddress, customeruserDetails.ContactNumber, PortfolioID);

            using (PortfolioDataContext pd = new PortfolioDataContext())
            { 
                //enable Login to portal field
                PortfolioContact pfc = pd.PortfolioContacts.Where(p => p.ID == contactID).FirstOrDefault();
                if (pfc != null)
                {
                    pfc.LogintoPortal = true;
                    pd.SubmitChanges();
                }
                // check if already exists or not
                int pCount = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == CustomerUserID && p.ContactID == contactID).Count();
                if (pCount == 0)
                {
                    PortfolioContactAssociate ps = new PortfolioContactAssociate();

                    ps.ContactID = contactID;
                    ps.CustomerUserID = CustomerUserID;

                    pd.PortfolioContactAssociates.InsertOnSubmit(ps);
                    pd.SubmitChanges();
                }
            }

        }
        public static void PortfolioContactAssociate_Delete(int CustomerUserID)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {

                PortfolioContactAssociate ps = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == CustomerUserID).FirstOrDefault();
                if (ps != null)
                {
                    pd.PortfolioContactAssociates.DeleteOnSubmit(ps);
                    pd.SubmitChanges();
                }
            }
        }


        public static int GetCustomerUser_ContactID(int UserID)
        {
            int ContactID = 0;

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                if (pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.CustomerUserID.Value).Count() > 0)
                {
                    ContactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p=>p.ContactID.Value).FirstOrDefault();
                }
            }

            return ContactID;
        }
        #endregion
    }
}