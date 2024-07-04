using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for CallDetailsBAL
    /// </summary>
    public class CallDetailsBAL
    {

        public static IQueryable<V_CallDetail> CallDetails_selectall()
        {
            IDCRespository<DC.Entity.V_CallDetail> callDetailsRepository = new DCRepository<DC.Entity.V_CallDetail>();

            return callDetailsRepository.GetAll();
        }
        #region Bind CallDetails
        public static List<CallDetail> BindCallDetails()
        {
            List<CallDetail> callList = new List<CallDetail>();
            using (DCDataContext dd = new DCDataContext())
            {
                callList = dd.CallDetails.Select(r => r).ToList();
            }
            return callList;
        }
        #endregion
     
        #region Add Call Details
        public static void AddCallDetails(CallDetail cdetail)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.CallDetails.InsertOnSubmit(cdetail);
                dd.SubmitChanges();
                //update company callid 
                FLSDetailsBAL.AddCallIDByCustomer(cdetail.ID,cdetail.CompanyID.Value);
            }
        }
        #endregion
       
        #region Select Call Detail by ID
        public static CallDetail SelectbyId(int id)
        {

            CallDetail cdetail = new CallDetail();
            using (DCDataContext dd = new DCDataContext())
            {
                cdetail = dd.CallDetails.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return cdetail;
        }
        #endregion

        #region Select LoggedName By LoggedID
        public static string LoggedByName(int id)
        {
            string loggedName = string.Empty;
            using (UserDataContext ud=new UserDataContext())
            {
                loggedName = ud.Contractors.Where(r => r.ID == id).Select(r => r.ContractorName).FirstOrDefault();
            }
            return loggedName;
        }
        #endregion

        # region Update CallDetails
        public static void CallDetailsUpdate(CallDetail cdetail)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                CallDetail cdcurrent = dd.CallDetails.Where(r => r.ID == cdetail.ID).Select(r => r).FirstOrDefault();
                cdcurrent.SiteID = cdetail.SiteID;
                cdcurrent.RequesterID = cdetail.RequesterID;
                cdcurrent.RequestTypeID = cdetail.RequestTypeID;
                cdcurrent.StatusID = cdetail.StatusID;
                cdcurrent.CompanyID = cdetail.CompanyID;
                //cdcurrent.LoggedDate = cdetail.LoggedDate;
                //cdcurrent.LoggedBy = cdetail.LoggedBy;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete CallDetails By ID
        public static void CallDetailsDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                CallDetail cdetail = dd.CallDetails.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (cdetail != null)
                {
                    dd.CallDetails.DeleteOnSubmit(cdetail);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion

       
    }
}