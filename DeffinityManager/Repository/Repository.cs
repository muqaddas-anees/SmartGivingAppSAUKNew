using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Data.Linq.Mapping;

using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
//namespace Deffinity.Repositories
//{
public interface IRepository<T>:IDisposable where T : class
{
   
    T GetById(int id);
    //IQueryable<T> GetAll();
    //void InsertOnSubmit(T entity);
    //void DeleteOnSubmit(T entity);
    //[Obsolete("Units of Work should be managed externally to the Repository.")]
    //void SubmitChanges();

    IQueryable<T> GetAll();
   // IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddAll(List<T> entityCollection);
    void Delete(T entity);
    void DeleteAll(List<T> entityCollection);
    void Edit(T entity);
    void Save();
}
//public interface IRepository
//{
//    object GetById(int id);
//    IQueryable GetAll();
//    void InsertOnSubmit(object entity);
//    void DeleteOnSubmit(object entity);
//    [Obsolete("Units of Work should be managed externally to the Repository.")]
//    void SubmitChanges();
//}
//public interface IDataContextProvider : IDisposable
//{
//    DataContext DataContext { get; }
//}

public abstract class Repository<T, C> : IRepository<T>
    where T : class
    where C : DataContext, new()
{
    
    private C _entities = new C();
    public C dataContext
    {

        get { return _entities; }
        set { _entities = value;
           
      //  _entities.Connection.ConnectionString = "";
        }
    }
     
    //public DataContext dataContext
    //{
    //    get;
    //    set;
    //} 
    //private readonly DataContext dataContext;
    //public Repository(IDataContextProvider dataContextProvider)
    //{
    //    dataContext = dataContextProvider.DataContext;
    //   // dataContext = new DataContext();
    //    //dataContext = new PortfolioMgt.DAL.PortfolioDataContext();
    //}
   
    public virtual T GetById(int id)
    {
        var itemParameter = Expression.Parameter(typeof(T), "item");
        var whereExpression = Expression.Lambda<Func<T, bool>>
            (
            Expression.Equal(
                Expression.Property(
                    itemParameter,
                    RepositoryUtill.GetPrimaryKey<T>().Name
                    ),
                Expression.Constant(id)
                ),
            new[] { itemParameter }
            );
        return GetAll().Where(whereExpression).Single();
    }
    public virtual IQueryable<T> GetAll()
    {
        return dataContext.GetTable<T>();
    }
    public virtual void Add(T entity)
    {
        GetTable().InsertOnSubmit(entity);
        Save();
    }
    public virtual void AddAll(List<T> entityCollection)
    {
        GetTable().InsertAllOnSubmit(entityCollection);
        Save();
    }
    public virtual void Edit(T entity)
    {
        //GetTable().GetOriginalEntityState(entity)
        Save();
    }
    public virtual void Delete(T entity)
    {
        GetTable().DeleteOnSubmit(entity);
        Save();
    }
    public virtual void DeleteAll(List<T> entityCollection)
    {
        GetTable().DeleteAllOnSubmit(entityCollection);
        Save();
    }
    public virtual void Save()
    {
        dataContext.SubmitChanges();
    }
    public virtual ITable GetTable()
    {
        
        return dataContext.GetTable<T>();
    }

     private bool disposed = false;

     protected virtual void Dispose(bool disposing)
     {

         if (!this.disposed)
             if (disposing)
                 dataContext.Dispose();

         this.disposed = true;
     }

     public void Dispose()
     {

         Dispose(true);
         GC.SuppressFinalize(this);
     }

    
}

public class RepositoryUtill
{
    #region Get Primary key
    public static PropertyInfo GetPrimaryKey<T>()
    {
        PropertyInfo[] infos = typeof(T).GetProperties();
        PropertyInfo PKProperty = null;
        foreach (PropertyInfo info in infos)
        {
            var column = info.GetCustomAttributes(false)
             .Where(x => x.GetType() == typeof(ColumnAttribute))
             .FirstOrDefault(x =>
              ((ColumnAttribute)x).IsPrimaryKey &&
              ((ColumnAttribute)x).DbType.Contains("NOT NULL"));
            if (column != null)
            {
                PKProperty = info;
                break;
            }
            if (PKProperty == null)
            {
                throw new NotSupportedException(
                  typeof(T).ToString() + " has no Primary Key");
            }
        }
        return PKProperty;

    }
    //public static PropertyInfo GetPrimaryKey(this Type entityType)
    //{
    //    foreach (PropertyInfo property in entityType.GetProperties())
    //    {
    //        ColumnAttribute[] attributes = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), true);
    //        if (attributes.Length == 1)
    //        {
    //            ColumnAttribute columnAttribute = attributes[0];
    //            if (columnAttribute.IsPrimaryKey)
    //            {
    //                if (property.PropertyType != typeof(int))
    //                {
    //                    throw new ApplicationException(string.Format("Primary key, '{0}', of type '{1}' is not int",
    //                        property.Name, entityType));
    //                }
    //                return property;
    //            }
    //        }
    //    }
    //    throw new ApplicationException(string.Format("No primary key defined for type {0}", entityType.Name));
    //}
}
    #endregion
//}

///// <summary>
///// Summary description for Repository
///// </summary>
//public class Repository
//{
//    public Repository()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
