<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_Default" Codebehind="Default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">

    $().ready(function () {
        $("#ddlName").change(function () {
            BindValues();
        });
    });
    $().ready(function () {
        BindValues();
    });
    function BindValues() {
        var ID = $('#ddlName').val();
        if (ID != "0") {
            $("#txtReqTelNo").html("");
            $.ajax({
                type: "POST",
                url: "../webservices/DCServices.asmx/GetReqTelNo",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    document.getElementById('txtReqTelNo').value = msg.d;
                }
            });

            $("#txtReqEmailAddress").html("");
            $.ajax({
                type: "POST",
                url: "../webservices/DCServices.asmx/GetReqEmail",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    document.getElementById('txtReqEmailAddress').value = msg.d;
                }
            });
        }
        document.getElementById('txtReqTelNo').value = "";
        document.getElementById('txtReqEmailAddress').value = "";
    }
    $().ready(function () {
        $("#ddlCompany").change(function () {
            $('#txtReqTelNo').val('');
            $('#txtReqEmailAddress').val('');
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table width="100%">
    <tr>
        <td colspan="7">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls"
                DisplayMode="BulletList" />
        </td>
    </tr>
    <tr>
        <td>
            Company
        </td>
        <td colspan="4">
            <asp:DropDownList ID="ddlCompany" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany"
                Category="company" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading site...]"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompany"
                Display="Dynamic" ErrorMessage="Please select company" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </td>
        <td>
            Site
        </td>
        <td>
            <asp:DropDownList ID="ddlSite" runat="server" Width="160px" ClientIDMode="Static">
            </asp:DropDownList>
             <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetSiteByCusomerId" ParentControlID="ddlCompany" LoadingText="[Loading site...]"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSite"
                Display="Dynamic" ErrorMessage="Please select site" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
           
            <asp:HiddenField ID="htid" runat="server" Value="0" />
        </td>
        <%--<td>
            Created Date/Time
            <br />
            <asp:TextBox ID="txtCreatedDate" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
        </td>--%>
    </tr>
    <tr>
        <td>
            Name
        </td>
        <td colspan="4">
            <asp:DropDownList ID="ddlName" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlName"
                Category="Name1" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlCompany" LoadingText="[Loading name...]" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlName"
                Display="Dynamic" ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </td>
        <td>
            Type of Request
        </td>
        <td>
            <asp:DropDownList ID="ddlTypeofRequest" runat="server" Width="140px">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                Display="Dynamic" ErrorMessage="Please select type of request" InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Requesters <br />
             Telephone No
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtReqTelNo" runat="server" Width="220px" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td>
            Status
        </td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server" Width="180px">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                Display="Dynamic" ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </td>
        <%-- <td>
            Requesters Email Address
            <br />
            <asp:TextBox ID="txtRequestersEmailAddress" runat="server" Width="200px"></asp:TextBox>
        </td>--%>
    </tr>
    <tr>
        <td>
            Requesters
            <br />Email Address
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtReqEmailAddress" runat="server" Width="220px" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td>
            Logged Name
        </td>
        <td>
            <asp:TextBox ID="txtLoggedName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Subject
        </td>
        <td colspan="4">
            <asp:DropDownList ID="ddlSubject" runat="server" Width="350px">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdSubject" runat="server" TargetControlID="ddlSubject"
                Category="Subject" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetSubject" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubject"
                Display="Dynamic" ErrorMessage="Please select subject" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </td>
        <td>
            Created Date/Time
        </td>
        <td>
            <asp:TextBox ID="txtCreatedDate" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Details
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtDetails" runat="server" Height="100px" TextMode="MultiLine" Width="450px"></asp:TextBox>
        </td>
       
         <td colspan="4">
          Scheduled Date/Time&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          
          <asp:TextBox ID="txtSeheduledDateTime" runat="server" Width="200px"></asp:TextBox>
            <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" />
            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgSeheduledDate" TargetControlID="txtSeheduledDateTime">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSeheduledDateTime"
                Display="Dynamic" ErrorMessage="Please enter scheduled date " SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                ControlToValidate="txtSeheduledDateTime" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                <br />
            Time Accumulated
            <br />
            <asp:TextBox ID="txtTimeAccumulated" runat="server" Width="110px"></asp:TextBox>(HH:MM)
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTimeAccumulated"
                Display="Dynamic" ErrorMessage="Please enter time accumulated " SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RvTimeAccumulated" runat="server" ControlToValidate="txtTimeAccumulated"
                Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                ValidationGroup="fls" />
            <br />
            Time Worked
            <br />
            <asp:TextBox ID="txtTimeWorked" runat="server" Width="110px"></asp:TextBox>(HH:MM)
            <asp:Button ID="btmSubmit" runat="server" SkinID="btnSubmit" />
        </td>
    </tr>
    <tr>
        <%--<td>
           &nbsp
        </td>--%>
        <%--<td colspan="3">
            <%--<asp:TextBox ID="txtTicketHistory" runat="server" Height="60px" TextMode="MultiLine"
                Width="450px"></asp:TextBox>
                &nbsp
        </td>--%>
       
    </tr>
    <tr>
        <td>
            Upload File(s)
        </td>
        <td colspan="2">
            <asp:FileUpload ID="DocumentsUpload" runat="server" />
            <asp:Button ID="ImgDocumentsUpload" runat="server" SkinID="ImgUpload" OnClick="ImgDocumentsUpload_Click"
                ValidationGroup="fls" />
        </td>
        <td colspan="4">
            Assigned to Department
            <br />
            <asp:DropDownList ID="ddlAssignedtoDept" runat="server" Width="180px" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAssignedtoDept"
                Display="Dynamic" ErrorMessage="Please select assigned to department " InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="ccdAssignedDept" runat="server" TargetControlID="ddlAssignedtoDept"
                Category="AssignedtoDepartment" PromptText="Please select..." PromptValue="0"
                ServicePath="~/webservices/DCServices.asmx" ServiceMethod="GetAssignedtoDepartment" />
            <br />
            Assigned to Name
            <br />
            <asp:DropDownList ID="ddlAssignedtoName" runat="server" Width="180px" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAssignedtoName"
                Display="Dynamic" ErrorMessage="Please select assigned to name " InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="ccdAssignedName" runat="server" TargetControlID="ddlAssignedtoName"
                Category="AssignedtoName" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetAssignedtoName" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GvDocuments" runat="server" Width="80%" EmptyDataText="No Documents found"
                OnRowCommand="GvDocuments_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Uploaded Documents">
                        <ItemTemplate>
                            <asp:HiddenField ID="hid" runat="server" Value='<%#Bind("ID")%>' />
                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("ID")%>' Text='<%#Bind("FileName") %>'
                                runat="server" CommandName="Download"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="8" align="right">
            <asp:Button ID="btnSave" runat="server" AlternateText="Save" SkinID="btnSubmit"
                OnClick="btnSave_Click" ValidationGroup="fls" />&nbsp
            <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" SkinID="btnCancel"
                OnClick="btnCancel_Click" />
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

