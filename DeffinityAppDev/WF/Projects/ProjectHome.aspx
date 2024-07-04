<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true"
     CodeBehind="ProjectHome.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.ProjectHome" EnableEventValidation="false" %>
<%@ Register Src="~/WF/CustomerAdmin/Controls/ChitChatCtrlHomeNew.ascx" TagPrefix="Pref" TagName="ChitChatCtrlHomeNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/assets/css/fonts/elusive/css/elusive.css" rel="stylesheet" />
	<style>
     .btn{
         margin-bottom:0px;
     }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%--<%= Resources.DeffinityRes.ProjectDashboard%>--%>My Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ajaxToolkit:TextBoxWatermarkExtender ID="TxtWaterProject" runat="server" TargetControlID="txtProjectRef" WatermarkText="Project Reference" />
                  <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ErrorMessage="Please enter project reference"
                                                     ControlToValidate="txtProjectRef" Display="None" ValidationGroup="ProjectGroup"></asp:RequiredFieldValidator>
                  <ajaxToolkit:ValidatorCalloutExtender ID="vcFname" runat="server" TargetControlID="rfvFirstname"></ajaxToolkit:ValidatorCalloutExtender>
         <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="searchBox" WatermarkText="Document Search here" />

                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter document name"
                                                     ControlToValidate="searchBox" Display="None" ValidationGroup="DocGroup"></asp:RequiredFieldValidator>
                  <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"></ajaxToolkit:ValidatorCalloutExtender>

                   <ajaxToolkit:ModalPopupExtender ID="ModalControlExtender1" CancelControlID="lnkClose"
                               BackgroundCssClass="modalBackground" runat="server" TargetControlID="btnSearchDocs"
                               PopupControlID="pnlSearchResults" ClientIDMode="Static"></ajaxToolkit:ModalPopupExtender>
    </div>
    <div class="panel panel-default">
        <div class="panel-body">
                <div class="form-group form-inline">
       <div class="col-md-6">
          <label class="col-sm-2 control-label" style="padding-top:5px;"> <%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-6">
               <asp:UpdatePanel ID="Update1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                   <ContentTemplate>
               <asp:DropDownList ID="ddlPortfolio" runat="server" DataTextField="PortFolio" DataValueField="ID" ClientIDMode="Static"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged1"
                        DataSourceID="SqlDataSourceTitle2"></asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                       </ContentTemplate>
                   <Triggers>
                        
                   </Triggers>
               </asp:UpdatePanel>
              
           </div>
           <div class="col-sm-2" style="padding-right:0px">
                <a runat="server" id="link_CustomerAdmin" href="~/WF/CustomerAdmin/Portfolio.aspx?tab=1" target="_self" class="btn btn-secondary" style="float:right;text-align:right;"
                                        title="<%$ Resources:DeffinityRes,Configurecustomerenvironment%>">Admin</a>
           </div>
            <div class="col-sm-2">
                  <a runat="server" id="link_CustomerPortal" href="javascript:CheckSelection('../Portal/CustomerPortalNav.ashx');" class="btn btn-secondary"
                        target="_self" title="<%$ Resources:DeffinityRes,Viewcustomerportal%>" style="padding-top:5px;">Portal</a>
            </div>
       </div>
       <div class="col-md-6 form-inline ">
             <div class="col-sm-6 form-group form-inline">
                  <asp:TextBox ID="txtProjectRef" runat="server" ClientIDMode="Static" SkinID="txt_80"></asp:TextBox>
                  <asp:LinkButton ID="btnSearchProject" runat="server" ClientIDMode="Static" ValidationGroup="ProjectGroup"
                                             SkinID="BtnLinkSearch" OnClick="btnSearchProject_Click"></asp:LinkButton>
            </div>
           <div class="col-sm-6 form-group form-inline">
               <asp:TextBox ID="searchBox" runat="server" ClientIDMode="Static" SkinID="txt_80"></asp:TextBox>
                 <asp:LinkButton ID="btnSearchDocs" ClientIDMode="Static" runat="server" SkinID="BtnLinkSearch" ValidationGroup="DocGroup"
                                             OnClick="btnSearchDocs_Click"></asp:LinkButton> 
           </div>

              
         
       </div>
 </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-3">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="linecons-calendar"></i>
						</div>
						<div class="xe-label">
							<a style="font-size:large;" href="ProjectPipeline.aspx?Status=2">View My Projects</a>
						</div>
					</div>
				</div>	
        <div class="col-sm-3">
					
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="linecons-desktop"></i>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="../DC/FLSJlist.aspx?type=FLS">View Tickets</a>
						</div>
					</div>
				
				</div>
     	<div class="col-sm-3">
					
					<div class="xe-widget xe-counter xe-counter-info" data-count=".num" data-from="1000" data-to="2470" data-duration="4" data-easing="true">
						<div class="xe-icon">
							<i class="linecons-clock"></i>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="../Resource/TimeSheetResourcesDaily.aspx">Log A Timesheet</a>
						</div>
					</div>
				
				</div>
	    <div class="col-sm-3">
					
					<div class="xe-widget xe-counter xe-counter-red"
                          data-count=".num" data-from="0" data-to="57" data-prefix="-," data-suffix="%" data-duration="5" data-easing="true" data-delay="1">
						<div class="xe-icon">
							<i class="linecons-paper-plane"></i>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="../Resource/VT.ResourceVacationRequest.aspx">Request A Holiday</a>
						</div>
					</div>
				
				</div>
