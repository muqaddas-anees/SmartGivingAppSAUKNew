<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ProjectBOMVendorCatalogue" EnableEventValidation="false" Codebehind="ProjectBOMVendorCatalogue.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <asp:Panel ID="pnlBOM" runat="server" ScrollBars="None">
 <div class="form-group">
     <div class="form-group">
      <div class="col-xs-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Vendors%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlVendors" runat="server"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="casCadCategory" runat="server" TargetControlID="ddlCategory"
                                Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                ServiceMethod="GetVendorCategory" ParentControlID="ddlVendors" />
            </div>
	  </div>
     <div class="col-xs-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" ></asp:DropDownList>
            </div>
	</div>
         </div>
     <div class="form-group">
     <div class="col-xs-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SubCategory%></label>
           <div class="col-sm-9">
                     <asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px"></asp:DropDownList>
                     <ajaxToolkit:CascadingDropDown ID="CascadingSubcategory" runat="server" TargetControlID="ddlSubCategory"
                                Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                ServiceMethod="GetVendorSubCategory" ParentControlID="ddlCategory" />
            </div>
	</div>
     <div class="col-xs-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Type%></label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="ddlSelect" runat="server">
                                            <asp:ListItem Value="0">Please select...</asp:ListItem>
                                            <asp:ListItem Value="1">Labour</asp:ListItem>
                                            <asp:ListItem Value="2">Material</asp:ListItem>
                                            <asp:ListItem Value="3">Service</asp:ListItem>
                                        </asp:DropDownList>
            </div>
	</div>
    
     </div>
       </div>
 
         <div class="form-group">
              <div class="col-xs-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Item%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtItemDescription" runat="server" MaxLength="100"></asp:TextBox> 
            </div>
	</div>
	<div class="col-xs-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-6">
                 <asp:Button ID="imgVendorSearch" runat="server"
                                              SkinID="btnSearch" onclick="imgVendorSearch_Click" CausesValidation="false"/>
            </div>
	</div>
</div>


                  
               
<asp:UpdateProgress ID="uProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
 <asp:Label ID="lblProgress" runat="server" SkinID="Loading" ></asp:Label>
</ProgressTemplate>
</asp:UpdateProgress>
                        
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <asp:Button ID="imgUpdate" runat="server" SkinID="btnApply" OnClick="imgUpdate_Click" />
                            </div>
   <asp:Label ID="lblErr" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
   <asp:Panel ID="panel_grid" runat="server" Width="100%" Height="230px" ScrollBars="Auto">
                            <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False"
                                 OnRowCommand="GridView2_RowCommand"
                                DataKeyNames="ID" EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable %>" width="100%">
                                <Columns>
                                    <asp:TemplateField Visible='false'>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                             <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                             <asp:Label ID="lblVendorID" runat="server" Text='<%# Bind("VID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle  CssClass="header_bg_l" />
                                        <ItemStyle  />
                                        <ItemTemplate>
                                           
                                            <asp:CheckBox ID="chkbox" runat="server" Enabled="<%#CommandField()%>" />
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField>
                                    <ItemTemplate>
                                       <%-- <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                       <%-- <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                       <%-- <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px">
                                       
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' ></asp:Label>
                                      </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendorName" HeaderText="<%$ Resources:DeffinityRes,VendorName%>" ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Type%>" Visible="false"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil12" runat="server" Text='<%#GetItemsType(DataBinder.Eval(Container.DataItem,"Type").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,BuyingPrice%>" ItemStyle-HorizontalAlign="Right" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil"  runat="server" Text='<%#Bind("BP","{0:F2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>"  HeaderStyle-CssClass="header_bg_r"
                                        ItemStyle-HorizontalAlign="Right">
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblSP" runat="server"  Text='<%#Bind("SP","{0:F2}")%>'></asp:Label>
                                            <asp:TextBox ID="txtQtyReq" Width="50px" Visible="false" runat="server" Text='<%#Bind("SP","{0:F2}")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="header_bg_r" />
                                        <ItemStyle HorizontalAlign="Right" />
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
   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
                    
</asp:Content>

