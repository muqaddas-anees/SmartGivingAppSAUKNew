using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

/// <summary>
/// Summary description for EmailFooter
/// </summary>
/// 
namespace DC.BAL
{

    public class FooterEmail
    {
        public FooterEmail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Select all types of requests
        public static List<RequestType> RequestTypes_selectAll()
        {
            List<RequestType> rt = new List<RequestType>();
            using (DCDataContext dc = new DCDataContext())
            {
                rt = (from p in dc.RequestTypes orderby p.Name select p).ToList();
            }
            return rt;
        }
        #endregion

        #region Select email footer by id
        public static EmailFooter EmailFooter_selectByID(int id,int customerId)
        {
            EmailFooter ef = new EmailFooter();
            using (DCDataContext dc = new DCDataContext())
            {

                ef = (from e in dc.EmailFooters
                     where e.RequestTypeID == id && e.customerID==customerId
                     select e).FirstOrDefault();
                if (ef == null)
                {
                    ef = new EmailFooter();
                    ef.EmailFooter1 = string.Empty;
                }
            }

            return ef;
        }
        public static EmailFooter EmailFooter_selectByID(int id)
        {
            EmailFooter ef = new EmailFooter();
            using (DCDataContext dc = new DCDataContext())
            {

                ef = (from e in dc.EmailFooters
                      where e.RequestTypeID == id
                      orderby e.ID descending
                      select e).FirstOrDefault();
                //if (ef == null)
                //{
                //    ef = new EmailFooter();
                //    ef.EmailFooter1 = string.Empty;
                //}
            }

            return ef;
        }
        #endregion

        #region Update email footer
        public static void EmailFooter_update(EmailFooter e)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                EmailFooter ef = (from p in dc.EmailFooters
                                    where p.ID == e.ID
                                    select p).FirstOrDefault();
                
                ef.EmailFooter1 = e.EmailFooter1;
                ef.RequestTypeID = e.RequestTypeID;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Insert email footer 
        public static void EmailFooter_Insert(EmailFooter ef)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.EmailFooters.InsertOnSubmit(ef);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}