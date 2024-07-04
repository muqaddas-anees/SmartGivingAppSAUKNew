using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Survey
{
    
    public partial class Grid_Set_Up_New_Survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BingGrid();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void BingGrid(bool getNewData = false)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> prRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();
                var prlist = prRep.GetAll().Where(o => o.EventUNID == QueryStringValues.EVENTUNID).ToList();
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                var EventInteracionDetailList = pRep.GetAll().Where(o => o.EventUNUID == QueryStringValues.EVENTUNID && o.QuestionforPollOrSurvey == "Survey").ToList();

                var rList = (from r in EventInteracionDetailList
                             select new
                             {
                                 r.UNID,
                                 r.EventUNUID,
                                 r.noOfUsersAttented,


                             }).ToList();

                var nrList = rList.Distinct();

                var frList = (from n in nrList
                              select new
                              {
                                  n.UNID,
                                  n.EventUNUID,
                                  noOfUsersAttented = prlist.Where(o=>o.UNID == n.UNID).Select(p=>p.UserUNID).Distinct().Count(),
                                  TotalQustions = EventInteracionDetailList.Where(o=>o.UNID == n.UNID).Count(),
                                  Question = EventInteracionDetailList.Where(o => o.UNID == n.UNID).FirstOrDefault().Question,
                                  QuestionID = EventInteracionDetailList.Where(o => o.UNID == n.UNID).FirstOrDefault().QuestionID,
                                  QuestionDescription = EventInteracionDetailList.Where(o => o.UNID == n.UNID).FirstOrDefault().QuestionDescription,


                              }).ToList();



                GridPolls.DataSource = frList;
                GridPolls.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnNewSurvey_Click(object sender, EventArgs e)
        {
            Response.Redirect("Set_Up_a_Survey.aspx?eventunid="+QueryStringValues.EVENTUNID+"&UNID="+QueryStringValues.UNID+"&EID=0",false);
        }

        protected void GridPolls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
                var edetails = pRep.GetAll().Where(o => o.UNID == e.CommandArgument.ToString() ).FirstOrDefault();
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
                    var elist = pRep.GetAll().Where(o => o.UNID == e.CommandArgument.ToString()).ToList();
                    foreach(var p in elist)
                    {
                        pRep.Delete(p);
                    }
                    
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    BingGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/Events/EventInteraction.aspx?eventunid=" + QueryStringValues.EVENTUNID , false);
        }
    }




}