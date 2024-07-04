<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_FLS_ctrl" CodeBehind="FLS.ascx.cs" %>
<%@ Register Src="~/WF/DC/Controls/FlsHistory.ascx" TagName="FlsHistory" TagPrefix="uc1" %>
<%@ Register Src="~/WF/DC/Controls/NotesCtrl.ascx" TagName="Notes" TagPrefix="uc3" %>
<%@ Register Src="~/WF/DC/Controls/QuickSearchCtrl.ascx" TagName="QuickSearchCtrl" TagPrefix="QSCtrl" %>
<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />

<div class="row" >
   
    </div>

  
<%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
<%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>--%>
<%--<script src="../js/jquery-1.3.2.js" type="text/javascript"></script>--%>
<style type="text/css">
    /*.lab {
        width: 250px;
        float: left;
        margin-right: 5px;
    }

    .FieldCls {
        width: 250px;
        display: block;
    }

    .TxtCount {
        display: none;
    }

    .auto-style1 {
        width: 10px;
    }

    .btn {
        margin-bottom: 0px;
    }
    .row {
    margin-left: -15px;
    margin-right: -15px;
    margin-top:15px;
}*/
    /*table.dataTable thead tr {
  background-color: #40bbea;
  color:#ffffff;
}*/
</style>
<style>
    .clsred{
        color:red;
    }
    .ralert-danger {
        background-color: #cc3f44;
        border-color: #cc3f44;
        color: #ffffff;
        display: inline-block;
    }

    .ralert {
        padding: 15px;
        margin-bottom: 18px;
        border: 1px solid transparent;
        border-radius: 0px;
    }
</style>
<style>
    .btn.btn-white.btn-icon-standalone i {
    background-color: #FF6600;
    border-right-color: #e6e6e6;
}
</style>
<script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>


<script type="text/javascript">


    function NameDropdownBind() {
        //BindValues();
        //FieldConfig();
        //$("#ddlName").change(function (){ 
        //    BindValues();
        //    GetTotalEvents();
        //});
        var Q_callid = GetParameterValues('callid');
        if (Q_callid == undefined) {
            $("#btnPop_open").show();
        }
        else {
            $("#btnPop_open").hide();
        }
    }

    $().ready(function () {
        NameDropdownBind();
    });
    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0].toLocaleLowerCase() == param.toLowerCase()) {
                return urlparam[1];
            }
        }
    }
    $().ready(function () {
        //BindValues();

        $('#pnlResolution').hide();
        //ValidatorEnable(myVal, false);
    });
    function onCallidPopulated() {

        //$get('ddlSubject').disabled = true;
        //$get('ddlCompany').disabled = true;
        //$get('ddlName').disabled = true;

        //       var assignedName = $("#ddlAssignedtoName").val();
        //        if (assignedName != 0) {
        //            $get('ddlAssignedtoName').disabled = true;
        //        }
    }
    $().ready(function () {

        $("#ddlStatus").change(function () {

            //var myVal = 
            //var rvtimeworked = document.getElementById("<%=rfvTimeWorked.ClientID%>");
            //var retimeworked = document.getElementById("<%=ReTimeWorked.ClientID%>");
            var status = $("#ddlStatus option:selected").text();
            if (status == "Resolved") {
                $('#pnlResolution').show();
                //ValidatorEnable(myVal, true);
                //  ValidatorEnable(rvtimeworked, true);
                // ValidatorEnable(retimeworked, true);
            }
            else if (status == "Closed") {
                //ValidatorEnable(rvtimeworked, true);
                //ValidatorEnable(retimeworked, true);
                //Sorry but you cannot amend a ticket that has been closed
            }
            else {
                $('#pnlResolution').hide();
                //ValidatorEnable(myVal, false);
                // ValidatorEnable(rvtimeworked, false);
                // ValidatorEnable(retimeworked, false);
            }

            // fnPageload();
            SetSubcategoryID();
            SetAssignedTechnician();
            //set value
            GetTotalEvents();
        });
    });

    function onPopulated() {
        $get("ddlTypeofRequest").disabled = true;
        var id = $('#htid').val();

        if (id == 0) {
            $get('ddlStatus').disabled = true;
        }
        if (id != 0) {

            //var rvtimeworked = document.getElementById("<%=rfvTimeWorked.ClientID%>");
            //var retimeworked = document.getElementById("<%=ReTimeWorked.ClientID%>");
            //ImgBtnRnameAdd
            $("#ImgBtnRnameAdd").hide();
            var status = $("#ddlStatus option:selected").text();
            if (status == "Resolved") {
                $('#pnlResolution').show();
                //ValidatorEnable(myVal, true);
                // ValidatorEnable(myValTimeworked, true);
                $("#txtNotes").attr('readonly', 'readonly');
                $("#txtCustomerRef").attr('readonly', 'readonly');
                $("#txtSeheduledDateTime").attr('readonly', 'readonly');
                $("#txtScheduledTime").attr('readonly', 'readonly');
                $("#imgSeheduledDate").hide();
                $("#PnlFileUpload").attr('style', 'display:none');
                $get('ddlSite').disabled = true;
                $get('ddlAssignedtoDept').disabled = true;
                $get('ddlAssignedtoName').disabled = true;
                $get('ddlSourceOfRequest').disabled = true;
                $get('ddlRequestType').disabled = true;
                $get('ddlCategory').disabled = true;
                $get('ddlSubCategory').disabled = true;
                //ddlSubCategory
                // ValidatorEnable(rvtimeworked, true);
                //ValidatorEnable(retimeworked, true);
            }
            //if the status is close disable all the controls
            else if (status == "Closed") {
                $("#txtNotes").attr('readonly', 'readonly');
                //txtResolution
                $("#txtResolution").attr('readonly', 'readonly');
                //po number
                $("#txtTimeWorked").attr('readonly', 'readonly');
                //time worked
                $("#txtPOnumber").attr('readonly', 'readonly');
                //PnlFileUpload
                $("#PnlFileUpload").attr('style', 'display:none');
                $get('ddlSite').disabled = true;
                $get('ddlAssignedtoDept').disabled = true;
                $get('ddlAssignedtoName').disabled = true;
                // $get('ddlStatus').disabled = true;

                var AdminId = $("#hfforAdminAccess").val();
                if (AdminId == 1) {
                    $get('ddlStatus').disabled = false;
                }
                else {
                    $get('ddlStatus').disabled = true;
                }

                $get('ddlSourceOfRequest').disabled = true;
                $get('ddlRequestType').disabled = true;
                $get('ddlCategory').disabled = true;
                $get('ddlSubCategory').disabled = true;
                //Customer ref
                $("#txtCustomerRef").attr('readonly', 'readonly');
                $("#imgSeheduledDate").hide();
                $("#txtSeheduledDateTime").attr('readonly', 'readonly');
                $("#txtScheduledTime").attr('readonly', 'readonly');
                //$get('ddlSubject').disabled = true;
                //$get('ddlCompany').disabled = true;
                //$get('ddlName').disabled = true;
                // ValidatorEnable(rvtimeworked, true);
                //ValidatorEnable(retimeworked, true);
            }
            else {
                $('#pnlResolution').hide();
                //ValidatorEnable(myVal, false);
                //ValidatorEnable(rvtimeworked, false);
                // ValidatorEnable(retimeworked, false);
            }
            $find("ccdSub").add_populated(onCallidPopulated);
            $find("ccdCom").add_populated(onCallidPopulated);
            //$find("ccdNa").add_populated(onCallidPopulated);
            $find("ccdAn").add_populated(onCallidPopulated);
            //$find("ccdkey").add_populated(onCallidPopulated);
            //$find("ccdCate").add_populated(onCallidPopulated);
            //$find("ccdSubcate").add_populated(onCallidPopulated);
            //$("#txtDetails").attr('readonly', 'readonly');
        }
    }

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pageLoad);
    function pageLoad(sender, args) {
        fnPageload();
        SetpnlAddress();
    }
    function fnPageload() {

       // $find("ccdNa").add_populated(onPopulated);
        $find("ccdT").add_populated(onPopulated);
        $find("ccdS").add_populated(onPopulated);
        $find("ccdS").add_populated(onPopulated);
        //$find("ccdkey").add_populated(onPopulated);
        var behavior = $find('<%=ccdSubCategory.BehaviorID %>');
        if (behavior != null) {
            behavior.add_populated(function () {
                SetSubcategoryID();
            });
        }

        var behavior = $find('<%=ccdAssignedName.BehaviorID %>');
        if (behavior != null) {
            behavior.add_populated(function () {
                SetAssignedTechnician();
            });
        }
    }
    
    function BindValuesNew() {
        var id = $("[id$='ddlContacts']").val();//$('#ddlName').val();

        if (id != "0") {
            $("#txtReqTelNo").html("");
            $("#txtReqEmailAddress").html("");
            $("#txtRequestersDepartment").html("");
            $("#txtRequestersJobTitle").html("");
            $("#txtRequestersAddress").html("");
            $("#txtRequestersCity").html("");
            $("#txtRequestersTown").html("");
            $("#txtRequestersPostcode").html("");
            //$("#txtLocation").html("");
            try {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetails",
                    data: "{id:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                        document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                        document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                        document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                        document.getElementById('txtRequestersAddress').value = msg.d.Address;
                        document.getElementById('txtRequestersCity').value = msg.d.City;
                        document.getElementById('txtRequestersTown').value = msg.d.Town;
                        document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                        //document.getElementById('txtLocation').value = msg.d.Location;



                    }
                });
            }
            catch (err) {
                var er = err;
            }
            //            $("#txtReqTelNo").html("");
            //            $.ajax({
            //                type: "POST",
            //                url: "../webservices/DCServices.asmx/GetReqTelNo",
            //                data: "{ID:'" + ID + "'}",
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (msg) {
            //                    document.getElementById('txtReqTelNo').value = msg.d;
            //                }
            //            });

            //            $("#txtReqEmailAddress").html("");
            //            $.ajax({
            //                type: "POST",
            //                url: "../webservices/DCServices.asmx/GetReqEmail",
            //                data: "{ID:'" + ID + "'}",
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (msg) {
            //                    document.getElementById('txtReqEmailAddress').value = msg.d;
            //                }
            //            });

        }
        document.getElementById('txtReqTelNo').value = "";
        document.getElementById('txtReqEmailAddress').value = "";
        document.getElementById('txtRequestersDepartment').value = "";
        document.getElementById('txtRequestersJobTitle').value = "";
        document.getElementById('txtRequestersJobTitle').value = "";
        document.getElementById('txtRequestersAddress').value = "";
        document.getElementById('txtRequestersCity').value = "";
        document.getElementById('txtRequestersTown').value = "";
        document.getElementById('txtRequestersPostcode').value = "";
        //document.getElementById('txtLocation').value = "";
    }

    function BindValues() {
        var id = $("[id$='hcid']").val();//$('#ddlName').val();

        if (id != "0") {
            $("#txtReqTelNo").html("");
            $("#txtReqEmailAddress").html("");
            $("#txtRequestersDepartment").html("");
            $("#txtRequestersJobTitle").html("");
            $("#txtRequestersAddress").html("");
            $("#txtRequestersCity").html("");
            $("#txtRequestersTown").html("");
            $("#txtRequestersPostcode").html("");
            //$("#txtLocation").html("");
            try {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetails",
                    data: "{id:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                        document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                        document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                        document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                        document.getElementById('txtRequestersAddress').value = msg.d.Address;
                        document.getElementById('txtRequestersCity').value = msg.d.City;
                        document.getElementById('txtRequestersTown').value = msg.d.Town;
                        document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                        //document.getElementById('txtLocation').value = msg.d.Location;



                    }
                });
            }
            catch (err) {
                var er = err;
            }
            //            $("#txtReqTelNo").html("");
            //            $.ajax({
            //                type: "POST",
            //                url: "../webservices/DCServices.asmx/GetReqTelNo",
            //                data: "{ID:'" + ID + "'}",
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (msg) {
            //                    document.getElementById('txtReqTelNo').value = msg.d;
            //                }
            //            });

            //            $("#txtReqEmailAddress").html("");
            //            $.ajax({
            //                type: "POST",
            //                url: "../webservices/DCServices.asmx/GetReqEmail",
            //                data: "{ID:'" + ID + "'}",
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (msg) {
            //                    document.getElementById('txtReqEmailAddress').value = msg.d;
            //                }
            //            });

        }
        document.getElementById('txtReqTelNo').value = "";
        document.getElementById('txtReqEmailAddress').value = "";
        document.getElementById('txtRequestersDepartment').value = "";
        document.getElementById('txtRequestersJobTitle').value = "";
        document.getElementById('txtRequestersAddress').value = "";
        document.getElementById('txtRequestersCity').value = "";
        document.getElementById('txtRequestersTown').value = "";
        document.getElementById('txtRequestersPostcode').value = "";
        //document.getElementById('txtLocation').value = "";
    }
    $().ready(function () {
        $("#ddlCompany").change(function () {
            $('#txtReqTelNo').val('');
            $('#txtReqEmailAddress').val('');
            $("#txtRequestersDepartment").val('');
            $("#txtRequestersJobTitle").val('');
            // $("#txtLocation").val('');
        });
    });
</script>
<script>
    $(document).ready(function () {

        $("#MainContent_MainContent_FlsForm_ddlContacts").change(function () {
            BindValuesNew();
        });

        $("#QSearch").hide();
        $("#BtnS").click(function () {
            $('#QSearch').show();
            $('#BtnS').hide();
            return false;
        });
    });
</script>

<script type="text/javascript"> 
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fieldConfig);
    function fieldConfig() {
        $('<%= li_ddlCompany.ClientID %>').insertBefore('<%= DivSourceOfRequest.ClientID %>');

        //alert($('#div_left').children('div').length);//$('#div_contains').children('div').length

        var Count = $("#lblFieldsCount").val();

        for (i = 0; i <= Count; i++) {
            var d = $("#MainDivfd").find("." + i);

            if ($(d).css("float") == 'left') {
                $("#" + d.attr('id')).insertBefore("#divl1");

                if (i == 4) {

                    $("#pnlKeyContacts").insertBefore("#divl1");
                }
                if (i == Count) {

                    $("#pnlResolution").insertBefore("#divl1");
                }
            }
            else {

                $("#" + d.attr('id')).insertBefore("#divr1");

               
                if (i == 5) {
                    $("#pnlEquipments").insertBefore("#divr1");
                    $("#pnlWarrantyExpiryDate").insertBefore("#divr1");
                }

            }
        }

        for (i = 0; i <= Count; i++) {
            var d1 = $("#DivfdAdditional").find("." + i);
            if (d1 != '') {
                if ($(d1).css("float") == 'left') {
                    $("#" + d1.attr('id')).insertBefore("#divl1Additional");
                }
                else {
                    $("#" + d1.attr('id')).insertBefore("#divr1Additional");
                }
            }
        }

        //$(".fdAdditional").find('div').each(function (i, el) {
        //    var s1 = $(el).get(0).id;

        //    if ($(el).css("float") == 'left') {

        //        $("#" + s1).insertBefore("#divl1Additional");
        //    }
        //    else {

        //        $("#" + s1).insertBefore("#divr1Additional");
        //    }
        //});
    }
    //function FieldConfig() {
    $(window).load(function () {

        fieldConfig();

    });
     // }


