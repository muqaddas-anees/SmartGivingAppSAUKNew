using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

/// <summary>
/// Summary description for DataDictionaryBAL
/// </summary>
public class DataDictionaryBAL
{
	public DataDictionaryBAL()
	{
		//
		// TODO: Add constructor logic here
		//

        
	}

    public static string DataDictionary_GetName(string FieldLabel)
    {
        var s =  (DataDictionaryCollection().Where(p => p.FieldLabel == FieldLabel).Select(p => p.Current)).FirstOrDefault();
        string CurrentLable = string.Empty;
        if (s != null)
            CurrentLable = s;

        return string.IsNullOrEmpty(CurrentLable) ? FieldLabel : CurrentLable;
    }

    public static int DataDictionary_Count(string FieldLabel)
    {
        int Count = 0;
        Count = DataDictionaryCollection().Where(p => p.FieldLabel == FieldLabel).Count();
        
        return Count;
 
    }

    public static List<DataDictionary> DataDictionaryCollection()
    { 
        List<DataDictionary> dlist = BaseCache.Cache_Select("DataDictionaries") as List<DataDictionary>;

        if (dlist == null)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                dlist = (from p in pd.DataDictionaries
                            select p).ToList();
                BaseCache.Cache_Insert("DataDictionaries", dlist);
            }
        }
      
        


        return dlist;
    }
    public static void DataDictionaryCollection_ClearCache()
    {
        BaseCache.Cache_Remove("DataDictionaries");
    }
}