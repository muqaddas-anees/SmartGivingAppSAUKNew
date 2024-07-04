using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VT.DAL;
using VT.Entity;
using System.IO;

namespace VT
{
    public partial class ApproveRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.PageHead = "Approve Requests";
        }

        protected bool enableApprove(string status)
        {
            status = status.Trim();
            if (status.Equals("Pending") || status.Equals("Rejected"))
                return true;
            else
                return false;
        }

        //protected bool enableRejection(string status,string fromdate)
        //{
        //    status = status.Trim();
        //    DateTime Ffromdate = Convert.ToDateTime(fromdate);
        //    if (status.Equals("Pending") || status.Equals("Approved") || Ffromdate >= DateTime.Now)
        //        return true;
        //    else
        //        return false;
        //}
        protected bool enableRejection(string status, string fromdate)
        {
            status = status.Trim();
            DateTime Ffromdate = Convert.ToDateTime(fromdate);
            if (status.Equals("Pending") || status.Equals("Approved"))
            {
                if (sessionKeys.SID != 1)
                {
                    return false;
                }
                else if (Ffromdate < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        protected void grdrequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                GridViewRow Row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int ID = Convert.ToInt32(e.CommandArgument);
                LeaveApproverHelper helper = new LeaveApproverHelper();

                Label lbluser = (Label)Row.FindControl("lblusername");
                Label lblusermail = (Label)Row.FindControl("lbluseremail");
                Label lbldays = (Label)Row.FindControl("lbldays");
                Label lblapprovermail = (Label)Row.FindControl("lblapproveremail");
                Label lblfrom = (Label)Row.FindControl("lblfromdate");
                Label lblto = (Label)Row.FindControl("lbltodate");
                Label lblstatus = (Label)Row.FindControl("lblstatus");
                Label lblabsence = (Label)Row.FindControl("lblabsensetype");
                TextBox ApproverNotes = (TextBox)Row.FindControl("txtapprovernotes");

                if (e.CommandName.Equals("Approve"))
                {
                    helper.SetApprovalStatus(ID, Convert.ToInt32(ApprovalStatus.Approved), ApproverNotes.Text.Trim());
                    objRequests.DataBind();
                    grdrequests.DataBind();
                    Mailer(lblusermail.Text.Trim(), lblapprovermail.Text.Trim(), lblabsence.Text, lbluser.Text, lblfrom.Text, lblto.Text, lbldays.Text, "Approved", ApproverNotes.Text);
                }
                else
                    if (e.CommandName.Equals("Reject"))
                    {
                        helper.SetApprovalStatus(ID, Convert.ToInt32(ApprovalStatus.Rejected), ApproverNotes.Text.Trim());
                        objRequests.DataBind();
                        grdrequests.DataBind();
                        Mailer(lblusermail.Text.Trim(), lblapprovermail.Text.Trim(), lblabsence.Text, lbluser.Text, lblfrom.Text, lblto.Text, lbldays.Text, "Rejected", ApproverNotes.Text);
                    }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void grdrequests_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdrequests.EditIndex = e.NewEditIndex;
        }

        private void Mailer(string usermail,string approvermail,string Type, string RecieveraName, string FromDate, string ToDate, string Days, string Status, string Notes)
        {
            try
            {
                VTMail1.Visible = true;

                VTMail1.Binddata(Type,RecieveraName,FromDate,ToDate,Days,Status, Notes);
                string htmlText = string.Empty;
                StringWriter sw = new StringWriter();
                Html32TextWriter htmlTW = new Html32TextWriter(sw);
                VTMail1.RenderControl(htmlTW);
                htmlText = htmlTW.InnerWriter.ToString();
                string errorString = string.Empty;
                Email eMail = new Email();

                if (!string.IsNullOrEmpty(approvermail))
                {
                    eMail.SendingMail(approvermail, "Leave Request " + Status, htmlText, usermail,"");
                }
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                VTMail1.Visible = false;
            }

          
            //string htmlText = string.Empty;
            //HtmlHijacker HTML = new HtmlHijacker();
            //htmlText = HTML.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/NewIncident.aspx?IncidentID=" + incident.ID, ref errorString);
            //Email eMail = new Email();

        }
}
}
