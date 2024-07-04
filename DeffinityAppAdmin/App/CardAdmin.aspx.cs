using DeffinityManager.PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class CardAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                        sessionKeys.Message = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }
        void BindGrid()
        {
            var uList = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.PartnerID == 2).ToList();
            grid_display.DataSource = uList.OrderByDescending(o => o.ID).ToList();
            grid_display.DataBind();
        }
        public void AddUser(string DisplayName, string Username, string Password, int PartnerID)
        {
            //ListItem li = new ListItem();
            try
            {
                var r = new PortfolioMgt.Entity.PortfolioTrackerLogin();
                r.DisplayName = DisplayName;
                r.Username = Username;
                r.Password = Password;
                r.BaseURL = "site.faithunionhub.com";
                r.IsActive = true;
                r.PartnerID = PartnerID;

                if (PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.Username.ToLower() == Username.ToLower() && o.PartnerID == PartnerID).Count() == 0)
                {
                    var p = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_AddNew(r);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            // return li;
        }

        protected void btnSubmit_onclick(object sender, EventArgs e)
        {

            try
            {
                if (txtUsername1.Text.Length > 0)
                {
                    AddUser(txtUsername1.Text.Trim(), txtEmail1.Text.Trim(), txtPassword1.Text.Trim(), 2);
                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                    Response.Redirect(Request.RawUrl, false);
                }
                //lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                //string script = "window.onload = function() { toastr.success('etetetetet', 'testet'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected void grid_display_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(id);
                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                        //BindGrid();
                        Response.Redirect(Request.RawUrl, false);
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