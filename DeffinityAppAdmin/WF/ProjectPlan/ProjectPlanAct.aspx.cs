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
using System.Web.Configuration;

public partial class ProjectPlanAct : System.Web.UI.Page
{
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    string error;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Project Proposal";
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                
                if (Request.QueryString["ProjectPlanID"] != null)
                {
                    if (Request.QueryString["ProjectPlanID"] == "")
                    {
                        HiddenField2.Value = "0";
                    }
                    else
                    {
                        HiddenField2.Value = Request.QueryString["ProjectPlanID"].ToString();
                    }
                    if (Convert.ToInt32(HiddenField2.Value) > 0)
                    {
                        fillDropdown(Convert.ToInt32(HiddenField2.Value));
                       // fillGrid(Convert.ToInt32(HiddenField2.Value));
                        SqlDataSource1.SelectParameters["ProjectPlanID"].DefaultValue = HiddenField2.Value;
                        GridView1.DataSourceID = "SqlDataSource1";
                        GridView1.DataBind();
                    }

                }
            }
            else
            {
                lblError.Text = "Please Enter Project Plan Details";

            }
        }
  }
    protected string getValue()
    {
        string Stemp1 = "";
        SqlConnection con = new SqlConnection(connectionString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID FROM Contractors WHERE ContractorName='" + Session["Uname"] + "'", con);
            Stemp1 = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        catch (Exception ex)
        {
            error = ex.ToString();
        }
        finally
        {
            con.Close();
        }
        return Stemp1;
    }
    private void fillDropdown(int Pid)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("Select ID,Description from MasterTemplate where Locked = 'N' and Active = 'Y'", myConnection);
            myCommand.CommandType = CommandType.Text;
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            myadapter.Fill(dt);
            ddlActive.DataSource = dt;
            ddlActive.DataTextField = "Description";
            ddlActive.DataValueField = "ID";
            ddlActive.DataBind();
            ddlActive.Items.Insert(0, "Select...");
            myCommand.Connection.Close();
            if (dt.Rows.Count == 0)
            {
                ddlActive.Visible = false;                
            }
            dt.Clear();
            myadapter.Dispose();
        }
        catch (Exception ex)
        {
            error = ex.ToString();
        }


    }



   

    protected void btnApply_Click(object sender, EventArgs e)
    {
        
        if (ddlActive.SelectedValue != "Select...")
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            try
            {
                SqlCommand myCommand = new SqlCommand("DN_InsertProjectPlanActivities", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;               
                myCommand.Parameters.Add(new SqlParameter("@ProjectPlanID", QueryStringValues.ProjectPlanID));
                myCommand.Parameters.Add(new SqlParameter("@MasterID", Convert.ToInt32(ddlActive.SelectedValue)));


                myCommand.Connection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                SqlDataSource1.SelectParameters["ProjectPlanID"].DefaultValue = QueryStringValues.ProjectPlanID.ToString();

                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
                ddlActive.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                myConnection.Close();
            }

        }
        else
        {
            lblError.Text = "Please select valid data";
        }

    }
    //protected void lbtnPlan_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/ProjectPipeline.aspx?Status=8");
    //}
   

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int index = GridView1.EditIndex;
        GridViewRow Grow = GridView1.Rows[index];
        
        TextBox txt = (TextBox)Grow.FindControl("txtAct");
        TextBox txtSdate = (TextBox)Grow.FindControl("txtSdate");
        TextBox txtEdate = (TextBox)Grow.FindControl("txtEdate");
        DropDownList ddlIndentLevel = (DropDownList)Grow.FindControl("ddlIndentLevel");
    
        e.NewValues["Activity"] = txt.Text;
        e.NewValues["StartDate"] = DateTime.Parse(txtSdate.Text.Trim());
        e.NewValues["EndDate"] = DateTime.Parse(txtEdate.Text.Trim());
        e.NewValues["IndentLevel"] = ddlIndentLevel.SelectedValue;

        
        SqlDataSource1.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();
        SqlDataSource1.UpdateParameters["Activity"].DefaultValue = e.NewValues["Activity"].ToString();
        SqlDataSource1.UpdateParameters["StartDate"].DefaultValue = e.NewValues["StartDate"].ToString();
        SqlDataSource1.UpdateParameters["EndDate"].DefaultValue = e.NewValues["EndDate"].ToString();
        SqlDataSource1.UpdateParameters["IndentLevel"].DefaultValue = e.NewValues["IndentLevel"].ToString();
        
        GridView1.DataSourceID = "SqlDataSource1";
        GridView1.DataBind();

    }
    protected bool getVisible(string i)
    {
        bool val = false;
        try
        {
            if (Convert.ToInt32(i) != 0)
            {
                val = true;
            }

        }
        catch (Exception ex) { }
        return val;
    }
    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string Item_F = string.Empty, Sdate_F = string.Empty, Edate_F = string.Empty, IndentLevel_F = string.Empty;


            if (((TextBox)GridView1.FooterRow.FindControl("txtAct_F")).Text != "")
            {
                Item_F = ((TextBox)GridView1.FooterRow.FindControl("txtAct_F")).Text;
            }
            if (((TextBox)GridView1.FooterRow.FindControl("txtSdate_F")).Text != "")
            {
                Sdate_F = ((TextBox)GridView1.FooterRow.FindControl("txtSdate_F")).Text;
            }
            if (((TextBox)GridView1.FooterRow.FindControl("txtEdate_F")).Text != "")
            {
                Edate_F = ((TextBox)GridView1.FooterRow.FindControl("txtEdate_F")).Text;
            }

            IndentLevel_F = ((DropDownList)GridView1.FooterRow.FindControl("ddlIndentLevel_F")).SelectedValue;
            
            SqlDataSource1.InsertParameters["Activity"].DefaultValue = Item_F;
            SqlDataSource1.InsertParameters["StartDate"].DefaultValue = DateTime.Parse(Sdate_F).ToShortDateString();
            SqlDataSource1.InsertParameters["EndDate"].DefaultValue = DateTime.Parse(Edate_F).ToShortDateString();
            SqlDataSource1.InsertParameters["IndentLevel"].DefaultValue = IndentLevel_F.ToString();
            SqlDataSource1.InsertParameters["ProjectPlanID"].DefaultValue = QueryStringValues.ProjectPlanID.ToString();
            SqlDataSource1.Insert();
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }
    }
    protected string getItemDes(string indent, string desc)
    {
        int temp = Convert.ToInt32(indent);
        string val = desc;
        if (temp == 0)
        {
            val = "<strong>" + desc + " </strong > ";
        }
        else if (temp == 1)
        {
            val = "<table border='0' cellspacing='0' cellpadding='0'><tr><td>&nbsp;</td><td align='left' width='170px'><strong>" + desc + "</strong></td></tr></table>";
        }
        else if (temp == 2)
        {
            val = "<table border='0' cellspacing='0' cellpadding='0'><tr><td>&nbsp;&nbsp;</td><td align='left' width='170px'>" + desc + "</td></tr></table>";
        }
        else if (temp == 3)
        {
            val = "<table border='0' cellspacing='0' cellpadding='0'><tr><td>&nbsp;&nbsp;&nbsp;</td><td align='left' width='170px'>" + desc + "</td></tr></table>";
        }
        else if (temp == 4)
        {
            val = "<table border='0' cellspacing='0' cellpadding='0'><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='left' width='170px'>" + desc + "</td></tr></table>";
        }
        return val;
    }
}
