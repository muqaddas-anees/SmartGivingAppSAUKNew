<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceTypeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.MaintenanceTypeCtrl" %>
<%--<div class="form-group row">
        <div class="col-md-12">
           <strong> Maintenance Type </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>--%>
   
    <div class="form-group row">
        <div class="col-md-12">
             <asp:Label ID="lblMsg_a" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblError_a" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
             <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ValidationGroup="AInsertSum" />
<asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ValidationGroup="AUpdateSum" />
            </div>
                  </div>
     <div class="form-group row">
        <div class="col-md-6">
            
     
      <asp:ListView ID="listAppliances" runat="server" InsertItemPosition="FirstItem" OnItemCanceling="listAppliances_ItemCanceling" OnItemCommand="listAppliances_ItemCommand" OnItemDataBound="listAppliances_ItemDataBound" OnItemEditing="listAppliances_ItemEditing">
           <LayoutTemplate>
            <table style="width:100%" class="table table-small-font table-bordered table-striped datatable" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td>Maintenance Type</td>
                         <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
              </LayoutTemplate>
          <ItemTemplate>
              <tr class="even_row">  
                 <td>
                      <asp:Label ID="lblType" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                     
                 </td>
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("ID") %>' />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" />
                   </td>
                </tr>     
          </ItemTemplate>
          <EditItemTemplate>
              <tr >  
                 <td>
                     <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                     <asp:TextBox ID="txtName_e" runat="server"  Width="250px"  Text='<%# Eval("Name")%>'></asp:TextBox>
                    
                     <asp:RequiredFieldValidator  ID="RequiredFieldValidator2U" runat="server" ErrorMessage="Please enter maintenance type" ControlToValidate="txtName_e" Display="None" ValidationGroup="AUpdateSum"></asp:RequiredFieldValidator>
                 </td>
             
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnUpdate" runat="server" SkinID="BtnLinkUpdate" CommandName="UpdateItem" CommandArgument='<%# Eval("ID")%>' Text="Update" ValidationGroup="AUpdateSum" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" Text="Cancel" SkinID="BtnLinkCancel" />
                       
                   </td>
                </tr>   
          </EditItemTemplate>
          <InsertItemTemplate>
            <tr>  
                <td>
                     <asp:TextBox ID="txtName" runat="server"  Width="250px"></asp:TextBox>
                        
                     <asp:RequiredFieldValidator  ID="RequiredFieldValidator2F" runat="server" ErrorMessage="Please enter Maintenance Type" ControlToValidate="txtName" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>
                 </td>
              
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton  ID="btnAdd" runat="server" CommandName="Add" ValidationGroup="AInsertSum" Text="Add" SkinID="BtnLinkAdd" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" SkinID="BtnLinkCancel"  />
                   </td>
                </tr>    
          </InsertItemTemplate>
      </asp:ListView>
            </div>
                  </div>