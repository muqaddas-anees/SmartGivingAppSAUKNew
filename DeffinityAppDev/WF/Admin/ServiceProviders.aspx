<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="ServiceProviders.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.ServiceProviders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%-- <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />--%>
     <style>
        .nav.nav-tabs + .tab-content
{
   padding:30px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <h1 class="title">Service Providers</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" >
       <div class="col-md-12">
           <ul class="nav nav-tabs">
						<%--<li class='<%=GetClassName("") %>'>
							<a href="UserManagement.aspx">All Members</a>
						</li>--%>
						<%--<li class='<%=GetClassName("Administrators") %>'>
							<a href="UserManagement.aspx?Type=Administrators">Administrators</a>
						</li>
                       <li class='<%=GetClassName("ServiceManagers") %>'>
                            <a href="UserManagement.aspx?Type=ServiceManagers">Service Managers</a>
                        </li>
                         <li class='<%=GetClassName("PM") %>' style="display:none;visibility:hidden">
                             <a href="UserManagement.aspx?Type=PM">PM with QA</a>
                         </li>--%>
						<li class='<%=GetClassName("ServiceProviders") %>'>
							  <a href="UserManagement.aspx?Type=ServiceProviders">Service Providers</a>
						</li>
              <%-- <li class='<%=GetClassName("Sales") %>'>
							  <a href="UserManagement.aspx?Type=Sales">Sales</a>
						</li>
               <li class='<%=GetClassName("CustomerService") %>'>
							  <a href="UserManagement.aspx?Type=CustomerService">Customer Service</a>
						</li>
                        <li class='<%=GetClassName("CasualLabour") %>' style="display:none;visibility:hidden">
                             <a href="UserManagement.aspx?Type=CasualLabour">Casual Labour</a>
                        </li>
                       <li class='<%=GetClassName("Dashboard") %>' style="display:none;visibility:hidden">
                            <a href="UserManagement.aspx?Type=Dashboard">Dashboard</a>
                        </li>
                        <li></li>
                        <li></li>--%>
					</ul>
        <div class="tab-content">
            <div class="tab-pane active">



                <div class="form-group row">


                      <div class="col-xs-6 form-inline">
                     <%--     <div class="form-group row">--%>
                                <%--<div class="col-xs-8 form-inline">--%>
                                      <asp:TextBox ID="TxtSearch" SkinID="txt_50" runat="server"></asp:TextBox>
                                       <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                                         runat="server" TargetControlID="TxtSearch" WatermarkText="Name or Email" />
                                      &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_20">
                                          <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                          <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Inactive" Value="2"></asp:ListItem>
                                      </asp:DropDownList> &nbsp;
                              <%--  </%--div>--%>
                               <%--  <div class="col-xs-4">--%>
                                  <asp:Button ID="BtnSearch" runat="server" Text="Search" SkinID="btnDefault" OnClick="BtnSearch_Click" />
                             <%-- </div>--%>
                         <%-- </div>--%>
                      </div>


                                <div class="col-xs-6 pull-right">
                <div style="float:right;">
                    <asp:Button ID="BtnCreateNewUser" SkinID="btnDefault" runat="server" Text="Create user" OnClick="BtnCreateNewUser_Click" />
                </div>
              </div>

                    </div>

                     <div class="form-group row">
                                <asp:GridView ID="GridUsers" SkinID="Member_Grid" runat="server" AutoGenerateColumns="false" Width="100%"
                                     OnRowCommand="GridUsers_RowCommand" PageSize="20" AllowPaging="true" OnPageIndexChanging="GridUsers_PageIndexChanging"
                   OnRowDataBound="GridUsers_RowDataBound" EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>" AllowSorting="true" OnSorting="GridUsers_Sorting">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="65px">
                        <ItemTemplate>
                            <a class="user-img">
                                <img src="../Admin/ImageHandler.ashx?type=user&id=<%#Eval("Uid") %>" class="img-responsive img-circle" alt="user-pic" />
                            </a> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name & Role" SortExpression="Username">
                        <ItemTemplate>
                           <asp:LinkButton ID="btnUsername" runat="server" CommandName="Url" CommandArgument='<%#Eval("Uid")+";"+Eval("SID") %>' Font-Bold="true"
                                                                Text='<%#Bind("Username") %>' CssClass="name"></asp:LinkButton><br />
                           <span><%#Eval("UsernameRole") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" SortExpression="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="CheckUserStatus" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                           <asp:Button SkinID="btnDefault" runat="server" Text="View SP Portal" CommandName="sp" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                           <asp:Button SkinID="btnOrange" runat="server" Text="View" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="action-links">
                        <ItemTemplate>
                             <asp:LinkButton ID="btnEditLink" runat="server" CommandName="Url"
                                                               CommandArgument='<%#Eval("Uid")+";"+Eval("SID") %>' CssClass="edit">
                                <i class="linecons-pencil"></i>Edit Profile
                             </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
                     </div>



            </div>
        </div>
    </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
         <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
           <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
    <script type="text/javascript">
        $(document).ready(function () {
            var Type = GetParameterValues("Type");
            if (Type == 'Administrators')
            { }
            else if (Type == 'Resource')
            { }
            else if (Type == 'CasualLabour')
            { }
            else
            {

            }
        });

        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
    </script>
</asp:Content>