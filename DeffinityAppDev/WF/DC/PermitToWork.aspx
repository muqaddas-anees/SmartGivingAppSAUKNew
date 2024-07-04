<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_PermitToWork" EnableEventValidation="false" Codebehind="PermitToWork.aspx.cs" %>

<%@ Register Src="controls/PermitToWorkHistory.ascx" TagName="History" TagPrefix="hi" %>
<%@ Register Src="~/WF/DC/controls/PermitTab.ascx" TagPrefix="hi" TagName="PermitTab" %>


<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <hi:PermitTab runat="server" ID="PermitTab" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" runat="Server">
    <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=PermittoWork" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
 <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            $(document.body).find("[id$='lblPageHead']").html('Permit to Work');
        });
    </script>--%>
    <script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>
    <script type="text/javascript">


        function onPopulated() {
            $get('ddlTypeofRequest').disabled = true;
            var id = $('#h_cid').val();
            if (id == 0) {
                $get('ddlStatus').disabled = true;
            } else if (id != 0) {
                var status = $("#ddlStatus option:selected").text();
                if (status == "Closed") {
                    $get('ddlStatus').disabled = true;
                    $get('ddlSite').disabled = true;
                    $get('ddlRequestersCompany').disabled = true;
                    $get('ddlRequestersName').disabled = true;
                    $get('ddlTypeofRequest').disabled = true;
                    $get('ddlArea').disabled = true;
                    $get('ddlPermit').disabled = true;
                    $('#txtFrom').attr('readonly', true);
                    $('#txtFromTime').attr('readonly', true);
                    $('#txtTo').attr('readonly', true);
                    $('#txtToTime').attr('readonly', true);
                    $('#txtArrivalDate').attr('readonly', true);
                    $('#txtArrivalTime').attr('readonly', true);
                    $('#txtDescription').attr('readonly', true);
                    $('#txtNotes').attr('readonly', true);
                    $('#txtLoggedDateTime').attr('readonly', true);
                    $('#txtLoggedTime').attr('readonly', true);
                    $('#txtReason').attr('readonly', true);
                    $('#txtTo').attr('readonly', true);
                    $('#txtTo').attr('readonly', true);
                    $('#DocumentsUploadnew').hide();
                    $('#ImgDeskImageUpload').hide();
                    $('#imgDatefrom').hide();
                    $('#imgDateto').hide();
                    $('#imgadate').hide();
                }
            }
        }
        function pageLoad(sender, args) {

            $find("ccdT").add_populated(onPopulated);
            $find("ccdS").add_populated(onPopulated);
        }

        $().ready(function () {
            $("#ddlRequestersName").change(function () {
                BindValues();
            });
        });
        $().ready(function () {
            BindValues();
        });
        function BindValues() {
            var ID = $('#ddlRequestersName').val();
            if (ID != "0") {
                $("#txtReqTelNo").html("");
                $.ajax({
                    type: "POST",
                    url: "/WF/DC/webservices/DCServices.asmx/GetContactDetails",
                    data: "{rid:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        document.getElementById('txtRequestersTelephoneNo').value = msg.d.Telephone;
                        document.getElementById('txtRequestersEmailAddress').value = msg.d.Email;
                    }
                });
            }
            document.getElementById('txtRequestersTelephoneNo').value = "";
            document.getElementById('txtRequestersEmailAddress').value = "";
        }
        $().ready(function () {
            $("#ddlRequestersCompany").change(function () {
                $('#txtRequestersTelephoneNo').val('');
                $('#txtRequestersEmailAddress').val('');
            });
        });

</script>

    <div class="form-group row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="pw" DisplayMode="BulletList" />
       <asp:Label ID="lblerr" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
	</div>
</div>
    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersCustomer%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlRequestersCompany" runat="server" SkinID="ddl_90" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="ddlRequestersCompany" Display="Dynamic" 
                  ErrorMessage="Please select customer" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90" ClientIDMode="Static">              
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlSite" Display="Dynamic" ErrorMessage="Please select site" 
                InitialValue="0" SetFocusOnError="True" ValidationGroup="pw">*</asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_Overdue" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_Charges" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_cid" runat="server" Value="0" ClientIDMode="Static"/>
              <asp:HiddenField ID="h_to" runat="server" />
            </div>
	</div>
