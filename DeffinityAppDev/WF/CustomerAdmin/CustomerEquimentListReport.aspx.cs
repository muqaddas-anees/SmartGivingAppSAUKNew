using ClosedXML.Excel;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class CustomerEquimentListReport : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> paRepository = null;
        IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
        PortfolioContact pContact = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // DisplayAddressMsg();
                hsid.Value = sessionKeys.SID.ToString();
                huid.Value = sessionKeys.UID.ToString();
                SetContactDetails();
                BindAddress();

                BindEquipmentReport(sessionKeys.PortfolioID);
                BindAdminUsers();
                if (Request.QueryString["ContactID"] != null)
                {
                    btnAddNewAddress.NavigateUrl = "~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx?ContactID=" + Request.QueryString["ContactID"].ToString();
                }
            }
        }

        private void BindAdminUsers()
        {
            try
            {
                UserMgt.BAL.ContractorsBAL cBal = new UserMgt.BAL.ContractorsBAL();
                var ulist = cBal.Contractor_Select_Admins();
                var rlist = (from u in ulist
                             select new { Value = u.ID, Text = u.ContractorName + " - " + u.EmailAddress }).ToList();
                ddlAdminUsers.DataSource = rlist;
                ddlAdminUsers.DataTextField = "Text";
                ddlAdminUsers.DataValueField = "Value";
                ddlAdminUsers.DataBind();
                ddlAdminUsers.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindAddress()
        {
            int addressid = 0;
            if (Request.QueryString["ContactID"] != null)
            {
                int contactID = Convert.ToInt32(Request.QueryString["ContactID"].ToString());
                paRepository = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();
                var adlist = paRepository.GetAll().Where(o => o.ContactID == contactID).ToList();
                if (Request.QueryString["addid"] != null)
                {
                    addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                }
                else
                {
                    if (adlist.Count > 0)
                    {
                        addressid = adlist.FirstOrDefault().AddressID;
                        LoadData(addressid);
                    }
                }




                if (adlist.Count > 0)
                {
                    ddlAddress.DataSource = (from a in adlist
                                             select new { ID = a.AddressID, Name = a.Name + " - " + a.Address + " " + a.City + " " + a.State + " " + a.PostCode }).ToList();
                    ddlAddress.DataValueField = "ID";
                    ddlAddress.DataTextField = "Name";
                    ddlAddress.DataBind();
                    if (addressid > 0)
                        ddlAddress.SelectedValue = addressid.ToString();
                }
                ddlAddress.Items.Insert(0, new ListItem("Please select...", "0"));
            }

        }
        private void SetContactDetails()
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    pnladdress.Visible = true;
                    ////btnuser.Visible = true;
                    ////lbtnUpload.Visible = true;
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();

                    if (pContact != null)
                    {
                        //    txtAddress.Text = pContact.Address1;
                        //    txtCity.Text = pContact.City;
                        //    txtEmail.Text = pContact.Email;
                        //    txtmobileno.Text = pContact.Mobile;
                        //    txtname.Text = pContact.Name;
                        //    txtPostal.Text = pContact.Postcode;
                        //    txtTelephone.Text = pContact.Telephone;
                        //    txtTown.Text = pContact.Town;
                        lblContact.Text = pContact.Name;
                        //    imguser.ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + pContact.ID.ToString();
                        //    // ListBox_setValues(pContact.Tags);
                    }
                    // Gridbind(Convert.ToInt32(Request.QueryString["ContactID"]));
                }
                else
                {
                    //btnuser.Visible = false;
                    //lbtnUpload.Visible = false;
                    pnladdress.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlAddress.SelectedValue) > 0)
            {
                LoadData(Convert.ToInt32(ddlAddress.SelectedValue));
            }
        }

        private void LoadData(int addid)
        {
            if (addid > 0)
            {
                if (Request.QueryString["ContactID"] != null)
                    Response.Redirect("~/WF/CustomerAdmin/CustomerEquimentList.aspx?ContactID=" + Request.QueryString["ContactID"].ToString() + "&addid=" + addid.ToString());
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void BindEquipmentReport(int portfolioID)
        {
            try
            {
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    var rList = pd.AssetsSummaryReport(portfolioID).ToList();
                    string searchtext = txtSearchNew.Text.Trim();
                    if (!string.IsNullOrEmpty(txtSearchNew.Text.Trim()))
                    {
                        rList = rList.Where(p => (
                         (p.PortfolioContact_Name != null ? p.PortfolioContact_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.PortfolioContactAddress_Address != null ? p.PortfolioContactAddress_Address.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Category_Name != null ? p.Category_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.SubCategory_Name != null ? p.SubCategory_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.ProductModel_ModelName != null ? p.ProductModel_ModelName.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Assets_SerialNo != null ? p.Assets_SerialNo.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.WarrantyTerm_Name != null ? p.WarrantyTerm_Name.ToLower().Contains(searchtext.ToLower()) : false)
                 || (p.Assets_FromNotes != null ? p.Assets_FromNotes.ToLower().Contains(searchtext.ToLower()) : false)
                        )).ToList();
                    }
                    GridEqipment.DataSource = rList;
                    GridEqipment.DataBind();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected string DateDisplay(DateTime? d)
        {
            string retval = string.Empty;
            if (d.HasValue)
            {
                retval = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.Value);
            }



            return retval;
        }
        protected string DateExpiredDisplay(DateTime? d)
        {
            string retval = string.Empty;
            if (d.HasValue)
            {
                if (DateTime.Now > d.Value)
                    return "Expired";
                else

                    retval = string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.Value);
            }



            return retval;
        }
        //Expired
        protected string GetImage(int? d)
        {
            string retval = string.Empty;
            if (d.HasValue)
            {
                retval = string.Format("~/WF/UploadData/Assets/{0}.png", d.Value);
            }

            return retval;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindEquipmentReport(sessionKeys.PortfolioID);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



        #region Download 
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("worksheet1");

                int x = 2;
                // Title
                ws.Cell("A1").Value = "Name";
                ws.Cell("B1").Value = "Address";
                ws.Cell("C1").Value = "Equipment Brand";
                ws.Cell("D1").Value = "Equipmet Type";
                ws.Cell("E1").Value = "Model";
                ws.Cell("F1").Value = "Serial Number";
                ws.Cell("G1").Value = "Purchase Date";
                ws.Cell("H1").Value = "Warranty Term";
                ws.Cell("I1").Value = "Expiry Date";
                ws.Cell("J1").Value = "Notes";


                ws.Columns(1, 10).AdjustToContents();
                var rngTable = ws.Range("A1:J1");
                // var rngHeaders = rngTable.Range("A2:I2");
                rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngTable.Style.Font.Bold = true;
                rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;

                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    var rList = pd.AssetsSummaryReport(sessionKeys.PortfolioID).ToList();
                    string searchtext = txtSearchNew.Text.Trim();
                    if (!string.IsNullOrEmpty(txtSearchNew.Text.Trim()))
                    {
                        rList = rList.Where(p => (
                         (p.PortfolioContact_Name != null ? p.PortfolioContact_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.PortfolioContactAddress_Address != null ? p.PortfolioContactAddress_Address.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Category_Name != null ? p.Category_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.SubCategory_Name != null ? p.SubCategory_Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.ProductModel_ModelName != null ? p.ProductModel_ModelName.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Assets_SerialNo != null ? p.Assets_SerialNo.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.WarrantyTerm_Name != null ? p.WarrantyTerm_Name.ToLower().Contains(searchtext.ToLower()) : false)
                 || (p.Assets_FromNotes != null ? p.Assets_FromNotes.ToLower().Contains(searchtext.ToLower()) : false)
                        )).ToList();
                    }
                    int sCount = 2;
                    foreach (var item in rList)
                    {
                        ws.Cell("A" + sCount.ToString()).Value = item.PortfolioContact_Name;
                        ws.Cell("B" + sCount.ToString()).Value = item.PortfolioContactAddress_Address;
                        ws.Cell("C" + sCount.ToString()).Value = item.Category_Name;
                        ws.Cell("D" + sCount.ToString()).Value = item.SubCategory_Name;
                        ws.Cell("E" + sCount.ToString()).Value = item.ProductModel_ModelName;
                        ws.Cell("F" + sCount.ToString()).Value = item.Assets_SerialNo;
                        ws.Cell("G" + sCount.ToString()).Value = string.Format(Deffinity.systemdefaults.GetStringDateformat(), item.Assets_PurchasedDate);
                        ws.Cell("H" + sCount.ToString()).Value = item.WarrantyTerm_Name;
                        ws.Cell("I" + sCount.ToString()).Value = string.Format(Deffinity.systemdefaults.GetStringDateformat(), item.Assets_ExpDate);
                        ws.Cell("J" + sCount.ToString()).Value = item.Assets_FromNotes;
                        sCount++;
                    }
                }


                HttpResponse httpResponse = HttpContext.Current.Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", "attachment;filename=\"EquipmentData.xlsx\"");

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion

        protected void GridEqipment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "emailclient")
                {
                    LogExceptions.LogException("start");
                    hAssetID.Value = e.CommandArgument.ToString();
                    string body = getDefaultEquipemntMailContent();
                    LogExceptions.LogException("read html");
                    UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                    //var getUser = cb.Contractor_Select_ByID(Convert.ToInt32(ddlAdminUsers.SelectedValue));
                    LogExceptions.LogException("get user details");
                    var porfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                    using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                    {
                        var rEntity = pd.AssetsSummaryReport(sessionKeys.PortfolioID).Where(o => o.Assets_ID == Convert.ToInt32(hAssetID.Value)).FirstOrDefault();
                        if (rEntity != null)
                        {
                            try
                            {
                               // body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                                body = body.Replace("[name]", rEntity.PortfolioContact_Name);

                                body = body.Replace("[instance]", porfolio.PortFolio);
                                body = body.Replace("[equipment]", rEntity.Category_Name + " - " + rEntity.SubCategory_Name + " - " + rEntity.ProductModel_ModelName);

                                body = body.Replace("[expiry]", string.Format("{0:dd/MM/yyyy}", rEntity.Assets_ExpDate.HasValue ? rEntity.Assets_ExpDate.Value : DateTime.Now));
                                //body = body.Replace("[contact]", getUser.ContactNumber);
                                txtsubject.Value = "Warranty expiry notification";

                                CKEditor1.Text = body;
                                txtto.Value = rEntity.PortfolioContact_Email;

                                //hFromEmail.Value = getUser.EmailAddress;
                                hClientID.Value = rEntity.PortfolioContact_ID.Value.ToString();
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                           
                        }

                        mdlManageOptions.Show();
                        LogExceptions.LogException("show popup");
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try {
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                var getUser = cb.Contractor_Select_ByID(Convert.ToInt32(ddlAdminUsers.SelectedValue));

                string body = CKEditor1.Text;
                body = body.Replace("[contact]", getUser.ContactNumber);
                body = body.Replace("[logo]", "<img src='"+ Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo()+ "' />");

                hFromEmail.Value = getUser.EmailAddress;
                var p = new PortfolioMgt.Entity.EquipmentClientCommunication();
                p.AssetID = Convert.ToInt32(hAssetID.Value);
                p.ClientID = Convert.ToInt32( hClientID.Value);
                p.DateandTimeEmailSent = DateTime.Now;
                p.FromEmail = hFromEmail.Value;
                p.MailSubject = txtsubject.Value;
                p.MailSentByID = sessionKeys.UID;
                p.ToEmail = txtto.Value;
                p.MailBody = Server.HtmlEncode(body);

                PortfolioMgt.BAL.EquipmentClientCommunicationBAL.EquipmentClientCommunicationBAL_Add(p);

                Emailer em = new Emailer();

                em.SendingMail(hFromEmail.Value, txtsubject.Value, body, txtto.Value);

                mdlManageOptions.Hide();
                hAssetID.Value = string.Empty;
                hClientID.Value = string.Empty;
                hFromEmail.Value = string.Empty;
                txtsubject.Value = string.Empty;
                txtto.Value = string.Empty;
                CKEditor1.Text = string.Empty;
                ddlAdminUsers.SelectedIndex = 0;

                //send mail

               


                lblMsg.Text = "Mail has been sent sucessfully";
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getDefaultEquipemntMailContent()
        {
            Emailer em = new Emailer();
            return em.ReadFile("~/WF/CustomerAdmin/EmailTemplates/equipment_default.html");
        }

    }
}