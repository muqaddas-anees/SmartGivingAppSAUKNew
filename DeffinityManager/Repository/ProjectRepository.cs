using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data.Linq.Mapping;
using System.Web.Configuration;
//using Deffinity.Repositories;

/// <summary>
/// Summary description for PortfolioRepository
/// </summary>

public interface IProjectRepository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
public class ProjectRepository<T> : Repository<T, ProjectMgt.DAL.projectTaskDataContext>, IProjectRepository<T> where T : class 
    {
  
       
    }
namespace ProjectMgt.DAL
{
    partial class projectTaskDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
           //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}
