using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.IncidentService;
using Deffinity.IncidentService_Price_Manager;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IncidentMgt.DAL;
using System.Linq;
using System.Collections;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using System.Collections.Generic;

public partial class Servicedesk_sdcontrols_sd_customerservice1 : System.Web.UI.UserControl
{

    public string Type
    {
        set;
        get;
    }
    Incidents.Entity.Incident incid = new Incidents.Entity.Incident();
    IncidentDataContext obj = new IncidentDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Service Request";
        //if (sessionKeys.IncidentID > 0)
        //    lblTitle.InnerText = "Services - SR Reference " + sessionKeys.IncidentID;
        //else
        //    lblTitle.InnerText = "Services - New Service Request";
        //ServiceState.ClearServiceCache();
        if (!IsPostBack)
        {
            try
            {
                CustomerOrderToSDteam1.Type = Type;
                if (Request.QueryString["callid"] != null || Request.QueryString["callid"] == "0")
                {
                    CallDetail cd = DC.BLL.CallDetailsBAL.SelectbyId(Convert.ToInt32(Request.QueryString["callid"].ToString()));
                    //status is in Quotation Submitted
                    if (cd.StatusID == 40)
                    {

                        pnlService.Visible = true;
                        btnRequestQuote.Visible = true;
                       // btnApprove.Visible = true;
                        btnDeclain.Visible = true;
                        btnProcessorder.Visible = true;
                        chek_confirm.Visible = true;
                        txtPONumber.Visible = true;
                        lblPonumber.Visible = true;
                    }
                    else
                    {
                        btnRequestQuote.Visible = false;
                        pnlService.Visible = true;
                       // btnApprove.Visible = false;
                        btnDeclain.Visible = false;
                        btnProcessorder.Visible = false;
                        chek_confirm.Visible = false;
                        txtPONumber.Visible = false;
                        lblPonumber.Visible = false;
                    }
                    pnlOrder.Visible = true;
                }
                else
                {
                    pnlOrder.Visible = false;
                    btnRequestQuote.Visible = false;
                    pnlService.Visible = true;
                   // btnApprove.Visible = true;
                    btnDeclain.Visible = true;
                    btnProcessorder.Visible = false;
                }
                bindGrid();
                //fill and get the values for total
                Service_Prices();
                Getstatus(sessionKeys.IncidentID);
                // hide the units values
                Hide_unitsection();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }


    private void Hide_unitsection()
    {
        lblUnit_title.Visible = false;
        lbluc.Visible = false;
    }

    private void Getstatus(int IncidentID)
    {

        if (Type == "FLS")
        {
            CallDetail callDetail = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
            if (callDetail != null)
            {
                string statusName = StatusBAL.StatusNamebyId(Convert.ToInt32(callDetail.StatusID));
                if ((statusName.ToLower() == "quote requested") || (statusName.ToLower() == "order received") || (statusName.ToLower() == "cancelled") || (statusName.ToLower() == "quotation submitted"))
                {
                    pnlStatus.Visible = true;
                    lblStatus.InnerText = statusName;
                }
                else
                {
                    pnlStatus.Visible = false;
                    lblStatus.Visible = false;

                }
            }
        }
        else
        {
            Incidents.Entity.Incident incid = new Incidents.Entity.Incident();
            incid = Incidents.DAL.IncidentHelper.SelectById(IncidentID);
            if ((incid.Status == "Approved") || (incid.Status == "Pending Approval") || (incid.Status == "Quote Requested") || (incid.Status == "Declined") || (incid.Status == "In Hand"))
            {
                if (incid.IncidentType == "Service Request")
                {
                    pnlStatus.Visible = true;
                    lblStatus.InnerText = incid.Status;
                }
            }
            else
            {
                pnlStatus.Visible = false;
                lblStatus.Visible = false;

            }
        }

    }
    private void Service_Prices()
    {
        SqlDataReader dr = IncidentService_Price.IncidentService_Price_Select(sessionKeys.IncidentID,Type,QueryStringValues.IVREF);
        while (dr.Read())
        {
            lblTotalPrice.InnerText = string.Format("{0:f2}", dr["OriginalPrice"]);
            lblDiscountValue.InnerText = string.Format("{0:f2}", dr["DiscountPrice"]);
            lblDiscountPer.InnerText = dr["DiscountPercent"].ToString();
            lblRevisedPrice.InnerText = string.Format("{0:f2}", dr["RevicedPrice"]);
            lbluc.InnerText = string.Format("{0:f2}", dr["UnitConsumption"]);
            //txtNotes.Text = dr["Notes"].ToString();
            lblNotes.InnerText = dr["Notes"].ToString();
        }
        dr.Close();
        dr.Dispose();
    }

   


    protected void Grid_services_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Update")
        //{
        //    int ID, QTY;
        //    double SellingPrice;
        //    double units;
        //    int i = Grid_services.EditIndex;
        //    GridViewRow Row = Grid_services.Rows[i];
        //    ID = int.Parse(e.CommandArgument.ToString());
        //    QTY = int.Parse(((TextBox)Row.FindControl("txtQty")).Text);
        //    SellingPrice = double.Parse(((TextBox)Row.FindControl("txtSellingPrice")).Text);
        //    units = double.Parse(((TextBox)Row.FindControl("txtuc")).Text);
        //    //update
        //    ServiceManager.Services_Update(ID, QTY, SellingPrice, units);
        //    Service_Prices();
        //    //Gridupdate
        //    bindGrid();

        //    //fill and get the values for total

        //}

    }
    private void bindGrid()
    {
        Grid_services.DataSource = ServiceManager.Services_SelectByIncidentID(sessionKeys.IncidentID,Type,QueryStringValues.IVREF);
        Grid_services.DataBind();
    }
   
