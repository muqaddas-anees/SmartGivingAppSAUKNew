<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TypeofRequestCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.TypeofRequestCtrl" %>
<asp:UpdateProgress ID="uprogressCategory" runat="server" AssociatedUpdatePanelID="upnlCategory">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlCategory" runat="server" UpdateMode="Conditional">
  
    <ContentTemplate>
          <%--<div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.TypeofRequest%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div class="form-group row">
        <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
         <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="type" DisplayMode="BulletList" />
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="c" DisplayMode="BulletList" />
        
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRequestType"
                        Display="Dynamic" ErrorMessage="Please select type of request" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_Add"></asp:RequiredFieldValidator>
                  
        </div>
      <div class="form-group row" >
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Type of Request</label>
                                      <div class="col-sm-8 form-inline">
                                          
                    <asp:DropDownList ID="ddlRequestType" runat="server" SkinID="ddl_50" ClientIDMode="Static">
                      <%--  <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                        <asp:ListItem Text="Faults" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Service Request" Value="2"></asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfRequestTypeId" runat="server" Value="0" />
                    <ajaxToolkit:CascadingDropDown ID="ccdTypeOfRequest" runat="server" TargetControlID="ddlRequestType"
                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestType" LoadingText="[Loading...]" />
                     <asp:LinkButton ID="btnAddTypeOfRequest"  runat="server"
                                SkinID="BtnLinkAdd" CausesValidation="False" OnClick="btnAddTypeOfRequest_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="btnEditTypeOfRequest" runat="server" ValidationGroup="Edit_catelog"
                                SkinID="BtnLinkEdit"  OnClick="btnEditTypeOfRequest_Click">
                            </asp:LinkButton>


                 
                   
                           <asp:LinkButton ID="btnDeleteTypeOfRequest" runat="server"
                                SkinID="BtnLinkDelete" OnClick="btnDeleteTypeOfRequest_Click" OnClientClick="return confirm('Do you want to delete the record?');" />
                    <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlRequestType" InitialValue="0" ErrorMessage="Please select type of request" ValidationGroup="c" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTypeOfRequest" runat="server" SkinID="txt_50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTypeOfRequest" runat="server" ControlToValidate="txtTypeOfRequest" ErrorMessage="Please enter type of request" ValidationGroup="type" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:LinkButton ID="btnSaveTypeOfRequest" runat="server" ValidationGroup="type" ImageAlign="AbsMiddle"
                                 SkinID="BtnLinkUpdate" OnClick="btnSaveTypeOfRequest_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelTypeOfRequest" runat="server" ToolTip="Cancel"  ImageAlign="AbsMiddle"
                                CausesValidation="False" SkinID="BtnLinkCancel" OnClick="btnCancelTypeOfRequest_Click"></asp:LinkButton>
                     <%--<asp:Button ID="btnCopyToAllCustomer" runat="server" SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomer_Click" ValidationGroup="c" Visible="false"/>--%>
					</div>
				</div>
</div>
        </ContentTemplate>
    
    </asp:UpdatePanel>