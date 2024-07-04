
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for SecurityAccessMail
    /// </summary>
    public class SecurityAccessMail
    {
        public SecurityAccessMail()
        {
            //
            // TODO: Add constructor logic here
            //
        }
      
        #region List of Contractors
        public static List<UserMgt.Entity.Contractor> BindContractor()
        {
            var llist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
            List<UserMgt.Entity.Contractor> contactorsList = new List<UserMgt.Entity.Contractor>();
            using(UserDataContext udc=new UserDataContext())
            {
                contactorsList = udc.Contractors.Where(r => r.Status.ToLower() == "active" && r.SID != 8 && r.SID != 10).Where(o => llist.Contains(o.ID)).OrderBy(r => r.ContractorName).Select(r => r).ToList();
            }
            return contactorsList;
        }

        #region List of Mail Manager
        public static IList BindManager(int rtid, int customerID)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                using (UserDataContext udc = new UserDataContext())
                {
                    var x = dd.Managers.Where(m=>m.CustomerID==customerID).Select(r => r).ToList();
                    var y = udc.Contractors.Select(r => r).ToList();
                    var result = (from p in x
                                  join o in y on p.UserID equals o.ID
                                  where p.RequestTypeID == rtid && o.Status.ToLower() == "active"
                                  orderby o.ContractorName
                                  select new { p.ID, p.UserID, o.ContractorName }).ToList();
                    return result;
                }
            }
        }
        #endregion
        #endregion
        #region Check Exists when Inserting
        public static bool CheckExists(int id, int rtid, int customerId)
        {

            Manager manager = new Manager();
            using (DCDataContext dd = new DCDataContext())
            {
                manager = dd.Managers.Where(r => r.UserID == id && r.RequestTypeID == rtid && r.CustomerID == customerId).Select(r => r).FirstOrDefault();
            }
            if (manager != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Insert Mail Manager
        public static void Add(Manager manager)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.Managers.InsertOnSubmit(manager);
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete MailManager By ID
        public static void DeleteById(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                Manager sub = dd.Managers.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (sub != null)
                {
                    dd.Managers.DeleteOnSubmit(sub);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
   
    #region Get Email Addresses
        public static List<string> GetEmailAddresses(List<int> idlistt)
        {
            using (UserDataContext udc = new UserDataContext())
            {
                List<string> strList = new List<string>();
                strList = udc.Contractors.Where(r => idlistt.Contains(r.ID) && r.Status.ToLower() == "active").Select(r => r.EmailAddress).ToList();
                return strList;
            }
        }
    #endregion
        #region Get  Distribution List IDs
        public static List<int> GetIds(int tid, int customerId)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    List<int> idLsit = new List<int>();
                    List<UserMgt.Entity.Contractor> list_users = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    List<DC.Entity.Manager> list_managers = dc.Managers.Where(p => p.RequestTypeID == tid && p.CustomerID == customerId).Select(p => p).ToList();
                    idLsit = (from m in list_managers
                              join c in list_users
                             on m.UserID equals c.ID
                              select m.UserID.Value).ToList();
                    //idLsit = dc.Managers.Where(r => r.RequestTypeID == tid).Select(r => r.UserID.Value).ToList();
                    return idLsit;
                }
            }
        }
        public static List<int> GetIds(int tid)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    List<int> idLsit = new List<int>();
                    List<UserMgt.Entity.Contractor> list_users = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    List<DC.Entity.Manager> list_managers = dc.Managers.Where(p => p.RequestTypeID == tid ).Select(p => p).ToList();
                    idLsit = (from m in list_managers
                              join c in list_users
                             on m.UserID equals c.ID
                              select m.UserID.Value).ToList();
                    //idLsit = dc.Managers.Where(r => r.RequestTypeID == tid).Select(r => r.UserID.Value).ToList();
                    return idLsit;
                }
            }
        }
        #endregion
        #region Delete Users By id
        public static void DeteleAllUsers(int ddlid)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                List<Manager> l = new List<Manager>();
                l = (from a in dc.Managers where a.UserID == ddlid select a).ToList();
                dc.Managers.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}