using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Incidents.Entity;
using Incidents.DAL;
using Incidents.StateManager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Mail;
using System.Net.Mail;
using System.Data.Common;
using System.IO;
using System.Linq;





public partial class CCAddApproval : System.Web.UI.Page
{
    ReportDocument rpt = null;
    ReportDocument rptApproval = null;
    ClsChangeControl ibjCls = new ClsChangeControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
           
            string AppVal = Request.QueryString["ApprovalVal"];
            if (AppVal != null)
            {
                int ApprovalVal = Convert.ToInt32(AppVal);
                bool Approval = Convert.ToBoolean(ApprovalVal);
                int CCID = Convert.ToInt16(Request.QueryString["CCID"]);
                object objVal = ibjCls.ApprovalUpdate(CCID, Approval);

            }
            if (sessionKeys.ChangeControlID == 0)
            {
                lblPageTitle.InnerText = "Change Control Approval -  Log New Change Control Request";

            }
            else
            {
                lblPageTitle.InnerText = "Change Control Approval - Reference " + sessionKeys.ChangeControlID.ToString();

            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Master.PageHead = "Change Control";
        //if (string.IsNullOrEmpty(txtDateRaised.Text))
            //txtDateRaised.Text = DateTime.Now.Date.ToShortDateString();
        if (sessionKeys.ChangeControlID == 0)
        {
            lblPageTitle.InnerText = "Change Control -  Log New Change Control Request";
            //btnAddChangeControl.Visible = true;
            //btnUpdateChangeControl.Visible = false;
        }
        else
        {
            lblPageTitle.InnerText = "Change Control - Reference " + sessionKeys.ChangeControlID.ToString();
            //btnAddChangeControl.Visible = false;
            //btnUpdateChangeControl.Visible = true;
        }
    }
    private void InsertApproval()
    {
        try
        {
            if (sessionKeys.ChangeControlID == 0)
            {
                lblApproval.Text = "Needs to add the Change Control, before you assign the Risk.";
            }
            else
            {
                Approval approval = new Approval();
                approval.Title = txtTitle1.Text.Trim();
                approval.DateApproved = Convert.ToDateTime("1/1/1900");
                approval.Comments = txtComments1.Text.Trim();
                approval.ChangeControlID = sessionKeys.ChangeControlID;
                approval.Approved = false;
                approval.ApprovalID = Convert.ToInt32(ddlName1.SelectedValue);
                ApprovalHelper.Insert(approval);
                GridView3.DataBind();
                ClearApproval();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ClearApproval()
    {
        txtTitle1.Text = string.Empty;
        ddlName1.ClearSelection();
        txtComments1.Text = string.Empty;
    }

    protected void btnAddApproval_Click(object sender, EventArgs e)
    {
        InsertApproval();
    }

    protected string checkEmptyText(string originalText)
    {
        if (string.IsNullOrEmpty(originalText.Trim()))
            //return "<p style=\"color:Blue;background-color:Transparent\">No comments added yet.</p>";
            return "No comments added yet.";
        else
            return originalText;
    }

    protected string GetApprovedDate(DateTime approvedDate)
    {
        if (approvedDate.Year == 1900)
            return "Not approved";
        else
            return approvedDate.ToString("d");
    }

    protected string approvedOrDenied(object approvedOrDenied)
    {
        string displayString = string.Empty;
        try
        {

            if (approvedOrDenied != null)
                displayString = Convert.ToBoolean((approvedOrDenied)) ? "Approved" : "Denied";
            else
                displayString = "Not verified";

        }
        catch (Exception ex)
        {
            //LogExceptions.WriteExceptionLog(ex);
            lblApproval.Text = ex.Message;
        }
        return displayString;
    }
    protected void btnSendApproval_Click(object sender, EventArgs e)
    {
        BindReport(sessionKeys.ChangeControlID);
    }
    protected void BindReport(int ChangeControlId)
    {
        try
        {
            Attachment at = null;
            rpt = new ReportDocument();
            //string str = "Reports/CC_Approval_Test.rpt";
            string str = "~/WF/Reports/Final_ChangeControlReports.rpt";
            // string str = "Reports/CC_Aug02.rpt";//Working fine..
            try
            {
                string path = Server.MapPath(str);
                rpt.Load(path);
            }
            catch (Exception)
            {
                rpt.Dispose();
            }
            //string DataSource = System.Configuration.ConfigurationManager.AppSettings["DataSource"].ToString();
            //string InitialCatalog = System.Configuration.ConfigurationManager.AppSettings["InitialCatalog"].ToString();
            //string UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();
            //string Password = System.Configuration.ConfigurationManager.AppSettings["RPassword"].ToString();

            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

            //string DataSource = "tirumala";
            //string InitialCatalog = "DeffinityExcel_01_06_2010";
            //string UserID = "sa";
            //string Password = "ems";



            if (rpt != null)
            {
                rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
                ClsChangeControl rptMgr = new ClsChangeControl();
                DataTable ds = rptMgr.GetReports(ChangeControlId);
                rpt.SetDataSource(ds);


                rpt.Subreports["CCTaskReport"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
                DataTable dsTask = rptMgr.GetTaskReport(ChangeControlId);
                rpt.Subreports["CCTaskReport"].SetDataSource(dsTask);


                //    CCTask

                rpt.Subreports["CCRiskReport"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
                DataTable dsRisk = rptMgr.GetRiskReport(ChangeControlId);
                rpt.Subreports["CCRiskReport"].SetDataSource(dsRisk);

                //  CCRisk

                rpt.Subreports["CCApprovalReport"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
                DataTable dsApproval = rptMgr.GetApprovalReport(ChangeControlId);
                rpt.Subreports["CCApprovalReport"].SetDataSource(dsApproval);

                //CCApproval    
                string path = string.Format("~/WF/UploadData/PdfFiles/Report_{0}_{1}.pdf", sessionKeys.ChangeControlID, string.Format("{0:ddMMyyyymm}", DateTime.Now));
                //string path = "~//SupplierRequisitions//" +projectRef + "_" + User.Identity.Name + ".pdf";
                //Stream rptstream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(path));

                at = new Attachment(Server.MapPath(path));

            }
            ReportInPDF(at);
            // sendmail();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void ReportInPDF(Attachment at)
    {
        try
        {
            System.Web.Mail.MailMessage mailMessage = new System.Web.Mail.MailMessage();
            lblApproval.Text = "";
            System.IO.MemoryStream oStream = (System.IO.MemoryStream)rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //string FilePath = Server.MapPath(string.Format( "UploadData/PdfFiles/Report_{0}.pdf",sessionKeys.ChangeControlID));
            //System.IO.File.WriteAllBytes(FilePath, oStream.ToArray());
            Email objeMail = new Email();
            ArrayList al = new ArrayList();
            int CCIDVal = sessionKeys.ChangeControlID;
            DataTable objEmailID = ibjCls.UserEmailID(Convert.ToInt32(sessionKeys.UID));
            string EmailAddress = objEmailID.Rows[0]["EmailAddress"].ToString();
            //al.Add("divya@emsysindia.com");
            al.Add(EmailAddress);

            //objeMail.SendingMail(EmailAddress, "Change Control", 
            //    "<a href='http://localhost:56836/Deffinity_underwork/CCAddApproval.aspx?ApprovalVal=1&CCID=" + CCIDVal + "'>Accept</a>&nbsp;&nbsp;&nbsp;<a href='http://localhost:56836/Deffinity_underwork/CCAddApproval.aspx?ApprovalVal=0&CCID=" + CCIDVal + "'>Deny</a>"
            //    ,"info@deffinity.com", at);
            var cData = ChangeControlData(sessionKeys.ChangeControlID);
            string htmlText = string.Format("Dear <strong>{0}</strong>, <br/> <br/> Please review the attached change control relating to </strong> {1}</strong>.<br/> To access the system please click here.<br/> Thank you.<br/>",sessionKeys.UName, cData.Title);
            objeMail.SendingMail(EmailAddress, "Change Control Authorisation Required",htmlText
               , "info@deffinity.com", at);
            lblApproval.Text = "Mail sent successfully";
            lblApproval.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private ChangeControlMgt.Entity.ChangeControl ChangeControlData(int ChangeID)
    {
        using (ChangeControlMgt.DAL.ChangeControlDataContext cdc = new ChangeControlMgt.DAL.ChangeControlDataContext())
        {
            return cdc.ChangeControls.Where(o => o.ID == ChangeID).FirstOrDefault();
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmdApprove")
        {

            int ApprovalID = Convert.ToInt16(e.CommandArgument);
            object objVal = ibjCls.ApprovalOrDenyByID(ApprovalID, true);
            ApprovalState.ClearApprovalCache();
            GridView3.DataBind();

        }
        if (e.CommandName == "cmdDeny")
        {

            int ApprovalID = Convert.ToInt16(e.CommandArgument);
            object objVal = ibjCls.ApprovalOrDenyByID(ApprovalID, false);
            ApprovalState.ClearApprovalCache();
            GridView3.DataBind();
        }
    }
    //protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.FindControl("btnFooterApprove").ToString() == "Approve")
    //    { Dim lbl As Label   
    //lbl = CType(GridView1.Rows(i).FindControl("lblUsername"),Label)     
    //If (lbl.Text.Equals("admin")) Then             
    //    GridView1.Rows(i).Cells(0-based index of your EditButton).Visible = False     
    //        End If

    //    }
    //}
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //string sas = e.Row.FindControl("lblApprovingStatus").ToString();
        //if (e.Row.FindControl("lblApprovingStatus").ToString() == "Approve")
        //{

        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnFooterApprove = (LinkButton)e.Row.Cells[0].FindControl("btnFooterApprove");

            Label lblApproved = (Label)e.Row.FindControl("lblApprovingStatus");
            string s = lblApproved.Text.Trim();
            if (s != null)
            {
                if (s != "Approved")
                {
                    btnFooterApprove.Enabled = true;
                }
                else
                {
                    btnFooterApprove.Enabled = false;
                }
            }
        }

    }
}
