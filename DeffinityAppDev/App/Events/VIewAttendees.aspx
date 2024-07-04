<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="VIewAttendees.aspx.cs" Inherits="DeffinityAppDev.App.Events.VIewAttendees" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/App/controls/EventDdlCtrl.ascx" TagPrefix="Pref" TagName="EventDdlCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblEvent" runat="server" Visible="false"></asp:Label><Pref:EventDdlCtrl runat="server" id="EventDdlCtrl" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
   <asp:Button ID="btnRegister" runat="server" SkinID="btnDefault" Text="Register" OnClick="btnRegister_Click" />  <asp:HyperLink ID="linkback" runat="server" NavigateUrl="~/App/Events/EventList.aspx" Text="Back to Events" CssClass="btn btn-light mx-5"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row p-0 mb-5 px-9">
														<!--begin::Col-->
														<div class="col-lg-3">
															<div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-primary d-block">Number Expected</span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true" >  <label id="lblPending" runat="server">0</label>  </span>
															</div>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-3">
															<div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-primary d-block">Attended</span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"><label id="lblAttended" runat="server">0</label></span>
															</div>
														</div>

       
														<!--end::Col-->
														<!--begin::Col-->
													
														<!--end::Col-->
													</div>
    <div class="row p-0 mb-5 px-9">
         <asp:ListView ID="listdata" runat="server">
            <LayoutTemplate>
               
															  <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
														
            </LayoutTemplate>
           
            <ItemTemplate>
                 <div class="col-lg-3">
                <div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-primary d-block"> <%# Eval("Title") %> </span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"> <%# Eval("Value") %> </span>
															</div>
                     </div>
            </ItemTemplate>
        </asp:ListView>
        </div>
  <div class="row p-0 mb-5 px-9">
											<div class="col-lg-2 d-flex d-inline">
                                                <asp:Label ID="lblstart" runat="server" Text="Start date" style="margin-top:10px"></asp:Label>
                                               <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>
                                                </div>
      	<div class="col-lg-2  d-flex d-inline">
                                    <asp:Label ID="Label1" runat="server" Text="End date" style="margin-top:10px"></asp:Label> <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>                  
                                                </div>
      	<div class="col-lg-2"><asp:TextBox ID="txtSearch" runat="server" placeholder="Search..."></asp:TextBox>
                                                
                                                </div><!--begin::Col-->
														
      <div class="col-lg-6">
          <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
          <asp:Button ID="btnDownload" runat="server" SkinID="btnDefault" Text="Download Report" OnClick="btnDownload_Click" />
          <asp:Button ID="btnSendMail" runat="server" SkinID="btnDefault" Text="Send Mail" OnClick="btnSendMail_Click" />
          </div>
      </div>

    <div class="row mb-6">
          <div class="col-lg-12">
              <asp:GridView ID="GridViewAttendece" runat="server" OnRowCommand="GridViewAttendece_RowCommand" OnRowDataBound="GridViewAttendece_RowDataBound" OnRowEditing="GridViewAttendece_RowEditing">
                  <Columns>
                      <asp:TemplateField HeaderText="Ticket Type">
                          <ItemTemplate>
                              <asp:Label ID="lblTypeofTicket" runat="server" Text='<%# Eval("TicketType") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="First Name">
                          <ItemTemplate>
                              <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("Firstname") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                          <ItemTemplate>
                              <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("Lastname") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Email">
                          <ItemTemplate>
                              <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cell">
                          <ItemTemplate>
                              <asp:Label ID="lblContact" runat="server" Text='<%# Eval("UserContact") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                          <ItemTemplate>
                               <table>
                                   <tr>
                                        <td>
                              <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Width="125px" Visible="false"></asp:Label>
                                            <asp:Label ID="lblDStatus" runat="server" Text='<%# Eval("DStatus") %>' Width="125px"></asp:Label>
                                            </td>
                                       <td>
                              <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" SkinID="BtnLinkEdit" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                           </td>
                                       </tr>
                                   </table>
                          </ItemTemplate>
                           <EditItemTemplate>
                               <table>
                                   <tr>
                                       <td colspan="2">
                                              <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_125px">
                                   <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Attended" Value="Attended"></asp:ListItem>
                               </asp:DropDownList>
                                       </td>
                                       
                                   </tr>
                                  <tr>
                                       <td>
                                            <asp:Button ID="btnEdit" runat="server" SkinID="btnSubmit" CommandName="updatedstats" CommandArgument='<%# Eval("ID") %>'></asp:Button>
                                       </td>
                                       <td> <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" CommandName="updatedcancel" CommandArgument='<%# Eval("ID") %>'></asp:Button>

                                        </td>
                                   </tr>
                               </table>
                             
                           </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField  HeaderText="Booking Date & Time">
                          <ItemTemplate>
                              <asp:Label ID="lblValidatedDateTime1" runat="server" Text='<%# Eval("BookingDate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Checked In Date & Time">
                          <ItemTemplate>
                              <asp:Label ID="lblValidatedDateTime2" runat="server" Text='<%# Eval("ValidatedDateTime1") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField  HeaderText="">
                          <ItemTemplate>
                             <asp:LinkButton ID="btnDel" runat="server" CommandName="del" Visible='<%# dvisible() %>' SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete record?');" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>

        </div>

        </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
