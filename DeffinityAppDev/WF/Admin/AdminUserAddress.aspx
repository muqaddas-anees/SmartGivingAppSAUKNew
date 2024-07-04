<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUserAddress" Title="Untitled Page" Codebehind="AdminUserAddress.aspx.cs" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="controls/MangeUserTab.ascx" TagName="MangeUserTab" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Manage&nbsp;Users&nbsp; - Address
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

   
    <div class="form-group row">
             <div class="col-md-12">
                 <%= Resources.DeffinityRes.UserAdminfor%>  <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
     <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
     <asp:Panel ID="pnlAddress" runat="server">
    <div class="form-group row">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="AddrsVldSummary" runat="server" />
                            <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                            <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
</div>
</div>
    <div class="form-group row">
             <div class="col-md-7">
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Address</label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtAddress1" runat="server" SkinID="txt_90"></asp:TextBox>
                                          <br />
                                          <asp:TextBox ID="txtAddress2" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
                  <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%--Town--%>State</label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txtTown" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
      <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.City%></label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtCounty" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
                  <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Zip code / Postcode </label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txtPostcode" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
      <div class="form-group row" style="display:none;visibility:hidden">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Country%></label>
                                      <div class="col-sm-7"><asp:DropDownList ID="ddlCountry" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
					</div>
				</div>
        </div>
      <div class="form-group row" style="display:none;visibility:hidden;">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Country%></label>
                                      <div class="col-sm-7">
                                          
					</div>
				</div>
        </div>
                  
     
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-7">
                                          <asp:Button ID="btnUpdate" runat="server" 
                                SkinID="btnUpdate" onclick="btnUpdate_Click" />
                                          </div>
                                      </div>
         </div>
    
   </div>
         <div class="col-md-5">
           
              <div class="form-group row">
        <div class="col-md-12">
             <asp:Label ID="lblMsg1" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblError1" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
             <asp:ValidationSummary ID="InsertSum1" runat="server" DisplayMode="List" ValidationGroup="InsertSum" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ValidationGroup="UpdateSum" />
            </div>
                  </div>
             <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label">Range in Kilometers Covered
                                           </label>
                                      <div class="col-sm-6 form-inline" >
                                          <asp:TextBox ID="txtRange" runat="server" SkinID="txt_100px" MaxLength="4"></asp:TextBox>
                                          <asp:Button ID="btnRange" runat="server" SkinID="btnSubmit" OnClick="btnRange_Click" />
                                          </div>
                                      </div>
                      </div>
              <div class="form-group row">
        <div class="col-md-12">
            
     
      <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="LastItem" OnItemCanceling="list_Customfields_ItemCanceling" 
          OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing"
          OnPagePropertiesChanging="OnPagePropertiesChanging">
           <LayoutTemplate>
            <table style="width:100%" class="table table-small-font table-bordered table-striped datatable" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td><asp:Literal runat="server" Text="Zip codes / Post codes Covered"></asp:Literal></td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>

                    <tr>
            <td colspan = "2">
                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="list_Customfields" PageSize="10">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                            ShowNextPageButton="false" />
                        <asp:NumericPagerField ButtonType="Link" />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
              </LayoutTemplate>
          <ItemTemplate>
              <tr class="even_row">  
                 <td>
                      <asp:Label ID="lblLable" runat="server" Text='<%# Eval("Postcode")%>'></asp:Label>
                 </td>
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" ImageAlign="AbsMiddle"/>
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" ImageAlign="AbsMiddle" />
                   </td>
                </tr>     
          </ItemTemplate>
          <EditItemTemplate>
              <tr >  
                 <td>
                     <asp:TextBox ID="txtLable" runat="server" Text='<%# Eval("Postcode") %>' Width="150px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter postcode/zipcode" ControlToValidate="txtLable" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                 </td>
              
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnUpdate" runat="server" SkinID="BtnLinkUpdate" CommandName="UpdateItem" CommandArgument='<%# Eval("ID")%>' Text="Update" ValidationGroup="UpdateSum" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" Text="Cancel" SkinID="BtnLinkCancel" ImageAlign="AbsMiddle" />
                       
                   </td>
                </tr>   
          </EditItemTemplate>
          <InsertItemTemplate>
            <tr>  
                 <td>
                      <asp:TextBox ID="txt_insert_lable" runat="server" ValidationGroup="InsertSum" Width="150px" MaxLength="50"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rqDescription" runat="server" ErrorMessage="Please enter postcode/zipcode" ControlToValidate="txt_insert_lable" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
                 </td>
               
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton  ID="btnAdd" runat="server" CommandName="Add" ValidationGroup="InsertSum" Text="Add" SkinID="BtnLinkAdd" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" SkinID="BtnLinkCancel"  />
                   </td>
                </tr>    
          </InsertItemTemplate>
      </asp:ListView>
            </div>
                  </div>
             </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlCategory" runat="server" Visible="false">
    <div class="form-group row">
        <div class="col-md-12">
           <strong> Services Covered </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
   
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
                        <td>Services Types</td>
                        <td>Make</td>
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
                      <asp:Label ID="lblType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                     
                 </td>
                   <td>
                      <asp:Label ID="lblMake" runat="server" Text='<%# Eval("MakeName")%>'></asp:Label>
                 </td>
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("ID") %>' />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" ImageAlign="AbsMiddle" />
                   </td>
                </tr>     
          </ItemTemplate>
          <EditItemTemplate>
              <tr >  
                 <td>
                     <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                     <asp:DropDownList ID="ddlType" runat="server"  Width="150px"></asp:DropDownList>
                     <ajaxToolkit:CascadingDropDown ID="ccdCategoryNew" runat="server" TargetControlID="ddlType"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading ...]"  />
                     <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator2U" runat="server" ErrorMessage="Please select services types" ControlToValidate="ddlType" Display="None" ValidationGroup="AUpdateSum" Width="225px"></asp:RequiredFieldValidator>
                 </td>
               <td>
                     <asp:DropDownList ID="ddlMake" runat="server"  Width="150px"></asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdSubCategoryNew" runat="server" TargetControlID="ddlMake"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlType" />
                    <%-- <asp:RequiredFieldValidator InitialValue="0"  ID="RequiredFieldValidator1U" runat="server" ErrorMessage="Please enter make" ControlToValidate="ddlMake" Display="None" ValidationGroup="AUpdateSum" Width="225px"></asp:RequiredFieldValidator>--%>
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
                     <asp:DropDownList ID="ddlTypeF" runat="server"  Width="150px"></asp:DropDownList>
                     <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlTypeF"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading ...]"  />
                  
                     <asp:RequiredFieldValidator InitialValue="0"  ID="RequiredFieldValidator2F" runat="server" ErrorMessage="Please select services types" ControlToValidate="ddlTypeF" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>
                 </td>
               <td>
                     <asp:DropDownList ID="ddlMakeF" runat="server"  Width="150px"></asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlMakeF"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlTypeF" />
                     <%--<asp:RequiredFieldValidator InitialValue="0"  ID="RequiredFieldValidator1F" runat="server" ErrorMessage="Please select make" ControlToValidate="ddlMakeF" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>--%>
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
        </asp:Panel>
</asp:Content>
