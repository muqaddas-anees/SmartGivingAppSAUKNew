<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectCheckpointForm" Codebehind="ProjectCheckpointForm.aspx.cs" %>

<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc1" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Checkpoint%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Form%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <uc1:OpsViewTabs ID="OpsViewTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <%--  <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
  <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript"></script>--%>
  <%--<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>--%>
   <%-- <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <%-- <link rel="stylesheet" href="stylcss/HCstyle.css"/>--%>

    <script type="text/javascript" src="../../Health/HC/jQuery.print.js"></script>
   
     
<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>

    
<div class="form-group">
      <div class="col-md-2">
          Tasks
	</div>
	<div class="col-md-4">
           <asp:DropDownList ID="ddlTasks" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlTasks_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlTasks"
                    runat="server" Display="None" ErrorMessage="Please select a task" InitialValue="0"
                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div id="pnlFormData" runat="server" visible="false" style="margin-left:5px;margin-right:1px;">
        
<div class="row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
	</div>
</div>
       <div class="row pull-right" style="margin-bottom:5px;">
          <div class="col-md-12 pull-right" style="float:right;">
              <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click"/>
	</div>
</div>     
           
           <div style="padding:5px 5px 5px 5px">
               
                      <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
         <asp:PlaceHolder ID="ph" runat="server">
                </asp:PlaceHolder>
                        </ContentTemplate>
              </asp:UpdatePanel>
               
           </div>
              
            
         <div class="row">
          <div class="col-md-12 pull-right" style="float:right;">
              <asp:Button ID="btnSubmitChanges" runat="server" SkinID="btnSave"
                    OnClick="btnSubmitChanges_Click" ValidationGroup="Form" />
              </div>
             </div>
        </div>
    <script type="text/javascript">
        activeTab('Form');
    </script>
</asp:Content>


