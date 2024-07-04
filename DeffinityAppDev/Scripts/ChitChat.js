$(document).ready(function () {
    $('#ddlCustomer').change(function () {
        debugger;
        var Cid = $('#ddlCustomer').val();
        var text = $("#ddlCustomer option:selected").text();
        $('#DivChitchat').html("");
        CustomerChange(Cid, text);
        ChitChatBind(10);
    });

    //alert("First time in Script");
    //  debugger;

    $('#ddlPortfolio').change(function () {
        var Cid = $('#ddlPortfolio').val();
        var text = $("#ddlPortfolio option:selected").text();
        $('#DivChitchat').html("");
        //alert(Cid + "," + text);
        CustomerChange(Cid, text);
        ChitChatBind(10);
    });


    ChitChatBind(10);
    var strbuild1 = "<div class='profile-post-form' style='border:0px;margin-bottom:0px;padding:0px;'><textarea id='TxtPost' class='form-control input-unstyled input-lg autogrow' placeholder='Whats on your mind?'></textarea>";
    strbuild1 = strbuild1 + "<br/><br/>";
    strbuild1 = strbuild1 + "<a onclick='AddPost(this)' class='btn btn-single btn-xs btn-success post-story-button'>Post</a></div>";
    $('#DivChitchatPost').append(strbuild1);
});


function CustomerChange(Cid, text) {
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/AddSessionValues",
        type: "POST",
        data: "{'Cid': '" + Cid + "','text':'" + text + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            var obj = jQuery.parseJSON(data.d);
        },
        error: function (msg) {
            var e = msg;
        }
    });
}
function BindSessionValue() {
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/BindSessionValue",
        type: "POST",
        data: "{}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            debugger;
            var obj = jQuery.parseJSON(data.d);
            $("#hdnSession").val(obj);
        },
        error: function (msg) {
            debugger;
            var e = msg;
        }
    });
}
function AddPost(e) {
    var projectRef = GetParameterValues('callid');
    var type = "";
    if (projectRef == undefined) {
        type = "customer";
    }
    else {
        type = "project";
    }
    var PostDescription = $("#TxtPost").val();
    if (PostDescription != '') {
        $.ajax({
            url: "../Projects/webservices/ChitchatService.asmx/AddPost",
            type: "POST",
            data: "{'PostDescription': '" + PostDescription + "','type':'" + type + "','projectRef':'" + projectRef + "'}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                debugger;
                $("#TxtPost").val("");
                var obj = jQuery.parseJSON(data.d);
                GetOneArticleHtml(obj, "New Article");
            },
            error: function (msg) {
                debugger;
                var e = msg;
            }
        });
    }
    else {
        alert("Please enter post description");
    }
}
function GetOneArticleHtml(ArticleID, Type) {

    var projectRef = GetParameterValues('callid');
    var type = "";
    if (projectRef == undefined) {
        type = "customer";
    }
    else {
        type = "project";
    }

    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/GetNewPostedData",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "','type':'" + type + "','projectRef':'" + projectRef + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                strbuild = '';
                debugger;
                strbuild = MainArticleHtml(obj[0].ID, obj[0].UserName, obj[0].ArticleID, obj[0].ArticleDesc, obj[0].ArticleDate, obj[0].UserID, obj[0].LikesCount, obj[0].CommentsCount, obj[0].LikeText, obj[0].SessionUid);
                if (Type == 'New Article') {
                    $('#DivChitchat').prepend(strbuild);
                }
                else if (Type == 'LikeOrComment') {
                    var L = $("#likeCount_" + ArticleID) + 1;
                    $("#likeCount_" + ArticleID).append(L);


                   // $("#DivArticle" + ArticleID).append(strbuild);
                }
            }
        },
        error: function (msg) {
            var e = msg;
            return '0';
        }
    });
}

