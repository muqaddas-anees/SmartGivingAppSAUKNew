using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.DAL;
using PortfolioMgt.DAL;
using UserMgt.DAL;
using DC.DAL;
using DC.Entity;
using DC.BLL;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class AssignedtoDepartmentCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDeptControl();
                UserChecklistBinding();
                BindDeptUsersGrid();
            }
        }


        public void UserChecklistBinding()
        {
            try
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var ulist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
                    var x = Udc.Contractors.Where(o=>ulist.Contains(o.ID)).Where(a => (a.SID == 1 || a.SID == 2 || a.SID == 3 || a.SID == 4 || a.SID == 9)
                        && (a.Status.ToLower() == "active")).OrderBy(o => o.ContractorName).ToList();
                    checkListUsers.DataSource = x;
                    checkListUsers.DataTextField = "ContractorName";
                    checkListUsers.DataValueField = "ID";
                    checkListUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region Bind Department
        public void BindDeptControl()
        {
            try
            {
                ddlAssignedtoDept.DataSource = FLSDepartment.Bind().Where(d => d.CustomerID == sessionKeys.PortfolioID).ToList();
                ddlAssignedtoDept.DataTextField = "DepartmentName";
                ddlAssignedtoDept.DataValueField = "ID";
                ddlAssignedtoDept.DataBind();
                ddlAssignedtoDept.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        #endregion
        private void HideControls(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
        {
            _ddlItems.Visible = true;
            _txtEdit.Visible = false;
            _imgSubmit.Visible = false;
            _imgCancel.Visible = false;
            _imgAdd.Visible = true;
            _imgDelete.Visible = true;
            _imgEdit.Visible = true;


        }
        private void VisibleControl(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
        {
            _ddlItems.Visible = false;
            _txtEdit.Visible = true;
            _imgSubmit.Visible = true;
            _imgCancel.Visible = true;
            _imgAdd.Visible = false;
            _imgDelete.Visible = false;
            _imgEdit.Visible = false;
            lblMsg.Text = string.Empty;

        }
        protected void btnAddDept_Click(object sender, EventArgs e)
        {
            try
            {
                txtAssignedtoDept.Text = string.Empty;
                VisibleControl(txtAssignedtoDept, ddlAssignedtoDept, btnSubmitDept, btnAddDept, btnEditDept, btnCancelDept, btnDeleteDept);
                txtAssignedtoDept.Focus();
                BtnSubmit.Visible = false;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnEditDept_Click(object sender, EventArgs e)
        {
            try
            {
                AssignedDepartment dept = FLSDepartment.SelectById(int.Parse(ddlAssignedtoDept.SelectedValue));

                if (dept != null)
                {
                    txtAssignedtoDept.Text = dept.DepartmentName;
                    hidDept.Value = dept.ID.ToString();
                    VisibleControl(txtAssignedtoDept, ddlAssignedtoDept, btnSubmitDept, btnAddDept, btnEditDept, btnCancelDept, btnDeleteDept);
                    BtnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSubmitDept_Click(object sender, EventArgs e)
        {
            try
            {
                int customerID = sessionKeys.PortfolioID;
                AssignedDepartment dept = new AssignedDepartment();
                dept.DepartmentName = txtAssignedtoDept.Text.Trim();
                dept.CustomerID = customerID;

                int id = int.Parse(string.IsNullOrEmpty(hidDept.Value) ? "0" : hidDept.Value);
                if (id > 0)
                {
                    bool exists = FLSDepartment.CheckByIdUpdate(id, txtAssignedtoDept.Text.Trim(), customerID);
                    if (!exists)
                    {
                        dept.ID = id;
                        FLSDepartment.Update(dept);
                        lblMsg.Text = "Item updated successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;

                        HideControls(txtAssignedtoDept, ddlAssignedtoDept, btnSubmitDept, btnAddDept, btnEditDept, btnCancelDept, btnDeleteDept);
                        BindDeptControl();
                        hidDept.Value = "0";
                        txtAssignedtoDept.Text = string.Empty;
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    bool exists = FLSDepartment.CheckExists(txtAssignedtoDept.Text.Trim(), customerID);

                    if (!exists)
                    {
                        FLSDepartment.Add(dept);
                        lblMsg.Text = "Item added successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        HideControls(txtAssignedtoDept, ddlAssignedtoDept, btnSubmitDept, btnAddDept, btnEditDept, btnCancelDept, btnDeleteDept);
                        BindDeptControl();
                        hidDept.Value = "0";
                        txtAssignedtoDept.Text = string.Empty;
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCancelDept_Click(object sender, EventArgs e)
        {
            try
            {
                HideControls(txtAssignedtoDept, ddlAssignedtoDept, btnSubmitDept, btnAddDept, btnEditDept, btnCancelDept, btnDeleteDept);
                BindDeptControl();
                BtnSubmit.Visible = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnDeleteDept_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssignedtoDept.SelectedValue != "0")
                {
                    int departmentid = int.Parse(ddlAssignedtoDept.SelectedValue);
                    if (!Departments_IsAssined(departmentid))
                    {
                        FLSDepartment.DeleteById(departmentid);
                        lblMsg.Text = "Item deleted successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindDeptControl();
                    }
                    else
                    {
                        lblError.Text = "Item assigned to request(s).Please check and try again";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Btnall_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssignedtoDept.SelectedValue != "0")
                {
                    List<AssignedDepartment> Dlist = new List<AssignedDepartment>();
                    string Dname = ddlAssignedtoDept.SelectedItem.ToString();
                    using (DCDataContext dc = new DCDataContext())
                    {
                        Dlist = dc.AssignedDepartments.Where(c => c.DepartmentName == Dname).ToList();
                        dc.AssignedDepartments.DeleteAllOnSubmit(Dlist);
                        dc.SubmitChanges();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    LblADmsg.Text = "Department deleted to all customers successfully";
                    BindDeptControl();
                }
                else
                {
                    LblADerror.ForeColor = System.Drawing.Color.Red;
                    //LblADmsg.Text = "Please select one Department";

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnsingle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssignedtoDept.SelectedValue != "0")
                {
                    int departmentid = int.Parse(ddlAssignedtoDept.SelectedValue);
                    if (!Departments_IsAssined(departmentid))
                    {
                        FLSDepartment.DeleteById(departmentid);
                        lblMsg.Text = "Item deleted successfully.";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindDeptControl();
                    }
                    else
                    {
                        lblError.Text = "Item assigned to request(s).Please check and try again";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindDeptUsersGrid()
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    var DepartmentUsersList = Ddc.DepartmentUsers.ToList();
                    var Dlist = Ddc.AssignedDepartments.ToList();
                    var x = (from a in DepartmentUsersList
                             join b in Dlist on a.DeptId equals b.ID
                             where a.CustomerID == sessionKeys.PortfolioID
                             select new
                             {
                                 ID = a.Id,
                                 Department = b.DepartmentName,
                                 ContractorName = NamesWithSpace(a.UserIds)
                             }).ToList();
                    GvMailManager.DataSource = x;
                    GvMailManager.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string NamesWithSpace(string Names)
        {
            string R_Names = string.Empty;
            try
            {
                UserDataContext Udc = new UserDataContext();
                var Userslist = Udc.Contractors.ToList();
                if (Names != string.Empty)
                {
                    string[] userIds = Names.Split(',');
                    for (int i = 0; i <= userIds.Length - 1; i++)
                    {
                        if (R_Names == string.Empty)
                        {
                            R_Names = Userslist.Where(a => a.ID == int.Parse(userIds[i].ToString())).FirstOrDefault().ContractorName;
                        }
                        else
                        {
                            R_Names = R_Names + "," + Userslist.Where(a => a.ID == int.Parse(userIds[i].ToString())).FirstOrDefault().ContractorName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return R_Names;
        }
        public void clearFields()
        {
            checkListUsers.ClearSelection();
            ddlAssignedtoDept.SelectedValue = "0";
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string userIds = string.Empty;
                foreach (ListItem Item in checkListUsers.Items)
                {
                    if (Item.Selected == true)
                    {
                        if (userIds == string.Empty)
                        {
                            userIds = Item.Value;
                        }
                        else
                        {
                            userIds = userIds + "," + Item.Value;
                        }
                    }
                }
                if (userIds != string.Empty)
                {
                    using (DCDataContext Ddc = new DCDataContext())
                    {
                        DepartmentUser D_user = Ddc.DepartmentUsers.Where(a => a.DeptId == int.Parse(ddlAssignedtoDept.SelectedValue)
                            && a.CustomerID == sessionKeys.PortfolioID).FirstOrDefault();
                        if (D_user == null)
                        {
                            DepartmentUser mails = new DepartmentUser();
                            mails.DeptId = int.Parse(ddlAssignedtoDept.SelectedValue);
                            mails.UserIds = userIds;
                            mails.CustomerID = sessionKeys.PortfolioID;
                            mails.CreatedBy = sessionKeys.UID;
                            Ddc.DepartmentUsers.InsertOnSubmit(mails);
                            Ddc.SubmitChanges();

                            //LblADmsg.ForeColor = System.Drawing.Color.Green;
                            LblADmsg.Text = "Added successfully";
                            BindDeptUsersGrid();
                            clearFields();
                        }
                        else
                        {
                            D_user.CreatedBy = sessionKeys.UID;
                            D_user.UserIds = userIds;
                            Ddc.SubmitChanges();
                            //LblADmsg.ForeColor = System.Drawing.Color.Green;
                            LblADmsg.Text = "Updated successfully";
                            BindDeptUsersGrid();
                            clearFields();
                        }
                    }
                }
                else
                {
                    //LblADmsg.ForeColor = System.Drawing.Color.Red;
                    LblADerror.Text = "Please select user";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnDeptCopyToAllCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int customerID = sessionKeys.PortfolioID;
                using (DCDataContext dc = new DCDataContext())
                {
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                        var departmentList = dc.AssignedDepartments.Where(c => c.CustomerID == customerID).ToList();
                        if (departmentList.Count() > 0)
                        {
                            List<AssignedDepartment> dList = new List<AssignedDepartment>();
                            foreach (var c in customerList)
                            {
                                foreach (var d in departmentList)
                                {
                                    bool exists = FLSDepartment.CheckExists(d.DepartmentName, c.ID);
                                    if (!exists)
                                    {
                                        AssignedDepartment assignedDepartment = new AssignedDepartment();
                                        assignedDepartment.CustomerID = c.ID;
                                        assignedDepartment.DepartmentName = d.DepartmentName;
                                        dList.Add(assignedDepartment);
                                    }
                                }
                            }
                            //Bulk insert
                            dc.AssignedDepartments.InsertAllOnSubmit(dList);
                            dc.SubmitChanges();
                            // LblADmsg.ForeColor=
                            LblADmsg.Text = "Successfully copied";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void GvMailManager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete1")
                {
                    DeleteById(int.Parse(e.CommandArgument.ToString()));
                    BindDeptUsersGrid();
                    LblADmsg.Text = "Deleted successfully";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GvMailManager_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GvMailManager_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GvMailManager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvMailManager.PageIndex = e.NewPageIndex;
            BindDeptUsersGrid();
        }
        public void DeleteById(int Id)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    DepartmentUser D_user = Ddc.DepartmentUsers.Where(a => a.Id == Id).FirstOrDefault();
                    Ddc.DepartmentUsers.DeleteOnSubmit(D_user);
                    Ddc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private bool Departments_IsAssined(int departmentid)
        {
            int retval = 0;
            using (DCDataContext dcontext = new DCDataContext())
            {
                retval = (from p in dcontext.FLSDetails
                          where p.DepartmentID == departmentid
                          select p).Count();
            }
            return retval > 0 ? true : false;
        }
    }
}