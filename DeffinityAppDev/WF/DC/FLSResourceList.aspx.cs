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

public partial class FLSResourceList : System.Web.UI.Page
{
    DisplayColumnBAL dcBAL = new DisplayColumnBAL();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7 )
        {
            this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    [WebMethod(EnableSession = true)]
    public static object GetSD(string ticketno, string type, string status, string accessno, string requestType = "", string company = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {


            List<FLSJList> flsList = FLSDetailsBAL.BindFLSResourceList(sessionKeys.UID);
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
    public static object GetSDList(string ticketno, string type, string status, string accessno, string requestType = "", string company = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {

        try
        {


            List<Jqgrid> flsList = new List<Jqgrid>();
            flsList = DC.BLL.FLSDetailsBAL.Jqgridlist();
            //flsList = flsList.Where(f => f.Status != "Quote Requested" && f.Status != "Order Received" && f.Status != "Quotation Submitted" && f.Status != "Cancelled").ToList();
            //if (sessionKeys.PortfolioID != 0)
            //{
            //    flsList = flsList.Where(f => f.Company.ToString() == sessionKeys.PortfolioName).ToList();
            //}
            //check the Service desk user
            flsList = flsList.Where(f => f.AssignedTechnicianID == sessionKeys.UID).ToList();
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
   
}