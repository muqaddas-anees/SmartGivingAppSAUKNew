using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events
{
    public partial class EventInteraction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                var rdata = rdlist.SelectedValue;

                if (rdata == "Fundraiser")
                    Response.Redirect("~/App/AddFundraiser.aspx?eventunid=" + QueryStringValues.UNID, false);
                else if (rdata == "Poll")
                    Response.Redirect("~/App/Events/PollAndSurvey/Polls/Set_up_a_Poll.aspx?eventunid=" + QueryStringValues.UNID, false);
                else if (rdata == "Survey")
                    Response.Redirect("~/App/Events/PollAndSurvey/Survey/Set_Up_a_Survey.aspx?eventunid=" + QueryStringValues.UNID, false);


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                var rdata = rdlist.SelectedValue;

                if (rdata == "Fundraiser")
                    Response.Redirect("~/App/FundraiserList.aspx?eventunid=" + QueryStringValues.UNID, false);
                else if (rdata == "Poll")
                    Response.Redirect("~/App/Events/PollAndSurvey/Polls/Grid_Set_Up_New_Poll.aspx?eventunid=" + QueryStringValues.UNID, false);
                else if (rdata == "Survey")
                    Response.Redirect("~/App/Events/PollAndSurvey/Survey/Grid_Set_Up_New_Survey.aspx?eventunid=" + QueryStringValues.UNID, false);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}