using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using Deffinity.Repositories;

/// <summary>
/// Summary description for PortfolioRepository
/// </summary>

public interface ILocationRepository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
public class LocationRepository<T> : Repository<T, Location.DAL.LocationDataContext>, ILocationRepository<T> where T : class
{



}



namespace Location.DAL
{
    partial class LocationDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}
