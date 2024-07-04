<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ChitChatCtrlHome" Codebehind="ChitChatCtrlHome.ascx.cs" %>

<script type="text/javascript">


    function DateTimeJs() {

        // This array holds the "friendly" day names
        var day_names = new Array(7)
        day_names[0] = "Sunday"
        day_names[1] = "Monday"
        day_names[2] = "Tuesday"
        day_names[3] = "Wednesday"
        day_names[4] = "Thursday"
        day_names[5] = "Friday"
        day_names[6] = "Saturday"

        // This array holds the "friendly" month names
        var month_names = new Array(12)
        month_names[0] = "January"
        month_names[1] = "February"
        month_names[2] = "March"
        month_names[3] = "April"
        month_names[4] = "May"
        month_names[5] = "June"
        month_names[6] = "July"
        month_names[7] = "August"
        month_names[8] = "September"
        month_names[9] = "October"
        month_names[10] = "November"
        month_names[11] = "December"

        // Get the current date
        date_now = new Date()

        // Figure out the friendly day name
        day_value = date_now.getDay()
        date_text = day_names[day_value]

        // Figure out the friendly month name
        month_value = date_now.getMonth()
        date_text += " " + month_names[month_value]

        // Add the day of the month
        date_text += " " + date_now.getDate()

        // Add the year
        date_text += ", " + date_now.getFullYear()

        // Get the minutes in the hour
        minute_value = date_now.getMinutes()
        if (minute_value < 10) {
            minute_value = "0" + minute_value
        }

        // Get the hour value and use it to customize the greeting
        hour_value = date_now.getHours()
        if (hour_value == 0) {
            greeting = "Good morning, "
            time_text = " at " + (hour_value + 12) + ":" + minute_value + " AM"
        }
        else if (hour_value < 12) {
            greeting = "Good morning!"
            time_text = " at " + hour_value + ":" + minute_value + " AM"
        }
        else if (hour_value == 12) {
            greeting = "Good afternoon!"
            time_text = " at " + hour_value + ":" + minute_value + " PM"
        }
        else if (hour_value < 17) {
            greeting = "Good afternoon!"
            time_text = " at " + (hour_value - 12) + ":" + minute_value + " PM"
        }
        else {
            greeting = "Good evening!"
            time_text = " at " + (hour_value - 12) + ":" + minute_value + " PM"
        }

        var todayTime = 'Today' + time_text;

        return todayTime + " .";
    }

    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    function guid() {
        return (S4() + S4() + "_" + S4() + "_" + S4() + "_" + S4() + "_" + S4() + S4() + S4());
    }

    //Add and remove Share File*********************************************************************
    function AddChild_To_divMainListFile(TopicID) {

        
        
        var ni = document.getElementById('<%=divMainCOM.ClientID%>');
        var newdiv = document.createElement('div');
        
        var hrfIDValue = 'divMainCOM' + TopicID;
        var divIdName = 'divMainChildCOM' + TopicID;  // Adding Child Divs to Mainlist
        var LikeIDValue = 'divLikeCOM' + TopicID;
        var UnLikeIDValue = 'divUnLikeCOM' + TopicID;
        var DelIDValue = 'divDelCOM' + TopicID;
        var DateIDValue = 'divDateCOM' + TopicID;
        newdiv.setAttribute('id', divIdName);
        newdiv.className = 'coment_blk';

        var lblMembersCOMID = 'lblMembersCOM' + TopicID;
        var strHiddenCount = '<input type=\'hidden\' id=\'' + lblMembersCOMID + '\'+  value=\'0\'  />';


        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>'); 
        var strUserName = HiddenUserNametxt.value;

        var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>'); 
        var strProfileImg = HiddenUserPhotoFile.value;

        var HiddenProfileLink = document.getElementById('<%=HiddenProfile.ClientID%>'); 
        var strProfileLink = HiddenProfileLink.value;
        

        var divLikethis = 'divLikethisCOM' + TopicID;  // Adding Likethis Divs to Mainlist       

        var strLikeLineUrl = 'default.aspx';
        var strLikeLineImg = 'media/NoteBook.JPG';
        var strDownloadImg = 'media/ico_download.png';
        var strSize = '(2 KB)';
        
//        var UserName = '<a href=\'' + strProfileLink + '\'>' + strUserName + '</a>';
//        var ProfileImage = '<a href=\'' + strProfileLink + '\'><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

        var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';


        var CommentsLine = '<div id=\'' + hrfIDValue + '\' class=\'chat_links\' ><a  title=\'Leave a comment\'   href=\'javascript:DisplayCommentBox(' + divIdName + ',' + hrfIDValue + ') \'   >Comment</a>  |</div>';  //'<a href=\'default.aspx\'>Comments </a>';
        var LikeLine = '<div id=\'' + LikeIDValue + '\' class=\'chat_links\' ><a href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',' + '1) \' >Like</a> |</div>';
        var UnLikeLine = '<div id=\'' + UnLikeIDValue + '\' style=\'display: none;\' class=\'chat_links\' ><a href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',' + '0) \' >UnLike </a> |</div>';        
        var DeleteLine = '<div id=\'' + DelIDValue + '\' class=\'chat_links\' ><a href=\'javascript:removedivMainChild(' + divIdName + ') \'   >Delete</a></div>';
        var PostedText = ' posted a file.';
        
        var UploadedFile = document.getElementById('txtfileUpload').value;
        var PostedLinkText = document.getElementById('txtfileDesc').value;
        
        
        var PostedLink = '<a href=\'' + UploadedFile + '\'>' + PostedLinkText + '</a>';
        var PostedFileName = '<a href=\'' + UploadedFile + '\'>' + document.getElementById('txtfileName').value + '</a>';
        var FileDownloadLink = '<a href=\'' + UploadedFile + '\'><img   class=\'link_icon\'  src=\'' + strDownloadImg + '\'/>Download txt</a>' + strSize;
        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><div class=\'clr\'></div>';

        var LikeThisLine = '<div class=\'clr\'></div><div id=\'' + divLikethis + '\' class=\'likeunlike\'  style=\'display: none;\'  ><b>&nbsp;</b>Like this article</div>';

        newdiv.innerHTML = '<p>' + ProfileImage + UserName + PostedText + '<br/><br/>' + PostedFileName + FileDownloadLink + document.getElementById('txtfileDesc').value + '</p>' + todaydate + CommentsLine + LikeLine + UnLikeLine + DeleteLine + LikeThisLine + '<div class=\'share_div\'></div>' + strHiddenCount; ;

        ni.insertBefore(newdiv, document.getElementById('<%=divMainCOM.ClientID%>').firstChild);
        
        

        document.getElementById('txtfileUpload').value = '';
        document.getElementById('txtfileDesc').value = '';
        document.getElementById('txtfileName').value = '';

        AttachFile();
        
        
        
