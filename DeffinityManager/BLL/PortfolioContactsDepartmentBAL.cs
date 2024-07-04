using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BLL
{
/// <summary>
/// Summary description for PortfolioContactsDepartmentBAL
/// </summary>
    public class PortfolioContactsDepartmentBAL
    {
        public PortfolioContactsDepartmentBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of PortfolioContactsDepartment
        public static IEnumerable<PortfolioContactsDepartment> GetPortfolioContactsDepartmentList()
        {
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                return db.PortfolioContactsDepartments.OrderBy(d=>d.Name).ToList();
            }
        }
        #endregion

        #region PortfolioContactsDepartment insert
        public static void AddPortfolioContactsDepartment(PortfolioContactsDepartment portfolioContactsDepartment)
        {
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                db.PortfolioContactsDepartments.InsertOnSubmit(portfolioContactsDepartment);
                db.SubmitChanges();
            }
        }
        #endregion

        #region PortfolioContactsDepartment update
        public static void UpdatePortfolioContactsDepartment(PortfolioContactsDepartment portfolioContactsDepartment)
        {
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                PortfolioContactsDepartment portfolioContactsDepartmentCurrent = db.PortfolioContactsDepartments.Where(r => r.ID == portfolioContactsDepartment.ID).FirstOrDefault();
                portfolioContactsDepartmentCurrent.Name = portfolioContactsDepartment.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check PortfolioContactsDepartment when an insert
        public static bool CheckPortfolioContactsDepartment(string name, int customerId)
        {
            bool exists = false;
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                PortfolioContactsDepartment portfolioContactsDepartment = db.PortfolioContactsDepartments.Where(r => r.Name == name && r.CustomerID == customerId).FirstOrDefault();
                if (portfolioContactsDepartment != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check PortfolioContactsDepartment when an update
        public static bool CheckPortfolioContactsDepartment(int id, string name,int customerId)
        {
            bool exists = false;
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                PortfolioContactsDepartment portfolioContactsDepartment = db.PortfolioContactsDepartments.Where(r => r.ID != id && r.Name == name && r.CustomerID==customerId).FirstOrDefault();
                if (portfolioContactsDepartment != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by PortfolioContactsDepartment Id
        public static PortfolioContactsDepartment SelectByID(int id)
        {
            PortfolioContactsDepartment portfolioContactsDepartment = new PortfolioContactsDepartment();
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                portfolioContactsDepartment = db.PortfolioContactsDepartments.Where(r => r.ID == id).FirstOrDefault();
            }
            return portfolioContactsDepartment;
        }

        #endregion

        #region Delete PortfolioContactsDepartment by Id
        public static void DeleteByID(int id)
        {
            using (PortfolioDataContext db = new PortfolioDataContext())
            {
                PortfolioContactsDepartment portfolioContactsDepartment = db.PortfolioContactsDepartments.Where(r => r.ID == id).FirstOrDefault();
                if (portfolioContactsDepartment != null)
                {
                    db.PortfolioContactsDepartments.DeleteOnSubmit(portfolioContactsDepartment);
                    db.SubmitChanges();
                }
            }
        }
        #endregion
        #region Delete all Department by Id
        public static void DeteleaReqDepttoAllCustomers(string ReqDept)
        {
            using (PortfolioDataContext dc = new PortfolioDataContext())
            {
                List<PortfolioContactsDepartment> l = new List<PortfolioContactsDepartment>();
                l = (from a in dc.PortfolioContactsDepartments where a.Name == ReqDept select a).ToList();
                dc.PortfolioContactsDepartments.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}