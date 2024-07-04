using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.IO;
using DeffinityManager;

public partial class controls_ChitChatCtrlHomeNew : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ctl00_MainContent_ddlPortfolio

        //txtTextShare.Visible = false;
        //btnMainShare.Visible = false;
        //divAttachfileAndLink.Visible = false;
        //divAttach.Visible = false;

        //if (sessionKeys.PortfolioID == 0)
        //{

        //    this.Visible = true;

        //}
        //else
        //{
        //    this.Visible = false;
        //}

        BindData();


    }

    public void ChitchatBind()
    {
        BindData();
    }

    private void BindData()
    {
        DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
        //List<DeffinityChat_SelectResult> GetChatData = objDeffinityChitChat.DeffinityChat_Select().ToList();
        if (Request.QueryString["project"] == null)
        {
            HiddenModule.Value = "customer";
            HiddenModuleID.Value = sessionKeys.PortfolioID.ToString();
        }
        else
        {
            HiddenModule.Value = "project";
            HiddenModuleID.Value = QueryStringValues.Project.ToString();
        }


        List<DeffinityChat_SelectBYModuleIDResult> GetChatData = objDeffinityChitChat.DeffinityChat_SelectBYModuleID(int.Parse(HiddenModuleID.Value), HiddenModule.Value).ToList();



        int userID = sessionKeys.UID;
        HiddenUserName.Value = sessionKeys.UName;

        HiddenUserPhoto.Value = GetNophoto(userID.ToString());


        HiddenProfile.Value = "WF/Admin/Userprofile.aspx?id=" + userID.ToString();
        HiddenUserID.Value = userID.ToString();

        //string[] names = new string[3] { "Matt", "Joanne", "Robert" };

        //string[] strArticleList = new string[3] { "First Article", "Second Article", "Third Article" };
        //string[] strCommentList = new string[3] { "First Article Comment1", "Second Article  Comment1", "Third Article  Comment1" };

        string strToday = "Today at 7:04 PM .";
        //string[] strArticleListCode = new string[3] { "784ae653_8d28_1c27_6cf5_883489d47896", "784ae653_8d28_1c27_6cf5_883489d47897", "73a972d7_d919_6543_a7c6_e1ed8efba764" };
        //string[] strCommentListCode = new string[3] { "49ebb847_6ef1_42d8_7372_c673c6a26cf2", "49ebb847_6ef1_42d8_7372_c673c6a26cf3", "49ebb847_6ef1_42d8_7372_c673c6a26cf4" };
        //string[] strCommentButton = new string[3] { "5e2988fa_4fa2_3013_d559_fc9b56829460", "5e2988fa_4fa2_3013_d559_fc9b56829461", "5e2988fa_4fa2_3013_d559_fc9b56829462" };


        string strText = string.Empty;
        bool blnCurrentUser = false;
        string strType = "file";

        //for (int i = 0; i < strArticleList.Length; i++)
        //{
        foreach (DeffinityChat_SelectBYModuleIDResult objItem in GetChatData)
        {

            strType = objItem.Type; // Type of the entry (share,file or link)
            strToday = objItem.ArticleDate.ToString();
            string divMainChildCOMID = "divMainChildCOM" + objItem.ArticleID;
            string divLikeCOMID = "divLikeCOM" + objItem.ArticleID;
            string divUnLikeCOMID = "divUnLikeCOM" + objItem.ArticleID;
            string divMainCOMID = "divMainCOM" + objItem.ArticleID;
            string divDateCOMID = "divDateCOM" + objItem.ArticleID;
            string divDelCOMID = "divDelCOM" + objItem.ArticleID;
            string divLikethisCOMID = "divLikethisCOM" + objItem.ArticleID;
            string lblMembersCOMID = "lblMembersCOM" + objItem.ArticleID;
            string strArticle = objItem.ArticleDesc;
            string strUserName = objItem.UserName;
            string strUserID = objItem.UserID.ToString();

            string strUserProfileUrl = "WF/Admin/Userprofile.aspx?id=" + strUserID;
            string strUserProfileImage = GetNophoto(strUserID);

            string strPopUserMain = @"""window.open('WF/Admin/Userprofile.aspx?id=','popup','width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no'); return false;""";
            strPopUserMain = strPopUserMain.Replace("id=", "id=" + strUserID);

            //for link
            string strLinkUrl = objItem.LinkUrl; // "http://google.com";
            string strLinkUrlName = objItem.LinkName;// "mygooglesite";

            //For file
            string strico_download = "fa fa-download";
            string strTypeofDocFormat = "fa fa-book";
            //string strDocumentName = "file:///C:/Documents%20and%20Settings/Administrator/Desktop/SQL_SPS.docx";
            string strDocumentName = objItem.FilePath;
            string strDownloadTxt = objItem.Filename; //"Download txt";
            string strDocumentSizeAndDesc = "( " + objItem.Filesize + "KB)" + objItem.Filedesc; //"(2 KB)SQL SPS Desc";

            int intCommentsList = 0; // Get the list of comments according article id
            int intCountLike = 0;
            int intCountMyLike = 0;
            string strPipe = " ";
            string strHideDelete = " style='display: none' ";
            //if (HiddenUserID.Value == userID.ToString())
            if (HiddenUserID.Value == objItem.UserID.ToString())
            {
                blnCurrentUser = true;
                strPipe = " |";
                // strHideDelete = " style='display: none' ";
                strHideDelete = " ";
            }
            List<DeffinityChatComment_SelectResult> GetCommentData = objDeffinityChitChat.DeffinityChatComment_Select(objItem.ArticleID).ToList();
            List<DeffinityArticleLike_SelectResult> GetArticleData = objDeffinityChitChat.DeffinityArticleLike_Select().ToList();

            //List<DeffinityArticleLike_SelectResult> GetArticleDataByArticle = GetArticleData.ToList().Where(objItem.ArticleID);
            var OutArticles = from tbArticle in GetArticleData
                              where tbArticle.ArticleID == objItem.ArticleID
                              select tbArticle;
            intCountLike = OutArticles.Count();

            var CurrentUserLike = from tbArticle in GetArticleData
                                  where tbArticle.ArticleID == objItem.ArticleID && tbArticle.UserID == Convert.ToInt32(HiddenUserID.Value)
                                  select tbArticle;
            intCountMyLike = CurrentUserLike.Count();
            intCommentsList = GetCommentData.Count;

            //string strHiddenCount = "<input type='hidden' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
            //string strHiddenCount = "<input type='text' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
            strText = strText + "<article class='timeline-story'> <i class='fa-paper-plane-empty block-icon'></i>";
            strText = strText + "<header>";
            if (strType == "share")
            {
                //Share text
                strText = strText + "<div class='coment_blk' id=" + divMainChildCOMID + ">";
                //strText = strText + "<a href='" + strUserProfileUrl + "'><img alt='" + strUserName + "' src='" + strUserProfileImage + "'/></a>";
                //strText = strText + "<div><a href='" + strUserCommentProfileUrl + "' onclick=" + strPopUser + ">";
                strText = strText + "<a  class='user-img' href='" + strUserProfileUrl + "'   onclick=" + strPopUserMain + ">" + "<img class='img-circle'  width='40' alt='" + strUserName + "' src='" + strUserProfileImage + "'/></a>";
                strText = strText + "<div class='user-details'> <a  class='' href='" + strUserProfileUrl + "'   onclick=" + strPopUserMain + "> " + strUserName;
                strText = strText + "</a> <time class=chat_timestamp id=" + divDateCOMID + ">" + strToday + "</time>";
                strText = strText +  "</header>";
                //strText = strText + "<a href='" + strUserProfileUrl + "'>" + strUserName + "</a>";
                //strText = strText + "<a href='" + strUserProfileUrl + "'   onclick=" + strPopUserMain + ">" + "<img alt='" + strUserName + "' src='" + strUserProfileImage + "'/></a>";
                strText = strText + "<div class='story-content'>";
                strText = strText + "<p>" + strArticle + "</p>";
               
                string strBuildLinks = BuildLinks(intCountLike, intCommentsList, intCountMyLike, divMainCOMID, divMainChildCOMID, divLikeCOMID, divLikethisCOMID, divUnLikeCOMID, lblMembersCOMID, divDelCOMID, strHideDelete, strPipe);
                strText = strText + strBuildLinks;
            }
            else if (strType == "link")
            {
                strText = strText + "<div class='story-comment coment_blk' id=" + divMainChildCOMID + ">";
                strText = strText + "<p><a  class='comment-user-img' href='" + strUserProfileUrl + "'    onclick=" + strPopUserMain + "><img  width='40' class='img-circle' alt='" + strUserName + "' src='" + strUserProfileImage + "'/></a><a href='" + strUserProfileUrl + "'    onclick=" + strPopUserMain + ">" + strUserName + "</a> posted a link.<br/><br/>";
                strText = strText + "<a title='" + strLinkUrl + "' href='" + strLinkUrl + "' target=_blank><i class='fa fa-link'></i> </a>";
                strText = strText + "<a href='" + strLinkUrl + "' target=_blank>" + strLinkUrlName + "</a> . " + strLinkUrl + "</p>";
                strText = strText + "<div class=chat_timestamp id=" + divDateCOMID + ">" + strToday + "</div>";

                string strBuildLinks = BuildLinks(intCountLike, intCommentsList, intCountMyLike, divMainCOMID, divMainChildCOMID, divLikeCOMID, divLikethisCOMID, divUnLikeCOMID, lblMembersCOMID, divDelCOMID, strHideDelete, strPipe);
                strText = strText + strBuildLinks;

            }
            else if (strType == "file")
            {
                strText = strText + "<div class='story-comment coment_blk' id='" + divMainChildCOMID + "'><p>";
                strText = strText + "<a  class='comment-user-img' href='" + strUserProfileUrl + "'><img  width='40' class='img-circle' alt='" + strUserName + "' src='" + strUserProfileImage + "'/></a>";
                strText = strText + "<a  class='comment-user-img' href='" + strUserProfileUrl + "'>" + strUserName + "</a> posted a file.<br/><br/>";
                //strText = strText + "<img class='link_icon' src='" + strTypeofDocFormat + "'/></a>";
                strText = strText + "<a href='" + strDocumentName + "'></a>";
                strText = strText + "<a href='" + strDocumentName + "'>";
                strText = strText + "<i class='" + strico_download + "'/>" + strDownloadTxt + "</a>" + " " + strDocumentSizeAndDesc + "</p>";
                strText = strText + "<div class='chat_timestamp' id='" + divDateCOMID + "'>" + strToday + "</div>";
                string strBuildLinks = BuildLinks(intCountLike, intCommentsList, intCountMyLike, divMainCOMID, divMainChildCOMID, divLikeCOMID, divLikethisCOMID, divUnLikeCOMID, lblMembersCOMID, divDelCOMID, strHideDelete, strPipe);
                strText = strText + strBuildLinks;
            }

            //if  any comments exists for article,list out the comments
            //Comment list starts
            if (GetCommentData != null && GetCommentData.Count() > 0)
            {
                strText = strText + "<ul class='list-unstyled story-comments'>";
                foreach (DeffinityChatComment_SelectResult objCommentItem in GetCommentData)
                //if (intCommentsList > 0)
                {
                    strText = strText + "<li>";
                    //string commentID = "b59cb3e1_5bb9_9d67_0ee7_6cb87217f5ef" + i;
                    string commentID = objCommentItem.ArticleCommentID;
                    string divCommentTextCOMID = "divCommentTextCOM" + commentID;
                    string divDateCOMIDComent = "divDateCOM" + commentID;
                    //string strCommentedUser = "Mahath Mrinendra Marpuri";
                    string strCommentedUser = objCommentItem.UserName;
                    string strUserComment = objCommentItem.CommentDesc; //"This is my first comment";
                    string strCommentedUserID = objCommentItem.UserID.ToString();// "25";
                    string strCommentedDate = objCommentItem.CommentDate.ToString();// "Today at 1:36 PM.";
                    string strCommentedUserPhoto = GetNophoto(objCommentItem.UserID.ToString());
                    string strUserCommentProfileUrl = "WF/Admin/Userprofile.aspx?id=" + strCommentedUserID;
                    string strUserCommentProfileImage = strCommentedUserPhoto;
                    string divDelComtID = "divDelComtCOM" + commentID;
                    strText = strText + "<div class='story-comment user_cmt_blk1' id='" + divCommentTextCOMID + "'>";

                    //var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + strCommentedUserID+'\''+ ',' + '\'UserProfile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'
                    string strPopUser = @"""window.open('WF/Admin/Userprofile.aspx?id=','popup','width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no'); return false;""";
                    strPopUser = strPopUser.Replace("id=", "id=" + strCommentedUserID);

                    //strText = strText + "<a href='" + strUserCommentProfileUrl + "' >";
                    strText = strText + "<a class='xe-user-img' href='" + strUserCommentProfileUrl + "' onclick=" + strPopUser + ">";
                    strText = strText + "<img class='img-circle' width='40' alt='" + strCommentedUser + "' src='" + strUserCommentProfileImage + "'/></a>";
                    strText = strText + "<div class='user-details'><a href='" + strUserCommentProfileUrl + "' onclick=" + strPopUser + ">" + strCommentedUser + "";
                    strText = strText + " <time class='chat_timestamp' id='" + divDateCOMIDComent + "'>" + strCommentedDate + "</time></a></div>";
                    strText = strText + " " + strUserComment + "<br/><br/>";

                    if (HiddenUserID.Value == strCommentedUserID)
                    {
                        strText = strText + " <div  id='" + divDelComtID + " class='chat_links' ><a href='javascript:removeCommentOfArticle(" + divMainChildCOMID + "," + divCommentTextCOMID + ")'>Delete</a></div></div>";
                        //strText = strText + " <div   style='display: none'  id='" + divDelComtID + " class='chat_links' ><a href='javascript:removeCommentOfArticle(" + divMainChildCOMID + "," + divCommentTextCOMID + ")'>Delete</a></div></div>";
                    }
                    else
                    {
                        strText = strText + " <div style='display: none' id='" + divDelComtID + " class='chat_links' ><a href='javascript:removeCommentOfArticle(" + divMainChildCOMID + "," + divCommentTextCOMID + ")'>Delete</a></div></div>";
                    }
                    strText = strText + "</li>";
                }
                //Comments textbox
                if (intCommentsList > 0)
                {
                    strText = strText + "<li>";
                    Guid obj = Guid.NewGuid();
                    //int userID = 23;
                    //HiddenUserName.Value = "Dinesh Kumar Marpuri";
                    //HiddenUserPhoto.Value = "media/DineshPhoto.jpg";
                    //HiddenProfile.Value = "Userprofile.aspx?id=" + userID.ToString();
                    //HiddenUserID.Value = "23";

                    //string NewCommentID = "3d33435c_6df4_1af3_c6c3_b9dd4712117c"; // This may need to create with guid on the server side
                    string NewCommentID = obj.ToString().Replace("-", "_");

                    string divCommentTextCOMIDNew = "divCommentTextCOM" + NewCommentID;
                    string txtCommentIDNew = "txtComment" + NewCommentID;

                    strText = strText + "<div class='story-comment user_cmt_blk1' id='" + divCommentTextCOMIDNew + "'>";
                    //strText = strText + "<div style='display: none' class='user_cmt_blk' id='" + divCommentTextCOMIDNew + "'>";

                    strText = strText + "<a class='xe-user-img' href='" + HiddenProfile.Value + "'>";
                    strText = strText + "<img width='40' class='img-circle' alt='" + HiddenUserName.Value + "' src='" + HiddenUserPhoto.Value + "'/></a>";
                    strText = strText + "<input style='height:30px' id='" + txtCommentIDNew + "' /> ";
                    strText = strText + "<input id='btnComments' title='Write a comment...' class='btn btn-secondary' type='button' value='Comment' onclick='OnComment_InsertClick(" + divMainChildCOMID + "," + txtCommentIDNew + "," + divMainChildCOMID + "," + divCommentTextCOMIDNew + "," + divMainCOMID + ");'" + " /></div>"; ;
                    strText = strText + "</li>";
                }
                strText = strText + "</ul>";
                intCommentsList = 0;
                //Comment list ends    
                //strText = strText + "</div></div>";
                strText = strText + "</div>";
                strText = strText + "</div>";
                strText = strText + "</article>";
            }
        }
        divMainCOM.InnerHtml = strText;
    }
    private string BuildLinks(int intCountLike, int intCommentsList, int intCountMyLike, string divMainCOMID, string divMainChildCOMID, string divLikeCOMID, string divLikethisCOMID, string divUnLikeCOMID, string lblMembersCOMID, string divDelCOMID, string strHideDelete, string strPipe)
    {

        string strPop = @"""window.open('WF/Admin/CustomersLikethis.aspx?id=','popup','width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no'); return false;""";
        strPop = strPop.Replace("id=", "id=" + divLikethisCOMID);

        string strText = string.Empty;
        strText = "<div class='story-options-links form-inline'>";
        //If there are comments for article hide the comment box
        if (intCommentsList > 0)
        {
            strText = strText + "<span  style='display: none' class=chat_links id=" + divMainCOMID + ">";
        }
        else
        {
            strText = strText + "<span  class=chat_links id=" + divMainCOMID + ">";
            //strText = strText + "<div  style='display: none' class=chat_links id=" + divMainCOMID + ">";

        }

        strText = strText + "<a  title=\'Leave a comment\' href='javascript:DisplayCommentBox(" + divMainChildCOMID + "," + divMainCOMID + ")'><i class='linecons-comment'></i> Comment </a> |</span>";



        if (intCountMyLike != 0)
        {
            strText = strText + "<span class=chat_links id=" + divLikeCOMID + "  style='display: none'>";
        }
        else
        {

            //strText = strText + "<div style='display: none' class=chat_links id=" + divLikeCOMID + ">";
            strText = strText + "<span class=chat_links id=" + divLikeCOMID + ">";

        }

        strText = strText + "<a href='javascript:LikeOrUnlike(" + divMainChildCOMID + "," + divLikethisCOMID + "," + divLikeCOMID + "," + divUnLikeCOMID + ",1)'><i class='linecons-thumbs-up'></i> Like </a>" + strPipe + "</span>";



        if (intCountMyLike != 0)
        {
            strText = strText + "<span class=chat_links id=" + divUnLikeCOMID + ">";
            //strText = strText + "<div style='display: none' class=chat_links id=" + divUnLikeCOMID + ">";


        }
        else
        {
            strText = strText + "<span class=chat_links id=" + divUnLikeCOMID + " style='display: none'>";
        }
        strText = strText + "<a href='javascript:LikeOrUnlike(" + divMainChildCOMID + "," + divLikethisCOMID + "," + divLikeCOMID + "," + divUnLikeCOMID + ",0)'>UnLike</a>" + strPipe + "</span>";

        strText = strText + "<span " + strHideDelete + " class=chat_links id=" + divDelCOMID + "><a href='javascript:removedivMainChild(" + divMainChildCOMID + ")'><i class='linecons-trash'></i> Delete </a></span>";
        
        string strLike = "<div class='story-options-links'>";

        if (intCountMyLike == 1 && intCountLike == 1)
        {
            intCountLike--;
            string strHiddenCount = "<input type='hidden' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
            strLike = "<span class=likeunlike id=" + divLikethisCOMID + " style='display: block'><b>&nbsp;</b>You like this.</span>" + strHiddenCount;

        }
        else
        {
            if (intCountMyLike != 0)
            {
                intCountLike--;
                //divLikethisCOMID

                string strHiddenCount = "<input type='hidden' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
                //strLike = "<div class='clr'></div><div class=likeunlike id=" + divLikethisCOMID + " style='display: block'><b>&nbsp;</b>You and " + "<a href='" + "CustomersLikethis.aspx?id=" + divLikethisCOMID + "' target=_blank>" + intCountLike + "</a>" + " others like this." + "</div>" + strHiddenCount;
                strLike = "<span class=likeunlike id=" + divLikethisCOMID + " style='display: block'><b>&nbsp;</b>You and " + "<a href='" + "CustomersLikethis.aspx?id=" + divLikethisCOMID + "'  onclick=" + strPop + ">(" + intCountLike + ")</a>" + "" + "</span>" + strHiddenCount;
                //<a href="CustomersLikethis.aspx" onclick="window.open('CustomersLikethis.aspx','popup','width=500,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">About</a>
            }
            else
            {
                if (intCountLike == 0)
                {
                    string strHiddenCount = "<input type='hidden' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
                    //   strLike = "<div class='clr'></div><div class=likeunlike id=" + divLikethisCOMID + " style='display: none'>" + "<a href='" + "CustomersLikethis.aspx?id=" + divLikethisCOMID + "' target=_blank>" + "<b>&nbsp;</b>" + intCountLike + "</a>" + " people like this." + "</div>" + strHiddenCount;
                    strLike = "<span class=likeunlike id=" + divLikethisCOMID + " style='display: none'>" + "<a href='" + "WF/Admin/CustomersLikethis.aspx?id=" + divLikethisCOMID + "'  onclick=" + strPop + ">" + "<b>&nbsp;</b>(" + intCountLike + ")</a>" + "" + "</span>" + strHiddenCount;

                }
                else
                {
                    string strHiddenCount = "<input type='hidden' id='" + lblMembersCOMID + "' value='" + intCountLike.ToString() + "' />";
                    // strLike = "<div class='clr'></div><div class=likeunlike id=" + divLikethisCOMID + " style='display: block'>" + "<a href='" + "CustomersLikethis.aspx?id=" + divLikethisCOMID + "' target=_blank>" + "<b>&nbsp;</b>" + intCountLike + "</a>" + " people like this." + "</div>" + strHiddenCount;
                    strLike = "<span class=likeunlike id=" + divLikethisCOMID + " style='display: block'>" + "<a href='" + "WF/Admin/CustomersLikethis.aspx?id=" + divLikethisCOMID + "'  onclick=" + strPop + ">" + "<b>&nbsp;</b>(" + intCountLike + ")</a>" + "" + "</span>" + strHiddenCount;
                }
            }
        }

        strLike = strLike + "</span>";
        strText = strText +" </span>";
        if (intCommentsList == 0)
        {
            strText = strText + strLike + "<div class=share_div></div></div>";
        }
        else
        {
            strText = strText + strLike + "<div class=share_div></div>";
        }

        return strText;
    }

    protected string GetNophoto(string strPath)
    {
        string strFileName = "../UploadData/Users/ThumbNailsMedium/user_" + strPath + ".png";
        if (!File.Exists((Server.MapPath(strFileName))))
        {
            strFileName = "../UploadData/Users/ThumbNailsMedium/user_" + strPath + ".jpg";
            if (!File.Exists((Server.MapPath(strFileName))))
            {
                strFileName = "../UploadData/Users/ThumbNailsMedium/user_0.png";
            }
        }
        return strFileName;
    }
}