<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="VT.ApproveRequests" Codebehind="VT.ApproveRequests.aspx.cs" %>
<%--<%@ Register Src="~/controls/VTMain.ascx" TagName="VTTabs" TagPrefix="VTMainTab" %>--%>
<%@ Register src="MailControls/VTLeaveApproveRejectMail.ascx" tagname="VT" tagprefix="Mail1" %>
<%@ Register Src="controls/VTsubtabs.ascx" TagName="VTTabs" TagPrefix="VTMainSubTab" %>
<%@ Register src="controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   <%-- <VTMainTab:VTTabs ID="VTTab" runat="server" />--%>
    <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
    <Mail1:VT ID="VTMail1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Resources%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Pending Request(s)
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <VTMainSubTab:VTTabs ID="VTTab" runat="server" />
    <br />
    <div class="row">
<div class="col-md-12 text-bold">
    <p>Pending Requests where you are an approver</p>
</div>
</div>
     <asp:GridView ID="grdrequests" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                                DataSourceID="objRequests" EmptyDataText="No requests arrived" DataKeyNames="UserID"
                                OnRowCommand="grdrequests_RowCommand" OnRowEditing="grdrequests_RowEditing">
                                <Columns>
                                    <%--<asp:BoundField HeaderText="Requester" DataField="UserName" HeaderStyle-CssClass="header_bg_l" />--%>
                                     <asp:TemplateField HeaderText="Requester">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="header_bg_l" />
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Requested Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreqdate" runat="server" Text='<% #Eval("DateRequested","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Absense Type" DataField="AbsenseType" />--%>
                                     <asp:TemplateField HeaderText="Absence Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblabsensetype" runat="server" Text='<% #Eval("AbsenseType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    
                                    <asp:TemplateField HeaderText="From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="server" Text='<% #Eval("FromDate","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="To">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<% #Eval("ToDate","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Days" DataField="Days" />--%>
                                    <asp:TemplateField HeaderText="Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldays" runat="server" Text='<% #Eval("Days") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Status" DataField="ApprovalStatus" />--%>
                                    
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<% #Eval("ApprovalStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" Enabled='<%#enableApprove(Eval("ApprovalStatus").ToString()) %>'
                                                CommandName="Approve" CommandArgument='<%#Eval("ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnReject" runat="server" Text="Reject" Enabled='<%#enableRejection((Eval("ApprovalStatus").ToString()),(Eval("FromDate").ToString())) %>'
                                                CommandName="Reject" CommandArgument='<%#Eval("ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseremail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproveremail" runat="server" Text='<%# Eval("ApproverEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver Notes">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtapprovernotes" runat="server" Text='<% #Eval("ApproverNotes") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="header_bg_r" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="objRequests" runat="server" SelectMethod="SelectByApprover"
                                TypeName="VT.DAL.LeaveApproverHelper" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:SessionParameter Name="approverID" SessionField="UID" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
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

