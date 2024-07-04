
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class AssociatedSites : System.Web.UI.Page
    {
        public const string users = "users";
        //set default country
        public int default_contry_usa = 190;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (QueryStringValues.OrgID != null)
                    {
                        BindCountry();
                        ddlCountry.SelectedValue = default_contry_usa.ToString();
                        if (QueryStringValues.EID > 0)
                        {
                            panelVisibility(false);
                            EditContact(QueryStringValues.EID);
                        }
                        else
                        {

                            BingGrid(QueryStringValues.OrgID);
                            panelVisibility(true);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));


        }
        public List<UserMgt.Entity.v_contractor> GetUsers(bool getNewdata = false)
        {
            //if (getNewdata)
            //    Session[users] = null;
            //if (Session[users] == null)
            //{
            //    Session[users] = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
            //}

            //return (Session[users] as List<UserMgt.Entity.v_contractor>).ToList();
            return UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
        }
        public void BingGrid(int Portfolioid)
        {
            try
            {
                var iList = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_ByPortoflioID(QueryStringValues.OrgID).ToList();
                //var tList = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
                //var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().ToList();

                //var dlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().ToList();
                //var pb = new PortfolioMgt.BAL.PortfolioContactBAL();
                //var pdlist = pb.PortfolioContact_SelectAll().ToList();


                //var uList = GetUsers(getNewData);

                var rData = (from p in iList
                                 //join u in uList on p.ID equals u.CompanyID
                             select new
                             {
                                ID=  p.AddressID,
                                 Name= p.BillingName,
                                // p.Email,
                                 p.Address,
                                 p.City,
                                 p.State,
                                 p.PostCode,
                                 OrgID = p.PortfolioID

                             }).ToList();

                // var ulist = GetUsers();

                //if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                //{
                //    rData = rData.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.EmailAddress != null ? p.EmailAddress.Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.Administrator != null ? p.Administrator.Contains(txtSearch.Text.ToLower()) : false)).ToList();
                //}


                if (QueryStringValues.Type == "all")
                {

                    var resultlist = rData.Where(o => o.Name != "").OrderBy(o => o.Name).ToList();

                    //lblNumberofInstances.Text = resultlist.Count().ToString();
                    // lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }
                else
                {
                    var resultlist = rData.Where(o => o.Address != "").OrderBy(o => o.Name).ToList();

                    //    if (Convert.ToInt32(ddlCountry.SelectedValue) > 0)
                    //        resultlist = resultlist.Where(o => o.CountryID == Convert.ToInt32(ddlCountry.SelectedValue)).ToList();

                    //    if (txtsearch.Value.Length > 0)
                    //    {
                    //        resultlist = resultlist.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                    //|| (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    //    }

                    //    if (txtst.Value.Length > 0)
                    //    {
                    //        resultlist = resultlist.Where(p => (p.State != null ? p.State.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                    //|| (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    //    }
                    // lblNumberofInstances.Text = resultlist.Count().ToString();
                    //lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["orgid"] != null)
            {
                BingGrid(Convert.ToInt32(Request.QueryString["orgid"].ToString()));
            }

        }
        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {



        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            if (Request.QueryString["orgid"] != null)
            {
                BingGrid(Convert.ToInt32(Request.QueryString["orgid"].ToString()));
            }
        }

        private void panelVisibility(bool showGrid)
        {
            pnlGrid.Visible = showGrid;
            pnlAddSite.Visible = !showGrid;
        }
        protected void GridInstances_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var userid = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "contactedit1")
                {
                    EditContact(userid);
                    // mdlManageOptions.Show();

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void EditContact(int eid)
        {
            // ClearFields();
            var iList = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectID(eid);

            txtAddress.Text = iList.Address;
            txtTown.Text = iList.City;
            txtState.Text = iList.State;
            
            txtName.Text = iList.BillingName;
            
            txtZipCode.Text = iList.PostCode;

            hid.Value = eid.ToString();
            lblAddSite.Text = "Update Associated Site";
            // panelVisibility(false);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            // GridViewRow grv = LnkSeletedRow.NamingContainer as GridViewRow;
            if (grdrow != null)
            {
                Label lblID = (Label)grdrow.FindControl("lblID"); // Suppose employee id is unique
                // now here you can do the coding for displaying record in another Gridview by datasource and databind()
            }
        }
        //btnAddOrganization
        protected void btnAddOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "0";
                panelVisibility(false);
                //mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ClearFields()
        {
            txtAddress.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtState.Text = string.Empty;
            
            txtName.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            hid.Value = "0";
            lblAddSite.Text = "Add Associated Site";
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("~/App/Organization.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/AssociatedSites.aspx?orgid=" + QueryStringValues.OrgID, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnSave_Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var eEntity = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectAll(QueryStringValues.OrgID).FirstOrDefault();
                var id = Convert.ToInt32(hid.Value);
                if (id > 0)
                {
                    if (txtName.Text.Trim().Length > 0)
                    {
                        var iList = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectID(QueryStringValues.EID);

                        iList.ContactID = eEntity.ID;
                        iList.Address = txtAddress.Text;
                        iList.City = txtTown.Text;
                        iList.State = txtState.Text;
                        iList.BillingName = txtName.Text;
                      
                        iList.PostCode = txtZipCode.Text;
                       

                        PortfolioMgt.BAL.PortfolioContactAddressBAL.PortfolioContactAddressBAL_update(iList);
                        Response.Redirect("~/App/AssociatedSites.aspx?orgid=" + QueryStringValues.OrgID, false);
                    }
                }
                else
                {
                    if (txtName.Text.Trim().Length > 0)
                    {
                        var iList = new PortfolioMgt.Entity.PortfolioContactAddress();
                        iList.ContactID = eEntity.ID;
                        iList.Address = txtAddress.Text;
                        iList.City = txtTown.Text;
                       
                        iList.BillingName = txtName.Text;
                        
                        iList.PostCode = txtZipCode.Text;
                       

                        PortfolioMgt.BAL.PortfolioContactAddressBAL.PortfolioContactAddressBAL_add(iList);
                        //panelVisibility(true);
                        Response.Redirect("~/App/AssociatedSites.aspx?orgid=" + QueryStringValues.OrgID, false);
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