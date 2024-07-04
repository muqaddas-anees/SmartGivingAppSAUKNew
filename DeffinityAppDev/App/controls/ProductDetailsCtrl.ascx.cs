using DC.BLL;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;

namespace DeffinityAppDev.App.controls
{
    public partial class ProductDetailsCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {

                    string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                    ddlMonth.DataSource = Month;
                    ddlMonth.DataBind();
                    //pre-select one for testing
                    ddlMonth.SelectedIndex = 4;

                    //populate year
                    ddlYear.Items.Add("");
                    int Year = DateTime.Now.Year;
                    for (int i = 0; i < 10; i++)
                    {
                        ddlYear.Items.Add((Year + i).ToString());
                    }
                    //pre-select one for testing
                    ddlYear.SelectedIndex = 3;

                    // tList = tList.Where(o => o.FundriserUNID.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();

                    //IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
                    //var p = pRep.GetAll().Where(o => o.ProductGuid == QueryStringValues.UNID).FirstOrDefault();


                    IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
                    IPortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> psRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();

                    var pd = pdRep.GetAll().Where(o => o.ProductGuid == QueryStringValues.UNID).ToList();

                    var ps = psRep.GetAll().Where(o => pd.Select(j => j.ProductGuid).ToList().Contains(o.ProductGuid)).Where(o => (o.IsPaid ?? false) == true).ToList();

                    var rlist = (from p in pd
                                 select new
                                 {
                                     p.PortfolioID,
                                     p.ID,
                                     p.ProductGuid,
                                     Status = (p.IsActive ?? true) ? "<span class='badge badge-success'>Active</span>" : "<span class='badge badge-danger'>Inactive</span>",
                                     p.ProductName,
                                     ProductDetails = p.ProductDetails,
                                     TotalUnits = p.TotalUnits - ps.Where(o => o.ProductGuid == p.ProductGuid).Count(),
                                     ProductPriceDisplay = string.Format("{1}{0:N2}", p.ProductPrice, Deffinity.Utility.GetCurrencySymbol()),
                                     p.ProductPrice,
                                     UnitsSold = ps.Where(o => o.ProductGuid == p.ProductGuid).Count(),
                                     Url = Deffinity.systemdefaults.GetWebUrl() + "/Product/" + p.ShortCode
                                 }).FirstOrDefault();




                    sessionKeys.PortfolioID = rlist.PortfolioID;


                    IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                    var paySettings = paRep.GetAll().Where(o => o.PortfolioID == rlist.PortfolioID && o.IsActive == true).FirstOrDefault();

                    if(paySettings != null)
                    {
                        if(paySettings.PayType == "cardconnect")
                        {
                            pnlCardConnect.Visible = true;
                            pnlCreditCard.Visible = false;
                        }
                        else
                        {
                            pnlCardConnect.Visible = false;
                            pnlCreditCard.Visible = true;
                        }
                    }
                    // var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.DonerEmail != null).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                    // if(QueryStringValues.qr)

                    //if (QueryStringValues.EVENTUNID.Length > 0)
                    //    listTithingDefaults = listTithingDefaults.Where(o => o.Event_unid == QueryStringValues.EVENTUNID).ToList();
                    //  var f = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.FundUNID == QueryStringValues.UNID && o.Email.ToLower() == sessionKeys.UEmail.ToLower()).FirstOrDefault();



                    //hraised.Value = string.Format("{0:F2}", dlist.RaisedAmount);
                    //hremaing.Value = string.Format("{0:F2}", dlist.DefaultTarget - dlist.RaisedAmount);
                    //// lblTarget.Text = "Target: " + string.Format("{0:C0}",  dlist.DefaultTarget);
                    //lblTarget.Text = string.Format("{0:C0}", dlist.DefaultTarget);
                    // imgQR.ImageUrl = "~/WF/UploadData/Events/" + dlist.unid + ".png"; ;
                    imgcenterimage.ImageUrl = GetImageUrl(rlist.ProductGuid.ToString());
                    lblTitle.Text = rlist.ProductName;
                    lblDescription.Text = rlist.ProductDetails;
                    lblPrice.Text = string.Format("{1}{0:N2}", rlist.ProductPrice, Deffinity.Utility.GetCurrencySymbol());
                    hmoney.Value = rlist.ProductPrice.ToString();

                    pnlNoStock.Visible = false;
                    pnlQty.Visible = false;
                    if (rlist.TotalUnits >0)
                    {
                       if(rlist.TotalUnits > rlist.UnitsSold )
                        {
                            pnlQty.Visible = true;
                            pnlNoStock.Visible = false;
                        }
                        else
                        {
                            pnlNoStock.Visible = true;
                            pnlQty.Visible = false;
                        }
                    }
                    else
                    {
                        pnlNoStock.Visible = true;
                        pnlQty.Visible = false;
                    }
                  
                }
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

        //protected void GetMyLogoImageUrl(string contactsId)
        //{
        //    //return GetImageUrl(a_gId, a_oThumbSize, true);
        //    bool isOriginal = false;

        //    string img = string.Empty;
        //    // contactsId = GetMasterID(contactsId);
        //    string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + "_mylogo.png";

        //    if (System.IO.File.Exists(filepath))
        //    {
        //        if (isOriginal)
        //            img = string.Format("/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());
        //        else
        //            img = string.Format("/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());

        //        pnl_img.InnerHtml = "<img ID='img' class='img-responsive' style='max-height:150px' src='" + img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString() + "'/>";
        //    }
        //    else
        //    {
        //        img = "/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";

        //        pnl_img.InnerHtml = "";
        //    }


        //}
        protected static string GetImageUrl(string contactsId)
        {
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Products/") + "Product_org_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt=" + DateTime.Now.TimeOfDay, contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt=" + DateTime.Now.TimeOfDay, contactsId.ToString());
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_online;

        }
       
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            pnlPaymentDetails.Visible = false;
            pnlProductDetails.Visible = false;
            pnlUserDetails.Visible = true;
            pnlResult.Visible = false;
          //  ContinuePayment();
        }

        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
            //pnlPaymentDetails.Visible = true;
            //pnlProductDetails.Visible = false;
            //pnlUserDetails.Visible = false;
            //pnlResult.Visible = false;
            ContinuePayment();
        }

        protected void btnBacktologin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx",false);
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            ContinuePayment();
        }

        private void ContinuePayment()
        {
            try
            {
                var cnumber = "";
                if (pnlCardConnect.Visible == true)
                {
                    cnumber = txtCardConnectNumber.Text.Trim();
                }
                else
                {
                    cnumber = txtCardNumber.Text.Trim();
                }
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                var paySettings = paRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.IsActive == true).FirstOrDefault();
                var retval = QuickPayBAL.OnlineCardConnectPay(sessionKeys.PortfolioID, QueryStringValues.UNID, cnumber, ddlMonth.SelectedItem.Text.Trim(),
                         ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                         Convert.ToDouble(hmoney.Value), txtNameOnCard.Text.Trim(), txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(),
                         txtaddress.Text.Trim(), txtTown.Text.Trim(), txtState.Text.Trim(), txtZipcode.Text.Trim(), Convert.ToInt32(txtQTY.Text.Trim()));

                if (retval.ToLower() == "approved")
                {
                    lblMsg.Text = "Thank you.";
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/PayProcess.aspx?frm=online&refid=" + retval + "&type=" + paySettings.PayType, false);
                }

                pnlPaymentDetails.Visible = false;
                pnlProductDetails.Visible = false;
                pnlUserDetails.Visible = false;
                pnlResult.Visible = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}