//        var varTextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
//        varTextBox1.value = '';
//        varTextBox1.value = ni.innerHTML;

    }
    //**********************************************************************************************
    //Add and remove Share text ********************************************************************
    function AddChild_To_divMainListText(TopicID) {
        
        var guidVal = guid();

        var ni = document.getElementById('<%=divMainCOM.ClientID%>');
        
        var newdiv = document.createElement('div');
       
        var hrfIDValue = 'divMainCOM' + TopicID;
        var LikeIDValue = 'divLikeCOM' + TopicID;
        var UnLikeIDValue = 'divUnLikeCOM' + TopicID;
        var DelIDValue = 'divDelCOM' + TopicID;
        var DateIDValue = 'divDateCOM' + TopicID;

        var lblMembersCOMID = 'lblMembersCOM' + TopicID;

        var strHiddenCount = '<input type=\'hidden\' id=\'' + lblMembersCOMID + '\'+  value=\'0\'  />';



        var divLikethis = 'divLikethisCOM' + TopicID;  // Adding Likethis Divs to Mainlist

        var divIdName = 'divMainChildCOM' + TopicID;  // Adding Child Divs to Mainlist        

        var divCommentID = 'divMainChildCOM' + TopicID;
        newdiv.setAttribute('id', divIdName);
        newdiv.className = 'coment_blk';

        
        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>');
        var strUserName = HiddenUserNametxt.value;
        
        
        
        var HiddenProfileLink =  document.getElementById('<%=HiddenProfile.ClientID%>');
        var strProfileLink = HiddenProfileLink.value;

        var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>'); 
        var strProfileImg = HiddenUserPhotoFile.value;
        var strLikeLineUrl = 'default.aspx';


        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'        
        
        var UserName = '<a href=\'' + strProfileLink + '\'' +  '   onclick=' + strPop +'\'/>' + strUserName + '</a>';        
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';
        
        var txtName = document.getElementById('<%=txtTextShare.ClientID%>');
        
        //Check this later need to make sure div guid works'
        var CommentsLine = '<div id=\'' + hrfIDValue + '\' class=\'chat_links\' > <a  title=\'Leave a comment\'  href=\'javascript:DisplayCommentBox(' + divIdName + ',' + hrfIDValue + ') \' >Comment</a> |</div>';  //'<a href=\'default.aspx\'>Comments </a>';
        var LikeLine = '<div id=\'' + LikeIDValue + '\' class=\'chat_links\' ><a  title=\'Like this\' href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',1) \' >Like </a> |</div>';
        var UnLikeLine = '<div id=\'' + UnLikeIDValue + '\' style=\'display: none;\'   title=\'UnLike this\' class=\'chat_links\' ><a href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',0) \' >UnLike </a> |</div>';

        var DeleteLine = '<div id=\'' + DelIDValue + '\' class=\'chat_links\' ><a href=\'javascript:removedivMainChild(' + divIdName + ') \'   >Delete</a></div>';
        var LikeThisLine = '<div class=\'clr\'></div><div id=\'' + divLikethis + '\' class=\'likeunlike\'  style=\'display: none;\'  ><b>&nbsp;</b>Like this article</div>';
        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><div class=\'clr\'></div>';

        newdiv.innerHTML = ProfileImage + UserName + '<p>' + txtName.value + '</p>' + todaydate + CommentsLine + LikeLine + UnLikeLine + DeleteLine + LikeThisLine + '<div class=\'share_div\'></div>' + strHiddenCount;        
        ni.insertBefore(newdiv, document.getElementById('<%=divMainCOM.ClientID%>').firstChild);        
        

        txtName.value = '';
        
