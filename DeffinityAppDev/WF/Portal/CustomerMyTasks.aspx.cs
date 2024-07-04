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
using Deffinity.Bindings;
using Deffinity.EmailService;
using System.Text;
using Deffinity.IssuesManager;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;

public partial class Portal_MyTasks : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    string Stemp = "";
    DisBindings getdata = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string strURL = "";
                //strURL = PermissionManager.GetNextMyTasksURL(PermissionManager.Feature.MyTasks);
                //if (!strURL.Contains("MyTasks.aspx"))
                   // Response.Redirect(strURL);
               // Master.PageHead = "My Tasks";
                getdata.DdlBindSelect(ddstatus, "SELECT ID, Status FROM ItemStatus ", "ID", "Status", false, true);
                ddlPortfolio.DataBind();
                if(ddlPortfolio.Items.Count >0)
                {
                    ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
                }
                ddlPortfolio.Enabled = false;
                
                ddlProjects.DataBind();
                SelectMyTask();

                ChangeTabControlByVendor();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ChangeTabControlByVendor()
    {
        if (Request.QueryString["vendorid"] != null)
        {
            changeTabVisibility(true, false, false);
            GridColumnVisibility(false,true);
        }
        else if (Request.QueryString["customer"] != null)
        {
            changeTabVisibility(false, false, true);
            GridColumnVisibility(false, true);
           // lblHeader.Attributes.Add("class", "section6");
           // lblborder.Attributes.Add("class", "p_section6 data_carrier_block");
        }
        else
        {
            changeTabVisibility(false, true, false);
            GridColumnVisibility(true, false);
        }
    }
    private void changeTabVisibility(bool RFI,bool projects,bool customerportal)
    {
        //RFIVendorTabs1.Visible = RFI;
        //ProjectStatus1.Visible = projects;
        //CustomerTabs1.Visible = customerportal;
    }
    private void GridColumnVisibility(bool prefWithLink,bool prefWithoutLink)
    {
        GridView1.Columns[2].Visible = prefWithoutLink;
        GridView1.Columns[3].Visible = prefWithLink;
    }
    private void SelectMyTask()
    {
        try
        {

            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DN_MyTasksdisplaySearch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            if (ddrag1.SelectedItem.Text == "Please select...")
            {
                myCommand.Parameters.Add("@RAGStatus", SqlDbType.VarChar, 50).Value = "";
            }
            else
            {
                myCommand.Parameters.Add("@RAGStatus", SqlDbType.VarChar, 50).Value = ddrag1.SelectedItem.Text;
            }
            if (ddstatus.SelectedItem.Text == "Please select...")
            {
                myCommand.Parameters.Add("@ItemStatus", SqlDbType.Int, 32).Value = 0;
            }
            else
            {
                myCommand.Parameters.Add("@ItemStatus", SqlDbType.Int, 32).Value = ddstatus.SelectedValue;
            }

            if (txtdudate.Text == "")
            {
                myCommand.Parameters.Add("@TaskDate", SqlDbType.SmallDateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                myCommand.Parameters.Add("@TaskDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(txtdudate.Text.Trim());
            }

            myCommand.Parameters.Add("@contractorID", SqlDbType.Int, 32).Value = sessionKeys.UID;
            if (ddlPriority.SelectedItem.Text == "Please select...")
            {
                myCommand.Parameters.Add("@Priority", SqlDbType.VarChar, 50).Value = "";
            }
            else
            {
                myCommand.Parameters.Add("@Priority", SqlDbType.VarChar, 50).Value = ddlPriority.SelectedValue.Trim();
            }

            myCommand.Parameters.Add("@Project", SqlDbType.VarChar, 50).Value = int.Parse(ddlProjects.SelectedValue);
            myCommand.Parameters.Add("@Portfolio", SqlDbType.VarChar, 50).Value = int.Parse(ddlPortfolio.SelectedValue);

            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "My task search");
        }
       
    }
    
    
   
    protected void btn_search_Click(object sender, EventArgs e)
    {

        SelectMyTask();

    }

    protected string loadImage(string status)
    {
        string returnColour = string.Empty;
        if (status != null)
        {
            switch (status.ToUpper())
            {
                case "RED":
                case "r":
                    returnColour = "images/indcate_red.png";
                    break;
                case "GREEN":
                case "g":
                    returnColour = "images/indcate_green.png";
                    break;
                case "AMBER":
                case "a":
                    returnColour = "images/indcate_yellow.png";
                    break;
               
            }

        }
        if (!string.IsNullOrEmpty(returnColour))
        {
            returnColour = "<img alt='' src='" + returnColour + "' />";
        }
        return returnColour;
    }
    protected string LoadPriority(string p)
    {
        string retVal = @"~\images\icon_priority_low.gif"; ;
        if (!string.IsNullOrEmpty(p))
        {
            retVal = @"~\images\icon_priority_" + p.ToLower().Trim()+ ".gif";
        }
        return retVal;    
        
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        SelectMyTask();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        SelectMyTask();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                GridViewRow Row = GridView1.Rows[i];

                string pref = ((Label)Row.FindControl("lblprojectReference")).Text;
                string Title = ((Label)Row.FindControl("lblprojectTitle")).Text;
                string Task = ((Label)Row.FindControl("Label5")).Text;
                //string MailID = ((Label)Row.FindControl("lblemailid")).Text;
                string Note = ((TextBox)Row.FindControl("txtNotes")).Text;
                string comments = ((TextBox)Row.FindControl("txtEditComments")).Text;
                string Percent = ((TextBox)Row.FindControl("txtEditPercentComplete")).Text;
                string Raisedby = sessionKeys.UName;
                //string Owner = ((Label)Row.FindControl("lblowner")).Text;
                int Status = Convert.ToInt32(((DropDownList)Row.FindControl("ddlRAGStatus")).SelectedValue);
                try
                {
                    Database db = DatabaseFactory.CreateDatabase("DBString");
                    DbCommand cmd = db.GetStoredProcCommand("DN_MYTaskProjectItemsupdate");
                    db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
                    db.AddInParameter(cmd, "@Notes", DbType.String, Note);
                    db.AddInParameter(cmd, "@ItemStatus", DbType.Int32, Status);
                    db.AddInParameter(cmd, "@Comments", DbType.String, comments);
                    db.AddInParameter(cmd, "@PercentComplete", DbType.String, Percent);
                    db.ExecuteNonQuery(cmd);
                    //insert comment text as issue 

                    try
                    {
                        if (!string.IsNullOrEmpty(comments.Trim()))
                        {
                            IssuesManager.InsertFromCustomer(int.Parse(pref), comments.Trim(), sessionKeys.UID);
                            SendMail(int.Parse(pref));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.LogException(ex.Message, "My tasks issue insert");
                    }
                    //if (!string.IsNullOrEmpty(Note))
                    //{
                    //    Mailer(pref, Title, Task, MailID, Note, Raisedby, Owner);
                    //}
                   
                    
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message, "Update status");
                }
                SelectMyTask();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void SendMail(int projectreference)
    {
        ProjectIssue1.Visible = true;
        try
        {
            string name = string.Empty;
            string email = string.Empty;
            string projectStr = string.Empty;
            projectTaskDataContext pt = new projectTaskDataContext();
            
            //SqlDataReader dr = ProjectManager.GetProjectDetails(projectreference);
            //while (dr.Read())
            //{
            //    name = dr["OwnerName"].ToString();
            //    email = dr["OwnerEmail"].ToString();
            //}
            //dr.Close();
            //dr.Dispose();
            ProjectDetails pd = (from p1 in pt.ProjectDetails
                  where p1.ProjectReference == projectreference
                  select p1).FirstOrDefault();
            name = pd.OwnerName;
            email = pd.OwnerEmail;
            projectStr = pd.ProjectReferenceWithPrefix;
            ProjectIssue1.BindData(IssuesManager.GetmaxID(projectreference), projectreference, name, true);
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ProjectIssue1.RenderControl(htmlWrite);
            Email ToEmail = new Email();
            ToEmail.SendingMail(email, "An issue has been raised against project " + projectStr, htmlWrite.InnerWriter.ToString());
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally { ProjectIssue1.Visible = false; }

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        SelectMyTask();
    }

    private void Mailer(string pref,string title,string task,string mailid,string notes,string raisedby,string owner)
    {
        TaskNotes1.Visible = true;
        try
        {
            TaskNotes1.Task = task;
            TaskNotes1.Notes = notes;
            TaskNotes1.RaisedBy = raisedby;
            TaskNotes1.Receiver = owner;
            TaskNotes1.FillDetails();
            string htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            Html32TextWriter htmlTW = new Html32TextWriter(sw);
            TaskNotes1.RenderControl(htmlTW);
            htmlText = htmlTW.InnerWriter.ToString();
            string errorString = string.Empty;
            Email eMail = new Email();
            eMail.SendingMail(mailid, "Issue raised for Project: " + pref , htmlText);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message,"Task notes Mail");
        }
        finally
        {
            TaskNotes1.Visible = false;
        }
    }
    protected string loadCommentsPicture(string comments)
    {
        if (comments != DBNull.Value.ToString() || comments != "")
            return @"~\images\commentsedit.gif";
        else
            return @"~\images\comments.gif";
    }
    protected string checkNoComments(string comments)
    {
        if (comments != DBNull.Value.ToString() || comments != "")
            return comments;
        else
            return "No comments for this item";
    }
}
