using ClosedXML.Excel;
using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

public partial class App_AppFormSearch : System.Web.UI.Page
{
    List<HealthCheckMgt.Entity.HealthCheck_FormControl> hcControls = new List<HealthCheckMgt.Entity.HealthCheck_FormControl>();
    protected void Page_Load(object sender, EventArgs e)
    {
        int appid = 0;
       
        if (Request.QueryString["appid"] != null)
        {
            appid = Convert.ToInt32(Request.QueryString["appid"]);
        }
        if (!IsPostBack)
        {
            //Master.PageHead = "Search";
            //BindSearchGrid(hcControls);
        }
        try
        {
            BindControls(appid);

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
            HealthCheckMgt.Entity.AppManagerAssignedForm hf = new HealthCheckMgt.Entity.AppManagerAssignedForm();
            List<HealthCheckMgt.Entity.gridColumnsVisibility> visibilityList = new List<HealthCheckMgt.Entity.gridColumnsVisibility>();
            int formid = 0;
            using (HealthCheckDataContext hcd = new HealthCheckDataContext())
            {
                hf = hcd.AppManagerAssignedForms.Where(o => o.AppID == HealthCheckID).FirstOrDefault();
                lblTitle.InnerText = hf.FormName;
                formid = hf.AppManager.FormID.Value;
                visibilityList = hcd.gridColumnsVisibilities.Where(a => a.Appid == HealthCheckID && a.Visibility == true).ToList();
            }
            var VisibleColumns = visibilityList.Select(a => a.ColumnId.Value).ToArray();
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            LiteralControl lc;
            
            //get list of existing panels
            var plist = hpanels.Where(o => o.PanelName != "Header" && o.PanelName != "Signature Panel").OrderBy(a => a.PnlPosition).ToList();
            var ctrlTypelist = new string[] { "image", "label" };
            var controlList = hcontrols.Where(o => plist.Select(p => p.PanelID).ToArray().Contains(o.PanelID.HasValue?o.PanelID.Value:0) && !ctrlTypelist.Contains(o.TypeOfField.ToLower())).ToList();

            if (visibilityList.Count > 0)
                controlList = controlList.Where(o => VisibleColumns.Contains(o.ControlID)).ToList();

            int totalControls = controlList.Count;
            int totalrows = 0;
            int totalcolumns = 3;
            if (totalControls > 0)
            {
                if (totalControls / 3 > 0)
                    totalrows = totalControls / 3;
                else
                    totalrows = 1;
            }
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "100%");
            TableRow tr = new TableRow();
            TableCell td = null;
            int cnt = 0;
            int jcnt = 1;
            int totalCnt = controlList.Count();
            foreach (var cval in controlList)
            {
                if (VisibleColumns.Contains(cval.ControlID))
                {
                    cval.ControlWidth = 50;
                    //string val = controlList.Where(p => p. == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                    string rval = string.Empty;
                    //if (val != null)
                    //    rval = val.ToString();

                    if (tr == null)
                        tr = new TableRow();

                    td = new TableCell();
                    //td.Style.Add("width", (100 / colCnt).ToString() + "%");
                    lc = new LiteralControl(cval.ControlLabelName + " <br/>");
                    td.Controls.Add(lc);

                    if (cval.TypeOfField.ToLower() == "date")
                    {
                        td.Controls.Add(GenerateTextboxDate(cval.ControlID.ToString(), string.Empty, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                        //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                        //{
                        //    rf = Add_validation(cval);
                        //    td.Controls.Add(rf);
                        //}
                        tr.Cells.Add(td);
                    }
                    else if (cval.TypeOfField.ToLower() == "textbox")
                    {
                        td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), string.Empty, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                        //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                        //{
                        //    rf = Add_validation(cval);
                        //    td.Controls.Add(rf);
                        //}
                        tr.Cells.Add(td);
                    }
                    else if (cval.TypeOfField.ToLower() == "dropdown")
                    {
                        td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), string.Empty, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                        //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                        //{
                        //    rf = Add_dropdown_validation(cval);
                        //    td.Controls.Add(rf);
                        //}
                        tr.Cells.Add(td);
                    }
                    else if (cval.TypeOfField.ToLower() == "checkbox")
                    {
                        td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), string.Empty, Isfirsttime));
                        tr.Cells.Add(td);
                    }
                    else if (cval.TypeOfField.ToLower() == "textarea")
                    {
                        td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), string.Empty, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                        //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                        //{
                        //    rf = Add_validation(cval);
                        //    td.Controls.Add(rf);
                        //}
                        tr.Cells.Add(td);
                    }
                    else if (cval.TypeOfField.ToLower() == "checkboxlist")
                    {
                        td.Controls.Add(GenerateChecklistbox(cval.DefaultText, cval.ListValues, cval.ControlID.ToString(), string.Empty, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                        tr.Cells.Add(td);
                    }


                    cnt = cnt + 1;
                    if (cnt == 3)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && (cnt == 1 || cnt == 2))
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
            }
            ph.Controls.Add(tbl);
            //bind the data
            if (Isfirsttime)
            BindSearchGrid(controlList);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

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
    public Label GenerateLable(string id, string lblName, int? width, int? Height, string position)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //    lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        lbl.EnableViewState = true;
        return lbl;
    }
    TextBox txt;
    public TextBox GenerateTextarea(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        //txt.TextMode = TextBoxMode.MultiLine;
        //txt.Height = 70;
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
        //var TblControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID, section);
        //var TblControlValues = null;
        int count = 0;
        for (int i = 1; i <= r; i++)
        {
            trow = new TableRow();
            for (int j = 0; j <= c; j++)
            {
                if (j != 0)
                {
                   // var TblCntldata = TblControlValues.Where(o => o.ControlID == TblCntlList[count].ControlID).FirstOrDefault();
                    tc = new TableCell();
                    if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "textbox")
                    {
                        var txt = new TextBox();
                        txt.ID = TblCntlList[count].ControlID.ToString();
                        //txt.Text = TblCntldata != null ? TblCntldata.ControlValue : string.Empty;
                        txt.Style.Add("width", width.HasValue ? width.ToString() + "%" : "100%");
                        txt.Style.Add("float", "right");
                        tc.Controls.Add(txt);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "checkbox")
                    {
                        var checkBox = new CheckBox();
                        checkBox.ID = TblCntlList[count].ControlID.ToString();
                        //checkBox.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
                        checkBox.Style.Add("float", "right");
                        tc.Controls.Add(checkBox);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "radiobutton")
                    {
                        var Rbtn = new RadioButton();
                        Rbtn.ID = TblCntlList[count].ControlID.ToString();
                        //Rbtn.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindSearchGrid(hcControls);
            ViewState["state"] = "2";
            List<HealthCheckMgt.Entity.HealthCheck_FormControl> currentSearch = GetSearchCollection();
            //bind grid
            BindSearchGrid(currentSearch);
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private List<HealthCheckMgt.Entity.HealthCheck_FormControl> GetSearchCollection()
    {
        int appid = 0;

        if (Request.QueryString["appid"] != null)
        {
            appid = Convert.ToInt32(Request.QueryString["appid"]);
        }
        int formid = 0;
        HealthCheckBAL hbal = new HealthCheckBAL();
        using (HealthCheckDataContext hcd = new HealthCheckDataContext())
        {
            var hfdatalist = hcd.AppManagerAssignedForms.Where(o => o.AppID == appid).ToList();
            var hf = hfdatalist.FirstOrDefault();
            lblTitle.InnerText = hf.FormName;
            formid = hf.AppManager.FormID.Value;
        }
        var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);

        List<HealthCheckMgt.Entity.HealthCheck_FormControl> currentSearch = new List<HealthCheckMgt.Entity.HealthCheck_FormControl>();
        foreach (HealthCheckMgt.Entity.HealthCheck_FormControl c in hcontrols.Where(o => o.TypeOfField.ToLower() != "fileupload"))
        {
            var cVal = new HealthCheckMgt.Entity.HealthCheck_FormControl();

            if (c.TypeOfField.ToLower() == "date")
            {
                var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                if (txt != null)
                {
                    cVal.ControlID = c.ControlID;
                    cVal.ControlValue = txt.Text;
                }
            }
            if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
            {
                var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                if (txt != null)
                {
                    cVal.ControlID = c.ControlID;
                    cVal.ControlValue = txt.Text;
                }
            }
            else if (c.TypeOfField.ToLower() == "dropdown")
            {
                var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                if (ddl != null)
                {
                    cVal.ControlID = c.ControlID;
                    cVal.ControlValue = ddl.SelectedValue;
                }
            }
            else if (c.TypeOfField.ToLower() == "checkbox")
            {
                var chk = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                if (chk != null)
                {
                    cVal.ControlID = c.ControlID;
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
                    cVal.ControlID = c.ControlID;
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
                        cVal.ControlID = c.ControlID;
                        cVal.ControlValue = txtInTbl.Text;
                    }
                }
                else if (c.TypeofFieldInTbl.ToLower() == "checkbox")
                {
                    var chkInTbl = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                    if (chkInTbl != null)
                    {
                        cVal.ControlID = c.ControlID;
                        cVal.ControlValue = chkInTbl.Checked.ToString();
                    }
                }
                else if (c.TypeofFieldInTbl.ToLower() == "radiobutton")
                {
                    var RbInTbl = ph.FindControl(c.ControlID.ToString()) as RadioButton;
                    if (RbInTbl != null)
                    {
                        cVal.ControlID = c.ControlID;
                        cVal.ControlValue = RbInTbl.Checked.ToString();
                    }
                }

                //hbal.HealthCheck_FormData_update(cVal);

            }
            currentSearch.Add(cVal);
        }
        return currentSearch;
    }

    private void BindSearchGrid(List<HealthCheckMgt.Entity.HealthCheck_FormControl> searchCollection)
    {
        List<HealthCheckMgt.Entity.HealthCheck_FormControl> columns = new List<HealthCheckMgt.Entity.HealthCheck_FormControl>();
        DataTable dv = GetSearchData(searchCollection, out columns);
        if (dv != null)
        {
            GridResult.DataSource = dv;
            GridResult.DataBind();
        }
    }

    private DataTable GetSearchData(List<HealthCheckMgt.Entity.HealthCheck_FormControl> searchCollection, out List<HealthCheckMgt.Entity.HealthCheck_FormControl> columns)
    {

        int appid = 0;
        DataView dv = new DataView();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        if (Request.QueryString["appid"] != null)
        {
            appid = Convert.ToInt32(Request.QueryString["appid"]);
        }
        HealthCheckBAL hbal = new HealthCheckBAL();
        HealthCheckMgt.Entity.AppManagerAssignedForm hf = new HealthCheckMgt.Entity.AppManagerAssignedForm();
        List<HealthCheckMgt.Entity.AppManagerAssignedForm> hfdatalist = new List<HealthCheckMgt.Entity.AppManagerAssignedForm>();
        int formid = 0;
        List<HealthCheckMgt.Entity.gridColumnsVisibility> visibilityList = new List<HealthCheckMgt.Entity.gridColumnsVisibility>();
        using (HealthCheckDataContext hcd = new HealthCheckDataContext())
        {
            hfdatalist = hcd.AppManagerAssignedForms.Where(o => o.AppID == appid && o.Form_Type == "Parent").ToList();
            hf = hfdatalist.FirstOrDefault();
            lblTitle.InnerText = hf.FormName;
            formid = hf.AppManager.FormID.Value;

            visibilityList = hcd.gridColumnsVisibilities.Where(a => a.Appid == appid && a.Visibility == true).ToList();
        }

        var hform = hbal.HealthCheck_Form_SelectByID(formid);
        var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
        var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);

        LiteralControl lc;

        //get list of existing panels
        var plist = hpanels.Where(o => o.PanelName != "Header" && o.PanelName != "Signature Panel").OrderBy(a => a.PnlPosition).ToList();
        var ctrlTypelist = new string[] { "image", "label" };

        var VisibleColumns = visibilityList.Select(a => a.ColumnId.Value).ToArray();
        //list of controls
        var controlList = hcontrols.Where(o => plist.Select(p => p.PanelID).ToArray().Contains(o.PanelID.HasValue ? o.PanelID.Value : 0) && !ctrlTypelist.Contains(o.TypeOfField.ToLower())).ToList();
        if (visibilityList.Count > 0)
            controlList = controlList.Where(o => VisibleColumns.Contains(o.ControlID)).ToList();

        List<HealthCheckMgt.Entity.HealthCheck_FormData> hcontrolData = new List<HealthCheckMgt.Entity.HealthCheck_FormData>();
        dt.Columns.Add("Record", typeof(string));
        dt.Columns.Add("Created Date", typeof(string));
        dt.Columns.Add("Created By", typeof(string));
        dt.Columns.Add("Notes", typeof(string));
        if (controlList.Count > 0)
        {

            columns = controlList;

            foreach (var c in controlList)
            {
                if (VisibleColumns.Contains(c.ControlID))
                {
                    if (dt.Columns.Contains(c.ControlLabelName))
                        //dt.Columns[i].ColumnName = dt_key.Rows[i][0].ToString() + "  ";
                        dt.Columns.Add(c.ControlLabelName + "  ", typeof(string));
                    else
                        dt.Columns.Add(c.ControlLabelName, typeof(string));
                    //dt.Columns[i].ColumnName = dt_key.Rows[i][0].ToString();
                }

            }
            List<UserMgt.Entity.Contractor> Clist = new List<UserMgt.Entity.Contractor>();
            using (UserDataContext Udc = new UserDataContext())
            {
                Clist = Udc.Contractors.ToList();
            }
            DataRow datarw;
            var controlids = controlList.Select(o => o.ControlID).ToList();
            foreach (var b in hfdatalist)
            {
                datarw = dt.NewRow();
                //collection app 
                hcontrolData = hbal.HealthCheck_FormData_SelectByHealthCheckID(b.ID, "app").Where(o => !ctrlTypelist.Contains(o.HealthCheck_FormControl.TypeOfField.ToLower())).ToList();
                //filter the data depends on data entered

                if (hcontrolData.Count > 0)
                {
                    int index = 0;
                    if (hcontrolData.Count == 0)
                    {
                        datarw[0] = b.FormName;
                        datarw[1] = string.Format("{0:d}", b.CreatedDate.HasValue ? b.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                        datarw[2] = Clist.Where(a => a.ID == b.CreatedBy).FirstOrDefault().ContractorName;
                        datarw[3] = Clist.Where(a => a.ID == b.ModifiedBy).FirstOrDefault().ContractorName;
                    }
                    else
                    {
                        if (index == 0)
                        {
                            datarw[0] = b.FormName;
                            datarw[1] = string.Format("{0:d}", b.CreatedDate.HasValue ? b.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                            datarw[2] = Clist.Where(a => a.ID == b.CreatedBy).FirstOrDefault().ContractorName;
                            datarw[3] = Clist.Where(a => a.ID == b.ModifiedBy).FirstOrDefault().ContractorName;
                            index = 4;
                        }
                        if (index > 0)
                        {
                            foreach (var cid in controlids)
                            {
                                var vVal = hcontrolData.Where(o => o.ControlID == cid).FirstOrDefault();
                                if (vVal != null)
                                    datarw[index] = vVal.ControlValue;
                                else
                                    datarw[index] = string.Empty;
                                index++;
                            }
                            dt.Rows.Add(datarw);
                        }
                    }
                }
            }

            //Data view
            dv = new DataView(dt);
            //Search filter
            string serachtext = string.Empty;
            int sIndex = 0;
            if (searchCollection != null)
            {
                var FilterIDs = new List<int>();
                foreach (var sc in searchCollection)
                {
                    var hcontrolData_copy = hcontrolData;
                    if (!string.IsNullOrEmpty(sc.ControlValue))
                    {
                        if (sc.ControlValue != "0")
                        {
                            if (!string.IsNullOrEmpty(serachtext.Trim()))
                                serachtext = serachtext + " AND ";
                            serachtext = serachtext + string.Format(" [{0}] = '{1}' ", controlList.Where(o => o.ControlID == sc.ControlID).Select(o => o.ControlLabelName).FirstOrDefault(), sc.ControlValue);

                            //hcontrolData_copy = hcontrolData.Where(o => o.ControlID == sc.ControlID && o.ControlValue.ToLower() == sc.ControlValue.ToLower()).ToList();
                            sIndex++;
                        }
                    }
                }

            }
            if (!string.IsNullOrEmpty(serachtext))
            {
                dv.RowFilter = serachtext;
                //dt = dv.Table;
            }
            return dv.ToTable();


        }
        else
        {
            columns = new List<HealthCheckMgt.Entity.HealthCheck_FormControl>();
            dt.Columns.Add("", typeof(string));
            return dt;
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
      
    }
    protected void GridResult_PreRender(object sender, EventArgs e)
    {
        if (GridResult.Rows.Count > 0)
        {
            //GridResult.Columns[0].HeaderStyle.CssClass = "header_bg_l";

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ViewState["state"] = "2";
        List<HealthCheckMgt.Entity.HealthCheck_FormControl> currentSearch = GetSearchCollection();

        List<HealthCheckMgt.Entity.HealthCheck_FormControl> columns = new List<HealthCheckMgt.Entity.HealthCheck_FormControl>();
        DataTable dv = GetSearchData(currentSearch, out columns);

        //Build Excel
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("App Report");

        var colCount = columns.Count;
        //string[] AlphabetArray = new string[colCount];
        string[] AlphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
                                     "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };
       // string[] AlphabetArray1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
       // int j = 0;
       //for(int k =0; k<= colCount; k++)
       //{
       //    if(AlphabetArray1.Count() < colCount)
       //    {
       //        AlphabetArray[k] = AlphabetArray1[k];
       //    }
       //    else
       //    {

       //    }
       //}

        int index = 0;
        foreach (var item in columns)
        {
            ws.Cell(AlphabetArray[index] + "1").Value = item.ControlLabelName;
            index++;
        }

        var rTable = dv;
        //Rows
        for(int i=0; i < rTable.Rows.Count; i++)
        {
            //columns
            for(int j=0; j< rTable.Columns.Count; j++)
            {
                //var s = rTable.Rows[0][0].ToString();
                //var s1 = rTable.Rows[i][j].ToString();

                ws.Cell(AlphabetArray[j] + (i+2).ToString()).Value = rTable.Rows[i][j].ToString() ;
            }
        }

        var rngHeaders = ws.Range("A1:" + AlphabetArray[index - 1].ToString() + "1");
        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        rngHeaders.Style.Font.Bold = true;
        rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

        string path = HttpContext.Current.Server.MapPath("~\\UploadData\\App");
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        string fileName = "Appreport_" + string.Format("{0:ddMMyyyyhhmmss}", DateTime.Now);
        wb.SaveAs(path + "\\" + fileName + ".xlsx");

        //var newfile= path + "\\" + fileName + ".xls";
        //System.IO.FileInfo fileInfo = new System.IO.FileInfo(newfile);
        Response.Redirect("~/App/DownloadAppFile.ashx?file=" + fileName);
        //if (fileInfo.Exists)
        //{

        //    System.Web.HttpContext.Current.Response.Clear();
        //    System.Web.HttpContext.Current.Response.ClearHeaders();
        //    //System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    //System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=\"" + fileName + ".xls\"");
        //    //System.Web.HttpContext.Current.Response.TransmitFile(newfile);
        //    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
        //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
        //    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
        //    System.Web.HttpContext.Current.Response.Flush();
        //    System.Web.HttpContext.Current.Response.End();  
            
        //    //System.Web.HttpContext.Current.Response.Clear();
        //    //System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
        //    //System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
        //    //System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
        //    //System.Web.HttpContext.Current.Response.Flush();
        //    //System.Web.HttpContext.Current.Response.End();
        //}

       

    }
}