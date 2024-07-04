using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for CustomFormDesignerBAL
    /// </summary>
    public class CustomFormDesignerBAL
    {
        public CustomFormDesignerBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //find the first data record
        public static int UpdateRowPostion(int id,int panelid,int newpos, int oldpos)
        {
            int retval = 0;
            using (DCDataContext db = new DCDataContext())
            {
                db.FLSCustomFields_UpdatePosition(newpos, oldpos, id, panelid);
                retval = 1;
            }
            return retval;
        }

        public static FLSCustomField findfirstRecord(int customerId)
        {
           
            using (DCDataContext db = new DCDataContext())
            {
              return   db.FLSCustomFields.Where(c => c.CustomerID == customerId).OrderByDescending(o=>o.ID).FirstOrDefault();
              
            }
           
        }
       

        public static int findformtRecord(int formID)
        {
            int id = 0;
            using (DCDataContext db = new DCDataContext())
            {
                FLSCustomField flsCustomField = db.FLSCustomFields.Where(c => c.FormID == formID).OrderByDescending(o => o.ID).FirstOrDefault();
                if (flsCustomField != null)
                {
                    if (flsCustomField.PartnerServiceID.HasValue)
                    {
                        id = flsCustomField.PartnerServiceID.Value;
                    }
                    else
                    {
                        id = 0;
                    }
                }
                else
                    id = 0;
            }
            return id;
        }
        #region Check postion when insert 
        public static bool CheckPosition(int customerId, string fieldPostion)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSCustomField flsCustomField = db.FLSCustomFields.Where(c => c.CustomerID == customerId && c.FieldPosition == fieldPostion).FirstOrDefault();
                if (flsCustomField != null)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region Check postion when update
        public static bool CheckPosition(int customerId, int fieldId, string fieldPostion)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSCustomField flsCustomField = db.FLSCustomFields.Where(c => c.CustomerID == customerId && c.ID != fieldId && c.FieldPosition == fieldPostion).FirstOrDefault();
                if (flsCustomField != null)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        public static IEnumerable<FLSCustomField> GetFieldByServiceID(int serviceID)
        {

            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.PartnerServiceID == serviceID).ToList();
            }

        }
        public static IEnumerable<FLSCustomField> GetFieldByPartner(int serviceID)
        {

            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.PartnerServiceID == serviceID).ToList();
            }

        }
        public static IEnumerable<FLSCustomField> GetFieldByPanel(int serviceID)
        {

            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.PartnerServiceID == serviceID).ToList();
            }

        }
        public static IEnumerable<FLSCustomField> GetFieldByFormID(int formID)
        {

            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.PartnerSubcategoryID == formID).ToList();
            }

        }
        public static IEnumerable<FLSCustomField> GetFieldList(int customerId,int formID)
        {
           
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.CustomerID == customerId ).ToList();
            }

        }

        public static FLSCustomField GetFieldByID(int fieldId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.ID == fieldId).FirstOrDefault();
            }
        }

        public static List<FLSCustomField> GetFieldListByID(int fieldId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSCustomFields.Where(f => f.ID == fieldId).ToList();
            }
        }

        public static void AddFields(FLSCustomField flsCustomField)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.FLSCustomFields.InsertOnSubmit(flsCustomField);
                db.SubmitChanges();
            }
        }

        public static void UpdateFields(FLSCustomField flsCustomField)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSCustomField flsCustomFieldCurrent = db.FLSCustomFields.Where(f => f.ID == flsCustomField.ID).FirstOrDefault();
                if (flsCustomFieldCurrent != null)
                {
                    flsCustomFieldCurrent.TypeOfField = flsCustomField.TypeOfField;
                    flsCustomFieldCurrent.LabelName = flsCustomField.LabelName;
                    flsCustomFieldCurrent.DefaultText = flsCustomField.DefaultText;
                    flsCustomFieldCurrent.MinimumValue = flsCustomField.MinimumValue;
                    flsCustomFieldCurrent.MaximumValue = flsCustomField.MaximumValue;
                    flsCustomFieldCurrent.ListValue = flsCustomField.ListValue;
                    flsCustomFieldCurrent.Mandatory = flsCustomField.Mandatory;
                    flsCustomFieldCurrent.FieldPosition = flsCustomField.FieldPosition;
                    flsCustomFieldCurrent.PartnerSubcategoryID = flsCustomField.PartnerSubcategoryID;
                    flsCustomFieldCurrent.PartnerServiceID = flsCustomField.PartnerServiceID;
                    flsCustomFieldCurrent.ListValueExt = flsCustomField.ListValueExt;
                    flsCustomFieldCurrent.Position = flsCustomField.Position;
                    db.SubmitChanges();
                }
            }
        }

        public static void DeleteField(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSCustomField flsCustomField = db.FLSCustomFields.Where(c => c.ID == id).FirstOrDefault();
                if (flsCustomField != null)
                {
                    db.FLSCustomFields.DeleteOnSubmit(flsCustomField); 
                    db.SubmitChanges();
                }
            }
        }
    }



}