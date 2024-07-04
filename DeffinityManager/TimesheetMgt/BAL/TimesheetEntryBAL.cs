using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetMgt.Entity;

namespace TimesheetMgt.BAL
{
    public class TimesheetEntryBAL
    {

        public static IQueryable<v_timesheetentry> TimesheetBAL_SelectAll()
        {
            ITimesheetRepository<TimesheetMgt.Entity.v_timesheetentry> tRep = new TimesheetRepository<TimesheetMgt.Entity.v_timesheetentry>();
            return tRep.GetAll();


        }

        public static IQueryable<v_timesheetentry> TimesheetBAL_SelectAll(int portfolioid)
        {
            ITimesheetRepository<TimesheetMgt.Entity.v_timesheetentry> tRep = new TimesheetRepository<TimesheetMgt.Entity.v_timesheetentry>();
            return tRep.GetAll().Where(o=> o.PorfolioID == portfolioid);


        }
    }
}
