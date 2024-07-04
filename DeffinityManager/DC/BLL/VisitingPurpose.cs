using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;


/// <summary>
/// Summary description for VisitingPurpose
/// </summary>
/// 
namespace DC.BAL
{
public class VisitingPurpose
{
	public VisitingPurpose()
	{
    }
		//
		// TODO: Add constructor logic here
    //
    #region Insert record
    public static void Names_Insert(PurposeToVisit p)
        {
            using (DCDataContext dc = new DCDataContext())
            {

                dc.PurposeToVisits.InsertOnSubmit(p);
                dc.SubmitChanges();
            }
        }
    #endregion

    #region Update record if exist
    public static bool Name_ExistUpdate(string strname, int id)
         {
             PurposeToVisit p = new PurposeToVisit();
             using (DCDataContext dc = new DCDataContext())
             {

                 p = (from n in dc.PurposeToVisits
                            where n.Name == strname && n.ID != id
                            select n).FirstOrDefault();
             }
             if (p == null)
                 return false;
             else
                 return true;

         }
        #endregion


    #region Update record
    public static void Name_update(PurposeToVisit c)
         {
             using (DCDataContext dc = new DCDataContext())
             {
                 PurposeToVisit n = (from p in dc.PurposeToVisits
                                          where p.ID == c.ID
                                          select p).FirstOrDefault();
                 n.Name = c.Name;
                 dc.SubmitChanges();
             }

         }
        #endregion


    #region Select all Records
    public static List<PurposeToVisit> Names_selectAll()
         {
             List<PurposeToVisit> names = new List<PurposeToVisit>();
             using (DCDataContext dc = new DCDataContext())
             {
                 names = (from p in dc.PurposeToVisits orderby p.Name select p).ToList();
             }
             return names;
         }
    #endregion
    #region Bind Visiting Purpose
    public static List<PurposeToVisit> BindVisitingPurpose()
    {
        List<PurposeToVisit> VpList = new List<PurposeToVisit>();
        using (DCDataContext dd = new DCDataContext())
        {
            VpList = dd.PurposeToVisits.Select(r => r).ToList();
        }
        return VpList;
    }
    #endregion

    #region Check whether record exist
    public static bool Name_Exist(string name)
         {
             PurposeToVisit p = new PurposeToVisit();
             using (DCDataContext dc = new DCDataContext())
             {

                 p = (from n in dc.PurposeToVisits
                             where n.Name == name
                             select n).FirstOrDefault();
             }
             if (p == null)
                 return false;
             else
                 return true;

         }
    #endregion


    #region Select record by id
    public static PurposeToVisit Name_selectByID(int id)
         {
             PurposeToVisit p = new PurposeToVisit();
             using (DCDataContext dc = new DCDataContext())
             {

                 p = (from n in dc.PurposeToVisits
                            where n.ID == id
                            select n).FirstOrDefault();
             }

             return p;
         }
    #endregion

    #region Delete record
    public static void Name_Delete(int id)
         {
             using (DCDataContext dc = new DCDataContext())
             {

                 var n = (from s in dc.PurposeToVisits
                               where s.ID == id
                               select s).FirstOrDefault();
                 dc.PurposeToVisits.DeleteOnSubmit(n);
                 dc.SubmitChanges();

             }


         }
    #endregion

}
}