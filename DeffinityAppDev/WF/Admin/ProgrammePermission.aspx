<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProgrammePermission" Codebehind="ProgrammePermission.aspx.cs" %>
<%@ Register src="controls/ProgrammeManagement.ascx" tagname="ProgrammeManagement" tagprefix="uc1" %>
    <%@ Register src="controls/PermissionsTab.ascx" TagName="PermissionTab" TagPrefix="UcPT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
   <UcPT:PermissionTab runat="server" ID="PT1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
  Admin
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Permission Manager
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   

<div class="row">

    <div class="col-md-12">
    
        <div class="col-sm-4">
           <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> Permissions&nbsp;at&nbsp;the&nbsp;Customer&nbsp;Level:</strong>
            <hr class="no-top-margin" />
            </div>
</div>
            <div class="form-group">
             <div class="col-md-12">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="Group1"/>
                 </div>
                </div>
                <div class="form-group">
                
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Customers:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="DropDownListCustomer" runat="server" SkinID="ddl_90" 
                                AutoPostBack="True" onselectedindexchanged="DropDownListCustomer_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" Display="None"
                            ControlToValidate="DropDownListCustomer" ValidationGroup="Group1"
                             ErrorMessage="Please select customer"></asp:RequiredFieldValidator>
        </div>
                        </div>
                    </div>
             <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                           Group:</div>
                        <div  class="col-sm-8">
                            <asp:DropDownList ID="ddlCLTeam" SkinID="ddl_90" runat="server" 
                                onselectedindexchanged="ddlCLTeam_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  InitialValue="0" ControlToValidate="ddlCLTeam"
                             ErrorMessage="Please select group" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            
                        </div>
                        </div>
                       </div>
             <div class="form-group">
                         <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Role:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCLRole" runat="server" SkinID="ddl_90" 
                                onselectedindexchanged="ddlCLRole_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" Display="None"
                            ControlToValidate="ddlCLRole" ValidationGroup="Group1"  ErrorMessage="Please select role"></asp:RequiredFieldValidator>
                        </div>
                        </div>
                 </div>
             <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-sm-4 control-label"></div>
                        <div class="col-sm-8">
                             <asp:Button ID="imgApplyCustomerLevel" runat="server" SkinID="btnApply" 
                                onclick="imgApplyCustomerLevel_Click" ValidationGroup="Group1" />
                        </div>
                   </div>
                 </div>
                    
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblCLMsg" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                          
                            <asp:GridView ID="grdCustomeLevel"  EmptyDataText="No Records Found"
                            runat="server" Width="100%" AutoGenerateColumns="false" 
                                onrowcommand="grdCustomeLevel_RowCommand" 
                                onrowdeleting="grdCustomeLevel_RowDeleting" 
                                onrowcancelingedit="grdCustomeLevel_RowCancelingEdit" 
                                onrowdatabound="grdCustomeLevel_RowDataBound" 
                                onrowediting="grdCustomeLevel_RowEditing" 
                                onrowupdating="grdCustomeLevel_RowUpdating">
                            <Columns>
                             <asp:TemplateField ControlStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("PID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("PID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("PID")%>'  SkinID="BtnLinkUpdate"
                                            ToolTip="Update"></asp:LinkButton>
                                 <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group">
                            <ItemTemplate>
                                <asp:Label ID="lblTeam" runat="server" Text='<%# Bind("TeamName")%>' Visible="false"></asp:Label>
                                 <asp:LinkButton ID="btnViewMembers1" runat="server" CommandArgument='<%# Bind("ID") %>' Text='<%#Eval("TeamName")%>' OnClick="btnViewMembers_Click" ></asp:LinkButton>
                                  <asp:HiddenField runat="server" ID="hidTeamID" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblTeamID" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCLTeamEdit" runat="server" SkinID="ddl_90"  OnSelectedIndexChanged="ddlCLTeamEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                            <asp:Label ID="lblPermissions" runat="server" Text='<%#RoleType(DataBinder.Eval(Container.DataItem,"RoleID").ToString())%>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("RoleID")%>' Visible="false"></asp:Label>
                             <asp:DropDownList ID="ddlCLPermissions" runat="server" SkinID="ddl_90">
                              <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                             </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("PID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                </div>
        
        <div class="col-sm-4">
            <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> Permissions&nbsp;at Programme&nbsp;Level: </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                <div class="form-group">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server"  ValidationGroup="Group2"/>
                    </div>

             <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Programme:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlProgramme" SkinID="ddl_90" runat="server" 
                                AutoPostBack="True" onselectedindexchanged="ddlProgramme_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProgramme"
                             ErrorMessage="Please select programme" ValidationGroup="Group2" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                        </div>
                        </div>
                 </div>
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Group:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlProgrammeLevelTeam" SkinID="ddl_90" runat="server" 
                                onselectedindexchanged="ddlProgrammeLevelTeam_SelectedIndexChanged" >
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  InitialValue="0" ControlToValidate="ddlProgrammeLevelTeam"
                             ErrorMessage="Please select group" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                            
                        </div>
                        </div>
                    </div>
            <div class="form-group">
                        <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Role:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlRolePL" runat="server" SkinID="ddl_90">
                            <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" Display="None"
                            ControlToValidate="ddlRolePL" ValidationGroup="Group2"  ErrorMessage="Please select role"></asp:RequiredFieldValidator>
                        </div>
                        </div>
                </div>
            <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-sm-4 control-label"></div>
                        <div class="col-sm-8">
                             <asp:Button ID="ImgApplyPL" runat="server" ValidationGroup="Group2" SkinID="btnApply" onclick="ImgApplyPL_Click"/>
                        </div>
                    </div>
                </div>
            <div class="form-group">
                    <div class="col-md-12">
                          <asp:Label ID="lblMsgPL" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                   </div>
            <asp:GridView ID="grdProgrammeLeve"  EmptyDataText="No Records Found"
                            runat="server" Width="100%" AutoGenerateColumns="false" 
                                onrowcommand="grdProgrammeLeve_RowCommand" 
                                onrowdeleting="grdProgrammeLeve_RowDeleting" 
                                onrowcancelingedit="grdProgrammeLeve_RowCancelingEdit" 
                                onrowdatabound="grdProgrammeLeve_RowDataBound" 
                                onrowediting="grdProgrammeLeve_RowEditing" 
                                onrowupdating="grdProgrammeLeve_RowUpdating">
                            <Columns>
                             <asp:TemplateField  ControlStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("PID")%>' Visible="false"> </asp:Label>
                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProgrammeID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("PID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                               
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("PID")%>'  SkinID="BtnLinkUpdate"
                                            ToolTip="Update"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group">
                            <ItemTemplate>
                                <asp:Label ID="lblPLTeam" runat="server" Text='<%# Bind("TeamName")%>' Visible="false"></asp:Label>
                                 <asp:LinkButton ID="btnViewMembers2" runat="server" CommandArgument='<%# Bind("ID") %>' Text='<%#Eval("TeamName")%>' OnClick="btnViewMembers1_Click" ></asp:LinkButton>
                                  <asp:HiddenField runat="server" ID="hidTeamID1" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblPLTeamID1" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlPLTeamEdit" runat="server" SkinID="ddl_90"  OnSelectedIndexChanged="ddlPLTeamEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                            <asp:Label ID="lblPLPermissions" runat="server" Text='<%#RoleType(DataBinder.Eval(Container.DataItem,"RoleID").ToString())%>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblSPLRoleID" runat="server" Text='<%# Bind("RoleID")%>' Visible="false"></asp:Label>
                             <asp:DropDownList ID="ddlPLPermissions" runat="server" SkinID="ddl_90">
                              <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                             </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblPLTeamID" runat="server" Text='<%# Bind("TeamID") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("PID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                            </asp:GridView>


                </div>
             
        <div class="col-sm-4">
            <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> Permissions&nbsp;at Sub&nbsp;Programme&nbsp;Level:</strong>
            <hr class="no-top-margin" />
            </div>
</div>
              <div class="form-group">
                <asp:ValidationSummary ID="ValidationSummary4" runat="server"  ValidationGroup="Group4"/>
                  </div>
                
             <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Sub&nbsp;Programme:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSubProgramme"  SkinID="ddl_90" runat="server" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlSubProgramme_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubProgramme"
                             ErrorMessage="Please select sub programme" ValidationGroup="Group4" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                 </div>
            <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Group:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSPLTeam" SkinID="ddl_90" runat="server" onselectedindexchanged="ddlSPLTeam_SelectedIndexChanged1" >
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"  InitialValue="0" ControlToValidate="ddlSPLTeam"
                             ErrorMessage="Please select group" Display="None" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                        </div>
                        </div>
                </div>
            <div class="form-group">
                     <div class="col-md-12">
                        <div class="col-sm-4 control-label">
                            Role:</div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSPLRole" runat="server" SkinID="ddl_90">
                           <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0" Display="None"
                            ControlToValidate="ddlSPLRole" ValidationGroup="Group4"  ErrorMessage="Please select role"></asp:RequiredFieldValidator>
                        </div>
                        </div>
                </div>
            <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-sm-4 control-label"></div>
                        <div class="col-sm-8">
                             <asp:Button ID="ImageButton2" runat="server" ValidationGroup="Group4" 
                                 SkinID="btnApply" onclick="ImageButton2_Click" />
                        </div>
                    </div>
                </div>
                    <div class="form-group">
                         <div class="col-md-12">
                    <asp:Label ID="lblSPL" runat="server" Text="" Visible="false"></asp:Label>
                             </div>
                        </div>
                            <asp:GridView ID="grdSubProgrammeLevel"  EmptyDataText="No Records Found"
                            runat="server" Width="100%" AutoGenerateColumns="false" 
                                onrowcommand="grdSubProgrammeLevel_RowCommand" 
                                onrowdeleting="grdSubProgrammeLevel_RowDeleting" 
                                onrowcancelingedit="grdSubProgrammeLevelRowCancelingEdit" 
                                onrowdatabound="grdSubProgrammeLevel_RowDataBound" 
                                onrowediting="grdSubProgrammeLevel_RowEditing" 
                                onrowupdating="grdSubProgrammeLevel_RowUpdating">
                            <Columns>
                             <asp:TemplateField  HeaderStyle-CssClass="header_bg_l">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("PID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("PID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("PID")%>'  SkinID="BtnLinkUpdate"
                                            ToolTip="Update"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group">
                            <ItemTemplate>
                                <asp:Label ID="lblSPLTeam" runat="server" Text='<%# Bind("TeamName")%>' Visible="false"></asp:Label>
                                 <asp:LinkButton ID="btnViewMembers3" runat="server" CommandArgument='<%# Bind("ID") %>' Text='<%#Eval("TeamName")%>' OnClick="btnViewMembers3_Click" ></asp:LinkButton>
                                  <asp:HiddenField runat="server" ID="hidTeamID3" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblSPLTeamID" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSPLTeamEdit" runat="server" Width="100px"  OnSelectedIndexChanged="ddlPLTeamEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                            <asp:Label ID="lblSPLPermissions" runat="server" Text='<%#RoleType(DataBinder.Eval(Container.DataItem,"RoleID").ToString())%>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lblSPLRoleID" runat="server" Text='<%# Bind("RoleID")%>' Visible="false"></asp:Label>
                             <asp:DropDownList ID="ddlSPLPermissions" runat="server" SkinID="ddl_90">
                               <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                             </asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("PID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                </div>
            
             </div>
        
            </div>
            

           
    <asp:Panel runat="server" ID="pnlTeamMembers" Visible="false">
        <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="Label3" runat="server" ></asp:Label>&nbsp;:Members within&nbsp;<asp:Label ID="Label2" runat="server" ></asp:Label>&nbsp; Group </strong>
            <hr class="no-top-margin" />
            </div>
</div>
       
        <div class="row">
<div class="col-md-12">
            <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></div>
            </div>
       
        <asp:GridView ID="gridMembers" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center"
            Width="100%" EmptyDataText="No Records Found" PageSize="10">
            <Columns>
                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="20px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="cbSelectAll" 
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll"  runat="server" />
                        <asp:Label ID="lblID" Text='<%# Bind("Name") %>' runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemStyle Width="200px" />
                    <ItemTemplate>
                      <img src="media/ico_member.png" />&nbsp;
                        <asp:Literal ID="litTeamSize" Text='<%#Eval("memName")%>' runat="server" />
                        <asp:HiddenField runat="server" ID="hidTeamID" Value='<%# Bind("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Email" DataField="EmailAddress" ItemStyle-Width="75px">
                    <ItemStyle Width="75px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Contact Number" DataField="ContactNumber"
                    ItemStyle-Width="75px">
                    <ItemStyle Width="75px" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" />
        </asp:GridView>
       
    </asp:Panel>
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>