</script>




<script type="text/javascript">
    $(document).ready(function () {
        $("p").click(function () {
        });
    });
</script>
<script language="javascript" type="text/javascript">
    function compareDate(fromdate, todate) {
        var sTime = moment(fromdate, "MM/dd/yyyy");
        var tTime = moment(todate, "MM/dd/yyyy");

        return tTime.isBefore(sTime)
    }
    function comparetime(fromtime, totime) {


        var sTime = moment(fromtime, "HH:mm");
        var tTime = moment(totime, "HH:mm");


        if (fromtime == "")
            return false;
        else if (tTime == "")
            return false
        else
            return tTime.isBefore(sTime)
    }
    //Fade out buttons when clicked but only if page validate
    $().ready(function () {
        $('#lbl_loading').hide();
        $('#div_buttons').show();
        $('#card shadow-sm mb-6').click(function (e) {

            if (Page_ClientValidate('fls')) {

                //prefered date1 compare time
                var p1 = comparetime($("[id$='txtScheduledTime']").val(), $("[id$='txtScheduledToTime']").val());

                if (p1 == false) {
                    var p2 = comparetime($("[id$='txtPreferreddatetime2']").val(), $("[id$='txtPreferreddatetimeto2']").val());
                    if (p2 == false) {
                        //compare preferend date & preferend date1
                        var p3 = comparetime($("[id$='txtPreferreddatetime3']").val(), $("[id$='txtPreferreddatetimeto3']").val());
                        if (p3 == false) {
                            $('#div_buttons').fadeOut('fast');
                            $('#lbl_loading').fadeIn('slow');
                        }
                        else {
                            $("[id$='lblMsg']").show();
                            $("[id$='lblMsg']").html('Please enter valid Preferred 3 to time');
                            return false;
                        }
                    }
                    else {
                        $("[id$='lblMsg']").show();
                        $("[id$='lblMsg']").html('Please enter valid Preferred 2 to time');
                        return false;
                    }
                }
                else {
                    //$("[id$='lblMsg']").show();
                    //$("[id$='lblMsg']").html('Please enter valid Preferred to time');
                    return true;
                }
            }
            else { return false; }
        });
    });
</script>

<script>

    function showAjaxModal() {
        $('#modal-6').modal('show', { backdrop: 'fade' });
    }
    function hideAjaxModal() {
        $('#modal-6').modal('hide');
    }

    function clearCustomerFields() {
        $("[id*=txt_c_name]").val('');
        $("[id*=txt_c_email]").val('');
        $("[id*=txt_c_cell]").val('');
        $("[id*=txt_c_address]").val('');
        $("[id*=txt_c_city]").val('');
        $("[id*=txt_c_state]").val('');
        $("[id*=txt_c_zipcode]").val('');
    }

    function AddNewCustomer() {
        //var id = $("[id$='hcid']").val();
        var name;
        var type;
        var id;

        name = $("[id*=txt_c_name]").val();
        var dataObject = JSON.stringify({
            'name': $("[id*=txt_c_name]").val(),
            'email': $("[id*=txt_c_email]").val(),
            'cell':$("[id*=txt_c_cell]").val(),
            'address': $("[id*=txt_c_address]").val(),
            'city':$("[id*=txt_c_city]").val(),
            'state': $("[id*=txt_c_state]").val(),
            'zipcode': $("[id*=txt_c_zipcode]").val(),
        });
        $.ajax({
            type: "POST",
            url: "../../WF/DC/webservices/DCServices.asmx/addNewCustomer",
            data: dataObject,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (r) {
                
                //var t = r.d;
                clearCustomerFields();
                
                //alert(t.Value);
                //$("[id*=hSector]").val(t.Value);
                hideAjaxModal();

                debugger;
                var t1 = JSON.parse(r.d);
                //var tp = JSON.stringify(r.d);
                var tp1 = r.d.result;
                debugger;
                var str = t1.result;

                var rid = t1.rid;
                debugger;
                var n = str.includes("Added");
                location.reload();
                if (n)
                {
                    //$("[id$='lblMsg']").show();
                    //$("[id$='lblMsg']").html("Client added successfully");
                    debugger;
                    $("#tsearch").val(name);
                    //$("[id*='txtSearch']").val(name);
                    $("[id$='haddressid']").val(rid);
                    debugger;
                   // Search_load();
                    GetAddressSearch();
                   
                    //$('#slist tbody tr:eq(0)').addClass('selected');
                    loadByAddressID();
                    

                }
                else {
                   // $("[id$='lblMsg']").show();
                   // $("[id$='lblMsg']").html("Email address already exists").delay(5000).fadeOut('slow');
                }


               
               
            }
        });
    }

    $().ready(function () {
       

        
        $("[id*=btnAddCustomer]").click(function () {
            clearCustomerFields();
            showAjaxModal();

            return false;

        });

        $("[id*=btnSubmitCustomer]").click(function () {

            if (!Page_ClientValidate("cg"))
                return false;
            else
            
            AddNewCustomer();

            return false;

        });

        //btnSubmitCustomer

    });


</script>

<div class="modal fade" id="modal-6" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
                    <h4 class="modal-title"><span id="modeltitle" > Add Project Co-ordinator </span></h4>
					<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true" >&times;</button>--%>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="btnCloseSector1"></button>
					
				</div>
				
				<div class="modal-body">

                    <div class="row mb-6">
                        <asp:ValidationSummary ID="cvalsum" runat="server" ValidationGroup="cg" />
                    </div>

				  <div class="row  mb-6">
     
          <div class="col-sm-2">
          <label class="control-label"> Name </label> <span class="clsred">*</span> 
              </div>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_name" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rvcname" runat="server" ControlToValidate="txt_c_name" ValidationGroup="cg" ErrorMessage="Please enter name" Display="None"></asp:RequiredFieldValidator>
            

              </div>
        
              </div>

                     <div class="row  mb-6">
      
           <div class="col-sm-2">
          <label class="control-label"> Email </label> <span class="clsred">*</span>
               </div>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_email" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_c_email" ValidationGroup="cg" ErrorMessage="Please enter email" Display="None"></asp:RequiredFieldValidator>

              </div>
          
              </div>

                    
                     <div class="row  mb-6">
      
          <label class="col-sm-2 control-label"> <% = Resources.DeffinityRes.Mobile %></label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_cell" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
             

              </div>
          
              </div>

                    
                     <div class="row mb-6">
     
          <div class="col-sm-2">
          <label class="control-label">Address</label> <span class="clsred">*</span>
              </div>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_address" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_c_address" ValidationGroup="cg" ErrorMessage="Please enter an address with including the postcode" Display="None"></asp:RequiredFieldValidator>

              </div>
          
              </div>

                    
                     <div class="row  mb-6">
     
          <label class="col-sm-2 control-label"> <% = Resources.DeffinityRes.City %></label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_city" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
             

              </div>
          
              </div>

                    
                     <div class="row  mb-6">
     
          <label class="col-sm-2 control-label"> State</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_state" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
             

              </div>
          
              </div>

                      <div class="row  mb-6">
     
          <div class="col-sm-2">
          <label class="control-label"><% = Resources.DeffinityRes.Postcode %> </label> <span class="clsred">*</span>
              </div>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txt_c_zipcode" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
             

              </div>
          
              </div>
					
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector" style="display: none;">Close</button>
					<asp:Button SkinID="btnSubmit" ID="btnSubmitCustomer" runat="server" ValidationGroup="cg"></asp:Button>
				</div>
			</div>
		</div>

	</div>

<div id="printablediv">

    <div class="card shadow-sm mb-6">
						<div class="card-header">
							<h3 class="card-title"> 
                                <label id="lblTitle" runat="server"></label>  
                            </h3>
							<div class="card-toolbar">
                                 <asp:Label ID="lbl_loading" runat="server" Text="Loading..." ClientIDMode="Static"
            Font-Bold="true"></asp:Label>
								 <asp:UpdatePanel ID="upnlSave" runat="server">
            <ContentTemplate>

                <asp:HiddenField ID="hfkeyId" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hpost" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hsubid" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hAssignedTechnicianid" runat="server" ClientIDMode="Static" Value="0" />

             <%--   <button id="btnVideo" runat="server" class="btn btn-white btn-icon btn-icon-standalone btn-sm">
									<i class="fa-video-camera" style="color:white;"></i>
									<span>Watch Video</span>
								</button>--%>
                <asp:Button ID="btnSave" runat="server" SkinID="btnSubmit" CausesValidation="false"
                    OnClick="btnSave_Click" ClientIDMode="Static" ValidationGroup="fls"  />
                <asp:LinkButton ID="btnCancel" runat="server" AlternateText="Cancel" SkinID="btnCancel"
                    OnClick="btnCancel_Click" Visible="false" />
                <asp:LinkButton SkinID="btnLogNewTicket" ID="btnCreateNewTicket" runat="server" Text="Create New Ticket" OnClick="btnCreateNewTicket_Click" Visible="false" />
                <asp:LinkButton SkinID="btnRescheduleVisit" ID="btnRescheduleVisit" runat="server" Text="Reschedule Visit" OnClick="btnRescheduleVisit_Click" Visible="false" />
              
     <%-- <ajaxToolkit:ModalPopupExtender ID="mdlVideo" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnVideo" PopupControlID="pnlVideo" CancelControlID="lblbtnClose" >
</ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="pnlVideo" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label7" runat="server" Text="How To Log a Job"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblbtnClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/516664404"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>--%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnCreateNewTicket" />
            </Triggers>
        </asp:UpdatePanel>
                               <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
							</div>
						</div>
						<div class="card-body">
                            <div class="row">
           
                                <div class="form-group row">
    <div class="col-md-12">
        <asp:Label ID="lblAlert" runat="server" CssClass="ralert ralert-danger" EnableViewState="true" Visible="false" Style="display: inline;"></asp:Label>
        <asp:Label ID="lblMsg" runat="server" SkinID="RedBackcolor" EnableViewState="true" Style="display: inline;"></asp:Label>

        <asp:Label ID="lblSuccessmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="true"></asp:Label>
        <asp:Label ID="lblNewTicketMsg" runat="server" ForeColor="Green"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls"
            DisplayMode="BulletList" ClientIDMode="Static" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AssetPopup"
            DisplayMode="BulletList" ClientIDMode="Static" />
        <asp:Panel Style="display: none;" ID="lblExpiredMsg" runat="server" CssClass="ralert ralert-danger">
            Sorry but your maintenance plan has expired. Please call the Customer Services Department to discuss your renewal
        </asp:Panel>
        <asp:Label ID="lbllnkBtnShowAssets" runat="server"></asp:Label>
    </div>
</div>



