<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="HVACDiagnostics.aspx.cs" Inherits="DeffinityAppDev.WF.DC.HVACDiagnostics" EnableEventValidation="false" %>

<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    HVAC Diagnostics
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
   
    <style>
  
.timer{
	display: -webkit-inline-box;
    font-size: -webkit-xxx-large;
}
#controls{
    display: -webkit-inline-box;
}
#controls > div{
	margin:5px;
}

.jst-timeout{
color:red
}
.container { margin: 150px auto; }
</style>
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
    </style>
     <div class="form-group row">
         <div id="loading">
        <asp:Label SkinID="Loading" ID="lblloading" runat="server" ClientIDMode="Static"></asp:Label>
             </div>

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
         <script>
            
             var input = {
                 year: 0,
                 month: 0,
                 day: 0,
                 hours: 0,
                 minutes: 0,
                 seconds: 0
             };

             var timestamp = new Date(input.year, input.month, input.day,
                 input.hours, input.minutes, input.seconds);
             var interval = 1;
             var timer;
           
             $(document).ready(function () {
                 GetHVACData(getQuerystring('callid'));
                 $("#btnStartTime").click(function () {
                     debugger;
                     $("#countdown2").show();
                     timer = setInterval(function () {

                         timestamp = new Date(timestamp.getTime() + interval * 1000);
                         // document.getElementById('countdown2').innerHTML = timestamp.getDay() + 'd:' + timestamp.getHours() + 'h:' + timestamp.getMinutes() + 'm:' + timestamp.getSeconds() + 's';
                         document.getElementById('countdown2').innerHTML = timestamp.getHours() + 'h:' + timestamp.getMinutes() + 'm:' + timestamp.getSeconds() + 's';
                     }, Math.abs(interval) * 1000);
                     StartTimeJob(getQuerystring('callid'), timestamp.getHours(), timestamp.getMinutes(), timestamp.getSeconds(),1);
                     return false;
                 });

                 $("#btnStopTimer").click(function () {
                     $("#countdown2").show();
                     StartTimeJob(getQuerystring('callid'), timestamp.getHours(), timestamp.getMinutes(), timestamp.getSeconds(), 0);
                     clearInterval(timer);
                    
                     return false;
                 });





             });
            
             function StartTimeJob(jobid, hour, minute, second, status) {
                 try {
                     var dataObject = JSON.stringify({
                         'jobid': jobid,
                         'hour': hour,
                         'minute': minute,
                         'second': second,
                         'status': status,
                     });
                     $.ajax({
                         type: "POST",
                         url: "../../WF/DC/Services/AdminServices.asmx/StartTimeJob",
                         data: dataObject,
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         async: false,
                         success: function (r) {
                             

                            // displayMsg('lblmsg', 'Applied successfully', 'success');

                         }
                     });
                 }
                 catch (e) {
                     var err = e;
                 }
             }
           
             function GetHVACData(jobid) {
                 try {
                     
                     var dataObject = JSON.stringify({
                         //'serviceid': serviceid,
                         'jobid': jobid
                     });
                     $.ajax({
                         url: "../../WF/DC/Services/AdminServices.asmx/BindHVACData",
                         type: "POST",
                         data: dataObject,
                         contentType: 'application/json; charset=utf-8',
                         dataType: "json",
                         async: true,
                         success: function (data) {
                            // document.getElementById('countdown2').innerHTML = '';
                             var NewData = jQuery.parseJSON(data.d);

                             for (var i = 0; i < NewData.length; i++) {
                                 //var CCID = NewData[i].CCID
                                 var ID = NewData[i].ID
                                 var LoggedBy = NewData[i].LoggedBy;
                                 var CallID = NewData[i].CallID;
                                 var StartTime = NewData[i].StartTime
                                 var StopTime = NewData[i].StopTime;
                                 var StartHour = NewData[i].StartHour;
                                 var StartMinute = NewData[i].StartMinute;
                                 var StartSecond = NewData[i].StartSecond;
                                 var Status = NewData[i].Status;
                                 $("#countdown2").show();
                                 document.getElementById('countdown2').innerHTML = StartHour + 'h:' + StartMinute + 'm:' + StartSecond + 's';

                             }
                         }
                     });
                 }
                 catch (e) {
                     var err = e;
                 }
             }
           
            
         </script>
         <style>
             #countdown2{
                 text-align:center;
                 width: 240px;
    font-size: 40px;
    background: #5ca323;
    padding: 15px;
    color:white;
    
             }
         </style>
        
          <div class="form-group row" >
      <div class="col-md-5 form-inline">
         
          <asp:Button ClientIDMode="Static" ID="btnStartTime" runat="server" SkinID="btnDefault" Text="Start Timer" CssClass="btn btn-primary btn-lg" style="padding:30px;font-size:25px" /> <asp:Button ClientIDMode="Static" ID="btnStopTimer" runat="server" SkinID="btnDefault" Text="Stop Timer" CssClass="btn btn-primary btn-lg" style="padding:30px;font-size:25px"  /> 
          </div>
               <div class="col-md-3 form-inline">
                    <div id="countdown2" style="display:none;"></div>
                   </div>
              </div>

      <div class="col-md-12" id="div_msg">
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

     <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Problem</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlCategory" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hCategory" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnEditCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnDelCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the category?');" style="display:none;visibility:hidden;"></asp:LinkButton>
              </div>
          </div>
        </div>
      <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Equipment Type</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlSubCategory" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hSubCategory" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddSubCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnEditSubCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnDelSubCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the sub category?');" style="display:none;visibility:hidden;"></asp:LinkButton>
              </div>
          </div>
        </div>
     <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Service</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlService" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hService" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddService"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnEditService" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static" style="display:none;visibility:hidden;"></asp:LinkButton>
              <asp:LinkButton ID="btnDelService" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the service?');" style="display:none;visibility:hidden;"></asp:LinkButton>
              </div>
          </div>
        </div>
     <div class="form-group row" >
          <div class="col-md-10">
      <label class="col-sm-2 control-label"> </label>
          <div class="col-sm-10 ">
               <asp:Button ID="btnApply" runat="server" SkinID="btnDefault" Text="Apply" ClientIDMode="Static"  />
          <asp:Button ID="btnAddform" runat="server" SkinID="btnDefault" Text="Add Service" ClientIDMode="Static" style="display:none;visibility:hidden;" />

              

          </div>
              </div>
         </div>

    <script type="text/javascript">

       

        //apply service to job
        $(document).ready(function () {
            $("#btnApply").click(function () {
                var serviceid = $("[id*=ddlService]").val();
                var callid = getQuerystring('callid');
                applyService(serviceid, callid);
                //Update the 
                //console.log(serviceid);

                return false;

            });
        });

        function applyService(serviceid,jobid) {
            var dataObject = JSON.stringify({
                'serviceid': serviceid,
                'jobid': jobid,
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/ApplyServicetoJob",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    GetFormData(jobid);

                    displayMsg('lblmsg', 'Applied successfully', 'success');

                }
            });
        }

        function loadfirsttime() {

        }

    </script>

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
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector" style="display:none;">Close</button>
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
					<h4 class="modal-title"><span id="modeltitle1"> Add Service </span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label"> Name</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtlable" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
              <asp:HiddenField ID="hfdid" runat="server" ClientIDMode="Static" />
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
                                <asp:ListItem Value="List" Text="List"> </asp:ListItem>
                 <asp:ListItem Value="Checklistbox" Text="Checklistbox"> </asp:ListItem>
             </asp:DropDownList>

              </div>
          </div>
              </div>
					  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label"> Values</label>
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
					<button type="button" class="btn btn-info" id="btnSubmit1">Submit</button>
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
        function bindGrid(serviceid,jobid) {
            //var id = $("[id*=ddlS]").val();
            GetFormData( jobid);
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
        function editFormdata(id, LabelName, TypeOfField, ListValue) {
            $("[id*=modeltitle1]").html("Edit Service");
            clearFormData();
            setfID(id);
            GetFormDatabyID(id);
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
            if (c == 'Textbox') {
                $("[id*=ul_items]").hide();
                $("[id*=txtadd]").hide();
                $("[id*=txtvalues]").show();
                $('#btnfadd').hide();
            }
            else {
                $("[id*=ul_items]").show();
                $("[id*=txtadd]").show()
                $("[id*=txtvalues]").hide();
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
                $("[id*=modeltitle1]").html("Add Service");
                setfID('0');
                clearFormData();
                showAjaxModalform();
                return false;
            });

            $("[id*=btnSubmit1]").click(function () {
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
            });
        });

        function AddFormData() {
            var id = $("[id*=hfdid]").val();
            var subid = $("[id*=ddlSubCategory]").val();
            var controltype = $("[id*=ddlControl]").val();
            var cname = $("[id*=txtlable]").val();
            var cvalues = '';
            if (controltype != 'Textbox')
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
                url: "../../WF/DC/Services/AdminServices.asmx/FromDataAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    //bindGrid();
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
                    //bindGrid();
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
                    $("[id*=hCategory]").val(t.Value);
                    //Bind dropdown
                    SetCategoryContactData();
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
                 $('[id*=' + element + ']').children("p").fadeOut(7000);
             }
             else if (msgtype == 'success') {
                 $('[id*=' + element + ']').html('<p class="bg-success">' + msg + '</p>');
                 $('[id*=' + element + ']').children("p").fadeOut(7000);
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
                $("[id*=modeltitle]").html("Add Subcategory");
                setPopDefault();
                showAjaxModal();
                return false;
            });

            $("[id*=btnEditSubCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('3');
                $("[id*=modeltitle]").html("Edit Subcategory");
                if ($("[id*=htype").val() == '3') {
                    $("[id*=hid").val($("[id*=ddlSubCategory]").val());
                    if ($("[id*=hid").val() != "0") {
                        $("[id*=txtName]").val($("#ddlSubCategory option:selected").text());
                        showAjaxModal();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select sub category', 'error');
                    }
                }
                return false;
            });

            $("[id*=btnDelSubCategory]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('3');
                if ($("[id*=htype").val() == '3') {
                    $("[id*=hid").val($("[id*=ddlSubCategory]").val());
                    if ($("[id*=hid").val() != "0") {
                        DeleteSubCategoryContactData();
                        displayMsg('lblmsg', 'Deleted successfully', 'success');
                        setPopDefault();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select sub category', 'error');
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
                 $("[id*=modeltitle]").html("Add Service");
                 setPopDefault();
                 showAjaxModal();
                 return false;
             });

             $("[id*=btnEditService]").click(function () {
                 $("[id*=hid]").val('0');
                 $("[id*=htype]").val('4');
                 $("[id*=modeltitle]").html("Edit Service");
                 if ($("[id*=htype").val() == '4') {
                     $("[id*=hid").val($("[id*=ddlService]").val());
                     if ($("[id*=hid").val() != "0") {
                         $("[id*=txtName]").val($("#ddlService option:selected").text());
                         showAjaxModal();
                     }
                     else {
                         displayMsg('lblmsg', 'Please select service', 'error');
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
                     }
                     else {
                         displayMsg('lblmsg', 'Please select service', 'error');
                     }
                 }
                 return false;
             });


         });


			</script>
    <script type="text/javascript">
        $(function () {

            // SetSectorContactData();
            SetCategoryContactData();
            SetSubCategoryContactData();
            SetServiceContactData();
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
        //        url: "../../WF/DC/Services/AdminServices.asmx/SectionsGet",
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
        //        url: "../../WF/DC/Services/AdminServices.asmx/SectionsAdd",
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
        //        url: "../../WF/DC/Services/AdminServices.asmx/SectionsUpdate",
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
        //        url: "../../WF/DC/Services/AdminServices.asmx/SectionsDelete",
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
            $("[id*=ddlSubCategory]").change(function () {
                $("[id*=hSubCategory").val($(this).val());
                SetServiceContactData();
            });
        });

        function setSubCategoryDropdownValue() {
            if ($("[id*=hSubCategory").val() != '') {
                $("[id*=ddlSubCategory]").val($("[id*=hSubCategory").val());
            }
        }
        function SetSubCategoryContactData() {
            var category = $("[id*=ddlCategory]").val();
            if (category == null)
                category = "0";
            else if (category == "")
                category = "0";
            
            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/Services/AdminServices.asmx/SubCategoryGet",
                    data: "{category:'" + category + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlSubCategory]");
                        //
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        $("[id*=hSubCategory]").val('0');
                        setSubCategoryDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlSubCategory]");
                //
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }

        function AddSubCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var category = $("[id*=ddlCategory]").val();
            //alert(category);
            var name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'category': category,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/SubCategoryAdd",
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
                url: "../../WF/DC/Services/AdminServices.asmx/SubCategoryUpdate",
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
                url: "../../WF/DC/Services/AdminServices.asmx/SubCategoryDelete",
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
                $("[id*=ddlSubCategory] option").each(function () {
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
            });
        });

        function setServiceDropdownValue() {
            
            if ($("[id*=hService").val() != '') {
                $("[id*=ddlService]").val($("[id*=hService").val());
            }
        }
        function SetServiceContactData() {
            var category = $("[id*=ddlSubCategory]").val();
            if (category == null)
                category = "0";
            else if (category == "")
                category = "0";
            
            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/Services/AdminServices.asmx/ServiceGet",
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
                        $("[id*=hService]").val('0');
                        setServiceDropdownValue();
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
            var subcategory = $("[id*=ddlSubCategory]").val();

            var name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'category': subcategory,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/DC/Services/AdminServices.asmx/ServiceAdd",
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
                url: "../../WF/DC/Services/AdminServices.asmx/ServiceUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    
                    //alert(t.Value);

                    $("[id*=hService]").val(t.Value);
                    //Bind dropdown
                    // SetSubCategoryContactData();
                    //set added value
                    // setSubCategoryDropdownValue();

                    SetServiceContactData();
                    setServiceDropdownValue();
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
                url: "../../WF/DC/Services/AdminServices.asmx/ServiceDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    //SetSubCategoryContactData();
                    //$("[id*=hSubCategory]").val('0');
                    $("[id*=hService]").val('0');
                    setServiceDropdownValue();

                }
            });
        }

        //function SetdropdownsValue() {
        //    if ($("[id*=hSubCategory]").val().trim() != "") {
        //        $("[id*=ddlSubCategory] option").each(function () {
        //            if ($(this).val() == $("[id*=hSubCategory]").val()) {
        //                $(this).attr('selected', 'selected');
        //            }
        //        });
        //    }
        //}
