//apply style to Table / grid header


$(document).ready(function () {
   //EndRequestHandler();
    //$('.mediaTable').mediaTable();
    (function ($) {

        $.fn.checked = function (value) {

            if (value === true || value === false) {
                // Set the value of the checkbox
                $(this).each(function () { this.checked = value; });

            } else if (value === undefined || value === 'toggle') {

                // Toggle the checkbox
                $(this).each(function () { this.checked = !this.checked; });
            }

        };

    })(jQuery);


});
//update date time
function grid_edit_style(index)
{
    $(".dataTable").each(function (i, element) {
        var jTbl = $(element);
        if (index == 0) {
            jTbl.find("tbody>tr>td a").wrap("<div class='block-options'></div>");
        }
        else {
            jTbl.find("tbody>tr>td:nth-child(" + index + ")").addClass('block-options form-inline');
            jTbl.find("tbody>tr>td:gt(" + (index + 1) + ") a").wrap("<div class='block-options'></div>");
            jTbl.find("tbody>tr>td:lt(" + (index - 1) + ") a").wrap("<div class='block-options'></div>");
        }
    });
}


function AddHeaderClass(tableEl) {
    debugger;
    var jTbl = $(tableEl).first();
    var s = jTbl.find("thead>tr>th").length;
    //data-tablesaw-priority
    if (jTbl.find("thead>tr>th").length > 0) {
        $(jTbl.find("thead>tr")).attr("data-tablesaw-priority", "persist");
        $(jTbl.find("thead>tr>th")).each(function (index, element) {
            //for (i = 0; i < Columns.length; i++) {
                $(element).attr("data-priority", index + 1);
            //}
        });

        //if (jTbl.find("thead>tr>th").length > 0) {
        //    $(jTbl.find("thead>tr>th")).each(function (index, element) {
        //        for (i = 0; i < Columns.length; i++) {
        //            $(element).attr("data-priority", index + 1);
        //        }
        //    });
            //for (i = 0; i < Columns.length; i++) {
            //    jTbl.find("tbody>tr>th:nth-child(" + Columns[i] + ")").removeAttr('data-priority');
            //    jTbl.find("thead>tr>th:nth-child(" + Columns[i] + ")").removeAttr('data-priority');
            //}
        return false;
   }
}

function Apply_th(tableEl)
{
    var Columns = [1];
    var jTbl = $(tableEl);
    for (i = 0; i < Columns.length; i++) {
        
        $(jTbl.find("tbody tr td:nth-child(" + Columns[i] + ")")).each(function () {
            if ($(this).closest('tr').attr('class') == 'even' || $(this).closest('tr').attr('class') == 'odd')
            { $(this).replaceWith('<th>' + $(this).html() + '</th>'); }
        });
    }
}
function Apply_jtable_th() {
    var Columns = [1];
    var jTbl = $('.jtable');
    for (i = 0; i < Columns.length; i++) {

        $(jTbl.find("tbody tr td:nth-child(" + Columns[i] + ")")).each(function () {
            if ($(this).closest('tr').attr('class') == 'jtable-data-row')
            { $(this).replaceWith('<th>' + $(this).html() + '</th>'); }
        });
    }
}

