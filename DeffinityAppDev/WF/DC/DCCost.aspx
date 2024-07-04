<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCCost.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCCost" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
      <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
       <style>
       .header_right{
           text-align:right;
           
       }

   </style>
     <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css?id=100")%>' rel="stylesheet" type="text/css" />
    <script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
    <div class="row mb-6">
        

    </div>
    
<div class="form-group row mb-6">
          
              <div class="col-md-12 d-flex d-inline">

                  <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                   <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                  </div>
    </div>
    <div class="form-group well" style="display:none;visibility:hidden;">
      <div class="col-md-12 d-flex d-inline">
           <label class="col-sm-2 control-label">Week Starting Date (Monday):</label>
           <div class="col-sm-8 form-inline d-flex d-inline"> <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" />
               <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" ValidationGroup="Group1"
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />

            </div>
	</div>
        <div class="col-md-4 form-inline d-flex d-inline">
        <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                        Display="None" ValidationGroup="Group1" ControlToValidate="txtweekcommencedate"></asp:RequiredFieldValidator>
         <asp:Button ID="imgSubmit" runat="server" SkinID="btnDefault" ToolTip="<%$ Resources:DeffinityRes,SendforApproval%>"
                                       Text="<%$ Resources:DeffinityRes,SendforApproval%>" style="display:none;visibility:hidden;"/>
	</div>
	<div class="col-md-4" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%> :</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlStatusupdate" Width="150px" runat="server">
                                        <asp:ListItem Value="0" Text="ALL">ALL</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="2" Text="Submitted for Approval">Submitted for Approval</asp:ListItem>
                                        <asp:ListItem Value="4" Text="Approved">Approved</asp:ListItem>
                                        <asp:ListItem Value="3" Text="Declined">Declined</asp:ListItem>
                                    </asp:DropDownList>
            </div>
	</div>
	
</div>


     <ajaxToolkit:ModalPopupExtender ID="mdlAddTimesheet" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddTimesheet" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>

    <asp:Panel ID="pnlAddTimesheet" runat="server" BackColor="White" Style="display:none;"
                       Width="750px" Height="570px" CssClass="card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Timesheet"></asp:Label> <asp:HiddenField ID="hid" runat="server" Value="0" />  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">

 <div class="form-group row mb-6">
      <div class="col-md-12 d-flex d-inline">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Date%> :</label>
           <div class="col-sm-9 form-inline d-flex d-inline">
                <asp:TextBox ID="txtDate" runat="server" SkinID="DateNew"></asp:TextBox>
                            <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"   runat="server"
                                PopupButtonID="imgbtnenddate7" TargetControlID="txtDate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                Display="None" ValidationGroup="c1" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
            </div>
	</div>
	
</div>

    
 <div class="form-group row mb-6">
      <div class="col-md-12 d-flex d-inline">
           <label class="col-sm-2 control-label"><%:sessionKeys.JobsDisplayName %> </label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlJobs" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredProjectTile" runat="server" ErrorMessage="Please select job"
                                ControlToValidate="ddlJobs" Display="None" ValidationGroup="c1" InitialValue="0"></asp:RequiredFieldValidator>
                           
            </div>
	</div>
	
</div>
              <div class="form-group row mb-6">
      <div class="col-md-12 d-flex d-inline">
           <label class="col-sm-2 control-label">Team Member</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSmartTech" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select smart tech"
                                ControlToValidate="ddlSmartTech" Display="None" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>--%>
                           
            </div>
	</div>
	
