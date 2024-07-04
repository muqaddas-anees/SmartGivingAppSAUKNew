using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Activities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (sessionKeys.PortfolioID >0)
                    {
                        var id = sessionKeys.PortfolioID;
                        var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                        if (pEntity != null)
                        {

                            lblOrgActivities.Text = pEntity.PortFolio;


                        }
                    }

                    //check users and update the ower id in Project portfoliio table
                    BindCategory();
                    BindSubCategory();
                    BindGrid();

                    if (!string.IsNullOrEmpty(sessionKeys.Message))
                    {
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, string.Empty);
                        sessionKeys.Message = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindCategory()
        {
            try
            {
                var lc = PortfolioMgt.BAL.ActivityCategoryBAL.ActivityCategoryBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();



                if (lc.Count > 0)
                {
                    ddlActiviteCategory.DataSource = lc;

                    ddlActiviteCategory.DataTextField = "Name";
                    ddlActiviteCategory.DataValueField = "ID";
                    ddlActiviteCategory.DataBind();
                }
                ddlActiviteCategory.Items.Insert(0, new ListItem("Please select...", "0"));
                // ddlSubCategory.Items.Insert(0, new ListItem("Activity Sub  Category: Bible Studies for Beginners", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }




        private void BindSubCategory()
        {
            try
            {
                string data = ddlActiviteCategory.SelectedValue;

                int ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);

                var lc = PortfolioMgt.BAL.ActivitySubCategoryBAL.ActivitySubCategoryBAL_SelectAll().Where(o => o.ID == ActivityCategoryID).ToList();


                if (lc.Count > 0)
                {
                    ddlSubCategory.DataSource = lc;
                    ddlSubCategory.DataTextField = "Name";
                    ddlSubCategory.DataValueField = "ID";
                    ddlSubCategory.DataBind();
                }

                ddlSubCategory.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       




        public void BindGrid(bool getNewData = false)
        {
            try
            {


                // IPortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail>();

                var ActivityDetailList = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID);

                if (ddlActiviteCategory.SelectedValue != "0")
                {
                    ActivityDetailList = ActivityDetailList.Where(o => o.ActivityCategoryID == Convert.ToInt32(ddlActiviteCategory.SelectedValue));
                }

                if (ddlSubCategory.SelectedValue != "0")
                {
                    ActivityDetailList = ActivityDetailList.Where(o => o.ActivitySubCategoryID == Convert.ToInt32(ddlSubCategory.SelectedValue));
                }
                GridInstances.DataSource = ActivityDetailList.OrderByDescending(o => o.ID).ToList();
                GridInstances.DataBind();



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }
        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                GridViewRow row = e.Row;

                // Make sure we aren't in header/footer rows
                if (row.DataItem == null)
                {
                    return;
                }
                else
                {
                    //gv = (GridView)row.FindControl("gvInnerUsers");

                    //var dataRow = row.DataItem as dynamic;
                    //var r1 = dataRow.PortfolioID;
                    //var r = Convert.ToInt32(r1);
                    //if (r > 0)
                    //{
                    //    gv.DataSource = GetUsers().Where(o => o.CompanyID == r).OrderBy(o => o.ContractorName).ToList();
                    //    gv.DataBind();
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if (gv.UniqueID == gvUniqueID)
            //{
            //    gv.PageIndex = gvNewPageIndex;
            //    gv.EditIndex = gvEditIndex;
            //    //Check if Sorting used


            //}


        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "del")
                {
                    // var uDetails= PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByID(Convert.ToInt32(id));
                    var result = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_delete(Convert.ToInt32(id));
                    if (result)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, string.Empty);
                        BindGrid();
                    }
                    // huid.Value = userid.ToString();
                    //  mdlManageOptions.Show();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnAddOrganization
        protected void btnAddActivitie_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Activity.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        //btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlActiviteCategory_TextChanged(object sender, EventArgs e)
        {
            BindSubCategory();
            BindGrid();
        }

        protected void ddlActiviteCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCategory();
            BindGrid();
        }

        protected void ddlActiviteSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindGrid();
        }
    }
}