using System;
using System.Collections.Generic;
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
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using UserMgt.DAL;
using UserMgt.Entity;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class AdminUsersSkills : System.Web.UI.Page
{
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    UserDataContext AdmnCntxt;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Admin";
        if (!this.IsPostBack)
        {
            //grdUserSkillsBind(Convert.ToInt32(Request.QueryString["uid"]));

            if (Request.QueryString["uid"] != null)
            {

                //SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
                //BindTrainingGrid(Convert.ToInt32(Request.QueryString["uid"]));
                // UserId=(Convert.ToInt32(Request.QueryString["uid"]));
                

            }
        }
    }
    // Back to Admin home
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }

    private void grdUserSkillsBind(int UserId)
    {
        try
        {
           
            //AdmnCntxt = new UserDataContext();
            //var Skills = from s in AdmnCntxt.AdminUserSkills_SelectByUserid(UserId).ToList()
                         
            //             select s;
                         
            //grdUserSkills.DataSource = Skills;
            //grdUserSkills.DataBind();
            AdmnCntxt = new UserDataContext();
            grdUserSkills.DataSource = AdmnCntxt.UserSkills_SelectByUserid(UserId).ToList();
            grdUserSkills.DataBind();



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Training Grid
    private void BindTrainingGrid(int Uid)
    {
        try
        {
            //List<v_Training_Booking> trainingRecords = (from r in AdmnCntxt.v_Training_Bookings
            //                                            where r.Employee == Uid
            //                                            select new v_Training_Booking {CategoryName = r.CategoryName, CourseTitle = r.CourseTitle, 
            //                                                StatusName = r.StatusName, DateofCourse = r.DateofCourse, EndDate = r.EndDate }).ToList();
           
         
            List<BookingsEntity> trainingRecords=(from r in Bookings.Bookings_SelectAll()
                                                  where r.Employee==Uid
                                                  select new  BookingsEntity{CategoryName = r.CategoryName, CourseTitle = r.CourseTitle, 
                                                         StatusName = r.StatusName, DateofCourse = r.DateofCourse, EndDate = r.EndDate }).ToList(); 
            if (trainingRecords != null)
            {
                grdTrainingRecords.DataSource = trainingRecords;
                grdTrainingRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
    #region SkillGridView
    protected void grdUserSkills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Insert_Empty")
            {

                ((TextBox)grdUserSkills.FooterRow.FindControl("txtSkills")).Text = string.Empty;
                ((TextBox)grdUserSkills.FooterRow.FindControl("txtSNotes")).Text = string.Empty;
               
                
            }

             if (e.CommandName == "Insert")
            {
                try
                {
                    string Skl = ((TextBox)grdUserSkills.FooterRow.FindControl("txtSkills")).Text.ToString();
                    if(Skl.ToString() !="")
                    {
                    AdmnCntxt = new UserDataContext();
                    UserSkill AUSkill = new UserSkill();
                    AUSkill.UserId = int.Parse(Request.QueryString["uid"]);
                    AUSkill.Skills = ((TextBox)grdUserSkills.FooterRow.FindControl("txtSkills")).Text.ToString();
                    AUSkill.SkillLevel = ((DropDownList)grdUserSkills.FooterRow.FindControl("ddlFSLevel")).SelectedItem.Text.ToString();
                    AUSkill.Notes = ((TextBox)grdUserSkills.FooterRow.FindControl("txtSNotes")).Text.ToString();
                    
                    AdmnCntxt.UserSkills.InsertOnSubmit(AUSkill);
                    AdmnCntxt.SubmitChanges();
                    int id = AUSkill.Id;
                    if (id > 0)
                    {
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "Address added successfully";
                        grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));

                    }
                    }
                    else{
                        grdUserSkills.EditIndex = -1;
                        grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));
                        lblSkillErr.Visible = true;
                        lblSkillErr.Text = "Please enter skill";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
             if (e.CommandName == "Delete")
             {
                 try
                 {
                     AdmnCntxt = new UserDataContext();
                     UserSkill ad = AdmnCntxt.UserSkills.Single(A => A.Id == int.Parse(e.CommandArgument.ToString()));
                     AdmnCntxt.UserSkills.DeleteOnSubmit(ad);
                     AdmnCntxt.SubmitChanges();
                     grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));
                 }
                 catch (Exception ex)
                 {
                     LogExceptions.WriteExceptionLog(ex);
                 }

             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        

    }
    protected void grdUserSkills_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdUserSkills.EditIndex = -1;
        grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));
    }
    protected void grdUserSkills_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdUserSkills.EditIndex = e.NewEditIndex;
        grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));

    }
    protected void grdUserSkills_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox skl = (TextBox)grdUserSkills.Rows[e.RowIndex].FindControl("txtEditSkills");
            if(skl.Text.ToString()!="" )
            {
            TextBox txtCSkill = (TextBox)grdUserSkills.Rows[e.RowIndex].FindControl("txtEditSkills");
            DropDownList ddlCSLevel = (DropDownList)grdUserSkills.Rows[e.RowIndex].FindControl("ddlESLevel");
            TextBox txtCNotes = (TextBox)grdUserSkills.Rows[e.RowIndex].FindControl("txtEditNotes");
            Label lblCUId = (Label)grdUserSkills.Rows[e.RowIndex].FindControl("lblUID");
            Label lblCId =  (Label)grdUserSkills.Rows[e.RowIndex].FindControl("lblID");
            LinkButton LinkButtonUpdate = (LinkButton)grdUserSkills.Rows[e.RowIndex].FindControl("btnupdate");
            //linq to sql start
            int CSkillID = int.Parse(lblCId.Text.ToString());

            AdmnCntxt = new UserDataContext();
            var Data = (from AUSk in AdmnCntxt.UserSkills
                        where AUSk.Id == CSkillID
                        select new
                        {
                            AUSk.Id,
                            AUSk.UserId,
                            AUSk.Skills,
                            AUSk.SkillLevel,
                            AUSk.Notes

                        }).Distinct();
            if (Data.Count() > 0)
            {
                AdmnCntxt.UserSkills_Update(CSkillID, int.Parse(lblCUId.Text.ToString()), txtCSkill.Text.ToString(),
                    ddlCSLevel.SelectedItem.Text.ToString(), txtCNotes.Text.ToString());
                AdmnCntxt.SubmitChanges();
                grdUserSkills.EditIndex = -1;
                grdUserSkillsBind(int.Parse(lblCUId.Text.ToString()));
            }
            }
            else
            {
                grdUserSkills.EditIndex = -1;
                grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));
                lblSkillErr.Visible = true;
                lblSkillErr.Text = "Please enter skill";
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

  
    protected void grdUserSkills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           UserSkills_SelectByUseridResult Drow = (UserSkills_SelectByUseridResult)e.Row.DataItem;
            if (Drow.Id.ToString() == "-99")
            {
                e.Row.Visible = false;

            }
           
           
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {

            //TextBox txtSkill = (TextBox)e.Row.FindControl("txtSkills") ;
            //txtSkill.Text = string.Empty;

            //TextBox txtLevel = (TextBox)e.Row.FindControl("txtSLevel") ;
            //txtLevel.Text = string.Empty;

            //TextBox txtNotes = (TextBox)e.Row.FindControl("txtNotes") ;
            //txtNotes.Text = string.Empty;

        }
    }

    protected void grdUserSkills_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdUserSkills.EditIndex = -1;
        grdUserSkillsBind(int.Parse(Request.QueryString["uid"]));
    }
    #endregion SkillGridview
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
    
    protected void grdTrainingRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTrainingRecords.PageIndex = e.NewPageIndex;
        if (Request.QueryString["uid"] != null)
        {

            
            BindTrainingGrid(Convert.ToInt32(Request.QueryString["uid"]));
            // UserId=(Convert.ToInt32(Request.QueryString["uid"]));


        }
    }
    protected void grdUserSkills_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUserSkills.PageIndex = e.NewPageIndex;
        if (Request.QueryString["uid"] != null)
        {
            grdUserSkillsBind(Convert.ToInt32(Request.QueryString["uid"]));
        }
    }
}

