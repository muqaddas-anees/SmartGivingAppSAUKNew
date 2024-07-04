using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class EmailSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try {


                   
                    BindTags();
                    setData();
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

            }
        }

        private void setData()
        {
            IPortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate> eREP = new PortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate>();

            var f = eREP.GetAll().FirstOrDefault();
            if (f != null)
            {
                txtEmailSubject.Text= f.EmailSubject;
                 txtEmailContent.Text= f.EMailBody ;
                txtButton1URL.Text= f.Button1Link;
                txtButton1Text.Text = f.Button1Text;
                txtButton2URL.Text = f.Button2Link;
                txtButton2Text.Text = f.Button2Text;
                txtEmailSinature.Text =  f.SignatureText;
f.EmailBannerLink = "";

                imgbanner.ImageUrl = "~/ImageHandler.ashx?id=" + "0" + "&s=" + ImageManager.file_section_email_banner;
                Gridbind(0);
            }

        }


        
        private void uploadImage(int portfolioid)
        {
            try

            {

                if (imgFile.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgFile.PostedFile.InputStream);
                    ImageManager.SaveEmailBannerImage_setpath(imgFile.FileBytes, "0");
                    // DisplayLogo();
                }

                //using (Stream fs = imgFile.PostedFile.InputStream)
                //{
                //    using (BinaryReader br = new BinaryReader(fs))
                //    {
                //        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //        ImageManager.FileDBSave(bytes, null, portfolioid.ToString(), ImageManager.file_section_email_banner, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);

                //    }
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void uploadAttachfiles(int portfolioid)
        {
            try

            {



                using (Stream fs = fileattachment.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        ImageManager.FileDBSave(bytes, null, "0", ImageManager.file_section_email_attach, System.IO.Path.GetExtension(fileattachment.PostedFile.FileName).ToLower(), fileattachment.PostedFile.FileName, fileattachment.PostedFile.ContentType);

                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void AddUpdateData()
        {

            IPortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate> eREP = new PortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate>();

            var f = eREP.GetAll().FirstOrDefault();
            if (f == null)
            {
                f = new PortfolioMgt.Entity.tblEmailTemplate();

                f.EmailSubject = txtEmailSubject.Text.Trim();
                f.EMailBody = txtEmailContent.Text.Trim();
                f.Button1Link = txtButton1URL.Text.Trim();
                f.Button1Text = txtButton1Text.Text.Trim();
                f.Button2Link = txtButton2URL.Text.Trim();
                f.Button2Text = txtButton2Text.Text.Trim();
                f.SignatureText = txtEmailSinature.Text.Trim();
                f.EmailBannerLink = "";


                eREP.Add(f);
            }
            else
            {
                f.EmailSubject = txtEmailSubject.Text.Trim();
                f.EMailBody = txtEmailContent.Text.Trim();
                f.Button1Link = txtButton1URL.Text.Trim();
                f.Button1Text = txtButton1Text.Text.Trim();
                f.Button2Link = txtButton2URL.Text.Trim();
                f.Button2Text = txtButton2Text.Text.Trim();
                f.SignatureText = txtEmailSinature.Text.Trim();
                f.EmailBannerLink = "";


                eREP.Edit(f);
            }

            uploadImage(0);
            DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "Ok");
        }


        public void Gridbind(int SID)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_email_attach).Where(o => o.FileID == SID.ToString()).ToList();

                var rList = (from r in fList
                             select new
                             {
                                 Value = r.FileID,
                                 Text = r.FileName,

                             }).ToList();
                gridfiles.DataSource = rList;
                gridfiles.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                // File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var f = fRep.GetAll().Where(o => o.FileID == filePath && o.Section == ImageManager.file_section_portfolio_doc).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id=" + filePath + "&s=" + ImageManager.file_section_portfolio_doc);
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUploa_Click(object sender, EventArgs e)
        {
            try
            {
                uploadAttachfiles(0);
                Gridbind(0);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            try
            {
                AddUpdateData();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public class Tags
        {
            public string text { set; get; }
            public string value { set; get; }
        }
        private void BindTags()
        {
            List<Tags> tList = new List<Tags>();
            tList.Add(new Tags() { text = " Select personalize tags ...", value = "" });
            //tList.Add(new Tags() { text = "Logo", value = "Logo" });
            tList.Add(new Tags() { text = "Organization", value = "Organization" });
            tList.Add(new Tags() { text = "Button1", value = "Button1" });
            tList.Add(new Tags() { text = "Button2", value = "Button2" });

            tList.Add(new Tags() { text = "Instance Name", value = "Instance Name" });
            //Instance Name


            //tList.Add(new Tags() { text = "Logo", value = "Logo" });
            //tList.Add(new Tags() { text = "Logo", value = "Logo" });

            ddlType.DataSource = tList.OrderBy(o => o.text).ToList();
            ddlType.DataTextField = "text";
            ddlType.DataValueField = "value";
            ddlType.DataBind();

           
            // return tList;

        }

        protected void btnDefaultTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                var contents = string.Empty;
                var FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/App/emailtemplates/default.htm");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }

                if (contents.Length > 0)
                {
                    txtEmailContent.Text = contents;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}