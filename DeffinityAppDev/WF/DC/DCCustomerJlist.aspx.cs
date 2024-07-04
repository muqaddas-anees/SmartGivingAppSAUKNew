using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DC.BLL;
using DC.Entity;
using DC.DAL;
public partial class DCCustomerJlist1 : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        {
            //this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";
            
        }
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["type"] == "AccessControl")
            {
                pnlAccessNo.Visible = true;
                lblPageHead.Text = Resources.DeffinityRes.AccessControl;
            }
           
            if (Request.QueryString["type"] == "FLS")
            {
                lblPageHead.Text = Resources.DeffinityRes.ServiceDesk;
                pnlFLStype.Visible = true;
               tdRTLable.Style.Add("display", "none");
               tdRTField.Style.Add("display", "none");
               div_RT.Style.Add("display", "none");
               // link_Dashboard.Visible = true;
            }
            if (Request.QueryString["type"].ToLower().Contains("permit"))
            {
                lblPageHead.Text = Resources.DeffinityRes.PermittoWork;
            }

            if (Request.QueryString["type"].ToLower().Contains("delivery"))
            {
                lblPageHead.Text = Resources.DeffinityRes.Delivery;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    [WebMethod(EnableSession = true)]
    public static object Get(string ticketno, string type, string status, string accessno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            List<FLSList> flsList = FLSDetailsBAL.BindFLSList(sessionKeys.UID);
           
                

                flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
                //if (sessionKeys.UID>0)
                //{
                //    flsList = flsList.Where(f => f.LoggedBy == sessionKeys.UID).ToList();
                //}
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                    flsList = flsList.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                }

                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }
                if (accessno != string.Empty)
                {
                    var accessticketNo = AccessNumberBAL.TicketNoByAccessNo(accessno);
                    flsList = flsList.Where(f => f.CallID.ToString() == accessticketNo.ToString()).ToList();
                }

                if (type != "ALL")
                {
                    flsList = flsList.Where(f => f.RequestType.Replace(" ", "").ToLower() == type.Replace(" ", "").ToLower()).ToList();
                }
                if (status != "ALL ACTIVE")
                {
                    flsList = flsList.Where(f => f.Status == status).ToList();
                }
                if (jtSorting.Equals("Company ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Name ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Site ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("Site DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
                }
                else if (jtSorting.Equals("RequestType DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("Status ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("Status DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Status).ToList();
                }
                else
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
           

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetOrder(string ticketno, string type, string status, string accessno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            if (type == "FLS")
            {

                List<FLSList> flsList = FLSDetailsBAL.BindFLSList(sessionKeys.UID);
                flsList = flsList.Where(f => f.Status == "Quote Requested" || f.Status == "Order Received" || f.Status == "Quotation Submitted" || f.Status == "Cancelled").ToList();
                //if (sessionKeys.UID>0)
                //{
                //    flsList = flsList.Where(f => f.LoggedBy == sessionKeys.UID).ToList();
                //}
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                    flsList = flsList.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                }

                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }
                if (accessno != string.Empty)
                {
                    var accessticketNo = AccessNumberBAL.TicketNoByAccessNo(accessno);
                    flsList = flsList.Where(f => f.CallID.ToString() == accessticketNo.ToString()).ToList();
                }

                if (type != "ALL")
                {
                    flsList = flsList.Where(f => f.RequestType == type).ToList();
                }
                if (status != "ALL ACTIVE")
                {
                    flsList = flsList.Where(f => f.Status == status).ToList();
                }
                if (jtSorting.Equals("Company ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Name ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Site ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("Site DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
                }
                else if (jtSorting.Equals("RequestType DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("Status ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("Status DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Status).ToList();
                }
                else
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
            }
            else
            {
                return new { Result = "OK", Records = "", TotalRecordCount = 0 };
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetService(string ticketno, string type, string status, string accessno, string requestType, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {

            //List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList(sessionKeys.UID);
            
            if (HttpContext.Current.Request.QueryString["type"].ToLower().ToString() != "fls")
            {
                List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList(sessionKeys.UID);

                //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
                //if (sessionKeys.UID>0)
                //{
                //    flsList = flsList.Where(f => f.LoggedBy == sessionKeys.UID).ToList();
                //}
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                    flsList = flsList.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                }

                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }
                if (accessno != string.Empty)
                {
                    var accessticketNo = AccessNumberBAL.TicketNoByAccessNo(accessno);
                    flsList = flsList.Where(f => f.CallID.ToString() == accessticketNo.ToString()).ToList();
                }

                if (type != "ALL")
                {
                    flsList = flsList.Where(f => f.RequestType == type).ToList();
                }
                if (status != "ALL ACTIVE")
                {
                    flsList = flsList.Where(f => f.Status == status).ToList();
                }
                if (jtSorting.Equals("Company ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Name ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Name).ToList();
                }
                else if (jtSorting.Equals("Site ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("Site DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
                }
                else if (jtSorting.Equals("RequestType DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("Status ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("Status DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Status).ToList();
                }
                else
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                if (requestType.ToLower() != "all" && requestType != "[Loading...]")
                    flsList = flsList.Where(f => f.RT.ToLower() == requestType.ToLower()).ToList();
                var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
            }
            else
            {
                var flsList = FLSDetailsBAL.JqgridlistByRequester(sessionKeys.UID);
                //var flsdetails = FLSDetailsBAL.BindFLSDetails();
                List<TypeOfRequest> typeofRequests = TypeOfRequestBAL.GetTypeOfRequestList().ToList();
                var flsList_new = (from p in flsList
                                   select new
                                   {
                                       p.CallID,
                                       p.Company,
                                       p.InHandSLA,
                                       p.LoggedBy,
                                       p.RequesterName,
                                       AssignedTechnician = p.AssignedTechnician,
                                       p.ResolutionSLA,
                                       p.Site,
                                       p.Status,
                                       p.TypeofRequest,
                                       p.LoggedDateTime,
                                       SLoggedDateTime = Convert.ToDateTime(p.LoggedDateTime),
                                       p.Details,
                                       //RequestType = typeofRequests.Where(t => t.ID == (flsdetails.Where(o => o.CallID == p.CallID).Select(o => o.RequestType).FirstOrDefault())).Select(t => t.Name).FirstOrDefault(),
                                       typeofrequest = "fls",
                                       
                                   }).ToList();

                //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
                //if (sessionKeys.UID>0)
                //{
                //    flsList = flsList.Where(f => f.LoggedBy == sessionKeys.UID).ToList();
                //}
                //if (status == "ALL ACTIVE" && ticketno == string.Empty)
                //{
                //    flsList_new = flsList_new.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                //}

                if (ticketno != string.Empty)
                {
                    flsList_new = flsList_new.Where(f => f.CallID.ToString() == ticketno).ToList();
                }
                if (accessno != string.Empty)
                {
                    var accessticketNo = AccessNumberBAL.TicketNoByAccessNo(accessno);
                    flsList_new = flsList_new.Where(f => f.CallID.ToString() == accessticketNo.ToString()).ToList();
                }

                if (type != "ALL")
                {
                    flsList_new = flsList_new.Where(f => f.typeofrequest == type).ToList();
                }
                if (status != "ALL ACTIVE")
                {
                    flsList_new = flsList_new.Where(f => f.Status == status).ToList();
                }
                if (jtSorting.Equals("Company ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Name ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.Details).ToList();
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.Details).ToList();
                }
                else if (jtSorting.Equals("Site ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("Site DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Site).ToList();
                }
                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
                }
                else if (jtSorting.Equals("LoggedDateTime ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.SLoggedDateTime).ToList();
                }
                else if (jtSorting.Equals("LoggedDateTime DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.SLoggedDateTime).ToList();
                }
                else if (jtSorting.Equals("Details ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.Details).ToList();
                }
                else if (jtSorting.Equals("Details DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.Details).ToList();
                }
                else if (jtSorting.Equals("AssignedTechnician ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.AssignedTechnician).ToList();
                }
                else if (jtSorting.Equals("AssignedTechnician DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.AssignedTechnician).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("Status ASC"))
                {
                    flsList_new = flsList_new.OrderBy(o => o.Status).ToList();
                }
                else if (jtSorting.Equals("Status DESC"))
                {
                    flsList_new = flsList_new.OrderByDescending(o => o.Status).ToList();
                }
                else
                {
                    flsList_new = flsList_new.OrderBy(o => o.CallID).ToList();
                }
                if (requestType.ToLower() != "all" && requestType != "[Loading...]")
                    flsList_new = flsList_new.ToList();
                var result = flsList_new.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList_new.Count() };

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetServiceHome(string ticketno, string requestType, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {

            List<FLSJList> flsList =  FLSDetailsBAL.BindFLSJList(sessionKeys.UID);
           
            flsList = flsList.Where(f => f.Status == "New" && f.RequestType == "FLS").ToList();
            //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
            //if (sessionKeys.UID>0)
            //{
            //    flsList = flsList.Where(f => f.LoggedBy == sessionKeys.UID).ToList();
            //}
          
            if (ticketno != string.Empty)
            {
                flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
            }
           

            //if (type != "ALL")
            //{
            //    flsList = flsList.Where(f => f.RequestType == 6).ToList();
            //}
           
            if (jtSorting.Equals("Company ASC"))
            {
                flsList = flsList.OrderBy(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("Company DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.Company).ToList();
            }
           
            else if (jtSorting.Equals("CallID ASC"))
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("CallID DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.CallID).ToList();
            }
            else
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            flsList = flsList.Where(f => f.RT.ToLower() == requestType).ToList();
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            string type = ddlTypeofRequest.SelectedItem.Text;
            if (type != null)
            {
                Response.Redirect("DCCustomerNavigation.ashx?type=" + type);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    [WebMethod(EnableSession = true)]
    public static object GetHome(string ticketno="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting=null)
    {
        try
        {
           using (DCDataContext dd=new DCDataContext())
           {
                             
            List<FLSList> flsList= FLSDetailsBAL.BindFLSList();
            if (sessionKeys.PortfolioID != 0)
            {
                    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            }
            flsList = flsList.Where(f => f.Status == "New").ToList();
             
            if (ticketno != string.Empty)
            {
                flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
            }
           
            if (jtSorting.Equals("Company ASC"))
            {
                flsList = flsList.OrderBy(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("Company DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.Company).ToList();
            }
           
            else if (jtSorting.Equals("RequestType ASC"))
            {
                flsList = flsList.OrderBy(o => o.RequestType).ToList(); 
            }
            else if (jtSorting.Equals("RequestType DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
            }
            else if (jtSorting.Equals("CallID ASC"))
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("CallID DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.CallID).ToList();
            }
           
            else
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
           }
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    [WebMethod(EnableSession = true)]
    public static object GetHomeOrder(string ticketno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            using (DCDataContext dd = new DCDataContext())
            {

                List<FLSList> flsList = FLSDetailsBAL.BindFLSList();
                if (sessionKeys.PortfolioID != 0)
                {
                    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
                }
                flsList = flsList.Where(f => f.Status == "Quote Requested" || f.Status == "Order Received" || f.Status == "Quotation Submitted").ToList();

                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }

                if (jtSorting.Equals("Company ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Company).ToList();
                }

                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList = flsList.OrderBy(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("RequestType DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).ToList();
                }

                else
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
            }
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    [WebMethod(EnableSession = true)]
    public static object GetCustomerHome(string ticketno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
           using (DCDataContext dd=new DCDataContext())
           {
            List<FLSList> flsList = FLSDetailsBAL.BindFLSList(sessionKeys.UID);
            flsList = flsList.Where(f => f.Status == "New").ToList();
            if (ticketno != string.Empty)
            {
                flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
            }
           
            if (jtSorting.Equals("Company ASC"))
            {
                flsList = flsList.OrderBy(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("Company DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.Company).ToList();
            }
           
            else if (jtSorting.Equals("RequestType ASC"))
            {
                flsList = flsList.OrderBy(o => o.RequestType).ToList(); 
            }
            else if (jtSorting.Equals("RequestType DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
            }
            else if (jtSorting.Equals("CallID ASC"))
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("CallID DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.CallID).ToList();
            }
           
            else
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
           }
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    [WebMethod(EnableSession = true)]
    public static object GetCustomerHomeOrder(string ticketno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            using (DCDataContext dd = new DCDataContext())
            {
                List<FLSList> flsList = FLSDetailsBAL.BindFLSList(sessionKeys.UID);
                flsList = flsList.Where(f => f.Status == "Quote Requested" || f.Status == "Order Received" || f.Status == "Quotation Submitted").ToList();
                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }

                if (jtSorting.Equals("Company ASC"))
                {
                    flsList = flsList.OrderBy(o => o.Company).ToList();
                }
                else if (jtSorting.Equals("Company DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.Company).ToList();
                }

                else if (jtSorting.Equals("RequestType ASC"))
                {
                    flsList = flsList.OrderBy(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("RequestType DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.RequestType).ToList();
                }
                else if (jtSorting.Equals("CallID ASC"))
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                else if (jtSorting.Equals("CallID DESC"))
                {
                    flsList = flsList.OrderByDescending(o => o.CallID).ToList();
                }

                else
                {
                    flsList = flsList.OrderBy(o => o.CallID).ToList();
                }
                var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
            }
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
   
   
}