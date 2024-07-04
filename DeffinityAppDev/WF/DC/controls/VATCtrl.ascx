<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VATCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.VATCtrl" %>
<%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="lblHeader" runat="server" Text="TAX"></asp:Label></strong>
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
                    ErrorMessage="Please enter VAT (%)" 
                    ControlToValidate="txtVatPercent" Display="Dynamic" SetFocusOnError="True" 
                    ValidationGroup="valsumSupportMailid"></asp:RequiredFieldValidator>
   
    </div>

<div class="form-group row mb-6">
            
                                       <label class="col-sm-2 control-label"><asp:Label ID="lblEmail" runat="server" Text="VAT (%)"></asp:Label></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtVatPercent" runat="server" SkinID="Price_150px" MaxLength="5"></asp:TextBox>
					</div>
				
                </div>
<div class="form-group row mb-6">
             
                 <label class="col-sm-2 control-label"></label>
                  <div class="col-sm-8 form-inline">
                     
        <asp:LinkButton ID="imgbtnupdateemail" runat="server" SkinID="btnUpdate" ValidationGroup="valsumSupportMailid"
        onclick="imgbtnupdateemail_Click" />
       
        
                  </div>

                
    </div>
                                        </ContentTemplate>
    </asp:UpdatePanel>