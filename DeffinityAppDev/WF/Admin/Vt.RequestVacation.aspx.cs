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
using UserMgt.DAL;
using UserMgt.Entity;

public partial class RequestVacation :BasePage
{

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.VacationTracker;
        
    }


    #endregion

    #region Control Events

    //protected void btnRequestLeave_Click(object sender, EventArgs e)
    //{
    //    LeaveRequest request = new LeaveRequest();
    //    request.AbsenseType = Convert.ToInt32(ddlAbsenceType.SelectedValue);
    //    request.ApprovalStatus = 1.ToString();
    //    request.FromDate = Convert.ToDateTime(txtDateFrom.Text);
    //    request.ToDate = Convert.ToDateTime(txtDateTo.Text);
    //    request.RequestNotes = txtNote.Text;
    //    request.RequesterID = Convert.ToInt32(ddlmembers.SelectedValue);
    //    request.MemberID = Convert.ToInt32(ddlmembers.SelectedValue);
    //    request.FromDatePeriod = float.Parse(ddlFromPeriod.SelectedValue);
    //    request.ToDatePeriod = float.Parse(ddlToPeriod.SelectedValue);

    //    if (!ddlTeams.SelectedItem.Text.Contains("SD-"))
    //    {
    //        request.TeamType = 1;// Convert.ToInt32(HD_TeamType.Value);
    //    }
    //    else
    //    {
    //        request.TeamType = 2;
    //    }

    //    request.SiteID = 0;
    //    LeaveRequestHelper helper = new LeaveRequestHelper();
    //    object result = helper.Insert(request);
    //    if (Convert.ToInt16(result) == 1)
    //    {
    //        lblMsg.Text = "No Allowance";
    //    }
    //    else if (Convert.ToInt16(result) == 2)
    //    {
    //        lblMsg.Text = "No Approvers";
    //    }
    //}

    #endregion

    #region Helper Methods

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txt_sFromdate.Text = Deffinity.Utility.StartDateOfMonth(DateTime.Now).ToShortDateString();
            txt_sTodate.Text = Deffinity.Utility.EndDateOfMonth(DateTime.Now).ToShortDateString();
            Bind_AbsentType();
            Bind_Customer();
            Bind_Team();
            Bind_Members();
            BindDate();
            Bind_Resources();
            //ddlmeridianto.Enabled = false;
            //ddlmeridianform.Enabled = false;
            //approvers();
            //SummaryBinding("0");
        }
        //DataList1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT_LEAVESUMMARY_USER", new SqlParameter("ResourceID", int.Parse("145")), new SqlParameter("@TeamType", int.Parse("1"))).Tables[0];
        //DataList1.DataBind();
        //add css dynamically
        //HtmlLink css = new HtmlLink();
        //css.Href = ResolveClientUrl("~/Content/css/ext-all.css");
        //css.Attributes["rel"] = "stylesheet";
        //css.Attributes["type"] = "text/css";
        ////css.Attributes["media"] = "all";
        //Page.Header.Controls.Add(css);

        //HtmlLink css2 = new HtmlLink();
        //css2.Href = ResolveClientUrl("~/Content/css/tabs.css");
        //css2.Attributes["rel"] = "stylesheet";
        //css2.Attributes["type"] = "text/css";
        //Page.Header.Controls.Add(css2);

        //HtmlLink css3 = new HtmlLink();
        //css3.Href = ResolveClientUrl("~/Content/css/linkedin-gray.css");
        //css3.Attributes["rel"] = "stylesheet";
        //css3.Attributes["type"] = "text/css";
        //Page.Header.Controls.Add(css3);
    }
    private void Bind_Customer()
    {
        //customer
        try
        {
            ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            ddlCustomer.DataTextField = "PortFolio";
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataBind();

            //if (sessionKeys.PortfolioID > 0)
            ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private void Bind_Team()
    {
        //team
        try 
        {
        DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomer.SelectedValue))).Tables[0];
        ddlTeam.DataSource = dt;
        ddlTeam.DataTextField = "TeamName";
        ddlTeam.DataValueField = "SDID";
        ddlTeam.DataBind();
        ddlTeam.Items.Insert(0, new ListItem("ALL", "0"));

        if (Request.QueryString["teamid"] != null)
        {
            try
            {
                ddlTeam.SelectedValue = Request.QueryString["teamid"].ToString();
            }
            catch (Exception ex) { ddlTeam.SelectedValue = "0"; }

        }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Bind_Resources()
    {
        try
        {
           //UserDataContext Resource = new UserDataContext();
           // var rslist = from r in Resource.Contractors
           //              where r.SID != 7 && r.SID != 8 && r.SID != 10 && r.Status != "InActive"
           //              orderby r.ContractorName ascending
           //              select r;
           // ddlMResource.DataSource = rslist.ToList();
           // ddlMResource.DataTextField = "ContractorName";
           // ddlMResource.DataValueField = "ID";
           // ddlMResource.DataBind();
           // ddlMResource.Items.Insert(0, new ListItem("ALL", "0"));

            DataTable dt = new DataTable();
            if (ddlTeam.SelectedItem.Text == "All")
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID as ID ,ContractorName as Name from Contractors where  status ='Active' and SID not in (7,8,10) order by Name", ddlTeam.SelectedValue)).Tables[0];
            }
            else if (!ddlTeam.SelectedItem.Text.Contains("SD-"))
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT distinct t.Name id, c.ContractorName Name FROM TeamMember as t INNER JOIN Contractors c ON t.Name = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and t.TeamID = {0} order by ContractorName", ddlTeam.SelectedValue)).Tables[0];
            }
            else
            {
                string SDID = ddlTeam.SelectedValue.Trim("SD-".ToCharArray()).Trim();
                dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "SELECT distinct c.ID, c.ContractorName as Name FROM SDteamToUsers s INNER JOIN Contractors c ON s.UserID = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and s.SDteamID = @TEAMID order by ContractorName", new SqlParameter("@TEAMID", Convert.ToInt32(SDID))).Tables[0];
            }
            ddlMResource.Items.Clear();
            ddlMResource.DataSource = dt;
            ddlMResource.DataTextField = "Name";
            ddlMResource.DataValueField = "ID";
            ddlMResource.DataBind();
            ddlMResource.Items.Insert(0, new ListItem("All", "0"));


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    

    private void Bind_Members()
    {
        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                ddlmembers.DataSource = (from p in ud.Contractors
                                         where p.SID != 8 && p.Status.ToLower() == "active"
                                         orderby p.ContractorName 
                                         select new { ID = p.ID, ContractorName = p.ContractorName }).ToList();

                //ddlmembers.DataSource = Deffinity.Bindings.DefaultDatabind.UserSelectAll_Withselect(true);
                ddlmembers.DataValueField = "ID";
                ddlmembers.DataTextField = "ContractorName";
                ddlmembers.DataBind();

               ddlmembers.Items.Insert(0, new ListItem("Please select...","0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        if (!ddlTeams.SelectedItem.Text.Contains("SD-"))
    //        {
    //            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT TeamMember.ID,(select ContractorName from Contractors where ID = TeamMember.Name) as Name FROM TeamMember INNER JOIN Team ON TeamMember.TeamID = Team.ID where Team.ID = {0} and PortfolioID = {1}", ddlTeams.SelectedValue.Trim(), sessionKeys.PortfolioID)).Tables[0];
    //            h_teamtype.Value = "1";
    //            HD_TeamType.Value = "1";
    //        }
    //        else
    //        {
    //            string SDID = ddlTeams.SelectedValue.Trim("SD-".ToCharArray()).Trim();
    //            dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETSD_TEAMMEMBERS", new SqlParameter[] { new SqlParameter("@PORTFOLIOID", sessionKeys.PortfolioID), new SqlParameter("@TEAMID", SDID) }).Tables[0];
    //            h_teamtype.Value = "2";
    //            HD_TeamType.Value = "2";
    //        }

    //        ddlmembers.DataSource = dt;
    //        ddlmembers.DataValueField = "ID";
    //        ddlmembers.DataTextField = "Name";
    //        ddlmembers.DataBind();
    //        // SummaryBinding();

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}
    //protected void ddlTeams1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        if (!ddlTeams1.SelectedItem.Text.Contains("SD-"))
    //        {
    //            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT TeamMember.ID,(select ContractorName from Contractors where ID = TeamMember.Name) as Name FROM TeamMember INNER JOIN Team ON TeamMember.TeamID = Team.ID where Team.ID = {0} and PortfolioID = {1}", ddlTeams1.SelectedValue.Trim(), sessionKeys.PortfolioID)).Tables[0];
    //            //h_teamtype.Value = "1";
    //        }
    //        else
    //        {
    //            string SDID = ddlTeams1.SelectedValue.Trim("SD-".ToCharArray()).Trim();
    //            dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETSD_TEAMMEMBERS", new SqlParameter[] { new SqlParameter("@PORTFOLIOID", sessionKeys.PortfolioID), new SqlParameter("@TEAMID", SDID) }).Tables[0];
    //           // h_teamtype.Value = "2";
    //        }
    //        ddlmembers1.DataSource = dt;
    //        ddlmembers1.DataValueField = "ID";
    //        ddlmembers1.DataTextField = "Name";
    //        ddlmembers1.DataBind();
    //        SummaryBinding("0");

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}

    #endregion
   
    private void SummaryBinding(string teamtype)
    {
        try
        {

            DataTable Dt_allowance;
            //Dt_allowance = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_Mod", new SqlParameter("ResourceID", int.Parse(ddlmembers.SelectedValue))).Tables[0];
            Dt_allowance = VT.DAL.LeaveRequestHelper.DisplayResourceSummary(int.Parse(ddlmembers.SelectedValue));
            dlist_summary.DataSource = Dt_allowance;
            dlist_summary.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //HID_Alloance.Value = Dt_allowance.Rows[0]["ALLOWANCE"].ToString();
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Team();
        Bind_Resources();

        //need to Bind Resource
    }

    protected void ddlmembers_SelectedIndexChanged(object sender, EventArgs e)
    {
        SummaryBinding(ddlmembers.SelectedValue);
        //approvers();
    }
    protected void btnViewData_Click(object sender, EventArgs e)
    {
        try
        {
        sessionKeys.PortfolioID = int.Parse(ddlCustomer.SelectedValue);
        sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
        //sessionKeys.ContractorId  =int.Parse(ddlMResource.SelectedValue);    

        //Response.Redirect(string.Format("~/VT.RequestVacation.aspx?teamid={0}&resourceid={1}", ddlTeam.SelectedValue,ddlMResource.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindDate()
    {
        int Year,PrevYear;

        string date ="31/03/"+DateTime.Now.Year.ToString();
        DateTime startdate = Convert.ToDateTime(date);
        
        if (DateTime.Now <= startdate)
        {
            Year = DateTime.Now.Year;
            PrevYear = DateTime.Now.Year - 1;
        }
        else
        {
            Year = DateTime.Now.Year+1;
            PrevYear = DateTime.Now.Year;
        }

       
        //lblthisyear.Text += "Allowance Summary for Period: <br/> 01 Apr " + PrevYear.ToString() + "- 31 March " + Year.ToString();
        //lblSummary_edit.Text += "Allowance Summary for Period: <br/> 01 Apr " + PrevYear.ToString() + "- 31 March " + Year.ToString();
    }

    //#region web service to bind resource type
    //[System.Web.Services.WebMethod]
    //public static IEnumerable<VTSummary> GetData(string resourceid)
    //{
    //    List<VTSummary> PTR = new List<VTSummary>();
    //    DataSet ds = new DataSet();
    //    ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_mod", new SqlParameter("ResourceID", int.Parse(resourceid))); ;
    //    int R_cont = ds.Tables[0].Rows.Count;
    //    if (R_cont > 0)
    //    {
    //        for (int T1_cnt = 0; T1_cnt <= ds.Tables[0].Rows.Count - 1; T1_cnt++)
    //        {
    //            var emp = new VTSummary
    //            {
    //                vts_title = ds.Tables[0].Rows[T1_cnt]["Titles"].ToString(),
    //                vts_val = ds.Tables[0].Rows[T1_cnt]["sum_values"].ToString()

    //            };
    //            PTR.Add(emp);
    //        }
    //    }
    //    return PTR;
    //}


    //#endregion
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
        request.RequesterID = Convert.ToInt32(ddlmembers.SelectedValue);
        request.MemberID = Convert.ToInt32(ddlmembers.SelectedValue);
        request.FromDatePeriod = float.Parse(ddlFromPeriod.SelectedValue);
        request.ToDatePeriod = float.Parse(ddlToPeriod.SelectedValue);
        request.FromDateMeridian = (request.FromDatePeriod == 0 ? 0 : int.Parse(ddlmeridianform.SelectedValue));
        request.ToDateMeridian = (request.ToDatePeriod == 0 ? 0 : int.Parse(ddlmeridianto.SelectedValue));
        request.SiteID = 0;
        LeaveRequestHelper helper = new LeaveRequestHelper();

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
                object ID = helper.Insert(request);
                SummaryBinding(ddlmembers.SelectedValue);
                Mailer(Convert.ToInt16(ID),true,false);
                //Mailer(HID_RequesterEmail.Value.ToString(),HID_ApproverEmail.Value.ToString(),
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }

    private DataTable Get_AbsentType()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Type from VT.AbsenseType order by type").Tables[0];
    }
    private void Bind_AbsentType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Get_AbsentType();

            ddl_edit_Absenttype.DataSource = dt;
            ddl_edit_Absenttype.DataTextField = "Type";
            ddl_edit_Absenttype.DataValueField = "ID";
            ddl_edit_Absenttype.DataBind();
            ddl_edit_Absenttype.Items.Insert(0, new ListItem("Please select..", "0"));
            ddlAbsenceType.DataSource = dt;
            ddlAbsenceType.DataTextField = "Type";
            ddlAbsenceType.DataValueField = "ID";
            ddlAbsenceType.DataBind();
            ddlAbsenceType.Items.Insert(0, new ListItem("Please select..", "0"));
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
    private void Mailer(int ID, bool Request, bool Cancel)
    {
        try
        {
            string user, usermail, approvermail, Type, RecieveraName, Days, Notes;
            DateTime FromDate, ToDate;
            LeaveRequestHelper helper = new LeaveRequestHelper();
            DataTable dt = helper.GetLeaveRequest(ID);
            Type = dt.Rows[0]["AbsenseType"].ToString();
            user = dt.Rows[0]["UserName"].ToString();
            usermail = dt.Rows[0]["UserEmail"].ToString();
            approvermail = dt.Rows[0]["ApproverEmail"].ToString();
            RecieveraName = dt.Rows[0]["Approver"].ToString();
            FromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString());
            ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString());
            Days = dt.Rows[0]["Days"].ToString();
            Notes = dt.Rows[0]["Notes"].ToString();


            VTMail1.Visible = true;

            VTMail1.Binddata(user, Type, RecieveraName, FromDate.ToShortDateString(), ToDate.ToShortDateString(), Days, Notes, Request,Cancel);
            string htmlText = string.Empty;
            StringWriter sw = new StringWriter();
            Html32TextWriter htmlTW = new Html32TextWriter(sw);
            VTMail1.RenderControl(htmlTW);
            htmlText = htmlTW.InnerWriter.ToString();
            string errorString = string.Empty;
            Email eMail = new Email();

            if (!string.IsNullOrEmpty(approvermail))
            {
                eMail.SendingMail(usermail, Resources.DeffinityRes.LeaveRequestFor + user, htmlText, approvermail,"");
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
    //protected void ddlFromPeriod_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlFromPeriod.SelectedValue == "0.5")
    //    {
    //        ddlmeridianform.Enabled = true;
    //    }
    //    else if (ddlFromPeriod.SelectedValue == "0")
    //    {
    //        ddlmeridianform.Enabled = false;
    //    }
    //}
    //protected void ddlToPeriod_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlToPeriod.SelectedValue == "0.5")
    //    {
    //        ddlmeridianto.Enabled = true;
    //    }
    //    if (ddlToPeriod.SelectedValue == "0")
    //    {
    //        ddlmeridianto.Enabled = false;
    //    }
    //}
    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Resources();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}

//#region Class_ProjectResourcetype
//public class VTSummary
//{
//    public string vts_title { get; set; }
//    public string vts_val { get; set; }

//}
//#endregion

