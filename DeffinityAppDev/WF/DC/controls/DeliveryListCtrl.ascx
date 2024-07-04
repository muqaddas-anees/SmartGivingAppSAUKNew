<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryListCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.DeliveryListCtrl" %>
<%@ Register Src="~/WF/DC/controls/QuickSearchCtrl.ascx" TagName="QuickSearchCtrl" TagPrefix="QSCtrl"%>
<%--<script src="../js/Utility.js" type="text/javascript" language="javascript"></script>--%>
<script type="text/javascript">
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
    //function reFresh() {
    //    location.reload(true)
    //}
</script>
<%--<link href="../../Content/jqgrid.css" rel="stylesheet" type="text/css" />--%>
<!-- jTable style file -->
<link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />

<%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
<div class="form-group row" id="pnlMap" runat="server">
    <div id="map_canvas" style="width: 99%; height: 400px" ></div>
</div>

<script type="text/javascript">
  
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
   
</script>
 <asp:LinkButton ID="BtnS" runat="server" ToolTip="Quick Search" ClientIDMode="Static"
                  ImageAlign="TextTop" SkinID="BtnLinkSearch" Visible="false" />

<div class="form-group row">
             <div class="col-md-4">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Ticketref%>:</label>
                                      <div class="col-sm-8"><input type="text" name="ticketno" id="ticketno" style="width:75px"  class="form-control" />
					</div>
				</div>
     <div class="col-md-4" style="display:none;visibility:hidden;" >
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%>:</label>
                                      <div class="col-sm-8 form-inline"> 
                                          <asp:DropDownList ID="ddlCompany" runat="server" ClientIDMode="Static" SkinID="ddl_70"
                                                                                                      CausesValidation="false"></asp:DropDownList>
                                          <%--<asp:Button ID="BtnHistory" runat="server" SkinID="btnDefault" OnClick="BtnHistory_Click" Text="History" CausesValidation="false" />--%>


                                          <asp:LinkButton ID="LinkButton1" SkinID="BtnLinkHistory"  Text="History" runat="server" OnClick="BtnHistory_Click" Visible="false"></asp:LinkButton>
                                                                                 
                                          <asp:Label ID="lblForPopUp" runat="server"></asp:Label>  

                                           <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany"
                    BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                    ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
					</div>
				</div>
     <div class="col-md-4">
                                        <div class="col-sm-4 control-label" id="tdRequestTypeLable" runat="server"><%= Resources.DeffinityRes.RequestType%>: </div>
                                     <div id="tdRequestTypeField" class="col-sm-8" runat="server">
                        <asp:DropDownList ID="ddlRequestType" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType" ParentControlID="ddlCompany"
                            BehaviorID="ccdTypeNew" Category="type" PromptText="Please select..." PromptValue="0"
                            ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestTypeByCustomer" LoadingText="[Loading...]" />
                   
					</div>
				</div>
     <div class="col-md-4">
          <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%>:</label>
         <div class="col-sm-8">  <asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                    LoadingText="[Loading status...]" Category="Name" PromptText="ALL ACTIVE" PromptValue="0"
                    ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByTypeId"
                    ParentControlID="ddlTypeofRequest" /> </div>
         </div>
                </div>
        
<div class="form-group row">
        

          <div class="col-md-4" id="tdRTpanel" runat="server">
                                       <label class="col-sm-4 control-label" id="tdRTLable" runat="server"> <%= Resources.DeffinityRes.RequestType%>:</label>
                                      <div class="col-sm-8" id="tdRTField" runat="server">  <asp:DropDownList ID="ddlTypeofRequest" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                    LoadingText="[Loading request...]" BehaviorID="ccdT" Category="type" PromptText="ALL"
                    PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetTypeofRequest"
                    ClientIDMode="Static" />
					</div>
				</div>

         <div class="col-md-4" ID="pnlAccessNo" runat="server" Visible="false">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.AccessNumber%>:</label>
                                      <div class="col-sm-7"> <input type="text" name="accessno" id="txtAccessNo" style="width:75px" class="form-control" />
					</div>
				</div>
    </div>


