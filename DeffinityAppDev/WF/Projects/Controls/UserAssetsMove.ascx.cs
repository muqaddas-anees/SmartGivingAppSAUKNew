using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using UserAssetsMovingSourecToDestination;
using AssetsImage_Manager;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
public partial class controls_UserAssetsMove : System.Web.UI.UserControl
{
    
    public int Project1 = 0;
    protected string  AssetID;
    public  string ID;
    AssetsMoving GetProperties = null;
    AssetsMoving ASAdminCls = new AssetsMoving();
    Guid _guid = Guid.NewGuid();
   
    DisBindings getDropDown = new DisBindings();

    private int assestproject;
    private int assetType_assign;
    private int assign_project_incident;
    private int assigntypenumber;
    private int getProtfoliyo;
    public int AssestProject
    {
        get { return assestproject; }
        set { assestproject = value; }
    }
    public int AssetType_Assign
    {
        get { return assetType_assign; }
        set { assetType_assign = value; }
    }
    public int Assign_Project_Incident
    {
        get { return assign_project_incident; }
        set { assign_project_incident = value; }
    }

    public int AssignTypenumber
    {
        get { return assigntypenumber; }
        set { assigntypenumber = value; }
    }

    public int GetProtfoliyo
    {
        get { return getProtfoliyo; }
        set { getProtfoliyo = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

     Project1 = AssestProject;
        
        
        try
        {
            if (!this.IsPostBack)
            {
                fillgrid();
                BindDropdown();
                
            }
            // getValue();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindDropdown()
    {

        try
        {
            getDropDown.DdlBindSelect(dt_Model, "select ModelID,Model from Assetsmodel", "ModelID", "Model", false, false, true);
            getDropDown.DdlBindSelect(dt_make, "select MakeID,Make from Assetsmake", "MakeID", "Make", false, false, true);
            getDropDown.DdlBindSelect(dt_Type, "select TypeID,Type from Assetstype", "TypeID", "Type", false, false, true);

            if (Project1 == 0)
            {
                getDropDown.DdlBindSelect(dt_Site, "select site.ID as ID,Site from site inner join AssignedSitesToPortfolio on Site.ID=AssignedSitesToPortfolio.SiteID where AssignedSitesToPortfolio.portfolio='" + getProtfoliyo + "'", "ID", "Site", false, false, true);
                getDropDown.DdlBindSelect(dt_deatinationSite, "select site.ID as ID,Site from site inner join AssignedSitesToPortfolio on Site.ID=AssignedSitesToPortfolio.SiteID where AssignedSitesToPortfolio.portfolio='" + getProtfoliyo + "'", "ID", "Site", false, false, true);
                      
            }
            else
            {
                getDropDown.DdlBindSelect(dt_Site, "select site.ID as ID,Site from site inner join AssignedSitesToPortfolio on Site.ID=AssignedSitesToPortfolio.SiteID where AssignedSitesToPortfolio.portfolio=(select portfolio from projects where projectreference=" + Project1.ToString() + ")", "ID", "Site", false, false, true);
                getDropDown.DdlBindSelect(dt_deatinationSite, "select site.ID as ID,Site from site inner join AssignedSitesToPortfolio on Site.ID=AssignedSitesToPortfolio.SiteID where AssignedSitesToPortfolio.portfolio=(select portfolio from projects where projectreference=" + Project1.ToString() + ")", "ID", "Site", false, false, true);
                  
            }
             
                
            //getDropDown.DdlBindSelect(AssetsType, "select ID,AssetType from AssetType", "ID", "AssetType", false, false, true);
                }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void btn_searchAssets_Click(object sender, EventArgs e)
    {
        SearchGrid();
    }

    public void SearchGrid()
    {
        GetProperties = new AssetsMoving();
        GetProperties.Attribute = Convert.ToInt32(ddlattribute.SelectedValue);
        GetProperties.TextValue = txtAttributeText.Text;

        try
        {
            AssetsMoving DisplayGrid = null;
            DisplayGrid = new AssetsMoving();
            SearchGridPanel.Visible = true;

            if (DisplayGrid.GetSearchGrid(GetProperties.Attribute, GetProperties.TextValue,assigntypenumber).Tables[0].Rows.Count > 0)
            {
                btnAssign.Visible = true;
                GridView2.DataSource = DisplayGrid.GetSearchGrid(GetProperties.Attribute, GetProperties.TextValue, assigntypenumber);
                GridView2.DataBind();
            }
            else
            {
                btnAssign.Visible = false;
                GridView2.DataSource = DisplayGrid.GetSearchGrid(GetProperties.Attribute, GetProperties.TextValue, assigntypenumber);
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgbtnAssetsnum_Click(object sender, EventArgs e)
    {

        AssetsMoving GetSearchAssets = new AssetsMoving();
        AssetsMoving GetSearchAssetsProperties = new AssetsMoving();
        GetSearchAssetsProperties.assetno = txt_assetno.Text;
        GetSearchAssetsProperties.serialno = txt_serialno.Text;

        try
        {
            DataTable dt = new DataTable();

            dt = GetSearchAssets.GetAssetsValues(GetSearchAssetsProperties.assetno, GetSearchAssetsProperties.serialno,0);
            if (dt.Rows.Count == 0)
            {
                lblerror.Visible = true;
                lblerror.Text = "Asset not found.Please check and try again.";
            }
            else
            {

                txt_Room.Text = dt.Rows[0]["FromRoom"].ToString();
                sourceIPAdress.Text = dt.Rows[0]["FromIPAddress"].ToString();
                sourcetxtsubnet.Text = dt.Rows[0]["FromSubnet"].ToString();
                sourceVLAN.Text = dt.Rows[0]["FromVLAN"].ToString();
                txt_DataPort.Text = dt.Rows[0]["FromPort"].ToString();
                txt_assetno.Text = dt.Rows[0]["AssetNo"].ToString();
                HiddenField2.Value = txt_assetno.Text;
                txt_serialno.Text = dt.Rows[0]["SerialNo"].ToString();
                HiddenField3.Value = txt_serialno.Text;
                txt_Bulding.Text = dt.Rows[0]["FromBuilding"].ToString();
                txt_Cab.Text = dt.Rows[0]["FromLocation"].ToString(); ;
                txt_Owner.Text = dt.Rows[0]["FromOwner"].ToString();
                txt_Floor.Text = dt.Rows[0]["FromFloor"].ToString();////System.Data.SqlTypes.SqlDateTime.Null;//
                ID = dt.Rows[0]["ID"].ToString();
                //projcet = Convert.ToInt32(dt.Rows[0]["ProjectReference"].ToString());
                if (dt.Rows[0]["Datecommision"].ToString() == "")
                {
                    txt_commision.Text = "";

                }
                else
                {
                    txt_commision.Text = Convert.ToDateTime(dt.Rows[0]["Datecommision"].ToString()).ToShortDateString();
                }

                if (dt.Rows[0]["Datemoved"].ToString() == "")
                {
                    txt_DateMoved.Text = "";
                }
                else
                {
                    txt_DateMoved.Text = Convert.ToDateTime(dt.Rows[0]["Datemoved"].ToString()).ToShortDateString();
                }



                if ((dt.Rows[0]["Make"].ToString() == "") || (dt.Rows[0]["Make"].ToString() == "0"))
                {
                    dt_make.SelectedIndex = 0;
                }
                else
                {
                    dt_make.SelectedValue = dt.Rows[0]["Make"].ToString();
                }
                if ((dt.Rows[0]["Model"].ToString() == "") || (dt.Rows[0]["Model"].ToString() == "0"))
                {
                    dt_Model.SelectedIndex = 0;
                }
                else
                {
                    dt_Model.SelectedValue = dt.Rows[0]["Model"].ToString();
                }
            

                if ((dt.Rows[0]["Type"].ToString() == "") || (dt.Rows[0]["Type"].ToString() == "0"))
                {

                    dt_Type.SelectedIndex = 0;

                }
                else
                {

                    dt_Type.SelectedValue = dt.Rows[0]["Type"].ToString();
                }



                if ((dt.Rows[0]["AssetsTypeID"].ToString() == "") || (dt.Rows[0]["AssetsTypeID"].ToString() == "0"))
                {

                    //AssetsType.SelectedIndex = 0;

                }
                else
                {

                    //AssetsType.SelectedValue = dt.Rows[0]["AssetsTypeID"].ToString();
                }


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }
    public void fillgrid()
    {
        //int AssetID = ID;
        HiddenField1.Value = ID;
        try
        {

            if (ASAdminCls.dt_ProjectAssets(Project1,assetType_assign,assign_project_incident ).Rows.Count > 0)
            {
                btnAssign.Visible = true;
                GridView1.DataSource = ASAdminCls.dt_ProjectAssets(Project1, assetType_assign, assign_project_incident);
                //Bind the tables to the datagrid
                GridView1.DataBind();
            }
            else
            {
                btnAssign.Visible = false;
                GridView1.DataSource = ASAdminCls.dt_ProjectAssets(Project1, assetType_assign, assign_project_incident);
                //Bind the tables to the datagrid
                GridView1.DataBind();
            }


         

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }



    public void fillListviewGrid()
    {
        HiddenField1.Value = ID;
        try
        {

            if (ASAdminCls.dt_ProjectAssets(Project1, assetType_assign, assign_project_incident).Rows.Count > 0)
            {
                btnAssign.Visible = true;
               // GridView5.DataSource = ASAdminCls.dt_ProjectAssets(Project1, assetType_assign, assign_project_incident);
                //Bind the tables to the datagrid
                //GridView5.DataBind();
            }
            else
            {
                btnAssign.Visible = false;
               // GridView5.DataSource = ASAdminCls.dt_ProjectAssets(Project1, assetType_assign, assign_project_incident);
                //Bind the tables to the datagrid
                //GridView5.DataBind();
            }




        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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

    protected bool getVisible(string Flag)
    {
        bool str1 = false;
        if (Flag == "1")
        {
            str1 = true;
        }

        return str1;

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

    protected void imagemake_Click(object sender, EventArgs e)
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
        //txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = false;
        imodel_submitt.Visible = false;
       // ilocation_submitt.Visible = false;

        imodel_cancel.Visible = false;
      //  ilocation_cancel.Visible = false;
        itype_cancel.Visible = false;
        i_makecancel.Visible = true;
        i_makesubmitt.Visible = true;

    }
    protected void i_makecancel_Click(object sender, EventArgs e)
    {
        dt_make.Visible = true;
        txtmkae.Visible = false;
        imagemake.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;
        //  i_makesubmitt.Visible = false;
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
      //  txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = false;
        imodel_submitt.Visible = true;
      //  ilocation_submitt.Visible = false;

        imodel_cancel.Visible = true;
       // ilocation_cancel.Visible = false;
        itype_cancel.Visible = false;
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
       // txt_site.Visible = false;

        //button for submitt and cancel

        itype_submitt.Visible = true;
        imodel_submitt.Visible = false;
     //   ilocation_submitt.Visible = false;

        imodel_cancel.Visible = false;
      //  ilocation_cancel.Visible = false;
        itype_cancel.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;

    }
    protected void itype_cancel_Click(object sender, EventArgs e)
    {
        dt_Type.Visible = true;
        txt_type.Visible = false;
        imagetype.Visible = true;
        itype_submitt.Visible = false;
        itype_cancel.Visible = false;
    }
    protected void imagelocation_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Locations.aspx?Project=" + Project1.ToString() + "&type=projectasset");

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        AssetsMoving GetAssignProjects = new AssetsMoving();
        try
        {
            int id;
            bool chk = false;
            bool chk_new = false;
            try
            {

                foreach (GridViewRow row in GridView2.Rows)
                {
                    //string s = row.Cells[0].Controls[1].ToString();
                    CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                    id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                    if (chkNew.Checked)
                    {
                        //AssigntoProject(id);
                          int i=0;
                          if (Project1 == 0)
                          {
                              i = Convert.ToInt32(GetAssignProjects.AssigntoProjectToMove(id, Project1, sessionKeys.UID, assign_project_incident, assetType_assign));
                          }
                          else
                          {
                              i = Convert.ToInt32(GetAssignProjects.AssigntoProjectToMove(id, Project1, sessionKeys.UID, assign_project_incident, assetType_assign));
                          }

                   
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
         
            fillgrid();
          SearchGridPanel.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    protected void Imgupdation_Click(object sender, EventArgs e)
    {
        AssetsMoving GetPortfolio11 = new AssetsMoving();
        AssetsMoving GetPermision = new AssetsMoving();
        AssetsMoving GetDocumentUpload = new AssetsMoving();
        AssetsMoving PassAssestImage = new AssetsMoving();
   
        if (!PermissionManager.IsPermitted(AssestProject, Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.AddAssets))
        {
            lblerror.Text = "User doesn't have rights to Add Assets";
            return;
        }
        if (!CheckForEmpty(txt_assetno.Text.Trim(), txt_serialno.Text.Trim()))
        {
            lblerror.Text = "Please enter Asset number or Serial number";
        }
        else
        {

            int insertStatus = ASAdminCls.checkexistsAssets(txt_serialno.Text.Trim(), txt_assetno.Text.Trim());
            if (insertStatus == 0)
            {
                int getprotfolio1=0;
                if (Project1.ToString() == "0")
                {
                    //GetPortfilioID
                 getprotfolio1 = getProtfoliyo; 

                }
                else
                {
                    getprotfolio1 = GetPortfolio11.GetPortfilioID(Convert.ToInt32(Project1));
                }
                int GetDocumentID = 0;
                

                int GetAssetsID=InsertorUpdateAssests(0,getprotfolio1);
                    //Here We need to Inert the Image.....................................



                if (GetAssetsID > 0)
                {
                      if (FileUpload1.HasFile)
                        {
                            Guid _guid = Guid.NewGuid();
                            AssetsImageManager.Assets_SaveImage(_guid, FileUpload1.FileBytes);
                            PassAssestImage.ImageInsertion(GetAssetsID, _guid);


                        }
                      bool test111 = true;
                  
                    if (FileUpload2.HasFile)
                        {
                        HttpFileCollection hfc = Request.Files;
                      foreach (string h in hfc.AllKeys)
                        {
                            if (h.Contains("FileUpload1"))
                            {
                                
                                test111 = false;
                                
                            }

                        }
                        int DocID = 0;
                       
                        if (test111 == false)
                        {
                            DocID = 1;
                        }


                        for (int i = DocID; i < hfc.Count; i++)
                        {
                            HttpPostedFile hpf = hfc[i];



                            if (hpf.ContentLength > 0)
                            {


                                HttpPostedFile myFile = hpf;

                                string strFilename = System.IO.Path.GetFileName(hpf.FileName);
                                Stream DocumentStream = myFile.InputStream;
                                int documentlenth = myFile.ContentLength;
                                string imgContentType = myFile.ContentType;
                                byte[] imgBinaryData = new byte[documentlenth];
                                byte[] OrizinalimgBinaryData = new byte[documentlenth];
                                int GetDoc = DocumentStream.Read(imgBinaryData, 0, documentlenth);
                                GetDocumentID = GetDocumentUpload.WriteToDB(strFilename, myFile.ContentType, ref imgBinaryData, ref OrizinalimgBinaryData, documentlenth, GetAssetsID);
                                DocumentStream.Flush();
                                DocumentStream.Close();


                                myFile.InputStream.Flush();
                                myFile.InputStream.Close();

                            }





                        }

                    }
                }




                    fillgrid();
                Clear_Fields();
                
                
      

            }
            else
            {
                lblerror.Text = "Please check Asset already exist";
            }
        }
    }





    private bool CheckForEmpty(string str1, string str2)
    {
        bool Val = false;
        if (str1 != string.Empty)
        {
            Val = true;
        }
        else if (str2 != string.Empty)
        {
            Val = true;
        }
        return Val;
    }

    public int InsertorUpdateAssests(int ID,int getprotfolio1)
    {
        int GetAssetsID = 0;
        if (ID == 0)
        {

            string MakeID = "";
            string ModelID = "";
            string TypeID = "";

            if (txtmkae.Text != "")
            {
                MakeID = "";

            }
            else
            {
                MakeID = getDropDown.getDdlval(dt_make.SelectedItem.Value).ToString();
            }


            if (txt_model.Text != "")
            {
                ModelID ="" ;
            }

            else
            {
                ModelID = (dt_Model.SelectedItem.Value).ToString();
            }

            if (txt_type.Text != "")
            {
                TypeID = "";
            }
            else
            {
                TypeID = (dt_Type.SelectedItem.Value).ToString();
            }



            try
            {



                string[] nwAsset = new string[] { 
                txt_serialno.Text.Trim(),
                txt_assetno.Text.Trim(), 
                Project1.ToString(),
                getDropDown.getDdlval(dt_make.SelectedItem.Value).ToString(),
                getDropDown.getDdlval(dt_Model.SelectedItem.Value).ToString(), 
                getDropDown.getDdlval(dt_Type.SelectedValue).ToString(), 
                getDropDown.getDdlval(dt_Site.SelectedValue).ToString(),
                txt_Bulding.Text.Trim(),
                txt_Floor.Text.Trim(), 
                txt_Room.Text.Trim(),
                txt_Cab.Text.Trim(), //This is From location
               txt_DataPort.Text.Trim(),
                txt_Owner.Text.Trim(),
                "",//from Noates not using
                "", //Technical Not using
                sourceVLAN.Text.Trim(),
                sourceIPAdress.Text.Trim(),
                sourcetxtsubnet.Text.Trim(), 
                "1", //new AssetID
                sessionKeys.UID.ToString(),//User ID
                txt_DateMoved.Text.Trim(),
                txt_commision.Text.Trim(),
                getDropDown.getDdlval(dt_deatinationSite.SelectedValue).ToString(),
                txt_tobuilding.Text.Trim(),
                txt_tofloor.Text.Trim(), 
                txt_toRoom.Text.Trim(),
                txt_ToDesklocation.Text.Trim(), 
                txtipadress.Text.Trim(),
                txtsubnet.Text.Trim(), 
                txt_toDataport.Text.Trim(),
                txt_Notes.Text.Trim(),
                NewOwner.Text.Trim(),
                txtvlan.Text.Trim(),
                getDropDown.getDdlval(dt_Type.SelectedValue).ToString(), 
                assign_project_incident.ToString(), 
                assetType_assign.ToString(),
                getprotfolio1.ToString(),
                 "false"
                };
                GetAssetsID = ASAdminCls.NewAssetSubmitBtn_Click(nwAsset,0);
               



            

            }
            catch (Exception ex)
            {
             LogExceptions.WriteExceptionLog(ex);
            }

            return GetAssetsID;
        }
        else
        {

            try
            {

               

                string[] nwAsset1 = new string[] { 
                txt_serialno.Text.Trim(),
                txt_assetno.Text.Trim(), 
                Project1.ToString(),
                getDropDown.getDdlval(dt_make.SelectedItem.Value).ToString(),
                getDropDown.getDdlval(dt_Model.SelectedItem.Value).ToString(), 
                getDropDown.getDdlval(dt_Type.SelectedValue).ToString(), 

                getDropDown.getDdlval(dt_Site.SelectedValue).ToString(),
                txt_Bulding.Text.Trim(),
                txt_Floor.Text.Trim(), 
                txt_Room.Text.Trim(),
                txt_Cab.Text.Trim(), //This is From location
                txt_DateMoved.Text.Trim(),
                txt_commision.Text.Trim(),
                 txt_DataPort.Text.Trim(),
                txt_Owner.Text.Trim(),
                sourceVLAN.Text.Trim(),
                sourceIPAdress.Text.Trim(),
                sourcetxtsubnet.Text.Trim(), 
               
                getDropDown.getDdlval(dt_deatinationSite.SelectedValue).ToString(),
                txt_tobuilding.Text.Trim(),
                txt_tofloor.Text.Trim(), 
                txt_toRoom.Text.Trim(),
                txt_ToDesklocation.Text.Trim(), 
                txtipadress.Text.Trim(),
                txtsubnet.Text.Trim(), 
                txt_toDataport.Text.Trim(),
                txt_Notes.Text.Trim(),
                NewOwner.Text.Trim(),
                txtvlan.Text.Trim(),
                getDropDown.getDdlval(dt_Type.SelectedValue).ToString(), 
               
                ID.ToString(),
                 "false"
                };
                ASAdminCls.updategridviewdestination(nwAsset1);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return GetAssetsID;
        }

    }



    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {


            AssetsMoving GetSearchAssets = new AssetsMoving();


            //pnl.Visible = true;
            ImageGet.Visible = true;
            GetDocmentID.Visible = true;

                try
                {

                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    HiddenField4.Value = ID.ToString();
                    DataTable GetTable = new DataTable();

                    #region Display Images and Documents
                    GetImageGrid(ID);
                    bindgrid(ID);

                    #endregion
                    GetTable = GetSearchAssets.GetAssetsValues("", "", ID);
                    if (GetTable.Rows.Count == 0)
                    {


                    }
                    else
                    {

                        #region Source Display
                        txt_Room.Text = GetTable.Rows[0]["FromRoom"].ToString();
                        sourceIPAdress.Text = GetTable.Rows[0]["FromIPAddress"].ToString();
                        sourcetxtsubnet.Text = GetTable.Rows[0]["FromSubnet"].ToString();
                        sourceVLAN.Text = GetTable.Rows[0]["FromVLAN"].ToString();
                        txt_DataPort.Text = GetTable.Rows[0]["FromPort"].ToString();
                        txt_assetno.Text = GetTable.Rows[0]["AssetNo"].ToString();
                        HiddenField2.Value = txt_assetno.Text;
                        txt_serialno.Text = GetTable.Rows[0]["SerialNo"].ToString();
                        HiddenField3.Value = txt_serialno.Text;
                        txt_Bulding.Text = GetTable.Rows[0]["FromBuilding"].ToString();
                        txt_Cab.Text = GetTable.Rows[0]["FromLocation"].ToString(); ;
                        txt_Owner.Text = GetTable.Rows[0]["FromOwner"].ToString();
                        txt_Floor.Text = GetTable.Rows[0]["FromFloor"].ToString();////System.Data.SqlTypes.SqlDateTime.Null;//
                        //ID = GetTable.Rows[0]["ID"].ToString();
                        //projcet = Convert.ToInt32(dt.Rows[0]["ProjectReference"].ToString());
                        if (GetTable.Rows[0]["Datecommision"].ToString() == "")
                        {
                            txt_commision.Text = "";

                        }
                        else
                        {
                            txt_commision.Text = Convert.ToDateTime(GetTable.Rows[0]["Datecommision"].ToString()).ToShortDateString();
                        }

                        if (GetTable.Rows[0]["Datemoved"].ToString() == "")
                        {
                            txt_DateMoved.Text = "";
                        }
                        else
                        {
                            txt_DateMoved.Text = Convert.ToDateTime(GetTable.Rows[0]["Datemoved"].ToString()).ToShortDateString();
                        }



                        if ((GetTable.Rows[0]["Make"].ToString() == "") || (GetTable.Rows[0]["Make"].ToString() == "0"))
                        {
                            dt_make.SelectedIndex = 0;
                        }
                        else
                        {
                            dt_make.SelectedValue = GetTable.Rows[0]["Make"].ToString();
                        }
                        if ((GetTable.Rows[0]["Model"].ToString() == "") || (GetTable.Rows[0]["Model"].ToString() == "0"))
                        {
                            dt_Model.SelectedIndex = 0;
                        }
                        else
                        {
                            dt_Model.SelectedValue = GetTable.Rows[0]["Model"].ToString();
                        }


                        if ((GetTable.Rows[0]["Type"].ToString() == "") || (GetTable.Rows[0]["Type"].ToString() == "0"))
                        {

                            dt_Type.SelectedIndex = 0;

                        }
                        else
                        {

                            dt_Type.SelectedValue = GetTable.Rows[0]["Type"].ToString();
                        }



                        if ((GetTable.Rows[0]["AssetsTypeID"].ToString() == "") || (GetTable.Rows[0]["AssetsTypeID"].ToString() == "0"))
                        {

                            //AssetsType.SelectedIndex = 0;

                        }
                        else
                        {

                            //AssetsType.SelectedValue = GetTable.Rows[0]["AssetsTypeID"].ToString();
                        }

                        if ((GetTable.Rows[0]["FromSite"].ToString() == "") || (GetTable.Rows[0]["FromSite"].ToString() == "0"))
                        {

                            dt_Site.SelectedIndex = 0;

                        }
                        else
                        {

                            dt_Site.SelectedValue = GetTable.Rows[0]["FromSite"].ToString();
                        }


                        #endregion


                        #region Destination Display
                     



                        if ((GetTable.Rows[0]["ToSite"].ToString() == "") || (GetTable.Rows[0]["ToSite"].ToString() == "0"))
                        {
                           
                            dt_deatinationSite.SelectedIndex = 0;

                        }
                        else
                        {

                            dt_deatinationSite.SelectedValue = GetTable.Rows[0]["ToSite"].ToString();
                        }

                        NewOwner.Text = GetTable.Rows[0]["ToOwner"].ToString();

                        txt_toDataport.Text = GetTable.Rows[0]["ToPort"].ToString();
                        txt_ToDesklocation.Text = GetTable.Rows[0]["ToLocation"].ToString();
                        txt_tobuilding.Text = GetTable.Rows[0]["ToBuilding"].ToString();
                        txt_tofloor.Text = GetTable.Rows[0]["ToFloor"].ToString();
                        txt_toRoom.Text = GetTable.Rows[0]["ToRoom"].ToString();
                        txtipadress.Text = GetTable.Rows[0]["ToIPAddress"].ToString();
                        txtsubnet.Text = GetTable.Rows[0]["ToSubnet"].ToString();
                        txtvlan.Text = GetTable.Rows[0]["ToVlan"].ToString();
                        txt_Notes.Text = GetTable.Rows[0]["ToNotes"].ToString();
                      
                        #endregion

                       


                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                //ImgBtnUpdate.Visible = true;
                btn_Update.Visible = true;
                btnCancel.Visible = true;
                //ImgBtnCancel.Visible = true;
                //Imgupdation.Visible = false;
                btn_Add.Visible = false;
         






        }


    }

   
    public void Clear_Fields()
    {
        txt_assetno.Text = "";
        txt_serialno.Text = "";

        //AssetsType.SelectedIndex = 0;
        dt_make.SelectedIndex = 0;
        dt_Model.SelectedIndex = 0;
        dt_Type.SelectedIndex = 0;
        dt_Site.SelectedIndex = 0;
        txt_commision.Text = "";
        txt_DateMoved.Text = "";
        txt_Owner.Text = "";
        txt_DataPort.Text = "";
        txt_Cab.Text = "";
        txt_Bulding.Text = "";
        txt_Floor.Text = "";
        txt_Room.Text = "";
        sourceIPAdress.Text = "";
        sourcetxtsubnet.Text = "";
        sourceVLAN.Text = "";

        //Destination fieds....


        dt_deatinationSite.SelectedIndex = 0;
        NewOwner.Text = "";
       // txt_ScheduleMoveDate.Text = "";
        txt_toDataport.Text = "";
        txt_ToDesklocation.Text = "";
        txt_tobuilding.Text = "";
        txt_tofloor.Text = "";
        txt_toRoom.Text = "";
        txtipadress.Text = "";
        txtsubnet.Text = "";
        txtvlan.Text = "";
        txt_Notes.Text = "";

    }




    public void GetImageGrid(int GetImageID)
    {
        AssetsMoving GetImageTodisplay = new AssetsMoving();
        DataTable dt = GetImageTodisplay.FetchAllImages(GetImageID);
        if (dt.Rows.Count > 0)
        {
            GetImage.Visible = true;
            GetImage.DataSource = dt;
            GetImage.DataBind();
        }
        else
        {
            GetImage.Visible = false;
        }


    }

    protected void ImgBtnUpdate_Click(object sender, EventArgs e)
    {

        AssetsMoving GetPortfolio11 = new AssetsMoving();
        AssetsMoving GetAssetsImageID = new AssetsMoving();
        AssetsMoving GetDocumentUpload = new AssetsMoving();
        AssetsMoving GetDocumentGetID = new AssetsMoving();
        AssetsMoving PassAssestImage = new AssetsMoving();
     
        int GetDocumentID = 0;
        
        int AssetsTESTID = 0;
        AssetsTESTID = Convert.ToInt32(HiddenField4.Value);

        //Here I am getting the Assets Image ID.

      
       // GetImageID = GetAssetsImageID.GetAssetsImageID(AssetsTESTID);

        //Here we are getting the Assets Documnets ID.

        int DocumentAssetsID = 0;
        DocumentAssetsID = GetDocumentGetID.GetAssetsDocumnetID(AssetsTESTID);


       


       
       
         InsertorUpdateAssests(AssetsTESTID, 0);

         #region ImageUpdate while Updating

         if (FileUpload1.HasFile)
         {
             string fileName = FileUpload1.PostedFile.FileName.ToString();

             string imgExtsn = fileName.Substring(fileName.LastIndexOf("."));
             imgExtsn = imgExtsn.ToLower();
             string imgType = FileUpload1.PostedFile.ContentType;
             int imgLen = FileUpload1.PostedFile.ContentLength;
             byte[] imgData = new byte[imgLen];
             Stream imgStream = FileUpload1.PostedFile.InputStream;
             int n = imgStream.Read(imgData, 0, imgLen);
             if ((imgExtsn == ".jpg") || (imgExtsn == ".bmp") || (imgExtsn == ".gif") || (imgExtsn == ".png"))
             {

                 if (FileUpload1.HasFile)
                 {
                     Guid _guid = Guid.NewGuid();
                     
                     AssetsImageManager.Assets_SaveImage(_guid, FileUpload1.FileBytes);
                     PassAssestImage.ImageInsertion(AssetsTESTID, _guid);


                 }

             }
             else
             {
                 lblerror.Text = "Please Select image formates png,Jpg,Gif and Bmp.";
             }

         }
      

         #endregion


         #region UpdateDoc or Insert DOC
         if (FileUpload2.HasFile)
        {
            bool checkcondition=true;
            HttpFileCollection hfc = Request.Files;

            foreach (string h in hfc.AllKeys)
            {
                if (h.Contains("FileUpload1"))
                {

                    checkcondition = false;

                }

            }
            int DocID = 0;

            if (checkcondition == false)
            {
                DocID = 1;
            }




            for (int i = DocID; i < hfc.Count; i++)
           {
               HttpPostedFile hpf = hfc[i];
               if (hpf.ContentLength > 0)
               {
                 

                   HttpPostedFile myFile = hpf;
                   string strFilename = System.IO.Path.GetFileName(hpf.FileName);
                   Stream DocumentStream = myFile.InputStream;
                   int documentlenth = myFile.ContentLength;
                   string imgContentType = myFile.ContentType;
                   byte[] imgBinaryData = new byte[documentlenth];
                   byte[] OrizinalimgBinaryData = new byte[documentlenth];
                   int GetDoc = DocumentStream.Read(imgBinaryData, 0, documentlenth);
                   GetDocumentID = GetDocumentUpload.WriteToDB(strFilename, myFile.ContentType, ref imgBinaryData, ref OrizinalimgBinaryData, documentlenth, AssetsTESTID);
                   DocumentStream.Flush();
                   DocumentStream.Close();


                   myFile.InputStream.Flush();
                   myFile.InputStream.Close();
               }
           }


        }
#endregion

          GridView1.EditIndex = -1;
        fillgrid();
        Clear_Fields();
        ImageGet.Visible = false;
        ImgBtnUpdate.Visible = false;
        ImgBtnCancel.Visible = false;
        Imgupdation.Visible = true;
        //////
        btn_Add.Visible = true;
         GetDocmentID.Visible = false;
        

    }


    
    protected void ImgBtnCancel_Click(object sender, EventArgs e)
    {
        Clear_Fields();
        ImageGet.Visible = false;
        ImgBtnUpdate.Visible = false;
        btn_Update.Visible = false;
        btnCancel.Visible = false;
        ImgBtnCancel.Visible = false;
        Imgupdation.Visible = true;
        ///////
        btn_Add.Visible = true;
        GetDocmentID.Visible = false;
    }
    //protected void GetDisplay_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (GetDisplay.SelectedValue == "1")
    //    {
    //        pnl.Visible = true;
    //        ListPannel.Visible = false;
    //    }
    //    else
    //    {
    //       pnl.Visible = false;
    //        ListPannel.Visible = true;
    //        fillListviewGrid();
    //    }
       
    //}
    protected void GetImage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in GetImage.Rows)
        {

            Image img = (Image)row.FindControl("img1");

            if (img != null)
            {

                img.Attributes.Add("onmouseover", "Large(this)");

                img.Attributes.Add("onmouseout", "Out(this)");

                img.Attributes.Add("onmousemove", "Move(this,event)");
            }



        }

    }

    #region DocumentDisplay and Upload
    public void bindgrid(int AssetsDocumentID)
    {


        AssetsMoving GetImageTodisplay = new AssetsMoving();
        DataTable dt = GetImageTodisplay.FetchAllDocumentsofAssets(AssetsDocumentID);
        if (dt.Rows.Count > 0)
        {
            GridView3.Visible = true;
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        else
        {
            GridView3.Visible = false;
        }
      
    }
    #endregion





    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int AssetsDocumentID = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "Remove")
        {
           
           
            AssetsDocumentRemove(AssetsDocumentID);
            bindgrid(Convert.ToInt32(HiddenField4.Value));
           // GetDocmentID.Visible = true;
        }

        if (e.CommandName == "DownLoad")
        {
            AssetsMoving Download = new AssetsMoving();
            Download.DownLoadDoc(AssetsDocumentID.ToString());
            bindgrid(AssetsDocumentID);
        }
    }


    public void AssetsDocumentRemove(int AssetsDocID)
    {
        int GetResults = 0;
         AssetsMoving RemoveDoc = new AssetsMoving();
        GetResults= RemoveDoc.RemoveAssetsDocuments(AssetsDocID);
    }
    

    public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);

        // ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        AssetsImageManager.ImageType AssetsImageType = AssetsImageManager.ImageType.OriginalData;

        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: AssetsImageType = AssetsImageManager.ImageType.ThumbNails; break;

                 //case AssetsImageManager.ThumbnailSize.MediumSmaller:AssetsImageType=AssetsImageManager.ImageType.ThumbNails:break;;
            }
        }
        else
        {
            AssetsImageType = AssetsImageManager.ImageType.OriginalData;
        }

        return "~/WF/UploadData/Assets/" + AssetsImageType.ToString() + "/" + a_gId.ToString() + ".png";
        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
        
    }
    public bool CheckImageVisibility(Guid a_guid)
    {
        bool _visible = false;
        if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        {
            _visible = true;
        }
        return _visible;
    }



    protected void i_makesubmitt_Click(object sender, EventArgs e)
    {
        int _outvalue = 0;
         AssetsMoving GetMakeType = new AssetsMoving();
         _outvalue=GetMakeType.AddMakeType(txtmkae.Text);

        if (_outvalue == 0)
        {

            lblerror.Visible = true;
           // lblerror.Text = " Please Check Make name already exist";
        }
        BindDropdown();
        //        fillmakelist();
        imagemake.Visible = true;
        i_makecancel.Visible = false;
        i_makesubmitt.Visible = false;
        dt_make.Visible = true;
        txtmkae.Visible = false;
    }
    protected void imodel_submitt_Click(object sender, EventArgs e)
    {
          AssetsMoving GetAssetsModel = new AssetsMoving();
          int _outvalue = GetAssetsModel.AddModelType(txt_model.Text);


        if (_outvalue == 0)
        {
           // lblerror.Text = " Please Check Model Already Exists";
        }
       
        BindDropdown();
        //fillmodel();
        imodel_submitt.Visible = false;
        imodel_cancel.Visible = false;
        dt_Model.Visible = true;
        txt_model.Visible = false;
        imagemodel.Visible = true;
    }
    protected void itype_submitt_Click(object sender, EventArgs e)
    {
       
        AssetsMoving GetAssetsType = new AssetsMoving();
        GetAssetsType.AddAsstesType(txt_type.Text);
        //AddAsstesType
        BindDropdown();
        //typefill();
        txt_type.Visible = false;
        itype_cancel.Visible = false;
        itype_submitt.Visible = false;
        dt_Type.Visible = true;
        imagetype.Visible = true;
    }
    protected void btn_copy_Click(object sender, EventArgs e)
    {
        dt_deatinationSite.SelectedValue = dt_Site.SelectedValue;

        NewOwner.Text = txt_Owner.Text;
        txt_toDataport.Text = txt_DataPort.Text;
        txt_ToDesklocation.Text = txt_Cab.Text;
        txt_tobuilding.Text = txt_Bulding.Text;
        txt_tofloor.Text = txt_Floor.Text;
        txt_toRoom.Text = txt_Room.Text;
        txtipadress.Text = sourceIPAdress.Text;
        txtsubnet.Text = sourcetxtsubnet.Text;
        txtvlan.Text = sourceVLAN.Text;
    }
   

    #region Dropdown bindings
    private DataTable GetProjectList()
    {
        try
        {
            if (Project1 != 0)
            {
                return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_DestSite", new SqlParameter("@projectreference", Project1)).Tables[0];
            }
            else
            {
                return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_ListDSList", new SqlParameter("@portfolio", getProtfoliyo)).Tables[0];
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        AssetsMoving GetPortfolio11 = new AssetsMoving();
        AssetsMoving GetPermision = new AssetsMoving();
        AssetsMoving GetDocumentUpload = new AssetsMoving();
        AssetsMoving PassAssestImage = new AssetsMoving();

        if (!PermissionManager.IsPermitted(AssestProject, Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.AddAssets))
        {
            lblerror.Text = "User doesn't have rights to Add Assets";
            return;
        }
        if (!CheckForEmpty(txt_assetno.Text.Trim(), txt_serialno.Text.Trim()))
        {
            lblerror.Text = "Please enter Asset number or Serial number";
        }
        else
        {

            int insertStatus = ASAdminCls.checkexistsAssets(txt_serialno.Text.Trim(), txt_assetno.Text.Trim());
            if (insertStatus == 0)
            {
                int getprotfolio1 = 0;
                if (Project1.ToString() == "0")
                {
                    //GetPortfilioID
                    getprotfolio1 = getProtfoliyo;

                }
                else
                {
                    getprotfolio1 = GetPortfolio11.GetPortfilioID(Convert.ToInt32(Project1));
                }
                int GetDocumentID = 0;


                int GetAssetsID = InsertorUpdateAssests(0, getprotfolio1);
                //Here We need to Inert the Image.....................................



                if (GetAssetsID > 0)
                {
                    if (FileUpload1.HasFile)
                    {
                        Guid _guid = Guid.NewGuid();
                        AssetsImageManager.Assets_SaveImage(_guid, FileUpload1.FileBytes);
                        PassAssestImage.ImageInsertion(GetAssetsID, _guid);


                    }
                    bool test111 = true;

                    if (FileUpload2.HasFile)
                    {
                        HttpFileCollection hfc = Request.Files;
                        foreach (string h in hfc.AllKeys)
                        {
                            if (h.Contains("FileUpload1"))
                            {

                                test111 = false;

                            }

                        }
                        int DocID = 0;

                        if (test111 == false)
                        {
                            DocID = 1;
                        }


                        for (int i = DocID; i < hfc.Count; i++)
                        {
                            HttpPostedFile hpf = hfc[i];



                            if (hpf.ContentLength > 0)
                            {


                                HttpPostedFile myFile = hpf;

                                string strFilename = System.IO.Path.GetFileName(hpf.FileName);
                                Stream DocumentStream = myFile.InputStream;
                                int documentlenth = myFile.ContentLength;
                                string imgContentType = myFile.ContentType;
                                byte[] imgBinaryData = new byte[documentlenth];
                                byte[] OrizinalimgBinaryData = new byte[documentlenth];
                                int GetDoc = DocumentStream.Read(imgBinaryData, 0, documentlenth);
                                GetDocumentID = GetDocumentUpload.WriteToDB(strFilename, myFile.ContentType, ref imgBinaryData, ref OrizinalimgBinaryData, documentlenth, GetAssetsID);
                                DocumentStream.Flush();
                                DocumentStream.Close();


                                myFile.InputStream.Flush();
                                myFile.InputStream.Close();

                            }





                        }

                    }
                }




                fillgrid();
                Clear_Fields();




            }
            else
            {
                lblerror.Text = "Please check Asset already exist";
            }
        }
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {

        

            AssetsMoving GetPortfolio11 = new AssetsMoving();
            AssetsMoving GetAssetsImageID = new AssetsMoving();
            AssetsMoving GetDocumentUpload = new AssetsMoving();
            AssetsMoving GetDocumentGetID = new AssetsMoving();
            AssetsMoving PassAssestImage = new AssetsMoving();

            int GetDocumentID = 0;

            int AssetsTESTID = 0;
            AssetsTESTID = Convert.ToInt32(HiddenField4.Value);

            //Here I am getting the Assets Image ID.


            // GetImageID = GetAssetsImageID.GetAssetsImageID(AssetsTESTID);

            //Here we are getting the Assets Documnets ID.

            int DocumentAssetsID = 0;
            DocumentAssetsID = GetDocumentGetID.GetAssetsDocumnetID(AssetsTESTID);







            InsertorUpdateAssests(AssetsTESTID, 0);

            #region ImageUpdate while Updating

            if (FileUpload1.HasFile)
            {
                string fileName = FileUpload1.PostedFile.FileName.ToString();

                string imgExtsn = fileName.Substring(fileName.LastIndexOf("."));
                imgExtsn = imgExtsn.ToLower();
                string imgType = FileUpload1.PostedFile.ContentType;
                int imgLen = FileUpload1.PostedFile.ContentLength;
                byte[] imgData = new byte[imgLen];
                Stream imgStream = FileUpload1.PostedFile.InputStream;
                int n = imgStream.Read(imgData, 0, imgLen);
                if ((imgExtsn == ".jpg") || (imgExtsn == ".bmp") || (imgExtsn == ".gif") || (imgExtsn == ".png"))
                {

                    if (FileUpload1.HasFile)
                    {
                        Guid _guid = Guid.NewGuid();

                        AssetsImageManager.Assets_SaveImage(_guid, FileUpload1.FileBytes);
                        PassAssestImage.ImageInsertion(AssetsTESTID, _guid);


                    }

                }
                else
                {
                    lblerror.Text = "Please Select image formates png,Jpg,Gif and Bmp.";
                }

            }


            #endregion


            #region UpdateDoc or Insert DOC
            if (FileUpload2.HasFile)
            {
                bool checkcondition = true;
                HttpFileCollection hfc = Request.Files;

                foreach (string h in hfc.AllKeys)
                {
                    if (h.Contains("FileUpload1"))
                    {

                        checkcondition = false;

                    }

                }
                int DocID = 0;

                if (checkcondition == false)
                {
                    DocID = 1;
                }




                for (int i = DocID; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {


                        HttpPostedFile myFile = hpf;
                        string strFilename = System.IO.Path.GetFileName(hpf.FileName);
                        Stream DocumentStream = myFile.InputStream;
                        int documentlenth = myFile.ContentLength;
                        string imgContentType = myFile.ContentType;
                        byte[] imgBinaryData = new byte[documentlenth];
                        byte[] OrizinalimgBinaryData = new byte[documentlenth];
                        int GetDoc = DocumentStream.Read(imgBinaryData, 0, documentlenth);
                        GetDocumentID = GetDocumentUpload.WriteToDB(strFilename, myFile.ContentType, ref imgBinaryData, ref OrizinalimgBinaryData, documentlenth, AssetsTESTID);
                        DocumentStream.Flush();
                        DocumentStream.Close();


                        myFile.InputStream.Flush();
                        myFile.InputStream.Close();
                    }
                }


            }
            #endregion

            GridView1.EditIndex = -1;
            fillgrid();
            Clear_Fields();
            ImageGet.Visible = false;
            ImgBtnUpdate.Visible = false;
        //set update button
            btn_Update.Visible = false;
            btnCancel.Visible = false;
            ImgBtnCancel.Visible = false;
            Imgupdation.Visible = true;
        ///
            btn_Add.Visible = true;
            GetDocmentID.Visible = false;


        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
