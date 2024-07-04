using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class SigUp_Form : System.Web.UI.Page
    {
        string orgref = "";
        int orgid = 0;
        int edid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if(!IsPostBack) {
                    if (Request.QueryString["orgref"] != null)

                    {

                        var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgref"].ToString()).FirstOrDefault();
                        if (rData != null)
                        {
                            orgid = rData.ID;
                            orgref = rData.OrgarnizationGUID;
                            var Is_senttoCardconnect = rData.SignatureSentToCardConnect;

                            //if (Is_senttoCardconnect.HasValue)
                            //{
                            //    btnSubmitDetails.Visible = !Is_senttoCardconnect.Value;
                            //}

                            //if (Request.QueryString["v"] != null)
                            //{
                            //    btnSubmitDetails.Visible = false;
                            //    //form1.dis
                            //}
                        }


                        var eData = ESignatureDetailBAL.ESignatureDetailBAL_SelectAll().Where(o => o.OrganizationGuid == Request.QueryString["orgref"].ToString()).FirstOrDefault();
                        if (eData != null)
                        {
                            if (sessionKeys.Message.Length > 0)
                            {
                                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                                sessionKeys.Message = "";
                            }

                            txtBusinessLegalName.Text = eData.BusinessLegalName;
                            txtDbaName.Text = eData.DBAName;
                            txtBusinessAddress.Text = eData.BusinessAddress;
                            txtCity.Text = eData.BusinessCity;
                            txtState.Text = eData.BusinessState;
                            txtZip.Text = eData.BusinessZip;
                            txtBusinessPhone.Text = eData.BusinessPhone;
                            txtBusinessFax.Text = eData.BusinessFax;
                            txtEmail.Text = eData.EmailEmail;
                            txtRequestedStartDate.Text = eData.RequestedStartDate.HasValue ? eData.RequestedStartDate.Value.ToShortDateString() : "";


                            txtServicesSold.Text = eData.ServicesSold;
                            txtFedTaxID.Text = eData.FedTaxID;
                            txtFedIDStarted.Text = eData.FedIDStarted;
                            txtTaxFilingType.Text = eData.TaxFilingType;


                            // not asigned
                            txtTaxFilingType.Text = eData.TaxFilingType;
                            txtNumberofEmployees.Text = eData.NumberofEmployees.HasValue ? eData.NumberofEmployees.Value.ToString() : "";
                            txtEstimatedAverage.Text = eData.Est_Avg_Cred_card_Indi_Sale_Amount.HasValue ? string.Format("{0:F2}", eData.Est_Avg_Cred_card_Indi_Sale_Amount.Value) : "";
                            txtEstimatedHighest.Text = eData.Est_Hig_Cred_card_Indi_Sale_Amount.HasValue ? string.Format("{0:F2}", eData.Est_Hig_Cred_card_Indi_Sale_Amount.Value) : "";



                            txtSignorOwnerName.Text = eData.Signor_OwnerName;
                            txtSignorTitle.Text = eData.SignorTitle;

                            txtSignorDateofBirth.Text = eData.SignorDateofBirth.HasValue ? eData.SignorDateofBirth.Value.ToShortDateString() : "";
                            txtSignorHomePhone.Text = eData.SignorHomePhone;
                            txtSignorHomeAddress.Text = eData.SignorHomeAddress;
                            txtSignorCity.Text = eData.SignorCity;
                            txtSignorState.Text = eData.SignorState;
                            txtSignorZip.Text = eData.SignorZip;



                            txtBankName.Text = eData.BankName;
                            txtBankAccount.Text = eData.BankAccount;
                            txtBankRouting.Text = eData.BankRouting;
                            txtBankPhone.Text = eData.BankPhone;
                            chk_date_flexible_no_12.Checked = eData.IsthisDateFlexible.Trim().Length > 0 ? (eData.IsthisDateFlexible.Trim() == "No" ? true : false) : false;
                            chk_date_flexible_yes_12.Checked = eData.IsthisDateFlexible.Trim().Length > 0 ? (eData.IsthisDateFlexible.Trim() == "Yes" ? true : false) : false;
                            chk_tax_no_12.Checked = eData.TaxExemptOrganization.Trim().Length > 0 ? (eData.TaxExemptOrganization.Trim() == "No" ? true : false) : false;
                            chk_tax_yes_12.Checked = eData.TaxExemptOrganization.Trim().Length > 0 ? (eData.TaxExemptOrganization.Trim() == "Yes" ? true : false) : false;
                            chkACHPayment.Checked = eData.ACHPayment.HasValue?eData.ACHPayment.Value:false;
                        
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }



       private void SaveDetails()
        {
            try
            {
                string orgref = "";
                int orgid = 0;
                int edid = 0;

                if (Request.QueryString["orgref"] != null)

                {
                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgref"].ToString()).FirstOrDefault();
                    if(rData != null)
                    {
                        orgid = rData.ID;
                        orgref = rData.OrgarnizationGUID;
                    }

                    var eData = ESignatureDetailBAL.ESignatureDetailBAL_SelectAll().Where(o => o.OrganizationGuid == Request.QueryString["orgref"].ToString()).FirstOrDefault();
                    if(eData != null)
                    {
                        edid = eData.ESignatureID;


                    }

                }

                string BusinessLegalName = txtBusinessLegalName.Text;
                string DbaName = txtDbaName.Text;
                string BusinessAddress = txtBusinessAddress.Text;
                string City = txtCity.Text;
                string State = txtState.Text;
                string Zip = txtZip.Text;
                string BusinessPhone = txtBusinessPhone.Text;
                string BusinessFax = txtBusinessFax.Text;
                string Email = txtEmail.Text;
                string RequestedStartDate = txtRequestedStartDate.Text;

                // not asigned 
                string Isthisdateflexible = txtRequestedStartDate.Text;
                string ServicesSold = txtServicesSold.Text;
                string FedTaxID = txtFedTaxID.Text;
                string FedIDStarted = txtFedIDStarted.Text;
                string TaxFilingType = txtTaxFilingType.Text;


                // not asigned
                string TaxExemptOrganization = txtTaxFilingType.Text;
                string NumberofEmployees = txtNumberofEmployees.Text;
                string EstimatedAverageCreditCardIndividualSaleAmount = txtEstimatedAverage.Text;
                string EstimatedHighestCreditCardIndividualSaleAmount = txtEstimatedHighest.Text;



                string SignorOwnerName = txtSignorOwnerName.Text;
                string SignorTitle = txtSignorTitle.Text;

                string SignorDateofBirth = txtSignorDateofBirth.Text;
                string SignorHomePhone = txtSignorHomePhone.Text;
                string SignorHomeAddress = txtSignorHomeAddress.Text;
                string SignorCity = txtSignorCity.Text;
                string SignorState = txtSignorState.Text;
                string SignorZip = txtSignorZip.Text;



                string BankName = txtBankName.Text;
                string BankAccount = txtBankAccount.Text;
                string BankRouting = txtBankRouting.Text;
                string BankPhone = txtBankPhone.Text;


                var v = new PortfolioMgt.Entity.ESignatureDetail();

                v.BusinessCity = txtCity.Text.Trim();
                v.BusinessState = txtState.Text.Trim();
                v.SignorCity = txtSignorCity.Text.Trim();
                v.SignorState = txtSignorState.Text.Trim();



                v.BusinessLegalName = BusinessLegalName;
                v.DBAName = DbaName;
                v.BusinessAddress = BusinessAddress;
                v.BusinessZip = Zip;
                v.BusinessPhone = BusinessPhone;
                v.BusinessFax = BusinessFax;
                v.EmailEmail = Email;
                if(RequestedStartDate.Length >0)
                v.RequestedStartDate = Convert.ToDateTime(RequestedStartDate);

                v.IsthisDateFlexible = chk_date_flexible_yes_12.Checked ? chk_date_flexible_yes_12.Value.Trim() : (chk_date_flexible_no_12.Checked ? chk_date_flexible_no_12.Value.Trim() : "");// (:(chk_date_flexible_no_12.Checked? chk_date_flexible_no_12.Value:""))

                v.ServicesSold = ServicesSold;
                v.FedTaxID = FedTaxID;

                v.FedIDStarted = FedIDStarted;

                v.TaxFilingType = TaxFilingType;

                v.TaxExemptOrganization = chk_tax_yes_12.Checked ? chk_tax_yes_12.Value.Trim() : (chk_tax_no_12.Checked ? chk_tax_no_12.Value.Trim() : "");

                v.NumberofEmployees = Convert.ToInt32(NumberofEmployees.Length >0?NumberofEmployees:"0");

                v.Est_Avg_Cred_card_Indi_Sale_Amount = Convert.ToDecimal(EstimatedAverageCreditCardIndividualSaleAmount.Length >0?EstimatedAverageCreditCardIndividualSaleAmount:"0");

                v.Est_Hig_Cred_card_Indi_Sale_Amount = Convert.ToDecimal(EstimatedHighestCreditCardIndividualSaleAmount.Length >0? EstimatedHighestCreditCardIndividualSaleAmount:"0" );

                v.Signor_OwnerName = SignorOwnerName;
                v.SignorTitle = SignorTitle;
                if(SignorDateofBirth.Length >0)
                v.SignorDateofBirth = Convert.ToDateTime(SignorDateofBirth);
                v.SignorHomePhone = SignorHomePhone;
                v.SignorHomeAddress = SignorHomeAddress;
                v.SignorZip = SignorZip;

                v.BankName = BankName;
                v.BankAccount = BankAccount;
                v.BankRouting = BankRouting;
                v.BankPhone = BankPhone;

                v.OrganizationGuid = orgref;
                v.OrganizationID = orgid;
                v.ACHPayment = chkACHPayment.Checked;


                IPortfolioRepository<PortfolioMgt.Entity.ESignatureDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ESignatureDetail>();
                if(edid ==0)
                pRep.Add(v);
                else
                {
                   // v.ESignatureID = edid;
                   // pRep.Edit(v);
                    ESignatureDetailBAL.ESignatureDetailBAL_Update(v);
                }

                if(v.ESignatureID >0)
                {
                    sessionKeys.Message = "Thank you. Sending to Card Card to process your application.";
                    //send mail to admins
                    //DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Application submitted successfully", "OK");

                    MID_BAL.SendMailToAdmin(orgref);
                    Response.Redirect(Request.RawUrl, false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitDetails_Click(object sender, EventArgs e)
        {


            try
            {

                SaveDetails();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }



        }
    }
}