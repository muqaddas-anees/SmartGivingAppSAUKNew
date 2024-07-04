using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
//using Deffinity.Repositories;

/// <summary>
/// Summary description for PortfolioRepository
/// </summary>

public interface IincidentRepository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
public class IncidentRepository<T> : Repository<T, IncidentMgt.DAL.IncidentDataContext>, IincidentRepository<T> where T : class 
    {
        

       
    }



namespace IncidentMgt.DAL
{
    partial class IncidentDataContext
    {
        partial void OnCreated()
        {
           // this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}
