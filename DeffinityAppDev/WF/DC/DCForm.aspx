<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="DC_DCForm" Codebehind="DCForm.aspx.cs" %>
<%@ Register Src="~/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
     <link rel="stylesheet" href="../stylcss/HCstyle.css"/>

     <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="jQuery.print.js"></script>
    <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>
      <link rel="stylesheet" href="../stylcss/HCstyle.css"/>
    <script type="text/javascript" src="../Scripts/HCform.js"></script>

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(document.body).find("[id$='lblPageHead']").html('Service Desk');

            $('#lblMsg').fadeOut(2000);
            $("li[id$='form']").addClass('current1');
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
<uc2:FlsTab ID="flstab1" runat="server" />
    <ul class="tabs_list5" style="float:right;">
    <li class="current5"><a id ="link_return" href="~/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><span>Return to Ticket Journal</span></a></li>
   
</ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section1">
              <span>
                  <label id="lblTitle" runat="server"> Form
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block" style="width:100%">
        <div><asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green" ClientIDMode="Static"></asp:Label> </div>
       <div id="pnlForm" runat="server"> <asp:DropDownList ID="ddlForms" runat="server" Width="250px"></asp:DropDownList> 
           <asp:Button ID="btnApply" runat="server" Text="Apply Form" OnClick="btnApply_Click" />  </div>
        <div id="pnlFormData" runat="server" visible="false" style="margin-left:5px;margin-right:1px;">
             <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
         <asp:PlaceHolder ID="ph" runat="server">
                </asp:PlaceHolder>
                        </ContentTemplate>
              </asp:UpdatePanel>
             <table width="100%">
        <tr align="right">
            <td style="padding-right:30px;">
                <asp:Button ID="btnSubmitChanges" runat="server" SkinID="ImgSave"
                    OnClick="btnSubmitChanges_Click" ValidationGroup="Form" />
            </td>
        </tr>
    </table>

        </div>
       
        </td>
      </tr>
         </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