</script>
   
     <div class="row" >
    <div class="col-md-12">
        <div style="text-align:right;"> <asp:Button ID="btnSave" runat="server" SkinID="btnDefault" Text="Save Changes" ClientIDMode="Static" style="float:right;" /> </div>
     <table id="students" class="table table-striped table-bordered"></table>
        </div>
         </div>
   <script type="text/javascript">

       $().ready(function () {

           var jobid = getQuerystring('callid');
           if (jobid != '')
               GetFormData(jobid);
           $('#btnSave').click(function () {
               
               var retval = saveData();

               displayMsg('lblmsg', 'Updated successfully', 'success');
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
       function GetFormDatabyID(id) {
          
           try {
               $.ajax({
                   url: "../../WF/DC/Services/AdminServices.asmx/BindFromDataByID",
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
                           if (TypeOfField != 'Textbox') {

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
       function GetFormData(jobid) {
          
           try {

               var dataObject = JSON.stringify({
                   //'serviceid': serviceid,
                   'jobid': jobid
               });
               $.ajax({
                   url: "../../WF/DC/Services/AdminServices.asmx/BindFromData",
                   type: "POST",
                   data: dataObject,
                   contentType: 'application/json; charset=utf-8',
                   dataType: "json",
                   async: false,
                   success: function (data) {
                       
                       var NewData = jQuery.parseJSON(data.d);
                       var x = "<thead><tr>"
                          // + "<th style='width:3%;'></th>"
                           + "<th  style='width:20%;color:white;'>Primary Task</th>"
                           //+ "<th  style='width:10%;'>Control</th>"
                           + "<th style='width:70%;color:white;'>Results/Findings</th>"
                           //+ "<th style='width:10%;color:white;'></th>"
                           + "<th style='width:3%;'></th> "
                           + "</tr></thead>";

                       x = x + "<tbody>"
                       debugger;
                       for (var i = 0; i < NewData.length; i++) {
                           //var CCID = NewData[i].CCID
                           var ID = NewData[i].ID
                           var TypeOfField = NewData[i].TypeOfField;
                           var LabelName = NewData[i].LabelName;
                           var ListValue = NewData[i].ListValue
                           var ListValueValues = NewData[i].ListValueValues;
                           var ListNotesValues = NewData[i].ListNotesValues;
                           var subcategoryid = NewData[i].subcategoryid;
                           var categoryid = NewData[i].categoryid;
                           var PartnerServiceID = NewData[i].PartnerServiceID;
                           var FileName = NewData[i].FileName;
                          
                           $("[id*=hCategory").val(categoryid);
                           setCategoryDropdownValue();
                           SetSubCategoryContactData();
                           $("[id*=hSubCategory").val(subcategoryid);
                           setSubCategoryDropdownValue();
                           SetServiceContactData();
                           $("[id*=hService").val(PartnerServiceID);
                           console.log("Service:" + PartnerServiceID);
                           setServiceDropdownValue();
                           
                          
                           var 
                           x = x +"<tr>"
                               //+ "<tr><td>" + ButtonHtml(ID, LabelName, TypeOfField, ListValue)
                               + "<td>" + LabelName
                               //+ "</td><td>" + TypeOfField
                               + "</td><td>" + DisplayValues(ListValue, TypeOfField, ID, ListValueValues, ListNotesValues)
                               //+ "</td><td>" + ""
                               + "</td><td>" + addfileupload(ID, FileName)
                               + "</td></tr>";
                       }

                       x = x + "</tbody>";
                       $("#students").empty();
                       $("#students").append(x);
                       BindTable();
                       $("#students").removeClass("no-footer");
                       //setStatusBackColor();
                   }
               });
           }
           catch (e) {
               var err = e;
           }
       }
       function BindTable() {
           var table = $('#students').DataTable({
               'Ordering': true,
               "order": [[0, "asc"]],
               'paging': false,
               'bFilter': false,
               'lengthChange': false,
               'destroy': true,
               'rowReorder': true,
               "columnDefs": [{
                   "targets": 0, "orderable": false
               },
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
               }

               // $('#result').html('Event result:<br>' + result);
           });
       }
       function isChecked(val) {
           var retval = "";
           if (val == "true")
               retval = "checked";
           else
               retval = "";
           return retval;
       }
       function setNotes(val) {
           var retval = "";
          
           if (val != "" && val != undefined)
               retval = val;
           else
               retval = "";
           return retval;
       }
       function DisplayValues(ListValue, TypeOfField, ControlID, ListValueValues, ListNotesValues) {
           var HtmlText = "";
           if (TypeOfField == 'Checklistbox') {
               HtmlText = "<div>"
               var array = ListValue.split(',');
               var svalues = ListValueValues.split(',');
               var snotes = ListNotesValues.split(',');

               $.each(array, function (index, value) {
                  // HtmlText = HtmlText + "<li>" + value + "</li>";
                   HtmlText = HtmlText + "<div style='display:inline-line;padding: 5px;'>";
                   HtmlText = HtmlText + "<input type='checkbox' value='" + value + "'  id='ctl_chk_" + ControlID + "_" + index + "' " + isChecked(svalues[index])+"  />";
                   HtmlText = HtmlText + "<label>" + value + "</label>" ;
                   HtmlText = HtmlText + "<input type='textbox'  id='cctl_chk_" + ControlID + "_" + index + "_chktxt' class='form-control' style='width:50%;float:right;' value='" + setNotes(snotes[index])+"' />";
                   HtmlText = HtmlText + "</div>"
               });
               HtmlText = HtmlText + "</div>";
           }
           else if (TypeOfField == 'List')
           {
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
               HtmlText = "<input type='text' id='ctl_txt_" + ControlID + "' class='form-control' value='" + ListValueValues +"' style='width:80%'/>" ;
           }
           return HtmlText;
       }

       function ButtonHtml(Id, LabelName, TypeOfField, ListValue) {
           var HtmlText = " <a target='_blank' id=Link" + Id + " onclick=editFormdata('" + Id + "') style=' font-weight: bold'><span class='fa-edit' style='font-size:1.2em'></span></a>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }

       function ButtondeleteHtml(Id) {
           var HtmlText = " <a target='_blank' id=Linkdel" + Id + " onclick=deleteFormdata('" + Id + "') style=' font-weight: bold'><span class='fa-trash' style='font-size:1.2em'></span></a>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }

       function addfileupload(Id, filename) {

           var s = "";
           var HtmlText = "";
           if (filename != "")
               s = "";
               else
               s = "display:none;";

           var h = "../../WF/DC/Services/filedownload.ashx?id=" + Id + "&jobid=" + getQuerystring('callid');
           HtmlText = HtmlText + "<div style='padding-bottom:15px;" + s + "' class='form-inline' id='div_file_" + Id + "' > <lable>" + filename+"</label> <a href="+h+" target='_blank' id=Linkdown" + Id + "  class='btn btn-info btn-xs'>Download</a> <a target='_blank' id=Linkdel" + Id + " onclick=deleteFile('" + Id + "') class='btn btn-info btn-xs'>Delete</a></div>";
           HtmlText = HtmlText + '<div style="padding-bottom:5px"> <input id="ctl_file_' + Id + '" name = "ctl_file_' + Id + '" type="file" /></div>';
           HtmlText = HtmlText + "<div style='padding-bottom:5px'> <a target='_blank' id=Linkup" + Id + " onclick=uploadfile('" + Id + "') class='btn btn-black btn-xs'>Upload</a> </div>";
           //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
           return HtmlText;
       }
       function downloadFile(id) {
           //window.location = "../../WF/DC/Services/filedownload.ashx?" + "id=" + id + "&jobid=" + getQuerystring('callid');
           //console.log('down load click');
           //debugger;
           //var data = new FormData();

           //data.append("id", id);
           //data.append("jobid", getQuerystring('callid'));

           //$.ajax({
           //    url: "../../WF/DC/Services/filedownload.ashx?" + "id=" + id + "&jobid=" + getQuerystring('callid'),
           //    type: "GET",
           //    //data: data,
           //    contentType: false,
           //    processData: false,
           //    success: function (result) {
           //       // displayMsg('lblmsg', 'Uploaded successfully', 'success');
           //        // alert(result);
           //    },
           //    error: function (err) {
           //        //alert(err.statusText)
           //    }
           //});

           return false;
       }
       function deleteFile(id) {
           var data = new FormData();
           data.append("id", id);
           data.append("jobid", getQuerystring('callid'));

           $.ajax({
               url: "../../WF/DC/Services/AdminServices.asmx/DeleteFile",
               type: "POST",
               data: data,
               contentType: false,
               processData: false,
               success: function (result) {
                   $('#div_file_' + id).hide();
                   displayMsg('lblmsg', 'Deleted successfully', 'success');
                   // alert(result);
               },
               error: function (err) {
                   //alert(err.statusText)
               }
           });

           return false;
       }
       function uploadfile(id) {
           console.log('click');
           debugger;
           var fileUpload = $("#ctl_file_"+id).get(0);
           var files = fileUpload.files;

           var data = new FormData();
           for (var i = 0; i < files.length; i++) {
               data.append(files[i].name, files[i]);
               console.log(files[i].name);
           }

           data.append("id", id);
           data.append("jobid", getQuerystring('callid'));

           $.ajax({
               url: "../../WF/DC/Services/AdminServices.asmx/UploadFile",
               type: "POST",
               data: data,
               contentType: false,
               processData: false,
               success: function (result) {
                   $('#div_file_' + id).show();
                   $('#ctl_file_' + id).val('');
                   displayMsg('lblmsg', 'Uploaded successfully', 'success');
                  // alert(result);
               },
               error: function (err) {
                   //alert(err.statusText)
               }
           });

           return false;
       }
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
               'files':fileList,
               'jobid': getQuerystring('callid')
           });
           $.ajax({
               type: "POST",
               url: "../../WF/DC/Services/AdminServices.asmx/UpdateData",
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
               }
           });


       }


</script>
    

    <script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js">
</script>
     <%--<script src="../../Scripts/jquery-countdown-timer-control.js"></script>--%>
    
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


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
