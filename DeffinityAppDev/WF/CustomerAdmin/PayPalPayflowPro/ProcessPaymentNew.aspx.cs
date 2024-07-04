
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro
{
    public partial class ProcessPaymentNew : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pcRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm> ptRep = null;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> pmRep = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //return;
                if (Request.QueryString["addid"] != null)
                {
                    paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                    var pEntity = paRep.GetAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["addid"].ToString())).FirstOrDefault();
                    txtAmount.Text = string.Format("{0:F2}", pEntity.Amount.HasValue ? pEntity.Amount.Value : 0.00);
                    hadid.Value = Request.QueryString["addid"].ToString();
                    pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var pcEntity = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();
                    lblHeader.Text = pcEntity.Name + " - " + "Payment";
                }


                //populate month
                string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                ddlMonth.DataSource = Month;
                ddlMonth.DataBind();
                //pre-select one for testing
                ddlMonth.SelectedIndex = 4;

                //populate year
                ddlYear.Items.Add("");
                int Year = DateTime.Now.Year;
                for (int i = 0; i < 10; i++)
                {
                    ddlYear.Items.Add((Year + i).ToString());
                }
                //pre-select one for testing
                ddlYear.SelectedIndex = 3;
            }





        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["addid"] != null)
                {

                    int addressid = Convert.ToInt32(Request.QueryString["addid"].ToString());
                    paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                    var pEntity = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                    ptRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();

                    pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var contact = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();

                    var TermEntity = ptRep.GetAll().Where(o => o.PCTID == pEntity.ContractTermID).FirstOrDefault();
                    if (TermEntity.Name == "Monthly")
                    {
                        //if month is grater than one month 
                        //if (Deffinity.Utility.MonthDifference(pEntity.StartDate.Value, pEntity.ExpiryDate.Value) > 1)
                      //  AddUpdateProfileWithTransaction();
                        //else
                        //    AddUpdateProfileWithTransaction_NonRcurring();
                    }
                    else
                    {
                       // AddUpdateProfileWithTransaction_NonRcurring();
                    }
                }
                //PayMethodNew();
                //OldPayMethod();
                //WorkingPaypal();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }



       


      

        private NameValueCollection GetPayPalCollection(string payPalInfo)
        {
            //place the responses into collection
            NameValueCollection PayPalCollection = new System.Collections.Specialized.NameValueCollection();
            string[] ArrayReponses = payPalInfo.Split('&');

            for (int i = 0; i < ArrayReponses.Length; i++)
            {
                string[] Temp = ArrayReponses[i].Split('=');
                PayPalCollection.Add(Temp[0], Temp[1]);
            }
            return PayPalCollection;
        }
        private string ShowPayPalInfo(NameValueCollection collection)
        {
            string PayPalInfo = "";
            foreach (string key in collection.AllKeys)
            {
                PayPalInfo += "<br /><span class=\"bold-text\">" + key + ":</span> " + collection[key];
            }
            return PayPalInfo;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
                Response.Redirect(string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), Request.QueryString["addid"].ToString()));
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
                Response.Redirect(string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID={0}&addid={1}", Request.QueryString["ContactID"].ToString(), Request.QueryString["addid"].ToString()));
        }
        string _TransactionID = string.Empty;
       
       


       
    }
}