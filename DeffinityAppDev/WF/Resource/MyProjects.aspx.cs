using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Resource_MyProjects : System.Web.UI.Page
{
    public DataSet ds;
    string error = "";
    int status;

    //status
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Resource";
        int status = Convert.ToInt32(Request.QueryString["status"].ToString());
        if (!IsPostBack)
        {
            //lblHead.Text = "Assigned Live Projects...";
            if (status == 0)
            {

            }
            else
            {
                if (sessionKeys.UID > 0)
                {
                    getHeader(status);
                    Bindings(sessionKeys.UID, status);
                }
            }
        }

    }
  
    #region functions
   
    private void getHeader(int status)
    {
        if (status == 2)
        { lblHead.InnerText = "Assigned Live Projects"; }
        else if (status == 1)
        { lblHead.InnerText = "Assigned Projects with Pending Status"; }
        else if (status == 3)
        { lblHead.InnerText= "Completed Projects"; }
        else if (status == 7)
        { lblHead.InnerText = "Assigned Projects with On Hold Status"; }

    }
    private void Bindings(int uid, int st)
    {
        SqlDataSource2.SelectParameters["ContractorID"].DefaultValue = uid.ToString();
        SqlDataSource2.SelectParameters["StatusID"].DefaultValue = st.ToString();
        GridView1.DataSourceID = "SqlDataSource2";
        GridView1.DataBind();    
    }
    #endregion
    protected void btn_timesheet_Click(object sender, ImageClickEventArgs e)
    {
       
        status = 4;
        if (sessionKeys.UID >0)
        {
            Bindings(sessionKeys.UID, status);
        }
        Response.Redirect("TimeSheetResources.aspx");
       
    }
   
}
