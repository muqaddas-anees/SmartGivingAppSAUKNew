<%@ Control Language="C#" AutoEventWireup="true" Inherits="Training_dropdownView" Codebehind="dropdownView.ascx.cs" %>

 <%-- <label class="col-sm-4 control-label">Select View </label>
     <div class="col-sm-8 form-inline">--%>
    <asp:DropDownList ID="ddlView" 
        runat="server" AutoPostBack="True" 
    onselectedindexchanged="ddlView_SelectedIndexChanged" >
</asp:DropDownList>
        <%-- </div>--%>


