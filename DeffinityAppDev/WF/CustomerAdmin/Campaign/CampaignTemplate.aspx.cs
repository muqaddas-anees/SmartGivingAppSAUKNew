using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlType.DataSource = DeffinityManager.TagsBAL.GetTemplageTags().OrderBy(o => o.Value).ToList();
                ddlType.DataTextField = "Value";
                ddlType.DataValueField = "ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("Please select...", ""));


                if (!string.IsNullOrEmpty( sessionKeys.Message))
                {
                    lblMsgNew.Text = sessionKeys.Message;
                    sessionKeys.Message = string.Empty;
                }
                if(QueryStringValues.CTID >0)
                {
                    LblHeader.Text = "Edit Email Template";
                    controlsbind(QueryStringValues.CTID);
                    Gridbind(QueryStringValues.CTID);
                }
                else
                {
                    LblHeader.Text = "Create Email Template";
                    MdlPopUp.Show();
                }
            }
        }



        public void controlsbind(int id)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var mail = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                if (mail != null)
                {
                    lblTemplateName.Text = mail.TemplateName;
                    txtsubject.Text = mail.Subject;
                    CKEditor1.Text = HttpUtility.HtmlDecode(mail.TemplateContent);
                    // txtnotes.Text = mail.Notes;
                    //BindCategoryddl(mail.CategoryId.HasValue ? mail.CategoryId.Value : 0);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void Gridbind(int SID)
        {
            try
            {
                //var folderpath = Server.MapPath("~/WF/UploadData/Campaign/Attachments/TemplateID" + SID);
                //if (System.IO.Directory.Exists(folderpath))
                //{
                //    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Campaign/Attachments/TemplateID" + SID));
                //    List<ListItem> files = new List<ListItem>();
                //    foreach (string filePath in filePaths)
                //    {
                //        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                //    }

                //    gridfiles.DataSource = files;
                //    gridfiles.DataBind();
                //}

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_messages).Where(o => o.FileID == SID.ToString()).ToList();

                var rList = (from r in fList
                             select new
                             {
                                 Value = r.ID,
                                 Text = r.FileName
                             }).ToList();
                gridfiles.DataSource = rList;
                gridfiles.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
               // File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var f = fRep.GetAll().Where(o => o.ID == Convert.ToInt32( filePath)).FirstOrDefault();
                if(f != null)
                {
                    fRep.Delete(f);
                }

                Response.Redirect(Request.Url.AbsoluteUri);
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

                var f = fRep.GetAll().Where(o => o.ID == Convert.ToInt32(filePath)).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id="+filePath+"&s="+ImageManager.file_section_messages);
                }

              
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void FileuploadMethod(string folder)
        {
            try
            {
                if (fileupload.HasFile)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];
                        string fileName = Path.GetFileName(userPostedFile.FileName);

                        if (!System.IO.Directory.Exists(folder))
                        {
                            System.IO.Directory.CreateDirectory(folder);
                            userPostedFile.SaveAs(folder + "\\" + fileName);
                        }
                        else
                        {
                            userPostedFile.SaveAs(folder + "\\" + fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       

      

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString["Type"] == null)
            //{
            //    Response.Redirect("TemplatesList.aspx", false);
            //}
            //else
            //{
            //    Response.Redirect("TemplatesList.aspx?Type=OneclickButton", false);
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                string subject = txtsubject.Text.Trim();
                string MailTemplatePath = "";
                string TemplateContent = HttpUtility.HtmlEncode(CKEditor1.Text.Trim());
                
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var tmp = cRep.GetAll().Where(o => o.ID == QueryStringValues.CTID).FirstOrDefault();

                if (tmp != null)
                {
                    tmp.ModifiedBy = sessionKeys.UID;
                    tmp.ModifiedDate = DateTime.Now;
                    tmp.Subject = txtsubject.Text.Trim();
                    tmp.TemplateContent =  CKEditor1.Text;
                    cRep.Edit(tmp);
                    //tmp.

                    var folder = Server.MapPath("~/WF/UploadData/Campaign/Attachments/TemplateID" + tmp.ID);
                    FileuploadMethod(folder);
                    uploadImage(tmp.ID);

                    //Response.Redirect("CampaignTemplateTags.aspx?CTID="+QueryStringValues.CTID);
                    if (QueryStringValues.CSID > 0)
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateTags.aspx?CTID={0}&CSID={1}", +QueryStringValues.CTID, QueryStringValues.CSID), false);
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateTags.aspx?CTID={0}", +QueryStringValues.CTID), false);
                    }
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //protected void btnback_Click(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["Type"] == null)
        //    {
        //        Response.Redirect("TemplatesList.aspx", false);
        //    }
        //    else if (Request.QueryString["Type"] == "email")
        //    {
        //        Response.Redirect("EmailScheduler.aspx", false);
        //    }
        //    else
        //    {
        //        Response.Redirect("TemplatesList.aspx?Type=OneclickButton", false);
        //    }
        //}

        protected void BtnAddNewCategory_Click(object sender, EventArgs e)
        {
            try
            {
                LblHeader.Text = "Add Email Template";
                TxtCName.Text = string.Empty;
                //BtnEditInCat.Visible = false;
                BtnAddCat.Visible = true;
                // MdlPopUp.Show();

                Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx", false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void BtnEditCategory_Click(object sender, EventArgs e)
        {
            try
            {

                if (QueryStringValues.CTID > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                    var m = cRep.GetAll().Where(o => o.ID == QueryStringValues.CTID).FirstOrDefault();
                    if (m != null)
                    {
                        TxtCName.Text = m.TemplateName;
                        MdlPopUp.Show();
                    }
                        
                }
                //if (int.Parse(ddlCategory.SelectedValue) > 0)
                //{
                //    using (DataClasses1DataContext Dc = new DataClasses1DataContext())
                //    {
                //        LblHeader.Text = "Edit Category";
                //        TxtCName.Text = Dc.TemplateCategories.
                //            Where(a => a.Id == int.Parse(ddlCategory.SelectedValue)).Select(a => a.CName).FirstOrDefault();
                //        BtnAddCat.Visible = false;
                //        BtnEditInCat.Visible = true;
                //        MdlPopUp.Show();
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void BtnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (QueryStringValues.CTID > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                    var m = cRep.GetAll().Where(o => o.ID == QueryStringValues.CTID).FirstOrDefault();
                    if (m != null)
                    {
                        cRep.Delete(m);

                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                        Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignList.aspx", false);
                    }
                }
                //using (DataClasses1DataContext Dc = new DataClasses1DataContext())
                //{
                //    if (Dc.MailTemplatesTbls.Where(x => x.CategoryId == int.Parse(ddlCategory.SelectedValue)).Count() == 0)
                //    {
                //        TemplateCategory t = Dc.TemplateCategories.Where(a => a.Id == int.Parse(ddlCategory.SelectedValue)).FirstOrDefault();
                //        Dc.TemplateCategories.DeleteOnSubmit(t);
                //        Dc.SubmitChanges();
                //        lblmsg.ForeColor = System.Drawing.Color.Green;
                //        lblmsg.Text = "Deleted successfully";
                //    }
                //    else
                //    {
                //        lblmsg.ForeColor = System.Drawing.Color.Red;
                //        lblmsg.Text = "You can't delete this record";
                //    }
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                TxtCName.Text = string.Empty;
                if (QueryStringValues.CTID > 0)
                    MdlPopUp.Hide();
                else
                    MdlPopUp.Show();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        

        protected void BtnAddCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtCName.Text.Trim()))
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();

                    if (QueryStringValues.CTID == 0)
                    {
                        var m = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.TemplateName.ToLower() == TxtCName.Text.Trim().ToLower()).FirstOrDefault();
                        if (m == null)
                        {
                            m = new PortfolioMgt.Entity.CampaignTemplate();
                            m.LoggedBy = sessionKeys.UID;
                            m.LoggedDate = DateTime.Now;
                            m.ModifiedBy = m.LoggedBy;
                            m.ModifiedDate = m.ModifiedDate;
                            m.PortfolioID = sessionKeys.PortfolioID;
                            m.TemplateName = TxtCName.Text.Trim();
                            cRep.Add(m);
                            sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                            Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID=" + m.ID, false);
                        }
                        else
                        {
                            lblpoperror.Text = "Please email template name already exists";
                            MdlPopUp.Show();
                        }
                    }
                    else
                    {
                        var m = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.ID == QueryStringValues.CTID ).FirstOrDefault();
                        if (m == null)
                        {
                           
                            m.ModifiedBy = sessionKeys.UID;
                            m.ModifiedDate = DateTime.Now;
                           
                            m.TemplateName = TxtCName.Text.Trim();
                            cRep.Edit(m);
                            sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                            Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID=" + m.ID, false);
                        }
                        else
                        {
                            lblpoperror.Text = "Please email template name already exists";
                            MdlPopUp.Show();
                        }
                    }

                   
                }
                else
                {

                    lblpoperror.Text = "Please enter email template name";
                    MdlPopUp.Show();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["callid"] != null)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                    var m = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.TemplateName.ToLower() == TxtCName.Text.Trim().ToLower()).FirstOrDefault();
                    if (m != null)
                    {
                        cRep.Delete(m);
                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                        Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignList.aspx", false);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            //save the current template to new template
            //get current template details
            try
            {
                var cTemp = CampaignTemplateBAL.CampaignTemplate_SelectByID(QueryStringValues.CTID);
                if (cTemp != null)
                {
                    //create new template
                    var nTemp = new PortfolioMgt.Entity.CampaignTemplate();
                    nTemp.LoggedBy = sessionKeys.UID;
                    nTemp.LoggedDate = DateTime.Now;
                    nTemp.ModifiedBy = sessionKeys.UID;
                    nTemp.ModifiedDate = DateTime.Now;
                    nTemp.PortfolioID = cTemp.PortfolioID;
                    nTemp.ScheduledEndDate = cTemp.ScheduledEndDate;
                    nTemp.ScheduledStartDate = cTemp.ScheduledStartDate;
                    nTemp.Subject = txtsubject.Text;
                    nTemp.Tags = cTemp.Tags;
                    nTemp.TemplateContent = CKEditor1.Text;
                    nTemp.TemplateName = cTemp.TemplateName + "_New";
                    CampaignTemplateBAL.CampaignTemplate_Add(nTemp);

                    //if navigation come from scheduler
                    //then need to update scheduler
                    if (QueryStringValues.CSID > 0)
                    {
                        //new template id =nTemp;
                        var csTemp = CampaignTemplateBAL.CampaignTemplate_Schedule_SelectByID(QueryStringValues.CSID);
                        if (csTemp != null)
                        {
                            csTemp.TemplateID = nTemp.ID;
                            csTemp.LoggedBy = sessionKeys.UID;
                            csTemp.LoggedDate = DateTime.Now;
                            CampaignTemplateBAL.CampaignTemplate_Schedule_Update(csTemp);
                        }
                    }
                    //copy all the files
                    CopyAll(QueryStringValues.CTID.ToString(),nTemp.ID.ToString());
                    //redirect 
                    if (QueryStringValues.CSID > 0)
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}&CSID={1}", +nTemp.ID, QueryStringValues.CSID), false);
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}", +nTemp.ID), false);
                    } if (QueryStringValues.CSID > 0)
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}&CSID={1}", +nTemp.ID, QueryStringValues.CSID), false);
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}", +nTemp.ID), false);
                    }

                }
            
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            
        }
        public void CopyAll(string OldTemplateID, string newTemplateID)
        {
            try
            {
                var Newfolder = Server.MapPath("~/WF/UploadData/Campaign/Attachments/TemplateID" + newTemplateID);
                var Oldfolder = Server.MapPath("~/WF/UploadData/Campaign/Attachments/TemplateID" + OldTemplateID);

                var oldDirectory = Directory.GetFiles(Oldfolder);
                //check old folder is exists
                if (System.IO.Directory.Exists(Oldfolder))
                {
                    //check new folder is exists or not
                    if (!System.IO.Directory.Exists(Newfolder))
                    {
                        //create new folder
                        System.IO.Directory.CreateDirectory(Newfolder);
                        // Copy each file into the new directory.
                        //get all old folder filds
                        DirectoryInfo oldDir = new DirectoryInfo(Oldfolder);
                        foreach (FileInfo fi in oldDir.GetFiles())
                        {
                            //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                            fi.CopyTo(Path.Combine(Newfolder, fi.Name), true);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }

        private void uploadImage(int messageid)
        {
            try
            {
                if (fileupload.HasFile)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];
                        string fileName = Path.GetFileName(userPostedFile.FileName);

                        using (Stream fs = userPostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                ImageManager.FileDBSave(bytes, null, messageid.ToString(), ImageManager.file_section_messages, System.IO.Path.GetExtension(userPostedFile.FileName).ToLower(), userPostedFile.FileName, userPostedFile.ContentType);


                            }
                        }

                             
                    }
                }


                //if (fileupload.PostedFile.FileName.Length > 0)
                //{
                //    Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileupload.PostedFile.InputStream);
                //    ImageManager.FileDBSave(fileupload.FileBytes, null, messageid.ToString(), ImageManager.file_section_messages, System.IO.Path.GetExtension(fileupload.PostedFile.FileName).ToLower(), fileupload.PostedFile.ContentType);
                //    // DisplayLogo();

                //  // Response.Redirect(Request.RawUrl + "&v=" + DateTime.Now.Ticks.ToString(), false);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}