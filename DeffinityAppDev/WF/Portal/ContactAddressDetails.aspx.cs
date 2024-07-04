using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactAddressDetails_portal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //set default value
                    // txtAddons.Text = "0";
                    //payment details
                    EnablePaymentFields(false);
                    //BindProductPolicyType();
                    //BindPolicyStart();
                    //BindPolicyContractTerm();
                    //BindProductAddons();
                    if (Request.QueryString["ContactID"] != null)
                    {
                        //btnBack.NavigateUrl = string.Format("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}", Request.QueryString["ContactID"].ToString());
                        BindContactDetails();
                        //txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    }
                    //Bind address details
                    if (Request.QueryString["addid"] != null)
                    {
                        //pnlAddon.Visible = false;
                        BindAddressDetails(Convert.ToInt32(Request.QueryString["addid"]));
                        //BindMailJournal(Convert.ToInt32(Request.QueryString["addid"]));
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindContactDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            var pEntity = pc.GetAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["ContactID"])).FirstOrDefault();
            // txtbName.Text = pEntity.Name;
            lblContact.Text = pEntity.Name;
        }


        private int UpdateAddressDetails(int addressid)
        {
            bool IsAdd = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();


            PortfolioMgt.Entity.PortfolioContactAddress sResult = pRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
            var pResult = paRepository.GetAll().Where(o => o.AddressID == addressid).ToList();
            int addid = 0;
            if (sResult == null)
            {
                sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                IsAdd = true;
            }

            //sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
            sResult.ContactID = Convert.ToInt32(Request.QueryString["ContactID"]);
            sResult.Address = txtAddress1.Text.Trim();
            sResult.BillingAddress1 = string.Empty; //txtbAddress1.Text.Trim();
            sResult.Address2 = txtAddress2.Text.Trim();
            sResult.BillingAddress2 = string.Empty; //txtbAddress2.Text.Trim();
            sResult.Amount = 0;// Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0") + Convert.ToDouble(!string.IsNullOrEmpty(txtAddons.Text.Trim()) ? txtAddons.Text.Trim() : "0");
            sResult.BillingCity = string.Empty; //txtbCity.Text.Trim();
            sResult.City = txtCity.Text.Trim();
            sResult.BillingName = string.Empty; //txtbName.Text.Trim();
            sResult.BillingState = string.Empty; //txtbState.Text.Trim();
            sResult.State = txtState.Text.Trim();
            sResult.BillingZipcode = string.Empty; //txtbZipcode.Text.Trim();
            sResult.PostCode = txtZipcode.Text.Trim();
            sResult.DaysRemaining = 0;// Convert.ToInt32(!string.IsNullOrEmpty(txtDaysRemaining.Text.Trim()) ? txtDaysRemaining.Text.Trim() : "0");
            sResult.PolicyNumber = string.Empty; //txtPolicynumber.Text;
            sResult.StartDate = DateTime.Now;// Convert.ToDateTime(!string.IsNullOrEmpty(txtStartDate.Text.Trim()) ? txtStartDate.Text.Trim() : DateTime.Now.ToShortDateString());
            sResult.ExpiryDate = DateTime.Now;// Convert.ToDateTime(!string.IsNullOrEmpty(txtExpiryDate.Text.Trim()) ? txtExpiryDate.Text.Trim() : DateTime.Now.ToShortDateString());
            sResult.ContractTermID = 1;// Convert.ToInt32(!string.IsNullOrEmpty(ddlContractTerm.SelectedValue) ? ddlContractTerm.SelectedValue : "0");
            sResult.PolicyStartsID = 1; //Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyStarts.SelectedValue) ? ddlPolicyStarts.SelectedValue : "0");
            sResult.PolicyTypeID = 2;// Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyType.SelectedValue) ? ddlPolicyType.SelectedValue : "0");
            sResult.PropertyType = string.Empty; // ddlPropertyType.SelectedValue;
            sResult.Other = string.Empty; //txtOther.Text.Trim();
            sResult.IsLessThan5KSqft = true;// btnft.SelectedValue == "1" ? true : false;
            sResult.Notes = string.Empty; //txtNotes.Text;
            if (IsAdd)
            {
                sResult.LoggedBy = sessionKeys.UID;
                sResult.LoggedDatetime = DateTime.Now;
                pRepository.Add(sResult);
                Session["msg"] = "Address details added successfully";
            }
            else
            {
                pRepository.Edit(sResult);
                Session["msg"] = "Address details updated successfully";
            }
            //return id
            addid = sResult.ID;

            //var payDetails = payRes.GetAll().Where(o => o.AddressID == addressid).OrderByDescending(o=>o.PayID).FirstOrDefault();
            //if (payDetails != null)
            //{
            //    EnablePaymentFields(true);

            //    if (payDetails.IsPaid)
            //    {
            //        //ddlPaymentStatus.SelectedValue = "Paid";
            //        btnSave.Visible = false;
            //        //btnOnlySave.Visible = false;
            //    }
            //    //else
            //    //    ddlPaymentStatus.SelectedValue = "Not Processed";
            //    //pnlWebSiteRef
            //    //if (!string.IsNullOrEmpty(payDetails.OrderRef))
            //    //{
            //    //    pnlWebSiteRef.Visible = true;

            //    //}
            //    //else
            //    //{
            //    //    pnlWebSiteRef.Visible = false;
            //    //}
            //    //txtPaypalreference.Text = payDetails.PayPalRef;
            //    //txtOrderReference.Text = payDetails.OrderRef;
            //    EnablePaymentFields(false);
            //}




            //double mcost = 0.00;
            //double ycost = 0.00;
            //foreach (var item in listAppliances.Items)
            //{
            //    CheckBox chkID = item.FindControl("chkID") as CheckBox;
            //    var id = (item.FindControl("lblID") as Label).Text;
            //    var mc = (item.FindControl("lblMontlyCost") as Label).Text;
            //    var yc = (item.FindControl("lblYearlyCost") as Label).Text;
            //    if (chkID.Checked)
            //    {
            //        if (paRepository.GetAll().Where(o => o.AddonID == Convert.ToInt32(id) && o.AddressID == addid).FirstOrDefault() == null)
            //        {
            //            var caid = new PortfolioMgt.Entity.ProductAddonPriceAssociate();
            //            caid.AddonID = Convert.ToInt32(id);
            //            caid.AddressID = addid;
            //            paRepository.Add(caid);
            //        }
            //    }
            //}
            sResult = pRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
            return addid;
        }
        public void EnablePaymentFields(bool setval)
        {
            //ddlPaymentStatus.Enabled = setval;
            //txtPaypalreference.ReadOnly = !setval;
            //txtOrderReference.ReadOnly = !setval;
        }
        private void BindAddressDetails(int addressid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            var sResult = pRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();

            if (sResult != null)
            {
                txtAddress1.Text = sResult.Address;
                // txtbAddress1.Text = sResult.BillingAddress1;
                txtAddress2.Text = sResult.Address2;
                //txtbAddress2.Text = sResult.BillingAddress2;
                //txtAmount.Text = string.Format("{0:F2}", sResult.Amount.HasValue ? sResult.Amount.Value : 0);
                //txtbCity.Text = sResult.BillingCity;
                txtCity.Text = sResult.City;
                //txtbName.Text = sResult.BillingName;
                //txtbState.Text = sResult.BillingState;
                txtState.Text = sResult.State;
                //txtbZipcode.Text = sResult.BillingZipcode;
                txtZipcode.Text = sResult.PostCode;
                //txtDaysRemaining.Text = (sResult.DaysRemaining.HasValue ? sResult.DaysRemaining.Value : 0).ToString();
                //txtPolicynumber.Text = sResult.PolicyNumber;
                //txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), sResult.StartDate.HasValue ? sResult.StartDate.Value : DateTime.Now);
                //txtExpiryDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), sResult.ExpiryDate.HasValue ? sResult.ExpiryDate.Value : DateTime.Now);
                //ddlContractTerm.SelectedValue = (sResult.ContractTermID.HasValue ? sResult.ContractTermID.Value : 0).ToString();
                //ddlPolicyStarts.SelectedValue = (sResult.PolicyStartsID.HasValue ? sResult.PolicyStartsID.Value : 0).ToString();
                //ddlPolicyType.SelectedValue = (sResult.PolicyTypeID.HasValue ? sResult.PolicyTypeID.Value : 0).ToString();
                // ddlPropertyType.SelectedValue = sResult.PropertyType;
                //ddlPropertyType.SelectedIndex = ddlPropertyType.Items.IndexOf(ddlPropertyType.Items.FindByValue(!string.IsNullOrEmpty(sResult.PropertyType) ? sResult.PropertyType : ""));
                //txtOther.Text = sResult.Other;
                //btnft.SelectedValue = sResult.IsLessThan5KSqft.HasValue ? (sResult.IsLessThan5KSqft.Value == false ? "0" : "1") : "0";
                //txtNotes.Text = sResult.Notes;

            }
            //set addon's
            //var addons = paRepository.GetAll().Where(o => o.AddressID == addressid).ToList();
            //foreach (var item in listAppliances.Items)
            //{
            //    CheckBox chkID = item.FindControl("chkID") as CheckBox;
            //    var id = (item.FindControl("lblID") as Label).Text;
            //    //var mc = (item.FindControl("lblMontlyCost") as Label).Text;
            //    //var yc = (item.FindControl("lblYearlyCost") as Label).Text;
            //    //var pentity = prlist.Where(o => o.AddonID == Convert.ToInt32(id)).FirstOrDefault();
            //    if (addons.Where(o => o.AddonID == Convert.ToInt32(id)).Count() == 1)
            //    {
            //        chkID.Checked = true;
            //    }
            //}
            //try
            //{
            //    var payDetails = payRes.GetAll().Where(o => o.AddressID == addressid && o.IsPaid).FirstOrDefault();
            //    if (payDetails != null)
            //    {
            //        EnablePaymentFields(true);

            //        if (payDetails.IsPaid)
            //        {
            //            ddlPaymentStatus.SelectedValue = "Paid";
            //            btnSave.Visible = false;
            //            //btnOnlySave.Visible = false;
            //            btnSendPolicy.Visible = true;
            //            btnDownloadPolicy.Visible = true;
            //        }
            //        else
            //            ddlPaymentStatus.SelectedValue = "Not Processed";


            //        txtOrderReference.Text = payDetails.OrderRef;
            //        txtPaypalreference.Text = payDetails.PayPalRef;

            //        EnablePaymentFields(false);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
        }
        //private void BindAddonDetails(int addressid)
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();

        //    var sResult = pRepository.GetAll().Where(o => o.AddressID == addressid).ToList();

        //    if (sResult.Count >0)
        //    {
        //        foreach(var s in sResult)
        //        {

        //        }
        //    }
        //}
        //private void BindProductPolicyType()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

        //    var sResult = pRepository.GetAll().OrderBy(o=>o.Title).ToList();

        //    var cd = (from s in sResult
        //              select new
        //              {
        //                  s.ID,
        //                  s.Title
        //              }).ToList();

        //    ddlPolicyType.DataSource = cd;
        //    ddlPolicyType.DataTextField = "Title";
        //    ddlPolicyType.DataValueField = "ID";
        //    ddlPolicyType.DataBind();
        //    ddlPolicyType.Items.Insert(0, new ListItem("Please select...", "0"));

        //}
        //private void BindPolicyStart()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn>();

        //    var sResult = pRepository.GetAll().OrderBy(o => o.Value).ToList();

        //    var cd = (from s in sResult
        //              select new
        //              {
        //                  s.PSIID,
        //                  s.Value
        //              }).ToList();

        //    ddlPolicyStarts.DataSource = cd;
        //    ddlPolicyStarts.DataTextField = "Value";
        //    ddlPolicyStarts.DataValueField = "PSIID";
        //    ddlPolicyStarts.DataBind();
        //    ddlPolicyStarts.Items.Insert(0, new ListItem("Please select...", "0"));

        //}
        //private void BindPolicyContractTerm()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

        //    var sResult = pRepository.GetAll().OrderBy(o => o.Value).ToList();

        //    var cd = (from s in sResult
        //              select new
        //              {
        //                  s.PCTID,
        //                  s.Name
        //              }).ToList();

        //    ddlContractTerm.DataSource = cd;
        //    ddlContractTerm.DataTextField = "Name";
        //    ddlContractTerm.DataValueField = "PCTID";
        //    ddlContractTerm.DataBind();
        //    ddlContractTerm.Items.Insert(0, new ListItem("Please select...", "0"));

        //}

        //private void BindProductAddons()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
        //    IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> prRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();

        //    var sResult = pRepository.GetAll().ToList();

        //    var cd = (from s in sResult
        //              select new
        //              {
        //                  s.PAPID,
        //                  s.AddOnDetails,
        //                  MontlyCost = string.Format("{0:F2}", s.MontlyCost),
        //                  YearlyCost = string.Format("{0:F2}", s.YearlyCost)
        //              }).ToList();

        //    listAppliances.DataSource = cd;
        //    listAppliances.DataBind();
        //    if (Request.QueryString["addid"] != null)
        //    {
        //        var prlist = prRepository.GetAll().Where(o => o.AddressID == Convert.ToInt32(Request.QueryString["addid"].ToString())).ToList();
        //        foreach (var item in listAppliances.Items)
        //        {
        //            CheckBox holder = item.FindControl("chkID") as CheckBox;
        //            var id = (item.FindControl("lblID") as Label).Text;
        //            var pentity = prlist.Where(o => o.AddonID == Convert.ToInt32(id)).FirstOrDefault();
        //            if (pentity != null)
        //                holder.Checked = true;
        //            else
        //                holder.Checked = false;
        //        }
        //    }

        //}


        //protected void listAppliances_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {

        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int addid = 0;
            if (Request.QueryString["addid"] == null)
                addid = UpdateAddressDetails(0);
            else
            {
                addid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                UpdateAddressDetails(addid);
            }
            //if (Request.QueryString["ContactID"] != null)
            //    Response.Redirect(string.Format("~/WF/CustomerAdmin/PayPalPayflowPro/ProcessPayment.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), addid));
        }

        protected void btnOnlySave_Click(object sender, EventArgs e)
        {
            int addid = 0;
            if (Request.QueryString["addid"] == null)
                addid = UpdateAddressDetails(0);
            else
            {
                addid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                UpdateAddressDetails(addid);
            }
            //if (Request.QueryString["ContactID"] != null)
            //    Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID=" + Request.QueryString["ContactID"].ToString(), false);
        }

        //protected void btnSendPolicy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Request.QueryString["addid"] != null)
        //        {
        //            GeneratePolicy.SendPolicyMail(Convert.ToInt32(Request.QueryString["addid"].ToString()));
        //            BindMailJournal(Convert.ToInt32(Request.QueryString["addid"]));
        //            lblMsg.Text = "Policy has been sent successfully";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}
        //protected void btnDownloadPolicy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Request.QueryString["addid"] != null)
        //        {
        //            GeneratePolicy.GeneratePolicyNew(Convert.ToInt32(Request.QueryString["addid"].ToString()));

        //            GeneratePolicy.DownloadPolicyMail(Convert.ToInt32(Request.QueryString["addid"].ToString()));
        //            //lblMsg.Text = "Policy has been sent successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        //public void BindMailJournal(int AddressID)
        //{
        //    try
        //    {

        //        var rlist = GeneratePolicy.PortfolioContactAddressMailTrack_Select(AddressID);
        //        if(rlist.Count >0)
        //        {
        //            pnlSentMailJournal.Visible = true;
        //            var dlist = rlist.AsEnumerable().Select((x, index) => new { index= index+1,x.AddressID, x.DisplayData});
        //            GridMailTrack.DataSource = dlist;
        //            GridMailTrack.DataBind();
        //        }
        //        else
        //        {
        //            pnlSentMailJournal.Visible = false;
        //        }


        //    }
        //    catch(Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}
    }
}