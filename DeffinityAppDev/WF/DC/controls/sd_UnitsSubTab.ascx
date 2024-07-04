<%@ Control Language="C#"  AutoEventWireup="true" Inherits="Servicedesk_sdcontrols_sd_UnitsSubTab" Codebehind="sd_UnitsSubTab.ascx.cs" %>

 <div class="form-wizard">
  
 <ul id="countrytabs" class="tabs">
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnUnitStatus1"   Target="_self" runat="server" Text="Unit Status"></asp:HyperLink>
 </li><li class="ms-hover">
 <asp:HyperLink ID="lbtnUnitAdministration1"   Target="_self"  runat="server" Text="Unit Administration"></asp:HyperLink>
 </li>
 </ul>
 </div>
<script type="text/javascript">
    $(document).ready(function () {
        $(".tabs a").each(function (index, element) {
            if (index < 7)
                {
            var cu = $(location).attr('href').toLowerCase();
            var ck = $(element).attr('href').toLowerCase();
            if (cu.indexOf($.trim(ck)) > -1) {
                $(element).attr('class', 'active');
                $(element).parents('li').attr('class', 'active');
                return false;
            }
            }
        });
    });
</script>

