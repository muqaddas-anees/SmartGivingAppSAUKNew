using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace DeffinityAppDev.App
{
    public partial class FundraiserView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if(!IsPostBack)
                {

                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.DonerEmail != null).Where(o => o.PaidDate.HasValue).ToList();

                    // tList = tList.Where(o => o.FundriserUNID.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();

                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var listTithingDefaults = pRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();

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
                                     RaisedAmount = p.unid == null ? 0.00 : tList.Where(o => o.FundriserUNID == p.unid).Select(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00).Sum()
                                 }).FirstOrDefault();


                    hraised.Value = string.Format("{0:F2}", dlist.RaisedAmount);
                    hremaing.Value = string.Format("{0:F2}", dlist.DefaultTarget- dlist.RaisedAmount);

                    imgQR.ImageUrl = "~/WF/UploadData/Events/" + dlist.unid + ".png"; ;
                    imgcenterimage.ImageUrl = GetImageUrl(dlist.ID.ToString());
                    lblTitle.Text = dlist.Title;
                    lblDescription.Text = dlist.Description;


                    var userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.FundriserUNID == QueryStringValues.UNID).Where(o => o.PaidDate.HasValue).Take(10);
                    var rList = (from r in userlist
                                 select new
                                 {
                                     Name = r.DonerName,
                                     Email = r.DonerEmail,
                                     Contact = r.DonerContact,
                                     Amount = r.PaidAmount
                                 }).ToList();

                    gridtopdonors.DataSource = rList;
                    gridtopdonors.DataBind();

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetImageUrl(string contactsId)
        {
            //contactsId = sessionKeys.UID.ToString();

            //bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {

                img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}