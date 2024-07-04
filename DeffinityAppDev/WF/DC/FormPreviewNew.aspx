<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPreviewNew.aspx.cs" Inherits="DeffinityAppDev.WF.DC.FormPreviewNew" %>

<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title> Message </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
</head>
<body class="page-body login-page login-light">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
<form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> </asp:ScriptManager>


    <div class="card shadow-sm">
						<div class="card-header">
							Form
						</div>
        <div class="panel-body">
          <div>

              <style>
        .delli{
            padding-left:10px;
        }
        .btn {
    outline: none;
    border: 1px solid transparent;
     margin-bottom: 0px; 
}
.modal {
    background: rgba(0,0,0,0.5);
}
table.hcolor>thead>tr>th{
    color:white;
}
/*.pnllist{
    border-style: solid;border-color: silver;margin: 15px;padding: 15px;
}
.pnllist label{
    font-size:18px;
}*/
    </style>
     <div class="form-group row">
         <div id="loading">
        <asp:Label SkinID="Loading" ID="lblloading" runat="server" ClientIDMode="Static"></asp:Label>
             <input type="hidden" class="hpnl" value="" />
             </div>

         <script>



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

             $().ready(function () {

                 var jobid = 0;////getQuerystring('callid');
                 if (jobid != '')
                     GetFormData(jobid);
                 $('#btnSave').click(function () {

                     var retval = saveData();


                     //update data on server
                     return false;
                 });
                 //$("[id*=ddlService]").change(function () {
                 //    var serviceid = $("[id*=ddlService]").val();
                 //    serviceid, jobid
                 //    GetFormData(serviceid,);
                 //});
             }

             );

             function saveData() {
                 var d = [];
                 var fileList = [];
                 var sret = "";
                 $("input[id^='ctl']").each(function (index) {
                     sret = "";
                     var c = $(this).attr('id');
                     var s = c.split('_');
                     if (s[1] == 'chk') {
                         if ($(this).is(":checked")) {
                             //console.log("Checkbox is checked.");
                             var chid = $(this).attr('id');
                             sret = sret + $(this).attr('id') + ":" + "true" + ":" + $('#c' + chid + '_chktxt').val();
                         }
                         else if ($(this).is(":not(:checked)")) {
                             sret = sret + $(this).attr('id') + ":" + "false" + ":" + "";
                             //console.log("Checkbox is unchecked.");
                         }
                         d.push(sret);
                         console.log(sret);
                         sret = "";
                     }
                     else if (s[1] == 'txt') {
                         sret = sret + $(this).attr('id') + ":" + $(this).val();
                         d.push(sret);
                         console.log(sret);
                         sret = "";
                     }
                     else if (s[1] == 'file') {
                         //sret = sret + $(this).attr('id') + ":" + $(this).val();
                         file.push($(this)[0]);
                         console.log(sret);
                         sret = "";
                     }
                     else {

                     }

                 });
                 $("textarea[id^='ctl']").each(function (index) {
                     sret = "";
                     var c = $(this).attr('id');
                     var s = c.split('_');
                     sret = sret + $(this).attr('id') + ":" + $(this).val();
                     d.push(sret);
                     //console.log(sret);
                     sret = "";
                 });
                 $("select[id^='ctl']").each(function (index) {
                     sret = "";
                     var c = $(this).attr('id');
                     var s = c.split('_');
                     sret = sret + $(this).attr('id') + ":" + $(this).val();
                     d.push(sret);
                     //console.log(sret);
                     sret = "";
                 });
                 // return d;

                 var dataObject = JSON.stringify({
                     'd': d,
                     // 'files':fileList,
                     'jobid': getQuerystring('callid'),
                     'formid': $("[id*=ddlForm]").val()
                 });
                 //debugger;
                 $.ajax({
                     type: "POST",
                     url: "../../WF/DC/Services/AdminServices.asmx/JobUpdateData",
                     data: dataObject,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     async: false,
                     success: function (r) {
                         var t = r.d;

                         //SetServiceContactData();
                         //alert(t.Value);
                         $("[id*=hService]").val(t.Value);
                         //Bind dropdown

                         //setServiceDropdownValue();
                         //set added value
                         // alert($("[id*=hSubCategory]").val());
                         // setSubCategoryDropdownValue();

                         displayMsg('lblmsg', 'Updated successfully', 'success');
                     }
                 });


             }


             function buildPanel() {
                 $('[id*=pnllist]').empty();
                 //alert('f');
                 var b = "";
                 //$("[id*=ddlService]")
                 $('[id*=ddlService] > option').each(function () {
                     if ($(this).val() != "0") {
                         //alert($(this).text() + ' ' + $(this).val());
                         var pnlid = $(this).val();
                         b = b + "<div  >"
                         b = b + "<div class='form-group'>";
                         b = b + "<div class='col-md-12' style='border-bottom: 2px solid #f5f5f5;'>";
                       //  b = b + " <div class='col-sm-6 '></div>";
                         b = b + "<label class='col-sm-6 control-label lblpnl' style='width:100%'> <b>" + $(this).text(); +"</b> </label> ";
                        

                         b = b + " </div> ";
                         b = b + " </div> ";
                       //  b = b + " <table id='tbl_" + pnlid + "' class='table table-striped table-bordered hcolor tblforms tbl_" + pnlid + "'></table>";
                        // b = b + " <div id='pnl_" + pnlid + "'> </div>";
                       
                         b = b + GetFormDataNew(pnlid);

                         var rs = GetFormDataNew(pnlid);

                         debugger;

                         b = b + "</div>";
                       
                        // b = "";

                        // GetFormDataNew(pnlid);
                     }
                 });
                 $('[id*=pnllist]').css({ "border-style": "solid", "border-color": "silver", "margin": "15px", "padding":"15px" });
                 $('[id*=pnllist]').append(b);
                // return b;
             }


             function GetFormDataNew(id) {

                 var y = "";
                 try {

                     var jobid = 0;// getQuerystring('callid');

                     var dataObject = JSON.stringify({
                         'id': id,
                         'jobid': jobid,


                     });

                     $.ajax({
                         url: "../../WF/DC/Services/AdminServices.asmx/BindFromData",
                         type: "POST",
                         data: dataObject,
                         contentType: 'application/json; charset=utf-8',
                         dataType: "json",
                         async: false,
                         success: function (data) {


                             var tbl_id = 'tbl_' + id;
                             var pnl_id = 'pnl_' + id;

                             //console.log('table id:' + tbl_id);
                           //  $("#" + tbl_id).empty();
                             //$("#" + pnl_id).empty();
                             var NewData = jQuery.parseJSON(data.d);
                             //var x = "<thead><tr>"
                             //    + "<th style='width:3%;'></th>"
                             //    + "<th style='width:3%;'></th>"
                             //    + "<th  style='width:5%;'>ID</th>"
                             //    + "<th  style='width:5%;'>Position</th>"
                             //    + "<th  style='width:30%;'>Label</th>"
                             //    + "<th  style='width:10%;'>Control</th>"
                             //    + "<th style='width:50%;'>Values</th>"
                             //    + "<th style='width:3%;'></th> "
                             //    + "</tr></thead>";

                             //x = x + "<tbody>"
                            // debugger;
                             console.log('len:' + NewData.length);
                             for (var i = 0; i < NewData.length; i++) {
                                 //var CCID = NewData[i].CCID
                                 var ID = NewData[i].ID
                                 var TypeOfField = NewData[i].TypeOfField;
                                 var LabelName = NewData[i].LabelName;
                                 var ListValue = NewData[i].ListValue
                                 var Position = NewData[i].Position;
                                 var ListValueValues = NewData[i].ListValueValues;
                                 var ListNotesValues = NewData[i].ListNotesValues;
                                 // alert(id);
                                 //x = x
                                 //    + "<tr><td>" + ButtonMoveHtml(ID, LabelName, TypeOfField, ListValue)
                                 //    + "<td>" + ButtonHtml(ID,id)
                                 //    + "</td><td>" + ID
                                 //    + "</td><td>" + Position
                                 //    + "</td><td>" + LabelName
                                 //    + "</td><td>" + TypeOfField
                                 //    + "</td><td>" + DisplayValues(ListValue, TypeOfField)
                                 //    + "</td><td>" + ButtondeleteHtml(ID)
                                 //    + "</td></tr>";


                                 if (TypeOfField == "Subheading") {
                                     y = y + "<div class='form-group'><div class='col-md-12 text-bold'> <strong>" + LabelName + "</strong><hr class='no-top-margin' /></div></div>";
                                 }
                                 else {
                                     y = y + "<div class='form-group'><div class='col-md-12'>";

                                     // y = y + "<label class='col-sm-12 control-label' style='text-align:right;'>" + LabelName + "</label>";
                                     y = y + "<label class='col-sm-12 control-label''>" + (i + 1) + ". " + LabelName + "</label>";
                                    // debugger;
                                     if (ListValueValues == null)
                                         ListValueValues = '';

                                     if (ListNotesValues == null)
                                         ListNotesValues = '';

                                     y = y + "<div class='col-sm-12' style='padding-top: 10px;'>" + DisplayValues(ListValue, TypeOfField, ID, ListValueValues, ListNotesValues) + "</div>"
                                     y = y + "</div>";
                                     y = y + "</div>";
                                 }
                             }

                             //x = x + "</tbody>";
                             //  $("#" + tbl_id).empty();
                             // $("#" + tbl_id).append(x);
                             // BindTable(tbl_id);
                            // $("#" + tbl_id).removeClass("no-footer");


                             // alert(TypeOfField);


                            // $("#" + pnl_id).empty();
                            // $("#" + pnl_id).append(y);
                           
                            // console.log(y);
                             //setStatusBackColor();
                         }
                     });
                 }
                 catch (e) {
                     var err = e;
                 }

                 return y;
             }

         </script>

