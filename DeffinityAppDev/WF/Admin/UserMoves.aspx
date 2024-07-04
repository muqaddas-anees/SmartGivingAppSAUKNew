<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="UserMoves" Codebehind="UserMoves.aspx.cs" %>

<%@ Register Src="controls/MangeUserTab.ascx" TagName="MangeUserTab" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Moves
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   
                <asp:Panel ID="pnlusername" runat="server">
                    <div class="form-group">
             <div class="col-md-12 form-inline">
                  <%= Resources.DeffinityRes.UserAdminfor%>:  <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
                    <asp:Panel ID="pnl" runat="server">
                        <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
                    </asp:Panel>
                    </asp:Panel>
                   <div class="form-group">
             <div class="col-md-12">
                 <p>This page is associated with the Moves module. The grid shows the desk movement of this user.</p>
</div>
</div>
                     
                   <div class="form-group">
             <div class="col-md-12 form-inline">
                 Current desk position:  <asp:Label ID="lblCurrentDesk" runat="server" Font-Bold="true" ></asp:Label>
</div>
</div>
                    
                   <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.History %> </strong>
            <hr class="no-top-margin" />
            </div>
</div> 
                <asp:GridView ID="gvUserMoves" runat="server" EmptyDataText ="No Moves Found">
                     <Columns>
                     <asp:BoundField DataField="Surname" HeaderText="Surname" />
                     <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                     <asp:BoundField DataField="Desk" HeaderText="Desk" />
                     <asp:BoundField DataField="MoveDate" HeaderText="Move Date" /> 
                     <asp:BoundField DataField="TicketRef" HeaderText="Ticket Reference"/> 
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
