using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using DeffinityAppDev.WF.DC.Feedback;

namespace DeffinityAppDev
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(sessionKeys.Message))
                {
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, string.Empty);
                    sessionKeys.Message = string.Empty;
                }



                lblOrgName.Text = sessionKeys.PortfolioName;
                imguser.Src = imguser1.Src = "~/ImageHandler.ashx?id="+ sessionKeys.UID.ToString()+ "&s=user";
                lblUserName.Text = sessionKeys.UName;

                //  linkProfile.HRef = string.Format("~/App/Member.aspx?mid={0}",sessionKeys.UID.ToString());
               
               if(sessionKeys.SID == 3)
                {
                    img_logo.Src = "~/ImageHandler.ashx?id=" + "0" + "&s=portfolio";// Deffinity.PortfolioManager.Portfilio.setLogo(sessionKeys.PortfolioID);
                    img_logo_mobile.Src = img_logo.Src;
                }
                else
                {
                    img_logo.Src = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio";// Deffinity.PortfolioManager.Portfilio.setLogo(sessionKeys.PortfolioID);
                    img_logo_mobile.Src = img_logo.Src;
                }

                linkProfile.HRef = string.Format("~/App/Member.aspx?mid={0}", sessionKeys.UID.ToString());

                //if (sessionKeys.SID == 4)
                //{
                //    link_home.HRef = "";
                //}

                if (sessionKeys.SID == 3)
                {
                    link_home.HRef = "~/App/FundraiserListView.aspx";
                    sidemenu_fundriser.Visible = true;
                    sidemenu.Visible = false;
                }
                else
                {
                    sidemenu.Visible = true;
                    sidemenu_fundriser.Visible = false;
                }


                if (sessionKeys.IsService)
                {
                    link_home.HRef = "~/App/Dashboard.aspx";
                }
                if (sessionKeys.SID == 4)
                {
                    link_home.HRef = "~/App/Donations.aspx";
                }

                link_home_app.HRef = link_home.HRef;
                if (sessionKeys.UID == 0)
                {
                    Response.Redirect("~/Default.aspx", false);
                }
            }

        }
        public string setLogo()
        {
            //~/Content/assets/images/logo-white-bg@2x.png
            //string defaultImg = "~/Content/assets/images/logo@2x.png";
            string defaultImg = "~/Content/assets/images/logo-white-bg@2x.png";
            string userImg = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID));
            if (File.Exists(userImg))
                defaultImg = userImg;
            return defaultImg;
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

        protected void btnSubmitFeedback_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {
                    var feedback = new FeedbackSystem
                    {
                        Submittedby = sessionKeys.UID,
                        PortfolioID = sessionKeys.PortfolioID,
                        Name = txtName.Text,
                        MobileNumber = txtMobile.Text,
                        CreatedDate = DateTime.Now,
                        IsAgreetobeContacted=chkContact.Checked,
                        Status = "Pending",
                        EmailAddress = txtEmail.Text,
                        FeedbackType = ddlFeedbackType.SelectedValue,
                        UrgencyLevel = ddlUrgencyLevel.SelectedValue,
                        Comments = txtComments.Text
                    };
                    context.FeedbackSystems.InsertOnSubmit(feedback);
                    context.SubmitChanges();
                    SendMails(feedback);
                    Response.Redirect(Request.RawUrl+(Request.RawUrl.Contains("?")?"&show=true": "?show=true"),false);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "showSuccessModal();", true);

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            // Call JavaScript function to show the success modal
        }

        private void SendMails(FeedbackSystem f)
        {
            try
            {
                string Subject = "Feedback from Donor Received";
                string emailcontext = $"Dear Team, <br /><br />The following feedback has been received from {f.Name}:<br />Category: {f.FeedbackType}<br /><Feedback: {f.Comments}<br /><br />Please log into the Admin portal to review it.<br /><br />Thanks";
                using (var context = new PortfolioDataContext())
                {
                    Email em = new Email();
                    var emails = context.EmailTrails.ToList();
                    foreach (var email in emails)
                    {

                        em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Subject, emailcontext, email.Email, "");

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    }
}