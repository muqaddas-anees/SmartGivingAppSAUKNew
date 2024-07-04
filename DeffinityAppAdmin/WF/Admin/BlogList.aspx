<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="BlogList.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.BlogList" %>
<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
   Financial Services
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group">
                                  <div class="col-md-12" style="float:right;">
                                      <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault" Text="Add New" OnClick="btnAdd_Click" style="float:right;"  />
                                      </div>
        </div>

    <asp:GridView ID="gridBlog" runat="server" OnRowCommand="gridBlog_RowCommand">
        <Columns>
            <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="blogedit" CommandArgument='<%# Bind("BlogRef") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField  ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Button ID="btnPreview" runat="server" SkinID="btnDefault" Text="Preview" CommandName="preview"  CommandArgument='<%# Bind("BlogRef") %>'></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Title"  ItemStyle-Width="50%">
                <ItemTemplate>
                   <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("BlogTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                   <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("IsActive") %>'></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Start Date"  ItemStyle-Width="15%">
                <ItemTemplate>
                   <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="End Date"  ItemStyle-Width="15%">
                <ItemTemplate>
                   <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Date"  ItemStyle-Width="15%">
                <ItemTemplate>
                   <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LoogedDatetime","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText=""  ItemStyle-Width="15%">
                <ItemTemplate>
                   <asp:Image ID="img" runat="server" Height="100" ImageUrl='<%# string.Format("https://site.smartgiving.app/WF/UploadData/Customers/Blog_{0}.png?d={1}", Eval("BlogRef"),DateTime.Now.ToShortTimeString() )%>' />
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField  ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete" CommandName="blogdelete" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
