using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.Entity;
using HealthCheckMgt.BAL;

public partial class DC_DCForm : System.Web.UI.Page
{
    HealthCheckBAL hb;
    public string section = "servicedesk";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (QueryStringValues.CallID > 0)
            {
                //check form already exists
                hb = new HealthCheckBAL();
                var reval = hb.HealthCheck_FormAssignToCall_SelectByCallID(QueryStringValues.CallID);
                if (reval != null)
                {
                    pnlForm.Visible = false;
                    pnlFormData.Visible = true;
                }
                else
                {
                    var callDetails = DC.BLL.CallDetailsBAL.SelectbyId(QueryStringValues.CallID);
                    Bindhealthcheck(callDetails.CompanyID.Value);
                    pnlFormData.Visible = false;
                    pnlForm.Visible = true;
                }
            }
        }
        if (pnlFormData.Visible)
        {
            int healthCheckListID = QueryStringValues.CallID;
            try
            {
                BindControls(healthCheckListID);
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
    }

    //public int 

    public void Bindhealthcheck(int customerID)
    {
        hb = new HealthCheckBAL();
        var formlist= hb.HealthCheck_Form_SelectByCustomerID(customerID);
        ddlForms.DataSource = (from h in formlist
                              select new { h.FormID, h.FormName }).ToList();
        ddlForms.DataTextField = "FormName";
        ddlForms.DataValueField = "FormID";
        ddlForms.DataBind();
        ddlForms.Items.Insert(0, new ListItem("Please select...","0"));
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            hb = new HealthCheckBAL();
            var retval = hb.HealthCheck_FormAssignToCall_Add(new HealthCheck_FormAssignToCall() { CallID = QueryStringValues.CallID, FormID = Convert.ToInt32(ddlForms.SelectedValue) });

            if (retval.ID > 0)
            {
                lblMsg.Text = "Added successfully.";
                pnlForm.Visible = false;
                Response.Redirect(Request.RawUrl);
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnSubmitChanges_Click(object sender, EventArgs e)
    {
        try
        {
            int healthCheckListID = QueryStringValues.CallID;
            
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
            var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
            int formid = hcData.FormID.Value;
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
                    pnltbl = new Table();
                    pnltbl.Style.Add("width", "100%");
                    pnltbl.Style.Add("background-color", pHeader.PanelBackColor);
                    pnltbl.CssClass = "tblcontrol";
                    // var td = null;
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
                                td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                lc = new LiteralControl(cval.ControlLabelName + " <br/>");
                                td.Controls.Add(lc);
                                if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
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
                                    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
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
                                    td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "checkboxlist")
                                {
                                    td.Controls.Add(GenerateChecklistbox(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
                                    tr.Cells.Add(td);
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
                            var Sval = hcontrols.Where(o => o.PanelID == Signature.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                            if (Sval != null)
                            {
                                var Sdata = hControlValues.Where(o => o.ControlID == Sval.ControlID).FirstOrDefault();
                                td.Controls.Add(GenerateLableForSignaturePnl(Sval.ControlID.ToString(), Sval.ControlLabelName, Sval.ControlPosition, 10, 22));
                                tr.Cells.Add(td);
                                if (Sval.TypeOfField.ToLower() == "textbox")
                                {
                                    if (!string.IsNullOrEmpty(Sval.TypeOfField))
                                    {
                                     //   td.Controls.Add(GenerateTextbox(Sval.ControlID.ToString(), (Sdata != null ? Sdata.ControlValue : string.Empty), Isfirsttime, 50, Sval.ControlPosition));
                                     // tr.Cells.Add(td);
                                        var txt = new TextBox();
                                        txt.ID = Sval.ControlID.ToString();
                                        txt.Text = Sdata != null ? Sdata.ControlValue : string.Empty;
                                        if (Sdata != null)
                                        {
                                            if (Sdata.ControlValue != "")
                                            {
                                                txt.ReadOnly = true;
                                            }
                                        }
                                        txt.Style.Add("width", Sval.ControlWidth.HasValue ? Sval.ControlWidth.ToString() + "%" : "100%");
                                        txt.Style.Add("float", "left");
                                        td.Controls.Add(txt);
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
    public void SavePlaceholderData(int HealthCheckID)
    {
        try
        {
            ViewState["state"] = "2";

            HealthCheckBAL hbal = new HealthCheckBAL();
            var hcData = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
            int formid = hcData.FormID.Value;
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID,section);

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

                    if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
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

                    cVal.ControlID = c.ControlID;
                    cVal.Section = section;
                    hbal.HealthCheck_FormData_Add(cVal);

                }
                else
                {

                    if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
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
                    hbal.HealthCheck_FormData_update(cVal);
                }
            }
            //save the form to folder
            //PrintAndSaveForm(HealthCheckID);
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

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
    public Label GenerateLable(string id, string lblName)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.EnableViewState = true;
        return lbl;
    }

    TextBox txt;

    public TextBox GenerateTextarea(string id, string setvalue, bool Isfirsttime, int? width, string position)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.TextMode = TextBoxMode.MultiLine;
        txt.Height = 70;
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;

        return txt;
    }
    public TextBox GenerateTextbox(string id, string setvalue, bool Isfirsttime, int? width, string position)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    CheckBoxList chl;
    public CheckBoxList GenerateChecklistbox(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position)
    {
        chl = new CheckBoxList();
        chl.ID = id;
        //ddl.Width = 200;
        chl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        chl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        chl.RepeatLayout = RepeatLayout.Table;
        chl.RepeatDirection = RepeatDirection.Vertical;
        chl.RepeatColumns = 2;
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
        return chl;
    }
    DropDownList ddl;
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position)
    {
        ddl = new DropDownList();
        try
        {
            ddl.ID = id;
            //ddl.Width = 200;
            ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
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
    

}