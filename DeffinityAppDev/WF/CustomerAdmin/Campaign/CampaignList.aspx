<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="CampaignList.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignList" %>

<%@ Register Src="~/WF/CustomerAdmin/Campaign/Controls/CampaignTabs.ascx" TagPrefix="Pref" TagName="CampaignTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Messaging 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:CampaignTabs runat="server" id="CampaignTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Email Templates
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <link rel="stylesheet" href="../../Content/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/multiselect/css/multi-select.css"/>
    <script src="../../Content/assets/js/select2/select2.min.js"></script>
	<%--<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>--%>
	<%--<script src="../../Content/assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>--%>
	<script src="../../Content/assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<%--<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>--%>
	<script src="../../Content/assets/js/multiselect/js/jquery.multi-select.js"></script>
    <style>
        .bootstrap-tagsinput {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        /*.bootstrap-tagsinput0 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput1 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput2 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput3 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
         .bootstrap-tagsinput4 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}*/
    </style>
     <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" EnableViewState ="false" SkinID="GreenBackcolor"></asp:Label>
              <asp:Label ID="lblError" runat="server" EnableViewState ="false" SkinID="RedBackcolor"></asp:Label>
                  </div>
         </div>
     <div class="form-group row">
              <div class="col-md-4 form-inline">

                  </div>
          <div class="col-md-4 form-inline">

                  </div>
          <div class="col-md-4 form-inline">
              
              <asp:HyperLink ID="btnCampain" runat="server" SkinID="Button" Text="Create Email Template" NavigateUrl="~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx" style="float:right;" />
                  
                  </div>
         </div>

     <div class="form-group row">
         <div class="col-md-12">
             <asp:GridView ID="GridList" runat="server" OnRowCommand="GridList_RowCommand">
                 <Columns>
                       <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                      <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblid1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Date and Scheduled Time" ItemStyle-Width="180px" HeaderStyle-Width="180px" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Title" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTemplateName1" runat="server" Text='<%# Bind("TemplateName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Tags" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="350px" HeaderStyle-Width="350px">
                                <ItemTemplate>
                                    <asp:Literal ID="liTags" runat="server" Text='<%# SetTagCss((string)Eval("Tags") )%>' ></asp:Literal>
                                </ItemTemplate>
                                </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Subject" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject1" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    
                     <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="del" SkinID="BtnLinkDelete" OnClientClick='return confirm("Do you really want to delete campaign?");' CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                 </Columns>
             </asp:GridView>
             </div>
         </div>
    
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        //hidetabs();
    </script>
</asp:Content>
