using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Contacts : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //firt time
                //Session[users] = null;
                //check users and update the ower id in Project portfoliio table
                //BindCountry();
                if (QueryStringValues.OrgID != null)
                {
                   
                   if(QueryStringValues.EID >0)
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


                var iList = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectAll(Portfolioid).ToList();
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
                                 p.ID,
                                 p.Name,
                                 p.Email,
                                 p.Telephone,
                                 p.Mobile,
                                 p.Address1,
                                 p.Town,
                                 p.City,
                                 p.Postcode,
                                 OrgID= p.PortfolioID

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
                    var resultlist = rData.Where(o => o.Name != "").OrderBy(o => o.Name).ToList();

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
            pnlAddContact.Visible = !showGrid;
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
            var iList = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_Select(eid);

            txtAddress.Text = iList.Address1;
            txtCity.Text = iList.City;
            txtEmail.Text = iList.Email;
            txtMobile.Text = iList.Mobile;
            txtName.Text = iList.Name;
            txtTown.Text = iList.Town;
            txtZipCode.Text = iList.Postcode;

            hid.Value = eid.ToString();
            lblAddContact.Text = "Edit Contact";
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
            txtCity.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtName.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            hid.Value = "0";
            lblAddContact.Text = "Add Contact";
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
                Response.Redirect("~/App/Contacts.aspx?orgid="+QueryStringValues.OrgID, false);
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
                var id = Convert.ToInt32(hid.Value);
                if (id > 0)
                {
                    if (txtName.Text.Trim().Length > 0)
                    {
                        var iList = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_Select(id);

                        iList.Address1 = txtAddress.Text;
                        iList.City = txtCity.Text;
                        iList.Mobile = txtMobile.Text;
                        iList.Name = txtName.Text;
                        iList.Town = txtTown.Text;
                        iList.Postcode = txtZipCode.Text;
                        iList.Email = txtEmail.Text;

                        PortfolioMgt.BAL.PortfolioContactsBAL.PortfolioContactsBAL_add(iList);
                        Response.Redirect("~/App/Contacts.aspx?orgid=" + QueryStringValues.OrgID, false);
                    }
                }
                else
                {
                    if (txtName.Text.Trim().Length > 0)
                    {
                        var iList = new PortfolioMgt.Entity.PortfolioContact();
                        iList.Address1 = txtAddress.Text;
                        iList.City = txtCity.Text;
                        iList.Mobile = txtMobile.Text;
                        iList.Name = txtName.Text;
                        iList.Town = txtTown.Text;
                        iList.Postcode = txtZipCode.Text;
                        iList.Email = txtEmail.Text;
                        iList.PortfolioID = QueryStringValues.OrgID;

                        PortfolioMgt.BAL.PortfolioContactsBAL.PortfolioContactsBAL_add(iList);
                        //panelVisibility(true);
                        Response.Redirect("~/App/Contacts.aspx?orgid=" + QueryStringValues.OrgID, false);
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