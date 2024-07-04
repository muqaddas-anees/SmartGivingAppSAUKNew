using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UserMgt.DAL;
using UserMgt.Entity;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using Finance.DAL;
using Finance.Entity;

public partial class FMRatecard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Master.PageHead = "Finance Rate Card";
            if (!IsPostBack)
            {
                BindClassification();
                //BindResource();
                BindRateTyep();
                GrdClassBind();
                //GrdResBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindClassification()
    {
        FinanceModuleDataContext user = new FinanceModuleDataContext();
        try
        {
            var classification = from r in user.ExperienceClassifications
                            orderby r.ExpClassification
                            select r;
            ddlClass.DataSource = classification;
            ddlClass.DataTextField = "ExpClassification";
            ddlClass.DataValueField = "ID";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

  

    private void BindRateTyep()
    {
        try
        {
            TimeSheetDataContext Tmdata = new TimeSheetDataContext();
            var rateType = from r in Tmdata.TimesheetEntryTypes
                           orderby r.EntryType
                           select r;
            ddlRateType.DataSource = rateType;
            ddlRateType.DataTextField = "EntryType";
            ddlRateType.DataValueField = "ID";
            ddlRateType.DataBind();
            ddlRateType.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
            
            if (ddlClass.SelectedItem.Value.ToString() != "0")
            {
                if (ddlRateType.SelectedItem.Value.ToString() != "0")
                {
                    int ClassType = int.Parse(ddlClass.SelectedItem.Value.ToString());
                    int RateType = int.Parse(ddlRateType.SelectedItem.Value.ToString());
            
                    FinanceRateCard FMRcard = new FinanceRateCard();
                    var myRateCard = (from r in FMDatacntxt.FinanceRateCards
                                      where r.CardClassfication == ClassType && r.RateType == RateType
                                      select r).ToList();

                    if (myRateCard.Count > 0)
                                            
                    {
                        
                        FMRcard = FMDatacntxt.FinanceRateCards.Single(c => c.CardClassfication == ClassType && c.RateType == RateType);
                        FMRcard.ClassType = 1;
                        FMRcard.CardClassfication = Convert.ToInt32(ddlClass.SelectedItem.Value.ToString());
                        FMRcard.DailyRate = Convert.ToDouble(txtDailyR.Text.ToString());
                        FMRcard.HourlyRate = Convert.ToDouble(txtHourlyR.Text.ToString());
                        FMRcard.RateType = Convert.ToInt32(ddlRateType.SelectedItem.Value.ToString());
                        //FMDatacntxt.FinanceRateCards.InsertOnSubmit(FMRcard);
                        FMDatacntxt.SubmitChanges();
                        GrdClassBind();
                        txtDailyR.Text = string.Empty;
                        txtHourlyR.Text = string.Empty;
                        ddlClass.SelectedValue = "0";
                        ddlRateType.SelectedValue = "0";
                        
                    }
                    else
                    {
                        FMRcard.CardClassfication = Convert.ToInt32(ddlClass.SelectedItem.Value.ToString());
                        FMRcard.ClassType = 1;
                        FMRcard.RateType = Convert.ToInt32(ddlRateType.SelectedItem.Value.ToString());
                        FMRcard.HourlyRate = Convert.ToDouble(txtHourlyR.Text.ToString());
                        FMRcard.DailyRate = Convert.ToDouble(txtDailyR.Text.ToString());
                        FMDatacntxt.FinanceRateCards.InsertOnSubmit(FMRcard);
                        FMDatacntxt.SubmitChanges();
                        GrdClassBind();
                        txtDailyR.Text = string.Empty;
                        txtHourlyR.Text = string.Empty;
                        ddlClass.SelectedValue = "0";
                        ddlRateType.SelectedValue = "0";

                    }
                }
                else
                {
                    //Please select Rate Type
                    lblErr.Visible = true;
                    lblErr.Text = "Please select Rate Type";
                }
            }
            else
            {
                //lbl message please select Class Type
                lblErr.Visible = true;
                lblErr.Text = "Please select Classification Type";
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void GrdClassBind()
    {
        try
        {
            //
//select ID,(select ExpClassification from ExperienceClassification where ID =F.CardClassfication )Classification,
//(select EntryType from TimesheetEntryType where ID=F.RateType) RateType, DailyRate,HourlyRate
 //from Financeratecard F where ClassType =1
            FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
            TimeSheetDataContext TSDatacntxt = new TimeSheetDataContext();
            UserDataContext UserMangDatacntx = new UserDataContext ();
            var FM = (from r in FMDatacntxt.FinanceRateCards
                      where r.ClassType==1
                      select r).ToList();
            var TS = (from r in TSDatacntxt.TimesheetEntryTypes

                      select r).ToList();

            var US = (from r in FMDatacntxt.ExperienceClassifications

                      select r).ToList();
            var GrdData = (from F in FM
                           join T in TS on F.RateType equals T.ID
                           join S in US on F.CardClassfication equals S.ID
                           where F.ClassType == 1
                           select new
                           {
                               F.ID,
                               S.ExpClassification,
                               T.EntryType,
                               F.DailyRate,
                               F.HourlyRate
                               

                           }).ToList().Distinct();



            grdClassfication.DataSource = GrdData;
            grdClassfication.DataBind();


           

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

   
    protected void grdClassfication_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdClassfication.EditIndex = -1;
            GrdClassBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //grdClassfication_RowDeleting

    protected void grdClassfication_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
    protected void grdClassfication_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdClassfication.EditIndex = e.NewEditIndex;
            GrdClassBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void grdClassfication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdClassfication.EditIndex;
                FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
                FinanceRateCard FMRcard = new FinanceRateCard();


                GridViewRow Row = grdClassfication.Rows[i];
                //DropDownList ddlEClassification = (DropDownList)Row.FindControl("ddlEClassification");
                //    DropDownList ddlERateType = (DropDownList)Row.FindControl("ddlERateType");
                TextBox txtECDRate = (TextBox)Row.FindControl("txtECDRate");
                TextBox txtECHRate = (TextBox)Row.FindControl("txtECHRate");
                //var myRateCard = (from r in FMDatacntxt.FinanceRateCards
                //                  where r.ClassType == Convert.ToInt32(ddlEClassification.SelectedItem.Value.ToString()) &&
                //                 r.RateType == Convert.ToInt32(ddlERateType.SelectedItem.Value.ToString())
                //                  select r).ToList();
                var myRateCard = (from r in FMDatacntxt.FinanceRateCards
                                     where r.ID == ID
                                     select r).ToList();
                //int ClassType = Convert.ToInt32(ddlEClassification.SelectedItem.Value.ToString());
                //int RateType = Convert.ToInt32(ddlERateType.SelectedItem.Value.ToString());


                if (myRateCard.Count > 0)
                {
                    FMRcard = FMDatacntxt.FinanceRateCards.Single(c => c.ID == ID);
                    //FMRcard.CardClassfication = Convert.ToInt32(ddlEClassification.SelectedValue.ToString());
                    FMRcard.DailyRate = Convert.ToDouble(txtECDRate.Text.ToString());
                    FMRcard.HourlyRate = Convert.ToDouble(txtECHRate.Text.ToString());
                    //FMRcard.RateType = Convert.ToInt32(ddlERateType.SelectedItem.Value.ToString());
                    
                    //FMDatacntxt.FinanceRateCards.InsertOnSubmit(FMRcard);
                    FMDatacntxt.SubmitChanges();
                    
                    


                }
                else
                {
                    // need to disply that already exists
                    
                }


            }
            if (e.CommandName == "Delete")
            {
                FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
                FinanceRateCard FMRcard = new FinanceRateCard();
                FMRcard = FMDatacntxt.FinanceRateCards.Single(c => c.ID == Convert.ToInt32(e.CommandArgument.ToString()));
                FMDatacntxt.FinanceRateCards.DeleteOnSubmit(FMRcard);
                FMDatacntxt.SubmitChanges();
                GrdClassBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdClassfication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                //FinanceRateCard FM = (FinanceRateCard)e.Row.DataItem;
                FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
                FinanceRateCard FMRcard = new FinanceRateCard();

                //if ((e.Row.FindControl("ddlEClassification") != null) && (e.Row.FindControl("ddlERateType")!= null))
                //{
                //    DropDownList ddlEClassification = (DropDownList)e.Row.FindControl("ddlEClassification");
                //    DropDownList ddlERateType = (DropDownList)e.Row.FindControl("ddlERateType");
                //    Label lblID = (Label)e.Row.FindControl("lblID");
                //    //e.Row.ID.ToString();
                //    //DataRowView myrow1 = (DataRowView)e.Row.DataItem;
                //    //DataRow myrow = (DataRow)e.Row.DataItem;
                //    Label classif = (Label)e.Row.FindControl("lblClassification");
                //    FMRcard = FMDatacntxt.FinanceRateCards.Single(c => c.ID == int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString()));
                //    UserDataContext user = new UserDataContext();

                //    var classification = from r in user.ExperienceClassifications
                //                         orderby r.ExpClassification
                //                         select r;
                //    ddlEClassification.DataSource = classification;
                //    ddlEClassification.DataTextField = "ExpClassification";
                //    ddlEClassification.DataValueField = "ID";
                //    ddlEClassification.DataBind();
                //    ddlEClassification.Items.Insert(0, new ListItem("Please select...", "0"));

                //    ddlEClassification.SelectedValue = FMRcard.CardClassfication.ToString();//DataBinder.Eval(e.Row.DataItem, "ExpClassification").ToString();//myrow1["ExpClassification"].ToString();
                //    TimeSheetDataContext Tmdata = new TimeSheetDataContext();
                //    var rateType = from r in Tmdata.TimesheetEntryTypes
                //                   orderby r.EntryType
                //                   select r;
                //    ddlERateType.DataSource = rateType;
                //    ddlERateType.DataTextField = "EntryType";
                //    ddlERateType.DataValueField = "ID";
                //    ddlERateType.DataBind();
                //    ddlERateType.Items.Insert(0, new ListItem("Please select...", "0"));
                //    ddlERateType.SelectedValue = FMRcard.RateType.ToString();

                    
                //}
               
               

        
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdClassfication_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            grdClassfication.EditIndex = -1;
            GrdClassBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdClassfication_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdClassfication.PageIndex = e.NewPageIndex;
            GrdClassBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnaddclassifi_Click(object sender, EventArgs e)
    {
        try
        {
            UserDataContext usr = new UserDataContext();
            //Contractor ctr = new Contractor();
            UserMgt.Entity.Contractor ctr = new UserMgt.Entity.Contractor();
            ctr = usr.Contractors.Single(u => u.ID == sessionKeys.UID);

            if (ctr.SID == 1)
            {
               
                PanelVisibilty(false, true);
            }
            else
            {
                lblErr.Visible = true;
                lblErr.Text = "Only admin can create Classification";
                lblErr.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    protected void btnSaveClassi_Click(object sender, EventArgs e)
    {
        try
        {
            FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
            if (txtAddClassi.Text.ToString() != string.Empty)
            {
                var myClassifi = (from c in FMDatacntxt.ExperienceClassifications
                                  where c.ExpClassification == txtAddClassi.Text.Trim()
                                  select c).ToList();
                if (myClassifi.Count > 0)
                {
                    lblErr.Visible = true;
                    lblErr.Text = "Classification already exists"; 
                }
                else
                {
                    ExperienceClassification ExpClass = new ExperienceClassification();
                    ExpClass.ExpClassification = txtAddClassi.Text.Trim();
                    FMDatacntxt.ExperienceClassifications.InsertOnSubmit(ExpClass);
                    FMDatacntxt.SubmitChanges();
                    BindClassification();
                    PanelVisibilty(true, false);

                }
                               
            }
            else
            {
                lblErr.Visible = true;
                lblErr.Text = "Please enter Classification"; 
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void PanelVisibilty(bool pnlCls, bool pnlAddcls)
    {
        try
        {
            pnlClassfi.Visible = pnlCls;
            pnladdclassfi.Visible = pnlAddcls;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCancelClassi_Click(object sender, EventArgs e)
    {
        try
        {
            PanelVisibilty(true, false);
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

   
   
}
