<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="LiveSessions.aspx.cs" Inherits="DeffinityAppDev.App.LiveSessions" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <style>
        select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }

         .search
        {
            background: url(find.png) no-repeat;
            padding-left: 18px;
            border:1px solid #ccc;
        }



    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button"  data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Live Sessions</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
              
                <asp:Button ID="btnAddVideo" runat="server" CssClass="btn btn-primary" Text="Add a New Live Session" OnClick="btnAddVideo_Click" />

            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div class="card-body border-top p-9">
                    <!--begin::Input group-->

                    <!--end::Input group-->

                    <br />

                    <div class="row ">

                        <div class="col-lg-3 mx-5">
                         
                             <asp:TextBox runat="server"></asp:TextBox>

                        </div>

                        <div class="col-lg-2">
                            
                          <asp:Button class="btn btn-primary" runat="server" SkinID="btnSearch" Text="Search" ID="btnsearch" OnClick="btnsearch_Click" />

                        </div>
                    </div>
                   
                    




                  


                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
               
                <!--end::Actions-->
                <input type="hidden" /><div></div>
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>

     <div class="row">
                         <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListFaithGiving_ItemCommand">
                            <LayoutTemplate>
                               
    <!--end::Toolbar-->
                    <!--begin::Post-->
                  
                        <!--begin::Container-->
                       
                            <!--begin::Post card-->
                            <div class="card">
                                <!--begin::Body-->
                                <div class="card-body p-lg-20 pb-lg-0">
                                <div class="row mb-6">
                                    <div runat="server" id="itemPlaceholder"></div>

                                </div>
                                    </div>
                                </div>
                           
                                  
                            </LayoutTemplate>

                            <ItemTemplate>


                                <div class="col-lg-2">
                           <div >
                                <asp:Image ID="img_event" runat="server"  CssClass="img-fluid" ImageUrl='<%# GetImmage(Eval("unid")) %>' />
                         
                                    
                                </div>
                           
                        </div>


                                 
                                 <div class="col-lg-10">
                                     <div class="row">
                                          <div class="col-lg-7">
                                         <h3>      <asp:Label ID="Label1" runat="server" Text='<%# Eval("SessionTitle") %>'></asp:Label>  </h3> 
                                              </div>
                                          <div class="col-lg-4 d-flex d-inline mx-5 ">
                                          <asp:Button ID="Button1" runat="server" Text="Mark as Attending" CommandArgument='<%# Eval("SessionId") %>' CommandName="Attending" OnClick="MarkasAttending"   OnClientClick="return confirm('Mark as Attending?');" style="margin-right:5px" />
                                               <asp:Button ID="Button2" runat="server" Text="View"  CommandArgument='<%# Eval("SessionId") %>' CommandName="View"  style="margin-right:5px" /> 
                                               <asp:Button ID="Button3" runat="server" Text="Edit"  CommandArgument='<%# Eval("SessionId") %>' CommandName="Edit1"    />
                                             </div>
                                           
                                     </div>

                                      <div class="row mb-6">

                                   <br /> <br /> 

                                     <h6>   Date : <%# Eval("DateScheduled") %>   </h6>  <br />
                                     <h6>   By   :  <%# Eval("Speakers") %>     </h6>  <br />
                                     <h6>   Link :  <%# Eval("ZoomLink") %>   </h6>  <br />
                                    </div>

                                     </div>
                                
                            </ItemTemplate>

                             

                        </asp:ListView>
                         

                        </div>
   <%-- <script src="../assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>--%>
</asp:Content>