<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignedtoDepartmentCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.AssignedtoDepartmentCtrl" %>

<asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>

<div class="row">
                                <div class="col-md-12">

                                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
        <asp:Label  SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
                                      <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                                      <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  Assigned to Department </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

                                      <div class="form-group row">
                                          <asp:Label ID="LblADmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                          <asp:Label ID="LblADerror" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAssignedtoDept"
                                ErrorMessage="Please select Department" InitialValue="0" ValidationGroup="Dept2"></asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAssignedtoDept"
                                ErrorMessage="Please enter Department" ValidationGroup="Dept1"> </asp:RequiredFieldValidator>
                                      </div>

                                      <div class="form-group row">
                                           <%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" 
                                  TargetControlID="btnDeleteDept"
                             BackgroundCssClass="modalBackground" CancelControlID="ImageButton1"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                                  <table>
                                      <tr>
                                           <td class="auto-style2">
                                           <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                Alert</div>
                                      </td>
                                          <td class="auto-style1">
                                           <asp:ImageButton ID="ImageButton1" runat="server" SkinID="ImgSymCancel" style="border-width:0px;"/>
                                      </td>
                                      </tr>
                                      <tr>
                                            <td class="auto-style2"><b>Delete from just this customer or all? </b></td>
                                      </tr>
                                      <tr>
                                             <td class="auto-style2">
                                                      <asp:Button ID="Btnall" runat="server" Text="All customers"
                                                           OnClick="Btnall_Click" SkinID="btnDefault"/>
                                    <asp:Button ID="btnsingle" runat="server" Text="Just this customer" OnClick="btnsingle_Click" SkinID="btnDefault" />
                                             </td>
                                      </tr>
                                  </table>
                              </asp:Panel>--%>
                                          </div>
                                      <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Assigned to Department</label>
                                      <div class="col-sm-4 form-inline"> <asp:TextBox ID="txtAssignedtoDept" runat="server" Visible="False" ValidationGroup="Dept1"
                                ></asp:TextBox>
                            <asp:DropDownList ID="ddlAssignedtoDept" runat="server" ValidationGroup="Dept2">
                            </asp:DropDownList>
                                          
					</div>
                                      <div class="col-sm-5">
                                           <asp:LinkButton runat="server" ID="btnDeleteDept" SkinID="BtnLinkDelete" OnClientClick="Do you want to delete this record?" OnClick="btnDeleteDept_Click" />
                            <asp:HiddenField ID="hidDept" runat="server" Value="0" />
                                           <asp:LinkButton ID="btnAddDept" runat="server" SkinID="btnAdd" 
                                onclick="btnAddDept_Click" />
                            <asp:LinkButton ID="btnEditDept" runat="server" ValidationGroup="Dept2" 
                                SkinID="btnEdit" onclick="btnEditDept_Click" />
                            <asp:LinkButton ID="btnSubmitDept" runat="server" Visible="false" ValidationGroup="Dept1"
                                SkinID="btnSubmit" onclick="btnSubmitDept_Click" />
                            <asp:LinkButton ID="btnCancelDept" runat="server" SkinID="btnCancel" Visible="false"
                                onclick="btnCancelDept_Click" />
                                      </div>
				</div>
</div>
                                      <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                          
                            <asp:Button ID="btnDeptCopyToAllCustomer" runat="server" SkinID="btnOrange" Text="Copy to All Customers" OnClick="btnDeptCopyToAllCustomer_Click" Visible="false"  />
					</div>
				</div>
</div>

           <div class="form-group row">
                 <div class="col-md-12">
                      <label class="col-sm-3 control-label">Users</label>
                      <div class="col-sm-9 form-inline">
                            <div style="height:100px;overflow-y:auto;width:240px;border:3px solid #d3d3d3">
                                <asp:CheckBoxList ID="checkListUsers" runat="server"></asp:CheckBoxList>
                            </div>
                      </div>
                 </div>
           </div>
           <div class="form-group row">
                 <div class="col-md-12">
                       <label class="col-sm-3 control-label"></label>
                        <div class="col-sm-9 form-inline">
                            <asp:LinkButton ID="BtnSubmit" runat="server" SkinID="btnApply" OnClick="BtnSubmit_Click" ValidationGroup="Dept2" />
                        </div>
                 </div>
           </div>
            <div class="form-group row">
                 <div class="col-md-12">
                      <asp:GridView ID="GvMailManager" runat="server" Width="80%" EmptyDataText="No records exist"
                                                    OnRowCommand="GvMailManager_RowCommand" AllowPaging="true" PageSize="3"
                           OnRowDeleting="GvMailManager_RowDeleting" OnRowEditing="GvMailManager_RowEditing" OnPageIndexChanging="GvMailManager_PageIndexChanging">
                          <Columns>
                              <asp:TemplateField HeaderText="Department">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl" runat="server" Text='<%#Bind("Department") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Users">
                                  <ItemTemplate>
                                      <asp:Label ID="lblUser" runat="server" Text='<%#Bind("ContractorName") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                           <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"
                                               CommandName="Delete1" OnClientClick="Do you want to delete this record?"
                                               CommandArgument='<%#Bind("ID") %>' SkinID="BtnLinkDelete"></asp:LinkButton>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                          </Columns>
                      </asp:GridView>
                 </div>
            </div>


        </ContentTemplate>
                                          <Triggers>
                                             <%-- <asp:AsyncPostBackTrigger ControlID="btnDeptCopyToAllCustomer" EventName="Click" />--%>
                                              <asp:AsyncPostBackTrigger ControlID="btnSubmitDept"  EventName="Click"  />
                                              
                                          </Triggers>
                                          </asp:UpdatePanel>
                                    </div>
    </div>