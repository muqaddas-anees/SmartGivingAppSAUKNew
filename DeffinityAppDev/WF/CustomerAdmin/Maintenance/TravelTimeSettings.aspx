<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="TravelTimeSettings.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.TravelTimeSettings" %>

<%@ Register Src="~/WF/CustomerAdmin/Maintenance/Controls/MaintenanceTabCtrl.ascx" TagPrefix="Pref" TagName="MaintenanceTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Maintenance Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:MaintenanceTabCtrl runat="server" ID="MaintenanceTabCtrl" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="server">
    Travel Time Settings
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="panel_options" runat="server">
      <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/AdminSettings.aspxx"><i class="fa fa-arrow-left"></i> Return to Settings</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
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

      <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Travel Time</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlTimeYear" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hTimeYear" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddTimeYear"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditTimeYear" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelTimeYear" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the sub category?');"></asp:LinkButton>
              </div>
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
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector" style="display:none;">Close</button>
					<button type="button" class="btn btn-info" id="btnSubmit">Submit</button>
				</div>
			</div>
		</div>
	</div>

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
                            UpdateTimeYearData();
                            hideAjaxModal();
                            displayMsg('lblmsg', 'Updated successfully', 'success');
                        }
                        else {
                            displayMsg('lblerror', 'Please select TravelTime', 'error');
                        }
                    }
                    else {
                        //add save data
                        if ($("[id*=txtName").val().length > 0) {
                            AddTimeYearData();
                            hideAjaxModal();
                            displayMsg('lblmsg', 'Added successfully', 'success');
                        }
                        else {
                            displayMsg('lblerror', 'Please select TravelTime', 'error');
                        }
                    }
                }

            });
              
         });
     </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("[id*=hid]").val('0');
            $("[id*=htype]").val('1');

        });


    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            //$("[id*=hid]").val('0');
            //$("[id*=htype]").val('1');

            $("[id*=btnAddTimeYear]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                $("[id*=modeltitle]").html("Add Travel Time");
                setPopDefault();
                showAjaxModal();
                return false;
            });

            $("[id*=btnEditTimeYear]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                $("[id*=modeltitle]").html("Edit Travel Time");
                if ($("[id*=htype").val() == '2') {
                    $("[id*=hid").val($("[id*=ddlTimeYear]").val());
                    if ($("[id*=hid").val() != "0") {
                        $("[id*=txtName]").val($("#ddlTimeYear option:selected").text());
                        showAjaxModal();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select TravelTime', 'error');
                    }
                }
                return false;
            });

            $("[id*=btnDelTimeYear]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('2');
                if ($("[id*=htype").val() == '2') {
                    $("[id*=hid").val($("[id*=ddlTimeYear]").val());
                    if ($("[id*=hid").val() != "0") {
                        DeleteTimeYearData();

                        displayMsg('lblmsg', 'Deleted successfully', 'success');
                        setPopDefault();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select TravelTime', 'error');
                    }
                }
                return false;
            });


        });


			</script>
     
    <script type="text/javascript">
        $(function () {

            SetTimeYearData();

        });

        function setSectorDropdownValue() {

        }


        function SetdropdownsValue() {

        }
</script>
    
    <script type="text/javascript">
        $(function () {

            //SetCategoryContactData();
            $("[id*=ddlTimeYear]").change(function () {
                $("[id*=hTimeYear").val($(this).val());
                setCategoryDropdownValue();
            });
        });

        function setCategoryDropdownValue() {
            if ($("[id*=hTimeYear").val() != '') {
                $("[id*=ddlTimeYear]").val($("[id*=hTimeYear").val());
            }
        }
        function SetTimeYearData() {
            var id = $("[id*=hSector]").val();
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
            debugger;
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../../WF/CustomerAdmin/Maintenance/services/TravelTimeSettingsServices.asmx/TimeYearGet",
                    data: "{typeid:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlTimeYear]");
                        //debugger;
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        $("[id*=hTimeYear]").val('0');

                        setCategoryDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlTimeYear]");
                //debugger;
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }


        function AddTimeYearData() {
            var typeid = $("[id*=hSector]").val();
            var name = $("[id*=txtName]").val();

            var dataObject = JSON.stringify({
                'typeid': typeid,
                'name': name
            });
           
            $.ajax({
                type: "POST",
                url: "../../../WF/CustomerAdmin/Maintenance/services/TravelTimeSettingsServices.asmx/TimeYearAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    //alert(t.Value);
                    $("[id*=hTimeYear]").val(t.Value);
                    //Bind dropdown
                    SetTimeYearData();
                    //set added value
                    // alert($("[id*=hTimeYear]").val());
                    setCategoryDropdownValue();
                }
            });
        }

        function UpdateTimeYearData() {
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
                url: "../../../WF/CustomerAdmin/Maintenance/services/TravelTimeSettingsServices.asmx/TimeYearUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    //alert(t.Value);
                    $("[id*=hTimeYear]").val(t.Value);
                    //Bind dropdown
                    SetTimeYearData();
                    //set added value
                    setCategoryDropdownValue();
                }
            });
        }

        function DeleteTimeYearData() {
            var id = $("[id*=hid]").val();
            var dataObject = JSON.stringify({
                'id': id
            });
           
            $.ajax({
                type: "POST",
                url: "../../../WF/CustomerAdmin/Maintenance/services/TravelTimeSettingsServices.asmx/TimeYearDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    SetTimeYearData();
                    $("[id*=hTimeYear]").val('0');
                    setCategoryDropdownValue();

                }
            });
        }
        function SetdropdownsValue() {
            if ($("[id*=hTimeYear]").val().trim() != "") {
                $("[id*=ddlTimeYear] option").each(function () {
                    if ($(this).val() == $("[id*=hTimeYear]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
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


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>

