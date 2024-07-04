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
using Flan.FutureControls;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class adminmasterlists1 : BasePage
{
    int AC2PID;
    int ProjectReference;
    int T;
    int ContractorID;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected string Layout_Header = string.Empty;
    protected string Layout_border = string.Empty;
    string setval = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            //check to set the drop down value
            if (Request.QueryString["setval"] != null)
            {
                setval = Request.QueryString["setval"].ToString();
                ChecklistType.SelectedValue = setval;
            }
            bind_ddlddlTemplates();
        }
        lblErrorgrid.Visible = false;
        lblSuccess.Visible = false;
        lblSuccess2.Visible = false;
        lblTemplatemsg.Visible = false;
        chkLock.Visible = false;

        if (ddlTemplates.SelectedItem.Text == "Select...")
        {
            GridView1.Visible = false;
        }
       //if link from Portfolio section
        ChangeLayout();

    }
    protected void ChangeLayout()
    {
        //if (QueryStringValues.Type == "portfolio")
        //{
        //    Layout_border = "p_section5 data_carrier_block";
        //    Layout_Header = "section5";
        //    //Master.PageHead = Resources.DeffinityRes.CustomerAdmin;// "Customer Admin";
        //    //Master.MasterTabs = true;
        //}
        //else if (QueryStringValues.Type == "health")
        //{
        //    Layout_border = "p_section4 data_carrier_block";
        //    Layout_Header = "section4";
        //   // Master.PageHead = Resources.DeffinityRes.HealthCheck; //"Health Check";
        //    //Master.MasterTabs = true;
        //}
        //else
        //{
        //    Layout_border = "p_section1 data_carrier_block";
        //    Layout_Header = "section1";
        //    //Master.PageHead = Resources.DeffinityRes.Admin;// "Admin";
        //   // Master.MasterTabs = false;
        //}
    }
    public void bind_ddlddlTemplates()
    {
        SqlDataAdapter adp_ddlTemplates = new SqlDataAdapter(string.Format(" Select * from MasterTemplate WHERE Active='Y' and ChecklistType = {0} order by Description",ChecklistType.SelectedValue) , con);
        DataSet ds = new DataSet();
        adp_ddlTemplates.Fill(ds);
        ddlTemplates.DataSource = ds;
        //ds.Clear();
        ddlTemplates.DataTextField = "Description";
        ddlTemplates.DataValueField = "ID";
        ddlTemplates.DataBind();
        ListItem _selectTemplates = new ListItem("Select...", "0");
        ddlTemplates.Items.Insert(0, _selectTemplates);
        if (ds.Tables[0].Rows.Count == 0)
        {
            //lblChkList.Text = "New Template Name";
            txtNewTemplate.Visible = true;
            ddlTemplates.Visible = false;
            btnNew.Visible = false;
            btnCancel.Visible = false;
            btndelete.Visible = false;
            btnSubmitTemplate.Visible = true;
            chkLock.Visible = false;
        }
        else
        {
            //lblChkList.Text = "Existing Templates";
            txtNewTemplate.Visible = false;
            btnCancel.Visible = false;
            ddlTemplates.Visible = true;
            btnNew.Visible = true;
            btnSubmitTemplate.Visible = false;
            btndelete.Visible = true;
            chkLock.Visible = false;
        }
    }
    protected void ddlTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTemplates.SelectedItem.Text != "Select...")
        {
            GridView1.Visible = true;
            bindgrid();
            checkLocked();
            //save as panel visibility change
            Visibility_Panel_SaveAS(true);
        }
        else
        {
            ds_grid.Clear();
            GridView1.DataBind();
        }

    }
    DataSet ds_grid = new DataSet();
    public void bindgrid()
    {
        if (ddlTemplates.SelectedItem.Text != "Select...")
        {
            GridView1.Visible = true;

            SqlDataAdapter adp_grid = new SqlDataAdapter("Select * from MasterTemplateItems where MasterTemplateID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + " order by PositionInList", con);
            //SqlDataAdapter adp_grid = new SqlDataAdapter("Select ID,PositionInList,ItemDescription,RAGRequired,AmberDays,RedDays,Price,Quantity,MasterTemplateID,SellingPrice,GrossProfit,convert(varchar,WorstCaseTime)+WCExtension as WorstCaseTime1,convert(varchar,BestCaseTime)+BCExtension as BestCaseTime1,convert(varchar,MostLikelyCaseTime)+MCExtension as MostLikelyCasetime1 from MasterTemplateItems where MasterTemplateID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + " order by PositionInList", con);
            adp_grid.Fill(ds_grid);

            GridView1.DataSource = ds_grid;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //lblChkList.Text = "New Template Name";
        txtNewTemplate.Visible = true;
        ddlTemplates.Visible = false;
        ddlTemplates.SelectedIndex = 0;
        //btnNew.Visible = false;
        btnCancel.Visible = true;
        //btndelete.Visible = false;
        btnSubmitTemplate.Visible = true;
        chkLock.Visible = false;
        GridView1.Visible = false;
        //save as panel
        Visibility_Panel_SaveAS(false);
    }
    protected void btnSubmitTemplate_Click(object sender, EventArgs e)
    {
        if (check(txtNewTemplate.Text) == 0)
        {
            SqlCommand comm_submittemplate = new SqlCommand("DN_MasterTemplate_Insert", con);
            comm_submittemplate.CommandType = CommandType.StoredProcedure;
            comm_submittemplate.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtNewTemplate.Text;
            comm_submittemplate.Parameters.Add("@Locked", SqlDbType.Char, 20).Value = "N";
            comm_submittemplate.Parameters.Add("@Active", SqlDbType.Char, 1).Value = "Y";
            comm_submittemplate.Parameters.Add("@ChecklistType", SqlDbType.Int).Value = Convert.ToInt32(ChecklistType.SelectedValue);
            try
            {
                con.Open();
                int i = comm_submittemplate.ExecuteNonQuery();
                con.Close();
                //if (i == 1)
                //{
                    hidbuttons();
                    bind_ddlddlTemplates();
                    txtNewTemplate.Text = "";
                    //bindgrid();
                    GridView1.Visible = false;
                //save as panel visibility change
                    Visibility_Panel_SaveAS(true);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        }
        else
        {
            //Already Exists
            lblTemplatemsg.Text = Resources.DeffinityRes.Templateexists; //"Template Already Exists";
            lblTemplatemsg.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hidbuttons();
        //save as panel visibility change
        Visibility_Panel_SaveAS(true);
    }
    public void hidbuttons()
    {
        //lblChkList.Text = "Existing Templates";
        txtNewTemplate.Visible = false;
        btnCancel.Visible = false;
        ddlTemplates.Visible = true;
        btnNew.Visible = true;
        btnSubmitTemplate.Visible = false;
        btndelete.Visible = true;
        chkLock.Visible = false;
        GridView1.Visible = true;
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlTemplates.SelectedValue) != 0)
        {
            lblConfirm.Visible = true;
            btnConfirmYes.Visible = true;
            btnConfirmNo.Visible = true;
        }
    }
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand comm_delete = new SqlCommand("delete from MasterTemplate where ID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + "", con);
            SqlCommand comm_deleteitems = new SqlCommand("delete from MasterTemplateItems where MasterTemplateID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + "", con);
            con.Open();
            int i = comm_delete.ExecuteNonQuery();
            int ii = comm_deleteitems.ExecuteNonQuery();
            con.Close();
            if (i == 1 || ii == 1)
            {
                bind_ddlddlTemplates();
                bindgrid();
                GridView1.Visible = false;
                //save as panel visibility change
                Visibility_Panel_SaveAS(true);
            }
            confirmvisible();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btnConfirmNo_Click(object sender, EventArgs e)
    {
        confirmvisible();
        //save as panel visibility change
        Visibility_Panel_SaveAS(true);
    }
    public void confirmvisible()
    {
        lblConfirm.Visible = false;
        btnConfirmYes.Visible = false;
        btnConfirmNo.Visible = false;
    }

    protected void chkLock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkLock.Checked == true)
            {
                SqlCommand comm_updateMasterTemplate = new SqlCommand("update MasterTemplate set Locked='Y' where ID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + "", con);
                con.Open();
                comm_updateMasterTemplate.ExecuteNonQuery();
                con.Close();
            }
            if (chkLock.Checked == false)
            {
                SqlCommand comm_updateMasterTemplate = new SqlCommand("update MasterTemplate set Locked='N' where ID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + "", con);
                con.Open();
                comm_updateMasterTemplate.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    public void checkLocked()
    {
        try
        {
            SqlCommand comm_chklock = new SqlCommand("select Locked from MasterTemplate where ID=" + Convert.ToInt32(ddlTemplates.SelectedValue) + "", con);
            con.Open();
            if (comm_chklock.ExecuteScalar().ToString().Trim() == "Y")
            {
                chkLock.Checked = true;
            }
            else
            {
                chkLock.Checked = false;
            }
            con.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btnSaveAsName_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlTemplates.SelectedItem.Text == "Select...")
            {
                lblTemplatemsg.Visible = true;
                lblTemplatemsg.Text = Resources.DeffinityRes.Pleaseselectatemplatefirst; //"Please select a template first!";
            }
            else
            {
                if (check(txtSaveAsName.Text) > 0)
                {
                    lblTemplatemsg.Visible = true;
                    lblTemplatemsg.Text = Resources.DeffinityRes.Templateexists; //"Template exists!";
                }
                else
                {
                    SqlCommand comm_templateadd = new SqlCommand("DN_MasterTemplateCreateandInsert", con);
                    comm_templateadd.CommandType = CommandType.StoredProcedure;
                    comm_templateadd.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = txtSaveAsName.Text;
                    comm_templateadd.Parameters.Add("@MasterTemplateID", SqlDbType.Int).Value = Convert.ToInt32(ddlTemplates.SelectedValue);
                    comm_templateadd.Parameters.Add("@ChecklistType", SqlDbType.Int).Value = Convert.ToInt32(ChecklistType.SelectedValue);
                    
                    con.Open();
                    int i = comm_templateadd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        bind_ddlddlTemplates();
                        bindgrid();
                        lblSuccess.Text =Resources.DeffinityRes.Templtcreatedsuccfully; //"Template created successfully!";
                        txtSaveAsName.Text = "";
                        lblTemplatemsg.Visible = false;
                        //lblSuccess2.Visible = true;
                        lblSuccess.Visible = true;
                        GridView1.Visible = false;
                    }
                    con.Close();
                }


            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    public int check(string chkstring)
    {
       
            SqlCommand comm_chk = new SqlCommand("DN_chelist_checkmaster", con);
            comm_chk.CommandType = CommandType.StoredProcedure;
            comm_chk.Parameters.Add("@chkstring", SqlDbType.NVarChar, 100).Value = chkstring;
            comm_chk.Parameters.Add("@ChecklistType", SqlDbType.Int).Value = int.Parse(ChecklistType.SelectedValue);
            
            con.Open();
            int count = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
            con.Close();
            int c;
            if (count > 0)
            {
                c = 1;
            }
            else
            {
                c = 0;
            }
       


        return c;

    }
    protected void btnSaveAsQA_Click(object sender, EventArgs e)
    {
        try
        {


            if (txtSaveAsName.Text == "")
            {
                lblTemplatemsg.Visible = true;
                lblTemplatemsg.Text = Resources.DeffinityRes.Templaterequiresaname; //"Template requires a name!";
            }
            else if (ddlTemplates.SelectedItem.Text == "Select...")
            {
                lblTemplatemsg.Visible = true;
                lblTemplatemsg.Text = Resources.DeffinityRes.Pleaseselectatemplatefirst; //"Please select a template first!";
            }
            else
            {
                if (checkQA(txtSaveAsName.Text) > 0)
                {
                    lblTemplatemsg.Visible = true;
                    lblTemplatemsg.Text = Resources.DeffinityRes.QATemplateexists; //"QA Template exists!";
                }
                else
                {
                    SqlCommand comm_QAtemplateadd = new SqlCommand("DN_QACheckListsCreateandInsert", con);
                    comm_QAtemplateadd.CommandType = CommandType.StoredProcedure;
                    comm_QAtemplateadd.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = txtSaveAsName.Text;
                    comm_QAtemplateadd.Parameters.Add("@MasterTemplateID", SqlDbType.Int).Value = Convert.ToInt32(ddlTemplates.SelectedValue);
                    con.Open();
                    int i = comm_QAtemplateadd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        lblSuccess.Text = Resources.DeffinityRes.QATemplatecreatedsuccfully; //"QA Template created successfully!";
                        lblTemplatemsg.Visible = false;
                        lblSuccess.Visible = true;
                        txtSaveAsName.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    public int checkQA(string chkstring)
    {
        SqlCommand comm_chk = new SqlCommand("DN_chelist_checkQA", con);
        comm_chk.CommandType = CommandType.StoredProcedure;
        comm_chk.Parameters.Add("@chkstring", SqlDbType.NVarChar, 100).Value = chkstring;
        con.Open();
        int count = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
        con.Close();
        int c;
        if (count > 0)
        {
            c = 1;
        }
        else
        {
            c = 0;
        }

        return c;

    }
    protected void lbInsert_Click(object sender, EventArgs e)
    {

    }

    public string getID(string ID, string PositionInList)
    {
        return ID + "," + PositionInList;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int AmberDays = 1;
            int RedDays = 3;
            string RAGRequired = "N";
            string ItemDesc = "";
            double Price = 0;
            int Quantity = 0;
            double SellingPrice = 0;
            double GrossProfit = 0;
            int WorstCaseTime = 0;
            int BestCaseTime = 0;
            int MostLikelyCaseTime = 0;
            string WcExtension ="";
            string BcExtension = "";
            string McExtension = "";
            string WorstCaseTime1 = "";
            string BestCaseTime1 = "";
            string MostLikelyCaseTime1 = "";
            //int MostlikelyCaseTime = 0;
            

            if (e.CommandName == "Insert")
            {
                //if (((TextBox)GridView1.FooterRow.FindControl("txtAmberDays")).Text != "")
                //{
                //    AmberDays = Convert.ToInt32(((TextBox)GridView1.FooterRow.FindControl("txtAmberDays")).Text);
                //}
                //if (((TextBox)GridView1.FooterRow.FindControl("txtRedDays")).Text != "")
                //{
                //    RedDays = Convert.ToInt32(((TextBox)GridView1.FooterRow.FindControl("txtRedDays")).Text);
                //}
                
                //RAGRequired = ((DropDownList)GridView1.FooterRow.FindControl("ddlRAGRequired")).SelectedItem.Value;
                ItemDesc = ((TextBox)GridView1.FooterRow.FindControl("txtItem")).Text;
                insert(ItemDesc, RAGRequired, AmberDays, RedDays, Price, SellingPrice, GrossProfit, WorstCaseTime, BestCaseTime, MostLikelyCaseTime, WcExtension, BcExtension, McExtension,WorstCaseTime1,BestCaseTime1,MostLikelyCaseTime1);
            }
            if (e.CommandName == "EmptyInsert")
            {
                ItemDesc = ((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtItem1")).Text;
                //if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtAmberDays1")).Text != "")
                //{
                //    AmberDays = Convert.ToInt32(((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtAmberDays1")).Text);
                //}
                //if (((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtRedDays1")).Text != "")
                //{
                //    RedDays = Convert.ToInt32(((TextBox)GridView1.Controls[0].Controls[0].FindControl("txtRedDays1")).Text);
                //}
               

                //RAGRequired = ((DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddlRAG1")).SelectedItem.Value;
                if (!string.IsNullOrEmpty(ItemDesc.Trim()))
                {
                    insert(ItemDesc, RAGRequired, AmberDays, RedDays, Price, SellingPrice, GrossProfit, WorstCaseTime, BestCaseTime, MostLikelyCaseTime, WcExtension, BcExtension, McExtension, WorstCaseTime1, BestCaseTime1, MostLikelyCaseTime1);
                }
                else
                {
                    lblErrorgrid.Visible = true;
                    lblErrorgrid.Text = Resources.DeffinityRes.PleaseenterItemdescription; //"Please enter item description"; 
                }

            }

            if (e.CommandName == "Update")
            {
                string ItemDesciption = "";
                int Amber = 1;
                int Red = 3;
                string RAG = "N";
                double Price1 = 0;
                int Quantity1 = 0;
                double SellingPrice1 = 0;
                double GrossProfit1 = 0;
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                GridViewRow Row = GridView1.Rows[i];
                ItemDesciption = ((TextBox)Row.FindControl("txtDesc")).Text;

                // if (((TextBox)Row.FindControl("txtAmber")).Text != "")
                //{
                //    Amber = Convert.ToInt32(((TextBox)Row.FindControl("txtAmber")).Text);
                //}
                //if (((TextBox)Row.FindControl("txtRed")).Text != "")
                //{
                //    Red = Convert.ToInt32(((TextBox)Row.FindControl("txtRed")).Text);
                //}
                //RAG = ((DropDownList)Row.FindControl("ddlRAG")).SelectedValue;
                
                SqlCommand comm_Update = new SqlCommand("DN_MasterTemplateItems_update", con);
                comm_Update.CommandType = CommandType.StoredProcedure;
                comm_Update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                comm_Update.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500).Value = ItemDesciption;
                comm_Update.Parameters.Add("@RAGRequired", SqlDbType.Char, 1).Value = RAG;
                comm_Update.Parameters.Add("@AmberDays", SqlDbType.Int).Value = Amber;
                comm_Update.Parameters.Add("@RedDays", SqlDbType.Int).Value = Red;
                comm_Update.Parameters.Add("@Price", SqlDbType.Float).Value = Price1;
                //comm_Update.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity1;
                comm_Update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = SellingPrice1;
                comm_Update.Parameters.Add("@GrossProfit", SqlDbType.Float).Value = GrossProfit1;
                comm_Update.Parameters.Add("@WorstCaseTime", SqlDbType.Int).Value = WorstCaseTime;
                comm_Update.Parameters.Add("@BestCaseTime", SqlDbType.Int).Value = BestCaseTime;
                comm_Update.Parameters.Add("@MostLikelyCaseTime", SqlDbType.Int).Value=MostLikelyCaseTime ;
                comm_Update.Parameters.Add("@WcExtension", SqlDbType.Char).Value = WcExtension;
                comm_Update.Parameters.Add("@BcExtension", SqlDbType.Char).Value = BcExtension;
                comm_Update.Parameters.Add("@McExtension", SqlDbType.Char).Value = McExtension;
                
                con.Open();
                int s = comm_Update.ExecuteNonQuery();
                con.Close();
            }
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                SqlCommand comm_del = new SqlCommand("delete from MasterTemplateItems where ID=" + id + "", con);
                con.Open();
                int ch = comm_del.ExecuteNonQuery();
                con.Close();
                if (ch > 0)
                {
                    bindgrid();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        //bindgrid();

    }
    public void insert(string ItemDesc, string RAGRequired, int AmberDays, int RedDays, double Price, double SellingPrice, double GrossProfit, int WorstCaseTime, int BestCaseTime, int MostLikelyCaseTime, string WcExtension, string BcExtension, string McExtension, string WorstCaseTime1,string BestCaseTime1,string MostLikelyCaseTime1)
    {
        int MasterTemplateID = 0;
        if (chkLock.Checked == false)
        {

            if (ddlTemplates.SelectedItem.Text == "Select...")
            {
                lblErrorgrid.Text = Resources.DeffinityRes.SelectaTemplate; //"Select a Template";
            }
         
            else
            {
                MasterTemplateID = Convert.ToInt32(ddlTemplates.SelectedItem.Value);
                //'check item doesnt already exist

                SqlCommand comm_chkitem = new SqlCommand("DN_chelist_check", con);
                comm_chkitem.CommandType = CommandType.StoredProcedure;
                comm_chkitem.Parameters.Add("@MasterTemplateID", SqlDbType.Int).Value = MasterTemplateID;
                comm_chkitem.Parameters.Add("@Itemdesc", SqlDbType.NVarChar, 100).Value = ItemDesc;
                con.Open();
                int c = Convert.ToInt32(comm_chkitem.ExecuteScalar().ToString());
                con.Close();
                if (c > 0)
                {
                    lblErrorgrid.Visible = true;
                    lblErrorgrid.Text = Resources.DeffinityRes.Itemalreadyexistsintemplate; //"Item already exists in this template!";
                }
                else
                {
                    //'get last position
                    SqlCommand comm_position = new SqlCommand("select PositionInList from MasterTemplateItems where MasterTemplateID=" + MasterTemplateID + " order by PositionInList desc", con);
                    con.Open();
                    int PositionInList = 0;
                    int NextPosition = 0;
                    SqlDataReader dr = comm_position.ExecuteReader();
                    if (dr.Read())
                    {
                        PositionInList = Convert.ToInt32(dr["PositionInList"].ToString());
                        NextPosition = PositionInList + 1;
                    }
                    else
                    {
                        PositionInList = 1;
                        NextPosition = 1;
                    }
                    //'add the item 
                    dr.Close();
                    con.Close();

                    SqlCommand comm_additem = new SqlCommand("DN_MasterTemplateItems_add", con);
                    comm_additem.CommandType = CommandType.StoredProcedure;
                    comm_additem.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500).Value = ItemDesc;
                    comm_additem.Parameters.Add("@RAGRequired", SqlDbType.Char, 1).Value = RAGRequired;
                    comm_additem.Parameters.Add("@AmberDays", SqlDbType.Int).Value = AmberDays;
                    comm_additem.Parameters.Add("@RedDays", SqlDbType.Int).Value = RedDays;
                    comm_additem.Parameters.Add("@MasterTemplateID", SqlDbType.Int).Value = MasterTemplateID;
                    comm_additem.Parameters.Add("@NextPosition", SqlDbType.Int).Value = NextPosition;
                    comm_additem.Parameters.Add("@Price", SqlDbType.Float).Value = Price;
                    //comm_additem.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
                    comm_additem.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = SellingPrice;
                    comm_additem.Parameters.Add("@GrossProfit", SqlDbType.Float).Value = GrossProfit;
                    comm_additem.Parameters.Add("@WorstCaseTime", SqlDbType.Int).Value = WorstCaseTime;
                    comm_additem.Parameters.Add("@BestCaseTime", SqlDbType.Int).Value = BestCaseTime;
                    comm_additem.Parameters.Add("@MostLikelyCaseTime", SqlDbType.Int).Value = MostLikelyCaseTime;
                    comm_additem.Parameters.Add("@WcExtension", SqlDbType.Char).Value = WcExtension;
                    comm_additem.Parameters.Add("@BcExtension", SqlDbType.Char).Value = BcExtension;
                    comm_additem.Parameters.Add("@McExtension", SqlDbType.Char).Value = McExtension;
                    
                    
                    con.Open();
                    int i = comm_additem.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {
                        bindgrid();
                        //txtAmberDays.Text = "";
                        //txtItem.Text = "";
                        //txtRedDays.Text = "";
                    }
                }
            }
        }
        else
        {
            lblErrorgrid.Visible = true;
            lblErrorgrid.Text = Resources.DeffinityRes.Templateislocked; //"Template is locked!";
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridView1.ShowFooter = false;
        bindgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        GridView1.EditIndex = -1;
        GridView1.ShowFooter = true;
        bindgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.ShowFooter = true;
        bindgrid();
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void rowDragOE_RowDrop(object sender, RowDropEventArgs e)
    {
        try
        {
            string stemp = "";
            int id;
            int newCnt;
            int i = 0;
            //UpdateRowCount();
            RowDragOverlayExtender rde = (RowDragOverlayExtender)sender;
            stemp = e.SourceGridViewID;
            stemp = e.SourceRowIndex.ToString();
            id = Convert.ToInt32(((GridView)Page.FindControl(e.SourceGridViewID)).DataKeys[e.SourceRowIndex].Value.ToString());
            stemp = rde.GridViewUniqueID;

            //grid to grid drag and drop        
            if (e.SourceGridViewID.Contains("GridView1") == true)
            {
                GridItemPosition(e.SourceRowIndex + 1, rde.RowIndex + 1, id, Convert.ToInt32(ddlTemplates.SelectedValue));
            }
            bindgrid();
        }
        catch (Exception ex)
        { 
            LogExceptions.LogException(ex.Message);
        }

    }
    private void GridItemPosition(int oldPos, int newPos, int id, int templateID)
    {
        DbCommand cmd = db.GetStoredProcCommand("DN_CheckListPosition");
        db.AddInParameter(cmd, "@newPos", DbType.Int32, newPos);
        db.AddInParameter(cmd, "@oldPos", DbType.Int32, oldPos);
        db.AddInParameter(cmd, "@id", DbType.Int32, id);
        db.AddInParameter(cmd, "@MasterTemplateID", DbType.Int32, templateID);
        db.ExecuteNonQuery(cmd);
        cmd.Dispose();
    }
    protected string getCase(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1)||(s1 =="0"))
        {
            s1 = "0";
            s2 = "M";
        }
        return s1 + s2;
    }

    protected void ChecklistType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ddlddlTemplates();
        bindgrid();
        //save as panel visibility change
        Visibility_Panel_SaveAS(true);
    }

    private void Visibility_Panel_SaveAS(bool a_visible)
    {
        div_SaveAs.Visible = a_visible;
    }

    protected void btnDecrease_Click(object sender, EventArgs e)
    {
        try
        {
            //[DN_MasterTemplateItems_UpdateIndent]
            LinkButton btnimg = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnimg.NamingContainer;
            Label lblID = (Label)row.FindControl("lblID");
            int s= int.Parse(btnimg.CommandArgument);
            if (s > 0 && s <= 8)
            {
                s = s - 1;
            }
            SqlCommand comm_additem = new SqlCommand("DN_MasterTemplateItems_UpdateIndent", con);
                    comm_additem.CommandType = CommandType.StoredProcedure;
                    comm_additem.Parameters.AddWithValue("@IndentLevel",s);
                    comm_additem.Parameters.AddWithValue("@ID", int.Parse(lblID.Text));
                    con.Open();
                    int i = comm_additem.ExecuteNonQuery();
                    con.Close();
                    bindgrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnIncrease_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnimg = sender as LinkButton;

            GridViewRow row = (GridViewRow)btnimg.NamingContainer;

            Label lblID = (Label)row.FindControl("lblID");
            int s = int.Parse(btnimg.CommandArgument);
            if (s >= 0 && s < 8)
            {
                s = s + 1;
            }
            SqlCommand comm_additem = new SqlCommand("DN_MasterTemplateItems_UpdateIndent", con);
            comm_additem.CommandType = CommandType.StoredProcedure;
            comm_additem.Parameters.AddWithValue("@IndentLevel", s);
            comm_additem.Parameters.AddWithValue("@ID", int.Parse(lblID.Text));
            con.Open();
            int i = comm_additem.ExecuteNonQuery();
            con.Close();
            bindgrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string getItemDes(string indent, string desc)
    {
        return Deffinity.ProjectTasksManagers.ProjectTasksManager.DisplayIndentLevel(desc, Convert.ToInt32(string.IsNullOrEmpty(indent) ? "0" : indent));
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    
}


