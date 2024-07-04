using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectTasksManagers;
using System.Web.UI.HtmlControls;
using Deffinity.GlobalIssues;
using Deffinity.ProgrammeManagers;
using System.IO;
using System.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class ProjectTaskDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListTasks(ddlltTasks);
            if (ddlltTasks.Items.Count > 0)
            {
                ddlltTasks.SelectedIndex = 1;
            }
            BindGrid();
        }
    }
    private void BindListTasks(DropDownList ddl)
    {
        projectTaskDataContext _db = new projectTaskDataContext();

        var listTasks = (from r in _db.ProjectTaskItems
                         where r.ProjectReference == QueryStringValues.Project
                         orderby r.index
                         select new { ID = r.ID, Name = (r.index.HasValue?r.index.Value.ToString():"0") + "-" + r.ItemDescription }).ToList();
        if (listTasks != null)
        {
            ddl.DataSource = listTasks;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Please select...", "0"));
        }


    }
    #region  "Task Documents"

    private void BindGrid()
    {
        if (ddlltTasks.SelectedValue != "0")
        {
            grdTaskDocuments.Visible = true;
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT ID,DocumentName,convert(decimal(10,2),DataSize/1024.0) as DataSize,UploadDateTime,(SELECT ContractorName FROM Contractors WHERE Contractors.ID=contractorid) AS UpdatedBy FROM AC2P_Documents  where TaskID=@TaskID order by DocumentName ",
                new SqlParameter("@TaskID", int.Parse(ddlltTasks.SelectedValue))).Tables[0];
            grdTaskDocuments.DataSource = dt;
            grdTaskDocuments.DataBind();
        }
        else
        {
            grdTaskDocuments.Visible = false;
        }

    }

    protected void grdTaskDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
        if (e.CommandName == "Download")
        {
            Download(int.Parse(e.CommandArgument.ToString()));


        }

        if (e.CommandName == "Delete")
        {
            //Download(int.Parse(e.CommandArgument.ToString()));

            SqlHelper.ExecuteNonQuery(Constants.DBString,CommandType.Text,"delete AC2P_Documents where ID=@ID",
                new SqlParameter("ID",int.Parse(e.CommandArgument.ToString())));
            BindGrid();
        }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgTaskUpload_Click(object sender, EventArgs e)
    {
        UploadDocuments();
        //
    }

    private void UploadDocuments()
    {
        try
        {
            // Get the HttpFileCollection

            if (flupTaskDoc.HasFile)
            {
                if (flupTaskDoc.PostedFile.ContentLength > 0)
                {
                    byte[] myFileData = new byte[flupTaskDoc.PostedFile.ContentLength];
                    flupTaskDoc.PostedFile.InputStream.Read(myFileData, 0, flupTaskDoc.PostedFile.ContentLength);
                    using (SqlConnection con = new SqlConnection(Constants.DBString))
                    {
                        using (SqlCommand cmd = new SqlCommand("Quote_InsertInvoice", con))// This sp is uesd to insert documents to database
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
                            cmd.Parameters.AddWithValue("@DocName", System.IO.Path.GetFileName(flupTaskDoc.PostedFile.FileName));
                            cmd.Parameters.AddWithValue("@ContentType", flupTaskDoc.PostedFile.ContentType);
                            cmd.Parameters.AddWithValue("@Document", myFileData);
                            cmd.Parameters.AddWithValue("@DataSize", flupTaskDoc.PostedFile.ContentLength);
                            cmd.Parameters.AddWithValue("@FolderName", "Task Documents");
                            cmd.Parameters.AddWithValue("@UserID", sessionKeys.UID);
                            cmd.Parameters.AddWithValue("@TaskID", int.Parse(ddlltTasks.SelectedValue));
                            cmd.ExecuteScalar();
                            //return Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    lblErrMsg.Text = "File has been uploaded successfully";
                    lblErrMsg.Visible = true;
                    lblErrMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                lblErrMsg.Text = "Please select file to upload";
                lblErrMsg.Visible = true;
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindGrid();
            //HttpFileCollection hfc = Request.Files;
            //for (int i = 0; i < hfc.Count; i++)
            //{
            //    HttpPostedFile hpf = hfc[i];
            //    if (hpf.ContentLength > 0)
            //    {



            //        byte[] myFileData = new byte[hpf.ContentLength];
            //        hpf.InputStream.Read(myFileData, 0, hpf.ContentLength);


            //        using (SqlConnection con = new SqlConnection(Constants.DBString))
            //        {
            //            using (SqlCommand cmd = new SqlCommand("Quote_InsertInvoice", con))// This sp is uesd to insert documents to database
            //            {
            //                con.Open();
            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
            //                cmd.Parameters.AddWithValue("@DocName", System.IO.Path.GetFileName(hpf.FileName));
            //                cmd.Parameters.AddWithValue("@ContentType", hpf.ContentType);
            //                cmd.Parameters.AddWithValue("@Document", myFileData);
            //                cmd.Parameters.AddWithValue("@DataSize", hpf.ContentLength);
            //                cmd.Parameters.AddWithValue("@FolderName", "Task Documents");
            //                cmd.Parameters.AddWithValue("@UserID", sessionKeys.UID);
            //                cmd.Parameters.AddWithValue("@TaskID", int.Parse(ddlltTasks.SelectedValue));
            //                cmd.ExecuteScalar();
            //                //return Convert.ToInt32(cmd.ExecuteScalar());
            //            }
            //        }

            //    }
            //}

            //mdlPopTaskDocs.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    private void Download(int ID)
    {
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID={0}", ID);
            using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        HttpContext.Current.Response.ContentType = reader.GetString(0);
                        byte[] getContent = (byte[])reader[1];
                        HttpContext.Current.Response.BinaryWrite(getContent);
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + reader.GetString(2).Replace(" ", string.Empty));
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        lblErrMsg.Text = Resources.DeffinityRes.DocumentNotFound;
                        lblErrMsg.Visible = true;
                        lblErrMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = Resources.DeffinityRes.DocumentNotFound;//"The document is not found.  Possible reasons may be the other users have deleted the document you are looking for.";
                    }
                }
            }
        }
    }




    protected void ddlltTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrMsg.Visible = false;
        BindGrid();
        // mdlPopTaskDocs.Show();
    }
    #endregion
    protected void grdTaskDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
   
}