</div>
    <div class="row" runat="server" id="OnlyForPmsandAdmin" >
        <div class="col-sm-3" id="link_customers" runat="server">
					
					<div class="xe-widget xe-counter">
						<div class="xe-icon">
							<i class="linecons-params" style="background-color:#ff794d;"></i>
						</div>
						<div class="xe-label">
						     <a style="font-size:large;" href="../CustomerAdmin/Portfolio.aspx?tab=1">Manage Customers</a>
						</div>
					</div>
					
				</div>
	    <div class="col-sm-3" id="link_timesheets" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-blue">
						<div class="xe-icon">
							<i class="linecons-note" style="background-color:#00b3b3;"></i>
						</div>
						<div class="xe-label">
							<a style="font-size:large;" href="../Admin/Timesheet.aspx">Authorise Timesheets</a>
						</div>
					</div>
				
				</div>
		<div class="col-sm-3" id="link_user" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-info">
						<div class="xe-icon">
							<i class="linecons-user" style="background-color:#33ccff;"></i>
						</div>
						<div class="xe-label">
							<a style="font-size:large;" href="../Admin/AdminUsers.aspx">Create a User</a>
						</div>
					</div>
				
				</div>
		<div class="col-sm-3" id="link_suppliers" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-red"
                          data-count=".num" data-from="0" data-to="57">
						<div class="xe-icon">
							<i class="linecons-truck" style="background-color:#944dff;"></i>
						</div>
						<div class="xe-label">
						  	<a style="font-size:large;" href="../Vendors/RFIVendors.aspx">Manage Suppliers</a>
						</div>
					</div>
				
				</div>
