using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for PriceJournal
/// </summary>
public class PriceJournal
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
	public PriceJournal()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertCost()
    {
        try         
        {

            
        }
        catch (Exception ex)
        { }
    }
}
