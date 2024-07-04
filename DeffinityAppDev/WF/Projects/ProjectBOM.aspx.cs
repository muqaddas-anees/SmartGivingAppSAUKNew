using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using POMgt.DAL;
using POMgt.Entity;
//using PortfolioMgt.DAL;
//using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.SqlClient;
using Deffinity.ServiceCatalogManager;
using Microsoft.ApplicationBlocks.Data;
using Quote.BLL;
using UserMgt.DAL;
using UserMgt.Entity;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using Finance.DAL;
using Finance.Entity;
using Deffinity.ProgrammeManagers;
using ProjectMgt.BLL;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;
public partial class ProjectBOM :BasePage
{
    string AllIds, AllTypes;
    string AllIdsG, AllTypesG;
    string IDs, Types;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Snapshot
                BindSnapsDll(QueryStringValues.Project);
                BindWorksheetDll();

                Bind_worksheet();
                HD_ServiceType1.Value = "3";
                if (Request.QueryString["project"] != null)
                {
                    CheckUserRole();
                    EnableButton();
                    //lnkRpt.NavigateUrl = "~/Reports/ProjectBOMSupplierInv.aspx?project=" +QueryStringValues.Project.ToString();
                    //txtModelWorksheet.Text = "";
                    //txtModelWorksheet.Focus();
                    lblError.Visible = false;
                    lblMsg.Visible = false;
                    //Master.PageHead = "Financial Section";//"Project Management";
                    Bind_worksheet();
                    SetWorksheet();
                    Bind_Savedworksheet();

                    BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                    BindSummary(int.Parse(ddlWorksheet.SelectedValue));
                    //report navigation set
                  
                    //Live condition checking
                    //using (projectTaskDataContext pdc = new projectTaskDataContext())
                    //{
                    //    if (pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.ProjectStatusID).FirstOrDefault() == 2)
                    //    {
                    //        ChangeControlStatus();
                    //        EnableSavingBtn();
                    //    }
                    //}


                }
                BindClassification(ddlClass);
                BindResource(ddlResource);
                int classiType1 = Convert.ToInt32(ddlClass.SelectedItem.Value.ToString());
                BindGrdViewRate(grdRatecard, classiType1);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void EnableSavingBtn()
    {
        try
        {
            //btnBack.Enabled = true;
            gridBOM.Enabled = true;
            if (gridBOM.Rows.Count > 1)
            {
                for (int i = 0; i < gridBOM.Rows.Count; i++)
                {
                    GridViewRow grow = gridBOM.Rows[i];
                    LinkButton Imgbtn = (LinkButton)grow.FindControl("ImgSave");
                    Imgbtn.Enabled = true;
                    HyperLink h1 = (HyperLink)grow.FindControl("hyplink1");
                    HyperLink h2 = (HyperLink)grow.FindControl("hyplink2");
                    HyperLink h3 = (HyperLink)grow.FindControl("hyplink3");
                    h1.Enabled = false;
                    h2.Enabled = false;
                    h3.Enabled = false;

                    LinkButton ImgButtonEdit = (LinkButton)grow.FindControl("LinkButtonEdit");
                    ImgButtonEdit.Enabled = false;
                    CheckBox GridCheckBox = (CheckBox)grow.FindControl("chkbox");
                    GridCheckBox.Enabled = false;

                    TextBox GridtxtGP = (TextBox)grow.FindControl("txtGP");
                    LinkButton GridImgbtn = (LinkButton)grow.FindControl("imgGP");
                    GridImgbtn.Enabled = false;
                    GridtxtGP.Enabled = false;

                    LinkButton GridImageButton1 = (LinkButton)grow.FindControl("ImageButton1");
                    GridImageButton1.Enabled = false;
                }
            }
            LinkButton FooterImgButtonEdit = gridBOM.FooterRow.FindControl("LinkButtonInsert") as LinkButton;
            TextBox Footertdes = gridBOM.FooterRow.FindControl("txtfoo_service") as TextBox;
            TextBox FootertxtPartNumberf = gridBOM.FooterRow.FindControl("txtPartNumberf") as TextBox;
            TextBox FootertxtUnitf = gridBOM.FooterRow.FindControl("txtUnitf") as TextBox;
            TextBox FootertxtQtyf = gridBOM.FooterRow.FindControl("txtQtyf") as TextBox;
            TextBox FootertxtMaterialf = gridBOM.FooterRow.FindControl("txtMaterialf") as TextBox;
            TextBox FootertxtLabourf = gridBOM.FooterRow.FindControl("txtLabourf") as TextBox;
            TextBox FootertxtMiscf = gridBOM.FooterRow.FindControl("txtMiscf") as TextBox;
            TextBox FootertxtSelling = gridBOM.FooterRow.FindControl("txtSelling") as TextBox;
            LinkButton FooterbtnPopUp = gridBOM.FooterRow.FindControl("btnPopUp") as LinkButton;
            LinkButton FooterbtnPopUpRate = gridBOM.FooterRow.FindControl("btnPopUpRate") as LinkButton;


            DropDownList FooterddlSupplierf = gridBOM.FooterRow.FindControl("ddlSupplierf") as DropDownList;
            DropDownList FooterddlManufacturer = gridBOM.FooterRow.FindControl("ddlManufacturerf") as DropDownList;
            DropDownList FooterddlCurrency_f = gridBOM.FooterRow.FindControl("ddlCurrency_f") as DropDownList;

            FooterImgButtonEdit.Enabled = false;
            Footertdes.Enabled = false;

            FootertxtPartNumberf.Enabled = false;
            FootertxtUnitf.Enabled = false;
            FootertxtQtyf.Enabled = false;
            FootertxtMaterialf.Enabled = false;
            FootertxtLabourf.Enabled = false;
            FootertxtMiscf.Enabled = false;
            FootertxtSelling.Enabled = false;
            FooterbtnPopUp.Enabled = false;
            FooterbtnPopUpRate.Enabled = false;
            FooterddlSupplierf.Enabled = false;
            FooterddlManufacturer.Enabled = false;
            FooterddlCurrency_f.Enabled = false;

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
       
    }
    public void ControlDisable(ControlCollection ptext, bool status)
    {
        try
        {
            foreach (Control ctrl in ptext)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Enabled = status;
                }
                else if (ctrl is Button)
                {
                    ((Button)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBoxList)
                {
                    ((CheckBoxList)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).Enabled = status;
                }
                else if (ctrl is Image)
                {
                    ((Image)ctrl).Enabled = status;
                }
                else if (ctrl is System.Web.UI.WebControls.Calendar)
                {
                    ((System.Web.UI.WebControls.Calendar)ctrl).Enabled = status;
                }
                else if (ctrl is ImageButton)
                {
                    ((ImageButton)ctrl).Enabled = status;
                }
                else if (ctrl is LinkButton)
                {
                    if (ctrl.ID != "BtnSnapShot" && ctrl.ID != "lnkViewSnapshots")
                    {
                        ((LinkButton)ctrl).Enabled = status;
                    }
                }
                else if (ctrl is DropDownList)
                {
                    if (ctrl.ID != "ddlWorksheet")
                    {
                        ((DropDownList)ctrl).Enabled = status;
                    }
                }
                else if (ctrl is HyperLink)
                {
                    ((HyperLink)ctrl).Enabled = status;
                }
                else if (ctrl is Label)
                {
                    ((Label)ctrl).Enabled = status;
                }
                else if (ctrl is GridView)
                {
                    ((GridView)ctrl).Enabled = status;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ChangeControlStatus()
    {
        var cl = this.Form.Controls;
        var M1Content = this.Form.FindControl("MainContent") as ContentPlaceHolder;
        if (M1Content != null)
        {
            var M2Content = M1Content.FindControl("MainContent") as ContentPlaceHolder;

            if (M2Content != null)
            {
                var Ccntrl = M2Content.Controls;
                ControlDisable(Ccntrl, false);
            }

            var M3Content = M2Content.FindControl("ProjectCostCntl") as UserControl;
            if (M3Content != null)
            {
                var Ccntrl1 = M3Content.Controls;
                ControlDisable(Ccntrl1, false);
            }
        }
    }
    #region worksheet
    private void Bind_worksheet()
    {
        ddlWorksheet.DataSource = Deffinity.Worksheet.Worksheet_SelectAll(QueryStringValues.Project);
        ddlWorksheet.DataTextField = "TypeName";
        ddlWorksheet.DataValueField = "ID";
        ddlWorksheet.DataBind();
        ddlWorksheet.Items.Insert(0, new ListItem("Please select...", "0"));

    }
    private void Bind_Savedworksheet()
    {
        projectTaskDataContext db = new projectTaskDataContext();
        var workSheets = (from r in db.BOM_TypesDUPs
                          //where r.ProjectReference == QueryStringValues.Project
                          select r).ToList();

        ddlSavesWorkSheets.DataSource = workSheets;
        ddlSavesWorkSheets.DataTextField = "WorksheetNameDUP";
        ddlSavesWorkSheets.DataValueField = "ID";
        ddlSavesWorkSheets.DataBind();
        ddlSavesWorkSheets.Items.Insert(0, new ListItem("Please select...", "0"));

    }

    private void SetWorksheet()
    {
        try
        {
            if (ddlWorksheet.Items.Count > 1)
            {
                //set the starting value
                ddlWorksheet.SelectedIndex = 1;
                HD_Type.Value = ddlWorksheet.SelectedValue;

               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnAdd_worksheet_click(object sender, EventArgs e)
    {

        txtModelWorksheet.Text = "";
        txtModelWorksheet.Focus();
        H_Worksheet.Value = "0";
        mdlWorksheet.Show();
    }
    protected void btnEdit_worksheet_click(object sender, EventArgs e)
    {
        if (int.Parse(ddlWorksheet.SelectedValue) > 0)
        {
            mdlWorksheet.Show();
            H_Worksheet.Value = ddlWorksheet.SelectedItem.Value;
            txtModelWorksheet.Text = ddlWorksheet.SelectedItem.Text;
            BindGrid(int.Parse(H_Worksheet.Value));
            BindSummary(int.Parse(H_Worksheet.Value));
        }

    }
    protected void btnDel_worksheet_click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlWorksheet.SelectedValue) > 0)
            {
                using (projectTaskDataContext pdc = new projectTaskDataContext())
                {
                    BOM_Type BOM = (from a in pdc.BOM_Types where a.ID == int.Parse(ddlWorksheet.SelectedValue) select a).FirstOrDefault();
                    BOM.IsDeleted = true;
                    pdc.SubmitChanges();
                    BOMDeletedWorksheet BOMdelete = new BOMDeletedWorksheet();
                    BOMdelete.deleteedBy = sessionKeys.UID;
                    BOMdelete.DeleteOn = DateTime.Now;
                    BOMdelete.Wid = int.Parse(ddlWorksheet.SelectedValue);
                    pdc.BOMDeletedWorksheets.InsertOnSubmit(BOMdelete);
                    pdc.SubmitChanges();
                }
                 //   bool Status = Deffinity.Worksheet.Worksheet_Delete(int.Parse(ddlWorksheet.SelectedValue));
                bool Status = true;
                    if (Status == false)
                    {
                        lblError.Text = "Unable delete worksheet.";
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                     
                        //bind ofter delete work sheet
                        // BindAllGridViews();
                        //Bind_worksheet();
                        //SetWorksheet();
                        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                        BindSummary(int.Parse(ddlWorksheet.SelectedValue));
                      //  linkRpt.NavigateUrl = "~/Reports/ProjectBOMprint.aspx?id=" + HD_Type.Value + "&project=" + QueryStringValues.Project;
                    }
                }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void imgBtnWorksheet_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtModelWorksheet.Text.Trim()))
            {
                int id = 0;
                if (!string.IsNullOrEmpty(H_Worksheet.Value))
                {
                    id = int.Parse(H_Worksheet.Value);
                }

                int returnID = Deffinity.Worksheet.Worksheet_InsertUpdate(id, QueryStringValues.Project, txtModelWorksheet.Text.Trim());
                txtModelWorksheet.Text = string.Empty;
                H_Worksheet.Value = "0";
                //bind worksheet dropdown
                Bind_worksheet();
                ddlWorksheet.SelectedValue = returnID.ToString();
                HD_Type.Value = returnID.ToString();
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
                //if (id == 0)
                //{
                //    BindAllGridViews();
                //}
                txtModelWorksheet.Text = "";
                txtModelWorksheet.Focus();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ddlWorksheet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            HD_Type.Value = ddlWorksheet.SelectedValue;
           // linkRpt.NavigateUrl = "~/Reports/ProjectBOMprint.aspx?id=" + ddlWorksheet.SelectedValue + "&project=" + QueryStringValues.Project;
            GetSeletedIds();
            //GetSeletedIdsG();
            BindGrid(int.Parse(ddlWorksheet.SelectedValue));
            BindSummary(int.Parse(ddlWorksheet.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion

    #region Grid
   

    private void BindGrid(int worksheetID)
    {
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var bomList = (from r in BOM.ProjectBOMDetils
                           where (r.ProjectReference == QueryStringValues.Project &&
                           r.WorkSheetID == worksheetID)
                           || r.ID==-99
                           
                           select r).ToList();
            
            gridBOM.DataSource = bomList;
            gridBOM.DataBind();
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                if (pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.ProjectStatusID).FirstOrDefault() == 1)
                {
                    gridBOM.Columns[2].Visible = false;
                }
            }	

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void DisableTotalControlsInPage()
    {
        try
        {
            foreach (Control c in this.Page.Controls)
            {
                if (c is TextBox)
                    ((TextBox)(c)).Enabled = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindVendors(DropDownList ddlVendor, int setvalue)
    {


        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

            var vendorsList = from r in Vendors.v_Vendors

                              orderby r.ContractorName
                              select r;
            ddlVendor.DataSource = vendorsList;
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "ContractorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlVendor.SelectedValue = setvalue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindManufacturer(DropDownList ddlManufacturer, int setValue)
    {
        try
        {
            ddlManufacturer.DataSource = ManufacturerBAL.GetManufacturerList();
            ddlManufacturer.DataValueField = "Id";
            ddlManufacturer.DataTextField = "Name";
            ddlManufacturer.DataBind();
            ddlManufacturer.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlManufacturer.SelectedValue = setValue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion 

    
    protected void gridBOM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProjectBOMDetils de = (ProjectBOMDetils)e.Row.DataItem;
                if (de.ID == -99)
                {
                    e.Row.Visible = false;
                }

                DropDownList ddlSupplier=(DropDownList)e.Row.FindControl("ddlSupplier");
                DropDownList ddlManufacturer = (DropDownList)e.Row.FindControl("ddlManufacturer");
                if (ddlSupplier != null)
                {
                    BindVendors(ddlSupplier, de.Supplier.Value);
                }
                if (ddlManufacturer != null)
                {
                    BindManufacturer(ddlManufacturer, de.ManufactureID);
                }
                gridBOM.HeaderRow.Cells[0].ToolTip = "Goods received status";



               
                
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlSupplier = (DropDownList)e.Row.FindControl("ddlSupplierf");
                DropDownList ddlManufacturer = (DropDownList)e.Row.FindControl("ddlManufacturerf");
                if (ddlSupplier != null)
                {
                    BindVendors(ddlSupplier,0);
                }

                if (ddlManufacturer != null)
                {
                    BindManufacturer(ddlManufacturer, 0);
                }
                DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClass");
                if (ddlClass != null)
                
                {
                    BindClassification(ddlClass);
                }

                DropDownList ddlResource = (DropDownList)e.Row.FindControl("ddlResource");
                if (ddlResource != null)
                {
                    BindResource(ddlResource);
                }
                if ((ddlClass != null) && (ddlResource != null))
                {
                    int classiType = Convert.ToInt32(ddlClass.SelectedItem.Value.ToString());
                    GridView grdRatecard = (GridView)e.Row.FindControl("grdRatecard");
                    if (grdRatecard != null)
                    {
                        BindGrdViewRate(grdRatecard, classiType);
                    }
                }

                DropDownList ddlcurrencey = (DropDownList)e.Row.FindControl("ddlCurrency_f");
                if (ddlcurrencey != null)
                {
                    projectTaskDataContext curr = new projectTaskDataContext();
                    var currencey = (from r in curr.ProjectDefaults
                                     select r).ToList().FirstOrDefault();
                    if(currencey!=null)
                    {

                        BindCurrencey(ddlcurrencey, currencey.DefaultCurrency.Value);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  
    protected bool SetImageGray(string ID)
    {
        bool visible = false;
        projectTaskDataContext BOM = new projectTaskDataContext();
        var getList = (from r in BOM.GoodsReceipts
                         where r.BOMID == int.Parse(ID)
                         select r).ToList();

        if (getList.Count != 0)
        {
            var getValues = (from r in BOM.GoodsReceipts
                             where r.BOMID == int.Parse(ID)
                             select r).ToList().FirstOrDefault();
            if (getValues != null)
            {
                if (getValues.QtyOrdered!= 0 && getValues.QtyReceived==0)
                {
                    visible = true;
                }
                if (getValues.QtyOrdered == 0 && getValues.QtyReceived == 0)
                {
                    visible = true;
                }
            }
        }
        else
        {
            visible = true;
        }
        return visible;
    }

    protected string GetPurchasedQty(string ID)
    {
        string Qty = "0";
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var getList = (from r in BOM.GoodsReceipts
                           where r.BOMID == int.Parse(ID)
                           select r).ToList();

            var bomList = (from r in BOM.ProjectBOMDetils
                           where (r.ProjectReference == QueryStringValues.Project &&
                           r.WorkSheetID==int.Parse(ddlWorksheet.SelectedValue))
                           || r.ID==-99
                           select r).ToList();

            if (getList.Count != 0)
            {
                var getValues = (from r in BOM.GoodsReceipts
                                 where r.BOMID == int.Parse(ID)
                                 select r).ToList().FirstOrDefault();
                if (getValues != null)
                {
                    Qty = getValues.QtyReceived.ToString() ;
                   
                }
            }
            else
            {
                //visible = true;
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Qty;

    }

    protected bool SetImageGreen(string ID)
    {
        bool visible = false;
        projectTaskDataContext BOM = new projectTaskDataContext();
        var getValues = (from r in BOM.GoodsReceipts
                         where r.BOMID == int.Parse(ID)
                         select r).ToList().FirstOrDefault();
        if (getValues != null)
        {
            if (getValues.OutStandingQty == 0)
            {
                visible = true;
            }
        }
        return visible;
    }
    protected bool SavingImageVisibility(string ID)
    {
        bool visible = true;
        projectTaskDataContext BOM = new projectTaskDataContext();
        var getValues = (from r in BOM.GoodsReceipts
                         where r.BOMID == int.Parse(ID)
                         select r).ToList().FirstOrDefault();
        if (getValues != null)
        {
            if (getValues.OutStandingQty == 0)
            {
                visible = false;
            }
        }
        return visible;
    }
    protected bool SetImageAmber(string ID)
    {
        bool visible = false;
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var getValues = (from r in BOM.GoodsReceipts
                             where r.BOMID == int.Parse(ID)
                             select r).ToList().FirstOrDefault();
            if (getValues != null)
            {
                if (getValues.OutStandingQty != 0 && getValues.QtyReceived != 0)
                {
                    visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return visible;
    }
    public string GetUrl(string id)
    {
        string url = string.Empty;
        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            var bom = db.ProjectBOMDetils.Where(p => p.ID == int.Parse(id)).FirstOrDefault();
            if (bom != null)
            {
                if (bom.Labour != 0)
                {
                    url = "ProjectLabourTracker.aspx?project=" + QueryStringValues.Project;
                }
                else if (bom.Material != 0)
                {
                    url = "ProjectMaterialTracker.aspx?project=" + QueryStringValues.Project;
                }
                else if (bom.Mics!= 0)
                {
                    url = "ProjectMiscTracker.aspx?project=" + QueryStringValues.Project;
                }

            }

            return url;
        }

    }
    protected void gridBOM_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            projectTaskDataContext InsertBOM = new projectTaskDataContext();
            var currencey = (from r in InsertBOM.ProjectDefaults
                             select r).ToList().FirstOrDefault();
            int Defaultcurrence = 0;
            if (currencey != null)
            {
                Defaultcurrence = currencey.DefaultCurrency.Value;
            }
            if (e.CommandName == "Edit1")
            {
                //int i = gridBOM.EditIndex;
                //GridViewRow row = gridBOM.Rows[i];
                string[] QtyArray = e.CommandArgument.ToString().Split('/');
                lblProjectBOMId.Text = QtyArray[2].ToString();
                lblProjectBOMId.Visible = false;
                // string lblqty = ((Label)row.FindControl("lblPurchased")).Text;
                InsertBOM = new projectTaskDataContext();
                var BOMSavingrecord = InsertBOM.GoodsReceiptSavings.Where(a => a.BOMId == int.Parse(QtyArray[2].ToString()) && a.S_type == "Qty").FirstOrDefault();
                if (BOMSavingrecord != null)
                {
                    txtActualReq.Text = BOMSavingrecord.ActualQty.Value.ToString();
                }
                else
                {
                    txtActualReq.Text = string.Empty;
                }
                txtbudgetQty.Text = QtyArray[1].ToString();
                lblQtyReceivedtoDate.Text = QtyArray[0].ToString();

                RangeValidator2.MinimumValue = QtyArray[0].ToString();
                RangeValidator2.MaximumValue = QtyArray[1].ToString(); //(int.Parse(QtyArray[1].ToString()) - int.Parse(QtyArray[0].ToString())).ToString();
                mdlpopupinGridToSave.Show();

            }
            if (e.CommandName == "Insert")
            {
                if (ddlWorksheet.SelectedValue != "0")
                {


                    TextBox txtfoo_service = (TextBox)gridBOM.FooterRow.FindControl("txtfoo_service");
                    TextBox txtPartNumberf = (TextBox)gridBOM.FooterRow.FindControl("txtPartNumberf");
                    DropDownList ddlSupplierf = (DropDownList)gridBOM.FooterRow.FindControl("ddlSupplierf");
                    DropDownList ddlManufacturerf = (DropDownList)gridBOM.FooterRow.FindControl("ddlManufacturerf");
                    TextBox txtUnitf = (TextBox)gridBOM.FooterRow.FindControl("txtUnitf");
                    TextBox txtQtyf = (TextBox)gridBOM.FooterRow.FindControl("txtQtyf");
                    TextBox txtMaterialf = (TextBox)gridBOM.FooterRow.FindControl("txtMaterialf");
                    TextBox txtLabourf = (TextBox)gridBOM.FooterRow.FindControl("txtLabourf");
                    TextBox txtMiscf = (TextBox)gridBOM.FooterRow.FindControl("txtMiscf");
                    TextBox txtSelling = (TextBox)gridBOM.FooterRow.FindControl("txtSelling");
                    DropDownList ddlCurrency_f = (DropDownList)gridBOM.FooterRow.FindControl("ddlCurrency_f");
                    
                    //TextBox txtSellingPrice = (TextBox)gridBOM.FooterRow.FindControl("txtSellingPrice");

                    ProjectMgt.Entity.ProjectBOM add = new ProjectMgt.Entity.ProjectBOM();
                    add.Description =string.IsNullOrEmpty(txtfoo_service.Text)?"0":txtfoo_service.Text;
                    add.PartNumber = string.IsNullOrEmpty(txtPartNumberf.Text)?" ":txtPartNumberf.Text;
                    add.ProjectReference = QueryStringValues.Project;
                    add.Supplier = int.Parse(string.IsNullOrEmpty(ddlSupplierf.SelectedValue) ? "0" : ddlSupplierf.SelectedValue);
                    add.ManufactureID = int.Parse(ddlManufacturerf.SelectedValue);
                    //add.Unit = Convert.ToDouble(txtUnitf.Text);
                    add.Unit = txtUnitf.Text;
                    add.WorkSheetID = int.Parse(ddlWorksheet.SelectedValue);
                    add.Qty = Convert.ToDouble(string.IsNullOrEmpty(txtQtyf.Text)?"0":txtQtyf.Text);
                    add.Material = Convert.ToDouble(string.IsNullOrEmpty(txtMaterialf.Text)?"0":txtMaterialf.Text);
                    add.Labour = Convert.ToDouble(string.IsNullOrEmpty(txtLabourf.Text)?"0":txtLabourf.Text);
                    add.Mics = Convert.ToDouble(string.IsNullOrEmpty(txtMiscf.Text)?"0":txtMiscf.Text);
                    add.CurrencyID = Convert.ToInt32((ddlCurrency_f.SelectedValue=="0") ? Defaultcurrence.ToString() : ddlCurrency_f.SelectedValue);
                    //add.sellingTotla = Convert.ToDouble(string.IsNullOrEmpty(txtSellingPrice.Text) ? "0" : txtSellingPrice.Text);
                    add.SellingTotal = Convert.ToDouble(string.IsNullOrEmpty(txtSelling.Text) ? "0" : txtSelling.Text);
                    InsertBOM.ProjectBOMs.InsertOnSubmit(add);
                    InsertBOM.SubmitChanges();

                    lblError.Text = "Inserted Successfully";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Green;
                   
                }
                else
                {
                    lblError.Text = "Please select worksheet";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            }
            if (e.CommandName == "Update")           
            {

                //Get existing recored
                ProjectMgt.Entity.ProjectBOM Update = InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                lblError.Visible = false;
                int i = gridBOM.EditIndex;
                GridViewRow row = gridBOM.Rows[i];
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtPartNumber = (TextBox)row.FindControl("txtPartNumber");
                DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");
                DropDownList ddlManufacturer = (DropDownList)row.FindControl("ddlManufacturer");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtMaterial = (TextBox)row.FindControl("txtMaterial");
                TextBox txtLabour = (TextBox)row.FindControl("txtLabour");
                TextBox txtMisc = (TextBox)row.FindControl("txtMisc");
                TextBox txtSellingPrice = (TextBox)row.FindControl("txtSellingPrice");
                DropDownList ddlCurrency = (DropDownList)row.FindControl("ddlCurrency");
                Label lblTotaldd=(Label)row.FindControl("lblTotaldd");
                //current total
                lblTotaldd.Text = string.Format("{0:F2}",((Convert.ToDouble(string.IsNullOrEmpty(txtLabour.Text) ? "0" : txtLabour.Text) + Convert.ToDouble(string.IsNullOrEmpty(txtMaterial.Text) ? "0" : txtMaterial.Text) + Convert.ToDouble(string.IsNullOrEmpty(txtMisc.Text) ? "0" : txtMisc.Text)) * Convert.ToDouble(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text)));
                //old total
                string total_p = string.Format("{0:F2}", ((Convert.ToDouble(!Update.Labour.HasValue ? "0" : Update.Labour.Value.ToString()) + Convert.ToDouble(!Update.Material.HasValue ? "0" : Update.Material.Value.ToString()) + Convert.ToDouble(!Update.Mics.HasValue ? "0" : Update.Mics.Value.ToString())) * Convert.ToDouble(!Update.Qty.HasValue ? "0" : Update.Qty.Value.ToString()) ) );

                Update.Description = txtDescription.Text;
                Update.Labour = Convert.ToDouble(string.IsNullOrEmpty(txtLabour.Text)?"0":txtLabour.Text);
                Update.Material = Convert.ToDouble(string.IsNullOrEmpty(txtMaterial.Text)?"0":txtMaterial.Text);
                Update.Mics = Convert.ToDouble(string.IsNullOrEmpty(txtMisc.Text)?"0":txtMisc.Text);
                Update.PartNumber = txtPartNumber.Text;
                Update.Supplier = int.Parse(string.IsNullOrEmpty(ddlSupplier.SelectedValue) ? "0" : ddlSupplier.SelectedValue);
                Update.ManufactureID = int.Parse(ddlManufacturer.SelectedValue);
                Update.Unit =txtUnit.Text;
                //if the qty is modified selling price will update 
                Update.Qty = Convert.ToDouble(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);
                if (Convert.ToDouble(string.IsNullOrEmpty(total_p) ? "0" : total_p) != Convert.ToDouble(string.IsNullOrEmpty(lblTotaldd.Text) ? "0" : lblTotaldd.Text))
                {
                    if (Update.GP > 0)
                        Update.SellingTotal = ((Convert.ToDouble(string.IsNullOrEmpty(lblTotaldd.Text) ? "0" : lblTotaldd.Text)) * (Update.GP / 100)) + Convert.ToDouble(string.IsNullOrEmpty(lblTotaldd.Text) ? "0" : lblTotaldd.Text);
                    else
                        Update.SellingTotal = Convert.ToDouble(string.IsNullOrEmpty(lblTotaldd.Text) ? "0" : lblTotaldd.Text);
                }
                else
                {
                    Update.SellingTotal = Convert.ToDouble(string.IsNullOrEmpty(txtSellingPrice.Text) ? "0" : txtSellingPrice.Text);
                }
                
               
                Update.GP = CalculateGP(Convert.ToDouble(string.IsNullOrEmpty(lblTotaldd.Text) ? "0" : lblTotaldd.Text), Convert.ToDouble(string.IsNullOrEmpty(txtSellingPrice.Text) ? "0" : txtSellingPrice.Text));
                Update.CurrencyID = Convert.ToInt32((ddlCurrency.SelectedValue == "0") ? Defaultcurrence.ToString() : ddlCurrency.SelectedValue);
                InsertBOM.SubmitChanges();
               
                lblError.Text = "Updated Successfully";
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Green;
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            }

            if (e.CommandName == "Delete")
            {
                ProjectMgt.Entity.ProjectBOM pvi = InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                InsertBOM.ProjectBOMs.DeleteOnSubmit(pvi);
                InsertBOM.SubmitChanges();
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            }

            if (e.CommandName =="Add")
            {
                //mdlSimpleDetails.Show();
            }
            if (e.CommandName == "GP")
            {
                int i = gridBOM.EditIndex;
                GridViewRow row = gridBOM.Rows[i];
                TextBox txtGP = (TextBox)row.FindControl("txtGP");
                Label lblTotald = (Label)row.FindControl("lblTotald");
                ProjectMgt.Entity.ProjectBOM Update =
             InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));

                Update.GP = Convert.ToDouble(txtGP.Text);
                Update.SellingTotal =(Convert.ToDouble(lblTotald.Text)*Convert.ToDouble(txtGP.Text))/100;
                InsertBOM.SubmitChanges();
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            }
            if (e.CommandName == "AddRate")
            {
                clearRateCards();
                mdlPopRateCard.Show();
             }
            
           
        
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }




    protected void gridBOM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        gridBOM.EditIndex = e.NewEditIndex;
        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
    }
    protected void gridBOM_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblError.Visible = false;
        gridBOM.EditIndex = -1;
        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
    }
    protected void gridBOM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblError.Visible = false;
        gridBOM.EditIndex = -1;
        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
    }


    protected void gridBOM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblError.Visible = false;
        gridBOM.EditIndex = -1;
        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
    }


    protected void lnkOk_Click(object sender, EventArgs e)
    {
        int items = 0;
        try
        {
            DataView myDataView = (DataView)DS_Services.Select(DataSourceSelectArguments.Empty);
            DataRowView myRow = myDataView[0];
            GridView grd_services = (GridView)gridBOM.FooterRow.FindControl("grd_services");
            for (int i = 0; i < grd_services.Rows.Count; i++)
            {
                GridViewRow row = grd_services.Rows[i];
                CheckBox chkbox1 = (CheckBox)row.FindControl("chkItem");
                Label lblTeamName = (Label)row.FindControl("lblTeamName");
                Label lblID = (Label)row.FindControl("lblID");
                


                if (chkbox1.Checked)
                {
                    items++;
                    //Label lblID = (Label)row.FindControl("lblID");
                    DataTable dsnew = myDataView.ToTable();// = (DataTable)DS_Services.Select(DataSourceSelectArguments.Empty);

                    for (int j = 0; j < dsnew.Rows.Count; j++)
                    {
                        string ID = dsnew.Rows[j]["ID"].ToString();
                        if (ID == lblID.Text)
                        {
                            TextBox txtfoo_service = (TextBox)gridBOM.FooterRow.FindControl("txtfoo_service");
                            TextBox txtPartNumberf = (TextBox)gridBOM.FooterRow.FindControl("txtPartNumberf");
                            DropDownList ddlSupplierf = (DropDownList)gridBOM.FooterRow.FindControl("ddlSupplierf");
                            TextBox txtUnitf = (TextBox)gridBOM.FooterRow.FindControl("txtUnitf");
                            TextBox txtQtyf = (TextBox)gridBOM.FooterRow.FindControl("txtQtyf");
                            TextBox txtMaterialf = (TextBox)gridBOM.FooterRow.FindControl("txtMaterialf");
                            TextBox txtLabourf = (TextBox)gridBOM.FooterRow.FindControl("txtLabourf");
                            TextBox txtMiscf = (TextBox)gridBOM.FooterRow.FindControl("txtMiscf");

                            txtfoo_service.Text = dsnew.Rows[j]["Description"].ToString();
                            txtPartNumberf.Text = dsnew.Rows[j]["PartNumber"].ToString();
                            txtUnitf.Text = string.Format("{0:f2}", dsnew.Rows[j]["Unit"]);
                            txtQtyf.Text = string.Format("{0:f2}", dsnew.Rows[j]["Qty"]);
                            txtMaterialf.Text = string.Format("{0:f2}", dsnew.Rows[j]["Material"]);
                            txtLabourf.Text = string.Format("{0:f2}", dsnew.Rows[j]["Labour"]);
                            txtMiscf.Text = string.Format("{0:f2}", dsnew.Rows[j]["Mics"]);
                            BindVendors(ddlSupplierf,int.Parse( dsnew.Rows[j]["Supplier"].ToString()));

                        }
                    }


                    chkbox1.Checked = false;
                    //lblGridMsg.Text = "Insertion failed. Please try again.";
                }
            }

            if (items == 0)
            {
                lblError.Text = "Please select at least one item";
                lblError.ForeColor=System.Drawing.Color.Red;
                lblError.Visible=true;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindSummary(int worksheetID)
    {
        try
        {
            lblMaterial_bp_ws.Text = "";
            lblMisc_bp_ws.Text = "";
            lblLabour_bp_ws.Text = "";
            lblTotal_bp_ws.Text = "";
            lblSellingPrice.Text = "";
            projectTaskDataContext BOM = new projectTaskDataContext();

            var listExist = (from r in BOM.ProjectBOMDetils
                             where (r.ProjectReference == QueryStringValues.Project &&
                             r.WorkSheetID == int.Parse(ddlWorksheet.SelectedValue))
                             select r).ToList();
            if (listExist != null)
            {
                if (listExist.Count > 0)
                {
                    var Material1 = (from r in BOM.ProjectBOMDetils
                                     where (r.ProjectReference == QueryStringValues.Project &&
                                     r.WorkSheetID == worksheetID)
                                     select (r.Material * r.Qty)).Sum();

                    var Labour1 = (from r in BOM.ProjectBOMDetils
                                   where (r.ProjectReference == QueryStringValues.Project &&
                                   r.WorkSheetID == worksheetID)
                                   select (r.Labour * r.Qty)).Sum();


                    var Mics1 = (from r in BOM.ProjectBOMDetils
                                 where (r.ProjectReference == QueryStringValues.Project &&
                                 r.WorkSheetID == worksheetID)


                                 select (r.Mics * r.Qty)).Sum();

                    var selling = (from r in BOM.ProjectBOMDetils
                                 where (r.ProjectReference == QueryStringValues.Project &&
                                 r.WorkSheetID == worksheetID)


                                 select (r.sellingTotal)).Sum();

                    lblMaterial_bp_ws.Text = string.Format("{0:f2}", Material1);
                    lblMisc_bp_ws.Text = string.Format("{0:f2}", Mics1);
                    lblLabour_bp_ws.Text = string.Format("{0:f2}", Labour1);
                    lblSellingPrice.Text = string.Format("{0:f2}", selling);
                    lblTotal_bp_ws.Text = string.Format("{0:f2}", (Material1 + Mics1 + Labour1));
                }
            }

           

            lblMaterial_bp.Text = "";
            lblMisc_bp.Text = "";
            lblLabour_bp.Text = "";

            lblTotal_bp.Text = "";
            lblWrkSellingPrice.Text = "";
            var listExist1 = (from r in BOM.ProjectBOMDetils
                             where r.ProjectReference == QueryStringValues.Project 
                           
                             select r).ToList();
            if (listExist1 != null)
            {
                if (listExist1.Count > 0)
                {
                    var MaterialAll = (from r in BOM.ProjectBOMDetils
                                       join b in BOM.BOM_Types on r.WorkSheetID equals b.ID
                                       where (r.ProjectReference == QueryStringValues.Project  && (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false)
                                       select (r.Material * r.Qty)).Sum();

                    var LabourAll = (from r in BOM.ProjectBOMDetils
                                     join b in BOM.BOM_Types on r.WorkSheetID equals b.ID
                                     where (r.ProjectReference == QueryStringValues.Project &&  (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false)
                                     select (r.Labour * r.Qty)).Sum();


                    var MicsAll = (from r in BOM.ProjectBOMDetils join b in BOM.BOM_Types on r.WorkSheetID equals b.ID
                                   where (r.ProjectReference == QueryStringValues.Project &&  (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false)

                                   select (r.Mics * r.Qty)).Sum();


                    var sellingAll = (from r in BOM.ProjectBOMDetils join b in BOM.BOM_Types on r.WorkSheetID equals b.ID
                                   where (r.ProjectReference == QueryStringValues.Project &&  (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false)

                                   select (r.sellingTotal)).Sum();

                    lblMaterial_bp.Text = string.Format("{0:f2}", MaterialAll);
                    lblMisc_bp.Text = string.Format("{0:f2}", MicsAll);
                    lblLabour_bp.Text = string.Format("{0:f2}", LabourAll);
                    lblWrkSellingPrice.Text = string.Format("{0:f2}", sellingAll);
                    lblTotal_bp.Text = string.Format("{0:f2}", (MaterialAll + MicsAll + LabourAll));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }
    protected void RedVisible()
    {
        projectTaskDataContext BOM = new projectTaskDataContext();
        var MaterialAll = (from r in BOM.ProjectBOMDetils
                           where (r.ProjectReference == QueryStringValues.Project)
                           select (r.Material * r.Qty)).Sum();

        var LabourAll = (from r in BOM.ProjectBOMDetils
                         where (r.ProjectReference == QueryStringValues.Project)
                         select (r.Labour * r.Qty)).Sum();


        var MicsAll = (from r in BOM.ProjectBOMDetils
                       where (r.ProjectReference == QueryStringValues.Project)

                       select (r.Mics * r.Qty)).Sum();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            Response.Redirect(string.Format("~/ProjectBudget.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            Response.Redirect(string.Format("~/ProjectBOM.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "3")
        {
            Response.Redirect(string.Format("~/ProjectBenfitBudget.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "5")
        {
            Response.Redirect(string.Format("~/ProjectBudgetbyTask.aspx?project={0}", QueryStringValues.Project));
        }
        else
        {
            Response.Redirect(string.Format("~/GoodsReceipt.aspx?project={0}", QueryStringValues.Project));
        }
    }
    //protected void imgRequistion_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/ProjectBOMSupplierRequisitions.aspx?project={0}", QueryStringValues.Project));
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/ProjectBOMSupplierRequisitions.aspx?project={0}", QueryStringValues.Project));
    }
    //protected void imgPrint_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Reports/ProjectBOMReport.aspx?id={0}", ddlWorksheet.SelectedValue));
    //}
    protected void btnaddtoquote_Click(object sender, EventArgs e)
    {
        GetSeletedIds();
    }
    protected void btngenerate_Click(object sender, EventArgs e)
    {
        try
        {
            GetSeletedIds();
            
            int ProjectReference = Convert.ToInt32(QueryStringValues.Project);
            AllIds = (string)Session["v_Ids"];
            AllTypes = (string)Session["v_TypeIds"];
            int QuoteID = 0;
            if (!string.IsNullOrEmpty(AllIds))
            {
                object obj = QuoteItemManager.QuoteItemInsert(AllIds, AllTypes, sessionKeys.UID, ProjectReference);
                QuoteID = int.Parse(obj.ToString());
                Session["v_Ids"] = null;
                Session["v_TypeIds"] = null;
                if (QuoteID == -99)
                {
                    lblMsg.Text = "Please add Quote start point in Quote Admin";
                    lblMsg.Visible = true;
                }
                else
                    if (QuoteID == -98)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Please select item(s) to quote";
                    }
                    else
                        Response.Redirect("QuoteMain.aspx?Project=" + QueryStringValues.Project + "&QuoteID=" + QuoteID + "&WorkID=" + ddlWorksheet.SelectedValue);
            }
               
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please select item(s) to quote";
            }
            Session["v_Idsg"] = null;
            Session["v_TypeIdsg"] = null;
           
        }
        catch (Exception ex)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please add item(s) to quote";
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void EnableButton()
    {
          string[] str = PermissionManager.GetFeatures();

          imgsupReq.Visible =Convert.ToBoolean(str[92]);
    }

    protected void imgsupReq_Click(object sender, EventArgs e)
    {
       // Response.Redirect(string.Format("~/ProjectBOMSupplierRequisitions.aspx?project={0}", QueryStringValues.Project));
        GetSeletedIds();
       
        int ProjectReference = Convert.ToInt32(QueryStringValues.Project);
        AllIds = (string)Session["v_Ids"];
        AllTypes = (string)Session["v_TypeIds"];

        DataTable dt = new DataTable();
        //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select * from ProjectDefaults ").Tables[0];
        int QuoteID = 0;
        int POID = 0;
        if (!string.IsNullOrEmpty(AllIds))
        {
            POID = PONumber();
            if (POID == -99)
            {
                lblMsg.Text = "Please add PO number start point in system Admin";
                lblMsg.Visible = true;
            }
            else
            {
                object obj = QuoteItemManager.SupplierQuoteItemInsert(AllIds, AllTypes, sessionKeys.UID, ProjectReference);
                QuoteID = int.Parse(obj.ToString());
               
                Session["v_Ids"] = null;
                Session["v_TypeIds"] = null;
                if (QuoteID == -99)
                {
                    lblMsg.Text = "Please add Quote start point in Quote Admin";
                    lblMsg.Visible = true;
                }
                //else if (POID == -99)
                //{
                //    lblMsg.Text = "Please add PO number start point in system Admin";
                //    lblMsg.Visible = true;
                //}
                else
                    Response.Redirect("ProjectBOMSupplierRequisitions.aspx?Project=" + QueryStringValues.Project + "&QuoteID=" + QuoteID);
            }
           
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please select item(s) to Supplier Requisitions";
        }
        Session["v_Idsg"] = null;
        Session["v_TypeIdsg"] = null;
    }
    private int PONumber()
    {
        using (SqlConnection con = new SqlConnection(Constants.DBString))
        {
            using (SqlCommand cmd = new SqlCommand("SupplierReqPO", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
    protected bool isChecked(string ID)
    {
        bool check = false;
        try
        {

            if (AllIds != null)
            {
                string[] stat = AllIds.Split(',');
                for (int i = 0; i < stat.Length; i++)
                {
                    if (stat[i] == ID)
                    {
                        check = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return check;
    }


    private void GetSeletedIds()
    {
        try
        {
            for (int i = 0; i < gridBOM.Rows.Count; i++)
            {
                GridViewRow row = gridBOM.Rows[i];
                bool ischecked = ((CheckBox)row.FindControl("chkbox")).Checked;
                if (ischecked)
                {
                    string id = ((Label)row.FindControl("lblID")).Text;
                    IDs = IDs + "," + id;
                    Types = Types + "," + "5";
                }
            }
            //ViewState["v_Ids"] = ViewState["v_Ids"] + IDs;
            //ViewState["v_TypeIds"] = ViewState["v_TypeIds"] + Types;

            Session["v_Ids"] = Session["v_Ids"] + IDs;
            Session["v_TypeIds"] = Session["v_TypeIds"] + Types;

            

            AllIds = (string)Session["v_Ids"];
            AllTypes = (string)Session["v_TypeIds"];


            //HD_Ids.Value = IDs.ToString();
            //HD_Types.Value = Types;

            if (IDs == "0")
            {
                lblMsg.Text = "Please select item(s)";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void GetSeletedIdsG()
    {
        try
        {
            for (int i = 0; i < gridBOM.Rows.Count; i++)
            {
                GridViewRow row = gridBOM.Rows[i];
                bool ischecked = ((CheckBox)row.FindControl("chkbox")).Checked;
                if (ischecked)
                {
                    string id = ((Label)row.FindControl("lblID")).Text;
                    IDs = IDs + "," + id;
                    Types = Types + "," + "5";
                }
            }
            //ViewState["v_Ids"] = ViewState["v_Ids"] + IDs;
            //ViewState["v_TypeIds"] = ViewState["v_TypeIds"] + Types;

            Session["v_Idsg"] = Session["v_Idsg"] + IDs;
            Session["v_TypeIdsg"] = Session["v_TypeIdsg"] + Types;



            AllIds = (string)Session["v_Idsg"];
            AllTypes = (string)Session["v_TypeIdsg"];


            //HD_Ids.Value = IDs.ToString();
            //HD_Types.Value = Types;

            if (IDs == "0")
            {
                lblMsg.Text = "Please select item(s)";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region RateCard Popup
    private void BindClassification(DropDownList ddlClass)
    {
        FinanceModuleDataContext user = new FinanceModuleDataContext();
        try
        {
            var classification = from r in user.ExperienceClassifications
                                 orderby r.ExpClassification
                                 select r;
            

            //DropDownList ddlClass = (DropDownList)gridBOM.FooterRow.FindControl("ddlClass");
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

    private void BindResource(DropDownList ddlResource)
    {
        try
        {
            UserDataContext contra = new UserDataContext ();
            
            var contractor = from c in contra.Contractors
                             where c.ID == sessionKeys.UID
                             select c;
            
           UserMgt.Entity.Contractor Resource = contra.Contractors.Single(c => c.ID == sessionKeys.UID);
            if (Resource.SID != 1)
            {
                //we need to select only assigned project resources
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select (select  ContractorName from Contractors where ID =CustomerID and Status='Active') Resource,CustomerID" +
                " from AssignedCustomerToPortfolio where Portfolio =(select Portfolio from Projects where ProjectReference=@ProjectRef) Order by Resource", new SqlParameter("@ProjectRef", sessionKeys.Project)).Tables[0];
                ddlResource.DataSource = dt;
                ddlResource.DataTextField = "Resource";
                ddlResource.DataValueField = "CustomerID";

                ddlResource.DataBind();
                ddlResource.Items.Insert(0, new ListItem("Please Select...", "0"));
            }
            else
            {
                //we need to select all resources here
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select ContractorName,ID from Contractors where Status='Active' Order by ContractorName").Tables[0];
                ddlResource.DataSource = dt;
                ddlResource.DataTextField = "ContractorName";
                ddlResource.DataValueField = "ID";
               
                ddlResource.DataBind();
                ddlResource.Items.Insert(0, new ListItem("Please Select...", "0"));
            }

            //var resource = from 
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //AjaxControlToolkit.ModalPopupExtender mdlPopupRatecard = (AjaxControlToolkit.ModalPopupExtender)gridBOM.FooterRow.FindControl("mdlPopRateCard");
            mdlPopRateCard.Show();
            int classiType = Convert.ToInt32(ddlClass.SelectedItem.Value.ToString());
            BindGrdViewRate(grdRatecard, classiType);
           
            grdRateRes.Visible = false;
            grdRatecard.Visible = true;
            ddlResource.SelectedIndex = 0;

            grdRateRes.DataSource = null;
            grdRateRes.DataBind();
            
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // AjaxControlToolkit.ModalPopupExtender mdlPopupRatecard = (AjaxControlToolkit.ModalPopupExtender)gridBOM.FooterRow.FindControl("mdlPopRateCard");
            mdlPopRateCard.Show();
            int Res = Convert.ToInt32(ddlResource.SelectedItem.Value.ToString());
            DisplayUserRate(Res);
            grdRateRes.Visible = true;
            ddlClass.SelectedIndex = 0;
            grdRatecard.Visible = false;
            grdRatecard.DataSource = null;
            grdRatecard.DataBind();
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    


    private void BindGrdViewRate(GridView grdRatecard, int classiType)
    {
        try
        {
            //
            //select ID,(select ExpClassification from ExperienceClassification where ID =F.CardClassfication )Classification,
            //(select EntryType from TimesheetEntryType where ID=F.RateType) RateType, DailyRate,HourlyRate
            //from Financeratecard F where ClassType =1
            FinanceModuleDataContext FMDatacntxt = new FinanceModuleDataContext();
            TimeSheetDataContext TSDatacntxt = new TimeSheetDataContext();
            UserDataContext UserMangDatacntx = new UserDataContext();
            var FM = (from r in FMDatacntxt.FinanceRateCards
                      where r.ClassType == 1
                      select r).ToList();
            var TS = (from r in TSDatacntxt.TimesheetEntryTypes

                      select r).ToList();

            var US = (from r in FMDatacntxt.ExperienceClassifications

                      select r).ToList();
            var GrdData = (from F in FM
                           join T in TS on F.RateType equals T.ID
                           join S in US on F.CardClassfication equals S.ID
                           where F.CardClassfication == classiType
                           select new
                           {
                               F.ID,
                               S.ExpClassification,
                               T.EntryType,
                               F.DailyRate,
                               F.HourlyRate


                           }).ToList().Distinct();



            grdRatecard.DataSource = GrdData;
            grdRatecard.DataBind();




        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCancel_click(object sender, EventArgs e)
    {
        try
        {
            clearRateCards();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void clearRateCards()
    {
        try
        {
            //AjaxControlToolkit.ModalPopupExtender mdlPopupRatecard = (AjaxControlToolkit.ModalPopupExtender)gridBOM.FooterRow.FindControl("mdlPopRateCard");
           // mdlPopupRatecard.Hide();
            ddlClass.SelectedIndex = 0;
            ddlResource.SelectedIndex = 0;
            grdRateRes.Visible = false;
            grdRatecard.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    public void DisplayUserRate(int ContractorID)
    {
        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand myCommand = new SqlCommand("DN_DisplayRate", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ContractorsId", SqlDbType.Int, 32).Value = ContractorID;
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        DataSet ds = new DataSet();
        myadapter.Fill(ds);
        grdRateRes.DataSource = ds;
        grdRateRes.DataBind();
        grdRateRes.Visible = true;
    }

    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";


        //   string GetHours1 = GetDisplay.ToString();
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }

    protected void imgbtnClsSel_Click(object sender, EventArgs e)
    {
        int items = 0;
        try
        {
            //DataView myDataView = new DataView(); //(DataView)DS_Services.Select(DataSourceSelectArguments.Empty);
           // DataRowView myRow = myDataView[0];
            //GridView grd_RateCard = (GridView)gridBOM.FooterRow.FindControl("grdRatecard");
            if (grdRatecard.Rows.Count >0)
            {
                for (int i = 0; i < grdRatecard.Rows.Count; i++)
                {
                    GridViewRow row = grdRatecard.Rows[i];
                    CheckBox chkbox1 = (CheckBox)row.FindControl("chkItem");
                    if (chkbox1.Checked)
                    {
                        items++;
                        TextBox txtLabourf = (TextBox)gridBOM.FooterRow.FindControl("txtLabourf");
                        TextBox txtfoo_service = (TextBox)gridBOM.FooterRow.FindControl("txtfoo_service");
                        Label lblDailyRate = (Label)grdRatecard.Rows[i].FindControl("lblDailyRate");
                        txtLabourf.Text = string.Format("{0:f2}", lblDailyRate.Text);
                        txtfoo_service.Text = ddlClass.SelectedItem.Text.ToString();
                        chkbox1.Checked = false;
                    }
                }
               

            }
            else if (grdRateRes.Rows.Count > 0)
            {
                for (int i = 0; i < grdRateRes.Rows.Count; i++)
                {
                    GridViewRow row = grdRateRes.Rows[i];
                    CheckBox chkbox1 = (CheckBox)row.FindControl("chkItem");
                    if (chkbox1.Checked)
                    {
                        items++;
                        TextBox txtLabourf = (TextBox)gridBOM.FooterRow.FindControl("txtLabourf");
                        TextBox txtfoo_service = (TextBox)gridBOM.FooterRow.FindControl("txtfoo_service");
                        Label lblHourlyRate = (Label)grdRateRes.Rows[i].FindControl("lblHourlyBuying");
                        txtLabourf.Text = string.Format("{0:f2}", lblHourlyRate.Text);
                        txtfoo_service.Text = ddlResource.SelectedItem.Text.ToString();
                        chkbox1.Checked = false;
                        
                    }
                }
             }
            if (items == 0)
            {
                lblError.Text = "Please select at least one item";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
            }
            clearRateCards();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
    #region "Permission Code Here"
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role =Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {
                    //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }
                role =Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                   // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }

            }
        }
    }
    private void Disable()
    {
        btnAdd_worksheet.Enabled = false;
        btnaddtoquote.Enabled = false;
        btnDel_worksheet.Enabled = false;
        btnEdit_worksheet.Enabled = false;
        btngenerate.Enabled = false;
        imgbtnClsSel.Enabled = false;
        imgsupReq.Enabled = false;


    }
    #endregion 

    #region Add GP
    protected void btn_IndetDecrease_OnClick(object sender, EventArgs e)
    {
        try
        {
            projectTaskDataContext projectBOM = new projectTaskDataContext();
            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
            //HIndent
            double GP = Convert.ToDouble(((TextBox)row.FindControl("txtGP")).Text);
            double total = Convert.ToDouble(((Label)row.FindControl("lblTotald")).Text);
            int ID = int.Parse(this.gridBOM.DataKeys[row.RowIndex].Value.ToString());
            
                ProjectMgt.Entity.ProjectBOM Update =
                 projectBOM.ProjectBOMs.Single(P => P.ID == ID);

                Update.GP = GP;
                Update.SellingTotal = total + ((total * GP) / 100);
                projectBOM.SubmitChanges();
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }



    }

    private double CalculateGP(double CostPrice,double SellingPrice)
    {
        double GP=0;
        //SellingPrice = CostPrice + ((CostPrice * GP) / 100);
        if (CostPrice == 0 || SellingPrice==0)
        {

            GP = 0;
        }
        else
        {
            GP = ((CostPrice - SellingPrice) * 100) / CostPrice;
        }

       // GP = ((SellingPrice - CostPrice) * 100) / SellingPrice;
        if (GP < 0)
        {
            GP = -1 * GP;
        }
        return GP;
    }


    public string CalculateGP_sum(string  CostPrice, string SellingPrice)
    {
        double GP = 0;
        //SellingPrice = CostPrice + ((CostPrice * GP) / 100);
        if (Convert.ToDouble(SellingPrice) == 0 || Convert.ToDouble(CostPrice)==0)
        {

            GP = 0;
        }
        else
        {
            GP = ((Convert.ToDouble(CostPrice) - Convert.ToDouble(SellingPrice)) * 100) / Convert.ToDouble(CostPrice);
        }
        if (GP < 0)
        {
            GP = -1 * GP;
        }
        return GP.ToString("N2");
    }
    #endregion


    #region BindCurrencey

    private void BindCurrencey(DropDownList ddlCurrencey,int setVal)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,CurrencyName from CurrencyList where Display ='Y' union select 0,' Please select..' order by CurrencyName").Tables[0];
        ddlCurrencey.DataTextField = "CurrencyName";
        ddlCurrencey.DataValueField = "ID";
        ddlCurrencey.DataSource =dt;
        ddlCurrencey.DataBind();

        ddlCurrencey.SelectedValue = setVal.ToString();
    }
    #endregion 
    #region "Add Service catalog"
    //private void BindPopWindow()
    //{
    //    //Query_SerchItems
    //    using (projectTaskDataContext projectDB = new projectTaskDataContext())
    //    {
    //        //var shopItems = (from r in projectDB.ShopItems_vendorDetails
    //        //                 orderby r.Type
    //        //                 select r).ToList();
    //        //var s = from r in projectDB.ShopItems_vendorDetails
    //        //        select r;
            
    //        var shopItems = projectDB.ExecuteQuery<ShopItems_vendorDetails>
    //            (Query_SerchItems(int.Parse(ddlVendors.SelectedValue), int.Parse(ddlSelect.SelectedValue), txtItemDescription.Text.Trim(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue)).ToList();

    //        if (shopItems != null)
    //        {
    //            GridView2.DataSource = shopItems;
    //            GridView2.DataBind();
    //        }
    //    }
    //}

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void lnkbtncatalog_Click(object sender, EventArgs e)
    {
        ifrmCatelog.Attributes.Add("src", string.Format("~/WF/Projects/ProjectBOMVendorCatalogue.aspx?Project={0}&worksheetid={1}", QueryStringValues.Project, ddlWorksheet.SelectedValue));
        ifrmCatelog.DataBind();
        //lblErr.Text = "";
        //BindVendors(ddlVendors, 0);
        //if (ddlVendors.Items.Count > 0)
        //{
        //    ddlVendors.SelectedIndex = 1;
        //}
        //BindPopWindow();
        mpopBOM.Show();
    }

    public string GetItemsType(string value)
    {
        string val = "";
        if (value == "1")
        {
            val = "Labour";
        }
        if (value == "2")
        {
            val = "Product";
        }
        if (value == "3")
        {
            val = "Service";
        }
        return val;
    }

    protected void imgUpdate_Click(object sender, EventArgs e)
    {

        //projectTaskDataContext InsertBOM = new projectTaskDataContext();
        //try
        //{

        //    var currencey = (from r in InsertBOM.ProjectDefaults
        //                     select r).ToList().FirstOrDefault();
        //    int Defaultcurrence = 0;
        //    if (currencey != null)
        //    {
        //        Defaultcurrence = currencey.DefaultCurrency.Value;
        //    }
        //    int items = 0;
        //    foreach (GridViewRow row in GridView2.Rows)
        //    {
        //        CheckBox chkRow = (CheckBox)row.FindControl("chkbox");
        //        if (chkRow.Checked)
        //        {
        //            items++;
        //            Label lblID = (Label)row.FindControl("lblID");
        //            ShopItems_vendorDetails vitems = InsertBOM.ShopItems_vendorDetails.Where(p => p.ID == int.Parse(lblID.Text)).FirstOrDefault();

        //            Label lblType = (Label)row.FindControl("lblType");
        //            Label lblVendorID = (Label)row.FindControl("lblVendorID");
        //            Label lblDescription = (Label)row.FindControl("lblDescription");


        //            ProjectMgt.Entity.ProjectBOM add = new ProjectMgt.Entity.ProjectBOM();
        //            add.Description = lblDescription.Text;
        //            add.PartNumber = vitems.PartNumber;
        //            add.ProjectReference = QueryStringValues.Project;
        //            add.Supplier = int.Parse(string.IsNullOrEmpty(lblVendorID.Text) ? "0" : lblVendorID.Text);
        //            //add.Unit = Convert.ToDouble(txtUnitf.Text);
        //            add.Unit = vitems.UnitPrice;
        //            add.WorkSheetID = int.Parse(string.IsNullOrEmpty(ddlWorksheet.SelectedValue) ? "0" : ddlWorksheet.SelectedValue);
        //            add.Qty = 1;
        //            add.Material = vitems.BP;
        //            add.Labour = 0;
        //            add.Mics = 0;//Convert.ToDouble(string.IsNullOrEmpty(txtMiscf.Text) ? "0" : txtMiscf.Text);
        //            add.CurrencyID = Defaultcurrence;
        //            add.SellingTotal = vitems.SP;
        //            add.GP = vitems.BP > 0 ? ((vitems.SP - vitems.BP) / vitems.BP) * 100 : 0;
        //            InsertBOM.ProjectBOMs.InsertOnSubmit(add);
        //            InsertBOM.SubmitChanges();

        //        }

        //    }
        //    if (items == 0)
        //    {
        //        lblErr.Text = "Please select items to apply";
        //        mpopBOM.Show();

        //    }
           
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
            BindSummary(int.Parse(ddlWorksheet.SelectedValue));
    }
    protected void imgVendorSearch_Click(object sender, EventArgs e)
    {
        //BindPopWindow();
        mpopBOM.Show();
    }
    protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindPopWindow();
        mpopBOM.Show();
    }
    private string Query_SerchItems(int VendorID, int Type, string Description,string category,string subcategory)
    {
       
        string sql = string.Empty;
        sql = "select ID,VID,Type,Description,SP,VendorName,BP,Category,SubCategory from v_ShopItems_vendor where vid!=0";
        if (VendorID != 0)
        {
            sql += " and vid=" + VendorID.ToString();
        }
        if (Type != 0)
        {
            sql += " and Type=" + Type.ToString();
        }
        if (!string.IsNullOrEmpty(Description))
        {
            sql += " and Description like '%" + Description + "%'";
        }
        if (!string.IsNullOrEmpty(category))
        {
            sql += " and Category = " + category;
        }
        if (!string.IsNullOrEmpty(subcategory))
        {
            sql += " and SubCategory = " + subcategory;
        }
        
        sql += " order by Type ";
        return sql;
    }
    #endregion


    #region "Saved Worksheet"

  
    protected void imgSaveWorksheet_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                BOM_TypesDUP insert = new BOM_TypesDUP();

                var wrkSheetID = (from r in db.BOM_TypesDUPs
                                  where r.WorksheetNameDUP == txtSaveWorksheetName.Text.Trim()
                                  select r).ToList();
                if (wrkSheetID != null)
                {
                    if (wrkSheetID.Count == 0)
                    {
                        insert.ProjectReference = QueryStringValues.Project;
                        insert.worksheetid = int.Parse(ddlWorksheet.SelectedValue);
                        insert.WorksheetNameDUP = txtSaveWorksheetName.Text;
                        db.BOM_TypesDUPs.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        //insert all template items
                        BOM_templateItems(insert.worksheetid.Value, insert.ID);
                        Bind_Savedworksheet();
                        lblError.Text = "Worksheet saved successfully";
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Green;
                        txtSaveWorksheetName.Text = "";
                    }
                    else
                    {
                        lblError.Text = "Worksheet with same name already exist";
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region Bulk Insert template items
    public void BOM_templateItems(int CurrentWorksheetid,int savedWorksheetid)
    {
        try
        {
            IProjectRepository<ProjectMgt.Entity.BOM_Type_TemplateItem> btRepository = new ProjectRepository<ProjectMgt.Entity.BOM_Type_TemplateItem>();
            IProjectRepository<ProjectMgt.Entity.ProjectBOM> bomRepository = new ProjectRepository<ProjectMgt.Entity.ProjectBOM>();
            var bomList = bomRepository.GetAll().Where(o => o.WorkSheetID == CurrentWorksheetid).ToList();
            List<ProjectMgt.Entity.BOM_Type_TemplateItem> bTemplateItems = new List<BOM_Type_TemplateItem>();

            foreach (var b in bomList)
            {
                var bt = new BOM_Type_TemplateItem();
                bt.CurrencyID = b.CurrencyID;
                bt.Description = b.Description;
                bt.GP = b.GP;
                bt.Labour = b.Labour;
                bt.ManufactureID = b.ManufactureID;
                bt.Material = b.Material;
                bt.Mics = b.Mics;
                bt.NumberComplete = b.NumberComplete;
                bt.oldItemID = b.oldItemID;
                bt.oldtype = b.oldtype;
                bt.PartNumber = b.PartNumber;
                bt.Qty = b.Qty;
                bt.SellingTotal = b.SellingTotal;
                bt.serviceCatLogID = b.serviceCatLogID;
                bt.serviceCatLogType = b.serviceCatLogType;
                bt.serviceCatLogTypeCurrencyID = b.serviceCatLogTypeCurrencyID;
                bt.Supplier = b.Supplier;
                bt.Unit = bt.Unit;
                bt.WorkSheetID = savedWorksheetid;

                bTemplateItems.Add(bt);
            }

            btRepository.AddAll(bTemplateItems);

        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    public void BOM_templateItems_deleteAll(int savedWorksheetid)
    {
        try
        {
            IProjectRepository<ProjectMgt.Entity.BOM_Type_TemplateItem> btRepository = new ProjectRepository<ProjectMgt.Entity.BOM_Type_TemplateItem>();
            var bomList = btRepository.GetAll().Where(o => o.WorkSheetID == savedWorksheetid).ToList();
            btRepository.DeleteAll(bomList);
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    #endregion
    protected void ddlSavesWorkSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // lblError.Visible = false;
           // HD_Type.Value = ddlSavesWorkSheets.SelectedValue;

           // ddlWorksheet.SelectedValue = ddlSavesWorkSheets.SelectedValue;
           // linkRpt.NavigateUrl = "~/Reports/ProjectBOMprint.aspx?id=" + ddlWorksheet.SelectedValue + "&project=" + QueryStringValues.Project;
           //// GetSeletedIds();
           // //GetSeletedIdsG();
          
           // BindGrid(int.Parse(ddlSavesWorkSheets.SelectedValue));
           // BindSummary(int.Parse(ddlSavesWorkSheets.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    protected void imgWrkSheetID_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlSavesWorkSheets.SelectedValue) > 0)
            {
                SqlParameter wrkID = new SqlParameter("@retWrkID", SqlDbType.Int);
                wrkID.Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "copy_ProjectBOMToProjects"
                    , new SqlParameter("@ProjectReference", QueryStringValues.Project),
                    new SqlParameter("@Dup_WorksheetID", int.Parse(ddlSavesWorkSheets.SelectedValue)), wrkID);

                Bind_worksheet();
                projectTaskDataContext db = new projectTaskDataContext();
                var records = (from r in db.BOM_Types
                               where r.TypeName.Equals(ddlSavesWorkSheets.SelectedItem.Text)
                               select r).ToList().FirstOrDefault();
                if (records != null)
                {
                    //  ddlWorksheet.SelectedItem.Text = records.TypeName.ToString();
                }
                ddlWorksheet.SelectedValue = wrkID.Value.ToString();
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                BindSummary(int.Parse(ddlWorksheet.SelectedValue));
            }
            else {
                lblMsg.Visible = true;
                lblMsg.Text = "Please select Worksheet";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnEdit_savedworksheet_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlSavesWorkSheets.SelectedValue) > 0)
            {
                hfsavedworksheet.Value = ddlSavesWorkSheets.SelectedItem.Value;
                txtsavedworksheet.Text = ddlSavesWorkSheets.SelectedItem.Text;
                mdlSavedWorksheet.Show();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please select Worksheet";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnDel_savedworksheet_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                if (int.Parse(ddlSavesWorkSheets.SelectedValue) > 0)
                {
                    int id = int.Parse(ddlSavesWorkSheets.SelectedValue);
                    var result = from p in db.BOM_TypesDUPs
                                 where p.ID == id
                                 select p;

                    db.BOM_TypesDUPs.DeleteAllOnSubmit(result);
                    db.SubmitChanges();
                    Bind_Savedworksheet();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void imgBtnsavedWorksheet_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                if (!string.IsNullOrEmpty(txtsavedworksheet.Text.Trim()))
                {
                    int id = 0;
                    if (!string.IsNullOrEmpty(hfsavedworksheet.Value))
                    {
                        id = int.Parse(hfsavedworksheet.Value);
                    }

                    BOM_TypesDUP bom = db.BOM_TypesDUPs.Where(p => p.ID == id).SingleOrDefault();
                    bom.WorksheetNameDUP = txtsavedworksheet.Text;
                    db.SubmitChanges();
                    Bind_Savedworksheet();
                    txtsavedworksheet.Text = string.Empty;
                    txtsavedworksheet.Focus();



                }
            }
        }
         
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtndeletedList_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                var x = (from a in pdc.BOMDeletedWorksheets
                         join b in pdc.BOM_Types on a.Wid equals b.ID
                         where b.ProjectReference == QueryStringValues.Project
                         select new
                         {
                             Wsid=a.Wid,
                             DeletedID=a.id,
                             WorksheetName=b.TypeName,
                             CreatedBy=b.Created_by,
                             DeleteDate=a.DeleteOn.Value.ToShortDateString(),
                             deletedBy=a.deleteedBy
                         }).ToList();
                var CIds = x.Select(a => a.CreatedBy).ToList();
                var DIds = x.Select(a => a.deletedBy).ToList();
                using (UserDataContext Udc = new UserDataContext())
                {
                    var CreatedIds = Udc.Contractors.Where(o=>CIds.ToArray().Contains(o.ID) ).ToList();
                    var DeletedIds = Udc.Contractors.Where(o => DIds.ToArray().Contains(o.ID)).ToList();
                    var y = (from a in x
                             //join b in FilteredIds on a.CreatedBy equals b.ID
                             select new
                             {
                                 Wsid = a.Wsid,
                                 DeletedID = a.DeletedID,
                                 WorksheetName = a.WorksheetName,
                                 CreatedBy = CreatedIds.FirstOrDefault() == null ? string.Empty : CreatedIds.FirstOrDefault().ContractorName,
                                 DeleteDate = a.DeleteDate,
                                 deletedBy = DeletedIds.FirstOrDefault() == null ? string.Empty : DeletedIds.FirstOrDefault().ContractorName 
                             }).ToList();


                    griddeletedlist.DataSource = y;
                    griddeletedlist.DataBind();
                    DeletedPopUp.Show();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindingDeletedWorksheets()
    {
       
    }
    protected void griddeletedlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Recover")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    BOMDeletedWorksheet bom = new BOMDeletedWorksheet();
                    bom = (from a in Pdc.BOMDeletedWorksheets where a.Wid == id select a).FirstOrDefault();
                    Pdc.BOMDeletedWorksheets.DeleteOnSubmit(bom);
                    Pdc.SubmitChanges();

                    BOM_Type B = (from a in Pdc.BOM_Types where a.ID == id select a).FirstOrDefault();
                    B.IsDeleted = false;
                    Pdc.SubmitChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Download BoM Template
    protected void btnDownloadBoMTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("worksheet1");

            int x = 2;
            // Title
            ws.Cell("A1").Value = "Description";

            ws.Cell("B1").Value = "Part Number";
            ws.Cell("C1").Value = "Type";
            ws.Cell("D1").Value = "Supplier";
            ws.Cell("E1").Value = "Manufacturer";
            ws.Cell("F1").Value = "Unit";
            ws.Cell("G1").Value = "Qty";
            ws.Cell("H1").Value = "Buying Price";
            ws.Cell("I1").Value = "Total";
            ws.Cell("J1").Value = "GP (%)";
            ws.Cell("K1").Value = "Selling Price";

            ws.Column(1).Width = 500;
            ws.Column(8).Style.NumberFormat.Format = "0.00";
            ws.Column(9).Style.NumberFormat.Format = "0.00";
            ws.Column(10).Style.NumberFormat.Format = "0.00%";
            ws.Column(11).Style.NumberFormat.Format = "0.00";

            ws.Columns(1, 11).AdjustToContents();
            var rngTable = ws.Range("A1:K1");
            // var rngHeaders = rngTable.Range("A2:I2");
            rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngTable.Style.Font.Bold = true;
            rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;

            var ws2 = wb.Worksheets.Add("X-Dropdowns").Hide();

            //Supplier List
            using (PurchaseOrderMgtDataContext po = new PurchaseOrderMgtDataContext())
            {
                var supplierList = from r in po.v_Vendors
                                   orderby r.ContractorName
                                   select r;

                string[] type = new string[3] { "Material", "Labour", "Misc" };

                var manufacturerList = ManufacturerBAL.GetManufacturerList();
                int sCount = 1;
                int tCount = 1;
                int mCount = 1;
                foreach (var item in supplierList)
                {
                    ws2.Cell("A" + sCount.ToString()).Value = item.ContractorName;
                    sCount++;
                }
                foreach (var item in type)
                {
                    ws2.Cell("B" + tCount.ToString()).Value = item;
                    tCount++;
                }
                foreach (var item in manufacturerList)
                {
                    ws2.Cell("E" + mCount.ToString()).Value = item.Name;
                    mCount++;
                }
                // Set drop down list for 500 rows and validation
                for (int a = 2; a < 502; a++)
                {
                    var cellWithtotal = ws.Cell(a, 9);
                    var cellWithGP = ws.Cell(a, 10);

                    // =IF((G2*H2)=0,"",(G2*H2))
                    string totalFormula = "=IF((G" + a + "*H" + a + ")=0,\"\",(G" + a + "*H" + a + "))";

                    //=IF((K2-IF(I2="",0,I2))<=0,"",((K2-I2))/I2)
                    string gpFormula = "=IF((K" + a + "-IF(I" + a + "=\"\",0,I" + a + "))<=0,\"\",((K" + a + "-I" + a + "))/I" + a + ")";

                    cellWithtotal.FormulaA1 = totalFormula;
                    cellWithGP.FormulaA1 = gpFormula;

                    // Supplier drop down
                    ws.Cell("D" + a.ToString()).DataValidation.List(ws2.Range("A1:A" + (sCount - 1).ToString()));
                    // type drop down
                    ws.Cell("C" + a.ToString()).DataValidation.List(ws2.Range("B1:B" + (tCount - 1).ToString()));

                    // manufacturer drop down
                    ws.Cell("E" + a.ToString()).DataValidation.List(ws2.Range("E1:E" + (mCount - 1).ToString()));

                    //// Qty validation
                    //ws.Cell(a, 6).DataValidation.Decimal.Between(0, 100000000);
                    //// Buying Price validation
                    //ws.Cell(a, 7).DataValidation.Decimal.Between(0, 100000000);
                    //// GP % validation
                    //ws.Cell(a, 9).DataValidation.Decimal.Between(0, 100);
                    //// Selling Price validation





                }
            }
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"BOM Template.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region BoM Upload
    protected void btnUploadData_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = fileUpload2.PostedFile.FileName;
            string Extension = Path.GetExtension(filePath);
            //Check the Extention of file
            if (string.IsNullOrEmpty(Extension))
            {
                lblUploadErrorMsg.Visible = true;
                lblUploadErrorMsg.ForeColor = System.Drawing.Color.Red;
                lblUploadErrorMsg.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                return;
            }
            if (IsValid(fileUpload2.PostedFile.FileName))
            {

                string path = Server.MapPath("WF\\UploadData\\BoM");
                string fileName = "\\" + fileUpload2.FileName;

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload2.SaveAs(path + fileName);
                string conStr = string.Empty;
                //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider= Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                        break;
                    case ".xlsx": //Excel 07
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                        break;

                }
                conStr = String.Format(conStr, path + fileName, "Yes");
                string[] sheetNames = GetExcelSheetNames(conStr);
                for (int i = 0; i < sheetNames.Length; i++)
                {
                    sheetNames[i] = sheetNames[i].Replace("$", string.Empty);
                    sheetNames[i] = sheetNames[i].Replace("'", string.Empty);
                }
                sheetNames = sheetNames.Where(s => s.ToLower() != "x-dropdowns").ToArray();

                List<ProjectMgt.Entity.ProjectBOM> projectBOMList = new List<ProjectMgt.Entity.ProjectBOM>();

                using (projectTaskDataContext db = new projectTaskDataContext())
                {
                    var bomTypeList = db.BOM_Types.Where(p => p.ProjectReference == QueryStringValues.Project).ToList();
                    var checkWorksheetNames = bomTypeList.Where(p => sheetNames.Contains(p.TypeName)).FirstOrDefault();
                    if (checkWorksheetNames == null)
                    {
                        lblUploadErrorMsg.Text = string.Empty;
                        using (PurchaseOrderMgtDataContext po = new PurchaseOrderMgtDataContext())
                        {
                            var supplierList = (from r in po.v_Vendors
                                                orderby r.ContractorName
                                                select r).ToList();
                            supplierList.Add(new POMgt.Entity.v_Vendor { VendorID = 0, ContractorName = "" });

                            var manufacturerList = ManufacturerBAL.GetManufacturerList().ToList();
                            manufacturerList.Add(new Manufacturer { Id = 0, Name = "" });
                            foreach (var item in sheetNames)
                            {
                                DataTable dataTable = Import_To_Grid(conStr, item);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataRow[] foundRows = dataTable.Select("Description <>''");
                                    int worksheetID = Deffinity.Worksheet.Worksheet_InsertUpdate(0, QueryStringValues.Project, item);

                                    for (int i = 0; i < foundRows.Count(); i++)
                                    {
                                        string description = dataTable.Rows[i][0].ToString();
                                        string partNumber = dataTable.Rows[i][1].ToString();
                                        string type = string.IsNullOrEmpty(dataTable.Rows[i][2].ToString()) ? "misc" : dataTable.Rows[i][2].ToString();
                                        string supplier = string.IsNullOrEmpty(dataTable.Rows[i][3].ToString()) ? "0" : dataTable.Rows[i][3].ToString();
                                        string manufacturer = string.IsNullOrEmpty(dataTable.Rows[i][4].ToString()) ? "0" : dataTable.Rows[i][4].ToString();
                                        string unit = dataTable.Rows[i][5].ToString();
                                        string qty = string.IsNullOrEmpty(dataTable.Rows[i][6].ToString()) ? "0" : dataTable.Rows[i][6].ToString();
                                        string buyingPrice = string.IsNullOrEmpty(dataTable.Rows[i][7].ToString()) ? "0" : dataTable.Rows[i][7].ToString();
                                        string gp = dataTable.Rows[i][9].ToString();
                                        string sellingPrice = string.IsNullOrEmpty(dataTable.Rows[i][10].ToString()) ? "0" : dataTable.Rows[i][10].ToString();
                                        double material, labour, misc;
                                        int supplierId = supplierList.Where(s => s.ContractorName.ToLower() == supplier.ToLower()).Select(s => s.VendorID).FirstOrDefault();
                                        int manufacturerId = manufacturerList.Where(m => m.Name.ToLower() == manufacturer.ToLower()).Select(m => m.Id).FirstOrDefault();
                                        TypeOfBoM(type, Convert.ToDouble(buyingPrice), out material, out labour, out misc);


                                        ProjectMgt.Entity.ProjectBOM projectBOM = new ProjectMgt.Entity.ProjectBOM();
                                        projectBOM.Description = description;
                                        projectBOM.PartNumber = partNumber;
                                        projectBOM.ProjectReference = QueryStringValues.Project;
                                        projectBOM.Supplier = supplierId;
                                        projectBOM.ManufactureID = manufacturerId;
                                        projectBOM.Unit = unit;
                                        projectBOM.WorkSheetID = worksheetID;
                                        projectBOM.Qty = Convert.ToDouble(qty);
                                        projectBOM.Material = material;
                                        projectBOM.Labour = labour;
                                        projectBOM.Mics = misc;
                                        projectBOM.CurrencyID = 54; // British Pound
                                        projectBOM.SellingTotal = Convert.ToDouble(sellingPrice);

                                        projectBOMList.Add(projectBOM);

                                    }
                                }

                            }
                        }
                        // bulk insert
                        db.ProjectBOMs.InsertAllOnSubmit(projectBOMList);
                        db.SubmitChanges();
                        Response.Redirect("~/WF/Projects/ProjectBOM.aspx?project=" + QueryStringValues.Project);
                    }
                    else
                    {
                        lblUploadErrorMsg.Text = "File not loaded - one or more tab names have previously been used. Please check and try again.";

                    }


                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void TypeOfBoM(string type, double buyingPrice, out double material, out double labour, out double misc)
    {
        material = 0; labour = 0; misc = 0;
        switch (type.ToLower())
        {
            case "material":
                {
                    material = buyingPrice;
                    break;
                }
            case "labour":
                {
                    labour = buyingPrice;
                    break;
                }
            case "misc":
                {
                    misc = buyingPrice;
                    break;
                }

        }
    }
    private DataTable Import_To_Grid(string conStr, string sheetName)
    {
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //string SheetName = "DealerVoice$";
        cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", sheetName + "$");
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        return dt;

    }
    public string[] GetExcelSheetNames(string connectionString)
    {
        OleDbConnection con = null; DataTable dt = null;
        String conStr = connectionString;
        con = new OleDbConnection(conStr);
        con.Close();
        con.Open();
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dt == null)
        {
            return null;
        }
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {

            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }

        con.Close();
        return excelSheetNames;
    }
    private bool IsValid(string fileName)
    {

        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {
            case ".xlsx":
                return true;
            case ".xls":
                return true;
            default:
                return false;
        }

    }
    #endregion
    protected void btnUpdateWorksheetData_Click(object sender, EventArgs e)
    {
        try
        {
            int save_worksheetID = Convert.ToInt32(ddlSavesWorkSheets.SelectedValue);
            int cur_worksheetid = Convert.ToInt32(ddlWorksheet.SelectedValue);
            if (save_worksheetID == 0)
            {
                lblSavedMsg.ForeColor = System.Drawing.Color.Red;
                lblSavedMsg.Text = "Please select saved worksheet";
                return;
            }
            if (cur_worksheetid == 0)
            {
                lblSavedMsg.ForeColor = System.Drawing.Color.Red;
                lblSavedMsg.Text = "Please select worksheet";
                return;
            }
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                BOM_TypesDUP bom = db.BOM_TypesDUPs.Where(p => p.ID == save_worksheetID).SingleOrDefault();
                bom.ProjectReference = QueryStringValues.Project;
                bom.worksheetid = cur_worksheetid;
                db.SubmitChanges();
                //delete all the items
                BOM_templateItems_deleteAll(save_worksheetID);
                //update all new items
                BOM_templateItems(cur_worksheetid, save_worksheetID);
                lblSavedMsg.ForeColor = System.Drawing.Color.Green;
                lblSavedMsg.Text = "Updated successfully.";
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GoodsReceiptSaving grSaving = null;
            using (projectTaskDataContext BOM = new projectTaskDataContext())
            {
                GoodsReceiptSaving grSaving1 = BOM.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project
                    && a.BOMId == int.Parse(lblProjectBOMId.Text) && a.S_type == "Qty").FirstOrDefault();
                if (grSaving1 == null)
                {
                    grSaving = new GoodsReceiptSaving();
                    grSaving.projectRef = QueryStringValues.Project;
                    grSaving.BOMId = int.Parse(lblProjectBOMId.Text);
                    grSaving.BudgetQty = int.Parse(txtbudgetQty.Text);
                    grSaving.ActualQty = int.Parse(txtActualReq.Text);
                    grSaving.Userid = sessionKeys.UID;
                    grSaving.DateModified = DateTime.Now;
                    grSaving.S_type = "Qty";
                    BOM.GoodsReceiptSavings.InsertOnSubmit(grSaving);
                    BOM.SubmitChanges();
                }
                else
                {
                    grSaving1.Userid = sessionKeys.UID;
                    grSaving1.DateModified = DateTime.Now;
                    grSaving1.S_type = "Qty";
                    grSaving1.ActualQty = int.Parse(txtActualReq.Text);
                    BOM.SubmitChanges();
                }
                BindGrid(int.Parse(ddlWorksheet.SelectedValue));
                mdlpopupinGridToSave.Hide();
                Response.Redirect(Request.RawUrl);
                lblError.Text = "Updated successfully.";
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {

        BindGrid(int.Parse(ddlWorksheet.SelectedValue));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectPipeline.aspx?Status=2");
    }

    protected void linkRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/ProjectBOMprint.aspx?id=" + ddlWorksheet.SelectedValue + "&project=" + QueryStringValues.Project);
    }

    protected void BtnSnapShot_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBomSnapShotBAL PBAl_Snap = new ProjectBomSnapShotBAL();
            PBAl_Snap.InsertDataIntoJournal(QueryStringValues.Project, "Snapshot - " + DateTime.Now.ToString(), sessionKeys.UID);
            BindSnapsDll(QueryStringValues.Project);
            BindWorksheetDll();
            //snap saved successfully;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindSnapShotGrid(int worksheetID, int SnapId)
    {
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var bomList = (from r in BOM.v_ProjectBOMJournals
                           where (r.ProjectReference == QueryStringValues.Project && r.SnapId == SnapId &&
                           r.WorkSheetID == worksheetID)
                           select r).ToList();

            GridSnapShots.DataSource = bomList;
            GridSnapShots.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindSnapsDll(int projectId)
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                var Snapslist = (from a in Pdc.ProjectSnapShots
                                 where a.ProjectID == QueryStringValues.Project
                                 orderby a.Id descending
                                 select new
                                 {
                                     Id = a.Id,
                                     Name = a.Name
                                 }).ToList();
                ddlSnapshot.DataSource = Snapslist;
                ddlSnapshot.DataValueField = "Id";
                ddlSnapshot.DataTextField = "Name";
                ddlSnapshot.DataBind();
                ddlSnapshot.Items.Insert(0, new ListItem("Please select...", "0"));
                if (ddlSnapshot.Items.Count > 1)
                {
                    ddlSnapshot.SelectedIndex = 1;
                    PnlSnapShot.Visible = true;
                }
                else
                {
                    PnlSnapShot.Visible = false;
                }
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindWorksheetDll()
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {

                var Wlist1 = (from a in Pdc.BOM_TypesJournals
                              where a.SnapId == int.Parse(ddlSnapshot.SelectedValue) && a.ProjectReference == QueryStringValues.Project
                              orderby a.TypeName
                              select new
                              {
                                  Id = a.BOM_TypeId,
                                  name = a.IsDeleted.HasValue ? (a.IsDeleted.Value == true ? a.TypeName + " (deleted)" : a.TypeName) : a.TypeName
                              }).ToList();

                ddlWorksheetName.DataSource = Wlist1;
                ddlWorksheetName.DataValueField = "Id";
                ddlWorksheetName.DataTextField = "name";
                ddlWorksheetName.DataBind();
                ddlWorksheetName.Items.Insert(0, new ListItem("Please select...", "0"));
                ddlWorksheetName.SelectedIndex = 1;
                BindSnapShotGrid(int.Parse(ddlWorksheetName.SelectedValue), int.Parse(ddlSnapshot.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlSnapshot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSnapshot.SelectedValue != "0")
        {
            BindWorksheetDll();
            BindSnapShotGrid(int.Parse(ddlWorksheetName.SelectedValue), int.Parse(ddlSnapshot.SelectedValue));
        }
        else
        {
            ddlWorksheetName.Items.Clear();
            ddlWorksheetName.Items.Insert(0, new ListItem("Please select...", "0"));
            GridSnapShots.DataBind();
        }
        //System.Web.Optimization.Scripts.Render("~/bundles/grid");
    }
    protected void ddlWorksheetName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSnapShotGrid(int.Parse(ddlWorksheetName.SelectedValue), int.Parse(ddlSnapshot.SelectedValue));
    }
    protected void GridSnapShots_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //RowDataChanged
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            v_ProjectBOMJournal de = (v_ProjectBOMJournal)e.Row.DataItem;
            if (de.RowDataChanged == 0)
            {
                LinkButton Imgbtn = (LinkButton)e.Row.FindControl("ImgSave");
                Imgbtn.Visible = false;
            }
        }
    }
    protected string GetPurchasedQtyInSnapGrid(string ID, string SnapId)
    {
        string Qty = "0";
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var getValues = (from r in BOM.GoodsReceiptJournal1s
                             where r.BOMID == int.Parse(ID) && r.SnapId == int.Parse(SnapId)
                             select r).FirstOrDefault();
            if (getValues != null)
            {
                Qty = getValues.QtyReceived.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Qty;
    }

   
   
}