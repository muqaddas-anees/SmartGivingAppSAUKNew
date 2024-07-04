<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ChangeControlManagement"
    Title="Change Control Summary" Codebehind="ChangeControlManagement.aspx.cs" %>
<%@ Register src="controls/changecontrol_summarytab.ascx" tagname="changecontrol_summarytab" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Change Request
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server" Visible="false">
    <%--<uc1:changecontrol_summarytab ID="changecontrol_summarytab1" runat="server" />--%>
     <%--<ul class="tabs_list1" style="float: right;">
     <li class="menu_tab" id="link_menu" runat="server"><a href="#"><span>&nbsp;Setup&nbsp;&nbsp;</span></a>
            <ul>
                <li><a href="SecurityAccess.aspx?tab=securityaccess">Admin Dropdown</a></li>
               
            </ul>
        </li>--%>
         <%--</ul>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-6">
                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                     <asp:ListItem Value="New">New</asp:ListItem>
                                    <asp:ListItem Value="In Hand">In Hand</asp:ListItem>
                                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6 pull-right" style="float:right;">
         
           <asp:Button ID="imgNewChange" runat="server" SkinID="btnDefault" Text="Log New Change Request" OnClick="imgNewChange_Click" style="float:right;" />
           
	</div>
</div>
     
               
             <asp:Panel ID="pnlChangeControl" runat="server">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <asp:GridView ID="gridChangeControl" runat="server" 
                        DataSourceID="gridFiller" AutoGenerateColumns="False"
                                OnRowCommand="gridChangeControl_RowCommand"  AllowPaging="True"
                                OnRowCreated="gridChangeControl_RowCreated" 
                        EmptyDataText="No change controls exist." Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Reference" SortExpression="ID">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnChangeControl" runat="server" Text='<%#"Change:"+Eval("ID") %>'
                                                CommandName="Change" CommandArgument='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:BoundField DataField="Portfolioname" HeaderText="Customer" SortExpression="Customer" />
                                    <asp:TemplateField HeaderText="Description" SortExpression="ChangeDescription" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ChangeDescription") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ChangeDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Justification" SortExpression="Justification" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Justification") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Justification") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    <asp:TemplateField HeaderText="Date Raised" SortExpression="DateRaised">
                                        <ItemTemplate>
                                            <asp:Literal ID="litDateRaised" runat="server" Text='<%#Eval("DateRaised","{0:d}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Release Date" SortExpression="TargetReleaseDate">
                                        <ItemTemplate>
                                            <asp:Literal ID="litReleaseDate" runat="server" Text='<%#Eval("TargetReleaseDate","{0:d}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Resource Impact" SortExpression="ResourceImpact" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ResourceImpact") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label3" runat="server" Text='<%# Bind("ResourceImpact") %>'></asp:Label>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CoOrdinatorName" HeaderText="Co-ordinator" >
                                    </asp:BoundField>
                                     <asp:TemplateField ItemStyle-Width="20px" HeaderText="In Hand SLA" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# GetInHandSLA(Eval("InHandSLAMet").ToString(), Eval("Status").ToString())%>
                        </ItemTemplate>
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-Width="20px" HeaderText="Resolution SLA" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# GetResolutionSLA(Eval("ClosedSLAMet").ToString(), Eval("Status").ToString())%>
                        </ItemTemplate>
                             <HeaderStyle CssClass="header_bg_r" />
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                       
                    <asp:ObjectDataSource ID="gridFiller" runat="server" TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtChangeControlTableAdapter"
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="GetChangeControlByStatus"  >
                        <SelectParameters>
                            <asp:Parameter Name="portfolioID" Type="Int32" DefaultValue="0" />
                            <asp:ControlParameter ControlID="ddlstatus" DefaultValue="All" Name="status" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>

     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
    
 <script  type="text/javascript">
     $(document).ready(function () {
         $('#navTab').hide();
     });
       </script>

</asp:Content>
