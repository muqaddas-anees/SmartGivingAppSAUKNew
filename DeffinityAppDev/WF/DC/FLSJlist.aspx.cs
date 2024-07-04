using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DC.BLL;
using DC.Entity;
using DC.DAL;

public partial class FLSJlist1 : System.Web.UI.Page
{
    DisplayColumnBAL dcBAL = new DisplayColumnBAL();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //if (sessionKeys.SID == 9)
        //{
        //    this.Page.MasterPageFile = "~/DeffinityResourceTab.master";
        //}
        //else
        //    Response.Redirect("~/Default.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

           // BindDashboardValues();
            if (Request.QueryString["type"] == "FLS")
            {
                lit_pagetitle.Text = Resources.DeffinityRes.ServiceDesk;
               // lit_paneltitle.Text = Resources.DeffinityRes.ServiceDesk;
            }

            if (Request.QueryString["type"] == "Delivery")
            {
                lit_pagetitle.Text = Resources.DeffinityRes.Delivery;
                //lit_paneltitle.Text = Resources.DeffinityRes.Delivery;
               
            }

            if (Request.QueryString["type"] == "AccessControl")
            {
                lit_pagetitle.Text = Resources.DeffinityRes.AccessControl;
                //lit_paneltitle.Text = Resources.DeffinityRes.AccessControl;
            }

            if (Request.QueryString["type"].ToLower() == "permittowork")
            {
                lit_pagetitle.Text = Resources.DeffinityRes.PermittoWork;
                //lit_paneltitle.Text = Resources.DeffinityRes.PermittoWork;
               
            }