<asp:Panel runat="server" ID="div_search">
    <div class="row" runat="server" id="pnlsearchtop">
        <div class="col-lg-6 d-flex d-inline mb-6">
             <input type="text" id="tsearch" style="width:40%;margin-right:10px" class="form-control form-control-lg form-control-solid" /> 
            <asp:DropDownList ID="ddlContacts" runat="server" SkinID="ddl_70" Visible="false"></asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" SkinID="txt_70" MaxLength="500" style="display:none;visibility:hidden;"></asp:TextBox>
            <asp:LinkButton ID="btnSearch" runat="server" SkinID="BtnLinkSearch" ClientIDMode="Static"  style="display:none;visibility:hidden;"/>
              <asp:Button ID="btnAddCustomer" runat="server" SkinID="" Text="Add Project Co-ordinator" ClientIDMode="Static"  />
             <asp:HyperLink ID="btnAddClient" runat="server" NavigateUrl="~/App/Member.aspx?type=sd" CssClass="btn btn-primary" Text="Add Project Co-ordinator" Visible="false"></asp:HyperLink> 
           
        </div>
         <div class="col-lg-4">
             
            <asp:HiddenField ID="haddressid" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hcid" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hapid" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="haid" runat="server" ClientIDMode="Static" Value="0" />
             </div>

    </div>
    <div id="pnlAddress" runat="server">
       <%-- <div class="form-group row">
            <div class="col-md-12 text-bold">
                <strong>Address</strong>
                <hr class="no-top-margin" />
            </div>
        </div>--%>
        <div class="row">
            <%--<div class="col-md-12">--%>
             <div class="table-responsive">
                <table id="addressgrid" class="table table-striped table-bordered"></table>
                 </div>
           <%-- </div>--%>
        </div>
    </div>
    <div id="pnlSearchAddress" runat="server" class="form-group row">
        <div class="row" >
            <%--<div class="col-md-12">--%>
             <div class="table-responsive">
                <table id="addressgridsearch" class="table table-striped table-bordered"></table>
                 </div>
           <%-- </div>--%>
        </div>

         <div class="table-responsive">


        <table id="slist" class="table table-striped table-bordered gy-7 gs-7" style="padding-left: 0px; padding-right: 0px" cellspacing="0" width="100%">
            <thead>
                <tr class="fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200">
                    <th></th>
                    <th>
                        <asp:Label ID="lblRequesterName" runat="server" Text="Co-ordinator"></asp:Label></th>
                    <th>Email</th>
                    <th>Address</th>
                    <th><% = Resources.DeffinityRes.City %></th>
                    <th> <% = Resources.DeffinityRes.TownandCity %></th>
                    <th> <% = Resources.DeffinityRes.Postcode %></th>
                    <%-- <th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                <th>Expiry Date</th>--%>
                </tr>
            </thead>
        </table>
             </div>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadByAddressID);
            function loadByAddressID() {

                var id = $("[id$='haddressid']").val();

                if (id != "") {
                    $("#txtReqTelNo").html("");
                    $("#txtReqEmailAddress").html("");
                    $("#txtRequestersDepartment").html("");
                    $("#txtRequestersJobTitle").html("");
                    $("#txtRequestersAddress").html("");
                    $("#txtRequestersCity").html("");
                    $("#txtRequestersTown").html("");
                    $("#txtRequestersPostcode").html("");
                    //$("[id$='txtPolicyType']").val("");
                    //$("[id$='txtPolicyNumber']").val("");
                    //$("[id$='txtpStartDate']").val("");
                    //$("[id$='txtpExpirtyDate']").val("");
                    //$("[id$='txtpDays']").val("");
                    //$("[id$='lblPolicyNotes']").html("");
                    //$("[id$='haddressid']").val("");
                    //
                    //$("#txtLocation").html("");
                    try {
                        $.ajax({
                            type: "POST",
                            url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetailsByAddressID",
                            data: "{id:'" + id + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {

                                document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                                document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                                document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                                document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                                document.getElementById('txtRequestersAddress').value = msg.d.Address;
                                document.getElementById('txtRequestersCity').value = msg.d.City;
                                document.getElementById('txtRequestersTown').value = msg.d.Town;
                                document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                                //document.getElementById('txtLocation').value = msg.d.Location;
                                //$("[id$='txtPolicyType']").val(msg.d.PolicyType);
                                //$("[id$='txtPolicyNumber']").val(msg.d.PolicyNumber);
                                //$("[id$='txtpStartDate']").val(msg.d.StartDate);
                                //$("[id$='txtpExpirtyDate']").val(msg.d.ExpiryDate);


                                //$("[id$='txtpDays']").css("color", "white");
                                ////$("[id$='txtpDays']").val(msg.d.DaysRemaining);
                                //$("[id$='lblPolicyNotes']").html(msg.d.PolicyNotes);

                                $("[id$='haddressid']").val(msg.d.AddressID);
                                $("[id$='hcid']").val(msg.d.ID);

                               


                                SetKeyContactData();
                                SetEquipmentData();
                                SetWarrentyDate($("[id*=hfEqID").val());
                               
                                
                                //GetAddressRecords(msg.d.AddressID);
                            }
                        });
                    }
                    catch (err) {
                        var er = err;
                    }
                }
                else {
                    document.getElementById('txtReqTelNo').value = "";
                    document.getElementById('txtReqEmailAddress').value = "";
                    document.getElementById('txtRequestersDepartment').value = "";
                    document.getElementById('txtRequestersJobTitle').value = "";
                    document.getElementById('txtRequestersAddress').value = "";
                    document.getElementById('txtRequestersCity').value = "";
                    document.getElementById('txtRequestersTown').value = "";
                    document.getElementById('txtRequestersPostcode').value = "";
                    //document.getElementById('txtLocation').value = "";
                }
                return false;
            }
        </script>
        <script type="text/javascript">
            $('#slist').hide();
            var editorSearch;
            var selectedSearch = [];
            var tableSearch;
            //Search_load();

            function Search_load() {
                //if ($.fn.DataTable.isDataTable("#slist")) {
                //    $('#slist').tableSearch.Destroy();
                //}
                $('#slist').empty();
                //tableSearch.clear();
                $('#slist').show();
                editorSearch = new $.fn.dataTable.Editor({
                    ajax: "/api/ContactAdressDetails",
                    table: "#slist",
                    fields: [
                        {
                            label: "",
                            name: "ID",
                            type: "hidden"
                        }
                    ]
                }
                );
                //$('#slist').DataTable().ajax.reload();
                tableSearch = $('#slist').DataTable({
                    //"iDisplayLength": "400",
                    "scrollY": "200px",
                    "scrollCollapse": true,
                    "paging": true,
                    "scrollX": true,
                    fixedHeader: true,
                    //fixedColumns: {
                    //    leftColumns: 2
                    //},
                    responsive: true,
                    destroy: true,
                    // paging: true,
                    searching: false,
                    dom: "Bfrtip",
                    ajax: {
                        url: "/api/ContactAdressDetails",
                        type: 'POST',
                        async: true,
                        data: function (d) {
                            d.search = $("[id$='txtSearch']").val()
                        }
                    },
                    "language":
                    {
                        "processing": "<i class='fa fa-refresh fa-spin'></i>",
                    },
                    columns: [
                        {
                            data: null,
                            defaultContent: '',
                            className: 'select-checkbox',
                            orderable: false
                        },
                        { data: "Name", title: "Name" },
                        { data: "Email", title: "Email" },
                        { data: "Address", title: "Address" },
                        { data: "City", title: "City" },
                        { data: "State", title: "State" },
                        { data: "PostCode", title: "Zipcode" },
                        //{ data: "PolicyTitle", title: "Policy Type" },
                        //{ data: "PolicyNumber", title: "Policy Number" },
                        //{
                        //    data: "StartDate",
                        //    title: "Start Date",
                        //    render: function (data, type, row) {
                        //        return (moment(row.StartDate).format("MM/DD/YYYY"));
                        //    }
                        //},
                        // {
                        //    data: "ExpiryDate",
                        //    title: "Expiry Date",
                        //    render: function (data, type, row) {
                        //            return (moment(row.ExpiryDate).format("MM/DD/YYYY"));
                        //        }
                        // },
                    ],
                    order: [1, 'asc'],
                    select: true,
                    buttons: [
                        { extend: "create", editor: editorSearch, text: "Add New Equipment" }
                    ]
                });//.clear().draw();;

                $('#slist').on('click', 'tbody td:first-child', function (e) {
                    //editor.inline(this);
                    if ($(this).parents('tr').hasClass('selected')) {
                        $(this).parents('tr').removeClass('selected');
                    }
                    else {
                        $(this).parents('tr').addClass('selected');
                        var id = tableSearch.row(this).id();
                        $("[id$='haddressid']").val(id.replace('row_', ''));
                        //alert($("[id$='haddressid']").val());
                        loadByAddressID();
                    }
                //var id = tableSearch.row(this).id();
                //$("[id$='haddressid']").val(id.replace('row_', ''));
                //alert($("[id$='haddressid']").val());
                //loadByAddressID();

                <%--var dataArr = [];
                $.each($("#slist tbody tr.selected"), function () {
                    var sid = $(this).attr('id');
                   
                    $("[id$='haddressid']").val(sid.replace('row_', ''));
                    alert($("[id$='haddressid']").val());
                    debugger
                    //load values by address id
                    loadByAddressID();
                    //$("#<%=hpid.ClientID%>").val(sid.replace('row_', ''));
                    dataArr.push(sid.replace('row_', ''));
                });--%>
                    // $("#selectedids").val(dataArr);
                });
                //$('#slist').on('click', 'tbody td:not(:first-child)', function (e) {
                //    editorSearch.inline(this, {
                //        onBlur: 'submit',
                //        submit: 'all'
                //    });
                //});
                //$('#slist').on('click', 'a.editor_edit_asset', function (e) {
                //    e.preventDefault();
                //    editorSearch.edit($(this).closest('tr'), {
                //        title: 'Edit record',
                //        buttons: 'Update'
                //    });
                //});
                $(".buttons-create").hide();
            }


            $(document).ready(function () {

                $("[id$='txtSearch']").keyup(function () {
                    Search_load();
                });

            });
        </script>


    </div>

