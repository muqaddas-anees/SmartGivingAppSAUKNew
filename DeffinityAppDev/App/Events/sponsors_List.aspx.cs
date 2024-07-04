using DeffinityAppDev.App.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Sponsor
{
   






    public partial class sponsors_List : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //firt time
                Session[users] = null;

                if (Request.QueryString["unid"] == null)
                {
                    huid.Value = Guid.NewGuid().ToString();
                }
                else
                {
                    huid.Value = Request.QueryString["unid"].ToString();
                   // displaydata();
                }

                BingGrid();
            }
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


                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();


                //var value = cRep.GetAll().Where(o => o.SponsorId >=0).FirstOrDefault();
                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> aRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var eventDetils = aRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                IPortfolioRepository<PortfolioMgt.Entity.SponsorTable> pRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();

                var Value = pRep.GetAll().Where(o => o.EventID == eventDetils.ID).ToList();


                GridInstances.DataSource = Value;
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
        protected static string GetImageUrl(object contactsId)
        {
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_spnsor;

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
                var sponsorid = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName == "del")
                {
                    // huid.Value = userid.ToString();
                    //  mdlManageOptions.Show();
                    IPortfolioRepository<PortfolioMgt.Entity.SponsorTable> pRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();

                    var Value = pRep.GetAll().Where(o => o.SponsorId == sponsorid).FirstOrDefault();

                    pRep.Delete(Value);
                    BingGrid();

                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.Deletedsuccessfully);

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
                Response.Redirect("~/App/Events/AddSponsor.aspx?unid="+QueryStringValues.UNID, false);

                ////E:\Smart Giving\DeffinityFirstDataDev\DeffinityAppAdmin\App\Sponsor\AddSponsor.aspx
                //Response.Redirect("~/App/Member.aspx", false);
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
                //Response.Redirect("~/App/Member.aspx", false);

                Response.Redirect("~/App/Events/AddSponsor.aspx?unid=" + QueryStringValues.UNID, false);
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


            contactsId = "423";

            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
    }




}