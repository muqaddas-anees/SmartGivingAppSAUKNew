// According to http://msdn2.microsoft.com/en-us/library/system.web.httppostedfile.aspx
// "Files are uploaded in MIME multipart/form-data format. 
// By default, all requests, including form fields and uploaded files, 
// larger than 256 KB are buffered to disk, rather than held in server memory."
// So we can use an HttpHandler to handle uploaded files and not have to worry
// about the server recycling the request do to low memory. 
// don't forget to increase the MaxRequestLength in the web.config.
// If you server is still giving errors, then something else is wrong.
// I've uploaded a 1.3 gig file without any problems. One thing to note, 
// when the SaveAs function is called, it takes time for the server to 
// save the file. The larger the file, the longer it takes.
// So if a progress bar is used in the upload, it may read 100%, but the upload won't
// be complete until the file is saved.  So it may look like it is stalled, but it
// is not.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Upload handler for uploading files.
/// </summary>
public class Upload : IHttpHandler, IReadOnlySessionState
{    
    public Upload()
    {
    }

    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            for (int j = 0; j < context.Request.Files.Count; j++)
            {
                HttpPostedFile uploadFile = context.Request.Files[j];
                if (context.Request.Files.Count > 0)
                {
                    SqlConnection myConnection = new SqlConnection(connectionString.retrieveConnString());
                    using (myConnection)
                    {
                        SqlCommand myCommand = new SqlCommand();
                        myCommand = new SqlCommand("DN_ProjectUploadInsertNew", myConnection);
                        using (myCommand)
                        {
                            myCommand.CommandType = CommandType.StoredProcedure;
                            myCommand.Parameters.Add(new SqlParameter("@DocumentName", uploadFile.FileName));
                            myCommand.Parameters.Add(new SqlParameter("@SourceFileName", uploadFile.FileName));
                            myCommand.Parameters.Add(new SqlParameter("@ContentType", uploadFile.ContentType));
                            myCommand.Parameters.Add(new SqlParameter("@DataSize", uploadFile.ContentLength));
                            myCommand.Parameters.Add(new SqlParameter("@ProjectReference", context.Request.QueryString["project"]));
                            myCommand.Parameters.Add(new SqlParameter("@ApplicationSection", "P"));
                            myCommand.Parameters.Add(new SqlParameter("@MasterID", context.Request.QueryString["folderID"]));
                            myCommand.Parameters.Add(new SqlParameter("@ContractorID", context.Request.QueryString["contractorID"]));
                            myCommand.Parameters.AddWithValue("SDID", context.Request.QueryString["IncidentID"]);
                            //Convert the file content to the byte array to store in the database.
                            byte[] myFileData = new byte[uploadFile.ContentLength];
                            uploadFile.InputStream.Read(myFileData, 0, uploadFile.ContentLength);
                            myCommand.Parameters.Add(new SqlParameter("@Document", myFileData));

                            myCommand.Connection.Open();
                            myCommand.ExecuteNonQuery();
                            myCommand.Connection.Close();
                            myCommand.Dispose();
                        }
                    }
                }
            }
        }
    }

    #endregion
}