</asp:Panel>

    <asp:UpdateProgress ID="upanelProgress" runat="server">
        <ProgressTemplate>
            <asp:Label runat="server" SkinID="Loading"></asp:Label>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upanelMain" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>



            <asp:LinkButton ID="BtnS" runat="server" ToolTip="Quick Search" ClientIDMode="Static" Visible="false"
                SkinID="BtnLinkSearch"></asp:LinkButton>
            <ajaxToolkit:CascadingDropDown ID="ccdSourceOfRequest" runat="server" TargetControlID="ddlSourceOfRequest"
                BehaviorID="ccdSource" Category="source" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetSourceOfRequest"
                LoadingText="[Loading...]" ClientIDMode="Static" />

            <div id="div_contains" class="row" style="margin-bottom: 0px">
                <div id="div_left" style="width: 50%; float: left" class="col-lg-6 row">
                    <div id="Div_ctrl" class="col-lg-12 row"></div>
                    <div id="divl1" class="col-lg-12 row"></div>
                </div>
                <div id="div_right" style="width: 50%; float: right;" class="col-lg-6 row">
                    <div id="divr1" class="col-lg-12 row"></div>
                </div>
            </div>
            <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory" BehaviorID="ccdCate"
                Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading category...]" ClientIDMode="Static" />
            <ajaxToolkit:CascadingDropDown ID="ccdAssignedName" runat="server" TargetControlID="ddlAssignedtoName"
                BehaviorID="ccdAn" Category="AssignedtoName" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetAssignedtoName" LoadingText="[Loading ...]" ParentControlID="ddlAssignedtoDept" ClientIDMode="Static" />

            <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory" BehaviorID="ccdSubcate"
                Category="SubCategoryId" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlCategory" ClientIDMode="Static" />

            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany"
                BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading company...]" ClientIDMode="Static" />
            <asp:HiddenField ID="hfforAdminAccess" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfCustomerID" runat="server" Value="0" />
             <asp:HiddenField ID="hportfolioid" runat="server" Value="0" />
            <asp:TextBox ID="lblFieldsCount" runat="server" CssClass="TxtCount" ClientIDMode="Static" Style="display: none"></asp:TextBox>
            <div id="MainDivfd" style="display: none;" class="fd row">

                <div id="DivComapany" runat="server" style="float: left; display: none;" class="0">
                    <div id="li_ddlCompany" runat="server">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblCompany" runat="server">
                                <asp:Label ID="lblCompany" runat="server" Text="Customer"></asp:Label>
                            </span>

                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlCompany" runat="server" CausesValidation="true" ClientIDMode="Static" AutoPostBack="true" SkinID="ddl_80"
                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>

                           <%-- <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlCompany"
                                Display="None" ErrorMessage="Please select customer" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>--%>
                        </div>

                    </div>

                </div>
                <div id="DivSourceOfRequest" runat="server" class="row d-flex d-inline mb-6">
                    <div id="li_ddlSourceOfRequest" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblSourceOfRequest" runat="server">
                                <asp:Label ID="lblSourceOfRequest" runat="server" Text="Source of Request"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlSourceOfRequest" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvSourceOfRequest" runat="server" ControlToValidate="ddlSourceOfRequest"
                                Display="None" ErrorMessage="Please select Source of Request" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="DivRequesterName" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlRequesterName" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequesterName" runat="server"></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <%-- <asp:DropDownList ID="ddlName" runat="server" SkinID="ddl_80" 
                 ClientIDMode="Static">
            </asp:DropDownList>--%>
                            <%--<asp:LinkButton ID="lnkBtnShowAssets" runat="server"
                 SkinID="BtnLinkSearch" OnClick="lnkBtnShowAssets_Click" Visible="false"></asp:LinkButton>--%> <%-- Visible='<%# AssetShowVisibility() %>'--%>

                            <%--<ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlName"
                BehaviorID="ccdNa" Category="Name" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetNameByCompanySession"
                 LoadingText="[Loading name...]" ClientIDMode="Static" />--%>
                            <%--  <asp:RequiredFieldValidator ID="rfvRequestersName" runat="server" ControlToValidate="ddlName"
                Display="None" ErrorMessage="Please select Name" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>--%>

                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlName"
                Display="None" ErrorMessage="Please select requester name" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="AssetPopup"></asp:RequiredFieldValidator>--%>


                            <asp:LinkButton ID="ImgBtnRnameAdd" runat="server" SkinID="BtnLinkAdd" ToolTip="Add new requester to the list" ClientIDMode="Static" Style="display: none;" />

                            <%-- <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="PanelRequesterName" TargetControlID="ImgBtnRnameAdd"></ajaxToolkit:ModalPopupExtender>--%>
                        </div>
                    </div>
                </div>
                <div id="DivRequesterTelePhoneNo" runat="server" class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersTelephoneNo" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersTelephoneNo" runat="server">
                                <asp:Label ID="lblRequestersTelephoneNo" runat="server" Text="Co-ordinator’s Mobile Number" ReadOnly="true"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtReqTelNo" runat="server" SkinID="txt_80" ClientIDMode="Static"
                                MaxLength="16"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtReqTelNo"
                                ValidChars="0123456789+ " />
                        </div>
                    </div>
                </div>

                <div id="DivRequesterAddress" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersAddress" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersAddress" runat="server">
                                <asp:Label ID="lblRequestersAddress" runat="server" Text="Address" ReadOnly="true"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersAddress" runat="server" ClientIDMode="Static"
                                MaxLength="500" SkinID="txt_80"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivRequestersCity" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersCity" runat="server"  class="row d-flex d-inline mb-6">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersCity" runat="server">
                                <asp:Label ID="lblRequestersCity" runat="server" Text="City" ReadOnly="true"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersCity" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivRequestersTown" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersTown" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersTown" runat="server">
                                <asp:Label ID="lblRequestersTown" runat="server" Text="Town" ReadOnly="true"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersTown" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivRequestersPostcode" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersPostcode" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersPostcode" runat="server">
                                <asp:Label ID="lblRequestersPostcode" runat="server" Text="Postcode/ Zipcode" ReadOnly="true"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersPostcode" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div id="DivRequesterEmailAddress" runat="server" class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersEmailAddress" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersEmailAddress" runat="server">
                                <asp:Label ID="lblRequestersEmailAddress" runat="server" Text="Co-ordinator’s Email Address"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtReqEmailAddress" runat="server" SkinID="txt_80" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivCategory" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlCategory" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblCategory" runat="server">
                                <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>

                            <%-- <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                Display="None" ErrorMessage="Please select Category" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>--%>

                            <%-- <ajaxToolkit:ModalPopupExtender ID="popIssues" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="PaneladdNew" TargetControlID="Lblpopup0"></ajaxToolkit:ModalPopupExtender>
               <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="paneladdsubcategory" TargetControlID="Lblpopup"></ajaxToolkit:ModalPopupExtender>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="panelsubject" TargetControlID="Lblpopup1"></ajaxToolkit:ModalPopupExtender>--%>
            <%--                <asp:LinkButton ID="ImgConfig" SkinID="BtnLinkAdd" runat="server"
                                ToolTip="Add catagory" OnClick="ImgConfig_Click"></asp:LinkButton>--%>
                            <asp:Label ID="Lblpopup0" runat="server"></asp:Label>
                            <asp:Label ID="Lblpopup" runat="server"></asp:Label>
                            <asp:Label ID="Lblpopup1" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfId" runat="server" Value="0" />

                        </div>


                    </div>
                </div>

                <div id="DivSubCategory" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlSubCategory" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblSubCategory" runat="server">
                                <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">

                            <asp:DropDownList ID="ddlSubCategory" runat="server" SkinID="ddl_80" ClientIDMode="Static"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSubCategory" runat="server" ControlToValidate="ddlSubCategory"
                                Display="None" ErrorMessage="Please select Sub category" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>
                           <%-- <asp:LinkButton ID="BtnsubCategoryEdit" SkinID="BtnLinkAdd" runat="server"
                                ToolTip="Add sub category" OnClick="BtnsubCategoryEdit_Click" />--%>

                        </div>
                    </div>
                </div>
                <div id="DivSubject" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlSubject" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblSubject" runat="server">
                                <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlSubject" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdSubject" runat="server" TargetControlID="ddlSubject"
                                BehaviorID="ccdSub" Category="Subject" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetSubject" LoadingText="[Loading subject...]" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="ddlSubject"
                                Display="None" ErrorMessage="Please select subject" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>

                            <%--<asp:LinkButton ID="ImageButton4" SkinID="BtnLinkAdd" runat="server"
                                ToolTip="Add subject" OnClick="ImageButton4_Click" />--%>
                        </div>
                    </div>
                </div>
                <div id="DivDetails" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_txtDetails" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblDetails" runat="server">
                                <asp:Label ID="lblDetails" runat="server" Text="Details"></asp:Label></span>
                        </div>

                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtDetails" runat="server" Height="100px" TextMode="MultiLine" SkinID="txt_80"
                                ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails"
                                Display="None" ErrorMessage="Please enter Details"
                                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="DivSite" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlSite" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblSite" runat="server">
                                <asp:Label ID="lblSite" runat="server" Text="Site" Cssclass=""></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                                Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite" LoadingText="[Loading site...]" ClientIDMode="Static" />
                            <%-- <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                    Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                    ServiceMethod="GetSiteByCusomerId" ParentControlID="ddlCompany" LoadingText="[Loading site...]" />--%>
                            <asp:RequiredFieldValidator ID="rfvSite" runat="server" ControlToValidate="ddlSite"
                                Display="None" ErrorMessage="Please select Site" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="htid" runat="server" Value="0" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div id="DivSiteDetails" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_txtSitedetails" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblSitedetails" runat="server">
                                <asp:Label ID="lblSitedetails" runat="server" Text="Site details"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtSitedetails" runat="server" SkinID="txt_80"
                                ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivStatus" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlStatus" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblStatus" runat="server">
                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                                BehaviorID="ccdS" Category="Name" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByTypeId"
                                ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus"
                                Display="None" ErrorMessage="Please select Status" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="h_status" runat="server" Value="0" />
                        </div>
                    </div>
                </div>

                <div id="DivPriority" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlPriority" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblPriority" runat="server">
                                <asp:Label ID="lblPriority" runat="server" Text="Priority"></asp:Label>

                                <asp:RequiredFieldValidator ID="rfvDllpriority" runat="server" ControlToValidate="ddlPriority"
                                    Display="None" ErrorMessage="Please select Priority" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="fls"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:UpdatePanel ID="UpdatePriority" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" ClientIDMode="Static">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlPriority" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="imgPriority" SkinID="Info" ClientIDMode="Static" ToolTip="" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlPriority" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
                <div id="DivTypeofRequest" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlTypeOfRequest" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblTypeOfRequest" runat="server">
                                <asp:Label ID="lblTypeOfRequest" runat="server" Text="Type of Request"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlRequestType" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                                <%-- <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                <asp:ListItem Text="Faults" Value="1"></asp:ListItem>
                <asp:ListItem Text="Service Request" Value="2"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType"
                                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestType" LoadingText="[Loading...]" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="rfvTypeOfRequest" runat="server" ControlToValidate="ddlRequestType"
                                Display="None" ErrorMessage="Please select type of request" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="DivRequesterDepartment" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersDepartment" runat="server"  class="row d-flex d-inline mb-6">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersDepartment" runat="server">
                                <asp:Label ID="lblRequestersDepartment" runat="server" Text="Requesters Department" ReadOnly="true"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersDepartment" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivLoggedBy" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtLoggedBy" runat="server"  class="row d-flex d-inline mb-6">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblLoggedBy" runat="server">
                                <asp:Label ID="lblLoggedBy" runat="server" Text="Logged By"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtLoggedName" runat="server" SkinID="txt_80" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivRequesterJobTitle" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtRequestersJobTitle" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblRequestersJobTitle" runat="server">
                                <asp:Label ID="lblRequestersJobTitle" runat="server" Text="Requesters Job Title" ReadOnly="true"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtRequestersJobTitle" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="DivLoggedDatetime" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtLoggedDateTime" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblLoggedDateTime" runat="server">
                                <asp:Label ID="lblLoggedDateTime" runat="server" Text="Logged Date/Time"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtCreatedDate" runat="server" SkinID="txtCalender" ReadOnly="true"></asp:TextBox>&nbsp
                            <asp:TextBox ID="txtCreatedTime" runat="server" SkinID="Time" ClientIDMode="Static"
                                ReadOnly="true"></asp:TextBox><%--(HH:MM)--%>
                        </div>
                    </div>
                </div>
                <div id="DivAssignedToDepartment" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlAssignedtoDepartment" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblAssignedtoDepartment" runat="server">
                                <asp:Label ID="lblAssignedtoDepartment" runat="server" Text="Assigned to Department"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlAssignedtoDept" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvAssignedToDepartment" runat="server" ControlToValidate="ddlAssignedtoDept"
                Display="Dynamic" ErrorMessage="Please select assigned to department " InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:CascadingDropDown ID="ccdAssignedDept" runat="server" TargetControlID="ddlAssignedtoDept"
                                Category="AssignedtoDepartment" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetAssignedtoDepartmentByCutomer" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div id="DivAssignedTechnician" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_ddlAssignedTechnician" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblAssignedTechnician" runat="server">
                                <asp:Label ID="lblAssignedTechnician" runat="server" Text="Assigned Technician"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlAssignedtoName" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAssignedToTechnician" runat="server" ControlToValidate="ddlAssignedtoName"
                                Display="None" ErrorMessage="Please select assigned to name " InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                </div>
                <div id="DivScheduledDateTime" runat="server" class="row d-flex d-inline mb-6" >
                    <%--<span class="col-md-12 col-md-offset-4" style="font-size: smaller;margin-top: 10px;margin-bottom: 5px;">Please input all times in 24 Hour Format (e.g. 3:30pm = 15:30)</span>--%>
                    <div id="li_txtScheduledDateTime" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblScheduledDateTime" runat="server">
                                <asp:Label ID="lblScheduledDateTime" runat="server" Text="Preferred Date / Time"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtSeheduledDateTime" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>
                            <asp:Label ID="imgSeheduledDate" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                                PopupButtonID="imgSeheduledDate" TargetControlID="txtSeheduledDateTime"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvScheduledDate" runat="server" ControlToValidate="txtSeheduledDateTime"
                                Display="None" ErrorMessage="Please enter Preferred date" SetFocusOnError="True"
                                ValidationGroup="fls"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                                ControlToValidate="txtSeheduledDateTime" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                                Display="None" SetFocusOnError="True"></asp:CompareValidator>
                            <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtSeheduledDateTime"
                ValidationGroup="fls" ErrorMessage="Scheduled Date must be a future date" Operator="GreaterThanEqual"
                Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True">
            </asp:CompareValidator>--%>
                            <asp:TextBox ID="txtScheduledTime" runat="server" SkinID="Time" ClientIDMode="Static" MaxLength="5"></asp:TextBox>
                            <asp:TextBox ID="txtScheduledToTime" runat="server" SkinID="Time" ClientIDMode="Static" MaxLength="5" Style="display: none; visibility: hidden;"></asp:TextBox>
                           <label><%--(HH:MM)--%>&nbsp; </label>
                               <asp:DropDownList ID="ddlStartTime" runat="server" SkinID="ddl_100px">
                                <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                    <asp:LinkButton ID="btnAddPrefdate" runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" Visible="false" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtScheduledTime"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtScheduledToTime"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />

                        </div>
                    </div>
                </div>

                <div id="pnlResolution" runat="server" clientidmode="Static" style="float: left; padding-top: 8px; display: none;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtResolution" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblResolution" runat="server">
                                <asp:Label ID="lblResolution" runat="server" Text="Resolution"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline" style="padding-left: 25px">
                            <asp:TextBox ID="txtResolution" runat="server" Height="100px" TextMode="MultiLine"
                                SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RFVResolution" runat="server" ControlToValidate="txtResolution"
                    Display="None" ErrorMessage="Please enter Resolution" SetFocusOnError="True"
                    ClientIDMode="Static" ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <div id="pnlKeyContacts" runat="server" clientidmode="Static" style="float: left; padding-top: 8px; width: 100%"  class="row d-flex d-inline mb-6" visible="false">
                    <div id="li_txtKeyContact" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblKeyContact" runat="server">
                                <asp:Label ID="lblKeyContact" runat="server" Text="Contact"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlKeyContact1" runat="server" SkinID="ddl_80" ClientIDMode="Static"></asp:DropDownList>


                        </div>
                    </div>
                </div>

                <div id="DivScheduledendDateTime" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_txtScheduledendDateTime" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblScheduledendDateTime" runat="server">
                                <asp:Label ID="lblScheduledEndDateTime" Text="Scheduled End Date/Time" runat="server"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtScheduledEndDateTime" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

                            <asp:Label ID="ImgScheduledEndDateTime" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="calScheduledEndDateTime" runat="server" CssClass="MyCalendar"
                                PopupButtonID="ImgScheduledEndDateTime" TargetControlID="txtScheduledEndDateTime"></ajaxToolkit:CalendarExtender>
                            <asp:TextBox ID="txtScheduledEndTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>
                            <label><%--(HH:MM)--%> &nbsp;</label>
                      <asp:DropDownList ID="ddlEndtime" runat="server" SkinID="ddl_100px">
                                <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtScheduledEndTime"
                 Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                 ValidationGroup="fls" SetFocusOnError="true" />

                         

                        </div>
                    </div>
                </div>


                <div id="DivPreferreddate2" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_txtPreferreddate2" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblPreferreddate2" runat="server">
                                <asp:Label ID="lblPreferreddate2" Text="Preferred Date/Time 2" runat="server"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtPreferreddate2" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

                            <asp:Label ID="ImgPreferreddate2" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="cxPreferreddate2" runat="server" CssClass="MyCalendar"
                                PopupButtonID="ImgPreferreddate2" TargetControlID="txtPreferreddate2"></ajaxToolkit:CalendarExtender>
                            <asp:TextBox ID="txtPreferreddatetime2" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00" MaxLength="5"></asp:TextBox>
                            <asp:TextBox ID="txtPreferreddatetimeto2" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00" MaxLength="5"></asp:TextBox>
                            <%--(HH:MM)--%>
                            <asp:RegularExpressionValidator ID="revPreferreddate2" runat="server" ControlToValidate="txtPreferreddatetime2"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPreferreddatetimeto2"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Preferred Date/Time 2 should be greater than Preferred Date/Time" ControlToCompare="txtSeheduledDateTime"
                                ControlToValidate="txtPreferreddate2" ValidationGroup="fls" Type="Date" Operator="GreaterThanEqual"
                                Display="None"></asp:CompareValidator>
                        </div>
                    </div>
                </div>

                <div id="DivPreferreddate3" runat="server"  class="row d-flex d-inline mb-6">
                    <div id="li_txtPreferreddate3" runat="server"  class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblPreferreddate3" runat="server">
                                <asp:Label ID="lblPreferreddate3" Text="Preferred Date/Time 2" runat="server"></asp:Label>
                            </span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtPreferreddate3" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

                            <asp:Label ID="ImgPreferreddate3" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="cxPreferreddate3" runat="server" CssClass="MyCalendar"
                                PopupButtonID="ImgPreferreddate3" TargetControlID="txtPreferreddate3"></ajaxToolkit:CalendarExtender>
                            <asp:TextBox ID="txtPreferreddatetime3" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00"></asp:TextBox>
                            <asp:TextBox ID="txtPreferreddatetimeto3" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00"></asp:TextBox>
                            <%--(HH:MM)--%>
                            <asp:RegularExpressionValidator ID="revPreferreddate3" runat="server" ControlToValidate="txtPreferreddatetime3"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPreferreddatetimeto3"
                                Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Preferred Date/Time 3 should be greater than Preferred Date/Time 2" ControlToCompare="txtPreferreddate2"
                                ControlToValidate="txtPreferreddate3" ValidationGroup="fls" Type="Date" Operator="GreaterThanEqual"
                                Display="None"></asp:CompareValidator>
                        </div>
                    </div>
                </div>

                <div id="DivHideType" runat="server"  class="row d-flex d-inline mb-6">
                    <div class="" id="liHideType" style="display: none;"  >

                        <asp:DropDownList ID="ddlTypeofRequest" runat="server" Width="250px" ClientIDMode="Static">
                        </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                            Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                            ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" BehaviorID="ccdT" ClientIDMode="Static" />
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                Display="None"" ErrorMessage="Please select type of request" InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="txtSubmittedBy" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div id="DivTicketManager" runat="server" style="display: none; visibility: hidden;"  class="row d-flex d-inline mb-6">
                    <div id="li_txtTicketManager" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblTicketManager" runat="server">
                                <asp:Label ID="lblTicketManager" runat="server" Text="Ticket Manager"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlTicketManager" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTicketManager"
                Display="None" ErrorMessage="Please select Ticket Manager" InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlEquipments" runat="server" ClientIDMode="Static" Style="float: left; padding-top: 8px; width: 100%" class="row d-flex d-inline mb-6" Visible="false">
                    <div id="li_Equipments" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblEquipment" runat="server">
                                <asp:Label ID="lblEquipment" runat="server" Text="Installed Equipment"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:DropDownList ID="ddlEquipment" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                            </asp:DropDownList>


                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlWarrantyExpiryDate" runat="server" ClientIDMode="Static" Style="float: left; padding-top: 8px; width: 100%" class="row d-flex d-inline mb-6" Visible="false">
                    <div id="li_WarrantyExpiryDate" runat="server" class="row d-flex d-inline">
                        <div class="col-md-4 col-form-label fw-bold fs-6">
                            <span class="" id="li_lblWarrantyExpiryDate" runat="server">
                                <asp:Label ID="lblWarrantyExpiryDate" runat="server" Text="Warranty Expiry Date"></asp:Label></span>
                        </div>
                        <div class="col-md-8 d-flex d-inline">
                            <asp:TextBox ID="txtWarrantyExpiryDate" runat="server" SkinID="Date" MaxLength="10" ClientIDMode="Static"></asp:TextBox>

                        </div>
                    </div>
                </asp:Panel>


            </div>



            <br />
           
          
        </ContentTemplate>
    </asp:UpdatePanel>


                                </div>
                </div>
            </div>
    

     <asp:Panel ID="PanelProducts" runat="server">
                <div class="form-group row" style="display: none; visibility: hidden;">
                    <div class="col-md-12 text-bold">
                        <strong>Registered Equipment </strong>
                        <hr class="no-top-margin" />
                    </div>
                </div>

                <script type="text/javascript" language="javascript" class="init">
                    function getUrlParameter(name) {
                        name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                        var results = regex.exec(location.search.toLowerCase());
                        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
                    };
                    function getAssignhaid() {
                        return $("[id$='hapid']").val();
                    }
                    function gethaid() {
                        return $("[id$='haddressid']").val();
                    }
                    function getcontactid() {
                        return $("[id$='hcid']").val();
                    }

                    var editor1;
                    var selected1 = [];
                    var table1;
                    function assets_table_load() {
                        try {
                            $('#plist').empty();

                            editor1 = new $.fn.dataTable.Editor({
                                ajax: "/api/ContactAdressAssets",
                                table: "#plist",
                                fields: [
                                    {
                                        label: ":",
                                        name: "Assets.ContactID",
                                        type: "hidden",
                                        def: getcontactid()
                                    },
                                    {
                                        label: ":",
                                        name: "Assets.ContactAddressID",
                                        type: "hidden",
                                        def: gethaid()
                                    },
                                    {
                                        label: "Type:",
                                        name: "Assets.Type",
                                        type: "select"
                                    },
                                    {
                                        label: "Make:",
                                        name: "Assets.Make",
                                        type: "select"
                                    },
                                    {
                                        label: "Model:",
                                        name: "Assets.Model",
                                        type: "select"
                                    },
                                    {
                                        label: "Serial number:",
                                        name: "Assets.SerialNo"
                                    },
                                    {
                                        label: "Purchase Date:",
                                        name: "Assets.PurchasedDate",
                                        type: 'datetime',
                                        def: function () { return new Date(); },
                                        dateFormat: 'MM/DD/YYYY'
                                    },
                                    {
                                        label: "Notes:",
                                        name: "Assets.FromNotes"
                                    },
                                ]
                            }
                            );


                            table1 = $('#plist').DataTable({
                                "fnInitComplete": function (oSettings, json) {
                                    var Q_callid = GetParameterValues('callid');
                                    if (Q_callid == undefined) {
                                    }
                                    else if (Q_callid == "") {
                                    }
                                    else {
                                        $('#plist tbody tr:eq(0)').addClass('selected');
                                    }
                                },
                                //"iDisplayLength": "400",
                                "scrollX": true,
                                responsive: true,
                                destroy: true,
                                paging: false,
                                searching: false,
                                dom: "Bfrtip",
                                ajax: {
                                    url: "/api/ContactAdressAssets",
                                    type: 'POST',
                                    async: true,
                                    data: function (d) {
                                        d.ContactID = getcontactid(),
                                            d.ContactAddressID = gethaid(),
                                            d.AssetID = getAssignhaid()
                                    }
                                },
                                columns: [
                                    {
                                        data: null,
                                        defaultContent: '',
                                        className: 'select-checkbox',
                                        orderable: false
                                    },
                                    { data: "Category.Name", editField: "Assets.Type", title: "<%= Deffinity.systemdefaults.GetCategoryName() %>" },
                                    { data: "SubCategory.Name", editField: "Assets.Make", title: "<%= Deffinity.systemdefaults.GetSubCategoryName() %>" },
                                    { data: "ProductModel.ModelName", editField: "Assets.Model", title: "Model" },
                                    { data: "Assets.SerialNo", title: "Serial Number" },
                                    {
                                        title: "Purchase Date",
                                        data: "Assets.PurchasedDate",
                                        render: function (data, type, row) {
                                            return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                        }
                                    },
                                    { data: "Assets.FromNotes", title: "Notes" },
                                ],
                                order: [1, 'asc'],
                                select: true,
                                buttons: [
                                    { extend: "create", editor: editor1, text: "Add New Equipment" }
                                ]
                            });

                            $('#plist').on('click', 'tbody td:first-child', function (e) {
                                //editor.inline(this);
                                if ($(this).parents('tr').hasClass('selected')) {
                                    $(this).parents('tr').removeClass('selected');
                                }
                                else {
                                    $(this).parents('tr').addClass('selected');
                                }
                                var id = table1.row(this).id();

                                var dataArr = [];
                                $.each($("#plist tbody tr.selected"), function () {
                                    var sid = $(this).attr('id');
                                    //
                                    $("#<%=hpid.ClientID%>").val(sid.replace('row_', ''));
                                    dataArr.push(sid.replace('row_', ''));
                                });
                                $("#selectedids").val(dataArr);
                            });
                            $('#plist').on('click', 'tbody td:not(:first-child)', function (e) {
                                editor1.inline(this, {
                                    onBlur: 'submit',
                                    submit: 'all'
                                });
                            });
                            $('#plist').on('click', 'a.editor_edit_asset', function (e) {
                                e.preventDefault();

                                editor1.edit($(this).closest('tr'), {
                                    title: 'Edit record',
                                    buttons: 'Update'
                                });
                            });
                            $(".buttons-create").hide();
                        }
                        catch (e) {
                            var err = e;
                        }
                    }
                    $(document).ready(function () {


                        //postCreate
                        $('#plist').on('create', function (e, json, data) {
                            alert('New row added');
                        });
                        //assets_table_load();

                        //editor.on('preSubmit', validation);  //execute presubmit
                        // Activate an inline edit on click of a table cell
                        //$('#plist').on('click', 'tbody td:not(:first-child)', function (e) {

                        //    editor1.inline(this, {
                        //        onBlur: 'submit',
                        //        submit: 'all'

                        //    });
                        //});

                    });
                </script>
                <div class="row pnl" style="display: none; visibility: hidden;">
                    <div class="table-responsive">
                    <table id="plist" class="table table-striped table-bordered gy-7 gs-7" style="padding-left: 0px; padding-right: 0px" cellspacing="0" width="100%">
                        <thead>
                            <tr class="fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200">
                                <th></th>
                                <th><%= Deffinity.systemdefaults.GetCategoryName() %></th>
                                <th><%= Deffinity.systemdefaults.GetSubCategoryName() %></th>
                                <th>Model</th>
                                <th>Serial Number</th>
                                <th>Purchase Date</th>
                                <th>Notes</th>
                                <%--<th>&nbsp;</th>--%>
                            </tr>
                        </thead>
                    </table>
                        </div>
                </div>


                <div>
                    <script type="text/javascript">
                        function CheckOne(obj) {
                            var grid = obj.parentNode.parentNode.parentNode;
                            var inputs = grid.getElementsByTagName("input");
                            for (var i = 0; i < inputs.length; i++) {
                                if (inputs[i].type == "checkbox") {
                                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                                        inputs[i].checked = false;
                                    }
                                }
                            }
                        }
                        function CheckOneProduct(obj) {
                            var grid = obj.parentNode.parentNode.parentNode;

                            var inputs = grid.getElementsByTagName("input");
                            for (var i = 0; i < inputs.length; i++) {
                                if (inputs[i].type == "checkbox") {
                                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                                        inputs[i].checked = false;
                                    }
                                }
                            }
                        }
                    </script>
                    <asp:UpdatePanel ID="pnlProduct" runat="server">
                        <ContentTemplate>
                            <script type="text/javascript">
                                function GetSelectedRow(lnk) {
                                    //var row = lnk.parentNode.parentNode;
                                    //var rowIndex = row.rowIndex - 1;
                                    //var customerId = row.cells[0].innerHTML;
                                    // var city = row.cells[1].getElementsByTagName("input")[0].value;
                                    //alert("RowIndex: " + rowIndex + " CustomerId: " + customerId + " City:" + city);
                                    $(".pchk").each(function () {
                                        // console.log(index + ": " + $(this).text());
                                        $(this).attr("checked", false);
                                    });
                                    //$("#chk").attr("checked", false);
                                    $("#<%=hpid.ClientID%>").val(lnk.value);
                                    lnk.checked = true;
                                    //alert(lnk.value);
                                    return false;
                                }
                            </script>
                            <asp:HiddenField ID="hpid" runat="server" ClientIDMode="Static" />
                            <asp:GridView ID="GridProducts" runat="server" EmptyDataText="No products available"
                                AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            <%--<asp:CheckBox ID="CheckSelection" runat="server" onclick ="CheckOneProduct(this)" />--%>
                                            <asp:CheckBox ID="CheckSelection" runat="server" Visible="false" />
                                            <input type="checkbox" id='chk<%# Eval("ID") %>' value='<%# Eval("ID") %>' onchange="return GetSelectedRow(this)" class="pchk" />
                                            <%--Checked='<%#CheckBoxSelecting(DataBinder.Eval(Container.DataItem,"callid").ToString(),DataBinder.Eval(Container.DataItem,"Id").ToString()) %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemStyle Wrap="True" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server"
                                                CausesValidation="false" CommandName="Selected" CommandArgument='<%# Bind("ID")%>'
                                                SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%-- <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                            <%--  <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />--%>
                                            <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((int)DataBinder.Eval(Container.DataItem,"ID"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                                Visible='<%# CheckImageVisibility((int)DataBinder.Eval(Container.DataItem,"ID"))%>' />
                                            <%--  <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" Visible="False" HeaderText="ID" />
                                    <asp:BoundField DataField="ID" Visible="False" HeaderText="ID" />
                                    <asp:BoundField DataField="AssetNo" HeaderText="Asset Num" Visible="false"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Notes" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%#Bind("FromNotes") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TypeName" HeaderText="Product Type"></asp:BoundField>
                                    <asp:BoundField DataField="MakeName" HeaderText="Make"></asp:BoundField>
                                    <asp:BoundField DataField="ModelName" HeaderText="Model"></asp:BoundField>
                                    <asp:BoundField DataField="SerialNo" HeaderText="Serial Number"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Warranty Start Date ">
                                        <ItemTemplate>
                                            <asp:Label ID="LblWarrantyStartDate" Text='<%#Bind("Datecommision","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="LblWarrantyEndDate" Text='<%#Bind("ExpDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                    <asp:TemplateField HeaderText="Date Purchased " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDatePurchased" runat="server" Text='<%#Bind("PurchasedDate","{0:d}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StatusName" HeaderText="Status"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Days Before Warranty Expires" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDaysBeforeWarrantyExpires"
                                                Text='<%#GetDatesDifferenceInDays(DataBinder.Eval(Container.DataItem,"Datecommision").ToString(),DataBinder.Eval(Container.DataItem,"ExpDate").ToString())%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="delete"
                                                SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                                OnClientClick="return confirm('Do you want to delete the Asset?');" ToolTip="delete"></asp:LinkButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>


                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </asp:Panel>
            


          

           
            
          


     <asp:UpdatePanel ID="updateAddtional" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>


      <asp:Panel ID="pnlPolicyInfo" runat="server" Style="display: none; visibility: hidden;">
                <div class="form-group row">
                    <div class="col-md-12 text-bold">
                        <strong>Maintenance Plan Information </strong>
                        <hr class="no-top-margin" />
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-3">
                        <label class="col-sm-5 control-label">Maintenance Plan Type</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtPolicyType" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="col-sm-5 control-label">Maintenance Plan Number</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtPolicyNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="col-sm-5 control-label">Start Date</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtpStartDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="col-sm-5 control-label">Expiry Date</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtpExpirtyDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="form-group row">
                    <div class="col-md-3">
                        <label class="col-sm-5 control-label">Days Remaining</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtpDays" runat="server" SkinID="txt_100px" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-12 control-label">Maintenance Plan Notes</label>
                    <div class="col-sm-12" style="min-height: 150px; max-height: 350px; overflow-y: auto; border-style: groove;">
                        <asp:Label ID="lblPolicyNotes" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                </div>
                <div>
                </div>
            </asp:Panel>
           

            <asp:Panel ID="pnlAdditionalInformation" runat="server" style="display:none;visibility:hidden;">

                <div class="row">
                    <div class="col-md-12">
                        <strong><%=Resources.DeffinityRes.AdditionalInformation %></strong>
                        <hr class="no-top-margin" />
                    </div>
                </div>

                <div id="DivAdditionalFields" runat="server" class="row">
                    <div id="div_leftAdditional" style="width: 50%; float: left" class="col-md-6 row">
                        <div id="Div_ctrlAdditional" class="col-md-12 row"></div>
                        <div id="divl1Additional" class="col-md-12 row"></div>
                    </div>
                    <div id="div_rightAdditional" style="width: 50%; float: right;" class="col-md-6 row">
                        <div id="divr1Additional" class="col-md-12 row"></div>
                    </div>
                </div>

                <div style="display: none;" id="DivfdAdditional" class="fdAdditional">
                    <div id="DivResolutionDateandTime" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtResolutionDateandTime" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_ResolutionDateandTime" runat="server">
                                    <asp:Label ID="lblResolutionDateandTime" Text="Resolution Date/Time" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtResolutionDateandTime" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                                <asp:Label ID="imgResolutionDateandTime" runat="server" SkinID="Calender" ClientIDMode="Static" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                    PopupButtonID="imgResolutionDateandTime" TargetControlID="txtResolutionDateandTime"></ajaxToolkit:CalendarExtender>
                                <asp:TextBox ID="txtResolutionTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox><%--(HH:MM)--%>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtResolutionTime"
                 Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                 ValidationGroup="fls" SetFocusOnError="true" />
                            </div>

                        </div>
                    </div>
                    <div id="DivDateAndTimeStarted" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtDateAndTimeStarted" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblDateAndTimeStarted" runat="server">
                                    <asp:Label ID="lblDateAndTimeStarted" runat="server" Text="Date and Time Started"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtStartedDate" runat="server" SkinID="txtCalender" ReadOnly="true"></asp:TextBox>&nbsp
                <asp:TextBox ID="txtStartedTime" runat="server" SkinID="Time" ClientIDMode="Static"
                    ReadOnly="true"></asp:TextBox><%--(HH:MM)--%>
                <%-- <asp:Image ID="imgStartedDate" runat="server" ImageAlign="AbsMiddle" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                     PopupButtonID="imgStartedDate" TargetControlID="txtStartedDate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvStartedDate" runat="server" ControlToValidate="txtStartedDate"
                    Display="Dynamic" ErrorMessage="Please enter date started" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter valid date"
                    ControlToValidate="txtStartedDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                    Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
               
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartedTime"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please enter valid time"
                    Text="*" ValidationExpression="^(\d{2}):(\d{2})" ValidationGroup="fls" />--%>
                            </div>
                        </div>
                    </div>
                    <div id="DivTimeAccumulated" runat="server">
                        <div id="li_txtTimeAccumulated" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblTimeAccumulated" runat="server">
                                    <asp:Label ID="lblTimeAccumulated" runat="server" Text="Time Accumulated"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtTimeAccumulated" runat="server" SkinID="Time" ReadOnly="true"></asp:TextBox><%--(HH:MM)--%>
                <%-- <asp:RequiredFieldValidator ID="rfvTimeAccumulated" runat="server" ControlToValidate="txtTimeAccumulated"
                Display="Dynamic" ErrorMessage="Please enter time accumulated " SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div id="DivDateAndTimeClosed" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtDateAndTimeClosed" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblDateAndTimeClosed" runat="server">
                                    <asp:Label ID="lblDateAndTimeClosed" runat="server" Text="Date and Time Closed"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtClosedDate" runat="server" SkinID="txtCalender" ReadOnly="true"></asp:TextBox>&nbsp
                <asp:TextBox ID="txtClosedTime" runat="server" SkinID="Time" ClientIDMode="Static"
                    ReadOnly="true"></asp:TextBox><%--(HH:MM)--%>
                <%--  <asp:Image ID="imgClosedDate" runat="server" ImageAlign="AbsMiddle" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                     PopupButtonID="imgClosedDate" TargetControlID="txtClosedDate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvClosedDate" runat="server" ControlToValidate="txtClosedDate"
                    Display="Dynamic" ErrorMessage="Please enter date closed" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Please enter valid date"
                    ControlToValidate="txtClosedDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                    Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
             
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtClosedTime"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please enter valid time"
                    Text="*" ValidationExpression="^(\d{2}):(\d{2})" ValidationGroup="fls" />--%>
                            </div>
                        </div>
                    </div>
                    <div id="DivTimeWorked" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtTimeWorked" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblTimeWorked" runat="server">
                                    <asp:Label ID="lblTimeWorked" runat="server" Text="Time Taken to Complete"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtTimeWorked" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox><%--(HH:MM)--%>
                <%--<asp:ImageButton ID="btmSubmit" runat="server" SkinID="ImgSubmit" />--%>
                                <asp:RequiredFieldValidator ID="rfvTimeWorked" runat="server" ErrorMessage="Please enter Time Worked"
                                    Display="None" ValidationGroup="fls" ControlToValidate="txtTimeWorked" ClientIDMode="Static">*</asp:RequiredFieldValidator>
                                <%-- <asp:RegularExpressionValidator ID="ReTimeWorked" runat="server" ControlToValidate="txtTimeWorked"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="fls" ClientIDMode="Static" />--%>
                                <asp:RegularExpressionValidator ID="ReTimeWorked" runat="server" ControlToValidate="txtTimeWorked"
                                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="fls"
                                    Display="None" ErrorMessage="Please enter valid time" ClientIDMode="Static"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div id="DivCustomerRef" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtCustomerRef" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblCustomerRef" runat="server">
                                    <asp:Label ID="lblCustomerRef" runat="server" Text="Customer Ref"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtCustomerRef" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCustomerRef" runat="server" ControlToValidate="txtCustomerRef"
                                    ErrorMessage="Please enter customer ref" SetFocusOnError="true" ValidationGroup="fls"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div id="DivTimeTakentoResolve" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtTimeTakentoResolve" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblTimeTakentoResolve" runat="server">
                                    <asp:Label ID="lblTimeTakentoResolve" runat="server" Text="Time Taken to Resolve"></asp:Label>
                                </span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtTimeTakentoResolve" runat="server" SkinID="Time" ReadOnly="true"></asp:TextBox>(HH:MM)
            <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter" runat="server" TargetControlID="txtTimeTakentoResolve" ValidChars="0123456789:"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div id="DivCustomerCostCode" runat="server">
                        <div id="li_txtCustomerCostCode" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblCustomerCostCode" runat="server">
                                    <asp:Label ID="lblCustomerCostCode" runat="server" Text="Repair Cost"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtCustomerCostCode" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCustomerCostCode" runat="server" ControlToValidate="txtCustomerCostCode"
                                    ErrorMessage="Please enter repair cost" SetFocusOnError="true" ValidationGroup="fls"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCustomerCostCode" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div id="DivNotes" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtNotes" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblNotes" runat="server">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtNotes" runat="server" ClientIDMode="Static" SkinID="txt_80" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ControlToValidate="txtNotes"
                                    ErrorMessage="Please enter notes" SetFocusOnError="true" ValidationGroup="fls"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div id="DivPONumber" runat="server" style="display: none; visibility: hidden;">
                        <div id="li_txtPONumber" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="li_lblPONumber" runat="server">
                                    <asp:Label ID="lblPONumber" runat="server" Text="PO Number"></asp:Label></span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:TextBox ID="txtPOnumber" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPONumber" runat="server" ControlToValidate="txtPOnumber"
                                    ErrorMessage="Please enter PO number" SetFocusOnError="true" ValidationGroup="fls"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div id="DivForms" runat="server" style="float: right; padding-top: 8px; width: 100%; display: none; visibility: hidden;" class="0">
                        <div id="li_ddlForms" runat="server">
                            <div class="col-md-4 col-form-label fw-bold fs-6">
                                <span class="" id="Li_selectform" runat="server">
                                    <asp:Label ID="Lblselectform" runat="server" Text="Form"></asp:Label>
                                </span>
                            </div>
                            <div class="col-md-8 d-flex d-inline">
                                <asp:DropDownList ID="ddlForms" runat="server" SkinID="ddl_80"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="DivHideAdditional" runat="server">
                        <div class="" id="liHideAdditional" style="display: none;">
                            <asp:TextBox ID="txtQty" runat="server" Width="80px" SkinID="Price"></asp:TextBox>
                            <asp:CompareValidator ID="CompQTY" runat="server" ControlToValidate="txtQty"
                                Display="None" ErrorMessage="Please enter valid qty" Operator="DataTypeCheck"
                                Type="Double" ValidationGroup="fls" SetFocusOnError="true"></asp:CompareValidator>
                            <asp:DropDownList ID="ddlTaskCategory" runat="server" Width="220px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdTaskCategory" runat="server" TargetControlID="ddlTaskCategory"
                                Category="Cat" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetTaskCategory" LoadingText="[Loading...]" ClientIDMode="Static" />
                            <asp:DropDownList ID="ddlTaskSubCategory" runat="server" Width="220px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdTaskSubCategory" runat="server" TargetControlID="ddlTaskSubCategory"
                                Category="Cat" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetTaskSubCategory" LoadingText="[Loading...]" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div class="clr">
                </div>
                <br />

                <%--    <table width="100%">
        <tr>
            <td style="width: 187px;"></td>
            <td style="width: 420px;"></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Date and Time Closed
            </td>
            <td></td>
            <td>Time Worked
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Customer Ref
            </td>
            <td></td>
            <td runat="server" id="pnlQTYlable" visible="false">Qty
            </td>
            <td runat="server" id="pnlQTY" visible="false"></td>
        </tr>
        <tr runat="server" id="pnlTaskcategory" visible="false">
            <td>Task Category
            </td>
            <td></td>
            <td>Task Sub Category
            </td>
            <td></td>
        </tr>
        <tr>
            <td runat="server" id="pnlPOlable">PO Number
            </td>
            <td runat="server" id="pnlPO">

               
            </td>
            <td runat="server" id="pnlCostcodeLable">Customer Cost Code
            </td>
            <td runat="server" id="pnlCostcode"></td>

        </tr>
        <tr>
            <td style="vertical-align: middle" valign="middle">Notes
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
               
            </td>
        </tr>
    </table>--%>


                <div class="form-group row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 pull-right">
                        <div id="div2">
                            <asp:Button ID="ImageButton3" runat="server" AlternateText="Save" SkinID="btnUpdate"
                                OnClick="btnSave_Click" ValidationGroup="fls" ClientIDMode="Static" Visible="false" />
                        </div>
                    </div>

                </div>
            </asp:Panel>

            <asp:Panel ID="pnlNotes" runat="server" Visible="false" CssClass="clsNotes">

                <div class="card shadow-sm mb-6">
						<div class="card-header">
							<h3 class="card-title d-flex d-inline"> 
                               Notes</h3>
							<div class="card-toolbar">
								
                              
							</div>
						</div>
						<div class="card-body">
                            <div class="row">
                                 <div class="col-lg-12">
               <uc3:Notes ID="NotesCtrl1" runat="server" />
                                     </div>
                                </div>
                </div>
            </div>


               
               
            </asp:Panel>

            <asp:Panel ID="pnlCustomFields" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12 d-flex d-inline">
                        <strong>Custom fields <%--<%=Resources.DeffinityRes.CustomFieldsfor %>--%>
                            <asp:Label ID="lblCustomFiledCustomer" runat="server" Visible="false"></asp:Label></strong>
                        <hr class="no-top-margin" />
                    </div>
                </div>

                <%-- <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="Custom" DisplayMode="BulletList" />--%>
                <div>
                    <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="form-group row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 pull-right">
                        <div id="div1">
                            <asp:Button ID="imgCustomFieldUpdate" runat="server" SkinID="btnUpdate" ValidationGroup="Custom"
                                OnClick="imgCustomFieldUpdate_Click" Visible="false" />
                        </div>
                    </div>

                </div>

            </asp:Panel>
            <asp:Panel ID="pnlDocument" runat="server">


                <div class="card shadow-sm mb-6">
						<div class="card-header">
							<h3 class="card-title d-flex d-inline"> 
                               Documents</h3>
							<div class="card-toolbar">
								
                               
							</div>
						</div>
						<div class="card-body">
                            
             <div class="form-group row mb-5">
                   
                        <label class="col-sm-1 control-label"><%=Resources.DeffinityRes.Uploadfile %></label>
                        <div class="col-sm-10">
                            <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None"
                                ScrollBars="Auto" ClientIDMode="Static">
                                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" />
                            </asp:Panel>
                             
                        </div>
                       

                </div>
                             <div class="form-group row mb-10">
                                  <div class="col-sm-1">
                                      </div>

                                  <div class="col-sm-10">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="ImgDocumentsUpload" runat="server" SkinID="btnUpload" OnClick="ImgDocumentsUpload_Click"
                                        ValidationGroup="fls" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ImgDocumentsUpload" />

                                </Triggers>
                            </asp:UpdatePanel>
                                      </div>
                                 </div>



                                <div class="form-group row">
                 <div class="col-lg-6">
                        <asp:GridView ID="GvDocuments" runat="server" Width="60%" EmptyDataText="No documents found"
                            OnRowCommand="GvDocuments_RowCommand" DataKeyNames="ID,ContentType,FileName" HorizontalAlign="Left">
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="Uploaded Documents">
                            <ItemTemplate>
                                <asp:HiddenField ID="hid" runat="server" Value='<%#Bind("ID")%>' />
                                <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                    runat="server" CommandName="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Uploaded Documents">
                                    <ItemTemplate>
                                        <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl(DataBinder.Eval(Container.DataItem,"DocumentID").ToString()) %>'
                                            Visible='<%# CheckImageVisibility(DataBinder.Eval(Container.DataItem,"DocumentID").ToString())%>' />
                                        <asp:HiddenField ID="hid" runat="server" Value='<%#Bind("ID")%>' />
                                        <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                            runat="server" CommandName="Download"></asp:LinkButton>

                                        <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl='<%# GetImageUrlOriginal(DataBinder.Eval(Container.DataItem,"DocumentID").ToString()) %>'
                                                Visible='<%# CheckImageVisibility(DataBinder.Eval(Container.DataItem,"DocumentID").ToString())%>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgDel" runat="server" CausesValidation="false" SkinID="BtnLinkDelete"
                                            CommandArgument='<%#Bind("DocumentID")%>' CommandName="DeleteFile" OnClientClick="return confirm('Do you want to delete this Document?');"
                                            ToolTip="delete" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                     </div>
                   
                </div>


                <div class="form-group row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 pull-right">
                        <div id="div3">
                            <asp:Button ID="ImageButton5" runat="server" AlternateText="Save" SkinID="btnUpdate"
                                OnClick="btnSave_Click" ValidationGroup="fls" ClientIDMode="Static" Visible="false" />
                        </div>
                    </div>

                </div>



                              
                </div>
            </div>
            </asp:Panel>


            </ContentTemplate>
         </asp:UpdatePanel>

      <div class="card shadow-sm mb-0 clsMap" style="display:none;visibility:hidden;">
						<div class="card-header">
							<h3 class="card-title"> 
                                Map</h3>
                            
							<div class="card-toolbar">
								
                              
							</div>
						</div>
						<div class="card-body">
                            <div class="row">
              
                    
                
                    <div class="form-group row">
                       <%-- <div id="map_canvas" style="width: 99%; height: 350px"></div>--%>
                      <%--  <script type="text/javascript"
                            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAOl__OC83ExEUvcNrvpkk5kgV9niClHrQ"></script>--%>
                        <asp:HiddenField ID="hkey" runat="server" Value="AIzaSyAOl__OC83ExEUvcNrvpkk5kgV9niClHrQ" />
                        <script type="text/javascript">
                            $(document).ready(function () {
                                //initialize();
                                //WeatherInitialize();
                            });
                            function WeatherInitialize() {

                                var foo = getParameterByName('callid');

                                try {
                                    var mdata = JSON.parse('<%=GetWeatherInfo() %>');

                                    for (i = 0; i < mdata.length; i++) {
                                        var data = mdata[i]
                                        if (i == 0) {
                                            $("#tt" + i.toString()).html(mdata[i].temp + "&deg;");
                                            $("#cc" + i.toString()).html(mdata[i].city);
                                            $("#dt" + i.toString()).html(mdata[i].date);
                                           
                                            var tlist = mdata[i].todaydata;
                                            var tstr = "<ul>";
                                            for (j = 0; j < tlist.length; j++) {
                                               
                                                var tda = tlist[j].time;
                                                var tda1 = tlist[j].timedeg;
                                               
                                                tstr = tstr + "<li><div class='xe-forecast-entry'><time>" + tda + "</time><div class='xe-icon'><i class='meteocons-sunrise'></i></div><strong class='xe-temp'>" + tda1 + "&deg;</strong></div></li>";
                                            }

                                            tstr = tstr + "</ul>"
                                            $("#stoday").html(tstr);
                                            //city
                                        }
                                        //alert(mdata[i].temp);
                                        $("#t" + i.toString()).html(mdata[i].temp + "&deg;");
                                        $("#d" + i.toString()).html(mdata[i].day);
                                    }
                                }
                                catch (err) {

                                }

                            }
                            function getbuttonid(btn) {

                                var v = $(btn).attr("value");
                                //onwaymail
                                //alert($(btn).attr("value"));
                                url = '/WF/DC/FLSJlist.aspx/onwaymail';
                                data = "{ticketno:'" + v + "'}";
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

                                    //$("#lblMessage").text("Configuration saved successfully");
                                }
                                function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
                                    //alert(xhr.statusText);
                                }
                                return false;
                            }
                            function getParameterByName(name, url) {
                                if (!url) {
                                    url = window.location.href;
                                }
                                name = name.replace(/[\[\]]/g, "\\$&");
                                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                                    results = regex.exec(url);
                                if (!results) return null;
                                if (!results[2]) return '';
                                return decodeURIComponent(results[2].replace(/\+/g, " "));
                            }
                          <%--  function initialize() {

                                var foo = getParameterByName('callid');

                                try {
                                    var markers = JSON.parse('<%=GetAllPincodesOfRequester() %>');

                                    if (markers[0] != null) {
                                        var mapOptions = {
                                            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                                            zoom: 16,
                                            mapTypeId: google.maps.MapTypeId.ROADMAP
                                            //  marker:true
                                        };

                                        var infoWindow = new google.maps.InfoWindow();
                                        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

                                        for (i = 0; i < markers.length; i++) {
                                            var data = markers[i]
                                           
                                            if (data.lat != "") {
                                                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                                                var marker = new google.maps.Marker({
                                                    position: myLatlng,
                                                    map: map,
                                                    //icon: 'http://localhost:55580/WF/Admin/ImageHandler.ashx?type=user&id=' + data.Id,
                                                    title: data.title
                                                });
                                                (function (marker, data) {

                                                    // Attaching a click event to the current marker
                                                    google.maps.event.addListener(marker, "click", function (e) {
                                                        infoWindow.setContent('<img src="../../WF/Admin/ImageHandler.ashx?v=1&type=user&id='
                                                            + data.Id + '" style="height:30px;" /> ' + data.description);
                                                        infoWindow.open(map, marker);
                                                    });
                                                })(marker, data);
                                            }


                                        }
                                    }
                                    else {

                                        defaultMap();
                                    }
                                }
                                catch (err) {

                                    var map = new google.maps.Map(document.getElementById("map_canvas"));
                                }







                            }--%>


                            //function defaultMap() {

                            //    var mapOptions = {
                            //        center: new google.maps.LatLng(51.5, -0.12),
                            //        zoom: 6,
                            //        mapTypeId: google.maps.MapTypeId.ROADMAP
                            //    }
                            //    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);


                            //}
                        </script>
                    </div>
                
                
           

                    
                                </div>
                </div>
            </div>

