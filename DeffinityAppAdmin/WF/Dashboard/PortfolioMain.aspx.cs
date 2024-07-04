using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using Infragistics.UltraChart.Shared.Styles;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web;

public partial class PortfolioMain : System.Web.UI.Page
{

    #region Fields

    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    private string User = string.Empty;
    int UserID=0;

    #endregion

    #region Control Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check dash board user
        

        User = sessionKeys.UName;
        UserID = sessionKeys.UID;        
        if (!this.IsPostBack)
        {
           // Master.PageHead = "Dashboard";
            fillViewAllSelectView();
        }
    }
    [System.Web.Services.WebMethod]
    public static object GraphInDashBoard(string Pid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            List<DashBoardDataCls> DashBoardDataClsList = new List<DashBoardDataCls>();
            DashBoardDataCls D_Cls = null;
            DataTable dt = new DataTable();
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DEFFINITY_PROJECTS_SUMMARY", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@PORTFOLIO", Pid);
            myCommand.Parameters.AddWithValue("@UID", sessionKeys.UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                D_Cls = new DashBoardDataCls();
                D_Cls.state = dr["ProjectReference"].ToString();//dr["ProjectReferencePrefix"].ToString() + "-" + dr["ProjectReferenceNew"].ToString();
                D_Cls.Budgetcost =Convert.ToDouble(dr["Budget Cost"].ToString());
                D_Cls.ActualcosttoDate = Convert.ToDouble(dr["Actual Cost to Date"].ToString());
                D_Cls.Variances = Convert.ToDouble(dr["Variances"].ToString());
                D_Cls.Invoiced = Convert.ToDouble(dr["Invoiced"].ToString());
                DashBoardDataClsList.Add(D_Cls);
            }
            return jsonSerializer.Serialize(DashBoardDataClsList).ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }

    protected void ddlselectview_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand myCommand = new SqlCommand("Project_PermissionCustomer", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        //SqlParameter UID=new SqlParameter("@UserID",SqlDbType.Int);
        //UID.Value=sessionKeys.UID;
        //myCommand.Parameters.Add(UID);
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        myadapter.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        myadapter.Fill(dt);
        ddlprojectPortfolio.Items.Clear();
        //ListItem listItem = new ListItem("Please Select..", "0");
        //ddlprojectPortfolio.Items.Add(listItem);
        ddlprojectPortfolio.DataSource = dt;
        ddlprojectPortfolio.DataTextField = "PortFolio";
        ddlprojectPortfolio.DataValueField = "ID";
        ddlprojectPortfolio.DataBind();
        myCommand.Connection.Close();
        dt.Clear();
        myadapter.Dispose();
        //SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        //SqlDataAdapter myadapter = new SqlDataAdapter("DN_Portfolio", myConnection);
        //myadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //int val = 0;
        //if (Convert.ToInt32(ddlselectview.SelectedValue) == 0)
        //{
        //    val = 0;
        //}
        //else if (Convert.ToInt32(ddlselectview.SelectedValue) == 1)
        //{
        //    val = UserID;
        //}
        //myadapter.SelectCommand.Parameters.Add("@Owner", SqlDbType.Int).Value = val;
        //DataSet ds = new DataSet();
        //myadapter.Fill(ds);
        //ddlprojectPortfolio.Items.Clear();
        //ListItem listItem = new ListItem("Please Select..", "0");
        //ddlprojectPortfolio.Items.Add(listItem);
        //ddlprojectPortfolio.DataSource = ds;
        //ddlprojectPortfolio.DataTextField = "PortFolio";
        //ddlprojectPortfolio.DataValueField = "ID";
        //ddlprojectPortfolio.DataBind();

        //ds.Clear();
        //myadapter.Dispose();
        displayDataOnControls();
        displaydetails();
    }


    private void getProjectDetailsByPortfolio()
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand cmd = new SqlCommand("dn_CheckProjectStatus", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter Portfolio = new SqlParameter("@Portfolio", SqlDbType.Int, 32);
        SqlParameter UID = new SqlParameter("@UID", SqlDbType.Int, 32);
        Portfolio.Value = ddlprojectPortfolio.SelectedValue;
        UID.Value = sessionKeys.UID;
        cmd.Parameters.Add(Portfolio);
        cmd.Parameters.Add(UID);
       
        //Portfolio.Value = ddlprojectPortfolio.SelectedValue;
        //
        //cmd.Parameters.Add(Portfolio);
        
        SqlDataReader reader;
        cn.Open();
        using (cn)
        {
            using (reader = cmd.ExecuteReader())
            {
                reader.Read();
                lblportfolioowner.Text = Session["Uname"].ToString();
                lblnpojectP.Text = reader["TotalProjects"].ToString();
                lblliveProject.Text = reader["Live"].ToString();
                lblpendingProject.Text = reader["Pending"].ToString();
                lblpBudget.Text = string.Format("{0:c}", reader[4]);
                lbltotpvalue.Text = string.Format("{0:c}", reader[5]);
                reader.Close();
            }
        }

        displayDataOnControls();
    }

    protected void ddlprojectPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        getProjectDetailsByPortfolio();
        displaydetails();
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        recordLogException(Server.GetLastError());
        Response.Redirect("Message.aspx?aspxerrorpath=/PortfolioMain.aspx",false);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (ddlprojectPortfolio.SelectedIndex <= 0)
            pnlSummary.Visible = false;
        else
            pnlSummary.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    #endregion 

    #region Helper Methods

    private void recordLogException(Exception ex)
    {
        StringBuilder sbLogException = new StringBuilder();
        sbLogException.Append(string.Format("\nMessage:{0}", ex.Message));
        sbLogException.Append(string.Format("\nInner Exception:{0}", ex.InnerException));
        sbLogException.Append(string.Format("\nSource:{0}", ex.Source));
        sbLogException.Append(string.Format("\nData:{0}", ex.Data));
        sbLogException.Append(string.Format("\nStack Trace:{0}", ex.StackTrace));
        sbLogException.Append(string.Format("\nTarget Site:{0}", ex.TargetSite));
        LogExceptions.LogException(sbLogException.ToString());
    }

    public void fillViewAllSelectView()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("Project_PermissionCustomer", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            //SqlParameter UID=new SqlParameter("@UserID",SqlDbType.Int);
            //UID.Value=sessionKeys.UID;
            //myCommand.Parameters.Add(UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            myadapter.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
            myadapter.Fill(dt);
            ddlprojectPortfolio.Items.Clear();
           // ListItem listItem = new ListItem("Please Select..", "0");
            //ddlprojectPortfolio.Items.Add(listItem);
            ddlprojectPortfolio.DataSource = dt;
            ddlprojectPortfolio.DataTextField = "PortFolio";
            ddlprojectPortfolio.DataValueField = "ID";
            ddlprojectPortfolio.DataBind();
            myCommand.Connection.Close();
            dt.Clear();
            myadapter.Dispose();
            displaydetails();
            displayDataOnControls();
        }
        catch
        {
            
        }
    }
    
    public void displaydetails()
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand cmd = new SqlCommand("DN_CheckProjectStatus", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter Portfolio = new SqlParameter("@Portfolio", SqlDbType.Int, 32);
        SqlParameter UID = new SqlParameter("@UID", SqlDbType.Int, 32);
        Portfolio.Value = ddlprojectPortfolio.SelectedValue;
        UID.Value = sessionKeys.UID;
        cmd.Parameters.Add(Portfolio);
        cmd.Parameters.Add(UID);
        SqlDataReader reader;
        try
        {
            using (cn)
            {
                cn.Open();
                using (reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    lblportfolioowner.Text = Session["Uname"].ToString();
                    lblnpojectP.Text = reader["TotalProjects"].ToString();
                    lblliveProject.Text = reader["Live"].ToString();
                    lblpendingProject.Text = reader["Pending"].ToString();
                    lblpBudget.Text = string.Format("{0:c}", reader[4]);
                    lbltotpvalue.Text = string.Format("{0:c}", reader[5]);
                    reader.Close();
                }
            }
        }
        catch 
        {
            
        }
        finally
        {
            cn.Close();
        }
    }
    public void displayDataOnControls()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("Deffinity_DisplayPortfolio", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@PortfolioID", ddlprojectPortfolio.SelectedValue);
            myCommand.Parameters.AddWithValue("@UID", sessionKeys.UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);

            girdLiveProjects.DataSource = ds.Tables[0];
            girdLiveProjects.DataBind();

            gridPendingProjects.DataSource = ds.Tables[1];
            gridPendingProjects.DataBind();

            gridCompletedProjects.DataSource = ds.Tables[2];
            gridCompletedProjects.DataBind();

            ListLiveIssues.DataSource = ds.Tables[3];
            ListLiveIssues.DataBind();

            ListResources.DataSource = ds.Tables[4];
            ListResources.DataBind();

            ListMitigation.DataSource = ds.Tables[5];
            ListMitigation.DataBind();

            //DataView dv;
            //dv = (DataView)SqlDataSource6.Select(DataSourceSelectArguments.Empty);

            //if (dv.Count == 0)
            //{
            //    UltraChart5.Visible = false;
            //}
            //else
            //{
            //    UltraChart5.Visible = true;
            //    UltraChart5.DataSource = dv;
            //    UltraChart5.DataBind();
            //}
        }
        catch 
        {
            
        }
        finally
        {

        }
    }

    #endregion
}
public class DashBoardDataCls
{
    public string state { get; set; }
    public double Budgetcost { get; set; }
    public double ActualcosttoDate { get; set; }
    public double Variances { get; set; }
    public double Invoiced { get; set; }
}