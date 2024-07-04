using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

/// <summary>
/// Summary description for DefaultDataBAL
/// </summary>
public class DefaultDataBAL
{
	public DefaultDataBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Insert site default data
    public static void SiteDefaultData_Insert(DefaultData dd)
    {
        using (DCDataContext dc = new DCDataContext())
        {

            dc.DefaultDatas.InsertOnSubmit(dd);
            dc.SubmitChanges();
        }
    }
    #endregion

    #region Select site default data
    public static DefaultData SiteDefaultData_select()
    {
        DefaultData dd = new DefaultData();
        using (DCDataContext dc = new DCDataContext())
        {

            dd = (from d in dc.DefaultDatas select d).FirstOrDefault();
        }
        return dd;
    }
    #endregion

    #region Update site default data
    public static void SiteDefaultData_update(DefaultData dd)
    {
        using (DCDataContext dc = new DCDataContext())
        {
            DefaultData ddDetails = (from a in dc.DefaultDatas
                                       where a.ID == dd.ID
                                       select a).FirstOrDefault();

            ddDetails.SiteID = dd.SiteID;
            dc.SubmitChanges();
        }

    }
    #endregion

    #region Delete default data
    public static void SiteDefaultData_Delete(int id)
    {
        using (DCDataContext dc = new DCDataContext())
        {

            var dd = (from a in dc.DefaultDatas
                      where a.ID == id
                      select a).FirstOrDefault();
            dc.DefaultDatas.DeleteOnSubmit(dd);
            dc.SubmitChanges();

        }


    }
    #endregion
}