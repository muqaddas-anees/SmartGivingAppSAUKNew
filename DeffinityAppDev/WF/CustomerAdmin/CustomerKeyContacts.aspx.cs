using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class CustomerKeyContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["ContactID"] != null)
                {
                    BindContactDetails();
                    BindGrid();
                }
            }
        }

        private void BindContactDetails()
        {

            lblContact.Text = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectName(Convert.ToInt32(Request.QueryString["ContactID"]));
        }
        private List<PortfolioMgt.Entity.CustomerKeyContact> MaintenanceScheduleList()
        {
            
            var rlist = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(Convert.ToInt32(Request.QueryString["ContactID"].ToString()));
            
           
            return rlist;
        }
        protected void GridList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;
            var list = MaintenanceScheduleList();
            if (Sortdir == "ASC")
            {
                list = Sort<PortfolioMgt.Entity.CustomerKeyContact>(list, SortExp, SortDirection.Ascending);
            }
            else
            {
                list = Sort<PortfolioMgt.Entity.CustomerKeyContact>(list, SortExp, SortDirection.Descending);
            }
            this.GridList.DataSource = list;
            this.GridList.DataBind();
        }
        /// <summary>
        /// GEt Sorting direction
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PortfolioMgt.Entity.CustomerKeyContact> Sort<TKey>(List<PortfolioMgt.Entity.CustomerKeyContact> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.CustomerKeyContact>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.CustomerKeyContact>();
            }
        }
        protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "recurr")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                   
                }
                else if (e.CommandName == "Edit1")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    var mData = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Select(Convert.ToInt32(hid.Value));
                    if (mData != null)
                    {
                        txtName.Text = mData.Name;
                        txtEmail.Text = mData.EmailAddress;
                        txtMobile.Text = mData.MobileNo;
                        txtPosition.Text = mData.JobTitle;
                        txtPhone.Text = mData.TelephoneNo;
                        
                        mdlExnter.Show();
                    }
                    else
                    {
                        ClearData();
                    }
                }
                
                else if (e.CommandName == "Delete1")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    var retval = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Delete(ID);
                    if (retval)
                    {
                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            try
            {

                var mData = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Select(Convert.ToInt32(hid.Value));
                if (mData != null)
                {
                    mData.EmailAddress = txtEmail.Text.Trim();
                    mData.JobTitle = txtPosition.Text.Trim();
                    mData.MobileNo = txtMobile.Text.Trim();
                    mData.Name = txtName.Text.Trim();
                    mData.TelephoneNo = txtPhone.Text.Trim();

                    PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Update(mData);

                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    ClearData();
                    mdlExnter.Hide();
                    BindGrid();
                }
                else
                {
                    mData = new PortfolioMgt.Entity.CustomerKeyContact();
                    mData.ContactID = Convert.ToInt32(Request.QueryString["ContactID"].ToString());
                    mData.EmailAddress = txtEmail.Text.Trim();
                    mData.JobTitle = txtPosition.Text.Trim();
                    mData.MobileNo = txtMobile.Text.Trim();
                    mData.Name = txtName.Text.Trim();
                    mData.TelephoneNo = txtPhone.Text.Trim();
                    PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Add(mData);

                    lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                    txtName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    
                    txtMobile.Text = string.Empty;
                    txtPhone.Text = string.Empty;
                    txtPosition.Text = string.Empty;
                    //hcontactid.Value = "0";
                    mdlExnter.Hide();
                    BindGrid();
                }
                // Storage_AddUpdate(Convert.ToInt32(hbomid.Value), Convert.ToInt32(ddlsiteInSearch.SelectedValue), Convert.ToInt32(ddlWareshouse.SelectedValue), Convert.ToDouble(txtQtyReceived.Text));
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private void ClearData()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;

            txtMobile.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtPosition.Text = string.Empty;
            hid.Value = "0";
        }
        private void BindGrid()
        {
            try
            {
                List<PortfolioMgt.Entity.CustomerKeyContact> rlist = MaintenanceScheduleList();
                GridList.DataSource = rlist;
                GridList.DataBind();

               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnRemainder_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
            {
                hid.Value = "0";
                hcontactid.Value = Request.QueryString["ContactID"].ToString();
                mdlExnter.Show();
            }
        }
    }
}