    protected void Grid_services_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            e.ExceptionHandled = true;
        }
    }
    protected void obj_services_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {

        if (e.Exception == null)
        {
            e.ExceptionHandled = true;
        }
    }
    protected void Grid_services_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Grid_services.EditIndex = -1;
        bindGrid();
    }
    protected void Grid_services_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        Grid_services.EditIndex = -1;
        bindGrid();
    }
    protected void Grid_services_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Grid_services.EditIndex = e.NewEditIndex;
        bindGrid();
    }
    //protected void btnSubmitToCustomer_Click(object sender, EventArgs e)
    //{
    //    BuildMail();
    //}
   
   
    //private Incident FillControlFields()
    //{
    //    Incident incident = new Incident();
    //    incident.Status = "Pending Approval";
    //    return incident;
    //}

    //Deffinity_CustomerOrderToSDteam

   
   
    //grid_delete
    protected void grid_delete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = sender as LinkButton;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Service_Delete",
                 new SqlParameter("@ID", int.Parse(btnDelete.CommandArgument.ToString())));

            //rebind the data
            bindGrid();
            Service_Prices();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void BuildDeclineMail()
    {
        SDCustomerDeclineMail1.Visible = true;
        try
        {
            if (sessionKeys.PortfolioID > 0)
            {
                Hashtable ht = new Hashtable();
                ArrayList ar = new ArrayList();

                var teamID = (from p in obj.Incident_AssignedTeams
                              where p.IncidentID == sessionKeys.IncidentID
                              select p.TeamID).SingleOrDefault();
                SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CustomerOrderToSDteam", new SqlParameter("@PortfolioID", sessionKeys.PortfolioID), new SqlParameter("@TeamID", teamID));
                while (dr.Read())
                {
                    try
                    {
                        ar.Add(new SDteamDetails(dr["UserID"].ToString(), dr["Email"].ToString(), dr["TeamName"].ToString()));
                        //ht.Add(dr["Email"].ToString(), dr["TeamName"].ToString());
                    }
                    catch
                    {
                        //avoid reted email id's
                    }
                }
                dr.Close();
                dr.Dispose();

                if (ar.Count > 0)
                {
                    //IDictionaryEnumerator en = ht.GetEnumerator();
                    //while (en.MoveNext())
                    //{
                    var result = (from p in obj.Incidents
                                  where p.ID == sessionKeys.IncidentID && p.PortfolioID == sessionKeys.PortfolioID
                                  select p).ToList();

                    foreach (SDteamDetails ar_sddetails in ar)
                    {

                        foreach (var i in result)
                        {

                            string[] _sddetails = ar_sddetails.ToString().Split(',');

                            SDCustomerDeclineMail1.BindControls(_sddetails[0],i.Subject);

                            StringWriter stringWrite = new StringWriter();
                            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                            SDCustomerDeclineMail1.RenderControl(htmlWrite);
                            Email ToEmail = new Email();
                            ToEmail.SendingMail(_sddetails[2], "Order Declined SR:"+sessionKeys.IncidentID+" "+ i.Subject, htmlWrite.InnerWriter.ToString());
                        }

                    }
                    // }
                }
                SDCustomerDeclineMail1.Dispose();
            }

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        finally { SDCustomerDeclineMail1.Visible = false; }


    }

    private void BuildMail()
    {
        SDCustomerApproveMail1.Visible = true;
        try

        {
            if (sessionKeys.PortfolioID > 0)
            {
                Hashtable ht = new Hashtable();
                ArrayList ar = new ArrayList();

                var teamID = (from p in obj.Incident_AssignedTeams
                             where p.IncidentID == sessionKeys.IncidentID
                             select p.TeamID).SingleOrDefault();
                SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CustomerOrderToSDteam", new SqlParameter("@PortfolioID", sessionKeys.PortfolioID), new SqlParameter("@TeamID", teamID));
                while (dr.Read())
                {
                    try
                    {
                        ar.Add(new SDteamDetails(dr["UserID"].ToString(), dr["Email"].ToString(), dr["TeamName"].ToString()));
                        //ht.Add(dr["Email"].ToString(), dr["TeamName"].ToString());
                    }
                    catch
                    {
                        //avoid reted email id's
                    }
                }
                dr.Close();
                dr.Dispose();

                if (ar.Count > 0)
                {
                    //IDictionaryEnumerator en = ht.GetEnumerator();
                    //while (en.MoveNext())
                    //{
                    var result = (from p in obj.Incidents
                                  where p.ID == sessionKeys.IncidentID && p.PortfolioID == sessionKeys.PortfolioID
                                  select p).ToList();

                    foreach (SDteamDetails ar_sddetails in ar)
                    {

                        foreach (var i in result)
                        {
                       
                            string[] _sddetails = ar_sddetails.ToString().Split(',');

                            SDCustomerApproveMail1.BindControls(_sddetails[0],i.Subject,i.Details,i.POnumber);

                            StringWriter stringWrite = new StringWriter();
                            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                            SDCustomerApproveMail1.RenderControl(htmlWrite);
                            Email ToEmail = new Email();
                            ToEmail.SendingMail(_sddetails[2], "Order Accepted SR:"+sessionKeys.IncidentID+ " " + i.Subject, htmlWrite.InnerWriter.ToString());
                        }
                        
                    }
                    // }
                }
                SDCustomerApproveMail1.Dispose();
            }

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        finally { SDCustomerApproveMail1.Visible = false; }

    }

    public class SDteamDetails
    {
        string TeamName = "";
        string UserID = "";
        string Email = "";
        public SDteamDetails(string uname, string email, string tname)
        {
            TeamName = tname;
            UserID = uname;
            Email = email;
        }

        public override string ToString()
        {
            return
              String.Format("{0},{1},{2}",
              TeamName, UserID, Email);
        }


    }

    private void SendFLSDistributionMail()
    {
        try
        {
            SDCustomerApproveMail1.Visible = true;
            string Company_Name = string.Empty;
            List<int> idlist = SecurityAccessMail.GetIds(6,sessionKeys.PortfolioID); // 6 for FLS
            if (idlist.Count > 0)
            {
                
                FLSDetail flsDetail= FLSDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                CallDetail cd = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                //get the company name
                Company_Name = GetCompany_Name(Company_Name, cd);
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                SDCustomerApproveMail1.BindControls("ALL","",flsDetail.Details,"");
                List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                SDCustomerApproveMail1.RenderControl(htmlWrite);
                Email ToEmail = null;
                // mail to customer 
                //ToEmail = new Email();
                //ToEmail.SendingMail(txtReqEmailAddress.Text, "Order", htmlWrite.InnerWriter.ToString());
                //send mail to Distribution list
                if (strList.Count > 0)
                {
                    foreach (string s in strList)
                    {
                        ToEmail = new Email();
                        ToEmail.SendingMail(s, Company_Name +" - " + "Order Accepted TN:" + sessionKeys.IncidentID, htmlWrite.InnerWriter.ToString());
                    }
                }

            }
            SDCustomerApproveMail1.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private static string GetCompany_Name(string Company_Name, CallDetail cd)
    {
        Company_Name = string.Empty;
        try
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pm = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                Company_Name = pm.ProjectPortfolios.Where(p => p.ID == cd.CompanyID).FirstOrDefault().PortFolio;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Company_Name;
    }

    private void SendDeclineMail() //Fls Decline mail
    {
        try
        {
            SDCustomerDeclineMail1.Visible = true;
           
            List<int> idlist = SecurityAccessMail.GetIds(6,sessionKeys.PortfolioID); // 6 for FLS
            if (idlist.Count > 0)
            {
               
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                SDCustomerDeclineMail1.BindControls("ALL","");
                List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                SDCustomerDeclineMail1.RenderControl(htmlWrite);
                Email ToEmail = null;
                // mail to customer 
                //ToEmail = new Email();
                //ToEmail.SendingMail(txtReqEmailAddress.Text, "Order", htmlWrite.InnerWriter.ToString());
                //send mail to Distribution list
                if (strList.Count > 0)
                {
                    foreach (string s in strList)
                    {
                        ToEmail = new Email();
                        ToEmail.SendingMail(s, "Order Declined TN:" + sessionKeys.IncidentID, htmlWrite.InnerWriter.ToString());
                    }
                }

            }
            SDCustomerDeclineMail1.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SDCustomerApproveMail1.Type = Type;

            if (Type == "FLS")
            {
                SendFLSDistributionMail();
            }
            using (IncidentDataContext su = new IncidentDataContext())
            {
                BuildMail();
                int id = sessionKeys.IncidentID;
                IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
                inc.Status = "In Hand";
                inc.InHandTime = DateTime.Now;
                su.SubmitChanges();
                lblcstatus.Text = "Order Approved Successfully.";
                Getstatus(sessionKeys.IncidentID);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnDeclain_Click(object sender, EventArgs e)
    {
        try
        {
            SDCustomerDeclineMail1.Type = Type;
            string Company_Name = string.Empty;
            if (Type == "FLS")
            {
                SendDeclineMail();
                CallDetail cd = DC.BLL.CallDetailsBAL.SelectbyId(Convert.ToInt32(Request.QueryString["callid"].ToString()));
               
                Company_Name = GetCompany_Name(Company_Name, cd);
                if (cd != null)
                {
                    //33	Cancelled
                    cd.StatusID = 33;
                    DC.BLL.CallDetailsBAL.CallDetailsUpdate(cd);
                    lblcstatus.ForeColor = System.Drawing.Color.Green;
                    lblcstatus.Text = "Order Declined Successfully.";
                    //update journal
                    AddCallDetailsJournal(cd, DateTime.Now);
                }
            }
            else
            {

                using (IncidentDataContext su = new IncidentDataContext())
                {
                    BuildDeclineMail();
                    int id = sessionKeys.IncidentID;
                    IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
                    inc.Status = "Declined";
                    su.SubmitChanges();
                    lblcstatus.Text = "Order Declined Successfully";
                    Getstatus(sessionKeys.IncidentID);
                }
            }
            pnlCustomer.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void btnProcessorder_Click(object sender, EventArgs e)
    {
        try
        {
            SDCustomerApproveMail1.Type = Type;
            if (chek_confirm.Checked)
            {
                if (!string.IsNullOrEmpty(txtPONumber.Text.Trim()))
                {

                    if (Type == "FLS")
                    {

                        if (Request.QueryString["callid"] != null)
                        {

                            CallDetail cd = DC.BLL.CallDetailsBAL.SelectbyId(Convert.ToInt32(Request.QueryString["callid"].ToString()));
                            FLSDetail fd = DC.BLL.FLSDetailsBAL.BindFLSDetails().Where(p => p.CallID == Convert.ToInt32(Request.QueryString["callid"].ToString())).FirstOrDefault();

                            if (cd != null)
                            {
                                //39	Order Received
                                cd.StatusID = 39;
                                DC.BLL.CallDetailsBAL.CallDetailsUpdate(cd);
                                fd.POnumber = txtPONumber.Text.Trim();
                                DC.BLL.FLSDetailsBAL.FLSDetailsUpdate(fd);
                                lblcstatus.ForeColor = System.Drawing.Color.Green;
                                lblcstatus.Text = "Thank you. Your order will now be processed.";
                                //update journal
                                DateTime modified_date = DateTime.Now;
                                AddCallDetailsJournal(cd, modified_date);
                                AddFlsDetailsJournal(fd, modified_date);
                                //send order approve mail to team
                                SendFLSDistributionMail();
                            }

                        }
                    }
                    else
                    {
                        using (IncidentDataContext su = new IncidentDataContext())
                        {
                            BuildMail();
                            int id = sessionKeys.IncidentID;
                            IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
                            inc.Status = "In Hand";
                            inc.InHandTime = DateTime.Now;
                            su.SubmitChanges();
                            lblcstatus.ForeColor = System.Drawing.Color.Green;
                            lblcstatus.Text = "Order Approved Successfully.";
                            Getstatus(sessionKeys.IncidentID);

                        }
                    }
                    pnlCustomer.Visible = false;
                }
                else
                {

                    lblcstatus.Text = "Please attach PO before accepting this order.";
                    lblcstatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblcstatus.Text = "Please select check box.";
                lblcstatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void AddCallDetailsJournal(CallDetail cd,DateTime modified_date)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cd.ID;
            cdj.CompanyID = cd.CompanyID;
            cdj.LoggedBy = cd.LoggedBy;
            cdj.LoggedDate = cd.LoggedDate;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = modified_date;
            cdj.RequesterID = cd.RequesterID;
            cdj.RequestTypeID = cd.RequestTypeID;
            cdj.SiteID = cd.SiteID;
            cdj.StatusID = cd.StatusID;
            cdj.VisibleToCustomer = true;
            ws.AddCallDetailsJournal(cdj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void AddFlsDetailsJournal(FLSDetail cd, DateTime modified_date)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            FLSDetailsJournal cdj = new FLSDetailsJournal();
            cdj.CallID = cd.ID;
            cdj.DepartmentID = cd.DepartmentID;
            cdj.Details = cd.Details;
            cdj.Notes = cd.Notes;
            cdj.POnumber = cd.POnumber;
            cdj.RAGStatus = cd.RAGStatus;
            cdj.Resolution = cd.Resolution;
            cdj.ScheduledDate = cd.ScheduledDate;
            cdj.SubjectID = cd.SubjectID;
            cdj.TimeAccumulated = cd.TimeAccumulated;
            cdj.TimeWorked = cd.TimeWorked;
            cdj.UserID = cd.UserID;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = modified_date;
            cdj.VisibleToCustomer = true;
            ws.AddFLSDetailsJournal(cdj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SendRequestQuoteMailToDistribution()
    {
        try
        {
            
            CustomerOrderToSDteam1.Visible = true;
            List<int> idlist = SecurityAccessMail.GetIds(6,sessionKeys.PortfolioID); // 6 for FLS
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                CustomerOrderToSDteam1.BindControls("ALL");
                List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                CustomerOrderToSDteam1.RenderControl(htmlWrite);
                Email ToEmail = null;
                // mail to customer 
                //ToEmail = new Email();
                //ToEmail.SendingMail(txtReqEmailAddress.Text, "Order", htmlWrite.InnerWriter.ToString());
                //send mail to Distribution list
                if (strList.Count > 0)
                {
                    foreach (string s in strList)
                    {
                        ToEmail = new Email();
                        ToEmail.SendingMail(s, "Order", htmlWrite.InnerWriter.ToString());
                    }
                }

            }
            CustomerOrderToSDteam1.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    

    protected void btnRequestQuote_Click(object sender, EventArgs e)
    {
        if (Type == "FLS")
        {

            if (Request.QueryString["callid"] != null)
            {
                SendRequestQuoteMailToDistribution();
                CallDetail cd = DC.BLL.CallDetailsBAL.SelectbyId(Convert.ToInt32(Request.QueryString["callid"].ToString()));

                if (cd != null)
                {
                    //38	Quote Requsted
                    cd.StatusID = 38;
                    DC.BLL.CallDetailsBAL.CallDetailsUpdate(cd);
                    lblcstatus.ForeColor = System.Drawing.Color.Green;
                    lblcstatus.Text = "Your request has been submitted successfully.";
                    //update journal
                    AddCallDetailsJournal(cd, DateTime.Now);
                }

            }
        }
        pnlCustomer.Visible = false;
    }
    
}