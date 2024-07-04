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

public partial class QASummary1 : System.Web.UI.Page
{
    public string ac2pstatus;
    public int Project, AC2PID, ContractorID;
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        int oldsid;

        oldsid = sessionKeys.SID;

        if ((oldsid == 3) ||(oldsid == 4) || (oldsid == 6))
        {
            Response.Redirect("Default.aspx");
        }
        else
        {

           // Master.PageHead = "QA";
            txtProjectTitle.Focus();
           // AC2PID = Convert.ToInt32(Request.QueryString["AC2PID"]);
            Project = Convert.ToInt32(Request.QueryString["Project"]);
            if (Request.QueryString["Project"] != null)
            {
                try
                {
                    Project = Convert.ToInt32(Request.QueryString["Project"]);
                    DisplayData(Project);
                    
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
            }

            selectProject();
        }
    }
    private void selectProject()
    {
       
       

    }
    
    public void DisplayData(int value)
    {

        SqlCommand com_select = new SqlCommand("Deffinity_ProjectDetails", con);
        com_select.CommandType = CommandType.StoredProcedure;
        com_select.Parameters.Add(new SqlParameter("@ProjectReference", QueryStringValues.Project));

        com_select.Connection.Open();
        try
        {
            using (IDataReader datareader = com_select.ExecuteReader())
            {
                while (datareader.Read())
                {
                    txt_city.InnerText = datareader["CityName"].ToString();
                    txt_counrty.InnerText = datareader["CountryName"].ToString();
                    txtsite.InnerText = datareader["SiteName"].ToString();                    
                    txtdescription.InnerText = datareader["ProjectDescription"].ToString();
                    txtProjectTitle.InnerText = datareader["ProjectTitle"].ToString();
                    txtowner.InnerText = datareader["OwnerName"].ToString();
                    txtemail.InnerText = datareader["OwnerEmail"].ToString();
                    txtPortfolio.InnerText = datareader["PortfolioName"].ToString();
                    txtProgramme.InnerText = datareader["ProgrammeName"].ToString();
                    txtSubprogramme.InnerText = datareader["SubProgrammeName"].ToString();
                    txtScheduledQAdate.InnerText = string.IsNullOrEmpty(datareader["ScheduledQADate"].ToString())?string.Empty:Convert.ToDateTime(datareader["ScheduledQADate"].ToString()).ToShortDateString();
                    txtactualdate.InnerText = string.Format("{0:#.00}", datareader["ActualCost"]).ToString();
                    txtbudgetlevel0.InnerText = string.Format("{0:#.00}", datareader["BudgetaryCost"]).ToString();
                    txtbudgetlevel3.InnerText = string.Format("{0:#.00}", datareader["BudgetaryCostLevel3"]).ToString();
                    txtstartdate.InnerText = Convert.ToDateTime(datareader["StartDate"].ToString()).ToShortDateString();
                    txtcompleteddate.InnerText = Convert.ToDateTime(datareader["ProjectEndDate"].ToString()).ToShortDateString();
                }
                datareader.Close();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            com_select.Connection.Close();
            com_select.Dispose();
        }
    }
}
