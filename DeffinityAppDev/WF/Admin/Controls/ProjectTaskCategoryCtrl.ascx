<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectTaskCategoryCtrl" Codebehind="ProjectTaskCategoryCtrl.ascx.cs" %>
<asp:UpdatePanel ID="upnlTaskCategory" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.TaskCategory%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                                        <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server"></asp:Label>
	</div>
</div>
                                      
                                      <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Category%> </label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_90">
                                                    </asp:DropDownList>
                                                  
                                                    <asp:TextBox ID="txtCategory" runat="server" CssClass="txt_field" SkinID="txt_90" ValidationGroup="cat"></asp:TextBox>
               <asp:LinkButton ID="imb_Delete" runat="server" SkinID="BtnLinkDelete"
                                                        ToolTip="<%$ Resources:DeffinityRes,Delete%>" OnClientClick="return confirm('Do you want to delete the record?');"
                                                       ValidationGroup="dte" onclick="imb_Delete_Click" />
            </div>
	</div>
</div>
                                        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> <asp:HiddenField ID="hfId" runat="server" Value="0" /></label>
           <div class="col-sm-9">
                <asp:Button ID="imb_Add" runat="server" SkinID="btnAdd" onclick="imb_Add_Click"/>
                                                    <asp:Button ID="imb_Submit" runat="server" SkinID="btnSubmit"  ValidationGroup="cat" 
                                                        onclick="imb_Submit_Click" />
                                                    <asp:Button ID="imb_Edit" runat="server" SkinID="btnEdit" ValidationGroup="cat_e" OnClick="imb_Edit_Click" />
                                                    <asp:Button ID="imb_Cancel" runat="server" SkinID="btnCancel" onclick="imb_Cancel_Click"  />
            </div>
	</div>
</div>
                                        <div class="form-group">
          <div class="col-md-12">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                                                        Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcategory%>" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="cat_e"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCategory"
                                                        Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentercategory%>" SetFocusOnError="True" ValidationGroup="cat"></asp:RequiredFieldValidator>
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