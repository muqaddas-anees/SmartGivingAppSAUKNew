<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeFile="PortfolioRag.aspx.cs" Inherits="PortfolioRag" Title="Untitled Page" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>
<%@ Register src="controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server" Visible="false">
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.KeyMilestones%>  - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <%-- <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong><%= Resources.DeffinityRes.AddKeyMilestones%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
    <div class="row">
<div class="col-md-12">
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtRagName" ErrorMessage="Please enter Key Milestone" 
                ValidationGroup="Group1"></asp:RequiredFieldValidator>
</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.KeyMilestone%></label>
                                      <div class="col-sm-6"><asp:TextBox ID="txtRagName" runat="server" SkinID="txt_80" MaxLength="200"></asp:TextBox>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Description%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtRAGDescription" runat="server" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox> 
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:Button  ID="btnApply" runat="server" SkinID="btnSubmit" 
                onclick="btnApply_Click" ValidationGroup="Group1"/> 
            <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" 
                CausesValidation="false" onclick="btnCancel_Click" />
					</div>
				</div>
</div>
   
 
    <asp:GridView ID="GridViewProjectRag" runat="server" DataKeyNames="ID" Width="100%" 
                onrowcancelingedit="GridViewProjectRag_RowCancelingEdit" 
                onrowcommand="GridViewProjectRag_RowCommand" 
                onrowediting="GridViewProjectRag_RowEditing" 
                onrowdeleting="GridViewProjectRag_RowDeleting" 
                onrowupdated="GridViewProjectRag_RowUpdated" 
                onrowupdating="GridViewProjectRag_RowUpdating">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="75px" />
            </asp:TemplateField>
        <asp:TemplateField  HeaderText="Key Milestone">
        <ItemStyle Width="200px" />
        <ItemTemplate>
        <asp:Literal ID="lblRAGSectionName" runat="server" Text='<%#Eval("RAGSectionName") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:TextBox ID="txtRAGSectionName" runat="server" Text='<%#Eval("RAGSectionName") %>' Width="200px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtRAGSectionName" ErrorMessage="Please enter RAG Name /Area" 
                ValidationGroup="Group2"></asp:RequiredFieldValidator>
        </EditItemTemplate>
        </asp:TemplateField>     
        <asp:TemplateField HeaderText="Description">
        <ItemStyle Width="300px" />
        <ItemTemplate>
        <asp:Literal ID="lblDescription" runat="server" Text='<%#Eval("RAGDescription") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("RAGDescription") %>' Width="300px"></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" SkinID="BtnLinkDelete"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="75px" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
        No <%= Resources.DeffinityRes.KeyMilestones%> available.
        </EmptyDataTemplate>
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

