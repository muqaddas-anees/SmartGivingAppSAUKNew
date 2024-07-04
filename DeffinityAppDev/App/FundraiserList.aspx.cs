using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class FaithGivingListManage : System.Web.UI.Page
    {

        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //firt time
                Session[users] = null;
                //check users and update the ower id in Project portfoliio table
                BindCountry();
                BingGrid();


                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                    sessionKeys.Message = string.Empty;
                }
            }
        }

        private void BindCountry()
        {

            //LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            //var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            //if (lc.Count > 0)
            //{
            //    ddlCountry.DataSource = lc;
            //    ddlCountry.DataTextField = "Country1";
            //    ddlCountry.DataValueField = "ID";
            //    ddlCountry.DataBind();
            //}
            //ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
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

                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.DonerEmail != null).Where(o=>o.PaidDate.HasValue).ToList();

               // tList = tList.Where(o => o.FundriserUNID.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();

                IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                var listTithingDefaults = pRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                if (QueryStringValues.EVENTUNID.Length > 0)
                    listTithingDefaults = listTithingDefaults.Where(o => o.Event_unid == QueryStringValues.EVENTUNID).ToList();


                var dlist = (from p in listTithingDefaults
                             select new
                             {
                                 p.CreatedDateTime,
                                 p.Currency,
                                 p.DefaultBanner,
                                 p.DefaultTarget,
                                 p.DefaultValues,
                                 p.Description,
                                 p.EndDate,
                                 p.Event_unid,
                                 p.ID,
                                 p.IsEnable,
                                 p.IsFundraiser,
                                 p.LoggedByID,
                                 p.ModifiedDateTime,
                                 p.OrganizationID,
                                 p.SendMailAfterDonation,
                                 p.ShowChart,
                                 p.ShowQRCode,
                                 p.StartDate,
                                 p.Title,
                                 p.unid,
                                 RaisedAmount = p.unid == null ? 0.00 : tList.Where(o => o.FundriserUNID == p.unid).Select(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00).Sum()
                             }).ToList();
                //RaisedAmount

                GridInstances.DataSource = dlist;
                GridInstances.DataBind();


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
                var uid = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "del")
                {

                    var pData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Delete(uid);
                    BingGrid();
                }

                //if (e.CommandName == "password")
                //{
                //    // huid.Value = userid.ToString();
                //    //  mdlManageOptions.Show();

                //}
                //else if (e.CommandName == "status")
                //{
                //    var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(userid);

                //    if (u.Status == UserMgt.BAL.ContractorStatus.Active)
                //        u.Status = UserMgt.BAL.ContractorStatus.InActive;
                //    else
                //        u.Status = UserMgt.BAL.ContractorStatus.Active;
                //    UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(userid, u.Status);
                //    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                //    BingGrid(true);
                //}
                //else if (e.CommandName == "Instance")
                //{
                //    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                //    if (c.Visible.HasValue ? c.Visible.Value : false == false)
                //    {
                //        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, false);
                //    }
                //    else
                //    {
                //        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, true);
                //    }

                //    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                //    BingGrid();
                //}
                //else if (e.CommandName == "Modules")
                //{
                //    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                //    if (c.AllowModules.HasValue ? c.AllowModules.Value : false == false)
                //    {
                //        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, false);
                //    }
                //    else
                //    {
                //        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, true);
                //    }

                //    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                //    BingGrid();
                //}
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
                if (QueryStringValues.UNID.Length > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var pdetails = pRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                    if (pdetails != null)
                        Response.Redirect("~/App/AddFundraiser.aspx?EventUnid=" + pdetails.Event_unid, false);
                }
                else
                {
                    Response.Redirect("~/App/AddFundraiser.aspx?EventUnid=" + QueryStringValues.EVENTUNID, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        //btnClose_Click


        //    protected static string GetImageUrl(string contactsId)
        //    {
        //    //contactsId = sessionKeys.UID.ToString();

        //    //bool isOriginal = false;

        //    string img = string.Empty;

        //    string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_" + contactsId.ToString() + ".png";

        //    if (System.IO.File.Exists(filepath))
        //    {

        //            img = string.Format("~/WF/UploadData/Tithing/Tithing_{0}.png", contactsId.ToString());
        //        // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
        //        //img = string.Format("<img src='{0}' />", imgurl);
        //    }
        //    else
        //    {
        //        img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
        //        //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
        //    }
        //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        //    // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        //}


        protected static string GetImageUrl(string contactsId)
        {
            //contactsId = sessionKeys.UID.ToString();

            //bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {

                img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

    }
}