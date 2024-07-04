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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
public partial class QACIPS : System.Web.UI.Page
{//int AC2PID;
    int ProjectReference;
    int ContractorID;
    string Stemp = "";
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Continuous Improvement Program ";
        TabVisiblity();
        if (!Page.IsPostBack)
        {
            fillgrid();
        }
    }
    private void TabVisiblity()
    { 
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["type"] == "pm")
            { OpsViewTabs1.Visible = true;
              QATab1.Visible = false;
            }
            else if (Request.QueryString["type"] == "qa")
            { 
                QATab1.Visible = true;
                OpsViewTabs1.Visible = false;
            }

        }
      
    }
    DataSet ds = new DataSet();
    public void fillgrid()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("DN_CIPDisplay", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ProjectReference", SqlDbType.Int, 32).Value = QueryStringValues.Project;
        con.Open();
        SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
        myadapter.Fill(dt);
        cmd.Connection.Close();
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    private void changeTabVisibility()
    {
    //    if (Request.QueryString["Type"] != null)
    //    {
    //        //change tabs visibility 
    //        if (Request.QueryString["Type"].ToString() == "PM")
    //        {
    //            OpsViewTabs1.Visible = false;
    //        }
    //        if (Request.QueryString["Type"].ToString() == "QA")
    //        {
    //            OpsViewTabs1.Visible = false;
    //            QATab1.Visible = true;
    //        }
    //    }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        string ResultExpected1 = "";
        string Improvement1 = "";
        int ddresource1 = 0;
        if (e.CommandName == "EmptyInsert")
        {


            if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtImprovement2")).Text != "")
            {
                Improvement1 = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtImprovement2")).Text;
            }

            if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtResultExpected2")).Text != "")
            {
                ResultExpected1 = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtResultExpected2")).Text;
            }

            if (Convert.ToInt32(((DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddresource2")).SelectedValue) != 0)
            {
                ddresource1 = Convert.ToInt32(((DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddresource2")).SelectedValue);
            }

            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("DN_InsertProjectCIPQA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = QueryStringValues.Project;
                cmd.Parameters.Add("@Improvement", SqlDbType.VarChar, 400).Value = Improvement1;
                cmd.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ddresource1;
                cmd.Parameters.Add("@ResultExpected", SqlDbType.VarChar, 100).Value = ResultExpected1;
                cmd.Parameters.Add("@RaisedBy", SqlDbType.Int).Value = sessionKeys.UID;
                
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }

            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);

            }
            finally
            {
                con.Close();

            }
            fillgrid();
        }

        if (e.CommandName == "Insert")
        {

            if (((TextBox)GridView1.FooterRow.FindControl("txtResultExpected1")).Text != "")
            {
               ResultExpected1 = ((TextBox)GridView1.FooterRow.FindControl("txtResultExpected1")).Text;
            }
            if (((TextBox)GridView1.FooterRow.FindControl("txtImprovement1")).Text != "")
            {
                Improvement1 = ((TextBox)GridView1.FooterRow.FindControl("txtImprovement1")).Text;
            }
           

            if (Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddresource1")).SelectedValue) != 0)
            {
                ddresource1 = Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddresource1")).SelectedValue);
            }

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("DN_InsertProjectCIPQA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = QueryStringValues.Project;
                cmd.Parameters.Add("@Improvement", SqlDbType.VarChar, 400).Value = Improvement1;
                cmd.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ddresource1;
                cmd.Parameters.Add("@ResultExpected", SqlDbType.VarChar, 100).Value = ResultExpected1;
                cmd.Parameters.Add("@RaisedBy", SqlDbType.Int).Value = sessionKeys.UID;
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            catch (Exception ex)
            {

                LogExceptions.LogException(ex.Message);
            }
            finally
            {
                con.Close();

            }
            fillgrid();

        }
        if (e.CommandName == "Update")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            string Improvement = ((TextBox)Row.FindControl("txtImprovement")).Text;
            int Resource1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddresource")).SelectedItem.Value);
            string ResultExpected = ((TextBox)Row.FindControl("txtResultExpected")).Text;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("updateProjectCIPUpdation1");
                db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@Improvement", DbType.String, Improvement);
                db.AddInParameter(cmd, "@ContractorID", DbType.Int32, Resource1);
                db.AddInParameter(cmd, "@ResultExpected", DbType.String, ResultExpected);
                db.ExecuteNonQuery(cmd);
               
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
            finally
            {

            }
            fillgrid();

        }

        if (e.CommandName == "Delete")
        {

            string id = e.CommandArgument.ToString();

            string delete = "delete from AC2PID_Improvement where ID='" + id + "'";

            SqlCommand cmd = new SqlCommand(delete, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Stemp = ex.ToString();
            }
            finally
            {
                con.Close();

            }
            GridView1.EditIndex = -1;
            fillgrid();
        }

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}
