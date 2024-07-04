using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for PageJornal
/// </summary>
public class PageJornal
{
    public static void PageJournal_Insert(string PageName, int UserID)
    {
        if (!Page_SkipChecking(PageName.ToLower()))
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "JOURNAL.PagesAccessed_insert", new SqlParameter("@PageName", PageName.ToLower()), new SqlParameter("@UserID", UserID));
        }
    }
     public static void PageJournal_Delete(string IDs)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "JOURNAL.PagesAccessed_delete", new SqlParameter("@IDs", IDs));
    }
     public static DataTable PageJournal_Select(string FromDate,string Todate,int UserID,string PageTitle)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "JOURNAL.PagesAccessed_select",
            new SqlParameter("FromDate",FromDate),new SqlParameter("Todate",Todate),new SqlParameter("UserID",UserID),new SqlParameter("PageTitle",PageTitle)).Tables[0];
    }
     public static SqlDataReader PageTitle_Select(string prefixtext)
     {
         return SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select PageName from Journal.PageTitles where PageName like '{0}%'",prefixtext));
     }
     public static bool Page_SkipChecking(string PageName)
     {
         bool chk = false;
         string[] page_url = { "message.aspx", "pagenotfound.aspx", "admin_usersjournal.aspx", "_xml.aspx", "deffinity_execute.aspx", "default_1.aspx", "projectmppfileupload.aspx", "sessionkeepalive.aspx", "vt.resourcesummarydisplay.aspx" };
          foreach(string ar_url in page_url)
         {
             if (PageName.ToLower().Contains(ar_url.ToLower()))
             {
                 chk = true;
             }
         }
          return chk;
     }
     public static bool Get_JorunalStatus()
     {
         bool journalstaus = false;
         //JournalEnable
         string key = CacheNames.DefaultNames.JournalEnable.ToString();
         if (BaseCache.Cache_Select(key) == null)
         {
             BaseCache.Cache_Insert(key, SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select Enablejournal from ProjectDefaults").ToString());
         }
         journalstaus = bool.Parse( BaseCache.Cache_Select(key).ToString());
         return journalstaus;
     }

    
}
