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

public interface IPortfolioRepository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
    public class PortfolioRepository<T> : Repository<T,PortfolioMgt.DAL.PortfolioDataContext>, IPortfolioRepository<T> where T :class 
    {
        

       
    }



    namespace PortfolioMgt.DAL
    {
        partial class PortfolioDataContext
        {
            partial void OnCreated()
            {
                //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
                this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            }
        }
    }
