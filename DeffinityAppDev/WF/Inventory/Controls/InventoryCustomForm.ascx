<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_InventoryCustomForm" Codebehind="InventoryCustomForm.ascx.cs" %>


<div class="row">
          <div class="col-md-12">
 <strong> Custom Fields for Customer: <asp:Literal ID="lblCustomer" runat="server"></asp:Literal></strong> 
<hr class="no-top-margin" />
	</div>
</div>

<div class="row">
          <div class="col-md-12">
               <asp:GridView ID="gvForm" runat="server" Width="100%" OnRowCommand="gvForm_RowCommand"
                        EmptyDataText="No fields found" OnRowDeleting="gvForm_RowDeleting" OnRowEditing="gvForm_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Linkedit" runat="server" CausesValidation="false" CommandName="Edit"
                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Label%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblLabel" runat="server" Text='<% #Bind("LabelName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Type%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfField" runat="server" Text='<% #Bind("TypeOfField") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Values%>">
                            <ItemStyle Width="400px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblValues" runat="server" Text='<% #Eval("ListValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DefaultText%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblDefaultText" runat="server" Text='<% #Eval("DefaultText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,MinimumValue%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumValue" runat="server" Text='<% #Eval("MinimumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,MaximumValue%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaximumValue" runat="server" Text='<% #Eval("MaximumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Mandatory%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblMandatory" runat="server" Text='<% #Eval("Mandatory").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lblPosition" runat="server" Text='<% #Bind("FieldPosition") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle Width="45px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.AddUpdateField%> </strong> 
            <hr class="no-top-margin" />
        </div>
    </div>      
    <div>
          <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
    </div>
        <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectTypeofField%></label>
                      <div class="col-sm-9">
                          <asp:DropDownList ID="ddlTypeOfField" runat="server" SkinID="ddl_70" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTypeOfField_SelectedIndexChanged">
                                <asp:ListItem Value="Text Box" Text="Text Box"> </asp:ListItem>
                                <asp:ListItem Value="Text Area" Text="Text Area"> </asp:ListItem>
                                <%--  <asp:ListItem Value="Instruction" Text="Instruction"> </asp:ListItem>--%>
                                <asp:ListItem Value="Dropdown List" Text="Dropdown List"> </asp:ListItem>
                                <asp:ListItem Value="Radio Button" Text="Radio Button"> </asp:ListItem>
                                <asp:ListItem Value="Checkbox" Text="Checkbox"> </asp:ListItem>
                                <asp:ListItem Value="Date" Text="Date"> </asp:ListItem>
                                <asp:ListItem Value="Number Field" Text="Number Field"> </asp:ListItem>
                                <asp:ListItem Value="Url" Text="Url"> </asp:ListItem>
                            </asp:DropDownList>
                      </div>
                 </div>
        </div>
          <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label">  <asp:Label ID="lblLableName" runat="server" Text="Label"></asp:Label></label>
                      <div class="col-sm-9">
                            <asp:TextBox ID="txtLabelName" runat="server" SkinID="txt_70"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLabel" runat="server" ControlToValidate="txtLabelName"
                                ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterlabel%>" ValidationGroup="form" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                      </div>
                 </div>
        </div>
          <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label"><asp:Label ID="lblDefaultText" runat="server" Text="Default Value/Text"></asp:Label></label>
                      <div class="col-sm-9">
                            <asp:TextBox ID="txtDefaultText" runat="server" SkinID="txt_70" TextMode="MultiLine"></asp:TextBox>
                      </div>
                 </div>
        </div>
          <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label">  <asp:Label ID="lblMandatoryField" runat="server" Text="Mandatory field"></asp:Label></label>
                      <div class="col-sm-9">
                          <asp:CheckBox ID="chkMandatoryField" runat="server" />
                      </div>
                 </div>
        </div>
          <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label"><asp:Label ID="lblListValues" runat="server" Text="List Values"></asp:Label></label>
                      <div class="col-sm-9">
                           <asp:TextBox ID="txtListValues" runat="server" SkinID="txt_70"></asp:TextBox>
                      </div>
                 </div>
        </div>
          <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label"><asp:Label ID="lblMinimumValue" runat="server" Text="Minimum Value"></asp:Label></label>
                      <div class="col-sm-9">
                           <asp:TextBox ID="txtMinimumValue" runat="server" SkinID="txt_40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMin" runat="server" ControlToValidate="txtMinimumValue"
                                ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterminimumvalue%>" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMin" runat="server" ControlToValidate="txtMinimumValue"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidmimimumvalue%>" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
                      </div>
                 </div>
        </div>
        <div class="form-group">
                 <div class="col-md-6">
                     <label class="col-sm-3 control-label"><asp:Label ID="lblMaximumValue" runat="server" Text="Maximum Value"></asp:Label></label>
                      <div class="col-sm-9">
                            <asp:TextBox ID="txtMaximumValue" runat="server" SkinID="txt_40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMax" runat="server" ControlToValidate="txtMaximumValue"
                                ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentermaximumvalue%>" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMax" runat="server" ControlToValidate="txtMaximumValue"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidmaximumvalue%>" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
                      </div>
                 </div>
        </div>
        <div class="form-group">
                 
                 <div class="col-md-6" style="padding-left:210px;">
                            <asp:HiddenField ID="hfId" runat="server" Value="0" />
                            <asp:Button ID="imgAdd" runat="server" Text="Add" OnClick="imgAdd_Click" ValidationGroup="form" />&nbsp;
                            <asp:Button ID="imgUpdate" runat="server" Text="Update" OnClick="imgUpdate_Click"
                                ValidationGroup="form" />&nbsp;
                            <asp:Button ID="imgCancel" runat="server" Text="Cancel" OnClick="imgCancel_Click" />
                 </div>
        </div>
        <div class="form-group">
                 <div class="col-md-12">
                     <asp:Panel ID="pnlPosition" runat="server" Visible="false">
                             <asp:CheckBoxList ID="chkListPostion" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="28">
                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="F" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="G" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="H" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="J" Value="J"></asp:ListItem>
                                    <asp:ListItem Text="K" Value="K"></asp:ListItem>
                                    <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="P" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                                    <asp:ListItem Text="R" Value="R"></asp:ListItem>
                                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="T" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                    <asp:ListItem Text="V" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                </asp:CheckBoxList>
                    </asp:Panel>
                 </div>
        </div>

	</div>
</div>


 <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="form" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr id="pnlCustomer" runat="server" visible="false">
                        <td>
                           <%= Resources.DeffinityRes.Customer%> 
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCustomer" runat="server" Width="250px" ClientIDMode="Static"
                                OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCustomer"
                                InitialValue="0" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%>" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdCustomer" runat="server" TargetControlID="ddlCustomer"
                                BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
                        </td>
                    </tr>
                </table>
               
                 