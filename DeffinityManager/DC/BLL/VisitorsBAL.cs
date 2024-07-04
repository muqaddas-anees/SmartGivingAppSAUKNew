using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
using System.Data.Linq;
using PortfolioMgt.DAL;

/// <summary>
/// Summary description for VisitorsBAL
/// </summary>
namespace DC.BAL
{
    public class VisitorsBAL
    {
        public VisitorsBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Insert Visitors
        public static void Visitors_Insert(Visitor v)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.Visitors.InsertOnSubmit(v);
                dc.SubmitChanges();
            }
        }
        #endregion

        #region Update visitors
        public static void Visitors_update(Visitor v)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                Visitor vi = (from a in dc.Visitors
                              where a.ID == v.ID
                              select a).FirstOrDefault();

                vi.ArriveStatus = v.ArriveStatus;
                vi.CallID = v.CallID;
                vi.Company = v.Company;
                vi.EmailAddress = v.EmailAddress;
                vi.Name = v.Name;
                vi.PhoneNumber = v.PhoneNumber;
                vi.PhotoID = v.PhotoID;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Select visitors
        public static List<Visitor> Visitors_selectAll()
        {
            List<Visitor> v = new List<Visitor>();
            using (DCDataContext dc = new DCDataContext())
            {
                v = (from vi in dc.Visitors select vi).ToList();
            }
            return v;
        }
        #endregion

        #region Check visitors exist
        public static bool CheckVisitorsExist(string Vname, int cid, string acno)
        {
            Visitor visitors = new Visitor();
            using (DCDataContext dc = new DCDataContext())
            {
                visitors = (from v in dc.Visitors
                            where v.Name == Vname && v.CallID == cid && v.AccessNo == acno
                            select v).FirstOrDefault();

            }
            if (visitors == null)
                return false;
            else
                return true;
        }
        #endregion

        #region Bind Visitors by callid

        public static List<Visitor> BindVisitorsByCallid()
        {
            List<Visitor> fList = new List<Visitor>();

            using (DCDataContext dd = new DCDataContext())
            {

                var rtList = dd.Visitors.Select(rt => rt).ToList();


                fList = (from cd in rtList

                         select new Visitor
                         {
                             ID = cd.ID,
                             CallID = cd.CallID,
                             Name = cd.Name,
                             Company = cd.Company,
                             EmailAddress = cd.EmailAddress,
                             PhoneNumber = cd.PhoneNumber,
                             ArriveStatus = cd.ArriveStatus,
                             DepartStatus = cd.DepartStatus,
                             ArrivalDate = cd.ArrivalDate,
                             DepatureDate = cd.DepatureDate,
                             PhotoID = cd.PhotoID,
                             AccessNo = cd.AccessNo
                         }).ToList();

            }
            return fList;
        }
        #endregion

      
        
        #region Bind Visitors
        public static List<VisitorDetails> BindVisitors(string acno)
        {
            List<VisitorDetails> fList = new List<VisitorDetails>();

            using (DCDataContext dd = new DCDataContext())
            {

                var rtList = dd.Visitors.Select(rt => rt).ToList();


                fList = (from cd in rtList
                         where cd.AccessNo == acno

                         select new VisitorDetails
                         {
                             ID = cd.ID,
                             CallID = cd.CallID,
                             Name = cd.Name,
                             Company = cd.Company,
                             EmailAddress = cd.EmailAddress,
                             PhoneNumber = cd.PhoneNumber,
                             ArriveStatus = cd.ArriveStatus,
                             DepartStatus = cd.DepartStatus,
                             ArrivalDate = cd.ArrivalDate,
                             DepatureDate = cd.DepatureDate,
                             PhotoID = cd.PhotoID,
                             NoShow = cd.NoShow
                         }).ToList();

            }
            return fList;
        }
        #endregion

        #region Select visitors by id
        public static Visitor Visitors_selectByID(int id)
        {
            Visitor v = new Visitor();
            using (DCDataContext dc = new DCDataContext())
            {

                v = (from vi in dc.Visitors
                     where vi.ID == id
                     select vi).FirstOrDefault();
            }

            return v;
        }
        #endregion


        #region Delete visitors
        public static void Visitors_Delete(int id)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                var v = (from vi in dc.Visitors
                         where vi.ID == id
                         select vi).FirstOrDefault();
                dc.Visitors.DeleteOnSubmit(v);
                dc.SubmitChanges();

            }


        }
        #endregion

        #region Insert Visitors journal
        public static void VisitorsJournal_Insert(VisitorsJournal vj)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.VisitorsJournals.InsertOnSubmit(vj);
                dc.SubmitChanges();
            }
        }
        #endregion


        #region Update visitor journal
        public static void VisitorJournal_update(VisitorsJournal v)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                VisitorsJournal vj = (from vs in dc.VisitorsJournals
                                      where vs.ID == v.ID
                                      select vs).FirstOrDefault();

                vj.ArriveDate = v.ArriveDate;
                vj.DepartDate = v.DepartDate;
                vj.VisitorID = v.VisitorID;
                dc.SubmitChanges();
            }

        }
        #endregion

        #region Select visitors journal
        public static List<VisitorsJournal> VisitorsJournal_selectAll()
        {
            List<VisitorsJournal> vj = new List<VisitorsJournal>();
            using (DCDataContext dc = new DCDataContext())
            {
                vj = (from v in dc.VisitorsJournals select v).ToList();
            }
            return vj;
        }
        #endregion

        #region Select visitor journal by id
        public static VisitorsJournal VisitorsJournal_selectByID(int vid)
        {
            VisitorsJournal vj = new VisitorsJournal();
            using (DCDataContext dc = new DCDataContext())
            {

                vj = (from v in dc.VisitorsJournals
                      where v.VisitorID == vid
                      select v).FirstOrDefault();
            }

            return vj;
        }
        #endregion
        #region Select visitor journal by ids
        public static List<VisitorsJournal> VisitorsJournal_selectByIDs(int vid)
        {
            List<VisitorsJournal> vj = new List<VisitorsJournal>();
            using (DCDataContext dc = new DCDataContext())
            {

                vj = (from v in dc.VisitorsJournals
                      where v.VisitorID == vid
                      select v).ToList();
            }

            return vj;
        }
        public static List<VisitorsJournal> VisitorsJournal_CustomerVisible_selectByIDs(int vid)
        {
            return VisitorsJournal_selectByIDs(vid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion

        #region Select visitor journal by  visitorID
        public static VisitorsJournal VisitorsJournal_selectByVisitorID(int id)
        {
            VisitorsJournal vj = new VisitorsJournal();
            using (DCDataContext dc = new DCDataContext())
            {

                vj = (from v in dc.VisitorsJournals
                      where v.VisitorID == id
                      select v).FirstOrDefault();
            }

            return vj;
        }
        #endregion

        #region Delete visitor journal
        public static void VisitorJournal_Delete(int id)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                var vj = (from v in dc.VisitorsJournals
                          where v.ID == id
                          select v).FirstOrDefault();
                dc.VisitorsJournals.DeleteOnSubmit(vj);
                dc.SubmitChanges();

            }


        }
        #endregion

        #region Select VisitorsJournal by CallID and VisitorID
        public static List<VisitorsJournal> SelectVisitorsJournalbyCallIDAndVisitorID(int cid, int vid)
        {
            List<VisitorsJournal> visitorslist = new List<VisitorsJournal>();
            using (DCDataContext dd = new DCDataContext())
            {

                visitorslist = dd.VisitorsJournals.Where(v => v.CallID == cid && v.VisitorID == vid).Select(v => v).ToList();
                return visitorslist;
            }
        }
        #endregion
        #region Select VisitorsJournal by CallID
        public static List<VisitorsJournal> SelectVisitorsJournalbyCallID(int cid)
        {
            List<VisitorsJournal> visitorslist = new List<VisitorsJournal>();
            using (DCDataContext dd = new DCDataContext())
            {

                visitorslist = dd.VisitorsJournals.Where(v => v.CallID == cid).Select(v => v).ToList();
                return visitorslist;
            }
        }
        #endregion
        #region Select VisitorsJournal by VisitorID
        public static List<VisitorsJournal> SelectVisitorsJournalbyVisitorID(int vid)
        {
            List<VisitorsJournal> visitorslist = new List<VisitorsJournal>();
            using (DCDataContext dd = new DCDataContext())
            {

                visitorslist = dd.VisitorsJournals.Where(v => v.VisitorID == vid).Select(v => v).ToList();
                return visitorslist;
            }
        }
        #endregion
        #region Select Visitors by CallID
        public static int[] SelectVisitorsbyCallID(int cid)
        {
            int[] visitors = null;
            using (DCDataContext dd = new DCDataContext())
            {

                visitors = dd.Visitors.Where(v => v.CallID == cid).Select(v => v.ID).ToArray<int>();

            }
            return visitors;
        }
        #endregion


        #region Select Visitors by callid and access number
        public static List<Visitor> SelectVisitorsByCallIDandAccessNo(int cid, string acno)
        {
            List<Visitor> visitors = new List<Visitor>();
            using (DCDataContext dc = new DCDataContext())
            {

                visitors = (from v in dc.Visitors
                            where v.CallID == cid && v.AccessNo == acno
                            select v).ToList();
            }
            return visitors;
        }
        #endregion
        #region Select visitors
        public static List<AccessReport> Visitors_SecurityModule()
        {
            List<AccessReport> v = new List<AccessReport>();
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (DCDataContext dc = new DCDataContext())
                {

                    var visitors = dc.Visitors.Select(vi => vi).ToList();
                    var vijour = dc.VisitorsJournals.Select(vi => vi).ToList();
                    var aclist = dc.AccessControls.Select(vi => vi).ToList();
                    var clist = dc.CallDetails.Select(vi => vi).ToList();
                    var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                    v = (from c in clist
                         join vst in dc.Visitors on c.ID equals vst.CallID
                         join ac in dc.AccessControls on c.ID equals ac.CallID
                         //join vj in vijour on vst.ID equals vj.VisitorID
                         join pp in ppList on c.CompanyID equals pp.ID
                         where c.RequestTypeID == 3
                         select new AccessReport
                         {
                             ID = vst.ID,
                             CallID = vst.CallID,
                             LoggedDate = Convert.ToDateTime(c.LoggedDate),
                             Name = vst.Name,
                             VisitorCompany = vst.Company,
                             PurposeofVisit = ac.PurposeOfVisitID,
                             Status = c.StatusID,
                             ArriveDate = vst.ArrivalDate,
                             DepartDate = vst.DepatureDate,
                             //VisitingDate = GetVisitingDatepart(vst.VisitingTime.Value.ToString()),
                             EmailAddress = vst.EmailAddress,
                             ArriveStatus = Convert.ToBoolean(vst.ArriveStatus),
                             IsPhotoID = Convert.ToBoolean(vst.PhotoID),
                             PhoneNumber = vst.PhoneNumber,
                             AccessNumber = vst.AccessNo,
                             Company = pp.PortFolio,
                             RequestedDate = ac.RequestedDate.Value

                         }).ToList();


                }
            }
            return v;
        }
        #endregion

        #region Check visitors depart status
        public static bool CheckVisitorsDepartStatus(string acno)
        {
            Visitor visitors = new Visitor();
            using (DCDataContext dc = new DCDataContext())
            {
                visitors = (from v in dc.Visitors
                            where v.AccessNo == acno &&( v.DepatureDate == null || v.ArrivalDate == null)
                            select v).FirstOrDefault();

            }
            if (visitors == null)
                return false;
            else
                return true;
        }
        #endregion
        #region Check visitors depart status
        public static bool CheckVisitorsDepartStatusByCallid(int cid)
        {
            Visitor visitors = new Visitor();
            using (DCDataContext dc = new DCDataContext())
            {
                visitors = (from v in dc.Visitors
                            where v.CallID == cid && (v.DepatureDate == null || v.ArrivalDate == null)
                            select v).FirstOrDefault();

            }
            if (visitors == null)
                return false;
            else
                return true;
        }
        public static bool CheckVisitorsArriveStatusByCallid(int cid)
        {
            Visitor visitors = new Visitor();
            using (DCDataContext dc = new DCDataContext())
            {
                visitors = (from v in dc.Visitors
                            where v.CallID == cid &&  v.ArrivalDate == null
                            select v).FirstOrDefault();

            }
            if (visitors == null)
                return false;
            else
                return true;
        }
        #endregion

        #region Select visitors
        public static List<Visitor> Visitors_selectByAccessNo(string acno)
        {
            List<Visitor> v = new List<Visitor>();
            using (DCDataContext dc = new DCDataContext())
            {
                v = (from vst in dc.Visitors
                     where vst.AccessNo == acno
                     select vst).ToList();
            }
            return v;
        }
        #endregion
    }
}