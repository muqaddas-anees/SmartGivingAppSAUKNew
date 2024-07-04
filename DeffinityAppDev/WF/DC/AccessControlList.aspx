<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_AccessControlList" Codebehind="AccessControlList.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
 Access Control
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="grdaccessctrl" runat="server" AutoGenerateColumns="false" 
        AllowPaging="true" PageSize="10" EmptyDataText="No data available" Width="100%" 
        onpageindexchanging="grdaccessctrl_PageIndexChanging" 
        onrowdatabound="grdaccessctrl_RowDataBound">
<Columns>

                         <asp:TemplateField HeaderText="Company" >
                        <ItemTemplate>
                            <asp:Label ID="lblcmpy" runat="server" Text='<%# Eval("Company") %>' ></asp:Label>
                            </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Name" >
                          <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' ></asp:Label>
                            </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Site" >
                        <ItemTemplate>
                            <asp:Label ID="lblSite" runat="server" Text='<%# Eval("Site") %>' ></asp:Label>
                            </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="RequestType" >
                        <ItemTemplate>
                            <asp:Label ID="lblRType" runat="server" Text='<%# Eval("RequestType") %>' ></asp:Label>
                            </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status" >
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' ></asp:Label>
                            </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                          <asp:TemplateField >
                   <ItemTemplate>
                    <%-- <asp:HiddenField ID="h_callid" runat="server" Value='<%# Eval("CallID") %>' />--%>
                      <a href='AccessControl.aspx?callid=<%# Eval("CallID")%>'>Edit</a>
                     
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Left" />
               </asp:TemplateField>
               
                         </Columns>

</asp:GridView>

     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>