<script>

    $(document).ready(function () {
        // invoked when sending ajax request
        $(document).ajaxSend(function () {
            $("#loading").show();
        });

        // invoked when sending ajax completed
        $(document).ajaxComplete(function () {
            $("#loading").hide();
        });

    });

    </script>
      <div class="col-md-10">
          <label id="lblmsg" style="width:100%"></label>
          </div>
         </div>
      <div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Sector</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlSector" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hSector" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddSector"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditSector" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelSector" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the sector?');"></asp:LinkButton>
              <asp:Button ID="btnCopy" runat="server" SkinID="btnDefault" Text="Copy" ClientIDMode="Static"></asp:Button>  
          </div>
          </div>
        </div>

     <div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Problem</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlCategory" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hCategory" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the category?');"></asp:LinkButton>
              </div>
          </div>
        </div>
      <div class="form-group row" >
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Form Name</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlForm" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hSubCategory" runat="server" ClientIDMode="Static" Value="0"  />
               <asp:HiddenField ID="hform" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddSubCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"  style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnEditSubCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"  style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnDelSubCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the form?');"  style="display:none;visibility:hidden;"></asp:LinkButton>
              </div>
          </div>
        </div>
     <div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Sub Section</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlService" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hService" runat="server" ClientIDMode="Static" Value="0"  />
               <asp:HiddenField ID="hset" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddService"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditService" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelService" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the sub section?');"></asp:LinkButton>
              </div>
          </div>
        </div>
     <div class="form-group row">
      <label class="col-sm-2 control-label"> </label>
          <div class="col-sm-10 form-inline">
          <asp:Button ID="btnAddform" runat="server" SkinID="btnDefault" Text="Add Controls" ClientIDMode="Static" style="display:none;" />
          </div>
         </div>

    <div class="form-group row">
         <div class="col-sm-12" style="border-style:dashed;">
             <div id="pnllist"></div>
             </div>
        </div>

    <div class="modal fade" id="modal-6" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector12">&times;</button>
					<h4 class="modal-title"><span id="modeltitle"> Add Sector </span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Name</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" SkinID="txt_70"></asp:TextBox>
              <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" />
              <asp:HiddenField ID="htype" runat="server" ClientIDMode="Static" />
              <label style="color:red" id="lblError"></label>

              </div>
          </div>
              </div>
					
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector" style="display: none;">Close</button>
					<button type="button" class="btn btn-info" id="btnSubmit">Submit</button>
				</div>
			</div>
		</div>
	</div>

    <div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector11">&times;</button>
					<h4 class="modal-title"><span id="modeltitle1"> Add Form Controls </span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label"> Name</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtlable" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
              <asp:HiddenField ID="hfdid" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="hfsubid" runat="server" ClientIDMode="Static" />
              <label style="color:red" id="lblError1"></label>

              </div>
          </div>
              </div>
                      <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label"> Control</label>
          <div class="col-sm-10">
            
			<asp:DropDownList ID="ddlControl" runat="server" SkinID="ddl_90" >
                                <asp:ListItem Value="Textbox" Text="Textbox"> </asp:ListItem>
                            <asp:ListItem Value="Textarea" Text="Textarea"> </asp:ListItem>
                                <asp:ListItem Value="List" Text="List"> </asp:ListItem>
                 <asp:ListItem Value="Checklistbox" Text="Checklistbox"> </asp:ListItem>
                 <asp:ListItem Value="Radiobuttonlist" Text="Radio Buttonlist"> </asp:ListItem>
                 <asp:ListItem Value="Date" Text="Date"> </asp:ListItem>
                 <%--<asp:ListItem Value="Subheading" Text="Sub Heading"> </asp:ListItem>--%>
             </asp:DropDownList>

              </div>
          </div>
              </div>
					  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label" id="lblvalues"> Values</label>
          <div class="col-sm-10 form-inline">

              <ul id="ul_items">

              </ul>
            
              <asp:TextBox ID="txtadd" runat="server" ClientIDMode="Static" SkinID="txt_90" MaxLength="300"></asp:TextBox>
			<asp:TextBox ID="txtvalues" runat="server" ClientIDMode="Static" SkinID="txt_90" MaxLength="300"></asp:TextBox> <asp:LinkButton ID="btnfadd" runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" />
              
              <label style="color:red" id="lblError3"></label>

              </div>
          </div>
              </div>
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector1" style="display:none;">Close</button>
					<button type="button" class="btn btn-info" id="btnSaveTask">Submit</button>
				</div>
			</div>
		</div>
	</div>
    <script type="text/javascript">
        var lid = -1;
        function getItems() {
            var listText = $('#ul_items li').map(function () {
                return $(this).text();
            }).get().join(',');
            return listText;
        }
        function setItems(s) {

            var array = s.split(',');

            $.each(array, function (index, value) {
                lid++;
                $("#ul_items").append("<li id='li" + lid + "'>" + value + " <span onclick=deleteli('li" + lid + "') class='fa-trash' style='font-size:1.0em'></span></li>");
            });


        }
        function bindGrid() {
            //var id = $("[id*=ddlService]").val();
            //GetFormData(id);
            var id = $("[id*=hfsubid").val();// $("[id*=ddlService]").val();
            GetFormDataNew(id);

        }
        function ConfirmDelete() {
            return confirm("Are you sure you want to delete?");
        }
        function getfID() {
            return $("[id*=hfdid]").val();
        }
        function setfID(value) {
            $("[id*=hfdid]").val(value);
        }
        function editFormdata(id, subid) {
            $("[id*=modeltitle1]").html("Edit Form Controls");
            clearFormData();
            setfID(id);
            $("[id*=hfsubid").val(subid);
          //  GetFormDatabyID(id);
            showAjaxModalform();
            return false;
        }

        function deleteFormdata(id) {
            var r = ConfirmDelete();
            if (r == true) {
                setfID(id);
                deleteFormDatabyMethod();
            }
        }

        function controldropdownshow(c) {
            if (c == 'Textbox' || c == 'Textarea') {
                $("[id*=ul_items]").hide();
                $("[id*=txtadd]").hide();
                $("[id*=txtvalues]").show();
                $("[id*=lblvalues]").show();
                $('#btnfadd').hide();
            }
            else if (c == 'Date' || c == 'Subheading') {
                $("[id*=ul_items]").hide();
                $("[id*=txtadd]").hide();
                $("[id*=txtvalues]").hide();
                $("[id*=lblvalues]").hide();
                $('#btnfadd').hide();
            }
            else {
                $("[id*=ul_items]").show();
                $("[id*=txtadd]").show()
                $("[id*=txtvalues]").hide();
                $("[id*=lblvalues]").show();
                $('#btnfadd').show();
            }
        }
        function item_remove(r) {
            // alert('test');
            $(r).remove();
            $(this).remove();

            $(this).parent('li').remove();
        }
        $(function () {
            $('#ul_items').on('click', 'li', function () {
                // alert('dynamicList');
                $(this).remove();
            });
        });
        function deleteli(lid) {
            //alert()
            document.getElementById(lid).remove();

        }
        $(document).ready(function () {
            controldropdownshow('Textbox');
            $("#ul_items span").on("click", function () {
                $(this).parent().remove();
            });
            $(".del_x").click(function () {
                //find parent li element and remove
                $(this).parent('li').remove();
            });
            $("[id*=ddlControl]").change(function () {
                var c = $(this).val();
                controldropdownshow(c)
            });


            $('#btnfadd').click(function () {
                if ($("[id*=txtadd]").val().trim() != '') {
                    lid++;
                    $("[id*=ul_items]").append("<li id='li" + lid + "'>" + $("[id*=txtadd]").val().trim() + "<span onclick=deleteli('li" + lid + "') class='delli fa-trash' style='font-size:1.0em'></span></li>");
                    $("[id*=txtadd]").val('');
                }

                return false;
            });

            $("#btnAddform").click(function () {
                $("[id*=modeltitle1]").html("Add Form Controls");
                setfID('0');
                clearFormData();
                $("[id*=hfsubid").val($("[id*=ddlService]").val());
                showAjaxModalform();
                return false;
            });

            $("[id*=btnSaveTask]").click(function () {

                var fid = getfID();
                if (fid != '0') {
                    AddFormData();
                    hideAjaxModalform();
                    displayMsg('lblmsg', 'Updated successfully', 'success');
                }
                else {

                    AddFormData();

                    hideAjaxModalform();
                    displayMsg('lblmsg', 'Added successfully', 'success');
                }

                return false;
            });
        });

        function showAddFormControls(id) {
            $("[id*=modeltitle1]").html("Add Form Controls");
            setfID('0');
            clearFormData();
            $("[id*=hfsubid").val(id);
            showAjaxModalform();
            return false;
        }

        function AddFormData() {
            var id = $("[id*=hfdid]").val();
            var subid = $("[id*=hfsubid]").val();  // $("[id*=ddlService]").val();
            var controltype = $("[id*=ddlControl]").val();
            var cname = $("[id*=txtlable]").val();
            var cvalues = '';
            if (controltype != 'Textbox' || controltype != 'Textarea')
                cvalues = getItems();
            else
                cvalues = $("[id*=txtvalues]").val();

            var dataObject = JSON.stringify({
                'id': id,
                'subid': subid,
                'controltype': controltype,
                'cname': cname,
                'cvalues': cvalues,

            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FromDataAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {




                    bindGrid();
                    clearFormData();

                }
            });
        }

        function deleteFormDatabyMethod() {
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/FromDataDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    bindGrid();
                    displayMsg('lblmsg', 'Deleted successfully', 'success');
                }
            });
        }

        function UpdateFormData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = getName();
            id = getID();
            var dataObject = JSON.stringify({
                'name': name,
                'id': id
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/CategoryUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    SetCategoryContactData();
                    $("[id*=hCategory]").val(t.Value);
                    //Bind dropdown
                    //set added value
                    setCategoryDropdownValue();
                }
            });
        }

        function clearFormData() {
            setfID('0');

            $("[id*=ddlControl]").val('Textbox');
            $("[id*=txtlable]").val('');
            $("[id*=txtvalues]").val('');
            $("[id*=ul_items]").empty();
            controldropdownshow('Textbox');
        }
    </script>
     <script type="text/javascript">



         function getID() {
             return $("[id*=hid]").val();
         }
         function setID(value) {
             $("[id*=hid]").val(value);
         }
         function getName() {
             return $("[id*=txtName]").val();
         }
         function setName(value) {
             $("[id*=txtName]").val(value);
         }
         function setPopDefault() {
             $("[id*=txtName]").val('');
             $("[id*=hid]").val('0');
         }
         function showAjaxModal() {
             $('#modal-6').modal('show', { backdrop: 'fade' });
         }
         function hideAjaxModal() {
             $('#modal-6').modal('hide');
         }
         function showAjaxModalform() {
             $('#modal-7').modal('show', { backdrop: 'fade' });
         }
         function hideAjaxModalform() {
             $('#modal-7').modal('hide');
         }
         function displayMsg(element, msg, msgtype) {
             if (msgtype == 'error') {
                 $('[id*=' + element + ']').html('<p class="bg-danger">' + msg + '</p>');
             }
             else if (msgtype == 'success') {
                 $('[id*=' + element + ']').html('<p class="bg-success">' + msg + '</p>');
             }
             else if (msgtype == 'clear') {
                 $('[id*=' + element + ']').html('');
             }
             else {
                 $('[id*=' + element + ']').html('');
             }

         }
         $(document).ready(function () {

             $("[id*=btnSubmit]").click(function () {
                 // alert($("[id*=htype").val());
                 if ($("[id*=htype").val() == '1') {
                     var id = getID();
                     if (id != '0') {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             UpdateSectorContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Updated successfully', 'success');
                         }
                         else {
                             displayMsg('lblerror', 'Please select sector', 'error');
                         }
                     }
                     else {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             AddSectorContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Added successfully', 'success');
                         }
                         else {
                             displayMsg('lblerror', 'Please select sector', 'error');
                         }
                     }

                 }
                 //category
                 else if ($("[id*=htype").val() == '2') {

                     var id = getID();
                     if (id != '0') {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             UpdateCategoryContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Updated successfully', 'success');
                         }
                         else {
                             displayMsg('lblerror', 'Please select category', 'error');
                         }
                     }
                     else {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             AddCategoryContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Added successfully', 'success');
                         }
                         else {
                             displayMsg('lblerror', 'Please select category', 'error');
                         }
                     }
                 }
                 //sub category
                 else if ($("[id*=htype").val() == '3') {

                     var id = getID();
                     if (id != '0') {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             UpdateSubCategoryContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Updated successfully', 'success');
                             //window.location.href = window.location.href;
                         }
                         else {
                             displayMsg('lblerror', 'Please select sub category', 'error');
                         }
                     }
                     else {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             AddSubCategoryContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Added successfully', 'success');
                             window.location.href = window.location.href;
                         }
                         else {
                             displayMsg('lblerror', 'Please select sub category', 'error');
                         }
                     }
                 }
                 else if ($("[id*=htype").val() == '4') {

                     var id = getID();
                     if (id != '0') {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             UpdateServiceContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Updated successfully', 'success');
                             window.location.href = window.location.href;
                         }
                         else {
                             displayMsg('lblerror', 'Please select service', 'error');
                         }
                     }
                     else {
                         //add save data
                         if ($("[id*=txtName").val().length > 0) {
                             AddServiceContactData();
                             hideAjaxModal();
                             displayMsg('lblmsg', 'Added successfully', 'success');
                             window.location.href = window.location.href;
                         }
                         else {
                             displayMsg('lblerror', 'Please select service', 'error');
                         }
                     }
                 }
                 return false;
             });
         });
     </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("[id*=hid]").val('0');
            $("[id*=htype]").val('1');

            //$("[id*=btnAddSector]").click(function () {
            //    $("[id*=modeltitle]").html("Add Sector");
            //    $("[id*=hid]").val('0');
            //    $("[id*=htype]").val('1');
            //    setPopDefault();
            //    showAjaxModal();
            //    return false;
            //});

            //$("[id*=btnEditSector]").click(function () {
            //    $("[id*=modeltitle]").html("Edit Sector");
            //    $("[id*=hid]").val('0');
            //    $("[id*=htype]").val('1');
            //    if ($("[id*=htype").val() == '1') {
            //        $("[id*=hid").val($("[id*=ddlSector]").val());
            //        if ($("[id*=hid").val() != "0") {
            //            $("[id*=txtName]").val($("#ddlSector option:selected").text());
            //            showAjaxModal();
            //        }
            //        else {
            //            displayMsg('lblmsg', 'Please select sector', 'error');
            //        }
            //    }
            //    return false;
            //});

            //$("[id*=btnDelSector]").click(function () {
            //    $("[id*=hid]").val('0');
            //    $("[id*=htype]").val('1');
            //    if ($("[id*=htype").val() == '1') {
            //        $("[id*=hid").val($("[id*=ddlSector]").val());
            //        if ($("[id*=hid").val() != "0") {
            //            DeleteSectorContactData();
            //            displayMsg('lblmsg', 'Deleted successfully', 'success');
            //            setPopDefault();
            //        }
            //        else {
            //            displayMsg('lblmsg', 'Please select sector', 'error');
            //        }
            //    }
            //    return false;
            //});


        });


			</script>

    <script type="text/javascript">

        $(document).ready(function () {
            //$("[id*=hid]").val('0');
            //$("[id*=htype]").val('1');

            $("[id*=btnAddCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                $("[id*=modeltitle]").html("Add Category");
                setPopDefault();
                showAjaxModal();
                return false;
            });

            $("[id*=btnEditCategory]").click(function () {
                $("[id*=modeltitle]").html("Edit Category");
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                if ($("[id*=htype").val() == '2') {
                    $("[id*=hid").val($("[id*=ddlCategory]").val());
                    if ($("[id*=hid").val() != "0") {
                        $("[id*=txtName]").val($("#ddlCategory option:selected").text());
                        showAjaxModal();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select category', 'error');
                    }
                }
                return false;
            });

            $("[id*=btnDelCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                if ($("[id*=htype").val() == '2') {
                    $("[id*=hid").val($("[id*=ddlCategory]").val());
                    if ($("[id*=hid").val() != "0") {
                        DeleteCategoryContactData();
                        displayMsg('lblmsg', 'Deleted successfully', 'success');
                        setPopDefault();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select category', 'error');
                    }
                }
                return false;
            });


        });


			</script>

    <script type="text/javascript">

        $(document).ready(function () {
            //$("[id*=hid]").val('0');
            //$("[id*=htype]").val('1');

            $("[id*=btnAddSubCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('3');
                $("[id*=modeltitle]").html("Add Form");
                setPopDefault();
                showAjaxModal();
                return false;
            });

            $("[id*=btnEditSubCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('3');
                $("[id*=modeltitle]").html("Edit Form");
                if ($("[id*=htype").val() == '3') {
                    $("[id*=hid").val($("[id*=ddlForm]").val());
                    if ($("[id*=hid").val() != "0") {
                        $("[id*=txtName]").val($("#ddlForm option:selected").text());
                        showAjaxModal();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select form', 'error');
                    }
                }
                return false;
            });

            $("[id*=btnDelSubCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('3');
                if ($("[id*=htype").val() == '3') {
                    $("[id*=hid").val($("[id*=ddlForm]").val());
                    if ($("[id*=hid").val() != "0") {
                        DeleteSubCategoryContactData();
                        displayMsg('lblmsg', 'Deleted successfully', 'success');
                        setPopDefault();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select form', 'error');
                    }
                }
                return false;
            });


        });


			</script>
     <script type="text/javascript">

         $(document).ready(function () {
             //$("[id*=hid]").val('0');
             //$("[id*=htype]").val('1');

             $("[id*=btnAddService]").click(function () {
                 $("[id*=hid]").val('0');
                 $("[id*=htype]").val('4');
                 $("[id*=modeltitle]").html("Add Sub Section");
                 setPopDefault();
                 showAjaxModal();
                 return false;
             });

             $("[id*=btnEditService]").click(function () {
                 $("[id*=hid]").val('0');
                 $("[id*=htype]").val('4');
                 $("[id*=modeltitle]").html("Edit Sub Section");
                 if ($("[id*=htype").val() == '4') {
                     $("[id*=hid").val($("[id*=ddlService]").val());
                     if ($("[id*=hid").val() != "0") {
                         $("[id*=txtName]").val($("#ddlService option:selected").text());
                         showAjaxModal();
                     }
                     else {
                         displayMsg('lblmsg', 'Please select sub section', 'error');
                     }
                 }
                 return false;
             });

             $("[id*=btnDelService]").click(function () {
                 $("[id*=hid]").val('0');
                 $("[id*=htype]").val('4');
                 if ($("[id*=htype").val() == '4') {
                     $("[id*=hid").val($("[id*=ddlService]").val());
                     if ($("[id*=hid").val() != "0") {
                         DeleteServiceContactData();
                         displayMsg('lblmsg', 'Deleted successfully', 'success');
                         setPopDefault();
                         window.location.href = window.location.href;
                     }
                     else {
                         displayMsg('lblmsg', 'Please select sub section', 'error');
                     }
                 }
                 return false;
             });


         });


			</script>
    <script type="text/javascript">
        $(function () {

            // SetSectorContactData();
            //SetCategoryContactData();


            SetServiceContactData();

            SetSubCategoryContactData();
            //$("[id*=ddlSector]").change(function () {
            //    //$("[id*=hSector").val($(this).val());

            //    SetCategoryContactData();
            //    SetSubCategoryContactData();
            //});
        });

        function setSectorDropdownValue() {
            //if ($("[id*=hSector").val() != '') {
            //    $("[id*=ddlSector]").val($("[id*=hSector").val());
            //}
        }
        //function SetSectorContactData() {
        //    //var id = $("[id$='hcid']").val();

        //    //if (id == "")
        //    //    id = "0";
        //    $.ajax({
        //        type: "POST",
        //        url: "Services/FormsServices.asmx/SectionsGet",
        //        //data: "{id:'" + id + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (r) {
        //            var ddlCustomers = $("[id*=ddlSector]");
        //            
        //            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
        //            $.each(r.d, function () {
        //                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
        //            });
        //            $("[id*=hCategory]").val('0');
        //           setSectorDropdownValue();
        //        }
        //    });
        //}

        //function AddSectorContactData() {
        //    //var id = $("[id$='hcid']").val();
        //    var name;
        //    var type;
        //    var id;

        //    name = $("[id*=txtName]").val();
        //    var dataObject = JSON.stringify({
        //        'name': name
        //    });
        //    $.ajax({
        //        type: "POST",
        //        url: "Services/FormsServices.asmx/SectionsAdd",
        //        data: dataObject,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (r) {
        //            var t = r.d;
        //            
        //            //alert(t.Value);
        //            $("[id*=hSector]").val(t.Value);
        //           //Bind dropdown
        //            SetSectorContactData();
        //            SetCategoryContactData();
        //            SetSubCategoryContactData();
        //           //set added value
        //           // alert($("[id*=hSector]").val());
        //           // setSectorDropdownValue();
        //        }
        //    });
        //}

        //function UpdateSectorContactData() {
        //    //var id = $("[id$='hcid']").val();
        //    var name;
        //    var type;
        //    var id;

        //    name = getName();
        //    id = getID();
        //    var dataObject = JSON.stringify({
        //        'name': name,
        //        'id':id
        //    });
        //    $.ajax({
        //        type: "POST",
        //        url: "Services/FormsServices.asmx/SectionsUpdate",
        //        data: dataObject,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (r) {
        //            var t = r.d;
        //            
        //            //alert(t.Value);
        //            $("[id*=hSector]").val(t.Value);
        //            //Bind dropdown
        //            SetSectorContactData();
        //            //set added value
        //            setSectorDropdownValue();
        //        }
        //    });
        //}

        //function DeleteSectorContactData() {
        //    //var id = $("[id$='hcid']").val();
        //    var id;
        //    var dataObject = JSON.stringify({
        //        'id': getID()
        //    });
        //    $.ajax({
        //        type: "POST",
        //        url: "Services/FormsServices.asmx/SectionsDelete",
        //        data: dataObject,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (r) {
        //            var t = r.d;
        //            //Bind dropdown
        //            SetSectorContactData();
        //            $("[id*=hSector]").val('0');
        //            setSectorDropdownValue();
        //            SetCategoryContactData();
        //            SetSubCategoryContactData();
        //        }
        //    });
        //}

        function SetdropdownsValue() {
            //if ($("[id*=hSector]").val().trim() != "") {
            //    $("[id*=ddlSector] option").each(function () {
            //        if ($(this).val() == $("[id*=hSector]").val()) {
            //            $(this).attr('selected', 'selected');
            //        }
            //    });
            //}
        }
