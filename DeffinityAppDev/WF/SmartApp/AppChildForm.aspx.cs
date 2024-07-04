using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_AppChildForm : System.Web.UI.Page
{
    HealthCheckBAL hb;
    public string section = "app";
    protected void Page_Load(object sender, EventArgs e)
    {
        int healthCheckListID = 0;
        pnlFormData.Visible = true;
        if (Request.QueryString["appformid"] != null)
        {
            healthCheckListID = Convert.ToInt32(Request.QueryString["appformid"]);
        }
        if (!IsPostBack)
        {
            PermissionsChecking();
        }
        try
        {
            BindControls(healthCheckListID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
                    btnSubmitChanges.Enabled = false;
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
    protected void btnSubmitChanges_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int healthCheckListID = 0;
            if (Request.QueryString["appformid"] != null)
            {
                healthCheckListID = Convert.ToInt32(Request.QueryString["appformid"]);
            }

            SavePlaceholderData(healthCheckListID);
          

            lblMsg.Text = "Saved successfully";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindControls(int HealthCheckID)
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
                hf = hcd.AppManagerAssignedForms.Where(o => o.ID == HealthCheckID).FirstOrDefault();
                lblTitle.InnerText = hf.FormName;
                formid = hf.AppManager.ChildFormId.HasValue ? hf.AppManager.ChildFormId.Value : 0;
              //  link_return.HRef = "~/App/AppForm.aspx?AppFormID=" + hcd.AppManagerAssignedForms.Where(o => o.AppID == hf.AppID).Select(o => o.ID).FirstOrDefault();
              //  link_return.InnerHtml = string.Format("<span>Return to {0} list</span>", hcd.AppManagerAssignedForms.Where(o => o.AppID == hf.AppID).Select(o => o.FormName).FirstOrDefault());
                link_return.HRef = "~/App/AppForm.aspx?AppFormID=" + (hf.ParentFormId.HasValue?hf.ParentFormId.Value:0).ToString();
                link_return.InnerHtml = string.Format("<span>Return to {0} </span>", hf.AppManager.Name);
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
                                img.ImageUrl = "~/UploadData/HC/" + cval.ControlValue + ".png";
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
                                    td.Controls.Add(GenerateLable((cval.ControlID + temp).ToString(), cval.ControlLabelName, cval.ControlWidth, cval.Height, cval.ControlPosition, cval.MinValue, cval.MaxValue));
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
                                }
                                else if (cval.TypeOfField.ToLower() == "dropdown")
                                {
                                    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
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
                                            img.ImageUrl = "~/UploadData/HC/" + cdata.ControlValue + ".png";
                                            img.Style.Add("width", "25%");
                                            img.Style.Add("height", "25%");
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
                                            img.ImageUrl = "~/UploadData/HC/" + cval.ControlValue + ".png";
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
                                    // td.Controls.Add(GeneratelblForSpace(cval.ControlID));
                                    td.Controls.Add(GenerateImageForToolTip(cval.Helptext, cval.ControlID));
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
                                td.Controls.Add(GenerateLable((cval.ControlID + temp).ToString(), cval.ControlLabelName, 10, 22, cval.ControlPosition, cval.MinValue, cval.MaxValue));
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
            ph.Controls.Add(tbl);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
    public Image GenerateImageForToolTip(string ToolTip, int Id)
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
    public Label GenerateLable(string id, string lblName, int? width, int? Height, string position, string fontbold, string fontsize)
    {
        lbl = new Label();
        //lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //    lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        if (!string.IsNullOrEmpty(fontsize))
            lbl.Style.Add("font-size", fontsize + "px");
        //font bold
        if (!string.IsNullOrEmpty(fontsize))
        {
            if (fontbold.ToLower() == "true")
                lbl.Style.Add("font-weight", "bold");
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
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        ddl = new DropDownList();
        try
        {
            ddl.ID = id;
            //ddl.Width = 200;
            ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  ddl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            ddl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                ddl.Items.Add(s);
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
        { LogExceptions.WriteExceptionLog(ex); }
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

    public void SavePlaceholderData(int HealthCheckID)
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
                hf = hcd.AppManagerAssignedForms.Where(o => o.ID == HealthCheckID).FirstOrDefault();
                lblTitle.InnerText = hf.FormName;
                formid = hf.AppManager.ChildFormId.Value;
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
                    else if (c.TypeOfField.ToLower() == "dropdown")
                    {
                        var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                        if (ddl != null)
                        {
                            if (ddl.SelectedValue != "0")
                                cVal.ControlValue = ddl.SelectedValue;
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
                            cVal.ControlValue = ddl.SelectedValue;
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
            var cfile = Server.MapPath("~/UploadData/HC/App") + hid.ToString() + ".pdf";
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
}