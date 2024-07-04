using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioContactAddressBAL
    {
        public static List<PortfolioMgt.Entity.PortfolioContactAddress> PorfolioContact_Address_SelectAll(int contactID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            return pc.GetAll().Where(o => o.ContactID == contactID).ToList();
        }
        public static PortfolioMgt.Entity.PortfolioContactAddress PorfolioContact_Address_SelectID(int AddressID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pc = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            return pc.GetAll().Where(o => o.ID == AddressID).FirstOrDefault();
        }
        public static List<PortfolioMgt.Entity.v_PortfolioContactAddress> PorfolioContact_Address_ByPortoflioID(int portfolioID)
        {

            IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> pc = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();
            return pc.GetAll().Where(o=>o.PortfolioID == portfolioID).ToList();

        }



        public static bool PorfolioContact_PolicyExists(int callid)
        {
            bool retval = false;


            var ppList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID);
            if (ppList.Count > 0)
            {
                retval = true;
                //check this addess has already existing plan
                //var fdetails = DC.BLL.FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
                //var pEntity = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectID(fdetails.ContactAddressID);
               
                //if (pEntity != null)
                //{
                //    if (pEntity.ExpiryDate.HasValue)
                //    {
                //        if ( (DateTime.Now  < pEntity.ExpiryDate) && (pEntity.Amount >0) )
                //        {
                //            var payEntity = PortfolioMgt.BAL.PortfolioContactPaymentDetailsBAL.PortfolioContactPaymentDetailsBAL_SelectLatestByAddress(fdetails.ContactAddressID, pEntity.PolicyTypeID.HasValue?pEntity.PolicyTypeID.Value:0);
                //            if((payEntity.IsPaid))
                //            retval = true;
                //        }
                //    }

                //}
            }

            return retval;

        }

        public static bool PorfolioContact_PolicyExistsByAddressID(int callid)
        {
            bool retval = false;


            var ppList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID);
            if (ppList.Count > 0)
            {
                retval = true;
                //check this addess has already existing plan
                var fdetails = DC.BLL.FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
                var pEntity = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectID(fdetails.ContactAddressID);

                if (pEntity != null)
                {
                    if (pEntity.ExpiryDate.HasValue)
                    {
                        if ((DateTime.Now < pEntity.ExpiryDate) && (pEntity.Amount > 0))
                        {
                            var payEntity = PortfolioMgt.BAL.PortfolioContactPaymentDetailsBAL.PortfolioContactPaymentDetailsBAL_SelectLatestByAddress(fdetails.ContactAddressID, pEntity.PolicyTypeID.HasValue ? pEntity.PolicyTypeID.Value : 0);
                            if ((payEntity.IsPaid))
                                retval = false;
                            else
                                return true;
                        }
                        else
                        {
                            return true;
                        }
                    }

                }
            }

            return retval;

        }

        //add address if not exists
        public static PortfolioMgt.Entity.PortfolioContactAddress PortfolioContactAddressBAL_add(PortfolioMgt.Entity.PortfolioContactAddress p )
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            var addressExist = pRep.GetAll().Where(o => o.ContactID == p.ContactID && o.Address.ToLower().Contains( p.Address.ToLower())).FirstOrDefault();
            var sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
            if (addressExist == null)
            {

                sResult.ContactID = p.ContactID;
                sResult.Address = p.Address;
                sResult.BillingAddress1 = p.Address; //txtbAddress1.Text.Trim();
                sResult.Address2 = p.Address2 != null? p.Address2: string.Empty;
                sResult.BillingAddress2 = p.Address2 != null ? p.Address2 : string.Empty; //txtbAddress2.Text.Trim();
                sResult.Amount = 0;// Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0") + Convert.ToDouble(!string.IsNullOrEmpty(txtAddons.Text.Trim()) ? txtAddons.Text.Trim() : "0");
                sResult.BillingCity = p.City != null ? p.City : string.Empty; //txtbCity.Text.Trim();
                sResult.City = p.City != null ? p.City : string.Empty;
                sResult.BillingName = string.Empty; //txtbName.Text.Trim();
                sResult.BillingState = p.State != null ? p.State : string.Empty;  //txtbState.Text.Trim();
                sResult.State = p.State != null ? p.State : string.Empty;
                sResult.BillingZipcode = p.PostCode != null ? p.PostCode : string.Empty;  //txtbZipcode.Text.Trim();
                sResult.PostCode = p.PostCode != null ? p.PostCode : string.Empty; ;
                sResult.DaysRemaining = 0;// Convert.ToInt32(!string.IsNullOrEmpty(txtDaysRemaining.Text.Trim()) ? txtDaysRemaining.Text.Trim() : "0");
                sResult.PolicyNumber = string.Empty; //txtPolicynumber.Text;
                sResult.StartDate = null;// Convert.ToDateTime(!string.IsNullOrEmpty(txtStartDate.Text.Trim()) ? txtStartDate.Text.Trim() : DateTime.Now.ToShortDateString());
                sResult.ExpiryDate = null;// Convert.ToDateTime(!string.IsNullOrEmpty(txtExpiryDate.Text.Trim()) ? txtExpiryDate.Text.Trim() : DateTime.Now.ToShortDateString());
                sResult.ContractTermID = null;// Convert.ToInt32(!string.IsNullOrEmpty(ddlContractTerm.SelectedValue) ? ddlContractTerm.SelectedValue : "0");
                sResult.PolicyStartsID = null; //Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyStarts.SelectedValue) ? ddlPolicyStarts.SelectedValue : "0");
                sResult.PolicyTypeID = null;// Convert.ToInt32(!string.IsNullOrEmpty(ddlPolicyType.SelectedValue) ? ddlPolicyType.SelectedValue : "0");
                sResult.PropertyType = string.Empty; // ddlPropertyType.SelectedValue;
                sResult.Other = string.Empty; //txtOther.Text.Trim();
                sResult.IsLessThan5KSqft = null;// btnft.SelectedValue == "1" ? true : false;
                sResult.Notes = string.Empty; //txtNotes.Text;

                sResult.LoggedBy = p.LoggedBy;
                sResult.LoggedDatetime = DateTime.Now;
                pRep.Add(sResult);

            }
            else
            {
                sResult = addressExist;
            }
            return sResult;
        }


        public static PortfolioMgt.Entity.PortfolioContactAddress PortfolioContactAddressBAL_update(PortfolioMgt.Entity.PortfolioContactAddress p)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            var sResult = pRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
          if(sResult != null)
            {

                sResult.BillingName = p.BillingName;
                sResult.BillingCity = p.City;
                sResult.BillingZipcode = p.BillingZipcode;
                sResult.BillingState = p.State;

                pRep.Edit(sResult);

            }
         
            return sResult;
        }

    }
}
