using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for ReceivedInformationBAL
    /// </summary>
    public class ReceivedInformationBAL
    {
        #region Bind ReceivedInformation
        public static List<RecievedInformation> BindReceivedInformation()
        {
            List<RecievedInformation> riList = new List<RecievedInformation>();
            using (DCDataContext dd = new DCDataContext())
            {
                riList = dd.RecievedInformations.Select(r => r).ToList();
            }
            return riList;
        }
        #endregion

        #region Add ReceivedInformation
        public static void AddReceivedInformation(RecievedInformation ri)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.RecievedInformations.InsertOnSubmit(ri);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select ReceivedInformation by ID
        public static RecievedInformation SelectbyId(int id)
        {

            RecievedInformation ri = new RecievedInformation();
            using (DCDataContext dd = new DCDataContext())
            {
                ri = dd.RecievedInformations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return ri;
        }
        #endregion
        #region Select ReceivedInformation by CallID
        public static RecievedInformation SelectbyCallId(int cid)
        {

            RecievedInformation ri = new RecievedInformation();
            using (DCDataContext dd = new DCDataContext())
            {
                ri = dd.RecievedInformations.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return ri;
        }
        #endregion
        # region Update ReceivedInformation
        public static void ReceivedInformationUpdate(RecievedInformation ri)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                RecievedInformation ricurrent = dd.RecievedInformations.Where(r => r.ID == ri.ID).Select(r => r).FirstOrDefault();
                ricurrent.CallID = ri.CallID;
                ricurrent.ConditionID = ri.ConditionID;
                ricurrent.NumofBoxesRec = ri.NumofBoxesRec;
                ricurrent.StorageLocationID = ri.StorageLocationID;
                //ricurrent.Notes = ri.Notes;
                ricurrent.DateRecieved = ri.DateRecieved;
                ricurrent.DaysInStore = ri.DaysInStore;
                ricurrent.ChargeableDate = ri.ChargeableDate;
                ricurrent.TotalCost = ri.TotalCost;
                ricurrent.StoragePeriodFrom = ri.StoragePeriodFrom;
                ricurrent.StoragePeriodTo = ri.StoragePeriodTo;
                ricurrent.PeriodCost = ri.PeriodCost;
                ricurrent.OurSiteID = ri.OurSiteID;
                ricurrent.NumofBoxesCollected = ri.NumofBoxesCollected;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region ReceivedInformation By ID
        public static void ReceivedInformationDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                RecievedInformation ri = dd.RecievedInformations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (ri != null)
                {
                    dd.RecievedInformations.DeleteOnSubmit(ri);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}