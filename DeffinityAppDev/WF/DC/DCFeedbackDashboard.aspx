<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DCFeedbackDashboard.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCFeedbackDashborad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <label id="lblTitle" runat="server">Feedback Dashboard</label> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
   Feedback Executive Summary 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     
    <h4></h4>
    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtfrom" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="lblsc" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-7 form-inline">
                <asp:TextBox ID="txtTodate" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="Label10" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	<div class="col-md-2">
           <asp:Button SkinID="btnView" runat="server" />
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-10">
    <asp:GridView ID="gridDashboard" runat="server">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("col1") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("col1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="Reviews Posted This Week">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("col2") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("col2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="Trend Previous Week">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("col3") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("col3") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="Reviews Posted Past 30 Days">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("col4") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("col4") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="Trend Previous Month">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("col5") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("col5") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
          </div>
        </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
      <script type="text/javascript">
        $(document).ready(function () {
            hidetabs();
        });
        
    </script>
</asp:Content>
