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
public partial class KPICustomers : System.Web.UI.Page
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
                          where r.PageType == 3
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
                if (lblID.LabelID == 52)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 53)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 54)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 55)
                {
                    text += "<tr class='even_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 56)
                {
                    text += "<tr class='odd_row'><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                }
            }
        }
        text += "</table>";
        ltlDisplay.Text = text;
    }
}
