//page load
var mform = '#pnlform';

$(document).ready(function () {
    
    debugger;
    $(document).ajaxStart(function () {
        
        $("#lblProgress").show();
        $("#lblProgress").html("please wait...");
        
    }).ajaxStop(function () {
        $("#lblProgress").html("");
        $("#lblProgress").hide();
    });
            //$(function () {
            //    $("#divmenu").menu();
            //});

            $(document.body).find("[id$='lblPageHead']").html('Health Check');
            var formid = getQuerystring('fid');
    debugger;
    if (formid == '') {
        
        ShowFromDialog();
    }
    else {
        //

        //load the form data
        GetFormData(formid);
        //getControllist(formid);

        getPanlelist(formid);
        GetTextboxId(formid);
    }
         //panel position
            $('#').click(function () {
               
                $('#Panelposition').dialog({
                    resizable: false,
                    height: 250,
                    modal: true,
                    buttons: {
                        "Submit": function () {
                            var pname = $('#txtPanelName').val();
                            var row = parseInt($('#txtRows').val());
                            var columns = parseInt($('#txtColumns').val());

                            createpanel(formid, pname, row, columns);
                            $(this).dialog("close");
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
            // Add panel
            $('#btnAddPanel').click(function () {
                var strbuild = '';
                $('#pnlAdd').dialog({
                    resizable: false,
                    height: 350,
                    modal: true,
                    buttons: {
                        "Submit": function () {
                            var pname = $('#txtPanelName').val();
                            var row = parseInt($('#txtRows').val());
                            var columns = parseInt($('#txtColumns').val());
                            
                            createpanel(formid, pname, row, columns);
                            $(this).dialog("close");
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
            //set back color to form
            $('#btnSetBack').click(function () {
                $('#pnlColor').dialog({
                    resizable: false,
                    height: 200,
                    modal: true,
                    buttons: {
                        "Submit": function () {
                            $('div#cblist input[type=checkbox]').each(function () {
                                if ($(this).is(":checked")) {
                                    UpdateFormBackcolor(mform);
                                }
                            });
                            $(this).dialog("close");
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
    //add signature panel
            $('#btnSignature').click(function () {
                createSignaturepanel(formid, 'Signature Panel', 1, 2);
                return false;
            });
          //color panel set checked only one 
            $('div#cblist :checkbox').on('change', function () {
                var th = $(this), name = th.prop('name');
                if (th.is(':checked')) {
                    $(':checkbox[name="' + name + '"]').not($(this)).prop('checked', false);
                }
            });
           //Add Header panel
            $('#btnHeader').click(function () {
                createheaderpanel(formid, 'Header', 1, 2);
                return false;
            });
 
            //update form name
            $('#btnUpdateform').click(function (e) {
                UpdateFormName();
                e.preventDefault();
            });
            //first time
            controlOption();
            $('#ddlfieldtype').change(function () {
                ClearControlDataInddlChange();
                controlOption()
            });
           
});

function applyDatePicker() {
    $(window).load(function () {
       
        $(".form-control").each(function () {
            if (this.style.width == '121px') {
                $(this).datepicker({ dateFormat: 'dd/mm/yy' });
                //$("#ctl00_MainContent_" + this.CntlID + "").datepicker({ dateFormat: 'dd/mm/yy' }); 
            }
        });

    });
}

function controlOption() {


    var lval = $('#ddlfieldtype').val();
    $("#divlistval").show();
    $("#IDDefaultValue").html("Default Value");
    $("#ListValues").html("List Values");

    $("#divimagecontrol").hide();
    $("#divlbl").show();
    $("#divlabelsize").hide();
    $("#divlabelbold").hide();

    $("#DivDropdownTypes").hide();
    if (lval == 'Date') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".c_class").each(function () {
            $(this).hide();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").hide();
        $("#DivMandatory").show();
        $("#DivPosition").hide();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Textbox') {
        $(".t_class").each(function () {
            $(this).show();
        });
        $(".l_class").each(function () {
            $(this).hide();
        });
        $(".d_class").each(function () {
            $(this).show();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Checkbox') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".c_class").each(function () {
            $(this).hide();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Label') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".c_class").each(function () {
            $(this).hide();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").show();
        $("#divlabelbold").show();
    }
    else if (lval == 'Textarea') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class").each(function () {
            $(this).hide();
        });
        $(".d_class").each(function () {
            $(this).show();
        });
        $(".H_class").each(function () {
            $(this).show();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Dropdown') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class").each(function () {
            $(this).show();
        });
        $(".d_class").each(function () {
            $(this).hide();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
        $("#DivDropdownTypes").show();
    }
    else if (lval == 'Image') {
        $(".d_class").each(function () {
            $(this).hide();
        });
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").hide();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#DivWidth").show();
        $("#DivMandatory").hide();
        $("#DivPosition").show();
        $("#divlbl").hide();
        $("#divimagecontrol").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Table') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class d_class").each(function () {
            $(this).hide();
        });
        $(".c_class").each(function () {
            $(this).show();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#tableoptions").show();
        $("#columnDivList").show();
        $("#Div_DefaultValue").hide();
        $("#DivWidth").hide();
        $("#DivMandatory").hide();
        $("#DivPosition").hide();
        $("#ListValues").html("Row values");
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();
    }
    else if (lval == 'Checkboxlist') {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class c_class").each(function () {
            $(this).show();
        });
        $(".d_class").each(function () {
            $(this).show();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").show();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        $("#IDDefaultValue").html("Number of Listview Columns(Integer)");
        $("#txtdefault").val("1");
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlistval").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();

    }
    else {
        $(".t_class").each(function () {
            $(this).hide();
        });
        $(".l_class c_class").each(function () {
            $(this).show();
        });
        $(".d_class").each(function () {
            $(this).show();
        });
        $(".H_class").each(function () {
            $(this).hide();
        });
        $("#divrblist").show();
        $("#tableoptions").hide();
        $("#columnDivList").hide();
        //$("#IDDefaultValue").html("Number of Listview Columns(Integer)");
        $("#DivWidth").show();
        $("#DivMandatory").show();
        $("#DivPosition").show();
        $("#divlabelsize").hide();
        $("#divlabelbold").hide();

    }
}

$(document).ready(function () {
    $(window).load(function () {
        $(".editbutton").button({ icons: { primary: 'ui-icon-check' } });
    });

});
function MenuLoad()
{
    $(document).ready(function () {
        $('.myMenu > li').bind('mouseover', openSubMenu);
        $('.myMenu > li').bind('mouseout', closeSubMenu);

        function openSubMenu() {
            $(this).find('ul').css('visibility', 'visible');
        };

        function closeSubMenu() {
            $(this).find('ul').css('visibility', 'hidden');
        };

    });
}
//Menu string
function GetPanelMenu()
{
    return "";
}
function GetControlMenu() {
    return "";
}
function getFormid()
{
    return getQuerystring('fid');
}
//logo region Start
function uploadCustomerlogo(e)
{
    debugger;
    var el = $(e).closest('.td_cls');
    var pid = $(el).attr('id');

    var fileUpload = $('#fileCustomerLogo').get(0);
    var files = fileUpload.files;

    var fdata = new FormData();
    for (var i = 0; i < files.length; i++) {
        fdata.append(files[i].name, files[i]);
    }

    var options = {};
   
    options.url = "../HC/HCWebService.asmx/UploadLogo?pid=" + pid;
    options.type = "POST";
    options.data = fdata;
    options.contentType = false;
    options.processData = false;
    options.async= true,
    options.success = function (result) {
        var obj = result;
        debugger;
        if (result != '') {
            setSuccessMsg("Uploaded successfully.");
            GetControlImageData_customer(pid);
        }
    };
    options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };
   
    $.ajax(options);
    return false;
}

function uploadOurlogo(e) {
    
    var el = $(e).closest('.td_cls');
    var pid = $(el).attr('id');

    var fileUpload = $('#fileOurLogo').get(0);
    var files = fileUpload.files;

    var fdata = new FormData();
    for (var i = 0; i < files.length; i++) {
        fdata.append(files[i].name, files[i]);
    }

    var options = {};

    options.url = "../HC/HCWebService.asmx/UploadLogo?pid=" + pid;
    options.type = "POST";
    options.data = fdata;
    options.contentType = false;
    options.processData = false;
    options.async = true,
    options.success = function (result) {
        var obj = result;
        
        if (result != '') {
            setSuccessMsg("Uploaded successfully.");
            
            GetControlImageData_our(pid);
            //getControllist(getFormid());
            //UpdateImgaeControl(pid, "<div class='imgCustomer'></div>");
            // $('#imgCustomer').append("<img src='../UploadData/HC/" + obj.ControlValue.toString() + ".png'/>");
        }
    };
    options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };

    $.ajax(options);
    return false;
}
function GetTextboxId(panelid)
{
    debugger;
    var el = $()
    $.ajax({
        url: "../HC/HCWebService.asmx/GetTextBoxId?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                debugger;
                var obj = jQuery.parseJSON(data.d);
                //debugger;
                //alert(data.CntlID);
                //debugger;
                if (obj != '')
                    debugger;
                $.each(obj, function () {
                    $("#" + this.CntlID + "").datepicker();
                });
            }
        },
        error: function (msg) { setMsg(Error); }
    })
}
function GetControlImageData_customer(panelid) {
    
    var el = $()
    $.ajax({
        url: "../HC/HCWebService.asmx/GetFormControlData?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                $('#imgCustomer').empty();
                var customerid = $('#hcustomerid').val(); 
                //undefined
                if (obj.ControlValue != undefined)
                    $('#imgCustomer').append("<img src='../../../WF/UploadData/HC/" + obj.ControlValue + ".png'/>");
                else
                    $('#imgCustomer').append("<img src='../../../WF/UploadData/Customers/portfolio_" + customerid + ".png'/>");
                    //$('#imgCustomer').append("");
               
            }

        },
        error: function (msg) { setMsg(Error); }
    });
}

function GetControlImageData_our(panelid) {
   
    $.ajax({
        url: "../HC/HCWebService.asmx/GetFormControlData?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                $('#imgOurlogo').empty();
                if (obj.ControlValue != undefined) {
                    $('#imgOurlogo').append("<img src='../../../WF/UploadData/HC/" + obj.ControlValue + ".png'/>");
                }
                else {
                    $('#imgOurlogo').append("");
                }
                
            }

        },
        error: function (msg) { setMsg(Error); }
    });
}
//logo region End
function GetFormControl(panelid) {
  
    $.ajax({
        url: "../HC/HCWebService.asmx/GetFormControlData?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                $('#' + panelid).empty();
                if (obj != '')
                {
                    $('#' + panelid).append("<div style='float:right;'> <a onclick='EditControlDialog(this);'><i class='fa fa-pencil' title='Edit Control'></i></a>&nbsp;&nbsp;<a onclick='DeleteControl(this);'><i class='fa fa-trash' title='Delete Control'></i></a></div><br/><br/>");
                    $('#' + panelid).append("<div style='text-align:center;vertical-align:bottom;padding-top:3px;border-style:solid;border-width:2px;border-color:lightblue;background-color:white;'><lable>" + obj.TypeOfField + "</lable>:<lable>" + obj.ControlLabelName + "</lable></div>");
                }
                else {
                    $('#' + panelid).append("<div style='float:right;'> <a onclick='AddControlDialog(this);' >  <i class='fa fa-plus' title=' Add Control'></i> </a></div>");
                }
              
            }

        },
        error: function (msg) { setMsg(Error); }
    });
}
function BindPanelName(panelid) {
    $.ajax({
        url: "../HC/HCWebService.asmx/BindPanelName?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                $('#txtEditPanelName').val(obj.PanelName);
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}
function SetFormControlValues(panelid) {
    $.ajax({
        url: "../HC/HCWebService.asmx/GetFormControlData?pid=" + panelid,
        type: "POST",
        data: "{'panelid': '" + panelid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);

                $('#txthelptext').val(obj.Helptext);

                if (obj.DefaultText != "") {
                    $("#ddltypeOfData option:contains(" + obj.DefaultText + ")").attr('selected', 'selected');
                }
                else {
                    $('#ddltypeOfData').val('Please select...');
                }
                $('#ddlfieldtype').val(obj.TypeOfField);
                $('#txtlabel').val(obj.ControlLabelName);
                $('#txtdefault').val(obj.DefaultText);
                $('#txtmin').val(obj.MinValue);
                $('#txtmax').val(obj.MaxValue);
                $('#txtlist').val(obj.ListValues);
                $('#txtclist').val(obj.columnlist);
                $('#txtwidth').val(obj.ControlWidth);
                $('#ddlposition').val(obj.ControlPosition);
                var Clist = $("[id*=rbcontrolsList] input[value=" + obj.TypeofFieldInTbl + "]");
                Clist.prop("checked", true)
                debugger;
                var radio = $("[id*=rblist] input[value=" + obj.CblPosition + "]");
                radio.prop("checked", true);
                debugger;

                if (obj.Mandatory == true) {
                    $('#chkmandate').attr('checked', true);
                }
                else {
                    $('#chkmandate').attr('checked', false);
                }
                if (obj.TypeOfField == "Label") {
                    $('#ddlBold').val(obj.MinValue);
                    $('#txtSize').val(obj.MaxValue);
                }
                else {
                    $('#ddlBold').val("False");
                    $('#txtSize').val("");
                }
                debugger;
                controlOption();
            }

        },
        error: function (msg) { setMsg(Error); }
    });
}

function PanelHtml(panelname, panelid, row, columns, backcolor)
{
    
    var strbuild = "<div class='pnl250' style='background-color:" + backcolor + ";' id='" + panelid.toString() + "'><fieldset><legend>" + panelname + " </legend>";
    strbuild += "<div style='float:right;'><a onclick='ChangePanelPosition(this);'><i class='fa fa-arrow-circle-o-up' title='Adjust Panels'></i></a>&nbsp;&nbsp;&nbsp;<a onclick='Editpanelname(this);' ><i class='fa fa-pencil' title='Edit Panel Name'></i></a> &nbsp;&nbsp;<a onclick='pcolorClick(this);' > <i class='fa fa-qrcode' title='Set Panel Background Colour'></i>  </a>&nbsp;&nbsp;<a onclick='pdelClick(this);'><i class='fa fa-trash' title='Delete Control'></i></a></div>";
    if (row > 0) {
        strbuild += "<table style='width:100%'>";
       
        for (i = 1; i <= row; i++) {
           
            strbuild += "<tr>";
            for (j = 1; j <= columns; j++) {
               
                strbuild += "<td class='td_cls' id='" + panelid.toString() + "_" + i.toString() + "_" + j.toString() + "'> ";
               
                //check already control exists
                //var cstr = GetControl($('#divdata').data() , panelid, i, j);
              
                    GetFormControl(panelid.toString() + "_" + i.toString() + "_" + j.toString());
                
                //debugger;
                //if (obj == '') {
                //    strbuild += "<div style='float:right;'> <a onclick='AddControlDialog(this);' > Add Control </a></div>";
                   
                //}
                //else {
                //    //"<div style='float:right;'> <a onclick='EditControlDialog(this);'> Edit Control </a><br><a onclick='DeleteControlDialog(this);'> Delete </a></div>"
                //    strbuild += "<div style='float:right;'> <a onclick='EditControlDialog(this);'> Edit Control </a><br><a onclick='DeleteControlDialog(this);'> Delete </a></div>";
                //}
                //strbuild += panelid.toString() + "_" + i.toString() + "_" + j.toString() + " </td>";
                strbuild += " </td>";
            }
            strbuild += "</tr>";
        }
        strbuild += "</table>";
    }
    strbuild += "</fieldset> <br/> </div>";
    
    return strbuild;

}

function PanelHeaderHtml(panelname, panelid, row, columns, backcolor) {
    
    var strbuild = "<div class='pnl100' style='background-color:" + backcolor + ";' id='" + panelid.toString() + "'><fieldset><legend>" + panelname + " </legend>";
    strbuild += "<div style='float:right;'> <a onclick='bClick(this);' > <i class='fa fa-qrcode' title='Set Panel Background Colour'></i>  </a></div>";
    if (row > 0) {
        strbuild += "<table style='width:100%'>";

        for (i = 1; i <= row; i++) {
            strbuild += "<tr>";
            strbuild += "<td style='vertical-align:top' class='td_cls' id='" + panelid.toString() + "_1_1" + "'><div id='imgCustomer' class='imgCustomer dimg'></div>";
            GetControlImageData_customer(panelid.toString() + "_1_1");
            //strbuild += GetImgaeControl(panelid,row,1,"<div class='imgCustomer'></div>");
            strbuild += "<input id='fileCustomerLogo' type='file' /> <br> <input type='button' id='btnCustomerLogo' onclick='uploadCustomerlogo(this);' value='Add Logo' /></td>";
            //strbuild += "<td class='td_cls' id='" + panelid.toString() + "_1_2" + "'> <div id='imgOurlogo' class='imgOurlogo'></div>";
            strbuild += "<td class='td_cls' id='" + panelid.toString() + "_1_2" + "'> <div id='imgOurlogo' class=''></div>";
            GetControlImageData_our(panelid.toString() + "_1_2");
            //strbuild += GetImgaeControl(panelid,row,2,"<div class='imgOurlogo'></div>");
           // strbuild += "<input id='fileOurLogo' type='file' /> <br> <input type='button' id='btnOurLogo' onclick='uploadOurlogo(this);' value='Add Our Logo' />";
            strbuild += "</td></tr>";
        }
        strbuild += "</table>";
    }
    strbuild += "</fieldset> <br/> </div>";

    return strbuild;

}
function PanelSignatureHtml(panelname, panelid, row, columns, backcolor) {
    
    var strbuild = "<div class='pnl250' style='background-color:" + backcolor + ";' id='" + panelid.toString() + "'><fieldset><legend>" + panelname + " </legend>";
    strbuild += "<div style='float:right;'> <a onclick='pcolorClick(this);' > <i class='fa fa-qrcode' title='Set Panel Background Colour'></i>  </a>&nbsp;&nbsp;<a onclick='pdelClick(this);'><i class='fa fa-trash' title='Delete Control'></i></a></div>";
    if (row > 0) {
        strbuild += "<table style='width:100%'>";

        for (i = 1; i <= row; i++) {
            strbuild += "<tr>";
            for (j = 1; j <= columns; j++) {
             
                    strbuild += "<td class='td_cls' id='" + panelid.toString() + "_" + i.toString() + "_" + j.toString() + "'> ";
                    GetFormControl(panelid.toString() + "_" + i.toString() + "_" + j.toString());
                    strbuild += " </td>";
                
            }
            strbuild += "</tr>";
        }
        strbuild += "</table>";
    }
    strbuild += "</fieldset> <br/> </div>";
    //alert(strbuild);
    return strbuild;

}

      
function bClick(e)
{
    var el = $(e).closest('.pnl100');
    ShowColorDialog(el);
}
function pcolorClick(e) {
    var el = $(e).closest('.pnl250');
    ShowColorDialog(el);
}
function Editpanelname(e) {
    var el = $(e).closest('.pnl250');
    ShowEditDialog(el);
}
function ChangePanelPosition(e)
{
    var el = $(e).closest('.pnl250');
    showpanelposition(el);
}
function showpanelposition(element)
{
    var formid1 = getQuerystring('fid');
    var el = $(element).closest('.pnl250');
    var pnlid = $(el).attr('id');
    BindPanelName(pnlid);
    $('#Panelposition').dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Submit": function () {
                var pnlPosition = $('#ddlpostion').val();
                SavepanelPostion(element, pnlid, pnlPosition, formid1);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}
function ShowEditDialog(element) {
    var el = $(element).closest('.pnl250');
    var pnlid = $(el).attr('id');
    BindPanelName(pnlid);
    $('#pnlEdit').dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Submit": function () {
                var pname = $('#txtEditPanelName').val();
                EditPanelname(element,pnlid, pname);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}
function ShowColorDialog(element) {
    $('#pnlColor').dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Submit": function () {
                $('div#cblist input[type=checkbox]').each(function () {
                    if ($(this).is(":checked")) {
                        //$(element).css('background-color', $(this).val());
                        updatepanelcolor(element, $(element).attr("id"), $(this).val());
                    }
                });
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddControlClick(e)
{
    //clear selected data
    var el = $(e).closest('.td_cls');
    AddControlDialog(el);
}
function AddControlDialog(element) {
    $('#pnlControl').attr('title', 'Add Control');
    ClearControlData();
    $('#pnlControl').dialog({
        resizable: false,
        height: 500,
        width: 320,
        modal: true,
        buttons: {
            "Submit": function () {
                var el = $(element).closest('.td_cls'); 
                AddControl(el);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function EditControlDialog(element) {
   
    var el = $(element).closest('.td_cls');
    //alert($(el).attr('id'));
    var pnlid = $(el).attr('id');
    var pids = pnlid.split("_");
    var panelid = pids[0];
    var row = pids[1];
    var column = pids[2];
   
    //var controlData = GetControlObject(panelid, row, column);
    //setControlData(controlData);
    SetFormControlValues(pnlid);
    
    $('#pnlControl').attr('title', 'Edit Control');
    $('#pnlControl').dialog({
        resizable: false,
        height: 500,
        width: 320,
        modal: true,
        buttons: {
            "Submit": function () {
                UpdateControl(pnlid);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function setControlData(obj)
{
    $('#ddlfieldtype').val(obj.TypeOfField);
    $('#txtlabel').val(obj.ControlLabelName);
    $('#txtdefault').val(obj.DefaultText);
    $('#txtmin').val(obj.MinValue);
    $('#txtmax').val(obj.MaxValue);
    $('#txtlist').val(obj.ListValues);
    
    if (obj.Mandatory == "True") {
        $('#chkmandate:checked').val();
    }
}
function ClearControlDataInddlChange() {
    $("#txtclist").val('');
    $('#txtlabel').val('');
    $('#txtdefault').val('');
    $('#txtmin').val('');
    $('#txtmax').val('');
    $('#txtlist').val('');
    $('#chkmandate').attr('checked', false);
    $('#ddlposition').val('left');
    $('#txtwidth').val('95');
    $('#txthelptext').val('');
    $("#ddltypeOfData option:contains(Please select...)").attr('selected', 'selected');
}
function ClearControlData() {
    $("#txtclist").val('');
    $('#ddlfieldtype').val('Textbox');
    $('#txtlabel').val('');
    $('#txtdefault').val('');
    $('#txtmin').val('');
    $('#txtmax').val('');
    $('#txtlist').val('');
    $('#chkmandate').attr('checked', false);
    $('#ddlposition').val('left');
    $('#txtwidth').val('95');
    $('#txthelptext').val('');
    $("#ddltypeOfData option:contains(Please select...)").attr('selected', 'selected');

}

function ShowFromDialog() {
    $('#pnlFormadd').dialog({
        resizable: false,
        height: 250,
        modal: true,
        buttons: {
            "Submit": function () {
                
                debugger;
                manageFormData();
                $(this).dialog("close");
            }
        }
    });
}

function getQuerystring(key, default_) {

    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href.toLowerCase());
    if (qs == null) {
        return default_;
    }
    else {
        return qs[1];
    }
}

function setMsg(msg)
{
    $('#lblerror').html(msg);
}

function setSuccessMsg(msg) {
    //$('#lblerror').css('color','green');
    $('#lblerror').removeClass("alert alert-danger").addClass("alert alert-success");
    $('#lblerror').html(msg);
}

function setErrorMsg(msg) {
    //$('#lblerror').css('color', 'red');
    $('#lblerror').removeClass("alert alert-success").addClass("alert alert-danger");
    $('#lblerror').html(msg);
}

function clearMsg() {
    $('#lblerror').removeClass("alert alert-danger");
    $('#lblerror').removeClass("alert alert-success");
    $('#lblerror').html('');
}

function getSelectedColor()
{
    var retval = ''
    $('div#cblist input[type=checkbox]').each(function () {
        if ($(this).is(":checked")) {
            retval =  $(this).val();
        }
    });
    return retval;
}

function manageFormData()
{
    clearMsg();
    var formid = 0;
    var formname = $('#txtAddForm').val();
   
    var customerid = $('#hcustomerid').val(); 
    $.ajax({
        url: "../HC/HCWebService.asmx/ManageFormdata",
        type: "Post",
        data: "{'formid': '" + formid + "','formname': " + '"' + formname + '"' + ",'customerid': '" + customerid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async:true,
        success: function (data) {
            debugger;
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('Form name already exists.');
                }
                else {
                    $('#hformid').val(obj.FormID);
                    $('#txtFormName').val(obj.FormName);
                    setSuccessMsg('Added successfully.');
                    window.location.href = "../HC/FormDesign.aspx?fid=" + obj.FormID;
                }
            }
            else {
                setErrorMsg('Form name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}

function UpdateFormName()
{
    clearMsg();
    var formid = $('#hformid').val();
    var formname = $('#txtFormName').val();
   
    $.ajax({
        url: "../HC/HCWebService.asmx/UpdateFormName",
        type: "POST",
        data: "{'formid': '" + formid + "','formname': " + '"' + formname + '"' + "}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('Form name already exists.');
                }
                else {
                    $('#txtFormName').val(obj.FormName);
                    setSuccessMsg('Updated successfully.');
                }
            }
            else {
                setErrorMsg('Form name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });

}

function UpdateFormBackcolor(element) {
    clearMsg();
    var formid = $('#hformid').val();
    var backcolor = getSelectedColor();
    $.ajax({
        url: "../HC/HCWebService.asmx/UpdateBackcolor",
        type: "POST",
        data: "{'formid': '" + formid + "','backcolor': '" + backcolor + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    // setErrorMsg('Form name already exists.');
                }
                else {
                    $(element).css('background-color', obj.FormBackColor);
                    setSuccessMsg('Updated successfully.');
                }
            }
        },
        error: function (msg) { setMsg(Error); }
    });

}

function GetFormData(formid) {
    clearMsg();
    $.ajax({
        url: "../HC/HCWebService.asmx/GetFormdata?fid="+formid,
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('No data available');
                }
                else {
                    $('#hformid').val(obj.FormID);
                    $('#txtFormName').val(obj.FormName);
                    $(mform).css('background-color', obj.FormBackColor);
                }
            }
                   
        },
        error: function (msg) { setMsg(Error); }
    });
}

function createpanel(formid, panelname, rows, columns)
{
    var strbuild = '';
    clearMsg();
    $.ajax({
        url: "../HC/HCWebService.asmx/CreatePanel",
        type: "Post",
        data: "{'formid': '" + formid + "','panelname': " + '"' + panelname + '"' + ",'rows':'" + rows + "','columns':'" + columns + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
               
                if (obj.PanelID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                    // $('#txtFormName').val(obj.FormName);
                   
                    strbuild = PanelHtml(obj.PanelName, obj.PanelID, obj.PanelRows, obj.PanelColumns, obj.PanelBackColor);
                    $('#pnlcontent').append(strbuild);
                    $('#txtPanelName').val('');
                    setSuccessMsg('Added successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
    
}
function createSignaturepanel(formid, panelname, rows, columns) {
    var strbuild = '';
    clearMsg();
    $.ajax({
        url: "../HC/HCWebService.asmx/CreatePanel",
        type: "Post",
        data: "{'formid': '" + formid + "','panelname': " + '"' + panelname + '"' + ",'rows':'" + rows + "','columns':'" + columns + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);

                if (obj.PanelID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                    
                    strbuild = PanelSignatureHtml(obj.PanelName, obj.PanelID, obj.PanelRows, obj.PanelColumns, obj.PanelBackColor);
                    $('#pnlsignoff').append(strbuild);
                    setSuccessMsg('Added successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}

function createheaderpanel(formid, panelname, rows, columns) {
    var strbuild = '';
    clearMsg();
    $.ajax({
        url: "../HC/HCWebService.asmx/CreatePanel",
        type: "Post",
        data: "{'formid': '" + formid + "','panelname': " + '"' + panelname + '"' + ",'rows':'" + rows + "','columns':'" + columns + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);

                if (obj.PanelID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                    strbuild = PanelHeaderHtml(obj.PanelName, obj.PanelID, obj.PanelRows, obj.PanelColumns, obj.PanelBackColor);
                    $('#pnlheader').append(strbuild);
                    setSuccessMsg('Added successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}
function SavepanelPostion(element, panelid, pnlPosition, formid1)
{
 
    $.ajax({
        url: "../HC/HCWebService.asmx/SavePnlPosition",
        type: "Post",
        data: "{'panelid': '" + panelid + "','pnlposition': '"+ pnlPosition + "','formid1':'" + formid1 + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                    location.reload();
                    //$(element.find('legend')).html(obj.PanelName);
                    //setSuccessMsg('Updated successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}
function EditPanelname(element, panelid, pname) {
    $.ajax({
        url: "../HC/HCWebService.asmx/UpdatePanelName",
        type: "Post",
        data: "{'panelid': '" + panelid + "','pname': " + '"' + pname + '"' + "}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                  
                    $(element.find('legend')).html(obj.PanelName);
                    setSuccessMsg('Updated successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}

function updatepanelcolor(element,panelid, panelbackcolor) {
    clearMsg();
    //var panelbackcolor = getSelectedColor();
    $.ajax({
        url: "../HC/HCWebService.asmx/UpdatePanelColor",
        type: "Post",
        data: "{'panelid': '" + panelid + "','panelbackcolor': '" + panelbackcolor + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
                if (obj.FormID == "0") {
                    setErrorMsg('Panel name already exists.');
                }
                else {
                   
                    // $('#txtFormName').val(obj.FormName);
                    $(element).css('background-color', obj.PanelBackColor);
                    setSuccessMsg('Updated successfully.');
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });
}
 
function getPanlelist(formid)
{
    var strbuild = '';
    $.ajax({
        url: "../HC/HCWebService.asmx/GetPaneldata?fid=" + formid,
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var obj = jQuery.parseJSON(data.d);
               
                for (var p in obj)
                {
                    strbuild = '';
                    if (obj[p].PanelName == 'Header') {
                        strbuild = PanelHeaderHtml(obj[p].PanelName, obj[p].PanelID, obj[p].PanelRows, obj[p].PanelColumns, obj[p].PanelBackColor);
                        $('#pnlheader').append(strbuild);
                    }
                    else if (obj[p].PanelName == 'Signature Panel')
                    {
                        strbuild = PanelSignatureHtml(obj[p].PanelName, obj[p].PanelID, obj[p].PanelRows, obj[p].PanelColumns, obj[p].PanelBackColor);
                        $('#pnlsignoff').append(strbuild);
                    }
                    else {
                        strbuild = PanelHtml(obj[p].PanelName, obj[p].PanelID, obj[p].PanelRows, obj[p].PanelColumns, obj[p].PanelBackColor);
                        $('#pnlcontent').append(strbuild);
                    }
                }
            }
            else {
                setErrorMsg('Panel name already exists.');
            }
        },
        error: function (msg) { setMsg(Error); }
    });

}

function AddControl(element) {
    $('#pnlControl').attr('title', 'Add Control');
    clearMsg();
    var panelid = $(element).attr('id');
    var typeoffield = $('#ddlfieldtype').val();
    var controllablename = $('#txtlabel').val();
    var controlvalue = '';
    var defaulttext = $('#txtdefault').val();

    if (typeoffield == "Dropdown") {
        defaulttext = $('#ddltypeOfData').find('option:selected').text();
    }
    var minvalue = $('#txtmin').val();
    var maxvalue = $('#txtmax').val();
    var listvalue = $('#txtlist').val();
    var ColumnsList = $('#txtclist').val();
    var mandatory = $('#chkmandate:checked').val();
    var retd = 'False';
    var RbList = $("#rblist").find("input:checked").val();
    var RbCntlList = $("#rbcontrolsList").find("input:checked").val();
    var ImaageFile = $("#ImageUploadcntl").val();


    var helptext = $("#txthelptext").val();

    if (mandatory == 'on') {
        retd = 'true';
    }
    var controlwidth = $('#txtwidth').val();
    var controlHeight = $('#txtHeight').val();
    var controlposition = $('#ddlposition').val();

    if (typeoffield == "Label") {
        minvalue = $('#ddlBold').val();
        maxvalue = $('#txtSize').val();
    }
    debugger;
    if (typeoffield != "Image") {
        $.ajax({
            url: "../HC/HCWebService.asmx/AddPanelControl",
            type: "POST",
            data: "{'panelid': '" + panelid + "','typeoffield': '" + typeoffield + "','controllablename': " + '"' + controllablename + '"' + ",'defaulttext':" + '"' + defaulttext + '"' + ",'minvalue':'" + minvalue + "','maxvalue':'" + maxvalue + "','listvalue':" + '"' + listvalue + '"' + ",'mandatory':'" + retd + "','controlwidth':'" + controlwidth + "','controlposition':'" + controlposition + "','controlHeight':'" + controlHeight + "','RbList':'" + RbList + "','RbCntlList':'" + RbCntlList + "','ColumnsList':'" + ColumnsList + "','helptext':'" + helptext + "'}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.d != '') {
                    var obj = jQuery.parseJSON(data.d);
                    GetFormControl(panelid);
                    ClearControlData();
                    setSuccessMsg('Added successfully.');
                }
            },
            error: function (msg) { setMsg(Error); }
        });
    }
    else {
        var fileUpload = $('#ImageUploadcntl').get(0);
        var files = fileUpload.files;
        var fdata = new FormData();
        for (var i = 0; i < files.length; i++) {
            fdata.append(files[i].name, files[i]);
        }
        var options = {};
        options.url = "../HC/HCWebService.asmx/UploadImage?panelid=" + panelid + "&&controlposition=" + controlposition + "&&controlwidth=" + controlwidth + "&&typeoffield=" + typeoffield + "&&helptext=" + helptext;
        options.type = "POST";
        options.data = fdata;
        options.contentType = false;
        options.processData = false;
        options.async = true,
        options.success = function (result) {
            var obj = result;
            if (result != '') {
                setSuccessMsg("Uploaded successfully.");
                GetFormControl(panelid);
                ClearControlData();
            }
        };
        options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };

        $.ajax(options);
        return false;
    }
}
function htmlEncode(controllablename) {
    return $('<div/>').text(controllablename).html();
}
function htmlDecode(controllablename) {
    return $('<div/>').html(controllablename).text();
}

function UpdateControl(panelid) {
    clearMsg();
    //var panelid = $(element).attr('id');
    var typeoffield = $('#ddlfieldtype').val();
    var controllablename = $('#txtlabel').val();
    var controlvalue = '';
    var defaulttext = $('#txtdefault').val();

    if (typeoffield == "Dropdown") {
        defaulttext = $('#ddltypeOfData').find('option:selected').text();
    }

    var minvalue = $('#txtmin').val();
    var maxvalue = $('#txtmax').val();
    var listvalue = $('#txtlist').val();
    var ColumnsList = $('#txtclist').val();
    var mandatory = $('#chkmandate:checked').val();
    var retd = 'False';
    var RbList = $("#rblist").find("input:checked").val();
    var RbCntlList = $("#rbcontrolsList").find("input:checked").val();
    var NoOfRows = $('#txtrowNo').val();
    var NoOfColumns = $('#txtcolno').val();

    var helptext = $("#txthelptext").val();


    if (mandatory == 'on') {
        retd = 'true';
    }
    var controlwidth = $('#txtwidth').val();
    var controlHeight = $('#txtHeight').val();
    var controlposition = $('#ddlposition').val();
    debugger;
    if (typeoffield == "Label") {
        minvalue = $('#ddlBold').val();
        maxvalue = $('#txtSize').val();
    }

    if (typeoffield != "Image") {
        $.ajax({
            url: "../HC/HCWebService.asmx/UpdateControl",
            type: "POST",
            data: "{'panelid': '" + panelid + "','typeoffield': '" + typeoffield + "','controllablename': " + '"' + controllablename + '"' + ",'defaulttext':" + '"' + defaulttext + '"' + ",'minvalue':'" + minvalue + "','maxvalue':'" + maxvalue + "','listvalue':" + '"' + listvalue + '"' + ",'mandatory':'" + retd + "','controlwidth':'" + controlwidth + "','controlposition':'" + controlposition + "','controlHeight':'" + controlHeight + "','RbList':'" + RbList + "','RbCntlList':'" + RbCntlList + "','ColumnsList':'" + ColumnsList + "','helptext':'" + helptext + "'}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.d != '') {
                    var obj = jQuery.parseJSON(data.d);
                    GetFormControl(panelid);
                    setSuccessMsg('Updated successfully.');
                    //clear the data
                    ClearControlData();
                }
            },
            error: function (msg) { setMsg(Error); }
        });
    }
    else {
        var fileUpload = $('#ImageUploadcntl').get(0);
        var files = fileUpload.files;
        var fdata = new FormData();
        for (var i = 0; i < files.length; i++) {
            fdata.append(files[i].name, files[i]);
        }
        var options = {};
        options.url = "../HC/HCWebService.asmx/UploadImage?panelid=" + panelid + "&&controlposition=" + controlposition + "&&controlwidth=" + controlwidth + "&&typeoffield=" + typeoffield + "&&helptext=" + helptext;
        options.type = "POST";
        options.data = fdata;
        options.contentType = false;
        options.processData = false;
        options.async = true,
        options.success = function (result) {
            var obj = result;
            if (result != '') {
                setSuccessMsg("Uploaded successfully.");
                GetFormControl(panelid);
                ClearControlData();
            }
        };
        options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };

        $.ajax(options);
        return false;
    }
}

function pdelClick(e)
{
    var checkstr = confirm('Do you want to delete panel?');
    if (checkstr == true) {
        
    var el = $(e).closest('.pnl250');
    var pnlid = $(el).attr('id');
    
    $.ajax({
        url: "../HC/HCWebService.asmx/DeletePanel",
        type: "POST",
        data: "{'panelid': '" + pnlid + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data != '') {
                
                $('#' + pnlid ).remove();
                var obj = jQuery.parseJSON(data);
                
                setSuccessMsg('Deleted successfully.');
            }

        },
        error: function (msg) { setMsg(Error); }
    });
    } else {
        return false;
    }
}

function DeleteControl(element)
{
    var checkstr = confirm('Do you want to delete control?');
    if (checkstr == true) {
        
        var el = $(element).closest('.td_cls');
        var pnlid = $(el).attr('id');


        $.ajax({
            url: "../HC/HCWebService.asmx/DeleteControl",
            type: "POST",
            data: "{'panelid': '" + pnlid + "'}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {

                if (data != '') {
                    GetFormControl(pnlid);
                    // var obj = jQuery.parseJSON(data);
                    setSuccessMsg('Deleted successfully.');
                }

            },
            error: function (msg) { setMsg(Error); }
        });
    } else {
        return false;
    }
}
$(document).ready(function () {
    $("#btn").click(function () {

        var listbox = document.getElementById("gridlist");
        var selIndex = listbox.selectedIndex;
        if (-1 == selIndex) { alert("Please select an option to move."); return; }
        var increment = -1;
        if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }

        var selValue = listbox.options[selIndex].value;
        var selText = listbox.options[selIndex].text;
        listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
        listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
        listbox.options[selIndex + increment].value = selValue;
        listbox.options[selIndex + increment].text = selText;
        listbox.selectedIndex = selIndex + increment;
    });
    $("#Btn2").click(function () {

        var listbox = document.getElementById("gridlist");
        var selIndex = listbox.selectedIndex;
        if (-1 == selIndex) { alert("Please select an option to move."); return; }
        var increment = 1;
        if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }
        var selValue = listbox.options[selIndex].value;
        var selText = listbox.options[selIndex].text;
        listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
        listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
        listbox.options[selIndex + increment].value = selValue;
        listbox.options[selIndex + increment].text = selText;
        listbox.selectedIndex = selIndex + increment;
    });
    $("#save").click(function () {

        var listbox = document.getElementById("gridlist");

        var index1 = '';


        for (var i = 0; i < listbox.length; i++) {

            var selValue = listbox.options[i].value;

            index1 = index1 + selValue + ",";
        }
        url = '../HC/HCWebService.asmx/InsertPanelPositions';
        data = "{value:'" + index1 + "'}";
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: OnCheckUserNameAvailabilitySuccess,
            error: OnCheckUserNameAvailabilityError
        });
        function OnCheckUserNameAvailabilitySuccess(response) {

            $("#lblMessage").text("Configuration saved successfully");
        }
        function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
            alert(xhr.statusText);
        }
    });
});



