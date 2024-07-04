using DeffinityManager;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class DeffinityChatService : System.Web.Services.WebService
{
    DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();

    #region "File upload"
    [WebMethod]
    public bool UploadFile(string PictureName, byte[] PictureStream)
    {
        FileStream fileStream = null;
        BinaryWriter writer = null;
        string filePath;

        try
        {

            filePath = HttpContext.Current.Server.MapPath(".") + System.Configuration.ConfigurationManager.AppSettings["PictureUploadDirectory"] + PictureName;

            if (PictureName != string.Empty)
            {
                fileStream = File.Open(filePath, FileMode.Create);
                writer = new BinaryWriter(fileStream);
                writer.Write(PictureStream);
            }

            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
        finally
        {
            if (fileStream != null)
                fileStream.Close();
            if (writer != null)
                writer.Close();
        }
    }
    #endregion "File upload"
    #region "Share Text Functions"
    [WebMethod()]
    public string MainShareText_Insert(string strUserName,string userID,string strTopicID,string strArticleDesc,string strDate,string Module,string ModuleID)
    {
        
        objDeffinityChitChat.DeffinityChat_InsertShare(System.Convert.ToInt32(userID), strTopicID, strArticleDesc, System.DateTime.Now,Module,System.Convert.ToInt32(ModuleID));

        string strText = SendMailSharedText(strUserName, userID, strArticleDesc, strTopicID, System.DateTime.Now.ToString());

        return strTopicID;

    }
    [WebMethod()]
    public string MainShareText_Remove(string TopicID)
            {
        string strTopicID = TopicID;
        strTopicID=strTopicID.Replace("divMainChildCOM", "");
        objDeffinityChitChat.DeffinityChat_Delete(strTopicID.Trim());
        return TopicID;

    }

    [WebMethod()]
    public string MainShareText_Remove_Success()
    {
        //return "Hello " + name;
        return "successData";

    }
    public string MainShareText_Insert_Success()
    {

        return "successData";

    }
    #endregion "Share Text Functions"

    [WebMethod()]
    public string MainShareLink_Insert_Success()
    {
        
        return "successData";

    }
    [WebMethod()]
    public string MainShareLink_Insert(string strUserName,string strUserID, string LinkURL, string LinkName, string TopicID,string Module,string ModuleID)
    {
        objDeffinityChitChat.DeffinityChat_InsertLink(System.Convert.ToInt32(strUserID), TopicID, System.DateTime.Now, LinkURL, LinkName, Module, System.Convert.ToInt32(ModuleID));
        string strText = SendMailLink(strUserName, strUserID, LinkURL, LinkName, TopicID, System.DateTime.Now.ToString());
        return TopicID;

    }
    //private void UploadDocuments()
    //{
    //    try
    //    {
    //        // Get the HttpFileCollection
    //        HttpFileCollection hfc = Request.Files;
    //        for (int i = 0; i < hfc.Count; i++)
    //        {
    //            HttpPostedFile hpf = hfc[i];
    //            if (hpf.ContentLength > 0)
    //            {
    //                //hpf.SaveAs(Server.MapPath("MyFiles") + "\\" +
    //                //  System.IO.Path.GetFileName(hpf.FileName));
    //                //Response.Write("<b>File: </b>" + hpf.FileName + "  <b>Size:</b> " +
    //                //     hpf.ContentLength + "  <b>Type:</b> " + hpf.ContentType + " Uploaded Successfully <br/>");



    //                byte[] myFileData = new byte[hpf.ContentLength];
    //                hpf.InputStream.Read(myFileData, 0, hpf.ContentLength);
    //                AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
    //                //string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", , folderID, sessionKeys.UID, sessionKeys.IncidentID);
    //                AC2PDocumentsController.DN_ProjectUploadInsertNew(sessionKeys.Project.ToString(), System.IO.Path.GetFileName(hpf.FileName), myFileData, System.IO.Path.GetFileName(hpf.FileName), hpf.ContentType, "P", hpf.ContentLength, folderID, sessionKeys.UID, sessionKeys.IncidentID);

    //            }
    //        }
    //        NewTreeBinding();
    //        //Infragistics.WebUI.UltraWebNavigator.Node SelectedFolder = null;
    //        //SelectedFolder = UltraWebTree1.SelectedNode;
    //        //SelectedFolder.Text = hfc.Count.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //    }


    //}

    #region "Share Text Functions"
    [WebMethod()]
    public string MainShareFile_Insert(string strUserName, string strUserID, string fileUpload, string fileName, string fileDesc, string TopicID,string Module,string ModuleID)
    //public string MainShareFile_Insert(string strUserID, HttpPostedFile fileUpload, string fileName, string fileDesc, string TopicID)
    {
        //string[] names = new string[3] { "C:\text.txt", "Joanne", "Robert" };
        //HttpFileCollection hfc = null;// (HttpFileCollection)names;


       // HttpPostedFile fileUploadNew = hfc;
        objDeffinityChitChat.DeffinityChat_InsertFile(System.Convert.ToInt32(strUserID), TopicID, System.DateTime.Now, fileUpload, fileName, "2300 KB", fileDesc, Module, System.Convert.ToInt32(ModuleID));
        string strText = SendMailFile(strUserName, strUserID, fileUpload, fileName, "2300 KB", TopicID, System.DateTime.Now.ToString());
        return TopicID;

    }

    [WebMethod()]
    public string LikeArticle_Insert(string strUserID, string TopicID)
    {
        string strTopicID = TopicID;
        strTopicID = strTopicID.Replace("divMainChildCOM", "");
        objDeffinityChitChat.DeffinityArticleLike_Insert(System.Convert.ToInt32(strUserID), strTopicID);

        return TopicID;

    }
    [WebMethod()]
    public string LikeArticle_Delete(string strUserID, string TopicID)
    {
        string strTopicID = TopicID;
        strTopicID = strTopicID.Replace("divMainChildCOM", "");
        objDeffinityChitChat.DeffinityArticleLike_Delete(System.Convert.ToInt32(strUserID), strTopicID);

        return TopicID;

    }
    [WebMethod()]
    public string LikeArticle_Insert_Success()
    {

        return "successData";

    }
    [WebMethod()]
    public string MainShareFile_Insert_Success()
    {
        
        return "successData";

    }


    #endregion #region "Share Text Functions"
    #region "Add,Remove Comment Functions"
    [WebMethod()]
    public string Comment_Insert(string strUserID,string txtComment, string CommentID, string TopicID)
    {


      
        string strTopicID = TopicID;
        strTopicID=strTopicID.Replace("divMainChildCOM", "");
        objDeffinityChitChat.DeffinityChatComment_Insert(System.Convert.ToInt32(strUserID), strTopicID, CommentID, System.DateTime.Now, txtComment);
        return TopicID + '-' + CommentID + '-' + txtComment;

    }
    [WebMethod()]
    public string Comment_Insert_Success()
    {

        return "successData";

    }

    [WebMethod()]
    public string Comment_Remove(string CommentID, string TopicID)
    {
        string strCommentID = CommentID;
        strCommentID = strCommentID.Replace("divCommentTextCOM", "");

        objDeffinityChitChat.DeffinityChatComment_Delete(strCommentID);

        return TopicID + '-' + CommentID ;

    }
    [WebMethod()]
    public string Comment_Remove_Success()
    {

        return "successData";

    }
    #endregion "Add,Remove Comment Funtions"

    #region "Mailing Related"


    private string SendMailSharedText(string strUserName, string strUserID,string strDesc,string TopicID,string strDate)
    {
        string strText = string.Empty;

        string Weburl=System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        string chatLogo = Weburl+"/media/email_head.png";
        string ProfileImage = Weburl + "/WF/UploadData/Users/ThumbNailsMedium/user_" + strUserID + ".jpg";
        string ProfileUrl = Weburl + "/DeffinityChat/Userprofile.aspx?id=" + strUserID;
        strText = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/CustomerAdmin/EmailTemplates/chatsharetext.htm"));
        strText = strText.Replace("[chatlogo]", chatLogo);
        strText = strText.Replace("[name]", strUserName);
        strText = strText.Replace("[articledesc]", strDesc);
        strText = strText.Replace("[profileimage]", ProfileImage);
        strText = strText.Replace("[profileurl]", ProfileUrl);
        strText = strText.Replace("[timestamp]", strDate); 
        
        //strText = strText + "<span style='font-size:9.0pt;font-family:'Arial','sans-serif';";
        //strText = strText + "mso-fareast-font-family:'Times New Roman';color:#222222;mso-ansi-language:EN-US;";
        //strText = strText + "mso-fareast-language:EN-US;mso-bidi-language:AR-SA'><img id='_x0000_i1025'";
        //strText = strText + "src='https://ap1.salesforce.com/img/chatterEmail/digestLogo.png ";
        //strText = strText + " alt='Deffinity Chatter' title='Deffinity Chatter'></span>";
        //strText = strText + "<span style='font-size:9.0pt;font-family:'Arial','sans-serif'; mso-fareast-font-family:'Times New Roman';color:#222222;mso-ansi-language:EN-US;";
        //strText = strText + "mso-fareast-language:EN-US;mso-bidi-language:AR-SA'><img id='_x0000_i1025' ";
        //strText = strText + "src='https://ap1.salesforce.com/img/chatterEmail/digestLogo.png' ";
        //strText = strText + " alt='Deffinity Chatter' title='Deffinity Chatter'></span>";

        //strText = strText + "<h3>Daily Digest for " + strUserName + "</h3>";
        //strText = strText + "<div class='coment_blk' id='divMainChildCOM"+TopicID+"'>";
        //strText = strText + "<a href='DeffinityChat/Userprofile.aspx?id="+strUserID+"'>";
        //strText = strText + "<img alt='" + strUserName + "' src='UploadData/Users/ThumbNailsMedium/user_" + strUserID + ".jpg' /></a> ";
        //strText = strText + "<a href='DeffinityChat/Userprofile.aspx?id=25'>" + strUserName + "</a><p> " + strDesc + " </p>";


        //strText = strText + "<div class='chat_timestamp' id='divDateCOM" + TopicID + "'>" + strDate + "</div>";
        //strText = strText + "<div class='share_div'></div></div>";

        return strText;

    }
    private string SendMailLink(string strUserName, string strUserID, string strLink, string strLinkName, string TopicID, string strDate)
    {
        string strText = string.Empty;

        string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        string chatLogo = Weburl + "/media/email_head.png";
        string ProfileImage = Weburl + "/WF/UploadData/Users/ThumbNailsMedium/user_" + strUserID + ".jpg";
        string ProfileUrl = Weburl + "/DeffinityChat/Userprofile.aspx?id=" + strUserID;
        string linkicon = Weburl + "/media/link.gif";
        strText = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/CustomerAdmin/EmailTemplates/chatsharelink.htm"));


        strText = strText.Replace("[chatlogo]", chatLogo);
        strText = strText.Replace("[name]", strUserName);
        strText = strText.Replace("[timestamp]", strDate);
        strText = strText.Replace("[profileimage]", ProfileImage);
        strText = strText.Replace("[profileurl]", ProfileUrl);
        strText = strText.Replace("[linkurl]", strLink);
        strText = strText.Replace("[linkname]", strLinkName);
        strText = strText.Replace("[linkicon]", linkicon);

        return strText;
    }
    private string SendMailFile(string strUserName, string strUserID, string strFileName,string strFileDesc,string strFileSize, string TopicID, string strDate)
    {
        
        string strText = string.Empty;

        string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        string chatLogo = Weburl + "/media/email_head.png";
        string ProfileImage = Weburl + "/WF/UploadData/Users/ThumbNailsMedium/user_" + strUserID + ".jpg";
        string ProfileUrl = Weburl + "/DeffinityChat/Userprofile.aspx?id=" + strUserID;
        string downloadicon = Weburl + "/media/ico_download.png";
        strText = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/CustomerAdmin/EmailTemplates/chatsharefile.htm"));

        string fileext = System.IO.Path.GetExtension(strFileName);
        strText = strText.Replace("[chatlogo]", chatLogo);
        strText = strText.Replace("[name]", strUserName);
        strText = strText.Replace("[timestamp]", strDate);
        strText = strText.Replace("[profileimage]", ProfileImage);
        strText = strText.Replace("[profileurl]", ProfileUrl);
        strText = strText.Replace("[downloadicon]", downloadicon);
        strText = strText.Replace("[downloadpath]", strFileName);
        strText = strText.Replace("[filename]", strFileDesc);
        strText = strText.Replace("[filesize]", strFileSize);
        strText = strText.Replace("[fileext]", fileext);
        strText = strText.Replace("[downloadpath]", strFileName); //Change later            
        return strText;
    }
    #endregion "Mailing Related"
}