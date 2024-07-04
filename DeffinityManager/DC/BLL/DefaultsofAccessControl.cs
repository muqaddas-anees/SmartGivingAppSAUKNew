using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

/// <summary>
/// Summary description for DefaultsofAccessControl
/// </summary>
namespace DC.BAL
{

    public class DefaultsOfAccessControl
    {
        public DefaultsOfAccessControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Insert Access No.

        public static void AccessNo_Insert(AccessNumber an)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.AccessNumbers.InsertOnSubmit(an);
                dc.SubmitChanges();
            }

        }
        #endregion
        #region Select Access No.
        public static AccessNumber AccessNo_select()
        {
            AccessNumber acsno = new AccessNumber();
            using (DCDataContext dc = new DCDataContext())
            {
                acsno = (from p in dc.AccessNumbers select p).FirstOrDefault();
            }
            return acsno;
        }
        #endregion

        #region Update Access No.
        public static void AccessNo_update(AccessNumber an)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                AccessNumber acsno = (from p in dc.AccessNumbers
                                      where p.ID == an.ID
                                      select p).FirstOrDefault();
                acsno.AccessNo = an.AccessNo;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Delete Access no.
        public static void AccessNo_Delete(int id)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                var acsno = (from s in dc.AccessNumbers
                             where s.ID == id
                             select s).FirstOrDefault();
                dc.AccessNumbers.DeleteOnSubmit(acsno);
                dc.SubmitChanges();

            }


        }
        #endregion

        #region Insert Access Control Email

        public static void AccessMail_Insert(AccessControlEmail ae)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.AccessControlEmails.InsertOnSubmit(ae);
                dc.SubmitChanges();
            }

        }
        #endregion


        #region Select Access Email
        public static AccessControlEmail AccessEmail_select(int customerId )
        {
            AccessControlEmail acsmail = new AccessControlEmail();
            using (DCDataContext dc = new DCDataContext())
            {
                acsmail = (from p in dc.AccessControlEmails where p.CustomerID==customerId select p).FirstOrDefault();
            }
            return acsmail;
        }
        public static AccessControlEmail AccessEmail_select()
        {
            AccessControlEmail acsmail = new AccessControlEmail();
            using (DCDataContext dc = new DCDataContext())
            {
                acsmail = (from p in dc.AccessControlEmails orderby p.CustomerID select p ).FirstOrDefault();
            }
            return acsmail;
        }
        #endregion

        #region Update Access Email
        public static void AccessEmail_update(AccessControlEmail an)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                AccessControlEmail acsmail = (from p in dc.AccessControlEmails
                                              where p.ID == an.ID
                                              select p).FirstOrDefault();
                acsmail.EmailAddress = an.EmailAddress;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Delete Access mail
        public static void AccessEmail_Delete(int id)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                var acsmail = (from s in dc.AccessControlEmails
                               where s.ID == id
                               select s).FirstOrDefault();
                dc.AccessControlEmails.DeleteOnSubmit(acsmail);
                dc.SubmitChanges();

            }


        }
        #endregion
    }
}