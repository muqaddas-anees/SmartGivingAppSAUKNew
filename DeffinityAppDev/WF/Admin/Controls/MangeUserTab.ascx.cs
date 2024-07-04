using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Incidents.Entity;
using Incidents.StateManager;
using Incidents.DAL;

public partial class controls_MangeUserTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sid"] != null)
        {
            if (Request.QueryString["sid"] == "7")
            {
                lbtnUsers.NavigateUrl = "~/WF/Admin/AdminUsers.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnUsers.Visible = true;
                lbtnDetails.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnDetails.Visible = false;
                lbtnAppliancesCovered.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?type=ac&uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnAppliancesCovered.Visible = false;
                lbtnRates.NavigateUrl = "~/WF/Admin/AdminUsersRates.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnRates.Visible = false;
                lbtnCertificates.NavigateUrl = "~/WF/Admin/AdminUsersCertificates.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnCertificates.Visible = false;
                lbtnAnnualLeave.NavigateUrl = "~/WF/Admin/AdminUsersAnnualLeave.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnAnnualLeave.Visible = false;
                lbtnPermissions.NavigateUrl = "~/WF/Admin/AdminUserPermissions.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnPermissions.Visible = false;
                lbtnSkills.NavigateUrl = "~/WF/Admin/AdminUsersSkills.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnSkills.Visible = false;
                //lbtnQuickLinks.NavigateUrl = "~/WF/Admin/AdminQuickLinks.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                //lbtnQuickLinks.Visible = true;
                lbtnPermissionManage.NavigateUrl = "~/WF/Admin/AdminPermissions.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnPermissionManage.Visible = false;
                //lbtnTheraphy.NavigateUrl = "../Therapy/PatientsTherapy.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                //lbtnTheraphy.Visible = false;
                lbtnMoves.NavigateUrl = "~/WF/Admin/UserMoves.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnMoves.Visible = false;
            }
            else
            {
                lbtnUsers.NavigateUrl = "~/WF/Admin/AdminUsers.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnUsers.Visible = true;
                lbtnDetails.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnDetails.Visible = true;
                lbtnAppliancesCovered.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?type=ac&uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnAppliancesCovered.Visible = false;
                lbtnRates.NavigateUrl = "~/WF/Admin/AdminUsersRates.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnRates.Visible = true;
                lbtnCertificates.NavigateUrl = "~/WF/Admin/AdminUsersCertificates.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnCertificates.Visible = true;
                lbtnAnnualLeave.NavigateUrl = "~/WF/Admin/AdminUsersAnnualLeave.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnAnnualLeave.Visible = true;
                lbtnPermissions.NavigateUrl = "~/WF/Admin/AdminUserPermissions.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnPermissions.Visible = true;
                lbtnSkills.NavigateUrl = "~/WF/Admin/AdminUsersSkills.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnSkills.Visible = true;
                //lbtnQuickLinks.NavigateUrl = "~/WF/Admin/AdminQuickLinks.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                //lbtnQuickLinks.Visible = true;
                lbtnPermissionManage.NavigateUrl = "~/WF/Admin/AdminPermissions.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnPermissionManage.Visible = true;
                lbtnMoves.NavigateUrl = "~/WF/Admin/UserMoves.aspx?uid=" + Request.QueryString["uid"] + "&sid=" + Request.QueryString["sid"];
                lbtnMoves.Visible = true;
            }
        }
        else
        {

            CheckDLT();

            lbtnUsers.NavigateUrl = "~/WF/Admin/AdminUsers.aspx?uid=" + Request.QueryString["uid"];
            lbtnDetails.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?uid=" + Request.QueryString["uid"];
            lbtnAppliancesCovered.NavigateUrl = "~/WF/Admin/AdminUserAddress.aspx?type=ac&uid=" + Request.QueryString["uid"] ;
            lbtnRates.NavigateUrl = "~/WF/Admin/AdminUsersRates.aspx?uid=" + Request.QueryString["uid"];
            lbtnCertificates.NavigateUrl = "~/WF/Admin/AdminUsersCertificates.aspx?uid=" + Request.QueryString["uid"];
            lbtnAnnualLeave.NavigateUrl = "~/WF/Admin/AdminUsersAnnualLeave.aspx?uid=" + Request.QueryString["uid"];
            lbtnPermissions.NavigateUrl = "~/WF/Admin/AdminUserPermissions.aspx?uid=" + Request.QueryString["uid"];
            lbtnSkills.NavigateUrl = "~/WF/Admin/AdminUsersSkills.aspx?uid=" + Request.QueryString["uid"];
            //lbtnQuickLinks.NavigateUrl = "~/WF/Admin/AdminQuickLinks.aspx?uid=" + Request.QueryString["uid"];
            lbtnPermissionManage.NavigateUrl = "~/WF/Admin/AdminPermissions.aspx?uid=" + Request.QueryString["uid"];
            //lbtnTheraphy.NavigateUrl = "../Therapy/PatientsTherapy.aspx?uid=" + Request.QueryString["uid"];
            lbtnMoves.NavigateUrl = "~/WF/Admin/UserMoves.aspx?uid=" + Request.QueryString["uid"];
           
        }
     
        //lbtnDetails.NavigateUrl = "~/AdminUserAddress.aspx?uid=" + Request.QueryString["uid"];
        //lbtnRates.NavigateUrl = "~/AdminUsersRates.aspx?uid=" + Request.QueryString["uid"];
        //lbtnCertificates.NavigateUrl = "~/AdminUsersCertificates.aspx?uid=" + Request.QueryString["uid"];
        //lbtnAnnualLeave.NavigateUrl = "~/AdminUsersAnnualLeave.aspx?uid=" + Request.QueryString["uid"];
        //lbtnPermissions.NavigateUrl = "~/AdminUserPermissions.aspx?uid=" + Request.QueryString["uid"];
        //lbtnSkills.NavigateUrl = "~/AdminUsersSkills.aspx?uid=" + Request.QueryString["uid"];
        if ((Request.Url.ToString().ToLower()).Contains("adminusers.aspx") == true)
        {
            lbtnUsers.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminuseraddress.aspx") == true)
        {
            lbtnDetails.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminuseraddress.aspx?type=ac") == true)
        {
            lbtnAppliancesCovered.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminusersrates.aspx") == true)
        {
            lbtnRates.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminuserscertificates.aspx") == true)
        {
            lbtnCertificates.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminusersannualleave.aspx") == true)
        {
            lbtnAnnualLeave.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("adminuserpermissions.aspx") == true)
        {
            lbtnPermissions.BackColor = System.Drawing.Color.White;
        }
        //AdminQuickLinks
        //else if ((Request.Url.ToString().ToLower()).Contains("adminquicklinks.aspx") == true)
        //{
        //    lbtnQuickLinks.BackColor = System.Drawing.Color.White;
        //}
        else if ((Request.Url.ToString().ToLower()).Contains("adminpermissions.aspx") == true)
        {
            lbtnPermissionManage.BackColor = System.Drawing.Color.White;
        }
        //else if ((Request.Url.ToString().ToLower()).Contains("patientstherapy.aspx") == true)
        //{
        //    lbtnTheraphy.BackColor = System.Drawing.Color.White;
        //}
        else if ((Request.Url.ToString().ToLower()).Contains("usermoves.aspx") == true)
        {
            lbtnMoves.BackColor = System.Drawing.Color.White;
        }
        else
        {
            lbtnSkills.BackColor = System.Drawing.Color.White;
        }

    }

    public void CheckDLT()
    {
        string[] str = PermissionManager.GetFeatures();
        //lbtnQuickLinks.Visible = false;
        lbtnUsers.Visible = true;
        lbtnSkills.Visible = true;
        lbtnPermissions.Visible = true;
        lbtnAnnualLeave.Visible = true;
        lbtnCertificates.Visible = true;
        lbtnRates.Visible = true;
        lbtnDetails.Visible = true;
        lbtnPermissionManage.Visible = true;
       // lbtnTheraphy.Visible = Convert.ToBoolean(str[144]);
    }

  
   
}
