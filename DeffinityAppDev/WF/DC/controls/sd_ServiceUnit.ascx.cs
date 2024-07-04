using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using Incidents.Entity;
using Incidents.StateManager;
using Deffinity.Bindings;
using IncidentMgt.DAL;
using IncidentMgt.Entity;
using System.Collections.Generic;
using AjaxControlToolkit;

using System.Data.Linq.SqlClient;

public partial class Servicedesk_sdcontrols_sd_ServiceUnit : System.Web.DynamicData.FieldTemplateUserControl {
    Incidents.Entity.Incident incident = new Incidents.Entity.Incident();
    IncidentDataContext obj = new IncidentDataContext();
    public string SectionName
    {
        set;
        get;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            int sdid=QueryStringValues.SDID;
            

            if (!IsPostBack)
            {


                if (SectionName == "FLS")
                {
                    lbltype.Text = "Type";
                    BindMasterCategory();
                    bindloads(incident.ProjectCategoryMasterID);
                    var ddlhoursresult = from p in obj.Incident_UnitConsumptionConfigurations
                                         where p.PortfolioID == sessionKeys.PortfolioID && p.SRType == "FLS"
                                         orderby p.FromTime
                                         select new { p.TypeOfHours };

                    ddlTypeOfHours.DataSource = ddlhoursresult;
                    ddlTypeOfHours.DataValueField = "TypeOfHours";
                    ddlTypeOfHours.DataTextField = "TypeOfHours";
                    ddlTypeOfHours.DataBind();
                    ddlTypeOfHours.Items.Insert(0, new ListItem("Please Select...", "0"));
                    ddlTypeOfHours.SelectedValue = AutoPageload();
                    ddlSRType.SelectedValue = "FLS";

                }
                else if (SectionName == "Change Control")
                {
                    //ddlSRType.SelectedValue = "Change Control";
                    lbltype.Text = "Type";
                    int PortfolioId = sessionKeys.PortfolioID;
                    //master category
                    BindMasterCategory();
                    //ddlmastercategory.SelectedIndex = ddlmastercategory.Items.IndexOf(ddlmastercategory.Items.FindByValue(incident.ProjectCategoryMasterID.ToString()));
                    //category dropdown

                    bindloads(incident.ProjectCategoryMasterID);
                    //ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(incident.ProjectCategoryID.ToString()));
                    //Type
                    string srtype = "Change Control";
                    ddlSRType.SelectedValue = "Change Control";
                    var ddlhoursresult = from p in obj.Incident_UnitConsumptionConfigurations
                                         where p.PortfolioID == PortfolioId && p.SRType == srtype
                                         orderby p.FromTime
                                         select new { p.TypeOfHours };

                    ddlTypeOfHours.DataSource = ddlhoursresult;

                    ddlTypeOfHours.DataValueField = "TypeOfHours";
                    ddlTypeOfHours.DataTextField = "TypeOfHours";
                    ddlTypeOfHours.DataBind();
                    ddlTypeOfHours.Items.Insert(0, new ListItem("Please Select...", "0"));
                    ddlTypeOfHours.SelectedValue = AutoPageload();

                }
                else
                {

                    if (IncidentState.IncidentSaver != null)
                    {
                        incident = IncidentState.IncidentSaver;
                        sessionKeys.PortfolioID = incident.PortfolioID;
                        sessionKeys.PortfolioName = incident.PortfolioName;
                        FillPageControls(incident);
                    }
                    else
                    {
                        BindMasterCategory();
                        bindloads(0);
                    }
                }
                LoadGrid();
               
                if ((AutoUnitAllocation() ==0))
                {
                    if ((AutoPageload() != "0"))
                    {
                       int portfolioId = sessionKeys.PortfolioID;
                       var minunitsapc = (from p in obj.Incident_MinimumUnitsAssignedPerCalls
                                         where p.PortfolioID == portfolioId
                                         select p.MinimumUnitsAssignedPerCall).FirstOrDefault();
                       if (minunitsapc == null)
                       {
                           txtQtyUsed.Text = "0";
                       }
                       else
                       {
                           txtQtyUsed.Text = minunitsapc.ToString();
                       }
                    }
                    else
                    {
                        txtQtyUsed.Text = "0";
                    }
                   
                }
                else
                {
                    txtQtyUsed.Text = AutoUnitAllocation().ToString();
                }
               
            }

        }
        catch (Exception ex)
        { 
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    public int GetType()
    {
        int sdid = 0;
        if (SectionName == "FLS")
            sdid = QueryStringValues.CallID;
        else if (SectionName == "Change Control")
            sdid = sessionKeys.ChangeControlID;
        else
            sdid = QueryStringValues.SDID;

        return sdid;
    }

    private void LoadGrid()
    {
        try
        {

            int sdid = GetType();

            //int SdID = QueryStringValues.SDID == 0 ? sessionKeys.ChangeControlID : QueryStringValues.SDID;
       

            //var result = obj.Incident_ServiceUnits.Where(p => p.SdID == SdID).ToList();
            var result = obj.Incident_ServiceUnits_SelectByType(sdid, SectionName).ToList();

            //List<Incident_ServiceUnits_SelectAllResult> result = obj.Incident_ServiceUnits_SelectAll(SdID).ToList();
            //var result = obj.Incident_ServiceUnits_SelectAll();
            GridView1.DataSource = result;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
        
 
    }

    private void FillPageControls(Incidents.Entity.Incident incident)
    {
        try
        {

           
            
                int PortfolioId = sessionKeys.PortfolioID;
                //master category
                BindMasterCategory();
                ddlmastercategory.SelectedIndex = ddlmastercategory.Items.IndexOf(ddlmastercategory.Items.FindByValue(incident.ProjectCategoryMasterID.ToString()));
                //category dropdown

                bindloads(incident.ProjectCategoryMasterID);
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(incident.ProjectCategoryID.ToString()));

                //SR Type
                ddlSRType.SelectedIndex = ddlSRType.Items.IndexOf(ddlSRType.Items.FindByValue(incident.IncidentType));

                //if (SectionName == "Change Control")
                //{
                //    string srtype = "Change Control";
                //}
                //else
                //{
                //    string srtype = incident.IncidentType;
                //}
                //string srtype = ddlSRType.SelectedItem.Text;
                string srtype = incident.IncidentType;


                var ddlhoursresult = from p in obj.Incident_UnitConsumptionConfigurations
                                     where p.PortfolioID == PortfolioId && p.SRType == srtype
                                     orderby p.FromTime
                                     select new { p.TypeOfHours };

                ddlTypeOfHours.DataSource = ddlhoursresult;

                ddlTypeOfHours.DataValueField = "TypeOfHours";
                ddlTypeOfHours.DataTextField = "TypeOfHours";
                ddlTypeOfHours.DataBind();
                ddlTypeOfHours.Items.Insert(0, new ListItem("Please Select...", "0"));
                ddlTypeOfHours.SelectedValue = AutoPageload();
               
          

            
            
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    private void BindMasterCategory()
    {
        try
        {
            ddlmastercategory.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.GetMasterCategory(sessionKeys.PortfolioID);
            ddlmastercategory.DataValueField = "ID";
            ddlmastercategory.DataTextField = "CategoryName";
            ddlmastercategory.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblerrormsg.Text = string.Empty;
            int masterid = Convert.ToInt32(ddlmastercategory.SelectedValue);
            bindloads(masterid);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    private void bindloads(int masterid)
    {
        try
        {
            ddlCategory.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.CategoryAssociatedToPortfolio(sessionKeys.PortfolioID, masterid);
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, Constants.ddlDefaultBind(true));
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //int Sdid =QueryStringValues.SDID == 0 ? sessionKeys.ChangeControlID: QueryStringValues.SDID;
            int sdid = GetType();

            
            var validationCount = (from p in obj.Incident_ServiceUnits
                                   where p.SRType == ddlSRType.SelectedItem.Text && p.TypeOfHours == ddlTypeOfHours.SelectedItem.Text
                                   && p.CategoryID == int.Parse(ddlmastercategory.SelectedValue) && p.SubCategoryID == int.Parse(ddlCategory.SelectedValue) &&
                                   p.QtyUsed == double.Parse(txtQtyUsed.Text) && p.SdID == sdid
                                   select p).Count();

            if (validationCount == 0)
            {

                lblerrormsg.Text = string.Empty;
                
                string CategoryID = ddlmastercategory.SelectedValue.ToString();
                string SubCategoryID = ddlCategory.SelectedValue;
                string QtyUsed = txtQtyUsed.Text.Trim();
                string Notes = txtNotes.Text.Trim();
                string Srtype = ddlSRType.SelectedItem.Text.Trim();
                string TypeOfhours = ddlTypeOfHours.SelectedItem.Text.Trim();

                int UID = sessionKeys.UID;

                Incident_ServiceUnit isu = new Incident_ServiceUnit
                {
                    SdID = sdid,
                    DateApplied = DateTime.Now,
                    CategoryID = int.Parse(CategoryID),
                    SubCategoryID = int.Parse(SubCategoryID),
                    QtyUsed = double.Parse(QtyUsed),
                    Notes = Notes,
                    AppliedBy = UID,
                    SRType = Srtype,
                    TypeOfHours = TypeOfhours,
                    SectionType=SectionName

                };
                obj.Incident_ServiceUnits.InsertOnSubmit(isu);
                obj.SubmitChanges();

                //obj.Incident_ServiceUnits_Insert(int.Parse(SdID), int.Parse(CategoryID), int.Parse(SubCategoryID), float.Parse(QtyUsed), Notes, UID);
                LoadGrid();
                txtNotes.Text = string.Empty;
                Unitconsumed(DateTime.Now, decimal.Parse(QtyUsed));
            }

            else
            {
                lblerrormsg.Text = "Please check the data already exists";
            }

         

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        LoadGrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        LoadGrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Incident_ServiceUnits_SelectByTypeResult de = (Incident_ServiceUnits_SelectByTypeResult)e.Row.DataItem;
                if (e.Row.FindControl("ddlmastercategory") != null)
                {
                    DropDownList ddlmaster = e.Row.FindControl("ddlmastercategory") as DropDownList;

                    ddlmaster.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.GetMasterCategory(sessionKeys.PortfolioID);
                    ddlmaster.DataValueField = "ID";
                    ddlmaster.DataTextField = "CategoryName";
                    ddlmaster.DataBind();
                    ddlmaster.SelectedValue = de.CategoryID.Value.ToString();

                   

                    CascadingDropDown casCadSubCattegory = (CascadingDropDown)e.Row.FindControl("casCadSubCattegory");
                    casCadSubCattegory.SelectedValue = de.SubCategoryID.Value.ToString();
                   
                }
                DropDownList ddlsrtype = e.Row.FindControl("ddlSrtype") as DropDownList;
                ddlsrtype.SelectedValue = de.SRType;

                CascadingDropDown casCadTypeOfHour = (CascadingDropDown)e.Row.FindControl("casCadTypeofhour");
                casCadTypeOfHour.SelectedValue = de.TypeOfHours;

                //if (e.Row.FindControl("ddlCategory") != null)
                //{
                //    DropDownList ddlSubCategory = e.Row.FindControl("ddlCategory") as DropDownList;
                //    string masterId = de.CategoryID.Value.ToString();
                //    ddlSubCategory.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.CategoryAssociatedToPortfolio(sessionKeys.PortfolioID, int.Parse(masterId));
                //    ddlSubCategory.DataValueField = "CategoryID";
                //    ddlSubCategory.DataTextField = "CategoryName";
                //    ddlSubCategory.DataBind();
                //    ddlSubCategory.SelectedValue = de.SubCategoryID.Value.ToString();

                //}


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    public void Unitconsumed(DateTime date, decimal qty)
    {
        int customerid = sessionKeys.PortfolioID;



        var result = (from p in obj.Incident_UnitsPurchaseHistories
                      where p.PortfolioID == customerid && p.DatePurchased <= date && p.ExpiryDate >= date
                      orderby p.UnitCategory, p.ExpiryDate
                      select p).ToList();

        var _result = (from p in obj.Incident_UnitsPurchaseHistories
                       where p.PortfolioID == customerid && p.DatePurchased <= date && p.ExpiryDate >= date && p.QtyUsed != null
                       orderby p.UnitCategory, p.ExpiryDate descending
                       select p).ToList();

        decimal extraqty = 0;
        decimal extra = 0;
        foreach (var item in result)
        {
            int purchaseID = item.ID;
            //DateTime Datepurchase = DateTime.Parse(item.DatePurchased.ToString());
            //DateTime dateexpiry = DateTime.Parse(item.ExpiryDate.ToString());
            int Unitcategory = (int)item.UnitCategory;
            decimal unitpurchased = (decimal)item.UnitsPurchased;
            decimal qtyused = item.QtyUsed == null ? 0 : (decimal)item.QtyUsed;

            if ((qtyused < unitpurchased) && (qtyused != unitpurchased))
            {
                decimal totalqty = qtyused + (qty - extraqty);

                if (totalqty <= unitpurchased)
                {

                    Incident_UnitsPurchaseHistory iup = obj.Incident_UnitsPurchaseHistories.Where(p => p.PortfolioID == customerid && p.ID == purchaseID).FirstOrDefault();
                    iup.QtyUsed = totalqty;
                    obj.SubmitChanges();
                    break;

                }

                else
                {
                    extra = extraqty;
                    extraqty = extra + (unitpurchased - qtyused);
                    //extra = extraqty;
                    Incident_UnitsPurchaseHistory iup = obj.Incident_UnitsPurchaseHistories.Where(p => p.PortfolioID == customerid && p.ID == purchaseID).FirstOrDefault();
                    iup.QtyUsed = unitpurchased;
                    obj.SubmitChanges();


                }

            }
            

        }

    }
    public void UnitConsumptionReverse(DateTime date, decimal qty)
    {
        int customerid = sessionKeys.PortfolioID;



        var result = (from p in obj.Incident_UnitsPurchaseHistories
                      where p.PortfolioID == customerid && p.DatePurchased <= date && p.ExpiryDate >= date && p.QtyUsed != null && p.QtyUsed != 0
                      orderby p.UnitCategory descending, p.ExpiryDate descending
                      select p).ToList();

        decimal extraqty = 0;
        decimal extra = 0;

        foreach (var item in result)
        {
            int purchaseID = item.ID;

            int Unitcategory = (int)item.UnitCategory;
            decimal unitpurchased = (decimal)item.UnitsPurchased;
            decimal qtyused = item.QtyUsed == null ? 0 : (decimal)item.QtyUsed;

            decimal totalqty = (qty - extraqty);

            if (qtyused >= totalqty)
            {
                Incident_UnitsPurchaseHistory iup = obj.Incident_UnitsPurchaseHistories.Where(p => p.PortfolioID == customerid && p.ID == purchaseID).FirstOrDefault();
                iup.QtyUsed = qtyused - totalqty;
                obj.SubmitChanges();
                break;
            }
            else
            {
                extra = extraqty;
                extraqty = extra + (qtyused);
                //extra = extraqty;
                Incident_UnitsPurchaseHistory iup = obj.Incident_UnitsPurchaseHistories.Where(p => p.PortfolioID == customerid && p.ID == purchaseID).FirstOrDefault();
                iup.QtyUsed = 0;
                obj.SubmitChanges();

            }

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update1")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                using (IncidentDataContext su = new IncidentDataContext())
                {
                    int i = GridView1.EditIndex;
                    GridViewRow Row = GridView1.Rows[i];


                    string date = ((Label)Row.FindControl("lbldateapplied")).Text;

                    string CategoryId = ((DropDownList)Row.FindControl("ddlmastercategory")).SelectedValue;
                    string SubcategoryId = ((DropDownList)Row.FindControl("ddlCategory")).SelectedValue;
                    string subCat = string.IsNullOrEmpty(SubcategoryId) ? "0" : SubcategoryId;

                    string QtyUsed = ((TextBox)Row.FindControl("txtQtyUsed")).Text;
                    string Notes = ((TextBox)Row.FindControl("txtNotes")).Text;
                    string Srtype = ((DropDownList)Row.FindControl("ddlSrtype")).SelectedValue;
                    string typeofhours = ((DropDownList)Row.FindControl("ddlTypeofhours")).SelectedValue;

                    Incident_ServiceUnit de = su.Incident_ServiceUnits.Where(p => p.ID == id).FirstOrDefault();

                    if (decimal.Parse(QtyUsed) >= (decimal)(de.QtyUsed))
                    {
                        Unitconsumed(DateTime.Parse(date), decimal.Parse(QtyUsed) - (decimal)(de.QtyUsed));
                    }
                    else
                    {
                        UnitConsumptionReverse(DateTime.Parse(date), (decimal)(de.QtyUsed) - decimal.Parse(QtyUsed));
                    }

                    de.CategoryID = int.Parse(CategoryId);

                    de.SubCategoryID = int.Parse(subCat);
                    de.Notes = Notes;
                    de.QtyUsed = double.Parse(QtyUsed);
                    de.SRType = Srtype;
                    de.TypeOfHours = typeofhours;
                    su.SubmitChanges();



                }

                GridView1.EditIndex = -1;
                LoadGrid();
            }
            if (e.CommandName == "Delete1")
            {
                using (IncidentDataContext obj = new IncidentDataContext())
                {
                    lblerrormsg.Text = string.Empty;
                    string id = e.CommandArgument.ToString();

                    var ServiceUnitDelete = from p in obj.Incident_ServiceUnits
                                            where p.ID == int.Parse(id)
                                            select p;
                    foreach (var item in ServiceUnitDelete)
                    {
                        UnitConsumptionReverse(DateTime.Parse(item.DateApplied.ToString()), (decimal)item.QtyUsed);
                    }

                    obj.Incident_ServiceUnits.DeleteAllOnSubmit(ServiceUnitDelete);
                    obj.SubmitChanges();


                }


                //obj.Incident_ServiceUnits_Delete(int.Parse(id));
                GridView1.EditIndex = -1;
                LoadGrid();

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }


    protected void ddlSRType_SelectedIndexChanged(object sender, EventArgs e)
    {

        int portfolioId = sessionKeys.PortfolioID;
        string srtype = ddlSRType.SelectedItem.Text;

        var ddlhoursresult = from p in obj.Incident_UnitConsumptionConfigurations
                             where p.PortfolioID == portfolioId && p.SRType == srtype orderby p.FromTime
                             select new { p.TypeOfHours };

        ddlTypeOfHours.DataSource = ddlhoursresult;
        ddlTypeOfHours.DataValueField = "TypeOfHours";
        ddlTypeOfHours.DataTextField = "TypeOfHours";
        ddlTypeOfHours.DataBind();
        ddlTypeOfHours.Items.Insert(0, new ListItem("Please Select...", "0"));
       
    }

    public string AutoPageload()   // Return Type of hours based on call log (Eg: Normal hours 07:00-15:00)
    {
        
        int sdid = QueryStringValues.SDID;
        int portfolioId = sessionKeys.PortfolioID;
        string srtype = ddlSRType.SelectedItem.Text;

        string typeofhour = "0";
        var dayofweek = (from p in obj.Incidents
                       where p.PortfolioID == portfolioId && p.ID == sdid
                       select p.DateLogged.Value.DayOfWeek).SingleOrDefault();
        var timeofday = (from p in obj.Incidents
                         where p.PortfolioID == portfolioId && p.ID == sdid
                         select p.DateLogged.Value.TimeOfDay).SingleOrDefault();

        var uc = (from p in obj.Incident_UnitConsumptionConfigurations
                 where p.PortfolioID == portfolioId && p.SRType==srtype
                 select p).ToList();

        foreach (var t in uc)
        {
            DayOfWeek FromDays = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.FromDay);
            DayOfWeek ToDays = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.ToDay);

            int FromdayOfWeek = (int)FromDays;
            int TodayOfWeek = (int)ToDays;



            TimeSpan FromTimes = TimeSpan.Parse(t.FromTime.ToString());
            TimeSpan ToTimes = TimeSpan.Parse(t.ToTime.ToString());


            TimeSpan ts = TimeSpan.Parse(timeofday.ToString());
            int xx = int.Parse(ts.TotalMinutes.ToString());




            int j = FromdayOfWeek;
            int K = TodayOfWeek;


            int x = Convert.ToInt32(FromTimes.TotalMinutes);
            int y = Convert.ToInt32(ToTimes.TotalMinutes);


            if ((FromTimes < ToTimes) && (FromdayOfWeek < TodayOfWeek))
            {
                if (((timeofday >= FromTimes) && (timeofday <= ToTimes)) && ((Convert.ToInt32(dayofweek) >= FromdayOfWeek) && (Convert.ToInt32(dayofweek) <= TodayOfWeek)))
                {
                    typeofhour = t.TypeOfHours;
                }

            }
            if ((FromTimes < ToTimes) && (FromdayOfWeek > TodayOfWeek))
            {
                if ((timeofday >= FromTimes) && (timeofday <= ToTimes))
                {
                    for (j = FromdayOfWeek; j <= 6; j++)
                    {
                        for (K = 0; K <= TodayOfWeek; K++)
                        {
                            if ((Convert.ToInt32(dayofweek) == j) || (Convert.ToInt32(dayofweek) == K))
                            {

                                typeofhour = t.TypeOfHours;

                            }
                        }
                    }

                }

            }
            if ((FromTimes > ToTimes) && (FromdayOfWeek < TodayOfWeek))
            {
                if ((Convert.ToInt32(timeofday) >= FromdayOfWeek) && (Convert.ToInt32(timeofday) <= TodayOfWeek))
                {
                    for (x = Convert.ToInt32(FromTimes.TotalMinutes); x <= 1440; x++)
                    {
                        for (y = 0; y <= Convert.ToInt32(ToTimes.TotalMinutes); y++)
                        {
                            if ((xx == x) || (xx == y))
                            {
                                typeofhour = t.TypeOfHours;
                               
                            }
                        }
                    }

                }
            }
            if ((FromTimes > ToTimes) && (FromdayOfWeek > TodayOfWeek))
            {

                for (j = FromdayOfWeek; j <= 6; j++)
                {
                    for (K = 0; K <= TodayOfWeek; K++)
                    {
                        for (x = Convert.ToInt32(FromTimes.TotalMinutes); x <= 1440; x++)
                        {
                            for (y = 0; y <= Convert.ToInt32(ToTimes.TotalMinutes); y++)
                            {
                                if (((Convert.ToInt32(dayofweek) == j) || (Convert.ToInt32(dayofweek) == K)) && ((xx == x) || (xx == y)))
                                {
                                    typeofhour = t.TypeOfHours;
                                    
                                }

                            }
                        }


                    }
                }
            }



        }
        return typeofhour;
    }


    public decimal AutoUnitAllocation() //Return Qty of Units (time diff form New to completion call log *  Auto units allocation in unitAdim ) Eg:60 min*10 Units return 600 units
    {
        int sdid = QueryStringValues.SDID;
        int portfolioId = sessionKeys.PortfolioID;
        decimal txt = 0;
        string srtype = ddlSRType.SelectedItem.Text;

        

         var test = (from p in obj.Incident_UnitConsumptionConfigurations
                     where p.PortfolioID == portfolioId && p.SRType == srtype && p.TypeOfHours == ddlTypeOfHours.SelectedItem.Text
                     select p).ToList();

        var result = (from p in obj.Incident_UnitConsumptionConfigurations
                      where p.PortfolioID == portfolioId && p.SRType==srtype && p.TypeOfHours == ddlTypeOfHours.SelectedItem.Text
                      select p.AutoUnitsAllocated).SingleOrDefault();

        foreach (var t in test)
        {
            DayOfWeek FromDays = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.FromDay);
            DayOfWeek ToDays = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.ToDay);

            int FromdayOfWeek = (int)FromDays;
            int TodayOfWeek = (int)ToDays;



            TimeSpan FromTimes = TimeSpan.Parse(t.FromTime.ToString());
            TimeSpan ToTimes = TimeSpan.Parse(t.ToTime.ToString());


            var tt = (from p in obj.Incidents
                      where p.PortfolioID == portfolioId && p.ID == sdid
                      select p.DateLogged.Value.DayOfWeek).SingleOrDefault(); //Get call log DayOfWeek

            var timeofdate = (from p in obj.Incidents
                              where p.PortfolioID == portfolioId && p.ID == sdid
                              select p.DateLogged.Value.TimeOfDay).SingleOrDefault();  //Get call log TimeOfDate
                    
                     TimeSpan ts = TimeSpan.Parse(timeofdate.ToString());
                     int xx = int.Parse(ts.TotalMinutes.ToString());  // Get call log total min 

           


                    int j = FromdayOfWeek;
                    int K = TodayOfWeek;


                    int x = Convert.ToInt32(FromTimes.TotalMinutes);
                    int y = Convert.ToInt32(ToTimes.TotalMinutes);
                   

                        var res = (from p in obj.Incidents
                                        where p.PortfolioID == portfolioId && p.ID == sdid
                                        select SqlMethods.DateDiffMinute(p.DateLogged, p.ClosedTime)).SingleOrDefault();
                   


                    //var res = timediff * result;
                    if ((FromTimes < ToTimes) && (FromdayOfWeek < TodayOfWeek))
                    {
                        if (((timeofdate >= FromTimes) && (timeofdate<=ToTimes)) && ((Convert.ToInt32(tt)>=FromdayOfWeek) &&(Convert.ToInt32(tt)<=TodayOfWeek))) 
                        {
                            if (t.PerUnitOfTime == "Minutes")
                            {
                                txt = Convert.ToInt32(res);
                            }
                            if (t.PerUnitOfTime == "Hours")
                            {
                                int min = Convert.ToInt32(res) / 60;
                                txt = min;
                            }
                            if (t.PerUnitOfTime == "Day")
                            {
                                int min = Convert.ToInt32(res) / 1440;
                                txt = min;
                            }
 
                        }
                    }
                    if ((FromTimes < ToTimes) && (FromdayOfWeek > TodayOfWeek))
                    {
                        if ((timeofdate >= FromTimes) && (timeofdate <= ToTimes))
                        {
                            for (j = FromdayOfWeek; j <= 6; j++)
                            {
                                for (K = 0; K <= TodayOfWeek; K++)
                                {
                                    if ((Convert.ToInt32(tt) == j) || (Convert.ToInt32(tt) == K))
                                    {
                                        if (t.PerUnitOfTime == "Minutes")
                                        {
                                            txt = Convert.ToInt32(res);
                                        }
                                        if (t.PerUnitOfTime == "Hours")
                                        {
                                            int min = Convert.ToInt32(res) / 60;
                                            txt = min;
                                        }
                                        if (t.PerUnitOfTime == "Day")
                                        {
                                            int min = Convert.ToInt32(res) / 1440;
                                            txt = min;
                                        }

                                    }
                                }
                            }

                        }

                    }
                    if ((FromTimes > ToTimes) && (FromdayOfWeek < TodayOfWeek))
                    {
                        if ((Convert.ToInt32(tt) >= FromdayOfWeek) && (Convert.ToInt32(tt) <= TodayOfWeek))
                        {
                            for (x = Convert.ToInt32(FromTimes.TotalMinutes); x <= 1440; x++)
                            {
                                for (y = 0; y <= Convert.ToInt32(ToTimes.TotalMinutes); y++)
                                {
                                    if ((xx == x) || (xx == y))
                                    {

                                        if (t.PerUnitOfTime == "Minutes")
                                        {
                                            txt = Convert.ToInt32(res);
                                        }
                                        if (t.PerUnitOfTime == "Hours")
                                        {
                                            int min = Convert.ToInt32(res) / 60;
                                            txt = min;
                                        }
                                        if (t.PerUnitOfTime == "Day")
                                        {
                                            int min = Convert.ToInt32(res) / 1440;
                                            txt = min;
                                        }
                                       
                                    }
                                }
                            }

                        }
                    }


                    if ((FromTimes > ToTimes) && (FromdayOfWeek > TodayOfWeek))
                    {
                        
                            for (j = FromdayOfWeek; j <= 6; j++)
                            {
                                for (K = 0; K <= TodayOfWeek; K++)
                                {
                                    for (x = Convert.ToInt32(FromTimes.TotalMinutes); x <= 1440; x++)
                                    {
                                        for (y = 0; y <= Convert.ToInt32(ToTimes.TotalMinutes); y++)
                                        {
                                            if (((Convert.ToInt32(tt) == j) || (Convert.ToInt32(tt) == K)) && ((xx == x) || (xx == y)))
                                            {

                                                if (t.PerUnitOfTime == "Minutes")
                                                {
                                                    txt = Convert.ToInt32(res);
                                                }
                                                if (t.PerUnitOfTime == "Hours")
                                                {
                                                    int min = Convert.ToInt32(res) / 60;
                                                    txt = min;
                                                }
                                                if (t.PerUnitOfTime == "Day")
                                                {
                                                    int min = Convert.ToInt32(res) / 1440;
                                                    txt = min;
                                                }
                                              
                                            }

                                        }
                                    }


                                }
                            }
                    }


            //    }
               
            //}
        }
        return txt* decimal.Parse(result.ToString()) ;
        
    }
    protected void ddlTypeOfHours_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var vv = AutoUnitAllocation().Split(',');

       

        if ((AutoUnitAllocation() == 0))
        {
            
            if ((AutoPageload() == ddlTypeOfHours.SelectedItem.Text))
            {
                int portfolioId = sessionKeys.PortfolioID;
                var minunitsapc = (from p in obj.Incident_MinimumUnitsAssignedPerCalls
                                   where p.PortfolioID == portfolioId
                                   select p.MinimumUnitsAssignedPerCall).FirstOrDefault();
                if (minunitsapc == null)
                {
                    txtQtyUsed.Text = "0";
                }
                else
                {
                    txtQtyUsed.Text = minunitsapc.ToString();
                }
            }
            else
            {
                txtQtyUsed.Text = "0";
            }

        }
        else
        {
            txtQtyUsed.Text = AutoUnitAllocation().ToString();
        }



    }

   

   
}
