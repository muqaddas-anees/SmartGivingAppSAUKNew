<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_AdminDropDown" EnableEventValidation="false" Codebehind="DeliveryDefaults.aspx.cs" %>
    
<%@ Register Src="controls/RequestType.ascx" TagName="RequestType" TagPrefix="rt" %>
<%@ Register Src="controls/Status.ascx" TagName="Status" TagPrefix="st" %>
<%@ Register Src="~/WF/Admin/controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc5" %>
 <%@ Register Src="controls/SADropDown.ascx" TagName="SA" TagPrefix="uc6" %>
  <%@ Register Src="controls/StorageLocation.ascx" TagName="StorageLocation" TagPrefix="sl" %>
  <%@ Register Src="controls/SecurityAccessMail.ascx" TagName="SecurityAccessMail" TagPrefix="uc1" %>
  <%@ Register Src="controls/OurSite.ascx" TagName="OurSite" TagPrefix="ucSite" %>
  <%@ Register Src="controls/EmailFooter.ascx" TagName="EmailFooter" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
   <%= Resources.DeffinityRes.AdminDropdownLists%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.DeliveryDefaults%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
     <script  type="text/javascript">
       $(document).ready(function () {
           $('#navTab').hide();
       });
       </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    
<div class="form-group row">
          <div class="col-md-12">
               <uc6:SA ID="sa1" runat="server"  Visible="false"  />
	</div>
</div>
 <div class="form-group row">
      <div class="col-md-6">
          <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
<div class="form-group row">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.DeliveryType%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                                       
<div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lbldtmsg" runat="server"></asp:Label>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DeliveryType%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddl_Type" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdDeliveryType" runat="server" TargetControlID="ddl_Type"
                                                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetDeliveryType" LoadingText="[Loading type...]" />
                                                    <asp:TextBox ID="txt_Type" runat="server" CssClass="txt_field" SkinID="txt_80" ValidationGroup="dta"></asp:TextBox>
                <asp:LinkButton ID="imb_DeleteType" runat="server" SkinID="BtnLinkDelete"
                                                         ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                        OnClick="imb_DeleteType_Click" ValidationGroup="dte" /><asp:HiddenField ID="h_dtId" runat="server" Value="0" />
            </div>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8 form-inline">
                <asp:Button ID="imb_AddType" runat="server" SkinID="btnAdd"
                                                        OnClick="imb_AddType_Click" />
                                                    <asp:Button ID="imb_SubmitType" runat="server" SkinID="btnSubmit"
                                                        OnClick="imb_SubmitType_Click" ValidationGroup="dta" />
                                                    <asp:Button ID="imb_EditType" runat="server" SkinID="btnEdit"
                                                        Style="border-width: 0px;" ValidationGroup="dte" OnClick="imb_EditType_Click" />
                                                    <asp:Button ID="imb_CancelType" runat="server" SkinID="btnCancel"
                                                        Style="border-width: 0px;" OnClick="imb_CancelType_Click" />
            </div>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_Type"
                                                        Display="Dynamic" ErrorMessage="Please select type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="dte"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Type"
                                                        Display="Dynamic" ErrorMessage="Please enter type" SetFocusOnError="True" ValidationGroup="dta"></asp:RequiredFieldValidator>
	</div>
</div>
                                       
                                        
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imb_AddType" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_SubmitType" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_EditType" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_CancelType" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
	</div>
	<div class="col-md-6">
           <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
<div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Condition%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                                       
<div class="form-group row">
          <div class="col-md-12">
               <asp:Label ID="lblcmsg" runat="server"></asp:Label>
	</div>
</div>
                                      
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Condition%></label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddl_Condition" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdCondition" runat="server" TargetControlID="ddl_Condition"
                                                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetCondition" LoadingText="[Loading condition...]" />
                                                    <asp:TextBox ID="txt_Condition" runat="server" CssClass="txt_field" SkinID="txt_80"
                                                        ValidationGroup="ca"></asp:TextBox>
                <asp:LinkButton ID="imb_DeleteCondition" runat="server" SkinID="BtnLinkDelete"
                                                         ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                        OnClick="imb_DeleteCondition_Click" ValidationGroup="ce" />
            </div>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
               <asp:HiddenField ID="h_cID" runat="server" />
               <asp:Button ID="imb_AddCondition" runat="server" SkinID="btnAdd" OnClick="imb_AddCondition_Click" />
                                                    <asp:Button ID="imb_SubmitCondition" runat="server" SkinID="btnSubmit" OnClick="imb_SubmitCondition_Click" ValidationGroup="ca" />
                                                    <asp:Button ID="imb_EditCondition" runat="server" SkinID="btnEdit" ValidationGroup="ce" OnClick="imb_EditCondition_Click" />
                                                    <asp:Button ID="imb_CancelCondition" runat="server" SkinID="btnCancel" OnClick="imb_CancelCondition_Click" />
            </div>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_Condition"
                                                        Display="Dynamic" ErrorMessage="Please select condition" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="ce"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_Condition"
                                                        Display="Dynamic" ErrorMessage="Please enter condition" SetFocusOnError="True"
                                                        ValidationGroup="ca"></asp:RequiredFieldValidator>
	</div>