function MainArticleHtml(ID, UserName, ArticleID, ArticleDesc, ArticleDate, UserID, LikesCount, CommentsCount, LikeText, SessionUid) {

    var strbuild = "<article class='timeline-story' id='Article" + ArticleID + "'><div id='DivArticle" + ArticleID + "'><i class='fa-paper-plane-empty block-icon'></i>";
    strbuild = strbuild + "<header><a class='user-img'><img src='../Admin/ImageHandler.ashx?type=user&id=" + UserID + "' alt='user-img' class='img-responsive img-circle' /></a>";
    strbuild = strbuild + "<div class='user-details'><a href=''>" + UserName + "</a> posted an update <time>" + ArticleDate + "</time></div></header>";
    strbuild = strbuild + "<div class='story-content'><p>" + ArticleDesc + "</p>";

    var text = "Like";
    if (LikeText != 0) {
        text = "UnLike";
    }
    strbuild = strbuild + "<div class=story-options-links' class='pnl251' id=" + ArticleID + "'>"
    strbuild = strbuild + "<a style='cursor:pointer;' onclick='AddOrDeleteLike(this)' id='like_" + ArticleID + "'><i class='linecons-heart'></i>" + text + "<span id='likeCount_" + ArticleID + "'>(" + LikesCount + ")</span></a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp";
    strbuild = strbuild + "<i class='linecons-comment'></i>Comments<span id='Comment_" + ArticleID + "'>(" + CommentsCount + ")</span> &nbsp&nbsp&nbsp&nbsp";
    if (UserID == SessionUid) {
        strbuild = strbuild + "<a style='cursor:pointer;' class='fa fa-trash' title='Delete' onclick='DeleteArticle(this)' id='Delete_" + ArticleID + "'></a>";
    }
    strbuild = strbuild + "</div>";
    //Story Comments  <a href=''  id='cmts_" + ArticleID + "'></a>
    GetArticleComments(ArticleID, SessionUid);
    strbuild = strbuild + "<div id='DivComments" + ArticleID + "'></div>";

    strbuild = strbuild + "<div class='story-comment-form'><div class='profile-post-form' style='border:0px;'><textarea id='txt_" + ArticleID + "' class='form-control input-unstyled autogrow' placeholder='Reply...'></textarea><br/><a onclick='AddComments(this)' class='btn btn-single btn-xs btn-success post-story-button' id='Btns_" + ArticleID + "'>Send</a></div></div> ";
    strbuild = strbuild + "</div>";
    strbuild = strbuild + "</article></div>";
    return strbuild;
}