</div>
<%--                </td>
        </tr>
</table>--%>
<script>
    $(document).ready(function () {

        $("#tsearch").keyup(function () {
            GetAddressSearch();
            //alert($("input").val());
        });
    });
</script>
<script type="text/javascript">
    GetAddressRecords($("#haid").val());
    //function getQuerystring(stid) {
    //    var stid = Status.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    //    var regex = new RegExp("[\\?&]" + stid + "=([^&#]*)");
    //    var qs = regex.exec(window.location.href);
    //    if (qs == null)
    //        return default_;
    //    else
    //        return qs[1];
    //}

    //BindAddressDataSearch
    function setchk(id) {
        
        check_uncheck_checkbox(false,id);
        //alert(id);
    }
    function check_uncheck_checkbox(isChecked,id) {
        if (isChecked) {
            $('input[name="gchk"]').each(function () {

                this.checked = true;
            });
        } else {
            $('input[name="gchk"]').each(function () {
                debugger;
                if ($(this).val() != id) {
                    this.checked = false;
                }
                else {
                    $("[id$='txtSearch']").val(id);
                    $("[id$='haddressid']").val(id);

                    if (id != "") {
                        $("#txtReqTelNo").html("");
                        $("#txtReqEmailAddress").html("");
                        $("#txtRequestersDepartment").html("");
                        $("#txtRequestersJobTitle").html("");
                        $("#txtRequestersAddress").html("");
                        $("#txtRequestersCity").html("");
                        $("#txtRequestersTown").html("");
                        $("#txtRequestersPostcode").html("");
                        //$("[id$='txtPolicyNumber']").val("");
                        //$("[id$='txtpStartDate']").val("");
                        //$("[id$='txtpExpirtyDate']").val("");
                        //$("[id$='txtpDays']").val("");
                        //$("[id$='lblPolicyNotes']").html("");
                        $("[id$='haddressid']").val("");
                        //
                        //$("#txtLocation").html("");
                        try {
                            $.ajax({
                                type: "POST",
                                url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetailsSearch",
                                data: "{id:'" + id + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    debugger;
                                    document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                                    document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                                    document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                                    document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                                    document.getElementById('txtRequestersAddress').value = msg.d.Address;
                                    document.getElementById('txtRequestersCity').value = msg.d.City;
                                    document.getElementById('txtRequestersTown').value = msg.d.Town;
                                    document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                                    //document.getElementById('txtLocation').value = msg.d.Location;
                                    //$("[id$='txtPolicyNumber']").val(msg.d.PolicyNumber);
                                    //$("[id$='txtpStartDate']").val(msg.d.StartDate);
                                    //$("[id$='txtpExpirtyDate']").val(msg.d.ExpiryDate);

                                    //$("[id$='txtpDays']").css("color", "white");
                                    //$("[id$='lblPolicyNotes']").html(msg.d.PolicyNotes);

                                    $("[id$='haddressid']").val(msg.d.AddressID);
                                    $("[id$='hcid']").val(msg.d.ID);


                                }
                            });
                        }
                        catch (err) {
                            var er = err;
                        }


                    }
                    else {
                        document.getElementById('txtReqTelNo').value = "";
                        document.getElementById('txtReqEmailAddress').value = "";
                        document.getElementById('txtRequestersDepartment').value = "";
                        document.getElementById('txtRequestersJobTitle').value = "";
                        document.getElementById('txtRequestersAddress').value = "";
                        document.getElementById('txtRequestersCity').value = "";
                        document.getElementById('txtRequestersTown').value = "";
                        document.getElementById('txtRequestersPostcode').value = "";
                        //document.getElementById('txtLocation').value = "";
                    }


                }
            });
        }
    }
    function GetAddressSearch() {

        //alert($("#haid").val());
        $.ajax({
            url: "../../WF/DC/webservices/DCServices.asmx/BindAddressDataSearch",
            type: "POST",
            data: "{'search': '" + $("#tsearch").val() + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: true,
            success: function (data) {

                var NewData = jQuery.parseJSON(data.d);
                var x = "<thead ><tr class='fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200'><th></th> <th>Co-ordinator </th>"
                    + "<th >Email </th><th >Address</th><th >City</th><th >Town</th><th  > Postcode</th></thead>";
                x = x + "<tbody>"

                for (var i = 0; i < NewData.length; i++) {
                    //var ID = NewData[i].ID
                    var Name = NewData[i].Name;
                    var Email = NewData[i].Email;
                    var Address = NewData[i].Address;
                    var City = NewData[i].City;
                    var State = NewData[i].State;
                    var Postcode = NewData[i].Postcode;

                    var chkstring = "";

                   if( $("[id$='haddressid']").val() == NewData[i].AddressID)
        chkstring = "<input type='checkbox' checked='checked' name='gchk' onclick='setchk(" + NewData[i].AddressID + ")' value='" + NewData[i].AddressID + "' id='" + NewData[i].AddressID + "'>";
        else
        chkstring = "<input type='checkbox' name='gchk' onclick='setchk(" + NewData[i].AddressID + ")' value='" + NewData[i].AddressID + "' id='" + NewData[i].AddressID + "'>";
        


                    x = x + "<tr><td>" + chkstring
                        + "</td><td>" + Name
                        + "</td><td>" + Email
                        + "</td><td>" + Address
                        + "</td><td>" + City
                        + "</td><td>" + State
                        + "</td><td>" + Postcode
                        + "</td></tr>";
                }

                x = x + "</tbody>";
                $("#addressgridsearch").empty();
                $("#addressgridsearch").append(x);
                BindAddressTable_search();
                $("#addressgridsearch").removeClass("no-footer");
                //setStatusBackColor();
            }
        });
    }

    function BindAddressTable_search() {
        try {
            var table = $('#addressgridsearch').DataTable({
                'Ordering': true,
                "order": [[1, "asc"]],
                'paging': false,
                'bFilter': false,
                'lengthChange': false,
                'destroy': true,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    //'render': function (data, type, full, meta) {
                    //    return '<input type="checkbox" name="" value="' + $('<div/>').text(data).html() + '">';
                    //}
                }],
            });
        }
        catch (e) {
            var err = e;
        }
    }
    function GetAddressRecords(id) {
       
        //alert($("#haid").val());
        $.ajax({
            url: "../../WF/DC/webservices/DCServices.asmx/BindAddressData",
            type: "POST",
            data: "{'id': '" + $("#haid").val() + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: true,
            success: function (data) {

                var NewData = jQuery.parseJSON(data.d);
                var x = "<thead ><tr class='fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200'><th  >Co-ordinator </th>"
                      + "<th >Email </th><th >Address</th><th >City</th><th >State</th><th  > Postcode</th></thead>";
                  x = x + "<tbody>"

                  for (var i = 0; i < NewData.length; i++) {
                      //var ID = NewData[i].ID
                      var Name = NewData[i].Name;
                      var Email = NewData[i].Email;
                      var Address = NewData[i].Address;
                      var City = NewData[i].City;
                      var State = NewData[i].State;
                      var Postcode = NewData[i].Postcode;


                      x = x + "<tr><td>" + Name
                          + "</td><td>" + Email
                          + "</td><td>" + Address
                          + "</td><td>" + City
                          + "</td><td>" + State
                          + "</td><td>" + Postcode
                          + "</td></tr>";
                  }

                  x = x + "</tbody>";
                  $("#addressgrid").empty();
                  $("#addressgrid").append(x);
                  BindAddressTable();
                  $("#addressgrid").removeClass("no-footer");
                  //setStatusBackColor();
              }
          });
    }
    function BindAddressTable() {
        try {
            var table = $('#addressgrid').DataTable({
                'Ordering': true,
                "order": [[1, "asc"]],
                'paging': false,
                'bFilter': false,
                'lengthChange': false,
                'destroy': true,
                "columnDefs": [{
                    "targets": 0, "orderable": false
                },
                    //{
                    //    "targets": 7,
                    //    "visible": true
                    //}

                ]
            });
        }
        catch (e) {
            var err = e;
        }
    }



