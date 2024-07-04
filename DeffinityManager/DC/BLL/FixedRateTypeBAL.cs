using DC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Entity;

namespace DC.BLL
{
    public class FixedRateTypeBAL
    {


        #region BindSubject
        public static List<FixedRateType> Bind()
        {
            List<FixedRateType> FixedRateTypeList = new List<FixedRateType>();
            using (DCDataContext dd = new DCDataContext())
            {
                FixedRateTypeList = dd.FixedRateTypes.OrderBy(r => r.FixedRateTypeName).Select(r => r).ToList();
            }
            return FixedRateTypeList;
        }
        #endregion
        #region Check Exists when Inserting
        public static bool CheckExists(string name, int customerId)
        {

            FixedRateType subject = new FixedRateType();
            using (DCDataContext dd = new DCDataContext())
            {
                subject = dd.FixedRateTypes.Where(r => r.FixedRateTypeName.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (subject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Add Subject
        public static void Add(FixedRateType sub)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.FixedRateTypes.InsertOnSubmit(sub);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Subject by ID
        public static FixedRateType SelectById(int id)
        {

            FixedRateType fixedRateType = new FixedRateType();
            using (DCDataContext dd = new DCDataContext())
            {
                fixedRateType = dd.FixedRateTypes.Where(r => r.FixedRateTypeID == id).Select(r => r).FirstOrDefault();
            }
            return fixedRateType;
        }
        #endregion
        #region Check Exists when Updating
        public static bool CheckByIdUpdate(int id, string name, int customerId)
        {

            FixedRateType subject = new FixedRateType();
            using (DCDataContext dd = new DCDataContext())
            {
                subject = dd.FixedRateTypes.Where(r => r.FixedRateTypeID != id && r.FixedRateTypeName.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (subject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region Update Subject
        public static void Update(FixedRateType fixedRateType)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                FixedRateType subCurrent = dd.FixedRateTypes.Where(r => r.FixedRateTypeID == fixedRateType.FixedRateTypeID).Select(r => r).FirstOrDefault();
                subCurrent.FixedRateTypeName = fixedRateType.FixedRateTypeName;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Subject By ID
        public static void DeleteById(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                FixedRateType sub = dd.FixedRateTypes.Where(r => r.FixedRateTypeID == id).Select(r => r).FirstOrDefault();
                if (sub != null)
                {
                    dd.FixedRateTypes.DeleteOnSubmit(sub);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}