//        var varTextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
//        varTextBox1.value = '';
//        varTextBox1.value = ni.innerHTML;

        
        
    }
    function AddChild_To_divMainListLink(TopicID) {

        
        var ni = document.getElementById('<%=divMainCOM.ClientID%>');
        var newdiv = document.createElement('div');
        var hrfIDValue = 'divMainCOM' + TopicID;
        var DateIDValue = 'divDateCOM' + TopicID;

        var lblMembersCOMID = 'lblMembersCOM' + TopicID;
        var strHiddenCount = '<input type=\'hidden\' id=\'' + lblMembersCOMID + '\'+  value=\'0\'  />';

        var divLikethis = 'divLikethisCOM' + TopicID;  // Adding Likethis Divs to Mainlist

        var divIdName = 'divMainChildCOM' + TopicID;  // Adding Child Divs to Mainlist
        newdiv.setAttribute('id', divIdName);
        newdiv.className = 'coment_blk';

        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>');

        var strUserName = HiddenUserNametxt.value;


        var HiddenProfileLink = document.getElementById('<%=HiddenProfile.ClientID%>');
        var strProfileLink = HiddenProfileLink.value;        

        var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>');
        var strProfileImg = HiddenUserPhotoFile.value;

        var strLikeLineImg = 'media/link.gif';




        var LikeIDValue = 'divLikeCOM' + TopicID;
        var UnLikeIDValue = 'divUnLikeCOM' + TopicID;
        var DelIDValue = 'divDelCOM' + TopicID;

        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

        var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        var CommentsLine = '<div id=\'' + hrfIDValue + '\' class=\'chat_links\' ><a  title=\'Leave a comment\'  href=\'javascript:DisplayCommentBox(' + divIdName + ',' + hrfIDValue + ') \'   >Comments</a>|</div>';
        var LikeLine = '<div id=\'' + LikeIDValue + '\' class=\'chat_links\' ><a href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',' + '1) \' >Like </a> |</div>';
        var UnLikeLine = '<div id=\'' + UnLikeIDValue + '\' style=\'display: none;\' class=\'chat_links\' ><a href=\'javascript:LikeOrUnlike(' + divIdName + ',' + divLikethis + ',' + LikeIDValue + ',' + UnLikeIDValue + ',' + '0) \' >UnLike </a> |</div>';
        var DeleteLine = '<div id=\'' + DelIDValue + '\' class=\'chat_links\' ><a href=\'javascript:removedivMainChild(' + divIdName + ') \'   >Delete</a></div>';
        var PostedText = ' posted a link.';
        var PostedLinkText = document.getElementById('txtLinkName').value;
        var strLikeLineUrl = document.getElementById('txtLinkURL').value; //Here passing the url name
        var PostedLinkimg = '<a href=\'' + strLikeLineUrl + '\'  target=\'_blank\' title=\'' + strLikeLineUrl + '\' ><img  class=\'link_icon\'   src=\'' + strLikeLineImg + '\'   title=\'' + strLikeLineUrl + '\' /> </a>';

        var LikeThisLine = '<div class=\'clr\'></div><div id=\'' + divLikethis + '\' class=\'likeunlike\'  style=\'display: none;\'  ><b>&nbsp;</b>Like this article</div>';
        var PostedLink = '<a   target=\'_blank\'  href=\'' + strLikeLineUrl + '\'>' + PostedLinkText + '</a>';

        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><div class=\'clr\'></div>';

        newdiv.innerHTML = '<p>' + ProfileImage + UserName + PostedText + '<br/><br/>' + PostedLinkimg + PostedLink + " . " + document.getElementById('txtLinkURL').value + '</p>' + todaydate + CommentsLine + LikeLine + UnLikeLine + DeleteLine + LikeThisLine + '<div class=\'share_div\'></div>' + strHiddenCount;
        ni.insertBefore(newdiv, document.getElementById('<%=divMainCOM.ClientID%>').firstChild);

        document.getElementById('txtLinkURL').value = 'http://';
        document.getElementById('txtLinkName').value = '';


        //        var varTextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
        //        varTextBox1.value = '';
        //        varTextBox1.value = ni.innerHTML;
        AttachLink();
    }
    
    //This function is tested and completed
    function removedivMainChild(divNum) {

        var answer = confirm("Are you sure you want to delete this post?")
        if (answer) {
            DeffinityChatService.MainShareText_Remove(divNum.id, MainShareText_Remove_Success);

        }
    }
    //This function is tested and completed
    function OnMainShareText_InsertClick() {
        var txtName = document.getElementById('<%=txtTextShare.ClientID%>');    
         
        if (txtName.value == '') {

            var lblName = document.getElementById('<%=lblTextShare.ClientID%>');
            lblName.style.visibility = 'visible';
            var txtName = document.getElementById('<%=txtTextShare.ClientID%>');
            return;
        }


        
        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var HiddenUserNameTxt = document.getElementById('<%=HiddenUserName.ClientID%>');
        var HiddenModuleTxt = document.getElementById('<%=HiddenModule.ClientID%>');
        var HiddenModuleIDTxt = document.getElementById('<%=HiddenModuleID.ClientID%>');
        
        
        var guidVal = guid();
        var todaydt=DateTimeJs();

        DeffinityChatService.MainShareText_Insert(HiddenUserNameTxt.value, HiddenUserIDTxt.value, guidVal, txtName.value, todaydt, HiddenModuleTxt.value, HiddenModuleIDTxt.value, MainShareText_Insert_Success);
    }


    //This function is tested and completed
    function MainShareText_Remove_Success(result) {
    
        var GetVal = result;
        var d = document.getElementById('<%=divMainCOM.ClientID%>');
        var olddiv = document.getElementById(GetVal);
        d.removeChild(olddiv);
    }
    //This function is tested and completed
    function MainShareText_Insert_Success(result) {
        var guidVal = result;
        AddChild_To_divMainListText(guidVal);

    }
    function LikeOrUnlike(LikeCommentID, ParentDiv, Like, UnLike, LikeValue) {



       // To get no of members like
        var lblMembersID = UnLike.id.replace('divUnLikeCOM', 'lblMembersCOM'); 
        var LikeCount = document.getElementById(lblMembersID);       
        
        // To get the
        var GetdivLikethisCOMID = UnLike.id.replace('divUnLikeCOM', 'divLikethisCOM');
        var divLikethisCOMID = document.getElementById(GetdivLikethisCOMID);


        var divLike = document.getElementById(Like.id);
        var divUnlike = document.getElementById(UnLike.id);
        var divParentDiv = document.getElementById(ParentDiv.id);



        if (LikeValue == '1') {

            divLike.style.display = "none"
            divUnlike.style.display = "block"
            divParentDiv.style.display = "block"

            if (LikeCount.value == 0) {
                divLikethisCOMID.innerHTML = '<b>&nbsp;</b>You like this.';
            }
            else if (LikeCount.value > 0) {          
            var strPop = '"window.open(' + '\'CustomersLikethis.aspx?id=' + UnLike.id +'\''+ ',' + '\'popup\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'
            

            var innerI = '<b>&nbsp;</b>You and ' + '<a href=' + 'CustomersLikethis.aspx?id=' + UnLike.id + ' onclick=' + strPop + '>' + LikeCount.value + '</a>' + ' others like this.';
            divLikethisCOMID.innerHTML = '<b>&nbsp;</b>You and ' + '<a href=' + 'CustomersLikethis.aspx?id=' + UnLike.id + ' onclick=' + strPop + '>' + LikeCount.value + '</a>' + ' others like this.';
            
            var kk = divLikethisCOMID.innerHTML;
            
            }

            divLikethisCOMID.style.display = 'block';
        }
        else {

            divLike.style.display = "block"
            divUnlike.style.display = "none"
            divParentDiv.style.display = "none"
            if (LikeCount.value > 0) {

                var strPop = '"window.open(' + '\'CustomersLikethis.aspx?id=' + UnLike.id + '\'' + ',' + '\'popup\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'
                divLikethisCOMID.innerHTML = '<b>&nbsp;</b><a href=' + 'CustomersLikethis.aspx?id=' + UnLike.id + '   onclick=' + strPop + '>' + LikeCount.value + '</a>' + ' people like this.';

                divLikethisCOMID.style.display = 'block';
            }
            else {
                divLikethisCOMID.style.display = 'none';
            }

        }
        LikeArticle_Insert_Delete(LikeCommentID.id, LikeValue);
    }
    function DisplayCommentBoxOpen(divNum, hrfIDValue) {
        
        var guidVal = guid();
        var hrfIDDIV = document.getElementById(hrfIDValue);
        hrfIDDIV.style.display = "none"; //Hidding Comment link
        var ni = document.getElementById(divNum);
        var newdivSecond = document.createElement('div');
        var divIdNameSecond = 'divCommentTextCOM' + guidVal;   // Adding Child Divs to Mainlist
        newdivSecond.setAttribute('id', divIdNameSecond);
        newdivSecond.className = 'user_cmt_blk';

        var HiddenUserNametxt =  document.getElementById('<%=HiddenUserName .ClientID%>');  
         var strUserName = HiddenUserNametxt.value;

         var HiddenProfileLink = document.getElementById('<%=HiddenProfile .ClientID%>');  
         var strProfileLink = HiddenProfileLink.value;

         var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto .ClientID%>');  
        var strProfileImg = HiddenUserPhotoFile.value;
        var DateIDValue = 'divDateCOM' + guidVal;


        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

        var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        
//        var UserName = '<a href=\'' + strProfileLink + '\'>' + strUserName + '</a>';
//        var ProfileImage = '<a href=\'' + strProfileLink + '\'><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><br/>';
        var txtCommentBoxID = 'txtComment' + guidVal;
        var PostedText = 'posted a file.';       
       
        var commentBox = ProfileImage + '<input id=\'' + txtCommentBoxID + '\'  /> <input id=\'btnComments\' title=\'Write a comment...\' value=\'Comment\' type=\'button\' onclick=\'OnComment_InsertClick(' + divNum + ',' + txtCommentBoxID + ',' + divNum + ',' + divIdNameSecond + ',' + hrfIDValue + ');\' />';
       
        newdivSecond.innerHTML = commentBox;
        ni.appendChild(newdivSecond);
        document.getElementById(txtCommentBoxID).focus();
        
    }
    //This function is to display comment textbox and comment button, does not maintain any guid to store in db
    function DisplayCommentBox(divNum, hrfIDValue) {
        
        var guidVal = guid();
        var hrfIDDIV = document.getElementById(hrfIDValue.id);
        hrfIDDIV.style.display = "none"; //Hidding Comment link
        var ni = document.getElementById(divNum.id);
        var newdivSecond = document.createElement('div');
        var divIdNameSecond = 'divCommentTextCOM' + guidVal;   // Adding Child Divs to Mainlist
        newdivSecond.setAttribute('id', divIdNameSecond);
        newdivSecond.className = 'user_cmt_blk';

        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>');  
         var strUserName = HiddenUserNametxt.value;

         var HiddenProfileLink = document.getElementById('<%=HiddenProfile.ClientID%>');  
         var strProfileLink = HiddenProfileLink.value;

         var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>');
         var strProfileImg = HiddenUserPhotoFile.value;

         var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
         var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

         var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
         var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';


        
//        var UserName = '<a href=\'' + strProfileLink + '\'>' + strUserName + '</a>';
//        var ProfileImage = '<a href=\'' + strProfileLink + '\'><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';
        var todaydate = DateTimeJs();
        var txtCommentBoxID = 'txtComment' + guidVal;        
        var PostedText = 'posted a file.';
        var commentBox = ProfileImage + '<input id=\'' + txtCommentBoxID + '\'   /> <input id=\'btnComments\' title=\'Comment\' value=\'Comment\' type=\'button\' onclick=\'OnComment_InsertClick(' + divNum.id + ',' + txtCommentBoxID + ',' + divNum.id + ',' + divIdNameSecond + ',' + hrfIDValue.id + ');\' />';


        newdivSecond.innerHTML = commentBox;
        ni.appendChild(newdivSecond);       
        document.getElementById(txtCommentBoxID).focus();

    }

    //Add Comment including parent id
    function OnComment_InsertClick(ParentDIV, txtCommentBoxID, divParent, divNum, DivCommentLink) {
        if (txtCommentBoxID.value == '') {
            return;
         }

        var CommentID = guid();
        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        DeffinityChatService.Comment_Insert(HiddenUserIDTxt.value, txtCommentBoxID.value, CommentID, ParentDIV.id, Comment_Insert_Success);        
        AddComment(ParentDIV, CommentID, txtCommentBoxID);
        txtCommentBoxID.value = '';

        CloseComment(divParent.id, divNum.id, DivCommentLink.id);        
        DisplayCommentBoxOpen(ParentDIV.id, DivCommentLink.id);

    }

    function LikeArticle_Insert_Delete(DivTopicID, LikeValue) {




        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>'); 
        if (LikeValue == '1') {
            DeffinityChatService.LikeArticle_Insert(HiddenUserIDTxt.value, DivTopicID, LikeArticle_Insert_Success);
        }
        else {
            DeffinityChatService.LikeArticle_Delete(HiddenUserIDTxt.value, DivTopicID, LikeArticle_Insert_Success);
        }
        
        
        
    }
    
    //Remove the comment based on the commentid and ParentID
    function removeCommentOfArticle(divParent, divNum) {

        var answer = confirm("Are you sure you want to delete this post?")
        if (answer) {


            DeffinityChatService.Comment_Remove(divNum.id, divParent.id, Comment_Remove_Success);

            var d = document.getElementById(divParent.id);
            var olddiv = document.getElementById(divNum.id);
            d.removeChild(olddiv);

        }


    }
    //Close the comment text box and display the "Comment" link
    function CloseComment(divParent, divNum, DivCommentLink) {

        var d = document.getElementById(divParent);
        var olddiv = document.getElementById(divNum);
        d.removeChild(olddiv);



    }
    //Calling this function in OnComment_InsertClick
    function AddLikeDIV(divParent ,CommentText,CommentID) {

        
        var ni = document.getElementById(divParent.id);

        var newdiv = document.createElement('div');

        var divIdNameFirst = CommentID;   // Adding Child Divs to Mainlist
        newdiv.setAttribute('id', divIdNameFirst);
        var DateIDValue = 'divDateCOM' + CommentID;

        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>'); 
         var strUserName = HiddenUserNametxt.value;

         var HiddenProfileLink = document.getElementById('<%=HiddenProfile.ClientID%>'); 
         var strProfileLink = HiddenProfileLink.value;

         var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>'); 
        var strProfileImg = HiddenUserPhotoFile.value;
        
        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

        var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

       
//        var UserName = '<a href=\'' + strProfileLink + '\'>' + strUserName + '</a>';
//        var ProfileImage = '<a href=\'' + strProfileLink + '\'><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><br/>';
        var commentBoxDisplay = ProfileImage + UserName + CommentText + '</br>' + todaydate ;
        newdiv.innerHTML = commentBoxDisplay;
        
        
        ni.appendChild(newdiv);
        

    }
    //Calling this function in OnComment_InsertClick
    function AddComment(divParent, CommentID, CommentText) {


        var ni = document.getElementById(divParent.id);

        var newdiv = document.createElement('div');
        var divIdNameFirst = 'divCommentTextCOM' + CommentID;  // Adding Child Divs to Mainlist
        newdiv.setAttribute('id', divIdNameFirst);
        newdiv.className = 'user_cmt_blk';

        var HiddenUserNametxt = document.getElementById('<%=HiddenUserName.ClientID%>');  
        var strUserName = HiddenUserNametxt.value;


        var HiddenProfileLink = document.getElementById('<%=HiddenProfile.ClientID%>');  
        var strProfileLink = HiddenProfileLink.value;

        var HiddenUserPhotoFile = document.getElementById('<%=HiddenUserPhoto.ClientID%>'); 
        var strProfileImg = HiddenUserPhotoFile.value;
        var DateIDValue = 'divDateCOM' + CommentID;
        var divDelComt = 'divDelComtCOM' + CommentID;

        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var strPop = '"window.open(' + '\'Userprofile.aspx?id=' + HiddenUserIDTxt.value + '\'' + ',' + '\'Profile\'' + ',' + '\'width=370,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0\'' + ');' + 'return false;"' + ';'

        var UserName = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/>' + strUserName + '</a>';
        var ProfileImage = '<a href=\'' + strProfileLink + '\'' + '   onclick=' + strPop + '\'/><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';


//        var UserName = '<a href=\'' + strProfileLink + '\'>' + strUserName +'</a>';        
//        var ProfileImage = '<a href=\'' + strProfileLink + '\'><img src=\'' + strProfileImg + '\' alt=\'' + strUserName + '\' /></a>';

        var todaydate = '<div id=\'' + DateIDValue + '\' class=\'chat_timestamp\' >' + DateTimeJs() + '</div><br/>';
        var DeleteLine = '<div  id=\'' + divDelComt + '\'  class="chat_links"  ><a href=\'javascript:removeCommentOfArticle(' + divParent.id + ',' + divIdNameFirst + ') \'   >Delete</a></div>';

        var commentBoxDisplay = ProfileImage + UserName + ' ' + CommentText.value + '<br/><br/>' + todaydate  +  DeleteLine;
        newdiv.innerHTML = commentBoxDisplay;
        
        ni.appendChild(newdiv);
        
//        var nic = document.getElementById('ChitChatCtrl1_divMainCOM');
//        var varTextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
//        varTextBox1.value = '';
//        varTextBox1.value = nic.innerHTML;
        
        

    }
    //*******************************************************************************************
    

    //This function for Attach file div 
    function AttachFile() {

        var divAttach = document.getElementById('<%=divAttach.ClientID%>');
        var divAttachfileAndLink = document.getElementById('<%=divAttachfileAndLink.ClientID%>');
        var btnShare = document.getElementById('<%=btnMainShare.ClientID%>');

        if (divAttach.style.display == "block") {
            divAttach.style.display = "none"; //Hide Attach div
            divAttachfileAndLink.style.display = "block"; //Show File and Link div
            btnShare.style.display = "block"; //Show main Share Button 
        }
        else {
            divAttach.style.display = "block"; //Show Attach div            
            divAttachfileAndLink.style.display = "none"; //Hide File and Link div
            btnShare.style.display = "none"; //Hide main Share Button
        }


        
        
    }
    //This function for Attach file div and hides the link div also
    function AttachLink() {

        var divAttach = document.getElementById('<%=divLink.ClientID%>');

        var divAttachfileAndLink = document.getElementById('<%=divAttachfileAndLink.ClientID%>');
        var btnShare = document.getElementById('<%=btnMainShare.ClientID%>');

        if (divAttach.style.display == "block") {
            divAttach.style.display = "none"; //Hide Link div
            divAttachfileAndLink.style.display = "block"; //Show File and Link div
            btnShare.style.display = "block"; //Hide main Share Button
        }
        else {
            divAttach.style.display = "block"; //Show Link div
            divAttachfileAndLink.style.display = "none"; //Hide File and Link div
            btnShare.style.display = "none"; //Hide main Share Button
        }


    }





    function OnMainShareFile_InsertClick() {
        var fileUpload = $get("txtfileUpload");
        var fileName = $get("txtfileName");
        var fileDesc = $get("txtfileDesc");
        if (fileUpload.value == '') {
            document.getElementById('txtfileUpload').focus();
            
            //document.getElementById('txtfileUpload').value="Please select file";        
            return;
        }
        
        if (fileName.value == '') {
            document.getElementById('txtfileName').focus();
            return;
        }

        var guidVal = guid();
        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var HiddenUserNameTxt = document.getElementById('<%=HiddenUserName.ClientID%>');
        var HiddenModuleTxt = document.getElementById('<%=HiddenModule.ClientID%>');
        var HiddenModuleIDTxt = document.getElementById('<%=HiddenModuleID.ClientID%>');

        DeffinityChatService.MainShareFile_Insert(HiddenUserNameTxt.value, HiddenUserIDTxt.value, fileUpload.value, fileName.value, fileDesc.value, guidVal, HiddenModuleTxt.value, HiddenModuleIDTxt.value, MainShareFile_Insert_Success);


        fileName.value = '';
    }

    function OnMainShareLink_InsertClick() {

        var LinkURL = $get("txtLinkURL");
        var LinkName = $get("txtLinkName");


        if (LinkURL.value == 'http://') {
            document.getElementById('txtLinkURL').focus();
            document.getElementById('txtLinkURL').value = 'http://';
            return;
        }

        
        if (LinkName.value == '') {
            document.getElementById('txtLinkName').focus();           
            return;
        }
        var HiddenUserIDTxt = document.getElementById('<%=HiddenUserID.ClientID%>');
        var HiddenUserNameTxt = document.getElementById('<%=HiddenUserName.ClientID%>');
        var HiddenModuleTxt = document.getElementById('<%=HiddenModule.ClientID%>');
        var HiddenModuleIDTxt = document.getElementById('<%=HiddenModuleID.ClientID%>');
        
        var guidVal = guid();
        DeffinityChatService.MainShareLink_Insert(HiddenUserNameTxt.value, HiddenUserIDTxt.value, LinkURL.value, LinkName.value, guidVal, HiddenModuleTxt.value, HiddenModuleIDTxt.value, MainShareLink_Insert_Success);

    }

    function MainShareLink_Insert_Success(result) {
        
        var guidVal = result;
        AddChild_To_divMainListLink(guidVal);

    }

    function MainShareFile_Insert_Success(result) {

        var guidVal = result;
        AddChild_To_divMainListFile(guidVal);

    }
    


    function Comment_Remove_Success(result) {
        //alert(document.getElementById('txtcommentBox').value);
    }
    function Comment_Insert_Success(result) {
        //alert(document.getElementById('txtcommentBox').value);
    }
    function LikeArticle_Insert_Success(result) {
        //alert(document.getElementById('txtcommentBox').value);
    }
    
    
    function txtshareonfocus() {        
        var lblName = document.getElementById('<%=lblTextShare.ClientID%>');
        lblName.style.visibility = 'hidden';        
        var txtName = document.getElementById('<%=txtTextShare.ClientID%>');
    }


