using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IncidentMgt.DAL;
using IncidentMgt.Entity;
using System.Net.Mail;
using System.Collections;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public partial class Servicedesk_sdcontrols_sd_unitadministration : System.Web.DynamicData.FieldTemplateUserControl {
    IncidentDataContext obj = new IncidentDataContext();
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        if (!IsPostBack)
        {
            LoadGrid();
            LoadUnitConsumptionGrid();
            GetMinunitAllocation();
            GetUnitConsumptionRAGalert();
        }
       
    }

   
    public void LoadGrid()
    {
        try
        {
            lblerrormsg.Text = string.Empty;
            int PortfolioId = sessionKeys.PortfolioID;
            List<Incident_UnitExpiryPeriod> iu = obj.Incident_UnitExpiryPeriods.Where(p => p.PortfolioID == PortfolioId).ToList();
            Incident_UnitExpiryPeriod iu_single = new Incident_UnitExpiryPeriod();
            iu_single.ID = 0;
            iu_single.CategoryID = 1;
            iu.Add(iu_single);
            var Result = from p in iu
                         join d in obj.Incident_UnitCategories on p.CategoryID equals d.ID 
                         select new { p.ID, p.PortfolioID,p.PeriodType, p.Period, d.Name };
            GridView1.DataSource = Result;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void GetMinunitAllocation()
    {
         int PortfolioId = sessionKeys.PortfolioID;
         var result = (from p in obj.Incident_MinimumUnitsAssignedPerCalls
                       where p.PortfolioID == PortfolioId
                       select p.MinimumUnitsAssignedPerCall).SingleOrDefault();
         txtMinUnits.Text = result.ToString();
    }

    public void GetUnitConsumptionRAGalert()
    {
        int PortfolioId = sessionKeys.PortfolioID;

        var txtremaining1 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                             where p.PortfolioID == PortfolioId && p.RAG == "Green"
                             select p.RemainingUnits).SingleOrDefault();

        var txtremaining2 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                             where p.PortfolioID == PortfolioId && p.RAG == "Amber"
                             select p.RemainingUnits).SingleOrDefault();
        var txtremaining3 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                             where p.PortfolioID == PortfolioId && p.RAG == "Red"
                             select p.RemainingUnits).SingleOrDefault();

        var txtemail1 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                         where p.PortfolioID == PortfolioId && p.RAG == "Green"
                         select p.EmailDistribution).SingleOrDefault();
        var txtemail2 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                             where p.PortfolioID == PortfolioId && p.RAG == "Amber"
                             select p.EmailDistribution).SingleOrDefault();
        var txtemail3 = (from p in obj.Incident_UnitConsumptionRAGAlerts
                             where p.PortfolioID == PortfolioId && p.RAG == "Red"
                             select p.EmailDistribution).SingleOrDefault();


        txtRemainingUnit1.Text = txtremaining1.ToString();
        txtRemainingUnit2.Text = txtremaining2.ToString();
        txtRemainingUnit3.Text = txtremaining3.ToString();
        txtEmail4.Text = txtemail1;
        txtEmail5.Text = txtemail2;
        txtEmail6.Text = txtemail3;

    }

   public void LoadUnitConsumptionGrid()
    {
        try
        {
            int PortfolioId = sessionKeys.PortfolioID;
            List<Incident_UnitConsumptionConfiguration> iucc = obj.Incident_UnitConsumptionConfigurations.Where(p => p.PortfolioID == PortfolioId).ToList();
            Incident_UnitConsumptionConfiguration iuccc_single = new Incident_UnitConsumptionConfiguration();
            iuccc_single.ID = 0;
            iuccc_single.PortfolioID = sessionKeys.PortfolioID;
            iucc.Add(iuccc_single);

            var UnitConsumptionResult = from p in iucc
                                        where p.PortfolioID == PortfolioId
                                        select p;

            gvunitconsumption.DataSource = UnitConsumptionResult;
            gvunitconsumption.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
     
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            var result = from p in obj.Incident_UnitCategories select p;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("lblid") != null)
                {
                    Label lblid = e.Row.FindControl("lblid") as Label;
                    if (lblid.Text == "0")
                    {
                        e.Row.Visible = false;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (e.Row.FindControl("ddlUnitCategory") != null)
                {
                    DropDownList ddlmaster = e.Row.FindControl("ddlUnitCategory") as DropDownList;
                    ddlmaster.DataSource = result;
                    ddlmaster.DataValueField = "ID";
                    ddlmaster.DataTextField = "Name";
                    ddlmaster.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete1")
            {
                using (IncidentDataContext obj = new IncidentDataContext())
                {
                    string id = e.CommandArgument.ToString();

                    var UnitExpiryPeriod_Delete = from p in obj.Incident_UnitExpiryPeriods
                                                  where p.ID == int.Parse(id)
                                                  select p;
                    obj.Incident_UnitExpiryPeriods.DeleteAllOnSubmit(UnitExpiryPeriod_Delete);
                    obj.SubmitChanges();
                }

                               
                GridView1.EditIndex = -1;
                LoadGrid();
                LoadUnitConsumptionGrid();


            }
            if (e.CommandName == "Add")
            {
                int PortfolioId = sessionKeys.PortfolioID;
                int UnitCategory = Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddlUnitCategory")).SelectedValue);
                string SetExpiryPeriod = ((DropDownList)GridView1.FooterRow.FindControl("ddlSetExpiryPeriod")).SelectedItem.Text.Trim();
                
                string periods = ((TextBox)GridView1.FooterRow.FindControl("txtPeriod")).Text.Trim();
                int Period = Convert.ToInt32(string.IsNullOrEmpty(periods) ? "0" : periods);

                var resut = (from p in obj.Incident_UnitExpiryPeriods
                             where p.PortfolioID == PortfolioId && p.CategoryID == UnitCategory
                             select p).Count();
                if (resut == 0)
                {
                    Incident_UnitExpiryPeriod ue = new Incident_UnitExpiryPeriod
                    {
                        PortfolioID = PortfolioId,
                        CategoryID = UnitCategory,
                        PeriodType = SetExpiryPeriod,
                        Period = Period
                    };

                    obj.Incident_UnitExpiryPeriods.InsertOnSubmit(ue);
                    obj.SubmitChanges();
                }

                else 
                {
                    Incident_UnitExpiryPeriod ue = obj.Incident_UnitExpiryPeriods.Where(p => p.PortfolioID == PortfolioId && p.CategoryID == UnitCategory).First();
                    ue.PortfolioID =PortfolioId;
                    ue.CategoryID = UnitCategory;
                    ue.PeriodType = SetExpiryPeriod;
                    ue.Period = Period;
                    obj.SubmitChanges();
                }

                //obj.Incident_UnitExpiryPeriod_InsertUpdate(SdID, UnitCategory, SetExpiryPeriod, Period);

                LoadGrid();
                LoadUnitConsumptionGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvunitconsumption_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Delete2")
            {
                using (IncidentDataContext obj = new IncidentDataContext())
                {
                    lblerrormsg.Text = string.Empty;
                    string id = e.CommandArgument.ToString();

                    var UnitConsumptionDelete = from p in obj.Incident_UnitConsumptionConfigurations
                                                where p.ID == int.Parse(id)
                                                select p;
                    obj.Incident_UnitConsumptionConfigurations.DeleteAllOnSubmit(UnitConsumptionDelete);
                    obj.SubmitChanges();
                   
                }

               
                gvunitconsumption.EditIndex = -1;
                LoadGrid();
                LoadUnitConsumptionGrid();

            }

            if (e.CommandName == "Add")
            {
                int PortfolioId = sessionKeys.PortfolioID;
                string SRType = ((DropDownList)gvunitconsumption.FooterRow.FindControl("ddlSRType")).SelectedItem.Text.Trim();
                string TypeOfHours = ((TextBox)gvunitconsumption.FooterRow.FindControl("txtTypeOfHours")).Text.Trim();
                string Fromday = ((DropDownList)gvunitconsumption.FooterRow.FindControl("ddlFromDay")).SelectedItem.Text.Trim();
                string Today = ((DropDownList)gvunitconsumption.FooterRow.FindControl("ddlToDay")).SelectedItem.Text.Trim();
                TimeSpan FromTime = TimeSpan.Parse(((TextBox)gvunitconsumption.FooterRow.FindControl("txtFromTime")).Text.Trim());
                TimeSpan ToTime = TimeSpan.Parse(((TextBox)gvunitconsumption.FooterRow.FindControl("txtToTime")).Text.Trim());
                bool Includepublicholiday = ((CheckBox)gvunitconsumption.FooterRow.FindControl("chkIncludePublicHoliday")).Checked == true ? true : false;
                string Autounits = ((TextBox)gvunitconsumption.FooterRow.FindControl("txtAutoUnitsAllocated")).Text.Trim();
                decimal AutounitsAllocated = decimal.Parse (string.IsNullOrEmpty(Autounits) ? "0" : Autounits);
                string perunitoftime = ((DropDownList)gvunitconsumption.FooterRow.FindControl("ddlPerUnitOfTime")).SelectedItem.Text.Trim();

                if (Fromday != Today && FromTime!=ToTime)
                {
                    lblerrormsg.Text = string.Empty;
                    Incident_UnitConsumptionConfiguration iucc = new Incident_UnitConsumptionConfiguration
                    {
                        PortfolioID = PortfolioId,
                        SRType = SRType,
                        TypeOfHours = TypeOfHours,
                        FromDay = Fromday,
                        ToDay = Today,
                        FromTime = FromTime,
                        ToTime = ToTime,
                        AutoUnitsAllocated = AutounitsAllocated,
                        PerUnitOfTime = perunitoftime,
                        IncludePublicHoliday = Includepublicholiday

                    };

                    obj.Incident_UnitConsumptionConfigurations.InsertOnSubmit(iucc);
                    obj.SubmitChanges();
                    LoadGrid();
                    LoadUnitConsumptionGrid();
                }
                else
                {
                    lblerrormsg.Text = "Can't be add same From Day to same To Day or same From Time to same To Time  ";
                }
            }


           
            
        }
        
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void gvunitconsumption_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("lblUnitConsumption") != null)
                {
                    Label lblId = e.Row.FindControl("lblUnitConsumption") as Label;
                    if (lblId.Text == "0")
                    {
                        e.Row.Visible = false;
                    }
                }
            }
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnRAGAlertSave_Click(object sender, EventArgs e)
    {
        try
        {

            int PortfolioId = sessionKeys.PortfolioID;
            int RemainigUnits1 = Convert.ToInt32(string.IsNullOrEmpty(txtRemainingUnit1.Text.Trim()) ? "0" : txtRemainingUnit1.Text.Trim());
            int RemainigUnits2 = Convert.ToInt32(string.IsNullOrEmpty(txtRemainingUnit2.Text.Trim()) ? "0" : txtRemainingUnit2.Text.Trim());
            int RemainigUnits3 = Convert.ToInt32(string.IsNullOrEmpty(txtRemainingUnit3.Text.Trim()) ? "0" : txtRemainingUnit3.Text.Trim());
            string Email4 = txtEmail4.Text.Trim();
            string Email5 = txtEmail5.Text.Trim();
            string Email6 = txtEmail6.Text.Trim();

            var RAGResultGreen = (from p in obj.Incident_UnitConsumptionRAGAlerts
                                  where p.PortfolioID == PortfolioId && p.RAG == "Green"
                                  select p).Count();
            var RAGResultAmber = (from p in obj.Incident_UnitConsumptionRAGAlerts
                                  where p.PortfolioID == PortfolioId && p.RAG == "Amber"
                                  select p).Count();
            var RAGResultRed = (from p in obj.Incident_UnitConsumptionRAGAlerts
                                where p.PortfolioID == PortfolioId && p.RAG == "Red"
                                select p).Count();

            if (RAGResultGreen == 0)
            {


                Incident_UnitConsumptionRAGAlert iur = new Incident_UnitConsumptionRAGAlert
                {
                    PortfolioID = PortfolioId,
                    RemainingUnits = RemainigUnits1,
                    EmailDistribution = Email4,
                    RAG = "Green"
                };

                obj.Incident_UnitConsumptionRAGAlerts.InsertOnSubmit(iur);
                obj.SubmitChanges();

            }
            else
            {                       
                Incident_UnitConsumptionRAGAlert iucr = obj.Incident_UnitConsumptionRAGAlerts.Where(p => p.PortfolioID ==PortfolioId && p.RAG == "Green").First();
                iucr.RemainingUnits = RemainigUnits1;
                iucr.EmailDistribution = Email4;
                obj.SubmitChanges();

            }
            if (RAGResultAmber == 0)
            {


                Incident_UnitConsumptionRAGAlert iur = new Incident_UnitConsumptionRAGAlert
                {
                    PortfolioID = PortfolioId,
                    RemainingUnits = RemainigUnits2,
                    EmailDistribution = Email5,
                    RAG = "Amber"
                };

                obj.Incident_UnitConsumptionRAGAlerts.InsertOnSubmit(iur);
                obj.SubmitChanges();

            }
            else
            {
                Incident_UnitConsumptionRAGAlert iucr = obj.Incident_UnitConsumptionRAGAlerts.Where(p => p.PortfolioID == PortfolioId && p.RAG == "Amber").First();
                iucr.RemainingUnits = RemainigUnits2;
                iucr.EmailDistribution = Email5;
                obj.SubmitChanges();

            }
            if (RAGResultRed == 0)
            {


                Incident_UnitConsumptionRAGAlert iur = new Incident_UnitConsumptionRAGAlert
                {
                    PortfolioID = PortfolioId,
                    RemainingUnits = RemainigUnits3,
                    EmailDistribution = Email6,
                    RAG = "Red"
                };

                obj.Incident_UnitConsumptionRAGAlerts.InsertOnSubmit(iur);
                obj.SubmitChanges();

            }
            else
            {
                Incident_UnitConsumptionRAGAlert iucr = obj.Incident_UnitConsumptionRAGAlerts.Where(p => p.PortfolioID == PortfolioId && p.RAG == "Red").First();
                iucr.RemainingUnits = RemainigUnits3;
                iucr.EmailDistribution = Email6;
                obj.SubmitChanges();

            }

            LoadGrid();
            LoadUnitConsumptionGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnMinUnitsave_Click(object sender, EventArgs e)
    {
        try
        {
            int PortfolioId = sessionKeys.PortfolioID;
            int MinUnitsAssignedPerCall = Convert.ToInt32(string.IsNullOrEmpty(txtMinUnits.Text.Trim()) ? "0" : txtMinUnits.Text.Trim());

            var MinUnitResult = (from p in obj.Incident_MinimumUnitsAssignedPerCalls
                                 where p.PortfolioID == PortfolioId
                                 select p).Count();

            if (MinUnitResult == 0)
            {
                Incident_MinimumUnitsAssignedPerCall imu = new Incident_MinimumUnitsAssignedPerCall
                {
                    PortfolioID = PortfolioId,
                    MinimumUnitsAssignedPerCall = MinUnitsAssignedPerCall
                };
                obj.Incident_MinimumUnitsAssignedPerCalls.InsertOnSubmit(imu);
                obj.SubmitChanges();
            }
            else
            {
                Incident_MinimumUnitsAssignedPerCall imua = obj.Incident_MinimumUnitsAssignedPerCalls.Where(p => p.PortfolioID == PortfolioId).First();
                imua.MinimumUnitsAssignedPerCall = MinUnitsAssignedPerCall;
                obj.SubmitChanges();
            }

            LoadGrid();
            LoadUnitConsumptionGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