function fixGridView(tableEl) {

    var jTbl = $(tableEl);

    if (jTbl.find("tbody>tr>th").length > 0) {
        jTbl.find("tbody").before("<thead><tr></tr></thead>");
        jTbl.find("thead:first tr").append(jTbl.find("th"));
        jTbl.find("tbody tr:first").remove();
        //jTbl.find("tbody tr:last").addClass("dataTables_paginate paging_bootstrap pagination pagination-sm remove-margin");
        //jTbl.find("tbody>tr>td a").closest("td").addClass("block-options");
       
        return false;
    }
    //var jTbl_all = $(tableEl);
    //var jTbl = jTbl_all.eq(0);
    //var i = jTbl.find("tbody>tr>th").length;
    //debugger;
    //if (jTbl.find("tbody>tr>th").length > 0) {
    //    jTbl.find("tbody").before("<thead><tr></tr></thead>");
    //    jTbl.find("thead tr").append(jTbl.find("th"));
    //    jTbl.find("tbody tr:first").remove();
    //    jTbl.find("tbody>tr>td a").closest("td").addClass("block-options");
       
    //    return false;
    //}
}
function fixGridView_Nested(tableEl) {

    var jTbl = $(tableEl);

    if (jTbl.find("tbody>tr>th").length > 0) {
        jTbl.find("tbody").before("<tbody><tr></tr></tbody>");
        jTbl.find("thead:first tr").append(jTbl.find("th"));
        jTbl.find("tbody tr:first").remove();
        //jTbl.find("tbody tr:last").addClass("dataTables_paginate paging_bootstrap pagination pagination-sm remove-margin");
        //jTbl.find("tbody>tr>td a").closest("td").addClass("block-options");
       
        return false;
    }
    //var jTbl_all = $(tableEl);
    //var jTbl = jTbl_all.eq(0);
    //var i = jTbl.find("tbody>tr>th").length;
    //debugger;
    //if (jTbl.find("tbody>tr>th").length > 0) {
    //    jTbl.find("tbody").before("<thead><tr></tr></thead>");
    //    jTbl.find("thead tr").append(jTbl.find("th"));
    //    jTbl.find("tbody tr:first").remove();
    //    jTbl.find("tbody>tr>td a").closest("td").addClass("block-options");
       
    //    return false;
    //}
}

