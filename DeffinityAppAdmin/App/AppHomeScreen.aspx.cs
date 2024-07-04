using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{




    public partial class AppHomeScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                


                if (!IsPostBack)
                {

                    BindListView();

                    //BindReligion();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction();", true);

                    if (Request.QueryString["orgid"] != null)
                    {
                       
                    }


                }

                else
                {
                 //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction();", true);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       

       

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Religion";
              //  ddlReligion.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAddDenimination_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Denomination";
              //  ddlDenimination.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnSaveChangesPop_Click
        protected void btnSaveChangesPop_Click(object sender, EventArgs e)
        {
           
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {

            //  var text = CKEditorTextArea.Text;

            try
            {
                var r = Convert.ToInt32(HiddenFieldReligion.Value);
                var d = Convert.ToInt32(HiddenFieldDenomination.Value);

                if (Request.QueryString["orgid"] == null)
                {
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                    if (pEntity != null)
                    {
                        pEntity.DenominationDetailsID = r;

                       

                        pEntity.SubDenominationDetailsID = d;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pEntity);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      




        protected void AddNewBanner_Click(object sender, EventArgs e)
        {

        }



     







        protected void Page_PreInit(object sender, EventArgs e)
        {
          
        }

        protected void UploadBanner_Click(object sender, EventArgs e)
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.AdvertisingBannerDetail>();



            var pEntity = new PortfolioMgt.Entity.AdvertisingBannerDetail();


            // Add New Banner

            if (lblModelHeading.Text== "Add New Banner")
            {
                

                var unicId = Guid.NewGuid();

                pEntity.BannerID = unicId + "";
                pEntity.ReligionID = 1;
                pEntity.DenominationID = 2;
                pEntity.LinkURL = txtLinkUrl.Text;
                pEntity.TransitionTime = Int32.Parse(txtTransitionTime.Text);

                cRep.Add(pEntity);



                if (FileUploadPageBackground.HasFile)
                {
                    string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();
                    // Checking the format of the uploaded file.  

                    string FileName = unicId + FileType;

                    if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                    {
                        lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                    }
                    else
                    {
                        string OrganizationImagesId = "OrganizationImages_" + 100;





                        string folderPath = Server.MapPath("~/WF/UploadData/OrganizationImages/" + OrganizationImagesId + "/");







                        if (!(Directory.Exists(folderPath)))
                        {
                            Directory.CreateDirectory(folderPath);



                            FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                        }
                        else
                        {


                            FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                        }






                        //Display the Picture in Image control.
                        ImagePageBackground.ImageUrl = "~/WF/UploadData/OrganizationImages/" + OrganizationImagesId + "/" + Path.GetFileName(FileName);

                    }





                }
                else
                {
                    lblFilenotSelected.Text = "No  file selected";
                }

            }
            else
            {

                try
                {
                    int ID = Int32.Parse(lblModelHeading.Text.Split(' ')[lblModelHeading.Text.Split(' ').Length - 1]);

                    var list = cRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                    //   FileUploadPageBackground.PostedFile.FileName;

                    string filename = ImagePageBackground.ImageUrl.Split('/')[ImagePageBackground.ImageUrl.Split('/').Length - 1];

                   

                    string filename2 = list.BannerID;


                    list.LinkURL = txtLinkUrl.Text;
                    list.TransitionTime = Int32.Parse(txtTransitionTime.Text);

                    cRep.Edit(list);





                }

                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }





            }


            BindListView();

        }













        protected void BindListView()
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.AdvertisingBannerDetail>();


            var list = cRep.GetAll().Where(o => o.ID > 0).ToList();


            int religion = Convert.ToInt32( HiddenFieldReligion.Value);

            int Group = Convert.ToInt32(HiddenFieldGroup.Value);

            int Denomination = Convert.ToInt32(HiddenFieldDenomination.Value);

            if (religion > 0)
            {
                list = cRep.GetAll().Where(o => o.ID == religion).ToList();

                if (Group > 0)
                {
                    list = cRep.GetAll().Where(o => o.ID == Group).ToList();

                    if (Denomination > 0)
                    {
                        list = cRep.GetAll().Where(o => o.ID == Denomination).ToList();
                    }

                }

            }

            

            BannerList.DataSource = list;
            BannerList.DataBind();


        }



        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/OrganizationImages/OrganizationImages_100/") + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/ WF / UploadData / OrganizationImages / OrganizationImages_100 / ", contactsId.ToString());
                else
                    img = string.Format("~/ WF / UploadData / OrganizationImages / OrganizationImages_100 / ", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + contactsId.ToString() + ".png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            // return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 


            //  return "~/WF/UploadData/OrganizationImages/OrganizationImages_100/0cd22689-8dd6-4763-9c06-e3a4b61d35d1.png";

            return "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + contactsId + ".png";
        }




        protected static string DeleteBanner(string contactsId)
        {
            return "" + contactsId;
        }



        protected static string EditBanner(string contactsId)
        {
            return "" + contactsId;
        }



        protected void deleteInListView(object sender, EventArgs e)
        {

            Button btn = (Button)sender;

            int ID = Int32.Parse(btn.CommandName);

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.AdvertisingBannerDetail>();

            var list = cRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();


            cRep.Delete(list);


        }



        protected void EditBannerInList(object sender, EventArgs e)
        {
            mdlManageOptions.Show();

            Button btn = (Button)sender;

            int ID = Int32.Parse(btn.CommandName);

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.AdvertisingBannerDetail>();

            var list = cRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();

            lblModelHeading.Text = "Edit Banner " + ID;

            // "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";

            string imageUrl = "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";

            ImagePageBackground.ImageUrl = imageUrl;

            txtLinkUrl.Text = list.LinkURL;

            txtTransitionTime.Text = list.TransitionTime.ToString();





        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblModelHeading.Text = "Add New Banner";
            ImagePageBackground.ImageUrl = "";

            txtLinkUrl.Text = "";
            txtTransitionTime.Text = "";

            mdlManageOptions.Show();

            //lblModelHeading.Text = "Edit Banner";
        }

        protected void RemoveImage_Click(object sender, EventArgs e)
        {
            ImagePageBackground.ImageUrl = "";
            mdlManageOptions.Show();
        }

        protected void RemoveImage_Click1(object sender, EventArgs e)
        {

        }

        protected void RemoveImageEdit_Click(object sender, EventArgs e)
        {
            if (ImagePageBackground.ImageUrl != "")
            {
                ImagePageBackground.ImageUrl = "";
                mdlManageOptions.Show();

            }
            else
            {
                mdlManageOptions.Show();
            }

        }





        protected void BtnGetVideo_Click(object sender, EventArgs e)
        {
            var religion = HiddenFieldReligion.Value;

            var Group = HiddenFieldGroup.Value;

            var Denomination = HiddenFieldDenomination.Value;


            // BELOW LINE IS TO CALL JAVASCRIPT FUNCTION  MyFunction

          //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "CallMyFunction", "MyFunction(" + religion + "," + Group + "," + Denomination + ");", true);

             Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction(" + religion + "," + Group + "," + Denomination + ");", true);
            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction()", true);

            BindListView();
        }





        //    protected void RemoveImage(object sender, EventArgs e)
        //{
        //    ImagePageBackground.ImageUrl = "";
        //}













    }

}