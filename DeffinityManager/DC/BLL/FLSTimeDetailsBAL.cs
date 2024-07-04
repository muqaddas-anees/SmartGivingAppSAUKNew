using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
namespace DC.BLL
{
    /// <summary>
    /// Summary description for FLSTimeDetailsBAL
    /// </summary>
    public class FLSTimeDetailsBAL
    {
        #region Add FLS Time Details
        public static void AddFLSTimeDetail(FLSTimeDetail ft)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.FLSTimeDetails.InsertOnSubmit(ft);
                dd.SubmitChanges();
            }
        }
        #endregion

        #region Select FLS Time Details By CallID and Status
        public static FLSTimeDetail SelectFLSTimeDetailsByID(int id, string status)
        {
            using(DCDataContext dd=new DCDataContext())
            {
                FLSTimeDetail ft = dd.FLSTimeDetails.Where(f => f.CallID == id && f.Status == status).OrderBy(f=>f.StatusTime).FirstOrDefault();
                return ft;
            }
        }
        #endregion

        #region Delete FLS Time Details By CallId
        public static void DeleteFLSTimeDetails(int id)
        {
            using(DCDataContext dd=new DCDataContext())
            {
                FLSTimeDetail ft = dd.FLSTimeDetails.Where(f => f.CallID == id).FirstOrDefault();
                if(ft!=null)
                {
                    dd.FLSTimeDetails.DeleteOnSubmit(ft);
                    dd.SubmitChanges();
                }
            }
        }

        #endregion

    }
}
