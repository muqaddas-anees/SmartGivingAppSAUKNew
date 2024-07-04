using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
[System.Web.Script.Services.ScriptService()] 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService {

    public AutoComplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [System.Web.Services.WebMethod]
    public string[] GetPageTitles(string prefixText, int count, string contextKey)
    {
        ArrayList ar = new ArrayList();
        //string[] items = new string[10];
        int i = 0;
        SqlDataReader dr;
        dr = PageJornal.PageTitle_Select(prefixText);
        
        while (dr.Read())
        {
            if (i < 10)
            {
                ar.Add(string.IsNullOrEmpty(dr["pagename"].ToString()) ? string.Empty : dr["pagename"].ToString());
                //items[i] = string.IsNullOrEmpty(dr["pagename"].ToString()) ? string.Empty : dr["pagename"].ToString();
                i++;
            }
        }
        dr.Close();
        dr.Dispose();


        return (string[])ar.ToArray(typeof(string)); 
    }
}

