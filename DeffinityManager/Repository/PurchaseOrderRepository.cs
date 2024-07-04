using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for PurchaseOrderRepository
/// </summary>
public interface IPurchaseOrderRepository<T> : IRepository<T> where T : class
{

    //T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();
}
public class PurchaseOrderRepository<T> : Repository<T, POMgt.DAL.PurchaseOrderMgtDataContext>, IPurchaseOrderRepository<T> where T : class
{



}

namespace POMgt.DAL
{
    partial class PurchaseOrderMgtDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
           // this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}
