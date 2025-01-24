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
using PortfolioMgt.DAL;
using TuesPechkin;
using Stripe;

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


        private string getDisplay(string id)
        {
            using (var context = new PortfolioDataContext())
            {
                var detail = context.ActivityDetails.FirstOrDefault(o => o.unid == id);
                return detail.isInPerson??true ? "none" : "";
             
            }
        }

        private void BindEventDetails(string sval)
        {
            try
            {
                using(var context=new PortfolioDataContext())
                {
                var viewOptions = GetViewOptions(sessionKeys.PortfolioID);
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
                                  display=getDisplay(a.unid),
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
                                  WordpressCode=GetEventEmbedCode(GetImageofEvent(a.unid),a.Title,Deffinity.Utility.RemoveHTMLTags(a.Description??""),a.unid,viewOptions),
                                  ListWPCode=GetEventEmbedListCode(GetImageofEvent(a.unid), a.Title??"",Deffinity.Utility.RemoveHTMLTags(a.Description??""), a.unid??"",a.StartDateTime, viewOptions),
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

                    string allEmbedHtml = "<div id='PlegitWordpressEmbed' data-show-paging='{{showPaging}}' data-paging-value='{{pageValue}}' style=\"display: grid; grid-template-columns: repeat(auto-fit, minmax(300px, 1fr)); gap: 20px; padding: 20px; box-sizing: border-box; height: {{height}}; width: {{width}};\">";
                    foreach(var activity in retval)
                    {
                        allEmbedHtml += activity.WordpressCode;
                    }
                    allEmbedHtml += "</div>";
                    hfAllEmbedCode.Value = HttpUtility.JavaScriptStringEncode(allEmbedHtml);
                    BasehfAllEmbedCode.Value = HttpUtility.JavaScriptStringEncode(allEmbedHtml);



                    string allEmbedListHtml = "<div id='PlegitWordpressEmbed' data-show-paging='{{showPaging}}' data-paging-value='{{pageValue}}' style='border: 1px solid #ddd; box-sizing: border-box;height:{{height}};width:{{width}}'>";
                    allEmbedListHtml += $@"
                    <div id=""listHeader"" style=""background-color: {viewOptions.ListHeaderBackgroundColour}; color: {viewOptions.ListHeaderFontColour}; padding: 10px 15px; display: flex; justify-content: space-between; align-items: center; font-size: {viewOptions.ListHeaderFontSize}; font-weight: bold;"">
                        <span>{DateTime.Now.ToShortDateString()}</span>
                     
  <input type=""date"" id=""headerDatePicker"" onchange=""filterEventsByDate(this)"" style=""background:transparent;border:none;font-size:30px;color:transparent"" />                    </div>
                    ";

                    foreach (var activity in retval)
                    {
                        allEmbedListHtml += activity.ListWPCode;
                    }
                    allEmbedListHtml += @"</div><script>
function filterEventsByDate(input) {
    const selectedDate = input.value; // Get selected date in YYYY-MM-DD format
      
    const formattedDate = selectedDate.split(""-"").reverse().join(""/""); // Convert to DD/MM/YYYY format
    const events = document.querySelectorAll("".event"");
if (!selectedDate) {
        // If no date is selected, display all events
        events.forEach(event => {
            event.style.display = ""flex""; // Show all events
        });
        return;
    }

    events.forEach(event => {
        const eventDate = event.getAttribute(""list-data-start-time"")?.split("" "")[0]; // Extract the date part
        if (eventDate === formattedDate) {
            event.style.display = ""flex""; // Show matching events
        } else {
            event.style.display = ""none""; // Hide non-matching events
        }
    });
}
</script>";
                    hfAllEmbedListCode.Value = HttpUtility.JavaScriptStringEncode(allEmbedListHtml);
                    BasehfAllEmbedListCode.Value = HttpUtility.JavaScriptStringEncode(allEmbedListHtml);



                }
                //}

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private string GetEventEmbedListCode(string img, string title, string description, string unid,DateTime startTime, ViewOption viewoption)
        {
            string link = GetDonationUrl(unid);
            string Desc = description.Length > 100 ? description.Substring(0, 100) + "..." : description;
            string embedCode = $@"
       <div onclick=""window.location.href = '{link}'"" list-data-start-time=""{startTime}"" class=""event"" list-data-end-time=""12:30AM"" style=""display: flex; border-bottom: 1px solid #ddd; background-color: #FFFFFF;"">
    <!-- Time Section -->
    <div style=""background-color: {viewoption.ListTimeSlotBackgroundColour}; color: {viewoption.ListTimeSlotFontColour}; width: 100px; text-align: center; padding: 10px; font-size: {viewoption.ListTimeSlotFontSize}"">
      <div style=""font-size: 18px; font-weight: bold;"">{startTime.ToString()}</div>
    </div>
    <!-- Content Section -->
    <div style=""flex: 1; padding: 10px; box-sizing: border-box;"">
      <div style=""font-size: {viewoption.ListEventTitleSize}; font-weight: bold; color: {viewoption.ListEventTitleColour};"">{title}</div>
      <div style=""font-size: {viewoption.ListEventSubjectFontSize}; color: {viewoption.ListEventSubjectColour}; margin-top: 5px;"">
       {Desc}
      </div>
    </div>
  </div>
";
            return embedCode;
        }


        private string GetEventEmbedCode(string img,string title,string description,string unid,ViewOption viewoption)
        {

            string Desc=description;
            string embedCode = $@"
                <div style='border: 1px solid #ddd; padding: 20px; border-radius: 10px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);'>
                    <img src='{img}' alt='{title}' style=""width: 100%; height: calc({{{{height}}}} * 0.64); border-radius: 10px;"" />
                    <h4 style='color: #333; text-align: center;'>{title}</h4>
<p style='color: #555; text-align: center;'>
    {(Desc?.Length > 100 ? Desc?.Substring(0, 100) + "..." : Desc ?? "")}
</p>                    <a href='{GetDonationUrl(unid)}' style='display: block; text-align: center; padding: 10px 20px; background-color: #50CD89; color: #fff; text-decoration: none; border-radius: 5px;'>View Event</a>
                </div>";
            return embedCode;
        }

        private string GetDonationUrl(string unid)
        {
            return $"{Request.Url.Scheme}://{Request.Url.Authority}/EventDetailsNew.aspx?unid={unid}";

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
            if (System.IO.File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid + "/0.png")))
            {
                retval = "<div class='bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px' style='background-image:url('"+"../../WF/UploadData/Events/" + activityid + "/0.png"+"?v="+DateTime.Now.Ticks+"')' ></div> ";
            }
            else
            {

                retval = "";
            }
            return retval;

        }

        public ViewOption GetViewOptions(int portfolioId)
        {
            // Default display options for List View
            var defaultViewOption = new ViewOption
            {
                ListHeaderBackgroundColour = "#FFFFFF",
                ListHeaderFontSize = 30+"px",
                ListHeaderFontColour = "#252F4A",

                ListTimeSlotBackgroundColour = "#DAF6E4",
                ListTimeSlotFontColour = "#252F4A",
                ListTimeSlotFontSize = 20 + "px",

                ListEventTitleColour = "#252F4A",
                ListEventTitleSize = 16 + "px",

                ListEventSubjectColour = "#98A0B7",
                ListEventSubjectFontSize = 12 + "px",

                ListEventPanelBackgroundColour = "#FFFFFF",
                ListDatePickerColour = "#9CA4BA",

                PanelBookTicketsButtonColour = "#1E2129",
                PanelBookTicketsButtonFontColour = "#FFFFFF",
                PanelViewLiveButtonColour = "#17C653",
                PanelViewLiveButtonFontColour = "#FFFFFF"
            };

            using (var context = new PortfolioDataContext())
            {
                var viewOptions = context.ViewOptions.FirstOrDefault(o => o.PortfolioID == portfolioId);
                if (viewOptions != null)
                {
                    return new ViewOption
                    {
                        ListHeaderBackgroundColour = viewOptions.ListHeaderBackgroundColour ?? defaultViewOption.ListHeaderBackgroundColour,
                        ListHeaderFontSize = viewOptions.ListHeaderFontSize ?? defaultViewOption.ListHeaderFontSize+"px",
                        ListHeaderFontColour = viewOptions.ListHeaderFontColour ?? defaultViewOption.ListHeaderFontColour,

                        ListTimeSlotBackgroundColour = viewOptions.ListTimeSlotBackgroundColour ?? defaultViewOption.ListTimeSlotBackgroundColour,
                        ListTimeSlotFontColour = viewOptions.ListTimeSlotFontColour ?? defaultViewOption.ListTimeSlotFontColour,
                        ListTimeSlotFontSize = viewOptions.ListTimeSlotFontSize ?? defaultViewOption.ListTimeSlotFontSize + "px",

                        ListEventTitleColour = viewOptions.ListEventTitleColour ?? defaultViewOption.ListEventTitleColour,
                        ListEventTitleSize = viewOptions.ListEventTitleSize ?? defaultViewOption.ListEventTitleSize + "px",

                        ListEventSubjectColour = viewOptions.ListEventSubjectColour ?? defaultViewOption.ListEventSubjectColour,
                        ListEventSubjectFontSize = viewOptions.ListEventSubjectFontSize ?? defaultViewOption.ListEventSubjectFontSize + "px",

                        ListEventPanelBackgroundColour = viewOptions.ListEventPanelBackgroundColour ?? defaultViewOption.ListEventPanelBackgroundColour,
                        ListDatePickerColour = viewOptions.ListDatePickerColour ?? defaultViewOption.ListDatePickerColour,

                        PanelBookTicketsButtonColour = viewOptions.PanelBookTicketsButtonColour ?? defaultViewOption.PanelBookTicketsButtonColour,
                        PanelBookTicketsButtonFontColour = viewOptions.PanelBookTicketsButtonFontColour ?? defaultViewOption.PanelBookTicketsButtonFontColour,
                        PanelViewLiveButtonColour = viewOptions.PanelViewLiveButtonColour ?? defaultViewOption.PanelViewLiveButtonColour,
                        PanelViewLiveButtonFontColour = viewOptions.PanelViewLiveButtonFontColour ?? defaultViewOption.PanelViewLiveButtonFontColour
                    };
                }
            }

            return defaultViewOption;
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
            else if (e.CommandName == "reminder")
            {
                var id = e.CommandArgument.ToString();

                Response.Redirect("~/App/Events/EventReminders.aspx?unid=" + id, false);
            }
            else if (e.CommandName == "golive")
            {
                var id = e.CommandArgument.ToString();

                Response.Redirect("~/LiveEvent.aspx?unid=" + id, false);
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
                    if (System.IO.File.Exists(Server.MapPath("~/WF/UploadData/Events/Event_" + heventid.Value + "_top.png")))

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

        private string GetImageofEvent(string unid)
        {
            return $"https://dev.plegit.ai/imagehandler.ashx?id={unid}&s=event";
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