</script>
    <script type="text/javascript">
        window.onload = function () {
            var grid = document.getElementById("addressgridsearch");
            var chks = grid.getElementsByTagName("INPUT");
            for (var i = 0; i < chks.length; i++){
                if(chks[i].type == "checkbox") {
                chks[i].onclick = function () {
                    for (var i = 0; i < chks.length; i++) {
                        if (chks[i] != this && this.checked) {
                            chks[i].checked = false;
                        }
                    }
                };
            }
        }
    };

        </script>
<div class="form-group clshistory" style="display:none;visibility:hidden;">
    <div class="col-md-12 text-bold">
        <strong>History of Tickets </strong>
        <hr class="no-top-margin" />
    </div>
</div>

<div class="card shadow-sm mb-6 clshistory" style="display:none;visibility:hidden">
						<div class="card-header">
							<h3 class="card-title"> 
                                History</h3>
							<div class="card-toolbar">
								
                               
							</div>
						</div>
						<div class="card-body">
                            <div class="row clshistory" >
    <div class="col-md-12">
        <table id="students" class="table table-striped table-bordered"></table>
    </div>
</div>
<script type="text/javascript">
    //function getQuerystring(stid) {
    //    var stid = Status.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    //    var regex = new RegExp("[\\?&]" + stid + "=([^&#]*)");
    //    var qs = regex.exec(window.location.href);
    //    if (qs == null)
    //        return default_;
    //    else
    //        return qs[1];
    //}

    $().ready(function () {

        var id = $("[id$='hcid']").val();

        if (id == "")
            id = "0";
        if (id != "0") {
            GetAssetRecords(id);
        }
    }

    );

    function GetAssetRecords(id) {
        try {
            $.ajax({
                url: "../../WF/DC/webservices/DCServices.asmx/BindCallAssets",
                type: "POST",
                data: "{'id': '" + id + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: true,
                success: function (data) {

                    var NewData = jQuery.parseJSON(data.d);
                    var x = "<thead><tr><th style='width:5%;color: #ffffff;'>Job Ref</th>"
                        + "<th  style='width:35%;color: #ffffff;'>Details</th><th  style='width:10%;color: #ffffff;'>Assigned Smart Tech</th><th style='width:10%;color: #ffffff;'>Date Logged</th><th  style='width:10%;color: #ffffff;'>Status</th></thead>";
                    x = x + "<tbody>"

                    for (var i = 0; i < NewData.length; i++) {
                        var CCID = NewData[i].CCID
                        var ID = NewData[i].ID
                        var LoggedDate = NewData[i].LoggedDate;
                        var Details = NewData[i].Details;
                        var AssignedTechnician = NewData[i].AssignedTechnician
                        //var PolicyType = NewData[i].PolicyType;
                        //var MakeName = NewData[i].MakeName;
                        //var ModelName = NewData[i].ModelName;
                        //var SerialNo = NewData[i].SerialNo;
                        var StatusName = NewData[i].StatusName;

                        x = x + "<tr><td>" + ButtonHtml(ID, CCID)
                            + "</td><td>" + Details
                            + "</td><td>" + AssignedTechnician
                            + "</td><td style=direction:rtl>" + LoggedDate
                            //+ "</td><td>" + PolicyType
                            //+ "</td><td>" + MakeName
                            //+ "</td><td>" + ModelName
                            //+ "</td><td>" + SerialNo
                            + "</td><td><span class='statuscls' style='color: white;font-weight: bold;'>" + StatusName + "</span></td></tr>";
                    }

                    x = x + "</tbody>";
                    $("#students").empty();
                    $("#students").append(x);
                   // BindTable();
                    $("#students").removeClass("no-footer");
                    setStatusBackColor();
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
            "order": [[0, "dsc"]],
            'paging': true,
            'bFilter': false,
            'lengthChange': false,
            'destroy': true,
            "columnDefs": [{
                "targets": 0, "orderable": false
            },
                //{
                //    "targets": 7,
                //    "visible": true
                //}

            ]
        });
    }

    function ButtonHtml(Id, ccid) {
        var HtmlText = " <a target='_blank' id=Link" + Id + " href='/WF/DC/FLSForm.aspx?CCID=" + ccid + "&CallID=" + Id + "&SDID="+Id+"' style=' font-weight: bold'>" + ccid + "</a>";
        //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
        return HtmlText;
    }



    </script>

                            </div>
    </div>


<div class="card shadow-sm mb-6 clshistory" style="display:none;visibility:hidden">
						<div class="card-header">
							<h3 class="card-title"> 
                                History</h3>
							<div class="card-toolbar">
								
                               
							</div>
						</div>
						<div class="card-body">
                            <div class="row">
              <iframe id="iframe1" runat="server" width="100%" height="370px" marginwidth="0" marginheight="0"
            scrolling="no" frameborder="1" style="border: thin"></iframe>
                                </div>
                </div>
            </div>



<script type="text/javascript">

    $(window).load(function () {

        if ($("[id$='haddressid']").val() != "") {
            //alert("aid");
            loadByAddressID()
        }


    });
    function loadSearch() {




        $("#btnSearch").click(function () {

            var id =  $("[id$='txtSearch']").val();
            Search_load();
            if (id != "") {
                $("#txtReqTelNo").html("");
                $("#txtReqEmailAddress").html("");
                $("#txtRequestersDepartment").html("");
                $("#txtRequestersJobTitle").html("");
                $("#txtRequestersAddress").html("");
                $("#txtRequestersCity").html("");
                $("#txtRequestersTown").html("");
                $("#txtRequestersPostcode").html("");
                $("[id$='txtPolicyNumber']").val("");
                $("[id$='txtpStartDate']").val("");
                $("[id$='txtpExpirtyDate']").val("");
                $("[id$='txtpDays']").val("");
                $("[id$='lblPolicyNotes']").html("");
                $("[id$='haddressid']").val("");
                //
                //$("#txtLocation").html("");
                try {
                    $.ajax({
                        type: "POST",
                        url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetailsSearch",
                        data: "{id:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                            document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                            document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                            document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                            document.getElementById('txtRequestersAddress').value = msg.d.Address;
                            document.getElementById('txtRequestersCity').value = msg.d.City;
                            document.getElementById('txtRequestersTown').value = msg.d.Town;
                            document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                            //document.getElementById('txtLocation').value = msg.d.Location;
                            $("[id$='txtPolicyNumber']").val(msg.d.PolicyNumber);
                            $("[id$='txtpStartDate']").val(msg.d.StartDate);
                            $("[id$='txtpExpirtyDate']").val(msg.d.ExpiryDate);

                            $("[id$='txtpDays']").css("color", "white");
                            $("[id$='lblPolicyNotes']").html(msg.d.PolicyNotes);
                          
                            $("[id$='haddressid']").val(msg.d.AddressID);
                            $("[id$='hcid']").val(msg.d.ID);


                        }
                    });
                }
                catch (err) {
                    var er = err;
                }


            }
            else {
                document.getElementById('txtReqTelNo').value = "";
                document.getElementById('txtReqEmailAddress').value = "";
                document.getElementById('txtRequestersDepartment').value = "";
                document.getElementById('txtRequestersJobTitle').value = "";
                document.getElementById('txtRequestersAddress').value = "";
                document.getElementById('txtRequestersCity').value = "";
                document.getElementById('txtRequestersTown').value = "";
                document.getElementById('txtRequestersPostcode').value = "";
                //document.getElementById('txtLocation').value = "";
            }
            return false;
        });
    }
   <%-- function ddlNameSetVal(vl) {
        //debugger;
        //$("[id$='ddlName']").find('option[value="' + vl + '"]').attr("selected", "selected").change();
        //debugger;
       // $("#<%= ddlName.ClientID %>").find('option[value="' + vl + '"]').attr("selected", "selected").change();
        //alert($("#<%= ddlName.ClientID %> option:selected").val());
    $("#<%= ddlName.ClientID %> option").each(function () {
         //
        if ($(this).val() == vl) {
            $(this).attr('selected', 'selected');
        }
        else {
            $(this).removeAttr('selected'); 
        }
        });
    }--%>
    $(window).load(function () {
        //NameDropdownBind();
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ddlNameSetVal);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(onPopulated);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(onCallidPopulated);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetSubcategoryID);
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NameDropdownBind);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetAssignedTechnician);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetpnlAddress);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(assets_table_load);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Search_load);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadSearch);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadByAddressID);
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GetTotalEvents);
        //assets_table_load
    });
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ddlNameSetVal);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(onPopulated);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(onCallidPopulated);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetSubcategoryID);
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NameDropdownBind);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetAssignedTechnician);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetpnlAddress);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(assets_table_load);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Search_load);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadSearch);

    // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GetTotalEvents);

    //GridResponsiveCss();

    function SetSubcategoryID() {
        var Q_callid = GetParameterValues('callid');
        if (Q_callid == undefined) {

        }
        else {
            // alert($('#hsubid').val());

            ddlSubVal($('#hsubid').val());
            //$('#ddlSubCategory').val($('#hsubid').val());
        }
    }

    function ddlSubVal(vl) {
        $("#ddlSubCategory option").each(function () {

            if ($(this).val() == vl) {
                $(this).attr('selected', 'selected');
            }
        });
    }
    function SetpnlAddress() {
        var Q_callid = GetParameterValues('callid');
        if (Q_callid == undefined) {
            
            $('.clsMap').hide();
            $('.clshistory').hide();
            $('.clsNotes').hide();
        }
        else {
            $('#pnlSearchAddress').hide();
            $('.clsMap').show();
            //$('.clshistory').show();
            $('.clsNotes').show();

        }
    }
    function SetAssignedTechnician() {
        var Q_callid = GetParameterValues('callid');
        if (Q_callid == undefined) {

        }
        else {
            // alert($('#hsubid').val());

            ddlAssignedTechnicianVal($('#hAssignedTechnicianid').val());
            //$('#ddlSubCategory').val($('#hsubid').val());
        }
    }
    function ddlAssignedTechnicianVal(vl) {
        $("#ddlAssignedtoName option").each(function () {

            if ($(this).val() == vl) {
                $(this).attr('selected', 'selected');
            }
        });
    }

