using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
public partial class KPIResource : System.Web.UI.Page
{
    projectTaskDataContext projectDB = new projectTaskDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "KPI";
            BindCustomers();
            BindProgramme();
            BuildTable();
        }
    }
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomer.DataSource = portfolio;
            ddlCustomer.DataTextField = "PortFolio";
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProgramme()
    {
        try
        {
            ProgrammeDataContext ProgrammeContext = new ProgrammeDataContext();
            var Owner = from c in ProgrammeContext.OperationsOwners
                        where c.Level == 1
                        orderby c.OperationsOwners
                        select c;
            ddlProgramme.DataSource = Owner;
            ddlProgramme.DataTextField = "OperationsOwners";
            ddlProgramme.DataValueField = "ID";
            ddlProgramme.DataBind();
            ddlProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void imgView_Click(object sender, EventArgs e)
    {

        BuildTable();
    }
    private void BuildTable()
    {

        var LabelsList = (from r in projectDB.KPI_LablesNames
                          join c in projectDB.KPI_Categories on r.KPICategoryID equals c.ID
                          where r.PageType == 2
                          select new
                          {
                              LabelID = r.LabelID,
                              LabelsName = r.LabelsName,
                              categoty = c.KpiCatogeryName,
                              ID = r.ID
                          }).ToList();

        string text = string.Empty;
        text += "<table width='100%' class='table table-small-font table-bordered table-striped dataTable'  id='mainTable'  bordercolor='White' border='0' cellpadding='0' cellspacing='1' BackColor='#FFFDF3' ><tr class='tab_header'><th class='header_bg_l' style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</th><td>Category</td><td>Target(" + int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text).ToString() + ")</td><td style='align:left'>Current Performance</td>";
        text += "<td style='align:left'>Variance</td><td></td><td style='align:left'>Performance(" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 1).ToString() + ")</td><th class='header_bg_r' style='align:left'>Performance (" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 2).ToString() + ")</th>";
        text += "</tr>";
        //style='color:black;font-size:10px'
        if (LabelsList != null)
        {
            foreach (var lblID in LabelsList)
            {
                if (lblID.LabelID == 38)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(38).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 39)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(39).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 40)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(40).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>"; 
                    //text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + ChangeToHours(NoOfProjects_Target(40).ToString("N2")) + "</td><td align='right'>" + ChangeToHours(Performance_Overtime(0, 40).ToString()) + "</td><td align='right'>" + ChangeToHours(Variance(Performance_Overtime(0, 40), NoOfProjects_Target(40))) + "</td><td>" + VarianceRAG(Performance_Overtime(0, 40), NoOfProjects_Target(40)) + "</td><td align='right'>" + ChangeToHours(Performance_Overtime(1, 40).ToString()) + "</td><td align='right'>" + ChangeToHours(Performance_Overtime(2, 40).ToString()) + "</td><tr>";
                }
                if (lblID.LabelID == 41)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(41).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 42)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(42).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 43)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(43).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 44)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(44).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 45)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(45).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 46)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(46).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 47)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(47).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 48)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(48).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 49)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(49).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 50)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(50).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 51)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(51).ToString("N2") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
               
            }
        }
        text += "</table>";
        ltlDisplay.Text = text;
    }
    private string Currentdate(string year)
    {
        int day = DateTime.Now.Day;
        int mont = DateTime.Now.Month;
        year = mont.ToString() + "/" + day.ToString() + "/" + year;
        return year;
    }
    private string NoOfProjectsTarget_Query1(string fromDate, int label)
    {
        // = "01/01/" + fromDate;
        string sql = "";
        //sql = "select  ISNULL(targetvalues,0) as ID from KPI_TargetValues where KPI_LabelID=2  and YEAR(TargetYears)=YEAR(DATEADD(year," + years.ToString() + ", '" + Currentdate(fromDate) + "'))";
        sql = "select  ISNULL(targetvalues,0) as BuyingPrice from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + Currentdate(fromDate) + "')";



        return sql;
    }
    private double NoOfProjects_Target(int label)
    {
        string fromDate = txtFromDate.Text;
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(NoOfProjectsTarget_Query1(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text, label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Val.Value;
        }

        return val;
    }
    #region TimesheetEntry-OverTime
    private double Performance_Overtime(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Performance_OverTime(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.BuyingPrice.Value;
        }

        return val;
    }
    private string Performance_OverTime(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        if (label == 40)
        {
            sql = " select  isnull(cast(dbo.ConvertToHours(sum(dbo.ConvertToMins(hours)))as float),0) as BuyingPrice from TimesheetEntry  where entrytype=2 and  YEAR(DateEntered)=year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))" + " and  ProjectReference in (";
        }
        sql = sql + " select ProjectReference from Projects P where P.ProjectStatusID in (6,2) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += ")";
        return sql;
    }
    #endregion
    private string RAG_Variance(double CurrentPerformance, double target)
    {
        string text = "";
        //if (target != 0)
        //{
        CurrentPerformance = -1 * CurrentPerformance;
        if (CurrentPerformance > target)
        {
            text = "<img alt='green' src='media/indcate_green.png' />";
            //text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_green.png' />";
        }
        else if (CurrentPerformance < target)
        {
            // text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_red.png' />";
            text = "<img alt='red' src='media/indcate_red.png'   />";
        }
        else
        {
            text = "";
        }
        //}
        //else
        //{

        //}
        return text;
    }

    private string Variance(double currentPerformance, double targetValue)
    {

        double val = targetValue - currentPerformance;

        return val.ToString("N2");
    }
    private string VarianceRAG(double currentPerformance, double targetValue)
    {
        string text = "";
        double val = targetValue - currentPerformance;
        if (currentPerformance > targetValue)
        {
            text = "<img  src='media/indcate_green.png' />";
            //text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_green.png' />";
        }
        else if (currentPerformance < targetValue)
        {
            // text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_red.png' />";
            text = "<img  src='media/indcate_red.png'   />";
        }
        else
        {
            text = "";
        }
        return text;
    }
    public string ChangeToHours(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        if (!GetHours.Contains("."))
        {
            GetHours = GetHours + ".00";

        }
        string[] displayTime = GetHours.Split(comm1);
       

        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }



}
