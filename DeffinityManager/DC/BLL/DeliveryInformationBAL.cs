using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Location.DAL;
using Location.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for DeliveryInformationBAL
    /// </summary>
    public class DeliveryInformationBAL
    {
        #region Bind DeliveryInformation
        public static List<DeliveryInformation> BindDeliveryInformation()
        {
            List<DeliveryInformation> diList = new List<DeliveryInformation>();
            using (DCDataContext dd = new DCDataContext())
            {
                diList = dd.DeliveryInformations.Select(r => r).ToList();
            }
            return diList;
        }
        #endregion

        #region Add DeliveryInformation
        public static void AddDeliveryInformation(DeliveryInformation di)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.DeliveryInformations.InsertOnSubmit(di);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select DeliveryInformation by ID
        public static DeliveryInformation SelectbyId(int id)
        {

            DeliveryInformation di = new DeliveryInformation();
            using (DCDataContext dd = new DCDataContext())
            {
                di = dd.DeliveryInformations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return di;
        }
        #endregion
        #region Select DeliveryInformation by CallID
        public static DeliveryInformation SelectbyCallId(int cid)
        {

            DeliveryInformation di = new DeliveryInformation();
            using (DCDataContext dd = new DCDataContext())
            {
                di = dd.DeliveryInformations.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return di;
        }
        #endregion
        # region Update DeliveryInformation
        public static void DeliveryInformationUpdate(DeliveryInformation di)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                DeliveryInformation dicurrent = dd.DeliveryInformations.Where(r => r.ID == di.ID).Select(r => r).FirstOrDefault();
                dicurrent.CallID = di.CallID;
                dicurrent.ArrivalDate = di.ArrivalDate;
                dicurrent.Weight = di.Weight;
                dicurrent.NumofBoxes = di.NumofBoxes;
                dicurrent.DeliveryTypeID = di.DeliveryTypeID;
                dicurrent.Description = di.Description;
                dicurrent.Pallet = di.Pallet;
                dicurrent.OverWeight = di.OverWeight;
                dicurrent.CourierNumber = di.CourierNumber;
                dicurrent.CourierCompany = di.CourierCompany;
                dicurrent.Notes = di.Notes;
                dicurrent.ItemWeight = di.ItemWeight;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region DeleteDeliveryInformation By ID
        public static void DeliveryInformationDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                DeliveryInformation di = dd.DeliveryInformations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (di != null)
                {
                    dd.DeliveryInformations.DeleteOnSubmit(di);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion

        #region Bind Delivery List

        public static List<DeliveryList> BindDeliveryList()
        {
            List<DeliveryList> dList = new List<DeliveryList>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).ToList();
                        var diList = dd.DeliveryInformations.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        dList = (from cd in cdList
                                 join di in diList on cd.ID equals di.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID

                                 select new DeliveryList
                                 {
                                     CallID = di.CallID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = s.Site1
                                 }).ToList();
                    }
                }
            }


            return dList;
        }
        #endregion
        #region Bind Delivery List by Company

        public static List<DeliveryList> BindDeliveryList(int cid)
        {
            List<DeliveryList> dList = new List<DeliveryList>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).ToList();
                        var diList = dd.DeliveryInformations.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        dList = (from cd in cdList
                                 join di in diList on cd.ID equals di.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 where pc.PortfolioID == cid
                                 select new DeliveryList
                                 {
                                     CallID = di.CallID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = s.Site1
                                 }).ToList();
                    }
                }
            }


            return dList;
        }
        #endregion

        public class AccessControl
        {
            public int? CallID { get; set; }
            public string Name { get; set; }
            public string Company { get; set; }
            public string RequestType { get; set; }
            public string Site { get; set; }
            public string Status { get; set; }


        }

        #region Bind Access Control List

        public static List<AccessControl> BindAccessControlList()
        {
            List<AccessControl> dList = new List<AccessControl>();
            using (LocationDataContext ld = new LocationDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                        var stList = ld.Sites.Select(s => s).ToList();
                        var pcList = pd.PortfolioContacts.Select(p => p).ToList();
                        var acList = dd.AccessControls.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var rtList = dd.RequestTypes.Select(rt => rt).ToList();
                        dList = (from cd in cdList
                                 join ac in acList on cd.ID equals ac.CallID
                                 join pc in pcList on cd.RequesterID equals pc.ID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 join s in stList on cd.SiteID equals s.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 join rt in rtList on cd.RequestTypeID equals rt.ID
                                 select new AccessControl
                                 {
                                     CallID = ac.CallID,
                                     Name = pc.Name,
                                     Company = pp.PortFolio,
                                     Status = st.Name,
                                     RequestType = rt.Name,
                                     Site = s.Site1
                                 }).ToList();
                    }
                }
            }


            return dList;
        }
        #endregion

        #region Bind Delivery Report List

        public static List<DeliveryReport> BindDeliveryReportList()
        {

            List<DeliveryReport> dList = new List<DeliveryReport>();
            try
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    using (DCDataContext dd = new DCDataContext())
                    {
                        var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                        var diList = dd.DeliveryInformations.Select(d => d).ToList();
                        var cdList = dd.CallDetails.Select(c => c).ToList();
                        var sList = dd.Status.Select(s => s).ToList();
                        var riList = dd.RecievedInformations.Select(ri => ri).ToList();
                        int overduedays = dd.DeliveryDefaults.Select(d => d.OverdueDays.Value).FirstOrDefault();
                        var slList = dd.StorageLocations.Select(s => s).ToList();
                        dList = (from cd in cdList
                                 join di in diList on cd.ID equals di.CallID
                                 //join ri in riList on cd.ID equals ri.CallID
                                 join pp in ppList on cd.CompanyID equals pp.ID
                                 join st in sList on cd.StatusID equals st.ID
                                 // join sl in slList on ri.StorageLocationID equals sl.ID
                                 where cd.RequestTypeID == 1   // 1 = Delivery
                                 select new DeliveryReport
                                 {
                                     CallID = di.CallID,
                                     CourierNumber = di.CourierNumber,
                                     Status = st.Name,
                                     NumofBoxesRec = GetNumberofBoxes(riList, di.CallID.Value),
                                     DateRecieved = GetRecievedDate(riList, di.CallID.Value),
                                     BoxesRemaining = di.NumofBoxes - GetNumberofBoxes(riList, di.CallID.Value),
                                     StorageLocation = GetStoragelocationName(slList, riList, di.CallID.Value),
                                     OverdueDays = CalculateOverdueDays(GetDaysInStore(riList, di.CallID.Value), overduedays),
                                     TotalCost = GetTotalCost(riList, di.CallID.Value),
                                     PeriodCost = GetPeriodCost(riList, di.CallID.Value),
                                     Company = pp.PortFolio

                                 }).ToList();
                    }
                }
                //Convert.ToDateTime(riList.Where(p => p.CallID == di.CallID).FirstOrDefault().DateRecieved.Value)
                //CallID = di.CallID,
                //                         CourierNumber = di.CourierNumber,
                //                         Status = st.Name,
                //                         NumofBoxesRec = ri.NumofBoxesRec,
                //                         DateRecieved = (ri.DateRecieved.Value),
                //                         BoxesRemaining = di.NumofBoxes - ri.NumofBoxesRec,
                //                         StorageLocation = sl.Name,
                //                         OverdueDays = CalculateOverdueDays(ri.DaysInStore.Value,overduedays),
                //                         TotalCost = ri.TotalCost,
                //                         PeriodCost = ri.PeriodCost,
                //                         Company = pp.PortFolio
            }
            catch (Exception ex)
            { throw ex; }
            return dList;

        }
        #endregion
        private static DateTime GetRecievedDate( List<RecievedInformation> rlist, int callID)
        {
            DateTime DateRecieved = Convert.ToDateTime("01/01/1900");
            try
            {
                var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
                if (rItem != null)
                    DateRecieved = rItem.DateRecieved.HasValue ? rItem.DateRecieved.Value : Convert.ToDateTime("01/01/1900");
            }
            catch (Exception ex)
            { throw ex; }

            return DateRecieved;
        }
        private static string GetStoragelocationName(List<StorageLocation> slist, List<RecievedInformation> rlist, int callID)
        {
            string StorageLocation = string.Empty;
            try{
            var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
            if (rItem != null)
                StorageLocation = slist.Where(p => p.ID == rItem.StorageLocationID.Value).FirstOrDefault().Name;
             }
            catch (Exception ex)
            { throw ex; }
            return StorageLocation;
        }
        private static decimal GetTotalCost( List<RecievedInformation> rlist, int callID)
        {
            decimal tcost = 0;
            try{
            var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
            if (rItem != null)
                tcost = rItem.TotalCost.HasValue?rItem.TotalCost.Value:0;
             }
            catch (Exception ex)
            { throw ex; }
            return tcost;
        }
        private static int GetNumberofBoxes(List<RecievedInformation> rlist, int callID)
        {
            int tcost = 0;
            try{
            var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
            if (rItem != null)
                tcost = rItem.NumofBoxesRec.HasValue?rItem.NumofBoxesRec.Value:0;
            }
            catch (Exception ex)
            { throw ex; }
            return tcost;
        }
        private static int? GetDaysInStore(List<RecievedInformation> rlist, int callID)
        {
            int? tcost = 0;
            try{
            var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
            if (rItem != null)
                tcost = rItem.DaysInStore.HasValue?rItem.DaysInStore.Value:0;
            }
            catch (Exception ex)
            { throw ex; }
            return tcost;
        }
        private static decimal GetPeriodCost(List<RecievedInformation> rlist, int callID)
        {
            decimal tcost = 0;
            try{
            var rItem = rlist.Where(p => p.CallID == callID).FirstOrDefault();
            if (rItem != null)
                tcost = rItem.PeriodCost.HasValue?rItem.PeriodCost.Value:0;
            }
            catch (Exception ex)
            { throw ex; }
            return tcost;
        }
        private static int CalculateOverdueDays(int? daysinstore, int days)
        {
            int overduedays = 0;
            try{
            if (daysinstore.HasValue)
            {
                overduedays = daysinstore.Value - days;
                if (overduedays > 0)
                    return overduedays;
                else
                    return 0;
            }
            }
            catch (Exception ex)
            { throw ex; }
            return 0;
        }

       
    }
}