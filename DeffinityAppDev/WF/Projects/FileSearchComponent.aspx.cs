using System;
using Deffinity.DocumentSearch;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

public partial class FileSearchComponent : System.Web.UI.Page
{

    delegate void displayDelegate(SearchResultList searchResults);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["searchString"]) && !IsPostBack)
        {
            txtSearchBox.Text = Request.QueryString["searchString"].ToString();
            searchTextFiles(new displayDelegate(DisplayResults));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSearchBox.Text))
            searchTextFiles(new displayDelegate(DisplayResults));
    }

    void DisplayResults(SearchResultList searchResults)
    {
        StringBuilder sb = new StringBuilder();
        if (searchResults.Count <= 0)
            sb.Append("<div><h4 class='none'>No results found..</h4></div>");
        else
            foreach (SearchResult result in searchResults)
            {
                if (string.IsNullOrEmpty(result.version.Trim()))
                    result.version = "1";
                sb.Append(string.Format("<div style='font-size:10px;'><span style='font-size:10px;'><img src='{0}' alt='File Icon' style='vertical-align:bottom'/>", GetIcon(result.fileName)));
                sb.Append(string.Format("<strong><a style='color:blue;padding-left:5px;text-decoration:underline;' target='_blank' href='Download.aspx?FileID={1}'>{0}</a></strong></span><br/>", result.fileName, result.fileID));
                sb.Append(string.Format("<table ><tr><td width='200px' style='padding:0px 8px 0px 20px;'>{0} {1}</td>", "Uploaded By:", Server.HtmlEncode(result.uploadedBy)));
                sb.Append(string.Format("<td style='padding-right:10px' >{0} {1}</td>", "Uploaded On:", result.uploadedTime));
                sb.Append(string.Format("<td>{0} {1}</td>", "Version:", result.version));
                sb.Append("</tr></table></div>");
            }
        divSearchResults.InnerHtml = sb.ToString();
    }

    protected string GetIcon(string fileName)
    {
        string imageURL = string.Empty;
        string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
        switch (fileExtension.ToLower())
        {
            case "xls":
            case "xlsx":
                imageURL = "media/ico_excel.png";
                break;
            case "doc":
            case "docx":
                imageURL = "media/ico_word.png";
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
                imageURL = "media/ico_image.png";
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
                imageURL = "media/ico_media.png";
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
                imageURL = "media/ico_zip.png";
                break;
            case "txt":
                imageURL = "media/ico_notepad.png";
                break;
            case "pdf":
                imageURL = "media/ico_pdf.png";
                break;
            case "ppt":
            case "pptx":
                imageURL = "media/ico_powerpoint.png";
                break;
            case "vsd":
            case "vsdx":
                imageURL = "media/ico_vsd.png";
                break;
            default:
                imageURL = "media/ico_noimage.png";
                break;
        }
        return imageURL;
    }

    void searchTextFiles(Delegate dispResults)
    {
        Dictionary<int, string> searchedFiles = new Dictionary<int, string>();
        string searchString = txtSearchBox.Text;
        if (!string.IsNullOrEmpty(searchString))
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DEFFINITY_SearchInFileAll", conn))
                {
                    cmd.Parameters.AddWithValue("SearchKey", searchString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        using (DataTableReader tableReader = table.CreateDataReader())
                        {
                            while (tableReader.Read())
                            {
                                searchedFiles.Add(Convert.ToInt32(tableReader["ID"]), tableReader["DocumentName"].ToString());
                            }
                        }
                    }
                }
            }

            SearchInFiles fileSearch = new SearchInFiles();
            SearchResultList searchResults = fileSearch.getAc2pDocument(searchedFiles, searchString);
            if (dispResults != null)
                dispResults.DynamicInvoke(searchResults);
        }
    }
}
