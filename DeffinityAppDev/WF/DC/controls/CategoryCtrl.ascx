<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_CategoryCtrl" Codebehind="CategoryCtrl.ascx.cs" %>
<asp:UpdateProgress ID="uprogressCategory" runat="server" AssociatedUpdatePanelID="upnlCategory">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlCategory" runat="server" UpdateMode="Conditional">
  
    <ContentTemplate>
         <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong> <%= Deffinity.systemdefaults.GetCategoryName() %> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div class="form-group row mb-6">
        <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
         <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="type" DisplayMode="BulletList" />
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="c" DisplayMode="BulletList" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                        Display="Dynamic" ErrorMessage="Please select category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_e"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCategory"
                        Display="Dynamic" ErrorMessage="Please enter category" SetFocusOnError="True" ValidationGroup="cat"></asp:RequiredFieldValidator>
                   <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRequestType"
                        Display="Dynamic" ErrorMessage="Please select type of request" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_Add"></asp:RequiredFieldValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubCategory"
                        Display="Dynamic" ErrorMessage="Please select sub category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="subcat_e"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSubCategory"
                        Display="Dynamic" ErrorMessage="Please enter sub category" SetFocusOnError="True" ValidationGroup="subcat"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCategory"
                        Display="Dynamic" ErrorMessage="Please select category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="subcat_Add"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlModel"
                        Display="Dynamic" ErrorMessage="Please select model" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="model_e"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtModel"
                        Display="Dynamic" ErrorMessage="Please enter model" SetFocusOnError="True" ValidationGroup="model"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSubCategory"
                        Display="Dynamic" ErrorMessage="Please select sub category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="model_Add"></asp:RequiredFieldValidator>
        </div>
      <div class="form-group row mb-6" style="display:none;visibility:hidden;">
                                 
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
                    <%--<asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlRequestType" InitialValue="0" ErrorMessage="Please select type of request" ValidationGroup="c" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    <asp:TextBox ID="txtTypeOfRequest" runat="server" SkinID="txt_50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTypeOfRequest" runat="server" ControlToValidate="txtTypeOfRequest" ErrorMessage="Please enter type of request" ValidationGroup="type" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:LinkButton ID="btnSaveTypeOfRequest" runat="server" ValidationGroup="type" ImageAlign="AbsMiddle"
                                 SkinID="BtnLinkUpdate" OnClick="btnSaveTypeOfRequest_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelTypeOfRequest" runat="server" ToolTip="Cancel"  ImageAlign="AbsMiddle"
                                CausesValidation="False" SkinID="BtnLinkCancel" OnClick="btnCancelTypeOfRequest_Click"></asp:LinkButton>
                     <asp:Button ID="btnCopyToAllCustomer" runat="server" SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomer_Click" ValidationGroup="c" Visible="false"/>
					</div>
				
</div>
        <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-4 control-label">  <%= Deffinity.systemdefaults.GetCategoryName() %></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_50">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfId" runat="server" Value="0" />
                     <asp:LinkButton ID="imb_Add" runat="server" SkinID="BtnLinkAdd"
                         OnClick="imb_Add_Click" ValidationGroup="cat_Add" />
                      <asp:LinkButton ID="imb_Edit" runat="server" SkinID="BtnLinkEdit" 
                        ValidationGroup="cat_e" OnClick="imb_Edit_Click" />
                    <asp:LinkButton ID="imb_Delete" runat="server" SkinID="BtnLinkDelete" ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                        ValidationGroup="dte" OnClick="imb_Delete_Click" />
                    <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading ...]" ParentControlID="ddlRequestType" />
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="txt_field" SkinID="txt_50" ValidationGroup="cat"  MaxLength="255" ></asp:TextBox>
                     <asp:LinkButton ID="imb_Submit" runat="server" SkinID="BtnLinkUpdate"
                        ValidationGroup="cat"
                        OnClick="imb_Submit_Click" />
                    <asp:LinkButton ID="imb_Cancel" runat="server" SkinID="BtnLinkCancel"
                         OnClick="imb_Cancel_Click" />
					</div>
				
</div>
        <div class="form-group row mb-6">
                               
                                       <label class="col-sm-4 control-label"> <%= Deffinity.systemdefaults.GetSubCategoryName() %></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="ddlSubCategory" runat="server" SkinID="ddl_50" ></asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetSubCategoryAdmin" LoadingText="[Loading...]" ParentControlID="ddlCategory" />
                    <asp:HiddenField ID="hfSubCategoryId" runat="server" Value="0" />
                     <asp:LinkButton ID="btnAddSubCategory"  runat="server"
                                SkinID="BtnLinkAdd"  ValidationGroup="subcat_Add" OnClick="btnAddSubCategory_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="btnEditSubCategory" runat="server"  ValidationGroup="subcat_e"
                                SkinID="BtnLinkEdit" OnClick="btnEditSubCategory_Click" >
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteSubCategory" runat="server" OnClientClick="return confirm('Do you want to delete the record?');"
                                SkinID="BtnLinkDelete" OnClick="btnDeleteSubCategory_Click"  />
                    <asp:TextBox ID="txtSubCategory" runat="server" SkinID="txt_50"  MaxLength="255" ></asp:TextBox>
                    
                            <asp:LinkButton ID="btnSubmitSubCategory" runat="server" ValidationGroup="subcat" ImageAlign="AbsMiddle"
                                 SkinID="BtnLinkUpdate" OnClick="btnSubmitSubCategory_Click"  ></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelSubCategory" runat="server" ToolTip="Cancel"  ImageAlign="AbsMiddle"
                                CausesValidation="False" SkinID="BtnLinkCancel" OnClick="btnCancelSubCategory_Click" ></asp:LinkButton>
					
				</div>
</div>
         <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Model%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="ddlModel" runat="server" SkinID="ddl_50" ></asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdModel" runat="server" TargetControlID="ddlModel"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetModel" LoadingText="[Loading...]" ParentControlID="ddlSubCategory" />
                    <asp:HiddenField ID="hfModelid" runat="server" Value="0" />
                     <asp:LinkButton ID="btnAddModel"  runat="server"
                                SkinID="BtnLinkAdd"  ValidationGroup="model_Add" OnClick="lbtnAddModel_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnEditModel" runat="server"  ValidationGroup="model_e"
                                SkinID="BtnLinkEdit" OnClick="lbtnEditModel_Click" >
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteModel" runat="server" OnClientClick="return confirm('Do you want to delete the record?');"
                                SkinID="BtnLinkDelete" OnClick="lbtnDeleteModel_Click" />
                    <asp:TextBox ID="txtModel" runat="server" SkinID="txt_50" MaxLength="255" ></asp:TextBox>
                    
                            <asp:LinkButton ID="btnSubmitModel" runat="server" ValidationGroup="model" 
                                 SkinID="BtnLinkUpdate" OnClick="lbtnUpdateModel_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelModel" runat="server" ToolTip="Cancel" 
                                CausesValidation="False" SkinID="BtnLinkCancel" OnClick="lbtnCancel_Click" ></asp:LinkButton>
					</div>
				
</div>
       
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imb_Add" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_Submit" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_Edit" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_Cancel" EventName="click" />
    </Triggers>
</asp:UpdatePanel>