</script>
    <script type="text/javascript">
        $(function () {

            //SetCategoryContactData();
            $("[id*=ddlCategory]").change(function () {
                $("[id*=hCategory").val($(this).val());
                SetSubCategoryContactData();
            });
        });

        function setCategoryDropdownValue() {
            if ($("[id*=hCategory").val() != '') {
                $("[id*=ddlCategory]").val($("[id*=hCategory").val());
            }
        }
        function SetCategoryContactData() {
            var id = $("[id*=hSector]").val();
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";

            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/Services/AdminServices.asmx/CategoryGet",
                    data: "{typeid:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlCategory]");

                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        $("[id*=hCategory]").val('0');
                        setCategoryDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlCategory]");

                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }

        function AddCategoryContactData() {
            var typeid = $("[id*=hSector]").val();
            var name = $("[id*=txtName]").val();

            var dataObject = JSON.stringify({
                'typeid': typeid,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/CategoryAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    $("[id*=hCategory]").val(t.Value);
                    //Bind dropdown
                    SetCategoryContactData();
                    SetSubCategoryContactData();
                    //set added value
                    // alert($("[id*=hCategory]").val());
                    // setCategoryDropdownValue();
                }
            });
        }

        function UpdateCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = getName();
            id = getID();
            var dataObject = JSON.stringify({
                'name': name,
                'id': id
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/CategoryUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    $("[id*=hCategory]").val(t.Value);
                    //Bind dropdown
                    SetCategoryContactData();
                    //set added value
                    setCategoryDropdownValue();
                }
            });
        }

        function DeleteCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var id;
            var dataObject = JSON.stringify({
                'id': getID()
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/CategoryDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    SetCategoryContactData();
                    $("[id*=hCategory]").val('0');
                    setCategoryDropdownValue();
                    SetSubCategoryContactData();

                }
            });
        }

        function SetdropdownsValue() {
            if ($("[id*=hCategory]").val().trim() != "") {
                $("[id*=ddlCategory] option").each(function () {
                    if ($(this).val() == $("[id*=hCategory]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
        }
</script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlForm]").change(function () {
                $("[id*=hSubCategory").val($(this).val());
                SetServiceContactData();
                buildPanel();
            });
        });

        function setSubCategoryDropdownValue() {
            if ($("[id*=hSubCategory").val() != '') {
                $("[id*=ddlForm]").val($("[id*=hSubCategory").val());
            }
        }
        function SetSubCategoryContactData() {

            var category = 1;// $("[id*=ddlCategory]").val();
            if (category == null)
                category = "0";
            else if (category == "")
                category = "0";

            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/Services/AdminServices.asmx/FormGet",
                    data: "{category:'" + category + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //async: false,
                    success: function (r) {
                        // alert('1');
                        var ddlCustomers = $("[id*=ddlForm]");
                        //
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });

                        var aid = $("[id*=hform]").val();
                        if (aid != "0") {
                            $("[id*=hSubCategory]").val(aid);
                            console.log('formid:' + aid);
                            setSubCategoryDropdownValue();
                            //GetFormData(aid);
                            SetServiceContactData();
                        }
                        else {
                            $("[id*=hSubCategory]").val('0');
                            setSubCategoryDropdownValue();
                        }

                        buildPanel();

                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlForm]");
                //
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }

        function AddSubCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var category = 0;// $("[id*=ddlCategory]").val();
            //alert(category);
            var name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'category': category,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/FormAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    $("[id*=hSubCategory]").val(t.Value);
                    //Bind dropdown
                    SetSubCategoryContactData();
                    //set added value
                    // alert($("[id*=hSubCategory]").val());
                    // setSubCategoryDropdownValue();
                }
            });
        }

        function UpdateSubCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = getName();
            id = getID();
            var dataObject = JSON.stringify({
                'name': name,
                'id': id
            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FormUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    $("[id*=hSubCategory]").val(t.Value);
                    //Bind dropdown
                    SetSubCategoryContactData();
                    //set added value
                    setSubCategoryDropdownValue();
                }
            });
        }

        function DeleteSubCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var id;
            var dataObject = JSON.stringify({
                'id': getID()
            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FormDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    SetSubCategoryContactData();
                    $("[id*=hSubCategory]").val('0');
                    setSubCategoryDropdownValue();

                }
            });
        }

        function SetdropdownsValue() {
            if ($("[id*=hSubCategory]").val().trim() != "") {
                $("[id*=ddlForm] option").each(function () {
                    if ($(this).val() == $("[id*=hSubCategory]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
        }
</script>

    <script type="text/javascript">
        $(function () {
            $("[id*=ddlService]").change(function () {
                $("[id*=hService").val($(this).val());
                var id = $("[id*=ddlService]").val();
                $("[id*=hfsubid").val($(this).val());
                //GetFormData(id);
                // console.log('id: ' + 51);
                // GetFormData('51');
            });
        });

        function setServiceDropdownValue() {

            if ($("[id*=hService").val() != '') {
                $("[id*=ddlService]").val($("[id*=hService").val());
            }
        }
        function SetServiceContactData() {
            var category = $("[id*=ddlForm]").val();

            if (category == null)
                category = "0";
            else if (category == "")
                category = "0";

            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/Services/AdminServices.asmx/FormPanelGet",
                    data: "{category:'" + category + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlService]");
                        //
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });




                        var panelid = $("[id*=hset]").val();
                        if (panelid != "0") {
                            $("[id*=hService]").val(panelid);
                            console.log('panelid: ' + $("[id*=hset]").val());
                            setServiceDropdownValue();

                            //  GetFormData(panelid);
                            // console.log('id: ' + 51);
                            //  GetFormData('51');
                        }
                        else {
                            $("[id*=hService]").val('0');
                            setServiceDropdownValue();
                        }

                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlService]");
                //
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }


        function AddServiceContactData() {
            //var id = $("[id$='hcid']").val();
            var subcategory = $("[id*=ddlForm]").val();

            var name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'category': subcategory,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FormPanelAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    SetServiceContactData();
                    //alert(t.Value);
                    $("[id*=hService]").val(t.Value);
                    //Bind dropdown

                    setServiceDropdownValue();

                    bindGrid();
                    //set added value
                    // alert($("[id*=hSubCategory]").val());
                    // setSubCategoryDropdownValue();
                }
            });
        }

        function UpdateServiceContactData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = getName();
            id = getID();
            var dataObject = JSON.stringify({
                'name': name,
                'id': id
            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FormPanelUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;

                    //alert(t.Value);
                    SetServiceContactData();
                    $("[id*=hService]").val(t.Value);
                    //Bind dropdown
                    // SetSubCategoryContactData();
                    //set added value
                    // setSubCategoryDropdownValue();


                    setServiceDropdownValue();
                }
            });
        }

        function UpdateRowPositionData(id, panelid, newposition, oldPosition) {

            var dataObject = JSON.stringify({
                'id': id,
                'panelid': panelid,
                'newposition': newposition,
                'oldPosition': oldPosition

            });
            $.ajax({
                type: "POST",
                url: "Services/FormsServices.asmx/FromDataUpdatePosition",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (r) {
                    var id = $("[id*=hfsubid").val();// $("[id*=ddlService]").val();
                    GetFormDataNew(id);
                    //GetFormData(id);
                    // console.log('id: ' + 51);
                    //  GetFormData('51');
                    //  var t = r.d;

                    //alert(t.Value);
                    // SetServiceContactData();
                    //$("[id*=hService]").val(t.Value);
                    //Bind dropdown
                    // SetSubCategoryContactData();
                    //set added value
                    // setSubCategoryDropdownValue();


                    // setServiceDropdownValue();
                }
            });
        }


        function DeleteServiceContactData() {
            //var id = $("[id$='hcid']").val();
            var id;
            var dataObject = JSON.stringify({
                'id': getID()
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/FormPanelDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    //SetSubCategoryContactData();
                    //$("[id*=hSubCategory]").val('0');
                    SetServiceContactData();
                    $("[id*=hService]").val('0');
                    setServiceDropdownValue();


                }
            });
        }

        //function SetdropdownsValue() {
        //    if ($("[id*=hSubCategory]").val().trim() != "") {
        //        $("[id*=ddlForm] option").each(function () {
        //            if ($(this).val() == $("[id*=hSubCategory]").val()) {
        //                $(this).attr('selected', 'selected');
        //            }
        //        });
        //    }
        //}
