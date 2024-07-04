using DeffinityManager.PortfolioMgt.BAL;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DeffinityAppDev.App
{
    public partial class MembersPage : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if(sessionKeys.Message.Length >0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.Deletedsuccessfully,"Ok");
                    sessionKeys.Message = "";
                }
                //firt time
                Session[users] = null;
                //check users and update the ower id in Project portfoliio table
                BindCountry();
                BingGrid();
            }
        }

        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        public List<UserMgt.Entity.v_contractor> GetUsers(bool getNewdata = false)
        {
            //if (getNewdata)
            //    Session[users] = null;
            //if (Session[users] == null)
            //{
            //    Session[users] = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
            //}

            //return (Session[users] as List<UserMgt.Entity.v_contractor>).ToList();
            return UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
        }
        public void BingGrid(bool getNewData = false)
        {
            try
            {


                var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();// //PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();
                //var tList = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
                //var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().ToList();

                //var dlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().ToList();
                //var pb = new PortfolioMgt.BAL.PortfolioContactBAL();
                //var pdlist = pb.PortfolioContact_SelectAll().ToList();
                //var uList = GetUsers(getNewData);

                var rData = (from p in iList
                                 //join u in uList on p.ID equals u.CompanyID
                             select new
                             {
                                 ID = p.ID,
                                 InstanceName = p.ContractorName,
                                 Name = p.ContractorName,
                                 Address = p.Address1,
                                 p.EmailAddress,
                                // p.OrgarnizationStatus,
                                 //p.OrgarnizationGUID,
                                 //p.OrgarnizationApproval,
                                 p.Town,
                                 p.State,
                                 CountryID = p.Country,
                                 p.DenominationDetailsID,
                                 Religion = p.DenominationDetailsName,
                                 Group = p.DenominationGroupDetailsName,
                                 // p.DenominationDetailsID.HasValue ? (rlist.Where(o => o.ID == p.DenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Denomination = p.SubDenominationDetailsName,// p.SubDenominationDetailsID.HasValue ? (dlist.Where(o => o.ID == p.SubDenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Contact = p.ContractorName,// pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Name : string.Empty,
                                                            // ContactEmail = pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Email : string.Empty,
                                 ContactNumber = p.ContactNumber, //pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Mobile : string.Empty,
                                 LogoPath = "",
                                 Status = p.Status,


                             }).ToList();

                // var ulist = GetUsers();

                //if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                //{
                //    rData = rData.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.EmailAddress != null ? p.EmailAddress.Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.Administrator != null ? p.Administrator.Contains(txtSearch.Text.ToLower()) : false)).ToList();
                //}


                if (QueryStringValues.Type == "all")
                {

                    var resultlist = rData.Where(o => o.InstanceName != "").OrderBy(o => o.InstanceName).ToList();

                    //lblNumberofInstances.Text = resultlist.Count().ToString();
                    // lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }
                else
                {
                    var resultlist = rData.Where(o => o.InstanceName != "").Where(o => !o.EmailAddress.ToLower().Contains("indra")).OrderBy(o => o.InstanceName).ToList();

                    if (Convert.ToInt32(ddlCountry.SelectedValue) > 0)
                        resultlist = resultlist.Where(o => o.CountryID == Convert.ToInt32(ddlCountry.SelectedValue)).ToList();

                    if (txtsearch.Value.Length > 0)
                    {
                        resultlist = resultlist.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                || (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    }

                    //    if (txtst.Value.Length > 0)
                    //    {
                    //        resultlist = resultlist.Where(p => (p.State != null ? p.State.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                    //|| (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    //    }
                    // lblNumberofInstances.Text = resultlist.Count().ToString();
                    //lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BingGrid();

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
            BingGrid();
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var userid = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "password")
                {
                    // huid.Value = userid.ToString();
                    //  mdlManageOptions.Show();

                }
                else if (e.CommandName == "status")
                {
                    var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(userid);

                    if (u.Status == UserMgt.BAL.ContractorStatus.Active)
                        u.Status = UserMgt.BAL.ContractorStatus.InActive;
                    else
                        u.Status = UserMgt.BAL.ContractorStatus.Active;
                    UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(userid, u.Status);
                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid(true);
                }
                else if (e.CommandName == "Instance")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.Visible.HasValue ? c.Visible.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if (e.CommandName == "Modules")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.AllowModules.HasValue ? c.AllowModules.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if(e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IUserRepository<UserMgt.Entity.Contractor> cRep = new UserRepository<UserMgt.Entity.Contractor>();
                        var c = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        if(c != null)
                        {
                            cRep.Delete(c);
                            sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                            Response.Redirect(Request.RawUrl, false);
                        }
                      
                        //BindGrid();
                      
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnAddOrganization
        protected void btnAddOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Member.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Member.aspx", false);
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

        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
            //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_user; //"img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
          
                try
                {

                    string filename = Server.MapPath("~/WF/UploadData/Templates/membertemplate.xlsx");
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                    if (fileInfo.Exists)
                    {
                        Response.Clear();

                        HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                        HttpContext.Current.Response.ContentType = "application/ms-excel";
                        HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=Member_Template.xlsx");
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            
        }
    }
}