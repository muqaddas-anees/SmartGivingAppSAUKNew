using AjaxControlToolkit;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DC.BLL;
using DC.Entity;
using DeffinityManager.PortfolioMgt;
using DocumentFormat.OpenXml.Wordprocessing;
using StreamChat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TuesPechkin;
using UserMgt.BAL;
using UserMgt.Entity;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Image = System.Web.UI.WebControls.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using Table = System.Web.UI.WebControls.Table;
using TableCell = System.Web.UI.WebControls.TableCell;
using TableRow = System.Web.UI.WebControls.TableRow;
using UserMgt.DAL;
using System.Collections;

namespace DeffinityAppDev.App
{
    public partial class Member : System.Web.UI.Page
    {
        int CID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
{

                    if(sessionKeys.ErrorMessage.Length >0)
                    {
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, sessionKeys.ErrorMessage, "Ok");
                        sessionKeys.ErrorMessage = "";

                    }

                    string addParam = Request.QueryString["add"];

                    if (addParam != null && addParam.Equals("donor", StringComparison.OrdinalIgnoreCase))
                    {
                        // Hide the RadioButtonList control
                        ddlPermission.Visible = false;
                    }
                    ddlPermission.SelectedValue = "2";
                    BindCountry();
                    ddlCountry.SelectedValue = Deffinity.systemdefaults.GetCoutryID();
                    txtCompanyPhone.Text = Deffinity.systemdefaults.GetCountryCode();
                    //BindReligion();
                   // BindDenomination(0);
                    BindCOmpany();
                    pnlDeactive.Visible = false;


                    if (QueryStringValues.Type == "2")
                    {
                        lblPageTitle.Text = "Donor Details";
                        btnBack.Text = "Back to Donors";
                        lblSection.Text = "Donor";
                        lblsubtitle.Text = "Donor Details";
                        ddlPermission.SelectedValue = "2";
                        btnBack.NavigateUrl = "~/App/Members.aspx?type=2";
                        pnlPasswordTextbox.Visible = false;
                        pnlUserType.Visible = false;
                        pnlStatus.Visible = true;
                    }
                    else
                    {
                        lblPageTitle.Text = "Member Details";
                        btnBack.Text = "Back to Members";
                        lblSection.Text = "Member";
                        lblsubtitle.Text = "Member Details";
                        ddlPermission.SelectedValue = "1";
                        btnBack.NavigateUrl = "~/App/Members.aspx?type=1";
                        pnlPasswordTextbox.Visible = true;
                        pnlUserType.Visible = true;
                        pnlStatus.Visible = true;

                    }


                    if (Request.QueryString["mid"] != null)
                    {
                        pnlDeactive.Visible = false;
                        pnlPassword.Visible = false;
                        pnlskills.Visible = true;
                        pnlDocuments.Visible = true;
                        pnlDonations.Visible = true;    
                        pnlCommunication.Visible = false;

                        var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                        CID = uid;
                        NewMethod(uid);
                        Gridfilesbind(uid.ToString());
                        BindSkills();
                        BindTages();
                        BindGrid();
                        BindCommunicationGrid();
                    }
                    else
                    {
                        pnlDeactive.Visible = false;
                        pnlPassword.Visible = false;
                        pnlskills.Visible = false;
                        pnlDocuments.Visible = false;
                        pnlDonations.Visible = false;
                        pnlCommunication.Visible = false;

                    }

                  
                    if(sessionKeys.SID == 1)
                    {
                        pnlUserType.Visible = true;
                    }
                    else
                    {
                        pnlUserType.Visible = false;
                    }

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }


