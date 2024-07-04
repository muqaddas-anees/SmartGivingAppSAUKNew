<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_AccessEmailId" Codebehind="AccessEmailId.ascx.cs" %>
<%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="lblHeader" runat="server"></asp:Label></strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
<asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="upnlemail">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlemail" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
<div class="form-group row mb-6">
    <asp:Label ID="lblsuccessemail" SkinID="GreenBackcolor" EnableViewState="false" runat="server"></asp:Label>
    <asp:Label ID="lblerror" SkinID="RedBackcolor" EnableViewState="false" runat="server"></asp:Label>
    <asp:RequiredFieldValidator ID="rfvemail" runat="server" 
                    ErrorMessage="Please enter email address." 
                    ControlToValidate="txtemail" Display="Dynamic" SetFocusOnError="True" 
                    ValidationGroup="valsumSupportMailid"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="rgemail" runat="server" 
                    ControlToValidate="txtemail" Display="Dynamic" ErrorMessage="Invalid email." SetFocusOnError="True" 
                    ValidationExpression=  "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="valsumSupportMailid"></asp:RegularExpressionValidator>
    </div>

<div class="form-group row mb-6">
            
                                       <label class="col-sm-4 control-label"><asp:Label ID="lblEmail" runat="server"></asp:Label></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
					</div>
				
                </div>
<div class="form-group row mb-6">
             <div class="col-lg-12">
                 <label class="col-sm-4 control-label"></label>
                  <div class="col-sm-8 form-inline">
                      <asp:Button ID="imgbtnaddmail" runat="server" SkinID="btnAdd" 
        onclick="imgbtnaddmail_Click"  ValidationGroup="valsumSupportMailid" />
        <asp:Button ID="imgbtnupdateemail" runat="server" SkinID="btnUpdate" ValidationGroup="valsumSupportMailid"
        onclick="imgbtnupdateemail_Click" />
        <asp:LinkButton ID="imgbtndel" runat="server" SkinID="BtnLinkDelete"
        onclick="imgbtndel_Click" OnClientClick="javascript:return confirm('Are you sure to delete?');" Visible="false" />
        <asp:Button ID="btnCopyToAllCustomers"  runat="server" SkinID="btnCopytoAllCustomers"  OnClick="btnCopyToAllCustomers_Click" Visible="false" />
                  </div>

                 </div>
    </div>
                                        </ContentTemplate>
    </asp:UpdatePanel>

    