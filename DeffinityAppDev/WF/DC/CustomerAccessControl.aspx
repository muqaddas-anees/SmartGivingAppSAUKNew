<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master"
    AutoEventWireup="true" Inherits="DC_CustomerAccessControl" Codebehind="CustomerAccessControl.aspx.cs" %>


<%@ Register Src="controls/AccessControlHistory.ascx" TagName="AccessControlHistory"
    TagPrefix="uc1" %>
<%@ Register Src="controls/NotesCtrl.ascx" TagName="Notes" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <label id="lblTitle" runat="server">
                        </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=accesscontrol">
<i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
     <script src="../../Content/moment.min.js" type="text/javascript"></script>
    <script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>
    <%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              $(document.body).find("[id$='lblPageHead']").html('Access Control');
          });
    </script>--%>
    <script type="text/javascript">
        function onPopulated() {
            $get("ddlRtype").disabled = true;
            $get("ddlRcmpy").disabled = true;
            $get("ddlRname").disabled = true;
            $get("ddlstatus").disabled = true;

        }
        function onDDLPopulated() {
            var id = $('#h_cid').val();
            if (id != 0) {
                $get("ddlarea").disabled = true;
                $get("ddlsite").disabled = true;
                $get("ddlvstngpurp").disabled = true;
                $get("ddlstatus").disabled = true;

            }

            var id = $('#h_callid').val();
            if (id != 0) {
                $get("ddlsite").disabled = true;
                //$get("ddlarea").disabled = true;
                $get("ddlvstngpurp").disabled = true;
                $get("ddlstatus").disabled = true;

                $get("ddlsite").disabled = true;


            }

        }

        function pageLoad(sender, args) {
            var be = $find("ccdrc");
            be.add_populated(onPopulated);
            var be = $find("ccdrt");
            be.add_populated(onPopulated);
            $find("ccds").add_populated(onPopulated);
            //$find("ccda").add_populated(onDDLPopulated);
            $find("ccdse").add_populated(onDDLPopulated);
            $find("ccdp").add_populated(onDDLPopulated);

            $find("ccdrn").add_populated(onPopulated);
            BindValues();
        }
        $(function () {
            var id = $('#h_cid').val();
            if (id != 0) {

                $('#txtRdate').attr('readonly', true);
                //$('#txtnotes').attr('readonly', true);
                $('#txtNodays').attr('readonly', true);
                $('#txtRemail').attr('readonly', true);
                $('#txtRtelno').attr('readonly', true);
            }

            var id = $('#h_callid').val();
            if (id != 0) {

                $('#txtRtelno').attr('readonly', true);
                $('#txtRemail').attr('readonly', true);
                $('#txtNodays').attr('readonly', true);
                $('#txtRdate').attr('readonly', true);

            }
        });

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
        // var filter = /^[0-9-+]+$/;
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


        function visible(callid) {
            if (callid != '')
                return false;
            else
                return true;
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
                        edit: visible(callid),
                        editicon: "ui-icon-pencil",
                        add: visible(callid),
                        addicon: "ui-icon-plus",
                        save: visible(callid),
                        saveicon: "ui-icon-disk",
                        cancel: visible(callid),
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
            //var d = days;
            //var jQGridDemo = '#ctl00_ctl00_MainContent_MainContent_repeter_jQGridDemo_' + days;
            //var jQGridDemoPager = '#ctl00_ctl00_MainContent_MainContent_repeter_jQGridDemoPager_' + days;
            //alert(jQGridDemo);

            var id = getQuerystring('callid');
            var arival_stat;
            $(jQGridDemo).jqGrid({
                pgbuttons: false, recordtext: '', pgtext: '',
                url: 'JQGridInlineHandler.ashx?callid=' + callid + '&' + 'accessno=' + accessnumber,
                datatype: "json",
                colNames: ['id', 'Name', 'Company', 'Email Address', 'Phone Number'],
                colModel: [
                        { name: 'ID', index: 'ID', hidden: true, stype: 'text' },
   		                { name: 'Name', index: 'Name', width: 300, stype: 'text', sortable: true, editable: true, editrules: { custom: true, custom_func: nameValidation} },
   		                { name: 'Company', index: 'Company', width: 300, editable: true, editrules: { custom: true, custom_func: companyValidation} },
                        { name: 'EmailAddress', index: 'EmailAddress', width: 300, stype: 'text', editrules: { custom: true, custom_func: emailValidation }, sortable: true, editable: true },
                        { name: 'PhoneNumber', index: 'PhoneNumber', width: 300, stype: 'text', stype: 'text', sortable: true, editable: true, editrules: { custom: true, custom_func: phoneValidation} },


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
                beforeSelectRow: function (rowid, e) {
                    if (id != "")
                        return false;
                    else
                        return true;
                }
            });
            BindBasicControls(jQGridDemo, jQGridDemoPager, id);

            BindCommandControls(jQGridDemo, jQGridDemoPager, id, arival_stat);


        }
   

    </script>
    <script language="javascript" type="text/javascript">
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
   <%-- <ul class="tabs_list5" style="float: right;">
        <li class="current5"><a id="A1" href="DCCustomerJlist.aspx?type=AccessControl" runat="server"
            target="_self"><span>Return to Ticket Journal</span></a></li>
    </ul>--%>
    <%--
    <link href="../Scripts/validationEngine/validationEngine.jquery.css" rel="stylesheet"
        type="text/css" />
    <link href="../Scripts/jqgrid.css" rel="stylesheet" type="text/css" />
    <!-- jTable style file -->
    <link href="../Scripts/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/modernizr-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jtablesite.js" type="text/javascript"></script>
    <!-- Import Javascript files for validation engine (in Head section of HTML) -->
    <script type="text/javascript" src="../Scripts/validationEngine/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="../Scripts/validationEngine/jquery.validationEngine-en.js"></script>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="../Scripts/jtable/external/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/external/moment.min.js"></script>
    <!-- Core jTable script file -->
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/localization/jquery.jtable.tr.js"></script>
    <!-- ASP.NET Web Forms extension for jTable -->
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>--%>
    <script src="../../Content/JQGridReq/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../Content/JQGridReq/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../Content/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/JQGridReq/grid.locale-en.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblText" runat="server" ForeColor="Red" Text="Please add details about visitors below. Once complete please ensure you click on the Submit button to complete your request."></asp:Label>
           <asp:Label ID="lblstatustext" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblmailerror" Visible="false" runat="server"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="s"
                                DisplayMode="BulletList" />
	</div>

</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRcmpy" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:HiddenField ID="h_callid" runat="server" Value="0" ClientIDMode="Static" />
                            <asp:HiddenField ID="h_cid" runat="server" Value="0" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRcmpy"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select customer"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdCompny" runat="server" TargetControlID="ddlRcmpy"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCompany" LoadingText="[Loading customers...]" BehaviorID="ccdrc"
                                ClientIDMode="Static" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.TypeofRequest%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRtype" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlRtype"
                                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetTypeofRequest" LoadingText="[Loading requester...]" BehaviorID="ccdrt"
                                ClientIDMode="Static" SelectedValue="3" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRtype"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select request type."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequesterName%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRname" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdreqname" runat="server" TargetControlID="ddlRname"
                                Category="SubName" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRcmpy" LoadingText="[Loading site...]"
                                ClientIDMode="Static" BehaviorID="ccdrn" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRname"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requesters name."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlstatus" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstatus"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select status."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlstatus"
                                Category="subtype" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlRtype" LoadingText="[Loading site...]"
                                ClientIDMode="Static" BehaviorID="ccds" />
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtRtelno" runat="server" SkinID="txt_90" ClientIDMode="Static" MaxLength="16"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtRtelno"
                                ValidChars="0123456789+ " />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestedDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtRdate" runat="server" ClientIDMode="Static" SkinID="Date"></asp:TextBox>
                            <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                                 PopupButtonID="imgSeheduledDate" TargetControlID="txtRdate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRdate"
                                ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requested date."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="MM/dd/yyyy format required"
                                ControlToValidate="txtRdate" ValidationGroup="s" Type="Date" Operator="DataTypeCheck"
                                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                           <%-- <asp:CompareValidator ID="cmpRdate" runat="server" ControlToValidate="txtRdate" ValidationGroup="s"
                                ErrorMessage="Requested date must be future date." Operator="GreaterThanEqual"
                                Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True"> </asp:CompareValidator>--%>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersEmailAddress%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtRemail" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlsite" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlsite"
                                Category="SubName" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" ClientIDMode="Static"
                                BehaviorID="ccdse" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlsite"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select site."
                                Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.PurposeofVisit%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlvstngpurp" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlvstngpurp"
                                ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select visiting purpose."
                                Display="Dynamic">*</asp:RequiredFieldValidator><br />
                            <ajaxToolkit:CascadingDropDown ID="ccdvp" runat="server" TargetControlID="ddlvstngpurp"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetVisitinPurpose" LoadingText="[Loading site...]" ClientIDMode="Static"
                                BehaviorID="ccdp" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Numberofdays%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtNodays" runat="server" SkinID="txt_100px" ClientIDMode="Static"></asp:TextBox>
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
      <div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4 pull-right">
            <div id="divhideCopyVisitors" style="float:right;">
                                <asp:Button ID="imgbtncpyvstrs"  ToolTip="Copy day 1 visitor(s) to the other days"
                                    runat="server" Text="Copy"  OnClick="imgbtncpyvstrs_Click"
                                    Visible="false" />
                            </div>
	</div>
</div>
    

<div class="form-group row">
      <div class="col-md-12">
            <div id="divvisitorsdata" runat="server" style="width: 90%">
                    <div>
                    <asp:Label ID="lblUsermsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                        </div>
                    <script language="javascript" type="text/javascript">
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
                            <asp:Table ID="jQGridDemo" runat="server">
                            </asp:Table>
                            <div id="jQGridDemoPager" runat="server">
                            </div>
                            <script type="text/javascript">

                                var callid = $('#h_callid').val();

                                //var accessNo = $('#hfAccessNo').val();
                                var accessNo = $('#MainContent_MainContent_repeter_hfAccessNo_' + rowcount.toString()).val();
                                // alert(accessNo);
                                //var days = $('#hday').val();
                                var days = $('#MainContent_MainContent_repeter_hday_' + rowcount.toString()).val();

                                var jQGridDemo = '#MainContent_MainContent_repeter_jQGridDemo_' + (days - 1).toString();
                                var jQGridDemoPager = '#MainContent_MainContent_repeter_jQGridDemoPager_' + (days - 1).toString();
                                //alert(days);





                                BindGrid(jQGridDemo, jQGridDemoPager, accessNo, days, callid, rowcount);
                                rowcount = rowcount + 1;





                            </script>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
	</div>

</div>
    

<div class="form-group row">
      <div class="col-md-12">
           <div id="divhideControls" runat="server">
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

</div>
    

<div class="form-group row">
      <div class="col-md-12">
          <asp:Panel ID="pnlNotes" runat="server" Visible="false">
              <div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Notes%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                   
                    <uc3:Notes ID="Notes1" runat="server" />
                </asp:Panel>
                <div>
                    <uc1:AccessControlHistory ID="ctr_history" runat="server" />
                </div>
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
