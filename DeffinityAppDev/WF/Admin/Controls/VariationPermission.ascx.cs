using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class controls_VariationPermission : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindDropDowns();
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindDropDowns()
    {
        using (UserDataContext db = new UserDataContext())
        {
            var userList = db.Contractors.Where(c => c.Status.ToLower() == "active" && c.SID != 8 && c.SID != 7).Select(c => c).OrderBy(c => c.ContractorName).ToList();
            ddlManager.DataSource = userList;
            ddlManager.DataValueField = "ID";
            ddlManager.DataTextField = "ContractorName";
            ddlManager.DataBind();
            ddlManager.Items.Insert(0,new ListItem("Please select...","0"));
            //user dropdown 
            ddlUser.DataSource = userList;
            ddlUser.DataValueField = "ID";
            ddlUser.DataTextField = "ContractorName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
        }
    }
    private int AssignedManagerToUser(string procedureName,int managerId,int userId)
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand(procedureName);
        db.AddInParameter(cmd, "@ManagerID", DbType.Int32, managerId);
        db.AddInParameter(cmd, "@UserID", DbType.String,userId);
        db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
        db.ExecuteNonQuery(cmd);
        int getVal = (int)db.GetParameterValue(cmd, "@output");
        cmd.Dispose();
        return getVal;
    }

    private void BindGrid()
    {

        int managerId = int.Parse(ddlManager.SelectedValue);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBString"].ToString());
        conn.Open();
        string cmdText = "SELECT v.ID,c.ContractorName as ManagerName,cn.ContractorName as UserName from VariationPermission v join Contractors c on v.ManagerID = c.ID join Contractors cn on v.UserID = cn.ID";
        if (managerId > 0)
        {
            cmdText = cmdText + " WHERE ManagerID=" + managerId;
        }
        cmdText = cmdText + " ORDER BY c.ContractorName,cn.ContractorName";
        
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        DataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        cmd.Dispose();
        conn.Close();
        gvAssignedUser.DataSource = ds;
        gvAssignedUser.DataBind();
       
     
    }
    protected void imgAssignUser_Click(object sender, EventArgs e)
    {
        try
        {
            int i = AssignedManagerToUser("DEFFINITY_VariationPermission_Insert", int.Parse(ddlManager.SelectedValue), int.Parse(ddlUser.SelectedValue));
            if (i == 0)
            {
                lblmsg.Text = "User assigned to manager successfully.";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            if (i == 1)
            {
                lblmsg.Text = "This user already assigned to manager. Please check and try again.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvAssignedUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Deleterow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBString"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM VariationPermission WHERE ID=" + id, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                BindGrid();
                lblmsg.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvAssignedUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssignedUser.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}