</div>

   <div class="form-group row mb-6" >
      <div class="col-md-12 d-flex d-inline" >
           <label class="col-sm-2 control-label">From Time</label>
           <div class="col-sm-6 form-inline d-flex d-inline">
               <asp:TextBox ID="txtFromTime" runat="server" MaxLength="5" Text="" SkinID="Time"></asp:TextBox><span>(hh:mm)</span>
               <asp:RegularExpressionValidator ID="rvFromTime" runat="server" ControlToValidate="txtFromTime"
                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="c1"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"></asp:RegularExpressionValidator> 
               <asp:Button ID="btnStart" runat="server" SkinID="btnDefault" ValidationGroup="c1" ToolTip="<%$ Resources:DeffinityRes,Addanewentry%>"
                                OnClick="imgBtnAdd_Click" Text="Start" Visible="false" />
               </div>
          </div>
        </div>
          <div class="form-group row mb-6">
      <div class="col-md-12 d-flex d-inline" >
    <label class="col-sm-2 control-label">To Time</label>
           <div class="col-sm-6 form-inline d-flex d-inline">
               <asp:TextBox ID="txtToTime" runat="server" MaxLength="5" Text="" SkinID="Time"></asp:TextBox><span>(hh:mm)</span> 
                <asp:RegularExpressionValidator ID="revToTime" runat="server" ControlToValidate="txtToTime"
                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="c1"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"></asp:RegularExpressionValidator>
               </div>
          </div>
        </div>
            <div class="form-group row mb-6">
      <div class="col-md-12 d-flex d-inline">
           <label class="col-sm-2 control-label"></label>
           <div class="col-sm-6">
               <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnSubmit" ValidationGroup="c1" ToolTip="<%$ Resources:DeffinityRes,Addanewentry%>"
                                OnClick="imgBtnAdd_Click" />
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
	
