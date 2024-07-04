<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="RFIVendors1" Codebehind="RFIVendors.aspx.cs" %>

<%@ Register src="controls/TenderMenuTab.ascx" tagname="TenderMenuTab" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    Supplier Management
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Supplier Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
   <%-- <uc1:TenderMenuTab ID="TenderMenuTab1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" Runat="Server">
   <asp:HyperLink id="linkBack" runat="Server" NavigateUrl="~/WF/CustomerAdmin/InventoryItemslist.aspx">
<i class="fa fa-arrow-left"></i>Return to Inventory</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="row">
          <div class="col-md-12 panel-right">
              <div style="float:right;">
<asp:Button ID="lbtnPlan" runat="server" OnClick="lbtnPlan_Click" SkinID="btnDefault" Text="Add 
                            New Supplier"></asp:Button>
                  </div>
	</div>
</div>
    <asp:Panel ID="Panel_fileupdload" runat="server" Width="100%">
        <div>
        <table style="width:100%;float:right;">
        <tr>
        <td><ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                TargetControlID="PanelCsv" ExpandControlID="PnlTitle" CollapseControlID="PnlTitle"
                TextLabelID="Lbl1" CollapsedText="Upload Excel File " ExpandedText="Upload Excel File "
                ImageControlID="UploadImg" Collapsed="true" SuppressPostBack="true" >
            </ajaxToolkit:CollapsiblePanelExtender>
            <asp:Panel Width="100%" ID="PnlTitle1" runat="server" HorizontalAlign="Right" Visible="false">
<div id="PnlTitle" runat="server" style="cursor:pointer"> <asp:Label ID="Lbl1" runat="server" Text="Upload Excel File " Font-Bold="true" ForeColor="Black" style="cursor:pointer;"></asp:Label></div>
</asp:Panel></td>
        <td> </td>
        </tr>
        </table>
        </div>
        <div class="clr"></div>


</asp:Panel>
    <asp:Panel ID="PanelCsv" runat="server" Width="100%" style="overflow:hidden;">
    <div class="tab_header_Bold"   >
		Excel file upload
		</div>
		 <div style="width:50%;margin-left:5px;float:left">
		 <iframe id="iframeMpp" height="100px" width="100%" runat="server" scrolling="no" frameborder="0"  ></iframe> 
		 </div>    
    </asp:Panel>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        GridLines="None" EmptyDataText="No Records Exists" DataKeyNames="VendorID" OnRowDeleting="GridView1_RowDeleting" OnRowDeleted="GridView1_RowDeleted" EnableViewState="False" >
                        <Columns>
                            <asp:BoundField DataField="VendorID" HeaderText="VendorID" Visible="False" />
                            <asp:TemplateField HeaderText="Supplier" ItemStyle-Width="200px"  ControlStyle-Width="200px" >

<ControlStyle Width="200px"></ControlStyle>

                                <ItemStyle />
                                <ItemTemplate>
                                    <a href="./RFIVendorOverview.aspx?VendorID=<%# DataBinder.Eval(Container.DataItem, "VendorID")%>">
                                        <%# DataBinder.Eval(Container.DataItem, "ContractorName")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="">
                             <ItemTemplate>
                                <a href="./PartnerSalesTargets.aspx?VendorID=<%# DataBinder.Eval(Container.DataItem, "VendorID")%>">Sales Target</a>
                             </ItemTemplate>
                             </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Address">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                 </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specialist Skills/Key Information">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Details") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="" Visible="false">
                             <ItemTemplate>
                                <a href="./ContractList.aspx?vendor=2&vendorid=<%# DataBinder.Eval(Container.DataItem, "VendorID")%>">Contract</a>
                             </ItemTemplate>
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="" >
                             <ItemTemplate>
                                <a href="./RFIVendorServiceCatalog.aspx?VendorID=<%# DataBinder.Eval(Container.DataItem, "VendorID")%>" class="btn btn-secondary">Catalogue</a>
                             </ItemTemplate>
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="" Visible="false">
                             <ItemTemplate>
                                <a href="./VendorCustomEmail.aspx?VendorID=<%# DataBinder.Eval(Container.DataItem, "VendorID")%>&Type=Vendor">Custom alerts</a>
                             </ItemTemplate>
                             </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="5%">                    
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("VendorID")%>'
                                CommandName="Delete" OnClientClick='return confirm("Do you really want to delete this vendor?");'
                                SkinID="BtnLinkDelete" AlternateText="Delete Vendor"  />
                        </ItemTemplate>

<HeaderStyle CssClass="header_bg_r"></HeaderStyle>

                       <ItemStyle Width="30px" />
                    </asp:TemplateField> 
                                                                           

                        </Columns>
                    </asp:GridView>
                    <%--<asp:ObjectDataSource TypeName="Deffinity.BLL.RFI_Vendor_SVC" SelectMethod="Fill"
                        runat="server" ID="obj_rfi" >
                        </asp:ObjectDataSource>      --%>
    
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        $(window).load(function () {
                     $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
                 });
    </script>     
</asp:Content>

