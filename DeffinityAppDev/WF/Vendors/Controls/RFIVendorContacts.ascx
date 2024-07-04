<%@ Control Language="C#" AutoEventWireup="true" Inherits="RFIVendorContacts" Codebehind="RFIVendorContacts.ascx.cs" %>
    <div class="form-group row">
          <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        <asp:ValidationSummary ID="AddNew" runat="server" ValidationGroup="Group11" />
                         <asp:ValidationSummary ID="Valsumgrid" runat="server" ValidationGroup="Group10" ForeColor="Red"
                                             DisplayMode="List" Enabled="true" ShowSummary="true" />

        <asp:ObjectDataSource ID="ObjDS_grid" runat="server" TypeName="Deffinity.BLL.ContractorContacts_Base_SVC"
                SelectMethod="Fill" UpdateMethod="Update" DeleteMethod="Delete">
                <%--<InsertParameters><asp:ControlParameter ControlID="HID1" DefaultValue="0" Name="contractorid" Type="Int32" /></InsertParameters>--%>
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="VendorID" Name="vendorid" Type="Int32" Direction="Input" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="contacts" Type="Object" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="form-group row">
        <asp:GridView ID="GridContactsInfo" runat="server" Width="96%" DataKeyNames="ID"
                DataSourceID="ObjDS_grid" AutoGenerateColumns="False" OnRowUpdating="GridContactsInfo_RowUpdating"
                OnRowCancelingEdit="GridContactsInfo_RowCancelingEdit" OnRowDeleting="GridContactsInfo_RowDeleting"
                OnRowEditing="GridContactsInfo_RowEditing" OnRowUpdated="GridContactsInfo_RowUpdated"
                OnRowCommand="GridContactsInfo_RowCommand" OnRowDataBound="GridContactsInfo_RowDataBound">
                <Columns>
                    <asp:TemplateField FooterText="Add New" ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblid1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtname" runat="server" Text='<%# Bind("Name") %>' Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname"
                                Display="None" ErrorMessage="Please enter Name" Text="*" ValidationGroup="Group10">
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20%" HorizontalAlign="left" />
                        <FooterTemplate>
                            <asp:TextBox ID="txtname1" runat="server" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rwqname1" runat="server" ControlToValidate="txtname1"
                                Display="None" ErrorMessage="Please enter Name" Text="*" ValidationGroup="Group11">
                            </asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Position">
                        <EditItemTemplate>
                            <asp:TextBox ID="txttitle" runat="server" Text='<%# Eval("JobTitle") %>' Width="90"
                                EnableViewState="false"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("JobTitle") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="15%" HorizontalAlign="left" />
                        <FooterTemplate>
                            <asp:TextBox ID="txttitle1" runat="server" Width="90%"></asp:TextBox></FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Address">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtemail" runat="server" Text='<%# Bind("Email") %>' Width="160"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid email Eg:Smith@Deffinity.com"
                                Text="Enter vaild email Eg:Smith@Enc.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtemail" Display="None" ValidationGroup="Group10"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtemail"
                                Display="None" Text="*" ErrorMessage="Please enter Email address" ValidationGroup="Group10"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtemail1" runat="server" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regemail1" runat="server" ErrorMessage="Enter valid email Eg:Smith@Deffinity.com"
                                Text="Enter vaild email Eg:Smith@Enc.com" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"     
                                ControlToValidate="txtemail1" Display="None" ValidationGroup="Group11"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="reqemail1" runat="server" ControlToValidate="txtemail1"
                                Display="None" Text="*" ErrorMessage="Please enter Email address" ValidationGroup="Group11"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20%" HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact Number">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtcontactnumber" runat="server" Text='<%# Bind("Telephone") %>'
                                SkinID="Price" Width="90%" MaxLength="15"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Enter the Mob.No."
                                ControlToValidate="txtcontactnumber" ValidationGroup="Group11" ValidationExpression="[0-9+-_\s]*">*</asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblcontactnumber" runat="server" Text='<%# Eval("Telephone") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="15%" HorizontalAlign="right" />
                        <FooterTemplate>
                            <asp:TextBox ID="txtcontactnumber1" runat="server" SkinID="Price" Width="90%" MaxLength="15"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionnumber" runat="server" ErrorMessage="Enter the Mob.No."
                                ControlToValidate="txtcontactnumber1" ValidationGroup="Group11" ValidationExpression="[0-9+-_\s]*">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile ">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtmobilenumber" runat="server" Text='<%# Bind("Mobile") %>' SkinID="Price"
                                Width="90%" MaxLength="15"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblmobilenumber" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="15%" HorizontalAlign="right" />
                        <FooterTemplate>
                            <asp:TextBox ID="txtmobilenumber1" runat="server" SkinID="Price" Width="90%" MaxLength="15"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete"
                                CommandArgument='<%# Eval("ID")%>' SkinID="BtnLinkDelete" ToolTip="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton runat="server" ID="btnaddsite" SkinID="BtnLinkUpload" ValidationGroup="Group11"
                                CommandName="AddContact" ToolTip="Add New Contact"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="imgbtncancel" SkinID="BtnLinkCancel"
                                                                CommandName="Cancel" ToolTip="Cancel"></asp:LinkButton>
                        </FooterTemplate>

<HeaderStyle CssClass="header_bg_r"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No Contacts available.
                </EmptyDataTemplate>
            </asp:GridView>
    </div>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script>



