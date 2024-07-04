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

public interface IinventoryRepository<T> : IRepository<T> where T : class
{

}
public class InventoryRepository<T> : Repository<T, InventoryMgt.DAL.InventoryDataContext>, IinventoryRepository<T> where T : class 
    {
        

       
    }



namespace InventoryMgt.DAL
{
    partial class InventoryDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}