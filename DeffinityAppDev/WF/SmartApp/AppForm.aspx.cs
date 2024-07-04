using HealthCheckMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using System.Data;
using UserMgt.BAL;
using UserMgt.DAL;
using System.IO;
using Location.DAL;
using DC.DAL;
using System.Web.UI.HtmlControls;

public partial class App_AppForm : System.Web.UI.Page
{
    HealthCheckBAL hb;
    public string section = "app";
    protected void Page_Load(object sender, EventArgs e)
    {
        int healthCheckListID = 0;
        pnlFormData.Visible = true;
        hPermission.Value = GetPermissionID().ToString();
        if (Request.QueryString["appformid"] != null)
        {
            healthCheckListID = Convert.ToInt32(Request.QueryString["appformid"]);
            //PermissionsChecking();
            
        }

        if (!IsPostBack)
        {
            //Master.PageHead = "Form";
            //PermissionsChecking();
            using (UserDataContext Udc = new UserDataContext())
            {
                List<UserMgt.Entity.Contractor> Clist = Udc.Contractors.ToList();
                BindChildRecordsGrid(Clist);
            }
        }
        try
        {
            BindControls(healthCheckListID, "Parent");
            if (lblchildFormId.Text != string.Empty)
            {
                BindControls(int.Parse(lblchildFormId.Text), "child");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        PermissionsToButtons();
    }
    public void PermissionsToButtons()
    {
        if(!ApplyPermission())
        {
            btnSubmitChanges.Visible = false;
            BtnFileUpload.Visible = false;
            imgSaveChildForm.Visible = false;
            btnPopUp.Visible = false;
        }
        else
        {
            btnSubmitChanges.Visible = true;
            BtnFileUpload.Visible = true;
            imgSaveChildForm.Visible = true;
            btnPopUp.Visible = true;
        }
    }
    public bool ApplyPermission()
    {
        bool retval = true;
        //2 - Readonly
        if (hPermission.Value == "2")
        {
            retval = false;
        }

        return retval;
    }
    public int GetPermissionID()
    {
        int i = 0;
        using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
        {
            App_Permission x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID).FirstOrDefault();
            int appid = 0;
            int AppFormID = 0;
            if (Request.QueryString["AppFormID"] != null)
            {
                AppFormID = Convert.ToInt32(Request.QueryString["AppFormID"]);
                appid = Ddc.AppManagerAssignedForms.Where(a => a.ID == AppFormID).Select(a => a.AppID.HasValue ? a.AppID.Value : 0).FirstOrDefault();
                x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID && a.AppId == appid).FirstOrDefault();
            }
            if (x != null)
            {
                if (x.PermissionId == 1)//Administrator
                {
                    i = 1;
                }
                else if (x.PermissionId == 2)//Read Only
                {
                    //btnSubmitChanges.Visible = false;
                    i = 2;
                }
                else if (x.PermissionId == 3)//Hide
                {
                    i = 3;
                }
                else if (x.PermissionId == 4)//Manage
                {
                    i = 4;
                }
            }
        }
        return i;
    }
    public int PermissionsChecking()
    {
        int i = 0;
        using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
        {
            App_Permission x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID).FirstOrDefault();
            int appid = 0;
            int AppFormID = 0;
            if (Request.QueryString["AppFormID"] != null)
            {
                AppFormID = Convert.ToInt32(Request.QueryString["AppFormID"]);
                appid = Ddc.AppManagerAssignedForms.Where(a => a.ID == AppFormID).Select(a => a.AppID.HasValue ? a.AppID.Value : 0).FirstOrDefault();
                x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID && a.AppId == appid).FirstOrDefault();
            }
            if (x != null)
            {
                if (x.PermissionId == 1)//Administrator
                {
                    i = 1;
                }
                else if (x.PermissionId == 2)//Read Only
                {
                    btnSubmitChanges.Visible = false;
                    i = 2;
                }
                else if (x.PermissionId == 3)//Hide
                {
                    i = 3;
                }
                else if (x.PermissionId == 4)//Manage
                {
                    i = 4;
                }
            }
        }
        return i;
    }
    public void BindControls(int HealthCheckID,string type)
    {
        try
        {
            int temp = 1;
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            HealthCheckBAL hbal = new HealthCheckBAL();
            HealthCheckMgt.Entity.AppManagerAssignedForm hf = new HealthCheckMgt.Entity.AppManagerAssignedForm();
            int formid = 0;
            using (HealthCheckDataContext hcd = new HealthCheckDataContext())
            {
                if (type == "Parent")
                {
                    hf = hcd.AppManagerAssignedForms.Where(o => o.ID == HealthCheckID).FirstOrDefault();
                    lblTitle.InnerText = hf.FormName;
                    formid = hf.AppManager.FormID.Value;
                }
                else
                {
                    int AppFormID = 0;
                    if (Request.QueryString["AppFormID"] != null)
                    {
                        AppFormID = Convert.ToInt32(Request.QueryString["AppFormID"]);
                    }
                    hf = hcd.AppManagerAssignedForms.Where(o => o.ID == AppFormID).FirstOrDefault();
                    lblTitle.InnerText = hf.FormName;
                    formid = hf.AppManager.ChildFormId.Value;
                  //  formid = HealthCheckID;//hf.AppManager.ChildFormId.HasValue ? hf.AppManager.ChildFormId.Value : 0;
                }
                if (hf.AppManager.Type == "Flatfile")
                {
                    Child1.Visible = false;
                }
                link_return.NavigateUrl = "~/WF/SmartApp/AppFormList.aspx?Appid=" + hf.AppID.ToString();
                link_return.Text = string.Format("<span>Return to {0} list</span>", hf.AppManager.Name);
            }

            //var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
            //int formid = hcData.PortfolioHealthCheckID.Value;
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID, section);
            //start table
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "98%");
            tbl.Style.Add("background-color", hform.FormBackColor);
            tbl.CssClass = "tblform";
            //check header is exists
            var pHeader = hpanels.Where(o => o.PanelName == "Header").FirstOrDefault();
            TableRow tr;
            TableCell td;
            TableHeaderRow th_row;
            TableHeaderCell th_cell;
            LiteralControl lc;
            Image img;
            Table pnltbl;
            RequiredFieldValidator rf;
            if (pHeader != null)
            {
                pnltbl = new Table();
                pnltbl.Style.Add("width", "100%");
                pnltbl.Style.Add("background-color", pHeader.PanelBackColor);
                pnltbl.CssClass = "tblheader";
                // var td = null;
                for (int row = 1; row <= pHeader.PanelRows; row++)
                {
                    tr = new TableRow();
                    var colCnt = pHeader.PanelColumns;
                    for (int col = 1; col <= pHeader.PanelColumns; col++)
                    {
                        td = new TableCell();
                        td.Style.Add("width", (100 / colCnt).ToString() + "%");
                        var cval = hcontrols.Where(o => o.PanelID == pHeader.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                        if (cval != null)
                        {

                            if (!string.IsNullOrEmpty(cval.ControlValue))
                            {
                                img = new Image();
                                img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                if (col == 2)
                                {
                                    img.Style.Add("float", "right");
                                }
                                td.Controls.Add(img);
                            }
                            else
                            {
                                var lblHead = new Label();
                                lblHead.Text = string.Empty;
                                td.Controls.Add(lblHead);

                            }
                        }
                        //add image
                        tr.Cells.Add(td);

                    }
                    pnltbl.Rows.Add(tr);
                }
                // ph.Controls.Add(tbl);
                tr = new TableRow();
                td = new TableCell();
                td.Controls.Add(pnltbl);
                tr.Cells.Add(td);
                tbl.Controls.Add(tr);
            }

            //get list of existing panels
            var plist = hpanels.Where(o => o.PanelName != "Header" && o.PanelName != "Signature Panel").OrderBy(a => a.PnlPosition).ToList();
            foreach (var pnl in plist)
            {
                if (pnl != null)
                {
                    var LblPanel = new Label();
                    LblPanel.Font.Bold = true;
                    LblPanel.Text = pnl.PanelName;
                    LblPanel.Font.Size = 10;

                    pnltbl = new Table();
                    pnltbl.Style.Add("width", "100%");
                    pnltbl.Style.Add("background-color", pnl.PanelBackColor);
                    pnltbl.CssClass = "tblcontrol";
                    // var td = null;
                    th_row = new TableHeaderRow();
                    th_cell = new TableHeaderCell();
                    th_cell.HorizontalAlign = HorizontalAlign.Left;
                    th_cell.Controls.Add(LblPanel);
                    th_row.Cells.Add(th_cell);
                    pnltbl.Controls.Add(th_row);
                    
                    for (int row = 1; row <= pnl.PanelRows; row++)
                    {
                        tr = new TableRow();
                        var colCnt = pnl.PanelColumns;
                        for (int col = 1; col <= pnl.PanelColumns; col++)
                        {
                            var cval = hcontrols.Where(o => o.PanelID == pnl.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();

                            if (cval != null)
                            {
                                var cdata = hControlValues.Where(o => o.ControlID == cval.ControlID).FirstOrDefault();
                                td = new TableCell();
                                if (cval.TypeOfField.ToLower() == "label")
                                {
                                    temp++;
                                    td.Controls.Add(GenerateLable((cval.ControlID+ temp).ToString(), cval.ControlLabelName, cval.ControlWidth, cval.Height, cval.ControlPosition, cval.MinValue, cval.MaxValue));
                                    temp++;
                                }
                                else
                                {
                                    td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                    lc = new LiteralControl(cval.ControlLabelName + " <br/>");
                                    td.Controls.Add(lc);
                                }
                                
                                //if (cval.TypeOfField.ToLower() == "textbox")
                                //{

                                //    td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), cval.DefaultText,(cdata != null ?cdata.ControlValue:string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}
                                //else if (cval.TypeOfField.ToLower() == "dropdown")
                                //{
                                //    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}
                                //else if (cval.TypeOfField.ToLower() == "checkbox")
                                //{
                                //    td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}

                                if (cval.TypeOfField.ToLower() == "date")
                                {
                                    td.Controls.Add(GenerateTextboxDate(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    //{
                                    //    rf = Add_validation(cval);
                                    //    td.Controls.Add(rf);
                                    //}
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);

                                    //image for Tooltip
                                }
                                else if (cval.TypeOfField.ToLower() == "dropdown")
                                {
                                    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cval.DefaultText));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_dropdown_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "checkbox")
                                {
                                    td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "textarea")
                                {
                                    td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "label")
                                {
                                    temp++;
                                    td.Controls.Add(GenerateLable((cval.ControlID + temp).ToString(), cval.DefaultText, cval.ControlWidth, cval.Height, cval.ControlPosition, cval.MinValue, cval.MaxValue));
                                    temp++;
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "image")
                                {
                                    if (cdata != null)
                                    {
                                        if (!string.IsNullOrEmpty(cdata.ControlValue))
                                        {
                                            img = new Image();
                                            img.ID = cdata.ControlValue.ToString();
                                            img.ImageUrl = "~/WF/UploadData/HC/" + cdata.ControlValue + ".png";
                                            img.EnableViewState = true;
                                            img.Style.Add("float", string.IsNullOrEmpty(cval.CblPosition) ? "left" : cval.CblPosition);
                                            img.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.Value.ToString() + "%" : "100%");
                                            //img.Style.Add("width", "25%");
                                            //img.Style.Add("height", "25%");
                                            td.Controls.Add(img);
                                            //   td.Controls.Add(GenerateFileupload(cval.ControlID.ToString()));
                                            tr.Cells.Add(td);
                                        }
                                    }
                                    else
                                    {

                                        img = new Image();
                                        if (!string.IsNullOrEmpty(cval.ControlValue))
                                        {
                                            img.ID = cval.ControlValue.ToString();
                                            img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                            img.EnableViewState = true;
                                            img.Style.Add("float", string.IsNullOrEmpty(cval.CblPosition) ? "left" : cval.CblPosition);
                                            img.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.Value.ToString() + "%" : "100%");
                                            td.Controls.Add(img);
                                            tr.Cells.Add(td);
                                        }
                                    }
                                }
                                else if (cval.TypeOfField.ToLower() == "checkboxlist")
                                {
                                    td.Controls.Add(GenerateChecklistbox(cval.DefaultText, cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "table")
                                {
                                    int colLength = cval.columnlist.Split(',').Length;
                                    int RowLength = cval.ListValues.Split(',').Length;
                                    td.Controls.Add(GenerateTable(row, col, cval.TypeofFieldInTbl, cval.ControlID.ToString(), cval.ControlWidth, cval.ListValues.ToString(), cval.columnlist.ToString(), cval.PanelID));
                                    tr.Cells.Add(td);
                                }
                                if (!string.IsNullOrEmpty(cval.Helptext))
                                {
                                    var span = new HtmlGenericControl("span");
                                    span.ID = type + cval.ControlID.ToString();
                                    span.Attributes["title"] = cval.Helptext;
                                    span.Attributes["class"] = "fa-info";
                                    span.Attributes["style"] = "padding-left:10px;font-size:medium;vertical-align:-webkit-baseline-middle";
                                    td.Controls.Add(span);
                                }
                           }
                        }
                        pnltbl.Rows.Add(tr);
                    }
                    tr = new TableRow();
                    td = new TableCell();

                    td.Controls.Add(pnltbl);
                    tr.Cells.Add(td);
                    tbl.Controls.Add(tr);
                }
            }
            //get signature panel
            var SignatureList = hpanels.Where(o => o.PanelName == "Signature Panel").ToList();
            foreach (var Signature in SignatureList)
            {
                if (Signature != null)
                {
                    pnltbl = new Table();
                    pnltbl.Style.Add("width", "100%");
                    pnltbl.Style.Add("background-color", Signature.PanelBackColor);
                    pnltbl.CssClass = "tblcontrol";
                    pnltbl.CellPadding = 8;
                    pnltbl.CellSpacing = 3;
                    for (int row = 1; row <= Signature.PanelRows; row++)
                    {
                        tr = new TableRow();
                        var colCnt = Signature.PanelColumns;
                        for (int col = 1; col <= Signature.PanelColumns; col++)
                        {
                            td = new TableCell();
                            td.Style.Add("width", (100 / colCnt).ToString() + "%");
                            var cval = hcontrols.Where(o => o.PanelID == Signature.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                            if (cval != null)
                            {
                                var Sdata = hControlValues.Where(o => o.ControlID == cval.ControlID).FirstOrDefault();
                                temp++;
                                td.Controls.Add(GenerateLable((cval.ControlID+temp).ToString(), cval.ControlLabelName, 10, 22, cval.ControlPosition,cval.MinValue,cval.MaxValue));
                                temp++;
                                tr.Cells.Add(td);
                                if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    if (!string.IsNullOrEmpty(cval.TypeOfField))
                                    {
                                        var txt = new TextBox();
                                        txt.ID = cval.ControlID.ToString();
                                        txt.Text = Sdata != null ? Sdata.ControlValue : string.Empty;
                                        if (Sdata != null)
                                        {
                                            //if (Sdata.ControlValue != "")
                                            //{
                                            //    txt.ReadOnly = true;
                                            //}
                                        }
                                        txt.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.ToString() + "%" : "100%");
                                        txt.Style.Add("float", "left");

                                        td.Controls.Add(txt);
                                        if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                        {
                                            rf = Add_validation(cval);
                                            //txtid = txtid + 1;
                                            td.Controls.Add(rf);
                                        }
                                        tr.Cells.Add(td);
                                    }
                                }
                            }
                        }
                        pnltbl.Rows.Add(tr);
                    }
                    tr = new TableRow();
                    td = new TableCell();
                    td.Controls.Add(pnltbl);
                    tr.Cells.Add(td);
                    tbl.Controls.Add(tr);
                }

            }
            if (type == "Parent")
            {
                phMainForm.Controls.Add(tbl);
            }
            else 
            {
                PhChild.Controls.Add(tbl);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public Label GeneratelblForSpace(int Id)
    {
        Label L1 = new Label();
        L1.ID = "LblSpace" + Id.ToString();
        L1.Text = " ";
        return L1;
    }
    public Image GenerateImageForToolTip(string ToolTip,int Id)
    {
        Image img = new Image();
        try
        {

            img.ID = "Img" + Id.ToString();
            img.ImageUrl = "~/media/ico_info.gif";
            img.EnableViewState = true;
            img.ToolTip = ToolTip;
            img.CssClass = "Imagespace";
          //  img.ImageAlign = System.Drawing.ContentAlignment.BottomLeft; 
          //  img.Style.Add("float", string.IsNullOrEmpty(cval.CblPosition) ? "left" : cval.CblPosition);
          //  img.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.Value.ToString() + "%" : "100%");
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return img;
    }

    public TextBox GenerateTextboxDate(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.CssClass = "dateclass";
        txt.Style.Add("width", "125px");
        // txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    Label lbl1 = null;
    public Label GenerateLableForSignaturePnl(string id, string lblName, string ControlPosition, int? width, int? Height)
    {
        lbl1 = new Label();
        lbl1.ID = "lbl" + id.ToString();
        lbl1.Text = lblName;
        lbl1.EnableViewState = true;
        lbl1.Style.Add("float", string.IsNullOrEmpty(ControlPosition) ? "left" : ControlPosition);
        lbl1.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        lbl1.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        return lbl1;
    }
    private static RequiredFieldValidator Add_validation(HealthCheckMgt.Entity.HealthCheck_FormControl cval)
    {
        RequiredFieldValidator rf;
        rf = new RequiredFieldValidator();
        rf.EnableViewState = true;
        rf.ControlToValidate = cval.ControlID.ToString();
        rf.ErrorMessage = "Please enter " + cval.ControlLabelName;
        rf.Text = "*";
        rf.ValidationGroup = "Form";
        return rf;
    }
    private static RequiredFieldValidator Add_dropdown_validation(HealthCheckMgt.Entity.HealthCheck_FormControl cval)
    {
        RequiredFieldValidator rf;
        rf = new RequiredFieldValidator();
        rf.EnableViewState = true;
        rf.ControlToValidate = cval.ControlID.ToString();
        rf.ErrorMessage = "Please select " + cval.ControlLabelName;
        rf.Text = "*";
        rf.InitialValue = "0";
        rf.ValidationGroup = "Form";
        return rf;
    }

    Label lbl = null;
    public Label GenerateLable(string id, string lblName, int? width, int? Height, string position,string fontbold,string fontsize)
    {
        lbl = new Label();
        //lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //    lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        if (!string.IsNullOrEmpty(fontsize))
        {
            lbl.Style.Add("font-size", fontsize + "px");
        }
        //font bold
        if (!string.IsNullOrEmpty(fontbold))
        {
            if (fontbold.ToLower() == "true")
            {
                lbl.Style.Add("font-weight", "bold");
            }
        }
        lbl.EnableViewState = true;
        return lbl;
    }
    TextBox txt;
    public TextBox GenerateTextarea(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.TextMode = TextBoxMode.MultiLine;
        txt.Height = 70;
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;

        return txt;
    }
    public TextBox GenerateTextbox(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        // txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    CheckBoxList chl;
    public CheckBoxList GenerateChecklistbox(string DeafultColList, string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        chl = new CheckBoxList();
        try
        {
            chl.ID = id;
            //ddl.Width = 200;
            chl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            chl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            chl.RepeatLayout = RepeatLayout.Table;
            chl.RepeatDirection = RepeatDirection.Vertical;
            int num1 = 2;

            if (int.TryParse(DeafultColList, out num1))
            {

            }
            chl.RepeatColumns = num1;
            //if (DeafultColList != string.Empty)
            //{
            //    if (int.TryParse(DeafultColList))
            //    chl.RepeatColumns = int.type.Parse(DeafultColList);
            //}
            //else
            //{
            //    chl.RepeatColumns = 2;
            //}
            chl.CellPadding = 5;
            chl.CellSpacing = 3;
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                chl.Items.Add(s);
            //set default values
            if (!string.IsNullOrEmpty(setvalue))
            {
                var srtsplit1 = setvalue.Split(',');
                var tcont = srtsplit1.Count();
                if (tcont > 0)
                {
                    var i = 0;
                    foreach (ListItem li in chl.Items)
                    {
                        if (i <= tcont)
                            li.Selected = Convert.ToBoolean(((srtsplit1[i] == null) ? "0" : srtsplit1[i]) == "1" ? "True" : "False");
                        i = i + 1;
                    }
                }
            }
            chl.EnableViewState = true;
            chl.SelectedIndexChanged += chk_SelectedIndexChanged;
            chl.AutoPostBack = true;
            if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            {
                //set updated values
                var srtsplit = setvalue.Split(',');
                var tcont1 = srtsplit.Count();
                if (srtsplit.Count() > 0)
                {
                    var j = 0;
                    foreach (ListItem li in chl.Items)
                    {
                        if (j <= tcont1)
                            li.Selected = Convert.ToBoolean(((srtsplit[j] == null) ? "0" : srtsplit[j]) == "1" ? "True" : "False");
                        j = j + 1;
                    }
                }
                //chl.SelectedValue = setvalue;
            }
            //ddlid = ddlid + 1;
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        return chl;
    }
    DropDownList ddl;
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height, string DdlDataType)
    {
        ddl = new DropDownList();
        try
        {
            ddl.ID = id;
            //ddl.Width = 200;
            ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  ddl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            ddl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            if (!string.IsNullOrEmpty(Items) && string.IsNullOrEmpty(DdlDataType))
            {
                string[] str = Items.Split(',').ToArray();
                foreach (string s in str)
                    ddl.Items.Add(s);
            }
            else
            {
                if (!string.IsNullOrEmpty(setvalue))
                {
                    int i = setvalue.LastIndexOf('$');
                    setvalue = setvalue.Substring(i + 1);
                }
                    if (DdlDataType == "List of Project Managers")
                    {
                        using (UserDataContext Udc = new UserDataContext())
                        {
                            var ProjectManagersList = (from a in Udc.Contractors
                                                       where (a.SID == 1 || a.SID == 2 || a.SID == 3) && (a.Status == "Active")
                                                       select new
                                                       {
                                                           Name = a.ContractorName,
                                                           Value = a.ID
                                                       }).ToList();
                            ddl.DataSource = ProjectManagersList;
                        }
                    }
                    else if (DdlDataType == "List of Customer Sites")
                    {
                        using (LocationDataContext Ldc = new LocationDataContext())
                        {
                            var OurCustomerSitesList = (from a in Ldc.Sites
                                                        where a.Visible == 'Y'
                                                        select new
                                                        {
                                                            Name = a.Site1,
                                                            Value = a.ID
                                                        }).ToList();
                            ddl.DataSource = OurCustomerSitesList;
                        }
                    }
                    else if (DdlDataType == "List of Our Sites")
                    {
                        using (DCDataContext Dc = new DCDataContext())
                        {
                            var OurSitesList = (from a in Dc.OurSites
                                                select new
                                                {
                                                    Name = a.Name,
                                                    Value = a.ID
                                                }).ToList();
                            if (sessionKeys.PortfolioID > 0)
                            {
                                OurSitesList = (from a in Dc.OurSites
                                                where a.CustomerID == sessionKeys.PortfolioID
                                                select new
                                                {
                                                    Name = a.Name,
                                                    Value = a.ID
                                                }).ToList();
                            }
                            ddl.DataSource = OurSitesList;
                        }
                    }
                    else if (DdlDataType == "List of Resources")
                    {
                        using (UserDataContext Udc = new UserDataContext())
                        {
                            var ResourcesList = (from a in Udc.Contractors
                                                 where (a.SID == 4 || a.SID == 9) && (a.Status == "Active")
                                                 select new
                                                 {
                                                     Name = a.ContractorName,
                                                     Value = a.ID
                                                 }).ToList();
                            ddl.DataSource = ResourcesList;
                        }
                    }
                    else if (DdlDataType == "List of Administrators")
                    {
                        using (UserDataContext Udc = new UserDataContext())
                        {
                            var AdministratorsList = (from a in Udc.Contractors
                                                      where a.SID == 1 && a.Status == "Active"
                                                      select new
                                                      {
                                                          Name = a.ContractorName,
                                                          Value = a.ID
                                                      }).ToList();
                            ddl.DataSource = AdministratorsList;
                        }
                    }
                    ddl.DataValueField = "Value";
                    ddl.DataTextField = "Name";
                    ddl.DataBind();
                }
            
            ddl.Items.Insert(0, new ListItem("Please select...", "0"));
            ddl.SelectedValue = setvalue;
            ddl.EnableViewState = true;
            ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            ddl.AutoPostBack = true;
            if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
                ddl.SelectedValue = setvalue;
            //ddlid = ddlid + 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ddl;
    }
    public void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (DropDownList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
    }

    public void chk_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cval = (CheckBoxList)sender;
        if (cval.SelectedIndex > 0)
        {
            string s = cval.SelectedValue;
        }
    }

    CheckBox chk;
    public CheckBox GenerateCheckBox(string id, string setvalue, bool Isfirsttime)
    {
        chk = new CheckBox();
        chk.ID = id;
        //txt.Width = 200;
        //chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue)?"False":setvalue);
        chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue) ? "False" : setvalue);
        chk.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue) ? "False" : setvalue);
        //txtid = txtid + 1;
        return chk;
    }

    Table t = null;
    TableRow trow = null;
    TableCell tc = null;
    TableHeaderRow HeadR = null;
    TableHeaderCell thc = null;
    public Table GenerateTable(int? row, int? col, string typeoffieldInTbl, string id, int? width, string Rlist, string Clist, int? PanelId)
    {
        string[] Rstr = Rlist.Split(',').ToArray();
        string[] Cstr = Clist.Split(',').ToArray();
        int r = Rlist.Split(',').Length;
        int c = Clist.Split(',').Length;

        t = new Table();
        trow = new TableRow();
        tc = new TableCell();

        HeadR = new TableHeaderRow();
        for (int HList = 0; HList <= c; HList++)
        {
            if (HList != 0)
            {
                thc = new TableHeaderCell();
                var l = new Label();
                l.ID = HList.ToString() + "" + id.ToString();
                l.Text = Cstr[HList - 1].ToString();
                thc.Controls.Add(l);
                HeadR.Cells.Add(thc);
                t.Rows.Add(HeadR);
            }
            else
            {
                thc = new TableHeaderCell();
                var l1 = new Label();
                l1.ID = HList.ToString() + "" + id.ToString();
                l1.Text = "";
                thc.Controls.Add(l1);
                HeadR.Cells.Add(thc);
                t.Rows.Add(HeadR);
            }
        }
        HealthCheckBAL hbal = new HealthCheckBAL();
        int HealthCheckID = QueryStringValues.CallID;
        var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
        var formid = hcData.FormID.Value;

        var Tblcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
        var TblCntlList = Tblcontrols.Where(o => o.PanelID == PanelId && o.RowIndex == row && o.ColumnIndex == col).ToList();
        // int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
        var TblControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID, section);
        int count = 0;
        for (int i = 1; i <= r; i++)
        {
            trow = new TableRow();
            for (int j = 0; j <= c; j++)
            {
                if (j != 0)
                {
                    var TblCntldata = TblControlValues.Where(o => o.ControlID == TblCntlList[count].ControlID).FirstOrDefault();
                    tc = new TableCell();
                    if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "textbox")
                    {
                        var txt = new TextBox();
                        txt.ID = TblCntlList[count].ControlID.ToString();
                        txt.Text = TblCntldata != null ? TblCntldata.ControlValue : string.Empty;
                        txt.Style.Add("width", width.HasValue ? width.ToString() + "%" : "100%");
                        txt.Style.Add("float", "right");
                        tc.Controls.Add(txt);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "checkbox")
                    {
                        var checkBox = new CheckBox();
                        checkBox.ID = TblCntlList[count].ControlID.ToString();
                        checkBox.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
                        checkBox.Style.Add("float", "right");
                        tc.Controls.Add(checkBox);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "radiobutton")
                    {
                        var Rbtn = new RadioButton();
                        Rbtn.ID = TblCntlList[count].ControlID.ToString();
                        Rbtn.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
                        Rbtn.Style.Add("float", "right");
                        tc.Controls.Add(Rbtn);
                        trow.Cells.Add(tc);
                    }
                    count = count + 1;
                }
                else
                {
                    tc = new TableCell();
                    var lb = new Label();
                    lb.ID = i.ToString() + "" + j.ToString() + "" + id.ToString();
                    lb.Text = Rstr[i - 1].ToString();
                    tc.Controls.Add(lb);
                    trow.Cells.Add(tc);
                }
            }
            t.Rows.Add(trow);
        }
        return t;
    }

    public void SavePlaceholderData(int HealthCheckID, PlaceHolder ph)
    {
        try
        {
            ViewState["state"] = "2";

            HealthCheckBAL hbal = new HealthCheckBAL();
            //var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
            //int formid = hcData.FormID.Value;
            int formid = 0;
            HealthCheckMgt.Entity.AppManagerAssignedForm hf = new HealthCheckMgt.Entity.AppManagerAssignedForm();
            using (HealthCheckDataContext hcd = new HealthCheckDataContext())
            {
                if (ph.ID != "PhChild")
                {
                    hf = hcd.AppManagerAssignedForms.Where(o => o.ID == HealthCheckID).FirstOrDefault();
                    lblTitle.InnerText = hf.FormName;
                    formid = hf.AppManager.FormID.Value;
                }
                else
                {
                    int AppFormID = 0;
                    if (Request.QueryString["AppFormID"] != null)
                    {
                        AppFormID = Convert.ToInt32(Request.QueryString["AppFormID"]);
                    }
                    hf = hcd.AppManagerAssignedForms.Where(o => o.ID == AppFormID).FirstOrDefault();
                    lblTitle.InnerText = hf.FormName;
                    formid = hf.AppManager.ChildFormId.Value;
                }
            }
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID, section);

            //pb = new ProjectAdditionalInfoBAL();
            //cb = new CustomFieldsBAL();
            //List<CustomField> clist = cb.CustomFields_SelectAll().ToList();

            foreach (HealthCheckMgt.Entity.HealthCheck_FormControl c in hcontrols.Where(o => o.TypeOfField.ToLower() != "fileupload"))
            {
                var cVal = hControlValues.Where(p => p.ControlID == c.ControlID).FirstOrDefault();
                if (cVal == null)
                {
                    cVal = new HealthCheckMgt.Entity.HealthCheck_FormData();
                    cVal.HealthCheckID = HealthCheckID;

                    if (c.TypeOfField.ToLower() == "date")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }

                    }
                    else if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }

                    }
                    else if (c.TypeOfField.ToLower() == "image")
                    {
                        var Image = ph.FindControl(c.ControlID.ToString()) as Image;
                        if (Image != null)
                        {
                            cVal.ControlValue = Image.ID;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "dropdown")
                    {
                        var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                        if (ddl != null)
                        {
                            if (ddl.SelectedValue != "0")
                            {
                                if (!string.IsNullOrEmpty(c.DefaultText))
                                {
                                    cVal.ControlValue = ddl.SelectedItem.Text + "$" + ddl.SelectedValue;
                                }
                                else
                                {
                                    cVal.ControlValue = ddl.SelectedValue;
                                }
                            }
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        var chk = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                        if (chk != null)
                        {
                            cVal.ControlValue = chk.Checked.ToString();
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkboxlist")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as CheckBoxList;
                        var chkValues = string.Empty;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                    chkValues = chkValues + "1,";
                                else
                                    chkValues = chkValues + "0,";
                            }
                            cVal.ControlValue = chkValues;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "table")
                    {
                        if (c.TypeofFieldInTbl == "Textbox")
                        {
                            var txtInTbl = ph.FindControl(c.ControlID.ToString()) as TextBox;
                            if (txtInTbl != null)
                            {
                                cVal.ControlValue = txtInTbl.Text;
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "checkbox")
                        {
                            var chkInTbl = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                            if (chkInTbl != null)
                            {
                                cVal.ControlValue = chkInTbl.Checked.ToString();
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "radiobutton")
                        {
                            var RbInTbl = ph.FindControl(c.ControlID.ToString()) as RadioButton;
                            if (RbInTbl != null)
                            {
                                cVal.ControlValue = RbInTbl.Checked.ToString();
                            }
                        }
                    }
                    cVal.ControlID = c.ControlID;
                    cVal.Section = section;
                    hbal.HealthCheck_FormData_Add(cVal);

                }
                else
                {
                    if (c.TypeOfField.ToLower() == "date")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }

                    }
                    else if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }

                    }
                    else if (c.TypeOfField.ToLower() == "dropdown")
                    {
                        var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                        if (ddl != null)
                        {
                            if (!string.IsNullOrEmpty(c.DefaultText))
                            {
                                cVal.ControlValue = ddl.SelectedItem.Text + "$" + ddl.SelectedValue;
                            }
                            else
                            {
                                cVal.ControlValue = ddl.SelectedValue;
                            }
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        var chk = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                        if (chk != null)
                        {
                            cVal.ControlValue = chk.Checked.ToString();
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkboxlist")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as CheckBoxList;
                        var chkValues = string.Empty;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                    chkValues = chkValues + "1,";
                                else
                                    chkValues = chkValues + "0,";
                            }
                            cVal.ControlValue = chkValues;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "table")
                    {
                        if (c.TypeofFieldInTbl.ToLower() == "textbox")
                        {
                            var txtInTbl = ph.FindControl(c.ControlID.ToString()) as TextBox;
                            if (txtInTbl != null)
                            {
                                cVal.ControlValue = txtInTbl.Text;
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "checkbox")
                        {
                            var chkInTbl = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                            if (chkInTbl != null)
                            {
                                cVal.ControlValue = chkInTbl.Checked.ToString();
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "radiobutton")
                        {
                            var RbInTbl = ph.FindControl(c.ControlID.ToString()) as RadioButton;
                            if (RbInTbl != null)
                            {
                                cVal.ControlValue = RbInTbl.Checked.ToString();
                            }
                        }
                    }
                    hbal.HealthCheck_FormData_update(cVal);
                }
            }
            //save the form to folder
            PrintAndSaveForm(HealthCheckID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void PrintAndSaveForm(int hid)
    {
        try
        {
            HealthCheckBAL hbal = new HealthCheckBAL();
            var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(hid);
            var wkhtmltopdfLocation = Server.MapPath("~/bin/") + "wkhtmltopdf.exe";
            var htmlUrl = string.Format(@"{0}/HC/FormDataPreview.aspx?appformid=" + hid.ToString(), Deffinity.systemdefaults.GetWebUrl());
            var cfile = Server.MapPath("~/WF/UploadData/HC/App") + hid.ToString() + ".pdf";
            if (System.IO.File.Exists(cfile))
            {
                System.IO.File.Delete(cfile);
            }

            var fpath = cfile + "\"";
            var pdfSaveLocation = "\"" + fpath;

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = wkhtmltopdfLocation;
                process.StartInfo.Arguments = htmlUrl + " " + pdfSaveLocation;
                process.Start();
                process.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            int healthCheckListID = 0;
            pnlFormData.Visible = true;
            if (Request.QueryString["appformid"] != null)
            {
                healthCheckListID = Convert.ToInt32(Request.QueryString["appformid"]);
            }
            SavePlaceholderData(healthCheckListID, phMainForm);
            Response.Redirect("~/HC/Print.ashx?appformid=" + healthCheckListID, false);

            //string filename;
            //filename = Server.MapPath("~\\UploadData\\HC\\SD" + healthCheckListID.ToString() + ".pdf");
            //System.IO.FileInfo file = new System.IO.FileInfo(filename);
            //if ((file.Exists))
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.ContentType = "application/octet-stream";
            //    Response.WriteFile(file.FullName);
            //    Response.End();
            //    Response.Close();
            //    file = null;
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnSubmitChanges_Click(object sender, EventArgs e)
    {
        try
        {
            int healthCheckListID = 0;
            if (Request.QueryString["appformid"] != null)
            {
                healthCheckListID = Convert.ToInt32(Request.QueryString["appformid"]);
            }

            SavePlaceholderData(healthCheckListID, phMainForm);


            lblMsg.Text = "Saved successfully";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public int CreateChildRecord()
    {
        int ChildFormId = 0;
        try
        {
            int AppId = 0;

            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (Request.QueryString["AppFormID"] != null)
                {
                    AppId = Hdc.AppManagerAssignedForms.Where(a => a.ID == int.Parse(Request.QueryString["AppFormID"])).Select(a => a.AppID.Value).FirstOrDefault();
                }

                AppManagerAssignedForm App_m = new AppManagerAssignedForm();
                //var isexists = Hdc.AppManagerAssignedForms.Where(o => o.FormName.ToLower().Trim() == txtChildName.Text.ToLower().Trim()
                //    && o.AppID == AppId && o.Form_Type.ToLower() == "child" && o.ParentFormId == int.Parse(Request.QueryString["AppFormID"])).FirstOrDefault();
                // if (isexists == null)
                // {
                App_m.AppID = AppId;
                App_m.FormName = string.Empty;
                App_m.Notes = string.Empty;
                App_m.CreatedDate = DateTime.Now;
                App_m.CreatedBy = sessionKeys.UID;
                App_m.ModifiedBy = sessionKeys.UID;
                App_m.ModifiedDate = DateTime.Now;
                App_m.Form_Type = "Child";
                App_m.ParentFormId = int.Parse(Request.QueryString["AppFormID"]);
                Hdc.AppManagerAssignedForms.InsertOnSubmit(App_m);
                Hdc.SubmitChanges();
                ChildFormId = App_m.ID;
                // Response.Redirect(string.Format("~/App/AppChildForm.aspx?appformid={0}", App_m.ID), false);
                //BindGrid(AppId);
                //txtFormName.Text = string.Empty;
                //txtNotes.Text = string.Empty;
                //}
                //else
                //{
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    lblMsg.Text = "Form name already exists";
                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ChildFormId;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            int healthCheckListID = 0;
            if (lblupdateorsubmit.Text == "New")
            {
                int ChildFormIdList = CreateChildRecord();
                lblchildFormId.Text = ChildFormIdList.ToString();
                healthCheckListID = Convert.ToInt32(lblchildFormId.Text);
            }
            else
            {
                healthCheckListID = Convert.ToInt32(lblchildFormId.Text);
            }
            lblupdateorsubmit.Text = string.Empty;
          // BindControls(int.Parse(lblchildFormId.Text), "child");
          // BindControls(healthCheckListID, "child");
            SavePlaceholderData(healthCheckListID, PhChild);
            using (UserDataContext Udc = new UserDataContext())
            {
                List<UserMgt.Entity.Contractor> Clist = Udc.Contractors.ToList();
                BindChildRecordsGrid(Clist);
            }
            lblMsg.Text = "Saved successfully";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnPopUp_Click(object sender, EventArgs e)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                //int ChildFormIdList = CreateChildRecord();
                //lblchildFormId.Text = ChildFormIdList.ToString();
                PhChild.Controls.Clear();
                lblupdateorsubmit.Text = "New";
                int ParentFormId = 0;
                if (Request.QueryString["appformid"] != null)
                {
                    ParentFormId = Convert.ToInt32(Request.QueryString["appformid"]);
                }
                int Appid = Hdc.AppManagerAssignedForms.Where(a => a.ID == ParentFormId).Select(a => a.AppID.Value).FirstOrDefault();
                int ChildFormIdList = Hdc.AppManagers.Where(a => a.ID == Appid).Select(a => a.ChildFormId.Value).FirstOrDefault();
                lblchildFormId.Text = ChildFormIdList.ToString();
                BindControls(ChildFormIdList, "child");
                mdlpopupForNewItem.Show();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void BindChildRecordsGrid(List<UserMgt.Entity.Contractor> Clist)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                int ParentFormID = 0;
                if (Request.QueryString["appformid"] != null)
                {
                    ParentFormID = Convert.ToInt32(Request.QueryString["appformid"]);
                }
                var childRecordsList = Hdc.AppManagerAssignedForms.Where(a => a.ParentFormId == ParentFormID).ToList();
                if (childRecordsList.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ChildID", typeof(string));
                    dt.Columns.Add("Form name", typeof(string));
                    dt.Columns.Add("Date", typeof(string));
                    dt.Columns.Add("Created By", typeof(string));
                    dt.Columns.Add("Modified By", typeof(string));

                    int AppID = childRecordsList.FirstOrDefault().AppID.Value;
                    int childFormId = Hdc.AppManagers.Where(a => a.ID == AppID).FirstOrDefault().ChildFormId.Value;

                    var PanelsList = Hdc.HealthCheck_FormPanels.Where(a => a.FormID == childFormId && a.PanelName != "Header" && a.PanelName != "Signature Panel").ToList();
                    foreach (var Pnl in PanelsList)
                    {
                        var FormControlsList = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == Pnl.PanelID).ToList();
                        foreach (var Formcntl in FormControlsList)
                        {
                            dt.Columns.Add(Formcntl.ControlLabelName, typeof(string));
                        }
                    }
                    DataRow datarw;
                    foreach (var d in childRecordsList)
                    {
                        datarw = dt.NewRow();
                        int i = 0;
                        if (PanelsList.Count == 0)
                        {
                            datarw[0] = d.ID;
                            datarw[1] = d.FormName;
                            datarw[2] = string.Format("{0:d}", d.CreatedDate.HasValue ? d.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                            datarw[3] = Clist.Where(a => a.ID == d.CreatedBy).FirstOrDefault().ContractorName;
                            datarw[4] = Clist.Where(a => a.ID == d.ModifiedBy).FirstOrDefault().ContractorName;
                        }
                        else
                        {
                                if (i == 0)
                                {
                                    datarw[0] = d.ID;
                                    datarw[1] = d.FormName;
                                    datarw[2] = string.Format("{0:d}", d.CreatedDate.HasValue ? d.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                                    datarw[3] = Clist.Where(a => a.ID == d.CreatedBy).FirstOrDefault().ContractorName;
                                    datarw[4] = Clist.Where(a => a.ID == d.ModifiedBy).FirstOrDefault().ContractorName;
                                    i = 5;
                                }
                                if (i > 0)
                                {
                                    if (PanelsList != null)
                                    {
                                        foreach (var l in PanelsList)
                                        {
                                            var Cntllist = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == l.PanelID).ToList();
                                            foreach (var c in Cntllist)
                                            {
                                                var cuslist = Hdc.HealthCheck_FormDatas.Where(a => a.HealthCheckID == d.ID && a.ControlID == c.ControlID && a.Section == "app").FirstOrDefault();
                                                if (cuslist != null)
                                                {
                                                    if (!string.IsNullOrEmpty(cuslist.ControlValue))
                                                    {
                                                        if (cuslist.ControlValue.Contains('$'))
                                                        {
                                                            int ij = cuslist.ControlValue.LastIndexOf('$');
                                                            datarw[i] = cuslist.ControlValue.Substring(0, ij);
                                                        }
                                                        else
                                                        {
                                                            datarw[i] = cuslist.ControlValue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        datarw[i] = string.Empty;
                                                    }
                                                }
                                                else
                                                {
                                                    datarw[i] = string.Empty;
                                                }
                                                i++;
                                            }
                                            if (PanelsList.Count == 1)
                                            {
                                                dt.Rows.Add(datarw);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        datarw[i] = string.Empty;
                                    }
                                }
                                //  i++;
                        }
                        if (PanelsList.Count != 1)
                        {
                            dt.Rows.Add(datarw);
                        }
                    }
                    GridChildrecords.DataSource = dt;
                    GridChildrecords.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }    

    protected void GridChildrecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit1")
            {
                lblupdateorsubmit.Text = "Update";
                PhChild.Controls.Clear();
                lblchildFormId.Text = e.CommandArgument.ToString();
                BindControls(int.Parse(lblchildFormId.Text), "child");
                mdlpopupForNewItem.Show();
            }
            if (e.CommandName == "Delete")
            {
                int Id =int.Parse(e.CommandArgument.ToString());
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    AppManagerAssignedForm AppForm = Hdc.AppManagerAssignedForms.Where(a => a.ID == Id).FirstOrDefault();
                    Hdc.AppManagerAssignedForms.DeleteOnSubmit(AppForm);
                    Hdc.SubmitChanges();
                    lblMsg.Text = "Deleted successfully.";
                    BindChildGrid();
                }
            }
            if (e.CommandName == "Upload")
            {
                LblchildFormIdForFile.Text = e.CommandArgument.ToString();
                MdlpopForFileUpload.Show();
            }
            if (e.CommandName == "Download")
            {
                
                LblchildFormIdForFile.Text = e.CommandArgument.ToString();
                DownloadFile();
            }
            if(e.CommandName == "DeleteFile")
            {
                //LblchildFormIdForFile.Text = e.CommandArgument.ToString();
                DeleteFile(e.CommandArgument.ToString());
                BindChildGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindChildGrid()
    {
        using (UserDataContext Udc = new UserDataContext())
        {
            List<UserMgt.Entity.Contractor> Clist = Udc.Contractors.ToList();
            BindChildRecordsGrid(Clist);
        }
    }

    public void DeleteFile(string fileid)
    {
        try
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/App/"), fileid + ".*");
            var filename = filePaths.FirstOrDefault();
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridChildrecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[7].Visible = false;

            LinkButton LnkUploadFile = (LinkButton)e.Row.FindControl("LnkUploadFile");
            LinkButton LnlDownloadFile = (LinkButton)e.Row.FindControl("LnlDownloadFile");
            LinkButton LnlDeletefile = (LinkButton)e.Row.FindControl("LnlDeletefile");
            Label lblCidForFile = (Label)e.Row.FindControl("lblCidForFile");
            
           // string[] files = Directory.GetFiles(Server.MapPath("~/UploadData/App/"));
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/App/"), lblCidForFile.Text +".*");
            var filename= filePaths.FirstOrDefault();
            if (File.Exists(filename))
            {
                LnkUploadFile.Visible = false;
                LnlDownloadFile.Visible = true;
                if (!ApplyPermission())
                    LnlDeletefile.Visible = false;
                else
                    LnlDeletefile.Visible = true;
            }
            else
            {
                if (!ApplyPermission())
                    LnkUploadFile.Visible = false;
                else
                    LnkUploadFile.Visible = true;
                LnlDownloadFile.Visible = false;
                LnlDeletefile.Visible = false;
            }

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
    }
    protected void GridChildrecords_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridChildrecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public void DownloadFile()
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/App/"), LblchildFormIdForFile.Text + ".*");
        
        Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.AppendHeader("Content-Disposition", "attachment; filename=" +"File_"+ Path.GetFileName(filePaths.FirstOrDefault()));
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(filePaths.FirstOrDefault());
        Response.End();
    }
    protected void BtnFileUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string filePath = "~/WF/UploadData/App/";
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                ////Get file extension
                //FileInfo fi = new FileInfo(FileUpload1.FileName);
                
                //Save file
                FileUpload1.PostedFile.SaveAs(Server.MapPath(filePath) + LblchildFormIdForFile.Text + System.IO.Path.GetExtension(FileUpload1.FileName));
                using (UserDataContext Udc = new UserDataContext())
                {
                    List<UserMgt.Entity.Contractor> Clist = Udc.Contractors.ToList();
                    BindChildRecordsGrid(Clist);
                }
            }
            else
            {
                lblMsgInFileUploadPopup.Text = "Please select file";
                MdlpopForFileUpload.Show();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}