<div class="row pull-right">
     <div class="col-md-12 form-inline ">
         <asp:Button ClientIDMode="Static" SkinID="btnDefault" runat="server" id="LoadTicket" Text="Search"  />
        <asp:HyperLink SkinID="ButtonOrange" runat="server" Text="Log New Ticket" id="Img1" NavigateUrl="~/WF/DC/DCNavigation.ashx?type=FLS" Style="vertical-align:bottom"/> &nbsp;
         <span id="pnlExport" runat="server" visible="false" >
    <asp:Button ID="imgBtnExportToExcel" runat="server" SkinID="btnDefault" Text="Export to Excel"  OnClick="imgBtnExportToExcel_Click" />
    <asp:Button ID="imgBtnExportToPDF" runat="server" SkinID="btnDefault" Text="Export to PDF" OnClick="imgBtnExportToPDF_Click" />
     <asp:LinkButton ID="ImgConfig" SkinID="BtnLinkConfig" Text="Config" runat="server" Style="vertical-align:baseline" ToolTip="Configure Grid View" />
</span>
         </div>
    </div>
<script type="text/javascript">
    Sys.Application.add_load(MyLoad);
    function MyLoad() {
        SDServiceRequestTable
        //alert("UC is loaded.")
        $find("ccdT").add_populated(onPopulated);
    }
    function pageLoad(sender, args) {
        var be = $find("ccdT");
        be.add_populated(onPopulated);
        loadConfigField();
        load_data();
    }
    function onPopulated() {
        //$get("ddlTypeofRequest").disabled = true;
    }
                </script>
 <ajaxToolkit:ModalPopupExtender ID="popIssues" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="PaneladdNew" TargetControlID="ImgConfig" />
<ajaxToolkit:ModalPopupExtender ID="ModelPopUpForHistoryRecords" BackgroundCssClass="modalBackground"
    runat="server" PopupControlID="PnlHistoryRecords" TargetControlID="lblForPopUp"></ajaxToolkit:ModalPopupExtender>
 

<%--<div class="filtering">
    

</div>--%>
<div class="row">
     <div class="col-md-12">

         </div>
    </div>
 <%-- <div id="QSearch">
               <%-- <QSCtrl:QuickSearchCtrl ID="QCCntl" runat="server"></QSCtrl:QuickSearchCtrl>
                     </div>--%>
<div class="row">
     <div class="col-md-12">
<div id="SDServiceRequestTable" style="width: auto; overflow: auto;">
</div>
<div id="FLSTableContainer">
</div>
<div id="SDFaultsTable">
</div>
<div id="WithoutFLSTableContainer">
</div>
<div id="FLSOrderTableContainer">
</div>
<div id="AccessControlJtable"></div>
    </div>
    </div>



<asp:Panel ID="PaneladdNew" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none" ScrollBars="Auto" ClientIDMode="Static">
    <asp:UpdateProgress runat="server" ID="PaneladdNew2" DisplayAfter="10" AssociatedUpdatePanelID="panelupdate" ClientIDMode="Static">
        <ProgressTemplate>
            <asp:Label ID="image1" runat="server" SkinID="Loading" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="panelupdate" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>

            <table>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" ForeColor="Green"></asp:Label></div>
                        <asp:Label ID="lblscreen" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                    </td>
                    <td></td>
                    <td>

                        <div style="text-align: right;">
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel" OnClick="lnkCancel_Click" />
                        </div>

                    </td>
                </tr>
                <tr>
                    <td>
                        <b><%= Resources.DeffinityRes.CurrentColumnsintheGrid%></b>
                        <br />
                        <asp:ListBox ID="gridlist" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                    <td>
                        <button id="btn" type="button">Up</button>
                        <button id="Btn2" type="button">Down</button>
                        <button id="save" type="button">Save</button>

                        <br />
                        <asp:Button ID="add" Text="<<Add Field" runat="server" OnClick="add_Click" />

                        <br />
                        <asp:Button ID="remove" Text="Remove Field>>" runat="server" OnClick="remove_Click" />
                    </td>
                    <td>
                        <b><%= Resources.DeffinityRes.AdditionalFields%></b>
                        <br />
                        <asp:ListBox ID="list" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="add" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="remove" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Panel>

