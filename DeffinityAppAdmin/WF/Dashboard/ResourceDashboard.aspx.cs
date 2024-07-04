using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Deffinity.ProgrammeManagers;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
public partial class ResourceDashboard : System.Web.UI.Page
{
    int resourceid = 0;
    string resourcename = string.Empty;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Master.PageHead = "Resources Dashboard";

            if (!IsPostBack)
            {
                BindResources();
                BindRepeater(sessionKeys.UID);

                if (RepeatInformation.Items.Count > 0)
                {
                    frm_setpage.Visible = true;
                    frm_setpage.Attributes.Add("src", GetUrl());
                    
                }
                frm_setpage.Attributes.Add("onLoad", "iFrameHeight()");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private string GetUrl()
    {
        return string.Format("ResourceDashboardByID.aspx?uid={0}&uname={1}", resourceid, resourcename);
    }
    private void BindRepeater(int loggeruserid)
    {
        
        RepeatInformation.DataSource =Deffinity.Projects.Resources.ResourceDashboardTeam_SelectByTeam(loggeruserid);        
        RepeatInformation.DataBind();
    }
    private void BindResources()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
            new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        DDLUsers.DataSource = dt;// Deffinity.Projects.Resources.GetResourceList();
        DDLUsers.DataTextField = "ContractorName";
        DDLUsers.DataValueField = "ID";
        DDLUsers.DataBind();
        DDLUsers.Items.Insert(0, new ListItem("Please select..."));
    }
    protected string GetUserImage(string UserID)
    {
       
        string img = string.Empty;
        string navUrl = string.Empty;
        string imgurl = string.Empty;
        string filepath = Server.MapPath("~/WF/UploadData/Users/ThumbNailsMedium/") + "user_" + UserID.ToString() + ".png";
        navUrl = string.Format("DisplayUser.aspx?userid={0}", UserID.ToString());
        if (System.IO.File.Exists(filepath))
        {
            imgurl = string.Format("../../WF/UploadData/Users/ThumbNailsMedium/user_{0}.png", UserID.ToString());
        }
        else
        {
            imgurl = string.Format("../../WF/UploadData/Users/ThumbNailsMedium/user_0.png", UserID.ToString());
        }
        img = string.Format("<div style='width:70px;float:left;'><img src='{0}' /></div>", imgurl, navUrl);
        return img;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(DDLUsers.SelectedValue) > 0)
            {
                Deffinity.Projects.Resources.ResourceDashboardTeam_Insert(sessionKeys.UID, int.Parse(DDLUsers.SelectedValue));
                DDLUsers.SelectedIndex = 0;
                BindRepeater(sessionKeys.UID);

                if (RepeatInformation.Items.Count > 0)
                {
                    frm_setpage.Visible = true;
                    frm_setpage.Attributes.Add("src", GetUrl());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void RepeatInformation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    object[] objList = ((System.Data.DataRowView)e.Item.DataItem).Row.ItemArray as object[];
                    resourceid = int.Parse(objList[0].ToString());
                    resourcename = objList[1].ToString();
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
        if (sessionKeys.SID != 1)
        {
            int role = 0;
            role = Admin.CheckLoginUserPermission(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }
            role = Admin.GetTeamID(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }

        }

    }
    private void Disable()
    {
        btnAdd.Enabled = false;
        frm_setpage.Disabled = true;

    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {

            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    vis = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    vis = false;

                    // Disable();

                }

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion
    protected void RepeatInformation_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteResource")
            {
                int resourceId = Convert.ToInt32(e.CommandArgument);
                int loggedUserId = sessionKeys.UID;
                if (loggedUserId > 0)
                {
                    using (projectTaskDataContext db = new projectTaskDataContext())
                    {
                        string delete = "delete from ResourceDashboardTeam where LoggedUserID=" + loggedUserId + " and ResourceID=" + resourceId;

                        SqlCommand cmd = new SqlCommand(delete, con);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        DDLUsers.SelectedIndex = 0;
                        BindRepeater(sessionKeys.UID);
                        if (RepeatInformation.Items.Count > 0)
                        {
                            frm_setpage.Attributes.Add("src", GetUrl());
                        }
                        else
                        {
                            frm_setpage.Visible = false;
                        }
                       
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
