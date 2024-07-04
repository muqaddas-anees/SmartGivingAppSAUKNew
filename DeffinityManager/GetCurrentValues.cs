using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



/// <summary>
/// Summary description for GetCurrentValues
/// </summary>
public class GetCurrentValues
{
	public GetCurrentValues()
	{
		//
		// TODO: Add constructor logic here
		//
	}

}
public class GetSessionValues
{
    int sid1;
    int cid;
    public int sid
    {
        get
        {
            if (HttpContext.Current.Session["SID"] != null)
            {
                sid1 = Convert.ToInt32(HttpContext.Current.Session["SID"].ToString());
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/default.aspx");
            }
            return sid1;
        }
        set
        {

        }

    }
    public int ResourceID
    {
        get
        {
            if (HttpContext.Current.Session["UID"] != null)
            {
                cid = Convert.ToInt32(HttpContext.Current.Session["UID"].ToString());
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/default.aspx");
            }
            return cid;
        }
        set
        {

        }
    }

}
public class Qstring
{
   
    private int Pref1;
    public int Pref
    {
        get
        {
            if (HttpContext.Current.Request.QueryString["Project"] != null)
            {
                try
                {
                    Pref1 = Convert.ToInt32(HttpContext.Current.Request.QueryString["Project"].ToString());
                }
                catch { HttpContext.Current.Response.Redirect("~/default.aspx"); }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/default.aspx");
            }
            return Pref1;
        
        }
        set { }
    }
    //public int Ac2pid
    //{
    //    get { }
    //    set { }
    //}
    //public int ResourceID
    //{
    //    get { }
    //    set { }
    //}

}
public class Svalues
{
    private int cid;
    //public int SID
    //{
    //    get { }
    //    set { }
    //}
    //public string Uname
    //{
    //    get { }
    //    set { }
    //}
    public int ResourceID
    {
       
        get
        {
            if (HttpContext.Current.Session["UID"] != null)
            {
                cid = Convert.ToInt32(HttpContext.Current.Session["UID"].ToString());
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/default.aspx");
            }
            return cid;
        
        }
        set { }
    }
    

}