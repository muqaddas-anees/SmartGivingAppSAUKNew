using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events.PollAndSurvey.Survey
{
    public partial class Survey_Display : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                    var value = new PortfolioMgt.Entity.PollAndSurveyDetail();



                    var pEntity = ProjectPortfolioBAL_SelectAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();


                    string question = pEntity.Question;

                    //string choise1 = pEntity.MultipleChoiseWithAnswer.Split('%')[0];
                    //string choise2 = pEntity.MultipleChoiseWithAnswer.Split('%')[1];
                    //string choise3 = pEntity.MultipleChoiseWithAnswer.Split('%')[2];
                    //string choise4 = pEntity.MultipleChoiseWithAnswer.Split('%')[3];


                    lblQuestion.Text = pEntity.Question;
                    lblDescription.Text = pEntity.QuestionDescription;
                    hmoney.Value = pEntity.MultipleChoiseWithAnswer;
                    imgQR.ImageUrl = "~/WF/UploadData/Events/" + pEntity.UNID + ".png";
                   //UpdateMoneyGrid();
                    //lbloption1.Text = choise1;
                    //lbloption2.Text = choise2;
                    //lbloption3.Text = choise3;
                    //lbloption4.Text = choise4;


                    //R1.Text = choise1;
                    //R2.Text = choise2;
                    //R3.Text = choise3;
                    //R4.Text = choise4;


                   // PopulatePollOptions();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public static IQueryable<PortfolioMgt.Entity.PollAndSurveyDetail> ProjectPortfolioBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            return pRep.GetAll();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}