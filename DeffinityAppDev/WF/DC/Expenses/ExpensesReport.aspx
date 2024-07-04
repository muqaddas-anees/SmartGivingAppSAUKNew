<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ExpensesReport.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Expenses.ExpensesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%= Resources.DeffinityRes.Expenses %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Expenses Report
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row mb-5">
        
         <div class="col-lg-1 d-flex d-inline"> <label class="control-label" style="padding-top:10px"><%= Resources.DeffinityRes.Date%>:</label>  <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="DateNew"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" Visible="false" />
               
                <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender></div>
         <div class="col-lg-3"> <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" 
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />        
             <asp:Button ID="btn_ExportExcel" runat="server" SkinID="btnDefault" Text="Export to Excel" 
                                        OnClick="btn_ExportExcelExpances_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" /></div>
     
	
       

      
        
	
</div>

      <div class="col-md-4" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label">Users</label>
           <div class="col-sm-8">
                  <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
                                          
            </div>
	</div>
    <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  >
        <Columns>
             <asp:TemplateField ItemStyle-Width="3%" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Job" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Item" ItemStyle-Width="7%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Details" ItemStyle-Width="20%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Bind("amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Reimburse To" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReimburseToName" runat="server" Text='<%# Bind("ReimburseToName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
         
            <asp:TemplateField HeaderText="Accounting Code" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountingCodesName" runat="server" Text='<%# Bind("AccountingCodesName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" Visible="false" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
