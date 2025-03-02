﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
         AutoEventWireup="true" Inherits="App_AppChildForm" Codebehind="AppChildForm.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
      <ul class="tabs_list5" style="float:right;">
    <li class="current5"><a id ="link_return" href="~/DC/AppFormList.aspx?Appid=0" runat="server" target="_self"><span>Return to App list</span></a></li></ul>

     <style>
        .Imagespace {
            margin-left: 5px;
            vertical-align:bottom;
            
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    
<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
    <script type="text/javascript" src="jQuery.print.js"></script>
    
    
  <%--<script src="https://code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>--%>
 <%-- <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.js" type="text/javascript"></script>--%>
      <script language="javascript" type="text/javascript">
        $(document).ready(function () {
           // $(document.body).find("[id$='lblPageHead']").html('Form');

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
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section1">
              <span>
                  <label id="lblTitle" runat="server"> 
                  </label> form
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block" style="width:100%">
        <div>
            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green" ClientIDMode="Static"></asp:Label> 
        </div>
       <div id="pnlForm" runat="server"></div>

        <div id="pnlFormData" runat="server" style="margin-left:5px;margin-right:1px;">
             <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
              <table  width="100%">
                            <tr align="right">
                                <td>
                                    <asp:Button ID="BtnPrint" runat="server" Text="Print" Visible="false"/>
                                </td>
                            </tr>
                </table>
                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
                              <asp:PlaceHolder ID="ph" runat="server">
                </asp:PlaceHolder>
                        </ContentTemplate>
              </asp:UpdatePanel>
             <table width="100%">
        <tr align="right">
            <td style="padding-right:30px;">
                <asp:ImageButton ID="btnSubmitChanges" runat="server" SkinID="ImgSave"
                     ValidationGroup="Form" OnClick="btnSubmitChanges_Click" />
            </td>
        </tr>
    </table>
        </div>
       
        </td>
      </tr>
         </table>
       <script type="text/javascript">
           //apply date 
           applyDatePicker();
    </script>
</asp:Content>

