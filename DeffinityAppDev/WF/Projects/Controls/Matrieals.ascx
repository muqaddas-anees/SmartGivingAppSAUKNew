<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Matrieals" Codebehind="Matrieals.ascx.cs" %>
 <div style="overflow:auto">
    <asp:GridView ID="grdMaterials" runat="server" AutoGenerateColumns="False" Width="100%"
                        EmptyDataText="No Materials Data Available" Visible="true" 
         onrowdatabound="grdMaterials_RowDataBound" 
         onrowcancelingedit="grdMaterials_RowCancelingEdit" 
         onrowcommand="grdMaterials_RowCommand" onrowdeleting="grdMaterials_RowDeleting" 
         onrowediting="grdMaterials_RowEditing" onrowupdating="grdMaterials_RowUpdating" >
                        <Columns>
                          <asp:TemplateField Visible="false">
                  
                    <ItemStyle Width="70px" />
                     
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                            CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkUpdate" ToolTip="Update" ValidationGroup="Group34">
                        </asp:LinkButton>
                        <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="ImgCancel" ToolTip="Cancel"></asp:ImageButton>
                    </EditItemTemplate>
                     <%--<FooterTemplate>
                     <asp:ImageButton ID="btnaddmisc" runat="server" CommandName="AddMisc"
                           ImageUrl="~/media/ico_update.png" ToolTip="Add Miscllenious" ValidationGroup="grpmisc">
                        </asp:ImageButton>
                        <asp:ImageButton ID="btncancelmisc" runat="server" CausesValidation="false" CommandName="CancelMisc"
                            ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton>
                    </FooterTemplate>
                    <FooterStyle Width="70px" />--%>
                </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Description" >
                            
                                <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="header_bg_l"/>
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDescriptionM" runat="server" Text="<%# Bind('Description')%>"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtItemDescription" runat="server" Text="<%# Bind('Description')%>"></asp:TextBox>
                                </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Part Number">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPartNumber" runat="server" Text="<%# Bind('PartNumber')%>"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPartNumber" Width="100px" runat="server" Text="<%# Bind('PartNumber')%>"></asp:TextBox>
                                </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Vendor" ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendor" runat="server" Text="<%# Bind('Company')%>"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlVendor" Width="120px">
                                    </asp:DropDownList></EditItemTemplate>
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderText="Qty Recieved" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate >                                
                                    <asp:Label ID="lblQty" runat="server"  Width="50px" Text="<%#Bind('QtyReceived')%>"></asp:Label></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" Width="75px" Text="<%#Bind('QtyReceived')%>" runat="server"></asp:TextBox></EditItemTemplate>                              
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" Width="50px" runat="server"  Text='<%#Bind("price","{0:f2}")%>'></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblTotalPrice" Width="50px" runat="server" Font-Bold="true"  Text='Total'></asp:Label></ItemTemplate>
                                </FooterTemplate>   
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Total" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="header_bg_r">
                                <ItemTemplate>
                                    <asp:Label ID="lblCostPrice"  runat="server" Width="50px" Text='<%#Bind("Toatls","{0:f2}")%>'></asp:Label></ItemTemplate>
                                   <FooterTemplate>
                                   <asp:Label ID="lblTotalPrice1" Width="50px" runat="server" Font-Bold="true"  Text='Total'></asp:Label>
                                   </FooterTemplate>
                                   <FooterStyle Width="50px" />
                                </asp:TemplateField>
                                  
                
                                </Columns>
                                </asp:GridView>
    </div>

 