</script>

<script>
    //show search panel if new ticket
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SearchPanelShow);
    SearchPanelShow();
    function SearchPanelShow() {

        $('#div_search').hide();
        var Q_callid = GetParameterValues('callid');

        if (Q_callid == undefined) {
            $('#div_search').show();
        }
        if (Q_callid == "") {
            $('#div_search').show();
        }
        else {
            $('#div_search').hide();
        }
    }

    $(window).load(function () {
        SetpnlAddress();
        
        loadSearch();
        SearchPanelShow();
        loadPdate();
       
    });



    function preferdate2_hide() {

        var q = getQuerystring('callid');
        //if ($("[id$='txtPreferreddate2']").val() == "") {
        if (q == "") {
            $("[id$='DivPreferreddate2']").hide();
        }
        else {
            $("[id$='DivPreferreddate2']").hide();
        }
    }
    function preferdate3_hide() {
        //if ($("[id$='txtPreferreddate3']").val() == "") {
        var q = getQuerystring('callid');
        //if ($("[id$='txtPreferreddate2']").val() == "") {
        if (q == "") {
            $("[id$='DivPreferreddate3']").hide();
        }
        else {
            $("[id$='DivPreferreddate3']").hide();
        }
    }
    function preferdate2_show() {
        $("[id$='DivPreferreddate2']").hide();
    }
    function preferdate3_show() {
        $("[id$='DivPreferreddate3']").hide();
        //if ($("[id$='txtPreferreddate3']").val() != "") {

        //}
    }

    function loadPdate() {
        preferdate2_hide();
        preferdate3_hide();
        var r = 0;
        $("#btnAddPrefdate").click(function (e) {
            e.preventDefault();
            r = r + 1;
            preferdate2_hide();
            preferdate3_hide();
            //if (r == 1) {
            //    preferdate2_show();
            //    preferdate3_hide();
            //}
            //if (r > 1) {
            //    preferdate2_show();
            //    preferdate3_show();
            //}
            return false;
        });
    }
    //btnAddPrefdate
    //btnSearch


