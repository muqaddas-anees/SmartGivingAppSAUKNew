<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TypeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.TypeCtrl" %>
 <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static" SkinID="ddl_50">
       
    </asp:DropDownList>

<script type="text/javascript">
$(function() {
    $('#ddlType').change(function () {
        debugger;
        var v = $(this).val()
        if (v == 'quotationtemplate') {
            window.location.href = "QuoteTemplate.aspx?tab=fls&&type=" + $(this).val();
        }
        
        else {
            window.location.href = "FLSDefault.aspx?tab=fls&type=" + $(this).val();
        }

    })
})

</script>