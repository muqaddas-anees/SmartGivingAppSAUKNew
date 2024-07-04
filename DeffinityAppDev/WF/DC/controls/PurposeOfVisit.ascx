<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_PurposeOfVisit" Codebehind="PurposeOfVisit.ascx.cs" %>

<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.PurposeofVisit%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server"></asp:Label>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.VisitingPurpose%></label>
           <div class="col-sm-8">
               
<asp:Panel ID="pnlnames" runat="server" Width="100%">
    <div class="row">
          <div class="col-md-12 form-inline">
               <asp:DropDownList ID="ddlNames" runat="server" SkinID="ddl_80"></asp:DropDownList>
               <asp:LinkButton ID="imgbtnDelete" runat="server" Text="Delete" SkinID="BtnLinkDelete" onclick="imgbtnDelete_Click"  OnClientClick="javascript:return confirm('Are you sure to delete the record?');" />

              </div>
        </div>
     
               <div class="row">
          <div class="col-md-12">
               <asp:Button ID="imgbtnAdd" runat="server" Text="Add" SkinID=""
              onclick="imgbtnAdd_Click" />
        <asp:Button ID="imgbtnEdit" runat="server" Text="Edit" onclick="imgbtnEdit_Click" SkinID="btnEdit"  />
              <asp:HiddenField ID="h_nameid" runat="server" Value="0" />
              </div>
                   </div>
      

   
        </asp:Panel>
               <asp:Panel ID="pnladd" runat="server">
                   <div class="row">
          <div class="col-md-12">
        <asp:TextBox ID="txtadd" runat="server" SkinID="txt_80"></asp:TextBox><asp:RequiredFieldValidator ID="rfvemail" runat="server" 
                    ErrorMessage="Please enter Visiting Purpose" 
                    ControlToValidate="txtadd" Display="Dynamic" SetFocusOnError="True" 
                    ValidationGroup="p"></asp:RequiredFieldValidator><br />
              </div>
                       </div>
        
      <div class="row">
          <div class="col-md-12">
          <asp:Button ID="imgbtnSubmit" ValidationGroup="p" runat="server" Text="Submit" SkinID="btnSubmit" onclick="imgbtnSubmit_Click" />&nbsp;
        <asp:Button ID="imgbtncnl" runat="server" Text="Cancel" onclick="imgbtncnl_Click" SkinID="btnCancel" />
              </div>

      </div>
        </asp:Panel>
            </div>
	</div>
</div>

        
