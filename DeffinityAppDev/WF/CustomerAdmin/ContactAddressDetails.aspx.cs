using DC.BAL;
using PortfolioMgt.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactAddressDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //set default value
                    txtAddons.Text = "0";
                    //payment details
                    EnablePaymentFields(false);
                    BindProductPolicyType();
                    BindPolicyStart();
                    BindPolicyContractTerm();
                    
                    if (Request.QueryString["ContactID"] != null)
                    {
                        btnBack.NavigateUrl = string.Format("~/WF/CustomerAdmin/CustomerAddressList.aspx?ContactID={0}", Request.QueryString["ContactID"].ToString());
                        //btnBack.NavigateUrl = string.Format("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}", Request.QueryString["ContactID"].ToString());
                        BindContactDetails();
                        txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    }
                    //Bind address details
                    if (Request.QueryString["addid"] != null)
                    {
                        pnlAddon.Visible = false;
                        BindAddressDetails(Convert.ToInt32(Request.QueryString["addid"]));
                        BindMailJournal(Convert.ToInt32(Request.QueryString["addid"]));
                    }
                    BindProductAddons();

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindContactDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            var pEntity = pc.GetAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["ContactID"])).FirstOrDefault();
            txtbName.Text = pEntity.Name;
            lblContact.Text = pEntity.Name;
        }
        // check address already exists
        private bool CheckAddressExists(int ContactID , int addressid, string address , string postcode)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            if(addressid >0)
            {
                var pdate = pRepository.GetAll().Where(o => o.ContactID == ContactID && o.Address.ToLower() == address.ToLower().Trim() && o.PostCode.ToLower() == postcode.ToLower() && o.ID != addressid).FirstOrDefault();
                if (pdate != null)
                    retval = true;
            }
            else
            {
                var pdate = pRepository.GetAll().Where(o => o.ContactID == ContactID && o.Address.ToLower() == address.ToLower().Trim() && o.PostCode.ToLower() == postcode.ToLower()).FirstOrDefault();
                if (pdate != null)
                    retval = true;
            }
            return retval;
        }

        private int UpdateAddressDetails(int addressid)
        {
            int addid = 0;
            bool IsAdd = false;

            if (!CheckAddressExists(Convert.ToInt32(Request.QueryString["ContactID"]),addressid, txtAddress1.Text.Trim(), txtZipcode.Text.Trim()))
            {
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();



                PortfolioMgt.Entity.PortfolioContactAddress sResult = pRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                var pResult = paRepository.GetAll().Where(o => o.AddressID == addressid).ToList();

                if (sResult == null)
                {
                    sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                    IsAdd = true;
                }

                //sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                sResult.ContactID = Convert.ToInt32(Request.QueryString["ContactID"]);
                sResult.Address = txtAddress1.Text.Trim();
                sResult.BillingAddress1 = txtbAddress1.Text.Trim();
                sResult.Address2 = txtAddress2.Text.Trim();
                sResult.BillingAddress2 = txtbAddress2.Text.Trim();
                sResult.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0") + Convert.ToDouble(!string.IsNullOrEmpty(txtAddons.Text.Trim()) ? txtAddons.Text.Trim() : "0");
                sResult.BillingCity = txtbCity.Text.Trim();
                sResult.City = txtCity.Text.Trim();
                sResult.BillingName = txtbName.Text.Trim();
                sResult.BillingState = txtbState.Text.Trim();
                sResult.State = txtState.Text.Trim();
                sResult.BillingZipcode = txtbZipcode.Text.Trim();
                sResult.PostCode = txtZipcode.Text.Trim();
                sResult.DaysRemaining = Convert.ToInt32(!string.IsNullOrEmpty(txtDaysRemaining.Text.Trim()) ? txtDaysRemaining.Text.Trim() : "0");
                sResult.PolicyNumber = txtPolicynumber.Text;
                sResult.StartDate = Convert.ToDateTime(!string.IsNullOrEmpty(txtStartDate.Text.Trim()) ? txtStartDate.Text.Trim() : DateTime.Now.ToShortDateString());
                sResult.ExpiryDate = Convert.ToDateTime(!string.IsNullOrEmpty(txtExpiryDate.Text.Trim()) ? txtExpiryDate.Text.Trim() : DateTime.Now.ToShortDateString());
                sResult.ContractTermID = Convert.ToInt32(!string.IsNullOrEmpty(ddlContractTerm.SelectedValue) ? ddlContractTerm.SelectedValue : "0");
                sResult.PolicyStartsID = Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyStarts.SelectedValue) ? ddlPolicyStarts.SelectedValue : "0");
                sResult.PolicyTypeID = Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyType.SelectedValue) ? ddlPolicyType.SelectedValue : "0");
                sResult.PropertyType = ddlPropertyType.SelectedValue;
                sResult.Other = txtOther.Text.Trim();
                sResult.IsLessThan5KSqft = btnft.SelectedValue == "1" ? true : false;
                sResult.Notes = txtNotes.Text;
                //sResult.Deductible = Convert.ToDouble(!string.IsNullOrEmpty(txtDeductible.Text.Trim()) ? txtDeductible.Text.Trim() : "0");
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

                var payDetails = payRes.GetAll().Where(o => o.AddressID == addressid).OrderByDescending(o => o.PayID).FirstOrDefault();
                if (payDetails != null)
                {
                    EnablePaymentFields(true);

                    if (payDetails.IsPaid)
                    {
                        ddlPaymentStatus.SelectedValue = "Paid";
                        btnSave.Visible = false;
                        //btnOnlySave.Visible = false;
                    }
                    else
                        ddlPaymentStatus.SelectedValue = "Not Processed";
                    //pnlWebSiteRef
                    //if (!string.IsNullOrEmpty(payDetails.OrderRef))
                    //{
                    //    pnlWebSiteRef.Visible = true;

                    //}
                    //else
                    //{
                    //    pnlWebSiteRef.Visible = false;
                    //}
                    txtPaypalreference.Text = payDetails.PayPalRef;
                    txtOrderReference.Text = payDetails.OrderRef;
                    EnablePaymentFields(false);
                }




                double mcost = 0.00;
                double ycost = 0.00;
                foreach (var item in listAppliances.Items)
                {
                    CheckBox chkID = item.FindControl("chkID") as CheckBox;
                    var id = (item.FindControl("lblID") as Label).Text;
                    var mc = (item.FindControl("lblMontlyCost") as Label).Text;
                    var yc = (item.FindControl("lblYearlyCost") as Label).Text;
                    if (chkID.Checked)
                    {
                        if (paRepository.GetAll().Where(o => o.AddonID == Convert.ToInt32(id) && o.AddressID == addid).FirstOrDefault() == null)
                        {
                            var caid = new PortfolioMgt.Entity.ProductAddonPriceAssociate();
                            caid.AddonID = Convert.ToInt32(id);
                            caid.AddressID = addid;
                            paRepository.Add(caid);
                        }
                    }
                }
                sResult = pRepository.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
            }
            else
            {
                lblError.Text = "Address already exist. Please try again";
            }
            return addid;
        }
        public void EnablePaymentFields(bool setval)
        {
            ddlPaymentStatus.Enabled = setval;
            txtPaypalreference.ReadOnly = !setval;
            txtOrderReference.ReadOnly = !setval;
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
                txtbAddress1.Text = sResult.BillingAddress1;
                txtAddress2.Text = sResult.Address2;
                txtbAddress2.Text = sResult.BillingAddress2;
                txtAmount.Text = string.Format("{0:F2}", sResult.Amount.HasValue ? sResult.Amount.Value : 0);
                txtbCity.Text = sResult.BillingCity;
                txtCity.Text = sResult.City;
                txtbName.Text = sResult.BillingName;
                txtbState.Text = sResult.BillingState;
                txtState.Text = sResult.State;
                txtbZipcode.Text = sResult.BillingZipcode;
                txtZipcode.Text = sResult.PostCode;
                txtDaysRemaining.Text = (sResult.DaysRemaining.HasValue ? sResult.DaysRemaining.Value : 0).ToString();
                txtPolicynumber.Text = sResult.PolicyNumber;
                txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), sResult.StartDate.HasValue ? sResult.StartDate.Value : DateTime.Now);
                txtExpiryDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), sResult.ExpiryDate.HasValue ? sResult.ExpiryDate.Value : DateTime.Now);
                ddlContractTerm.SelectedValue = (sResult.ContractTermID.HasValue ? sResult.ContractTermID.Value : 0).ToString();
                ddlPolicyStarts.SelectedValue = (sResult.PolicyStartsID.HasValue ? sResult.PolicyStartsID.Value : 0).ToString();
                ddlPolicyType.SelectedValue = (sResult.PolicyTypeID.HasValue ? sResult.PolicyTypeID.Value : 0).ToString();
               // ddlPropertyType.SelectedValue = sResult.PropertyType;
                ddlPropertyType.SelectedIndex = ddlPropertyType.Items.IndexOf(ddlPropertyType.Items.FindByValue(!string.IsNullOrEmpty(sResult.PropertyType) ? sResult.PropertyType : ""));
                txtOther.Text = sResult.Other;
                btnft.SelectedValue = sResult.IsLessThan5KSqft.HasValue ? (sResult.IsLessThan5KSqft.Value == false ? "0" : "1") : "0";
                txtNotes.Text = sResult.Notes;
                txtDeductible.Text = string.Format("{0:F2}", (sResult.Deductible.HasValue ? sResult.Deductible.Value : 0));

            }
            //set addon's
            var addons = paRepository.GetAll().Where(o => o.AddressID == addressid).ToList();
            foreach (var item in listAppliances.Items)
            {
                CheckBox chkID = item.FindControl("chkID") as CheckBox;
                var id = (item.FindControl("lblID") as Label).Text;
                //var mc = (item.FindControl("lblMontlyCost") as Label).Text;
                //var yc = (item.FindControl("lblYearlyCost") as Label).Text;
                //var pentity = prlist.Where(o => o.AddonID == Convert.ToInt32(id)).FirstOrDefault();
                if (addons.Where(o => o.AddonID == Convert.ToInt32(id)).Count() == 1)
                {
                    chkID.Checked = true;
                }
            }
            try
            {
                var payDetails = payRes.GetAll().Where(o => o.AddressID == addressid && o.IsPaid).FirstOrDefault();
                if (payDetails != null)
                {
                    EnablePaymentFields(true);

                    if (payDetails.IsPaid)
                    {
                        ddlPaymentStatus.SelectedValue = "Paid";
                        btnSave.Visible = false;
                        //btnOnlySave.Visible = false;
                        btnSendPolicy.Visible = false;
                        btnDownloadPolicy.Visible = false;
                    }
                    else
                        ddlPaymentStatus.SelectedValue = "Not Processed";


                    txtOrderReference.Text = payDetails.OrderRef;
                    txtPaypalreference.Text = payDetails.PayPalRef;

                    EnablePaymentFields(false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
        private void BindProductPolicyType()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

            var sResult = pRepository.GetAll().Where(o=>o.CustomerID == sessionKeys.PortfolioID).OrderBy(o=>o.Title).ToList();

            var cd = (from s in sResult
                      select new
                      {
                          s.ID,
                          s.Title
                      }).ToList();

            ddlPolicyType.DataSource = cd;
            ddlPolicyType.DataTextField = "Title";
            ddlPolicyType.DataValueField = "ID";
            ddlPolicyType.DataBind();
            ddlPolicyType.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        private void BindPolicyStart()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn>();

            var sResult = pRepository.GetAll().OrderBy(o => o.Value).ToList();

            var cd = (from s in sResult
                      select new
                      {
                          s.PSIID,
                          s.Value
                      }).ToList();

            ddlPolicyStarts.DataSource = cd;
            ddlPolicyStarts.DataTextField = "Value";
            ddlPolicyStarts.DataValueField = "PSIID";
            ddlPolicyStarts.DataBind(); 
            ddlPolicyStarts.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        private void BindPolicyContractTerm()
        {

            IPortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

            var sResult = pRepository.GetAll().OrderBy(o => o.Value).ToList();

            var cd = (from s in sResult
                      select new
                      {
                          s.PCTID,
                          s.Name
                      }).ToList();

            ddlContractTerm.DataSource = cd;
            ddlContractTerm.DataTextField = "Name";
            ddlContractTerm.DataValueField = "PCTID";
            ddlContractTerm.DataBind();
            ddlContractTerm.Items.Insert(0, new ListItem("Please select...", "0"));
            //set default addid
            //if (Request.QueryString["addid"] == null)
            //{
            //    try
            //    {
            //        ddlContractTerm.SelectedValue = "1";
            //    }
            //    catch(Exception ex)
            //    {
            //        LogExceptions.WriteExceptionLog(ex);
            //    }
            //}

        }
        private string GetPolicyType(int? typeid, List<PortfolioMgt.Entity.ProductPolicyType> policylist)
        {
            string retval = string.Empty;
            if (typeid.HasValue)
            {
                var v = policylist.Where(o => o.ID == typeid.Value).FirstOrDefault();
                if(v!= null)
                retval = v.Title;

            }

            return retval;
        }
        private void BindProductAddons()
        {
            try
            {
                PolicyTypeBAL pb = new PolicyTypeBAL();
                var policyList = pb.PolicyType_SelectAll().ToList();
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> prRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();

                var sResult = pRepository.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID && o.ProductPolicyTypeID == Convert.ToInt32(ddlPolicyType.SelectedValue)).ToList();

                var cd = (from s in sResult
                          select new
                          {
                              s.PAPID,
                              s.AddOnDetails,
                              MontlyCost = string.Format("{0:F2}", s.MontlyCost),
                              YearlyCost = string.Format("{0:F2}", s.YearlyCost),
                              Ptype = GetPolicyType(s.ProductPolicyTypeID, policyList),
                              PtypeID = s.ProductPolicyTypeID.HasValue ? s.ProductPolicyTypeID.Value : 0
                          }).ToList();
                //if (Convert.ToInt32(ddlPolicyType.SelectedValue) != 4)
                //{ 
                //listAppliances.DataSource = cd.Where(o=>(o.PtypeID == 0) || (o.PtypeID == Convert.ToInt32(ddlPolicyType.SelectedValue))).ToList();
                //listAppliances.DataBind();
                //}
                //else
                //{
                listAppliances.DataSource = cd.Where(o => (o.PtypeID >= 0)).ToList();
                listAppliances.DataBind();

                //}
                if (Request.QueryString["addid"] != null)
                {
                    var prlist = prRepository.GetAll().Where(o => o.AddressID == Convert.ToInt32(Request.QueryString["addid"].ToString())).ToList();
                    foreach (var item in listAppliances.Items)
                    {
                        CheckBox holder = item.FindControl("chkID") as CheckBox;
                        var id = (item.FindControl("lblID") as Label).Text;
                        var pentity = prlist.Where(o => o.AddonID == Convert.ToInt32(id)).FirstOrDefault();
                        if (pentity != null)
                            holder.Checked = true;
                        else
                            holder.Checked = false;
                    }
                }
                else
                {
                    //foreach (var item in listAppliances.Items)
                    //{
                    //    CheckBox holder = item.FindControl("chkID") as CheckBox;
                    //    Label lblPtype = item.FindControl("lblPtype") as Label;


                    //         if (ddlPolicyType.SelectedValue == "2")
                    //         {
                    //              if(lblPtype.Text == "S"){
                    //                  holder.Checked = true;
                    //              }
                    //         }
                    //         else if (ddlPolicyType.SelectedValue == "3")
                    //         {
                    //             if (lblPtype.Text == "A")
                    //             {
                    //                 holder.Checked = true;
                    //             }
                    //         }
                    //         else if (ddlPolicyType.SelectedValue == "4")
                    //         {
                    //             if (lblPtype.Text == "A" || lblPtype.Text == "S")
                    //             {
                    //                 holder.Checked = true;
                    //             }
                    //         }


                    //}
                    //foreach (var item in listAppliances.Items)
                    //{
                    //    CheckBox holder = item.FindControl("chkID") as CheckBox;
                    //    var id = (item.FindControl("lblID") as Label).Text;
                    //    var pentity = prlist.Where(o => o..AddonID == Convert.ToInt32(id)).FirstOrDefault();
                    //    if (pentity != null)
                    //        holder.Checked = true;
                    //    else
                    //        holder.Checked = false;
                    //}
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //private string GetPolicyType(int? typeid)
        //{
        //    string retval = string.Empty;
        //    if (typeid.HasValue)
        //    {
        //        if (typeid.Value == 0)
        //            retval = "O";
        //        else if (typeid.Value == 2)
        //            retval = "S";
        //        else if (typeid.Value == 3)
        //            retval = "A";
        //    }
        //    else
        //        retval = "O";
        //    return retval;
        //}
        protected void listAppliances_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

            }
        }

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
            if (Request.QueryString["ContactID"] != null)
                Response.Redirect(string.Format("~/WF/CustomerAdmin/PayPalPayflowPro/ProcessPayment.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), addid));
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
             if (Request.QueryString["ContactID"] != null)
            Response.Redirect("~/WF/CustomerAdmin/CustomerAddressList.aspx?ContactID=" + Request.QueryString["ContactID"].ToString(),false);
        }

        protected void btnSendPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["addid"] != null)
                {
                    GeneratePolicy.SendPolicyMail(Convert.ToInt32(Request.QueryString["addid"].ToString()));
                    BindMailJournal(Convert.ToInt32(Request.QueryString["addid"]));
                    lblMsg.Text = "Policy has been sent successfully";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnDownloadPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["addid"] != null)
                {
                    GeneratePolicy.GeneratePolicyNew(Convert.ToInt32(Request.QueryString["addid"].ToString()));

                    GeneratePolicy.DownloadPolicyMail(Convert.ToInt32(Request.QueryString["addid"].ToString()));
                    //lblMsg.Text = "Policy has been sent successfully";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void BindMailJournal(int AddressID)
        {
            try
            {

                var rlist = GeneratePolicy.PortfolioContactAddressMailTrack_Select(AddressID);
                if(rlist.Count >0)
                {
                    pnlSentMailJournal.Visible = true;
                    var dlist = rlist.AsEnumerable().Select((x, index) => new { index= index+1,x.AddressID, x.DisplayData});
                    GridMailTrack.DataSource = dlist;
                    GridMailTrack.DataBind();
                }
                else
                {
                    pnlSentMailJournal.Visible = false;
                }


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlPolicyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProductAddons();
            
            //PolicyTypeBAL pb = new PolicyTypeBAL();
            //var p = pb.PolicyType_SelectBYID(Convert.ToInt32(ddlPropertyType.SelectedValue));
            //if(p != null)
            //{
            //    if(ddlContractTerm.SelectedValue != )
            //    txtAmount.Text = string.Format("{0:F2}", p.Monthly.HasValue ? p.Monthly.Value : 0.00);
            //}
        }
    }
}