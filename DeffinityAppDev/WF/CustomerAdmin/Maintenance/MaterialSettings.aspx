<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="MaterialSettings.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.MaterialSettings" %>

<%@ Register Src="~/WF/CustomerAdmin/Maintenance/Controls/MaintenanceTabCtrl.ascx" TagPrefix="Pref" TagName="MaintenanceTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:MaintenanceTabCtrl runat="server" ID="MaintenanceTabCtrl" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Maintenance Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="server">
    Material Settings
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="server">
      <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/AdminSettings.aspxx"><i class="fa fa-arrow-left"></i> Return to Settings</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
   <style>
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
     <div class="form-group row">
     <%-- <label class="col-sm-2 control-label"> </label>--%>
          <div class="col-sm-12 form-inline">
          <asp:Button ID="btnAddform" runat="server" SkinID="btnDefault" Text="Add Material" ClientIDMode="Static" style="float:right;"/>
             <%--  <asp:HiddenField ID="hSector" runat="server" ClientIDMode="Static" Value="0"  />--%>
          </div>
         </div>

     <div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector11">&times;</button>
					<h4 class="modal-title"><span id="modeltitle1"> Add Material </span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Material</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtlable" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
              <asp:HiddenField ID="hfdid" runat="server" ClientIDMode="Static" />
              <label style="color:red" id="lblError1"></label>

              </div>
          </div>
              </div>
                      <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Cost</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="Textcost" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
            
              <label style="color:red" id="lblError2"></label>

              </div>
          </div>
              </div>
					
                    <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Markup</label>
          <div class="col-sm-10">            
			<asp:TextBox ID="Textmarkup" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>             
              <label style="color:red" id="lblError3"></label>

              </div>
          </div>
				</div>

        <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Price</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="Textprice" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
              <label style="color:red" id="lblError4"></label>

              </div>
          </div>
				</div>

				
				<div class="modal-footer" >
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector1" style="display:none;">Close</button>
					<button type="button" class="btn btn-info" id="btnSaveTask">Submit</button>
				</div>
			</div>
		</div>
	</div>
  </div>
    
    
    <script type="text/javascript">
        $(function () {
            bindGrid();
           
        });
    </script>
    <script type="text/javascript">
       
        function getfID() {
            return $("[id*=hfdid]").val();
        }
        function setfID(value) {
            $("[id*=hfdid]").val(value);
        }

        
        function showAjaxModalform() {
            $('#modal-7').modal('show', { backdrop: 'fade' });
        }
        function hideAjaxModalform() {
            $('#modal-7').modal('hide');
        }
        function clearFormData() {
            setfID('0');

            $("[id*=txtlable]").val('');
            $("[id*=Textcost]").val('');
            $("[id*=Textmarkup]").val('');
            $("[id*=Textprice]").val('');           
        }
        function bindGrid() {
            var id = $("[id*=hfdid]").val();
            GetFormData(id);
        }


        $("#btnAddform").click(function () {
            $("[id*=modeltitle1]").html("Add Material");
            setfID('0');
            clearFormData();
            showAjaxModalform();
            return false;
        });
        //button to save
        $("[id*=btnSaveTask]").click(function () {

            var fid = getfID();
            if (fid != '0') {
                UpdateServiceContactData();               
                hideAjaxModalform();
                displayMsg('lblmsg', 'Updated successfully', 'success');
            }
            else {
                debugger;
                AddFormData();
                debugger;
                hideAjaxModalform();
               // displayMsg('lblmsg', 'Added successfully', 'success');
            }

            return false;
        });

        //ajax fuction for add a meterials

        function AddFormData() {
            var id = getfID();
            var metrial = $("[id*=txtlable]").val();
            var tcost= $("[id*=Textcost]").val();
            var mark= $("[id*=Textmarkup]").val();
            var prices=$("[id*=Textprice]").val();  

            var dataObject = JSON.stringify({
                'id': id,
                'metrial': metrial,
                'tcost': tcost,
                'mark': mark,
                'prices': prices

            });
           
            $.ajax({
                type: "POST",
                url: "../../../WF/CustomerAdmin/Maintenance/services/MaterialSettingService.asmx/AddMeterial",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {

                   
                    debugger;
                    
                    bindGrid();
                    clearFormData();

                }
            });
        }

        //Get form data
        function editFormdata(id, material, tcost, mark, pric) {
            $("[id*=modeltitle1]").html("Edit Material");
            clearFormData();
            setfID(id);
            GetFormDatabyID();
            showAjaxModalform();
            return false;
        }

        function GetFormDatabyID() {
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
           
            try {
                $.ajax({
                    url: "../../../WF/CustomerAdmin/Maintenance/services/MaterialSettingService.asmx/BindFromDataByID",
                    type: "POST",
                    data: dataObject,
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        debugger;
                        var NewData = jQuery.parseJSON(data.d);


                        for (var i = 0; i < NewData.length; i++) {
                            //var CCID = NewData[i].CCID
                            var ID = NewData[i].ID
                           
                            var MaterialTitle = NewData[i].MaterialTitle;
                            var Cost = NewData[i].Cost;
                            var Markup = NewData[i].Markup;
                            var Price = NewData[i].Price;

                          
                            $("[id*=txtlable]").val(MaterialTitle);
                            $("[id*=Textcost]").val(Cost);
                            $("[id*=Textmarkup]").val(Markup);
                            $("[id*=Textprice]").val(Price);
                            
                           
                        }


                    }
                });
            }
            catch (e) {
                var err = e;
            }
        }
        //delete form date

        function deleteFormdata(id) {
            var r = ConfirmDelete();
            if (r == true) {
                setfID(id);
                deleteFormDatabyMethod();
            }
        }
        function ConfirmDelete() {
            return confirm("Are you sure you want to delete?");
        }

        function deleteFormDatabyMethod() {
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
            $.ajax({
                type: "POST",
                url: "../../../WF/CustomerAdmin/Maintenance/services/MaterialSettingService.asmx/FromDataDelete",
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
        //edit form data
        function UpdateServiceContactData() {
            var id = getfID();
            var metrial = $("[id*=txtlable]").val();
            var tcost = $("[id*=Textcost]").val();
            var mark = $("[id*=Textmarkup]").val();
            var prices = $("[id*=Textprice]").val();

            var dataObject = JSON.stringify({
                'id': id,
                'metrial': metrial,
                'tcost': tcost,
                'mark': mark,
                'prices': prices

            });
            $.ajax({
                type: "POST",
                url: "../../../WF/CustomerAdmin/Maintenance/services/MaterialSettingService.asmx/MeterialUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    bindGrid();
                }
            });
        }


    </script>
     <div class="row" >
    <div class="col-md-12">
     <table id="students1" class="table table-striped table-bordered"></table>
        </div>
         </div>
    <script type="text/javascript">
        function GetFormData(id) {
            try {
                $.ajax({
                    url: "../../../WF/CustomerAdmin/Maintenance/services/MaterialSettingService.asmx/BindFromData",
                    type: "POST",
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        debugger;
                        var NewData = jQuery.parseJSON(data.d);
                        var x = "<thead><tr>"
                            + "<th style='width:10%;color:white;'></th>"
                            + "<th  style='width:20%;color:white;'>Materials</th>"
                            + "<th  style='width:10%;color:white;'>Cost</th>"
                            + "<th style='width:20%;color:white;'>Markup</th>"
                            + "<th style='width:20%;color:white;'>Price</th>"
                            + "<th style='width:10%;color:white;'></th> "
                            + "</tr></thead>";

                        x = x + "<tbody>"

                        for (var i = 0; i < NewData.length; i++) {
                            //var CCID = NewData[i].CCID
                            var ID = NewData[i].ID
                            var material = NewData[i].material;
                            var tcost = NewData[i].tcost;
                            var mark = NewData[i].mark;
                            var pric = NewData[i].pric;


                            x = x + "<tr><td>" + ButtonHtml(ID, material, tcost, mark, pric)
                                + "</td><td>" + material
                                + "</td><td style='text-align:right'>" + tcost
                                + "</td><td style='text-align:right'>" + mark
                                + "</td><td style='text-align:right'>" + pric
                                + "</td><td>" + ButtondeleteHtml(ID)
                                + "</td></tr>";
                        }

                        x = x + "</tbody>";
                        $("#students1").empty();
                        $("#students1").append(x);
                        BindTable();
                        $("#students1").removeClass("no-footer");
                        //setStatusBackColor();
                    }
                });
            }
            catch (e) {
                var err = e;
            }
        }

        function BindTable() {
            var table = $('#students1').DataTable({
                'Ordering': true,
                "order": [[0, "asc"]],
                'paging': true,
                'bFilter': false,
                'lengthChange': false,
                'destroy': true,                
                //'rowReorder': true,
                "columnDefs": [{
                    "targets": 0, "orderable": false
                },
                    
                ]
            });

         
        }

        function DisplayValues(ListValue, TypeOfField) {
            var HtmlText = "";
            if (TypeOfField != 'Textbox') {
                HtmlText = "<ul>"
                var array = ListValue.split(',');

                $.each(array, function (index, value) {
                    HtmlText = HtmlText + "<li>" + value + "</li>";
                });
                HtmlText = HtmlText + "</ul>";
            }
            else {
                HtmlText = ListValue;
            }
            return HtmlText;
        }

        function ButtonHtml(Id, material, tcost, mark, pric) {
            var HtmlText = " <a target='_blank' id=Link" + Id + " onclick=editFormdata('" + Id + "') style=' font-weight: bold'><span class='fa-edit' style='font-size:1.2em'></span></a>";
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
  

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
