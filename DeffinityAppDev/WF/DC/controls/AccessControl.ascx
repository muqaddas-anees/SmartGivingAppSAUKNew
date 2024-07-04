<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_AccessControl" Codebehind="AccessControl.ascx.cs" %>
<%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>--%>
<link href="/Volta_Underwork/Scripts/validationEngine/validationEngine.jquery.css"
        rel="stylesheet" type="text/css" />
    <link href="/Volta_Underwork/Scripts/jqgrid.css" rel="stylesheet" type="text/css" />
    <!-- jTable style file -->
    <link href="/Volta_Underwork/Scripts/jtable/themes/metro/lightgray/jtable.css" rel="stylesheet"
        type="text/css" />
    <script src="/Volta_Underwork/Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="/Volta_Underwork/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Volta_Underwork/Scripts/modernizr-1.7.min.js" type="text/javascript"></script>
    <script src="/Volta_Underwork/Scripts/jtablesite.js" type="text/javascript"></script>
    <!-- Import Javascript files for validation engine (in Head section of HTML) -->
    <script type="text/javascript" src="/Volta_Underwork/Scripts/validationEngine/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="/Volta_Underwork/Scripts/validationEngine/jquery.validationEngine-en.js"></script>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="/Volta_Underwork/Scripts/jtable/external/json2.js"></script>
    <!-- Core jTable script file -->
    <script type="text/javascript" src="/Volta_Underwork/Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="/Volta_Underwork/Scripts/jtable/localization/jquery.jtable.tr.js"></script>
    <!-- ASP.NET Web Forms extension for jTable -->
    <script type="text/javascript" src="/Volta_Underwork/Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>

    <table>
<tr>
<td colspan="4">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="s" DisplayMode="BulletList" />
    </td>
</tr>
<tr>
<td style="width: 270px"><label>Site</label><asp:DropDownList ID="ddlsite" Width="150px" runat="server"></asp:DropDownList>
<ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlsite" 
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetSiteByCusomerId" ParentControlID="ddlRcmpy"  />
                <asp:RequiredFieldValidator ID="rfqsite" runat="server" ControlToValidate="ddlsite" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select site." Display="Dynamic">*</asp:RequiredFieldValidator>
<asp:HiddenField ID="h_callid" runat="server" />
</td>
<td style="width: 370px"><label>Type of Request</label><asp:DropDownList 
        ID="ddlRtype" runat="server" Width="150px" ></asp:DropDownList>
         <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlRtype"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRtype" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select request type." Display="Dynamic">*</asp:RequiredFieldValidator>
        </td>
<td align="left" style="width: 350px"><label>Status</label><asp:DropDownList ID="ddlstatus" 
        runat="server" Width="150px"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstatus" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select status." Display="Dynamic">*</asp:RequiredFieldValidator>
        
         <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlstatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlRtype" LoadingText="[Loading site...]" />
        </td>
<td style="width: 350px">
Requesters Company <br />
<asp:DropDownList ID="ddlRcmpy" Width="200px" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRcmpy" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select company." Display="Dynamic">*</asp:RequiredFieldValidator>
<ajaxToolkit:CascadingDropDown ID="ccdCompny" runat="server" TargetControlID="ddlRcmpy"
                Category="company" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" />
                
<br />


Requesters Name <br />
<asp:DropDownList ID="ddlRname" Width="200px" runat="server"></asp:DropDownList>
<ajaxToolkit:CascadingDropDown ID="ccdreqname" runat="server" TargetControlID="ddlRname"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRcmpy" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRname" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requesters name." Display="Dynamic">*</asp:RequiredFieldValidator>
                <br />
Requesters Telephone No.<br />
<asp:TextBox ID="txtRtelno" Width="200px" Height="25px" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>

<br />

Requesters Email Address<br />
<asp:TextBox ID="txtRemail" Width="200px" Height="25px" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
<br />

Requested Date <br />
<asp:TextBox ID="txtRdate" Width="200px" Height="25px" runat="server"></asp:TextBox>
<asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" />
        <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgSeheduledDate" TargetControlID="txtRdate">
        </ajaxToolkit:CalendarExtender>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRdate" ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select requested date." Display="Dynamic">*</asp:RequiredFieldValidator>


<br />

Number of days <br />
<asp:TextBox ID="txtNodays" Width="200px" Height="25px"  runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNodays" ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please enter no. of days." Display="Dynamic">*</asp:RequiredFieldValidator>

<br />

</td>

</tr>

<tr><td align="center" ><label>Area and/or Collection</label></td><td><asp:DropDownList ID="ddlarea" Width="250px" runat="server"></asp:DropDownList>

<ajaxToolkit:CascadingDropDown ID="ccdarea" runat="server" TargetControlID="ddlarea"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetAreaByCompanyId" ParentControlID="ddlRcmpy" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlarea" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select area." Display="Dynamic">*</asp:RequiredFieldValidator>
</td><td>Delivery Number <asp:TextBox ID="txtdelno" Width="150px" Height="25px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtdelno" ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please enter delivery number." Display="Dynamic">*</asp:RequiredFieldValidator><br /><br /></td>
</tr>
<tr><td align="center" ><label>Purpose of Visit</label></td><td><asp:DropDownList ID="ddlvstngpurp" Width="250px" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlvstngpurp" ForeColor="Red" InitialValue="0" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please select visiting purpose." Display="Dynamic">*</asp:RequiredFieldValidator><br />
 <ajaxToolkit:CascadingDropDown ID="ccdvp" runat="server" TargetControlID="ddlvstngpurp"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetVisitinPurpose" />
