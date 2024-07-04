using DC.DAL;
using HealthCheckMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HC_FormDataPreview : System.Web.UI.Page
{
    public string section = "healthcheck";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["hcid"] != null)
            {
                section = "healthcheck";
                BindControls(Convert.ToInt32(Request.QueryString["hcid"].ToString()),"health");
             
            }
            if (Request.QueryString["callid"] != null)
            {
                section = "servicedesk";
                BindControls(Convert.ToInt32(Request.QueryString["callid"].ToString()),"SD");
              
            }
            if (Request.QueryString["taskid"] != null)
            {
                section = "checkpoint";
                BindControls(Convert.ToInt32(Request.QueryString["taskid"].ToString()), "PD");

            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Form

    public void BindControls(int HealthCheckID,string type)
    {
        try
        {
            int formid = 0;
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
            if (type == "SD")
            {
                var hcDataSd = hbal.HealthCheck_FormAssignToCall_SelectByCallID(HealthCheckID);
                formid = hcDataSd.FormID.Value;

            }
            else if (type == "PD")
            {
                var hpRepsoitory = new HCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask>();
                var hcData = hpRepsoitory.GetAll().Where(o => o.TaskID == HealthCheckID).FirstOrDefault();
                formid = hcData.FormID.Value;
            }
            else
            {
                using (DCDataContext Dc = new DCDataContext())
                {
                    formid = Dc.FLSFormToCalls.Where(o => o.CallID == HealthCheckID).FirstOrDefault().FormID;

                }
                //var hcData = hbal.HealthCheckList_SelectByID(HealthCheckID);
                //formid = hcData.PortfolioHealthCheckID.Value;
            }
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID);
            //start table
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "100%");
            tbl.Style.Add("background-color", hform.FormBackColor);
            tbl.CssClass = "tblform";
            tbl.CellPadding = 5;
            tbl.CellSpacing = 3;
            //check header is exists
            var pHeader = hpanels.Where(o => o.PanelName == "Header").FirstOrDefault();
            TableRow tr;
            TableCell td;
            TableHeaderRow th_row;
            TableHeaderCell th_cell;
            LiteralControl lc;
            Image img;
            Table pnltbl;
            //RequiredFieldValidator rf;
            if (pHeader != null)
            {
                pnltbl = new Table();
                pnltbl.Style.Add("width", "100%");
                pnltbl.Style.Add("background-color", pHeader.PanelBackColor);
                //pnltbl.Style.Add("border-spacing", "5px");
                pnltbl.CssClass = "tblheader";
                tbl.CellPadding = 8;
                tbl.CellSpacing = 3;
                // var td = null;
                for (int row = 1; row <= pHeader.PanelRows; row++)
                {
                    tr = new TableRow();
                    var colCnt = pHeader.PanelColumns;
                    for (int col = 1; col <= 1; col++)
                    {
                        td = new TableCell();
                        td.Style.Add("width", (100 / colCnt).ToString() + "%");
                        var cval = hcontrols.Where(o => o.PanelID == pHeader.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                        //if (cval != null)
                        //{

                        //    if (!string.IsNullOrEmpty(cval.ControlValue))
                        //    {
                        //        img = new Image();
                        //        img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                        //        td.Controls.Add(img);
                        //    }
                        //    else
                        //    {
                        //        var lblHead = new Label();
                        //        lblHead.Text = string.Empty;
                        //        td.Controls.Add(lblHead);

                        //    }
                        //}
                        if (cval != null)
                        {
                            if (!string.IsNullOrEmpty(cval.ControlValue))
                            {
                                img = new System.Web.UI.WebControls.Image();
                                img.ImageUrl = Deffinity.systemdefaults.GetWebUrl()+"/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                td.Controls.Add(img);
                            }
                            else
                            {
                                img = new System.Web.UI.WebControls.Image();
                                img.ImageUrl = Deffinity.systemdefaults.GetWebUrl() +"/WF/UploadData/Customers/portfolio_" + sessionKeys.PortfolioID + ".png";
                                td.Controls.Add(img);
                                //var lblHead = new Label();
                                //lblHead.Text = string.Empty;
                                //td.Controls.Add(lblHead);

                            }
                        }
                        else
                        {
                            img = new System.Web.UI.WebControls.Image();
                            img.ImageUrl = Deffinity.systemdefaults.GetWebUrl() +"/WF/UploadData/Customers/portfolio_" + sessionKeys.PortfolioID + ".png";
                            td.Controls.Add(img);
                            //var lblHead = new Label();
                            //lblHead.Text = string.Empty;
                            //td.Controls.Add(lblHead);

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
                    //pnltbl.Style.Add("border-spacing", "5px");
                    //border-spacing:
                    pnltbl.CssClass = "tblcontrol";
                    tbl.CellPadding = 10;
                    tbl.CellSpacing = 3;
                    // var td = null;
                    th_row = new TableHeaderRow();
                    th_cell = new TableHeaderCell() { ColumnSpan = 2 };
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
                                td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                if (cval.ControlLabelName != null)
                                {
                                    //if (cval.TypeOfField.ToLower() == "checkbox")
                                    //    lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + "");
                                    //else
                                    //    lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + " <br/>");
                                    //td.Controls.Add(lc);
                                    if (cval.TypeOfField.ToLower() == "checkbox")
                                        td = new TableCell();// { RowSpan = 2 };
                                                             //lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + "");
                                    else
                                    {
                                        td = new TableCell();
                                        td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                        lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + " <br/>");
                                        td.Controls.Add(lc);
                                    }
                                }
                                //if (cval.TypeOfField.ToLower() == "checkbox")
                                //    lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + "");
                                //else
                                //    lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + " <br/>");
                                //td.Controls.Add(lc);
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
                                    tr.Cells.Add(td);
                                }

                                if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    //td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
                                    //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    //{
                                    //    rf = Add_validation(cval);
                                    //    //txtid = txtid + 1;
                                    //    td.Controls.Add(rf);
                                    //}
                                    td.Controls.Add(GenerateTextLable(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty),cval.ControlWidth,cval.Height,cval.ControlPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "dropdown")
                                {
                                    //td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
                                    //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    //{
                                    //    rf = Add_dropdown_validation(cval);
                                    //    //txtid = txtid + 1;
                                    //    td.Controls.Add(rf);
                                    //}
                                    td.Controls.Add(GenerateTextLable(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), cval.ControlWidth, cval.Height, cval.ControlPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "checkbox")
                                {
                                    td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                    tr.Cells.Add(td);
                                    td = new TableCell();
                                    td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                    lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + "");
                                    td.Controls.Add(lc);
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "textarea")
                                {
                                    //td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition));
                                    //if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    //{
                                    //    rf = Add_validation(cval);
                                    //    //txtid = txtid + 1;
                                    //    td.Controls.Add(rf);
                                    //}
                                    td.Controls.Add(GenerateTextLable(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), cval.ControlWidth, cval.Height, cval.ControlPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "checkboxlist")
                                {
                                    td.Controls.Add(GenerateChecklistbox(cval.DefaultText,cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition,cval.Height));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "radiobutton")
                                {
                                    td.Controls.Add(GenerateRadioBtnlistbox(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cval.CblPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "image")
                                {
                                    if (cval != null)
                                    {
                                        if (!string.IsNullOrEmpty(cval.ControlValue))
                                        {
                                            img = new System.Web.UI.WebControls.Image();
                                            img.ID = cval.ControlValue.ToString();
                                            img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                            img.Style.Add("float", string.IsNullOrEmpty(cval.ControlPosition) ? "left" : cval.ControlPosition);
                                            //img.Style.Add("width", "25%");
                                            //img.Style.Add("height", "25%");
                                            td.Controls.Add(img);
                                            //   td.Controls.Add(GenerateFileupload(cval.ControlID.ToString()));
                                            tr.Cells.Add(td);
                                        }
                                    }
                                    else
                                    {
                                        img = new System.Web.UI.WebControls.Image();
                                        img.ID = cval.ControlValue.ToString();
                                        img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                        img.EnableViewState = true;
                                        img.Style.Add("float", string.IsNullOrEmpty(cval.ControlPosition) ? "left" : cval.ControlPosition);
                                        //img.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.Value.ToString() + "%" : "100%");
                                        td.Controls.Add(img);
                                        tr.Cells.Add(td);
                                    }
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
                    td.Controls.Add(LblPanel);
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
                         var cval = hcontrols.Where(o => o.PanelID == Signature.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                         if (cval != null)
                         {
                             td.Controls.Add(GenerateLable(cval.ControlID.ToString(), cval.ControlLabelName, 10, 22, cval.ControlPosition));
                             tr.Cells.Add(td);
                             if (cval.TypeOfField.ToLower() == "textbox")
                             {
                                 if (!string.IsNullOrEmpty(cval.TypeOfField))
                                 {
                                     td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, 50, cval.ControlPosition, cval.Height));
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
        txt.Style.Add("width", "121px");
        // txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
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
        //var formid = Request.QueryString["fid"].ToString();
       // var Tblcontrols = hbal.HealthCheck_FormControl_SelectByFormID(int.Parse(formid));
        var Tblcontrols = hbal.HealthCheck_FormControl_SelectAll();
        var TblCntlList = Tblcontrols.Where(o => o.PanelID == PanelId && o.RowIndex == row && o.ColumnIndex == col).ToList();
        int healthCheckListID = 0;
        if (section == "healthcheck")
        {
            healthCheckListID = Convert.ToInt32(Request.QueryString["hcid"]);
        }
        else if (section == "checkpoint")
        {
            healthCheckListID = Convert.ToInt32(Request.QueryString["taskid"]);
        }
        else
        {
            healthCheckListID = Convert.ToInt32(Request.QueryString["callid"]);
        }
       
        var TblControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(healthCheckListID, section);
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

    RadioButtonList Rbl;
    public RadioButtonList GenerateRadioBtnlistbox(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height, string Rblist)
    {
        Rbl = new RadioButtonList();
        try
        {
            Rbl.ID = id;
            //ddl.Width = 200;
            Rbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            Rbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            Rbl.RepeatLayout = RepeatLayout.Table;
            if (Rblist == "Horizontal")
            {
                Rbl.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                Rbl.RepeatDirection = RepeatDirection.Vertical;
            }
            int num1 = 2;

            if (int.TryParse(setvalue, out num1))
            {

            }
            Rbl.RepeatColumns = num1;
            //if (setvalue != string.Empty)
            //{
            //    chl.RepeatColumns = int.Parse(setvalue);
            //}
            //else
            //{
            //    chl.RepeatColumns = 2;
            //}
            Rbl.CellSpacing = 3;
            Rbl.CellPadding = 3;
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                Rbl.Items.Add(s);
            Rbl.EnableViewState = true;
            //chl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            //Rbl.AutoPostBack = true;
            if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            {
                Rbl.Items.FindByValue(setvalue).Selected = true;
            }
            //ddlid = ddlid + 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Rbl;
    }


    Label lbl = null;
    public Label GenerateLable(string id, string lblName, int? width, int? Height, string position)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
      //  lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        lbl.EnableViewState = true;
        return lbl;
    }

    Label Textlbl = null;
    public Label GenerateTextLable(string id, string lblName, int? width, int? Height, string position)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
       // lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        if (!string.IsNullOrEmpty(lblName))
            lbl.Text = lblName.Replace(Environment.NewLine, "<BR/>");
        else
            lbl.Text = " ";
        lbl.Style.Add("width", "98%");
        lbl.BorderStyle = BorderStyle.Solid;
        lbl.BorderColor = System.Drawing.Color.LightGray;
        lbl.BorderWidth = 1;
        lbl.BackColor = System.Drawing.Color.White;
        lbl.CssClass = "lblStyle";
        lbl.Style.Add("display", "inline-block");
        lbl.EnableViewState = true;
        return lbl;
    }

    TextBox txt;

    public TextBox GenerateTextarea(string id, string setvalue, bool Isfirsttime, int? width, string position,int? Height)
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
      //  txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    CheckBoxList chl;
    public CheckBoxList GenerateChecklistbox(string DeafultDeafultColList, string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        chl = new CheckBoxList();
        chl.ID = id;
        //ddl.Width = 200;
        chl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
      //   chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        chl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        chl.RepeatLayout = RepeatLayout.Table;
        chl.RepeatDirection = RepeatDirection.Vertical;

        //if (DeafultDeafultColList != string.Empty)
        //{
        //    chl.RepeatColumns = int.Parse();
        //}
        //else
        //{
        //    chl.RepeatColumns = 2;
        //}
        int num1 = 2;

        if (int.TryParse(DeafultDeafultColList, out num1))
        {

        }
        chl.RepeatColumns = num1;

        chl.CellSpacing = 3;
        chl.CellPadding = 3;
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
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position,int? Height)
    {
        ddl = new DropDownList();
        try
        {
            ddl.ID = id;
            //ddl.Width = 200;
            ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
         //   ddl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
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


    public void SavePlaceholderData(int HealthCheckID)
    {
        try
        {
            ViewState["state"] = "2";

            HealthCheckBAL hbal = new HealthCheckBAL();
            var hcData = hbal.HealthCheckList_SelectByID(HealthCheckID);
            int formid = hcData.PortfolioHealthCheckID.Value;
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID);

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
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }

    #endregion
}