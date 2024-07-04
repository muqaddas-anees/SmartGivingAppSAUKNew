using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Deffinity.BLL;
using System.Linq;
using Deffinity.BE;
using System.Web.Security;

public partial class RFIVendors1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }

            BindGrid();



        }
        //Master.PageHead = "Tenders";
        iframeMpp.Attributes.Add("src", string.Format("RFIVendorServiceCatalogFileupload.aspx?VendorID={0}", QueryStringValues.Vendor));

        if (Request.QueryString["back"] != null)
        {
            if (Request.QueryString["pnl"] != null)
                linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
            else
                linkBack.NavigateUrl = Request.QueryString["back"];
            linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
            linkBack.Visible = true;
        }
    }


    private bool CheckVendorExists()
    {
        bool retval = false;
        var uname = sessionKeys.PortfolioName;
        var vList = UserMgt.BAL.ContractorsBAL.Contractor_SelectVendros();

        if (vList.Count > 0)
        {
            var rEntity = vList.Where(o => o.ContractorName.ToLower() == uname.ToLower() && o.LoginName.ToLower() == uname.ToLower() && o.CompanyID == sessionKeys.PortfolioID && o.SID == 8).FirstOrDefault();
            if (rEntity != null)
                retval = true;
            else
                retval = false;
        }
        return retval;
    }
    private void AddUsertoCompany(int VendorID)
    {
        try
        {
            if (sessionKeys.PortfolioID > 0)
            {
                IRFIRepository<RFI.Entity.RFI_Vendor> rRep = new RFIRepository<RFI.Entity.RFI_Vendor>();

                var rData = rRep.GetAll().Where(o => o.VendorID == VendorID).FirstOrDefault();
                if (rData != null)
                {

                    IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    var uEntity = uRep.GetAll().Where(o => o.UserID == rData.ContractorID && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (uEntity == null)
                    {
                        uEntity = new UserMgt.Entity.UserToCompany();
                        uEntity.CompanyID = sessionKeys.PortfolioID;
                        uEntity.UserID = rData.ContractorID;
                        uRep.Add(uEntity);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  
    private void AddDefaultSupplier()
    {
        try
        {

            int vendorID = 0;

            RFI_Vendor _rfiVendor = new RFI_Vendor();
            var uname = sessionKeys.PortfolioName;
            _rfiVendor.CONTRACTORNAME = uname;
            _rfiVendor.LOGINAME =uname;
            _rfiVendor.PASSWORD = FormsAuthentication.HashPasswordForStoringInConfigFile(uname, "SHA1");
            _rfiVendor.ADDRESS =string.Empty;
            _rfiVendor.COMPANY = "";//txtCompany.Text.Trim();
            _rfiVendor.EMAILADDRESS =string.Empty;
            _rfiVendor.HQADDRESS = "";// txtHQAddress.Text.Trim();
            _rfiVendor.POSTCODE = string.Empty;
            _rfiVendor.REGNO = string.Empty;
            _rfiVendor.VATNO = string.Empty;
            _rfiVendor.SKILLS = string.Empty;

            if (vendorID == 0)
            {
                vendorID = RFI_Vendor_SVC.Insert(_rfiVendor);
                AddUsertoCompany(vendorID);
               // AddTendorScore(vendorID);
            

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindGrid()
    {
        try
        {
            RFIRepository<RFI.Entity.VendorDetails> RRep = new RFIRepository<RFI.Entity.VendorDetails>();
            GridView1.DataSource = RRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                if (!CheckVendorExists())
                {
                    AddDefaultSupplier();

                    GridView1.DataSource = RRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                    GridView1.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void lbtnPlan_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIVendorOverview.aspx", true);
    }

    //This event occurs on click of the Delete button
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Get the value        
        //string strSubSectionID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblSubsectionID")).Text;
        int _av2pID =Convert.ToInt32( ((LinkButton)GridView1.Rows[e.RowIndex].FindControl("btnDelete")).CommandArgument.ToString());//Convert.ToInt32(e.Keys["VendorID"]);
          

        //

        //Prepare the delete Command of the DataSource control
        try
        {
            
            RFI_Vendor_SVC.DeleteVendorByvendorID(_av2pID);
            BindGrid();
        }
        catch ( Exception ex)
        {
            string strEx = ex.ToString();
        }
    }
    
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        //Check if there is any exception while deleting
        if (e.Exception != null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }
}
