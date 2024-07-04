using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Polls
{
    public partial class Set_up_a_Poll : System.Web.UI.Page
    {
       


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    hmoney.Value = "";
                    chkbarchart.Checked = true;
                    chkpie.Checked = false;
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                    if(QueryStringValues.UNID != "")
                    {
                        var value = cRep.GetAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();
                        if(value != null)
                        {
                            txtQuestion.Text = value.Question;
                            TextAreaDescription.Text = value.QuestionDescription ;
                            hmoney.Value = value.MultipleChoiseWithAnswer;

                            if (value.ChartType == "bar")
                                chkbarchart.Checked = true;
                            else chkpie.Checked = true;

                            UpdateMoneyGrid();
                        }
                    }
                }
              

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        public void GetData()
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var value = new PortfolioMgt.Entity.PollAndSurveyDetail();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            try
            {
                if (QueryStringValues.UNID.Length == 0)
                {
                    var value = new PortfolioMgt.Entity.PollAndSurveyDetail();

                    value.Question = txtQuestion.Text;
                    value.QuestionDescription = TextAreaDescription.Text;
                    value.QuestionforPollOrSurvey = "Poll";
                    value.QuestionTypeSingleOrMultiple = "";
                    value.MultipleChoiseWithAnswer = getMoney();// txtMultipleChoice1.Text + "%" + txtMultipleChoice2.Text + "%" + txtMultipleChoice3.Text + "%" + txtMultipleChoice4.Text;
                    value.EventUNUID = QueryStringValues.EVENTUNID;
                    value.UNID = Guid.NewGuid().ToString();
                    value.createdon = DateTime.Now;
                    value.Createdby = sessionKeys.UID;

                    value.ChartType = chkpie.Checked ? "bar" : "pie";

                    cRep.Add(value);

                    

                    UpdateQRCodeImage(value.UNID);
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Poll created successfully ", "Ok");
                }
                else
                {
                    var value = cRep.GetAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();
                    if (value != null)
                    {
                        value.Question = txtQuestion.Text.Trim();
                        value.QuestionDescription = TextAreaDescription.Text ;
                        value.MultipleChoiseWithAnswer = getMoney();
                        value.ChartType = chkpie.Checked ? "bar" : "pie";
                        cRep.Edit(value);
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Poll updated successfully ", "Ok");
                    }

                }

                Response.Redirect("~/App/Events/PollAndSurvey/Polls/Grid_Set_Up_New_Poll.aspx?eventunid=" + QueryStringValues.EVENTUNID);

               

            }
            catch( Exception ex)
            {

            }
        }

        private void UpdateQRCodeImage(string unid)
        {
            try
            {
                string code = Deffinity.systemdefaults.GetWebUrl() + "/polldetails.aspx?unid=" + unid; //ab.MoreDetails;

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
                    hmoney.Value = m  + txtMoney.Text.Trim()+",";
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
    }
}