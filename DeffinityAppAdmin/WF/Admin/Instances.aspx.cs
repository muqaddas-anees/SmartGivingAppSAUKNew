using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace DeffinityAppDev.WF.Admin
{
    public partial class Instances : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //firt time
                Session[users] = null;
                //check users and update the ower id in Project portfoliio table

                BingGrid();
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

        public void BingGrid(bool getNewData = false)
        {
            try
            {


                var iList = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();
                var tList = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select().ToList();
                var uList = GetUsers(getNewData);

                var rData = (from p in iList
                             join u in uList on p.Owner equals u.ID
                             select new
                             {
                                 ID = u.ID,
                                 InstanceName = p.PortFolio,
                                 Sector = tList.Where(o => o.ID == p.PortfolioTypeID).FirstOrDefault() != null ? tList.Where(o => o.ID == p.PortfolioTypeID).FirstOrDefault().Portfoliotype1 : string.Empty,
                                 Administrator = u.ContractorName,
                                 EmailAddress = u.EmailAddress,
                                 Contact = u.ContactNumber,
                                 Address = u.Address1,
                                 Town = u.Town,
                                 Zipcode = u.PostCode,
                                 PortfolioID = p.ID,
                                 Visibility = p.Visible.HasValue ? p.Visible.Value : false,
                                 CreatedDate = u.EmploymentStartDate,
                                 AllowModules = p.AllowModules.HasValue?p.AllowModules.Value:false
                             }).ToList();

                // var ulist = GetUsers();

                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    rData = rData.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtSearch.Text.ToLower()) : false)
                || (p.EmailAddress != null ? p.EmailAddress.Contains(txtSearch.Text.ToLower()) : false)
                || (p.Administrator != null ? p.Administrator.Contains(txtSearch.Text.ToLower()) : false)).ToList();
                }


                if (QueryStringValues.Type == "all")
                {

                    var resultlist = rData.Where(o => o.InstanceName != "").OrderBy(o => o.InstanceName).ToList();

                    lblNumberofInstances.Text = resultlist.Count().ToString();
                    lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }
                else
                {
                    var resultlist = rData.Where(o => o.InstanceName != "").Where(o => !o.EmailAddress.ToLower().Contains("indra")).OrderBy(o => o.InstanceName).ToList();

                    lblNumberofInstances.Text = resultlist.Count().ToString();
                    lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                GridViewRow row = e.Row;

                // Make sure we aren't in header/footer rows
                if (row.DataItem == null)
                {
                    return;
                }
                else
                {
                    gv = (GridView)row.FindControl("gvInnerUsers");

                    var dataRow = row.DataItem as dynamic;
                    var r1 = dataRow.PortfolioID;
                    var r = Convert.ToInt32(r1);
                    if (r > 0)
                    {
                        gv.DataSource = GetUsers().Where(o => o.CompanyID == r).OrderBy(o => o.ContractorName).ToList();
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if (gv.UniqueID == gvUniqueID)
            //{
            //    gv.PageIndex = gvNewPageIndex;
            //    gv.EditIndex = gvEditIndex;
            //    //Check if Sorting used


            //}


        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            BingGrid();
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var userid = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "password")
                {
                    huid.Value = userid.ToString();
                    mdlManageOptions.Show();

                }
                else if (e.CommandName == "status")
                {
                    var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(userid);

                    if (u.Status == UserMgt.BAL.ContractorStatus.Active)
                        u.Status = UserMgt.BAL.ContractorStatus.InActive;
                    else
                        u.Status = UserMgt.BAL.ContractorStatus.Active;
                    UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(userid, u.Status);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid(true);
                }
                else if (e.CommandName == "Instance")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.Visible.HasValue ? c.Visible.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, true);
                    }

                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if(e.CommandName == "Modules")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.AllowModules.HasValue ? c.AllowModules.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, true);
                    }

                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public string SetCssActiveUsers(string status)
        {
            if (status == UserMgt.BAL.ContractorStatus.Active)
            {
                return "btn btn-gray";
            }
            else
            {
                return "btn btn-secondary";
            }
        }
        public string SetCssActiveInstance(string visibilty)
        {
            if (visibilty.ToLower() == "false")
            {
                return "btn btn-secondary";
            }
            else
            {
                return "btn btn-gray";
            }
        }

        protected void btnSubmitPop_Click(object sender, EventArgs e)
        {
            try
            {
                //var user
                if (!string.IsNullOrEmpty(huid.Value))
                {

                    if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        //update password
                        var u = UserMgt.BAL.ContractorsBAL.Contractor_UpdatePassword(Convert.ToInt32(huid.Value.Trim()), txtPassword.Text.Trim());
                        PasswordSendToMail(u, txtPassword.Text.Trim());
                        txtPassword.Text = string.Empty;
                        huid.Value = string.Empty;
                        lblMsgPop.Text = "Password sent to user successfully";
                        mdlManageOptions.Show();
                    }
                    else
                        lblErrorPop.Text = "Please enter password";

                }
                else
                {
                    lblErrorPop.Text = "Please select user";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnGenaratePassword_Click(object sender, EventArgs e)
        {
            txtPassword.Text = DeffinityManager.Utill.RandomPasswordGenerator.GeneratePassword(8);
        }
        protected void btnCancelPop_Click(object sender, EventArgs e)
        {

        }

        public void PasswordSendToMail(UserMgt.Entity.Contractor con, string password)
        {
            try
            {
                Email em = new Email();
                var v_contact = UserMgt.BAL.ContractorsBAL.v_Contractor_SelectByID(con.ID);
                
                var url = string.Empty;
                var instancetitle = string.Empty;
                var fromemail = string.Empty;
                try
                {
                    var pentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(v_contact.PartnerID.HasValue ? v_contact.PartnerID.Value : 0);
                    if (pentity != null)
                    {
                        url = pentity.ParnerPortal;
                        instancetitle = pentity.Portalname;
                        fromemail = pentity.FromEmail;
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

                string Body = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/Admin/EmailTemplates/PasswordSendingmail.html"));
                Body = Body.Replace("[border]", url + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                Body = Body.Replace("[Logo]", url + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                if (Body.Contains("[name]"))
                {
                    Body = Body.Replace("[name]", con.ContractorName);
                }
                if (Body.Contains("[password]"))
                {
                    Body = Body.Replace("[password]", password);
                }
                if (Body.Contains("[Link]"))
                {
                    Body = Body.Replace("[Link]", url);
                }
                // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                em.SendingMail(fromemail, instancetitle + " " + con.ContractorName + " Password", Body, con.EmailAddress, "");

            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BingGrid();
        }

        protected void btnDeleteContacts_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateGrid()
        {
            int countRow = GridInstances.Rows.Count;
           
            for (int i = 0; i < countRow; i++)
            {
                try
                {
                    GridViewRow row = GridInstances.Rows[i];
                    Label lblID = (Label)row.FindControl("lblPortfolioID");
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if(chk != null)
                    {
                        if (chk.Checked)
                        {
                            var retval = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_DeleteByPortfolioID(Convert.ToInt32(lblID.Text.Trim()));
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                            BingGrid(true);
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
}