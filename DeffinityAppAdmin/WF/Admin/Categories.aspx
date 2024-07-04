<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Categories" EnableEventValidation="false" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
      <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Categories by Sector
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <style>
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

     <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Category</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlCategory" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hCategory" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the category?');"></asp:LinkButton>
              </div>
          </div>
        </div>
      <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Sub Category</label>
          <div class="col-sm-10 form-inline">
            <asp:DropDownList ID="ddlSubCategory" CssClass="ddl1" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
              <asp:HiddenField ID="hSubCategory" runat="server" ClientIDMode="Static" Value="0"  />
              <asp:LinkButton ID="btnAddSubCategory"  runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnEditSubCategory" runat="server" SkinID="BtnLinkEdit" ClientIDMode="Static"></asp:LinkButton>
              <asp:LinkButton ID="btnDelSubCategory" runat="server" SkinID="BtnLinkDelete" ClientIDMode="Static" OnClientClick="return confirm('Do you want to delete the sub category?');"></asp:LinkButton>
              </div>
          </div>
        </div>
    <div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseCopy1">&times;</button>
					<h4 class="modal-title">Copy Sector to </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Sector</label>
          <div class="col-sm-10">
            <div style="height:150px;border-style:solid;overflow:scroll;">
								<div id="ddlCopySector" ></div>
                </div>
              <asp:HiddenField ID="hcopyid" runat="server" ClientIDMode="Static" />
             
              <label style="color:red" id="lblErrorCopy"></label>

              </div>
          </div>
              </div>
					
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseCopy" style="display:none;">Close</button>
					<button type="button" class="btn btn-info" id="btnSubmitCopy" style="background-color">Submit</button>
				</div>
			</div>
		</div>
	</div>

    <div class="modal fade" id="modal-6" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector1">&times;</button>
					<h4 class="modal-title"><span id="modeltitle"> Add Sector </span> </h4>
				</div>
				
				<div class="modal-body">
				  <div class="form-group">
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
        $(document).ready(function () {
            
            $("[id*=btnSubmitCopy]").click(function () {

                var fromid = '';
                var toid = '';

                CopySectorData();
                hideCopyModal()

                return false;
            });


            $("[id*=btnCopy]").click(function () {

                SetCopyData();
                showCopyModal()

                return false;
            });
        });

        function CopySectorData() {

            var toid = '';
            $("input[name='chk']:checked").each(function (index, obj) {
                // loop all checked items
                toid = toid + this.value +",";
            });

            var dataObject = JSON.stringify({
                'fromid': $("[id*=ddlSector]").val(),
                'toid': toid //$("[id*=ddlCopySector]").val()
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/CopySection",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;

                    displayMsg('lblmsg', 'Copied successfully', 'success');
                    
                }
            });
        }
        function showCopyModal() {
            $('#modal-7').modal('show', { backdrop: 'fade' });
        }
        function hideCopyModal() {
            $('#modal-7').modal('hide');
        }

        function SetCopyData() {
            //var id = $("[id$='hcid']").val();

            //if (id == "")
            //    id = "0";
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SectionsGet",
                //data: "{id:'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var ddlCustomers = $("[id*=ddlCopySector]");
                    debugger;
                    ddlCustomers.empty();
                    $.each(r.d, function () {
                        ddlCustomers.append("<input type='checkbox' name='chk' value='" + this['Value']+"'>" + this['Text']+"<br>");
                        //ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                    //$("[id*=hCategory]").val('0');
                    //setSectorDropdownValue();
                    
                }
            });
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
                 return false;
             });
         });
     </script>

    <script type="text/javascript">
       
        $(document).ready(function () {
            $("[id*=hid]").val('0');
            $("[id*=htype]").val('1');

            $("[id*=btnAddSector]").click(function () {
                $("[id*=modeltitle]").html("Add Sector");
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('1');
                setPopDefault();
                showAjaxModal();
                return false;
            });

            $("[id*=btnEditSector]").click(function () {
                $("[id*=modeltitle]").html("Edit Sector");
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('1');
                if ($("[id*=htype").val() == '1') {
                    $("[id*=hid").val($("[id*=ddlSector]").val());
                    if ($("[id*=hid").val() != "0") {
                        $("[id*=txtName]").val($("#ddlSector option:selected").text());
                        showAjaxModal();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select sector', 'error');
                    }
                }
                return false;
            });

            $("[id*=btnDelSector]").click(function () {
                $("[id*=hid]").val('0');
                $("[id*=htype]").val('1');
                if ($("[id*=htype").val() == '1') {
                    $("[id*=hid").val($("[id*=ddlSector]").val());
                    if ($("[id*=hid").val() != "0") {
                        DeleteSectorContactData();
                        displayMsg('lblmsg', 'Deleted successfully', 'success');
                        setPopDefault();
                    }
                    else {
                        displayMsg('lblmsg', 'Please select sector', 'error');
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
        $(function () {

            SetSectorContactData();
            SetCategoryContactData();
            SetSubCategoryContactData();
            $("[id*=ddlSector]").change(function () {
                $("[id*=hSector").val($(this).val());

                SetCategoryContactData();
                SetSubCategoryContactData();
            });
        });

        function setSectorDropdownValue() {
            if ($("[id*=hSector").val() != '') {
                $("[id*=ddlSector]").val($("[id*=hSector").val());
            }
        }
        function SetSectorContactData() {
            //var id = $("[id$='hcid']").val();

            //if (id == "")
            //    id = "0";
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SectionsGet",
                //data: "{id:'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var ddlCustomers = $("[id*=ddlSector]");
                    debugger;
                    ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(r.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                    $("[id*=hCategory]").val('0');
                   setSectorDropdownValue();
                }
            });
        }

        function AddSectorContactData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SectionsAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    //alert(t.Value);
                    $("[id*=hSector]").val(t.Value);
                   //Bind dropdown
                    SetSectorContactData();
                    SetCategoryContactData();
                    SetSubCategoryContactData();
                   //set added value
                   // alert($("[id*=hSector]").val());
                   // setSectorDropdownValue();
                }
            });
        }

        function UpdateSectorContactData() {
            //var id = $("[id$='hcid']").val();
            var name;
            var type;
            var id;

            name = getName();
            id = getID();
            var dataObject = JSON.stringify({
                'name': name,
                'id':id
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SectionsUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    //alert(t.Value);
                    $("[id*=hSector]").val(t.Value);
                    //Bind dropdown
                    SetSectorContactData();
                    //set added value
                    setSectorDropdownValue();
                }
            });
        }

        function DeleteSectorContactData() {
            //var id = $("[id$='hcid']").val();
            var id;
            var dataObject = JSON.stringify({
                'id': getID()
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SectionsDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    //Bind dropdown
                    SetSectorContactData();
                    $("[id*=hSector]").val('0');
                    setSectorDropdownValue();
                    SetCategoryContactData();
                    SetSubCategoryContactData();
                }
            });
        }

        function SetdropdownsValue() {
            if ($("[id*=hSector]").val().trim() != "") {
                $("[id*=ddlSector] option").each(function () {
                    if ($(this).val() == $("[id*=hSector]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
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
            var id = $("[id*=ddlSector]").val();
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
            debugger;
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/Admin/Services/AdminServices.asmx/CategoryGet",
                    data: "{typeid:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlCategory]");
                        debugger;
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
                debugger;
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }

        function AddCategoryContactData() {
            var typeid = $("[id*=ddlSector]").val();
            var name = $("[id*=txtName]").val();
           
            var dataObject = JSON.stringify({
                'typeid': typeid,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/CategoryAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
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
                url: "../../WF/Admin/Services/AdminServices.asmx/CategoryUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
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
                url: "../../WF/Admin/Services/AdminServices.asmx/CategoryDelete",
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
            debugger;
            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/Admin/Services/AdminServices.asmx/SubCategoryGet",
                    data: "{category:'" + category + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlSubCategory]");
                        //debugger;
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
                //debugger;
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }

        function AddSubCategoryContactData() {
            //var id = $("[id$='hcid']").val();
            var category = $("[id*=ddlCategory]").val();
            var name = $("[id*=txtName]").val();
            var dataObject = JSON.stringify({
                'category': category,
                'name': name
            });
            $.ajax({
                type: "POST",
                url: "../../WF/Admin/Services/AdminServices.asmx/SubCategoryAdd",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
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
                url: "../../WF/Admin/Services/AdminServices.asmx/SubCategoryUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
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
                url: "../../WF/Admin/Services/AdminServices.asmx/SubCategoryDelete",
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
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    
</asp:Content>
