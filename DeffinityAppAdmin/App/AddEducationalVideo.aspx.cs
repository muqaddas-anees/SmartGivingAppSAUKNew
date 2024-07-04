using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
   

   

    public partial class AddEducationalVideo : System.Web.UI.Page
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

                    if (Request.QueryString["orgid"] != null)
                    {
                        var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                        if (pEntity != null)
                        {
                            ddlReligion.SelectedValue = (pEntity.DenominationDetailsID.HasValue ? pEntity.DenominationDetailsID.Value : 0).ToString();
                            BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
                            ddlDenimination.SelectedValue = (pEntity.SubDenominationDetailsID.HasValue ? pEntity.SubDenominationDetailsID.Value : 0).ToString();
                           
                            
                        }
                    }

                    if(QueryStringValues.EID >0)
                    {
                       var eEdit= PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_SelectAll().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();

                        if(eEdit != null)
                        {
                            ddlReligion.SelectedValue = (eEdit.DenominationDetailsID.HasValue ? eEdit.DenominationDetailsID.Value : 0).ToString();
                            BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
                            ddlDenimination.SelectedValue = (eEdit.SubDenominationDetailsID.HasValue ? eEdit.SubDenominationDetailsID.Value : 0).ToString();
                            ddlCategoryID.SelectedValue = (eEdit.CategoryID.HasValue ? eEdit.CategoryID.Value : 0).ToString();
                            txtTitle.Text = eEdit.Title;
                            TextBoxVideourl.Text = eEdit.VideoUrl;
                        }

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
                var rlist = PortfolioMgt.BAL.FaithEducationCategoryBAL.FaithEducationCategoryBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).OrderBy(o => o.CategoryName).ToList();

                if (rlist.Count() == 0)
                {
                    string[] str = { "Spiritual", "Motivation", "Money", "Business", "Investments", "General Education" };

                    foreach (string s in str)
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Religion";
                ddlReligion.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAddDenimination_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Denomination";
                ddlDenimination.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnSaveChangesPop_Click
        protected void btnSaveChangesPop_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {

                if (QueryStringValues.EID == 0)
                {
                    var r = Convert.ToInt32(ddlReligion.SelectedValue);
                    var d = Convert.ToInt32(ddlDenimination.SelectedValue);
                    var c = Convert.ToInt32(ddlCategoryID.SelectedValue);

                    var v = new PortfolioMgt.Entity.FaithEducationDetail();
                    v.CategoryID = c;
                    v.DenominationDetailsID = r;
                    v.SubDenominationDetailsID = d;
                    v.Title = txtTitle.Text;
                    v.VideoUrl = TextBoxVideourl.Text;
                    v.DateLogged = DateTime.Now;
                    v.LoggedBy = sessionKeys.UID;
                    v.OrganizationID = sessionKeys.PortfolioID;
                    PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_Add(v);

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                    Response.Redirect("~/App/FaithEducationConfig.aspx", false);
                    //if (Request.QueryString["orgid"] == null)
                    //{
                    //    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                    //    if (pEntity != null)
                    //    {
                    //        pEntity.DenominationDetailsID = r;
                    //        pEntity.SubDenominationDetailsID = d;
                    //        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pEntity);
                    //    }

                    //}
                }
                else
                {
                    var v = PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_SelectAll().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();

                    if (v != null)
                    {
                        var r = Convert.ToInt32(ddlReligion.SelectedValue);
                        var d = Convert.ToInt32(ddlDenimination.SelectedValue);
                        var c = Convert.ToInt32(ddlCategoryID.SelectedValue);

                        v.CategoryID = c;
                        v.DenominationDetailsID = r;
                        v.SubDenominationDetailsID = d;
                        v.Title = txtTitle.Text;
                        v.VideoUrl = TextBoxVideourl.Text;
                        v.DateLogged = DateTime.Now;
                        v.LoggedBy = sessionKeys.UID;
                        v.OrganizationID = sessionKeys.PortfolioID;
                        PortfolioMgt.BAL.FaithEducationDetailBAL.FaithEducationDetailBAL_Update(v);

                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                        Response.Redirect("~/App/FaithEducationConfig.aspx", false);
                    }
                }
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
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }

}