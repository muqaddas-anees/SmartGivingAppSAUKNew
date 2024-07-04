using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface ITimesheetRepository<T> : IRepository<T> where T : class
    {
        
    }
    public class TimesheetRepository<T> : Repository<T, TimesheetMgt.DAL.TimeSheetDataContext>, ITimesheetRepository<T> where T : class
    {

    }


    namespace TimesheetMgt.DAL
    {
        partial class TimeSheetDataContext
        {
            partial void OnCreated()
            {
                this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
                //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            }
        }
    }
