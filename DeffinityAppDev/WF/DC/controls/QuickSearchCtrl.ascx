<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_QuickSearchCtrl" Codebehind="QuickSearchCtrl.ascx.cs" %>
<%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
 <script type="text/javascript">
     $(document).ready(function () {
         $("#btnHide").click(function () {
             $("#QSearch").hide();
             $("#BtnS").show();
             return false;
         });
         var sname = GetParameterValues('S');
         function GetParameterValues(param) {
             var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
             for (var i = 0; i < url.length; i++) {
                 var urlparam = url[i].split('=');
                 if (urlparam[0] == param) {
                     return urlparam[1];
                 }
             }
         }
         if (sname != undefined) {
             $.ajax({
                 url: "../webservices/DCServices.asmx/QuickSearchResult",
                 type: "Post",
                 data: "{'stringValue': '" + sname + "'}",
                 dataType: "json",
                 contentType: 'application/json; charset=utf-8',
                 async: true,
                 success: function (data) {
                     var xmlDoc = $.parseJSON(data.d);
                     //   var tblText = "<table><tr>";
                     var NewText = "";
                     $.each(xmlDoc, function () {
                         //   debugger;
                         NewText += "<div><div id='" + this.CallID + "'> <span style='font-weight:bold;'> Ticket Ref:</span><a href='FLSForm.aspx?callid=" + this.CallID + "&S=" + sname + "'>" + this.CallID + "</a></div><div id='" + this.Company + "'><span style='font-weight:bold;'>Company: </span>" + this.Company + "</div><div id='" + this.Details + "'><span style='font-weight:bold;'> Description:</span> " + this.Details + "</div></div><div style='border:1px solid;width:130px;overflow:hidden;white-space:nowrap;color:green'></div>";
                     });
                     tblText = "<table><tr><td><div style='overflow-y:scroll;max-height:390px; max-width:180px'> " + NewText + "</div></td></tr></table>";
                     debugger;
                     $("#Callid").html(tblText);
                     $('#QSearch').show();
                     $('#BtnS').hide();
                     debugger;
                 },
                 error: function (msg) { setMsg(Error); }
             });
         }
         $("#BtnQuickSearch").click(function () {
             $("#DivQS").show();
             var stringValue = $("#Txtsearch").val().trim();
             if (stringValue != '') {
                 $.ajax({
                     url: "../webservices/DCServices.asmx/QuickSearchResult",
                     type: "Post",
                     data: "{'stringValue': '" + stringValue + "'}",
                     dataType: "json",
                     contentType: 'application/json; charset=utf-8',
                     async: true,
                     success: function (data) {
                         var xmlDoc = $.parseJSON(data.d);
                         //   var tblText = "<table><tr>";
                         var NewText = "";
                         $.each(xmlDoc, function () {
                             NewText += "<div><div id='" + this.CallID + "'> <span style='font-weight:bold;'> Ticket Ref:</span><a href='FLSForm.aspx?callid=" + this.CallID + "&S=" + stringValue + "'>" + this.CallID + "</a></div><div id='" + this.Company + "'><span style='font-weight:bold;'>Company: </span>" + this.Company + "</div><div id='" + this.Details + "'><span style='font-weight:bold;'> Description:</span> " + this.Details + "</div></div><div style='border:1px solid;width:150px;overflow:hidden;white-space:nowrap;color:gray;margin-bottom:10px;'></div>";
                         });
                         tblText = "<table><tr><td><div style='overflow-y:scroll;height:390px;width:192px'> " + NewText + "</div></td></tr></table>";
                         debugger;
                         $("#Callid").html(tblText);
                         debugger;
                     },
                     error: function (msg) { setMsg(Error); }
                 });
             }
             return false;
         });

     });
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#QSearch").hide();
            $("#BtnS").click(function () {
                $('#QSearch').show();
                $('#BtnS').hide();
                return false;
            });
        });
</script>

<div style="border:groove;height:500px;width:200px;">
<div class="sec_header">
    Quick Search
</div>
<table>
    <tr>
        <td>
         
       <asp:TextBox ID="Txtsearch" runat="server" ClientIDMode="Static"></asp:TextBox>
                 <br />
                     <asp:Button ID="BtnQuickSearch" runat="server" Text="Search" ClientIDMode="Static"/>
            <asp:Button ID="btnHide" runat="server" ClientIDMode="Static" Text="Close" OnClick="btnHide_Click" />
       <br />
            </td></tr>
       <tr><td>
           <asp:Label ID="Callid" runat="server" ClientIDMode="Static"></asp:Label><br />
            </td>
    </tr>
</table>
    </div>