<script type="text/javascript">
    function loadConfigField() {
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
            url = '/WF/DC/webservices/DCServices.asmx/InsertSDColumnPosition';
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
    }
</script>
<br />
<asp:Label ID="lblFooter" runat="server"></asp:Label>
<script type="text/javascript">
    function GetType(id) {
        if (id == "1")
            return "Delivery";
        else if (id == "2")
            return "Permit to Work";
        else if (id == "3")
            return "Access Control";
        else if (id == "4")
            return "Collection";
        else if (id == "6")
            return "FLS";
        else
            return "ALL ACTIVE";
    }


    function New() {
        var typeVal = $('#ddlTypeofRequest').val();
        var typeText = $("#ddlTypeofRequest option:selected").text();
        debugger;
        if (typeVal == 0) {
            alert('Please select request type');
        }
        else {
            window.location = "/WF/DC/DCNavigation.ashx?type=FLS";// + typeText;
        }
    }


    $(document).ready(function () {

        load_data();
        // window.setInterval("reFresh()", 60000);


    });

    function load_data() {

        var type = getQuerystring('type');
        var Actrlid = getQuerystring('Actrlid');
        //order table
        //$('#FLSOrderTableContainer').hide();
        //$('#FLSTableContainer').hide();
        $('#AccessControlJtable').hide();
        $('#WithoutFLSTableContainer').hide();
        $('#SDServiceRequestTable').hide();
        //$('#SDFaultsTable').hide();
        var ccdType1 = $find('ccdT');
        $find('ccdT').add_populated(onPopulated);
        debugger;
        if (type.toLowerCase() == "delivery") {
            ccdType1.set_SelectedValue('1');
            $('#WithoutFLSTableContainer').show();
            $('#AccessControlJtable').hide();
            $('#SDServiceRequestTable').hide();
        }
        else if (type.toLowerCase() == "permittowork") {
            ccdType1.set_SelectedValue('2');
            $('#WithoutFLSTableContainer').show();

        }
        else if (type.toLowerCase() == "accesscontrol") {
            ccdType1.set_SelectedValue('3');
            $('#WithoutFLSTableContainer').hide();//kishore
            $('#AccessControlJtable').show();
        }
        else if (type.toLowerCase() == "collection") {
            ccdType1.set_SelectedValue('4');
        }
        else if (type.toLowerCase() == "fls") {
            ccdType1.set_SelectedValue('6');
            debugger;
            $('#SDServiceRequestTable').show();
            $('#AccessControlJtable').hide();
            //$('#SDFaultsTable').show();
            //$('#FLSOrderTableContainer').hide();
            //$('#FLSTableContainer').hide();
        }
        else if (Actrlid != null)
            ccdType1.set_SelectedValue('3');
        else
            ccdType1.set_SelectedValue('0');

        $('#WithoutFLSTableContainer').jtable({
            //title: 'List of Tickets',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            useBootstrap: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/Get'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets available',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {

                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },
                Company: {
                    title: '<%= Resources.DeffinityRes.Customer%>',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },
                Description: {
                    title: '<%= Resources.DeffinityRes.Description%>',
                    sorting: true,
                    width: '15%',
                    display: function (data) {
                        var dv = data.record.Description;
                        var len = dv.length;
                        var aptext = '';
                        if (len > 75) {
                            aptext = dv.substr(0, 75);

                            aptext = aptext + ' <a href="DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '">Read more...</a>';
                        }
                        else {
                            aptext = dv;
                        }

                        return aptext;
                    }
                },
                ScheduleDate: {
                    title: '<%= Resources.DeffinityRes.ScheduleDate%>',
                    width: '7%'
                },

                Name: {
                    title: '<%= Resources.DeffinityRes.Requester%>',
                    width: '10%'
                },

                RequestType: {
                    title: '<%= Resources.DeffinityRes.RequestType%>',
                    sorting: true,
                    width: '10%'
                },
                Status: {
                    title: '<%= Resources.DeffinityRes.Status%>',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                },
                Note: {
                    title: '<%= Resources.DeffinityRes.Notes%>',
                    sorting: false,
                    width: '10%'
                }

            }
        });

        $('#AccessControlJtable').jtable({
            //title: 'List of Tickets',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            useBootstrap: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/Get1'
            },
            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets available',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {

                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },
                Company: {
                    title: '<%= Resources.DeffinityRes.Customer%>',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },

                ScheduleDate: {
                    title: '<%= Resources.DeffinityRes.ScheduledDate%>',
                    width: '7%'

                },
                Note: {
                    title: '<%= Resources.DeffinityRes.Notes%>',
                    sorting: false,
                    width: '10%'
                },
                Name: {
                    title: '<%= Resources.DeffinityRes.Requester%>',
                    width: '15%'
                },
                PurposeofVisit: {
                    title: '<%= Resources.DeffinityRes.PurposeofVisit%>',
                    width: '15%'
                },
                Status: {
                    title: '<%= Resources.DeffinityRes.Status%>',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                }
            }
        });
       
        function serviceDeskFields() {

            var fields = {

            };
            $.ajax({
                type: "POST",
                url: "/WF/DC/FLSJlist.aspx/GetColumns",
                data: '{}',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    debugger;
                    var r = 0;
                    var myArray = response.d;
                    var arrayLength = myArray.length;
                    for (var i = 0; i < arrayLength; i++) {

                       

                        fields[myArray[i].FieldName] = {
                            title: myArray[i].ColumnName,

                            display: function (data) {
                                if (arrayLength == r) {
                                    r = 0;
                                }
                                var result = myArray[r].FieldName;
                                r++;
                                if (result == "CallID") {
                                    return '<a href="/WF/DC/DCNavigation.ashx?callid=' + data.record.CallID + '&type=FLS"><b>TN:' + data.record.CallID + '</b></a>';

                                }
                                else if (result == "Details") {
                                    var dv = data.record.Details;
                                    var len = dv.length;
                                    var aptext = '';
                                    if (len > 75) {
                                        aptext = dv.substr(0, 75);

                                        aptext = aptext + ' <a href="/WF/DC/DCNavigation.ashx?callid=' + data.record.CallID + '&type=FLS">Read more...</a>';
                                    }
                                    else {
                                        aptext = dv;
                                    }
                                    return aptext;
                                }
                                else if (result == "InHandSLA") {

                                    var inHandSLAStatus = data.record.InHandSLA;

                                    if (inHandSLAStatus == "False" || inHandSLAStatus == "Red") {
                                        return '<label style="color:red;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (inHandSLAStatus == "True" || inHandSLAStatus == "Green") {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (inHandSLAStatus == "Amber") {
                                        return '<label style="color:yellow;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                }
                                else if (result == "ResolutionSLA") {
                                    var resolutionSLA = data.record.ResolutionSLA;
                                    if (resolutionSLA == "False" || resolutionSLA == "Red") {
                                        return '<label style="color:red;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (resolutionSLA == "True" || resolutionSLA == "Green") {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (resolutionSLA == "Amber") {
                                        return '<label style="color:yellow;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                }
                                else if (result == "Image") {
                                    return '<img style="height:50px;margin-left:auto;margin-right:auto;display:block;" src="../../WF/Admin/ImageHandler.ashx?type=user&id='
                                        + data.record.AssignedTechnicianID + '" title="' + data.record.AssignedTechnician + '"/>';
                                }
                                else if (result == "Email") {
                                    return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-mail" title="' + data.record.AssignedTechnicianEmail + '"></i>';
                                }
                                else if (result == "Contact") {
                                    return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="fa fa-phone" title="' + data.record.AssignedTechnicianContact + '"></i>';
                                }
                                else {
                                    return data.record[result];
                                }
                            }
                        };

                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return fields;
        }
        $('#SDServiceRequestTable').jtable({
            //title: 'List of Tickets',
            paging: true,
            width: '100%',//Enables paging
            pageSize: 10, //Actually this is not needed since default value is 10.
            sorting: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/GetSDList'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets have been logged',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: serviceDeskFields()


        });
        var cnt = 0;
        //Re-load records when user click 'load records' button.
        $('#LoadTicket').click(function (e) {
            e.preventDefault();
            var type = document.getElementById('ddlTypeofRequest');
            var status = document.getElementById('ddlStatus');

            var company = document.getElementById('ddlCompany');
            var requestType = document.getElementById('ddlRequestType');
            var accessNo = $('#txtAccessNo').val();
            if (accessNo != null) {
                accessNo = accessNo;
            }
            else {
                accessNo = "";
            }
            var qval = getQuerystring('type')
            var url = document.URL;
            if (qval.toLowerCase() == 'fls') {
                //$('#FLSTableContainer').jtable('load', {
                //    ticketno: $('#ticketno').val(),
                //    type: GetType(ccdType.get_SelectedValue()),
                //    status: status.options[status.selectedIndex].innerHTML,
                //    accessno: accessNo

                //});
               // $('#SDServiceRequestTable').empty();
                $('#SDServiceRequestTable').jtable('load', {
                    company: company.options[company.selectedIndex].innerHTML,
                    ticketno: $('#ticketno').val(),
                    type: GetType(ccdType1.get_SelectedValue()),
                    status: status.options[status.selectedIndex].innerHTML,
                    accessno: accessNo,
                    requestType: requestType.options[requestType.selectedIndex].innerHTML,
                    url: url
                });
                //$('#SDFaultsTable').jtable('load', {
                //    ticketno: $('#ticketno').val(),
                //    type: GetType(ccdType.get_SelectedValue()),
                //    status: status.options[status.selectedIndex].innerHTML,
                //    accessno: accessNo,
                //    requestType:"faults"

                //});
                debugger;
                if (cnt == 0) {
                    $('#SDServiceRequestTable table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#SDServiceRequestTable table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                    $("#SDServiceRequestTable").attr("style", "");
                    //$("sticky-table-header fixed-solution").
                    //$("div").remove(".sticky-table-header");
                   
                    cnt = cnt + 1;
                }
            }
            else if (qval.toLowerCase() == 'delivery' || qval.toLowerCase() == 'permittowork') {
                cnt = 0;
               // $('#WithoutFLSTableContainer').empty();
                $('#WithoutFLSTableContainer').jtable('load', {
                    company: company.options[company.selectedIndex].innerHTML,
                    ticketno: $('#ticketno').val(),
                    type: GetType(ccdType1.get_SelectedValue()),
                    status: status.options[status.selectedIndex].innerHTML,
                    accessno: accessNo
                });

                
                debugger;
                if (cnt == 0) {
                    $('#WithoutFLSTableContainer table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#WithoutFLSTableContainer table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                    $("#WithoutFLSTableContainer").attr("style", "");
                    //$("sticky-table-header fixed-solution").
                    //$("div").remove(".sticky-table-header");

                    cnt = cnt + 1;
                }
            }
            else
            {
                //$('#AccessControlJtable').empty();
                
                $('#AccessControlJtable').jtable('load', {
                    company: company.options[company.selectedIndex].innerHTML,
                    ticketno: $('#ticketno').val(),
                    type: GetType(ccdType1.get_SelectedValue()),
                    status: status.options[status.selectedIndex].innerHTML,
                    accessno: accessNo
                });
                cnt = 0;
                debugger;
                if (cnt == 0) {
                    $('#AccessControlJtable table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#AccessControlJtable table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                    $("#AccessControlJtable").attr("style", "");
                    //$("sticky-table-header fixed-solution").
                    //$("div").remove(".sticky-table-header");

                    cnt = cnt + 1;
                }
            }
            //$('#FLSOrderTableContainer').jtable('load', {
            //    ticketno: $('#ticketno').val(),
            //    type: GetType(ccdType.get_SelectedValue()),
            //    status: status.options[status.selectedIndex].innerHTML,
            //    accessno: accessNo
            //});
        });
        //Load all records when page is first shown
        $('#LoadTicket').click();
    }
  
</script>

<%-- 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>--%>
    <script type="text/javascript">
       
        //grid_responsive_display();
        
        $(window).load(function () {
           
            $("div").remove(".sticky-table-header");
            $("span").remove(".jtable-page-info");
            jtable_css();
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
    </script>