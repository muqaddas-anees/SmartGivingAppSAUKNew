<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
    AutoEventWireup="true" Inherits="DC_AccessControl" Codebehind="AccessControl.aspx.cs" %>


<%@ Register Src="controls/AccessControlHistory.ascx" TagName="AccessControlHistory"
    TagPrefix="uc1" %>
<%@ Register Src="controls/NotesCtrl.ascx" TagName="Notes" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    Access Control
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
        <label id="lblTitle" runat="server">
                        </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <a id="A1" href="/WF/DC/FLSJlist.aspx?type=AccessControl" runat="server"
            target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <script  type="text/javascript">
        $(document).ready(function () {
            $('#navTab').hide();
        });
       </script>
      <%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
   <%-- <script src="../js/jquery-1.2.6.js" type="text/javascript"></script>--%>
    <script src="../../Content/moment.min.js" type="text/javascript"></script>
    <%-- <script type="text/javascript">
         $(document).ready(function () {
             $(document.body).find("[id$='lblPageHead']").html('Access Control');
         });
    </script>--%>
    <script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        function onPopulated() {
            $get("ddlRtype").disabled = true;

        }
        function onCallidPopulated() {
            var id = $('#h_cid').val();
            if (id != 0) {
                DisableControls();
            }
            var id1 = $('#h_callid').val();
            if (id1 != 0) {
                DisableControls();
            }
        }
        function DisableControls() {
            $get("ddlRcmpy").disabled = true;
            $get("ddlRname").disabled = true;
            //$get("ddlarea").disabled = true;
            $get("ddlvstngpurp").disabled = true;
            //$get("ddlstatus").disabled = true;

            $get("ddlsite").disabled = true;
            $('#txtRtelno').attr('readonly', true);
            $('#txtRemail').attr('readonly', true);
            // $('#txtnotes').attr('readonly', true);
            $('#txtNodays').attr('readonly', true);
            $('#txtRdate').attr('readonly', true);
        }
        function DisableStatusIfClosed() {

            var ID = $('#ddlstatus').val();
            if (ID == "21") { // 21 for status "Closed"
                $get("ddlstatus").disabled = true;
                $('#divhideCopyVisitors').hide('slow');
                $('#txtnotes').attr('readonly', true);
            }

        }

        function statusCheck() {
            //            var id1 = $('#h_cid').val();
            //            if (id1 == 0) { // set for status "Expected"
            //                //ccdst.set_SelectedValue('15');
            //                $get("ddlstatus").disabled = true;
            //            }

            var callid = getQuerystring('callid').trim();
            //var id = $('#h_cid').val();

            if (callid.length != 0) {
                $get("ddlstatus").disabled = false;
            }
            else {
                $get("ddlstatus").disabled = true;
            }


        }

        function pageLoad(sender, args) {
            var be = $find("ccdrt");
            be.add_populated(onPopulated);
            $find("ccdc").add_populated(onCallidPopulated);
            $find("ccdrn").add_populated(onCallidPopulated);
            //$find("ccda").add_populated(onCallidPopulated);
            $find("ccdPV").add_populated(onCallidPopulated);
            $find("ccdsi").add_populated(onCallidPopulated);

            $find("ccdst").add_populated(statusCheck);
            $find("ccdst").add_populated(DisableStatusIfClosed);
            //  $find("ccds").add_populated(OnStatusChangedToClosed);


        }

        $().ready(function () {
            $("#ddlRname").change(function () {
                BindValues();
            });
        });
        $().ready(function () {
            BindValues();
        });
        function BindValues() {
            var ID = $('#ddlRname').val();
            if (ID != "0") {
                $("#txtRtelno").html("");
                $.ajax({
                    type: "POST",
                    url: "/WF/DC/webservices/DCServices.asmx/GetReqTelNo",
                    data: "{ID:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        document.getElementById('txtRtelno').value = msg.d;
                    }
                });

                $("#txtRemail").html("");
                $.ajax({
                    type: "POST",
                    url: "/WF/DC/webservices/DCServices.asmx/GetReqEmail",
                    data: "{ID:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        document.getElementById('txtRemail').value = msg.d;
                    }
                });
            }
            document.getElementById('txtRtelno').value = "";
            document.getElementById('txtRemail').value = "";
        }
        $().ready(function () {
            $("#ddlRcmpy").change(function () {
                $('#txtRtelno').val('');
                $('#txtRemail').val('');
            });
        });

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
        //Jq grid section

        //Validation 
        function validatePhone(sEmail) {
            var filter = /^[0-9-+ ]+$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }
        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function companyValidation(value, colname) {
            var sName = value;
            if ($.trim(sName).length == 0)
                return [false, "Please enter company name"];
            else
                return [true, ""];

        }

        function nameValidation(value, colname) {
            var sName = value;
            if ($.trim(sName).length == 0)
                return [false, "Please enter name"];
            else
                return [true, ""];

        }
        function emailValidation(value, colname) {
            var sEmail = value;
            if ($.trim(sEmail).length == 0) {
                return [false, "Please enter email address"];

            }
            else if (validateEmail(sEmail)) {
                return [true, ""];
            }
            else {
                return [false, "Invalid email address"];
            }

        }

        function phoneValidation(value, colname) {

            var sPhone = value;
            if ($.trim(sPhone).length == 0) {
                return [false, "Please enter phone number"];

            }
            else if (validatePhone(sPhone)) {
                return [true, ""];
            }
            else {
                return [false, "Invalid phone number"];
            }

        }

        function photoIDValidation(value, colname) {

            var sPhoto = value;
            if ($.trim(sPhoto) == "No") {
                $(sPhoto).val('');
                return [false, "Please select photo"];
            }
            else
                return [true, ""];

        }

        function displayStatusButtons(cellvalue, options, rowObject) {

            var $jg = options.gid;
            var $ID = rowObject.ID;
            var $photoid = rowObject.PhotoID

            if (rowObject.ArriveStatus == 1) {

                var disable = "disabled"
                var $link = '<input type="button" value="Arrive" onclick="ArrivedStatus(' + $ID + ',' + $jg + ',' + options.rowId + ',' + $photoid + ')"  /><input type="button" value="Depart" disabled="' + disable + '" />'
            }
            else if ((rowObject.ArriveStatus == 0) && (rowObject.DepartStatus == 0)) {
                var disable = "disabled"
                var $link = '<input type="button" value="Arrived" disabled="' + disable + '"  /><input type="button" value="Departed" disabled="' + disable + '" />'
            }

            else {

                var disable = "disabled"
                var $link = '<input type="button" value="Arrived" disabled="' + disable + '"  /><input type="button" value="Depart" onclick="DepartureStatus(' + $ID + ',' + $jg + ')" />'
            }
            return $link;


        }

        function DisplayNoShowButton(cellvalue, options, rowObject) {
            var $jg = options.gid;
            var $vid = rowObject.ID;
            if (rowObject.NoShow == true) {
                var disable = "disabled"
                var $link = '<input type="button" value="No show" disabled="' + disable + '"  />';

            }
            else if (rowObject.NoShow == false) {
                var $link = '<input type="button" value="No show" onclick="DisableStatus(' + $vid + ',' + $jg + ')" >';
            }
            else {
                var $link = '<input type="button" value="No show" disabled="disabled"  />';
            }

            return $link;
        }
        function DisplayChekbox(cellvalue, options, rowObject) {

            var $jg = options.gid;
            var $vid = rowObject.ID;

            if (rowObject.PhotoID) {
                var $cb = '<input type="checkbox" id = "checkbox1" checked onclick="CheckPhotoID(' + $vid + ',' + $jg + ')"/>';
            }
            else {
                var $cb = '<input type="checkbox" id = "checkbox1" onclick="CheckPhotoID(' + $vid + ',' + $jg + ')"/>';
            }
            return $cb;
        }
        function utcDateFormatter(cellvalue, options, rowObject) {
            if (cellvalue) {
                return moment(cellvalue).utc().format("MM/DD/YYYY HH:mm");
            } else {
                return '';
            }
        }
        function CheckPhotoID(ID, jq) {

            $.ajax({
                type: "POST",
                url: "AccessControl.aspx/PhotoID",
                data: "{ 'ID': '" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // $('#divVisitors').jtable('reload');
                    //$('.jtable-child-table-container').jtable('reload');
                    $('#' + jq.id).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                    return [true, '']

                }
            });
        }
        function ArrivedStatus(ID, jq, rowID, photoid) {

            var element = jq.id.toLowerCase();
            var arrayOfStrings = element.split("_");
            var rowcnt = arrayOfStrings[arrayOfStrings.length - 1];

            if (photoid) {
                $('#MainContent_MainContent_repeter_lblphoto_' + rowcnt.toString()).hide();
                $.ajax({
                    type: "POST",
                    url: "AccessControl.aspx/ArriveStatus",
                    data: "{ 'ID': '" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#' + jq.id).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                        return [true, '']

                    }
                });
            }
            else {
                $('#MainContent_MainContent_repeter_lblphoto_' + rowcnt.toString()).show();
                //alert('Please select Photo ID');
                //return [false, 'Please select Photo ID'];


            }
        }
        function PhotoStatus(ID) {
            $.ajax({
                type: "POST",
                url: "AccessControl.aspx/PhotoStatus",
                data: "{ 'ID': '" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert(msg.d);
                    return msg.d;

                }
            });
        }

        function DepartureStatus(ID, jq) {
            $.ajax({
                type: "POST",
                url: "AccessControl.aspx/DepartStatus",
                data: "{ 'ID': '" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#' + jq.id).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                    return [true, '']

                }
            });
        }

        function DisableStatus(ID, jq) {
            $.ajax({
                type: "POST",
                url: "AccessControl.aspx/DisableArriveAndDepartStatus",
                data: "{ 'ID': '" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#' + jq.id).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                    return [true, '']

                }
            });
        }
        function visible(callid) {
            if (callid != '')
                return false;
            else
                return true;
        }

        function arrival_check(arival_status) {

            if ($.trim(arival_status) == "" || arival_status == undefined) {
                return true;
            }
            else
                return false;
        }

        function CollapsibleGrid(rowcount) {
            if (rowcount == 0)
                return false;
            else
                return true;
        }
        function BindBasicControls(jQGridDemo, jQGridDemoPager, callid) {
            $(jQGridDemo).jqGrid('navGrid', jQGridDemoPager,
                   {
                       edit: false,
                       add: false,
                       del: visible(callid),
                       search: false,
                       refresh: false

                   },
                     {
                         closeOnEscape: true, //Closes the popup on pressing escape key
                         reloadAfterSubmit: true,
                         drag: true,
                         afterSubmit: function (response, postdata) {
                             if (response.responseText == "") {

                                 $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                                 return [true, '']
                             }
                             else {
                                 $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                                 return [false, response.responseText]//Captures and displays the response text on th Edit window
                             }
                         },
                         editData: {
                             visitorID: function () {
                                 var sel_id = $(jQGridDemo).jqGrid('getGridParam', 'selrow');
                                 var value = $(jQGridDemo).jqGrid('getCell', sel_id, 'ID');
                                 alert(value);
                                 return value;
                             }
                         }
                     },
                   {
                       closeAfterAdd: true, //Closes the add window after add
                       afterSubmit: function (response, postdata) {
                           if (response.responseText == "") {

                               $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                               return [true, '']
                           }
                           else {
                               $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                               return [false, response.responseText]
                           }
                       }
                   },
                   {   //DELETE
                       closeOnEscape: true,
                       closeAfterDelete: true,
                       reloadAfterSubmit: true,
                       closeOnEscape: true,
                       drag: true,
                       afterSubmit: function (response, postdata) {
                           if (response.responseText == "") {

                               $(jQGridDemo).trigger("reloadGrid", [{ current: true}]);
                               return [false, response.responseText]
                           }
                           else {
                               $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                               return [true, response.responseText]
                           }
                       },
                       delData: {
                           visitorID: function () {
                               var sel_id = $(jQGridDemo).jqGrid('getGridParam', 'selrow');
                               var value = $(jQGridDemo).jqGrid('getCell', sel_id, 'ID');
                               return value;
                           }
                       }
                   },
                   {//SEARCH
                       closeOnEscape: true

                   }
            );
        }

        function BindCommandControls(jQGridDemo, jQGridDemoPager, callid, arival_status) {
            $(jQGridDemo).jqGrid('inlineNav', jQGridDemoPager,
                    {
                        edit: arrival_check(arival_status),
                        editicon: "ui-icon-pencil",
                        add: visible(callid),
                        addicon: "ui-icon-plus",
                        save: arrival_check(arival_status),
                        saveicon: "ui-icon-disk",
                        cancel: arrival_check(arival_status),
                        cancelicon: "ui-icon-cancel",


                        editParams: {
                            keys: false,
                            oneditfunc: null,
                            successfunc: function (val) {
                                if (val.responseText != "") {
                                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                                }
                            },
                            url: null,
                            extraparam: {
                                visitorId: function () {
                                    var sel_id = $(jQGridDemo).jqGrid('getGridParam', 'selrow');
                                    var value = $(jQGridDemo).jqGrid('getCell', sel_id, 'ID');
                                    return value;
                                }
                            },
                            aftersavefunc: null,
                            errorfunc: null,
                            afterrestorefunc: null,
                            restoreAfterError: true,
                            mtype: "POST"
                        },


                        addParams: {
                            useDefValues: true,
                            addRowParams: {
                                keys: true,
                                extraparam: {},
                                // oneditfunc: function () { alert(); },
                                successfunc: function (val) {
                                    if (val.responseText != "") {
                                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                                    }
                                }
                            }
                        }
                    }
        );
        }
        function BindGrid(jQGridDemo, jQGridDemoPager, accessnumber, days, callid, rowcount) {
            debugger;
            //var d = days;
            //var jQGridDemo = '#ctl00_ctl00_MainContent_MainContent_repeter_jQGridDemo_' + days;
            //var jQGridDemoPager = '#ctl00_ctl00_MainContent_MainContent_repeter_jQGridDemoPager_' + days;
            //alert(jQGridDemo);
            var id = getQuerystring('callid');
            var arival_stat;
            $(jQGridDemo).jqGrid({
                pgbuttons: false,
                recordtext: '',
                pgtext: '',
                url: 'JQGridInlineHandler.ashx?callid=' + callid + '&' + 'accessno=' + accessnumber,
                datatype: "json",
                colNames: ['id', 'Name', 'Company', 'Email Address', 'Phone Number', 'Arrival Date', 'Departure Date', 'Arrival Status', 'PhotoID', ''],
                colModel: [
                        { name: 'ID', index: 'ID', hidden: true, stype: 'text' },
   		                { name: 'Name', index: 'Name', width: 170, stype: 'text', sortable: true, editable: true, editrules: { custom: true, custom_func: nameValidation} },
   		                { name: 'Company', index: 'Company', width: 150, editable: true, editrules: { custom: true, custom_func: companyValidation} },
                        { name: 'EmailAddress', index: 'EmailAddress', width: 190, stype: 'text', editrules: { custom: true, custom_func: emailValidation }, sortable: true, editable: true },
                        { name: 'PhoneNumber', index: 'PhoneNumber', width: 140, stype: 'text', stype: 'text', sortable: true, editable: true, editrules: { custom: true, custom_func: phoneValidation} },
                        { name: 'ArrivalDate', index: 'ArrivalDate', width: 130, stype: 'text', editable: false, formatter: utcDateFormatter },
                        { name: 'DepatureDate', index: 'DepatureDate', width: 130, stype: 'text', editable: false, formatter: utcDateFormatter },
                         { name: '', width: 120, formatter: displayStatusButtons },
                        { name: 'PhotoID', index: 'PhotoID', width: 60, formatter: DisplayChekbox },
                        { name: '', width: 60, formatter: DisplayNoShowButton },

   	                  ],

                gridview: true,

                mtype: 'GET',
                loadonce: true,
                //                                        rowList: [10, 20, 30],
                pager: jQGridDemoPager,
                sortname: 'ID',
                hiddengrid: CollapsibleGrid(rowcount), //enable this one to see collapsable grid
                viewrecords: true,
                sortorder: 'desc',
                caption: "Vistors",
                editurl: 'JQGridInlineHandler.ashx?callid=' + callid + '&' + 'accessno=' + accessnumber,
                onSelectRow: function (rowid, e) {

                },
                beforeSelectRow: function (rowid, e) {
                    var row_data = $(this).jqGrid('getRowData', rowid);
                    arival_stat = row_data['ArrivalDate'];

                    if ($.trim(arival_stat) != "") {
                        return false;
                    }
                    else {
                        //if query sting is not existing - do not select
                        //                      if (id != "")
                        //                          return false;
                        //                      else
                        return true;
                    }
                }
            });

            BindBasicControls(jQGridDemo, jQGridDemoPager, id);
            BindCommandControls(jQGridDemo, jQGridDemoPager, id, arival_stat);

        }
         
    </script>
    <script type="text/javascript">
        //Fade out buttons when clicked but only if page validate
        $().ready(function () {
            $('#lbl_loading').hide();
            $('#div_buttons').show();
            $('#imgbtnSubmit').click(function (e) {
                if (Page_ClientValidate()) {
                    $('#div_buttons').fadeOut('fast');
                    $('#lbl_loading').fadeIn('slow');
                }
                else { return false; }
            });
        });
    </script>
    
    <script src="../../Content/JQGridReq/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../Content/JQGridReq/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../Content/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/JQGridReq/grid.locale-en.js" type="text/javascript"></script>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblmailerror" Visible="false" runat="server"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="s"
                                DisplayMode="BulletList" />
              <asp:Label ID="lblstatustext" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersCustomer%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlRcmpy" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:HiddenField ID="h_callid" runat="server" Value="0" ClientIDMode="Static" />
                            <asp:HiddenField ID="h_cid" ClientIDMode="Static" runat="server" Value="0" />
                            <asp:HiddenField ID="h_hidevisitors" ClientIDMode="Static" runat="server" Value="0" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRcmpy"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select customer."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdCompny" runat="server" TargetControlID="ddlRcmpy"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" BehaviorID="ccdc"
                                ClientIDMode="Static" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.TypeofRequest%></label>
           <div class="col-sm-8 form-inline">
                 <asp:DropDownList ID="ddlRtype" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlRtype"
                                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetTypeofRequest" LoadingText="[Loading site...]" BehaviorID="ccdrt"
                                ClientIDMode="Static" SelectedValue="3" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRtype"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select request type."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersName%></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlRname" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdreqname" runat="server" TargetControlID="ddlRname"
                                Category="SubName" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRcmpy" LoadingText="[Loading site...]"
                                BehaviorID="ccdrn" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRname"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requesters name."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlstatus" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstatus"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select status."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlstatus"
                                Category="subtype" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlRtype" LoadingText="[Loading status...]"
                                ClientIDMode="Static" BehaviorID="ccdst" />
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtRtelno" runat="server" SkinID="txt_90" ClientIDMode="Static" MaxLength="16"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtRtelno"
                                ValidChars="0123456789+ " />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestedDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtRdate" runat="server" ValidationGroup="s" ClientIDMode="Static" SkinID="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRdate"
                                ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requested date."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="MM/dd/yyyy format required"
                                ControlToValidate="txtRdate" ValidationGroup="s" Type="Date" Operator="DataTypeCheck"
                                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                            <asp:CompareValidator ID="cmpRdate" runat="server" ControlToValidate="txtRdate" ValidationGroup="s"
                                ErrorMessage="Requested date must be future date." Operator="GreaterThanEqual"
                                Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True"> </asp:CompareValidator>
                            <asp:Label ID="imgDateRequested" runat="server"  SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                                 PopupButtonID="imgDateRequested" TargetControlID="txtRdate">
                            </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersEmailAddress%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtRemail" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlsite" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlsite"
                                Category="SubName" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" BehaviorID="ccdsi"
                                ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlsite"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select site."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PurposeofVisit%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlvstngpurp" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlvstngpurp"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select visiting purpose."
                                Display="Dynamic">*</asp:RequiredFieldValidator><br />
                            <ajaxToolkit:CascadingDropDown ID="ccdvp" runat="server" TargetControlID="ddlvstngpurp"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetVisitinPurpose" LoadingText="[Loading site...]" BehaviorID="ccdPV"
                                ClientIDMode="Static" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.LoggedDateTime%></label>
           <div class="col-sm-8 form-inline">
              <asp:TextBox ID="txtLoggedDateTime" runat="server" SkinID="Date" ReadOnly="true"></asp:TextBox>
                            <asp:TextBox ID="txtLoggedTime" runat="server" SkinID="Time" ReadOnly="true"></asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
         
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Numberofdays%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtNodays" runat="server" SkinID="Price_100px" ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNodays"
                                ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please enter no. of days."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="Range_days" runat="server" ControlToValidate="txtNodays"
                                Type="Integer" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please enter no. of day between 1 to 30"
                                ForeColor="Red" Display="Dynamic" MaximumValue="30" MinimumValue="1">*</asp:RangeValidator>
            </div>
	</div>
