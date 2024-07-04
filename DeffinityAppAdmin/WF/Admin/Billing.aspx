<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Billing" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Billing
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    
    <asp:GridView ID="GridBilling" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridBilling_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" Visible="false"  >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Instance">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPortFolio" runat="server" Text='<%# Bind("PortFolio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Billing To">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBillingTo" runat="server" Text='<%# Bind("BillingTo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Payment Date"  ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%# String.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Eval("PaymentDate")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Payment Reference">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentReference" runat="server" Text='<%# Bind("PaymentReference") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Payment Term">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentTerm" runat="server" Text='<%# Bind("PaymentTerm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Transation Start Date" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransationStartDate" runat="server" Text='<%# String.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Eval("TransationStartDate")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Transation End Date" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransationEndDate" runat="server" Text='<%# String.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Eval("TransationEndDate")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Notes">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSendInvoice" runat="server" SkinID="btnDefault" Text="Send Invoice" CommandName="send" CommandArgument='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
          
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
