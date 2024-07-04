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
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.ApplicationBlocks.Data;


public partial class ReportView : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    
    string listID ="";
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ( Convert.ToInt32(Request.QueryString["ID"]) == 0 || Request.QueryString["ID"] == null)
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
                //SqlCommand cmd = new SqlCommand("select ID,ContractorName from Contractors where Status ='Active' order by ContractorName ", myConnection);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);
               
                ListBox1.DataSource = dt;
                ListBox1.DataTextField = "ContractorName";
                ListBox1.DataValueField = "ID";
                ListBox1.DataBind();
                ListBox1.Items.RemoveAt(0);

                //dt.Clear();
            }
            else
            {
                //listPanel.Visible = false;
                
                               
                    BindRpt(Request.QueryString["ID"]);
                }
            
            }
        else
        {
                for (int i = 0; i <= (ListBox1.Items.Count - 1); i++)
                    {
                        string s = ListBox1.Items[i].Selected.ToString();
                        string s1 = ListBox1.Items[i].Value.ToString();
                        if (ListBox1.Items[i].Selected)
                        {
                            listID = listID + ListBox1.Items[i].Value + ",";
                        }
                    }
            BindRpt(listID);
                
        }

    }
    
    protected void BtnView_Click(object sender, ImageClickEventArgs e)
    {
       // string s2 = ListBox1.SelectedValue.ToString();
        string st="";
        lblMsg.Text = "";
        st=ListBox1.SelectedValue.ToString();
        
        if ( st == "")
        {
            lblMsg.Text="Please select contractor name";
        }
        else
        {
            for (int i = 0; i <= (ListBox1.Items.Count - 1); i++)
            {
                string s = ListBox1.Items[i].Selected.ToString();
                string s1 = ListBox1.Items[i].Value.ToString();
                if (ListBox1.Items[i].Selected)
                {
                    listID = listID + ListBox1.Items[i].Value + ",";
                }
            }


            BindRpt(listID);
        }



    }

    protected void BindRpt(string LID)
    {

        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "GanttReport4.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("AMPS_ProjectforecastContractorsDetails", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        
        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = 0;
        myCommand.Parameters.Add("@LID", SqlDbType.VarChar, 100).Value = LID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;
        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Resource Report");
        Response.End();
    
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
