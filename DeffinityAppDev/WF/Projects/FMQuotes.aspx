<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="FMQuotes" Codebehind="FMQuotes.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Quotes%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab runat="server" ID="fm" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <asp:HiddenField ID="hdQuote" runat="server" />

<asp:UpdatePanel runat="server" ID="IDPOP">
        <ContentTemplate>
    <ajaxToolkit:ModalPopupExtender CancelControlID="ImageButton1" ID="mpopBOM" runat="server"
                 PopupControlID="pnlBOM" TargetControlID="imgItemEdit"  
                 BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

                 <asp:Panel ID="pnlBOM" runat="server"  BackColor="White"
                 Style="display:none" Width="720px" Height="400px" 
                 BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
                  <div style="float:right" >
    <asp:LinkButton ID="ImageButton1" runat="server"  SkinID="BtnLinkCancel"  /></div>
    <div class="clr"></div>
    <div style="padding-left:10px">
                   <asp:GridView ID="grdItems" runat="server" Width="591px" DataKeyNames="ID" ShowFooter="true"
                                                 AutoGenerateColumns="false"  EmptyDataText="No items found" 
                                                 onrowcommand="grdItems_RowCommand" onrowupdated="grdItems_RowUpdated" 
                                                 onrowupdating="grdItems_RowUpdating" onrowdeleting="grdItems_RowDeleting" 
                                                 onrowdatabound="grdItems_RowDataBound" 
                       OnRowEditing="grdItems_RowEditing" 
                       onrowcancelingedit="grdItems_RowCancelingEdit">
                                                 <Columns>
                                                     <asp:TemplateField>
                                                         <HeaderStyle CssClass="header_bg_l" />
                                                        <%--<ItemStyle Width="100px" />--%>
                                                         <ItemTemplate>
                                                             <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                 CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                                                 
                                                             </asp:LinkButton>
                                                           
                                                         </ItemTemplate>
                                                         <EditItemTemplate >
                                                             <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" CommandArgument="<%# Bind('ID')%>"
                                                                 SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>" >
                                                             </asp:LinkButton>
                                                             <asp:LinkButton  ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                 SkinID="BtnLinkCancel"  ToolTip="<%$ Resources:DeffinityRes,Cancel%>"></asp:LinkButton>
                                                         </EditItemTemplate>
                                                         
                                                     </asp:TemplateField>
                                                     <asp:TemplateField Visible='false'>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Worksheet%>">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblworksheet" runat="server" Text='<%# Bind("WorkSheet") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                                         <EditItemTemplate>
                                                           <asp:Label ID="lblItemNo" runat="server" Text="<%#Bind('ItemNo')%>" Visible="false"></asp:Label>
                                                             <asp:TextBox ID="txtitemdesc" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="Req2" runat="server" Display="None"
                                                                 ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterItemDescription%>" ValidationGroup="grpgrd" ControlToValidate="txtitemdesc"></asp:RequiredFieldValidator>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,UnitPrice%>" Visible="false">
                                                         <%--<EditItemTemplate>
                                                             <asp:TextBox ID="txtunitprice" runat="server" Text='<%# Bind("UnitPrice","{0:N2}") %>' SkinID="Price"></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="req3" runat="server" Display="None"
                                                                 ErrorMessage="Please enter Price" ValidationGroup="grpgrd" ControlToValidate="txtunitprice"></asp:RequiredFieldValidator>
                                                         </EditItemTemplate>--%>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblunitprice" runat="server" Text='<%# Bind("UnitPrice","{0:N2}") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Quantity%>">
                                                     <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                       
                                                         <ItemTemplate>
                                                             <asp:Label ID="lbltxtqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                         
                  <asp:Label ID="lblTotal11" runat="server" Text="Sub Total" Font-Bold="true"></asp:Label>                 
                  <br/>           
                  <asp:Label ID="lblVatText1" runat="server" Text=" " Font-Bold="true"></asp:Label>                 
                   <br/>
                  <asp:Label ID="lblSum11" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                 
              </FooterTemplate>
              <FooterStyle HorizontalAlign="Left" Width="100px" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Total%>">
                                                         <ItemStyle Width="100px"  HorizontalAlign="Right"/> 
                                                         <ItemTemplate>
                                                             <asp:Label ID="lbltotal" runat="server" Text='<%# Bind("Total","{0:N2}") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         
                                                         <ItemStyle HorizontalAlign="Right" />
                                                           <EditItemTemplate>
                                                             <asp:TextBox ID="txtTotal" runat="server" Text='<%# Bind("Total","{0:N2}") %>' Width="50px" SkinID="Price"></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="Req1" runat="server" Display="None"
                                                                 ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentertotal%>" ValidationGroup="grpgrd" ControlToValidate="txtTotal"></asp:RequiredFieldValidator>
                                                         </EditItemTemplate>
                                                         <FooterTemplate>
                                                      <asp:Label ID="lblSum1" runat="server" Text=""></asp:Label>
                                                       <br/>               
                                                      <asp:Label ID="lblVat1" runat="server" Text=""></asp:Label>
                                                       <br/>
                                                      <asp:Label ID="lblTotal1" runat="server" Text=""></asp:Label>
                                                      
                                                                </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Right" Width="100px"  />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                         
                                                         <ItemTemplate>
                                                             <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" CommandArgument="<%# Bind('ID')%>"
                                                                 SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes,Delete%>"></asp:LinkButton>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                     </asp:TemplateField>
                                                 </Columns>
                                             </asp:GridView>
                                             </div>
                                             
                 </asp:Panel>
               
 <asp:Button runat="server" ID="imgItemEdit"  style="display:none"/>
   </ContentTemplate>
                 </asp:UpdatePanel>

    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlCustomers" runat="server" SkinID="ddl_90"  AutoPostBack="true"
                onselectedindexchanged="ddlCustomers_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Projects%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlProjects" runat="server" SkinID="ddl_90" 
                AutoPostBack="True" onselectedindexchanged="ddlProjects_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
   <asp:HiddenField ID="hidVat" runat="server" />
    <asp:GridView ID="grdProjets" runat="server" AutoGenerateColumns="false"
             Width="60%" EmptyDataText="No Records Found" AllowPaging="True" 
                 PageSize="15" onpageindexchanging="grdProjets_PageIndexChanging" 
            onrowcommand="grdProjets_RowCommand" 
            onrowdeleting="grdProjets_RowDeleting">
             <Columns>
             <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QuoteNo%>" HeaderStyle-CssClass="header_bg_l" HeaderStyle-Width="10%">
             <ItemTemplate>
                <asp:LinkButton runat="server" Text="<%# Bind('QuoteNo')%>" ID="lnkQuote" CommandArgument="<%# Bind('QuoteID')%>" CommandName="View" ></asp:LinkButton>
                 <asp:Label ID="lblVat" runat="server" Text='<%# Bind("Vat") %>' Visible="false"> </asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
                 
                   <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>"  DataField="ProjectTitle" ItemStyle-Width="25%" />
                 <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Customer%>"  DataField="PortFolio" ItemStyle-Width="25%" />
             <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Author%>"  DataField="RaisedBy" ItemStyle-Width="25%" />
              <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Date%>" DataField="RaisedDate" 
               DataFormatString="{0:d}" ItemStyle-Width="10%"/>
               <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,QuoteValue%>" DataField="TotalVal" ItemStyle-Width="10%"
              ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
               <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderStyle-Width="5%">
                                                         
                                                         <ItemTemplate>
                                                             <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" CommandArgument="<%# Bind('ID')%>"
                                                                 SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes,Delete%>"></asp:LinkButton>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                     </asp:TemplateField>
                 </Columns>
                 </asp:GridView>

   
    
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


