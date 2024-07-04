using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using UserMgt.DAL;
using UserMgt.Entity;
using System.Collections.Generic;
using System.Data.Linq;





public partial class AdminUserAddress : System.Web.UI.Page
{
    Location.DAL.LocationDataContext locationCntxt;
    UserDataContext AdminULnqcntxt;
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            // Master.PageHead = "Admin";
            if (!this.IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {
                    pnlAddress.Visible = false;
                    pnlCategory.Visible = true;
                }
                BindCountry();
                AddressBind();
                BindCustomFields();
                if (Request.QueryString["uid"] != null)
                {
                    SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
                    BindProductType();
                    // UserId=(Convert.ToInt32(Request.QueryString["uid"]));
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }
    private void BindCountry()
    {
        try
        {
            locationCntxt = new Location.DAL.LocationDataContext();
            var Data = (from c in (locationCntxt.CountryClasses)
                        select new
                                     {
                                         c.ID,
                                         c.Country1

                                     }).Distinct();
            ddlCountry.DataSource = Data.OrderBy(o => o.Country1);
            ddlCountry.DataTextField = "Country1";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
            try
            {
                var setDefault = Data.Where(o => o.Country1.ToLower() == "usa").FirstOrDefault();
                if (setDefault != null)
                    ddlCountry.SelectedValue = setDefault.ID.ToString();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            locationCntxt.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateDetails();
    }

    private bool UpdateDetails()
    {
        bool isZip_change = false;
        try
        {
            int UID = Convert.ToInt32(Request.QueryString["uid"]);
            int Id = 0;
            AdminULnqcntxt = new UserDataContext();
            //UserDetail mydetail = AdminULnqcntxt.UserDeatils_SelectByID(UID);
            var Data = (from UA in AdminULnqcntxt.UserDetails
                        where UA.UserId == UID
                        select new
                        {
                            UA.Id,
                            UA.Address1,
                            UA.Address2,
                            UA.Town,
                            UA.County,
                            UA.PostCode,
                            UA.Country,
                            UA.RangeCovered
                        }).Distinct();

            if (Data.Count() > 0)
            {
                foreach (var d in Data)
                {
                    Id = d.Id;
                }
                if (ddlCountry.SelectedValue.ToString() != "0")
                {
                    if (txtAddress1.Text.ToString() != string.Empty)
                    {
                        var oldData = Data.Where(o => o.Id== Id).FirstOrDefault();
                        if ((oldData.RangeCovered.HasValue ? oldData.RangeCovered.Value.ToString() : "0") != txtRange.Text.Trim())
                            isZip_change = true;
                        else
                            isZip_change = false;
                       

                        AdminULnqcntxt.UserDetails_Update(Id, UID, txtAddress1.Text.ToString(),
                            txtAddress2.Text.ToString(), txtTown.Text.ToString(), txtCounty.Text.ToString(), txtPostcode.Text.ToString(),
                            int.Parse(ddlCountry.SelectedValue.ToString()), Convert.ToInt32(!string.IsNullOrEmpty(txtRange.Text.Trim()) ? txtRange.Text.Trim() : "0"));
                        AdminULnqcntxt.SubmitChanges();

                        lblMsg.Visible = true;
                        lblMsg.Text = "updated successfully";
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Please enter address";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please select county";
                }
            }
            else
            {
                //if (ddlCountry.SelectedValue.ToString() != "0")
                //{
                if (txtAddress1.Text.ToString() != string.Empty)
                {
                    UserDetail UD = new UserDetail();
                    //AdminULnqcntxt.UserDetails UD = new AdminULnqcntxt.UserDetails();
                    UD.UserId = Convert.ToInt32(Request.QueryString["uid"]);
                    UD.Address1 = txtAddress1.Text.ToString();
                    UD.Address2 = txtAddress2.Text.ToString();
                    UD.Town = txtTown.Text.ToString();
                    UD.County = txtCounty.Text.ToString();
                    UD.PostCode = txtPostcode.Text.ToString();
                    UD.Country = int.Parse(ddlCountry.SelectedValue.ToString());
                    UD.RangeCovered = Convert.ToInt32(!string.IsNullOrEmpty(txtRange.Text.Trim()) ? txtRange.Text.Trim() : "0");
                    AdminULnqcntxt.UserDetails.InsertOnSubmit(UD);
                    AdminULnqcntxt.SubmitChanges();
                    int id = UD.Id;
                    if (id > 0)
                    {
                        isZip_change = true;
                        lblMsg.Visible = true;
                        lblMsg.Text = "Added successfully";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please enter address";
                }
                //}
                //else
                //{
                //    lblError.Visible = true;
                //    lblError.Text = "Please select county";
                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return isZip_change;
    }

    private void AddressBind()
    {
        try
        {
            int UID = Convert.ToInt32(Request.QueryString["uid"]);
            AdminULnqcntxt = new UserDataContext();
            
            var Data = (from UA in AdminULnqcntxt.UserDetails
                        where UA.UserId == UID
                        select new
                        {
                            UA.Address1,
                            UA.Address2,
                            UA.Town,
                            UA.County,
                            UA.PostCode,
                            UA.Country,
                            UA.RangeCovered
                        }).Distinct();
            if (Data.Count() > 0)
            {
                foreach (var d in Data)
                {
                    txtAddress1.Text = d.Address1.ToString();
                    txtAddress2.Text = d.Address2.ToString();
                    txtTown.Text = d.Town.ToString();
                    txtCounty.Text = d.County.ToString();
                    txtPostcode.Text = d.PostCode.ToString();
                    ddlCountry.SelectedValue =Convert.ToString(d.Country.ToString());
                    txtRange.Text = d.RangeCovered.HasValue ? d.RangeCovered.Value.ToString() : string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    lblusername.Text = dr["ContractorName"].ToString();
                    //lblallowanceusername.Text = dr["ContractorName"].ToString();
                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }

    private void BindCustomFields()
    {
        IUserRepository<UserMgt.Entity.UserAssociatedPostcode> pRepository = new UserRepository<UserMgt.Entity.UserAssociatedPostcode>();
        var cb = pRepository.GetAll().Where(o => o.UserID == Convert.ToInt32(Request.QueryString["uid"])).ToList();
        list_Customfields.DataSource = cb;
        list_Customfields.DataBind();
    }
    protected void list_Customfields_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            IUserRepository<UserMgt.Entity.UserAssociatedPostcode> pRepository = new UserRepository<UserMgt.Entity.UserAssociatedPostcode>();
            if (e.CommandName == "UpdateItem")
            {
                var dc = pRepository.GetAll().Where(o=>o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                TextBox txteDescription = (TextBox)e.Item.FindControl("txtLable");
                dc.Postcode = txteDescription.Text.Trim();
                if (pRepository.GetAll().Where(p => p.ID != Convert.ToInt32(e.CommandArgument.ToString())  && p.Postcode.ToLower() == txteDescription.Text.Trim().ToLower() && p.UserID == Convert.ToInt32(Request.QueryString["uid"])).Count() == 0)
                {
                    pRepository.Edit(dc);
                    lblMsg1.Text = "Updated sucessfully";
                    list_Customfields.EditIndex = -1;
                    BindCustomFields();
                }
                else
                {
                    lblError1.Text = "Postcode/ Zipcode already exist";
                    //lblMsg1.ForeColor = System.Drawing.Color.Red;
                }
               
            }
            else if (e.CommandName == "Add")
            {
                UserMgt.Entity.UserAssociatedPostcode cf = new UserMgt.Entity.UserAssociatedPostcode();
               
                TextBox txtLable = (TextBox)e.Item.FindControl("txt_insert_lable");
                string[] slist = txtLable.Text.Trim().Split(',');

                foreach (string s in slist)
                {
                    if (pRepository.GetAll().Where(p => p.Postcode.ToLower() ==s.Trim().ToLower() && p.UserID == Convert.ToInt32(Request.QueryString["uid"])).Count() == 0)
                    {
                        cf = new UserMgt.Entity.UserAssociatedPostcode();
                        cf.Postcode = s.Trim();
                        cf.UserID = Convert.ToInt32(Request.QueryString["uid"]);
                        pRepository.Add(cf);
                        lblMsg1.Text = "Added sucessfully";
                        
                    }
                    else
                    {
                        lblError1.Text = "Postcode/ Zipcode already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }



                }

                BindCustomFields();

            }
            else if (e.CommandName == "Del")
            {
                //cb = new CustomFieldsBAL();
                if (Convert.ToInt32(e.CommandArgument) > 0)
                {
                    var entity = pRepository.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    pRepository.Delete(entity);
                    lblMsg1.Text = "Deleted sucessfully";
                    BindCustomFields();
                }
            }
            else if (e.CommandName == "Cancel")
            {
                TextBox txtLable = (TextBox)e.Item.FindControl("txtDescription");
                txtLable.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void list_Customfields_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        list_Customfields.EditIndex = -1;
        //BindCustomFields();
    }
    protected void list_Customfields_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        list_Customfields.EditIndex = e.NewEditIndex;
        BindCustomFields();
    }
    protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
       
    }
    private List<DC.Entity.Category> BindType()
    {
        IDCRespository<DC.Entity.Category> atRepository = new DCRepository<DC.Entity.Category>();
        return atRepository.GetAll().ToList();
    }
    private List<DC.Entity.SubCategory> BindMake()
    {
        IDCRespository<DC.Entity.SubCategory> atRepository = new DCRepository<DC.Entity.SubCategory>();
        return atRepository.GetAll().ToList();
    }
    //private void BindMake()
    //{
    //    IUserRepository<UserMgt.Entity.UserAssociateToType> pRepository = new UserRepository<UserMgt.Entity.UserAssociateToType>();
    //    var cb = pRepository.GetAll().Where(o => o.UserID == Convert.ToInt32(Request.QueryString["uid"])).ToList();
    //    listAppliances.DataSource = cb;
    //    listAppliances.DataBind();
    //}
    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (list_Customfields.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindCustomFields();
    }
    private void BindProductType()
    {
        IUserRepository<UserMgt.Entity.UserAssociateToType> pRepository = new UserRepository<UserMgt.Entity.UserAssociateToType>();
        var typelist = BindType();
        var makelist = BindMake();
        var sResult = pRepository.GetAll().Where(o => o.UserID == Convert.ToInt32(Request.QueryString["uid"])).ToList();

        var cd = (from s in sResult
                  select new
                  {
                      s.Contractor,
                      s.MakeID,
                      s.ProductTypeID,
                      ID = s.UATID,
                      s.UserID,
                      TypeName = typelist.Where(p=>p.ID == s.ProductTypeID).FirstOrDefault().Name,
                      MakeName = s.MakeID >0?  makelist.Where(p=>p.ID == s.MakeID).FirstOrDefault().Name:string.Empty,
                  }).ToList();
         
        listAppliances.DataSource = cd;
        listAppliances.DataBind();
        //DropDownList ddltype = (DropDownList)listAppliances.InsertItem.FindControl("ddlTypeF");
        //ddltype.DataSource = typelist.OrderBy(o => o.Type).ToList();
        //ddltype.DataTextField = "Type";
        //ddltype.DataValueField = "TypeID";
        //ddltype.DataBind();
        //ddltype.Items.Insert(0, new ListItem("Please select...", "0"));

        //DropDownList ddlmake = (DropDownList)listAppliances.InsertItem.FindControl("ddlMakeF");
        //ddlmake.DataSource = BindMake().OrderBy(o => o.Make).ToList();
        //ddlmake.DataTextField = "Make";
        //ddlmake.DataValueField = "MakeID";
        //ddlmake.DataBind();
        //ddlmake.Items.Insert(0, new ListItem("Please select...", "0"));
    }

    protected void listAppliances_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            IUserRepository<UserMgt.Entity.UserAssociateToType> pRepository = new UserRepository<UserMgt.Entity.UserAssociateToType>();
              if (e.CommandName == "Edit")
            {
                //var dc = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                //DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                //DropDownList ddlmake = (DropDownList)e.Item.FindControl("ddlMake");
                //ddltype.DataSource = BindType().OrderBy(o => o.Type).ToList();
                //ddltype.DataTextField = "Type";
                //ddltype.DataValueField = "TypeID";
                //ddltype.DataBind();

                //ddltype.Items.Insert(0, new ListItem("Please select...", "0"));

                //ddltype.SelectedValue = dc.ProductTypeID.Value.ToString();

                //ddlmake.DataSource = BindMake().OrderBy(o => o.Make).ToList();
                //ddlmake.DataTextField = "Make";
                //ddlmake.DataValueField = "MakeID";
                //ddlmake.DataBind();
                //ddlmake.Items.Insert(0, new ListItem("Please select...", "0"));
                //ddlmake.SelectedValue = dc.MakeID.ToString();

             }
            else if (e.CommandName == "UpdateItem")
            {
                var dc = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlType");
                DropDownList ddlMake = (DropDownList)e.Item.FindControl("ddlMake");
                dc.ProductTypeID = Convert.ToInt32(ddlType.SelectedValue);
                var makeid = 0;
                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                {
                    makeid = Convert.ToInt32(ddlMake.SelectedValue);
                }
                dc.MakeID = makeid;
                if (pRepository.GetAll().Where(p => p.UATID != Convert.ToInt32(e.CommandArgument.ToString()) && p.MakeID == Convert.ToInt32(!string.IsNullOrEmpty(ddlMake.SelectedValue) ? ddlMake.SelectedValue : "0") && p.ProductTypeID == Convert.ToInt32(ddlType.SelectedValue) && p.UserID == Convert.ToInt32(Request.QueryString["uid"])).Count() == 0)
                {
                    pRepository.Edit(dc);
                    lblMsg_a.Text = "Updated sucessfully";
                    listAppliances.EditIndex = -1;
                    BindProductType();
                }
                else
                {
                    lblError_a.Text = "Item already exist";
                    //lblMsg1.ForeColor = System.Drawing.Color.Red;
                }

            }
            else if (e.CommandName == "Add")
            {
                UserMgt.Entity.UserAssociateToType cf = new UserMgt.Entity.UserAssociateToType();

                DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlTypeF");
                DropDownList ddlMake = (DropDownList)e.Item.FindControl("ddlMakeF");
                if (pRepository.GetAll().Where(p => p.MakeID == Convert.ToInt32(!string.IsNullOrEmpty(ddlMake.SelectedValue)?ddlMake.SelectedValue:"0") && p.ProductTypeID == Convert.ToInt32(ddlType.SelectedValue) && p.UserID == Convert.ToInt32(Request.QueryString["uid"])).Count() == 0)
                {
                    var makeid = 0;
                    if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    {
                        makeid = Convert.ToInt32(ddlMake.SelectedValue);
                    }
                    cf.ProductTypeID = Convert.ToInt32(ddlType.SelectedValue);
                    cf.MakeID = makeid;
                    cf.UserID = Convert.ToInt32(Request.QueryString["uid"]);
                    pRepository.Add(cf);
                    lblMsg_a.Text = "Added sucessfully";
                    BindProductType();
                }
                else
                {
                    lblError_a.Text = "item already exist";
                    //lblMsg1.ForeColor = System.Drawing.Color.Red;
                }

            }
            else if (e.CommandName == "Del")
            {
                //cb = new CustomFieldsBAL();
                if (Convert.ToInt32(e.CommandArgument) > 0)
                {
                    var entity = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    pRepository.Delete(entity);
                    lblMsg_a.Text = "Deleted sucessfully";
                    BindProductType();
                }
            }
            else if (e.CommandName == "Cancel")
            {
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void listAppliances_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        listAppliances.EditIndex = -1;
        //BindCustomFields();
    }
    protected void listAppliances_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        listAppliances.EditIndex = e.NewEditIndex;
        BindProductType();
    }
    protected void listAppliances_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
       
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblID = (Label)e.Item.FindControl("lblID");//lblID
            
            
            DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
            DropDownList ddlmake = (DropDownList)e.Item.FindControl("ddlMake");
            AjaxControlToolkit.CascadingDropDown ctype = (AjaxControlToolkit.CascadingDropDown)e.Item.FindControl("ccdCategoryNew");
            AjaxControlToolkit.CascadingDropDown cmake = (AjaxControlToolkit.CascadingDropDown)e.Item.FindControl("ccdSubCategoryNew");
            if (ddltype != null)
            {
                IUserRepository<UserMgt.Entity.UserAssociateToType> pRepository = new UserRepository<UserMgt.Entity.UserAssociateToType>();
                var dc = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(lblID.Text)).FirstOrDefault();

                //ddltype.DataSource = BindType().OrderBy(o => o.Type).ToList();
                //ddltype.DataTextField = "Type";
                //ddltype.DataValueField = "TypeID";
                //ddltype.DataBind();

                //ddltype.Items.Insert(0, new ListItem("Please select...", "0"));
                ctype.SelectedValue = dc.ProductTypeID.ToString();
                cmake.DataBind();
                cmake.SelectedValue = dc.MakeID.ToString();
                //ddltype.SelectedValue = dc.ProductTypeID.ToString();

                //ddlmake.DataSource = BindMake().OrderBy(o => o.Make).ToList();
                //ddlmake.DataTextField = "Make";
                //ddlmake.DataValueField = "MakeID";
                //ddlmake.DataBind();
                //ddlmake.Items.Insert(0, new ListItem("Please select...", "0"));
                //ddlmake.SelectedValue = dc.MakeID.ToString();
            }
        }

    }

    protected void btnRange_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(!string.IsNullOrEmpty(txtRange.Text.Trim()) ? txtRange.Text.Trim() : "0") > 0)
        {
            if (txtPostcode != null)
            {
                var retval = UpdateDetails();
                //if details are update zip code import will delete and re add the zip codes
                if (retval)
                    DeffinityManager.BLL.ZipcodeBAL.UpdateNearByZipCodes(Convert.ToInt32(Request.QueryString["uid"]), txtPostcode.Text.Trim(), Convert.ToInt32(!string.IsNullOrEmpty(txtRange.Text.Trim()) ? txtRange.Text.Trim() : "0"));
                else
                DeffinityManager.BLL.ZipcodeBAL.AddNearByZipCodes(Convert.ToInt32(Request.QueryString["uid"]), txtPostcode.Text.Trim(), Convert.ToInt32(!string.IsNullOrEmpty(txtRange.Text.Trim()) ? txtRange.Text.Trim() : "0"));

                BindCustomFields();
                lblMsg1.Text = "Added successfully";
            }
            else
            {
                lblError1.Text = "Please enter the Zip code/ Postcode";
            }
        }
        else
        {
            lblError1.Text = "Please enter valid range";
        }
    }
}