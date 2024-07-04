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
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
public partial class QACheckList : System.Web.UI.Page
{
    public string AC2PID, Project;
    DisBindings getdata = new DisBindings();
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        //Master.PageHead = "QA";
        if (!this.IsPostBack)
        {
            if (QueryStringValues.Project>0)
            {
                //bind qa checklist dropdown
                try
                {
                    BindQAChecklist();
                    displaydata();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
    }
    public void BindQAChecklist()
    {
        ddQAAssign.DataSource = DefaultDatabind.b_PortfolioQAchecklist(QueryStringValues.Project);
        ddQAAssign.DataTextField = "Description";
        ddQAAssign.DataValueField = "ID";
        ddQAAssign.DataBind();
        ddQAAssign.Items.Insert(0, Constants.ddlDefaultBind(true));
    }
    public void displaydata()
    {
       
        DataTable dt = new DataTable();
        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand myCommand = new SqlCommand("DN_QACheckListDisplay", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@Project", SqlDbType.Int, 32).Value = QueryStringValues.Project;
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        DataSet ds = new DataSet();
        myadapter.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }


    protected void btn_assign_Click(object sender, EventArgs e)
    {

        Database db = DatabaseFactory.CreateDatabase("DBString");

        try
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_AssignCheckList");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, QueryStringValues.Project);
            db.AddInParameter(cmd, "@ListItem", DbType.Int32, Convert.ToInt32(ddQAAssign.SelectedValue));
            db.AddOutParameter(cmd, "@outval", DbType.Int32, 4);
             db.ExecuteNonQuery(cmd);
             int getVal = (int)db.GetParameterValue(cmd, "@outval");
            cmd.Dispose();
           if(getVal==1)
           {
               displaydata(); 
           }
            else
           {
            lblError.Text="No items in the QA Checklist.";
           }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
        ddQAAssign.SelectedIndex = 0;
    }

    public string CheckIssued(string ID)
    {
        string  CheckIssued;
        Project = Request.QueryString["Project"].ToString();
        int OldSID;
        OldSID = sessionKeys.SID;
        string sql = "update AC2P_QAItems set DateQAApproved='" + DateTime.Now.ToShortDateString() + "', CheckedStatus='1', CheckedBy='" + OldSID + "' where ID='" + ID + "' and PRojectReference= '" + Project + "'";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand comm_Approve = new SqlCommand(sql, con);
        con.Open();
          CheckIssued = comm_Approve.ExecuteNonQuery().ToString();
        comm_Approve.Connection.Close();
         return CheckIssued;
      
    }


    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //lbl_Results.Visible = true;
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string delete = "delete from AC2P_QAItems where ID='" + int.Parse(id) + "'";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(delete, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
        finally
        {
            con.Close();
        }
       
        displaydata();
      

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        displaydata();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DueDate = "";
        string Notes = "";
        string ListDetails = "";
        int QACheckDetails = 0;

        if (e.CommandName == "EmptyInsert")
        {

            if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtDueDate2")).Text != "")
            {
                DueDate = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtDueDate2")).Text;
            }
            if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtListItem2")).Text != "")
            {
                ListDetails = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtListItem2")).Text;
            }
            if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtNotes2")).Text != "")
            {
                Notes = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtNotes2")).Text;
            }
           
                      
            try
            {
               
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_InsetQACheckList");
                db.AddInParameter(cmd, "@Project", DbType.Int32, QueryStringValues.Project);
                db.AddInParameter(cmd, "@AC2PID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@QAListID", DbType.String , Convert.ToInt32(ddQAAssign.SelectedValue));
                db.AddInParameter(cmd, "@ListItem", DbType.String, ListDetails);
                db.AddInParameter(cmd, "@Notes", DbType.String, Notes);
                db.AddInParameter(cmd, "@ListPosition", DbType.Int32, 1);
                db.AddInParameter(cmd, "@DueDateValue", DbType.DateTime, DueDate);
                db.AddOutParameter(cmd, "@out", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd);
                int getVal = (int)db.GetParameterValue(cmd, "@out");
                cmd.Dispose();
                if (getVal == 0)
                {
                    
                    lblError.Text = "Error while due date is greater than the project end date";
                }              

            }

            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
             

            }
        }

        if (e.CommandName =="Insert")
        {
            if (((TextBox)GridView1.FooterRow.FindControl("txtDueDate1")).Text != "")
            {
                DueDate = ((TextBox)GridView1.FooterRow.FindControl("txtDueDate1")).Text;
            }
            if (((TextBox)GridView1.FooterRow.FindControl("txtListItem1")).Text != "")
            {
                ListDetails = ((TextBox)GridView1.FooterRow.FindControl("txtListItem1")).Text;
            }
            if (((TextBox)GridView1.FooterRow.FindControl("txtNotes1")).Text != "")
            {
                Notes = ((TextBox)GridView1.FooterRow.FindControl("txtNotes1")).Text;
            }
                      
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_InsetQACheckList");
                db.AddInParameter(cmd, "@Project", DbType.Int32, QueryStringValues.Project);
                db.AddInParameter(cmd, "@AC2PID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@QAListID", DbType.String, ddQAAssign.SelectedValue);
                db.AddInParameter(cmd, "@ListItem", DbType.String, ListDetails);
                db.AddInParameter(cmd, "@Notes", DbType.String, Notes);
                db.AddInParameter(cmd, "@ListPosition", DbType.Int32, 1);
                db.AddInParameter(cmd, "@DueDateValue", DbType.DateTime, DueDate);
                db.AddOutParameter(cmd, "@out", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd);
                int getVal = (int)db.GetParameterValue(cmd, "@out");
                cmd.Dispose();
                if (getVal == 0)
                {
                    lblError.Text = "Error while due date is greater than the project end date";
                }
            }

            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
        }
        if (e.CommandName == "Update")
        {
            int OldSID;

            OldSID = sessionKeys.SID;
            int id1 = Convert.ToInt32(e.CommandArgument.ToString());
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];

          
            TextBox tx2 = (TextBox)GridView1.Rows[i].FindControl("txtListItem");
            TextBox tx3 = (TextBox)GridView1.Rows[i].FindControl("txtDueDate");
            TextBox tx4 = (TextBox)GridView1.Rows[i].FindControl("txtNotes");
            SqlConnection con = new SqlConnection(connectionString);
            
            try
            {
                SqlCommand myCommand = new SqlCommand("DN_QACheckUpadate", con);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = id1;
                myCommand.Parameters.Add("@ListItem", SqlDbType.VarChar, 50).Value = tx2.Text;


                if (tx3.Text == "")
                {
                    myCommand.Parameters.Add("@DueDate", SqlDbType.SmallDateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    myCommand.Parameters.Add("@DueDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(tx3.Text).ToShortDateString();
                }

                myCommand.Parameters.Add("@Notes", SqlDbType.VarChar, 50).Value = tx4.Text;
                myCommand.Parameters.Add("@SID", SqlDbType.VarChar, 50).Value = OldSID;
                con.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                GridView1.EditIndex = -1;

            }

            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                con.Close();

            }
            

        }

        if (e.CommandName == "Checked")
        {
            int OldSID;
            OldSID = sessionKeys.SID;
            int id1 = Convert.ToInt32(e.CommandArgument.ToString());

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("update AC2P_QAItems set DateQAApproved='" + DateTime.Now.ToShortDateString() + "', CheckedStatus='1', CheckedBy='" + OldSID + "' where ID='" + id1 + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                con.Close();

            }
        }
        displaydata();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
    }
    protected void QACheckDisplay_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
      
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        try
        {
            updateapprove();
            Response.Redirect(string.Format("QACheckList.aspx?Project={0}",QueryStringValues.Project.ToString()),false);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void updateapprove()
    {
        int OldSID;
        string AppName = "", Description = "", QAApprovalDate="";
        string ProjectPrefix = "";
        string WebURL = "";
        string QAEmail = "";
        int QAID = 0;
        string ProjectDescription = "";
        OldSID = sessionKeys.SID;

        //AC2PID = Request.QueryString["AC2PID"].ToString();
        Project = Request.QueryString["Project"].ToString();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand comm_ApplicationName = new SqlCommand("select * from ProjectDefaults", con);
        con.Open();
        SqlDataReader dr1 = comm_ApplicationName.ExecuteReader();
        //dr1.Read();
        if (dr1.Read())
        {
            AppName = dr1.GetValue(8).ToString();
            ProjectPrefix = dr1.GetValue(9).ToString();
          
        }
        dr1.Close();
        con.Close();

        SqlCommand comm_Approve = new SqlCommand("Update AssignedContractorsToProjects set OpsStatusID='6',QAApprovalDate='" + DateTime.Now.ToShortDateString() + "' where ID=" + AC2PID + "", con);
        con.Open();
        int i = comm_Approve.ExecuteNonQuery();
        con.Close();
        SqlCommand comm_ApplicationName1 = new SqlCommand("SELECT MasterTemplate.Description,AssignedContractorsToProjects.ID,AssignedContractorsToProjects.ProjectReference,AssignedContractorsToProjects.ContractorsName,AssignedContractorsToProjects.QAApprovalDate FROM AssignedContractorsToProjects INNER JOIN MasterTemplate ON AssignedContractorsToProjects.TemplateID = MasterTemplate.ID WHERE AssignedContractorsToProjects.ID ='" + AC2PID + "'", con);
        con.Open();
        SqlDataReader dr3 = comm_ApplicationName1.ExecuteReader();
        //dr1.Read();
        if (dr3.Read())
        {
            Description = dr3.GetValue(2).ToString();
            QAApprovalDate = dr3.GetValue(4).ToString();

        }
        dr1.Close();
        con.Close();

        string TextToAppend = "QA approval received for Project: " + ProjectPrefix + Project + " completed by: " + OldSID + " ProjectDescription " + Description;
     SqlCommand comm_Append = new SqlCommand("UPDATE Projects SET ProjectComments= ProjectComments+ '" + TextToAppend + "' where ProjectReference='" + Project + "'", con);
     con.Open();
        int i1 = comm_Append.ExecuteNonQuery();
        con.Close();
        
        
    }

    
    protected void BtnCheckAll_Click(object sender, EventArgs e)
    {
        try
        {
            //AC2PID = Request.QueryString["AC2PID"].ToString();
            Project = Request.QueryString["Project"].ToString();
            int OldSID;
            OldSID = sessionKeys.SID;
            string sql = "update AC2P_QAItems set DateQAApproved='" + DateTime.Now.ToShortDateString() + "', CheckedStatus='1', CheckedBy='" + OldSID + "' where PRojectReference= '" + Project + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand comm_Approve = new SqlCommand(sql, con);
            con.Open();
            int i = comm_Approve.ExecuteNonQuery();
            con.Close();
            displaydata();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        displaydata();
    }
}
