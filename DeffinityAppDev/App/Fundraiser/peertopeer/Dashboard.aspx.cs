using DC.Entity;
using shortid.Configuration;
using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Linq;
using TuesPechkin;

namespace DeffinityAppDev.App.Fundraiser.peertopeer
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (sessionKeys.Message.Length > 0)
                    {
                        if (sessionKeys.Message.ToLower().Contains("error:"))
                        {
                            sessionKeys.Message = sessionKeys.Message.ToLower().Replace("error:", "");
                            DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, sessionKeys.Message);
                        }
                        else
                            DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message);

                        sessionKeys.Message = string.Empty;
                    }

                    UpdateSummaryValues();
                    BindGrid();
                    BindDonationsGrid();


                    //if (Request.QueryString["tab"] == null)
                    //{
                    //    if (QueryStringValues.UNID.Length > 0)
                    //    {
                    //        link1.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + QueryStringValues.UNID + "&tab=1";
                    //        link2.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + QueryStringValues.UNID + "&tab=2";
                    //    }

                    //    if (QueryStringValues.MUNID.Length > 0)
                    //    {
                    //        link1.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?munid=" + QueryStringValues.MUNID + "&tab=1";
                    //        link2.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?munid=" + QueryStringValues.MUNID + "&tab=2";
                    //    }

                    //    kt_tab_pane_1.Visible = true;
                    //    kt_tab_pane_2.Visible = false;
                    //    link1.CssClass = "nav-link active";
                    //}
                    //else
                    //{
                    //    if (QueryStringValues.UNID.Length > 0)
                    //    {
                    //        link1.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + QueryStringValues.UNID + "&tab=1";
                    //        link2.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + QueryStringValues.UNID + "&tab=2";
                    //    }

                    //    if (QueryStringValues.MUNID.Length > 0)
                    //    {
                    //        link1.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?munid=" + QueryStringValues.MUNID + "&tab=1";
                    //        link2.NavigateUrl = "~/App/Fundraiser/peertopeer/Dashboard.aspx?munid=" + QueryStringValues.MUNID + "&tab=2";
                    //    }

                    //    var t = Request.QueryString["tab"].ToString();
                    //    if (t == "1")
                    //    {
                    //        link1.CssClass = "nav-link active";
                    //        link2.CssClass = "nav-link";
                    //        kt_tab_pane_1.Visible = true;
                    //        kt_tab_pane_2.Visible = false;

                    //    }
                    //    else if (t == "2")
                    //    {
                    //        link1.CssClass = "nav-link";
                    //        link2.CssClass = "nav-link active";
                    //        kt_tab_pane_1.Visible = false;
                    //        kt_tab_pane_2.Visible = true;

                    //    }
                    //}
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void UpdateSummaryValues()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> fRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();

                var fList = fRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();

                if(fList.Count >0)
                {
                    lbltotal_donations.Text = "0.00";
                    lbltotal_donors.Text = "0.00";
                    lblaverage_donation.Text = "0.00";
                }

                
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnInvite_Click(object sender, EventArgs e)
        {
            txtEmail.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            mdl.Show();

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //status - Invitation Sent
                //sessionKeys.Message = "Invitation has been sent";

                Response.Redirect("~/FundraiserView.aspx?unid="+ QueryStringValues.UNID , false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnCashDonation_Click(object sender, EventArgs e)
        {
            try
            {
                //status - Invitation Sent
                //sessionKeys.Message = "Invitation has been sent";

                Response.Redirect("~/App/OtherDonations.aspx?type=cash&munid="+QueryStringValues.UNID, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveInvite_Click(object sender, EventArgs e)
        {
            try
            {
                var IsValid = true;
                //not a donor
                var userDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o=>o.SID != 2).Where(o => o.EmailAddress.ToLower().Trim() == txtEmail.Text.Trim().ToLower()).FirstOrDefault();
                if (userDetails != null)
                {
                    //if (userDetails.SID == 3)
                    //    IsValid = true;
                    //else
                    //    IsValid = false;
                    IsValid = true;
                }


                if (IsValid)
                {

                    IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
                    var f = fRep.GetAll().Where(o => o.MainFundUNID == QueryStringValues.UNID).Where(o => o.Email == txtEmail.Text.Trim()).FirstOrDefault();
                    if (f == null)
                    {
                       
                        f = new PortfolioMgt.Entity.FundraisersInfo();
                        f.FirstName = txtFirstName.Text.Trim();
                        f.LastName = txtLastName.Text.Trim();
                        f.Email = txtEmail.Text.Trim();
                        f.MainFundUNID = QueryStringValues.UNID;
                        f.Status = "Invitation Sent";

                        f.IsInvitationSent = true;
                        f.IsDeleted = false;
                        f.IsAddMember = false;
                        f.InvitationSentOn = DateTime.Now;
                        var options = new GenerationOptions(useSpecialCharacters: false);
                        string shortid = ShortId.Generate(options);
                        f.ShortCode = shortid;

                        fRep.Add(f);
                        if (f.ID > 0)
                            sendInviteMail(f);
                        //send mail


                        //status - Invitation Sent
                        sessionKeys.Message = "Invitation has been sent";

                        Response.Redirect(Request.RawUrl, false);
                    }
                    
                    else
                    {
                        if ((f.IsDeleted ?? false) == true)
                        {
                            f.IsDeleted = false;
                            fRep.Edit(f);

                            if (f.ID > 0)
                                sendInviteMail(f);

                            sessionKeys.Message = "Invitation has been sent";

                            Response.Redirect(Request.RawUrl, false);

                        }
                        else
                        {
                            sessionKeys.Message = "Error: Email already exists. Please try again";

                            Response.Redirect(Request.RawUrl, false);
                        }
                    }
                }
                else
                {
                    sessionKeys.Message = "Error: Email already exists. Please try again";

                    Response.Redirect(Request.RawUrl, false);
                }


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void sendInviteMail(PortfolioMgt.Entity.FundraisersInfo f)
        {

            try
            {
                if (f != null)
                {

                    //var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                    var pdetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(tclist.OrganizationID.Value);
                    // var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                    Random generator = new Random();

                    String body = "";

                    Emailer em = new Emailer();
                    body = em.ReadFile("~/App/Fundraiser/peertopeer/EmailTemplates/invitation.html");


                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");
                    body = body.Replace("{{name}}", f.FirstName + " " + f.LastName);
                    body = body.Replace("{{fundriser_name}}", tclist.Title);
                    //{{fundraiser_name}}
                    body = body.Replace("{{fundraiser_url}}", Deffinity.systemdefaults.GetWebUrl() + "/" + "Fundraiser" + "/" + tclist.QRcode);
                    body = body.Replace("{{instance_url}}", Deffinity.systemdefaults.GetWebUrl());
                    body = body.Replace("{{instance_name}}", pdetails.PortFolio);
                    body = body.Replace("{{app_name}}", pdetails.PortFolio);
                    body = body.Replace("{{app_logo}}", Deffinity.systemdefaults.GetWebUrl()+ "/assets/media/logos/logo-1.png");
                    body = body.Replace("{{register_url}}", Deffinity.systemdefaults.GetWebUrl() + "/" + "Register" + "/" + f.ShortCode);


                   // html_body = html_body.Replace("[table]", body);
                   // body = html_body;

                    string fromid = Deffinity.systemdefaults.GetFromEmail();

                    string toid = f.Email.Trim();
                    string subject = "Invitation";

                    // CKEditor1.Text = body;
                    //mdlShowMail.Show();
                    Email ToEmail = new Email();


                    ToEmail.SendingMail(fromid, subject, body, toid, "");

                    ToEmail.SendingMail(fromid, subject, body, "indra@deffinity.com", "");


                    // CKEditor1.Text = p.EmailContent;
                    //hid.Value = p.ID.ToString();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridFundrisers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                var id = e.CommandArgument.ToString();
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
                var f = fRep.GetAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                if (e.CommandName == "resend")
                {
                    sendInviteMail(f);
                    sessionKeys.Message = "Invitation has been sent";

                    Response.Redirect(Request.RawUrl, false);

                }
                else if(e.CommandName == "preview")
                {
                   if(f != null)
                    {
                        if(f.FundUNID != null)
                        Response.Redirect("~/FundraiserView.aspx?unid="+f.FundUNID, false);
                        else
                            Response.Redirect(Request.RawUrl, false);
                    }
                }
                else if(e.CommandName == "del")
                {
                    if (f != null)
                    {
                        f.IsInvitationSent = false;
                        f.Status = "";
                        f.IsDeleted = true;
                        fRep.Edit(f);

                        //update the useraccount

                      

                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;

                        Response.Redirect(Request.RawUrl, false);
                        //Response.Redirect("", false);
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindGrid()
        {
            try
            {

                
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
                var fList = fRep.GetAll().Where(o => o.MainFundUNID == QueryStringValues.UNID).Where(o=>(o.IsDeleted??false) == false).ToList();


                var tithing_values = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o=>fList.Select(p=>p.FundUNID).Contains( o.FundriserUNID)).ToList();

                var rList = (from r in fList
                             select new
                             {
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
                                 r.ShortCode,
                                 Status = r.Status == "Invitation Sent"? "<span class='badge badge-danger'>Invitation Sent</span>":(r.Status == "Invitation Accepted" ? "<span class='badge badge-success'>Invitation Accepted</span>": ""),
                                 Name = r.FirstName + " " + r.LastName,
                                 TotalRaised = getDonorAmount(tithing_values, r),
                                 TotalRaisedDisplay = string.Format("{1}{0:N2}", getDonorAmount(tithing_values, r), Deffinity.Utility.GetCurrencySymbol()) ,
                                 NumberofDonors = getDonors(tithing_values,r),



                             }).ToList();

                if (txtSearchFunraiser.Text.Length > 0)
                {
                    var searchtext = txtSearchFunraiser.Text.Trim();
                    rList = (rList.Where(p => (
                (p.Name != null ? p.Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Email != null ? p.Email.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false)
                ))).ToList();
                }

                GridFundrisers.DataSource = rList.OrderByDescending(p=>p.TotalRaised).ToList();
                GridFundrisers.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private int getDonors(List<PortfolioMgt.Entity.TithingPaymentTracker> tlist, PortfolioMgt.Entity.FundraisersInfo f )
        {
            var retval = 0;
            if(f.FundUNID != null)
            {
                retval = tlist.Where(o => o.FundriserUNID == f.FundUNID).Where(o => (o.IsPaid ?? false) == true).Count();
            }

            return retval;

        }

        private double getDonorAmount(List<PortfolioMgt.Entity.TithingPaymentTracker> tlist, PortfolioMgt.Entity.FundraisersInfo f)
        {
            var retval = 0.00;
            if (f.FundUNID != null)
            {
                retval = tlist.Where(o => o.FundriserUNID == f.FundUNID).Where(o => (o.IsPaid ?? false) == true).Sum(p => p.PaidAmount) ?? 0.00;
            }

            return retval;

        }

        protected void btnSearchFundraiser_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private string getTithing(List<PortfolioMgt.Entity.TithingDefaultDetail> tulist, int id)
        {
            string retval = "";


            if (id == 0)
                retval = "";
            else
            {
                var eDetails = tulist.Where(o => o.ID == id).FirstOrDefault();
                if (eDetails != null)
                {

                    retval = eDetails.Title;

                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }
        protected void GridDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

                if (e.CommandName == "SendReceipt")
                {
                    var id = e.CommandArgument.ToString();
                    //hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                    Random generator = new Random();
                    var dItem = (from t in tList
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 where t.ID == Convert.ToInt32(id)
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     //CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     //CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.unid,

                                 }).FirstOrDefault();

                    // var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                    if (dItem.unid == null)
                    {
                        //var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        //if (dEntity != null)
                        //{
                        //    dEntity.unid = Guid.NewGuid().ToString();

                        //    PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(dEntity);
                        //    //hunid.Value = dEntity.unid;
                        //}
                    }
                    //else
                    //    hunid.Value = dItem.unid;


                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                    var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
                    if (tn == null)
                        tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();


                    // ddlTemplate.SelectedValue = tn.ID.ToString();

                    String body = "";
                    if (tn != null)
                    {
                        body = tn.EmailContent;
                        //{{currentyear}}
                        body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                        body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                        body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0));
                        body = body.Replace("{{name}}", dItem.Name);
                        //body = body.Replace("{{category}}", dItem.CategoryList);
                        body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                        body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                        body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.Amount));

                        body = body.Replace("{{donorfirstname}}", dItem.Name);
                        body = body.Replace("{{donorsurname}}", dItem.Name);
                        //donorcompany
                        //body = body.Replace("{{category}}", dItem.CategoryList);

                        body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


                        body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.Amount));

                        body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                        body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                        //logo

                        body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

                    }



                    if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                    {
                        Emailer em = new Emailer();
                        string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                        html_body = html_body.Replace("[table]", body);
                        body = html_body;

                        string fromid = Deffinity.systemdefaults.GetFromEmail();

                        string toid = dItem.Email;
                        string subject = "Donation";
                        //htomail.Value = toid;
                        //hsubject.Value = subject;
                        //CKEditor1.Text = body;

                        //if (dItem.Status.Contains("Successful"))
                        //    mdlShowMail.Show();
                        //Email ToEmail = new Email();


                        //ToEmail.SendingMail(fromid, subject,body,toid,"");

                        //sessionKeys.Message = "Your message is on it's way!";

                        //Response.Redirect(Request.RawUrl, false);
                    }




                }
                if (e.CommandName == "member")
                {
                    try
                    {
                        var id = e.CommandArgument.ToString();
                       // hid.Value = id.ToString();
                        var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                        var uEntity = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == tList.OrganizationID).Where(o => o.EmailAddress.ToLower().Trim() == tList.DonerEmail.ToLower().Trim()).FirstOrDefault();

                        if (uEntity != null)
                        {
                            Response.Redirect("~/App/Member.aspx?mid=" + uEntity.ID, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                }
                if (e.CommandName == "view")
                {
                    var id = e.CommandArgument.ToString();
                    //hid.Value = id.ToString();
                    var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                    var tEntity = tList.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (tEntity != null)
                    {
                        if (QueryStringValues.Type.Length > 0)
                        {
                            Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + tEntity.unid, false);
                        }
                    }

                    var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();


                    Random generator = new Random();
                    var rlist = (from t in tList
                                 .Where(o => o.ID == Convert.ToInt32(id))
                                     // join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     // Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     //CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     //CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.Notes,
                                     t.unid,
                                     PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                 }).ToList();

                    var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    if (dItem != null)
                    {
                        //lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                        //lblStatus.Text = dItem.Status;
                        //txtname.Text = dItem.Name;
                        //txttype.Text = dItem.PaymentType;
                        //txtemail.Text = dItem.Email;
                        //lblCategories.Text = dItem.CategoryListWithAmount;
                        //txtNotes.Text = dItem.Notes;
                        //hunid.Value = dItem.unid;
                        //lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                        //lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                        //Gridfilesbind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string getUserData(List<UserMgt.Entity.Contractor> ulist, int loggedby, string check_value, string nameOrEmail)
        {
            string retval = "";


            if (loggedby == 0)
                retval = check_value;
            else
            {
                var eDetails = ulist.Where(o => o.ID == loggedby).FirstOrDefault();
                if (eDetails != null)
                {
                    if (nameOrEmail == "name")
                        retval = eDetails.ContractorName;
                    else
                        retval = eDetails.EmailAddress;
                }
                else
                {
                    retval = "";
                }
            }


            return retval;

        }

        private void BindDonationsGrid()
        {
            try
            {

                var getall_unids = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(p => p.unid ==  QueryStringValues.UNID).ToList();

                var otherlist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(p => p.MasterUNID == QueryStringValues.UNID).ToList();
                if (otherlist.Count() >0)
                {
                    getall_unids.AddRange(otherlist);
                }



                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o=> getall_unids.Select(p=>p.unid).Contains( o.FundriserUNID)).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();

                if (QueryStringValues.Type == "cash")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "cash"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.ValueOfGoods,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.CheckNumber,
                                     PaymentType = "Cash",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     Email = t.DonerEmail

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
//                        lblamount.Text = string.Format("{0:F2}", dItem.Amount);
//lblStatus.Text = dItem.Status;
//                        txtname.Text = dItem.Name;
//                        txttype.Text = dItem.PaymentType;
//                        txtemail.Text = dItem.Email;
//                        lblCategories.Text = dItem.CategoryListWithAmount;
//                        txtNotes.Text = dItem.Notes;
//                        hunid.Value = dItem.unid;
//                        Gridfilesbind();

                    }
                }
                else if (QueryStringValues.Type == "inkind")
                {
                    var rlist = (from t in tList
                                 join c in ulist on t.LoggedByID equals c.ID
                                 where t.DonationType == "inkind"
                                 //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = c.ContractorName,
                                     TithigName = t.DetailsOfDonation,
                                     PaidBy = c.ContractorName,
                                     Amount = t.ValueOfGoods,
                                     PaidDate = t.PaidDate,
                                     PayRef = String.Empty,
                                     PaymentType = "In Kind",
                                     REcurring = t.RecurringType,
                                     // Status = (t.IsPaid.HasValue?t.IsPaid.Value:false)? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>"
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                     CategoryListWithAmount = "",
                                     CategoryList = "",
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     Email = t.DonerEmail

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
                        //lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                        //lblStatus.Text = dItem.Status;
                        //txtname.Text = dItem.Name;
                        //txttype.Text = dItem.PaymentType;
                        //txtemail.Text = dItem.Email;
                        //lblCategories.Text = dItem.CategoryListWithAmount;
                        //txtNotes.Text = dItem.Notes;
                        //hunid.Value = dItem.unid;
                        //Gridfilesbind();

                    }

                }
                else
                {
                    //tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                    var rlist = (from t in tList
                                     //join c in ulist on t.LoggedByID equals c.ID
                                     //join tc in tclist on t.TithingID equals tc.ID
                                 select new
                                 {
                                     ID = t.ID,
                                     Name = t.DonerName,// t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName,"name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Email = t.DonerEmail,// t.LoggedByID == 0 ? t.DonerEmail : getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                     TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                     PaidBy = t.LoggedByID == 0 ? t.DonerName : getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                     Amount = t.PaidAmount,
                                     PaidDate = t.PaidDate,
                                     PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                     PaymentType = t.DonationType == null ? (t.RecurringType == null ? "Normal" : "Recurring") : (t.DonationType == "inkind" ? "In Kind" : "Cash"),
                                     REcurring = t.RecurringType,
                                     Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                     //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                    // CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                     //CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                     t.MoreDetails,
                                     t.unid,
                                     t.Notes,
                                     PlatformFee = t.PlatformFee.HasValue ? t.PlatformFee.Value : 0,
                                     TransactionFee = t.TransactionFee.HasValue ? t.TransactionFee.Value : 0,
                                     IsPaid = (t.IsPaid??false)

                                 }).ToList();


                    GridDashboard.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                    GridDashboard.DataBind();
                    var am = rlist.Where(o => o.IsPaid).Sum(o => o.Amount);
                    var cnt = rlist.Where(o => o.IsPaid).Count();
                    lbltotal_donations.Text = string.Format("{1}{0:N2}", am, Deffinity.Utility.GetCurrencySymbol());//string.Format("{0:C2}", am);
                    lbltotal_donors.Text = (rlist.Where(o => o.IsPaid).Count()).ToString();
                    lblaverage_donation.Text = string.Format("{1}{0:N2}", cnt > 0 ? am / cnt : 0, Deffinity.Utility.GetCurrencySymbol());// string.Format("{0:C2}", cnt > 0 ? am / cnt : 0);

                    var dItem = rlist.LastOrDefault();
                    if (dItem != null)
                    {
                        //lblamount.Text = string.Format("{0:F2}", dItem.Amount);
                        //lblStatus.Text = dItem.Status;
                        //txtname.Text = dItem.Name;
                        //txttype.Text = dItem.PaymentType;
                        //txtemail.Text = dItem.Email;
                        //lblCategories.Text = dItem.CategoryListWithAmount;
                        //txtNotes.Text = dItem.Notes;
                        //hunid.Value = dItem.unid;
                        //lbltr.Text = dItem.TransactionFee == 0 ? "NO" : string.Format("{0:F2}", dItem.TransactionFee);
                        //lblpf.Text = dItem.PlatformFee == 0 ? "NO" : string.Format("{0:F2}", dItem.PlatformFee);
                      //  Gridfilesbind();

                    }
                }

                //if (rlist.Count > 0)
                //{
                //    //lblthisweek.Text = string.Format("{0:F2}", rlist.Sum(o=>o.Amount.HasValue?o.Amount.Value:0));
                //    //lblthismonth.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

    }
}