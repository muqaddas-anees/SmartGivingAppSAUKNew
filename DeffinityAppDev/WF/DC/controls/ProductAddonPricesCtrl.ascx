<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAddonPricesCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.ProductAddonPricesCtrl" %>
  <%--<div class="form-group row">
        <div class="col-md-12">
           <strong> Optional Add on Prices </strong> 
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
        <div class="col-md-12">
            
     
      <asp:ListView ID="listAppliances" runat="server" InsertItemPosition="LastItem" OnItemCanceling="listAppliances_ItemCanceling" OnItemCommand="listAppliances_ItemCommand" OnItemDataBound="listAppliances_ItemDataBound" OnItemEditing="listAppliances_ItemEditing">
           <LayoutTemplate>
            <table style="width:100%" class="table table-small-font table-bordered table-striped datatable" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td>Add on</td>
                        <td>Type</td>
                        <td>Montly Cost</td>
                        <td>Yearly Cost</td>
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
                      <asp:Label ID="lblType" runat="server" Text='<%# Eval("AddOnDetails")%>'></asp:Label>
                     
                 </td>
                  <td>
                      <asp:Label ID="lblptype" runat="server" Text='<%# Eval("Ptype")%>'></asp:Label>
                  </td>
                   <td style="text-align:right;">
                      <asp:Label ID="lblMontlyCost" runat="server" Text='<%# Eval("MontlyCost")%>'></asp:Label>
                 </td>
                  <td style="text-align:right;">
                      <asp:Label ID="txtYearlyCost" runat="server" Text='<%# Eval("YearlyCost")%>'></asp:Label>
                  </td>
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("PAPID") %>' />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("PAPID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" ImageAlign="AbsMiddle" />
                   </td>
                </tr>     
          </ItemTemplate>
          <EditItemTemplate>
              <tr >  
                 <td>
                     <asp:Label ID="lblID" runat="server" Text='<%# Eval("PAPID")%>' Visible="false"></asp:Label>
                     <asp:TextBox ID="txtAddOnDetails" runat="server"  Width="150px"  Text='<%# Eval("AddOnDetails")%>'></asp:TextBox>
                    
                     <asp:RequiredFieldValidator  ID="RequiredFieldValidator2U" runat="server" ErrorMessage="Please enter Add on" ControlToValidate="txtAddOnDetails" Display="None" ValidationGroup="AUpdateSum" Width="225px"></asp:RequiredFieldValidator>
                 </td>
                  <td>
                      <asp:DropDownList ID="ddlType" runat="server">
                         
                      </asp:DropDownList>
                  </td>
               <td>
                     <asp:TextBox ID="txtMontlyCost" runat="server" SkinID="Price_125px" MaxLength="10" Text='<%# Eval("MontlyCost")%>'></asp:TextBox>
                    
                 </td>
                   <td>
                     <asp:TextBox ID="txtYearlyCost" runat="server"  SkinID="Price_125px" MaxLength="10" Text='<%# Eval("YearlyCost")%>'></asp:TextBox>
                   
                 </td>
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnUpdate" runat="server" SkinID="BtnLinkUpdate" CommandName="UpdateItem" CommandArgument='<%# Eval("PAPID")%>' Text="Update" ValidationGroup="AUpdateSum" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" Text="Cancel" SkinID="BtnLinkCancel" />
                       
                   </td>
                </tr>   
          </EditItemTemplate>
          <InsertItemTemplate>
            <tr>  
                <td>
                     <asp:TextBox ID="txtAddOnDetailsF" runat="server"  Width="150px"></asp:TextBox>
                        
                     <asp:RequiredFieldValidator  ID="RequiredFieldValidator2F" runat="server" ErrorMessage="Please enter Add on" ControlToValidate="txtAddOnDetailsF" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>
                 </td>
                <td>
                      <asp:DropDownList ID="ddlTypeF" runat="server" >
                                               </asp:DropDownList>
                  </td>
               <td>
                     <asp:TextBox ID="txtMontlyCostF" runat="server"   SkinID="Price_125px" MaxLength="10"></asp:TextBox>
                   </td>
                 <td>
                     <asp:TextBox ID="txtYearlyCostF" runat="server"   SkinID="Price_125px" MaxLength="10"></asp:TextBox>
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