using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for DCMailtoManager
    /// </summary>
    public class DCMailtoManager
    {
        public DCMailtoManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region List of MailAddress
        //public static IList MailList()
        //{
        //    using (DCDataContext dd = new DCDataContext())
        //    {
        //        //var mailList = (from p in dd.Managers
        //        //                join o in dd.ContractorDCs on p.UserID equals o.ID
        //        //                where o.EmailAddress != string.Empty
        //        //                select new { Email= o.EmailAddress }).ToList();
        //        return mailList;
        //    }
        //}
        #endregion

    }
}