using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.BAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
//using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using Location.DAL;
using UserMgt.DAL;
using DC.DAL;
using System.Web.UI.HtmlControls;

public partial class HC_FormPreview : System.Web.UI.Page
{
    bool Isfirsttime = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["fid"] != null)
            {
                var formid = Request.QueryString["fid"].ToString(); 
                BindControls(Convert.ToInt32(formid));
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void BindControls(int formid)
    {
        
        HealthCheckBAL hbal = new HealthCheckBAL();
        var hform = hbal.HealthCheck_Form_SelectByID(formid);
        var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
        var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
      
        //start table
        Table tbl= new Table();
        tbl.EnableViewState = true;
        tbl.Style.Add("width", "100%");
        tbl.Style.Add("background-color", hform.FormBackColor + " !important;");
        tbl.CssClass = "tblform";
        tbl.CellPadding = 5;
        tbl.CellSpacing = 3;
        //check header is exists
        var pHeader = hpanels.Where(o => o.PanelName == "Header").FirstOrDefault();
        TableRow tr;
        TableHeaderRow th_row;
        TableHeaderCell th_cell;
        TableCell td;
        LiteralControl lc;
        System.Web.UI.WebControls.Image img;
        Table pnltbl;
        if(pHeader != null)
        {
            pnltbl = new Table();
            pnltbl.Style.Add("width", "100%");
            pnltbl.Style.Add("background-color", pHeader.PanelBackColor );
            pnltbl.CssClass = "tblheader";
            pnltbl.CellPadding = 8;
            pnltbl.CellSpacing = 3;
           // var td = null;
            for(int row=1;row <=pHeader.PanelRows;row++)
            {
                tr = new TableRow();
                var colCnt = pHeader.PanelColumns;
               // for(int col=1;col <= pHeader.PanelColumns;col++)
                    for (int col = 1; col <= 1; col++)
                    {
                    td = new TableCell();
                    td.Style.Add("width", (100/colCnt).ToString()+ "%");
                    var cval = hcontrols.Where(o => o.PanelID == pHeader.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                    if (cval != null)
                    {
                        if (!string.IsNullOrEmpty(cval.ControlValue))
                        {
                            img = new System.Web.UI.WebControls.Image();
                            img.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/HC/" + cval.ControlValue + ".png";
                            td.Controls.Add(img);
                        }
                        else
                        {
                            img = new System.Web.UI.WebControls.Image();
                            img.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Customers/portfolio_" + sessionKeys.PortfolioID + ".png";
                            td.Controls.Add(img);
                            //var lblHead = new Label();
                            //lblHead.Text = string.Empty;
                            //td.Controls.Add(lblHead);

                        }
                    }
                    else
                    {
                        img = new System.Web.UI.WebControls.Image();
                        img.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Customers/portfolio_" + sessionKeys.PortfolioID + ".png";
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
        var plist = hpanels.Where(o => o.PanelName != "Header" && o.PanelName != "Signature Panel").OrderBy(a=>a.PnlPosition).ToList();
        foreach (var pnl in plist)
        {
            if (pnl != null)
            {
               var LblPanel=new Label();
                LblPanel.Font.Bold = true;
                LblPanel.Text = pnl.PanelName;
                LblPanel.Font.Size = 10;
                LblPanel.CssClass = "tblcontrol";

                pnltbl = new Table();
                pnltbl.Style.Add("width", "100%");
                pnltbl.Style.Add("background-color", pnl.PanelBackColor);
                pnltbl.CssClass = "tblcontrol";
                pnltbl.CellPadding = 8;
                pnltbl.CellSpacing = 3;
                
                //pnltbl.Caption = LblPanel.Text;
                //pnltbl.CaptionAlign = TableCaptionAlign.Left;
                // var td = null;
                th_row = new TableHeaderRow();
                string[] s = { "System Components", "Premium Comfort Equipment" };
                //if (s.Contains(pnl.PanelName))
                    th_cell = new TableHeaderCell() { ColumnSpan=2};
                //else
                //th_cell = new TableHeaderCell();
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
                            //string ImgName = string.Empty;
                            //using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                            //{
                            //    ImgName = Hdc.HealthCheck_FormDatas.Where(a => a.ControlID == cval.ControlID).Select(a => a.ControlValue).FirstOrDefault();
                            //}
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
                            if (cval.TypeOfField.ToLower() == "date")
                            {
                                td.Controls.Add(GenerateTextboxDate(cval.ControlID.ToString(), (cval != null ? cval.DefaultText : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "textbox")
                            {
                                td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "dropdown")
                            {
                                td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "checkbox")
                            {
                                td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cval != null ? cval.ControlValue : string.Empty), Isfirsttime));
                                tr.Cells.Add(td);
                                td = new TableCell();
                                td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                lc = new LiteralControl(HttpUtility.HtmlDecode(cval.ControlLabelName.ToString()) + "");
                                td.Controls.Add(lc);
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "textarea")
                            {
                                td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "checkboxlist")
                            {
                                td.Controls.Add(GenerateChecklistbox(cval.ListValues, cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cval.CblPosition));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "radiobutton")
                            {
                                td.Controls.Add(GenerateRadioBtnlistbox(cval.ListValues, cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cval.CblPosition));
                                tr.Cells.Add(td);
                            }
                            else if (cval.TypeOfField.ToLower() == "label")
                            {
                                td.Controls.Add(GenerateLable(cval.ControlID.ToString(), cval.DefaultText, cval.ControlPosition, cval.ControlWidth, cval.Height));
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
                                span.ID = cval.ControlID.ToString();
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
                               td.Controls.Add(GenerateLable(cval.ControlID.ToString(), cval.ControlLabelName, cval.ControlPosition, 10, 22));
                               tr.Cells.Add(td);
                               if (cval.TypeOfField.ToLower() == "textbox")
                               {
                                   if (!string.IsNullOrEmpty(cval.TypeOfField))
                                   {
                                    //   td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, 50, cval.ControlPosition, cval.Height));
                                     //  tr.Cells.Add(td);
                                       var txt = new TextBox();
                                       txt.ID = cval.ControlID.ToString();
                                       txt.Text = cval.DefaultText;
                                       txt.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                       txt.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.ToString() + "%" : "100%");
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
        //pnlPerson.Controls.Add(tbl);
        //form1.Controls.Add(tbl);
    }
    Label lbl = null;
    public Label GenerateLable(string id, string lblName, string ControlPosition, int? width, int? Height)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.EnableViewState = true;
        lbl.Style.Add("float", string.IsNullOrEmpty(ControlPosition) ? "left" : ControlPosition);
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
     //   lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        return lbl;
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


    System.Web.UI.WebControls.Image Ima = null;
    public System.Web.UI.WebControls.Image GenerateImage(string id, string Url, string ControlPosition, int? width)
    {
        Ima = new System.Web.UI.WebControls.Image();
        Ima.ID = id.ToString();
        Ima.ImageUrl = "~/WF/UploadData/HC/" + Url + ".png";
        Ima.EnableViewState = true;
        Ima.Style.Add("float", string.IsNullOrEmpty(ControlPosition) ? "left" : ControlPosition);
        Ima.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //Ima.Style.Add("height", "55px");
        return Ima;
    }
    Table t = null;
    TableRow tr = null;
    TableCell tc = null;
    TableHeaderRow HeadR = null;
    TableHeaderCell thc = null;
    public Table GenerateTable(int? row,int? col,string typeoffieldInTbl,string id,int? width,string Rlist,string Clist,int? PanelId)
    {
        string[] Rstr = Rlist.Split(',').ToArray();
        string[] Cstr = Clist.Split(',').ToArray();
        int r=Rlist.Split(',').Length;
        int c = Clist.Split(',').Length;

        t = new Table();
        tr = new TableRow();
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
        var formid = Request.QueryString["fid"].ToString(); 
        var Tblcontrols = hbal.HealthCheck_FormControl_SelectByFormID(int.Parse(formid));
        var TblCntlList = Tblcontrols.Where(o => o.PanelID == PanelId && o.RowIndex == row && o.ColumnIndex == col).ToList();
        int count = 0;
            for (int i = 1; i <= r; i++)
            {
                tr = new TableRow();
                for (int j = 0; j <= c; j++)
                {
                    if (j != 0)
                    {
                        tc = new TableCell();
                        if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "textbox")
                        {
                            var txt = new TextBox();
                            txt.ID = TblCntlList[count].ControlID.ToString();
                            txt.Text = string.Empty;
                            txt.Style.Add("width", width.HasValue ? width.ToString() + "%" : "100%");
                            txt.Style.Add("float", "right");
                            tc.Controls.Add(txt);
                            tr.Cells.Add(tc);
                        }
                        else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "checkbox")
                        {
                            var checkBox = new CheckBox();
                            checkBox.ID = TblCntlList[count].ControlID.ToString();
                            checkBox.Text = string.Empty;
                            checkBox.Style.Add("float", "right");
                            tc.Controls.Add(checkBox);
                            tr.Cells.Add(tc);
                        }
                        else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "radiobutton")
                        {
                            var Rbtn = new RadioButton();
                            Rbtn.ID = TblCntlList[count].ControlID.ToString();
                            Rbtn.Text = string.Empty;
                            Rbtn.Style.Add("float", "right");
                            tc.Controls.Add(Rbtn);
                            tr.Cells.Add(tc);
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
                        tr.Cells.Add(tc);
                    }
                 
                }
                t.Rows.Add(tr);
            }
            return t;
    }
    Button Btn = null;
    public Button GenerateButton(string id)
    {
        Btn = new Button();
        Btn.ID = "lbl" + id.ToString();
        Btn.Text = "Upload";
        Btn.OnClientClick = "";
        Btn.EnableViewState = true;
        return Btn;
    }
    FileUpload Fload = null;
    public FileUpload GenerateFileupload(string id)
    {
        Fload = new FileUpload();
        Fload.ID = "File" + id.ToString();
        Fload.EnableViewState = true;
        return Fload;
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
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    public TextBox GenerateTextbox(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue? width.Value.ToString()+"%":"100%");
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
    public CheckBoxList GenerateChecklistbox(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height,string Rblist)
    {
        chl = new CheckBoxList();
        chl.ID = id;
        //ddl.Width = 200;
        chl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
      //  chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        chl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        chl.RepeatLayout = RepeatLayout.Table;
        if (Rblist == "Horizontal")
        {
            chl.RepeatDirection = RepeatDirection.Horizontal;
        }
        else
        {
            chl.RepeatDirection = RepeatDirection.Vertical;
        }
        int num1 = 2;

        if (int.TryParse(setvalue, out num1))
        {

        }
        chl.RepeatColumns = num1;
        //if (setvalue != string.Empty)
        //{
        //    chl.RepeatColumns = int.Parse(setvalue);
        //}
        //else
        //{
        //    chl.RepeatColumns = 2;
        //}
        chl.CellSpacing = 3;
        chl.CellPadding = 3;
        string[] str = Items.Split(',').ToArray();
        foreach (string s in str)
            chl.Items.Add(s);
        chl.EnableViewState = true;
        //chl.SelectedIndexChanged += ddl_SelectedIndexChanged;
       // chl.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chl.SelectedValue = setvalue;
        //ddlid = ddlid + 1;
        return chl;
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
                Rbl.SelectedValue = setvalue;
            //ddlid = ddlid + 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Rbl;
    }




    DropDownList ddl;
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        ddl = new DropDownList();
        ddl.ID = id;
        //ddl.Width = 200;
        ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        ddl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        // ddl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        //if (string.IsNullOrEmpty(setvalue))
        //{
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
            {
                ddl.Items.Add(s);
            }
        //}
        //else
        //{
        //    //if (setvalue == "List of Project Managers")
        //    //{
        //    //    using (UserDataContext Udc = new UserDataContext())
        //    //    {
        //    //        var ProjectManagersList = (from a in Udc.Contractors
        //    //                                   where (a.SID == 1 || a.SID == 2 || a.SID == 3) && (a.Status == "Active")
        //    //                                   select new
        //    //                                   {
        //    //                                       Name = a.ContractorName,
        //    //                                       Value = a.ID
        //    //                                   }).ToList();
        //    //        ddl.DataSource = ProjectManagersList;
        //    //    }
        //    //}
        //    //else if (setvalue == "List of Customer Sites")
        //    //{
        //    //    using (LocationDataContext Ldc = new LocationDataContext())
        //    //    {
        //    //        var OurCustomerSitesList = (from a in Ldc.Sites
        //    //                                    where a.Visible == 'Y'
        //    //                                    select new
        //    //                                    {
        //    //                                        Name = a.Site1,
        //    //                                        Value = a.ID
        //    //                                    }).ToList();
        //    //        ddl.DataSource = OurCustomerSitesList;
        //    //    }
        //    //}
        //    //else if (setvalue == "List of Our Sites")
        //    //{
        //    //    using (DCDataContext Dc = new DCDataContext())
        //    //    {
        //    //        var OurSitesList = (from a in Dc.OurSites
        //    //                            select new
        //    //                            {
        //    //                                Name = a.Name,
        //    //                                Value = a.ID
        //    //                            }).ToList();
        //    //        if (sessionKeys.PortfolioID > 0)
        //    //        {
        //    //            OurSitesList = (from a in Dc.OurSites
        //    //                            where a.CustomerID == sessionKeys.PortfolioID
        //    //                            select new
        //    //                            {
        //    //                                Name = a.Name,
        //    //                                Value = a.ID
        //    //                            }).ToList();
        //    //        }
        //    //        ddl.DataSource = OurSitesList;
        //    //    }
        //    //}
        //    //else if (setvalue == "List of Resources")
        //    //{
        //    //    using (UserDataContext Udc = new UserDataContext())
        //    //    {
        //    //        var ResourcesList = (from a in Udc.Contractors
        //    //                             where (a.SID == 4 || a.SID == 9) && (a.Status == "Active")
        //    //                             select new
        //    //                             {
        //    //                                 Name = a.ContractorName,
        //    //                                 Value = a.ID
        //    //                             }).ToList();
        //    //        ddl.DataSource = ResourcesList;
        //    //    }
        //    //}
        //    //else if (setvalue == "List of Administrators")
        //    //{
        //    //    UserDataContext Udc = new UserDataContext();
        //    //    //string[] AdministratorsList = Udc.Contractors.Where(a => a.SID == 1 || a.SID == 2 || a.SID == 3).Select(a=>a.ContractorName).ToArray();

        //    //    var AdministratorsList = (from a in Udc.Contractors
        //    //                              where a.SID == 1 && a.Status == "Active"
        //    //                              select new
        //    //                              {
        //    //                                  Name = a.ContractorName,
        //    //                                  Value = a.ID
        //    //                              }).ToList();
        //    //    ddl.DataSource = AdministratorsList;
        //    //}
        //    //ddl.DataValueField = "Value";
        //    //ddl.DataTextField = "Name";
        //    //ddl.DataBind();
        //}
        ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        //ddl.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            ddl.SelectedValue = setvalue;
        //ddlid = ddlid + 1;
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

    CheckBox chk;
    public CheckBox GenerateCheckBox(string id, string setvalue, bool Isfirsttime)
    {
        chk = new CheckBox();
        chk.ID = id;
        //chk.CssClass = "mobilesubtitle";
        //txt.Width = 200;
        //chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue)?"False":setvalue);
        chk.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue) ? "False" : setvalue);
        //txtid = txtid + 1;
        return chk;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=ConvertedPDF.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        this.printdiv.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        string PDF_FileName = Server.MapPath("~") + "/App_Data/PDF_File.pdf";
        //new FileStream(PDF_FileName, FileMode.Create)
        PdfWriter.GetInstance(pdfDoc, new FileStream(PDF_FileName, FileMode.Create));//, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        
      
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        var wkhtmltopdfLocation = Server.MapPath("~/bin/") + "wkhtmltopdf.exe";
        var htmlUrl = @"http://localhost:49316/HC/FormPreview.aspx?fid=19";
        var pdfSaveLocation = "\"" + Server.MapPath("~/WF/UploadData/") + "question.pdf\"";

        var process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.FileName = wkhtmltopdfLocation;
        process.StartInfo.Arguments = htmlUrl + " " + pdfSaveLocation;
        process.Start();
        process.WaitForExit();
      
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        if(Request.QueryString["fid"] != null)
        Response.Redirect(string.Format("~/WF/Health/HC/FormDesign.aspx?fid={0}", Request.QueryString["fid"].ToString()), false);
    }
}