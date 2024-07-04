using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using Deffinity.Repositories;

/// <summary>
/// Summary description for PortfolioRepository
/// </summary>

public interface IUserRepository<T> : IRepository<T> where T : class
{

}
public class UserRepository<T> : Repository<T, UserMgt.DAL.UserDataContext>, IUserRepository<T> where T : class 
    {
        
    
       
    }
namespace UserMgt.DAL
{
    partial class UserDataContext
    {
        partial void OnCreated()
        {
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
           // this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
        }
    }
}