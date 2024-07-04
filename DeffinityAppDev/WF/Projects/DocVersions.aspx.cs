using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.BLL;
using System.Text;


public partial class DocVersions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gridFiles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        //lblMsg.Text = string.Empty;

        if (e.CommandName.Equals("PortalView"))
        {

            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            int intToggleProjectDoc = 1;


            if (commandArgs[1] == "1")
            {
                intToggleProjectDoc = 0;
            }




            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DEFFINITY_Document_PortalView", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", commandArgs[0]);
                    cmd.Parameters.AddWithValue("@projectDoc", intToggleProjectDoc);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        //lblMsg.Text = Resources.DeffinityRes.DoucmentDeleted;  //"Document deleted successfully.";
                        gridFiles.DataBind();
                       // NewTreeBinding();
                    }
                    else
                    {

                        //lblMsg.Text = Resources.DeffinityRes.DocumentDeleteFailed;  //"Document deletion failed. Possible reasons are, you may not have delete permission for the document or the file is already deleted.";
                    }
                }
            }
        }

        if (e.CommandName.Equals("Download"))
        {
            AC2P_DocumentsController _AC2P_DocumentsController = new AC2P_DocumentsController();
            _AC2P_DocumentsController.DocumentJournalInsert(Convert.ToInt32(e.CommandArgument.ToString()), sessionKeys.PortfolioID, sessionKeys.UID);


            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID={0}", e.CommandArgument);
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

                            //lblMsg.Text = Resources.DeffinityRes.DocumentNotFound;//"The document is not found.  Possible reasons may be the other users have deleted the document you are looking for.";
                        }
                    }
                }
            }
        }
        else if (e.CommandName.Equals("DeleteFile"))
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Deffinity_DeleteDocument", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@ContractorID", sessionKeys.UID);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        //lblMsg.Text = Resources.DeffinityRes.DoucmentDeleted;  //"Document deleted successfully.";
                        gridFiles.DataBind();
                       //NewTreeBinding();
                    }
                    else
                    {

                        //lblMsg.Text = Resources.DeffinityRes.DocumentDeleteFailed;  //"Document deletion failed. Possible reasons are, you may not have delete permission for the document or the file is already deleted.";
                    }
                }
            }
        }
        else if (e.CommandName.Equals("Select"))
        {

            //DocumentPermissionsList(e.CommandArgument.ToString());
            //ModalControlExtender1.Show();
        }
    }
    protected void gridFiles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvrEditRow = gridFiles.Rows[e.RowIndex];
        TextBox txtEditname = (TextBox)gridFiles.Rows[e.RowIndex].FindControl("txtEditName");
        sqlFileList.UpdateParameters["DocumentName"].DefaultValue = txtEditname.Text.Trim();
        sqlFileList.Update();
    }
    protected string GetIcon(string fileName)
    {
        string imageURL = string.Empty;
        string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
        switch (fileExtension.ToLower())
        {
            case "xls":
            case "xlsx":
                imageURL = "/WF/media/ico_excel.png";
                break;
            case "doc":
            case "docx":
                imageURL = "/WF/media/ico_word.png";
                break;
            case "jpeg":
            case "jpg":
            case "png":
            case "gif":
            case "bmp":
            case "ico":
            case "psd":
            case "tif":
            case "psp":
            case "dwg":
            case "dxf":
            case "3dm":
                imageURL = "/WF/media/ico_image.png";
                break;
            case "aac":
            case "aif":
            case "iff":
            case "m3u":
            case "midi":
            case "mp3":
            case "mpa":
            case "wma":
            case "mov":
            case "flv":
            case "avi":
            case "swf":
            case "vob":
            case "wmv":
                imageURL = "/WF/media/ico_media.png";
                break;
            case "7z":
            case "deb":
            case "gz":
            case "pkg":
            case "rar":
            case "sit":
            case "sitx":
            case "zip":
            case "zipx":
                imageURL = "/WF/media/ico_zip.png";
                break;
            case "txt":
                imageURL = "/WF/media/ico_notepad.png";
                break;
            case "pdf":
                imageURL = "/WF/media/ico_pdf.png";
                break;
            case "ppt":
            case "pptx":
                imageURL = "/WF/media/ico_powerpoint.png";
                break;
            case "vsd":
            case "vsdx":
                imageURL = "/WF/media/ico_vsd.png";
                break;
            default:
                imageURL = "/WF/media/ico_noimage.png";
                break;
        }
        return imageURL;
    }
    protected bool GetCheckInOutEnable(string CheckOut)
    {
        bool blnEnable = true;

        if (CheckOut.Trim() == "True")
        {
            blnEnable = false;

        }
        else
        {
            blnEnable = true;
        }
        return blnEnable;
    }
    protected bool CheckBoxInvisible()
    {
        bool blnEnable = true;
        if (Page.Request.Path.ToLower().Contains("WF/Portal/customerprojectdocuments.aspx"))
        {
            blnEnable = false;
        }

        return blnEnable;
    }
}
