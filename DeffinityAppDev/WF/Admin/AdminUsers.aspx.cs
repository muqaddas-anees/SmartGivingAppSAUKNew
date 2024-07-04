using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.BE;
using Deffinity.BLL;
using Certifications;
using VT.Entity;
using VT.DAL;
using System.Text;
//using Deffinity.TrainingEntity;
//using Deffinity.TrainingManager;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using UserMgt.DAL;
using UserMgt.Entity;
using System.Linq;

public partial class AdminUsers_1 : System.Web.UI.Page
{
    Location.DAL.LocationDataContext locationCntxt;
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Master.PageHead = "Admin";

            lblError1.Text = "";
            btnListUsers.Visible = false;

            if (!this.IsPostBack)
            {
                //Bind Asset type/ expert data
                BindExpertdata();
                //bind changes resposibility users
                bind_ddluserrespose();
                if (drContractors.SelectedValue != "0")
                {
                    RequiredFieldValidator4.Enabled = false;
                    RequiredFieldValidator5.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    RequiredFieldValidator8.Enabled = false;
                    ////RequiredFieldValidator8.Visible = false;
                    //txtConfirmPassword.CausesValidation = false;
                }
                //else
                //{
                //    RequiredFieldValidator4.Enabled = true;
                //    RequiredFieldValidator5.Visible = true;
                //    RequiredFieldValidator7.Enabled = true;
                //    RequiredFieldValidator8.Enabled = true;
                //}
                defaultBindings();
                //BindReqData();
                BindCountry();
                usertabsvisibility(true, false);
                
                if (Request.QueryString["sid"] != null)
                {
                    if (Request.QueryString["sid"] == "10")
                    {
                        //Panel1.Visible = true;
                        drpermission.SelectedValue = "10";
                        drpermission.Enabled = false;
                        GetID.Visible = true;
                        txtEmail.Visible = false;
                        txtCompany.Visible = false;
                        GetUser.Visible = false;
                        //ddlCasual_Labour.SelectedValue = "3";
                        BindUsers(int.Parse(drpermission.SelectedValue));
                        drContractors.SelectedValue = "0";
                       
                    }
                    if (Request.QueryString["sid"] == "7")
                    {
                        //GetUser.Visible = false;
                       // Panel1.Visible = true;
                        GetID.Visible = false;
                        drpermission.SelectedValue = "7";
                        drpermission.Enabled = false;
                        GetCustomer.Visible = false;
                        BindUsers(int.Parse(drpermission.SelectedValue));
                       // drContractors.SelectedValue = "0";
                        //txtCasualEmail.Visible = false;
                        //txtEmail.Visible = false;
                    }
                }
               
                if (Request.QueryString["uid"] != null)
                {
                    PanelVisble();
                    SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
                    DisplayImage(Convert.ToInt32(Request.QueryString["uid"]));
                    //set userdetails
                    Set_UserDetails(Convert.ToInt32(Request.QueryString["uid"]));
                    //Newly added
                    SelectContractorDetails(Convert.ToInt32(Request.QueryString["uid"]));

                }
                if (Request.QueryString["Type"] != null)
                {
                    ChangeInsertVisible();
                }

            }
            getUserId.Value = Request.QueryString["uid"];

