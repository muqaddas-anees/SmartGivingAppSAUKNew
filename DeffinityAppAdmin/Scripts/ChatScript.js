

$(function () {

    setScreen(false);

    // Declare a proxy to reference the hub. 
    var chatHub1 = $.connection.myChatHub;

    registerClientMethods(chatHub1);

    // Start Hub
    $.connection.hub.start().done(function () {

        registerEvents(chatHub1)

    });

});

function setScreen(isLogin) {

    if (!isLogin) {

        $("#divChat").hide();
        $("#divLogin").show();
    }
    else {

        $("#divChat").show();
        $("#divLogin").hide();
    }

}

function registerEvents(chatHub1) {



    var name = $("#txtNickName").val();
    var ID = $("#txtID").val();
    if (name.length > 0) {
        chatHub1.server.connect(name, ID);
    }
    else {
        alert("Please enter name");
    }



}

function registerClientMethods(chatHub1) {

    // Calls when user successfully logged in
    chatHub1.client.onConnected = function (id, userName, allUsers, messages, uid) {

        setScreen(true);

        $('#hdId1').val(id);
        $('#hduId1').val(uid);
        $('#hdUserName1').val(userName);
        $('#spanUser').html(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub1, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].userid);
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {

            AddMessage(messages[i].UserName, messages[i].Message);
        }


    }

    // On New User Connected
    chatHub1.client.onNewUserConnected = function (id, name, uid) {
        AddUser(chatHub1, id, name, uid);
    }


    // On User Disconnected
    chatHub1.client.onUserDisconnected = function (id, userName) {
        $('#' + id).remove();
    }




    chatHub1.client.sendPrivateMessage = function (windowId, fromUserName, message, fromuid) {

        var ctrId = 'private_' + windowId;
        //to get current time in 12 hour format
        var date = new Date();
        var hours = date.getHours() > 12 ? date.getHours() - 12 : date.getHours();
        var am_pm = date.getHours() >= 12 ? "PM" : "AM";
        hours = hours < 10 ? "0" + hours : hours;
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var time = hours + ":" + minutes + " " + am_pm;

        if ($('#' + ctrId).length == 0) {

            createPrivateChatWindow(chatHub1, windowId, ctrId, fromUserName);

        }

        $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName" style="color: #a6a6a6;font-size:medium">' + fromUserName + '</span> <span style="float:right;font: status-bar;color: #a6a6a6;">' + time + '</span><br/>' + message + '</div><div class="li12"/>');

        // set scrollbar
        var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').scrollTop(height);

    }


    chatHub1.client.userstatusonline = function (id, name) {
        var userId = $('#hdId1').val();
        if (userId != id)
            OpenPrivateChatWindow(chatHub1, id, name);
    }

    chatHub1.client.userstatusoffline = function (id, name) {
        debugger;
        $('#private_' + id).remove();
        OpenPrivateChatWindow(chatHub1, id, name);
        $('#private_' + id).find('#divMessage').append('<div class="message"><p style="text-align:center"> user is not active </p></div>');

    }

}
function getid(idd) {
    var chatHub1 = $.connection.myChatHub;
    var id1 = idd.id;
    debugger;
    chatHub1.server.checkuserstatus(id1, idd.name);
   // return false;

}

function AddUser(chatHub1, id, name, uid) {

    var userId = $('#hdId1').val();
    var uidd = $('#hduId1').val();
    var code = "";

    if (userId == id || uidd == uid) {

        code = $('<div class="loginUser" hidden="hidden">' + name + "</div>");
        //document.getElementById(uid).style.display = "none";
    }
}



function OpenPrivateChatWindow(chatHub1, id, userName) {

    var ctrId = 'private_' + id;

    if ($('#' + ctrId).length > 0) return;

    createPrivateChatWindow(chatHub1, id, ctrId, userName);

}

function createPrivateChatWindow(chatHub1, userId, ctrId, userName) {
    var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" style="z-index:1000;" rel="0">' +
               '<div class="header">' +
                  '<div  style="float:right;padding-right: .2em;padding-top: .18em;">' +
                      '<img id="imgDelete"  style="cursor:pointer;" src="/images/delete.png"/>' +
                   '</div>' +

                   '<span class="selText" rel="0">' + userName + '</span>' +
               '</div>' +
               '<div id="divMessage" class="messageArea">' +

               '</div>' +
               '<div class="buttonBar">' +
                  '<input id="txtPrivateMessage" class="msgText" type="text"/>' +
                  '<input id="btnSendMessage" class="submitButton button" type="button" value="Send"   />' +
               '</div>' +
            '</div>';

    var $div = $(div);
    AddDivToContainer($div);
    // DELETE BUTTON IMAGE
    $div.find('#imgDelete').click(function () {
        $('#' + ctrId).remove();
    });

    // Send Button event
    $div.find("#btnSendMessage").click(function () {

        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        var fromuid = $('#hduId1').val();
        if (msg.length > 0) {

            chatHub1.server.sendPrivateMessage(userId, msg, fromuid);
            $textBox.val('');
        }
    });


    // Text Box event
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });

}

function AddDivToContainer($div) {
    $('#dataContainer').prepend($div);

    $div.draggable({

        handle: ".header",
        stop: function () {

        }
    });
}

