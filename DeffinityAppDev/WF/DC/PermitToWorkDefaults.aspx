<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_PermitToWorkDefaults" EnableEventValidation="false"  Codebehind="PermitToWorkDefaults.aspx.cs" %>

<%@ Register Src="~/WF/Admin/controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc5" %>
 <%@ Register Src="controls/SADropDown.ascx" TagName="SA" TagPrefix="uc6" %>
 <%@ Register Src="controls/SecurityAccessMail.ascx" TagName="SecurityAccessMail" TagPrefix="uc1" %>
   <%@ Register Src="controls/EmailFooter.ascx" TagName="EmailFooter" TagPrefix="uc3" %>
<%@ Register Src="~/WF/DC/controls/OurSite.ascx" TagPrefix="uc1" TagName="OurSite" %>
<%@ Register Src="~/WF/DC/controls/HealthandSafetyNoticeinPermitsCntl.ascx" TagName="HealthandSafetyNoticeinPermitsCntl" TagPrefix="uc4" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
   
   <%= Resources.DeffinityRes.PermittoWork%> 
   </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.AdminDropdownLists%>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
      <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=PermittoWork">
<i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="form-group row">
      <div class="col-md-12">
          
	<uc6:SA ID="sa1" runat="server" Visible="false" />
	</div>
	
</div>
    
<div class="form-group row">
      <div class="col-md-6">
           <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.PermitType%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>
                                        <div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblptmsg" runat="server"></asp:Label>
	</div>
</div>
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.PermitType%></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList ID="ddlType" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdPermitType" runat="server" TargetControlID="ddlType"
                                                        Category="Type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetPermitType" LoadingText="[Loading type...]" />
                                                    <asp:TextBox ID="txtType" runat="server" CssClass="txt_field" SkinID="txt_80" ValidationGroup="pta"></asp:TextBox>
               <asp:LinkButton ID="imb_DeleteType" runat="server" SkinID="BtnLinkDelete"
                                                         ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                        OnClick="imb_DeleteType_Click" ValidationGroup="pte" />
            </div>
	</div>
</div>
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:Button ID="imb_AddType" runat="server" SkinID="btnAdd"
                                                        OnClick="imb_AddType_Click" />
                                                    <asp:Button ID="imb_SubmitType" runat="server" SkinID="btnSubmit"
                                                         OnClick="imb_SubmitType_Click" ValidationGroup="pta" />
                                                    <asp:Button ID="imb_EditType" runat="server" SkinID="btnEdit" 
                                                         ValidationGroup="pte" OnClick="imb_EditType_Click" />
                                                    <asp:Button ID="imb_CancelType" runat="server" SkinID="btnCancel"
                                                         OnClick="imb_CancelType_Click" />
               <asp:HiddenField ID="h_ptId" runat="server" Value="0" />
            </div>
	</div>
</div>
                                        
<div class="row">
          <div class="col-md-12">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlType"
                                                        Display="Dynamic" ErrorMessage="Please select type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="pte"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtType"
                                                        Display="Dynamic" ErrorMessage="Please enter type" SetFocusOnError="True" ValidationGroup="pta"></asp:RequiredFieldValidator>
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
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
<div class="row">
          <div class="col-md-12">
 <strong>Assign Checklist </strong> 
<hr class="no-top-margin" />
	</div>
</div>
                                        
<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblcmsg" runat="server"></asp:Label>
	</div>
</div>
            <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CheckList%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlChecklist" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdChecklist" runat="server" TargetControlID="ddlChecklist"
                                                        Category="Type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetChecklists" LoadingText="[Loading list...]" />  
            </div>
	</div>
</div>   
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="btnProjectClassApply" runat="server" 
                                             SkinID="btnApply" onclick="btnProjectClassApply_Click" ValidationGroup="ptc" />
            </div>
	</div>
</div>    
                                        
<div class="row">
          <div class="col-md-12">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlChecklist"
                                                        Display="Dynamic" ErrorMessage="Please select list" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="ptc"></asp:RequiredFieldValidator>
	</div>
</div>                    
                                      
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnProjectClassApply" EventName="click" />                                        
                                    </Triggers>
                                </asp:UpdatePanel>
	</div>
	
</div>
    
<div class="form-group row">
      <div class="col-md-6">
       
                
                        <uc1:SecurityAccessMail ID="sae1" runat="server" />
	</div>
	<div class="col-md-6">
           <uc3:EmailFooter ID="EmailFooter1" runat="server" RequestTypeID="2" />
	</div>
	
</div>

</asp:Content>


