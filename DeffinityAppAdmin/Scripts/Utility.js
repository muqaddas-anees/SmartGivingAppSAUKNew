
$(window).on('load', function () {
//    debugger;
    $("div.valert-danger").each(function () {
        if ($(this).html().trim() == "") {
            $(this).hide(); // or .hide() to hide
        }
    });
});



function getQuerystring(key, default_) {

    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href.toLowerCase());
    if (qs == null)
        return default_;
    else
        return qs[1];
}


function hidepagetitle()
{
   // debugger;
    $(document).ready(function () {
        $('.page-title').hide();
    });

}

function setpagetitle() {
    //debugger;
    $(document).ready(function () {
        $('.page-title').hide();
    });

}


function setpagename(name) {
    debugger;
    $('div > h1.title').html(name);
}
function setdescription(name) {
    debugger;
    $(document).ready(function () {
        $('div > h1.description').html(name);
    });

}

function hidetabs() {
    $(window).load(function () {
        var length = $('#navTab ul > li').length;
        //alert(length);
        if (length == 0) {
            $('#navTab').hide();
        } else {
            $('#navTab').show();
        }
    });
}

function showtabs() {
    $(window).load(function () {
        $('#navTab').show();
    });
}


function seterror(elementid) {
    var value = $.trim($("#" + elementid).val());
    if (value.length != 0) {
        $('#' + elementid).addClass('bg-danger');
    }
}

valDesign();
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(valDesign);

function valDesign() {
    $("span.alert-danger:empty").each(function () {
        // alert($(this).empty());

        $(this).hide(); // or .hide() to hide
    });

    $("span.alert-danger:not(:empty)").each(function () {
        // var SuccessMessageDiv = "<div class='alert alert-success'></div>";
        var ErrorMessageDiv = "<div class='alert-danger text-light-danger bg-danger' style='padding:15px'></div>";
        $(this).wrap(ErrorMessageDiv);
        $(this).fadeOut(7000);
        $(this).closest("div").fadeOut(7000);
    });

    $("span.alert-success:empty").each(function () {
        // alert($(this).empty());
        $(this).hide(); // or .hide() to hide
    });

    $("span.alert-success:not(:empty)").each(function () {
        var SuccessMessageDiv = "<div class='alert-success text-light-success bg-success' style='padding:15px'></div>";
        $(this).wrap(SuccessMessageDiv);
        $(this).fadeOut(7000);
        $(this).closest("div").fadeOut(7000);
    });

    function displayMsg(element, msg, msgtype) {
        if (msgtype == 'error') {
            $('[id*=' + element + ']').html('<p class="alert-danger text-light-danger bg-danger"  style="padding: 15px">' + msg + '</p>');
        }
        else if (msgtype == 'success') {
            $('[id*=' + element + ']').html('<p class="alert-success text-light-success bg-success" style="padding: 15px">' + msg + '</p>');
        }
        else if (msgtype == 'clear') {
            $('[id*=' + element + ']').html('');
        }
        else {
            $('[id*=' + element + ']').html('');
        }

    }

    /* Add Button*/
    //$("input.btn-secondary[value|='Add']").each(function (index) {
    //    var cls = "myadd" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-plus-square-o'></i>");

    //});

    ///* Add new Button*/
    //$("input.btn-secondary[value|='Add New']").each(function (index) {
    //    var cls = "myaddnew" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-plus-square-o'></i>");

    //});

    ///* Edit Button*/
    //$("input.btn-secondary[value|='Edit']").each(function (index) {
    //    var cls = "myedit" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-pencil'></i>");

    //});

    ///* Cancel Button*/
    //$("input.btn-secondary[value|='Cancel']").each(function (index) {
    //    var cls = "mycancel" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-times'></i>");

    //});

    ///* Delete Button*/
    //$("input.btn-secondary[value|='Delete']").each(function (index) {
    //    var cls = "mydelete" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-trash-o'></i>");

    //});

    ///* Print Button*/
    //$("input.btn-secondary[value|='Print']").each(function (index) {
    //    var cls = "myprint" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-print'></i>");
    //});
    ///* Report Button*/
    //$("input.btn-secondary[value|='Report']").each(function (index) {
    //    var cls = "myreport" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-pie-chart'></i>");
    //});

    ///* Search Button*/
    //$("input.btn-secondary[value|='Search']").each(function (index) {
    //    var cls = "mysearch" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-search'></i>");
    //});
    ///* Save Button*/
    //$("input.btn-secondary[value|='Save']").each(function (index) {
    //    var cls = "mysave" + index;
    //    var divspan = "<div class='btn btn-secondary btn-icon btn-icon-standalone " + cls + "'></div>";
    //    $(this).wrap(divspan);
    //    $("." + cls).append("<i class='fa-save'></i>");
    //    $("." + cls).css("width", "95px");
    //});

}