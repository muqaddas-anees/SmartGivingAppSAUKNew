<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DefaultSettings.aspx.cs" Inherits="DeffinityAppDev.App.DefaultSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Default Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Site Name</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtSiteName" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
     <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Site URL</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtSiteUrl" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
      <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Country</label>
                                      <div class="col-sm-10 form-inline">
                                  <asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>    
                    
					</div>
				
</div>
      <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Country Code</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtCountryCode" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
      <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">City Display</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtCity" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
      <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">State Display</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtState" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
      <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Postcode</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtPostcode" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
     <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Currency Setting</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:DropDownList ID="ddlCulture" runat="server">
                                       <asp:ListItem Text="USA"  Value="en-US"></asp:ListItem>
                                       <asp:ListItem Text="UK" Value="en-GB"></asp:ListItem>
                                       <asp:ListItem Text="SA" Value="af-ZA"></asp:ListItem>
                                   </asp:DropDownList>   
                    
					</div>
				
</div>

    <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" />

                                          </div>
        </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    
</asp:Content>
