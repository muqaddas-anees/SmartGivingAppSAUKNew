using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
namespace DC.BLL
{
    public class CustomerContactsBAL
    {
        #region Add Customer Contacts
        public static void AddCustomerContacts(PortfolioContact pc)
        {
            using(PortfolioDataContext pd = new PortfolioDataContext())
            {
                pd.PortfolioContacts.InsertOnSubmit(pc);
                pd.SubmitChanges();
            }
        }
        #endregion

        #region Check If the contact is exists
        public static PortfolioContact CheckContact(string name, string email, int portfolioID)
        {
            PortfolioContact pc = new PortfolioContact();
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                 pc = pd.PortfolioContacts.Where(p => p.Name.ToLower() == name.ToLower() && p.Email.ToLower() == email.ToLower() && p.PortfolioID == portfolioID).Select(p => p).FirstOrDefault();
               
            }
            return pc;
        }

        public static PortfolioContact CheckContact( string email, int portfolioID)
        {
            PortfolioContact pc = new PortfolioContact();
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                pc = pd.PortfolioContacts.Where(p =>  p.Email.ToLower() == email.ToLower() && p.PortfolioID == portfolioID).Select(p => p).FirstOrDefault();

            }
            return pc;
        }
        #endregion

        #region Update Customer Contacts
        public static void UpdateCustomerContacts(PortfolioContact pc)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                PortfolioContact pconcurrent = pd.PortfolioContacts.Where(p => p.ID == pc.ID).Select(p => p).FirstOrDefault();

                pconcurrent.Name = pc.Name;
                pconcurrent.Title = pc.Title;
                pconcurrent.Email = pc.Email;
                pconcurrent.Telephone = pc.Telephone;
                pconcurrent.LogintoPortal = pc.LogintoPortal;
                
                pd.SubmitChanges();


            }
        }
        #endregion

    }
}