</div>
    
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-10 control-label"><%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-10">
               <asp:TextBox ID="txtnotes" ClientIDMode="Static" Width="630px" Height="90px" TextMode="MultiLine"
                                runat="server"></asp:TextBox>
            </div>
	</div>
</div>
    <table id="jQGridDem" runat="server">
    </table>
    
<div class="form-group pull-right">
          <div class="col-md-12" id="divhideCopyVisitors">
               <asp:Button ID="imgbtncpyvstrs" ToolTip="Copy day 1 visitor(s) to the other days"
                                    runat="server" SkinID="btnDefault" Text="Copy" OnClick="imgbtncpyvstrs_Click"
                                    Visible="false" />
	</div>
</div>
   
                <div id="divvisitorsdata" runat="server" style="width: 100%">
                    <script  type="text/javascript">
                        var rowcount = 0;
                    </script>
                    <asp:Repeater ID="repeter" runat="server" OnItemDataBound="repeter_ItemDataBound"
                        ClientIDMode="Predictable">
                        <ItemTemplate>
                            <asp:Label ID="lblTrialName" runat="server" Text='<%# "Access Number " + Eval("AccessNumber")+" Day "+Eval("Day") %>'
                                Font-Bold="true" Font-Size="10pt" ForeColor="#0099CC"></asp:Label><hr />
                            <asp:HiddenField ID="hfAccessNo" runat="server" Value='<%# Eval("AccessNumber") %>' />
                            <asp:HiddenField ID="hday" runat="server" Value='<%# Eval("Day") %>' />
                            <asp:Literal ID="scriptBind" runat="server"></asp:Literal>
                            <asp:Label ID="lblphoto" runat="server" ForeColor="Red" Text="Please select Photo ID"
                                Style="display: none;" Font-Size="Smaller"></asp:Label>
                            <asp:Table ID="jQGridDemo" runat="server">
                            </asp:Table>
                            <div id="jQGridDemoPager" runat="server">
                            </div>
                            <script type="text/javascript">

                                var callid = $('#h_callid').val();

                                //var accessNo = $('#hfAccessNo').val();
                                var accessNo = $('#MainContent_MainContent_repeter_hfAccessNo_' + rowcount.toString()).val();
                                 //alert(accessNo);
                                //var days = $('#hday').val();
                                var days = $('#MainContent_MainContent_repeter_hday_' + rowcount.toString()).val();

                                var jQGridDemo = '#MainContent_MainContent_repeter_jQGridDemo_' + (days - 1).toString();
                                var jQGridDemoPager = '#MainContent_MainContent_repeter_jQGridDemoPager_' + (days - 1).toString();
                                //alert(days);
                                debugger;
                                BindGrid(jQGridDemo, jQGridDemoPager, accessNo, days, callid, rowcount);
                                rowcount = rowcount + 1;

                            </script>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
               
    
