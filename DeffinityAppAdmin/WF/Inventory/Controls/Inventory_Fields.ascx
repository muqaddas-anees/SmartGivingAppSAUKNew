<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Inventory_Fields" Codebehind="Inventory_Fields.ascx.cs" %>
<style type="text/css">
    .auto-style1 {
        height: 26px;
    }
</style>
<asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanelFields">
          <ProgressTemplate>
                 <asp:Image ID="image12" runat="server" ImageUrl="~/media/ico_loading.gif" />
          </ProgressTemplate>
      </asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanelFields" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="paneladd" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Width="500px"
                                                                 Style="display: none" ClientIDMode="Static">
                <div class="form-group">
                            <div class="col-md-12">
                                <strong>  <asp:Label ID="lbladd" runat="server"></asp:Label> </strong> 
                           <div style="text-align: right;width:40px;float:right;">
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel"
                                                                  OnClick="btncancel_Click"></asp:LinkButton>
                            </div>
                                 <hr class="no-top-margin" />
                            </div>
                </div>

                <div>
                    <div>
                           <asp:Label ID="lblerror1" runat="server" EnableViewState="false"></asp:Label>
                                   <asp:ValidationSummary ID="catSummary" runat="server" ValidationGroup="popup" />
                                   <asp:RequiredFieldValidator ID="rfvtxt" runat="server" ForeColor="Red" ControlToValidate="txtbox" Display="None"
                                                           ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentername%>" ValidationGroup="popup"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                          <div class="col-md-12 form-inline">
                           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Name%></label>
                          <div class="col-sm-10 form-inline">
                               <asp:TextBox ID="txtbox" SkinID="txt_70" runat="server" ValidationGroup="popup"></asp:TextBox>
                                  <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                                                         OnClick="btnsubmit_Click" ValidationGroup="popup" />
                          </div>
                      </div>
                     </div>
                    <div>
                         <asp:Label ID="lblspace3" runat="server"></asp:Label>
                    </div>
                 </div>
              
        </asp:Panel>
        <asp:Panel ID="panelEdit" runat="server" BackColor="White" Width="500px"
                             BorderStyle="Double" BorderColor="LightSteelBlue" style="display:none" ClientIDMode="Static">
                 <div class="form-group">
                      <div class="col-md-12">
                            <strong><asp:Label ID="lbledit" runat="server"></asp:Label></strong>
                            <div style="text-align: right;width:40px;float:right;">
                                 <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"
                                                                 SkinID="BtnLinkCancel" OnClick="BtnEditCancel_Click"></asp:LinkButton>
                            </div>
                            <hr class="no-top-margin" />
                       </div>
                 </div>
                <div>
                    <asp:Label ID="lblError2" EnableViewState="false" runat="server"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="edit" />
                                        <asp:RequiredFieldValidator ID="rfvtxtedit" runat="server" ForeColor="Red" Display="None"
                                                                 ControlToValidate="txtedit" ValidationGroup="edit" 
                                            ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentername%>"></asp:RequiredFieldValidator>
                </div>
                  <div class="form-group">
                            <div class="col-md-12 form-inline">
                                 <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Name%></label>
                                   <div class="col-sm-10 form-inline">
                                       <asp:TextBox ID="txtedit" SkinID="txt_70" runat="server" ValidationGroup="edit"></asp:TextBox>
                                       <asp:Button ID="BtnUpdate" runat="server" Text="Update"
                                                                                ValidationGroup="edit" OnClick="BtnUpdate_Click" />
                                   </div>
                            </div>
                  </div>        
</asp:Panel>
            
<div class="row">
          <div class="col-md-12">
 <strong> Location Configurator </strong> 
<hr class="no-top-margin" />
	</div>
</div>
            
