<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Admin_QuoteAdmin" Codebehind="QuoteAdmin.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.QuoteAdmin%> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:Panel ID="pnlAdminEntry" runat="server">
     <div class="form-group">
             <div class="col-md-12">
                                      <asp:Label ID="lblmsg" runat='server' ForeColor="Red"></asp:Label>
                         <asp:ValidationSummary ID="valsum" runat='server' ForeColor="Red" ValidationGroup="ValAdd" />
                         <asp:HiddenField ID="HD_ID" runat="server" Value="0" />
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-5">
                                           <asp:DropDownList ID="ddlcustomer" runat="server" SkinID="ddl_90" AutoPostBack="true" 
                                 onselectedindexchanged="ddlcustomer_SelectedIndexChanged" ></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="Req1" ControlToValidate="ddlcustomer" runat='server'
                                 ErrorMessage="Please select a customer" Text="*" InitialValue="0" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.QuoteNumberPrefix%></label>
                                      <div class="col-sm-5">
                                           <asp:TextBox ID="txtprefix" runat="server" SkinID="txt_90"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req2" ControlToValidate="txtprefix" runat='server'
                                 ErrorMessage="Please enter Prefix" Text="*" InitialValue="0" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-12 control-label"> <%= Resources.DeffinityRes.StandardHeader %></label>
                                      <div class="col-sm-12">
                                           <asp:TextBox ID="txtheader" runat="server"  TextMode="MultiLine" Height="81px" SkinID="txtMulti_150" ></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req5" runat='server' ControlToValidate="txtheader" ErrorMessage="Please enter Header" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>

					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.QuoteStartPoint%></label>
                                      <div class="col-sm-5">  <asp:TextBox ID="txtstartpoint" runat="server" SkinID="txt_90"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req3" runat='server' ControlToValidate="txtstartpoint"
                                 ErrorMessage="Please enter Start Point Quote" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegEx2" runat="server" ControlToValidate="txtstartpoint" ValidationExpression="^\d+$"
                                     ErrorMessage="Please enter positive integers for Start point" Text="*" ValidationGroup="ValAdd">
                                 </asp:RegularExpressionValidator> 
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.VATRATEPercentage%></label>
                                      <div class="col-sm-5">
                                          <asp:TextBox ID="txtvat" runat="server" Text="17.5" SkinID="Price_100px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req4" runat='server' ControlToValidate="txtvat" ErrorMessage="Please enter VAT"
                                 Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Regex1" runat="server" ControlToValidate="txtvat" ValidationExpression="^\d*\.?\d*$"
                                     ErrorMessage="Please enter valid VAT" Text="*" ValidationGroup="ValAdd">
                                 </asp:RegularExpressionValidator>  
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-12 control-label"> <%= Resources.DeffinityRes.StandardFooter%></label>
                                      <div class="col-sm-12"><asp:TextBox ID="txtfooter" runat="server" Height="81px"  TextMode="MultiLine" 
                                 SkinID="txtMulti_150" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="Req6" runat='server' ControlToValidate="txtfooter" ErrorMessage="Please enter Footer" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.DefaultFolderName%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtfolder" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ContactName%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtcontactname" runat="server" SkinID="txt_90"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="Req7" runat="server" ControlToValidate="txtcontactname" ErrorMessage="Please enter Name" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Email%></label>
                                      <div class="col-sm-5">
                                          <asp:TextBox ID="txtemail" runat="server" SkinID="txt_90" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="Req8" runat="server" ControlToValidate="txtemail" ErrorMessage="Please enter Email" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="Reg1" runat='server' 
                             ControlToValidate="txtemail" 
                             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                             ErrorMessage="Enter valid Email" Text="*" ValidationGroup="ValAdd"></asp:RegularExpressionValidator>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ContactNumber%></label>
                                      <div class="col-sm-5">
                                          <asp:TextBox ID="txtcontactno" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                 <label class="col-sm-2 control-label"></label>
                 <div class="col-sm-9">
     <asp:Button ID="btnsubmit" runat="server" 
                             SkinID="btnSubmit" onclick="btnsubmit_Click" ValidationGroup="ValAdd" />
                     </div>
                 </div>
         </div>
    </asp:Panel>
</asp:Content>

