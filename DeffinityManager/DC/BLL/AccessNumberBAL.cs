using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

/// <summary>
/// Summary description for AccessNumberBAL
/// </summary>
public class AccessNumberBAL
{
    #region Insert access number
    public static void AccessNumber_Insert(AccessNumbersBasedonDay ac)
    {
        using (DCDataContext dc = new DCDataContext())
        {

            dc.AccessNumbersBasedonDays.InsertOnSubmit(ac);
            dc.SubmitChanges();
        }
    }
    #endregion
    #region Select access number
    public static AccessNumber AccessNumber_select()
    {
        AccessNumber acno = new AccessNumber();
        using (DCDataContext dc = new DCDataContext())
        {

            acno = (from ac in dc.AccessNumbers select ac).FirstOrDefault();
        }
        return acno;
    }
    #endregion

    #region Select access number based on days
    public static List<AccessNumbersBasedonDay> AccessNumber_selectByID(int id)
    {
        List<AccessNumbersBasedonDay> ano = new List<AccessNumbersBasedonDay>();
        using (DCDataContext dc = new DCDataContext())
        {

            ano = (from a in dc.AccessNumbersBasedonDays
                    where a.CallID == id
                    select a).ToList();
        }

        return ano;
    }
    #endregion

    #region Select maximum access number based on days
    public static AccessNumbersBasedonDay SelectMaximumAccessNumber()
    {
        AccessNumbersBasedonDay ano = new AccessNumbersBasedonDay();
        using (DCDataContext dc = new DCDataContext())
        {
          
            ano = (from a in dc.AccessNumbersBasedonDays 
                   orderby a.ID descending
                   select a ).FirstOrDefault();
        }
        return ano;
    }
    //public static int AccessNumber_MaxValue()
    //{
    //    int retval = 0;
    //    using (DCDataContext dc = new DCDataContext())
    //    {
            
    //        var p = (from a in dc.AccessNumbersBasedonDays
    //                 orderby a.AccessNumber descending
    //                 select a).FirstOrDefault();
    //        if (p != null)
    //        {
    //            retval = Convert.ToInt32( (from a in dc.AccessNumbersBasedonDays
    //                      orderby a.AccessNumber descending
    //                      select a.AccessNumber).Max());
    //        }
    //    }

    //    return retval;
    //}

    #endregion

    #region Delete Access number
    public static List<AccessNumbersBasedonDay> AccessNumber_Delete(int id)
    {
        List<AccessNumbersBasedonDay> acno = new List<AccessNumbersBasedonDay>();
        using (DCDataContext dc = new DCDataContext())
        {

            acno = (from vi in dc.AccessNumbersBasedonDays
                     where vi.CallID == id
                     select vi).ToList();
            dc.AccessNumbersBasedonDays.DeleteAllOnSubmit(acno);
            dc.SubmitChanges();

        }
        return acno;
       
    }
    #endregion

    #region Select access number by callid
    public static List<AccessNumbersBasedonDay> SelectByCallid(int cid)
    {
        List<AccessNumbersBasedonDay> ano = new List<AccessNumbersBasedonDay>();
        using (DCDataContext dc = new DCDataContext())
        {

            ano = (from a in dc.AccessNumbersBasedonDays
                   where a.CallID == cid orderby a.AccessNumber ascending
                   select a).ToList();
        }
        return ano;
    }
    #endregion

    #region Select TicketNo Depends on AccessNo

    public static int TicketNoByAccessNo(string accessNo)
    {
        int ticketNo = 0;
        using (DCDataContext dd = new DCDataContext())
        {
            ticketNo = dd.AccessNumbersBasedonDays.Where(a => a.AccessNumber == accessNo).Select(a => Convert.ToInt32(a.CallID)).FirstOrDefault();
        }
        return ticketNo;
    }

    #endregion


    #region Select maximum day by callid
    public static AccessNumbersBasedonDay SelectMaximumDayByCallid(int cid)
    {
        AccessNumbersBasedonDay ano = new AccessNumbersBasedonDay();
        using (DCDataContext dc = new DCDataContext())
        {

            ano = (from a in dc.AccessNumbersBasedonDays where a.CallID == cid

                   orderby a.Day descending
                   select a).FirstOrDefault();
        }
        return ano;
    }
    #endregion

    #region Select minimum access number by callid
    public static AccessNumbersBasedonDay SelectMinimumAccessNumberByCallID(int cid)
    {
        AccessNumbersBasedonDay ano = new AccessNumbersBasedonDay();
        using (DCDataContext dc = new DCDataContext())
        {

            ano = (from a in dc.AccessNumbersBasedonDays where a.CallID == cid

                   orderby a.AccessNumber ascending
                   select a).FirstOrDefault();
        }
        return ano;
    }
    #endregion

   


}