</script>
    
      
     <div class="row" >
    <div class="col-md-12">
         
        <div id="tbl"></div>

     <table id="students" class="table table-striped table-bordered hcolor tblforms"></table>
        </div>
         </div>
   <script type="text/javascript">

       $().ready(function () {


       }

       );
       function setNotes(val) {
           var retval = "";

           if (val != "" && val != undefined)
               retval = val;
           else
               retval = "";
           return retval;
       }
       function isChecked(val) {
           var retval = "";
           if (val == "true")
               retval = "checked";
           else
               retval = "";
           return retval;
       }
       function GetFormDatabyID(id) {
           try {
               $.ajax({
                   url: "Services/FormsServices.asmx/BindFromDataByID",
                   type: "POST",
                   data: "{'id': '" + id + "'}",
                   contentType: 'application/json; charset=utf-8',
                   dataType: "json",
                   async: true,
                   success: function (data) {

                       var NewData = jQuery.parseJSON(data.d);


                       for (var i = 0; i < NewData.length; i++) {
                           //var CCID = NewData[i].CCID
                           var ID = NewData[i].ID
                           var TypeOfField = NewData[i].TypeOfField;
                           var LabelName = NewData[i].LabelName;
                           var ListValue = NewData[i].ListValue

                           $("[id*=ddlControl]").val(TypeOfField);
                           $("[id*=txtlable]").val(LabelName);
                           $("[id*=txtvalues]").val(ListValue);
                           controldropdownshow(TypeOfField);
                           if (TypeOfField != 'Textbox' || TypeOfField != 'Textarea') {

                               setItems(ListValue);
                           }
                       }


                   }
               });
           }
           catch (e) {
               var err = e;
           }
       }
       function GetFormData(id) {
           try {
               $.ajax({
                   url: "../../WF/DC/Services/AdminServices.asmx/BindFromData",
                   type: "POST",
                   data: "{'id': '" + id + "'}",
                   contentType: 'application/json; charset=utf-8',
                   dataType: "json",
                   async: true,
                   success: function (data) {

                       var tbl_id = 'tbl_' + id;
                       console.log('table id:' + tbl_id);
                       var NewData = jQuery.parseJSON(data.d);
                       var x = "<table id='" + tbl_id + "' class='table table-striped table-bordered hcolor tblforms'><thead><tr>"
                           + "<th style='width:3%;'></th>"
                           + "<th style='width:3%;'></th>"
                           + "<th  style='width:5%;'>ID</th>"
                           + "<th  style='width:5%;'>Position</th>"
                           + "<th  style='width:30%;'>Label</th>"
                           + "<th  style='width:10%;'>Control</th>"
                           + "<th style='width:50%;'>Values</th>"
                           + "<th style='width:3%;'></th> "
                           + "</tr></thead>";

                       x = x + "<tbody>"

                       for (var i = 0; i < NewData.length; i++) {
                           //var CCID = NewData[i].CCID
                           var ID = NewData[i].ID
                           var TypeOfField = NewData[i].TypeOfField;
                           var LabelName = NewData[i].LabelName;
                           var ListValue = NewData[i].ListValue
                           var Position = NewData[i].Position;

                           x = x
                               + "<tr><td>" + ButtonMoveHtml(ID, LabelName, TypeOfField, ListValue)
                               + "<td>" + ButtonHtml(ID, LabelName, TypeOfField, ListValue)
                               + "</td><td>" + ID
                               + "</td><td>" + Position
                               + "</td><td>" + LabelName
                               + "</td><td>" + TypeOfField
                               + "</td><td>" + DisplayValues(ListValue, TypeOfField)
                               + "</td><td>" + ButtondeleteHtml(ID)
                               + "</td></tr>";
                       }

                       x = x + "</tbody></table>";



                       $("#" + tbl_id).empty();
                       $("#tbl").append(x);
                       BindTable(tbl_id);
                       $("#" + tbl_id).removeClass("no-footer");
                       //setStatusBackColor();
                   }
               });
           }
           catch (e) {
               var err = e;
           }
       }
       function BindTable(tbl_id) {

           var table = $('#' + tbl_id).DataTable({
               'Ordering': true,
               "order": [[3, "asc"]],
               'paging': false,
               'bFilter': false,
               'lengthChange': false,
               'destroy': true,
               'rowReorder': true,
               "columnDefs": [{
                   "targets": 0, "orderable": false, className: 'reorder'
               },
                   // { orderable: true, className: 'reorder', targets: 0 },
                   //{
                   //    "targets": 7,
                   //    "visible": true
                   //}

               ]
           });

           table.on('row-reorder', function (e, diff, edit) {
               var result = 'Reorder started on row: ' + edit.triggerRow.data()[1] + '<br>';

               for (var i = 0, ien = diff.length; i < ien; i++) {
                   var rowData = table.row(diff[i].node).data();

                   result += rowData[1] + ' updated to be in position ' +
                       diff[i].newData + ' (was ' + diff[i].oldData + ')<br>';
                   var id = rowData[2];
                   var panelid = $("[id*=ddlService]").val();
                   var newPosition = diff[i].newPosition;
                   var oldPosition = diff[i].oldPosition;

                   UpdateRowPositionData(id, panelid, newPosition + 1, oldPosition + 1);

               }

               // $('#result').html('Event result:<br>' + result);
           });

           table.columns([2, 3]).visible(false);
       }

       //function DisplayValues(ListValue, TypeOfField) {
       //    var HtmlText = "";
       //    if (TypeOfField == 'Checklistbox' || TypeOfField == 'Radiobuttonlist' || TypeOfField == 'List') {
       //        HtmlText = "<ul>"
       //        var array = ListValue.split(',');

       //        $.each(array, function (index, value) {
       //            HtmlText = HtmlText + "<li>" + value + "</li>";
       //        });
       //        HtmlText = HtmlText + "</ul>";
       //    }
       //    else if (TypeOfField == 'Textbox' || TypeOfField == 'Textarea' || TypeOfField == 'Date') {
       //        HtmlText = ListValue;
       //    }
       //    else {
       //        HtmlText = ListValue;
       //    }
       //    return HtmlText;
       //}
       function DisplayValues(ListValue, TypeOfField, ControlID, ListValueValues, ListNotesValues) {
           console.log(TypeOfField);
           var HtmlText = "";

           if (TypeOfField == 'Checklistbox') {
              // debugger;
               HtmlText = "<div>"
               var array = ListValue.split(',');
               var svalues = ListValueValues.split(',');
               var snotes = ListNotesValues.split(',');
              // debugger;
               $.each(array, function (index, value) {
                   // HtmlText = HtmlText + "<li>" + value + "</li>";
                   HtmlText = HtmlText + "<div style='display:inline-line;padding: 5px;'>";
                   HtmlText = HtmlText + "<input type='checkbox' value='" + value + "'  id='ctl_chk_" + ControlID + "_" + index + "' " + isChecked(svalues[index]) + "  />";
                   HtmlText = HtmlText + "<label>" + value + "</label>";
                   // HtmlText = HtmlText + "<input type='textbox'  id='cctl_chk_" + ControlID + "_" + index + "_chktxt' class='form-control' style='width:50%;float:right;' value='" + setNotes(snotes[index])+"' />";
                   HtmlText = HtmlText + "</div>"
               });
               HtmlText = HtmlText + "</div>";
           }
           else if (TypeOfField == 'Radiobuttonlist') {
               HtmlText = "<div>"
               var array = ListValue.split(',');
               var svalues = ListValueValues.split(',');
               var snotes = ListNotesValues.split(',');

               $.each(array, function (index, value) {
                   // HtmlText = HtmlText + "<li>" + value + "</li>";
                   HtmlText = HtmlText + "<div style='display:inline-line;padding: 5px;'>";
                   HtmlText = HtmlText + "<input type='radio' name='ctl_chk_" + ControlID + "' value='" + value + "'  id='ctl_chk_" + ControlID + "_" + index + "' " + isChecked(svalues[index]) + "  />";
                   HtmlText = HtmlText + "<label>" + value + "</label>";
                   // HtmlText = HtmlText + "<input type='textbox'  id='cctl_chk_" + ControlID + "_" + index + "_chktxt' class='form-control' style='width:50%;float:right;' value='" + setNotes(snotes[index])+"' />";
                   HtmlText = HtmlText + "</div>"
               });
               HtmlText = HtmlText + "</div>";
           }
           else if (TypeOfField == 'List') {
               HtmlText = "<select class='form-control' name='ctl_ddl_" + ControlID + "' id='ctl_ddl_" + ControlID + "' style='width:80%'>";
               HtmlText = HtmlText + "<option value=''>" + "Please select..." + "</option>";
               var array = ListValue.split(',');

               $.each(array, function (index, value) {
                   if (value == ListValueValues)
                       HtmlText = HtmlText + "<option value='" + value + "' selected>" + value + "</option>";
                   else
                       HtmlText = HtmlText + "<option value='" + value + "'>" + value + "</option>";
                   // HtmlText = HtmlText + "<li>" + value + "</li>";
               });
               HtmlText = HtmlText + "</select>";
           }
           else if (TypeOfField == 'Textbox') {
               HtmlText = "<input type='text' id='ctl_txt_" + ControlID + "' class='form-control' value='" + ListValueValues + "' style='width:80%'/>";
           }
           else if (TypeOfField == 'Date') {
               HtmlText = "<input type='text' id='ctl_txt_" + ControlID + "' class='form-control datepicker' data-format='mm/dd/yyyy' value='" + ListValueValues + "' style='width:125px'/>";
           }
           else if (TypeOfField == 'Textarea') {
               HtmlText = "<textarea id='ctl_txt_" + ControlID + "' class='form-control' style='width:80%;height:180px'> " + ListValueValues + " </textarea>";
           }



           return HtmlText;
       }
       function ButtonHtml(Id, subid) {
           var HtmlText = " <a target='_blank' id=Link" + Id + " onclick=editFormdata('" + Id + "','" + subid + "') style=' font-weight: bold'><span class='fa-edit' style='font-size:1.2em'></span></a>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }
       function ButtonMoveHtml(Id, LabelName, TypeOfField, ListValue) {
           var HtmlText = " <a target='_blank' id=Link" + Id + " style=' font-weight: bold'><span class='fa-arrows' style='font-size:1.2em'></span></a>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }
       function ButtondeleteHtml(Id) {
           var HtmlText = " <a target='_blank' id=Linkdel" + Id + " onclick=deleteFormdata('" + Id + "') style=' font-weight: bold'><span class='fa-trash' style='font-size:1.2em'></span></a>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }



</script>
    

    <script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js">
</script>
    
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />


    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/rowreorder/1.2.7/css/rowReorder.dataTables.min.css" />




<script type="text/javascript" src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js">

</script>
    <script type="text/javascript" src="https://cdn.datatables.net/rowreorder/1.2.7/js/dataTables.rowReorder.min.js"></script>
<%--<script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
</script>--%>
<%--<script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>--%>
   

<%--<script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
</script>--%>
<%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>--%>

                <%# System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%# System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
        </div>

            </div>
        </div>

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
  
	
</body>
</html>