function AddComments(e) {
    var ArticleID = $(e).get(0).id;
    ArticleID = ArticleID.substring(5, ArticleID.length);
    var Description = $("#txt_" + ArticleID).val();
    AddCommentsAjaxMethod(ArticleID, Description);
   //  $("#DivArticle" + ArticleID).html("");
   // GetOneArticleHtml(ArticleID, "LikeOrComment");
    return false;
}
function AddCommentsAjaxMethod(ArticleID, Description) {
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/AddCommentsToAtricle",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "','Description':'" + Description + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                debugger;
                var S = ArticleCommentsHtml(obj.CommentID, obj.UserName, obj.ArticleID, obj.CommentDesc, obj.CommentDate, obj.UserID, obj.SessionUid);
                $("#DivComments" + ArticleID).append(S);
                $("#txt_" + ArticleID).val("");
                $("#Comment_" + ArticleID).html("");
                $("#Comment_" + ArticleID).append("(" + obj.C_count + ")");
            }
        },  
        error: function (msg) {
            var e = msg;
        }
    });
}
function AddOrDeleteLike(e) {
    var ArticleID = $(e).get(0).id;
    ArticleID = ArticleID.substring(5, ArticleID.length);
    AddOrDeleteLikeAjaxMethod(ArticleID);
    return false;
}
function DeleteArticle(e) {
    var ArticleID = $(e).get(0).id;
    ArticleID = ArticleID.substring(7, ArticleID.length);
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/DeleteArticleMethod",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $("#DivArticle" + ArticleID).html("");
            $("#DivArticle" + ArticleID).hide();
            $("#Article" + ArticleID).hide();
            var obj = jQuery.parseJSON(data.d);
        },
        error: function (msg) {
            debugger;
            var e = msg;
        }
    });
}
function DeleteArticleComment(e) {
    var CommentID = $(e).get(0).id;
    CommentID = CommentID.substring(7, CommentID.length);
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/DeleteArticleCommentMethod",
        type: "POST",
        data: "{'CommentID': '" + CommentID + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $("#ul" + CommentID).html("");
            $("#ul" + CommentID).hide();
            var obj = jQuery.parseJSON(data.d);
            debugger;
            var ArticleID = obj.ArticleId;

            $("#Comment_" + ArticleID).html("");
            $("#Comment_" + ArticleID).append("(" + obj.C_Count + ")");
        },
        error: function (msg) {
            debugger;
            var e = msg;
        }
    });
}
function AddOrDeleteLikeAjaxMethod(ArticleID) {
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/AddOrDeleteLike",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
           // $("#DivArticle" + ArticleID).html("");
            //   GetOneArticleHtml(ArticleID, "LikeOrComment");
            GetNoOfLikesWithArticleId(ArticleID);
        },
        error: function (msg) {
            debugger;
            var e = msg;
        }
    });
}
function GetNoOfLikesWithArticleId(ArticleID) {
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/GetNoOfLikesWithArticleId",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != [])
            {
                var obj = jQuery.parseJSON(data.d);
                debugger;
                $("#like_" + ArticleID).html("");

                var S = "<i class='linecons-heart'></i>";
                if (obj.Liketext == 0) {
                    S = S + "Like";
                }
                else {
                    S = S + "UnLike";
                }
                S = S + "<span id='likeCount_" + ArticleID + "'>(" + obj.cnt + ")</span>";
                $("#like_" + ArticleID).append(S);
            }
        },
        error: function (msg) {
            var e = msg;
        }
    });
}
function ArticleCommentsHtml(CommentID, UserName, ArticleID, CommentDesc, CommentDate, UserID, SessionUid)
{
    var strbuild = "<ul class='list-unstyled story-comments' id='ul" + CommentID + "' ><li><div class='story-comment'>";
    strbuild = strbuild + "<a class='comment-user-img'><img src='../Admin/ImageHandler.ashx?type=user&id=" + UserID + "' alt='user-img' class='img-circle img-responsive' /></a>";
    strbuild = strbuild + "<div class='story-comment-content'><a class='story-comment-user-name'>" + UserName + "<time>" + CommentDate + "</time>";

    if (UserID == SessionUid)
    {
        strbuild = strbuild + "&nbsp&nbsp&nbsp&nbsp<b style='cursor:pointer;' href='#' class='fa fa-trash' title='Delete' onclick='DeleteArticleComment(this)' id='Delete_" + CommentID + "'></b>";
    }
    strbuild = strbuild + "</a><p>" + CommentDesc + "</p></div>";

    strbuild = strbuild + "</div></li></ul>";
    return strbuild;
}
function GetArticleComments(ArticleID, SessionUid) {
    var strbuild = '';
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/GetArticleComments",
        type: "POST",
        data: "{'ArticleID': '" + ArticleID + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            debugger;
            if (data.d != "[]") {
                debugger;
                var obj = jQuery.parseJSON(data.d);
                strbuild = '';
                for (var p in obj) {
                    strbuild = strbuild + ArticleCommentsHtml(obj[p].CommentID, obj[p].UserName, obj[p].ArticleID, obj[p].CommentDesc, obj[p].CommentDate, obj[p].UserID, SessionUid);
                }
                $('#DivComments' + ArticleID).append(strbuild);
            }
        },
        error: function (msg) {
            var e = msg;
            return '';
        }
    });
}
function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}
function ChitChatBind(NOofRecord) {
 
    $('#DivChitchat').html("");
    var projectRef = GetParameterValues('callid');
    var type = "";
    if (projectRef == undefined) {
        type = "customer";
    }
    else {
        type = "project";
    }
    var strbuild = "";
    var LoadingDivHtml = "<div id='DivTotalRecords' class='timeline-story'><div style='text-align:center;'><b style='cursor:pointer;' onclick='ChitChatAllArticlesBind()'>Load more data</b></div></div>";
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/GetTotalArticles",
        type: "POST",
        data: "{'type': '" + type + "','projectRef':'" + projectRef + "','NOofRecord':'" + NOofRecord + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                debugger;
                if (NOofRecord == 10) {
                    for (var p in obj) {
                        strbuild = '';
                        strbuild = MainArticleHtml(obj[p].ID, obj[p].UserName, obj[p].ArticleID, obj[p].ArticleDesc, obj[p].ArticleDate, obj[p].UserID, obj[p].LikesCount, obj[p].CommentsCount, obj[p].LikeText, obj[p].SessionUid);
                        $('#DivChitchat').append(strbuild);
                    }
                    $('#DivChitchat').append(LoadingDivHtml);
                    if (obj.length < 10) {
                        $("#DivTotalRecords").hide();
                    }
                }
                else {
                    for (var p in obj) {
                        strbuild = '';
                        strbuild = MainArticleHtml(obj[p].ID, obj[p].UserName, obj[p].ArticleID, obj[p].ArticleDesc, obj[p].ArticleDate, obj[p].UserID, obj[p].LikesCount, obj[p].CommentsCount, obj[p].LikeText, obj[p].SessionUid);
                        $('#DivChitchat').append(strbuild);
                    }
                    $("#DivTotalRecords").hide();
                }
            }
        },
        error: function (msg) {
            var e = msg;
        }
    });
}
function ChitChatAllArticlesBind() {
    ChitChatBind(0);
}
function ChitChatAllArticlesBind1() {
    var projectRef = GetParameterValues('callid');
    var type = "";
    if (projectRef == undefined) {
        type = "customer";
    }
    else {
        type = "project";
    }
    var strbuild = "";
    $("#DivTotalRecords").hide();
    $.ajax({
        url: "../Projects/webservices/ChitchatService.asmx/GetTotalArticles",
        type: "POST",
        data: "{'type': '" + type + "','projectRef':'" + projectRef + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);

                if (obj.length > 10) {
                    for (var p1 = 11; p1 <= obj.length; p1++) {
                        strbuild = '';
                        strbuild = MainArticleHtml(obj[p1].ID, obj[p1].UserName, obj[p1].ArticleID, obj[p1].ArticleDesc, obj[p1].ArticleDate, obj[p1].UserID, obj[p1].LikesCount, obj[p1].CommentsCount, obj[p1].LikeText, obj[p1].SessionUid);
                        $('#DivChitchat').append(strbuild);
                    }
                }
            }
        },
        error: function (msg) {
            var e = msg;
        }
    });
}