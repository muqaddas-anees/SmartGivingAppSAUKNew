using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

/// <summary>
/// Summary description for AccessControlBAL
/// </summary>
namespace DC.BAL
{
    public class AccessControlBAL
    {
        public AccessControlBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Insert access control details
        public static void AccessControlDetails_Insert(AccessControl ac)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.AccessControls.InsertOnSubmit(ac);
                dc.SubmitChanges();
            }
        }
        #endregion

        #region Update access control details
        public static void AccessControlDetails_update(AccessControl ac)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                AccessControl acdetails = (from a in dc.AccessControls
                                           where a.ID == ac.ID
                                    select a).FirstOrDefault();
                acdetails.AreaID = ac.AreaID;
                acdetails.CallID = ac.CallID;
                //acdetails.DeliveryNumber = ac.DeliveryNumber;
                acdetails.Notes = ac.Notes;
                acdetails.NumberOfDays = ac.NumberOfDays;
                acdetails.PurposeOfVisitID = ac.PurposeOfVisitID;
                acdetails.RequestedDate = ac.RequestedDate;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Select access control details
        public static List<AccessControl> AccessControlDetails_selectAll()
        {
            List<AccessControl> acldetails = new List<AccessControl>();
            using (DCDataContext dc = new DCDataContext())
            {
                acldetails = (from ac in dc.AccessControls select ac).ToList();
            }
            return acldetails;
        }
        #endregion


        #region Select access control details
        public static List<AccessControl> AccessControlDetails_selectByVisitingPurpose(string vpurpose)
        {
            List<AccessControl> acldetails = new List<AccessControl>();
            using (DCDataContext dc = new DCDataContext())
            {
                acldetails = (from ac in dc.AccessControls where ac.PurposeOfVisitID.ToString() == vpurpose select ac).ToList();
            }
            return acldetails;
        }
        #endregion
        #region Select access control details by id
        public static AccessControl AccessControlDetails_selectByID(int id)
        {
            AccessControl actl = new AccessControl();
            using (DCDataContext dc = new DCDataContext())
            {

                actl = (from ac in dc.AccessControls
                     where ac.CallID == id
                     select ac).FirstOrDefault();
            }

            return actl;
        }
        #endregion

       

        #region Select access control details by callid
        public static AccessControl AccessControlDetails_selectByCallID(int id)
        {
            AccessControl actl = new AccessControl();
            using (DCDataContext dc = new DCDataContext())
            {

                actl = (from ac in dc.AccessControls
                        where ac.CallID == id
                        select ac).FirstOrDefault();
            }

            return actl;
        }
        #endregion
      

        #region Delete details
        public static void AccessControlDetails_Delete(int id)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                var ac = (from a in dc.AccessControls
                         where a.ID == id
                         select a).FirstOrDefault();
                dc.AccessControls.DeleteOnSubmit(ac);
                dc.SubmitChanges();

            }


        }
        #endregion

     
        

        private static DateTime  GetVisitingDatepart(string vdt)
        {
            DateTime dt = new DateTime();
            if (vdt != null)
            {
                string[] d = vdt.ToString().Split(' ');
                dt = Convert.ToDateTime(d[0]);
                
            }
            return dt;
        }
        private static DateTime GetArriveDatepart(string adt)
        {
            DateTime dt = new DateTime();
            if (adt != null)
            {
                string[] d = adt.ToString().Split(' ');
                dt = Convert.ToDateTime(d[0]);
            }
            return dt;
        }
        private static DateTime GetDepartDatepart(string ddt)
        {
            DateTime dt = new DateTime();
            if (ddt != null)
            {
                string[] d = ddt.ToString().Split(' ');
                dt = Convert.ToDateTime(d[0]);
            }
            return dt;
        }


        #region Atleaset one visitor should exist for each access number

        public static bool CheckVisitorExist(int Callid)
        {
            bool retval = true;

            using (DCDataContext dc = new DCDataContext())
            {
                List<AccessNumbersBasedonDay> anum_list = dc.AccessNumbersBasedonDays.Where(p => p.CallID == Callid).ToList();
                List<Visitor> visitor_list = dc.Visitors.Where(p => p.CallID == Callid).ToList();
                //check for each day
                foreach (AccessNumbersBasedonDay aday in anum_list)
                {
                    if (visitor_list.Where(p => p.CallID == Callid && p.AccessNo == aday.AccessNumber).Count() == 0)
                    {
                        retval = false;
                        break;
                    }
                }
            }

            return retval;
        }

        #endregion
    }
}