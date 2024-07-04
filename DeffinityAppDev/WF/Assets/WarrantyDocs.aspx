<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="WarrantyDocs.aspx.cs" Inherits="DeffinityAppDev.WF.Assets.WarrantyDocs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Warranty Docs
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    Warranty Docs
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

     <div class="form-group row">
          <div class="col-md-12">
              <label class="col-sm-3 control-label">Upload Documents</label>
               <div class="col-sm-4">
                    <asp:FileUpload ID="fileupload" runat="server" ClientIDMode="Static" AllowMultiple="true" />
               </div>
              <div class="col-sm-4">
                  <asp:Button SkinID="btnUpload" ID="btnUpload" runat="server" OnClick="btnUpload_Click" />
                  </div>
          </div>
    </div>
    <div class="form-group row">
          <div class="col-md-12">
              <label class="col-sm-2 control-label"></label>
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false">
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" />
                      <asp:TemplateField>
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
