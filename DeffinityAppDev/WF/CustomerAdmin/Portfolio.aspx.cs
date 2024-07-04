using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using System.IO;
using Deffinity.CostCenterManager;
using Deffinity.PortfolioManager;
using UserMgt.DAL;
using System.Linq;
using ProjectMgt.BAL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using DeffinityManager.DC.BLL;

public partial class admin_Portfolio : System.Web.UI.Page
{

    #region Fields

    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string cs = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    private string er = string.Empty;
    DisBindings getdata = new DisBindings();

    #endregion

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

        lblError.Text = string.Empty;
        lblError1.Text = string.Empty;
        Label1.Visible = false;
        //Page Header
        //Master.PageHead = "Customer Admin";
        if (Request.QueryString["type"] != null)
        {
            ImageButton1.Visible = true;
        }
        if (Request.QueryString["Project"] != null)
        {
            ImageButton1.Visible = true;
        }
        //display title based on tabs
        if (Request.QueryString["tab"] != null)
        {
            DisplayTitle(int.Parse(Request.QueryString["tab"].ToString()));
        }
        if (!IsPostBack)
        {
            try
            {
                BindSalesExecutiveddl();
                BindData();
                //filldlportfoliotype();
                bindgrid();
                bindAssignedSites();
                ddlPortfolio.SelectedIndex = ddlPortfolio.Items.IndexOf(ddlPortfolio.Items.FindByValue(sessionKeys.PortfolioID.ToString()));
                if (sessionKeys.PortfolioID > 0)
                {
                    selectPortFolio();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        tabVisibility();
        PnlCountry.Visible = false;
        Pnlcity.Visible = false;
        Pnlsite.Visible = false;
        lbltest.Text = "";
    }
    public void BindSalesExecutiveddl()
    {
        using (UserDataContext Udc = new UserDataContext())
        {
            ddlAssignedSalesExecutive.DataSource = (from a in Udc.Contractors
                                                    where (a.SID == 1 || a.SID == 2 || a.SID == 3) && a.Status.ToLower() == "active"
                                                    select new
                                                    {
                                                        id = a.ID,
                                                        Name = a.ContractorName
                                                    }).OrderBy(a => a.Name).ToList();
            ddlAssignedSalesExecutive.DataValueField = "id";
            ddlAssignedSalesExecutive.DataTextField = "Name";
            ddlAssignedSalesExecutive.DataBind();
            ddlAssignedSalesExecutive.Items.Insert(0, new ListItem("Please select...", "0"));
            
        }
    }
    private void DisplayTitle(int tabID)
    {
        //if (tabID == 1)
        //    lblPageTitle.InnerText = "Customer Details";
        //else if (tabID == 2)
        //    lblPageTitle.InnerText = "Customer Association";
        //else if (tabID == 3)
        //    lblPageTitle.InnerText = "Customer Locations";

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (ddlPortfolio.SelectedValue == "Please select...")
            sessionKeys.PortfolioName = string.Empty;
    }

    #endregion

    #region Control Events

    protected void btnNew_Click(object sender, EventArgs e)
    {

        //divddl.Visible = false;
        ddlPortfolio.Visible = false;
        //divtxt.Visible = true;
        txtPortfolio.Visible = true;
        btnCancel.Visible = true;
        btnNew.Visible = false;
        BindSalesExecutiveddl();
        clear_fields();
        setDefaults();

       
        DiVCustomerName.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        lblError1.Text = string.Empty;
        //divddl.Visible = true;
        ddlPortfolio.Visible = true;
        //divtxt.Visible = false;
        txtPortfolio.Visible = false;
        btnCancel.Visible = false;
        btnNew.Visible = true;
        clear_fields();

        DiVCustomerName.Visible = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            //if (checkimages())
            //{
                if (txtPortfolio.Visible == true)
                {
                    if(!string.IsNullOrEmpty(txtPortfolio.Text.Trim()))
                    {
                    insertPortfolio();
                    //filldlportfoliotype();
                    //setDefaults();
                    //clear_fields();
                    //Response.Redirect("~/WF/Admin/ProgrammePermission.aspx");
                    }
                    else
                    {lblError.Text = "Please enter Customer name";}
                }
               
                else
                {
                    updatePortfolio();
                }
            //}
            //else
            //{
            //    lblError.Text = "File size can not exceed 15kb and image height width with in 200px and height with in 100px";
            //}
            //RegularExpressionValidator1.ErrorMessage = "";
            selectPortFolio();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public Boolean  checkimages()
    {
        bool condisition = true;
        try
        {
            if (FileUpload1.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload1.PostedFile.InputStream);
                if (upBmp.Height <= 100 && upBmp.Width <= 200 && FileUpload1.PostedFile.ContentLength <= 15360)
                {
                    condisition = true;
                }
                else
                {
                    condisition = false;
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        return condisition;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //check this portfolio is exist
            int OutVal;
            Portfilio.InsertAssociatedCustomers(sessionKeys.PortfolioID, int.Parse(ddlCustomer.SelectedValue), out OutVal);
            if (OutVal == 1)
            {
                Label1.Visible = true;
                Label1.Text = "The user has already been assigned to another customer";
                //This customer already assigned to another portfolio
            }
            else if (OutVal == 2)
            {
                Label1.Visible = true;
                Label1.Text = "The user has already been assigned to a customer portal";
            }
            else if (OutVal == 3)
            {
                //bind grid
                bindgrid();
                Label1.Visible = true;
                Label1.Text = "Added successfully.";
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


        //if (check() == true)
        //{
        //    SqlCommand comm = new SqlCommand("insert into AssignedCustomerToPortfolio(Portfolio,CustomerID) values(" + sessionKeys.PortfolioID.ToString() + "," + ddlCustomer.SelectedItem.Value + ")", conn);
        //    conn.Open();
        //    try
        //    {
        //        int i = comm.ExecuteNonQuery();
        //        if (i > 0)
        //        {
        //            ddlCustomer.SelectedIndex = 0;
        //            bindgrid();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        //else
        //{
        //    Label1.Visible = true;
        //    Label1.Text = "This customer already assigned to a portfolio";

        //}
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {

        if (QueryStringValues.Type == "projectplan")
        {
            Response.Redirect(string.Format("~/WF/ProjectPlan/ProjectPlan.aspx?projectplanid={0}",QueryStringValues.ProjectPlanID.ToString()));        
        }
        else if (QueryStringValues.Type == "project")
        {
            Response.Redirect("~/WF/Projects/ProjectOverview.aspx?type=project");
        }
        else if (QueryStringValues.Type == "assets")
        {
            Response.Redirect("~/WF/CustomerAdmin/AssetsAdmin.aspx");
        
        }
        
        //project reference
        if (QueryStringValues.Project > 0)
        {
            if (QueryStringValues.Type == "projectasset")
            {
                Response.Redirect(string.Format("~/WF/Projects/ProjectAsset.aspx?project={0}", QueryStringValues.Project));

            }
            else if (QueryStringValues.Type == "checkpointasset")
            {

                Response.Redirect(string.Format("~/WF/Projects/Checkpoints/Checkpoint_Assets.aspx?project={0}", QueryStringValues.Project));
            }
            else
            {
                Response.Redirect(string.Format("~/WF/Projects/ProjectOverview.aspx?project={0}", QueryStringValues.Project));
            }
        }

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnSiteSave_Click(object sender, EventArgs e)
    {
        //clearfields();
        if (check1() == true)
        {
            SqlCommand comm = new SqlCommand("insert into AssignedSitesToPortfolio(Portfolio,SIteID,CityID,CountryID) values(" + sessionKeys.PortfolioID.ToString() + "," + ddlSite.SelectedItem.Value + "," + ddlCity.SelectedItem.Value + "," + ddlCountry.SelectedItem.Value + ")", conn);
            conn.Open();
            try
            {
                int i = comm.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    ddlSite.SelectedIndex = 0;
                    bindAssignedSites();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "This site already assigned to the portfolio";
        }
    }
    public void clearfields()
    {
        
        ddlCountry.Visible = true; 
        ddlCity.Visible = true;
        ddlSite.Visible = true;
        btnCountry.Visible = true;
        btnCity.Visible = true;
        btnSite.Visible = true;
        bindcountry();
        getdata.DdlBindSelect(ddlCity, string.Format("Select ID,City from City where ID=0"), "ID", "City", false, true);
        //getdata.DdlBindSelect(ddlSite, string.Format("Select ID,Site from Site where CityID={0} AND Visible='N'", 0), "ID", "Site", false, true);
        
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                SqlCommand comm_del = new SqlCommand("delete from AssignedCustomerToPortfolio where ID=" + ID, conn);
                conn.Open();
                int c1 = comm_del.ExecuteNonQuery();
                conn.Close();
                if (c1 > 0)
                {
                    bindgrid();
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                SqlCommand comm_del = new SqlCommand("delete from AssignedSitesToPortfolio where ID=" + ID, conn);
                conn.Open();
                int c1 = comm_del.ExecuteNonQuery();
                conn.Close();
                if (c1 > 0)
                {
                    bindAssignedSites();
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPortfolio.SelectedValue != "Please select...")
        {
            sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
           imgLogo.Visible = true;
            selectPortFolio();
            DiVCustomerName.Visible = true;
        }
        else
        {
            imgLogo.Visible = false ;
            clear_fields();
            DiVCustomerName.Visible = false;
        }
    }

    #endregion

    #region Helper Methods

    private void tabVisibility()
    {
        int tabID = 1;
        if (Request.QueryString["tab"] != null)
        {
            tabID = Convert.ToInt32(Request.QueryString["tab"]);
            switch (tabID)
            {
                case 2:
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    break;
                case 3:
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    break;
                case 1:
                default:
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    break;
            }
        }
        else
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
    }

    private void BindData()
    {
        getdata.DdlBindSelect(ddlOwner, "select ID,ContractorName from Contractors where Status='ACTIVE' and SID in (1,2,3) order by ContractorName", "ID", "ContractorName", false, true);
        //getdata.DdlBindSelect(ddlStatus, "select ID,Status from PortfolioStatus", "ID", "Status", false, true);
        getdata.DdlBindSelect(ddlPortfolio, "DN_SelectPortfolio", "ID", "PortFolio", true, true);
        //getdata.DdlBindSelect(dlportfoliodirection, "select ID,ContractorName from Contractors where Status='ACTIVE'", "ID", "ContractorName", false, true);
       // getdata.DdlBindSelect(ddlCustomer, "select ID,ContractorName from contractors where SID=7 and lower(Status)='active'  order by ContractorName", "ID", "ContractorName", false, true, true);
        getdata.DdlBindSelect(ddlCountry, "DN_SelectCountry", "ID", "Country", true,false,true);
        Bindcity();
        BindSite();
    }

    //private void filldlportfoliotype()
    //{
    //    SqlConnection conn = new SqlConnection(cs);
    //    try
    //    {
    //        string selectsql = "select ID,Portfoliotype from PortfolioType order by Portfoliotype";
    //        SqlCommand cmd = new SqlCommand(selectsql, conn);
    //        conn.Open();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        dlportfoliotype.DataSource = dt;
    //        dlportfoliotype.DataTextField = "Portfoliotype";
    //        dlportfoliotype.DataValueField = "ID";
    //        dlportfoliotype.DataBind();
    //        dlportfoliotype.Items.Insert(0, new ListItem("Please select...", "0"));
    //        dt.Clear();

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //}
#region insert & update & select portfolio
    //clear the cache when insert or update is happend
    private void ClearPortfolioCache()
    {
        BaseCache.Cache_Remove(CacheNames.DefaultNames.Portfolio.ToString());
    }
    private void insertPortfolio()
    {
        SqlConnection cn = new SqlConnection(cs);
        try
        {

            using (SqlCommand cmd = new SqlCommand("DN_InsertPortfolio", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PortFolio", txtPortfolio.Text.Trim());
                cmd.Parameters.AddWithValue("Description", string.Empty);
                cmd.Parameters.AddWithValue("Owner", ddlOwner.SelectedValue);
                cmd.Parameters.AddWithValue("MaxBudget", "0");
                cmd.Parameters.AddWithValue("RisksandIssues", string.Empty);
                cmd.Parameters.AddWithValue("ResourcesRequired", string.Empty);
                cmd.Parameters.AddWithValue("PortfolioTypeID", "0");
                cmd.Parameters.AddWithValue("Visible", chkVisible.Checked);
                cmd.Parameters.AddWithValue("KeyContactName", string.Empty);
                cmd.Parameters.AddWithValue("EmailAddress", string.Empty);
                cmd.Parameters.AddWithValue("TelephoneNumber", string.Empty);
                cmd.Parameters.AddWithValue("OtherNumber", string.Empty);
                cmd.Parameters.AddWithValue("Address", string.Empty);
                cmd.Parameters.AddWithValue("docenable", chkAllowCustomers.Checked);
                cmd.Parameters.AddWithValue("AssignedSalesExecutive", ddlAssignedSalesExecutive.SelectedValue);

                SqlParameter Outval = new SqlParameter("@Outval", SqlDbType.Int, 4);
                Outval.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Outval);

                SqlParameter OutPortfolioID = new SqlParameter("@PortfolioID", SqlDbType.Int, 4);
                OutPortfolioID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OutPortfolioID);
               //int.Parse(Outval.ToString());
                int Outvalu = 0;
                int PortfolioID = 0;
                cn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Outvalu = (int)Outval.Value;
                    PortfolioID = (int)OutPortfolioID.Value;
                    sessionKeys.PortfolioID = (int)OutPortfolioID.Value;
                    sessionKeys.PortfolioName = txtPortfolio.Text.Trim();
                    //apply service desk default data
                    UpdateServiceDeskDefaultData(sessionKeys.PortfolioID.ToString());
                    //String fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    //String loc = Server.MapPath(Request.ApplicationPath)+"\\images" + "\\" + fn.Replace(fn, "Portfolio_" + PortfolioID + ".gif");
                    //FileUpload1.PostedFile.SaveAs(loc);
                    if (FileUpload1.PostedFile.FileName.Length > 0)
                    {
                        Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload1.PostedFile.InputStream);
                        ImageManager.SaveProtfolioImage_setpath(FileUpload1.FileBytes, sessionKeys.PortfolioID.ToString());
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                finally
                {
                    cn.Close();
                }
                //get output value and check weather exist or not
                if (Outvalu == 2)
                {
                     setDefaults();
                    //divddl.Visible = true;
                    ddlPortfolio.Visible = true;
                    //divtxt.Visible = false;
                    txtPortfolio.Visible = false;
                    btnCancel.Visible = false;
                    btnNew.Visible = true;
                    txtPortfolio.Text = string.Empty;
                   
                    //clear cache
                    ClearPortfolioCache();
                    //page bindings                
                    BindData();
                    
                    ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
                    selectPortFolio();
                }
                else
                {
                    lblError.Text = "The customer already exists";
                }
                cmd.Dispose();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            cn.Close();
        }
    }
    //Apply service desk changes
    private void UpdateServiceDeskDefaultData(string CustomerID)
    {
        try
        {
            DefaultConfigurationToAllCustomer D_Configuration = new DefaultConfigurationToAllCustomer();
            D_Configuration.DataBindToTables(CustomerID);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void updatePortfolio()
    {
        try
        {
            SqlConnection cn = new SqlConnection(cs);

            if (ddlPortfolio.SelectedValue != "Please select...")
            {
                if (TxtCustomerName.Text.Trim() != string.Empty)
                {
                    using (SqlCommand cmd = new SqlCommand("DN_PortfolioUpdate", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("ID", ddlPortfolio.SelectedValue);
                        cmd.Parameters.AddWithValue("Description", string.Empty);
                        cmd.Parameters.AddWithValue("Owner", ddlOwner.SelectedValue);
                        cmd.Parameters.AddWithValue("MaxBudget", "0");
                        cmd.Parameters.AddWithValue("RisksandIssues", string.Empty);
                        cmd.Parameters.AddWithValue("ResourcesRequired", string.Empty);
                        cmd.Parameters.AddWithValue("PortfolioTypeID", "0");
                        cmd.Parameters.AddWithValue("Visible", chkVisible.Checked);
                        cmd.Parameters.AddWithValue("KeyContactName", string.Empty);
                        cmd.Parameters.AddWithValue("EmailAddress", string.Empty);
                        cmd.Parameters.AddWithValue("TelephoneNumber", string.Empty);
                        cmd.Parameters.AddWithValue("OtherNumber", string.Empty);
                        cmd.Parameters.AddWithValue("Address", string.Empty);
                        cmd.Parameters.AddWithValue("docenable", chkAllowCustomers.Checked);
                        cmd.Parameters.AddWithValue("AssignedSalesExecutive", ddlAssignedSalesExecutive.SelectedValue);

                        cmd.Parameters.AddWithValue("PortFolio", TxtCustomerName.Text.Trim());
                        SqlParameter Outval = new SqlParameter("@Outval", SqlDbType.Int, 4);
                        Outval.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(Outval);
                        int Outvalu = 0;

                        cn.Open();
                        try
                        {

                            cmd.ExecuteNonQuery();
                            cn.Close();

                            Outvalu = (int)Outval.Value;



                            //clear cache
                            ClearPortfolioCache();

                            if (FileUpload1.PostedFile.FileName.Length > 0)
                            {
                                //String fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                                //String loc = Server.MapPath(Request.ApplicationPath) + "/images" + "/" + fn.Replace(fn, "Portfolio_" + ddlPortfolio.SelectedItem.Value + ".gif");
                                //if (File.Exists((Server.MapPath("images") + "/" + "Portfolio_" + ddlPortfolio.SelectedItem.Value + ".gif")))
                                //{
                                //    FileUpload1.PostedFile.SaveAs(loc);
                                //}
                                //else
                                //{
                                //    FileUpload1.PostedFile.SaveAs(loc);
                                //}
                                if (FileUpload1.PostedFile.FileName.Length > 0)
                                {
                                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload1.PostedFile.InputStream);
                                    ImageManager.SaveProtfolioImage_setpath(FileUpload1.FileBytes, ddlPortfolio.SelectedItem.Value);
                                }
                                //redirect to same page
                                //Response.Redirect(string.Format("~/Portfolio.aspx?tab=1&ran={0}",RandomNumber()),false);
                            }
                            if (Outvalu == 1)
                            {
                                lblError1.ForeColor = System.Drawing.Color.Red;
                                lblError1.Text = "The customer already exists";
                            }
                            else if (Outvalu == 0)
                            {
                                BindData();
                                ddlPortfolio.SelectedIndex = ddlPortfolio.Items.IndexOf(ddlPortfolio.Items.FindByValue(sessionKeys.PortfolioID.ToString()));
                                sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
                                selectPortFolio();
                                lblError1.ForeColor = System.Drawing.Color.Green;
                                lblError1.Text = "Updated successfully";
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        finally
                        {
                            if (cn.State == ConnectionState.Open)
                                cn.Close();
                        }
                        btnCancel.Visible = false;
                        btnNew.Visible = true;
                    }
                }
                else
                {
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    lblError1.Text = "Please select valid " + Resources.DeffinityRes.Customer;
                }
            }
            else
            {
                lblError1.ForeColor = System.Drawing.Color.Red;
                lblError1.Text = "Please enter customer name";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private int RandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 1000);
    }


    private void selectPortFolio()
    {
        try
        {
            SqlDataReader dr = Portfilio.SelectPortfolio(int.Parse(ddlPortfolio.SelectedValue));

            TxtCustomerName.Text = ddlPortfolio.SelectedItem.Text;
            DiVCustomerName.Visible = true;

            while (dr.Read())
            {
               
                //txtDesc.Text = dr["Description"].ToString();
                //getdata.DdlBindSelect(ddlOwner, "select ID,ContractorName from Contractors where Status='ACTIVE' and SID in (1,2,3) order by ContractorName", "ID", "ContractorName", false, true);
        
                ddlOwner.SelectedValue = dr["Owner"].ToString();

                BindSalesExecutiveddl();
                if (dr["AssignedSalesExecutive"] != null)
                {
                    ddlAssignedSalesExecutive.SelectedValue = !string.IsNullOrEmpty(dr["AssignedSalesExecutive"].ToString()) ? dr["AssignedSalesExecutive"].ToString() : "0";
                }
                else
                    ddlAssignedSalesExecutive.SelectedIndex = 0;
                //txtBudget.Text = dr["MaxBudget"].ToString();
                //txtresourcerequired.Text = dr["ResourcesRequired"].ToString();
                //txtriskandissues.Text = dr["RisksandIssues"].ToString();
                //dlportfoliotype.SelectedValue = dr["PortfolioTypeID"].ToString();
               // txtAddress.Text = dr["Address"].ToString();
                //txtKeyContactName.Text = dr["KeyContactName"].ToString();
                //txtMobileNumber.Text = dr["TelephoneNumber"].ToString();
                //txtOtherNumber.Text=dr["OtherNumber"].ToString();
                //txtEmailAddress.Text =  dr["EmailAddress"].ToString();
               // imgLogo.Src = System.Web.HttpContext.Current.Request.Path + "/../images/Portfolio_" + ddlPortfolio.SelectedItem.Value + ".gif"+"?"+RandomNumber();
                string filePath = Server.MapPath("~/WF/UploadData/Customers/Portfolio_" + ddlPortfolio.SelectedItem.Value + ".png");
                if (File.Exists(filePath))
                {
                    imgLogo.ImageUrl = "/WF/UploadData/Customers/Portfolio_" + ddlPortfolio.SelectedItem.Value + ".png" + "?rid=" + RandomNumber();
                    imgLogo.Visible = true;

                }
                else
                    imgLogo.Visible = false;
                chkVisible.Checked = Convert.ToBoolean(string.IsNullOrEmpty(dr["Visible"].ToString()) ? "false" : dr["Visible"].ToString());
                chkAllowCustomers.Checked = Convert.ToBoolean(string.IsNullOrEmpty(dr["docenable"].ToString()) ? "false" : dr["docenable"].ToString());
                
            }
            dr.Close();
            dr.Dispose();

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
#endregion
    private void setDefaults()
    {
       // txtBudget.Text = string.Empty;
        //txtCostCenter.Text = string.Empty;
       // txtDesc.Text = string.Empty;
        txtPortfolio.Text = string.Empty;
        ddlOwner.SelectedIndex = 0;
        ddlPortfolio.SelectedIndex = 0;
        //ddlStatus.SelectedIndex = 0;
    }

    private void clear_fields()
    {
        //txtBudget.Text = string.Empty;
        //txtDesc.Text = string.Empty;
        //txtresourcerequired.Text = string.Empty;
        txtPortfolio.Text = string.Empty;
        //txtriskandissues.Text = string.Empty;
        //txtEmailAddress.Text = string.Empty;
        //txtKeyContactName.Text = string.Empty;
        //txtMobileNumber.Text = string.Empty;
        //txtOtherNumber.Text = string.Empty;
        //txtAddress.Text = string.Empty;
        //dlportfoliotype.SelectedIndex = 0;
        ddlOwner.SelectedIndex = 0;
        ddlPortfolio.SelectedIndex = 0;

        TxtCustomerName.Text = string.Empty;

        ddlAssignedSalesExecutive.SelectedIndex = 0;
        
    }

    

    public void bindgrid()
    {
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("SELECT   a.ID,a.Portfolio, a.CustomerID, c.ContractorName, c.EmailAddress,ContactNumber as Telephone FROM AssignedCustomerToPortfolio a INNER JOIN Contractors c ON a.CustomerID = c.ID where lower(c.Status)='active' and a.Portfolio=" + sessionKeys.PortfolioID, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void bindAssignedSites()
    {
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("SELECT AssignedSitesToPortfolio.ID,AssignedSitesToPortfolio.Portfolio,Site.Site, City.City, Country.Country,isnull(Address1,'') as Address FROM AssignedSitesToPortfolio INNER JOIN  Site ON AssignedSitesToPortfolio.SiteID = Site.ID INNER JOIN City ON Site.CityID = City.ID INNER JOIN Country ON City.CountryID = Country.ID where AssignedSitesToPortfolio.portfolio=" + sessionKeys.PortfolioID, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public bool check()
    {
        int c = 0;
        SqlCommand comm_chk = new SqlCommand("select count(*) from AssignedCustomerToPortfolio where CustomerID=" + ddlCustomer.SelectedItem.Value + " and PortFolio=" + sessionKeys.PortfolioID.ToString() , conn);
        using (comm_chk)
        {
            conn.Open();
            c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
            conn.Close();
        }
        bool re = false; 
        if (c == 0)
        {
            re = true;
        }
        return re;
    }

    public bool check1()
    {
        int c = 0;
        SqlCommand comm_chk = new SqlCommand("select count(*) from AssignedSitesToPortfolio where SiteID=" + ddlSite.SelectedItem.Value + " and PortFolio=" + sessionKeys.PortfolioID.ToString() + "", conn);
        using (comm_chk)
        {
            conn.Open();
            try
            {
                c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
            }
            catch { lbltest.Text = "Check country,city and site"; }
            conn.Close();
         }
        bool re = false;
        if (c == 0)
        {
            re = true;
        }
        return re;
    }

    private void recordLogException(Exception ex)
    {
        StringBuilder sbLogException = new StringBuilder();
        sbLogException.Append(string.Format("\nMessage:{0}", ex.Message));
        sbLogException.Append(string.Format("\nInner Exception:{0}", ex.InnerException));
        sbLogException.Append(string.Format("\nSource:{0}", ex.Source));
        sbLogException.Append(string.Format("\nData:{0}", ex.Data));
        sbLogException.Append(string.Format("\nStack Trace:{0}", ex.StackTrace));
        sbLogException.Append(string.Format("\nTarget Site:{0}", ex.TargetSite));
        LogExceptions.LogException(sbLogException.ToString());
    }

    #endregion
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindcity();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSite();
    }
    private void bindcountry()
    {
        getdata.DdlBindSelect(ddlCountry, "DN_SelectCountry", "ID", "Country", true, true);
    }
    private void Bindcity()
    {
        getdata.DdlBindSelect(ddlCity, string.Format("Select ID,City from City where CountryID={0} AND Visible='Y' order by City", ddlCountry.SelectedValue), "ID", "City", false, false,true);
    }
    private void BindSite()
    {
        getdata.DdlBindSelect(ddlSite, string.Format("Select ID,Site from Site where CityID={0} AND Visible='Y'", ddlCity.SelectedValue), "ID", "Site", false, false,true);
    }
    protected void btnCountry_Click(object sender, EventArgs e)
    {
        ddlCountry.Visible = false;
        ddlCity.Visible = true;
        btnCity.Visible = true;
        ddlSite.Visible = true;
        btnSite.Visible = true;
        btnCountry.Visible = false;
        PnlCountry.Visible = true;
        
        getdata.DdlBindSelect(ddlCity, string.Format("Select ID,City from City where CountryID={0} AND Visible='N'",0), "ID", "City", false, false,true);
        getdata.DdlBindSelect(ddlSite, string.Format("Select ID,Site from Site where CityID={0} AND Visible='N'",0), "ID", "Site", false, false,true);
        
    }
    protected void btnCity_Click(object sender, EventArgs e)
    {
        ddlCountry.Visible =true;
        btnCountry.Visible = true;
        ddlSite.Visible = true;
        btnSite.Visible = true;
        ddlCity.Visible = false;
        btnCity.Visible = false;
        Pnlcity.Visible = true;
        btnSave.Enabled = false;
    }
    protected void btnSite_Click(object sender, EventArgs e)
    {
        ddlCountry.Visible = true;
        btnCountry.Visible = true;
        ddlCity.Visible = true;
        btnCity.Visible = true;
        Pnlsite.Visible = true;
        btnSite.Visible = false;
        ddlSite.Visible = false;
        btnSave.Enabled = false;
        pnlhideSite.Visible = false;
    }

    protected void Cancel1_Click(object sender, EventArgs e)
    {
        bindcountry();
        ddlCountry.Visible = true;
        PnlCountry.Visible = false;
        btnCountry.Visible = true;
        btnSave.Enabled =true;
    }
    protected void Cancel2_Click(object sender, EventArgs e)
    {
        bindcountry();
        ddlCity.Visible = true;
        Pnlcity.Visible = false;
        btnCity.Visible = true;
        btnSave.Enabled = true;
    }
    protected void Cancel3_Click(object sender, EventArgs e)
    {
        bindcountry();
        ddlSite.Visible = true;
        Pnlsite.Visible = false;
        btnSite.Visible = true;
        btnSave.Enabled = true;
        pnlhideSite.Visible = true;
    }
    protected void InsCountry_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("deffinity_InsertCountry", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = txtCountry.Text.ToString();
            SqlParameter outValue = new SqlParameter("@Outvalue", SqlDbType.Int);
            outValue.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(outValue);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            string _value = cmd.Parameters["@OutValue"].Value.ToString();

            if (_value == "1")
            {
                lbltest.Text = "Country has been sucessfully updated";
            }
            else
            {
                lbltest.Text = "Country has been sucessfully added";
            }
            bindcountry();
            ddlCountry.Visible = true;
            btnCountry.Visible = true;
            PnlCountry.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void InsCity_Click(object sender, EventArgs e)
    {
        try{
        if (ddlCountry.SelectedItem.Value == "Please select...")
        {
            lbltest.Text = "Please select country";
        }
        else
        {
            
            SqlCommand cmd = new SqlCommand("deffinity_InsertCity", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = txtCity.Text.ToString();
            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = ddlCountry.SelectedItem.Text;
            SqlParameter outValue = new SqlParameter("@Outvalue", SqlDbType.Int);
            outValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outValue);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            string _value = cmd.Parameters["@OutValue"].Value.ToString();
            if (_value == "1")
            {
                lbltest.Text = "city has been successfully updated";
            }
            else if (_value == "2")
            {
                lbltest.Text = "City has been successfully added";
            }
            else
            {
                lbltest.Text = "City name is already exists";
            }
        }
        Bindcity();
        btnCity.Visible = true;
        ddlCity.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void InsSite_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCity.SelectedItem.Value == "0" || ddlCountry.SelectedItem.Value == "0")
            {
                lbltest.Text = "Please select city";
            }
            else
            {

                SqlCommand cmd = new SqlCommand("deffinity_Insertsite", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ID = new SqlParameter("@ID", SqlDbType.Int, 32);
                ID.Value = 0;
                cmd.Parameters.Add(ID);
                cmd.Parameters.Add("@Site", SqlDbType.VarChar, 50).Value = txtSite.Text.ToString();

                cmd.Parameters.Add("@CityID", SqlDbType.VarChar, 50).Value = ddlCity.SelectedItem.Value;

                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 1000).Value = txtaddr1.Text.ToString();

                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = string.Empty;

                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = string.Empty;

                cmd.Parameters.Add("@PostCode", SqlDbType.VarChar, 50).Value = txtpostcode.Text.ToString();


                SqlParameter outValue = new SqlParameter("@Outvalue", SqlDbType.Int);
                outValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outValue);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                string _value = cmd.Parameters["@OutValue"].Value.ToString();
                if (_value == "1")
                {
                    lbltest.Text = "Site has been successfully updated";
                }
                else if (_value == "2")
                {
                    lbltest.Text = "Site has been successfully added";
                }
                else
                {
                    lbltest.Text = "Site already exists";
                }
            }
            BindSite();
            ddlSite.Visible = true;
            btnSite.Visible = true;
            Pnlsite.Visible = false;
            pnlhideSite.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //btnEditCostCenter_OnClick
    //btnAddCostCenter_OnClick
    
   

    protected void lnkPermission_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/ProgrammePermission.aspx");
    }
}