</script>
  

    <ul>
        <li>
            <asp:TextBox ID="TextBox1" runat="server" Height="125px" TextMode="MultiLine" 
                Width="257px" Visible="False"   ></asp:TextBox>
        </li>
</ul>
                
    <div class="chatterbox">    
        
    
     
        <h1>Recent Chit Chat</h1>
     <div class="clr"></div>
        <div id="divChatter" class="chatter_carrier" runat="server" >
        <asp:HiddenField   ID="HiddenUserName" runat="server" />
        <asp:HiddenField    ID="HiddenUserPhoto" runat="server" />
        <asp:HiddenField    ID="HiddenProfile" runat="server" />
        <asp:HiddenField    ID="HiddenUserID" runat="server" />
        <asp:HiddenField    ID="HiddenModule" runat="server" />
        <asp:HiddenField    ID="HiddenModuleID" runat="server" />
        <div >
               <textarea id="txtTextShare"  onfocus="javascript:txtshareonfocus();"    class="share_field"  rows="2" cols="50" runat="server"></textarea>
               <label id="lblTextShare" runat="server" style="visibility:hidden;color:Red" >*</label>
               <input id="btnMainShare"  class="btn_share" title='Share' value="Share" runat="server"  type="button" onclick="OnMainShareText_InsertClick();" />            
               
        </div>        
        <div>
        <div>
        <div class="clr">

            </div>
            <div id="divAttachfileAndLink" class="attachment"  runat="server">
               <h2> Attach&nbsp;</h2> <a  style="display: none;" href="javascript:AttachFile();"><img  src="media/file.gif" /></a><p><a  style="display: none;" href="javascript:AttachFile();" id="hrefAttach" runat="server">File</a></p>
                
               <a href="javascript:AttachLink();"  ><img  src="media/link.gif" /></a><p><a href="javascript:AttachLink();" id="A2" runat="server">Link</a></p>
            </div><div class="clr"></div>
            <div id="divAttach" style="display:none; border: 1px solid rgb(1, 1, 1); " runat="server">                
               
                      <h2><div style="float:left"><img style="float:left" alt="Attach a File" src="media/file.gif"  />Attach a File </div><a href="javascript:AttachFile();" id="A1"><img alt="Close the publisher" src="media/Cancel_chat.gif"/></a></h2>
                    <div>
                        <ul>
                            <li > <label>&nbsp;</label><b>*</b> Required Information</li>
                            <li>
                                <label>
                                    Select a file </label><b>*</b>
                                <input type="file" id="txtfileUpload" /></li>
                            <li>
                                <label>
                                    Name </label><b>*</b>
                                <input id="txtfileName" /></li>
                            <li>
                                <label>
                                    Description</label><b>&nbsp;&nbsp;</b>
                                <textarea id="txtfileDesc" cols="30" rows="3"></textarea>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label>
                               <b>&nbsp;&nbsp;</b> <input id="btnShareFile" title="Share File" value="Share File" type="button" onclick="OnMainShareFile_InsertClick();" /></li>
                        </ul>
                </div>
              
            </div>
            <div id="divLink" style="display: none; border: 1px solid rgb(1, 1, 1);" runat="server">
            
               <h2><div style="float:left"><img style="float:left" alt="Attach a Link" src="media/link.gif"  /> Attach a Link </div>
                    <a href="javascript:AttachLink();" id="hrefAttachLinkClose">
                        <img alt="Close the publisher" src="media/Cancel_chat.gif"/></a> </h2>
                <div>
                 <ul>
                   <li>  <label>&nbsp;</label><b>*</b> Required Information<br /></li>
                <li>          <label>      Link URL </label><b>*</b><br />
                <input id="txtLinkURL" value="http://" /></li>
              
              <li> <label>   Link Name  </label><b>*</b><br />
                <input id="txtLinkName" /></li>
                <li><label>&nbsp;</label><b>&nbsp;&nbsp;&nbsp;</b><input id="btnShareLink" title="Share Link" value="Share Link" type="button" onclick="OnMainShareLink_InsertClick();" /></li></ul>   
                </div>
                
            </div>
            <div id="divMainCOM"  class="chatscroll" runat="server">
            
            </div>      
            
        </div>
    </div>
    </div>
    
    </div>