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

public interface IDCRespository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
public class DCRepository<T> : Repository<T, DC.DAL.DCDataContext>, IDCRespository<T> where T : class 
    {
        

       
    }


namespace DC.DAL
{
    partial class DCDataContext
    {
        partial void OnCreated()
        {
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
        }
    }
}
