using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// 
/// Project:    AJAX enabled Web Service
/// Company:    semenoff, http://www.semenoff.dk
/// Author:     Rostislav Semenov, http://www.semenoff.dk
/// Created:    August 2007
/// </summary>
[WebService(Namespace = "http://semenoff.dk/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
public class MySampleService : System.Web.Services.WebService
{

	public MySampleService()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	[WebMethod]
	public string GetServerResponse(string callerName)
	{
		if(callerName== string.Empty)
			throw new Exception("Web Service Exception: invalid argument");

		return string.Format("Service responded to {0} at {1}", callerName, DateTime.Now.ToString());
	}

}

