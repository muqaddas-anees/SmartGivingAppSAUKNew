<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DC_PermitToWorkCustomer" EnableEventValidation="false" Codebehind="PermitToWorkCustomer.aspx.cs" %>
<%@ Register src="controls/PermitToWorkHistory.ascx" tagname="PermitHistory" tagprefix="uc1" %>
<%@ Register Src="controls/NotesCtrl.ascx" TagName="Notes" TagPrefix="uc3" %>
<%@ Register Src="~/WF/DC/controls/PermitCustomerTab.ascx" TagPrefix="uc1" TagName="PermitCustomerTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:PermitCustomerTab runat="server" id="PermitCustomerTab" Visible="false" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" Runat="Server">
   <%= Resources.DeffinityRes.CustomerPortal%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=permittowork">
<i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
 <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>--%>
   <script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>
    <%-- <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(document.body).find("[id$='lblPageHead']").html('Permit to Work');
        });
    </script>--%>
    <script type="text/javascript" language="javascript">


        function onPopulated() {
            $get('ddlTypeofRequest').disabled = true;
            $get('ddlRequestersCompany').disabled = true;
            $get('ddlRequestersName').disabled = true;
            $get('ddlStatus').disabled = true;
            var id = $('#h_callid').val();
            if (id != 0) {
                $get('ddlSite').disabled = true;
                $get('ddlArea').disabled = true;
                $get('ddlPermit').disabled = true;
            }




        }
        function pageLoad(sender, args) {

            $find("ccdT").add_populated(onPopulated);
            $find("ccdC").add_populated(onPopulated);
            $find("ccdS").add_populated(onPopulated);
            $find("ccdst").add_populated(onPopulated);
            $find("ccda").add_populated(onPopulated);
            $find("ccdtp").add_populated(onPopulated);
            $find("ccdrn").add_populated(onPopulated);

            BindValues();

        }
        $(function () {
            var id = $('#h_callid').val();
            if (id != 0) {
                $('#txtFrom').attr('readonly', true);
                $('#txtFromTime').attr('readonly', true);
                $('#txtTo').attr('readonly', true);
                $('#txtToTime').attr('readonly', true);
                $('#txtArrivalDate').attr('readonly', true);
                $('#txtArrivalTime').attr('readonly', true);
                $('#txtDescription').attr('readonly', true);
                //$('#txtReason').attr('readonly', true);
                $('#txtRequestersTelephoneNo').attr('readonly', true);
                $('#txtRequestersEmailAddress').attr('readonly', true);
                $('#imgDatefrom').hide();
                $('#imgDateto').hide();
                $('#imgadate').hide();
            }
        });

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
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="pw" DisplayMode="BulletList" />
       <asp:Label ID="lblerr" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersCompany%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRequestersCompany" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading company...]" BehaviorID="ccdC" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="ddlRequestersCompany" Display="Dynamic" 
                  ErrorMessage="Please select company" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlSite" runat="server" Width="200px" ClientIDMode="Static">              
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" BehaviorID="ccdst" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlSite" Display="Dynamic" ErrorMessage="Please select site" 
                InitialValue="0" SetFocusOnError="True" ValidationGroup="pw">*</asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_Overdue" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_Charges" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_cid" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="h_callid" runat="server" Value="0" ClientIDMode="Static" />
              <asp:HiddenField ID="h_to" runat="server" />
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersName%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRequestersName" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequestersName"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRequestersCompany" LoadingText="[Loading name...]" BehaviorID="ccdrn" ClientIDMode="Static" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ControlToValidate="ddlRequestersName" Display="Dynamic" 
                  ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.TypeofRequest%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlTypeofRequest" runat="server" Width="220px" ClientIDMode="Static">
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
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtRequestersTelephoneNo" runat="server" Width="220px" ClientIDMode="Static" MaxLength="16"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server"
    TargetControlID="txtRequestersTelephoneNo" ValidChars="0123456789+ "  />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlStatus" Display="Dynamic" 
                ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" BehaviorID="ccdS" SelectedValue="2" />
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersEmail%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtRequestersEmailAddress" runat="server" Width="220px" MaxLength="250"  
                ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Dateofworks%> <%= Resources.DeffinityRes.FromDate%> </label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtFrom" runat="server" Width="90px" 
                               ClientIDMode="Static" SkinID="txtCalender"></asp:TextBox>
              <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDatefrom" TargetControlID="txtFrom">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFrom" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgDatefrom" runat="server"  
                   SkinID="Calender" ClientIDMode="Static" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                   ControlToValidate="txtFrom" Display="Dynamic" 
                   ErrorMessage="Please enter from date" SetFocusOnError="True" 
                   ValidationGroup="pw">*</asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtFrom" 
                    ValidationGroup="pw" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                         <asp:TextBox ID="txtFromTime" runat="server" Width="50px" ClientIDMode="Static" SkinID="Time"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
	</div>
          <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Area%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlArea" runat="server" Width="200px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdArea" runat="server" TargetControlID="ddlArea"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStorageLocation" ParentControlID="ddlRequestersCompany" LoadingText="[Loading area...]" BehaviorID="ccda" ClientIDMode="Static" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="ddlArea" Display="Dynamic" 
                ErrorMessage="Please select area" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
	
