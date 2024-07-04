<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ChangeControlDashBorad" Codebehind="ChangeControlDashBorad.aspx.cs" %>

<%@ Register src="controls/changecontrol_summarytab.ascx" tagname="changecontrol_summarytab" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Dashboard
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:changecontrol_summarytab ID="changecontrol_summarytab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
   <asp:UpdatePanel ID="Updatepanel1" runat="server">
<ContentTemplate>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList 
        ID="ddlCustomer" runat="server" SkinID="ddl_90" 
        onselectedindexchanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Select type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlDropDown" 
        runat="server" AutoPostBack="True" Width="550px" 
        onselectedindexchanged="ddlView_SelectedIndexChanged">
        <%--<asp:ListItem Value="1" Text="Completed Requests per Week by Site"></asp:ListItem>
        <asp:ListItem Value="2" Text="Stacked Categories against Site for Completed Reqests"></asp:ListItem>--%>
        <asp:ListItem Value="1" Text="Volume of Completed Requests by category"></asp:ListItem>
        <asp:ListItem Value="2" Text="Volume of Completed Requests by Category Year on Year"></asp:ListItem>
        <asp:ListItem Value="3" Text="Volume of Requests by Area Stacked by Category"></asp:ListItem>
        <asp:ListItem Value="4" Text="Journal of Change Controls Logged (completed as well as active)"></asp:ListItem>
        </asp:DropDownList>
            </div>
	</div>
</div>

<div style="margin-top:10px"><iframe id="iFrameSetUrl" runat="server" width="100%" frameborder="0" scrolling="auto"></iframe>  </div>

</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlDropDown" EventName="SelectedIndexChanged" />
<asp:AsyncPostBackTrigger ControlID="ddlCustomer" EventName="SelectedIndexChanged" />
</Triggers>
</asp:UpdatePanel>

 <script language="javascript" type="text/javascript">
     function iFrameHeight() {
         if (document.getElementById && !(document.all)) {
             h = document.getElementById("<%=iFrameSetUrl.ClientID%>").contentDocument.body.scrollHeight + 100;
             document.getElementById("<%=iFrameSetUrl.ClientID%>").style.height = h + "px";
         }
         else if (document.all) {
             h = document.frames("<%=iFrameSetUrl.ClientID%>").document.body.scrollHeight + 30;
             //document.all.iframename.style.height = h + "px";
             document.getElementById("<%=iFrameSetUrl.ClientID%>").style.height = h + "px";
         }
     }
    </script>

    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(iFrameHeight);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
 </script> 
</asp:Content>


