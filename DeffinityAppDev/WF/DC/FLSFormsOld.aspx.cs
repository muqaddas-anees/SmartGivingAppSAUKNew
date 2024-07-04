using AjaxControlToolkit;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class FLSForms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["msg"] != null)
                {
                    lblMsg.Text = Session["msg"].ToString();
                    Session["msg"] = null;
                }
                if (QueryStringValues.CallID > 0)
                    lblTitle.InnerText = "Form" + " - Job Reference " + QueryStringValues.CallID;
                else
                    lblTitle.InnerText = "Form";
                if (!IsPostBack)
                {
                    BindForms();
                    //set form
                    SetForm(QueryStringValues.CallID);
                    //apply form
                   
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var v = dc.FLSFormToCalls.Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                        if (v != null)
                        {
                            ddlForms.SelectedValue = Convert.ToString(v.FormID);
                            imgCustomFieldUpdate.Visible = true;
                        }
                        else
                            imgCustomFieldUpdate.Visible = false;
                    }
                }
                if(sessionKeys.PortfolioID >0 && Convert.ToInt32(ddlForms.SelectedValue) >0)
                BindPlaceholderFields(sessionKeys.PortfolioID, Convert.ToInt32(ddlForms.SelectedValue));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindForms()
        {
            try
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    var flist = dc.FLSForms.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                    ddlForms.DataSource = flist;
                    ddlForms.DataTextField = "FormName";
                    ddlForms.DataValueField = "ID";
                    ddlForms.DataBind();
                    ddlForms.Items.Insert(0, new ListItem("Please select...", "0"));
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SetForm(int callid)
        {
            var formid = 0;
            using (DCDataContext dc = new DCDataContext())
            {
                var fdata = dc.FLSFormToCalls.Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                if (fdata == null)
                {
                    var cDetails = dc.FLSDetails.Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();

                    var aDetails = dc.FLSForms.Where(o => o.AssignedTypeOfRequestID == cDetails.RequestType).FirstOrDefault();
                    if (aDetails != null)
                    {
                        formid = aDetails.ID;

                        ddlForms.SelectedValue = formid.ToString();
                       

                        SaveHealthForm(QueryStringValues.CallID);
                    }
                }
            }
            }
        public void SaveHealthForm(int CallID)
        {
            try
            {
               
                if (Convert.ToInt32(ddlForms.SelectedValue) > 0)
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        

                        var fdata = dc.FLSFormToCalls.Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                        if (fdata != null)
                        {
                            //if (fdata.FormID != Convert.ToInt32(ddlForms.SelectedValue))
                            //{
                            //dc.FLSFormToCalls.DeleteOnSubmit(fdata);
                           // dc.SubmitChanges();
                           // FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID_Delete(QueryStringValues.CallID);

                            //var d = new FLSFormToCall();
                            //d.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                            //d.CallID = QueryStringValues.CallID;
                            //dc.FLSFormToCalls.InsertOnSubmit(d);
                            //dc.SubmitChanges();
                           
                            //Session["msg"] = "Form applied successfully";
                            // }
                        }
                        else
                        {
                            var d = new FLSFormToCall();
                            d.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                            d.CallID = QueryStringValues.CallID;
                            dc.FLSFormToCalls.InsertOnSubmit(d);
                            dc.SubmitChanges();
                            Response.Redirect(Request.RawUrl);
                            //Session["msg"] = "Form applied successfully";
                        }
                        
                        //var retval = hb.HealthCheck_FormAssignToCall_Add(new HealthCheck_FormAssignToCall() { CallID = CallID, FormID = Convert.ToInt32(ddlForms.SelectedValue) });

                    }
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            SaveHealthForm(QueryStringValues.CallID);
        }
        protected void imgCustomFieldUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // FLS custom fields data insert
                int id = Convert.ToInt32(Request.QueryString["callid"]);
                SavePlaceholderData(id, sessionKeys.PortfolioID, Convert.ToInt32(ddlForms.SelectedValue));
                pnlCustomFields.Visible = false;
                Session["msg"] = Resources.DeffinityRes.UpdatedSuccessfully;
                Response.Redirect(Request.RawUrl);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region FLS Custom form designer

        TextBox txt = null;
        DropDownList ddl = null;
        Label lbl = null;
        Button btn = null;
        RadioButtonList rbn = null;
        CheckBox chk = null;

        int txtid = 1;
        int ddlid = 1;
        int lblid = 1;
        int btnid = 1;
        int rbtnid = 1;
        int chkid = 1;

        string[] typeOfFields = new string[] { "text box", "number field", "date", "text area", "url" };

        public void BindPlaceholderFields(int customerId,int formid)
        {
            int cid = 0;
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
                if (Request.QueryString["callid"] != null)
                {
                    cid = int.Parse(Request.QueryString["callid"]);
                }


                List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(cid);

                List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(sessionKeys.PortfolioID,formid).ToList();
                hcount.Value = clist.Count().ToString();
                Table tbl = new Table();
                tbl.EnableViewState = true;
                tbl.Style.Add("width", "100%");
                TableRow tr = new TableRow();
                TableCell td = null;
                int cnt = 0;
                int jcnt = 1;
                int totalCnt = clist.Count();
                if (clist.Count > 0)
                {
                    //if (cid > 0)
                    //{
                    //pnlCustomFields.Visible = false;
                    //lblCustomFiledCustomer.Text = GetCompanyName(Convert.ToInt32(customerId));
                    //}
                    //else
                    //{
                    //    pnlCustomFields.Visible = false;
                    //}
                }
                else
                    pnlCustomFields.Visible = false;

                foreach (FLSCustomField c in clist)
                {
                    string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                    string rval = string.Empty;
                    if (val != null)
                        rval = val.ToString();

                    //if (tr == null)
                       

                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        tbl = new Table();
                        tbl.CssClass = "well";
                        tbl.Style.Add("width", "100%");
                        tr = new TableRow();
                        td = new TableCell();
                        tr.ID = "lbl_"+cnt.ToString();
                        tr.CssClass = "tr"+ cnt.ToString();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        //td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        td.Style.Add("font-size", "large");

                        tr.Cells.Add(td);
                        //add to table
                        tbl.Rows.Add(tr);
                        tr = new TableRow();
                        tr.CssClass = "tr" + cnt.ToString();
                        tr.ID = "ctr_" + cnt.ToString();
                        td = new TableCell();
                        td.Controls.Add(GenerateTextbox(c.ID.ToString(), rval, val, Isfirsttime, c.TypeOfField.ToLower(), Convert.ToBoolean(c.Mandatory), c.LabelName, c.MinimumValue, c.MaximumValue, c.DefaultText));
                        if (c.TypeOfField.ToLower() == "date")
                        {
                            td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));

                        }
                        //td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        td.Style.Add("padding-bottom", "30px");
                        tr.Cells.Add(td);
                        tbl.Rows.Add(tr);
                        ph.Controls.Add(tbl);
                       
                    }

                    else if (c.TypeOfField.ToLower() == "dropdown list")
                    {
                        tbl = new Table();
                        tbl.CssClass = "well";
                        tbl.Style.Add("width", "100%");
                        tr = new TableRow();
                        td = new TableCell();
                        tr.ID = "lbl_" + cnt.ToString();
                        tr.CssClass = "tr" + cnt.ToString();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        //td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td.Style.Add("font-size", "large");
                        //add to table
                        tbl.Rows.Add(tr);
                        tr = new TableRow();
                        tr.CssClass = "tr" + cnt.ToString();
                        tr.ID = "ctr_" + cnt.ToString();
                        td = new TableCell();
                        td.Controls.Add(GenerateDropDown(c.ListValue, c.ID.ToString(), rval, Isfirsttime, Convert.ToBoolean(c.Mandatory), c.LabelName));
                        //td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        td.Style.Add("padding-bottom", "30px");
                        tr.Cells.Add(td);
                        tbl.Rows.Add(tr);
                        ph.Controls.Add(tbl);
                    }
                    else if (c.TypeOfField.ToLower() == "radio button")
                    {
                        tbl = new Table();
                        tbl.CssClass = "well";
                        tbl.Style.Add("width", "100%");
                        tr = new TableRow();
                        td = new TableCell();
                        tr.ID = "lbl_" + cnt.ToString();
                        tr.CssClass = "tr" + cnt.ToString();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        //td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td.Style.Add("font-size", "large");
                        //add to table
                        tbl.Rows.Add(tr);
                        tr = new TableRow();
                        tr.CssClass = "tr" + cnt.ToString();
                        tr.ID = "ctr_" + cnt.ToString();
                        td = new TableCell();
                        td.Controls.Add(GenerateRadioButton(c.ListValue, c.ID.ToString(), rval, Isfirsttime));
                        //td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        td.Style.Add("padding-bottom", "30px");
                        tr.Cells.Add(td);
                        tbl.Rows.Add(tr);
                        ph.Controls.Add(tbl);
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        tbl = new Table();
                        tbl.CssClass = "well";
                        tbl.Style.Add("width", "100%");
                        tr = new TableRow();
                        td = new TableCell();
                        tr.ID = "lbl_" + cnt.ToString();
                        tr.CssClass = "tr" + cnt.ToString();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        //td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td.Style.Add("font-size", "large");
                        //add to table
                        tbl.Rows.Add(tr);
                        tr = new TableRow();
                        tr.CssClass = "tr" + cnt.ToString();
                        tr.ID = "ctr_" + cnt.ToString();
                        td = new TableCell();
                        td.Controls.Add(GenerateCheckbox(c.ID.ToString(), rval, Isfirsttime));
                        //td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        td.Style.Add("padding-bottom", "30px");
                        tr.Cells.Add(td);
                        tbl.Rows.Add(tr);

                        ph.Controls.Add(tbl);
                    }
                    cnt = cnt + 1;
                    //tbl.Rows.Add(tr);
                    //tr = null;
                    //if (cnt == 2)
                    //{
                    //    tbl.Rows.Add(tr);
                    //    tr = null;
                    //    cnt = 0;
                    //}
                    //if (jcnt == totalCnt && cnt == 1)
                    //{
                    //    tbl.Rows.Add(tr);
                    //    tr = null;
                    //}
                    jcnt = jcnt + 1;
                }
                //ph.Controls.Add(tbl);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        // string validationGroup = "Custom";
        string validationGroup = "fls";
        public TextBox GenerateTextbox(string id, string setvalue, string val, bool Isfirsttime, string type, bool isMandatory, string labelName, string minValue, string maxValue, string defaultText)
        {

            txt = new TextBox();
            txt.ID = id;
            //txt.Width = 200;
            txt.SkinID = "txt_80";
            txt.Text = setvalue;

            if (type == "text area")
                txt.TextMode = TextBoxMode.MultiLine;
            txt.EnableViewState = true;

            if (val == null)
            {
                txt.Text = defaultText;
            }
            if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
                txt.Text = setvalue;


            // when we create fls new form set defult text from admin
            if (Request.QueryString["callid"] == null)
            {

            }
            //Validator settings
            if (isMandatory)
            {
                RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();

                requiredFieldValidator.ControlToValidate = txt.ID;
                requiredFieldValidator.ErrorMessage = "Please enter " + labelName.ToLower();
                requiredFieldValidator.SetFocusOnError = true;
                //rfv.Text = "*";
                requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                requiredFieldValidator.ValidationGroup = validationGroup;

                ph.Controls.Add(requiredFieldValidator);
            }
            if (type == "number field")
            {
                RangeValidator rangeValidator = new RangeValidator();
                rangeValidator.MinimumValue = minValue;
                rangeValidator.MaximumValue = maxValue;
                rangeValidator.ControlToValidate = txt.ID;
                rangeValidator.Type = ValidationDataType.Double;
                rangeValidator.SetFocusOnError = true;
                rangeValidator.ValidationGroup = validationGroup;
                rangeValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                rangeValidator.ErrorMessage = "The " + labelName.ToLower() + " must be " + rangeValidator.MinimumValue + " to " + rangeValidator.MaximumValue;

                ph.Controls.Add(rangeValidator);
            }
            if (type == "date")
            {
                txt.Width = 80;

                CalendarExtender calendarExtender = new CalendarExtender();
                calendarExtender.PopupButtonID = "imgDate" + id;
                calendarExtender.Format = Deffinity.systemdefaults.GetDateformat();
                calendarExtender.TargetControlID = txt.ID;
                calendarExtender.CssClass = "MyCalendar";
                ph.Controls.Add(calendarExtender);


                RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                regularExpressionValidator.ControlToValidate = txt.ID;
                regularExpressionValidator.SetFocusOnError = true;
                regularExpressionValidator.Display = ValidatorDisplay.None;
                regularExpressionValidator.ErrorMessage = "Please enter a valid " + labelName.ToLower();
                regularExpressionValidator.ValidationExpression = "(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\\d\\d";
                regularExpressionValidator.ValidationGroup = validationGroup;
                ph.Controls.Add(regularExpressionValidator);
            }

            txtid = txtid + 1;
            return txt;
        }

        public Image GenerateCalendarImageButton(string id)
        {
            Image img = new Image();
            img.ID = "imgDate" + id;
            img.ImageAlign = ImageAlign.AbsMiddle;
            img.ImageUrl = "~/WF/DC/media/icon_calender.png";
            return img;
        }

        public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, bool isMandatory, string labelName)
        {
            ddl = new DropDownList();
            ddl.ID = id;
            //ddl.Width = 200;
            ddl.SkinID = "ddl_80";
            string[] str = Regex.Split(Items, @"\s*,\s*");
            // The regex \s*,\s* can be read as: "match zero or more white space characters,
            //followed by a comma followed by zero or more white space characters".
            // http://stackoverflow.com/questions/1483645/what-is-the-cleanest-way-to-remove-all-extra-spaces-from-a-user-input-comma-deli
            System.Array.Sort(str);
            foreach (string s in str)
                ddl.Items.Add(s);
            ddl.Items.Insert(0, new ListItem("Please select...", "0"));
            ddl.EnableViewState = true;
            ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            ddl.AutoPostBack = true;
            if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
                ddl.SelectedValue = setvalue;

            //RequiredFieldValidator setting
            if (isMandatory)
            {
                RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                requiredFieldValidator.ControlToValidate = ddl.ID;
                requiredFieldValidator.ErrorMessage = "Please select " + labelName.ToLower();
                requiredFieldValidator.SetFocusOnError = true;
                //rfv.Text = "*";
                requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                requiredFieldValidator.ValidationGroup = validationGroup;
                requiredFieldValidator.InitialValue = "0";
                ddl.ValidationGroup = validationGroup;
                ph.Controls.Add(requiredFieldValidator);
            }

            ddlid = ddlid + 1;
            return ddl;
        }

        public RadioButtonList GenerateRadioButton(string Items, string id, string setvalue, bool Isfirsttime)
        {
            rbn = new RadioButtonList();
            rbn.ID = id;
            rbn.Width = 200;
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                rbn.Items.Add(s);
            rbn.EnableViewState = true;
            rbn.SelectedIndexChanged += rbn_SelectedIndexChanged;
            rbn.AutoPostBack = true;
            if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
                rbn.SelectedValue = setvalue;
            rbtnid = rbtnid + 1;
            return rbn;

        }

        public CheckBox GenerateCheckbox(string id, string setvalue, bool Isfirsttime)
        {
            bool val;
            if (string.IsNullOrEmpty(setvalue))
            {
                val = false;
            }
            else
            {
                val = bool.Parse(setvalue);
            }
            chk = new CheckBox();
            chk.ID = id;
            //chk.Width = 200;
            chk.Checked = val;

            chk.EnableViewState = true;
            if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
                chk.Checked = val;
            chkid = chkid + 1;
            return chk;
        }

        public void rbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dval = (RadioButtonList)sender;
            if (dval.SelectedIndex > 0)
            {
                string s = dval.SelectedValue;
            }
        }

        public void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dval = (DropDownList)sender;
            if (dval.SelectedIndex > 0)
            {
                string s = dval.SelectedValue;
            }
        }

        public Button GenerateButton(string lblName)
        {
            btn = new Button();
            btn.ID = "btn" + lblid.ToString();
            btn.Text = lblName;
            btn.EnableViewState = true;
            btnid = btnid + 1;
            return btn;
        }
        public Label GenerateLable(string lblName)
        {
            lbl = new Label();
            lbl.ID = "lbl" + lblid.ToString();
            lbl.Text = lblName;
            lbl.EnableViewState = true;
            lblid = lblid + 1;
            return lbl;
        }

        FLSAdditionalInfo flsAdditionalInfo = null;

        public void SavePlaceholderData(int callId, int customerId,int formid)
        {
            try
            {
                ViewState["state"] = "2";


                List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(sessionKeys.PortfolioID, formid).ToList();

                foreach (FLSCustomField c in clist)
                {
                    flsAdditionalInfo = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(callId).Where(p => p.CustomFieldID == c.ID).FirstOrDefault();
                    if (flsAdditionalInfo == null)
                    {
                        flsAdditionalInfo = new FLSAdditionalInfo();
                        flsAdditionalInfo.CallID = callId;
                        if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                        {
                            var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                            if (txt != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = txt.Text;
                            }

                        }
                        else
                            if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                        flsAdditionalInfo.CustomFieldID = c.ID;
                        FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(flsAdditionalInfo);

                    }
                    else
                    {

                        if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                        {
                            var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                            if (txt != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = txt.Text;
                            }

                        }
                        else
                            if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                        FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(flsAdditionalInfo);
                    }
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

        }

        #endregion


    }
}