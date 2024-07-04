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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Data.Common;
using System.Text.RegularExpressions;
using UserAssetsMovingSourecToDestination;
using System.Linq;
using AssetsMgr.DAL;
using AssetsMgr.Entity;
using Microsoft.ApplicationBlocks.Data;
using POMgt.Entity;
using POMgt.DAL;
using PortfolioMgt.Entity;
using System.Collections.Generic;
public partial class controls_AdminAssetsMove : System.Web.UI.UserControl
{
    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
    PortfolioContact pContact = null;

    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string DBstring = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    AssetsToSoftwareDataContext assetsReport = new AssetsToSoftwareDataContext();
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    string UID = "";
    string Stemp;
    public string date = "";
    //public static int ID, OldID;
    SqlDataReader dr;
    protected string getUrl = "";
  //  AssetsAdminClass ASAdminCls = new AssetsAdminClass();
    DisBindings getDropDown = new DisBindings();
    LogExceptions ex = new LogExceptions();
   
    
    AssetsMoving GetProperties = null;
   AssetsMoving ASAdminCls = new AssetsMoving();
   protected void Page_Load(object sender, EventArgs e)
   {
       //OldID = Master.sid;
       //Master.PageHead = "Admin&nbsp;Assets";
       lblerror.Text = "";
       lbl_Results.Text = "";
       if (!Page.IsPostBack)
       {
           try
           {
               //New 
               BindVendorAsAssignedTechnision();
               BindCustomers();
               BindDropdown();
               BindVendors();
               //lblNewCountry.Text = "&nbsp;Add&nbsp;New&nbsp;Country";
               //lblNewCity.Text = "&nbsp;Add&nbsp;New&nbsp;City";
               //lblNewSite.Text = "&nbsp;Add&nbsp;New&nbsp;Site";

               //bindtree();

               //treeview1.Nodes[0].Expanded = true;        
               lblerror.Text = "";

               if (Request.QueryString["central"] != null)
               {
                   pnlAdd.Visible = false;
                   pnlSearch.Visible = true;
               }
               else
               {
                   pnlAdd.Visible = true;
                   pnlSearch.Visible = false;
               }

               if (Request.QueryString["PContactId"] != null)
               {
                    int contactID = int.Parse(Request.QueryString["PContactId"]);
                   int ddlCustomerId = 0;
                   if (sessionKeys.PortfolioID != 0)
                   {
                       ddlCustomerId = sessionKeys.PortfolioID;
                       lblCustomer.Visible = true;
                       lblCustomer.Text = "Customer:" + ddlCustomer.SelectedItem;
                   }
                   else
                   {
                       lblCustomer.Visible = false;
                       sessionKeys.PortfolioID = (int.Parse(ddlCustomer.SelectedValue));
                       ddlCustomerId = sessionKeys.PortfolioID;
                       lblCustomer.Text = "Customer:" + sessionKeys.PortfolioName;
                   }
                   //set contact name
                    cRepository = new PortfolioRepository<PortfolioContact>();
                    var pcontact = cRepository.GetAll().Where(o => o.ID == contactID).FirstOrDefault();
                    if (pcontact != null)
                    {
                        lblContactUser.Text = "<b>Contact Name: </b>" + pcontact.Name;
                        lblCustomer.Text = string.Empty;
                     }
                    GridBindWithCantactId(contactID, ddlCustomerId);
               }
               else
               {

                   if (sessionKeys.PortfolioID != 0)
                   {
                       lblCustomer.Visible = true;
                       lblCustomer.Text = "Customer:" + ddlCustomer.SelectedItem;
                       // lblCustomer.Text = "Customer:<b>" + sessionKeys.PortfolioName + "<b>";
                       //filldatabindassets(sessionKeys.PortfolioID);
                       //bindAssignedSites(sessionKeys.PortfolioID);
                       GridBindWithCantactId(0, sessionKeys.PortfolioID);
                   }
                   else
                   {
                       lblCustomer.Visible = false;
                       sessionKeys.PortfolioID = (int.Parse(ddlCustomer.SelectedValue));
                       //filldatabindassets(int.Parse(ddlCustomer.SelectedValue));
                       //bindAssignedSites(sessionKeys.PortfolioID);
                       GridBindWithCantactId(0, sessionKeys.PortfolioID);
                   }
               }
               if (Convert.ToInt32(Request.QueryString["EditItem"]) > 0 || Request.QueryString["EditItem"] != null)
               {

                   btn_cancel.Visible = true;
                   ImgBtnSubmit.Visible = false;
                   ImgBtnUpdate.Visible = true;
               }
               if (Request.QueryString["PContactId"] != null)
               {
                   int PContactId = int.Parse(Request.QueryString["PContactId"]);
               }
           }
           catch (Exception ex)
           {
               LogExceptions.LogException(ex.Message);
           }
       }
       //getValue();
   }
   public void GridBindWithCantactId(int ContactId,int PortfolioID)
   {
       try
       {
           List<string> asContactlist = new List<string>();
           List<AssetAssociatedToContact> AssociatedContactlist = new List<AssetAssociatedToContact>();

           using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
           {
               using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
               {

                   //get only single customers/contact asset
                   if (ContactId > 0)
                   {
                       //get contact list
                       var contactlist = pd.PortfolioContacts.Where(o => o.ID == ContactId).ToList();

                       var x = asset.AssetAssociatedToContacts.Where(a => a.ContactId == ContactId).Select(a => a.AssetId.ToString()).ToList();
                       var Asslist = (from a in asset.V_Assets where a.PortfolioID == PortfolioID select a).ToList();
                       Asslist = (from a in Asslist where x.Contains(a.ID.ToString()) select a).ToList();

                       var result = (from p in Asslist
                                     select new
                                     {
                                         CustomerName = contactlist.Where(o => o.ID == AssociatedContactlist.Where(a => a.AssetId == p.ID).Select(a => a.ContactId).FirstOrDefault()).Select(o => o.Name).FirstOrDefault(),
                                         CustomerID = contactlist.Where(o => o.ID == AssociatedContactlist.Where(a => a.AssetId == p.ID).Select(a => a.ContactId).FirstOrDefault()).Select(o => o.ID).FirstOrDefault(),
                                         p.Approve,
                                         p.AssestValue,
                                         p.AssetNo,
                                         p.AssetsType,
                                         p.AssetsTypeID,
                                         p.AssignName,
                                         p.AssignType,
                                         p.ContractorName,
                                         p.Datecommision,
                                         p.Datemoved,
                                         p.ExpDate,
                                         p.FromBuilding,
                                         p.FromFloor,
                                         p.FromIPAddress,
                                         p.FromLocation,
                                         p.FromNotes,
                                         p.FromOwner,
                                         p.FromPort,
                                         p.FromRoom,
                                         p.FromSite,
                                         p.FromSiteName,
                                         p.FromSubnet,
                                         p.FromVLAN,
                                         p.ID,
                                         p.Make,
                                         p.MakeName,
                                         p.Model,
                                         p.ModelName,
                                         p.NewAsset,
                                         p.PortfolioID,
                                         p.PortfolioName,
                                         p.ProjectReference,
                                         p.PurchasedDate,
                                         p.SerialNo,
                                         p.StatusId,
                                         p.StatusName,
                                         p.Technical,
                                         p.ToBuilding,
                                         p.ToFloor,
                                         p.ToIPAddress,
                                         p.ToLocation,
                                         p.ToNotes,
                                         p.ToOwner,
                                         p.ToPort,
                                         p.ToRoom,
                                         p.ToSite,
                                         p.ToSiteName,
                                         p.ToSubnet,
                                         p.ToVLAN,
                                         p.Type,
                                         p.TypeName,
                                         p.userid,
                                         p.VendorID,
                                         DifferenceInDays = Math.Round(Convert.ToDouble(GetDatesDifferenceInDays(p.Datecommision.Value.ToShortDateString(), p.ExpDate.Value.ToShortDateString())), 0)
                                     }).ToList();



                       GridView1.DataSource = result;
                       GridView1.DataBind();
                   }
                   //get all asset
                   else
                   {
                     


                       //get selected contact list
                       if (Convert.ToInt32(string.IsNullOrEmpty(ddlCustomerUser.SelectedValue) ? "0" : ddlCustomerUser.SelectedValue) > 0)
                       {
                           AssociatedContactlist = asset.AssetAssociatedToContacts.Where(a => a.ContactId == Convert.ToInt32(ddlCustomerUser.SelectedValue)).ToList();
                           asContactlist = AssociatedContactlist.Select(a => a.AssetId.ToString()).ToList();
                       }
                       else
                       {
                           AssociatedContactlist = asset.AssetAssociatedToContacts.Where(a => a.ContactId > 0).ToList();
                           asContactlist = AssociatedContactlist.Select(a => a.AssetId.ToString()).ToList();
                       }

                       //get contact list
                       var contactlist = pd.PortfolioContacts.Where(o => AssociatedContactlist.Select(p=>p.ContactId).Contains(o.ID)).ToList();

                       var Asslist = (from a in asset.V_Assets where a.PortfolioID == PortfolioID select a).ToList();
                       Asslist = (from a in Asslist where asContactlist.Contains(a.ID.ToString()) select a).ToList();

                       if (Asslist != null)
                       {
                           var result = (from p in Asslist
                                         select new
                                         {
                                             CustomerName = contactlist.Where(o=>o.ID ==  AssociatedContactlist.Where(a=>a.AssetId == p.ID).Select(a=>a.ContactId).FirstOrDefault()).Select(o=>o.Name).FirstOrDefault(),
                                             CustomerID = GetContactName( contactlist, AssociatedContactlist, p.ID),
                                             p.Approve,
                                             p.AssestValue,
                                             p.AssetNo,
                                             p.AssetsType,
                                             p.AssetsTypeID,
                                             p.AssignName,
                                             p.AssignType,
                                             p.ContractorName,
                                             p.Datecommision,
                                             p.Datemoved,
                                             p.ExpDate,
                                             p.FromBuilding,
                                             p.FromFloor,
                                             p.FromIPAddress,
                                             p.FromLocation,
                                             p.FromNotes,
                                             p.FromOwner,
                                             p.FromPort,
                                             p.FromRoom,
                                             p.FromSite,
                                             p.FromSiteName,
                                             p.FromSubnet,
                                             p.FromVLAN,
                                             p.ID,
                                             p.Make,
                                             p.MakeName,
                                             p.Model,
                                             p.ModelName,
                                             p.NewAsset,
                                             p.PortfolioID,
                                             p.PortfolioName,
                                             p.ProjectReference,
                                             p.PurchasedDate,
                                             p.SerialNo,
                                             p.StatusId,
                                             p.StatusName,
                                             p.Technical,
                                             p.ToBuilding,
                                             p.ToFloor,
                                             p.ToIPAddress,
                                             p.ToLocation,
                                             p.ToNotes,
                                             p.ToOwner,
                                             p.ToPort,
                                             p.ToRoom,
                                             p.ToSite,
                                             p.ToSiteName,
                                             p.ToSubnet,
                                             p.ToVLAN,
                                             p.Type,
                                             p.TypeName,
                                             p.userid,
                                             p.VendorID,
                                             DifferenceInDays = Math.Round(Convert.ToDouble(GetDatesDifferenceInDays(p.Datecommision.Value.ToShortDateString(), p.ExpDate.Value.ToShortDateString())), 0)
                                         }).ToList();

                           //check serial no
                           if (!string.IsNullOrEmpty(txtSearchSerialno.Text.Trim()))
                               result = result.Where(o => txtSearchSerialno.Text.Trim().ToLower().Contains(o.SerialNo.ToLower())).ToList();
                           //check exprire days
                           if (!string.IsNullOrEmpty(txtSearchExpire.Text.Trim()))
                               result = result.Where(o => o.DifferenceInDays >0 && o.DifferenceInDays <= Convert.ToInt32(txtSearchExpire.Text.Trim())).ToList();
                           //check expired
                           if (chkExpired.Checked)
                               result = result.Where(o => o.DifferenceInDays < 0).ToList();

                           GridView1.DataSource = result;
                           GridView1.DataBind();
                       }
                   }
               }
           }
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

    public int GetContactName(List<PortfolioContact> contactlist,List<AssetAssociatedToContact> AssociatedContactlist, int assetid)
   {
       var contactid = AssociatedContactlist.Where(a=>a.AssetId == assetid).Select(a=>a.ContactId).FirstOrDefault();
       var id = contactlist.Where(o => o.ID == contactid).Select(o => o.ID).FirstOrDefault();

       return id;
   }
   public void BindVendorAsAssignedTechnision()
   {
       try 
       {
           using (PurchaseOrderMgtDataContext VDc = new PurchaseOrderMgtDataContext())
           {
               var VList = (from a in VDc.v_Vendors
                            select new
                            {
                                Id = a.VendorID,
                                VName = a.ContractorName
                            }).ToList();
               DdlAssignedTechnician.DataValueField = "Id";
               DdlAssignedTechnician.DataTextField = "VName";
               DdlAssignedTechnician.DataSource = VList;
               DdlAssignedTechnician.DataBind();
               DdlAssignedTechnician.Items.Insert(0, new ListItem("Please select...", "0"));
           }
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }
   public void MailBody()
   {
       try
       {
           Emailer em = new Emailer();
           string body1 = em.ReadFile("~/WF/Assets/EmailTemplates/VendorEmailfromServiceDesk.html");
           body1 = body1.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
           body1 = body1.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
           body1 = body1.Replace("[username]", string.Empty);
           body1 = body1.Replace("[CustomerName]", string.Empty);
           body1 = body1.Replace("[TypeofCall]", string.Empty);
           body1 = body1.Replace("[Description]", string.Empty);
           body1 = body1.Replace("[AssetNumber]", string.Empty);
           body1 = body1.Replace("[Make]", string.Empty);
           body1 = body1.Replace("[Model]", string.Empty);
           body1 = body1.Replace("[SerialNumber]", string.Empty);
           body1 = body1.Replace("[WarrantyExpiryDate]", string.Empty);
           body1 = body1.Replace("[Scheduleddateandtime]", string.Empty);

           body1 = body1.Replace("[AcceptLink]", Deffinity.systemdefaults.GetWebUrl());
           body1 = body1.Replace("[CostLink]", Deffinity.systemdefaults.GetWebUrl());

           //
           em.SendingMail("fromEmailId", "subject", body1, "ToEmailAddress");


       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }
   private void BindCustomers()
   {
       ddlCustomer.DataSource = assetsReport.Assets_PortfilioSelect();
       ddlCustomer.DataValueField = "ID";
       ddlCustomer.DataTextField = "PortFolio";
       ddlCustomer.DataBind();
       if (sessionKeys.PortfolioID != 0)
       {
           ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
       }
   }
    private void BindVendors()
    {
        using (AssetsMgr.DAL.AssetsToSoftwareDataContext assets = new AssetsToSoftwareDataContext())
        {
            ddlVendors.DataSource = assets.GoodsReceipt_Vendors();
            ddlVendors.DataTextField = "ContractorName";
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataBind();
            ddlVendors.Items.Insert(0, new ListItem("Please select...", "0"));
            
        }
    }
    public void BindStatusDdl()
    {
        try
        {
            using (AssetsToSoftwareDataContext ass = new AssetsToSoftwareDataContext())
            {
                var slist = ass.Asset_Status.ToList();
                DdlStatus.DataSource = slist;
                DdlStatus.DataTextField = "StatusName";
                DdlStatus.DataValueField = "Id";
                DdlStatus.DataBind();
                DdlStatus.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindDropdown()
    {
        BindStatusDdl();

        getDropDown.DdlBindSelect(dt_Model, "select ModelID,Model from Assetsmodel", "ModelID", "Model", false, false, true);
        getDropDown.DdlBindSelect(dt_make, "select MakeID,Make from Assetsmake", "MakeID", "Make", false, false, true);
        getDropDown.DdlBindSelect(dt_Type, "select TypeID,Type from Assetstype", "TypeID", "Type", false, false, true);
         //getDropDown.DdlBindSelect(dt_Site, "select ID,Site from Site Order by Site", "ID", "Site", false, false,true);
        getDropDown.DdlBindSelect(dt_Site, string.Format("Select Distinct SiteID as ID,(select Site from Site where ID = AssignedSitesToPortfolio.SiteID) as Site from AssignedSitesToPortfolio where Portfolio ={0}",int.Parse(sessionKeys.PortfolioID!=0?sessionKeys.PortfolioID.ToString():ddlCustomer.SelectedValue)), "ID", "Site", false, false, true);
    }

    protected void getValue()
    {
        SqlConnection con = new SqlConnection(connectionString);
        try
        {

            string Stemp1;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID FROM Contractors WHERE ContractorName='" + sessionKeys.UID.ToString() + "'", con);
            Stemp1 = cmd.ExecuteScalar().ToString();

            getUrl = "Project=0&CID=" + Stemp1;

            con.Close();
        }
        catch (Exception ex)
        {

            LogExceptions.LogException(ex.Message);

        }
        finally
        {
            con.Close();

        }

    }


    private int getPref(string sPref)
    {

        int rt;
        if (sPref != "ALL")
        {

            rt = Convert.ToInt32(sPref.Substring(sPref.LastIndexOf(" ") + 1));

        }
        else
        {
            rt = 0;
        }

        return rt;

    }
    private void filldatabindassets(int portfolioID)
    {
        try
        {
            clear();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Deffinity_AssetSelectByPortfolio", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PortfolioID", portfolioID));
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView1.DataSource = ds;
            ////Bind the tables to the datagrid
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    protected void btn_Sumitt_Click(object sender, EventArgs e)
    {

    }
    protected void imgViewSoftware_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AssetSoftware.aspx?AssetID=" + HiddenAssID.Value);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imagemake_Click1(object sender, EventArgs e)
    {

        //dropdownlist visiblity
        dt_make.Visible = false;
        dt_Model.Visible = true;
        dt_Type.Visible = true;
        dt_Site.Visible = true;

        //imagebuttons visibility
        imagemake.Visible = false;
        imagemodel.Visible = true;
        imagetype.Visible = true;
        imagelocation.Visible = true;

        //textboxes
        txtmkae.Visible = true;
        txt_model.Visible = false;
        txt_type.Visible = false;
        txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = false;
        imodel_submitt.Visible = false;
        ilocation_submitt.Visible = false;

        imodel_cancel.Visible = false;
        ilocation_cancel.Visible = false;
        itype_cancel.Visible = false;
        i_makecancel.Visible = true;
        i_makesubmitt.Visible = true;

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_Results.Visible = true;
        imgViewSoftware.Visible = false;
       int  ID = int.Parse(sessionKeys.PortfolioID != 0 ? sessionKeys.PortfolioID.ToString() : ddlCustomer.SelectedValue);
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        //int ID =convert.GridView1.Rows[e.RowIndex].Cells[0]).Text;
        string delete = "delete from Assets where ID='" + int.Parse(id) + "'";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(delete, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            //Response.Write("<b> SUCCESSFULL DELETED<b>");
            lbl_Results.Text = "<b> SUCCESSFULLY DELETED<b>";
        }
        catch (Exception e1)
        {
            LogExceptions.LogException(e1.Message);

        }
        finally
        {
            con.Close();
        }
        GridView1.EditIndex = -1;
        filldatabindassets(ID);
        clear();
        // lbl_Results.Visible =true;

    }
    protected void imagemodel_Click(object sender, EventArgs e)
    {
        //dropdownlist visiblity
        dt_make.Visible = true;
        dt_Model.Visible = false;
        dt_Type.Visible = true;
        dt_Site.Visible = true;

        //imagebuttons visibility
        imagemake.Visible = true; ;
        imagemodel.Visible = false;
        imagetype.Visible = true;
        imagelocation.Visible = true;

        //textboxes
        txtmkae.Visible = false;
        txt_model.Visible = true; ;
        txt_type.Visible = false;
        txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = false;
        imodel_submitt.Visible = true;
        ilocation_submitt.Visible = false;

        imodel_cancel.Visible = true;
        ilocation_cancel.Visible = false;
        itype_cancel.Visible = false;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;
    }
    protected void imagetype_Click(object sender, EventArgs e)
    {

        //dropdownlist visiblity
        dt_make.Visible = true;
        dt_Model.Visible = true;
        dt_Type.Visible = false;
        dt_Site.Visible = true;

        //imagebuttons visibility
        imagemake.Visible = true; ;
        imagemodel.Visible = true;
        imagetype.Visible = false;
        imagelocation.Visible = true;

        //textboxes
        txtmkae.Visible = false;
        txt_model.Visible = false;
        txt_type.Visible = true;
        txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = true;
        imodel_submitt.Visible = false;
        ilocation_submitt.Visible = false;

        imodel_cancel.Visible = false;
        ilocation_cancel.Visible = false;
        itype_cancel.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;

    }
    protected void imagelocation_Click(object sender, EventArgs e)
    {

        //Response.Redirect("~/Locations.aspx?type=assets");
        //txtContry.Text = "";
        //txtsitename.Text = "";
        //txtaddr1.Text = "";
        //// txtaddr2.Text = "";
        //// txtaddr3.Text = "";
        //txtpostcode.Text = "";
        //lblinSite.Text = "";
        //lblSity.Text = "";
        //DivCtr.Visible = false;
        //DivSite.Visible = false;
        //DivCity.Visible = false;
        //lblinCity.Visible = false;
        //lblCountry.Visible = false;
        //modelPopupAddSite.Show();

    }
    protected void i_makecancel_Click(object sender, EventArgs e)
    {
        dt_make.Visible = true;
        txtmkae.Visible = false;
        imagemake.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;

    }
    protected void imodel_cancel_Click(object sender, EventArgs e)
    {
        dt_Model.Visible = true;
        imagemodel.Visible = true;
        txt_model.Visible = false;
        imodel_cancel.Visible = false;
        imodel_submitt.Visible = false;
    }
    protected void itype_cancel_Click(object sender, EventArgs e)
    {
        dt_Type.Visible = true;
        txt_type.Visible = false;
        imagetype.Visible = true;
        itype_submitt.Visible = false;
        itype_cancel.Visible = false;
    }
    protected void ilocation_cancel_Click(object sender, EventArgs e)
    {
        dt_Site.Visible = true;
        txt_site.Visible = false;
        imagelocation.Visible = true;
        ilocation_cancel.Visible = false;
        ilocation_submitt.Visible = false;
    }
    protected void i_makesubmitt_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            ////string updateSQL;
            SqlCommand sqlcomm = new SqlCommand("display_i_make", con);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            SqlParameter makename = new SqlParameter("@makename", SqlDbType.VarChar, 50);
            makename.Value = txtmkae.Text;
            sqlcomm.Parameters.Add(makename);
            SqlParameter makeno = new SqlParameter("@func", SqlDbType.Int, 32);
            makeno.Value = 1;
            sqlcomm.Parameters.Add(makeno);
            SqlParameter _outvalue = new SqlParameter("@OutValue", SqlDbType.Int, 32);
            _outvalue.Direction = ParameterDirection.Output;

            sqlcomm.Parameters.Add(_outvalue);

            sqlcomm.ExecuteNonQuery();
            if (Convert.ToInt32(_outvalue.Value) == 0)
            {

                lblerror.Visible = true;
                lblerror.Text = " Please Check Make name already exist";
            }
            else
            {
                getDropDown.DdlBindSelect(dt_make, "select MakeID,Make from Assetsmake", "MakeID", "Make", false, false, true);


                dt_make.SelectedValue = Convert.ToInt32(_outvalue.Value).ToString();
            }
            con.Close();
            //lblerror.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        //BindDropdown();
        //        fillmakelist();
        imagemake.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;
        dt_make.Visible = true;
        txtmkae.Visible = false;


    }
    protected void imodel_submitt_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string updateSQL;
           
            SqlCommand sqlcomm = new SqlCommand("display_i_model", con);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            SqlParameter modelname = new SqlParameter("@modelname", SqlDbType.VarChar, 50);
            modelname.Value = txt_model.Text;
            sqlcomm.Parameters.Add(modelname);
            SqlParameter modelid = new SqlParameter("@func", SqlDbType.Int, 32);
            modelid.Value = 1;

            sqlcomm.Parameters.Add(modelid);

            SqlParameter _outvalue = new SqlParameter("@OutValue", SqlDbType.Int,32);
            _outvalue.Direction = ParameterDirection.Output;

            sqlcomm.Parameters.Add(_outvalue);

           sqlcomm.ExecuteNonQuery();
            
            if (Convert.ToInt32(_outvalue.Value) == 0)
            {
                lblerror.Text = " Please Check Model Already Exists";
            }
            else
            {
                getDropDown.DdlBindSelect(dt_Model, "select ModelID,Model from Assetsmodel", "ModelID", "Model", false, false, true);
                              
                
                dt_Model.SelectedValue = Convert.ToInt32(_outvalue.Value).ToString();
            }
           
            con.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }
      //BindDropdown();
        //fillmodel();
        imodel_submitt.Visible = false;
        imodel_cancel.Visible = false;
        dt_Model.Visible = true;
       
        txt_model.Visible = false;
        imagemodel.Visible = true;
    }
    protected void itype_submitt_Click(object sender, EventArgs e)
    {
        //try
        //{
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        //string updateSQL;
        SqlCommand sqlcomm = new SqlCommand("display_i_type", con);
        sqlcomm.CommandType = CommandType.StoredProcedure;
        SqlParameter typename = new SqlParameter("@typename", SqlDbType.VarChar, 50);
        typename.Value = txt_type.Text;
        sqlcomm.Parameters.Add(typename);
        SqlParameter typeid = new SqlParameter("@func", SqlDbType.Int, 32);
        typeid.Value = 1;
        sqlcomm.Parameters.Add(typeid);

        SqlParameter _outvalue = new SqlParameter("@OutValue", SqlDbType.Int, 32);
        _outvalue.Direction = ParameterDirection.Output;

        sqlcomm.Parameters.Add(_outvalue);

        sqlcomm.ExecuteNonQuery();

        if (Convert.ToInt32(_outvalue.Value) == 0)
        {
            lblerror.Text = " Please Check type Already Exists";
        }
        else
        {
            
            getDropDown.DdlBindSelect(dt_Type, "select TypeID,Type from Assetstype", "TypeID", "Type", false, false, true);
            
            dt_Type.SelectedValue = Convert.ToInt32(_outvalue.Value).ToString();
        }
        con.Close();
        // }
        //catch (Exception ex)
        //{
        //    LogExceptions.LogException(ex.Message);
        //}
       // BindDropdown();
        //typefill();
        txt_type.Visible = false;
        itype_cancel.Visible = false;
        itype_submitt.Visible = false;
        dt_Type.Visible = true;
        imagetype.Visible = true;
    }
    protected void ilocation_submitt_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string updateSQL;
            SqlCommand sqlcomm = new SqlCommand("display_i_site", con);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            SqlParameter sitename = new SqlParameter("@sitename", SqlDbType.VarChar, 50);
            sitename.Value = txt_site.Text;
            sqlcomm.Parameters.Add(sitename);
            SqlParameter siteid = new SqlParameter("@func", SqlDbType.Int, 32);
            siteid.Value = 1;
            sqlcomm.Parameters.Add(siteid);
            SqlParameter _outvalue = new SqlParameter("@OutValue", SqlDbType.Int, 32);
            _outvalue.Direction = ParameterDirection.Output;

            sqlcomm.Parameters.Add(_outvalue);

            sqlcomm.ExecuteNonQuery();

            if (Convert.ToInt32(_outvalue.Value) == 0)
            {
                // lblerror.Visible = true;
                lblerror.Text = "Site Already Exists";
            }
            else
            {
                getDropDown.DdlBindSelect(dt_Site, string.Format("Select Distinct SiteID as ID,(select Site from Site where ID = AssignedSitesToPortfolio.SiteID) as Site from AssignedSitesToPortfolio where Portfolio ={0}", int.Parse(sessionKeys.PortfolioID != 0 ? sessionKeys.PortfolioID.ToString() : ddlCustomer.SelectedValue)), "ID", "Site", false, false, true);
                dt_Site.SelectedValue = Convert.ToInt32(_outvalue.Value).ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

        BindDropdown();
        //fillsite();
        txt_site.Visible = false;
        ilocation_cancel.Visible = false;
        ilocation_submitt.Visible = false;
        dt_Site.Visible = true;
        imagelocation.Visible = true;
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        filldatabindassets(Convert.ToInt32(ddlCustomer.SelectedValue));
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {


    }

    protected void imagesearch_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DN_Adminassetssearch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter assign0 = new SqlParameter("@serialno", SqlDbType.VarChar, 50);
        if (txt_serialno.Text == "")
        {
            txt_serialno.Text = "0";
        }

        assign0.Value = txt_serialno.Text;
        cmd.Parameters.Add(assign0);
        SqlParameter assign1 = new SqlParameter("@AssetNo", SqlDbType.VarChar, 50);
        if (txt_assetno.Text == "")
        {
            txt_assetno.Text = "0";

        }
        assign1.Value = txt_assetno.Text;
        cmd.Parameters.Add(assign1);
        if (txt_assetno.Text == "" && txt_serialno.Text == "")
        {
            lblerror.Text = "Asset not found.Please Check and try again..";
        }
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {

            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            con.Close();
        }
        lbl_Results.Visible = true;
        clear();




    }
    public void clear()
    {
        txt_assetno.Text = "";
        txt_Bulding.Text = "";
        txt_Cab.Text = "";
        txt_commision.Text = "";
        //txt_DateMoved.Text = "";
        txt_Floor.Text = "";
        txt_WarrantyPeriod.Text = "";
        txt_Room.Text = "";
        //txt_Technical.Text = "";
        txt_Owner.Text = "";
        txt_Notes.Text = "";
        txt_serialno.Text = "";
        txtipadress.Text = "";
        txtvlan.Text = "";
        txtsubnet.Text = "";
        dt_make.SelectedIndex = 0;
        dt_Model.SelectedIndex = 0;
        dt_Site.SelectedIndex = 0;
        dt_Type.SelectedIndex = 0;
        ddlVendors.SelectedIndex = 0;
        txtValue.Text = "";
        txtPurchasedDate.Text = "";
        txtExpDate.Text ="";
        txtScheduleMoveDate.Text = "";
        HiddenAssID.Value = "";
        DdlStatus.SelectedIndex = 0;
        //clear image field
        setAssetImage(0);
    }


    protected void lnkjournal_Click(object sender, EventArgs e)
    {
        // Response.Redirect("AssetsAdminJournal.aspx");

    }

    protected String CheckValue(string serilaNo, string assetNo)
    {
        SqlConnection con1 = new SqlConnection(connectionString);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand("DN_AssetsCheckSerialNo", con1);
        cmd1.CommandType = CommandType.StoredProcedure;
        SqlParameter serialno = new SqlParameter("@serialno", SqlDbType.VarChar, 50);
        SqlParameter assetno = new SqlParameter("@assetno", SqlDbType.VarChar, 50);
        if (serilaNo == "")
        {
            serilaNo = "null";

        }

        if (assetNo == "")
        {
            assetNo = "null";

        }
        serialno.Value = serilaNo;
        assetno.Value = assetNo;
        cmd1.Parameters.Add(serialno);
        cmd1.Parameters.Add(assetno);
        string count = cmd1.ExecuteScalar().ToString();
        con1.Close();

        return count;

    }
    public void InsertAssectToContactRelation(int AssectId, int ContactId)
    {
        try
        {
            using (AssetsToSoftwareDataContext AssDc = new AssetsToSoftwareDataContext())
            {
                AssetAssociatedToContact asset = new AssetAssociatedToContact();
                asset.AssetId = AssectId;
                asset.ContactId = ContactId;
                AssDc.AssetAssociatedToContacts.InsertOnSubmit(asset);
                AssDc.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImgBtnSubmit_Click(object sender, EventArgs e)
    {
        //if (DropValidations() == true)
        //{
        string error = "";

        AssetsMoving GetPortfolio11 = new AssetsMoving();
        AssetsMoving GetPermision = new AssetsMoving();

        string make = dt_make.SelectedItem.Value.ToString();


        lbl_Results.Visible = true;
        //string serialno = txt_serialno.Text;
        if ((txt_assetno.Text == "") && (txt_serialno.Text == ""))
        {
            lblerror.Text = "Please enter the Asset number or Serial number";
        }
        else
        {

            string count = CheckValue(txt_serialno.Text, txt_assetno.Text);

            if (int.Parse(count) == 1)
            {
                lblerror.Text = "* Record Already Exists";
            }
            else
            {
              
                //sessionKeys.PortfolioID.ToString(),
                if (sessionKeys.PortfolioID != 0)
                {
                    InsertNewAssetTomainDBPref(sessionKeys.PortfolioID);

                    // ASAdminCls.NewAssetSubmitBtn_Click(nwAsset);

                    if (Request.QueryString["PContactId"] != null)
                    {
                        GridBindWithCantactId(int.Parse(Request.QueryString["PContactId"]), sessionKeys.PortfolioID);
                    }
                    else
                    {
                        filldatabindassets(sessionKeys.PortfolioID);
                    }
                }
                else
                {
                    InsertNewAssetTomainDBPref(int.Parse(ddlCustomer.SelectedValue));

                    // ASAdminCls.NewAssetSubmitBtn_Click(nwAsset);
                    if (Request.QueryString["PContactId"] != null)
                    {
                        GridBindWithCantactId(int.Parse(Request.QueryString["PContactId"]), int.Parse(ddlCustomer.SelectedValue));
                    }
                    else
                    {
                        filldatabindassets(int.Parse(ddlCustomer.SelectedValue));
                    }
                }
                clear();

                btn_cancel.Visible = false;

            }
        }

    }
    public void InsertNewAssetTomainDBPref(int getprotfolio1)
    {

        try
        {
            int? assetid = 0;
            AssetsMgr.DAL.AssetsToSoftwareDataContext adc = new AssetsToSoftwareDataContext();
            adc.DN_InsertNewAdminAssetsDB(txt_serialno.Text.Trim(), txt_assetno.Text.Trim(), QueryStringValues.Project,
                Convert.ToInt32(getDropDown.getDdlval(dt_make.SelectedItem.Value).ToString()),
                Convert.ToInt32(getDropDown.getDdlval(dt_Model.SelectedItem.Value).ToString()),
                 Convert.ToInt32(getDropDown.getDdlval(dt_Type.SelectedItem.Value).ToString()),
                 Convert.ToInt32(getDropDown.getDdlval(dt_Site.SelectedItem.Value).ToString()),
                 txt_Bulding.Text.Trim(),
                 txt_Floor.Text.Trim(),
                 txt_Room.Text.Trim(),
                 txt_Cab.Text.Trim(),
                 Convert.ToDateTime(string.IsNullOrEmpty(txtScheduleMoveDate.Text.Trim()) ? "01/01/1900" : txtScheduleMoveDate.Text.Trim()),
                 Convert.ToDateTime(string.IsNullOrEmpty(txt_commision.Text.Trim()) ? "01/01/1900" : txt_commision.Text.Trim()),
                 txt_WarrantyPeriod.Text.Trim(),
                 txt_Owner.Text.Trim(),
                 txt_Notes.Text.Trim(),
                 string.Empty,
                 sessionKeys.UID,
                 txtvlan.Text.Trim(),
                 txtipadress.Text.Trim(),
                 txtsubnet.Text.Trim(),
                 0,
                 Convert.ToInt32(getDropDown.getDdlval(ddlCustomer.SelectedItem.Value).ToString()),
                 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, 0, 0, 0,
                 Convert.ToInt32(string.IsNullOrEmpty(ddlVendors.SelectedValue) ? "0" : ddlVendors.SelectedValue),
                 Convert.ToDateTime(string.IsNullOrEmpty(txtPurchasedDate.Text.Trim()) ? "01/01/1900" : txtPurchasedDate.Text.Trim()),
                 Convert.ToDateTime(string.IsNullOrEmpty(txtExpDate.Text.Trim()) ? "01/01/1900" : txtExpDate.Text.Trim()),
                 Convert.ToDouble(string.IsNullOrEmpty(txtValue.Text.Trim()) ? "0" : txtValue.Text.Trim()),
                 int.Parse(DdlStatus.SelectedValue),TxtColor.Text.Trim(),
                 ref assetid
                 );
            if (Request.QueryString["PContactId"] != null && assetid.Value > 0)
            {
                InsertAssectToContactRelation(assetid.Value, int.Parse(Request.QueryString["PContactId"]));
            }
            if (assetid.Value > 0)
            {
                AssetImageUpload(assetid.Value);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }




    protected void ImgBtnUpdate_Click(object sender, EventArgs e)
    {


        int ID = 0;
        try
        {

             AssetsMgr.DAL.AssetsToSoftwareDataContext adc = new AssetsMgr.DAL.AssetsToSoftwareDataContext();
             adc.DN_UpdateAdmin_AssetsNew(HiddenField5.Value.ToString(), QueryStringValues.Project, Convert.ToInt32(getDropDown.getDdlval(dt_make.SelectedItem.Value).ToString()),
                 Convert.ToInt32(getDropDown.getDdlval(dt_Model.SelectedItem.Value).ToString()),
                 Convert.ToInt32(getDropDown.getDdlval(dt_Type.SelectedItem.Value).ToString()),
                 Convert.ToInt32(getDropDown.getDdlval(dt_Site.SelectedItem.Value).ToString()),
                 txt_Bulding.Text.Trim(),
                  txt_Floor.Text.Trim(),
                  txt_Room.Text.Trim(),
                  txt_Cab.Text.Trim(),
                  Convert.ToDateTime(string.IsNullOrEmpty(txtScheduleMoveDate.Text.Trim()) ? "01/01/1900" : txtScheduleMoveDate.Text.Trim()),
                  Convert.ToDateTime(string.IsNullOrEmpty(txt_commision.Text.Trim()) ? "01/01/1900" : txt_commision.Text.Trim()),
                  txt_WarrantyPeriod.Text.Trim(),
                  txt_Owner.Text.Trim(),
                  sessionKeys.UID,
                  txtvlan.Text.Trim(),
                  txtipadress.Text.Trim(),
                  txtsubnet.Text.Trim(), 0,
                  Convert.ToInt32(getDropDown.getDdlval(ddlCustomer.SelectedItem.Value).ToString()),
                  txt_serialno.Text.Trim(),
                  string.Empty,
                  txt_Notes.Text.Trim(),
                  0,
                 int.Parse(HiddenAssID.Value),
                 0, 0,
                 Convert.ToInt32(string.IsNullOrEmpty(ddlVendors.SelectedValue) ? "0" : ddlVendors.SelectedValue),
                 Convert.ToDateTime(string.IsNullOrEmpty(txtPurchasedDate.Text.Trim()) ? "01/01/1900" : txtPurchasedDate.Text.Trim()),
                 Convert.ToDateTime(string.IsNullOrEmpty(txtExpDate.Text.Trim()) ? "01/01/1900" : txtExpDate.Text.Trim()),
                 Convert.ToDouble(string.IsNullOrEmpty(txtValue.Text.Trim()) ? "0" : txtValue.Text.Trim())
                 , int.Parse(DdlStatus.SelectedValue), TxtColor.Text.Trim());
             //upload file
             if (int.Parse(HiddenAssID.Value) > 0)
             {
                 AssetImageUpload(int.Parse(HiddenAssID.Value));
             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //bind depends on customer
        if (Request.QueryString["PContactId"] != null)
        {
            GridBindWithCantactId(int.Parse(Request.QueryString["PContactId"]), int.Parse(ddlCustomer.SelectedValue));
        }
        else
        {
            filldatabindassets(Convert.ToInt32(ddlCustomer.SelectedValue));
        }
        //filldatabindassets1();
        clear();
        btn_cancel.Visible = false;
        ImgBtnSubmit.Visible = true;
        ImgBtnUpdate.Visible = false;


    }
    protected void ImgBtnCancel_Click(object sender, EventArgs e)
    {
        clear();

        btn_cancel.Visible = false;
        ImgBtnSubmit.Visible = true;
        ImgBtnUpdate.Visible = false;
    }
    private bool DropValidations()
    {

        lblerror.Text = "";
        lblerror.Visible = true;
        bool myVal = false;
        if (txt_assetno.Text == "" && txt_serialno.Text == "")
        {

            lblerror.Text = "* Please enter the AssetsNo or Serialno";
            myVal = false;
        }
        else if ((dt_Site.SelectedIndex == 0) || (dt_make.SelectedIndex == 0) || (dt_Model.SelectedIndex == 0) || (dt_Type.SelectedIndex == 0))
        {
            lblerror.Text = "* Please select dropdown fields";

            myVal = false;
        }
        else
        {
            myVal = true;
        }

        return myVal;

    }
    public void DeleteAssetAssociation(int Aid)
    {
        using (AssetsToSoftwareDataContext Adc = new AssetsToSoftwareDataContext())
        {
            AssetAssociatedToContact a_new = Adc.AssetAssociatedToContacts.Where(a => a.AssetId == Aid).FirstOrDefault();
            Adc.AssetAssociatedToContacts.DeleteOnSubmit(a_new);
            Adc.SubmitChanges();
        }
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        lbl_Results.Visible = true;
        imgViewSoftware.Visible = false;
        int ID = int.Parse(sessionKeys.PortfolioID != 0 ? sessionKeys.PortfolioID.ToString() : ddlCustomer.SelectedValue);
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        DeleteAssetAssociation(int.Parse(id));
        string delete = "delete from Assets where ID='" + int.Parse(id) + "'";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(delete, con);
        try
        {
                   

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //AssetsToSoftware assetsToSoft =
            //  assetsReport.AssetsToSoftwares.Single(P => P.AssetsID == int.Parse(id));
            //assetsReport.AssetsToSoftwares.DeleteOnSubmit(assetsToSoft);
            //assetsReport.SubmitChanges();

        }
        catch (Exception e1)
        {
            LogExceptions.LogException(e1.Message);

        }
        finally
        {
            con.Close();
        }
        GridView1.EditIndex = -1;
        if (Request.QueryString["PContactId"] != null)
        {
            GridBindWithCantactId(int.Parse(Request.QueryString["PContactId"]), ID);
        }
        else
        {
            filldatabindassets(ID);
        }
        clear();

    }


    protected void lnkcsv_Click(object sender, EventArgs e)
    {

    }

    protected void imgbtnAssetsnum_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("DN_TestSerialNoSearch", con);
        SqlCommand cmd = new SqlCommand("DN_TestSerialNoSearchNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter portfolio = new SqlParameter("@portfolio", SqlDbType.Int, 32);
        portfolio.Value = int.Parse(ddlCustomer.SelectedValue);
        cmd.Parameters.Add(portfolio);
        SqlParameter assign0 = new SqlParameter("@serialno", SqlDbType.VarChar, 50);
        if (txt_serialno.Text == "")
        {

            assign0.Value = "";

        }
        else
        {
            assign0.Value = txt_serialno.Text;

        }
        cmd.Parameters.Add(assign0);
        SqlParameter assign1 = new SqlParameter("@AssetNo", SqlDbType.VarChar, 50);
        if (txt_assetno.Text == "")
        {

            assign1.Value = "";


        }
        else
        {
            assign1.Value = txt_assetno.Text;
        }

        cmd.Parameters.Add(assign1);
        SqlParameter assign5 = new SqlParameter("@ProjectReference", SqlDbType.VarChar, 50);


        if (Request.QueryString["Project"] != null)
        {
            assign5.Value = (Convert.ToInt32(Request.QueryString["Project"]).ToString());
        }
        else
        {
            assign5.Value = "";
        }


        //assign5.Value = "0";
        cmd.Parameters.Add(assign5);




        DataSet ds = new DataSet();

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblerror.Text = "Sorry no records were found. ";
            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            con.Close();
        }
        // clear();
        ImgBtnSubmit.Visible = true;
        ImgBtnUpdate.Visible = false;
        btn_cancel.Visible = false;
    }
    protected void imgbtnserialnum_Click(object sender, EventArgs e)
    {



        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DN_TestSerialNoSearchNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter portfolio = new SqlParameter("@portfolio", SqlDbType.Int, 32);
        portfolio.Value = int.Parse(ddlCustomer.SelectedValue);
        cmd.Parameters.Add(portfolio);
        SqlParameter assign0 = new SqlParameter("@serialno", SqlDbType.VarChar, 50);
        if (txt_serialno.Text == "")
        {

            assign0.Value = "";

        }
        else
        {
            assign0.Value = txt_serialno.Text;

        }
        cmd.Parameters.Add(assign0);
        SqlParameter assign1 = new SqlParameter("@AssetNo", SqlDbType.VarChar, 50);
        if (txt_assetno.Text == "")
        {

            assign1.Value = "";


        }
        else
        {
            assign1.Value = txt_assetno.Text;
        }

        cmd.Parameters.Add(assign1);
        SqlParameter assign5 = new SqlParameter("@ProjectReference", SqlDbType.VarChar, 50);


        if (Request.QueryString["Project"] != null)
        {
            assign5.Value = (Convert.ToInt32(Request.QueryString["Project"]).ToString());
        }
        else
        {
            assign5.Value = "";
        }


        //assign5.Value = "0";
        cmd.Parameters.Add(assign5);


        DataSet ds = new DataSet();

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {

            lblerror.Text = "Sorry no records were found. ";
            //lblerror.Text += ex.Message;
        }
        finally
        {
            con.Close();
        }
        // clear();

        ImgBtnSubmit.Visible = true;
        ImgBtnUpdate.Visible = false;
        btn_cancel.Visible = false;

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            try
            {
                Response.Redirect("WarrantyDocs.aspx?assetid=" + e.CommandArgument);
                //Response.Redirect("AssetSoftware.aspx?AssetID=" + e.CommandArgument);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        if (e.CommandName == "Selected")
        {

            try
            {
                clear();
                //imgViewSoftware.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                HiddenAssID.Value = ID.ToString();
                // GridView1.DataSource = ASAdminCls.dt_AdminAsset(ID);
                // GridView1.DataBind();

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("DN_EDITASSETSMoveNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 32).Value = int.Parse(HiddenAssID.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txt_serialno.Text = dr["SerialNo"].ToString();
                    //txt_serialno.ReadOnly = true;
                    //txt_assetno.ReadOnly = true;
                    HiddenField1.Value = dr["SerialNo"].ToString();
                    txt_assetno.Text = dr["AssetNo"].ToString();
                    HiddenField5.Value = dr["AssetNo"].ToString();
                    txt_Bulding.Text = dr["FromBuilding"].ToString();
                    txt_Room.Text = dr["FromRoom"].ToString();
                    txt_Owner.Text = dr["FromOwner"].ToString();
                    txt_Notes.Text = dr["FromNotes"].ToString();
                    txt_Floor.Text = dr["FromFloor"].ToString();
                    txt_WarrantyPeriod.Text = dr["FromPort"].ToString();
                    //txt_Technical.Text = dr["Technical"].ToString();
                    txtvlan.Text = dr["FromVLAN"].ToString();
                    txtsubnet.Text = dr["FromSubnet"].ToString();
                    txtipadress.Text = dr["FromIPAddress"].ToString();
                    txt_Cab.Text = dr["FromLocation"].ToString();
                    TxtColor.Text = dr["color"].ToString();

                    if (dr["StatusId"] == null)
                    {
                        DdlStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        DdlStatus.SelectedValue = dr["StatusId"].ToString();
                    }

                    if (dr["Type"] == null)
                    {
                        dt_Type.SelectedIndex = 0;
                    }
                    else
                    {
                        dt_Type.SelectedValue = dr["Type"].ToString();
                    }


                    if (Convert.ToDateTime(dr["Datecommision"]).ToShortDateString() == "01/01/1900")
                    {
                        txt_commision.Text = "";

                    }
                    else
                    {
                        txt_commision.Text = Convert.ToDateTime(dr["Datecommision"].ToString()).ToShortDateString();
                    }



                    if (Convert.ToDateTime(dr["Datemoved"]).ToShortDateString() == "01/01/1900")
                    {
                        txtScheduleMoveDate.Text = "";

                    }
                    else
                    {
                        txtScheduleMoveDate.Text = Convert.ToDateTime(dr["Datemoved"].ToString()).ToShortDateString();
                    }



                    if (dr["Make"] == null)
                    {
                        dt_make.SelectedIndex = 0;
                    }
                    else
                    {
                        dt_make.SelectedValue = dr["Make"].ToString();
                    }
                    if (dr["Model"]== null)
                    {
                        dt_Model.SelectedIndex = 0;
                    }
                    else
                    {
                        dt_Model.SelectedValue = dr["Model"].ToString();
                    }
                    //if (dr["Site"] == null)
                    //{
                    //    dt_Site.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    dt_Site.SelectedValue = dr["Site"].ToString();
                    //}
                    if (dr["VendorID"] == null)
                    {
                        ddlVendors.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlVendors.SelectedValue = dr["VendorID"].ToString();
                    }
                    if (Convert.ToDateTime(dr["PurchasedDate"]).ToShortDateString() == "01/01/1900")
                    {
                        txtPurchasedDate.Text = "";

                    }
                    else
                    {
                        txtPurchasedDate.Text = Convert.ToDateTime(dr["PurchasedDate"].ToString()).ToShortDateString();
                    }
                    if (Convert.ToDateTime(dr["ExpDate"]).ToShortDateString() == "01/01/1900")
                    {
                        txtExpDate.Text = "";

                    }
                    else
                    {
                        txtExpDate.Text = Convert.ToDateTime(dr["ExpDate"].ToString()).ToShortDateString();
                    }
                    if (dr["AssestValue"]== null)
                    {
                        txtValue.Text = "";

                    }
                    else
                    {
                        txtValue.Text = dr["AssestValue"].ToString();
                    }

                }
                dr.Close();
                con.Close();
                //set asset image
                setAssetImage(ID);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            btn_cancel.Visible = true;
            ImgBtnUpdate.Visible = true;
            ImgBtnSubmit.Visible = false;
        }
    }
    protected bool getVisible1(string Flag)
    {
        bool str1 = false;
        if ((Flag == "0") || (Flag == ""))
        {
            str1 = true;
        }
        return str1;

    }
   

    protected string ValidateData(string CheckValue)
    {
        string str1 = "";
        if ((CheckValue == "0") || (CheckValue == ""))
        {
            str1 = "**CheckInfo**";
        }
        else
        {
            str1 = CheckValue.ToString();
        }
        return str1;

    }

    public static string getInitCapital(string originalString)
    {
        try
        {
            string s = originalString;
            //s = s.ToLower();
            string e = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            string sProper = "";
            //if(s==e)
            foreach (Match m in Regex.Matches(s, e))
            {
                sProper += (m.Value[0]) + m.Value.Substring(1, m.Length - 1);
                // sProper = e;
            }
            return sProper;
        }
        catch
        {
            return "";
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCustomer.Visible = true;
        lblCustomer.Text = "Customer:" + ddlCustomer.SelectedItem ;
        sessionKeys.PortfolioID = (int.Parse(ddlCustomer.SelectedValue));
        filldatabindassets(int.Parse(ddlCustomer.SelectedValue));
       // bindAssignedSites(sessionKeys.PortfolioID);
        getDropDown.DdlBindSelect(dt_Site, string.Format("Select Distinct SiteID as ID,(select Site from Site where ID = AssignedSitesToPortfolio.SiteID) as Site from AssignedSitesToPortfolio where Portfolio ={0}", int.Parse(sessionKeys.PortfolioID != 0 ? sessionKeys.PortfolioID.ToString() : ddlCustomer.SelectedValue)), "ID", "Site", false, false, true);
    }

    public void AssetImageUpload(int assetid)
    {
        try
        {
            string fileName = string.Empty;
            //FileUpload1.HasFile
            string path = Server.MapPath("~/WF/UploadData/Assets");
            fileName = "\\" + fileName;
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
           
            if (FileUpload1.HasFile)
            {
                string fname = "asset_" + assetid;
                ImageManager.SaveImage_setpath(fname, FileUpload1.FileBytes, "Assets");
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void setAssetImage(int AssetID)
    {
        string path = Server.MapPath("~/WF/UploadData/Assets") + "\\ThumbNails\\asset_" + AssetID + ".png";
        if (File.Exists(path))
        {
            imgAsset.ImageUrl = path;
            imgAsset.ImageUrl = string.Format("~/WF/UploadData/Assets/ThumbNails/asset_{0}.png?t=",AssetID,DateTime.Now.Second);
        }
        else
        {
            imgAsset.ImageUrl = string.Format("~/WF/UploadData/Assets/ThumbNails/asset_{0}.png?t=", 0, DateTime.Now.Second);
        }
    }
    public static string GetImageUrl(int a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);

        ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
            }
        }
        else
        {
            eImageType = ImageManager.ImageType.OriginalData;
        }

        string path = "~/WF/UploadData/Assets/" + eImageType.ToString() + "/asset_" + a_gId.ToString() + ".png";

        if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            path = "~/WF/UploadData/Assets/" + eImageType.ToString() + "/asset_0.png?t=" + DateTime.Now.Second.ToString();
        }
        return path;

        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }
    public static string GetDatesDifferenceInDays(string StartDate,string EndDate)
    {
        double Nodays = 0;
        try
        {
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                //DateTime dstart = DateTime.Parse(StartDate);
                //DateTime dEnd = DateTime.Parse(EndDate);
                DateTime dstart = DateTime.Parse(EndDate);
                DateTime dEnd = DateTime.Now;

                Nodays = ( dstart - dEnd).TotalDays;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Nodays.ToString();
    }
    public bool CheckImageVisibility(int a_guid)
    {
        bool _visible = false;
        if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        {
            _visible = true;
        }
        return _visible;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridBindWithCantactId(0, sessionKeys.PortfolioID);
    }
}