</div>

    

 
        </div>
        
    </asp:Panel>
    <div class="form-group row mb-6"  style="display:none;visibility:hidden;">
              <div class="col-md-12 d-flex justify-content-end">
                  <asp:Button ID="btnAddTimesheet" runat="server" SkinID="btnDefault" Text="Add Timesheet" style="text-align:right;float:right;" OnClick="btnAddTimesheet_Click"  />
                  </div>
        </div>
    <asp:HiddenField ID="htimeid" runat="server" />
    <asp:GridView ID="grdTimeSheetEntry" runat="server" Width="100%" AutoGenerateColumns="False"
                    EmptyDataText="No Timesheet(s) available" OnRowDataBound="grdTimeSheetEntry_RowDataBound"
                    OnRowCommand="grdTimeSheetEntry_RowCommand" Visible="false">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="form-inline d-flex d-inline">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                 <div style="width:40px;padding-bottom:5px">       <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="edit1"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>" 
                                            Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>'>
                                        </asp:LinkButton> </div>
                                <asp:Button ID="btnStop" runat="server"  Text="Stop Timer" CommandArgument='<%# Bind("ID")%>' SkinID="btnDefault" CommandName="stopped" />
                                <asp:Button ID="btnResumit" runat="server" Visible='<%# GetTimeSheetStatusDeclineCheck(Eval("TimesheetstatusID").ToString())%>' Text="Resubmit" CommandArgument='<%# Bind("ID")%>' SkinID="btnDefault" CommandName="resubmit" />
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>" ItemStyle-CssClass="form-inline">
                           
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Job" ItemStyle-CssClass="col-nowrap">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="45%" />
                            <ItemTemplate>
                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Smart tech" ItemStyle-CssClass="col-nowrap">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>" ItemStyle-CssClass="col-nowrap" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            
                            <ItemTemplate>
                                <asp:Label ID="lblEntry" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="From Time (HH:MM)" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridStartTime" runat="server" ></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText=" To Time (HH:MM)" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridEndTime" runat="server"></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblStaus_Time"  runat="server" Text='<%# TimesheetStatus(Eval("TimesheetstatusID").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="del" 
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"
                                    Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

    <div class="row mb-6">
        <div class="col-md-4">

        
            </div>

        <div class="col-md-4">

            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
										<!--begin::Header-->
										<div class="card-header pt-5">
											<!--begin::Title-->
											<div class="card-title d-flex flex-column">
												<!--begin::Info-->
												<div class="d-flex align-items-center">
													<!--begin::Currency-->
													<span class="fs-4 fw-semibold text-gray-400 me-1 align-self-start"></span>
													<!--end::Currency-->
													<!--begin::Amount-->
													<span class="fs-2hx fw-bold text-dark me-2 lh-1 ls-n2"><asp:Literal ID="lblTarget" runat="server"></asp:Literal>  </span>
													<!--end::Amount-->
													<!--begin::Badge-->
													<%--<span class="badge badge-light-success fs-base">--%>
													<!--begin::Svg Icon | path: icons/duotune/arrows/arr066.svg-->
													<%--<span class="svg-icon svg-icon-5 svg-icon-success ms-n1">
														<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
															<rect opacity="0.5" x="13" y="6" width="13" height="2" rx="1" transform="rotate(90 13 6)" fill="currentColor" />
															<path d="M12.5657 8.56569L16.75 12.75C17.1642 13.1642 17.8358 13.1642 18.25 12.75C18.6642 12.3358 18.6642 11.6642 18.25 11.25L12.7071 5.70711C12.3166 5.31658 11.6834 5.31658 11.2929 5.70711L5.75 11.25C5.33579 11.6642 5.33579 12.3358 5.75 12.75C6.16421 13.1642 6.83579 13.1642 7.25 12.75L11.4343 8.56569C11.7467 8.25327 12.2533 8.25327 12.5657 8.56569Z" fill="currentColor" />
														</svg>
													</span>
													<!--end::Svg Icon-->2.2%</span>--%>
													<!--end::Badge-->
												</div>
												<!--end::Info-->
												<!--begin::Subtitle-->
												<span class="text-gray-400 pt-1 fw-semibold fs-6">Total Donations</span>
												<!--end::Subtitle-->
											</div>
											<!--end::Title-->
										</div>
										<!--end::Header-->
										<!--begin::Card body-->
										<div class="card-body pt-2 pb-4 d-flex flex-wrap align-items-center">
											<!--begin::Chart-->
											<div class="d-flex flex-center me-5 pt-2">
												<%--<div id="kt_card_widget_17_chart" style="min-width: 70px; min-height: 70px" data-kt-size="70" data-kt-line="11"></div>--%>
                                                 <canvas id="project_overview_chart" style="height:70px"></canvas>
											</div>
											<!--end::Chart-->
											<!--begin::Labels-->
											<div class="d-flex flex-column content-justify-center flex-row-fluid">
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 bg-success me-3"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Total Cost</div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblTotalCost" runat="server" Text="0.00"></asp:Literal> </div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center my-3">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 bg-primary me-3"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Donations collected </div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblRaised" runat="server" Text="0.00"></asp:Literal></div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 me-3" style="background-color: #E4E6EF"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Remaining</div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblRemainig" runat="server" Text="0.00"></asp:Literal></div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
											</div>
											<!--end::Labels-->
										</div>
										<!--end::Card body-->
									</div>
        </div>
    </div>

    <div class="form-group row mb-6">
          <div class="col-md-12 d-flex justify-content-end">
        <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault" Text="Add Cost" style="float:right;" OnClick="btnAdd_Click" />
              </div>
        </div>
    <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridPartner_RowCommand" EmptyDataText="No cost data available"   >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Height="100px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_right" ItemStyle-CssClass="header_right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Details" ItemStyle-Width="30%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Total" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="header_right" ItemStyle-CssClass="header_right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Bind("amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Reimburse To" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReimburseToName" runat="server" Text='<%# Bind("ReimburseToName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="Job" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Accounting Code" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountingCodesName" runat="server" Text='<%# Bind("AccountingCodesName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
         
            
           
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    

     <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:Label ID="Label2" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="750px" Height="700px" CssClass=" card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label3" runat="server" Text="Add Cost"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="LinkButton1" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="Group2" />
                       </div>
            </div>

         
       
        <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label">Date </label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                           <asp:TextBox ID="txtDate_e" runat="server" SkinID="DateNew"></asp:TextBox>
                          <%--  <asp:Label ID="lblEdate" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />--%>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"   runat="server"
                                PopupButtonID="lblEdate" TargetControlID="txtDate_e" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtDate_e"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
 
      <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label">Item Name </label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                          <asp:TextBox ID="txtItemName" runat="server" MaxLength="1000"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter item name"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>
                                           
                                          </div>
                                      </div>
    </div>

        <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label">Details</label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                          <asp:TextBox ID="txtDetails" runat="server" MaxLength="1000" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
           <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label">Total </label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                          <asp:TextBox ID="txtTotal" runat="server" SkinID="Price_150px" MaxLength="20"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter total"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label"> Reimburse to</label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                         <asp:DropDownList ID="ddlReimburseto" runat="server"></asp:DropDownList>
                                          </div>
                                      </div>
    </div>
        <%-- <div class="form-group row mb-6">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label"> <%:sessionKeys.JobDisplayName %></label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                         <asp:DropDownList ID="ddlJobsE" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select job"
                                ControlToValidate="ddlJobsE" Display="None" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>--%>
          <div class="form-group row mb-6" style="display:none;visibility:hidden;">
                                  <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label"> Accounting code</label>
                                      <div class="col-sm-9 form-inline d-flex d-inline">
                                         <asp:DropDownList ID="ddlAccountingcode" runat="server"></asp:DropDownList>
                                          </div>
                                      </div>
    </div>
           <div class="form-group row mb-6">
                        <div class="col-md-12 d-flex d-inline">
                                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-9"><asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload> <asp:HiddenField ID="hImageID" runat="server" Value="00000000-0000-0000-0000-000000000000" />
                                
					</div>
				</div>

                        </div>
       
           <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline d-flex d-inline">
                        <label class="col-sm-2 control-label"></label>
                       <div class="col-sm-10 form-inline d-flex d-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save" OnClick="btnSubmitSettings_Click" ValidationGroup="Group2" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>

    

<asp:HiddenField ID="hraised" runat="server" Value="30.00" ClientIDMode="Static" />

<asp:HiddenField ID="hremaing" runat="server" Value="40.00" ClientIDMode="Static" />
<script>
    dchart();

    function dchart() {
        // init chart
        var element = document.getElementById("project_overview_chart");

        if (!element) {
            return;
        }
        const darray = [];
        darray[0] = $("#hraised").val();
        darray[1] = $("#hremaing").val();
        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: darray,
                    backgroundColor: ['#00A3FF', '#E4E6EF']
                }],
                labels: ['Raised amount', 'Remaining amount']
            },
            options: {
                chart: {
                    fontFamily: 'inherit'
                },
                rotation: -90,
                // circumference: 180,
                cutoutPercentage: 75,
                responsive: true,
                maintainAspectRatio: true,
                cutout: '85%',
                title: {
                    display: false
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                },
                tooltips: {
                    enabled: true,
                    intersect: false,
                    mode: 'nearest',
                    bodySpacing: 5,
                    yPadding: 10,
                    xPadding: 10,
                    caretPadding: 0,
                    displayColors: false,
                    backgroundColor: '#20D489',
                    titleFontColor: '#ffffff',
                    cornerRadius: 4,
                    footerSpacing: 0,
                    titleSpacing: 0
                },
                plugins: {
                    legend: {
                        display: false,
                        position: 'bottom',
                        textStyle: { fontSize: 34 },
                        labels: {
                            generateLabels: (chart) => {
                                const datasets = chart.data.datasets;
                                return datasets[0].data.map((data, i) => ({
                                    text: `${chart.data.labels[i]} ${data}`,
                                    fillStyle: datasets[0].backgroundColor[i],
                                }))
                            }
                        }
                    }
                }
            }
        };

        var ctx = element.getContext('2d');
        var myDoughnut = new Chart(ctx, config);
    }
</script>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
