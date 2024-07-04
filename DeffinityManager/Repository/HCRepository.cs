using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HCRepository
/// </summary>
/// 

public interface IHCRepository<T> : IRepository<T> where T : class
{

}
public class HCRepository<T> : Repository<T, HealthCheckMgt.DAL.HealthCheckDataContext>, IHCRepository<T> where T : class
{



}


namespace HealthCheckMgt.DAL
{
    partial class HealthCheckDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}