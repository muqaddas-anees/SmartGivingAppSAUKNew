using PortfolioMgt.Entity;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class FundraiserDetailsCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {

                    if (QueryStringValues.Type.Length > 0)
                        btnBack.Visible = true;
                    else
                        btnBack.Visible = false;

                    // tList = tList.Where(o => o.FundriserUNID.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();

                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var listTithingDefaults = pRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();


                    sessionKeys.FundPortfolioID = listTithingDefaults.FirstOrDefault().OrganizationID.Value;
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.FundPortfolioID).Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue?o.IsPaid.Value:false) ==  true).ToList();
                    // if(QueryStringValues.qr)

                    //if (QueryStringValues.EVENTUNID.Length > 0)
                    //    listTithingDefaults = listTithingDefaults.Where(o => o.Event_unid == QueryStringValues.EVENTUNID).ToList();
                    var f = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.FundUNID == QueryStringValues.UNID && o.Email.ToLower() == sessionKeys.UEmail.ToLower()).FirstOrDefault();

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
                                     p.ShowProgress,
                                     p.StartDate,
                                     p.Title,
                                     p.unid,
                                     p.SocialDescription,
                                     p.SocialKeywords,
                                     p.SocialTitle,
                                     p.FundraiserDetails,
                                     p.Address,
                                     p.State,
                                     p.City,
                                     p.Country,
                                     p.Postcode,
                                     RaisedAmount = p.unid == null ? 0.00 : tList.Where(o => o.FundriserUNID == p.unid).Where(o=>o.IsPaid.HasValue?o.IsPaid.Value:false).Select(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00).Sum()
                                 }).FirstOrDefault();
                    if(dlist != null)
                    {

                        UpdateQRCodeImage(dlist.unid);

                        //update shot description 
                        if (dlist.SocialDescription ==  null)
                        {
                            var dEntity = pRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                        }

                       if(dlist.ShowProgress == null)
                            {
                            FundProgressCtrl.Visible = true;
                        }

                        if (dlist.ShowProgress.HasValue)
                            {
                            FundProgressCtrl.Visible = dlist.ShowProgress.Value;
                        }
                        //FundraiserPayCtrl

                        //dlist.show
                    }

                 
                    // lblTarget.Text = "Target: " + string.Format("{0:C0}",  dlist.DefaultTarget);
                    lblTarget.Text = string.Format("{1}{0:N0}", dlist.DefaultTarget, Deffinity.Utility.GetCurrencySymbol());// string.Format("{0:C0}", dlist.DefaultTarget);
                   // imgQR.ImageUrl = "~/WF/UploadData/Events/" + dlist.unid + ".png"; ;
                    imgcenterimage.ImageUrl = GetImageUrl(dlist.ID.ToString());
                    lblTitle.Text = dlist.Title;
                   // lblTitle1.Text= dlist.Title;
                    lblDescription.Text = dlist.Description;
                    //if (dlist.StartDate.HasValue)
                    //    lbldates.Text = dlist.StartDate.Value.ToShortDateString() + " - " + dlist.EndDate.Value.ToShortDateString();

                    //lblAddress.Text = (dlist.Address ?? "") + " " + (dlist.City ?? "") + " " + (dlist.State ?? "") + " " + (dlist.Postcode ?? "") + " " + dlist.Country;

                    GetMyLogoImageUrl(dlist.ID.ToString());


                    SetQRCode(dlist.unid);
                    // if (f != null)
                    if((dlist.FundraiserDetails ?? "").Length >0)
                    {
                        pnl_mystory.Visible = true;
                        lblMyStory.Text = (dlist.FundraiserDetails ?? "");

                        lblDescription.Text = dlist.Description;
                    }
                    else
                    {
                        pnl_mystory.Visible = false;
                        lblDescription.Text = dlist.Description;
                    }


                    var userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll()
                        .Where(o => o.FundriserUNID == QueryStringValues.UNID).Where(o => o.IsPaid.HasValue ? o.IsPaid.Value : false).
                        OrderByDescending(o=>(o.PaidAmount.HasValue?o.PaidAmount.Value:0)).ToList();

                    //get list of fundrisers unids
                    var p2pIDs =pRep.GetAll().Where(o => o.MasterUNID == QueryStringValues.UNID).ToList();
                    if(p2pIDs.Count >0)
                    {
                      var   new_userlist = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll()
                       .Where(o => p2pIDs.Select(p=>p.unid).Contains( o.FundriserUNID )).Where(o => o.IsPaid.HasValue ? o.IsPaid.Value : false).
                       OrderByDescending(o => (o.PaidAmount.HasValue ? o.PaidAmount.Value : 0)).ToList();
                        userlist.AddRange(new_userlist);
                    }

                    var rList = (from r in userlist
                                 select new
                                 {
                                     Name = getDonorName( r),
                                     Email = getDonorEmail( r),
                                     Contact = getDonorContact(r),
                                     Amount = r.PaidAmount,
                                     AmountDis = string.Format("{1}{0:N2}", r.PaidAmount, Deffinity.Utility.GetCurrencySymbol())
                                 }).Take(10).ToList();

                    gridtopdonors.DataSource = rList.Where(o=>o.Amount >0).ToList();
                    gridtopdonors.DataBind();


                    totalsupporters.Value = "6"; //I am confused how to get this value
                    hraised.Value = string.Format("{0:F2}", userlist.Select(o=>o.PaidAmount).Sum());
                    hremaing.Value = string.Format("{0:F2}", dlist.DefaultTarget - userlist.Select(o => o.PaidAmount).Sum());
                    if (dlist.EndDate.HasValue)
                    {
                        // Get the end date from dlist
                        DateTime endDate = dlist.EndDate.Value; // Access the underlying DateTime value

                        // Calculate the number of days left from the current date to the end date
                        int daysLeft = (endDate - DateTime.Now).Days;

                        // Calculate the remaining value
                        remainingTime.Value = daysLeft.ToString();

                        // Format the remaining value as a string with 2 decimal places
                        
                    }
                    else
                    {
                        // Handle the case where EndDate is null
                        remainingTime.Value = "N\\A";

                    }

                    // Calculate the remaining value


                    // Format the remaining value as a string with 2 decimal places

                    if (rList.Count >0)
                    {
                        pnl_topdonors.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string getDonorName(TithingPaymentTracker t)
        {
            string retval = "";

            if (t.IsAnonymously.HasValue)
            {
                if (t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerName;
                }
            }
            else
            {
                retval = t.DonerName;
            }

            return retval;
        }
        private string getDonorEmail(TithingPaymentTracker t)
        {
            string retval = "";

            if (t.IsAnonymously.HasValue)
            {
                if (t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerEmail;
                }
            }
            else
            {
                retval = t.DonerEmail;
            }

            return retval;
        }

        private string getDonorContact(TithingPaymentTracker t)
        {
            string retval = "";

            if (t.IsAnonymously.HasValue)
            {
                if (t.IsAnonymously.Value)
                {
                    retval = "Anonymous";
                }
                else
                {
                    retval = t.DonerContact;
                }
            }
            else
            {
                retval = t.DonerContact;
            }

            return retval;
        }
        private void UpdateQRCodeImage(string unid)
        {
            try
            {
                string code = Deffinity.systemdefaults.GetWebUrl() + "/fundraiserview.aspx?.aspx?unid=" + unid; //ab.MoreDetails;

                var filepath = "~/WF/UploadData/Events/" + unid + ".png";

              //  if (!File.Exists(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png"))
                //{
                    // var filepathpdf = "~/WF/UploadData/Events/" + unid + ".pdf";

                    // var filepath = Server.MapPath("~/WF/UploadData/Events/") + id + ".png";
                    //if (File.Exists(Server.MapPath(filepath)))
                    //{
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();
                            System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms);
                            img1.Save(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        // plBarCode.Controls.Add(imgBarCode);
                    }
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void SetQRCode(string unid)
        {

            try
            {
                string code = Deffinity.systemdefaults.GetWebUrl() + "/FundraiserView.aspx?unid=" + unid;
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 150;
                imgBarCode.Width = 150;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();

                        ImageManager.FileDBSave(byteImage, null, unid, ImageManager.file_section_fundriser_qr, ".png", unid);
                        //System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms);
                       // img1.Save(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plBarCode.Controls.Add(imgBarCode);
                }
                // imgQR.ImageUrl = "~/WF/UploadData/Events/" + unid + ".png";
                imgQR.ImageUrl = "~/ImageHandler.ashx?id=" + unid + "&s=" + ImageManager.file_section_fundriser_qr;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected static string GetDonorImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/donor.png";
            //    //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            img = "~/WF/UploadData/Users/ThumbNailsMedium/donor.png";
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected void GetMyLogoImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
          //  bool isOriginal = false;

            //string img = string.Empty;
            //// contactsId = GetMasterID(contactsId);
            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + "_mylogo.png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());
            //    else
            //        img = string.Format("/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());

            //    pnl_img.InnerHtml = "<img ID='img' class='img-responsive' style='max-height:150px' src='" + img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString() + "'/>";
            //}
            //else
            //{
            //    img = "/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";

            //    pnl_img.InnerHtml = "";
            //}


            if (ImageManager.FileIsExists(contactsId.ToString(), ImageManager.file_section_fundriser_my_logo))
            {

               var img = "ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_fundriser_my_logo;

                pnl_img.InnerHtml = "<img ID='img' class='img-responsive' style='max-height:150px' src='" + img  + "'/>";
            }

            else
            {
                pnl_img.InnerHtml = "";
            }


        }
        protected static string GetImageUrl(string contactsId)
        {
            //contactsId = sessionKeys.UID.ToString();

            //bool isOriginal = false;

            //string img = string.Empty;
            //contactsId = GetMasterID(contactsId);

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{

            //    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //    //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            return "~/ImageHandler.ashx?id=" + contactsId.ToString() + "&s=" + ImageManager.file_section_fundriser;
        }
        private static string GetMasterID(string id)
        {
            string retId = "";
            var eDetails = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
            if (eDetails != null)
            {
                // contactsId = eDetails.MasterUNID
                if (eDetails.MasterUNID != null)
                {
                    eDetails = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == eDetails.MasterUNID).FirstOrDefault();
                    if (eDetails != null)
                        retId = eDetails.ID.ToString();
                }
                else
                {
                    retId = id;
                }
            }
            else
            {
                retId = id;
            }

            return retId;

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}