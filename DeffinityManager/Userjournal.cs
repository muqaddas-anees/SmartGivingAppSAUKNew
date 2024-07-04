using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Userjournal
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Userjournal : System.Web.Services.WebService {

    public Userjournal () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void userJournal_insert(string pagename,int userid)
    {
        try
        {
            PageJornal.PageJournal_Insert(pagename, userid);
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    
}

