<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUsersSkills" Title="Untitled Page" Codebehind="AdminUsersSkills.aspx.cs" %>

<%@ Register Src="controls/MangeUserTab.ascx" TagName="MangeUserTab" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Training/controls/ManageUserSkillsCtrl.ascx" TagName="ManageUserSkills"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Manage&nbsp;Users&nbsp; - Skills/Training
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
     <asp:Panel ID="UserData" runat="server">
    <div class="form-group">
             <div class="col-md-12 form-inline">
                 <%= Resources.DeffinityRes.UserAdminfor%>:  <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
    <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
    <div class="form-group">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="valsummarySkills" runat="server" />
                            <asp:Label ID="lblSkillErr" runat="server" Visible="false" EnableViewState="false" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblSkillMsg" runat="server" Visible="false" EnableViewState="false" ForeColor="Green"></asp:Label>
</div>
</div>
          <div class="form-group">
             <div class="col-md-12">
     <uc1:ManageUserSkills ID="ManageUserSkills1" runat="server" />
                 </div>
              </div>
     <asp:GridView ID="grdTrainingRecords" runat="server"  DataKeyNames="ID" AllowPaging="True"  PageSize="10" Visible="false"
                                      EmptyDataText="No Records Found" 
                                 onpageindexchanging="grdTrainingRecords_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Training" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraining" runat="server" Text='<%# Bind("CourseTitle") %>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Category" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName")%>'   Width="150px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Start Date"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("DateofCourse","{0:d}")%>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="End Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate","{0:d}")%>'  Width="150px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header_bg_r" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusName")%>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                               
                            </Columns>
                        </asp:GridView>
    <asp:GridView ID="grdUserSkills" runat="server" ShowFooter="True" DataKeyNames="ID" AllowPaging="True" 
                                      onrowcancelingedit="grdUserSkills_RowCancelingEdit" 
                                      onrowcommand="grdUserSkills_RowCommand" onrowediting="grdUserSkills_RowEditing" 
                                      onrowupdating="grdUserSkills_RowUpdating" 
                                      onrowdatabound="grdUserSkills_RowDataBound" 
                                      EmptyDataText="No Skills/Training exist with this user" 
                                      onrowdeleting="grdUserSkills_RowDeleting" PageSize="10" 
                                      onpageindexchanging="grdUserSkills_PageIndexChanging" Visible="false" >
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUID" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CausesValidation="false" CommandName="Edit"
                                            SkinID="BtnLinkEdit" ToolTip="Edit" />
                                        <asp:Label ID="lblID1" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" SkinID="BtnLinkUpdate"
                                            CommandArgument='<%# Bind("ID") %> ' ToolTip="Update" ValidationGroup="ValEdit" />
                                        <asp:LinkButton ID="btncancel" runat="server" CausesValidation="false" CommandName="cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Skills" SortExpression="Skills">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSkills" runat="server" Text='<%# Bind("Skills") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditSkills" Width="200px" runat="server" Text='<%# Bind("Skills") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSkills" Width="200px" runat="server" Text='<%# Bind("Skills") %>'></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemStyle Width="200px" />
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level" SortExpression="SkillLevel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSkillLevel" runat="server" Text='<%# Bind("SkillLevel") %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlESLevel" runat="server">
                                        <asp:ListItem Value="1" Text="Basic" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Intermediate"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Advanced"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    <asp:DropDownList ID="ddlFSLevel" runat="server" >
                                        <asp:ListItem Value="1" Text="Basic" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Intermediate"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Advanced"></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes")%>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditNotes" runat="server" Text='<%# Bind("Notes") %>'
                                            Width="200px"></asp:TextBox>
                                       
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSNotes" runat="server" Text='<%# Bind("Notes") %>'
                                            Width="200px"></asp:TextBox>
                                       
                                    </FooterTemplate>
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                 <asp:TemplateField >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton CommandName="Insert" ID="ImageButton5" runat="server" SkinID="BtnLinkUpdate"
                                                ValidationGroup="TimeSheetFooter" />
                                            <asp:LinkButton CommandName="Insert_Empty" ID="ImageButton20" runat="server" SkinID="BtnLinkCancel" />
                                        </FooterTemplate>
                                        <FooterStyle Width="45px" />
                                    </asp:TemplateField>
                               
                            </Columns>
                        </asp:GridView>
   
                   
                        
                    </asp:Panel>
             
     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>
