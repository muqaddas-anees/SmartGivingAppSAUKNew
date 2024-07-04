using System;
using System.Web;
/// <summary>
/// Summary description for SessionTimeOut
/// </summary>
public class SessionTimeOut
{
	public SessionTimeOut()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void SessionTimeoutCheck()
    {
        if (sessionKeys.UID == 0 || sessionKeys.SID == 0)
        { 
         HttpContext.Current.Response.Redirect("~/default.aspx");
        }

    }
}
