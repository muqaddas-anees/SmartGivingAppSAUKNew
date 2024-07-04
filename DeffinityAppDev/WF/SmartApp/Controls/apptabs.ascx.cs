using HealthCheckMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Controls_apptabs : System.Web.UI.UserControl
{
    string[] array_path = { "/App/AppFormList.aspx", "/App/AppFormSearch.aspx", "/App/AppPermissionManager.aspx", "/App/AppManager.aspx" };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                int appid = 0;
                if (Request.QueryString["appid"] != null)
                {
                    appid = Convert.ToInt32(Request.QueryString["appid"]);
                }
                HealthCheckMgt.Entity.AppManager hf = new HealthCheckMgt.Entity.AppManager();
                int formid = 0;
                using (HealthCheckDataContext hcd = new HealthCheckDataContext())
                {
                    hf = hcd.AppManagers.Where(o => o.ID == appid).FirstOrDefault();
                    lblform.InnerText = hf.Name + " List" ;
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string getUrl(int i)
    {
        string retval = string.Empty;
        string[] array_path = { "AppFormList.aspx", "AppFormSearch.aspx", "AppPermissionManager.aspx", "GridColumns.aspx" };
        if (Request.QueryString["appid"] != null)
        {
            string appID = Request.QueryString["appid"].ToString();
            retval = array_path[i] + "?appid=" + appID;
        }
        return retval;
    }
    protected string GetCssClass(int i)
    {

        string rtValue = string.Empty;
        if (i < array_path.Length)
        {
            if ((Request.Url.ToString().ToLower()).Contains(array_path[i].ToLower()) == true)
            {
                rtValue = "current1";
            }
        }
        return rtValue;
    }
}