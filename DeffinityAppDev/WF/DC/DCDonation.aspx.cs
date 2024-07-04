using DC.BLL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCDonation : System.Web.UI.Page
    {
        int entrytype = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["callid"] != null)
                    {
                        sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                    }
                    if (QueryStringValues.CCID > 0)
                        lblTitle.InnerText = sessionKeys.JobDisplayName + " Donation for " + sessionKeys.JobDisplayName + " Reference " + QueryStringValues.CCID + ": " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID);
                    else
                        lblTitle.InnerText = sessionKeys.JobDisplayName + " Donation for " + " " + Resources.DeffinityRes.ServiceDesk;

                  
                   


                    //Bind donors data
                    var dENtity = FLSDetailsBAL.FLSDetailsBAL_SelectAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                    if (dENtity != null)
                    {
                        if (dENtity.UNID != null)
                        {
                            if(dENtity.Section == "category")
                            {
                                BindDonorsData_category(dENtity.UNID);
                            }
                            else
                            {
                                BindDonorsData(dENtity.UNID);
                            }
                        }
                            
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDonorsData_category(string unid)
        {
            //var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => listTithingCategory.Select(p => p.DonationID).Contains(o.ID)).Where(o => o.OrganizationID == sessionKeys.PortfolioID)
            //  .Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
            IPortfolioRepository<PortfolioMgt.Entity.TithingCategoryAmount> cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingCategoryAmount>();
            var listTithingCategory = cRep.GetAll().Where(o => o.CategoryUNID == unid).ToList();


            //var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => listTithingCategory.Select(p => p.DonationID).Contains(o.ID)).Where(o => o.OrganizationID == sessionKeys.PortfolioID)
            // .Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
            var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => listTithingCategory.Select(p => p.DonationID).Contains(o.ID)).Where(o => o.OrganizationID == sessionKeys.PortfolioID)
             .Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();

            //list of valid payids


            listTithingCategory = listTithingCategory.Where(o => tList.Select(p => p.ID).Contains(o.DonationID.Value)).ToList();

            

            var rised_amount = listTithingCategory.Sum(o => o.CategoryAmount.HasValue?o.CategoryAmount.Value:0.00);
            lblTargetAmount.Text = string.Format("{1}{0:N2}", 0.00);

            lblRaised.Text = string.Format("{1}{0:N2}", rised_amount, Deffinity.Utility.GetCurrencySymbol());

            lblRemainig.Text = string.Format("{1}{0:N2}", 0.00, Deffinity.Utility.GetCurrencySymbol());

            hraised.Value = string.Format("{0:F2}", rised_amount);
            hremaing.Value = string.Format("{0:F2}", rised_amount - rised_amount);
            // lblTarget.Text = "Target: " + string.Format("{0:C0}",  dlist.DefaultTarget);
            lblTarget.Text = string.Format("{1}{0:N2}", 0.00, Deffinity.Utility.GetCurrencySymbol());
            // imgQR.ImageUrl = "~/WF/UploadData/Events/" + dlist.unid + ".png"; ;
            // imgcenterimage.ImageUrl = GetImageUrl(dlist.ID.ToString());
            // lblTitle.Text = dlist.Title;
            //  lblDescription.Text = dlist.Description;


            //var userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == unid).Where(o => o.IsPaid.HasValue ? o.IsPaid.Value : false).OrderByDescending(o => (o.PaidAmount.HasValue ? o.PaidAmount.Value : 0)).Take(10);
            var userlist = tList;// PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == unid).ToList();
            var rList = (from r in userlist
                         select new
                         {
                             Name = r.DonerName,
                             Email = r.DonerEmail,
                             Contact = r.DonerContact,
                             Amount = r.PaidAmount,
                             r.PaidDate
                         }).ToList();

            gridtopdonors.DataSource = rList.Where(o => o.Amount > 0).ToList();
            gridtopdonors.DataBind();

        }

        private void BindDonorsData(string unid)
        {
            
            
            
            IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
            var listTithingDefaults = pRep.GetAll().Where(o => o.unid == unid).ToList();


            sessionKeys.PortfolioID = listTithingDefaults.FirstOrDefault().OrganizationID.Value;
             var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
           // var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == unid).Where(o => o.DonerEmail != null).ToList();
            // if(QueryStringValues.qr)

            //if (QueryStringValues.EVENTUNID.Length > 0)
            //    listTithingDefaults = listTithingDefaults.Where(o => o.Event_unid == QueryStringValues.EVENTUNID).ToList();


            var dlist = (from p in listTithingDefaults
                         select new
                         {
                             p.CreatedDateTime,
                             p.Currency,
                             p.DefaultBanner,
                             p.DefaultTarget,
                             p.DefaultValues,
                             p.Description,
                             p.EndDate,
                             p.Event_unid,
                             p.ID,
                             p.IsEnable,
                             p.IsFundraiser,
                             p.LoggedByID,
                             p.ModifiedDateTime,
                             p.OrganizationID,
                             p.SendMailAfterDonation,
                             p.ShowChart,
                             p.ShowQRCode,
                             p.StartDate,
                             p.Title,
                             p.unid,
                             p.SocialDescription,
                             p.SocialKeywords,
                             p.SocialTitle,
                              RaisedAmount = p.unid == null ? 0.00 : tList.Where(o => o.FundriserUNID == p.unid).Where(o => o.IsPaid.HasValue ? o.IsPaid.Value : false).Select(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00).Sum()
                            // RaisedAmount = p.unid == null ? 0.00 : tList.Where(o => o.FundriserUNID == p.unid).Select(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00).Sum()
                         }).FirstOrDefault();
            

            lblTargetAmount.Text = string.Format("{1}{0:N2}", dlist.DefaultTarget, Deffinity.Utility.GetCurrencySymbol());

            lblRaised.Text = string.Format("{1}{0:N2}", dlist.RaisedAmount, Deffinity.Utility.GetCurrencySymbol());

            lblRemainig.Text = string.Format("{1}{0:N2}", dlist.DefaultTarget - dlist.RaisedAmount, Deffinity.Utility.GetCurrencySymbol());

            hraised.Value = string.Format("{0:F2}", dlist.RaisedAmount);
            hremaing.Value = string.Format("{0:F2}", dlist.DefaultTarget - dlist.RaisedAmount);
            // lblTarget.Text = "Target: " + string.Format("{0:C0}",  dlist.DefaultTarget);
            lblTarget.Text = string.Format("{1}{0:N2}", dlist.DefaultTarget, Deffinity.Utility.GetCurrencySymbol());
            // imgQR.ImageUrl = "~/WF/UploadData/Events/" + dlist.unid + ".png"; ;
            // imgcenterimage.ImageUrl = GetImageUrl(dlist.ID.ToString());
            // lblTitle.Text = dlist.Title;
            //  lblDescription.Text = dlist.Description;


            //var userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == unid).Where(o => o.IsPaid.HasValue ? o.IsPaid.Value : false).OrderByDescending(o => (o.PaidAmount.HasValue ? o.PaidAmount.Value : 0)).Take(10);
            var userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == unid).ToList();
            var rList = (from r in userlist
                         select new
                         {
                             Name = r.DonerName,
                             Email = r.DonerEmail,
                             Contact = r.DonerContact,
                             Amount = r.PaidAmount,
                             r.PaidDate
                         }).ToList();

            gridtopdonors.DataSource = rList.Where(o => o.Amount > 0).ToList();
            gridtopdonors.DataBind();

        }

       

        #region timesheets
       

      


      
        #region Grid functions
        protected bool GetTimeSheetStatusCheck(string statusid)
        {
            bool st = false;
            try
            {
                //check the status is approve or submitted
                if ((int.Parse(statusid) == 1))
                    st = true;
                if ((int.Parse(statusid) == 3))
                    st = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return st;

        }

        protected bool GetTimeSheetStatusDeclineCheck(string statusid)
        {
            bool st = false;
            try
            {
                //check the status is approve or submitted
                if ((int.Parse(statusid) == 3))
                    st = true;
                else
                    st = false;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return st;

        }
        public string ChangeHoues(string GetHours)
        {

            string GetActivity = "";
            try
            {
                char[] comm1 = { '.' };
                string[] displayTime = GetHours.Split(comm1);


                GetActivity = displayTime[0] + ":" + displayTime[1];


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetActivity;
        }
        public string TimesheetStatus(string Status)
        {
            string TimesheetStaus = "";
            try
            {

                if (Status == "4")
                {
                    TimesheetStaus = "Approved";
                }
                else if (Status == "2")
                {
                    TimesheetStaus = "Submitted for Approval";
                }
                else if (Status == "1")
                {
                    TimesheetStaus = "Not Submitted";
                }
                else
                {
                    TimesheetStaus = "Declined";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return TimesheetStaus;
        }
        #endregion
      
      

      

       
        #endregion


        #region expenses

      
      

       
       

        public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);

            ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
            if (a_oThumbSize.HasValue)
            {
                switch (a_oThumbSize.Value)
                {
                    case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
                }
            }
            else
            {
                eImageType = ImageManager.ImageType.OriginalData;
            }

            // return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
            return "~/WF/UploadData/" + a_gId.ToString() + "_thumb.png";
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
        public bool CheckImageVisibility(Guid a_guid)
        {
            bool _visible = true;
            if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                _visible = true;
            }
            return _visible;
        }


        #endregion

    }
}
