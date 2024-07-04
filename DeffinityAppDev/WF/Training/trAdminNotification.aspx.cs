using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trAdminNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // lblTitle.InnerText = "Admin";
        //Master.PageHead = "Training Management";
        if (!IsPostBack)
        {
            lblException.Visible = false;
            BindMainAdministrator();
            BindGrid();
        }
    }

#region "Bind Data"
     private void BindDepartment(DropDownList ddlDepartment,int setValue)
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
       ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlDepartment.SelectedValue = setValue.ToString();

    }
    private void BindUser(DropDownList ddlUser, int setValue,int deptId)
    {
        ddlUser.DataSource = Deffinity.TrainingManager.Contractors.Users_OrderByAsc(deptId);
        ddlUser.DataValueField = "ID";
        ddlUser.DataTextField = "Name";
        ddlUser.DataBind();

        ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlUser.SelectedValue = setValue.ToString();
    }

    private void BindMainAdministrator()
    {
        ddlUserList.DataSource = Deffinity.TrainingManager.Contractors.Contractors_OrderByAsc();
        ddlUserList.DataTextField = "Name";
        ddlUserList.DataValueField = "ID";
        ddlUserList.DataBind();

    }
    private void BindGrid()
    {
        Grid_DepartmentUsers.DataSource = Notification.Notifiaction_Select(int.Parse(ddlUserList.SelectedValue));
        Grid_DepartmentUsers.DataBind();
    }