</div>
     <div class="form-group row">
     <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtTo" runat="server" Width="90px" 
                               ClientIDMode="Static" SkinID="txtCalender"></asp:TextBox>
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDateto" TargetControlID="txtTo">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTo" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgDateto" runat="server"  
                       SkinID="Calender" ClientIDMode="Static" />
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
                 <asp:TextBox ID="txtToTime" runat="server" Width="50px" ClientIDMode="Static" SkinID="Time"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.TypeofPermit%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlPermit" runat="server" Width="200px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdPermit" runat="server" TargetControlID="ddlPermit"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetPermitType" LoadingText="[Loading permit...]" BehaviorID="ccdtp" ClientIDMode="Static" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="ddlPermit" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="pw">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ArrivalDateTime%></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ProjectTitle%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtArrivalDate" runat="server" Width="90px" ClientIDMode="Static" SkinID="txtCalender"></asp:TextBox>
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
                        <asp:TextBox ID="txtArrivalTime" runat="server" Width="50px" ClientIDMode="Static" SkinID="Time"></asp:TextBox>(HH:MM)
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtArrivalTime"
                    Display="Dynamic" ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="pw" />
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DescriptionofWorks%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtDescription" runat="server" Height="100px" TextMode="MultiLine" ClientIDMode="Static" Width="420px"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Uploadfiles%></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-6" id="trupload" runat="server">
           <label class="col-sm-3 control-label">Upload File(s)</label>
           <div class="col-sm-9">
               <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None" ScrollBars="Auto">
                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" />
            </asp:Panel>                         
             <asp:Button ID="ImgDeskImageUpload" runat="server" SkinID="ImgUpload" 
                    onclick="ImgDeskImageUpload_Click" ValidationGroup="pw" Visible="false" />  
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9" runat="server" id="tdbuttons">
                <asp:Button ID="btnSave" runat="server" AlternateText="Save" 
                     SkinID="btnSubmit" onclick="btnSave_Click" ValidationGroup="pw" />&nbsp
                    <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" 
                     SkinID="btnCancel" onclick="btnCancel_Click" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>

      File Location<br />
                <asp:GridView ID="GvDocuments" runat="server" Width="100%" EmptyDataText="No Documents found"
                OnRowCommand="GvDocuments_RowCommand" DataKeyNames="ID,ContentType,FileName">
                <Columns>
                    <asp:TemplateField HeaderText="Uploaded Documents">
                        <ItemTemplate>                            
                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                runat="server" CommandName="Download"></asp:LinkButton>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>    
            
     <asp:Panel ID="pnlNotes" runat="server" Visible="false">
                    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                        Notes</div>
                    <uc3:Notes ID="Notes1" runat="server" />
                </asp:Panel>
      <div>
      <uc1:PermitHistory ID="ctr_history" runat="server" />
      </div>
</asp:Content>

