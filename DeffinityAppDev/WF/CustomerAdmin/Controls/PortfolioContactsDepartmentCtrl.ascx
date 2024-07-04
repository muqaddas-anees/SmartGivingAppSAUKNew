<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_PortfolioContactsDepartmentCtrl" Codebehind="PortfolioContactsDepartmentCtrl.ascx.cs" %>
<asp:UpdatePanel ID="upnlDepartment" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong> Requesters Department</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group row">
           <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
          <asp:Label ID="lblReqDept" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                        Display="Dynamic" ErrorMessage="Please select department" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="dpt_e"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDepartment"
                        Display="Dynamic" ErrorMessage="Please enter department" SetFocusOnError="True"
                        ValidationGroup="dpt"></asp:RequiredFieldValidator>
         </div>
      <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Requesters Department</label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlDepartment" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdDepartment" runat="server" TargetControlID="ddlDepartment"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetDepartment" LoadingText="[Loading department...]" />
                    <asp:TextBox ID="txtDepartment" runat="server" SkinID="txt_80"
                        ValidationGroup="dpt"></asp:TextBox>
                                           <%-- <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="imb_Delete" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                                <div class="form-group row">
        <div class="col-md-11 text-bold">
             <strong>   <%=Resources.DeffinityRes.Alert %> </strong>
            <hr class="no-top-margin" />
            </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkClose" />
                                    </div>
</div>
                                <div class="form-group row">
             <div class="col-md-12">
                 <b>Delete from just this customer or all? </b>
</div>
</div>
                                <div class="form-group row">
             <div class="col-md-12">
                   <asp:Button ID="btndeleteAll" runat="server" Text="All customers"
                                      OnClick="btndeleteAll_Click"
                                     SkinID="btnDefault"  />
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" Text="Just this customer"
                                                 SkinID="btnDefault"
                                                 OnClick="BtnDelete_Click"/>
</div>
</div>
                           
                              </asp:Panel>--%>


                    <asp:LinkButton ID="imb_Delete" runat="server" SkinID="BtnLinkDelete"
                         ToolTip="Delete" CausesValidation="false" OnClick="imb_Delete_Click" OnClientClick="return confirm('Do you want to delete the record?');"  />
                                          <asp:HiddenField ID="hfId" runat="server" Value="0" />
					</div>
				</div>
</div>
       <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
                                          <asp:LinkButton ID="imb_Add" runat="server" SkinID="btnAdd"
                        OnClick="imb_Add_Click" />
                    <asp:LinkButton  ID="imb_Submit" runat="server" SkinID="btnSubmit" ValidationGroup="dpt" OnClick="imb_Submit_Click" />
                    <asp:LinkButton ID="imb_Edit" runat="server" SkinID="btnEdit"
                        ValidationGroup="dpt_e" OnClick="imb_Edit_Click" />
                    <asp:LinkButton ID="imb_Cancel" runat="server" SkinID="btnCancel"
                         OnClick="imb_Cancel_Click" />
                    <asp:Button ID="btnCopyToAllCustomers" runat="server" SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomers_Click" Visible="false" />
					</div>
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