</td>
<td style="width: 200px">Notes <asp:TextBox ID="txtnotes" Width="400px" Height="90px" TextMode="MultiLine" runat="server"></asp:TextBox></td><td align="left" style="width: 400px" >
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtnotes" ForeColor="Red" SetFocusOnError="true" ValidationGroup="s" ErrorMessage="Please give some notes." Display="Dynamic">*</asp:RequiredFieldValidator><br /></td></tr>

                            
</table>

<script type="text/javascript">
//    $().ready(function () {
//        $("#ddlRname").change(function () {
//            BindValues();
//        });
//    });
//    $().ready(function () {
//        debugger
//        BindValues();
//    });
//    function BindValues() {
//        debugger
//        var ID = $('#ddlRname').val();
//        if (ID != "0") {
//            $("#txtRtelno").html("");
//            $.ajax({
//                type: "POST",
//                url: "/Volta_Underwork/webservices/DCServices.asmx/GetReqTelNo",
//                data: "{ID:'" + ID + "'}",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (msg) {
//                    document.getElementById('txtRtelno').value = msg.d;
//                }
//            });

//            $("#txtRemail").html("");
//            $.ajax({
//                type: "POST",
//                url: "/Volta_Underwork/webservices/DCServices.asmx/GetReqEmail",
//                data: "{ID:'" + ID + "'}",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (msg) {
//                    document.getElementById('txtRemail').value = msg.d;
//                }
//            });
//        }
//        document.getElementById('txtRtelno').value = "";
//        document.getElementById('txtRemail').value = "";
//    }



     function myFunction(ID) {
        //            alert("Hello World!");
        //            var $rows = "";
        //            alert(link);
        //            alert(link1);
        $.ajax({
            type: "POST",
            url: "AccessControl.ascx/PhotoID",
            data: "{ 'ID': '" + ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#divVisitors').jtable('reload');

            }
        });
    }
    function ArriveImg(ID) {
        //            alert("Hello World!");
        //            var $rows = "";
        //            alert(link);
        //            alert(link1);
        $.ajax({
            type: "POST",
            url: "AccessControl.ascx/Status",
            data: "{ 'ID': '" + ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#divVisitors').jtable('reload');
                document.getElementById("btnarrive").disabled = true;
            }
        });
    }
    $(document).ready(function () {
        $('#divVisitors').jtable({
            title: 'Visitors',
            paging: true, //Enables paging
            pageSize: 5, //Actually this is not needed since default value is 10.
            sorting: true, //Enables sorting
            //defaultSorting: 'UnitCategory ASC', //Optional. Default sorting on first load.
            actions: {
                listAction: '/Volta_Underwork/DC/AccessControl.ascx/Get',
                createAction: '/Volta_Underwork/DC/AccessControl.ascx/Create',
                updateAction: '/Volta_Underwork/DC/AccessControl.ascx/Update',
                deleteAction: '/Volta_Underwork/DC/AccessControl.ascx/Delete'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: 'Loading records...',
                noDataAvailable: 'No data available!',
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
                canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {
                ID: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },


                Name: {
                    title: 'Name'
                    
                },
                Company: {
                    title: 'Company'
                                      

                },
                EmailAddress: {
                    title: 'Email Address'

                },
                PhoneNumber: {
                    title: 'PhoneNumber'

                },
                ArriveStatus: {
                
                    title: 'ArriveStatus',
                    display: function (data) {

                        var $ID = data.record.ID;
                        if (data.record.ArriveStatus == null || data.record.ArriveStatus == 0) {

                                              
                            var disable = "disabled"
                            var $link = '<input type="button" value="Arrive" onclick="ArriveImg(' + $ID + ')" /><input type="button" value="Depart" disabled="' + disable + '" />'
                        }
                        else {
                                                           

                            var disable = "disabled"
                            var $link = '<input type="button" value="Arrive" disabled="' + disable + '"  /><input type="button" value="Depart"/>'
                        }

                        return $link;
                    }

                },

                PhotoID: {
                    title: 'Is active',
                    edit: false,
                    create: false,
                    display: function (data) {
                        var $ID = data.record.ID;


                        var $cb = $('<input type="checkbox" id = "checkbox1" onclick="myFunction(' + $ID + ')"/>');
                        if (data.record.PhotoID) {

                            $cb.attr('checked', 'checked');
                        }

                        return $cb;
                    }
                }
            }




//            formCreated: function (event, data) {
//                data.form.find('input[name="PurchaseOrderNo"]').addClass('validate[required]');
//                data.form.find('input[name="UnitsPurchased"]').addClass('validate[required]');

//                data.form.validationEngine();
//            },
//            //Validate form when it is being submitted
//            formSubmitting: function (event, data) {
//                return data.form.validationEngine('validate');
//            },
//            //Dispose validation logic when form is closed
//            formClosed: function (event, data) {
//                data.form.validationEngine('hide');
//                data.form.validationEngine('detach');
//            }

        });
       

//        //Re-load records when user click 'load records' button.
//        $('#LoadRecordsButton').click(function (e) {
//            e.preventDefault();
//            $('#divVisitors').jtable('load', {

//                UnitCategory: $('#UnitCategory').val()
//            });
//        });

        //Load all records when page is first shown
        $('#divVisitors').click();
    });
    </script>

    <div id="divVisitors"></div>



<asp:Button ID="imgbtnSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="s" 
        onclick="imgbtnSubmit_Click" />