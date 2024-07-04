using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events.PollAndSurvey.Survey
{
    public partial class Grid_View_Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BingGrid();
            }
        }
        public void BingGrid(bool getNewData = false)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                var EventInteracionDetailList = pRep.GetAll().Where(o => o.EventUNUID == QueryStringValues.EVENTUNID &&o.UNID == QueryStringValues.UNID && o.QuestionforPollOrSurvey == "Survey").ToList();

                GridPolls.DataSource = EventInteracionDetailList;
                GridPolls.DataBind();

                if(EventInteracionDetailList.FirstOrDefault().Question.Length >50)
                lblFirst.Text = EventInteracionDetailList.FirstOrDefault().Question.Substring(0,49)+ "...";
                else
                    lblFirst.Text = EventInteracionDetailList.FirstOrDefault().Question;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
      

        protected void GridPolls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                var edetails = pRep.GetAll().Where(o => o.UNID == e.CommandArgument.ToString()).FirstOrDefault();
                if (e.CommandName == "edit1")
                {
                    Response.Redirect("~/app/Events/PollAndSurvey/Polls/Set_Up_a_Survey.aspx?eventunid=" + edetails.EventUNUID + "&unid=" + e.CommandArgument.ToString(), false);
                }
                else if (e.CommandName == "ViewReport")
                {
                    Response.Redirect("~/app/Events/PollAndSurvey/Polls/Poll_Result.aspx?eventunid=" + edetails.EventUNUID + "&unid=" + e.CommandArgument.ToString(), false);
                }
                else if (e.CommandName == "ViewSurvey")
                {
                    Response.Redirect("~/app/Events/PollAndSurvey/Polls/Poll_Display.aspx?eventunid=" + edetails.EventUNUID + "&unid=" + e.CommandArgument.ToString(), false);
                }
                else if (e.CommandName == "del")
                {
                    pRep.Delete(edetails);
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    BingGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Set_Up_a_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID+"&unid="+QueryStringValues.UNID+"&EID=0", false);
        }

        protected void btnBakTOlist_Click(object sender, EventArgs e)
        {
            Response.Redirect("Grid_Set_Up_New_Survey.aspx?eventunid=" + QueryStringValues.EVENTUNID  , false);
        }
    }
}