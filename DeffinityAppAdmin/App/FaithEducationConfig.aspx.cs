
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class FaithEducationConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindReligion();
                    BindDenomination(0);
                    BindCategory();
                    BindListView();
                }
                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                    sessionKeys.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindReligion()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

                ddlReligion.DataSource = rlist;
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataValueField = "ID";
                ddlReligion.DataBind();

                ddlReligion.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int religionID)
        {
            try
            {
                if (religionID > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    ddlDenimination.DataSource = rlist;
                    ddlDenimination.DataTextField = "Name";
                    ddlDenimination.DataValueField = "ID";
                    ddlDenimination.DataBind();
                }

                ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlDenimination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindListView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindListView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        //
        private void BindCategory()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.FaithEducationCategoryBAL.FaithEducationCategoryBAL_SelectAll().Where(o=>o.OrganizationID == sessionKeys.PortfolioID).OrderBy(o => o.CategoryName).ToList();

                if(rlist.Count() == 0 )
                {
                    string[] str = { "Spiritual", "Motivation", "Money", "Business", "Investments", "General Education", "Mindset","Health and Fitness", "Self Improvement" };

                    foreach(string s in str)
                    {
                        PortfolioMgt.BAL.FaithEducationCategoryBAL.FaithEducationCategoryBAL_Add(new PortfolioMgt.Entity.FaithEducationCategory() { OrganizationID = sessionKeys.PortfolioID, CategoryName = s });
                    }
                    rlist = PortfolioMgt.BAL.FaithEducationCategoryBAL.FaithEducationCategoryBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).OrderBy(o => o.CategoryName).ToList();
                }

                

                ddlCategoryID.DataSource = rlist;
                ddlCategoryID.DataTextField = "CategoryName";
                ddlCategoryID.DataValueField = "ID";
                ddlCategoryID.DataBind();

                ddlCategoryID.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
                BindListView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindListView()
        {
            try {

                var elist = PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_SelectAll();

                if (Convert.ToInt32(ddlReligion.SelectedValue) > 0)
                    elist = elist.Where(o => o.DenominationDetailsID == Convert.ToInt32(ddlReligion.SelectedValue));

                if (Convert.ToInt32(ddlDenimination.SelectedValue) > 0)
                    elist = elist.Where(o => o.SubDenominationDetailsID == Convert.ToInt32(ddlDenimination.SelectedValue));

                if (Convert.ToInt32(ddlCategoryID.SelectedValue) > 0)
                    elist = elist.Where(o => o.CategoryID == Convert.ToInt32(ddlCategoryID.SelectedValue));

                var rlist = (from r in elist.ToList()
                            select new
                             {
                                 ID =r.ID,
                                 r.VideoUrl,
                                 VideoID = GetVideoID(r.VideoUrl),
                                 VideoImage = r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1),//"background-image:url('https://img.youtube.com/vi/"+ r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1) + "/0.jpg')",
                                 VideoDisplayUrl = "background-image:url('https://img.youtube.com/vi/" + r.VideoUrl.Substring(r.VideoUrl.LastIndexOf("/") + 1) + "/0.jpg')",
                                 Title= r.Title,
                                 r.Notes,
                                 r.OrganizationID,
                                 r.LoggedBy,


                             }).ToList();


                ListFaithGiving.DataSource = rlist.OrderByDescending(o=>o.ID).ToList();
                ListFaithGiving.DataBind();

            }
            catch(Exception ex)
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
        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/AddEducationalVideo.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {

                    string v = e.CommandArgument.ToString();

                    PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_delete(Convert.ToInt32(v));

                    BindListView();
                }
                else if(e.CommandName == "edit1")
                {
                    Response.Redirect("~/App/AddEducationalVideo.aspx?eid=" + e.CommandArgument.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    }
}