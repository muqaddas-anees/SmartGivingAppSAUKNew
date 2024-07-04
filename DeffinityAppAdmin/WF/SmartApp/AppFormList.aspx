<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
     AutoEventWireup="true" Inherits="App_App_ManagerAssignedForm" EnableEventValidation="false" Codebehind="AppFormList.aspx.cs" %>
<%@ Register Src="Controls/apptabs.ascx" TagPrefix="app" TagName="apptabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <app:apptabs runat="server" ID="apptabs" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
   Smart Apps
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
    <asp:Literal ID="form_lable" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
          <div class="col-md-12">
                <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="Create" />
                <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
          </div>
    </div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><asp:Literal ID="form_subname" runat="server"></asp:Literal></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtFormName" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="req1" runat="server" ValidationGroup="Create" ControlToValidate="txtFormName"
                         ErrorMessage="Please enter form name" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">Notes</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtNotes" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <div class="col-sm-9">
                <asp:Button ID="btnCreateNewEntry" runat="server" Text="Create A New Entry" ValidationGroup="Create" OnClick="btnCreateNewEntry_Click" />
            </div>
	</div>
    </div>
    <div style="overflow-x:scroll;" class="form-group">
        <asp:GridView ID="GridCreatedEntries" runat="server" Width="100%" OnRowCommand="GridCreatedEntries_RowCommand"
             ShowFooter="false" ShowHeader="true" AutoGenerateColumns="true" SkinID="Custom_Grid"
            OnRowDeleting="GridCreatedEntries_RowDeleting" OnRowEditing="GridCreatedEntries_RowEditing" OnRowDataBound="GridCreatedEntries_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="Editlink" runat="server" SkinID="Editicon" 
                                         NavigateUrl='<%# string.Format("~/WF/SmartApp/AppForm.aspx?AppFormID={0}", HttpUtility.UrlEncode(Eval("ID").ToString())) %>'></asp:HyperLink>
                        <asp:Label ID="lblDeleteId" Text='<%#Bind("ID") %>' runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                           <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                SkinID="BtnLinkDelete" CommandArgument='<%#Bind("ID") %>' CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
  
</asp:Content>

