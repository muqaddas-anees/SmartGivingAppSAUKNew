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
using System.Data.Common;
using System.Net.Mail;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class QAApprove : System.Web.UI.Page
{
     //smartNavigation = "True";
    public int Project, AC2PID, ContractorID,QAID;
    Email sendMail = new Email();
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        int oldsid;
        oldsid = sessionKeys.SID;
        lblmsg.Visible = false;
        //DEFFINITY_CUST_FEEDBACK
        try
        {
            int pid = Convert.ToInt32(Request.QueryString["Project"]);
            string strFeedback="";
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            // Initialize the Stored Procedure
            DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_CUST_FEEDBACK");
            db.AddInParameter(cmd, "@ProjectID", DbType.Int32, pid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    strFeedback = dr["Satisfied"].ToString()+",";
                    strFeedback = strFeedback + dr["WaystoImprove"].ToString() + ",";
                    strFeedback = strFeedback + dr["NonPerformingIndividuals"].ToString() + ",";
                    strFeedback = strFeedback + dr["Discuss"].ToString() ;
                    lnksendRequest.Enabled = false;
                    hlViewFeedback.Visible = true;
                    string str = SelectForToolTip(strFeedback);
                    hlViewFeedback.Attributes.Add("onmouseover", " return overlib('" + str + "');");
                    hlViewFeedback.Attributes.Add("onmouseout", "return nd();");   
                }
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "CUSTOMER FEEDBACK ");
        }

        
        if ( (oldsid == 4) || (oldsid == 6))
        {

            Response.Redirect("~/WF/Default.aspx");
        }
        else
        {

           // Master.PageHead = "QA";
            Project = Convert.ToInt32(Request.QueryString["Project"]);

            if (!this.IsPostBack)
            {
            }

            

        }
    }
  
   
    DataSet ds = new DataSet();
    public void updateapprove()
    {
        SqlCommand comm_Approve = new SqlCommand("Update AssignedContractorsToProjects set OpsStatusID='6' where ProjectReference=" + Project + "", con);
        con.Open();
        comm_Approve.ExecuteNonQuery();
        string TextToAppend = "Project Approved on " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString();
        SqlCommand comm_Append = new SqlCommand("Update AssignedContractorsToProjects set OpsStatusID='6', ProjectComments=ProjectComments + '" + TextToAppend + "' where ProjectReference=" + Project + "", con);
        comm_Append.ExecuteNonQuery();
        con.Close();
    }

    private string SelectForToolTip(string ItemList)
    {
        string retVal = "";
        //ItemList = "satisfied,test,test,no";
        try
        {
            string[] arInfo = new string[4];

            // define which character is seperating fields
            char[] splitter = { ',' };

            arInfo = ItemList.Split(splitter);

            retVal = retVal + "<div style=background-color:#F9FBFD><table style=width:100%><tr style=background-color:#ECEEEE><td style=width:41%><strong></strong></td><td><strong></strong></td></tr>";
            retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Satisfaction:</strong></td><td>" + arInfo[0] + "<br/></td></tr>";
            retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Ways to Improve:</strong></td><td>" + arInfo[1]+ "<br/></td></tr>";
            retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Non Performing Resources:</strong></td><td>" + arInfo[2] + "<br/></td></tr>";
            retVal = retVal + "<tr><td style=background-color:#ECEEEE><strong>Would you like to Discuss:</strong></td><td>" + arInfo[3] + "<br/></td></tr>";
            retVal = retVal + "</table></div>";
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return retVal;

    }
    
   
  
  
    protected void btn_Approve_Click(object sender, EventArgs e)
    {

     
    }



    protected void btn_Approve_Click1(object sender, EventArgs e)
    {
        try
        {
            string TodaysDate = DateTime.Now.ToShortDateString();
            string UpdateLog = string.Format("'Project has been QA approved and closed on {0}'", DateTime.Now.ToShortDateString());

            //close the project set the Status to 5 (QA Approved) + date stamp + update the log
            //approve all elements of the project (AC2P) set status to 6 + date stamp
            //If QA Levels =2 then inform group onwers that its been approved
            //Inform Contractor that the project has been approved (part of same email)

            
            SqlCommand sqlProject = new SqlCommand(string.Format("update Projects set ProjectStatusID='6', DateQAApproved={0}, ProjectComments={1} where ProjectReference={2}", TodaysDate, UpdateLog, Project), con);
            con.Open();
            sqlProject.ExecuteNonQuery();
            con.Close();


            SqlCommand cmd = new SqlCommand(string.Format("update AssignedContractorsToProjects set OpsStatusID='6', QAApprovalDate={0} where ProjectReference={1}", TodaysDate, Project), con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("~/WF/Project/QA/QASummary.aspx");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      
      //      //'get Primary QA Designate or if missing then Email Default Project Owner
      //      SqlCommand comm_QA = new SqlCommand("select * from Projects where ProjectReference=" + ProjectReference + "", con);
      //      con.Open();
      //      SqlDataReader dr_QA = comm_QA.ExecuteReader();

      //      if (dr_QA.Read())
      //      {
      //          QAID = Convert.ToInt32(dr_QA.GetValue(19).ToString());
      //          ProjectDescription = dr_QA.GetValue(3).ToString();
      //          StartDate = dr_QA.GetValue(14).ToString();
      //      }
      //      dr.Close();
      //      con.Close();

      //      if (QAID == 0 || QAID == null)
      //      {
      //          //'use the default project owners email address
      //          SqlCommand comm_Email = new SqlCommand("select OwnerEmail from ProjectDefaults", con);
      //          con.Open();
      //          QAEmail = comm_Email.ExecuteScalar().ToString();
      //          con.Close();
      //      }
      //      else
      //      {
      //          SqlCommand comm_Email = new SqlCommand("select EmailAddress from Contractors where ID=" + QAID + "", con);
      //          con.Open();
      //          QAEmail = comm_Email.ExecuteScalar().ToString();
      //      }
      //      //MailAddress to = new MailAddress(QAEmail);
      //      MailAddress from = new MailAddress(AppName + "@2iProjects.com");
      //      MailMessage msg = new MailMessage();
      //      msg.Subject = "Project " + ProjectPrefix + ProjectReference + " is has been approved by QA and is now complete";
      //      string strBody = "";
      //      strBody = strBody + "<p>";
      //      strBody = strBody + "<p><font face=Verdana size=2>The following Project has been marked as complete:";
      //      strBody = strBody + "<p>";
      //      strBody = strBody + "<p>Project " + ProjectPrefix + Request.QueryString.Item("Project") & " is has been approved by QA and is now complete";
      //      strBody = strBody + "<p>";
      //      //strBody = strBody + "<p>Project Description: " + ProjectDescription;
      //      //strBody = strBody + "<p>Project Start Date: " + StartDate;
      //      //strBody = strBody + "<p>Project Completion: " + DateTime.Now.ToShortDateString();
      //      //strBody = strBody + "<p>";
      //      //strBody = strBody + "<p><font face=Verdana size=2>This project now requires your approval.";
      //      //strBody = strBody + "<p><font face=Verdana size=2>Please log into the system and review the details before signing the job off.";
      //      //strBody = strBody + "<p><font face=Verdana size=2>";
      //      //strBody = strBody + "<p>";
      //      strBody = strBody + "<a href='" + WebURL + "'>Click here</a> to access the system";
      //      strBody = strBody + "</BODY></HTML>";
      //      msg.Body = strBody;
      //      //SmtpClient clinet = new SmtpClient();
      //      //clinet.Send(msg);
      //   con.Close();

    }
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        try
        {
            string TodaysDate = DateTime.Now.ToShortDateString();
            string UpdateLog = string.Format("'QA Approval recieved on {0}'", DateTime.Now.ToShortDateString());

            SqlCommand sqlProject = new SqlCommand(string.Format("update Projects set ProjectStatusID='2', ProjectComments={0} where ProjectReference={1}", UpdateLog, Project), con);
            con.Open();
            sqlProject.ExecuteNonQuery();
            con.Close();

            SqlCommand cmd = new SqlCommand(string.Format("update AssignedContractorsToProjects set OpsStatusID='2' where ProjectReference={0}", Project), con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("~/WF/Projects/ProjectPipeline.aspx?Status=2");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    protected void lnksendRequest_Click(object sender, EventArgs e)
    {
        //send mail to Convert.ToInt32(Request.QueryString["Project"])s project Requestor
        sendMail.sendMail(Convert.ToInt32(Request.QueryString["Project"].ToString()), 0, 10);
        lblmsg.Visible = true;
    }
}

