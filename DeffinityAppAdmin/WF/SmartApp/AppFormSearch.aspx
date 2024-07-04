<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="App_AppFormSearch" Codebehind="AppFormSearch.aspx.cs" %>
<%@ Register Src="Controls/apptabs.ascx" TagPrefix="app" TagName="apptabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <app:apptabs runat="server" ID="apptabs" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
     Smart Apps
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server"> Search</label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
    
    <%-- <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="jQuery.print.js"></script>
   <%-- <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>
      <link rel="stylesheet" href="../stylcss/HCstyle.css"/>
    <script type="text/javascript" src="../Scripts/HCform.js"></script>--%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
 <%-- <script src="https://code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>
  <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" type="text/javascript"></script>--%>
      <script language="javascript" type="text/javascript">
        $(document).ready(function () {
           //$(document.body).find("[id$='lblPageHead']").html('Search');

            $('#lblMsg').fadeOut(2000);
            //$("li[id$='form']").addClass('current1');
        });
          
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var CallId = GetParameterValues('callid');
            GetCallId(CallId);
        });
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        function GetCallId(CallId) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetCallId?CallId" + CallId,
                type: "POST",
                data: "{'CallId': '" + CallId + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        GetTextboxId(obj);
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
        function GetTextboxId(panelid) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetTextBoxId?pid" + panelid,
                type: "POST",
                data: "{'panelid': '" + panelid + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        if (obj != '')
                            debugger;
                        $.each(obj, function () {
                            if ($("#ctl00_ctl00_MainContent_MainContent_" + this.CntlID + "").val() =='')
                            {
                                $("#ctl00_ctl00_MainContent_MainContent_" + this.CntlID + "").datepicker({ dateFormat: 'dd/mm/yy' });
                              }
                        });
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
    </script>
     <div>
         <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green" ClientIDMode="Static"></asp:Label> 
     </div>
     <div id="pnlForm" runat="server"></div>    
      
        <div id="pnlFormData" runat="server">
            <div class="form-group">
             <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
                </div>
               <div class="form-group">
             <asp:UpdateProgress ID="uprogress" runat="server">
                 <ProgressTemplate>
                     <asp:Label SkinID="Loading" runat="server"></asp:Label>
                 </ProgressTemplate>
             </asp:UpdateProgress>
                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder> 

                    </ContentTemplate>
              </asp:UpdatePanel>
                   </div>
              <div class="form-group">
                   <div class="col-md-12 form-inline">
                       <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" CausesValidation="false" />
                       <asp:Button runat="server" Text="Excel" ID="btnExport" OnClick="btnExport_Click"  CausesValidation="false" />
                   </div>
                  </div>
             <div class="form-group">
                 <asp:Panel ID="gridpnl" runat="server">
                        <asp:GridView ID="GridResult" runat="server" 
                            AutoGenerateColumns="true" Width="100%" OnPreRender="GridResult_PreRender" SkinID="Custom_Grid"></asp:GridView>
                              </asp:Panel>
             </div>
        </div>
    <script type="text/javascript">
        //apply date 
        applyDatePicker();
    </script>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