</script>



<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
<%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">

<link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
<%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
<%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>

<script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js">
</script>


<script type="text/javascript" src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

</script>
<script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
</script>
<script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>

<script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
</script>
<%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>--%>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
</script>
<script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
</script>
<script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.4/js/dataTables.fixedColumns.min.js">
</script>
<script type="text/javascript" src="/web/js/dataTables.editor.min.js">
</script>


<style type="text/css" class="init">
    div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }

    div.dt-buttons {
        float: right;
    }
</style>
<%--  <script type="text/javascript" language="javascript" src="/web/examples/resources/syntax/shCore.js">
    </script>--%>
<%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/demo.js">
    </script>--%>
<%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/editor-demo.js">
    </script>--%>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
    setStatusBackColor();
    //grid_responsive_display();
    function setStatusBackColor() {

        
        $('.statuscls').each(function () {

            var s = $(this).html();
            if (s == 'New')
                $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Cancelled')
                $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Resolved')
                $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Closed')
                $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Scheduled')
                $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Awaiting Schedule')
                $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Arrived')
                $(this).closest("td").css({ "background-color": "#57579D", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Customer Not Responding')
                $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
            else if (s == ' Feedback Submitted')
                $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
            else if (s == 'Feedback Received')
                $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
        });


    }
    $(window).load(function () {

        $("div").remove(".sticky-table-header");
        $("span").remove(".jtable-page-info");
        //jtable_css();
        //grid_responsive();
        $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
        //$(".btn-toolbar").hide();
        //var cols = [];
        //$(".dropdown-menu li").each(function () {
        //    $(this).hide();
        //});
        //$(".checkbox-row").eq(1).hide();
        //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
        //    $(".checkbox-row").eq(value).hide();
        //});
    });


     // setStatusBackColor();

</script>

<script>
    //var key = 'cea0d6b75754fe3ab77d3983f22573d5';
    //var z = '10025';
    //$.ajax({
    //    url: 'http://api.openweathermap.org/data/2.5/forecast', //API Call
    //    dataType: 'json',
    //    type: 'GET',
    //    data: {
    //        zip: z,
    //        appid: key,
    //        units: 'metric',
    //        cnt: '5'
    //    },
    //    success: function (data) {
    //        var wf = '';
    //        $.each(data, function (index, val) {


    //            wf += '<p><b>' + data.city.name + '</b><img src=http://openweathermap.org/img/w/' + data.list[0].weather.icon + '.png></p>' + data.list[0].main.temp + '&deg;C' + ' | ' + data.list[0].weather.main + ", " + data.list[0].weather.description
    //        });
    //        
    //        $("#showWeatherForcast").html(wf);
    //    }
    //});
</script>

<script type="text/javascript">
    $(function () {



        SetKeyContactData();


        $("[id*=ddlKeyContact1]").change(function () {

            $("[id*=hfkeyId").val($(this).val());
        });

        //setKeyDropdownValue();
        //set dropdown value
    });

    function setKeyDropdownValue() {
        if ($("[id*=hfkeyId").val() != '') {
            $("[id*=ddlKeyContact1]").val($("[id*=hfkeyId").val());
        }
    }
    function SetKeyContactData() {
        var id = $("[id$='hcid']").val();

        if (id == "")
            id = "0";
        $.ajax({
            type: "POST",
            url: "../../WF/DC/webservices/DCServices.asmx/GetKeyContactName1",
            data: "{id:'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var ddlCustomers = $("[id*=ddlKeyContact1]");
               
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(r.d, function () {
                    ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
                setKeyDropdownValue();
            }
        });
    }

    function SetdropdownsValue() {
        if ($("[id*=hfkeyId]").val().trim() != "") {
            $("[id*=ddlKeyContact1] option").each(function () {
                if ($(this).val() == $("[id*=hfkeyId]").val()) {
                    $(this).attr('selected', 'selected');
                }
            });
        }
    }
</script>
<asp:HiddenField ID="hfEqID" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hfWE" runat="server" ClientIDMode="Static" />
<script type="text/javascript">
    $(function () {
        SetEquipmentData();
        //SetEquipment();
        $("[id*=ddlEquipment]").change(function () {
            var id = $("[id*=ddlEquipment]").val();
            SetWarrentyDate(id);
        });
        //setKeyDropdownValue();
        //set dropdown value
    });

    function SetWarrentyDate(id) {

        if (id == "")
            id = "0";
        $.ajax({
            type: "POST",
            url: "../../WF/DC/webservices/DCServices.asmx/GetEquipmentsByID",
            data: "{id:'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $("[id*=txtWarrantyExpiryDate]").val(r.d);

            }
        });

    }

    function SetEquipment() {
        if ($("[id*=hfEqID").val() != '') {
            $("[id*=ddlEquipment]").val($("[id*=hfEqID").val());
        }
    }
    function SetEquipmentData() {
        var id = $("[id$='haddressid']").val();

        if (id == "")
            id = "0";
        $.ajax({
            type: "POST",
            url: "../../WF/DC/webservices/DCServices.asmx/GetEquipments",
            data: "{id:'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var ddlCustomers = $("[id*=ddlEquipment]");
                ddlCustomers.empty();
               
                //ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                $.each(r.d, function () {
                    ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
                SetEquipmentValue();
            }
        });
    }

    function SetEquipmentValue() {
        if ($("[id*=hfEqID]").val().trim() != "") {
            $("[id*=ddlEquipment] option").each(function () {
                if ($(this).val() == $("[id*=hfEqID]").val()) {
                    $(this).attr('selected', 'selected');
                }
            });
        }
    }
</script>


<script type="text/javascript">
    $(document).ready(function () {
        //Dropdownlist Selectedchange event
        $("[id*=ddlEquipment]").change(function () {
            $("[id*=hfEqID]").val($("[id*=ddlEquipment]").val());

            return false;
        })
    });
</script>
