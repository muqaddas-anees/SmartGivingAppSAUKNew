using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;


/// <summary>
/// Summary description for CustomerSCAutoComplete
/// </summary>
[System.Web.Script.Services.ScriptService()] 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CustomerSCAutoComplete : System.Web.Services.WebService
{

    public CustomerSCAutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [System.Web.Services.WebMethod]
   
    
    public string[] GetServices(string prefixText, int count, string contextKey)
    {
        int Type=0, Category=0, SubCategory=0;
       
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByName",
            new SqlParameter("@Type", Type), new SqlParameter("@PortfolioID", int.Parse(contextKey)), new SqlParameter("@Category", Category),
            new SqlParameter("@SubCategory", SubCategory), new SqlParameter("@PreFix", prefixText)).Tables[0];
        int cnt = dt.Rows.Count;
        if (cnt > 10)
        {
            cnt = 10;
        }      
        
        string[] items = new string[cnt];
        int i = 0;        
            foreach (DataRow dr in dt.Rows)
            {
                if (i < 10)
                {
                    items[i]=dr["Description"].ToString();
                    i++;
                }
               
            }
        
        return items;
    }

   

    [System.Web.Services.WebMethod]

    public string[] GetVenorItems(string prefixText, int count, string contextKey)
    {


        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue_ByVendors",
             new SqlParameter("@PreFix", prefixText)).Tables[0];
        int cnt = dt.Rows.Count;
        if (cnt > 10)
        {
            cnt = 10;
        }

        string[] items = new string[cnt];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (i < 10)
            {
                items[i] = dr["Description"].ToString();
                i++;
            }

        }

        return items;
    }
 }

