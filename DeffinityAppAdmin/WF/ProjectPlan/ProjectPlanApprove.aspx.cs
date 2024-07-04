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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class ProjectPlanApprove : System.Web.UI.Page
{
    ReportDocument rpt;    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Proposal";
    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindReport();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
     void BindReport()
    {
        if (QueryStringValues.ProjectPlanID == 0)
        {
            return;
        }
        string mailId = GetOwnerEmail(QueryStringValues.ProjectPlanID);
        rpt = new ReportDocument();        

        //Load the selected report file.

        string str = "~/WF/ProjectPlan/Reports/ProjectPlanNew.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DEFFINITY_ProjectplanRpt", MyCon);
        SqlCommand myCommand1 = new SqlCommand("DEFFINITY_ProjectplanActivityRpt", MyCon);


        //if (Request.QueryString["projectPlanID"] != null)
        //    projectPlanID = Convert.ToInt32(Request.QueryString["projectPlanID"]);
        //else
        //    projectPlanID = 0;

        myCommand.Parameters.AddWithValue("ProjectPlanID", QueryStringValues.ProjectPlanID);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.AddWithValue("ProjectPlanID", QueryStringValues.ProjectPlanID);
        myCommand1.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
        myAdapter1.Fill(dt1);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);
       
        System.IO.Stream myStream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
        myStream.ToString();
        Email email = new Email();
        ArrayList mailIds = new ArrayList();
        mailIds.Add(mailId);
        email.SendingMail(string.Empty, "Report", "Report for Project Plan - " + QueryStringValues.ProjectPlanID.ToString(), "", string.Empty, mailIds, myStream);//, "dummyReport.pdf");
        
    }
     public string GetOwnerEmail(int ProjectPlanID)
     {
         //SqlCommand cmd = new SqlCommand();
         SqlConnection con = new SqlConnection(Constants.DBString);
         string strcmd;
         string OwnerEmailID=string.Empty;
         strcmd = string.Format("select EmailAddress from contractors inner join Projectplan on contractors.ID=Projectplan.OwnerID where Projectplan.ProjectplanID={0}",QueryStringValues.ProjectPlanID); 
         SqlCommand cmd = new SqlCommand(strcmd,con);
         cmd.CommandType = CommandType.Text;
         try
         {
             con.Open();
             OwnerEmailID = cmd.ExecuteScalar().ToString();
         }
         catch (Exception ex)
         {
             LogExceptions.LogException(ex.Message);
         }
         finally
         {
             con.Close();
         }

         return OwnerEmailID;
     }
     protected void Page_UnLoad(object sender, EventArgs e)
     {
         if (rpt != null)
         {
             rpt.Close();
             rpt.Dispose();
         }
     }
}
