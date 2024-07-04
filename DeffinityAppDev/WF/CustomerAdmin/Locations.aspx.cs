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
using Location.DAL;
using Location.Entity;
using System.Data.SqlClient;

public partial class Locations : BasePage
{
    LocationDataContext LocDataCntxt;
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
       

        //btnBack
        if (Request.QueryString["type"] != null)
        {
            btnBack.Visible = true;
        }
        if (Request.QueryString["Project"] != null)
        {
            btnBack.Visible = true;
        }
        if (Request.QueryString["Tender"] != null)
        {
            btnBack.Visible = true;
        }
        if ((Request.QueryString["Project"] != null) || (Request.QueryString["ProjectPlanUrlID"] != null) || (Request.QueryString["ProjectPlanID"] != null) || (Session["Flag"] == "1"))
        {
            btnBack.Visible = true;

        }
        if (Request.QueryString["teams"] != null)
        {
            btnBack.Visible = true;
        }



        if (!IsPostBack)
        {
            BaseBindings();
            //lblNewCity1.Text = Resources.DeffinityRes.AddCity;// "&nbsp;Add&nbsp;New&nbsp;City";
            //lblNewSite.Text = Resources.DeffinityRes.AddSite;
            
        }
        
    }


    private void BaseBindings()
    {
        try
        {
            bindCountry();
            bindCity();
            bindSite();
            bindAssignedSites();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


     
    //protected void imgbtnInsertCity_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        LocDataCntxt = new LocationDataContext();
    //        City newCity = new City();
    //        string mycity =  txtNewCity.Text.Trim();
    //        int Cid = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
    //        var myCities = (from r in LocDataCntxt.Cities
    //                       where r.CountryID == Cid && r.City1 == mycity
    //                       select r).ToList();

            

    //        if (myCities.Count > 0)
    //        {
    //            // need to show error message that, entered City is already exist with that county
    //        }
    //        else
    //        {

    //            char visible = 'N';
    //            if (chkCityVisible.Checked == true)
    //            {
    //                visible = 'Y';

    //            }
    //            newCity.City1 = mycity;
    //            newCity.CountryID = Cid;
    //            newCity.Visible = visible;
    //            LocDataCntxt.Cities.InsertOnSubmit(newCity);
    //            LocDataCntxt.SubmitChanges();
    //            int id = newCity.ID;

    //            if (id == null)
    //            {
    //            }
    //            else
    //            {
    //                //DivCity.Visible = false;
    //                //txtNewCity.Text = string.Empty;
    //                lbltest.ForeColor = System.Drawing.Color.Green;
    //                lbltest.Text = Resources.DeffinityRes.City_Added;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}

    //imgbtnCancelCity
    //protected void imgbtnCancelCity_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DivCity.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //    }


    //}
    private void bindCountry()
    {
        try
        {
            LocDataCntxt = new LocationDataContext();
            
            var data = (from f in LocDataCntxt.CountryClasses
                        where f.Visible == 'Y'
                        orderby f.Country1
                        select new
                        {
                            f.ID,
                            f.Country1

                        }).Distinct();
            ddlCountry.DataSource = data.OrderBy(o => o.Country1);
            ddlCountry.DataTextField = "Country1";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();

            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void bindCity()
    {
        try
        {
            LocDataCntxt = new LocationDataContext();
            int Cid = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
            var data = (from f in LocDataCntxt.Cities
                        where f.CountryID == Cid
                        orderby f.City1
                        select new
                        {
                            f.ID,
                            f.City1

                        }).Distinct();
            ddlCity.DataSource = data.OrderBy(o => o.City1);
            ddlCity.DataTextField = "City1";
            ddlCity.DataValueField = "ID";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void bindSite()
    {
        try
        {
            LocDataCntxt = new LocationDataContext();
            int Cid = Convert.ToInt32(ddlCity.SelectedItem.Value.ToString());
            
            var mySite = from a in LocDataCntxt.AssignedSitesToPortfolios
                          join s in LocDataCntxt.Sites on a.SiteID equals s.ID
                          
                          where a.Portfolio == sessionKeys.PortfolioID && a.CityID == Cid
                          orderby s.Site1
                          select new { ID = s.ID, ASite = s.Site1};

            ddlSite.DataSource = mySite.OrderBy(o => o.ASite);
            ddlSite.DataTextField = "ASite";
            ddlSite.DataValueField = "ID";
            ddlSite.DataBind();
            ddlSite.Items.Insert(0, new ListItem("Please select...", "0"));

            //var data = (from f in LocDataCntxt.Sites
            //            where f.CityID == Cid && f.Visible == 'Y'
            //            orderby f.Site1
            //            select new
            //            {
            //                f.ID,
            //                f.Site1

            //            }).Distinct();
            //ddlSite.DataSource = data.OrderBy(o => o.Site1);
            //ddlSite.DataTextField = "Site1";
            //ddlSite.DataValueField = "ID";
            //ddlSite.DataBind();

            //ddlSite.Items.Insert(0, new ListItem("Please select...", "0"));
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            DivSite.Visible = false;
                LocDataCntxt = new LocationDataContext();
                int Cid = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
                var data = (from f in LocDataCntxt.Cities
                            where f.CountryID == Cid
                            orderby f.City1
                            select new
                            {
                                f.ID,
                                f.City1

                            }).Distinct();
                ddlCity.DataSource = data.OrderBy(o => o.City1);
                ddlCity.DataTextField = "City1";
                ddlCity.DataValueField = "ID";
                ddlCity.DataBind();

                ddlCity.Items.Insert(0, new ListItem("Please select...", "0"));

            
            


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void grdSites_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());

                LocDataCntxt = new LocationDataContext();
                AssignedSitesToPortfolio Asp = LocDataCntxt.AssignedSitesToPortfolios.Single(P => P.ID == ID);
                LocDataCntxt.AssignedSitesToPortfolios.DeleteOnSubmit(Asp);
                LocDataCntxt.SubmitChanges();
                int siteID = Asp.SiteID.Value;
                Site si = LocDataCntxt.Sites.Single(S => S.ID == siteID);
                LocDataCntxt.Sites.DeleteOnSubmit(si);
                LocDataCntxt.SubmitChanges();
                bindAssignedSites();
                bindSite();
                              

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void grdSites_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
           // grdSites.EditIndex = -1;
            grdSites.DataBind();
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
            grdSites.DataSource = ds;
            grdSites.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

  



    protected void lbtnAddNewCity_Click(object sender, EventArgs e)
    {
        try
        {

            LocDataCntxt = new LocationDataContext();
            City newCity = new City();
            string mycity = txtCityNew.Text.Trim();
            int Cid = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
            var myCities = (from r in LocDataCntxt.Cities
                            where r.CountryID == Cid && r.City1 == mycity
                            select r).ToList();



            if (myCities.Count > 0)
            {
                // need to show error message that, entered City is already exist with that county


            }
            else
            {

                char visible = 'Y';
                //if (chkCityVisible.Checked == true)
                //{
                //    visible = 'Y';

                //}
                newCity.City1 = mycity;
                newCity.CountryID = Cid;
                newCity.Visible = visible;
                LocDataCntxt.Cities.InsertOnSubmit(newCity);
                LocDataCntxt.SubmitChanges();
                int id = newCity.ID;

                if (id == null)
                {
                }
                else
                {

                    lbltest.Visible = true;
                    lbltest.ForeColor = System.Drawing.Color.Green;
                    lbltest.Text = Resources.DeffinityRes.City_Added;
                    ddlCity.Visible = true;
                    txtCityNew.Visible = false;
                    lbtnAddCity.Visible = true;
                    //ImgAddCity.Visible = true;
                    imgCancelNewCity.Visible = false;
                    imgAddNewCity.Visible = false;
                    bindCity();
                    ddlCity.SelectedValue =Convert.ToString(id);


                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    //lbtnAssignSite_Click
    //protected void lbtnAssignSite_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (sessionKeys.PortfolioID != null && sessionKeys.PortfolioID != 0 )
    //        {
    //            if (Convert.ToInt32(ddlSite.SelectedItem.Value.ToString()) != 0)
    //            {
    //                LocDataCntxt = new LocationDataContext();
    //                AssignedSitesToPortfolio newAssignPort = new AssignedSitesToPortfolio();

    //                int Ctid = Convert.ToInt32(ddlCity.SelectedItem.Value.ToString());
    //                int SiteId = Convert.ToInt32(ddlSite.SelectedItem.Value.ToString());
    //                int PorId = sessionKeys.PortfolioID;
    //                var myCities = (from r in LocDataCntxt.AssignedSitesToPortfolios
    //                                where r.CityID == Ctid && r.SiteID == SiteId && r.Portfolio == PorId
    //                                select r).ToList();



    //                if (myCities.Count > 0)
    //                {
    //                    // need to show error message that, entered City is already exist with that country
    //                    lbltest.Visible = true;
    //                    lbltest.ForeColor = System.Drawing.Color.Red;
    //                    lbltest.Text = "Selected Site is already assigned"; //Resources.DeffinityRes.Citynamealrdyexists;
    //                }
    //                else
    //                {

    //                    newAssignPort.SiteID = SiteId;
    //                    newAssignPort.Portfolio = PorId;
    //                    newAssignPort.CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
    //                    newAssignPort.CityID = Ctid;
    //                    LocDataCntxt.AssignedSitesToPortfolios.InsertOnSubmit(newAssignPort);
    //                    LocDataCntxt.SubmitChanges();
    //                    int id = newAssignPort.ID;

    //                    if (id == null)
    //                    {
    //                    }
    //                    else
    //                    {

    //                        //lbltest.ForeColor = System.Drawing.Color.Green;
    //                        //lbltest.Text = Resources.DeffinityRes.;

    //                        bindAssignedSites();

    //                    }
    //                }
    //            }
    //            else
    //            {
    //                lbltest.Visible = true;
    //                lbltest.ForeColor = System.Drawing.Color.Red;
    //                lbltest.Text = Resources.DeffinityRes.PleaseselectSite;
    //            }
    //        }
    //        else
    //        {
    //            lbltest.Visible = true;
    //            lbltest.ForeColor = System.Drawing.Color.Red;
    //            lbltest.Text = Resources.DeffinityRes.Pleaseselectacustomer;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}


    //lbtnAddSite

    protected void lbtnAddSite_Click(object sender, EventArgs e)
    {
        try
        {
            ddlSite.Visible = false;
            txtsitename.Visible = true;
          
            DivSite.Visible = true;
            //lblNewSite.Text = Resources.DeffinityRes.AddSite;
            //lbtnAssignSite.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


   

    protected void imgbtnInsertSite_Click(object sender, EventArgs e)
    {
        try
        {
            if(ddlCity.SelectedIndex != 0)
            {
                if(txtsitename.Text.ToString() !="")
                {
               

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            con.Open();
            SqlCommand cmd = new SqlCommand("deffinity_Insertsite", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //if (HiddenField3.Value == "")
            //{
            //    HiddenField3.Value = "0";
            //}
            //if (HiddenField4.Value == "")
            //{
            //    HiddenField4.Value = "0";
            //}
            SqlParameter ID = new SqlParameter("@ID", SqlDbType.Int, 32);
            ID.Value = 0;

            cmd.Parameters.Add(ID);

            SqlParameter site = new SqlParameter("@Site", SqlDbType.VarChar, 50);
            site.Value = txtsitename.Text;
            cmd.Parameters.Add(site);

            SqlParameter cityname = new SqlParameter("@CityID", SqlDbType.VarChar, 50);
            cityname.Value = Convert.ToInt32(ddlCity.SelectedItem.Value.ToString());// HiddenField4.Value;
            cmd.Parameters.Add(cityname);

            SqlParameter Address1 = new SqlParameter("@Address1", SqlDbType.VarChar, 50);
            Address1.Value = txtaddr1.Text;
            cmd.Parameters.Add(Address1);

            SqlParameter Address2 = new SqlParameter("@Address2", SqlDbType.VarChar, 50);
            Address2.Value = "";// txtaddr2.Text;
            cmd.Parameters.Add(Address2);

            SqlParameter Address3 = new SqlParameter("@Address3", SqlDbType.VarChar, 50);
            Address3.Value = "";// txtaddr3.Text;
            cmd.Parameters.Add(Address3);

            SqlParameter PostCode = new SqlParameter("@PostCode", SqlDbType.VarChar, 50);
            PostCode.Value = txtpostcode.Text;
            cmd.Parameters.Add(PostCode);

            SqlParameter Portfolio = new SqlParameter("@PortfolioID", SqlDbType.Int, 4);
            Portfolio.Value = sessionKeys.PortfolioID;
            cmd.Parameters.Add(Portfolio);

            SqlParameter outValue = new SqlParameter("@Outvalue", SqlDbType.Int);
            outValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outValue);
            cmd.ExecuteNonQuery();

            string _value = cmd.Parameters["@OutValue"].Value.ToString();
            if (_value == "1")
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Green;
                lbltest.Text = Resources.DeffinityRes.Site_Updated;//"Site has been successfully updated";
            }
            else if (_value == "2")
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Green;
                lbltest.Text = Resources.DeffinityRes.Site_Added;//"Site has been successfully added";
            }
            else
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Red;
                lbltest.Text = Resources.DeffinityRes.Sitealreadyexists;//"Site already exists";
            }
           
            txtaddr1.Text = "";
            txtpostcode.Text = "";
            txtsitename.Text = string.Empty;
            //HiddenField3.Value = "";
            // HiddenField4.Value = "";  
            DivSite.Visible = false;
            
            //lblCity.Visible = false;
            txtsitename.Visible = false;
            ddlSite.Visible = true;
            bindSite();
            bindAssignedSites();
          //  lbtnAssignSite.Visible = true;
            
            }
            else
                {
                    lbltest.Visible = true;
                    lbltest.ForeColor = System.Drawing.Color.Red;
                    lbltest.Text = Resources.DeffinityRes.Pleaseentersitename;
                }
        }

        else
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Red;
                lbltest.Text = Resources.DeffinityRes.Pleaseselectcity;
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    
    protected void imgbtnCancelSite_Click(object sender, EventArgs e)
    {
        try
        {
            
            DivSite.Visible = false;
            txtaddr1.Text = string.Empty;
            txtpostcode.Text = string.Empty;
            txtsitename.Text = string.Empty;
            txtsitename.Visible = false;
            ddlSite.Visible = true;
            bindSite();
            bindAssignedSites();
            //lbtnAssignSite.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        if ((Request.QueryString["Project"] != null) && (Request.QueryString["type"] != "Projectasset") && (Request.QueryString["type"] != "PMAssignasset"))
        {
            string Project = Request.QueryString["Project"].ToString();
            Response.Redirect("~/ProjectOverview.aspx?Project=" + Project);
        }
        else if (Request.QueryString["ProjectPlanID"] != null)
        {
            Response.Redirect("~/ProjectPlan.aspx?ProjectPlanID=" + Request.QueryString["ProjectPlanID"].ToString());
        }
        else if (Request.QueryString["Asset"] != null)
        {
            Response.Redirect("~/AssetsAdmin.aspx?Asset=" + Request.QueryString["Asset"].ToString());
        }
        else if ((Request.QueryString["type"] != null) && (Request.QueryString["type"] != "Projectasset") && (Request.QueryString["type"] != "PMAssignasset"))
        {
            if (Request.QueryString["type"].ToLower() == "project")
            {
                Response.Redirect("~/ProjectOverview.aspx?type=project");
            }
            else if (Request.QueryString["type"] == "plan")
            {
                Response.Redirect("~/ProjectPlan.aspx");
            }
            else if (Request.QueryString["type"] == "assets")
            {
                Response.Redirect("~/AssetsAdmin.aspx");
            }

        }

        else if ((Request.QueryString["type"] != null) && (Request.QueryString["type"] == "Projectasset") && (Request.QueryString["type"] != "PMAssignasset"))
        {
            //if (Request.QueryString["type"] == "Projectasset")
            //{
            string Project = Request.QueryString["Project"].ToString();
            Response.Redirect("~/ProjectAsset.aspx?Project=" + Project);
            //}
        }

        else if ((Request.QueryString["type"] != null) && (Request.QueryString["type"] == "PMAssignasset"))
        {
            //if (Request.QueryString["type"] == "Projectasset")
            //{
            string Project = Request.QueryString["Project"].ToString();
            Response.Redirect("~/Checkpoint_Assets.aspx?Project=" + Project);
            //}
        }
        else if ((Request.QueryString["teams"] != null))
        {
            Response.Redirect("~/TeamScheduler.aspx");
        }
        else if ((Request.QueryString["Tender"] != null))
        {
            Response.Redirect("~/RFIOverview.aspx?Project=" + Request.QueryString["Tender"].ToString());
        }


    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindSite();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    
    protected void imgAddNewCity_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry.SelectedValue.ToString() != "0")
            {
                if (txtCityNew.Text != "")
                {
                    LocDataCntxt = new LocationDataContext();
                    City newCity = new City();
                    string mycity = txtCityNew.Text.Trim();
                    int Cid = Convert.ToInt32(ddlCountry.SelectedItem.Value.ToString());
                    var myCities = (from r in LocDataCntxt.Cities
                                    where r.CountryID == Cid && r.City1 == mycity
                                    select r).ToList();



                    if (myCities.Count > 0)
                    {
                        // need to show error message that, entered City is already exist with that county
                        lbltest.Visible = true;
                        lbltest.ForeColor = System.Drawing.Color.Red;
                        lbltest.Text = "City already exists. Please check and try again.";
                    }
                    else
                    {

                        char visible = 'Y';
                        newCity.City1 = mycity;
                        newCity.CountryID = Cid;
                        newCity.Visible = visible;
                        LocDataCntxt.Cities.InsertOnSubmit(newCity);
                        LocDataCntxt.SubmitChanges();
                        int id = newCity.ID;

                        if (id == null)
                        {
                        }
                        else
                        {
                            //DivCity.Visible = false;
                            lbltest.Visible = true;
                            lbltest.ForeColor = System.Drawing.Color.Green;
                            txtCityNew.Text = string.Empty;
                            lbltest.Text = Resources.DeffinityRes.City_Added;
                            lbtnAddCity.Visible = true;
                            //ImgAddCity.Visible = true;
                            ImgbtnDelCity.Visible = true;
                            imgAddNewCity.Visible = true;
                            ddlCity.Visible = true;
                            txtCityNew.Visible = false;
                            //lblNewCity1.Text = Resources.DeffinityRes.AddCity;
                            
                            txtCityNew.Visible = false;
                            imgAddNewCity.Visible = false;
                            imgCancelNewCity.Visible = false;
                            bindCity();
                            bindSite();
                            ddlCity.SelectedValue = Convert.ToString(id);


                        }
                    }
                }
                else
                {
                    lbltest.Visible = true;
                    lbltest.ForeColor = System.Drawing.Color.Red;
                    lbltest.Text = Resources.DeffinityRes.PlsEnterCityName;
                }
            }
            else
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Red;
                lbltest.Text = Resources.DeffinityRes.Plsselectcountry;
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgCancelNewCity_Click(object sender, EventArgs e)
    {
        try
        {
            //lblNewCity1.Text = Resources.DeffinityRes.AddCity;
            //lblNewCity1.Visible = true;
            
            imgAddNewCity.Visible = false;
            imgCancelNewCity.Visible = false;
            txtCityNew.Visible = false;
            //ImgAddCity.Visible = true;
            lbtnAddCity.Visible = true;
            ddlCity.Visible = true;
            ImgbtnDelCity.Visible = true;
            bindCity();
            bindAssignedSites();
            bindSite();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void lbtnAddCity_Click(object sender, EventArgs e)
    {
        try
        {
            imgAddNewCity.Visible = false;
            ImgbtnDelCity.Visible = false;
            //ImgAddCity.Visible = false;
            lbtnAddCity.Visible = false;
            ddlCity.Visible = false;
            imgAddNewCity.Visible = true;
            imgCancelNewCity.Visible = true;
            txtCityNew.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnDelSite_Click(object sender, EventArgs e)
    {
        try
        {
            LocDataCntxt = new LocationDataContext();
            int SiteId = Convert.ToInt32(ddlSite.SelectedItem.Value.ToString());
            if (SiteId != 0)
            {
                LocDataCntxt = new LocationDataContext();
                var myAssignedSites = from M in LocDataCntxt.AssignedSitesToPortfolios
                                      where M.SiteID == SiteId && M.Portfolio == sessionKeys.PortfolioID
                                      select M;

                LocDataCntxt.AssignedSitesToPortfolios.DeleteAllOnSubmit(myAssignedSites);
                LocDataCntxt.SubmitChanges();
                bindAssignedSites();

                var mySites = from S in LocDataCntxt.Sites
                              where S.ID == SiteId
                              select S;
                LocDataCntxt.Sites.DeleteAllOnSubmit(mySites);
                LocDataCntxt.SubmitChanges();
                bindSite();
            }
            else
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Red;
                lbltest.Text = Resources.DeffinityRes.PleaseselectSite;
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void ImgbtnDelCity_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCity.SelectedValue.ToString() != "0")
            {
                LocDataCntxt = new LocationDataContext();
                int CityID = Convert.ToInt32(ddlCity.SelectedItem.Value.ToString());
                var myCity = from L in LocDataCntxt.Cities
                             where L.ID == CityID
                             select L;
                LocDataCntxt.Cities.DeleteAllOnSubmit(myCity);
                LocDataCntxt.SubmitChanges();
                bindCity();

                var myAssignedSites = from M in LocDataCntxt.AssignedSitesToPortfolios
                              where M.CityID == CityID
                              select M;

                LocDataCntxt.AssignedSitesToPortfolios.DeleteAllOnSubmit(myAssignedSites);
                LocDataCntxt.SubmitChanges();
                bindAssignedSites();

                var mySites = from S in LocDataCntxt.Sites
                              where S.CityID == CityID
                              select S;
                LocDataCntxt.Sites.DeleteAllOnSubmit(mySites);
                LocDataCntxt.SubmitChanges();
                bindSite();
            }
            else
            {
                lbltest.Visible = true;
                lbltest.ForeColor = System.Drawing.Color.Red;
                lbltest.Text = Resources.DeffinityRes.Pleaseselectcity;
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void grdSites_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdSites.PageIndex = e.NewPageIndex;
            bindAssignedSites();
            //grdSites.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
