using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Survey
{
    public partial class Survey_Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                var pData = pRep.GetAll().Where(o => o.UNID == QueryStringValues.UNID).ToList();
                var list = SelectedOptionDetails().Where(o => o.UNID == QueryStringValues.UNID).ToList();

                listview.DataSource = (from p in pData
                                       select new
                                       {
                                           unid = p.UNID,
                                           p.QuestionID,
                                           p.Question,
                                           p.QuestionTypeSingleOrMultiple,
                                           p.SingleQuestionAnswer,

                                       }).ToList();
                listview.DataBind();

                string v = "";
                foreach(var p in pData)
                {
                    v = v + p.QuestionID.ToString() + ",";
                }

                hid.Value = v;
            }



            if (Session["TotalCount"]!=null|| Session["CorrectAnswers"] != null)
            {
                int totalcount = (int)Session["TotalCount"];

                int CorrectAnswers = (int)Session["CorrectAnswers"];

                lblCorrectAnswers.Text = CorrectAnswers.ToString();

                lblTotalQuestions.Text = totalcount.ToString();
                int percentage = 0;
                if (CorrectAnswers!=0|| totalcount != 0)
                {
                     percentage = (CorrectAnswers / totalcount) * 100;
                }

               

                Label1.Text= CorrectAnswers.ToString();
                Label2.Text= totalcount.ToString();

                lblResult.Text = percentage + " %";

            }
            else
            {
                lblQuestion.Text = "Not Attended the Survey";
            }
           



        }




        public static IQueryable<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> SelectedOptionDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();
            return pRep.GetAll();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}