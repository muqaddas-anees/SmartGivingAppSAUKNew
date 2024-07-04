using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for FLSAdditionalInfoBAL
    /// </summary>
    public class FLSAdditionalInfoBAL
    {
        public FLSAdditionalInfoBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertFLSAdditionalInfo(FLSAdditionalInfo flsAdditionalInfo)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.FLSAdditionalInfos.InsertOnSubmit(flsAdditionalInfo);
                db.SubmitChanges();
            }
        }
        public static void UpdateFLSAddtionalInfo(FLSAdditionalInfo flsAdditionalInfo)
        {
            using (DCDataContext db = new DCDataContext())
            {
                var currentFLSAddtionalInfo = db.FLSAdditionalInfos.Where(f => f.ID == flsAdditionalInfo.ID).FirstOrDefault();
                if (currentFLSAddtionalInfo!=null)
                {
                    currentFLSAddtionalInfo.CustomFieldValue = flsAdditionalInfo.CustomFieldValue;
                    currentFLSAddtionalInfo.CustomFieldValueExt = flsAdditionalInfo.CustomFieldValueExt;
                    currentFLSAddtionalInfo.Hours = flsAdditionalInfo.Hours;
                    currentFLSAddtionalInfo.Minutes = flsAdditionalInfo.Minutes;
                    currentFLSAddtionalInfo.FileID = flsAdditionalInfo.FileID;
                    currentFLSAddtionalInfo.FileName = flsAdditionalInfo.FileName;
                    db.SubmitChanges();
                }
            }
        }

        public static FLSAdditionalInfo GetFLSAdditionalInfoByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSAdditionalInfos.Where(f => f.ID == id).FirstOrDefault();
            }
        }
        public static List<FLSAdditionalInfo> GetFLSAdditonalInfoByCallID(int callId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSAdditionalInfos.Where(f => f.CallID == callId).ToList();

            }
        }

        public static bool GetFLSAdditonalInfoByCallID_Delete(int callId)
        {
            bool retval = false;
            using (DCDataContext db = new DCDataContext())
            {
                var list = db.FLSAdditionalInfos.Where(f => f.CallID == callId).ToList();
                if (list.Count > 0)
                {
                    db.FLSAdditionalInfos.DeleteAllOnSubmit(list);
                    db.SubmitChanges();
                    retval = true;
                }
            }
            return retval;
        }

    }
}