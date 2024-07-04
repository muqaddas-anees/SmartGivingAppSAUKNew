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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class QASummary : System.Web.UI.Page
{
    int oldsid;
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    protected void Page_Load(object sender, EventArgs e)
    {
        oldsid = sessionKeys.SID;
        //Master.PageHead = "QA";

        if ((oldsid == 3) || (oldsid == 4)||(oldsid ==6))
        {

            Response.Redirect("~/WF/Default.aspx");
        }
        else
        {
            if (!this.IsPostBack)
            {
                DataGrid();
            }
        }

    }
    public void DataGrid()
    {
        try
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand("DN_QASummaryDisplay", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            //myCommand.Parameters.Add("@ProjectStatusID", SqlDbType.Int, 32).Value = 3;
            myCommand.Parameters.Add("@AssignedQA", SqlDbType.Int, 32).Value = sessionKeys.UID;
            myCommand.Parameters.Add("@SID", SqlDbType.Int, 32).Value = sessionKeys.SID;
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            myCommand.Dispose();
            myConnection.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("~/WF/Projects/QA/QAProjectSummary.aspx?Project=" + e.CommandArgument.ToString());

            }
            else if (e.CommandName == "Archived")
            {
                UpdateProjectStatus(int.Parse(e.CommandArgument.ToString()));
                DataGrid();
            }
            else if (e.CommandName == "EmailFinance")
            {
                //UpdateProjectStatus(int.Parse(e.CommandArgument.ToString()));
                Deffinity.ProjectMangers.ProjectManager.UpdateStatusTOReadytoInvoice(int.Parse(e.CommandArgument.ToString()));
                string toEmail= Deffinity.systemdefaults.GetFinanceDistributionEmail();
                string MailContent = Deffinity.ProjectMangers.ProjectManager.Get_ReadyToInvoiceMailContent(int.Parse(e.CommandArgument.ToString()));
                Email email = new Email();
                try
                {
                    email.SendingMail(toEmail, "Ready to Invoice", MailContent);
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
                
                DataGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    #region Update Project status
    private void UpdateProjectStatus(int Projectref)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update projects set ProjectStatusID=5 where ProjectReference=@ProjectReference", new SqlParameter("@ProjectReference", Projectref));
    }
    #endregion

}
