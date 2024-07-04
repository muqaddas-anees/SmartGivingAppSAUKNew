using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class FaithGivingManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                        sessionKeys.Message = string.Empty;
                    }
                    Bind_Campaigning_Owner();

                    //  BindReligion();
                    if (QueryStringValues.EID > 0)
                    {
                        var uid = QueryStringValues.EID;

                        IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                        var tithingDetail = pRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                        if (tithingDetail != null)
                        {
                            //int id = sessionKeys.UID;

                            //txtcurrenyValue.ReadOnly = false;

                            txtTitle.Text = tithingDetail.Title;
                            txtDescriptionArea.Text = tithingDetail.Description;
                            ddlCurrency.Text = tithingDetail.Currency;

                            txtcurrenyValue.Text = string.Format("{0:F2}", tithingDetail.DefaultTarget);
                            txtStartDate.Text = tithingDetail.StartDate.HasValue ? tithingDetail.StartDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            TextEndDate.Text = tithingDetail.EndDate.HasValue ? tithingDetail.EndDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            img.ImageUrl = GetImageUrl(uid.ToString());
                            SetAmountValues(tithingDetail.DefaultValues);
                            chkSendEmail.Checked = tithingDetail.SendMailAfterDonation.HasValue ? tithingDetail.SendMailAfterDonation.Value : false;
                            ddlOwner.SelectedValue = tithingDetail.LoggedByID.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



        private void SetAmountValues(string setvalues)
        {
            if (setvalues.Length > 1)
            {
                var sList = setvalues.Split(',');
                foreach (var s in sList)
                {
                    for (int i = 0; i < ckbCurrencyList.Items.Count; i++)
                    {
                        if (s.Length > 0)
                        {
                            if (Convert.ToDouble(ckbCurrencyList.Items[i].Value.Trim()) == Convert.ToDouble(s.Trim()))
                            {
                                ckbCurrencyList.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }


        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();

        }



        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();

                var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();
                if (value == null)
                {
                    value = new PortfolioMgt.Entity.TithingDefaultDetail();

                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.DefaultTarget = Convert.ToDouble(txtcurrenyValue.Text);
                    value.Currency = ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = Convert.ToInt32(ddlOwner.SelectedValue);
                    value.OrganizationID = 1;
                    value.SendMailAfterDonation = chkSendEmail.Checked;
                    value.DefaultValues = GetChecklist();
                    value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    value.EndDate = Convert.ToDateTime(TextEndDate.Text.Trim());
                    value.DefaultBanner = "";
                    value.OrganizationID = sessionKeys.PortfolioID;
                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Add(value);
                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    StartUpLoad(value.ID);
                    //Response.Redirect(Request.RawUrl+"?eid="+value.ID.ToString(), false);
                    Response.Redirect("~/App/FaithGivingListManage.aspx", false);
                }
                else
                {
                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.DefaultTarget = float.Parse(txtcurrenyValue.Text);
                    value.Currency = ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = Convert.ToInt32(ddlOwner.SelectedValue);
                    value.OrganizationID = 1;
                    value.SendMailAfterDonation = chkSendEmail.Checked;
                    value.DefaultValues = GetChecklist();
                    value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    value.EndDate = Convert.ToDateTime(TextEndDate.Text.Trim());
                    value.DefaultBanner = "";
                    value.OrganizationID = sessionKeys.PortfolioID;
                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(value);
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    StartUpLoad(value.ID);
                    Response.Redirect("~/App/FaithGivingListManage.aspx", false);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetChecklist()
        {
            string retval = "";
            for (int i = 0; i < ckbCurrencyList.Items.Count; i++)
            {
                if (ckbCurrencyList.Items[i].Selected == true)// getting selected value from CheckBox List  
                {
                    retval += ckbCurrencyList.Items[i].Text + " ,"; // add selected Item text to the String .  
                }
            }
            return retval;
        }
        protected void btnPublish_Click(object sender, EventArgs e)
        {

        }


        private void Bind_Campaigning_Owner()
        {
            var ownerlist = (from c in UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                             where c.SID == UserMgt.BAL.UserType.SmartPros
                             orderby c.ContractorName
                             select new
                             {
                                 ID = c.ID,
                                 Name = c.ContractorName
                             }).ToList();
            ddlOwner.DataSource = ownerlist;
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataValueField = "ID";
            ddlOwner.DataBind();
            ddlOwner.Items.Insert(0, new ListItem("Please select...", "0"));
        }






        protected void Check_Clicked(object sender, EventArgs e)
        {

            int id = sessionKeys.UID;

            float num = 0;

            foreach (ListItem item in ckbCurrencyList.Items)
            {
                if (item.Selected)
                {

                    if (item.Value == "Other Amount")
                    {
                        txtcurrenyValue.ReadOnly = false;
                        txtcurrenyValue.Text = "";
                        // ddlCurrency.SelectedValue = "US Doller";

                        foreach (ListItem li in ckbCurrencyList.Items)
                        {
                            li.Selected = false;
                        }
                        item.Selected = true;

                    }
                    else
                    {
                        float num1 = float.Parse(item.Value);
                        num = num + num1;
                        txtcurrenyValue.Text = num.ToString();
                    }


                }
            }



        }








        private void StartUpLoad(int tithingid)
        {


            if (imgLogo.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgLogo.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }
            //int id = tithingid;
            ////get the file name of the posted image  
            //string imgName = imgLogo.FileName;
            ////sets the image path  
            //string imgPath = "~/WF/UploadData/Tithing/" + "Tithing_" + id + ".png";
            ////get the size in bytes that  

            //int imgSize = imgLogo.PostedFile.ContentLength;




            ////validates the posted file before saving  
            //if (imgLogo.PostedFile != null && imgLogo.PostedFile.FileName != "")
            //{
            //    //then save it to the Folder  
            //    imgLogo.SaveAs(Server.MapPath(imgPath));
            //    img.ImageUrl = "~/" + imgPath;


            //    //// 10240 KB means 10MB, You can change the value based on your requirement  
            //    //if (imgLogo.PostedFile.ContentLength > 10240)
            //    //{
            //    //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
            //    //}
            //    //else
            //    //{
            //    //    //then save it to the Folder  
            //    //    imgLogo.SaveAs(Server.MapPath(imgPath));
            //    //    img.ImageUrl = "~/" + imgPath;

            //    //}

            //}
        }









    }


}