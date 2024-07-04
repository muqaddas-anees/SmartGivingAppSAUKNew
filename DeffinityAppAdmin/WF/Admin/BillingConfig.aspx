<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="BillingConfig.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.BillingConfig" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
      <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Billing Plan 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
      <div class="form-group">
      <div class="col-md-12">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
              </div>
     <div class="form-group">
      <div class="col-md-6 form-inline">

          <label>Partner:</label> <asp:DropDownList ID="ddlPartner" runat="server" SkinID="ddl_80" AutoPostBack="true" OnSelectedIndexChanged="ddlPartner_SelectedIndexChanged"></asp:DropDownList>
         

          </div>
         </div>

      <div class="form-group">
      <div class="col-md-6 form-inline">
           <label> Trail Period:</label> <asp:TextBox ID="txtDays" runat="server" SkinID="Price_100px" MaxLength="4"></asp:TextBox>  <label>Day(s)</label>
          <asp:Button ID="btnUpdatePartner" runat="server" SkinID="btnSave" OnClick="btnUpdatePartner_Click1" ></asp:Button>
          </div>
          </div>
    <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <div class="form-group ">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
              <div class="col-md-4">
              <div class="well" style="min-height:400px;">
                   <div class="form-group" style="text-align:right;margin-bottom:-5px;">
       
            <p style="font-size:15px;text-align:right;">
           
                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                </p>
           
                        </div>

                     <div class="form-group">
        <div class="col-md-12" style="padding-left:0px;padding-right:0px;padding-top:5px">
                   <asp:Button ID="btnItemView" runat="server" Text='<%# Eval("PlanName") %>' SkinID="btnDefault" CommandName="Item" CommandArgument='<%# Eval("ID") %>' CausesValidation="false" style="float:left;width:100%" ></asp:Button>
                 
            </div>
                         </div>
          
                   <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> Monthly</label>
          <div class="col-sm-6">
              <asp:TextBox ID="txtMonth" runat="server" SkinID="Price_125px" MaxLength="10" Text='<%# Eval("MonthlyPrice") %>'></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterPrice" runat="server" ValidChars="0123456789." TargetControlID="txtMonth"></ajaxToolkit:FilteredTextBoxExtender>
              </div>
          </div>
              </div>
                   <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> Yearly</label>
          <div class="col-sm-6">
              <asp:TextBox ID="txtYear" runat="server" SkinID="Price_125px" MaxLength="10" Text='<%# Eval("YearlyPrice") %>'></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789." TargetControlID="txtYear"></ajaxToolkit:FilteredTextBoxExtender>
              </div>
          </div>
                        
              </div>
                   <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> Active</label>
          <div class="col-sm-6">
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' />
              </div>
          </div>
                             </div>
                  <div class="form-group" style="display:none;visibility:hidden;">
                       <div class="col-md-6">
                          
                           </div>
                       <div class="col-md-6" style="text-align:right;">
                          
                           </div>
                      </div>
                   
                    <div class="form-group" style="margin-bottom:0px">
        <div class="col-md-12 text-center alert alert-info" style="padding:5px;margin-bottom:10px">
        <strong> <asp:HyperLink ID="hLinkFinnace" runat="server"  Text="Modules" ForeColor="White" Target="_blank"></asp:HyperLink> </strong>
            
            </div>
    </div>
                  
             <%--    
                  <div class="form-group" style="height:150px;overflow-y:auto;overflow-x:hidden;">
                       <%--<div class="col-md-12">
                           <ul>
                           <asp:Label ID="lblItemsList" runat="server" Text='<%# Eval("ItemsList") %>'></asp:Label>
                               </ul>
                           <%--</div>
                       </div>--%>
                   <div class="form-group">
                       <div class="col-md-12 form-inline" style="text-align:center;">
                           
                          <asp:GridView ID="gv" runat="server" Width="100%" ShowHeader="false">
                              <Columns>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:CheckBox runat="server" ID="chk" />
                                          <asp:Label ID="lblModuleID" runat="server"  Text='<%# Eval("ModuleID") %>' Visible="false"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                      <ItemTemplate>
                                          <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("ModuleName") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                       </div>
                       </div>
                
    </div>
                  </div>
              </ItemTemplate>
               </asp:ListView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
