<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_SADropDown" Codebehind="SADropDown.ascx.cs" %>
<div align="right">
<asp:DropDownList ID="ddlAdmin" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmin_SelectedIndexChanged">
 <asp:ListItem Value="accesscontrol" Text="Access Control"></asp:ListItem>
  <asp:ListItem Value="delivery" Text="Delivery"></asp:ListItem>
 <%--  <asp:ListItem Value="fls" Text="FLS"></asp:ListItem>--%>
    <asp:ListItem Value="permittowork" Text="Permit to Work"></asp:ListItem>
    <asp:ListItem Value="securityaccess" Text="Security Access"></asp:ListItem>
</asp:DropDownList>
<script src="../js/Utility.js" type="text/javascript" language="javascript"></script>
<script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        var qval = getQuerystring('tab');
        //alert(qval);
        if (qval.toLowerCase() == 'fls') {
            $("#a_backlink").attr("href", "FLSJlist.aspx?type=FLS");
            $("#a_backlink").html("<b> back to FLS - Ticket journal</b>");
        }
        else if (qval.toLowerCase() == 'delivery') {
            $("#a_backlink").attr("href", "FLSJlist.aspx?type=Delivery");
            $("#a_backlink").html("<b> back to Delivery - Ticket journal</b>");
        }
        else if (qval.toLowerCase() == 'accesscontrol') {
            $("#a_backlink").attr("href", "FLSJlist.aspx?type=Accesscontrol");
            $("#a_backlink").html("<b> back to Access Control - Ticket journal</b>");
        }
        else if (qval.toLowerCase() == 'permittowork') {
            $("#a_backlink").attr("href", "FLSJlist.aspx?type=Permittowork");
            $("#a_backlink").html("<b> back to Permit to Work - Ticket journal</b>");
        }
        else {
            $("#a_backlink").attr("href", "FLSJlist.aspx?type=FLS");
            $("#a_backlink").html("<b> back to FLS - Ticket journal</b>");
        }

    });


</script>
<a id="a_backlink"></a>
</div>

