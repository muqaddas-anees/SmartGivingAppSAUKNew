using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Location.DAL;
using Location.Entity;
using UserMgt.DAL;

namespace DC.BLL
{

    /// <summary>
    /// Summary description for FLSDetailsBAL
    /// </summary>
    public class FLSDetailsBAL
    {

        public static IQueryable<FLSDetail> FLSDetailsBAL_SelectAll()
        {
            IDCRespository<FLSDetail> crRepository = new DCRepository<FLSDetail>();
            return crRepository.GetAll();
        }

        public static CallResourceSchedule GetTicketByAssingedResourceID(int ID)
        {
           
            IDCRespository<CallResourceSchedule> crRepository = new DCRepository<CallResourceSchedule>();
            return crRepository.GetAll().Where(o => o.ID == ID).FirstOrDefault();

        }

        #region CCID
        public static void UpdateCallIDByCustomer()
        {
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var cnt = dContext.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).Count();
                if (cnt == 0)
                {
                    List<CallIDByCustomer> clList = new List<CallIDByCustomer>();
                    var clist = dContext.CallDetails.Where(o => o.CompanyID == sessionKeys.PortfolioID).OrderBy(o => o.ID).ToList();
                    int i = 0;
                    foreach (var c in clist)
                    {
                        var cl = new CallIDByCustomer();
                        cl.CallID = c.ID;
                        cl.CompanyID = sessionKeys.PortfolioID;
                        cl.CompanyCallID = i + 1;
                        clList.Add(cl);
                        i++;
                    }
                    if (clList.Count > 0)
                    {
                        dContext.CallIDByCustomers.InsertAllOnSubmit(clList);
                        dContext.SubmitChanges();
                    }
                }
            }
        }
        //insert new data
        public static void AddCallIDByCustomer(int callid)
        {
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var cnt = dContext.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).Count();
                if (cnt > 0)
                {

                    var cl = new CallIDByCustomer();
                    cl.CallID = callid;
                    cl.CompanyID = sessionKeys.PortfolioID;
                    cl.CompanyCallID = cnt + 1;


                    dContext.CallIDByCustomers.InsertOnSubmit(cl);
                    dContext.SubmitChanges();

                }
            }
        }

        public static void AddCallIDByCustomer(int callid,int portfolioid)
        {
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var cnt = dContext.CallIDByCustomers.Where(o => o.CompanyID == portfolioid).Count();
                if (cnt > 0)
                {

                    var cl = new CallIDByCustomer();
                    cl.CallID = callid;
                    cl.CompanyID = portfolioid;
                    cl.CompanyCallID = cnt + 1;


                    dContext.CallIDByCustomers.InsertOnSubmit(cl);
                    dContext.SubmitChanges();

                }
                else
                {
                    var cl = new CallIDByCustomer();
                    cl.CallID = callid;
                    cl.CompanyID = portfolioid;
                    cl.CompanyCallID = 1;


                    dContext.CallIDByCustomers.InsertOnSubmit(cl);
                    dContext.SubmitChanges();
                }
            }
        }

        public static int GetCallIDByCustomer(int callid)
        {
            int retval = 0;
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var cnt = dContext.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID && o.CallID == callid).FirstOrDefault();
                if (cnt != null)
                {
                    retval = cnt.CompanyCallID;
                }
                else
                {
                    cnt = dContext.CallIDByCustomers.Where(o => o.CallID == callid).FirstOrDefault();
                    if (cnt != null)
                    {
                        retval = cnt.CompanyCallID;
                    }
                }
            }
            return retval;
        }
        public static int GetCallIDByCustomerWithoutCompany(int callid)
        {
            int retval = 0;
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {

                var cnt = dContext.CallIDByCustomers.Where(o => o.CallID == callid).FirstOrDefault();
                if (cnt != null)
                {
                    retval = cnt.CompanyCallID;
                }

            }
            return retval;
        }

        public static int GetCallID(int CustomerCallID)
        {
            int retval = 0;
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var cnt = dContext.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID && o.CompanyCallID == CustomerCallID).FirstOrDefault();
                if (cnt != null)
                {
                    retval = cnt.CallID;
                }
            }
            return retval;
        }
        public static List<CallIDByCustomer> GetCCIDS()
        {

            IDCRespository<CallIDByCustomer> crRepository = new DCRepository<CallIDByCustomer>();
            return crRepository.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
        }

        public static List<CallIDByCustomer> GetCCIDS(int PortfolioID)
        {
            IDCRespository<CallIDByCustomer> crRepository = new DCRepository<CallIDByCustomer>();
            return crRepository.GetAll().Where(o => o.CompanyID == PortfolioID).ToList();
        }
        #endregion

        public static List<PortfolioMgt.Entity.PortfolioContact> GetRequesters()
        {
            IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            var sids = new int[] { 22, 43, 44 };
            var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == sessionKeys.PortfolioID).ToList();
            return pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
        }
        public static List<PortfolioMgt.Entity.PortfolioContact> GetRequesters(int PortfolioID)
        {
            IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            var sids = new int[] { 22, 43, 44 };
            var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == PortfolioID).ToList();
            return pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
        }
        public static List<PortfolioMgt.Entity.PortfolioContactAddress> GetRequestersAddress()
        {
            IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            IDCRespository<DC.Entity.FLSDetail> fReporsitory = new DCRepository<DC.Entity.FLSDetail>();
            var sids = new int[] { 22, 43, 44 };
            var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == sessionKeys.PortfolioID).ToList();
            var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
            return paReporsitory.GetAll().Where(o => fList.Select(r => r.ContactAddressID).ToArray().Contains(o.ID)).ToList();
        }
        public static List<PortfolioMgt.Entity.PortfolioContactAddress> GetRequestersAddress(int PortfolioID)
        {
            IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            IDCRespository<DC.Entity.FLSDetail> fReporsitory = new DCRepository<DC.Entity.FLSDetail>();
            var sids = new int[] { 22, 43, 44 };
            var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == PortfolioID).ToList();
            var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
            return paReporsitory.GetAll().Where(o => fList.Select(r => r.ContactAddressID).ToArray().Contains(o.ID)).ToList();
        }

        public static List<UserMgt.Entity.Contractor> GetServiceProviders()
        {
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
            return urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && o.SID == 4).ToList();

        }
        public static List<UserMgt.Entity.Contractor> GetServiceProviders(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany(PortfolioID);
            return urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && o.SID == 4).ToList();

        }

        public static List<UserMgt.Entity.UserDetail> GetUserAddresses()
        {
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
            return udrep.GetAll().Where(o => uidlist.Contains(o.UserId.HasValue ? o.UserId.Value : 0)).ToList();

        }
        public static List<UserMgt.Entity.UserDetail> GetUserAddresses(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany(PortfolioID);
            return udrep.GetAll().Where(o => uidlist.Contains(o.UserId.HasValue ? o.UserId.Value : 0)).ToList();

        }

        public static List<UserLocation> GetUserLocation()
        {
           // IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();
            IDCRespository<UserLocation> ulReporsitory = new DCRepository<UserLocation>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
            return ulReporsitory.GetAll().Where(o => uidlist.Contains(o.UserID)).ToList();

        }
        public static List<UserLocation> GetUserLocation(int PortfolioID)
        {
            // IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();
            IDCRespository<UserLocation> ulReporsitory = new DCRepository<UserLocation>();
            var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany(PortfolioID);
            return ulReporsitory.GetAll().Where(o => uidlist.Contains(o.UserID)).ToList();

        }
        public static List<string> InvoiceStatus()
        {
            List<string> str = new List<string>();
            str.Add("Pending Payment");
            str.Add("Paid in Full");
            str.Add("Paid in Part");
            str.Add("Cancelled");
            return str;

        }

        public static int AddDefaultTypeofRequest(int CustomerID)
        {
            var retval = 0;
            var item = "Fault";
            using (DC.DAL.DCDataContext dContext = new DCDataContext())
            {
                var dData = dContext.TypeOfRequests.Where(o => o.CustomerID == CustomerID && o.Name.ToLower() == item.ToLower()).FirstOrDefault();
                if (dData == null)
                {
                    dData = new TypeOfRequest();
                    dData.Name = "Fault";
                    dData.CustomerID = CustomerID;
                    dContext.TypeOfRequests.InsertOnSubmit(dData);
                    dContext.SubmitChanges();
                    retval = dData.ID;
                }
                else
                {
                    retval = dData.ID;
                }
            }
            return retval;
        }
        public static void TicketCancelled(int CallID)
        {
            IDCRespository<CallDetail> cRepository = new DCRepository<CallDetail>();
            IDCRespository<FLSDetail> fRepository = new DCRepository<FLSDetail>();
            IDCRespository<CallResourceSchedule> crRepository = new DCRepository<CallResourceSchedule>();
            IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();

            IDCRespository<CallDetailsJournal> cjRepository = new DCRepository<CallDetailsJournal>();

            var cd = cRepository.GetAll().Where(o => o.ID == CallID).FirstOrDefault();
            if (cd != null)
            {
                //33 Cancelled
                cd.StatusID = 33;
                cRepository.Edit(cd);
                //add journal entiry
                var cj = new CallDetailsJournal();
                cj.CallID = CallID;
                cj.CompanyID = cd.CompanyID;
                cj.LoggedBy = cd.LoggedBy;
                cj.LoggedDate = cd.LoggedDate;
                cj.ModifiedBy = sessionKeys.UID;
                cj.ModifiedDate = DateTime.Now;
                cj.RequesterID = cd.RequesterID;
                cj.RequestTypeID = cd.RequestTypeID;
                cj.SiteID = cd.SiteID;
                cj.StatusID = cd.StatusID;
                cj.VisibleToCustomer = false;
                cjRepository.Add(cj);
            }

            //var fd = fRepository.GetAll().Where(o => o.CallID == CallID).FirstOrDefault();
            //if (fd != null)
            //{
            //    fd.UserID = null;
            //    fRepository.Edit(fd);
            //}

            var cr = crRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (cr != null)
            {
                crRepository.DeleteAll(cr);
            }
            IDCRespository<Incident_Service> insRepository = new DCRepository<Incident_Service>();
            //delete Price
            var inp = insRepository.GetAll().Where(o => o.IncidentID == CallID).ToList();
            if (inp != null)
            {
                insRepository.DeleteAll(inp);
            }
            IDCRespository<Incident_ServicePrice> instRepository = new DCRepository<Incident_ServicePrice>();
            //delete price totals
            var inst = instRepository.GetAll().Where(o => o.IncidentID == CallID).ToList();
            if (inst != null)
            {
                instRepository.DeleteAll(inst);
            }
            //delete invoice
            var inr = inRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (inr != null)
            {
                inRepository.DeleteAll(inr);
            }

            IDCRespository<CallCustomerSurvey> ccsRepository = new DCRepository<CallCustomerSurvey>();
            var ccs = ccsRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (ccs != null)
            {
                ccsRepository.DeleteAll(ccs);
            }
        }
        public static void TicketReschedule(int CallID)
        {
            IDCRespository<CallDetail> cRepository = new DCRepository<CallDetail>();
            IDCRespository<FLSDetail> fRepository = new DCRepository<FLSDetail>();
            IDCRespository<CallResourceSchedule> crRepository = new DCRepository<CallResourceSchedule>();
            IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();

            IDCRespository<CallDetailsJournal> cjRepository = new DCRepository<CallDetailsJournal>();

            var cd = cRepository.GetAll().Where(o => o.ID == CallID).FirstOrDefault();
            if (cd != null)
            {
                //22 New
                cd.StatusID = 22;
                cRepository.Edit(cd);
                //add journal entiry
                var cj = new CallDetailsJournal();
                cj.CallID = CallID;
                cj.CompanyID = cd.CompanyID;
                cj.LoggedBy = cd.LoggedBy;
                cj.LoggedDate = cd.LoggedDate;
                cj.ModifiedBy = sessionKeys.UID;
                cj.ModifiedDate = DateTime.Now;
                cj.RequesterID = cd.RequesterID;
                cj.RequestTypeID = cd.RequestTypeID;
                cj.SiteID = cd.SiteID;
                cj.StatusID = cd.StatusID;
                cj.VisibleToCustomer = false;
                cjRepository.Add(cj);
            }

            var fd = fRepository.GetAll().Where(o => o.CallID == CallID).FirstOrDefault();
            if (fd != null)
            {
                fd.UserID = null;
                fRepository.Edit(fd);
            }

            var cr = crRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (cr != null)
            {
                crRepository.DeleteAll(cr);
            }
            IDCRespository<Incident_Service> insRepository = new DCRepository<Incident_Service>();
            //delete Price
            var inp = insRepository.GetAll().Where(o => o.IncidentID == CallID).ToList();
            if (inp != null)
            {
                insRepository.DeleteAll(inp);
            }
            IDCRespository<Incident_ServicePrice> instRepository = new DCRepository<Incident_ServicePrice>();
            //delete price totals
            var inst = instRepository.GetAll().Where(o => o.IncidentID == CallID).ToList();
            if (inst != null)
            {
                instRepository.DeleteAll(inst);
            }
            //delete invoice
            var inr = inRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (inr != null)
            {
                inRepository.DeleteAll(inr);
            }

            IDCRespository<CallCustomerSurvey> ccsRepository = new DCRepository<CallCustomerSurvey>();
            var ccs = ccsRepository.GetAll().Where(o => o.CallID == CallID).ToList();
            if (ccs != null)
            {
                ccsRepository.DeleteAll(ccs);
            }
        }

        public static void UpdateTicketStatus(int CallID, int resourceid, int statusid)
        {
            IDCRespository<CallDetail> cRepository = new DCRepository<CallDetail>();
            IDCRespository<CallDetailsJournal> cjRepository = new DCRepository<CallDetailsJournal>();

            CallDetail cd = cRepository.GetAll().Where(a => a.ID == CallID).FirstOrDefault();
            if (cd != null)
            {
                if (statusid != cd.StatusID)
                {
                    //35	Closed
                    cd.StatusID = statusid;
                    cRepository.Edit(cd);
                    //add journal entiry
                    var cj = new CallDetailsJournal();
                    cj.CallID = CallID;
                    cj.CompanyID = cd.CompanyID;
                    cj.LoggedBy = cd.LoggedBy;
                    cj.LoggedDate = cd.LoggedDate;
                    cj.ModifiedBy = resourceid;
                    cj.ModifiedDate = DateTime.Now;
                    cj.RequesterID = cd.RequesterID;
                    cj.RequestTypeID = cd.RequestTypeID;
                    cj.SiteID = cd.SiteID;
                    cj.StatusID = cd.StatusID;
                    cj.VisibleToCustomer = false;
                    cjRepository.Add(cj);
                }

            }

        }

        #region FLS Details
        public static List<FLSDetail> BindFLSDetails()
        {
            List<FLSDetail> FlsList = new List<FLSDetail>();
            using (DCDataContext dd = new DCDataContext())
            {
                FlsList = dd.FLSDetails.Select(r => r).ToList();
            }
            return FlsList;
        }
        #endregion

        #region Add FLS Details
        public static void AddFLSDetails(FLSDetail fls)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.FLSDetails.InsertOnSubmit(fls);
                dd.SubmitChanges();

            }
        }
        #endregion
        #region Select FLS Details by ID
        public static FLSDetail SelectbyId(int id)
        {

            FLSDetail fls = new FLSDetail();
            using (DCDataContext dd = new DCDataContext())
            {
                fls = dd.FLSDetails.Where(r => r.CallID == id).Select(r => r).FirstOrDefault();
            }
            return fls;
        }
        #endregion

        # region Update FLS Details
        public static void FLSDetailsUpdate(FLSDetail fls)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                FLSDetail flscurrent = dd.FLSDetails.Where(r => r.ID == fls.ID).Select(r => r).FirstOrDefault();
                flscurrent.CallID = fls.CallID;
                flscurrent.SubjectID = fls.SubjectID;
                flscurrent.Details = fls.Details;
                flscurrent.ScheduledDate = fls.ScheduledDate;
                flscurrent.TimeAccumulated = fls.TimeAccumulated;
                flscurrent.TimeWorked = fls.TimeWorked;
                flscurrent.DepartmentID = fls.DepartmentID;
                flscurrent.UserID = fls.UserID;
                flscurrent.Resolution = fls.Resolution;
                flscurrent.Notes = fls.Notes;
                flscurrent.RAGStatus = fls.RAGStatus;
                flscurrent.POnumber = fls.POnumber;
                flscurrent.RequestType = fls.RequestType;
                flscurrent.SourceOfRequestID = fls.SourceOfRequestID;
                flscurrent.CategoryID = fls.CategoryID;
                flscurrent.SubCategoryID = fls.SubCategoryID;
                flscurrent.CustomerReference = fls.CustomerReference;
                flscurrent.CustomerCostCode = fls.CustomerCostCode;
                flscurrent.DateTimeClosed = fls.DateTimeClosed;
                flscurrent.SubmittedBy = fls.SubmittedBy;
                flscurrent.TaskCategoryID = fls.TaskCategoryID;
                flscurrent.TaskSubcategoryID = fls.TaskSubcategoryID;
                flscurrent.Qty = fls.Qty;
                flscurrent.ResolutionDateandTime = fls.ResolutionDateandTime;
                flscurrent.TimeTakentoResolve = fls.TimeTakentoResolve;
                flscurrent.Sitedetails = fls.Sitedetails;
                flscurrent.PriorityId = fls.PriorityId;
                flscurrent.ScheduledEndDateTime = fls.ScheduledEndDateTime;
                flscurrent.Preferreddate2 = fls.Preferreddate2;
                flscurrent.Preferreddate3 = fls.Preferreddate3;

                flscurrent.ContactAddressID = fls.ContactAddressID;
                flscurrent.ScheduledDatetotime = fls.ScheduledDatetotime;
                flscurrent.Preferreddatetotime2 = fls.Preferreddatetotime2;
                flscurrent.Preferreddatetotime3 = fls.Preferreddatetotime3;
                flscurrent.TicketManagerID = fls.TicketManagerID;
                flscurrent.AppliedPreferredDate = fls.AppliedPreferredDate;
                flscurrent.ProviderPreferredDateTime = fls.ProviderPreferredDateTime;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete FLS Details By ID
        public static void FLSDetailsDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                FLSDetail fls = dd.FLSDetails.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (fls != null)
                {
                    dd.FLSDetails.DeleteOnSubmit(fls);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion

        #region Jlist
        public static List<FLSList> BindFLSList() //It Returns ALL 
        {
            List<FLSList> fList = new List<FLSList>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                        // var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).ToList();
                        flsList = dd.FLSDetails.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        dlist = dd.DeliveryInformations.Select(o => o).ToList();
                        AcList = dd.AccessControls.Select(a => a).ToList();
                        PwList = dd.PermitToWorks.Select(a => a).ToList();
                        fList = (from cd in cdList

                                     //join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 select new FLSList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     InHandSLA = null,
                                     ResolutionSLA = null,
                                     Note = "", //GetNotes(cd.RequestTypeID.HasValue?cd.RequesterID.Value:0, cd.ID, flsList, dlist, AcList, PwList).ToString(),
                                     Description = cd.RequestTypeID == 1 ? (dlist.Where(o => o.CallID == cd.ID).FirstOrDefault().Description) : string.Empty,
                                     ScheduleDate = ""// GetScheduleDate(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString().Substring(0,10)
                                 }).ToList();

                    }
                }
            }
            return fList;
        }

        public static List<FLSJList> BindFLSJList() //It Returns ALL 
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (DCDataContext dd = new DCDataContext())
                {
                    var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();

                    var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                    //var stList = ld.Sites.Select(s => s).ToList();
                    var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                    flsList = dd.FLSDetails.Select(d => d).ToList();

                    var sList = dd.Status.Select(s => s).ToList();
                    var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                    dlist = dd.DeliveryInformations.Select(o => o).ToList();
                    var PurposeToVisitList = dd.PurposeToVisits.Select(a => a).ToList();
                    AcList = dd.AccessControls.Select(a => a).ToList();
                    PwList = dd.PermitToWorks.Select(a => a).ToList();
                    // var alist = dd.AccessControls.Select(o => o).ToList();
                    fList = (from cd in cdList

                                 //join f in flsList on cd.ID equals f.CallID
                             join pc in pcList on cd.RequesterID equals pc.ID
                             join pp in ppList on cd.CompanyID equals pp.ID
                             //join s in stList on cd.SiteID equals s.ID
                             join st in sList on cd.StatusID equals st.ID
                             join rt in rtList on cd.RequestTypeID equals rt.ID

                             select new FLSJList
                             {
                                 CallID = cd.ID,
                                 Name = pc.Name,
                                 Company = pp.PortFolio,
                                 Status = st.Name,
                                 RequestType = rt.Name,
                                 Site = "",
                                 LoggedBy = cd.LoggedBy,
                                 InHandSLA = null,
                                 ResolutionSLA = null,
                                 PurposeofVisit = "Visit",
                                 Note = "", //GetNotes(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString(),
                                 Description = cd.RequestTypeID == 1 ? (dlist.Where(o => o.CallID == cd.ID).FirstOrDefault().Description) : string.Empty,
                                 ScheduleDate = ""//GetScheduleDate(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString().Substring(0,10)
                             }).ToList();

                }
            }

            return fList;
        }
        public static DateTime GetScheduleDate(int Requesttype, int CallID, List<FLSDetail> fd, List<DeliveryInformation> di, List<AccessControl> ac, List<PermitToWork> pt)
        {
            DateTime retval = Convert.ToDateTime("01/01/1900");

            try
            {
                if (Requesttype == 1)
                {
                    CallID = 7867;
                    var s = di.Where(o => o.CallID == CallID).FirstOrDefault();
                    retval = di.Where(o => o.CallID == CallID).FirstOrDefault().ArrivalDate.Value;
                }
                else if (Requesttype == 2)
                {
                    retval = pt.Where(o => o.CallID == CallID).FirstOrDefault().ArrivalDate.Value;
                }
                else if (Requesttype == 3)
                {
                    retval = ac.Where(o => o.CallID == CallID).FirstOrDefault().RequestedDate.Value;
                }
                else if (Requesttype == 6)
                {
                    retval = fd.Where(o => o.CallID == CallID).FirstOrDefault().ScheduledDate.Value;
                }

            }
            catch (Exception ex)
            {
                retval = Convert.ToDateTime("01/01/1900");
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        public static string GetNotes(int Requesttype, int CallID, List<FLSDetail> fd, List<DeliveryInformation> di, List<AccessControl> ac, List<PermitToWork> pt)
        {
            string retval = string.Empty;
            try
            {
                if (Requesttype == 1)
                {
                    if (di.Where(o => o.CallID == CallID).FirstOrDefault() != null)
                        retval = di.Where(o => o.CallID == CallID).FirstOrDefault().Notes;
                }
                else if (Requesttype == 2)
                {
                    if (pt.Where(o => o.CallID == CallID).FirstOrDefault() != null)
                        retval = pt.Where(o => o.CallID == CallID).FirstOrDefault().Notes;
                }
                else if (Requesttype == 3)
                {
                    if (ac.Where(o => o.CallID == CallID).FirstOrDefault() != null)
                        retval = ac.Where(o => o.CallID == CallID).FirstOrDefault().Notes;
                }
                else if (Requesttype == 6)
                {
                    if (fd.Where(o => o.CallID == CallID).FirstOrDefault() != null)
                        retval = fd.Where(o => o.CallID == CallID).FirstOrDefault().Notes;
                }
            }
            catch (Exception ex)
            {
                retval = string.Empty;
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }
        public static List<FLSJList> BindFLSJList1() //It Returns ALL 
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (DCDataContext dd = new DCDataContext())
                {
                    var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                    //var stList = ld.Sites.Select(s => s).ToList();
                    var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                    flsList = dd.FLSDetails.Select(d => d).ToList();
                    var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                    var sList = dd.Status.Select(s => s).ToList();
                    var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                    dlist = dd.DeliveryInformations.Select(o => o).ToList();
                    var PurposeToVisitList = dd.PurposeToVisits.Select(a => a).ToList();
                    AcList = dd.AccessControls.Select(a => a).ToList();
                    PwList = dd.PermitToWorks.Select(a => a).ToList();
                    // var alist = dd.AccessControls.Select(o => o).ToList();
                    fList = (from cd in cdList

                                 //join f in flsList on cd.ID equals f.CallID
                             join pc in pcList on cd.RequesterID equals pc.ID
                             join pp in ppList on cd.CompanyID equals pp.ID
                             //join s in stList on cd.SiteID equals s.ID
                             join st in sList on cd.StatusID equals st.ID
                             join rt in rtList on cd.RequestTypeID equals rt.ID
                             join ac in AcList on cd.ID equals ac.CallID
                             join pv in PurposeToVisitList on ac.PurposeOfVisitID equals pv.ID
                             select new FLSJList
                             {
                                 CallID = cd.ID,
                                 Name = pc.Name,
                                 Company = pp.PortFolio,
                                 Status = st.Name,
                                 RequestType = rt.Name,
                                 Site = "",
                                 LoggedBy = cd.LoggedBy,
                                 InHandSLA = null,
                                 ResolutionSLA = null,
                                 PurposeofVisit = pv.Name,
                                 Note = ac.Notes,
                                 Description = cd.RequestTypeID == 1 ? (dlist.Where(o => o.CallID == cd.ID).FirstOrDefault().Description) : string.Empty,
                                 ScheduleDate = ""// GetScheduleDate(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString().Substring(0,10)
                             }).ToList();
                }
            }

            return fList;
        }

        public static List<FLSJList> BindFLSJList1(int RequestTypeid) //It Returns ALL 
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();

                        var ppList = pd.ProjectPortfolios.Where(o => o.ID == sessionKeys.PortfolioID).Where(p => cdList.Select(o => o.CompanyID.Value).ToArray().Contains(p.ID)).Select(p => p).ToList();
                        //var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(p => cdList.Select(o => o.RequesterID.Value).ToArray().Contains(p.ID)).Select(p => p).ToList();
                        flsList = dd.FLSDetails.Select(d => d).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        dlist = dd.DeliveryInformations.Select(o => o).ToList();
                        var PurposeToVisitList = dd.PurposeToVisits.Select(a => a).ToList();
                        AcList = dd.AccessControls.Select(a => a).ToList();
                        PwList = dd.PermitToWorks.Select(a => a).ToList();
                        // var alist = dd.AccessControls.Select(o => o).ToList();
                        fList = (from cd in cdList

                                     //join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 join ac in AcList on cd.ID equals ac.CallID
                                 join pv in PurposeToVisitList on ac.PurposeOfVisitID equals pv.ID
                                 select new FLSJList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     InHandSLA = null,
                                     ResolutionSLA = null,
                                     PurposeofVisit = pv.Name,
                                     Note = ac.Notes,
                                     Description = cd.RequestTypeID == 1 ? (dlist.Where(o => o.CallID == cd.ID).FirstOrDefault().Description) : string.Empty,
                                     ScheduleDate = ""// GetScheduleDate(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString().Substring(0,10)
                                 }).ToList();
                    }
                }
            }
            return fList;
        }

        //public static List<FLSJList> BindFLSJList()  //It returns only FLS Data
        //{
        //    List<FLSJList> fList = new List<FLSJList>();
        //    List<PortfolioSLA> ps = new List<PortfolioSLA>();
        //    using (LocationDataContext ld = new LocationDataContext())
        //    {
        //        using (PortfolioDataContext pd = new PortfolioDataContext())
        //        {
        //            using (DCDataContext dd = new DCDataContext())
        //            {

        //                var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
        //                var stList = ld.Sites.Select(s => s).ToList();
        //                var pcList = pd.PortfolioContacts.Select(p => p).ToList();
        //                pcList.Add(new PortfolioContact { ID = 0, Name = "" });
        //                var flsList = dd.FLSDetails.Select(d => d).ToList();
        //                var cdList = dd.CallDetails.Select(c => c).ToList();
        //                var sList = dd.Status.Select(s => s).ToList();
        //                var rtList = dd.RequestTypes.Select(rt => rt).ToList();
        //                var requestTypeList = dd.TypeOfRequests.ToList();

        //                //SLA

        //                //ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
        //                ps = pd.PortfolioSLAs.Select(p=>p).ToList();


        //                fList = (from cd in cdList

        //                         join f in flsList on cd.ID equals f.CallID
        //                         join pc in pcList on cd.RequesterID equals pc.ID
        //                         join pp in ppList on cd.CompanyID equals pp.ID
        //                         //join s in stList on cd.SiteID equals s.ID
        //                         join st in sList on cd.StatusID equals st.ID
        //                         join rt in rtList on cd.RequestTypeID equals rt.ID
        //                         join tr in requestTypeList on f.RequestType  equals tr.ID
        //                         select new FLSJList
        //                         {
        //                             CallID = cd.ID,
        //                             Name = pc.Name,
        //                             Company = pp.PortFolio,
        //                             Status = st.Name,
        //                             RequestType = rt.Name,
        //                             RT = tr.Name,
        //                             Site = "",
        //                             LoggedBy = cd.LoggedBy,
        //                             DateLogged = string.Format("{0:dd/MM/yyyy HH:mm}", cd.LoggedDate),
        //                             Description = f.Details,
        //                             InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(cd.ID,f.CategoryID.HasValue?f.CategoryID.Value:0,cd.CompanyID.HasValue?cd.CompanyID.Value:0, ps) : f.InHandSLA.ToString(),
        //                             ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString()

        //                         }).ToList();

        //            }
        //        }
        //    }
        //    return fList;
        //}
        public static List<FLSJList> BindFLSResourceList(int resourceId)  //It returns only FLS Data
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {

                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                        pcList.Add(new PortfolioContact { ID = 0, Name = "" });
                        flsList = dd.FLSDetails.Where(d => d.UserID == resourceId).Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        AcList = dd.AccessControls.Select(a => a).ToList();
                        PwList = dd.PermitToWorks.Select(a => a).ToList();
                        dlist = dd.DeliveryInformations.Select(o => o).ToList();
                        var requestTypeList = dd.TypeOfRequests.ToList();
                        requestTypeList.Add(new TypeOfRequest { ID = 0, Name = "" });
                        //SLA

                        //ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                        ps = pd.PortfolioSLAs.Select(p => p).ToList();


                        fList = (from cd in cdList

                                 join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 join tr in requestTypeList on f.RequestType equals tr.ID
                                 select new FLSJList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     RT = tr.Name,
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cd.LoggedDate),
                                     Description = f.Details,
                                     InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                     ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                     Note = "", //GetNotes(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString(),
                                     ScheduleDate = ""// GetScheduleDate(cd.RequestTypeID.HasValue ? cd.RequesterID.Value : 0, cd.ID, flsList, dlist, AcList, PwList).ToString().Substring(0,10)
                                 }).ToList();

                    }
                }
            }
            return fList;
        }


        public static List<FLSJList> BindFLSJList(int UserID)  //It returns only FLS Data
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {

                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                        pcList.Add(new PortfolioContact { ID = 0, Name = "" });
                        flsList = dd.FLSDetails.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        var contactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.ContactID).FirstOrDefault();

                        //SLA

                        //ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                        ps = pd.PortfolioSLAs.Select(p => p).ToList();


                        fList = (from cd in cdList

                                 join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 where cd.RequesterID == contactID
                                 select new FLSJList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     RT = f.RequestType == 1 ? "Faults" : "Service Request",
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cd.LoggedDate),
                                     Description = f.Details,
                                     InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                     ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString()

                                 }).ToList();

                    }
                }
            }
            return fList;
        }

        public static List<FLSJList> BindFLSJList(int UserID, int CustomerID)  //It returns only FLS Data
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == CustomerID).ToList();
                        flsList = dd.FLSDetails.Select(d => d).Where(o => cdList.Select(c => c.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();

                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == CustomerID).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == CustomerID).ToList();
                        pcList.Add(new PortfolioContact { ID = 0, Name = "" });


                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        var contactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.ContactID).FirstOrDefault();

                        //SLA

                        //ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                        ps = pd.PortfolioSLAs.Select(p => p).ToList();


                        fList = (from cd in cdList

                                 join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 where cd.RequesterID == contactID
                                 select new FLSJList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     RT = f.RequestType == 1 ? "Faults" : "Service Request",
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cd.LoggedDate),
                                     Description = f.Details,
                                     InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                     ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString()

                                 }).ToList();

                    }
                }
            }
            return fList;
        }

        public static List<FLSJList> BindFLSJList(int UserID, int CustomerID, int[] statusids)  //It returns only FLS Data
        {
            List<FLSJList> fList = new List<FLSJList>();
            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<FLSDetail> flsList = new List<FLSDetail>();
            List<DeliveryInformation> dlist = new List<DeliveryInformation>();
            List<AccessControl> AcList = new List<AccessControl>();
            List<PermitToWork> PwList = new List<PermitToWork>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == CustomerID && statusids.Contains(o.StatusID.HasValue ? o.StatusID.Value : 0)).ToList();
                        flsList = dd.FLSDetails.Select(d => d).Where(o => cdList.Select(c => c.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();

                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == CustomerID).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == CustomerID).ToList();
                        pcList.Add(new PortfolioContact { ID = 0, Name = "" });


                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        var contactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.ContactID).FirstOrDefault();

                        //SLA

                        //ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                        ps = pd.PortfolioSLAs.Select(p => p).ToList();


                        fList = (from cd in cdList

                                 join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 where cd.RequesterID == contactID
                                 select new FLSJList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     RT = f.RequestType == 1 ? "Faults" : "Service Request",
                                     Site = "",
                                     LoggedBy = cd.LoggedBy,
                                     DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cd.LoggedDate),
                                     Description = f.Details,
                                     InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                     ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(cd.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, cd.CompanyID.HasValue ? cd.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString()

                                 }).ToList();

                    }
                }
            }
            return fList;
        }

        public static List<FLSList> BindFLSList(int UserID)
        {
            List<FLSList> fList = new List<FLSList>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).Where(o => o.ID == sessionKeys.PortfolioID).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                        var flsList = dd.FLSDetails.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        var contactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.ContactID).FirstOrDefault();

                        fList = (from cd in cdList
                                     //join f in flsList on cd.ID equals f.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 //join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 where cd.RequesterID == contactID
                                 select new FLSList
                                 {
                                     CallID = cd.ID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = "",
                                     LoggedBy = cd.LoggedBy

                                 }).ToList();

                    }
                }
            }
            return fList;
        }


        #endregion
        public static string GetColorOfInHandSLA(double time, int TimeInHand)
        {
            string statusColor = string.Empty;
            try
            {
                if (time > TimeInHand)
                {
                    statusColor = "Red";
                }
                else
                {
                    statusColor = "Green";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return statusColor;
        }

        public static string GetInHandSLAStatusNew(int Callid, int TypeOFReqId, int CategoryId, int SubCategoryId, List<PortfolioSLA> psList, int PortFolioId)
        {
            string statusColor = string.Empty;
            try
            {
                using (DCDataContext dd = new DCDataContext())
                {
                    PortfolioSLA ps = new PortfolioSLA();
                    if (psList.Count > 0)
                    {
                        ps = psList
                            .Where(a => a.Portfolio == PortFolioId && a.TypeofRequestID == TypeOFReqId
                                && a.MasterCategoryID == CategoryId
                                && a.Category == SubCategoryId).FirstOrDefault();
                        double time = 0.00;
                        if (ps != null)
                        {
                            CallDetailsJournal callDetailFirstOne = dd.CallDetailsJournals.Where(c => c.CallID == Callid).FirstOrDefault();
                            int StatusId = dd.Status.Where(a => a.Name == "In Hand/WIP" && a.RequestTypeID == 6).Select(a => a.ID).FirstOrDefault();
                            CallDetailsJournal callDetailInHand = dd.CallDetailsJournals
                                                                  .Where(c => c.CallID == Callid
                                                                      && c.StatusID == StatusId).OrderBy(c => c.ID).FirstOrDefault();
                            TimeSpan? DateTimeDifference = null;
                            if (callDetailInHand != null)
                            {
                                DateTimeDifference = (Convert.ToDateTime(callDetailInHand.ModifiedDate) - Convert.ToDateTime(callDetailFirstOne.ModifiedDate));
                            }
                            else
                            {
                                DateTimeDifference = (DateTime.Now - Convert.ToDateTime(callDetailFirstOne.ModifiedDate));
                            }
                            if (ps.TimeInHandEx.ToString().ToLower() == "m")
                            {
                                time = DateTimeDifference.Value.TotalMinutes;
                                statusColor = GetColorOfInHandSLA(time, ps.TimeInHand.Value);
                            }
                            if (ps.TimeInHandEx.ToString().ToLower() == "h")
                            {
                                time = (DateTimeDifference.Value.Hours * 60) + DateTimeDifference.Value.TotalMinutes;
                                statusColor = GetColorOfInHandSLA(time, (ps.TimeInHand.Value * 60));
                            }
                            if (ps.TimeInHandEx.ToString().ToLower() == "d")
                            {
                                time = (DateTimeDifference.Value.Days * 24 * 60) +
                                    (DateTimeDifference.Value.Hours * 60) + DateTimeDifference.Value.TotalMinutes;
                                statusColor = GetColorOfInHandSLA(time, (ps.TimeInHand.Value * 24 * 60));
                            }
                        }
                        else
                        {
                            //No time limits are this TypeOfRequests,category,subcategory
                            statusColor = "Green";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return statusColor;
        }


        public static string GetInHandSLAStatus(int callId, int CategoryID, int PortfolioID, List<PortfolioSLA> psList)
        {
            using (DCDataContext dd = new DCDataContext())
            {


                PortfolioSLA ps = new PortfolioSLA();
                if (psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == PortfolioID).Count() > 0)
                {
                    //ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == callDetail.CompanyID).FirstOrDefault();
                    ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == PortfolioID).FirstOrDefault();
                }
                else
                {
                    ps = psList.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                }

                string status = string.Empty;
                double time = 0.00;
                if (ps != null)
                {
                    CallDetail callDetail = dd.CallDetails.Where(c => c.ID == callId).FirstOrDefault();

                    TimeSpan timeDifference = (DateTime.Now - Convert.ToDateTime(callDetail.LoggedDate));

                    if (ps.TimeInHandEx.ToString().ToLower() == "m")
                    {
                        time = timeDifference.TotalMinutes;

                        time = (time / double.Parse(ps.TimeInHand.ToString())) * 100;

                        if (time > 80)
                        {
                            status = "Red";
                        }
                        else if (time >= 50 && time <= 80)
                        {
                            status = "Amber";
                        }
                        else
                        {
                            status = "Green";
                        }
                    }
                    if (ps.TimeInHandEx.ToString().ToLower() == "h")
                    {
                        time = timeDifference.TotalHours;

                        time = (time / double.Parse(ps.TimeInHand.ToString())) * 100;

                        if (time > 80)
                        {
                            status = "Red";
                        }
                        else if (time >= 50 && time <= 80)
                        {
                            status = "Amber";
                        }
                        else
                        {
                            status = "Green";
                        }
                    }
                    if (ps.TimeInHandEx.ToString().ToLower() == "d")
                    {
                        time = timeDifference.TotalDays;

                        time = (time / double.Parse(ps.TimeInHand.ToString())) * 100;

                        if (time > 80)
                        {
                            status = "Red";
                        }
                        else if (time >= 50 && time <= 80)
                        {
                            status = "Amber";
                        }
                        else
                        {
                            status = "Green";
                        }
                    }

                }
                return status;
            }
        }


        public static string GetResolutionSLAStatusNew(int Callid, int TypeOFReqId, int CategoryId, int SubCategoryId, List<PortfolioSLA> psList, int PortFolioId)
        {
            string statusColor = string.Empty;
            try
            {
                using (DCDataContext dd = new DCDataContext())
                {
                    PortfolioSLA ps = new PortfolioSLA();
                    if (psList.Count > 0)
                    {
                        ps = psList
                            .Where(a => a.Portfolio == PortFolioId && a.TypeofRequestID == TypeOFReqId
                                && a.MasterCategoryID == CategoryId
                                && a.Category == SubCategoryId).FirstOrDefault();
                        double time = 0.00;
                        if (ps != null)
                        {
                            int StatusInHandId = dd.Status.Where(a => a.Name == "In Hand/WIP" && a.RequestTypeID == 6).Select(a => a.ID).FirstOrDefault();
                            int StatusResolvedId = dd.Status.Where(a => a.Name == "Resolved" && a.RequestTypeID == 6).Select(a => a.ID).FirstOrDefault();

                            CallDetailsJournal callDetailFirstOne = dd.CallDetailsJournals.Where(c => c.CallID == Callid && c.StatusID == StatusInHandId).FirstOrDefault();

                            CallDetailsJournal callDetailInHand = dd.CallDetailsJournals
                                                                  .Where(c => c.CallID == Callid
                                                                      && c.StatusID == StatusResolvedId).OrderBy(c => c.ID).FirstOrDefault();
                            if (callDetailFirstOne != null)
                            {
                                TimeSpan? DateTimeDifference = null;
                                if (callDetailInHand != null)
                                {
                                    DateTimeDifference = (Convert.ToDateTime(callDetailInHand.ModifiedDate) - Convert.ToDateTime(callDetailFirstOne.ModifiedDate));
                                }
                                else
                                {
                                    DateTimeDifference = (DateTime.Now - Convert.ToDateTime(callDetailFirstOne.ModifiedDate));
                                }
                                if (ps.TimeEx.ToString().ToLower() == "m")
                                {
                                    time = DateTimeDifference.Value.TotalMinutes;
                                    statusColor = GetColorOfInHandSLA(time, ps.TimetoResolve.Value);
                                }
                                if (ps.TimeEx.ToString().ToLower() == "h")
                                {
                                    time = (DateTimeDifference.Value.Hours * 60) + DateTimeDifference.Value.TotalMinutes;
                                    statusColor = GetColorOfInHandSLA(time, (ps.TimetoResolve.Value * 60));
                                }
                                if (ps.TimeEx.ToString().ToLower() == "d")
                                {
                                    time = (DateTimeDifference.Value.Days * 24 * 60) +
                                        (DateTimeDifference.Value.Hours * 60) + DateTimeDifference.Value.TotalMinutes;
                                    statusColor = GetColorOfInHandSLA(time, (ps.TimetoResolve.Value * 24 * 60));
                                }
                            }
                            else
                            {
                                //call not changed to InHand.so its shows yellow 
                                statusColor = "yellow";
                            }
                        }
                        else
                        {
                            //No time limits are this TypeOfRequests,category,subcategory
                            statusColor = "Green";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return statusColor;
        }


        public static string GetResolutionSLAStatus(int callId, int CategoryID, int PortfolioID, List<PortfolioSLA> psList)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                string status = string.Empty;
                double time = 0.00;
                //PortfolioSLA ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                PortfolioSLA ps = new PortfolioSLA();
                if (psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == PortfolioID).Count() > 0)
                {
                    //ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == callDetail.CompanyID).FirstOrDefault();
                    ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == PortfolioID).FirstOrDefault();
                }
                else
                {
                    ps = psList.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                }
                if (ps != null)
                {
                    FLSTimeDetail flsInHand = dd.FLSTimeDetails.Where(f => f.CallID == callId && f.Status == "In Hand/WIP").OrderBy(f => f.StatusTime).FirstOrDefault();

                    if (flsInHand != null)
                    {
                        TimeSpan timeDifference = (DateTime.Now - Convert.ToDateTime(flsInHand.StatusTime));
                        if (ps.TimeEx.ToString().ToLower() == "m")
                        {
                            time = timeDifference.TotalMinutes;

                            time = (time / double.Parse(ps.TimetoResolve.ToString())) * 100;

                            if (time > 80)
                            {
                                status = "Red";
                            }
                            else if (time >= 50 && time <= 80)
                            {
                                status = "Amber";
                            }
                            else
                            {
                                status = "Green";
                            }
                        }
                        if (ps.TimeEx.ToString().ToLower() == "h")
                        {
                            time = timeDifference.TotalHours;

                            time = (time / double.Parse(ps.TimetoResolve.ToString())) * 100;

                            if (time > 80)
                            {
                                status = "Red";
                            }
                            else if (time >= 50 && time <= 80)
                            {
                                status = "Amber";
                            }
                            else
                            {
                                status = "Green";
                            }
                        }
                        if (ps.TimeEx.ToString().ToLower() == "d")
                        {
                            time = timeDifference.TotalDays;

                            time = (time / double.Parse(ps.TimetoResolve.ToString())) * 100;

                            if (time > 80)
                            {
                                status = "Red";
                            }
                            else if (time >= 50 && time <= 80)
                            {
                                status = "Amber";
                            }
                            else
                            {
                                status = "Green";
                            }
                        }

                    }

                }
                return status;
            }
        }

        #region SLA Status
        public static void SetInHandSLAStatus(int portfolioID, int callId)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    double time = 0.00;
                    //PortfolioSLA ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == portfolioID).FirstOrDefault();
                    List<PortfolioSLA> psList = pd.PortfolioSLAs.Select(p => p).ToList();
                    FLSDetail fls = dd.FLSDetails.Where(f => f.CallID == callId).FirstOrDefault();
                    PortfolioSLA ps = new PortfolioSLA();
                    if (psList.Where(p => p.MasterCategoryID == fls.CategoryID && p.Portfolio == portfolioID).Count() > 0)
                    {
                        //ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == callDetail.CompanyID).FirstOrDefault();
                        ps = psList.Where(p => p.MasterCategoryID == fls.CategoryID && p.Portfolio == portfolioID).FirstOrDefault();
                    }
                    else
                    {
                        ps = psList.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                    }
                    if (ps != null)
                    {
                        FLSTimeDetail flsNew = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callId, "New");
                        FLSTimeDetail flsInHand = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callId, "In Hand/WIP");

                        if (flsNew != null && flsInHand != null)
                        {
                            TimeSpan timeDifference = (Convert.ToDateTime(flsInHand.StatusTime) - Convert.ToDateTime(flsNew.StatusTime));

                            if (ps.TimeInHandEx.ToString().ToLower() == "m")
                            {
                                time = timeDifference.TotalMinutes;
                                if (time > ps.TimeInHand)
                                {
                                    fls.InHandSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.InHandSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                            if (ps.TimeInHandEx.ToString().ToLower() == "h")
                            {
                                time = timeDifference.TotalHours;
                                if (time > ps.TimeInHand)
                                {
                                    fls.InHandSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.InHandSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                            if (ps.TimeInHandEx.ToString().ToLower() == "d")
                            {
                                time = timeDifference.TotalDays;
                                if (time > ps.TimeInHand)
                                {
                                    fls.InHandSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.InHandSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }

        }
        public static void SetResolutionSLAStatus(int portfolioID, int callId)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    double time = 0.00;
                    //PortfolioSLA ps = pd.PortfolioSLAs.Where(p => p.MasterCategoryID == 0 && p.Portfolio == portfolioID).FirstOrDefault();
                    List<PortfolioSLA> psList = pd.PortfolioSLAs.Select(p => p).ToList();
                    FLSDetail fls = dd.FLSDetails.Where(f => f.CallID == callId).FirstOrDefault();
                    PortfolioSLA ps = new PortfolioSLA();
                    if (psList.Where(p => p.MasterCategoryID == fls.CategoryID && p.Portfolio == portfolioID).Count() > 0)
                    {
                        //ps = psList.Where(p => p.MasterCategoryID == CategoryID && p.Portfolio == callDetail.CompanyID).FirstOrDefault();
                        ps = psList.Where(p => p.MasterCategoryID == fls.CategoryID && p.Portfolio == portfolioID).FirstOrDefault();
                    }
                    else
                    {
                        ps = psList.Where(p => p.MasterCategoryID == 0 && p.Portfolio == 0).FirstOrDefault();
                    }
                    if (ps != null)
                    {
                        FLSTimeDetail flsNew = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callId, "In Hand/WIP");
                        FLSTimeDetail flsResolved = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callId, "Resolved");
                        //FLSDetail fls = dd.FLSDetails.Where(f => f.CallID == callId).FirstOrDefault();
                        if (flsNew != null && flsResolved != null)
                        {
                            TimeSpan timeDifference = (Convert.ToDateTime(flsResolved.StatusTime) - Convert.ToDateTime(flsNew.StatusTime));

                            if (ps.TimeEx.ToString().ToLower() == "m")
                            {
                                time = timeDifference.TotalMinutes;
                                if (time > ps.TimetoResolve)
                                {
                                    fls.ResolutionSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.ResolutionSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                            if (ps.TimeEx.ToString().ToLower() == "h")
                            {
                                time = timeDifference.TotalHours;
                                if (time > ps.TimetoResolve)
                                {
                                    fls.ResolutionSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.ResolutionSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                            if (ps.TimeEx.ToString().ToLower() == "d")
                            {
                                time = timeDifference.TotalDays;
                                if (time > ps.TimetoResolve)
                                {
                                    fls.ResolutionSLA = false;
                                    dd.SubmitChanges();
                                }
                                else
                                {
                                    fls.ResolutionSLA = true;
                                    dd.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion
        #region FLS Report
        public static List<Jqgrid> GetFLSReportList()
        {

            List<Jqgrid> rpt = new List<Jqgrid>();
            //using (UserDataContext ud = new UserDataContext())
            //{
            //    using (PortfolioDataContext pd = new PortfolioDataContext())
            //    {
            //        using (DCDataContext dd = new DCDataContext())
            //        {

                         rpt = Jqgridlist();
                        //rpt = (from c in jlist
                        //           //join f in fDetails on c.ID equals f.CallID
                        //           //join cl in callIDListByCustomer on c.ID equals cl.CallID
                        //       select new FLSReport
                        //       {
                        //           CallID = f.CallID.Value,
                        //           CCID = cl.CompanyCallID,
                        //           RequesterID = c.RequesterID,
                        //           RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.Name).FirstOrDefault(),
                        //           StatusID = c.StatusID,
                        //           StatusName = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                        //           LoggedDate = c.LoggedDate,
                        //           ScheduleDate = f.ScheduledDate,
                        //           SiteID = c.SiteID,
                        //           SiteName = c.SiteID.HasValue ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                        //           DepartmentID = f.DepartmentID,
                        //           DepartmentName = f.DepartmentID.HasValue ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                        //           TechnecianID = f.UserID,
                        //           TechnecianName = f.UserID.HasValue ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                        //           CompanyID = c.CompanyID,
                        //           ComapanyName = pCompany.Where(p => p.ID == c.CompanyID).Select(p => p.PortFolio).FirstOrDefault(),
                        //           SubjectID = f.SubjectID,
                        //           SubjectName = f.SubjectID.HasValue ? subjectList.Where(p => p.ID == f.SubjectID).Select(p => p.SubjectName).FirstOrDefault() : string.Empty


                        //       }).ToList();
            //        }
            //    }
            //}

            return rpt;
        }

        #endregion

        #region set Source of request

        public static int Set_CustomerPortal_SourceOfRequest()
        {
            int retval = 0;
            string source_of_request = "Customer Portal";
            using (DCDataContext dc = new DCDataContext())
            {
                FLSSourceOfRequest fs = dc.FLSSourceOfRequests.Where(o => o.CustomerID == sessionKeys.PortfolioID).Where(p => p.Name.ToLower() == source_of_request.ToLower()).FirstOrDefault();
                if (fs == null)
                {
                    fs = new FLSSourceOfRequest();
                    fs.Name = source_of_request;
                    dc.FLSSourceOfRequests.InsertOnSubmit(fs);
                    dc.SubmitChanges();
                }
                retval = fs.ID;
            }

            return retval;
        }

        #endregion
        #region JqGrid
        public static List<Jqgrid> Jqgridlist()
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            // 6 - FLS
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.CompanyID == sessionKeys.PortfolioID).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();
                            var callids = cList.Select(o => o.ID).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();
                            var pCompany = pd.ProjectPortfolios.Where(o => o.ID == sessionKeys.PortfolioID).Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Where(o => callids.Contains(o.CallID.HasValue ? o.CallID.Value : 0)).Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();
                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                           
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();


                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();

                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                                       LoggedDate = c.LoggedDate.HasValue?c.LoggedDate.Value:DateTime.Now,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManagerID = f.TicketManagerID.HasValue ?f.TicketManagerID.Value:0,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty
                                   }).ToList();
                        }
                    }
                }
            }

            return rpt;
        }

        public static List<Jqgrid> JqgridlistByPortfolioID(int PortfolioID)
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            var weburl = Deffinity.systemdefaults.GetWebUrl(); 
                            // 6 - FLS
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.CompanyID == PortfolioID).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();
                            var callids = cList.Select(o => o.ID).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();

                            var pCompany = pd.ProjectPortfolios.Where(o => o.ID == PortfolioID).Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Where(o => callids.Contains(o.CallID.HasValue ? o.CallID.Value : 0)).Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();
                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();
                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == PortfolioID).ToList();
                            var docList = dd.Documents.Where(o => callids.Contains(o.CallID.HasValue ? o.CallID.Value : 0)).Select(d => d).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                                       LoggedDate = c.LoggedDate.HasValue ? c.LoggedDate.Value : DateTime.Now,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty,
                                       ImagePath = GetImagesList(docList.Where(d => d.CallID == c.ID).ToList(),weburl,c.ID)// docList.Where(d=>d.CallID == c.ID).Count()>0? string.Format("{0}/WF/UploadData/DC/{1}.png", weburl , docList.Where(d => d.CallID == c.ID).OrderByDescending(d=>d.ID).FirstOrDefault().DocumentID):string.Empty
                                   }).ToList();
                        }
                    }
                }
            }

            return rpt;
        }


        public static string GetImagesList(List<Document> docList,string weburl,int callid)
        {

            var retval = string.Empty;
            foreach(var dEntity in docList)
            {
                retval = retval + string.Format("{0}/WF/UploadData/DC/{1}.png ,", weburl, dEntity.DocumentID);
            }

            return retval;

        }


        public static List<Jqgrid> JqgridlistByStatus(int statusID)
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            List<int> stlist = new List<int>() ;
                            // 6 - FLS
                            if (statusID > 0)
                                stlist.Add(statusID);
                            else
                            {
                                stlist.Add(JobStatus.Arrived);
                                stlist.Add(JobStatus.Awaiting_Information);
                                stlist.Add(JobStatus.Awaiting_Schedule);
                                stlist.Add(JobStatus.Closed);
                                stlist.Add(JobStatus.Customer_Not_Responding);
                                stlist.Add(JobStatus.New);
                                stlist.Add(JobStatus.Quote_Accepted);
                                stlist.Add(JobStatus.Scheduled);
                                stlist.Add(JobStatus.Waiting_On_Parts);
                                stlist.Add(JobStatus.Feedback_Received);
                                stlist.Add(JobStatus.Feedback_Submitted);
                            }
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.CompanyID == sessionKeys.PortfolioID && stlist.Contains(o.StatusID.Value )).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();
                            var callids = cList.Select(o => o.ID).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();
                            var pCompany = pd.ProjectPortfolios.Where(o => o.ID == sessionKeys.PortfolioID).Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Where(o => callids.Contains(o.CallID.HasValue ? o.CallID.Value : 0)).Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();
                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();
                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                      
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                                       LoggedDate = c.LoggedDate.HasValue?c.LoggedDate.Value:DateTime.Now,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty
                                       
                                   }).ToList();
                        }
                    }
                }
            }

            return rpt;
        }

        public static PortfolioContactAddress GetAddress(int? AddressID, List<UserMgt.Entity.v_contractor> aList)
        {
            UserMgt.Entity.v_contractor eretval = new  UserMgt.Entity.v_contractor();
            var retval = new PortfolioContactAddress();
            if (AddressID.HasValue)
            {
               // retval = aList.Where(o => o.ID == AddressID.Value).FirstOrDefault();
                if (retval == null)
                {
                   //  retval = new PortfolioContactAddress();
                    retval.Address = string.Empty;
                    retval.Address2 = string.Empty;
                    retval.PostCode = string.Empty;
                    retval.City = string.Empty;
                    retval.State = string.Empty;
                }
            }
            else
            {
                retval.Address = string.Empty;
                retval.Address2 = string.Empty;
                retval.PostCode = string.Empty;
                retval.City = string.Empty;
                retval.State = string.Empty;
            }
            return retval;
        }


        public static List<Jqgrid> JqgridlistByRequester(int UserID,int requsterID =0)
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            //getContactID
                            int contactID = 0;

                            if (requsterID == 0)
                                contactID = pd.PortfolioContactAssociates.Where(p => p.CustomerUserID == UserID).Select(p => p.ContactID.HasValue?p.ContactID.Value:0).FirstOrDefault();
                            else
                                contactID = requsterID;
                            // 6 - FLS
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.RequesterID == contactID && o.CompanyID == sessionKeys.PortfolioID).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();
                            var pCompany = pd.ProjectPortfolios.Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();
                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();
                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() != null ? sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty

                                   }).ToList();
                        }
                    }
                }
            }
            return rpt;
        }


        

        public static double GetTotalCost(double? total, double? vat)
        {
            double v = 0.00;
            double vtotal = 0.00;
            var sp = total.HasValue ? total.Value : 0.00;
            if (sp > 0)
            {
                v = vat.HasValue ? vat.Value : 0.00;
                if (v > 0)
                    vtotal = (sp * v) / 100;
            }
            return sp + vtotal;
        }

        public static List<Jqgrid> Jqgridlist(int callid)
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {

                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            // 6 - FLS
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.ID == callid && o.CompanyID == sessionKeys.PortfolioID).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();
                            var pCompany = pd.ProjectPortfolios.Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Where(o => o.CallID == callid).Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();

                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();
                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() != null ? sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty
                                   }).ToList();
                        }
                    }
                }
            }

            return rpt;
        }


        public static List<Jqgrid> Jqgridlist(int callid, int portfolioid)
        {
            var dDate = Convert.ToDateTime("01/01/1900");

            List<PortfolioSLA> ps = new List<PortfolioSLA>();
            List<Jqgrid> rpt = new List<Jqgrid>();
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {

                        using (ProjectMgt.DAL.projectTaskDataContext pj = new ProjectMgt.DAL.projectTaskDataContext())
                        {
                            // 6 - FLS
                            var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.ID == callid && o.CompanyID == portfolioid).Select(c => c).ToList();
                            var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                            var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();


                            ps = pd.PortfolioSLAs.Where(p => companyids.Contains(p.Portfolio.HasValue ? p.Portfolio.Value : 0)).Select(p => p).ToList();
                            var pCompany = pd.ProjectPortfolios.Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                            var fDetails = dd.FLSDetails.Where(o => o.CallID == callid).Select(d => d).ToList();
                            var technicianids = fDetails.Select(o => o.UserID.HasValue ? o.UserID.Value : 0).Distinct().ToArray();

                            var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                            var siteList = dd.OurSites.Select(s => s).ToList();
                            var requesterList = ud.v_contractors.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList(); // pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                            var deprtmentList = dd.AssignedDepartments.Select(s => s).ToList();
                            var technicianList = ud.Contractors.Where(p => technicianids.Contains(p.ID)).Select(s => s).ToList();
                            var tmids = fDetails.Select(o => o.TicketManagerID.HasValue ? o.TicketManagerID.Value : 0).Distinct().ToArray();
                            var ticketmanagerList = ud.Contractors.Where(p => tmids.Contains(p.ID)).Select(s => s).ToList();
                            var subjectList = dd.Subjects.Select(s => s).ToList();
                            var categorylist = dd.Categories.Select(c => c).ToList();
                            var typeofreequestList = dd.RequestTypes.Select(r => r).ToList();
                            var sourceofrequest = dd.FLSSourceOfRequests.Select(a => a).ToList();
                            var requestTypeList = dd.TypeOfRequests.ToList();
                            var displaylist = dd.DisplayColumnsByUsers.Select(a => a).ToList();
                            var priotitylist = dd.PriorityLevels.Select(a => a).ToList();
                            var addressList = ud.v_contractors.Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(s => s).ToList(); //pd.PortfolioContactAddresses.Select(a => a).ToList();
                            var invoiceList = dd.CallInvoices.ToList();
                            var pricelist = dd.Incident_ServicePrices.ToList();
                            var pdefaults = pj.ProjectDefaults.ToList();
                            var callIDListByCustomer = dd.CallIDByCustomers.Where(o => o.CompanyID == portfolioid).ToList();
                            rpt = (from c in cList
                                   join f in fDetails on c.ID equals f.CallID
                                   join cl in callIDListByCustomer on c.ID equals cl.CallID
                                   select new Jqgrid
                                   {
                                       CallID = f.CallID.Value,
                                       CCID = cl.CompanyCallID,
                                       RequesterID = c.RequesterID.HasValue ? c.RequesterID.Value : 0,
                                       RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() != null ? sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       LoggedDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       ScheduledDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledDate),
                                       Preferreddate2 = f.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate2) : string.Empty,
                                       Preferreddate3 = f.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.Preferreddate3) : string.Empty,
                                       ScheduledEndDateTime = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ScheduledEndDateTime),
                                       Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() != null ? siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       AssignedtoDepartment = deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() != null ? deprtmentList.Where(p => p.ID == f.DepartmentID).Select(p => p.DepartmentName).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianEmail = technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AssignedTechnicianContact = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContactNumber).FirstOrDefault() : string.Empty,
                                       AssignedTechnician = technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() != null ? technicianList.Where(p => p.ID == f.UserID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       SourceofRequest = sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() != null ? sourceofrequest.Where(p => p.ID == f.SourceOfRequestID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       Category = categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() != null ? categorylist.Where(p => p.ID == f.CategoryID).Select(p => p.Name).FirstOrDefault() : string.Empty,
                                       TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() != null ? requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault() : string.Empty,
                                       Subject = subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() != null ? subjectList.Where(a => a.ID == f.SubjectID).Select(a => a.SubjectName).FirstOrDefault() : string.Empty,
                                       Notes = f.Notes,
                                       TimeAccumulated = f.TimeAccumulated,
                                       TimeWorked = f.TimeWorked,
                                       CustomerCostCode = f.CustomerCostCode,
                                       PONumber = f.POnumber,
                                       Details = f.Details,
                                       LoggedBy = technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() != null ? technicianList.Where(a => a.ID == c.LoggedBy).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       RequestersEmailAddress = requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() != null ? requesterList.Where(p => p.ID == c.RequesterID).Select(a => a.EmailAddress).FirstOrDefault() : string.Empty,
                                       RequestersTelephoneNo = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContactNumber).FirstOrDefault() : string.Empty),
                                       CustomerRef = f.CustomerReference != null ? f.CustomerReference.ToString() : string.Empty,
                                       RequestersJobTitle = requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() != null ? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.ContractorName).FirstOrDefault() : string.Empty,
                                       DateandTimeStarted = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                       DateandTimeClosed = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.DateTimeClosed),
                                       RequestersDepartment = string.Empty,
                                       Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() != null ? pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault() : string.Empty,
                                       InHandSLA = string.IsNullOrEmpty(f.InHandSLA.ToString()) ? GetInHandSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.InHandSLA.ToString(),
                                       ResolutionSLA = string.IsNullOrEmpty(f.ResolutionSLA.ToString()) ? GetResolutionSLAStatus(c.ID, f.CategoryID.HasValue ? f.CategoryID.Value : 0, c.CompanyID.HasValue ? c.CompanyID.Value : 0, ps) : f.ResolutionSLA.ToString(),
                                       AssignedTechnicianID = Convert.ToInt32(f.UserID.HasValue ? f.UserID.Value : 0),
                                       LoggedByID = Convert.ToInt32(c.LoggedBy.HasValue ? c.LoggedBy.Value : 0),
                                       RequestersAddress = GetAddress(f.ContactAddressID, addressList).Address,// (f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Address1).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address !=  null? addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().Address:string.Empty),
                                       RequestersPostCode = GetAddress(f.ContactAddressID, addressList).PostCode,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault()!= null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Postcode).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().PostCode:string.Empty),
                                       RequestersCity = GetAddress(f.ContactAddressID, addressList).City,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault() != null?requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.City).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().City:string.Empty),
                                       RequestersTown = GetAddress(f.ContactAddressID, addressList).State,//(f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0) == 0 ? (requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault() != null? requesterList.Where(a => a.ID == c.RequesterID).Select(a => a.Town).FirstOrDefault():string.Empty) : (addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State != null?addressList.Where(o => o.ID == f.ContactAddressID.Value).FirstOrDefault().State:string.Empty),
                                       Priority = f.PriorityId.HasValue ? (priotitylist.Where(o => o.Id == f.PriorityId.Value).Select(o => o.Value).FirstOrDefault()) : string.Empty,
                                       ContactAddressID = f.ContactAddressID.HasValue ? f.ContactAddressID.Value : 0,
                                       InvoiceRef = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? "Invoice No:" + invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().ID.ToString() : string.Empty,
                                       InvoiceDate = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().CreatedDate) : string.Empty,
                                       Cost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice) : "0.00",
                                       VAT = pdefaults.ToList().FirstOrDefault() != null ? string.Format("{0:F2}", pdefaults.ToList().FirstOrDefault().VAT) : "0.00",
                                       TotalCost = pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault() != null ? string.Format("{0:F2}", GetTotalCost(pricelist.Where(o => o.IncidentID == c.ID).FirstOrDefault().OriginalPrice, pdefaults.ToList().FirstOrDefault().VAT)) : "0.00",
                                       InvoiceStatus = invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault() != null ? (invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue != null ? invoiceList.Where(o => o.CallID == c.ID).FirstOrDefault().StatusValue : string.Empty) : string.Empty,
                                       ScheduledDatetotime = !string.IsNullOrEmpty(f.ScheduledDatetotime) ? f.ScheduledDatetotime.ToString() : string.Empty,
                                       Preferreddatetotime2 = !string.IsNullOrEmpty(f.Preferreddatetotime2) ? f.Preferreddatetotime2.ToString() : string.Empty,
                                       Preferreddatetotime3 = !string.IsNullOrEmpty(f.Preferreddatetotime3) ? f.Preferreddatetotime3.ToString() : string.Empty,
                                       TicketManager = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.ContractorName).FirstOrDefault() : string.Empty,
                                       TicketManagerEmail = f.TicketManagerID.HasValue ? ticketmanagerList.Where(p => p.ID == f.TicketManagerID).Select(p => p.EmailAddress).FirstOrDefault() : string.Empty,
                                       AppliedPreferredDate = (f.AppliedPreferredDate.HasValue ? f.AppliedPreferredDate.Value : 0).ToString(),
                                       ProviderPreferredDateTime = f.ProviderPreferredDateTime.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), f.ProviderPreferredDateTime) : string.Empty
                                   }).ToList();
                        }
                    }
                }
            }

            return rpt;
        }


        public static string GetJobDetails(int callid)
        {
            string retval = string.Empty;

            IDCRespository<DC.Entity.FLSDetail> fRep = new DCRepository<DC.Entity.FLSDetail>();
            var fEntity = fRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (fEntity != null)
            {
                if (fEntity.Details != null)
                {
                    if (fEntity.Details.Length > 90)
                        retval = fEntity.Details.Substring(0, 89) + "...";
                    else
                        retval = fEntity.Details;
                    
                }
                
            }


            return retval;

        }
        #endregion



            #region Ticket manager
        public class TicketManagerDisplay
        {
            public int ID { set; get; }
            public string Name { set; get; }
            public string Email { set; get; }
        }

        public static List<TicketManagerDisplay> GetTicketManagers()
        {
            List<TicketManagerDisplay> retlist = new List<TicketManagerDisplay>();
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IDCRespository<DC.Entity.TicketManager> trep = new DCRepository<DC.Entity.TicketManager>();
            var tlist = trep.GetAll().ToList();
            if (tlist.Count() > 0)
            {
                var ulist = urep.GetAll().Where(o => tlist.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();


                retlist = (from p in tlist
                           join c in ulist on p.UserID equals c.ID
                           select new TicketManagerDisplay()
                           {
                               ID = p.TMID,
                               Name = c.ContractorName,
                               Email = c.EmailAddress
                           }).ToList();
            }



            return retlist;
        }
        #endregion

        #region ticket assoicate to key contact
        public static CallAssoiciateKeyContact CallAssoiciateKeyContact_Add(int callid, int keyContactID)
        {
            IDCRespository<DC.Entity.CallAssoiciateKeyContact> kRep = new DCRepository<DC.Entity.CallAssoiciateKeyContact>();
            var k = kRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (k == null)
            {
                k = new CallAssoiciateKeyContact() { CallID = callid, KeyContactID = keyContactID };
                kRep.Add(k);
            }
            else
            {
                k.KeyContactID = keyContactID;
                kRep.Edit(k);
            }

            return k;
        }
        public static int CallAssoiciateKeyContact_Select(int callid)
        {
            int keyContactID = 0;
            IDCRespository<DC.Entity.CallAssoiciateKeyContact> kRep = new DCRepository<DC.Entity.CallAssoiciateKeyContact>();
            var k = kRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (k != null)
                keyContactID = k.KeyContactID.HasValue?k.KeyContactID.Value:0;
            return keyContactID;
        }

        #endregion

        #region Get Assigned technician 
        public class AssignedTechnicianDisplay
        {
            public int ID { set; get; }
            public int CCID { set; get; }
            public int CallID { set; get; }
            public string CallRef { set; get; }
            public int ResourceID { set; get; }
            public string TechnicianName { set; get; }
            public string TechnicianContactNo { set; get; }
            public string TechnicianEmail { set; get; }
        }
        public static List<AssignedTechnicianDisplay> GetAssinedTechnicians(int ccid)
        {
            List<AssignedTechnicianDisplay> alist = new List<AssignedTechnicianDisplay>();
            IDCRespository<CallResourceSchedule> dReposit = new DCRepository<CallResourceSchedule>();
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            var callref = GetCallID(ccid);
            var clist = dReposit.GetAll().Where(o => o.CallID == callref && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
            if(clist.Count>0)
            {
                var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany().ToList();
                int[] sids = { 4 };
                var ulist = urep.GetAll().Where(o => uidlist.Contains(o.ID) && sids.Contains(o.SID.Value)).ToList();
                alist = (from p in clist
                         join u in ulist on p.ResourceID equals u.ID
                         select new AssignedTechnicianDisplay
                         {
                             CallID = p.CallID.Value,
                             CCID = ccid,
                             CallRef = Deffinity.systemdefaults.GetCallPrefix() + p.CallID.Value.ToString(),
                             ID = p.ID,
                             ResourceID = u.ID,
                             TechnicianName = u.ContractorName,
                             TechnicianEmail = u.EmailAddress,
                             TechnicianContactNo = u.ContactNumber

                         }).ToList();
            }

            return alist;
        }
        #endregion

        //update user

        public static bool CallStatusSchedule(int callid,int assignedUserID)
        {
            bool retval = false;
            var cReporsitory = new DCRepository<CallDetail>();
            var cd = cReporsitory.GetAll().Where(o => o.ID == callid).FirstOrDefault();
            var fReporsitory = new DCRepository<FLSDetail>();
            var fd = fReporsitory.GetAll().Where(o => o.CallID == callid).FirstOrDefault();

            if (cd.StatusID == JobStatus.Awaiting_Schedule)
            {
                if (assignedUserID > 0)
                {
                    FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, JobStatus.Scheduled);
                    //43	Assigned Technician
                    //cd.StatusID = 43;
                    //cReporsitory.Edit(cd);
                    fd.UserID = assignedUserID;

                    fReporsitory.Edit(fd);

                    using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                    {
                        var userlist = Pdc.Contractors.Where(p => p.ID == assignedUserID).ToList();

                        foreach (var c in userlist)
                        {
                            //update to associated callids
                            UpdateAssignResources(fd, cd, null, false, false, 0, c, true);
                        }
                        //MailSendingToAssignResource(fd, cd);
                        //update 
                        //sendmailtoCustomer(QueryStringValues.CallID.ToString(), fd.UserID.Value.ToString());
                    }
                }
            }
            return retval;
        }


        private static bool UpdateAssignResources(FLSDetail fd, CallDetail cd, ServiceProviderScheduling blastValues, bool tocheck, bool smail, int cnt, UserMgt.Entity.Contractor c, bool IsAssigned)
        {
            using (DCDataContext Dc = new DCDataContext())
            {
                var cdResource = Dc.CallResourceSchedules.Where(o => o.CallID == cd.ID && o.ResourceID == c.ID).FirstOrDefault();
                if (cdResource == null)
                {
                    cdResource = new CallResourceSchedule();
                    cdResource.IsActive = false;
                    cdResource.ResourceID = c.ID;
                    cdResource.CallID = cd.ID;
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }

                    Dc.CallResourceSchedules.InsertOnSubmit(cdResource);
                    Dc.SubmitChanges();
                }
                else
                {
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }


                    Dc.SubmitChanges();
                }
            }

            return smail;
        }


        #region Job Details
        public class DropdownList
        {
            public int id { set; get; }
            public string name { set; get; }
        }
        public class JobDropdownList
        {
            public int jobid { set; get; }
            public string jobtitle { set; get; }
        }

        public static List<JobDropdownList> GetActiveJobDropdownLists(int portfilioID)
        {
            int[] ids = { JobStatus.Closed, JobStatus.Cancelled };

            IDCRespository<DC.Entity.V_CallDetail> dRep = new DCRepository<DC.Entity.V_CallDetail>();
            var jList = dRep.GetAll().Where(o => o.CompanyID == portfilioID).Where(o => !ids.Contains(o.StatusID.Value)).ToList();
            var jblist = (from j in jList
                              //where ids.Contains(j.StatusID.Value)
                          orderby j.CCID
                          select new JobDropdownList
                          {
                              jobid = j.ID,
                              jobtitle = j.CCID.ToString() + " - " + (j.Details.Length > 50 ? j.Details.Substring(0, 49).ToString() + "..." : j.Details.ToString())

                          }).ToList();
            jblist.Add(new JobDropdownList() { jobid = 0, jobtitle = "Please select..." });
            return jblist.ToList();

        }

        #endregion

    }



}