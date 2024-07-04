using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Location.DAL;
using Location.Entity;

/// <summary>
/// Summary description for SiteBAL
/// </summary>
public class SiteBAL
{
    #region Select site by ID
    public static Site SelectbyId(int id)
    {

        Site type = new Site();
        using (LocationDataContext dd = new LocationDataContext())
        {
            type = dd.Sites.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
        }
        return type;
    }
    #endregion
}