                    imageDiv.Style["background-image"] = "url('"+ "/ImageHandler.ashx?id=" + QueryStringValues.MID.ToString() + "&s=" + ImageManager.file_section_user+"')";
                }

                if(QueryStringValues.Type.Length >0)
                {
                    btnBack.Visible = true;
                }

                //hide for fundrisers
                if(sessionKeys.SID == UserType.Fundraiser)
                {
                    btnBack.Visible = false;
                }

                BindPlaceholderFields(sessionKeys.PortfolioID);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region FLS Custom form designer

        TextBox txt = null;
        DropDownList ddl = null;
        Label lbl = null;
        RadioButtonList rbn = null;
        CheckBox chk = null;

        int txtid = 1;
        int ddlid = 1;
        int lblid = 1;
        int rbtnid = 1;
        int chkid = 1;

        string[] typeOfFields = new string[] { "text box", "number field", "date", "text area", "url" };

        public void BindPlaceholderFields(int customerId)
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
               

                List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(QueryStringValues.MID);

                List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId, 0).ToList();
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
                    pnlCustomfields.Visible = false;

                foreach (FLSCustomField c in clist)
                {
                    string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                    string rval = string.Empty;
                    if (val != null)
                        rval = val.ToString();

                    if (tr == null)
                        tr = new TableRow();

                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        td = new TableCell();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Controls.Add(GenerateTextbox(c.ID.ToString(), rval, val, Isfirsttime, c.TypeOfField.ToLower(), Convert.ToBoolean(c.Mandatory), c.LabelName, c.MinimumValue, c.MaximumValue, c.DefaultText));
                        if (c.TypeOfField.ToLower() == "date")
                        {
                            td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));

                        }
                        td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);

                    }

                    else if (c.TypeOfField.ToLower() == "dropdown list")
                    {
                        td = new TableCell();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Controls.Add(GenerateDropDown(c.ListValue, c.ID.ToString(), rval, Isfirsttime, Convert.ToBoolean(c.Mandatory), c.LabelName));
                        td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                    }
                    else if (c.TypeOfField.ToLower() == "radio button")
                    {
                        td = new TableCell();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Controls.Add(GenerateRadioButton(c.ListValue, c.ID.ToString(), rval, Isfirsttime));
                        td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        td = new TableCell();
                        td.Controls.Add(GenerateLable(c.LabelName));
                        td.Style.Add("width", "250px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                        td = new TableCell();
                        td.Controls.Add(GenerateCheckbox(c.ID.ToString(), rval, Isfirsttime));
                        td.Style.Add("width", "390px");
                        td.Style.Add("padding", "5px");
                        tr.Cells.Add(td);
                    }
                    cnt = cnt + 1;
                    if (cnt == 2)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                        cnt = 0;
                    }
                    if (jcnt == totalCnt && cnt == 1)
                    {
                        tbl.Rows.Add(tr);
                        tr = null;
                    }
                    jcnt = jcnt + 1;
                }
                ph.Controls.Add(tbl);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        // string validationGroup = "Custom";
        string validationGroup = "group1";
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
                requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
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
                rangeValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
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
                requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
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

        public void SavePlaceholderData(int callId, int customerId)
        {
            try
            {
                ViewState["state"] = "2";


                List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId, 0).ToList();

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

                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "Ok");
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

        }

        #endregion
        private void BindCommunicationGrid()
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                var rtOutput = rpNew.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.MailTo.Trim().ToLower() == txtEmailAddress.Text.ToLower().Trim()).ToList();

                GridCommunication.DataSource = (from r in rtOutput
                                                orderby r.ID descending
                                                select new
                                                {
                                                    r.ID,
                                                    Subject =r.MailSubject,
                                                    r.MailTo,
                                                    SentDateTime = string.Format( Deffinity.systemdefaults.GetStringDateTimeformat() ,  r.SentOn),
                                                }).ToList();
                GridCommunication.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string getUserData(List<UserMgt.Entity.Contractor> ulist, int loggedby, string check_value, string nameOrEmail)
        {
            string retval = "";


            if (loggedby == 0)
                retval = check_value;
            else
            {
                var eDetails = ulist.Where(o => o.ID == loggedby).FirstOrDefault();
                if (eDetails != null)
                {
                    if (nameOrEmail == "name")
                        retval = eDetails.ContractorName;
                    else
                        retval = eDetails.EmailAddress;
                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }

        private string getTithing(List<PortfolioMgt.Entity.TithingDefaultDetail> tulist, int id)
        {
            string retval = "";


            if (id == 0)
                retval = "";
            else
            {
                var eDetails = tulist.Where(o => o.ID == id).FirstOrDefault();
                if (eDetails != null)
                {

                    retval = eDetails.Title;

                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }
        private void BindGrid()
        {
            try
            {
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o=>o.DonerEmail != null).ToList();

                tList = tList.Where(o => o.DonerEmail.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();

                if (QueryStringValues.Type == "cash")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "cash"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.DetailsOfDonation,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.CheckNumber,
                                     PaymentType = "Cash",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();
                }
                else if (QueryStringValues.Type == "inkind")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "inkind"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.DetailsOfDonation,
                                     PaidDate = t.PaidDate,
                                     PayRef = String.Empty,
                                     PaymentType = "In Kind",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                }
                else
                {
                    //tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var rlist = (from t in tList
                                     //join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                      PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                 }).ToList();

                  
                        GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                        GridDashboard.DataBind();

                    if (rlist.Count > 0)
                    {
                        pnl_TranstactionDetails.Visible = true;
                        var dItem = rlist.OrderByDescending(o => o.ID).FirstOrDefault();
                        if (dItem != null)
                        {
                           
                            lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                            lblStatus.Text = dItem.Status;
                            txtname.Text = dItem.Name;
                            txttype.Text = dItem.PaymentType;
                            txtemail.Text = dItem.Email;
                            lblCategories.Text = dItem.CategoryListWithAmount;
                            txtMethod.Text = dItem.PaymentType;
                            lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                            lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                            // txtNotes.Text = dItem.Notes;
                            //hunid.Value = dItem.unid;
                            // Gridfilesbind();
                        }
                    }
                    else
                    {
                        pnl_TranstactionDetails.Visible = false;
                    }


                }

                //if (rlist.Count > 0)
                //{
                //    //lblthisweek.Text = string.Format("{0:F2}", rlist.Sum(o=>o.Amount.HasValue?o.Amount.Value:0));
                //    //lblthismonth.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }
        private string GetDonationCategories(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f.Split(':').First() + " " + "<br/>";
                        }
                    }
                }
            }

            return retval;

        }

        private string GetDonationCategoriesWithAmount(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f.Split(':').First() + "   :   <b>" + string.Format("{0:F2}", Convert.ToDouble(f.Split(':').Last() != null ? f.Split(':').Last() : "0.00")) + "</b><br/>";
                        }
                    }
                }
            }

            return retval;

        }
        private void NewMethod(int uid)
        {
            try
            {
                var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == uid).FirstOrDefault();
                if (cDetails != null)
                {
                    txtFirstName.Text = cDetails.ContractorName;
                    txtSurname.Text = cDetails.LastName;
                    txtAddress.Text = cDetails.Address1;
                    txtState.Text = cDetails.State;
                    txtTown.Text = cDetails.Town;
                    txtEmailAddress.Text = cDetails.EmailAddress;
                    lblEmail.Text = cDetails.EmailAddress;
                    txtemailaddress_update.Value = cDetails.EmailAddress;
                    //txtcompany.Text = cDetails.Company;
                    try
                    {
                        if (cDetails.Company.Trim().Length > 0)
                            ddlCompany.SelectedValue = cDetails.Company.Trim();
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    txtContactNumber.Text = cDetails.ContactNumber;
                    txtZipcode.Text = cDetails.PostCode;
                    ddlCountry.SelectedValue = (cDetails.Country.HasValue ? cDetails.Country.Value : 0).ToString();
                    //ddlReligion.SelectedValue = cDetails.DenominationDetailsID.ToString();
                   // BindDenomination(cDetails.DenominationDetailsID);
                    ddlDenimination.SelectedValue = cDetails.SubDenominationDetailsID.ToString();
                    img.ImageUrl = "~/ImageHandler.ashx?id=" + cDetails.ID.ToString() + "&s=" + ImageManager.file_section_user; //GetImageUrl(cDetails.ID.ToString());
                    ddlPermission.SelectedValue = cDetails.SID.Value.ToString();
                    ddlStatus.SelectedValue = cDetails.Status;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        private int SaveUserData()
        {
            int userid = 0;
            if (Request.QueryString["mid"] != null)
            {
                var cRep = new UserRepository<Contractor>();
                var uRep = new UserRepository<UserDetail>();
                var cvRep = new UserRepository<v_contractor>();
                var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == uid).FirstOrDefault();
                if (cDetails != null)
                {
                    try
                    {
                        var value = cRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                        value.ContractorName = txtFirstName.Text.Trim();
                        value.LastName = txtSurname.Text.Trim();
                        value.EmailAddress = txtEmailAddress.Text;
                        value.LoginName = txtEmailAddress.Text;
                        if (txtPassword.Text.Trim().Length > 0)
                        {
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim());
                        }
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = ddlStatus.SelectedValue;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = ddlCompany.SelectedValue;
                        value.ContactNumber = txtContactNumber.Text;
                        cRep.Edit(value);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    try
                    {
                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = cdRep.GetAll().Where(o => o.UserId == uid).FirstOrDefault();
                        cdEntity.Address1 = txtAddress.Text;
                        cdEntity.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                        cdEntity.PostCode = txtZipcode.Text;
                        cdEntity.State = txtState.Text;
                        cdEntity.Town = txtTown.Text;
                        cdRep.Edit(cdEntity);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    // Store role in tblRoles if QueryStringValues.Type is "2" (Donor role)
                    if (QueryStringValues.Type == "2")
                    {
                        try
                        {
                            PortfolioDataContext context = new PortfolioDataContext();

                            // Check if the role already exists



                            // Add new role
                            if (chkVolunteers.Checked)
                            {

                                bool roleExists = context.tblRoles.Any(r => r.ContractorID == uid && r.RoleType == "Volunteer");
                                if (!roleExists)
                                {
                                    // Add new role
                                    var newRole = new tblRole
                                    {
                                        ContractorID = uid,
                                        RoleType = "Volunteer"
                                        // Add other properties as needed
                                    };
                                    context.tblRoles.InsertOnSubmit(newRole);

                                }
                            }

                            if (chkLeads.Checked)
                            {
                                bool roleExists = context.tblRoles.Any(r => r.ContractorID == uid && r.RoleType == "Lead");
                                if (!roleExists)
                                {
                                    // Add new role
                                    var newRole = new tblRole
                                    {
                                        ContractorID = uid,
                                        RoleType = "Lead"
                                        // Add other properties as needed
                                    };
                                    context.tblRoles.InsertOnSubmit(newRole);

                                }
                            }

                            if (chkMembers.Checked)
                            {
                                bool roleExists = context.tblRoles.Any(r => r.ContractorID == uid && r.RoleType == "Member");
                                if (!roleExists)
                                {
                                    // Add new role
                                    var newRole = new tblRole
                                    {
                                        ContractorID = uid,
                                        RoleType = "Member"
                                        // Add other properties as needed
                                    };
                                    context.tblRoles.InsertOnSubmit(newRole);

                                }
                            }
                            if (chkDonors.Checked)
                            {
                                bool roleExists = context.tblRoles.Any(r => r.ContractorID == uid && r.RoleType == "Member");
                                if (!roleExists)
                                {
                                    // Add new role
                                    var newRole = new tblRole
                                    {
                                        ContractorID = uid,
                                        RoleType = "Donor"
                                        // Add other properties as needed
                                    };
                                    context.tblRoles.InsertOnSubmit(newRole);

                                }
                            }
                        }
                        
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }

                    uploadLogo(uid);
                    img.ImageUrl = "~/ImageHandler.ashx?id=" + uid.ToString() + "&s=" + ImageManager.file_section_user;
                    userid = uid;
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                }
            }
            else
            {
                var cRep = new UserRepository<Contractor>();
                var cvRep = new UserRepository<v_contractor>();
                string pw = "Smart@2022";
                userid = 0;
                try
                {
                    var value = new UserMgt.Entity.Contractor();
                    value.ContractorName = txtFirstName.Text.Trim();
                    value.LastName = txtSurname.Text.Trim();
                    value.EmailAddress = txtEmailAddress.Text;
                    value.LoginName = txtEmailAddress.Text;
                    if (txtPassword.Text.Trim().Length > 0)
                    {
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim());
                    }
                    else
                    {
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);
                    }
                    value.SID = Convert.ToInt32(ddlPermission.SelectedValue);
                    value.CreatedDate = DateTime.Now;
                    value.ModifiedDate = DateTime.Now;
                    value.Status = ddlStatus.SelectedValue;
                    value.isFirstlogin = 0;
                    value.ResetPassword = false;
                    value.Company = ddlCompany.SelectedValue;
                    value.ContactNumber = txtContactNumber.Text.Trim();

                    if (QueryStringValues.Type == "2")
                    {
                        if (cvRep.GetAll().Where(o => o.LoginName.ToLower().Trim() == value.LoginName.ToLower().Trim() && o.SID == UserType.Donor && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                        {
                            cRep.Add(value);
                            userid = value.ID;
                        }
                        else
                        {
                            DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Email already exists. Please try again ", "Ok");
                        }
                    }
                    else
                    {
                        if (cvRep.GetAll().Where(o => o.LoginName.ToLower().Trim() == value.LoginName.ToLower().Trim() && o.SID != UserType.Donor).Count() == 0)
                        {
                            cRep.Add(value);
                            userid = value.ID;
                        }
                        else
                        {
                            DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Email already exists. Please try again ", "Ok");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

                if (userid > 0)
                {
                    try
                    {
                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = txtAddress.Text;
                        cdEntity.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                        cdEntity.PostCode = txtZipcode.Text;
                        cdEntity.State = txtState.Text;
                        cdEntity.Town = txtTown.Text;
                        cdEntity.UserId = userid;
                        cdRep.Add(cdEntity);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    try
                    {
                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    try
                    {
                        uploadLogo(userid);
                        img.ImageUrl = "~/ImageHandler.ashx?id=" + userid + "&s=" + ImageManager.file_section_user;
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    userid = userid;
                }
            }

            return userid;
        }
       
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {

                var userid = SaveUserData();

                if(userid >0)

                Response.Redirect("~/App/Member.aspx?mid=" + userid +"&type="+QueryStringValues.Type);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindReligion()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

                ddlReligion.DataSource = rlist;
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataValueField = "ID";
                ddlReligion.DataBind();

                ddlReligion.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int religionID)
        {
            try
            {
                if (religionID > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    ddlDenimination.DataSource = rlist;
                    ddlDenimination.DataTextField = "Name";
                    ddlDenimination.DataValueField = "ID";
                    ddlDenimination.DataBind();
                }

                ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void uploadLogo(int partnerID)
        {
            try
            {
                if (imgLogo.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                    ImageManager.SaveUserImage_setpath(imgLogo.FileBytes, partnerID.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().FirstOrDefault();
                    if (cDetails != null)
                    {
                       
                        UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(uid, "InActive");
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                }
               
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {
            try
            {

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnUpdateSkill_Click

        private void BindSkills()
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                   var uskill =  UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == uid).FirstOrDefault();// (new UserMgt.Entity.UserSkill() { Skills = txtSkills.Value, UserId = uid });
                    if(uskill != null)
                    {
                        txtSkills.Value = uskill.Skills;
                    }
                    //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                   // Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindTages()
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    var uskill = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == uid).FirstOrDefault();// (new UserMgt.Entity.UserSkill() { Skills = txtSkills.Value, UserId = uid });
                    if (uskill != null)
                    {
                        txtTags.Value = uskill.Notes;
                    }
                    //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    // Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpdateSkill_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    UpdateSkillTags(uid);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString() + "&type=" + QueryStringValues.Type);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateSkillTags(int uid)
        {

            var uskill = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == uid).FirstOrDefault();
            if (uskill == null)
            {
                UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Skills = txtSkills.Value.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"value\":", "").Replace("\"", "").Trim(), UserId = uid });
            }
            else
            {
                uskill.Skills = txtSkills.Value.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"value\":", "").Replace("\"", "").Trim();
                UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(uskill);
            }
          
        }
        protected void btnUpdateTags_Click(object sender, EventArgs e)
        {
            try
            {

                var uid = SaveUserData();

                if (uid > 0)
                {
                    UpdateTags(uid);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    Response.Redirect("~/App/Member.aspx?mid=" + uid.ToString() + "&type=" + QueryStringValues.Type);
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateTags(int uid)
        {
            var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == uid).FirstOrDefault();
            if (ud == null)
            {
                UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = txtTags.Value, UserId = uid });
            }
            else
            {
                ud.Notes = txtTags.Value.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"value\":", "").Replace("\"", "").Trim(); ;
                UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
            }
        }

        #region File
        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Download")
            {
                try
                {
                    GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    // string contenttype = gridfiles.DataKeys[gvrow.RowIndex].Values[1].ToString();
                    //string filename = gridfiles.DataKeys[gvrow.RowIndex].Values[2].ToString();
                    //string[] ex = filename.Split('.');
                    //string ext = ex[ex.Length - 1];
                    //"~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString(
                    string filepath = string.Format("~/WF/UploadData/Donations/{0}/", hunid.Value, e.CommandArgument.ToString());
                    //Response.ContentType = contenttype;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + e.CommandArgument.ToString() + "\"");
                    Context.Response.ContentType = "octet/stream";
                    Response.TransmitFile(filepath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }


        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                //  File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.ID == Convert.ToInt32(filePath)).FirstOrDefault();

                if (fList != null)
                    fRep.Delete(fList);

                Gridfilesbind(QueryStringValues.MID.ToString());

                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void Gridfilesbind(string SID)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                // Fetch files where FileID starts with the given SID
                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_user_doc && o.FileID.StartsWith(SID)).ToList();

                // Fetch all contractors
                UserDataContext contractorsContext = new UserDataContext();
                var contractorList = contractorsContext.Contractors.ToList();

                // Join FileData with Contractors to get the UserID
                var rList = (from f in fList
                             join c in contractorList on f.UserID equals c.ID into fcJoin
                             from c in fcJoin.DefaultIfEmpty()
                             select new
                             {
                                 ID = f.ID,
                                 Value = f.FileID,
                                 Time = f.UploadedDate?.ToString("dd-MM-yyyy    HH:mm:ss") ?? "N/A", // Provide a default value if null
                                 UploadedBy = c != null ? c.ContractorName : "Unknown", // Provide a default value if null
                                 Text = f.FileName
                             }).ToList();

                gridfiles.DataSource = rList;

                // Step 1: Count the items in fList
                int fileCount = fList.Count;

                // Step 2: Create a script tag with this count
                string script = $"<script type='text/javascript'>var fileCount = {fileCount};</script>";

                // Step 3: Add the script tag to the header
                Page.Header.Controls.Add(new LiteralControl(script));

                gridfiles.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {

                string filePath = (sender as LinkButton).CommandArgument;
                // File.Delete(filePath);
                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var f = fRep.GetAll().Where(o => o.FileID == filePath && o.Section == ImageManager.file_section_user_doc).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id=" + filePath + "&s=" + ImageManager.file_section_user_doc);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #endregion

        protected void GridCommunication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "ViewCommunication")
                {
                    var id = e.CommandArgument.ToString();

                    IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                    var rtOutput = rpNew.GetAll().Where(o => o.ID == Convert.ToInt32( id)).FirstOrDefault();
                    if (rtOutput != null)
                    {
                        htomail.Value = rtOutput.MailTo;
                        hcid.Value = rtOutput.ID.ToString();
                        txtSubject.Text = rtOutput.MailSubject;
                        CKEditor1.Text = rtOutput.MailContent;

                        mdlShowMail.Show();
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string fromid = Deffinity.systemdefaults.GetFromEmail(sessionKeys.PortfolioID);

               
Email ToEmail = new Email();


                ToEmail.SendingMail(fromid, txtSubject.Text.Trim(), CKEditor1.Text, htomail.Value, "");

                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail sent successfully", "");

                sendThankyouMailTracker(Convert.ToInt32( hcid.Value), CKEditor1.Text, txtSubject.Text.Trim(), htomail.Value, 0);


                BindCommunicationGrid();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void sendThankyouMailTracker(int donationid, string mailcontent, string mailsubject, string mailto, int templateid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                rpNew.Add(new PortfolioMgt.Entity.ThankYouMailTracker()
                {
                    DonationID = donationid,
                    MailContent = mailcontent,
                    MailSubject = mailsubject,
                    MailTo = mailto,
                    PortfolioID = sessionKeys.PortfolioID,
                    TemplateID = templateid
                    ,
                    SentOn = DateTime.Now,
                    SendBy = sessionKeys.UID
                });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void GridDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

//                if (e.CommandName == "SendReceipt")
//                {
//                    var id = e.CommandArgument.ToString();
//                    hid.Value = id.ToString();
//                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
//                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
//                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
//                    Random generator = new Random();
//                    var dItem = (from t in tList
//                                     // join c in ulist on t.LoggedByID equals c.ID
//                                     //join tc in tclist on t.TithingID equals tc.ID
//                                 where t.ID == Convert.ToInt32(id)
//                                 select new
//                                 {
//                                     ID = t.ID,
//                                     Name = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
//                                     Email = getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
//                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
//                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
//                                     Amount = t.PaidAmount,
//                                     PaidDate = t.PaidDate,
//                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
//                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
//                                     REcurring = t.RecurringType,
//                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
//                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
//                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
//                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
//                                     t.MoreDetails,
//                                     t.unid,

//                                 }).FirstOrDefault();

//                    // var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

//                    if (dItem.unid == null)
//                    {
//                        var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
//                        if (dEntity != null)
//                        {
//                            dEntity.unid = Guid.NewGuid().ToString();

//                            PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(dEntity);
//                            hunid.Value = dEntity.unid;
//                        }
//}
//else
//                        hunid.Value = dItem.unid;


//                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

//                    var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
//                    if (tn == null)
//                        tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();


//                    // ddlTemplate.SelectedValue = tn.ID.ToString();

//                    String body = "";
//                    if (tn != null)
//                    {
//                        body = tn.EmailContent;
//                        //{{currentyear}}
//                        body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
//                        body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
//                        body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
//                        body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
//                        body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0));
//                        body = body.Replace("{{name}}", dItem.Name);
//                        body = body.Replace("{{category}}", dItem.CategoryList);
//                        body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
//                        body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


//                        body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount));

//                        body = body.Replace("{{donorfirstname}}", dItem.Name);
//                        body = body.Replace("{{donorsurname}}", dItem.Name);
//                        //donorcompany
//                        body = body.Replace("{{category}}", dItem.CategoryList);

//                        body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


//                        body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.Amount));

//                        body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
//                        body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
//                        //logo

//                        body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

//                    }



//                    if (!body.Contains("!DOCTYPE HTML PUBLIC"))
//                    {
//                        Emailer em = new Emailer();
//                        string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

//                        html_body = html_body.Replace("[table]", body);
//                        body = html_body;

//                        string fromid = Deffinity.systemdefaults.GetFromEmail();

//                        string toid = dItem.Email;
//                        string subject = "Donation";
//                        htomail.Value = toid;
//                        hsubject.Value = subject;
//                        CKEditor1.Text = body;

//                        if (dItem.Status.Contains("Successful"))
//                            mdlShowMail.Show();
//                        //Email ToEmail = new Email();


//                        //ToEmail.SendingMail(fromid, subject,body,toid,"");

//                        //sessionKeys.Message = "Your message is on it's way!";

//                        //Response.Redirect(Request.RawUrl, false);
//                    }




//                }
              
                if (e.CommandName == "view")
                {
                    var id = e.CommandArgument.ToString();
                    hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                    var tEntity = tList.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    //if (tEntity != null)
                    //{
                    //    if (QueryStringValues.Type.Length > 0)
                    //    {
                    //        Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + tEntity.unid, false);
                    //    }
                    //}

                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();


                    Random generator = new Random();
                    var rlist = (from t in tList
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName, //t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail, //t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     // Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.Notes,
                                     t.unid,
                                     PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                 }).ToList();

                    var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (dItem != null)
                    {
                        pnl_TranstactionDetails.Visible = true;
                        lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                        lblStatus.Text = dItem.Status;
                        txtname.Text = dItem.Name;
                        txttype.Text = dItem.PaymentType;
                        txtemail.Text = dItem.Email;
                        lblCategories.Text = dItem.CategoryListWithAmount;
                       // txtNotes.Text = dItem.Notes;
                        hunid.Value = dItem.unid;
                        lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                        lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                        // Gridfilesbind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void imgCustomFieldUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int uid = SaveUserData();
                if (uid > 0)
                {
                    SavePlaceholderData(uid, sessionKeys.PortfolioID);
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    Response.Redirect("~/App/Member.aspx?mid=" + uid + "&type=" + QueryStringValues.Type);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddCompnay_Click(object sender, EventArgs e)
        {
            txtAddCompany.Text = string.Empty;
            
            txtCompanyAddress.Text = string.Empty;
            txtCompanyPhone.Text = string.Empty;
            txtCompanyEmail.Text = string.Empty;
            txtCompanyNotes.Text = string.Empty;
            mdlPopup.Show();
        }

        protected void btnSubmitCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddCompany.Text.Trim().Length > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                    var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == txtAddCompany.Text.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                    if (cnt == 0)
                    {
                        var cDetails = new PortfolioMgt.Entity.UserCompany()
                        {
                            Name = txtAddCompany.Text.Trim(),
                            OrganisationID = sessionKeys.PortfolioID,
                            Address = txtCompanyAddress.Text.Trim(),
                            Contactno = txtCompanyPhone.Text.Trim(),
                            Email = txtCompanyEmail.Text.Trim(),
                            Notes = txtCompanyNotes.Text.Trim()
                        };
                        uRep.Add(cDetails);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                    }
                    BindCOmpany();

                    txtAddCompany.Text = string.Empty;
                    txtCompanyAddress.Text = string.Empty;
                    txtCompanyPhone.Text = string.Empty;
                    txtCompanyEmail.Text = string.Empty;
                    txtCompanyNotes.Text = string.Empty;
                    mdlPopup.Hide();
                }
               
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindCOmpany()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                var pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();

                if(pList.Count ==0)
                {
                    IUserRepository<UserMgt.Entity.v_contractor> pRep = new UserRepository<UserMgt.Entity.v_contractor>();
                    var rList = pRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                    foreach(var r in rList)
                    {
                        if (r.Company.Trim().Length > 0)
                        {
                            var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == r.Company.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                            if (cnt == 0)
                            {
                                uRep.Add(new PortfolioMgt.Entity.UserCompany() { Name = r.Company, OrganisationID = sessionKeys.PortfolioID });
                            }
                        }
                    }

                    pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();
                }

                ddlCompany.DataSource = pList.OrderBy(o=>o.Name).ToList();
                ddlCompany.DataTextField = "Name";
                ddlCompany.DataValueField = "Name";
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new ListItem("Please select...", ""));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}