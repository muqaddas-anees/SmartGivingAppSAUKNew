using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

public partial class controls_CustomerOrder : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetLabelValues();
            if (Request.QueryString["callid"] != null)
            {
                int ticketNo = int.Parse(Request.QueryString["callid"].ToString());
                BindData(ticketNo);
            }
            else if (Request.QueryString["incidentid"] != null)
            {
                int incidentId = int.Parse(Request.QueryString["incidentid"].ToString());
                BindData(incidentId);
            }

           
           
                pnlStatus.Visible = true;
                var getpriceEntity = GetPrice();
                if (getpriceEntity != null)
                {
                    if (getpriceEntity.OriginalPrice > GetThresholdPrice())
                    {
                         var fdata = GetFixedPriceApprovalData();
                         if (fdata != null)
                         {
                             if (fdata.ApprovedBy.HasValue)
                             {
                                 txtStatus.Text = "Price Approved";
                                 //txtStatus.BackColor = System.Drawing.Color.Green;
                                 //txtStatus.ForeColor = System.Drawing.Color.White;
                             }
                             else if (fdata.DeniedBy.HasValue)
                             {
                                 txtStatus.Text = "Denied";
                                 //txtStatus.BackColor = System.Drawing.Color.Red;
                                 //txtStatus.ForeColor = System.Drawing.Color.White;
                             }
                             else
                                 txtStatus.Text = "Not Selected";


                         }
                         else
                         {
                             pnlStatus.Visible = false;
                         }
                       
                    }
                    else
                    {
                        pnlStatus.Visible = false;
                    }
                }
                else
                {
                    pnlStatus.Visible = false;
                }
            //}
            //else
            //{
            //    pnlStatus.Visible = false;
            //}
        }
    }

    private void SetLabelValues()
    {
        var fieldsList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).OrderBy(g => g.Position).ToList();
        foreach(var item in fieldsList)
        {
            if (item.DefaultName.ToLower().Contains("requester name"))
            {
                lblRequesterName.Text = item.InstanceName;

            }
            else if (item.DefaultName.ToLower().Contains("requesters email address"))
            {
                lblRequesterMail.Text = item.InstanceName;
            }
            else if (item.DefaultName.ToLower().Contains("requesters telephone no"))
            {
                lblRequesterContact.Text = item.InstanceName;
            }
            else if (item.DefaultName.ToLower().StartsWith("details"))
            {
                lblFlsDetails.Text = item.InstanceName;
            }
        }
    }
    private DC.Entity.Incident_ServicePrice GetPrice()
    {
        var aRepository = new DCRepository<DC.Entity.Incident_ServicePrice>();
        return aRepository.GetAll().Where(o => o.IncidentID == QueryStringValues.CallID).FirstOrDefault();
    }
    private DC.Entity.FixedPriceApproval GetFixedPriceApprovalData()
    {
        var aRepository = new DCRepository<DC.Entity.FixedPriceApproval>();
        return aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
    }
    private double GetThresholdPrice()
    {
        double retval = 0.0;
        var tRepository = new DCRepository<DC.Entity.FixedPriceThreshold>();
        var fentiry = tRepository.GetAll().FirstOrDefault();
        if (fentiry != null)
            retval = fentiry.FPTPrice;

        return retval;
    }
    private void BindData(int ticketNo)
    {
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            using (DCDataContext db = new DCDataContext())
            {
                //var ppList = pd.ProjectPortfolios.Select(p => p).ToList();
                //var pcList = pd.PortfolioContacts.Select(p => p).ToList();
                //var flsList = db.FLSDetails.Where(d=>d.CallID==ticketNo).Select(d => d).ToList();
                //var cdList = db.CallDetails.Where(d => d.ID == ticketNo).Select(c => c).ToList();

                //var result = (from cd in cdList
                //              join f in flsList on cd.ID equals f.CallID
                //              join pc in pcList on cd.RequesterID equals pc.ID
                //              join pp in ppList on cd.CompanyID equals pp.ID
                //              select new { Company = pp.PortFolio, Name = pc.Name, Email = pc.Email, MobileNo = pc.Telephone, Details = f.Details }).FirstOrDefault();
                var result = FLSDetailsBAL.Jqgridlist(ticketNo).FirstOrDefault();
                
                    if (result != null)
                {
                   // lblCompany.Text = result.Company;
                    lblName.Text = result.RequesterName;
                    lblRequesterEmail.Text = result.RequestersEmailAddress;
                    lblRequesterTelepnoneNo.Text = result.RequestersTelephoneNo;
                    lblDetails.Text = result.Details;
                    lblRaddress.Text = result.RequestersAddress + ", " + result.RequestersCity + ", " + result.RequestersTown + ", " + result.RequestersPostCode;
                }

            }
        }
    } 
}