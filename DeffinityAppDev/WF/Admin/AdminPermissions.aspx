<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminPermissions" Codebehind="AdminPermissions.aspx.cs" %>

<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
<%@ Register Src="controls/MangeUserTab.ascx" TagName="MangeUserTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Permission Manager
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group">
             <div class="col-md-12 form-inline">
                User Admin for : <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12 form-inline">
                 <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
                 </div>
        </div>
    

    <div class="form-group">
             <div class="col-md-12 form-inline pull-right">
                  <div class="col-sm-4"> </div>
                 <div class="col-sm-4"> 
                 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%> :</label>
            <div class="col-sm-8">
                         <asp:DropDownList ID="ddlCustomers" SkinID="ddl_90" runat="server"
                             AutoPostBack="True" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged">
                         </asp:DropDownList>
                </div>
                     </div>
                 <div class="col-sm-4"> </div>
</div>
</div>
    <div class="form-group">
                                  <div class="col-md-4 form-inline">
                                        <asp:Label ID="lblUser" runat='server' Font-Bold="true"></asp:Label>
                            &nbsp;is a member of
                                      <br />
                            the following groups:
				</div>
 <div class="col-md-4">
                                       <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   Project Permissions</strong>
            <hr class="no-top-margin" />
            </div>
</div>

      <asp:LinkButton ID="lnkSelectAll" runat="server" OnClick="lnkSelectAll_Click">Select All</asp:LinkButton>
                            <asp:Panel Width="250px" runat="server" ID="Panel1" Height="300px" CssClass="txt_field" BorderStyle="Inset">
                                <evy:ScrollableListBox ID="CheckBoxList2" runat="server" BorderWidth="0px"
                                    RepeatLayout="Flow" Height="300px" Width="250px">
                                </evy:ScrollableListBox>
                            </asp:Panel>
      <div class="form-group">
    <div class="col-sm-12" style="padding-top:10px"> 
                             <asp:Button ID="imgApply" SkinID="btnApply" runat="server"
                                OnClick="imgApply_Click" />
        </div>
          </div>
				</div>
<div class="col-md-4" style="display:none;">
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   Service Desk Permissions </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                        
                            <asp:LinkButton ID="lnkSelecSDtAll" runat="server" OnClick="lnkSelectSDAll_Click">Select All</asp:LinkButton>
                            <asp:Panel Width="250px" runat="server" ID="Panel2" Height="300px" BorderStyle="Inset">

                                <evy:ScrollableListBox ID="chkSDTeam" runat="server" BorderWidth="0px"
                                    RepeatLayout="Flow" Height="300px" Width="250px">
                                </evy:ScrollableListBox>
                            </asp:Panel>
    <div class="form-group">
    <div class="col-sm-12" style="padding-top:10px"> 
        
                            <asp:Button ID="imgSDTeamApply" SkinID="btnApply" runat="server" OnClick="imgSDTeamApply_Click" />
        </div>
        </div>
				</div>
</div>
   
</asp:Content>


