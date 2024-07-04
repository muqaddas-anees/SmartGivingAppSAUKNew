using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    //public partial class LiveSessions : System.Web.UI.Page
    //{
    //    protected void Page_Load(object sender, EventArgs e)
    //    {

    //    }
    //}



    public partial class LiveSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                   
                    BindListView();

                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction()", true);
                }
                else
                {

                }
                if (sessionKeys.Message.Length > 0)
                {
                    //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                    sessionKeys.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindReligion()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int religionID)
        {
            try
            {
                if (religionID > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                   
                }

                // ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlDenimination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindListView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.BtnGetVideo_Click(sender, e);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        //
       
        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                BindListView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindListView()
        {
            try
            {


                

                IPortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail>();
                var data = pRep.GetAll().Where(o => o.SessionId > 0).ToList();

                



                ListView1.DataSource = data.OrderByDescending(o => o.SessionId).ToList();
                ListView1.DataBind();



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetVideoID(string url)
        {
            string retval = string.Empty;
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (query["v"] != null)
            {
                retval = query["v"];
            }
            else
            {
                retval = url.Substring(url.LastIndexOf("/") + 1);
            }


            return retval;
        }
        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/sessions/AddLiveSession.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {

                    //string v = e.CommandArgument.ToString();

                    //PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_delete(Convert.ToInt32(v));

                    //BindListView();
                }
                else if (e.CommandName == "Edit1")
                {
                    Response.Redirect("~/App/sessions/AddLiveSession.aspx?eid=" + e.CommandArgument.ToString(), false);
                }

                else if (e.CommandName == "View")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail>();
                    var data = pRep.GetAll().Where(o => o.SessionId == Convert.ToInt32( e.CommandArgument.ToString())).FirstOrDefault();
                    if(data != null)
                    Response.Redirect("~/App/sessions/LiveSession.aspx?unid="+ data.unid, false);
                }

                else if (e.CommandName == "Attending")
                {
                    //Response.Redirect("~/App/AddLiveSession.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void LinkButtonClick_Click(object sender, EventArgs e)
        {

        }

        protected void BtnGetVideo_Click(object sender, EventArgs e)
        {
          
            BindListView();
        }


        protected void MarkasAttending(object sender, EventArgs e)
        {


            Button button = (Button)sender;
            string buttonId = button.ID;
            string cm = button.CommandArgument;

            button.BackColor = System.Drawing.Color.Red;





        }



        protected void ViewLiveSessionsInZoom(object sender, EventArgs e)
        {

            


        }





        protected void EditLiveSessions(object sender, EventArgs e)
        {




        }

        protected string GetImmage(object activityid)
        {
            string retval = string.Empty;
            if (activityid != null)
            {
                if (File.Exists(Server.MapPath("~/WF/UploadData/sessions/" + activityid.ToString() + ".png")))
                {
                    retval = "~/WF/UploadData/sessions/" + activityid + ".png";
                }

                else
                {

                    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
                }
            }
            else
                retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            return retval;

        }



        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

        }
    }

}