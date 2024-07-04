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
/// Summary description for Constants
/// </summary>
public class Constants
{
    public static string DBString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBstring"].ToString();
   
    public static string ddlValField = "ID";
    public static string ddlContractorTextField = "ContractorName";
    public static string ddlUserTextField = "UserName";
    public static string ddlUserTypeTextField = "Type";
    public static string ddlCompanyTextField = "Company";  
    public static string ddlDefaultValue = "Please select...";
    public static string ddlDefaultValueAll = "ALL";
    public static string ddlItemStatusTextField = "Status";

    ///<summary>
    ///if all_select is true then will returns Select else All
    /// <param name="all_select"></param>
    /// returns List itme
    /// 
    ///</summary>

    public static ListItem ddlDefaultBind(bool all_select)
    {
        ListItem li;
        if (!all_select)
        { li = new ListItem(ddlDefaultValueAll, "0"); }
        else
        { li = new ListItem(ddlDefaultValue, "0"); }
        
        return li;
    }    
	public Constants()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
