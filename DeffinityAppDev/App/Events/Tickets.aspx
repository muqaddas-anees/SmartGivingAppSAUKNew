<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="DeffinityAppDev.App.Events.Tickets" %>

<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Event
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    

    <!--begin::Head-->
	<%--<head>
		<title>Event Name and Details</title>
		
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	</head>--%>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height:55px;--kt-toolbar-height-tablet-and-mobile:55px">
		<!--begin::Main-->
		



			<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">


					<%--	<asp:HiddenField ID="hEventid" runat="server" />--%>
                        <asp:HiddenField ID="hUnid" runat="server" />

                        <div id="LocationEventId">


                            <div class="card card-xl-stretch mb-5 mb-xl-8">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-person-video3 text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Manage Tickets</h3>
                                    </div>
                                    <!--end::Card title-->
                                     <div class="card-toolbar">
                                          <asp:Button ID="Button1" runat="server"  Text="Add Tickets" ToolTip="Add Tickets" OnClick="btnUpload_Click" Style="margin-right: 20px;" /> 
                                         <asp:HiddenField ID="hEventID" runat="server" Value="0" />
                                </div>
                                </div>
                               

                                <div class="card-body border-top p-9">



                                    <div class="row mb-6 d-flex justify-content-end"" >
												   <div class="col-sm-3 d-flex justify-content-end">

                                  <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           
</div>
                                   
                                    </div>

                                    <div class="row ">
                                        <div class="col-lg-12">

                                            <style>
                                                .rightcls{
                                                   
                                                    text-align:right;
                                                    margin-right:15px;
                                                }
                                                 .centercls{
                                                   
                                                    text-align:center;
                                                    
                                                }
                                            </style>
											<asp:GridView runat="server" ID="BannerList" OnRowCommand="BannerList_RowCommand" Width="100%">

												<Columns>
													<asp:TemplateField ItemStyle-Width="5%">
														<ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit1" SkinID="BtnLinkEdit"></asp:LinkButton>
                                                            
															 <%-- <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="btn btn-gray" > </input >--%>
                                                                                    
														</ItemTemplate>
													</asp:TemplateField>
													
													<asp:TemplateField HeaderText="Ticket Type" ItemStyle-Width="70%">
														<ItemTemplate>
															<asp:Label ID="lblSpeaker" runat="server" Text=' <%# Eval("TypeOfTicket") %> '></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Price" ItemStyle-Width="10%" ItemStyle-CssClass="rightcls" HeaderStyle-CssClass="rightcls" >
														<ItemTemplate>
															<asp:Label ID="lblPrice" runat="server" Text=' <%# Eval("PriceDisplay") %>' ></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Slot(s)" ItemStyle-Width="10%" ItemStyle-CssClass="centercls" HeaderStyle-CssClass="centercls">
														<ItemTemplate>
															<asp:Label ID="lblSolts" runat="server" Text= '<%# Eval("Solts") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton ID="btnDel" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>' SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete this record?');"  />
														</ItemTemplate>
													</asp:TemplateField>
													
												</Columns>
											</asp:GridView>
                                            </div>
										</div>
											     
                                            <asp:HiddenField ID="HiddenSpeakerID" runat="server" />


                                            <asp:Button ID="btnEditSpeaker" runat="server" Text="Button"  OnClick="EditSpeakerinList"  style="display: none"    />


						<ajaxToolkit:ModalPopupExtender ID="mdlAddSpeaker" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnSpeakerAddOption" PopupControlID="pnlAddSpeaker" CancelControlID="btnSpekerClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnSpeakerAddOption" runat="server"></asp:Label>
    <asp:Label ID="btnSpekerClose" runat="server"></asp:Label>

	<asp:Panel ID="pnlAddSpeaker" runat="server" BackColor="White" Style="display: none;"
        Width="1000px"  Height="450px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered p-10">
            <div class="card-header mb-6">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Add Ticket"></asp:Label>
                </h3>
                <div class="card-toolbar">
                  
                </div>
            </div>
           

            <!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												 <label class="col-lg-3 col-form-label required fw-bold fs-6"> Ticket type: </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtTicketType" runat="server"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->




               <div class="row mb-6">
                <!--begin::Label-->
             <label class="col-lg-3 col-form-label required fw-bold fs-6"> Slots :</label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-9 fv-row fv-plugins-icon-container">
                           <asp:TextBox ID="txtSlots" placeholder="0" runat="server" SkinID="Price_125px" MaxLength="10"></asp:TextBox>
                            <div class="fv-plugins-message-container invalid-feedback"></div>
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>

               <div class="row mb-6">
                <!--begin::Label-->
             <label class="col-lg-3 col-form-label required fw-bold fs-6"> Price :</label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-9 fv-row fv-plugins-icon-container">
                           <asp:TextBox ID="txtPrice" SkinID="Price_125px" MaxLength="100" runat="server"></asp:TextBox>
                            <div class="fv-plugins-message-container invalid-feedback"></div>
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>


            <div class="card-footer d-flex justify-content-end py-6 px-9 gap-3">
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-light" Text="Close" />

                

                <asp:Button runat="server" ID="UploadBanner" Text="Submit"  OnClick="UploadBanner_Click"  />

               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>

    </asp:Panel>


	



                                </div>

                                <div class="card-footer d-flex justify-content-end py-6 px-9">              
                                   

                             
                                  <asp:Button ID="btnPublish" runat="server" SkinID="btnDefault" Text="Save & Publish"  Visible="false" />

                                </div>


                            </div>

                        </div>


						

	  <style>
        .modalBackground {
            overflow : scroll;
           
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
           
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }



          select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }

    </style>


						</div>
						</body>
	
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>

