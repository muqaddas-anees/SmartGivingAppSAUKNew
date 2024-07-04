<%@ Page Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="CustomerSC" Title="Customer Service Catalogue" Codebehind="CustomerSC.aspx.cs" %>
<%@ Register Src="controls/CustomerOrderTabs.ascx" TagName="CustomerOrderTabs" TagPrefix="uc3" %>
<%@ Register Src="controls/CARTSummary.ascx" TagName="CART" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc3:CustomerOrderTabs ID="CustomerOrderTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%: Resources.DeffinityRes.CustomerPortal %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
  <%-- Services/Products--%> Service Catalogue
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
      <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="form-group row">
          <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
          </div>
    <div class="row">
                                <div class="col-md-9">
                                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_90" DataTextField="CategoryName" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
        </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.SubCategory%></label>
                                      <div class="col-sm-7"><asp:DropDownList ID="ddlSubCategory" runat="server" SkinID="ddl_90" AutoPostBack="True"
            OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
            <asp:ListItem Text=" Please Select..." Value="0"></asp:ListItem>
        </asp:DropDownList>
					</div>
				</div>

</div>
                                     <div class="form-group row">
                                         <div class="col-md-4">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.SelectCatalogue%></label>
                                      <div class="col-sm-6"><asp:DropDownList ID="ddlSelect" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlSelect_SelectedIndexChanged1" >
        <asp:ListItem Value="0">All</asp:ListItem>
        <asp:ListItem Value="1">Labour</asp:ListItem>
        <asp:ListItem Value="2">Products</asp:ListItem>
        <asp:ListItem Value="3">Service</asp:ListItem>
        <%--<asp:ListItem Value="4">Services minus Labour</asp:ListItem>--%>
    </asp:DropDownList>
					</div>
				</div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SearchbyDescription%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtSearchDescription" runat="server" ></asp:TextBox>
<ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="GetServices"
 UseContextKey="true" ContextKey="Country" ServicePath="CustomerSCAutoComplete.asmx" TargetControlID="txtSearchDescription">
 </ajaxToolkit:AutoCompleteExtender>
					</div>
				</div>
 <div class="col-md-2">
                                      <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" 
        onclick="btnSearch_Click" CausesValidation="false" />
				
				</div>
</div>

                                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="Obj_Description"
        OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="100%" 
        EnableViewState="false" AllowPaging="True" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
              <%--  <ajaxToolkit:AnimationExtender id="MyExtender"
          runat="server" TargetControlID="pnlOriginalImage_1" >
          <Animations>
          <%--<OnLoad><FadeIn Duration=".5" Fps="20" /></OnLoad>--%>
            <%--<OnClick>
              <FadeIn Duration=".5" Fps="20" />
            </OnClick>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
          </Animations>
</ajaxToolkit:AnimationExtender>--%>
           <%-- <ajaxToolkit:HoverMenuExtender ID="hmeDetails1" runat="server" 
                            TargetControlID="Image1" 
                            PopupControlID="pnlOriginalImage_1" 
                            PopDelay="0"
                            PopupPosition="Left" 
                            EnableViewState="false"
                            OffsetY="26" />--%>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'  Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                <%-- <div id="pnlOriginalImage_1" runat="server" class="PrepRecipeDetails"  style="display: none; height:auto;width:auto;">

            <asp:Image ID="Image2" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>' Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
            </div>--%>
                </ItemTemplate>
                <HeaderStyle CssClass="header_bg_l" />
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" SortExpression="CategoryName" />
            <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category" SortExpression="SubCategoryName" />
            <asp:BoundField DataField="SP" HeaderText="Selling Price" 
                ItemStyle-HorizontalAlign="right" SortExpression="SP" 
                DataFormatString="{0:F2}" HtmlEncode="false" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
            <asp:BoundField DataField="DiscountPrice" HeaderText="Discount Price to Customer"
                ItemStyle-HorizontalAlign="Right" SortExpression="DiscountPrice"
                DataFormatString="{0:F2}" HtmlEncode="false"  ItemStyle-Width="60px" />
            <asp:BoundField DataField="Details" HeaderText="Details" Visible="false" SortExpression="DiscountPrice" />
            
            <asp:BoundField DataField="Details" HeaderText="Details" Visible="false" SortExpression="Details" />
            <asp:TemplateField>
                <HeaderTemplate>
                    Add to Cart
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hdnID" runat="Server" Value='<%#Bind("ID") %>' />
                    <asp:HiddenField ID="HD_ServiceType" runat="Server" Value='<%#Bind("ServiceType") %>' />
                    <asp:LinkButton ID="lnkPayNow" runat="server" Text="Add to Cart" CommandName="Select"
                        CommandArgument='<%# Bind("ID")%>' />
                    <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="lnkPayNow"
                        PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="center" EnableViewState="false"
                        OffsetY="26" />
                        <div class="drop_add" id="pnlOriginalImage1" runat="server" visible="false">
