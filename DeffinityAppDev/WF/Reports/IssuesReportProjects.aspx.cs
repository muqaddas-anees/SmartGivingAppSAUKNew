using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Reports_IssuesReportProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand cmd = new SqlCommand("AMPS_Projects", MyCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyCon;
            SqlParameter UID = new SqlParameter("@UserID", SqlDbType.Int);
            UID.Value = sessionKeys.UID;
            cmd.Parameters.Add(UID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Binds data to Contractor's dropdownlist
            ddlProjects.DataSource = dt;
            ddlProjects.DataTextField = "ProjectTitle";
            ddlProjects.DataValueField = "ProjectReference";
            ddlProjects.DataBind();
        }
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectIssuesReport.aspx?Project=" + Convert.ToInt32(ddlProjects.SelectedValue));
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
