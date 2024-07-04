<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Publish.aspx.cs" Inherits="DeffinityAppDev.App.Events.Publish" %>

<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px}
       </style>
    

    <div class="container-xxl" id="kt_content_container">
        <!--begin::Row-->
        <div class="row gy-5 g-xl-8">
            <br />
            <br />
            <h1 class="d-flex align-items-center text-dark fw-bolder fs-3 my-1">Publish Event</h1>
            <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />

            <!--end::Col-->
            <!--begin::Col-->
            <div class="col-xl-12">
                <!--begin::Mixed Widget 12-->




                <div class="card card-xl-stretch mb-5 mb-xl-8">
                    <!--begin::Header-->



                    <div id="kt_content_container" class="container-xxl">


                        <div id="MainEventImageId" >
                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-images text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Publish Event</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">


                                     <div class="card mb-5 mb-xl-10">
                                  
                                    <div class="card-body  p-9">
                                        <div class="row mb-6">
                                            <div class="col-lg-6">
                                                <div class="fv-row">
       <div class="overlay mt-8">
																<!--begin::Image-->
																<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px"  style=background-image:url('<%= GetImmage(huid.Value) %>')></div>  
																<!--end::Image-->
																<!--begin::Links-->
																
																<!--end::Links-->
															
															</div>
    </div>
                                                	

                                            </div>

                                             <div class="col-lg-6 pt-10">
                                                 
                                            <div class="row mb-6 ms-0">
                                                <div class="col-lg-12 fv-row fv-plugins-icon-container mb-6">
                                                    <h5 class="fw-bolder m-0 display-5"><asp:Label ID="lblEventTitle" runat="server"></asp:Label> </h5>
                                                    </div>

                                                <div class="col-lg-12 fv-row fv-plugins-icon-container  mb-6">
                                                    <h5 class="fw-bolder m-0"><i class='far fa-calendar-alt' style='font-size:24px;padding-right:10px'></i> <asp:Label ID="lblStartDate" runat="server" Font-Size="20px"></asp:Label> </h5>
                                                    </div>
                                                  <div class="col-lg-12 fv-row fv-plugins-icon-container  mb-6">
                                                    <h5 class="fw-bolder m-0"><i class='fas fa-location-arrow' style='font-size:24px;padding-right:10px'></i><asp:Label ID="lblVenue" runat="server" Font-Size="20px"></asp:Label></h5>
                                                    
                                                </div>
                                                  <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0"><asp:Label ID="lblTickets" runat="server"></asp:Label></h5>
                                                  </div>
                                            </div>

                                          
                                             </div>
                                            <!--begin::Label-->

                                            <!--end::Label-->
                                            <!--begin::Col-->

                                            <!--end::Col-->
                                        </div>

                                    </div>
                                </div>
                              

                                  

                                


                                    <div class="row mb-6 ms-0" style="font-size:17px;display:none;visibility:hidden;">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h3 class="fw-bolder m-0">Who can your event? </h3>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                   <asp:RadioButtonList id="rdlist" runat="server" CssClass="mycheckBig">
                                                       <asp:ListItem Value="1" Text="Public"></asp:ListItem>
                                                       <asp:ListItem Value="0" Text="Private"></asp:ListItem>
                                                   </asp:RadioButtonList>
                                                </div>

                                               
                                            </div>
                                      <div class="row mb-6 ms-0" style="font-size:17px">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h3 class="fw-bolder m-0">When should we publish your event? </h3>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                   <asp:RadioButtonList id="rplist" runat="server" CssClass="mycheckBig">
                                                       <asp:ListItem Value="1" Text="Publish Now"></asp:ListItem>
                                                      <%-- <asp:ListItem Value="0" Text="Schedule for later"></asp:ListItem>--%>
                                                   </asp:RadioButtonList>
                                                </div>
                                           <div class="row mb-6 ms-0" style="display:none;visibility:hidden;">
                                                <div class="col-lg-2 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Start Date </h5>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" Style="width:175px"  placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>

                                                <div class="col-lg-2 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Start  time</h5>
                                                    <%--<input type="time" id="Time13" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start time" />--%>
                                                    <asp:TextBox ID="txtStartTime" runat="server" SkinID="TimeNew"  Style="width:150px" placeholder="hh:mm"></asp:TextBox>
                                                </div>
                                            </div>
                                               
                                            </div>

                                </div>

                                <div class="card-header border-0 " aria-expanded="true">
                                </div>

                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                  


                                    <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
                                    <%-- <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="" value="Back" />--%>
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                   <%-- <input type="button" onclick="NextAddTicketsIdTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black" class="btn btn-primary" value="Next" />--%>

                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;




                                </div>


                               

                            </div>

                        </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>
    



</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
