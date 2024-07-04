<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.AddNewUser" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
     Users
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_title" runat="server">
    Users
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
     <div class="form-group">
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
     <div class="form-group">
    
          <div class="col-sm-12 form-inline">
          <asp:Button ID="btnAddform" runat="server" SkinID="btnDefault" Text="Add User" ClientIDMode="Static" style="float:right;"/>
             <%--  <asp:HiddenField ID="hSector" runat="server" ClientIDMode="Static" Value="0"  />--%>
          </div>
         </div>

     <div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector11">&times;</button>
					<h4 class="modal-title"><span id="modeltitle1">Add New User</span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Name</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtlable" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
              <asp:HiddenField ID="hfdid" runat="server" ClientIDMode="Static" />
              <label style="color:red" id="lblError1"></label>

              </div>
          </div>
              </div>
                      <div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Username</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="Textcost" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
            
              <label style="color:red" id="lblError2"></label>

              </div>
          </div>
              </div>
					
                    <div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Password</label>
          <div class="col-sm-10">            
			<asp:TextBox ID="Textmarkup" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>             
              <label style="color:red" id="lblError3"></label>

              </div>
          </div>
				</div>

        <div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Partner</label>
          <div class="col-sm-10">
            
			<%--<asp:TextBox ID="Textprice" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>--%>
             
               <asp:DropDownList ID="ddlTimeYear" CssClass="ddl1"  runat="server" ClientIDMode="Static" SkinID="txt_90" Width="350px" Height="30px"></asp:DropDownList>
              <asp:HiddenField ID="hTimeYear" runat="server" ClientIDMode="Static" Value="0"  />
              <label style="color:red" id="lblError4"></label>
             
              <asp:Label SkinID="TextBox1" ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>

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
            $("[id*=modeltitle1]").html("Add User");
            setfID('0');
            clearFormData();
            showAjaxModalform();
            controldropdownHide();
            GetPatnerData();
            return false;
        });
        //button to save
        $("[id*=btnSaveTask]").click(function () {

            var fid = getfID();
            if (fid != '0') {
                UpdateServiceContactData();               
                hideAjaxModalform();
               // displayMsg('lblmsg', 'Updated successfully', 'success');
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

        //ajax fuction for add a user

        function AddFormData() {
            var id = getfID();
            var DisplayName = $("[id*=txtlable]").val();
            var Username= $("[id*=Textcost]").val();
            var Password= $("[id*=Textmarkup]").val();
            var PartnerID = $("[id*=hTimeYear]").val();
          
            var dataObject = JSON.stringify({
                'id': id,
                'DisplayName': DisplayName,
                'Username': Username,
                'Password': Password,
                'PartnerID': PartnerID,
            });
           // alert(dataObject)
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AddNewUserServices.asmx/AddUser",
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
        function controldropdownHide() {
            $("[id*=TextBox1]").hide();
        }
        function controldropdownShow() {
            $("[id*=TextBox1]").show();
        }
        function editFormdata(id, DisplayName, Username, PartnerName) {
            $("[id*=modeltitle1]").html("Edit User");
            clearFormData();
            setfID(id);
            GetFormDatabyID();
            showAjaxModalform();
            controldropdownShow();
            return false;
        }

        function GetFormDatabyID() {
            GetPatnerData();
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
            
            try {
                $.ajax({
                    url: "../../WF/Admin/Services/AddNewUserServices.asmx/BindFromDataByID",
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

                            var DisplayName = NewData[i].DisplayName;
                            var Username = NewData[i].Username;
                            var Password = NewData[i].Password;
                            var PartnerName = NewData[i].PartnerName;
                            var partnerID = NewData[i].partnerID;

                            $("[id*=txtlable]").val(DisplayName);
                            $("[id*=Textcost]").val(Username);
                            $("[id*=Textmarkup]").val('');
                            
                            $("[id*=hTimeYear]").val(partnerID);
                            debugger;
                            setCategoryDropdownValue();


                        }
                       // GetPatnerData();


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
                url: "../../WF/Admin/Services/AddNewUserServices.asmx/FromDataDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    bindGrid();
                   // displayMsg('lblmsg', 'Deleted successfully', 'success');
                }
            });
        }
        //edit form data
        function UpdateServiceContactData() {
            var id = getfID();
            var DisplayName = $("[id*=txtlable]").val();
            var Username = $("[id*=Textcost]").val();
            var Password = $("[id*=Textmarkup]").val();
            var PartnerID = $("[id*=hTimeYear]").val();
            var PartnerName = $("[id*=ddlTimeYear]").val();

            var dataObject = JSON.stringify({
                'id': id,
                'DisplayName': DisplayName,
                'Username': Username,
                'Password': Password,
                'PartnerID': PartnerID,
                'PartnerName': PartnerName,

            });
          
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AddNewUserServices.asmx/UserUpdate",
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
        ////  Get  patener data ..
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


        function GetPatnerData() {
            var id = $("[id*=hTimeYear]").val();
            
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
           
            debugger;
            if (id = "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/Admin/Services/AddNewUserServices.asmx/GetPateners",
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
                    url: "../../WF/Admin/Services/AddNewUserServices.asmx/BindFromData",
                    type: "POST",
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        debugger;
                        var NewData = jQuery.parseJSON(data.d);
                        var x = "<thead><tr>"
                            + "<th style='width:5%;'></th>"
                            + "<th  style='width:20%;'>Name</th>"
                            + "<th  style='width:20%;'>Username</th>"                                                      
                            + "<th  style='width:20%;'>Partner</th>"                                                      
                            + "<th style='width:5%;'></th> "
                            + "</tr></thead>";

                        x = x + "<tbody>"

                        for (var i = 0; i < NewData.length; i++) {
                            //var CCID = NewData[i].CCID
                            var ID = NewData[i].ID
                            var DisplayName = NewData[i].DisplayName;
                            var Username = NewData[i].Username;
                            var PartnerName = NewData[i].PartnerName;
                           


                            x = x + "<tr><td>" + ButtonHtml(ID, DisplayName, Username, PartnerName)
                                + "</td><td>" + DisplayName
                                + "</td><td>" + Username                                                        
                                + "</td><td>" + PartnerName                                                        
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

        function ButtonHtml(Id, DisplayName, Username, PartnerName) {
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

  

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
