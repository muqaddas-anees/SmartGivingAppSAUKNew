using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Deffinity.BE;
using Deffinity.BLL;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;

public partial class Vendor_overview : System.Web.UI.Page
{
    DisBindings getdata = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {

        //Master.PageHead = "Vendor Account";
        
        if (!Page.IsPostBack)
        {
            int _CURRENTYEAR = DateTime.Now.Year;
            lblLastyr1.InnerText = (_CURRENTYEAR - 1).ToString();
            lblLastyr2.InnerText = (_CURRENTYEAR - 2).ToString();
            lblLastyr3.InnerText = (_CURRENTYEAR - 3).ToString();
            if (QueryStringValues.Vendor != 0)
            {
                //Reqtxtpwd.Enabled = false;
                fillcontrols(QueryStringValues.Vendor);
                fillBBBEEDetails(QueryStringValues.Vendor);
            }
            else
            {
                BindDDL();
            }

            //else
            //{
            //    object obj;               
            //    obj = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "SELECT VendorID FROM v_vendors WHERE CONTRACTORID="+ sessionKeys.UID);
            //    QueryStringValues.Vendor = Convert.ToInt32(obj);
            //    fillcontrols(QueryStringValues.Vendor);
            //}
        }
    }

    private void fillcontrols(int vendorID)
    {
        RFI_Vendor _rfiVendor=new RFI_Vendor();
        _rfiVendor = RFI_Vendor_SVC.Select(vendorID);
        txtAddress.Text = _rfiVendor.ADDRESS;
        //txtCompany.Text = _rfiVendor.COMPANY;
        txtEmailAddress.Text = _rfiVendor.EMAILADDRESS;
        //txtHQAddress.Text = _rfiVendor.HQADDRESS;
        txtLoginName.Text = _rfiVendor.LOGINAME;
        //txtPassword.Enabled = false;
        
        txtPostCode.Text = _rfiVendor.POSTCODE;
        txtRegNo.Text = _rfiVendor.REGNO;
        txtSkills.Text = _rfiVendor.SKILLS;
        txtUserName.Text = _rfiVendor.CONTRACTORNAME;
        txtVATNumber.Text = _rfiVendor.VATNO;
        DataTable _dt = RFI_TradingHistory_SVC.Fill(vendorID);
        txtLastyr1.Text = _dt.Rows[0][2].ToString();
        txtLastyr2.Text = _dt.Rows[1][2].ToString();
        txtLastyr3.Text = _dt.Rows[2][2].ToString();
        txtNetProfit1.Text = _dt.Rows[0][3].ToString();
        txtNetProfit2.Text = _dt.Rows[1][3].ToString();
        txtNetProfit3.Text = _dt.Rows[2][3].ToString();
        txtStaffRatio1.Text = _dt.Rows[0][4].ToString().Trim();
        txtStaffRatio2.Text = _dt.Rows[1][4].ToString().Trim();
        txtStaffRatio3.Text = _dt.Rows[2][4].ToString().Trim();
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        int vendorID = QueryStringValues.Vendor;
        try
        {
            if (CheckVendorExists(txtUserName.Text.Trim(),txtLoginName.Text.Trim()) && vendorID== 0)
            {
                lblmsg.Text = "Supplier already exists with the given details";
            }
            else
            {
                RFI_Vendor _rfiVendor = new RFI_Vendor();
                _rfiVendor.CONTRACTORNAME = txtUserName.Text.Trim();
                _rfiVendor.LOGINAME = txtUserName.Text.Trim();
                _rfiVendor.PASSWORD = FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserName.Text.Trim(), "SHA1");
                _rfiVendor.ADDRESS = txtAddress.Text.Trim();
                _rfiVendor.COMPANY = "";//txtCompany.Text.Trim();
                _rfiVendor.EMAILADDRESS = txtEmailAddress.Text.Trim();
                _rfiVendor.HQADDRESS = "";// txtHQAddress.Text.Trim();
                _rfiVendor.POSTCODE = txtPostCode.Text.Trim();
                _rfiVendor.REGNO = txtRegNo.Text.Trim();
                _rfiVendor.VATNO = txtVATNumber.Text.Trim();
                _rfiVendor.SKILLS = txtSkills.Text.Trim();
                
                if (vendorID == 0)
                {
                    vendorID = RFI_Vendor_SVC.Insert(_rfiVendor);
                    AddUsertoCompany(vendorID);
                    AddTendorScore(vendorID);
                    if (vendorID > 0)
                    {
                        InsertUpdateTradingHistory(vendorID,false);
                        Response.Redirect("~/WF/Vendors/RFIVendorOverview.aspx?VendorID=" + vendorID);
                    }
                    else
                    {
                        lblmsg.Text = "Error occured while adding Vendor";
                    }
                }
                else
                {
                    _rfiVendor.VENDORID = vendorID;
                    int i = RFI_Vendor_SVC.Update(_rfiVendor);
                    if (i == -1)
                    {
                        InsertUpdateTradingHistory(vendorID, true);
                        Response.Redirect("~/WF/Vendors/RFIVendorOverview.aspx?VendorID=" + vendorID);
                    }
                    else
                    {
                        lblmsg.Text = "Error occured while updating vendor";
                    }
                }      
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //Response.Redirect("RFIVendorSites.aspx?Vendor=" + QueryStringValues.Vendor.ToString(), true);
    }

    private bool CheckVendorExists(string username , string loginname)
    {
        bool retval = false;
        var vList = UserMgt.BAL.ContractorsBAL.Contractor_SelectVendros();

        if(vList .Count >0)
        {
          var rEntity = vList.Where(o => o.ContractorName.ToLower() == username.ToLower() && o.LoginName.ToLower() == loginname.ToLower() && o.CompanyID == sessionKeys.PortfolioID && o.SID == 8).FirstOrDefault();
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
    private void InsertUpdateTradingHistory(int vendorID,bool isUpdate)
    {
        RFI_TradingHistory _rfiTrading1= new RFI_TradingHistory();
        RFI_TradingHistory _rfiTrading2 = new RFI_TradingHistory();
        RFI_TradingHistory _rfiTrading3 = new RFI_TradingHistory();

        _rfiTrading1.NETPROFIT = Convert.ToDouble(txtNetProfit1.Text.Trim());
        _rfiTrading1.VENDORID = vendorID;
        _rfiTrading1.TURNOVER = Convert.ToDouble(txtLastyr1.Text.Trim());
        _rfiTrading1.PERBYCONSTAFFRATION = txtStaffRatio1.Text.Trim();
        _rfiTrading1.YEAR = Convert.ToInt32(lblLastyr1.InnerText);


        _rfiTrading2.NETPROFIT = Convert.ToDouble(txtNetProfit2.Text.Trim());
        _rfiTrading2.VENDORID = vendorID;
        _rfiTrading2.TURNOVER = Convert.ToDouble(txtLastyr2.Text.Trim());
        _rfiTrading2.PERBYCONSTAFFRATION = txtStaffRatio2.Text.Trim();
        _rfiTrading2.YEAR = Convert.ToInt32(lblLastyr2.InnerText);


        _rfiTrading3.NETPROFIT = Convert.ToDouble(txtNetProfit3.Text.Trim());
        _rfiTrading3.VENDORID = vendorID;
        _rfiTrading3.TURNOVER = Convert.ToDouble(txtLastyr3.Text.Trim());
        _rfiTrading3.PERBYCONSTAFFRATION = txtStaffRatio3.Text.Trim();
        _rfiTrading3.YEAR = Convert.ToInt32(lblLastyr3.InnerText);
        if (isUpdate)
        {
            RFI_TradingHistory_SVC.Update(_rfiTrading1);
            RFI_TradingHistory_SVC.Update(_rfiTrading2);
            RFI_TradingHistory_SVC.Update(_rfiTrading3);
        }
        else
        {

            RFI_TradingHistory_SVC.Insert(_rfiTrading1);
            RFI_TradingHistory_SVC.Insert(_rfiTrading2);
            RFI_TradingHistory_SVC.Insert(_rfiTrading3);
        }
        
    }

    private void BindDDL()
    {
        DataTable dt = new DataTable();
        dt = getdata.GetDatatable("DEFFINITY_RFI_BBBEE_MAIN_FILL", true);
        ddlBBBEERating.DataSource = dt;
        ddlBBBEERating.DataTextField = "BEEStatus";
        ddlBBBEERating.DataValueField = "ID";
        ddlBBBEERating.DataBind();
        ddlBBBEERating.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlBBBEERating.Visible = true;
        txtBBBEERating.Visible = false;
        txtBBBEERating.Enabled = true;
        txtCurrentPoints.Enabled = true;
    }

    private void fillBBBEEDetails(int vendorID)
    {
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("@VendorID", vendorID);
        DataSet ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_TenderVendorScore_FILLBYVendor", parameters);
        txtBBBEERating.Text = ds.Tables[0].Rows[0]["BBBEERating"].ToString();
        txtCurrentPoints.Text = ds.Tables[0].Rows[0]["CurrentPoints"].ToString();
        ddlBBBEERating.Visible = false;
        txtBBBEERating.Visible = true;
        txtBBBEERating.Enabled = false;
        txtCurrentPoints.Enabled = false;
    }

    private void AddTendorScore(int vendorID)
    {
        if (txtCurrentPoints.Enabled == true)
        {
            SqlParameter[] parameter = new SqlParameter[8];
            parameter[0] = new SqlParameter("@VendorID", vendorID);
            parameter[1] = new SqlParameter("@ProjectReference", 0);
            parameter[2] = new SqlParameter("@CategoryID", 0);
            parameter[3] = new SqlParameter("@ElementID", 0);
            parameter[4] = new SqlParameter("@Points", Convert.ToDouble(txtCurrentPoints.Text.ToString()));
            parameter[5] = new SqlParameter("@BaseType", 1);
            parameter[6] = new SqlParameter("@UserID", sessionKeys.UID);
            parameter[7] = new SqlParameter("@DateScored", DateTime.Now);
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_TenderVendorScore_INSERT", parameter);
        }
    }
    protected void ddlBBBEERating_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCurrentPoints.Text = Convert.ToString(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_MAIN_FILLPOINTS", new SqlParameter("@ID", Convert.ToInt32(ddlBBBEERating.SelectedValue)))); 
    }
}
