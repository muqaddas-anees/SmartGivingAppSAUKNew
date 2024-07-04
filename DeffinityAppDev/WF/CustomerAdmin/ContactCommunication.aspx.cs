using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactCommunication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetContactDetails();
                BindGrid();
            }
        }
        private void SetContactDetails()
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    
                    ////btnuser.Visible = true;
                    ////lbtnUpload.Visible = true;
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    var cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();

                    if (pContact != null)
                    {
                       
                        lblContact.Text = pContact.Name;
                        //    imguser.ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + pContact.ID.ToString();
                        //    // ListBox_setValues(pContact.Tags);
                    }
                   
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid()
        {
            try
            {
                int contactid = Convert.ToInt32(Request.QueryString["ContactID"].ToString());
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    var rList = pd.AssetsSummaryReport(sessionKeys.PortfolioID).Where(o=>o.Assets_ContactID == contactid).ToList();
                    
                    var clist = PortfolioMgt.BAL.EquipmentClientCommunicationBAL.EquipmentClientCommunicationBAL_Select(contactid);

                    GridMail.DataSource = (from c in clist
                                           join e in rList on c.ClientID equals e.Assets_ContactID
                                           select new
                                           {
                                               c.ID,
                                               c.AssetID,
                                               c.ClientID,
                                               c.DateandTimeEmailSent,
                                               c.FromEmail,
                                               c.MailBody,
                                               c.MailSubject,
                                               Equipment = e.Category_Name + " - " + e.SubCategory_Name + " - " + e.ProductModel_ModelName
                                           }).ToList();
                    GridMail.DataBind();
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string DateDisplay(DateTime? d)
        {
            string retval = string.Empty;
            if (d.HasValue)
            {
                retval = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), d.Value);
            }



            return retval;
        }

        protected void GridMail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "emailclient")
                {
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"].ToString());
                    int id = Convert.ToInt32( e.CommandArgument.ToString());

                    var clist = PortfolioMgt.BAL.EquipmentClientCommunicationBAL.EquipmentClientCommunicationBAL_Select(contactid).Where(o=>o.ID == id).FirstOrDefault();
                    if(clist != null)
                    {

                        Emailer em = new Emailer();

                        em.SendingMail(clist.FromEmail, clist.MailSubject, Server.HtmlDecode( clist.MailBody),clist.ToEmail);
                        clist.DateandTimeEmailSent = DateTime.Now;
                        PortfolioMgt.BAL.EquipmentClientCommunicationBAL.EquipmentClientCommunicationBAL_Edit(clist);
                        lblMsg.Text = "Mail has been sent sucessfully";
                        //update sent date

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