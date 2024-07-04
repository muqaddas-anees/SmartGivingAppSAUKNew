<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" CodeBehind="VideoLibrary.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.VideoLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Video Library
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">

             <div  class="col-sm-4" runat="server">
                                     <div class="xe-widget xe-todo-list xe-counter-blue">
                                         <div class="xe-header">
                                             <div class="xe-icon">
                                                 <%--<i class="fa-desktop"></i>--%>
                                                 <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                             </div>
                                             <div class="xe-label">
                                                 <strong style="font-size:medium;"><asp:Literal ID="lbltitle" runat="server"></asp:Literal></strong>
                                             </div>
                                         </div>
                                         <div class="xe-body" style="font-size:medium;">
                                                 <div style="height:120px;">
                                                    <asp:Literal ID="lblMdata" runat="server"></asp:Literal>
                                             </div>
                                            
                                         </div>
                                        
                                         <div class="xe-footer" style="padding-top:8px;">
                                           
                                              <br /><br />
                                             <asp:Button ID="BtnView" runat="server" CommandName="view" Text="Watch Video" SkinID="btnDefault" Font-Size="Large" style="width:100%"  />

                                         </div>
                                     </div>
                                 </div>

            </div>
        </div>
</asp:Content>
