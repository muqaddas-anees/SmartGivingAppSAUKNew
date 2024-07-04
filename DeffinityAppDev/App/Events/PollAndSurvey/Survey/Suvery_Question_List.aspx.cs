using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Survey
{
   






    public partial class Suvery_Question_List : System.Web.UI.Page
    {
        


        
       
        


        protected void Page_Load(object sender, EventArgs e)
        {




            try
            {




                if (!IsPostBack)
                {
                    var cRep1 = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                    var list1 = cRep1.GetAll().Where(o => o.QuestionID > 0).ToList();
                    int totalcount = cRep1.GetAll().Where(o => o.QuestionID > 0).ToList().Count;




                    ArrayList questionNoList = new ArrayList();


                    ArrayList al = new ArrayList();

                    ArrayList Answers = new ArrayList();


                    int[] QuestionArray = new int[totalcount];

                    int internalcount = 0;

                    Session["CurrentQuestionIndex"] = 1;

                    foreach (PortfolioMgt.Entity.PollAndSurveyDetail product in list1)
                    {
                        int id = product.QuestionID;

                        string stringAnswers = product.SingleQuestionAnswer;

                       

                        QuestionArray[internalcount] = id;

                        questionNoList.Add(id);

                        Answers.Add(stringAnswers);



                        al.Add("");


                        internalcount++;



                    }

                    Session["index"] = QuestionArray;

                    Session["arrayList"] = al;

                    Session["questionNoarrayList"] = questionNoList;

                    Session["Answer"] = Answers;

                    //Session.Remove("questionNoarrayList");

                    Session["FirstQuestionNo"] = questionNoList[0];

                    Session["LastQuestionNo"] = questionNoList[questionNoList.Count -1];


                    BindListView(1);

                   

                    if (Request.QueryString["orgid"] != null)
                    {

                    }


                }

                else
                {
                    //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction();", true);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }




       



        protected void BindListView( int questionno )
        {

           Session["executedQuestionNo"]  = questionno;


            


            int first = (int)Session["FirstQuestionNo"];

            int last = (int)Session["LastQuestionNo"];

            if (questionno == first) {

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



            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();


            var list = cRep.GetAll().Where(o => o.QuestionID > 0).ToList();

            var pEntity = ProjectPortfolioBAL_SelectAll().Where(o => o.QuestionID >= 1).ToList();


            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var ActivityDetailList = pRep.GetAll().Where(o => o.QuestionID == questionno).FirstOrDefault();


            string question = ActivityDetailList.Question;


            string choise1 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[0];
            string choise2 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[1];
            string choise3 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[2];
            string choise4 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[3];




            lblQuestion.Text = question ;


            rbtnOption1.Text = choise1;
            rbtnOption2.Text = choise2;
            rbtnOption3.Text = choise3;
            rbtnOption4.Text = choise4;



            




            //lvCustomers.DataSource = ActivityDetailList;
            //lvCustomers.DataBind();


        }




        protected void OptionSelected_CheckedChanged(object sender, EventArgs e)
        {

           

            ArrayList questionNoarrayList = (ArrayList)Session["questionNoarrayList"];


            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];


            //SelectedOptionArrList = (ArrayList)Session["arrayList"];

            int  qno = (int)Session["executedQuestionNo"];

            int[] QuestionArray = (int[])Session["index"];




            //int pos = questionNoarrayList.IndexOf(questionNoarrayList, (int)Session["executedQuestionNo"])+1;


            int pos = Array.IndexOf(QuestionArray, qno);


            RadioButton rbtn = (RadioButton)sender;

            SelectedOptionArrList[pos]= rbtn.Text;


            //SelectedOptionArrList.Add("testing");

            Session["arrayList"] = SelectedOptionArrList;

            Session["CurrentQuestionIndex"] = (int)Session["CurrentQuestionIndex"];


        }



        protected void NextButtonClick(object sender, EventArgs e)
        {
            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];

            ArrayList questionNoarrayList = (ArrayList)Session["questionNoarrayList"];

            //int pos = questionNoarrayList.IndexOf(questionNoarrayList, (int)Session["executedQuestionNo"]) + 1;

            int[] QuestionArray = (int[])Session["index"];

            int qno = (int)Session["executedQuestionNo"];

            int pos = Array.IndexOf(QuestionArray, qno);

           


          int size=  questionNoarrayList.Count;

            int count = pos + 1;

            if (count < size)
            {
                int QuestionID = (int)questionNoarrayList[pos + 1];
                BindListView(QuestionID);



                CheckSelectedOrNotNext(QuestionID, count);
            }

            

            

        }



        protected void PreviousButtonClick(object sender, EventArgs e)
        {
            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];

            ArrayList questionNoarrayList = (ArrayList)Session["questionNoarrayList"];

           // int pos = questionNoarrayList.IndexOf(questionNoarrayList, (int)Session["executedQuestionNo"]);

            int[] QuestionArray = (int[])Session["index"];

            int qno = (int)Session["executedQuestionNo"];

            int pos = Array.IndexOf(QuestionArray, qno);

            int count = pos ;

            if (count >= 0)
            {
                int QuestionID = 0;


                if (pos - 1 != -1)
                {
                    QuestionID = (int)questionNoarrayList[pos - 1];

                    if (QuestionID != -1)
                    {
                        BindListView(QuestionID);

                        CheckSelectedOrNotPrevious(QuestionID, pos);
                    }

                }

                



            }

            

        }




        public void CheckSelectedOrNotPrevious(int Questionid , int index )
        {
            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];

            string Value = (String)SelectedOptionArrList[index-1];

            rbtnOption1.Checked = false;
            rbtnOption2.Checked = false;
            rbtnOption3.Checked = false;
            rbtnOption4.Checked = false;

            if (Value!=""|| Value != null)
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                var ActivityDetailList = pRep.GetAll().Where(o => o.QuestionID == Questionid).FirstOrDefault();


                string question = ActivityDetailList.Question;


                string choise1 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[0];
                string choise2 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[1];
                string choise3 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[2];
                string choise4 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[3];

                if (Value.ToLower() == choise1.ToLower())
                {
                    rbtnOption1.Checked = true;
                    
                }

                if (Value.ToLower() == choise2.ToLower())
                {
                    rbtnOption2.Checked = true;
                   
                }

                if (Value.ToLower() == choise3.ToLower())
                {
                    rbtnOption3.Checked = true;
                   
                }

                if (Value.ToLower() == choise4.ToLower())
                {
                    rbtnOption4.Checked = true;
                }


               

            }

        }



        public void CheckSelectedOrNotNext(int Questionid, int index)
        {
            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];

            string Value = (String)SelectedOptionArrList[index ];

            rbtnOption1.Checked = false;
            rbtnOption2.Checked = false;
            rbtnOption3.Checked = false;
            rbtnOption4.Checked = false;

            if (Value != "" || Value != null)
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                var ActivityDetailList = pRep.GetAll().Where(o => o.QuestionID == Questionid).FirstOrDefault();


                string question = ActivityDetailList.Question;


                string choise1 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[0];
                string choise2 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[1];
                string choise3 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[2];
                string choise4 = ActivityDetailList.MultipleChoiseWithAnswer.Split('%')[3];

                if (Value.ToLower() == choise1.ToLower())
                {
                    rbtnOption1.Checked = true;

                }

                if (Value.ToLower() == choise2.ToLower())
                {
                    rbtnOption2.Checked = true;

                }

                if (Value.ToLower() == choise3.ToLower())
                {
                    rbtnOption3.Checked = true;

                }

                if (Value.ToLower() == choise4.ToLower())
                {
                    rbtnOption4.Checked = true;
                }




            }

        }



        protected void SubmitButtonClick(object sender, EventArgs e)
        {

            ArrayList SelectedOptionArrList = (ArrayList)Session["arrayList"];

            ArrayList questionNoarrayList = (ArrayList)Session["questionNoarrayList"];

            ArrayList Answers = (ArrayList)Session["Answer"];

            int CorrectAnsCount = 0;

            int totalCount = questionNoarrayList.Count;

            for(int i=0; i < totalCount; i++)
            {
                string ans = Answers[i].ToString().Trim(' ').ToLower();

                string selAns = SelectedOptionArrList[i].ToString().Trim(' ').ToLower();


                if (ans == selAns)
                {
                    CorrectAnsCount = CorrectAnsCount + 1;
                }

            }



           

            
            int count = CorrectAnsCount;


        }





























        protected void OnOptionChanged(object sender, EventArgs e)
        {






            RadioButton radioButton = (RadioButton)sender;

            string selectedOption = radioButton.Text;

            string id = radioButton.ID;



            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

            var value = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();


            value.QuestionID = 1;
            value.SelectedChoise = selectedOption;
            value.EventID = 1;
            value.userID = 1;




            cRep.Add(value);






            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            pRep.GetAll();



            var list = SelectedOptionDetails();


            var results = list.GroupBy(x => x.SelectedChoise)
            .Select(g => g.OrderBy(x => x.SelectedChoise).FirstOrDefault());




            var pl = from r in list
                     orderby r.SelectedChoise
                     group r by r.SelectedChoise into grp
                     select new { key = grp.Key, cnt = grp.Count() };








            foreach (var line in list.GroupBy(info => info.SelectedChoise)
                        .Select(group => new {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                Console.WriteLine("{0} {1}", line.Metric, line.Count);
            }





            //rptPollCountGrid.DataSource = pl.ToList();
            //rptPollCountGrid.DataBind();








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




















    }







}