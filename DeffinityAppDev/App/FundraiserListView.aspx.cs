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
using Microsoft.Extensions.Logging;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using iText.Kernel.Geom;

namespace DeffinityAppDev.App
{
    public partial class FaithGivingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FundriaserUsersBind();
                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    //var tithingDetail = pRep.GetAll().Where(o => o.OrganizationID == 0).FirstOrDefault();

                    var tithinglist = pRep.GetAll().Where(o=>o.Title != "").Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                    if(sessionKeys.SID == 3)
                    {
                        var fList = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.Email.ToLower().Trim() == sessionKeys.UEmail.ToLower().Trim()).ToList();

                        tithinglist = tithinglist.Where(o => fList.Select(p => p.FundUNID).Contains(o.unid)).ToList();

                        pnl_user_fundrisers.Visible = true;
                        pnlNodata.Visible = false;
                        pnl_video.Visible = false;
                    }
                    else if(QueryStringValues.Type== "camp")
                    {
                        var fList = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.Email.ToLower().Trim() == sessionKeys.UEmail.ToLower().Trim()).ToList();

                        tithinglist = tithinglist.Where(o => fList.Select(p => p.FundUNID).Contains(o.unid)).ToList();

                        pnl_user_fundrisers.Visible = true;
                        pnlNodata.Visible = false;
                        pnl_video.Visible = false;
                    }
                    else
                    {
                        tithinglist = tithinglist.Where(o => o.MasterUNID == null).ToList();
                        pnl_user_fundrisers.Visible = false;
                    }

                    var alist = tithinglist;


                    //update shotr url
                    try
                    {

                        //update shot description
                        foreach (var eventEntity in alist.Where(o => o.QRcode == null).ToList())
                        {
                            var eDetails = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == eventEntity.ID).FirstOrDefault();
                            if (eventEntity.QRcode == null)
                            {
                                var options = new GenerationOptions(useSpecialCharacters: false);
                                string shortid = ShortId.Generate(options);
                                eDetails.QRcode = shortid;

                                PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(eDetails);


                            }

                            //
                        }

                        //update shot description
                        foreach (var eventEntity in alist.Where(o => o.SocialDescription == null).ToList())
                        {
                            var eDetails = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == eventEntity.ID).FirstOrDefault();

                            if (eventEntity.SocialDescription == null)
                            {
                                var desc = Deffinity.Utility.RemoveHTMLTags(eventEntity.Description == null ? "" : eventEntity.Description).ToString();
                                eDetails.SocialDescription = desc.Length > 300 ? desc.Substring(0, 299) + "..." : desc;
                                PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(eDetails);
                            }

                            //

                            //
                        }
                        foreach (var eventEntity in alist.ToList())
                        {
                            var isExists = File.Exists(Server.MapPath("~/WF/UploadData/Tithing/Fund_" + eventEntity.unid + "_top.png"));
                            if (!isExists)
                            {

                                File.Copy(Server.MapPath("~/WF/UploadData/Tithing/Tithing_org_" + eventEntity.ID + ".png"), Server.MapPath("~/WF/UploadData/Tithing/Fund_" + eventEntity.unid + "_top.png"));
                            }
                        }

                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }


                    //if (tithinglist != null)
                    //{

                    //    //string curr = tithingDetail.Currency;
                    //}
                    try
                    {
                        if (tithinglist.Count > 0)
                        {

                            var vValues = tithinglist.OrderByDescending(o => o.ID).ToList();

                            var rlist = (from r in vValues
                                         select new
                                         {
                                             r.CreatedDateTime,
                                             r.Currency,
                                             r.DefaultBanner,
                                             r.DefaultTarget,
                                             r.DefaultValues,
                                             Description = Deffinity.Utility.RemoveHTMLTags(r.Description == null ? "" : r.Description),
                                             r.EndDate,
                                             r.Event_unid,
                                             r.ID,
                                             r.IsEnable,
                                             r.IsFundraiser,
                                             r.LoggedByID,
                                             r.ModifiedDateTime,
                                             r.OrganizationID,
                                             r.SendMailAfterDonation,
                                             r.ShowChart,
                                             r.ShowQRCode,
                                             r.StartDate,
                                             r.Title,
                                             r.unid,
                                             QRcode = Deffinity.systemdefaults.GetWebUrl() + "/Fundraiser/" + r.QRcode,
                                             r.SocialDescription,
                                             r.SocialKeywords,
                                             r.SocialTitle


                                         }).ToList();

                            ListFaithGiving.DataSource = rlist;


                            ListFaithGiving.DataBind();


                            pnlNodata.Visible = false;
                            pnlFunriserlist.Visible = true;
                        }
                        else
                        {

                            if (sessionKeys.SID == 3)
                            {
                                pnlNodata.Visible = false;
                                pnlFunriserlist.Visible = false;
                                
                            }
                            else if (QueryStringValues.Type == "camp")
                            {
                                pnlNodata.Visible = false;
                                pnlFunriserlist.Visible = false;
                            }
                            else
                            {
                                pnlNodata.Visible = true;
                                pnlFunriserlist.Visible = false;
                            }
                                
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }

                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void FundriaserUsersBind()
        {

            try
            {
               

                var result = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.Email.ToLower().Trim() == sessionKeys.UEmail.ToLower().Trim()).Where(o=>o.Status == PortfolioMgt.BAL.FundriaserUserStatus.Invitation_Sent ).ToList();

                var finallist= PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => result.Select(p => p.MainFundUNID).Contains(o.unid)).ToList();

                var plist = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();

                var InstanceURL = Deffinity.systemdefaults.GetWebUrl();

                list_Users_fundrisers.DataSource = (from r in result
                                                   select new
                                                   {
                                                       Name = r.FirstName + " "+ r.LastName??"",
                                                       r.ContactNo,
                                                       r.Email,
                                                       r.FirstName,
                                                       r.FundUNID,
                                                       r.ID,
                                                       r.InvitationSentOn,
                                                       r.IsAddMember,
                                                       r.IsDeleted,
                                                       r.IsInvitationSent,
                                                       r.LastName,
                                                       r.MainFundUNID,
                                                       MainUnidName = finallist.Where(o => o.unid == r.MainFundUNID).FirstOrDefault()?.Title ?? "",
                                                       r.ShortCode,
                                                       r.Status,
                                                       FundUrl = InstanceURL + "/FundraiserView.aspx?unid="+ r.MainFundUNID+"&type=b",
                                                       CompanyName = GetCompanyName(plist, finallist.Where(o=>o.unid == r.MainFundUNID).FirstOrDefault())
                                                   } ).ToList();
                list_Users_fundrisers.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetCompanyName(List<ProjectPortfolio> plist, PortfolioMgt.Entity.TithingDefaultDetail tdata )
        {
            string retval = "";

            if (tdata != null)

            {
                if ((tdata.OrganizationID ?? 0) > 0)
                {

                    retval = (plist.Where(o => o.ID == (tdata.OrganizationID ?? 0)).FirstOrDefault()?.PortFolio);
                }
            }


            return retval;
        }
        protected void list_Users_fundrisers_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string v = e.CommandArgument.ToString();
                var f = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(v)).FirstOrDefault();
                if(e.CommandName == "inv")
                {
                    Response.Redirect(string.Format("~/App/Fundraiser/MyFunraiser.aspx?fuserid={0}&munid={1}", f.ID,f.MainFundUNID), false);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private static string GetMasterID(string id)
        {
            string retId = "";
            if (sessionKeys.SID == 3)
            {
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
                }
            }
            else
            {
                retId = id;
            }

            return retId;

        }
            protected static string GetImageUrl(string contactsId)
        {

            //return GetImageUrl(a_gId, a_oThumbSize, true);
            //bool isOriginal = false;

            //string img = string.Empty;
            //contactsId = GetMasterID(contactsId);



            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    //if (isOriginal)
            //    //    img = string.Format("~/WF/UploadData/Tithing/Tithing_{0}.png", contactsId.ToString());
            //    //else
            //    img = string.Format("../../WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "../../WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //    //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            return "../../ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_fundriser; //"img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        }


        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                    string v = e.CommandArgument.ToString();
                    if (sessionKeys.SID == 3)
                    {
                        var d = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(v)).FirstOrDefault();
                        if (d != null)
                        {
                            Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + d.unid, false);
                        }
                    }
                    else
                        Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" + v, false);
                }
                else if (e.CommandName == "del")
                {
                    string v = e.CommandArgument.ToString();
                    //  var eventEntity = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(v)).FirstOrDefault();

                    PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Delete(Convert.ToInt32(v));
                    sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                    Response.Redirect(Request.RawUrl, false);
                }
                else if (e.CommandName == "social")
                {
                    var id = e.CommandArgument.ToString();
                    var eventEntity = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    // var eventEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                    if (eventEntity != null)
                    {
                        heventid.Value = eventEntity.ID.ToString();

                        txtDescription.Text = eventEntity.SocialDescription;
                        imgTop.ImageUrl = "~/ImageHandler.ashx?id=" + eventEntity.ID + "&s=" + ImageManager.file_section_fundriser_top;// GetImageUrl(tithingDetail.ID.ToString());
                        //if (File.Exists(Server.MapPath("~/WF/UploadData/Tithing/Fund_" + eventEntity.unid + "_top.png")))

                        //{
                        //    imgTop.Visible = true;
                        //    imgTop.ImageUrl = "~/WF/UploadData/Tithing/Fund_" + eventEntity.unid + "_top.png";
                        //}
                        txtKeywords.Text = eventEntity.SocialKeywords;
                        mdlSocialSettings.Show();
                    }

                }
                else if (e.CommandName == "socialshare")
                {
                    var id = e.CommandArgument.ToString();

                    var eventEntity = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (eventEntity != null)
                    {
                        txtUrl.Text = Deffinity.systemdefaults.GetWebUrl() + "/Fundraiser/" + eventEntity.QRcode;

                        mdlSocial.Show();
                    }
                }
                else if (e.CommandName == "QR")
                {
                    var id = e.CommandArgument.ToString();

                    var eventEntity = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                    var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(eventEntity.OrganizationID.Value);

                    string code = Deffinity.systemdefaults.GetWebUrl() + "/FundraiserView.aspx?unid=" + eventEntity.unid; //ab.MoreDetails;

                    var filepath = "~/WF/UploadData/Tithing/" + eventEntity.unid + ".png";
                    var filepathpdf = "~/WF/UploadData/Tithing/" + eventEntity.unid + ".pdf";

                    // var filepath = Server.MapPath("~/WF/UploadData/Events/") + id + ".png";
                    //if (File.Exists(Server.MapPath(filepath)))
                    //{
                  
                    // }

                    var pdfpath = Server.MapPath(filepathpdf);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream)))
                        {
                            // Create a document
                            using (Document document = new Document(pdfDocument, PageSize.A4))
                            {
                             
                                Paragraph header = new Paragraph(orgEntity.PortFolio)
                                   .SetTextAlignment(TextAlignment.CENTER)
                                   .SetFontSize(24).SetBold();


                                document.Add(header);

                                Paragraph subheader1 = new Paragraph(eventEntity.Title)
                          .SetTextAlignment(TextAlignment.CENTER)
                          .SetFontSize(24);
                                document.Add(subheader1);
                               
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
                                      
                                        iText.IO.Image.ImageData imageData = ImageDataFactory.Create(byteImage);

                                        // Create an Image object from the ImageData
                                        Image image = new Image(imageData);

                                        // Set the height of the image
                                        image.SetHeight(500);

                                        // Center-align the image horizontally
                                        image.SetHorizontalAlignment(HorizontalAlignment.CENTER);


                                        document.Add(image);
                                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    }
                                    // plBarCode.Controls.Add(imgBarCode);
                                }


                                Paragraph subheader = new Paragraph("SCAN TO FUNDRAISE")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(35);
                                document.Add(subheader);

                                document.Close();

                               // Response.Redirect("~/App/downloadpdf.ashx?type=fund&unid=" + eventEntity.unid, false);
                            }
                        }

                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "application/pdf";
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename="+ eventEntity.Title.Replace(" ", "_") + ".pdf");
                        HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }

                }
                else
                {
                    string v = e.CommandArgument.ToString();
                    var eventEntity = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(v)).FirstOrDefault();

                    Response.Redirect("~/FundraiserView.aspx?unid=" + eventEntity.unid, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(heventid.Value);

                var pData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o=>o.ID == id).FirstOrDefault();
                if (pData != null)
                {
                    string FileName = "Fund_" + pData.unid + "_top.png";
                    pData.SocialDescription = txtDescription.Text.Trim();
                    pData.SocialKeywords = txtKeywords.Text.Trim();
                    //if (fileUploadimage.HasFile)
                    //{
                    //    string folderPath = Server.MapPath("~/WF/UploadData/Tithing/");
                    //    fileUploadimage.SaveAs(folderPath + Path.GetFileName(FileName));
                    //}

                    if (fileUploadimage.PostedFile.FileName.Length > 0)
                    {
                        Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileUploadimage.PostedFile.InputStream);
                        ImageManager.SaveTithingTopImage_setpath(fileUploadimage.FileBytes, id.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                        // DisplayLogo();
                    }

                    PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(pData);


                }

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected static string GetDescription(object description)
        {
            string retval = "";
            if (description != null)
            {

                if (description.ToString().Length > 500)
                {
                    retval = description.ToString().Substring(0, 490) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        protected void link_addnew_Click(object sender, EventArgs e)
        {
            if(sessionKeys.SID == UserMgt.BAL.UserType.Fundraiser)
            {
                Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main", false);
            }
            else
            {
                Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main", false);
            }
        }
    }
}