</div>

    <div class="row">
       <div class="col-md-4">
           <div class="panel panel-default">
                            <div class="panel-heading">
                                 Tasks
                             </div>
                            <div class="panel-body">
                              <div id="TaskeGraph" style="width: 100%;"></div>
                       </div>
                   </div>
           </div>
        <div class="col-md-4">
            <div class="panel panel-default">
                     <div class="panel-heading">
                                 Issues
                             </div>
                     <div class="panel-body">
                                <div id="IssuesGraph" style="width:100%;height:300px;"></div>
                            </div>
                    </div>
           </div>
        <div class="col-md-4">
            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Latest Docs<%-- Added To My Projects--%>
                                </div>
                                <div class="panel-body" data-max-height="300">
                                 	
                                      <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImageNew" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                                       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                         <asp:GridView ID="GridDocuments" runat="server" AutoGenerateColumns="false"
                                                DataSourceID="sqlFileList" EmptyDataText="No documents found"
                                                                         ShowFooter="false" Width="100%" OnRowCommand="GridDocuments_RowCommand"
                                                                                           OnRowDataBound="GridDocuments_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlDocumentDownload" runat="server" Text='<%#Bind("Dname") %>' ForeColor="#388787"
                                                                                          CommandName="Download" CommandArgument='<%#Bind("ID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document" Visible="false">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblDocName" runat="server" Text='<%#Bind("Dname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAuthor" runat="server" Text='<%#Bind("U_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUploaded" runat="server" Text='<%#Bind("U_date","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    
                                         <asp:SqlDataSource ID="sqlFileList" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                                                 SelectCommandType="StoredProcedure" SelectCommand="Select_AC2P_Documents">
                                        <SelectParameters>
                                              <asp:SessionParameter DefaultValue="0" Name="CurrentUserID" Type="Int32" SessionField="UID" /> 
                                              <asp:SessionParameter DefaultValue="0" Name="Cid" Type="Int32" SessionField="PortfolioID" /> 
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                   </ContentTemplate>
                                           </asp:UpdatePanel>
                                        
                                </div>
                          </div>
           </div>
        </div>

     <div runat="server" id="ServiceDesk_PerDayCharts" class="panel panel-default">
                <div class="panel-body">
                             <div class="col-sm-3">
                                        <p class="text-medium">The number of open calls of the week at the moment.</p>  
                                        <div id="PerdayCount" class="super-large text-secondary"></div>
                             </div>
                             <div class="col-sm-3">
                                        <div id="reqs-per-Day" style="height: 150px;"></div>
                             </div>
                             <div class="col-sm-6">
                                        <div id="reqs-per-second-chart" style="height: 150px;"></div>
                             </div>
                 </div> 
     </div>

     <div class="row">
       <div class="col-md-12">
           <div class="panel panel-default">
                   <div class="panel-heading">
                       My Projects
                       <div class="panel-options">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                   </div>
                   <div class="panel-body">
                               <asp:UpdateProgress ID="up2" runat="server" AssociatedUpdatePanelID="Update2">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="Update2" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                             <asp:GridView ID="gviewclientprojectstatus" runat="server" AutoGenerateColumns="False"
                                                                           Width="100%" GridLines="None" AllowPaging="true" PageSize="4" ShowFooter="false"
                                                                            EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>"
                                                   OnPageIndexChanging="gviewclientprojectstatus_PageIndexChanging" OnRowCommand="gviewclientprojectstatus_RowCommand">
                            <Columns>
                               <asp:TemplateField HeaderText="Assigned Projects">
                                   <ItemTemplate>
                                      <b> <asp:LinkButton ID="linkbtnProjectRef" Text='<%#Bind("ProjectReference") %>' ForeColor="#006600"
                                                                 CommandArgument='<%#Bind("ProjectReference1") %>' CommandName="Url" runat="server"></asp:LinkButton></b>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Title">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname7" runat="server" Text='<%#Bind("ProjectTitle") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status">
                                   <ItemTemplate>
                                       <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("StatusName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname8" runat="server" Text='<%#Bind("PortfolioName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Site">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname9" runat="server" Text='<%#Bind("SiteName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRef" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname" runat="server" Text='<%#Bind("ProjectReference") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                       </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                   </asp:UpdatePanel>
                    </div>
               </div>
       </div>
   </div>

     <div runat="server" id="ServiceDesk_Calls" class="row">
       <div class="col-md-12">
           <div class="panel panel-default">
                   <div class="panel-heading">
                      Service Desk 
                       <div class="panel-options">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                   </div>
                   <div class="panel-body">
                       
                                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelServices">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImageServices" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                                  <asp:UpdatePanel ID="UpdatePanelServices" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>
                             <asp:GridView ID="GridServices" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="4"
                            Width="100%" 
                            EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>" OnPageIndexChanging="GridServices_PageIndexChanging" OnRowCommand="GridServices_RowCommand">
                               <Columns>
                                <asp:TemplateField HeaderText="Ticket Ref">
                                   <ItemTemplate>
                                      <b>   <asp:LinkButton ID="linkbtnTicketRef" Text='<%#Bind("CallID") %>' ForeColor="#006600"
                                                                               CommandArgument='<%#Bind("CallID1") %>' CommandName="Url" runat="server"></asp:LinkButton></b>
                                       <asp:Label ID="lblPname1" runat="server" Visible="false" Text='<%#Bind("CallID") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Company">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname2" runat="server" Text='<%#Bind("Company") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Details">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname3" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Request">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname4" runat="server" Text='<%#Bind("TypeofRequest") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname5" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Logged Date/Time">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname6" runat="server" Text='<%#Bind("LoggedDateTime") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                                          </ContentTemplate>
                                           <Triggers>                                       
                                           </Triggers>
                                      </asp:UpdatePanel>
                             
                    </div>
               </div>
       </div>
   </div>

     <div class="row">
       <div class="col-md-12">
           <div class="panel panel-default">
                   <div class="panel-heading">
                      Issues
                       <div class="panel-options">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                   </div>
                   <div class="panel-body">
                                  <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelIssues">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImageIssues" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                                  <asp:UpdatePanel ID="UpdatePanelIssues" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>
                            <asp:GridView ID="GridIssues" runat="server" AutoGenerateColumns="False"
                            Width="100%" GridLines="None" AllowPaging="true" PageSize="4" PagerStyle-CssClass="dataTables_paginate paging_simple_numbers"
                            EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>" OnPageIndexChanging="GridIssues_PageIndexChanging">
                               <Columns>
                                <asp:TemplateField HeaderText="Issue Reference">
                                   <ItemTemplate>
                                    <%--   <asp:LinkButton ID="lnkBtn" runat="server" Text='<%#Bind("IssueReference") %>'></asp:LinkButton>--%>
                                      <b> <asp:Label ID="lblPname10" runat="server" ForeColor="#006600" Text='<%#Bind("IssueReference") %>'></asp:Label></b>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname11" runat="server" Text='<%#Bind("Issue") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Raised">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname12" runat="server" Text='<%#Bind("DateRaised") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname13" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname14" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Assign To">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname15" runat="server" Text='<%#Bind("AssignTo") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                            
                    </div>
               </div>
       </div>
   </div>
      <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage11" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
                                               <div class="row">
                            <div class="col-md-12">
    <section class="profile-env">
				<div class="form-group" style="padding-bottom:0px">
                     <div class="panel panel-default" style="width:100%">
                          <div class="panel-heading">
                                 Chit chat
                          </div>
                          <div class="panel-body" style="padding-bottom:0px">
                                      <div class="col-sm-12" style="padding-bottom:0px;padding-left:0px">
                                                   <div id="DivChitchatPost" style="width:74%;"></div>
                                      </div>
                              </div>
                         </div>

                    <div class="scrollable ps-container ps-active-y" data-max-height="650" style="max-height:650px;">
                            <div class="col-sm-12" style="padding-left:0px;">
                        <section class="user-timeline-stories">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
                    </div>

				</div>
        </section>
           <%--<Pref:ChitChatCtrlHomeNew runat="server" id="ChitChatCtrlHomeNew" />--%>
       </div>
                    </div>
                                     </ContentTemplate>
                                   <Triggers>
                                      
                                   </Triggers>
                               </asp:UpdatePanel>
        
    

     <div class="row" style="display:none;">
       <div class="col-md-12">
           <div class="panel panel-default">
                     <div class="panel-heading">
                       <div class="panel-options">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                   </div>

                   <div class="panel-body">
                             <div class="form-group">
                    <div class="col-md-7">
                          <div class="xe-widget xe-weather">
						<div class="xe-background xe-background-animated">
							<%--<img src="clouds.png" />--%>
						</div>
						
						<div class="xe-current-day">
							<div class="xe-now">
								<div class="xe-temperature">
									<div class="xe-icon">
										<i class="meteocons-cloud-moon"></i>
									</div>
									<div class="xe-label">
										Now
										<strong>21&deg;</strong>
									</div>
								</div>
								<div class="xe-location">
									<h4>London, UK</h4>
									<time>Today, 11 October</time>
								</div>
							</div>
							
							<div class="xe-forecast">
								<ul>
									<li>
										<div class="xe-forecast-entry">
											<time>11:00</time>
											<div class="xe-icon">
												<i class="meteocons-sunrise"></i>
											</div>
											<strong class="xe-temp">12&deg;</strong>
										</div>
									</li>
									<li>
										<div class="xe-forecast-entry">
											<time>12:00</time>
											<div class="xe-icon">
												<i class="meteocons-clouds-flash"></i>
											</div>
											<strong class="xe-temp">13&deg;</strong>
										</div>
									</li>
									<li>
										<div class="xe-forecast-entry">
											<time>13:00</time>
											<div class="xe-icon">
												<i class="meteocons-cloud-moon-inv"></i>
											</div>
											<strong class="xe-temp">16&deg;</strong>
										</div>
									</li>
									<li>
										<div class="xe-forecast-entry">
											<time>14:00</time>
											<div class="xe-icon">
												<i class="meteocons-eclipse"></i>
											</div>
											<strong class="xe-temp">19&deg;</strong>
										</div>
									</li>
									<li>
										<div class="xe-forecast-entry">
											<time>15:00</time>
											<div class="xe-icon">
												<i class="meteocons-rain"></i>
											</div>
											<strong class="xe-temp">21&deg;</strong>
										</div>
									</li>
									<li>
										<div class="xe-forecast-entry">
											<time>16:00</time>
											<div class="xe-icon">
												<i class="meteocons-cloud-sun"></i>
											</div>
											<strong class="xe-temp">25&deg;</strong>
										</div>
									</li>
								</ul>
							</div>
						</div>
						
						<div class="xe-weekdays">
							<ul class="list-unstyled">
								<li>
									<div class="xe-weekday-forecast">
										<div class="xe-temp">21&deg;</div>
										<div class="xe-day">Monday</div>
										<div class="xe-icon">
											<i class="meteocons-windy-inv"></i>
										</div>
									</div>
								</li>
								<li>
									<div class="xe-weekday-forecast">
										<div class="xe-temp">23&deg;</div>
										<div class="xe-day">Tuesday</div>
										<div class="xe-icon">
											<i class="meteocons-sun"></i>
										</div>
									</div>
								</li>
								<li>
									<div class="xe-weekday-forecast">
										<div class="xe-temp">19&deg;</div>
										<div class="xe-day">Wednesday</div>
										<div class="xe-icon">
											<i class="meteocons-na"></i>
										</div>
									</div>
								</li>
								<li>
									<div class="xe-weekday-forecast">
										<div class="xe-temp">18&deg;</div>
										<div class="xe-day">Thursday</div>
										<div class="xe-icon">
											<i class="meteocons-windy"></i>
										</div>
									</div>
								</li>
								<li>
									<div class="xe-weekday-forecast">
										<div class="xe-temp">20&deg;</div>
										<div class="xe-day">Friday</div>
										<div class="xe-icon">
											<i class="meteocons-sun"></i>
										</div>
									</div>
								</li>
							</ul>
						</div>
						
					</div>
                      </div>
               </div>
                   </div>
               </div>
       </div>
   </div>

    
    <asp:Panel ID="pnlSearchResults" runat="server" Style="display: none; width: 800px;
                     height: 400px" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" ClientIDMode="Static"
                     ScrollBars="Auto">
        <div class="panel panel-default">
            <div class="panel-heading">
                Document Search 
            </div>
            <div class="panel-body">
                <div class="form-group">
                   <div class="col-md-12">
                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Search%></label>
                       <div class="col-sm-6">
                           <input type="text" id="txtSearchBox" style="width:100%;" />
                       </div>
                       <div class="col-sm-4">
                           <asp:LinkButton SkinID="BtnLinkSearch" ID="btnSearch" runat="server" ClientIDMode="Static"></asp:LinkButton>
                           <asp:LinkButton ID="lnkClose" runat="server" ClientIDMode="Static" SkinID="BtnLinkCancel"></asp:LinkButton>
                       </div>
                   </div>
                  <div class="col-md-4">

                  </div>
                </div>
                <div class="form-group">
                     <div id="searchResults" style="padding-bottom: 20px;font-size:8pt;"></div>
                </div>
            </div>
        </div>        
    </asp:Panel>

 

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
      <%: System.Web.Optimization.Styles.Render("~/bundles/chitchatcss") %>
      <%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>
    <script>
       // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChitChatBind(10));
    </script>
    <script>
       function CheckSelection(linkname) {
            var frm = document.forms[0]
            var checked = 0;
            var second = 0;
            var ddl = document.getElementById("<%=ddlPortfolio.ClientID %>");
            if (ddl.options[ddl.selectedIndex].value == "0") {
                alert("Please select a customer");
                return;
            }
            else {
                window.location = linkname;
            }
        }
       
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Postback1);
        function Postback1() {
            $('#ddlPortfolio').change(function () {
                var Cid = $('#ddlPortfolio').val();
                var text = $("#ddlPortfolio option:selected").text();
                CustomerChange(Cid, text);
                GetTasksData();
                GetIssuesData();
                OpenCallforToday();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChitChatBind(10));
            });
        };

        var xenonPalette = ['red', '#FFC200', 'green'];
        $(document).ready(function () {
            $("#<%=btnSearchProject.ClientID%>").click(function () {
                //debugger;
                if ($.trim($("#txtProjectRef").val()).length > 0) {
                    var pref = $("#txtProjectRef").val();
                    $.ajax({
                        type: "POST",
                        url: "ProjectHome.aspx/GetProject",
                        data: "{projectRef:'" + pref + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == true)
                                window.location.href = "ProjectOverviewV4.aspx?project=" + pref;
                            else
                                alert('Please enter a valid project reference number.');
                        }
                    });
                }
                return false;
            });
        
            GetTasksData();
            GetIssuesData();

            OpenCallforToday();
            reqsperDayChartBind();
            $('#ddlPortfolio').change(function () {
                var Cid = $('#ddlPortfolio').val();
                var text = $("#ddlPortfolio option:selected").text();
                CustomerChange(Cid, text);
                GetTasksData();
                GetIssuesData();
                OpenCallforToday();
            });

            function searchMethod() {
                var SearchText = $.trim($("#txtSearchBox").val());
                var queryString = $.param({ searchString: SearchText });
                if ($.trim($("#txtSearchBox").val()).length > 0) {
                    var url = "FileSearchComponent.aspx?" + queryString;
                    $.get(url, function (data) {
                        $("#searchResults").html("");
                        $("#searchResults").append(data);
                    });
                }
                else
                    $("#searchResults").text("Please enter the keywords to search");
            }
            $("#btnSearchDocs").click(function () {
                var t = $("#searchBox").val();
                //debugger;
                if (t != "Docs") {
                    $("#txtSearchBox").val($("#searchBox").val());
                    searchMethod();
                }
                else {
                    alert("Please enter document name");
                    $("#pnlSearchResults").hide();
                }
            });
            $("#btnSearch").click(function () {
                //debugger;
                $("#pnlSearchResults").show();
                searchMethod();
                return false;
            });
        });

        function GetTasksData() {
            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetTasksData',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    //debugger;
                    $('#TaskeGraph').dxBarGauge({
                        startValue: -30,
                        endValue: 30,
                        baseValue: 0,
                        values: data.d, //[-21.3, 14.8, -30.9, 45.2],
                        label: {
                            customizeText: function (arg) {
                                if (arg.valueText > 0) {
                                    return (arg.valueText.substring(0, arg.valueText.length - 3) + '');
                                }
                                else {
                                    return -(arg.valueText.substring(0, arg.valueText.length - 3) + '');
                                }
                            }
                        },
                        tooltip: {
                            enabled: true,
                            customizeTooltip: function (arg) {
                                return {
                                    text: ''
                                };
                            }
                        },
                        palette: xenonPalette,
                        title: {
                            text: '',
                            font: {
                                size: 18
                            }
                        }
                    });

                    //add alert 
                    svgAlert();
                }
               
            });
        }
        function GetIssuesData() {
            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetIssuesData',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                Name: newData[i].Name,
                                Value: newData[i].Value
                            });
                        }
                        $("#IssuesGraph").dxChart({
                            dataSource: ds,
                            series: {
                                argumentField: "Name",
                                valueField: "Value",
                                name: "Category",
                                type: "bar",
                                color: '#68b828'
                            },
                            valueAxis: {
                                tickInterval: 1
                            },
                            tooltip: {
                                enabled: true
                            },
                            legend: {
                                visible: false
                            }
                        });
                    }
                }
            });
        }
        function CustomerChange(Cid, text) {
            $.ajax({
                url: "../DC/webservices/DCServices.asmx/AddSessionValues",
                type: "POST",
                data: "{'Cid': '" + Cid + "','text':'" + text + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    var obj = jQuery.parseJSON(data.d);
                },
                error: function (msg) {
                    var e = msg;
                }
            });
        }

        function reqsperDayBind(EndValue, setvalue) {
            var gaugesPalette = ['#8dc63f', '#40bbea', '#ffba00', '#cc3f44'];

            var tickIntervalValue = (EndValue / 4);

            $('#reqs-per-Day').dxCircularGauge({
                scale: {
                    startValue: 0,
                    endValue: EndValue,
                    majorTick: {
                        tickInterval: tickIntervalValue
                    }
                },
                rangeContainer: {
                    palette: 'pastel',
                    width: 5,
                    ranges: [
                        {
                            startValue: 0,
                            endValue: tickIntervalValue,
                            color: gaugesPalette[0]
                        }, {
                            startValue: tickIntervalValue,
                            endValue: tickIntervalValue * 2,
                            color: gaugesPalette[1]
                        }, {
                            startValue: tickIntervalValue * 2,
                            endValue: tickIntervalValue * 3,
                            color: gaugesPalette[2]
                        }, {
                            startValue: tickIntervalValue * 3,
                            endValue: tickIntervalValue * 4,
                            color: gaugesPalette[3]
                        }
                    ],
                },
                value: setvalue,
                valueIndicator: {
                    offset: 10,
                    color: '#2c2e2f',
                    spindleSize: 12
                }
            });
        }
        function reqsperDayChartBind() {
            var reqs_per_second_data = [];
            $.ajax({
                url: '../DC/webservices/DCServices.asmx/TodayOpenCallWithTime',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var obj = jQuery.parseJSON(data.d);
                        //debugger;
                        $.map(obj, function (arg, i) {
                            reqs_per_second_data.push({
                                time: new Date(arg.d_Time),
                                reqs: arg.Calls_Count
                            });
                        });
                        BindGraph(reqs_per_second_data);
                    }
                },
                error: function (msg) {
                    var e = msg;
                }
            });
        }
        function BindGraph(reqs_per_second_data) {
            $("#reqs-per-second-chart").dxChart({
                dataSource: reqs_per_second_data,
                commonPaneSettings: {
                    border: {
                        visible: true,
                        color: '#f5f5f5'
                    }
                },
                commonSeriesSettings: {
                    type: "area",
                    argumentField: "time",
                    border: {
                        color: '#68b828',
                        width: 1,
                        visible: true
                    }
                },
                series: [
                    { valueField: "reqs", name: "Reqs per Day", color: '#68b828', opacity: .5 },
                ],
                commonAxisSettings: {
                    label: {
                        visible: true
                    },
                    grid: {
                        visible: true,
                        color: '#f5f5f5'
                    }
                },
                argumentAxis: {
                    valueMarginsEnabled: false,
                    label: {
                        customizeText: function (arg) {
                            return date('h:i A', arg.value);
                        }
                    },
                },
                legend: {
                    visible: false
                }
            });
        }
        function OpenCallforToday() {
            $.ajax({
                url: '../DC/webservices/DCServices.asmx/OpenCallforToday',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != "[]") {
                        var obj = jQuery.parseJSON(data.d);
                        $("#PerdayCount").html("");
                    
                        var EndValue = obj[0].TotalCount;
                        var setvalue = obj[0].PerdayCount;
                        $("#PerdayCount").append(setvalue);
                        reqsperDayBind(EndValue, setvalue);
                    }
                    else {
                        $("#PerdayCount").html("");
                        $("#PerdayCount").append(0);
                        //var EndValue = 0;
                        //var setvalue = obj[0].PerdayCount;
                        //reqsperDayBind(EndValue, setvalue);
                    }
                },
                error: function (msg) {
                    var e = msg;
                }
            });
        }
        </script>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
      <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);

    GridResponsiveCss();

    function svgAlert()
    {
        $("#TaskeGraph svg").find('g.dxg-tracker').click(function () {
            debugger;
            //alert('ttt');
            //alert($(this).attr('d'));
            var NWin = window.open('/WF/Projects/ProjectTasks.aspx', '', 'height=600,width=1200');
            if (window.focus) {
                NWin.focus();
            }
            return false;

        });
        //$('path').click(function () {

        //    alert('ttt');
        //});

    }
    //$(window).load(function () {
      
    //    $('#TaskeGraph svg').click(function () {

    //        alert('ttt');
    //    });
    //    $('path').click(function () {

    //        alert('ttt');
    //    });
    //});
   
 </script> 
</asp:Content>