<div class="form-group row">
          <div class="col-md-12">
               <span style="margin-right: 80px">
                    <asp:Label ID="lbl_loading" runat="server" Text="Loading..." ClientIDMode="Static"
                        Font-Bold="true"></asp:Label></span>
                <div id="div_buttons">
                    <asp:Button ID="imgbtnSubmit" runat="server" SkinID="btnDefault" ValidationGroup="s"
                        OnClick="imgbtnSubmit_Click" ClientIDMode="Static" />
                    <asp:Button ID="imgbtnCancel" runat="server" SkinID="btnCancel" OnClick="imgbtnCancel_Click" />
                </div>
               
	</div>
</div>
               
<div class="form-group row">
          <div class="col-md-12">
              <asp:Panel ID="pnlNotes" runat="server" Visible="false">
                    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                        Notes</div>
                    <uc3:Notes ID="Notes1" runat="server" />
                </asp:Panel>
	</div>
</div>
                
              
               
<div class="form-group row">
          <div class="col-md-12">
               <iframe id="iframe1" runat="server" width="100%" height="370px" marginwidth="0" marginheight="0"
                        scrolling="no" frameborder="0"></iframe>
	</div>
</div>
                   
       
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">

     //grid_responsive();
     grid_responsive_display();

     $(window).load(function () {


         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });

     });
    </script>
</asp:Content>
