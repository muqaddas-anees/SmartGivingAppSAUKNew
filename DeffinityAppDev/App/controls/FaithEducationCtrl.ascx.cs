using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class FaithEducationCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    BindListView();
                }

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }

                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindListView()
        {
            try
            {

                var elist = PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_SelectAll().Where(o=>o.OrganizationID == sessionKeys.PortfolioID).ToList();

                //if (Convert.ToInt32(ddlReligion.SelectedValue) > 0)
                //    elist = elist.Where(o => o.DenominationDetailsID == Convert.ToInt32(ddlReligion.SelectedValue));

                //if (Convert.ToInt32(ddlDenimination.SelectedValue) > 0)
                //    elist = elist.Where(o => o.SubDenominationDetailsID == Convert.ToInt32(ddlDenimination.SelectedValue));

                //if (Convert.ToInt32(ddlCategoryID.SelectedValue) > 0)
                //    elist = elist.Where(o => o.CategoryID == Convert.ToInt32(ddlCategoryID.SelectedValue));

                var rlist = (from r in elist.ToList()
                             select new
                             {
                                 ID = r.ID,
                                 r.VideoUrl,
                                 VideoID = GetVideoID(r.VideoUrl),
                                 VideoImage = r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1),//"background-image:url('https://img.youtube.com/vi/"+ r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1) + "/0.jpg')",
                                 VideoDisplayUrl = "background-image:url('https://img.youtube.com/vi/" + r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1) + "/0.jpg')",
                                 Title = r.Title,
                                 r.Notes,
                                 r.OrganizationID,
                                 r.LoggedBy,


                             }).ToList();


                ListFaithGiving.DataSource = rlist.OrderByDescending(o => o.ID).ToList();
                ListFaithGiving.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetVideoID(string url)
        {
            string retval = string.Empty;
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (query["v"] != null)
            {
                retval = query["v"];
            }
            else
            {
                retval = url.Substring(url.LastIndexOf("/") + 1);
            }


            return retval;
        }


        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                string v = e.CommandArgument.ToString();

               // Response.Redirect("~/App/FaithGivingDetails.aspx?eid=" + v);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}