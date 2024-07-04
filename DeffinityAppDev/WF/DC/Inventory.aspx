<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="DC_Inventory" Codebehind="Inventory.aspx.cs" %>

<%@ Register Src="~/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FlsTab ID="flstab1" runat="server" />
    <ul class="tabs_list5" style="float:right;">
    <li class="current5"><a id="A1" href="FLSJlist.aspx?type=FLS" runat="server" target="_self"><span>Return to Ticket Journal</span></a></li>
   
</ul>
</asp:Content>
<asp:Content ID="Content11" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Panel ID="pnlInventroy" runat="server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
       <tr>    
      <td>
          <h1 class="section1">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          <div class="flds_11">
             <%-- <span class="space_r50 float_l">Customer: <b>
                  <%=sessionKeys.PortfolioName %></b></span>--%>
                  
          </div>
           <span class="space_r50" style="float:right">
          <%--<asp:Button ID="ImageButton1" runat="server" SkinID="ImgRetruntoSR" OnClick="rtrnSD_Click" />--%>
          </span>
      </td>
  </tr>
        <tr>

<td class="p_section1 data_carrier_block">
                
            <asp:Label ID="lblErroMsg" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
            <table>
            <tr>
                <td>Category </td> <td><asp:DropDownList ID="ddlCategory" runat="server"
                 onselectedindexchanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" Width="150px"></asp:DropDownList> </td>
                <td>Sub Category </td><td><asp:DropDownList ID="ddlSubCategory" runat="server"
                onselectedindexchanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="true" Width="150px" ></asp:DropDownList> </td>
               <td>Site</td>                                
                <td><asp:DropDownList ID="ddlSite" runat="server"></asp:DropDownList></td>
                <td>Product</td> <td><asp:DropDownList ID="ddlProduct" runat="server"
                onselectedindexchanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" Width="150px"></asp:DropDownList> 
                
                
                &nbsp; <%--<asp:Button ID="imgAddInventory" runat="server" SkinID="ImgSymAdd" 
                                onclick="imgAddInventory_Click" />--%>
                 </td>
            </tr>
            <tr>
                <td>Qty Used </td><td><asp:TextBox ID="txtQty" runat="server" Width="50px"></asp:TextBox>
                <asp:CompareValidator ID="cmpRate11" runat="server" 
                         ControlToValidate="txtQty" Display="None" 
                         ErrorMessage="Please enter valid ReorderLevel" Operator="DataTypeCheck" 
                         SetFocusOnError="True" Text="Invalid ReorderLevel" Type="Integer" 
                         ValidationGroup="AddInvnt" />
                 </td>
                <td>Qty Replenished </td><td><asp:TextBox ID="txtQtyRel" runat="server" Width="50px" ></asp:TextBox> </td>
                <td><asp:Button ID="imgbtnUpdate" runat="server" SkinID="ImgUpdate" 
                        onclick="imgbtnUpdate_Click" /> </td>
            </tr>
            </table>
            <asp:Panel ID="pnlInventory" runat="server" Visible="false">
            <table cellpadding="0" cellspacing="0" class="sec_table2" width="100%">
            <tr><td>
            <asp:ValidationSummary ID="grdInventoryValidation" runat="server" ValidationGroup="UpdInvnt" />
           </td></tr> 
           <tr>
           <td>
             <asp:GridView ID="grdInventory" runat="server" 
             AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
             OnPageIndexChanging="grdInventory_PageIndexChanging" Visible="true" 
             OnRowCancelingEdit="grdInventory_RowCancelingEdit" 
             OnRowCommand="grdInventory_RowCommand" 
             OnRowEditing="grdInventory_RowEditing" OnRowUpdating="grdInventory_RowUpdating" 
                   onrowdeleting="grdInventory_RowDeleting" >
         <Columns>
             <asp:TemplateField>
                 <HeaderStyle Width="55px" />
                 <ItemStyle Width="55px" />
                 <ItemTemplate>
                     <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" 
                         CommandArgument="<%# Bind('ID')%>" CommandName="Edit" 
                         SkinID="BtnLinkEdit" ToolTip="Edit"   />
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:LinkButton ID="LinkButtonUpdate" runat="server" 
                         CommandArgument="<%# Bind('ID')%>" CommandName="Update" 
                         SkinID="BtnLinkUpdate" ToolTip="Update" ValidationGroup="UpdInvnt" />
                     <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" 
                         CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" />
                 </EditItemTemplate>
                 <HeaderStyle CssClass="header_bg_l" />
             </asp:TemplateField>
             <asp:TemplateField>
                 <ItemTemplate>
                     <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" 
                         TargetControlID="pnlOriginalMaterial">
                         <Animations>
          
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
          </Animations>
                     </ajaxToolkit:AnimationExtender>
                     <ajaxToolkit:HoverMenuExtender ID="hmeMaterials" runat="server" 
                         EnableViewState="false" OffsetY="26" PopDelay="0" 
                         PopupControlID="pnlOriginalMaterial" PopupPosition="Left" 
                         TargetControlID="imgContractor" />
                     <asp:Image ID="imgContractor" runat="server" 
                         ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>' 
                         Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                     <div ID="pnlOriginalMaterial" runat="server" class="PrepRecipeDetails" 
                         style="display: none;">
                         <asp:Image ID="Image1" runat="server" 
                             ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>' 
                             Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                     </div>
                 </ItemTemplate>
                 <ItemStyle Width="100px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Category" SortExpression="Category">
             <ItemTemplate>
             <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
             </ItemTemplate>
             <ItemStyle Width="150px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Sub Category" SortExpression="SubCategory">
                  <ItemTemplate>
                     <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("SubCategoryName") %>'> 
                         </asp:Label>
                 </ItemTemplate>
                 <ItemStyle Width="150px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">
                 <ItemTemplate>
                     <asp:Label ID="lblDesc" runat="server" Text='<% #Bind("ItemDescription")  %>'></asp:Label>
                 </ItemTemplate>
                 <ItemStyle Width="150px" />
                 </asp:TemplateField>
             <asp:TemplateField HeaderText="Site" SortExpression="Site">
                 <ItemTemplate>
                     <asp:Label ID="lblManufcturer" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                 </ItemTemplate>
                 <ControlStyle Width="150px" />
             </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Colour" SortExpression="Colour">
                 <ItemTemplate>
                     <asp:Label ID="lblColour" runat="server" Text='<%# Bind("Colour") %>'></asp:Label>
                 </ItemTemplate>
                 <ControlStyle Width="60px" />
                 <ItemStyle />
             </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Length" SortExpression="Length">
                 <ItemTemplate>
                     <asp:Label ID="lblLength" runat="server" 
                         Text='<%# Bind("Length") %>'></asp:Label>
                 </ItemTemplate>
                 <ControlStyle Width="60px" />
                 <ItemStyle />
             </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Quantity used" SortExpression="QtyUsed">
                 <EditItemTemplate>
                     <asp:TextBox ID="QTYtxt" runat="server" 
                         Text='<%# Bind("QtyUsed") %>' Width="60px"></asp:TextBox>
                     <asp:CompareValidator ID="cmpRate10" runat="server" 
                         ControlToValidate="QTYtxt" Display="None"
                         ErrorMessage="Please enter valid QTY" Operator="DataTypeCheck" 
                         SetFocusOnError="True" Text="Invalid QTY" Type="Integer" 
                         ValidationGroup="UpdInvnt" />
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblQTY" runat="server" 
                         Text='<%# Bind("QtyUsed") %>'></asp:Label>
                 </ItemTemplate>
                 <ControlStyle Width="60px" />
                 <ItemStyle HorizontalAlign="Right" Width="100px"/>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Qty Replenished" SortExpression="QtyReplenish">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtReOrderLevel" runat="server" 
                         Text='<%# Bind("QtyReplenish") %>' Width="60px" ></asp:TextBox>
                     <asp:CompareValidator ID="cmpRate11" runat="server" 
                         ControlToValidate="txtReOrderLevel" Display="None" 
                         ErrorMessage="Please enter valid ReorderLevel" Operator="DataTypeCheck" 
                         SetFocusOnError="True" Text="Invalid ReorderLevel" Type="Integer" 
                       ValidationGroup="UpdInvnt" />
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblReOrderLevel" runat="server" 
                         Text='<%# Bind("QtyReplenish") %>'></asp:Label>
                 </ItemTemplate>
                 <ControlStyle Width="60px" />
                 <ItemStyle HorizontalAlign="Right" Width="100px" />
                
             </asp:TemplateField>   
              <asp:TemplateField>
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="LinkButtonDelete1" runat="server" CommandName="Delete"
                                CommandArgument="<%# Bind('ID')%>" SkinID="ImgSymDel" ToolTip="Delete">
                            </asp:Button>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <HeaderStyle CssClass="header_bg_r" />
                    </asp:TemplateField>                   
         </Columns>
         </asp:GridView>
         </td>
           </tr>
           </table>
           
            </asp:Panel>
            
            </td>
</tr>
</table>                
</asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