function fixGridViewFooter(tableEl) {
    var jTbl = $(tableEl);
    jTbl.wrap("<div class=' row'></div>");
    jTbl.each(function () {
        var list = $("<div class='dataTables_paginate paging_bootstrap' />");
        $(this).find("tr").each(function () {
            var p = $(this).children().map(function () {
                var ihtml = $(this).html();
                if (ihtml.indexOf($.trim('span')) > -1) {
                   return "<li class=' active'>" + $(this).html() + "</li>";
                }
                else
                return "<li>" + $(this).html() + "</li>";
            });
            list.append("<ul class='pagination pagination-sm remove-margin'>" + $.makeArray(p).join("") + "</ul>");
        });
        $(this).replaceWith(list);
    });
    
    return false;
}
function EndRequestHandler()
{
    debugger;
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
    
    $("table.dataTable").each(function (index, element) {
        var jTbl = $(element);
        fixGridView($(element));
        
        AddHeaderClass($(element).first());
        Apply_th($(element).first());
    });

    $("table.dataTable table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}

function grid_responsive() {
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");

    $("table.dataTable").each(function (index, element) {
        var jTbl = $(element);
        fixGridView($(element));
        AddHeaderClass($(element));
        Apply_th($(element));
    });

    $("table.dataTable table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}

function grid_responsive_fixedHeader() {
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='false' data-add-display-all-btn='false' data-add-focus-btn='false'></div>");

    $("table.dataTable").each(function (index, element) {
        var jTbl = $(element);
        fixGridView($(element));
        AddHeaderClass($(element));
        Apply_th($(element));
    });

    $("table.dataTable table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}

function grid_responsive_display() {
    debugger;
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='false' data-add-display-all-btn='false' data-add-focus-btn='false'></div>");

    $("table.dataTable").each(function (index, element) {
        if (index == 0) {
            var jTbl = $(element).find("table:first");
            fixGridView($(element).find("table:first"));
            AddHeaderClass($(element).find("table:first"));
            Apply_th($(element).find("table:first"));
        }
    });

    //$("table.dataTable table").each(function (index, element) {
    //    var jTbl1 = $(element);
    //    fixGridViewFooter($(element));
    //});
     $(".pager_css table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}

function grid_responsive_parent_display() {
    debugger;
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='false' data-add-display-all-btn='false' data-add-focus-btn='false'></div>");

    $("table.dataTable").each(function (index, element) {
        if (index == 0) {
            var jTbl = $(element).find("table:first");
            fixGridView($(element).find("table:first"));
            AddHeaderClass($(element).find("table:first"));
            Apply_th($(element).find("table:first"));
        }
    });

    //$("table.dataTable table").each(function (index, element) {
    //    var jTbl1 = $(element);
    //    fixGridViewFooter($(element).find("table:last"));
    //});
    $(".pager_css table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}


function grid_responsive_nested_display() {
    debugger;
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    $(".nestedtable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='false' data-add-display-all-btn='false' data-add-focus-btn='false'></div>");

    $("table.nestedtable").each(function (index, element) {
       
            var jTbl = $(element).find("table:first");
        //fixGridView($(element).find("table:first"));
            fixGridView_Nested($(element).find("table:first"));
            AddHeaderClass($(element).find("table:first"));
            Apply_th($(element).find("table:first"));
        
    });

    //$("table.nestedtable table").each(function (index, element) {
    //    var jTbl1 = $(element);
    //    fixGridViewFooter($(element));
    //});
    $(".pager_css table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}
function grid_css() {
    $(".input_date").closest("td").addClass("form-inline");
    $(".input_time").closest("td").addClass("form-inline");
    //$(".dataTable").wrap("<div class='dataTables_wrapper form-inline dt-bootstrap no-footer' ></div>");

    $("table.editable").each(function (index, element) {
        var jTbl = $(element);
        fixGridView($(element));
        AddHeaderClass($(element));
        Apply_th($(element));
    });

    $("table.editable table").each(function (index, element) {
        var jTbl1 = $(element);
        fixGridViewFooter($(element));
    });
}

function AddJtableHeaderClass() {
    var jTbl = $('.jtable');
    if (jTbl.find("thead>tr>th").length > 0) {
        $(jTbl.find("thead>tr>th")).each(function (index, element) {
            //for (i = 0; i < Columns.length; i++) {
            $(element).attr("data-priority", index + 1);
            //}
        });

        //if (jTbl.find("thead>tr>th").length > 0) {
        //    $(jTbl.find("thead>tr>th")).each(function (index, element) {
        //        for (i = 0; i < Columns.length; i++) {
        //            $(element).attr("data-priority", index + 1);
        //        }
        //    });
        //for (i = 0; i < Columns.length; i++) {
        //    jTbl.find("tbody>tr>th:nth-child(" + Columns[i] + ")").removeAttr('data-priority');
        //    jTbl.find("thead>tr>th:nth-child(" + Columns[i] + ")").removeAttr('data-priority');
        //}
        return false;
    }
}

function jtable_css() {
   // $(".input_date").closest("td").addClass("form-inline");
   // $(".input_time").closest("td").addClass("form-inline");
    //$(".dataTable").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
    
    $("table.dataTable").each(function (index, element) {
        //debugger;
        //alert(index);
        //if (index == 0) {
            var jTbl = $(element);
            //fixGridView($(element));
           // AddJtableHeaderClass();
            //AddHeaderClass($(element));
            //Apply_th($(element));
            Apply_jtable_th();
        //}
    });

    //$("table.dataTable table").each(function (index, element) {
    //    debugger;
    //    if (index == 0) {
    //        var jTbl1 = $(element);
    //        fixGridViewFooter($(element));
    //    }
    //});
}

function GridResposiveCssSub()
{

    grid_responsive_display();
    $(window).on('load', function () {
        $("button:contains('Display all')").click(function (e) {
            e.preventDefault();
            $(".dropdown-menu li")
      .find("input[type='checkbox']")
      .prop('checked', 'checked').trigger('change');
        });
    });
}

function GridResponsiveCss()
{
    grid_responsive_display();
    $(window).on('load', function () {
        $("button:contains('Display all')").click(function (e) {
            e.preventDefault();
            $(".dropdown-menu li")
      .find("input[type='checkbox']")
      .prop('checked', 'checked').trigger('change');
        });
    });

   // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResposiveCssSub);

}

function NestedGridResponsiveCss() {

    grid_responsive_parent_display();
    grid_responsive_nested_display();

    $(window).on('load', function () {
        $(".dropdown-menu li")
      .find("input[type='checkbox']")
      .prop('checked', 'checked').trigger('change');
        $(".btn-toolbar").hide();
    });
}

//Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);