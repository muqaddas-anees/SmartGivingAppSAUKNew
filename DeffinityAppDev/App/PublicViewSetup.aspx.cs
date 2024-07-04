using AngleSharp.Dom;
using iText;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static QRCoder.SvgQRCode;
using Document = iText.Layout.Document;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using Image = iText.Layout.Element.Image;

namespace DeffinityAppDev.App
{
    public partial class PublicViewSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var pval = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (pval != null)
                    {
                        if(pval.OrgUniqID != null)
                        {
                            btnView.NavigateUrl = "~/Web/" + pval.OrgUniqID.ToString();
                            myInput.Value = Deffinity.systemdefaults.GetWebUrl()+ "/Web/" + pval.OrgUniqID.ToString();
                        }
                        else
                        btnView.NavigateUrl = "~/OrgHomeNew.aspx?orgguid=" + pval.OrgarnizationGUID.ToString();
                        btnBuilder.NavigateUrl = "~/WF/PageBuilder.aspx?type=PageBuilder";
                        txtDescription.Text = pval.Description;

                        imgTop.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_landing_top;
                        ////if (File.Exists(Server.MapPath("~/WF/UploadData/Customers/Portfolio_" + sessionKeys.PortfolioID + "_top.png")))

                        ////{
                        ////    imgTop.Visible = true;
                        ////    imgTop.ImageUrl = "~/WF/UploadData/Customers/Portfolio_"+sessionKeys.PortfolioID+"_top.png";
                        ////}
                        txtKeywords.Text = pval.StrategicFit;

                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnGetQRCOde_Click(object sender, EventArgs e)
        {
            try
            {
                var pval = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();

                // string code = Deffinity.systemdefaults.GetWebUrl() + "/Web/" + pval.OrgUniqID.ToString(); //ab.MoreDetails;
                string code = Deffinity.systemdefaults.GetWebUrl() + "/OrgHomeNewV2.aspx?h="+DateTime.Now.Ticks+"&orgguid=" + pval.OrgarnizationGUID.ToString(); //ab.MoreDetails;
                var filepathpdf ="~/WF/UploadData/Customers/" + pval.OrgarnizationGUID + ".pdf";

                //if (!File.Exists(Server.MapPath("~/WF/UploadData/Customers/") + pval.OrgarnizationGUID + ".png"))
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
                            img1.Save(Server.MapPath("~/WF/UploadData/Customers/") + pval.OrgarnizationGUID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            // imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        // plBarCode.Controls.Add(imgBarCode);
                    }
               // }
                    var pdfpath = Server.MapPath(filepathpdf);

                    PdfWriter writer = new PdfWriter(pdfpath);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);
                    Paragraph header = new Paragraph(pval.PortFolio)
                       .SetTextAlignment(TextAlignment.CENTER)
                       .SetFontSize(24).SetBold();


                    document.Add(header);

             

                    Image img = new Image(ImageDataFactory
               .Create(Server.MapPath("~/WF/UploadData/Customers/") + pval.OrgarnizationGUID + ".png"))
               .SetTextAlignment(TextAlignment.CENTER)
               .SetHeight(500)
               .SetHorizontalAlignment(HorizontalAlignment.CENTER)

               ;

                    document.Add(img);

                    Paragraph subheader = new Paragraph("SCAN TO DONATE")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(35);
                    document.Add(subheader);

                    document.Close();

                    Response.Redirect("~/App/downloadpdf.ashx?unid=" + pval.OrgarnizationGUID, false);
                

               // Response.Redirect("~/OrgQRCode.aspx?unid="+ pval.OrgarnizationGUID.ToString(), false);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSoicialShare_Click(object sender, EventArgs e)
        {
            mdlAddSpeaker.Show();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                var pData = pRep.GetAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                if(pData != null)
                {
                    string FileName = "portfolio_" + pData .ID.ToString()+ "_top.png";
                    pData.Description = txtDescription.Text.Trim();
                    pData.StrategicFit = txtKeywords.Text.Trim();
                    //if ( fileUploadimage.HasFile)
                    //{
                    //    string folderPath = Server.MapPath("~/WF/UploadData/Customers/");
                    //    fileUploadimage.SaveAs(folderPath + Path.GetFileName(FileName));
                    //}


                  

                    pRep.Edit(pData);
                    try
                    {
                        if (fileUploadimage.PostedFile.FileName.Length > 0)
                        {
                            Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileUploadimage.PostedFile.InputStream);
                            ImageManager.SaveLadndingPageTopImage_setpath(fileUploadimage.FileBytes, sessionKeys.PortfolioID.ToString());


                            Deffinity.PortfolioManager.Portfilio.UpdateLogo(sessionKeys.PortfolioID);
                            imgTop.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_landing_top;
                            //DisplayLogo();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                }

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                Response.Redirect(Request.RawUrl,false);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSoicialShareDefault_Click(object sender, EventArgs e)
        {
            try
            {
                var pval = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();

                if (pval != null)
                {
                    txtUrl.Text = Deffinity.systemdefaults.GetWebUrl() + "/Web/" + pval.OrgUniqID;

                    mdlSocial.Show();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //Visit Your Web Page
    }
}