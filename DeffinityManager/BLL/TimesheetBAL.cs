using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

/// <summary>
/// Summary description for TimesheetBAL
/// </summary>
namespace TimesheetMgt.BAL
{
    public class TimesheetBAL
    {
        public List<v_TimesheetEntryCustom> TimesheetentrycustomCollection = null;

       
        public List<v_TimesheetEntryCustom> GetTimesheetentrycustomCollection()
        {

            using (TimesheetMgtDataContext td = new TimesheetMgtDataContext())
            {
                TimesheetentrycustomCollection = (from p in td.v_TimesheetEntryCustoms
                                                 select p).ToList();
            }

            return TimesheetentrycustomCollection;
        }

        public List<v_TimesheetEntryCustom> GetTimesheetentrycustomCollectionByDaytype(string daytype)
        {
            TimesheetentrycustomCollection = GetTimesheetentrycustomCollection().Where(p=>p.Daytype == daytype || p.ID==-99).ToList();

            return TimesheetentrycustomCollection;
        }

        public void Timesheetentrycustom_Insert(TimesheetEntryCustom te)
        {
            using (TimesheetMgtDataContext tm = new TimesheetMgtDataContext())
            {
                tm.TimesheetEntryCustoms.InsertOnSubmit(te);
                tm.SubmitChanges();
            }
        }
        public void Timesheetentrycustom_update(int ID,TimeSpan? fromtime,TimeSpan? totime,int? entrytype,int? categoryId,string daytype, DateTime? customdate )
        {
            using (TimesheetMgtDataContext td = new TimesheetMgtDataContext())
            {
                TimesheetEntryCustom t = td.TimesheetEntryCustoms.Single(P => P.ID == ID);
                t.ID = ID;
                if(fromtime.HasValue)
                    t.FromTime = fromtime;
                if (totime.HasValue)
                    t.ToTime = totime;
                if(entrytype.HasValue)
                
                t.TimesheetEntryTypeID = entrytype;
                if (categoryId.HasValue)
                    t.CategoryID = categoryId;
                if(!string.IsNullOrEmpty(daytype))
                t.Daytype = daytype;
                if (customdate.HasValue)
                    t.CustomDate = customdate;
                td.SubmitChanges();                

            }
        }
       
    }
}