<table width="200" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>Quantity : </td>
    <td><input  class="txt" name="input" type="text" style="width:40px" /></td>
    <td><input name="input2" class="btn_login" type="button" title="Add to Cart" /></td>
  </tr>
</table>

</div>
                    <div id="pnlOriginalImage" runat="server" style="background-color:ThreeDFace;" >
                        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Quantity%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtQty" runat="server" Text="1" ValidationGroup="AddItem" SkinID="txt_50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtQty"
                                                            ErrorMessage="Please enter valid quantity" Operator="DataTypeCheck" Type="Integer"
                                                            ValidationGroup="AddItem"></asp:CompareValidator>
					</div>
				</div>
                </div>
                        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8"> <asp:Button id="imgbtnAdd" runat="server" SkinID="btnAdd" ToolTip="Add to Cart" ValidationGroup="AddItem" CommandName="Select"
                                                            CommandArgument='<%# Bind("ID")%>' ></asp:Button>
					</div>
				</div>
                </div>
                   
                    </div>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <asp:ObjectDataSource ID="Obj_Description" runat="server" 
            SelectMethod="ServiceCatalog_ByName" 
            TypeName="Deffinity.ServiceCatalogManager.ServiceCatalogManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSelect" DefaultValue="0" Name="Type" 
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter DefaultValue="0" Name="PortfolioID" 
                    SessionField="PortfolioID" Type="Int32" />
                <asp:ControlParameter ControlID="ddlCategory" DefaultValue="0" Name="Category" 
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlSubCategory" DefaultValue="0" 
                    Name="SubCategory" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="txtSearchDescription" DefaultValue="" 
                    Name="prefixText" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        SelectCommand="Deffinity_GetServiceCatlogue" SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:DBstring %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlSelect" DefaultValue="1" Name="Type" PropertyName="SelectedValue"
                Type="Int32" />
           <asp:ControlParameter ControlID="ddlCategory" DefaultValue="1" Name="Category" PropertyName="SelectedValue"
                Type="Int32" /> 
           <asp:ControlParameter ControlID="ddlSubCategory" DefaultValue="1" Name="SubCategory" PropertyName="SelectedValue"
                Type="Int32" />         
                <asp:SessionParameter Name="PortfolioID" DefaultValue="0" SessionField="PortfolioID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="DS_Category" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBstring %>" 
            SelectCommand="Deffinity_GetServiceCategory" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="PortfolioID" SessionField="PortfolioID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="DS_SubCategory" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBstring %>" 
            SelectCommand="Deffinity_GetServiceSubCategory" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCategory" DefaultValue="0" 
                    Name="CategoryId" PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter DefaultValue="0" Name="PortfolioID" 
                    SessionField="PortfolioID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
                                    </div>
                                  <div class="col-md-3">
                                      <uc1:CART ID="CART1" runat="server" />
                                    </div>
                                 </div>
   
  
</ContentTemplate>
    </asp:UpdatePanel>
        <asp:Button ID="btnNext" 
            runat="server" SkinID="btnDefault" CausesValidation="False" 
            onclick="btnNext_Click" Visible="False" />

   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

