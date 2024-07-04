//using AngleSharp.Dom;
using DocumentFormat.OpenXml.Office2010.Excel;
using iText;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using PortfolioMgt.Entity;
using QRCoder;
using shortid.Configuration;
using shortid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using static DC_FLS_ctrl;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using Image = iText.Layout.Element.Image;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using iTextSharp.text.pdf.qrcode;
using QRCode = QRCoder.QRCode;
using DocumentFormat.OpenXml.Vml;
using Path = System.IO.Path;

namespace DeffinityAppDev.App
{

    public partial class EventList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sval = ddlSelect.SelectedValue;
                BindEventDetails(sval);
            }
        }




        private void BindEventDetails(string sval)
        {
            try
            {

                var currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                var alist = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o=>o.OrganizationID == sessionKeys.PortfolioID).ToList();

              
                //update shotr url
                foreach (var eventEntity in alist.Where(o=>o.QRcode == null).ToList())
                {
                    var eDetails = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(eventEntity.unid);
                    if (eventEntity.QRcode == null)
                    {
                        var options = new GenerationOptions(useSpecialCharacters: false);
                        string shortid = ShortId.Generate(options);
                        eDetails.QRcode = shortid;

                        PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eDetails);
                       

                    }
                }

                foreach (var eventEntity in alist.Where(o => o.SocialDescription == null).ToList())
                {
                    var eDetails = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(eventEntity.unid);
                   
                        if (eventEntity.SocialDescription == null)
                    {

                        eDetails.SocialDescription = Deffinity.Utility.RemoveHTMLTags(eventEntity.Description == null ? "" : eventEntity.Description).Take(200).ToString();
                        PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eDetails);
                    }
                }

                var tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();
                var tList = tRep.GetAll().ToList();

                //foreach (var eventEntity in alist.ToList())
                //{
                //    var isExists = File.Exists(Server.MapPath("~/WF/UploadData/Events/Event_" + eventEntity.unid + "_top.png"));
                //    if (!isExists)
                //    {

                //        File.Copy(Server.MapPath("~/WF/UploadData/Events/" + eventEntity.unid+"/" + "0.png"), Server.MapPath("~/WF/UploadData/Events/Event_" + eventEntity.unid + "_top.png"));
                //    }
                //}

                var retval = (from a in alist

                              select new
                              {
                                  a.ID ,
                                  a.ActivityCategoryID,
                                  a.ActivitySubCategoryID,
                                  a.Address1,
                                  a.Address2,
                                  a.BookingEndDate,
                                  a.BookingStartDate,
                                  a.CategoryName,
                                  a.City,
                                  a.CreatedDate,
                                  Description = Deffinity.Utility.RemoveHTMLTags(a.Description == null ? "" : a.Description),
                                  a.EndDateTime,
                                  a.Event_Capacity,
                                  a.OrganizationID,
                                  a.OrganizationName,
                                  a.Title,
                                  a.unid,
                                  a.StartDateTime,
                                  a.LoggedBy,
                                  a.LoggedByName,
                                  a.state_Province,
                                  a.VenueLocation_Link,
                                  a.Venue_Name,
                                  a.Venue_Type,
                                  a.Slots,
                                  Price = GetPrices(tList.Where(o => o.unid == a.unid).ToList()),
                                  a.Postalcode,
a.Country,
                                  QRcode = Deffinity.systemdefaults.GetWebUrl() + "/Event/" + a.QRcode

                              }).ToList();
                if(retval.Count ==0)
                {
                    pnlNodata.Visible = true;
                    kt_content.Visible = false;
                }
               
                var cdate = Convert.ToDateTime( DateTime.Now.ToShortDateString());
                if(sval == "past")
                {
                    retval = retval.Where(o => o.StartDateTime < cdate).ToList();
                }
                else if (sval == "up")
                {
                    retval = retval.Where(o => o.StartDateTime >= cdate).ToList();
                    if (retval.Count == 0)
                    {
                        pnlNodata.Visible = true;
                        kt_content.Visible = false;
                    }
                    else
                    {
                        pnlNodata.Visible = false;
                        kt_content.Visible = true;
                    }
                }
                else
                {
                    if (retval.Count == 0)
                    {
                        pnlNodata.Visible = true;
                        kt_content.Visible = false;
                    }
                    else
                    {
                        pnlNodata.Visible = false;
                        kt_content.Visible = true;
                    }
                }
                
                //var list = cRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                //ListEventDetails.DataSource = list;
                //ListEventDetails.DataBind();
                //if (retval.Count > 0)
                //{
                    lvCustomers.DataSource = retval.OrderBy(o => o.StartDateTime).ToList();
                    lvCustomers.DataBind();
                //}
             
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private string GetPrices(List<PortfolioMgt.Entity.ActivityTicketSetting> tList)
        {
            string retval = string.Empty;
            var t = tList.OrderBy(o => o.Price).FirstOrDefault();
            if (t != null)
            {
                if (t.Price == 0.00)
                {
                    retval = "Free";
                }
                else
                {
                    if (retval.Length > 0)
                    {
                        retval = retval + " - " + string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                    }
                    else
                    {
                        retval = string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                    }
                }
            }

            if (tList.Count > 1)
            {
                t = tList.OrderBy(o => o.Price).LastOrDefault();
                if (t != null)
                {
                    if (t.Price == 0.00)
                    {
                        retval = retval = retval + " - " + "Free";
                    }
                    else
                    {
                        if (retval.Length > 0)
                        {
                            retval = retval + " - " + string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                        }
                        else
                        {
                            retval = string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                        }
                    }
                }
            }



            return retval;
        }

        protected string GetImmage(object activityid)
        {
            //string retval = string.Empty;
            //if (activityid != null)
            //{
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
            //    {
            //        retval = "~/WF/UploadData/Events/" + activityid + "/0.png?v=" + DateTime.Now.Ticks;
            //    }
                
            //   else
            //    {

            //        retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
            //}
            //else
            //    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //return retval;
            return "~/ImageHandler.ashx?id=" + activityid + "&s=" + ImageManager.file_section_event; //"img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        }

        protected string GetImmageString(string activityid)
        {
            string retval = string.Empty;
            if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid + "/0.png")))
            {
                retval = "<div class='bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px' style='background-image:url('"+"../../WF/UploadData/Events/" + activityid + "/0.png"+"?v="+DateTime.Now.Ticks+"')' ></div> ";
            }
            else
            {

                retval = "";
            }
            return retval;

        }



        protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            var sval = ddlSelect.SelectedValue;
            (lvCustomers.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.BindEventDetails(sval);
        }

        protected void lvCustomers_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "viewmore")
            {
                var id = e.CommandArgument.ToString();

                Response.Redirect("~/App/Events/EventDetails.aspx?unid=" + id, false);

            }
            else if (e.CommandName == "editevent")
            {
                var id = e.CommandArgument.ToString();

                Response.Redirect("~/App/Events/BasicInfo.aspx?unid=" + id, false);
            }
            else if (e.CommandName == "url")
            {

                var id = e.CommandArgument.ToString();
                var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                //if(eventEntity != null)
                //{
                //    if (eventEntity.QRcode == null)
                //    {
                //        var options = new GenerationOptions(useSpecialCharacters: false);
                //        string shortid = ShortId.Generate(options);
                //        eventEntity.QRcode = shortid;

                //        PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eventEntity);


                //    }

                //    Clipboard.SetText(Deffinity.systemdefaults.GetWebUrl() + "/Events/" + eventEntity.QRcode);
                // }

                //Clipboard.SetText(Val);

                //  Response.Redirect("~/App/Events/BasicInfo.aspx?unid=" + id, false);
            }
            else if (e.CommandName == "qr")
            {
                var id = e.CommandArgument.ToString();
                var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o => o.unid == id).FirstOrDefault();
                var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();

                string code = Deffinity.systemdefaults.GetWebUrl() + "/EventDetailsNew.aspx?unid=" + id; //ab.MoreDetails;

                var filepath = "~/WF/UploadData/Events/" + id + ".png";
                var filepathpdf = "~/WF/UploadData/Events/" + id + ".pdf";

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
                        img1.Save(Server.MapPath("~/WF/UploadData/Events/") + id + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plBarCode.Controls.Add(imgBarCode);
                }
                // }

                var pdfpath = Server.MapPath(filepathpdf);

                PdfWriter writer = new PdfWriter(pdfpath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph header = new Paragraph(pEntity.PortFolio)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(24).SetBold();


                document.Add(header);

                Paragraph subheader1 = new Paragraph(eventEntity.Title)
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(24);
                document.Add(subheader1);
                //Paragraph subheader2 = new Paragraph(string.Format("{0:MM/dd/yyyy HH:mm} - {1:MM/dd/yyyy HH:mm}", eventEntity.StartDateTime, eventEntity.EndDateTime))
                Paragraph subheader2 = new Paragraph(string.Format("{0:MM/dd/yyyy hh:mm tt} - {1:MM/dd/yyyy hh:mm tt}", eventEntity.StartDateTime, eventEntity.EndDateTime))
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(20);
                document.Add(subheader2);

                Image img = new Image(ImageDataFactory
           .Create(Server.MapPath(filepath)))
           .SetTextAlignment(TextAlignment.CENTER)
           .SetHeight(500)
           .SetHorizontalAlignment(HorizontalAlignment.CENTER)

           ;

                document.Add(img);

                Paragraph subheader = new Paragraph("SCAN TO VIEW EVENT")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(35);
                document.Add(subheader);

                document.Close();

                Response.Redirect("~/App/Events/downloadpdf.ashx?unid=" + id, false);

            }
            else if (e.CommandName == "attendees")
            {
                var id = e.CommandArgument.ToString();

                Response.Redirect("~/App/Events/ViewAttendees.aspx?unid=" + id, false);
            }
            else if (e.CommandName == "del")
            {
                var id = e.CommandArgument.ToString();
                var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                if (eventEntity != null)
                {
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_delete(eventEntity.ID);
                    var sval = ddlSelect.SelectedValue;
                    BindEventDetails(sval);
                }

            }
            else if (e.CommandName == "social")
            {
                var id = e.CommandArgument.ToString();
                var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                if (eventEntity != null)
                {
                    heventid.Value = eventEntity.unid;
                    txtDescription.Text = eventEntity.SocialDescription;
                    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/Event_" + heventid.Value + "_top.png")))

                    {
                        imgTop.Visible = true;
                        imgTop.ImageUrl = "~/WF/UploadData/Events/Event_" + heventid.Value + "_top.png";
                    }
                    txtKeywords.Text = eventEntity.KeyWords;
                    mdlSocialSettings.Show();
                }

            }
            else if (e.CommandName == "interaction")
            {
                var id = e.CommandArgument.ToString();
                var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                Response.Redirect("~/App/Events/EventInteraction.aspx?eventunid=" + eventEntity.unid, false);

            }
        }

        protected string getPrice(string price )
        {
            if(Convert.ToDouble(price) >0)
            {
                return string.Format("{0:F2}", Convert.ToDouble(price));
            }
            else
            {
                return "Free";
            }
        }

        protected static string GetAddress(object description)
        {
            string retval = "";
            if (description != null)
            {
                if(description.ToString().Length >500)
                {
                    retval = description.ToString().Substring(0, 490)+"...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        protected static string GetAddress(object address1, object address2,object city,object state,object postcode,object country)
        {
            string retval = "";

            if (address1 != null)
                retval  =  retval + address1.ToString() + " ";

            if (address2 != null)
                retval =  retval + address2.ToString() + " ";

            if (city != null)
                retval = retval + city.ToString() + " ";

            if (state != null)
                retval =  retval + state.ToString() + " ";

            if (postcode != null)
                retval = retval + postcode.ToString() + " ";

            if (country != null)
                retval =  retval + country.ToString() + " ";



            return retval;
        }
            protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var sval = ddlSelect.SelectedValue;
                BindEventDetails(sval);
            }
            catch( Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

               

                var pData = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(heventid.Value);
                if (pData != null)
                {
                    string FileName = "Event_" + heventid.Value + "_top.png";
                    pData.SocialDescription = txtDescription.Text.Trim();
                    pData.KeyWords = txtKeywords.Text.Trim();
                    if (fileUploadimage.HasFile)
                    {
                        string folderPath = Server.MapPath("~/WF/UploadData/Events/");
                        fileUploadimage.SaveAs(folderPath + Path.GetFileName(FileName));
                    }

                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(pData);


                }

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }




}