            //add css dynamically
            HtmlLink css = new HtmlLink();
            css.Href = ResolveClientUrl("~/stylcss/ajaxtabs.css");
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            //css.Attributes["media"] = "all";
            Page.Header.Controls.Add(css);
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void PanelVisble()
    {
        pnlusername.Visible = true;
        UserData.Visible = true;
        pnluserbuttons.Visible = false;
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
            ddlCountry1.DataSource = Data.OrderBy(o => o.Country1);
            ddlCountry1.DataTextField = "Country1";
            ddlCountry1.DataValueField = "ID";
            ddlCountry1.DataBind();
            ddlCountry1.Items.Insert(0, new ListItem("Please select...", "0"));
            locationCntxt.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);


        }


    }

    private void BindUsers(int SID)
    {
        //int i = int.Parse(drContractors.SelectedValue);
        //drContractors.SelectedValue = "0";
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text,
            "select ContractorName,ID from Contractors where Status='ACTIVE' and sid<>99 and sid=@Sid and ID in (select UserID from UserToCompany where CompanyID = @CompanyID) order by ContractorName",
            new SqlParameter("@Sid", SID), new SqlParameter("@CompanyID", sessionKeys.PortfolioID)).Tables[0];
        drContractors.DataSource = dt;
        drContractors.DataTextField = "ContractorName";
        drContractors.DataValueField = "ID";
        drContractors.DataBind();
        drContractors.Items.Insert(0, new ListItem("Please select...", "0"));
        if (int.Parse(drContractors.SelectedValue) != 0)
        {
            drContractors.SelectedValue = drContractors.SelectedValue; //"0"; //i.ToString();
        }
        else
        {
            drContractors.SelectedValue = "0";
        }
        //if (string.IsNullOrEmpty(drCon == "")
        //{
        //    drContractors.SelectedValue = "0";
        //}
        

  
    }
    private void defaultBindings()
    {
        try
        {
            DataTable dt = new DataTable();

            getData.DdlBindSelect(drContractors, "select * from Contractors where Status='ACTIVE' and sid not in (-99,7,10,8) and ID in (select UserID from UserToCompany where CompanyID = "+sessionKeys.PortfolioID+") order by ContractorName", "ID", "ContractorName", false, false, true);
            getData.DdlBindSelect(ddrTimesheetApprove, "DN_ResourcesAdmin", "ID", "ContractorName", true, false, true);
            getData.DdlBindSelect(timesheetsecondapprover, "DN_ResourcesAdmin", "ID", "ContractorName", true, false, true);
            if (Request.QueryString["sid"] != null)
            {
                int id = int.Parse(string.IsNullOrEmpty(drpermission.SelectedValue) ? "0" : drpermission.SelectedValue);
                getData.DdlBindSelect(drpermission, "DN_ResourceType", "ID", "Type", true, true);
                drpermission.SelectedValue = id.ToString();

                if (Request.QueryString["Type"] != null)
                {
                    string T = ReturnTypeValue(int.Parse(Request.QueryString["Type"]));

                    btngohome.NavigateUrl = "~/WF/Admin/UserManagement.aspx?Type=" + T;
                }
            }
            else
            {
                int id = int.Parse(string.IsNullOrEmpty(drpermission.SelectedValue)?"0":drpermission.SelectedValue);
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text,
                    "select * from ContractorType where id in (1,4) order by type ").Tables[0];
                drpermission.DataSource = dt;
                drpermission.DataTextField = "Type";
                drpermission.DataValueField = "ID";
                drpermission.DataBind();
                
                drpermission.Items.Insert(0, new ListItem("Please select...", "0"));

                if (Request.QueryString["Type"] != null)
                {
                    drpermission.SelectedValue = Request.QueryString["Type"].ToString();
                    string T=ReturnTypeValue(int.Parse(Request.QueryString["Type"]));

                    btngohome.NavigateUrl = "~/WF/Admin/UserManagement.aspx?Type=" + T;
                }
            }
            //getData.DdlBindSelect(ddlCasual_Labour, "Deffinity_LabourCategory1", "ID", "LabourType", true, false, true);
            getData.DdlBindSelect(ddlCompany, "Select distinct Company,Company from contractors where Company <>''", "Company", "Company", false, true);
            ddlCompany.SelectedIndex = 0;
           // drContractors.SelectedIndex = 0;
           // drpermission.SelectedIndex = 0;
           // ddrTimesheetApprove.SelectedIndex = 0;
            //bind ExperienceClassification 
            BindExperienceClassification();
            BindDepartment();
            BindArea();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public string ReturnTypeValue(int type)
    {
        string ReturnType = string.Empty;
        if (type == 1)
        {
            ReturnType = "SmartPros";
        }
        else if (type == 2)
        {
            ReturnType = "ServiceManagers";
        }
        else if (type == 3)
        {
            ReturnType = "PM";
        }
        else if (type == 4)
        {
            ReturnType = "SmartTechs";
        }
        else if (type == 10)
        {
            ReturnType = "CasualLabour";
        }
        else if (type == 6)
        {
            ReturnType = "Dashboard";
        }
        else if (type == 11)
        {
            ReturnType = "Sales";
        }
        else if (type == 12)
        {
            ReturnType = "Customer Service";
        }
        return ReturnType;
    }
    private void BindExperienceClassification()
    {
        try
        {
            ddlExperienceClassification.DataSource = DefaultDatabind.b_ExperienceClassification();
            ddlExperienceClassification.DataTextField = "ExpClassification";
            ddlExperienceClassification.DataValueField = "ID";
            ddlExperienceClassification.DataBind();
            ddlExperienceClassification.Items.Insert(0, Constants.ddlDefaultBind(true));
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    private void clearFields()
    {
        txtDetails.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtsid.Text = string.Empty;
        txtUser.Text = string.Empty;
        txtusername.Text = string.Empty;
        txtCompany.Visible = false;
        ddlCompany.Visible = true;
        btnAddCompany.Visible = true;
        txtEditName.Text = string.Empty;
        txtContactNumber.Text = string.Empty;
        txtReleaseDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        drContractors.SelectedIndex = 0;
        //drpermission.SelectedIndex = 0;
        ddrTimesheetApprove.SelectedIndex = 0;
        ddlExperienceClassification.SelectedIndex = -1;// 0;
        txtCasualEmail.Text = string.Empty;
        //ddlCasual_Labour.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = -1;
        ddlArea.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;
    }
    private void ChangeInsertVisible()
    {
        RequiredFieldValidator4.Enabled = true;
        RequiredFieldValidator5.Visible = true;
        RequiredFieldValidator7.Enabled = true ;
        RequiredFieldValidator8.Enabled = true;
        drContractors.Visible = false;
        txtUser.Visible = true;
        imgCancel.Visible = false;
        defaultBindings();
        clearFields();
        PanleEditName.Visible = false;
    }
    private void ChangeUpdateVisible()
    {
        defaultBindings();
        drContractors.Visible = true;
        txtUser.Visible = false;
        imgCancel.Visible = false;
        clearFields();
    }
    protected void imgAddNew_Click(object sender, EventArgs e)
    {
        ChangeInsertVisible();
        //timeSheetRate.Visible = false;
    }
    protected void imgCancel_Click(object sender, EventArgs e)
    {
        clearNamefromName();
        if (drpermission.SelectedValue == "10")
        {
            Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=10");
        }
        else if (drpermission.SelectedValue == "7")
        {
            Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=7");
        }
        else
        {
            Response.Redirect("~/WF/Admin/AdminUsers.aspx");
        }
    }
    private void clearNamefromName()
    {
        imgAddNew.Visible = true;
        imgCancel.Visible = false;
        drContractors.Visible = true;
        txtUser.Visible = false;
        clearFields();
        PanleEditName.Visible = false;
    }
    protected void imgactiveuser_Click(object sender, EventArgs e)
    {
        ChangeUpdateVisible();
        imgactiveuser.Visible = false;
        btn_showInActiveUser.Visible = false;
        if (Request.QueryString["sid"] != null)
        {
            if (Request.QueryString["sid"] == "10")
            {
                getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='ACTIVE' and sid=10 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
            }
            else if (Request.QueryString["sid"] == "7")
            {
                getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='ACTIVE' and sid=7 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
            }


        }
        else
        {
            getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='ACTIVE' and sid not in (10,7) and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ")  order by ContractorName", "ID", "ContractorName", false, false,true);
        }
            
        //defaultBindings();
        
      

    }
    protected void btn_showInActiveUser_Click(object sender, EventArgs e)
    {
        try
        {
            ChangeUpdateVisible();
            if (Request.QueryString["sid"] != null)
            {
                if (Request.QueryString["sid"] == "10")
                {
                    getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid=10 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, true);
                }
                else if (Request.QueryString["sid"] == "7")
                {
                    getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid=7 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, true);
                }
               
                
            }
            else
            {
                getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid not in (10,7) and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ")  order by ContractorName", "ID", "ContractorName", false, true);
            }
            
            clearFields();
            btn_showInActiveUser.Visible = false;
            imgactiveuser.Visible = false;
           
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    protected void drContractors_SelectedIndexChanged(object sender, EventArgs e)
    {
        usertabsvisibility(false, true);
       
        if (drContractors.SelectedValue != "Please select...")
        {
            PanleEditName.Visible = true;
            if (drpermission.SelectedValue == "10")
            {
                Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue) + "&sid=10");
            }
            else if (drpermission.SelectedValue == "7")
            {
                Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue) + "&sid=7");
            }
            else
            {
                Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
            }
        }
        
    }
    private void DisplayImage(int UserID)
    {
        string filepath = Server.MapPath("~/WF/UploadData/Users/ThumbNails/") + "user_" + UserID.ToString() + ".png";
        if (System.IO.File.Exists(filepath))
        {
            imgUser.Visible = true;
            imgUser.ImageUrl = string.Format("~/WF/UploadData/Users/ThumbNails/user_{0}.png?date={1}", UserID.ToString(), DateTime.Now.Second.ToString());
            //imgUser.NavigateUrl = string.Format("~/DisplayUser.aspx?userid={0}", UserID.ToString());
            imgUser.Target = "_black";
        }
        else
        {
            imgUser.Visible = false;
        }
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
           PanleEditName.Visible = true;
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    
                    txtEditName.Text = dr["ContractorName"].ToString();

                    if (dr["SID"].ToString() == "10")
                    {
                        GetID.Visible = true;
                        txtEmail.Visible = false;
                        txtCompany.Visible = false;
                        GetUser.Visible = false;
                       //ddlCasual_Labour.SelectedValue=Convert.ToInt16(dr["CasualLabourType"]).ToString();
                       drpermission.SelectedValue = dr["SID"].ToString();
                       txtEditName.Text = dr["ContractorName"].ToString();
                       txtCasualEmail.Text = dr["EmailAddress"].ToString();
                       lblusername.Text = txtEditName.Text;
                       //rbGender.SelectedValue = Convert.ToString(dr["Gender"]);
                       //if (Convert.ToBoolean(dr["Gender"]) == true)
                       //{
                       //    rbGender.SelectedValue = "1";
                       //}
                       //else
                       //{
                       //    rbGender.SelectedValue = "0";
                       //}
                       
                       //txtDOB.Text = (dob == "01/01/1900" ? string.Empty : dob);
                       
                       try
                       {
                           
                           drpermission.SelectedValue = dr["SID"].ToString();
                           if (Convert.ToInt32(dr["SID"]) == 8)
                               btnListUsers.Visible = true;
                           ddlCasualLab.SelectedValue = dr["Status"].ToString();
                           if (ddlCasualLab.SelectedValue.ToLower() == "inactive")
                           {
                               getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid=10 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
                           }
                           txtDetails.Text = dr["Details"].ToString();
                           txtEmail.Text = dr["EmailAddress"].ToString();
                           txtCasualEmail.Text = dr["EmailAddress"].ToString();
                           txtsid.Text = dr["SID"].ToString();
                           txtusername.Text = dr["LoginName"].ToString();
                           ddrTimesheetApprove.SelectedValue = dr["TimeApproveID"].ToString() == "" ? "0" : dr["TimeApproveID"].ToString();
                           timesheetsecondapprover.SelectedValue = dr["SecondTSApprover"].ToString() == "" ? "0" : dr["SecondTSApprover"].ToString();
                           ddlCompany.SelectedValue = RetComapanyName(dr["Company"].ToString().Trim());
                           txtContactNumber.Text = dr["ContactNumber"].ToString();
                           ddlExperienceClassification.SelectedValue = dr["ExpClassification"].ToString() == "" ? "0" : dr["ExpClassification"].ToString();
                           txtStartDate.Text = (dr["EmploymentStartDate"].ToString().Contains("01/01/1990") == true || dr["EmploymentStartDate"].ToString() == "") ? "" : DateTime.Parse(dr["EmploymentStartDate"].ToString()).ToShortDateString();
                           txtReleaseDate.Text = (dr["ReleaseDate"].ToString().Contains("01/01/1990") == true || dr["ReleaseDate"].ToString() == "") ? "" : DateTime.Parse(dr["ReleaseDate"].ToString()).ToShortDateString();
                           
                           ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(dr["DepartmentID"].ToString()));
                           //Bind area based on seleted department
                           BindArea();
                           ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(dr["DepartmentID"].ToString()));
                           chkReset.Checked = false;
                       }
                       catch (Exception ex)
                       {
                           LogExceptions.LogException(ex.Message, "Casula labours");
                       }

                    }

                    else
                    {
                        
                        //change the panel
                        GetID.Visible = false;
                        GetUser.Visible = true;
                        txtEmail.Visible = true;
                        //read the fields
                        userName = txtEditName.Text;
                        lblusername.Text = userName;
                        drStatus.SelectedValue = dr["Status"].ToString();
                        //if status is inactive then we need to display inactive users
                       
                            if (drStatus.SelectedValue.ToLower() == "inactive")
                            {
                                if (dr["SID"].ToString() == "7")
                                {
                                    getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid=7 and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
                                }
                                else
                                {
                                    getData.DdlBindSelect(drContractors, "select * from Contractors where upper(Status)='INACTIVE' and sid not in (7,10) and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
                                }
                        }
                        drpermission.SelectedValue = dr["SID"].ToString();
                        if (Convert.ToInt32(dr["SID"]) == 8)
                            btnListUsers.Visible = true;
                        //if (Convert.ToBoolean(dr["Gender"]) == true)
                        //{
                        //    rbGender.SelectedValue = "1";
                        //}
                        //else
                        //{
                        //    rbGender.SelectedValue = "0";
                        //}
                        
                        //txtDOB.Text = (dob == "01/01/1900" ? string.Empty : dob);
                        txtDetails.Text = dr["Details"].ToString();
                        txtEmail.Text = dr["EmailAddress"].ToString();
                        txtCasualEmail.Text = dr["EmailAddress"].ToString();
                        txtsid.Text = dr["SID"].ToString();
                        txtusername.Text = dr["LoginName"].ToString();
                        ddrTimesheetApprove.SelectedValue = dr["TimeApproveID"].ToString() == "" ? "0" : dr["TimeApproveID"].ToString();
                        timesheetsecondapprover.SelectedValue = dr["SecondTSApprover"].ToString() == "" ? "0" : dr["SecondTSApprover"].ToString();
                        ddlCompany.SelectedValue = RetComapanyName(dr["Company"].ToString().Trim());
                        txtContactNumber.Text = dr["ContactNumber"].ToString();
                        ddlExperienceClassification.SelectedValue = dr["ExpClassification"].ToString() == "" ? "0" : dr["ExpClassification"].ToString();
                        txtStartDate.Text = (dr["EmploymentStartDate"].ToString().Contains("01/01/1990") == true || dr["EmploymentStartDate"].ToString() == "") ? "" : DateTime.Parse(dr["EmploymentStartDate"].ToString()).ToShortDateString();
                        txtReleaseDate.Text = (dr["ReleaseDate"].ToString().Contains("01/01/1990") == true || dr["ReleaseDate"].ToString() == "") ? "" : DateTime.Parse(dr["ReleaseDate"].ToString()).ToShortDateString();
                        ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(dr["DepartmentID"].ToString()));
                        //Bind area based on seleted department
                        try
                        {
                            BindArea();
                        }
                        catch (Exception ex)
                        { LogExceptions.WriteExceptionLog(ex); }
                        ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(dr["AreaID"].ToString()));
                        chkReset.Checked = Convert.ToBoolean(dr["ResetPassword"]);
                        //ddlDepartment.SelectedValue = dr["DepartmentID"].ToString();
                        //ddlArea.SelectedValue = dr["AreaID"].ToString();
                    }
                    //set contractor value
                    drContractors.SelectedValue = cid.ToString();
                    //isImage = Convert.ToBoolean(dr["IsImage"].ToString());

                    //try
                    //{

                    //    drContractors.SelectedValue = cid.ToString();
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogExceptions.WriteExceptionLog(ex);
                    //}
                    
                }
                dr.Close();
            }
            cmd.Dispose();
            //Response.Redirect("AdminUsers.aspx?sid=" + drpermission.SelectedValue + "&uid=" + drContractors.SelectedValue);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private string RetComapanyName(string company)
    {
        string retVal = company;
        if ((string.IsNullOrEmpty(retVal)) ||(retVal=="0"))
        {
            retVal = "Please select...";

        }
        return retVal;
    }
    protected void imgsubmitt_Click(object sender, EventArgs e)
    {
        //DN_InsertUser,DN_UpdateUser
        lblError1.Text = string.Empty;
       
        try
        {



            if(CheckEmailNotExists(txtEmail.Text.Trim(),Convert.ToInt32(drContractors.SelectedValue)))
            {
            
          
            int userid = 0;
            //bool Gender;
            //if (rbGender.SelectedValue == "0")
            //    Gender = false;
            //else
            //    Gender = true;
            if (int.Parse(drpermission.SelectedValue) == 10)
            {
                if (!drContractors.Visible)
                {
                    userid = UserInsert(txtUser.Text.Trim(), txtUser.Text.Trim(),
                                        txtUser.Text.Trim(), Convert.ToInt32(drpermission.SelectedValue), ddlCasualLab.SelectedItem.Value,
                                        string.Empty, txtCasualEmail.Text.Trim()
                                        , 0, 0, string.Empty, string.Empty, string.Empty, string.Empty,
                                        0, 0, 0, int.Parse(ddlCasual_Labour.SelectedValue), false);
                    if (userid > 0)
                    {
                        // ContractorDetail Insert
                        ContractorDetail contractorDetail = new ContractorDetail();
                        contractorDetail.UserID = userid;
                        if(rbGender.SelectedValue!=string.Empty)
                        contractorDetail.Gender = (rbGender.SelectedValue == "0" ? false : true);
                        contractorDetail.DOB = Convert.ToDateTime(string.IsNullOrEmpty(txtDOB.Text)?"01/01/1900":txtDOB.Text);
                        contractorDetail.City = txtCity.Text;
                        contractorDetail.Country = int.Parse(ddlCountry1.SelectedValue);
                        contractorDetail.ShowFinancialCosts = (chkFinance.Checked == true ? true : false);
                        contractorDetail.DefaultCustomerSite = Convert.ToInt32(ddlSite.SelectedValue);
                        
                        ContractorDetailsInsert(contractorDetail);
                    }
                  

                    if (userid > 0)
                    {
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + userid + "&sid=10");
                    }
                    if (ddlCasualLab.SelectedItem.Value == "InActive")
                    {
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=10");
                    }

                   // usertabsvisibility(false, true);
                }
                else
                {
                    if (drContractors.SelectedValue != "0")
                    {
                        UserUpdate(int.Parse(drContractors.SelectedValue), txtEditName.Text.Trim(), txtEditName.Text.Trim(),
                                           txtCasulaPas.Text.Trim(), Convert.ToInt32(drpermission.SelectedValue), ddlCasualLab.SelectedItem.Value, string.Empty,
                                            txtCasualEmail.Text.Trim(), 0, 0, string.Empty, string.Empty, string.Empty, string.Empty,
                                            0, 0, 0, int.Parse(ddlCasual_Labour.SelectedValue),false);

                        //ContractorDetail Update
                        ContractorDetail contractorDetail = new ContractorDetail();
                        contractorDetail.UserID = Convert.ToInt32(Request.QueryString["uid"]);
                        if (rbGender.SelectedValue != string.Empty)
                        contractorDetail.Gender = (rbGender.SelectedValue == "0" ? false : true);
                        contractorDetail.DOB = Convert.ToDateTime(string.IsNullOrEmpty(txtDOB.Text)?"01/01/1900":txtDOB.Text);
                        contractorDetail.City = txtCity.Text;
                        contractorDetail.ShowFinancialCosts = (chkFinance.Checked == true ? true : false);
                        contractorDetail.Country = int.Parse(ddlCountry1.SelectedValue);
                        contractorDetail.DefaultCustomerSite = Convert.ToInt32(ddlSite.SelectedValue);
                        ContractorDetailsUpdate(contractorDetail);

                        if (ddlCasualLab.SelectedItem.Value == "InActive")
                        {
                            Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=10");
                        }
                    }
                    else
                    {
                        lblError1.Text = "Please enter fullname";
                    }

                }
            }
            else
            {

                if (checkUsername())
                {
                    //check company name
                    //if (ValidCompanyName())
                    //{
                        //insert user
                        if (CheckPassword())
                        {
                            if (PermissionManager.CheckMaxUsersAllowed(Convert.ToInt32(drpermission.SelectedValue)))
                            {
                                userid = UserInsert(txtUser.Text.Trim(), txtusername.Text.Trim(),
                                            txtPassword.Text.Trim(), Convert.ToInt32(drpermission.SelectedValue), drStatus.SelectedItem.Value, txtDetails.Text.Trim(),
                                            txtEmail.Text.Trim(), Convert.ToInt32(ddrTimesheetApprove.SelectedValue), Convert.ToInt32(timesheetsecondapprover.SelectedValue),
                                            GetCompanyName(), txtContactNumber.Text.Trim(), txtReleaseDate.Text.Trim(), txtStartDate.Text.Trim(),
                                            Convert.ToInt32(ddlExperienceClassification.SelectedValue), int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlArea.SelectedValue), int.Parse(ddlCasual_Labour.SelectedValue),chkReset.Checked);
                                if (userid > 0)
                                {
                                    // ContractorDetail Insert
                                    ContractorDetail contractorDetail = new ContractorDetail();
                                    contractorDetail.UserID = userid;
                                    if (rbGender.SelectedValue != string.Empty)
                                    contractorDetail.Gender = (rbGender.SelectedValue == "0" ? false : true);
                                     contractorDetail.DOB = Convert.ToDateTime(string.IsNullOrEmpty(txtDOB.Text)?"01/01/1900":txtDOB.Text);
                                    contractorDetail.City = txtCity.Text;
                                    contractorDetail.ShowFinancialCosts = (chkFinance.Checked == true ? true : false);
                                    contractorDetail.Country = int.Parse(ddlCountry1.SelectedValue);
                                    contractorDetail.DefaultCustomerSite = Convert.ToInt32(ddlSite.SelectedValue);
                                    ContractorDetailsInsert(contractorDetail);
                                }


                                if (drpermission.SelectedValue == "7"&& userid > 0)
                                {
                                    Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + userid + "&sid=7");
                                }
                                if (userid > 0)
                                {
                                    Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + userid);
                                }
                                //else
                                //{
                                //    Response.Redirect("AdminUsers.aspx?uid=" + userid);
                                //}
                                
                                if (drStatus.SelectedItem.Value == "InActive")
                                {
                                    if (drpermission.SelectedValue == "7")
                                    {
                                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=7");
                                    }
                                    else
                                    {
                                        Response.Redirect("~/WF/Admin/AdminUsers.aspx");
                                    }
                                }
                                //usertabsvisibility(false, true);
                            }
                            else
                            {
                                RequiredFieldValidator2.ErrorMessage = "";
                                lblError1.Text = "Sorry, but you have exceeded the number of licences for this type of user. Please contact Deffinity on 0800 680 0215";
                            }
                        }
                        else
                        {
                            RequiredFieldValidator2.ErrorMessage = "";
                            lblError1.Text = "Please enter password";
                        }
                       
                    // }
                    //else
                    //{
                    //    RequiredFieldValidator2.ErrorMessage = "";
                    //    lblError1.Text = "Please enter Company name";
                    //}
                }
                else
                {
                    txtEditName.Visible = true;
                    if (txtEditName.Text.Trim() != string.Empty)
                    {
                        //check company name
                        //if (ValidCompanyName() == true)
                        //{
                        

                            UserUpdate(int.Parse(drContractors.SelectedValue), txtEditName.Text.Trim(), txtusername.Text.Trim(),
                                        txtPassword.Text.Trim(), Convert.ToInt32(drpermission.SelectedValue), drStatus.SelectedItem.Value, txtDetails.Text.Trim(),
                                        txtEmail.Text.Trim(), Convert.ToInt32(ddrTimesheetApprove.SelectedValue), Convert.ToInt32(timesheetsecondapprover.SelectedValue),
                                        GetCompanyName(), txtContactNumber.Text.Trim(), txtReleaseDate.Text.Trim(), txtStartDate.Text.Trim(),
                                        Convert.ToInt32(ddlExperienceClassification.SelectedValue), int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlArea.SelectedValue), int.Parse(ddlCasual_Labour.SelectedValue),chkReset.Checked);
                            //ContractorDetail Update
                            ContractorDetail contractorDetail = new ContractorDetail();
                            contractorDetail.UserID = Convert.ToInt32(Request.QueryString["uid"]);
                            if(rbGender.SelectedValue!=string.Empty)
                            contractorDetail.Gender = (rbGender.SelectedValue == "0" ? false : true);
                            contractorDetail.DOB = Convert.ToDateTime(string.IsNullOrEmpty(txtDOB.Text)?"01/01/1900":txtDOB.Text);
                            contractorDetail.City = txtCity.Text;
                            contractorDetail.ShowFinancialCosts = (chkFinance.Checked == true ? true : false);
                            contractorDetail.Country = int.Parse(ddlCountry1.SelectedValue);
                            contractorDetail.DefaultCustomerSite = Convert.ToInt32(ddlSite.SelectedValue);
                            ContractorDetailsUpdate(contractorDetail);

                            if (drStatus.SelectedItem.Value == "InActive")
                            {
                                if (drpermission.SelectedValue == "7")
                                {
                                    Response.Redirect("~/WF/Admin/AdminUsers.aspx?sid=7");
                                }
                                else
                                {
                                    Response.Redirect("~/WF/Admin/AdminUsers.aspx");
                                }
                            }
                                
                            drContractors.SelectedValue = drContractors.SelectedValue;
                        //}
                        //else
                        //{
                        //    RequiredFieldValidator1.ErrorMessage = "";
                        //    lblError1.Text = "Please enter Company name";
                        //}
                    }
                    else
                    {
                        RequiredFieldValidator1.ErrorMessage = "";
                        lblError1.Text = "Please enter Full Name";
                    }

                }
            }
           
            if (Request.QueryString["sid"] != null)
            {
                if (Request.QueryString["sid"] == "10")
                {
                    drpermission.SelectedValue = "10";
                    drpermission.Enabled = false;
                    GetID.Visible = true;
                    txtEmail.Visible = false;
                    txtCompany.Visible = false;
                    GetUser.Visible = false;
                    //ddlCasual_Labour.SelectedValue = "3";
                    BindUsers(int.Parse(drpermission.SelectedValue));
                }
                if (Request.QueryString["sid"] == "7")
                {
                    drpermission.SelectedValue = "7";
                    drpermission.Enabled = false;
                    BindUsers(int.Parse(drpermission.SelectedValue));
                }
            }
                 }
            else{
                lblError1.Text = "Email already exists";
            }
           
        }
        catch (Exception ex)
        {
            RequiredFieldValidator2.ErrorMessage = "";
            LogExceptions.LogException(ex.Message);
        }

    }
    #region Contractors Details Select, Insert,Update 
    //Newly added 

    private void SelectContractorDetails(int userId)
    {
        using (UserDataContext db = new UserDataContext())
        {
            ContractorDetail cd = db.ContractorDetails.Where(c => c.UserID == userId).Select(c => c).FirstOrDefault();
            if (cd != null)
            {
                if(cd.Gender!=null)
                rbGender.SelectedValue = (cd.Gender == false ? "0" : "1");
                txtDOB.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.DOB) == "01/01/1900" ? string.Empty : cd.DOB.Value.ToString(Deffinity.systemdefaults.GetDateformat());
                txtCity.Text = cd.City;
                chkFinance.Checked = (cd.ShowFinancialCosts == null ? false : Convert.ToBoolean(cd.ShowFinancialCosts));
                ccdSite.SelectedValue = cd.DefaultCustomerSite.HasValue ? cd.DefaultCustomerSite.ToString() : "0";
                ddlCountry1.SelectedValue = cd.Country.ToString();
            }
        }
    }
    private void ContractorDetailsInsert(ContractorDetail contractorDetail)
    {

        using (UserDataContext db = new UserDataContext())
        {
            db.ContractorDetails.InsertOnSubmit(contractorDetail);
            db.SubmitChanges();
        }
    }

    private void ContractorDetailsUpdate(ContractorDetail contractorDetail)
    {
        using (UserDataContext db = new UserDataContext())
        {
            ContractorDetail cd = db.ContractorDetails.Where(c => c.UserID == contractorDetail.UserID).Select(c => c).FirstOrDefault();
            //update
            if (cd != null)
            {
                cd.Gender = contractorDetail.Gender;
                cd.City = contractorDetail.City;
                cd.Country = contractorDetail.Country;
                cd.DOB = contractorDetail.DOB;
                cd.ShowFinancialCosts = contractorDetail.ShowFinancialCosts;
                cd.DefaultCustomerSite = contractorDetail.DefaultCustomerSite;
                db.SubmitChanges();
            }
            //Insert
            else
            {
                db.ContractorDetails.InsertOnSubmit(contractorDetail);
                db.SubmitChanges();
            }
        }
    }

    #endregion

    #region Usert insert update
    private int UserInsert(string fullname,string loginname,string password,int permissionlevel,string status,string details,
        string email, int timesheet_primary_approver, int timesheet_secondary_approver,string company,string contactnumber,
        string ReleaseDate, string EmploymentStartDate, int ExpClassification, int DepartmentID, int AreaID, int CasualLabourType, bool resetpassword)
    {
        DbCommand cmd = db.GetStoredProcCommand("DN_InsertUser");
        db.AddInParameter(cmd, "@ContractorName", DbType.String, fullname);
        db.AddInParameter(cmd, "@LoginName", DbType.String, loginname);
        db.AddInParameter(cmd, "@Password", DbType.String, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1"));
        db.AddInParameter(cmd, "@SID", DbType.Int32, permissionlevel);
        db.AddInParameter(cmd, "@Status", DbType.String, status);
        db.AddInParameter(cmd, "@Details", DbType.String, details);
        db.AddInParameter(cmd, "@EmailAddress", DbType.String, email);
        db.AddInParameter(cmd, "@TimeApproveID", DbType.Int32, timesheet_primary_approver);
        db.AddInParameter(cmd, "@SecondTSApprover", DbType.Double, timesheet_secondary_approver);
        db.AddInParameter(cmd, "@Company", DbType.String, company);
        db.AddInParameter(cmd, "@ContactNumber", DbType.String, contactnumber);
        db.AddInParameter(cmd, "@ReleaseDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(ReleaseDate) ? "01/01/1990" : ReleaseDate));
        db.AddInParameter(cmd, "@EmploymentStartDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(EmploymentStartDate) ? "01/01/1990" : EmploymentStartDate));
        db.AddInParameter(cmd, "@ExpClassification", DbType.Int32, ExpClassification);
        db.AddInParameter(cmd, "@DepartmentID", DbType.Int32, DepartmentID);
        db.AddInParameter(cmd, "@AreaID", DbType.Int32, AreaID);
        db.AddInParameter(cmd, "@CasualLabourType", DbType.Int32, CasualLabourType);
        db.AddInParameter(cmd, "@ResetPassword", DbType.Boolean, resetpassword);

        db.AddOutParameter(cmd, "@OutStatus", DbType.Int32, 4);
        db.AddOutParameter(cmd, "@OutID", DbType.Int32, 4);
        db.AddInParameter(cmd, "@CompanyID", DbType.Int32, sessionKeys.PortfolioID);
        db.ExecuteNonQuery(cmd);
        //if getVal = 1 sucess 2 for already item exist
        int getVal = (int)db.GetParameterValue(cmd, "OutStatus");
        int OutID = (int)db.GetParameterValue(cmd, "OutID");
        cmd.Dispose();
        if (getVal == 1)
        {
           

            if (OutID > 0)
            {
                // update the userdetails
                UserDetails_update(OutID, chk_disable_customerportal.Checked);
                //set values
                SelectUserData(OutID);
                //set userdetails
                Set_UserDetails(OutID);

                AddUsertoCompany(OutID);
            }
            //change visibility
            //clearNamefromName();
            //defaultBindings();
            lblMsg1.Text = "User added successfully";
            
            Response.Redirect(string.Format("~/WF/Admin//AdminUsers.aspx?uid={0}", OutID));
            //lblError1.ForeColor = System.Drawing.Color.Green;
            //ChangeUpdateVisible();
            emailblank.ErrorMessage = string.Empty;
        }
        else if (getVal == 2)
        {
            lblError1.Text = "Sorry but the username is already in use by another user.";
        }

        RequiredFieldValidator2.ErrorMessage = "";
        return OutID;
    }
    private void UserUpdate(int ID,string fullname, string loginname, string password, int permissionlevel, string status, string details,
        string email, int timesheet_primary_approver, int timesheet_secondary_approver, string company, string contactnumber,
        string ReleaseDate, string EmploymentStartDate, int ExpClassification, int DepartmentID, int AreaID, int CasualLabourType, bool resetpassword)
    {

        int s_val = Convert.ToInt32(drContractors.SelectedValue);
        //update data
        DbCommand cmd = db.GetStoredProcCommand("DN_UpdateUser");
        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        db.AddInParameter(cmd, "@ContractorName", DbType.String, fullname);
        db.AddInParameter(cmd, "@LoginName", DbType.String, loginname);
        if (txtPassword.Text.Trim() == "")
        {
            db.AddInParameter(cmd, "@Password", DbType.String, password);
        }
        else
        {
            db.AddInParameter(cmd, "@Password", DbType.String, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1"));
        }
        db.AddInParameter(cmd, "@SID", DbType.Int32, permissionlevel);
        db.AddInParameter(cmd, "@Status", DbType.String, status);
        db.AddInParameter(cmd, "@Details", DbType.String, details);
        db.AddInParameter(cmd, "@EmailAddress", DbType.String, email);
        db.AddInParameter(cmd, "@TimeApproveID", DbType.Int32, timesheet_primary_approver);
        db.AddInParameter(cmd, "@SecondTSApprover", DbType.Double, timesheet_secondary_approver);
        db.AddInParameter(cmd, "@Company", DbType.String, company);
        db.AddInParameter(cmd, "@ContactNumber", DbType.String, contactnumber);
        db.AddInParameter(cmd, "@ReleaseDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(ReleaseDate) ? "01/01/1990" : ReleaseDate));
        db.AddInParameter(cmd, "@EmploymentStartDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(EmploymentStartDate) ? "01/01/1990" : EmploymentStartDate));
        db.AddInParameter(cmd, "@ExpClassification", DbType.Int32, ExpClassification);
        db.AddInParameter(cmd, "@CasualLabourType", DbType.Int32, CasualLabourType);
        db.AddInParameter(cmd, "@DepartmentID", DbType.Int32, DepartmentID);
        db.AddInParameter(cmd, "@AreaID", DbType.Int32, AreaID);
        db.AddInParameter(cmd, "@ResetPassword", DbType.Boolean, resetpassword);
        db.AddOutParameter(cmd, "@OutStatus", DbType.Int32, 4);
        db.AddInParameter(cmd, "@CompanyID", DbType.Int32, sessionKeys.PortfolioID);
        db.ExecuteNonQuery(cmd);
        //if getVal = 1 sucess 2 for already item exist
        int getVal = (int)db.GetParameterValue(cmd, "OutStatus");
        cmd.Dispose();
        if (getVal == 1)
        {
            // update the userdetails
            UserDetails_update(ID, chk_disable_customerportal.Checked);

            lblMsg1.Text = "User details updated successfully";
            //lblError1.ForeColor = System.Drawing.Color.Green;
            ChangeUpdateVisible();
            emailblank.ErrorMessage = string.Empty;
        }
        else if (getVal == 2)
        {
            lblError1.Text = "Sorry but the username is already in use by another user.";
        }

        RequiredFieldValidator1.ErrorMessage = "";

        //reload dropdown and select existing data
        //getData.DdlBindSelect(drContractors, "DN_ResourcesList", "ID", "ContractorName", true, true);
        //drContractors.SelectedItem.Text = txtEditName.Text;
        //BindUsers(int.Parse(drpermission.SelectedValue));
        if (Request.QueryString["sid"] != null)
        {
            if (Request.QueryString["sid"] == "10")
            {
                drpermission.SelectedValue = "10";
                drpermission.Enabled = false;
                GetID.Visible = true;
                txtEmail.Visible = false;
                txtCompany.Visible = false;
                GetUser.Visible = false;
                //ddlCasual_Labour.SelectedValue = "3";
                BindUsers(int.Parse(drpermission.SelectedValue));
            }
            else if (Request.QueryString["sid"] == "7")
            {
                drpermission.SelectedValue = "7";
                drpermission.Enabled = false;
                BindUsers(int.Parse(drpermission.SelectedValue));
            }
            else
            {
                getData.DdlBindSelect(drContractors, "select * from Contractors where Status='ACTIVE' and sid not in (-99,7,10,8) and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false,true);
            }
        }
        SelectUserData(s_val);
     
        //set userdetails
        Set_UserDetails(ID);
      
        //edit panel
        PanleEditName.Visible = false;
        

    }
    #endregion
    #region model popups
    private void BindDepartment()
    {
        //ddlDepartment.DataSource = Department.Department_SelectAll();
        //ddlDepartment.DataValueField = "ID";
        //ddlDepartment.DataTextField = "Name";
        //ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    protected void btnModelDepartmentInsert_Click(object sender, EventArgs e)
    {
        try
        {
            //DepartmentEntity de = new DepartmentEntity();
            //if (int.Parse(H_Department.Value) > 0)
            //{
            //    de.ID = int.Parse(H_Department.Value);
            //    de.Name = txtModelDepartment.Text.Trim();
            //    //reset the value
            //    H_Department.Value = "0";
            //}
            //else
            //{
            //    de.ID = 0;
            //    de.Name = txtModelDepartment.Text.Trim();
            //}
            //Department.Department_InsertUpdate(de);

            txtModelDepartment.Text = string.Empty;
            //Bind category dropdown
            BindDepartment();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindArea()
    {
        //ddlArea.DataSource = Area.Area_SelectByDepartment(int.Parse(ddlDepartment.SelectedValue));
        //ddlArea.DataValueField = "ID";
        //ddlArea.DataTextField = "Name";
        //ddlArea.DataBind();
        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    #region "ModelPopup  Area"
    protected void imgAreaSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //AreaEntity ae = new AreaEntity();
            //if (int.Parse(H_Area.Value) > 0)
            //{
            //    ae.ID = int.Parse(H_Area.Value);
            //    ae.Name = txtArea.Text.Trim();
            //    ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
            //}
            //else
            //{
            //    ae.ID = 0;
            //    ae.Name = txtArea.Text.Trim();
            //    ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
            //}
            //Area.Area_InsertUpdate(ae);
            //txtArea.Text = string.Empty;

            BindArea();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion "ModelPopUp Area"
    #endregion
    private bool checkUsername()
    {
        bool retVal = false;
        if (txtUser.Visible == true)
        {
            retVal = true;
        }
       

        //check if the user is casual labour or not
        if (drpermission.SelectedValue == "10")
        {
            retVal = true;
        }

        return retVal;

    }
    private double getDouble(string st)
    {
        double t = 0;
        try
        {
            if (st != "")
            {
                t = Convert.ToDouble(st);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return t;
    }
    protected void btn_Managecontacts_Click(object sender, EventArgs e)
    {

        if (drContractors.SelectedValue == "0")
        {
            lblError1.Text = "Please select full name";
        }
        else
        {
            //Response.Redirect("AdminCContacts.aspx?CID=" + drContractors.SelectedValue.ToString() + "&ContractorName=" + drContractors.SelectedItem.Text.ToString());
        }
    }
    public void fillgrid()
    {
        lblError1.Text = "";
        if (drContractors.SelectedValue == "0")
        {
            lblError1.Text = "Please select resource";
            txtPassword.Text = "";
            txtusername.Text = "";
            txtDetails.Text = "";
            txtUser.Text = "";
        }
        
    }

    protected void btnListUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Vendors/RFIVendors.aspx");
    }

    protected void btn_Managecontacts1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/ProgrammeManagement.aspx?type=cid");
    }
    private string GetCompanyName()
    {
        string retVal = "";
        if (ddlCompany.Visible == true)
        {
            retVal = ddlCompany.SelectedValue;
        }
        else if (txtCompany.Visible == true)
        {
            retVal = txtCompany.Text.Trim();
        }
        return retVal;
    }
    private bool CheckPassword()
    {
        bool retVal = false;

        if(!string.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            retVal = true;
        }
        // check to avoid casual labour checking
        if (drpermission.SelectedValue == "10")
        {
            retVal = true;
        }


        return retVal;
    }
    private bool CheckConfirmPassword()
    {
        bool retVal = false;

        if (!string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
        {
            retVal = true;
        }
        // check to avoid casual labour checking
        if (drpermission.SelectedValue == "10")
        {
            retVal = true;
        }


        return retVal;
    }

    private bool ValidCompanyName()
    {
        bool retVal = false;
        if (ddlCompany.Visible == true)
        {
            if ((ddlCompany.Items.Count > 1))
            {
                retVal = true;
            }
            if (ddlCompany.SelectedValue != "Please select...")
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
        }
        else
            if (txtCompany.Visible == true)
            {
                if (txtCompany.Text.Trim() != "")
                {
                    retVal = true;
                }
            }

        //check if the user is casual labour or not
        if (drpermission.SelectedValue == "10")
        {
            retVal = true;
        }

        return retVal;
    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        chageAddCompanyVisibility();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

            GetID.Visible = false;
            txtEmail.Visible = true;
            txtCompany.Visible = false;
            GetUser.Visible = true;
            if (Request.QueryString["sid"] == "10")
            {
                GetCustomer.Visible = false;
            }
            else if (Request.QueryString["sid"] == "7")
            {
                GetCustomer.Visible = false;
            }
            else
            {
                GetCustomer.Visible = true;
            }
            //GetCustomer.Visible = false;
           

            chageAddCompanyVisibility();
            clearFields();
            PanleEditName.Visible = false;
            Response.Redirect(string.Format("~/WF/Admin/AdminUsers.aspx"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    private void chageAddCompanyVisibility()
    {
        txtCompany.Text = "";
        txtCompany.Visible = true;
        ddlCompany.Visible = false;
        btnAddCompany.Visible = false;


    }
    
   

   
    #region UserTabs
    public void usertabsvisibility(bool userbuttons, bool usernametab)
    {
        if (drContractors.SelectedValue != "0")
        {
            pnluserbuttons.Visible = userbuttons;
            pnlusername.Visible = usernametab;
            getUserId.Value = drContractors.SelectedIndex.ToString();
        }
        else
        {
            pnluserbuttons.Visible = true;
            pnlusername.Visible = false;
            getUserId.Value = drContractors.SelectedIndex.ToString();
        }

    }
    #endregion



    
   
    //protected void btngohome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Admin.aspx?tab=3");
    //}
    protected void drpermission_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpermission.SelectedValue == "10")
        {
            GetID.Visible = true;
            txtEmail.Visible = false;
            txtCompany.Visible = false;
            GetUser.Visible = false;
            //ddlCasual_Labour.SelectedValue = "3";
            BindUsers(int.Parse(drpermission.SelectedValue));
        }
        else if(drpermission.SelectedValue == "7")
         {
             BindUsers(int.Parse(drpermission.SelectedValue));
        }
        else 
        {

            GetID.Visible = false;
            txtEmail.Visible = true;
            txtCompany.Visible = true;
            GetUser.Visible = true;
            txtCompany.Visible = false;
            //ddlCasual_Labour.SelectedValue = "3";
            //defaultBindings();
        }

    }
    protected void btn_Casuallobour_Click(object sender, EventArgs e)
    {
        string s = "";
        //check to redirect from portfolio or admin section

        s = "Adminusers";

        Response.Redirect(string.Format("~/WF/Admin/AdminDropDown.aspx?type={0}", s));
    }

    protected void btnImgUpload_Click(object sender, EventArgs e)
    {
        if (drContractors.SelectedValue.ToLower() != "0")
        {

            if (Fileupload_user.HasFile)
            {
                string fname = "user_" + drContractors.SelectedValue;
                ImageManager.SaveImage_setpath(fname, Fileupload_user.FileBytes, "Users");

                //display image
                DisplayImage(Convert.ToInt32(drContractors.SelectedValue));
            }
        }
    }
   
    protected void imgBtnArea_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlDepartment.SelectedValue) > 0)
        {
            mdlArea.Show();
        }
        else
        {
            lblError1.Text = "Please select Department";
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindArea();
    }
    #region Change resposebilities
    protected void btnChange_response_Click(object sender, EventArgs e)
    {
        try
        {
            int uid = 0;
            if (Request.QueryString["uid"] != null)
                uid = int.Parse(Request.QueryString["uid"].ToString());
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Contractors_ChangeResponsibility", new SqlParameter("@currentuserid", uid), new SqlParameter("@updatebyuserid", int.Parse(ddlUser_respos.SelectedValue)));
            //lblError1.ForeColor = System.Drawing.Color.Green;
            //lblError1.Text = "";
            Response.Redirect(string.Format("~/WF/Admin/AdminUsers.aspx?uid={0}", uid));
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private void bind_ddluserrespose()
    {
        UserDataContext um = new UserDataContext();
        ddlUser_respos.DataSource = from p in um.Contractors
                                    where p.Status.ToUpper() == "ACTIVE" && p.SID!=8
                                    orderby p.ContractorName
                                    select new { p.ID, p.ContractorName };
        ddlUser_respos.DataTextField = "ContractorName";
        ddlUser_respos.DataValueField = "ID";
        ddlUser_respos.DataBind();
        ddlUser_respos.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    #endregion
    #region UserDetails
    private void UserDetails_update(int userid, bool diable_customerportalaccess)
    {
        using (UserDataContext um = new UserDataContext())
        {
            UserDetail ud = (from p in um.UserDetails
                             where p.UserId == userid
                             select p).FirstOrDefault();

            if (ud != null)
            {
                ud.Disable_CustomerPortalAccess = chk_disable_customerportal.Checked;
                ud.PostCode = txtPostcode.Text.Trim();
                ud.ExpertiseTypeID = GetExpertValues();// Convert.ToInt32(ddlExpert.SelectedValue);
                
            }
            else
            {
                ud = new UserDetail();
                ud.UserId = userid;
                ud.Disable_CustomerPortalAccess = chk_disable_customerportal.Checked;
                ud.PostCode = txtPostcode.Text.Trim();
                ud.ExpertiseTypeID = GetExpertValues();// Convert.ToInt32(ddlExpert.SelectedValue);
                um.UserDetails.InsertOnSubmit(ud);
            }

            um.SubmitChanges();
        }
    }
    private void Set_UserDetails(int userid)
    {
        using (UserDataContext um = new UserDataContext())
        {
            UserDetail ud = (from p in um.UserDetails
                             where p.UserId == userid
                             select p).FirstOrDefault();
            if (ud != null)
            {
                chk_disable_customerportal.Checked = ud.Disable_CustomerPortalAccess.HasValue ? ud.Disable_CustomerPortalAccess.Value : false;
                txtPostcode.Text = ud.PostCode;
                SetExpertValues((ud.ExpertiseTypeID != null ? ud.ExpertiseTypeID : string.Empty).ToString());
                //ccdExperts.SelectedValue = (ud.ExpertiseTypeID.HasValue ? ud.ExpertiseTypeID.Value : 0).ToString();
            }
        }
    }
    #endregion


    private void BindExpertdata()
    {
        try
        {
            IAssetRespository<AssetsMgr.Entity.Assetstype> atypeRepository = new AssetRespository<AssetsMgr.Entity.Assetstype>();
            var typeList = atypeRepository.GetAll().OrderBy(o => o.Type).ToList();
            chkExpert.DataSource = typeList;
            chkExpert.DataValueField = "TypeID";
            chkExpert.DataTextField = "Type";
            chkExpert.DataBind();
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void SetExpertValues(string typeIDs)
    {
        if (typeIDs.Length > 0)
        {
            var ids = typeIDs.Split(',').ToArray();


            foreach (var s in ids)
            {
                foreach (ListItem litem in chkExpert.Items)
                {
                    if (litem.Value == s)
                        litem.Selected = true;
                    //else
                    //    litem.Selected = false;
                }
            }
        }
    }
    private string GetExpertValues()
    {
        string typeIDs = string.Empty;
        foreach (ListItem litem in chkExpert.Items)
        {
            if (litem.Selected == true)
                typeIDs = typeIDs + litem.Value + ",";
        }
        return typeIDs;
    }

    private void AddUsertoCompany(int userID)
    {
        try
        {
            if (sessionKeys.PortfolioID >0)
            {
                IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var uEntity = uRep.GetAll().Where(o => o.UserID == userID && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                if (uEntity == null)
                {
                    uEntity = new UserMgt.Entity.UserToCompany();
                    uEntity.CompanyID = sessionKeys.PortfolioID;
                    uEntity.UserID = userID;
                    uRep.Add(uEntity);
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private bool CheckEmailNotExists(string EmailAddress ,int userid=0)
    {
        bool retval = false;
        var cRep = new UserRepository<UserMgt.Entity.v_contractor>();
        if (userid == 0)
        {
            if (cRep.GetAll().Where(o => o.EmailAddress.ToLower() == EmailAddress.ToLower() && o.Status.ToLower() == "active" && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
            {
                retval = true;
            }
        }
        else
        {
             if (cRep.GetAll().Where(o => o.EmailAddress.ToLower() == EmailAddress.ToLower() && o.Status.ToLower() == "active" && o.CompanyID == sessionKeys.PortfolioID && o.ID != userid).Count() == 0)
            {
                retval = true;
            }
        }

        return retval;
    }
}