</div>
    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequesterName%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlRequestersName" runat="server" SkinID="ddl_90" ClientIDMode="Static">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequestersName"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRequestersCompany" LoadingText="[Loading name...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ControlToValidate="ddlRequestersName" Display="Dynamic" 
                  ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.TypeofRequest%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlTypeofRequest" runat="server" SkinID="ddl_90" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading request...]" SelectedValue="2" ClientIDMode="Static" BehaviorID="ccdT"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="ddlTypeofRequest" Display="Dynamic" 
                ErrorMessage="Please select request" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtRequestersTelephoneNo" runat="server" SkinID="txt_90" ClientIDMode="Static" ReadOnly="True" MaxLength="16"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server"
    TargetControlID="txtRequestersTelephoneNo" ValidChars="0123456789+ "  />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_90" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlStatus" Display="Dynamic" 
                ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" SelectedValue="2" BehaviorID="ccdS" />
            </div>
	</div>
</div>

    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersEmailAddress%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtRequestersEmailAddress" runat="server" Width="250px"  
                ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Area%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlArea" runat="server" Width="200px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdArea" runat="server" TargetControlID="ddlArea"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStorageLocation" ParentControlID="ddlRequestersCompany" LoadingText="[Loading area...]" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="ddlArea" Display="Dynamic" 
                ErrorMessage="Please select area" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>


 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Dateofworks%></label>
           <div class="col-sm-8">
              <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-12 form-inline">
                <asp:TextBox ID="txtFrom" runat="server" SkinID="Date" 
                               ClientIDMode="Static"></asp:TextBox>
              <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDatefrom" TargetControlID="txtFrom">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFrom" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgDatefrom" runat="server" SkinID="Calender" ClientIDMode="Static" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                   ControlToValidate="txtFrom" Display="Dynamic" 
                   ErrorMessage="Please enter from date" SetFocusOnError="True" 
                   ValidationGroup="pw">*</asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtFrom" 
                    ValidationGroup="pw" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                     <asp:TextBox ID="txtFromTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
        <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.ToDate%></label>
        <div class="col-sm-12 form-inline">
            <asp:TextBox ID="txtTo" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDateto" TargetControlID="txtTo">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTo" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgDateto" runat="server" SkinID="Calender" ClientIDMode="Static" />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                       ControlToValidate="txtTo" Display="Dynamic" 
                       ErrorMessage="Please enter to date" SetFocusOnError="True" 
                       ValidationGroup="pw">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvdate" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtTo" 
                    ValidationGroup="pw" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                      <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtTo"
                ValidationGroup="pw" ErrorMessage="To Date should be greater than or equal to From date." Operator="GreaterThanEqual"
                Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True" ControlToCompare="txtFrom"></asp:CompareValidator>
                 <asp:TextBox ID="txtToTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
            </div>
	</div>
	<div class="col-md-6">
          <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.TypeofPermit%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPermit" runat="server" Width="200px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdPermit" runat="server" TargetControlID="ddlPermit"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetPermitType" LoadingText="[Loading permit...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="ddlPermit" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ArrivalDateTime%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtArrivalDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgadate" TargetControlID="txtArrivalDate">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender1" runat="server" TargetControlID="txtArrivalDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgadate" runat="server"  
                       SkinID="Calender" ClientIDMode="Static" />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                       ControlToValidate="txtArrivalDate" Display="Dynamic" 
                       ErrorMessage="Please enter arrival date/time" SetFocusOnError="True" 
                       ValidationGroup="pw">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtArrivalDate" 
                    ValidationGroup="pw" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                     <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="Arrival date must be future date" ControlToValidate="txtArrivalDate" 
                    ValidationGroup="pw" Type="Date" Operator="GreaterThanEqual" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                           <asp:TextBox ID="txtArrivalTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtArrivalTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.LoggedDateTime%></label>
           <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txtLoggedDateTime" runat="server" SkinID="Date"
                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtLoggedTime" runat="server" SkinID="Time" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>

    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.DescriptionofWorks%></label>
           <div class="col-sm-12">
               <asp:TextBox ID="txtDescription" runat="server" Height="100px" TextMode="MultiLine" 
                Width="500px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-10 control-label"><%= Resources.DeffinityRes.ReasonwhyAccessDenied%></label>
           <div class="col-sm-12">
                <asp:TextBox ID="txtReason" runat="server" Height="100px" TextMode="MultiLine" 
                Width="500px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
    
 <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-10 control-label"><%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-12">
               <asp:TextBox ID="txtNotes" runat="server" Height="100px" TextMode="MultiLine" 
                Width="500px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Uploadfiles%></label>
           <div class="col-sm-10 form-inline">
               <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None" ScrollBars="Auto">
                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" ClientIDMode="Static" />
            </asp:Panel>                    
             <asp:Button ID="ImgDeskImageUpload" runat="server" SkinID="ImgUpload" 
                    onclick="ImgDeskImageUpload_Click" ValidationGroup="pw" Visible="false" ClientIDMode="Static" /> 
            </div>
	</div>
</div>
    <asp:GridView ID="GvDocuments" runat="server" Width="100%" EmptyDataText="No Documents found"
                OnRowCommand="GvDocuments_RowCommand" DataKeyNames="ID,ContentType,FileName">
                <Columns>
                    <asp:TemplateField HeaderText="Uploaded Documents">
                        <ItemTemplate>                            
                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                runat="server" CommandName="Download"></asp:LinkButton>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemStyle Width="5%" />
                        <ItemTemplate>
                            <asp:LinkButton ID="ImgDel" runat="server" CausesValidation="false" SkinID="ImgSymDel"
                                CommandArgument='<%#Bind("DocumentID")%>' CommandName="DeleteFile" OnClientClick="return confirm('Do you want to delete this Document?');"
                                ToolTip="delete" />
                        </ItemTemplate>
                        </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
    <div class="form-group row">
          <div class="col-md-12 form-inline">
              <asp:Button ID="btnSave" runat="server" AlternateText="Save" 
                     SkinID="btnSubmit" onclick="btnSave_Click" ValidationGroup="pw" />&nbsp
                    <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" 
                     SkinID="btnCancel" onclick="btnCancel_Click" />
	</div>
</div>
 
<div class="form-group row">
          <div class="col-md-12">
              <iframe id="iframe1" runat="server" width="100%" height="370px" marginwidth="0" marginheight="0" scrolling="no" frameborder="0"></iframe>
	</div>
</div>
</asp:Content>


