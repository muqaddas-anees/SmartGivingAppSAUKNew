<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
         AutoEventWireup="True" Inherits="GridColumns1" Codebehind="GridColumns.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    Smart Apps
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Grid columns
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink runat="Server" ID="link_back">
<i class="fa fa-arrow-left"></i> Return To App</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%-- <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#LblMsg").fadeOut(5000)
        });
    </script>
               
    <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="LblMsg" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
          <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <asp:GridView ID="GridColumns" runat="server" 
                          Width="100%" AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="GridColumns_RowDataBound">
                          <Columns>
                                <asp:TemplateField HeaderText="Visibility" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25%">
                                  <ItemTemplate>
                                      <asp:Label ID="Lblvisibility" runat="server" Text='<%#Bind("visibility")%>' Visible="false"></asp:Label>
                                      <asp:CheckBox ID="checkvisibility" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Name">
                                  <ItemTemplate>
                                          <asp:Label ID="LblId" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                      <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                            
                          </Columns>
                      </asp:GridView>
          
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 

</asp:Content>


