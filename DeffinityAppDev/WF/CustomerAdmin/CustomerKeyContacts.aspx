<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CustomerKeyContacts.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.CustomerKeyContacts" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/ContactTabCtrl.ascx" TagPrefix="Pref" TagName="ContactTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Contacts
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:ContactTabCtrl runat="server" ID="ContactTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <asp:Label ID="lblContact" runat="server"></asp:Label> - Contacts
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
        
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
               
        </div>
 <div class="form-group row">
      <div class="col-md-12">
     <asp:HiddenField ID="hcontactid" runat="server" Value ="0" />
     <asp:Button ID="btnRemainder" runat="server" style="float:right;" Text="Add Contact" OnClick="btnRemainder_Click"></asp:Button>
          </div>
     </div>
    <asp:GridView ID="GridList" runat="server" OnPageIndexChanging="GridList_PageIndexChanging" OnRowCommand="GridList_RowCommand" PageSize="20" AllowPaging="true" AllowSorting="true" OnSorting="GridList_Sorting" >
        <Columns>
            <asp:TemplateField ItemStyle-Width="5%" >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit1" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Position" SortExpression="JobTitle">
                <ItemTemplate>
                    <asp:Label ID="lblJobTitle" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Email" SortExpression="EmailAddress">
                <ItemTemplate>
                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Phone no" SortExpression="TelephoneNo">
                <ItemTemplate>
                    <asp:Label ID="lblTelephoneNo" runat="server" Text='<%# Bind("TelephoneNo") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Mobile" SortExpression="MobileNo">
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MobileNo") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete1" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
      <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblStorageNew" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="460px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"> Add/Update Contact</h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
							</div>
						</div>
    <div class="panel-body">
     <div class="form-group row">
          <div class="col-md-12" >
     
               <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Name</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtName" runat="server" SkinID="txt_90" MaxLength="250"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtName"
                                        Display="None" ErrorMessage="Please enter name" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    
    
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Position</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtPosition" runat="server" SkinID="txt_90"></asp:TextBox>
               
            </div>
	</div>
</div>
   
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Email</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtEmail" runat="server" SkinID="txt_90"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage="Please enter email" ValidationGroup="vd"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="vd"></asp:RegularExpressionValidator>
            </div>
	</div>
</div>
               <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Phone</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtPhone" runat="server" SkinID="txt_90" MaxLength="50"></asp:TextBox>
                
               </div>
            </div>
	</div>
                <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Cell</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtMobile" runat="server" SkinID="txt_90" MaxLength="50"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobile"
                                        Display="None" ErrorMessage="Please enter cell" ValidationGroup="vd"></asp:RequiredFieldValidator>
                  
            </div>
              </div>
	</div>
              
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnSelect" runat="server" SkinID="btnSubmit" OnClick="btnSelect_OnClick" ValidationGroup="vd" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
        </div>
</asp:Panel>
    <asp:HiddenField ID="hid" runat="server" />

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