</div>

                                     
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imb_AddCondition" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_SubmitCondition" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_EditCondition" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_CancelCondition" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <sl:StorageLocation ID="sl1" runat="server" />
	</div>
	<div class="col-md-6">
          <%-- <div class="sec_header" style="width: 600px">
                Delivery Notification Distribution List</div>--%>
                        <uc1:SecurityAccessMail ID="sae1" runat="server" />
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <ucSite:OurSite ID="ourSite1" runat="server" />
	</div>
	<div class="col-md-6">
           <asp:UpdatePanel ID="up6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.DeliveryDefaults%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            
<div class="form-group row">
          <div class="col-md-12"> <asp:Label ID="lbldmsg" runat="server"></asp:Label>
	</div>
</div>                            
                                       <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.MaximumWeight%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txt_Weight" runat="server" SkinID="txt_90" ValidationGroup="la"></asp:TextBox>
            </div>
	</div>
</div>
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.OverdueDays%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txt_Date" runat="server" SkinID="txt_90" ValidationGroup="la"></asp:TextBox>
            </div>
	</div>
</div>

                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StorageCharges%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txt_SCharges" runat="server" SkinID="txt_90"
                                                        ValidationGroup="la"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_SCharges" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
            </div>
	</div>
</div>

                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.AutoCloseDays%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txt_ACperiod" runat="server" SkinID="txt_90"
                                                        ValidationGroup="la"></asp:TextBox>
            </div>
	</div>
</div>

                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.HeavyItemNotice%></label>
           <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtNotice" TextMode="MultiLine" Width="600px" Height="400px" />
                                                <ajaxToolkit:HtmlEditorExtender ID="ht1" TargetControlID="txtNotice" runat="server"    />
            </div>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8 form-inline">
               <asp:Button ID="imb_AddDefaults" runat="server" SkinID="btnAdd" OnClick="imb_AddDefaults_Click" />
                                                    <asp:Button ID="imb_SubmitDefaults" runat="server" SkinID="btnSubmit" OnClick="imb_SubmitDefaults_Click" ValidationGroup="dd" />
                                                    <asp:Button ID="imb_EditDefaults" runat="server" SkinID="btnEdit" OnClick="imb_EditDefaults_Click" />
                                                    <asp:Button ID="imb_CancelDefaults" runat="server" SkinID="btnCancel" OnClick="imb_CancelDefaults_Click" />
            </div>
	</div>
</div>
                                        
<div class="form-group row">
          <div class="col-md-12">
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_Weight"
                                                        Display="Dynamic" ErrorMessage="Please enter weight" SetFocusOnError="True" ValidationGroup="dd"></asp:RequiredFieldValidator><br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_Date"
                                                        Display="Dynamic" ErrorMessage="Please enter Days" SetFocusOnError="True" ValidationGroup="dd"></asp:RequiredFieldValidator><br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_SCharges"
                                                        Display="Dynamic" ErrorMessage="Please enter Charge" SetFocusOnError="True" ValidationGroup="dd"></asp:RequiredFieldValidator><br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_ACperiod"
                                                        Display="Dynamic" ErrorMessage="Please enter AutoClosePeriod" SetFocusOnError="True"
                                                        ValidationGroup="dd"></asp:RequiredFieldValidator>
	</div>
</div>

                                      
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imb_AddDefaults" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_SubmitDefaults" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_EditDefaults" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_CancelDefaults" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
          <uc3:EmailFooter ID="EmailFooter1" runat="server" RequestTypeID="1" />
	</div>
	<div class="col-md-6">
          
	</div>
</div>
  
   
</asp:Content>
