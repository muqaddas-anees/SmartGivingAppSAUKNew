using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using UserMgt.DAL;
using UserMgt.DAL;
using PortfolioMgt.DAL;

public partial class DC_controls_MailSendingWithPriorityCntl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ddlBind();
                UserChecklistBinding();
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    //public List<ListItem> Priority()
    //{
    //    List<ListItem> li = new List<ListItem>();
    //    li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
    //    li.Add(new System.Web.UI.WebControls.ListItem("Frist", "1"));
    //    li.Add(new System.Web.UI.WebControls.ListItem("Second", "2"));
    //    return li;
    //}
    public void UserChecklistBinding()
    {
        try 
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                var ulist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
                var x = Udc.Contractors.Where(o=>ulist.Contains(o.ID)).Where(a => (a.SID == 1 || a.SID == 2 || a.SID == 3) && (a.Status.ToLower() == "active")).OrderBy(o=>o.ContractorName).ToList();
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
    public void ddlBind()
    {
        try
        {
            using (DCDataContext Ddc = new DCDataContext())
            {
                ddlPriority.DataSource = Ddc.PriorityLevels.ToList();
                ddlPriority.DataValueField = "Id";
                ddlPriority.DataTextField = "Value";
                ddlPriority.DataBind();
                ddlPriority.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
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
                    if (Ddc.MailSendingWithPriorities.Where(a => a.PriorityId == int.Parse(ddlPriority.SelectedValue) && a.CustomerID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        MailSendingWithPriority mails = new MailSendingWithPriority();
                        mails.PriorityId = int.Parse(ddlPriority.SelectedValue);
                        mails.UserID = userIds;
                        mails.CustomerID = sessionKeys.PortfolioID;

                        Ddc.MailSendingWithPriorities.InsertOnSubmit(mails);
                        Ddc.SubmitChanges();

                        //Lblmsg.ForeColor = System.Drawing.Color.Green;
                        Lblmsg.Text = "Added successfully";
                        BindGrid();
                        clearFields();
                    }
                    else
                    {
                        //Lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblErrorMsg.Text = "Priority already exists";
                    }
                }
            }
            else
            {
                //Lblmsg.ForeColor = System.Drawing.Color.Red;
                lblErrorMsg.Text = "Please select user";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void clearFields()
    {
        ddlPriority.SelectedValue = "0";
        checkListUsers.ClearSelection();
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
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
                    if (Ddc.MailSendingWithPriorities.Where(a => a.PriorityId == int.Parse(ddlPriority.SelectedValue) && a.CustomerID == sessionKeys.PortfolioID).Count() == 1)
                    {
                        MailSendingWithPriority mails = Ddc.MailSendingWithPriorities.Where(a => a.ID == int.Parse(lblRecordId.Text)).FirstOrDefault();
                        mails.PriorityId = int.Parse(ddlPriority.SelectedValue);
                        mails.UserID = userIds;
                        // mails.CustomerID = sessionKeys.PortfolioID;
                        Ddc.SubmitChanges();
                        //Lblmsg.ForeColor = System.Drawing.Color.Green;
                        Lblmsg.Text = "Updated successfully";
                        BindGrid();
                        clearFields();
                    }
                    else
                    {
                        //Lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblErrorMsg.Text = "Priority already exists";
                    }
                }
            }
            else
            {
                //Lblmsg.ForeColor = System.Drawing.Color.Red;
                lblErrorMsg.Text = "Please select user";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            using (DCDataContext Ddc = new DCDataContext())
            {
                var MailSendingList = Ddc.MailSendingWithPriorities.ToList();
                var Plist = Ddc.PriorityLevels.ToList();
                var x = (from a in MailSendingList
                         join b in Plist on a.PriorityId equals b.Id
                         where a.CustomerID == sessionKeys.PortfolioID
                         select new
                         {
                             ID = a.ID,
                             Priority = b.Value,
                             ContractorName = NamesWithSpace(a.UserID)
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
    public void SetEditMode(int RecordId)
    {
        try
        {
            using (DCDataContext Ddc = new DCDataContext())
            {
                MailSendingWithPriority mails = new MailSendingWithPriority();
                mails = Ddc.MailSendingWithPriorities.Where(a => a.ID == RecordId).FirstOrDefault();
                if (mails != null)
                {
                    ddlPriority.SelectedValue = mails.PriorityId.HasValue ? mails.PriorityId.Value.ToString() : "0";
                    string userId = mails.UserID.ToString();
                    string[] userIds = userId.Split(',');
                    for (int i = 0; i <= userIds.Length-1; i++)
                    {
                        foreach (ListItem Item in checkListUsers.Items)
                        {
                            if (Item.Value == userIds[i].ToString())
                            {
                                Item.Selected = true;
                            }
                        }
                    }
                }
                BtnUpdate.Visible = true;
                BtnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void DeleteById(int Id)
    {
        try 
        {
            using (DCDataContext Ddc = new DCDataContext())
            {
                MailSendingWithPriority mails = new MailSendingWithPriority();
                mails = Ddc.MailSendingWithPriorities.Where(a => a.ID == Id).FirstOrDefault();
                Ddc.MailSendingWithPriorities.DeleteOnSubmit(mails);
                Ddc.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void DeleteToAllCustomer(int Id)
    {
        try
        {
            using (DCDataContext dc = new DCDataContext())
            {
                List<MailSendingWithPriority> l = new List<MailSendingWithPriority>();
                int PriorityId = dc.MailSendingWithPriorities.Where(r => r.ID == Id).Select(a => a.PriorityId.HasValue ? a.PriorityId.Value : 0).FirstOrDefault();
                l = (from a in dc.MailSendingWithPriorities where a.PriorityId == PriorityId select a).ToList();
                dc.MailSendingWithPriorities.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
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
                BindGrid();
                //Lblmsg.ForeColor = System.Drawing.Color.Green;
                Lblmsg.Text = "Deleted successfully";
            }
            if (e.CommandName == "DeleteAll")
            {
                DeleteToAllCustomer(int.Parse(e.CommandArgument.ToString()));
                BindGrid();
                //Lblmsg.ForeColor = System.Drawing.Color.Green;
                Lblmsg.Text = "Deleted successfully";
            }
            if (e.CommandName == "Edit")
            {
                checkListUsers.ClearSelection();
                lblRecordId.Text = e.CommandArgument.ToString();
                SetEditMode(int.Parse(lblRecordId.Text));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GvMailManager_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GvMailManager_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnCopyToallCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (DCDataContext ddc = new DCDataContext())
                {
                    var GroupMailsList = ddc.MailSendingWithPriorities.Where(a => a.CustomerID == sessionKeys.PortfolioID).ToList();
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != sessionKeys.PortfolioID).ToList();
                    if (GroupMailsList.Count > 0)
                    {
                        List<MailSendingWithPriority> mList = new List<MailSendingWithPriority>();
                        foreach (var c in customerList)
                        {
                            foreach (var s in GroupMailsList)
                            {
                                var Checking = ddc.MailSendingWithPriorities.Where(a => a.CustomerID == c.ID && a.PriorityId == s.PriorityId).ToList();
                                if (Checking.Count == 0)
                                {
                                    MailSendingWithPriority Mails_P = new MailSendingWithPriority();
                                    Mails_P.UserID = s.UserID;
                                    Mails_P.PriorityId = s.PriorityId;
                                    Mails_P.CustomerID = c.ID;
                                    mList.Add(Mails_P);
                                }
                                else
                                {
                                    MailSendingWithPriority Mails_Update = ddc.MailSendingWithPriorities.Where(a => a.CustomerID == c.ID && a.PriorityId == s.PriorityId).FirstOrDefault();
                                    Mails_Update.UserID = s.UserID;
                                    ddc.SubmitChanges();
                                }
                            }
                        }
                        ddc.MailSendingWithPriorities.InsertAllOnSubmit(mList);
                        ddc.SubmitChanges();
                        //Lblmsg.ForeColor = System.Drawing.Color.Green;
                        Lblmsg.Text = "Successfully copied";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlPriority.SelectedIndex = 0;
            checkListUsers.ClearSelection();
            if(BtnUpdate.Visible)
            {
                BtnSave.Visible = true;
                BtnUpdate.Visible = false;
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
}