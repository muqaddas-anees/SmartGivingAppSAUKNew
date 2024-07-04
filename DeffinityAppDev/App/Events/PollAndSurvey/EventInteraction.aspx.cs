using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey
{
    public partial class EventInteraction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BingGrid();
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }





        public void BingGrid(bool getNewData = false)
        {
            try
            {


                var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();// //PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();




                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                var EventInteracionDetailList = pRep.GetAll().Where(o => o.QuestionID >= 0).ToList();




               

               


               
                    

                    
                    GridInstances.DataSource = EventInteracionDetailList;
                    GridInstances.DataBind();
              



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            bool fundriser = rbtnFundraiser.Checked;
            bool poll = rbtnPoll.Checked;
            bool survey = rbtnSurvey.Checked;

            if (poll)
            {
                Response.Redirect("./Polls/Grid_Set_Up_New_Poll.aspx");
            }

            if (survey)
            {

                Response.Redirect("./Survey/Grid_Set_Up_New_Survey.aspx");
           


            }

        }
    }
}