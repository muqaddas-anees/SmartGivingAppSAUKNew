using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;
using System.IO;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class Warrantydocs : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> aRepository = null;
        IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
        PortfolioContact pContact = null;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                SetContactDetails();
            }
        }

      

        private void SetContactDetails()
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    btnuser.Visible = true;
                    lbtnUpload.Visible = true;
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    int addressid = Convert.ToInt32(Request.QueryString["AddressID"]);
                    cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();

                    aRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();

                    var aAddress = aRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                    if (pContact != null)
                    {
                        //txtAddress.Text = pContact.Address1;
                        //txtCity.Text = pContact.City;
                        txtEmail.Text = pContact.Email;
                        txtmobileno.Text = pContact.Mobile;
                        txtname.Text = pContact.Name;
                        //txtPostal.Text = pContact.Postcode;
                        txtTelephone.Text = pContact.Telephone;
                        txtTown.Text = pContact.Town;
                        lblFullName.Text = pContact.Name;
                        imguser.ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + pContact.ID.ToString();
                       // ListBox_setValues(pContact.Tags);
                    }
                    if (aAddress != null)
                    {
                        txtAddress.Text = aAddress.Address;
                        txtCity.Text = aAddress.City;
                        txtPostal.Text = aAddress.PostCode;

                    }
                    Gridbind(contactid, addressid);
                }
                else
                {
                    btnuser.Visible = false;
                    lbtnUpload.Visible = false;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
           
            try
            {
                string[] emails = txtEmail.Text.ToLower().Trim().Split(',');
                cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                 uRepository = new UserRepository<UserMgt.Entity.Contractor>();
                if (Request.QueryString["ContactID"] == null)
                {
                    if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID).Count() == 0) &&
                         (uRepository.GetAll().Where(o => o.SID != 7 && o.EmailAddress.ToLower() == emails[0]).Count() == 0))
                    {
                        pContact = new PortfolioContact();
                        pContact.Name = txtname.Text.Trim();
                        pContact.PortfolioID = sessionKeys.PortfolioID;
                        pContact.Email = txtEmail.Text.Trim();
                        pContact.Telephone = txtTelephone.Text.Trim();
                        pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
                        pContact.Mobile = txtmobileno.Text.Trim();
                        pContact.Address1 = txtAddress.Text.Trim();
                        pContact.Town = txtTown.Text.Trim();
                        pContact.City = txtCity.Text.Trim();
                        pContact.Postcode = txtPostal.Text.Trim();
                        
                        //add to contact 
                        cRepository.Add(pContact);
                        if (pContact != null)
                        {
                            if (Fileupload_contact.HasFile)
                            {
                                string fname = "contact_" + pContact.ID;
                                ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                            }
                            FileuploadMethod(pContact.ID, Convert.ToInt32(Request.QueryString["AddressID"]));
                            lblmsg.Text = "<p class='bg-success'>Added successfully</p>";
                            //Response.Redirect("~/WF/CustomerAdmin/PortfolioContacts.aspx", false);
                        }
                        else
                        {
                            lblmsg.Text = "Cannot insert contact";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                    }
                }
                else
                {
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    int AddressID = Convert.ToInt32(Request.QueryString["AddressID"]);
                    //if (cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID && o.ID != contactid).Count() == 0
                    //    &&
                    //     uRepository.GetAll().Where(o => o.SID != 7 && o.EmailAddress.ToLower() == emails[0]).Count() == 0)
                    //{
                        pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();
                        pContact.Name = txtname.Text.Trim();
                        pContact.Email = txtEmail.Text.Trim();
                        pContact.Telephone = txtTelephone.Text.Trim();
                        pContact.Mobile = txtmobileno.Text.Trim();
                        pContact.Address1 = txtAddress.Text.Trim();
                        pContact.Town = txtTown.Text.Trim();
                        pContact.City = txtCity.Text.Trim();
                        pContact.Postcode = txtPostal.Text.Trim();
                        
                        //add to contact 
                        cRepository.Edit(pContact);
                        if (pContact != null)
                        {
                            if (Fileupload_contact.HasFile)
                            {
                                string fname = "contact_" + pContact.ID;
                                ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                            }
                            
                            FileuploadMethod(pContact.ID, AddressID);
                            lblmsg.Text = "<p class='bg-success'> Updated successfully</p>";
                            //Response.Redirect("~/WF/CustomerAdmin/PortfolioContacts.aspx", false);
                            Gridbind(contactid, AddressID);
                        }
                        else
                        {
                            lblmsg.Text = "Cannot insert contact";
                        }
                    //}
                    //else
                    //{
                    //    lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                    //}
                }
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

      

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/CustomerAdmin/PortfolioContacts.aspx", false);
        }

        protected void btnuser_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
            {
                int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                if (Fileupload_contact.HasFile)
                {
                    string fname = "contact_" + contactid;
                    ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                }
                // set contact details
                SetContactDetails();
            }
        }

       

        public void Gridbind(int SID, int AddressID)
        {
            try
            {
                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID +"_"+ AddressID);
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID + "_" + AddressID));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                    }

                    gridfiles.DataSource = files;
                    gridfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void FileuploadMethod(int contactid,int AddressID)
        {
            try
            {
                if (fileupload.HasFile)
                {
                    // Server.MapPath("~/WF/UploadData/AutoResponder/Attachments/TemplateID" + TemplateID);
                    var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + contactid + "_" + AddressID);
                    //HttpFileCollection uploadedFiles = fileupload.PostedFiles // Request.Files;
                    foreach(var postedfiles in fileupload.PostedFiles)
                    {
                         string fileName = Path.GetFileName(postedfiles.FileName);
                         if (!System.IO.Directory.Exists(folderpath))
                        {
                            System.IO.Directory.CreateDirectory(folderpath);
                            fileupload.SaveAs(folderpath + "\\" + fileName);
                        }
                        else
                        {
                            fileupload.SaveAs(folderpath + "\\" + fileName);
                        }
                    }


                    //for (int i = 0; i < uploadedFiles.Count; i++)
                    //{
                    //    HttpPostedFile userPostedFile =  uploadedFiles[i];
                    //    string fileName = Path.GetFileName(userPostedFile.FileName);

                    //    if (!System.IO.Directory.Exists(folderpath))
                    //    {
                    //        System.IO.Directory.CreateDirectory(folderpath);
                    //        userPostedFile.SaveAs(folderpath + "\\" + fileName);
                    //    }
                    //    else
                    //    {
                    //        userPostedFile.SaveAs(folderpath + "\\" + fileName);
                    //    }
                    //}
                }
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
                File.Delete(filePath);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Download")
            {
                var id = e.CommandArgument;
                var contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                var AddressID = Convert.ToInt32(Request.QueryString["AddressID"]);
                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + contactid + "_" + AddressID);

            }
        }
      
    }
}