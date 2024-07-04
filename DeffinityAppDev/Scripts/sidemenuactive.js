$(document).ready(function () {
    var url = window.location;
    $('.main-menu a').filter(function () {
        return this.href == url;
    }).parents('li').addClass('opened');

    $(".main-menu a").each(function (index, element) {
        var cu = $(location).attr('href').toLowerCase();
        var ck = $(element).attr('href').toLowerCase();
        if (cu.indexOf($.trim(ck)) > -1) {
            //  $(element).attr('class', 'active');
            $(element).closest('li').addClass('active');
            return false;
        }
    });
});

function sideMenuActive(name) {
    $(".main-menu span").each(function (index, element) {
        var cu = name.toLowerCase();
        var ck = $(element).html().toLowerCase();
        if (cu.indexOf($.trim(ck)) > -1) {
            $(element).closest('li').attr('class', 'active');
        }
    });
}