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
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Data.Common;
using Deffinity.Bindings;
public partial class MyQAIssuesaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.PageHead = "Resource";
            //Grid_Issues.DataBind();
           
        }
        
    }
    
    protected void btn_search_Click(object sender, ImageClickEventArgs e)
    {
        Grid_Issues.DataBind();
    }
   
    
    protected void Grid_Issues_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = Grid_Issues.EditIndex;
        GridViewRow Grow = Grid_Issues.Rows[index];

        TextBox txtNotes = (TextBox)Grow.FindControl("txtNotes");
        DropDownList ddlGdStatus = (DropDownList)Grow.FindControl("ddlGdStatus");

        objDS_SelectIssues.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();
        objDS_SelectIssues.UpdateParameters["Status"].DefaultValue = ddlGdStatus.SelectedValue;
        objDS_SelectIssues.UpdateParameters["Notes"].DefaultValue = txtNotes.Text.Trim();
        objDS_SelectIssues.Update();
    }
    protected void Grid_Issues_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }
}
