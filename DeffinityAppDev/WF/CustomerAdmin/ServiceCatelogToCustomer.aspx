<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ServiceCatelogToCustomer_1" EnableEventValidation="false" Codebehind="ServiceCatelogToCustomer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="pnlBOM" runat="server" Width="850px" Height="440px" ScrollBars="None">

                           
                        <div class="clr">
                        </div>
                        <div>
                        <table>
                        
                        <tr>
                        <%--<td> <%= Resources.DeffinityRes.Vendors%></td> <td>--%>
                        <%--<asp:UpdatePanel runat="server" ID="upd">
                        <ContentTemplate>--%>
                         <%-- <asp:DropDownList ID="ddlVendors" runat="server"
                                Width="150px" >
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="casCadCategory" runat="server" TargetControlID="ddlCategory"
                                Category="Task" PromptText="Please select..." ServicePath="~/ServiceMgr.asmx"
                                ServiceMethod="GetVendorCategory" ParentControlID="ddlVendors" />--%>
                                   <%-- </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>--%> <td>   <%= Resources.DeffinityRes.Category%></td><td><asp:DropDownList ID="ddlCategory" runat="server" Width="150px" ></asp:DropDownList>  
                                    <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetCategoryByAdmin" LoadingText="[Loading Category...]" BehaviorID="ccdCategory1" ClientIDMode="Static" />  </td>
                                    <td> <%= Resources.DeffinityRes.SubCategory%></td>
                                    <td><asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px"></asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetSubCategoryByAdmin" LoadingText="[Loading Sub Category...]" BehaviorID="ccdSubCategory1" ClientIDMode="Static" ParentControlID="ddlCategory" /> 
                                   </td> 
                                <td> <%= Resources.DeffinityRes.Type%></td> <td> <asp:DropDownList ID="ddlSelect" runat="server" Width="100px" >
                                            <asp:ListItem Value="0">Please select...</asp:ListItem>
                                            <asp:ListItem Value="1">Labour</asp:ListItem>
                                            <asp:ListItem Value="2">Products</asp:ListItem>
                                            <asp:ListItem Value="3">Service</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                  </tr><tr>  <td> <%= Resources.DeffinityRes.Item%></td><td><asp:TextBox ID="txtItemDescription" runat="server" Width="200px" MaxLength="100"></asp:TextBox> </td> <td><asp:ImageButton ID="imgVendorSearch" runat="server" 
                                    SkinID="ImgSearch" onclick="imgVendorSearch_Click" CausesValidation="false"/></td>
                        </tr>
                        </table>
                       
                        </div>
                          <div class="clr">
                        </div>
                         <asp:UpdateProgress ID="uProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
  <img src="media/ico_loading.gif" alt="Loading" style="border:0px" />  
</ProgressTemplate>
</asp:UpdateProgress>
                        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                         <table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr style="background-color:whitesmoke;"><td style="width:100%"><asp:ImageButton ID="imgUpdate" runat="server" SkinID="ImgApply" OnClick="imgUpdate_Click" /></td></tr>
  </table>
   <asp:Label ID="lblErr" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
   <asp:Panel ID="panel_grid" runat="server" Width="100%" Height="355px" ScrollBars="Auto">
                            <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False"
                                 OnRowCommand="GridView2_RowCommand"
                                DataKeyNames="ID" EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable%>" width="830px">
                                <Columns>
                                    <asp:TemplateField Visible='false'>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                             <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                            <asp:Label ID="lblSubcategoryID" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle  CssClass="header_bg_l" />
                                        <ItemStyle  />
                                        <ItemTemplate>
                                           
                                            <asp:CheckBox ID="chkbox" runat="server"  />
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>" ItemStyle-HorizontalAlign="Left">
                                       
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' ></asp:Label>
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Description%>" DataField="Description"  Visible="false"/>
                                    
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Type%>"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil12" runat="server" Text='<%#GetItemsType(DataBinder.Eval(Container.DataItem,"Type").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,BuyingPrice%>" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil"  runat="server" Text="<%#Bind('BP','{0:F2}')%>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>"  HeaderStyle-CssClass="header_bg_r"
                                        ItemStyle-HorizontalAlign="Right">
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblSP" runat="server"  Text="<%#Bind('SP','{0:F2}')%>"></asp:Label>
                                            <asp:TextBox ID="txtQtyReq" Width="50px" Visible="false" runat="server" Text="<%#Bind('SP','{0:F2}')%>"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                             </asp:Panel>
                            </ContentTemplate>
                         <Triggers>
                       
                        <asp:PostBackTrigger ControlID="imgVendorSearch" />
                        </Triggers>
                        
                    </asp:UpdatePanel>
                       
                       
                        <div style="text-align: left">
                            
                         
                            <asp:Button runat="server" ID="imgItemEdit" Style="display: none" />
                        </div>
                    </asp:Panel>
</asp:Content>

