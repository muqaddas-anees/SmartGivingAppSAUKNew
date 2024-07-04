using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Survey
{
   




    public partial class Set_Up_a__Survey : System.Web.UI.Page
    {
      


        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                if (!IsPostBack)
                {
                    hmoney.Value = "";
                    chkbarchart.Checked = true;
                    chkpie.Checked = false;
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                    if (QueryStringValues.EID>0)
                    {
                        var value = cRep.GetAll().Where(o => o.QuestionID == QueryStringValues.EID).FirstOrDefault();
                        if (value != null)
                        {
                            txtQuestion.Text = value.Question;
                            TextAreaDescription.Text = value.QuestionDescription;
                            hmoney.Value = value.MultipleChoiseWithAnswer;

                            DropDownListQuestionType.SelectedValue = value.QuestionTypeSingleOrMultiple.Trim();

                            if (value.ChartType == "bar")
                            {
                                chkbarchart.Checked = true;
                                chkpie.Checked = false;
                            }
                            else
                            {
                                chkpie.Checked = true;
                                chkbarchart.Checked = false;
                            }

                            UpdateMoneyGrid();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            DisplayChoise.Visible = false;




            DisplayMultipleChoise.Visible = false;

            DisplaySingleChoise.Visible = false;

            DisplayText.Visible = false;

            DisplayDetailedAnswer.Visible = false;




            string Qtvalue = DropDownListQuestionType.SelectedValue;




            if (Qtvalue == "Multiple Choice")
            {
                DisplayMultipleChoise.Visible = true;
            }











            }


        public void GetData()
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var value = new PortfolioMgt.Entity.PollAndSurveyDetail();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            AddQuestion();
        }

        private void AddQuestion(bool IsSamepage = false)
        {
            try
            {




                bool validate = false;







                string MultipleChoiseWithAnswer = "";

                string QuestionTypeSingleOrMultiple = "";


                string Qtvalue = DropDownListQuestionType.SelectedValue;

                if (Qtvalue == null || Qtvalue == "")
                {
                    lblQuestionType.Text = "Select The Question Type";
                }




                if (Qtvalue == "Multiple Choice")
                {
                    lblQuestionType.Text = "";
                    if (txtQuestion.Text == "" || txtQuestion.Text == null)
                    {
                        lblQuestion.Text = "Enter the Question";
                        validate = false;
                    }
                    else if (TextAreaDescription.Text == "" || TextAreaDescription.Text == null)
                    {
                        lblQuestion.Text = "";
                        lblDescription.Text = "Enter the Description";
                        validate = false;
                    }

                    //else if(txtMultipleChoice1.Text == null || txtMultipleChoice2.Text == null || txtMultipleChoice3.Text == null || txtMultipleChoice4.Text == null || txtMultipleChoice1.Text == "" || txtMultipleChoice2.Text == "" || txtMultipleChoice3.Text == "" || txtMultipleChoice4.Text == "")
                    //{
                    //    lblDescription.Text = "";
                    //    lblTexboxReminder.Text = "Please add choise to Multiple Choise text box ";
                    //    validate = false;
                    //    DisplayChoise.Visible = false;
                    //}
                    //else if (!RadioButtonChoice1.Checked && !RadioButtonChoice2.Checked && !RadioButtonChoice3.Checked && !RadioButtonChoice4.Checked)
                    //{
                    //    lblTexboxReminder.Text = "";
                    //    lblSelectChoiseReminder.Text = "Select the option or correct answer ";
                    //    validate = false;

                    //    txtMultipleChoice4_TextChanged(null, null);


                    //}

                    else
                    {
                        DisplayChoise.Visible = true;
                        validate = true;

                    }

                    MultipleChoiseWithAnswer = txtMultipleChoice1.Text + "%" + txtMultipleChoice2.Text + "%" + txtMultipleChoice3.Text + "%" + txtMultipleChoice4.Text;

                    QuestionTypeSingleOrMultiple = "MC";
                }



                if (Qtvalue == "Text")
                {
                    lblQuestionType.Text = "";

                    if (txtQuestion.Text == "" || txtQuestion.Text == null)
                    {
                        lblQuestion.Text = "Enter the Question";
                        validate = false;
                    }
                    else if (TextAreaDescription.Text == "" || TextAreaDescription.Text == null)
                    {
                        lblQuestion.Text = "";
                        lblDescription.Text = "Enter the Description";
                        validate = false;
                    }

                    else
                    {
                        lblDescription.Text = "";
                        validate = true;

                    }
                    QuestionTypeSingleOrMultiple = "Text";

                }

                if (Qtvalue == "Detailed Answer")
                {
                    lblQuestionType.Text = "";

                    QuestionTypeSingleOrMultiple = "DA";
                    if (txtQuestion.Text == "" || txtQuestion.Text == null)
                    {
                        lblQuestion.Text = "Enter the Question";
                        validate = false;
                    }
                    else if (TextAreaDescription.Text == "" || TextAreaDescription.Text == null)
                    {
                        lblQuestion.Text = "";
                        lblDescription.Text = "Enter the Description";
                        validate = false;
                    }

                    else
                    {
                        lblDescription.Text = "";
                        validate = true;

                    }
                }

                if (Qtvalue == "Single Selection")
                {
                    lblQuestionType.Text = "";

                    QuestionTypeSingleOrMultiple = "SS";
                    if (txtQuestion.Text == "" || txtQuestion.Text == null)
                    {
                        lblQuestion.Text = "Enter the Question";
                        validate = false;
                    }
                    else if (TextAreaDescription.Text == "" || TextAreaDescription.Text == null)
                    {
                        lblQuestion.Text = "";
                        lblDescription.Text = "Enter the Description";
                        validate = false;
                    }

                    else
                    {
                        lblDescription.Text = "";
                        validate = true;

                    }
                }



                if (validate)
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();


                    if (QueryStringValues.EID == 0)
                    {
                        var value = new PortfolioMgt.Entity.PollAndSurveyDetail();
                        value.Question = txtQuestion.Text;
                        value.QuestionDescription = TextAreaDescription.Text;
                        value.QuestionforPollOrSurvey = "Survey";
                        value.QuestionTypeSingleOrMultiple = DropDownListQuestionType.SelectedValue.Trim();
;
                        value.MultipleChoiseWithAnswer = getMoney(); //txtMultipleChoice1.Text + "%" + txtMultipleChoice2.Text + "%" + txtMultipleChoice3.Text + "%" + txtMultipleChoice4.Text;
                        if (rdList.SelectedValue != "")
                            value.SingleQuestionAnswer = rdList.SelectedValue;
                        else
                            value.SingleQuestionAnswer = "";

                        value.ChartType = chkbarchart.Checked ? "bar" : "pie";
                        value.EventUNUID = QueryStringValues.EVENTUNID;
                        if (QueryStringValues.UNID.Length > 0)
                            value.UNID = QueryStringValues.UNID;
                        else
                            value.UNID = Guid.NewGuid().ToString();

                        value.createdon = DateTime.Now;
                        value.Createdby = sessionKeys.UID;
                        value.DateLogged = DateTime.Now;
                        cRep.Add(value);

                        UpdateQRCodeImage(value.UNID);

                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                        if(IsSamepage)
                        {
                            Response.Redirect("~/App/Events/PollAndSurvey/Survey/Set_Up_a_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + value.UNID + "&EID=" + 0,false);
                        }
                        else
                        Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_View_Questions.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + value.UNID,false);
                        // Response.Redirect("~/App/Events/PollAndSurvey/Survey/Survey_Question_List.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + value);

                    }

                    else
                    {
                        var value = cRep.GetAll().Where(o => o.QuestionID == QueryStringValues.EID).FirstOrDefault();
                        if (value != null)
                        {
                            value.Question = txtQuestion.Text;
                            value.QuestionDescription = TextAreaDescription.Text;
                            //  value.QuestionforPollOrSurvey = "Survey";
                            value.QuestionTypeSingleOrMultiple = DropDownListQuestionType.SelectedValue.Trim();
                            value.MultipleChoiseWithAnswer = getMoney(); //txtMultipleChoice1.Text + "%" + txtMultipleChoice2.Text + "%" + txtMultipleChoice3.Text + "%" + txtMultipleChoice4.Text;
                            if (rdList.SelectedValue != "")
                                value.SingleQuestionAnswer = rdList.SelectedValue;
                            else
                                value.SingleQuestionAnswer = "";

                            value.ChartType = chkbarchart.Checked ? "bar" : "pie";


                            cRep.Edit(value);
                            DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                            if (IsSamepage)
                            {
                                Response.Redirect("~/App/Events/PollAndSurvey/Survey/Set_Up_a_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + value.UNID+"&EID="+value.QuestionID,false);
                            }
                            else
                                Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_View_Questions.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + value.UNID,false);
                        }
                    }

                    // Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_Set_Up_New_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID);




                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void txtMultipleChoice4_TextChanged(object sender, EventArgs e)
        {



            DisplayMultipleChoise.Visible = true;

            if (hmoney.Value.Length ==0)
            {
                lblTexboxReminder.Text = "Please add choise to Multiple Choise text box ";
            }
            else
            {
                UpdateMoneyGrid();
            }


                //if (txtMultipleChoice1.Text == null || txtMultipleChoice2.Text == null || txtMultipleChoice3.Text == null || txtMultipleChoice4.Text == null || txtMultipleChoice1.Text == "" || txtMultipleChoice2.Text == "" || txtMultipleChoice3.Text == "" || txtMultipleChoice4.Text == "")
                //{
                //    lblTexboxReminder.Text = "Please add choise to Multiple Choise text box ";
                //}
                //else
                //{
                //    lblTexboxReminder.Text = "";

                //    DisplayChoise.Visible = true;

                //    string text1= txtMultipleChoice1.Text;
                //    string text2= txtMultipleChoice2.Text;
                //    string text3= txtMultipleChoice3.Text;
                //    string text4 = txtMultipleChoice4.Text;



                //    RadioButtonChoice1.Text = txtMultipleChoice1.Text;
                //    RadioButtonChoice2.Text = txtMultipleChoice2.Text;
                //    RadioButtonChoice3.Text = txtMultipleChoice3.Text;
                //    RadioButtonChoice4.Text = txtMultipleChoice4.Text;
                //}
            }

        protected void DropDownListQuestionType_SelectedIndexChanged(object sender, EventArgs e)

        {
            chkbarchart.Checked = true;
            chkpie.Checked = false;
            string val = DropDownListQuestionType.SelectedValue;

            lblQuestionType.Text = "";

            if (val== "Text")
            {
                DisplayText.Visible = true;



                DisplayMultipleChoise.Visible = false;

                DisplaySingleChoise.Visible = false;

                DisplayDetailedAnswer.Visible = false;
            }

            if (val == "Detailed Answer")
            {
                DisplayDetailedAnswer.Visible = true;


                DisplayMultipleChoise.Visible = false;

                DisplaySingleChoise.Visible = false;

                DisplayText.Visible = false;
            }

            if (val == "Single Selection")
            {
                DisplaySingleChoise.Visible = true;

               

                DisplayText.Visible = false;

                DisplayDetailedAnswer.Visible = false;

                DisplayMultipleChoise.Visible = true;
            }

            if (val == "Multiple Choice")
            {
               
                DisplayMultipleChoise.Visible = true;


               
               


                DisplaySingleChoise.Visible = false;

                DisplayDetailedAnswer.Visible = false;

                DisplayText.Visible = false;

              

            }


            
        }

        protected void rbtnMultipleChoise_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateQRCodeImage(string unid)
        {
            try
            {
                string code = Deffinity.systemdefaults.GetWebUrl() + "/surveydetails.aspx?unid=" + unid; //ab.MoreDetails;

                var filepath = "~/WF/UploadData/Events/" + unid + ".png";
                // var filepathpdf = "~/WF/UploadData/Events/" + unid + ".pdf";

                // var filepath = Server.MapPath("~/WF/UploadData/Events/") + id + ".png";
                //if (File.Exists(Server.MapPath(filepath)))
                //{
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 150;
                imgBarCode.Width = 150;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms);
                        img1.Save(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plBarCode.Controls.Add(imgBarCode);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridMoney_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit1")
            {

            }
            else if (e.CommandName == "del")
            {
                var d = e.CommandArgument.ToString();

                hmoney.Value = hmoney.Value.Replace(d.ToString() + ",", "");

                UpdateMoneyGrid();
            }

        }

        protected void btnAddMoney_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMoney.Text.Trim().Length > 0)
                {
                    var m = hmoney.Value;
                    hmoney.Value = m + txtMoney.Text.Trim() + ",";
                    txtMoney.Text = string.Empty;
                    UpdateMoneyGrid();
                }
                else
                {
                    DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "Please enter choice", "");
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        class moneycls
        {
            public double id { set; get; }
            public string value { set; get; }
        }

        private void UpdateMoneyGrid()
        {
            try
            {
                List<moneycls> mcList = new List<moneycls>();
                var mLIst = hmoney.Value;
                int index = 1;
                foreach (var m in mLIst.Split(','))
                {
                    if (m.Trim().Length > 0)
                        mcList.Add(new moneycls() { id = index, value = m });

                    index++;
                }

                GridMoney.DataSource = mcList;
                GridMoney.DataBind();

                rdList.DataSource = mcList;
                rdList.DataValueField = "id";
                rdList.DataTextField = "value";
                rdList.DataBind();

                if (rdList.Items.Count > 0)
                {
                    DisplayChoise.Visible = true;
                }
                else
                {
                    DisplayChoise.Visible = false;
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getMoney()
        {
            var retval = "";
            foreach (GridViewRow gvrow in GridMoney.Rows)
            {
                TextBox lblValue = (TextBox)gvrow.FindControl("lblValue");
                if (lblValue.Text.Trim().Length > 0)
                {
                    retval = retval + lblValue.Text.Trim() + ",";
                }
            }
            return retval;
        }

        protected void btnAddanAnswer_Click(object sender, EventArgs e)
        {
            try
            {
               
                UpdateMoneyGrid();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddNext_Click(object sender, EventArgs e)
        {
            AddQuestion(true);
        }

        protected void btnBacktoSurvery_Click(object sender, EventArgs e)
        {
            if(QueryStringValues.UNID.Length >0)
            {
                Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_View_Questions.aspx?eventunid=" + QueryStringValues.EVENTUNID + "&UNID=" + QueryStringValues.UNID);
            }
            else
            {

                Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_Set_Up_New_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID );
            }
        }
    }




}