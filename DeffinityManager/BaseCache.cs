using System;
using System.Data;
using System.Configuration;
using System.Web.Caching;



/// <summary>
/// Summary description for BaseCache
/// </summary>
public class BaseCache
{
    public static void Cache_Insert(string Key,object DT)
    {
        System.Web.HttpContext.Current.Cache.Insert(Key, DT, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
    }
    //public static void Cache_Insert(string Key, DataTable DT)
    //{
    //    Cache_Insert(Key,DT as object);
    //}
    public static object Cache_Select(string Key)
    {
       return  System.Web.HttpContext.Current.Cache.Get(Key);
    }

    public static void Cache_Remove(string Key)
    {
        System.Web.HttpContext.Current.Cache.Remove(Key);
    }

		
}