#endregion

    protected void Grid_DepartmentUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "AddNew")
        {
            try
            {
                DropDownList ddlDepartmentFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlDepartmentFooter");
                DropDownList ddlUserFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlUserFooter");
                TextBox txtEmailFooter = (TextBox)Grid_DepartmentUsers.FooterRow.FindControl("txtEmailFooter");

                NotificationEntity ne = new NotificationEntity();
                ne.ID = 0;
                ne.UserID = int.Parse(ddlUserFooter.SelectedValue);
                ne.DepartmentID = int.Parse(ddlDepartmentFooter.SelectedValue);
                ne.DepartmentName = ddlDepartmentFooter.SelectedItem.ToString();
                ne.UserName = ddlUserFooter.SelectedItem.ToString();
                ne.Email = txtEmailFooter.Text;
                ne.AdminID = int.Parse(ddlUserList.SelectedValue);
               int exist= Notification.Notification_InsertUpdate(ne);
               if (exist == 1)
               {
                   lblException.Visible = true;
                   lblException.Text = "Inserting of duplicate record not allowed";
               }
                BindGrid();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        //if (e.CommandName == "Delete")
        //{
        //    try
        //    {
        //        Notification.Notification_Delete(int.Parse(e.CommandArgument.ToString()));

        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }



        //}




    }

    protected void Grid_DepartmentUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NotificationEntity ne = (NotificationEntity)e.Row.DataItem;
            if (ne.ID == -99)
            {
                e.Row.Visible = false;
            }
            if (e.Row.FindControl("ddlDepartment") != null)
            {
                try
                {
                     DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");
                     DropDownList ddlUser = (DropDownList)e.Row.FindControl("ddlUser");
                     TextBox txtEmail = (TextBox)e.Row.FindControl("txtEmail");
                     BindDepartment(ddlDepartment,ne.DepartmentID);
                     BindUser(ddlUser, ne.UserID,ne.DepartmentID);
                     txtEmail.Text = ne.Email;


                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            try
            {
                  NotificationEntity ne = (NotificationEntity)e.Row.DataItem;
                DropDownList ddlDepartmentFooter = (DropDownList)e.Row.FindControl("ddlDepartmentFooter");
                DropDownList ddlUserFooter = (DropDownList)e.Row.FindControl("ddlUserFooter");
                TextBox txtEmailFooter = (TextBox)e.Row.FindControl("txtEmailFooter");
               
                BindDepartment(ddlDepartmentFooter, 0);

                BindUser(ddlUserFooter, 0,ne.DepartmentID);

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

    }
    protected void ddlDepartmentFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDepartmentFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlDepartmentFooter");
        DropDownList ddlUserFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlUserFooter");
        TextBox txtEmailFooter = (TextBox)Grid_DepartmentUsers.FooterRow.FindControl("txtEmailFooter");
        //BindDepartment(ddlDepartmentFooter, 0);
        //BindUser(ddlUserFooter, 0, int.Parse(ddlDepartmentFooter.SelectedValue));
        int ID = int.Parse(ddlDepartmentFooter.SelectedValue);
        ddlUserFooter.DataSource = Department.DepartmentCustomer_select(ID);
        ddlUserFooter.DataValueField = "ID";
        ddlUserFooter.DataTextField = "ContractorName";
        ddlUserFooter.DataBind();

        ddlUserFooter.Items.Insert(0, new ListItem("Please select...", "0"));
        txtEmailFooter.Text = "";
       // ddlUserFooter.SelectedValue = setValue.ToString();


    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index = Grid_DepartmentUsers.EditIndex;
            GridViewRow row = Grid_DepartmentUsers.Rows[index];
            DropDownList ddlDepartment = (DropDownList)row.FindControl("ddlDepartment");
            DropDownList ddlUser = (DropDownList)row.FindControl("ddlUser");
            TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
            int ID = int.Parse(ddlDepartment.SelectedValue);
            ddlUser.DataSource = Department.DepartmentCustomer_select(ID);
            ddlUser.DataValueField = "ID";
            ddlUser.DataTextField = "ContractorName";
            ddlUser.DataBind();

            ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
            txtEmail.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
     }

   
    protected void ddlUserFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlDepartmentFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlDepartmentFooter");
            DropDownList ddlUserFooter = (DropDownList)Grid_DepartmentUsers.FooterRow.FindControl("ddlUserFooter");
            TextBox txtEmailFooter = (TextBox)Grid_DepartmentUsers.FooterRow.FindControl("txtEmailFooter");
            //BindDepartment(ddlDepartmentFooter, 0);
            //BindUser(ddlUserFooter, 0, int.Parse(ddlDepartmentFooter.SelectedValue));
            int ID = int.Parse(ddlUserFooter.SelectedValue);
            ContratorsEntity ce = Deffinity.TrainingManager.Contractors.Contractors_GetEmailAddress(ID);//BookingsEntity be = Bookings.Bookings_GetEmailAddress(ID);
           txtEmailFooter.Text = "";
           txtEmailFooter.Text = ce.EmailAddress;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      

    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  
            int index = Grid_DepartmentUsers.EditIndex;
            GridViewRow row = Grid_DepartmentUsers.Rows[index];
            DropDownList ddlDepartment = (DropDownList)row.FindControl("ddlDepartment");
            DropDownList ddlUser = (DropDownList)row.FindControl("ddlUser");
            TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
            //BindDepartment(ddlDepartmentFooter, 0);
            //BindUser(ddlUserFooter, 0, int.Parse(ddlDepartmentFooter.SelectedValue));
            int ID = int.Parse(ddlUser.SelectedValue);
            ContratorsEntity ce = Deffinity.TrainingManager.Contractors.Contractors_GetEmailAddress(ID);
            txtEmail.Text = "";
            txtEmail.Text = ce.EmailAddress;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


    protected void Grid_DepartmentUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Grid_DepartmentUsers.EditIndex = -1;
        BindGrid();
    }
    protected void Grid_DepartmentUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            DropDownList ddlDepartment = (DropDownList)Grid_DepartmentUsers.Rows[e.RowIndex].FindControl("ddlDepartment");
            DropDownList ddlUser = (DropDownList)Grid_DepartmentUsers.Rows[e.RowIndex].FindControl("ddlUser");
            TextBox txtEmail = (TextBox)Grid_DepartmentUsers.Rows[e.RowIndex].FindControl("txtEmail");
            ImageButton btnEdit = (ImageButton)Grid_DepartmentUsers.Rows[e.RowIndex].FindControl("LinkButtonUpdate");
            NotificationEntity ne = Notification.Notifiaction_SelectByID(int.Parse(btnEdit.CommandArgument));
            ne.ID = int.Parse(btnEdit.CommandArgument);
            ne.UserID = int.Parse(ddlUser.SelectedValue);
            ne.UserName = ddlUser.SelectedItem.ToString();
            ne.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
            ne.DepartmentName = ddlDepartment.SelectedValue.ToString();
            ne.Email = txtEmail.Text;
            ne.AdminID = int.Parse(ddlUserList.SelectedValue);
            int exist=Notification.Notification_InsertUpdate(ne);
            if (exist == 1)
            {
                lblException.Visible = true;
                lblException.Text = "Duplicate records not allowed ";
                lblException.Focus();
            }
           
           
            Grid_DepartmentUsers.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void Grid_DepartmentUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Grid_DepartmentUsers.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void Grid_DepartmentUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // int index = Grid_DepartmentUsers.datab
            //GridViewRow row = Grid_DepartmentUsers.Rows[index];
            //ImageButton imgBtn=(ImageButton)row.FindControl("imgDelete");
           // imgBtn.CommandArgument
            Grid_DepartmentUsers.EditIndex = -1;
            int ID = (int)Grid_DepartmentUsers.DataKeys[e.RowIndex].Value;
            Notification.Notification_Delete(ID);

            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        
       

    }
}
