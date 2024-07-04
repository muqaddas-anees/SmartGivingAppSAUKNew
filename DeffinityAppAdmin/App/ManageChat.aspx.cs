using DeffinityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class ManageChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    BindOrgs();
                    BindMember();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindOrgs()
        {
            var orgList = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o=>o.PortFolio != "").ToList();

            if(orgList.Count() >0)
            {
                ddlOrg.DataSource = orgList.OrderBy(o => o.PortFolio).ToList();
                ddlOrg.DataTextField = "PortFolio";
                ddlOrg.DataValueField = "ID";
                ddlOrg.DataBind();
                ddlOrg.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }

        private void BindMember()
        {
            var orgList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();

            if (orgList.Count() > 0)
            {
                ddlMember.DataSource = orgList.OrderBy(o => o.ContractorName).ToList();
                ddlMember.DataTextField = "ContractorName";
                ddlMember.DataValueField = "ID";
                ddlMember.DataBind();
                ddlMember.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string retval = string.Empty;
                var orgDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(ddlOrg.SelectedValue)).FirstOrDefault();
                if(orgDetails != null)
                {
                    retval = retval + "<br><b> Orgname: </b>" + orgDetails.PortFolio;
                    retval = retval + "<br><b> Chat_ChannelID: </b>" + orgDetails.Chat_ChannelID;
                    retval = retval + "<br><b> Chat_ChannelName: </b>" + orgDetails.Chat_ChannelName;
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    lblorgdetails.Text = retval;
                }


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnCreateChannel_Click(object sender, EventArgs e)
        {
            try
            {
                StreamChatBAL sb = new StreamChatBAL();
                sb.CreateUser(Convert.ToInt32(ddlMember.SelectedValue), Convert.ToInt32(ddlOrg.SelectedValue));
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddModarator_Click(object sender, EventArgs e)
        {
            try
            {
                StreamChatBAL sb = new StreamChatBAL();
                sb.CreateUser(Convert.ToInt32(ddlMember.SelectedValue), Convert.ToInt32(ddlOrg.SelectedValue));
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            try
            {
                StreamChatBAL sb = new StreamChatBAL();
                sb.CreateUser(Convert.ToInt32(ddlMember.SelectedValue), Convert.ToInt32(ddlOrg.SelectedValue));
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string retval = string.Empty;
                var mDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == Convert.ToInt32(ddlMember.SelectedValue)).FirstOrDefault();
                if (mDetails != null)
                {
                    retval = retval + "<br><b> Name: </b>" + mDetails.ContractorName;
                    retval = retval + "<br><b> Email: </b>" + mDetails.EmailAddress;
                    retval = retval + "<br><b> Chat_ID: </b>" + mDetails.Chat_ID;
                    retval = retval + "<br><b> Chat_Key: </b>" + mDetails.Chat_Key;
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    //retval = retval + "" + "";
                    lblMembersDetails.Text = retval;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}