<div class="form-group">
      <div class="col-md-12">
           <div>
                          <asp:Label ID="lblmsg" ForeColor="red" EnableViewState="false" runat="server" ></asp:Label>
                     </div>
                    <div class="form-group">
                         <div class="col-md-6">
                             <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Area%></label>
                             <div class="col-sm-10 form-inline">
                                   <asp:DropDownList ID="ddlarea" runat="server" AutoPostBack="true" SkinID="ddl_50"
                                                                              OnSelectedIndexChanged="ddlarea_SelectedIndexChanged"></asp:DropDownList>

                                       <asp:Button ID="imb_Addarea" runat="server" Text="Add" OnClick="imb_Addarea_Click" />
                                       <asp:Button ID="imb_EditArea" runat="server" Text="Edit"  OnClick="imb_EditArea_Click" />
                                       <asp:LinkButton ID="imb_Deletearea" runat="server" SkinID="BtnLinkDelete"
                                               ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');" OnClick="imb_Deletearea_Click"/>
                        <asp:CheckBox ID="AreaCheckBox" runat="server" AutoPostBack="true" Text="Visible" OnCheckedChanged="AreaCheckBox_CheckedChanged" />
                        <asp:Label ID="l1" runat="server"></asp:Label>
                        <asp:Label ID="l2" runat="server"></asp:Label>
                        <ajaxToolkit:ModalPopupExtender ID="popupAdd" runat="server" BackgroundCssClass="modalBackground"
                             TargetControlID="l2" PopupControlID="paneladd"></ajaxToolkit:ModalPopupExtender>
                        <ajaxToolkit:ModalPopupExtender ID="popupEdit" runat="server" BackgroundCssClass="modalBackground"
                             TargetControlID="l1" PopupControlID="panelEdit"></ajaxToolkit:ModalPopupExtender>

                             </div>
                         </div>
                    </div>
                     <div class="form-group">
                         <div class="col-md-6">
                             <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Location%></label>
                             <div class="col-sm-10 form-inline">
                                   <asp:DropDownList ID="ddllocation" runat="server" SkinID="ddl_50"
                                                       AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged"></asp:DropDownList>
                                   <asp:Button ID="imb_AddLocation" runat="server" Text="Add" CausesValidation="false" OnClick="imb_AddLocation_Click" />
                                   <asp:Button ID="imb_EditLocation" runat="server" Text="Edit" OnClick="imb_EditLocation_Click"/>
              
                    <asp:LinkButton ID="imb_DeleteLocation" runat="server" SkinID="BtnLinkDelete"
                                          ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');" OnClick="imb_DeleteLocation_Click"></asp:LinkButton>
                        <asp:CheckBox ID="LocationCheckBox" runat="server" AutoPostBack="true" Text="Visible" OnCheckedChanged="LocationCheckBox_CheckedChanged" />
                     <asp:HiddenField ID="hdedit" runat="server" />
                            <asp:HiddenField ID="hd" runat="server"/>
                             </div>
                         </div>
                    </div>
                     <div class="form-group">
                         <div class="col-md-6">
                             <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Shelf%></label>
                             <div class="col-sm-10 form-inline">
                                   <asp:DropDownList ID="ddlshelf" runat="server" SkinID="ddl_50"
                                                               AutoPostBack="true" OnSelectedIndexChanged="ddlshelf_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:Button ID="imb_AddShelf" runat="server" Text="Add"  CausesValidation="false" OnClick="imb_AddShelf_Click" />
                                 <asp:Button ID="imb_EditShelf" runat="server" Text="Edit"  OnClick="imb_EditShelf_Click" />
                                 <asp:LinkButton ID="imb_DeleteShelf" runat="server" SkinID="BtnLinkDelete"
                                                             ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                              OnClick="imb_DeleteShelf_Click"></asp:LinkButton>
                        <asp:CheckBox ID="ShelfCheckBox" runat="server" AutoPostBack="true" Text="Visible" OnCheckedChanged="ShelfCheckBox_CheckedChanged" />
                             </div>
                         </div>
                    </div>
                     <div class="form-group">
                         <div class="col-md-6">
                             <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Bin%></label>
                             <div class="col-sm-10 form-inline">
                                   <asp:DropDownList ID="ddlbin" runat="server" SkinID="ddl_50"></asp:DropDownList>
                                    <asp:Button ID="imb_AddBin" runat="server" Text="Add" CausesValidation="false" OnClick="imb_AddBin_Click"/>
                                    <asp:Button ID="imb_EditBin" runat="server" Text="Edit" OnClick="imb_EditBin_Click"/>
                                    <asp:LinkButton ID="imb_DeleteBin" runat="server" SkinID="BtnLinkDelete"
                                                 ToolTip="<%$ Resources:DeffinityRes,Delete%>" OnClientClick="return confirm('Do you want to delete the record?');"
                                                 OnClick="imb_DeleteBin_Click"/>
                                    <asp:CheckBox ID="BinChechBox" runat="server" AutoPostBack="true" Text="Visible" OnCheckedChanged="BinChechBox_CheckedChanged" />
                             </div>
                         </div>
                    </div>
	</div>

</div>
           
            <div>
            <div id="pnlCustomer" runat="server" visible="false">
                <div> <%= Resources.DeffinityRes.Customer%>  </div>
                <div>
                    <asp:DropDownList ID="ddlcustomer" runat="server" AutoPostBack="true" Width="150px"
                         OnSelectedIndexChanged="ddlcustomer_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div>
                    
                        <asp:Button ID="imb_Addcustomer" runat="server" SkinID="btnAdd" Visible="false" 
                         CausesValidation="false" OnClick="imb_Addcustomer_Click"/>
                     <asp:Button ID="imb_Editcustomer" runat="server" SkinID="btnEdit" Visible="false"
                         OnClick="imb_Editcustomer_Click"/>
                    <asp:LinkButton ID="imb_Deletecustomer" runat="server" Visible="false"
                        SkinID="BtnLinkDelete" ImageAlign="Top" 
                        ToolTip="<%$ Resources:DeffinityRes,Delete%>" OnClientClick="return confirm('Do you want to delete the record?');" OnClick="imb_Deletecustomer_Click"/>
                </div>
                <div>
                    <%--<asp:RequiredFieldValidator ID="rfvddlcustmoer" runat="server" ErrorMessage="Select one customer."
                         ControlToValidate="ddlcustomer" InitialValue="0">
                    </asp:RequiredFieldValidator>--%>
                </div>
            </div>
        </div>
             </ContentTemplate>
      <Triggers>
          <asp:PostBackTrigger ControlID="ddlcustomer"/>
      </Triggers>
</asp:UpdatePanel>