            if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
            {
              
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //#region Dashboard display
    //private void BindDashboardValues()
    //{
    //    try
    //    {
    //       using(DC.DAL.DCDataContext DC = new DCDataContext())
    //        {
    //            //New Status
    //            //22	New
    //            //35	Closed
    //            var c = DC.ServiceDesk_DashboardDisplay(sessionKeys.PortfolioID).FirstOrDefault();
                
    //            if(c != null)
    //            {

    //                lblNewJobs.InnerText = c.NewCount.ToString();
    //                lblCallback.InnerText = string.Format("{0:N2}", c.MaintenancePlansThisMonth);
    //                lblQuoteAccepted.InnerText = string.Format("{0:N2}",c.quoteValueOfJobs);
    //                lblCompletedJobsAmount.InnerText = string.Format("{0:N2}", c.revenueClosedJobs);
    //                lblThisMonthCompletedJobsAmount.InnerText = string.Format("{0:N2}", c.revenuedueClosedJobs);
    //                lblThisMonthDueAmount.InnerText = string.Format("{0:N2}", c.revenuedueMaintenancePlan);
    //             }


    //        }

    //    }
    //    catch(Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}


    //#endregion

    [WebMethod(EnableSession = true)]
    public static object Get1(string ticketno, string type, string status, string accessno, string company = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList1();
            if (type.ToLower().Contains("access"))
            {
                flsList = flsList.Where(o => o.RequestType.ToLower().Contains("access")).ToList();
                using (DCDataContext dc = new DCDataContext())
                {
                    var dList = dc.AccessControls.Select(o => o).ToList();
                    flsList = (from p in flsList
                               select new FLSJList
                               {
                                   CallID = p.CallID,
                                   Company = p.Company,
                                   DateLogged = p.DateLogged,
                                   Description = p.Description,
                                   InHandSLA = p.InHandSLA,
                                   LoggedBy = p.LoggedBy,
                                   Name = p.Name,
                                   Note = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes != null ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes : "",
                                   PurposeofVisit = p.PurposeofVisit,
                                   RequestType = p.RequestType,
                                   ResolutionSLA = p.ResolutionSLA,
                                   RT = p.RT,
                                   ScheduleDate = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().RequestedDate.HasValue ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().RequestedDate.Value.ToString().Substring(0, 10) : "",
                                   Site = p.Site,
                                   Status = p.Status
                               }).ToList();
                }
            }
            //if (sessionKeys.PortfolioID != 0)
            //{
            //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            //}
            if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
            {
                flsList = flsList.Where(p => p.LoggedBy == sessionKeys.UID).ToList();
            }
            if (!string.IsNullOrEmpty(company))
            {
                var d_array = new string[] { "[Loading customer...]", "Please select..." };
                if (!d_array.Contains(company))
                {
                    flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                }
            }
            if (status == "ALL ACTIVE" && ticketno == string.Empty)
            {
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
                }
                else
                flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved").ToList();
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
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
                }
                else
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
            else if (jtSorting.Equals("PurposeofVisit ASC"))
            {
                flsList = flsList.OrderBy(o => o.PurposeofVisit).ToList();
            }
            else if (jtSorting.Equals("PurposeofVisit DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.PurposeofVisit).ToList();
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
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    [WebMethod(EnableSession = true)]
    public static object Get(string ticketno, string type, string status, string accessno, string company = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            if (type == "FLS")
            {

                List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList();
                flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
                //if (sessionKeys.PortfolioID != 0)
                //{
                //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
                //}
                //check the Service desk user
                if (sessionKeys.SID == 9  || sessionKeys.SID==4 )
                {
                    flsList = flsList.Where(p => p.LoggedBy == sessionKeys.UID).ToList();
                }
                if (!string.IsNullOrEmpty(company))
                {
                    var d_array = new string[] { "[Loading customer...]", "Please select..." };
                    if (!d_array.Contains(company))
                    {
                        flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                    }
                }
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                   
                    flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved").ToList();
                }
                else  if (status == "All Tickets")
                {
                     flsList = flsList.ToList();
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
                    if (status == "All Tickets")
                    {
                        flsList = flsList.ToList();
                    }
                    else
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
                    flsList = flsList.OrderBy(o => o.RequestType).ToList();
                    //flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
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
                List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList();
                if (type.ToLower() == "delivery")
                {
                    flsList = flsList.Where(o => o.RequestType.ToLower() == "delivery").ToList();
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var dList = dc.DeliveryInformations.Select(o => o).ToList();
                        flsList = (from p in flsList
                                   select new FLSJList
                                   {
                                       CallID = p.CallID,
                                       Company = p.Company,
                                       DateLogged = p.DateLogged,
                                       Description = p.Description,
                                       InHandSLA = p.InHandSLA,
                                       LoggedBy = p.LoggedBy,
                                       Name = p.Name,
                                       Note = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes != null ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes : "",
                                       PurposeofVisit = p.PurposeofVisit,
                                       RequestType = p.RequestType,
                                       ResolutionSLA = p.ResolutionSLA,
                                       RT = p.RT,
                                       ScheduleDate = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().ArrivalDate.HasValue ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().ArrivalDate.Value.ToString().Substring(0, 10) : "",
                                       Site = p.Site,
                                       Status = p.Status
                                   }).ToList();
                    }
                }
                else
                    if (type.ToLower().Contains("permit"))
                    {
                        flsList = flsList.Where(o => o.RequestType.ToLower().Contains("permit")).ToList();
                        using (DCDataContext dc = new DCDataContext())
                        {
                            var dList = dc.PermitToWorks.Select(o => o).ToList();
                            flsList = (from p in flsList
                                       select new FLSJList
                                       {
                                           CallID = p.CallID,
                                           Company = p.Company,
                                           DateLogged = p.DateLogged,
                                           Description = p.Description,
                                           InHandSLA = p.InHandSLA,
                                           LoggedBy = p.LoggedBy,
                                           Name = p.Name,
                                           Note = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes != null ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().Notes : "",
                                           PurposeofVisit = p.PurposeofVisit,
                                           RequestType = p.RequestType,
                                           ResolutionSLA = p.ResolutionSLA,
                                           RT = p.RT,
                                           ScheduleDate = dList.Where(o => o.CallID == p.CallID).FirstOrDefault().ArrivalDate.HasValue ? dList.Where(o => o.CallID == p.CallID).FirstOrDefault().ArrivalDate.Value.ToString().Substring(0, 10) : "",
                                           Site = p.Site,
                                           Status = p.Status
                                       }).ToList();
                        }
                    }
                //if (sessionKeys.PortfolioID != 0)
                //{
                //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
                //}
                if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
                {
                    flsList = flsList.Where(p => p.LoggedBy == sessionKeys.UID).ToList();
                }
                if (!string.IsNullOrEmpty(company))
                {
                    var d_array = new string[] { "[Loading customer...]", "Please select..." };
                    if (!d_array.Contains(company))
                    {
                        flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                    }
                }
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                    if (status == "All Tickets")
                    {
                        flsList = flsList.ToList();
                    }
                    else
                    flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved").ToList();
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
                    if (status == "All Tickets")
                    {
                        flsList = flsList.ToList();
                    }
                    else
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
                    flsList = flsList.OrderBy(o => o.RequestType).ToList();
                    //flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
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



        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetOrders(string ticketno, string type, string status, string accessno, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            if (type == "FLS")
            {
                List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList();
                flsList = flsList.Where(f => f.Status == "Quote Requested" || f.Status == "Order Received" || f.Status == "Quotation Submitted" || f.Status == "Cancelled").ToList();
                //if (sessionKeys.PortfolioID != 0)
                //{
                //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
                //}
                if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
                {
                    flsList = flsList.Where(p => p.LoggedBy == sessionKeys.UID).ToList();
                }
                if (status == "ALL ACTIVE" && ticketno == string.Empty)
                {
                    if (status == "All Tickets")
                    {
                        flsList = flsList.ToList();
                    }
                    else
                    flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved").ToList();
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
                    if (status == "All Tickets")
                    {
                        flsList = flsList.ToList();
                    }
                    else
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
                    flsList = flsList.OrderBy(o => o.RequestType).ToList();
                    //flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
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
            return new { Result = "ERROR", Message = ex.Message };
        }
    }


    [WebMethod(EnableSession = true)]
    public static object GetSD(string ticketno, string type, string status, string accessno, string requestType = "", string company = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {


            List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList();
            //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
            //if (sessionKeys.PortfolioID != 0)
            //{
            //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            //}
            //check the Service desk user
            if (!string.IsNullOrEmpty(company))
            {
                var d_array = new string[] { "[Loading customer...]", "Please select..." };
                if (!d_array.Contains(company))
                {
                    flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                }
            }
            if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
            {
                flsList = flsList.Where(p => p.LoggedBy == sessionKeys.UID).ToList();
            }
            if (status == "ALL ACTIVE" && ticketno == string.Empty)
            {
                flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved").ToList();
            }
            else if (status == "All Tickets")
            {
                flsList = flsList.ToList();
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
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
                }
                else
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
                flsList = flsList.OrderBy(o => o.RequestType).ToList();
                //flsList = flsList.OrderByDescending(o => o.CallID).OrderByDescending(o => o.Status == "New" || o.Status == "Awaiting Approval" || o.Status == "Expected" || o.Status == "Pending Approval").ToList();
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
            if (!string.IsNullOrEmpty(requestType))
            {
                var array = new string[] { "[Loading...]", "Please select..." };
                if (!array.Contains(requestType))
                    flsList = flsList.Where(f => f.RT.ToLower() == requestType.ToLower()).ToList();
            }
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };




        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    [WebMethod(EnableSession = true)]
    public static object GetSDList(string ticketno, string type, string status, string accessno, string requestType = "", string company = "", string url = "", string txtsearch="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {

        try
        {
            //var dd1 = DC.BLL.FLSDetailsBAL.Jqgridlist(1060);
            List<Jqgrid> flsList = new List<Jqgrid>();
            flsList = DC.BLL.FLSDetailsBAL.Jqgridlist();
            if (url.ToLower().Contains("flsresourcelist.aspx"))
            {
                flsList = flsList.Where(f => f.LoggedByID == sessionKeys.UID || f.AssignedTechnicianID == sessionKeys.UID).ToList();

                //flsList = flsList.Where(f => f.AssignedTechnicianID == sessionKeys.UID).ToList();
            }
            //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
            //if (sessionKeys.PortfolioID != 0)
            //{
            //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            //}
            //check the Service desk user
            if (!string.IsNullOrEmpty(company))
            {
                var d_array = new string[] { "[Loading customer...]", "Please select..." };
                if (!d_array.Contains(company))
                {
                    flsList = flsList.Where(o => (o.Company != null &&  o.Company.ToLower() == company.ToLower())).ToList();
                }
            }

            if (status == "ALL ACTIVE" && ticketno == string.Empty)
            {
                flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved" && f.Status != "Cancelled" && f.Status != "").ToList();
                //flsList = flsList.Where(f =>  f.Status != "Resolved" && f.Status != "Cancelled" && f.Status != "").ToList();
            }
            else if (status == "All Tickets")
            {
                flsList = flsList.ToList();
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


            if (status != "ALL ACTIVE")
            {
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
                }
                else
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
                // flsList = flsList.OrderBy(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("Name DESC"))
            {
                //flsList = flsList.OrderByDescending(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("Site ASC"))
            {
                flsList = flsList.OrderBy(o => o.Site).ToList();
            }
            else if (jtSorting.Equals("Site DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.Site).ToList();
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
            else if (jtSorting.Equals("LoggedDateTime ASC"))
            {
                flsList = flsList.OrderBy(o => Convert.ToDateTime(o.LoggedDateTime)).ToList();
            }
            else if (jtSorting.Equals("LoggedDateTime DESC"))
            {
                flsList = flsList.OrderByDescending(o => Convert.ToDateTime(o.LoggedDateTime)).ToList();
            }
            else if (jtSorting.Equals("RequestersAddress ASC"))
            {
                flsList = flsList.OrderBy(o => o.RequestersAddress).ToList();
            }
            else if (jtSorting.Equals("RequestersAddress DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.RequestersAddress).ToList();
            }
            else
            {
                flsList = flsList.OrderBy(o => o.CallID).ToList();
            }
            if (!string.IsNullOrEmpty(requestType))
            {
                var array = new string[] { "[Loading...]", "Please select..." };
                if (!array.Contains(requestType))
                    flsList = flsList.Where(f => f.TypeofRequest == requestType).ToList();
            }

            if(!string.IsNullOrEmpty(txtsearch) )
            {
                txtsearch = txtsearch.ToLower();
                flsList = (from x in flsList
                          where( 
                          //x.AssignedTechnician.ToString().Contains(txtsearch) ||
                          //x.AssignedTechnicianContact.ToString().Contains(txtsearch) ||
                          //x.AssignedTechnicianEmail.ToString().Contains(txtsearch) ||
                          //x.AssignedtoDepartment.ToString().Contains(txtsearch) ||
                          (x.CallID != null && x.CallID.ToString().ToLower().Contains(txtsearch)) ||
                          (x.Category != null && x.Category.ToLower().ToString().Contains(txtsearch)) ||
                          //x.Company != null ?x.Company.ToString().Contains(txtsearch):false ||
                          //x.CustomerRef != null ?x.CustomerRef.ToString().Contains(txtsearch):false ||
                          (x.Details != null && x.Details.ToLower().ToString().Contains(txtsearch)) ||
                          (x.Notes != null&& x.Notes.ToLower().ToString().Contains(txtsearch)) ||
                          //x.PONumber != null?x.PONumber.ToString().Contains(txtsearch):false ||
                          (x.Priority != null && x.Priority.ToLower().ToString().Contains(txtsearch)) ||
                          (x.RequesterName!= null && x.RequesterName.ToLower().ToString().Contains(txtsearch)) ||
                          (x.RequestersAddress!= null && x.RequestersAddress.ToLower().ToString().Contains(txtsearch)) ||
                          //x.RequestersCity!= null?x.RequestersCity.ToString().Contains(txtsearch):false ||
                          //x.RequestersDepartment != null? x.RequestersDepartment.ToString().Contains(txtsearch):false ||
                          (x.RequestersEmailAddress!= null && x.RequestersEmailAddress.ToLower().ToString().Contains(txtsearch)) ||
                          //x.RequestersJobTitle.ToString().Contains(txtsearch) ||
                          (x.RequestersPostCode!= null && x.RequestersPostCode.ToString().Contains(txtsearch)) ||
                           (x.RequestersTelephoneNo!= null && x.RequestersTelephoneNo.ToLower().ToString().Contains(txtsearch)) ||
                           //x.RequestersTown != null? x.RequestersTown.ToString().Contains(txtsearch):false ||
                            // x.Site.ToString().Contains(txtsearch) ||
                             (x.SourceofRequest!= null && x.SourceofRequest.ToLower().ToString().Contains(txtsearch)) ||
                             (x.Status!= null && x.Status.ToString().ToLower().Contains(txtsearch)) ||
                             (x.Subject != null && x.Subject.ToString().ToLower().Contains(txtsearch)) ||
                             (x.TypeofRequest!= null && x.TypeofRequest.ToLower().ToString().Contains(txtsearch))
                             )

                          select x).ToList();
   
            }


            //.OrderBy(o => o.Priority == string.Empty ? "Z" : o.Priority).OrderByDescending(o => o.LoggedDateTime)
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };




        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetSDListDashboard(string ticketno, string type, string status, string accessno, string requestType = "", string company = "", string url = "",string town="",string postcode="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {

        try
        {

            List<Jqgrid> flsList = new List<Jqgrid>();
            flsList = DC.BLL.FLSDetailsBAL.Jqgridlist();
            if (url.ToLower().Contains("flsresourcelist.aspx"))
            {
                flsList = flsList.Where(f => f.LoggedByID == sessionKeys.UID || f.AssignedTechnicianID == sessionKeys.UID).ToList();

                //flsList = flsList.Where(f => f.AssignedTechnicianID == sessionKeys.UID).ToList();
            }
            //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
            //if (sessionKeys.PortfolioID != 0)
            //{
            //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            //}
            //check the Service desk user
            if (!string.IsNullOrEmpty(company))
            {
                var d_array = new string[] { "[Loading customer...]", "Please select..." };
                if (!d_array.Contains(company))
                {
                    flsList = flsList.Where(o => (o.Company != null && o.Company.ToLower() == company.ToLower())).ToList();
                }
            }

            if (status == "ALL ACTIVE" && ticketno == string.Empty)
            {

                flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Resolved" && f.Status != "Cancelled" && f.Status != "").ToList();
            }
            else
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
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


            if (status != "ALL ACTIVE")
            {
                if (status == "All Tickets")
                {
                    flsList = flsList.ToList();
                }
                else
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
                // flsList = flsList.OrderBy(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("Name DESC"))
            {
                //flsList = flsList.OrderByDescending(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("Site ASC"))
            {
                flsList = flsList.OrderBy(o => o.Site).ToList();
            }
            else if (jtSorting.Equals("Site DESC"))
            {
                flsList = flsList.OrderByDescending(o => o.Site).ToList();
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
            if (!string.IsNullOrEmpty(requestType))
            {
                var array = new string[] { "[Loading...]", "Please select..." };
                if (!array.Contains(requestType))
                    flsList = flsList.Where(f => f.TypeofRequest == requestType).ToList();
            }
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };




        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetSDHome(string requestType, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            List<FLSJList> flsList = FLSDetailsBAL.BindFLSJList().Where(f => f.LoggedBy == sessionKeys.UID).ToList();
            flsList = flsList.Where(f => f.Status != "Job Complete" && f.Status != "Cancelled").ToList();
            if (jtSorting.Equals("CallID ASC"))
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
            flsList = flsList.Where(f => f.RT.ToLower() == requestType).ToList();
            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }


    }

    [WebMethod(EnableSession = true)]
    public static List<SDColumn> GetColumns(string url="")
    {

        using (DCDataContext db = new DCDataContext())
        {
            if (url.ToLower().Contains("flsresourcelist.aspx"))
            {
                var name = (from d in db.DisplayColumns
                            join u in db.DisplayColumnsByUsers
                                 on d.ID equals u.DisplayColumnID
                            where u.UserID == sessionKeys.UID 
                            orderby u.Position
                            select new SDColumn
                            {
                                FieldName = d.Value,
                                ColumnName = ColumnTitleChange(d.ColumnName)
                            }).ToList();

                return name;
            }

            else
            {
                var name = (from d in db.DisplayColumns
                            join u in db.DisplayColumnsByUsers
                                 on d.ID equals u.DisplayColumnID
                            where u.UserID == sessionKeys.UID && d.ColumnName != "Im On My Way"
                            orderby u.Position
                            select new SDColumn
                            {
                                FieldName = d.Value,
                                ColumnName = ColumnTitleChange(d.ColumnName)
                            }).ToList();

                return name;
            }

        }

    }



    public static string ColumnTitleChange(string columnName)
    {
        string retval = string.Empty;
        if (columnName == "Resource Image")
            retval = "Assigned Technician";
        else if (columnName == "Email Icon")
            retval = "Email";
        else if (columnName == "Contact")
            retval = "Mobile No";
        else if (columnName == "Im On My Way")
            retval = "";
        else
            retval = columnName;
        return retval;
    }
    [WebMethod(EnableSession = true)]
    public static object onwaymail(string ticketno)
    {
        try
        {
            var flsdata = DC.BLL.FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault() ;
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
           
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/CallOntheWay.htm");
            string subject = string.Empty;
            subject = "I’m on my way" + "- Ticket Reference:"+ flsdata.CallID.ToString();
            body = body.Replace("[mail_head]", "Service Desk Request");

            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

            body = body.Replace("[user]", flsdata.RequesterName);


            body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());

            
            em.SendingMail(fromemailid, subject, body, flsdata.RequestersEmailAddress);



            return new { Result = "ERROR", Message = string.Empty };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    //protected void btnNew_Click(object sender, EventArgs e)
    //{
    //    if (ddlTypeofRequest.SelectedValue != "")
    //    {
    //        string type = ddlTypeofRequest.SelectedItem.Text;
    //        if (type != null)
    //        {
    //            Response.Redirect("DCNavigation.ashx?type=" + type);
    //        }
    //    }


    //}


}
public class SDColumn
{
    public string FieldName { get; set; }
    public string ColumnName { get; set; }
}