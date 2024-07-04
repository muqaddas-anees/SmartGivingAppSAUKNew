//$(document).ready(function () {
//    $(".navbar-nav a").each(function (index, element) {
//        var cu = $(location).attr('href').toLowerCase();
//        var ck = $(element).attr('href').toLowerCase();
//        if (cu.indexOf($.trim(ck)) > -1) {
//            $(element).attr('class', 'active');
//            $(element).parents('li').attr('class', 'active');
//           // $(element).prepend("<i class='fa fa-circle' style='color:green;'></i>  ");
//            debugger;
//            if ($(element).html().toLocaleLowerCase().indexOf("fa fa-circle") != -1) {

//               // alert("found");
//            }
//            else
//                $(element).prepend("<i class='fa fa-circle' style='color:green;'></i>  ");
//                //alert($(element).html());
//            //return false;
//        }
//    });
//});
$(document).ready(function () {
    $(".navbar-nav a").each(function (index, element) {
        var cu = $(location).attr('href').toLowerCase();
        var ck = $(element).attr('href').toLowerCase();
        if (cu.indexOf($.trim(ck)) > -1) {
            $(element).attr('class', 'active');
            $(element).parents('li').attr('class', 'active');
            $(element).prepend("<i class='fa fa-circle' style='color:#FF6600;'></i>  ");
            //return false;
        }
    });
});

function activeTab(name) {
    $(".navbar-nav a").each(function (index, element) {
        var cu = name.toLowerCase().trim();
        var ck = $(element).html().toLowerCase().trim();
       
        if (cu.indexOf($.trim(ck)) > -1) {
            $(element).closest('li').attr('class', 'active');
            $(element).prepend("<i class='fa fa-circle' style='color:#FF6600;'></i>  ");
        }
    });
}

//function activeTab(name) {
//    $(".navbar-nav a").each(function (index, element) {
//        var cu = name.toLowerCase();
//        var ck = $(element).html().toLowerCase();

//        //debugger;
//        if (cu.indexOf($.trim(ck)) > -1) {
//            $(element).closest('li').attr('class', 'active');
//            //alert($(element).has('.active').length);
//            if ($(element).has('.active').length) {
//                //do something
//            }
//            else
//            $(element).prepend("<i class='fa fa-circle' style='color:green;'></i>  ");
//        }
//    });
//}