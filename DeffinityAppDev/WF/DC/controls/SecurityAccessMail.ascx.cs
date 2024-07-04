using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.BLL;
using DC.DAL;
using PortfolioMgt.DAL;

public partial class DC_controls_SecurityAccessMail : System.Web.UI.UserControl
{
    public int RequestTypeID
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                h_rtid.Value = RequestTypeID.ToString();
                BindUsers();
                BindGrid();
            }
            //CopyToAllCustomer show only for fls or service desk requesters
            //if (Request.QueryString["tab"] != null)
            //{
            //    if (Request.QueryString["tab"].ToString().ToLower() == "fls")
            //        btnCopyToAllCustomers.Visible = true;
            //    else
            //        sessionKeys.PortfolioID = 0;
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindUsers()
    {
        try
        {
            ddlUsers.DataSource = SecurityAccessMail.BindContractor().Where(p=>p.SID ==1 || p.SID==2 || p.SID==3).ToList();
            ddlUsers.DataValueField = "ID";
            ddlUsers.DataTextField = "ContractorName";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Please select...","0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGrid()
    {
        try
        {
            using (DCDataContext dd = new DCDataContext())
            {
                GvMailManager.DataSource = SecurityAccessMail.BindManager(int.Parse(h_rtid.Value), sessionKeys.PortfolioID);
                GvMailManager.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
           
    }
    protected void btnaddUser_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;

            Manager manager = new Manager();
            manager.UserID = int.Parse(ddlUsers.SelectedValue);
            manager.RequestTypeID = int.Parse(h_rtid.Value);
            manager.CustomerID = customerID;
            bool exists = SecurityAccessMail.CheckExists(int.Parse(ddlUsers.SelectedValue), int.Parse(h_rtid.Value),customerID);
            if (!exists)
            {
                SecurityAccessMail.Add(manager);
                lblMsg.Text = "User added successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblErrorMsg.Text = "User already exists.";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btndeleteUser_Click(object sender, EventArgs e)
    {

    }
    protected void GvMailManager_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete1")
            {
                SecurityAccessMail.DeleteById(int.Parse(e.CommandArgument.ToString()));
                BindGrid();
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Deleted successfully";
            }
            if (e.CommandName == "DeleteAll")
            {
                SecurityAccessMail.DeteleAllUsers(int.Parse(e.CommandArgument.ToString()));
                BindGrid();
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Deleted successfully";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }
    protected void btnCopyToAllCustomers_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext db = new DCDataContext())
            {
               
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var managerList = db.Managers.Where(m => m.CustomerID == sessionKeys.PortfolioID && m.RequestTypeID == Convert.ToInt32(h_rtid.Value)).ToList();
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                   // var subjectList = dc.Subjects.Where(c => c.CustomerID == customerID).ToList();
                    if (managerList.Count() > 0)
                    {
                        List<Manager> mList = new List<Manager>();
                        foreach (var c in customerList)
                        {
                            foreach (var s in managerList)
                            {
                                bool exists = SecurityAccessMail.CheckExists(Convert.ToInt32(s.UserID), int.Parse(h_rtid.Value), c.ID);
                                if (!exists)
                                {
                                    Manager manager = new Manager();
                                    manager.UserID = s.UserID;
                                    manager.CustomerID = c.ID;
                                    manager.RequestTypeID = s.RequestTypeID;
                                    mList.Add(manager);
                                }
                            }
                        }
                        //Bulk insert
                        db.Managers.InsertAllOnSubmit(mList);
                        db.SubmitChanges();
                        lblEmailDisList.Text = "Successfully copied";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
}