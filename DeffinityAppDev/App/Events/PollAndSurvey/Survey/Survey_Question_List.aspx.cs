using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Survey
{
  
    public partial class Survey_Question_List : System.Web.UI.Page
    {

        public class qclass
        {
            public int id { set; get; }
            public int questionid { set; get; }

            public string unid { set; get; }
            public string controltype { set; get; 
            }
            public string question { set; get; }
            public string questiondesc { set; get; }
            public string options { set; get; }

            public string answer { set; get; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    Huser_guid.Value = Guid.NewGuid().ToString();
                    List<qclass> qlist = new List<qclass>();
                    var cRep1 = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                    var unid = QueryStringValues.UNID;

                    var list1 = cRep1.GetAll().Where(o => o.UNID == unid).ToList();
                    int totalcount = list1.Count;

                    if (totalcount > 0)
                    {
                        int i = 1;
                        foreach (var p in list1)
                        {
                            qlist.Add(new qclass() { id = i, questionid = p.QuestionID, controltype = p.QuestionTypeSingleOrMultiple, unid = p.UNID, question = p.Question, answer = "", questiondesc = p.QuestionDescription, options = p.MultipleChoiseWithAnswer });
                            i++;
                        }

                        Session["q"] = qlist;
                        Session["cnt"] = qlist.Count();
                        Session["data"] = qlist;
                        Session["c"] = qlist.First().id;
                        imgQR.ImageUrl = "~/WF/UploadData/Events/" + unid + ".png";
                        BindListView(qlist.First());
                        Hevent_unid.Value = list1.FirstOrDefault().EventUNUID;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



        protected void BindListView(qclass q, int qid=0)
        {
            try
            {
                if (Session["data"] != null)
                {
                    List<qclass> qlist = (Session["data"] as List<qclass>);

                    int first = qlist.First().id;

                    int last = qlist.Last().id;

                    if(qid >0)
                    q = qlist.Where(o => o.id == qid).FirstOrDefault();

                    int questionno = q.id;

                    int current = (int)Session["c"];

                    if (questionno == first)
                    {

                        btnPrevious.Visible = false;
                        btnFinesh.Visible = false;
                    }

                    else
                    {
                        btnPrevious.Visible = true;
                        btnFinesh.Visible = false;
                    }



                    if (questionno == last)
                    {

                        btnNext.Visible = false;

                        btnFinesh.Visible = true;

                    }

                    else
                    {
                        btnNext.Visible = true;

                        btnFinesh.Visible = false;
                    }


                    lblQuestion.Text = q.question;
                    lblDescription.Text = q.questiondesc;

                    hmoney.Value = q.options;
                    UpdateMoneyGrid();

                    if (q.controltype == "Multiple Choice")
                    {
                        DisplayMultipleChoise.Visible = true;
                        DisplayDetailedAnswer.Visible = false;
                        DisplayText.Visible = false;
                        DisplaySingleSlection.Visible = false;
                    }

                    if (q.controltype == "Text")
                    {
                        DisplayText.Visible = true;


                        DisplayMultipleChoise.Visible = false;
                        DisplaySingleSlection.Visible = false;
                        DisplayDetailedAnswer.Visible = false;
                    }

                    if (q.controltype == "Detailed Answer")
                    {

                        DisplayDetailedAnswer.Visible = true;


                        DisplayMultipleChoise.Visible = false;
                        DisplaySingleSlection.Visible = false;
                        DisplayText.Visible = false;

                    }

                    if (q.controltype == "Single Selection")
                    {
                        DisplaySingleSlection.Visible = true;

                        DisplayMultipleChoise.Visible = false;
                        DisplayDetailedAnswer.Visible = false;
                        DisplayText.Visible = false;
                    }


                }

            }
            catch(Exception ex)
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

                rdList.DataSource = mcList;
                rdList.DataValueField = "value";
                rdList.DataTextField = "value";
                rdList.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

     

        private void UpdateCollection(int currentid)
        {
            if (Session["data"] != null)
            {
                List<qclass> qlist = (Session["data"] as List<qclass>);

                int cid = (int)Session["c"];

                var q = qlist.Where(o => o.id == cid).FirstOrDefault();
                string ans = "";
                string singleWordAnswer = txtSingleSelection.Text.Trim();

                string textbox = txtTextvalue.Text.Trim();

                string DetailedAnswer = TextAreaDetailedAnswer.Text.Trim();

                string radio_select = rdList.SelectedValue;
                if (q.controltype == "Multiple Choice")
                {
                    ans = radio_select;
                }

                if (q.controltype == "Text")
                {
                    ans = textbox;
                }

                if (q.controltype == "Detailed Answer")
                {
                    ans = DetailedAnswer;
                }

                if (q.controltype == "Single Selection")
                {
                    ans = radio_select;
                }

                q.answer = ans;

                var cRep1 = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

                var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                var pEntity = cRep.GetAll().Where(o => o.QuestionID == q.questionid).FirstOrDefault();

                var ps = cRep1.GetAll().Where(o => o.UserUNID == Huser_guid.Value && o.QuestionID == q.questionid).FirstOrDefault();
                if (ps == null)
                {
                     ps = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();
                    //ps.correctAnswers = ans;
                    ps.SelectedChoise = ans;
                    ps.CorrectChoise = pEntity.SingleQuestionAnswer;
                    ps.DateLogged = DateTime.Now;
                    ps.EventUNID = Hevent_unid.Value;
                    ps.UNID = q.unid;
                    ps.UserUNID = Huser_guid.Value.ToString();
                    ps.QuestionID = q.questionid;
                    cRep1.Add(ps);
                }
                else
                {
                    ps.SelectedChoise = ans;
                    ps.DateLogged = DateTime.Now;
                    
                    cRep1.Edit(ps);
                }

            }
        }

        protected void NextButtonClick(object sender, EventArgs e)
        {

            try
            {
                int cid = (int)Session["c"];

                UpdateCollection(cid);
                cid = cid + 1;

                Session["c"] = cid;
                BindListView(null, cid);

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



        protected void PreviousButtonClick(object sender, EventArgs e)
        {

            try
            {
                int cid = (int)Session["c"];

                UpdateCollection(cid);
                cid = cid - 1;

                Session["c"] = cid;
                BindListView(null, cid);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }





        protected void SubmitButtonClick(object sender, EventArgs e)
        {

            try
            {
                int cid = (int)Session["c"];

                //cid = cid - 1;

                UpdateCollection(cid);

                //Session["c"] = cid;

                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page,Resources.DeffinityRes.UpdatedSuccessfully,"Ok");

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
                    


        }


















































        protected void OnOptionChanged(object sender, EventArgs e)
        {




            if (Session["TotalCount"] != null || Session["CorrectAnswers"] != null)
            {
                int totalcount = (int)Session["TotalCount"];

                int CorrectAnswers = (int)Session["CorrectAnswers"];


                var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

                var value = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();


                value.QuestionID = 1;

                value.EventID = 1;
                value.userID = 1;

                value.PollOrSurvey = "Survey";

                value.TotalQuestions = totalcount;
                value.correctAnswers = CorrectAnswers;



                cRep.Add(value);



            }
            else
            {
              
            }




            var list = SelectedOptionDetails();


            

            var pl = from r in list
                     orderby r.SelectedChoise
                     group r by r.SelectedChoise into grp
                     select new { key = grp.Key, cnt = grp.Count() };







        }


        public static IQueryable<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> SelectedOptionDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();
            return pRep.GetAll();
        }


        public static IQueryable<PortfolioMgt.Entity.PollAndSurveyDetail> ProjectPortfolioBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            return pRep.GetAll();
        }
        public static void getArryData()
        {




            var cRep1 = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var list1 = cRep1.GetAll().Where(o => o.QuestionID > 0).ToList();
            int totalcount = cRep1.GetAll().Where(o => o.QuestionID > 0).ToList().Count;









            int count = 0;

            foreach (PortfolioMgt.Entity.PollAndSurveyDetail product in list1)
            {
                int id = product.QuestionID;



                count++;


            }




        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Survey_Result.aspx?UNID=" + QueryStringValues.UNID, false);
        }
    }









}