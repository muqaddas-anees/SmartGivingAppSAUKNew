using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class CustomerEquimentList : System.Web.UI.Page
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
                if (Request.QueryString["ContactID"] != null)
                {
                    btnAddNewAddress.NavigateUrl = "~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx?ContactID=" + Request.QueryString["ContactID"].ToString();
                }
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
                                            select new { ID = a.AddressID, Name = a.Name + " - " + a.Address + " " + a.City + " " + a.State+ " " + a.PostCode }).ToList();
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
    }
}