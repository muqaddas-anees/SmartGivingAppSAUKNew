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
using System.Net.Mail;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class controls_CheckPointRecommendation : System.Web.UI.UserControl {
    int ProjectReference;
    int ContractorID, ID;
    DisBindings getdata = new DisBindings();
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ProjectReference = QueryStringValues.Project;
            ContractorID = sessionKeys.UID;
            if (!Page.IsPostBack)
            {
                lbl1.Visible = false;
                if ((Request.QueryString["Project"] != null))
                {
                    step1_selectProject();

                }

            }

        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }



    }
    protected void btn_ProceedwithProject_Click(object sender, EventArgs e)
    {

        try
        {

            ProjectReference = QueryStringValues.Project;
            ContractorID = sessionKeys.UID;
            if ((txtTechRec.Text != "") || (txtBussinessRec.Text != "") || (txtFinancialRec.Text != "") || (txtOtherRec.Text != "") || (txtPMNotes.Text != "") || (txtResourceRec.Text != ""))
            {
                if (HiddenField1.Value != "")
                {
                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                    DbCommand cmd = db.GetStoredProcCommand("[DN_Recommendationsupdate]");
                    db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
                    // db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(HiddenField1.Value).ToString());
                    db.AddInParameter(cmd, "@ID", DbType.Int32, HiddenField1.Value);
                    db.AddInParameter(cmd, "@TechnicalRecommendations", DbType.String, txtTechRec.Text);
                    db.AddInParameter(cmd, "@FinancialRecommendations", DbType.String, txtFinancialRec.Text);
                    db.AddInParameter(cmd, "@ResourceRecommendations", DbType.String, txtResourceRec.Text);
                    db.AddInParameter(cmd, "@BusinessRecommendations", DbType.String, txtBussinessRec.Text);
                    db.AddInParameter(cmd, "@PMNotes", DbType.String, txtPMNotes.Text);
                    db.AddInParameter(cmd, "@Other", DbType.String, txtOtherRec.Text);
                    db.ExecuteNonQuery(cmd);
                    cmd.Dispose();
                }

                else
                {

                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                    DbCommand cmd = db.GetStoredProcCommand("DN_RecommendationsInsert");
                    //add parameters
                    db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
                    db.AddInParameter(cmd, "@ContractorID", DbType.Int32, ContractorID);
                    db.AddInParameter(cmd, "@TechnicalRecommendations", DbType.String, txtTechRec.Text);
                    db.AddInParameter(cmd, "@FinancialRecommendations", DbType.String, txtFinancialRec.Text);
                    db.AddInParameter(cmd, "@ResourceRecommendations", DbType.String, txtResourceRec.Text);
                    db.AddInParameter(cmd, "@BusinessRecommendations", DbType.String, txtBussinessRec.Text);
                    db.AddInParameter(cmd, "@PMNotes", DbType.String, txtPMNotes.Text);
                    db.AddInParameter(cmd, "@Other", DbType.String, txtOtherRec.Text);
                    db.ExecuteNonQuery(cmd);
                    cmd.Dispose();
                }
            }
            else
            {
                lbl1.Visible = true;
                lbl1.Text = "Please enter any recommendations";
            }
            //updateapprove();
            //Response.Redirect("Opsapproval.aspx?Project=" + ProjectReference + "&T=2");
            //txtTechRec.Text = "";
            //txtFinancialRec.Text = "";
            //txtResourceRec.Text = "";
            //txtBussinessRec.Text = "";
            //txtPMNotes.Text = "";
            //txtOtherRec.Text = "";
            lbl1.Text = "Recommendations updated successfully";
            if (sessionKeys.SID == 7)
            {
                Response.Redirect("~/WF/Portal/CustomerNewHome.aspx?customer=0");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


    private void step1_selectProject()
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            // Initialize the Stored Procedure
            DbCommand cmd = db.GetSqlStringCommand("select * from Recommendations where ProjectReference = " + QueryStringValues.Project.ToString());
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    HiddenField1.Value = dr["ID"].ToString();
                    txtBussinessRec.Text = dr["Business"].ToString();
                    txtFinancialRec.Text = dr["Financial"].ToString();
                    txtOtherRec.Text = dr["Other"].ToString();
                    txtPMNotes.Text = dr["PMNotes"].ToString();
                    txtResourceRec.Text = dr["Resource"].ToString();
                    txtTechRec.Text = dr["Technical"].ToString();


                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //public void updateapprove()
    //{
    //    SqlCommand comm_Approve = new SqlCommand("Update AssignedContractorsToProjects set OpsStatusID='5' where ProjectReference=" + QueryStringValues.Project.ToString() + "", myConnection);
    //    myConnection.Open();
    //    int i = comm_Approve.ExecuteNonQuery();
    //    string TextToAppend = "Project Approved on " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString();
    //    SqlCommand comm_Append = new SqlCommand("UPDATE Projects SET ProjectComments=  '" + TextToAppend + "' where ProjectReference=" + QueryStringValues.Project.ToString() + "", myConnection);
    //    int i1 = comm_Append.ExecuteNonQuery();
    //    myConnection.Close();
    //    lbl1.Visible = true;   



    //    //SqlCommand comm_issues = new SqlCommand("Update ProjectQASchedule set Status=3 where Projectreference=" + ProjectReference + " and AC2PID=" + AC2PID + "", con);
    //    //con.Open();
    //    //int i2=comm_issues.ExecuteNonQuery();
    //    //con.Close ();
    //    //if (i >= 1 || i1 >= 1)
    //    //{
    //    //    Response.Redirect("Opsapproval.aspx?Project=" + ProjectReference + "&T=2");
    //    //}
    //}
    protected void btn_ProjectonHold_Click(object sender, EventArgs e)
    {

        // Response.Redirect("AdminCContacts.aspx?CID=" + drContractors.SelectedValue.ToString() + "&ContractorName=" + drContractors.SelectedItem.Text.ToString());and ToCurrencyID=" + BaseCurrencyID + ""
        ContractorID = Convert.ToInt32(Session["UID"]);
        SqlCommand comm_Approve = new SqlCommand("Update Projects set ProjectStatusID='7' where ProjectReference=" + ProjectReference + "", myConnection);
        myConnection.Open();
        int i = comm_Approve.ExecuteNonQuery();
        string TextToAppend = "Project On Hold " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString();
        SqlCommand comm_Append = new SqlCommand("UPDATE Projects SET ProjectComments=  '" + TextToAppend + "' where ProjectReference=" + ProjectReference + "", myConnection);
        int i1 = comm_Append.ExecuteNonQuery();
        myConnection.Close();
        lbl1.Visible = true;
        lbl1.Text = "Project status has been changed to hold.";
    }
}
