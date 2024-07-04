using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
public partial class KPIServiceDesk : System.Web.UI.Page
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
    protected void imgView_Click(object sender, EventArgs e)
    {
        BuildTable();
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
    private void BuildTable()
    {

        var LabelsList = (from r in projectDB.KPI_LablesNames
                          join c in projectDB.KPI_Categories on r.KPICategoryID equals c.ID
                          where r.PageType == 4
                          select new
                          {
                              LabelID = r.LabelID,
                              LabelsName = r.LabelsName,
                              categoty = c.KpiCatogeryName,
                              ID = r.ID
                          }).ToList();

        string text = string.Empty;
        text += "<table width='100%' class='table table-small-font table-bordered table-striped dataTable'  id='mainTable'  bordercolor='White' border='0' cellpadding='0' cellspacing='1' BackColor='#FFFDF3' ><tr class='tab_header'><th class='header_bg_l' style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</th><td style='align:left'>Category</td><td style='align:left'>Target(" + int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text).ToString() + ")</td><td style='align:left'>Current Performance</td>";
        text += "<td style='align:left'>Variance</td><td style='align:left'>Performance(" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 1).ToString() + ")</td><th class='header_bg_r' style='align:left'>Performance (" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 2).ToString() + ")</th>";
        text += "</tr>";
        //style='color:black;font-size:10px'
        if (LabelsList != null)
        {
            foreach (var lblID in LabelsList)
            {
                if (lblID.LabelID == 57)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 58)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 59)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 60)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 61)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 62)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 63)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 64)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 65)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 66)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 67)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 68)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 69)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 70)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 71)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 72)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 73)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 74)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
              
            }
        }
        text += "</table>";
        ltlDisplay.Text = text;
    }
}
