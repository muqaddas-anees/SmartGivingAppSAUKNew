<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatNew.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.chat.ChatNew" %>

<%@ Register Src="~/WF/Controls/SideMenu.ascx" TagName="SideMenu" TagPrefix="uc1" %>

<%@ Register Src="~/WF/Controls/TopNavigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Controls/PageTitle_BreadCrumb.ascx" TagName="Pagetitle_breadcrumb" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Controls/ResourceSideMenu.ascx" TagPrefix="uc2" TagName="ResourceSideMenu" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <title></title>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	
    <%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <link href="../../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
<link href="../../../Content/css/jquery.ui.theme.css" rel="stylesheet" />
	<script src='<%:ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
<script src="../../../Scripts/ui/jquery.ui.core.js"></script>
     <script src="../../../Scripts/ui/jquery.ui.widget.js"></script>    
    <script src="../../../Scripts/ui/jquery.ui.mouse.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.dialog.js"></script>
     <script src="../../../Scripts/ui/jquery.ui.resizable.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.draggable.js"></script>    
   
   
    <%--signalr--%>
    <script src="../../../Scripts/jquery.signalR-2.2.1.js"></script>
        <script src="/SignalR/hubs" type="text/javascript"></script>

</head>
<body class="page-body">
    <div class="page-loading-overlay">
		<div class="loader-2"></div>
	</div>
    <form id="form1" runat="server">
        <script type="text/javascript">
            function load() {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            UserSkinName();

        });
        function UserSkinName() {
            $.ajax({
                url: '<%:ResolveClientUrl("~/WF/Projects/ProjectHome.aspx/GetUserSkinName") %>',
                type: "POST",
                data: "",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    debugger;
                    if (data.d != '') {
                        debugger;
                        $("body").removeClass().addClass(data.d);
                    }
                },
                error: function (msg) {
                    var e = msg;
                }
            });
        }
    </script>


   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true" EnableCdn="true" >
       <Scripts>
           <%--<asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
       </Scripts>
   </asp:ScriptManager>
        <div class="settings-pane">
			
		<a href="#" data-toggle="settings-pane" data-animate="true" id="btnSettings" runat="server">
			&times;
		</a>
		
		<div class="settings-pane-inner" id="pnlSettings" runat="server">
			
			<div class="row">
				
				<div class="col-md-3">
					
					<div class="user-info">
						
						<div class="user-image">
							<a  id="link_editprofile1" runat="server">
								<img class="img-responsive img-circle" id="img_user" runat="server" />
							</a>
						</div>
						
						<div class="user-details">
							<h3>
								
							<div class="user-links">
								<a class="btn btn-primary" id="link_editprofile" runat="server">Edit Profile</a>
							
							</div>
						</div>
					</div>
				</div>
				
				<div class="col-md-9 link-blocks-env vertical-top" style="display:none;">
					
					<div class="links-block left-sep">
						<h4>
							<a href="#">
								<span><%= Resources.DeffinityRes.Security%></span>
							</a>
						</h4>
						
						<ul class="list-unstyled">
							<li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/SystemDefaults.aspx") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.SystemSetup%>
								</a>
							</li>
							<li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/UserManagement.aspx?Type=Administrators") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.UserAdmin%>
								</a>
							</li>
							<li style="display:none;">
								<a href='<%:ResolveClientUrl("~/WF/Admin/UserManagement.aspx?sid=10&Type=CasualLabour") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.CasualLabour%>
								</a>
							</li>
							<li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/ManageTeamMembersNew.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.GroupsandPermissions%>
								</a>
							</li>
                            <li>
                                <a href='<%:ResolveClientUrl("~/WF/Admin/FMRatecard.aspx") %>'>
                                    <i class="fa-angle-right"></i>
                                 <%= Resources.DeffinityRes.FinanceRatecard%>
                                </a>
                            </li>
                            <li>
                                <a href='<%:ResolveClientUrl("~/WF/Admin/PasswordExpiry.aspx") %>'>
                                    <i class="fa-angle-right"></i>
                                  <%= Resources.DeffinityRes.PasswordExpiry%>  
                                </a>
                            </li>
                           
						</ul>
					</div>
                    
					<div class="links-block left-sep" style="display:none;">
						<h4>
							<a href="#">
								<span>System Configuration</span>
							</a>
						</h4>
						
						<ul class="list-unstyled">
							<li>
								<a href='<%:ResolveClientUrl("~/WF/CustomerAdmin/Portfolio.aspx?tab=1") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.Customers%>
								</a>
							</li>
							
							<li id="LinkProgrammeManagement" runat="server">
								<a href='<%:ResolveClientUrl("~/WF/Admin/ProgrammeManagement.aspx") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.ProgrammeManagement%>
								</a>
							</li>
							<li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/adminmasterlists.aspx") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.Checklists%>
								</a>
							</li>
                            <li id="LinkVacationTrackerAdmin" runat="server">
                                <a href='<%:ResolveClientUrl("~/WF/Admin/VTAdmin.aspx") %>'>
                                    <i class="fa-angle-right"></i>
                                 <%= Resources.DeffinityRes.VacationTrackerAdmin%> 
                                </a>
                            </li>
                            <li id="LinkVendorManagement" runat="server">
                                <a href='<%:ResolveClientUrl("~/WF/Vendors/RFIVendors.aspx") %>'>
                                    <i class="fa-angle-right"></i>
                                <%= Resources.DeffinityRes.VendorManagement%> 
                                </a>
                            </li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/Admin_UsersJournal.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.Journals%>
								</a>
							</li>
							<li>
								<a href='<%:ResolveClientUrl("~/WF/Admin/AdminDropDown.aspx?Panel=0") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.AdminDropdownLists%>
                                    
								</a>
							</li>
                           
						</ul>
					</div>
                    <div class="links-block left-sep" id="DivTotalFinanceSection" runat="server" >
						<h4>
							<a href="#">
								<span><%= Resources.DeffinityRes.FinanceSection%></span>
							</a>
						</h4>
						
						<ul class="list-unstyled">
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMResources.aspx") %>'>
									<i class="fa-angle-right"></i>
								 <%= Resources.DeffinityRes.TimesheetView%>	
								</a>
							</li>
                             <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMProjects.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.Projects%>
								</a>
							</li>
                             <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMInvoicing.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.Invoicing%>	
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/POJournal.aspx") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.PODatabase%>		
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/BoMSupplierPayments.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.SupplierInvoices%>	
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMWorkInProgress.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.WorkInProgress%>		
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMSalesForeCast.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.RevenueForecast%>		
								</a>
							</li>
                            <li style="display:none;">
								<a href='<%:ResolveClientUrl("~/WF/Projects/KPIFinancial.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.KPI%>
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/ExportofProjectOverviewdata.aspx") %>'>
									<i class="fa-angle-right"></i>
								<%= Resources.DeffinityRes.ExportData%>	
								</a>
							</li>
                            <li>
								<a href='<%:ResolveClientUrl("~/WF/Projects/FMQuotes.aspx") %>'>
									<i class="fa-angle-right"></i>
									<%= Resources.DeffinityRes.Quotes%>
								</a>
							</li>
                            </ul>
                        </div>
				</div>
			</div>
		
		</div>
		
	</div>
	<div class="page-container"><!-- add class "sidebar-collapsed" to close sidebar by default, "chat-visible" to make chat appear always -->
			
		<uc1:SideMenu ID="ctrl_sidemenu" runat="server" />
		<%--<uc2:ResourceSideMenu runat="server" id="ResourceSideMenu"  />--%>
       
     
		<div class="main-content">
					
			<!-- User Info, Notifications and Menu Bar -->
			<uc1:Navigation ID="ctrl_Navigation" runat="server" />
			<div class="page-title">
				<div class="title-env">
					<h1 class="title"</h1>
					<p class="description">
                       
					</p>

				</div>
					<div class="breadcrumb-env">
							
								 <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server"  />
                    <i class="fa-home"></i><asp:SiteMapPath ID="SiteMap1" SiteMapProvider="AdminSiteMap" runat="server" 
                         CurrentNodeStyle-Font-Bold="true" CssClass="breadcrumb bc-1" 
                        PathSeparator="/" CurrentNodeStyle-CssClass="breadcrumb bc-1 active" 
                        NodeStyle-CssClass="breadcrumb bc-1" RootNodeStyle-CssClass="breadcrumb bc-1" >
                    </asp:SiteMapPath>
				</div>
					
			</div>
			
             <!--Main Content-->
 
        <div id="dataContainer">
            <div id="divLogin" class="login">
                <div>
                    <input id="txtNickName" type="text" class="textBox" value="<%= Session["uname"]%>" />
                    <input id="txtID" type="text" class="textBox" value="<%= Session["userid"]%>" />
                </div>
            </div>

            <div id="divChat" class="chatRoom">
                <div  class="chat-group">
                             <%--   <div id="divusers" class="chat-group"></div>--%>

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="id">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("Uid") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Uid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%#Eval("Username")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("Username")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="mailid">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%#Eval("Email")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="phno">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("SID")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("SID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="chat session"  >
                                <ItemTemplate>
    
                                    <a href="#" id='<%# Eval("Uid") %>'  onclick="getid(this)" name="<%#Eval("Username")%>"> <span class="user-status is-online" ></span><em>Chat</em></a>
                                </ItemTemplate>
                                <ItemStyle ForeColor="#00CC00" />
                            </asp:TemplateField>
                        </Columns>
                        
                                </asp:GridView>
   
                      

                
                    </div>
                <%--class="users"--%>
            </div>
            <%-- to maintain the login id & username--%>
            <input id="hdId1" type="hidden" />
            <input id="hduId1" type="hidden" />
            <input id="hdUserName1" type="hidden" />
        </div>
			<!--footer-->
            <uc1:Footer ID="ctrl_footer" runat="server" />
		</div>
	</div>
      

        <script src="../../../Scripts/ChatScript.js"></script>
        <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
   <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
        <%: System.Web.Optimization.Styles.Render("~/bundles/less") %>
        <style type="text/css">
            .li12{
                margin-top: 0px;
                margin-bottom: 0px;
                border: 0;
                border-top: 1px solid #eee;
            }

            .draggable {
                position: absolute;
                border: #7c807f solid 1px !important;
                width: 250px;
            }

                .draggable .header {
                    cursor: move;
                    background-color: #7c807f;
                    border-bottom: #7c807f solid 1px;
                    color: #7c807f;
                    height: 25px;
                }

                .draggable .selText {
                    color: white;
                    padding: 1em;
                    font-size: medium;
                    font-family: cursive;
                }

                .draggable .messageArea {
                    /*width: 250px;*/
                    overflow-y: scroll;
                    height: 230px;
                    border-bottom: #999C99 solid 1px;
                    background-color: white;
                }

                    .draggable .messageArea .message {
                        padding: 4px;
                    }

                .draggable .buttonBar {
                    width: 250px;
                    padding: 4px;
                }

                    .draggable .buttonBar .msgText {
                        width: 180px;
                    }

                    .draggable .buttonBar .button {
                        margin-left: 4px;
                        width: 55px;
                    }
        </style>
    

         <div style="display:none;visibility:hidden">
<iframe id="frm_setpage" name="frm_setpage" runat="server" width="100%" frameborder="0" src="~/WF/SessionKeepAlive.aspx" scrolling="no" style="display:none;visibility:hidden" ></iframe>
</div>
   
    </form>
    
</body>
</html>