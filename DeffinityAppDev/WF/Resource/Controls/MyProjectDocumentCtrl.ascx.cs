using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Deffinity.BLL;


public partial class controls_MyProjectDocumentCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            if (!IsPostBack)
                BindDocuments(txtDocName.Text.Trim());

        }
    }

    private void BindDocuments(string documentName)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBstring"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            con.Open();
            cmd = new SqlCommand("DEFFINITY_GETMYPROJECTDOCUMENTS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OwnerID", sessionKeys.UID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataView dataView = dt.DefaultView;
            if (documentName != string.Empty)
                dataView.RowFilter = "DocumentName LIKE '%" + documentName + "%'";
            gvMyDocs.EmptyDataText = "No documents found";
            gvMyDocs.DataSource = dataView;
            gvMyDocs.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }


    }
    protected string GetIcon(string fileName)
    {
        string imageURL = string.Empty;
        string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
        switch (fileExtension.ToLower())
        {
            case "xls":
            case "xlsx":
                imageURL = "~/WF/media/ico_excel.png";
                break;
            case "doc":
            case "docx":
                imageURL = "~/WF/media/ico_word.png";
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
                imageURL = "~/WF/media/ico_image.png";
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
                imageURL = "~/WF/media/ico_media.png";
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
                imageURL = "~/WF/media/ico_zip.png";
                break;
            case "txt":
                imageURL = "~/WF/media/ico_notepad.png";
                break;
            case "pdf":
                imageURL = "~/WF/media/ico_pdf.png";
                break;
            case "ppt":
            case "pptx":
                imageURL = "~/WF/media/ico_powerpoint.png";
                break;
            case "vsd":
            case "vsdx":
                imageURL = "~/WF/media/ico_vsd.png";
                break;
            default:
                imageURL = "~/WF/media/ico_noimage.png";
                break;
        }
        return imageURL;
    }
    protected void gvMyDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
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

                                // lblMsg.Text = Resources.DeffinityRes.DocumentNotFound;//"The document is not found.  Possible reasons may be the other users have deleted the document you are looking for.";
                            }
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
    protected void gvMyDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMyDocs.PageIndex = e.NewPageIndex;
        BindDocuments(txtDocName.Text.Trim());
    }
    protected void imgSearchDocument_Click(object sender, EventArgs e)
    {
        BindDocuments(txtDocName.Text.Trim());
    }
}