using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAssetRespository<T> : IRepository<T> where T : class
{

}
public class AssetRespository<T> : Repository<T, AssetsMgr.DAL.AssetsToSoftwareDataContext>, IAssetRespository<T> where T : class
{

}

namespace AssetsMgr.DAL
{
    partial class AssetsToSoftwareDataContext
    {
        partial void OnCreated()
        {
            //this.Connection.ConnectionString = DeffinityManager.DBCentralAccess.getConnectionString();
            this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        }
    }
}