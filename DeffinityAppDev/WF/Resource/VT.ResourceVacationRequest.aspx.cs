using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VT.Entity;
using VT.DAL;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ResourceVacationRequest : BasePage
{

    #region Page Events
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.VacationTracker;
    }


    #endregion

    #region Control Events

    
    #endregion

    #region Helper Methods



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bind_AbsentType();
            BindDate();
            SummaryBinding();
            txt_edit_resource.Text = sessionKeys.UName;
            //approvers();
            //SummaryBinding("0");
        }
        //HtmlLink css1 = new HtmlLink();
        //css1.Href = ResolveClientUrl("~/stylcss/jquery.tabs.css");
        //css1.Attributes["rel"] = "stylesheet";
        //css1.Attributes["type"] = "text/css";
        //css1.Attributes["media"] = "print, projection, screen";
        //Page.Header.Controls.Add(css1);
       
    }
   
   

    #endregion

    private void SummaryBinding()
    {
        try
        {
            //Current year
            dlist_summary.DataSource = RetSummaryTable(0);
            dlist_summary.DataBind();

            //previous year
            dl_previous.DataSource = RetSummaryTable(-1);
            dl_previous.DataBind();

            //Next year
            dl_nextyear.DataSource = RetSummaryTable(1);
            dl_nextyear.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private DataTable RetSummaryTable(int addYear)
    {
        DataTable Dt_allowance;
        //Dt_allowance = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_Mod", new SqlParameter("ResourceID", sessionKeys.UID)).Tables[0];
        Dt_allowance = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_byYear", new SqlParameter("@ResourceID", sessionKeys.UID), new SqlParameter("@addYear", addYear)).Tables[0];

        return Dt_allowance;
    }
    private void BindDate()
    {
        int Year, PrevYear;
        string date = "31/03/" + DateTime.Now.Year.ToString();
        DateTime startdate = Convert.ToDateTime(date);

        if (DateTime.Now <= startdate)
        {
            Year = DateTime.Now.Year;
            PrevYear = DateTime.Now.Year - 1;
        }
        else
        {
            Year = DateTime.Now.Year + 1;
            PrevYear = DateTime.Now.Year;
        }

        //lblthisyear.Text += "Allowance Summary for Period: <br/> 01 Apr " + PrevYear.ToString() + "- 31 March " + Year.ToString();
        //lblSummary_edit.Text += "Allowance Summary for Period: 01 Apr " + PrevYear.ToString() + "- 31 March " + Year.ToString();
    }

    #region web service to bind resource type
    [System.Web.Services.WebMethod]
    public static IEnumerable<VTSummary> GetData(string resourceid)
    {
        List<VTSummary> PTR = new List<VTSummary>();
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource", new SqlParameter("ResourceID", int.Parse(resourceid))); ;
        int R_cont = ds.Tables[0].Rows.Count;
        if (R_cont > 0)
        {
            for (int T1_cnt = 0; T1_cnt <= ds.Tables[0].Rows.Count - 1; T1_cnt++)
            {
                var emp = new VTSummary
                {
                    vts_title = ds.Tables[0].Rows[T1_cnt]["Titles"].ToString(),
                    vts_val = ds.Tables[0].Rows[T1_cnt]["sum_values"].ToString()

                };
                PTR.Add(emp);
            }
        }
        return PTR;
    }


    #endregion
    protected void btnRequestLeave_Click(object sender, EventArgs e)
    {
        try
        {
            LeaveRequest request = new LeaveRequest();
            request.AbsenseType = Convert.ToInt32(ddlAbsenceType.SelectedValue);
            request.ApprovalStatus = 1.ToString();
            request.FromDate = Convert.ToDateTime(txtDateFrom.Text);
            request.ToDate = Convert.ToDateTime(txtDateTo.Text);
            request.RequestNotes = txtNote.Text;
            request.RequesterID = sessionKeys.UID;
            request.MemberID = sessionKeys.UID;
            request.FromDatePeriod = float.Parse(ddlFromPeriod.SelectedValue);
            request.ToDatePeriod = float.Parse(ddlToPeriod.SelectedValue);
            request.FromDateMeridian = (request.FromDatePeriod == 0 ? 0 : int.Parse(ddlmeridianform.SelectedValue));
            request.ToDateMeridian = (request.ToDatePeriod == 0 ? 0 : int.Parse(ddlmeridianto.SelectedValue));
            request.SiteID = 0;
            LeaveRequestHelper helper = new LeaveRequestHelper();
            if (request.AbsenseType == -99 && (request.FromDate > DateTime.Now || request.ToDate > DateTime.Now))
            {
                lblMsg.Text = Resources.DeffinityRes.YoucannotbookSickonafuturedate;
            }
            else
            {
                object result = helper.Insert_PreRequest(request);
                if (Convert.ToInt16(result) == 1)
                {
                    lblMsg.Text = Resources.DeffinityRes.Sorrybutyouhavealready;
                    
                }
                else if (Convert.ToInt16(result) == 2)
                {
                    lblMsg.Text = Resources.DeffinityRes.Theallowanceinformation;
                }
                else if (Convert.ToInt16(result) == 3)
                {
                    lblMsg.Text = Resources.DeffinityRes.Approverinformation;
                }
                else if (Convert.ToInt16(result) == 4)
                {
                    lblMsg.Text = Resources.DeffinityRes.Pleasecheckdaterange;
                }
                else if (Convert.ToInt16(result) == 5)
                {
                    lblMsg.Text = Resources.DeffinityRes.Pleasecheckallowance;
                }
                else if (Convert.ToInt16(result) == 6)
                {
                    lblMsg.Text = Resources.DeffinityRes.TimeinLieuallowance;
                }
                else if (Convert.ToInt16(result) == 7)
                {
                    lblMsg.Text = Resources.DeffinityRes.TimeinLieuallowanceisexceeded;
                }
                else if (Convert.ToInt16(result) == 0)
                {
                   object ID= helper.Insert(request);
                    grdrequests.DataBind();
                    SummaryBinding();
                    Mailer(Convert.ToInt16(ID), true, false);
                    
                    ClearFields();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void ClearFields()
    {
        ddlAbsenceType.SelectedIndex = 0;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
        ddlFromPeriod.SelectedIndex = 0;
        ddlmeridianform.SelectedIndex = 0;
        ddlmeridianto.SelectedIndex = 0;
        ddlToPeriod.SelectedIndex = 0;
    }
    private DataTable Get_AbsentType()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Type from VT.AbsenseType order by Type").Tables[0];
    }
    private void Bind_AbsentType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Get_AbsentType();

            //ddl_edit_Absenttype.DataSource = dt;
            //ddl_edit_Absenttype.DataTextField = "Type";
            //ddl_edit_Absenttype.DataValueField = "ID";
            //ddl_edit_Absenttype.DataBind();
            //ddl_edit_Absenttype.Items.Insert(0, new ListItem("Please select..", "0"));
            ddlAbsenceType.DataSource = dt;
            ddlAbsenceType.DataTextField = "Type";
            ddlAbsenceType.DataValueField = "ID";
            ddlAbsenceType.DataBind();
            ddlAbsenceType.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected bool enableCacel(string status)
    {
        bool enab = false;
        try
        {
            

                //status = status.Trim();
                //if (status.Equals("Pending"))
                //{
                //    //Label chkdate = (Label)grdrequests.Rows[0].FindControl("lblToDate");

                //    if ((sessionKeys.SID != 1)&&(Convert.ToDateTime(chkdate.Text) > DateTime.Now))
                //    {
                //        enab = false;
                //        LinkButton lnkcancel = (LinkButton)grdrequests.Rows[0].FindControl("btnReject");
                //        lnkcancel.Visible = false;

                //    }

                //    else
                //    {
                //        enab = true;
                //    }
                   
                //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return enab;
        
    }
    protected void grdrequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label chkdate = (Label)e.Row.FindControl("lblFromDate");
                Label status = (Label)e.Row.FindControl("lblStatus");
                if (sessionKeys.SID != 1) 
                {

                    LinkButton lnkcancel = (LinkButton)e.Row.FindControl("btnReject");
                    if (status.Text == "Approved") 
                    {
                        lnkcancel.Visible = false;
                    }
                    else if (Convert.ToDateTime(chkdate.Text) < DateTime.Now)
                    {
                        lnkcancel.Visible = false;
                    }
                    else
                    {
                        lnkcancel.Visible = true;
                    }

                }
                else
                {
                    LinkButton lnkcancel = (LinkButton)e.Row.FindControl("btnReject");
                   
                    lnkcancel.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdrequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            LeaveRequestHelper helper = new LeaveRequestHelper();

            if (e.CommandName.Equals("Cancel"))
            {
                Mailer(ID, false, true);
                helper.Delete(ID);
                objRequests.DataBind();
                grdrequests.DataBind();
                SummaryBinding();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    public System.Drawing.Color Hilightcolor(string Attr)
    {
        if (Attr == "Total Available for this year:")
        {
          return System.Drawing.Color.BlueViolet;
        }
        if (Attr == "Remaining:(Excl of Sick and Discretionary)")
        {
            return System.Drawing.Color.Red;
        }
        if (Attr == "Approved:(Incl of Sick and Discretionary)")
        {
            return System.Drawing.Color.Green;
        }
        else

            return System.Drawing.Color.Gray;
    }
    

    private void Mailer(int ID,bool Request,bool Cancel)
    {
        try
        {
            string user,usermail, approvermail, Type, RecieveraName,Days, Notes;
            DateTime FromDate, ToDate;
            LeaveRequestHelper helper = new LeaveRequestHelper();
            DataTable dt = helper.GetLeaveRequest(ID);
            Type = dt.Rows[0]["AbsenseType"].ToString();
            user = dt.Rows[0]["UserName"].ToString(); 
            usermail = dt.Rows[0]["UserEmail"].ToString().Trim();
            approvermail = dt.Rows[0]["ApproverEmail"].ToString().Trim();
            RecieveraName = dt.Rows[0]["Approver"].ToString();
            FromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString());
            ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString()); 
            Days = dt.Rows[0]["Days"].ToString();
            Notes = dt.Rows[0]["Notes"].ToString();


            VTMail1.Visible = true;

            VTMail1.Binddata(user, Type, RecieveraName, FromDate.ToShortDateString(), ToDate.ToShortDateString(), Days, Notes,Request,Cancel);
            string htmlText = string.Empty;
            StringWriter sw = new StringWriter();
            Html32TextWriter htmlTW = new Html32TextWriter(sw);
            VTMail1.RenderControl(htmlTW);
            htmlText = htmlTW.InnerWriter.ToString();
            string errorString = string.Empty;
            Email eMail = new Email();

            if (!string.IsNullOrEmpty(approvermail))
            {
                eMail.SendingMail(usermail, Resources.DeffinityRes.LeaveRequestFor +" "+ user, htmlText, approvermail,"");
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

#region Class_ProjectResourcetype
public class VTSummary
{
    public string vts_title { get; set; }
    public string vts_val { get; set; }

}
#endregion

