using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for FLSDepartment
    /// </summary>


    public class FLSDepartment
    {
        public FLSDepartment()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region BindDepartment
        public static List<AssignedDepartment> Bind()
        {
            List<AssignedDepartment> dept = new List<AssignedDepartment>();
            using (DCDataContext dd = new DCDataContext())
            {
                dept = dd.AssignedDepartments.OrderBy(r=>r.DepartmentName).Select(r => r).ToList();
            }
            return dept;
        }
        #endregion
        #region Check Exists when Inserting
        public static bool CheckExists(string name, int customerID)
        {

            AssignedDepartment dept = new AssignedDepartment();
            using (DCDataContext dd = new DCDataContext())
            {
                dept = dd.AssignedDepartments.Where(r => r.DepartmentName.ToLower() == name.ToLower() && r.CustomerID == customerID).Select(r => r).FirstOrDefault();
            }
            if (dept != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region AddDepartment
        public static void Add(AssignedDepartment dept)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.AssignedDepartments.InsertOnSubmit(dept);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select by ID
        public static AssignedDepartment SelectById(int id)
        {

            AssignedDepartment dept = new AssignedDepartment();
            using (DCDataContext dd = new DCDataContext())
            {
                dept = dd.AssignedDepartments.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return dept;
        }
        #endregion
        #region Check Exists when Updating
        public static bool CheckByIdUpdate(int id, string name, int customerID)
        {

            AssignedDepartment dept = new AssignedDepartment();
            using (DCDataContext dd = new DCDataContext())
            {
                dept = dd.AssignedDepartments.Where(r => r.ID != id && r.DepartmentName.ToLower() == name.ToLower() && r.CustomerID==customerID).Select(r => r).FirstOrDefault();
            }
            if (dept != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region UpdateDepartment
        public static void Update(AssignedDepartment dept)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                AssignedDepartment Current = dd.AssignedDepartments.Where(r => r.ID == dept.ID).Select(r => r).FirstOrDefault();
                Current.DepartmentName = dept.DepartmentName;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region DeleteDepartmentByID
        public static void DeleteById(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                AssignedDepartment dept = dd.AssignedDepartments.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (dept != null)
                {
                    dd.AssignedDepartments.DeleteOnSubmit(dept);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}