using DeffinityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using UserMgt.DAL;

namespace DeffinityAppDev.WF.Projects.webservices
{
    /// <summary>
    /// Summary description for ChitchatService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ChitchatService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetArticleComments(string ArticleID)
        {
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                List<DeffinityChatComment_SelectResult> GetCommentData = objDeffinityChitChat.DeffinityChatComment_Select(ArticleID).ToList();
                var retcls = from h in GetCommentData
                             select new
                             {
                                 h.UserID,
                                 h.CommentID,
                                 h.UserName,
                                 h.ArticleID,
                                 h.CommentDesc,
                                 CommentDate = string.Format("{0:dd/MM/yyyy HH:mm:ss}", h.CommentDate),
                             };
                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object AddOrDeleteLike(string ArticleID)
        {
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                tblArticleLike ExistOeNot = objDeffinityChitChat.tblArticleLikes.Where(a => a.ArticleID == ArticleID && a.UserID == sessionKeys.UID).FirstOrDefault();
                if (ExistOeNot == null)
                {
                    tblArticleLike tblLike = new tblArticleLike();
                    tblLike.ArticleID = ArticleID;
                    tblLike.UserID = sessionKeys.UID;
                    tblLike.LikeDate = DateTime.Now;
                    objDeffinityChitChat.tblArticleLikes.InsertOnSubmit(tblLike);
                    objDeffinityChitChat.SubmitChanges();
                }
                else
                {
                    objDeffinityChitChat.tblArticleLikes.DeleteOnSubmit(ExistOeNot);
                    objDeffinityChitChat.SubmitChanges();
                }
                return jsonSerializer.Serialize("Like added successfully").ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object BindSessionValue()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                return jsonSerializer.Serialize(sessionKeys.UID.ToString()).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object DeleteArticleMethod(string ArticleID)
        {
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                tblDeffinityChat Tbl_chat = objDeffinityChitChat.tblDeffinityChats.Where(a => a.ArticleID == ArticleID).FirstOrDefault();
                objDeffinityChitChat.tblDeffinityChats.DeleteOnSubmit(Tbl_chat);
                objDeffinityChitChat.SubmitChanges();
                return jsonSerializer.Serialize("Deleted successfully").ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object AddPost(string PostDescription, string type, string projectRef)
        {
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                tblDeffinityChat Tbl_chat = new tblDeffinityChat();
                Tbl_chat.UserID = sessionKeys.UID;
                Guid g = Guid.NewGuid();
                Tbl_chat.ArticleID = g.ToString();
                Tbl_chat.ArticleDesc = PostDescription;
                Tbl_chat.ArticleDate = DateTime.Now;
                Tbl_chat.Type = "share";
                if (type == "customer")
                {
                    Tbl_chat.Module = type;
                    Tbl_chat.ModuleID = sessionKeys.PortfolioID;
                }
                else
                {
                    Tbl_chat.Module = type;
                    Tbl_chat.ModuleID = int.Parse(projectRef);
                }
                objDeffinityChitChat.tblDeffinityChats.InsertOnSubmit(Tbl_chat);
                objDeffinityChitChat.SubmitChanges();

                return jsonSerializer.Serialize(Tbl_chat.ArticleID).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object AddCommentsToAtricle(string ArticleID,string Description)
        {
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                tblDeffinityChatComment Tblcmt = new tblDeffinityChatComment();
                Tblcmt.ArticleID = ArticleID;
                Tblcmt.CommentDate = DateTime.Now;
                Tblcmt.CommentDesc = Description;
                Tblcmt.UserID = sessionKeys.UID;
                Guid g = Guid.NewGuid();
                Tblcmt.ArticleCommentID = g.ToString();
                objDeffinityChitChat.tblDeffinityChatComments.InsertOnSubmit(Tblcmt);
                objDeffinityChitChat.SubmitChanges();

                var C_list = objDeffinityChitChat.tblDeffinityChatComments.Where(a => a.ArticleID == ArticleID).ToList();

                var retcls = new
                {
                    CommentID = Tblcmt.CommentID,
                    UserName = sessionKeys.UName,
                    ArticleID = Tblcmt.ArticleID,
                    CommentDesc = Tblcmt.CommentDesc,
                    CommentDate = string.Format("{0:dd/MM/yyyy HH:mm:ss}", Tblcmt.CommentDate),
                    UserID = Tblcmt.UserID,
                    C_count = C_list.Count,
                    SessionUid=sessionKeys.UID
                };

                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ArticleTotalCommentsCount(string ArticleID)
        {
            int Count = 0;
            DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
            try
            {
                var CommentCount = objDeffinityChitChat.tblDeffinityChatComments.Where(a => a.ArticleID == ArticleID).ToList();
                if (CommentCount != null)
                {
                    Count = CommentCount.Count;
                }
                else
                {
                    Count = 0;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return Count;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object ArticleTotalLikes(string ArticleID)
        {
            int Count = 0;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
                var LikesCount = objDeffinityChitChat.tblArticleLikes.Where(a => a.ArticleID == ArticleID).ToList();
                if (LikesCount != null)
                {
                    Count = LikesCount.Count;
                    var retcls = new { cnt = LikesCount.Count.ToString() };
                    return jsonSerializer.Serialize(retcls).ToString();
                }
                else
                {
                    return jsonSerializer.Serialize("0").ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize("0").ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetNoOfLikesWithArticleId(string ArticleID)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
                var LikesCount = objDeffinityChitChat.tblArticleLikes.Where(a => a.ArticleID == ArticleID).ToList();
                var retcls = new
                {
                    cnt = LikesCount.Count,
                    Liketext = LikesCount.Where(a => a.ArticleID == ArticleID && a.UserID == sessionKeys.UID).Count()
                };
                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetNewPostedData(string ArticleID, string type, string projectRef)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
                List<DeffinityChat_SelectBYModuleIDResult> GetChatData = new List<DeffinityChat_SelectBYModuleIDResult>();
                if (type == "customer")
                {
                    GetChatData = objDeffinityChitChat.DeffinityChat_SelectBYModuleID(sessionKeys.PortfolioID, type).ToList();
                }
                else
                {
                    GetChatData = objDeffinityChitChat.DeffinityChat_SelectBYModuleID(int.Parse(projectRef), type).ToList();
                }


                List<tblArticleLike> ListOfLikes = objDeffinityChitChat.tblArticleLikes.ToList();
                List<tblDeffinityChatComment> listOfComments = objDeffinityChitChat.tblDeffinityChatComments.ToList();
                var retcls = from h in GetChatData
                             where h.ArticleID == ArticleID
                             orderby h.ID descending
                             select new
                             {
                                 ArticleDate = string.Format("{0:dd/MM/yyyy HH:mm:ss}", h.ArticleDate),
                                 h.ArticleDesc,
                                 h.ArticleID,
                                 h.Filedesc,
                                 h.Filename,
                                 h.FilePath,
                                 h.Filesize,
                                 h.FileType,
                                 h.ID,
                                 h.LinkName,
                                 h.LinkUrl,
                                 h.Mailsent,
                                 h.Module,
                                 h.ModuleID,
                                 h.Type,
                                 h.UserID,
                                 h.UserName,
                                 LikesCount = ListOfLikes.Where(a => a.ArticleID == h.ArticleID).Count(),
                                 CommentsCount = listOfComments.Where(b => b.ArticleID == h.ArticleID).Count(),
                                 LikeText = ListOfLikes.Where(a => a.ArticleID == h.ArticleID && a.UserID == sessionKeys.UID).Count(),
                                 SessionUid = sessionKeys.UID
                             };
                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object AddSessionValues(string Cid, string text)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                sessionKeys.PortfolioID = int.Parse(Cid);
                sessionKeys.PortfolioName = text;
                return jsonSerializer.Serialize("Session updated succussfully").ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object DeleteArticleCommentMethod(string CommentID)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
                tblDeffinityChatComment Tbl_cmt = objDeffinityChitChat.tblDeffinityChatComments.Where(a => a.CommentID ==int.Parse(CommentID)).FirstOrDefault();

                string ArticleId = Tbl_cmt.ArticleID;
               
                objDeffinityChitChat.tblDeffinityChatComments.DeleteOnSubmit(Tbl_cmt);
                objDeffinityChitChat.SubmitChanges();

                int C_Count = objDeffinityChitChat.tblDeffinityChatComments.Where(a => a.ArticleID == ArticleId).Count();

                var retcls = new
                {
                    C_Count,
                    ArticleId
                };

                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetTotalArticles(string type, string projectRef, string NOofRecord)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                DeffinityChatDataClassesDataContext objDeffinityChitChat = new DeffinityChatDataClassesDataContext();
                List<DeffinityChat_SelectBYModuleIDResult> GetChatData = new List<DeffinityChat_SelectBYModuleIDResult>();

                if (type == "customer")
                {
                    GetChatData = objDeffinityChitChat.DeffinityChat_SelectBYModuleID(sessionKeys.PortfolioID, type).ToList();
                }
                else
                {
                    GetChatData = objDeffinityChitChat.DeffinityChat_SelectBYModuleID(int.Parse(projectRef), type).ToList();
                }

                List<tblArticleLike> ListOfLikes = objDeffinityChitChat.tblArticleLikes.ToList();
                List<tblDeffinityChatComment> listOfComments = objDeffinityChitChat.tblDeffinityChatComments.ToList();

                var retcls = from h in GetChatData
                             orderby h.ID descending
                             select new
                             {
                                 ArticleDate = string.Format("{0:dd/MM/yyyy HH:mm:ss}", h.ArticleDate),
                                 h.ArticleDesc,
                                 h.ArticleID,
                                 h.Filedesc,
                                 h.Filename,
                                 h.FilePath,
                                 h.Filesize,
                                 h.FileType,
                                 h.ID,
                                 h.LinkName,
                                 h.LinkUrl,
                                 h.Mailsent,
                                 h.Module,
                                 h.ModuleID,
                                 h.Type,
                                 h.UserID,
                                 h.UserName,
                                 LikesCount = ListOfLikes.Where(a => a.ArticleID == h.ArticleID).Count(),
                                 CommentsCount = listOfComments.Where(b => b.ArticleID == h.ArticleID).Count(),
                                 LikeText = ListOfLikes.Where(a => a.ArticleID == h.ArticleID && a.UserID == sessionKeys.UID).Count(),
                                 SessionUid = sessionKeys.UID
                             };
                if (int.Parse(NOofRecord) == 10)
                {
                    retcls = retcls.Take(10);
                }
                return jsonSerializer.Serialize(retcls).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
    }
}
