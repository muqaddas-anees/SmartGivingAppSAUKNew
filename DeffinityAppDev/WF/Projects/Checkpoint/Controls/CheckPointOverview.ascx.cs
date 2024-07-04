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

public partial class controls_CheckPointOverview : System.Web.DynamicData.FieldTemplateUserControl {
    int AC2PID;
    int ProjectReference;
    int ContractorID;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    SqlCommand com_select;
    SqlDataAdapter adp;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Overview";

        ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
        if (!Page.IsPostBack)
        {
            binddata();
            //if (sessionKeys.SID == 7)
            //    pnlVisible.Visible = false;
            //else
            //    pnlVisible.Visible = true;
        }
    }
    public void binddata()
    {

        com_select = new SqlCommand("Deffinity_ProjectDetails", con);
        com_select.CommandType = CommandType.StoredProcedure;
        com_select.Parameters.Add(new SqlParameter("@ProjectReference", QueryStringValues.Project));

        com_select.Connection.Open();
        try
        {
            using (IDataReader datareader = com_select.ExecuteReader())
            {
                while (datareader.Read())
                {

                    txtProjectTitle.InnerText = datareader["ProjectTitle"].ToString();
                    txtOwner.InnerText = datareader["OwnerName"].ToString();
                    txtOwneremail.InnerText = datareader["OwnerEmail"].ToString();
                    txtStatus.InnerText = datareader["ProjectStatusName"].ToString();
                    txtProjectdesc.InnerText = datareader["ProjectDescription"].ToString();
                    txtCountry.InnerText = datareader["CountryName"].ToString();
                    txtCity.InnerText = datareader["CityName"].ToString();
                    txtSite.InnerText = datareader["SiteName"].ToString();
                    txtScheduledQAdate.InnerText = String.Format("{0:d}", datareader["ScheduledQADate"]);
                    txtCostCentre.InnerText = datareader["CostCentre"].ToString();
                    //txtBudgetCost.InnerText = String.Format("{0:#.00}", datareader["BudgetaryCost"]);
                    txtStartdate.InnerText = String.Format("{0:d}", datareader["StartDate"]);
                    txtEndDate.InnerText = String.Format("{0:d}", datareader["ProjectEndDate"]);
                    //txtBudjectCostL3.InnerText = String.Format("{0:#.00}", datareader["BudgetaryCostLevel3"]);
                    //txtActualcosts2date.InnerText = String.Format("{0:#.00}", datareader["ActualCost"]);
                    txtPortfolio.InnerText = datareader["PortfolioName"].ToString();
                    txtProgramme.InnerText = datareader["ProgrammeName"].ToString();
                    txtSubprogramme.InnerText = datareader["SubProgrammeName"].ToString();

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
        //lblCalcLabour.InnerText = CalculateLabour();
        //lblCalcMeterialsS.InnerText = CalculateMaterials();
        //lblCalcMisc.InnerText = CalculateMisc();
        //lbltotal.InnerText = TotalCostingSheet();
    }
    double TotalLabour = 0.00;
    double TotalMaterials = 0.00;
    double TotalMisc = 0.00;
    public string CalculateLabour()
    {
        SqlCommand comm_CLabour = new SqlCommand("select ConvertedTotal from AC2Proj_Labour where ProjectReference='" + ProjectReference + "'", con);
        con.Open();
        SqlDataReader dr = comm_CLabour.ExecuteReader();
        if (dr.Read())
        {
            do
            {
                TotalLabour = TotalLabour + Convert.ToDouble(dr.GetValue(0).ToString());

            } while (dr.Read());
            //TotalLabour = formatnumber(TotalLabour,2);
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label1.Text = TotalLabour.ToString();
        }
        else
        {
            TotalLabour = 0.00;
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label1.Text = TotalLabour.ToString();
        }
        dr.Close();
        con.Close();
        Double x = Math.Round(TotalLabour, 2);
        //return string.Format("{0:c}", MiscTotal);
        //return Math.Round(TotalLabour, 2).ToString("0.00");
        return string.Format("{0:c}", TotalLabour);

    }
    public string CalculateMaterials()
    {
        SqlCommand comm_CMaterials = new SqlCommand("select ConvertedTotal from AC2Proj_Materials where ProjectReference='" + ProjectReference + "'", con);
        con.Open();
        SqlDataReader dr = comm_CMaterials.ExecuteReader();
        if (dr.Read())
        {
            do
            {
                TotalMaterials = TotalMaterials + Convert.ToDouble(dr.GetValue(0).ToString());

            } while (dr.Read());
            //TotalLabour = formatnumber(TotalLabour,2);
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label2.Text = TotalMaterials.ToString();
        }
        else
        {
            TotalMaterials = 0.00;
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label2.Text = TotalMaterials.ToString();
        }
        dr.Close();
        con.Close();
        //return Math.Round(TotalMaterials, 2).ToString("0.00");
        return string.Format("{0:c}", TotalMaterials);
    }
    public string CalculateMisc()
    {
        SqlCommand comm_CMisc = new SqlCommand("select ConvertedTotal from AC2Proj_Misc where ProjectReference='" + ProjectReference + "'", con);
        con.Open();
        SqlDataReader dr = comm_CMisc.ExecuteReader();
        if (dr.Read())
        {
            do
            {
                TotalMisc = TotalMisc + Convert.ToDouble(dr.GetValue(0).ToString());

            } while (dr.Read());
            //TotalLabour = formatnumber(TotalLabour,2);
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label3.Text = TotalMisc.ToString();
        }
        else
        {
            TotalMisc = 0.00;
            //Response.Write("<FONT face=Verdana size=1>" + TotalLabour);
            //Label3.Text = TotalMisc.ToString();
        }
        dr.Close();
        con.Close();
        //return Math.Round(TotalMisc, 2).ToString("0.00");
        return string.Format("{0:c}", TotalMisc);
    }
    public string TotalCostingSheet()
    {
        double total = TotalLabour + TotalMaterials + TotalMisc;
        //Label4.Text = total.ToString();
        //return Math.Round(total, 2).ToString("0.00");
        return string.Format("{0:c}", total);
    }


    protected void btnProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectOverviewV4.aspx?project=" + QueryStringValues.Project);
    }
}
