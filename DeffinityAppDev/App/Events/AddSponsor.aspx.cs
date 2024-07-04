using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Sponsor
{
   





    public partial class AddSponsor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    txtDatePaid.Text = DateTime.Now.ToShortDateString();
                    txtDatePledged.Text = DateTime.Now.ToShortDateString();

                    //  BindReligion();
                    if (Request.QueryString["mid"] != null)
                    {
                        var id = Request.QueryString["mid"].ToString();
                        var Aid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                        IPortfolioRepository<PortfolioMgt.Entity.SponsorTable> pRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();


                        var sDetails = pRep.GetAll().Where(o => o.SponsorId == Aid).FirstOrDefault();

                        if(sDetails != null)
                        {
                            txtDescriptionAboutCompany.Text = sDetails.AboutCompany;
                            txtCompanyName.Text = sDetails.CompanyName;
                            txtAmountSponsored.Text = (sDetails.AmountSponsored.HasValue ? sDetails.AmountSponsored.Value.ToString() : Convert.ToDecimal("0").ToString());
                            txtContactEmail.Text = sDetails.ContactEmail;
                            txtContactName.Text = sDetails.ContactName;
                            txtContactPhoneNumber.Text = sDetails.ContactPhoneNumber;
                            txtDatePaid.Text = sDetails.DatePaid.HasValue ? sDetails.DatePaid.Value.ToShortDateString() : "";
                            txtDatePledged.Text = sDetails.DatePledged.HasValue ? sDetails.DatePledged.Value.ToShortDateString() : "";
                            DropDownListStatus.SelectedValue = sDetails.Status;
                            img.ImageUrl = GetImageUrl(sDetails.SponsorId);
                        }

                    }
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }











        protected static string GetImageUrl(string contactsId)
        {
            //string img = string.Empty;
            //if (contactsId != null)
            //{
            //    //return GetImageUrl(a_gId, a_oThumbSize, true);
            //    bool isOriginal = false;



            //    string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + contactsId.ToString() + ".png";

            //    if (System.IO.File.Exists(filepath))
            //    {
            //        if (isOriginal)
            //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId.ToString() + ".png");
            //        else
            //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId.ToString() + ".png");
            //        // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //        //img = string.Format("<img src='{0}' />", imgurl);
            //    }
            //    else
            //    {
            //        img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //        //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //    }
            //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            //    // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            //}
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_spnsor;
        }



        protected void ddlActiviteCategory_TextChanged()
        {
            
        }





        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = sessionKeys.UID;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                if (Request.QueryString["mid"] != null)
                {



                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();

                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = cRep.GetAll().Where(o => o.SponsorId == uid).FirstOrDefault();
                    if (cDetails != null)
                    {
                        var value = cRep.GetAll().Where(o => o.SponsorId == uid).FirstOrDefault();

                        value.CompanyName = txtCompanyName.Text;
                        value.AboutCompany = txtDescriptionAboutCompany.Text;
                        value.ContactName = txtContactName.Text;
                        value.ContactEmail = txtContactEmail.Text;
                        value.ContactPhoneNumber = txtContactPhoneNumber.Text;
                        if (txtAmountSponsored.Text.Length > 0)
                            value.AmountSponsored = Convert.ToDouble(txtAmountSponsored.Text);
                        else
                            value.AmountSponsored = 0;

                        value.Status = DropDownListStatus.SelectedValue;
                        if (txtDatePledged.Text.Length > 0)
                            value.DatePledged = Convert.ToDateTime(txtDatePledged.Text);
                        else
                            value.DatePledged = DateTime.Now;

                        if (txtDatePaid.Text.Length > 0)
                            value.DatePaid = Convert.ToDateTime(txtDatePaid.Text);
                        else
                            value.DatePaid = DateTime.Now;


                        cRep.Edit(value);

                        if (imgLogo.HasFile)
                        {

                            if (imgLogo.PostedFile.FileName.Length > 0)
                            {
                                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                                ImageManager.SaveSponserImage_setpath(imgLogo.FileBytes, value.SponsorId.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                                // DisplayLogo();
                            }
                            //string FileType = Path.GetExtension(imgLogo.PostedFile.FileName).ToLower().Trim();
                            //// Checking the format of the uploaded file.  

                            //string FileName = value.SponsorUNID + FileType;

                            //if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                            //{
                            //    //lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                            //}
                            //else
                            //{


                            //    string folderPath = Server.MapPath("~/WF/UploadData/Speakers_Imgages/");


                            //    if (!(Directory.Exists(folderPath)))
                            //    {
                            //        Directory.CreateDirectory(folderPath);



                            //        imgLogo.SaveAs(folderPath + Path.GetFileName(FileName));
                            //    }
                            //    else
                            //    {


                            //        imgLogo.SaveAs(folderPath + Path.GetFileName(FileName));
                            //    }



                            //}


                                //Display the Picture in Image control.
                                // ImagePageBackground.ImageUrl = "~/WF/UploadData/Speakers_Imgages/" + Path.GetFileName(FileName);

                            }

                            Response.Redirect("~/App/Events/sponsors_List.aspx?unid=" + QueryStringValues.UNID, false);

                    }
                }
                else
                {




                    //PortfolioMgt.Entity.SponsorTable

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> aRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                    var eventDetils = aRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();


                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();

                    var value = new PortfolioMgt.Entity.SponsorTable();
                    value.SponsorUNID = Guid.NewGuid().ToString();

                    value.EventUNID = QueryStringValues.UNID;

                    value.EventID = eventDetils.ID;
                    value.CompanyName = txtCompanyName.Text;
                    value.AboutCompany = txtDescriptionAboutCompany.Text;
                    value.ContactName = txtContactName.Text;
                    value.ContactEmail = txtContactEmail.Text;
                    value.ContactPhoneNumber = txtContactPhoneNumber.Text;
                    if (txtAmountSponsored.Text.Length > 0)
                        value.AmountSponsored = Convert.ToDouble(txtAmountSponsored.Text);
                    else
                        value.AmountSponsored = 0;

                    value.Status = DropDownListStatus.SelectedValue;
                    if (txtDatePledged.Text.Length > 0)
                        value.DatePledged = Convert.ToDateTime(txtDatePledged.Text);
                    else
                        value.DatePledged = DateTime.Now;

                    if (txtDatePaid.Text.Length > 0)
                        value.DatePaid = Convert.ToDateTime(txtDatePaid.Text);
                    else
                        value.DatePaid = DateTime.Now;

                    value.createdon = DateTime.Now;
                    
                    //value.LogoPath=


                    cRep.Add(value);

                    if (imgLogo.PostedFile.FileName.Length > 0)
                    {
                        Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                        ImageManager.SaveSponserImage_setpath(imgLogo.FileBytes, value.SponsorId.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                        // DisplayLogo();
                    }

                    //if (imgLogo.HasFile)
                    //    {
                    //        string FileType = Path.GetExtension(imgLogo.PostedFile.FileName).ToLower().Trim();
                    //        // Checking the format of the uploaded file.  

                    //        string FileName = value.SponsorUNID + FileType;

                    //        if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                    //        {
                    //            //lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                    //        }
                    //        else
                    //        {


                    //            string folderPath = Server.MapPath("~/WF/UploadData/Speakers_Imgages/");


                    //            if (!(Directory.Exists(folderPath)))
                    //            {
                    //                Directory.CreateDirectory(folderPath);



                    //                imgLogo.SaveAs(folderPath + Path.GetFileName(FileName));
                    //            }
                    //            else
                    //            {


                    //                imgLogo.SaveAs(folderPath + Path.GetFileName(FileName));
                    //            }






                    //            //Display the Picture in Image control.
                    //            // ImagePageBackground.ImageUrl = "~/WF/UploadData/Speakers_Imgages/" + Path.GetFileName(FileName);

                    //        }
                    //    }


                        Response.Redirect("~/App/Events/sponsors_List.aspx?unid=" + QueryStringValues.UNID, false);

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetImageUrl(object contactsId)
        {
            //string img = string.Empty;
            //if (contactsId != null)
            //{
            //    //return GetImageUrl(a_gId, a_oThumbSize, true);
            //    bool isOriginal = false;



            //    string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + contactsId.ToString() + ".png";

            //    if (System.IO.File.Exists(filepath))
            //    {
            //        if (isOriginal)
            //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId.ToString() + ".png");
            //        else
            //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId.ToString() + ".png");
            //        // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //        //img = string.Format("<img src='{0}' />", imgurl);
            //    }
            //    else
            //    {
            //        img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //        //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //    }
            //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            //    // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            //}


            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_spnsor;

        }

        protected void BackToGrid_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/App/Events/sponsors_List.aspx?unid=" + QueryStringValues.UNID, false);

        }
    }






}