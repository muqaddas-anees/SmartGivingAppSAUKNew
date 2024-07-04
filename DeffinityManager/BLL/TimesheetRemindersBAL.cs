using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
/// <summary>
/// Summary description for TimesheetRemindersBAL
/// </summary>
public class TimesheetRemindersBAL
{
	public TimesheetRemindersBAL()
	{

	}
    public void Insert(TimesheetRemindersTbl t)
    {
        using (TimeSheetDataContext dc = new TimeSheetDataContext())
        {
            dc.TimesheetRemindersTbls.InsertOnSubmit(t);
            dc.SubmitChanges();
        }
    }
    public void Update(TimesheetRemindersTbl t)
    {
        using (TimeSheetDataContext dc = new TimeSheetDataContext())
        {
            var x = (from a in dc.TimesheetRemindersTbls select a).FirstOrDefault();
            x.SwitchOnTimesheetAlert = t.SwitchOnTimesheetAlert;
            x.SendReminderAt = t.SendReminderAt;
            x.Avoidweekends = t.Avoidweekends;
            dc.SubmitChanges();
        }
    }
    public TimesheetRemindersTbl Select()
    {
        using (TimeSheetDataContext dc = new TimeSheetDataContext())
        {
            TimesheetRemindersTbl x = (from a in dc.TimesheetRemindersTbls select a).FirstOrDefault();
            return x;
        }
    }
}